using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Crosscutting.Compression
{
    public static class StringCompressionFactory
    {
        #region Members

        static IStringCompressionFactory _stringCompressionFactory = null;

        #endregion

        #region Public Static Methods


        public static void SetCurrent(IStringCompressionFactory compressionFactory)
        {
            _stringCompressionFactory = compressionFactory;
        }

        public static IStringCompression CreateStringCompression()
        {
            return _stringCompressionFactory != null ? _stringCompressionFactory.Create() : null;
        }
        #endregion
    }
}
