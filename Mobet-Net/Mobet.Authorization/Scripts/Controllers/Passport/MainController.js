/**
 *  MainController
 *
 *  摘要：布局页面控制
 *
 */

(function () {
    angular.module('passport').controller('MainController', function ($scope) {

        $scope._init = function () {
            $('.n-main-nav').find('li').click(function () {
                $('.n-main-nav').find('li').removeClass('current');
                $(this).addClass('current');
            });
        };
        $scope._init();
    });
})();


