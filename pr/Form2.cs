using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace wfg
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            textBox1.Text = "1)Добавление вершины производится нажатем левой кнопки мыши" +Environment.NewLine+
                "2)Удаление вершины или ребра производится нажатием правой кнопки мыши" + Environment.NewLine +"3)Добавление ребра производится выбором двух вершин поочередным нажатием левой кнопкой мыши";
        }
    }
}
