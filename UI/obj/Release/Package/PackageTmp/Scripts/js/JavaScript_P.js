var PageIndex_p = 1;
var MaxPageIndex_p = 0;
$(function () {
    //显示添加多个商品窗体
    $("#orderbody").delegate("tr", "dblclick", function () {
        $("#product_add1").modal({ height: 570, width: 1000 });
    });
    //删除已选商品
    $("#add_model_body1").delegate("button", 'click', function () {
        $(this).parent().parent().parent().remove();
    });
    //添加商品
    $("#add_model_body").delegate("tr", "click", function () {
        var ProID = $(this).children("td")[0].innerHTML;
        var ProName = $(this).children("td")[1].innerHTML;
        var PTName = $(this).children("td")[2].innerHTML;
        var PCName = $(this).children("td")[3].innerHTML;
        var PUName = $(this).children("td")[4].innerHTML;
        var PSName = $(this).children("td")[5].innerHTML;
        var ProPrice = $(this).children("td")[6].innerHTML;
        var fs = true;
        $("#add_model_body1 tr").each(function (k, v) {
            var pid = $(v).children("td")[0].innerHTML;
            if (pid == ProID) {
                alert("已存在该商品！");
                fs = false;
            }
        });
        if (fs) {
            var tr = $("<tr><td>" + ProID + "</td><td>" + ProName + "</td><td>" + PTName + "</td><td>" + PCName + "</td><td>" + PUName + "</td><td>" + PSName + "</td><td>" + ProPrice + "</td><td><input type='number' name='count_p' value='1'  size='5' /></td><td><a href='#' name='del_p'><button style='font-size:10px;' class='am-btn am-btn-danger am-radius am-btn-xs' name='del_p' ><span class='am-icon-trash-o'></span> 删除</button></a></td></tr>");
            $("#add_model_body1").append(tr);
        }
    });
    Page_p();
    //查询
    $("#brn_search_pro").click(function () {
        PageIndex_p = 1;
        Page_p();
    });
});
function Page_p() {
    var ProName = $("#ProName").val();
    var PTID = $("#PTID_p option:selected").val();
    var PSID = $("#PSID_p option:selected").val();
    var PCID = $("#PCID_p option:selected").val();
    var PUID = $("#PUID_p option:selected").val();
    var obj = { ProName: ProName, PTID: PTID, PUID: PUID, PCID: PCID, PSID: PSID, PageIndex: PageIndex_p };
    $.post("/InStock/Home/FindProducts", obj, function (data) {
        //  alert(JSON.stringify(data));
        $("#add_model_body tr").remove();
        $("#SelPage_p option").remove();
        MaxPageIndex_p = data[0].MaxPageIndex;
        for (var i = 1; i <= MaxPageIndex_p; i++) {
            $("<option value='" + i + "'>" + i + "/" + MaxPageIndex_p + "</option>").appendTo("#SelPage_p");
        }
        $("#SelPage_p option[value=" + PageIndex_p + "]").prop("selected", "selected");
        if (!data[0].ProID == "") {
            $(data).each(function (k, v) {
                var tr = $("<tr><td>" + v.ProID + "</td><td>" + v.ProName + "</td><td>" + v.PTName + "</td><td>" + v.PCName + "</td><td>" + v.PUName + "</td><td>" + v.PSName + "</td><td>" + v.ProPrice + "</td></tr>");
                $("#add_model_body").append(tr);
            });
        }
    });
}
function shouye_p() {
    PageIndex_p = 1;
    Page_p();
}
function weiye_p() {
    PageIndex_p = MaxPageIndex_p;
    Page_p();
}
function xiayiye_p() {
    PageIndex_p = PageIndex_p < MaxPageIndex_p ? PageIndex_p + 1 : PageIndex_p;
    Page_p();
}
function shangyiye_p() {
    PageIndex_p = PageIndex_p > 1 ? PageIndex_p - 1 : PageIndex_p;
    Page_p();
}
function change_p(obj) {
    PageIndex_p = parseInt(obj.value);
    Page_p();
}