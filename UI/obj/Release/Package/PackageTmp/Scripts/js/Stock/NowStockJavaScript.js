/// <reference path="../../assets/js/jquery.min.js" />

var PageIndex = 1;
var MaxPageIndex = 0;
var ProID = "";
var ProName = "";
var PTID = "0";
var PCID = "0";
var PSID = "0";
var PUID = "0";
var DepotID ="";
$(function () {
    $("#btn_search").click(function () {
        PageIndex = 1;
        Find();
    });
    MaxPageIndex = $("#MaxPageIndex").val();
    //树点击事件
    $("#tree").delegate("span", "click", function () {
        $("#tree span").removeClass("ck");
        $(this).addClass("ck");
        var id = $(this).parent().attr("key");
        var obj = { ProID: ProID, ProName: ProName, PTID: id, PCID: PCID, PSID: PSID, PUID: PUID, DepotID: DepotID, PageIndex: PageIndex };
        //alert(JSON.stringify(obj));
        $.post("/Stock/NowStock/Find", obj, function (data) {
            loeaddata(data);
        }, "json");
    });
    Find();
});

///查询
function Find() {
  
     ProID=$("#ProID").val();
     ProName=$("#ProName").val();
     PTID=$("#PTID option:selected").val();
     PCID=$("#PCID option:selected").val();
     PSID=$("#PSID option:selected").val();
     PUID=$("#PUID option:selected").val();
     DepotID = $("#DepotID option:selected").val();
    var obj = { ProID: ProID, ProName: ProName, PTID: PTID, PCID: PCID, PSID: PSID, PUID: PUID, DepotID: DepotID, PageIndex: PageIndex };
    //alert(JSON.stringify(obj));
    $.post("/Stock/NowStock/Find", obj, function (data) {
     
        loeaddata(data);
    },"json");
}

///加载数据
function loeaddata(data) {
   // alert(JSON.stringify(data));
    $("#tab_pro_bd tr").remove();
    $("#SelPage option").remove();
    $.post("/Stock/NowStock/FindCount", null, function (count) {
        if (count) {
            MaxPageIndex =parseInt( count);
             if (MaxPageIndex=="0") {
                return;
            }
        }
    });
   
    for (var i = 1; i <= MaxPageIndex; i++) {
        $("<option value='" + i + "'>" + i + "/" + MaxPageIndex + "</option>").appendTo("#SelPage");
    }
    $("#SelPage option[value=" + PageIndex + "]").prop("selected", true);
    $(data).each(function (k, v) {
        var tr = $("<tr></tr>");
        var td1 = $("<td>" + v.ProName + "</td>");
        var td2 = $("<td>" + v.ProID + "</td>");
        var td3 = $("<td>" + v.PTName + "</td>");
        var td4 = $("<td>" + v.PUName + "</td>");
        var td5 = $("<td>" + v.PSName + "</td>");
        var td6 = $("<td>" + v.PCName + "</td>");
        var td7 = $("<td>" + v.DepotName + "</td>");
        var td8 = $("<td>" + v.DSAmount + "</td>");
        var td9 = $("<td>" + v.ProMax + "</td>");
        var td10 = $("<td>" + v.ProMin + "</td>");
        var td11 = $("<td>" + v.DSPrice + "</td>");
        var td12 = $("<td>" + v.ProInPrice + "</td>");
        var td13 = $("<td>" + v.ProDesc + "</td>");
        tr.append(td1)
            .append(td2)
            .append(td3)
            .append(td4)
            .append(td5)
            .append(td6)
            .append(td7)
            .append(td8)
            .append(td9)
            .append(td10)
            .append(td11)
             .append(td12)
             .append(td13);
        $("#tab_pro_bd").append(tr);
    });
}

function shouye() {
    PageIndex = 1;
    Find();
}
function weiye() {
    PageIndex = MaxPageIndex;
    Find();
}
function xiayiye() {
    PageIndex = PageIndex < MaxPageIndex ? PageIndex + 1 : PageIndex;
    Find();
}
function shangyiye() {
    PageIndex = PageIndex > 1 ? PageIndex - 1 : PageIndex;
    Find();
}
function change(obj) {
    PageIndex = parseInt(obj.value);
    Find();
}