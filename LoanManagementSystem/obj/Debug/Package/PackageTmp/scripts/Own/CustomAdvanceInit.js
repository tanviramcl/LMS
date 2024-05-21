
    $.validator.addMethod("ddlAdvanceType", function (value, element, param) {
        if (value == '0')
            return false;
        else
            return true;
    }, "Please select a Advance.");

    $.validator.addMethod("ddlEmployee", function (value, element, param) {
        if (value == '0') {
            alert(ss);
            return false;
        }
        else
            return true;
    }, "Please select a Employee.");

    $("#form_AVD_int").validate({
        submitHandler: function () {

            Submit();
        },
        rules: {
            ddlAdvanceType: {
                ddlAdvanceType: true
            },
            EMPLOYEE_BASIC: {
                required: true,

            },
            ddlEmployee: {
                required: true,
                ddlEmployee: true
            },
            EMPLOYEE_JOIN_DATE: {
                required: true,

            },
            EMPLOYEE_RETIRE_DATE: {
                required: true,

            },
            SANCTION_AMT: {
                required: true, 

            },
            INSTALLMENT_AMT: {
                required: true,

            }

        },
        messages: {
            EMPLOYEE_BASIC: {
                required: "Please enter Your Basic"
            }

        }
    });
    function Submit() {
        var EMPLOYEE_ID = $("#EMPLOYEE_ID").val();
      
        if (EMPLOYEE_ID != "") {
            var jsonObjU = [];
            var AdvanceType = $("#ddlAdvanceType").val();
            var EMPLOYEE_ID = EMPLOYEE_ID;
            var EMPLOYEE_BASIC = $("#EMPLOYEE_BASIC").val();
            var EMPLOYEE_JOIN_DATE = $("#EMPLOYEE_JOIN_DATE").val();
            var EMPLOYEE_RETIRE_DATE = $("EMPLOYEE_RETIRE_DATE").val();
            var SANCTION_AMT = $("#SANCTION_AMT").val();
            var INSTALLMENT_AMT = $("#INSTALLMENT_AMT").val();

            var itemU = {};
            itemU["AdvanceType"] = AdvanceType;
            itemU["EMPLOYEE_ID"] = EMPLOYEE_ID;
            itemU["EMPLOYEE_BASIC"] = EMPLOYEE_BASIC;
            itemU["EMPLOYEE_JOIN_DATE"] = EMPLOYEE_JOIN_DATE;
            itemU["EMPLOYEE_RETIRE_DATE"] = EMPLOYEE_RETIRE_DATE;
            itemU["SANCTION_AMT"] = SANCTION_AMT;
            itemU["INSTALLMENT_AMT"] = INSTALLMENT_AMT;
           
            jsonObjU.push(itemU);

            $.ajax({

                url: '/Loan/AddAdvanceInitialization',
                type: 'POST',
                dataType: 'json',
                contentType: 'application/json',
                cache: false,
                data: JSON.stringify({ AvdInt: JSON.stringify(jsonObjU) }),
                success: function (s) {

                    if (s == "Success") {
                        alert(s);
                        window.location.href = "/Home/index";
                        $("#MyModal").modal("hide");
                    }
                    else {

                        alert("Error");
                    }
                }
            });


        }
        else
        {
            alert("Please Select a Employee");
        }
         
      

       

    }


//$("#SaveAvd_int_Record").click(function () {
//    alert();
//    var data = $("#SubmitForm").serialize();
//    $.ajax({
//        type: "Post",
//        url: "/Loan/AddAdvanceInitialization",
//        data: data,
//        success: function (result) {
//            alert("Success!..");
//            window.location.href = "/Home/index";
//            $("#MyModal").modal("hide");

//        }
//    })
//})