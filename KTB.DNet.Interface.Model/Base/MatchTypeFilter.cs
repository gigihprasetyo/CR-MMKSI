#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : MatchTypeFilter  class
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
using KTB.DNet.Interface.Resources;
using System.ComponentModel.DataAnnotations;
#endregion

namespace KTB.DNet.Interface.Model
{
    public class MatchTypeFilter
    {
        /// <summary>
        /// Match Type 
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        public MatchType MatchType { get; set; }

        /// <summary>
        /// Property Name 
        /// </summary>
        [Required(ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        public string PropertyName { get; set; }

        /// <summary>
        /// Property Value 
        /// </summary>        
        public string PropertyValue { get; set; }

        /// <summary>
        /// SQL Operation
        /// </summary> 
        [Required(ErrorMessageResourceType = typeof(MessageResource), ErrorMessageResourceName = "ErrorMsgDataRequired")]
        public SQLOperation SqlOperation { get; set; }
    }
}
