(function () {
    'use strict';

    angular.module('DNet.endpoints', [])
        .service('Endpoint', Endpoint);

    Endpoint.$inject = ['config'];
    function Endpoint(config) {

        var root = config.rootUrl;

        var Auth = {
            login: "api/login",
            token: "api/token",
            userPermission: "api/user/GetUserPermissions"
        };

        var _apiClient = "api/Client/";
        var Client = {
            getById: _apiClient + "Get",
            getAll: _apiClient + "GetAll",
            getByAppId: _apiClient + "GetByAppId",
            getListById: _apiClient + "GetListById",
            create: _apiClient + "Create",
            update: _apiClient + "Update",
            delete: _apiClient + "Delete",
            search: _apiClient + "Search",
            getOptions: _apiClient + "GetOptions?appId=",
            getAppOptions: _apiClient + "GetAppOptions"
        };

        var _apiClientRolePermission = "api/ClientRolePermission/";
        var ClientRolePermission = {
            getClientOptions: _apiClientRolePermission + "GetClientOptions",
            getSelectedPermissionIdsByClientRoleId: _apiClientRolePermission + "GetSelectedPermissionIdsByClientRoleId?clientRoleId=",
            getRolesByClientId: _apiClientRolePermission + "GetRolesByClientId?clientId=",
            getAllPermissionsByClientId: _apiClientRolePermission + "GetAllPermissionsByClientId?clientId=",
            updateClientRole: _apiClientRolePermission + "UpdateClientRole"
        };

        var _apiClientUser = "api/ClientUser/";
        var ClientUser = {
            saveClientUser: _apiClientUser + "SaveClientUser"
        }

        var Deployment = {
            getJenkinsJobs: "api/Deployment/GetJenkinsJobs",
            deploy: "api/Deployment/Deploy",
            output: "api/Deployment/Output",
            restartJenkins: "api/Deployment/RestartJenkins"
        };

        var Dashboard = {
            getInfoBoxes: "api/Dashboard/GetInfoBoxes",
            getRankedAPIList: "api/Dashboard/GetTopRankedApi",
            getLatestTransactionList: "api/Dashboard/GetLatestTransactionList",
            getLatestErrorList: "api/Dashboard/GetLatestErrorList",
            getFailedTransactionSummaryPerDealer: "api/Dashboard/GetFailedTransactionSummaryPerDealer",
            getTransactionSummary: "api/Dashboard/GetTransactionSummary",
            getTransactionSummaryPerDealer: "api/Dashboard/GetTransactionSummaryPerDealer"
        };

        var _apiEndpointPermission = "api/EndpointPermission/";
        var EndpointPermission = {
            getById: _apiEndpointPermission + "Get",
            getAll: _apiEndpointPermission + "GetAll",
            getAllPermission: _apiEndpointPermission + "GetAllPermission",
            getByAppId: _apiEndpointPermission + "GetByAppId",
            create: _apiEndpointPermission + "Create",
            update: _apiEndpointPermission + "Update",
            delete: _apiEndpointPermission + "Delete",
            search: _apiEndpointPermission + "Search",
            getOptions: _apiEndpointPermission + "GetOptions",
            getAllPermissionsByClientId: _apiEndpointPermission + "GetAllPermissionsByClientId?clientId=",
            getEndpointGroupOptions: _apiEndpointPermission + "GetEndpointGroupOptions",
            saveEndpointPermissionGroup: _apiEndpointPermission + "SaveEndpointPermissionGroup",
            getEndpointsByEndpointGroup: _apiEndpointPermission + "GetEndpointsByEndpointGroup?id=",
            getEndpointsByEndpointType: _apiEndpointPermission + "GetEndpointsByEndpointType?id=",
            getEndpointsByOperationType: _apiEndpointPermission + "GetEndpointsByOperationType?id=",
            getUnselectedEndpointsByEndpointGroup: _apiEndpointPermission + "GetUnselectedEndpointsByEndpointGroup?id=",
            saveEndpointType: _apiEndpointPermission + "SaveEndpointType",
            saveOperationType: _apiEndpointPermission + "SaveOperationType"
        };

        var _apiEndpointSchedule = "api/EndpointSchedule/";
        var EndpointSchedule = {
            create: _apiEndpointSchedule + "Create",
            update: _apiEndpointSchedule + "Update",
            delete: _apiEndpointSchedule + "Delete",
            search: _apiEndpointSchedule + "Search?endpointId=",
            searchScheduled: _apiEndpointSchedule + "SearchScheduled",
            getUnassignedSchedule: _apiEndpointSchedule + "GetUnassignedSchedule?endpointId=",
            addBulkEndpointSchedule: _apiEndpointSchedule + "AddBulkEndpointSchedule",
            getOptions: _apiEndpointSchedule + "GetOptions"
        };

        var _apiErrorLog = "api/ErrorLog/";
        var ErrorLog = {
            getById: _apiErrorLog + "GetAsync",
            search: _apiErrorLog + "Search",
            getErrorDetail: _apiErrorLog + "GetErrorDetailAsync",
            getErrorLogSummaryPerApplication: _apiErrorLog + "GetErrorLogSummaryPerApplication",
            getLatestErrorLog: _apiErrorLog + "GetLatestErrorLog",
            getErrorLogMainInfo: _apiErrorLog + "GetErrorLogMainInfo",
            getApplicationLogList: _apiErrorLog + "GetApplicationLogList",
            delete: _apiErrorLog + "Delete"
        };

        var _apiRole = "api/Role/";
        var Role = {
            getById: _apiRole + "Get",
            getAll: _apiRole + "GetAll",
            getByAppId: _apiRole + "GetByAppId",
            create: _apiRole + "Create",
            update: _apiRole + "Update",
            delete: _apiRole + "Delete",
            search: _apiRole + "Search",
            getOptions: _apiRole + "GetOptions",
            getAppOptions: _apiRole + "GetAppOptions"
        };

        var Rollback = {
            getRollbackData: "api/Rollback/GetRollbackData",
            run: "api/Rollback/Run",
        };

        var Schedule = {
            getById: "api/Schedule/Get",
            create: "api/Schedule/Create",
            update: "api/Schedule/Update",
            delete: "api/Schedule/Delete",
            search: "api/Schedule/Search",
            getOptions: "api/Schedule/GetOptions"

        };

        var _apiThreadLog = "api/ThreadLog/";
        var ThreadLog = {
            search: _apiThreadLog + "Search",
            getDetailTransactionLog: _apiThreadLog + "GetDetailTransactionLog"
        };

        var _apiThrottler = "api/Throttler/";
        var Throttler = {
            getById: _apiThrottler + "Get",
            getAll: _apiThrottler + "GetAll",
            create: _apiThrottler + "Create",
            update: _apiThrottler + "Update",
            delete: _apiThrottler + "Delete",
            search: _apiThrottler + "Search",
            export: _apiThrottler + "Export",
            getOptions: _apiThrottler + "GetOptions"
        };

        var Transaction = {
            getById: "api/Transaction/Get",
            search: "api/Transaction/Search",
            resend: "api/Transaction/Resend",
            searchError: "api/Transaction/SearchError",
            getApplicationList: "api/Transaction/GetApplicationList",
            getClientList: "api/Transaction/GetClientList",
            getDealerList: "api/Transaction/GetDealerList",
            getTopApiList: "api/Transaction/GetTopRankedApi",
            searchByParam: "api/Transaction/SearchByParam",
            searchErrorByParam: "api/Transaction/SearchErrorByParam",
            delete:"api/Transaction/Delete"
        };

        var UserActivity = {
            search: "api/UserActivity/Search",
            getOptions: "api/UserActivity/GetOptions"
        };

        var _apiUser = "api/User/";
        var User = {
            getById: _apiUser + "Get",
            getAll: _apiUser + "GetAll",
            getByAppId: _apiUser + "GetByAppId",
            create: _apiUser + "CreateWithRepositories",
            updateUserInfo: _apiUser + "UpdateUserInfo",
            updateUserClient: _apiUser + "UpdateUserClient",
            updateUserRole: _apiUser + "UpdateUserRole",
            updateUserPermission: _apiUser + "UpdateUserPermission",
            delete: _apiUser + "Delete",
            search: _apiUser + "Search",
            getOptions: _apiUser + "GetOptions",
            loadUserPermissionOptions: _apiUser + "LoadUserPermissionOptions",
            getUserProfile: _apiUser + "GetUserProfile",
            getUploadTemplate: _apiUser + "GetUploadTemplate",
            upload: _apiUser + "Upload",
            getUnassignedUsers: _apiUser + "GetUnassignedUsers",
            getUsersByClientId: _apiUser + "GetUsersByClientId"
        };

        var _apiApplication = "api/MsApplication/";
        var Application = {
            getById: _apiApplication + "Get",
            create: _apiApplication + "Create",
            update: _apiApplication + "Update",
            delete: _apiApplication + "Delete",
            search: _apiApplication + "Search",
            getPermissions: _apiApplication + "GetPermissionOptions",
            getJenkinsJobOptions: _apiApplication + "GetJenkinsJobOptions"
        };

        var _apiSystemConfig = "api/ApplicationConfig/";
        var SystemConfig = {
            getById: _apiSystemConfig + "Get",
            getAll: _apiSystemConfig + "GetAll",
            getByAppId: _apiSystemConfig + "GetByAppId",
            create: _apiSystemConfig + "Create",
            update: _apiSystemConfig + "Update",
            delete: _apiSystemConfig + "Delete",
            search: _apiSystemConfig + "Search",
            getOptions: _apiSystemConfig + "GetOptions"
        };

        var _apiAppVersion = "api/MsAppVersion/";
        var AppVersion = {
            getById: _apiAppVersion + "Get",
            create: _apiAppVersion + "Create",
            update: _apiAppVersion + "Update",
            delete: _apiAppVersion + "Delete",
            search: _apiAppVersion + "Search",
            getOptions: _apiAppVersion + "GetOptions"
        };

        var _apiStandardCode = "api/StandardCode/";
        var StandardCode = {
            getById: _apiStandardCode + "Get",
            create: _apiStandardCode + "Create",
            update: _apiStandardCode + "Update",
            delete: _apiStandardCode + "Delete",
            search: _apiStandardCode + "Search"
        };

        var _apiStandardCodeChar = "api/StandardCodeChar/";
        var StandardCodeChar = {
            getById: _apiStandardCodeChar + "Get",
            create: _apiStandardCodeChar + "Create",
            update: _apiStandardCodeChar + "Update",
            delete: _apiStandardCodeChar + "Delete",
            search: _apiStandardCodeChar + "Search"
        };

        var service = {
            auth: generateUri(Auth),
            client: generateUri(Client),
            clientUser: generateUri(ClientUser),
            clientRolePermission: generateUri(ClientRolePermission),
            dashboard: generateUri(Dashboard),
            deployment: generateUri(Deployment),
            endpointPermission: generateUri(EndpointPermission),
            endpointSchedule: generateUri(EndpointSchedule),
            errorLog: generateUri(ErrorLog),
            role: generateUri(Role),
            rollback: generateUri(Rollback),
            schedule: generateUri(Schedule),
            threadLog: generateUri(ThreadLog),
            throttler: generateUri(Throttler),
            transaction: generateUri(Transaction),
            userActivity: generateUri(UserActivity),
            application: generateUri(Application),
            appVersion: generateUri(AppVersion),
            user: generateUri(User),
            systemConfig: generateUri(SystemConfig),
            standardCode: generateUri(StandardCode),
            standardCodeChar: generateUri(StandardCodeChar)
        }

        return service;

        function generateUri(endpoints) {
            var generated = {}
            for (var property in endpoints) {
                if (endpoints.hasOwnProperty(property)) {
                    generated[property] = root + endpoints[property]
                }
            }
            return generated
        }

    }

})();
