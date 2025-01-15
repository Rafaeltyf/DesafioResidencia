using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Crosscutting.Extensions
{
    public static class StringExtensionsFactory
    {
        #region Members

        static IStringExtensionsFactory _stringExtensionsFactory = null;

        #endregion

        #region Public Static Methods

        /// <summary>
        /// Set the current type adapter factory
        /// </summary>
        /// <param name="adapterFactory">The adapter factory to set</param>
        public static void SetCurrent(IStringExtensionsFactory extensionsFactory)
        {
            _stringExtensionsFactory = extensionsFactory;
        }
        /// <summary>
        /// Create a new type adapter from currect factory
        /// </summary>
        /// <returns>Created type adapter</returns>
        public static IStringExtensions CreateStringExtensions()
        {
            return _stringExtensionsFactory != null ? _stringExtensionsFactory.Create() : null;
        }
        #endregion
    }
}
