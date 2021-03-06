﻿<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="MasterJenis.aspx.vb" Inherits="LMS.MasterJenis" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v7.3, Version=7.3.7.0, Culture=neutral, PublicKeyToken=49d90c14d24271b5"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dxwgv" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v7.3, Version=7.3.7.0, Culture=neutral, PublicKeyToken=49d90c14d24271b5"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
    
<%@ Register Assembly="DevExpress.Web.v7.3, Version=7.3.7.0, Culture=neutral, PublicKeyToken=49d90c14d24271b5"
    Namespace="DevExpress.Web.ASPxMenu" TagPrefix="dxm" %>

<%@ Register assembly="System.Web, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="System.Web.UI" tagprefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Jenis in Linked Account</title>
    
    <link rel="stylesheet" type="text/css" href="../css/style.css" />
    <link rel="stylesheet" type="text/css" href="../css/main.css"  />
    
    
</head>
<body>
    <form id="form1" runat="server">
    <div class = "Divutama" >
        <div class="formtitle">Jenis In Linked Account</div>
        <br />
        
        <div  class="div_input" >
             <div class="div_umum">
                <table>
  
                    <tr>
                        <td>
                            Jenis
                        </td>
                        <td>
                            <asp:TextBox ID="TxtJenis" runat="server" Width="373px"></asp:TextBox>
                        </td>
                    </tr>
                    
                    
                    <tr>
                        <td colspan = "2">
                            <asp:Label ID="linfo" runat="server" Text="" CssClass = "error"></asp:Label>
                        <asp:Label ID="linfoberhasil" runat="server" Text="" CssClass = "berhasil"></asp:Label>
                        </td>   
                    </tr>
                    
                    <tr>
                        <td>
                              <table>
                                    <tr>
                                        <td>
			                                <dxe:ASPxButton ID="btSimpan" runat="server" Text="Simpan" Width="90px" 
                                                style="height: 25px">
				                                <Image Url="../images/save-alt.png" />
        				                        
			                                </dxe:ASPxButton>
		                                </td>
		                                <td>
        	                                <dxe:ASPxButton ID="btBatal" runat="server" Text="Reset" Width="90px">
				                                <Image Url="../images/undo.png" />
			                                </dxe:ASPxButton>
		                                </td>
                                    </tr>
                                </table> 
                        </td>
                    </tr>
                </table>
                <br />
                 <dxwgv:ASPxGridView ID="Grid_Jenis" runat="server" ClientInstanceName = "Grid_LinkedAccount" KeyFieldName = "ID">
                     <SettingsBehavior AllowFocusedRow="True" />
                     <SettingsPager PageSize="15">
                    </SettingsPager>
			        <settings showfilterrow="True" />
			        
                     <styles>
                        <header backcolor="#2c3848" font-bold="true" forecolor="#FFFFFF" 
                            horizontalalign="Center" hoverstyle-border-bordercolor="#515763">
                            <hoverstyle>
                                <border bordercolor="#515763"></border>
                            </hoverstyle>
                        </header>
                        
                        <FocusedRow BackColor="#D3D1D4" ForeColor="#000000"></FocusedRow>
                        
                    </styles>
                    <Columns>
                        <dxwgv:GridViewDataTextColumn Caption="ID" FieldName="ID" Name="ID" 
                            Visible="false" VisibleIndex="0">
                        </dxwgv:GridViewDataTextColumn>

                        <dxwgv:GridViewDataTextColumn Caption="Jenis" FieldName="Jenis" 
                            Name="Jenis"  Visible="True" VisibleIndex="2" Width="20%">
                        </dxwgv:GridViewDataTextColumn>

                        <dxwgv:GridViewDataTextColumn Name="Edit" Caption="#" VisibleIndex="14" Width="1%">
								    <DataItemTemplate>
								    <asp:LinkButton ID="tbedit" ToolTip="Edit Item" CommandName="Edit" runat="server">Edit</asp:LinkButton>
								    </DataItemTemplate>
						</dxwgv:GridViewDataTextColumn>
						<dxwgv:GridViewDataTextColumn Name="Delete" Caption="#" VisibleIndex="14" Width="1%">
								    <DataItemTemplate>
								    <asp:LinkButton ID="tbdelete" ToolTip="Delete Item" CommandName="Delete" runat="server">Delete</asp:LinkButton>
								    </DataItemTemplate>
						</dxwgv:GridViewDataTextColumn>
                    </Columns>
                    
                 </dxwgv:ASPxGridView>
             </div>
					<asp:HiddenField ID="hfID" runat="server" />
					<asp:HiddenField ID="hfMode" runat="server" />
		</div>
    </div>
    </form>
</body>
</html>

