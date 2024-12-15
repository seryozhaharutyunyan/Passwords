using Passwords.Model;
using Passwords.Repositories.Interfaces;

namespace Passwords.Components.Pages
{
    public partial class Home
    {
        public IEnumerable<Data> data;
        private Code code;
        public IDataRepository dataRepository;
        public ICodeRepository codeRepository;
        public int count = 0;

        public Home(IDataRepository dataRepository, ICodeRepository codeRepository)
        {
            this.dataRepository = dataRepository;
            this.codeRepository = codeRepository;
            data = this.dataRepository.RetrieveAllAsync().Result;
            code = codeRepository.RetrieveAll()?[0];
        }
    }
}