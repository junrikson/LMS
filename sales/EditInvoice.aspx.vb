Imports System.Data
Imports System.Data.SqlClient
Imports DevExpress.Web.ASPxGridView

Partial Public Class EditInvoice
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
                Session("Grid_Header_Input_Invoice_Edit") = Nothing
                Session("GridPilihInvoiceEdit") = Nothing
                Load_DDLKapal()
                HFMode.Value = "Insert"

                HFmuatBarangID.Value = ""

                load_grid_invoice_header()
                TxtHargaInvoice.Attributes.Add("onkeyup", "changenumberformat('" & TxtHargaInvoice.ClientID & "')")
                TxtTotalUkuran.Attributes.Add("onkeyup", "changenumberformat('" & TxtTotalUkuran.ClientID & "')")
                TxtNamaCustomer.Attributes.Add("ReadOnly", "ReadOnly")
            End If


            If Not Session("Grid_Header_Input_Invoice_Edit") Is Nothing Then
                Grid_Invoice_Parent.DataSource = CType(Session("Grid_Header_Input_Invoice_Edit"), DataTable)
                Grid_Invoice_Parent.DataBind()
            End If

            If Not Session("GridPilihInvoiceEdit") Is Nothing Then
                Grid_Pilih_invoice.DataSource = CType(Session("GridPilihInvoiceEdit"), DataTable)
                Grid_Pilih_invoice.DataBind()
            End If

        Catch ex As Exception
            Response.Write("<b>Error Page Load :</b>" & ex.ToString)
        End Try
    End Sub

    

#End Region

#Region "Button"


    Private Sub btBatal_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btBatal.Click
        Try
            clear_all()
            clear_detail()
            clear_Label()
        Catch ex As Exception
            Response.Write("<b>Error btnbatal :</b>" & ex.ToString)
        End Try
    End Sub


    Private Sub btSimpan_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btSimpan.Click
        Try
            clear_Label()

            If HFMode.Value = "Update" Then
                If validation_update() Then
                    UPDATE(HFNoInvoice.Value)


                End If
            End If
            
        Catch ex As Exception
            Response.Write("<b>Error BtnSimpan</b>" & ex.ToString)
        End Try
    End Sub

    
#End Region

