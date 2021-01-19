using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JPProject.Sso.AspNetIdentity.Models
{
    public class QuickValidator
    {
        public enum User
        {
            FirstNameMaxLength = 50,
            LastNameMaxLength = 50,
            EmailMaxLength = 50,
            FirstNameMinLength = 2,
            LastNameMinLength = 2,
            EmailMinLength = 7
        }
        public enum State
        {
            NameMaxLength = 20
        }

        public enum Business
        {
            NameMaxLength = 50,
            NameMinLength = 2,
            PhoneMaxLength = 20,
            PhoneMinLength = 9,
            EmailMaxLength = 50,
            WebsiteMaxLength = 50,
            LogoMaxLength = 500,
            CountryMaxLength = 50,
            CurrencyMaxLength = 50,
        }

    }
}
