using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSharpForm
{
    class DataGridViewManagement
    {
        public void Disp(string[][] ReadData, DataGridView dataGridView)
        {
            DataSet dataSet = new DataSet();
            DataTable dataTable = new DataTable("Data");

            // 1行目をヘッダーとして読み込む
            for(int i = 0; i < ReadData[0].Length; i++)
            {
                DataColumn dataColumn = new DataColumn(ReadData[0][i], typeof(string));
                dataTable.Columns.Add(dataColumn);
            }

            // １行目のヘッダーをとばす
            for(int i = 1; i < ReadData.Length; i++)
            {
                dataTable.Rows.Add(ReadData[i]);
            }

            dataSet.Tables.Add(dataTable);
            dataGridView.DataSource = dataSet;
        }
    }
}
