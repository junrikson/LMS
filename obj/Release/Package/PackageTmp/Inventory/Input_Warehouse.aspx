<%@ Page Language="VB" AutoEventWireup="false" Inherits="LMS.Inventory_Input_Warehouse" Codebehind="Input_Warehouse.aspx.vb" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v7.3, Version=7.3.7.0, Culture=neutral, PublicKeyToken=49d90c14d24271b5"
	Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dxwgv" %>

<%@ Register assembly="DevExpress.Web.ASPxEditors.v7.3, Version=7.3.7.0, Culture=neutral, PublicKeyToken=49d90c14d24271b5" namespace="DevExpress.Web.ASPxEditors" tagprefix="dxe" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd"><html xmlns="http://www.w3.org/1999/xhtml"><head runat="server"><title>Input Gudang</title><link rel="stylesheet" type="text/css" href="../css/style.css" /><link rel="stylesheet" type="text/css" href="../css/main.css"  /><link href="../css/RoundCorner.css" type="text/css" rel="Stylesheet" /><script language="javascript" type ="text/javascript" src="../script/main.js" ></script><script language="javascript" src="../script/NumberFormat.js" type="text/javascript"></script><script type="text/javascript" language="javascript">
    function changenumberformat(control) {
        var conval = document.getElementById(control).value;
        if (conval.lastIndexOf(",") != conval.length - 1) {
            document.getElementById(control).value = FormatNumberBy3(conval.replace(/[.]/g, ""), ",", ".");
        }
        else {
            document.getElementById(control).value = conval;
        }
    }

    function getSatuan() {
        returnValue = ShowDialog2('Satuan', 'Arg', '610', '450');
        if (returnValue) {
            var comp = new Array();
            comp = returnValue.split(";");
            var Name = document.getElementById("Txtlain2");
            Name.value = comp[1];
            var ID = document.getElementById("hfOID")
            ID.value = comp[0]
        }
    }

