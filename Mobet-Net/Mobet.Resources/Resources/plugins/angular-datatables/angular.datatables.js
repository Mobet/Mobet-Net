(function (angular) {
    'use strict';
    angular.module('angular.datatables', []).factory('$ngtable', function () {
        var defaultOptions = {
            "oLanguage": {
                'sSearch': '搜索',
                "sLengthMenu": "每页显示 _MENU_ 条记录",
                "sInfo": "从 _START_ 到 _END_ /共 _TOTAL_ 条数据",
                "sInfoEmpty": "没有数据",
                "sInfoFiltered": "(从 _MAX_ 条数据中检索)",
                "oPaginate": {
                    "sFirst": "首页",
                    "sPrevious": "前一页",
                    "sNext": "后一页",
                    "sLast": "尾页"
                },
                "sZeroRecords": "没有检索到数据",
            }
                    , "bSingleSelect": true             //单选或多选
					, "iDisplayLength": 10              //用于指定一屏显示的条数，需开启分页器
					, "bServerSide": true               //是否想服务器端传递参数，用于服务端分页。
					, "bPaginate": true                 //是否显示分页器
					, "bFilter": false                  //是否启用客户端过滤器
					, "bAutoWith": true                 //是否自适应宽度
					, "bDeferRender": true              //延时渲染
					, "bLengthChange": false            //是否显示每页大小的下拉框
					, "bProcessing": false               //以指定当正在处理数据的时候，是否显示“正在处理”这个提示信息
					, "bScrollInfinite": false          //以指定是否无限滚动（与sScrollY配合使用），在大数据量的时候很有用。当这个标志为true的时候，分页器就默认关闭
					, "bSort": true                     //是否启用各列具有按列排序的功能
					, "sPaginationType": "full_numbers" //用于指定分页器风格
                    , "fnAfterDrawCallback": function () {
                    }
					, "fnDrawCallback": function (oSettings) {
					    handlerRowClick($(oSettings.nTable).attr('angular-table'));

					    if ($.isFunction(tableOptions[$(oSettings.nTable).attr('angular-table')].fnAfterDrawCallback)) {
					        tableOptions[$(oSettings.nTable).attr('angular-table')].fnAfterDrawCallback();
					    }
					    if (oSettings.sScrollX != undefined) {
					        try {
					            $('.dataTables_scrollBody').css({ 'overflow': 'visible' });
					            $(".dataTables_scroll").mCustomScrollbar({
					                setWidth: false,
					                setHeight: false,
					                setTop: 0,
					                setLeft: 0,
					                axis: "x",
					                scrollbarPosition: "inside",
					                scrollInertia: 950,
					                autoDraggerLength: true,
					                autoHideScrollbar: false,
					                autoExpandScrollbar: false,
					                alwaysShowScrollbar: 0,
					                snapAmount: null,
					                snapOffset: 0,
					                mouseWheel: {
					                    enable: true,
					                    scrollAmount: "auto",
					                    axis: "x",
					                    preventDefault: false,
					                    deltaFactor: "auto",
					                    normalizeDelta: false,
					                    invert: false,
					                    disableOver: ["select", "option", "keygen", "datalist", "textarea"]
					                },
					                scrollButtons: {
					                    enable: false,
					                    scrollType: "stepless",
					                    scrollAmount: "auto"
					                },
					                keyboard: {
					                    enable: true,
					                    scrollType: "stepless",
					                    scrollAmount: "auto"
					                },
					                contentTouchScroll: 25,
					                advanced: {
					                    autoExpandHorizontalScroll: false,
					                    autoScrollOnFocus: "input,textarea,select,button,datalist,keygen,a[tabindex],area,object,[contenteditable='true']",
					                    updateOnContentResize: true,
					                    updateOnImageLoad: true,
					                    updateOnSelectorChange: false,
					                    releaseDraggableSelectors: false
					                },
					                theme: "minimal-dark",
					                callbacks: {
					                    onInit: false,
					                    onScrollStart: false,
					                    onScroll: false,
					                    onTotalScroll: false,
					                    onTotalScrollBack: false,
					                    whileScrolling: false,
					                    onTotalScrollOffset: 0,
					                    onTotalScrollBackOffset: 0,
					                    alwaysTriggerOffsets: true,
					                    onOverflowY: false,
					                    onOverflowX: false,
					                    onOverflowYNone: false,
					                    onOverflowXNone: false
					                },
					                live: false,
					                liveSelector: null
					            });

					            var right = $('.DTFC_RightWrapper').css('right') + '';
					            var fixpoint = (right.replace('px', '') - 17 ) > 0 ? right.replace('px', '') - 17 + 'px' : '0px'
					            $('.DTFC_RightWrapper').css('right', fixpoint);
					        } catch (e) {
					            console.log(e);
					        }
					    }
					    
					}
					, "fnInitComplete": function (oSettings, json) {

					}
        }

		, table = {}

		, tables = {}

		, tablename = ''

		, handlerRowClick = function (tablename) {
		    $(".datatable-checks").iCheck({
		        checkboxClass: 'icheckbox_square-green',
		        radioClass: 'iradio_square-green'
		    });
		    $('table[angular-table="' + tablename + '"]').find("tbody tr[role='row']").find(".iCheck-helper").unbind("click").bind("click", function () {
		        var elems = $(this).find(".datatable-checks").parents('.icheckbox_square-green');
		        if (elems.length > 0) {
		            var elem = $(elems[0]);
		            if (!elem.hasClass("checked")) {
		                if (tableOptions[tablename]['bSingleSelect'] == true) {
		                    $('table[angular-table="' + tablename + '"]').find(".datatable-checks").parents('.icheckbox_square-green').removeClass("checked");
		                    oSelections[tablename] = []
		                }
		                oSelections[tablename].push(tables[tablename].fnGetData(this));
		                elem.addClass("checked");
		            } else {
		                oSelections[tablename].splice($.inArray(tables[tablename].fnGetData(this), oSelections[tablename]), 1);
		                elem.removeClass("checked");
		            }
		        }
		        //console.log(tablename)
		        //console.log(oSelections[tablename])
		    });

		    $('table[angular-table="' + tablename + '"]').find("tbody tr[role='row']").unbind("click").bind("click", function () {
		        var elems = $(this).find(".datatable-checks").parents('.icheckbox_square-green');
		        if (elems.length > 0) {
		            var elem = $(elems[0]);
		            if (!elem.hasClass("checked")) {
		                if (tableOptions[tablename]['bSingleSelect'] == true) {
		                    $('table[angular-table="' + tablename + '"]').find(".datatable-checks").parents('.icheckbox_square-green').removeClass("checked");
		                    oSelections[tablename] = []
		                }
		                oSelections[tablename].push(tables[tablename].fnGetData(this));
		                elem.addClass("checked");
		            } else {
		                oSelections[tablename].splice($.inArray(tables[tablename].fnGetData(this), oSelections[tablename]), 1);
		                elem.removeClass("checked");
		            }
		        }
		        //console.log(tablename)
		        //console.log(oSelections[tablename])
		    });

		    $('table[angular-table="' + tablename + '"]').find(".datatables-check-all").unbind("click").bind("click", function () {
		        if ($(this).attr('checked') == undefined || $(this).attr('checked') == false) {
		            oSelections[tablename] = [];
		            $('table[angular-table="' + tablename + '"]').find("tbody tr[role='row']").each(function () {
		                oSelections[tablename].push(tables[tablename].fnGetData(this));
		            });
		            $('table[angular-table="' + tablename + '"]').find(".datatable-checks").parents('.icheckbox_square-green').addClass("checked");
		            $(this).attr('checked', true);
		        } else {
		            oSelections[tablename] = [];
		            $('table[angular-table="' + tablename + '"]').find(".datatable-checks").parents('.icheckbox_square-green').removeClass("checked")
		            $('table[angular-table="' + tablename + '"]').find(this).attr('checked', false);
		        }
		    });

		    oSelections[tablename] = [];

		}

		, setSelection = function (data) {
		    $(".icheckbox_square-green").removeClass("checked");
		    oSelections[tablename] = [];
		    var models = tables[tablename].fnGetData()
		    for (var i = 0 ; i < data.length; i++) {
		        for (var k = 0 ; k < models.length; k++) {
		            if (data[i].ID == models[k].ID) {
		                $('table[angular-table="' + tablename + '"]').find("tbody tr[role='row']").each(function () {
		                    if ($(this).find('input[type="hidden"]').val() == models[k].ID) {
		                        $(this).find(".icheckbox_square-green").addClass("checked");
		                    }
		                });
		                oSelections[tablename].push(models[k]);
		            }
		        }
		    }
		}

		, tableOptions = {}

		, oSelections = {}

        return {
            getTableOptions: function (name) {
                return tableOptions[name];
            },
            setTableOptions: function (options) {
                var opts = angular.extend(defaultOptions, options);
                tablename = options['sName'];
                if (tablename == undefined || tablename == '') {
                    return; 
                }
                table = $('table[angular-table="' + tablename + '"]').dataTable(opts);

                tables[tablename] = table;

                tableOptions[tablename] = opts;
            },
            getSeletion: function (tablename) {
                return oSelections[tablename];
            },
            setSeletion: function (data) {
                setSelection(data);
            },
            getTable: function (tablename) {
                return tables[tablename];
            }
        }
    }).directive('angularTable', ['$ngtable', function ($ngtable) {
        return {
            restrict: 'A',
            link: function ($scope, $element, $attrs) {
                $scope.table = {};

                var table = $ngtable.getTable($attrs.angularTable);

                if (table) {
                    table.fnGetSeletion = function () {
                        return $ngtable.getSeletion($attrs.angularTable);
                    }
                    table.fnSetSeletion = function (data) {
                        return $ngtable.setSeletion(data);
                    }
                    $scope.table[$attrs.angularTable] = table;
                }

                $scope.setTableOptions = function (options) {
                    $ngtable.setTableOptions(options);
                    var table = $ngtable.getTable(options.sName);
                    if (table) {
                        table.fnGetSeletion = function () {
                            return $ngtable.getSeletion(options.sName);
                        }
                        table.fnSetSeletion = function (data) {
                            return $ngtable.setSeletion(data);
                        }
                        $scope.table[options.sName] = table;
                    }
                }
                $.fn.dataTable.ext.errMode = 'throw';

                window.onresize = function () {
                    $('table[angular-table="' + $attrs.angularTable + '"]').css({'width': '100%','min-width':'800px'});
                }
            }
        };
    }])
}(angular));
