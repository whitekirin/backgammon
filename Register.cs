using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;

namespace 五子棋_註冊_登錄
{
    public partial class Register : Form
    {
        private login initialization;

        private bool verify(string account, string passward, string confirm_passward) // 驗證textBox內有沒有資料
        {
            if (account.Equals("") && passward.Equals(""))
            {
                MessageBox.Show("帳號或密碼不能為空，請重新輸入");
                return false;
            }
            else if(!passward.Equals(confirm_passward))
            {
                MessageBox.Show("密碼兩個要相同，請重新輸入");
                return false;
            }
            else
                return true;
        }
        public Register()
        {
            InitializeComponent();
        }

        private bool select(string account)
        {
            string connectionString = @"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = D:\程式資料\21醫資系上課資料(視窗程式設計)\期末project\五子棋-註冊、登錄\五子棋-註冊、登錄\Database1.mdf;Integrated Security = True; MultipleActiveResultSets=True";

            SqlConnection sqlconnection = new SqlConnection(connectionString);
            sqlconnection.Open();
            SqlCommand sqlcommand = new SqlCommand("Select * From Player where Account_Id = @account", sqlconnection);
            sqlcommand.Parameters.Add("@account", SqlDbType.NVarChar).Value = account;

            SqlDataReader sqldatareader = sqlcommand.ExecuteReader(); //執行查詢
            if (sqldatareader.HasRows)
            {
                sqldatareader.Dispose();
                sqlcommand.Dispose();
                sqlconnection.Close();
                sqlconnection.Dispose();
                return false;
            }
            else
            {
                sqldatareader.Dispose();
                sqlcommand.Dispose();
                sqlconnection.Close();
                sqlconnection.Dispose();
                return true;
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            initialization = new login();

            this.Hide();
            initialization.ShowDialog();
            Application.ExitThread();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(verify(textBox1.Text, textBox2.Text, textBox3.Text))
            {
                if(select(textBox1.Text))
                {
                    string connectionString = @"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = D:\程式資料\21醫資系上課資料(視窗程式設計)\期末project\五子棋-註冊、登錄\五子棋-註冊、登錄\Database1.mdf;Integrated Security = True; MultipleActiveResultSets=True";

                    SqlConnection sqlconnection = new SqlConnection(connectionString);
                    sqlconnection.Open();

                    SqlCommand sqlcommand = new SqlCommand("Insert Into player (Account_Id,passward, previous_round,Time_Left,State,Chessboard_State) values (@account,@passward, @previous,@left,@state,@cheeseboard)", sqlconnection);
                    sqlcommand.Parameters.Add("@account", SqlDbType.NVarChar).Value = textBox1.Text;
                    sqlcommand.Parameters.Add("@passward", SqlDbType.NVarChar).Value = textBox2.Text;
                    sqlcommand.Parameters.Add("@previous", SqlDbType.NVarChar).Value = "None";
                    sqlcommand.Parameters.Add("@left", SqlDbType.NVarChar).Value = "60:00";
                    sqlcommand.Parameters.Add("@state", SqlDbType.NVarChar).Value = "None";
                    sqlcommand.Parameters.Add("@cheeseboard", SqlDbType.NVarChar).Value = "None";

                    sqlcommand.ExecuteNonQuery();

                    sqlcommand.Dispose();
                    sqlconnection.Close();
                    sqlconnection.Dispose();

                    MessageBox.Show("新增成功，若要返回主畫面則按下返回按鈕");
                    textBox1.Text = "";
                    textBox2.Text = "";
                    textBox3.Text = "";
                }
                else
                {
                    MessageBox.Show("已經註冊過，請輸入新的帳號");
                }
            }
        }
    }
}
