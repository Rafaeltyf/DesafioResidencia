using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Crosscutting.Extensions
{
    public interface IStringExtensions
    {
        string ConvertToMD5(string value);

        string ConvertToCrypt(string S);
    }
}
