using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Crosscutting.Passport
{
    public interface IPassportConfigFactory
    {
        IPassportConfig Create();
    }
}
