var addContactBtn = document.getElementById('add_chat_user')
var createContact = document.getElementById('create-contact')
addContactBtn.addEventListener('click', function () {
    createContact.classList.add('active')
})
var closeAddBox = function() {
    createContact.classList.remove('active')
}

var createContact = document.getElementById('create-contact')

addContactBtn.addEventListener('click', function () {
    createContact.classList.add('active')
})
let id_active_contact = null;

$('.chat_list').click(function (e) {
    console.log("enter\n");
    e.preventDefault();
    if (id_active_contact != null) {
        $('.chat_list#active_chat').removeAttr("id");;
    }
    id_active_contact = $(this).data('contact-id');
    $(this).attr('id', 'active_chat');
    window.location.href = '/' + id_active_contact;
        //'@Url.Action("Chat", "Home", new {id = "ID"})'.replace("ID", id_active_contact);

});ghjh
let newSensMsg =
    `<div class="incoming_msg">
        <div class="received_msg">
            <div class="received_withd_msg">
                <p>@message.Text</p>
                <span class="time_date"> @message.sendTime</span>
            </div>
        </div>
    </div>`;
let newReceiveMsg =
    `<div class="outgoing_msg">
        <div class="sent_msg">
            <p>What's up?</p>
            <span class="time_date"> 14:43 | April 7</span>
        </div>
    </div>`;
