const uri = 'api/pdfcreator';

function addItem() {
    const addFirstName = document.getElementById('fName');
    const addSurname = document.getElementById('surname');
    const addBody = document.getElementById('body');
    const alert = document.getElementById('alert');

    const item = {
        "FirstName": addFirstName.value.trim(),
        "Surname": addSurname.value.trim(),
        "Body": addBody.value.trim(),
    };

    fetch(uri, {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(item)
    })
        .then(response => response.json())
        .then(() => {
            addFirstName.value = '';
            addSurname.value = '';
            addBody.value = '';

            alert.classList.remove('d-none');
        })
        .catch(error => console.error('Unable to generate PDF.', error));
}