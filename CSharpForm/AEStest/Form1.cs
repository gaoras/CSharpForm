using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Lib_Encryption;

namespace AEStest
{
    public partial class Form1 : Form
    {
        Encryption enc = new Encryption();
        string ExeFilePath = "";

        public Form1()
        {
            InitializeComponent();
        }
#if false
        private void button1_Click(object sender, EventArgs e)
        {
            string outpath = path;
            if (enc.FileEncrypt(path, "123"))
            {
                MessageBox.Show("完了");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (StreamWriter sw = new StreamWriter(path, false))
            {
                StringBuilder sb = new StringBuilder(5000);
                for (int i = 0; i < 5000; i++)
                {
                    sb.Append(i.ToString());
                }
                sw.Write(sb.ToString());
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string data = "";
            using (StreamReader sr = new StreamReader(path))
            {
                data = sr.ReadToEnd();
            }
            enc.FileEncrypt(data, "123", path);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string data;
            enc.FileDecrypt(encPath, "123", out data);
        }
#endif
        private void Form1_Load(object sender, EventArgs e)
        {
            RB_enc.Checked = true;
        }

        /// <summary>
        /// ドロップ時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_DragDrop(object sender, DragEventArgs e)
        {
            string[] DropFilePath = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            if(File.Exists(DropFilePath[0]))
            {
                ExeFilePath = DropFilePath[0];
                TB_FileName.Text = Path.GetFileName(ExeFilePath);
            }
            else
            {
                MessageBox.Show("ファイルをドロップしてください");
            }
        }
        
        /// <summary>
        /// ファイルのドラッグ処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.All;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }

        }

        /// <summary>
        /// 処理開始
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BT_exe_Click(object sender, EventArgs e)
        {
            //ファイル入力チェック
            if (string.IsNullOrEmpty(ExeFilePath))
            {
                MessageBox.Show("ファイルをドロップしてください");
                return;
            }
            //パスワード入力チェック
            if (string.IsNullOrEmpty(TB_password.Text))
            {
                MessageBox.Show("パスワードを入力してください");
                return;
            }

            if (RB_enc.Checked)
            {
                //暗号化
                bool ret = this.Encode();
                if (ret)
                {
                    MessageBox.Show("完了");
                }
                else
                {
                    MessageBox.Show("失敗");
                }
            }
            else if(RB_dec.Checked)
            {
                //復号化
                bool ret = this.Decode();
                if (ret)
                {
                    MessageBox.Show("完了");
                }
                else
                {
                    MessageBox.Show("失敗");
                }
            }
            else
            {
                MessageBox.Show("処理内容を選択してください");
            }
        }

        private bool Encode()
        {
            bool ret = enc.FileEncrypt(ExeFilePath, TB_password.Text);
            return ret;
        }
        private bool Decode()
        {
            bool ret = enc.FileDecrypt(ExeFilePath, TB_password.Text);
            return ret;
        }
    }
}
