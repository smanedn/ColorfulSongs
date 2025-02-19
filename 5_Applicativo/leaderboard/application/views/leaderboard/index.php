<!doctype html>
<html lang="en">
<head>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <title>Leaderboard</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet">
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js"></script>
    <script src="https://kit.fontawesome.com/f7749cdce8.js" crossorigin="anonymous"></script>
</head>
<body>
    <div class="container-fluid p-0 row mx-auto mt-5 bg-dark bg-opacity-50">
        <div class="col-2 p-3 bg-success bg-opacity-50 text-start">
            <h1 class="fw-bold">Filter</h1>
            <form id="myForm" method="POST" action="<?php echo URL; ?>leaderboard/radioFilter">
                <div class="form-check">
                    <input class="form-check-input" type="radio" name="type" id="friendRadio" value="friend" onchange="this.form.submit()"
                        <?php if($checked == "friend"){?> checked <?php }?>
                    >
                    <label class="form-check-label" for="friendRadio">Friends</label>
                </div>
                <div class="form-check">
                    <input class="form-check-input" type="radio" name="type" id="globalRadio" value="global" onchange="this.form.submit()"
                        <?php if($checked == "global"){ ?> checked <?php }?>
                    >
                    <label class="form-check-label" for="globalRadio">Global</label>
                </div>

                <section class="mt-3">
                    <div>
                        <label for="searchMapCode" class="form-label fw-bold fs-6">Maps</label>
                        <input class="form-control" id="searchMapCode" placeholder="Map Code">
                        <i class="fa-solid fa-magnifying-glass"></i>
                    </div>
                </section>
            </form>

        </div>
        <div class="col bg-dark bg-opacity-10 text-center text-white p-5">
            <h1>Leaderboard <?php echo $checked; ?></h1>
            <a href="<?php echo URL ?>login/logout" class="btn btn-outline-dark">Logout</a>
            <div class="container-md bg-dark bg-opacity-25 rounded-3 pt-4 pb-1">
                <table class="table table-bordered">
                    <thead>
                        <tr>
                            <th>Username</th>
                            <th>High Score</th>
                            <th>Map Code</th>
                        </tr>
                    </thead>
                    <tbody>
                    <?php foreach ($leaderboard_data as $leaderboardValue) : ?>
                        <tr>
                            <td><?php echo $leaderboardValue->getUsername(); ?></td>
                            <td><?php echo $leaderboardValue->getScore(); ?></td>
                            <td><?php echo $leaderboardValue->getMapCode(); ?></td>
                        </tr>
                    <?php endforeach; ?>
                    </tbody>
                </table>
            </div>
        </div>
    </div>
</body>
</html>
