#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : SwaggerParameterAttribute class
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
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class SwaggerParameterAttribute : Attribute
    {
        public SwaggerParameterAttribute(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public string Name { get; private set; }

        public string Description { get; private set; }

        public string Type { get; set; }

        public bool Required { get; set; }
    }
}