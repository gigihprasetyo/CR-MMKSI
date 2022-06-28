
(function () {
    'use strict';

    angular.module('DNet.pages.accessControl')
        .controller('userFormCtrl', userFormCtrl)

    userFormCtrl.$inject = ['$scope', '$state', '$stateParams', 'UserService', 'ClientService', 'ModalHelper', 'ArrayHelper'];
    function userFormCtrl($scope, $state, $stateParams, UserService, ClientService, ModalHelper, ArrayHelper) {
        $scope.user = {};
        $scope.entity = {};

        $scope.dealerOptions = [];
        $scope.roleOptions = [];
        $scope.clientOptions = [];
        $scope.permissionOptions = [];
        $scope.selectedClientUser = {};
        $scope.selectedClientUser.Id = null;

        $scope.clientTableDefinition = _getClientTableDefinition();
        $scope.user.Clients = [];
        $scope.selectedClient = {};
        $scope.selectedClient.ClientId = null;
        $scope.addClient = _addClient;
        $scope.deleteClient = _deleteClient;

        $scope.roleTableDefinition = _getRoleTableDefinition();
        $scope.user.Roles = [];
        $scope.selectedRole = {};
        $scope.selectedRole.Id = null;
        $scope.addRole = _addRole;
        $scope.deleteRole = _deleteRole;

        $scope.permissionTableDefinition = _getPermissionTableDefinition();
        $scope.user.UserPermissions = [];
        $scope.selectedPermission = {};
        $scope.selectedPermission.Id = null;
        $scope.addPermission = _addPermission;
        $scope.deletePermission = _deletePermission;
        $scope.selectAnotherClient = _selectAnotherClient;

        $scope.getOptions = _getOptions;
        $scope.loadUserPermissionOptions = _loadUserPermissionOptions;
        $scope.isPermissionLoaded = false;

        $scope.createUser = _createUser;
        $scope.updateUserInfo = _updateUserInfo;
        $scope.updateUserClient = _updateUserClient;
        $scope.updateUserRole = _updateUserRole;
        $scope.updateUserPermission = _updateUserPermission;

        $scope.hasClientOrRoleBeenUpdated = false;

        $scope.getOptions();

        if ($stateParams.id) {
            UserService.GetById($stateParams.id)
                .then(function (response) {
                    $scope.entity.selectedClientIds = [];
                    $scope.clientTableDefinition.dataSource = response.Data.Clients;
                    $scope.roleTableDefinition.dataSource = response.Data.Roles;

                    $scope.user = response.Data;
                    $scope.user.Clients = response.Data.Clients;
                    $scope.user.Roles = response.Data.Roles;
                    debugger;
                    response.Data.Clients.forEach(function (entry) {
                        $scope.entity.selectedClientIds.push(entry.ClientId);
                    })
                    _setFilteredClient();
                    _setFilteredRole();

                }, function (error) { });
        }

        function _getOptions() {
            UserService.getOptions()
                .then(function (response) {
                    $scope.dealerOptions = response.Data.DealerOptions;
                    $scope.roleOptions = response.Data.RoleOptions;
                    $scope.clientOptions = response.Data.ClientOptions;
                    _setFilteredClient();
                    _setFilteredRole();
                }, function (error) { });
        }

        function _getSelectedOption(options, selectedValue, key, dataName) {
            if (selectedValue[key] == null || selectedValue[key] == undefined) {
                ModalHelper.showMessage("Please select the " + dataName + ".", "warning");
                return null;
            }

            var selectedOption = ArrayHelper.getItemByProperty(key, selectedValue[key], options);

            if (selectedOption) {
                return selectedOption;
            }

            ModalHelper.showMessage("Invalid " + dataName + " value.", "warning");
            return null;
        }


        function _loadUserPermissionOptions() {

            if ($scope.hasClientOrRoleBeenUpdated) {
                ModalHelper.showMessage("User's Clients or User's Roles has been modified but has not been saved yet. Please save it first.", "warning");
                return;
            }

            var selectedClientUser = _getSelectedOption($scope.user.ClientUsers, $scope.selectedClientUser, "Id", "client");

            if (selectedClientUser) {
                $scope.selectedClientUser = angular.copy(selectedClientUser);

                UserService.loadUserPermissionOptions($scope.selectedClientUser)
                    .then(function (response) {
                        $scope.permissionTableDefinition.dataSource = response.Data.UserPermissions;

                        $scope.user.UserPermissions = response.Data.UserPermissions;
                        
                        $scope.permissionOptions = response.Data.PermissionOptions;
                        _setFilteredPermission();
                        $scope.isPermissionLoaded = true;
                    }, function (error) {
                        
                    });
            }
        }

        /*
         * User Client CRUD Section 
         */

        // client table definition
        function _getClientTableDefinition() {
            return {
                name: "client",
                key: "ClientId",
                identifier: "Name",
                action: {
                    delete: { customDelete: _deleteClient }
                },
                columns: [
                    { name: "Name", label: "Client Name" },
                    { name: "Actions", label: "Actions", action: true }
                ],
                searchable: false,
                tablePageSize: 10 // rows
            };
        }

        // client options, but exclude user clients
        function _setFilteredClient() {
            $scope.filteredClientOptions = ArrayHelper.removeSubset($scope.clientOptions, $scope.user.Clients, "ClientId");
        }

        // add client
        function _addClient() {
            var client = _getSelectedOption($scope.filteredClientOptions, $scope.selectedClient, "ClientId", "client")

            if (client) {
                $scope.selectedClient.ClientId = null;
                $scope.user.Clients.unshift(client);
                $scope.clientTableDefinition.dataSource = $scope.user.Clients;
                _setFilteredClient();
                $scope.hasClientOrRoleBeenUpdated = true;
                _selectAnotherClient();
            }
        }

        // delete client
        function _deleteClient(row) {
            var id = row["ClientId"];
            ArrayHelper.removeItemByProperty("ClientId", id, $scope.user.Clients);
            $scope.clientTableDefinition.dataSource = $scope.user.Clients;
            _setFilteredClient();
            $scope.hasClientOrRoleBeenUpdated = true;
            _selectAnotherClient();
        }
        /*** End of User Client CRUD Section ***/


        /*
         * User Role CRUD Section
         */

        // role table definition
        function _getRoleTableDefinition() {
            return {
                name: "role",
                key: "Id",
                identifier: "Name",
                action: {
                    delete: { customDelete: _deleteRole }
                },
                columns: [
                    { name: "Name", label: "Role Name" },
                    { name: "Actions", label: "Actions", action: true }
                ],
                searchable: false,
                tablePageSize: 10 // rows
            };
        }

        // role options, but exclude user roles
        var _setFilteredRole = function () {
            $scope.filteredRoleOptions = ArrayHelper.removeSubset($scope.roleOptions, $scope.user.Roles, "Id");
        }

        // add role
        function _addRole() {
            var role = _getSelectedOption($scope.filteredRoleOptions, $scope.selectedRole, "Id", "role")

            if (role) {
                $scope.selectedRole.Id = null;
                $scope.user.Roles.unshift(role);
                $scope.roleTableDefinition.dataSource = $scope.user.Roles;
                _setFilteredRole();
                $scope.hasClientOrRoleBeenUpdated = true;
                _selectAnotherClient();
            }
        }

        // delete role
        function _deleteRole(row) {
            var id = row["Id"];
            ArrayHelper.removeItemByProperty("Id", id, $scope.user.Roles);
            $scope.roleTableDefinition.dataSource = $scope.user.Roles;
            _setFilteredRole();
            $scope.hasClientOrRoleBeenUpdated = true;
            _selectAnotherClient();
        }
        /*** End of User Role CRUD Section ***/


        /* 
         * User Permission CRUD Section 
         */

        // user permission table definition
        function _getPermissionTableDefinition() {
            return {
                name: "user permission",
                key: "Id",
                identifier: "Permission.Name",
                action: {
                    delete: { customDelete: _deletePermission },
                    customActions: [
                        { text: "Enable", type: "primary", isButton: true, customText: _enableOrDisableButtonText, customType: _enableOrDisableButtonType, action: _enableOrDisablePermission }
                    ]
                },
                columns: [
                    { name: "Permission.Name", label: "Permission Name" },
                    { name: "IsCustomPermission", label: "Custom Permission", datatype: "boolean" },
                    { name: "IsDismantledPermission", label: "Disabled", datatype: "boolean" },
                    { name: "Actions", label: "Actions", action: true }
                ],
                searchable: true,
                tablePageSize: 10 // rows
            };
        }

        // permission options, but exclude user permissions
        function _setFilteredPermission() {
            $scope.filteredPermissionOptions = ArrayHelper.removeSubset($scope.permissionOptions, $scope.user.UserPermissions, "Id", "PermissionId");
        }

        // add permission
        function _addPermission() {
            var permission = _getSelectedOption($scope.filteredPermissionOptions, $scope.selectedPermission, "Id", "permission")

            if (permission) {
                var userPermission = {
                    ClientUserId: $scope.selectedClientUser.Id,
                    Permission: permission,
                    PermissionId: $scope.selectedPermission.Id,
                    IsCustomPermission: true,
                    IsDismantledPermission: false
                };

                $scope.selectedPermission.Id = null;
                $scope.user.UserPermissions.unshift(userPermission);
                $scope.permissionTableDefinition.dataSource = $scope.user.UserPermissions;
                _setFilteredPermission();
            }
        }

        // delete permission
        function _deletePermission(row) {
            var id = row["PermissionId"];
            ArrayHelper.removeItemByProperty("PermissionId", id, $scope.user.UserPermissions);
            $scope.permissionTableDefinition.dataSource = $scope.user.UserPermissions;
            _setFilteredPermission();
        }

        function _enableOrDisablePermission(row) {
            row.IsDismantledPermission = !row.IsDismantledPermission;
        }

        function _enableOrDisableButtonType(row) {
            if (row.IsDismantledPermission) {
                return "success";
            }
            return "danger";
        }

        function _enableOrDisableButtonText(row) {
            if (row.IsDismantledPermission) {
                return "Enable";
            }
            return "Disable";
        }

        function _selectAnotherClient() {
            $scope.isPermissionLoaded = false;
            $scope.user.UserPermissions = [];
            $scope.selectedClientUser.Id = null;
            $scope.permissionTableDefinition.dataSource = $scope.user.UserPermissions;
        }
        /*** End of User Permission CRUD Section ***/

        function _createUser() {
            ClientService.GetListById($scope.entity.selectedClientIds)
                .then(function (response) {
                    $scope.user.Clients = response.Data;
                    UserService.Create($scope.user)
                        .then(function (response) {
                        $state.go('accessControl.user.list');
                        $scope.hasClientOrRoleBeenUpdated = false;
                    }, function (error) { });

                }, function (error) { });

            
            
        }

        function _updateUserInfo() {
            UserService.updateUserInfo($scope.user)
                .then(function (response) {
                }, function (error) { });
        }

        function _updateUserClient() {
            var data = {
                "UserId": $scope.user.Id,
                "ListOfClientId": $scope.entity.selectedClientIds
            };

            UserService.updateUserClient(data)
                .then(function (response) {
                    $scope.user.ClientUsers = response.Data;
                    $scope.hasClientOrRoleBeenUpdated = false;
                }, function (error) { });
        }

        function _updateUserRole() {
            var data = {
                "UserId": $scope.user.Id,
                "ListOfRoleId": ArrayHelper.getArrayOfProperty(
                    $scope.user.Roles, "Id"
                )
            };
            UserService.updateUserRole(data)
                .then(function (response) {
                    $scope.hasClientOrRoleBeenUpdated = false;
                }, function (error) { });
        }

        function _updateUserPermission() {
            var data = {
                "UserId": $scope.user.Id,
                "ListOfUserPermission": $scope.user.UserPermissions
            };

            UserService.updateUserPermission(data)
                .then(function (response) {
                }, function (error) { });
        }
    }

})();