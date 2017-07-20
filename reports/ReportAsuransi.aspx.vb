Imports System.Data
Imports DevExpress.Web.ASPxGridView
Imports System.Data.SqlClient
Partial Public Class ReportAsuransi1
    Inherits System.Web.UI.Page
    Private DT As DataTable
    Private DTD As DataTable
    Private DS As DataSet
    Private DSD As DataSet
    Private DR As DataRow
    Private sqlstring As String
    Dim iDT As New DataTable
    Dim result As String
    Dim hasil As Integer

#Region "PAGE"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Session("Grid_Asuransi") = Nothing
            Panel_Input.Visible = True
            Panel_Grid.Visible = True
            Panel_Report.Visible = False
            hfInvoiceType.Value = ""
            'ddltypeinvoice.Visible = False
            'lbljenis.Visible = False
            'lbltitikdua.Visible = False
            'load_invoice_parent("No")
            load_ddl()
        End If

        If Not Session("Grid_Asuransi") Is Nothing Then
            Grid_Asuransi_Parent.DataSource = CType(Session("Grid_Asuransi"), DataTable)
            Grid_Asuransi_Parent.DataBind()
        End If

    End Sub

#End Region

#Region "LOAD_GRID"
    Private Function create_table() As DataTable
        Dim kDT As New DataTable
        Try
            kDT.Columns.Add(New DataColumn("ID", GetType(String)))
        Catch ex As Exception
            Response.Write("Error Create Session <BR> : " & ex.ToString)
        End Try
        Return kDT
    End Function
    Private Sub load_invoice_parent(ByVal Ket As String)
        Dim cekdp As String = ""
        Dim cekhasil As String = ""
        Try
            iDT.Columns.Add(New DataColumn("ID", GetType(String)))
            iDT.Columns.Add(New DataColumn("MuatBarangID", GetType(String)))
            iDT.Columns.Add(New DataColumn("No_Invoice", GetType(String)))
            iDT.Columns.Add(New DataColumn("InvoiceDate", GetType(DateTime)))
            iDT.Columns.Add(New DataColumn("Nama_Customer", GetType(String)))
            iDT.Columns.Add(New DataColumn("Ditujukan", GetType(String)))
            iDT.Columns.Add(New DataColumn("KapalID", GetType(String)))
            iDT.Columns.Add(New DataColumn("Kapal", GetType(String)))
            iDT.Columns.Add(New DataColumn("Total", GetType(String)))
            iDT.Columns.Add(New DataColumn("DP", GetType(String)))
            iDT.Columns.Add(New DataColumn("NoAsuransi", GetType(String)))
            iDT.Columns.Add(New DataColumn("HargaAsuransi", GetType(String)))
            iDT.Columns.Add(New DataColumn("TotalAsuransi", GetType(String)))
            iDT.Columns.Add(New DataColumn("Premi", GetType(String)))
            iDT.Columns.Add(New DataColumn("Polis", GetType(String)))


            If Ket = "Freight" Then
                'If hfInvoiceType.Value = "Yes" Then
                '    sqlstring = " SELECT ih.No_Invoice as ID,ih.MuatBarangID,ih.DP,ih.No_Invoice,ih.InvoiceDate,ih.Total,mc.Nama_Customer,ih.Ditujukan, " & _
                '   " ih.KapalID,mk.Nama_Kapal,ih.HargaAsuransi,ih.Premi,ih.Polis,TotalAsuransi,NoAsuransi From InvoiceHeader ih left join WarehouseHeader wh on ih.MuatBarangID = wh.WarehouseItem_Code " & _
                '   " left join MasterQuotation mq on wh.Quotation_No =  mq.Quotation_No " & _
                '   " left join MasterCustomer mc on mq.Customer_Id = mc.Kode_Customer left join Kapal mk on mk.IDDetail = ih.KapalID  " & _
                '   " Where (ih.status = 1 or ih.status = 7) and mk.status = 1 and mc.status = 1 and mq.status <> 0 and wh.status <> 0 and ih.DP = '" & hfInvoiceType.Value & "' order by ih.ID "
                'ElseIf hfInvoiceType.Value = "No" Then
                sqlstring = " SELECT ih.No_Invoice as ID,ih.MuatBarangID,ih.DP,ih.No_Invoice,ih.InvoiceDate,ih.Total,mc.Nama_Customer,ih.Ditujukan, " & _
                               " ih.KapalID,mk.Nama_Kapal,ih.HargaAsuransi,ih.Premi,ih.Polis,TotalAsuransi,NoAsuransi From InvoiceHeader ih left join MuatBarang mb on ih.MuatBarangID = mb.Mb_No " & _
                               " left join MasterCustomer mc on mb.Customer_Id = mc.Kode_Customer left join Kapal mk on mb.Kapal = mk.IDDetail  " & _
                               " Where (ih.status = 1 or ih.status = 7) and mk.status = 1 and mc.status = 1 and mb.status <> 0 and ih.NoAsuransi <> '' order by ih.ID desc"
                'End If

                DS = SQLExecuteQuery(sqlstring)
                DT = DS.Tables(0)


                If DT.Rows.Count > 0 Then
                    For i As Integer = 0 To DT.Rows.Count - 1
                        With DT.Rows(i)
                            DR = iDT.NewRow()
                            DR("ID") = .Item("ID")
                            DR("MuatBarangID") = .Item("MuatBarangID")
                            DR("No_Invoice") = .Item("No_Invoice")
                            DR("InvoiceDate") = CDate(.Item("InvoiceDate").ToString).ToString("MM/dd/yyyy")
                            DR("Nama_Customer") = .Item("Nama_Customer")
                            DR("Ditujukan") = .Item("Ditujukan")
                            DR("KapalID") = .Item("KapalID")
                            DR("Kapal") = .Item("Nama_Kapal")
                            DR("Total") = .Item("Total")
                            DR("NoAsuransi") = .Item("NoAsuransi")
                            DR("HargaAsuransi") = .Item("HargaAsuransi")
                            DR("TotalAsuransi") = .Item("TotalAsuransi")
                            DR("Premi") = .Item("Premi")
                            DR("Polis") = .Item("Polis")
                            DR("DP") = .Item("DP")
                            iDT.Rows.Add(DR)
                        End With
                    Next
                    Session("Grid_Asuransi") = iDT
                    Grid_Asuransi_Parent.DataSource = iDT
                    Grid_Asuransi_Parent.KeyFieldName = "ID"
                    Grid_Asuransi_Parent.DataBind()

                    Grid_Asuransi_Parent.Columns("No_Invoice").Visible = True
                    Grid_Asuransi_Parent.Columns("Total").Visible = True
                    Grid_Asuransi_Parent.Columns("Ditujukan").Visible = True
                    Grid_Asuransi_Parent.Columns("Nama_Customer").Caption = "Pengirim"
                    Grid_Asuransi_Parent.Columns("InvoiceDate").Caption = "Tanggal"
                    Grid_Asuransi_Parent.Columns("Ditujukan").Caption = "Ditujukan"
                    Grid_Asuransi_Parent.Columns("DP").Caption = "DP"
                    Grid_Asuransi_Parent.Columns("Kapal").Caption = "Kapal"
                    Grid_Asuransi_Parent.Columns("Kapal").VisibleIndex = 11
                    Grid_Asuransi_Parent.Columns("NoAsuransi").VisibleIndex = 3
                Else
                    Grid_Asuransi_Parent.DataSource = Nothing
                    Grid_Asuransi_Parent.DataBind()
                End If

            Else

                sqlstring = "SELECT AL.ID, AL.Tanggal, MC.Nama_Customer, AL.KodeCustomer, AL.TglPeriodeAwal, AL.TglPeriodeAkhir, AL.NoAsuransi, AL.HargaAsuransi, " & _
                        "AL.Premi, AL.Polis,Discount, AL.TotalAsuransi, AL.Keterangan " & _
                        "FROM AsuransiLain AL " & _
                        "JOIN MasterCustomer MC ON AL.KodeCustomer = Kode_Customer " & _
                        "WHERE AL.[status] = 1 AND MC.[status] <> 0 " & _
                        "ORDER BY ID"
                DS = SQLExecuteQuery(sqlstring)
                DT = DS.Tables(0)


                If DT.Rows.Count > 0 Then
                    For i As Integer = 0 To DT.Rows.Count - 1
                        With DT.Rows(i)
                            DR = iDT.NewRow()
                            DR("ID") = .Item("ID")
                            DR("No_Invoice") = .Item("KodeCustomer")
                            DR("InvoiceDate") = CDate(.Item("Tanggal").ToString).ToString("dd MMMM yyyy")
                            DR("Nama_Customer") = .Item("Nama_Customer")
                            DR("Ditujukan") = CDate(.Item("TglPeriodeAwal")).ToString("dd MMMM yyyy") & " s/d " & CDate(.Item("TglPeriodeAkhir")).ToString("dd MMMM yyyy")
                            DR("NoAsuransi") = .Item("NoAsuransi")
                            DR("HargaAsuransi") = .Item("HargaAsuransi")
                            DR("TotalAsuransi") = .Item("TotalAsuransi")
                            DR("Premi") = .Item("Premi")
                            DR("Polis") = .Item("Polis")
                            DR("DP") = .Item("Discount")
                            DR("Kapal") = .Item("Keterangan")
                            iDT.Rows.Add(DR)
                        End With
                    Next
                    Session("Grid_Asuransi") = iDT
                    Grid_Asuransi_Parent.DataSource = iDT
                    Grid_Asuransi_Parent.KeyFieldName = "ID"
                    Grid_Asuransi_Parent.DataBind()

                    Grid_Asuransi_Parent.Columns("No_Invoice").Visible = False
                    Grid_Asuransi_Parent.Columns("Total").Visible = False
                    Grid_Asuransi_Parent.Columns("Nama_Customer").Caption = "Nama Customer"
                    Grid_Asuransi_Parent.Columns("Ditujukan").Caption = "Periode"
                    Grid_Asuransi_Parent.Columns("InvoiceDate").Caption = "Tanggal"
                    Grid_Asuransi_Parent.Columns("DP").Caption = "Discount"
                    Grid_Asuransi_Parent.Columns("Kapal").Caption = "Keterangan"
                    Grid_Asuransi_Parent.Columns("Kapal").VisibleIndex = 9
                    Grid_Asuransi_Parent.Columns("NoAsuransi").VisibleIndex = 3
                Else
                    Grid_Asuransi_Parent.DataSource = Nothing
                    Grid_Asuransi_Parent.DataBind()
                End If
            End If


        Catch ex As Exception
            Response.Write("load_kapal_parent : <BR> " & ex.ToString)
        End Try
    End Sub
    'Private Sub load_invoice_child(ByVal grid As ASPxGridView)
    '    Dim kDT As New DataTable
    '    Dim cDS As New DataSet
    '    Dim cDT As New DataTable
    '    Dim kSTR As String
    '    Dim lSTR As String
    '    Dim lDS As DataSet
    '    Dim lDT As DataTable
    '    Dim namabarang As String
    '    Try

    '        kDT.Columns.Add(New DataColumn("ID", GetType(String)))
    '        kDT.Columns.Add(New DataColumn("Nama_Barang", GetType(String)))
    '        kDT.Columns.Add(New DataColumn("Hargatotal", GetType(String)))

    '        lSTR = "select MuatBarangDetailID , Mb_No ,Hargatotal from InvoiceDetail where No_Invoice = '" & grid.GetMasterRowKeyValue() & "' and ( status =1 or status =7 )"
    '        lDS = SQLExecuteQuery(lSTR)
    '        lDT = lDS.Tables(0)
    '        If lDT.Rows.Count > 0 Then
    '            For e As Integer = 0 To lDT.Rows.Count - 1
    '                If grid.GetMasterRowFieldValues("DP") = "No" Then
    '                    sqlstring = " SELECT id.Hargatotal,MBD.ID,MBD.Mb_No,MBD.PackedContID,WH.Nama_Barang,MBD.Quantity,WH.Container FROM InvoiceDetail id " & _
    '                        " left join V_MuatBarang_Detail MBD on (id.MuatBarangDetailID = MBD.IDDetail and id.Mb_No = MBD.Mb_No ) " & _
    '                        " left JOIN WarehouseDetail WH on (MBD.Warehouse_Id = WH.IDDetail and WH.WarehouseItem_Code = MBD.WarehouseHeaderID ) " & _
    '                        " Where id.MuatBarangDetailID = '" & lDT.Rows(e).Item("MuatBarangDetailID").ToString & "' and id.Mb_No ='" & lDT.Rows(e).Item("Mb_No").ToString & "' " & _
    '                        " AND ( MBD.status = 5 or MBD.status = 7 ) AND ( id.status = 7 or id.status = 1 ) Order by MBD.timestamp desc"
    '                Else
    '                    sqlstring = " SELECT id.Hargatotal,WH.ID,WH.Nama_Barang,WH.Quantity,WH.Container FROM InvoiceDetail id " & _
    '                        " left join WarehouseDetail WH on (id.MuatBarangDetailID = WH.IDDetail and WH.WarehouseItem_Code = id.Mb_No ) " & _
    '                        " Where id.MuatBarangDetailID = '" & lDT.Rows(e).Item("MuatBarangDetailID").ToString & "' and id.Mb_No ='" & lDT.Rows(e).Item("Mb_No").ToString & "' " & _
    '                        " AND WH.status <>0 AND ( id.status = 7 or id.status = 1 ) Order by WH.timestamp desc"
    '                End If

    '                DS = SQLExecuteQuery(sqlstring)
    '                DT = DS.Tables(0)
    '                If DT.Rows.Count > 0 Then
    '                    For i As Integer = 0 To DT.Rows.Count - 1
    '                        DR = kDT.NewRow
    '                        With DT.Rows(i)
    '                            If .Item("Container").ToString = "true" Or .Item("Container").ToString = "kubikasi" Then
    '                                kSTR = "Select cd.NamaBarang,ch.ContainerCode from ContainerDetail cd left join ContainerHeader ch on cd.ContainerCode = ch.ContainerCode where cd.ContainerCode = '" & .Item("Nama_Barang").ToString & "' and cd.status <>0 "
    '                                cDS = SQLExecuteQuery(kSTR)
    '                                cDT = cDS.Tables(0)
    '                                namabarang = ""
    '                                If cDT.Rows.Count > 0 Then
    '                                    For j As Integer = 0 To cDT.Rows.Count - 1
    '                                        If j > 3 Then
    '                                            namabarang &= "dll."
    '                                            Exit For
    '                                        End If

    '                                        If j = cDT.Rows.Count - 1 Then
    '                                            namabarang &= cDT.Rows(j).Item("NamaBarang").ToString + "."
    '                                        Else
    '                                            namabarang &= cDT.Rows(j).Item("NamaBarang").ToString + ","
    '                                        End If

    '                                    Next
    '                                    DR("Nama_Barang") = namabarang.ToString
    '                                    DR("Hargatotal") = .Item("Hargatotal").ToString
    '                                    DR("ID") = .Item("ID").ToString
    '                                    kDT.Rows.Add(DR)
    '                                End If
    '                            Else
    '                                DR("Nama_Barang") = .Item("Nama_Barang").ToString
    '                                DR("Hargatotal") = .Item("Hargatotal").ToString
    '                                DR("ID") = .Item("ID").ToString
    '                                kDT.Rows.Add(DR)
    '                            End If
    '                        End With
    '                    Next
    '                End If
    '            Next
    '        End If
    '        grid.DataSource = kDT
    '    Catch ex As Exception
    '        Response.Write("Error Load Grid Child :<BR>" & ex.ToString)
    '    End Try
    'End Sub

    'Protected Sub Grid_Invoice_Child_DataSelect(ByVal sender As Object, ByVal e As EventArgs)
    '    Try
    '        Call load_invoice_child(TryCast(sender, ASPxGridView))
    '    Catch ex As Exception
    '        Response.Write("Error Load Grid Child DataSelect  :<BR>" & ex.ToString)
    '    End Try
    'End Sub

    Private Sub Grid_Invoice_Parent_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Grid_Asuransi_Parent.PreRender
        Grid_Asuransi_Parent.FocusedRowIndex = -1
    End Sub

    Private Sub Grid_Invoice_Parent_RowCommand(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewRowCommandEventArgs) Handles Grid_Asuransi_Parent.RowCommand
        Try
            Select Case e.CommandArgs.CommandName
                Case "PrintInvoice"
                    If validation_print() Then
                        lblError.Visible = False
                        lblError.Text = ""
                        Panel_Input.Visible = False
                        Panel_Grid.Visible = False
                        Panel_Report.Visible = True
                        'lblReport.Text = invoiceHeader()
                        lblReport.Text = invoiceHeader(Grid_Asuransi_Parent.GetRowValues(e.VisibleIndex, "No_Invoice").ToString, Grid_Asuransi_Parent.GetRowValues(e.VisibleIndex, "NoAsuransi").ToString)
                    End If

            End Select
        Catch ex As Exception
            Response.Write("Error Grid_Invoice_Parent_RowCommand <BR> : " & ex.ToString)
        End Try

    End Sub
