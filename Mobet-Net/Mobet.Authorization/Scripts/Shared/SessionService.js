(function () {

    angular.module('app').factory('session', [
        '$http', function ($http) {
            return new function () {
                this.getCurrentLoginInformations = function (httpParams) {
                    return $http(angular.extend({
                        abp: true,
                        url: '/Account/GetCurrentIdentity',
                        method: 'POST',
                        data: JSON.stringify({})
                    }, httpParams));
                };

            };
        }
    ]);
})();