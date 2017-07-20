Imports System.Data
Imports System.Data.SqlClient
Imports DevExpress.Web.ASPxGridView
Partial Public Class InvoiceDP
    Inherits System.Web.UI.Page
    Private DS As DataSet
    Private DT As DataTable
    Private DR As DataRow
    Private sqlstring As String
    Dim iDT As New DataTable
    Dim hasil As Integer
    Dim result As String

#Region "PAGE"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try
            If Session("UserID") = Nothing Then
                FormsAuthentication.SignOut()
                Response.Redirect("Index.aspx")
            End If


            If Not Page.IsPostBack Then
                Session("Grid_Header_InvoiceDP") = Nothing
                HFMode.Value = "Insert"
                HFModeItem.Value = "Insert"
                HFmuatBarangID.Value = ""
                HFIDKapal.Value = ""
                DtInvoice.Date = Today

                load_grid_invoice_header()

            End If
            Show_Text_Box()

            If Not Session("Grid_Header_InvoiceDP") Is Nothing Then
                Grid_Invoice_Parent.DataSource = CType(Session("Grid_Header_InvoiceDP"), DataTable)
                Grid_Invoice_Parent.DataBind()
            End If
        Catch ex As Exception
            Response.Write(ex.ToString)
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
            Dim hasil As String = ""

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



                value = no.ToString("0000") & "/" & Singkatan & "/" & aliaskapal & "/" & CekBulan(bulan) & "/" & Date.Today.ToString("yy")
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

                value = "B" & nodpn.Substring(1, nodpn.Length - 1) & "/" & Singkatan & "/" & aliaskapal & "/" & CekBulan(bulan) & "/" & Date.Today.ToString("yy")

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
   

                value = no.ToString("0000") & "/" & Singkatan & "/" & aliaskapal & "/" & CekBulan(bulan) & "/" & Date.Today.ToString("yy")

            End If


            Return value
        Catch ex As Exception
            Throw New Exception("<b>Error load invoice : </b>" & ex.ToString)
        End Try
    End Function
    Private Sub Show_Text_Box()
        TxtKapal.Text = HFNamakapal.Value
        TxtNamaPengirim.Text = HFNamaPengirim.Value
        TxtTujuan.Text = HFTujuan.Value
        TxtPenerima.Text = HFPenerima.Value


    End Sub
#End Region

