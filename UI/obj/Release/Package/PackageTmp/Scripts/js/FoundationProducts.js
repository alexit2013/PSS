/// <reference path="jquery.easyui.min.js" />
/// <reference path="../assets/js/amazeui.js" />
/// <reference path="FoundationJavaScript.js" />
/// <reference path="modal.js" />




       $(function () {
           //加载数据
           loeadData();
           //商品单位
           $("#AddUnit").click(function () {
               $("#modaltitle").text("添加商品单位");
               $("#btn_unit").val("添加");
               $("#PUName").val("");
               $("#spec").hide();
               $("#color").hide();
               $("#unit").show();
               $("#Promodal").modal({ closeViaDimmer: 0,width:400,height:160 });
           });
           $("#btn_unit").click(function () {
               var type = $("#btn_unit").val();
               var PUName = $("#PUName").val();
               var PUID = $("#PUID").val();
               var ico = $("<i class='am-icon-spinner am-icon-spin hi' id='hico'></i>");
               $(this).append(ico);
               if (type == "添加") {
                   $.post("/Foundation/Home/AddProcutUnit", { PUName: PUName }, function (data) {
                       if (data == "add_yes") {
                           loeadData();
                           new tmm_alert1("add-1", "提示", "添加成功！");
                           $("#Promodal").modal("close");
                           $("#add-1").modal();
                       } else {
                           new tmm_alert("add-1", "提示", "添加失败！");
                           $("#add-1").modal();
                       }
                   });
               } else if (type == "修改") {
                   $.post("/Foundation/Home/EditProcutUnit", { PUID: PUID, PUName: PUName }, function (data) {
                       if (data == "edit_yes") {
                           loeadData();
                           new tmm_alert1("add-1", "提示", "修改成功！");
                           $("#Promodal").modal("close");
                           $("#add-1").modal();
                       } else {
                           new tmm_alert("add-1", "提示", "修改失败！");
                           $("#add-1").modal();
                       }
                   });
               }

               $("#btn_unit i").remove();
           });
           //商品规格
           $("#AddSpec").click(function () {
               $("#modaltitle").text("添加商品规格");
               $("#btn_spec").val("添加");
               $("#PSName").val("");
               $("#unit").hide();
               $("#color").hide();
               $("#spec").show();
               $("#Promodal").modal({ closeViaDimmer: 0, width: 400, height: 160 });
           });
           $("#btn_spec").click(function () {
               var type = $("#btn_spec").val();
               var PSName = $("#PSName").val();
               var PSID = $("#PSID").val();
               var ico = $("<i class='am-icon-spinner am-icon-spin hi' id='hico'></i>");
               $(this).append(ico);
               if (type == "添加") {
                   $.post("/Foundation/Home/AddProductSpec", { PSName: PSName }, function (data) {
                       if (data == "add_yes") {
                           loeadData();
                           new tmm_alert1("add-1", "提示", "添加成功！");
                           $("#Promodal").modal("close");
                           $("#add-1").modal();
                       } else {
                           new tmm_alert("add-1", "提示", "添加失败！");
                           $("#add-1").modal();
                       }
                   });
               } else if (type == "修改") {
                   $.post("/Foundation/Home/EditProductSpec", { PSID: PSID, PSName: PSName }, function (data) {
                       if (data == "edit_yes") {
                           loeadData();
                           new tmm_alert1("add-1", "提示", "修改成功！");
                           $("#Promodal").modal("close");
                           $("#add-1").modal();
                       } else {
                           new tmm_alert("add-1", "提示", "修改失败！");
                           $("#add-1").modal();
                       }
                   });
               }

               $("#btn_unit i").remove();
           });
           //商品颜色
           $("#AddColor").click(function () {
               $("#modaltitle").text("添加商品颜色");
               $("#btn_color").val("添加");
               $("#PCName").val("");
               $("#unit").hide();
               $("#spec").hide();
               $("#color").show();
               $("#Promodal").modal({ closeViaDimmer: 0, width: 400, height: 160 });
           });
           $("#btn_color").click(function () {
               var type = $("#btn_color").val();
               var PCName = $("#PCName").val();
               var PCID = $("#PCID").val();
               var ico = $("<i class='am-icon-spinner am-icon-spin hi' id='hico'></i>");
               $(this).append(ico);
               if (type == "添加") {
                   $.post("/Foundation/Home/AddProductColor", { PCName: PCName }, function (data) {
                       if (data == "add_yes") {
                           loeadData();
                           new tmm_alert1("add-1", "提示", "添加成功！");
                           $("#Promodal").modal("close");
                           $("#add-1").modal();
                       } else {
                           new tmm_alert("add-1", "提示", "添加失败！");
                           $("#add-1").modal();
                       }
                   });
               } else if (type == "修改") {
                   $.post("/Foundation/Home/EditProductColor", { PCID: PCID, PCName: PCName }, function (data) {
                       if (data == "edit_yes") {
                           loeadData();
                           new tmm_alert1("add-1", "提示", "修改成功！");
                           $("#Promodal").modal("close");
                           $("#add-1").modal();
                       } else {
                           new tmm_alert("add-1", "提示", "修改失败！");
                           $("#add-1").modal();
                       }
                   });
               }

               $("#btn_unit i").remove();
           });
           //$("#PUID1").selected({ btnWidth: 185, maxHeight: 200, btnSize: 'sm', btnStyle: 'secondary' });
       });
