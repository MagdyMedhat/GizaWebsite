
App.controller('LayoutController', ["Utility", "api", "$scope", "$rootScope", "$window",
    function (Utility, api, $scope, $rootScope, $window) {

        if (sessionStorage.isLogged == "null" || sessionStorage.isLogged == undefined)
            sessionStorage.isLogged = false;
        if (sessionStorage.isAdmin == "null" || sessionStorage.isAdmin == undefined)
            sessionStorage.isAdmin = false;

        $scope.model = {};
        $scope.model.isLogged = sessionStorage.isLogged;
        $scope.model.isAdmin = sessionStorage.isAdmin;

        $scope.LogoClick = function () {
            $window.location.href = "/home";
        }

        $scope.LoginClick = function () {
            $window.location.href = "/login";
        }
        $scope.LogoutClick = function () {
            sessionStorage.isLogged = false;
            sessionStorage.isAdmin = false;
            $window.location.href = "/login";
        }
        $scope.HomePageClick = function () {
            $window.location.href = "/dashboard/homepage";
        }
        $scope.TagsClick = function () {
            $window.location.href = "/dashboard";
        }
        $scope.EmployeeClick = function () {
            $window.location.href = '/employee/index';
        }
        $scope.StatsClick = function () {
            $window.location.href = '/stats';
        }
        $scope.CorrespondenceINClick = function () {
            $window.location.href = '/correspondenceIN';

        }
        $scope.CorrespondenceOUTClick = function () {
            $window.location.href = '/correspondenceOUT';

        }
    }]);