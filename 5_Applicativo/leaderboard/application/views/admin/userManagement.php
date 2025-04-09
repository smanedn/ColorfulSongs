<body>
    <nav class="navbar navbar-expand-lg bg-dark" >
        <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarNav" aria-controls="navbarNav" aria-expanded="false" aria-label="Toggle navigation">
            <span class="navbar-toggler-icon"></span>
        </button>
        <div class="collapse navbar-collapse" id="navbarNav">
            <ul class="navbar-nav">
                <li class="nav-item">
                    <a class="nav-link <?php if(str_ends_with($_SERVER['REQUEST_URI'],'/')){echo 'text-white';}else{echo'text-secondary';} ?>"  href="<?php echo URL ?>leaderboardController">Leaderboard <span class="sr-only">(current)</span></a>
                </li>
                <li class="nav-item">
                    <a class="nav-link <?php if(str_ends_with($_SERVER['REQUEST_URI'],'admin')){echo 'text-white';}else{echo'text-secondary';} ?>" href="<?php echo URL ?>admin">Gestione Utenti</a>
                </li>
            </ul>
        </div>
    </nav>

    <div class="col bg-dark bg-opacity-50 text-center text-white p-5">
        <h1>Leaderboard <?php if (isset($checked)) echo $checked; ?></h1>
        <div class="container-md bg-dark bg-opacity-25 rounded-3 pt-4 pb-1">
            <table class="table table-bordered">
                <thead>
                <tr>
                    <th>Username</th>
                    <th>High Score</th>
                    <th>Map Code</th>
                    <th></th>
                </tr>
                </thead>
                <tbody>
                <?php if (isset($error)) : echo "" ?>
                <?php else :
                    foreach ($leaderboard as $leaderboardValue) : ?>
                        <tr>
                            <td><?php echo $leaderboardValue->username; ?></td>
                            <td><?php echo $leaderboardValue->score; ?></td>
                            <td><?php echo $leaderboardValue->dungeon_id; ?></td>
                            <td><a href="<?php echo URL . 'admin/delete/'. $leaderboardValue->id; ?>" class="btn btn-danger"
                                   onclick="return confirm('Sicuro?')">Delete</a></td>
                        </tr>
                    <?php endforeach; ?>
                <?php endif; ?>
                </tbody>
            </table>
        </div>
    </div>
</body>