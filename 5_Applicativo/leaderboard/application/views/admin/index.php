<body>
    <nav class="navbar navbar-expand-lg bg-dark" >
        <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarNav" aria-controls="navbarNav" aria-expanded="false" aria-label="Toggle navigation">
            <span class="navbar-toggler-icon"></span>
        </button>
        <div class="collapse navbar-collapse" id="navbarNav">
            <ul class="navbar-nav">
                <li class="nav-item">
                    <a class="nav-link text-white"  href="<?php echo URL ?>leaderboardController">Leaderboard</a>
                </li>
                <li class="nav-item">
                    <a class="nav-link text-secondary" href="<?php echo URL ?>admin">Gestione Utenti</a>
                </li>
            </ul>
        </div>
    </nav>

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
                        <label for="usernameSearch" class="form-label fw-bold fs-6">Users</label>
                        <div class="d-inline-flex">
                            <input class="form-control w-75" name="usernameSearch" placeholder="Username...">
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
                        <th></th>
                    </tr>
                    </thead>
                    <tbody>
                        <?php foreach ($leaderboard_data as $leaderboardValue): ?>
                        <?php
                        $isFriend = in_array($leaderboardValue->id, $friendIds);
                        $alreadyFriend = $isFriend ? "Remove Friends" : "Send Friend Request";
                        ?>
                        <tr>
                            <td><?php echo $leaderboardValue->username ?></td>
                            <td><?php echo $leaderboardValue->score ?></td>
                            <td>
                                <?php if ($isFriend): ?>
                                    <a href="<?php echo URL . 'friendController/removeFriend/' . $leaderboardValue->id; ?>" class="btn btn-outline-warning" ><?php echo $alreadyFriend?></a>
                                <?php else: ?>
                                <a href="<?php echo URL . 'friendController/friendRequest/'. $leaderboardValue->id; ?>" class="btn btn-outline-info"><?php echo $alreadyFriend?></a></td>
                                <?php endif; ?>
                            </td>
                        </tr>
                        <?php endforeach; ?>
                    </tbody>
                </table>
            </div>
        </div>
    </div>
</body>