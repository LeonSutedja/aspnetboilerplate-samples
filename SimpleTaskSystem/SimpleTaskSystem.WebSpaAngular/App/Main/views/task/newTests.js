///<reference path="../../../../Scripts/jasmine.js"/>
///<reference path="../../../../Scripts/angular.js"/>
///<reference path="../../../../Scripts/angular-mocks.js"/>
///<reference path="../../../../Scripts/angular-animate.js"/>
///<reference path="../../../../Scripts/angular-sanitize.js"/>
///<reference path="../../../../Scripts/angular-ui-router.js"/>
///<reference path="../../../../Scripts/angular-ui/ui-bootstrap.js"/>
///<reference path="../../../../Scripts/angular-ui/ui-utils.js"/>

///<reference path="../../../../Abp/Framework/scripts/abp.js"/>
///<reference path="../../../../Abp/Framework/scripts/libs/angularjs/abp.ng.js"/>

///<reference path="../../app.js"/>
///<reference path="new.js"/>

describe('sts.views.task.new', function () {
    var scope, controller;
    var taskServiceMock = {
        createTask: function (inputValue) { }
    };
    var personServiceMock = {
        getAllPeople: function() {}
    };
    var taskReferenceServiceMock = {
        getTaskCriticalities : function() {}
    };
    var mockAbpUi = {
        setBusy: function (arg1, arg2) { }
    };
    var mockAbpUtils = {
        formatString: function (input1, input2) { return "stringsFormatted"; }
    };
    var mockAbpLocalization = {
        getSource: function (value) { return function (value2) { }; }
    };
    var mockAbp = {
        localization: mockAbpLocalization,
        utils: mockAbpUtils,
        ui: mockAbpUi
    };

    // Set up the module
    beforeEach(module('app'));

    beforeEach(inject(function ($rootScope, $controller) {
        scope = $rootScope.$new();
        controller = $controller;
    }));

    beforeEach(function () {
        abp = mockAbp;
        spyOn(personServiceMock, "getAllPeople").and.returnValue({
            success: function (c) { }
        });
        spyOn(taskReferenceServiceMock, "getTaskCriticalities").and.returnValue({
            success: function (c) { }
        });
    });
    

    it("saveTask uses taskService.createTask", function () {
        var vm = controller("sts.views.task.new as vm",
            {
                $scope: scope,
                "abp.services.tasksystem.task": taskServiceMock,
                "abp.services.tasksystem.person": personServiceMock,
                "abp.services.tasksystem.taskreference": taskReferenceServiceMock
            });

        spyOn(taskServiceMock, "createTask").and.returnValue({
            success: function (c) { }
        });
        vm.saveTask();

        expect(taskServiceMock.createTask).toHaveBeenCalled();
    });      
});