//===================================================================================
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

namespace Infrastructure.Data.Seedwork
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Domain.Seedwork1;
    using Domain.Seedwork1.Specification;
    using Infrastructure.Crosscutting.Logging;
    using Infrastructure.Data.Seedwork.Resources;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Repository base class
    /// </summary>
    /// <typeparam name="TView">The type of underlying entity in this repository</typeparam>
    public class RepositoryView<TView> : IRepositoryView<TView> where TView : class
    {
        #region Members

        IQueryableUnitOfWork _UnitOfWork;

        #endregion

        #region Constructor

        /// <summary>
        /// Create a new instance of repository
        /// </summary>
        /// <param name="unitOfWork">Associated Unit Of Work</param>
        public RepositoryView(IQueryableUnitOfWork unitOfWork)
        {
            if (unitOfWork == null)
                throw new ArgumentNullException("unitOfWork");

            _UnitOfWork = unitOfWork;
        }

        #endregion

        #region IRepository Members

        /// <summary>
        /// <see cref="IRIS.Domain.Seedwork.IRepository{TValueObject}"/>
        /// </summary>
        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _UnitOfWork;
            }
        }

        /// <summary>
        /// <see cref="IRIS.Domain.Seedwork.IRepository{TValueObject}"/>
        /// </summary>
        /// <param name="item"><see cref="IRIS.Domain.Seedwork.IRepository{TValueObject}"/></param>
        public virtual void Add(TView item)
        {

            if (item != null)
                GetSet().Add(item); // add new item in this set
            else
            {
                LoggerFactory.CreateLog()
                          .LogInfo(Messages.info_CannotAddNullEntity, typeof(TView).ToString());

            }

        }
        /// <summary>
        /// <see cref="IRIS.Domain.Seedwork.IRepository{TValueObject}"/>
        /// </summary>
        /// <param name="item"><see cref="IRIS.Domain.Seedwork.IRepository{TValueObject}"/></param>
        public virtual void Remove(TView item)
        {
            if (item != null)
            {
                //attach item if not exist
                _UnitOfWork.Attach(item);

                //set as "removed"
                GetSet().Remove(item);
            }
            else
            {
                LoggerFactory.CreateLog()
                          .LogInfo(Messages.info_CannotRemoveNullEntity, typeof(TView).ToString());
            }
        }

        /// <summary>
        /// <see cref="IRIS.Domain.Seedwork.IRepository{TValueObject}"/>
        /// </summary>
        /// <param name="item"><see cref="IRIS.Domain.Seedwork.IRepository{TValueObject}"/></param>
        public virtual void TrackItem(TView item)
        {
            if (item != null)
                _UnitOfWork.Attach(item);
            else
            {
                LoggerFactory.CreateLog()
                          .LogInfo(Messages.info_CannotRemoveNullEntity, typeof(TView).ToString());
            }
        }

        /// <summary>
        /// <see cref="IRIS.Domain.Seedwork.IRepository{TValueObject}"/>
        /// </summary>
        /// <param name="item"><see cref="IRIS.Domain.Seedwork.IRepository{TValueObject}"/></param>
        public virtual void Modify(TView item)
        {
            if (item != null)
                _UnitOfWork.SetModified(item);
            else
            {
                LoggerFactory.CreateLog()
                          .LogInfo(Messages.info_CannotModifyNullEntity, typeof(TView).ToString());
            }
        }


        /// <summary>
        /// <see cref="IRIS.Domain.Seedwork.IRepository{TValueObject}"/>
        /// </summary>
        /// <returns><see cref="IRIS.Domain.Seedwork.IRepository{TValueObject}"/></returns>
        public virtual IEnumerable<TView> GetAll()
        {
            return GetSet();
        }
        /// <summary>
        /// <see cref="IRIS.Domain.Seedwork.IRepository{TValueObject}"/>
        /// </summary>
        /// <param name="specification"><see cref="IRIS.Domain.Seedwork.IRepository{TValueObject}"/></param>
        /// <returns><see cref="IRIS.Domain.Seedwork.IRepository{TValueObject}"/></returns>
        public virtual IEnumerable<TView> AllMatching(ISpecification<TView> specification)
        {
            return GetSet().Where(specification.SatisfiedBy());
        }
        /// <summary>
        /// <see cref="IRIS.Domain.Seedwork.IRepository{TValueObject}"/>
        /// </summary>
        /// <typeparam name="S"><see cref="IRIS.Domain.Seedwork.IRepository{TValueObject}"/></typeparam>
        /// <param name="pageIndex"><see cref="IRIS.Domain.Seedwork.IRepository{TValueObject}"/></param>
        /// <param name="pageCount"><see cref="IRIS.Domain.Seedwork.IRepository{TValueObject}"/></param>
        /// <param name="orderByExpression"><see cref="IRIS.Domain.Seedwork.IRepository{TValueObject}"/></param>
        /// <param name="ascending"><see cref="IRIS.Domain.Seedwork.IRepository{TValueObject}"/></param>
        /// <returns><see cref="IRIS.Domain.Seedwork.IRepository{TValueObject}"/></returns>
        public virtual IQueryable<TView> GetPaged<KProperty>(int pageIndex, int pageCount, System.Linq.Expressions.Expression<Func<TView, KProperty>> orderByExpression, bool ascending)
        {
            var set = GetSet();

            if (ascending)
            {
                return set.OrderBy(orderByExpression)
                          .Skip(pageCount * pageIndex)
                          .Take(pageCount);
            }
            else
            {
                return set.OrderByDescending(orderByExpression)
                          .Skip(pageCount * pageIndex)
                          .Take(pageCount);
            }
        }

        /// <summary>
        /// <see cref="IRIS.Domain.Seedwork.IRepository{TValueObject}"/>
        /// </summary>
        /// <returns><see cref="Count"/></returns>
        public virtual long GetCountPaged()
        {
            return GetSet().Count();
        }

        /// <summary>
        /// <see cref="IRIS.Domain.Seedwork.IRepository{TValueObject}"/>
        /// </summary>
        /// <param name="filter"><see cref="IRIS.Domain.Seedwork.IRepository{TValueObject}"/></param>
        /// <returns><see cref="IRIS.Domain.Seedwork.IRepository{TValueObject}"/></returns>
        public virtual IEnumerable<TView> GetFiltered(System.Linq.Expressions.Expression<Func<TView, bool>> filter)
        {
            return GetSet().Where(filter);
        }

        /// <summary>
        /// <see cref="IRIS.Domain.Seedwork.IRepository{TValueObject}"/>
        /// </summary>
        /// <param name="persisted"><see cref="IRIS.Domain.Seedwork.IRepository{TValueObject}"/></param>
        /// <param name="current"><see cref="IRIS.Domain.Seedwork.IRepository{TValueObject}"/></param>
        public virtual void Merge(TView persisted, TView current)
        {
            _UnitOfWork.ApplyCurrentValues(persisted, current);
        }

        #endregion

        #region IDisposable Members

        /// <summary>
        /// <see cref="M:System.IDisposable.Dispose"/>
        /// </summary>
        public void Dispose()
        {
            if (_UnitOfWork != null)
                _UnitOfWork.Dispose();
        }

        #endregion

        #region Private Methods

        public DbSet<TView> GetSet()
        {
            return _UnitOfWork.CreateSet<TView>();
        }
        #endregion

    }
}
