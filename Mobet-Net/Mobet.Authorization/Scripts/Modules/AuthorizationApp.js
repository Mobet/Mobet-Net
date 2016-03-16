(function () {
    'use strict';

    angular.module('AuthorizationApp', ['ui.router'
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

    }).config(function ($stateProvider, $urlRouterProvider, cfpLoadingBarProvider) {


        cfpLoadingBarProvider.includeSpinner = false; //是否显示spinner
    });
   // angular.bootstrap(document, ['AuthorizationApp']);

})();
