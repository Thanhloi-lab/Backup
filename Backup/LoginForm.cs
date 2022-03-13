using System;
using System.Windows.Forms;

namespace Backup
{
    public partial class LoginForm : DevExpress.XtraEditors.XtraForm
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void login()
        {
            tbLoginName.Focus();
            if (tbServer.Text.Trim() == "" || tbLoginName.Text.Trim() == "" || tbPassword.Text.Trim() == "")
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!\nMời nhập lại.", "Login error!", MessageBoxButtons.OK);
                return;
            }
            Program.serverName = tbServer.Text.Trim();
            Program.mLogin = tbLoginName.Text.Trim();
            Program.password = tbPassword.Text.Trim();
            if (Program.Connect() == 0)
            {
                return;
            }
            Program.conn.Close();
            try
            {
                Program.backupForm = new BackupForm();
                Program.backupForm.Activate();
                Program.backupForm.Show();
                this.Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!", MessageBoxButtons.OK);
                tbLoginName.Focus();
                return;
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            login();
        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}