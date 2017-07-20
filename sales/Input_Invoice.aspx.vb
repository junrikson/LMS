Imports System.Data
Imports System.Data.SqlClient
Imports DevExpress.Web.ASPxGridView

'***********************************************************************************************************************************************
' TAG               Programmer      Purpose
' EH00_20111022_01  Eddy Handaya    Add debugging info when data exists.
'***********************************************************************************************************************************************

Partial Public Class Input_Invoice
    Inherits System.Web.UI.Page
    Private DT As DataTable
    Private DS As DataSet
    Private DR As DataRow
    Private sqlstring As String
    Private result As String
    Private hasil As String

#Region "PAGE"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Session("UserID") = Nothing Then
                FormsAuthentication.SignOut()
                Response.Redirect("Index.aspx")
            End If


            If Not Page.IsPostBack Then
                Session("Grid_Header_Invoice") = Nothing
                HFMode.Value = "Insert"
                HFModeItem.Value = "Insert"
                HFmuatBarangID.Value = ""
                TxtAsuransi.Visible = True
                PanelHitung.Visible = False
                TxtIndikator.Enabled = True
                Create_Session()
                'DtInvoice.Date = Today
                load_grid_invoice_header()
                TxtHargaAsuransi.Attributes.Add("onkeyup", "changenumberformat('" & TxtHargaAsuransi.ClientID & "')")
                TxtPolis.Attributes.Add("onkeyup", "changenumberformat('" & TxtPolis.ClientID & "')")
                TxtJumlahOngkosAngkutan.Attributes.Add("onkeyup", "changenumberformat('" & TxtJumlahOngkosAngkutan.ClientID & "')")
            End If

            If Not Session("GridPilihInvoice") Is Nothing Then
                Grid_Pilih_invoice.DataSource = CType(Session("GridPilihInvoice"), DataTable)
                Grid_Pilih_invoice.DataBind()
            End If

            If Not Session("Grid_Header_Invoice") Is Nothing Then
                Grid_Invoice_Parent.DataSource = CType(Session("Grid_Header_Invoice"), DataTable)
                Grid_Invoice_Parent.DataBind()
            End If

        Catch ex As Exception
            Response.Write("<b>Error Page Load :</b>" & ex.ToString)
        End Try
    End Sub

    Private Function Load_Invoice_No(ByVal flag As String) As String
        Try
            Dim pisah() As String
            Dim bulan As String
            Dim tahun As String
            Dim no As Integer
            Dim value As String = ""
            Dim aliaskapal As String
            Dim nodpn As String = ""
            Dim TglKapalBErangkat As String

            sqlstring = "select Distinct MBR.Depart_Date FROM MuatBarangReport MBR " & _
                        "JOIN MBRDetail MBRD ON MBR.Mbr_No = MBRD.Mbr_No " & _
                        "WHERE MBRD.Mb_No = '" & HFMBNO.Value & "' "
            TglKapalBErangkat = SQLExecuteScalar(sqlstring)
            HFTglBerangkat.Value = TglKapalBErangkat
            bulan = CDate(TglKapalBErangkat).ToString("MM")
            tahun = CDate(TglKapalBErangkat).ToString("yy")

            sqlstring = "SELECT Alias_Kapal From Kapal WHERE IDDetail = " & HFIDKapal.Value & " and status = 1 "
            aliaskapal = SQLExecuteScalar(sqlstring)

            If flag = "0" Then
                sqlstring = "select TOP 1 A.NoInvoice FROM " & _
                            "( " & _
                            "( SELECT TOP 1 LEFT(No_Invoice,25) as NoInvoice, timestamp  " & _
                            "FROM InvoiceHeader " & _
                            "WHERE No_Invoice LIKE '0%'   " & _
                            "AND ([status] = 1 or [status]=7) AND StatusNumber = 1   " & _
                            "and BlnInvoice = '" & CekBulan(bulan) & "/" & tahun & "' ORDER BY [timestamp] DESC)  " & _
                            "UNION ALL " & _
                            "(SELECT TOP 1 LEFT(No_Asuransi,25) as NoInvoice, timestamp  " & _
                            "FROM Asuransi " & _
                            "WHERE No_Asuransi LIKE '0%'   " & _
                            "AND ([status] = 1 or [status]=7) AND StatusNumber = 1   " & _
                            "and BlnInvoice = '" & CekBulan(bulan) & "/" & tahun & "'   " & _
                            "ORDER BY [timestamp] DESC )) as A  ORDER BY a.timestamp desc"
                hasil = SQLExecuteScalar(sqlstring)

                If hasil <> "" Then
                    pisah = hasil.ToString.Split("/")
                    If pisah(3) <> CekBulan(bulan) Then
                        no = 1
                    Else
                        no = CDbl(pisah(0)) + 1
                    End If
                Else

                    no = 1
                End If


                value = no.ToString("0000") & "/" & Singkatan & "/" & aliaskapal & "/" & CekBulan(bulan) & "/" & tahun

                If chkAsuransi.Checked = True Then
                    nodpn = no.ToString("0000")

                    TxtAsuransi.Text = "A" & nodpn.Substring(1, nodpn.Length - 1) & "/" & Singkatan & "/" & aliaskapal & "/" & CekBulan(bulan) & "/" & tahun

                End If

            ElseIf flag = "B" Or flag = "b" Then
                sqlstring = "select TOP 1 A.NoInvoice FROM " & _
                            "( " & _
                            "( SELECT TOP 1 LEFT(No_Invoice,25) as NoInvoice, timestamp  " & _
                            "FROM InvoiceHeader " & _
                            "WHERE No_Invoice LIKE 'B%'   " & _
                            "AND ([status] = 1 or [status]=7) AND StatusNumber = 1   " & _
                            "and BlnInvoice = '" & CekBulan(bulan) & "/" & tahun & "' ORDER BY [timestamp] DESC)  " & _
                            "UNION ALL " & _
                            "(SELECT TOP 1 LEFT(No_Asuransi,25) as NoInvoice, timestamp  " & _
                            "FROM Asuransi " & _
                            "WHERE No_Asuransi LIKE 'B%'   " & _
                            "AND ([status] = 1 or [status]=7) AND StatusNumber = 1   " & _
                            "and BlnInvoice = '" & CekBulan(bulan) & "/" & tahun & "'   " & _
                            "ORDER BY [timestamp] DESC )) as A  ORDER BY a.timestamp desc"
                hasil = SQLExecuteScalar(sqlstring)

                If hasil <> "" Then
                    pisah = hasil.ToString.Split("/")


                    If pisah(3) <> CekBulan(bulan) Then
                        no = 1
                    Else
                        no = CDbl(pisah(0).Replace("B", "0").Replace("b", "0")) + 1
                    End If
                Else
                    no = 1
                End If



                nodpn = no.ToString("0000")

                value = "B" & nodpn.Substring(1, nodpn.Length - 1) & "/" & Singkatan & "/" & aliaskapal & "/" & CekBulan(bulan) & "/" & tahun

                If chkAsuransi.Checked = True Then
                    nodpn = no.ToString("0000")

                    TxtAsuransi.Text = "A" & nodpn.Substring(1, nodpn.Length - 1) & "/" & Singkatan & "/" & aliaskapal & "/" & CekBulan(bulan) & "/" & tahun

                End If
            Else
                sqlstring = "select TOP 1 A.NoInvoice FROM " & _
                            "( " & _
                            "( SELECT TOP 1 LEFT(No_Invoice,25) as NoInvoice, timestamp  " & _
                            "FROM InvoiceHeader " & _
                            "WHERE No_Invoice LIKE '0%'   " & _
                            "AND ([status] = 1 or [status]=7) AND StatusNumber = 1   " & _
                            "and BlnInvoice = '" & CekBulan(bulan) & "/" & tahun & "' ORDER BY [timestamp] DESC)  " & _
                            "UNION ALL " & _
                            "(SELECT TOP 1 LEFT(No_Asuransi,25) as NoInvoice, timestamp  " & _
                            "FROM Asuransi " & _
                            "WHERE No_Asuransi LIKE '0%'   " & _
                            "AND ([status] = 1 or [status]=7) AND StatusNumber = 1   " & _
                            "and BlnInvoice = '" & CekBulan(bulan) & "/" & tahun & "'   " & _
                            "ORDER BY [timestamp] DESC )) as A  ORDER BY a.timestamp desc"
                hasil = SQLExecuteScalar(sqlstring)
                If hasil <> "" Then
                    pisah = hasil.ToString.Split("/")
                    If pisah(3) <> CekBulan(bulan) Then
                        no = 1
                    Else
                        no = CDbl(pisah(0)) + 1
                    End If
                Else

                    no = 1
                End If


                value = no.ToString("0000") & "/" & Singkatan & "/" & aliaskapal & "/" & CekBulan(bulan) & "/" & tahun
                If chkAsuransi.Checked = True Then
                    nodpn = no.ToString("0000")

                    TxtAsuransi.Text = "A" & nodpn.Substring(1, nodpn.Length - 1) & "/" & Singkatan & "/" & aliaskapal & "/" & CekBulan(bulan) & "/" & tahun

                End If
            End If


            Return value
        Catch ex As Exception
            Throw New Exception("<b>Error load invoice : </b>" & ex.ToString)
        End Try
    End Function

    Private Sub Load_N0_Asuransi()
        Try
            Dim aliaskapal As String
            Dim nodpn As String = ""
            Dim pisah(5) As String
            Dim no As Integer
            Dim bulan As String = Date.Today.ToString("MM")
            Dim value As String = ""

            sqlstring = "SELECT Alias_Kapal From Kapal WHERE IDDetail = " & HFIDKapal.Value & " AND [status] = 1"
            aliaskapal = SQLExecuteScalar(sqlstring)

            sqlstring = "SELECT TOP 1 NoAsuransi FROM InvoiceHeader " & _
                        "WHERE ([status] = 1 or [status] = 7) AND NoAsuransi <> '' " & _
                        "ORDER BY ID DESC"
            hasil = SQLExecuteScalar(sqlstring)

            If hasil = "" Then
                no = 1
                value = "A" & no & "/" & aliaskapal & "/" & CekBulan(bulan) & "/" & Date.Today.ToString("yy")

            Else
                pisah = hasil.ToString.Split("/")
                no = CDbl(pisah(0).Replace("A", "0").Replace("a", "0")) + 1

                value = "A" & no & "/" & aliaskapal & "/" & CekBulan(bulan) & "/" & Date.Today.ToString("yy")


            End If

            TxtAsuransi.Text = ""
            TxtAsuransi.Text = value

        Catch ex As Exception
            Throw New Exception("<b>Error Load No Asuransi :</b>" & ex.ToString)
        End Try
    End Sub

    Private Function Get_Kota_Asal_Barang(ByVal MBNO As String) As String
        Try
            Dim strkotabrg As String
            Dim kotaasalbarang As String

            strkotabrg = "select MC.KotaDitunjukan FROM MuatBarang MB " & _
                            "JOIN WarehouseHeader WH on MB.WarehouseHeaderID = WH.WarehouseItem_Code " & _
                            "JOIN MasterQuotation MQ ON WH.Quotation_No = MQ.Quotation_No " & _
                            "JOIN MasterCustomer MC ON MQ.Customer_Id = MC.Kode_Customer " & _
                            "WHERE MB.Mb_No = '" & MBNO.ToString & "' " & _
                            "AND MC.status <> 0"
            kotaasalbarang = SQLExecuteScalar(strkotabrg)

            If kotaasalbarang = "" Then
                kotaasalbarang = "JAKARTA"
            End If

            Return kotaasalbarang
        Catch ex As Exception
            Throw New Exception(ex.ToString)
        End Try
    End Function

#End Region

#Region "Button"

    Protected Sub btAdd_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btAdd.Click
        Try
            Dim KtaAsalAsalBrg As String = ""

            If HFNamakapal.Value <> "" And HFNamaPengirim.Value <> "" And HFTujuan.Value <> "" Then
                load_DDL()
                KtaAsalAsalBrg = Get_Kota_Asal_Barang(HFMBNO.Value)
                'TxtNoInvoice.Text = Load_Invoice_No("0")
                Show_Text_Box()
                clear_Label()

                Load_Pilih_Grid()
            End If


        Catch ex As Exception
            Throw New Exception("<b>Error BtnAdd :</b>" & ex.ToString)
        End Try
    End Sub

    Private Sub btBatal_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btBatal.Click
        Try
            clear_all()
            clear_detail()
            clear_Label()
            DtInvoice.Date = Today
        Catch ex As Exception
            Response.Write("<b>Error btnbatal :</b>" & ex.ToString)
        End Try
    End Sub

    Private Sub btSimpan_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btSimpan.Click
        Try
            clear_Label()

            If HFMode.Value = "Insert" Then
                If validation_addnew() Then
                    Insert()
                    Grid_Pilih_invoice.DataSource = Nothing
                    Grid_Pilih_invoice.DataBind()
                    load_grid_invoice_header()


                    chkAsuransi.Checked = False
                    LblBayar.Text = ""
                    clear_asuransi()
                End If
                linfoberhasil.Visible = True
            Else
                If validation_update() Then
                    UPDATE(HFNoInvoice.Value)
                    Grid_Pilih_invoice.DataSource = Nothing
                    Grid_Pilih_invoice.DataBind()
                    load_grid_invoice_header()


                    chkAsuransi.Checked = False
                    clear_asuransi()

                End If
            End If
        Catch ex As Exception
            Response.Write("<b>Error BtnSimpan</b>" & ex.ToString)
        End Try
    End Sub

    Protected Sub TxtNoInvoice_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtNoInvoice.TextChanged
        Try
            If HFMode.Value = "Insert" Then
                TxtNoInvoice.Text = Load_Invoice_No(Left(TxtNoInvoice.Text, 1))
                HFNoInvoice.Value = TxtNoInvoice.Text
            End If

        Catch ex As Exception
            Response.Write("<b>Error txtchange :</b>" & ex.ToString)
        End Try
    End Sub

    Private Sub chkAsuransi_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkAsuransi.CheckedChanged

        If HFIDKapal.Value <> "" Then
            If chkAsuransi.Checked = True Then
                If HFMode.Value = "Insert" Then
                    PanelAsuransi.Visible = True
                    Load_N0_Asuransi()
                Else
                    If TxtAsuransi.Text = "" Then
                        PanelAsuransi.Visible = True
                        Load_N0_Asuransi()
                    Else
                        Exit Sub
                    End If

                End If

            Else
                If HFMode.Value = "Insert" Then
                    PanelAsuransi.Visible = False
                    TxtAsuransi.Text = ""
                    TxtPolis.Text = ""
                    TxtHargaAsuransi.Text = ""
                    TxtPremi.Text = ""
                Else
                    sqlstring = "SELECT ID FROM InvoiceHeader where NoAsuransi = '" & TxtAsuransi.Text & "'"
                    hasil = SQLExecuteScalar(sqlstring)

                    If hasil <> "" Then
                        PanelAsuransi.Visible = False
                        TxtAsuransi.Text = ""
                        TxtPolis.Text = ""
                        TxtHargaAsuransi.Text = ""
                        TxtPremi.Text = ""
                    Else
                        Exit Sub
                    End If

                End If

            End If
        End If

    End Sub
#End Region

