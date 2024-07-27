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
function logIn() {

    console.log("io")

    $.post("getVerifyUser", {
        email: $("#email").val(),
        password: $("#password").val()
    },
        function (data) {
            console.log(data.Message);
            if (data.Message != "") {
                if (data.Message == "ADMIN") {
                    window.location = "/AdminPage/AdminPages";
                } else if (data.Message == "VISITOR") {
                    window.location = "/Visitor/VisitorHome ";
                } else {
                    window.location = "/Recep/Recept";
                }
            }
        });
}




function getMeeting() {
    

    $.post("getMeet", {}, function (data) {

        if (data.Message != "") {
            $("#meet").html(data.Message)
        }
    })
}
