using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 五子棋_註冊_登錄
{
    public partial class startGame : Form
    {
        public startGame()
        {
            InitializeComponent();
        }

        private void startGame_Load(object sender, EventArgs e)
        {
            // TODO: 這行程式碼會將資料載入 'database1DataSet2.player' 資料表。您可以視需要進行移動或移除。
            this.playerTableAdapter1.Fill(this.database1DataSet2.player);
            // TODO: 這行程式碼會將資料載入 'database1DataSet1.player' 資料表。您可以視需要進行移動或移除。
            this.playerTableAdapter.Fill(this.database1DataSet1.player);

        }

        private void startGame_FormClosing(object sender, FormClosingEventArgs e)
        {
            login initialization = new login();

            this.Hide();
            initialization.ShowDialog();
            Application.ExitThread();
        }
    }
}
