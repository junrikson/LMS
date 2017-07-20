<%@ Page Language="VB" AutoEventWireup="false" Inherits="LMS.Blank" Codebehind="Blank.aspx.vb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">

<head id="Head1" runat="server">
  <meta charset="utf-8">
  <meta http-equiv="X-UA-Compatible" content="IE=edge">
  <title id="lmsTittle" runat="server"></title>
  <!-- Tell the browser to be responsive to screen width -->
  <meta content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" name="viewport">
  <link id="Link1" rel="shortcut icon" href="~/images/favicon.ico" runat="server" />
  <!-- Bootstrap 3.3.6 -->
  <link id="Link2" rel="stylesheet" href="~/bootstrap/css/bootstrap.min.css" runat="server" />
  <!-- Font Awesome -->
  <link id="Link3" rel="stylesheet" href="~/plugins/font-awesome/css/font-awesome.min.css" runat="server" />
    <script type="text/javascript">
        function openWindow() {
            var winArgs = window.open("/menu/dialog2.aspx?&mode=MasterCustomer", '', "width=300,height=210");
            if (winArgs != null) {
                var text = winArgs[0];
                document.getElementById('Label1').value = text;
            }
        }
    </script>
</head>
<body class="mainmenu">
   
    <form id="Form1" runat="server">
    <div>
       <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
        <asp:Button ID="Button1" runat="server" Text="Button" OnClientClick="openWindow();return false;" />

    </div>
    </form>
<!-- jQuery 2.2.3 -->
<script src="/plugins/jQuery/jquery-2.2.3.min.js"></script>
<!-- jQuery UI 1.11.4 -->
<script src="/plugins/jQuery/jquery-ui.min.js"></script>
<!-- Resolve conflict in jQuery UI tooltip with Bootstrap tooltip -->
<script>
    $.widget.bridge('uibutton', $.ui.button);
</script>
<script>
    $("#info-alert").fadeTo(2000, 500).slideUp(500, function () {
        $("#info-alert").slideUp(500);
    });
</script>
<!-- Bootstrap 3.3.6 -->
<script src="/bootstrap/js/bootstrap.min.js"></script>
</body>
</html>
