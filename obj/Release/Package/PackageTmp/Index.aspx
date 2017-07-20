<%@ Page Language="VB" AutoEventWireup="false" Inherits="LMS.Index" Codebehind="Index.aspx.vb" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v7.3, Version=7.3.7.0, Culture=neutral, PublicKeyToken=49d90c14d24271b5"
	Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>LOGIN IBP</title>
    
     <script type="text/javascript">
      if (top.location != self.location) {
          top.location.replace(self.location);
      }

      function setFocus(oElement) {
          document.getElementById(oElement).focus();
      }
    </script>
    
    <link rel="stylesheet" type="text/css" href="css/style.css" />
    <link href="css/Roundcorner2.css" type="text/css" rel="Stylesheet" />

    
    <style type="text/css">
        .border
        {
	        -moz-border-radius-bottomleft : 10px;
	        -moz-border-radius-bottomright : 10px;
	        -moz-border-radius-topleft : 10px;
	        -moz-border-radius-topright : 10px;
	     
	        border : 1px solid;
	        border-color:Maroon;
	        padding: 1px;
         }
         .error {
             font-weight:bold;
             color:red;
             padding-left:0px;
             padding-right:5px;
             width:100%;
             font-size:13px;
        }
        .style5
        {
            width: 86px;
            height: 30px;
        }
        .style6
        {
            width: 183px;
            height: 30px;
        }
        .style7
        {
            height: 37px;
        }
        .style8
        {
            width: 12px;
        }
        .style9
        {
            height: 146px;
        }
        .warna
        {
            color : #C40211;    
        }
        #form1
        {
            height: 245px;
        }
    </style>
</head>
<body style="background-image:url('images/body-bg.gif'); background-repeat:repeat">
    <form id="form1" runat="server">
    <br/><br /><br /><br/><br /><br /><br />
    
    <div>
    <table align="center" style="vertical-align:middle; width: 545px;">
    <tr>
        <td align="center" style="font-size:30PX; font-family:Arial;">
            <strong style="color: #26318a">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; INTERNUSA BAHARI PERSADA</strong> 
        </td>
    </tr>
    </table>
    <br />
    <table style="background-color:Transparent; vertical-align:middle; width: 42%;" 
            align="center" cellpadding="0" cellspacing="0">
    
    <tr>
        <td class="style9" align="center">
        <table cellpadding = "0" cellspacing= "0" >
      
            <tr>
            <td>
                <img src="images/internusabaharipersada.png" style="height: 80px; width: 79px" />
            </td>
                <td class="style8" align ="center" valign ="middle">
                    &nbsp;</td><td>
                    <table style="border:solid 2px #FFFFFF"; cellpadding=0 cellspacing=0  class="border"> 
                    <tr >
                        <td class="style5" align =center style="border-bottom:solid 2px #FFFFFF; font-weight:bold"  > 
                            User ID
                        </td>
                        <td class="style6" style="border-bottom:solid 2px #FFFFFF" >
                            
                            <asp:TextBox ID="TxtUserID" runat="server"></asp:TextBox>
                            
                        </td>
                    </tr>
                    <tr>
                        <td class="style5" align =center style="font-weight:bold; border-bottom:solid 2px #FFFFFF;">
                            Password
                        </td>
                        <td class="style6" style="border-bottom:solid 2px #FFFFFF;">
                        
                            <asp:TextBox ID="TxtPassword" runat="server" TextMode="Password"></asp:TextBox>
                        
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align = "center"> 
                        
                            <asp:Label ID="lInfo" runat="server" CssClass="error" Visible="False"></asp:Label>
                        
                        </td>
                    </tr>
                    <tr>
                        <td class="style7" colspan = 2>
                        <table cellpadding="0" cellspacing="0">
										<tr>
											<td style="padding-left:50px;padding-right:5px;" align="center">
												<dxe:ASPxButton ID="btSubmit" runat="server" Text="Submit">
													<Image Url="images/ok.png" />
												</dxe:ASPxButton>			
											</td>
											<td style="padding-left:5px;" align="center">
												<dxe:ASPxButton ID="btCancel" runat="server" Text="Cancel">
													<Image Url="images/delete.png" />
												</dxe:ASPxButton>		
											</td>
										</tr>
									</table>
                        </td>  
                     </tr>
                 </table>
                </td>
              </tr>
           </table>
          </td> 
           </tr>  
    </table>
    </div>
    </form>
</body>
</html>
