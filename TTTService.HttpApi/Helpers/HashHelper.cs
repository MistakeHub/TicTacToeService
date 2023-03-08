using System.Text;
using XSystem.Security.Cryptography;

namespace TTTService.HttpApi.Helpers
{
    public static class HashHelper
    {

        public static string GetHashString(string str)
        {

            byte[] bytes = Encoding.Unicode.GetBytes(str);

            //создаем объект для получения средст шифрования  
            MD5CryptoServiceProvider CSP =
                new MD5CryptoServiceProvider();

            //вычисляем хеш-представление в байтах  
            byte[] byteHash = CSP.ComputeHash(bytes);

            string hash = string.Empty;

            //формируем одну цельную строку из массива  
            foreach (byte b in byteHash)
                hash += string.Format("{0:x2}", b);

            return hash;

        }
    }
}
