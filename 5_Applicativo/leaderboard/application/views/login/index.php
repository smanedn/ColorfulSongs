<!doctype html>
<html lang="en">
<head>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <title>Leaderboard</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet">
    <script src="https://kit.fontawesome.com/f7749cdce8.js" crossorigin="anonymous"></script>
</head>
<body>
    <div class="container-sm p-0 row mx-auto mt-5 rounded-4 rounded-end-4 shadow-lg">
        <div class="col p-5">
            <p class="text-center fw-bold h3">LOGIN</p>
            <p class="text-bg-danger text-center"><?php if(isset($error)) echo $error ?></p>
            <form method="POST" action="<?php echo URL; ?>login/logIn">
                <div class="input-group mb-3">
                    <span class="input-group-text" id="inputGroup-sizing-default"><i class="fa-regular fa-user"></i></span>
                    <input type="text" class="form-control" placeholder="Username" name="username" aria-describedby="inputGroup-sizing-default">
                </div>
                <div class="input-group mb-3">
                    <span class="input-group-text" id="inputGroup-sizing-default"><i class="fa-solid fa-lock"></i></span>
                    <input type="password" class="form-control" placeholder="Password" name="password" aria-describedby="inputGroup-sizing-default">
                </div>
                <div class="text-center">
                    <button type="submit" class="btn btn-info rounded-5 text-white" name="login" value="Login">Login</button>
                </div>

            </form>
        </div>
        <div class="col bg-info text-center rounded-end-4 text-white p-5">
            <h1>MagicPortal</h1>
        </div>
    </div>

</body>
</html>
