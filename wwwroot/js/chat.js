var addContactBtn = document.getElementById('add_chat_user')
var createContact = document.getElementById('create-contact')
addContactBtn.addEventListener('click', function () {
    createContact.classList.add('active')
})
var closeAddBox = function() {
    createContact.classList.remove('active')
}

var active_chat_user = '';
var createContact = document.getElementById('create-contact')

addContactBtn.addEventListener('click', function () {
    createContact.classList.add('active')
})

function chatWith(element, id) {
    window.location.href = '/' + id
    const old_active_chat = document.getElementById("active_chat");

    if (typeof (old_active_chat) != 'undefined' && old_active_chat != null) {
        old_active_chat.removeAttribute('id');
    }
    element.setAttribute('id', 'active_chat');
}