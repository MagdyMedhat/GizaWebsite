
App.controller('LoginController', ["Utility", "api", "$scope", "$rootScope", "$window",
    function (Utility, api, $scope, $rootScope, $window) {

        $scope.model = {};
        $scope.model.flag = false;
        sessionStorage.isLogged = false;
        sessionStorage.isAdmin = false;
        $scope.authenticate = function () {
            if ($scope.model.username == "admin" && $scope.model.password == "admin") {
                sessionStorage.isLogged = true;
                sessionStorage.isAdmin = true;
                $window.location.href = "/Home/Index";
            }
            else if
                ($scope.model.username == "user" && $scope.model.password == "user") {
                sessionStorage.isLogged = true;
                sessionStorage.isAdmin = false;
                $window.location.href = "/Home/Index";
            }
                else
                $scope.model.flag = true;

        };
        $scope.logout = function () {
            sessionStorage.isLogged = false;
            $scope.$apply(function () {
                $scope.model.isAdmin = false;
            });
        }
       
       
    }]);
