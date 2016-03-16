'use strict';

angular.module('angular.inputmask', []).directive("inputMask", ["$timeout", function ($timeout) {
    return {
        require: 'ngModel',
        link: function (scope, elm, attrs, ngModel) {
            elm.bind("blur", function () {
                if (elm.val().indexOf('_') > -1) {
                    return;
                }
                var type = attrs.inputMask;
                switch (type) {
                    case 'telphone':
                        $timeout(function () {
                            ngModel.$setViewValue(elm.val().replace(/-/g, ''));
                        });
                        break;
                    default:

                }
            });

        }
    }
}]);
