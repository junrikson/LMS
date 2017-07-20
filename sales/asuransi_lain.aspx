<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="asuransi_lain.aspx.vb" Inherits="LMS.AsuransiLain" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v7.3, Version=7.3.7.0, Culture=neutral, PublicKeyToken=49d90c14d24271b5"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dxe" %>
    
    <%@ Register Assembly="DevExpress.Web.ASPxGridView.v7.3, Version=7.3.7.0, Culture=neutral, PublicKeyToken=49d90c14d24271b5"
	Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dxwgv" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Input asuransi Lain</title>
    <link rel="stylesheet" type="text/css" href="../css/style.css" />
    <link rel="stylesheet" type="text/css" href="../css/main.css"  />
    <style type="text/css">
        .style1
        {
            width: 40px;
        }
     </style>
<script language="javascript" type="text/javascript" src="../script/main.js" ></script>

<script type="text/javascript" language="javascript">

    function changenumberformat(control) {
        var conval = document.getElementById(control).value;
        if (conval.lastIndexOf(",") != conval.length - 1) {
            document.getElementById(control).value = FormatNumberBy3(conval.replace(/[.]/g, ""), ",", ".");
        }
        else {
            document.getElementById(control).value = conval;
        }
    }

    function getCustomer() {
        returnValue = ShowDialog2('MasterCustomer', 'Arg', '610', '450');
        if (returnValue) {
                var comp = new Array();
                comp = returnValue.split(";");

                var cust2 = document.getElementById("hfCodeCustomer");
                cust2.value = comp[2];
                var Name = document.getElementById("TxtCustName");
                Name.value = comp[1];
                var Name2 = document.getElementById("hfNamaCustomer");
                Name2.value = comp[1];

        }
    }


</script>

