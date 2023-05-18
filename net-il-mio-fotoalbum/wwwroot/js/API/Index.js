loadPhotos();

function loadPhotos(searchKey) {
    axios.get('/API/ApiHome', {
        params: {
            search: searchKey
        }
    })
        .then((res) => {  //se la richiesta va a buon fine
            console.log('Risposta ok', res);
            //if (res.data.length == 0) {  //non ci sono post da mostrare => nascondo la tabella
            //    document.getElementById('post-table').classList.add('d-none');
            //    document.getElementById('no-posts').classList.remove('d-none');
            //} else {  //ci sono post da mostrare => visualizzo la tabella
            //    document.getElementById('post-table').classList.remove('d-none');
            //    document.getElementById('no-posts').classList.add('d-none');

            //svuoto la tabella
            document.getElementById('photos').innerHTML = '';

            res.data.forEach(photo => {
                if (photo.visible == true) {
                    document.getElementById('photos').innerHTML +=
                        `
                        <div class="col">
                            <div class="card shadow-sm">
                                <h3 class="">${photo.title}</h3>
                                
                                <img class="bd-placeholder-img card-img-top" src="${photo.image}" alt="" />

                                <p card-text>${photo.description}</p>

                                <div class="d-flex justify-content-between align-items-center">
                                    <div class="btn btn-sm btn-outline-secondary">View</div>

                                    <div>
                                        <div class="btn btn-sm btn-outline-secondary">Edit</div>
                                        <div class="btn btn-sm btn-outline-secondary">Delete</div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        `;
                }
            })
        //}
        })
        .catch((res) => {  //se la richiesta non è andata a buon fine
            console.error('errore', res);
            alert('errore nella richiesta');
        });

}