var id = 0;

$(document).ready(function () {
    getPerson();
    showMeetByEmailVisitor()
    $("#saveVisit").click(function () {
        saveVisit()
    })

});

function getPerson() {
    console.log("getPerson called"); $.post("getPerson", {}, function (data) {
        if (data.Message) {
            $("#person").html(data.Message);
            console.log("Select element added to the DOM");

            // Debug log to ensure element is present
            if ($("#personSelect").length) {
                console.log("personSelect exists");
            } else {
                console.error("personSelect does not exist");
            }

            // Use event delegation for binding the change event
            $("#person").on('change', '#personSelect', function () {
                var selectedId = $(this).val();
                console.log("Selected ID:", selectedId);
                if (selectedId) {
                    id = selectedId
                    console.log("Valid selected ID:", selectedId);
                }
            });
        } else {
            console.log("No message received");
        }
    }).fail(function (xhr, status, error) {
        console.error("Error in getPerson:", status, error);
    });
}


function saveVisit() {

    $.post("saveMeeting", {
        name: $("#name").val(),
        email: $("#email").val(),
        org: $("#orgname").val(),
        phone: $("#mobile").val(),
        date: $("#date").val(),
        time: $("#time").val(),
        about: $("#about").val(),
        m_id: id

    }, function (data) {
        if (data.Messae != "") {
            console.log("Data Saved Successfully");
        }
    });
}

function showMeetByEmailVisitor() {

    $.post("getMeetByEmailVisitor", {}, function (data) {
        if (data.Message != "") {
            $("#meetEmailVisitor").html(data.Message)
        }
    })

}