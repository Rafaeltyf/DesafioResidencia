using Infrastructure.Crosscutting.Compression;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Crosscutting.NetFramework.Compression
{
    public class GzipStringCompressionFactory : IStringCompressionFactory
    {
        public IStringCompression Create()
        {
            return new GzipStringCompression();
        }
    }
}
