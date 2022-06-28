#region Summary
// ===========================================================================
// AUTHOR        : PT Mitrais 
// PURPOSE       : StructureMapFilterProvider.cs class
// SPECIAL NOTES : DNet WebApi Project
// ---------------------
// Copyright  (c) 2018 
// ---------------------
// $History      : $
// Created on 28/11/2018 17:43
//
// ===========================================================================	
#endregion

using StructureMap;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace KTB.DNet.Interface.WebUI.DependencyResolution
{
    public class StructureMapFilterProvider : ActionDescriptorFilterProvider, IFilterProvider
    {
        public StructureMapFilterProvider(IContainer container)
        {
            _container = container;
        }

        private IContainer _container;

        public IEnumerable<FilterInfo> GetFilters(HttpConfiguration configuration, HttpActionDescriptor actionDescriptor)
        {
            var filters = base.GetFilters(configuration, actionDescriptor);
            foreach (var filter in filters)
            {
                _container.BuildUp(filter.Instance);
            }
            return filters;
        }
    }
}