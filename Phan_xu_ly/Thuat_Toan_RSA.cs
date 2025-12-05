using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DO_AN_BMCSDL.Phan_GUI
{
    public class Grey
    {
        public static void Run()
        {
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                string publicKey = Convert.ToBase64String(rsa.ExportCspBlob(false));
                string privateKey = Convert.ToBase64String(rsa.ExportCspBlob(true));

                string currentDirectory = Path.GetDirectoryName(typeof(Grey).Assembly.Location);

                File.WriteAllText(Path.Combine(currentDirectory, "public_key.txt"), publicKey);
                File.WriteAllText(Path.Combine(currentDirectory, "private_key.txt"), privateKey);
            }
        }
    }
    public class Encrypt_RSA
    {
        private RSACryptoServiceProvider _rsa;
        public Encrypt_RSA(string publickey)
        {
            _rsa = new RSACryptoServiceProvider();
            _rsa.ImportCspBlob(Convert.FromBase64String(publickey));
        }
        public byte[] EncryptWithRSAPublickey(byte[] data)
        {
            return _rsa.Encrypt(data, true);
        }
        public string Encrypt(string plaintext)
        {
            byte[] encryptedData = EncryptWithRSAPublickey(Encoding.UTF8.GetBytes(plaintext));
            return Convert.ToBase64String(encryptedData);
        }

        public static string Run(string plaintext)
        {
            string ketqua = "";
            string nl = Environment.NewLine;

            ketqua = "Plain Text: " + plaintext + nl;

            string publicKeyFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "public_key.txt");
            string publicKey = File.ReadAllText(publicKeyFilePath);

            ketqua += "Public Key: " + publicKey + nl;

            Encrypt_RSA encryptRsa = new Encrypt_RSA(publicKey);
            string encryptedText = encryptRsa.Encrypt(plaintext);

            string encryptedTextFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "encryptedText.txt");
            File.WriteAllText(encryptedTextFilePath, encryptedText);

            ketqua += "Encrypted Text: " + encryptedText + nl;
            ketqua += "Encrypted text saved to: " + encryptedTextFilePath;

            return ketqua;
        }
    }
    public class Decrypt_RSA
    {
        private RSACryptoServiceProvider _rsa;

        public Decrypt_RSA(string privatekey)
        {
            _rsa = new RSACryptoServiceProvider();
            _rsa.ImportCspBlob(Convert.FromBase64String(privatekey));
        }
        public string Decrypt(string encryptedtext)
        {
            byte[] decryptedData = _rsa.Decrypt(Convert.FromBase64String(encryptedtext), true);
            return Encoding.UTF8.GetString(decryptedData);
        }

        public static string Run()
        {
            string ketqua = "";
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            string nl = Environment.NewLine;
            string encryptedTextFilePath = Path.Combine(baseDir, "encryptedText.txt");
            string encryptedText = File.ReadAllText(encryptedTextFilePath);
            ketqua += "Encrypted Text: " + encryptedText + nl;

            string privateKeyFilePath = Path.Combine(baseDir, "private_key.txt");
            string privateKey = File.ReadAllText(privateKeyFilePath);
            ketqua += "Private Key (truncated): " + privateKey.Substring(0, 50) + " ..." + nl;

            // Giải mã
            Decrypt_RSA decryptRsa = new Decrypt_RSA(privateKey);
            string decryptedText = decryptRsa.Decrypt(encryptedText);
            ketqua += "Decrypted Text: " + decryptedText + nl;

            return ketqua;
        }
    }
}