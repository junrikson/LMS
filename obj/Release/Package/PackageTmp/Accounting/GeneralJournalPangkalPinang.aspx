<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="GeneralJournalPangkalPinang.aspx.vb" Inherits="LMS.GeneralJournalPangkalPinang" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v7.3, Version=7.3.7.0, Culture=neutral, PublicKeyToken=49d90c14d24271b5"
	Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dxwgv" %>
	
	<%@ Register Assembly="DevExpress.Web.v7.3, Version=7.3.7.0, Culture=neutral, PublicKeyToken=49d90c14d24271b5"
	Namespace="DevExpress.Web.ASPxPopupControl" TagPrefix="dxpc" %>

<%@ Register assembly="DevExpress.Web.ASPxEditors.v7.3, Version=7.3.7.0, Culture=neutral, PublicKeyToken=49d90c14d24271b5" namespace="DevExpress.Web.ASPxEditors" tagprefix="dxe" %>

<%@ Register assembly="System.Web, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="System.Web.UI" tagprefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Jurnal Umum Pangkal Pinang</title>
    <link rel="stylesheet" type="text/css" href="../css/style.css" />
    <link rel="stylesheet" type="text/css" href="../css/main.css"  />
    <style type="text/css">
        .style2
        {
            width: 260px;
        }
        .style3
        {
            width: 10px;
        }
        </style>
   <script type= "text/javascript" language="javascript" src="../script/main.js" ></script>
   <script language="javascript" src="../script/NumberFormat.js" type="text/javascript"></script>
    <script type="text/javascript">

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
            document.getElementById("hfAccountType").value = values[3];

            Account_PopUp.Hide();
            document.getElementById("tbAmountOthersJournal").focus();
        }

        function EditJournal(idx) {

            var arr = document.getElementById("hfArrayOthersJournal").value.split("~");
            var arr_root = arr[idx];
            var arr_child = arr_root.split("`");
            document.getElementById("hfAccountCode").value = arr_child[0];
            document.getElementById("hfAccountType").value = arr_child[1];
            document.getElementById("DDLDorCOthersJournal").value = arr_child[2];
            tbAccountNoOthersJournal.SetValue(arr_child[3]);
            document.getElementById("hfAccountName").value = arr_child[4];
            document.getElementById("tbDescriptionOthersJournal").value = arr_child[5];
            document.getElementById("tbAmountOthersJournal").value = arr_child[6];
            document.getElementById("hfIndexOthersJournal").value = idx;
            document.getElementById("hfModeOthersJournal").value = 'Update';
            document.getElementById("btResetOthersJournal").value = 'Delete';
            changenumberformat("tbAmountOthersJournal");
        }
        
        
    </script>
