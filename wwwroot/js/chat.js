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
    e.preventDefault();
    if (id_active_contact != null) {
        $('.chat_list#active_chat').removeAttr("id");;
    }
    id_active_contact = $(this).data('contact-id');
    $(this).attr('id', 'active_chat');
    window.location.href = '/' + id_active_contact;
        //'@Url.Action("Chat", "Home", new {id = "ID"})'.replace("ID", id_active_contact);

});

$('#add-body-form').submit(async function (e) {
    e.preventDefault();
    const contactid = $('#addUserName').val();
    const Name = $('#addNickName').val();
    const Server = $('#addServer').val();
    const userId = '@ViewBag.UserName';
    const localServer = "localhost:5005";
    const r1 = await fetch('http://localhost:5005/api/contacts', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify({ id: contactid, name: Name, server: Server })
    });
    /*
    const r2 = await fetch('http://localhost:5005/api/invitations', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify({ from: userId, to: contactid, server: localServer })
    })
    .catch((error) => {
        console.error('Error:', error);
    });;
    */
    
    window.location.href = '/' + contactid;
});
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
