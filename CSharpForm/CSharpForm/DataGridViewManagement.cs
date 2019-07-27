using System;
using System.Collections.Generic;
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
            //TODO: データ表示
            
        }
    }
}