</head>
<body>
    <form id="form1" runat="server">
   <div class="formtitle">
        <b>Jurnal Umum Pangkal Pinang</b>  
   </div><br />
        <div class="div_input">
            <div class="div_umum">
            	<table width="99%" cellpadding="0" cellspacing="0" class="Table_Olive">
			<tr class="trHeader_Olive">
				<td>
        <table style ="border:#465946 1px solid;font-size:9pt;"  width="100%"  >
            <tr class = "trHeader_Olive ">
                <td >
                  Tanggal :  
                  </td>
                <td >
                   	<dxe:aspxdateedit id="tb_tgl" runat="server" Width="194px" cssfilepath="~/App_Themes/Office2003 Olive/{0}/styles.css"
						csspostfix="Office2003_Olive" imagefolder="~/App_Themes/Office2003 Olive/{0}/" EditFormat="Custom" 
									EditFormatString="dd MMMM yyyy">

						<ButtonStyle Width="13px" Cursor="pointer"></ButtonStyle>
					</dxe:aspxdateedit>
				</td>			
                <td >
                    GJNo :
                </td>
                <td >
                    <asp:TextBox ID="tbGJNumber" runat="server" Width="129px"></asp:TextBox>
                </td>
                <td >
                    <asp:TextBox ID ="TxtDesc" runat="server" Width="406px" Visible="False"></asp:TextBox>
                </td>
                	<td>
						<asp:Label ID="linfo" runat="server" ForeColor="Red"></asp:Label>
                        <asp:Label ID="linfoberhasil" runat="server" CssClass="berhasil" Visible="False"></asp:Label>
					</td>
            </tr>
        </table>
        		</td>
			</tr>
			
			<tr>
			    <td align="left">
          <table width="100%" cellpadding="4" cellspacing="0">
            <tr>
				<td style="padding:0;">
					<asp:Label ID="lOthersJournal" runat="server"></asp:Label>
				</td>
            </tr>
            <tr>
                <td style="border-top:#465946 1px solid;">
					<table>
						<tr>
							<td>
								<div style="font-size:8pt;"><strong>#Acc No</strong></div>
								<dxe:ASPxButtonEdit ID="tbAccountNoOthersJournal" 
									ClientInstanceName="tbAccountNoOthersJournal" runat="server" 
									CssFilePath="~/App_Themes/Office2003 Olive/Olive.css" CssPostfix="Office2003_Olive" 
									ImageFolder="~/App_Themes/Office2003 Olive/{0}/">
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
								<asp:HiddenField ID="hfAccountName" runat="server" />
								<asp:HiddenField ID="hfAccountType" runat="server" />
								<asp:HiddenField ID="hfAccountCode" runat="server" />
                            </td>
                            <td>
                                <div style="font-size:8pt;"><strong>D/C</strong></div>
								<asp:DropDownList ID="DDLDorCOthersJournal" runat="server" >
									<asp:ListItem Value="Debit">Debit</asp:ListItem>
									<asp:ListItem Value="Credit">Credit</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                             <td>
                                <div style="font-size:8pt;"><strong>Description</strong></div>
									<asp:TextBox ID="tbDescriptionOthersJournal" runat="server"  Width="250"></asp:TextBox>
                             </td>
										
                             <td>
                                <div style="font-size:8pt;"><strong>Amount</strong></div>
									<asp:TextBox ID="tbAmountOthersJournal" runat="server"  style="text-align:right;"></asp:TextBox>
                            </td>
                            <td>
								<div style="font-size:8pt;"><strong>&nbsp;</strong></div>
									<asp:Button ID="btSaveOthersJournal" runat="server" Text="Save" />
									<asp:Button ID="btResetOthersJournal" runat="server" Text="Reset" />
									<asp:HiddenField ID="hfModeOthersJournal" runat="server" />
									<asp:HiddenField ID="hfIndexOthersJournal" runat="server" />
									<asp:HiddenField ID="hfArrayOthersJournal" runat="server" />
                            </td>
						</tr>
						<tr>
						    <td>
						        <asp:CheckBox ID="chkPenyusutan" runat ="server" Text="Penyusutan" 
                                    AutoPostBack="True" />
						    </td>
						    <td>
						      
						    </td>
	                        <td>
						      <asp:Panel ID="PnlPenyusutan" runat ="server" >
						        <asp:TextBox ID="TxtPenyusutan" runat="server"></asp:TextBox>%/tahun
						        </asp:Panel>
						    </td>
						    <td>
						    
						    </td>
						    <td>
						    
						    </td>
						</tr>
					</table>
				</td>
            </tr>
        </table>
</td>
			</tr>
			<tr>
			    <td>
			        <table>
			            <tr>
			                <td style="width:130px;">
			                    Tipe Jurnal
			                </td>
			                <td>
                                <asp:RadioButtonList ID="RBLJournalType" runat="server" RepeatDirection="Horizontal">
                                   <asp:ListItem Text="Pengeluaran kas/bank" Value ="Pembayaran"></asp:ListItem>
			                        <asp:ListItem Text="Memo" Value ="Memo"></asp:ListItem>
			                        <asp:ListItem Text="Lain Lain" Value ="Lain Lain"></asp:ListItem>
                                </asp:RadioButtonList>
			                </td>
			              
			            </tr>
			        </table>
			    </td>
			</tr>
			<tr>
				<td align="left" style="border-top:#465946 1px solid; border-bottom:#465946 1px solid;">
					<table width="100%" cellpadding="4" cellspacing="0">
						<tr bgcolor="#EDF2D4">
							<td>
								<table width="100%">
									<tr>
										<td align="right">
											<asp:Button ID="btRecord" runat="server" Text="Record" />
											<asp:Button ID="btDelete" runat="server" Text="Delete" Visible="false" OnClientClick="return confirm('Are you sure want to delete this journal? ');" />
											<%--<input type="button" value="Exit" onclick="javascript:window.location='paymentregister.aspx';" class="ButtonDefault" />--%>
											<%--  <asp:RequiredFieldValidator 
                                ID="ReqiredFieldValidator1"
                                runat="server"
                                ControlToValidate="RBLJournalType"
                                ErrorMessage="Select Journal Type!"
                                >
                            </asp:RequiredFieldValidator>--%>

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
        </div>
        <br />
        
        <div>
            
        </div>
             <dxwgv:ASPxGridView ID="Grid_Jurnal" ClientInstanceName="grid_jurnal" 
						runat="server" AutoGenerateColumns="False" KeyFieldName="ID" 
						SettingsDetail-AllowOnlyOneMasterRowExpanded="True" 
        SettingsDetail-ShowDetailButtons="True" SettingsDetail-ShowDetailRow="True" 
        CssFilePath="~/App_Themes/Office2003 Olive/{0}/styles.css" 
        CssPostfix="Office2003_Olive" Width="80%">
