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
    public class Thuat_Toan_RSA
    {
        public BigInteger p;   // Số nguyên tố thứ nhất
        public BigInteger q;   // Số nguyên tố thứ hai
        public BigInteger N;   // N = p * q
        public BigInteger phi; // Euler totient của N
        public BigInteger e;   // Số nguyên tố cùng nhau với phi
        public BigInteger d;   // Nghịch đảo modular của e
        private static Random random = new Random();

        public Thuat_Toan_RSA(int keySize)
        {
            GenerateKeys(keySize);
        }

        // Sinh cặp khóa RSA
        public void GenerateKeys(int bitSize)
        {
            List<BigInteger> primes = GeneratePrimes(bitSize);
            p = primes[0];
            q = primes[1];
            N = p * q;
            phi = (p - 1) * (q - 1);
            e = FindE(phi);
            d = CalculateD(e, phi);
        }

        // Mã hóa một chuỗi ký tự thành một mảng số nguyên lớn
        public BigInteger[] Encrypt(string plaintext)
        {
            char[] chars = plaintext.ToCharArray();
            BigInteger[] encryptedValues = new BigInteger[chars.Length];

            for (int i = 0; i < chars.Length; i++)
            {
                byte[] asciiBytes = Encoding.ASCII.GetBytes(chars[i].ToString());
                BigInteger encryptedValue = BigInteger.ModPow(new BigInteger(asciiBytes), e, N);
                encryptedValues[i] = encryptedValue;
            }
            return encryptedValues;
        }

        // Giải mã mảng số nguyên lớn thành một chuỗi ký tự
        public string Decrypt(BigInteger[] encryptedValues)
        {
            byte[] bytes = new byte[encryptedValues.Length];

            for (int i = 0; i < encryptedValues.Length; i++)
            {
                BigInteger decryptedValue = BigInteger.ModPow(encryptedValues[i], d, N);
                bytes[i] = decryptedValue.ToByteArray()[0];
            }

            string message = Encoding.ASCII.GetString(bytes);
            return message;
        }

        // Sinh các số nguyên tố cần thiết cho việc tạo khóa RSA
        private List<BigInteger> GeneratePrimes(int bitSize)
        {
            List<BigInteger> primes = new List<BigInteger>();

            while (primes.Count < 2)
            {
                BigInteger prime = GeneratePrime(bitSize);
                if (IsPrime(prime) && !primes.Contains(prime))
                {
                    primes.Add(prime);
                }
            }

            return primes;
        }

        // Sinh số nguyên tố ngẫu nhiên
        private BigInteger GeneratePrime(int bitSize)
        {
            BigInteger prime;
            do
            {
                byte[] buffer = new byte[bitSize / 8];
                random.NextBytes(buffer);
                prime = new BigInteger(buffer);
                prime = BigInteger.Abs(prime);
            } while (!IsPrime(prime));

            return prime;
        }

        // Kiểm tra xem một số có phải là số nguyên tố hay không
        private bool IsPrime(BigInteger number)
        {
            if (number <= BigInteger.One)
                return false;
            if (number == 2 || number == 3)
                return true;
            if (number % 2 == 0 || number % 3 == 0)
                return false;

            BigInteger sqrt = Sqrt(number);
            for (BigInteger i = 5; i <= sqrt; i += 6)
            {
                if (number % i == 0 || number % (i + 2) == 0)
                    return false;
            }

            return true;
        }

        // Tính căn bậc hai của một số nguyên dương
        private BigInteger Sqrt(BigInteger number)
        {
            if (number == BigInteger.Zero)
                return BigInteger.Zero;

            BigInteger n = number;
            BigInteger x0 = n / 2;
            BigInteger x1 = (x0 + n / x0) / 2;

            while (x1 < x0)
            {
                x0 = x1;
                x1 = (x0 + n / x0) / 2;
            }

            return x0;
        }

        // Tìm số nguyên e thỏa mãn gcd(e, phi) = 1
        private BigInteger FindE(BigInteger phi)
        {
            BigInteger e = 5;
            while (e < phi)
            {
                if (IsCoprime(e, phi))
                    return e;
                e++;
            }
            return BigInteger.Zero;
        }

        // Kiểm tra hai số có nguyên tố cùng nhau hay không
        private bool IsCoprime(BigInteger a, BigInteger b)
        {
            return BigInteger.GreatestCommonDivisor(a, b) == BigInteger.One;
        }

        // Tính d = e^-1 mod phi (sử dụng Euclid mở rộng)
        private BigInteger CalculateD(BigInteger e, BigInteger phi)
        {
            BigInteger d = ExtendedEuclideanAlgorithm(e, phi);
            if (d < 0)
                d += phi;
            return d;
        }

        // Thuật toán Euclid mở rộng
        private BigInteger ExtendedEuclideanAlgorithm(BigInteger a, BigInteger b)
        {
            BigInteger old_r = a, r = b;
            BigInteger old_s = 1, s = 0;
            BigInteger old_t = 0, t = 1;

            while (r != 0)
            {
                BigInteger quotient = old_r / r;
                BigInteger temp = r;
                r = old_r - quotient * r;
                old_r = temp;

                temp = s;
                s = old_s - quotient * s;
                old_s = temp;

                temp = t;
                t = old_t - quotient * t;
                old_t = temp;
            }

            return old_s;
        }
    }
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