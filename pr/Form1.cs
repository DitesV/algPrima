using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace wfg
{
    public partial class Form1 : Form
    {
        int kv = 0;
        int x, y;
        //int [] ves=new int [101];
        int ves = 1;//вес ребра
        int tcount;//кол-во ребер
        bool Dr;//рисование 
        bool svyaznost;//связность
        int numberv1 = -1;//номер первой вершины
        int numberv2 = -1;//номер второй вершины
        int[] X = new int[1000];//коорд х
        int[] Y = new int[1000];//коорд у
        int[] XR = new int[1000];//коорд х прима
        int[] YR = new int[1000];//коорд у прима
        int[,] line = new int[1000, 1000];//смежн.табл
        int[,] table = new int[1000, 1002];//инц.табл
        int R = 30;
        int[,] vline = new int[1000, 1000];//вспомогат табл.инц
        int[,] vlin = new int[1000, 1000];//вспомог.табл.смеж
        Pen pen = new Pen(Brushes.Black, 2);

        //for(int i=0)

        Brush brush = new SolidBrush(Color.Black);
        Brush brush2 = new SolidBrush(Color.White);

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = "Граф";
            button1.Text = "Очистить";
            //button2.Text = "Связность";
            button3.Text = "А.Прима";
            Font = new System.Drawing.Font("Times New Roman", 10, FontStyle.Bold);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Graphics Графика = pictureBox1.CreateGraphics();
            Графика.Clear(SystemColors.Window);
            for (int i = 0; i <= kv; i++)
            {
                for (int j = 0; j <= kv; j++)
                {
                    line[i, j] = 0;//// удаляет табл смежности
                }
            }

            kv = 0;
            ves = 1;
            tcount = 0;
            Dr = false;
            svyaznost = false;
            numberv1 = -1;
            numberv2 = -1;
            textBox1.Clear();

        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {

        }
        int Collision(int x, int y, int delta)
        {
            for (int i = 0; i < kv; i++)
            {
                if (X[i] - R * delta < x && X[i] + R * delta > x && Y[i] - R * delta < y && Y[i] + R * delta > y)
                {
                    return i;
                }
            }
            return -1;
        }
        bool ss ()
            {
           
                int rr = 0;
        int xx = 1;
        int[] rx = new int[100];//массив необходимости повторного прохода
        int[] var = new int[100];//массив наличия связей
        tcount = 0;

                for (int i = 0; i< 100; i++)
                {
                    for (int j = 0; j< 100; j++)
                    {
                        table[i, j] = 0;
                    }
}
                for (int i = 0; i<kv; i++)
                {
                    for (int j = 0; j<i; j++)
                    {
                        if (line[i, j] != 0)
                        {
                            table[tcount, i] = line[i, j];//заполнение таблицы
                            table[tcount, j] = line[i, j];
                            tcount++;
                        }
                    }
                }
                for (int i = 0; i<kv; i++)
                {
                    var[i] = 0;
                }
                var[0] = 1;

                for (int i = 0; i< 100; i++)
                    rx[i] = 0;

                for (int l = 0; l<xx; l++)
                {
                    for (int i = 0; i <= kv; i++)
                    {
                        if (var[i] == 1)
                        {
                            for (int j = 0; j <= tcount; j++)
                            {
                                if (table[j, i] != 0)
                                {
                                    for (int k = 0; k <= kv; k++)
                                    {
                                        if (table[j, k] != 0)
                                        {
                                            if ((var[k] == 0 || rx[k] > 1) && k <= i)
                                            {
                                                xx++;
                                                rx[k] = rx[k] + 1;
                                            }
                                            var[k] = 1;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                for (int r = 0; r<kv; r++)
                {
                    if (var[r] == 0) break;
                    rr++;
                }
            if (rr == kv && kv != 0)
            {
               // textBox1.Text = "yes";//связный
                svyaznost = true;
            }
            else
            {
               // textBox1.Text = "No";// не связный
                svyaznost = false;
            }           
            return svyaznost;
        }
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            textBox1.Clear();
            svyaznost = false;
            textBox3.Clear();
            if (e.Button == MouseButtons.Left)
            {
                Dr = true;
                {
                    x = e.X;
                    y = e.Y;
                    for (int i = 0; i <= kv; i++)
                    {
                        if (Math.Abs(X[i] - e.X) < R && Math.Abs(Y[i] - e.Y) < R)
                        {
                            Dr = false;
                            if (numberv1 == -1)
                            {
                                numberv1 = i;
                            }
                            if (numberv1 != -1 && numberv1 != i)
                                numberv2 = i;
                        }                       
                    }
                }
             
                if (Dr == true)
                {
                    X[kv] = e.X;
                    Y[kv] = e.Y;
                    kv++;
                    {
                       
                        numberv1 = -1;
                        numberv2 = -1;
                    }
                }
                else
                {
                    if (numberv2 != -1)
                    {
                        textBox1.Text = "";
                        if (textBox2.Text != null)
                            ves = int.Parse(textBox2.Text);
                        line[numberv1, numberv2] = ves;
                        line[numberv2, numberv1] = ves;

                        numberv1 = -1;
                        numberv2 = -1;
                    }
                }

            }
            if (e.Button == MouseButtons.Right)

            {
                numberv1 = -1;
                numberv2 = -1;

                {
                    int dioganalMain,
                        dioganal1,
                        dioganal2;
                    //	удаление вершины
                    int iPos = Collision(e.X, e.Y, 1);
                    if (iPos != -1)
                    {
                        // delete vershina
                        for (int j = iPos; j < kv; j++)
                        {
                            X[j] = X[j + 1];
                            Y[j] = Y[j + 1];
                        }
                        // delet rebra of vershina
                        for (int i = 0; i < kv; i++)
                        {
                            for (int j = iPos; j < kv; j++)
                            {
                                line[i, j] = line[i, j + 1];
                            }
                        }
                        for (int i = iPos; i < kv; i++)
                        {
                            for (int j = 0; j < kv; j++)
                            {
                                line[i, j] = line[i + 1, j];
                            }
                        }
                        kv--;

                        //return true;
                    }
                    //	удаление ребра
                    for (int i = 0; i < kv; i++)
                    {
                        for (int j = 0; j < kv; j++)
                        {
                            dioganalMain = (int)Math.Sqrt(Math.Pow((double)X[j] - X[i], 2) + Math.Pow((double)Y[j] - Y[i], 2));
                            dioganal1 = (int)Math.Sqrt(Math.Pow((double)X[j] - e.X, 2) + Math.Pow((double)Y[j] - e.Y, 2));
                            dioganal2 = (int)Math.Sqrt(Math.Pow((double)e.X - X[i], 2) + Math.Pow((double)e.Y - Y[i], 2));
                            if (dioganalMain - 5 <= dioganal1 + dioganal2 &&
                                dioganalMain + 5 >= dioganal1 + dioganal2)
                            {
                                line[i, j] = 0;
                                line[j, i] = 0;
                                //return true;
                            }
                        }
                    }

                    // return false;
                }

            }
            pictureBox1.Refresh();
            
            if(ss()==true)
                {
                    textBox1.Text = "yes";//связный
                   // svyaznost = true;
                }
                else
                {
                    textBox1.Text = "No";// не связный
                   // svyaznost = false;
                }
        } 

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {


            char number = e.KeyChar;
            if (e.KeyChar <= 47 || e.KeyChar >= 58)
            {
                e.Handled = true;
            }
            
        }

        private void textBox2_Click(object sender, EventArgs e)
        {
            textBox2.Clear();
        }

        private void textBox2_MouseLeave(object sender, EventArgs e)
        {
            pictureBox1.Focus();
            if (textBox2.Text == "")
            {
               
                textBox2.Text += ves; }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
            if(svyaznost==true)
            {
                bool end = false;
                int stoim = 0;
                int maxstoim=0;
                int rr = 0;
                int numbervv;
                int numbervv2=0;
                int[] rx = new int[100];//массив необходимости повторного прохода
                int[] var = new int[100];//массив наличия связей
                if (numberv1 > 0)
                    numbervv = numberv1;
                else
                    numbervv = 0;
                var[numbervv] = 1;
                
                for (int i = 0; i < tcount; i++)
                    for (int j = 0; j < kv; j++)
                    {
                        vline[i, j] = table[i, j];
                    }
                for(int i=0;i<tcount;i++)
                for (int j = 0; j < kv; j++)
                {
                    if (vline[i,j] > stoim)
                        stoim = vline[i,j];
                }
                maxstoim = 0;
                                   
                    do
                    {
                    stoim = 0;
                        for (int i = 0; i < tcount; i++)
                            for (int j = 0; j < kv; j++)
                            {
                                if (vline[i, j] > stoim)
                                    stoim = vline[i, j] + 1;
                            }
                       
                        for (int j = 0; j < kv; j++)
                        {
                            if (var[j] == 1)
                            {
                                numbervv = j;                                
                            }
                            for (int i = 0; i < tcount; i++)
                                if (vline[i, numbervv] < stoim && vline[i, numbervv] > 0)
                                {
                                    numbervv2 = numbervv;
                                    stoim = vline[i, numbervv];
                                    rr = i;
                                    var[numbervv2] = 1;                                
                                }
                        }
                    XR[numbervv2] = X[numbervv2];
                    YR[numbervv2] = Y[numbervv2];
                    vline[rr, numbervv2] = 0;
                    for (int j=0;j<kv;j++)
                        {
                            if (vline[rr, j] == stoim&&var[j]!=1)
                            {
                                XR[j] = X[j];
                                YR[j] = Y[j];
                                var[j] = 1;
                                vlin[j, numbervv2] = 1;
                                vlin[numbervv2,j] = 1;

                            maxstoim = maxstoim + vline[rr, j];
                            vline[rr, j] = 0;
                            }

                        }


                    end = true;
                    for (int i = 0; i < kv; i++)
                        if (var[i] == 0)
                            end = false;
                } while (end == false);
                pictureBox1.Refresh();
                textBox3.Clear();
                textBox3.Text += "" ;
                textBox3.Text += maxstoim;
                for (int i = 0; i < kv; i++)
                {
                    XR[i] = 0;
                    XR[i] = 0;
                }
                for(int i=0;i<kv;i++)
                    for(int j=0;j<kv;j++)
                        vlin[j, i] = 0;
            }
            else
            {
                MessageBox.Show(
                    "Граф должен быть связным",
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information,
                    MessageBoxDefaultButton.Button1
                  );
            }
            numberv1 = -1;
        }

        private void помощьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 f = new Form2();
            f.Hide();
            f.Show();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Brush brushblue = new SolidBrush(Color.Red);
            Pen penRed = new Pen(Color.Red, 2);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            for (int i = 0; i <kv; i++)
                for(int j=i;j<kv;j++)
                {
                    if (line[i, j] != 0)
                    {
                        int dioganal1;
                        int dioganal2;
                        String text = (line[i, j]).ToString();
                        dioganal1 = X[j] + (X[i] - X[j]) / 2; 
                        dioganal2 = Y[j] + (Y[i] - Y[j]) / 2;
                        e.Graphics.DrawLine(pen, X[i], Y[i], X[j], Y[j]);
                        e.Graphics.DrawString(text, Font, brushblue, dioganal1, dioganal2 );
                    }
                }
                for (int j = 0; j < kv; j++)
            for (int i = j; i < kv; i++)
                {
                    if (vlin[j, i] != 0)
                    {
                        e.Graphics.DrawLine(penRed, XR[j], YR[j], XR[i], YR[i]);                      
                    }
                }

            for (int i=0;i<kv;i++)
            {
                String text = (i+1).ToString();
                
                e.Graphics.FillEllipse(brush2, X[i] - 15, Y[i] - 15, 30, 30);
                if (numberv1 == i) e.Graphics.DrawEllipse(penRed, X[i] - 15, Y[i] - 15, 30, 30);
                else
                {
                    e.Graphics.DrawEllipse(pen, X[i] - 15, Y[i] - 15, 30, 30);
                }
                e.Graphics.DrawString(text, Font, brush, X[i] - 8, Y[i] - 8);


            }
        }
    }
}
