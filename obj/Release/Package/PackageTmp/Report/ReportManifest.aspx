<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ReportManifest.aspx.vb" Inherits="LMS.ReportManifest" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v7.3, Version=7.3.7.0, Culture=neutral, PublicKeyToken=49d90c14d24271b5"
	Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dxwgv" %>
	
<%@ Register assembly="DevExpress.Web.ASPxEditors.v7.3, Version=7.3.7.0, Culture=neutral, PublicKeyToken=49d90c14d24271b5" namespace="DevExpress.Web.ASPxEditors" tagprefix="dxe" %>

<%@ Register assembly="DevExpress.Web.ASPxGridView.v7.3" namespace="DevExpress.Web.ASPxGridView" tagprefix="dxwgv" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v7.3" namespace="DevExpress.Web.ASPxEditors" tagprefix="dxe" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<title>Report Manifest</title>
     <style type="text/css" media="screen">
			.NoPrint { FONT-SIZE: 9pt; FONT-FAMILY: Arial }
		 .style1
         {
             width: 200px;
         }
		</style>
		<style type="text/css" media="print">
			.NoPrint, .btn, .kembali { DISPLAY: none }
		</style>		
    <link rel="stylesheet" type="text/css" href="../css/style.css" />
    <link rel="stylesheet" type="text/css" href="../css/main.css"  />
    <link rel="stylesheet" type="text/css" href="../css/print.css"  />
    <script language="javascript" type ="text/javascript" src="../script/main.js" ></script>
     <script type="text/javascript" language="javascript">
   function getmb() {
        returnValue = ShowDialog2('Muat', document.getElementById("hfIDK").value,'Arg', '610', '450');
        if (returnValue) {
            var comp = new Array();
            comp = returnValue.split(";");
            var ID = document.getElementById("hfMID");
            ID.value = comp[0]
            var Name = document.getElementById("TxtCustName");
            Name.value = comp[1];
            var per = document.getElementById("TxtPenerima");
            per.value = comp[2];
        }
    }
    function getTujuan() {
        returnValue = ShowDialog2('Tujuan', 'Arg', '610', '450');
        if (returnValue) {
            var comp = new Array();
            comp = returnValue.split(";");
            var Name = document.getElementById("Ke");
            Name.value = comp[1];
        }
    }
    function getFrom() {
        returnValue = ShowDialog2('Tujuan', 'Arg', '610', '450');
        if (returnValue) {
            var comp = new Array();
            comp = returnValue.split(";");
            var Name = document.getElementById("Dari");
            Name.value = comp[1];
        }
    }
    function getKapal() {
        returnValue = ShowDialog2('Kapal', 'Arg', '610', '450');
        if (returnValue) {
            var comp = new Array();
            comp = returnValue.split(";");            
            var ID = document.getElementById("hfIDK");
            ID.value = comp[0];
            var Name = document.getElementById("TxtKapal");
            Name.value = comp[1];
            var hfName = document.getElementById("hfnamakapal");
            hfName.value = comp[1];

        }
    }
    function getKapalhis() {
        returnValue = ShowDialog2('Kapal', 'Arg', '500', '300');
        if (returnValue) {
            var comp = new Array();
            comp = returnValue.split(";");
            var Name = document.getElementById("TxtKapalHis");
            Name.value = comp[1];
        }
    }

    function getitems() {
        returnValue = Showdialogwarehouse('MuatBarang', 'Arg', '610', '450');
        if (returnValue) {
            var comp = new Array();
            comp = returnValue.split(";");
            var idcustomer = document.getElementById("hfidcustomer");
            idcustomer.value = comp[0];
            var pengirim = document.getElementById("TxtNamaPenerima");
            pengirim.value = comp[6];
            var nopelayaran = document.getElementById("hfnopelayaran");
            nopelayaran.value = comp[1];
            var mbrno = document.getElementById("hfHID");
            mbrno.value = comp[4];
            var idkapal = document.getElementById("hfIDK");
            idkapal.value = comp[3];
        }
    }

    function GetNoKontainer() {
        returnValue = Showdialogwarehouse('Stuffing', 'Arg', '610', '450');
        if (returnValue) {
            var comp = new Array();
            comp = returnValue.split(";");
            var nocont = document.getElementById("HfContStuffing");
            nocont.value = comp[0];
            var nocont2 = document.getElementById("TxtNoKontainer");
            nocont2.value = comp[0];
            var nopelayaran = document.getElementById("hfnopelayaran");
            nopelayaran.value = comp[2];
            
            var KapalID = document.getElementById("HFIDKapal");
            KapalID.value = comp[6];
            var KapalID2 = document.getElementById("hfIDK");
            KapalID2.value = comp[6];
            var MbrNo = document.getElementById("hfHID");
            MbrNo.value = comp[4];
        }
    }

    function GetNoInputKontainer() {
        returnValue = Showdialogwarehouse('LoadContainer', 'Arg', '610', '450');
        if (returnValue) {
            var comp = new Array();
            comp = returnValue.split(";");
            var containercode = document.getElementById("hfcontainercode");
            containercode.value = comp[3];
            var nocont2 = document.getElementById("TxtInputContainer");
            nocont2.value = comp[0];
            var nopelayaran = document.getElementById("hfnopelayaran");
            nopelayaran.value = comp[2];

            var KapalID = document.getElementById("HFIDKapal");
            KapalID.value = comp[6];
            var KapalID2 = document.getElementById("hfIDK");
            KapalID2.value = comp[6];
            var MbrNo = document.getElementById("hfHID");
            MbrNo.value = comp[4];
        }
    }
    
    </script>
