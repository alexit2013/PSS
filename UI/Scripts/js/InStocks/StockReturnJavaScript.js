
var nowtr = null;//当前行
var PageIndex = 1;
var PageIndex1 = 1;
var MaxPageIndex = 0;
var addCl = 0;
$(function () {
    $("#tab_sotck_bd").delegate("tr", "click", function () {
        $("#tab_sotck_bd tr").removeClass("ck");
        $(this).addClass("ck");
        var id = $(this).children("td")[0].innerHTML;
        $.post("/InStock/StockReturn/GetStockReturnDetailByQPID", { id: id }, function (data) {
            if (data) {
                $("#tab_cusx_bd tr").remove();
                $(data).each(function (k, v) {
                    var tr = $("<tr></tr>");
                    var td1 = $("<td>" + v.ProName + "</td>");
                    var td2 = $("<td>" + v.KRDPrice + "</td>");
                    var td3 = $("<td>" + v.KRDAmount + "</td>");
                    var td7 = $("<td><i style='color:green;cursor:pointer;font-size:16px;' class='am-icon-comment'  title='" + v.KRDDesc + "'></i></td>");
                    tr.append(td1).append(td2).append(td3).append(td7);
                    $("#tab_cusx_bd").append(tr);
                });
            }
        });
    });
    MaxPageIndex = $("#MaxPageIndex").val();
    MaxPageIndex1 = $("#MaxPageIndex1").val();
    Page1();
    $("#brn_search1").click(function () {
        $(this).addClass("am-animation-scale-down");
        PageIndex1 = 1;
        Page1();

    });
    //打开报价单窗体
            
    $("#KRID").dblclick(function () {
        $("#select_modal").modal({ closeViaDimmer: 0, width: 1100, height: 550 });
    });
    //查询
    $("#brn_search").click(function () {
        var brn_search = $(this);
        $(this).addClass("am-animation-scale-down");
        PageIndex = 1;
        Page();

    });

    //转换为添加
    $("#ConvertAdd").click(function () {
        document.getElementById("form_order").reset();
        $("#orderbody tr").remove();
        $("#btn_addorder").val("保存");
        getid();
    });
    //双击选择报价单
    $("#probodyed").delegate("tr", "dblclick", function () {
        var SIDID = $(this).attr("SIDID");
        var PPID = $(this).attr("PPID");
        var DepotID = $(this).attr("DepotID");
        $("#DepotID>option[value='" + DepotID + "']").attr("selected", true);
        $("#PPID").val(PPID);
        $("#SIDID").val(SIDID);
        $("#KRDate").val(getNowDate_());
        $.post("/InStock/StockInDepot/GetStockInDepotDetailByStockID", { id: SIDID }, function (data) {
            $("#orderbody tr").remove();
            if (data) {
                $(data).each(function (k, v) {
                    $("#orderbody tr").remove();
                    if (data) {
                        $(data).each(function (k, v) {
                            var newtr = $('<tr><td align="center">' + v.ProID + '</td><td>' + v.ProName + '</td><td>' + v.PUName + '</td><td>' + v.PSName + '</td><td>' + v.PCName + '</td><td ><input  type="number" name="procount" value="' + v.SIDAmount + '" placeholder="请输入商品数量" style="width:100px;font-size:11px;" required/></td><td><input type="number" value="' + v.SIDPrice + '"  name="proprice" readonly  placeholder="请输入商品单价" style="width:100px;font-size:11px;" required /></td><td>' + (v.SIDAmount * v.SIDPrice).toFixed(2) + '</td><td><input class="am-form-field" style="width:80px;font-size:11px;border-color:red;"type="number"  value="' + v.SIDAmount + '"  placeholder="退货数量" required/></td><td><input type="text" value="' + v.SIDDesc + '" placeholder="备注"  style="width:120px;font-size:11px;"  /></td><td align="center"><a class="am-icon-remove" title="删除" href="#" style="font-size:18px;color:red;"></a></td></tr>');
                            $("#orderbody").append(newtr);
                        });
                    }
                });
                getid();
                $("#select_modal").modal("close");
            }
        });
    });
    //加载所有的采购单信息
    Page();
    //添加订单
    $("#btn_addorder").click(function () {
        var list = [];
        var KRID = $("#KRID").val();
        var PPID = $("#PPID").val();
        var SIDID = $("#SIDID").val();
        var DepotID = $("#DepotID option:selected").val();
        var KRDate = $("#KRDate").val();
        var KRDesc = $("#KRDesc").text();
        $("#orderbody tr").each(function (k, v) {
            var tdid = $(v).children("td")[0].innerHTML;
            if (!isNaN(tdid)) {
                var KRDAmount = $($(v).children("td")[8]).children("input")[0].value;//数量
                var KRDPrice = $($(v).children("td")[6]).children("input")[0].value;//价格
                var KRDDesc = $($(v).children("td")[9]).children("input")[0].value;//备注
                var st = { KRID: KRID, ProID: tdid, KRDAmount: KRDAmount, KRDPrice: KRDPrice, KRDDesc: KRDDesc };
                list.push(st);
            }
        });
        var stock = { KRID: KRID, PPID: PPID, SIDID: SIDID, DepotID: DepotID,KRDate:KRDate, KRState: 0, KRDesc: KRDesc };
        //alert(JSON.stringify(list));
        //alert(JSON.stringify(stock));
        var type = $(this).val();
        if (type == "保存") {
            $.post("/InStock/StockReturn/AddStockReturn", { qp: stock, list: list }, function (data) {
                if (data == "add_yes") {
                    new tmm_alert1("add_modal", "提示", "添加成功");
                    document.getElementById("form_order").reset();
                    $("#orderbody tr").remove();
                    $("#btn_addorder").val("保存");
                    getid();
                } else if (data == "add_no") {
                    new tmm_alert("add_modal", "提示", "添加失败");
                } else {
                    new tmm_alert("add_modal", "提示", "未知错误，添加失败");
                }
                $("#add_modal").modal({ closeViaDimmer: 0 });
            });
        } else if (type == "修改") {
            $.post("/InStock/StockReturn/EditStockReturn", { qp: stock, list: list }, function (data) {
                if (data == "edit_yes") {
                    new tmm_alert1("edit_modal", "提示", "修改成功");
                    $("#btn_addorder").val("保存");
                    $("#tab_2").removeClass("am-active");
                    $("#tab_1").addClass("am-active");
                    $("#tab2").removeClass("am-fade").removeClass("am-in").removeClass("am-active");
                    $("#tab1").addClass("am-fade").addClass("am-in").addClass("am-active");
                    document.getElementById("form_order").reset();
                    $("#orderbody tr").remove();
                    Page1();
                } else if (data == "edit_no") {
                    new tmm_alert("edit_modal", "提示", "修改失败");
                } else {
                    new tmm_alert("edit_modal", "提示", "未知错误，添加失败");
                }
                $("#edit_modal").modal({ closeViaDimmer: 0 });
            });
        }

    });
    //算取总金额
    $("#orderbody").delegate("input[name=procount]", "keyup", function () {
        var num1 = $(this);
        if (isNaN(num1.val()) || addCl == undefined) {
            return;
        } else {
            var tr = num1.parent().parent();
            tr.children("td")[9].innerHTML = (parseFloat(num1.val()) * parseFloat(tr.find("input")[1].value) - (parseFloat(num1.val()) * parseFloat(tr.find("input")[1].value) / addCl)).toFixed(2);
        }


    });
    $("#orderbody").delegate("input[name=proprice]", "keyup", function () {
        var num1 = $(this);
        if (isNaN(num1.val()) || addCl == undefined) {
            return;
        } else {
            var tr = num1.parent().parent();
            tr.children("td")[9].innerHTML = (parseFloat(num1.val()) * parseFloat(tr.find("input")[0].value) - (parseFloat(num1.val()) * parseFloat(tr.find("input")[0].value) / addCl)).toFixed(2);
            tr.children("td")[8].innerHTML = (parseFloat(num1.val()) - parseFloat(num1.val()) / addCl).toFixed(2);
        }

    });
    //点击选择商品
    $("#probody").delegate("tr", "dblclick", function () {
        var id = $(this).children("td")[0].innerHTML;
        var trs = $("#orderbody tr");
        var fg = true;
        $(trs).each(function (k, v) {
            var pid = $(v).children("td")[0].innerText;
            if (id == parseInt(pid)) {
                new tmm_alert("addpro_no", "提示", "该商品已经存在，请勿重复添加！！！");
                $("#addpro_no").modal();
                fg = false;
            }
        });
        if (fg) {
            nowtr.children("td")[0].innerHTML = id;
            nowtr.children("td")[1].innerHTML = $(this).children("td")[1].innerHTML;
            nowtr.children("td")[2].innerHTML = $(this).children("td")[2].innerHTML;
            nowtr.children("td")[3].innerHTML = $(this).children("td")[3].innerHTML;
            nowtr.children("td")[4].innerHTML = $(this).children("td")[4].innerHTML;
            var price = $(this).children("td")[5].innerHTML;
            nowtr.children("td")[6].innerHTML = '<input type="number"  name="proprice" value="' + price + '"  placeholder="请输入商品单价" style="width:100px;font-size:11px;" required />';
            if (addCl != undefined) {
                nowtr.children("td")[7].innerHTML = addCl + "%";
            }
            if (addCl != undefined) {
                nowtr.children("td")[8].innerHTML = (price - (price / addCl)).toFixed(2);
            } else {
                nowtr.children("td")[8].innerHTML = price;
            }

            $("#prodrown").hide(300);
        }
    });
    //点击出现商品列表
    $("table").delegate("button[name=showpro]", "click", function () {
        nowtr = $(this).parent().parent();
        var $prodrown = $("#prodrown");
        //alert(event.clientX + "--------" + event.clientY);
        //alert($(document).scrollTop());
        $prodrown.css("top", $(document).scrollTop() + event.clientY - 85 + "px");
        $prodrown.show(300);
    });
    //加载所有商品
    $.post("/InStock/StockInDepot/GetProductsAll", {}, function (data) {
        if (data) {
            $("#probody tr").remove();
            $(data).each(function (k, v) {
                //<tr><td>商品编号</td><td>商品名称</td><td>单位</td><td>规格</td><td>颜色</td></tr>
                var tr = $("<tr></tr>");
                var td1 = $("<td>" + v.ProID + "</td>");
                var td2 = $("<td>" + v.ProName + "</td>");
                var td3 = $("<td>" + v.PUName + "</td>");
                var td4 = $("<td>" + v.PSName + "</td>");
                var td5 = $("<td>" + v.PCName + "</td>");
                var td6 = $("<td>" + v.ProInPrice + "</td>");
                tr.append(td1).append(td2).append(td3).append(td4).append(td5).append(td6);
                $("#probody").append(tr);
            });
        }
    });
    
    //删除行
    $("table").delegate("a[title=删除]", "click", function (e) {
        // alert(event.clientX+"--------"+event.clientY);
        var tr = $(this).parent().parent();
        tr.remove();
    });
});
//
function Page1() {
    var KRID = $("#KRID1").val();
    var PPID = $("#PPID1 option:selected").val();
    var DepotID = $("#DepotID1").val();
    var KRDate = $("#RKDate1").val();
    var UsersName = $("#UsersName1").val();
    var SIDID = $("#SIDID1").val();
    var KRState = $("input[name=RKState1]:checked").val();
    var obj = { KRID: KRID, UsersName: UsersName, PPID: PPID, SIDID: SIDID, DepotID: DepotID, KRDate: KRDate, KRState: KRState, PageIndex: PageIndex1 };
      //alert(JSON.stringify(obj));
      $.post("/InStock/StockReturn/Find", obj, function (data) {
        if (data) {
            $("#SelPage1 option").remove();
            $("#tab_sotck_bd tr").remove();
            if (data[0].KRID == "") {
                return;
            }
            MaxPageIndex1 = data[0].MaxPageIndex;
            for (var i = 1; i <= MaxPageIndex1; i++) {
                $("<option value='" + i + "'>" + i + "/" + MaxPageIndex1 + "</option>").appendTo("#SelPage1");
            }
            setPage1(data);
            $("#brn_search1").removeClass("am-animation-scale-down");
        }
    });
}
function setPage1(data) {
    if (data[0].KRID == "") {
        return;
    }
    $("#SelPage1 option[value=" + PageIndex1 + "]").prop("selected", true);
    $(data).each(function (k, v) {
        var tr = $("<tr></tr>");
        var td1 = $("<td>" + v.KRID + "</td>");
        var td2 = $("<td>" + v.PPName + "</td>");
        var td3 = $("<td>" + v.DepotName + "</td>");
        var td4 = $("<td></td>");
        if (v.KRDate != "null" && v.KRDate != "undefined") {
            td4.text(ConvertTime(v.KRDate));
        }
        var td5 = $("<td>" + v.UsersName + "</td>");
        var td6 = $("<td align='center' ></td>");
        if (+v.KRState == "0")
            td6.html("<font style='color:red'>未审核</font>");
        else
            td6.html("<font style='color:green'>已审核</font>");

        var td7 = $("<td ><i style='color:green;cursor:pointer;font-size:16px;' class='am-icon-comment'  title='" + v.KRDesc + "'></i></td>");
        var td8 = $("<td align='center'><button style='font-size:9px;' class='am-btn am-btn-warning am-radius   am-btn-xs '  " + (v.KRState == "1" ? "disabled='disabled'" : "") + "  onclick='orderedit(\"" + v.KRID + "\",\"" + v.PPID + "\",\"" + v.SIDID + "\",\"" + v.DepotID + "\",\"" + v.KRDate + "\",\"" + v.KRDesc + "\")'><span class='am-icon-pencil-square-o'></span> 编辑</button><button style='font-size:9px;margin-left:3px;' class='am-btn am-btn-success am-radius am-btn-xs' " + (v.KRState == "1" ? "disabled='disabled'" : "") + "  onclick='order_ck(\"" + v.KRID + "\")' ><span class='am-icon-check'></span> 审核</button> <button style='font-size:9px;' class='am-btn am-btn-danger am-radius am-btn-xs' " + (v.KRState == "1" ? "disabled='disabled'" : "") + " onclick='order_del(\"" + v.KRID + "\")' ><span class='am-icon-trash-o'></span> 删除</button></td>");
        tr.append(td1).append(td2).append(td3).append(td4).append(td5).append(td6).append(td7).append(td8);
        $("#tab_sotck_bd").append(tr);
    });
}