//加载数据
function loeadData() {
    $.post("/Foundation/Home/ProductUnitPage", {}, function (data) {
        $("#uintbody tr").remove();
        $(data).each(function (k, v) {
            var tr = $("<tr ondblclick='_editunit(\"" + v.PUID + "\",\"" + v.PUName + "\")'></tr>");
            var th1 = $("<td>" + v.PUID + "</td>")
            var th2 = $("<td>" + v.PUName + "</td>")
            var th3 = $("<th> <button class='am-btn am-btn-default am-btn-xs am-text-secondary' onclick='_editunit(\"" + v.PUID + "\",\"" + v.PUName + "\")'><span class='am-icon-pencil-square-o'></span> 编辑</button> <button class='am-btn am-btn-default am-btn-xs am-text-danger' onclick='unit_del(\"" + v.PUID + "\")' ><span class='am-icon-trash-o'></span> 删除</button></th>")
            tr.append(th1).append(th2).append(th3);
            $("#uintbody").append(tr);
            //$("<option value='" + v.PUID + "'>" + v.PUName + "</option>").appendTo("#PUID1");
        });

    });
    $.post("/Foundation/Home/ProductSpecPage", {}, function (data) {
        $("#specbody tr").remove();
        $(data).each(function (k, v) {
            var tr = $("<tr ondblclick='_editspec(\"" + v.PSID + "\",\"" + v.PSName + "\")'></tr>");
            var th1 = $("<td>" + v.PSID + "</td>")
            var th2 = $("<td>" + v.PSName + "</td>")
            var th3 = $("<th><button class='am-btn am-btn-default am-btn-xs am-text-secondary' onclick='_editspec(\"" + v.PSID + "\",\"" + v.PSName + "\")'><span class='am-icon-pencil-square-o'></span> 编辑</button> <button class='am-btn am-btn-default am-btn-xs am-text-danger' onclick='spec_del(\"" + v.PSID + "\")' ><span class='am-icon-trash-o'></span> 删除</button></th>");
            tr.append(th1).append(th2).append(th3);
            $("#specbody").append(tr);
        });

    });
    $.post("/Foundation/Home/ProductColorPage", {}, function (data) {
        $("#colorbody tr").remove();
        $(data).each(function (k, v) {
            var tr = $("<tr ondblclick='_editcolor(\"" + v.PCID + "\",\"" + v.PCName + "\")'></tr>");
            var th1 = $("<td>" + v.PCID + "</td>")
            var th2 = $("<td>" + v.PCName + "</td>")
            var th3 = $("<th><button class='am-btn am-btn-default am-btn-xs am-text-secondary' onclick='_editcolor(\"" + v.PCID + "\",\"" + v.PCName + "\")'><span class='am-icon-pencil-square-o'></span> 编辑</button> <button class='am-btn am-btn-default am-btn-xs am-text-danger' onclick='color_del(\"" + v.PCID + "\")'><span class='am-icon-trash-o'></span> 删除</button></th>")
            tr.append(th1).append(th2).append(th3);
            $("#colorbody").append(tr);
        });

    });
}
//商品单位修改赋值
function _editunit(PUID, PUName) {
    $("#modaltitle").text("修改商品单位");
    $("#PUID").val("");
    $("#PUID").val(PUID);
    $("#PUName").val("");
    $("#PUName").val(PUName);
    $("#spec").hide();
    $("#color").hide();
    $("#unit").show();
    $("#btn_unit").val("修改");
    $("#Promodal").modal({ closeViaDimmer: 0, width: 400, height: 160 });
}
function _editspec(PSID, PSName) {
    $("#modaltitle").text("修改商品单位");
    $("#PSID").val("");
    $("#PSID").val(PSID);
    $("#PSName").val("");
    $("#PSName").val(PSName);
    $("#unit").hide();
    $("#color").hide();
    $("#spec").show();
    $("#btn_spec").val("修改");
    $("#Promodal").modal({ closeViaDimmer: 0, width: 400, height: 160 });
}
function _editcolor(PCID, PCName) {
    $("#modaltitle").text("修改商品颜色");
    $("#PCID").val("");
    $("#PCID").val(PCID);
    $("#PCName").val("");
    $("#PCName").val(PCName);
    $("#unit").hide();
    $("#spec").hide();
    $("#color").show();
    $("#btn_color").val("修改");
    $("#Promodal").modal({ closeViaDimmer: 0, width: 400, height: 160 });
}
function unit_del(PUID) {
    if (confirm("是否删除？")) {
        $.post("/Foundation/Home/DelProductUnit", { id: PUID }, function (data) {
            if (data == "del_yes") {
                new tmm_alert1("del-1", "提示", "删除成功！");
                $("#del-1").modal();
                loeadData();
            } else {
                new tmm_alert("del-1", "提示", "删除失败！");
                $("#del-1").modal();
            }
        });
    }
}
function spec_del(PSID) {
    if (confirm("是否删除？")) {
        $.post("/Foundation/Home/DelProductSpec", { id: PSID }, function (data) {
            if (data == "del_yes") {
                new tmm_alert1("del-1", "提示", "删除成功！");
                $("#del-1").modal();
                loeadData();
            } else {
                new tmm_alert("del-1", "提示", "删除失败！");
                $("#del-1").modal();
            }
        });
    }
}
function color_del(PCID) {
    if (confirm("是否删除？")) {
        $.post("/Foundation/Home/DelProductColor", { id: PCID }, function (data) {
            if (data == "del_yes") {
                new tmm_alert1("del-1", "提示", "删除成功！");
                $("#del-1").modal();
                loeadData();
            } else {
                new tmm_alert("del-1", "提示", "删除失败！");
                $("#del-1").modal();
            }
        });
    }
}
//商品
$(function () {
    $("#tree").delegate("span", "click", function () {
        PageIndex = 1;
        var key = $(this).parent().attr("key");
        //alert(key);
        $.post("/Foundation/Home/ProductsGetByPTID", { id: key, PageIndex: PageIndex }, function (data) {
            loeadprodata(data);
            $("#SelPage option").remove();
            $.post("/Foundation/Home/ProductsGetByPTIDCount", { id: key }, function (dd) {
                for (var i = 0; i < dd.Page; i++) {
                    $("<option value='"+(i+1)+"'>" + (i+1 )+ "/" + dd.Page + "</option>").appendTo("#SelPage");
                }
            });
                   
        });
    });
    //表单按键事件
    $("#ProName").keyup(checkform);
    $("#ProTM").keyup(checkform);
    $("#ProJP").keyup(checkform);
    $("#ProTM").keyup(checkform);
    $("#ProWorkShop").keyup(checkform);
    $("#ProMax").keyup(checkform);
    $("#ProMin").keyup(checkform);
    $("#ProInPrice").keyup(checkform);
    $("#ProPrice").keyup(checkform);
    //删除商品
    $("#probody").delegate("button[del]", "click", function () {
        if (confirm("确认要删除吗？")) {
            var key = $(this).attr("key");
            $.post("/Foundation/Home/DelProducts", { id: key }, function (data) {
                if (data == "del_yes") {
                    new tmm_alert1("del_yes", "提示", "删除成功！");
                    propage();
                    $("#del_yes").modal({ width: 350 });
                } else if (data == "del_no") {
                    new tmm_alert("del_no", "提示", "删除失败！！！");
                    $("#del_no").modal({width:350});
                }
            });
        }
    });
    //修改商品
    $("#probody").delegate("tr", "dblclick", function () {
        recheckform();
        $("#protitle").text("修改商品信息");
        $("#btn_product").val("修改");
        var key = $(this).attr("key");
        $("#ProID").val(key);
        $.post("/Foundation/Home/ProductsGetByID", { id: key }, function (data) {
            if (data != "notfind") {
                      
                //绑定下拉列表
                $("select option[value=" + data.PTID + "]").attr("selected", true);
                $("#PUID1 option[value=" + data.PUID + "]").attr("selected", true);
                $("#PCID1 option[value=" + data.PCID + "]").attr("selected", true);
                $("#PSID1 option[value=" + data.PSID + "]").attr("selected", true);
                $("#DepotID1 option[value=" + data.DepotID + "]").attr("selected", true);
                $("#ProName").val(data.ProName);
                $("#ProJP").val(data.ProJP);
                $("#ProTM").val(data.ProTM);
                $("#ProWorkShop").val(data.ProWorkShop);
                $("#ProMax").val(data.ProMax);
                $("#ProMin").val(data.ProMin);
                $("#ProInPrice").val(data.ProInPrice);
                $("#ProPrice").val(data.ProPrice);
                $("#ProDesc").text(data.ProDesc);
                $("#promodals").modal({ closeViaDimmer: 0, width: 620 });
            } else {
                alert("未知错误" + data);
            }
        });
    });
    $("#probody").delegate("button[edit]", "click", function () {
        recheckform();
        $("#protitle").text("修改商品信息");
        $("#btn_product").val("修改");
        var key = $(this).attr("key");
        $("#ProID").val(key);
        $.post("/Foundation/Home/ProductsGetByID", { id: key }, function (data) {
            if (data != "notfind") {
                $("#PTID1 option[value=" + data.PTID + "]").attr("selected", true);
                $("#PUID1 option[value=" + data.PUID + "]").attr("selected", true);
                $("#PCID1 option[value=" + data.PCID + "]").attr("selected", true);
                $("#PSID1 option[value=" + data.PSID + "]").attr("selected", true);
                $("#DepotID1 option[value=" + data.DepotID + "]").attr("selected", true);
                $("#ProName").val(data.ProName);
                $("#ProJP").val(data.ProJP);
                $("#ProTM").val(data.ProTM);
                $("#ProWorkShop").val(data.ProWorkShop);
                $("#ProMax").val(data.ProMax);
                $("#ProMin").val(data.ProMin);
                $("#ProInPrice").val(data.ProInPrice);
                $("#ProPrice").val(data.ProPrice);
                $("#ProDesc").text(data.ProDesc);
                $("#promodals").modal({ closeViaDimmer: 0 });
            } else {
                alert("未知错误" + data);
            }
        });
    });
    //添加商品
    $("#btn_product").click(function () {
        var ProID = $("#ProID").val();
        var PTID = $("#PTID1 option:selected").val();
        var PUID = $("#PUID1 option:selected").val();
        var PCID = $("#PCID1 option:selected").val();
        var PSID = $("#PSID1 option:selected").val();
        var DepotID = $("#DepotID1 option:selected").val();
        var ProName = $("#ProName").val();
        var ProJP = $("#ProJP").val();
        var ProTM = $("#ProTM").val();
        var ProWorkShop = $("#ProWorkShop").val();
        var ProMax = $("#ProMax").val();
        var ProMin = $("#ProMin").val();
        var ProInPrice = $("#ProInPrice").val();
        var ProPrice = $("#ProPrice").val();
        var ProDesc = $("#ProDesc").text();
        if (checkform()) {
            var Pro = { ProID: ProID, PTID: PTID, PUID: PUID, PCID: PCID, PSID: PSID, DepotID: DepotID, ProName: ProName, ProJP: ProJP, ProTM: ProTM, ProWorkShop: ProWorkShop, ProMax: ProMax, ProMin: ProMin, ProInPrice: ProInPrice, ProPrice: ProPrice, ProDesc: ProDesc };
            var type = $(this).val();
            if (type == "添加") {
                $.post("/Foundation/Home/AddProducts", Pro, function (data) {
                    if (data) {
                        if (data == "add_yes")
                            propage();
                        $("#promodals").modal("close");
                        new tmm_alert1("add-yes", "提示", "添加成功！");
                        $("#add-yes").modal();
                    } else {
                        new tmm_alert("add-no", "提示", "未知错误，添加失败！");
                        $("#add-no").modal();
                    }
                });
            } else if (type == "修改") {
                $.post("/Foundation/Home/EditProducts", Pro, function (data) {
                    if (data) {
                        if (data == "edit_yes")
                            propage();
                        $("#promodals").modal("close");
                        new tmm_alert1("edit-yes", "提示", "修改成功！");
                        $("#edit-yes").modal();
                    } else {
                        new tmm_alert("edit-no", "提示", "未知错误，修改失败！");
                        $("#edit-no").modal();
                    }
                });
            } else {
                alert("未知错误");
            }
        }
    });
    $("#addshow").click(function () {
        recheckform();
        $("#btn_product").val("添加");
        $("ProID").val("");
        document.getElementById("form_products").reset();
        $("#promodals").modal({ closeViaDimmer: 0,width:620 });
    });
    new ProType();
    MaxPageIndex = $("#MaxPageIndex").val();
    propage();

});
var PageIndex = 1;
var MaxPageIndex = 0;
//显示商品数据
function propage() {
    $("#SelPage option[value=" + PageIndex + "]").prop("selected", true);
           
    $.post("/Foundation/Home/ProductsPage", { PageIndex: PageIndex }, function (data) {
        if (data) {
            loeadprodata(data);
        }
    });
}
//分页查询
function shouye() {
    PageIndex = 1;
    propage();
}
function weiye() {
    PageIndex = MaxPageIndex;
    propage();
}
function xiayiye() {
    PageIndex = PageIndex < MaxPageIndex ? PageIndex + 1 : PageIndex;
    propage();
}
function shangyiye() {
    PageIndex = PageIndex > 1 ? PageIndex - 1 : PageIndex;
    propage();
}
function change(obj) {
    PageIndex = parseInt(obj.value);
    propage();
}
function checkform() {
    var ProName = $("#ProName").val();
    var ProJP = $("#ProJP").val();
    var ProTM = $("#ProTM").val();
    var ProWorkShop = $("#ProWorkShop").val();
    var ProMax = $("#ProMax").val();
    var ProMin = $("#ProMin").val();
    var ProInPrice = $("#ProInPrice").val();
    var ProPrice = $("#ProPrice").val();
    var ProDesc = $("#ProDesc").text();
    if (ProName.trim().length == 0 || ProName == "Undefined") {
        $("#ProName").parent().addClass("am-form-error");
        return false;
    } else {
        $("#ProName").parent().removeClass("am-form-error");
    }
    if (ProTM.length == 0 || ProTM == "Undefined") {
        $("#ProTM").parent().addClass("am-form-error");
        return false;
    } else {
        $("#ProTM").parent().removeClass("am-form-error");
    }
    if (ProJP.length == 0 || ProJP == "Undefined") {
        $("#ProJP").parent().addClass("am-form-error");
        return false;
    } else {
        $("#ProJP").parent().removeClass("am-form-error");
    }
    if (ProTM.length == 0 || ProTM == "Undefined") {
        $("#ProTM").parent().addClass("am-form-error");
        return false;
    } else {
        $("#ProTM").parent().removeClass("am-form-error");
    }
    if (ProWorkShop.length == 0 || ProWorkShop == "Undefined") {
        $("#ProWorkShop").parent().addClass("am-form-error");
        return false;
    } else {
        $("#ProWorkShop").parent().removeClass("am-form-error");
    }
    if (ProMax.length == 0 || ProMax == "Undefined") {
        $("#ProMax").parent().addClass("am-form-error");
        return false;
    } else {
        $("#ProMax").parent().removeClass("am-form-error");
    }
    if (ProMin.length == 0 || ProMin == "Undefined") {
        $("#ProMin").parent().addClass("am-form-error");
        return false;
    } else {
        $("#ProMin").parent().removeClass("am-form-error");
    }
    if (ProInPrice.length == 0 || ProInPrice == "Undefined") {
        $("#ProInPrice").parent().addClass("am-form-error");
        return false;
    } else {
        $("#ProInPrice").parent().removeClass("am-form-error");
    }
    if (ProPrice.length == 0 || ProPrice == "Undefined") {
        $("#ProPrice").parent().addClass("am-form-error");
        return false;
    } else {
        $("#ProPrice").parent().removeClass("am-form-error");
    }
    return true;
}
//重置表单状态
function recheckform() {
    $("#ProName").parent().removeClass("am-form-error");
    $("#ProTM").parent().removeClass("am-form-error");
    $("#ProJP").parent().removeClass("am-form-error");
    $("#ProTM").parent().removeClass("am-form-error");
    $("#ProWorkShop").parent().removeClass("am-form-error");
    $("#ProMax").parent().removeClass("am-form-error");
    $("#ProMin").parent().removeClass("am-form-error");
    $("#ProInPrice").parent().removeClass("am-form-error");
    $("#ProPrice").parent().removeClass("am-form-error");
}
function loeadprodata(data) {
    $("#probody tr").remove();
    $(data).each(function (k, v) {
        var tr = $("<tr key=" + v.ProID + "></tr>");
        var th1 = $("<th>" + v.ProID + "</th>");
        var th2 = $("<th>" + v.PTName + "</th>");
        var th3 = $("<th>" + v.ProName + "</th>");
        var th4 = $("<th>" + v.PUName + "</th>");
        var th5 = $("<th>" + v.PCName + "</th>");
        var th6 = $("<th>" + v.PSName + "</th>");
        var th7 = $("<th>" + v.ProJP + "</th>");
        var th8 = $("<th>" + v.ProTM + "</th>");
        var th9 = $("<th>" + v.ProWorkShop + "</th>");
        var th10 = $("<th> <button class='am-btn am-btn-default am-btn-xs am-text-secondary'  style='font-size:9px;' edit='edit' key=" + v.ProID + " ><span class='am-icon-pencil-square-o' ></span> 编辑</button> </th>");//<button del='del' class='am-btn am-btn-default am-btn-xs am-text-danger' key=" + v.ProID + " ><span class='am-icon-trash-o'></span> 删除</button>
        tr.append(th1).append(th2).append(th3).append(th4).append(th5).append(th6).append(th7).append(th8).append(th9).append(th10);
        $("#probody").append(tr);
    });
}