</head>
<body>
    <form id="form1" runat="server">
     
    
    <div class = "Divutama">
    <asp:Panel ID = "PanelJudul" runat = "server">
    <div class="formtitle">Report Surat Muat dan Manifest</div>
    
    <br />
   </asp:Panel>
  <asp:Panel ID = "PanelPilihReport" runat = "server">
                <table>                  
                    <tr>
                        <td>
                            Tipe Report
                        </td>
                        <td>
                            :
                        </td>
                        
                        <td>
                            <asp:DropDownList ID="ddltype" runat="server">
                                    <asp:ListItem Value="-Pilih-">-Pilih-</asp:ListItem>
                                    <asp:ListItem Value="SuratMuat" >Surat Muat</asp:ListItem>
                                    <asp:ListItem Value="Manifest">Manifest</asp:ListItem>
                    </asp:DropDownList>
                        </td>
                        
                    </tr>
                    
                </table>
         </asp:Panel>    
                
    <asp:Panel ID = "Panel_Input" runat = "server">
       <div  class="div_input" >
        <div class="div_umum">
        
                
                <table style="width: 502px">
                
                    <tr>
                    
                        <td class="style1" >
                            Tanggal Keberangkatan</td>
                        <td>:</td>
                        <td  style =" width:100px;" >
                            <dxe:ASPxDateEdit ID="ManifestDate" runat="server" EditFormat="Custom" EditFormatString="dd MMMM yyyy">
                            </dxe:ASPxDateEdit>
                        </td>
                    </tr>
                   <%-- <tr>
                        <td class="style1" >
                            Dari
                        </td>
                        <td>:</td>
                        <td>
                             <asp:TextBox ID="Dari" runat="server"></asp:TextBox>
                            <img alt="Browse" src="../images/search.png" onclick="javascript:getFrom();" />
                        </td>
                    </tr>--%>
                     <tr>
                        <td class="style1" >
                            Dari
                        </td>
                        <td>:</td>
                        <td>
                            <asp:DropDownList ID="DDLDari" runat="server">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1" >
                            Menuju
                        </td>
                        <td>:</td>
                        <td>
                            <asp:DropDownList ID="DDLKe" runat="server">
                            </asp:DropDownList>
                        </td>
                    </tr>
                   <%-- <tr>
                        <td class="style1" >
                            Ke
                        </td>
                        <td>:</td>
                        <td>
                            <asp:TextBox ID="Ke" runat="server"></asp:TextBox> 
                            <img alt="Browse" src="../images/search.png" onclick="javascript:getTujuan();" />
                        </td>
                    </tr>--%>
                    <tr>
                        <td >
                            Header
                        </td>
                        <td>:</td>
                        <td>
                            <asp:DropDownList ID="ddlHeader" runat = "server" ></asp:DropDownList>
                        </td>
                    </tr>

                    <tr>
                        <td >
                            Kapal
                        </td>
                        <td >:</td>
                        <td>
                            <asp:TextBox ID = "TxtKapal" runat = "server" ReadOnly =true Width="140px" ></asp:TextBox>
                            <img alt="Browse" onclick="javascript:getKapal();" src="../images/search.png" /></td>
                    </tr>
                                        <tr>
			                        <td>Nama Gudang</td>
                                    <td >:</td>
			                        <td > <asp:DropDownList 
                                            ID="DdlNamaGudang" runat="server" Width="157">
                                        </asp:DropDownList>&nbsp;</td>
            						                        
		                        </tr>
                    <tr>
                        <td>
                            <table>
                                <tr>
                                    <td align="left">
                                        <dxe:ASPxButton ID="btAdd" runat="server" Text="Add">
                                        </dxe:ASPxButton>
                                    </td>
                                    <td>
                                        <dxe:ASPxButton ID="btView" runat="server" Text="Create" Width="95px" >
                                        </dxe:ASPxButton>
                                    </td>
                                    <td>
                                        <dxe:ASPxButton ID="btReset" runat="server" Text="Reset">
                                        </dxe:ASPxButton>
                                    </td>
                                    <td>
                                        <dxe:ASPxButton ID="btViewHistory" runat="server" Text="ViewHistory">
                                        </dxe:ASPxButton>
                                    </td>
                             
                                </tr>
                            </table>
                        </td>
                    <td>
                    </td>
                    <td>
                        <asp:Label ID= "lblError" runat ="server" CssClass = "error"></asp:Label>                    
                    </td>
                    </tr>
                </table>
 </div>
    </div> </asp:Panel>
            </div>
  
            <asp:Panel ID ="historyinput" runat ="server">
            <div class = "Divutama">
             <div class = "div_input">
            <div class = "div_umum">
                <table>
                    
                    <tr>
                        <td class="style1">Pilih Header</td>
                         <td>
                             <asp:DropDownList ID="ddlHeaderHist" runat="server">
                             </asp:DropDownList>
                        </td>
                        
                    </tr>
                    <tr>
                        <td class="style1">Tipe Print</td>
                         <td>
                             <asp:DropDownList ID="DDLTipePrint" runat="server" AutoPostBack="true">
                             <asp:ListItem Value="SemuaKapal">Semua Isi Kapal</asp:ListItem>
                             <asp:ListItem Value="PerCustomer">Per Customer</asp:ListItem>
                             <asp:ListItem Value="Stuffing">Stuffing</asp:ListItem>
                             <asp:ListItem Value="InputContainer">Input Container</asp:ListItem>
                             </asp:DropDownList>
                        </td>
                        
                    </tr>
                    
                    <tr>
                    
                        <td colspan = "2">
                        <asp:Panel runat="server" ID="PanelNamaPenerima">
                        <div>
                            <table>
                                <tr>
                                    <td>
                                        Nama Penerima
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TxtNamaPenerima" runat="server" Width="300px" ReadOnly="true"></asp:TextBox>
                                        <img alt="Browse" onclick="javascript:getitems();" src="../images/search.png" /></td>
                                    </td>
                                    <td>
                                        <asp:Button ID="BtnPrint" runat="server" Text="Cetak" />
                                    </td>
                                </tr>
                            </table>
                            </div>
                          </asp:Panel>
                        </td>
                        
                        </tr>
                        <tr>
                        <td colspan = "2">
                        <asp:Panel runat="server" ID="PanelContainer">
                        <div>
                            <table>
                                <tr>
                                    <td>
                                        No Kontainer
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TxtNoKontainer" runat="server" Width="300px" ReadOnly="true"></asp:TextBox>
                                        <img alt="Browse" onclick="javascript:GetNoKontainer();" src="../images/search.png" /></td>
                                    </td>
                                    <td>
                                        <asp:Button ID="BtnView2" runat="server" Text="Cetak" />
                                    </td>
                                </tr>
                            </table>
                            </div>
                          </asp:Panel>
                        </td>
                        </tr>
                        
                        <tr>
                        <td colspan = "2">
                        <asp:Panel runat="server" ID="PanelInputKontainer">
                        <div>
                            <table>
                                <tr>
                                    <td>
                                        No Kontainer
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TxtInputContainer" runat="server" Width="300px" ReadOnly="true"></asp:TextBox>
                                        <img alt="Browse" onclick="javascript:GetNoInputKontainer();" src="../images/search.png" /></td>
                                    </td>
                                    <td>
                                        <asp:Button ID="BtnPrintInputContainer" runat="server" Text="Cetak" />
                                    </td>
                                </tr>
                            </table>
                            </div>
                          </asp:Panel>
                        </td>
                        </tr>
                    
                    
                    <tr>
                        <td colspan = "3" align = "center">
                            <asp:Label ID= "lblerror2" runat ="server" CssClass = "error" Visible="False"></asp:Label>
                            <asp:Label ID= "lblBerhasil" runat ="server" CssClass = "berhasil" Visible="False"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style1">
                        <table>
                        <tr>
                        <td>
                          <%--  <dxe:ASPxButton ID="ViewGrid" runat="server" Text="ViewGrid">
                            </dxe:ASPxButton>--%>
                        </td>
                           <td>
                            <dxe:ASPxButton ID="Back" runat="server" Text="Back">
                            </dxe:ASPxButton>
                        </td>
                        </tr>  
                        </table> 
                        </td>
                    </tr>
                </table>
                <asp:HiddenField ID="hfidcustomer" runat="server" />
                <asp:HiddenField ID="hfnopelayaran" runat="server" />
                <asp:HiddenField ID="HfContStuffing" runat="server" />
                <asp:HiddenField ID="HFIDKapal" runat="server" />
               
                       </div>
        </div>
        </div>
            </asp:Panel>

