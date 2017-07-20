<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Asuransi.aspx.vb" Inherits="LMS.Asuransi" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v7.3, Version=7.3.7.0, Culture=neutral, PublicKeyToken=49d90c14d24271b5"
	Namespace="DevExpress.Web.ASPxGridView" tagprefix="dxwgv" %>
	
<%@ Register Assembly="DevExpress.Web.v7.3, Version=7.3.7.0, Culture=neutral, PublicKeyToken=49d90c14d24271b5"
	Namespace="DevExpress.Web.ASPxPopupControl" TagPrefix="dxpc" %>
	
<%@ Register assembly="DevExpress.Web.ASPxEditors.v7.3, Version=7.3.7.0, Culture=neutral, PublicKeyToken=49d90c14d24271b5" namespace="DevExpress.Web.ASPxEditors" tagprefix="dxe" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Lain2</title>
    
    <link rel="stylesheet" type="text/css" href="../css/style.css" />
    <link rel="stylesheet" type="text/css" href="../css/main.css"  />
    <script language="javascript" type="text/javascript" src="../script/main.js" ></script>
    <script language="javascript" src="../script/NumberFormat.js" type="text/javascript"></script>
    
    <script type="text/javascript" language="javascript">

        function getCustomer() {

            returnValue = ShowDialog2('MasterCustomer', 'Arg', '610', '450');
            if (returnValue) {

                var comp = new Array();
                comp = returnValue.split(";");
                var cust = document.getElementById("TxtNmLain");
                cust.value = comp[1];
                var cust3 = document.getElementById("HFNmCust");
                cust3.value = comp[1];
                var cust2 = document.getElementById("HFCodeCust");
                cust2.value = comp[0];
                var cust4 = document.getElementById("TxtCodeCustomer");
                cust4.value = comp[0];
            }
        }

        function changenumberformat(control) {
            var conval = document.getElementById(control).value;
            if (conval.lastIndexOf(",") != conval.length - 1) {
                document.getElementById(control).value = FormatNumberBy3(conval.replace(/[.]/g, ""), ",", ".");
            }
            else {
                document.getElementById(control).value = conval;
            }
        }

        function OnGetRowValues(values) {
            document.getElementById("hfAccountCode").value = values[0];
            tbAccountNoOthersJournal.SetValue(values[1]);
            document.getElementById("hfAccountName").value = values[2];
            document.getElementById("TxtKetCoa").value = values[2];
            document.getElementById("hfAccountType").value = values[3];

            Account_PopUp.Hide();
            document.getElementById("tbAmountOthersJournal").focus();
        }
        
     </script>
    
