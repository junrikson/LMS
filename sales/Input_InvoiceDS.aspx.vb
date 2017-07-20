Imports System.Data
Imports System.Data.SqlClient
Imports DevExpress.Web.ASPxGridView
Partial Public Class Input_InvoiceDS
    Inherits System.Web.UI.Page
    Private DT As DataTable
    Private aDT As DataTable
    Private DS As DataSet
    Private DR As DataRow
    Private sqlstring As String
    Private result As String
    Private hasil As String
    Dim TOtalByr As String

#Region "PAGE"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            If Session("UserID") = Nothing Then
                FormsAuthentication.SignOut()
                Response.Redirect("Index.aspx")
            End If

            If Not Page.IsPostBack Then
                Session("Grid_Header_DS_Invoice") = Nothing
                HFMode.Value = "Insert"
                HFModeItem.Value = "Insert"
                load_DDLSatuanBayar()
                load_DDLSatuanBrg()
                Load_DDLKapal()
                TxtAsuransi.Visible = True
                create_session()
                load_grid_invoice_header()
                TxtHargaByr.Attributes.Add("onkeyup", "changenumberformat('" & TxtHargaByr.ClientID & "')")
                TxtQty.Attributes.Add("onkeyup", "changenumberformat('" & TxtQty.ClientID & "')")
                TxtUkuran.Attributes.Add("onkeyup", "changenumberformat('" & TxtUkuran.ClientID & "')")
                TxtHargaAsuransi.Attributes.Add("onkeyup", "changenumberformat('" & TxtHargaAsuransi.ClientID & "')")
                TxtPolis.Attributes.Add("onkeyup", "changenumberformat('" & TxtPolis.ClientID & "')")
                TxtJumlahOngkosAngkutan.Attributes.Add("onkeyup", "changenumberformat('" & TxtJumlahOngkosAngkutan.ClientID & "')")
            End If

            If Not Session("Grid_Invoice_DS_Detail") Is Nothing Then
                Grid_Invoice_DS_Detail.DataSource = CType(Session("Grid_Invoice_DS_Detail"), DataTable)
                Grid_Invoice_DS_Detail.DataBind()
            End If

            If Not Session("Grid_Header_DS_Invoice") Is Nothing Then
                Grid_Invoice_ParentDS.DataSource = CType(Session("Grid_Header_DS_Invoice"), DataTable)
                Grid_Invoice_ParentDS.DataBind()
            End If

        Catch ex As Exception
            Response.Write("<b>Error Page Load :</b>" & ex.ToString)
        End Try
    End Sub

    Private Function Load_Invoice_No(ByVal flag As String) As String
        Try
            Dim pisah() As String
            Dim bulan As String = DtInvoice.Date.ToString("MM")
            Dim tahun As String = DtInvoice.Date.ToString("yy")
            Dim no As Integer
            Dim value As String = ""
            Dim aliaskapal As String
            Dim nodpn As String = ""

            sqlstring = "SELECT Alias_Kapal From Kapal WHERE IDDetail = " & DDLKapal.SelectedValue & " and status = 1 "
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
                    no = CDbl(pisah(0)) + 1
                Else
                    no = 1
                End If



                value = no.ToString("0000") & "/" & Singkatan & "/" & aliaskapal & "/" & CekBulan(bulan) & "/" & tahun & "/DS"
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

                    no = CDbl(pisah(0).Replace("B", "0").Replace("b", "0")) + 1
                Else
                    no = 1
                End If

                nodpn = no.ToString("0000")

                value = "B" & nodpn.Substring(1, nodpn.Length - 1) & "/" & Singkatan & "/" & aliaskapal & "/" & CekBulan(bulan) & "/" & tahun & "/DS"
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

                'pisah = hasil.ToString.Split("/")

                'no = CDbl(pisah(0)) + 1

                If hasil <> "" Then
                    pisah = hasil.ToString.Split("/")

                    no = CDbl(pisah(0).Replace("B", "0").Replace("b", "0")) + 1
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

            sqlstring = "SELECT Alias_Kapal From Kapal WHERE IDDetail = " & DDLKapal.SelectedValue & " AND [status] = 1"
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

    Private Sub ADDDetail()
        Try



            If HFMode.Value = "Insert" Then
                Dim DT As DataTable
                Dim DR As DataRow
                DT = CType(Session("Grid_Invoice_DS_Detail"), DataTable)

                TOtalByr = UbahKomaJdTitik(TxtUkuran.Text.Trim.Replace("'", "")) * ReplaceString(TxtHargaByr.Text.Replace("'", ""))

                DR = DT.NewRow
                DR("No") = ""
                DR("IDJenisPembayaran") = DDLSatuan.SelectedValue
                DR("JenisPembayaran") = DDLSatuan.SelectedItem
                DR("Harga") = ReplaceString(TxtHargaByr.Text.Trim.Replace("'", ""))
                DR("Quantity") = TxtQty.Text.Trim.Replace("'", "").Replace(".", "")
                DR("NamaBarang") = TxtNamaBarang.Text.Trim.ToString.Replace("'", "''")
                DR("IDSatuan") = DDLSatuanbrg.SelectedValue
                DR("Ukuran") = UbahKomaJdTitik(TxtUkuran.Text.Replace("'", "''"))
                DR("Satuan") = DDLSatuanbrg.SelectedItem
                DR("TotalByr") = TOtalByr
                DT.Rows.Add(DR)

                Session("Grid_Invoice_DS_Detail") = DT
                Grid_Invoice_DS_Detail.DataSource = DT
                Grid_Invoice_DS_Detail.DataBind()

            Else
                Dim DT As DataTable
                Dim DR As DataRow
                DT = CType(Session("Grid_Invoice_DS_Detail"), DataTable)

                TOtalByr = UbahKomaJdTitik(TxtUkuran.Text.Trim.Replace("'", "")) * ReplaceString(TxtHargaByr.Text.Replace("'", ""))

                DR = DT.NewRow
                DR("No") = ""
                DR("IDJenisPembayaran") = DDLSatuan.SelectedValue
                DR("JenisPembayaran") = DDLSatuan.SelectedItem
                DR("Harga") = ReplaceString(TxtHargaByr.Text.Trim.Replace("'", ""))
                DR("Quantity") = TxtQty.Text.Trim.Replace("'", "").Replace(".", "")
                DR("NamaBarang") = TxtNamaBarang.Text.Trim.ToString.Replace("'", "''")
                DR("IDSatuan") = DDLSatuanbrg.SelectedValue
                DR("Ukuran") = UbahKomaJdTitik(TxtUkuran.Text.Replace("'", "''"))
                DR("Satuan") = DDLSatuanbrg.SelectedItem
                DR("TotalByr") = TOtalByr
                DT.Rows.Add(DR)

                Session("Grid_Invoice_DS_Detail") = DT
                Grid_Invoice_DS_Detail.DataSource = DT
                Grid_Invoice_DS_Detail.DataBind()
            End If

            HFModeItem.Value = "Insert"
            HFIDDetail.Value = ""
        Catch ex As Exception
            Throw New Exception(ex.ToString)
        End Try
    End Sub

    Private Sub EditDetail()
        Try

            If HFMode.Value = "Insert" Then
                Dim DT As DataTable
                DT = CType(Session("Grid_Invoice_DS_Detail"), DataTable)

                For i As Integer = 0 To DT.Rows.Count - 1
                    If DT.Rows(i).Item("NamaBarang").ToString = TxtNamaBarang.Text.Replace("'", "''") Then

                        TOtalByr = UbahKomaJdTitik(TxtUkuran.Text.Trim.Replace("'", "")) * ReplaceString(TxtHargaByr.Text.Replace("'", ""))

                        DT.Rows(i).Item("IDJenisPembayaran") = DDLSatuan.SelectedValue
                        DT.Rows(i).Item("JenisPembayaran") = DDLSatuan.SelectedItem
                        DT.Rows(i).Item("Harga") = ReplaceString(TxtHargaByr.Text.Replace("'", ""))
                        DT.Rows(i).Item("Quantity") = TxtQty.Text.Replace("'", "").Replace(".", "")
                        DT.Rows(i).Item("IDSatuan") = DDLSatuanbrg.SelectedValue
                        DT.Rows(i).Item("Satuan") = DDLSatuanbrg.SelectedItem
                        DT.Rows(i).Item("Ukuran") = UbahKomaJdTitik(TxtUkuran.Text.Replace("'", ""))
                        DT.Rows(i).Item("TotalByr") = TOtalByr
                        Exit For
                    End If
                Next

                Session("Grid_Invoice_DS_Detail") = DT
                Grid_Invoice_DS_Detail.DataSource = DT
                Grid_Invoice_DS_Detail.DataBind()
                HFModeItem.Value = "Insert"
            Else
                Dim DT As DataTable
                DT = CType(Session("Grid_Invoice_DS_Detail"), DataTable)

                For i As Integer = 0 To DT.Rows.Count - 1
                    If HFIDDetail.Value <> "" Then
                        If DT.Rows(i).Item("No").ToString = HFIDDetail.Value Then
                            TOtalByr = UbahKomaJdTitik(TxtUkuran.Text.Trim.Replace("'", "")) * ReplaceString(TxtHargaByr.Text.Replace("'", ""))

                            DT.Rows(i).Item("IDJenisPembayaran") = DDLSatuan.SelectedValue
                            DT.Rows(i).Item("JenisPembayaran") = DDLSatuan.SelectedItem
                            DT.Rows(i).Item("NamaBarang") = TxtNamaBarang.Text.Replace("'", "")          
                            DT.Rows(i).Item("Harga") = ReplaceString(TxtHargaByr.Text.Replace("'", ""))
                            DT.Rows(i).Item("Quantity") = TxtQty.Text.Replace("'", "").Replace(".", "")
                            DT.Rows(i).Item("IDSatuan") = DDLSatuanbrg.SelectedValue
                            DT.Rows(i).Item("Satuan") = DDLSatuanbrg.SelectedItem
                            DT.Rows(i).Item("Ukuran") = UbahKomaJdTitik(TxtUkuran.Text.Replace("'", ""))
                            DT.Rows(i).Item("TotalByr") = TOtalByr
                            Exit For
                            HFIDDetail.Value = ""
                        End If
                    Else
                        If DT.Rows(i).Item("NamaBarang").ToString = TxtNamaBarang.Text.Replace("'", "''") Then
                            TOtalByr = UbahKomaJdTitik(TxtUkuran.Text.Trim.Replace("'", "")) * ReplaceString(TxtHargaByr.Text.Replace("'", ""))

                            DT.Rows(i).Item("IDJenisPembayaran") = DDLSatuan.SelectedValue
                            DT.Rows(i).Item("JenisPembayaran") = DDLSatuan.SelectedItem
                            DT.Rows(i).Item("Harga") = ReplaceString(TxtHargaByr.Text.Replace("'", ""))
                            DT.Rows(i).Item("Quantity") = TxtQty.Text.Replace("'", "").Replace(".", "")
                            DT.Rows(i).Item("IDSatuan") = DDLSatuanbrg.SelectedValue
                            DT.Rows(i).Item("Satuan") = DDLSatuanbrg.SelectedItem
                            DT.Rows(i).Item("Ukuran") = UbahKomaJdTitik(TxtUkuran.Text.Replace("'", ""))
                            DT.Rows(i).Item("TotalByr") = TOtalByr
                            Exit For
                        End If
                    End If
                Next

                Session("Grid_Invoice_DS_Detail") = DT
                Grid_Invoice_DS_Detail.DataSource = DT
                Grid_Invoice_DS_Detail.DataBind()
                HFModeItem.Value = "Insert"
                HFIDDetail.Value = ""
            End If
            
        Catch ex As Exception
            Throw New Exception(ex.ToString)
        End Try
    End Sub

    Private Sub Remove_Item(ByVal ID As Integer)
        Try
            DT = CType(Session("Grid_Invoice_DS_Detail"), DataTable)

            DT.Rows.RemoveAt(ID)

            Session("Grid_Invoice_DS_Detail") = DT
            Grid_Invoice_DS_Detail.DataSource = DT
            Grid_Invoice_DS_Detail.DataBind()

            clear_label()
        Catch ex As Exception
            Response.Write("Error Remove_Item <BR> : " & ex.ToString)
        End Try
    End Sub

    Private Sub Remove_itemDB(ByVal ID As String, ByVal InvoiceNo As String)
        Try
            sqlstring = "UPDATE InvoiceDetail SET [status] = 0 WHERE No_Invoice = '" & InvoiceNo & "' AND IDDetail = '" & ID & "' AND [status] <> 0"

            hasil = SQLExecuteNonQuery(sqlstring)


        Catch ex As Exception
            Throw New Exception(ex.ToString)
        End Try
    End Sub

