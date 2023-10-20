using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace 五子棋_註冊_登錄
{
    class chess : PictureBox//建立棋子的類別並繼承picturebox控制項的屬性
    {
        public chess(int x, int y)
        {
            this.BackColor = Color.Transparent;//指定背景色
            this.Location = new Point(x - (15 / 2), y - (15 / 2));//指定位置，將點擊位置當成中心點
            this.Size = new Size(15, 15);//設定大小
            this.SizeMode = PictureBoxSizeMode.StretchImage;

        }

    }
}
