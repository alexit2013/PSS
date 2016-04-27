var nowtr = null;//当前行
var PageIndex = 1;
var PageIndex1 = 1;
var MaxPageIndex = 0;
var brn_search1 = null;//查询按钮
$(function () {
    
    MaxPageIndex = $("#MaxPageIndex").val();
    MaxPageIndex1 = $("#MaxPageIndex1").val();
    brn_search1 = $("#brn_search1");
    Page1();
    Page();
    //选择入库订单
    $("#model_body").delegate("tr", "dblclick", function () {
        var stockid = $(this).children("td")[0].innerHTML;
        $.post("/InStock/StockInDepot/GetStockByID", { id: stockid }, function (data) {
            if (data) {
                $("#StockID").val(stockid);
                $("#PPID option[value='" + data.PPID + "']").attr("selected", true);
                $("#SIDDate").val(data.StockInDate!=null?ConvertTime_(data.StockInDate):"");
                $("#SIDDesc").text(data.StockDesc);
            }
        });
        $.post("/InStock/StockInDepot/GetStockDetailByStockID", { id: stockid }, function (data) {
            if (data) {
                $("#orderbody tr").remove();
                $(data).each(function (k,v) {
                    var newtr = $('<tr><td align="center">' + v.ProID + '</td><td>' + v.ProName + '</td><td>' + v.PUName + '</td><td>' + v.PSName + '</td><td>' + v.PCName + '</td><td ><input  type="number" name="procount" value="' + v.SDetailAmount + '" placeholder="请输入商品数量" style="width:100px;font-size:11px;" required/></td><td><input type="number" value="' + v.SDetailPrice + '"  name="proprice"   placeholder="请输入商品单价" style="width:100px;font-size:11px;" required /></td><td>￥' + v.SDetailAmount * v.SDetailPrice + '</td><td><input type="text" value="' + v.SDetailDesc + '" placeholder="备注"  style="width:160px;font-size:11px;"  /></td><td align="center"><a class="am-icon-remove" title="删除" href="#" style="font-size:18px;color:red;"></a></td></tr>');
                    $("#orderbody").append(newtr);
                });
            }
        });
        getid();
        $("#select_modal").modal('close');
    });
    $("#SIDID").dblclick(function () {
        $("#select_modal").modal({closeViaDimmer:0,width:1100,height:550});
    });
    //采购单查询
    $("#brn_search1").click(function () {
        $(this).addClass("am-animation-scale-down");
        Page1();
    });
    //商品查询
    $("#brn_search").click(function () {
        var brn_search = $(this);
        $(this).addClass("am-animation-scale-down");
        PageIndex = 1;
        // $(this).removeClass("am-animation-scale-down");
        var obj = formCore.getFormValues("Search_form");
        var obj1 = obj.substr(0, obj.length - 1);
        obj1 = obj1 + ',"PageIndex":"' + PageIndex + '"}';
        $.post("/InStock/StockInDepot/Find", JSON.parse(obj1), function (data) {
            if (data) {
                MaxPageIndex = data[0].MaxPageIndex;
                $("#SelPage option").remove();
                for (var i = 1; i <= MaxPageIndex; i++) {
                    $("<option value='" + i + "'>" + i + "/" + MaxPageIndex + "</option>").appendTo("#SelPage");
                }
                setPage(data);
                brn_search.removeClass("am-animation-scale-down");
            }
        });

    });
    //转换为添加
    $("#ConvertAdd").click(function () {
        document.getElementById("form_order").reset();
        $("#orderbody tr").remove();
        $("#btn_addorder").val("保存");
    });
    //显示详单
    $("#stockin_body").delegate("tr", "click", function () {
        $("#stockin_body tr").removeClass("ck");
        $(this).addClass("ck");
        var sdid = $(this).children("td")[0].innerHTML;
        $.post("/InStock/StockInDepot/GetStockInDepotDetailByStockID", { id: sdid }, function (data) {
            if (data) {
                $("#StockInDepotDetailbody tr").remove();
                $(data).each(function (k, v) {
                    var tr = $("<tr></tr>");
                    var td1 = $("<td>" + v.SIDDID + "</td>");
                    var td2 = $("<td>" + v.ProName + "</td>");
                    var td3 = $("<td>" + v.SIDAmount + "</td>");
                    var td4 = $("<td>" + v.SIDPrice + "</td>");
                    var td5 = $("<td><i style='color:green;cursor:pointer;font-size:16px;' class='am-icon-comment'  title='" + v.SIDDesc + "'></i></td>");
                    tr.append(td1).append(td2).append(td3).append(td4).append(td5);
                    $("#StockInDepotDetailbody").append(tr);
                });
            }
        });
    });
    //【添加/修改】订单
    $("#btn_addorder").click(function () {
        var list = [];
        var PPID = $("#PPID option:selected").val();
        var DepotID = $("#DepotID option:selected").val();
        var SIDDate = $("#SIDDate").val();
        var SIDID = $("#SIDID").val();
        var StockID = $("#StockID").val();
        var SIDDesc = $("#SIDDesc").text();
        var SIDDeliver = $("#SIDDeliver").val();
        var SIDFreight = $("#SIDFreight").val();
        $("#orderbody tr").each(function (k, v) {
            var tdid = $(v).children("td")[0].innerHTML;
            if (!isNaN(tdid)) {
                var SIDDAmount = $($(v).children("td")[5]).children("input")[0].value;
                var SIDDPrice = $($(v).children("td")[6]).children("input")[0].value;
                var SIDDDesc = $($(v).children("td")[8]).children("input")[00].value;
                var st = { ProID: tdid, SIDID: SIDID, SIDAmount: SIDDAmount, SIDPrice:SIDDPrice, SIDDesc: SIDDDesc };
                list.push(st);
            }
        });
        var stockin = { DepotID: DepotID, SIDID: SIDID, StockID: StockID, PPID: PPID, SIDDate: SIDDate, SIDData: 0, SIDDesc: SIDDesc, SIDDeliver: SIDDeliver, SIDFreight: SIDFreight };
        var type = $(this).val();
        if (type == "保存") {
            $.post("/InStock/StockInDepot/AddStockIn", { sti: stockin,list: list }, function (data) {
                if (data == "add_yes") {
                    new tmm_alert1("add_modal", "提示", "添加成功");
                    document.getElementById("form_order").reset();
                    Page1();
                    $("#orderbody tr").remove();
                    $("#btn_addorder").val("保存");
                } else if (data == "add_no") {
                    new tmm_alert("add_modal", "提示", "添加失败");
                } else {
                    new tmm_alert("add_modal", "提示", "未知错误，添加失败");
                }
                $("#tab_2").removeClass("am-active");
                $("#tab_1").addClass("am-active");
                $("#tab2").removeClass("am-fade").removeClass("am-in").removeClass("am-active");
                $("#tab1").addClass("am-fade").addClass("am-in").addClass("am-active");
                document.getElementById("form_order").reset();
                $("#orderbody tr").remove();
                $("#add_modal").modal({ closeViaDimmer: 0 });
           });
        } else if (type == "修改") {
            $.post("/instock/stockindepot/editstockin", { sti: stockin, list: list }, function (data) {
                if (data == "edit_yes") {
                    new tmm_alert1("edit_modal", "提示", "修改成功");
                    $("#btn_addorder").val("保存");
                    Page1();
                } else if (data == "edit_no") {
                    new tmm_alert("edit_modal", "提示", "修改失败");
                } else {
                    new tmm_alert("edit_modal", "提示", "未知错误，修改失败");
                }
                $("#tab_2").removeClass("am-active");
                $("#tab_1").addClass("am-active");
                $("#tab2").removeClass("am-fade").removeClass("am-in").removeClass("am-active");
                $("#tab1").addClass("am-fade").addClass("am-in").addClass("am-active");
                document.getElementById("form_order").reset();
                $("#orderbody tr").remove();
                $("#edit_modal").modal({ closeviadimmer: 0 });
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
                $("#SelPage option[value=" + PageIndex + "]").attr("selected", true);
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
        var newtr = $('<tr><td align="center"><button name="showpro" type="button" class="am-btn am-btn-primary am-dropdown-toggle am-btn-sm" style="font-size:10px;">商品 <span class="am-icon-caret-down"></span></button></td><td></td><td></td><td></td><td></td><td ><input  type="number" name="procount" placeholder="请输入商品数量" style="width:100px;font-size:11px;" required/></td><td><input type="number"  name="proprice"   placeholder="请输入商品单价" style="width:100px;font-size:11px;" required /></td><td></td><td><input type="text" placeholder="备注"  style="width:160px;font-size:11px;"  /></td><td align="center"><a class="am-icon-remove" title="删除" href="#" style="font-size:18px;color:red;"></a></td></tr>');
        $("#orderbody").append(newtr);
    });
    //删除行
    $("table").delegate("a[title=删除]", "click", function (e) {
        // alert(event.clientX+"--------"+event.clientY);
        var tr = $(this).parent().parent();
        tr.remove();
    });
});
//入库订单
function Page1() {
    var SIDID = $("#SIDID1").val();
    var PPID = $("#PPID1 option:selected").val();
    var DepotID = $("#DepotID1 option:selected").val();
    var UsersName = $("#UsersName1").val();
    var SIDDate = $("#SIDDate1").val();
    var SIDData = $("input[name=SIDData]:checked").val();
    //alert(PPID);
    $.post("/InStock/StockInDepot/FindStockIn", { PageIndex: PageIndex1, SIDID: SIDID, UsersName: UsersName, PPID: PPID, DepotID: DepotID, SIDDate: SIDDate, SIDData: SIDData }, function (data) {
        if (data) {
            brn_search1.removeClass("am-animation-scale-down");
            MaxPageIndex1 = data[0].MaxPageIndex;
            $("#SelPage1 option").remove();
            for (var i = 1; i <= MaxPageIndex1; i++) {
                $("<option value='" + i + "'>" + i + "/" + MaxPageIndex1 + "</option>").appendTo("#SelPage1");
            }
            $("#stockin_body tr").remove();
            if (data[0].SIDID == "") {
                return;
            }
            $("#SelPage1 option[value=" + PageIndex1 + "]").prop("selected", true);
            $(data).each(function (k, v) {
                var tr = $("<tr></tr>");
                var td1 = $("<td>" + v.SIDID + "</td>");
                var td2 = $("<td>" + v.PPName + "</td>");
                var td3 = $("<td>" + v.DepotName + "</td>");
                var td4 = $("<td>" + v.StockID + "</td>");
                var td5 = $("<td>" + ConvertTime(v.SIDDate) + "</td>");
                var td6 = $("<td>" + v.SIDDeliver + "</td>");
                //var td7 = $("<td>" + v.SIDFreight + "</td>");
                //var td8 = $("<td>" + v.UsersName + "</td>");
                var td9 = $("<td align='center' ></td>");
                if (+v.SIDData == "0")
                    td9.html("<font style='color:red'>未审核</font>");
                else
                    td9.html("<font style='color:green'>已审核</font>");

                var td10 = $("<td ><i style='color:green;cursor:pointer;font-size:16px;' class='am-icon-comment'  title='" + v.SIDDesc + "'></i></td>");
                var td11 = $("<td align='center'><button style='font-size:10px;' class='am-btn am-btn-warning am-radius   am-btn-xs '  " + (v.SIDData == "1" ? "disabled='disabled'" : "") + "  onclick='orderedit(\"" + v.SIDID + "\",\"" + v.PPID + "\",\"" + v.DepotID + "\",\"" + v.StockID + "\",\"" + v.SIDDate + "\",\"" + v.SIDDeliver + "\",\"" + v.SIDFreight + "\",\"" + v.SIDUser + "\",\"" + v.SIDData + "\",\"" + v.SIDDesc + "\")'><span class='am-icon-pencil-square-o'></span> 编辑</button><button style='font-size:10px;margin-left:3px;' class='am-btn am-btn-success am-radius am-btn-xs' " + (v.SIDData == "1" ? "disabled='disabled'" : "") + "  onclick='order_ck(\"" + v.SIDID + "\")' ><span class='am-icon-check'></span> 审核</button> <button style='font-size:10px;' class='am-btn am-btn-warning am-radius   am-btn-xs ' " + (v.SIDData == "1" ? "disabled='disabled'" : "") + "   onclick='order_del(\"" + v.SIDID + "\")' ><span class='am-icon-trash-o'></span> 删除</button></td>");
                tr.append(td1).append(td2).append(td3).append(td4).append(td5).append(td6).append(td9).append(td10).append(td11);
                $("#stockin_body").append(tr);
            });
        }
    });
}
///商品数据【分页】
function Page() {
    //$("#search_sel option[value=GY201209210003]").prop("selected", true);
    var obj = formCore.getFormValues("Search_form");
    var obj1 = obj.substr(0, obj.length - 1);
    obj1 = obj1 + ',"PageIndex":"' + PageIndex + '","StockState":"1"}';
    $.post("/InStock/StockInDepot/Find", JSON.parse(obj1), function (data) {
        if (data) {
            MaxPageIndex = data[0].MaxPageIndex;
            $("#SelPage option").remove();
            for (var i = 1; i <= MaxPageIndex; i++) {
                $("<option value='" + i + "'>" + i + "/" + MaxPageIndex + "</option>").appendTo("#SelPage");
            }
            setPage(data);
        }
    });
}
//加载商品数据
function setPage(data) {
    $("#model_body tr").remove();
    if (data[0].StockID == "") {
        return;
    }
    $("#SelPage option[value=" + PageIndex + "]").prop("selected", true);
    $(data).each(function (k, v) {
        var tr = $("<tr></tr>");
        var td1 = $("<td>" + v.StockID + "</td>");
        var td2 = $("<td>" + v.PPName + "</td>");
        var td3 = $("<td>" + ConvertTime(v.StockDate) + "</td>");
        var td4 = $("<td>" + ConvertTime(v.StockInDate) + "</td>");
        var td5 = $("<td>" + v.UsersName + "</td>");
        var td6 = $("<td align='center' ></td>");
        if (+v.StockState == "0")
            td6.html("<font style='color:red'>未审核</font>");
        else
            td6.html("<font style='color:green'>已审核</font>");

        var td7 = $("<td ><i style='color:green;cursor:pointer;font-size:16px;' class='am-icon-comment'  title='" + v.StockDesc + "'></i></td>");
        tr.append(td1).append(td2).append(td3).append(td4).append(td5).append(td6).append(td7);
        $("#model_body").append(tr);
    });
}
//删除入库单
function order_del(id) {
    new tmm_confirm("del", "提示", "确认要删除吗？", function () {
        $.post("/InStock/StockInDepot/DelStockInDepotDetail", { id: id }, function (data) {
            if (data == "del_yes") {
                new tmm_alert1("atdel", "提示", "删除成功！");
                Page1();
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
function orderedit(SIDID, PPID, DepotID, StockID, SIDDate, SIDDeliver, SIDFreight, SIDUser, SIDData, SIDDesc) {
    $("#btn_addorder").val("修改");
    $("#tab_1").removeClass("am-active");
    $("#tab_2").addClass("am-active");
    $("#tab1").removeClass("am-fade").removeClass("am-in").removeClass("am-active");
    $("#tab2").addClass("am-fade").addClass("am-in").addClass("am-active");
    $("#SIDID").val(SIDID);
    $("#StockID").val(StockID);
    $("#PPID option[value='" + PPID + "']").attr("selected",true);
    $("#DepotID option[value='" + DepotID + "']").attr("selected", true);
    $("#SIDDate").val(ConvertTime_(SIDDate));
    $("#SIDDeliver").val(SIDDeliver);
    $("#SIDFreight").val(SIDFreight);
    $("#SIDDesc").text(SIDDesc);
    //显示商品信息
    $.post("/InStock/StockInDepot/GetStockInDepotDetailByStockID", { id: SIDID }, function (data) {
        $("#orderbody tr").remove();
        if (data) {
            $(data).each(function (k, v) {
                var newtr = $('<tr><td align="center">' + v.ProID + '</td><td>' + v.ProName + '</td><td>' + v.PUName + '</td><td>' + v.PSName + '</td><td>' + v.PCName + '</td><td ><input  type="number" name="procount" value="' + v.SIDAmount + '" placeholder="请输入商品数量" style="width:100px;font-size:11px;" required/></td><td><input type="number" value="' + v.SIDPrice + '"  name="proprice"   placeholder="请输入商品单价" style="width:100px;font-size:11px;" required /></td><td>￥' + v.SIDAmount * v.SIDPrice + '</td><td><input type="text" value="' + v.SIDDesc + '" placeholder="备注"  style="width:160px;font-size:11px;"  /></td><td align="center"><a class="am-icon-remove" title="删除" href="#" style="font-size:18px;"></a></td></tr>');
                $("#orderbody").append(newtr);
            });
        }
    });

}
//审核入库单
function order_ck(id) {
    new tmm_confirm("ck", "提示", "确认操作？", function () {
        $.post("/InStock/StockInDepot/CKStockIn", { id: id }, function (data) {
            if (data == "ck_yes") {
                new tmm_alert1("ck1", "提示", "操作成功！");
                Page1();
            } else if (data == "ck_no") {
                new tmm_alert("ck1", "提示", "操作失败！");
            } else {
                new tmm_alert1("ck1", "提示", "未知错误，操作失败！");
            }
            $("#ck1").modal({closeViaDimmer:0,width:350});
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
//商品信息的分页
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
//入库订单的分页
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
//获取ID
function getid() {
    $.post("/InStock/StockInDepot/GetStockInID", {}, function (data) {
        if (data) {
            $("#SIDID").val(data);
        } else {
            new tmm_alert("getid_error", "提示", "未能获取入库编号！！！");
            $("#getid_error").modal({closeViaDimmer:0,width:350});
        }
    });
}