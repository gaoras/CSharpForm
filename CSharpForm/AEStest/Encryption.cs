using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Compression;
using System.Security.Cryptography;

namespace Lib_Encryption
{
    /// <summary>
    /// 暗号化・復号化するためのクラス
    /// </summary>
    public class Encryption
    {
        private int n;
        public Encryption()
        {
        }

        /// <summary>
        /// string配列を暗号化してファイルに書き込みます
        /// </summary>
        /// <param name="data">暗号化したいデータ</param>
        /// <param name="Password">パスワード</param>
        /// <param name="FilePath">出力パス（フルパス）</param>
        /// <returns>所要時間[msec]</returns>
        public long FileEncrypt(string data, string Password, string FilePath)
        {
            //Stopwatchオブジェクトを作成する
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            //ストップウォッチを開始する
            sw.Start();

            int len;
            byte[] buffer = new byte[4096];

            //Output file path.
            string OutFilePath = Path.Combine(Path.GetDirectoryName(FilePath), Path.GetFileNameWithoutExtension(FilePath)) + ".enc";

            using (FileStream outfs = new FileStream(OutFilePath, FileMode.Create, FileAccess.Write))
            {
                using (AesManaged aes = new AesManaged())
                {
                    aes.BlockSize = 128;              // BlockSize = 16bytes
                    aes.KeySize = 128;                // KeySize = 16bytes
                    aes.Mode = CipherMode.CBC;        // CBC mode
                    aes.Padding = PaddingMode.PKCS7;    // Padding mode is "PKCS7".

                    //入力されたパスワードをベースに擬似乱数を新たに生成
                    Rfc2898DeriveBytes deriveBytes = new Rfc2898DeriveBytes(Password, 16);
                    byte[] salt = new byte[16]; // Rfc2898DeriveBytesが内部生成したなソルトを取得
                    salt = deriveBytes.Salt;
                    // 生成した擬似乱数から16バイト切り出したデータをパスワードにする
                    byte[] bufferKey = deriveBytes.GetBytes(16);

                    aes.Key = bufferKey;
                    // IV ( Initilization Vector ) は、AesManagedにつくらせる
                    aes.GenerateIV();

                    //Encryption interface.
                    ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                    using (CryptoStream cse = new CryptoStream(outfs, encryptor, CryptoStreamMode.Write))
                    {
                        outfs.Write(salt, 0, 16);       // salt をファイル先頭に埋め込む
                        outfs.Write(aes.IV, 0, 16);     // 次にIVもファイルに埋め込む
                        using (DeflateStream ds = new DeflateStream(cse, CompressionMode.Compress)) //圧縮
                        {
                            //char[] c_data = data.ToCharArray();
                            byte[] b_data = Encoding.ASCII.GetBytes(data);
                            int n = b_data.Length;
                            int ptr = 0;
                            len = 4096;
                            while (ptr + len < n)       //ptrから4096バイト分のデータがあるなら
                            {
                                Array.Copy(b_data, ptr, buffer, 0, len);    //4096バイトコピー
                                ds.Write(buffer, 0, len);
                                ptr += len;
                            }
                            //4096未満の余りの処理
                            len = n - ptr;
                            Array.Copy(b_data, ptr, buffer, 0, len);
                            ds.Write(buffer, 0, len);
                        }
                    }
                }
            }
            //ストップウォッチを止める
            sw.Stop();

            //結果を表示する
            long resultTime = sw.ElapsedMilliseconds;

            //Encryption succeed.
            //textBox1.AppendText("暗号化成功: " + Path.GetFileName(OutFilePath) + Environment.NewLine);
            //textBox1.AppendText("実行時間: " + resultTime.ToString() + "ms");

            return (resultTime);
        }

