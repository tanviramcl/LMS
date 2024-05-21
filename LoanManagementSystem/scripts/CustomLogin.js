$("#login").validate({
    submitHandler: function () {
           
        Submit();
    },
    rules: {
        userName: {
            required: true,

        },
        password: {
            required: true
            
        }


    },
    messages: {
        userName: {
            required: "Please enter a Name"
        },
        password: {
            required: "Please provide a Password",
        }

    }
});
function Submit() {
   // alert();
    //Master
    var jsonObjU = [];
    var username = $("#userName").val();
    var password = $("#password").val();
   
    var itemU = {};
    itemU["UserName"] = username;
    itemU["PassWord"] = password;
    
    jsonObjU.push(itemU);

    $.ajax({

        url: '/Login/Login',
        type: 'POST',
        dataType: 'json',
        contentType: 'application/json',
        cache: false,
        data: JSON.stringify({ loginMdl: JSON.stringify(jsonObjU) }),
        success: function (s) {
            if (s="SUCCESS") {
                window.location.href = "../Home/" //urlObj2.GenerateUrl();
            } else {
                alert(s);
            }
          
        }
    });
}




