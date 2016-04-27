
var nowtr = null;//当前行
var PageIndex = 1;
var PageIndex1 = 1;
var MaxPageIndex = 0;
var addCl = 0;
$(function () {
    getid();
    //查询
    $("#brn_search").click(function () {
        var brn_search = $(this);
        $(this).addClass("am-animation-scale-down");
        PageIndex = 1;
        Page();

    });
    MaxPageIndex = $("#MaxPageIndex").val();
    //转换为添加
    $("#ConvertAdd").click(function () {
        document.getElementById("form_order").reset();
        $("#orderbody tr").remove();
        $("#btn_addorder").val("保存");
        getid();
    });
    //采购订单行点击事件
    $("#probodyed").delegate("tr", "click", function () {
        $("#probodyed tr").removeClass("ck");
        $(this).addClass("ck");
        var stockid = $(this).children("td")[0].innerHTML;
        $.post("/Stock/StockAllot/GetDeteilByID", { id: stockid }, function (data) {
            if (data) {
                $("#stockbodyed tr").remove();
                $(data).each(function (k, v) {
                    var tr = $("<tr></tr>");
                    var td1 = $("<td>" + v.ProName + "</td>");
                    var td3 = $("<td>" + v.DevDAmount + "</td>");
                    var td4 = $("<td><i style='color:green;cursor:pointer;font-size:16px;' class='am-icon-comment'  title='" + v.DevDDesc + "'></i></td>");
                    tr.append(td1).append(td3).append(td4);
                    $("#stockbodyed").append(tr);
                });
            }
        });
    });
    //加载所有的采购单信息
    Page();
    //添加订单
    $("#btn_addorder").click(function () {
        var list = [];
        var DevID = $("#DevID").val();
        var DevOutID = $("#DevOutID option:selected").val();
        var DevInID = $("#DevInID option:selected").val();
        var DevDate = $("#DevDate").val();
        var DevDesc = $("#DevDesc").text();
        if (DevOutID == DevInID) {
            new tmm_alert("ck_1", "提示", "调入仓库和调入仓库不能相同！！！");
            $("#ck_1").modal({ closeViaDimmer: 0 });
          
          //  return;
        } else {
            $("#orderbody tr").each(function (k, v) {
                var tdid = $(v).children("td")[0].innerHTML;
                if (!isNaN(tdid)) {
                    var DevDAmount = $($(v).children("td")[5]).children("input")[0].value;//数量
                    if (DevDAmount.trim() == "" || isNaN(DevDAmount)) {
                        new tmm_alert("ck_2", "提示", "请输入正确的数量！");
                        $("#ck_2").modal({ closeViaDimmer: 0 });
                        return;
                    }
                    var DevDDesc = $($(v).children("td")[8]).children("input")[0].value;//描述
                    var st = { DevID: DevID, ProID: tdid, DevDAmount: DevDAmount, DevDDesc: DevDDesc };
                    list.push(st);
                }
            });
            var stock = { DevID: DevID, DevOutID: DevOutID, DevInID: DevInID, DevDate: DevDate, DevState: 0, DevDesc: DevDesc };
            //alert(JSON.stringify(list));
            //alert(JSON.stringify(stock));
            var type = $(this).val();
            if (type == "保存") {
                $.post("/Stock/StockAllot/Add", { pd: stock, list: list }, function (data) {
                    if (data == "add_yes") {
                        new tmm_alert1("add_modal", "提示", "添加成功");
                    } else if (data == "add_no") {
                        new tmm_alert("add_modal", "提示", "添加失败");
                    } else {
                        new tmm_alert("add_modal", "提示", "未知错误，添加失败");
                    }
                    $("#add_modal").modal({ closeViaDimmer: 0 });
                });
            } else if (type == "修改") {
                $.post("/Stock/StockAllot/Edit", { pd: stock, list: list }, function (data) {
                    if (data == "edit_yes") {
                        new tmm_alert1("edit_modal", "提示", "修改成功");
                        $("#btn_addorder").val("保存");
                        
                        Page();
                    } else if (data == "edit_no") {
                        new tmm_alert("edit_modal", "提示", "修改失败");
                    } else {
                        new tmm_alert("edit_modal", "提示", "未知错误，添加失败");
                    }
                    $("#edit_modal").modal({ closeViaDimmer: 0 });
                });
            }
            $("#tab_2").removeClass("am-active");
            $("#tab_1").addClass("am-active");
            $("#tab2").removeClass("am-fade").removeClass("am-in").removeClass("am-active");
            $("#tab1").addClass("am-fade").addClass("am-in").addClass("am-active");
            document.getElementById("form_order").reset();
            $("#orderbody tr").remove();
        }
      

    });
    //算取总金额
    $("#orderbody").delegate("input[name=procount]", "keyup", function () {
        var num1 = $(this);
        if (isNaN(num1.val())) {
            return;
        } else {
            var tr = num1.parent().parent();
            tr.children("td")[7].innerHTML = (parseFloat(num1.val()) * parseFloat(tr.find("input")[1].value)).toFixed(2);
        }


    });
    $("#orderbody").delegate("input[name=proprice]", "keyup", function () {
        var num1 = $(this);
        if (isNaN(num1.val())) {
            return;
        } else {
            var tr = num1.parent().parent();
            tr.children("td")[7].innerHTML = (parseFloat(num1.val()) * parseFloat(tr.find("input")[0].value)).toFixed(2);
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
            nowtr.children("td")[7].innerHTML = (price - 0).toFixed(2);
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
    //添加行
    $("#Addtr").click(function () {
        $("#prodrown").hide(300);
        var newtr = $('<tr><td align="center"><button name="showpro" type="button" class="am-btn am-btn-primary am-dropdown-toggle am-btn-sm" style="font-size:10px;">商品 <span class="am-icon-caret-down"></span></button></td><td></td><td></td><td></td><td></td><td ><input  type="number" name="procount" placeholder="请输入商品数量" style="width:100px;font-size:11px;" required/></td><td><input type="number"  name="proprice"   placeholder="请输入商品单价"  readonly="readonly" style="width:100px;font-size:11px;" required /></td><td></td><td><input type="text" placeholder="备注"  style="width:160px;font-size:11px;"  /></td><td align="center"><a class="am-icon-remove" title="删除" href="#" style="font-size:18px;"></a></td></tr>');
        $("#orderbody").append(newtr);
    });
    //删除行
    $("table").delegate("a[title=删除]", "click", function (e) {
        // alert(event.clientX+"--------"+event.clientY);
        var tr = $(this).parent().parent();
        tr.remove();
    });
});
///加载采购订单数据【分页】
function Page() {
    var DevID = $("#DevID1").val();
    var DevOutID = $("#DevOutID1 option:selected").val();
    var DevInID = $("#DevInID1 option:selected").val();
    var DevDate = $("#DevDate1").val();
    var UsersName = $("#UsersName1").val();
    var DevState = $("input[name=DevState1]:checked").val();
    var obj = { DevID: DevID, DevOutID: DevOutID, DevInID: DevInID, UsersName: UsersName, DevDate: DevDate, DevState: DevState, PageIndex: PageIndex };
    // alert(JSON.stringify(obj));
    $.post("/Stock/StockAllot/Find", obj, function (data) {
        if (data) {
            $("#SelPage option").remove();
            $("#probodyed tr").remove();
            if (data[0].DevID == "") {
                return;
            }
            MaxPageIndex = data[0].MaxPageIndex;
            for (var i = 1; i <= MaxPageIndex; i++) {
                $("<option value='" + i + "'>" + i + "/" + MaxPageIndex + "</option>").appendTo("#SelPage");
            }
            setPage(data);
            $("#brn_search").removeClass("am-animation-scale-down");
        }
    });
}

function setPage(data) {
    if (data[0].DevID == "") {
        return;
    }
    $("#SelPage option[value=" + PageIndex + "]").prop("selected", true);
    $(data).each(function (k, v) {
        var tr = $("<tr></tr>");
        var td1 = $("<td>" + v.DevID + "</td>");
        var td2 = $("<td>" + v.OutDepotName + "</td>");
        var td3 = $("<td>" + v.InDepotName + "</td>");
        var td4 = $("<td></td>");
        if (v.DevDate != "null" && v.DevDate != "undefined") {
            td4.text(ConvertTime(v.DevDate));
        }

        var td5 = $("<td>" + v.UsersName + "</td>");
        var td6 = $("<td align='center' ></td>");
        if (+v.DevState == "0")
            td6.html("<font style='color:red'>未审核</font>");
        else
            td6.html("<font style='color:green'>已审核</font>");

        var td7 = $("<td ><i style='color:green;cursor:pointer;font-size:16px;' class='am-icon-comment'  title='" + v.DevDesc + "'></i></td>");
        var td8 = $("<td align='center'><button style='font-size:10px;' class='am-btn am-btn-warning am-radius   am-btn-xs '  " + (v.DevState == "1" ? "disabled='disabled'" : "") + "  onclick='orderedit(\"" + v.DevID + "\",\"" + v.DevOutID + "\",\"" + v.DevInID + "\",\"" + v.UserID + "\",\"" + v.DevDate + "\",\"" + v.DevState + "\",\"" + v.DevDesc + "\")'><span class='am-icon-pencil-square-o'></span> 编辑</button><button style='font-size:10px;margin-left:3px;' class='am-btn am-btn-success am-radius am-btn-xs' " + (v.DevState == "1" ? "disabled='disabled'" : "") + "  onclick='order_ck(\"" + v.DevID + "\")' ><span class='am-icon-check'></span> 审核</button> <button style='font-size:10px;' class='am-btn am-btn-danger am-radius am-btn-xs' " + (v.DevState == "1" ? "disabled='disabled'" : "") + " onclick='order_del(\"" + v.DevID + "\")' ><span class='am-icon-trash-o'></span> 删除</button></td>");
        tr.append(td1).append(td2).append(td3).append(td4).append(td5).append(td6).append(td7).append(td8);
        $("#probodyed").append(tr);
    });
}

//删除采购单
function order_del(id) {
    new tmm_confirm("del", "提示", "确认要删除吗？", function () {
        $.post("/Stock/StockAllot/Del", { id: id }, function (data) {
            if (data == "del_yes") {
                new tmm_alert1("atdel", "提示", "删除成功！");
                Page();
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
function orderedit(DevID, DevOutID, DevInID, UserID, DevDate, DevState, DevDesc) {
    $("#btn_addorder").val("修改");
    $("#tab_1").removeClass("am-active");
    $("#tab_2").addClass("am-active");
    $("#tab1").removeClass("am-fade").removeClass("am-in").removeClass("am-active");
    $("#tab2").addClass("am-fade").addClass("am-in").addClass("am-active");
    $("#DevID").val(DevID);
    $("#DevDate").val(ConvertTime_(DevDate));
    $("#DevDesc").val(DevDesc);
    $("#DevOutID option[value='" + DevOutID + "']").prop("selected", true);
    $("#DevInID option[value='" + DevInID + "']").prop("selected", true);

    //显示商品信息
    $.post("/Stock/StockAllot/GetDeteilByID", { id: DevID }, function (data) {
        $("#orderbody tr").remove();
        if (data) {
            $(data).each(function (k, v) {
                var newtr = $('<tr><td align="center">' + v.ProID + '</td><td>' + v.ProName + '</td><td>' + v.PUName + '</td><td>' + v.PSName + '</td><td>' + v.PCName + '</td><td ><input  type="number" name="procount" value="' + v.DevDAmount + '" placeholder="请输入商品数量" style="width:100px;font-size:11px;" required/></td><td><input type="number" value="' + v.ProInPrice + '"  name="proprice"   placeholder="请输入商品单价" readonly="readonly" style="width:100px;font-size:11px;" required /></td><td>' + (v.DevDAmount * v.ProInPrice).toFixed(2) + '</td><td><input type="text" value="' + v.DevDDesc + '" placeholder="备注"  style="width:160px;font-size:11px;"  /></td><td align="center"><a class="am-icon-remove" title="删除" href="#" style="font-size:18px;"></a></td></tr>');
                $("#orderbody").append(newtr);
            });
        }
    });

}
//审核订单
function order_ck(id) {
    new tmm_confirm("ck", "提示", "确认操作？", function () {
        $.post("/Stock/StockAllot/CKOtherDepot", { id: id }, function (data) {
            if (data == "ck_yes") {
                new tmm_alert1("ck1", "提示", "操作成功！");
                Page();
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
    $.post("/Stock/StockAllot/GetID", {}, function (data) {
        if (data) {
            $("#DevID").val(data);
        } else {
            $("#DevID").val("DB201501010001");
        }
    });
}
//选择客户时
function SelectCus() {
    addCl = $("#CusID option:selected").attr("cl");
    if (addCl != undefined) {
        $("#orderbody tr").children("td")[7].innerHTML = addCl + "%";
        var price = parseFloat($($("#orderbody tr").children("td")[6]).children("input")[0].value);
        $("#orderbody tr").children("td")[8].innerHTML = (price - (price / addCl)).toFixed(2);
        $("#orderbody tr").children("td")[9].innerHTML = ($($("#orderbody tr").children("td")[5]).children("input")[0].value * (price - (price / addCl))).toFixed(2);
    }

}
