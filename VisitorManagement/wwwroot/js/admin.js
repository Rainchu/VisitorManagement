var senderEmail1 = "rainchu786420@gmail.com";
var recieverEmail = ""

var body = "<h3 style='color:red;'>Sorry, I am not available for this time</h3>"
var m_Id = "";

$(document).ready(function () {

    getMeeting()
    showMeetByEmail()
    $("#send").click(function () {
        sendEmail()
    });
});


function getMeeting() {
    
  

    $.post("getMeet", {}, function (data) {
        
        if (data.Message != "") {
            $("#meet").html(data.Message)


           
            $(".card-disabled").each(function () {
                
                $(this).find("button").attr("disabled", true); // Disable buttons within the card
                $(this).find("textarea").attr("disabled", true); // Ensure textarea is disabled
            });
        }
    })
}

function AcceptButton(senderEmail,recEmail,mId) {

   /* senderEmail1 = senderEmail;*/
    recieverEmail = recEmail;
    m_Id = mId;
    console.log("Updated Successfully : " + mId)
   
    $("#model_dialog").css("display","block")   
   
}
function sendEmail() {

    console.log("Updated Successfully : " + m_Id)

    $.post("sendEmail", {

        fromEmail: senderEmail1,
        toEmail: recieverEmail,
        subject: $("#subject").val(),
        body: $("#body").val(),
        m_id: m_Id
    }, function (data) {
        console.log("Updated Successfully")

        if (data.Message != "") {
            console.log("Updated Successfully")
        }
    })
}

function RejectRequest(email, id) {
    console.log(email + " : " + id)
    $.post("rejectEmail", {
        fromEmail: senderEmail1,
        toEmail: email,
        subjet: "Regarding Meeting ",
        body: body,
        m_id: id
    }, function (data) {
        if (data.Message != "") {

            console.log(data.Message)
            console.log("You Reject the Request")
        }
    })
}


function showMeetByEmail() {

    $.post("getMeetByEmail", {}, function (data) {
        if (data.Message != "") {
            $("#meetEmail").html(data.Message)
        }
    })

}