</head>
<body>
    <form id="form1" runat="server">
    <div class = "Divutama" >
        <div class="formtitle"><b>Input Asuransi Lain</b></div>
		<br />
		<div  class="div_input" >
		<div class="div_umum">
            <table >
                <tr>
                <td>
                    <table style="width: 600px" class = "borderdot">
                         
                <tr>
                
				        <td>
				            No Asuransi
				        </td>
				        <td>
				            <asp:TextBox ID="TxtNoAsuransi" runat="server" Width="256px" ReadOnly="True"></asp:TextBox>
				        </td>
				    </tr>
				    <tr>
                    <td >
                        Tanggal Asuransi</td>
                    <td >
                        <dxe:ASPxDateEdit ID="DtAsuransi" runat="server" EditFormat="Custom" 
									EditFormatString="dd MMMM yyyy" Cursor ="pointer" Height="21px" Width="180px">
                        </dxe:ASPxDateEdit>
                    </td>
                </tr>
                <tr>
				        <td>
                            Nama Customer
				        </td>
				        <td>
				            <asp:TextBox ID="TxtCustName" runat="server" Width="180px" ReadOnly ="true"></asp:TextBox>
				            <img alt="Browse" src="../images/search.png" onclick="javascript:getCustomer();" />
				        </td>
				    </tr>
				    <tr>
                    <td >
                        Periode </td>
                    <td >
                        <table>
                            <tr>
                                <td>
                                    <dxe:ASPxDateEdit ID="DtPeriodeAwal" runat="server" EditFormat="Custom" 
									EditFormatString="dd MMMM yyyy" Cursor ="pointer" Height="21px" Width="150px">
                                    </dxe:ASPxDateEdit>
                                </td>
                                <td>
                                    s/d
                                </td>
                                <td>
                                    <dxe:ASPxDateEdit ID="DtPeriodeAkhir" runat="server" EditFormat="Custom" 
									EditFormatString="dd MMMM yyyy" Cursor ="pointer" Height="21px" Width="150px">
                                    </dxe:ASPxDateEdit>
                                </td>
                            </tr>
                        </table>
                        
                        
                        
                    </td>
                </tr>
				    <tr>
				        <td>
                            Harga
				        </td>
				        <td>
				            <asp:TextBox ID="TxtHargaAsuransi" runat="server" ></asp:TextBox>
				        </td>
				    </tr>
				    <tr>
				        <td>
				            Premi
				        </td>
				        <td>
                            <asp:TextBox ID="TxtPremi" runat="server"></asp:TextBox>
				        </td>
				    </tr>
				    <tr>
				        <td>
				            Biaya Polis/Materai
				        </td>
				        <td>
                            <asp:TextBox ID="TxtPolis" runat="server" ></asp:TextBox>
				        </td>
				    </tr>
				    <tr>
				        <td>
				            Discount
				        </td>
				        <td>
                            <asp:TextBox ID="TxtDiscount" runat="server" ></asp:TextBox>
				        </td>
				    </tr>
				    <tr>
				        <td>
				            keterangan
				        </td>
				        <td>
                            <asp:TextBox ID="TxtKetAsuransi" runat="server"  
                                TextMode="MultiLine" Height="104px" Width="212px"></asp:TextBox>
				        </td>
				    </tr>
                    <td colspan = "3">
                        <asp:Label ID="linfoberhasil" runat="server" CssClass = "berhasil" Visible="False"></asp:Label>
				        &nbsp;<asp:Label ID="lInfo" runat="server" CssClass="error" Visible="False"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table>
					        <tr>
		                        <td align="right" >
			                        <dxe:ASPxButton ID="btSimpan" runat="server" Text="Simpan" Width="90px">
				                        <Image Url="../images/save-alt.png" />
			                        </dxe:ASPxButton>
		                        </td>
		                        <td align="left" >
        	                        <dxe:ASPxButton ID="btBatal" runat="server" Text="Reset" Width="90px">
				                        <Image Url="../images/undo.png" />
			                        </dxe:ASPxButton>
		                        </td>        
		                    </tr>
					    </table>
					</td>
                </tr></table>
                        </table>
                    </td>
                </tr>
            
                
            </table>
         
        <br />
        
        
            <dxwgv:ASPxGridView ID="Grid_Asuransi" runat="server" AutoGenerateColumns = "false" ClientInstanceName = "grid"
                 KeyFieldName = "ID" Font-Size = "9pt" Width = "100%">
                <SettingsBehavior AllowFocusedRow="True" />
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
                    <SettingsPager AlwaysShowPager="True" PageSize="15"></SettingsPager>
                    <Settings ShowFilterRow="True"  />
                    <SettingsBehavior AllowFocusedRow="True" />
                <Columns>
                    <dxwgv:GridViewDataTextColumn Caption="ID" FieldName="ID" Name="ID" 
                        Visible="false" VisibleIndex="0">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataDateColumn Caption="Tanggal" FieldName="Tanggal" 
                        Name="Tanggal"  Visible="true" VisibleIndex="1" Width="160px" >
                        <PropertiesdateEdit DisplayFormatString="dd MMMM yyyy"></PropertiesdateEdit>
                    </dxwgv:GridViewDataDateColumn>
                    
                    <dxwgv:GridViewDataTextColumn Caption="No Asuransi" FieldName="NoAsuransi" 
                        Name="NoAsuransi"  Visible="true" VisibleIndex="1">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataColumn Caption="Nama Customer" FieldName="NamaCustomer" 
                        Name="NamaCustomer"  Visible="true" VisibleIndex="1" Width="140px">
                    </dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn Caption="Code Customer" FieldName="CodeCustomer" 
                        Name="CodeCustomer"  Visible="false" VisibleIndex="1" Width="140px">
                    </dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn Caption="Periode" FieldName="Periode" 
                        Name="Periode"  Visible="true" VisibleIndex="1" Width="150px">
                    </dxwgv:GridViewDataColumn>
                    
                    <dxwgv:GridViewDataTextColumn Caption="Harga Asuransi" FieldName="HargaAsuransi" 
                        Name="HargaAsuransi"  Visible="true" VisibleIndex="2" Width="140px">
                        <PropertiesTextEdit DisplayFormatString="{0:###,###,###}"></PropertiesTextEdit>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Premi" FieldName="Premi" 
                        Name="Premi"  Visible="true" VisibleIndex="3" Width="140px">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Polis" FieldName="Polis" 
                        Name="Polis"  Visible="true" VisibleIndex="3">
                        <PropertiesTextEdit DisplayFormatString="{0:###,###,###}"></PropertiesTextEdit>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Discount" FieldName="Discount" 
                        Name="Discount"  Visible="true" VisibleIndex="3" >
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Total Asuransi" FieldName="TotalAsuransi" 
                        Name="TotalAsuransi"  Visible="true" VisibleIndex="3" Width="140px">
                        <PropertiesTextEdit DisplayFormatString="{0:###,###,###}"></PropertiesTextEdit>
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataTextColumn Caption="Keterangan" FieldName="Keterangan" 
                        Name="Keterangan"  Visible="true" VisibleIndex="3" Width="140px">
                    </dxwgv:GridViewDataTextColumn>
                    <dxwgv:GridViewDataColumn Caption="#" Name="Edit" VisibleIndex="3" Width="1%">
                        <dataitemtemplate>
                            <asp:LinkButton ID="tbEdit" runat="server" CommandName="Edit" 
                                ToolTip="Edit Barang">Edit</asp:LinkButton>
                        </dataitemtemplate>
                    </dxwgv:GridViewDataColumn>
                    <dxwgv:GridViewDataColumn Caption="#" Name="Delete" VisibleIndex="4" Width="1%">
                        <dataitemtemplate>
                            <asp:LinkButton ID="tbDelete" runat="server" CommandName="Delete" 
                                ToolTip="Delete Barang" OnClientClick="return confirm('Are You Sure Want to Delete ?');" >Delete</asp:LinkButton>
                        </dataitemtemplate>
                    </dxwgv:GridViewDataColumn>
                </Columns>
            </dxwgv:ASPxGridView>
            
            <br />
            </div>
        </div>
    </div>
	<asp:HiddenField ID="hfNoAsuransi" runat="server" />
	<asp:HiddenField ID="hfMode" runat="server" />
	<asp:HiddenField id="hfNamaCustomer" runat="server"></asp:HiddenField>
         <asp:HiddenField id="hfCodeCustomer" runat="server"></asp:HiddenField>
    </form>
</body>
</html>
