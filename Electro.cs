using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 五子棋_註冊_登錄
{
    class Electro
    {
        int[,] place = new int[10, 10];//紀錄目前以下過的位置
        int x1, y1, x2, y2;


        public Electro()
        {
            x1 = -1;//上次電腦下的位置
            y1 = -1;
            x2 = -1;//玩家剛下的位置
            y2 = -1;
            //longest[0] =-1;
            //longest[0] = -1;
            //longest[0] = 0;

        }
        public int GetX()//取得電腦下的位置x
        {
            return x1;
        }
        public int GetY()//取得電腦下的位置y
        {
            return y1;
        }
        public void Get_place(int x, int y)
        {

            bool che = true;
            int sx = 0, sy = 0, count;
            int[,] flag1 = new int[4, 2] { { 0, 1 }, { 0, 2 }, { 0, 3 }, { 0, 4 } };//記錄各方向的棋子數

            do
            {
                if (x != -1 && y != -1)//-1代表玩家超時沒下棋
                {
                    place[x, y] = 1;//更新目前以下過的位置
                                    //取得玩家當前下的這手直排、橫排、兩條斜排相連的棋子數
                    flag1[0, 0] = Chess_number(x, y, -1, 0);
                    flag1[1, 0] = Chess_number(x, y, 0, -1);
                    flag1[2, 0] = Chess_number(x, y, -1, 1);
                    flag1[3, 0] = Chess_number(x, y, -1, -1);

                    //取得位置
                    for (int z = 0; z < 4; z++)//避免最先讀到的道路已經沒位置
                    {
                        if (flag1[z, 0] >= 2)
                        {
                            //取得該方向所要走的方向
                            switch (z)
                            {
                                case 0:
                                    sx = 1;
                                    sy = 0;
                                    break;
                                case 1:
                                    sx = 0;
                                    sy = 1;
                                    break;
                                case 2:
                                    sx = 1;
                                    sy = -1;
                                    break;
                                case 3:
                                    sx = 1;
                                    sy = 1;
                                    break;
                            }

                            //  去那個方向找有無最近的該方向的空格
                            count = Chess_count(x, y, sx, sy) + 1;
                            if ((x + sx * count) > -1 && (x + sx * count) < 9 && (y + sy * count) > -1 && (y + sy * count) < 9 && place[(x + sx * count), (y + sy * count)] == 0)
                            {
                                x1 = x + sx * count; //有則將x1 y1設為該空格
                                y1 = y + sy * count;
                                che = false;
                                break;
                            }
                            else
                            {
                                count = Chess_count(x, y, -sx, -sy) + 1;
                                if ((x - sx * count) > -1 && (x - sx * count) < 9 && (y - sy * count) > -1 && (y - sy * count) < 9 && place[(x - sx * count), (y - sy * count)] == 0)
                                {

                                    x1 = x - sx * count;
                                    y1 = y - sy * count;
                                    che = false;
                                    break;

                                }
                            }
                        }
                    }
                }


                if (che)
                {
                    //若當前下的一手沒有找到位置，則去判斷前一手有無連線
                    flag1[0, 0] = Chess_number(x2, y2, -1, 0);
                    flag1[1, 0] = Chess_number(x2, y2, 0, -1);
                    flag1[2, 0] = Chess_number(x2, y2, -1, 1);
                    flag1[3, 0] = Chess_number(x2, y2, -1, -1);

                    for (int z = 0; z < 4; z++)
                    {
                        if (flag1[z, 0] >= 2)
                        {
                            switch (z)
                            {
                                case 0:
                                    sx = 1;
                                    sy = 0;
                                    break;
                                case 1:
                                    sx = 0;
                                    sy = 1;
                                    break;
                                case 2:
                                    sx = 1;
                                    sy = -1;
                                    break;
                                case 3:
                                    sx = 1;
                                    sy = 1;
                                    break;
                            }
                            count = Chess_count(x2, y2, sx, sy) + 1;
                            if ((x2 + sx * count) > -1 && (x2 + sx * count) < 9 && (y2 + sy * count) > -1 && (y2 + sy * count) < 9 && place[(x2 + sx * count), (y2 + sy * count)] == 0)
                            {//  去那個方向找有無最近的該方向的空格
                                x1 = x2 + sx * count; //有則將x1 y1設為該空格
                                y1 = y2 + sy * count;
                                che = false;
                                break;
                            }
                            else
                            {
                                count = Chess_count(x2, y2, -sx, -sy) + 1;
                                if ((x2 - sx * count) > -1 && (x2 - sx * count) < 9 && (y2 - sy * count) > -1 && (y2 - sy * count) < 9 && place[(x2 - sx * count), (y2 - sy * count)] == 0)
                                {

                                    x1 = x2 - sx * count;
                                    y1 = y2 - sy * count;
                                    che = false;
                                    break;

                                }
                            }
                        }
                    }

                    if (che)
                    {
                        //  若無上一個白棋且玩家沒下棋，則放正中間
                        if (x < 0 && y < 0 && x1 < 0 && y1 < 0)
                        {
                            x1 = 4;
                            y1 = 4;
                        }
                        else if (x1 < 0 && y1 < 0)//  若無上一個白棋，則在玩家下的附近隨機生成一個
                        {
                            do
                            {
                                var rand = new Random();
                                double c = rand.NextDouble();
                                x1 = (int)(c * 10) % 3 - 1 + x;
                                c = rand.NextDouble();
                                y1 = (int)(c * 10) % 3 - 1 + y;
                            } while (y1 < 0 || y1 > 8 || x1 < 0 || x1 > 8 || place[x1, y1] != 0);
                        }
                        else
                        {
                            //判斷是否有白棋序列
                            flag1[0, 0] = Chess_number(x1, y1, -1, 0);
                            flag1[1, 0] = Chess_number(x1, y1, 0, -1);
                            flag1[2, 0] = Chess_number(x1, y1, -1, 1);
                            flag1[3, 0] = Chess_number(x1, y1, -1, -1);
                            for (int i = 0; i < 4; i++)
                            {
                                for (int j = 0; j < 4; j++)
                                {
                                    if (flag1[i, 0] > flag1[j, 0])
                                    {
                                        int temp;
                                        temp = flag1[j, 0];
                                        flag1[j, 0] = flag1[i, 0];
                                        flag1[i, 0] = temp;
                                        temp = flag1[j, 1];
                                        flag1[j, 1] = flag1[i, 1];
                                        flag1[i, 1] = temp;
                                    }
                                }
                            }
                            for (int z = 0; z < 4; z++)
                            {

                                switch (flag1[z, 1])
                                {
                                    case 1:
                                        sx = 1;
                                        sy = 0;
                                        break;
                                    case 2:
                                        sx = 0;
                                        sy = 1;
                                        break;
                                    case 3:
                                        sx = 1;
                                        sy = -1;
                                        break;
                                    case 4:
                                        sx = 1;
                                        sy = 1;
                                        break;
                                }


                                if (flag1[z, 0] > 0)
                                {
                                    count = Chess_count(x1, y1, sx, sy) + 1;
                                    if ((x1 + sx * count) > -1 && (x1 + sx * count) < 9 && (y1 + sy * count) > -1 && (y1 + sy * count) < 9 && place[(x1 + sx * count), (y1 + sy * count)] == 0)
                                    {//  去那個方向找有無最近的該方向的空格
                                        x1 = x1 + sx * count; //有則將x1 y1設為該空格
                                        y1 = y1 + sy * count;
                                        che = false;
                                        break;
                                    }
                                    else
                                    {
                                        count = Chess_count(x1, y1, -sx, -sy) + 1;
                                        if ((x1 - sx * count) > -1 && (x1 - sx * count) < 9 && (y1 - sy * count) > -1 && (y1 - sy * count) < 9 && place[(x1 - sx * count), (y1 - sy * count)] == 0)
                                        {
                                            x1 = x1 - sx * count;
                                            y1 = y1 - sy * count;
                                            che = false;
                                            break;

                                        }

                                    }
                                }

                            }

                            //若沒有白棋序列，則隨機在上個白棋附近生成一個
                            if (che)
                            {
                                var rand = new Random();
                                do
                                {
                                    double c = rand.NextDouble();
                                    x1 += (int)(c * 10) % 3 - 1;
                                    c = rand.NextDouble();
                                    y1 += (int)(c * 10) % 3 - 1;
                                } while (y1 < 0 || y1 > 8 || x1 < 0 || x1 > 8 || place[x1, y1] != 0);

                            }

                        }
                    }
                }

            } while (place[x1, y1] != 0);
            x2 = x;
            y2 = y;
            place[x1, y1] = 2;
        }

        int Chess_number(int x, int y, int sx, int sy)//確認方向的棋子數
        {
            int count = 0;
            if (x > -1 && y > -1)
            {
                count = Chess_count(x, y, sx, sy);
                count += Chess_count(x, y, -sx, -sy);
            }
            return count;
        }

        int Chess_count(int x, int y, int sx, int sy)//計算方向的棋子數
        {
            int count = 0;
            for (int a = x + sx, b = y + sy; a < 9 && a > -1 && b < 9 && b > -1; a += sx, b += sy)
            {
                if (place[x, y] == place[a, b])
                    count++;
                else
                    break;
            }

            return count;
        }
    }
}
