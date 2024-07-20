$(document).ready(function () {
    getMeeting()
})


function getMeeting() {
    
    console.log("ad")

    $.post("getMeet", {}, function (data) {
        
        if (data.Message != "") {
            $("#meet").html(data.Message)
        }
    })
}

function AcceptButton() {

   

    $("#model_dialog").dialog({
            title: "jQuery Modal Dialog Popup",
            buttons: {
                Close: function () {
                    $(this).dialog('close');
                }
            },
            modal: true
        });
   
}
