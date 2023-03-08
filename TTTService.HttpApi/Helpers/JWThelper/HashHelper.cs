using System.Security.Cryptography;
using System.Text;

namespace TTTService.HttpApi.Helpers.JWThelper
{
    public static class HashHelper
    {
        public const string SALT = "$2y$10$";
        public static string GetHashString(string str)
        {

            byte[] bytes = Encoding.Unicode.GetBytes(str);

            //создаем объект для получения средст шифрования  
           

            //вычисляем хеш-представление в байтах  
            byte[] byteHash = SHA1.Create().ComputeHash(bytes); ;

            string hash = SALT;

            //формируем одну цельную строку из массива  
            foreach (byte b in byteHash)
                hash += string.Format("{0:x2}", b);

            return hash;

        }
    }
}