#Region "Method"

    Private Sub Create_Session()
        Try
            Dim adt As New DataTable

            With adt.Columns
                .Add(New DataColumn("MuatBarangDetailID", GetType(String)))
                .Add(New DataColumn("Mb_NoI", GetType(String)))
                .Add(New DataColumn("Nama_Barang", GetType(String)))
                .Add(New DataColumn("Satuan", GetType(String)))
                .Add(New DataColumn("NamaPaket", GetType(String)))
                .Add(New DataColumn("Harga_Satuan", GetType(String)))
                .Add(New DataColumn("Tanggal", GetType(String)))
                .Add(New DataColumn("Harga", GetType(String)))
                .Add(New DataColumn("Berat", GetType(String)))
                .Add(New DataColumn("Quantity", GetType(String)))
                .Add(New DataColumn("Total", GetType(String)))
            End With

            Session("GridPilihInvoice") = adt
            Grid_Pilih_invoice.DataSource = adt
            Grid_Pilih_invoice.KeyFieldName = "MuatBarangDetailID"
            Grid_Pilih_invoice.DataBind()
        Catch ex As Exception
            Throw New Exception("<b>Error Create Session :</b>" & ex.ToString)
        End Try
    End Sub


    Private Sub Refresh_Grid()
        Dim DT As DataTable
        Try
            DT = CType(Session("GridDetailInvoice"), DataTable)
            'For i As Integer = 0 To DT.Rows.Count - 1
            '    DT.DefaultView(i).Row("IDW") = (i + 1).ToString
            'Next
            Session("GridDetailContainer") = DT
            Grid_Pilih_invoice.DataSource = DT
            Grid_Pilih_invoice.DataBind()
        Catch ex As Exception
            Response.Write(ex.ToString)
        End Try

    End Sub

    Private Sub clear_Label()
        Try
            lInfo.Text = ""
            linfoberhasil.Text = ""
            lInfo.Visible = False
            linfoberhasil.Visible = False
        Catch ex As Exception
            Throw New Exception("<b>Error clear label :</b>" & ex.ToString)
        End Try
    End Sub

    Private Sub clear_all()
        Try
            Create_Session()
            PanelHitung.Visible = False
            TxtNamaPengirim.Text = ""
            TxtTujuan.Text = ""
            TxtNoInvoice.Text = ""
            TxtPenerima.Text = ""
            HFMode.Value = "Insert"
            HFModeItem.Value = "Insert"
            TxtNamaKapal.Text = ""
            HFInvoiceHeaderID.Value = ""
            HFID.Value = ""
            HFmuatBarangID.Value = ""
            HFInvoiceDetailID.Value = ""
            HFNmBarang.Value = ""
            HFTujuan.Value = ""
            hfDel.Value = ""
            HFNamaPengirim.Value = ""
            HFMBNO.Value = ""
            HFSatuan.Value = ""
            HFIDKapal.Value = ""
            HFNamakapal.Value = ""
            HFPembayaran.Value = ""
            HFPenerima.Value = ""
            TxtNoInvoice.Enabled = True
            HFNoInvoice.Value = ""
            ChkMiniByr.Checked = False
            txtMinByr.Text = ""
            ddlDitunjukan.Items.Clear()
            ChkLainLain.Checked = False
            TxtNmLain.Text = ""
            TxtKotaLain.Text = ""
            ChkOngkos.Checked = False
            TxtOngkos.Text = ""
            TxtJumlahOngkosAngkutan.Text = ""
            HFCodeCust.Value = ""
            HFCodeCustLain.Value = ""
            HFTglBerangkat.Value = ""
            TxtIndikator.Text = ""
            TxtIndikator.Visible = True
            TxtNmBrg.Text = ""

        Catch ex As Exception
            Throw New Exception("<b>error function clear :</b>" & ex.ToString)
        End Try
    End Sub

    Private Sub clear_asuransi()
        TxtAsuransi.Text = ""
        TxtPremi.Text = ""
        TxtHargaAsuransi.Text = ""
        TxtPolis.Text = ""

    End Sub

    Private Sub clear_detail()
        Try
            HFMuatBarangDetailID.Value = ""
            ' TxtNamaBarang.Text = ""
            'TxtQuantity.Text = ""
            ' TxtHargaSatuan.Text = ""
            ' TxtSatuan.Text = ""

        Catch ex As Exception
            Throw New Exception("<b>Error function clear detail :</b>" & ex.ToString)
        End Try
    End Sub
    Private Sub CloseHeader(ByVal ID As String, ByVal NoInvoice As String, ByVal IDMuatBarang As String)

        Dim Warehouse As String
        Dim mbstring As String
        Dim quotation As String
        Dim mbdt As DataTable
        Dim mbds As DataSet
        Dim str As String = ""
        Dim strdelete As String = ""
        Dim ddt As DataTable
        Dim dds As DataSet
        Dim dstr As String
        Dim edt As DataTable
        Dim eds As DataSet
        Dim estr As String
        Dim fdt As DataTable
        Dim fds As DataSet
        Dim fstr As String

        Try
            If Left(NoInvoice.ToString, 1) = "b" Or Left(NoInvoice.ToString, 1) = "B" Then
                mbstring = "select WarehouseheaderID from MuatBarang where Mb_No = '" & IDMuatBarang & "' "
                mbds = SQLExecuteQuery(mbstring)
                mbdt = mbds.Tables(0)
                Warehouse = mbdt.Rows(0).Item("WarehouseheaderID").ToString

                dstr = " select IDDetail from MuatBarangDetail where Mb_No = '" & IDMuatBarang.ToString & "' and  status= 5"
                dds = SQLExecuteQuery(dstr)
                ddt = dds.Tables(0)
                If ddt.Rows.Count <= 0 Then
                    str = " UPDATE MuatBarang " & _
                                "SET " & _
                                "LastModified = '" & Now.ToString & "' " & _
                                ",[status] = 7 " & _
                                "WHERE Mb_No = '" & IDMuatBarang.ToString & "' and status <> 0; "

                    dstr = " select ID from MuatBarangDetail where Mb_No = '" & IDMuatBarang.ToString & "' and  (status= 5 or status =7 )"
                    dds = SQLExecuteQuery(dstr, False)
                    ddt = dds.Tables(0)
                    If ddt.Rows.Count <= 0 Then
                        strdelete &= " delete from MuatBarang where Mb_No = '" & IDMuatBarang.ToString & "';"
                    Else
                        dstr = " select ID from MuatBarangDetail where Mb_No = '" & IDMuatBarang.ToString & "' and  status = 5"
                        dds = SQLExecuteQuery(dstr, False)
                        ddt = dds.Tables(0)
                        If ddt.Rows.Count <= 0 Then
                            strdelete &= " UPDATE MuatBarang " & _
                                "SET " & _
                                "LastModified = '" & Now.ToString & "' " & _
                                ",[status] = 7 " & _
                                "WHERE  Mb_No = '" & IDMuatBarang.ToString & "' and status <>0 ; "
                        End If
                    End If


                    estr = " select ID from WarehouseDetail where WarehouseItem_Code =  '" & Warehouse & "' and status = 1"
                    eds = SQLExecuteQuery(estr)
                    edt = eds.Tables(0)
                    If edt.Rows.Count <= 0 Then
                        str &= " UPDATE WarehouseHeader " & _
                                "SET " & _
                                "LastModified = '" & Now.ToString & "' " & _
                                ",[status] = 7 " & _
                                "WHERE WarehouseItem_Code = '" & Warehouse.ToString & "' and status = 1; "
                        estr = " select ID from WarehouseDetail where WarehouseItem_Code =  '" & Warehouse & "' and (status = 1 or status =7)"
                        eds = SQLExecuteQuery(estr, False)
                        edt = eds.Tables(0)
                        If edt.Rows.Count <= 0 Then
                            estr = "select id.id from InvoiceHeader id left join MuatBarang mbd on id.MuatBarangID= mbd.Mb_No left join " & _
                                       " WarehouseHeader wd on mbd.WarehouseHeaderID = wd.WarehouseItem_Code where wd.WarehouseItem_Code =  '" & Warehouse & "' and id.status = 7;"
                            eds = SQLExecuteQuery(dstr, False)
                            edt = dds.Tables(0)
                            If edt.Rows.Count > 0 Then
                                strdelete &= " UPDATE WarehouseHeader " & _
                              "SET " & _
                              "LastModified = '" & Now.ToString & "' " & _
                              ",[status] = 7 " & _
                              "WHERE WarehouseItem_Code = '" & Warehouse.ToString & "' and status <>0; "
                            Else
                                strdelete &= " delete from WarehouseHeader where WarehouseItem_Code = '" & Warehouse.ToString & "';"
                            End If
                        Else
                            estr = " select ID from WarehouseDetail where WarehouseItem_Code =  '" & Warehouse & "' and status = 1 "
                            eds = SQLExecuteQuery(estr, False)
                            edt = eds.Tables(0)
                            If edt.Rows.Count <= 0 Then
                                strdelete &= " UPDATE WarehouseHeader " & _
                                "SET " & _
                                "LastModified = '" & Now.ToString & "' " & _
                                ",[status] = 7 " & _
                                "WHERE WarehouseItem_Code = '" & Warehouse.ToString & "' and status <>0; "
                            End If
                        End If

                        fstr = "select Quotation_No from Warehouseheader where WarehouseItem_Code = '" & Warehouse.ToString & "' and status = 1"
                        quotation = SQLExecuteScalar(fstr)

                        fstr = "select ID from QuotationDetail where Quotation_No = '" & quotation.ToString & "' and status = 2"
                        fds = SQLExecuteQuery(fstr)
                        fdt = fds.Tables(0)
                        If fdt.Rows.Count > 0 Then
                            str &= " UPDATE MasterQuotation " & _
                           "SET " & _
                           "LastModified = '" & Now.ToString & "' " & _
                                ",[Close] = 'Deal'   " & _
                           ",[status] = 2 " & _
                           "WHERE Quotation_No = '" & quotation.ToString & "' and status = 1; "
                        End If


                        'If fdt.Rows.Count <= 0 Then
                        '    str &= " UPDATE MasterQuotation " & _
                        '        "SET " & _
                        '        "LastModified = '" & Now.ToString & "' " & _
                        '        ",[status] = 5 " & _
                        '        "WHERE Quotation_No = '" & quotation.ToString & "' and status = 1; "
                        '    fstr = "select ID from QuotationDetail where Quotation_No = '" & quotation.ToString & "' and (status = 1 or status = 5)"
                        '    fds = SQLExecuteQuery(fstr, False)
                        '    fdt = fds.Tables(0)
                        '    If fdt.Rows.Count <= 0 Then
                        '        strdelete &= " delete from MasterQuotation where Quotation_No = '" & quotation.ToString & "';"
                        '    Else
                        '        fstr = "select ID from QuotationDetail where Quotation_No = '" & quotation.ToString & "' and (status = 1 )"
                        '        fds = SQLExecuteQuery(fstr, False)
                        '        fdt = fds.Tables(0)
                        '        If fdt.Rows.Count <= 0 Then
                        '            strdelete &= " UPDATE MasterQuotation " & _
                        '                                    "SET " & _
                        '                                    "LastModified = '" & Now.ToString & "' " & _
                        '                                    ",[status] = 5 " & _
                        '                                    "WHERE Quotation_No = '" & quotation.ToString & "' and status <> 0; "
                        '        End If
                        '    End If
                        'End If
                    End If
                    If strdelete <> "" Then
                        result = SQLExecuteNonQuery(strdelete, True, False) ' PPN
                    End If
                    If str <> "" Then
                        result = SQLExecuteNonQuery(str, False, True) 'NPPN
                    End If
                End If
            Else
                mbstring = "select WarehouseheaderID from MuatBarang where Mb_No = '" & IDMuatBarang & "' "
                mbds = SQLExecuteQuery(mbstring)
                mbdt = mbds.Tables(0)
                Warehouse = mbdt.Rows(0).Item("WarehouseheaderID").ToString

                dstr = " select ID from MuatBarangDetail where Mb_No = '" & IDMuatBarang.ToString & "' and  status= 5"
                dds = SQLExecuteQuery(dstr)
                ddt = dds.Tables(0)
                If ddt.Rows.Count <= 0 Then
                    str = " UPDATE MuatBarang " & _
                                "SET " & _
                                "LastModified = '" & Now.ToString & "' " & _
                                ",[status] = 7 " & _
                                "WHERE Mb_No = '" & IDMuatBarang.ToString & "' and status <> 0; "

                    estr = " select ID from WarehouseDetail where WarehouseItem_Code =  '" & Warehouse & "' and status = 1"
                    eds = SQLExecuteQuery(estr)
                    edt = eds.Tables(0)
                    If edt.Rows.Count <= 0 Then
                        str &= " UPDATE WarehouseHeader " & _
                                "SET " & _
                                "LastModified = '" & Now.ToString & "' " & _
                                ",[status] = 7 " & _
                                "WHERE WarehouseItem_Code = '" & Warehouse.ToString & "' and status = 1; "
                        fstr = "select Quotation_No from Warehouseheader where WarehouseItem_Code = '" & Warehouse.ToString & "' and status = 1"
                        quotation = SQLExecuteScalar(fstr)

                        fstr = "select ID from QuotationDetail where Quotation_No = '" & quotation.ToString & "' and status = 2"
                        fds = SQLExecuteQuery(fstr)
                        fdt = fds.Tables(0)
                        If fdt.Rows.Count > 0 Then
                            str &= " UPDATE MasterQuotation " & _
                                "SET " & _
                                "LastModified = '" & Now.ToString & "' " & _
                                ",[Close] = 'Deal'  " & _
                                ",[status] = 2 " & _
                                "WHERE Quotation_No = '" & quotation.ToString & "' and status = 1; "

                        End If
                        'If fdt.Rows.Count <= 0 Then
                        '    str &= " UPDATE MasterQuotation " & _
                        '        "SET " & _
                        '        "LastModified = '" & Now.ToString & "' " & _
                        '        ",[status] = 5 " & _
                        '        "WHERE Quotation_No = '" & quotation.ToString & "' and status = 1; "

                        'End If
                    End If
                    result = SQLExecuteNonQuery(str)

                End If
            End If
            load_grid_invoice_header()
        Catch ex As Exception
            Throw New Exception("Error CloseHeader function " & ex.ToString)
        End Try

    End Sub

    Private Function create_table() As DataTable
        Dim kDT As New DataTable
        Try
            kDT.Columns.Add(New DataColumn("ID", GetType(String)))
            kDT.Columns.Add(New DataColumn("Quotation_No", GetType(String)))
            kDT.Columns.Add(New DataColumn("Jumlah", GetType(String)))
        Catch ex As Exception
            Response.Write("Error Create Session <BR> : " & ex.ToString)
        End Try
        Return kDT
    End Function

    Private Sub CekDitujukan(ByVal MbNo As String, ByVal NamaDitujukan As String, ByVal KotaDitujukan As String)
        Try
            Dim flg As Integer = 0

            sqlstring = "SELECT distinct (MC.Jenis_Perusahaan + ' ' + MC.Nama_Customer + ' - ' + MC.KotaDitunjukan) as dituju " & _
                        "FROM MuatBarang MB " & _
                        "LEFT JOIN MasterCustomer MC ON MB.Customer_Id = MC.Kode_Customer  " & _
                        "WHERE MB.Mb_No  = '" & MbNo.ToString & "' " & _
                        "AND MB.[status] = 5 " & _
                        "AND MC.[status] = 1 " & _
                        "UNION " & _
                        "SELECT distinct (MQ.Penerima + ' - ' + MQ.Tujuan) as dituju " & _
                        "FROM MuatBarang MB " & _
                        "LEFT JOIN WarehouseHeader WHD ON MB.WarehouseHeaderID= WHD.WarehouseItem_Code " & _
                        "LEFT JOIN MasterQuotation MQ ON WHD.Quotation_No = MQ.Quotation_No " & _
                        "WHERE MB.Mb_No = '" & MbNo.ToString & "' " & _
                        "AND MB.[status] = 5 " & _
                        "AND WHD.[status] = 1 " & _
                        "AND (mq.[status] = 1 or mq.[status] = 2 ) "
            DT = SQLExecuteQuery(sqlstring).Tables(0)

            If DT.Rows.Count > 0 Then
                For i As Integer = 0 To DT.Rows.Count - 1
                    With DT.Rows(i)
                        If .Item("dituju") = NamaDitujukan.Trim & " " & "-" & " " & KotaDitujukan.Trim Then
                            flg = flg + 1
                        Else
                            flg = 0
                        End If
                    End With
                Next

                If flg = 0 Then
                    ChkLainLain.Checked = True
                    TxtNmLain.Text = NamaDitujukan.Trim
                    TxtKotaLain.Text = KotaDitujukan.Trim
                Else
                    ChkLainLain.Checked = False
                End If
            End If
        Catch ex As Exception
            Throw New Exception(ex.ToString)
        End Try
    End Sub

    Private Function GETNamaCustomer(ByVal KodeCustomer As String) As String
        Try
            Dim NamaCust As String = ""
            Dim TipeCustomer As String = ""
            Dim STR As String = ""

            STR = "select Tipe_Customer FROM MasterCustomer " & _
                    "where status <> 0 and Kode_Customer = '" & KodeCustomer & "' "
            TipeCustomer = SQLExecuteScalar(STR)

            If TipeCustomer = "Perusahaan" Then
                STR = "select Nama_Customer+','+ Jenis_Perusahaan FROM MasterCustomer " & _
                        "WHERE status <> 0 AND Kode_Customer = '" & KodeCustomer & "' "
            Else
                STR = "select Nama_Customer FROM MasterCustomer " & _
                        "WHERE status <> 0 AND Kode_Customer = '" & KodeCustomer & "' "
            End If

            NamaCust = SQLExecuteScalar(STR)

            Return NamaCust

        Catch ex As Exception
            Throw New Exception(ex.ToString)
        End Try
    End Function


    Private Sub Insert()
        Try

            sqlstring = ""
            Dim TotalBayar As Double = 0
            Dim TotalSatuan As Double = 0
            Dim pisah() As String
            Dim sqlponly As String = ""
            Dim iddetail As Integer
            Dim hargasatuan As Double = 0
            Dim TotalAsuransi As Double = 0
            Dim NamaDitunjukan As String = ""
            Dim KotaDitunjukan As String = ""
            Dim KeteranganOngkosAngkut As String = ""
            Dim CodeCust As String = ""
            Dim bulan As String
            Dim tahun As String
            'added to insert journal
            Dim eDS As DataSet
            Dim eDT As DataTable
            Dim RunNo As Int64 = GetRunNoHeader()

            TxtNoInvoice.Text = Load_Invoice_No(TxtIndikator.Text.Trim.Replace("'", "''"))

            bulan = CDate(HFTglBerangkat.Value).ToString("MM")
            tahun = CDate(HFTglBerangkat.Value).ToString("yy")

            If ChkLainLain.Checked Then
                NamaDitunjukan = GETNamaCustomer(HFCodeCustLain.Value)
                KotaDitunjukan = TxtKotaLain.Text.ToString.Trim.Replace("'", "''")
                CodeCust = HFCodeCustLain.Value
            Else
                pisah = ddlDitunjukan.SelectedValue.ToString.Split("-")
                NamaDitunjukan = pisah(0)
                KotaDitunjukan = pisah(1)
                CodeCust = HFCodeCust.Value
            End If



            If chkAsuransi.Checked = True Then
                TotalAsuransi = (CDbl(ReplaceString(TxtHargaAsuransi.Text)) * (TxtPremi.Text.Replace(",", ".") / 100)) + CDbl(ReplaceString(TxtPolis.Text))
            End If

            For i As Integer = 0 To Grid_Pilih_invoice.VisibleRowCount - 1
                TotalBayar = TotalBayar + CDbl(Grid_Pilih_invoice.GetRowValues(i, "TotalByrI").ToString)
            Next


            If ChkOngkos.Checked Then
                KeteranganOngkosAngkut = TxtOngkos.Text.Trim.Replace("'", "''") & "-" & TxtJumlahOngkosAngkutan.Text
                TotalBayar = TotalBayar + ReplaceString(TxtJumlahOngkosAngkutan.Text.Trim.Replace("'", "''"))
            End If

            If CDbl(hfPaid.Value) > 0 Then
                hfPaid.Value = 100 - CDbl(hfPaid.Value)
            End If

            '<--- Insert to AR journal ---->'


            If GetCOAPiutangCust(NamaDitunjukan.ToString) = True Then
                If GetCOAPenghasilanKapal(HFIDKapal.Value) = True Then
                    eDS = GetConfigAR(NamaDitunjukan.ToString, HFIDKapal.Value)
                    eDT = eDS.Tables(0)
                    If eDT.Rows.Count > 0 Then
                        'INSERT TO INVOICE HEADER N DETAIL

                        sqlstring = "INSERT INTO InvoiceHeader(No_Invoice, MuatbarangID, KapalID, Total, InvoiceDate, Ditujukan, DaerahDitujukan, " & _
                        "NoAsuransi, HargaAsuransi, Premi, Polis, TotalAsuransi,Paid,DP, UserName, [status], KeteranganOngkosAngkut, CodeCust, NamaBarang, StatusNumber, BlnInvoice) VALUES " & _
                      "('" & TxtNoInvoice.Text.ToString & "', '" & HFmuatBarangID.Value & "', " & HFIDKapal.Value & " " & _
                      ", " & TotalBayar & ", '" & DtInvoice.Text & "', '" & NamaDitunjukan.ToString & "', '" & KotaDitunjukan.ToString & "', " & _
                      " '" & TxtAsuransi.Text.ToString & "', " & CekNilai(ReplaceString(TxtHargaAsuransi.Text.Replace("'", "''").Trim)) & ", " & _
                      "'" & CekNilai(TxtPremi.Text.Trim.Replace("'", "''").Replace(",", ".")) & "', '" & CekNilai(ReplaceString(TxtPolis.Text.Trim.Replace("'", "''"))) & "', " & _
                      "" & TotalAsuransi & ",'" & hfPaid.Value & "' , 'No', '" & Session("UserId") & "', 1, '" & KeteranganOngkosAngkut & "', '" & CodeCust & "', " & _
                      " '" & TxtNmBrg.Text.ToString.Trim.Replace("'", "''") & "', 1, '" & CekBulan(bulan) & "/" & tahun & "' );"
                        iddetail = 0

                        '<--- Check New Invoice Number Start With b or not ---->'

                        If Left(TxtNoInvoice.Text, 1) = "b" Or Left(TxtNoInvoice.Text, 1) = "B" Then
                            TotalBayar = 0
                            For i As Integer = 0 To Grid_Pilih_invoice.VisibleRowCount - 1
                                With Grid_Pilih_invoice
                                    '<---- Check jenis satuan ----->'

                                    Dim TOtalUkuran As String = ""

                                    Select Case .GetRowValues(i, "JenisPembayaranI").ToString.ToUpper
                                        Case "TON"
                                            TOtalUkuran = (CDbl(System.Math.Round(.GetRowValues(i, "BeratTotalI").ToString / 1000, 3))).ToString
                                        Case "KUBIK"
                                            TOtalUkuran = .GetRowValues(i, "VolumeTotalI").ToString
                                        Case "UNIT"
                                            TOtalUkuran = .GetRowValues(i, "TotalQtyI").ToString
                                        Case "CONTAINER"
                                            TOtalUkuran = .GetRowValues(i, "TotalQtyI").ToString
                                        Case "SATUAN"
                                            TOtalUkuran = .GetRowValues(i, "TotalQtyI").ToString
                                    End Select

                                    TotalSatuan = CDbl(.GetRowValues(i, "TotalByrI").ToString)
                                    TotalBayar = TotalBayar + TotalSatuan
                                    iddetail = iddetail + 1
                                    sqlstring &= "INSERT INTO InvoiceDetail(IDDetail, Mb_No,No_Invoice,QuotationDetailID, Hargatotal, Paid, DP, UserName, [status], TotalUkuran) VALUES " & _
                                         "('" & iddetail & "', '" & .GetRowValues(i, "Mb_NoI") & "','" & TxtNoInvoice.Text.ToString & "','" & .GetRowValues(i, "IDQuotationDetailI") & "', " & TotalSatuan & " " & _
                                         ", '" & hfPaid.Value & "' ,'No','" & Session("UserId") & "', 1, '" & TOtalUkuran & "'); "

                                End With
                            Next
                            result = SQLExecuteNonQuery(sqlstring, False, True)
                        Else
                            iddetail = 0
                            TotalBayar = 0
                            For i As Integer = 0 To Grid_Pilih_invoice.VisibleRowCount - 1
                                With Grid_Pilih_invoice

                                    Dim TOtalUkuran As String = ""

                                    Select Case .GetRowValues(i, "JenisPembayaranI").ToString.ToUpper
                                        Case "TON"
                                            TOtalUkuran = (CDbl(System.Math.Round(.GetRowValues(i, "BeratTotalI").ToString / 1000, 3))).ToString
                                        Case "KUBIK"
                                            TOtalUkuran = .GetRowValues(i, "VolumeTotalI").ToString
                                        Case "UNIT"
                                            TOtalUkuran = .GetRowValues(i, "TotalQtyI").ToString
                                        Case "CONTAINER"
                                            TOtalUkuran = .GetRowValues(i, "TotalQtyI").ToString
                                        Case "SATUAN"
                                            TOtalUkuran = .GetRowValues(i, "TotalQtyI").ToString
                                    End Select

                                    TotalSatuan = CDbl(.GetRowValues(i, "TotalByrI").ToString)
                                    TotalBayar = TotalBayar + TotalSatuan
                                    iddetail = iddetail + 1
                                    sqlstring &= "INSERT INTO InvoiceDetail(IDDetail, Mb_No,No_Invoice,QuotationDetailID, Hargatotal, Paid, DP, UserName, [status], TotalUkuran) VALUES " & _
                                         "('" & iddetail & "', '" & .GetRowValues(i, "Mb_NoI") & "','" & TxtNoInvoice.Text.ToString & "','" & .GetRowValues(i, "IDQuotationDetailI") & "', " & TotalSatuan & " " & _
                                         ", '" & hfPaid.Value & "' ,'No','" & Session("UserId") & "', 1, '" & TOtalUkuran & "'); "

                                End With

                            Next
                            If sqlstring <> "" Then
                                result = SQLExecuteNonQuery(sqlstring)
                            End If

                        End If

                        'INSERT JOURNAL
                        InsertARHeader(RunNo, TxtNoInvoice.Text.ToString, DtInvoice.Text, TotalBayar, CodeCust.ToString, "NULL", Session("UserId").ToString)
                        InsertARDetailDebit(RunNo, NamaDitunjukan, TotalBayar, Session("UserId").ToString)
                        InsertARDetailKredit(RunNo, HFIDKapal.Value, TotalBayar, Session("UserId").ToString)
                        'END INSERT JOURNAL

                        If result <> "" Then
                            If ChkMiniByr.Checked Then 'cek 1
                                PembayaranMinimum()
                            End If

                            Grid_Pilih_invoice.DataSource = Nothing
                            Grid_Pilih_invoice.DataBind()
                            clear_all()
                            linfoberhasil.Text = "Simpan Berhasil"
                        End If
                        'END INSERT INVOICE


                    Else
                        'INSERT LINKED ACCOUNT DULU
                        InsertLinkedAccount(HFIDKapal.Value, NamaDitunjukan.ToString, Session("UserId"))
                        'END INSERT LINKED ACCOUNT

                        'INSERT TO INVOICE HEADER N DETAIL

                        sqlstring = "INSERT INTO InvoiceHeader(No_Invoice, MuatbarangID, KapalID, Total, InvoiceDate, Ditujukan, DaerahDitujukan, " & _
                        "NoAsuransi, HargaAsuransi, Premi, Polis, TotalAsuransi,Paid,DP, UserName, [status], KeteranganOngkosAngkut, CodeCust, NamaBarang, StatusNumber, BlnInvoice) VALUES " & _
                      "('" & TxtNoInvoice.Text.ToString & "', '" & HFmuatBarangID.Value & "', " & HFIDKapal.Value & " " & _
                      ", " & TotalBayar & ", '" & DtInvoice.Text & "', '" & NamaDitunjukan.ToString & "', '" & KotaDitunjukan.ToString & "', " & _
                      " '" & TxtAsuransi.Text.ToString & "', " & CekNilai(ReplaceString(TxtHargaAsuransi.Text.Replace("'", "''").Trim)) & ", " & _
                      "'" & CekNilai(TxtPremi.Text.Trim.Replace("'", "''").Replace(",", ".")) & "', '" & CekNilai(ReplaceString(TxtPolis.Text.Trim.Replace("'", "''"))) & "', " & _
                      "" & TotalAsuransi & ",'" & hfPaid.Value & "' , 'No', '" & Session("UserId") & "', 1, '" & KeteranganOngkosAngkut & "', '" & CodeCust & "', " & _
                      " '" & TxtNmBrg.Text.ToString.Trim.Replace("'", "''") & "', 1, '" & CekBulan(bulan) & "/" & tahun & "' );"
                        iddetail = 0

                        '<--- Check New Invoice Number Start With b or not ---->'

                        If Left(TxtNoInvoice.Text, 1) = "b" Or Left(TxtNoInvoice.Text, 1) = "B" Then
                            TotalBayar = 0
                            For i As Integer = 0 To Grid_Pilih_invoice.VisibleRowCount - 1
                                With Grid_Pilih_invoice
                                    '<---- Check jenis satuan ----->'

                                    Dim TOtalUkuran As String = ""

                                    Select Case .GetRowValues(i, "JenisPembayaranI").ToString.ToUpper
                                        Case "TON"
                                            TOtalUkuran = (CDbl(System.Math.Round(.GetRowValues(i, "BeratTotalI").ToString / 1000, 3))).ToString
                                        Case "KUBIK"
                                            TOtalUkuran = .GetRowValues(i, "VolumeTotalI").ToString
                                        Case "UNIT"
                                            TOtalUkuran = .GetRowValues(i, "TotalQtyI").ToString
                                        Case "CONTAINER"
                                            TOtalUkuran = .GetRowValues(i, "TotalQtyI").ToString
                                        Case "SATUAN"
                                            TOtalUkuran = .GetRowValues(i, "TotalQtyI").ToString
                                    End Select

                                    TotalSatuan = CDbl(.GetRowValues(i, "TotalByrI").ToString)
                                    TotalBayar = TotalBayar + TotalSatuan
                                    iddetail = iddetail + 1
                                    sqlstring &= "INSERT INTO InvoiceDetail(IDDetail, Mb_No,No_Invoice,QuotationDetailID, Hargatotal, Paid, DP, UserName, [status], TotalUkuran) VALUES " & _
                                         "('" & iddetail & "', '" & .GetRowValues(i, "Mb_NoI") & "','" & TxtNoInvoice.Text.ToString & "','" & .GetRowValues(i, "IDQuotationDetailI") & "', " & TotalSatuan & " " & _
                                         ", '" & hfPaid.Value & "' ,'No','" & Session("UserId") & "', 1, '" & TOtalUkuran & "'); "

                                End With
                            Next
                            result = SQLExecuteNonQuery(sqlstring, False, True)
                        Else
                            iddetail = 0
                            TotalBayar = 0
                            For i As Integer = 0 To Grid_Pilih_invoice.VisibleRowCount - 1
                                With Grid_Pilih_invoice

                                    Dim TOtalUkuran As String = ""

                                    Select Case .GetRowValues(i, "JenisPembayaranI").ToString.ToUpper
                                        Case "TON"
                                            TOtalUkuran = (CDbl(System.Math.Round(.GetRowValues(i, "BeratTotalI").ToString / 1000, 3))).ToString
                                        Case "KUBIK"
                                            TOtalUkuran = .GetRowValues(i, "VolumeTotalI").ToString
                                        Case "UNIT"
                                            TOtalUkuran = .GetRowValues(i, "TotalQtyI").ToString
                                        Case "CONTAINER"
                                            TOtalUkuran = .GetRowValues(i, "TotalQtyI").ToString
                                        Case "SATUAN"
                                            TOtalUkuran = .GetRowValues(i, "TotalQtyI").ToString
                                    End Select

                                    TotalSatuan = CDbl(.GetRowValues(i, "TotalByrI").ToString)
                                    TotalBayar = TotalBayar + TotalSatuan
                                    iddetail = iddetail + 1
                                    sqlstring &= "INSERT INTO InvoiceDetail(IDDetail, Mb_No,No_Invoice,QuotationDetailID, Hargatotal, Paid, DP, UserName, [status], TotalUkuran) VALUES " & _
                                         "('" & iddetail & "', '" & .GetRowValues(i, "Mb_NoI") & "','" & TxtNoInvoice.Text.ToString & "','" & .GetRowValues(i, "IDQuotationDetailI") & "', " & TotalSatuan & " " & _
                                         ", '" & hfPaid.Value & "' ,'No','" & Session("UserId") & "', 1, '" & TOtalUkuran & "'); "

                                End With

                            Next
                            If sqlstring <> "" Then
                                result = SQLExecuteNonQuery(sqlstring)
                            End If

                        End If

                        'INSERT JOURNAL
                        InsertARHeader(RunNo, TxtNoInvoice.Text.ToString, DtInvoice.Text, TotalBayar, CodeCust.ToString, "NULL", Session("UserId").ToString)
                        InsertARDetailDebit(RunNo, NamaDitunjukan, TotalBayar, Session("UserId").ToString)
                        InsertARDetailKredit(RunNo, HFIDKapal.Value, TotalBayar, Session("UserId").ToString)
                        'END INSERT JOURNAL

                        If result <> "" Then
                            If ChkMiniByr.Checked Then
                                PembayaranMinimum() 'cek 2
                            End If

                            Grid_Pilih_invoice.DataSource = Nothing
                            Grid_Pilih_invoice.DataBind()
                            clear_all()
                            linfoberhasil.Text = "Simpan Berhasil"
                        End If
                        'END INSERT INVOICE
                    End If
                Else
                    'INSERT COA PENGHASILAN DAN LINKED ACCOUNT DULU
                    InsertCOAPenghasilanKapal(HFIDKapal.Value, Session("UserId"))
                    InsertLinkedAccount(HFIDKapal.Value, NamaDitunjukan.ToString, Session("UserId"))
                    'END INSERT LINKED ACCOUNT

                    'INSERT TO INVOICE HEADER N DETAIL

                    sqlstring = "INSERT INTO InvoiceHeader(No_Invoice, MuatbarangID, KapalID, Total, InvoiceDate, Ditujukan, DaerahDitujukan, " & _
                    "NoAsuransi, HargaAsuransi, Premi, Polis, TotalAsuransi,Paid,DP, UserName, [status], KeteranganOngkosAngkut, CodeCust, NamaBarang, StatusNumber, BlnInvoice) VALUES " & _
                  "('" & TxtNoInvoice.Text.ToString & "', '" & HFmuatBarangID.Value & "', " & HFIDKapal.Value & " " & _
                  ", " & TotalBayar & ", '" & DtInvoice.Text & "', '" & NamaDitunjukan.ToString & "', '" & KotaDitunjukan.ToString & "', " & _
                  " '" & TxtAsuransi.Text.ToString & "', " & CekNilai(ReplaceString(TxtHargaAsuransi.Text.Replace("'", "''").Trim)) & ", " & _
                  "'" & CekNilai(TxtPremi.Text.Trim.Replace("'", "''").Replace(",", ".")) & "', '" & CekNilai(ReplaceString(TxtPolis.Text.Trim.Replace("'", "''"))) & "', " & _
                  "" & TotalAsuransi & ",'" & hfPaid.Value & "' , 'No', '" & Session("UserId") & "', 1, '" & KeteranganOngkosAngkut & "', '" & CodeCust & "', " & _
                  " '" & TxtNmBrg.Text.ToString.Trim.Replace("'", "''") & "', 1, '" & CekBulan(bulan) & "/" & tahun & "' );"
                    iddetail = 0

                    '<--- Check New Invoice Number Start With b or not ---->'

                    If Left(TxtNoInvoice.Text, 1) = "b" Or Left(TxtNoInvoice.Text, 1) = "B" Then
                        TotalBayar = 0
                        For i As Integer = 0 To Grid_Pilih_invoice.VisibleRowCount - 1
                            With Grid_Pilih_invoice
                                '<---- Check jenis satuan ----->'

                                Dim TOtalUkuran As String = ""

                                Select Case .GetRowValues(i, "JenisPembayaranI").ToString.ToUpper
                                    Case "TON"
                                        TOtalUkuran = (CDbl(System.Math.Round(.GetRowValues(i, "BeratTotalI").ToString / 1000, 3))).ToString
                                    Case "KUBIK"
                                        TOtalUkuran = .GetRowValues(i, "VolumeTotalI").ToString
                                    Case "UNIT"
                                        TOtalUkuran = .GetRowValues(i, "TotalQtyI").ToString
                                    Case "CONTAINER"
                                        TOtalUkuran = .GetRowValues(i, "TotalQtyI").ToString
                                    Case "SATUAN"
                                        TOtalUkuran = .GetRowValues(i, "TotalQtyI").ToString
                                End Select

                                TotalSatuan = CDbl(.GetRowValues(i, "TotalByrI").ToString)
                                TotalBayar = TotalBayar + TotalSatuan
                                iddetail = iddetail + 1
                                sqlstring &= "INSERT INTO InvoiceDetail(IDDetail, Mb_No,No_Invoice,QuotationDetailID, Hargatotal, Paid, DP, UserName, [status], TotalUkuran) VALUES " & _
                                     "('" & iddetail & "', '" & .GetRowValues(i, "Mb_NoI") & "','" & TxtNoInvoice.Text.ToString & "','" & .GetRowValues(i, "IDQuotationDetailI") & "', " & TotalSatuan & " " & _
                                     ", '" & hfPaid.Value & "' ,'No','" & Session("UserId") & "', 1, '" & TOtalUkuran & "'); "

                            End With
                        Next
                        result = SQLExecuteNonQuery(sqlstring, False, True)
                    Else
                        iddetail = 0
                        TotalBayar = 0
                        For i As Integer = 0 To Grid_Pilih_invoice.VisibleRowCount - 1
                            With Grid_Pilih_invoice

                                Dim TOtalUkuran As String = ""

                                Select Case .GetRowValues(i, "JenisPembayaranI").ToString.ToUpper
                                    Case "TON"
                                        TOtalUkuran = (CDbl(System.Math.Round(.GetRowValues(i, "BeratTotalI").ToString / 1000, 3))).ToString
                                    Case "KUBIK"
                                        TOtalUkuran = .GetRowValues(i, "VolumeTotalI").ToString
                                    Case "UNIT"
                                        TOtalUkuran = .GetRowValues(i, "TotalQtyI").ToString
                                    Case "CONTAINER"
                                        TOtalUkuran = .GetRowValues(i, "TotalQtyI").ToString
                                    Case "SATUAN"
                                        TOtalUkuran = .GetRowValues(i, "TotalQtyI").ToString
                                End Select

                                TotalSatuan = CDbl(.GetRowValues(i, "TotalByrI").ToString)
                                TotalBayar = TotalBayar + TotalSatuan
                                iddetail = iddetail + 1
                                sqlstring &= "INSERT INTO InvoiceDetail(IDDetail, Mb_No,No_Invoice,QuotationDetailID, Hargatotal, Paid, DP, UserName, [status], TotalUkuran) VALUES " & _
                                     "('" & iddetail & "', '" & .GetRowValues(i, "Mb_NoI") & "','" & TxtNoInvoice.Text.ToString & "','" & .GetRowValues(i, "IDQuotationDetailI") & "', " & TotalSatuan & " " & _
                                     ", '" & hfPaid.Value & "' ,'No','" & Session("UserId") & "', 1, '" & TOtalUkuran & "'); "

                            End With

                        Next
                        If sqlstring <> "" Then
                            result = SQLExecuteNonQuery(sqlstring)
                        End If

                    End If

                    'INSERT JOURNAL
                    InsertARHeader(RunNo, TxtNoInvoice.Text.ToString, DtInvoice.Text, TotalBayar, CodeCust.ToString, "NULL", Session("UserId").ToString)
                    InsertARDetailDebit(RunNo, NamaDitunjukan, TotalBayar, Session("UserId").ToString)
                    InsertARDetailKredit(RunNo, HFIDKapal.Value, TotalBayar, Session("UserId").ToString)
                    'END INSERT JOURNAL

                    If result <> "" Then
                        If ChkMiniByr.Checked Then
                            PembayaranMinimum() 'cek 3
                        End If

                        Grid_Pilih_invoice.DataSource = Nothing
                        Grid_Pilih_invoice.DataBind()
                        clear_all()
                        linfoberhasil.Text = "Simpan Berhasil"
                    End If
                    'END INSERT INVOICE
                End If
            Else
                'CEK ADA COA PENGHASILANNYA ATAU GA
                If GetCOAPenghasilanKapal(HFIDKapal.Value) = True Then
                    'INSERT COA PIUTANG DAN LINKED ACCOUNT DULU
                    InsertCOAPiutang(NamaDitunjukan.ToString, Session("userId"))
                    InsertLinkedAccount(HFIDKapal.Value, NamaDitunjukan.ToString, Session("UserId"))
                    'END INSERT LINKED ACCOUNT

                    'INSERT TO INVOICE HEADER N DETAIL

                    sqlstring = "INSERT INTO InvoiceHeader(No_Invoice, MuatbarangID, KapalID, Total, InvoiceDate, Ditujukan, DaerahDitujukan, " & _
                    "NoAsuransi, HargaAsuransi, Premi, Polis, TotalAsuransi,Paid,DP, UserName, [status], KeteranganOngkosAngkut, CodeCust, NamaBarang, StatusNumber, BlnInvoice) VALUES " & _
                  "('" & TxtNoInvoice.Text.ToString & "', '" & HFmuatBarangID.Value & "', " & HFIDKapal.Value & " " & _
                  ", " & TotalBayar & ", '" & DtInvoice.Text & "', '" & NamaDitunjukan.ToString & "', '" & KotaDitunjukan.ToString & "', " & _
                  " '" & TxtAsuransi.Text.ToString & "', " & CekNilai(ReplaceString(TxtHargaAsuransi.Text.Replace("'", "''").Trim)) & ", " & _
                  "'" & CekNilai(TxtPremi.Text.Trim.Replace("'", "''").Replace(",", ".")) & "', '" & CekNilai(ReplaceString(TxtPolis.Text.Trim.Replace("'", "''"))) & "', " & _
                  "" & TotalAsuransi & ",'" & hfPaid.Value & "' , 'No', '" & Session("UserId") & "', 1, '" & KeteranganOngkosAngkut & "', '" & CodeCust & "', " & _
                  " '" & TxtNmBrg.Text.ToString.Trim.Replace("'", "''") & "', 1, '" & CekBulan(bulan) & "/" & tahun & "' );"
                    iddetail = 0

                    '<--- Check New Invoice Number Start With b or not ---->'

                    If Left(TxtNoInvoice.Text, 1) = "b" Or Left(TxtNoInvoice.Text, 1) = "B" Then
                        TotalBayar = 0
                        For i As Integer = 0 To Grid_Pilih_invoice.VisibleRowCount - 1
                            With Grid_Pilih_invoice
                                '<---- Check jenis satuan ----->'

                                Dim TOtalUkuran As String = ""

                                Select Case .GetRowValues(i, "JenisPembayaranI").ToString.ToUpper
                                    Case "TON"
                                        TOtalUkuran = (CDbl(System.Math.Round(.GetRowValues(i, "BeratTotalI").ToString / 1000, 3))).ToString
                                    Case "KUBIK"
                                        TOtalUkuran = .GetRowValues(i, "VolumeTotalI").ToString
                                    Case "UNIT"
                                        TOtalUkuran = .GetRowValues(i, "TotalQtyI").ToString
                                    Case "CONTAINER"
                                        TOtalUkuran = .GetRowValues(i, "TotalQtyI").ToString
                                    Case "SATUAN"
                                        TOtalUkuran = .GetRowValues(i, "TotalQtyI").ToString
                                End Select

                                TotalSatuan = CDbl(.GetRowValues(i, "TotalByrI").ToString)
                                TotalBayar = TotalBayar + TotalSatuan
                                iddetail = iddetail + 1
                                sqlstring &= "INSERT INTO InvoiceDetail(IDDetail, Mb_No,No_Invoice,QuotationDetailID, Hargatotal, Paid, DP, UserName, [status], TotalUkuran) VALUES " & _
                                     "('" & iddetail & "', '" & .GetRowValues(i, "Mb_NoI") & "','" & TxtNoInvoice.Text.ToString & "','" & .GetRowValues(i, "IDQuotationDetailI") & "', " & TotalSatuan & " " & _
                                     ", '" & hfPaid.Value & "' ,'No','" & Session("UserId") & "', 1, '" & TOtalUkuran & "'); "

                            End With
                        Next
                        result = SQLExecuteNonQuery(sqlstring, False, True)
                    Else
                        iddetail = 0
                        TotalBayar = 0
                        For i As Integer = 0 To Grid_Pilih_invoice.VisibleRowCount - 1
                            With Grid_Pilih_invoice

                                Dim TOtalUkuran As String = ""

                                Select Case .GetRowValues(i, "JenisPembayaranI").ToString.ToUpper
                                    Case "TON"
                                        TOtalUkuran = (CDbl(System.Math.Round(.GetRowValues(i, "BeratTotalI").ToString / 1000, 3))).ToString
                                    Case "KUBIK"
                                        TOtalUkuran = .GetRowValues(i, "VolumeTotalI").ToString
                                    Case "UNIT"
                                        TOtalUkuran = .GetRowValues(i, "TotalQtyI").ToString
                                    Case "CONTAINER"
                                        TOtalUkuran = .GetRowValues(i, "TotalQtyI").ToString
                                    Case "SATUAN"
                                        TOtalUkuran = .GetRowValues(i, "TotalQtyI").ToString
                                End Select

                                TotalSatuan = CDbl(.GetRowValues(i, "TotalByrI").ToString)
                                TotalBayar = TotalBayar + TotalSatuan
                                iddetail = iddetail + 1
                                sqlstring &= "INSERT INTO InvoiceDetail(IDDetail, Mb_No,No_Invoice,QuotationDetailID, Hargatotal, Paid, DP, UserName, [status], TotalUkuran) VALUES " & _
                                     "('" & iddetail & "', '" & .GetRowValues(i, "Mb_NoI") & "','" & TxtNoInvoice.Text.ToString & "','" & .GetRowValues(i, "IDQuotationDetailI") & "', " & TotalSatuan & " " & _
                                     ", '" & hfPaid.Value & "' ,'No','" & Session("UserId") & "', 1, '" & TOtalUkuran & "'); "

                            End With

                        Next
                        If sqlstring <> "" Then
                            result = SQLExecuteNonQuery(sqlstring)
                        End If

                    End If

                    'INSERT JOURNAL
                    InsertARHeader(RunNo, TxtNoInvoice.Text.ToString, DtInvoice.Text, TotalBayar, CodeCust.ToString, "NULL", Session("UserId").ToString)
                    InsertARDetailDebit(RunNo, NamaDitunjukan, TotalBayar, Session("UserId").ToString)
                    InsertARDetailKredit(RunNo, HFIDKapal.Value, TotalBayar, Session("UserId").ToString)
                    'END INSERT JOURNAL

                    If result <> "" Then
                        If ChkMiniByr.Checked Then
                            PembayaranMinimum()  ' cek 4
                        End If

                        Grid_Pilih_invoice.DataSource = Nothing
                        Grid_Pilih_invoice.DataBind()
                        clear_all()
                        linfoberhasil.Text = "Simpan Berhasil"
                    End If
                    'END INSERT INVOICE
                Else
                    'INSERT COA PENGHASILAN DAN LINKED ACCOUNT DULU
                    InsertCOAPiutang(NamaDitunjukan.ToString, Session("userId"))
                    InsertCOAPenghasilanKapal(HFIDKapal.ToString, Session("userId"))
                    InsertLinkedAccount(HFIDKapal.Value, NamaDitunjukan.ToString, Session("UserId"))
                    'END INSERT LINKED ACCOUNT

                    'INSERT TO INVOICE HEADER N DETAIL

                    sqlstring = "INSERT INTO InvoiceHeader(No_Invoice, MuatbarangID, KapalID, Total, InvoiceDate, Ditujukan, DaerahDitujukan, " & _
                    "NoAsuransi, HargaAsuransi, Premi, Polis, TotalAsuransi,Paid,DP, UserName, [status], KeteranganOngkosAngkut, CodeCust, NamaBarang, StatusNumber, BlnInvoice) VALUES " & _
                  "('" & TxtNoInvoice.Text.ToString & "', '" & HFmuatBarangID.Value & "', " & HFIDKapal.Value & " " & _
                  ", " & TotalBayar & ", '" & DtInvoice.Text & "', '" & NamaDitunjukan.ToString & "', '" & KotaDitunjukan.ToString & "', " & _
                  " '" & TxtAsuransi.Text.ToString & "', " & CekNilai(ReplaceString(TxtHargaAsuransi.Text.Replace("'", "''").Trim)) & ", " & _
                  "'" & CekNilai(TxtPremi.Text.Trim.Replace("'", "''").Replace(",", ".")) & "', '" & CekNilai(ReplaceString(TxtPolis.Text.Trim.Replace("'", "''"))) & "', " & _
                  "" & TotalAsuransi & ",'" & hfPaid.Value & "' , 'No', '" & Session("UserId") & "', 1, '" & KeteranganOngkosAngkut & "', '" & CodeCust & "', " & _
                  " '" & TxtNmBrg.Text.ToString.Trim.Replace("'", "''") & "', 1, '" & CekBulan(bulan) & "/" & tahun & "' );"
                    iddetail = 0

                    '<--- Check New Invoice Number Start With b or not ---->'

                    If Left(TxtNoInvoice.Text, 1) = "b" Or Left(TxtNoInvoice.Text, 1) = "B" Then
                        TotalBayar = 0
                        For i As Integer = 0 To Grid_Pilih_invoice.VisibleRowCount - 1
                            With Grid_Pilih_invoice
                                '<---- Check jenis satuan ----->'

                                Dim TOtalUkuran As String = ""

                                Select Case .GetRowValues(i, "JenisPembayaranI").ToString.ToUpper
                                    Case "TON"
                                        TOtalUkuran = (CDbl(System.Math.Round(.GetRowValues(i, "BeratTotalI").ToString / 1000, 3))).ToString
                                    Case "KUBIK"
                                        TOtalUkuran = .GetRowValues(i, "VolumeTotalI").ToString
                                    Case "UNIT"
                                        TOtalUkuran = .GetRowValues(i, "TotalQtyI").ToString
                                    Case "CONTAINER"
                                        TOtalUkuran = .GetRowValues(i, "TotalQtyI").ToString
                                    Case "SATUAN"
                                        TOtalUkuran = .GetRowValues(i, "TotalQtyI").ToString
                                End Select

                                TotalSatuan = CDbl(.GetRowValues(i, "TotalByrI").ToString)
                                TotalBayar = TotalBayar + TotalSatuan
                                iddetail = iddetail + 1
                                sqlstring &= "INSERT INTO InvoiceDetail(IDDetail, Mb_No,No_Invoice,QuotationDetailID, Hargatotal, Paid, DP, UserName, [status], TotalUkuran) VALUES " & _
                                     "('" & iddetail & "', '" & .GetRowValues(i, "Mb_NoI") & "','" & TxtNoInvoice.Text.ToString & "','" & .GetRowValues(i, "IDQuotationDetailI") & "', " & TotalSatuan & " " & _
                                     ", '" & hfPaid.Value & "' ,'No','" & Session("UserId") & "', 1, '" & TOtalUkuran & "'); "

                            End With
                        Next
                        result = SQLExecuteNonQuery(sqlstring, False, True)
                    Else
                        iddetail = 0
                        TotalBayar = 0
                        For i As Integer = 0 To Grid_Pilih_invoice.VisibleRowCount - 1
                            With Grid_Pilih_invoice

                                Dim TOtalUkuran As String = ""

                                Select Case .GetRowValues(i, "JenisPembayaranI").ToString.ToUpper
                                    Case "TON"
                                        TOtalUkuran = (CDbl(System.Math.Round(.GetRowValues(i, "BeratTotalI").ToString / 1000, 3))).ToString
                                    Case "KUBIK"
                                        TOtalUkuran = .GetRowValues(i, "VolumeTotalI").ToString
                                    Case "UNIT"
                                        TOtalUkuran = .GetRowValues(i, "TotalQtyI").ToString
                                    Case "CONTAINER"
                                        TOtalUkuran = .GetRowValues(i, "TotalQtyI").ToString
                                    Case "SATUAN"
                                        TOtalUkuran = .GetRowValues(i, "TotalQtyI").ToString
                                End Select

                                TotalSatuan = CDbl(.GetRowValues(i, "TotalByrI").ToString)
                                TotalBayar = TotalBayar + TotalSatuan
                                iddetail = iddetail + 1
                                sqlstring &= "INSERT INTO InvoiceDetail(IDDetail, Mb_No,No_Invoice,QuotationDetailID, Hargatotal, Paid, DP, UserName, [status], TotalUkuran) VALUES " & _
                                     "('" & iddetail & "', '" & .GetRowValues(i, "Mb_NoI") & "','" & TxtNoInvoice.Text.ToString & "','" & .GetRowValues(i, "IDQuotationDetailI") & "', " & TotalSatuan & " " & _
                                     ", '" & hfPaid.Value & "' ,'No','" & Session("UserId") & "', 1, '" & TOtalUkuran & "'); "

                            End With

                        Next
                        If sqlstring <> "" Then
                            result = SQLExecuteNonQuery(sqlstring)
                        End If

                    End If

                    'INSERT JOURNAL
                    InsertARHeader(RunNo, TxtNoInvoice.Text.ToString, DtInvoice.Text, TotalBayar, CodeCust.ToString, "NULL", Session("UserId").ToString)
                    InsertARDetailDebit(RunNo, NamaDitunjukan, TotalBayar, Session("UserId").ToString)
                    InsertARDetailKredit(RunNo, HFIDKapal.Value, TotalBayar, Session("UserId").ToString)
                    'END INSERT JOURNAL

                    If result <> "" Then
                        If ChkMiniByr.Checked Then
                            PembayaranMinimum() 'cek 5
                        End If

                        Grid_Pilih_invoice.DataSource = Nothing
                        Grid_Pilih_invoice.DataBind()
                        clear_all()
                        linfoberhasil.Text = "Simpan Berhasil"
                    End If
                    'END INSERT INVOICE
                End If
            End If




        Catch ex As Exception
            Throw New Exception("<b>Error Insert :</b>" & ex.ToString)
        End Try
    End Sub

    Private Function HitungBayar(ByVal mbno As String) As Double
        Try
            Dim zDT As New DataTable
            Dim harga As Double = 0

            sqlstring = "select A.mb_no, CAST( SUM(A.totalvolume) as decimal(30,3)) * A.Harga as hargaVolume , SUM(A.Totalberat) as TotalBerat, " & _
                        "SUM(A.totalberat)*A.Harga as hargaberat, " & _
                        "CAST(SUM(A.totalvolume) as decimal(30,3)) as TOtalVolume, A.NamaHarga, A.Nama_Barang, A.Harga " & _
                        "            from " & _
                        "( " & _
                        "select mbd.Mb_No, " & _
                        "((wd.panjang*wd.lebar*wd.tinggi) * mbd.quantity) as totalvolume, wd.Berat * mbd.Quantity as totalberat, " & _
                        "mh.NamaHarga,wd.Paid, qd.Harga,qd.nama_barang, " & _
                        "(SELECT CASE   " & _
                        "When wd.Container='true' then  " & _
                        "	(  " & _
                        "	((mbd.Quantity * qd.Harga) * ((100 - wd.Paid )/100 )) " & _
                        "	)  " & _
                        "when wd.Container='kubikasi' then  " & _
                        "(  " & _
                        "((cast(wd.Panjang * wd.Lebar*wd.Tinggi * mbd.Quantity as Decimal(30,3)) * qd.Harga)* ((100 - wd.Paid )/100 ))  " & _
                        ")  " & _
                        "else  " & _
                        "	(  " & _
                        "	(SELECT case   " & _
                        "		when (mh.NamaHarga = 'Kubik' or mh.NamaHarga = 'kubik') then " & _
                        "			(  " & _
                        "				((cast(wd.Panjang * wd.Lebar * wd.Tinggi * mbd.Quantity AS decimal(30,3)) * qd.Harga)* ((100 - wd.Paid )/100 )) " & _
                        "			)  " & _
                        "		when mh.NamaHarga = 'Ton' or mh.NamaHarga = 'ton' or mh.NamaHarga = 'Berat' or mh.NamaHarga ='berat' then  " & _
                        "			(  " & _
                        "				((cast((wd.Berat / 1000) * mbd.Quantity as Decimal(30,3)) * qd.Harga) * ((100 - wd.Paid )/100 ))  " & _
                        "			)  " & _
                        "		when mh.NamaHarga = 'Unit' or mh.NamaHarga = 'unit' then  " & _
                        "			(  " & _
                        "				(cast(mbd.Quantity * qd.Harga as Decimal(30,3)) * ((100 - wd.Paid )/100 ))  " & _
                        "			)  " & _
                        "		else(  " & _
                        "				((mbd.Quantity * qd.Harga)* ((100 - wd.Paid )/100 ))  " & _
                        "			)  " & _
                        "		end)  " & _
                        "	)  " & _
                        "end) as Total  " & _
                        "from V_MuatBarang_Detail mbd, V_Warehouse_Satuan wd ,QuotationDetail qd ,MasterHargaDefault mh   " & _
                        "        where (mbd.WarehouseHeaderID = wd.WarehouseItem_Code and mbd.Warehouse_Id  = wd.IDDetail) and  " & _
                        "mbd.Mb_No = '" & mbno & "' and  " & _
                        "(wd.Quotation_No = qd.Quotation_No And wd.QuotationDetailID = qd.IDDetail)  " & _
                        "and qd.SatuanID = mh.ID  " & _
                        "and  wd.status = 1 and qd.status <>0 and mh.status = 1) as A " & _
                        "GROUP BY A.NamaHarga, A.Paid, A.totalberat, A.Mb_No,A.Harga,A.NamaHarga, A.Nama_Barang,A.totalberat, A.Harga"
            zDT = SQLExecuteQuery(sqlstring).Tables(0)

            If zDT.Rows.Count > 0 Then
                For i As Integer = 0 To zDT.Rows.Count - 1
                    With zDT.Rows(i)
                        If .Item("NamaHarga").ToString.ToUpper = "KUBIK" Then
                            harga = harga + CDbl(.Item("hargaVolume"))
                        ElseIf .Item("NamaHarga").ToString.ToUpper = "TON" Then
                            harga = harga + CDbl(.Item("hargaberat"))
                        ElseIf .Item("NamaHarga").ToString.ToUpper = "CONTAINER" Then
                            harga = harga + CDbl(.Item("Harga"))
                        ElseIf .Item("NamaHarga").ToString.ToUpper = "UNIT" Then
                            harga = harga + CDbl(.Item("Harga"))
                        End If
                    End With
                Next
            End If

            Return harga
        Catch ex As Exception
            Throw New Exception(ex.ToString)
        End Try

    End Function

    Private Sub UPDATE(ByVal id As String)
        Try
            Dim pisah() As String
            Dim TotalAsuransi As Double = 0
            Dim totalbayar As Double = 0
            Dim NamaDitunjukan As String = ""
            Dim KotaDitunjukan As String = ""
            Dim KeteranganOngkosAngkut As String = ""
            Dim CodeCust As String = ""



            For i As Integer = 0 To Grid_Pilih_invoice.VisibleRowCount - 1
                totalbayar = totalbayar + CDbl(Grid_Pilih_invoice.GetRowValues(i, "TotalByrI").ToString)
            Next

            If ChkLainLain.Checked Then
                NamaDitunjukan = HFNmCust.Value.ToString.Trim.Replace("'", "''")
                KotaDitunjukan = TxtKotaLain.Text.ToString.Trim.Replace("'", "''")
                CodeCust = HFCodeCustLain.Value
            Else
                pisah = ddlDitunjukan.SelectedValue.ToString.Split("-")
                NamaDitunjukan = pisah(0)
                KotaDitunjukan = pisah(1)
                CodeCust = HFCodeCust.Value
            End If

            If ChkOngkos.Checked Then
                KeteranganOngkosAngkut = TxtOngkos.Text.Trim.Replace("'", "''") & "-" & TxtJumlahOngkosAngkutan.Text
                totalbayar = totalbayar + ReplaceString(TxtJumlahOngkosAngkutan.Text.Trim.Replace("'", "''"))
            End If

            sqlstring = "UPDATE InvoiceHeader " & _
                        "SET " & _
                        "InvoiceDate = '" & DtInvoice.Date & "', " & _
                        "DaerahDitujukan = '" & KotaDitunjukan & "', " & _
                        "Ditujukan = '" & NamaDitunjukan & "', " & _
                        "Total = " & totalbayar & ", " & _
                        "UserName = '" & Session("UserId").ToString & "', " & _
                        "Keterangan = '', " & _
                        "KeteranganOngkosAngkut = '" & KeteranganOngkosAngkut & "', " & _
                        "NamaBarang = '" & TxtNmBrg.Text.ToString.Trim.Replace("'", "''") & "', " & _
                        "LastModified = '" & Now.ToString & "' " & _
                        "WHERE No_Invoice = '" & id & "' AND [status] <> 0;"

            If chkAsuransi.Checked = True Then
                TotalAsuransi = (CDbl(ReplaceString(TxtHargaAsuransi.Text)) * (TxtPremi.Text.Replace(",", ".") / 100)) + CDbl(ReplaceString(TxtPolis.Text))
            Else
                TotalAsuransi = 0
            End If

            sqlstring &= "UPDATE InvoiceHeader " & _
                         "SET " & _
                         "NoAsuransi = '" & TxtAsuransi.Text.Trim.Replace("'", "''") & "', " & _
                         "HargaAsuransi = " & CekNilai(ReplaceString(TxtHargaAsuransi.Text.Replace("'", "''").Trim)) & ", " & _
                         "Premi = '" & CekNilai(TxtPremi.Text.Trim.Replace("'", "''").Replace(",", ".")) & "', " & _
                         "Polis = '" & CekNilai(ReplaceString(TxtPolis.Text.Trim.Replace("'", "''"))) & "', " & _
                         "TotalAsuransi = " & TotalAsuransi & " " & _
                         "WHERE No_Invoice = '" & id & "' AND [status] <> 0;"


            If Left(TxtNoInvoice.Text, 1) = "b" Or Left(TxtNoInvoice.Text, 1) = "B" Then
                result = SQLExecuteNonQuery(sqlstring, False, True)
            Else
                result = SQLExecuteNonQuery(sqlstring)
            End If

            TxtIndikator.Enabled = True

            If result <> "" Then
                If ChkMiniByr.Checked Then
                    PembayaranMinimum() 'cek 6
                End If
                clear_all()
                linfoberhasil.Visible = True
                linfoberhasil.Text = "Update Berhasil"
                LblBayar.Text = ""

            End If

        Catch ex As Exception
            Throw New Exception(ex.ToString)
        End Try
    End Sub

    Private Sub PembayaranMinimum()
        Try
            Dim hargaQuo As Double = 0
            Dim hargatotalMinByr As Double = 0

            hargaQuo = CDbl(Grid_Pilih_invoice.GetRowValues(0, "HargaI").ToString)
            hargatotalMinByr = hargaQuo * CDbl(txtMinByr.Text.Trim.Replace("'", "''").Replace(",", "."))

            If CDbl(Grid_Pilih_invoice.GetRowValues(0, "PaidI").ToString) > 0 Then
                hargatotalMinByr = (hargaQuo * CDbl(txtMinByr.Text.Trim.Replace("'", "''").Replace(",", "."))) * (CDbl(Grid_Pilih_invoice.GetRowValues(0, "PaidI").ToString) / 100)
            End If

            If ChkOngkos.Checked Then
                hargatotalMinByr = hargatotalMinByr + ReplaceString(TxtJumlahOngkosAngkutan.Text.Trim.Replace("'", "''"))
            End If

            sqlstring = "Update InvoiceHeader set Total = " & hargatotalMinByr & " where No_Invoice= '" & TxtNoInvoice.Text.ToString & "' and status = 1; "
            sqlstring &= "Update InvoiceHeader set Keterangan = 'PembayaranMinimum-' + '" & txtMinByr.Text.Trim.Replace("'", "''") & "' where No_Invoice= '" & TxtNoInvoice.Text.ToString & "' and status = 1 "

            SQLExecuteNonQuery(sqlstring)

        Catch ex As Exception
            Throw New Exception(ex.ToString)
        End Try
    End Sub

    Private Sub Delete(ByVal id As String)
        Try
            sqlstring = "UPDATE InvoiceHeader " & _
                        "SET " & _
                        "LastModified = '" & Now.ToString & "' " & _
                        ",[status] = 0 " & _
                        "WHERE No_Invoice = '" & id.ToString & "'; "
            sqlstring &= "UPDATE InvoiceDetail " & _
                         "SET " & _
                         "LastModified = '" & Now.ToString & "' " & _
                         ",[status] = 0 " & _
                         "WHERE No_Invoice = '" & id.ToString & "'; "

            result = SQLExecuteNonQuery(sqlstring)
            If result <> "" Then
                clear_all()
                linfoberhasil.Visible = True
                linfoberhasil.Text = "Delete Berhasil"
                load_grid_invoice_header()
            End If

        Catch ex As Exception
            Throw New Exception("<b>Error function Delete</b>" & ex.ToString)
        End Try
    End Sub
    Private Sub remove_pp(ByVal id As String, ByVal mbno As String)
        Dim sqlstring As String
        Dim result As String
        Try
            sqlstring = "Delete from InvoiceDetail where MuatBarangDetailID = '" & id.ToString & "' and Mb_No = '" & mbno.ToString & "'  "
            result = SQLExecuteNonQuery(sqlstring, True, False)

        Catch ex As Exception
            Throw New Exception("Error function remove_pp :" & ex.ToString)
        End Try
    End Sub

    Private Sub remove_item(ByVal namabarang As String)
        Try
            DT = CType(Session("GridDetailInvoice"), DataTable)

            For i As Integer = 0 To DT.Rows.Count - 1
                If DT.Rows(i).Item("Nama_BarangI").ToString = namabarang.ToString.Trim Then
                    DT.Rows(i).Delete()
                    Exit For
                End If
            Next

            Create_Session()
            Session("GridDetailInvoice") = DT
            Grid_Pilih_invoice.DataSource = DT
            Grid_Pilih_invoice.DataBind()

            Refresh_Grid()

        Catch ex As Exception
            Throw New Exception("<b>Error Remove item :</b>" & ex.ToString)
        End Try
    End Sub

    Private Sub remove_item_DB(ByVal ID As String)
        Try
            sqlstring = hfDel.Value
            sqlstring &= "UPDATE InvoiceDetail " & _
                         "SET " & _
                         "LastModified = '" & Now.ToString & "', " & _
                         "[status] = 0 " & _
                         "WHERE ID = " & ID.ToString & "; "
            hfDel.Value = sqlstring

            DT = CType(Session("GridDetailInvoice"), DataTable)

            For i As Integer = 0 To DT.Rows.Count - 1
                If DT.Rows(i).Item("IDI").ToString = ID Then
                    DT.Rows(i).Delete()
                    Exit For
                End If
            Next

            Session("GridDetailInvoice") = DT
            Grid_Pilih_invoice.DataSource = DT
            Grid_Pilih_invoice.DataBind()

            Refresh_Grid()
        Catch ex As Exception
            Throw New Exception("<b>Error remove_DB :</b>" & ex.ToString)
        End Try
    End Sub

    Private Sub kurangin_harga_total(ByVal id As String, ByVal mbno As String)
        Dim dt As DataTable
        Dim ds As DataSet
        Dim sqlstring As String
        Dim harga As Double
        Dim invoice As String
        Dim result As String
        Try
            sqlstring = "select id.Hargatotal,ih.No_Invoice from InvoiceDetail id left join InvoiceHeader ih on id.No_Invoice = ih.No_Invoice where id.MuatBarangDetailID = '" & id.ToString & "' and id.Mb_No = '" & mbno.ToString & "' and id.status = 1 "
            ds = SQLExecuteQuery(sqlstring)
            dt = ds.Tables(0)
            If dt.Rows.Count > 0 Then
                harga = CDbl(dt.Rows(0).Item("Hargatotal").ToString)
                invoice = dt.Rows(0).Item("No_Invoice").ToString
                sqlstring = "update InvoiceHeader set Total = Total - " & harga & "  where No_Invoice = '" & invoice.ToString & "' and status = 1  "
                result = SQLExecuteNonQuery(sqlstring)
            End If
        Catch ex As Exception
            Throw New Exception("Error kurangin_harga_total : " & ex.ToString)
        End Try

    End Sub

    Private Sub cek_status_muat_barang(ByVal ID As String)
        Dim mbno As String
        Dim Warehouse As String
        Dim mbstring As String
        Dim mbdt As DataTable
        Dim mbds As DataSet
        Dim str As String
        Dim ddt As DataTable
        Dim dds As DataSet
        Dim dstr As String
        Dim edt As DataTable
        Dim eds As DataSet
        Dim estr As String
        Dim idmb As String


        mbstring = "select ID,Mb_No,WarehouseheaderID from MuatBarang where Mb_No = '" & ID & "' "
        mbds = SQLExecuteQuery(mbstring, False)
        mbdt = mbds.Tables(0)
        mbno = mbdt.Rows(0).Item("Mb_No").ToString
        idmb = mbdt.Rows(0).Item("ID").ToString
        Warehouse = mbdt.Rows(0).Item("WarehouseheaderID").ToString

        dstr = " select ID from MuatBarangDetail where Mb_No = '" & mbno.ToString & "' and  (status= 5 or status = 7 )"
        dds = SQLExecuteQuery(dstr, False)
        ddt = dds.Tables(0)
        If ddt.Rows.Count <= 0 Then
            str = " UPDATE MuatBarang " & _
                        "SET " & _
                        "LastModified = '" & Now.ToString & "' " & _
                        ",[status] = 0 " & _
                        "WHERE Mb_No = '" & mbno.ToString & "' and status <> 0 ; "

            estr = " select ID from WarehouseDetail where WarehouseItem_Code =  '" & Warehouse & "' and (status = 1 or status = 7)"
            eds = SQLExecuteQuery(estr, False)
            edt = eds.Tables(0)
            If edt.Rows.Count <= 0 Then
                str &= " UPDATE WarehouseHeader " & _
                        "SET " & _
                        "LastModified = '" & Now.ToString & "' " & _
                        ",[status] = 0 " & _
                        "WHERE WarehouseItem_Code = '" & Warehouse.ToString & "' and status = 1; "
                'fstr = "select Quotation_No from Warehouseheader where WarehouseItem_Code = '" & Warehouse.ToString & "' and status = 1"
                'quotation = SQLExecuteScalar(fstr, False)

                'fstr = "select IDDetail from QuotationDetail where Quotation_No = '" & quotation.ToString & "' and (status = 1 or status = 5)"
                'fds = SQLExecuteQuery(fstr, False)
                'fdt = fds.Tables(0)

                'If fdt.Rows.Count <= 0 Then
                '    str &= " UPDATE MasterQuotation " & _
                '        "SET " & _
                '        "LastModified = '" & Now.ToString & "' " & _
                '        ",[status] = 0 " & _
                '        "WHERE Quotation_No = '" & quotation.ToString & "' and status = 1; "

                'End If
            End If
            result = SQLExecuteNonQuery(str, True, False)
        Else
            str = " UPDATE MuatBarang " & _
                     "SET " & _
                     "LastModified = '" & Now.ToString & "' " & _
                     ",[status] = 5 " & _
                     "WHERE ID = " & idmb.ToString & " and Mb_No = '" & mbno.ToString & "' ; "

            estr = " select ID from WarehouseDetail where WarehouseItem_Code =  '" & Warehouse & "' and status = 1"
            eds = SQLExecuteQuery(estr, False)
            edt = eds.Tables(0)
            If edt.Rows.Count > 0 Then
                str &= " UPDATE WarehouseHeader " & _
                        "SET " & _
                        "LastModified = '" & Now.ToString & "' " & _
                        ",[status] = 1 " & _
                        "WHERE WarehouseItem_Code =  '" & Warehouse.ToString & "' and status = 0; "
                'fstr = "select Quotation_No from Warehouseheader where WarehouseItem_Code = '" & Warehouse.ToString & "' and status = 1"
                'quotation = SQLExecuteScalar(fstr, False)

                'fstr = "select ID from QuotationDetail where Quotation_No = '" & quotation.ToString & "' and status = 1"
                'fds = SQLExecuteQuery(fstr, False)
                'fdt = fds.Tables(0)

                'If fdt.Rows.Count > 0 Then
                '    str &= " UPDATE MasterQuotation " & _
                '        "SET " & _
                '        "LastModified = '" & Now.ToString & "' " & _
                '        ",[status] = 1 " & _
                '        "WHERE Quotation_No = '" & quotation.ToString & "' and status = 0 ; "

                'End If
            End If
            result = SQLExecuteNonQuery(str, True, False)
        End If


    End Sub

    Private Sub ubah_status_0(ByVal IDDetail As String, ByVal IDHeader As String)

        Dim str As String
        Dim ddt As DataTable
        Dim dds As DataSet
        Dim dstr As String


        Try
            str = " UPDATE MuatBarangDetail " & _
                        "SET " & _
                        "LastModified = '" & Now.ToString & "' " & _
                        ",[status] = 0 " & _
                        "WHERE IDDetail = '" & IDDetail.ToString & "' and Mb_No = '" & IDHeader.ToString & "' ; "
            dstr = "Select mbd.Warehouse_Id,mb.WarehouseHeaderID from MuatBarangDetail mbd inner join MuatBarang mb on mbd.Mb_No = mb.Mb_No where mbd.IDDetail = " & IDDetail.ToString & " and mbd.Mb_No = '" & IDHeader.ToString & "' ;"
            dds = SQLExecuteQuery(dstr, False)
            ddt = dds.Tables(0)
            If ddt.Rows.Count > 0 Then
                str &= " UPDATE WarehouseDetail " & _
                        "SET " & _
                        "LastModified = '" & Now.ToString & "' " & _
                        ",[status] = 0 " & _
                        "WHERE IDDetail = '" & ddt.Rows(0).Item("Warehouse_Id").ToString & "' and " & _
                        " WarehouseItem_Code = '" & ddt.Rows(0).Item("WarehouseHeaderID").ToString & "' and Quantity = 0 ; "

                hasil = SQLExecuteNonQuery(str, True, False)
                'estr = "select Container,QuotationDetailID,Nama_Barang,Quotation_No from V_Warehouse_Satuan where IDDetail = '" & ddt.Rows(0).Item("Warehouse_Id").ToString & "' and " & _
                '            " WarehouseItem_Code = '" & ddt.Rows(0).Item("WarehouseHeaderID").ToString & "' and Quantity = 0;"
                'eds = SQLExecuteQuery(estr, False)
                'edt = eds.Tables(0)
                'If edt.Rows.Count > 0 Then
                '    If edt.Rows(0).Item("Container").ToString = "true" Then
                '        fstr = "select IDDetail from V_Warehouse_Satuan where QuotationDetailID = '" & edt.Rows(0).Item("QuotationDetailID").ToString & "' and  " & _
                '                   " Quotation_No = '" & edt.Rows(0).Item("Quotation_No").ToString & "' and status = 1"
                '        fds = SQLExecuteQuery(fstr, False)
                '        fdt = fds.Tables(0)
                '        If fdt.Rows.Count <= 1 Then
                '            str &= " UPDATE QuotationDetail " & _
                '                        "SET " & _
                '                        "LastModified = '" & Now.ToString & "' " & _
                '                        ",[status] = 0 " & _
                '                        "WHERE IDDetail = '" & edt.Rows(0).Item("QuotationDetailID").ToString & "' and Quotation_No = '" & edt.Rows(0).Item("Quotation_No").ToString & "' ;  "

                '            str &= " UPDATE ContainerHeader " & _
                '                        "SET " & _
                '                        "LastModified = '" & Now.ToString & "' " & _
                '                        ",[status] = 0 " & _
                '                        "WHERE ContainerCode = '" & edt.Rows(0).Item("Nama_Barang").ToString & "' and status <>0 ; "

                '            str &= " UPDATE ContainerDetail " & _
                '                        "SET " & _
                '                        "LastModified = '" & Now.ToString & "' " & _
                '                        ",[status] = 0 " & _
                '                        "WHERE ContainerCode = '" & edt.Rows(0).Item("Nama_Barang").ToString & "' and status <>0 ; "
                '        End If


                '    Else
                '        fstr = "select IDDetail from V_Warehouse_Satuan where QuotationDetailID = '" & edt.Rows(0).Item("QuotationDetailID").ToString & "' and " & _
                '                    " Quotation_No = '" & edt.Rows(0).Item("Quotation_No").ToString & "' and status = 1"
                '        fds = SQLExecuteQuery(fstr, False)
                '        fdt = fds.Tables(0)
                '        If fdt.Rows.Count <= 1 Then
                '            str &= " UPDATE QuotationDetail " & _
                '                        "SET " & _
                '                        "LastModified = '" & Now.ToString & "' " & _
                '                        ",[status] = 0 " & _
                '                        "WHERE IDDetail = '" & edt.Rows(0).Item("QuotationDetailID").ToString & "'  and Quotation_No = '" & edt.Rows(0).Item("Quotation_No").ToString & "' ; "
                '        End If
                '    End If
                'End If
            End If
        Catch ex As Exception
            Throw New Exception("Error Ubah status 0 :" & ex.ToString)
        End Try
    End Sub

    Private Sub ubah_status_1(ByVal IDDetail As String, ByVal IDHeader As String)

        Dim str As String
        Dim ddt As DataTable
        Dim dds As DataSet
        Dim dstr As String
        Dim hasil As String

        Try

            str = " UPDATE MuatBarangDetail " & _
                        "SET " & _
                        "LastModified = '" & Now.ToString & "' " & _
                        ",[status] = 5 " & _
                        "WHERE IDDetail = '" & IDDetail.ToString & "' and Mb_No = '" & IDHeader.ToString & "' ; "
            dstr = "Select mbd.Warehouse_Id ,mb.WarehouseHeaderID from MuatBarangDetail mbd inner join MuatBarang mb on mbd.Mb_No = mb.Mb_No where mbd.IDDetail = " & IDDetail.ToString & " and mbd.Mb_No = '" & IDHeader.ToString & "' ;"
            dds = SQLExecuteQuery(dstr, False)
            ddt = dds.Tables(0)
            If ddt.Rows.Count > 0 Then
                str &= " UPDATE WarehouseDetail " & _
                        "SET " & _
                        "LastModified = '" & Now.ToString & "' " & _
                        ",[status] = 1 " & _
                        "WHERE IDDetail = '" & ddt.Rows(0).Item("Warehouse_Id").ToString & "' and " & _
                        " WarehouseItem_Code = '" & ddt.Rows(0).Item("WarehouseHeaderID").ToString & "' ; "
                hasil = SQLExecuteNonQuery(str, True, False)

                'estr = "select Container,QuotationDetailID,Nama_Barang , Quotation_No from V_Warehouse_Satuan where IDDetail = '" & ddt.Rows(0).Item("Warehouse_Id").ToString & "' and" & _
                '            " WarehouseItem_Code = '" & ddt.Rows(0).Item("WarehouseHeaderID").ToString & "' ;"
                'eds = SQLExecuteQuery(estr, False)
                'edt = eds.Tables(0)
                'If edt.Rows.Count > 0 Then
                '    If edt.Rows(0).Item("Container").ToString = "true" Then
                '        str &= " UPDATE QuotationDetail " & _
                '        "SET " & _
                '        "LastModified = '" & Now.ToString & "' " & _
                '        ",[status] = 1 " & _
                '        "WHERE IDDetail = '" & edt.Rows(0).Item("QuotationDetailID").ToString & "' and Quotation_No = '" & edt.Rows(0).Item("Quotation_No").ToString & "' ; "

                '        str &= " UPDATE ContainerHeader " & _
                '        "SET " & _
                '        "LastModified = '" & Now.ToString & "' " & _
                '        ",[status] = 1 " & _
                '        "WHERE ContainerCode = '" & edt.Rows(0).Item("Nama_Barang").ToString & "' ; "

                '        str &= " UPDATE ContainerDetail " & _
                '        "SET " & _
                '        "LastModified = '" & Now.ToString & "' " & _
                '        ",[status] = 1 " & _
                '        "WHERE ContainerCode = '" & edt.Rows(0).Item("Nama_Barang").ToString & "'; "

                '    Else
                '        str &= " UPDATE QuotationDetail " & _
                '       "SET " & _
                '       "LastModified = '" & Now.ToString & "' " & _
                '       ",[status] = 1 " & _
                '       "WHERE IDDetail = '" & edt.Rows(0).Item("QuotationDetailID").ToString & "' and Quotation_No = '" & edt.Rows(0).Item("Quotation_No").ToString & "' ; "

                '    End If
                'End If
            End If
        Catch ex As Exception
            Throw New Exception("Error Ubah status 1 :" & ex.ToString)
        End Try
    End Sub

    Private Sub Show_Text_Box()
        TxtNamaKapal.Text = HFNamakapal.Value
        TxtNamaPengirim.Text = HFNamaPengirim.Value
        TxtTujuan.Text = HFTujuan.Value
        TxtPenerima.Text = HFPenerima.Value


    End Sub

    Private Function DDL_ditujukan_Selected(ByVal ID As String) As String
        Try
            Dim ditujukan As String = ""

            sqlstring = "SELECT Ditujukan, DaerahDitujukan " & _
                        "FROM " & _
                        "InvoiceHeader " & _
                        "WHERE ID = " & ID & " " & _
                        "AND [status] = 1"
            DS = SQLExecuteQuery(sqlstring)
            DT = DS.Tables(0)

            If DT.Rows.Count > 0 Then
                ditujukan = DT.Rows(0).Item("Ditujukan").ToString & " - " & DT.Rows(0).Item("DaerahDitujukan").ToString
            End If

            Return ditujukan
        Catch ex As Exception
            Throw New Exception("<b>Error DDL_Kapal_Selected :</b>" & ex.ToString)
        End Try
    End Function

    'Private Sub Ceklist(ByVal ID As String)
    '    Try
    '        Dim dtcek As DataTable
    '        Dim dscek As DataSet

    '        If Grid_Pilih_invoice.VisibleRowCount > 0 Then
    '            sqlstring = "SELECT ID, MuatBarangDetailID,Mb_No FROM InvoiceDetail WHERE No_Invoice = '" & ID & "' AND [status] = 1"
    '            dscek = SQLExecuteQuery(sqlstring)
    '            dtcek = dscek.Tables(0)

    '            Grid_Pilih_invoice.Selection.UnselectAll()

    '            For j As Integer = 0 To Grid_Pilih_invoice.VisibleRowCount - 1
    '                For k As Integer = 0 To dtcek.Rows.Count - 1
    '                    With Grid_Pilih_invoice
    '                        If .GetRowValues(j, "MuatBarangDetailIDI").ToString = dtcek.Rows(k).Item("MuatBarangDetailID").ToString And .GetRowValues(j, "Mb_NoI").ToString = dtcek.Rows(k).Item("Mb_No").ToString Then
    '                            .Selection.SelectRow(j)
    '                        End If
    '                    End With
    '                Next
    '            Next
    '        End If
    '    Catch ex As Exception
    '        Throw New Exception("<b>Error function ceklist :</b>" & ex.ToString)
    '    End Try
    'End Sub

    Private Function CekNilai(ByVal Value As String) As Double
        Try
            Dim hasil As Double

            If Value = "" Then
                hasil = 0

            Else
                hasil = Value
            End If

            Return hasil

        Catch ex As Exception
            Throw New Exception("<b>Error Function Cek Nilai :</b>" & ex.ToString)
        End Try
    End Function

