
namespace Backup
{
    partial class BackupForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.gcDbName = new DevExpress.XtraGrid.GridControl();
            this.gvDbName = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.DataBaseName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnReload = new System.Windows.Forms.Button();
            this.btnCreateDevice = new System.Windows.Forms.Button();
            this.btnRestore = new System.Windows.Forms.Button();
            this.tbBackupCount = new System.Windows.Forms.TextBox();
            this.btnBackup = new System.Windows.Forms.Button();
            this.tbDeviceName = new System.Windows.Forms.TextBox();
            this.tbDbName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cbDeleteAll = new System.Windows.Forms.CheckBox();
            this.cbTimeParam = new System.Windows.Forms.CheckBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.gcBackups = new DevExpress.XtraGrid.GridControl();
            this.gvBackups = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.Position = new DevExpress.XtraGrid.Columns.GridColumn();
            this.BackupDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.UserBackup = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pnRestoreTime = new System.Windows.Forms.Panel();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.dtpHour = new System.Windows.Forms.DateTimePicker();
            this.pnNote = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.gcDbName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvDbName)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcBackups)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvBackups)).BeginInit();
            this.panel2.SuspendLayout();
            this.pnRestoreTime.SuspendLayout();
            this.pnNote.SuspendLayout();
            this.SuspendLayout();
            // 
            // gcDbName
            // 
            this.gcDbName.Dock = System.Windows.Forms.DockStyle.Left;
            this.gcDbName.EmbeddedNavigator.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gcDbName.EmbeddedNavigator.Appearance.Options.UseFont = true;
            this.gcDbName.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gcDbName.Location = new System.Drawing.Point(0, 0);
            this.gcDbName.MainView = this.gvDbName;
            this.gcDbName.Name = "gcDbName";
            this.gcDbName.Size = new System.Drawing.Size(281, 598);
            this.gcDbName.TabIndex = 1;
            this.gcDbName.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvDbName});
            // 
            // gvDbName
            // 
            this.gvDbName.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.DataBaseName});
            this.gvDbName.GridControl = this.gcDbName;
            this.gvDbName.Name = "gvDbName";
            this.gvDbName.OptionsBehavior.Editable = false;
            this.gvDbName.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gvDbName_FocusedRowChanged);
            // 
            // DataBaseName
            // 
            this.DataBaseName.AppearanceCell.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DataBaseName.AppearanceCell.Options.UseFont = true;
            this.DataBaseName.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DataBaseName.AppearanceHeader.Options.UseFont = true;
            this.DataBaseName.Caption = "Name";
            this.DataBaseName.FieldName = "Name";
            this.DataBaseName.Name = "DataBaseName";
            this.DataBaseName.Visible = true;
            this.DataBaseName.VisibleIndex = 0;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.btnReload);
            this.panel1.Controls.Add(this.btnCreateDevice);
            this.panel1.Controls.Add(this.btnRestore);
            this.panel1.Controls.Add(this.tbBackupCount);
            this.panel1.Controls.Add(this.btnBackup);
            this.panel1.Controls.Add(this.tbDeviceName);
            this.panel1.Controls.Add(this.tbDbName);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(281, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(798, 106);
            this.panel1.TabIndex = 2;
            // 
            // btnReload
            // 
            this.btnReload.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReload.Location = new System.Drawing.Point(420, 9);
            this.btnReload.Name = "btnReload";
            this.btnReload.Size = new System.Drawing.Size(90, 60);
            this.btnReload.TabIndex = 12;
            this.btnReload.Text = "Reload";
            this.btnReload.UseVisualStyleBackColor = true;
            // 
            // btnCreateDevice
            // 
            this.btnCreateDevice.Enabled = false;
            this.btnCreateDevice.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCreateDevice.Location = new System.Drawing.Point(420, 75);
            this.btnCreateDevice.Name = "btnCreateDevice";
            this.btnCreateDevice.Size = new System.Drawing.Size(355, 27);
            this.btnCreateDevice.TabIndex = 10;
            this.btnCreateDevice.Text = "Create new device";
            this.btnCreateDevice.UseVisualStyleBackColor = true;
            this.btnCreateDevice.Click += new System.EventHandler(this.btnCreateDevice_Click);
            // 
            // btnRestore
            // 
            this.btnRestore.Enabled = false;
            this.btnRestore.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRestore.Location = new System.Drawing.Point(674, 9);
            this.btnRestore.Name = "btnRestore";
            this.btnRestore.Size = new System.Drawing.Size(101, 60);
            this.btnRestore.TabIndex = 9;
            this.btnRestore.Text = "Restore";
            this.btnRestore.UseVisualStyleBackColor = true;
            this.btnRestore.Click += new System.EventHandler(this.btnRestore_Click);
            // 
            // tbBackupCount
            // 
            this.tbBackupCount.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbBackupCount.ForeColor = System.Drawing.Color.Red;
            this.tbBackupCount.Location = new System.Drawing.Point(154, 6);
            this.tbBackupCount.Name = "tbBackupCount";
            this.tbBackupCount.ReadOnly = true;
            this.tbBackupCount.Size = new System.Drawing.Size(244, 27);
            this.tbBackupCount.TabIndex = 5;
            // 
            // btnBackup
            // 
            this.btnBackup.Enabled = false;
            this.btnBackup.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBackup.Location = new System.Drawing.Point(543, 9);
            this.btnBackup.Name = "btnBackup";
            this.btnBackup.Size = new System.Drawing.Size(100, 60);
            this.btnBackup.TabIndex = 8;
            this.btnBackup.Text = "Backup";
            this.btnBackup.UseVisualStyleBackColor = true;
            this.btnBackup.Click += new System.EventHandler(this.btnBackup_Click);
            // 
            // tbDeviceName
            // 
            this.tbDeviceName.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbDeviceName.ForeColor = System.Drawing.Color.Red;
            this.tbDeviceName.Location = new System.Drawing.Point(154, 71);
            this.tbDeviceName.Name = "tbDeviceName";
            this.tbDeviceName.ReadOnly = true;
            this.tbDeviceName.Size = new System.Drawing.Size(244, 27);
            this.tbDeviceName.TabIndex = 4;
            // 
            // tbDbName
            // 
            this.tbDbName.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbDbName.ForeColor = System.Drawing.Color.Red;
            this.tbDbName.Location = new System.Drawing.Point(154, 38);
            this.tbDbName.Name = "tbDbName";
            this.tbDbName.ReadOnly = true;
            this.tbDbName.Size = new System.Drawing.Size(244, 27);
            this.tbDbName.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(6, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(132, 19);
            this.label3.TabIndex = 2;
            this.label3.Text = "Backups count:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(6, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(119, 19);
            this.label2.TabIndex = 1;
            this.label2.Text = "Device name:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(142, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "Database name:";
            // 
            // cbDeleteAll
            // 
            this.cbDeleteAll.AutoSize = true;
            this.cbDeleteAll.Enabled = false;
            this.cbDeleteAll.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbDeleteAll.ForeColor = System.Drawing.Color.Red;
            this.cbDeleteAll.Location = new System.Drawing.Point(47, 43);
            this.cbDeleteAll.Name = "cbDeleteAll";
            this.cbDeleteAll.Size = new System.Drawing.Size(170, 23);
            this.cbDeleteAll.TabIndex = 7;
            this.cbDeleteAll.Text = "Detele all backup";
            this.cbDeleteAll.UseVisualStyleBackColor = true;
            // 
            // cbTimeParam
            // 
            this.cbTimeParam.AutoSize = true;
            this.cbTimeParam.Enabled = false;
            this.cbTimeParam.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbTimeParam.ForeColor = System.Drawing.Color.Red;
            this.cbTimeParam.Location = new System.Drawing.Point(47, 14);
            this.cbTimeParam.Name = "cbTimeParam";
            this.cbTimeParam.Size = new System.Drawing.Size(170, 23);
            this.cbTimeParam.TabIndex = 6;
            this.cbTimeParam.Text = "With Time Param";
            this.cbTimeParam.UseVisualStyleBackColor = true;
            this.cbTimeParam.CheckedChanged += new System.EventHandler(this.cbTimeParam_CheckedChanged);
            // 
            // gcBackups
            // 
            this.gcBackups.Dock = System.Windows.Forms.DockStyle.Top;
            this.gcBackups.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gcBackups.Location = new System.Drawing.Point(281, 106);
            this.gcBackups.MainView = this.gvBackups;
            this.gcBackups.Name = "gcBackups";
            this.gcBackups.Size = new System.Drawing.Size(798, 302);
            this.gcBackups.TabIndex = 3;
            this.gcBackups.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvBackups});
            // 
            // gvBackups
            // 
            this.gvBackups.Appearance.Row.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gvBackups.Appearance.Row.Options.UseFont = true;
            this.gvBackups.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.Position,
            this.BackupDate,
            this.UserBackup});
            this.gvBackups.GridControl = this.gcBackups;
            this.gvBackups.Name = "gvBackups";
            this.gvBackups.OptionsBehavior.Editable = false;
            this.gvBackups.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gvBackups_FocusedRowChanged);
            // 
            // Position
            // 
            this.Position.Caption = "Position";
            this.Position.FieldName = "position";
            this.Position.Name = "Position";
            this.Position.Visible = true;
            this.Position.VisibleIndex = 0;
            // 
            // BackupDate
            // 
            this.BackupDate.Caption = "Backup date";
            this.BackupDate.DisplayFormat.FormatString = "yyyy-MM-dd HH:mm:ss";
            this.BackupDate.FieldName = "backup_start_date";
            this.BackupDate.Name = "BackupDate";
            this.BackupDate.Visible = true;
            this.BackupDate.VisibleIndex = 1;
            // 
            // UserBackup
            // 
            this.UserBackup.Caption = "User backup";
            this.UserBackup.FieldName = "user_name";
            this.UserBackup.Name = "UserBackup";
            this.UserBackup.Visible = true;
            this.UserBackup.VisibleIndex = 2;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.pnRestoreTime);
            this.panel2.Controls.Add(this.pnNote);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.cbDeleteAll);
            this.panel2.Controls.Add(this.cbTimeParam);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(281, 408);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(798, 190);
            this.panel2.TabIndex = 4;
            // 
            // pnRestoreTime
            // 
            this.pnRestoreTime.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnRestoreTime.Controls.Add(this.dtpDate);
            this.pnRestoreTime.Controls.Add(this.label4);
            this.pnRestoreTime.Controls.Add(this.dtpHour);
            this.pnRestoreTime.Location = new System.Drawing.Point(243, 14);
            this.pnRestoreTime.Name = "pnRestoreTime";
            this.pnRestoreTime.Size = new System.Drawing.Size(419, 52);
            this.pnRestoreTime.TabIndex = 11;
            this.pnRestoreTime.Visible = false;
            // 
            // dtpDate
            // 
            this.dtpDate.CustomFormat = "yyyy-MM-dd";
            this.dtpDate.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDate.Location = new System.Drawing.Point(134, 8);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.dtpDate.Size = new System.Drawing.Size(124, 27);
            this.dtpDate.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(2, 14);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(101, 19);
            this.label4.TabIndex = 3;
            this.label4.Text = "Restore to:";
            // 
            // dtpHour
            // 
            this.dtpHour.CustomFormat = "HH:mm:ss";
            this.dtpHour.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpHour.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpHour.Location = new System.Drawing.Point(299, 8);
            this.dtpHour.Name = "dtpHour";
            this.dtpHour.ShowUpDown = true;
            this.dtpHour.Size = new System.Drawing.Size(99, 27);
            this.dtpHour.TabIndex = 5;
            // 
            // pnNote
            // 
            this.pnNote.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnNote.Controls.Add(this.label5);
            this.pnNote.Controls.Add(this.label6);
            this.pnNote.Location = new System.Drawing.Point(41, 75);
            this.pnNote.Name = "pnNote";
            this.pnNote.Size = new System.Drawing.Size(621, 78);
            this.pnNote.TabIndex = 10;
            this.pnNote.Visible = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Top;
            this.label5.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(0, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 19);
            this.label5.TabIndex = 6;
            this.label5.Text = "Note: ";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(74, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(481, 114);
            this.label6.TabIndex = 7;
            this.label6.Text = "The time you choose that is your database will restore to.\r\nThis time must be aft" +
    "er your backup\'s time you choose in the table\r\nand before current time at least " +
    "1 minute.\r\n\r\n\r\n\r\n";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(102, 143);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(9, 38);
            this.label8.TabIndex = 9;
            this.label8.Text = "\r\n\r\n";
            // 
            // BackupForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1079, 598);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.gcBackups);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.gcDbName);
            this.Name = "BackupForm";
            this.Text = "BackupForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.BackupForm_FormClosing);
            this.Load += new System.EventHandler(this.BackupForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gcDbName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvDbName)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcBackups)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvBackups)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.pnRestoreTime.ResumeLayout(false);
            this.pnRestoreTime.PerformLayout();
            this.pnNote.ResumeLayout(false);
            this.pnNote.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraGrid.GridControl gcDbName;
        private DevExpress.XtraGrid.Views.Grid.GridView gvDbName;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnCreateDevice;
        private System.Windows.Forms.Button btnRestore;
        private System.Windows.Forms.Button btnBackup;
        private System.Windows.Forms.CheckBox cbDeleteAll;
        private System.Windows.Forms.CheckBox cbTimeParam;
        private System.Windows.Forms.TextBox tbBackupCount;
        private System.Windows.Forms.TextBox tbDeviceName;
        private System.Windows.Forms.TextBox tbDbName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private DevExpress.XtraGrid.GridControl gcBackups;
        private DevExpress.XtraGrid.Views.Grid.GridView gvBackups;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel pnNote;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DateTimePicker dtpHour;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel pnRestoreTime;
        private new DevExpress.XtraGrid.Columns.GridColumn DataBaseName;
        private System.Windows.Forms.Button btnReload;
        private DevExpress.XtraGrid.Columns.GridColumn Position;
        private DevExpress.XtraGrid.Columns.GridColumn BackupDate;
        private DevExpress.XtraGrid.Columns.GridColumn UserBackup;
    }
}