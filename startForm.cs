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
    public partial class startForm : Form
    {
        private int size = 20;
        public startForm()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            size += 2;
            label1.Font = new Font("Colonna MT", size);

            if (size == 72)
            {
                timer1.Stop();
                login form1 = new login();
                this.Hide();
                form1.ShowDialog();
                Application.ExitThread();
            }
        }

        private void startForm_Load(object sender, EventArgs e)
        {
            label1.Font = new Font("Colonna MT", 20);
        }
    }
}
