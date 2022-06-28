#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : AggregateFilter  class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 19/10/2018 3:32
//
// ===========================================================================	
#endregion

#region Namespace Imports
using KTB.DNet.Domain.Search;
#endregion

namespace KTB.DNet.Interface.Model
{
    /// <summary>
    /// Aggregate Filter Class
    /// </summary>
    public class AggregateFilter
    {
        /// <summary>
        /// Aggregate Type
        /// </summary>
        public AggregateType AggregateType { get; set; }

        /// <summary>
        /// Property\Column Name 
        /// </summary>
        public string PropertyName { get; set; }

        /// <summary>
        /// Property Value 
        /// </summary>
        public string PropertyValue { get; set; }

        /// <summary>
        /// SQL Operation
        /// </summary>
        public SQLOperation SqlOperation { get; set; }
    }
}