<div>
   
</div>
       
            <asp:Panel ID ="historygrid" runat ="server" >
            <div class = "Divutama">
            <dxwgv:ASPxGridView ID="Grid_History" runat="server" 
                    AutoGenerateColumns="False" Font-Size="9pt" KeyFieldName="ID" 
                    Width="1008px" ClientInstanceName = "grid_manifest" 
                    SettingsDetail-AllowOnlyOneMasterRowExpanded="True" 
                    SettingsDetail-ShowDetailRow="True">
                    <styles>
                        <header backcolor="#2c3848" font-bold="true" forecolor="#FFFFFF" 
                            horizontalalign="Center" hoverstyle-border-bordercolor="#515763">
                            <hoverstyle>
                                <border bordercolor="#515763"></border>
                            </hoverstyle>
                        </header>
                        <FocusedRow BackColor="#D3D1D4" ForeColor="#000000"></FocusedRow>
                        <AlternatingRow Enabled="True" BackColor="#f4f4e9"></AlternatingRow>
                        <Row BackColor="#ffffff"></Row>
                       
                    </styles>
                    <SettingsPager Mode="ShowPager"></SettingsPager>
			        <SettingsDetail AllowOnlyOneMasterRowExpanded="True" ShowDetailRow="True" />
			        <Settings ShowFilterRow="True"  />
                    <SettingsBehavior AllowFocusedRow="True" />
			        <Columns>
                        <dxwgv:GridViewDataColumn Caption="ID" FieldName="ID" Name="ID" 
                            Visible="False" VisibleIndex="1">
                        </dxwgv:GridViewDataColumn>
                        <dxwgv:GridViewDataDateColumn FieldName="Depart_Date" Name="Depart_Date" 
                            Visible="true" VisibleIndex= "0" Caption="Tanggal Keberangkatan" >
						    <PropertiesdateEdit DisplayFormatString="dd MMMM yyyy"></PropertiesdateEdit>
						</dxwgv:GridViewDataDateColumn>
						<dxwgv:GridViewDataColumn FieldName="Kapal" Name="Kapal" 
                            Visible="False" VisibleIndex= "3" Caption="Kapal" Width="1%" >
						</dxwgv:GridViewDataColumn>
						<dxwgv:GridViewDataColumn FieldName="Nama_Kapal" Name="Nama_Kapal" 
                            VisibleIndex= "1" Caption="Nama Kapal" >
						</dxwgv:GridViewDataColumn>
						<dxwgv:GridViewDataColumn FieldName="NoPelayaran" Name="NoPelayaran" 
                            VisibleIndex= "2" Caption="Nomor Pelayaran" >
						</dxwgv:GridViewDataColumn>
						<dxwgv:GridViewDataColumn Caption="Dari" FieldName="Dari" Name="Dari" 
                            VisibleIndex="3">
                        </dxwgv:GridViewDataColumn>
                        <dxwgv:GridViewDataColumn Caption="Tujuan" FieldName="Tujuan" Name="Tujuan" 
                            VisibleIndex="4">
                        </dxwgv:GridViewDataColumn>
                        <dxwgv:GridViewDataColumn Caption="#" Name="Arrive" 
                            VisibleIndex="5" Width="1%">
                            <dataitemtemplate>
                                <asp:LinkButton ID="tbArrive" runat="server" CommandName="Arrive" 
                                    ToolTip="Kapal Tiba">Arrive</asp:LinkButton>
                            </dataitemtemplate>
                        </dxwgv:GridViewDataColumn>
                        <dxwgv:GridViewDataColumn Caption="#" Name="Reset" VisibleIndex="6" 
                            Width="30px">
                                            <dataitemtemplate>
                                                <asp:LinkButton ID="tbResetPengiriman" runat="server" CommandName="Reset" 
                                                    ToolTip="Reset Pengiriman">Reset Pengiriman</asp:LinkButton>
                                            </dataitemtemplate>
                        </dxwgv:GridViewDataColumn>
                        <dxwgv:GridViewDataColumn Caption="#" Name="Print" VisibleIndex="7" Width="1%">
                                            <dataitemtemplate>
                                                <asp:LinkButton ID="tbPrint" runat="server" CommandName="Print" 
                                                    ToolTip="Print">View</asp:LinkButton>
                                            </dataitemtemplate>
                        </dxwgv:GridViewDataColumn>
                    </Columns>
                    <Templates>
                        <DetailRow>
                            <dxwgv:ASPxGridView ID="Grid_History_Child" runat="server" 
                                AutoGenerateColumns="true" Font-Size="9pt" KeyFieldName="ID" 
                                Width="569px" ClientInstanceName = "grid_manifest_child" onbeforeperformdataselect="Grid_History_Child_DataSelect">
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
                                <SettingsPager Mode="ShowAllRecords"></SettingsPager>
			                    <Settings ShowFilterRow="True"  />
                                <SettingsBehavior AllowFocusedRow="True" />
			                    <Columns>
                                    <dxwgv:GridViewDataColumn Caption="ID" FieldName="ID" Name="ID" 
                                        Visible="false" VisibleIndex="1">
                                    </dxwgv:GridViewDataColumn>
                                    <dxwgv:GridViewDataColumn FieldName="Nama_Barang" Name="Nama_Barang" 
                                        Visible="true" VisibleIndex= "2" Caption="Nama Barang" >
						            </dxwgv:GridViewDataColumn>
						            <dxwgv:GridViewDataTextColumn FieldName="Total_Berat" Name="Total_Berat" 
                                        VisibleIndex= "3" Caption="Total Berat" >
						            </dxwgv:GridViewDataTextColumn>
						            <dxwgv:GridViewDataTextColumn FieldName="Ukuran" Name="Ukuran" 
                                        VisibleIndex= "4" Caption="Ukuran" >
                                        <PropertiesTextEdit DisplayFormatString="{0:###,###,##0.00000;}"></PropertiesTextEdit>
						            </dxwgv:GridViewDataTextColumn>
						            <dxwgv:GridViewDataColumn FieldName="Jumlah_Colli" Name="Jumlah_Colli" 
                                        VisibleIndex= "5" Caption="Jumlah Colli" >
						            </dxwgv:GridViewDataColumn>
                                </Columns>    
                            </dxwgv:ASPxGridView>     
                                        
                        </DetailRow>
                    </Templates>
                </dxwgv:ASPxGridView>     
               </div>
            </asp:Panel>
        
        
            <asp:Panel ID ="Panel_Grid" runat ="server" >
            <div class = "Divutama">
                <dxwgv:ASPxGridView ID="Grid_Manifest" runat="server" 
                    AutoGenerateColumns="true" Font-Size="9pt" KeyFieldName="IDM" 
                    Width="100%" ClientInstanceName = "grid_manifest_awal">
                    <styles>
                        <header backcolor="#2c3848" font-bold="true" forecolor="#FFFFFF" horizontalalign="Center" hoverstyle-border-bordercolor="#515763">
                            <hoverstyle>
                                <border bordercolor="#515763"></border>
                            </hoverstyle>
                        </header>
                        <FocusedRow BackColor="#D3D1D4" ForeColor="#000000"></FocusedRow>
                        <AlternatingRow Enabled="True" BackColor="#f4f4e9"></AlternatingRow>
                        <Row BackColor="#ffffff"></Row>
                       
                    </styles>
                    <SettingsPager Mode="ShowAllRecords"></SettingsPager>
			        <Settings ShowFilterRow="True"  />
                    <SettingsBehavior AllowFocusedRow="True" />
			        <Columns>
                        <dxwgv:GridViewDataColumn Caption="ID" FieldName="IDM" Name="IDM" 
                            Visible="true" VisibleIndex="1">
                        </dxwgv:GridViewDataColumn>
                        <dxwgv:GridViewDataColumn FieldName="MB_NOM" Name="MB_NOM" 
                            Visible="false" VisibleIndex= "2" Caption="MB_NOM" >
						</dxwgv:GridViewDataColumn>
                        <dxwgv:GridViewDataColumn FieldName="MB_IDM" Name="MB_IDM" 
                            Visible="true" VisibleIndex= "2" Caption="MB ID" >
						</dxwgv:GridViewDataColumn>
						 <dxwgv:GridViewDataColumn FieldName="QuotationDetailIDM" Name="QuotationDetailIDM" 
                            Visible="false" VisibleIndex= "2" Caption="QuotationDetailIDM" >
						</dxwgv:GridViewDataColumn>
                        <dxwgv:GridViewDataColumn FieldName="NoContainerM" Name="NoContainerM" 
                            Visible="true" VisibleIndex= "2" Caption="Nomor Kontainer" >
						</dxwgv:GridViewDataColumn>
						<dxwgv:GridViewDataColumn FieldName="CustomerM" Name="CustomerM" 
                            VisibleIndex= "3" Caption="Kostumer" >
						</dxwgv:GridViewDataColumn>
						<dxwgv:GridViewDataColumn FieldName="PenerimaM" Name="PenerimaM" 
                            VisibleIndex= "4" Caption="Penerima" >
						</dxwgv:GridViewDataColumn>
                        <dxwgv:GridViewDataColumn FieldName="MerkM" Name="MerkM" 
                                        VisibleIndex="5" Caption="Merk" >
						</dxwgv:GridViewDataColumn>
						<dxwgv:GridViewDataColumn FieldName="JumlahColliM" Name="JumlahColliM" 
                                        VisibleIndex="6" Caption="Jumlah Colli" >
						</dxwgv:GridViewDataColumn>
						<dxwgv:GridViewDataColumn FieldName="NamaBarangM" Name="NamaBarangM" VisibleIndex="7" Caption="Nama Barang" >
						</dxwgv:GridViewDataColumn>
				        <dxwgv:GridViewDataColumn FieldName="BeratM" Name="BeratM" VisibleIndex="8" Caption="Berat" >
						</dxwgv:GridViewDataColumn>
				        <dxwgv:GridViewDataColumn FieldName="UkuranM" Name="UkuranM" VisibleIndex="9" Caption="Ukuran" >
						</dxwgv:GridViewDataColumn>
						<dxwgv:GridViewDataColumn FieldName="PanjangM" Name="PanjangM" VisibleIndex="9" Caption="Panjang" >
						</dxwgv:GridViewDataColumn>
						<dxwgv:GridViewDataColumn FieldName="LebarM" Name="LebarM" VisibleIndex="9" Caption="Lebar" >
						</dxwgv:GridViewDataColumn>
						<dxwgv:GridViewDataColumn FieldName="TinggiM" Name="TinggiM" VisibleIndex="9" Caption="Tinggi" >
						</dxwgv:GridViewDataColumn>
						<dxwgv:GridViewDataColumn FieldName="VolumeM" Name="VolumeM" VisibleIndex="9" Caption="VolumeM" Visible = "true" >
						</dxwgv:GridViewDataColumn>
							<dxwgv:GridViewDataColumn FieldName="QuantityM" Name="QuantityM" VisibleIndex="9" Caption="Quantity" >
						</dxwgv:GridViewDataColumn>
				        <dxwgv:GridViewDataColumn FieldName="Nama_KapalM" Name="Nama_KapalM" VisibleIndex="10" Caption="Nama Kapal" >
						</dxwgv:GridViewDataColumn>
				        <dxwgv:GridViewDataColumn FieldName="KapalM" Name="KapalM" VisibleIndex="11" Caption="Kapal" Visible = "false" >
						</dxwgv:GridViewDataColumn>
				        <dxwgv:GridViewDataColumn FieldName="KeteranganM" Name="KeteranganM" VisibleIndex="12" Caption="Keterangan" >
						</dxwgv:GridViewDataColumn>
						<dxwgv:GridViewDataColumn FieldName="NamaSupplierM" Name="NamaSupplierM" VisibleIndex="12" Caption="Nama Pengirim" >
						</dxwgv:GridViewDataColumn>
						<dxwgv:GridViewDataColumn FieldName="NoSealM" Name="NoSealM" VisibleIndex="12" Caption="No Seal" Visible="false" >
						</dxwgv:GridViewDataColumn>
						<dxwgv:GridViewDataColumn FieldName="WarehouseHeaderIDM" Name="WarehouseHeaderIDM" VisibleIndex="12" Caption="WarehouseHeaderIDM" Visible="true" >
						</dxwgv:GridViewDataColumn>
						<dxwgv:GridViewDataColumn FieldName="IDDetailWarehouseDetail" Name="IDDetailWarehouseDetail" VisibleIndex="12" Caption="IDDetailWarehouseDetail" Visible="false" >
						</dxwgv:GridViewDataColumn>
						
                        <dxwgv:GridViewDataColumn Caption="#" Name="Delete" VisibleIndex="13" Width="1%">
                                            <dataitemtemplate>
                                                <asp:LinkButton ID="tbDelete" runat="server" CommandName="Delete" 
                                                    ToolTip="Edit Barang">Delete</asp:LinkButton>
                                            </dataitemtemplate>
                        </dxwgv:GridViewDataColumn>
                    </Columns>
                </dxwgv:ASPxGridView>     
               </div>
            </asp:Panel>

        <div align ="center">
            <asp:Panel ID = "Panel_Report" runat = "server" >
            <asp:Label  ID="lblReport" runat="server"></asp:Label>
            
            <table style="width: 100%" align="center">
                <tr align ="center">
                    <td align ="center">
                    <br />
                    <table align ="center">
                        <tr align ="center">
                            <td>
                                <input type="button" onclick="window.print()" value="Print" class="btn" />
                            </td>
                            <td>
                                <asp:Button CssClass="btn" ID="btKembaliDevPeriod" runat="server" 
                        Text="Kembali" Width="89px" /> 
                            </td>
                        </tr>
                    </table>
                        
                    </td>
                </tr>
            </table>
            
            </asp:Panel>
        
    </div>
    	<asp:HiddenField ID="hfHID" runat="server" />
    	<asp:HiddenField ID="hfMID" runat="server" />
    	<asp:HiddenField ID="hfnamakapal" runat="server" />
    	<asp:HiddenField ID="hfIDK" runat="server" />
    	<asp:HiddenField ID="hfcontainercode" runat="server" />

    </form>
    
</body>

</html>
