﻿(function() {
    var app = angular.module('app');

    var controllerId = 'sts.views.task.list';
    app.controller(controllerId, [
        '$scope', 'abp.services.tasksystem.task',
        function($scope, taskService) {
            var vm = this;

            vm.localize = abp.localization.getSource('SimpleTaskSystem');

            vm.tasks = [];

            $scope.selectedTaskState = 0;

            $scope.$watch('selectedTaskState', function(value) {
                vm.refreshTasks();
            });

            vm.refreshTasks = function() {
                abp.ui.setBusy( //Set whole page busy until getTasks complete
                    null,
                    taskService.getTasks({ //Call application service method directly from javascript
                        state: $scope.selectedTaskState > 0 ? $scope.selectedTaskState : null
                    }).success(function(data) {
                        vm.tasks = data.tasks;
                    })
                );
            };

            vm.deleteTask = function (task) {
                var taskDescription = task.taskDescription;
                taskService.deleteTask({
                    TaskId: task.id
                }).success(function () {
                    vm.refreshTasks();
                    abp.notify.info(vm.localize('TaskDeletedMessage'), taskDescription);
                });
            };

            vm.changeTaskState = function(task) {
                taskService.switchTaskState({
                    taskId: task.id
                }).success(function () {
                    vm.refreshTasks();
                    abp.notify.info(vm.localize('TaskUpdatedMessage'));
                });
            };

            vm.getTaskCountText = function() {
                return abp.utils.formatString(vm.localize('Xtasks'), vm.tasks.length);
            };
        }
    ]);
})();