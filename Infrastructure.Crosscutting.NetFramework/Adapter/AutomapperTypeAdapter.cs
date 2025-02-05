﻿//===================================================================================
// Microsoft Developer & Platform Evangelism
//=================================================================================== 
// THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, 
// EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED WARRANTIES 
// OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.
//===================================================================================
// Copyright (c) Microsoft Corporation.  All Rights Reserved.
// This code is released under the terms of the MS-LPL license, 
// http://microsoftnlayerapp.codeplex.com/license
//===================================================================================

namespace Infrastructure.Crosscutting.NetFramework.Adapter
{
    using AutoMapper;
    using Infrastructure.Crosscutting.Adapter;

    /// <summary>
    /// Automapper type adapter implementation
    /// </summary>
    public class AutomapperTypeAdapter
        : ITypeAdapter
    {
        #region ITypeAdapter Members

        /// <summary>
        /// <see cref="ITypeAdapter"/>
        /// </summary>
        /// <typeparam name="TSource"><see cref="ITypeAdapter"/></typeparam>
        /// <typeparam name="TTarget"><see cref="ITypeAdapter"/></typeparam>
        /// <param name="source"><see cref="ITypeAdapter"/></param>
        /// <returns><see cref="ITypeAdapter"/></returns>
        public TTarget Adapt<TSource, TTarget>(TSource source, IConfigurationProvider provider)
            where TSource : class
            where TTarget : class, new()
        {
            return new Mapper(provider).Map<TSource, TTarget>(source);
        }

        /// <summary>
        /// <see cref="ITypeAdapter"/>
        /// </summary>
        /// <typeparam name="TTarget"><see cref="ITypeAdapter"/></typeparam>
        /// <param name="source"><see cref="ITypeAdapter"/></param>
        /// <returns><see cref="ITypeAdapter"/></returns>
        public TTarget Adapt<TTarget>(object source, IConfigurationProvider provider) where TTarget : class, new()
        {
            return new Mapper(provider).Map<TTarget>(source);
        }

        #endregion
    }
}
