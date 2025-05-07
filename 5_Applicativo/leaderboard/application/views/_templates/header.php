<!doctype html>
<html lang="en">
<head>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <title>Leaderboard</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet">
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js"></script>
    <script src="https://kit.fontawesome.com/f7749cdce8.js" crossorigin="anonymous"></script>

    <script>
        const getUsername = (toSearch,<?php echo $checked ?>) => {
            let toFetch = "<?php echo URL;?>ajax/getUsername";

            if(toSearch == ""){
                toSearch = "*";
            }

            if (toSearch){
                toFetch += "/" + toSearch;
            }

            console.log("toSearch: " + toSearch);
            console.log(<?php echo $checked ?>);
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

                const table = document.getElementById("username");
                if (table.childElementCount > 1){
                    cleanUsername();
                }

                Object.keys(data).forEach((element) => {
                    const row = document.createElement('tr');

                    Object.keys(data[element]).forEach((cellData) => {
                        if(cellData != "id") {
                            const cell = document.createElement('td');
                            console.log(cellData)
                            //if(cellData == "friendRequest"){
                            //    const btn = document.createElement("a");
                            //    const href = "<?php //echo URL . 'friendController/friendRequest/'. $leaderboardValue->id; ?>//" + data[element]["id"];
                            //    btn.href = href;
                            //    btn.textContent = data[element][cellData];
                            //    cell.appendChild(btn);
                            //}else{
                                cell.textContent = data[element][cellData];
                            // }

                            row.appendChild(cell);
                        }
                    });
                    table.appendChild(row);
                });
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

        const order = () => {

        }

    </script>

</head>