#Region "GRID"
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
            pdt.Columns.Add(New DataColumn("Paid", GetType(Double)))
            pdt.Columns.Add(New DataColumn("Total", GetType(Double)))
            pdt.Columns.Add(New DataColumn("TglInvoice", GetType(DateTime)))
            pdt.Columns.Add(New DataColumn("Ditujukan", GetType(String)))
            pdt.Columns.Add(New DataColumn("NamaBarang", GetType(String)))
            pdt.Columns.Add(New DataColumn("Keterangan", GetType(String)))
            pdt.Columns.Add(New DataColumn("YgInput", GetType(String)))

            sqlstring = "Select IH.ID, IH.MuatBarangID, IH.No_Invoice,mc.Nama_Customer, " & _
                        "mb.Penerima, mq.Tujuan, mb.Kapal, K.Nama_Kapal, IH.InvoiceDate, IH.Ditujukan, IH.DaerahDitujukan, IH.Total,IH.Paid, IH.Keterangan, IH.NamaBarang, IH.Username " & _
                        "from InvoiceHeader IH " & _
                        "left join MuatBarang MB ON IH.MuatbarangID = MB.Mb_No " & _
                        "left join MasterCustomer mc on mb.Customer_Id = mc.Kode_Customer " & _
                        "left join WarehouseHeader wh on mb.WarehouseHeaderID = wh.WarehouseItem_Code " & _
                        "left join MasterQuotation mq on wh.Quotation_No = mq.Quotation_No " & _
                        "left join Kapal K on mb.Kapal = K.IDDetail " & _
                        "where " & _
                        "(mq.[status] = 1 Or mq.[status] = 2) " & _
                        "and MB.[status] <> 0 " & _
                        "and wh.[status] = 1 and IH.[status] = 1 and  IH.DP='Yes' "

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
                        DR("Paid") = .Item("Paid")
                        DR("DaerahDitujukan") = .Item("DaerahDitujukan")
                        DR("Tujuan") = .Item("Tujuan")
                        DR("Total") = .Item("Total")
                        DR("TglInvoice") = CDate(.Item("invoicedate").ToString).ToString("MM/dd/yyyy")
                        DR("Ditujukan") = .Item("Ditujukan")
                        DR("NamaBarang") = .Item("NamaBarang").ToString
                        DR("Keterangan") = .Item("Keterangan").ToString
                        DR("NamaKapal") = .Item("Nama_Kapal")
                        DR("YgInput") = .Item("Username").ToString
                    End With
                    pdt.Rows.Add(DR)
                Next

                If Session("namaroles").ToString.Trim = "Admin" Or Session("namaroles").ToString.Trim = "Master Accounting" Or Session("namaroles").ToString.Trim = "Accounting" Then
                    Grid_Invoice_Parent.Columns("YgInput").Visible = True
                Else
                    Grid_Invoice_Parent.Columns("YgInput").Visible = False
                End If

                Session("Grid_Header_InvoiceDP") = pdt
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
            Dim NamaSatuan As String = ""
            Dim NamaBarang As String = ""
            'Dim iDS As DataSet
            'Dim istr As String

            'With zDT.Columns
            '    .Add(New DataColumn("ID", GetType(String)))
            '    .Add(New DataColumn("Nama_Barang", GetType(String)))
            '    .Add(New DataColumn("Satuan", GetType(String)))
            '    .Add(New DataColumn("NamaPaket", GetType(String)))
            '    .Add(New DataColumn("Tanggal", GetType(String)))
            '    .Add(New DataColumn("Harga", GetType(Double)))
            '    .Add(New DataColumn("Paid", GetType(Double)))
            'End With

            With zDT.Columns
                .Add(New DataColumn("IDDetail", GetType(String)))
                .Add(New DataColumn("Paid", GetType(String)))
                .Add(New DataColumn("Hargatotal", GetType(Double)))
                .Add(New DataColumn("Harga", GetType(Double)))
                .Add(New DataColumn("Nama_Barang", GetType(String)))
                .Add(New DataColumn("JenisPembayaran", GetType(String)))
            End With
         

            'sqlstring = "select id.IDDetail as ID,wd.Container,wd.Nama_Barang,qd.Nama_Barang as namaquo,wd.Nama_Satuan,id.Hargatotal,mh.NamaHarga,id.Paid, " & _
            '                    "(SELECT CASE  " & _
            '                    "	When wd.Container='true' then " & _
            '                     "	( " & _
            '                     "	(wd.Quantity * qd.Harga) " & _
            '                    "		) " & _
            '                    "when wd.Container='kubikasi' then " & _
            '                      "( " & _
            '                      "	((wd.Panjang * wd.Lebar*wd.Tinggi * wd.Quantity ) * (qd.Harga)) " & _
            '                      ") " & _
            '                    "	else " & _
            '                    "		( " & _
            '                    "		(SELECT case  " & _
            '                    "			when (mh.NamaHarga = 'Kubik' or mh.NamaHarga = 'kubik') then " & _
            '                    "				( " & _
            '                    "					(cast(wd.Panjang * wd.Lebar*wd.Tinggi * wd.Quantity AS decimal(30,3)) * (qd.Harga)) " & _
            '                    "				) " & _
            '                    "			when mh.NamaHarga = 'Ton' or mh.NamaHarga = 'ton' or mh.NamaHarga = 'Berat' or mh.NamaHarga ='berat' then " & _
            '                    "				( " & _
            '                    "					(cast((wd.Berat/100) * wd.Quantity as Decimal(30,3))*(qd.Harga)) " & _
            '                    "				) " & _
            '                    "			when mh.NamaHarga = 'Unit' or mh.NamaHarga = 'unit' then " & _
            '                    "				( " & _
            '                    "					(wd.Quantity * qd.Harga) " & _
            '                    "				) " & _
            '                    "			else( " & _
            '                    "					(wd.Quantity * qd.Harga) " & _
            '                    "				) " & _
            '                    "			end) " & _
            '                    "		) " & _
            '                    "	end) as Total " & _
            '                    "from InvoiceDetail id ,V_Warehouse_Satuan wd ,QuotationDetail qd ,MasterHargaDefault mh  " & _
            '                    "where(ID.MuatBarangDetailID = wd.IDDetail And ID.Mb_No = wd.WarehouseItem_Code) And " & _
            '                    "( wd.Quotation_No = qd.Quotation_No and wd.QuotationDetailID= qd.IDDetail ) " & _
            '                    "and qd.SatuanID = mh.ID  " & _
            '                    "and  wd.status = 1 and qd.status <>0 and mh.status = 1 and id.status = 1 and id.No_Invoice = '" & grid.GetMasterRowFieldValues("No_Invoice") & "' and DP='Yes' "
            'DS = SQLExecuteQuery(sqlstring)
            'DT = DS.Tables(0)

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
            'If DT.Rows.Count > 0 Then
            '    For i As Integer = 0 To DT.Rows.Count - 1
            '        With DT.Rows(i)
            '            If .Item("Container").ToString = "true" Then
            '                NamaSatuan = "Container"
            '                NamaBarang = .Item("namaquo").ToString
            '            Else
            '                NamaSatuan = .Item("Nama_Satuan").ToString
            '                NamaBarang = .Item("Nama_Barang").ToString
            '            End If
            '            zDR = zDT.NewRow
            '            zDR("ID") = .Item("ID").ToString
            '            zDR("Nama_Barang") = NamaBarang
            '            zDR("Satuan") = .Item("NamaHarga").ToString
            '            zDR("NamaPaket") = NamaSatuan
            '            zDR("Harga") = CDbl(.Item("HargaTotal").ToString)
            '            zDR("Paid") = .Item("Paid").ToString
            '            zDT.Rows.Add(zDR)

            '        End With
            '    Next
            'End If



            'sqlstring = "SELECT IVD.ID,IVD.Mb_No, WHD.Container, IVD.MuatBarangDetailID " & _
            '            "From InvoiceDetail IVD " & _
            '            "JOIN V_Warehouse_Satuan WHD on (IVD.MuatBarangDetailID = WHD.IDDetail and WHD.WarehouseItem_Code = IVD.Mb_No)  " & _
            '            "JOIN QuotationDetail QD on (WHD.QuotationDetailID = QD.IDDetail and QD.Quotation_No= WHD.Quotation_No) " & _
            '            "WHERE IVD.No_Invoice = '" & grid.GetMasterRowFieldValues("No_Invoice") & "' " & _
            '            "AND WHD.[status] = 1 " & _
            '            "AND IVD.[status] = 1 " & _
            '            "AND ( QD.[status] = 1 or QD.[status] = 2 ) "
            'DS = SQLExecuteQuery(sqlstring)
            'DT = DS.Tables(0)

            'If DT.Rows.Count > 0 Then
            '    For i As Integer = 0 To DT.Rows.Count - 1
            '        zDR = zDT.NewRow
            '        With DT.Rows(i)
            '            If DT.Rows(i).Item("Container").ToString = "true" Then
            '                istr = "SELECT IVD.ID, IVD.No_Invoice as InvoiceHeaderID, IVD.MuatBarangDetailID,whd.Container, QD.Nama_Barang, WHD.Quantity, QD.Harga, IVD.HargaTotal " & _
            '                        "From InvoiceDetail IVD " & _
            '                        "JOIN V_Warehouse_Satuan WHD on (IVD.MuatBarangDetailID = WHD.IDDetail and WHD.WarehouseItem_Code = IVD.Mb_No ) " & _
            '                        "JOIN ContainerHeader CH ON WHD.Nama_Barang = CH.ContainerCode " & _
            '                        "JOIN QuotationDetail QD on (WHD.QuotationDetailID = QD.IDDetail and WHD.Quotation_No = QD.Quotation_No) " & _
            '                        "WHERE IVD.No_Invoice = '" & grid.GetMasterRowFieldValues("No_Invoice") & "' " & _
            '                        "AND IVD.MuatBarangDetailID = '" & DT.Rows(i).Item("MuatBarangDetailID").ToString & "' " & _
            '                        "AND IVD.Mb_No = '" & DT.Rows(i).Item("Mb_No").ToString & "' " & _
            '                        "AND IVD.[status] = 1 " & _
            '                        "AND WHD.[status] = 1 " & _
            '                        "AND CH.[status] = 1 " & _
            '                        "AND ( QD.[status] = 1 or QD.[status] = 2 )"
            '                iDS = SQLExecuteQuery(istr)
            '                iDT = iDS.Tables(0)

            '                If iDT.Rows.Count > 0 Then
            '                    For j As Integer = 0 To iDT.Rows.Count - 1
            '                        With iDT.Rows(j)

            '                            zDR("ID") = .Item("ID").ToString
            '                            zDR("InvoiceHeaderID") = .Item("InvoiceHeaderID").ToString
            '                            zDR("Nama_Barang") = .Item("Nama_Barang").ToString
            '                            zDR("Quantity") = .Item("Quantity").ToString
            '                            zDR("Harga_Satuan") = .Item("Harga").ToString
            '                            zDR("Satuan") = "Container"
            '                            zDR("NamaPaket") = "Container"
            '                            zDR("Total") = .Item("HargaTotal").ToString
            '                            zDT.Rows.Add(zDR)


            '                        End With
            '                    Next
            '                End If
            '            Else

            '                istr = "SELECT Distinct(MHD.NamaHarga) " & _
            '                        "From InvoiceDetail IVD " & _
            '                        "LEFT JOIN V_Warehouse_Satuan WHD ON (IVD.MuatBarangDetailID = WHD.IDDetail and WHD.WarehouseItem_Code = IVD.Mb_No )  " & _
            '                        "LEFT JOIN QuotationDetail QD ON (WHD.QuotationDetailID = QD.IDDetail and QD.Quotation_No = WHD.Quotation_No ) " & _
            '                        "LEFT JOIN MasterHargaDefault MHD ON QD.SatuanID = MHD.ID " & _
            '                        "WHERE IVD.No_Invoice = '" & grid.GetMasterRowFieldValues("No_Invoice") & "' " & _
            '                        "AND IVD.MuatBarangDetailID = '" & DT.Rows(i).Item("MuatBarangDetailID").ToString & "' " & _
            '                        "AND IVD.Mb_No = '" & DT.Rows(i).Item("Mb_No").ToString & "' " & _
            '                        "AND IVD.[status] = 1 " & _
            '                        "AND WHD.[status] = 1  " & _
            '                        "AND ( QD.[status] = 1 or QD.[status] = 2 )  " & _
            '                        "AND MHD.[status] = 1"
            '                NamaSatuan = SQLExecuteScalar(istr)

            '                If NamaSatuan.ToString = "Ton" Or NamaSatuan.ToString = "ton" Or NamaSatuan.ToString = "berat" Or NamaSatuan.ToString = "Berat" Then
            '                    sqlstring = "SELECT IVD.ID, IVD.No_Invoice as InvoiceHeaderID, WHD.Nama_Barang, MHD.NamaHarga as Satuan, WHD.Nama_Satuan as Namapaket, QD.Harga, " & _
            '                                "WHD.Berat,WHD.Quantity, (WHD.Berat * WHD.Quantity) as totalberat " & _
            '                                "FROM InvoiceDetail IVD " & _
            '                                "LEFT JOIN V_Warehouse_Satuan WHD ON (IVD.MuatBarangDetailID = WHD.IDDetail and WHD.WarehouseItem_Code = IVD.Mb_No)  " & _
            '                                "LEFT JOIN QuotationDetail QD ON (WHD.QuotationDetailID = QD.IDDetail and WHD.Quotation_No = QD.Quotation_No ) " & _
            '                                "LEFT JOIN MasterHargaDefault MHD ON QD.SatuanID = MHD.ID " & _
            '                                "WHERE IVD.No_Invoice = '" & grid.GetMasterRowFieldValues("No_Invoice") & "' " & _
            '                                "AND IVD.MuatBarangDetailID = '" & DT.Rows(i).Item("MuatBarangDetailID").ToString & "' " & _
            '                                "AND IVD.Mb_No = '" & DT.Rows(i).Item("Mb_No").ToString & "' " & _
            '                                "AND WHD.[status] = 1 " & _
            '                                "AND ( QD.[status] = 1 or QD.[status] = 2 )  " & _
            '                                "AND MHD.[status] = 1 " & _
            '                                "AND IVD.[status] = 1 " & _
            '                                "Order BY WHD.Nama_Barang"
            '                    DS = SQLExecuteQuery(sqlstring)
            '                    cDT = DS.Tables(0)

            '                    If cDT.Rows.Count > 0 Then
            '                        For j As Integer = 0 To cDT.Rows.Count - 1
            '                            With cDT.Rows(j)
            '                                zDR("MuatBarangDetailID") = .Item("ID").ToString
            '                                zDR("Nama_Barang") = .Item("Nama_Barang").ToString
            '                                zDR("Satuan") = .Item("Satuan").ToString
            '                                zDR("NamaPaket") = .Item("Namapaket").ToString
            '                                zDR("Harga_Satuan") = .Item("Harga").ToString
            '                                zDR("JumlahSatuan") = .Item("Berat").ToString
            '                                zDR("Quantity") = .Item("Quantity").ToString
            '                                zDR("Total") = .Item("totalberat").ToString
            '                                zDT.Rows.Add(zDR)

            '                            End With

            '                        Next

            '                    End If

            '                ElseIf NamaSatuan.ToString = "Kubik" Or NamaSatuan.ToString = "kubik" Then

            '                    sqlstring = "SELECT IVD.ID, IVD.No_Invoice as InvoiceHeaderID, WHD.Nama_Barang, MHD.NamaHarga as Satuan, WHD.Nama_Satuan as Namapaket,  QD.Harga, " & _
            '                                "(WHD.Lebar * WHD.Tinggi * WHD.Tinggi ) as volume,WHD.Quantity, (WHD.panjang * WHD.Lebar * WHD.Tinggi * WHD.Quantity) as totalvolume " & _
            '                                "FROM InvoiceDetail IVD " & _
            '                                "LEFT JOIN V_Warehouse_Satuan WHD ON ( IVD.MuatBarangDetailID = WHD.IDDetail and WHD.WarehouseItem_Code = IVD.Mb_No ) " & _
            '                                "LEFT JOIN QuotationDetail QD ON (WHD.QuotationDetailID = QD.IDDetail and QD.Quotation_No = WHD.Quotation_No ) " & _
            '                                "LEFT JOIN MasterHargaDefault MHD ON QD.SatuanID = MHD.ID " & _
            '                                "WHERE IVD.No_Invoice = '" & grid.GetMasterRowFieldValues("No_Invoice") & "' " & _
            '                                "AND IVD.MuatBarangDetailID = '" & DT.Rows(i).Item("MuatBarangDetailID").ToString & "' " & _
            '                                "AND IVD.Mb_No = '" & DT.Rows(i).Item("Mb_No").ToString & "' " & _
            '                               "AND WHD.[status] = 1 " & _
            '                                "AND ( QD.[status] = 1 or QD.[status] = 2 )  " & _
            '                                "AND MHD.[status] = 1 " & _
            '                                "AND IVD.[status] = 1 " & _
            '                                "Order BY WHD.Nama_Barang"
            '                    DS = SQLExecuteQuery(sqlstring)
            '                    cDT = DS.Tables(0)

            '                    If cDT.Rows.Count > 0 Then
            '                        For j As Integer = 0 To cDT.Rows.Count - 1
            '                            With cDT.Rows(j)
            '                                zDR("MuatBarangDetailID") = .Item("ID").ToString
            '                                zDR("Nama_Barang") = .Item("Nama_Barang").ToString
            '                                zDR("Satuan") = .Item("satuan").ToString
            '                                zDR("NamaPaket") = .Item("namapaket").ToString
            '                                zDR("Harga_Satuan") = .Item("harga").ToString
            '                                zDR("JumlahSatuan") = .Item("volume").ToString
            '                                zDR("Quantity") = .Item("quantity").ToString
            '                                zDR("Total") = .Item("totalvolume").ToString
            '                                zDT.Rows.Add(zDR)

            '                            End With

            '                        Next

            '                    End If

            '                ElseIf NamaSatuan.ToString = "Unit" Or NamaSatuan.ToString = "unit" Then
            '                    sqlstring = "SELECT IVD.ID, IVD.No_Invoice as InvoiceHeaderID, WHD.Nama_Barang, MHD.NamaHarga as Satuan, WHD.Nama_Satuan as Namapaket, " & _
            '                                " QD.Harga, WHD.Quantity " & _
            '                                "FROM InvoiceDetail IVD " & _
            '                                "LEFT JOIN V_Warehouse_Satuan WHD ON ( IVD.MuatBarangDetailID = WHD.IDDetail and WHD.WarehouseItem_Code = IVD.Mb_No )" & _
            '                                "LEFT JOIN QuotationDetail QD ON (WHD.QuotationDetailID = QD.IDDetail and QD.Quotation_No = WHD.Quotation_No ) " & _
            '                                "LEFT JOIN MasterHargaDefault MHD ON QD.SatuanID = MHD.ID " & _
            '                                "WHERE IVD.No_Invoice = '" & grid.GetMasterRowFieldValues("No_Invoice") & "' " & _
            '                                "AND IVD.MuatBarangDetailID = '" & DT.Rows(i).Item("MuatBarangDetailID").ToString & "'" & _
            '                                "AND IVD.Mb_No = '" & DT.Rows(i).Item("Mb_No").ToString & "' " & _
            '                                "AND WHD.[status] = 1 " & _
            '                                "AND ( QD.[status] = 1 or QD.[status] = 2 ) " & _
            '                                "AND MHD.[status] = 1 " & _
            '                                "AND IVD.[status] = 1 " & _
            '                                "Order BY WHD.Nama_Barang"
            '                    DS = SQLExecuteQuery(sqlstring)
            '                    cDT = DS.Tables(0)

            '                    If cDT.Rows.Count > 0 Then
            '                        For j As Integer = 0 To cDT.Rows.Count - 1
            '                            With cDT.Rows(j)
            '                                zDR("MuatBarangDetailID") = .Item("ID").ToString
            '                                zDR("Nama_Barang") = .Item("Nama_Barang").ToString
            '                                zDR("Satuan") = .Item("Satuan").ToString
            '                                zDR("NamaPaket") = .Item("Namapaket").ToString
            '                                zDR("Harga_Satuan") = .Item("Harga").ToString
            '                                zDR("Quantity") = .Item("Quantity").ToString
            '                                zDR("Total") = .Item("Quantity").ToString
            '                                zDT.Rows.Add(zDR)
            '                            End With

            '                        Next

            '                    End If

            '                    'Grid_Invoice_Child.Columns("JumlahSatuan").Visible = False
            '                    'Grid_Invoice_Child.Columns("Total").Visible = False

            '                Else
            '                    sqlstring = "SELECT IVD.ID, IVD.No_Invoice as InvoiceHeaderID, WHD.Nama_Barang, MHD.NamaHarga as Satuan, WHD.Nama_Satuan as Namapaket, " & _
            '                                " QD.Harga, WHD.Quantity " & _
            '                                "FROM InvoiceDetail IVD " & _
            '                               "LEFT JOIN V_Warehouse_Satuan WHD ON ( IVD.MuatBarangDetailID = WHD.IDDetail and WHD.WarehouseItem_Code = IVD.Mb_No ) " & _
            '                                "LEFT JOIN QuotationDetail QD ON (WHD.QuotationDetailID = QD.IDDetail and WHD.Quotation_No = QD.Quotation_No) " & _
            '                                "LEFT JOIN MasterHargaDefault MHD ON QD.SatuanID = MHD.ID " & _
            '                                "WHERE IVD.No_Invoice = '" & grid.GetMasterRowFieldValues("No_Invoice") & "' " & _
            '                                "AND IVD.MuatBarangDetailID = '" & DT.Rows(i).Item("MuatBarangDetailID").ToString & "'  " & _
            '                                "AND IVD.[status] = 1 " & _
            '                                "AND WHD.[status] = 1 " & _
            '                                "AND ( QD.[status] = 1 or QD.[status] = 2 ) " & _
            '                                "AND MHD.[status] = 1 "
            '                    DS = SQLExecuteQuery(sqlstring)
            '                    cDT = DS.Tables(0)

            '                    If cDT.Rows.Count > 0 Then
            '                        For j As Integer = 0 To cDT.Rows.Count - 1
            '                            With cDT.Rows(j)
            '                                zDR("MuatBarangDetailID") = .Item("ID").ToString
            '                                zDR("Nama_Barang") = .Item("Nama_Barang").ToString
            '                                zDR("Satuan") = .Item("Satuan").ToString
            '                                zDR("NamaPaket") = .Item("Namapaket").ToString
            '                                zDR("Harga_Satuan") = .Item("Harga").ToString
            '                                zDR("Total") = .Item("Quantity").ToString
            '                                zDR("Quantity") = .Item("Quantity").ToString
            '                                zDT.Rows.Add(zDR)
            '                            End With

            '                        Next

            '                    End If

            '                    'Grid_Invoice_Child.Columns("JumlahSatuan").Visible = False
            '                    'Grid_Invoice_Child.Columns("Total").Visible = False

            '                End If
            '            End If
            '        End With

            '    Next

            'End If



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

    Private Sub Grid_Invoice_Parent_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Grid_Invoice_Parent.PreRender
        Try
            Grid_Invoice_Parent.FocusedRowIndex = -1
        Catch ex As Exception
            Throw New Exception("Error function grid_prerender :" & ex.ToString)
        End Try
    End Sub

    Private Sub Grid_Invoice_Parent_RowCommand(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewRowCommandEventArgs) Handles Grid_Invoice_Parent.RowCommand
        Try
            Dim pisah() As String

            Select Case e.CommandArgs.CommandName
                Case "Delete"
                    Delete(Grid_Invoice_Parent.GetRowValues(e.VisibleIndex, "No_Invoice").ToString)
                Case "Edit"
                    HFMode.Value = "Update"
                    HFmuatBarangID.Value = Grid_Invoice_Parent.GetRowValues(e.VisibleIndex, "MuatBarangID")
                    DtInvoice.Date = CDate(Grid_Invoice_Parent.GetRowValues(e.VisibleIndex, "TglInvoice").ToString).ToString("dd MMMM yyyy")
                    TxtNamaPengirim.Text = Grid_Invoice_Parent.GetRowValues(e.VisibleIndex, "Pengirim")
                    TxtTujuan.Text = Grid_Invoice_Parent.GetRowValues(e.VisibleIndex, "Tujuan")
                    TxtPenerima.Text = Grid_Invoice_Parent.GetRowValues(e.VisibleIndex, "Penerima")
                    TxtNoInvoice.Text = Grid_Invoice_Parent.GetRowValues(e.VisibleIndex, "No_Invoice").ToString
                    HFNoInvoice.Value = Grid_Invoice_Parent.GetRowValues(e.VisibleIndex, "No_Invoice").ToString
                    TxtKapal.Text = Grid_Invoice_Parent.GetRowValues(e.VisibleIndex, "NamaKapal").ToString
                    TxtPercen.Text = Grid_Invoice_Parent.GetRowValues(e.VisibleIndex, "Paid").ToString
                    TxtNamaBarang.Text = Grid_Invoice_Parent.GetRowValues(e.VisibleIndex, "NamaBarang").ToString
                    If Grid_Invoice_Parent.GetRowValues(e.VisibleIndex, "Keterangan").ToString.Contains("PembayaranMinimum") Then
                        pisah = Grid_Invoice_Parent.GetRowValues(e.VisibleIndex, "Keterangan").ToString.Split("-")

                        ChkMiniByr.Checked = True
                        txtMinByr.Text = pisah(1)
                    End If

                    TxtNoInvoice.Enabled = False
                    TxtIndikator.Enabled = False
                    load_DDLWhenEdit(Grid_Invoice_Parent.GetRowValues(e.VisibleIndex, "MuatBarangID").ToString)
                    ' Case "Close"
                    ' Close(Grid_Invoice_Parent.GetRowValues(e.VisibleIndex, "No_Invoice").ToString, Grid_Invoice_Parent.GetRowValues(e.VisibleIndex, "MuatBarangID").ToString, Grid_Invoice_Parent.GetRowValues(e.VisibleIndex, "Paid").ToString)
            End Select
        Catch ex As Exception
            Throw New Exception("Error function grid_invoice_parent_row_command :" & ex.ToString)
        End Try
    End Sub