#End Region

#Region "Button"

    Protected Sub btAdd_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btAdd.Click
        Try
            Dim KtaAsalAsalBrg As String = ""
            LblBayar.Text = 0

            If HFModeItem.Value = "Insert" Then
                If validation_addnew() Then

                    If HFMode.Value = "Insert" And TxtNoInvoice.Text = "" Then
                        KtaAsalAsalBrg = Get_Kota_Asal_Barang(HFCodeCust.Value)
                        'TxtNoInvoice.Text = Load_Invoice_No("0")
                    End If
                    
                    ADDDetail()
                    clear_detail()
                    TxtNamaPengirim.Text = HFNmCust.Value
                    TxtDaerahDitujukan.Text = HFDaerahDitujukan.Value

                    For i As Integer = 0 To Grid_Invoice_DS_Detail.VisibleRowCount - 1
                        LblBayar.Text = CDbl(LblBayar.Text) + CDbl(Grid_Invoice_DS_Detail.GetRowValues(i, "TotalByr").ToString)
                    Next
                    LblBayar.Text = UbahKoma(LblBayar.Text)
                End If
            Else
                If validation_addnew() Then
                    EditDetail()
                    clear_detail()
                    TxtNamaPengirim.Text = HFNmCust.Value
                    TxtDaerahDitujukan.Text = HFDaerahDitujukan.Value
                    For i As Integer = 0 To Grid_Invoice_DS_Detail.VisibleRowCount - 1
                        LblBayar.Text = CDbl(LblBayar.Text) + CDbl(Grid_Invoice_DS_Detail.GetRowValues(i, "TotalByr").ToString)
                    Next
                    LblBayar.Text = UbahKoma(LblBayar.Text)
                End If

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
                If validation_insert() Then
                    Insert()
                    Grid_Invoice_DS_Detail.DataSource = Nothing
                    Grid_Invoice_DS_Detail.DataBind()



                    chkAsuransi.Checked = False
                    LblBayar.Text = ""
                    clear_asuransi()
                End If
                linfoberhasil.Visible = True
            Else
                If validation_update() Then
                    UPDATE(HFNoInvoice.Value)
                    Grid_Invoice_DS_Detail.DataSource = Nothing
                    Grid_Invoice_DS_Detail.DataBind()


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

        If DDLKapal.SelectedIndex <> 0 Then
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
            create_session()

            TxtNamaPengirim.Text = ""

            TxtNoInvoice.Text = ""

            HFMode.Value = "Insert"
            HFModeItem.Value = "Insert"

            HFInvoiceHeaderID.Value = ""
            HFID.Value = ""
            HFIDDetail.Value = ""
            HFInvoiceDetailID.Value = ""

            HFTujuan.Value = ""
            hfDel.Value = ""

            HFSatuan.Value = ""

            HFNamakapal.Value = ""
            HFPembayaran.Value = ""
            HFPenerima.Value = ""
            TxtNoInvoice.Enabled = True
            HFNoInvoice.Value = ""
            ChkMiniByr.Checked = False
            txtMinByr.Text = ""
            ChkOngkos.Checked = False
            TxtOngkos.Text = ""
            TxtJumlahOngkosAngkutan.Text = ""
            HFCodeCust.Value = ""
            HFDaerahDitujukan.Value = ""
            LblBayar.Text = ""
            HFDaerahDitujukan.Value = ""
            TxtDaerahDitujukan.Text = ""
            DDLKapal.SelectedIndex = 0
            TxtIndikator.Text = ""
            TxtIndikator.Enabled = True
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
            DDLSatuan.SelectedIndex = 0
            DDLSatuanbrg.SelectedIndex = 0
            TxtNamaBarang.Text = ""
            TxtQty.Text = ""
            TxtHargaByr.Text = ""
            TxtUkuran.Text = ""
            Lblukuran.Text = ""
        Catch ex As Exception
            Throw New Exception("<b>Error function clear detail :</b>" & ex.ToString)
        End Try
    End Sub

    Private Sub Insert()
        Try
            Dim bulan As String = DtInvoice.Date.ToString("MM")
            Dim tahun As String = DtInvoice.Date.ToString("yy")
            sqlstring = ""
            Dim TotalBayar As Double = 0
            Dim TotalSatuan As Double = 0
            Dim sqlponly As String = ""
            Dim iddetail As Integer
            Dim hargasatuan As Double = 0
            Dim TotalAsuransi As Double = 0
            Dim NamaDitunjukan As String = ""
            Dim KotaDitunjukan As String = ""
            Dim KeteranganOngkosAngkut As String = ""
            Dim CodeCust As String = ""
            'ADDED TO INSERT JOURNAL
            Dim eDS As DataSet
            Dim eDT As DataTable
            Dim RunNo As Int64 = GetRunNoHeader()

            TxtNoInvoice.Text = Load_Invoice_No(TxtIndikator.Text.Trim)

            Dim splitkota() As String = {}

            CodeCust = HFCodeCust.Value

            splitkota = HFDaerahDitujukan.Value.Split("(")

            If splitkota.Length > 0 Then
                KotaDitunjukan = splitkota(0).Trim
            Else
                KotaDitunjukan = HFDaerahDitujukan.Value
            End If


            NamaDitunjukan = HFNmCust.Value

            If chkAsuransi.Checked = True Then
                TotalAsuransi = (CDbl(ReplaceString(TxtHargaAsuransi.Text)) * (TxtPremi.Text.Replace(",", ".") / 100)) + CDbl(ReplaceString(TxtPolis.Text))
            End If

            For i As Integer = 0 To Grid_Invoice_DS_Detail.VisibleRowCount - 1
                TotalBayar = TotalBayar + CDbl(Grid_Invoice_DS_Detail.GetRowValues(i, "TotalByr").ToString)
            Next

            If ChkOngkos.Checked Then
                KeteranganOngkosAngkut = TxtOngkos.Text.Trim.Replace("'", "''") & "-" & TxtJumlahOngkosAngkutan.Text
                TotalBayar = TotalBayar + ReplaceString(TxtJumlahOngkosAngkutan.Text.Trim.Replace("'", "''"))
            End If

            '<--- Insert to AR journal ---->'
            If GetCOAPiutangCust(NamaDitunjukan.ToString) = True Then
                If GetCOAPenghasilanKapal(DDLKapal.SelectedValue) = True Then
                    eDS = GetConfigAR(NamaDitunjukan.ToString, DDLKapal.SelectedValue)
                    eDT = eDS.Tables(0)
                    If eDT.Rows.Count > 0 Then
                        sqlstring = "INSERT INTO InvoiceHeader(No_Invoice, MuatbarangID, KapalID, Total, InvoiceDate, Ditujukan, DaerahDitujukan, " & _
                        "NoAsuransi, HargaAsuransi, Premi, Polis, TotalAsuransi,Paid,DP, UserName, [status], KeteranganOngkosAngkut, CodeCust, KeteranganDS, StatusNumber, BlnInvoice) VALUES " & _
                      "('" & TxtNoInvoice.Text.ToString & "', '', " & DDLKapal.SelectedValue & " " & _
                      ", " & TotalBayar & ", '" & DtInvoice.Text & "', '" & NamaDitunjukan.ToString & "', '" & KotaDitunjukan.ToString & "', " & _
                      " '" & TxtAsuransi.Text.ToString & "', " & CekNilai(ReplaceString(TxtHargaAsuransi.Text.Replace("'", "''").Trim)) & ", " & _
                      "'" & CekNilai(TxtPremi.Text.Trim.Replace("'", "''").Replace(",", ".")) & "', '" & CekNilai(ReplaceString(TxtPolis.Text.Trim.Replace("'", "''"))) & "', " & _
                      "" & TotalAsuransi & ",'" & hfPaid.Value & "' , 'No', '" & Session("UserId") & "', 1, '" & KeteranganOngkosAngkut & "', '" & CodeCust & "', 'Yes', 1, '" & CekBulan(bulan) & "/" & tahun & "' );"

                        iddetail = 0
                        '<--- Check New Invoice Number Start With b or not ---->'

                        If Left(TxtNoInvoice.Text, 1) = "b" Or Left(TxtNoInvoice.Text, 1) = "B" Then
                            TotalBayar = 0
                            For i As Integer = 0 To Grid_Invoice_DS_Detail.VisibleRowCount - 1
                                With Grid_Invoice_DS_Detail
                                    '<---- Check jenis satuan ----->'

                                    Dim TOtalUkuran As String = .GetRowValues(i, "Ukuran").ToString

                                    TotalSatuan = CDbl(.GetRowValues(i, "TotalByr").ToString)
                                    TotalBayar = TotalBayar + TotalSatuan
                                    iddetail = iddetail + 1
                                    sqlstring &= "INSERT INTO InvoiceDetailDS(IDDetail,No_Invoice,HargaID, Quantity, NamaBarang,HargaSatuan, SatuanID,DP,TotalUkuran, UserName, [status]) VALUES " & _
                                         "('" & iddetail & "','" & TxtNoInvoice.Text.ToString & "','" & .GetRowValues(i, "IDJenisPembayaran") & "', " & .GetRowValues(i, "Quantity") & ",'" & .GetRowValues(i, "NamaBarang") & "', " & _
                                         "" & .GetRowValues(i, "Harga") & ",'" & .GetRowValues(i, "IDSatuan") & "','No', '" & TOtalUkuran & "','" & Session("UserId") & "', 1); "


                                End With
                            Next
                            result = SQLExecuteNonQuery(sqlstring, False, True)
                        Else
                            
                            iddetail = 0
                            TotalBayar = 0
                            For i As Integer = 0 To Grid_Invoice_DS_Detail.VisibleRowCount - 1
                                With Grid_Invoice_DS_Detail

                                    Dim TOtalUkuran As String = .GetRowValues(i, "Ukuran").ToString

                                    TotalSatuan = CDbl(.GetRowValues(i, "TotalByr").ToString)
                                    TotalBayar = TotalBayar + TotalSatuan
                                    iddetail = iddetail + 1
                                    sqlstring &= "INSERT INTO InvoiceDetailDS(IDDetail,No_Invoice,HargaID, Quantity, NamaBarang,HargaSatuan, SatuanID,DP,TotalUkuran, UserName, [status]) VALUES " & _
                                         "('" & iddetail & "','" & TxtNoInvoice.Text.ToString & "','" & .GetRowValues(i, "IDJenisPembayaran") & "', " & .GetRowValues(i, "Quantity") & ",'" & .GetRowValues(i, "NamaBarang") & "', " & _
                                         "" & .GetRowValues(i, "Harga") & ",'" & .GetRowValues(i, "IDSatuan") & "','No', '" & TOtalUkuran & "','" & Session("UserId") & "', 1); "



                                End With

                            Next
                            If sqlstring <> "" Then
                                result = SQLExecuteNonQuery(sqlstring)
                            End If

                        End If

                        'INSERT JOURNAL
                        InsertARHeader(RunNo, TxtNoInvoice.Text.ToString, DtInvoice.Text, TotalBayar, CodeCust.ToString, "NULL", Session("UserId").ToString)
                        InsertARDetailDebit(RunNo, NamaDitunjukan, TotalBayar, Session("UserId").ToString)
                        InsertARDetailKredit(RunNo, DDLKapal.SelectedValue, TotalBayar, Session("UserId").ToString)
                        'END INSERT JOURNAL
                    Else
                        'INSERT LINKED ACCOUNT DULU
                        InsertLinkedAccount(DDLKapal.SelectedValue, NamaDitunjukan.ToString, Session("UserId"))
                        'END INSERT LINKED ACCOUNT
                        sqlstring = "INSERT INTO InvoiceHeader(No_Invoice, MuatbarangID, KapalID, Total, InvoiceDate, Ditujukan, DaerahDitujukan, " & _
                        "NoAsuransi, HargaAsuransi, Premi, Polis, TotalAsuransi,Paid,DP, UserName, [status], KeteranganOngkosAngkut, CodeCust, KeteranganDS, StatusNumber, BlnInvoice) VALUES " & _
                      "('" & TxtNoInvoice.Text.ToString & "', '', " & DDLKapal.SelectedValue & " " & _
                      ", " & TotalBayar & ", '" & DtInvoice.Text & "', '" & NamaDitunjukan.ToString & "', '" & KotaDitunjukan.ToString & "', " & _
                      " '" & TxtAsuransi.Text.ToString & "', " & CekNilai(ReplaceString(TxtHargaAsuransi.Text.Replace("'", "''").Trim)) & ", " & _
                      "'" & CekNilai(TxtPremi.Text.Trim.Replace("'", "''").Replace(",", ".")) & "', '" & CekNilai(ReplaceString(TxtPolis.Text.Trim.Replace("'", "''"))) & "', " & _
                      "" & TotalAsuransi & ",'" & hfPaid.Value & "' , 'No', '" & Session("UserId") & "', 1, '" & KeteranganOngkosAngkut & "', '" & CodeCust & "', 'Yes', 1, '" & CekBulan(bulan) & "/" & tahun & "' );"

                        iddetail = 0
                        '<--- Check New Invoice Number Start With b or not ---->'

                        If Left(TxtNoInvoice.Text, 1) = "b" Or Left(TxtNoInvoice.Text, 1) = "B" Then
                            TotalBayar = 0
                            For i As Integer = 0 To Grid_Invoice_DS_Detail.VisibleRowCount - 1
                                With Grid_Invoice_DS_Detail
                                    '<---- Check jenis satuan ----->'

                                    Dim TOtalUkuran As String = .GetRowValues(i, "Ukuran").ToString

                                    TotalSatuan = CDbl(.GetRowValues(i, "TotalByr").ToString)
                                    TotalBayar = TotalBayar + TotalSatuan
                                    iddetail = iddetail + 1
                                    sqlstring &= "INSERT INTO InvoiceDetailDS(IDDetail,No_Invoice,HargaID, Quantity, NamaBarang,HargaSatuan, SatuanID,DP,TotalUkuran, UserName, [status]) VALUES " & _
                                         "('" & iddetail & "','" & TxtNoInvoice.Text.ToString & "','" & .GetRowValues(i, "IDJenisPembayaran") & "', " & .GetRowValues(i, "Quantity") & ",'" & .GetRowValues(i, "NamaBarang") & "', " & _
                                         "" & .GetRowValues(i, "Harga") & ",'" & .GetRowValues(i, "IDSatuan") & "','No', '" & TOtalUkuran & "','" & Session("UserId") & "', 1); "


                                End With
                            Next
                            result = SQLExecuteNonQuery(sqlstring, False, True)
                        Else
                            iddetail = 0
                            TotalBayar = 0
                            For i As Integer = 0 To Grid_Invoice_DS_Detail.VisibleRowCount - 1
                                With Grid_Invoice_DS_Detail

                                    Dim TOtalUkuran As String = .GetRowValues(i, "Ukuran").ToString

                                    TotalSatuan = CDbl(.GetRowValues(i, "TotalByr").ToString)
                                    TotalBayar = TotalBayar + TotalSatuan
                                    iddetail = iddetail + 1
                                    sqlstring &= "INSERT INTO InvoiceDetailDS(IDDetail,No_Invoice,HargaID, Quantity, NamaBarang,HargaSatuan, SatuanID,DP,TotalUkuran, UserName, [status]) VALUES " & _
                                         "('" & iddetail & "','" & TxtNoInvoice.Text.ToString & "','" & .GetRowValues(i, "IDJenisPembayaran") & "', " & .GetRowValues(i, "Quantity") & ",'" & .GetRowValues(i, "NamaBarang") & "', " & _
                                         "" & .GetRowValues(i, "Harga") & ",'" & .GetRowValues(i, "IDSatuan") & "','No', '" & TOtalUkuran & "','" & Session("UserId") & "', 1); "



                                End With

                            Next
                            If sqlstring <> "" Then
                                result = SQLExecuteNonQuery(sqlstring)
                            End If

                        End If

                        'INSERT JOURNAL
                        InsertARHeader(RunNo, TxtNoInvoice.Text.ToString, DtInvoice.Text, TotalBayar, CodeCust.ToString, "NULL", Session("UserId").ToString)
                        InsertARDetailDebit(RunNo, NamaDitunjukan, TotalBayar, Session("UserId").ToString)
                        InsertARDetailKredit(RunNo, DDLKapal.SelectedValue, TotalBayar, Session("UserId").ToString)
                        'END INSERT JOURNAL
                    End If
                Else
                    'INSERT COA PENGHASILAN DAN LINKED ACCOUNT DULU
                    InsertCOAPenghasilanKapal(DDLKapal.SelectedValue, Session("UserId"))
                    InsertLinkedAccount(DDLKapal.SelectedValue, NamaDitunjukan.ToString, Session("UserId"))
                    'END INSERT LINKED ACCOUNT
                    sqlstring = "INSERT INTO InvoiceHeader(No_Invoice, MuatbarangID, KapalID, Total, InvoiceDate, Ditujukan, DaerahDitujukan, " & _
                        "NoAsuransi, HargaAsuransi, Premi, Polis, TotalAsuransi,Paid,DP, UserName, [status], KeteranganOngkosAngkut, CodeCust, KeteranganDS, StatusNumber, BlnInvoice) VALUES " & _
                      "('" & TxtNoInvoice.Text.ToString & "', '', " & DDLKapal.SelectedValue & " " & _
                      ", " & TotalBayar & ", '" & DtInvoice.Text & "', '" & NamaDitunjukan.ToString & "', '" & KotaDitunjukan.ToString & "', " & _
                      " '" & TxtAsuransi.Text.ToString & "', " & CekNilai(ReplaceString(TxtHargaAsuransi.Text.Replace("'", "''").Trim)) & ", " & _
                      "'" & CekNilai(TxtPremi.Text.Trim.Replace("'", "''").Replace(",", ".")) & "', '" & CekNilai(ReplaceString(TxtPolis.Text.Trim.Replace("'", "''"))) & "', " & _
                      "" & TotalAsuransi & ",'" & hfPaid.Value & "' , 'No', '" & Session("UserId") & "', 1, '" & KeteranganOngkosAngkut & "', '" & CodeCust & "', 'Yes', 1, '" & CekBulan(bulan) & "/" & tahun & "' );"

                    iddetail = 0
                    '<--- Check New Invoice Number Start With b or not ---->'

                    If Left(TxtNoInvoice.Text, 1) = "b" Or Left(TxtNoInvoice.Text, 1) = "B" Then
                        TotalBayar = 0
                        For i As Integer = 0 To Grid_Invoice_DS_Detail.VisibleRowCount - 1
                            With Grid_Invoice_DS_Detail
                                '<---- Check jenis satuan ----->'

                                Dim TOtalUkuran As String = .GetRowValues(i, "Ukuran").ToString

                                TotalSatuan = CDbl(.GetRowValues(i, "TotalByr").ToString)
                                TotalBayar = TotalBayar + TotalSatuan
                                iddetail = iddetail + 1
                                sqlstring &= "INSERT INTO InvoiceDetailDS(IDDetail,No_Invoice,HargaID, Quantity, NamaBarang,HargaSatuan, SatuanID,DP,TotalUkuran, UserName, [status]) VALUES " & _
                                     "('" & iddetail & "','" & TxtNoInvoice.Text.ToString & "','" & .GetRowValues(i, "IDJenisPembayaran") & "', " & .GetRowValues(i, "Quantity") & ",'" & .GetRowValues(i, "NamaBarang") & "', " & _
                                     "" & .GetRowValues(i, "Harga") & ",'" & .GetRowValues(i, "IDSatuan") & "','No', '" & TOtalUkuran & "','" & Session("UserId") & "', 1); "


                            End With
                        Next
                        result = SQLExecuteNonQuery(sqlstring, False, True)
                    Else
                        iddetail = 0
                        TotalBayar = 0
                        For i As Integer = 0 To Grid_Invoice_DS_Detail.VisibleRowCount - 1
                            With Grid_Invoice_DS_Detail

                                Dim TOtalUkuran As String = .GetRowValues(i, "Ukuran").ToString

                                TotalSatuan = CDbl(.GetRowValues(i, "TotalByr").ToString)
                                TotalBayar = TotalBayar + TotalSatuan
                                iddetail = iddetail + 1
                                sqlstring &= "INSERT INTO InvoiceDetailDS(IDDetail,No_Invoice,HargaID, Quantity, NamaBarang,HargaSatuan, SatuanID,DP,TotalUkuran, UserName, [status]) VALUES " & _
                                     "('" & iddetail & "','" & TxtNoInvoice.Text.ToString & "','" & .GetRowValues(i, "IDJenisPembayaran") & "', " & .GetRowValues(i, "Quantity") & ",'" & .GetRowValues(i, "NamaBarang") & "', " & _
                                     "" & .GetRowValues(i, "Harga") & ",'" & .GetRowValues(i, "IDSatuan") & "','No', '" & TOtalUkuran & "','" & Session("UserId") & "', 1); "



                            End With

                        Next
                        If sqlstring <> "" Then
                            result = SQLExecuteNonQuery(sqlstring)
                        End If

                    End If

                    'INSERT JOURNAL
                    InsertARHeader(RunNo, TxtNoInvoice.Text.ToString, DtInvoice.Text, TotalBayar, CodeCust.ToString, "NULL", Session("UserId").ToString)
                    InsertARDetailDebit(RunNo, NamaDitunjukan, TotalBayar, Session("UserId").ToString)
                    InsertARDetailKredit(RunNo, DDLKapal.SelectedValue, TotalBayar, Session("UserId").ToString)
                    'END INSERT JOURNAL
                End If
            Else

                If GetCOAPenghasilanKapal(DDLKapal.SelectedValue) = True Then
                    'INSERT COA PIUTANG DAN LINKED ACCOUNT DULU
                    InsertCOAPiutang(NamaDitunjukan.ToString, Session("userId"))
                    InsertLinkedAccount(DDLKapal.SelectedValue, NamaDitunjukan.ToString, Session("UserId"))
                    'END INSERT LINKED ACCOUNT
                    sqlstring = "INSERT INTO InvoiceHeader(No_Invoice, MuatbarangID, KapalID, Total, InvoiceDate, Ditujukan, DaerahDitujukan, " & _
                        "NoAsuransi, HargaAsuransi, Premi, Polis, TotalAsuransi,Paid,DP, UserName, [status], KeteranganOngkosAngkut, CodeCust, KeteranganDS, StatusNumber, BlnInvoice) VALUES " & _
                      "('" & TxtNoInvoice.Text.ToString & "', '', " & DDLKapal.SelectedValue & " " & _
                      ", " & TotalBayar & ", '" & DtInvoice.Text & "', '" & NamaDitunjukan.ToString & "', '" & KotaDitunjukan.ToString & "', " & _
                      " '" & TxtAsuransi.Text.ToString & "', " & CekNilai(ReplaceString(TxtHargaAsuransi.Text.Replace("'", "''").Trim)) & ", " & _
                      "'" & CekNilai(TxtPremi.Text.Trim.Replace("'", "''").Replace(",", ".")) & "', '" & CekNilai(ReplaceString(TxtPolis.Text.Trim.Replace("'", "''"))) & "', " & _
                      "" & TotalAsuransi & ",'" & hfPaid.Value & "' , 'No', '" & Session("UserId") & "', 1, '" & KeteranganOngkosAngkut & "', '" & CodeCust & "', 'Yes', 1, '" & CekBulan(bulan) & "/" & tahun & "' );"

                    iddetail = 0
                    '<--- Check New Invoice Number Start With b or not ---->'

                    If Left(TxtNoInvoice.Text, 1) = "b" Or Left(TxtNoInvoice.Text, 1) = "B" Then
                        TotalBayar = 0
                        For i As Integer = 0 To Grid_Invoice_DS_Detail.VisibleRowCount - 1
                            With Grid_Invoice_DS_Detail
                                '<---- Check jenis satuan ----->'

                                Dim TOtalUkuran As String = .GetRowValues(i, "Ukuran").ToString

                                TotalSatuan = CDbl(.GetRowValues(i, "TotalByr").ToString)
                                TotalBayar = TotalBayar + TotalSatuan
                                iddetail = iddetail + 1
                                sqlstring &= "INSERT INTO InvoiceDetailDS(IDDetail,No_Invoice,HargaID, Quantity, NamaBarang,HargaSatuan, SatuanID,DP,TotalUkuran, UserName, [status]) VALUES " & _
                                     "('" & iddetail & "','" & TxtNoInvoice.Text.ToString & "','" & .GetRowValues(i, "IDJenisPembayaran") & "', " & .GetRowValues(i, "Quantity") & ",'" & .GetRowValues(i, "NamaBarang") & "', " & _
                                     "" & .GetRowValues(i, "Harga") & ",'" & .GetRowValues(i, "IDSatuan") & "','No', '" & TOtalUkuran & "','" & Session("UserId") & "', 1); "


                            End With
                        Next
                        result = SQLExecuteNonQuery(sqlstring, False, True)
                    Else
                        iddetail = 0
                        TotalBayar = 0
                        For i As Integer = 0 To Grid_Invoice_DS_Detail.VisibleRowCount - 1
                            With Grid_Invoice_DS_Detail

                                Dim TOtalUkuran As String = .GetRowValues(i, "Ukuran").ToString

                                TotalSatuan = CDbl(.GetRowValues(i, "TotalByr").ToString)
                                TotalBayar = TotalBayar + TotalSatuan
                                iddetail = iddetail + 1
                                sqlstring &= "INSERT INTO InvoiceDetailDS(IDDetail,No_Invoice,HargaID, Quantity, NamaBarang,HargaSatuan, SatuanID,DP,TotalUkuran, UserName, [status]) VALUES " & _
                                     "('" & iddetail & "','" & TxtNoInvoice.Text.ToString & "','" & .GetRowValues(i, "IDJenisPembayaran") & "', " & .GetRowValues(i, "Quantity") & ",'" & .GetRowValues(i, "NamaBarang") & "', " & _
                                     "" & .GetRowValues(i, "Harga") & ",'" & .GetRowValues(i, "IDSatuan") & "','No', '" & TOtalUkuran & "','" & Session("UserId") & "', 1); "



                            End With

                        Next
                        If sqlstring <> "" Then
                            result = SQLExecuteNonQuery(sqlstring)
                        End If

                    End If

                    'INSERT JOURNAL
                    InsertARHeader(RunNo, TxtNoInvoice.Text.ToString, DtInvoice.Text, TotalBayar, CodeCust.ToString, "NULL", Session("UserId").ToString)
                    InsertARDetailDebit(RunNo, NamaDitunjukan, TotalBayar, Session("UserId").ToString)
                    InsertARDetailKredit(RunNo, DDLKapal.SelectedValue, TotalBayar, Session("UserId").ToString)
                    'END INSERT JOURNAL
                Else
                    'INSERT COA PENGHASILAN DAN LINKED ACCOUNT DULU
                    InsertCOAPiutang(NamaDitunjukan.ToString, Session("userId"))
                    InsertCOAPenghasilanKapal(DDLKapal.SelectedValue, Session("userId"))
                    InsertLinkedAccount(DDLKapal.SelectedValue, NamaDitunjukan.ToString, Session("UserId"))
                    'END INSERT LINKED ACCOUNT
                    sqlstring = "INSERT INTO InvoiceHeader(No_Invoice, MuatbarangID, KapalID, Total, InvoiceDate, Ditujukan, DaerahDitujukan, " & _
                        "NoAsuransi, HargaAsuransi, Premi, Polis, TotalAsuransi,Paid,DP, UserName, [status], KeteranganOngkosAngkut, CodeCust, KeteranganDS, StatusNumber, BlnInvoice) VALUES " & _
                      "('" & TxtNoInvoice.Text.ToString & "', '', " & DDLKapal.SelectedValue & " " & _
                      ", " & TotalBayar & ", '" & DtInvoice.Text & "', '" & NamaDitunjukan.ToString & "', '" & KotaDitunjukan.ToString & "', " & _
                      " '" & TxtAsuransi.Text.ToString & "', " & CekNilai(ReplaceString(TxtHargaAsuransi.Text.Replace("'", "''").Trim)) & ", " & _
                      "'" & CekNilai(TxtPremi.Text.Trim.Replace("'", "''").Replace(",", ".")) & "', '" & CekNilai(ReplaceString(TxtPolis.Text.Trim.Replace("'", "''"))) & "', " & _
                      "" & TotalAsuransi & ",'" & hfPaid.Value & "' , 'No', '" & Session("UserId") & "', 1, '" & KeteranganOngkosAngkut & "', '" & CodeCust & "', 'Yes', 1, '" & CekBulan(bulan) & "/" & tahun & "' );"

                    iddetail = 0
                    '<--- Check New Invoice Number Start With b or not ---->'

                    If Left(TxtNoInvoice.Text, 1) = "b" Or Left(TxtNoInvoice.Text, 1) = "B" Then
                        TotalBayar = 0
                        For i As Integer = 0 To Grid_Invoice_DS_Detail.VisibleRowCount - 1
                            With Grid_Invoice_DS_Detail
                                '<---- Check jenis satuan ----->'

                                Dim TOtalUkuran As String = .GetRowValues(i, "Ukuran").ToString

                                TotalSatuan = CDbl(.GetRowValues(i, "TotalByr").ToString)
                                TotalBayar = TotalBayar + TotalSatuan
                                iddetail = iddetail + 1
                                sqlstring &= "INSERT INTO InvoiceDetailDS(IDDetail,No_Invoice,HargaID, Quantity, NamaBarang,HargaSatuan, SatuanID,DP,TotalUkuran, UserName, [status]) VALUES " & _
                                     "('" & iddetail & "','" & TxtNoInvoice.Text.ToString & "','" & .GetRowValues(i, "IDJenisPembayaran") & "', " & .GetRowValues(i, "Quantity") & ",'" & .GetRowValues(i, "NamaBarang") & "', " & _
                                     "" & .GetRowValues(i, "Harga") & ",'" & .GetRowValues(i, "IDSatuan") & "','No', '" & TOtalUkuran & "','" & Session("UserId") & "', 1); "


                            End With
                        Next
                        result = SQLExecuteNonQuery(sqlstring, False, True)
                    Else
                        iddetail = 0
                        TotalBayar = 0
                        For i As Integer = 0 To Grid_Invoice_DS_Detail.VisibleRowCount - 1
                            With Grid_Invoice_DS_Detail

                                Dim TOtalUkuran As String = .GetRowValues(i, "Ukuran").ToString

                                TotalSatuan = CDbl(.GetRowValues(i, "TotalByr").ToString)
                                TotalBayar = TotalBayar + TotalSatuan
                                iddetail = iddetail + 1
                                sqlstring &= "INSERT INTO InvoiceDetailDS(IDDetail,No_Invoice,HargaID, Quantity, NamaBarang,HargaSatuan, SatuanID,DP,TotalUkuran, UserName, [status]) VALUES " & _
                                     "('" & iddetail & "','" & TxtNoInvoice.Text.ToString & "','" & .GetRowValues(i, "IDJenisPembayaran") & "', " & .GetRowValues(i, "Quantity") & ",'" & .GetRowValues(i, "NamaBarang") & "', " & _
                                     "" & .GetRowValues(i, "Harga") & ",'" & .GetRowValues(i, "IDSatuan") & "','No', '" & TOtalUkuran & "','" & Session("UserId") & "', 1); "



                            End With

                        Next
                        If sqlstring <> "" Then
                            result = SQLExecuteNonQuery(sqlstring)
                        End If

                    End If

                    'INSERT JOURNAL
                    InsertARHeader(RunNo, TxtNoInvoice.Text.ToString, DtInvoice.Text, TotalBayar, CodeCust.ToString, "NULL", Session("UserId").ToString)
                    InsertARDetailDebit(RunNo, NamaDitunjukan, TotalBayar, Session("UserId").ToString)
                    InsertARDetailKredit(RunNo, DDLKapal.SelectedValue, TotalBayar, Session("UserId").ToString)
                    'END INSERT JOURNAL
                End If
            End If
            '<--- END Insert to AR journal ---->'

            

            If result <> "" Then
                If ChkMiniByr.Checked Then
                    PembayaranMinimum()
                End If

                Grid_Invoice_ParentDS.DataSource = Nothing
                Grid_Invoice_ParentDS.DataBind()
                clear_all()
                linfoberhasil.Text = "Simpan Berhasil"
                load_grid_invoice_header()
            End If

        Catch ex As Exception
            Throw New Exception("<b>Error Insert :</b>" & ex.ToString)
        End Try
    End Sub

    Private Sub UPDATE(ByVal id As String)
        Try
            Dim iddetail As Integer = 0
            Dim TotalAsuransi As Double = 0
            Dim totalbayar As Double = 0
            Dim NamaDitunjukan As String = ""
            Dim KotaDitunjukan As String = ""
            Dim KeteranganOngkosAngkut As String = ""
            Dim CodeCust As String = ""
            Dim splitkota() As String = {}

            splitkota = HFDaerahDitujukan.Value.Split("(")

            If splitkota.Length > 0 Then
                KotaDitunjukan = splitkota(0).Trim
            Else
                KotaDitunjukan = HFDaerahDitujukan.Value
            End If

            NamaDitunjukan = HFNmCust.Value

            For i As Integer = 0 To Grid_Invoice_DS_Detail.VisibleRowCount - 1
                totalbayar = totalbayar + CDbl(Grid_Invoice_DS_Detail.GetRowValues(i, "TotalByr").ToString)
            Next


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
                        "LastModified = '" & Now.ToString & "' " & _
                        "WHERE No_Invoice = '" & id & "' AND [status] <> 0;"

            iddetail = getDetailID("InvoiceDetailDS", "No_Invoice", HFNoInvoice.Value)

            For i As Integer = 0 To Grid_Invoice_DS_Detail.VisibleRowCount - 1
                If Grid_Invoice_DS_Detail.GetRowValues(i, "No").ToString = "" Then
                    iddetail = iddetail + 1
                    Dim TOtalUkuran As String = Grid_Invoice_DS_Detail.GetRowValues(i, "Ukuran").ToString

                    With Grid_Invoice_DS_Detail
                        sqlstring &= "INSERT INTO InvoiceDetailDS(IDDetail,No_Invoice,HargaID, Quantity, NamaBarang,HargaSatuan, SatuanID,DP,TotalUkuran, UserName, [status]) VALUES " & _
                             "('" & iddetail & "','" & TxtNoInvoice.Text.ToString & "','" & .GetRowValues(i, "IDJenisPembayaran") & "', " & .GetRowValues(i, "Quantity") & ",'" & .GetRowValues(i, "NamaBarang") & "', " & _
                             "" & .GetRowValues(i, "Harga") & ",'" & .GetRowValues(i, "IDSatuan") & "','No', '" & TOtalUkuran & "','" & Session("UserId") & "', 1); "

                    End With

                Else
                    sqlstring &= "UPDATE InvoiceDetailDS SET " & _
                                 "HargaID = " & Grid_Invoice_DS_Detail.GetRowValues(i, "IDJenisPembayaran") & ", " & _
                                 "Quantity = " & Grid_Invoice_DS_Detail.GetRowValues(i, "Quantity") & ", " & _
                                 "NamaBarang = '" & Grid_Invoice_DS_Detail.GetRowValues(i, "NamaBarang") & "', " & _
                                 "SatuanID = " & Grid_Invoice_DS_Detail.GetRowValues(i, "IDSatuan") & ", " & _
                                 "HargaSatuan = " & Grid_Invoice_DS_Detail.GetRowValues(i, "Harga") & ", " & _
                                 "TotalUkuran = " & Grid_Invoice_DS_Detail.GetRowValues(i, "Ukuran") & " " & _
                                 "WHERE No_Invoice = '" & HFNoInvoice.Value & "' AND IDDetail = " & Grid_Invoice_DS_Detail.GetRowValues(i, "No") & "; "
                End If
            Next


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

            If result <> "" Then
                If ChkMiniByr.Checked Then
                    PembayaranMinimum()
                End If
                clear_all()
                linfoberhasil.Visible = True
                linfoberhasil.Text = "Update Berhasil"
                LblBayar.Text = ""
                load_grid_invoice_header()
            End If

        Catch ex As Exception
            Throw New Exception(ex.ToString)
        End Try
    End Sub

    Private Sub PembayaranMinimum()
        Try
            Dim hargaQuo As Double = 0
            Dim hargatotalMinByr As Double = 0

            hargaQuo = CDbl(Grid_Invoice_DS_Detail.GetRowValues(0, "Harga").ToString)
            hargatotalMinByr = hargaQuo * CDbl(txtMinByr.Text.Trim.Replace("'", "''").Replace(",", "."))

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
            sqlstring &= "UPDATE InvoiceDetailDS " & _
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

