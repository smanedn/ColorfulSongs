const getUsername = (toSearch, checked) => {
    let toFetch = URL + "ajax/getUsername";

    if(toSearch == ""){
        toSearch = "*";
    }

    if (toSearch){
        toFetch += "/" + toSearch;
    }

    console.log("toSearch: " + toSearch);
    console.log(checked);
    console.log(toFetch);

    fetch(toFetch, {
        method: 'GET',
        headers: { 'Content-Type': 'application/json' }
    }).then(function(response) {
        if (!response.ok) {
            console.error("response not OK")
        }
        return response.json();
    }).then(function(data) {
        console.log(data);

        // const table = document.getElementById("username");
        // if (table.childElementCount > 1){
        //     cleanUsername();
        // }

        displayUser(data);

    }).catch(function(error) {
        console.error('Error:', error);
    });
};

const cleanUsername = () => {
    const table = document.getElementById('username');
    const rows = table.rows;

    for (let i = rows.length - 1; i >= 0; i--) {
        table.deleteRow(i);
    }
}

// const order = () => {
//
// }