#End Region

#Region "VALIDATION"
    Private Function validation_update() As Boolean

        Try
            clear_Label()
            If ddlDitunjukan.SelectedIndex = 0 Then
                clear_Label()
                lInfo.Visible = True
                lInfo.Text = "Pilih ditunjukan kepada"
                Return False
            End If
            If TxtPercen.Text.Trim = "" Then
                lInfo.Visible = True
                lInfo.Text = "Anda harus memasukkan persentase DP "
                Return False
            Else
                If Not IsNumeric(TxtPercen.Text.Replace(",", ".")) Then
                    lInfo.Visible = True
                    lInfo.Text = "Percentase harus angka "
                    Return False
                End If
            End If
            If TxtNamaBarang.Text.Trim = "" Then
                lInfo.Visible = True
                lInfo.Text = "Nama Barang Harus diisi"
                Return False
            End If

            Return True
        Catch ex As Exception
            Throw New Exception("Error validation update :" & ex.ToString)
        End Try


    End Function
    Private Function validation_add() As Boolean

        Try
            If HFIDKapal.Value = "" Then
                lInfo.Visible = True
                lInfo.Text = "Anda harus memilih kapal barang akan di muat "
                Return False
            End If
        Catch ex As Exception
            Throw New Exception("Error function validation_add :" & ex.ToString)
        End Try
        Return True
    End Function
    Private Function validation() As Boolean
        Dim str As String
        Dim Hsl As String = ""

        Try
            If TxtPercen.Text.Trim = "" Then
                lInfo.Visible = True
                lInfo.Text = "Anda harus memasukkan persentase DP "
                Return False
            Else
                If Not IsNumeric(TxtPercen.Text.Replace(",", ".")) Then
                    lInfo.Visible = True
                    lInfo.Text = "Percentase harus angka "
                    Return False
                End If
            End If

            If ddlDitunjukan.SelectedIndex = 0 Then
                clear_Label()
                lInfo.Visible = True
                lInfo.Text = "Pilih ditunjukan kepada"
                Return False
            End If

            If TxtIndikator.Text.Trim = "" Then
                lInfo.Visible = True
                lInfo.Text = "Indikator Harus diisi"
                Return False
            End If

            If TxtNamaBarang.Text.Trim = "" Then
                lInfo.Visible = True
                lInfo.Text = "Nama Barang Harus diisi"
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

            str = "select MuatBarangID from InvoiceHeader where " & _
                "MuatbarangID = '" & HFMBNO.Value & "' AND status <> 0 and DP = 'Yes' "
            Hsl = SQLExecuteScalar(str)

            If Hsl <> "" Then
                lInfo.Visible = True
                lInfo.Text = "DP sudah Pernah Dibuat "
                Return False
            End If

            Return True
        Catch ex As Exception
            Throw New Exception("Error function validation :" & ex.ToString)
        End Try
    End Function

#End Region

