//Global Variable
var last_chat_id = 0;
//When resizing windows then recalculate chat window
$(window).resize(function () {
    //set new height
    setHeightOutputContainer();
})

//When everything is reloaded 
$(document).ready(function () {
    //set new height
    setHeightOutputContainer();
    //clear localstorage
    localStorage.clear();
    //every 5 second get message if user logged in 
    setInterval(function () { getMessages() }, 5000);
    //when user presse enter in username field it should do the same as button
    $("#username").keypress(function (e) {
        //keycode 13 is enter 
        if (e.keyCode == 13) {
            loggeInFunction();
        }
    });
    //when click on submit button 
    $("#submit_btn").click(function () {
        loggeInFunction();
    });
    //when pressed enter in input field should do the same as suvmit button
    $("#chat-input").keypress(function (e) {
        if (e.keyCode == 13) {
            // console.log("Enter pressed in input field");
            //get the value of the element I can use this because its in the input field
            let inputvalue = this.value;
            sendMessage(inputvalue);
        }
    });
    //send button 
    $("#sendTxtBtn").click(function () {
        console.log("Klik")
        //need to get the value with jquery
        let inputvalue = $("#chat-input").value;
        sendMessage(inputvalue);
    });

});

//function to check username
function checkUsername(input) {
    //Trim input so all space in each side will be trimmed 
    let trimmedInput = $.trim(input);
    // console.log(trimmedInput);
    //If input is empty return false and make an alert message
    if (trimmedInput == null || trimmedInput == "") {
        alert("Fejl");
        return false;
    }
    else {
        return true;
    }
}

function loggeInFunction() {
    //save username as a cookie
    var name = $("#username").val();
    //if username is valid 
    if (checkUsername(name)) {
        //save username in local storage
        localStorage.setItem("username", name);
        // console.log(localStorage.getItem("username"));
        //Hide Login div
        $("#login-container").hide();
        //Run showchat function
        showChatContainer();
    }
}
//get messages
function getMessages() {
    // console.log("Getmessages");
    if (isUserLoggedIn()) {
        //use ajax to make request
        $.ajax({
            url: "http://chillyskye.dk/api_v2/",
            type: "get",
            data: { amount: 100, last_chat_id },
            dataType: "json",
            success: function (result) {
                console.log("success");
                displayMessage(result);
            }
        });
    }
}
function isUserLoggedIn() {
    if (localStorage.getItem("username") != null && ischatroomvisible()) return true;
    return false;
}

function ischatroomvisible() {
    if ($("#chat-container").css("display") == "none") return false;
    //else return true
    return true;
}

function sendMessage(textmessage) {
    let username = localStorage.getItem("username");

    $.ajax({
        url: "http://chillyskye.dk/api_v2/",
        type: "post",
        data: { name: username, message: textmessage },
        dataType: "json",
        success: function (result) {
            console.log(result);
            if (result.error == true) {
                alert(result.message);
                console.log("Fejl");
            }
            else {

                //clear chat input
                clearChatInput();
            }
        }
    });




}

function displayMessage(messages) {
    messages.forEach(message => {
        console.log(message);
        //read only new messages 
        if (last_chat_id >= parseInt(message.id)) {

        }
        else {
            last_chat_id = message.id;

            //create elements
            var messageWrapper = $("<div>").addClass("messagewrapper").appendTo($("#text-output-container"));

            if (message.name == localStorage.getItem("username")) {
                messageWrapper.addClass("myownmessage");
                var messageEl = $("<label>").addClass("messagetext").appendTo(messageWrapper);
                messageEl.html(message.message); // need to use html else output will be shown wrong it could show ' and "
                var usernameEl = $("<label>").addClass("messageUsername").insertAfter(messageEl);
                usernameEl.text(message.name);
            }
            else {
                var usernameEl = $("<label>").addClass("messageUsername").appendTo(messageWrapper);
                usernameEl.text(message.name);
                var messageEl = $("<label>").addClass("messagetext").insertAfter(usernameEl);
                messageEl.html(message.message);
            }
        }
        //convert timestamp * with 1000 so it came in seconds
        var dateobj = new Date(message.timestamp * 1000);
        var date = dateobj.getHours() + ":" + dateobj.getMinutes() + " " + dateobj.getDate() + "-" + (dateobj.getMonth() + 1);
        // console.log(date);
    });

}
//Show chat container function
function showChatContainer() {
    $("#chat-name").text(localStorage.getItem("username"));
    //show chat container
    $("#chat-container").show();
}
//make my website more responsible take 80% of parent height of outputcontainer
function setHeightOutputContainer() {
    let parentHeight = $("#chat-room").height()
    // console.log(parentHeight);
    let containerHeight = (parentHeight * 0.8);
    $("#relative-output-container").css({ 'height': `${containerHeight}` });
}
function clearChatInput() {
    console.log("Clear chat input");
    //Set value on chat input to "empty"
    $("#chat-input").val('');
}