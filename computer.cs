using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 五子棋_註冊_登錄
{
    public partial class computer : Form
    {
        public computer(string player1, int gamestate)
        {
            InitializeComponent();
            set_Dictionary();
            account1 = player1;
            state = gamestate;
        }
        Dictionary<string, string> cheese_root = new Dictionary<string, string>();

        struct record
        {
            public string x, y;
            public string color, state;
        }
        List<record> records = new List<record>();
        string account1;
        int[,] p = new int[9, 9];//紀錄下棋狀況的陣列
        int timeleft, wait = 1;
        int i = 0, number = 0, state = 0;
        Electro electro = new Electro();

        SqlConnection sqlconnection;
        SqlCommand sqlcommand;
        SqlDataReader sqldatareader;

        private void save_the_game_and_jump_out()
        {
            string root = "", roots = "";
            for (int i = 0; i < records.Count; i++)
            {
                string value = records[i].x + "," + records[i].y;
                foreach (var values in cheese_root)
                {
                    if (values.Value.Equals(value))
                        root = values.Key;
                }
                roots += records[i].color + "," + root;
                if (i != records.Count - 1)
                    roots += ",";
            }

            if (records.Count == 0)
                update_data(show_name1.Text, timeleft.ToString(), "None", "None");
            else
                update_data(show_name1.Text, timeleft.ToString(), "None", roots);

            timer1.Stop();
            timer2.Stop();

            records.Clear();
            jump_to_login();
        }
        private void set_Dictionary()
        {
            for (int i = 0; i < 9; i++)
                for (int j = 0; j < 9; j++)
                {
                    char character = (char)(i + 65);
                    string root = character.ToString() + j.ToString();
                    cheese_root.Add(root, ((j * 40).ToString() + "," + (i * 40).ToString()));
                }

        }
        private void select_sql_data(string account) // 資料庫搜尋
        {
            // 搜尋登入資料
            //string connectionString = @"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = D:\程式資料\21醫資系上課資料(視窗程式設計)\期末project\五子棋-註冊、登錄\五子棋-註冊、登錄;Integrated Security = True; MultipleActiveResultSets=True";

            sqlcommand = new SqlCommand("Select * From Player where Account_Id = @account and previous_round = @previous", sqlconnection);
            sqlcommand.Parameters.Add("@account", SqlDbType.NVarChar).Value = account;
            sqlcommand.Parameters.Add("@previous", SqlDbType.NVarChar).Value = "computer";

            sqldatareader = sqlcommand.ExecuteReader(); //執行查詢
        }
        private void close()
        {
            //釋放物件及連線資源
            if(sqldatareader != null && sqlcommand != null)
            {
                sqldatareader.Dispose();
                sqlcommand.Dispose();
            }
            sqlconnection.Close();
            sqlconnection.Dispose();
        }
        private void output_gobang(string cheeseBoard)
        {
            string cheese = "";
            string[] root = cheeseBoard.Split(',');

            for (int i = 0; i < root.Length; i += 2)
            {
                switch (root[i])
                {
                    case "black":
                        cheese = cheese_root[root[i + 1]];
                        string[] localBlack = cheese.Split(',');

                        p[Convert.ToInt32(localBlack[0]) / number, Convert.ToInt32(localBlack[1]) / number] = 1;//這個位置是玩家2的
                        records.Add(record_root(Convert.ToInt32(localBlack[0]), Convert.ToInt32(localBlack[1]), "black"));

                        this.Controls.Add(new black_chess(Convert.ToInt32(localBlack[0]) + 181, Convert.ToInt32(localBlack[1]) + 10));//寫入棋盤的絕對位置及典籍棋盤時所產生的相對位置
                        panel3.SendToBack();//重新載入棋盤，將新的棋子畫進去
                        break;
                    case "white":
                        cheese = cheese_root[root[i + 1]];
                        string[] localWhite = cheese.Split(',');

                        p[Convert.ToInt32(localWhite[0]) / number, Convert.ToInt32(localWhite[1]) / number] = 2;//這個位置是玩家2的
                        records.Add(record_root(Convert.ToInt32(localWhite[0]), Convert.ToInt32(localWhite[1]), "white"));

                        this.Controls.Add(new white_chess(Convert.ToInt32(localWhite[0]) + 181, Convert.ToInt32(localWhite[1]) + 10));//寫入棋盤的絕對位置及典籍棋盤時所產生的相對位置
                        panel3.SendToBack();//重新載入棋盤，將新的棋子畫進去
                        break;
                }
            }
        }
        private record record_root(int x, int y, string color)
        {
            record temp = new record();

            temp.x = x.ToString();
            temp.y = y.ToString();
            temp.color = color;

            return temp;
        }
        private void update_data(string account, string time, string state, string cheesees)
        {
            close();
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB; AttachDbFilename=D:\程式資料\21醫資系上課資料(視窗程式設計)\期末project\五子棋-註冊、登錄\五子棋-註冊、登錄\Database1.mdf;Integrated Security=True;Connect Timeout=30";

            sqlconnection = new SqlConnection(connectionString);
            sqlconnection.Open();
            sqlcommand = new SqlCommand("Update player Set Time_Left=@Time_Left,State=@State,Chessboard_State=@Chessboard_State Where Account_Id=@account and previous_round = @previous", sqlconnection);
            sqlcommand.Parameters.Add("@account", SqlDbType.NVarChar).Value = account;
            sqlcommand.Parameters.Add("@previous", SqlDbType.NVarChar).Value = "computer";
            sqlcommand.Parameters.Add("@Time_Left", SqlDbType.NVarChar).Value = time;
            sqlcommand.Parameters.Add("@State", SqlDbType.NVarChar).Value = state;
            sqlcommand.Parameters.Add("@Chessboard_State", SqlDbType.NVarChar).Value = cheesees;

            sqlcommand.ExecuteNonQuery();

            sqlcommand.Dispose();
            sqlconnection.Close();
            sqlconnection.Dispose();

            MessageBox.Show("儲存成功");
            close();
        }
        private void jump_to_new_computer()
        {
            computer start_computer_game = new computer(account1, 1);
            this.Hide();
            start_computer_game.ShowDialog();
            Application.ExitThread();
        }
        private void jump_to_login()
        {
            playerToComputer login = new playerToComputer();
            this.Hide();
            login.ShowDialog();
            Application.ExitThread();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            this.StartPosition = FormStartPosition.CenterScreen; // 讓視窗顯示在螢幕正中間

            string connectionString = @"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = D:\程式資料\21醫資系上課資料(視窗程式設計)\期末project\五子棋-註冊、登錄\五子棋-註冊、登錄\Database1.mdf;Integrated Security = True; MultipleActiveResultSets=True";

            sqlconnection = new SqlConnection(connectionString);
            sqlconnection.Open();

            panel3.Width = 320;
            panel3.Height = 320;
            timeleft = 30;
            number = 320 / 8;
            timer1.Interval = 1000;
            timer1.Enabled = true;

            label1.Text = timeleft + "秒";
            show_name1.Text = account1;
            if (state == 2)
            {
                select_sql_data(account1);
                while(sqldatareader.Read())
                    output_gobang(sqldatareader["Chessboard_State"].ToString());
            }
            i++;
        }

        private void insert_player_record(string account, string previous, string state)
        {
            close();

            string connectionString = @"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = D:\程式資料\21醫資系上課資料(視窗程式設計)\期末project\五子棋-註冊、登錄\五子棋-註冊、登錄\Database1.mdf;Integrated Security = True; MultipleActiveResultSets=True";

            sqlconnection = new SqlConnection(connectionString);
            sqlconnection.Open();
            sqlcommand = new SqlCommand("Insert Into play_record (account, previous_round, state) values (@account,@previous,@state)", sqlconnection);
            sqlcommand.Parameters.Add("@account", SqlDbType.NVarChar).Value = account;
            sqlcommand.Parameters.Add("@previous", SqlDbType.NVarChar).Value = previous;
            sqlcommand.Parameters.Add("@state", SqlDbType.NVarChar).Value = state;

            sqlcommand.ExecuteNonQuery();

            close();
        }
        private void panel3_MouseDown(object sender, MouseEventArgs e)
        {
            timeleft = 30;

            int x = Cal(e.X);

            int y = Cal(e.Y);

            if (p[x / number, y / number] == 0)
            {
                p[x / number, y / number] = 1;

                records.Add(record_root(x, y, "black"));
                this.Controls.Add(new black_chess(x + 181, y + 10));//寫入棋盤的絕對位置及典籍棋盤時所產生的相對位置

                panel3.SendToBack();//重新載入棋盤，將新的棋子畫進去

                if (Win(x, y))
                {
                    DialogResult result;
                    MessageBoxButtons buttons = MessageBoxButtons.OK;
                    result = MessageBox.Show(this, "玩家1贏了", "恭喜!!", buttons);
                    update_data(show_name1.Text, "00:00", show_name1.Text, "None");
                    insert_player_record(show_name1.Text, "computer", show_name1.Text + " Win");
                    jump_to_new_computer();

                }
                else
                {
                    timer1.Enabled = false;
                    timer2.Enabled = true;


                    electro.Get_place(x / number, y / number);//電腦計算下一顆棋子的位置


                }
            }
            else
            {
                MessageBox.Show("已有棋子了");

            }

            if (i == 81)//若棋盤滿了(共81個棋子)則判定平手
            {
                MessageBox.Show(this, "雙方平手!", "平手!!");
                jump_to_new_computer();

            }

        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            if (timeleft > -1)
            {
                label1.Text = timeleft + "秒";
                timeleft = timeleft - 1;


            }
            else
            {
                MessageBox.Show("時間到!");
                int x, y;
                electro.Get_place(-1, -1);
                x = electro.GetX() * number;
                y = electro.GetY() * number;
                this.Controls.Add(new white_chess(x + 181, y + 10));//寫入棋盤的絕對位置及典籍棋盤時所產生的相對位置
                //records.Add(record_root(electro.x1, electro.y1, "white"));

                panel3.SendToBack();//重新載入棋盤，將新的棋子畫進去
                Win(x, y);
                if (Win(x, y))
                {
                    MessageBox.Show("電腦贏了");
                    update_data(show_name1.Text, "00:00", "computer", "None");
                }
                else
                {
                    timeleft = 30;
                }

            }
        }
        private int Cal(int x)//計算擺在棋盤上的位置
        {

            if (x < 0)
            {
                x = 0;
            }
            else
            {
                int check = x % number;
                if (check < number / 2)
                    check = 0;
                else
                    check = 1;
                x = ((x - (x % number)) / number + check) * number;
            }

            return x;
        }

        private bool Win(int x, int y)//判斷輸贏
        {
            int count = 0;

            count = Check(x / number, y / number, 1, 0);
            count += Check(x / number, y / number, -1, 0);

            if (count <= 4)//若某方向同時存在5科級以上的旗子則判定為贏，否則繼續判斷其他的方向
            {
                count = 0;
                count += Check(x / number, y / number, 0, 1);
                count += Check(x / number, y / number, 0, -1);
                if (count < 4)
                {
                    count = 0;
                    count += Check(x / number, y / number, 1, 1);
                    count += Check(x / number, y / number, -1, -1);
                    if (count < 4)
                    {
                        count = 0;
                        count += Check(x / number, y / number, -1, 1);
                        count += Check(x / number, y / number, 1, -1);
                        if (count < 4)
                            return false;
                        else
                            return true;
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return true;
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (wait > 0)
            {
                wait -= 1;
            }
            else
            {
                wait = 1;
                timer1.Enabled = true;
                timer2.Enabled = false;

                int x = electro.GetX() * number;//取得x的位置
                int y = electro.GetY() * number;//取得y的位置
                p[electro.GetX(), electro.GetY()] = 2;

                records.Add(record_root(x, y, "white"));
                this.Controls.Add(new white_chess(x + 181, y + 10));//寫入棋盤的絕對位置及典籍棋盤時所產生的相對位置
                panel3.SendToBack();//重新載入棋盤，將新的棋子畫進去

                if (Win(x, y))
                {
                    MessageBox.Show(this, "電腦贏了", "恭喜!!");
                    update_data(show_name1.Text, "00:00", "computer", "None");
                    insert_player_record("computer", show_name1.Text, "computer Win");
                    jump_to_new_computer();

                }
            }
        }

        private void button1_Click(object sender, EventArgs e) // 儲存目前遊戲狀況
        {
            save_the_game_and_jump_out();
        }

        private void button2_Click(object sender, EventArgs e)//求和
        {
            MessageBox.Show(this, "雙方平手!", "平手!!");
            jump_to_new_computer();

        }

        private int Check(int x, int y, int sx, int sy)//計算特定方向的棋子數量
        {
            int count = 0;
            for (int k = x + sx, l = y + sy; k < 9 && l < 9 && k > -1 && l > -1; k += sx, l += sy)
            {

                if (p[x, y] == p[k, l])
                    count++;
                else
                    break;
            }
            return count;
        }
    }
}
