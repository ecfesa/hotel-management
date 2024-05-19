using System.Security.Cryptography;
using System.Text;

public static class HashHelper
{
    public static string ComputeSha256Hash(string rawData)
    {

        Console.WriteLine("password raw: " + rawData);

        using (SHA256? sha256Hash = SHA256.Create())
        {
            byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }


            Console.WriteLine("builder:" + builder.ToString());
            return builder.ToString();
        }
    }
}
