using Passwords.Helpers;
using Passwords.Model;
using Passwords.Repositories.Interfaces;

namespace Passwords.Components.Pages
{
    public partial class NewRegistry
    {
        public string generatedPassword = "";
        public Data data = new();
        public string siteName = "";
        public string emailOrPone = "";
        public string password = "";
        public Error error = new();
        private Code code;
        public bool flag = false;
        public readonly IDataRepository dataRepository;
        public readonly ICodeRepository codeRepository;

        public NewRegistry(IDataRepository dataRepository,
            ICodeRepository codeRepository) 
        {
            this.dataRepository = dataRepository;
            this.codeRepository = codeRepository;

            code = codeRepository.RetrieveAll()?[0];
        }

        public void Generator()
        {
            Random random = new Random();

            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789#$@!*&%~?*";
            generatedPassword = new string(Enumerable.Repeat(chars, 16)
            .Select(s => s[random.Next(s.Length)]).ToArray());
            flag = true;
        }

        public void Save()
        {  
            error.siteNameError = "";
            error.emailOrPoneError = "";
            error.passwordError = "";
            error.flag = true;
            siteName = siteName.Trim();
            emailOrPone = emailOrPone.Trim();
            
            data.Password= generatedPassword;
            password = generatedPassword;

            error = ValidHelper.IsValid(siteName, emailOrPone, password);

            flag = error.flag;

            
            if (!flag)
            {
                return;
            }

            if (code is null) 
            {
                generatedPassword = "";
                flag= false;
                return;
            }

            data.SiteName = siteName;
            data.EmailOrPone = emailOrPone;
            data.Password = EncodeDecodeHelper.Encrypt(data.Password, code.Key);
            dataRepository.CreateAsync(data);
            siteName = "";
            emailOrPone = "";
            generatedPassword = "";
        }
    }
}
