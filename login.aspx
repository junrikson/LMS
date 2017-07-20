<%@ Page Language="vb" AutoEventWireup="false" Inherits="LMS.login" Codebehind="login.aspx.vb" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v7.3, Version=7.3.7.0, Culture=neutral, PublicKeyToken=49d90c14d24271b5"
	Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>

<!DOCTYPE html>
<html lang="en">
<head runat="server">
  <meta charset="utf-8">
  <meta http-equiv="X-UA-Compatible" content="IE=edge">

  <title>Login - Logistics Management System</title>

  <!-- Tell the browser to be responsive to screen width -->
  <meta content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" name="viewport">
  <link id="Link1" rel="shortcut icon" href="~/images/favicon.ico" runat="server" />
  <!-- Bootstrap 3.3.6 -->
  <link rel="stylesheet" href="~/bootstrap/css/bootstrap.min.css" runat="server" />
  <!-- Font Awesome -->
  <link rel="stylesheet" href="~/plugins/font-awesome/css/font-awesome.min.css" runat="server" />
  <!-- Ionicons -->
  <link rel="stylesheet" href="~/plugins/ionicons/css/ionicons.min.css" runat="server" />
  <!-- Theme style -->
  <link rel="stylesheet" href="~/dist/css/AdminLTE.min.css" runat="server" />
  <!-- iCheck -->
  <link rel="stylesheet" href="~/plugins/iCheck/square/blue.css" runat="server" />

  <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
  <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
  <!--[if lt IE 9]>
  <script src="https://oss.maxcdn.com/html5shiv/3.7.3/html5shiv.min.js"></script>
  <script src="https://oss.maxcdn.com/respond/1.4.2/respond.min.js"></script>
  <![endif]-->
</head>
<body class="hold-transition login-page">
<asp:Label ID="lblInfo" runat="server" CssClass="error" Visible="False"></asp:Label>
<div class="login-box">
  <div class="login-logo">
    <a href="#" target="_blank"><i class="fa fa-fw fa-truck"></i><b> L M S</b></a>
  </div>
  <!-- /.login-logo -->
  <div class="login-box-body">
    <p class="login-box-msg">Masukkan username dan password!</p>

    <form runat="server">
      <div class="form-group has-feedback">
        <asp:TextBox ID="txtUserID" runat="server" CssClass="form-control" placeholder="Username" required="required"></asp:TextBox>
        <span class="glyphicon glyphicon-user form-control-feedback"></span>
      </div>
      <div class="form-group has-feedback">
        <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control" placeholder="Password" required="required"></asp:TextBox>
        <span class="glyphicon glyphicon-lock form-control-feedback"></span>
      </div>
      <div class="row">
        <!-- /.col -->
        <div class="col-xs-6 text-right">
          <dxe:ASPxButton ID="btnSubmit" runat="server" Text="Login" CssClass="btn btn-primary" EnableDefaultAppearance="False" UseSubmitBehavior="False">
		  </dxe:ASPxButton>
        </div>
        <div class="col-xs-6">
		  <dxe:ASPxButton ID="btnReset" runat="server" Text="Reset" CssClass="btn btn-danger" EnableDefaultAppearance="False" UseSubmitBehavior="False">
		  </dxe:ASPxButton>
        </div>
        <!-- /.col -->
      </div>
    </form>
  </div>
  <br /><p class="text-center">©2017 All Rights Reserved. <a href="#" target="_blank">Logistics Management System.</a></p>
  <!-- /.login-box-body -->
</div>
<!-- /.login-box -->

<!-- jQuery 2.2.3 -->
<script src="/plugins/jQuery/jquery-2.2.3.min.js"></script>
<!-- Bootstrap 3.3.6 -->
<script src="/bootstrap/js/bootstrap.min.js"></script>
<!-- iCheck -->
<script src="/plugins/iCheck/icheck.min.js"></script>
<script>
    $(function () {
        $('input').iCheck({
            checkboxClass: 'icheckbox_square-blue',
            radioClass: 'iradio_square-blue',
            increaseArea: '20%' // optional
        });
    });
</script>
</body>
</html>
