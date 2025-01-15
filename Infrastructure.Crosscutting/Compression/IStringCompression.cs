using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Crosscutting.Compression
{
    public interface IStringCompression
    {
        string Compress(string value);
        string Uncompress(string value);
    }
}
