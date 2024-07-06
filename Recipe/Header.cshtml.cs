<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Document</title>
</head>
<body>
    @if (User.Identity.IsAuthenticated)
    {
        <div>
            <p>Hello, @User.Identity.Name</p>
            <form asp-action="Logout" method="post">
                <button type="submit">Logout</button>
            </form>
        </div>
    }
    else
    {
        <p>You are not logged in.</p>
    }
</body>
</html>
