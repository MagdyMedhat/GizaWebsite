App.controller('DashboardController', ["Utility", "api", "$scope", "$rootScope", "$window", "$timeout",
    function (Utility, api, $scope, $rootScope, $window, $timeout) {

        if (sessionStorage.isAdmin == "false")
            $window.location.href = "/home/noaccess";

        $scope.flag = false;
        $scope.errorflag = false;
        $scope.sending_entity_types = [];
        $scope.status_types = [];
        $scope.document_types = [];
        $scope.topic_types = [];
        $scope.adding_entity_types = [];
        $scope.c_types = [];
        $scope.importance_types = [];
        $scope.closets = [];
        $scope.shelves = [];

        var sending_entity_types = [];
        var status_types = [];
        var document_types = [];
        var topic_types = [];
        var adding_entity_types = [];
        var c_types = [];
        var importance_types = [];
        var closets = [];
        var shelves = [];

        $scope.getLookups = function () {
            $("#loading").css("display", "block");
            api.Get("/Dashboard/getLookups").success(function (Response) {
                if (Response[0]) {
                    sending_entity_types = angular.copy(Response[0]);
                    $scope.sending_entity_types = Response[0];
                }
                if (Response[1]) {
                    status_types = angular.copy(Response[1]);
                    $scope.status_types = Response[1];
                }
                if (Response[2]) {
                    document_types = angular.copy(Response[2]);
                    $scope.document_types = Response[2];
                }
                if (Response[3]) {
                    topic_types = angular.copy(Response[3]);
                    $scope.topic_types = Response[3];
                }
                if (Response[4]) {
                    adding_entity_types = angular.copy(Response[4]);
                    $scope.adding_entity_types = Response[4];
                }
                if (Response[5]) {
                    c_types = angular.copy(Response[5]);
                    $scope.c_types = Response[5];
                }
                if (Response[6]) {
                    importance_types = angular.copy(Response[6]);;
                    $scope.importance_types = Response[6];
                }
                if (Response[7]) {
                    closets = angular.copy(Response[7]);;
                    $scope.closets = Response[7];
                }
                if (Response[8]) {
                    shelves = angular.copy(Response[8]);;
                    $scope.shelves = Response[8];
                }
                $("#loading").css("display", "none");
            }).error(function (error) { console.log(error); })
        }
        $scope.getAdditions = function (before, after) {
            var toBeAdded = [];
            for (var i = 0; i < after.length; i++) {
                var found = false;
                for (var j = 0; j < before.length; j++) {
                    if (after[i].text == before[j].text) {
                        found = true;
                        break;
                    }
                }
                if (!found)
                    toBeAdded.push(after[i].text);
            }
            return toBeAdded;
        }
        $scope.getDeletions = function (before, after) {
            var toBeDeleted = [];
            for (var i = 0; i < before.length; i++) {
                var found = false;
                for (var j = 0; j < after.length; j++) {
                    if (after[j].text == before[i].text) {
                        found = true;
                        break;
                    }
                }
                if (!found)
                    toBeDeleted.push(before[i].text);
            }
            return toBeDeleted;
        }

        $scope.saveSendingEntityTypes = function () {
            var toBeAdded = $scope.getAdditions(sending_entity_types, $scope.sending_entity_types);
            var toBeDeleted = $scope.getDeletions(sending_entity_types, $scope.sending_entity_types);
            api.Post("/Dashboard/saveSendingEntityTypes", null, { toBeAdded: toBeAdded, toBeDeleted: toBeDeleted }).success(function (Response) {
                if (Response[0]) {
                    if (Response[0].length > 0) {
                        var str = '';
                        for (var i = 0; i < Response[0].length; i++) {
                            str += Response[i] + ',';
                        }
                        str = str.substring(0, str.length - 1);
                        $('#errormsg').html(' ":لم نتمكن من حذف الحقول' + str + ' "لارتباطها بعناصر');
                        $scope.flag = false;
                        $scope.errorflag = false;
                        $timeout(function () {
                            $scope.errorflag = true;
                        }, 1000);
                    }
                    else {
                        if (Response[1]) {
                            $scope.flag = false;
                            $scope.errorflag = false;
                            $timeout(function () {
                                $scope.flag = true;
                            }, 1000);
                            $scope.sending_entity_types = angular.copy(Response[1]);
                        }
                    }
                }
                $("#scroll_top").focus();
            }).error(function (error) { console.log(error); })
        }
        $scope.saveStatus = function () {
            var toBeAdded = $scope.getAdditions(status_types, $scope.status_types);
            var toBeDeleted = $scope.getDeletions(status_types, $scope.status_types);
            api.Post("/Dashboard/saveStatus", null, { toBeAdded: toBeAdded, toBeDeleted: toBeDeleted }).success(function (Response) {

                if (Response[0]) {
                    if (Response[0].length > 0) {
                        var str = '';
                        for (var i = 0; i < Response[0].length; i++) {
                            str += Response[i] + ',';
                        }
                        str = str.substring(0, str.length - 1);
                        $('#errormsg').html(' :لم نتمكن من حذف الحقول' + str + ' لارتباطها بعناصر');
                        $scope.flag = false;
                        $scope.errorflag = false;
                        $timeout(function () {
                            $scope.errorflag = true;
                        }, 1000);
                    }
                    else {
                        if (Response[1]) {
                            $scope.flag = false;
                            $scope.errorflag = false;
                            $timeout(function () {
                                $scope.flag = true;
                            }, 1000);
                            $scope.status_types = angular.copy(Response[1]);
                        }
                    }
                }
                $("#scroll_top").focus();
            }).error(function (error) { console.log(error); })
        }
        $scope.saveDocumentTypes = function () {
            var toBeAdded = $scope.getAdditions(document_types, $scope.document_types);
            var toBeDeleted = $scope.getDeletions(document_types, $scope.document_types);
            api.Post("/Dashboard/saveDocumentTypes", null, { toBeAdded: toBeAdded, toBeDeleted: toBeDeleted }).success(function (Response) {
                if (Response[0]) {
                    if (Response[0].length > 0) {
                        var str = '';
                        for (var i = 0; i < Response[0].length; i++) {
                            str += Response[i] + ',';
                        }
                        str = str.substring(0, str.length - 1);
                        $('#errormsg').html(' :لم نتمكن من حذف الحقول' + str + ' لارتباطها بعناصر');
                        $scope.flag = false;
                        $scope.errorflag = false;
                        $timeout(function () {
                            $scope.errorflag = true;
                        }, 1000);
                    }
                    else {
                        if (Response[1]) {
                            $scope.flag = false;
                            $scope.errorflag = false;
                            $timeout(function () {
                                $scope.flag = true;
                            }, 1000);
                            $scope.document_types = angular.copy(Response[1]);
                        }
                    }
                }
                $("#scroll_top").focus();
            }).error(function (error) { console.log(error); })
        }
        $scope.saveTopicTypes = function () {
            var toBeAdded = $scope.getAdditions(topic_types, $scope.topic_types);
            var toBeDeleted = $scope.getDeletions(topic_types, $scope.topic_types);
            api.Post("/Dashboard/saveTopicTypes", null, { toBeAdded: toBeAdded, toBeDeleted: toBeDeleted }).success(function (Response) {
                if (Response[0]) {
                    if (Response[0].length > 0) {
                        var str = '';
                        for (var i = 0; i < Response[0].length; i++) {
                            str += Response[i] + ',';
                        }
                        str = str.substring(0, str.length - 1);
                        $('#errormsg').html(' :لم نتمكن من حذف الحقول' + str + ' لارتباطها بعناصر');
                        $scope.flag = false;
                        $scope.errorflag = false;
                        $timeout(function () {
                            $scope.errorflag = true;
                        }, 1000);
                    }
                    else {
                        if (Response[1]) {
                            $scope.flag = false;
                            $scope.errorflag = false;
                            $timeout(function () {
                                $scope.flag = true;
                            }, 1000);
                            $scope.topic_types = angular.copy(Response[1]);
                        }
                    }
                }
                $("#scroll_top").focus();
            }).error(function (error) { console.log(error); })
        }
        $scope.saveAddingEntityTypes = function () {
            var toBeAdded = $scope.getAdditions(adding_entity_types, $scope.adding_entity_types);
            var toBeDeleted = $scope.getDeletions(adding_entity_types, $scope.adding_entity_types);
            api.Post("/Dashboard/saveAddingEntityTypes", null, { toBeAdded: toBeAdded, toBeDeleted: toBeDeleted }).success(function (Response) {
                if (Response[0]) {
                    if (Response[0].length > 0) {
                        var str = '';
                        for (var i = 0; i < Response[0].length; i++) {
                            str += Response[i] + ',';
                        }
                        str = str.substring(0, str.length - 1);
                        $('#errormsg').html(' :لم نتمكن من حذف الحقول' + str + ' لارتباطها بعناصر');
                        $scope.flag = false;
                        $scope.errorflag = false;
                        $timeout(function () {
                            $scope.errorflag = true;
                        }, 1000);
                    }
                    else {
                        if (Response[1]) {
                            $scope.flag = false;
                            $scope.errorflag = false;
                            $timeout(function () {
                                $scope.flag = true;
                            }, 1000);
                            $scope.adding_entity_types = angular.copy(Response[1]);
                        }
                    }
                }
                $("#scroll_top").focus();
            }).error(function (error) { console.log(error); })
        }
        $scope.saveCTypes = function () {
            var toBeAdded = $scope.getAdditions(c_types, $scope.c_types);
            var toBeDeleted = $scope.getDeletions(c_types, $scope.c_types);
            api.Post("/Dashboard/saveCTypes", null, { toBeAdded: toBeAdded, toBeDeleted: toBeDeleted }).success(function (Response) {
                if (Response[0]) {
                    if (Response[0].length > 0) {
                        var str = '';
                        for (var i = 0; i < Response[0].length; i++) {
                            str += Response[i] + ',';
                        }
                        str = str.substring(0, str.length - 1);
                        $('#errormsg').html(' :لم نتمكن من حذف الحقول' + str + ' لارتباطها بعناصر');
                        $scope.flag = false;
                        $scope.errorflag = false;
                        $timeout(function () {
                            $scope.errorflag = true;
                        }, 1000);
                    }
                    else {
                        if (Response[1]) {
                            $scope.flag = false;
                            $scope.errorflag = false;
                            $timeout(function () {
                                $scope.flag = true;
                            }, 1000);
                            $scope.c_types = angular.copy(Response[1]);
                        }
                    }
                }
                $("#scroll_top").focus();
            }).error(function (error) { console.log(error); })
        }
        $scope.saveImportanceTypes = function () {
            var toBeAdded = $scope.getAdditions(importance_types, $scope.importance_types);
            var toBeDeleted = $scope.getDeletions(importance_types, $scope.importance_types);
            api.Post("/Dashboard/saveImportanceTypes", null, { toBeAdded: toBeAdded, toBeDeleted: toBeDeleted }).success(function (Response) {
                if (Response[0]) {
                    if (Response[0].length > 0) {
                        var str = '';
                        for (var i = 0; i < Response[0].length; i++) {
                            str += Response[i] + ',';
                        }
                        str = str.substring(0, str.length - 1);
                        $('#errormsg').html(' :لم نتمكن من حذف الحقول' + str + ' لارتباطها بعناصر');
                        $scope.flag = false;
                        $scope.errorflag = false;
                        $timeout(function () {
                            $scope.errorflag = true;
                        }, 1000);
                    }
                    else {
                        if (Response[1]) {
                            $scope.flag = false;
                            $scope.errorflag = false;
                            $timeout(function () {
                                $scope.flag = true;
                            }, 1000);
                            $scope.importance_types = angular.copy(Response[1]);
                        }
                    }
                }
                $("#scroll_top").focus();
            }).error(function (error) { console.log(error); })
        }
        $scope.saveClosets = function () {
            var toBeAdded = $scope.getAdditions(closets, $scope.closets);
            var toBeDeleted = $scope.getDeletions(closets, $scope.closets);
            api.Post("/Dashboard/saveClosets", null, { toBeAdded: toBeAdded, toBeDeleted: toBeDeleted }).success(function (Response) {
                if (Response[0]) {
                    if (Response[0].length > 0) {
                        var str = '';
                        for (var i = 0; i < Response[0].length; i++) {
                            str += Response[i] + ',';
                        }
                        str = str.substring(0, str.length - 1);
                        $('#errormsg').html(' :لم نتمكن من حذف الحقول' + str + ' لارتباطها بعناصر');
                        $scope.flag = false;
                        $scope.errorflag = false;
                        $timeout(function () {
                            $scope.errorflag = true;
                        }, 1000);
                    }
                    else {
                        if (Response[1]) {
                            $scope.flag = false;
                            $scope.errorflag = false;
                            $timeout(function () {
                                $scope.flag = true;
                            }, 1000);
                            $scope.closets = angular.copy(Response[1]);
                        }
                    }
                }
                $("#scroll_top").focus();
            }).error(function (error) { console.log(error); })
        }
        $scope.saveShelves = function () {
            var toBeAdded = $scope.getAdditions(shelves, $scope.shelves);
            var toBeDeleted = $scope.getDeletions(shelves, $scope.shelves);
            api.Post("/Dashboard/saveShelves", null, { toBeAdded: toBeAdded, toBeDeleted: toBeDeleted }).success(function (Response) {
                if (Response[0]) {
                    if (Response[0].length > 0) {
                        var str = '';
                        for (var i = 0; i < Response[0].length; i++) {
                            str += Response[i] + ',';
                        }
                        str = str.substring(0, str.length - 1);
                        $('#errormsg').html(' :لم نتمكن من حذف الحقول' + str + ' لارتباطها بعناصر');
                        $scope.flag = false;
                        $scope.errorflag = false;
                        $timeout(function () {
                            $scope.errorflag = true;
                        }, 1000);
                    }
                    else {
                        if (Response[1]) {
                            $scope.flag = false;
                            $scope.errorflag = false;
                            $timeout(function () {
                                $scope.flag = true;
                            }, 1000);
                            $scope.shelves = angular.copy(Response[1]);
                        }
                    }
                }
                $("#scroll_top").focus();
            }).error(function (error) { console.log(error); })
        }

        $scope.getLookups();
    }]);