#End Region

#Region "Validasi"

    Private Function cek_invoice_no(ByVal ID As String, ByVal mbno As String)
        Dim Nomor As String
        Dim STR As String
        Dim Nomornomor As String
        Nomor = ""
        Try
            Nomornomor = ""
            STR = "select No_Invoice from InvoiceDetail where MuatBarangDetailID = '" & ID.ToString & "' and Mb_No = '" & mbno.ToString & "' and (status = 1 or status = 7 or status = 10)"
            Nomornomor = SQLExecuteScalar(STR)
            If Nomornomor <> "" Then
                Nomor = Left(Nomornomor, 1)
            End If

        Catch ex As Exception
            Throw New Exception("error cek_invoice_no :" & ex.ToString)
        End Try


        Return Nomor
    End Function
    Private Function getInvoiceNo(ByVal ID As String, ByVal mbno As String)
        Dim Nomor As String
        Dim STR As String
        Dim Nomornomor As String
        Nomor = ""
        Try
            STR = "select No_Invoice from InvoiceDetail where MuatBarangDetailID = '" & ID.ToString & "' and Mb_No = '" & mbno.ToString & "' "
            Nomornomor = SQLExecuteScalar(STR)
            Nomor = Left(Nomornomor, 1)

        Catch ex As Exception
            Throw New Exception("error cek_invoice_no :" & ex.ToString)
        End Try


        Return Nomornomor
    End Function
    Private Function cek_invoice_now(ByVal ID As String, ByVal IDDetail As String, ByVal MbNo As String) As Boolean
        Dim str As String
        Dim dtcek As DataTable
        Dim dscek As DataSet

        Try
            str = "select status from InvoiceDetail where No_Invoice = '" & ID.ToString & " ' and MuatBarangDetailID = '" & IDDetail.ToString & "' and Mb_No = '" & MbNo.ToString & "' and (status = 1 or status = 7 or status =10) "
            dscek = SQLExecuteQuery(str)
            dtcek = dscek.Tables(0)

            If dtcek.Rows.Count > 0 Then
                Return True
            End If

        Catch ex As Exception
            Throw New Exception("error cek_invoice_now " & ex.ToString)
        End Try
        Return False
    End Function

    Private Function cek_invoice_status(ByVal id As String, ByVal mbno As String) As Integer
        Dim status As Integer
        Dim str As String

        Try
            status = 2
            str = "select status from InvoiceDetail where MuatBarangDetailID = '" & id.ToString & "' and Mb_No = '" & mbno.ToString & "' and (status = 1 or status = 7 or status = 10 ) "
            status = SQLExecuteScalar(str)
            Return status
        Catch ex As Exception
            Throw New Exception("Error cek_invoice_status " & ex.ToString)
        End Try
        Return status
    End Function

    Private Function cek_invoice_status_invoiceno(ByVal id As String, ByVal IDHeader As String, ByVal mb_no As String) As Integer
        Dim status As Integer
        Dim str As String

        Try
            status = 2
            str = "select status from InvoiceDetail where No_Invoice = '" & IDHeader.ToString & "' and MuatBarangDetailID = '" & id.ToString & "' and Mb_No = '" & mb_no.ToString & "' and (status =1 or status = 7 or status =10)"
            status = SQLExecuteScalar(str)
            Return status
        Catch ex As Exception
            Throw New Exception("Error cek_invoice_status " & ex.ToString)
        End Try
        Return status
    End Function
    Private Function cek_invoice_detail(ByVal ID As String, ByVal mb_no As String) As Boolean
        Dim str As String
        Dim dtcek As DataTable
        Dim dscek As DataSet

        Try
            str = "select status from InvoiceDetail where MuatBarangDetailID = '" & ID.ToString & "' and Mb_No = '" & mb_no.ToString & "' and (status = 1 or status = 7 or status = 10)"
            dscek = SQLExecuteQuery(str)
            dtcek = dscek.Tables(0)

            If dtcek.Rows.Count > 0 Then
                Return True
            End If
        Catch ex As Exception
            Throw New Exception("error cek_invoice_detail " & ex.ToString)
        End Try

        Return False
    End Function

    Private Function cek_grid_Child() As Boolean
        Try
            DT = CType(Session("GridDetailInvoice"), DataTable)
            DR = DT.NewRow
            If DT.Rows.Count > 0 Then
                For i As Integer = 0 To Grid_Pilih_invoice.VisibleRowCount - 1
                    If DT.Rows(i).Item("MuatBarangDetailIDI").ToString = HFMuatBarangDetailID.Value Then
                        clear_Label()
                        lInfo.Visible = True
                        lInfo.Text = " Item yang anda masukkan sudah ada , Harap pilih item lain"
                        Return False
                        Exit For
                    End If
                Next
            End If
            Return True
        Catch ex As Exception
            Throw New Exception("<b>Error Function cek grid child :</b>" & ex.ToString)
        End Try

    End Function

    Private Function validation_insert() As Boolean
        Dim str As String
        Dim dtcek As DataTable
        Dim dscek As DataSet


        DT = CType(Session("GridDetailInvoice"), DataTable)
        If DT.Rows.Count > 0 Then
            For i As Integer = 0 To Grid_Pilih_invoice.VisibleRowCount - 1
                str = "select MuatBarangDetailID from InvoiceDetail where status = 1"
                dscek = SQLExecuteQuery(str)
                dtcek = dscek.Tables(0)
                For e As Integer = 0 To dtcek.Rows.Count - 1
                    If DT.Rows(i).Item("MuatBarangDetailIDI").ToString = dtcek.Rows(e).Item("MuatBarangDetailID").ToString Then
                        clear_Label()
                        lInfo.Visible = True
                        lInfo.Text = " Item yang anda masukkan sudah ada , Harap pilih item lain.  Row < " & i & ">"
                        Return False
                        Exit For
                    End If
                Next
            Next
            Return True
        End If

    End Function

    Private Function validation_update() As Boolean

        Try
            If TxtNmBrg.Text.Trim = "" Then

                lInfo.Visible = True
                lInfo.Text = "Nama Barang harus diisi"
                Return False
            End If

            If ChkLainLain.Checked Then
                If HFCodeCustLain.Value = "" Then
                    lInfo.Visible = True
                    lInfo.Text = "Pilih nama customer"
                    Return False
                End If

                If TxtKotaLain.Text = "" Then
                    lInfo.Visible = True
                    lInfo.Text = "Kota Lain harus diisi"
                    Return False
                End If
            Else
                If ddlDitunjukan.SelectedIndex = 0 Then
                    clear_Label()
                    lInfo.Visible = True
                    lInfo.Text = "Pilih ditunjukan kepada"
                    Return False
                End If
            End If

            If ChkMiniByr.Checked = True Then

                'For i As Integer = 0 To Grid_Pilih_invoice.VisibleRowCount - 1
                '    If Grid_Pilih_invoice.Selection.IsRowSelected(i) = False Then
                '        lInfo.Visible = True
                '        lInfo.Text = "Harus memilih semua apabila menggunakan pembayaran minimum"
                '        Return False
                '    End If
                'Next

                If txtMinByr.Text.Trim.Replace("'", "") = "" Then
                    lInfo.Visible = True
                    lInfo.Text = "Harga Minimal Charge harus diisi"
                    Return False
                End If

                If IsNumeric(txtMinByr.Text.Trim.Replace("'", "").Replace(".", "").Replace(",", "")) = False Then
                    lInfo.Visible = True
                    lInfo.Text = "Harga Minimal Charge harus Numeric"
                    Return False
                End If
            End If

        Catch ex As Exception
            Throw New Exception("Error validation_update " & ex.ToString)
        End Try

        Return True
    End Function
    Private Function validation_addnew() As Boolean
        Dim str As String
        Dim dtcek As DataTable
        Dim dscek As DataSet
        Try

            clear_Label()

            If TxtNmBrg.Text.Trim = "" Then
                lInfo.Visible = True
                lInfo.Text = "Nama Barang harus Diisi"
                Return False
            End If

            If TxtIndikator.Text.Trim.Replace("'", "''") = "" Then
                lInfo.Visible = True
                lInfo.Text = "Masukan Indikator"
                Return False
            End If

            If ChkLainLain.Checked Then
                If HFCodeCustLain.Value = "" Then
                    lInfo.Visible = True
                    lInfo.Text = "Pilih nama customer"
                    Return False
                End If

                If TxtKotaLain.Text = "" Then
                    lInfo.Visible = True
                    lInfo.Text = "Kota Lain harus diisi"
                    Return False
                End If
            Else
                If ddlDitunjukan.SelectedIndex = 0 Then
                    clear_Label()
                    lInfo.Visible = True
                    lInfo.Text = "Pilih ditunjukan kepada"
                    Return False
                End If
            End If



            If ChkMiniByr.Checked = True Then

                If txtMinByr.Text.Trim.Replace("'", "") = "" Then
                    lInfo.Visible = True
                    lInfo.Text = "Harga Minimal Charge harus diisi"
                    Return False
                End If

                If IsNumeric(txtMinByr.Text.Trim.Replace("'", "").Replace(".", "").Replace(",", "")) = False Then
                    lInfo.Visible = True
                    lInfo.Text = "Harga Minimal Charge harus Numeric"
                    Return False
                End If
            End If

            If ChkOngkos.Checked = True Then
                If TxtOngkos.Text.Trim.Replace("'", "''") = "" Then
                    lInfo.Visible = True
                    lInfo.Text = "Keterangan Ongkos Harus Diisi"
                    Return False

                End If

                If TxtJumlahOngkosAngkutan.Text.Trim = "" Then
                    lInfo.Visible = True
                    lInfo.Text = "Harga Ongkos harus diisi"
                    Return False
                End If

                If IsNumeric(TxtJumlahOngkosAngkutan.Text.Trim.Replace("'", "").Replace(".", "").Replace(",", "")) = False Then
                    lInfo.Visible = True
                    lInfo.Text = "Harga Ongkos Angkutan harus Numeric"
                    Return False
                End If

            End If

            '***** EH00_20111022_01 START *****
            'str = "select QuotationDetailID,Mb_No from InvoiceDetail where status = 1"
            str = "select No_Invoice, QuotationDetailID,Mb_No from InvoiceDetail where status = 1"
            '***** EH00_20111022_01 END   *****
            dscek = SQLExecuteQuery(str)
            dtcek = dscek.Tables(0)

            If dtcek.Rows.Count = 0 Then
                Return True
            Else
                If Grid_Pilih_invoice.VisibleRowCount > 0 Then
                    'For j As Integer = 0 To Grid_Pilih_invoice.VisibleRowCount - 1
                    '    For k As Integer = 0 To dtcek.Rows.Count - 1
                    '        With Grid_Pilih_invoice
                    '            If .Selection.IsRowSelected(j) Then
                    '                If .GetRowValues(j, "IDQuotationDetailI") = dtcek.Rows(k).Item("QuotationDetailID").ToString And .GetRowValues(j, "Mb_NoI") = dtcek.Rows(k).Item("Mb_No").ToString Then

                    '                    lInfo.Visible = True
                    '                    lInfo.Text &= "Item dengan nama <b>" & .GetRowValues(j, "Nama_BarangI") & "</b> sudah ada<br>"
                    '                    Return False
                    '                End If
                    '            End If
                    '        End With
                    '    Next

                    'Next
                    For j As Integer = 0 To Grid_Pilih_invoice.VisibleRowCount - 1
                        For k As Integer = 0 To dtcek.Rows.Count - 1
                            If Grid_Pilih_invoice.GetRowValues(j, "IDQuotationDetailI") = dtcek.Rows(k).Item("QuotationDetailID").ToString And Grid_Pilih_invoice.GetRowValues(j, "Mb_NoI") = dtcek.Rows(k).Item("Mb_No").ToString Then
                                lInfo.Visible = True
                                '***** EH00_20111022_01 START *****
                                'lInfo.Text &= "Data Sudah ada"
                                lInfo.Text &= "Data Sudah ada. Invoice: " & dtcek.Rows(k).Item("No_Invoice").ToString & " - MB: " & dtcek.Rows(k).Item("Mb_No").ToString
                                '***** EH00_20111022_01 END   *****
                                Return False
                            End If
                        Next
                    Next

                End If
            End If




            Return True
        Catch ex As Exception

        End Try
    End Function
