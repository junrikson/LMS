<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="404.aspx.vb" Inherits="LMS._404" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title runat="server">404 Not Found - Logistics Management System</title>
    <meta content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" name="viewport">
    <link id="Link1" rel="shortcut icon" href="~/images/favicon.ico" runat="server" />
    <!-- Bootstrap 3.3.6 -->
    <link id="Link2" rel="stylesheet" href="~/bootstrap/css/bootstrap.min.css" runat="server" />
    <!-- Font Awesome -->
    <link id="Link3" rel="stylesheet" href="~/plugins/font-awesome/css/font-awesome.min.css" runat="server" />
    <style>
        .error-page
        {
            padding: 40px 15px;
            text-align: center;
        }

        .error-actions
        {
            margin-top: 15px;
            margin-bottom: 15px;
        }

            .error-actions .btn
            {
                margin-right: 10px;
            }

        h1.404error
        {
            font-size: 100px !important;
        }
    </style>
</head>
<body>
    <div class="error-page">
    <h2>Oops!</h2>
    <h1 class="404error"> 404 </h1>
    <h2>Not Found</h2>
    <div class="error-details">
        Sorry, an error has occured. Requested page not found!
    </div>    
    
    <div class="error-actions">
        <a href="~/index.aspx" class="btn btn-primary btn-lg" runat="server"><span class="glyphicon glyphicon-home"></span>
            Take Me Home </a><a href="mailto:junrikson@gmail.com" class="btn btn-default btn-lg"><span class="glyphicon glyphicon-envelope"></span> Contact Support </a>
    </div>
</div>
</body>
</html>
