using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace ChitraLeka.Common
{
    public static class Helper
    {
        public static string GenerateName(int len)
        {
            Random r = new Random();
            string[] consonants = { "b", "c", "d", "f", "g", "h", "j", "k", "l", "m", "l", "n", "p", "q", "r", "s", "sh", "zh", "t", "v", "w", "x" };
            string[] vowels = { "a", "e", "i", "o", "u", "ae", "y" };
            string Name = "";
            Name += consonants[r.Next(consonants.Length)].ToUpper();
            Name += vowels[r.Next(vowels.Length)];
            int b = 2; //b tells how many times a new letter has been added. It's 2 right now because the first two letters are already in the name.
            while (b < len)
            {
                Name += consonants[r.Next(consonants.Length)];
                b++;
                Name += vowels[r.Next(vowels.Length)];
                b++;
            }

            return Name;
        }

        public static DateTime GenerateRandomDate()
        {
            Random rd = new Random();
            int offset = rd.Next(1, 30);
            return DateTime.Now.AddDays(-1 * offset);
        }

        public static string GeneratePassword(string password)
        {
            SHA1Managed sha1m = new SHA1Managed();
            string passwordHash = string.Empty;
            byte[] temp = sha1m.ComputeHash(Encoding.UTF8.GetBytes(password));
            foreach (var _byte in temp)
            {
                passwordHash = passwordHash + _byte.ToString("X2");
            }

            return passwordHash;
        }

        private static Random random = new Random((int)DateTime.Now.Ticks);

        public static string RandomString(int size)
        {
            StringBuilder builder = new StringBuilder();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }

            return builder.ToString();
        }

        public static bool StringIsEmail(string str)
        {
            return new EmailAddressAttribute().IsValid(str);
        }

        public static bool IsAlphaNum(string str)
        {
            if (string.IsNullOrEmpty(str))
                return false;

            return (str.ToCharArray().All(c => Char.IsLetter(c) || Char.IsNumber(c)));
        }

        public static bool ValidateFileBeforeUpload(HttpPostedFileBase file, int maxFileLengthInMB, string fileExtensions)
        {
            bool okToUpload = true;
            if (file == null)
            {
                okToUpload = false;
            }

            if (file.ContentLength > 0)
            {
                string[] AllowedFileExtensions = fileExtensions.Split(',');
                if (!AllowedFileExtensions.Contains(file.FileName.Substring(file.FileName.LastIndexOf('.'))))
                {
                    okToUpload = false;
                }

                else if (file.ContentLength > maxFileLengthInMB)
                {
                    okToUpload = false;
                }
                else
                {
                    okToUpload = true;
                }
            }
            else
            {
                okToUpload = false;
            }

            return okToUpload;
        }

        public static int GetDifferenceInYears(DateTime startDate, DateTime endDate)
        {
            //Excel documentation says "COMPLETE calendar years in between dates"
            int years = endDate.Year - startDate.Year;

            if (startDate.Month == endDate.Month &&// if the start month and the end month are the same
                endDate.Day < startDate.Day)// BUT the end day is less than the start day
            {
                years--;
            }
            else if (endDate.Month < startDate.Month)// if the end month is less than the start month
            {
                years--;
            }

            return years;
        }

        public static string EncryptString(string input)
        {
            SHA1Managed sha1m = new SHA1Managed();
            var temp = sha1m.ComputeHash(System.Text.Encoding.UTF8.GetBytes(input));
            string output = "";
            foreach (var _byte in temp)
            {
                output = output + _byte.ToString("X2");
            }

            return output;
        }
}
}
