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

namespace Domain.Seedwork1.Specification
{
    using System;
    using System.Linq.Expressions;


    /// <summary>
    /// Represent a Expression Specification
    /// <remarks>
    /// Specification overload operators for create AND,OR or NOT specifications.
    /// Additionally overload AND and OR operators with the same sense of ( binary And and binary Or ).
    /// C# couldn’t overload the AND and OR operators directly since the framework doesn’t allow such craziness. But
    /// with overloading false and true operators this is posible. For explain this behavior please read
    /// http://msdn.microsoft.com/en-us/library/aa691312(VS.71).aspx
    /// </remarks>
    /// </summary>
    /// <typeparam name="TValueObject">Type of item in the criteria</typeparam>
    public abstract class Specification<TEntity>
         : ISpecification<TEntity>
         where TEntity : class
    {
        #region ISpecification<TEntity> Members

        /// <summary>
        /// IsSatisFied Specification pattern method,
        /// </summary>
        /// <returns>Expression that satisfy this specification</returns>
        public abstract Expression<Func<TEntity, bool>> SatisfiedBy();

        #endregion

        #region Override Operators

        /// <summary>
        ///  And operator
        /// </summary>
        /// <param name="leftSideSpecification">left operand in this AND operation</param>
        /// <param name="rightSideSpecification">right operand in this AND operation</param>
        /// <returns>New specification</returns>
        public static Specification<TEntity> operator &(Specification<TEntity> leftSideSpecification, Specification<TEntity> rightSideSpecification)
        {
            return new AndSpecification<TEntity>(leftSideSpecification, rightSideSpecification);
        }

        /// <summary>
        /// Or operator
        /// </summary>
        /// <param name="leftSideSpecification">left operand in this OR operation</param>
        /// <param name="rightSideSpecification">left operand in this OR operation</param>
        /// <returns>New specification </returns>
        public static Specification<TEntity> operator |(Specification<TEntity> leftSideSpecification, Specification<TEntity> rightSideSpecification)
        {
            return new OrSpecification<TEntity>(leftSideSpecification, rightSideSpecification);
        }

        /// <summary>
        /// Not specification
        /// </summary>
        /// <param name="specification">Specification to negate</param>
        /// <returns>New specification</returns>
        public static Specification<TEntity> operator !(Specification<TEntity> specification)
        {
            return new NotSpecification<TEntity>(specification);
        }

        /// <summary>
        /// Override operator false, only for support AND OR operators
        /// </summary>
        /// <param name="specification">Specification instance</param>
        /// <returns>See False operator in C#</returns>
        public static bool operator false(Specification<TEntity> specification)
        {
            return false;
        }

        /// <summary>
        /// Override operator True, only for support AND OR operators
        /// </summary>
        /// <param name="specification">Specification instance</param>
        /// <returns>See True operator in C#</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "specification")]
        public static bool operator true(Specification<TEntity> specification)
        {
            return false;
        }

        #endregion

        private Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> _orderBy;

        public virtual IQueryable<TEntity> Apply(IQueryable<TEntity> query)
        {
            if (_orderBy != null)
            {
                query = _orderBy(query);
            }

            return query;
        }

        public Specification<TEntity> OrderBy(Expression<Func<TEntity, object>> orderExpression)
        {
            if (orderExpression != null)
            {
                _orderBy = query => query.OrderBy(orderExpression);
            }

            return this;
        }
    }
}

