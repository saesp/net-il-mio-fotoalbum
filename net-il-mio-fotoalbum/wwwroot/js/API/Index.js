loadPhotos();

function loadPhotos(searchKey) {
    axios.get('/API/ApiHome', {
        params: {
            search: searchKey
        }
    })
        .then((res) => {  //se la richiesta va a buon fine
            console.log('Risposta ok', res);
            
            document.getElementById('photos').innerHTML = ''; //svuoto la tabella

            res.data.forEach(photo => {
                if (photo.visible == true) {

                    document.getElementById('photos').innerHTML +=
                    `<div class="col">
                        <div class="card shadow-sm">
                            <h3 class="text-center">${photo.title}</h3>
                                
                            <img class="bd-placeholder-img card-img-top" src="${photo.image}" alt="" />

                            <p card-text>${photo.description}</p>
                        </div>
                    </div>`;
                }
            })
        })
        .catch((res) => {  //se la richiesta non è andata a buon fine
            console.error('errore', res);
        });
}