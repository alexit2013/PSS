//将数据库格式时间转成年月日
function ConvertTime(str) {
    var ss = str.replace("/Date(", "").replace(")/", "");
    var ts = new Date(parseInt(ss));
    return ts.getFullYear() + "年" + (ts.getMonth() + 1) +"月" + ts.getDate() +"日";
}

function ConvertTime_(str) {
    var ss = str.replace("/Date(", "").replace(")/", "");
    var ts = new Date(parseInt(ss));
    return ts.getFullYear() + "-" + (ts.getMonth() + 1) + "-" + ts.getDate();
}

//封转的Ajax方法
function myAjax(url, datas, fn) {
    $.ajax({
        type:"POST",
        url:url,
        data:datas,
        contentType: "application/json;charset=utf-8",
        success:fn
    });
}
//获取当前系统时间yyyy-MM-dd may
function getNowDate() {
    var ts = new Date();
    return ts.getFullYear() + "年" + (ts.getMonth() + 1) + "月" + ts.getDate() + "日";

}

//获取当前系统时间yyyy-MM-dd may
function getNowDate_() {
    var ts = new Date();
    return ts.getFullYear() + "-" + (ts.getMonth() + 1) + "-" + ts.getDate();

}
//将年月日格式转成yyyy-MM-dd
function ConvertToDT(str) {
   return str.replace("年","-").replace("月","-").replace("日","");
}
//将yyyy-MM-dd格式事件转成年月日
function ConvertToDTX(str) {
    strs = str.split("-");
    return strs[0] + "年" + strs[1] + "月" + strs[2] + "日";
}
//非空判断验证
function IsNULL(str) {
    if (!str) {
        return true;
    }
     else if (str.trim().length == 0) {
        return true;
    } else {
        return false;
    }
}

