
using Microsoft.AspNetCore.Components;
using Passwords.Helpers;
using Passwords.Model;
using Passwords.Repositories.Interfaces;

namespace Passwords.Components.Layout
{
    public partial class MainLayout 
    {
        public Code? code = null;
        private readonly ICodeRepository repository;
        public MainPage mainPage;
        public string pin = "";
        public string rPin = "";
        public string err = "";
        bool flag = false;
       
        public MainLayout(ICodeRepository repository) 
        { 
            this.repository = repository;
            mainPage = App.Current.MainPage as MainPage;
            code = repository.RetrieveAll()?[0];  
        }

        public async void  Save()
        {
            pin = pin.Trim();
            rPin = rPin.Trim();
            err = "";
            if(pin == "" | rPin == "" | pin is null | rPin is null)
            {
                err = "Fill in the field";
                return;
            }
                if(pin.Length >= 6 & rPin.Length >= 6)
                {
                    if (rPin != pin)
                    {
                        err = "Pin don't match!";
                        return;
                    }
                    string pinResult = EncodeDecodeHelper.GenerateHash(pin);
                    Code data = new()
                    {
                        Key = pinResult,
                    };
                
                    Code? code = await repository.CreateAsync(data);
                    mainPage.flag = false;
                    Refresh(mainPage);
                    return;
                }
            
            err = "Incorrect data!";
            return;
        }

        public void GetPin(string pin)
        {
            this.pin = pin;
        }
        
        public void GetRPin(string rPin)
        { 
            this.rPin = rPin;
        }

        public void Validate()
        {
            pin = pin.Trim();
            if (!EncodeDecodeHelper.ValidatePassword(pin, code.Key))
            {
                err = "Incorrect pin code!";
                return;
            }
            mainPage.flag = true;
        }

        public void Refresh(MainPage? mainPage)
        {
            mainPage.Reload(nav.Uri);
        }

    }
}