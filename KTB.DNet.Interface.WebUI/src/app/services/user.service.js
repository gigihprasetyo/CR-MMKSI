(function () {
    'use strict';

    angular.module('DNet.services.user', [])
        .service('UserService', UserService);

    UserService.$inject = ['Endpoint', 'BaseHTTPService', 'RequestHandler', 'Upload'];
    function UserService(Endpoint, BaseHTTPService, RequestHandler, Upload) {
        var service = {};
        service.endpoint = Endpoint.user;
        service.getOptions = _getOptions;
        service.loadUserPermissionOptions = _loadUserPermissionOptions;

        service.updateUserInfo = _updateUserInfo;
        service.updateUserClient = _updateUserClient;
        service.updateUserRole = _updateUserRole;
        service.updateUserPermission = _updateUserPermission;

        service.getUserProfile = _getUserProfile;
        service.getUploadTemplate = _getUploadTemplate;
        service.uploadUser = _uploadUser;

        service.GetUnassignedUsers = _getUnassignedUsers;
        service.GetUsersByClientId = _getUsersByClientId;
        service = angular.extend(angular.copy(BaseHTTPService), service);

        return service;


        /* public methods */
        function _getOptions() {
            var url = this.endpoint.getOptions;
            return RequestHandler.get(url);
        }

        function _loadUserPermissionOptions(data) {
            var url = this.endpoint.loadUserPermissionOptions;
            return RequestHandler.post(url, data);
        }

        function _updateUserInfo(user) {
            var url = this.endpoint.updateUserInfo;
            return RequestHandler.post(url, user);
        }

        function _updateUserClient(data) {
            var url = this.endpoint.updateUserClient;
            return RequestHandler.post(url, data);
        }

        function _updateUserRole(data) {
            var url = this.endpoint.updateUserRole;
            return RequestHandler.post(url, data);
        }

        function _updateUserPermission(data) {
            var url = this.endpoint.updateUserPermission;
            return RequestHandler.post(url, data);
        }

        function _getUserProfile() {
            var url = this.endpoint.getUserProfile;
            return RequestHandler.get(url);
        }

        function _getUploadTemplate() {
            var url = this.endpoint.getUploadTemplate;
            return RequestHandler.getFile(url, 'UserTemplate.xlsx');
        }

        function _getUnassignedUsers(clientId) {
            var url = this.endpoint.getUnassignedUsers + "?ClientId=" + clientId;
            return RequestHandler.get(url);
        }

        function _getUsersByClientId(clientId) {
            var url = this.endpoint.getUsersByClientId + "?ClientId=" + clientId;
            return RequestHandler.get(url);
        }

        function _uploadUser(files) {
            var url = this.endpoint.upload;
            return Upload.upload({
                url: url,
                data: {
                    files: files
                }
            });
        }
    }

})();
