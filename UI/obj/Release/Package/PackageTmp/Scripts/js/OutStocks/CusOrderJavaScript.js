
var nowtr = null;//当前行
var PageIndex = 1;
var PageIndex1 = 1;
var MaxPageIndex = 0;
var addCl = 0;
$(function () {
    $("#tab_cus_bd").delegate("tr", "click", function () {
        $("#tab_cus_bd tr").removeClass("ck");
        $(this).addClass("ck");
        var id = $(this).children("td")[0].innerHTML;
        $.post("/InOut/CustomerOrder/GetCustomerOrderDetailByQPID", { id: id }, function (data) {
            if (data) {
                $("#tab_cusx_bd tr").remove();
                $(data).each(function (k, v) {
                    var tr = $("<tr></tr>");
                    var td1 = $("<td>" + v.ProName + "</td>");
                    var td2 = $("<td>" + v.CODPrice + "</td>");
                    var td3 = $("<td>" + v.CODAmount + "</td>");
                    var td4 = $("<td align='center'>" + v.CODDiscont + "%</td>");
                    var td5 = $("<td>" + v.CODDisPrice + "</td>");
                    var td6 = $("<td>" + v.CODSale + "</td>");
                    var td7 = $("<td><i style='color:green;cursor:pointer;font-size:16px;' class='am-icon-comment'  title='" + v.CODDesc + "'></i></td>");
                    tr.append(td1).append(td2).append(td3).append(td4).append(td5).append(td6).append(td7);
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
    $("#COID").dblclick(function () {
        $("#select_modal").modal({closeViaDimmer:0,width:1100,height:550});
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
        var stockid = $(this).children("td")[0].innerHTML;
        var CusID = $(this).attr("CusID");
        var QPDate = $(this).attr("QPDate");
        var QPDesc = $(this).attr("QPDesc");
        $("#CusID>option[value='" + CusID + "']").attr("selected", true);
        //$("#CODate").val(ConvertTime_(QPDate));
        $("#CODesc").text(QPDesc);
        $.post("/InOut/QuotePrice/GetQuotePriceDetailByQPID", { id: stockid }, function (data) {
            $("#orderbody tr").remove();
            if (data) {
                $(data).each(function (k, v) {
                    $("#orderbody tr").remove();
                    if (data) {
                        $(data).each(function (k, v) {
                            addCl = v.QPDDiscont;
                            var newtr = $('<tr><td align="center">' + v.ProID + '</td><td>' + v.ProName + '</td><td>' + v.PUName + '</td><td>' + v.PSName + '</td><td>' + v.PCName + '</td><td ><input  type="number" name="procount" value="' + v.QPDAmount + '" placeholder="请输入商品数量" style="width:100px;font-size:11px;" required/></td><td><input type="number" value="' + v.QPDPrice + '"  name="proprice"   placeholder="请输入商品单价" style="width:100px;font-size:11px;" required /></td><td align="center">' + v.QPDDiscont + '%</td><td>' + v.QPDDisPrice + '</td><td>￥' + v.QPDAmount * v.QPDDisPrice + '</td><td><input class="am-form-field" style="width:80px;font-size:11px;"type="number" placeholder="已销数量" readonly="readonly" required/></td><td><input type="text" value="' + v.QPDDesc + '" placeholder="备注"  style="width:120px;font-size:11px;"  /></td><td align="center"><a class="am-icon-remove" title="删除" href="#" style="font-size:18px;"></a></td></tr>');
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
        var COID = $("#COID").val();
        var CusID = $("#CusID option:selected").val();
        var CODate = $("#CODate").val();
        var CORefDate = $("#CORefDate").val();
        var CODesc = $("#CODesc").text();
        $("#orderbody tr").each(function (k, v) {
            var tdid = $(v).children("td")[0].innerHTML;
            if (!isNaN(tdid)) {
                var CODAmount = $($(v).children("td")[5]).children("input")[0].value;//数量
                var CODPrice = $($(v).children("td")[6]).children("input")[0].value;//价格
                var CODDiscont = addCl;//折扣
                var CODDisPrice = $(v).children("td")[8].innerHTML//折后价
                var CODSale = $($(v).children("td")[10]).children("input")[0].value;//已售数量
                var CODDesc = $($(v).children("td")[11]).children("input")[0].value;//描述
                var st = { COID: COID, ProID: tdid, CODAmount: CODAmount, CODPrice: CODPrice, CODDiscont: CODDiscont, CODDisPrice: CODDisPrice, CODSale: CODSale, CODDesc: CODDesc };
                list.push(st);
            }
        });
        var stock = { COID: COID, CusID: CusID, CODate: CODate, CORefDate: CORefDate, COState: 0, CODesc: CODesc };
        //alert(JSON.stringify(list));
        //alert(JSON.stringify(stock));
        var type = $(this).val();
        if (type == "保存") {
            $.post("/InOut/CustomerOrder/AddCustomerOrder", { qp: stock, list: list }, function (data) {
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
            $.post("/InOut/CustomerOrder/EditCustomerOrder", { qp: stock, list: list }, function (data) {
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
                $("#edit_modal").modal();
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
    //添加行
    $("#Addtr").click(function () {
        $("#prodrown").hide(300);
        var newtr = $('<tr><td align="center"><button name="showpro" type="button" class="am-btn am-btn-primary am-dropdown-toggle am-btn-sm" style="font-size:10px;">商品 <span class="am-icon-caret-down"></span></button></td><td></td><td></td><td></td><td></td><td><input name="procount" type="number" max="100000000" placeholder="请输入商品数量" style="width:100px;font-size:11px;" required /></td><td><input type="number" name="proprice" placeholder="请输入商品单价" readonly="readonly"  style="width:100px;font-size:11px;" required /></td><td align="center"></td><td></td><td></td><td><input class="am-form-field" style="width:80px;font-size:11px;"type="number" readonly="readonly"  placeholder="已销数量" required/></td><td><input type="text" placeholder="备注" style="width:120px;font-size:11px;" /></td><td align="center"><a class="am-icon-remove" title="删除" href="#" style="font-size:18px;"></a></td></tr>');
        $("#orderbody").append(newtr);
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
    var COID = $("#COID1").val();
    var CusID = $("#CusID1 option:selected").val();
    var CODate = $("#CODate1").val();
    var CORefDate = $("#CORefDate1").val(); 
    var UsersName = $("#UsersName1").val();
    var COState = $("input[name=COState1]:checked").val();
    var obj = { COID: COID, UsersName: UsersName, CusID: CusID, CODate: CODate, CORefDate: CORefDate, COState: COState, PageIndex: PageIndex1 };
  //  alert(JSON.stringify(obj));
    $.post("/InOut/CustomerOrder/Find", obj, function (data) {
        if (data) {
            $("#SelPage1 option").remove();
            $("#tab_cus_bd tr").remove();
            if (data[0].COID == "") {
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
    if (data[0].COID == "") {
        return;
    }
    $("#SelPage1 option[value=" + PageIndex1 + "]").prop("selected", true);
    $(data).each(function (k, v) {
        var tr = $("<tr></tr>");
        var td1 = $("<td>" + v.COID + "</td>");
        var td2 = $("<td>" + v.CusName + "</td>");
        var td3 = $("<td></td>");
        if (v.CODate != "null" && v.CODate != "undefined") {
            td3.text(ConvertTime(v.CODate));
        }
        var td4 = $("<td></td>");
        if (v.CORefDate != "null" && v.CORefDate != "undefined") {
            td4.text(ConvertTime(v.CORefDate));
        }
        var td5 = $("<td>" + v.UsersName + "</td>");
        var td6 = $("<td align='center' ></td>");
        if (+v.COState == "0")
            td6.html("<font style='color:red'>未审核</font>");
        else
            td6.html("<font style='color:green'>已审核</font>");

        var td7 = $("<td ><i style='color:green;cursor:pointer;font-size:16px;' class='am-icon-comment'  title='" + v.CODesc + "'></i></td>");
        var td8 = $("<td align='center'><button style='font-size:10px;' class='am-btn am-btn-warning am-radius   am-btn-xs '  " + (v.COState == "1" ? "disabled='disabled'" : "") + "  onclick='orderedit(\"" + v.COID + "\",\"" + v.CusID + "\",\"" + v.CODate + "\",\"" + v.CORefDate + "\",\"" + v.CLAgio + "\",\"" + v.CODesc + "\")'><span class='am-icon-pencil-square-o'></span> 编辑</button><button style='font-size:10px;margin-left:3px;' class='am-btn am-btn-success am-radius am-btn-xs' " + (v.COState == "1" ? "disabled='disabled'" : "") + "  onclick='order_ck(\"" + v.COID + "\")' ><span class='am-icon-check'></span> 审核</button> <button style='font-size:10px;' class='am-btn am-btn-danger am-radius am-btn-xs' " + (v.COState == "1" ? "disabled='disabled'" : "") + " onclick='order_del(\"" + v.COID + "\")' ><span class='am-icon-trash-o'></span> 删除</button></td>");
        tr.append(td1).append(td2).append(td3).append(td4).append(td5).append(td6).append(td7).append(td8);
        $("#tab_cus_bd").append(tr);
    });
}


///加载采购订单数据【分页】
function Page() {
    var QPID = $("#QPID1").val();
    var CusID = $("#CusID1 option:selected").val();
    var QPDate = $("#QPDate1").val();
    var UsersName = $("#UsersName1").val();
    var QPState = 1;
    var obj = { QPID: QPID, UsersName: UsersName, CusID: CusID, QPDate: QPDate, QPState: QPState, PageIndex: PageIndex };
    $.post("/InOut/QuotePrice/Find", obj, function (data) {
        if (data) {
            $("#SelPage option").remove();
            $("#probodyed tr").remove();
            if (data[0].QPID == "") {
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
    if (data[0].QPID == "") {
        return;
    }
    $("#SelPage option[value=" + PageIndex + "]").prop("selected", true);
    $(data).each(function (k, v) {
        var tr = $("<tr CusID='" + v.CusID + "' QPDate='" + v.QPDate + "' QPDesc='" + v.QPDesc + "'></tr>");
        var td1 = $("<td>" + v.QPID + "</td>");
        var td2 = $("<td>" + v.CusName + "</td>");
        var td3 = $("<td></td>");
        if (v.QPDate != "null" && v.QPDate != "undefined") {
            td3.text(ConvertTime(v.QPDate));
        }
        var td4 = $("<td>" + v.UsersName + "</td>");
        var td6 = $("<td ><i style='color:green;cursor:pointer;font-size:16px;' class='am-icon-comment'  title='" + v.QPDesc + "'></i></td>");
        tr.append(td1).append(td2).append(td3).append(td4).append(td6);
        $("#probodyed").append(tr);
    });
}

//删除采购单
function order_del(id) {
    new tmm_confirm("del", "提示", "确认要删除吗？", function () {
        $.post("/InOut/CustomerOrder/DelCustomerOrder", { id: id }, function (data) {
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
function orderedit(COID, CusID, CODate, CORefDate, CLAgio, CODesc) {
    $("#btn_addorder").val("修改");
    $("#tab_1").removeClass("am-active");
    $("#tab_2").addClass("am-active");
    $("#tab1").removeClass("am-fade").removeClass("am-in").removeClass("am-active");
    $("#tab2").addClass("am-fade").addClass("am-in").addClass("am-active");
    $("#COID").val(COID);
    $("#CODate").val(ConvertTime_(CODate));
    $("#CORefDate").val(ConvertTime_(CORefDate));
    $("#CODesc").text(CODesc);
    $("#CusID option[value='" + CusID + "']").prop("selected", true);
    addCl = CLAgio;//折扣
    //显示商品信息
    $.post("/InOut/CustomerOrder/GetCustomerOrderDetailByQPID", { id: COID }, function (data) {
        $("#orderbody tr").remove();
        if (data) {
            $(data).each(function (k, v) {
            
                var newtr = $('<tr><td align="center">' + v.ProID + '</td><td>' + v.ProName + '</td><td>' + v.PUName + '</td><td>' + v.PSName + '</td><td>' + v.PCName + '</td><td ><input  type="number" name="procount" value="' + v.CODAmount + '" placeholder="请输入商品数量" style="width:100px;font-size:11px;" required/></td><td><input type="number" value="' + v.CODPrice + '"  name="proprice"   placeholder="请输入商品单价" style="width:100px;font-size:11px;" required /></td><td align="center">' + v.CODDiscont + '%</td><td>' + v.CODDisPrice + '</td><td>' + (v.CODDiscont * v.CODDisPrice).toFixed(2) + '</td><td><input class="am-form-field" style="width:80px;font-size:11px;"type="number" value="' + v.CODSale + '" placeholder="已销数量" required/></td><td><input type="text" value="' + v.CODDesc + '" placeholder="备注"  style="width:120px;font-size:11px;"  /></td><td align="center"><a class="am-icon-remove" title="删除" href="#" style="font-size:18px;"></a></td></tr>');
                $("#orderbody").append(newtr);
            });
        }
    });

}
//审核订单
function order_ck(id) {
    new tmm_confirm("ck", "提示", "确认操作？", function () {
        $.post("/InOut/CustomerOrder/CKCustomerOrder", { id: id }, function (data) {
            if (data == "ck_yes") {
                new tmm_alert1("ck1", "提示", "操作成功！");
                Page1();
            } else if (data == "ck_no") {
                new tmm_alert("ck1", "提示", "操作失败！");
            } else {
                new tmm_alert1("ck1", "提示", "未知错误，操作失败！");
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
    PageIndex1 = PageIndex1 < MaxPageIndex1 ? PageIndex1+ 1 : PageIndex1;
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
    $.post("/InOut/CustomerOrder/GetCustomerOrderID", {}, function (data) {
        if (data) {
            $("#COID").val(data);
        } else {
            $("#COID").val("KD201501010001");
        }
    });
}
//选择客户时
function SelectCus() {
    addCl = $("#CusID option:selected").attr("cl");
    if (addCl != undefined) {

        $("#orderbody tr").each(function (k,v) {
            $(v).children("td")[7].innerHTML = addCl + "%";
            var price = parseFloat($($(v).children("td")[6]).children("input")[0].value);
            $(v).children("td")[8].innerHTML = (price - (price / addCl)).toFixed(2);
            $(v).children("td")[9].innerHTML = ($($(v).children("td")[5]).children("input")[0].value * (price - (price / addCl))).toFixed(2);
        });
       
    }

}