//    function itungkubikasi() {
//        var kubikTotal = prompt("Masukkan total ukuran ?", "0")
//        if (kubikTotal) {
//            var quantity = prompt("Masukkan jumlah barang ?", "0")
//            if (quantity) {
//                document.getElementById("TxtPanjang").value = kubikTotal;
//                document.getElementById("TxtLebar").value = "1";
//                document.getElementById("TxtTinggi").value = "1";
//                document.getElementById("TxtQuantity").value = "1";
//                document.getElementById("Txtketerangan").value = quantity + " Colli";
//            } else {
//            alert("Harap input jumlah barang ")
//        }
//               
//        } else {
//            alert("Harap input total ukurannya " )
//        }
//       
//    }

    function getitems() {
        returnValue = Showdialogwarehouse('Quotation','Arg', '610', '450');
        if (returnValue) {
            var comp = new Array();
            comp = returnValue.split(";");
            var quotation_No = document.getElementById("hfquotationno");
            quotation_No.value = comp[0];
            var idcustomer = document.getElementById("hfidcustomer");
            idcustomer.value = comp[1];
            var pengirim = document.getElementById("TxtNamaCustomer");
            pengirim.value = comp[6];
            var nm = document.getElementById("hfnama");
            nm.value = comp[6];
        }
    }

    function getmasterItem() {
        returnValue = ShowDialog7('MasterItem&idcust=' + document.getElementById("hfidcustomer").value, 'Arg', '1000', '500');
        if (returnValue) {
            var comp = new Array();
            comp = returnValue.split(";");
            var namabarang = document.getElementById("TxtNamaBarang");
            namabarang.value = comp[1];
            var berat = document.getElementById("TxtBerat");
            berat.value = comp[3];
            var panjang = document.getElementById("TxtPanjang");
            panjang.value = comp[4];
            var lebar = document.getElementById("TxtLebar");
            lebar.value = comp[6];
            var tinggi = document.getElementById("TxtTinggi");
            tinggi.value = comp[5];
        }
    }

    function getContainer() {
        returnValue = Showdialogcontainer('Container&QDID=' + document.getElementById("hfQDID").value , 'Arg', '1000', '1000');
        if (returnValue) {
            var comp = new Array
            comp = returnValue.split(";");
            var ContainerID = document.getElementById("hfContID");
            ContainerID.value = comp[0];
            var Container = document.getElementById("TxtContainer");
            Container.value = comp[1];
//            var qdid = document.getElementById("hfQDID");
//            qdid.value = comp[2];

            if (document.getElementById("hfCont").value == "Kubikasi" || document.getElementById("hfCont").value == "kubikasi") {
                document.getElementById("TxtPanjang").value = comp[2] * 1000000;
                document.getElementById("TxtLebar").value = 1
                document.getElementById("TxtTinggi").value = 1        
            }
            
        }
    }
    
    function getitemsQuo() {
        returnValue = Showdialogwarehouseitem('Item', document.getElementById("hfquotationno").value ,'Arg', '610', '450');
        if (returnValue) {
            var comp = new Array();
            comp = returnValue.split(";");
            var nama_barang = document.getElementById("TxtJenisBarang");
            nama_barang.value = comp[1];
            var nama_barang2 = document.getElementById("hfjenisbarang");
            nama_barang2.value = comp[1];
            var satuan = document.getElementById("hfSatuan");
            satuan.value = comp[3];
            var qdid = document.getElementById("hfQDID");
            qdid.value = comp[0];
            if (satuan.value == "Container" || satuan.value == "Kontainer") {
                alert("Silahkan pilih container ");
//                document.getElementById("nama_barang_label").innerHTML = "Ukuran Container";
                document.getElementById("TxtNamaBarang").value = comp[1]; ;
                document.getElementById("TxtMerk").style.visibility = "hidden";
                document.getElementById("TxtBerat").style.visibility = "hidden";
                document.getElementById("TxtPanjang").style.visibility = "hidden";
                document.getElementById("TxtLebar").style.visibility = "hidden";
                document.getElementById("TxtTinggi").style.visibility = "hidden";
                document.getElementById("Txtlain2").style.visibility = "hidden";
                document.getElementById("lblkg").style.visibility = "hidden";
                document.getElementById("lblm").style.visibility = "hidden";
                document.getElementById("Label1").style.visibility = "hidden";
                document.getElementById("Label2").style.visibility = "hidden";
                document.getElementById("TxtQuantity").style.visibility = "visible";
                document.getElementById("TxtQuantity").value = "1";
                document.getElementById("Txtketerangan").style.visibility = "visible";
                document.getElementById("hfCont").value = "true";
            }else if (satuan.value == "Kubikasi" || satuan.value == "kubikasi") {
                alert("Silahkan pilih kumpulan kubikasi ");
                document.getElementById("TxtNamaBarang").value = comp[1]; ;
                document.getElementById("TxtMerk").style.visibility = "hidden";
                document.getElementById("TxtBerat").style.visibility = "hidden";
                document.getElementById("TxtPanjang").style.visibility = "visible";
                document.getElementById("TxtLebar").style.visibility = "visible";
                document.getElementById("TxtTinggi").style.visibility = "visible";
                document.getElementById("Txtlain2").style.visibility = "hidden";
                document.getElementById("lblkg").style.visibility = "hidden";
                document.getElementById("lblm").style.visibility = "visible";
                document.getElementById("Label1").style.visibility = "visible";
                document.getElementById("Label2").style.visibility = "visible";
                document.getElementById("TxtQuantity").style.visibility = "visible";
                document.getElementById("TxtQuantity").value = "1";
                document.getElementById("Txtketerangan").style.visibility = "visible";
                document.getElementById("hfCont").value = "kubikasi";
            }
            else {
//                document.getElementById("nama_barang_label").innerHTML = "Nama Barang";
//                document.getElementById("TxtNamaBarang").value = "";
                document.getElementById("TxtMerk").style.visibility = "visible";
                document.getElementById("TxtBerat").style.visibility = "visible";
                document.getElementById("TxtPanjang").style.visibility = "visible";
                document.getElementById("TxtLebar").style.visibility = "visible";
                document.getElementById("TxtTinggi").style.visibility = "visible";
                document.getElementById("Txtlain2").style.visibility = "visible";
                document.getElementById("lblkg").style.visibility = "visible";
                document.getElementById("lblm").style.visibility = "visible";
                document.getElementById("Label1").style.visibility = "visible";
                document.getElementById("Label2").style.visibility = "visible";
                document.getElementById("TxtQuantity").style.visibility = "visible";
                document.getElementById("Txtketerangan").style.visibility = "visible";
                document.getElementById("hfCont").value = "false";               
            }
        }
    }
    
    </script><style type="text/css">
        .style3
        {
            text-align: left;
            padding-left: 20px;
            width: 20%;
        }
        .style4
        {
            width: 25%;
        }
        .style5
        {
            text-align: left;
            width: 10%;
        }
        .style6
        {
            width: 10%;
        }
        .style7
        {
            width: 1%;
        }
        .style12
        {
            width: 1%;
        }
    </style></head><body class= "mainmenu"><form id="form1" runat="server">
        <div class = "Divutama" >
            
			<div class="formtitle"><b>Input Gudang</b></div>
			<br />
			
			
			<div class = "div_input">
			<div class = "div_umum">
				<table class = "borderdot">
					<tr>
					    <td class = "style4">
					        <table>
		                       
		                         <tr>
		                            <td class="style5" align="center"> Kode Item Gudang</td>
                                    <td class="style12">:</td>
                                    <td class="style3">
                                    <asp:Label ID="lblKode" runat ="server"></asp:Label>
                                    </td>
		                        </tr>
		                        <tr>
		                            <td class="style5" align="center">Nama Penerima</td>
                                    <td class="style12">:</td>
                                    <td class="style3">
                                        <asp:TextBox ID="TxtNamaCustomer" runat="server" Width="150" MaxLength="50" ReadOnly="true"></asp:TextBox>
                                        <img alt="Browse" onclick="javascript:getitems();" src="../images/search.png" /></td>
		                        </tr>
		                        
		                        <tr>
			                        <td class="style5" align="center">Nama Gudang</td>
                                    <td class="style12">:</td>
			                        <td class="style3"> <asp:DropDownList 
                                            ID="DdlNamaGudang" runat="server" Width="157">
                                        </asp:DropDownList>&nbsp;</td>
            						                        
		                        </tr>
		                        <tr>
			                        <td class="style5" align="center">Tanggal Datang</td>
                                    <td class="style12">:</td>
			                        <td class="style3"> <dxe:ASPxDateEdit ID="tb_tgl" runat="server" EditFormat="Custom" 
                                            EditFormatString="dd MMMM yyyy" Width="157">
                                        </dxe:ASPxDateEdit>
                                    </td>
		                        </tr>
					             <tr>    
		                            <td class="style5" align="center"> Tally Sheet</td>
                                    <td class="style12" >:</td>
                                    <td class="style3">
                                        <asp:TextBox ID="TxtTally" runat="server" Width="150" ></asp:TextBox>
                                    </td>
		                        </tr>
		                        <tr>
		                            <td class="style5" align="center"> NO Surat Jalan</td>
                                    <td class="style12">:</td>
                                    <td class="style3">
                                        <asp:TextBox ID="txtNoSuratJalan" runat="server" Width="150" MaxLength="50"></asp:TextBox>
                                    </td>
		                        </tr>
					            <tr>
					                <td>
					                    &nbsp;
					                </td>
					            </tr>
		                        <tr>
					                <td>
					                    &nbsp;
					                </td>
					            </tr>
						
		    		            <tr>
					                <td>
					                    &nbsp;
					                </td>
					            </tr>
		                        <tr>
					                <td>
					                    &nbsp;
					                </td>
					            </tr>
						        
					            <tr>
					                <td>
					                    &nbsp;
					                </td>
					            </tr>
					            
					            				            
					        </table>
					    </td>
					    <td class="style7">
					        &nbsp;
					    </td>
					    <td>
					        <table>
					            <tr>
						            <td class="style5" align="center">Jenis Tarif</td>
                                    <td class="style12">:</td>
						            <td class="style3"><asp:TextBox ID="TxtJenisBarang" runat="server" Width="150" 
                                            MaxLength="50" Enabled = "false" ReadOnly="True"></asp:TextBox>
                                      <img alt="Browse" onclick="javascript:getitemsQuo();" src="../images/search.png" />
                                   <%--<asp:ImageButton ID="ImageButton1" runat="server" src="../images/search.png"  />--%>
                                       <%-- <asp:Image ID="Image1" runat="server" onclick="javascript:getitemsQuo();" 
                                            src="../images/search.png" style="width: 16px"/>--%>
                                        </td>
					            </tr>
					            <tr>
						            <td class="style5" align="center"><asp:Label ID = "nama_barang_label" runat = "server" Text = "Nama Barang" ></asp:Label></td>
                                    <td class="style12">:</td>
						            <td class="style3"><asp:TextBox ID="TxtNamaBarang" runat="server" Width="150" MaxLength="50"></asp:TextBox>
                                        <img alt="Browse" onclick="javascript:getmasterItem();" src="../images/search.png" /></td>
					            </tr>
					            <tr>
		                            <td class="style5" align="center">Nama Pengirim Barang</td>
                                    <td class="style12">:</td>
                                    <td class="style3">
                                        <asp:TextBox ID="TxtPengirimBarang" runat="server" Width="150" MaxLength="50"></asp:TextBox>
                                        </td>
		                        </tr>
            	
					            <tr>
					                <td class="style5" align="center"> Merk</td>
                                    <td class="style12">:</td>
                                    <td class="style3">
                                        <asp:TextBox ID="TxtMerk" runat="server" Width="150" MaxLength="50"></asp:TextBox>
                                    </td>
					            </tr>
            					
					            <tr>
					                <td class="style5" align="center"> Berat</td>
                                    <td class="style12">:</td>
                                    <td class="style3">
                                        <asp:TextBox ID="TxtBerat" runat="server" Width="150" MaxLength="50"></asp:TextBox> &nbsp;<asp:Label ID="lblkg"
                                runat="server" Text="Kg"></asp:Label>
                                    </td>
					            </tr>
					            <tr>
					                <td class="style5" align="center"> Panjang</td>
                                    <td class="style12">:</td>
                                    <td class="style3">
                                        <asp:TextBox ID="TxtPanjang" runat="server" Width="150" MaxLength="50"></asp:TextBox>
                                        <asp:Label ID="lblm" runat="server" Text="centimeter"></asp:Label>
                                    </td>
					            </tr>
					            <tr>
					                <td class="style5" align="center"> Lebar</td>
                                    <td class="style12">:</td>
                                    <td class="style3">
                                        <asp:TextBox ID="TxtLebar" runat="server" Width="150" MaxLength="50"></asp:TextBox>
                                        <asp:Label ID="Label1" runat="server" Text="centimeter"></asp:Label>
                                    </td>
					            </tr>
					            <tr>
					                <td class="style5" align="center"> Tinggi</td>
                                    <td class="style12">:</td>
                                    <td class="style3">
                                        <asp:TextBox ID="TxtTinggi" runat="server" Width="150" MaxLength="50"></asp:TextBox>
                                        <asp:Label ID="Label2" runat="server" Text="centimeter"></asp:Label>
                                    </td>
					            </tr>
					            <tr>
					                <td class="style5" align="center">Quantity</td>
                                    <td class="style12">:</td>
					                <td class="style3"><asp:TextBox ID="TxtQuantity" runat="server" Width="150" MaxLength="50"></asp:TextBox></td>    
					            </tr>
					            <tr>
					                <td class="style5" align="center">Packing </td>
                                    <td class="style12">:</td>
					                <td class="style3"><asp:TextBox ID="Txtlain2" runat="server" Width="150" MaxLength="50"></asp:TextBox>
					                <img alt="Browse" onclick="javascript:getSatuan();" src="../images/search.png" /></td>    
					            </tr>
					            
					            <tr>
					                <td class="style5" align="center">Keterangan</td>
                                    <td class="style12">:</td>
					                <td class="style3"><asp:TextBox ID="Txtketerangan" runat="server" Width="150" MaxLength="50"></asp:TextBox></td>    
					            </tr>
					            
					        </table>
					    </td>
					</tr>
					<tr>
					    <td colspan = "2">
					        <div id="error">
				                &nbsp;</div>
					    </td>
					</tr>
					
					<tr>
					    <td>
					        <table style="width: 595px">
					            <tr>
					                <td align="right" class="style6">
						                
					                </td>
					                <td align="left" class="style4">
			        	        
			                        </td>        
					            </tr>
					        </table>
					    </td>
					    <td>
					        &nbsp;
					    </td>
					    <td align = "center">
					        <table>
					            <tr>
					                <td>
					                    <dxe:ASPxButton ID="btAdd" runat="server" Text="Tambah" Width="90px">
				                <Image Url="../images/add.png" />
			                </dxe:ASPxButton>
					                </td>
					                <td>
					                    <dxe:ASPxButton ID="btBatal" runat="server" Text="Reset" Width="90px">
							                <Image Url="../images/undo.png" />
						                </dxe:ASPxButton>
					                </td>
					                <td>
					                <%--<dxe:ASPxButton ID="btKubikGlobal" runat="server" Text="KubikasiGlobal" Width="90px">
							                <Image Url="../images/undo.png" />
						                </dxe:ASPxButton>--%>
						                </td>
					            </tr>
					        </table>
			        	                
		                    
					    </td>
					</tr>
				</table>
				<br />
				<div align = "center" style="width:100%">
			        <asp:Label ID="lInfo" runat="server" CssClass="error" Visible="False"></asp:Label>
                    <asp:Label ID="linfoberhasil" runat="server" CssClass = "berhasil" Visible="False"></asp:Label>
			    </div>
				<asp:HiddenField ID="hfWID" runat="server" />
				<asp:HiddenField ID="hfID" runat="server" />
				<asp:HiddenField ID="hfModeItem" runat="server" />
				<asp:HiddenField ID="hfMode" runat="server" />
				<asp:HiddenField ID="hfDel" runat="server" />
				<asp:HiddenField ID="hfSatuan" runat="server" />
				<asp:HiddenField ID="hfContID" runat="server" />
				<asp:HiddenField ID="hfOID" runat="server" />
				<asp:HiddenField ID="hfCont" runat="server" />
				<asp:HiddenField ID="hfQDID" runat="server" />
				<asp:HiddenField ID="hfIndex" runat="server" />
				<asp:HiddenField ID="hfquotationno" runat="server" />
				<asp:HiddenField ID="hfidcustomer" runat="server" />
				<asp:HiddenField ID="hfnama" runat="server" />
				<asp:HiddenField ID="hfjenisbarang" runat="server" />
				
			
            <br />
            <div align = "center" >
                <dxwgv:ASPxGridView ID="Grid_Item" runat="server" 
                    AutoGenerateColumns="true" Font-Size="9pt" KeyFieldName="IDW" 
                    Width="100%" ClientInstanceName = "grid_item">
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
			        <Settings ShowFilterRow="True"  />
                    <SettingsBehavior AllowFocusedRow="True" />
			        <Columns>
                        <dxwgv:GridViewDataColumn Caption="ID" FieldName="IDW" Name="IDW" 
                            Visible="false" VisibleIndex="1">
                        </dxwgv:GridViewDataColumn>
                        <dxwgv:GridViewDataColumn FieldName="WarehouseItem_CodeW" Name="WarehouseItem_CodeW" 
                            Visible="false" VisibleIndex= "2" Caption="WarehouseItem_CodeW" >
						</dxwgv:GridViewDataColumn>
                        <dxwgv:GridViewDataColumn FieldName="Warehouseheader_IDW" Name="Warehouseheader_IDW" 
                            Visible="false" VisibleIndex= "2" Caption="Warehouseheader_IDW" >
						</dxwgv:GridViewDataColumn>
						<dxwgv:GridViewDataColumn FieldName="QDIDW" Name="QDIDW" 
                            Visible="false" VisibleIndex= "3" Caption="QDID" >
						</dxwgv:GridViewDataColumn>
                        <dxwgv:GridViewDataColumn FieldName="ContainerW" Name="ContainerW" 
                            Visible="true" VisibleIndex= "4" Caption="Container" >
						</dxwgv:GridViewDataColumn>
						<dxwgv:GridViewDataColumn FieldName="IDContainerW" Name="IDContainerW" 
                            Visible="false" VisibleIndex= "5" Caption="Container" >
						</dxwgv:GridViewDataColumn>
                        <dxwgv:GridViewDataColumn FieldName="Nama_BarangW" Name="Nama_BarangW" 
                                        VisibleIndex="6" Caption="Nama Barang" >
						</dxwgv:GridViewDataColumn>
						<dxwgv:GridViewDataColumn FieldName="MerkW" Name="MerkW" VisibleIndex="7" Caption="Merk" >
						</dxwgv:GridViewDataColumn>
				        <dxwgv:GridViewDatatextColumn FieldName="BeratW" Name="BeratW" VisibleIndex="8" Caption="Berat(kg)" >
						</dxwgv:GridViewDatatextColumn>
						<dxwgv:GridViewDataColumn FieldName="PanjangW" Name="PanjangW" VisibleIndex="9" Caption="Panjang(cm)" >
						</dxwgv:GridViewDataColumn>
						<dxwgv:GridViewDataColumn FieldName="LebarW" Name="LebarW" VisibleIndex="10" Caption="Lebar(cm)" >
						</dxwgv:GridViewDataColumn>
						<dxwgv:GridViewDataColumn FieldName="TinggiW" Name="TinggiW" VisibleIndex="11" Caption="Tinggi(cm)" >
						</dxwgv:GridViewDataColumn>
						<dxwgv:GridViewDataColumn FieldName="QuantityW" Name="QuantityW" VisibleIndex="12" Caption="Quantity " >
						</dxwgv:GridViewDataColumn>
						<dxwgv:GridViewDataColumn FieldName="QuantityMskW" Name="QuantityMskW" VisibleIndex="12" Caption="Quantity Msk" >
						</dxwgv:GridViewDataColumn>
						<dxwgv:GridViewDataColumn FieldName="OtherIDW" Name="OtherIDW" VisibleIndex="13" Caption= " OtherIDs " Visible="false" >
						</dxwgv:GridViewDataColumn>
						<dxwgv:GridViewDataColumn FieldName="OtherW" Name="OtherW" VisibleIndex="14" Caption= "Ukuran" >
						</dxwgv:GridViewDataColumn>
						<dxwgv:GridViewDataColumn FieldName="KeteranganW" Name="KeteranganW" VisibleIndex="15" Caption= " Keterangan ">
						</dxwgv:GridViewDataColumn>
						<dxwgv:GridViewDataColumn FieldName="PengirimW" Name="PengirimW" VisibleIndex="16" Caption= " Pengirim ">
						</dxwgv:GridViewDataColumn>
						<dxwgv:GridViewDataColumn FieldName="TallySheet_NoW" Name="TallySheet_NoW" VisibleIndex="16" Caption= " No Tally ">
						</dxwgv:GridViewDataColumn>
						<dxwgv:GridViewDataColumn FieldName="No_Surat_JalanW" Name="No_Surat_JalanW" VisibleIndex="16" Caption= "No Surat Jln">
						</dxwgv:GridViewDataColumn>
						<dxwgv:GridViewDataDateColumn FieldName="Time_ItemcomeW" Name="Time_ItemcomeW" VisibleIndex="16" Caption= "Tgl Dtg">
						    <PropertiesdateEdit DisplayFormatString="dd MMMM yyyy"></PropertiesDateEdit>
						</dxwgv:GridViewDataDateColumn>
                        <dxwgv:GridViewDataColumn Caption="#" Name="Delete" VisibleIndex="17" Width="1%">
                            <dataitemtemplate>
                                <asp:LinkButton ID="tbDelete" runat="server" CommandName="Delete" 
                                    ToolTip="Delete Item" OnClientClick="return confirm('Are You Sure Want to Delete ?');" >Delete</asp:LinkButton>
                            </dataitemtemplate>
                        </dxwgv:GridViewDataColumn>
                        <dxwgv:GridViewDataColumn Caption="#" Name="Edit" VisibleIndex="18" Width="1%">
                            <dataitemtemplate>
                                <asp:LinkButton ID="tbEdit" runat="server" CommandName="Edit" 
                                    ToolTip="Edit Item">Edit</asp:LinkButton>
                            </dataitemtemplate>
                        </dxwgv:GridViewDataColumn>
                    </Columns>
                </dxwgv:ASPxGridView> 
                </div>
            <br />
            <table width= "100%">
                <tr>
                    <td align ="center">
                        <dxe:ASPxButton ID="btSimpan" runat="server" Text="Simpan" Width="90px">
							                <Image Url="../images/save-alt.png" />
							                
						                </dxe:ASPxButton>
                        <br />
                    </td>
                </tr>
            </table>
            
            
            
			<div align = "center">
				<table width="100%" cellpadding="0" cellspacing="0">
					<tr valign="top">
						<td rowspan="2" align = "center">
							<dxwgv:ASPxGridView ID="Grid_Warehouse_Parent" ClientInstanceName="grid_parent" 
                                runat="server" KeyFieldName="ID" 
								 AutoGenerateColumns="False" Width="100%">			
								<Styles>
									<Header HoverStyle-Border-BorderColor="#515763" BackColor="#2c3848" ForeColor="#ffffff" Font-Bold="true">
                                        <HoverStyle>
                                        <Border BorderColor="#515763"></Border>
                                        </HoverStyle>
                                    </Header>
									<FocusedRow BackColor="#D3D1D4" ForeColor="#000000"></FocusedRow>
									<AlternatingRow Enabled="True" BackColor="#f4f4e9"></AlternatingRow>
									<Row BackColor="#ffffff"></Row>
									<Cell Paddings-PaddingLeft="3" Paddings-PaddingRight="3" Paddings-PaddingBottom="1" Paddings-PaddingTop="1">
                                        <Paddings PaddingLeft="3px" PaddingTop="1px" PaddingRight="3px" PaddingBottom="1px"></Paddings>
                                    </Cell>
								</Styles>
								<SettingsDetail AllowOnlyOneMasterRowExpanded="True" ShowDetailRow="True" />
								<SettingsPager PageSize="15">
                                </SettingsPager>
								<Settings ShowFilterRow="True" />
								<SettingsBehavior AllowFocusedRow="True" />
								<ClientSideEvents FocusedRowChanged="function(s, e) { OnGridFocusedRowChanged(); }" />
								<Columns>
									<dxwgv:GridViewDataColumn FieldName="ID" Name="ID" VisibleIndex="0" Visible="false">
									</dxwgv:GridViewDataColumn>
									<dxwgv:GridViewDataColumn Name="Status" Caption="Status" VisibleIndex="2" Width="1%" Visible = "false">
									    <DataItemTemplate>
									        <asp:Image ID = "ImageGrid" runat = "server" />
									    </DataItemTemplate>
									</dxwgv:GridViewDataColumn>
									<dxwgv:GridViewDataColumn FieldName="WarehouseItem_Code" Name="WarehouseItem_Code" 
                                        VisibleIndex="3" Caption="Kode Data Gudang" Visible ="True">
									</dxwgv:GridViewDataColumn>
									<dxwgv:GridViewDataColumn FieldName="Penerima" Name="Penerima" VisibleIndex="5" >
									</dxwgv:GridViewDataColumn>
                                    <dxwgv:GridViewDataColumn FieldName="Quotation_No" Name="Quotation_No" 
                                        VisibleIndex="1" Caption="Quotation Number" visible = "true">
									</dxwgv:GridViewDataColumn>
                                    
                                    
									<dxwgv:GridViewDataColumn FieldName="Warehouse_Name" Name="Warehouse_Name" 
                                        VisibleIndex="8" Caption="Tempat" >
									</dxwgv:GridViewDataColumn>
									<dxwgv:GridViewDataColumn FieldName="Warehouse_Code" Name="Warehouse_Code" VisibleIndex="9" Visible="false" >
									</dxwgv:GridViewDataColumn>
									<dxwgv:GridViewDataColumn FieldName="Kode_Customer" Name="Kode_Customer" VisibleIndex="9" Visible="false" >
									</dxwgv:GridViewDataColumn>
									<dxwgv:GridViewDataColumn FieldName="Nama_Customer" Name="Nama_Customer" Caption="Pengirim(quotation)" VisibleIndex="4" Visible="true" >
									</dxwgv:GridViewDataColumn>
									<dxwgv:GridViewDataTextColumn Caption="Penginput Data" FieldName="PenginputData" 
                                     Name="PenginputData" VisibleIndex="10"  Visible="true"></dxwgv:GridViewDataTextColumn>
									<dxwgv:GridViewDataColumn Name="Edit" Caption="#" VisibleIndex="11" Width="1%">
								    <DataItemTemplate>
								    <asp:LinkButton ID="tbedit" ToolTip="Edit Item" CommandName="Edit" runat="server">Edit</asp:LinkButton>
								    </DataItemTemplate>
								    </dxwgv:GridViewDataColumn>
								    <dxwgv:GridViewDataColumn Name="Delete" Caption="#" VisibleIndex="12" Width="1%">
								    <DataItemTemplate>
								    <asp:LinkButton ID="tbDelete" ToolTip="Delete Item" CommandName="Delete" runat="server" OnClientClick="return confirm('Are You Sure Want to Delete ?');" >Delete</asp:LinkButton>
								    </DataItemTemplate>
								    </dxwgv:GridViewDataColumn>				
								    </Columns>
								    <Templates>
								        <DetailRow>
				                            <dxwgv:ASPxGridView ID="Grid_Warehouse_Child" runat="server" 
                                                AutoGenerateColumns="true" Font-Size="9pt" KeyFieldName="ID" 
                                                Width="100%" ClientInstanceName = "grid_child" onbeforeperformdataselect = "Grid_Warehouse_Child_DataSelect" >
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
                                                    <dxwgv:GridViewDataColumn FieldName="Warehouseheader_ID" Name="Warehouseheader_ID" 
                                                        visible="false" VisibleIndex="2" Caption="Warehouseheader_ID" >
						                            </dxwgv:GridViewDataColumn>
						                            <dxwgv:GridViewDataColumn FieldName="QDID" Name="QDID" 
                                                        Visible="false" VisibleIndex= "3" Caption="QDID" >
						                            </dxwgv:GridViewDataColumn>
						                             <dxwgv:GridViewDataColumn FieldName="Container" Name="Container" 
                                                        Visible="true" VisibleIndex= "4" Caption="Container" >
						                            </dxwgv:GridViewDataColumn>
						                            <dxwgv:GridViewDataColumn FieldName="IDContainer" Name="IDContainer" 
                                                        Visible="false" VisibleIndex= "5" Caption="Container" >
						                            </dxwgv:GridViewDataColumn>
						                            <dxwgv:GridViewDataColumn FieldName="Nama_Barang" Name="Nama_Barang" 
                                                        VisibleIndex="6" Caption="Nama Barang" >
						                            </dxwgv:GridViewDataColumn>
						                            <dxwgv:GridViewDataColumn FieldName="TallySheet_No" Name="TallySheet_No" 
                                                        VisibleIndex="7" Caption="No TallySheet" visible = "true">
									                </dxwgv:GridViewDataColumn>
                                                    <dxwgv:GridViewDataColumn FieldName="No_Surat_Jalan" Name="No_Surat_Jalan" 
                                                        VisibleIndex="8" Caption="No Surat Jalan" visible = "true">
									                </dxwgv:GridViewDataColumn>
									                <dxwgv:GridViewDataDateColumn FieldName="Time_Itemcome" Name="Time_Itemcome" 
                                                        VisibleIndex="9" Caption="Tgl Datang" >
                                                        <PropertiesdateEdit DisplayFormatString="dd MMMM yyyy"></PropertiesDateEdit>
									                </dxwgv:GridViewDataDateColumn>
						                            <dxwgv:GridViewDataColumn FieldName="Merk" Name="Merk" VisibleIndex="10" Caption="Merk" >
						                            </dxwgv:GridViewDataColumn>
				                                    <dxwgv:GridViewDataColumn FieldName="Berat" Name="Berat" VisibleIndex="11" Caption="Berat(kg)" >
						                            </dxwgv:GridViewDataColumn>
						                            <dxwgv:GridViewDataColumn FieldName="Panjang" Name="Panjang" VisibleIndex="12" Caption="Panjang(cm)" >
						                            </dxwgv:GridViewDataColumn>
						                            <dxwgv:GridViewDataColumn FieldName="Lebar" Name="Lebar" VisibleIndex="13" Caption="Lebar(cm)" >
						                            </dxwgv:GridViewDataColumn>
						                            <dxwgv:GridViewDataColumn FieldName="Tinggi" Name="Tinggi" VisibleIndex="14" Caption="Tinggi(cm)" >
						                            </dxwgv:GridViewDataColumn>
						                            <dxwgv:GridViewDataTextColumn FieldName="Quantity" Name="Quantity" VisibleIndex="15" Caption="Quantity Muat" >
						                            <PropertiesTextEdit DisplayFormatString="{0:###.###.###}"></PropertiesTextEdit>
						                            </dxwgv:GridViewDataTextColumn>
						                            <dxwgv:GridViewDataTextColumn FieldName="QtyMsk" Name="QtyMsk" VisibleIndex="16" Caption="Qty Msk " >
						                            <PropertiesTextEdit DisplayFormatString="{0:###.###.###}"></PropertiesTextEdit>
						                            </dxwgv:GridViewDataTextColumn>
						                            <dxwgv:GridViewDataColumn FieldName="OtherIDs" Name="OtherIDs" VisibleIndex="17" Caption= " OtherIDs " Visible="false" >
						                            </dxwgv:GridViewDataColumn>
						                            <dxwgv:GridViewDataColumn FieldName="Others" Name="Others" VisibleIndex="18" Caption= " Satuan " >
						                            </dxwgv:GridViewDataColumn>
						                            <dxwgv:GridViewDataColumn FieldName="Keterangan" Name="Keterangan" VisibleIndex="19" Caption= " Keterangan ">
						                            </dxwgv:GridViewDataColumn>
						                            <dxwgv:GridViewDataColumn FieldName="NamaSupplier" Name="NamaSupplier" VisibleIndex="20" Caption= " Pengirim ">
						                            </dxwgv:GridViewDataColumn>
                                                </Columns>
                                            </dxwgv:ASPxGridView> 
								        </DetailRow>
								    </Templates>
							    </dxwgv:ASPxGridView>			
						    <br />
						</td>
						
					</tr>
					<tr>
						<td valign="bottom" align="right">
							&nbsp;</td>
					</tr>
				</table>
				
				</div>
			
        </div>
        </div>
        </div>
    </form>
</body>
</html>