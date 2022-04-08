using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Collections.Generic;
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
        private int backup_set_id = 0;
        private List<int> list_backup_ids = new List<int>();

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
            string query = "use master " +
                "exec GetAllBackupInfo '@DBNAME'";
            //string query = "SELECT position, name, backup_start_date, user_name, backup_set_id " +
            //                "FROM msdb.dbo.backupset WHERE database_name = '@DBNAME' AND type = 'D' AND backup_set_id > (SELECT MAX(backup_set_id) - MAX(position) + 1 " +
            //                "FROM msdb.dbo.backupset WHERE media_set_id = (SELECT MAX(media_set_id) FROM msdb.dbo.backupset WHERE database_name = '@DBNAME' AND type = 'D')) " +
            //                "ORDER BY backup_start_date DESC";
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
                    backup_set_id = dt.Rows[0].Field<int>("backup_set_id");
                    foreach(DataRow item in dt.Rows)
                    {
                        list_backup_ids.Add(item.Field<int>("backup_set_id"));
                    }
                    btnDelete.Enabled = true;
                }
                else
                {
                    tbBackupCount.Text = "0";
                    btnDelete.Enabled = false;
                }
                LoadToolWhenHadDevice();
            }
            else
            {
                btnDelete.Enabled = false;
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
            bool isDevicePathExists = true;
            if (tbBackupCount.Text == "0")
            {
                MessageBox.Show("Chưa có bản sao lưu nào", "!", MessageBoxButtons.OK);
                return;
            }

            if (!IsDevicePathExists(devicePhysicalName))
            {
                MessageBox.Show("Đường dẫn device không hợp lệ.\nVui lòng chọn đường dẫn Device.", "Warning!", MessageBoxButtons.OK);
                OpenFileDialog folderBrowser = new OpenFileDialog();
                folderBrowser.ValidateNames = true;
                folderBrowser.CheckFileExists = true;
                folderBrowser.CheckPathExists = true;
                folderBrowser.FileName = "Folder Selection.";
                folderBrowser.Filter = "txt files (*.bak)|*.bak|All files (*.bak)|*.bak";
                if (folderBrowser.ShowDialog() == DialogResult.OK)
                {
                    string folderPath = Path.GetFullPath(folderBrowser.FileName);
                    devicePhysicalName = folderPath;
                    isDevicePathExists = false;
                }
                else
                {
                    return;
                }
            }
            if (Program.conn != null && Program.conn.State == ConnectionState.Open)
                Program.conn.Close(); // đóng kết nối 
            if (Program.Db.Trim() == "" || deviceName == NO_DEVICE) return;

            string query = "ALTER DATABASE " + Program.Db + " SET SINGLE_USER WITH ROLLBACK IMMEDIATE " + "USE tempdb ";
            if (cbTimeParam.Checked == false)
            {
                if (isDevicePathExists)
                {
                    query += "RESTORE DATABASE " + Program.Db + " FROM" + deviceName + " WITH FILE= " + backupPosition + ",REPLACE " + "ALTER DATABASE " + Program.Db + " SET MULTI_USER";
                }
                else
                {
                    query += "RESTORE DATABASE " + Program.Db + " FROM DISK='" + devicePhysicalName + "'" + " WITH FILE= " + backupPosition + ",REPLACE " + "ALTER DATABASE " + Program.Db + " SET MULTI_USER";
                }
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
                }
            }
        }

        private bool IsDevicePathExists(string devicePath)
        {
            if (File.Exists(devicePath))
            {
                return true;
            }
            return false;
        }

        private bool DeleteBackUp()
        {
            string query = "use master " +
                "EXEC DeleteBackup " + backup_set_id;
            Cursor.Current = Cursors.WaitCursor;

            if (Program.ExecSqlNonQuery(query) == 0)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show("Xóa bản backup thành công!", "", MessageBoxButtons.OK);
            }
            else
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show("Xóa thất bại!", "", MessageBoxButtons.OK);
            }
            Program.conn.Close();
            LoadDeviceAndBackups();
            return false;
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
                    backup_set_id = int.Parse(drv[4].ToString());
                    selectedBackupTime = (DateTime)(drv[2]);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (gvBackups.DataRowCount == 1)
            {
                MessageBox.Show("Không thể xóa tất cả bản backup.\nSử dụng tính năng xóa tất cả để thực hiện xóa bản backup cuối.", "");
                return;
            }
            DeleteBackUp();
        }

        #endregion
    }
}