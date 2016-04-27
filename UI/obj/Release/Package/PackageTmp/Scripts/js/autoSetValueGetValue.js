/*
         *
         * 表单自动取值、赋值插件
         * 表单类型：text radio select-one checkbox textarea
         * 表单必须设置name属性
         * 调用
         * 取值：formCore.getFormValues()
         * 赋值：formCore.setFormValues(json)
*/


var formCore = (function () {
    var getCbxValues = function (name) {
        var values = '';
        var cks = document.getElementsByName(name);

        for (var i = 0; i < cks.length; i++) {
            if (cks[i].checked) {
                values += cks[i].value + ',';
            }
        }

        if (values != '') {
            values = values.substring(0, values.length - 1);
        }

        return values;
    };

    var checkFieldId = function (id) {
        var obj = document.getElementById(id);

        if (obj == null) {
            alert("该区域ID对象不存在，请检查！");
            return false;
        }

        return true;
    }

    /*
    * 表单值"和\需要转义 如\\ \"
    */
    var checkIptValue = function (v) {
        return v.replace(/\\/g, '\\\\').replace(/"/g, '\\"');
    }

    var isInArray = function (arr, value) {
        for (var i = 0; i < arr.length; i++) {
            if (arr[i] == value) {
                return true;
            }
        }

        return false;
    }

    var setChecked = function (name, value) {
        var cks = document.getElementsByName(name);
        var arr = value.split(',');

        for (var i = 0; i < cks.length; i++) {
            if (isInArray(arr, cks[i].value)) {
                cks[i].checked = true;
            }
        }
    }


    var getFormValues = function (fieldId) {
        var ipts = null;

        if (fieldId != undefined) {
            if (checkFieldId(fieldId)) {
                ipts = $("#" + fieldId).find(":input");
            } else {
                return false;
            }
        } else {
            ipts = $(document.body).find(":input");
        }

        var json = '{';

        if (ipts.length > 0) {
            var obj = null;
            var oldCbxName = '',
                objName,
                objType;

            for (var i = 0; i < ipts.length; i++) {
                obj = ipts[i];
                objName = obj.name;
                objType = obj.type;

                if (objType == "text" || objType == "textarea") {
                    json += '"' + objName + '":"' + checkIptValue($(obj).val()) + '",';
                } else if (objType == "radio") {
                    if (obj.checked) {
                        json += '"' + objName + '":"' + $(obj).val() + '",';
                    }
                } else if (objType == "select-one") {
                    json += '"' + objName + '":"' + $(obj).val() + '",';
                } else if (objType == "checkbox") {
                    //相同类型checkbox只取一次
                    if (oldCbxName != objName) {
                        oldCbxName = objName;
                        json += '"' + objName + '":"' + getCbxValues(oldCbxName) + '",';
                    }
                }
            }

            json = json.substring(0, json.length - 1);
        }

        json += '}';
        return json;
    };


    var setFormValues = function (json) {
        if (json != undefined && json != null) {
            var obj = null,
                sel = null,
                objType = null;

            for (var a in json) {
                sel = ":input[name='" + a + "']";
                obj = $(sel);
                objType = obj[0].type;

                if (objType == "text" || objType == "select-one" || objType == "textarea") {
                    obj.val(json[a]);
                } else if (objType == "radio" || objType == "checkbox") {
                    setChecked(a, json[a]);
                }
            }
        }
    };

    return { getFormValues: getFormValues, setFormValues: setFormValues };
})();