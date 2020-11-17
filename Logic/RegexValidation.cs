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
            catch (RegexMatchTimeoutException reg)
            {
                return false;
            }
            catch (ArgumentException ex)
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
        public static void VerifyPassword(string password)
        {
            IsNullorEmpty(password);

            var input = password;
            var hasNumber = new Regex(@"[0-9]+");
            var hasUpperChar = new Regex(@"[A-Z]+");
            var hasLowerChar = new Regex(@"[a-z]+");

            if (!hasLowerChar.IsMatch(input))
            {
                throw new ExceptionHandling.PasswordFormat();
            }
            else if(!hasUpperChar.IsMatch(input))
            {
                throw new ExceptionHandling.PasswordFormat();
            }
            else if (!hasNumber.IsMatch(input))
            {
                throw new ExceptionHandling.PasswordFormat();
            }

        }
        #endregion
        #region EmailLightVersion
        public static bool CheckForEmail(string email)
        {
            IsNullorEmpty(email);
            bool IsValid = false;
            Regex r = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            if (r.IsMatch(email))
            {
                IsValid = true;
            }
            else
            {
                throw new ExceptionHandling.EmailFormatException();
            }
            return IsValid;
        }
        #endregion
        #region LettersOnly
        public static bool LettersOnly(string prop)
        {
            IsNullorEmpty(prop);
            var text = new Regex(@"^[a-zA-Z]+$");

            if (text.IsMatch(prop))
            {
                return true;
            }
            else
                throw new ExceptionHandling.NameFormatException();
                
        }
        #endregion
        #region IsNullorEmpty
        public static void IsNullorEmpty(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                throw new ExceptionHandling.EmptyTextBoxException();
            }
        }
        #endregion
        #region NumbersOnly
        public static void NumberOnly(string input)
        {
            
            var prop = new Regex(@"^[0-9]+$");

            if (!prop.IsMatch(input))
            {
                throw new ExceptionHandling.NumbersOnlyException();
            }
            
        }
        #endregion
        #region RegNumber
        public static void RegistrationNumber(string properties)
        {
            var prop = new Regex(@"^(?=.{0,6}$)[A-Za-z]{3}[0-9]{2}[A-Za-z0-9]{1}");

            if (!prop.IsMatch(properties))
            {
                throw new ExceptionHandling.RegNumberException();
            }
            
        }
        #endregion
    }
}

