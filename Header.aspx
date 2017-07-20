<%@ Page Language="VB" Strict="true" AutoEventWireup="false" Inherits="LMS.header2" Codebehind="Header2.aspx.vb" %>

<%@ Register assembly="DevExpress.Web.ASPxEditors.v7.3, Version=7.3.7.0, Culture=neutral, PublicKeyToken=49d90c14d24271b5" namespace="DevExpress.Web.ASPxEditors" tagprefix="dxe" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>LIGITA JAYA - Page Header</title>

    </head>
<body style="padding:0px; margin:0px">
    <form id="form1" runat="server">
    <div style="padding:0; margin:0; background-image:url('images/body-bg.gif'); background-repeat:repeat;">
        <div style="background-color:#2c3848; height:30px; padding:10px 10px 0 10px;">
			<a href = "javascript:parent.frames(content).location.href='blank.aspx' "> </a>
			<asp:LinkButton ID = "btnhome" runat="server" style="float:left; padding-right:7px;"><img src="Images/home.png" border="0" title="Back To Home" /></asp:LinkButton>
			<asp:LinkButton ID="btnswitch" runat="server" style="float:left; padding-right:7px;"><img src="Images/switchuser.png" border="0" title="Switch User" /></asp:LinkButton>
			<asp:LinkButton ID="btlogout" runat="server" style="float:right; height: 18px;"><img src="Images/btLogOut.png" border="0" title="Log Out" /></asp:LinkButton>
			<div style="color:#3DF2F9; float:right; padding-right:5px;"><asp:Label ID="lUser" runat="server" Text="User" Font-Bold="true"></asp:Label>&nbsp;|| <asp:Label ID="lroles" runat="server" Text="roles"></asp:Label> &nbsp;||</div>
		</div>
		<div height: 78px;">
		    <div style="padding:5px 0 0 5px; float:left">
			    <img src="Images/internusabaharipersada.png" border="0" style="width:70px; height:70px;" />    
		    </div>
		    
		    <div style="float:left; padding:25px 0 0 5px;">
			    <img src="Images/LogoIBP.png" border="0" style="height: 31px; width: 198px" />
		    </div>
		    <div style="clear:both; height: 26px;"></div>
	    </div>
    </div>    
    </form>
</body>
</html>
