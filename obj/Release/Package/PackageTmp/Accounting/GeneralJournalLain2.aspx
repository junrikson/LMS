<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="GeneralJournalLain2.aspx.vb" Inherits="LMS.GeneralJournalLain2" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v7.3, Version=7.3.7.0, Culture=neutral, PublicKeyToken=49d90c14d24271b5"
	Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dxwgv" %>
	
	<%@ Register Assembly="DevExpress.Web.v7.3, Version=7.3.7.0, Culture=neutral, PublicKeyToken=49d90c14d24271b5"
	Namespace="DevExpress.Web.ASPxPopupControl" TagPrefix="dxpc" %>

<%@ Register assembly="DevExpress.Web.ASPxEditors.v7.3, Version=7.3.7.0, Culture=neutral, PublicKeyToken=49d90c14d24271b5" namespace="DevExpress.Web.ASPxEditors" tagprefix="dxe" %>

<%@ Register assembly="System.Web, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="System.Web.UI" tagprefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Jurnal Lain-Lain</title>
    <link rel="stylesheet" type="text/css" href="../css/style.css" />
    <link rel="stylesheet" type="text/css" href="../css/main.css"  />
    
   <script type= "text/javascript" language="javascript" src="../script/main.js" ></script>
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
            document.getElementById("hfAccountType").value = values[3];

            Account_PopUp.Hide();
            document.getElementById("tbAmountOthersJournal").focus();
        }
        
     </script>
    
</head>
<body>
    <form id="form1" runat="server">
    <div class = "Divutama" >
        <div class="formtitle">Journal Lain - Lain</div>
		<br />
		<div  class="div_input" >
		    <div class="div_umum">
            
           <br />
                
			    
                        <asp:HiddenField ID="HFInvoiceHeaderID" runat="server" />
                        <asp:HiddenField ID = "HFID" runat="server" />
                        <asp:HiddenField ID = "HFMode" runat="server" />
				        <asp:HiddenField ID="hfDel" runat="server" />
				        <asp:HiddenField ID="HFNoInvoice" runat="server" />
				        <asp:HiddenField ID="HFTanggal" runat="server" />
				        <asp:HiddenField ID="HFCodeCust" runat="server" />
				        <asp:HiddenField ID="HFNmCust" runat="server" />
				<div align = "left" style="width:100%">
				     
				</div>
				<br />
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
				                Nama Customer
				            </td>
				            <td>
				                <asp:TextBox ID="TxtNmLain" runat="server" Width="200px" ReadOnly="true"></asp:TextBox><img alt="Browse" src="../images/search.png" onclick="javascript:getCustomer();" />
				            </td>
				            
				        </tr>
				        <tr>
                    
                    
                    
                <td>
                        No</td>
                    <td> <asp:TextBox ID="TxtNoInvoice" runat="server" Width="200px"></asp:TextBox>
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
                    Kredit :
               </td>
               
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
               
               </tr>
               
                <tr>
				            <td>
				                Keterangan :
				            </td>
				            <td>
				                <asp:TextBox ID="TxtKeterangan" runat="server" 
                                    Width="200px" TextMode="MultiLine"></asp:TextBox>
				            </td>
				 </tr>
               <TR>
                    <td class="style4">
                        <asp:CheckBox ID = "chkAsuransi" runat="server" Text = "Asuransi" AutoPostBack="True" Visible="false" /> </td>
                    <td></td>
                    
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
                
                
                
            
            </div>
            
            		<asp:SqlDataSource ID="SqlDataSourceAccount" runat="server" ConnectionString="<%$ ConnectionStrings:LigitaConString %>"
			 SelectCommand="SELECT ID, [Types],[Types] + '.' + SUBSTRING(Code,6,LEN(Code) -5 ) as Code, Name, Levels, Lokasi FROM ChartOfAccount WHERE Parent <> 'TOP' and status = 1 and Levels <> 1 ORDER BY Code">
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
        </div>
    </form>
</body>
</html>
