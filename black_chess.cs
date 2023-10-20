using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 五子棋_註冊_登錄
{
    class black_chess : chess
    {
        public black_chess(int x, int y) : base(x, y)
        {
            this.Image = Properties.Resources.black;
            this.BackColor = System.Drawing.Color.Transparent;
        }
    }
}
