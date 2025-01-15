using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Crosscutting.Passport
{
    public static class PassportConfigFactory
    {
        static IPassportConfigFactory _passportConfigFactory = null;

        public static void SetCurrent(IPassportConfigFactory passportConfigFactory)
        {
            _passportConfigFactory = passportConfigFactory;
        }

        public static IPassportConfig CreatePassportConfig()
        {
            return (_passportConfigFactory != null) ? _passportConfigFactory.Create() : null;
        }
    }
}
