/**
 *  GlobalSettingsController
 *
 *  摘要：全局配置管理
 *
 */

(function () {
    angular.module('app').controller('GlobalSettingsController', function ($scope, $state, $http, $timeout, toaster, loadingBar, $dialogs, $compile) {
        var globalSettings;
        $scope.model = {};
        $scope.domain = {};

        $scope.additional = function () {
            dialog = $dialogs.create('/GlobalSettings/Detail', 'GlobalSettingsDetailController', null, { key: false, back: 'static' });
            dialog.result.then(function (model) {
                $scope.model = model;
                $scope.save(true);
            });
        };
        $scope.modify = function (row, $event) {
            var model;
            if ($event) {
                $event.stopPropagation();
                model = globalSettings.fnGetData(globalSettings.fnGetNodes()[row]);
            } else {
                model = globalSettings.fnGetSeletion()[0];
            }
            if (model == undefined) {
                toaster.pop('error', "提示", "请选择一条数据");
                return;
            }
            dialog = $dialogs.create('/GlobalSettings/Detail', 'GlobalSettingsDetailController',model, { key: false, back: 'static' });
            dialog.result.then(function (model) {
                $scope.model = model;
                $scope.save(false);
            });
        };
        $scope.clearCache = function (row, $event) {
            var model;
            if ($event) {
                $event.stopPropagation();
                model = globalSettings.fnGetData(globalSettings.fnGetNodes()[row]);
            } else {
                model = globalSettings.fnGetSeletion()[0];
            }
        }

        $scope.save = function (flag) {
            if (flag) {
                $http.post('/GlobalSettings/CreateAsync', $scope.model).success(function (data) {
                    toaster.pop(data.Done, "提示", data.Message);
                    if (data.Done == 'success') {
                        $scope.query();
                    }
                });
            } else {
                $http.post('/GlobalSettings/SaveAsync', $scope.model).success(function (data) {
                    toaster.pop(data.Done, "提示", data.Message);
                    if (data.Done == 'success') {
                        $scope.query();
                    }
                });
            }
        };
        $scope.remove = function () {
            var selections = globalSettings.fnGetSeletion();
            if (selections != undefined && selections.length > 0) {
                dlg = $dialogs.confirm('询问', '确定要删除此数据?');
                dlg.result.then(function (btn) {
                    $http.post('/GlobalSettings/DeleteAsync', selections).success(function (data) {
                        toaster.pop(data.Done, "提示", data.Message);
                        if (data.Done == 'success') {
                            $scope.query();
                        }
                    });
                }, function (btn) {

                });

            } else {
                toaster.pop('error', "提示", "请选择一条数据");
            }
        };
        $scope.active = function () {
            if (globalSettings.fnGetSeletion() != undefined && globalSettings.fnGetSeletion().length == 1) {
                $scope.Model = globalSettings.fnGetSeletion()[0];
                $scope.Model.IS_ACTIVED = $scope.Model.IS_ACTIVED ? 0 : 1;
                $http.post('/GlobalSettings/SaveAsync', $scope.Model).success(function (data) {
                    toaster.pop(data.Done, "提示", data.Message);
                    if (data.Done == 'success') {
                        $scope.query();
                    }
                });
            } else {
                toaster.pop('error', "提示", "请选择一条数据");
            }
        };
        $scope.query = function () {
            globalSettings.fnReloadAjax();
        };
        $scope.reset = function () {
            $scope.domain = {};
        }

        $timeout(function () {
            $scope.setTableOptions({
                "sName": "globalsettings",
                "ajax": {
                    url: '/GlobalSettings/Get',
                    type: 'POST',
                    data: function (d) {
                        d.Data = $scope.domain;
                    }
                },
                "aoColumns": [
                             { "mDataProp": "Id", "bSortable": false, "sWidth": "70px", "sTitle": "<span class='datatables-check-all'>全选</span>", "sClass": "center", "mRender": function () { return "<input class='datatable-checks' type='checkbox' />"; } },
                             { "mDataProp": "DisplayName", "sWidth": "200px", "sTitle": "显示名称" },
                             { "mDataProp": "Name", "sWidth": "200px", "sTitle": "名称" },
                             { "mDataProp": "Value", "sTitle": "值", "sWidth": "200px", "mRender": function (v) { if (v.length > 80) { return v.substring(0, 80) + '...'; } return v; } },
                             { "mDataProp": "Description", "sWidth": "200px", "sTitle": "说明" },
                             {
                                 "mDataProp": "Id", "sTitle": "操作", "mRender": function (data, type, row, meta) {
                                     return '<a href="javascript:void(0)" ng-click="modify(' + meta.row + ',$event)" >编辑</a>&nbsp;&nbsp;&nbsp;&nbsp;<a href="javascript:void(0);" ng-click="clearCache(' + meta.row + ',$event)">清除缓存</a>';
                                 }
                             },
                ],
                'fnRowCallback': function (nRow, aData, iDisplayIndex, iDisplayIndexFull) {
                    $compile(nRow)($scope);
                },
                //"fixedColumns": {
                //    leftColumns: false,
                //    rightColumns: 1
                //},
                "fnAfterDrawCallback": function () {
                    loadingBar.complete();
                },
                "fnPreDrawCallback": function () {
                    loadingBar.start();
                },
                "fnInitComplete": function () {
                }
            });
            globalSettings = $scope.table['globalsettings'];
        });

        $('[data-toggle="tooltip"]').tooltip();
    }).controller('GlobalSettingsDetailController', function ($scope, $modalInstance, data) {
        $scope.model = data || { IS_ACTIVED: 1 };
        $scope.title = data == null ? "添加" : "修改";
        $scope.reset = function () {
            $scope.model = {};
        };
        $scope.save = function () {
            console.log($scope.model)
            $modalInstance.close($scope.model);
        };
        $scope.close = function () {
            $modalInstance.dismiss('canceled');
        };
    });
})();