#Region "METHOD"
    Private Sub insert()
        'added to insert journal
        Dim RunNo As Int64 = GetRunNoHeader()
        Dim dt As DataTable
        Dim ds As DataSet
        Dim eDS As DataSet
        Dim eDT As DataTable
        Dim cekstring As String
        Dim total As Double
        Dim totalsatuan As Double
        Dim pisah() As String
        Dim iddetail As Integer
        Dim TOtalUkuran As String = ""
        Dim Keterangan As String = ""
        Dim bulan As String = DtInvoice.Date.ToString("MM")
        Dim tahun As String = DtInvoice.Date.ToString("yy")
        Try


            cekstring = "select A.IDQuotationDetail, A.mb_no, A.namaquo, Cast(SUM(A.volumetotal) as Decimal(30,3)) as VolumeTotal, SUM(A.TotalBerat) as BeratTotal, A.Harga, " & _
                        "A.NamaHarga As JenisPembayaran, SUM(A.TotalQty) as TotalQty, SUM(A.Total) as Total, A.Paid " & _
            "            FROM " & _
            "(select qd.IDDetail as IDQuotationDetail, mbd.Mb_No ,wd.Container,qd.Nama_Barang as namaquo, SUM(wd.Panjang * wd.Lebar * wd.Tinggi * mbd.Quantity) as volumetotal, " & _
            "SUM(wd.berat * mbd.Quantity) as TotalBerat, mh.NamaHarga,wd.Paid, qd.Harga, sum(mbd.quantity) as TotalQty, " & _
            "(SELECT CASE   " & _
            "When wd.Container='true' then " & _
            "	(  " & _
            "	    ((mbd.Quantity * qd.Harga)) " & _
            "	)  " & _
            "when wd.Container='kubikasi' then  " & _
            "   (  " & _
            "       ((cast(wd.Panjang * wd.Lebar*wd.Tinggi * mbd.Quantity as Decimal(30,3)) * qd.Harga)) " & _
            "   )  " & _
            "else " & _
            "	(  " & _
            "	(SELECT case " & _
            "		when (mh.NamaHarga = 'Kubik' or mh.NamaHarga = 'kubik') then  " & _
            "			(  " & _
            "				((cast(SUM(wd.Panjang * wd.Lebar * wd.Tinggi * mbd.Quantity) AS decimal(30,3)) * qd.Harga))  " & _
            "			)  " & _
            "		when mh.NamaHarga = 'Ton' or mh.NamaHarga = 'ton' or mh.NamaHarga = 'Berat' or mh.NamaHarga ='berat' then  " & _
            "			(  " & _
            "				((((SUM(mbd.Quantity) * wd.Berat) / 1000)  * qd.Harga))  " & _
            "			)  " & _
            "		when mh.NamaHarga = 'Unit' or mh.NamaHarga = 'unit' then  " & _
            "			(  " & _
            "				(cast(SUM(mbd.Quantity * qd.Harga) as Decimal(30,3)))  " & _
            "			)  " & _
            "		else( " & _
            "				((mbd.Quantity * qd.Harga))  " & _
            "			)  " & _
            "		end)  " & _
            "	)  " & _
            "end) as Total  " & _
            "from V_MuatBarang_Detail mbd, V_Warehouse_Satuan wd ,QuotationDetail qd ,MasterHargaDefault mh   " & _
            "        where (mbd.WarehouseHeaderID = wd.WarehouseItem_Code and mbd.Warehouse_Id  = wd.IDDetail) and " & _
            "mbd.Mb_No = '" & HFMBNO.Value & "' and  " & _
            "(wd.Quotation_No = qd.Quotation_No And wd.QuotationDetailID = qd.IDDetail)  " & _
            "and qd.SatuanID = mh.ID  " & _
            "and  wd.status = 1 and qd.status <>0 and mh.status = 1 " & _
            "GROUP BY mh.NamaHarga, mbd.Mb_No ,wd.Container, qd.Nama_Barang,qd.harga,wd.Paid, qd.Harga, " & _
            "wd.Panjang,wd.Lebar,wd.Tinggi,wd.Berat, mbd.Quantity, qd.IDDetail) as A " & _
            "GROUP BY A.NamaHarga, A.mb_no, A.namaquo, A.Harga, A.Paid, A.IDQuotationDetail "

            ds = SQLExecuteQuery(cekstring)
            dt = ds.Tables(0)
            total = 0
            iddetail = 0



            TxtNoInvoice.Text = Load_Invoice_No(TxtIndikator.Text.Trim.Replace("'", "''"))

            sqlstring = ""

            pisah = ddlDitunjukan.SelectedValue.ToString.Split("-")

            '<--- Insert to AR journal ---->'
            If GetCOAPiutangCust(pisah(0).ToString.Trim) = True Then
                If GetCOAPenghasilanKapal(HFIDKapal.Value) = True Then
                    eDS = GetConfigAR(pisah(0).ToString.Trim, HFIDKapal.Value)
                    eDT = eDS.Tables(0)
                    If eDT.Rows.Count > 0 Then
                        If Left(TxtNoInvoice.Text, 1) = "b" Or Left(TxtNoInvoice.Text, 1) = "B" Then
                            If dt.Rows.Count > 0 Then
                                For i As Integer = 0 To dt.Rows.Count - 1
                                    With dt.Rows(i)

                                        Select Case .Item("JenisPembayaran").ToString.ToUpper
                                            Case "TON"
                                                TOtalUkuran = (CDbl(System.Math.Round(.Item("BeratTotal").ToString / 1000, 3))).ToString
                                            Case "KUBIK"
                                                TOtalUkuran = .Item("VolumeTotal").ToString
                                            Case "UNIT"
                                                TOtalUkuran = .Item("TotalQty").ToString
                                            Case "CONTAINER"
                                                TOtalUkuran = .Item("TotalQty").ToString
                                            Case "SATUAN"
                                                TOtalUkuran = .Item("TotalQty").ToString
                                        End Select

                                        iddetail = iddetail + 1

                                        totalsatuan = CDbl(.Item("Total").ToString) * (CDbl(TxtPercen.Text.ToString.Replace(",", ".")) / 100)
                                        total = total + totalsatuan

                                        sqlstring &= "INSERT INTO InvoiceDetail(IDDetail, Mb_No,No_Invoice,QuotationDetailID, Hargatotal,Paid,DP, UserName, [status], TotalUkuran) VALUES " & _
                                               "('" & iddetail & "', '" & HFMBNO.Value & "','" & TxtNoInvoice.Text.ToString & "','" & .Item("IDQuotationDetail").ToString & "', '" & totalsatuan & "' " & _
                                               ",'" & TxtPercen.Text.ToString.Replace(",", ".") & "' , 'Yes', '" & Session("UserId") & "', 1, '" & TOtalUkuran & "'); "

                                    End With
                                Next

                                If ChkMiniByr.Checked = True Then
                                    total = (CDbl(txtMinByr.Text.Trim.ToString.Replace("'", "''")) * CDbl(dt.Rows(0).Item("Harga").ToString)) * (TxtPercen.Text.Replace(",", ".").Replace("'", "") / 100)
                                    Keterangan = "PembayaranMinimum-" & txtMinByr.Text.Trim.Replace("'", "")
                                End If




                                sqlstring &= "INSERT INTO InvoiceHeader(No_Invoice, MuatbarangID, KapalID, Total, InvoiceDate, Ditujukan, DaerahDitujukan, " & _
                                       "NoAsuransi, HargaAsuransi, Premi, Polis, TotalAsuransi,Paid,DP, UserName, [status], KeteranganOngkosAngkut, " & _
                                       " NamaBarang, StatusNumber, CodeCust, BlnInvoice, Keterangan) VALUES " & _
                                     "('" & TxtNoInvoice.Text.ToString & "', '" & HFMBNO.Value & "', " & HFIDKapal.Value & " " & _
                                     ", " & total & ", '" & DtInvoice.Text & "', '" & pisah(0).ToString.Trim & "', '" & pisah(1).ToString.Trim & "', " & _
                                     " '',  0 , 0, 0, 0,'" & TxtPercen.Text.Replace(",", ".") & "' , 'Yes', '" & Session("UserId") & "', 1, '', '" & TxtNamaBarang.Text.ToString.Trim.Replace("'", "''") & "', 1, " & _
                                     " '" & HFCodeCustomer.Value & "', '" & CekBulan(bulan) & "/" & tahun & "', '" & Keterangan & "' ) ; "


                                result = SQLExecuteNonQuery(sqlstring, False, True)
                                If result <> "" Then
                                    Session("Grid_Header_InvoiceDP") = Nothing
                                    Grid_Invoice_Parent.DataSource = Nothing
                                    linfoberhasil.Visible = True
                                    linfoberhasil.Text = "Insert Berhasil"
                                    load_grid_invoice_header()
                                End If
                            End If
                        Else
                            If dt.Rows.Count > 0 Then
                                For i As Integer = 0 To dt.Rows.Count - 1
                                    With dt.Rows(i)

                                        Select Case .Item("JenisPembayaran").ToString.ToUpper
                                            Case "TON"
                                                TOtalUkuran = (CDbl(System.Math.Round(.Item("BeratTotal").ToString / 1000, 3))).ToString
                                            Case "KUBIK"
                                                TOtalUkuran = .Item("VolumeTotal").ToString
                                            Case "UNIT"
                                                TOtalUkuran = .Item("TotalQty").ToString
                                            Case "CONTAINER"
                                                TOtalUkuran = .Item("TotalQty").ToString
                                            Case "SATUAN"
                                                TOtalUkuran = .Item("TotalQty").ToString
                                        End Select

                                        iddetail = iddetail + 1

                                        totalsatuan = CDbl(.Item("Total").ToString) * (CDbl(TxtPercen.Text.ToString.Replace(",", ".")) / 100)
                                        total = total + totalsatuan

                                        sqlstring &= "INSERT INTO InvoiceDetail(IDDetail, Mb_No,No_Invoice,QuotationDetailID, Hargatotal,Paid,DP, UserName, [status], TotalUkuran) VALUES " & _
                                               "('" & iddetail & "', '" & HFMBNO.Value & "','" & TxtNoInvoice.Text.ToString & "','" & .Item("IDQuotationDetail").ToString & "', '" & totalsatuan & "' " & _
                                               ",'" & TxtPercen.Text.ToString.Replace(",", ".") & "' , 'Yes', '" & Session("UserId") & "', 1, '" & TOtalUkuran & "'); "


                                    End With
                                Next

                                If ChkMiniByr.Checked = True Then
                                    total = (CDbl(txtMinByr.Text.Trim.ToString.Replace("'", "''")) * CDbl(dt.Rows(0).Item("Harga").ToString)) * (TxtPercen.Text.Replace(",", ".").Replace("'", "") / 100)
                                    Keterangan = "PembayaranMinimum-" & txtMinByr.Text.Trim.Replace("'", "")
                                End If

                                sqlstring &= "INSERT INTO InvoiceHeader(No_Invoice, MuatbarangID, KapalID, Total, InvoiceDate, Ditujukan, DaerahDitujukan, " & _
                                       "NoAsuransi, HargaAsuransi, Premi, Polis, TotalAsuransi,Paid,DP, UserName, [status], KeteranganOngkosAngkut, " & _
                                       "NamaBarang, StatusNumber, CodeCust, BlnInvoice, Keterangan) VALUES " & _
                                     "('" & TxtNoInvoice.Text.ToString & "', '" & HFMBNO.Value & "', " & HFIDKapal.Value & " " & _
                                     ", " & total & ", '" & DtInvoice.Text & "', '" & pisah(0).ToString.Trim & "', '" & pisah(1).ToString.Trim & "', " & _
                                     " '',  0 , 0, 0, 0,'" & TxtPercen.Text.Replace(",", ".") & "' , 'Yes', '" & Session("UserId") & "', 1, '', '" & TxtNamaBarang.Text.ToString.Trim.Replace("'", "''") & "', 1, " & _
                                     " '" & HFCodeCustomer.Value & "', '" & CekBulan(bulan) & "/" & tahun & "', '" & Keterangan & "' ) ; "

                                result = SQLExecuteNonQuery(sqlstring)
                                'INSERT JOURNAL
                                InsertARHeader(RunNo, TxtNoInvoice.Text.ToString, DtInvoice.Text, total, HFCodeCustomer.Value, "NULL", Session("UserId").ToString)
                                InsertARDetailDebit(RunNo, pisah(0).ToString.Trim, total, Session("UserId").ToString)
                                InsertARDetailKredit(RunNo, HFIDKapal.Value, total, Session("UserId").ToString)
                                'END INSERT JOURNAL
                                If result <> "" Then
                                    Session("Grid_Header_InvoiceDP") = Nothing
                                    Grid_Invoice_Parent.DataSource = Nothing
                                    linfoberhasil.Visible = True
                                    linfoberhasil.Text = "Insert Berhasil"
                                    load_grid_invoice_header()
                                End If
                            End If
                        End If

                    Else
                        'INSERT LINKED ACCOUNT DULU
                        InsertLinkedAccount(HFIDKapal.Value, pisah(0).ToString.Trim, Session("UserId"))
                        'END INSERT LINKED ACCOUNT
                        If Left(TxtNoInvoice.Text, 1) = "b" Or Left(TxtNoInvoice.Text, 1) = "B" Then
                            If dt.Rows.Count > 0 Then
                                For i As Integer = 0 To dt.Rows.Count - 1
                                    With dt.Rows(i)

                                        Select Case .Item("JenisPembayaran").ToString.ToUpper
                                            Case "TON"
                                                TOtalUkuran = (CDbl(System.Math.Round(.Item("BeratTotal").ToString / 1000, 3))).ToString
                                            Case "KUBIK"
                                                TOtalUkuran = .Item("VolumeTotal").ToString
                                            Case "UNIT"
                                                TOtalUkuran = .Item("TotalQty").ToString
                                            Case "CONTAINER"
                                                TOtalUkuran = .Item("TotalQty").ToString
                                            Case "SATUAN"
                                                TOtalUkuran = .Item("TotalQty").ToString
                                        End Select

                                        iddetail = iddetail + 1

                                        totalsatuan = CDbl(.Item("Total").ToString) * (CDbl(TxtPercen.Text.ToString.Replace(",", ".")) / 100)
                                        total = total + totalsatuan

                                        sqlstring &= "INSERT INTO InvoiceDetail(IDDetail, Mb_No,No_Invoice,QuotationDetailID, Hargatotal,Paid,DP, UserName, [status], TotalUkuran) VALUES " & _
                                               "('" & iddetail & "', '" & HFMBNO.Value & "','" & TxtNoInvoice.Text.ToString & "','" & .Item("IDQuotationDetail").ToString & "', '" & totalsatuan & "' " & _
                                               ",'" & TxtPercen.Text.ToString.Replace(",", ".") & "' , 'Yes', '" & Session("UserId") & "', 1, '" & TOtalUkuran & "'); "

                                    End With
                                Next

                                If ChkMiniByr.Checked = True Then
                                    total = (CDbl(txtMinByr.Text.Trim.ToString.Replace("'", "''")) * CDbl(dt.Rows(0).Item("Harga").ToString)) * (TxtPercen.Text.Replace(",", ".").Replace("'", "") / 100)
                                    Keterangan = "PembayaranMinimum-" & txtMinByr.Text.Trim.Replace("'", "")
                                End If




                                sqlstring &= "INSERT INTO InvoiceHeader(No_Invoice, MuatbarangID, KapalID, Total, InvoiceDate, Ditujukan, DaerahDitujukan, " & _
                                       "NoAsuransi, HargaAsuransi, Premi, Polis, TotalAsuransi,Paid,DP, UserName, [status], KeteranganOngkosAngkut, " & _
                                       " NamaBarang, StatusNumber, CodeCust, BlnInvoice, Keterangan) VALUES " & _
                                     "('" & TxtNoInvoice.Text.ToString & "', '" & HFMBNO.Value & "', " & HFIDKapal.Value & " " & _
                                     ", " & total & ", '" & DtInvoice.Text & "', '" & pisah(0).ToString.Trim & "', '" & pisah(1).ToString.Trim & "', " & _
                                     " '',  0 , 0, 0, 0,'" & TxtPercen.Text.Replace(",", ".") & "' , 'Yes', '" & Session("UserId") & "', 1, '', '" & TxtNamaBarang.Text.ToString.Trim.Replace("'", "''") & "', 1, " & _
                                     " '" & HFCodeCustomer.Value & "', '" & CekBulan(bulan) & "/" & tahun & "', '" & Keterangan & "' ) ; "


                                result = SQLExecuteNonQuery(sqlstring, False, True)
                                If result <> "" Then
                                    Session("Grid_Header_InvoiceDP") = Nothing
                                    Grid_Invoice_Parent.DataSource = Nothing
                                    linfoberhasil.Visible = True
                                    linfoberhasil.Text = "Insert Berhasil"
                                    load_grid_invoice_header()
                                End If
                            End If
                        Else
                            If dt.Rows.Count > 0 Then
                                For i As Integer = 0 To dt.Rows.Count - 1
                                    With dt.Rows(i)

                                        Select Case .Item("JenisPembayaran").ToString.ToUpper
                                            Case "TON"
                                                TOtalUkuran = (CDbl(System.Math.Round(.Item("BeratTotal").ToString / 1000, 3))).ToString
                                            Case "KUBIK"
                                                TOtalUkuran = .Item("VolumeTotal").ToString
                                            Case "UNIT"
                                                TOtalUkuran = .Item("TotalQty").ToString
                                            Case "CONTAINER"
                                                TOtalUkuran = .Item("TotalQty").ToString
                                            Case "SATUAN"
                                                TOtalUkuran = .Item("TotalQty").ToString
                                        End Select

                                        iddetail = iddetail + 1

                                        totalsatuan = CDbl(.Item("Total").ToString) * (CDbl(TxtPercen.Text.ToString.Replace(",", ".")) / 100)
                                        total = total + totalsatuan

                                        sqlstring &= "INSERT INTO InvoiceDetail(IDDetail, Mb_No,No_Invoice,QuotationDetailID, Hargatotal,Paid,DP, UserName, [status], TotalUkuran) VALUES " & _
                                               "('" & iddetail & "', '" & HFMBNO.Value & "','" & TxtNoInvoice.Text.ToString & "','" & .Item("IDQuotationDetail").ToString & "', '" & totalsatuan & "' " & _
                                               ",'" & TxtPercen.Text.ToString.Replace(",", ".") & "' , 'Yes', '" & Session("UserId") & "', 1, '" & TOtalUkuran & "'); "


                                    End With
                                Next

                                If ChkMiniByr.Checked = True Then
                                    total = (CDbl(txtMinByr.Text.Trim.ToString.Replace("'", "''")) * CDbl(dt.Rows(0).Item("Harga").ToString)) * (TxtPercen.Text.Replace(",", ".").Replace("'", "") / 100)
                                    Keterangan = "PembayaranMinimum-" & txtMinByr.Text.Trim.Replace("'", "")
                                End If

                                sqlstring &= "INSERT INTO InvoiceHeader(No_Invoice, MuatbarangID, KapalID, Total, InvoiceDate, Ditujukan, DaerahDitujukan, " & _
                                       "NoAsuransi, HargaAsuransi, Premi, Polis, TotalAsuransi,Paid,DP, UserName, [status], KeteranganOngkosAngkut, " & _
                                       "NamaBarang, StatusNumber, CodeCust, BlnInvoice, Keterangan) VALUES " & _
                                     "('" & TxtNoInvoice.Text.ToString & "', '" & HFMBNO.Value & "', " & HFIDKapal.Value & " " & _
                                     ", " & total & ", '" & DtInvoice.Text & "', '" & pisah(0).ToString.Trim & "', '" & pisah(1).ToString.Trim & "', " & _
                                     " '',  0 , 0, 0, 0,'" & TxtPercen.Text.Replace(",", ".") & "' , 'Yes', '" & Session("UserId") & "', 1, '', '" & TxtNamaBarang.Text.ToString.Trim.Replace("'", "''") & "', 1, " & _
                                     " '" & HFCodeCustomer.Value & "', '" & CekBulan(bulan) & "/" & tahun & "', '" & Keterangan & "' ) ; "

                                result = SQLExecuteNonQuery(sqlstring)
                                'INSERT JOURNAL
                                InsertARHeader(RunNo, TxtNoInvoice.Text.ToString, DtInvoice.Text, total, HFCodeCustomer.Value, "NULL", Session("UserId").ToString)
                                InsertARDetailDebit(RunNo, pisah(0).ToString.Trim, total, Session("UserId").ToString)
                                InsertARDetailKredit(RunNo, HFIDKapal.Value, total, Session("UserId").ToString)
                                'END INSERT JOURNAL
                                If result <> "" Then
                                    Session("Grid_Header_InvoiceDP") = Nothing
                                    Grid_Invoice_Parent.DataSource = Nothing
                                    linfoberhasil.Visible = True
                                    linfoberhasil.Text = "Insert Berhasil"
                                    load_grid_invoice_header()
                                End If
                            End If
                        End If

                    End If
                Else
                    'INSERT COA PENGHASILAN DAN LINKED ACCOUNT DULU
                    InsertCOAPenghasilanKapal(HFIDKapal.Value, Session("UserId"))
                    InsertLinkedAccount(HFIDKapal.Value, pisah(0).ToString.Trim, Session("UserId"))
                    'END INSERT LINKED ACCOUNT
                    If Left(TxtNoInvoice.Text, 1) = "b" Or Left(TxtNoInvoice.Text, 1) = "B" Then
                        If dt.Rows.Count > 0 Then
                            For i As Integer = 0 To dt.Rows.Count - 1
                                With dt.Rows(i)

                                    Select Case .Item("JenisPembayaran").ToString.ToUpper
                                        Case "TON"
                                            TOtalUkuran = (CDbl(System.Math.Round(.Item("BeratTotal").ToString / 1000, 3))).ToString
                                        Case "KUBIK"
                                            TOtalUkuran = .Item("VolumeTotal").ToString
                                        Case "UNIT"
                                            TOtalUkuran = .Item("TotalQty").ToString
                                        Case "CONTAINER"
                                            TOtalUkuran = .Item("TotalQty").ToString
                                        Case "SATUAN"
                                            TOtalUkuran = .Item("TotalQty").ToString
                                    End Select

                                    iddetail = iddetail + 1

                                    totalsatuan = CDbl(.Item("Total").ToString) * (CDbl(TxtPercen.Text.ToString.Replace(",", ".")) / 100)
                                    total = total + totalsatuan

                                    sqlstring &= "INSERT INTO InvoiceDetail(IDDetail, Mb_No,No_Invoice,QuotationDetailID, Hargatotal,Paid,DP, UserName, [status], TotalUkuran) VALUES " & _
                                           "('" & iddetail & "', '" & HFMBNO.Value & "','" & TxtNoInvoice.Text.ToString & "','" & .Item("IDQuotationDetail").ToString & "', '" & totalsatuan & "' " & _
                                           ",'" & TxtPercen.Text.ToString.Replace(",", ".") & "' , 'Yes', '" & Session("UserId") & "', 1, '" & TOtalUkuran & "'); "

                                End With
                            Next

                            If ChkMiniByr.Checked = True Then
                                total = (CDbl(txtMinByr.Text.Trim.ToString.Replace("'", "''")) * CDbl(dt.Rows(0).Item("Harga").ToString)) * (TxtPercen.Text.Replace(",", ".").Replace("'", "") / 100)
                                Keterangan = "PembayaranMinimum-" & txtMinByr.Text.Trim.Replace("'", "")
                            End If




                            sqlstring &= "INSERT INTO InvoiceHeader(No_Invoice, MuatbarangID, KapalID, Total, InvoiceDate, Ditujukan, DaerahDitujukan, " & _
                                   "NoAsuransi, HargaAsuransi, Premi, Polis, TotalAsuransi,Paid,DP, UserName, [status], KeteranganOngkosAngkut, " & _
                                   " NamaBarang, StatusNumber, CodeCust, BlnInvoice, Keterangan) VALUES " & _
                                 "('" & TxtNoInvoice.Text.ToString & "', '" & HFMBNO.Value & "', " & HFIDKapal.Value & " " & _
                                 ", " & total & ", '" & DtInvoice.Text & "', '" & pisah(0).ToString.Trim & "', '" & pisah(1).ToString.Trim & "', " & _
                                 " '',  0 , 0, 0, 0,'" & TxtPercen.Text.Replace(",", ".") & "' , 'Yes', '" & Session("UserId") & "', 1, '', '" & TxtNamaBarang.Text.ToString.Trim.Replace("'", "''") & "', 1, " & _
                                 " '" & HFCodeCustomer.Value & "', '" & CekBulan(bulan) & "/" & tahun & "', '" & Keterangan & "' ) ; "


                            result = SQLExecuteNonQuery(sqlstring, False, True)
                            If result <> "" Then
                                Session("Grid_Header_InvoiceDP") = Nothing
                                Grid_Invoice_Parent.DataSource = Nothing
                                linfoberhasil.Visible = True
                                linfoberhasil.Text = "Insert Berhasil"
                                load_grid_invoice_header()
                            End If
                        End If
                    Else
                        If dt.Rows.Count > 0 Then
                            For i As Integer = 0 To dt.Rows.Count - 1
                                With dt.Rows(i)

                                    Select Case .Item("JenisPembayaran").ToString.ToUpper
                                        Case "TON"
                                            TOtalUkuran = (CDbl(System.Math.Round(.Item("BeratTotal").ToString / 1000, 3))).ToString
                                        Case "KUBIK"
                                            TOtalUkuran = .Item("VolumeTotal").ToString
                                        Case "UNIT"
                                            TOtalUkuran = .Item("TotalQty").ToString
                                        Case "CONTAINER"
                                            TOtalUkuran = .Item("TotalQty").ToString
                                        Case "SATUAN"
                                            TOtalUkuran = .Item("TotalQty").ToString
                                    End Select

                                    iddetail = iddetail + 1

                                    totalsatuan = CDbl(.Item("Total").ToString) * (CDbl(TxtPercen.Text.ToString.Replace(",", ".")) / 100)
                                    total = total + totalsatuan

                                    sqlstring &= "INSERT INTO InvoiceDetail(IDDetail, Mb_No,No_Invoice,QuotationDetailID, Hargatotal,Paid,DP, UserName, [status], TotalUkuran) VALUES " & _
                                           "('" & iddetail & "', '" & HFMBNO.Value & "','" & TxtNoInvoice.Text.ToString & "','" & .Item("IDQuotationDetail").ToString & "', '" & totalsatuan & "' " & _
                                           ",'" & TxtPercen.Text.ToString.Replace(",", ".") & "' , 'Yes', '" & Session("UserId") & "', 1, '" & TOtalUkuran & "'); "


                                End With
                            Next

                            If ChkMiniByr.Checked = True Then
                                total = (CDbl(txtMinByr.Text.Trim.ToString.Replace("'", "''")) * CDbl(dt.Rows(0).Item("Harga").ToString)) * (TxtPercen.Text.Replace(",", ".").Replace("'", "") / 100)
                                Keterangan = "PembayaranMinimum-" & txtMinByr.Text.Trim.Replace("'", "")
                            End If

                            sqlstring &= "INSERT INTO InvoiceHeader(No_Invoice, MuatbarangID, KapalID, Total, InvoiceDate, Ditujukan, DaerahDitujukan, " & _
                                   "NoAsuransi, HargaAsuransi, Premi, Polis, TotalAsuransi,Paid,DP, UserName, [status], KeteranganOngkosAngkut, " & _
                                   "NamaBarang, StatusNumber, CodeCust, BlnInvoice, Keterangan) VALUES " & _
                                 "('" & TxtNoInvoice.Text.ToString & "', '" & HFMBNO.Value & "', " & HFIDKapal.Value & " " & _
                                 ", " & total & ", '" & DtInvoice.Text & "', '" & pisah(0).ToString.Trim & "', '" & pisah(1).ToString.Trim & "', " & _
                                 " '',  0 , 0, 0, 0,'" & TxtPercen.Text.Replace(",", ".") & "' , 'Yes', '" & Session("UserId") & "', 1, '', '" & TxtNamaBarang.Text.ToString.Trim.Replace("'", "''") & "', 1, " & _
                                 " '" & HFCodeCustomer.Value & "', '" & CekBulan(bulan) & "/" & tahun & "', '" & Keterangan & "' ) ; "

                            result = SQLExecuteNonQuery(sqlstring)
                            'INSERT JOURNAL
                            InsertARHeader(RunNo, TxtNoInvoice.Text.ToString, DtInvoice.Text, total, HFCodeCustomer.Value, "NULL", Session("UserId").ToString)
                            InsertARDetailDebit(RunNo, pisah(0).ToString.Trim, total, Session("UserId").ToString)
                            InsertARDetailKredit(RunNo, HFIDKapal.Value, total, Session("UserId").ToString)
                            'END INSERT JOURNAL
                            If result <> "" Then
                                Session("Grid_Header_InvoiceDP") = Nothing
                                Grid_Invoice_Parent.DataSource = Nothing
                                linfoberhasil.Visible = True
                                linfoberhasil.Text = "Insert Berhasil"
                                load_grid_invoice_header()
                            End If
                        End If
                    End If

                End If
            Else
                If GetCOAPenghasilanKapal(HFIDKapal.Value) = True Then
                    'INSERT COA PIUTANG DAN LINKED ACCOUNT DULU
                    InsertCOAPiutang(pisah(0).ToString.Trim, Session("userId"))
                    InsertLinkedAccount(HFIDKapal.Value, pisah(0).ToString.Trim, Session("UserId"))
                    'END INSERT LINKED ACCOUNT
                    If Left(TxtNoInvoice.Text, 1) = "b" Or Left(TxtNoInvoice.Text, 1) = "B" Then
                        If dt.Rows.Count > 0 Then
                            For i As Integer = 0 To dt.Rows.Count - 1
                                With dt.Rows(i)

                                    Select Case .Item("JenisPembayaran").ToString.ToUpper
                                        Case "TON"
                                            TOtalUkuran = (CDbl(System.Math.Round(.Item("BeratTotal").ToString / 1000, 3))).ToString
                                        Case "KUBIK"
                                            TOtalUkuran = .Item("VolumeTotal").ToString
                                        Case "UNIT"
                                            TOtalUkuran = .Item("TotalQty").ToString
                                        Case "CONTAINER"
                                            TOtalUkuran = .Item("TotalQty").ToString
                                        Case "SATUAN"
                                            TOtalUkuran = .Item("TotalQty").ToString
                                    End Select

                                    iddetail = iddetail + 1

                                    totalsatuan = CDbl(.Item("Total").ToString) * (CDbl(TxtPercen.Text.ToString.Replace(",", ".")) / 100)
                                    total = total + totalsatuan

                                    sqlstring &= "INSERT INTO InvoiceDetail(IDDetail, Mb_No,No_Invoice,QuotationDetailID, Hargatotal,Paid,DP, UserName, [status], TotalUkuran) VALUES " & _
                                           "('" & iddetail & "', '" & HFMBNO.Value & "','" & TxtNoInvoice.Text.ToString & "','" & .Item("IDQuotationDetail").ToString & "', '" & totalsatuan & "' " & _
                                           ",'" & TxtPercen.Text.ToString.Replace(",", ".") & "' , 'Yes', '" & Session("UserId") & "', 1, '" & TOtalUkuran & "'); "

                                End With
                            Next

                            If ChkMiniByr.Checked = True Then
                                total = (CDbl(txtMinByr.Text.Trim.ToString.Replace("'", "''")) * CDbl(dt.Rows(0).Item("Harga").ToString)) * (TxtPercen.Text.Replace(",", ".").Replace("'", "") / 100)
                                Keterangan = "PembayaranMinimum-" & txtMinByr.Text.Trim.Replace("'", "")
                            End If




                            sqlstring &= "INSERT INTO InvoiceHeader(No_Invoice, MuatbarangID, KapalID, Total, InvoiceDate, Ditujukan, DaerahDitujukan, " & _
                                   "NoAsuransi, HargaAsuransi, Premi, Polis, TotalAsuransi,Paid,DP, UserName, [status], KeteranganOngkosAngkut, " & _
                                   " NamaBarang, StatusNumber, CodeCust, BlnInvoice, Keterangan) VALUES " & _
                                 "('" & TxtNoInvoice.Text.ToString & "', '" & HFMBNO.Value & "', " & HFIDKapal.Value & " " & _
                                 ", " & total & ", '" & DtInvoice.Text & "', '" & pisah(0).ToString.Trim & "', '" & pisah(1).ToString.Trim & "', " & _
                                 " '',  0 , 0, 0, 0,'" & TxtPercen.Text.Replace(",", ".") & "' , 'Yes', '" & Session("UserId") & "', 1, '', '" & TxtNamaBarang.Text.ToString.Trim.Replace("'", "''") & "', 1, " & _
                                 " '" & HFCodeCustomer.Value & "', '" & CekBulan(bulan) & "/" & tahun & "', '" & Keterangan & "' ) ; "


                            result = SQLExecuteNonQuery(sqlstring, False, True)
                            If result <> "" Then
                                Session("Grid_Header_InvoiceDP") = Nothing
                                Grid_Invoice_Parent.DataSource = Nothing
                                linfoberhasil.Visible = True
                                linfoberhasil.Text = "Insert Berhasil"
                                load_grid_invoice_header()
                            End If
                        End If
                    Else
                        If dt.Rows.Count > 0 Then
                            For i As Integer = 0 To dt.Rows.Count - 1
                                With dt.Rows(i)

                                    Select Case .Item("JenisPembayaran").ToString.ToUpper
                                        Case "TON"
                                            TOtalUkuran = (CDbl(System.Math.Round(.Item("BeratTotal").ToString / 1000, 3))).ToString
                                        Case "KUBIK"
                                            TOtalUkuran = .Item("VolumeTotal").ToString
                                        Case "UNIT"
                                            TOtalUkuran = .Item("TotalQty").ToString
                                        Case "CONTAINER"
                                            TOtalUkuran = .Item("TotalQty").ToString
                                        Case "SATUAN"
                                            TOtalUkuran = .Item("TotalQty").ToString
                                    End Select

                                    iddetail = iddetail + 1

                                    totalsatuan = CDbl(.Item("Total").ToString) * (CDbl(TxtPercen.Text.ToString.Replace(",", ".")) / 100)
                                    total = total + totalsatuan

                                    sqlstring &= "INSERT INTO InvoiceDetail(IDDetail, Mb_No,No_Invoice,QuotationDetailID, Hargatotal,Paid,DP, UserName, [status], TotalUkuran) VALUES " & _
                                           "('" & iddetail & "', '" & HFMBNO.Value & "','" & TxtNoInvoice.Text.ToString & "','" & .Item("IDQuotationDetail").ToString & "', '" & totalsatuan & "' " & _
                                           ",'" & TxtPercen.Text.ToString.Replace(",", ".") & "' , 'Yes', '" & Session("UserId") & "', 1, '" & TOtalUkuran & "'); "


                                End With
                            Next

                            If ChkMiniByr.Checked = True Then
                                total = (CDbl(txtMinByr.Text.Trim.ToString.Replace("'", "''")) * CDbl(dt.Rows(0).Item("Harga").ToString)) * (TxtPercen.Text.Replace(",", ".").Replace("'", "") / 100)
                                Keterangan = "PembayaranMinimum-" & txtMinByr.Text.Trim.Replace("'", "")
                            End If

                            sqlstring &= "INSERT INTO InvoiceHeader(No_Invoice, MuatbarangID, KapalID, Total, InvoiceDate, Ditujukan, DaerahDitujukan, " & _
                                   "NoAsuransi, HargaAsuransi, Premi, Polis, TotalAsuransi,Paid,DP, UserName, [status], KeteranganOngkosAngkut, " & _
                                   "NamaBarang, StatusNumber, CodeCust, BlnInvoice, Keterangan) VALUES " & _
                                 "('" & TxtNoInvoice.Text.ToString & "', '" & HFMBNO.Value & "', " & HFIDKapal.Value & " " & _
                                 ", " & total & ", '" & DtInvoice.Text & "', '" & pisah(0).ToString.Trim & "', '" & pisah(1).ToString.Trim & "', " & _
                                 " '',  0 , 0, 0, 0,'" & TxtPercen.Text.Replace(",", ".") & "' , 'Yes', '" & Session("UserId") & "', 1, '', '" & TxtNamaBarang.Text.ToString.Trim.Replace("'", "''") & "', 1, " & _
                                 " '" & HFCodeCustomer.Value & "', '" & CekBulan(bulan) & "/" & tahun & "', '" & Keterangan & "' ) ; "

                            result = SQLExecuteNonQuery(sqlstring)
                            'INSERT JOURNAL
                            InsertARHeader(RunNo, TxtNoInvoice.Text.ToString, DtInvoice.Text, total, HFCodeCustomer.Value, "NULL", Session("UserId").ToString)
                            InsertARDetailDebit(RunNo, pisah(0).ToString.Trim, total, Session("UserId").ToString)
                            InsertARDetailKredit(RunNo, HFIDKapal.Value, total, Session("UserId").ToString)
                            'END INSERT JOURNAL
                            If result <> "" Then
                                Session("Grid_Header_InvoiceDP") = Nothing
                                Grid_Invoice_Parent.DataSource = Nothing
                                linfoberhasil.Visible = True
                                linfoberhasil.Text = "Insert Berhasil"
                                load_grid_invoice_header()
                            End If
                        End If
                    End If

                Else
                    'INSERT COA PENGHASILAN DAN LINKED ACCOUNT DULU
                    InsertCOAPiutang(pisah(0).ToString.Trim, Session("userId"))
                    InsertCOAPenghasilanKapal(HFIDKapal.ToString, Session("userId"))
                    InsertLinkedAccount(HFIDKapal.Value, pisah(0).ToString.Trim, Session("UserId"))
                    'END INSERT LINKED ACCOUNT
                    If Left(TxtNoInvoice.Text, 1) = "b" Or Left(TxtNoInvoice.Text, 1) = "B" Then
                        If dt.Rows.Count > 0 Then
                            For i As Integer = 0 To dt.Rows.Count - 1
                                With dt.Rows(i)

                                    Select Case .Item("JenisPembayaran").ToString.ToUpper
                                        Case "TON"
                                            TOtalUkuran = (CDbl(System.Math.Round(.Item("BeratTotal").ToString / 1000, 3))).ToString
                                        Case "KUBIK"
                                            TOtalUkuran = .Item("VolumeTotal").ToString
                                        Case "UNIT"
                                            TOtalUkuran = .Item("TotalQty").ToString
                                        Case "CONTAINER"
                                            TOtalUkuran = .Item("TotalQty").ToString
                                        Case "SATUAN"
                                            TOtalUkuran = .Item("TotalQty").ToString
                                    End Select

                                    iddetail = iddetail + 1

                                    totalsatuan = CDbl(.Item("Total").ToString) * (CDbl(TxtPercen.Text.ToString.Replace(",", ".")) / 100)
                                    total = total + totalsatuan

                                    sqlstring &= "INSERT INTO InvoiceDetail(IDDetail, Mb_No,No_Invoice,QuotationDetailID, Hargatotal,Paid,DP, UserName, [status], TotalUkuran) VALUES " & _
                                           "('" & iddetail & "', '" & HFMBNO.Value & "','" & TxtNoInvoice.Text.ToString & "','" & .Item("IDQuotationDetail").ToString & "', '" & totalsatuan & "' " & _
                                           ",'" & TxtPercen.Text.ToString.Replace(",", ".") & "' , 'Yes', '" & Session("UserId") & "', 1, '" & TOtalUkuran & "'); "

                                End With
                            Next

                            If ChkMiniByr.Checked = True Then
                                total = (CDbl(txtMinByr.Text.Trim.ToString.Replace("'", "''")) * CDbl(dt.Rows(0).Item("Harga").ToString)) * (TxtPercen.Text.Replace(",", ".").Replace("'", "") / 100)
                                Keterangan = "PembayaranMinimum-" & txtMinByr.Text.Trim.Replace("'", "")
                            End If




                            sqlstring &= "INSERT INTO InvoiceHeader(No_Invoice, MuatbarangID, KapalID, Total, InvoiceDate, Ditujukan, DaerahDitujukan, " & _
                                   "NoAsuransi, HargaAsuransi, Premi, Polis, TotalAsuransi,Paid,DP, UserName, [status], KeteranganOngkosAngkut, " & _
                                   " NamaBarang, StatusNumber, CodeCust, BlnInvoice, Keterangan) VALUES " & _
                                 "('" & TxtNoInvoice.Text.ToString & "', '" & HFMBNO.Value & "', " & HFIDKapal.Value & " " & _
                                 ", " & total & ", '" & DtInvoice.Text & "', '" & pisah(0).ToString.Trim & "', '" & pisah(1).ToString.Trim & "', " & _
                                 " '',  0 , 0, 0, 0,'" & TxtPercen.Text.Replace(",", ".") & "' , 'Yes', '" & Session("UserId") & "', 1, '', '" & TxtNamaBarang.Text.ToString.Trim.Replace("'", "''") & "', 1, " & _
                                 " '" & HFCodeCustomer.Value & "', '" & CekBulan(bulan) & "/" & tahun & "', '" & Keterangan & "' ) ; "


                            result = SQLExecuteNonQuery(sqlstring, False, True)
                            If result <> "" Then
                                Session("Grid_Header_InvoiceDP") = Nothing
                                Grid_Invoice_Parent.DataSource = Nothing
                                linfoberhasil.Visible = True
                                linfoberhasil.Text = "Insert Berhasil"
                                load_grid_invoice_header()
                            End If
                        End If
                    Else
                        If dt.Rows.Count > 0 Then
                            For i As Integer = 0 To dt.Rows.Count - 1
                                With dt.Rows(i)

                                    Select Case .Item("JenisPembayaran").ToString.ToUpper
                                        Case "TON"
                                            TOtalUkuran = (CDbl(System.Math.Round(.Item("BeratTotal").ToString / 1000, 3))).ToString
                                        Case "KUBIK"
                                            TOtalUkuran = .Item("VolumeTotal").ToString
                                        Case "UNIT"
                                            TOtalUkuran = .Item("TotalQty").ToString
                                        Case "CONTAINER"
                                            TOtalUkuran = .Item("TotalQty").ToString
                                        Case "SATUAN"
                                            TOtalUkuran = .Item("TotalQty").ToString
                                    End Select

                                    iddetail = iddetail + 1

                                    totalsatuan = CDbl(.Item("Total").ToString) * (CDbl(TxtPercen.Text.ToString.Replace(",", ".")) / 100)
                                    total = total + totalsatuan

                                    sqlstring &= "INSERT INTO InvoiceDetail(IDDetail, Mb_No,No_Invoice,QuotationDetailID, Hargatotal,Paid,DP, UserName, [status], TotalUkuran) VALUES " & _
                                           "('" & iddetail & "', '" & HFMBNO.Value & "','" & TxtNoInvoice.Text.ToString & "','" & .Item("IDQuotationDetail").ToString & "', '" & totalsatuan & "' " & _
                                           ",'" & TxtPercen.Text.ToString.Replace(",", ".") & "' , 'Yes', '" & Session("UserId") & "', 1, '" & TOtalUkuran & "'); "


                                End With
                            Next

                            If ChkMiniByr.Checked = True Then
                                total = (CDbl(txtMinByr.Text.Trim.ToString.Replace("'", "''")) * CDbl(dt.Rows(0).Item("Harga").ToString)) * (TxtPercen.Text.Replace(",", ".").Replace("'", "") / 100)
                                Keterangan = "PembayaranMinimum-" & txtMinByr.Text.Trim.Replace("'", "")
                            End If

                            sqlstring &= "INSERT INTO InvoiceHeader(No_Invoice, MuatbarangID, KapalID, Total, InvoiceDate, Ditujukan, DaerahDitujukan, " & _
                                   "NoAsuransi, HargaAsuransi, Premi, Polis, TotalAsuransi,Paid,DP, UserName, [status], KeteranganOngkosAngkut, " & _
                                   "NamaBarang, StatusNumber, CodeCust, BlnInvoice, Keterangan) VALUES " & _
                                 "('" & TxtNoInvoice.Text.ToString & "', '" & HFMBNO.Value & "', " & HFIDKapal.Value & " " & _
                                 ", " & total & ", '" & DtInvoice.Text & "', '" & pisah(0).ToString.Trim & "', '" & pisah(1).ToString.Trim & "', " & _
                                 " '',  0 , 0, 0, 0,'" & TxtPercen.Text.Replace(",", ".") & "' , 'Yes', '" & Session("UserId") & "', 1, '', '" & TxtNamaBarang.Text.ToString.Trim.Replace("'", "''") & "', 1, " & _
                                 " '" & HFCodeCustomer.Value & "', '" & CekBulan(bulan) & "/" & tahun & "', '" & Keterangan & "' ) ; "

                            result = SQLExecuteNonQuery(sqlstring)
                            'INSERT JOURNAL
                            InsertARHeader(RunNo, TxtNoInvoice.Text.ToString, DtInvoice.Text, total, HFCodeCustomer.Value, "NULL", Session("UserId").ToString)
                            InsertARDetailDebit(RunNo, pisah(0).ToString.Trim, total, Session("UserId").ToString)
                            InsertARDetailKredit(RunNo, HFIDKapal.Value, total, Session("UserId").ToString)
                            'END INSERT JOURNAL
                            If result <> "" Then
                                Session("Grid_Header_InvoiceDP") = Nothing
                                Grid_Invoice_Parent.DataSource = Nothing
                                linfoberhasil.Visible = True
                                linfoberhasil.Text = "Insert Berhasil"
                                load_grid_invoice_header()
                            End If
                        End If
                    End If

                End If
            End If
            '<--- END Insert to AR journal ---->'

        Catch ex As Exception
            Throw New Exception("Error function Insert : " & ex.ToString)
        End Try
    End Sub


    Private Sub Update(ByVal no_invoice As String)
        Dim pisah() As String
        pisah = ddlDitunjukan.SelectedValue.ToString.Split("-")
        Dim Keterangan As String = ""
        Dim total As Double = 0
        Dim totalsatuan As Double = 0



        Try

            sqlstring = "select A.IDQuotationDetail, A.mb_no, A.namaquo, Cast(SUM(A.volumetotal) as Decimal(30,3)) as VolumeTotal, SUM(A.TotalBerat) as BeratTotal, A.Harga, " & _
                        "A.NamaHarga As JenisPembayaran, SUM(A.TotalQty) as TotalQty, SUM(A.Total) as Total, A.Paid " & _
            "            FROM " & _
            "(select qd.IDDetail as IDQuotationDetail, mbd.Mb_No ,wd.Container,qd.Nama_Barang as namaquo, SUM(wd.Panjang * wd.Lebar * wd.Tinggi * mbd.Quantity) as volumetotal, " & _
            "SUM(wd.berat * mbd.Quantity) as TotalBerat, mh.NamaHarga,wd.Paid, qd.Harga, sum(mbd.quantity) as TotalQty, " & _
            "(SELECT CASE   " & _
            "When wd.Container='true' then " & _
            "	(  " & _
            "	    ((mbd.Quantity * qd.Harga)) " & _
            "	)  " & _
            "when wd.Container='kubikasi' then  " & _
            "   (  " & _
            "       ((cast(wd.Panjang * wd.Lebar*wd.Tinggi * mbd.Quantity as Decimal(30,3)) * qd.Harga)) " & _
            "   )  " & _
            "else " & _
            "	(  " & _
            "	(SELECT case " & _
            "		when (mh.NamaHarga = 'Kubik' or mh.NamaHarga = 'kubik') then  " & _
            "			(  " & _
            "				((cast(SUM(wd.Panjang * wd.Lebar * wd.Tinggi * mbd.Quantity) AS decimal(30,3)) * qd.Harga))  " & _
            "			)  " & _
            "		when mh.NamaHarga = 'Ton' or mh.NamaHarga = 'ton' or mh.NamaHarga = 'Berat' or mh.NamaHarga ='berat' then  " & _
            "			(  " & _
            "				((((SUM(mbd.Quantity) * wd.Berat) / 1000)  * qd.Harga))  " & _
            "			)  " & _
            "		when mh.NamaHarga = 'Unit' or mh.NamaHarga = 'unit' then  " & _
            "			(  " & _
            "				(cast(SUM(mbd.Quantity * qd.Harga) as Decimal(30,3)))  " & _
            "			)  " & _
            "		else( " & _
            "				((mbd.Quantity * qd.Harga))  " & _
            "			)  " & _
            "		end)  " & _
            "	)  " & _
            "end) as Total  " & _
            "from V_MuatBarang_Detail mbd, V_Warehouse_Satuan wd ,QuotationDetail qd ,MasterHargaDefault mh   " & _
            "        where (mbd.WarehouseHeaderID = wd.WarehouseItem_Code and mbd.Warehouse_Id  = wd.IDDetail) and " & _
            "mbd.Mb_No = '" & HFmuatBarangID.Value & "' and  " & _
            "(wd.Quotation_No = qd.Quotation_No And wd.QuotationDetailID = qd.IDDetail)  " & _
            "and qd.SatuanID = mh.ID  " & _
            "and  wd.status = 1 and qd.status <>0 and mh.status = 1 " & _
            "GROUP BY mh.NamaHarga, mbd.Mb_No ,wd.Container, qd.Nama_Barang,qd.harga,wd.Paid, qd.Harga, " & _
            "wd.Panjang,wd.Lebar,wd.Tinggi,wd.Berat, mbd.Quantity, qd.IDDetail) as A " & _
            "GROUP BY A.NamaHarga, A.mb_no, A.namaquo, A.Harga, A.Paid, A.IDQuotationDetail "

            DS = SQLExecuteQuery(sqlstring)
            DT = DS.Tables(0)

            sqlstring = ""

            For i As Integer = 0 To DT.Rows.Count - 1
                totalsatuan = CDbl(DT.Rows(i).Item("Total").ToString) * (CDbl(TxtPercen.Text.ToString.Replace(",", ".")) / 100)
                total = total + totalsatuan

                sqlstring &= "UPDATE InvoiceDetail " & _
                         "SET " & _
                        "Paid = '" & TxtPercen.Text.ToString.Replace(",", ".") & "' ,  " & _
                        "Hargatotal = '" & totalsatuan & "', " & _
                         "LastModified = '" & Now.ToString & "' " & _
                         "WHERE No_Invoice = '" & no_invoice.ToString & "' and status = 1; "
            Next

            If ChkMiniByr.Checked = True Then

                total = (CDbl(txtMinByr.Text.Trim.ToString.Replace("'", "''")) * CDbl(DT.Rows(0).Item("Harga").ToString)) * (TxtPercen.Text.Replace(",", ".").Replace("'", "") / 100)
                Keterangan = "PembayaranMinimum-" & txtMinByr.Text.Trim.Replace("'", "")

                sqlstring &= "UPDATE InvoiceHeader " & _
                       "SET " & _
                       " Ditujukan = '" & pisah(0).ToString.Trim & "' , " & _
                       " DaerahDitujukan = '" & pisah(1).ToString.Trim & "' ," & _
                       " NamaBarang = '" & TxtNamaBarang.Text.ToString.Trim.Replace("'", "''") & "', " & _
                       "Paid = '" & TxtPercen.Text.ToString.Replace(",", ".") & "' ,  " & _
                       "Total = '" & total & "', " & _
                       "Keterangan = '" & Keterangan & "', " & _
                       "InvoiceDate = '" & DtInvoice.Date & "', " & _
                       "LastModified = '" & Now.ToString & "' " & _
                       "WHERE No_Invoice = '" & no_invoice.ToString & "' and status = 1; "
            Else
                sqlstring &= "UPDATE InvoiceHeader " & _
                       "SET " & _
                       " Ditujukan = '" & pisah(0).ToString.Trim & "' , " & _
                       " DaerahDitujukan = '" & pisah(1).ToString.Trim & "' ," & _
                       " NamaBarang = '" & TxtNamaBarang.Text.ToString.Trim.Replace("'", "''") & "', " & _
                       "Paid = '" & TxtPercen.Text.ToString.Replace(",", ".") & "' ,  " & _
                       "Total = '" & total & "', " & _
                       "Keterangan = '" & Keterangan & "', " & _
                       "InvoiceDate = '" & DtInvoice.Date & "', " & _
                       "LastModified = '" & Now.ToString & "' " & _
                       "WHERE No_Invoice = '" & no_invoice.ToString & "' and status = 1; "
            End If

            
            

            result = SQLExecuteNonQuery(sqlstring)
            If result <> "" Then
                Session("Grid_Header_InvoiceDP") = Nothing
                Grid_Invoice_Parent.DataSource = Nothing
                linfoberhasil.Visible = True
                linfoberhasil.Text = "Update Berhasil"
                load_grid_invoice_header()
            End If
        Catch ex As Exception
            Throw New Exception("Error Function Update :" & ex.ToString)
        End Try

    End Sub

    Private Sub Close(ByVal no_invoice As String, ByVal warehouseitem_Code As String, ByVal paid As String)
        Try
            sqlstring = "UPDATE InvoiceHeader " & _
                        "SET " & _
                        "LastModified = '" & Now.ToString & "' " & _
                        ",[status] = 7 " & _
                        "WHERE No_Invoice = '" & no_invoice.ToString & "' and status =1 ; "
            sqlstring &= "UPDATE InvoiceDetail " & _
                         "SET " & _
                         "LastModified = '" & Now.ToString & "' " & _
                         ",[status] = 7 " & _
                         "WHERE No_Invoice = '" & no_invoice.ToString & "' and status =1 ; "

            sqlstring &= "Update WarehouseHeader set Paid = " & CDbl(paid) & " where Warehouseitem_Code = '" & warehouseitem_Code.ToString & "' and status = 1;"
            sqlstring &= "Update WarehouseDetail set Paid = " & CDbl(paid) & " where Warehouseitem_Code = '" & warehouseitem_Code.ToString & "' and status = 1"

            If Left(TxtNoInvoice.Text, 1) = "b" Or Left(TxtNoInvoice.Text, 1) = "B" Then
                result = SQLExecuteNonQuery(sqlstring, False, True)
            Else
                result = SQLExecuteNonQuery(sqlstring)
            End If
            
            If result > 0 Then
                linfoberhasil.Visible = True
                linfoberhasil.Text = "Close Berhasil"
                load_grid_invoice_header()
            End If
        Catch ex As Exception
            Throw New Exception("<b>Error function Delete</b>" & ex.ToString)
        End Try
    End Sub
    Private Sub Delete(ByVal no_invoice As String)
        Try
            sqlstring = "UPDATE InvoiceHeader " & _
                        "SET " & _
                        "LastModified = '" & Now.ToString & "' " & _
                        ",[status] = 0 " & _
                        "WHERE No_Invoice = '" & no_invoice.ToString & "' and status =1 ; "
            sqlstring &= "UPDATE InvoiceDetail " & _
                         "SET " & _
                         "LastModified = '" & Now.ToString & "' " & _
                         ",[status] = 0 " & _
                         "WHERE No_Invoice = '" & no_invoice.ToString & "' and status =1 ; "

            result = SQLExecuteNonQuery(sqlstring)
            If result <> "" Then
                Session("Grid_Header_InvoiceDP") = Nothing
                linfoberhasil.Visible = True
                linfoberhasil.Text = "Delete Berhasil"
                load_grid_invoice_header()
            End If

        Catch ex As Exception
            Throw New Exception("<b>Error function Delete</b>" & ex.ToString)
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
            TxtNamaPengirim.Text = ""
            TxtTujuan.Text = ""
            TxtNoInvoice.Text = ""
            TxtKapal.Text = ""
            TxtPercen.Text = ""
            TxtPenerima.Text = ""
            HFMode.Value = "Insert"
            HFModeItem.Value = "Insert"
            HFInvoiceHeaderID.Value = ""
            HFID.Value = ""
            HFmuatBarangID.Value = ""
            HFInvoiceDetailID.Value = ""
            HFNmBarang.Value = ""
            HFTujuan.Value = ""
            hfDel.Value = ""
            HFNamaPengirim.Value = ""
            HFIDKapal.Value = ""
            HFMBNO.Value = ""
            HFSatuan.Value = ""
            HFPembayaran.Value = ""
            HFPenerima.Value = ""
            HFNamakapal.Value = ""
            TxtNoInvoice.Enabled = True
            ddlDitunjukan.Items.Clear()

            TxtIndikator.Text = ""
            TxtIndikator.Enabled = True
            HFCodeCustomer.Value = ""
            txtMinByr.Text = ""
            TxtNamaBarang.Text = ""
            ChkMiniByr.Checked = False
        Catch ex As Exception
            Throw New Exception("<b>error function clear :</b>" & ex.ToString)
        End Try
    End Sub

