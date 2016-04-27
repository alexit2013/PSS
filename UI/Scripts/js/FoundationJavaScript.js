

//------------------【消息框】------------
var action_alert = function () {
    var fg = $("#fg").val();
    if (fg != "" && fg != "undefined") {
        if (fg == "del_yes") {
            new tmm_alert1("action_alert", "提示", "删除成功！");
        } else if (fg == "edit_yes") {
            new tmm_alert1("action_alert", "提示", "修改成功！");
        } if (fg == "add_yes") {
            new tmm_alert1("action_alert", "提示", "添加成功！");
        } else if (fg == "add_no") {
            new tmm_alert("action_alert", "提示", "添加失败，未知错误！");
        } else if (fg == "edit_no") {
            new tmm_alert("action_alert", "提示", "修改失败，未知错误！");
        } else if (fg == "del_no") {
            new tmm_alert("action_alert", "提示", "删除失败，未知错误！");
        }
        $("#action_alert").modal({closeViaDimmer:0,width:300});
    }

}

//------------------【树形图递归】--------------
var addtrees= function(parent) {
    var ptid = $(parent).attr("key");
    $.each(ptyes, function (k, v) {
        if (v.PTParentID == ptid) {
            var div = $("<div style='display:none'  key='" + v.PTID + "' class='trees'><a class='am-icon-plus-square-o' style='color:#0094ff' ></a>&nbsp; <span >" + v.PTName + "</span><span ></div>");
            addtrees(div);
            $(parent).append(div);
        }
    });
}

//------------------【商品类型】-------------------
var ProType = function () {
    $.post("/Foundation/Home/ProductTypeTree", {}, function (data) {
        var trress = $("#tree");
        ptyes = data;
        $(data).each(function (k, v) {
            if (v.PTParentID == 0) {
                var div = $("<div class='trees'  key='" + v.PTID + "'><a class='am-icon-plus-square-o' style='color:#0094ff'></a>&nbsp; <span>" + v.PTName + "</span><span >");
                addtrees(div);
                trress.append(div);
            }

        });
    });
    $("#tree").delegate("a", "click", function () {
        var a = $(this);
        if ($(this).parent().children("div").css("display") == "none") {
            a.removeClass("am-icon-plus-square-o").addClass("am-icon-minus-square-o");
            $(this).parent().children("div").show(400);
        } else {
            a.removeClass("am-icon-minus-square-o").addClass("am-icon-plus-square-o");
            $(this).parent().children("div").hide(400);
        }
    });
    //删除确认框
    new tmm_confirm("delete_com", "提示", "确认要删除吗？", function () {
        var id = $("#deleteid").val();
        window.location.href = "/Foundation/Home/DeleteType?id=" + id;
    }, function () {
        $("#delete_com").modal('close');
    });
   
    //查看帮助
    $("#span").click(function () {
        $("#my-id").offCanvas("open");
    });
    $("#close_my").click(function () {
        $("#my-id").offCanvas("close");
    });
    //显示添加窗体
    $("#tree").delegate("span","dblclick",function () {
        $("#addkey").val( $(this).parent().attr("key"));
        $("#add-type").modal();
    });
    //添加类型
    $("button[add]").click(function () {
        var addkey = $("#addkey").val();
        var type = $(this).text();
        var PTName = $("#ptname").val();
        var PTDes = $("#ptdes").val();
        if (type == "添加小类") {
            window.location.href = "/Foundation/Home/AddProductType?PTParentID=" + addkey + "&PTName=" + PTName + "&PTDes=" + PTDes;
        } else if (type == "添加大类") {
            window.location.href = "/Foundation/Home/AddProductType?PTParentID=0&PTName=" + PTName + "&PTDes=" + PTDes;
        }
    });
}