#End Region

#Region "Validasi"


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

    Private Function validation_insert() As Boolean
        Try
            If Grid_Invoice_DS_Detail.VisibleRowCount = 0 Then
                lInfo.Visible = True
                lInfo.Text = "masukan data detail"
                Return False
            End If

            If HFCodeCust.Value = "" Then
                lInfo.Visible = True
                lInfo.Text = "Pilih Customer"
                Return False
            End If

            If TxtIndikator.Text.Trim.Replace("'", "''") = "" Then
                lInfo.Visible = True
                lInfo.Text = "Masukan Indikator"
                Return False
            End If

            Return True
        Catch ex As Exception
            Throw New Exception(ex.ToString)
        End Try

    End Function

    Private Function validation_update() As Boolean

        Try
           

            If ChkMiniByr.Checked = True Then

                For i As Integer = 0 To Grid_Invoice_ParentDS.VisibleRowCount - 1
                    If Grid_Invoice_ParentDS.Selection.IsRowSelected(i) = False Then
                        lInfo.Visible = True
                        lInfo.Text = "Harus memilih semua apabila menggunakan pembayaran minimum"
                        Return False
                    End If
                Next

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
        'Dim str As String
        'Dim dtcek As DataTable
        'Dim dscek As DataSet
        Try

            clear_Label()

            If DtInvoice.Text = "" Then
                lInfo.Visible = True
                lInfo.Text = "Tanggal Masih Kosong"
                Return False
            End If

            If DDLKapal.SelectedIndex = 0 Then
                lInfo.Visible = True
                lInfo.Text = "Pilih Kapal"
                Return False
            End If

            If DDLSatuan.SelectedIndex = 0 Then
                lInfo.Visible = True
                lInfo.Text = "Jenis Pembayaran Harus diisi"
                Return False
            End If

            If TxtHargaByr.Text.Trim = "" Then
                lInfo.Visible = True
                lInfo.Text = "Harga Bayar Masih Kosong"
                Return False
            End If

            If TxtQty.Text.Trim = "" Then
                lInfo.Visible = True
                lInfo.Text = "Quantity Masih Kosong"
                Return False
            End If

            If TxtNamaBarang.Text.Trim = "" Then
                lInfo.Visible = True
                lInfo.Text = "Nama Barang Masih Kosong"
                Return False
            End If

            If DDLSatuanbrg.SelectedIndex = 0 Then
                lInfo.Visible = True
                lInfo.Text = "Satuan Barang Harus dipilih"
                Return False
            End If

            If TxtUkuran.Text.Trim = "" Then
                lInfo.Visible = True
                lInfo.Text = "Ukuran Masih Kosong"
                Return False
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

            If Grid_Invoice_DS_Detail.VisibleRowCount > 0 Then
                For j As Integer = 0 To Grid_Invoice_DS_Detail.VisibleRowCount - 1

                    If Grid_Invoice_DS_Detail.GetRowValues(j, "IDJenisPembayaran") = DDLSatuan.SelectedValue And Grid_Invoice_DS_Detail.GetRowValues(j, "Harga") = TxtHargaByr.Text.Trim.Replace("'", "") And _
                    Grid_Invoice_DS_Detail.GetRowValues(j, "Quantity") = TxtQty.Text.Replace("'", "") And Grid_Invoice_DS_Detail.GetRowValues(j, "NamaBarang") = TxtNamaBarang.Text.Replace("'", "") And _
                    Grid_Invoice_DS_Detail.GetRowValues(j, "IDSatuan") = DDLSatuanbrg.SelectedValue And Grid_Invoice_DS_Detail.GetRowValues(j, "Ukuran") = TxtUkuran.Text.Trim.Replace("'", "") Then
                        lInfo.Visible = True
                        lInfo.Text &= "Data Sudah ada"
                        Return False
                    End If

                Next

            End If





            Return True
        Catch ex As Exception

        End Try
    End Function
