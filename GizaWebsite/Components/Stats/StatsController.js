App.controller('StatsController', ["Utility", "api", "$scope", "$rootScope", "$window", "$timeout","$sce",
function (Utility, api, $scope, $rootScope, $window, $timeout,$sce) {

        if (sessionStorage.isLogged == "false")
            $window.location.href = "/home/noaccess";
        $scope.html_content = "";

        $scope.stats = {};
        $scope.getStats = function () {
            $("#loading").css("display", "block");
            api.Get("/Stats/getStats").success(function (Response) {
                $scope.stats = Response;
                $("#loading").css("display", "none");
            }).error(function (error) { console.log(error); })
        }
        $scope.getStats();
       
    }]);
