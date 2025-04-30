
<body>
    <header class="container-fluid text-center">
        <div class="row">
            <div class="col">
                <h1>Colorful Songs</h1>
                <h3>Welcome <?php echo $_SESSION['username'] ?>!</h3>
            </div>
            <div class="col-1 mt-3 p-0">
                <a href="<?php echo URL ?>login/logout" class="btn btn-outline-dark ">Logout</a>
            </div>
        </div>
    </header>
    <div class="container-fluid p-0 row mx-auto mt-5">
        <div class="col-2 p-3 text-start">
            <h1 class="fw-bold">Filter</h1>
            <form id="radioFilterForm" method="POST" action="<?php echo URL; ?>leaderboardController/radioFilter">
                <div class="form-check">
                    <input class="form-check-input" type="radio" name="type" id="friendRadio" value="friend" onchange="this.form.submit()"
                        <?php if(isset($_SESSION['type']) && $_SESSION['type'] == "friend"){?> checked <?php }?>>
                    <label class="form-check-label" for="friendRadio">Friends</label>
                </div>
                <div class="form-check">
                    <input class="form-check-input" type="radio" name="type" id="globalRadio" value="global" onchange="this.form.submit()"
                        <?php if(isset($_SESSION['type']) && $_SESSION['type'] == "global"){ ?> checked <?php }?>>
                    <label class="form-check-label" for="globalRadio">Global</label>
                </div>
            </form>

            <form method="POST" action="<?php echo URL; ?>leaderboardController/searchFilter">
                <section class="mt-3">
                    <div>
                        <label for="mapCode" class="form-label fw-bold fs-6">Maps</label>
                        <div class="d-inline-flex">
                            <input class="form-control w-75" name="mapCode" placeholder="Map Code">
                            <button type="submit" class="btn btn-outline-dark ms-2" name="search" value="Search"><i class="fa-solid fa-magnifying-glass"></i></button>
                        </div>
                        <p class="text-danger w-75"><?php if(isset($error)) echo $error ?></p>
                    </div>

                    <button type="submit" class="btn btn-outline-dark ms-2" name="deleteFilter" value="deleteFilter">Delete Filter</button>
                </section>


            </form>

        </div>
        <div class="col bg-dark bg-opacity-50 text-center text-white p-5">
            <h1>Leaderboard <?php if (isset($checked)) echo $checked; ?></h1>
            <div class="container-md bg-dark bg-opacity-25 rounded-3 pt-4 pb-1">
                <table class="table table-bordered">
                    <thead>
                        <tr>
                            <th>Username</th>
                            <th>High Score</th>
                        </tr>
                    </thead>
                    <tbody>
                    <?php if (isset($error)) : echo "" ?>
                    <?php else :
                        foreach ($leaderboard_data as $leaderboardValue) : ?>
                        <tr>
                            <td><?php echo $leaderboardValue->username; ?></td>
                            <td><?php echo $leaderboardValue->score; ?></td>
                        </tr>
                    <?php endforeach; ?>
                    <?php endif; ?>
                    </tbody>
                </table>
            </div>
        </div>
    </div>
</body>
</html>