///加载采购订单数据【分页】
function Page() {
    var SIDID = $("#SIDID2").val();
    var PPID = $("#PPID2 option:selected").val();
    var DepotID = $("#DepotID2 option:selected").val();
    var SIDDate = $("#SIDDate2").val();
    var UsersName = $("#UsersName2").val();
    var SIDData = 1;
    var obj = { PageIndex: PageIndex, SIDID: SIDID, UsersName: UsersName, PPID: PPID, DepotID: DepotID, SIDDate: SIDDate, SIDData: SIDData };
    $.post("/InStock/StockInDepot/FindStockIn", obj, function (data) {
        if (data) {
            $("#SelPage option").remove();
            $("#probodyed tr").remove();
            if (data[0].SIDID == "") {
                return;
            }
            MaxPageIndex = data[0].MaxPageIndex;
            for (var i = 1; i <= MaxPageIndex; i++) {
                $("<option value='" + i + "'>" + i + "/" + MaxPageIndex + "</option>").appendTo("#SelPage");
            }
            //alert(data);
            setPage(data);//绑定数据
            $("#brn_search").removeClass("am-animation-scale-down");
        }
    });
}
function setPage(data) {
    if (data[0].SIDID == "") {
        return;
    }
    $("#SelPage option[value=" + PageIndex + "]").prop("selected", true);
    $(data).each(function (k, v) {
        var tr = $("<tr SIDID='" + v.SIDID + "' PPID='" + v.PPID + "' PPID='" + v.PPID + "'  StockID='" + v.StockID + "'  SIDDate='" + v.SIDDate + "'></tr>");
        var td1 = $("<td>" + v.SIDID + "</td>");
        var td2 = $("<td>" + v.PPName + "</td>");
        var td3 = $("<td>" + v.DepotName + "</td>");
        var td4 = $("<td></td>");
        if (v.SIDDate != "null" && v.SIDDate != "undefined") {
            td4.text(ConvertTime(v.SIDDate));
        }
        var td4 = $("<td>" + v.UsersName + "</td>");
        var td6 = $("<td ><i style='color:green;cursor:pointer;font-size:16px;' class='am-icon-comment'  title='" + v.SIDDesc + "'></i></td>");
        tr.append(td1).append(td2).append(td3).append(td4).append(td6);
        $("#probodyed").append(tr);
    });
}

