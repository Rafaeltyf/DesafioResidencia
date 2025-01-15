using Infrastructure.Crosscutting.Passport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Crosscutting.NetFramework.Passport
{
    public class PassportConfigFromConfigurationManagerFactory : IPassportConfigFactory
    {
        public IPassportConfig Create()
        {
            return new PassportConfigFromConfigurationManager();
        }
    }
}
