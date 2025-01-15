//===================================================================================
// microsoft developer & platform evangelism
//=================================================================================== 
// this code and information are provided "as is" without warranty of any kind, 
// either expressed or implied, including but not limited to the implied warranties 
// of merchantability and/or fitness for a particular purpose.
//===================================================================================
// copyright (c) microsoft corporation.  all rights reserved.
// this code is released under the terms of the ms-lpl license, 
// http://microsoftnlayerapp.codeplex.com/license
//===================================================================================
 
using Infrastructure.Crosscutting.Adapter;

namespace infrastructure.crosscutting.netframework.adapter
{

    public class automappertypeadapterfactory
        : ITypeAdapterFactory
    {

        /// <summary>
        /// create a new automapper type adapter factory
        /// </summary>
        public automappertypeadapterfactory()
        {

        }

        public ITypeAdapter Create()
        {
            throw new NotImplementedException();
        }
    }
}
