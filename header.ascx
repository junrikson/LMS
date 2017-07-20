<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="header.ascx.vb" Inherits="LMS.header" %>

<!DOCTYPE html>
<html>
<head runat="server">
  <meta charset="utf-8">
  <meta http-equiv="X-UA-Compatible" content="IE=edge">
  <title id="lmsTittle" runat="server"></title>
  <!-- Tell the browser to be responsive to screen width -->
  <meta content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" name="viewport">
  <link rel="shortcut icon" href="~/images/favicon.ico" runat="server" />
  <!-- Bootstrap 3.3.6 -->
  <link rel="stylesheet" href="~/bootstrap/css/bootstrap.min.css" runat="server" />
  <!-- Font Awesome -->
  <link rel="stylesheet" href="~/plugins/font-awesome/css/font-awesome.min.css" runat="server" />
  <!-- Ionicons -->
  <link rel="stylesheet" href="~/plugins/ionicons/css/ionicons.min.css" runat="server" />
  <!-- Theme style -->
  <link rel="stylesheet" href="~/dist/css/AdminLTE.css" runat="server" />
  <!-- AdminLTE Skins. Choose a skin from the css/skins
       folder instead of downloading all of them to reduce the load. -->
  <link rel="stylesheet" href="~/dist/css/skins/_all-skins.css" runat="server" />
  <!-- iCheck -->
  <link rel="stylesheet" href="~/plugins/iCheck/flat/blue.css" runat="server" />
  <!-- Morris chart -->
  <link rel="stylesheet" href="~/plugins/morris/morris.css" runat="server" />
  <!-- jvectormap -->
  <link rel="stylesheet" href="~/plugins/jvectormap/jquery-jvectormap-1.2.2.css" runat="server" />
  <!-- Date Picker -->
  <link rel="stylesheet" href="~/plugins/datepicker/datepicker3.css" runat="server" />
  <!-- Daterange picker -->
  <link rel="stylesheet" href="~/plugins/daterangepicker/daterangepicker.css" runat="server" />
  <!-- bootstrap wysihtml5 - text editor -->
  <link rel="stylesheet" href="~/plugins/bootstrap-wysihtml5/bootstrap3-wysihtml5.min.css" runat="server" />

  <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
  <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
  <!--[if lt IE 9]>
  <script src="https://oss.maxcdn.com/html5shiv/3.7.3/html5shiv.min.js"></script>
  <script src="https://oss.maxcdn.com/respond/1.4.2/respond.min.js"></script>
  <![endif]-->
    
  <!-- Toggle Fullscreen -->
  <script type="text/javascript">
        window.onload = function () {
            var a = document.getElementById("toggleFullScreen");

            a.onclick = function () {
                if ((document.fullScreenElement && document.fullScreenElement !== null) ||
                 (!document.mozFullScreen && !document.webkitIsFullScreen)) {
                    if (document.documentElement.requestFullScreen) {
                        document.documentElement.requestFullScreen();
                    } else if (document.documentElement.mozRequestFullScreen) {
                        document.documentElement.mozRequestFullScreen();
                    } else if (document.documentElement.webkitRequestFullScreen) {
                        document.documentElement.webkitRequestFullScreen(Element.ALLOW_KEYBOARD_INPUT);
                    }
                } else {
                    if (document.cancelFullScreen) {
                        document.cancelFullScreen();
                    } else if (document.mozCancelFullScreen) {
                        document.mozCancelFullScreen();
                    } else if (document.webkitCancelFullScreen) {
                        document.webkitCancelFullScreen();
                    }
                }
                return false;
            }
        }
  </script>
