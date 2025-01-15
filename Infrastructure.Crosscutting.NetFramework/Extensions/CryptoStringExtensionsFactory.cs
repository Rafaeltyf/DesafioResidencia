using Infrastructure.Crosscutting.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Crosscutting.NetFramework.Extensions
{
    public class CryptoStringExtensionsFactory : IStringExtensionsFactory
    {
        public IStringExtensions Create()
        {
            return new CryptoStringExtensions();
        }
    }
}
