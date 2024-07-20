var dId =1

$(document).ready(function () {
    getDesignation()
    
    $("#submitAdmin").click(function () {
        addAdmin()
    })

    $("#login").click(function () {
        logIn()
    })

})

function getDesignation() {

   

    $.post("getDesignation", {}, function (data) {
        if (data.Message != null) {

            $("#designation").html(data.Message)

            alert(data.Message)
        }
    })
}

function addAdmin() {

    $.post("addCAdmins", {
        name: $("#name").val(),
        email: $("#email").val(),
        phone: $("#mobile").val(),
        self: $("#about").val(),
        password: $("#password").val(),
        dId: dId
    }, function (data) {
        if (data.Message != "") {
            console.log(data.Message)
        }
    }
    )

}

function logIn(){

    $.post("getVerifyUser", {
        email: $("#email").val(),
        password: $("#password").val()
    },

        function (data) {
            if (data.Message != "") {
                alert(data.Message)
                console.log(data.Message)
                window.location = "/AdminPage/AdminPages";
            }
        }

    )

}




function getMeeting() {
    

    $.post("getMeet", {}, function (data) {

        if (data.Message != "") {
            $("#meet").html(data.Message)
        }
    })
}
