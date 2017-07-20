<%@ Page Language="VB" AutoEventWireup="false" Inherits="LMS.menu_Menu" Codebehind="Menu.aspx.vb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <title></title>
 
</head>
<body style="margin:0; padding:0; background-image:url('../images/body-bg.gif'); background-repeat:repeat;">
    <form id="form1" runat="server">
        <div style="width:100%; height:100%; padding-top:5px; padding-right:5px;">
	    <asp:TreeView ID="TreeView1" runat="server" ImageSet="Simple" NodeIndent="10" 
                ShowLines="True" >
		    <ParentNodeStyle Font-Bold="False" />
		    <%--<HoverNodeStyle Font-Underline="True" ForeColor="#DD5555" />--%>
		     <SelectedNodeStyle Font-Underline="True" ForeColor="#3333CC" 
            HorizontalPadding="0px" VerticalPadding="0px" BackColor="Transparent" />
		    <NodeStyle Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" 
			    HorizontalPadding="0px" NodeSpacing="0px" ChildNodesPadding="4px" Font-Bold="True" 
			    Font-Italic="False" Font-Overline="False" VerticalPadding="0px" />
	    </asp:TreeView>
            <br />
    </div>

    </form>
</body>
</html>