#Region "Method"




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

            TxtNoInvoice.Text = ""

            HFMode.Value = "Insert"
            HFInvoiceHeaderID.Value = ""
            HFID.Value = ""
            HFmuatBarangID.Value = ""
            HFInvoiceDetailID.Value = ""
            HFIDDetailInvoice.Value = ""

            HFTujuan.Value = ""

            HFMBNO.Value = ""

            HFNamakapal.Value = ""
            HFPembayaran.Value = ""
            HFNoInvoice.Value = ""
  
            TxtHargaInvoice.Text = ""
            HFCodeCust.Value = ""
            HFNamaCust.Value = ""
            TxtNamaCustomer.Text = ""
            DDLKapal.SelectedIndex = 0
            TxtCodeCustomer.Text = ""
            TxtDaerahDitujukan.Text = ""
            DtInvoice.Text = ""
        Catch ex As Exception
            Throw New Exception("<b>error function clear :</b>" & ex.ToString)
        End Try
    End Sub

    

    Private Sub clear_detail()
        Try
            TxtHargaSatuan.Text = ""
            TxtJenisBarang.Text = ""
            TxtJenisPembayaran.Text = ""
            TxtTotalUkuran.Text = ""

        Catch ex As Exception
            Throw New Exception("<b>Error function clear detail :</b>" & ex.ToString)
        End Try
    End Sub



    Private Sub UPDATE(ByVal id As String)
        Try
            'Dim pisah() As String
            Dim totalbayar As Double = 0
            Dim hargatotalDetail As Double = 0
            Dim NamaDitunjukan As String = ""

            totalbayar = ReplaceString(TxtHargaInvoice.Text.Trim.Replace("'", ""))

            NamaDitunjukan = Get_namaCustomer(TxtCodeCustomer.Text.Trim.Replace("'", "''"))

            If NamaDitunjukan = "" Then
                lInfo.Text = "Code Customer Salah"
                lInfo.Visible = True
                Return
            End If


            sqlstring = "UPDATE InvoiceHeader " & _
                        "SET " & _
                        "No_Invoice = '" & TxtNoInvoice.Text.ToString.Replace("'", "").Trim & "', " & _
                        "InvoiceDate = '" & DtInvoice.Date & "', " & _
                        "KapalID = '" & DDLKapal.SelectedValue & "', " & _
                        "CodeCust = '" & TxtCodeCustomer.Text.Trim.Replace("'", "") & "', " & _
                        "Ditujukan = '" & NamaDitunjukan & "', " & _
                        "DaerahDitujukan = '" & TxtDaerahDitujukan.Text.Trim.Replace("'", "") & "', " & _
                        "Total = " & totalbayar & ", " & _
                        "UpdatedBy = '" & Session("UserId").ToString & "', " & _
                        "LastModified = '" & Now.ToString & "' " & _
                        "WHERE No_Invoice = '" & id & "' AND [status] <> 0;"

            For i As Integer = 0 To Grid_Pilih_invoice.VisibleRowCount - 1
                With Grid_Pilih_invoice
                    sqlstring &= "UPDATE InvoiceDetail " & _
                                 "SET " & _
                                 "TotalUkuran  = '" & .GetRowValues(i, "TotalUkuranI").ToString & "', " & _
                                 "HargaTotal = " & .GetRowValues(i, "TotalByrI") & " " & _
                                 "WHERE No_Invoice = '" & id & "' AND IDDetail = '" & .GetRowValues(i, "NoI") & "' AND [status] <> 0 "
                End With
            Next


            If Left(TxtNoInvoice.Text, 1) = "b" Or Left(TxtNoInvoice.Text, 1) = "B" Then
                result = SQLExecuteNonQuery(sqlstring, False, True)
            Else
                result = SQLExecuteNonQuery(sqlstring)
            End If

            If result <> "" Then
                clear_all()
                linfoberhasil.Visible = True
                linfoberhasil.Text = "Update Berhasil"
                load_grid_invoice_header()
                Grid_Pilih_invoice.DataSource = Nothing
                Grid_Pilih_invoice.DataBind()
            End If

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

    Private Sub load_grid(ByVal No_Invoice As String)
        Try
            Dim idt As New DataTable

            With idt.Columns
                .Add(New DataColumn("NoI", GetType(String)))
                .Add(New DataColumn("IDQuotationDetailI", GetType(String)))
                .Add(New DataColumn("TotalUkuranI", GetType(String)))
                .Add(New DataColumn("Nama_BarangI", GetType(String)))
                .Add(New DataColumn("HargaI", GetType(Double)))
                .Add(New DataColumn("JenisPembayaranI", GetType(String)))
                .Add(New DataColumn("TotalByrI", GetType(String)))
                .Add(New DataColumn("PaidI", GetType(String)))
            End With

            sqlstring = "SELECT ID.IDDetail,ID.QuotationDetailID,ID.TotalUkuran,ID.HargaTotal, QD.Harga, MHD.NamaHarga, QD.Nama_Barang, ID.Paid FROM InvoiceDetail ID  " & _
                        "JOIN MuatBarang MB ON ID.Mb_No= MB.Mb_No " & _
                        "JOIN Warehouseheader WH ON  MB.WareHouseHeaderID = WH.WarehouseItem_Code " & _
                        "JOIN MasterQuotation MQ ON WH.Quotation_No = MQ.Quotation_No " & _
                        "JOIN QuotationDetail QD ON (ID.QuotationDetailID = QD.IDDetail AND MQ.Quotation_No = Qd.Quotation_No) " & _
                        "JOIN MasterHargaDefault MHD ON QD.SatuanID = MHD.ID " & _
                        "WHERE ID.No_Invoice = '" & No_Invoice.ToString & "' AND ID.status <> 0 AND MB.[status] <> 0 and WH.status<> 0 AND MHD.status <> 0 "
            DT = SQLExecuteQuery(sqlstring).Tables(0)

            If DT.Rows.Count > 0 Then
                For i As Integer = 0 To DT.Rows.Count - 1
                    With DT.Rows(i)
                        DR = idt.NewRow
                        DR("NoI") = .Item("IDDetail").ToString
                        DR("IDQuotationDetailI") = .Item("QuotationDetailID").ToString
                        DR("Nama_BarangI") = .Item("Nama_Barang").ToString
                        DR("TotalUkuranI") = .Item("TotalUkuran").ToString
                        DR("HargaI") = CDbl(.Item("Harga").ToString)
                        DR("JenisPembayaranI") = .Item("NamaHarga").ToString
                        DR("TotalByrI") = .Item("HargaTotal").ToString
                        DR("PaidI") = .Item("Paid").ToString
                        idt.Rows.Add(DR)
                    End With
                Next
            End If

            Session("GridPilihInvoiceEdit") = idt
            Grid_Pilih_invoice.KeyFieldName = "NoI"
            Grid_Pilih_invoice.DataSource = idt
            Grid_Pilih_invoice.DataBind()

        Catch ex As Exception
            Throw New Exception(ex.ToString)
        End Try
    End Sub

    Private Sub Load_DDLKapal()
        Try
            sqlstring = " SELECT IDDetail As ID ,Nama_Kapal from Kapal where [status] = 1 order by Nama_Kapal"
            Dim dt As DataTable = SQLExecuteQuery(sqlstring).Tables(0)
            With DDLKapal
                .DataSource = dt
                .DataTextField = "Nama_Kapal"
                .DataValueField = "ID"
                .DataBind()
            End With
            DDLKapal.Items.Insert(0, "-Pilih Kapal-")
        Catch ex As Exception
            Throw New Exception(ex.ToString)
        End Try
    End Sub

    Private Function Get_namaCustomer(ByVal codeCust As String) As String
        Try

            Dim hsl As String

            sqlstring = "select Nama_Customer + ',' + Jenis_Perusahaan as NamaCustomer " & _
                        "FROM MasterCustomer " & _
                        "WHERE Kode_Customer = '" & codeCust & "' " & _
                        "AND [status] = 1"
            hsl = SQLExecuteScalar(sqlstring)

            If hsl <> "" Then
                hsl = hsl.Trim
            Else
                hsl = ""
            End If

            Return hsl
        Catch ex As Exception
            Throw New Exception(ex.ToString)
        End Try
    End Function

