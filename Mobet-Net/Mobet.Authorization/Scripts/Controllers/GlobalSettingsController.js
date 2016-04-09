/**
 *  GlobalSettingsController
 *
 *  摘要：全局配置管理
 *
 */

(function () {
    angular.module('app').controller('GlobalSettingsController', function ($scope, $state, $http, $timeout, toaster, loadingBar, $dialogs, $compile) {
        $scope.globalSettings   =     {};
        $scope.model            =     {};
        $scope.domain           =     {};

        $scope.addOrUpdate = function (opt) {
            if (opt == 'update' && $scope.globalSettings.fnGetSeletion()[0] == undefined) {
                toaster.pop('error', "提示", "请选择一条数据");
                return;
            }
            var model = opt == 'add' ? null : $scope.globalSettings.fnGetSeletion()[0];
            dialog = $dialogs.create('/GlobalSettings/Detail', 'GlobalSettingsDetailController', model, { key: false, back: 'static' });
            dialog.result.then(function (model) {
                $scope.model = model;
                $scope._save();
            });
        };
        $scope.clearAllCache = function (row, $event) {

            // 行内操作获取当前行信息
            //var model;
            //if ($event) {
            //    $event.stopPropagation();
            //    model = $scope.globalSettings.fnGetData($scope.globalSettings.fnGetNodes()[row]);
            //} else {
            //    model = $scope.globalSettings.fnGetSeletion()[0];
            //}

            $http.post('/GlobalSettings/ClearAllCache').success(function (data) {
                toaster.pop(data.Done, "提示", data.Message);
            });
        }
        $scope.query = function () {
            $scope.globalSettings.fnReloadAjax();
        };
        $scope.reset = function () {
            $scope.domain = {};
        }


        $scope._save = function (flag) {
            $http.post('/GlobalSettings/AddOrUpdate', $scope.model).success(function (data) {
                toaster.pop(data.Result ? 'sucess' : 'error', "提示", data.Message);
                if (data.Result) {
                    $scope.query();
                }
            });
        };
        $scope._init = function () {
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
                             { "mDataProp": "Group", "bSortable": false, "bVisible": false },
                             { "mDataProp": "Name", "sWidth": "70px", "bAutoWidth": false, "bSortable": false, "sTitle": "<span class='datatables-check-all'>全选</span>", "sClass": "center", "mRender": function () { return "<input class='datatable-checks' type='checkbox' />"; } },
                             { "mDataProp": "DisplayName", "sWidth": "200px", "bSortable": false, "sTitle": "显示名称" },
                             { "mDataProp": "Name", "sWidth": "200px", "bSortable": false, "sTitle": "名称" },
                             { "mDataProp": "Value", "sTitle": "值", "bSortable": false, "mRender": function (v) { if (v.length > 80) { return v.substring(0, 80) + '...'; } return v; } },
                             { "mDataProp": "Description", "sWidth": "200px", "bSortable": false, "sTitle": "说明" },

                ],
                "iDisplayLength": 8,
                'fnRowCallback': function (nRow, aData, iDisplayIndex, iDisplayIndexFull) {
                    $compile(nRow)($scope);
                },
                "fnAfterDrawCallback": function (settings) {
                    var api = $scope.globalSettings.api();
                    var rows = api.rows({
                        page: 'current'
                    }).nodes();
                    var last = null;
                    api.column(0, {
                        page: 'current'
                    }).data().each(function (group, i) {
                        if (last !== group) {
                            $(rows).eq(i).before('<tr class="group"><td colspan="5" style="padding-left:30px;">' + group + '</td></tr>');
                            last = group;
                        }
                    });
                    loadingBar.complete();
                },
                "fnPreDrawCallback": function () {
                    loadingBar.start();
                },
                "fnInitComplete": function () {
                }
            });
            $scope.globalSettings = $scope.table['globalsettings'];

            $('[data-toggle="tooltip"]').tooltip();
        }

        $timeout(function () {
            $scope._init();
        });
    }).controller('GlobalSettingsDetailController', function ($scope, $modalInstance, data) {

        $scope.title = data == null ? "添加" : "修改";
        $scope.model = data;

        $scope.reset = function () {
            $scope.model = {};
        };
        $scope.save = function () {
            $modalInstance.close($scope.model);
        };
        $scope.close = function () {
            $modalInstance.dismiss('canceled');
        };

    });
})();


