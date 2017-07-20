<%@ Page Language="VB" AutoEventWireup="false" Inherits="LMS.MainMenu" Codebehind="MainMenu.aspx.vb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
  <head>
	    <title>PT. INTERNUSA BAHARI PERSADA</title>
    </head>

    <frameset rows="120,*" border="0">
	    <frame src="header.aspx" name="header" style="border-bottom:solid 1px #73779d;" scrolling="no" noresize="noresize" />
	    <frameset cols="200,100%" border="0">
		    <frame src="menu/menu.aspx" name="menu" style="border-right:solid 2px #FFFFFF;" scrolling="yes" noresize="noresize" />
		    <frame src="Blank.aspx" name="content" id="content" scrolling="yes" noresize="noresize" />
	    </frameset>
    </frameset>

</html>