//删除采购单
function order_del(id) {
    new tmm_confirm("del", "提示", "确认要删除吗？", function () {
        $.post("/InStock/StockReturn/DelStockReturn", { id: id }, function (data) {
            if (data == "del_yes") {
                new tmm_alert1("atdel", "提示", "删除成功！");
                Page1();
                $("#probodyed tr").remove();
            } else {
                new tmm_alert("atdel", "提示", "删除失败！");
            }
            $("#atdel").modal({ closeViaDimmer: 0, width: 350 });
        });
        $("#del").remove();
    }, function () {
        $("#del").remove();
    });
    $("#del").modal({ closeViaDimmer: 0, width: 350 });

}
//编辑入库单
function orderedit(KRID, PPID, SIDID, DepotID, KRDate, KRDesc) {
    $("#btn_addorder").val("修改");
    $("#tab_1").removeClass("am-active");
    $("#tab_2").addClass("am-active");
    $("#tab1").removeClass("am-fade").removeClass("am-in").removeClass("am-active");
    $("#tab2").addClass("am-fade").addClass("am-in").addClass("am-active");
    //数据绑定
    $("#KRID").val(KRID);
    $("#PPID").val(PPID);
    $("#SIDID").val(SIDID);
    $("#KRDate").val(ConvertTime_(KRDate));
    $("#KRDesc").text(KRDesc);
    $("#DepotID option[value='" + DepotID + "']").prop("selected", true);
    //显示商品信息
    $.post("/InStock/StockReturn/GetStockReturnDetailByQPID", { id: KRID }, function (data) {
        $("#orderbody tr").remove();
        if (data) {
            $(data).each(function (k, v) {

                var newtr = $('<tr><td align="center">' + v.ProID + '</td><td>' + v.ProName + '</td><td>' + v.PUName + '</td><td>' + v.PSName + '</td><td>' + v.PCName + '</td><td ><input  type="number" name="procount" value="' + v.KRDAmount + '" placeholder="请输入商品数量" style="width:100px;font-size:11px;" required/></td><td><input type="number" value="' + v.KRDPrice + '"  name="proprice"   placeholder="请输入商品单价" style="width:100px;font-size:11px;" required /></td><td>' + (v.KRDAmount * v.KRDPrice).toFixed(2) + '</td><td><input class="am-form-field" style="width:80px;font-size:11px;"type="number" value="' + v.KRDAmount + '" placeholder="退货数量" required/></td><td><input type="text" value="' + v.KRDDesc + '" placeholder="备注"  style="width:120px;font-size:11px;"  /></td><td align="center"><a class="am-icon-remove" title="删除" href="#" style="font-size:18px;"></a></td></tr>');
                $("#orderbody").append(newtr);
            });
        }
    });

}
//审核订单
function order_ck(id) {
    new tmm_confirm("ck", "提示", "确认操作？", function () {
        $.post("/InStock/StockReturn/CKStockReturn", { id: id }, function (data) {
            if (data == "ck_yes") {
                new tmm_alert1("ck1", "提示", "操作成功！");
                Page1();
            } else if (data == "ck_no") {
                new tmm_alert("ck1", "提示", "操作失败！");
            } else {
                new tmm_alert("ck1", "提示", data);
            }
            $("#ck1").modal({ closeViaDiammer: 0, width: 350 });
        });
        $("#ck").remove();
    }, function () {
        $("#ck").remove();
    });
    $("#ck").modal({ closeViaDimmer: 0, width: 350 });
}

