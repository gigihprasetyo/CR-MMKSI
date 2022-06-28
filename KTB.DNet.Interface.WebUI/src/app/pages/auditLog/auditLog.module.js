(function () {
    'use strict';

    angular.module('DNet.pages.auditLog', [
    ])
        .config(routeConfig);

    /** @ngInject */
    routeConfig.$inject = ['$stateProvider'];
    function routeConfig($stateProvider) {
        $stateProvider
            .state('auditLog', {
                url: '/auditLog',
                permissionCodes: ["WebUI_ErrorLog_Read", "WebUI_TransactionLog_Read",
                    "WebUI_FailedTransactionLog_Read", "WebUI_TransactionRuntime_Read",
                    "WebUI_Activity_Read"],
                template: '<div ui-view autoscroll="true" autoscroll-body-top></div>',
                abstract: true,
                module: true,
                title: 'Audit Log',
                sidebarMeta: {
                    icon: 'ion-android-clipboard',
                    order: 20,
                }
            })

            .state('auditLog.elmahlog', {
                url: '/elmahErrorLog',
                permissionCodes: ["WebUI_ErrorLog_Read"],
                abstract: true,
                template: '<div ui-view autoscroll="true" autoscroll-body-top></div>',
                title: 'System Errors',
                controller: 'elmahCtrl',
                sidebarMeta: {
                    order: 203,
                }
            })

            .state('auditLog.elmahlog.dashboard', {
                url: '',
                templateUrl: 'src/app/pages/auditLog/elmahErrorLogs/elmah.html',
                title: 'System Error Logs Dashboard',
                controller: 'elmahCtrl',
                breadchrumb: {
                    text: 'Dashboard'
                }
            })

            .state('auditLog.elmahlog.list', {
                url: '/list/:app',
                templateUrl: 'src/app/pages/auditLog/elmahErrorLogs/errorLogList/errorLogList.html',
                title: 'System Error Logs',
                controller: 'errorLogListCtrl',
                breadchrumb: {
                    text: 'List'
                }
            })
            .state('auditLog.elmahlog.view', {
                url: '/detail/:id?app',
                templateUrl: 'src/app/pages/auditLog/elmahErrorLogs/errorLogDetail/errorLogDetail.Html',
                title: 'Error Log Detail',
                controller: 'errorLogDetailCtrl',
                breadchrumb: {
                    text: 'Detail'
                }
            })

            .state('auditLog.transactionRuntime', {
                url: '/transactionRuntime',
                permissionCodes: ["WebUI_TransactionRuntime_Read"],
                abstract: true,
                template: '<div ui-view autoscroll="true" autoscroll-body-top></div>',
                title: 'Transaction Runtime',
                controller: 'threadLogCtrl',
                sidebarMeta: {
                    order: 203,
                }
            })

            .state('auditLog.transactionRuntime.list', {
                url: '',
                templateUrl: 'src/app/pages/auditLog/threadLog/threadLog.html',
                title: 'Transaction Runtimes',
                controller: 'threadLogCtrl',
                breadchrumb: {
                    text: 'List'
                }
            })

            .state('auditLog.transactionRuntime.view', {
                url: '/detail/:id',
                templateUrl: 'src/app/pages/auditLog/allTransactionLog/transactionLogDetail.html',
                title: 'Transaction Runtime Log Detail',
                controller: 'transactionLogDetailCtrl',
                breadchrumb: {
                    text: 'Detail'
                }
            })

            .state('auditLog.transactionLog', {
                url: '/transaction',
                permissionCodes: ["WebUI_TransactionLog_Read"],
                abstract: true,
                template: '<div ui-view autoscroll="true" autoscroll-body-top></div>',
                title: 'All Transactions',
                controller: 'allTransactionLogCtrl',
                sidebarMeta: {
                    order: 201,
                }
            })

            .state('auditLog.transactionLog.list', {
                url: '',
                templateUrl: 'src/app/pages/auditLog/allTransactionLog/allTransactionLog.html',
                title: 'All Transaction Logs',
                controller: 'allTransactionLogCtrl',
                breadchrumb: {
                    text: 'List'
                }
            })

            .state('auditLog.transactionLog.view', {
                url: '/detail/:id',
                templateUrl: 'src/app/pages/auditLog/allTransactionLog/transactionLogDetail.html',
                title: 'Transaction Log Detail',
                controller: 'transactionLogDetailCtrl',
                breadchrumb: {
                    text: 'Detail'
                }
            })

            .state('auditLog.transactionLog.resolve', {
                url: '/resolve/:id',
                templateUrl: 'src/app/pages/auditLog/allTransactionLog/transactionLogResolve.html',
                title: 'Transaction Log Resolve',
                controller: 'transactionLogDetailCtrl',
                breadchrumb: {
                    text: 'Resolve'
                }
            })

           
            .state('auditLog.failedTransactionLog', {
                url: '/failedTransaction',
                permissionCodes: ["WebUI_FailedTransactionLog_Read"],
                abstract: true,
                template: '<div ui-view autoscroll="true" autoscroll-body-top></div>',
                title: 'Failed Transactions',
                controller: 'allTransactionLogCtrl',
                sidebarMeta: {
                    order: 202,
                }
            })

            .state('auditLog.failedTransactionLog.list', {
                url: '',
                templateUrl: 'src/app/pages/auditLog/allTransactionLog/failedTransactionLog.html',
                title: 'Failed Transaction Logs',
                controller: 'failedTransactionLogCtrl',
                breadchrumb: {
                    text: 'List'
                }
            })

            .state('auditLog.failedTransactionLog.view', {
                url: '/detail/:id',
                templateUrl: 'src/app/pages/auditLog/allTransactionLog/transactionLogDetail.html',
                title: 'Failed Transaction Log Detail',
                controller: 'transactionLogDetailCtrl',
                breadchrumb: {
                    text: 'Detail'
                }
            })

            .state('auditLog.failedTransactionLog.resolve', {
                url: '/resolve/:id',
                templateUrl: 'src/app/pages/auditLog/allTransactionLog/transactionLogResolve.html',
                title: 'Resolve Failed Transaction',
                controller: 'transactionLogDetailCtrl',
                breadchrumb: {
                    text: 'Resolve'
                }
            })

            .state('auditLog.userActivity', {
                url: '/userActivity',
                permissionCodes: ["WebUI_Activity_Read"],
                abstract: true,
                template: '<div ui-view autoscroll="true" autoscroll-body-top></div>',
                title: 'User Activity',
                controller: 'userActivityCtrl',
                sidebarMeta: {
                    order: 204,
                }
            })

            .state('auditLog.userActivity.list', {
                url: '',
                templateUrl: 'src/app/pages/auditLog/userActivity/userActivity.html',
                title: 'User Activities',
                controller: 'userActivityCtrl',
                breadchrumb: {
                    text: 'List'
                }
            })

            .state('auditLog.topApi', {
                url: '/topApi',
                permissionCodes: ["WebUI_TopApiList_Sidebar"],
                abstract: true,
                template: '<div ui-view autoscroll="true" autoscroll-body-top></div>',
                title: 'Top API',
                controller: 'allRankedApiListCtrl',
                sidebarMeta: {
                    order: 205,
                }
            })
             .state('auditLog.cleanLog', {
                 url: '/cleanLog',
                 permissionCodes: ["WebUI_Log_Delete"],
                 abstract: true,
                 template: '<div ui-view autoscroll="true" autoscroll-body-top></div>',
                 title: 'Clean Log',
                 controller: 'cleanLogCtrl',
                 sidebarMeta: {
                     order: 206,
                 }
             })

            .state('auditLog.cleanLog.clean', {
                url: '',
                templateUrl: 'src/app/pages/auditLog/cleanLog/cleanLog.html',
                title: 'Clean Log',
                controller: 'cleanLogCtrl',
                breadchrumb: {
                    text: 'Clean'
                }
            })

            .state('auditLog.topApi.list', {
                url: '',
                templateUrl: 'src/app/pages/auditLog/topApi/allRankedApiList.html',
                title: 'Top Api List',
                controller: 'allRankedApiListCtrl',
                breadchrumb: {
                    text: 'List'
                }
            })

            
    }
})();