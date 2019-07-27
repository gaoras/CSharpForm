using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSharpForm
{
    class DataGridViewManagement
    {
        private DataGridView DataGridView;

        public DataGridViewManagement(DataGridView dataGridView1)
        {
            this.DataGridView = dataGridView1;
        }

        /// <summary>
        /// 指定したデータをデータグリッドビューに表示
        /// </summary>
        /// <param name="csvData"></param>
        public void Disp(string[][] csvData)
        {
            DataTable dataTable = new DataTable("Table");
            //データチェック（2行未満の場合はエラー）
            if (csvData.Length < 2)
            {
                return;
            }
            //カラム名の追加
            for (int i = 0; i < csvData[0].Length; i++)
            {
                dataTable.Columns.Add(csvData[0][i]);
            }
            //データの追加
            for (int i = 1; i < csvData.Length; i++)
            {
                dataTable.Rows.Add(csvData[i]);
            }
            DataGridView.DataSource = dataTable;
        }
    }
}