#End Region

#Region "Grid"

    Private Sub Grid_Invoice_ParentDS_RowCommand(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewRowCommandEventArgs) Handles Grid_Invoice_ParentDS.RowCommand
        Try
            Dim pisah() As String

            clear_Label()

            Select Case e.CommandArgs.CommandName
                Case "Edit"
                    HFMode.Value = "Update"
                    DtInvoice.Date = Grid_Invoice_ParentDS.GetRowValues(e.VisibleIndex, "TglInvoice")
                    DDLKapal.SelectedValue = Grid_Invoice_ParentDS.GetRowValues(e.VisibleIndex, "IDKapal")
                    TxtNamaPengirim.Text = Grid_Invoice_ParentDS.GetRowValues(e.VisibleIndex, "Ditujukan")
                    HFCodeCust.Value = Grid_Invoice_ParentDS.GetRowValues(e.VisibleIndex, "Ditujukan")
                    TxtDaerahDitujukan.Text = Grid_Invoice_ParentDS.GetRowValues(e.VisibleIndex, "DaerahDitujukan")
                    HFDaerahDitujukan.Value = Grid_Invoice_ParentDS.GetRowValues(e.VisibleIndex, "DaerahDitujukan")
                    TxtNoInvoice.Text = Grid_Invoice_ParentDS.GetRowValues(e.VisibleIndex, "No_Invoice")
                    HFNoInvoice.Value = Grid_Invoice_ParentDS.GetRowValues(e.VisibleIndex, "No_Invoice")
                    LblBayar.Text = UbahKoma(Grid_Invoice_ParentDS.GetRowValues(e.VisibleIndex, "Total").ToString)
                    TxtNoInvoice.Enabled = False
                    TxtIndikator.Enabled = False

                    If Grid_Invoice_ParentDS.GetRowValues(e.VisibleIndex, "Keterangan").ToString.Contains("PembayaranMinimum") Then
                        pisah = Grid_Invoice_ParentDS.GetRowValues(e.VisibleIndex, "Keterangan").ToString.Split("-")

                        ChkMiniByr.Checked = True
                        txtMinByr.Text = pisah(1)
                    End If

                    If Grid_Invoice_ParentDS.GetRowValues(e.VisibleIndex, "KeteranganOngkosAngkut").ToString <> "" Then
                        ChkOngkos.Checked = True

                        pisah = Grid_Invoice_ParentDS.GetRowValues(e.VisibleIndex, "KeteranganOngkosAngkut").ToString.Split("-")
                        TxtOngkos.Text = pisah(0)
                        TxtJumlahOngkosAngkutan.Text = pisah(0)

                    End If
                    Cek_Asuransi(Grid_Invoice_ParentDS.GetRowValues(e.VisibleIndex, "No_Invoice").ToString)
                    Load_Grid_Invoice_Child(Grid_Invoice_ParentDS.GetRowValues(e.VisibleIndex, "No_Invoice").ToString)

                Case "Delete"
                    Delete(Grid_Invoice_ParentDS.GetRowValues(e.VisibleIndex, "No_Invoice").ToString)
            End Select
        Catch ex As Exception
            Throw New Exception(ex.ToString)
        End Try
    End Sub


    Private Sub Grid_Invoice_DS_Detail_RowCommand(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewRowCommandEventArgs) Handles Grid_Invoice_DS_Detail.RowCommand
        Try

            clear_Label()

            Select Case e.CommandArgs.CommandName

                Case "Edit"
                    If HFMode.Value = "Insert" Then
                        HFModeItem.Value = "Edit"

                        DDLSatuan.SelectedValue = Grid_Invoice_DS_Detail.GetRowValues(e.VisibleIndex, "IDJenisPembayaran").ToString
                        TxtHargaByr.Text = UbahKoma(Grid_Invoice_DS_Detail.GetRowValues(e.VisibleIndex, "Harga").ToString)
                        TxtQty.Text = UbahKoma(Grid_Invoice_DS_Detail.GetRowValues(e.VisibleIndex, "Quantity").ToString)
                        TxtNamaBarang.Text = Grid_Invoice_DS_Detail.GetRowValues(e.VisibleIndex, "NamaBarang").ToString
                        DDLSatuanbrg.SelectedValue = Grid_Invoice_DS_Detail.GetRowValues(e.VisibleIndex, "IDSatuan").ToString
                        TxtUkuran.Text = UbahKomaDlmUkuran(Grid_Invoice_DS_Detail.GetRowValues(e.VisibleIndex, "Ukuran").ToString)
                        Show_ukuran(Grid_Invoice_DS_Detail.GetRowValues(e.VisibleIndex, "JenisPembayaran").ToString)
                    Else
                        HFModeItem.Value = "Edit"
                        HFIDDetail.Value = Grid_Invoice_DS_Detail.GetRowValues(e.VisibleIndex, "No").ToString
                        DDLSatuan.SelectedValue = Grid_Invoice_DS_Detail.GetRowValues(e.VisibleIndex, "IDJenisPembayaran").ToString
                        TxtHargaByr.Text = UbahKoma(Grid_Invoice_DS_Detail.GetRowValues(e.VisibleIndex, "Harga").ToString)
                        TxtQty.Text = UbahKoma(Grid_Invoice_DS_Detail.GetRowValues(e.VisibleIndex, "Quantity").ToString)
                        TxtNamaBarang.Text = Grid_Invoice_DS_Detail.GetRowValues(e.VisibleIndex, "NamaBarang").ToString
                        DDLSatuanbrg.SelectedValue = Grid_Invoice_DS_Detail.GetRowValues(e.VisibleIndex, "IDSatuan").ToString
                        TxtUkuran.Text = UbahKomaDlmUkuran(Grid_Invoice_DS_Detail.GetRowValues(e.VisibleIndex, "Ukuran").ToString)
                        Show_ukuran(Grid_Invoice_DS_Detail.GetRowValues(e.VisibleIndex, "JenisPembayaran").ToString)
                    End If


                Case "Delete"
                    If HFMode.Value = "Insert" Then
                        Remove_Item(e.VisibleIndex)
                    Else
                        Remove_Item(e.VisibleIndex)
                        Remove_itemDB(Grid_Invoice_DS_Detail.GetRowValues(e.VisibleIndex, "No").ToString, HFNoInvoice.Value)
                    End If
            End Select
        Catch ex As Exception
            Throw New Exception(ex.ToString)
        End Try
    End Sub


    Private Sub load_grid_invoice_header()
        Try

            Dim pdt As New DataTable

            pdt.Columns.Add(New DataColumn("ID", GetType(String)))
            pdt.Columns.Add(New DataColumn("No_Invoice", GetType(String)))
            pdt.Columns.Add(New DataColumn("NamaKapal", GetType(String)))
            pdt.Columns.Add(New DataColumn("IDKapal", GetType(String)))
            pdt.Columns.Add(New DataColumn("DaerahDitujukan", GetType(String)))
            pdt.Columns.Add(New DataColumn("Total", GetType(Double)))
            pdt.Columns.Add(New DataColumn("TglInvoice", GetType(DateTime)))
            pdt.Columns.Add(New DataColumn("Ditujukan", GetType(String)))
            pdt.Columns.Add(New DataColumn("Paid", GetType(String)))
            pdt.Columns.Add(New DataColumn("Keterangan", GetType(String)))
            pdt.Columns.Add(New DataColumn("KeteranganOngkosAngkut", GetType(String)))
            pdt.Columns.Add(New DataColumn("CodeCust", GetType(String)))
            pdt.Columns.Add(New DataColumn("YgInput", GetType(String)))

            sqlstring = "Select IH.ID, IH.No_Invoice,mc.Nama_Customer,mc.Area as DaerahDitujukan, " & _
                        "K.Nama_Kapal, IH.InvoiceDate, IH.Ditujukan, IH.Total,IH.Paid, IH.Keterangan, IH.KeteranganOngkosAngkut, IH.KapalID, IH.CodeCust, IH.Username " & _
                        "from InvoiceHeader IH " & _
                        "left join MasterCustomer mc on ih.codecust = mc.Kode_Customer " & _
                        "left join Kapal K on ih.KapalID = K.IDDetail " & _
                        "where " & _
                        "K.[status] = 1 " & _
                        "and IH.[status] = 1 and IH.DP = 'No' and IH.KeteranganDS = 'Yes' ORDER BY IH.ID DESC "
            DS = SQLExecuteQuery(sqlstring)
            DT = DS.Tables(0)

            If DT.Rows.Count > 0 Then
                For i As Integer = 0 To DT.Rows.Count - 1
                    With DT.Rows(i)
                        DR = pdt.NewRow
                        DR("ID") = .Item("ID").ToString
                        DR("No_Invoice") = .Item("No_Invoice").ToString
                        DR("Ditujukan") = .Item("Nama_Customer")
                        DR("NamaKapal") = .Item("Nama_Kapal")
                        DR("IDKapal") = .Item("KapalID")
                        DR("DaerahDitujukan") = .Item("DaerahDitujukan")
                        DR("Total") = .Item("Total")
                        DR("TglInvoice") = CDate(.Item("invoicedate").ToString).ToString("dd MMMM yyyy")
                        DR("Paid") = .Item("Paid")
                        DR("Keterangan") = .Item("Keterangan")
                        DR("KeteranganOngkosAngkut") = .Item("KeteranganOngkosAngkut")
                        DR("CodeCust") = .Item("CodeCust")
                        DR("YgInput") = .Item("Username")
                    End With
                    pdt.Rows.Add(DR)
                Next

                If Session("namaroles").ToString.Trim = "Admin" Or Session("namaroles").ToString.Trim = "Master Accounting" Or Session("namaroles").ToString.Trim = "Accounting" Then
                    Grid_Invoice_ParentDS.Columns("YgInput").Visible = True
                Else
                    Grid_Invoice_ParentDS.Columns("YgInput").Visible = False
                End If

                Session("Grid_Header_DS_Invoice") = pdt
                Grid_Invoice_ParentDS.DataSource = pdt
                Grid_Invoice_ParentDS.KeyFieldName = "ID"
                Grid_Invoice_ParentDS.DataBind()

            Else
                Grid_Invoice_ParentDS.DataSource = Nothing
                Grid_Invoice_ParentDS.DataBind()
            End If


        Catch ex As Exception
            Throw New Exception("<b> Error load grid invoice : </b>" & ex.ToString)
        End Try
    End Sub

    Private Sub Load_Grid_Invoice_Child(ByVal No_Invoice As String)
        Try

            Dim idt As New DataTable
            Dim cdt As New DataTable
            Dim zdr As DataRow
            Dim NamaSatuan As String = ""
            Dim NamaBarang As String = ""
            Dim totalbayar As Double = 0

            With cdt.Columns
                .Add(New DataColumn("No", GetType(String)))
                .Add(New DataColumn("IDJenisPembayaran", GetType(String)))
                .Add(New DataColumn("JenisPembayaran", GetType(String)))
                .Add(New DataColumn("Harga", GetType(String)))
                .Add(New DataColumn("Quantity", GetType(String)))
                .Add(New DataColumn("NamaBarang", GetType(String)))
                .Add(New DataColumn("IDSatuan", GetType(String)))
                .Add(New DataColumn("Satuan", GetType(String)))
                .Add(New DataColumn("Ukuran", GetType(String)))
                .Add(New DataColumn("TotalByr", GetType(String)))
            End With

            sqlstring = "select IDS.IDDetail, IDS.No_Invoice, IDS.NamaBarang, IDS.Quantity, MSO.Nama_Satuan ,ids.HargaSatuan, " & _
                        "MHD.NamaHarga, IDS.TotalUkuran,iDS.HargaID,SatuanID, " & _
                        "CAST(IDS.TotalUkuran as decimal(30,3)) * IDS.HargaSatuan as TotalHarga " & _
                        "FROM InvoiceDetailDS IDS " & _
                        "JOIN MasterHargaDefault MHD ON IDS.HargaID = MHD.ID " & _
                        "JOIN MasterSatuanOther MSO ON IDS.SatuanID = MSO.IDDetail " & _
                        "WHERE IDS.status <> 0 And MHD.status <> 0 And MSO.status <> 0 " & _
                        "AND IDS.No_Invoice = '" & No_Invoice & "' "

            DS = SQLExecuteQuery(sqlstring)
            DT = DS.Tables(0)

            If DT.Rows.Count > 0 Then
                For i As Integer = 0 To DT.Rows.Count - 1
                    With DT.Rows(i)
                        zdr = cdt.NewRow
                        zdr("No") = .Item("IDDetail").ToString
                        zdr("TotalByr") = CDbl(.Item("TotalHarga").ToString)
                        zdr("Harga") = CDbl(.Item("HargaSatuan").ToString)
                        zDR("NamaBarang") = .Item("NamaBarang").ToString
                        zDR("Quantity") = .Item("Quantity").ToString
                        zdr("JenisPembayaran") = .Item("NamaHarga").ToString
                        zdr("Ukuran") = .Item("TotalUkuran").ToString
                        zdr("Satuan") = .Item("Nama_Satuan").ToString
                        zdr("TotalByr") = .Item("TotalHarga").ToString
                        zdr("IDSatuan") = .Item("SatuanID").ToString
                        zdr("IDJenisPembayaran") = .Item("HargaID").ToString
                        cdt.Rows.Add(zdr)

                    End With
                Next
            End If

            Session("Grid_Invoice_DS_Detail") = cdt
            Grid_Invoice_DS_Detail.KeyFieldName = "No"
            Grid_Invoice_DS_Detail.DataSource = cdt
            Grid_Invoice_DS_Detail.DataBind()

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
            Dim NamaSatuan As String = ""
            Dim NamaBarang As String = ""

            With zDT.Columns
                .Add(New DataColumn("IDDetail", GetType(String)))
                .Add(New DataColumn("No_Invoice", GetType(String)))
                .Add(New DataColumn("TotalHarga", GetType(Double)))
                .Add(New DataColumn("HargaSatuan", GetType(Double)))
                .Add(New DataColumn("Nama_Barang", GetType(String)))
                .Add(New DataColumn("Quantity", GetType(String)))
                .Add(New DataColumn("NamaHarga", GetType(String)))
                .Add(New DataColumn("TotalUkuran", GetType(String)))
            End With

            sqlstring = "select IDS.IDDetail, IDS.No_Invoice, IDS.NamaBarang, CAST(IDS.Quantity as varchar) + ' ' +  MSO.Nama_Satuan as Quantity,ids.HargaSatuan, " & _
                        "MHD.NamaHarga, IDS.TotalUkuran, " & _
                        "CAST(IDS.TotalUkuran as decimal(30,3)) * IDS.HargaSatuan as TotalHarga " & _
                        "FROM InvoiceDetailDS IDS " & _
                        "JOIN MasterHargaDefault MHD ON IDS.HargaID = MHD.ID " & _
                        "JOIN MasterSatuanOther MSO ON IDS.SatuanID = MSO.IDDetail " & _
                        "WHERE IDS.status <> 0 And MHD.status <> 0 And MSO.status <> 0 " & _
                        "AND IDS.No_Invoice = '" & grid.GetMasterRowFieldValues("No_Invoice") & "' "

            DS = SQLExecuteQuery(sqlstring)
            DT = DS.Tables(0)

            If DT.Rows.Count > 0 Then
                For i As Integer = 0 To DT.Rows.Count - 1
                    With DT.Rows(i)
                        zDR = zDT.NewRow
                        zDR("IDDetail") = .Item("IDDetail").ToString
                        zDR("No_Invoice") = .Item("No_Invoice").ToString
                        zDR("TotalHarga") = CDbl(.Item("TotalHarga").ToString)
                        zDR("HargaSatuan") = CDbl(.Item("HargaSatuan").ToString)
                        zDR("Nama_Barang") = .Item("NamaBarang").ToString
                        zDR("Quantity") = .Item("Quantity").ToString
                        zDR("NamaHarga") = .Item("NamaHarga").ToString
                        zDR("TotalUkuran") = .Item("TotalUkuran").ToString
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

    'Protected Sub grid_Asuransi_DataSelect(ByVal sender As Object, ByVal e As EventArgs)
    '    Try
    '        'Call load_grid_Asuransi(TryCast(sender, ASPxGridView))
    '    Catch ex As Exception
    '        Response.Write("Error Load Grid Child DataSelect  :<BR>" & ex.ToString)
    '    End Try
    'End Sub

    'Private Sub load_grid_Asuransi(ByVal grid As ASPxGridView)
    '    Try
    '        Dim asDT As New DataTable
    '        Dim asDS As DataSet
    '        Dim zDT As New DataTable
    '        Dim zDR As DataRow

    '        With zDT.Columns
    '            .Add(New DataColumn("NoAsuransi", GetType(String)))
    '            .Add(New DataColumn("HargaAsuransi", GetType(Double)))
    '            .Add(New DataColumn("Premi", GetType(String)))
    '            .Add(New DataColumn("Polis", GetType(Double)))
    '            .Add(New DataColumn("TotalAsuransi", GetType(Double)))
    '        End With

    '        sqlstring = "SELECT NoAsuransi, HargaAsuransi, Premi, Polis, TotalAsuransi FROM InvoiceHeader WHERE No_Invoice = '" & grid.GetMasterRowFieldValues("No_Invoice") & "' AND [status] = 1 "
    '        asDS = SQLExecuteQuery(sqlstring)
    '        asDT = asDS.Tables(0)

    '        If asDT.Rows.Count > 0 Then
    '            For i As Integer = 0 To asDT.Rows.Count - 1
    '                With asDT.Rows(i)
    '                    zDR = zDT.NewRow
    '                    zDR("NoAsuransi") = .Item("NoAsuransi").ToString
    '                    zDR("HargaAsuransi") = CDbl(.Item("HargaAsuransi").ToString)

    '                    zDR("Premi") = .Item("Premi").ToString & " %"
    '                    zDR("Polis") = CDbl(.Item("Polis").ToString)
    '                    zDR("TotalAsuransi") = CDbl(.Item("TotalAsuransi").ToString)
    '                    zDT.Rows.Add(zDR)

    '                End With
    '            Next
    '        End If

    '        grid.DataSource = zDT
    '    Catch ex As Exception
    '        Throw New Exception("<b>Error Load Grid Asuransi :</b>" & ex.ToString)
    '    End Try
    'End Sub

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

    Private Sub create_session()
        Try
            Dim adt As New DataTable

            adt.Columns.Add(New DataColumn("No", GetType(String)))
            aDT.Columns.Add(New DataColumn("IDJenisPembayaran", GetType(String)))
            aDT.Columns.Add(New DataColumn("JenisPembayaran", GetType(String)))
            adt.Columns.Add(New DataColumn("Harga", GetType(Double)))
            aDT.Columns.Add(New DataColumn("Quantity", GetType(String)))
            aDT.Columns.Add(New DataColumn("NamaBarang", GetType(String)))
            aDT.Columns.Add(New DataColumn("IDSatuan", GetType(String)))
            aDT.Columns.Add(New DataColumn("Satuan", GetType(String)))
            aDT.Columns.Add(New DataColumn("Ukuran", GetType(String)))
            aDT.Columns.Add(New DataColumn("TotalByr", GetType(Double)))

            Session("Grid_Invoice_DS_Detail") = aDT
            Grid_Invoice_DS_Detail.DataSource = aDT
            Grid_Invoice_DS_Detail.KeyFieldName = "No"
            Grid_Invoice_DS_Detail.DataBind()

        Catch ex As Exception
            Response.Write("Error Load Create Session :<BR>" & ex.ToString)
        End Try

    End Sub

