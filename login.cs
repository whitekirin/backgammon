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
    public partial class login : Form
    {
        private Form computer_to_people;
        private Register register;
        public login()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            computer_to_people = new playerToComputer();

            this.Hide();
            computer_to_people.ShowDialog();
            Application.ExitThread();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            register = new Register();

            this.Hide();
            register.ShowDialog();
            Application.ExitThread();
        }
    }
}