        /// <summary>
        /// 暗号化されたファイルを復号化します
        /// </summary>
        /// <param name="FilePath">暗号化されたファイルパス</param>
        /// <param name="Password">パスワード</param>
        /// <returns></returns>
        public bool FileDecrypt(string FilePath, string Password, out string data)
        {
            int i, len;
            byte[] buffer = new byte[4096];

            if (String.Compare(Path.GetExtension(FilePath), ".enc", true) != 0)
            {
                throw new Exception("ファイルの形式が異なります");
            }

            //Output file path.
            string OutFilePath = Path.Combine(Path.GetDirectoryName(FilePath), Path.GetFileNameWithoutExtension(FilePath)) + ".txt";

            using (FileStream fs = new FileStream(FilePath, FileMode.Open, FileAccess.Read))
            {
                using (AesManaged aes = new AesManaged())
                {
                    aes.BlockSize = 128;              // BlockSize = 16bytes
                    aes.KeySize = 128;                // KeySize = 16bytes
                    aes.Mode = CipherMode.CBC;        // CBC mode
                    aes.Padding = PaddingMode.PKCS7;    // Padding mode is "PKCS7".

                    // salt
                    byte[] salt = new byte[16];
                    fs.Read(salt, 0, 16);

                    // Initilization Vector
                    byte[] iv = new byte[16];
                    fs.Read(iv, 0, 16);
                    aes.IV = iv;

                    // ivをsaltにしてパスワードを擬似乱数に変換
                    Rfc2898DeriveBytes deriveBytes = new Rfc2898DeriveBytes(Password, salt);
                    byte[] bufferKey = deriveBytes.GetBytes(16);    // 16バイトのsaltを切り出してパスワードに変換
                    aes.Key = bufferKey;

                    //Decryption interface.
                    ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                    StringBuilder sb_data = new StringBuilder();
                    using (CryptoStream cse = new CryptoStream(fs, decryptor, CryptoStreamMode.Read))
                    {
                        using (DeflateStream ds = new DeflateStream(cse, CompressionMode.Decompress))   //解凍
                        {
                            while ((len = ds.Read(buffer, 0, 4096)) > 0)
                            {
                                {
                                    string tmp = Encoding.ASCII.GetString(buffer);
                                    sb_data.Append(tmp);
                                }
                            }
                        }
                    }
                    data = sb_data.ToString();
                }
            }
            //Decryption succeed.
            return (true);
        }

        /// <summary>
        /// オリジナル
        /// </summary>
        /// <param name="FilePath"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        public bool FileEncrypt(string FilePath, string Password)
        {
            //Stopwatchオブジェクトを作成する
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            //ストップウォッチを開始する
            sw.Start();

            int i, len;
            byte[] buffer = new byte[4096];

            //Output file path.
            string OutFilePath = Path.Combine(Path.GetDirectoryName(FilePath), Path.GetFileNameWithoutExtension(FilePath)) + ".enc";

            using (FileStream outfs = new FileStream(OutFilePath, FileMode.Create, FileAccess.Write))
            {
                using (AesManaged aes = new AesManaged())
                {
                    aes.BlockSize = 128;              // BlockSize = 16bytes
                    aes.KeySize = 128;                // KeySize = 16bytes
                    aes.Mode = CipherMode.CBC;        // CBC mode
                    aes.Padding = PaddingMode.PKCS7;    // Padding mode is "PKCS7".

                    //入力されたパスワードをベースに擬似乱数を新たに生成
                    Rfc2898DeriveBytes deriveBytes = new Rfc2898DeriveBytes(Password, 16);
                    byte[] salt = new byte[16]; // Rfc2898DeriveBytesが内部生成したなソルトを取得
                    salt = deriveBytes.Salt;
                    // 生成した擬似乱数から16バイト切り出したデータをパスワードにする
                    byte[] bufferKey = deriveBytes.GetBytes(16);

                    /*
                    // パスワード文字列が大きい場合は、切り詰め、16バイトに満たない場合は0で埋めます
                    byte[] bufferKey = new byte[16];
                    byte[] bufferPassword = Encoding.UTF8.GetBytes(Password);
                    for (i = 0; i < bufferKey.Length; i++)
                    {
                        if (i < bufferPassword.Length)
                        {
                            bufferKey[i] = bufferPassword[i];
                        }
                        else
                        {
                            bufferKey[i] = 0;
                        }
                    */

                    aes.Key = bufferKey;
                    // IV ( Initilization Vector ) は、AesManagedにつくらせる
                    aes.GenerateIV();

                    //Encryption interface.
                    ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                    using (CryptoStream cse = new CryptoStream(outfs, encryptor, CryptoStreamMode.Write))
                    {
                        outfs.Write(salt, 0, 16);     // salt をファイル先頭に埋め込む
                        outfs.Write(aes.IV, 0, 16); // 次にIVもファイルに埋め込む
                        using (DeflateStream ds = new DeflateStream(cse, CompressionMode.Compress)) //圧縮
                        {
                            using (FileStream fs = new FileStream(FilePath, FileMode.Open, FileAccess.Read))
                            {
                                while ((len = fs.Read(buffer, 0, 4096)) > 0)
                                {
                                    ds.Write(buffer, 0, len);
                                }
                            }
                        }
                    }
                }
            }
            //ストップウォッチを止める
            sw.Stop();

            //結果を表示する
            long resultTime = sw.ElapsedMilliseconds;

            //Encryption succeed.
            //textBox1.AppendText("暗号化成功: " + Path.GetFileName(OutFilePath) + Environment.NewLine);
            //textBox1.AppendText("実行時間: " + resultTime.ToString() + "ms");

            return (true);
        }

