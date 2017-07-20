<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="LinkedAccount.aspx.vb" Inherits="LMS.LinkedAccount" %>

<%@ Register Assembly="DevExpress.Web.v7.3, Version=7.3.7.0, Culture=neutral, PublicKeyToken=49d90c14d24271b5"
    Namespace="DevExpress.Web.ASPxPopupControl" TagPrefix="dxpc" %>

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
    <title>Linked Account</title>
    
    <link rel="stylesheet" type="text/css" href="../css/style.css" />
    <link rel="stylesheet" type="text/css" href="../css/main.css"  />
    
    <script type="text/javascript">
		
    function OnGetRowValuesDebet(values) {
        document.getElementById("hfAccountDebet").value = values[1];
        document.getElementById("TxtDesDebet").value = values[2];
        document.getElementById("hfDescDebet").value = values[2];
        TxtDebet.SetValue(values[1]);
        PopUpDebet.Hide();
    }
    function OnGetRowValuesKredit(values) {
        document.getElementById("hfAccountKredit").value = values[1];
        document.getElementById("TxtDesKredit").value = values[2];
        document.getElementById("hfDescKredit").value = values[2];
        TxtKredit.SetValue(values[1]);
        PopUpKredit.Hide();
    }
	</script>
    
</head>
<body>
    <form id="form1" runat="server">
    <div class = "Divutama" >
        <div class="formtitle">Linked Account</div>
        <br />
        <dxpc:ASPxPopupControl ID="PopUpDebet" runat="server" ClientInstanceName="PopUpDebet" HeaderText="Account List" >
		    <Controls>
		        <dxwgv:ASPxGridView ID="GridDebet" runat="server" DataSourceID="SqlDataSourceAccount" 
                    AutoGenerateColumns = "False" ClientInstanceName = "GridDebet"
                 KeyFieldName = "ID" Font-Size = "9pt" Width = "300px">
                <SettingsBehavior AllowFocusedRow="True"  />
                    <Columns>
                        <dxwgv:GridViewDataTextColumn Caption="ID" FieldName="ID" Name="ID" 
                            VisibleIndex="0" Visible = "false">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Code" FieldName="Code" Name="Code" 
                            VisibleIndex="1" Width="140px">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Description" FieldName="Description" 
                            Name="Description" VisibleIndex="2" Width="140px">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Types" FieldName="Types" 
                            Name="Types" VisibleIndex="2" Width="140px">
                        </dxwgv:GridViewDataTextColumn>
                    </Columns>
                    <ClientSideEvents RowDblClick="function(s, e) {
               	    GridDebet.GetRowValues(GridDebet.GetFocusedRowIndex(), 'ID;Code;Description', OnGetRowValuesDebet)}" />
                    <SettingsPager AlwaysShowPager="True" PageSize="5">
                    </SettingsPager>
                    <Settings ShowFilterRow="True" />
                    <Styles>
                        <Header BackColor="#2C3848" Font-Bold="True" ForeColor="White" 
                            HorizontalAlign="Center">
                            <HoverStyle>
                                <Border BorderColor="#515763" />
                            </HoverStyle>
                        </Header>
                        <FocusedRow BackColor="#D3D1D4" ForeColor="Black">
                        </FocusedRow>
                    </Styles>

            </dxwgv:ASPxGridView>
		    </Controls>
        </dxpc:ASPxPopupControl>
        <dxpc:ASPxPopupControl ID="PopUpKredit" runat="server" ClientInstanceName="PopUpKredit" HeaderText="Account List" >
		    <Controls>
		        <dxwgv:ASPxGridView ID="GridKredit" runat="server" DataSourceID="SqlDataSourceAccount" 
                    AutoGenerateColumns = "False" ClientInstanceName = "GridKredit"
                 KeyFieldName = "ID" Font-Size = "9pt" Width = "300px">
                <SettingsBehavior AllowFocusedRow="True"  />
                    <Columns>
                        <dxwgv:GridViewDataTextColumn Caption="ID" FieldName="ID" Name="ID" 
                            VisibleIndex="0" Visible = "false">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Code" FieldName="Code" Name="Code" 
                            VisibleIndex="1" Width="140px">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Description" FieldName="Description" 
                            Name="Description" VisibleIndex="2" Width="140px">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Types" FieldName="Types" 
                            Name="Types" VisibleIndex="2" Width="140px">
                        </dxwgv:GridViewDataTextColumn>
                    </Columns>
                    <ClientSideEvents RowDblClick="function(s, e) {
               	    GridKredit.GetRowValues(GridKredit.GetFocusedRowIndex(), 'ID;Code;Description', OnGetRowValuesKredit)}" />
                    <SettingsPager AlwaysShowPager="True" PageSize="5">
                    </SettingsPager>
                    <Settings ShowFilterRow="True" />
                    <Styles>
                        <Header BackColor="#2C3848" Font-Bold="True" ForeColor="White" 
                            HorizontalAlign="Center">
                            <HoverStyle>
                                <Border BorderColor="#515763" />
                            </HoverStyle>
                        </Header>
                        <FocusedRow BackColor="#D3D1D4" ForeColor="Black">
                        </FocusedRow>
                    </Styles>

            </dxwgv:ASPxGridView>
		    </Controls>
        </dxpc:ASPxPopupControl>
        <div  class="div_input" >
             <div class="div_umum">
                <table>
                    <tr>
                        <td>
                            Mode
                        </td>
                        <td>
                            <asp:DropDownList ID="DDLMode" runat="server">
                                
                                <asp:ListItem Text="Sales" Value="Sales"></asp:ListItem>
                                
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Jenis
                        </td>
                        <td>
                            <asp:DropDownList ID="DDLJenis" runat="server">

                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Debet</td>
                        <td>
                            <dxe:ASPxButtonEdit ID="TxtDebet" runat="server" ReadOnly="True">
                                <Buttons>
                                    <dxe:EditButton>
                                    </dxe:EditButton>
                                </Buttons>
                                <ClientSideEvents ButtonClick="function(s, e) {
									e.processOnServer = false; 
									PopUpDebet.ShowAtElement(s.GetMainElement());
									}" />
                            </dxe:ASPxButtonEdit>
                            <asp:TextBox ID="TxtDesDebet" runat="server" Width="300px" ReadOnly="true" CssClass="TextBoxAsLabel" ></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Kredit</td>
                        <td>
                            <dxe:ASPxButtonEdit ID="TxtKredit" runat="server" ReadOnly="True">
                                <Buttons>
                                    <dxe:EditButton>
                                    </dxe:EditButton>
                                </Buttons>
                                <ClientSideEvents ButtonClick="function(s, e) {
									e.processOnServer = false; 
									PopUpKredit.ShowAtElement(s.GetMainElement());
									}" />
                            </dxe:ASPxButtonEdit>
                            <asp:TextBox ID="TxtDesKredit" runat="server" ReadOnly="true" CssClass="TextBoxAsLabel" Width = "300px" ></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Description
                        </td>
                        <td>
                            <asp:TextBox ID="TxtDescription" runat="server" Width = "300px" ></asp:TextBox>
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
                 <dxwgv:ASPxGridView ID="Grid_LinkedAccount" runat="server" ClientInstanceName = "Grid_LinkedAccount" KeyFieldName = "ID">
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
                        <dxwgv:GridViewDataTextColumn Caption="COADetailID" FieldName="COADetailID" Name="COADetailID" 
                          Visible="false" VisibleIndex="1">
                        </dxwgv:GridViewDataTextColumn>
                        
                        <dxwgv:GridViewDataTextColumn Caption="Mode" FieldName="Mode" 
                            Name="Mode"  Visible="True" VisibleIndex="2" Width="20%">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn Caption="Jenis" FieldName="Jenis" 
                            Name="Jenis"  Visible="True" VisibleIndex="2" Width="20%">
                        </dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="Description" Name="Description" 
                          VisibleIndex="3" Caption="Deskripsi" >
						</dxwgv:GridViewDataTextColumn>
                        <dxwgv:GridViewDataTextColumn FieldName="Debet" Name="Debet" 
                                        VisibleIndex="4" Caption="Debet" >
                        </dxwgv:GridViewDataTextColumn>
                        
                        <dxwgv:GridViewDataTextColumn FieldName="Kredit" Name="Kredit" 
                                        VisibleIndex="5" Caption="Kredit" >
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
             <asp:SqlDataSource ID="SqlDataSourceAccount" runat="server" ConnectionString="<%$ ConnectionStrings:LigitaConString %>"
                SelectCommand="SELECT ID,[Types],Code, Name as Description FROM ChartOfAccount WHERE Parent <> 'TOP' and status = 1 AND levels <> 1 ORDER BY Code">
             </asp:SqlDataSource>
             		<asp:HiddenField ID="hfAccountDebet" runat="server" />
					<asp:HiddenField ID="hfAccountKredit" runat="server" />
					<asp:HiddenField ID="hfDescDebet" runat="server" />
					<asp:HiddenField ID="hfDescKredit" runat="server" />
					<asp:HiddenField ID="hfID" runat="server" />
					<asp:HiddenField ID="hfMode" runat="server" />
		</div>
    </div>
    </form>
</body>
</html>
