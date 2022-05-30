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
let Server_active_contact = null;
let UserName = null;

$('.chat_list').click(function (e) {
    e.preventDefault();
    if (id_active_contact != null) {
        $('.chat_list#active_chat').removeAttr("id");;
    }
    id_active_contact = $(this).data('contact-id');
    Server_active_contact = $(this).data('contact-server');
    console.log(Server_active_contact);
    UserName = $(this).data('user-id');

    $(this).attr('id', 'active_chat');
    window.location.href = '/' + id_active_contact;
        //'@Url.Action("Chat", "Home", new {id = "ID"})'.replace("ID", id_active_contact);

});
