#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : ApiGroupAttribute class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 23/9/2018 15:13
//
// ===========================================================================	
#endregion

using System;

namespace KTB.DNet.Interface.WebApi.Helper
{
    public class ApiGroupAttribute : Attribute
    {
        /// <summary>
        /// Group name
        /// </summary>
        public string GroupName { get; private set; }

        /// <summary>
        /// ApiGroupAttribute
        /// </summary>
        /// <param name="groupName"></param>
        public ApiGroupAttribute(string groupName)
        {
            if (string.IsNullOrEmpty(groupName))
            {
                throw new ArgumentNullException("groupName");
            }
            GroupName = groupName;
        }
    }
}