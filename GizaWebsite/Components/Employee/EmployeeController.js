App.controller('EmployeeController', ["Utility", "api", "ComboBoxHandler", "$scope", "$rootScope", "$window", "Upload",
    function (Utility, api, ComboBoxHandler, $scope, $rootScope, $window, Upload) {

        if (sessionStorage.isLogged == "false")
            $window.location.href = "/home/noaccess";

        $scope.model = {};
        $scope.model.employee = {};
        $scope.model.files = [];
        $scope.model.fileNames = [];
        

        $scope.model.dateFixer = Utility.FixJsonDate;


        $scope.getPDF = function (file_url) {
            var success = new PDFObject({ url: file_url }).embed("pdf");
        }

        $scope.openFile = function (path) {
            //$window.open(path);
            $scope.getPDF(path);
        }
        $scope.Open = function (id) {
            sessionStorage.employee_id = id;
            $window.location.href = "/Employee/Open/";
        }
        $scope.Update = function (id) {
            sessionStorage.employee_id = id;
            $window.location.href = "/Employee/Update/";
        }
        $scope.Create = function () {
            $window.location.href = "/Employee/Create/";
        }

        $scope.CreateEmployee = function () {
            $("#loading").css("display", "block");
            api.Post('/Employee/CreateEmployee', $scope.model.employee).success(function (response) {
                if (response != null && response != 0)
                    $scope.uploadFiles(response);
            }).error(function (error) {
                console.log(error);
            })
        };
        $scope.UpdateEmployee = function () {
            $("#loading").css("display", "block");
            api.Post("/Employee/UpdateEmployee", $scope.model.employee).success(function (Response) {
                if (Response != "0")
                    $scope.uploadFiles(Response);
                $("#loading").css("display", "none");
                $scope.model.showMessage = true;
                $("#scroll_top").focus();
            }).error(function (error) { console.log(error); })
        }
        $scope.DeleteEmployee = function (id) {
            $("#loading").css("display", "block");
            api.Post("/Employee/DeleteEmployee/" + id).success(function (Response) {
                if (Response == "True") {
                    $scope.SearchEmployee();
                    $scope.model.showMessage = true;
                    $("#scroll_top").focus();
                }
                $("#loading").css("display", "none");
            }).error(function (error) { console.log(error); })
        }
        $scope.GetEmployee = function () {
            $("#loading").css("display", "block");
            api.Get('/Employee/GetEmployee/' + sessionStorage.employee_id).success(function (response) {
                $scope.model.employee = response[0];
                $scope.model.employee.DATE_HIRED = $scope.model.dateFixer($scope.model.employee.DATE_HIRED);
                $scope.model.fileNames = response[1];
                $("#loading").css("display", "none");
            }).error(function (error) {
                console.log(error);
            });
        }
        $scope.SearchEmployee = function () {
            $("#loading").css("display", "block");
            api.Get("/Employee/search", { keyword: $scope.model.searchKeyword, pageNumber: $scope.model.tableCurrentPage }).success(function (Response) {
                $scope.model.employees = Response[0];
                $scope.model.tableTotalCount = Response[1];
                $("#loading").css("display", "none");
            }).error(function (error) { console.log(error); })
        }
        $scope.uploadFiles = function (id) {
            var files = $scope.model.files;
            if (files && files.length) {
                Upload.upload({
                    url: '/Employee/uploadFile/' + id,
                    data: {
                        files: files
                    }
                }).then(function (response) {
                    if (response.status > 0) {
                        $scope.errorMsg = response.status + ': ' + response.data;
                        $("#loading").css("display", "none");
                        $scope.model.showMessage = true;
                        $("#c_number").focus();
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


        /*Entry Point
        [1] Employee Index
        [2] Open Employee
        [3] New Employee 
        [4] Update Employee*/
        $scope.model.token = $("#token").val();
        if ($scope.model.token == "open" || $scope.model.token == "update")
            $scope.GetEmployee(sessionStorage.employee_id);
        else if ($scope.model.token == "index")
            $scope.SearchEmployee();
        else if ($scope.model.token == "create");
    }]);