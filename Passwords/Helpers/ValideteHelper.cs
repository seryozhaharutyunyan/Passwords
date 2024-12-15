using System.Globalization;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Passwords.Model;


namespace Passwords.Helpers
{
    public class ValidHelper
    {
        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,
                                      RegexOptions.None, TimeSpan.FromMilliseconds(200));

                string DomainMapper(Match match)
                {
                    var idn = new IdnMapping();

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

        public static bool IsValidPone(string pone)
        {
            if (string.IsNullOrWhiteSpace(pone))
                return false;

            try
            {
                return Regex.IsMatch(pone,
                    @"^\+\d*$");
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }

        public static Error IsValid(string siteName, string emailOrPone, string password)
        {
            Error error = new();
            error.flag = true;
            if (siteName == "" | siteName == null)
            {
                error.siteNameError = "The site name field is empty";
                error.flag =false;
            }
            else if (siteName?.Length <= 1)
            {
                error.siteNameError = "The site name field is not correct";
                error.flag = false;
            }

            if (emailOrPone == "" | emailOrPone == null)
            {
                error.emailOrPoneError = "The email or pone field is empty";
                error.flag = false;
            }
            else if (!ValidHelper.IsValidEmail(emailOrPone) &
                !ValidHelper.IsValidPone(emailOrPone))
            {
                error.emailOrPoneError = "The email or pone field is not correct";
                error.flag = false;
            }

            if (password == "" | password == null)
            {
                error.passwordError = "The password field is empty";
                error.flag = false;
            }
            else if (password.Length <= 5)
            {
                error.passwordError = "The password field is not correct";
                error.flag = false;
            }
            return error;
        }
    }
}
