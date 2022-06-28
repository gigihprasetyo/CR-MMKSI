// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DefaultRegistry.cs" company="Web Advanced">
// Copyright 2012 Web Advanced (www.webadvanced.com)
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0

// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using KTB.DNet.Interface.Domain;
using KTB.DNet.Interface.Framework;
using KTB.DNet.Interface.Repository.Dapper;
using KTB.DNet.Interface.Repository.Interface;
using StructureMap.Configuration.DSL;
using StructureMap.Graph;
using System;

namespace KTB.DNet.Interface.WebUI.DependencyResolution
{
    public class DefaultRegistry : Registry
    {
        #region Constructors and Destructors

        public DefaultRegistry()
        {
            Scan(
                scan =>
                {
                    scan.TheCallingAssembly();
                    scan.WithDefaultConventions();
                });

            string interfaceConnectionString = AppConfigs.ConnectionString(Constants.ConnectionStringName.InterfaceConnection);
            string logConnectionString = AppConfigs.ConnectionString(Constants.ConnectionStringName.LogConnection);
            string dnetConnectionString = AppConfigs.ConnectionString(Constants.ConnectionStringName.DNetConnection);

            #region Dapper Implementation
            For<IMsAppVersionRepository<MsAppVersion, int>>().Use<MsAppVersionRepository>().Ctor<string>("connectionString").Is(interfaceConnectionString);
            For<IMsApplicationRepository<MsApplication, Guid>>().Use<MsApplicationRepository>().Ctor<string>("connectionString").Is(interfaceConnectionString);
            For<IThrottleRepository<APIThrottle, int>>().Use<ThrottleRepository>().Ctor<string>("connectionString").Is(interfaceConnectionString);
            For<IEndpointPermissionRepository<APIEndpointPermission, int>>().Use<EndpointPermissionRepository>().Ctor<string>("connectionString").Is(interfaceConnectionString);
            For<IElmahErrorRepository<ELMAH_Error, Guid>>().Use<ElmahErrorRepository>().Ctor<string>("connectionString").Is(logConnectionString);
            For<IScheduleRepository<APISchedule, int>>().Use<ScheduleRepository>().Ctor<string>("connectionString").Is(interfaceConnectionString);
            For<IDealerRepository<Dealer, int>>().Use<DealerRepository>().Ctor<string>("connectionString").Is(interfaceConnectionString);
            For<IApplicationConfigRepository<ApplicationConfig, long>>().Use<ApplicationConfigRepository>().Ctor<string>("connectionString").Is(logConnectionString);
            For<IUserRepository<APIUser, int>>().Use<UserRepository>().Ctor<string>("connectionString").Is(interfaceConnectionString);
            For<IClientUserRepository<APIClientUser, int>>().Use<ClientUserRepository>().Ctor<string>("connectionString").Is(interfaceConnectionString);
            For<IUserPermissionRepository<APIUserPermission, int>>().Use<UserPermissionRepository>().Ctor<string>("connectionString").Is(interfaceConnectionString);
            For<IUserRoleRepository<APIUserRole, int>>().Use<UserRoleRepository>().Ctor<string>("connectionString").Is(interfaceConnectionString);
            For<IUserActivityRepository<UserActivity, long>>().Use<UserActivityRepository>().Ctor<string>("connectionString").Is(logConnectionString);
            For<ITransactionLogRepository<TransactionLog, long>>().Use<TransactionLogRepository>().Ctor<string>("connectionString").Is(logConnectionString);
            For<ITransactionRuntimeRepository<TransactionRuntime, long>>().Use<TransactionRuntimeRepository>().Ctor<string>("connectionString").Is(logConnectionString);
            For<IEndpointScheduleRepository<APIEndpointSchedule, int>>().Use<EndpointScheduleRepository>().Ctor<string>("connectionString").Is(interfaceConnectionString);
            For<IRolePermissionRepository<APIRolePermission, int>>().Use<RolePermissionRepository>().Ctor<string>("connectionString").Is(interfaceConnectionString);
            For<IRoleRepository<APIRole, int>>().Use<RoleRepository>().Ctor<string>("connectionString").Is(interfaceConnectionString);
            For<IClientRepository<APIClient, Guid>>().Use<ClientRepository>().Ctor<string>("connectionString").Is(interfaceConnectionString);
            For<IEndpointScheduleRepository<APIEndpointSchedule, int>>().Use<EndpointScheduleRepository>().Ctor<string>("connectionString").Is(interfaceConnectionString);
            For<IClientRoleRepository<APIClientRole, int>>().Use<ClientRoleRepository>().Ctor<string>("connectionString").Is(interfaceConnectionString);
            For<IStandardCodeRepository<StandardCode, int>>().Use<StandardCodeRepository>().Ctor<string>("connectionString").Is(dnetConnectionString);
            For<IStandardCodeRepository<StandardCodeChar, int>>().Use<StandardCodeCharRepository>().Ctor<string>("connectionString").Is(dnetConnectionString);
            #endregion

        }

        #endregion
    }
}