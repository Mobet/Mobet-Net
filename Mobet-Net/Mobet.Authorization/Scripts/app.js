(function () {
    'use strict';

    angular.module('app', ['ui.router'
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

        //系统配置
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
        //$provide.decorator("$exceptionHandler", function ($delegate) {
        //    return function (exception, cause, loadingBar, toaster) {
        //        console.log(exception)
        //        toaster.pop("error", "系统繁忙，请稍候再试");
        //        loadingBar.complete();
        //        $delegate(exception, cause);
        //    };
        //});  
    });
})();