#End Region

#Region "Validasi"

    Private Function validation_update() As Boolean

        Try

            Dim id As String
            Dim pisah() As String
            pisah = TxtNoInvoice.Text.Trim.Split("/")
            clear_Label()


            If TxtNoInvoice.Text.Trim.Replace("'", "''") = "" Then
                lInfo.Visible = True
                lInfo.Text = "No Invoice Harus Diisi"
                Return False
            End If

            If pisah.Length < 5 Then
                lInfo.Visible = True
                lInfo.Text = "Penulisan No Invoice Salah"
                Return False
            End If

            sqlstring = "SELECT ID FROM Kapal WHERE Alias_Kapal = '" & pisah(2) & "' AND status <> 0"
            id = SQLExecuteScalar(sqlstring)

            If id = "" Then
                lInfo.Visible = True
                lInfo.Text = "Kode Kapal Salah"
                Return False
            End If

            


            If TxtHargaInvoice.Text.Trim = "" Then
                lInfo.Visible = True
                lInfo.Text = "Harga Invoice Harus diisi"
                Return False
            End If

            If IsNumeric(ReplaceString(TxtHargaInvoice.Text.Trim)) = False Then
                lInfo.Visible = True
                lInfo.Text = "Harga Invoice Harus Angka"
                Return False
            End If

            If DtInvoice.Text = "" Then
                lInfo.Visible = True
                lInfo.Text = "Tanggal Harus Diisi"
                Return False
            End If

            If DDLKapal.SelectedIndex = 0 Then
                lInfo.Visible = True
                lInfo.Text = "Kapal Harus dipilih"
                Return False
            End If

            If TxtCodeCustomer.Text.Trim = "" Then
                lInfo.Visible = True
                lInfo.Text = "Customer Harus Diisi"
                Return False
            End If

            If TxtDaerahDitujukan.Text.Trim.Replace("'", "") = "" Then
                lInfo.Visible = True
                lInfo.Text = "Daerah Ditujukan Harus diisi"
                Return False
            End If


            Return True
        Catch ex As Exception
            Throw New Exception("Error validation_update " & ex.ToString)
        End Try


    End Function

    Private Function validation_addnew() As Boolean

        Try

            If TxtTotalUkuran.Text.Trim = "" Then
                lInfo.Text = "Total Ukuran kosong"
                lInfo.Visible = True
                Return False
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
            hfModeItem.Value = ""
            Select Case e.CommandArgs.CommandName
                Case "Edit"

                    HFMode.Value = "Update"
                    HFID.Value = Grid_Invoice_Parent.GetRowValues(e.VisibleIndex, "ID").ToString
                    HFmuatBarangID.Value = Grid_Invoice_Parent.GetRowValues(e.VisibleIndex, "MuatBarangID")
                    HFCodeCust.Value = Grid_Invoice_Parent.GetRowValues(e.VisibleIndex, "CodeCust").ToString
                    TxtNoInvoice.Text = Grid_Invoice_Parent.GetRowValues(e.VisibleIndex, "No_Invoice").ToString
                    HFNoInvoice.Value = Grid_Invoice_Parent.GetRowValues(e.VisibleIndex, "No_Invoice").ToString
                    TxtHargaInvoice.Text = UbahKoma(Grid_Invoice_Parent.GetRowValues(e.VisibleIndex, "Total").ToString)
                    TxtCodeCustomer.Text = Grid_Invoice_Parent.GetRowValues(e.VisibleIndex, "CodeCust").ToString
                    DtInvoice.Text = CDate(Grid_Invoice_Parent.GetRowValues(e.VisibleIndex, "TglInvoice")).ToString("dd MMMM yyyy")
                    DDLKapal.SelectedValue = Grid_Invoice_Parent.GetRowValues(e.VisibleIndex, "IDKapal").ToString
                    TxtNamaCustomer.Text = Grid_Invoice_Parent.GetRowValues(e.VisibleIndex, "Ditujukan").ToString
                    TxtDaerahDitujukan.Text = Grid_Invoice_Parent.GetRowValues(e.VisibleIndex, "DaerahDitujukan").ToString
             
                    load_grid(Grid_Invoice_Parent.GetRowValues(e.VisibleIndex, "No_Invoice").ToString)
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
            pdt.Columns.Add(New DataColumn("NamaKapal", GetType(String)))
            pdt.Columns.Add(New DataColumn("IDKapal", GetType(String)))
            pdt.Columns.Add(New DataColumn("Total", GetType(Double)))
            pdt.Columns.Add(New DataColumn("TglInvoice", GetType(DateTime)))
            pdt.Columns.Add(New DataColumn("Ditujukan", GetType(String)))
            pdt.Columns.Add(New DataColumn("DaerahDitujukan", GetType(String)))
            pdt.Columns.Add(New DataColumn("CodeCust", GetType(String)))
            pdt.Columns.Add(New DataColumn("UpdatedBy", GetType(String)))

            sqlstring = "Select IH.ID, IH.MuatBarangID, IH.No_Invoice,IH.CodeCust, " & _
                        " IH.KapalID, K.Nama_Kapal, IH.InvoiceDate, IH.Ditujukan, IH.DaerahDitujukan, IH.Total,IH.Paid, IH.Keterangan, IH.UpdatedBy " & _
                        "from InvoiceHeader IH " & _
                        "left join Kapal K on IH.KapalID = K.IDDetail " & _
                        "where " & _
                        "K.[status] = 1 " & _
                        "and IH.[status] <> 0 and IH.[statuspembayaran] = 'belum bayar' "
            DS = SQLExecuteQuery(sqlstring)
            DT = DS.Tables(0)

            If DT.Rows.Count > 0 Then
                For i As Integer = 0 To DT.Rows.Count - 1
                    With DT.Rows(i)
                        DR = pdt.NewRow
                        DR("ID") = .Item("ID").ToString
                        DR("No_Invoice") = .Item("No_Invoice").ToString
                        DR("MuatBarangID") = .Item("MuatBarangID")
                        DR("NamaKapal") = .Item("Nama_Kapal")
                        DR("IDKapal") = .Item("KapalID")
                        DR("DaerahDitujukan") = .Item("DaerahDitujukan").ToString
                        DR("Total") = .Item("Total")
                        DR("TglInvoice") = CDate(.Item("invoicedate").ToString).ToString("dd MMMM yyyy")
                        DR("Ditujukan") = .Item("Ditujukan")
                        DR("CodeCust") = .Item("CodeCust")
                        DR("UpdatedBy") = .Item("UpdatedBy").ToString
                    End With
                    pdt.Rows.Add(DR)
                Next

                Session("Grid_Header_Input_Invoice_Edit") = pdt
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


            'sqlstring = "select id.ID, wd.Container,wd.Nama_Satuan,id.Hargatotal,mh.NamaHarga,wd.Nama_Barang,id.Paid,qd.Nama_Barang as namaquo " & _
            '                    "from InvoiceDetail id ,V_MuatBarang_Detail mbd, V_Warehouse_Satuan wd ,QuotationDetail qd ,MasterHargaDefault mh " & _
            '                     "           where(ID.MuatBarangDetailID = mbd.IDDetail And ID.Mb_No = mbd.Mb_No) " & _
            '                    "and (mbd.Warehouse_Id= wd.IDDetail and mbd.WarehouseHeaderID = wd.WarehouseItem_Code) and " & _
            '                    "( wd.Quotation_No = qd.Quotation_No and wd.QuotationDetailID= qd.IDDetail ) " & _
            '                    "and qd.SatuanID = mh.ID  " & _
            '                    "and  id.No_Invoice = '" & grid.GetMasterRowFieldValues("No_Invoice") & "' and " & _
            '                    " wd.status <> 0 and qd.status <>0 and mh.status = 1 and id.status = 1 and mbd.status<>0 "

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