//隐藏商品选择框
function hideproshow() {
    $("#prodrown").hide(200);
}


function shouye1() {
    PageIndex1 = 1;
    Page1();
}
function weiye1() {
    PageIndex1 = MaxPageIndex1;
    Page1();
}
function xiayiye1() {
    PageIndex1 = PageIndex1 < MaxPageIndex1 ? PageIndex1 + 1 : PageIndex1;
    Page1();
}
function shangyiye1() {
    PageIndex1 = PageIndex1 > 1 ? PageIndex1 - 1 : PageIndex1;
    Page1();
}
function change1(obj) {
    PageIndex1 = parseInt(obj.value);
    Page1();
}


function shouye() {
    PageIndex = 1;
    Page();
}
function weiye() {
    PageIndex = MaxPageIndex;
    Page();
}
function xiayiye() {
    PageIndex = PageIndex < MaxPageIndex ? PageIndex + 1 : PageIndex;
    Page();
}
function shangyiye() {
    PageIndex = PageIndex > 1 ? PageIndex - 1 : PageIndex;
    Page();
}
function change(obj) {
    PageIndex = parseInt(obj.value);
    Page();
}
//获取ID
function getid() {
    $.post("/InStock/StockReturn/GetStockReturnID", {}, function (data) {
        if (data) {
            $("#KRID").val(data);
        } else {
            $("#KRID").val("CT201501010001");
        }
    });
}
//选择客户时
function SelectCus() {
    addCl = $("#CusID option:selected").attr("cl");
    if (addCl != undefined) {

        $("#orderbody tr").each(function (k, v) {
            $(v).children("td")[7].innerHTML = addCl + "%";
            var price = parseFloat($($(v).children("td")[6]).children("input")[0].value);
            $(v).children("td")[8].innerHTML = (price - (price / addCl)).toFixed(2);
            $(v).children("td")[9].innerHTML = ($($(v).children("td")[5]).children("input")[0].value * (price - (price / addCl))).toFixed(2);
        });

    }

}