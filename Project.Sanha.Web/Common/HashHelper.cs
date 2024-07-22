using System;
using System.Security.Cryptography;
using System.Text;
namespace Project.Sanha.Web.Common
{
	public static class HashHelper
	{
		public static string GenerateMD5Hash(string input)
		{
            string hash = "giveAnyKeyCodeHere@123.-_";
            byte[] data = Convert.FromBase64String(input);

            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            TripleDESCryptoServiceProvider tripleDES = new TripleDESCryptoServiceProvider();

            tripleDES.Key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(hash));
            tripleDES.Mode = CipherMode.ECB;

            ICryptoTransform transform = tripleDES.CreateDecryptor();
            byte[] result = transform.TransformFinalBlock(data, 0, data.Length);

            return UTF8Encoding.UTF8.GetString(result);
        }


        public static string DecodeFrom64(this string encryptData)
        {
            byte[] encodedDataAsBytes = System.Convert.FromBase64String(encryptData);
            string returnValue = System.Text.ASCIIEncoding.ASCII.GetString(encodedDataAsBytes);
            return returnValue;
        }

        public static string Encrypt(string input)
        {
            string hash = "giveAnyKeyCodeHere@123.-_";
            byte[] data = UTF8Encoding.UTF8.GetBytes(input);

            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            TripleDESCryptoServiceProvider tripleDES = new TripleDESCryptoServiceProvider();

            tripleDES.Key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(hash));
            tripleDES.Mode = CipherMode.ECB;

            ICryptoTransform transform = tripleDES.CreateEncryptor();
            byte[] result = transform.TransformFinalBlock(data, 0, data.Length);

            return Convert.ToBase64String(result);
        }

        public static string GenerateApproveNumber(int count, string projectId, string unitId)
        {
            int newCount = count + 1;

            string approveString = newCount.ToString("D5");

            string year = DateTime.Now.ToString("yy");
            string month = DateTime.Now.ToString("MM");

            string result = projectId+'.'+ unitId +'.'+ year +'.'+ month +'.'+ approveString;
            return result;
        } 
    }

}

