#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : CompareTypeFilter  class
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
    /// Compare Type Filter
    /// </summary>
    public class CompareTypeFilter
    {
        /// <summary>
        /// Compare Match Type
        /// </summary>
        public CompareMatchType CompareMatchType { get; set; }

        /// <summary>
        /// Property Type \Column Name 
        /// </summary>
        public string PropertType { get; set; }

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