        /// <summary>
        /// オリジナル
        /// </summary>
        /// <param name="FilePath"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        public bool FileDecrypt(string FilePath, string Password)
        {
            int i, len;
            byte[] buffer = new byte[4096];

            if (String.Compare(Path.GetExtension(FilePath), ".enc", true) != 0)
            {
                //The file are not encrypted file! Decryption failed
                //MessageBox.Show("暗号化されたファイルではありません！" + Environment.NewLine + "復号に失敗しました。",
                //    "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return (false); ;
            }

            //Output file path.
            string OutFilePath = Path.Combine(Path.GetDirectoryName(FilePath), Path.GetFileNameWithoutExtension(FilePath)) + ".txt";

            using (FileStream outfs = new FileStream(OutFilePath, FileMode.Create, FileAccess.Write))
            {
                using (FileStream fs = new FileStream(FilePath, FileMode.Open, FileAccess.Read))
                {
                    using (AesManaged aes = new AesManaged())
                    {
                        aes.BlockSize = 128;              // BlockSize = 16bytes
                        aes.KeySize = 128;                // KeySize = 16bytes
                        aes.Mode = CipherMode.CBC;        // CBC mode
                        aes.Padding = PaddingMode.PKCS7;    // Padding mode is "PKCS7".

                        // salt
                        byte[] salt = new byte[16];
                        fs.Read(salt, 0, 16);

                        // Initilization Vector
                        byte[] iv = new byte[16];
                        fs.Read(iv, 0, 16);
                        aes.IV = iv;

                        /*
                        // パスワード文字列が大きい場合は、切り詰め、16バイトに満たない場合は0で埋めます
                        byte[] bufferKey = new byte[16];
                        byte[] bufferPassword = Encoding.UTF8.GetBytes(Password);
                        for (i = 0; i < bufferKey.Length; i++)
                        {
                            if (i < bufferPassword.Length)
                            {
                                bufferKey[i] = bufferPassword[i];
                            }
                            else
                            {
                                bufferKey[i] = 0;
                            }
                        */

                        // ivをsaltにしてパスワードを擬似乱数に変換
                        Rfc2898DeriveBytes deriveBytes = new Rfc2898DeriveBytes(Password, salt);
                        byte[] bufferKey = deriveBytes.GetBytes(16);    // 16バイトのsaltを切り出してパスワードに変換
                        aes.Key = bufferKey;

                        //Decryption interface.
                        ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                        using (CryptoStream cse = new CryptoStream(fs, decryptor, CryptoStreamMode.Read))
                        {
                            using (DeflateStream ds = new DeflateStream(cse, CompressionMode.Decompress))   //解凍
                            {
                                while ((len = ds.Read(buffer, 0, 4096)) > 0)
                                {
                                    outfs.Write(buffer, 0, len);
                                }
                            }
                        }
                    }
                }
            }
            //Decryption succeed.
            //textBox1.AppendText("復号成功: " + Path.GetFileName(OutFilePath) + Environment.NewLine);
            return (true);
        }

    }
}