//------------------【供货商】-----------------------
var ProductLend = function () {
    $("#addshow").click(function () {
        document.getElementById("proform").reset();
        $.post("/Foundation/Home/GetProLendID", {}, function (data) {
            if (data) {
                $("#PPID").val(data);
            } else {
                $("#PPID").val("201501010001");
            }
        });
        $("#prolendTitle").text("添加供应商");
        $("#proform").attr("action", "/Foundation/Home/AddProductLend");
        $("#add-prolend").modal({ closeViaDimmer: 0 });
    });
    new tmm_confirm("delete_com", "提示", "确认要删除吗？", function () {
        var id = $("#deleteid").val();
        window.location.href = "/Foundation/Home/DelProductLend?id=" + id;
    }, function () {
        $("#delete_com").modal('close');
    });
    //查询
    $("#seach").click(function () {
        PageIndex = 1;
        var type = $("#seachtype option:selected").val();
        var content = $("#seachcontent").val();
        $.post("/Foundation/Home/ProLendFind", { type: type, content: content, PageIndex: PageIndex }, function (data) {
            setPage(data);
            $.post("/Foundation/Home/ProLendFindCount", { type: type, content: content, PageIndex: PageIndex }, function (dd) {
                $("#SelPage option").remove();
                for (var i = 1; i <=dd.Page; i++) {
                    $("<option value='" + i + "'>" + i + "/" + dd.Page + "</option>").appendTo("#SelPage");
                }
            });
        });
    });

};

var checkform = function () {
    var PPName=$("#PPName").val();
    var PPCompany = $("#PPCompany").val();
    var PPMan = $("#PPMan").val();
    var PPTel = $("#PPTel").val();
    var PPAddress = $("#PPAddress").val();
    var PPFax = $("#PPFax").val();
    var PPEmail = $("#PPEmail").val();
    var PPUrl = $("#PPUrl").val();
    var PPBank = $("#PPBank").val();
    var PPGoods = $("#PPGoods").val();
    if (PPName.trim().length == 0 || PPName == "Undefined") {
        $("#PPName").parent().addClass("am-form-error");
        return false;
    } else {
        $("#PPName").parent().removeClass("am-form-error");
    }
    if (PPCompany.trim().length == 0 || PPCompany == "Undefined") {
        return false;
        $("#PPCompany").parent().addClass("am-form-error");
    } else {
        $("#PPCompany").parent().removeClass("am-form-error");
    }
    if (PPMan.trim().length == 0 || PPMan == "Undefined") {
        return false;
        $("#PPMan").parent().addClass("am-form-error");
    } else {
        $("#PPMan").parent().removeClass("am-form-error");
    }
    if (PPTel.trim().length == 0 || PPTel == "Undefined") {
        return false;
        $("#PPTel").parent().addClass("am-form-error");
    } else {
        $("#PPTel").parent().removeClass("am-form-error");
    }
    if (PPAddress.trim().length == 0 || PPAddress == "Undefined") {
        return false;
        $("#PPAddress").parent().addClass("am-form-error");
    } else {
        $("#PPAddress").parent().removeClass("am-form-error");
    }
    if (PPFax.trim().length == 0 || PPFax == "Undefined") {
        return false;
        $("#PPFax").parent().addClass("am-form-error");
    } else {
        $("#PPFax").parent().removeClass("am-form-error");
    }
    if (PPEmail.trim().length == 0 || PPEmail == "Undefined") {
        return false;
        $("#PPEmail").parent().addClass("am-form-error");
    } else {
        $("#PPEmail").parent().removeClass("am-form-error");
    }
    if (PPUrl.trim().length == 0 || PPUrl == "Undefined") {
        return false;
        $("#PPUrl").parent().addClass("am-form-error");
    } else {
        $("#PPUrl").parent().removeClass("am-form-error");
    }
    if (PPBank.trim().length == 0 || PPBank == "Undefined") {
        return false;
        $("#PPBank").parent().addClass("am-form-error");
    } else {
        $("#PPBank").parent().removeClass("am-form-error");
    }
    if (PPGoods.trim().length == 0 || PPGoods == "Undefined") {
        return false;
        $("#PPGoods").parent().addClass("am-form-error");
    } else {
        $("#PPGoods").parent().removeClass("am-form-error");
    }
    return true;
}

//var recheckform = function () {
//    $("#PPName").parent().removeClass("am-form-error");
//}







