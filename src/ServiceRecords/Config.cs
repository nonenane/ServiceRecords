using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.ComponentModel;
using System.IO;

namespace ServiceRecords
{
    public class Config
    {
        public static Procedures hCntMain { get; set; } //осн. коннект
        public static Procedures hCntSecond { get; set; } //доп. коннект

        public static Procedures hCntDocumentsDZ { get; set; } //DocumentsDZ

        public static string[] RunArguments = null;

        public static string CodeUser = "";

        public static DataTable bufferDataTable;

        public enum StatusSZ {Отчет_проверен = 18, Отчет_отклонен = 19, Отчет_подтвержден = 20 }

        public static string centralText(string str)
        {
            int[] arra = new int[255];
            int count = 0;
            int maxLength = 0;
            int indexF = -1;
            arra[count] = 0;
            count++;
            indexF = str.IndexOf("\n");
            arra[count] = indexF;
            while (indexF != -1)
            {
                count++;
                indexF = str.IndexOf("\n", indexF + 1);
                arra[count] = indexF;
            }
            maxLength = arra[1] - arra[0];
            for (int i = 1; i < count; i++)
            {
                if (maxLength < (arra[i] - arra[i - 1]))
                {

                    maxLength = arra[i] - arra[i - 1];
                    if (i >= 2)
                    {
                        maxLength = maxLength - 1;
                    }
                }
            }
            string newString = "";
            string buffString = "";
            for (int i = 1; i < count; i++)
            {
                if (i >= 2)
                {

                    buffString = str.Substring(arra[i - 1] + 1, (arra[i] - arra[i - 1] - 1));
                    buffString = buffString.PadLeft(Convert.ToInt32(buffString.Length + ((maxLength - (arra[i] - arra[i - 1] - 1)) / 2) * 1.8));
                    //    buffString = buffString.PadRight(buffString.Length + ((maxLength - (arra[i] - arra[i - 1] - 1)) / 2)*2);
                    newString += buffString + "\n";
                }
                else
                {
                    buffString = str.Substring(arra[i - 1], arra[i]);
                    buffString = buffString.PadLeft(Convert.ToInt32(buffString.Length + ((maxLength - (arra[i] - arra[i - 1] - 1)) / 2) * 1.8));
                    // buffString = buffString.PadRight(buffString.Length + ((maxLength - (arra[i] - arra[i - 1])) / 2)*2);
                    newString = buffString + "\n";
                }

            }

            return newString;
        }

        public static Guid GetHashString(string s)
        {
            //переводим строку в байт-массим  
            byte[] bytes = Encoding.Unicode.GetBytes(s);

            //создаем объект для получения средст шифрования  
            MD5CryptoServiceProvider CSP =
                new MD5CryptoServiceProvider();

            //вычисляем хеш-представление в байтах  
            byte[] byteHash = CSP.ComputeHash(bytes);

            string hash = string.Empty;

            //формируем одну цельную строку из массива  
            foreach (byte b in byteHash)
                hash += string.Format("{0:x2}", b);

            return new Guid(hash);
        }

        //метод дешифрования строки
        public static string Decode(string ciphText, string password,
               string sol = "народный", string cryptographicAlgorithm = "SHA1",
               int passIter = 2, string initVec = "a8doSuDitOz1hZe#",
               int keySize = 256)
        {
          if (string.IsNullOrEmpty(ciphText))
            return "";

          byte[] initVecB = Encoding.ASCII.GetBytes(initVec);
          byte[] solB = Encoding.ASCII.GetBytes(sol);
          byte[] cipherTextBytes = Convert.FromBase64String(ciphText);

          PasswordDeriveBytes derivPass = new PasswordDeriveBytes(password, solB, cryptographicAlgorithm, passIter);
          byte[] keyBytes = derivPass.GetBytes(keySize / 8);

          RijndaelManaged symmK = new RijndaelManaged();
          symmK.Mode = CipherMode.CBC;

          byte[] plainTextBytes = new byte[cipherTextBytes.Length];
          int byteCount = 0;

          using (ICryptoTransform decryptor = symmK.CreateDecryptor(keyBytes, initVecB))
          {
            using (MemoryStream mSt = new MemoryStream(cipherTextBytes))
            {
              using (CryptoStream cryptoStream = new CryptoStream(mSt, decryptor, CryptoStreamMode.Read))
              {
                byteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
                mSt.Close();
                cryptoStream.Close();
              }
            }
          }

          symmK.Clear();
          return Encoding.UTF8.GetString(plainTextBytes, 0, byteCount);
        }

        public static List<int> listSelectedDZ = new List<int>();
    }
}
