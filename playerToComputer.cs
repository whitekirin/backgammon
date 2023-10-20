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
using System.Globalization;

namespace 五子棋_註冊_登錄
{
    public partial class playerToComputer : Form
    {
        private login initialization;
        //private startGame start_game;
        private SqlConnection sqlconnection = null;
        private SqlCommand sqlcommand = null;
        private SqlDataReader sqldatareader = null;
        List<record> records = new List<record>();

        private struct record
        {
            public string account, time_left, state, Chessboard_State;
        }
        private bool verify(string account, string passward) // 驗證textBox內有沒有資料
        {
            if (account.Equals("") && passward.Equals(""))
                return false;
            else
                return true;
        }
        private bool select_sql_data(string account, string passward)
        {
            // 搜尋登入資料
            string connectionString = @"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = D:\程式資料\21醫資系上課資料(視窗程式設計)\期末project\五子棋-註冊、登錄\五子棋-註冊、登錄\Database1.mdf;Integrated Security = True; MultipleActiveResultSets=True";

            sqlconnection = new SqlConnection(connectionString);

            try
            {
                sqlconnection.Open();

                sqlcommand = new SqlCommand("Select * From Player where Account_Id = @account and passward = @passward", sqlconnection);
                sqlcommand.Parameters.Add("@account", SqlDbType.NVarChar).Value = account;
                sqlcommand.Parameters.Add("@passward", SqlDbType.NVarChar).Value = passward;

                sqldatareader = sqlcommand.ExecuteReader(); //執行查詢

                if (!sqldatareader.HasRows) // 若資料不存在資料庫中
                {
                    return false;
                }
                else // 若存在在資料庫中則進入遊玩畫面(等宇珊那邊成品好了在來思考這個要不要加上之前遊玩得紀錄)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
        } // 資料庫搜尋
        private void close()
        {
            //釋放物件及連線資源
            sqldatareader.Dispose();
            sqlcommand.Dispose();
            sqlconnection.Close();
            sqlconnection.Dispose();
        }

        private void insert_new_data(string account, string passward, string previous1, string previous2)
        {
            try
            {
                sqlcommand = new SqlCommand("Select * From Player where Account_Id = @account and previous_round = @previous", sqlconnection);
                sqlcommand.Parameters.Add("@account", SqlDbType.NVarChar).Value = account;
                sqlcommand.Parameters.Add("@previous", SqlDbType.NVarChar).Value = previous1;

                sqldatareader = sqlcommand.ExecuteReader(); //執行查詢

                if (!sqldatareader.HasRows) // 若資料不存在資料庫中
                {
                    sqlcommand = new SqlCommand("Select * From Player where Account_Id = @account and previous_round = @previous", sqlconnection);
                    sqlcommand.Parameters.Add("@account", SqlDbType.NVarChar).Value = account;
                    sqlcommand.Parameters.Add("@previous", SqlDbType.NVarChar).Value = previous2;

                    sqldatareader = sqlcommand.ExecuteReader();
                    if(!sqldatareader.HasRows)
                    {
                        sqlcommand = new SqlCommand("Insert Into player (Account_Id,passward, previous_round,Time_Left,State,Chessboard_State) values (@account,@passward, @previous,@left,@state,@cheeseboard)", sqlconnection);
                        sqlcommand.Parameters.Add("@account", SqlDbType.NVarChar).Value = account;
                        sqlcommand.Parameters.Add("@passward", SqlDbType.NVarChar).Value = passward;
                        sqlcommand.Parameters.Add("@previous", SqlDbType.NVarChar).Value = previous1;
                        sqlcommand.Parameters.Add("@left", SqlDbType.NVarChar).Value = "60:00";
                        sqlcommand.Parameters.Add("@state", SqlDbType.NVarChar).Value = "None";
                        sqlcommand.Parameters.Add("@cheeseboard", SqlDbType.NVarChar).Value = "None";

                        sqlcommand.ExecuteNonQuery();
                    }
                    else
                    {
                        sqlcommand = new SqlCommand("Update player Set previous_round = @previous Where Account_Id=@account", sqlconnection);
                        sqlcommand.Parameters.Add("@account", SqlDbType.NVarChar).Value = account;
                        sqlcommand.Parameters.Add("@previous", SqlDbType.NVarChar).Value = "computer";

                        sqlcommand.ExecuteNonQuery();
                    }
                }
                else // 若存在在資料庫中則進入遊玩畫面(等宇珊那邊成品好了在來思考這個要不要加上之前遊玩得紀錄)
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private record packeg_struct(string account, string left, string state, string chessboard)
        {
            record temp = new record();
            temp.account = account;
            temp.state = left;
            temp.time_left = state;
            temp.Chessboard_State = chessboard;

            return temp;
        }
        private bool search_format(string compare_string)
        {
           for(int i = 0; i < records.Count; i++)
            {
                if(records[i].account.Equals(compare_string) && !records[i].Chessboard_State.Equals("None"))
                {
                    return true;
                }
            }
            return false;
        }
        private void jump_to_new(int gamestate)
        {
            player start_game = new player(textBox1.Text, textBox3.Text, gamestate);
            this.Hide();
            start_game.ShowDialog();
            Application.ExitThread();
        }
        private void jump_to_new_computer(int gamestate)
        {
            computer start_computer_game = new computer(textBox1.Text, gamestate);
            this.Hide();
            start_computer_game.ShowDialog();
            Application.ExitThread();
        }
        public playerToComputer()
        {
            InitializeComponent();
            panel2.Visible = false;
            radioButton3.Enabled = false;
            radioButton4.Enabled = false;
        }

        // 當玩家點下登入的按鈕，要判斷的東西有: 
        // 1. 選擇的模式(玩家對玩家或玩家對電腦)
        // 2. 選擇上一局或選新遊戲
        // 3. 若選擇上一局，則搜尋資料庫資料，並找出對應的上一局資料
        private void button1_Click(object sender, EventArgs e)
        {
            bool judge = false;

            // 有輸入玩家一的帳號跟密碼
            if (verify(textBox1.Text, textBox2.Text))
            {
                // 搜尋該帳號的資料，true表示有找到資料
                judge = select_sql_data(textBox1.Text, textBox2.Text);
                if(judge)
                {
                    // 將資料庫中所有有關這個帳號的資料都存下來
                    while (sqldatareader.Read())
                        records.Add(packeg_struct(sqldatareader["previous_round"].ToString(), sqldatareader["Time_Left"].ToString(), sqldatareader["State"].ToString(), sqldatareader["Chessboard_State"].ToString()));

                    // 判斷選擇玩家 + 模式
                    if (radioButton1.Checked)
                    { 
                        if (radioButton5.Checked) // 新遊戲
                        {
                            insert_new_data(textBox1.Text, textBox2.Text, "computer", "None");
                            // 關閉所有連線資源
                            close();
                            // 跳到玩家對電腦的新遊戲中(直接跳轉)
                            jump_to_new_computer(1);
                        }
                        else if(radioButton6.Checked) // 電腦對玩家的上一局
                        {
                            if (search_format("computer"))
                            {
                                // 關閉所有連線資源
                                close();
                                jump_to_new_computer(2);
                            }
                            else
                                MessageBox.Show("這場遊戲未開始或已經結束，請開啟一場新的遊戲");
                        }
                    }
                   
                    // 玩家對玩家
                    else if(verify(textBox3.Text, textBox4.Text))
                    {
                        judge = select_sql_data(textBox3.Text, textBox4.Text);
                        if(judge)
                        {
                            // 將資料庫中所有有關這個帳號的資料都存下來
                            while (sqldatareader.Read())
                                records.Add(packeg_struct(sqldatareader["previous_round"].ToString(), sqldatareader["Time_Left"].ToString(), sqldatareader["State"].ToString(), sqldatareader["Chessboard_State"].ToString()));
                            //if (!search_format("None") && !search_format(textBox3.Text))
                            //    insert_new_data(textBox1.Text, textBox3.Text);
                            //if (!search_format("None") && !search_format(textBox1.Text))
                            //insert_new_data(textBox3.Text, textBox1.Text);
                            if (radioButton5.Checked)
                            {
                                insert_new_data(textBox1.Text, textBox2.Text, textBox3.Text, "None");
                                insert_new_data(textBox3.Text, textBox4.Text, textBox1.Text, "None");
                                // 關閉所有連線資源
                                close();
                                // 跳到玩家對電腦的新遊戲中(直接跳轉)
                                jump_to_new(1);
                            }
                            else if (radioButton6.Checked)
                            {
                                if(search_format(textBox3.Text))
                                {
                                    // 關閉所有連線資源
                                    close();
                                    jump_to_new(2);
                                }
                                else
                                    MessageBox.Show("這場遊戲未開始或已經結束，請開啟一場新的遊戲");
                            }
                        }
                        else
                            MessageBox.Show("玩家二帳號或密碼輸入錯誤，請重新輸入");
                    }
                }
                else
                    MessageBox.Show("玩家一帳號或密碼輸入錯誤，請重新輸入");
            }
            else
                MessageBox.Show("帳號或密碼不能為空，請重新輸入");
        }

        private void button2_Click(object sender, EventArgs e) // 跳轉回初始畫面
        {
            initialization = new login();

            this.Hide();
            initialization.ShowDialog();
            Application.ExitThread();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            radioButton3.Enabled = true;
            radioButton4.Enabled = true;
            panel1.Visible = true;
            panel2.Visible = false;
            panel5.Visible = false;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            radioButton3.Enabled = true;
            radioButton4.Enabled = true;
            panel1.Visible = true;
            panel2.Visible = true;
            panel5.Visible = false;
        }

        private void playerToComputer_FormClosing(object sender, FormClosingEventArgs e)
        {
            initialization = new login();

            this.Hide();
            initialization.ShowDialog();
            Application.ExitThread();
        }

        private void radioButton7_CheckedChanged(object sender, EventArgs e)
        {
            panel1.Visible = false;
            panel2.Visible = false;
            radioButton3.Enabled = false;
            radioButton4.Enabled = false;
            panel5.Location = panel1.Location;
            panel5.Visible = true;
            panel6.Visible = false;
        }

        private void GetData(string account)
        {
            try
            {
                string connectionString = @"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = D:\程式資料\21醫資系上課資料(視窗程式設計)\期末project\五子棋-註冊、登錄\五子棋-註冊、登錄\Database1.mdf;Integrated Security = True; MultipleActiveResultSets=True";
                string selectCommand = "Select * From play_record where account = '" + account + "'";
                // Specify a connection string.
                // Replace <SQL Server> with the SQL Server for your Northwind sample database.
                // Replace "Integrated Security=True" with user login information if necessary.

                // Create a new data adapter based on the specified query.
                SqlDataAdapter dataAdapter = new SqlDataAdapter(selectCommand, connectionString);

                // Create a command builder to generate SQL update, insert, and
                // delete commands based on selectCommand.
                SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapter);

                // Populate a new data table and bind it to the BindingSource.
                DataTable table = new DataTable
                {
                    Locale = CultureInfo.InvariantCulture
                };
                dataAdapter.Fill(table);
                bindingSource1.DataSource = table;

                // Resize the DataGridView columns to fit the newly loaded content.
                dataGridView1.AutoResizeColumns(
                    DataGridViewAutoSizeColumnsMode.AllCells);
            }
            catch (SqlException)
            {
                MessageBox.Show("To run this example, replace the value of the " +
                    "connectionString variable with a connection string that is " +
                    "valid for your system.");
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox5.Text != "")
            {
                panel6.Location = panel1.Location;
                panel5.Visible = false;
                panel6.Visible = true;

                dataGridView1.DataSource = bindingSource1;
                GetData(textBox5.Text);
            }
            else
                MessageBox.Show("請輸入搜尋對象");
            textBox5.Text = "";
        }
    }
}