#End Region

#Region "Grid"

    Private Sub Grid_Invoice_Parent_RowCommand(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewRowCommandEventArgs) Handles Grid_Invoice_Parent.RowCommand
        Try
            clear_Label()
            clear_detail()
            Dim pisah() As String

            Select Case e.CommandArgs.CommandName
                Case "Edit"

                    HFMode.Value = "Update"
                    hfDel.Value = ""
                    hfPaid.Value = Grid_Invoice_Parent.GetRowValues(e.VisibleIndex, "Paid").ToString
                    HFID.Value = Grid_Invoice_Parent.GetRowValues(e.VisibleIndex, "ID").ToString
                    HFmuatBarangID.Value = Grid_Invoice_Parent.GetRowValues(e.VisibleIndex, "MuatBarangID")
                    DtInvoice.Date = CDate(Grid_Invoice_Parent.GetRowValues(e.VisibleIndex, "TglInvoice").ToString).ToString("dd MMMM yyyy")
                    HFIDKapal.Value = Grid_Invoice_Parent.GetRowValues(e.VisibleIndex, "IDKapal")
                    TxtNamaKapal.Text = Grid_Invoice_Parent.GetRowValues(e.VisibleIndex, "NamaKapal")
                    HFNamakapal.Value = Grid_Invoice_Parent.GetRowValues(e.VisibleIndex, "NamaKapal")
                    TxtNamaPengirim.Text = Grid_Invoice_Parent.GetRowValues(e.VisibleIndex, "Pengirim")
                    HFNamaPengirim.Value = Grid_Invoice_Parent.GetRowValues(e.VisibleIndex, "Pengirim")
                    TxtTujuan.Text = Grid_Invoice_Parent.GetRowValues(e.VisibleIndex, "Tujuan")
                    HFTujuan.Value = Grid_Invoice_Parent.GetRowValues(e.VisibleIndex, "Tujuan")
                    TxtPenerima.Text = Grid_Invoice_Parent.GetRowValues(e.VisibleIndex, "Penerima")
                    HFPenerima.Value = Grid_Invoice_Parent.GetRowValues(e.VisibleIndex, "Penerima")
                    TxtNoInvoice.Text = Grid_Invoice_Parent.GetRowValues(e.VisibleIndex, "No_Invoice").ToString
                    HFNoInvoice.Value = Grid_Invoice_Parent.GetRowValues(e.VisibleIndex, "No_Invoice").ToString
                    TxtNmBrg.Text = Grid_Invoice_Parent.GetRowValues(e.VisibleIndex, "NamaBarang").ToString
                    If Grid_Invoice_Parent.GetRowValues(e.VisibleIndex, "Keterangan").ToString.Contains("PembayaranMinimum") Then
                        pisah = Grid_Invoice_Parent.GetRowValues(e.VisibleIndex, "Keterangan").ToString.Split("-")

                        ChkMiniByr.Checked = True
                        txtMinByr.Text = pisah(1)
                    End If

                    If Grid_Invoice_Parent.GetRowValues(e.VisibleIndex, "KeteranganOngkosAngkut").ToString <> "" Then
                        ChkOngkos.Checked = True

                        pisah = Grid_Invoice_Parent.GetRowValues(e.VisibleIndex, "KeteranganOngkosAngkut").ToString.Split("-")
                        TxtOngkos.Text = pisah(0)
                        TxtJumlahOngkosAngkutan.Text = pisah(1)

                    End If
                    Load_Grid_Invoice_Child(Grid_Invoice_Parent.GetRowValues(e.VisibleIndex, "MuatBarangID").ToString, Grid_Invoice_Parent.GetRowValues(e.VisibleIndex, "Total").ToString)
                    load_DDL1(Grid_Invoice_Parent.GetRowValues(e.VisibleIndex, "MuatBarangID").ToString)
                    'ddlDitunjukan.SelectedValue = DDL_ditujukan_Selected((Grid_Invoice_Parent.GetRowValues(e.VisibleIndex, "ID").ToString)).ToString
                    'Ceklist(Grid_Invoice_Parent.GetRowValues(e.VisibleIndex, "No_Invoice").ToString)
                    'Cek_Asuransi(Grid_Invoice_Parent.GetRowValues(e.VisibleIndex, "No_Invoice").ToString)
                    TxtNoInvoice.Enabled = False
                    TxtIndikator.Enabled = False
                Case "Delete"
                    'Delete(Grid_Invoice_Parent.GetRowValues(e.VisibleIndex, "No_Invoice").ToString)
                    'Case "Close"
                    '    Close(Grid_Invoice_Parent.GetRowValues(e.VisibleIndex, "ID").ToString, Grid_Invoice_Parent.GetRowValues(e.VisibleIndex, "No_Invoice").ToString, Grid_Invoice_Parent.GetRowValues(e.VisibleIndex, "MuatBarangID").ToString)
                    '    CloseHeader(Grid_Invoice_Parent.GetRowValues(e.VisibleIndex, "ID").ToString, Grid_Invoice_Parent.GetRowValues(e.VisibleIndex, "No_Invoice").ToString, Grid_Invoice_Parent.GetRowValues(e.VisibleIndex, "MuatBarangID").ToString)
            End Select
        Catch ex As Exception
            Throw New Exception("<b>Error Row Command Parent :</b>" & ex.ToString)
        End Try
    End Sub

    Private Sub load_grid_invoice_header()
        Try

            Dim pdt As New DataTable

            pdt.Columns.Add(New DataColumn("ID", GetType(String)))
            pdt.Columns.Add(New DataColumn("No_Invoice", GetType(String)))
            pdt.Columns.Add(New DataColumn("MuatBarangID", GetType(String)))
            pdt.Columns.Add(New DataColumn("Pengirim", GetType(String)))
            pdt.Columns.Add(New DataColumn("Penerima", GetType(String)))
            pdt.Columns.Add(New DataColumn("NamaKapal", GetType(String)))
            pdt.Columns.Add(New DataColumn("IDKapal", GetType(String)))
            pdt.Columns.Add(New DataColumn("Tujuan", GetType(String)))
            pdt.Columns.Add(New DataColumn("DaerahDitujukan", GetType(String)))
            pdt.Columns.Add(New DataColumn("Total", GetType(Double)))
            pdt.Columns.Add(New DataColumn("TglInvoice", GetType(DateTime)))
            pdt.Columns.Add(New DataColumn("Ditujukan", GetType(String)))
            pdt.Columns.Add(New DataColumn("Paid", GetType(String)))
            pdt.Columns.Add(New DataColumn("Keterangan", GetType(String)))
            pdt.Columns.Add(New DataColumn("KeteranganOngkosAngkut", GetType(String)))
            pdt.Columns.Add(New DataColumn("NamaBarang", GetType(String)))
            pdt.Columns.Add(New DataColumn("YgInput", GetType(String)))

            sqlstring = "Select IH.ID, IH.MuatBarangID, IH.No_Invoice,mc.Nama_Customer, " & _
                        "mq.Penerima, mq.Tujuan, mb.Kapal, K.Nama_Kapal, IH.InvoiceDate, IH.Ditujukan, " & _
                        "IH.DaerahDitujukan, IH.Total,IH.Paid, IH.Keterangan, IH.KeteranganOngkosAngkut, IH.NamaBarang, IH.Username " & _
                        "from InvoiceHeader IH " & _
                        "left join MuatBarang MB ON IH.MuatbarangID = MB.Mb_No " & _
                        "left join MasterCustomer mc on mb.Customer_Id = mc.Kode_Customer " & _
                        "left join WarehouseHeader wh on mb.WarehouseHeaderID = wh.WarehouseItem_Code " & _
                        "left join MasterQuotation mq on wh.Quotation_No = mq.Quotation_No " & _
                        "left join Kapal K on mb.Kapal = K.IDDetail " & _
                        "where " & _
                        "mb.status = 5 " & _
                        "and wh.[status] <> 0 " & _
                        "and K.[status] = 1 " & _
                        "and (mq.[status] = 1 or mq.[status] = 2 ) " & _
                        "and IH.[status] = 1 and IH.DP = 'No' ORDER BY IH.ID Desc "
            DS = SQLExecuteQuery(sqlstring)
            DT = DS.Tables(0)

            If DT.Rows.Count > 0 Then
                For i As Integer = 0 To DT.Rows.Count - 1
                    With DT.Rows(i)
                        DR = pdt.NewRow
                        DR("ID") = .Item("ID").ToString
                        DR("No_Invoice") = .Item("No_Invoice").ToString
                        DR("MuatBarangID") = .Item("MuatBarangID")
                        DR("Pengirim") = .Item("Nama_Customer")
                        DR("Penerima") = .Item("Penerima")
                        DR("NamaKapal") = .Item("Nama_Kapal")
                        DR("IDKapal") = .Item("Kapal")
                        DR("DaerahDitujukan") = .Item("DaerahDitujukan")
                        DR("Tujuan") = .Item("Tujuan")
                        DR("Total") = .Item("Total")
                        DR("TglInvoice") = CDate(.Item("invoicedate").ToString).ToString("dd MMMM yyyy")
                        DR("Ditujukan") = .Item("Ditujukan")
                        DR("Paid") = .Item("Paid")
                        DR("Keterangan") = .Item("Keterangan")
                        DR("KeteranganOngkosAngkut") = .Item("KeteranganOngkosAngkut")
                        DR("NamaBarang") = .Item("NamaBarang")
                        DR("YgInput") = .Item("Username").ToString
                    End With
                    pdt.Rows.Add(DR)
                Next

                If Session("namaroles").ToString.Trim = "Admin" Or Session("namaroles").ToString.Trim = "Master Accounting" Or Session("namaroles").ToString.Trim = "Accounting" Then
                    Grid_Invoice_Parent.Columns("YgInput").Visible = True
                Else
                    Grid_Invoice_Parent.Columns("YgInput").Visible = False
                End If

                Session("Grid_Header_Invoice") = pdt
                Grid_Invoice_Parent.DataSource = pdt
                Grid_Invoice_Parent.KeyFieldName = "ID"
                Grid_Invoice_Parent.DataBind()

            Else
                Grid_Invoice_Parent.DataSource = Nothing
                Grid_Invoice_Parent.DataBind()
            End If


        Catch ex As Exception
            Throw New Exception("<b> Error load grid invoice : </b>" & ex.ToString)
        End Try
    End Sub

    Private Sub Load_Grid_Invoice_Child(ByVal MuatBarangID As String, ByVal totalHargaByr As String)
        Try

            Dim idt As New DataTable
            Dim cdt As New DataTable
            Dim NamaSatuan As String = ""
            Dim MBNO As String = MuatBarangID
            Dim NamaBarang As String = ""
            Dim totalbayar As Double = 0
            Dim Keterangan As String = ""
            Dim HargaSatuan As String = ""
            Dim PersenSisa As String = ""
            Dim PisahDataKet() As String = {}

            With idt.Columns
                .Add(New DataColumn("NoI", GetType(String)))
                .Add(New DataColumn("IDQuotationDetailI", GetType(String)))
                .Add(New DataColumn("Mb_NoI", GetType(String)))
                .Add(New DataColumn("Nama_BarangI", GetType(String)))
                .Add(New DataColumn("VolumeTotalI", GetType(Double)))
                .Add(New DataColumn("BeratTotalI", GetType(Double)))
                .Add(New DataColumn("HargaI", GetType(Double)))
                .Add(New DataColumn("JenisPembayaranI", GetType(String)))
                .Add(New DataColumn("TotalQtyI", GetType(String)))
                .Add(New DataColumn("TotalByrI", GetType(String)))
                .Add(New DataColumn("PaidI", GetType(String)))
            End With

            sqlstring = "select A.IDQuotationDetail, A.mb_no, A.namaquo, Cast(SUM(A.volumetotal) as Decimal(30,3)) as VolumeTotal, SUM(A.TotalBerat) as BeratTotal, A.Harga, " & _
                        "A.NamaHarga As JenisPembayaran, SUM(A.TotalQty) as TotalQty, SUM(A.Total) as TotalByr, A.Paid " & _
            "            FROM " & _
            "(select qd.IDDetail as IDQuotationDetail, mbd.Mb_No ,wd.Container,qd.Nama_Barang as namaquo, SUM(wd.Panjang * wd.Lebar * wd.Tinggi * mbd.Quantity) as volumetotal, " & _
            "SUM(wd.berat * mbd.Quantity) as TotalBerat, mh.NamaHarga,wd.Paid, qd.Harga, sum(mbd.quantity) as TotalQty, " & _
            "(SELECT CASE   " & _
            "When wd.Container='true' then " & _
            "	(  " & _
            "	    ((sum(mbd.Quantity) * qd.Harga) * ((100 - wd.Paid )/100 )) " & _
            "	)  " & _
            "when wd.Container='kubikasi' then  " & _
            "   (  " & _
            "       ((cast(wd.Panjang * wd.Lebar*wd.Tinggi * mbd.Quantity as Decimal(30,3)) * qd.Harga)* ((100 - wd.Paid )/100 )) " & _
            "   )  " & _
            "else " & _
            "	(  " & _
            "	(SELECT case " & _
            "		when (mh.NamaHarga = 'Kubik' or mh.NamaHarga = 'kubik') then  " & _
            "			(  " & _
            "				((cast(SUM(wd.Panjang * wd.Lebar * wd.Tinggi * mbd.Quantity) AS decimal(30,3)) * qd.Harga)* ((100 - wd.Paid )/100 ))  " & _
            "			)  " & _
            "		when mh.NamaHarga = 'Ton' or mh.NamaHarga = 'ton' or mh.NamaHarga = 'Berat' or mh.NamaHarga ='berat' then  " & _
            "			(  " & _
            "				((((SUM(mbd.Quantity) * wd.Berat) / 1000)  * qd.Harga) * ((100 - wd.Paid )/100 ))  " & _
            "			)  " & _
            "		when mh.NamaHarga = 'Unit' or mh.NamaHarga = 'unit' then  " & _
            "			(  " & _
            "				(cast(SUM(mbd.Quantity * qd.Harga) as Decimal(30,3)) * ((100 - wd.Paid )/100 ))  " & _
            "			)  " & _
            "		else( " & _
            "				((mbd.Quantity * qd.Harga)* ((100 - wd.Paid )/100 ))  " & _
            "			)  " & _
            "		end)  " & _
            "	)  " & _
            "end) as Total  " & _
            "from V_MuatBarang_Detail mbd, V_Warehouse_Satuan wd ,QuotationDetail qd ,MasterHargaDefault mh   " & _
            "        where (mbd.WarehouseHeaderID = wd.WarehouseItem_Code and mbd.Warehouse_Id  = wd.IDDetail) and " & _
            "mbd.Mb_No = '" & MuatBarangID & "' and  " & _
            "(wd.Quotation_No = qd.Quotation_No And wd.QuotationDetailID = qd.IDDetail)  " & _
            "and qd.SatuanID = mh.ID  " & _
            "and  wd.status = 1 and qd.status <>0 and mh.status = 1 " & _
            "GROUP BY mh.NamaHarga, mbd.Mb_No ,wd.Container, qd.Nama_Barang,qd.harga,wd.Paid, qd.Harga, " & _
            "wd.Panjang,wd.Lebar,wd.Tinggi,wd.Berat, mbd.Quantity, qd.IDDetail) as A " & _
            "GROUP BY A.NamaHarga, A.mb_no, A.namaquo, A.Harga, A.Paid, A.IDQuotationDetail "

            DS = SQLExecuteQuery(sqlstring)
            DT = DS.Tables(0)

            If DT.Rows.Count > 0 Then
                For i As Integer = 0 To DT.Rows.Count - 1
                    With DT.Rows(i)
                        DR = idt.NewRow
                        DR("NoI") = i + 1
                        DR("IDQuotationDetailI") = .Item("IDQuotationDetail").ToString
                        DR("Mb_NoI") = .Item("Mb_No").ToString
                        DR("Nama_BarangI") = .Item("namaquo").ToString
                        DR("VolumeTotalI") = .Item("VolumeTotal").ToString
                        DR("BeratTotalI") = .Item("BeratTotal").ToString
                        DR("HargaI") = CDbl(.Item("Harga").ToString)
                        DR("JenisPembayaranI") = .Item("JenisPembayaran").ToString
                        DR("TotalQtyI") = .Item("TotalQty").ToString
                        DR("TotalByrI") = .Item("TotalByr").ToString
                        If CDbl(.Item("Paid")) > 0 Then
                            DR("PaidI") = 100 - CDbl(.Item("Paid"))
                        Else
                            DR("PaidI") = .Item("Paid").ToString
                        End If
                        totalbayar = totalbayar + CDbl(.Item("TotalByr"))
                        idt.Rows.Add(DR)
                    End With
                Next
            End If
            HargaSatuan = DT.Rows(0).Item("Harga").ToString
            PersenSisa = 100 - CDbl(DT.Rows(0).Item("Paid").ToString)

            LblBayar.Text = UbahKoma(totalHargaByr)


            'untuk cek pembyran minumum
            'sqlstring = "SELECT Keterangan FROM InvoiceHeader " & _
            '            "WHERE MuatBarangID = '" & HFMBNO.Value & "' AND [status] <> 0 and DP = 'Yes' "
            'Keterangan = SQLExecuteScalar(sqlstring)

            'If Keterangan <> "" And Keterangan.Contains("PembayaranMinimum") Then
            '    PisahDataKet = Keterangan.Split("-")

            '    totalbayar = (CDbl(PisahDataKet(1)) * HargaSatuan) * (CDbl(PersenSisa) / 100)
            '    LblBayar.Text = UbahKoma(totalbayar)
            '    ChkMiniByr.Checked = True
            '    txtMinByr.Text = PisahDataKet(1)
            'End If

            Session("GridPilihInvoice") = idt
            Grid_Pilih_invoice.KeyFieldName = "NoI"
            Grid_Pilih_invoice.DataSource = idt
            Grid_Pilih_invoice.DataBind()



        Catch ex As Exception
            Throw New Exception("<b>Error load grid invoice child :</b>" & ex.ToString)
        End Try
    End Sub

    Private Sub load_grid_child(ByVal grid As ASPxGridView)
        Try
            Dim iDT As New DataTable
            Dim zDT As New DataTable
            Dim cDT As New DataTable
            Dim zDR As DataRow
            'Dim istr As String
            'Dim iDS As DataSet
            Dim NamaSatuan As String = ""
            Dim NamaBarang As String = ""

            With zDT.Columns
                .Add(New DataColumn("IDDetail", GetType(String)))
                .Add(New DataColumn("Paid", GetType(String)))
                .Add(New DataColumn("Hargatotal", GetType(Double)))
                .Add(New DataColumn("Harga", GetType(Double)))
                .Add(New DataColumn("Nama_Barang", GetType(String)))
                .Add(New DataColumn("JenisPembayaran", GetType(String)))
            End With


            sqlstring = "select distinct id.IDDetail, ID.Paid, ID.Hargatotal, QD.Harga, QD.Nama_Barang, MHD.NamaHarga as JenisPembayaran " & _
                        "FROM InvoiceDetail ID " & _
                        "LEFT JOIN MuatBarang MB on ID.Mb_No = MB.Mb_No " & _
                        "LEFT JOIN WarehouseHeader WH ON WH.WarehouseItem_Code = MB.WarehouseHeaderID " & _
                        "LEFT JOIN MasterQuotation MQ ON MQ.Quotation_No = WH.Quotation_No " & _
                        "JOIN QuotationDetail QD ON QD.Quotation_No= MQ.Quotation_No " & _
                        "JOIN MasterHargaDefault MHD ON QD.SatuanID = MHD.ID " & _
                        "where ID.No_Invoice = '" & grid.GetMasterRowFieldValues("No_Invoice") & "' " & _
                        "and QD.IDDetail = ID.quotationdetailID " & _
                        "and ID.[status] <> 0 " & _
                        "and MB.[status] <> 0 " & _
                        "and WH.[status] <> 0 " & _
                        "and QD.[status] <> 0 " & _
                        "and MHD.[status] <> 0 "

            DS = SQLExecuteQuery(sqlstring)
            DT = DS.Tables(0)

            If DT.Rows.Count > 0 Then
                For i As Integer = 0 To DT.Rows.Count - 1
                    With DT.Rows(i)
                        zDR = zDT.NewRow
                        zDR("IDDetail") = .Item("IDDetail").ToString
                        zDR("Paid") = .Item("Paid").ToString
                        zDR("Hargatotal") = CDbl(.Item("Hargatotal").ToString)
                        zDR("Harga") = CDbl(.Item("Harga").ToString)
                        zDR("Nama_Barang") = .Item("Nama_Barang").ToString
                        zDR("JenisPembayaran") = .Item("JenisPembayaran").ToString
                        zDT.Rows.Add(zDR)

                    End With
                Next
            End If

            grid.DataSource = zDT

        Catch ex As Exception
            Throw New Exception("Error Load Grid Child :<BR>" & ex.ToString)
        End Try
    End Sub

    Protected Sub Grid_Invoice_Child_DataSelect(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Call load_grid_child(TryCast(sender, ASPxGridView))
        Catch ex As Exception
            Response.Write("Error Load Grid Child DataSelect  :<BR>" & ex.ToString)
        End Try
    End Sub

    Private Sub Load_Pilih_Grid()
        Try
            Dim idt As New DataTable
            Dim cdt As New DataTable
            Dim NamaSatuan As String = ""
            Dim NamaBarang As String = ""
            Dim totalbayar As Double = 0
            Dim Keterangan As String = ""
            Dim HargaSatuan As String = ""
            Dim PersenSisa As String = ""
            Dim PisahDataKet() As String = {}

            With idt.Columns
                .Add(New DataColumn("NoI", GetType(String)))
                .Add(New DataColumn("IDQuotationDetailI", GetType(String)))
                .Add(New DataColumn("Mb_NoI", GetType(String)))
                .Add(New DataColumn("Nama_BarangI", GetType(String)))
                .Add(New DataColumn("VolumeTotalI", GetType(Double)))
                .Add(New DataColumn("BeratTotalI", GetType(Double)))
                .Add(New DataColumn("HargaI", GetType(Double)))
                .Add(New DataColumn("JenisPembayaranI", GetType(String)))
                .Add(New DataColumn("TotalQtyI", GetType(String)))
                .Add(New DataColumn("TotalByrI", GetType(Double)))
                .Add(New DataColumn("PaidI", GetType(String)))
            End With

            sqlstring = "select A.IDQuotationDetail, A.mb_no, A.namaquo, Cast(SUM(A.volumetotal) as Decimal(30,3)) as VolumeTotal, SUM(A.TotalBerat) as BeratTotal, A.Harga, " & _
                        "A.NamaHarga As JenisPembayaran, SUM(A.TotalQty) as TotalQty, SUM(A.Total) as TotalByr, A.Paid " & _
            "            FROM " & _
            "(select qd.IDDetail as IDQuotationDetail, mbd.Mb_No ,wd.Container,qd.Nama_Barang as namaquo, SUM(Cast(wd.Panjang * wd.Lebar * wd.Tinggi * mbd.Quantity as Decimal(30,3))) as volumetotal, " & _
            "SUM(wd.berat * mbd.Quantity) as TotalBerat, mh.NamaHarga,wd.Paid, qd.Harga, sum(mbd.quantity) as TotalQty, " & _
            "(SELECT CASE   " & _
            "When wd.Container='true' then " & _
            "	(  " & _
            "	    ((sum(mbd.Quantity) * qd.Harga) * ((100 - wd.Paid )/100 )) " & _
            "	)  " & _
            "when wd.Container='kubikasi' then  " & _
            "   (  " & _
            "       ((cast(wd.Panjang * wd.Lebar*wd.Tinggi * mbd.Quantity as Decimal(30,3)) * qd.Harga)* ((100 - wd.Paid )/100 )) " & _
            "   )  " & _
            "else " & _
            "	(  " & _
            "	(SELECT case " & _
            "		when (mh.NamaHarga = 'Kubik' or mh.NamaHarga = 'kubik') then  " & _
            "			(  " & _
            "				((SUM(Cast(wd.Panjang * wd.Lebar * wd.Tinggi * mbd.Quantity AS decimal(30,3))) * qd.Harga)* ((100 - wd.Paid )/100 ))  " & _
            "			)  " & _
            "		when mh.NamaHarga = 'Ton' or mh.NamaHarga = 'ton' or mh.NamaHarga = 'Berat' or mh.NamaHarga ='berat' then  " & _
            "			(  " & _
            "				((Cast(SUM((mbd.Quantity * wd.Berat) / 1000) as Decimal(30,3))  * qd.Harga) * ((100 - wd.Paid )/100 ))    " & _
            "			)  " & _
            "		when mh.NamaHarga = 'Unit' or mh.NamaHarga = 'unit' then  " & _
            "			(  " & _
            "				(SUM(mbd.Quantity) * qd.Harga) * ((100 - wd.Paid )/100 )   " & _
            "			)  " & _
            "		else( " & _
            "				(SUM(mbd.Quantity) * qd.Harga) * ((100 - wd.Paid )/100 )  " & _
            "			)  " & _
            "		end)  " & _
            "	)  " & _
            "end) as Total  " & _
            "from V_MuatBarang_Detail mbd, V_Warehouse_Satuan wd ,QuotationDetail qd ,MasterHargaDefault mh   " & _
            "        where (mbd.WarehouseHeaderID = wd.WarehouseItem_Code and mbd.Warehouse_Id  = wd.IDDetail) and " & _
            "mbd.Mb_No = '" & HFMBNO.Value.ToString & "' and  " & _
            "(wd.Quotation_No = qd.Quotation_No And wd.QuotationDetailID = qd.IDDetail)  " & _
            "and qd.SatuanID = mh.ID  " & _
            "and  wd.status = 1 and qd.status <>0 and mh.status = 1 " & _
            "GROUP BY mh.NamaHarga, mbd.Mb_No ,wd.Container, qd.Nama_Barang,qd.harga,wd.Paid, qd.Harga, " & _
            "wd.Panjang,wd.Lebar,wd.Tinggi,wd.Berat, mbd.Quantity, qd.IDDetail) as A " & _
            "GROUP BY A.NamaHarga, A.mb_no, A.namaquo, A.Harga, A.Paid, A.IDQuotationDetail "

            DS = SQLExecuteQuery(sqlstring)
            DT = DS.Tables(0)

            If DT.Rows.Count > 0 Then
                For i As Integer = 0 To DT.Rows.Count - 1
                    With DT.Rows(i)
                        DR = idt.NewRow
                        DR("NoI") = i + 1
                        DR("IDQuotationDetailI") = .Item("IDQuotationDetail").ToString
                        DR("Mb_NoI") = .Item("Mb_No").ToString
                        DR("Nama_BarangI") = .Item("namaquo").ToString
                        DR("VolumeTotalI") = .Item("VolumeTotal").ToString
                        DR("BeratTotalI") = .Item("BeratTotal").ToString
                        DR("HargaI") = CDbl(.Item("Harga").ToString)
                        DR("JenisPembayaranI") = .Item("JenisPembayaran").ToString
                        DR("TotalQtyI") = .Item("TotalQty").ToString
                        DR("TotalByrI") = CDbl(.Item("TotalByr").ToString)

                        If CDbl(.Item("Paid")) > 0 Then
                            DR("PaidI") = 100 - CDbl(.Item("Paid"))
                        Else
                            DR("PaidI") = .Item("Paid").ToString
                        End If
                        totalbayar = totalbayar + CDbl(.Item("TotalByr"))
                        idt.Rows.Add(DR)
                    End With
                Next
            End If

            HargaSatuan = DT.Rows(0).Item("Harga").ToString
            PersenSisa = 100 - CDbl(DT.Rows(0).Item("Paid").ToString)
            LblBayar.Text = UbahKoma(totalbayar)


            'untuk cek pembyran minumum dari Invoice DP
            sqlstring = "SELECT Keterangan FROM InvoiceHeader " & _
                        "WHERE MuatBarangID = '" & HFMBNO.Value & "' AND [status] <> 0 and DP = 'Yes' "
            Keterangan = SQLExecuteScalar(sqlstring)

            If Keterangan <> "" And Keterangan.Contains("PembayaranMinimum") Then
                PisahDataKet = Keterangan.Split("-")

                totalbayar = (CDbl(PisahDataKet(1)) * HargaSatuan) * (CDbl(PersenSisa) / 100)
                LblBayar.Text = UbahKoma(totalbayar)
                ChkMiniByr.Checked = True
                txtMinByr.Text = PisahDataKet(1)
            End If

            'fnish---------------------------------------------------------------------------

            Session("GridPilihInvoice") = idt
            Grid_Pilih_invoice.KeyFieldName = "NoI"
            Grid_Pilih_invoice.DataSource = idt
            Grid_Pilih_invoice.DataBind()

            sqlstring = "select distinct MBR.Depart_date FROM MuatBarangReport MBR " & _
                        "JOIN MBRDetail MBRD ON MBR.Mbr_No = MBRD.Mbr_No " & _
                        "where MBRD.Mb_No = '" & HFMBNO.Value & "' "
            HFTanggal.Value = SQLExecuteScalar(sqlstring)

            DtInvoice.Date = CDate(HFTanggal.Value).ToString("dd MMMM yyyy")

        Catch ex As Exception
            Throw New Exception("<b>Error Load function pilih : </b>" & ex.ToString)
        End Try
    End Sub

    Private Sub Cek_Asuransi(ByVal ID As String)
        Try
            sqlstring = "SELECT NoAsuransi, HargaAsuransi, Premi, Polis, TotalAsuransi " & _
                        "FROM InvoiceHeader Where No_Invoice = '" & ID & "' "
            DS = SQLExecuteQuery(sqlstring)
            DT = DS.Tables(0)

            If DT.Rows.Count > 0 Then
                If DT.Rows(0).Item("NoAsuransi") = "" Then
                    chkAsuransi.Checked = False
                Else
                    chkAsuransi.Checked = True
                    PanelAsuransi.Visible = True
                    TxtAsuransi.Text = DT.Rows(0).Item("NoAsuransi").ToString
                    TxtHargaAsuransi.Text = UbahKoma(DT.Rows(0).Item("HargaAsuransi").ToString)
                    TxtPremi.Text = DT.Rows(0).Item("Premi").ToString
                    TxtPolis.Text = UbahKoma(DT.Rows(0).Item("Polis").ToString)
                End If
            End If
        Catch ex As Exception
            Throw New Exception("<b>Error Cek_Asuransi :</b>" & ex.ToString)
        End Try
    End Sub

#End Region

#Region "DDL"
    Private Sub load_DDL1(ByVal id As String)
        Try
            sqlstring = "SELECT distinct (MC.Jenis_Perusahaan + ' ' + MC.Nama_Customer + ' - ' + MC.KotaDitunjukan) as dituju " & _
                        "FROM MuatBarang MB " & _
                        "LEFT JOIN MasterCustomer MC ON MB.Customer_Id = MC.Kode_Customer  " & _
                        "WHERE MB.Mb_No  = '" & id.ToString & "' " & _
                        "AND MB.[status] = 5 " & _
                        "AND MC.[status] = 1 " & _
                        "UNION " & _
                        "SELECT distinct (MQ.Penerima + ' - ' + MQ.Tujuan) as dituju " & _
                        "FROM MuatBarang MB " & _
                        "LEFT JOIN WarehouseHeader WHD ON MB.WarehouseHeaderID= WHD.WarehouseItem_Code " & _
                        "LEFT JOIN MasterQuotation MQ ON WHD.Quotation_No = MQ.Quotation_No " & _
                        "WHERE MB.Mb_No = '" & id.ToString & "' " & _
                        "AND MB.[status] = 5 " & _
                        "AND WHD.[status] = 1 " & _
                        "AND (mq.[status] = 1 or mq.[status] = 2 ) "
            DT = SQLExecuteQuery(sqlstring).Tables(0)
            ddlDitunjukan.Items.Clear()
            With ddlDitunjukan
                .DataSource = DT
                .DataTextField = "dituju"
                .DataValueField = "dituju"
                .DataBind()
            End With

            ddlDitunjukan.Items.Insert(0, "Pilih yang Dituju")
        Catch ex As Exception
            Throw New Exception("<b>Error LoadDDL :</b>" & ex.ToString)
        End Try
    End Sub
    Private Sub load_DDL()
        Try
            sqlstring = "SELECT distinct (MC.Nama_Customer + ',' + MC.Jenis_Perusahaan + ' - ' + MC.Area) as dituju " & _
                        "FROM MuatBarang MB " & _
                        "LEFT JOIN MasterCustomer MC on MB.Customer_Id = MC.Kode_Customer " & _
                        "WHERE MB.Mb_No = '" & HFMBNO.Value.ToString & "' " & _
                        "AND MB.[status] = 5 " & _
                        "AND MC.[status] = 1 " & _
                        "UNION " & _
                        "SELECT distinct (MQ.Penerima + ' - ' + MQ.Tujuan) as dituju " & _
                        "FROM MuatBarangDetail MBD " & _
                        "LEFT JOIN MuatBarang MB ON MBD.Mb_No = MB.Mb_No " & _
                        "LEFT JOIN WarehouseHeader WHD ON WHD.WarehouseItem_Code = MB.WarehouseHeaderID " & _
                        "LEFT JOIN MasterQuotation MQ ON WHD.Quotation_No = MQ.Quotation_No " & _
                        "LEFT JOIN MasterCustomer MC ON MQ.Customer_Id = MC.Kode_Customer  " & _
                        "WHERE MBD.Mb_No = '" & HFMBNO.Value.ToString & "' " & _
                        "AND MBD.[status] = 5 " & _
                        "AND MB.[status] = 5 " & _
                        "AND WHD.[status] <> 0 " & _
                        "AND (mq.[status] = 1 or mq.[status] = 2 ) " & _
                        "AND MC.[status] = 1 "
            DT = SQLExecuteQuery(sqlstring).Tables(0)
            ddlDitunjukan.Items.Clear()
            With ddlDitunjukan
                .DataSource = DT
                .DataTextField = "dituju"
                .DataValueField = "dituju"
                .DataBind()
            End With

            ddlDitunjukan.Items.Insert(0, "Pilih yang Dituju")
        Catch ex As Exception
            Throw New Exception("<b>Error LoadDDL :</b>" & ex.ToString)
        End Try
    End Sub

    Private Sub load_DDL_Edit(ByVal MBNO As String)
        Try
            sqlstring = "SELECT distinct (MC.Jenis_Perusahaan + ' ' + MC.Nama_Customer + ' - ' + MC.Area) as dituju " & _
                        "FROM MuatBarangDetail MBD " & _
                        "LEFT JOIN MuatBarang MB ON MBD.Mb_No = MB.Mb_No " & _
                        "LEFT JOIN WarehouseHeader WHD ON WHD.WarehouseItem_Code = MB.WarehouseHeaderID " & _
                        "LEFT JOIN MasterQuotation MQ ON WHD.Quotation_No = MQ.Quotation_No " & _
                        "LEFT JOIN MasterCustomer MC ON MQ.Customer_Id = MC.Kode_Customer  " & _
                        "WHERE MBD.Mb_No = '" & MBNO & "' " & _
                        "AND MBD.[status] = 5 " & _
                        "AND MB.[status] = 5 " & _
                        "AND WHD.[status] = 1 " & _
                        "AND MC.[status] = 1 " & _
                        "AND (mq.[status] = 1 or mq.[status] = 2 ) " & _
                        "UNION " & _
                        "SELECT distinct (MQ.Penerima + ' - ' + MQ.Tujuan) as dituju " & _
                        "FROM MuatBarangDetail MBD " & _
                        "LEFT JOIN MuatBarang MB ON MBD.Mb_No = MB.Mb_No " & _
                        "LEFT JOIN WarehouseHeader WHD ON WHD.WarehouseItem_Code = MB.WarehouseHeaderID " & _
                        "LEFT JOIN MasterQuotation MQ ON WHD.Quotation_No = MQ.Quotation_No " & _
                        "LEFT JOIN MasterCustomer MC ON MQ.Customer_Id = MC.Kode_Customer  " & _
                        "WHERE MBD.Mb_No = '" & MBNO & "' " & _
                        "AND MBD.[status] = 5 " & _
                        "AND MB.[status] = 5 " & _
                        "AND WHD.[status] = 1 " & _
                        "AND (mq.[status] = 1 or mq.[status] = 2 ) " & _
                        "AND MC.[status] = 1 "
            DT = SQLExecuteQuery(sqlstring).Tables(0)

            With ddlDitunjukan
                .DataSource = DT
                .DataTextField = "dituju"
                .DataValueField = "dituju"
                .DataBind()
            End With

            ddlDitunjukan.Items.Insert(0, "Pilih yang Dituju")
        Catch ex As Exception
            Throw New Exception("<b>Error LoadDDL :</b>" & ex.ToString)
        End Try
    End Sub

#End Region

End Class