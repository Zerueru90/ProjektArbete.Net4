using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;
using System.Dynamic;
using System.ComponentModel;
using System.Globalization;
using System.Security.Cryptography.X509Certificates;

namespace Logic
{
    public static class RegexValidation
    {
        #region VerifyEmail
        public static bool VerifyEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                // Normaliserr domänen
                email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,
                                      RegexOptions.None, TimeSpan.FromMilliseconds(200));

                // Undersöker domändelen av e-postmeddelandet och normaliserar det
                string DomainMapper(Match match)
                {
                    // Använd IdnMapping-klassen för att konvertera Unicode-domännamn.
                    var idn = new IdnMapping();

                    // Dra ut och bearbeta domännamn(kastar ArgumentException på ogiltigt)
                    string domainName = idn.GetAscii(match.Groups[2].Value);

                    return match.Groups[1].Value + domainName;
                }
            }
            catch (RegexMatchTimeoutException e)
            {
                return false;
            }
            catch (ArgumentException e)
            {
                return false;
            }
            try
            {
                return Regex.IsMatch(email,
                    @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }
        #endregion
        #region PasswordVerf
        public static bool VerifyPassword(string password)
        {
            var input = password;
            //ErrorMessage = string.Empty;

            if (string.IsNullOrWhiteSpace(input))
            {
                throw new Exception("Password should not be empty");
            }

            var hasNumber = new Regex(@"[0-9]+");
            var hasUpperChar = new Regex(@"[A-Z]+");
            var hasLowerChar = new Regex(@"[a-z]+");

            if (!hasLowerChar.IsMatch(input))
            {
                //ErrorMessage = "Password should contain At least one lower case letter";
                return false;
            }
            else if (!hasUpperChar.IsMatch(input))
            {
                //ErrorMessage = "Password should contain At least one upper case letter";
                return false;
            }
            else if (!hasNumber.IsMatch(input))
            {
                //ErrorMessage = "Password should contain At least one numeric value";
                return false;
            }
            else
            {
                return true;
            }

        }
        #endregion
        public static bool checkForEmail(string email)
        {
            bool IsValid = false;
            Regex r = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            if (r.IsMatch(email))
            {
                IsValid = true;
            }
            return IsValid;
        }
    }
}
