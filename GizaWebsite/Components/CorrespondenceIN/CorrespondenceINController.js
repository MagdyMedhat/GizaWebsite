
App.controller('CorrespondenceINController', ["Utility", "api", "ComboBoxHandler", "$scope", "$rootScope", "$window", "Upload","$sce",
function (Utility, api, ComboBoxHandler, $scope, $rootScope, $window, Upload,$sce) {

        if (sessionStorage.isLogged == "false")
            $window.location.href = "/Home/Noaccess";

        $scope.model = {};
        $scope.model.comboBoxData = ComboBoxHandler.model;
        $scope.model.correspondence = {}; 
        $scope.model.files = [];
        $scope.model.fileNames = [];
        $scope.model.showMessage = false;

        $scope.model.dateFixer = Utility.FixJsonDate;
        $scope.content = {};
        $scope.Open = function (id) {
            sessionStorage.correspondence_id = id;
            $window.location.href = "/CorrespondenceIN/open/";           
        }
        $scope.Update = function (id) {
            sessionStorage.correspondence_id = id;
            $window.location.href = "/CorrespondenceIN/update/";
        }
        $scope.Create = function () {
            $window.location.href = "/CorrespondenceIN/create/";
        }

        $scope.TabClick = function (type) {
            if (type == "OUT")
                $window.location.href = "/CorrespondenceOUT";
        }

        $scope.CheckSelection = function (option, value) {
            if (option == value)
                return true;
            else
                return false;
        }

        $scope.openFile = function (path) {
            //$window.open(path);
            $scope.getPDF(path);
        }

        $scope.OpenCorrespondence = function (id) {
            $("#loading").css("display", "block");
            api.Get("/CorrespondenceIN/openCorrespondence/" + id).success(function (Response) {
                $scope.model.correspondence = Response[0];

                //convert dates to readable format
                $scope.model.correspondence.TO_OFFICE_RECEIPT_DATE = new Date(Utility.FixJsonDate($scope.model.correspondence.TO_OFFICE_RECEIPT_DATE));
                $scope.model.correspondence.INCOMING_DATE = new Date(Utility.FixJsonDate($scope.model.correspondence.INCOMING_DATE));

                if (Response[1] != null) {
                    $scope.model.fileNames = Response[1];
                }
                if (Response[2] != null)
                    $scope.model.linkageIDs = Response[2];
               
                $("#loading").css("display", "none");
            }).error(function (error) { console.log(error); })
        }

        $scope.CreateCorrespondence = function () {
            $scope.model.correspondence.CREATED_DATE = new Date();
            $("#loading").css("display", "block");
            api.Post("/CorrespondenceIN/createCorrespondence/", $scope.model.correspondence).success(function (Response) {
                if (Response != null && Response != 0)
                    $scope.uploadFiles(Response); //response is returned with correspondence id
            }).error(function (error) { console.log(error); })
        }

        $scope.UpdateCorrespondence = function () {
            $scope.model.correspondence.CREATED_DATE = new Date();
            $("#loading").css("display", "block");
            api.Post("/CorrespondenceIN/updateCorrespondence/", $scope.model.correspondence).success(function (Response) {
                if (Response != null && Response != 0)
                    $scope.uploadFiles(Response); //response is returned with correspondence id
            }).error(function (error) { console.log(error); })
        }

        $scope.DeleteCorrespondence = function (id) {
            $("#loading").css("display", "block");
            api.Post("/CorrespondenceIN/deleteCorrespondence/" + id).success(function (Response) {
                if (Response == "True") {
                    $scope.SearchCorrespondence();
                    $scope.model.showMessage = true;
                    $("#scroll_top").focus();
                }
                $("#loading").css("display", "none");
            }).error(function (error) { console.log(error); })
        }

        $scope.SearchCorrespondence = function () {
            $("#loading").css("display", "block");
            api.Get("/CorrespondenceIN/searchCorrespondence/", { keyword: $scope.model.searchKeyword, correspondenceType: $scope.model.correspondence.IN_OUT, pageNumber: $scope.model.tableCurrentPage }).success(function (Response) {
                $scope.model.correspondences = Response[0];
                $scope.model.tableTotalCount = Response[1];
                $("#loading").css("display", "none");
            }).error(function (error) { console.log(error); })
        }

        $scope.uploadFiles = function (id) {
            var files = $scope.model.files;
            if (files && files.length) {
                Upload.upload({
                    url: '/CorrespondenceIN/uploadFile/' + id,
                    data: {
                        files: files
                    }
                }).then(function (response) {
                    if (response.status > 0) {
                        $scope.errorMsg = response.status + ': ' + response.data;
                        $("#loading").css("display", "none");
                        $scope.model.showMessage = true;
                        $("#scroll_top").focus();
                    }
                }, function (evt) {
                    $scope.progress =
                        Math.min(100, parseInt(100.0 * evt.loaded / evt.total));
                });
            }
            $("#loading").css("display", "none");
            $scope.model.showMessage = true;
            $("#scroll_top").focus();
        };
        
        $scope.getPDF=function(file_url)
        {
            var success = new PDFObject({ url: file_url }).embed("pdf");
        }
        /*Entry Point
         [1] Correspondence Index
         [2] Open Correspondence
         [3] New Correspondence 
         [4] Update Correspondence*/

        $scope.model.token = $("#token").val();
        if ($scope.model.token == "open" || $scope.model.token == "update")
            $scope.OpenCorrespondence(sessionStorage.correspondence_id);
        else if ($scope.model.token == "index")
            $scope.SearchCorrespondence();
        else if ($scope.model.token == "create");
    }]);