$(document).ready(function () {

    getPerson()


})

function getPerson() {
    $.post("getPerson", {},

        function (data) {
            if (data.Message != "") {
                $("#person").html(data.Message)
            }
        }
    )
}