</head>
<body>
    <form id="form1" runat="server">
    <div class = "Divutama" >
        <div class="formtitle">Input Lain - Lain</div>
		<br />
		<div  class="div_input" >
		    <div class="div_umum">
            
 
                
			  
                        <asp:HiddenField ID = "HFID" runat="server" />
                        <asp:HiddenField ID = "HFMode" runat="server" />
				        <asp:HiddenField ID="HFNoAsuransi" runat="server" />
				        <asp:HiddenField ID="HFCodeCust" runat="server" />
				        <asp:HiddenField ID="HFNmCust" runat="server" />
				
				<table>
				<tr>
				<td>
                        Tanggal</td>
                    <td style = "width: 240px">
                        <dxe:ASPxDateEdit ID="DtInvoice" runat="server" EditFormat="Custom" 
									EditFormatString="dd MMMM yyyy" Cursor ="pointer" Height="21px" Width="160px">
                        </dxe:ASPxDateEdit>
                    </td>
                    </tr>
                    <tr>
                        <td>
                            Jenis
                        </td>
                        <td>
                            <asp:RadioButtonList ID="RbJenis" runat="server" RepeatDirection="Horizontal" 
                                Width="500px" AutoPostBack ="true">
                                <asp:ListItem Text="Asuransi" Value="Asuransi">Asuransi</asp:ListItem>
                                <asp:ListItem Text="KapalAgen" Value="KapalAgen">Kapal Agen</asp:ListItem>
                                <asp:ListItem Text="Lain2" Value="Lain2">Lain-Lain</asp:ListItem>
                                <asp:ListItem Text="SewaKran" Value="SewaKran">Sewa Kran</asp:ListItem>
                            </asp:RadioButtonList>
                            
                        </td>
                    </tr>
                    
               <tr>
                   <td colspan="2">
                    <asp:Panel runat="server" ID="PnlPilih">
                        <table>
                            <tr>
                                <td style="width:90px;">No</td>
                            <td> <asp:TextBox ID="TxtNoInvoice" runat="server" Width="200px" /></td>
                            <td>
                                <asp:CheckBox ID="ChkPilhTipeAsuransi" runat="server" Text="Non-Freight" />
                            </td>
                            </tr>
                        </table>
                     </asp:Panel>
                   </td>                  
               </tr>
               <tr>
                <td colspan = "2">
                    <asp:Panel runat="server" ID="PnlKapal">
                        <table>
                            <tr>
                                <td style="width:90px;">
                                    Kapal
                                </td>
                                <td>
                                    <asp:DropDownList ID="DDLKapal" runat="server" />   
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Indikator
                                </td>
                                <td>
                                    <asp:TextBox ID="TxtIndikator" runat="server" AutoPostBack = "False" 
                                    Width="20" MaxLength="1"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
               </tr>
               
               <tr>
                    <td>
                        Harga
                    </td>
                    <td>
                        <asp:TextBox ID="TxtHargaInvoice" runat="server" Width="200px"></asp:TextBox>
                    </td>
               </tr>
               <tr>
				            
				            
				            <td>
				                Nama Customer
				            </td>
				            <td>
				                <asp:TextBox ID="TxtNmLain" runat="server" Width="200px" ReadOnly="true"></asp:TextBox><img alt="Browse" src="../images/search.png" onclick="javascript:getCustomer();" />
				            </td>
				            
				        </tr>
				        <tr>
				            <td>
				                Code Customer
				            </td>
				            <td>
				                 <asp:TextBox ID="TxtCodeCustomer" runat="server" Width="200px" ReadOnly="false"></asp:TextBox>
				            </td>
				        </tr>
				        <tr>
				            <td>
				                Keterangan :
				            </td>
				            <td>
				                <asp:TextBox ID="TxtKeterangan" runat="server" 
                                    Width="200px" Height="100px" TextMode="MultiLine" ></asp:TextBox>
				            </td>
				        </tr>
                
                </table>   
				
				 
       
                        <br />

                <div align = "center" style="width:100%">
			        <asp:Label ID="lInfo" runat="server" CssClass="error" Visible="False"></asp:Label>
                    <asp:Label ID="linfoberhasil" runat="server" CssClass = "berhasil" Visible="False"></asp:Label>
			    </div>
                

                        
                        <table width= "100%">
                <tr>
                    <td align ="right">
                        <dxe:ASPxButton ID="btSimpan" runat="server" Text="Simpan" Width="90px">
							                <Image Url="../images/save-alt.png" />
							                
						                </dxe:ASPxButton>
                        <br />
                    </td>
                    <td align="left" valign="top" >
        	                        <dxe:ASPxButton ID="btBatal" runat="server" Text="Reset" Width="90px">
				                        <Image Url="../images/undo.png" />
			                        </dxe:ASPxButton>
		                        </td> 
                </tr>
            </table>
                      
						                
                        <br />
                <dxwgv:ASPxGridView ID="Grid_JurnalLain2" ClientInstanceName="Grid_JurnalLain2" 
						runat="server" AutoGenerateColumns="False" KeyFieldName="ID"  Width="80%">

						   <SettingsPager AlwaysShowPager="True" PageSize="15"></SettingsPager>
						   <styles>
                        <header backcolor="#2c3848" font-bold="true" forecolor="#FFFFFF" 
                            horizontalalign="Center" hoverstyle-border-bordercolor="#515763">
                            <hoverstyle>
                                <border bordercolor="#515763"></border>
                            </hoverstyle>
                        </header>
                        <FocusedRow BackColor="#D3D1D4" ForeColor="#000000"></FocusedRow>
                        
                        <Row BackColor="#ffffff"></Row>
                       
                    </styles>
						<SettingsBehavior AllowFocusedRow="True" />
						   
						<Settings ShowFilterRow="True" />
						<Columns>
							<dxwgv:GridViewDataColumn FieldName="ID" VisibleIndex="0" Visible="False">
							</dxwgv:GridViewDataColumn>
							<dxwgv:GridViewDataColumn FieldName="No" VisibleIndex="1" Visible="True">
							</dxwgv:GridViewDataColumn>
							<dxwgv:GridViewDataDateColumn FieldName="Tanggal" VisibleIndex="2" >
							    <PropertiesdateEdit DisplayFormatString="dd MMMM yyyy"></PropertiesdateEdit>
							</dxwgv:GridViewDataDateColumn>
							<dxwgv:GridViewDataColumn FieldName="Description" VisibleIndex="3">
							</dxwgv:GridViewDataColumn>
							<dxwgv:GridViewDataColumn FieldName="NamaCustomer" VisibleIndex="3">
							</dxwgv:GridViewDataColumn>
							<dxwgv:GridViewDataColumn FieldName="CodeCustomer" VisibleIndex="3" Visible ="false">
							</dxwgv:GridViewDataColumn>
							<dxwgv:GridViewDatatextColumn FieldName="Harga" VisibleIndex="3">
							    <PropertiesTextEdit DisplayFormatString="{0:###,###,###.00}"></PropertiesTextEdit>
							</dxwgv:GridViewDatatextColumn>
							<dxwgv:GridViewDataColumn FieldName="Type" VisibleIndex="3" Visible = "false">
							</dxwgv:GridViewDataColumn>
							<dxwgv:GridViewDataColumn FieldName="Jenis" VisibleIndex="3" Visible="true">
							</dxwgv:GridViewDataColumn>
							<dxwgv:GridViewDataColumn FieldName="KapalID" VisibleIndex="3" Visible="false">
							</dxwgv:GridViewDataColumn>
							<dxwgv:GridViewDataColumn FieldName="Ditujukan" VisibleIndex="3" Visible="false">
							</dxwgv:GridViewDataColumn>
							<dxwgv:GridViewDataTextColumn Caption="Yang Input" Name="YgInput" VisibleIndex="3" 
							    FieldName="YgInput">
						    </dxwgv:GridViewDataTextColumn>
						    <dxwgv:GridViewDataColumn Caption="#" Name="Edit" VisibleIndex="4" Width="1%">
                                <dataitemtemplate>
                                    <asp:LinkButton ID="tbEdit" runat="server" CommandName="Edit" 
                                        ToolTip="Edit Jurnal">Edit</asp:LinkButton>
                                </dataitemtemplate>
                            </dxwgv:GridViewDataColumn>
                            <dxwgv:GridViewDataColumn Caption="#" Name="Delete" VisibleIndex="5" Width="1%">
                                <dataitemtemplate>
                                    <asp:LinkButton ID="tbDelete" runat="server" CommandName="Delete" 
                                        ToolTip="Delete Jurnal">Delete</asp:LinkButton>
                                </dataitemtemplate>
                            </dxwgv:GridViewDataColumn>
						</Columns>
					</dxwgv:ASPxGridView>
                    
            
            </div>
        </div>
        </div>
        
        <asp:SqlDataSource ID="SqlDataSourceAccount" runat="server" ConnectionString="<%$ ConnectionStrings:LigitaConString %>"
			 SelectCommand="SELECT ID, [Types],[Types] + '.' + SUBSTRING(Code,6,LEN(Code) -5 ) as Code, Name, Levels, Lokasi FROM ChartOfAccount WHERE status = 1 and Levels <> 1 and Parent <> 'TOP' ORDER BY Code">
		</asp:SqlDataSource>
		 
		<dxpc:ASPxPopupControl ID="Account_PopUp" ClientInstanceName="Account_PopUp" 
					runat="server" HeaderText="Account List" EnableClientSideAPI="True" 
					AllowResize="True" Width= "430px">
				<Controls>
					<dxwgv:ASPxGridView ID="grid_Account" ClientInstanceName="grid_Account" 
						runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSourceAccount" 
						KeyFieldName="ID" Width="100%">
						<SettingsPager PageSize="10">
                                </SettingsPager>
								<Settings ShowFilterRow="True" />
								<%--<SettingsBehavior AllowFocusedRow="True"  ColumnResizeMode="Control" />--%>
						<Columns>
							<dxwgv:GridViewDataColumn FieldName="ID" VisibleIndex="0" Visible="False">
							</dxwgv:GridViewDataColumn>
							<dxwgv:GridViewDataColumn FieldName="Types" VisibleIndex="1" Visible="TRUE"  Width="30px">
							</dxwgv:GridViewDataColumn>
							<dxwgv:GridViewDataColumn FieldName="Code" VisibleIndex="2" Width="70px" >
							</dxwgv:GridViewDataColumn>
							<dxwgv:GridViewDataColumn FieldName="Name" VisibleIndex="3" Width = "200px">
							</dxwgv:GridViewDataColumn>
							<dxwgv:GridViewDataColumn FieldName="Levels" VisibleIndex="4" Width="30px">
							</dxwgv:GridViewDataColumn>
							<dxwgv:GridViewDataColumn FieldName="Lokasi" VisibleIndex="5" Width="30px">
							</dxwgv:GridViewDataColumn>
						</Columns>
						<ClientSideEvents RowDblClick="function(s, e) {
	grid_Account.GetRowValues(grid_Account.GetFocusedRowIndex(), 'ID;Code;Name;Types', OnGetRowValues)
}" />
						<SettingsBehavior AllowFocusedRow="True" />
						<Settings ShowFilterRow="True" />
					</dxwgv:ASPxGridView>
				</Controls>
			</dxpc:ASPxPopupControl>
    </form>
</body>
</html>
