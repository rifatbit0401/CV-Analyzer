/**
 * Created by Anik 0422 on 10/25/15.
 */

function Remote($http, serverUrl) {


    this.signIn = function (loginModel) {
        var url = serverUrl + 'account/signin';
        return $http.post(url, loginModel);
    };


    this.signUp = function (registerModel) {
        var url = serverUrl + 'account/signup';
        return $http.post(url, registerModel);
    };

    this.logOut = function (userId) {
        var url = serverUrl + 'account/logout';
        return $http.post(url, userId);
    };
};

function DummyRemote($http, serverUrl, $window) {
    this.signIn = function (loginModel) {
        var url = serverUrl + 'cvanalyzer/signin';
        $window.localStorage.authToken = "test token";
        //return $http.post(url, loginModel);
    };

    this.signUp = function (registerModel) {
        var url = serverUrl + 'account/signup';
        console.log(registerModel);
        //return $http.post(url, registerModel);
    };

    this.logOut = function (user) {
        var url = serverUrl + 'accout/logout';
        //return $http.post(url, user);
        $window.localStorage.clear();
        return true;
    };
};

app.service('remote', ['$http', 'serverUrl', Remote]);
//app.service('remote', ['$http', 'serverUrl', '$window', DummyRemote]);