
var App = angular.module('App', ['ngWig', 'ngTagsInput', 'ngFileUpload', 'ui.bootstrap']);

App.factory("api", ["$http", "$rootScope", "$q", function ($http, $rootScope, $q) {

    function send(method, url, params, data) {
        return $http({
            method: method,
            url: url,
            params: params,
            data: data
        });
    }

    function Get(url, params, data) { return send("GET", url, params, data); }

    function Post(url, params, data) { return send("POST", url, params, data); }

    return {
        Get: Get,
        Post: Post
    };
}]);

App.factory("Utility", ["$rootScope", function ($rootScope) {

    function FixJsonDate(json_date) {
        
        if (json_date != null && json_date != "") {

            var date = new Date(parseInt(json_date.substr(6)));
            var day = ("0" + date.getDate()).slice(-2);
            var month = ("0" + (date.getMonth() + 1)).slice(-2);
            var year = date.getFullYear();
            //var properlyFormatted = year + '-' + month + '-' + day;
            var properlyFormatted = month + '-' + day + '-' + year;
            return properlyFormatted;
        }
        return "";
    }

    return {
        FixJsonDate: FixJsonDate
    };
}]);

//App.directive('fileModel', ['$parse', function ($parse) {
//    return {
//        restrict: 'A',
//        link: function(scope, element, attrs) {
//            var model = $parse(attrs.fileModel);
//            var modelSetter = model.assign;
            
//            element.bind('change', function(){
//                scope.$apply(function(){
//                    modelSetter(scope, element[0].files[0]);
//                });
//            });
//        }
//    };
//}]);

//App.service('Upload', ['$http', function ($http) {
//    this.uploadFileToUrl = function(file, uploadUrl){
//        var fd = new FormData();
//        fd.append('file', file);
//        $http.post(uploadUrl, fd, {
//            transformRequest: angular.identity,
//            headers: {'Content-Type': undefined}
//        })
//        .success(function(){
//        })
//        .error(function(){
//        });
//    }
//}]);

            
App.controller('UploadMultiple', ['$scope', 'Upload', '$timeout', function ($scope, Upload, $timeout) {
    $scope.uploadFiles = function (files) {
        $scope.files = files;
        if (files && files.length) {
            Upload.upload({
                url: '',
                data: {
                    files: files
                }
            }).then(function (response) {
                $timeout(function () {
                    $scope.result = response.data;
            });
            }, function (response) {
                if (response.status > 0) {
                    $scope.errorMsg = response.status + ': ' + response.data;
        }
            }, function (evt) {
                $scope.progress =
                    Math.min(100, parseInt(100.0 * evt.loaded / evt.total));
        });
    }
    };
}]);

//App.config(function ($mdDateLocaleProvider) {
//    $mdDateLocaleProvider.formatDate = function (date) {
//        return moment(date).format('DD-MM-YYYY');
//    };
//});