#End Region

#Region "REPORT"
    Private Function invoiceHeader() As String
        Dim header As String
        Dim sesHeader As String
        Dim STR As String
        Dim iDT As DataTable
        Dim hDS As DataSet
        sesHeader = ddlHeader.SelectedValue
        STR = "Select * from HeaderForm where ID = '" & sesHeader & "' and status = 1;"
        hDS = SQLExecuteQuery(STR)
        iDT = hDS.Tables(0)

        Session("namaper") = iDT.Rows(0).Item("Nama").ToString

        header = "<table width=772px bgcolor=white cellpadding=0 cellspacing=0 style=""font-family:verdana;font-size:12px; "" border=0>" & _
             " <tr >" & _
             "   <td style=""width:110px;vertical-align:top;"" align=left  >" & _
             "      <img src=""" & iDT.Rows(0).Item("PathLogo").ToString & """ style=""height: 100px; width: 100px"" />" & _
             "   </td>" & _
             "   <td style=""vertical-align:top;#2c3848;"" align = center>" & _
              "      <table width=100% bgcolor=white cellpadding=0 cellspacing=0 style=""font-family:verdana;font-size:10px;"">" & _
             "         <tr>" & _
             "            <td style=""vertical-align:top"" align = center>" & _
             "              <font size=""6"" > <b>" & iDT.Rows(0).Item("Nama").ToString & " </b><font> " & _
             "            </td>" & _
             "          </tr>" & _
             "         <tr>" & _
             "            <td style=""padding-left:50px;vertical-align:top"" align = center>" & _
             "              <font size=""6"" > <b>" & iDT.Rows(0).Item("Daerah").ToString & " </b><font> " & _
             "            </td>" & _
             "          </tr>" & _
             "      </table>" & _
             "   </td>" & _
             " </tr>" & _
             " </table>" & _
             " </br>" & _
             " </br>" & _
             " </br>" & _
             " </br>" & _
             " </br>" & _
             " </br>" & _
             " </br>" & _
             " </br>" & _
             " </br>" & _
             " </br>" & _
             " <br />"
        Return header
    End Function
    'coding load report yg dicomment
    'Private Function codingcomment()

    'sqlstring = " select wd.QuotationDetailID,ih.HargaAsuransi,ih.Ditujukan,wd.Quotation_No, msh.NamaHarga,wd.Others,wd.Nama_Satuan,ih.No_Invoice,mq.Penerima,mc.Nama_Customer,mk.Nama_Kapal,mc.KotaDitunjukan as DaerahDitujukan,mc.Area,mq.Tujuan,mbd.Tanggal " & _
    '            " from InvoiceDetail ihd " & _
    '            " left join InvoiceHeader ih on ihd.No_Invoice= ih.No_Invoice" & _
    '            " left join V_MuatBarang_Detail mbd on (ihd.Mb_No= mbd.Mb_No and ihd.MuatBarangDetailID = mbd.IDDetail) " & _
    '            " left join Kapal mk on mbd.Kapal = mk.IDDetail " & _
    '            " left join V_Warehouse_Satuan wd on ( mbd.Warehouse_Id = wd.IDDetail and mbd.WarehouseHeaderID = wd.WarehouseItem_Code ) " & _
    '            " left join QuotationDetail qd on (wd.QuotationDetailID = qd.IDDetail and wd.Quotation_No = qd.Quotation_No ) " & _
    '            " left join MasterQuotation mq on qd.Quotation_No= mq.Quotation_No" & _
    '            " left join MasterCustomer mc on mq.Customer_Id= mc.Kode_Customer" & _
    '            " left join MasterHargaDefault msh on qd.SatuanID = msh.ID " & _
    '            " where (ihd.status = 7 or ihd.status = 1 ) and msh.status = 1 and ih.No_Invoice ='" & blid.ToString & "' " & _
    '            " and mk.status <> 0 and wd.status <> 0 and mq.status <> 0 and qd.status <> 0 and mc.status <> 0 and mbd.status <> 0 and ih.status <> 0 "
    'ds = SQLExecuteQuery(sqlstring)
    'dt = ds.Tables(0)


    'noinvoice = ""
    'Detail = ""
    'namabarang = ""
    'HargaTotal = 0
    'HargaSementara = 0
    'If dt.Rows.Count > 0 Then
    '    noinvoice = dt.Rows(0).Item("No_Invoice").ToString
    '    cekdt = create_table()
    '    For i As Integer = 0 To dt.Rows.Count - 1
    '        cekb = True
    '        With dt.Rows(i)

    '            If .Item("NamaHarga").ToString = "Kubik" Then
    '                STR = " select wd.QuotationDetailID, msh.NamaHarga,wd.Others,wd.Nama_Satuan,ih.No_Invoice,mq.Penerima,mc.Nama_Customer,mk.Nama_Kapal,qd.Harga,qd.Nama_Barang, " & _
    '                            " SUM(mbd.Quantity * wd.Panjang * wd.Lebar * wd.Tinggi)as Total ,SUM(mbd.Quantity * wd.Panjang * wd.Lebar * wd.Tinggi * qd.Harga)as TotalHarga, SUM(mbd.Quantity) as Quantity from InvoiceDetail ihd " & _
    '                            " left join InvoiceHeader ih on ihd.No_Invoice= ih.No_Invoice " & _
    '                            " left join V_MuatBarang_Detail mbd on (ihd.MuatBarangDetailID = mbd.IDDetail and mbd.Mb_No = ihd.Mb_No ) " & _
    '                            " left join Kapal mk on mbd.Kapal = mk.IDDetail " & _
    '                            " left join V_Warehouse_Satuan wd on (mbd.Warehouse_Id = wd.IDDetail and mbd.WarehouseHeaderID = wd.WarehouseItem_Code ) " & _
    '                            " left join QuotationDetail qd on (wd.QuotationDetailID = qd.IDDetail and wd.Quotation_No = qd.Quotation_No ) " & _
    '                            " left join MasterQuotation mq on qd.Quotation_No= mq.Quotation_No" & _
    '                            " left join MasterCustomer mc on mq.Customer_Id= mc.Kode_Customer " & _
    '                            " left join MasterHargaDefault msh on qd.SatuanID = msh.ID " & _
    '                            " where (ihd.status = 7 or ihd.status = 1 )and msh.status = 1 and ih.No_Invoice ='" & blid.ToString & "' and msh.NamaHarga = 'Kubik' " & _
    '                            " and mk.status <> 0 and wd.status <> 0 and mq.status <> 0 and qd.status <> 0 and mc.status <> 0 and mbd.status <> 0 and ih.status <> 0 " & _
    '                            " and wd.QuotationDetailID = '" & .Item("QuotationDetailID").ToString & "' and wd.Quotation_No = '" & .Item("Quotation_No").ToString & "' " & _
    '                            " GROUP BY wd.QuotationDetailID, msh.NamaHarga ,wd.Others,wd.Nama_Satuan,ih.No_Invoice,mq.Penerima,mc.Nama_Customer,mk.Nama_Kapal,qd.Harga,qd.Nama_Barang  " & _
    '                            "        having SUM(mbd.Quantity * wd.Panjang * wd.Lebar * wd.Tinggi) > 0 and SUM(mbd.Quantity * wd.Panjang * wd.Lebar * wd.Tinggi * qd.Harga)>0 and SUM(mbd.Quantity) >0 "
    '                dss = SQLExecuteQuery(STR)
    '                dts = dss.Tables(0)

    '                If dts.Rows.Count > 0 Then
    '                    For e As Integer = 0 To dts.Rows.Count - 1
    '                        With dts.Rows(e)
    '                            quantity = CDbl(.Item("Quantity").ToString)
    '                            satuan = .Item("Nama_Satuan").ToString
    '                            If i = 0 Then
    '                                DR = cekdt.NewRow()
    '                                DR("ID") = .Item("QuotationDetailID").ToString
    '                                cekdt.Rows.Add(DR)
    '                                Detail &= "( " & .Item("Total").ToString & " M3 X Rp " & (Integer.Parse(.Item("Harga").ToString).ToString("###,##.00")) & ",- = " & (Integer.Parse(.Item("TotalHarga").ToString).ToString("###,##.00")) & " ),<BR/>"
    '                                namabarang &= quantity.ToString + " " + satuan.ToString + " " + .Item("Nama_Barang").ToString + ","
    '                                HargaSementara = CDbl(.Item("TotalHarga").ToString)
    '                                HargaTotal = HargaTotal + HargaSementara
    '                            ElseIf i < dt.Rows.Count - 1 Then
    '                                If cekdt.Rows.Count > 0 Then
    '                                    For c As Integer = 0 To cekdt.Rows.Count - 1
    '                                        If cekdt.Rows(c).Item("ID") = .Item("QuotationDetailID").ToString Then
    '                                            cekb = False
    '                                        End If
    '                                    Next
    '                                End If
    '                                If cekb = True Then
    '                                    DR = cekdt.NewRow()
    '                                    DR("ID") = .Item("QuotationDetailID").ToString
    '                                    cekdt.Rows.Add(DR)
    '                                    Detail &= "( " & .Item("Total").ToString & " M3 X Rp " & (Integer.Parse(.Item("Harga").ToString).ToString("###,##.00")) & ",- = " & (Integer.Parse(.Item("TotalHarga").ToString).ToString("###,##.00")) & " ),<BR/>"
    '                                    namabarang &= quantity.ToString + " " + satuan.ToString + " " + .Item("Nama_Barang").ToString + ","
    '                                    HargaSementara = CDbl(.Item("TotalHarga").ToString)
    '                                    HargaTotal = HargaTotal + HargaSementara
    '                                End If

    '                            ElseIf i = dt.Rows.Count - 1 Then
    '                                If cekdt.Rows.Count > 0 Then
    '                                    For c As Integer = 0 To cekdt.Rows.Count - 1
    '                                        If cekdt.Rows(c).Item("ID") = .Item("QuotationDetailID").ToString Then
    '                                            cekb = False
    '                                        End If
    '                                    Next
    '                                End If
    '                                If cekb = True Then
    '                                    DR = cekdt.NewRow()
    '                                    DR("ID") = .Item("QuotationDetailID").ToString
    '                                    cekdt.Rows.Add(DR)
    '                                    Detail &= "( " & .Item("Total").ToString & " M3 X Rp " & (Integer.Parse(.Item("Harga").ToString).ToString("###,##.00")) & ",- = " & (Integer.Parse(.Item("TotalHarga").ToString).ToString("###,##.00")) & " ) "
    '                                    namabarang &= quantity.ToString + " " + satuan.ToString + " " + .Item("Nama_Barang").ToString + "."
    '                                    HargaSementara = CDbl(.Item("TotalHarga").ToString)
    '                                    HargaTotal = HargaTotal + HargaSementara
    '                                End If

    '                            End If
    '                            'HargaSementara = CDbl(.Item("TotalHarga").ToString)
    '                            'HargaTotal = HargaTotal + HargaSementara
    '                        End With
    '                    Next
    '                End If
    '            ElseIf .Item("NamaHarga").ToString = "Ton" Then
    '                STR = " select wd.QuotationDetailID, msh.NamaHarga,wd.Others,wd.Nama_Satuan,ih.No_Invoice,mq.Penerima,mc.Nama_Customer,mk.Nama_Kapal,qd.Harga,qd.Nama_Barang, " & _
    '                            " SUM(mbd.Quantity * wd.Berat) as Total ,SUM(mbd.Quantity * wd.Berat * qd.Harga) as TotalHarga, SUM(mbd.Quantity) as Quantity from InvoiceDetail ihd " & _
    '                            " left join InvoiceHeader ih on ihd.No_Invoice= ih.No_Invoice " & _
    '                            " left join V_MuatBarang_Detail mbd on (ihd.MuatBarangDetailID = mbd.IDDetail and mbd.Mb_No = ihd.Mb_No ) " & _
    '                            " left join Kapal mk on (mbd.Kapal = mk.IDDetail) " & _
    '                            " left join V_Warehouse_Satuan wd on (mbd.Warehouse_Id = wd.IDDetail and mbd.WarehouseHeaderID = wd.WarehouseItem_Code ) " & _
    '                            " left join QuotationDetail qd on (wd.QuotationDetailID = qd.IDDetail and wd.Quotation_No = qd.Quotation_No ) " & _
    '                            " left join MasterQuotation mq on qd.Quotation_No= mq.Quotation_No" & _
    '                            " left join MasterCustomer mc on mq.Customer_Id= mc.Kode_Customer " & _
    '                            " left join MasterHargaDefault msh on qd.SatuanID = msh.ID " & _
    '                            " where (ihd.status = 7 or ihd.status = 1 )and msh.status = 1 and ih.No_Invoice ='" & blid.ToString & "' and msh.NamaHarga = 'Ton' " & _
    '                            "  and mk.status <> 0 and wd.status <> 0 and mq.status <> 0 and qd.status <> 0 and mc.status <> 0 and mbd.status <> 0 and ih.status <> 0 " & _
    '                            " and wd.QuotationDetailID = '" & .Item("QuotationDetailID").ToString & "' and wd.Quotation_No = '" & .Item("Quotation_No").ToString & "' " & _
    '                            " GROUP BY wd.QuotationDetailID, msh.NamaHarga ,wd.Others,wd.Nama_Satuan,ih.No_Invoice,mq.Penerima,mc.Nama_Customer,mk.Nama_Kapal,qd.Harga,qd.Nama_Barang  " & _
    '                            " having SUM(mbd.Quantity * wd.Berat ) > 0 and SUM(mbd.Quantity * wd.Berat * qd.Harga) > 0 and SUM(mbd.Quantity) >0 "


    '                'STR = " select wd.QuotationDetailID, msh.NamaHarga,wd.Others,mso.Nama_Satuan,ih.No_Invoice,mq.Penerima,mc.Nama_Customer,mk.Nama_Kapal,qd.Harga,qd.Nama_Barang, " & _
    '                '            " SUM(mbd.Quantity * wd.Berat )as Total ,SUM(mbd.Quantity * wd.Berat * qd.Harga) as TotalHarga, SUM(mbd.Quantity) as Quantity from InvoiceDetail ihd " & _
    '                '            " left join InvoiceHeader ih on ihd.InvoiceHeaderID= ih.ID" & _
    '                '            " left join MuatBarang mb on ih.MuatbarangID = mb.ID" & _
    '                '            " left join MuatBarangDetail mbd on ihd.MuatBarangDetailID = mbd.ID " & _
    '                '            " left join Kapal mk on mb.Kapal = mk.ID " & _
    '                '            " left join WarehouseDetail wd on mbd.Warehouse_Id = wd.ID " & _
    '                '            " left join QuotationDetail qd on wd.QuotationDetailID = qd.ID" & _
    '                '            " left join MasterQuotation mq on qd.Quotation_No= mq.Quotation_No" & _
    '                '            " left join MasterCustomer mc on mq.Customer_Id= mc.ID" & _
    '                '            " left join MasterHargaDefault msh on qd.SatuanID = msh.ID " & _
    '                '            " left join MasterSatuanOther mso on wd.Others = mso.ID " & _
    '                '            " where (ihd.status = 5 or ihd.status = 1 )and msh.status = 1 and ih.ID ='" & blid.ToString & "' and msh.NamaHarga = 'Ton' " & _
    '                '            " and wd.QuotationDetailID = '" & .Item("QuotationDetailID").ToString & "' " & _
    '                '            " GROUP BY wd.QuotationDetailID, msh.NamaHarga ,wd.Others,mso.Nama_Satuan,ih.No_Invoice,mq.Penerima,mc.Nama_Customer,mk.Nama_Kapal,qd.Harga,qd.Nama_Barang  " & _
    '                '            " having SUM(mbd.Quantity * wd.Berat ) > 0 and SUM(mbd.Quantity * wd.Berat * qd.Harga) > 0 and SUM(mbd.Quantity) >0 "
    '                dss = SQLExecuteQuery(STR)
    '                dts = dss.Tables(0)

    '                If dts.Rows.Count > 0 Then
    '                    For e As Integer = 0 To dts.Rows.Count - 1
    '                        With dts.Rows(e)
    '                            quantity = CDbl(.Item("Quantity").ToString)
    '                            satuan = .Item("Nama_Satuan").ToString
    '                            If i = 0 Then
    '                                DR = cekdt.NewRow()
    '                                DR("ID") = .Item("QuotationDetailID").ToString
    '                                cekdt.Rows.Add(DR)
    '                                Detail &= "( " & .Item("Total").ToString & " Kg X Rp " & (Integer.Parse(.Item("Harga").ToString).ToString("###,##.00")) & ",- = " & (Integer.Parse(.Item("TotalHarga").ToString).ToString("###,##.00")) & " ),<BR/>"
    '                                namabarang &= quantity.ToString + " " + satuan.ToString + " " + .Item("Nama_Barang").ToString + ","
    '                                HargaSementara = CDbl(.Item("TotalHarga").ToString)
    '                                HargaTotal = HargaTotal + HargaSementara
    '                            ElseIf i < dt.Rows.Count - 1 Then
    '                                If cekdt.Rows.Count > 0 Then
    '                                    For c As Integer = 0 To cekdt.Rows.Count - 1
    '                                        If cekdt.Rows(c).Item("ID") = .Item("QuotationDetailID").ToString Then
    '                                            cekb = False
    '                                        End If
    '                                    Next
    '                                End If
    '                                If cekb = True Then
    '                                    DR = cekdt.NewRow()
    '                                    DR("ID") = .Item("QuotationDetailID").ToString
    '                                    cekdt.Rows.Add(DR)
    '                                    Detail &= "( " & .Item("Total").ToString & " Kg X Rp " & (Integer.Parse(.Item("Harga").ToString).ToString("###,##.00")) & ",- = " & (Integer.Parse(.Item("TotalHarga").ToString).ToString("###,##.00")) & " ),<BR/>"
    '                                    namabarang &= quantity.ToString + " " + satuan.ToString + " " + .Item("Nama_Barang").ToString + ","
    '                                    HargaSementara = CDbl(.Item("TotalHarga").ToString)
    '                                    HargaTotal = HargaTotal + HargaSementara
    '                                End If

    '                            ElseIf i = dt.Rows.Count - 1 Then
    '                                If cekdt.Rows.Count > 0 Then
    '                                    For c As Integer = 0 To cekdt.Rows.Count - 1
    '                                        If cekdt.Rows(c).Item("ID") = .Item("QuotationDetailID").ToString Then
    '                                            cekb = False
    '                                        End If
    '                                    Next
    '                                End If
    '                                If cekb = True Then
    '                                    DR = cekdt.NewRow()
    '                                    DR("ID") = .Item("QuotationDetailID").ToString
    '                                    cekdt.Rows.Add(DR)
    '                                    Detail &= "( " & .Item("Total").ToString & " Kg X Rp " & (Integer.Parse(.Item("Harga").ToString).ToString("###,##.00")) & ",- = " & (Integer.Parse(.Item("TotalHarga").ToString).ToString("###,##.00")) & " ) "
    '                                    namabarang &= quantity.ToString + " " + satuan.ToString + " " + .Item("Nama_Barang").ToString + "."
    '                                    HargaSementara = CDbl(.Item("TotalHarga").ToString)
    '                                    HargaTotal = HargaTotal + HargaSementara
    '                                End If

    '                            End If
    '                            'HargaSementara = CDbl(.Item("TotalHarga").ToString)
    '                            'HargaTotal = HargaTotal + HargaSementara
    '                        End With
    '                    Next
    '                End If
    '            ElseIf .Item("NamaHarga").ToString = "Container" Then

    '                STR = " select wd.QuotationDetailID, msh.NamaHarga,wd.Others,wd.Nama_Satuan,ih.No_Invoice,mq.Penerima,mc.Nama_Customer,mk.Nama_Kapal,qd.Harga,qd.Nama_Barang, " & _
    '                     " SUM(mbd.Quantity ) as Total ,SUM(mbd.Quantity * qd.Harga) as TotalHarga, SUM(mbd.Quantity) as Quantity from InvoiceDetail ihd " & _
    '                     " left join InvoiceHeader ih on ihd.No_Invoice= ih.No_Invoice " & _
    '                     " left join V_MuatBarang_Detail mbd on (ihd.MuatBarangDetailID = mbd.IDDetail and mbd.Mb_No = ihd.Mb_No ) " & _
    '                     " left join Kapal mk on mbd.Kapal = mk.IDDetail " & _
    '                     " left join V_Warehouse_Satuan wd on (mbd.Warehouse_Id = wd.IDDetail and mbd.WarehouseHeaderID = wd.WarehouseItem_Code ) " & _
    '                     " left join QuotationDetail qd on (wd.QuotationDetailID = qd.IDDetail and wd.Quotation_No = qd.Quotation_No ) " & _
    '                     " left join MasterQuotation mq on qd.Quotation_No= mq.Quotation_No" & _
    '                     " left join MasterCustomer mc on mq.Customer_Id= mc.Kode_Customer " & _
    '                     " left join MasterHargaDefault msh on qd.SatuanID = msh.ID " & _
    '                     " where (ihd.status = 7 or ihd.status = 1 )and msh.status = 1 and ih.No_Invoice ='" & blid.ToString & "' and (msh.NamaHarga = 'Container' or msh.NamaHarga = 'Kontainer' ) " & _
    '                     " and mk.status <> 0 and wd.status <> 0 and mq.status <> 0 and qd.status <> 0 and mc.status <> 0 and mbd.status <> 0 and ih.status <> 0 " & _
    '                     " and wd.QuotationDetailID = '" & .Item("QuotationDetailID").ToString & "' and wd.Quotation_No = '" & .Item("Quotation_No").ToString & "' " & _
    '                     " GROUP BY wd.QuotationDetailID, msh.NamaHarga ,wd.Others,wd.Nama_Satuan,ih.No_Invoice,mq.Penerima,mc.Nama_Customer,mk.Nama_Kapal,qd.Harga,qd.Nama_Barang  " & _
    '                     " having SUM(mbd.Quantity) > 0 and SUM(mbd.Quantity * qd.Harga) > 0 and SUM(mbd.Quantity) >0 "


    '                'STR = " select wd.QuotationDetailID, msh.NamaHarga,wd.Others,mso.Nama_Satuan,ih.No_Invoice,mq.Penerima,mc.Nama_Customer,mk.Nama_Kapal,qd.Harga,qd.Nama_Barang, " & _
    '                '            " SUM(mbd.Quantity )as Total ,SUM(mbd.Quantity * qd.Harga)as TotalHarga, SUM(mbd.Quantity) as Quantity from InvoiceDetail ihd " & _
    '                '            " left join InvoiceHeader ih on ihd.InvoiceHeaderID= ih.ID" & _
    '                '            " left join MuatBarang mb on ih.MuatbarangID = mb.ID" & _
    '                '            " left join MuatBarangDetail mbd on ihd.MuatBarangDetailID = mbd.ID " & _
    '                '            " left join Kapal mk on mb.Kapal = mk.ID " & _
    '                '            " left join WarehouseDetail wd on mbd.Warehouse_Id = wd.ID " & _
    '                '            " left join QuotationDetail qd on wd.QuotationDetailID = qd.ID" & _
    '                '            " left join MasterQuotation mq on qd.Quotation_No= mq.Quotation_No" & _
    '                '            " left join MasterCustomer mc on mq.Customer_Id= mc.ID" & _
    '                '            " left join MasterHargaDefault msh on qd.SatuanID = msh.ID " & _
    '                '            " left join MasterSatuanOther mso on wd.Others = mso.ID " & _
    '                '            " where (ihd.status = 5 or ihd.status = 1 )and msh.status = 1 and ih.ID ='" & blid.ToString & "' and msh.NamaHarga = 'Container' " & _
    '                '            " and wd.QuotationDetailID = '" & .Item("QuotationDetailID").ToString & "' " & _
    '                '            " GROUP BY wd.QuotationDetailID, msh.NamaHarga ,wd.Others,mso.Nama_Satuan,ih.No_Invoice,mq.Penerima,mc.Nama_Customer,mk.Nama_Kapal,qd.Harga,qd.Nama_Barang  " & _
    '                '            " having SUM(mbd.Quantity) > 0 and SUM(mbd.Quantity * qd.Harga) > 0 and SUM(mbd.Quantity) >0 "
    '                dss = SQLExecuteQuery(STR)
    '                dts = dss.Tables(0)

    '                If dts.Rows.Count > 0 Then
    '                    For e As Integer = 0 To dts.Rows.Count - 1
    '                        With dts.Rows(e)
    '                            quantity = CDbl(.Item("Quantity").ToString)
    '                            satuan = "Container"
    '                            If i = 0 Then
    '                                DR = cekdt.NewRow()
    '                                DR("ID") = .Item("QuotationDetailID").ToString
    '                                cekdt.Rows.Add(DR)
    '                                Detail &= "( " & .Item("Total").ToString & " Container X Rp " & (Integer.Parse(.Item("Harga").ToString).ToString("###,##.00")) & ",- = " & (Integer.Parse(.Item("TotalHarga").ToString).ToString("###,##.00")) & " ),<BR/>"
    '                                namabarang &= quantity.ToString + " " + satuan.ToString + " " + .Item("Nama_Barang").ToString + ","
    '                                HargaSementara = CDbl(.Item("TotalHarga").ToString)
    '                                HargaTotal = HargaTotal + HargaSementara
    '                            ElseIf i < dt.Rows.Count - 1 Then
    '                                If cekdt.Rows.Count > 0 Then
    '                                    For c As Integer = 0 To cekdt.Rows.Count - 1
    '                                        If cekdt.Rows(c).Item("ID") = .Item("QuotationDetailID").ToString Then
    '                                            cekb = False
    '                                        End If
    '                                    Next
    '                                End If
    '                                If cekb = True Then
    '                                    DR = cekdt.NewRow()
    '                                    DR("ID") = .Item("QuotationDetailID").ToString
    '                                    cekdt.Rows.Add(DR)
    '                                    Detail &= "( " & .Item("Total").ToString & " Container X Rp " & (Integer.Parse(.Item("Harga").ToString).ToString("###,##.00")) & ",- = " & (Integer.Parse(.Item("TotalHarga").ToString).ToString("###,##.00")) & " ),<BR/>"
    '                                    namabarang &= quantity.ToString + " " + satuan.ToString + " " + .Item("Nama_Barang").ToString + ","
    '                                    HargaSementara = CDbl(.Item("TotalHarga").ToString)
    '                                    HargaTotal = HargaTotal + HargaSementara
    '                                End If
    '                            ElseIf i = dt.Rows.Count - 1 Then
    '                                If cekdt.Rows.Count > 0 Then
    '                                    For c As Integer = 0 To cekdt.Rows.Count - 1
    '                                        If cekdt.Rows(c).Item("ID") = .Item("QuotationDetailID").ToString Then
    '                                            cekb = False
    '                                        End If
    '                                    Next
    '                                End If
    '                                If cekb = True Then
    '                                    DR = cekdt.NewRow()
    '                                    DR("ID") = .Item("QuotationDetailID").ToString
    '                                    cekdt.Rows.Add(DR)
    '                                    Detail &= "( " & .Item("Total").ToString & " Container X Rp " & (Integer.Parse(.Item("Harga").ToString).ToString("###,##.00")) & ",- = " & (Integer.Parse(.Item("TotalHarga").ToString).ToString("###,##.00")) & " ) "
    '                                    namabarang &= quantity.ToString + " " + satuan.ToString + " " + .Item("Nama_Barang").ToString + "."
    '                                    HargaSementara = CDbl(.Item("TotalHarga").ToString)
    '                                    HargaTotal = HargaTotal + HargaSementara
    '                                End If
    '                            End If

    '                        End With
    '                    Next
    '                End If
    '            ElseIf .Item("NamaHarga").ToString = "Kubikasi" Then

    '                STR = " select wd.QuotationDetailID, msh.NamaHarga,wd.Others,wd.Nama_Satuan,ih.No_Invoice,mq.Penerima,mc.Nama_Customer,mk.Nama_Kapal,qd.Harga,qd.Nama_Barang, " & _
    '                     " SUM(mbd.Quantity ) as Total ,SUM(mbd.Quantity * qd.Harga) as TotalHarga, SUM(mbd.Quantity) as Quantity from InvoiceDetail ihd " & _
    '                     " left join InvoiceHeader ih on ihd.No_Invoice= ih.No_Invoice " & _
    '                     " left join V_MuatBarang_Detail mbd on (ihd.MuatBarangDetailID = mbd.IDDetail and mbd.Mb_No = ihd.Mb_No ) " & _
    '                     " left join Kapal mk on mbd.Kapal = mk.IDDetail " & _
    '                     " left join V_Warehouse_Satuan wd on (mbd.Warehouse_Id = wd.IDDetail and mbd.WarehouseHeaderID = wd.WarehouseItem_Code ) " & _
    '                     " left join QuotationDetail qd on (wd.QuotationDetailID = qd.IDDetail and wd.Quotation_No = qd.Quotation_No ) " & _
    '                     " left join MasterQuotation mq on qd.Quotation_No= mq.Quotation_No" & _
    '                     " left join MasterCustomer mc on mq.Customer_Id= mc.Kode_Customer " & _
    '                     " left join MasterHargaDefault msh on qd.SatuanID = msh.ID " & _
    '                     " where (ihd.status = 7 or ihd.status = 1 )and msh.status = 1 and ih.No_Invoice ='" & blid.ToString & "' and (msh.NamaHarga = 'Kubikasi' or msh.NamaHarga = 'kubikasi' ) " & _
    '                     " and mk.status <> 0 and wd.status <> 0 and mq.status <> 0 and qd.status <> 0 and mc.status <> 0 and mbd.status <> 0 and ih.status <> 0 " & _
    '                     " and wd.QuotationDetailID = '" & .Item("QuotationDetailID").ToString & "' and wd.Quotation_No = '" & .Item("Quotation_No").ToString & "' " & _
    '                     " GROUP BY wd.QuotationDetailID, msh.NamaHarga ,wd.Others,wd.Nama_Satuan,ih.No_Invoice,mq.Penerima,mc.Nama_Customer,mk.Nama_Kapal,qd.Harga,qd.Nama_Barang  " & _
    '                     " having SUM(mbd.Quantity) > 0 and SUM(mbd.Quantity * qd.Harga) > 0 and SUM(mbd.Quantity) >0 "


    '                'STR = " select wd.QuotationDetailID, msh.NamaHarga,wd.Others,mso.Nama_Satuan,ih.No_Invoice,mq.Penerima,mc.Nama_Customer,mk.Nama_Kapal,qd.Harga,qd.Nama_Barang, " & _
    '                '            " SUM(mbd.Quantity )as Total ,SUM(mbd.Quantity * qd.Harga)as TotalHarga, SUM(mbd.Quantity) as Quantity from InvoiceDetail ihd " & _
    '                '            " left join InvoiceHeader ih on ihd.InvoiceHeaderID= ih.ID" & _
    '                '            " left join MuatBarang mb on ih.MuatbarangID = mb.ID" & _
    '                '            " left join MuatBarangDetail mbd on ihd.MuatBarangDetailID = mbd.ID " & _
    '                '            " left join Kapal mk on mb.Kapal = mk.ID " & _
    '                '            " left join WarehouseDetail wd on mbd.Warehouse_Id = wd.ID " & _
    '                '            " left join QuotationDetail qd on wd.QuotationDetailID = qd.ID" & _
    '                '            " left join MasterQuotation mq on qd.Quotation_No= mq.Quotation_No" & _
    '                '            " left join MasterCustomer mc on mq.Customer_Id= mc.ID" & _
    '                '            " left join MasterHargaDefault msh on qd.SatuanID = msh.ID " & _
    '                '            " left join MasterSatuanOther mso on wd.Others = mso.ID " & _
    '                '            " where (ihd.status = 5 or ihd.status = 1 )and msh.status = 1 and ih.ID ='" & blid.ToString & "' and msh.NamaHarga = 'Container' " & _
    '                '            " and wd.QuotationDetailID = '" & .Item("QuotationDetailID").ToString & "' " & _
    '                '            " GROUP BY wd.QuotationDetailID, msh.NamaHarga ,wd.Others,mso.Nama_Satuan,ih.No_Invoice,mq.Penerima,mc.Nama_Customer,mk.Nama_Kapal,qd.Harga,qd.Nama_Barang  " & _
    '                '            " having SUM(mbd.Quantity) > 0 and SUM(mbd.Quantity * qd.Harga) > 0 and SUM(mbd.Quantity) >0 "
    '                dss = SQLExecuteQuery(STR)
    '                dts = dss.Tables(0)

    '                If dts.Rows.Count > 0 Then
    '                    For e As Integer = 0 To dts.Rows.Count - 1
    '                        With dts.Rows(e)
    '                            quantity = CDbl(.Item("Quantity").ToString)
    '                            satuan = "Colly"
    '                            If i = 0 Then
    '                                DR = cekdt.NewRow()
    '                                DR("ID") = .Item("QuotationDetailID").ToString
    '                                cekdt.Rows.Add(DR)
    '                                Detail &= "( " & .Item("Total").ToString & " Colly X Rp " & (Integer.Parse(.Item("Harga").ToString).ToString("###,##.00")) & ",- = " & (Integer.Parse(.Item("TotalHarga").ToString).ToString("###,##.00")) & " ),<BR/>"
    '                                namabarang &= quantity.ToString + " " + satuan.ToString + " " + .Item("Nama_Barang").ToString + ","
    '                                HargaSementara = CDbl(.Item("TotalHarga").ToString)
    '                                HargaTotal = HargaTotal + HargaSementara
    '                            ElseIf i < dt.Rows.Count - 1 Then
    '                                If cekdt.Rows.Count > 0 Then
    '                                    For c As Integer = 0 To cekdt.Rows.Count - 1
    '                                        If cekdt.Rows(c).Item("ID") = .Item("QuotationDetailID").ToString Then
    '                                            cekb = False
    '                                        End If
    '                                    Next
    '                                End If
    '                                If cekb = True Then
    '                                    DR = cekdt.NewRow()
    '                                    DR("ID") = .Item("QuotationDetailID").ToString
    '                                    cekdt.Rows.Add(DR)
    '                                    Detail &= "( " & .Item("Total").ToString & " Colly X Rp " & (Integer.Parse(.Item("Harga").ToString).ToString("###,##.00")) & ",- = " & (Integer.Parse(.Item("TotalHarga").ToString).ToString("###,##.00")) & " ),<BR/>"
    '                                    namabarang &= quantity.ToString + " " + satuan.ToString + " " + .Item("Nama_Barang").ToString + ","
    '                                    HargaSementara = CDbl(.Item("TotalHarga").ToString)
    '                                    HargaTotal = HargaTotal + HargaSementara
    '                                End If
    '                            ElseIf i = dt.Rows.Count - 1 Then
    '                                If cekdt.Rows.Count > 0 Then
    '                                    For c As Integer = 0 To cekdt.Rows.Count - 1
    '                                        If cekdt.Rows(c).Item("ID") = .Item("QuotationDetailID").ToString Then
    '                                            cekb = False
    '                                        End If
    '                                    Next
    '                                End If
    '                                If cekb = True Then
    '                                    DR = cekdt.NewRow()
    '                                    DR("ID") = .Item("QuotationDetailID").ToString
    '                                    cekdt.Rows.Add(DR)
    '                                    Detail &= "( " & .Item("Total").ToString & " Colly X Rp " & (Integer.Parse(.Item("Harga").ToString).ToString("###,##.00")) & ",- = " & (Integer.Parse(.Item("TotalHarga").ToString).ToString("###,##.00")) & " ) "
    '                                    namabarang &= quantity.ToString + " " + satuan.ToString + " " + .Item("Nama_Barang").ToString + "."
    '                                    HargaSementara = CDbl(.Item("TotalHarga").ToString)
    '                                    HargaTotal = HargaTotal + HargaSementara
    '                                End If
    '                            End If

    '                        End With
    '                    Next
    '                End If
    '            ElseIf .Item("NamaHarga").ToString = "Unit" Then

    '                STR = " select wd.QuotationDetailID, msh.NamaHarga,wd.Others,wd.Nama_Satuan,ih.No_Invoice,mq.Penerima,mc.Nama_Customer,mk.Nama_Kapal,qd.Harga,qd.Nama_Barang, " & _
    '                   " SUM(mbd.Quantity ) as Total ,SUM(mbd.Quantity * qd.Harga) as TotalHarga, SUM(mbd.Quantity) as Quantity from InvoiceDetail ihd " & _
    '                   " left join InvoiceHeader ih on ihd.No_Invoice= ih.No_Invoice " & _
    '                   " left join V_MuatBarang_Detail mbd on (ihd.MuatBarangDetailID = mbd.IDDetail and mbd.Mb_No = ihd.Mb_No ) " & _
    '                   " left join Kapal mk on mbd.Kapal = mk.IDDetail " & _
    '                   " left join V_Warehouse_Satuan wd on (mbd.Warehouse_Id = wd.IDDetail and mbd.WarehouseHeaderID = wd.WarehouseItem_Code ) " & _
    '                   " left join QuotationDetail qd on (wd.QuotationDetailID = qd.IDDetail and wd.Quotation_No = qd.Quotation_No ) " & _
    '                   " left join MasterQuotation mq on qd.Quotation_No= mq.Quotation_No" & _
    '                   " left join MasterCustomer mc on mq.Customer_Id= mc.Kode_Customer " & _
    '                   " left join MasterHargaDefault msh on qd.SatuanID = msh.ID " & _
    '                   " where (ihd.status = 7 or ihd.status = 1 )and msh.status = 1 and ih.No_Invoice ='" & blid.ToString & "' and msh.NamaHarga = 'Unit'  " & _
    '                   " and mk.status <> 0 and wd.status <> 0 and mq.status <> 0 and qd.status <> 0 and mc.status <> 0 and mbd.status <> 0 and ih.status <> 0 " & _
    '                   " and wd.QuotationDetailID = '" & .Item("QuotationDetailID").ToString & "' and wd.Quotation_No = '" & .Item("Quotation_No").ToString & "' " & _
    '                   " GROUP BY wd.QuotationDetailID, msh.NamaHarga ,wd.Others,wd.Nama_Satuan,ih.No_Invoice,mq.Penerima,mc.Nama_Customer,mk.Nama_Kapal,qd.Harga,qd.Nama_Barang  " & _
    '                            "        having SUM(mbd.Quantity ) > 0 and SUM(mbd.Quantity * qd.Harga) > 0 and SUM(mbd.Quantity) >0 "


    '                'STR = " select wd.QuotationDetailID, msh.NamaHarga,wd.Others,mso.Nama_Satuan,ih.No_Invoice,mq.Penerima,mc.Nama_Customer,mk.Nama_Kapal,qd.Harga,qd.Nama_Barang, " & _
    '                '            " SUM(mbd.Quantity )as Total ,SUM(mbd.Quantity * qd.Harga)as TotalHarga, SUM(mbd.Quantity) as Quantity from InvoiceDetail ihd " & _
    '                '            " left join InvoiceHeader ih on ihd.InvoiceHeaderID= ih.ID" & _
    '                '            " left join MuatBarang mb on ih.MuatbarangID = mb.ID" & _
    '                '            " left join MuatBarangDetail mbd on ihd.MuatBarangDetailID = mbd.ID " & _
    '                '            " left join Kapal mk on mb.Kapal = mk.ID " & _
    '                '            " left join WarehouseDetail wd on mbd.Warehouse_Id = wd.ID " & _
    '                '            " left join QuotationDetail qd on wd.QuotationDetailID = qd.ID" & _
    '                '            " left join MasterQuotation mq on qd.Quotation_No= mq.Quotation_No" & _
    '                '            " left join MasterCustomer mc on mq.Customer_Id= mc.ID" & _
    '                '            " left join MasterHargaDefault msh on qd.SatuanID = msh.ID " & _
    '                '            " left join MasterSatuanOther mso on wd.Others = mso.ID " & _
    '                '            " where (ihd.status = 5 or ihd.status = 1 )and msh.status = 1 and ih.ID ='" & blid.ToString & "' and msh.NamaHarga = 'Unit' " & _
    '                '            " and wd.QuotationDetailID = '" & .Item("QuotationDetailID").ToString & "' " & _
    '                '            " GROUP BY wd.QuotationDetailID, msh.NamaHarga ,wd.Others,mso.Nama_Satuan,ih.No_Invoice,mq.Penerima,mc.Nama_Customer,mk.Nama_Kapal,qd.Harga,qd.Nama_Barang  " & _
    '                '            "        having SUM(mbd.Quantity ) > 0 and SUM(mbd.Quantity * qd.Harga) > 0 and SUM(mbd.Quantity) >0 "
    '                dss = SQLExecuteQuery(STR)
    '                dts = dss.Tables(0)

    '                If dts.Rows.Count > 0 Then
    '                    For e As Integer = 0 To dts.Rows.Count - 1
    '                        With dts.Rows(e)
    '                            quantity = CDbl(.Item("Quantity").ToString)
    '                            satuan = .Item("Nama_Satuan").ToString
    '                            If i = 0 Then
    '                                DR = cekdt.NewRow()
    '                                DR("ID") = .Item("QuotationDetailID").ToString
    '                                cekdt.Rows.Add(DR)
    '                                Detail &= "( " & .Item("Total").ToString & " Unit X Rp " & (Integer.Parse(.Item("Harga").ToString).ToString("###,##.00")) & ",- = " & (Integer.Parse(.Item("TotalHarga").ToString).ToString("###,##.00")) & " ), <BR/>"
    '                                namabarang &= quantity.ToString + " " + satuan.ToString + " " + .Item("Nama_Barang").ToString + ", "
    '                            ElseIf i < dt.Rows.Count - 1 Then
    '                                If cekdt.Rows.Count > 0 Then
    '                                    For c As Integer = 0 To cekdt.Rows.Count - 1
    '                                        If cekdt.Rows(c).Item("ID") = .Item("QuotationDetailID").ToString Then
    '                                            cekb = False
    '                                        End If
    '                                    Next
    '                                End If
    '                                If cekb = True Then
    '                                    DR = cekdt.NewRow()
    '                                    DR("ID") = .Item("QuotationDetailID").ToString
    '                                    cekdt.Rows.Add(DR)
    '                                    Detail &= "( " & .Item("Total").ToString & " Unit X Rp " & (Integer.Parse(.Item("Harga").ToString).ToString("###,##.00")) & ",- = " & (Integer.Parse(.Item("TotalHarga").ToString).ToString("###,##.00")) & " ), <BR/>"
    '                                    namabarang &= quantity.ToString + " " + satuan.ToString + " " + .Item("Nama_Barang").ToString + ", "
    '                                    HargaSementara = CDbl(.Item("TotalHarga").ToString)
    '                                    HargaTotal = HargaTotal + HargaSementara
    '                                End If
    '                            ElseIf i = dt.Rows.Count - 1 Then
    '                                If cekdt.Rows.Count > 0 Then
    '                                    For c As Integer = 0 To cekdt.Rows.Count - 1
    '                                        If cekdt.Rows(c).Item("ID") = .Item("QuotationDetailID").ToString Then
    '                                            cekb = False
    '                                        End If
    '                                    Next
    '                                End If
    '                                If cekb = True Then
    '                                    DR = cekdt.NewRow()
    '                                    DR("ID") = .Item("QuotationDetailID").ToString
    '                                    cekdt.Rows.Add(DR)
    '                                    Detail &= "( " & .Item("Total").ToString & " Unit X Rp " & (Integer.Parse(.Item("Harga").ToString).ToString("###,##.00")) & ",- = " & (Integer.Parse(.Item("TotalHarga").ToString).ToString("###,##.00")) & " )"
    '                                    namabarang &= quantity.ToString + " " + satuan.ToString + " " + .Item("Nama_Barang").ToString + "."
    '                                    HargaSementara = CDbl(.Item("TotalHarga").ToString)
    '                                    HargaTotal = HargaTotal + HargaSementara
    '                                End If
    '                            End If
    '                        End With
    '                    Next
    '                End If
    '            Else
    '                STR = " select wd.QuotationDetailID, msh.NamaHarga,wd.Others,wd.Nama_Satuan,ih.No_Invoice,mq.Penerima,mc.Nama_Customer,mk.Nama_Kapal,qd.Harga,qd.Nama_Barang, " & _
    '                  " SUM(mbd.Quantity ) as Total ,SUM(mbd.Quantity * qd.Harga) as TotalHarga, SUM(mbd.Quantity) as Quantity from InvoiceDetail ihd " & _
    '                  " left join InvoiceHeader ih on ihd.No_Invoice= ih.No_Invoice " & _
    '                  " left join V_MuatBarang_Detail mbd on (ihd.MuatBarangDetailID = mbd.IDDetail and mbd.Mb_No = ihd.Mb_No ) " & _
    '                  " left join Kapal mk on mbd.Kapal = mk.IDDetail " & _
    '                  " left join V_Warehouse_Satuan wd on (mbd.Warehouse_Id = wd.IDDetail and mbd.WarehouseHeaderID = wd.WarehouseItem_Code ) " & _
    '                  " left join QuotationDetail qd on (wd.QuotationDetailID = qd.IDDetail and wd.Quotation_No = qd.Quotation_No ) " & _
    '                  " left join MasterQuotation mq on qd.Quotation_No= mq.Quotation_No" & _
    '                  " left join MasterCustomer mc on mq.Customer_Id= mc.Kode_Customer " & _
    '                  " left join MasterHargaDefault msh on qd.SatuanID = msh.ID " & _
    '                  " where (ihd.status = 7 or ihd.status = 1 )and msh.status = 1 and ih.No_Invoice ='" & blid.ToString & "' and msh.NamaHarga = 'Satuan'  " & _
    '                  " and mk.status <> 0 and wd.status <> 0 and mq.status <> 0 and qd.status <> 0 and mc.status <> 0 and mbd.status <> 0 and ih.status <> 0 " & _
    '                  " and wd.QuotationDetailID = '" & .Item("QuotationDetailID").ToString & "' and wd.Quotation_No = '" & .Item("Quotation_No").ToString & "' " & _
    '                  " GROUP BY wd.QuotationDetailID, msh.NamaHarga ,wd.Others,wd.Nama_Satuan,ih.No_Invoice,mq.Penerima,mc.Nama_Customer,mk.Nama_Kapal,qd.Harga,qd.Nama_Barang  " & _
    '                           "        having SUM(mbd.Quantity ) > 0 and SUM(mbd.Quantity * qd.Harga) > 0 and SUM(mbd.Quantity) >0 "

    '                'STR = " select wd.QuotationDetailID, msh.NamaHarga,wd.Others,mso.Nama_Satuan,ih.No_Invoice,mq.Penerima,mc.Nama_Customer,mk.Nama_Kapal,qd.Harga,qd.Nama_Barang, " & _
    '                '           " SUM(mbd.Quantity )as Total ,SUM(mbd.Quantity * qd.Harga)as TotalHarga, SUM(mbd.Quantity) as Quantity from InvoiceDetail ihd " & _
    '                '           " left join InvoiceHeader ih on ihd.InvoiceHeaderID= ih.ID" & _
    '                '           " left join MuatBarang mb on ih.MuatbarangID = mb.ID" & _
    '                '           " left join MuatBarangDetail mbd on ihd.MuatBarangDetailID = mbd.ID " & _
    '                '           " left join Kapal mk on mb.Kapal = mk.ID " & _
    '                '           " left join WarehouseDetail wd on mbd.Warehouse_Id = wd.ID " & _
    '                '           " left join QuotationDetail qd on wd.QuotationDetailID = qd.ID" & _
    '                '           " left join MasterQuotation mq on qd.Quotation_No= mq.Quotation_No" & _
    '                '           " left join MasterCustomer mc on mq.Customer_Id= mc.ID" & _
    '                '           " left join MasterHargaDefault msh on qd.SatuanID = msh.ID " & _
    '                '           " left join MasterSatuanOther mso on wd.Others = mso.ID " & _
    '                '           " where (ihd.status = 5 or ihd.status = 1 )and msh.status = 1 and ih.ID ='" & blid.ToString & "' and msh.NamaHarga = 'Satuan' " & _
    '                '            " and wd.QuotationDetailID = '" & .Item("QuotationDetailID").ToString & "' " & _
    '                '           " GROUP BY wd.QuotationDetailID, msh.NamaHarga ,wd.Others,mso.Nama_Satuan,ih.No_Invoice,mq.Penerima,mc.Nama_Customer,mk.Nama_Kapal,qd.Harga,qd.Nama_Barang  " & _
    '                '           "        having SUM(mbd.Quantity ) > 0 and SUM(mbd.Quantity * qd.Harga) > 0 and SUM(mbd.Quantity) >0 "
    '                dss = SQLExecuteQuery(STR)
    '                dts = dss.Tables(0)

    '                If dts.Rows.Count > 0 Then
    '                    For e As Integer = 0 To dts.Rows.Count - 1
    '                        With dts.Rows(e)
    '                            quantity = CDbl(.Item("Quantity").ToString)
    '                            satuan = .Item("Nama_Satuan").ToString
    '                            If i = 0 Then
    '                                DR = cekdt.NewRow()
    '                                DR("ID") = .Item("QuotationDetailID").ToString
    '                                cekdt.Rows.Add(DR)
    '                                Detail &= "( " & .Item("Total").ToString & " Colly X Rp " & (Integer.Parse(.Item("Harga").ToString).ToString("###,##.00")) & " = " & (Integer.Parse(.Item("TotalHarga").ToString).ToString("###,##.00")) & " ),<BR/>"
    '                                namabarang &= quantity.ToString + " " + satuan.ToString + " " + .Item("Nama_Barang").ToString + ","
    '                                HargaSementara = CDbl(.Item("TotalHarga").ToString)
    '                                HargaTotal = HargaTotal + HargaSementara
    '                            ElseIf i < dt.Rows.Count - 1 Then
    '                                If cekdt.Rows.Count > 0 Then
    '                                    For c As Integer = 0 To cekdt.Rows.Count - 1
    '                                        If cekdt.Rows(c).Item("ID") = .Item("QuotationDetailID").ToString Then
    '                                            cekb = False
    '                                        End If
    '                                    Next
    '                                End If
    '                                If cekb = True Then
    '                                    DR = cekdt.NewRow()
    '                                    DR("ID") = .Item("QuotationDetailID").ToString
    '                                    cekdt.Rows.Add(DR)
    '                                    Detail &= "( " & .Item("Total").ToString & " Colly X Rp " & (Integer.Parse(.Item("Harga").ToString).ToString("###,##.00")) & " = " & (Integer.Parse(.Item("TotalHarga").ToString).ToString("###,##.00")) & " ),<BR/>"
    '                                    namabarang &= quantity.ToString + " " + satuan.ToString + " " + .Item("Nama_Barang").ToString + ","
    '                                    HargaSementara = CDbl(.Item("TotalHarga").ToString)
    '                                    HargaTotal = HargaTotal + HargaSementara
    '                                End If

    '                            ElseIf i = dt.Rows.Count - 1 Then
    '                                If cekdt.Rows.Count > 0 Then
    '                                    For c As Integer = 0 To cekdt.Rows.Count - 1
    '                                        If cekdt.Rows(c).Item("ID") = .Item("QuotationDetailID").ToString Then
    '                                            cekb = False
    '                                        End If
    '                                    Next
    '                                End If
    '                                If cekb = True Then
    '                                    DR = cekdt.NewRow()
    '                                    DR("ID") = .Item("QuotationDetailID").ToString
    '                                    cekdt.Rows.Add(DR)
    '                                    Detail &= "( " & .Item("Total").ToString & " Colly X Rp " & (Integer.Parse(.Item("Harga").ToString).ToString("###,##.00")) & " = " & (Integer.Parse(.Item("TotalHarga").ToString).ToString("###,##.00")) & " )"
    '                                    namabarang &= quantity.ToString + " " + satuan.ToString + " " + .Item("Nama_Barang").ToString + "."
    '                                    HargaSementara = CDbl(.Item("TotalHarga").ToString)
    '                                    HargaTotal = HargaTotal + HargaSementara
    '                                End If

    '                            End If
    '                            'HargaSementara = CDbl(.Item("TotalHarga").ToString)
    '                            'HargaTotal = HargaTotal + HargaSementara
    '                        End With
    '                    Next
    '                End If
    '            End If
    '        End With
    '    Next
    'End If
    'End Function
    Private Function invoiceHeader(ByVal InvoiceNo As String, ByVal AsuransiNo As String) As String
        Dim HeaderReport As String
        Dim Tanggal As Date
        Dim QuoNo As String
        Dim year As String
        Dim sqlstring As String
        Dim ds As DataSet
        Dim dt As DataTable
        Dim cekstring As String = ""
        Dim Detail As String
        Dim hargaasuransi As Double
        Dim HargaTotal As Double
        Dim noinvoice As String
        Dim detail2 As String
        Dim satuan As String
        Dim namabarang As String
        Dim ejaanharga As String
        Dim NoAsuransi As String = ""
        Dim discount As Double = 0

        'sqlstring = " select Tujuan , Nama_Customer, Kapal, Nahkoda, Penerima from BillLand bl "
        QuoNo = Session("Quotation_No")
        Tanggal = CDate(Now).ToString("dd MMMMM yyyy")
        year = CDate(Tanggal).ToString("yyyy")
        namabarang = ""
        Detail = ""
        satuan = ""
        detail2 = ""
        noinvoice = InvoiceNo.ToString
        If ddltype.SelectedValue = "Freight" Then

            sqlstring = "select wd.WarehouseItem_Code,ih.Ditujukan,ih.No_Invoice,qd.Quotation_No, wd.Container,wd.Nama_Satuan,ih.Ditujukan, " & _
                                           "id.Hargatotal as TotalHarga,mh.NamaHarga,qd.Nama_Barang,qd.Harga,id.Paid,SUM(mbd.Quantity) as quantity, " & _
                                           "mc.Jenis_Perusahaan + ' ' + mc.Nama_Customer as Nama_Customer,mq.Penerima,mk.Nama_Kapal,ih.DaerahDitujukan as DaerahDitujukan, mc.KotaDitunjukan as kotaaslbrg, mc.Area,mq.Tujuan,mbd.Tanggal,ih.Keterangan," & _
                                           "ih.NoAsuransi,ih.HargaAsuransi, ih.Premi, ih.Polis, ih.TotalAsuransi, " & _
                                           "(SELECT CASE " & _
                                           "	When wd.Container='true' then " & _
                                           "		( " & _
                                            "       SUM(mbd.Quantity) " & _
                                           "		) " & _
                                           "	when wd.Container='kubikasi' then " & _
                                           "		( " & _
                                            "       SUM((wd.Panjang * wd.Lebar * wd.Tinggi * mbd.Quantity)) " & _
                                           "		) " & _
                                           "	else " & _
                                           "		( " & _
                                           "		(SELECT case  " & _
                                           "			when (mh.NamaHarga = 'Kubik' or mh.NamaHarga = 'kubik') then " & _
                                           "				( " & _
                                            "       SUM((wd.Panjang * wd.Lebar * wd.Tinggi * mbd.Quantity)) " & _
                                           "				) " & _
                                           "			when mh.NamaHarga = 'Ton' or mh.NamaHarga = 'ton' or mh.NamaHarga = 'Berat' or mh.NamaHarga ='berat' then " & _
                                           "				( " & _
                                            "       SUM((wd.Berat * mbd.Quantity)/1000) " & _
                                           "				) " & _
                                           "			when mh.NamaHarga = 'Unit' or mh.NamaHarga = 'unit' then " & _
                                           "				( " & _
                                            "       SUM(mbd.Quantity) " & _
                                           "				) " & _
                                           "			else( " & _
                                            "       SUM(mbd.Quantity) " & _
                                           "				) " & _
                                           "			end) " & _
                                           "		) " & _
                                           "	end) as Total " & _
                                           "from InvoiceDetail id ,V_MuatBarang_Detail mbd, V_Warehouse_Satuan wd ,QuotationDetail qd  " & _
                                          " ,MasterHargaDefault mh ,MasterCustomer mc, Kapal mk,MasterQuotation mq,InvoiceHeader ih " & _
                                            "       where(ih.No_Invoice = ID.No_Invoice And (ID.QuotationDetailID = qd.IDDetail And ID.Mb_No = mbd.Mb_No)) " & _
                                                   "and (mbd.Warehouse_Id= wd.IDDetail and mbd.WarehouseHeaderID = wd.WarehouseItem_Code) and " & _
                                           "( wd.Quotation_No = qd.Quotation_No and wd.QuotationDetailID= qd.IDDetail ) " & _
                                           "and qd.SatuanID = mh.ID and mbd.Customer_Id = mc.Kode_Customer " & _
                                           "and mk.IDDetail = ih.KapalID and mq.Quotation_No = qd.Quotation_No " & _
                                           "and  wd.status <> 0 and qd.status <>0 and mh.status = 1 and id.status <> 0 and ih.status <>0 " & _
                                           "and mbd.status<>0 and mc.status = 1 and mk.status = 1 and ih.No_Invoice ='" & InvoiceNo.ToString & "'  " & _
                                           "GROUP BY wd.WarehouseItem_Code,ih.TotalAsuransi,ih.No_Invoice,wd.QuotationDetailID, wd.Container, wd.Nama_Satuan ,mh.NamaHarga,qd.Nama_Barang,ih.TotalAsuransi, " & _
                                            "ID.Paid, mc.Nama_Customer, mq.Penerima, mk.Nama_Kapal, mc.KotaDitunjukan,qd.Quotation_No,qd.Harga, mc.Area, mq.Tujuan, mbd.Tanggal, ih.Ditujukan,ih.DaerahDitujukan, mc.Jenis_Perusahaan,ih.Keterangan,ih.total,id.Hargatotal, " & _
                                            "id.Hargatotal,ih.NoAsuransi,ih.Polis, ih.Premi, ih.HargaAsuransi"
            ds = SQLExecuteQuery(sqlstring)
            dt = ds.Tables(0)
            If dt.Rows.Count > 0 Then

                For i As Integer = 0 To dt.Rows.Count - 1
                    With dt.Rows(i)
                        If .Item("Container") = "true" Then
                            satuan = "Container"
                        Else
                            satuan = .Item("Nama_Satuan").ToString
                        End If

                        If i = dt.Rows.Count - 1 Then
                            namabarang &= .Item("Quantity").ToString + " " + satuan + " " + .Item("Nama_Barang").ToString
                        Else
                            namabarang &= .Item("Quantity").ToString + " " + satuan + " " + .Item("Nama_Barang").ToString + ","
                        End If
                    End With
                Next
                AsuransiNo = dt.Rows(0).Item("NoAsuransi").ToString

                hargaasuransi = CDbl(dt.Rows(0).Item("HargaAsuransi").ToString) * (CDbl(dt.Rows(0).Item("Premi").ToString) / 100)

                Detail &= dt.Rows(0).Item("Premi").ToString + "% X Rp. " + (Double.Parse(dt.Rows(0).Item("HargaAsuransi").ToString).ToString("###,##")) & ",-  <BR/> "
                Detail &= "Biaya Polis/Materai  <BR/> "
                detail2 &= "       =  RP.  " + Cek_Data(Format(CDbl(hargaasuransi), "##,###,###,##.000").ToString) + " ,-<BR/>"
                detail2 &= "       =  Rp.  " + "<u>" + Cek_Data(Format(CDbl(dt.Rows(0).Item("Polis").ToString), "##,###,###,##.000").ToString) + " ,-</u><BR/>"
                detail2 &= "       =  Rp.  " + "<u> " + Cek_Data(Format(CDbl(dt.Rows(0).Item("TotalAsuransi").ToString), "##,###,###,##.000").ToString) + ", -</u>  "
            End If
            HargaTotal = dt.Rows(0).Item("TotalAsuransi").ToString
            ejaanharga = Bilangan2(HargaTotal.ToString("###,##.00"))

            HeaderReport = "<Table width=772px><tr style=""height:83px""><td></td></tr></table> " & _
                          "<table width=772px bgcolor=white cellpadding=0 cellspacing=0 style=""font-family:verdana;font-size:12px;position:relative"" >" & _
                          " <tr>" & _
                          "    <td valign=""top"" align=right >" & _
                          "        <b><font size=""3""> " & AsuransiNo.ToString & "</font></b> " & _
                          "    </td>" & _
                          "    <td valign=""top"" align=right style=""width:40px;"" >" & _
                          "    </td>" & _
                          " </tr>" & _
                          "</table>" & _
                          "<Table width=772px><tr style=""height:3px""><td></td></tr></table> "

            HeaderReport &= "<table width=772px bgcolor=white cellpadding=0 cellspacing=0 style=""font-family:verdana;font-size:12px;position:relative"" >" & _
                       " <tr style=""height:30px"">" & _
                       "    <td align=left  style=""width:170px;"" >" & _
                       "       &nbsp;" & _
                       "    </td>" & _
                       "    <td align=left  style=""width:273px;"">" & _
                       " " & TukarNamaPerusahaan(dt.Rows(0).Item("Ditujukan").ToString) & _
                       "    </td>" & _
                       "    <td align=right>" & _
                       "        Di :" & _
                       "    </td>" & _
                       "    <td align=left style=""padding-left:14px;"">" & _
                       " " & dt.Rows(0).Item("DaerahDitujukan").ToString & _
                       "    </td>" & _
                       " </tr>" & _
                       "</table>" & _
                       " <div style=""height:13px;""> " & _
                       " &nbsp; " & _
                       " </div>"

            HeaderReport &= "<table width=772px bgcolor=white cellpadding=0 cellspacing=0 style=""height:238px;font-family:verdana;font-size:12px;position:relative"" >" & _
                       " <tr>" & _
                       "    <td align=left style=""width:170px;"">" & _
                       "        &nbsp; " & _
                       "    </td>" & _
                       "    <td align=left> " & _
                       " #" & ejaanharga & " RUPIAH# " & _
                       "    </td>" & _
                       " </tr>" & _
                       " <tr >" & _
                       "    <td align=left valign=""top"" >" & _
                       "        &nbsp;" & _
                       "    </td>"
            HeaderReport &= "    <td colspan=""2"" align=left valign=""top""> <br />" & _
                                   " Biaya Asuransi Pengakutan  " & namabarang.ToString & " dari " & dt.Rows(0).Item("DaerahDitujukan").ToString & " ke " & TxtKotaTujuan.Text.ToString & " dengan " & dt.Rows(0).Item("Nama_Kapal").ToString & " tanggal " & CDate(dt.Rows(0).Item("Tanggal").ToString).ToString("dd MMMM yyyy") & ".<BR /> " & _
                                   "<table>" & _
                                 "          <tr>" & _
                                 "              <td valign=""top"">" & _
                                 "                  " & Detail.ToString & _
                                 "              </td>" & _
                                 "              <td valign=""top"">" & _
                                 "                  " & detail2.ToString & _
                                 "              </td>" & _
                                 "          </tr>" & _
                                 "      </table>" & _
                                   "    </td>" & _
                                   " </tr>"



            'HeaderReport &= " <tr >" & _
            '                     "    <td colspan=""2"" align=left valign=""top"" style="""">" & _
            '                     "        &nbsp;" & _
            '                     "    </td>" & _
            '                     "    <td align=left valign=""top"" style="""">" & _
            '                     "        &nbsp; " & _
            '                     "    </td>" & _
            '                     "    <td align=left valign=""top"" style=""padding-left:35px;"">" & _
            '                     "      
            '                     "    </td>" & _
            '                     " </tr>" & _
            '                     "</table>"

            'HeaderReport &= " <tr >" & _
            '                "    <td align=left valign=""top"">" & _
            '                "        &nbsp;" & _
            '                "    </td>" & _
            '                "    <td align=left valign=""top"">" & _
            '                "        &nbsp; " & _
            '                "    </td>" & _
            '                "    <td align=center valign=""top"" "">" & _
            '                "       &nbsp;" & _
            '                "    </td>" & _
            '                " </tr>" & _
            '                "</table>"
        Else
            sqlstring = "SELECT AL.ID, AL.Tanggal, MC.Nama_Customer, AL.KodeCustomer, AL.TglPeriodeAwal, AL.TglPeriodeAkhir, AL.NoAsuransi, AL.HargaAsuransi, " & _
                            "AL.Premi, AL.Polis,Discount, AL.TotalAsuransi, AL.Keterangan, MC.Area " & _
                            "FROM AsuransiLain AL " & _
                            "JOIN MasterCustomer MC ON AL.KodeCustomer = Kode_Customer " & _
                            "WHERE AL.[status] = 1 AND MC.[status] <> 0 AND AL.NoAsuransi = '" & AsuransiNo & "'" & _
                            "ORDER BY ID"
            ds = SQLExecuteQuery(sqlstring)
            dt = ds.Tables(0)
            If dt.Rows.Count > 0 Then
               
                hargaasuransi = CDbl(dt.Rows(0).Item("HargaAsuransi").ToString) * CDbl(dt.Rows(0).Item("Premi").ToString) / 100
                discount = (CDbl(dt.Rows(0).Item("Discount").ToString) / 100) * hargaasuransi

                Detail &= dt.Rows(0).Item("Premi").ToString + "% X Rp. " + (Double.Parse(dt.Rows(0).Item("HargaAsuransi").ToString).ToString("###,##")) & ",-  <BR/> "
                Detail &= "Biaya Polis/Materai  <BR/> "

                If CDbl(dt.Rows(0).Item("Discount").ToString) > 0.0 Then
                    Detail &= "Discount " & dt.Rows(0).Item("Discount").ToString & " % <BR/> "
                End If

                detail2 &= "       =  RP.  " + Cek_Data(Format(CDbl(hargaasuransi), "##,###,###,##.000").ToString) + " ,-<BR/>"
                detail2 &= "       =  Rp.  " + "<u>" + Cek_Data(Format(CDbl(dt.Rows(0).Item("Polis").ToString), "##,###,###,##.000").ToString) + " ,-</u><BR/>"

                If CDbl(dt.Rows(0).Item("Discount").ToString) > 0.0 Then
                    detail2 &= "       =  Rp.  " + "<u>" + Cek_Data(Format(CDbl(discount.ToString), "##,###,###,##.000").ToString) + " ,-</u><BR/>"
                End If

                detail2 &= "       =  Rp.  " + "<u> " + Cek_Data(Format(CDbl(dt.Rows(0).Item("TotalAsuransi").ToString), "##,###,###,##.000").ToString) + ", -</u>  "
            End If
            HargaTotal = dt.Rows(0).Item("TotalAsuransi").ToString
            ejaanharga = Bilangan2(HargaTotal.ToString("###,##"))

            HeaderReport = "<Table width=772px><tr style=""height:115px""><td></td></tr></table> " & _
                          "<table width=772px bgcolor=white cellpadding=0 cellspacing=0 style=""font-family:verdana;font-size:12px;position:relative"" >" & _
                          " <tr>" & _
                          "    <td valign=""top"" align=right >" & _
                          "        <b><font size=""3""> " & AsuransiNo.ToString & "</font></b> " & _
                          "    </td>" & _
                          "    <td valign=""top"" align=right style=""width:40px;"" >" & _
                          "    </td>" & _
                          " </tr>" & _
                          "</table>" & _
                          "<Table width=772px><tr style=""height:3px""><td></td></tr></table> "

            HeaderReport &= "<table width=772px bgcolor=white cellpadding=0 cellspacing=0 style=""font-family:verdana;font-size:12px;position:relative"" >" & _
                       " <tr style=""height:30px"">" & _
                       "    <td align=left  style=""width:170px;"" >" & _
                       "       &nbsp;" & _
                       "    </td>" & _
                       "    <td align=left  style=""width:273px;"">" & _
                       " " & TukarNamaPerusahaan(dt.Rows(0).Item("Ditujukan").ToString) & _
                       "    </td>" & _
                       "    <td align=right>" & _
                       "        Di :" & _
                       "    </td>" & _
                       "    <td align=left style=""padding-left:14px;"">" & _
                       " " & dt.Rows(0).Item("DaerahDitujukan").ToString & _
                       "    </td>" & _
                       " </tr>" & _
                       "</table>" & _
                       " <div style=""height:13px;""> " & _
                       " &nbsp; " & _
                       " </div>"

            HeaderReport &= "<table width=772px bgcolor=white cellpadding=0 cellspacing=0 style=""height:238px;font-family:verdana;font-size:12px;position:relative"" >" & _
                       " <tr>" & _
                       "    <td align=left style=""width:170px;"">" & _
                       "        &nbsp; " & _
                       "    </td>" & _
                       "    <td align=left> " & _
                       " #" & ejaanharga & " RUPIAH# " & _
                       "    </td>" & _
                       " </tr>" & _
                       " <tr >" & _
                       "    <td align=left valign=""top"" >" & _
                       "        &nbsp;" & _
                       "    </td>"

            HeaderReport &= "    <td colspan=""2"" align=left valign=""top""> <br />" & _
                               " Biaya Asuransi " & dt.Rows(0).Item("Keterangan").ToString & " periode " & CDate(dt.Rows(0).Item("TglPeriodeAwal").ToString).ToString("dd MMMM yyyy") & " s/d " & CDate(dt.Rows(0).Item("TglPeriodeAkhir").ToString).ToString("dd MMMM yyyy") & " .<BR /> " & _
                               "<table>" & _
                                 "          <tr>" & _
                                 "              <td valign=""top"">" & _
                                 "                  " & Detail.ToString & _
                                 "              </td>" & _
                                 "              <td valign=""top"">" & _
                                 "                  " & detail2.ToString & _
                                 "              </td>" & _
                                 "          </tr>" & _
                                 "      </table>" & _
                               " </tr>"

            'HeaderReport &= "    <td align=left valign=""top"" style="" padding-left:35px;;"">" & _
            '                   " Biaya Asuransi " & dt.Rows(0).Item("Keterangan").ToString & " periode " & CDate(dt.Rows(0).Item("TglPeriodeAwal").ToString).ToString("dd MMMM yyyy") & " s/d " & CDate(dt.Rows(0).Item("TglPeriodeAkhir").ToString).ToString("dd MMMM yyyy") & " . " & _
            '                   "    </td>" & _
            '                   " </tr>"



            'HeaderReport &= " <tr >" & _
            '                     "    <td align=left valign=""top"" style="" border-left:1px black solid;border-bottom:1px black solid;"">" & _
            '                     "        &nbsp;" & _
            '                     "    </td>" & _
            '                     "    <td align=left valign=""top"" style="" border-bottom:1px black solid;"">" & _
            '                     "        &nbsp; " & _
            '                     "    </td>" & _
            '                     "    <td align=left valign=""top"" style="" padding-left:35px; border-right:1px black solid;border-bottom:1px black solid;"">" & _
            '                     "      <table>" & _
            '                     "          <tr>" & _
            '                     "              <td valign=""top"">" & _
            '                     "                  " & Detail.ToString & _
            '                     "              </td>" & _
            '                     "              <td valign=""top"">" & _
            '                     "                  " & detail2.ToString & _
            '                     "              </td>" & _
            '                     "          </tr>" & _
            '                     "      </table>" & _
            '                     "    </td>" & _
            '                     " </tr>" & _
            '                     "</table>"

            'HeaderReport &= " <tr >" & _
            '                "    <td align=left valign=""top"" style="" border-left:1px black solid;border-bottom:1px black solid;"">" & _
            '                "        &nbsp;" & _
            '                "    </td>" & _
            '                "    <td align=left valign=""top"" style="" border-bottom:1px black solid;"">" & _
            '                "        &nbsp; " & _
            '                "    </td>" & _
            '                "    <td align=center valign=""top"" style="" border-right:1px black solid;border-bottom:1px black solid;"">" & _
            '                "       &nbsp;" & _
            '                "    </td>" & _
            '                " </tr>" & _
            '                "</table>"

        End If





        If ChkViewRekening.Checked = True Then
            HeaderReport &= "<table width=772px bgcolor=white cellpadding=0 cellspacing=0 style=""font-family:verdana;font-size:12px;position:relative"" >" & _
                 "<tr>" & _
                 "   <td style=""border:solid 1px black; width :250px""> " & _
                 "      No Rekening : " & TxtRekening.Text.ToString & " <BR/> " & _
                 "       &nbsp;Jumlah <font size=""4""> Rp. <b>  " & HargaTotal.ToString("###,##.00") & _
                 "   </font></b></td> " & _
                 "   <td style=""width = 25%;""> " & _
                 "   &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td> " & _
                 "   <td align = ""center"" style=""width = 50%;""> " & _
                 "       " & Tanggal.ToString("dd MMMM yyyy") & _
                 "   </td>" & _
                 "</tr>" & _
                 "</table>"
        Else
            HeaderReport &= "<table width=772px bgcolor=white cellpadding=0 cellspacing=0 style=""font-family:verdana;font-size:12px;position:relative"" >" & _
                 "<tr>" & _
                 "   <td style=""width :250px;padding-left:125px;""><br /><br /> " & _
                 "       &nbsp; <font size=""4""> <b>  " & Cek_Data(HargaTotal.ToString("###,##.00")) & " ,- " & _
                 "   </font></b></td> " & _
                 "   <td style=""width = 25%;"" >" & _
                 "   &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td> " & _
                 "   <td valign=""top"" align = ""left"" style=""width = 50%;padding-left:70px;""><br /> " & _
                 "       " & Tanggal.ToString("dd MMMM yyyy") & _
                 "   </td>" & _
                 "</tr>" & _
                 "</table>"
            'HeaderReport &= "<table width=772px bgcolor=white cellpadding=0 cellspacing=0 style=""font-family:verdana;font-size:12px;position:relative"" >" & _
            '     "<tr>" & _
            '     "   <td style=""width :250px""> " & _
            '     "       &nbsp;Jumlah <font size=""4""> Rp. <b>  " & Cek_Data(HargaTotal.ToString("###,##.00")) & " ,- " & _
            '     "   </font></b></td> " & _
            '     "   <td style=""width = 25%;""> " & _
            '     "   &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td> " & _
            '     "   <td align = ""center"" style=""width = 50%;""> " & _
            '     "       " & Tanggal.ToString("dd MMMM yyyy") & _
            '     "   </td>" & _
            '     "</tr>" & _
            '     "</table>"
        End If
        'HeaderReport &= "<table width=772px bgcolor=white cellpadding=0 cellspacing=0 style=""font-family:verdana;font-size:12px;position:relative"" >" & _
        '                "   <tr>" & _
        '                "       <td>" & _
        '                "           <font size =""1"" > - pembayaran dengan Cross Chequel/Giro  </font>" & _
        '                "       </td>" & _
        '                "   </tr>" & _
        '                "   <tr>" & _
        '                "       <td>" & _
        '                "           <font size =""1"" > &nbsp; harap atas nama " & Session("namaper").ToString & _
        '                "       </td>" & _
        '                "   </tr>" & _
        '                "   <tr>" & _
        '                "       <td>" & _
        '                "           <font size =""1"" > - pembayaran per Chequel/Giro dianggap sah,</font>" & _
        '                "       </td>" & _
        '                "   </tr>" & _
        '                "   <tr>" & _
        '                "       <td>" & _
        '                "           <font size =""1"" > - bila disetujui oleh yang bersangkutan </font>" & _
        '                "       </td>" & _
        '                "   </tr>" & _
        '                "</table>"


        Return HeaderReport
    End Function
