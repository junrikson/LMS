<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="sidebar.ascx.vb" Inherits="LMS.sidebar" %>

<!DOCTYPE html>
<html lang="en">
  <head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
    <!-- Meta, title, CSS, favicons, etc. -->
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">

    <title>Logistics Management System</title>

    <!-- Bootstrap -->
    <link id="Link1" href="~/vendors/bootstrap/dist/css/bootstrap.min.css" rel="stylesheet" runat="server" />
    <!-- Font Awesome -->
    <link id="Link2" href="~/vendors/font-awesome/css/font-awesome.min.css" rel="stylesheet" runat="server" />
    <!-- NProgress -->
    <link id="Link3" href="~/vendors/nprogress/nprogress.css" rel="stylesheet" runat="server" />
    <!-- iCheck -->
    <link id="Link4" href="~/vendors/iCheck/skins/flat/green.css" rel="stylesheet" runat="server" />
	
    <!-- bootstrap-progressbar -->
    <link id="Link5" href="~/vendors/bootstrap-progressbar/css/bootstrap-progressbar-3.3.4.min.css" rel="stylesheet" runat="server" />
    <!-- JQVMap -->
    <link id="Link6" href="~/vendors/jqvmap/dist/jqvmap.min.css" rel="stylesheet" runat="server" />
    <!-- bootstrap-daterangepicker -->
    <link id="Link7" href="~/vendors/bootstrap-daterangepicker/daterangepicker.css" rel="stylesheet" runat="server" />

    <!-- Custom Theme Style -->
    <link id="Link8" href="~/build/css/custom.min.css" rel="stylesheet" runat="server" />

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
            
  <body class="nav-md">
    <div class="container body">
      <div class="main_container">
        <div class="col-md-3 left_col">
          <div class="left_col scroll-view">
            <div class="navbar nav_title" style="border: 0;">
              <a href="/index.aspx" class="site_title"><i class="fa fa-truck"></i> <span>&nbsp;L M S</span></a>
            </div>

            <div class="clearfix"></div>

            <!-- sidebar menu -->
            <div id="sidebar-menu" class="main_menu_side hidden-print main_menu">
              <div class="menu_section">
                <ul class="nav side-menu">
                    <asp:Literal ID="ltrSidebar" runat="server"></asp:Literal>
                </ul>
              </div>
            </div>
            <!-- /sidebar menu -->
            
            <!-- /menu footer buttons -->
            <div class="sidebar-footer hidden-small">
              <a data-toggle="tooltip" data-placement="top" title="Settings">
                <span class="glyphicon glyphicon-cog" aria-hidden="true"></span>
              </a>
              <a data-toggle="tooltip" data-placement="top" title="FullScreen" id="toggleFullScreen" href="#">
                <span class="glyphicon glyphicon-fullscreen" aria-hidden="true"></span>
              </a>
              <a data-toggle="tooltip" data-placement="top" title="Lock">
                <span class="glyphicon glyphicon-eye-close" aria-hidden="true"></span>
              </a>
              <a data-toggle="tooltip" data-placement="top" title="Logout" href="/logout.aspx">
                <span class="glyphicon glyphicon-off" aria-hidden="true"></span>
              </a>
            </div>
            <!-- /menu footer buttons -->
          </div>
        </div>
        </div>