#End Region

#Region "BUTTON"

    Protected Sub btAdd_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btAdd.Click

        If validation_add() Then
            clear_Label()
            load_DDL()
            'TxtNoInvoice.Text = Load_Invoice_No("0")
        End If
         End Sub

    Protected Sub btBatal_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btBatal.Click
        clear_all()
        clear_Label()
    End Sub
    Protected Sub btSave_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btSave.Click
        Try
            clear_Label()
            If HFMode.Value = "Insert" Then
                If validation() Then
                    Insert()
                    load_grid_invoice_header()
                    clear_all()
                    linfoberhasil.Text = "Simpan Berhasil"
                    linfoberhasil.Visible = True
                End If
            Else
                If validation_update() Then
                    Update(HFNoInvoice.Value)
                    clear_all()
                    linfoberhasil.Text = "Update berhasil"
                    linfoberhasil.Visible = True
                End If
               
            End If


        Catch ex As Exception
            Response.Write("<b>Error BtnSimpan</b>" & ex.ToString)
        End Try
    End Sub

#End Region

#Region "DDL"


    Private Sub load_DDL()
        Try
            sqlstring = "SELECT distinct (MC.Nama_Customer + ',' + MC.Jenis_Perusahaan + ' - ' + MC.Area) as dituju " & _
                        "FROM WarehouseHeader WHD " & _
                        "LEFT JOIN MasterQuotation MQ ON WHD.Quotation_No = MQ.Quotation_No " & _
                        "LEFT JOIN MasterCustomer MC ON MQ.Customer_Id = MC.Kode_Customer  " & _
                        "WHERE WHD.WarehouseItem_Code = '" & HFmuatBarangID.Value.ToString & "' " & _
                        "AND WHD.[status] = 1 " & _
                        "AND MC.[status] = 1 " & _
                        "AND (mq.[status] = 1 or mq.[status] = 2 ) " & _
                        "UNION " & _
                        "SELECT distinct (MQ.Penerima + ' - ' + MQ.Tujuan) as dituju " & _
                        "FROM WarehouseHeader WHD  " & _
                        "LEFT JOIN MasterQuotation MQ ON WHD.Quotation_No = MQ.Quotation_No " & _
                        "LEFT JOIN MasterCustomer MC ON MQ.Customer_Id = MC.Kode_Customer  " & _
                        "WHERE WHD.WarehouseItem_Code = '" & HFmuatBarangID.Value.ToString & "' " & _
                        "AND WHD.[status] = 1 " & _
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

    Private Sub load_DDLWhenEdit(ByVal id As String)
        Try
            sqlstring = "SELECT distinct (MC.Nama_Customer + ',' + MC.Jenis_Perusahaan + ' - ' + MC.Area) as dituju " & _
                        "FROM WarehouseHeader WHD " & _
                        "LEFT JOIN MasterQuotation MQ ON WHD.Quotation_No = MQ.Quotation_No " & _
                        "LEFT JOIN MasterCustomer MC ON MQ.Customer_Id = MC.Kode_Customer  " & _
                        "LEFT JOIN MuatBarang MB ON MB.WarehouseHeaderID = WHD.WarehouseItem_Code " & _
                        "WHERE MB.Mb_No  = '" & id.ToString & "' " & _
                        "AND WHD.[status] <> 0 " & _
                        "AND MC.[status] = 1 " & _
                        "AND (mq.[status] = 1 or mq.[status] = 2 ) " & _
                        "UNION " & _
                        "SELECT distinct (MQ.Penerima + ' - ' + MQ.Tujuan) as dituju " & _
                        "FROM WarehouseHeader WHD  " & _
                        "LEFT JOIN MasterQuotation MQ ON WHD.Quotation_No = MQ.Quotation_No " & _
                        "LEFT JOIN MuatBarang MB ON MB.WarehouseHeaderID = WHD.WarehouseItem_Code " & _
                        "WHERE MB.Mb_No  = '" & id.ToString & "' " & _
                        "AND WHD.[status] <> 0 " & _
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


#End Region

 
    Private Sub TxtNoInvoice_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtNoInvoice.TextChanged
        Try
            If HFMode.Value = "Insert" Then
                TxtNoInvoice.Text = Load_Invoice_No(Left(TxtNoInvoice.Text, 1))
                HFNoInvoice.Value = TxtNoInvoice.Text
            Else
                TxtNoInvoice.Text = HFNoInvoice.Value
            End If

        Catch ex As Exception
            Response.Write("<b>Error txtchange :</b>" & ex.ToString)
        End Try
    End Sub
End Class