App.controller('HomeController', ["Utility", "api", "$scope", "$rootScope", "$window", "$timeout","$sce",
function (Utility, api, $scope, $rootScope, $window, $timeout,$sce) {

        if (sessionStorage.isLogged == "false")
            $window.location.href = "/home/noaccess";
        $scope.html_content="";
        $scope.getHomePageContent = function () {
            $("#loading").css("display", "block");
            api.Get("/Home/getHomePageContent").success(function (Response) {
                var data = decodeURIComponent(Response);
                var elem = document.createElement('textarea');
                elem.innerHTML = data;
                var decoded = elem.value;
                $('#HomePageContent').append(decoded);
                $("#loading").css("display", "none");
            }).error(function (error) { console.log(error); })
        }
        $scope.getHomePageContent();
       
    }]);
