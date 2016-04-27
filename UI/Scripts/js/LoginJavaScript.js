/// <reference path="../jquery-1.10.2.min.js" />
/// <reference path="../assets/img/jquery.backstretch.min.js" />
/// <reference path="modal.js" />

var login = function () {
    //确保登录页面的独立
    if (window.top != window.self) {
        window.top.location = "/Home/Login";
    }
    $("#showlogin").slideDown(800);

    $("#UserLoginName").keyup(check_login);
    $("#UserLoginPwd").keyup(check_login);
    $("#Rename").keyup(check_edit);
    $("#Repwd").keyup(check_edit);
    $("#Newpwd").keyup(check_edit);
    $("#ReNewpwd").keyup(check_edit);
            //@*帮助*@
           var help = $('<div class="am-popup" id="my-popup">  <span style="margin-left:280px; margin-right:250px;">查看帮助</span>    <span data-am-modal-close class="am-close">X</span>   <div class="am-popup-inner">  <div class="am-popup-bd">  <h3>   进销存管理系统（PSS）<h3> &nbsp;&nbsp;进销存管理系统是对企业生产经营中物料流、资金流进行条码全程跟踪管理，从接获订单合同开始，进入物料采购、入库、领用到产品完工入库、交货、回收货款、支付原材料款等，每一步都为您提供详尽准确的数据。有效辅助企业解决业务管理、分销管理、存货管理、营销计划的执行和监控、统计信息的收集等方面的业务问题。<br /><br/>&nbsp;&nbsp;&nbsp;&nbsp;在信息技术的催化之下，世界经济的变革已经进入了历史阶段。世界经济一体化，企业经营全球化，以及高度竞争造成的高度个性化与迅速改变的客户需求，令企业与顾客、企业与供方的关系变得更加密切和依赖。强化管理，精细管理，规范业务流程，提高透明度，提升管理品质，加快商品资金周转，以及为流通领域信息管理全面网络化打下基础，是中小企业乃至众多商家梦寐以求的愿望。 </div>  </div></div>');
           $("body").append(help);
           $("#editpwd").click(function () {
               tmm_loeading("editpwdloeading","修改密码中，请稍后...");
               $("#editpwdloeading").modal();
               var name = $("#Rename").val();
               var pwd = $("#Repwd").val();
               var newpwd = $("#Newpwd").val();
               var jsstr = { "name": name, "pwd": pwd, "newpwd": newpwd };
               $.post("/Home/EditPwd", jsstr, function (data) {
                   $("#editpwdloeading").modal('close');
                   if (data < 1) {
                       tmm_alert("alerterror", "提示", "账号或密码错误，修改失败！")
                       $("#alerterror").modal({ width: 350 });
                   } else {
                       tmm_modal("tmd1", "提示", "修改成功，请用密码登陆。")
                       $("#tmd1").modal({ width: 350 });
                   }
               });
           });
           $("#loginsub").click(function () {
               $("#modeling").modal({ closeViaDimmer: 0 });
               var uname = $("#UserLoginName").val();
               var upwd = $("#UserLoginPwd").val();
               var udname = $("#UserLoginName").parent();
               var udpwd = $("#UserLoginPwd").parent();
               if (uname.length < 6 || uname.length > 15) {
                   udname.removeClass("am-form-success").addClass("am-form-error");
                   $("#Unameck").removeClass("am-icon-check").addClass("am-icon-error").show();
                   $("#UserLoginName").focus();
                   return;
               } else {
                   udname.removeClass("am-form-error").addClass("am-form-success ");
                   $("#Unameck").removeClass("am-icon-error").addClass("am-icon-check");
               }
               if (upwd.length < 3 || uname.length > 20) {
                   udpwd.removeClass("am-form-success").addClass("am-form-error");
                   $("#Upwdck").removeClass("am-icon-check").addClass("am-icon-error").show();
                   $("#UserLoginPwd").focus();
                   return;
               } else {
                   udpwd.removeClass("am-form-error").addClass("am-form-success ");
                   $("#Upwdck").removeClass("am-icon-error").addClass("am-icon-check");
               }
               $("#hico").removeClass("hi");
               $.post("/Home/Login", { UserLoginName: uname, UserLoginPwd: upwd }, function (data) {
                   $("#modeling").modal('close');
                   if (data == "login_no") {
                       new tmm_alert("login_no", "提示", "账号或密码错误，登陆失败！");
                       
                       $("#hico").addClass("hi");
                       $("#login_no").modal({width:350});
                   } else if (data == "login_yes") {
                       //window.close();
                      // window.open("/Home/Index1", "", "fullscreen,scrollbars");
                       window.location.href = "/Home/Index1";
                   } else if (data = "login_tf") {
                       new tmm_alert("login_tf", "提示", "改账号已被禁用，若要继续操作，请联系系统管理员！");

                       $("#hico").addClass("hi");
                       $("#login_tf").modal({ width: 350 });
                   }
                 
               });
           });
           $("#UserLoginName").focus();
           //背景图片渐变色
           $.backstretch([
               "../Scripts/assets/img/1.jpg",
                "../Scripts/assets/img/3.jpg",
                "../Scripts/assets/img/4.jpg",
                "../Scripts/assets/img/5.jpg"
           ], {
               fade: 3000,
               duration: 5000
           });
}


function check_login() {
    var uname = $("#UserLoginName").val();
    var upwd = $("#UserLoginPwd").val();
    var udname = $("#UserLoginName").parent();
    var udpwd = $("#UserLoginPwd").parent();
    if (uname.length < 6 || uname.length > 15) {
        udname.removeClass("am-form-success").addClass("am-form-error");
        $("#Unameck").removeClass("am-icon-check").addClass("am-icon-error").show();
        //$("#UserLoginName").focus();
        return;
    } else {
        udname.removeClass("am-form-error").addClass("am-form-success ");
        $("#Unameck").removeClass("am-icon-error").addClass("am-icon-check");
    }
    if (upwd.length < 3 || uname.length > 20) {
        udpwd.removeClass("am-form-success").addClass("am-form-error");
        $("#Upwdck").removeClass("am-icon-check").addClass("am-icon-error").show();
        //$("#UserLoginPwd").focus();
        return;
    } else {
        udpwd.removeClass("am-form-error").addClass("am-form-success ");
        $("#Upwdck").removeClass("am-icon-error").addClass("am-icon-check");
    }
}

function check_edit() {
    $("#editpwd").addClass("am-disabled");
    var name = $("#Rename").val();
    var pwd = $("#Repwd").val();
    var newpwd = $("#Newpwd").val();
    var repwd = $("#ReNewpwd").val();
    if (name.length < 6 || name.length > 15 || pwd.length < 3 || pwd.length > 20 || newpwd.length < 3 || newpwd.length > 20 || repwd.length < 3 || repwd.length > 20 || repwd != newpwd) {
        return;
    }
    $("#editpwd").removeClass("am-disabled");
}

