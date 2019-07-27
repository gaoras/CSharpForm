using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LibFileManagement;

namespace CSharpForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button_Disp_Click(object sender, EventArgs e)
        {
            DataGridViewManagement DGVMng = new DataGridViewManagement(dataGridView1);

            //csvファイルの読み込み
            string path = "csvTest.csv";
            string[][] csvData = CsvReader.Read(path);
            //データグリッドビューに表示
            DGVMng.Disp(csvData);
        }
    }
}
