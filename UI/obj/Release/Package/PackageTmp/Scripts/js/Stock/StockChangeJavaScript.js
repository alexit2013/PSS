var PageIndex = 1;
var MaxPageIndex = 0;
$(function () {
    MaxPageIndex = $("#MaxPageIndex").val();
    Find();
    $("#btn_search").click(function () {
        PageIndex = 1;
        Find();
    });
})
//查询
function Find() {
    var ProID = $("#ProID").val();
    var BDate = $("#BDate").val();
    var EDate = $("#EDate").val();
    var DeptID = $("#DepotID option:selected").val();
    $("#SelPage option").remove();
    $("#tab_sotck_bd tr").remove();
    $.post("/Stock/StockChange/Find", { ProID: ProID, BDate: BDate, EDate: EDate, DepotID: DeptID, PageIndex: PageIndex }, function (data) {
        if (data[0].IODID != "") {
        MaxPageIndex = data[0].MaxPageIndex;
        for (var i = 1; i <= MaxPageIndex; i++) {
            $("<option value='" + i + "'>" + i + "/" + MaxPageIndex + "</option>").appendTo("#SelPage");
        }
        $("#SelPage option[value=" + PageIndex + "]").prop("selected",true);
        if (data[0].IODID != "") {
            $.each(data, function (k, v) {
                var tr = $("<tr><td> " + v.IODNum + "</td><td>" + ConvertTime(v.IODDate) + "</td><td>" + v.ProID + "</td><td>" + v.ProID + "</td><td>" + v.PUName + "</td><td>" + v.PSName + "</td><td>" + v.PCName + "</td><td>" + v.IODDAmount + "</td><td>" + v.IODDPrice + "</td><td>" + (v.IODDAmount * +v.IODDPrice).toFixed(2) + "</td><td>" + v.DepotName + "</td><td>" + (v.IODType == "1" ? "入库" : "出库") + "</td></tr>");
                $("#tab_sotck_bd").append(tr);
            });
        }
        }
    });
}
//首页
function shouye() {
    PageIndex = 1;
    Find();
}
//尾页
function weiye() {
    PageIndex = MaxPageIndex;
    Find();
}
//下一页
function xiayiye() {
    PageIndex = PageIndex < MaxPageIndex ? PageIndex + 1 : PageIndex;
    Find();
}
//上一页
function shangyiye() {
    PageIndex = PageIndex > 1 ? PageIndex - 1 : PageIndex;
    Find();
}
//下拉列表change
function change(obj) {
    PageIndex = parseInt(obj.value);
    Find();
}