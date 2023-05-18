
function createMessage() {

    messageCreate = {
        subject: document.getElementById('subject').value,
        content: document.getElementById('content').value,
        email: document.getElementById('email').value
    };


    axios.post('/API/ApiHome', messageCreate)
        .then((res) => {
            alert('Messaggio inviato!');
            window.location.href = '/Client';
        })
        .catch((res) => {
            //gli errori sono in questo dictionary: res.response.data.errors
            for (let errorKey in res.response.data.errors) {
                // testo errore: res.response.data.errors[errorKey]
                let spanId = errorKey.toLowerCase() + "_validation";
                let span = document.getElementById(spanId);
                span.innerHTML = res.response.data.errors[errorKey];
                console.log('Errore: ' + res.response.data.errors[errorKey]);
            }
            console.error("errore", res);
        });
}