</head>
<body class="hold-transition skin-blue sidebar-mini">
    <div class="wrapper">

        <header class="main-header">
            <!-- Logo -->
            <a href="~/index.aspx" class="logo" runat="server">
                <!-- mini logo for sidebar mini 50x50 pixels -->
                <span class="logo-mini"><b><asp:Literal ID="ltrCompanyID" runat="server"></asp:Literal></b></span>
                <!-- logo for regular state and mobile devices -->
                <span class="logo-lg"><b><asp:Literal ID="ltrCompanyName" runat="server"></asp:Literal></b></span>
            </a>
            <!-- Header Navbar: style can be found in header.less -->
            <nav class="navbar navbar-static-top">
                <!-- Sidebar toggle button-->
                <a href="#" class="sidebar-toggle" data-toggle="offcanvas" role="button">
                    <span class="sr-only">Toggle navigation</span>
                </a>
                <a href="#" class="sidebar-fullscreen" role="button" data-toggle="offcanvas" title="FullScreen" id="toggleFullScreen">
                    <span class="glyphicon glyphicon-fullscreen"></span>
                </a>

                <div class="navbar-custom-menu">
                    <ul class="nav navbar-nav">
                        <!-- Messages: style can be found in dropdown.less-->
                        <li class="dropdown messages-menu">
                            <a href="#" class="dropdown-toggle" data-toggle="dropdown">
                                <i class="fa fa-envelope-o"></i>
                            </a>
                            <ul class="dropdown-menu">
                                <li class="header">You have 0 message</li>
                                <li>
                                    <!-- inner menu: contains the actual data -->
                                    <ul class="menu">
                                        <li>
                                        </li>
                                    </ul>
                                </li>
                                <li class="footer"><a href="#">See All Messages</a></li>
                            </ul>
                        </li>
                        <!-- Notifications: style can be found in dropdown.less -->
                        <li class="dropdown notifications-menu">
                            <a href="#" class="dropdown-toggle" data-toggle="dropdown">
                                <i class="fa fa-bell-o"></i>
                            </a>
                            <ul class="dropdown-menu">
                                <li class="header">You have 0 notification</li>
                                <li>
                                    <!-- inner menu: contains the actual data -->
                                    <ul class="menu">
                                        <li>
                                        </li>
                                    </ul>
                                </li>
                                <li class="footer"><a href="#">View all</a></li>
                            </ul>
                        </li>
                        <!-- Tasks: style can be found in dropdown.less -->
                        <li class="dropdown tasks-menu">
                            <a href="#" class="dropdown-toggle" data-toggle="dropdown">
                                <i class="fa fa-flag-o"></i>
                            </a>
                            <ul class="dropdown-menu">
                                <li class="header">You have 0 task</li>
                                <li>
                                    <!-- inner menu: contains the actual data -->
                                    <ul class="menu">
                                        <li>
                                        </li>
                                    </ul>
                                </li>
                                <li class="footer">
                                    <a href="#">View all tasks</a>
                                </li>
                            </ul>
                        </li>
                        <!-- User Account: style can be found in dropdown.less -->
                        <li class="dropdown user user-menu">
                            <a href="#" class="dropdown-toggle" data-toggle="dropdown">
                                <img src="~/images/user.png" class="user-image" alt="User Image" runat="server">
                                <span class="hidden-xs">
                                    <asp:Literal ID="ltrName2" runat="server"></asp:Literal></span>
                            </a>
                            <ul class="dropdown-menu">
                                <!-- User image -->
                                <li class="user-header">
                                    <img src="~/images/user.png" class="img-circle" alt="User Image" runat="server">

                                    <p>
                                        <asp:Literal ID="ltrName" runat="server"></asp:Literal>
                                        <small>Member since Nov. 2012</small>
                                    </p>
                                </li>
                                <!-- Menu Body -->
                                <li class="user-body">
                                    <div class="row">
                                        <div class="col-xs-4 text-center">
                                            <a href="#">Followers</a>
                                        </div>
                                        <div class="col-xs-4 text-center">
                                            <a href="#">Sales</a>
                                        </div>
                                        <div class="col-xs-4 text-center">
                                            <a href="#">Friends</a>
                                        </div>
                                    </div>
                                    <!-- /.row -->
                                </li>
                                <!-- Menu Footer-->
                                <li class="user-footer">
                                    <div class="pull-left">
                                        <a href="#" class="btn btn-default btn-flat">Profile</a>
                                    </div>
                                    <div class="pull-right">
                                        <a href="~/logout.aspx" class="btn btn-default btn-flat" runat="server">Sign out</a>
                                    </div>
                                </li>
                            </ul>
                        </li>
                        <!-- Control Sidebar Toggle Button -->
                        <li>
                            <a href="#" data-toggle="control-sidebar"><i class="fa fa-gears"></i></a>
                        </li>
                    </ul>
                </div>
            </nav>
        </header>
        <!-- Left side column. contains the logo and sidebar -->
        <aside class="main-sidebar">
    <!-- sidebar: style can be found in sidebar.less -->
            <section class="sidebar">
                <!-- Sidebar user panel -->
                <div class="user-panel">
                    <p class="text-left" style="color: #FFF;">
                        <asp:Literal ID="ltrCompanyDetail" runat="server"></asp:Literal>
                    </p>
                </div>

                <!-- sidebar menu: : style can be found in sidebar.less -->
                <ul class="sidebar-menu">
                    <li class="header">Logistics Management System</li>
                    <asp:Literal ID="ltrSidebar" runat="server"></asp:Literal>
                </ul>
            </section>
            <!-- /.sidebar -->
        </aside>
    