#End Region


    Private Sub Grid_Pilih_invoice_RowCommand(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewRowCommandEventArgs) Handles Grid_Pilih_invoice.RowCommand
        Try
            Select Case e.CommandArgs.CommandName
                Case "Edit"
                    hfModeItem.Value = "UpdateItem"
                    HFIDDetailInvoice.Value = Grid_Pilih_invoice.GetRowValues(e.VisibleIndex, "NoI").ToString
                    TxtHargaSatuan.Text = Grid_Pilih_invoice.GetRowValues(e.VisibleIndex, "HargaI").ToString
                    TxtJenisBarang.Text = Grid_Pilih_invoice.GetRowValues(e.VisibleIndex, "Nama_BarangI").ToString
                    TxtJenisPembayaran.Text = Grid_Pilih_invoice.GetRowValues(e.VisibleIndex, "JenisPembayaranI").ToString
                    If Grid_Pilih_invoice.GetRowValues(e.VisibleIndex, "TotalUkuranI").ToString = "" Then
                        TxtTotalUkuran.Text = ""
                    Else
                        TxtTotalUkuran.Text = UbahKomaDlmUkuran(Grid_Pilih_invoice.GetRowValues(e.VisibleIndex, "TotalUkuranI").ToString)

                    End If
            End Select
        Catch ex As Exception
            Throw New Exception(ex.ToString)
        End Try
    End Sub

    Private Sub BtnTambah_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnTambah.Click
        Try
            If hfModeItem.Value = "UpdateItem" Then
                If validation_addnew() Then
                    EditData()
                End If
            End If
            
        Catch ex As Exception
            Throw New Exception(ex.ToString)

        End Try
    End Sub

    Private Sub EditData()
        Try
            Dim DT As DataTable

            DT = CType(Session("GridPilihInvoiceEdit"), DataTable)

            For i As Integer = 0 To DT.Rows.Count - 1
                If DT.Rows(i).Item("NoI").ToString = HFIDDetailInvoice.Value Then
                    DT.Rows(i).Item("TotalUkuranI") = UbahKomaJdTitik(TxtTotalUkuran.Text.Trim.Replace("'", ""))
                    DT.Rows(i).Item("TotalByrI") = UbahKomaJdTitik(TxtTotalUkuran.Text.Trim.Replace("'", "")) * CDbl(DT.Rows(i).Item("HargaI").ToString)
                End If
            Next

            Session("GridPilihInvoiceEdit") = DT
            Grid_Pilih_invoice.DataSource = DT
            Grid_Pilih_invoice.DataBind()
            HFIDDetailInvoice.Value = ""
            clear_detail()

            hfModeItem.Value = ""
        Catch ex As Exception
            Throw New Exception(ex.ToString)
        End Try
    End Sub
End Class