<SettingsDetail ShowDetailRow="True" AllowOnlyOneMasterRowExpanded="True"></SettingsDetail>

						   <SettingsPager AlwaysShowPager="True" PageSize="15"></SettingsPager>
						   <Styles CssFilePath="~/App_Themes/Office2003 Olive/{0}/styles.css" 
                               CssPostfix="Office2003_Olive">
                               <Header ImageSpacing="5px" SortingImageSpacing="5px">
                               </Header>
                               <LoadingPanel ImageSpacing="10px">
                               </LoadingPanel>
                           </Styles>
						<SettingsBehavior AllowFocusedRow="True" />
						   <Images ImageFolder="~/App_Themes/Office2003 Olive/{0}/">
                               <CollapsedButton Height="12px" 
                                   Url="~/App_Themes/Office2003 Olive/GridView/gvCollapsedButton.png" 
                                   Width="11px" />
                               <ExpandedButton Height="12px" 
                                   Url="~/App_Themes/Office2003 Olive/GridView/gvExpandedButton.png" 
                                   Width="11px" />
                               <DetailCollapsedButton Height="12px" 
                                   Url="~/App_Themes/Office2003 Olive/GridView/gvCollapsedButton.png" 
                                   Width="11px" />
                               <DetailExpandedButton Height="12px" 
                                   Url="~/App_Themes/Office2003 Olive/GridView/gvExpandedButton.png" 
                                   Width="11px" />
                           </Images>
						<Settings ShowFilterRow="True" />
						<Columns>
							<dxwgv:GridViewDataColumn FieldName="ID" VisibleIndex="0" Visible="False">
							</dxwgv:GridViewDataColumn>
							<dxwgv:GridViewDataColumn FieldName="GJNO" VisibleIndex="1" Visible="True">
							</dxwgv:GridViewDataColumn>
							<dxwgv:GridViewDataDateColumn FieldName="Tanggal" VisibleIndex="2" >
							    <PropertiesdateEdit DisplayFormatString="dd MMMM yyyy"></PropertiesdateEdit>
							</dxwgv:GridViewDataDateColumn>
							<dxwgv:GridViewDataColumn FieldName="Description" VisibleIndex="3">
							</dxwgv:GridViewDataColumn>
							<dxwgv:GridViewDataColumn FieldName="TypeJournal" VisibleIndex="3">
							</dxwgv:GridViewDataColumn>
							<dxwgv:GridViewDataTextColumn Caption="Yang Input" Name="UserName" VisibleIndex="3" 
			                            FieldName="UserName">
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
                             <dxwgv:GridViewDataColumn Caption="#" Name="Print" VisibleIndex="6" Width="1%">
                                <dataitemtemplate>
                                    <asp:LinkButton ID="tbPrint" runat="server" CommandName="Print" 
                                        ToolTip="Print Jurnal">Print</asp:LinkButton>
                                </dataitemtemplate>
                            </dxwgv:GridViewDataColumn>
						</Columns>
						<Templates>
                            <DetailRow>
                            
                                 <dxwgv:ASPxGridView ID="Grid_Jurnal_Detail" runat="server" 
                                                AutoGenerateColumns="true" Font-Size="9pt" KeyFieldName="ID" 
                                                Width="569px" ClientInstanceName = "GridPelunasanDetail" 
                                                onbeforeperformdataselect = "Grid_Jurnal_Detail_DataSelect" 
                                                OnRowCommand="Grid_Jurnal_Detail_RowCommand" >
                                                <Styles CssFilePath="~/App_Themes/Office2003 Olive/{0}/styles.css" 
                               CssPostfix="Office2003_Olive">
                               <Header ImageSpacing="5px" SortingImageSpacing="5px">
                               </Header>
                               <LoadingPanel ImageSpacing="10px">
                               </LoadingPanel>
                           </Styles>
                                                
                                                <SettingsPager Mode="ShowAllRecords"></SettingsPager>
			                                    <Settings ShowFilterRow="True"  />
			                                    <Images ImageFolder="~/App_Themes/Office2003 Olive/{0}/">
                               <CollapsedButton Height="12px" 
                                   Url="~/App_Themes/Office2003 Olive/GridView/gvCollapsedButton.png" 
                                   Width="11px" />
                               <ExpandedButton Height="12px" 
                                   Url="~/App_Themes/Office2003 Olive/GridView/gvExpandedButton.png" 
                                   Width="11px" />
                               <DetailCollapsedButton Height="12px" 
                                   Url="~/App_Themes/Office2003 Olive/GridView/gvCollapsedButton.png" 
                                   Width="11px" />
                               <DetailExpandedButton Height="12px" 
                                   Url="~/App_Themes/Office2003 Olive/GridView/gvExpandedButton.png" 
                                   Width="11px" />
                           </Images>
                                                <SettingsBehavior AllowFocusedRow="True"  />
			                                    <Columns>
                                                    <dxwgv:GridViewDataTextColumn Caption="ID" FieldName="ID" Name="ID" 
                                                        Visible="false" VisibleIndex="1">
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn Caption="GJHeaderID" FieldName="GJHeaderID" Name="GJHeaderID" 
                                                        Visible="false" VisibleIndex="1">
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn Caption="GJNO" FieldName="GJNO" Name="GJNO" 
                                                        Visible="false" VisibleIndex="1">
                                                    </dxwgv:GridViewDataTextColumn>
                                                     <dxwgv:GridViewDataTextColumn Caption="IDDetail" FieldName="IDDetail" Name="IDDetail" 
                                                        Visible="false" VisibleIndex="1">
                                                    </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn FieldName="Types" Name="Types" 
                                                        visible="false" VisibleIndex="2" Caption="Types" >
						                            </dxwgv:GridViewDataTextColumn>
                                                    <dxwgv:GridViewDataTextColumn FieldName="DorC" Name="DorC" 
                                                                    VisibleIndex="3" Caption="D or C" >
						                            </dxwgv:GridViewDataTextColumn>
						                            <dxwgv:GridViewDataTextColumn FieldName="AccCode" Name="AccCode" VisibleIndex="4" Caption="AccCode" Visible = "true" >
						                            </dxwgv:GridViewDataTextColumn>
						                            <dxwgv:GridViewDataTextColumn FieldName="AccName" Name="AccName" VisibleIndex="5" Caption="AccName" >
						                            </dxwgv:GridViewDataTextColumn>
						                            <dxwgv:GridViewDataTextColumn FieldName="Description" Name="Description" VisibleIndex="5" Caption="Description Detail" >
						                            </dxwgv:GridViewDataTextColumn>
						                            <%--<dxwgv:GridViewDataTextColumn FieldName="Amount" Name="Amount" VisibleIndex="6" Caption="Amount" >
						                            <PropertiesTextEdit DisplayFormatString="{0:###,###,###}"></PropertiesTextEdit>
						                            </dxwgv:GridViewDataTextColumn>--%>
						                            <dxwgv:GridViewDataTextColumn Caption="Amount" FieldName="Amount" Name="Amount" VisibleIndex="6">
                                                        <PropertiesTextEdit DisplayFormatString="{0:###,###,###,###,###.###}"></PropertiesTextEdit>
                                                    </dxwgv:GridViewDataTextColumn>
						                            <dxwgv:GridViewDatatextColumn Caption="#" Name="Delete" VisibleIndex="16" Width="6%">
                                                        <DataItemTemplate>
                                                            <asp:LinkButton ID="LBDelChild" runat="server" Text="Delete" CommandName="DelChild" />
                                                        </DataItemTemplate>
                                                    </dxwgv:GridViewDatatextColumn>
						                            
						                            </Columns>
						                            
                                            </dxwgv:ASPxGridView> 
                            </DetailRow>
                        </Templates>
					</dxwgv:ASPxGridView>          
        <div>
		
		<asp:SqlDataSource ID="SqlDataSourceAccount" runat="server" ConnectionString="<%$ ConnectionStrings:LigitaConString %>"
			 SelectCommand="SELECT ID, [Types],[Types] + '.' + SUBSTRING(Code,6,LEN(Code) -5 ) as Code, Name, Levels, Lokasi FROM ChartOfAccount WHERE status = 1 AND Parent <> 'TOP' and Levels <> 1 and Lokasi = 2 ORDER BY Code">
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
		
	</div>
		<asp:HiddenField ID="hfDebit" runat="server" />
	<asp:HiddenField ID="hfIDJurnalUtama" runat="server" />
	<asp:HiddenField ID="hfID" runat="server" />
	<asp:HiddenField ID="hfCredit" runat="server" />
	<asp:HiddenField ID="HfGJNO" runat="server" />
    </form>
</body>
</html>