#End Region

#Region "DDL"

    Private Sub load_DDLSatuanBayar()
        Try
            sqlstring = " SELECT ID,NamaHarga from MasterHargaDefault where [status] = 1 order by NamaHarga"
            Dim dt As DataTable = SQLExecuteQuery(sqlstring).Tables(0)
            With DDLSatuan
                .DataSource = dt
                .DataTextField = "NamaHarga"
                .DataValueField = "ID"
                .DataBind()
            End With
            DDLSatuan.Items.Insert(0, "-Please Select Satuan-")
        Catch ex As Exception
            Throw New Exception("<b>Error LoadDDL :</b>" & ex.ToString)
        End Try
    End Sub

    Private Sub load_DDLSatuanBrg()
        Try
            sqlstring = " SELECT IDDetail As ID ,Nama_Satuan from MasterSatuanOther where [status] = 1 order by Nama_Satuan"
            Dim dt As DataTable = SQLExecuteQuery(sqlstring).Tables(0)
            With DDLSatuanbrg
                .DataSource = dt
                .DataTextField = "Nama_Satuan"
                .DataValueField = "ID"
                .DataBind()
            End With
            DDLSatuanbrg.Items.Insert(0, "-Please Select Satuan-")
        Catch ex As Exception
            Throw New Exception("<b>Error LoadDDL :</b>" & ex.ToString)
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

    Protected Sub DDLSatuan_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles DDLSatuan.SelectedIndexChanged
        Try
            Select Case DDLSatuan.SelectedItem.ToString.ToUpper

                Case "KUBIK"
                    Lblukuran.Text = "meter"
                Case "TON"
                    Lblukuran.Text = "ton"
                Case "UNIT"
                    Lblukuran.Text = "unit"
                Case "CONTAINER"
                    Lblukuran.Text = "container"

            End Select

            If DDLSatuan.SelectedIndex = 0 Then
                Lblukuran.Text = ""
            End If

            TxtNamaPengirim.Text = HFNmCust.Value
            TxtDaerahDitujukan.Text = HFDaerahDitujukan.Value

        Catch ex As Exception
            Throw New Exception(ex.ToString)
        End Try
    End Sub

#End Region

    Private Sub Show_ukuran(ByRef UkuranPembayaran As String)
        Try
            Select Case UkuranPembayaran.ToUpper

                Case "KUBIK"
                    Lblukuran.Text = "meter"
                Case "TON"
                    Lblukuran.Text = "ton"
                Case "UNIT"
                    Lblukuran.Text = "unit"
                Case "CONTAINER"
                    Lblukuran.Text = "container"

            End Select
        Catch ex As Exception
            Throw New Exception(ex.ToString)
        End Try
    End Sub




End Class