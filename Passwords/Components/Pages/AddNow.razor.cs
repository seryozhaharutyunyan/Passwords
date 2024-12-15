using Passwords.Model;
using Passwords.Helpers;
using Passwords.Repositories.Interfaces;

namespace Passwords.Components.Pages
{
    public partial class AddNow
    {
        public Data data = new();
        public Error error = new();
        public string siteName = "";
        public string emailOrPone = "";
        public string password = "";
        private Code code;
        public bool flag = false;
        public IDataRepository dataRepository;
        public ICodeRepository codeRepository;

        public AddNow(IDataRepository dataRepository,
            ICodeRepository codeRepository) 
        {
            this.dataRepository = dataRepository;
            this.codeRepository = codeRepository;

            code = codeRepository.RetrieveAll()?[0];
        }

        public void Save()
        {
            error.siteNameError = "";
            error.emailOrPoneError = "";
            error.passwordError = "";
            siteName = siteName.Trim();
            emailOrPone = emailOrPone.Trim();
            password = password.Trim();

            error = ValidHelper.IsValid(siteName, emailOrPone, password);

            flag = error.flag;

            if (!flag)
            {
                return;
            }

            if (code is null)
            {
                flag = false;
                return;
            }
            data.SiteName = siteName;
            data.EmailOrPone = emailOrPone;
            data.Password = EncodeDecodeHelper.Encrypt(password, code.Key);
            dataRepository.CreateAsync(data);
            siteName = "";
            emailOrPone = "";
            password = "";
        }

    }
}