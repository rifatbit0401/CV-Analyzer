/**
 * Created by Anik 0422 on 10/25/15.
 */
app.service('authService', ['$window', 'remote', function($window, remote){

    this.signIn = function(loginModel){
        return remote.signIn(loginModel);
    };

    this.signUp = function(registerModel){
        return remote.signUp(registerModel);
    };

    this.logOut = function(){
        return remote.logOut(this.getUserId());
    };

    this.setToken = function(tokenValue){
        $window.localStorage.authToken = tokenValue;
    };
    this.getToken = function(){
        return $window.localStorage.authToken;
    };

    this.setUserId = function(userId){
        $window.localStorage.userId = userId;
    };
    this.getUserId = function(){
        return $window.localStorage.userId;
    };

    this.setUser = function(loggedInUser){
        $window.localStorage.userId = loggedInUser.UserId;
        $window.localStorage.authToken = loggedInUser.TokenValue;
    };
    this.getUser = function(){
        var user = {};
        user.userId = $window.localStorage.userId;
        user.tokenValue = $window.localStorage.authToken;
        return user;
    };

    this.deleteUser = function (){
        $window.localStorage.clear();
    };

    this.isUserLoggedIn = function(){
        if($window.localStorage.authToken)
            return true;
        else
            return false;
    };

    this.isValidUser = function(loggedInUserId, ownerId){
        if(loggedInUserId == ownerId)
            return true;
        else
            return false;
    };
}]);