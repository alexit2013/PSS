/// <reference path="../jquery-1.10.2.js" />


//模态框
var tmm_modal = function (id,title,context) {
    var tmm_md = $("<div class='am-modal am-modal-no-btn' tabindex='-1' id='" + id + "'><div class='am-modal-dialog'><div class='am-modal-hd' >" + title + "<a href='javascript: void(0)' class='am-close am-close-spin' data-am-modal-close>&times;</a></div><div class='am-modal-bd'>" + context + "</div></div></div>");
    $("body").append(tmm_md);
};
//消息框
var tmm_alert = function (id,title,context) {
    var tmm_at = $("<div class='am-modal am-modal-alert' tabindex='-1'  closeViaDimmer='false'  id='" + id + "'><div class='am-modal-dialog'><div class='am-modal-hd' style='background-color:#ff6a00;color:#ffffff'><i class='am-icon-warning' style='color:red'></i>&nbsp;&nbsp;&nbsp;" + title + "</div><div class='am-modal-bd'>" + context + "</div><div class='am-modal-footer'> <span class='am-modal-btn'>确定</span> </div></div></div>");
    $("body").append(tmm_at);
};

var tmm_alert1 = function (id, title, context) {
    var tmm_at = $("<div class='am-modal am-modal-alert' tabindex='-1'  closeViaDimmer='false'  id='" + id + "'><div class='am-modal-dialog'><div class='am-modal-hd'  style='background-color:green;color:#ffffff'> <i class='am-icon-warning' style='color:#ffffff'></i>&nbsp;&nbsp;&nbsp;" + title + "</div><div class='am-modal-bd'>" + context + "</div><div class='am-modal-footer'> <span class='am-modal-btn'>确定</span> </div></div></div>");
    $("body").append(tmm_at);
};
//确认框
var tmm_confirm = function (id,title1, context1,fun1,fun2) {
    var tmm_cf = $("<div class='am-modal am-modal-confirm'  tabindex='-1' id='" + id + "'><div class='am-modal-dialog'><div class='am-modal-hd'  style='background-color:#ff6a00;color:#ffffff'> <i class='am-icon-warning' style='color:ffffff'></i>&nbsp;&nbsp;&nbsp;" + title1 + "</div><div class='am-modal-bd'>" + context1 + "</div><div class='am-modal-footer'><span class='am-modal-btn' id='qx' data-am-modal-cancel>取消</span><span class='am-modal-btn' data-am-modal-confirm id='qd'>确定</span></div></div></div>");
    $("body").append(tmm_cf);
    $("#qd").click(fun1);
    $("#qx").click(fun2);
};

//加载窗体
var tmm_loeading = function (id,context) {
    var tmm_ld = $('<div class="am-modal am-modal-loading am-modal-no-btn" tabindex="-1" id="'+id+'"><div class="am-modal-dialog"><div class="am-modal-hd">'+context+'...</div><div class="am-modal-bd"><span class="am-icon-spinner am-icon-spin"></span></div> </div></div>');
    $("body").append(tmm_ld);
};
var tmm_loeading1 = function (id,id1, context) {
    var tmm_ld = $('<div class="am-modal am-modal-loading am-modal-no-btn" tabindex="-1" id="' + id + '"><div class="am-modal-dialog"><div id="'+id1+'" class="am-modal-hd">' + context + '...</div><div class="am-modal-bd"><span class="am-icon-spinner am-icon-spin"></span></div> </div></div>');
    $("body").append(tmm_ld);
};

//左侧栏

//右侧栏

//下拉菜单