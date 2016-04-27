
var nowtr = null;//当前行
var PageIndex = 1;
var PageIndex1 = 1;
var MaxPageIndex = 0;
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
        $.post("/InStock/InDepot/GetProductInDepotDeteilByID", { id: stockid }, function (data) {
            if (data) {
                $("#stockbodyed tr").remove();
                $(data).each(function (k, v) {
                    var tr = $("<tr></tr>");
                    var td1 = $("<td>" + v.PIDDID + "</td>");
                    var td2 = $("<td>" + v.ProName + "</td>");
                    var td4 = $("<td>" + v.PIDDPrice + "</td>");
                    var td5 = $("<td>" + v.PIDDAmount + "</td>");
                    var td6 = $("<td><i style='color:green;cursor:pointer;font-size:16px;' class='am-icon-comment'  title='" + v.PIDDDesc + "'></i></td>");
                    tr.append(td1).append(td2).append(td4).append(td5).append(td6);
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
        var PIDID = $("#PIDID").val();
        var DepotID = $("#DepotID option:selected").val();
        var PIDDate = $("#PIDDate").val();
        var PDIDesc = $("#PDIDesc").text();
        $("#orderbody tr").each(function (k, v) {
            var tdid = $(v).children("td")[0].innerHTML;
            if (!isNaN(tdid)) {
                var PIDDAmount = $($(v).children("td")[5]).children("input")[0].value;
                var PIDDPrice = $($(v).children("td")[6]).children("input")[0].value;
                var PIDDDesc = $($(v).children("td")[8]).children("input")[00].value;
                var st = { PIDID: PIDID, ProID: tdid, PIDDAmount: PIDDAmount, PIDDPrice: PIDDPrice, PIDDDesc: PIDDDesc };
                list.push(st);
            }
        });
        var stock = { PIDID: PIDID, DepotID: DepotID, PIDDate: PIDDate, PIDState: 0, PDIDesc: PDIDesc };
        //alert(JSON.stringify(list));
        //alert(JSON.stringify(stock));
        var type = $(this).val();
        if (type == "保存") {
            $.post("/InStock/InDepot/AddProductDepot", { pd: stock, list: list }, function (data) {
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
            $.post("/InStock/InDepot/EditProductInDepot", { pd: stock, list: list }, function (data) {
                if (data == "edit_yes") {
                    new tmm_alert1("edit_modal", "提示", "修改成功");
                    $("#btn_addorder").val("保存");
                    $("#tab_2").removeClass("am-active");
                    $("#tab_1").addClass("am-active");
                    $("#tab2").removeClass("am-fade").removeClass("am-in").removeClass("am-active");
                    $("#tab1").addClass("am-fade").addClass("am-in").addClass("am-active");
                    document.getElementById("form_order").reset();
                    $("#orderbody tr").remove();
                    Page();
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
        if (isNaN(num1.val())) {
            return;
        } else {
            var tr = num1.parent().parent();
            tr.children("td")[7].innerHTML = "￥" + (parseFloat(num1.val()) * parseFloat(tr.find("input")[1].value));
        }


    });
    $("#orderbody").delegate("input[name=proprice]", "keyup", function () {
        var num1 = $(this);
        if (isNaN(num1.val())) {
            return;
        } else {
            var tr = num1.parent().parent();
            tr.children("td")[7].innerHTML = "￥" + (parseFloat(num1.val()) * parseFloat(tr.find("input")[0].value));
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
        var newtr = $('<tr><td align="center"><button name="showpro" type="button" class="am-btn am-btn-primary am-dropdown-toggle am-btn-sm" style="font-size:10px;">商品 <span class="am-icon-caret-down"></span></button></td><td></td><td></td><td></td><td></td><td ><input  type="number" name="procount" placeholder="请输入商品数量" style="width:100px;font-size:11px;" required/></td><td><input type="number"  name="proprice"   placeholder="请输入商品单价" style="width:100px;font-size:11px;" required /></td><td></td><td><input type="text" placeholder="备注"  style="width:160px;font-size:11px;"  /></td><td align="center"><a class="am-icon-remove" title="删除" href="#" style="font-size:18px;"></a></td></tr>');
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
    var PIDID = $("#PIDID1").val();
    var DepotID = $("#DepotID1 option:selected").val();
    var PIDDate = $("#PIDDate1").val();
    var UsersName = $("#UsersName1").val();
    var PIDState = $("input[name=PIDState1]:checked").val();
    var obj = { PIDID: PIDID, UsersName: UsersName, DepotID: DepotID, PIDDate: PIDDate, PIDState: PIDState, PageIndex: PageIndex };
    $.post("/InStock/InDepot/Find", obj, function (data) {
        if (data) {
            $("#SelPage option").remove();
            $("#probodyed tr").remove();
            if (data[0].PIDID=="") {
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
    if (data[0].StockID == "") {
        return;
    }
    $(data).each(function (k, v) {
        var tr = $("<tr></tr>");
        var td1 = $("<td>" + v.PIDID + "</td>");
        var td2 = $("<td>" + v.DepotName + "</td>");
        var td3 = $("<td></td>");
        if (v.PIDDate!="null"&&v.PIDDate!="undefined") {
            td3.text(ConvertTime(v.PIDDate));
        }   
        
        var td4 = $("<td>" + v.UsersName + "</td>");
        var td5 = $("<td align='center' ></td>");
        if (+v.PIDState == "0")
            td5.html("<font style='color:red'>未审核</font>");
        else
            td5.html("<font style='color:green'>已审核</font>");

        var td6 = $("<td ><i style='color:green;cursor:pointer;font-size:16px;' class='am-icon-comment'  title='" + v.PDIDesc + "'></i></td>");
        var td7 = $("<td align='center'><button style='font-size:10px;' class='am-btn am-btn-warning am-radius   am-btn-xs '  " + (v.PIDState == "1" ? "disabled='disabled'" : "") + "  onclick='orderedit(\"" + v.PIDID + "\",\"" + v.DepotID + "\",\"" + v.PIDDate + "\",\"" + v.PIDUser + "\",\"" + v.PIDState + "\",\"" + v.PDIDesc + "\")'><span class='am-icon-pencil-square-o'></span> 编辑</button><button style='font-size:10px;margin-left:3px;' class='am-btn am-btn-success am-radius am-btn-xs' " + (v.PIDState == "1" ? "disabled='disabled'" : "") + "  onclick='order_ck(\"" + v.PIDID + "\")' ><span class='am-icon-check'></span> 审核</button> <button style='font-size:10px;' class='am-btn am-btn-danger am-radius am-btn-xs' " + (v.PIDState == "1" ? "disabled='disabled'" : "") + " onclick='order_del(\"" + v.PIDID + "\")' ><span class='am-icon-trash-o'></span> 删除</button></td>");
        tr.append(td1).append(td2).append(td3).append(td4).append(td5).append(td6).append(td7);
        $("#probodyed").append(tr);
    });
}

//删除采购单
function order_del(id) {
    new tmm_confirm("del", "提示", "确认要删除吗？", function () {
        $.post("/InStock/InDepot/DelProductDepot", { id: id }, function (data) {
            if (data == "del_yes") {
                new tmm_alert1("atdel", "提示", "删除成功！");
                Page();
                $("#probodyed tr").remove();
            } else {
                new tmm_alert("atdel", "提示", "删除失败！");
            }
            $("#atdel").modal({ closeViaDimmer: 0 });
        });
        $("#del").remove();
    }, function () {
        $("#del").remove();
    });
    $("#del").modal({ closeViaDimmer: 0, width: 350 });

}
//编辑入库单
function orderedit(PIDID, DepotID, PIDDate, PIDUser, PIDState, PDIDesc) {
    $("#btn_addorder").val("修改");
    $("#tab_1").removeClass("am-active");
    $("#tab_2").addClass("am-active");
    $("#tab1").removeClass("am-fade").removeClass("am-in").removeClass("am-active");
    $("#tab2").addClass("am-fade").addClass("am-in").addClass("am-active");
    $("#PIDID").val(PIDID);
    $("#PIDDate").val(ConvertTime_(PIDDate));
    $("#PDIDesc").val(PDIDesc);
    $("#DepotID option[value='" + DepotID + "']").prop("selected", true);
    //显示商品信息
    $.post("/InStock/InDepot/GetProductInDepotDeteilByID", { id: PIDID }, function (data) {
        $("#orderbody tr").remove();
        if (data) {
            $(data).each(function (k, v) {
                var newtr = $('<tr><td align="center">' + v.ProID + '</td><td>' + v.ProName + '</td><td>' + v.PUName + '</td><td>' + v.PSName + '</td><td>' + v.PCName + '</td><td ><input  type="number" name="procount" value="' + v.PIDDAmount + '" placeholder="请输入商品数量" style="width:100px;font-size:11px;" required/></td><td><input type="number" value="' + v.PIDDPrice + '"  name="proprice"   placeholder="请输入商品单价" style="width:100px;font-size:11px;" required /></td><td>￥' + v.PIDDAmount * v.PIDDPrice + '</td><td><input type="text" value="' + v.PIDDDesc + '" placeholder="备注"  style="width:160px;font-size:11px;"  /></td><td align="center"><a class="am-icon-remove" title="删除" href="#" style="font-size:18px;"></a></td></tr>');
                $("#orderbody").append(newtr);
            });
        }
    });

}
//审核订单
function order_ck(id) {
    new tmm_confirm("ck", "提示", "确认操作？", function () {
        $.post("/InStock/InDepot/CKProductDepot", { id: id }, function (data) {
            if (data == "ck_yes") {
                new tmm_alert1("ck1", "提示", "操作成功！");
                Page();
            } else if (data == "ck_no") {
                new tmm_alert("ck1", "提示", "操作失败！");
            } else {
                new tmm_alert1("ck1", "提示", "未知错误，操作失败！");
            }
            $("#ck1").modal({closeViaDiammer:0,width:350});
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
    $.post("/InStock/InDepot/GetProductID", {}, function (data) {
        if (data) {
            $("#PIDID").val(data);
        } else {
            $("#PIDID").val("SR201501010001");
        }
    });
}