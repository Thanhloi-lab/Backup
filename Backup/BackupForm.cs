using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace Backup
{
    public partial class BackupForm : DevExpress.XtraEditors.XtraForm
    {
        private string deviceName = "";
        private string devicePhysicalName = "";
        private const string NO_DEVICE = "Không có device";
        private int backupPosition = 0;
        private DateTime selectedBackupTime;

        public BackupForm()
        {
            InitializeComponent();
        }

        #region methods

        private void LoadAllDataBase()
        {
            string query = "SELECT Name, database_id " +
                "FROM sys.databases " +
                "WHERE(database_id >= 5) AND " +
                "(NOT(name LIKE N'ReportServer%')) " +
                "ORDER BY NAME";

            Cursor.Current = Cursors.WaitCursor;

            if (Program.Connect() == 0)
            {
                MessageBox.Show("Có lỗi trong quá trình xử lý.");
                return;
            }

            DataTable dt = Program.ExecSqlDataTable(query);
            if (dt == null) return;
            gcDbName.DataSource = dt;

            Cursor.Current = Cursors.Default;
        }

        private DataTable GetDatabaseBackupInfo()
        {
            //string query = "exec GetAllBackupInfo @DBNAME=N'" + Program.Db + "'";
            string query = "SELECT position, name ,backup_start_date , user_name " +
                            "FROM msdb.dbo.backupset WHERE database_name = '@DBNAME' AND type = 'D' AND backup_set_id >= (SELECT MAX(backup_set_id) " +
                            "FROM msdb.dbo.backupset WHERE media_set_id = (SELECT MAX(media_set_id) FROM msdb.dbo.backupset WHERE database_name = '@DBNAME' AND type = 'D')  AND position = 1 ) " +
                            "ORDER BY backup_start_date DESC";
            query = query.Replace("@DBNAME", Program.Db);
            Cursor.Current = Cursors.WaitCursor;

            if (Program.Connect() == 0)
            {
                MessageBox.Show("Có lỗi trong quá trình xử lý.");
                return null;
            }
            DataTable dt = Program.ExecSqlDataTable(query);
            if (dt == null) return null;
            gcBackups.DataSource = dt;

            Cursor.Current = Cursors.Default;

            return dt;
        }

        private DataTable GetDeviceByName(string deviceName)
        {
            string query = "SELECT * FROM sys.backup_devices WHERE name = '" + deviceName + "'";

            Cursor.Current = Cursors.WaitCursor;

            if (Program.Connect() == 0)
            {
                MessageBox.Show("Có lỗi trong quá trình xử lý.");
                return null;
            }
            DataTable dt = Program.ExecSqlDataTable(query);
            if (dt == null) return null;

            Cursor.Current = Cursors.Default;

            return dt;
        }

        private void LoadToolWhenHadDevice()
        {
            btnBackup.Enabled = btnRestore.Enabled = true;
            cbDeleteAll.Enabled = cbTimeParam.Enabled = true;
        }

        private void LoadToolWhenNotHadDevice()
        {
            btnBackup.Enabled = btnRestore.Enabled = false;
            btnCreateDevice.Enabled = true;
            cbDeleteAll.Enabled = cbTimeParam.Enabled = false;
        }

        private void LoadDefautTool()
        {
            btnBackup.Enabled = btnCreateDevice.Enabled = btnRestore.Enabled = false;
            cbDeleteAll.Enabled = cbTimeParam.Enabled = false;
        }

        private void LoadDeviceAndBackups()
        {
            Cursor.Current = Cursors.WaitCursor;

            deviceName = "DEVICE_" + Program.Db;
            DataTable dt = GetDatabaseBackupInfo();
            DataTable dtDevice = GetDeviceByName("DEVICE_" + Program.Db);

            Cursor.Current = Cursors.Default;
            if (dtDevice.Rows.Count > 0)
            {
                deviceName = tbDeviceName.Text = dtDevice.Rows[0].Field<string>(0);
                devicePhysicalName = dtDevice.Rows[0].Field<string>(3);
                if (dt != null && dt.Rows.Count > 0)
                {
                    tbBackupCount.Text = dt.Rows.Count.ToString();
                }
                else
                {
                    tbBackupCount.Text = "0";
                }
                LoadToolWhenHadDevice();
            }
            else
            {
                tbDeviceName.Text = NO_DEVICE;
                LoadToolWhenNotHadDevice();
            }
        }

        private void CreateNewDevice()
        {
            string query = "";
            OpenFileDialog folderBrowser = new OpenFileDialog();
            folderBrowser.ValidateNames = false;
            folderBrowser.CheckFileExists = false;
            folderBrowser.CheckPathExists = true;
            folderBrowser.FileName = "Folder Selection.";
            if (folderBrowser.ShowDialog() == DialogResult.OK)
            {
                string folderPath = Path.GetDirectoryName(folderBrowser.FileName);
                devicePhysicalName = folderPath + "\\";
                devicePhysicalName = devicePhysicalName + deviceName + ".BAK";

                query = "USE master\n EXEC sp_addumpdevice " +
                "'" + Program.device_type + "', '" + deviceName + "','" + devicePhysicalName + "'";

                try
                {
                    Cursor.Current = Cursors.WaitCursor;
                    Program.dataReader = Program.ExecSqlDataReader(query);
                    Cursor.Current = Cursors.Default;
                    if (Program.dataReader != null)
                    {
                        LoadDeviceAndBackups();
                        MessageBox.Show("Tạo Device thành công!", "", MessageBoxButtons.OK);
                    }
                    else MessageBox.Show(" Tạo Device thất bại!", "", MessageBoxButtons.OK);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK);
                }
                finally
                {
                    Program.conn.Close();
                }
            }
        }

        private void CreateNewBackup()
        {
            if (Program.Db.Trim() == "" || deviceName == NO_DEVICE) return;

            string query = "BACKUP DATABASE " + Program.Db + " TO " + deviceName;
            if (cbDeleteAll.Checked == true)
            {
                if (MessageBox.Show("Bạn có muốn xóa các sao lưu cũ ?", "Xác nhận", MessageBoxButtons.OKCancel) == DialogResult.OK)
                    query = query + " WITH INIT";
            }

            try
            { 
                Cursor.Current = Cursors.WaitCursor;

                Program.dataReader = Program.ExecSqlDataReader(query);

                Cursor.Current = Cursors.Default;
                if (Program.dataReader != null)
                {
                    MessageBox.Show(" Tạo Backup thành công!", "", MessageBoxButtons.OK);
                }
                else MessageBox.Show(" Tạo Backup thất bại!", "", MessageBoxButtons.OK);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error" + ex, "!", MessageBoxButtons.OK);
                return;
            }
            Program.conn.Close();
            LoadDeviceAndBackups();
        }

        private void Restore()
        {
            if (tbBackupCount.Text == "0")
            {
                MessageBox.Show("Chưa có bản sao lưu nào", "!", MessageBoxButtons.OK);
                return;
            }
            if (Program.conn != null && Program.conn.State == ConnectionState.Open)
                Program.conn.Close(); // đóng kết nối 
            if (Program.Db.Trim() == "" || deviceName == NO_DEVICE) return;

            string query = "ALTER DATABASE " + Program.Db + " SET SINGLE_USER WITH ROLLBACK IMMEDIATE " + "USE tempdb ";
            if (cbTimeParam.Checked == false)
            {
                query += "RESTORE DATABASE " + Program.Db + " FROM " + deviceName + " WITH FILE= " + backupPosition + ",REPLACE " + "ALTER DATABASE " + Program.Db + " SET MULTI_USER";
                Cursor.Current = Cursors.WaitCursor;
                if (Program.ExecSqlNonQuery(query) == 0)
                {
                    Cursor.Current = Cursors.Default;
                    MessageBox.Show(" Phục hồi thành công!", "", MessageBoxButtons.OK);
                }
                else
                {
                    Cursor.Current = Cursors.Default;
                    MessageBox.Show(" Phục hồi thất bại!", "", MessageBoxButtons.OK);
                }
            }
            else
            {
                
                DateTime dateStop = dtpDate.Value.Date + dtpHour.Value.TimeOfDay;
                string dt = dtpDate.Value.ToString("yyyy-MM-dd") + " " + dtpHour.Value.ToString("HH:mm:ss");
                if (DateTime.Compare(selectedBackupTime, dateStop) >=0)
                {
                    MessageBox.Show("Thời điểm muốn phục hồi phải sau thời điểm sao lưu đã chọn", "", MessageBoxButtons.OK);
                    return;
                }
                else if(DateTime.Now < dateStop)
                {
                    MessageBox.Show(" Thời điểm muốn phục hồi phải trước thời điểm hiện tại", "", MessageBoxButtons.OK);
                    return;
                }
                else
                {
                    //if (Directory.Exists(devicePhysicalName) == true)
                    //{
                        string strFullPathBackLog = devicePhysicalName.Replace(".BAK", ".TRN");
                        query += "BACKUP LOG " + Program.Db + " TO DISK='" + strFullPathBackLog + "' WITH INIT\n "
                            + "RESTORE DATABASE " + Program.Db + " FROM DISK='" + devicePhysicalName + "' WITH NORECOVERY , REPLACE\n "
                            + "RESTORE DATABASE " + Program.Db + " FROM DISK='" + strFullPathBackLog + "' WITH STOPAT='" + dt
                            + "'\n ALTER DATABASE " + Program.Db + " SET MULTI_USER";

                        MessageBox.Show(" " + query, "", MessageBoxButtons.OK);

                        Cursor.Current = Cursors.WaitCursor;

                        if (Program.ExecSqlNonQuery(query)==0)
                        {
                            Cursor.Current = Cursors.Default;
                            MessageBox.Show(" Phục hồi thời gian lúc " + dt + " Thành công!", "", MessageBoxButtons.OK);
                        }
                        else
                        {
                            Cursor.Current = Cursors.Default;
                            MessageBox.Show(" Sai đường dẫn backup . Kiểm tra lại  ", "", MessageBoxButtons.OK);
                        }
                    //}

                    //else
                    //{
                    //    return;
                    //}
                    
                }
            }
        }

        private void RestoreDiaglog(String dtStopAt)
        {
            //DialogResult rsdiaglog = MessageBox.Show("Thời gian phục hồi: " + dtStopAt, Program.Db + " - Restore dialog", MessageBoxButtons.OKCancel);
            //if (rsdiaglog == DialogResult.OK)
            //{
            //    String strFullPathBackLog = Program.strDefaultPath + Program.Db + ".TRN";
            //    strRestore += "BACKUP LOG " + Program.Db + " TO DISK='" + strFullPathBackLog + "' WITH INIT\n "
            //       + "RESTORE DATABASE " + Program.Db + " FROM DISK='" + strFullPathDevice + "' WITH NORECOVERY , REPLACE\n "
            //        + "RESTORE DATABASE " + Program.Db + " FROM DISK='" + strFullPathBackLog + "' WITH STOPAT='" + dtStopAt
            //        + "'\n ALTER DATABASE " + Program.Db + " SET MULTI_USER";
            //    int checkErr = Program.ExecSqlNonQuery(strRestore, Program.connstr, "lỗi phục hồi cơ sở dữ liệu");
            //    if (checkErr == 0)
            //    {
            //        int i;
            //        this.PrgLoad.Visible = true;
            //        this.PrgLoad.Minimum = 0;
            //        this.PrgLoad.Maximum = 100;
            //        this.PrgLoad.Step = 20;
            //        for (i = this.PrgLoad.Minimum; i <= this.PrgLoad.Maximum; i++)
            //        {
            //            this.PrgLoad.Value = i;
            //            PrgLoad.PerformStep();
            //            Thread.Sleep(10);

            //        }
            //        PrgLoad.Visible = false;
            //        MessageBox.Show(" Phục hồi thời gian lúc " + dtStopAt + " Thành công!", "", MessageBoxButtons.OK);

            //    }
            //    else
            //    {
            //        MessageBox.Show(" Phục hồi thất bại ", "", MessageBoxButtons.OK);
            //    }
            //}

            //else
            //{
            //    return;
            //}
        }


        #endregion


        #region Events

        private void gvDbName_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            GridView gv = sender as GridView;
            DataRowView drv = gv.GetRow(e.FocusedRowHandle) as DataRowView;

            if (drv != null)
            {
                try
                {
                    Program.Db = tbDbName.Text = drv[0].ToString();
                    LoadDeviceAndBackups();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }
            }
        }

        private void BackupForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn thoát không?", "Notification", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                Program.loginForm.Visible = true;
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void BackupForm_Load(object sender, System.EventArgs e)
        {
            LoadDefautTool();
            LoadAllDataBase();
        }

        private void cbTimeParam_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb = sender as CheckBox;
            if (cb.Checked)
            {
                pnNote.Visible = pnRestoreTime.Visible = true;
            }
            else
            {
                pnNote.Visible = pnRestoreTime.Visible = false;
            }
        }

        private void btnCreateDevice_Click(object sender, EventArgs e)
        {
            CreateNewDevice();
        }

        private void btnBackup_Click(object sender, EventArgs e)
        {
            CreateNewBackup();
        }

        private void btnRestore_Click(object sender, EventArgs e)
        {
            Restore();
        }

        private void gvBackups_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            GridView gv = sender as GridView;
            DataRowView drv = gv.GetRow(e.FocusedRowHandle) as DataRowView;

            if (drv != null)
            {
                try
                {
                    backupPosition = int.Parse(drv[0].ToString());
                    selectedBackupTime = (DateTime)(drv[2]);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }
            }
        }

        #endregion


    }
}