#End Region

#Region "VALIDATION"
    Private Function validation_print() As Boolean

        'If ddlHeader.SelectedIndex = 0 Then
        '    lblError.Visible = True
        '    lblError.Text = "Harap Pilih Header terlebih dahulu"
        '    Return False
        'End If

        If ddltype.SelectedValue = "Freight" Then
            If TxtKotaTujuan.Text.Trim = "" Then
                lblError.Visible = True
                lblError.Text = "isi kota tujuan "
                Return False
            End If
        End If
        

        If ChkViewRekening.Checked = True Then
            If TxtRekening.Text.Trim = "" Then
                lblError.Visible = True
                lblError.Text = "isi no rekening apabila anda ingin menampilkannya "
                Return False
            End If
        End If

        Return True
    End Function
#End Region

#Region "METHOD"

#End Region

#Region "BUTTON"
    Protected Sub btKembaliDevPeriod_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btKembaliDevPeriod.Click
        Try
            Panel_Input.Visible = True
            Panel_Grid.Visible = True
            Panel_Report.Visible = False
        Catch ex As Exception
            Response.Write("<b>Error btn kembali :</b>" & ex.ToString)
        End Try
    End Sub
#End Region

#Region "DDL"

    Private Sub load_ddl()
        Try
            sqlstring = "select ID,Nama from HeaderForm where [status] = 1 and Nama LIKE '%" & Profil & "%'"
            DS = SQLExecuteQuery(sqlstring)
            DT = DS.Tables(0)

            With ddlHeader
                .DataSource = DT
                .DataTextField = "Nama"
                .DataValueField = "ID"
                .DataBind()
            End With

        Catch ex As Exception
            Throw New Exception("Error load ddl header: " & ex.ToString)
        End Try
    End Sub

#End Region


    Private Sub ddltype_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddltype.SelectedIndexChanged
        Try
            If ddltype.SelectedValue = "Freight" Then
                load_invoice_parent("Freight")
                'ddltypeinvoice.Visible = True
                'lblReport.Visible = True
                'lbltitikdua.Visible = True
                'ddltypeinvoice.Enabled = True
            Else
                'ddltypeinvoice.Visible = False
                'hfInvoiceType.Value = ""
                'lblReport.Visible = False
                'lbltitikdua.Visible = False
                'ddltypeinvoice.Enabled = False
                load_invoice_parent("NonFreight")
            End If
        Catch ex As Exception
            Throw New Exception("Error ddltype selectedindexchange function :" & ex.ToString)
        End Try
    End Sub

    Private Sub ChkViewRekening_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkViewRekening.CheckedChanged
        Try
            If ChkViewRekening.Checked = True Then
                TxtRekening.Visible = True
            Else
                TxtRekening.Visible = False
            End If
        Catch ex As Exception
            Throw New Exception("Error function rekening checked changed :" & ex.ToString)
        End Try
    End Sub



End Class