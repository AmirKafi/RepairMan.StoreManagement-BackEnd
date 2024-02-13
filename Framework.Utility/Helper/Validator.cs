using Framework.Utility.Convert.String;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Framework.Utility.Helper
{
    public static class Validator
    {
        public static bool EmailValidator(string email)
        {
            if (String.IsNullOrEmpty(email))
                return true;
            var trimmedEmail = email.Trim();

            if (trimmedEmail.EndsWith("."))
            {
                return false; // suggested by @TK-421
            }
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == trimmedEmail;
            }
            catch
            {
                return false;
            }
        }

        public static bool CellPhoneNumberValidator(string cellPhoneNumber)
        {
            String phone_regexp = @"^0?9\d{9}$";
            Match m = Regex.Match(cellPhoneNumber, phone_regexp);
            if (m.Success)
                return true;
            return false;
        }

        public static bool NationalIdValidator(string nationalId)
        {
            if (nationalId == " ")
                return false;
            else if (StringUtility.IsNullOrEmptyOrWhiteSpace(nationalId)) // اجبار باید در خود دامین چک شه
                return true;
            else if (nationalId.Trim().Length < 10)
                return false;
            int result = 0, controlNr = (int)(nationalId[9] - 48);
            for (int i = 0; i < nationalId.Length - 1; i++)
                result += (nationalId[i] - 48) * (10 - i);

            int remainder = result % 11;
            bool isValid = controlNr == (remainder < 2 ? remainder : 11 - remainder);
            return isValid;
        }
    }
}
