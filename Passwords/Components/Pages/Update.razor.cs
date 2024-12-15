using Passwords.Model;
using Passwords.Repositories.Interfaces;
using Passwords.Helpers;

namespace Passwords.Components.Pages
{
    public partial class Update
    {
        public string generatedPassword = "";
        public Data data;
        public Error error = new();
        private Code code;
        public string siteName = "";
        public string emailOrPone = "";
        public string password = "";
        public bool flag = false;
        public IDataRepository dataRepository;
        public ICodeRepository codeRepository;

        public Update(IDataRepository dataRepository,
            ICodeRepository codeRepository, int Id)
        {
            this.dataRepository = dataRepository;
            this.codeRepository = codeRepository;

            code = codeRepository.RetrieveAll()?[0];
            data = this.dataRepository.RetrieveAsync(Id).Result;
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
            siteName = siteName.Trim();
            emailOrPone = emailOrPone.Trim();
            password = password.Trim();

            data.Password = generatedPassword;

            error = ValidHelper.IsValid(siteName, emailOrPone, password);

            flag = error.flag;


            if (!flag)
            {
                return;
            }

            if (code is null)
            {
                generatedPassword = "";
                flag = false;
                return;
            }
            data.Password = EncodeDecodeHelper.Encrypt(data.Password, code.Key);
            //save data
        }
    }

}
