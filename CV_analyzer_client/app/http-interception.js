app.config(['$httpProvider', function ($httpProvider) {
    $httpProvider.interceptors.push(['$window', function ($window) {
        return {
            request: function (request) {
                request.headers.authToken = $window.localStorage.authToken;
                return request;
            }
        }
    }]);
}]);
