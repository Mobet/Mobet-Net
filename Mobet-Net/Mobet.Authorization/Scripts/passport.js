(function () {
    'use strict';

    angular.module('passport', ['ui.router'
        , 'angular.radio'
        , 'angular.icheck'
        , 'angular.datatables'
        , 'angular.autovalidate'
        , 'angular.inputmask'
        , 'angular.laydate'
        , 'angular.chosen'
        , 'angular.loadingbar'
        , 'angular.toaster'
        , 'angular.switchery'
        , 'angular.dialogs'
        , 'ui.bootstrap'
    ]).run(function ($rootScope, $state, $stateParams) {

        $rootScope.$state = $state;
        $rootScope.$stateParams = $stateParams;

    }).config(function ($stateProvider, $urlRouterProvider, loadingBarProvider, $provide, $httpProvider) {

        //账号安全
        $stateProvider.state('Security', {
            url: '/Security',
            controller: 'SecurityController',
            templateUrl: '/Passport/Security'
        });
        //绑定授权
        $stateProvider.state('Permissions', {
            url: '/Permissions',
            controller: 'PermissionsController',
            templateUrl: 'https://localhost:44373/core/permissions'
        });
        //绑定授权
        $stateProvider.state('Profile', {
            url: '/Profile',
            controller: 'ProfileController',
            templateUrl: '/Passport/Profile'
        });
        //服务
        $stateProvider.state('GlobalSettings', {
            url: '/GlobalSettings',
            controller: 'GlobalSettingsController',
            templateUrl: '/GlobalSettings/Index'
        });


        loadingBarProvider.includeSpinner = false;

        $httpProvider.interceptors.push(['$q', function ($q) {
            return {
                'responseError': function (error) {
                    console.log(error);
                    return $q.reject(error);
                }
            };
        }]);
    });
})();
