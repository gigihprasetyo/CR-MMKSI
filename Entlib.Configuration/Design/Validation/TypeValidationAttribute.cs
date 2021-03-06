//===============================================================================
// Microsoft patterns & practices Enterprise Library
// Configuration Application Block
//===============================================================================
// Copyright ? Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
//===============================================================================

using System;
using System.Reflection;
using System.Security.Permissions;
using Microsoft.Practices.EnterpriseLibrary.Common;

namespace Microsoft.Practices.EnterpriseLibrary.Configuration.Design.Validation
{
    /// <summary>
    /// <para>Specifies a type name should be validated to ensure that it is a valid <see cref="Type"/>.</para>
    /// </summary>
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, Inherited=true)]
	public sealed class TypeValidationAttribute : RequiredAttribute
	{
        /// <summary>
        /// <para>Initialize a new instance of the <see cref="TypeValidationAttribute"/> class.</para>
        /// </summary>
		public TypeValidationAttribute()
		{
		}

        /// <summary>
        /// <para>Validate the ranige data for the given <paramref name="instance"/> and the <paramref name="propertyInfo"/>.</para>
        /// </summary>
        /// <param name="instance">
        /// <para>The instance to validate.</para>
        /// </param>
        /// <param name="propertyInfo">
        /// <para>The property contaning the value to validate.</para>
        /// </param>
        /// <param name="errors">
        /// <para>The collection to add any errors that occur durring the validation.</para>
        /// </param>
        [ReflectionPermission(SecurityAction.Demand,  Flags=ReflectionPermissionFlag.MemberAccess)]
        public override void Validate(object instance, PropertyInfo propertyInfo, ValidationErrorCollection errors)
        {
            ArgumentValidation.CheckForNullReference(instance, "instance");
            ArgumentValidation.CheckForNullReference(propertyInfo, "propertyInfo");
            ArgumentValidation.CheckForNullReference(errors, "errors");

            base.Validate(instance, propertyInfo, errors);
            object propertyValue = propertyInfo.GetValue(instance, null);
            string typeName = propertyValue as string;
            // the required attribute will catch this for us
            if (typeName == null || typeName.Length == 0) return;
            Type t = Type.GetType(typeName, false, true);
            if (t == null)
            {
                string name = propertyInfo.Name;
                errors.Add(instance, name, SR.ExceptionTypeNotValid(typeName));
            }
        }
	}
}
