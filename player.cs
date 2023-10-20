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
    public partial class player : Form
    {
        public player(string player1, string player2, int gamestate)
        {
            InitializeComponent();
            set_Dictionary();
            account1 = player1;
            account2 = player2;
            state = gamestate;
        }
        Dictionary<string, string> cheese_root = new Dictionary<string, string>();

        struct record
        {
            public string x, y;
            public string color, state;
        }
        List<record> records = new List<record>();
        string account1, account2;
        int state = 0;
        int[,] p = new int[9, 9];//紀錄下棋狀況的陣列

        int timeleft = 60;//時間設為60秒
        int number = 0;
        static Random rm = new Random();
        int i = rm.Next(0, 2);//亂數產生誰先下棋,產生的數介於0、1之間，0是玩家一，1是玩家二

        private SqlConnection sqlconnection = null;
        private SqlCommand sqlcommand = null;
        private SqlDataReader sqldatareader = null;

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
            {
                update_data(show_name1.Text, show_name2.Text, timeleft.ToString(), "None", "None");
                update_data(show_name2.Text, show_name1.Text, timeleft.ToString(), "None", "None");
            }
            else
            {
                update_data(show_name1.Text, show_name2.Text, timeleft.ToString(), "None", roots);
                update_data(show_name2.Text, show_name1.Text, timeleft.ToString(), "None", roots);
            }

            timer1.Stop();
            timer2.Stop();

            records.Clear();
            MessageBox.Show("儲存成功");
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
        private void select_sql_data(string account, string previous) // 資料庫搜尋
        {
            // 搜尋登入資料
            sqlcommand = new SqlCommand("Select * From player where Account_Id = @account and previous_round = @previous", sqlconnection);
            sqlcommand.Parameters.Add("@account", SqlDbType.NVarChar).Value = account;
            sqlcommand.Parameters.Add("@previous", SqlDbType.NVarChar).Value = previous;

            sqldatareader = sqlcommand.ExecuteReader(); //執行查詢
        }
        private void close()
        {
            //釋放物件及連線資源
            if (sqldatareader != null && sqlcommand != null)
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
                switch(root[i])
                {
                    case "black":
                        cheese = cheese_root[root[i + 1]];
                        string[] localBlack = cheese.Split(',');

                        p[Convert.ToInt32(localBlack[0]) / number, Convert.ToInt32(localBlack[1]) / number] = 2;//這個位置是玩家2的
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
        private void jump_to_new_computer()
        {
            player start_game = new player(account1, account2, 1);
            this.Hide();
            start_game.ShowDialog();
            Application.ExitThread();
        }
        private void jump_to_login()
        {
            playerToComputer login = new playerToComputer();
            this.Hide();
            login.ShowDialog();
            Application.ExitThread();
        }
        private record record_root(int x, int y, string color)
        {
            record temp = new record();

            temp.x = x.ToString();
            temp.y = y.ToString();
            temp.color = color;

            return temp;
        }
        private void update_data(string account, string previous, string time, string state, string cheesees)
        {
            close();

            string connectionString = @"Data Source = (LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\程式資料\21醫資系上課資料(視窗程式設計)\期末project\五子棋-註冊、登錄\五子棋-註冊、登錄\Database1.mdf;Integrated Security = True;MultipleActiveResultSets=True";

            sqlconnection = new SqlConnection(connectionString);
            sqlconnection.Open();
            SqlCommand sqlcommand = new SqlCommand("Update player Set Time_Left=@Time_Left,State=@State,Chessboard_State=@Chessboard_State Where Account_Id=@account and previous_round=@previous", sqlconnection);
            sqlcommand.Parameters.Add("@account", SqlDbType.NVarChar).Value = account;
            sqlcommand.Parameters.Add("@previous", SqlDbType.NVarChar).Value = previous;
            sqlcommand.Parameters.Add("@Time_Left", SqlDbType.NVarChar).Value = time;
            sqlcommand.Parameters.Add("@State", SqlDbType.NVarChar).Value = state;
            sqlcommand.Parameters.Add("@Chessboard_State", SqlDbType.NVarChar).Value = cheesees;

            sqlcommand.ExecuteNonQuery();

            sqlcommand.Dispose();
            sqlconnection.Close();
            sqlconnection.Dispose();

            close();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            string connectionString = @"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = D:\程式資料\21醫資系上課資料(視窗程式設計)\期末project\五子棋-註冊、登錄\五子棋-註冊、登錄\Database1.mdf;Integrated Security = True; MultipleActiveResultSets=True";

            sqlconnection = new SqlConnection(connectionString);
            sqlconnection.Open();

            show_name1.Text = account1;
            show_name2.Text = account2;

            panel3.Width = 320;//設定棋盤的大小
            panel3.Height = 320;

            number = 320 / 8;//棋盤的每格的大小

            timer1.Interval = 1000;
            timer2.Interval = 1000;

            time2_txt.Text = timeleft + "秒";//顯示玩家一開始的秒數
            time_txt.Text = timeleft + "秒";

            if(state == 2)
            {
                select_sql_data(account1, account2);
                while (sqldatareader.Read())
                    output_gobang(sqldatareader["Chessboard_State"].ToString());
            }
            if (i % 2 == 0)//如果是玩家1則觸發第一個計時器
            {

                timer1.Enabled = true;
                ply_lab.ForeColor = Color.Red;//玩家一的標籤變色
                                              //   ply_lab2.ForeColor = Color.Black;
                timer1.Start();
                MessageBox.Show("玩家一先下!");


            }
            else
            {
                //  ply_lab.ForeColor = Color.Black;//玩家一的標籤變色
                ply_lab2.ForeColor = Color.Red;//玩家二的標籤變色
                timer2.Enabled = true;
                timer2.Start();
                MessageBox.Show("玩家二先下!");

            }
            i++;

        }
        private int Cal(int x)
        {

            if (x < 0)//如果超過棋盤
            {
                x = 0;
            }
            else
            {
                int check = x % number;//座標除上每格格子的大小，如果餘數比格子的一半小就找最近的放
                if (check < number / 2)
                    check = 0;
                else
                    check = 1;
                x = ((x - (x % number)) / number + check) * number;//計算放置的位置
            }

            return x;
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
        private void panel3_MouseDown(object sender, MouseEventArgs e)//下棋
        {

            //loct_text.Text = "x:" + e.X.ToString() + "y:" + e.Y.ToString();//顯示滑鼠點擊的座標
            int x = Cal(e.X);//計算滑鼠在棋盤上點擊的位置
            int y = Cal(e.Y);
            int a = i % 2;
            timeleft = 60;
            if (p[x / number, y / number] == 0)//如果棋盤上沒有棋子
            {
                // panel3.Cursor = Cursors.Hand;
                //p[x / number, y / number] = i % 2 + 1;
                //if (!Win(x / number, y / number))
                //{
                if (a == 0)
                {
                    records.Add(record_root(x, y, "white"));
                    ply_lab.ForeColor = Color.Black;//玩家一的標籤變色
                    ply_lab2.ForeColor = Color.Red;//玩家二的標籤變色
                    p[x / number, y / number] = 2;//這個位置是玩家2的
                    this.Controls.Add(new white_chess(x + 181, y + 10));//寫入棋盤的絕對位置及典籍棋盤時所產生的相對位置
                    //this.BackColor = Color.Transparent;
                    panel3.SendToBack();//重新載入棋盤，將新的棋子畫進去
                    if (all_board() == true)//判斷是否滿棋
                    {

                        show_peace();

                    }
                    else
                    {
                        if (Win(x / number, y / number))
                        {
                            MessageBox.Show("玩家二贏了!");
                            update_data(show_name1.Text, show_name2.Text, "00:00", show_name2.Text, "None");
                            update_data(show_name2.Text, show_name1.Text, "00:00", show_name2.Text, "None");

                            insert_player_record(show_name2.Text, show_name1.Text, show_name2.Text + " Win");
                            timer1.Stop();
                            timer2.Enabled = false;
                            timer2.Stop();

                            if (MessageBox.Show("是否重新開始??", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                jump_to_new_computer();
                            else
                                jump_to_login();
                            //Application.Restart();
                        }
                        else
                        {
                            timer2.Enabled = false;
                            time_txt.Text = "60秒";
                            timer1.Start();
                        }
                        ply_lab2.ForeColor = Color.Black;//玩家二的標籤變色
                        ply_lab.ForeColor = Color.Red;
                    }

                }
                else
                {
                    records.Add(record_root(x, y, "black"));

                    ply_lab.ForeColor = Color.Red;//玩家一的標籤變色
                    ply_lab2.ForeColor = Color.Black;
                    // panel1.BorderStyle = BorderStyle.FixedSingle;
                    p[x / number, y / number] = 1;//這個位置是玩家1的
                    this.Controls.Add(new black_chess(x + 181, y + 10));
                    panel3.SendToBack();//重新載入棋盤，將新的棋子畫進去
                    if (all_board() == true)//判斷是否滿棋
                    {

                        show_peace();

                    }
                    else
                    {
                        if (Win(x / number, y / number))
                        {
                            MessageBox.Show("玩家一贏了");
                            update_data(show_name1.Text, show_name2.Text, "00:00", show_name1.Text, "None");
                            update_data(show_name2.Text, show_name1.Text, "00:00", show_name1.Text, "None");

                            insert_player_record(show_name1.Text, show_name2.Text, show_name1.Text + " Win");
                            timer1.Stop();
                            timer1.Enabled = false;
                            timer2.Stop();

                            if (MessageBox.Show("是否重新開始??", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                jump_to_new_computer();
                            else
                                jump_to_login();
                        }
                        else
                        {
                            timer1.Enabled = false;
                            time2_txt.Text = "60秒";
                            timer2.Start();
                        }
                        ply_lab.ForeColor = Color.Black;//玩家一的標籤變色
                        ply_lab2.ForeColor = Color.Red;
                    }

                }

            }
            else
            {
                MessageBox.Show("已有棋子了");
            }
            i++;
        }
        private void timer2_Tick(object sender, EventArgs e)
        {

            if (timeleft > 0)
            {
                timeleft = timeleft - 1;
                time_txt.Text = timeleft + "秒";
            }
            else
            {
                timer2.Enabled = false;
                timer2.Stop();
                MessageBox.Show("時間到!");
                timeleft = 60;//重新計時
                timer1.Start();
                timer1.Enabled = true;

            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {

            if (timeleft > 0)
            {
                timeleft = timeleft - 1;
                time2_txt.Text = timeleft + "秒";
            }
            else
            {
                timer1.Enabled = false;
                timer1.Stop();
                MessageBox.Show("時間到!");
                timeleft = 60;//重新計時
                timer2.Start();
                timer2.Enabled = true;

            }

        }

        private bool Win(int x, int y)
        {
            int count = 0;
            count += Counts(x, y, 1, 0);//判斷正x
            count += Counts(x, y, -1, 0);//判斷負x
            if (count < 4)
            {
                count = Counts(x, y, 0, 1);
                count += Counts(x, y, 0, -1);
                if (count < 4)
                {
                    count = Counts(x, y, 1, 1);
                    count += Counts(x, y, -1, -1);
                    if (count < 4)
                    {
                        count += Counts(x, y, 1, -1);
                        count += Counts(x, y, -1, 1);
                        if (count < 4)
                        {
                            return false;
                        }

                    }
                }
            }
            return true;
        }
        //sx、sy是位移量
        private int Counts(int x, int y, int sx, int sy)
        {
            int count = 0;
            for (int a = x + sx, b = sy + y; (a < 9 && a > -1) && (b < 9 && b > -1); a += sx, b += sy)
            {
                if (p[a, b] == p[x, y])
                {
                    count += 1;
                }
                else
                    break;
            }

            return count;
        }

        private void peace1_Click(object sender, EventArgs e)//求和案件
        {
            timer1.Enabled = false;
            timer1.Stop();


            timer2.Enabled = false;
            timer2.Stop();
            DialogResult myResult = MessageBox.Show("是否接受玩家一的求和?", "求和", MessageBoxButtons.YesNo);


            if (myResult == DialogResult.Yes)
            {

                MessageBox.Show("此局平手!");

                jump_to_new_computer();
            }
            else
            {
                MessageBox.Show("玩家二不同意求和!此局繼續!!!");
                timer1.Start();
            }
        }

        private void peace2_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            timer1.Stop();


            timer2.Enabled = false;
            timer2.Stop();
            DialogResult myResult = MessageBox.Show("是否接受玩家二的求和?", "求和", MessageBoxButtons.YesNo);


            if (myResult == DialogResult.Yes)
            {

                MessageBox.Show("此局平手!");

                jump_to_new_computer();
            }
            else
            {
                MessageBox.Show("玩家一不同意求和!此局繼續!!!");
                timer2.Start();

            }
        }
        private bool all_board()//判斷是否滿棋
        {
            bool full = true;
            for (int j = 0; j < 9; j++)//棋盤大小9個格子
            {
                for (int k = 0; k < 9; k++)
                {
                    if (p[j, k] == 0)//如果這個位置是空的
                    {
                        return full = false;
                    }
                }
            }
            return full;
        }
        private void show_peace()
        {
            DialogResult result = MessageBox.Show("此局平手!是否重新開始?", "平局", MessageBoxButtons.OKCancel);
            if (result == DialogResult.OK)
            {
                MessageBox.Show("重新開始遊戲!");
                jump_to_new_computer();//重新刷新頁面
            }
            else
            {
                MessageBox.Show("結束遊戲!");
                Application.Exit();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            save_the_game_and_jump_out();
        }

        private void panel3_MouseMove(object sender, MouseEventArgs e)//滑鼠經過棋盤如果可以放置旗子就改變鼠標樣式
        {
            int x = Cal(e.X);//計算滑鼠在棋盤上點擊的位置
            int y = Cal(e.Y);
            if (p[x / number, y / number] == 0)//如果棋盤上沒有棋子
            {
                panel3.Cursor = Cursors.Hand;//改變鼠標圖案
            }
        }
    }
}
