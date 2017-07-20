<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ConfigPenyusutan.aspx.vb" Inherits="LMS.ConfigPenyusutan" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v7.3, Version=7.3.7.0, Culture=neutral, PublicKeyToken=49d90c14d24271b5"
	Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dxwgv" %>
	
	<%@ Register Assembly="DevExpress.Web.v7.3, Version=7.3.7.0, Culture=neutral, PublicKeyToken=49d90c14d24271b5"
	Namespace="DevExpress.Web.ASPxPopupControl" TagPrefix="dxpc" %>

<%@ Register assembly="DevExpress.Web.ASPxEditors.v7.3, Version=7.3.7.0, Culture=neutral, PublicKeyToken=49d90c14d24271b5" namespace="DevExpress.Web.ASPxEditors" tagprefix="dxe" %>

<%@ Register assembly="System.Web, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="System.Web.UI" tagprefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
        <title>Config Penyusutan</title>
    <link rel="stylesheet" type="text/css" href="../css/style.css" />
    <link rel="stylesheet" type="text/css" href="../css/main.css"  />
    <style type="text/css">
        .style2
        {
            width: 180px;
        }
        .style3
        {
            width: 10px;
        }
        </style>
   <script type= "text/javascript" language="javascript" src="../script/main.js" ></script>
    <script type="text/javascript">
        var flag;
        function OnGetRowValues(values) {
            if (flag == 1) {
                BEKodeAkun.SetValue(values[1]);
                document.getElementById("TxtNamaAkun").value = values[2];
                document.getElementById("hfKode").value = values[3] +"."+ values[1];
                
            }

            if (flag == 2) {
                BEDebitAkun.SetValue(values[1]);
                document.getElementById("TxtDebitNamaAkun").value = values[2];
                document.getElementById("hfDebitKode").value = values[3] + "." + values[1];
            }

            if (flag == 3) {
                BEKreditAkun.SetValue(values[1]);
                document.getElementById("TxtKreditNamaAkun").value = values[2];
                document.getElementById("hfKreditKode").value = values[3] + "." + values[1];
            }
            
            Account_PopUp.Hide();
        }
       </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="formtitle">
        <b>Config Penyusutan</b>  
   </div><br />
    <div class ="div_input">
            <div class="div_umum">
                <table  cellpadding="0" cellspacing="0" class="Table_Olive">
                    <tr class="trHeader_Olive">
                        <td class ="style2">
                            Kode Akun
                        </td>
                        <td >
                            :
                        </td>
                        <td>
                            <dxe:ASPxButtonEdit ID="BEKodeAkun" ClientInstanceName="BEKodeAkun" runat="server" 
									 Width="189px">
										<Buttons>
										<dxe:EditButton>
										</dxe:EditButton>
									</Buttons>
                            <ClientSideEvents ButtonClick="function(s, e) {
										e.processOnServer = false; 
										Account_PopUp.ShowAtElement(s.GetMainElement());
										flag=1;
										}" />
                            </dxe:ASPxButtonEdit>
                        </td>
                        <td>
                            <asp:TextBox ID="TxtNamaAkun" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Akun Penyusutan Debit
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <dxe:ASPxButtonEdit ID="BEDebitAkun" ClientInstanceName="BEDebitAkun" runat="server" 
									 Width="189px">
										<Buttons>
										<dxe:EditButton>
										</dxe:EditButton>
									</Buttons>
                            <ClientSideEvents ButtonClick="function(s, e) {
										e.processOnServer = false; 
										Account_PopUp.ShowAtElement(s.GetMainElement());
										flag=2;
										}" />
                            </dxe:ASPxButtonEdit>
                        </td>
                         <td>
                            <asp:TextBox ID="TxtDebitNamaAkun" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Akun Penyusutan Kredit
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <dxe:ASPxButtonEdit ID="BEKreditAkun" ClientInstanceName="BEKreditAkun" runat="server" 
									 Width="189px">
										<Buttons>
										<dxe:EditButton>
										</dxe:EditButton>
									</Buttons>
                            <ClientSideEvents ButtonClick="function(s, e) {
										e.processOnServer = false; 
										Account_PopUp.ShowAtElement(s.GetMainElement());
										flag=3;
										}" />
                            </dxe:ASPxButtonEdit>
                        </td>
                        <td>
                          <asp:TextBox ID="TxtKreditNamaAkun" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                              <table width="100%" cellpadding="4" cellspacing="0">
			                        <tr>
				                        <td align ="right">
                                            <dxe:ASPxButton ID="btn_save" runat="server" Text="Save">
                                            <Image Url="../images/add.png" />
                                            </dxe:ASPxButton></td>
                                        <td align ="left">
                                            <dxe:ASPxButton ID="btn_reset" runat="server" Text="Reset">
                                            <Image Url="../images/undo.png" />
                                            </dxe:ASPxButton></td>
			                        </tr>
                                </table>
                        </td>
                        <td></td>
                        <td></td>
                         <td>
						<asp:Label ID="linfo" runat="server" ForeColor="Red"></asp:Label>
                        <asp:Label ID="linfoberhasil" runat="server" CssClass="berhasil" Visible="False"></asp:Label>
					    </td>
                    </tr>
                </table>
            </div>    
    </div>
    <div class="div_input">
    
    <dxwgv:ASPxGridView ID="Grid_Config" ClientInstanceName="grid_config" 
						runat="server" AutoGenerateColumns="False" KeyFieldName="ID" >
						   <SettingsPager AlwaysShowPager="True" PageSize="15"></SettingsPager>
						<SettingsBehavior AllowFocusedRow="True" />
						<Settings ShowFilterRow="True" />
						<Columns>
							<dxwgv:GridViewDataColumn FieldName="ID" VisibleIndex="0" Visible="False">
							</dxwgv:GridViewDataColumn>
							<dxwgv:GridViewDataColumn FieldName="AccCode" Caption="Kode Akun" VisibleIndex="1" Visible="False">
							</dxwgv:GridViewDataColumn>
							<dxwgv:GridViewDataColumn FieldName="Name" Caption="Nama Akun" VisibleIndex="2" Visible="True">
							</dxwgv:GridViewDataColumn>
							<dxwgv:GridViewDataColumn FieldName="DebitAccount" VisibleIndex="3" Visible="False">
							</dxwgv:GridViewDataColumn>
							<dxwgv:GridViewDataColumn FieldName="DebitAccountName" Caption="Akun Debit" VisibleIndex="4" >
							</dxwgv:GridViewDataColumn>
							<dxwgv:GridViewDataColumn FieldName="KreditAccount" VisibleIndex="5" Visible="False">
							</dxwgv:GridViewDataColumn>
							<dxwgv:GridViewDataColumn FieldName="KreditAccountName" Caption="Akun Kredit" VisibleIndex="6">
							</dxwgv:GridViewDataColumn>
						    <dxwgv:GridViewDataColumn Caption="#" Name="Edit" VisibleIndex="7" Width="1%">
                                <dataitemtemplate>
                                    <asp:LinkButton ID="tbEdit" runat="server" CommandName="Edit" 
                                        ToolTip="Edit Jurnal">Edit</asp:LinkButton>
                                </dataitemtemplate>
                            </dxwgv:GridViewDataColumn>
                            <dxwgv:GridViewDataColumn Caption="#" Name="Delete" VisibleIndex="8" Width="1%">
                                <dataitemtemplate>
                                    <asp:LinkButton ID="tbDelete" runat="server" CommandName="Delete" 
                                        ToolTip="Delete Jurnal">Delete</asp:LinkButton>
                                </dataitemtemplate>
                            </dxwgv:GridViewDataColumn>
						</Columns>
				</dxwgv:ASPxGridView>
    </div>
    
    <div>
    <asp:SqlDataSource ID="SqlDataSourceAccount" runat="server" ConnectionString="<%$ ConnectionStrings:LigitaConString %>"
			 SelectCommand="SELECT ID, [Types],SUBSTRING(Code,6,LEN(Code) -5 ) as Code, Name, Levels FROM ChartOfAccount WHERE Levels <> 1 and Parent <> 'TOP' and status = 1 ORDER BY Code">
		</asp:SqlDataSource>
		 
		<dxpc:ASPxPopupControl ID="Account_PopUp" ClientInstanceName="Account_PopUp" 
					runat="server" HeaderText="Account List" EnableClientSideAPI="True" 
					AllowResize="True">
				<Controls>
					<dxwgv:ASPxGridView ID="grid_Account" ClientInstanceName="grid_Account" 
						runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSourceAccount" 
						KeyFieldName="ID">
						<Columns>
							<dxwgv:GridViewDataColumn FieldName="ID" VisibleIndex="0" Visible="False">
							</dxwgv:GridViewDataColumn>
							<dxwgv:GridViewDataColumn FieldName="Types" VisibleIndex="1" Visible="False">
							</dxwgv:GridViewDataColumn>
							<dxwgv:GridViewDataColumn FieldName="Code" VisibleIndex="2" >
							</dxwgv:GridViewDataColumn>
							<dxwgv:GridViewDataColumn FieldName="Name" VisibleIndex="3">
							</dxwgv:GridViewDataColumn>
							<dxwgv:GridViewDataColumn FieldName="Levels" VisibleIndex="4">
							</dxwgv:GridViewDataColumn>
						</Columns>
						<ClientSideEvents RowDblClick="function(s, e) {
	grid_Account.GetRowValues(grid_Account.GetFocusedRowIndex(), 'ID;Code;Name;Types', OnGetRowValues)
}" />
						<SettingsBehavior AllowFocusedRow="True"  />
						<Settings ShowFilterRow="True" />
					</dxwgv:ASPxGridView>
				</Controls>
			</dxpc:ASPxPopupControl>
		
    </div>
    <asp:HiddenField ID="hfID" runat="server" />
    <asp:HiddenField ID="hfKreditKode" runat="server" />
    <asp:HiddenField ID="hfDebitKode" runat="server" />
    <asp:HiddenField ID="hfKode" runat="server" />
    <asp:HiddenField ID="hfMode" runat="server" />
    </form>
</body>
</html>
