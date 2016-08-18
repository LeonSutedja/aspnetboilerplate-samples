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
///<reference path="list.js"/>

describe('sts.views.task.list', function () {
    var scope, controller;
    var taskServiceMock = {};
    
    var mockAbpUtils = {
        formatString: function (input1, input2) { return "stringsFormatted"; }
    };

    var mockAbpLocalization = {
        getSource: function (value) { return function (value2) { }; }
    };

    var mockAbp = {
        localization: mockAbpLocalization,
        utils: mockAbpUtils
    };

    // Set up the module
    beforeEach(module('app'));

    beforeEach(inject(function ($rootScope, $controller) {
        scope = $rootScope.$new();
        controller = $controller;
    }));

    beforeEach(function () {
        abp = mockAbp;
    });
    
    it("getTaskCountText is not null", function () {
        var vm = controller("sts.views.task.list as vm",
            {
                $scope: scope,
                "abp.services.tasksystem.task": taskServiceMock                
            });
        expect(vm.getTaskCountText()).not.toBe(null)
    });
});