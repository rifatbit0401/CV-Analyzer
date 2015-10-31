/**
 * Created by Anik 0422 on 10/25/15.
 */
app.controller('AuthController', ['$scope', 'authService', '$state', function ($scope, authService, $state) {

    $scope.signIn = function (loginModel) {
        authService.signIn(loginModel).success(function (responseData) {
            authService.setUser(responseData);
            $state.go('home');
            console.log("Successfully logged in.")
        }).error(function () {
                console.log("Wrong email or password. Please try again.")
            });
    };
    $scope.signUp = function (registerModel) {
        if (registerModel.Password == registerModel.ConfirmPassword) {
            authService.signUp(registerModel).success(function(responseData){
                authService.setUser(responseData);
                $state.go('home');
                console.log("Successfully registered.");
            }).error(function(){
                    console.log("Registration error");
                });
        }
        else{
            console.log("Password not matched");
        }
    };
    $scope.logOut = function () {
        authService.logOut().success(function (responseData) {
            authService.deleteUser();
            $state.go('home');
        }).error(function () {
                console.log("Can't log out. Server error!!!")
            });
    };

    $scope.isLoggedIn = function () {
        return authService.isUserLoggedIn();
    };
}]);