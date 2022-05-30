
$('#add-body-form').submit(async function (e) {
    e.preventDefault();
    const contactid = $('#addUserName').val();
    const Name = $('#addNickName').val();
    const Server = $('#addServer').val();
    const userId = $('#useridfrom').val();
    //const userId = '@ViewBag.UserName';
    const localServer = "localhost:5005";
    $('#ErrorMessage').text("bla");

    try {
        const response = await fetch('http://' + Server + '/api/invitations', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({ from: userId, to: contactid, server: localServer })
        });
        /*.catch((error) => {
            //'@ViewBag.MessageUserName' = error;
            console.error('Error:', error);
        });*/
        if (!response.ok) {
            $('#ErrorMessage').text("username error");

            //'@ViewBag.MessageUserName' = 'username error';
            const message = `An error has occured: ${response.status}`;
            //throw new Error(message);
        } else {
            await fetch('http://localhost:5005/api/contacts/', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify({ id: contactid, name: Name, server: Server })
            });
            window.location.href = '/' + contactid;
            //addContact(contactid, Name, Server);
        }
    } catch {
        //'@ViewBag.MessageUserName' = error;
        //console.error('Error:', error);
        $('#ErrorMessage').text("username hapend error");

    }
    

});
$('#create_message').submit(async function (e) {
    e.preventDefault();
    const contactid = $('#contactMessageid').val();
    const Server = $('#contactServer').val();
    const userId = $('#userMessageid').val();
    const Content = $('#messageContent').val();

    try {
        const response = await fetch('http://'+Server+'/api/transfer', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({ from: userId, to: contactid, content: Content })
        })
        if (!response.ok) {
            $('#ErrorMessage').text("username error");

            //'@ViewBag.MessageUserName' = 'username error';
            const message = `An error has occured: ${response.status}`;
            //throw new Error(message);
        } else {
            const url = 'http://localhost:5005/api/contacts/' + contactid + '/messages/';
            console.log(url);
            await fetch(url, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify({ content: Content })
            })
            window.location.href = '/' + contactid;
            //addContact(contactid, Name, Server);
        }
    } catch {
        //'@ViewBag.MessageUserName' = error;
        //console.error('Error:', error);
        $('#ErrorMessage').text("username hapend error");

    }
});