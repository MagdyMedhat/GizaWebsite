App.controller('HomePageController', ["Utility", "api", "$scope", "$rootScope", "$window", "$timeout", "$sce",
    function (Utility, api, $scope, $rootScope, $window, $timeout, $sce) {

        if (sessionStorage.isAdmin == "false")
            $window.location.href = "/home/noaccess";

        $scope.ui = {};
        $scope.ui.dummy = "";
        $scope.ui.content = "";
        $scope.ui.html_data = "";
        $scope.flag = false;
        $scope.errorflag = false;
        $scope.saveContent = function () {
            $("#loading").css("display", "block");
            var data = encodeURIComponent($('.nw-editor__res').text()); 
            api.Post("/Dashboard/saveHomePageContent", null, { htmlContent:data }).success(function (Response) {
                if (Response == 1) {
                    $scope.errorflag = false;
                    $scope.flag = false;
                    setTimeout(function () {
                        $scope.$apply(function () {
                            $scope.flag = true;
                        });
                    }, 1000);
                }
                else if(Response==0)
                {
                    $scope.flag = false;
                    $scope.errorflag = false;
                    setTimeout(function () {
                        $scope.$apply(function () {
                            $scope.errorflag = true;
                        });
                    }, 1000);
                   
                }
                $("#loading").css("display", "none");
            }).error(function (error) { console.log(error); })
        }
        $scope.getHomePageContent = function () {
            api.Get("/Dashboard/getHomePageContent").success(function (Response) {
                var data = decodeURIComponent(Response);
                $scope.ui.html_data = $sce.trustAsHtml(data);
                var elem = document.createElement('textarea');
                elem.innerHTML = data;
                var decoded = elem.value;
                $('.nw-editor__res').text(decoded);
            }).error(function (error) { console.log(error); })
        }
        $scope.getHomePageContent();
        $scope.convert = function (convert) {
            return $("<span />", { html: convert }).text();
        };

    }]);
