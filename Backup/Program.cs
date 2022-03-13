using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Backup
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// 
        public static LoginForm loginForm;
        public static BackupForm backupForm;

        public static SqlDataAdapter da;
        public static SqlConnection conn = new SqlConnection();
        public static SqlDataReader dataReader;

        public static string connStr = "";
        public static string serverName = "";
        public static string mLogin = "";
        public static string password = "";
        public static string Db ="";
        public static string database = "tempdb";
        public static string mlogin = "";
        public static int flagRestore = 0;
        public static string backupPath = "";
        public static string device_type = "Disk";


        public static int Connect()
        {
            if (Program.conn != null && Program.conn.State == ConnectionState.Open)
                Program.conn.Close();
            try
            {
                Program.connStr = "Data Source=" + Program.serverName + ";Initial Catalog=" +
                    Program.database + ";User ID=" +
                    Program.mLogin + ";password=" + Program.password;
                Program.conn.ConnectionString = Program.connStr;
                Program.conn.Open();
                return 1;
            }
            catch (Exception e)
            {
                MessageBox.Show("Lỗi kết nối cơ sở dữ liệu." + Environment.NewLine + "Bạn vui lòng xem lại username và pasword." + Environment.NewLine + "" + e.Message, "", MessageBoxButtons.OK);
                return 0;
            }
        }
        public static int ExecSqlNonQuery(string query)
        {
            SqlCommand sqlcmd = new SqlCommand(query, Program.conn);
            sqlcmd.CommandType = CommandType.Text;
            sqlcmd.CommandTimeout = 600;
            if (Program.conn.State == ConnectionState.Closed) Program.conn.Open();
            try
            {
                sqlcmd.ExecuteNonQuery();
                conn.Close();
                return 0;
            }
            catch (SqlException e)
            {
                if (e.Message.Contains("Error converting data type varchar to int"))
                    MessageBox.Show("Bạn format cell lại cột\"Ngày\" qua kiểu number hoặc mở file Exel.");
                else MessageBox.Show(e.Message);
                Program.conn.Close();
                return e.State;
            }
        }
        public static SqlDataReader ExecSqlDataReader(string query)
        {
            SqlDataReader reader;
            SqlCommand sqlcmd = new SqlCommand(query, Program.conn);
            sqlcmd.CommandType = CommandType.Text;
            sqlcmd.CommandTimeout = 300;

            if (Program.conn.State == ConnectionState.Closed) Program.conn.Open();
            try
            {
                reader = sqlcmd.ExecuteReader();
                return reader;
            }
            catch (SqlException ex)
            {

                MessageBox.Show(ex.Message);
                return null;
            }
        }
        public static DataTable ExecSqlDataTable(string cmd)
        {
            DataTable dt = new DataTable();
            
            if (Program.conn.State == ConnectionState.Closed) Program.conn.Open();
            SqlDataAdapter da = new SqlDataAdapter(cmd, conn);
            da.Fill(dt);
            conn.Close();
            return dt;
        }

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            loginForm = new LoginForm();
            Application.Run(loginForm);
        }
    }
}
