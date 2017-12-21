App.factory("ComboBoxHandler", ["api", "$rootScope", "$window", function (api, $rootScope, $window) {

    var ControllerUrl = "/ComboBox/";
    var model = {};

    GetAddingEntity = function () {
        api.Get(ControllerUrl + "GetAddingEntity").success(function (response) {
            model.addingEntity = response;
            model.addingEntity.splice(0, 0, "");
        })
        .error(function (error) {
            status = 'Unable to load data at function ComboBoxController.GetAddingEntity ' + error;
            console.log(status);
        })
    }

    GetCorrespondenceType = function () {
        api.Get(ControllerUrl + "GetCorrespondenceType").success(function (response) {
            model.correspondenceType = response;
            model.correspondenceType.splice(0, 0, "");
        })
        .error(function (error) {
            status = 'Unable to load data at function ComboBoxController.GetCorrespondenceType ' + error;
            console.log(status);
        })
    }

    GetDocumentType = function () {
        api.Get(ControllerUrl + "GetDocumentType").success(function (response) {
            model.documentType = response;
            model.documentType.splice(0, 0, "");
        })
        .error(function (error) {
            status = 'Unable to load data at function ComboBoxController.GetDocumentType ' + error;
            console.log(status);
        })
    }

    GetImportance = function () {
        api.Get(ControllerUrl + "GetImportance").success(function (response) {
            model.importance = response;
            model.importance.splice(0, 0, "");
        })
        .error(function (error) {
            status = 'Unable to load data at function ComboBoxController.GetImportance ' + error;
            console.log(status);
        })
    }

    GetSavedDocumentType = function () {
        api.Get(ControllerUrl + "GetSavedDocumentType").success(function (response) {
            model.savedDocumentType = response;
            model.savedDocumentType.splice(0, 0, "");
        })
        .error(function (error) {
            status = 'Unable to load data at function ComboBoxController.GetSavedDocumentType ' + error;
            console.log(status);
        })
    }

    GetSendingEntityType = function () {
        api.Get(ControllerUrl + "GetSendingEntityType").success(function (response) {
            model.sendingEntityType = response;
            model.sendingEntityType.splice(0, 0, "");
        })
        .error(function (error) {
            status = 'Unable to load data at function ComboBoxController.GetSendingEntityType ' + error;
            console.log(status);
        })
    }

    GetStatus = function () {
        api.Get(ControllerUrl + "GetStatus").success(function (response) {
            model.status = response;
            model.status.splice(0, 0, "");
        })
        .error(function (error) {
            status = 'Unable to load data at function ComboBoxController.GetStatus ' + error;
            console.log(status);
        })
    }

    GetTopicType = function () {
        api.Get(ControllerUrl + "GetTopicType").success(function (response) {
            model.topicType = response;
            model.topicType.splice(0, 0, "");
        })
        .error(function (error) {
            status = 'Unable to load data at function ComboBoxController.GetTopicType ' + error;
            console.log(status);
        })
    }

    GetShelves = function () {
        api.Get(ControllerUrl + "GetShelves").success(function (response) {
            model.shelves = response;
            model.shelves.splice(0, 0, "");
        })
        .error(function (error) {
            status = 'Unable to load data at function ComboBoxController.GetShelves ' + error;
            console.log(status);
        })
    }

    GetClosets = function () {
        api.Get(ControllerUrl + "GetClosets").success(function (response) {
            model.closets = response;
            model.closets.splice(0, 0, "");
        })
        .error(function (error) {
            status = 'Unable to load data at function ComboBoxController.GetClosets ' + error;
            console.log(status);
        })
    }
    GetComplaintTypes = function () {
        api.Get(ControllerUrl + "GetComplaintTypes").success(function (response) {
            model.Complaints = response;
            model.Complaints.splice(0, 0, "");
        })
        .error(function (error) {
            status = 'Unable to load data at function ComboBoxController.GetClosets ' + error;
            console.log(status);
        })
    }
    GetReplyTypes = function () {
        api.Get(ControllerUrl + "GetReplyTypes").success(function (response) {
            model.Replies = response;
            model.Replies.splice(0, 0, "");
        })
        .error(function (error) {
            status = 'Unable to load data at function ComboBoxController.GetClosets ' + error;
            console.log(status);
        })
    }
    GetAddingEntity();
    GetCorrespondenceType();
    GetDocumentType();
    GetImportance();
    GetSavedDocumentType();
    GetSendingEntityType();
    GetStatus();
    GetTopicType();
    GetShelves();
    GetClosets();
    GetReplyTypes();
    GetComplaintTypes();
    return {
        model: model
    };

}]);