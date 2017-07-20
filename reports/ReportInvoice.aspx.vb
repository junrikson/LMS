Imports System.Data
Imports DevExpress.Web.ASPxGridView
Imports System.Data.SqlClient
Partial Public Class ReportInvoice
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

        Try
            If Session("UserID") = Nothing Then
                FormsAuthentication.SignOut()
                Response.Redirect("Index.aspx")
            End If


            If Not Page.IsPostBack Then
                Session("Grid_Invoice_Parent_Report") = Nothing
                Panel_Input.Visible = True
                Panel_Grid.Visible = True
                Panel_Report.Visible = False
                hfMode.Value = "Insert"
                load_invoice_parent("No")
                load_ddl()
            End If

            If Not Session("Grid_Invoice_Parent_Report") Is Nothing Then
                Grid_Invoice_Parent.DataSource = CType(Session("Grid_Invoice_Parent_Report"), DataTable)
                Grid_Invoice_Parent.DataBind()
            End If
        Catch ex As Exception
            Response.Write(ex.ToString)
        End Try
        

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
    Private Sub load_invoice_parent(ByVal dp As String)
        Try
            Session("Grid_Invoice_Parent_Report") = Nothing

            If dp = "No" Then
                'sqlstring = " SELECT ih.No_Invoice as ID,ih.MuatBarangID,ih.DP,ih.No_Invoice,ih.InvoiceDate,ih.Total,mc.Nama_Customer,ih.Ditujukan,ih.KeteranganOngkosAngkut, " & _
                '                    " ih.KapalID,mk.Nama_Kapal, ih.Paid, ih.Keterangan,ih.StatusPembayaran, ih.KeteranganDS From InvoiceHeader ih " & _
                '                    " left join MuatBarang mb on ih.MuatBarangID = mb.Mb_No " & _
                '                    " left join MasterCustomer mc on mb.Customer_Id = mc.Kode_Customer " & _
                '                    " left join Kapal mk on mb.Kapal = mk.IDDetail  " & _
                '                    " Where (ih.status = 1 or ih.status = 7) and mk.status = 1 and mc.status = 1 and mb.status <> 0 and ih.DP = '" & dp.ToString & "' order by ih.ID "
                sqlstring = "SELECT ih.ID as ID2, ih.No_Invoice as ID,ih.MuatBarangID,ih.DP,ih.No_Invoice,ih.InvoiceDate,ih.Total,mc.Nama_Customer,ih.Ditujukan,ih.KeteranganOngkosAngkut,  " & _
                             "ih.KapalID,mk.Nama_Kapal, ih.Paid, ih.Keterangan,ih.StatusPembayaran, ih.KeteranganDS, ih.UserName From InvoiceHeader ih " & _
                             "left join MasterCustomer mc on ih.CodeCust = mc.Kode_Customer " & _
                             "left join Kapal mk on ih.KapalID = mk.IDDetail   " & _
                             "Where (ih.status = 1 or ih.status = 7) and " & _
                             "mk.status = 1 And mc.status = 1 " & _
                             "and ih.DP = 'no' and ih.StatusNumber = 1 " & _
                             "UNION ALL " & _
                            "select A.ID as ID2, A.No_Asuransi as ID, '' As MuatBarangID, 'No' as DP, A.No_Asuransi as No_Invoice,  " & _
                            "A.TglAsuransi as InvoiceDate, A.Harga as Total,mc.Nama_Customer, mc.Nama_Customer as Ditujukan,'' as KeteranganOngkosAngkut, " & _
                            "A.KapalID, MK.Nama_Kapal, 0 as Paid, A.Keterangan,   " & _
                            "A.statuspembayaran, '' as KeteranganDS, A.UserName   " & _
                            "FROM asuransi A   " & _
                            "JOIN MasterCustomer MC ON A.CodeCust = MC.Kode_Customer   " & _
                            "LEFT JOIN Kapal mk on A.KapalID = mk.IDDetail and mk.status = 1   " & _
                            "Where A.status = 1 And mc.status = 1 order by id2 desc "

            Else

                sqlstring = " SELECT ih.No_Invoice as ID,ih.MuatBarangID,ih.DP,ih.No_Invoice,ih.InvoiceDate,ih.Total,mc.Nama_Customer,ih.Ditujukan,ih.KeteranganOngkosAngkut, " & _
                                   " ih.KapalID,mk.Nama_Kapal, ih.Paid, ih.Keterangan, ih.StatusPembayaran, ih.KeteranganDS, ih.UserName From InvoiceHeader ih left join MuatBarang mb on ih.MuatBarangID = mb.Mb_No " & _
                                   " left join MasterCustomer mc on mb.Customer_Id = mc.Kode_Customer left join Kapal mk on mb.Kapal = mk.IDDetail  " & _
                                   " Where (ih.status = 1 or ih.status = 7) and mk.status = 1 and mc.status = 1 and mb.status <> 0 and ih.DP = '" & dp.ToString & "' and ih.StatusNumber = 1 order by ih.ID DESC "

            End If

            DS = SQLExecuteQuery(sqlstring)
            DT = DS.Tables(0)

            iDT.Columns.Add(New DataColumn("ID", GetType(String)))
            iDT.Columns.Add(New DataColumn("MuatBarangID", GetType(String)))
            iDT.Columns.Add(New DataColumn("No_Invoice", GetType(String)))
            iDT.Columns.Add(New DataColumn("InvoiceDate", GetType(DateTime)))
            iDT.Columns.Add(New DataColumn("Nama_Customer", GetType(String)))
            iDT.Columns.Add(New DataColumn("Ditujukan", GetType(String)))
            iDT.Columns.Add(New DataColumn("KapalID", GetType(String)))
            iDT.Columns.Add(New DataColumn("Kapal", GetType(String)))
            iDT.Columns.Add(New DataColumn("Total", GetType(Double)))
            iDT.Columns.Add(New DataColumn("Paid", GetType(Double)))
            iDT.Columns.Add(New DataColumn("DP", GetType(String)))
            iDT.Columns.Add(New DataColumn("Keterangan", GetType(String)))
            iDT.Columns.Add(New DataColumn("StatusPembayaran", GetType(String)))
            iDT.Columns.Add(New DataColumn("KeteranganOngkosAngkut", GetType(String)))
            iDT.Columns.Add(New DataColumn("KeteranganDS", GetType(String)))
            iDT.Columns.Add(New DataColumn("YgInput", GetType(String)))

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

                        If .Item("Nama_Kapal").ToString = "" Then
                            DR("Kapal") = ""
                        Else
                            DR("Kapal") = .Item("Nama_Kapal")
                        End If

                        DR("Total") = CDbl(.Item("Total").ToString)
                        DR("Paid") = .Item("Paid").ToString
                        DR("DP") = .Item("DP")
                        DR("keterangan") = .Item("keterangan").ToString
                        DR("StatusPembayaran") = .Item("StatusPembayaran").ToString
                        DR("KeteranganOngkosAngkut") = .Item("KeteranganOngkosAngkut").ToString
                        DR("KeteranganDS") = .Item("KeteranganDS").ToString
                        DR("YgInput") = .Item("UserName").ToString
                        iDT.Rows.Add(DR)
                    End With
                Next

                If Session("namaroles").ToString.Trim = "Admin" Or Session("namaroles").ToString.Trim = "Master Accounting" Or Session("namaroles").ToString.Trim = "Accounting" Then
                    Grid_Invoice_Parent.Columns("YgInput").Visible = True
                Else
                    Grid_Invoice_Parent.Columns("YgInput").Visible = False
                End If

                Session("Grid_Invoice_Parent_Report") = iDT
                Grid_Invoice_Parent.DataSource = iDT
                Grid_Invoice_Parent.KeyFieldName = "ID"
                Grid_Invoice_Parent.DataBind()

                If ddltype.SelectedValue = "InvoiceDP" Then
                    Grid_Invoice_Parent.Columns("Paid").Visible = True
                Else
                    Grid_Invoice_Parent.Columns("Paid").Visible = False
                End If

            Else
                Grid_Invoice_Parent.DataSource = Nothing
                Grid_Invoice_Parent.DataBind()
            End If

        Catch ex As Exception
            Response.Write("load_kapal_parent : <BR> " & ex.ToString)
        End Try
    End Sub
    Private Sub load_invoice_child(ByVal grid As ASPxGridView)
        Dim zDT As New DataTable
        Dim cDS As New DataSet
        Dim cDT As New DataTable
        Dim zDR As DataRow
        Try


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
                        "and MHD.[status] <> 0 " & _
                        "AND ID.Paid <> 100 "


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
            Response.Write("Error Load Grid Child :<BR>" & ex.ToString)
        End Try
    End Sub

    Protected Sub Grid_Invoice_Child_DataSelect(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Call load_invoice_child(TryCast(sender, ASPxGridView))
        Catch ex As Exception
            Response.Write("Error Load Grid Child DataSelect  :<BR>" & ex.ToString)
        End Try
    End Sub

    Private Sub Grid_Invoice_Parent_HtmlRowCreated(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewTableRowEventArgs) Handles Grid_Invoice_Parent.HtmlRowCreated
        Try
            Dim ID As String
            Dim str As String = ""
            Dim status As String = ""
            Dim NamaRoles As String = ""

            If e.RowType <> DevExpress.Web.ASPxGridView.GridViewRowType.Data Then
                Return
            End If
            ID = Grid_Invoice_Parent.GetRowValues(e.VisibleIndex, "No_Invoice").ToString

            str = " select StatusPrint from InvoiceHeader where No_Invoice = '" & ID.ToString & "' "
            status = SQLExecuteScalar(str)

            str = "select R.RoleID from Roles R " & _
                    "LEFT JOIN MasterUser MU on R.RoleID = MU.RoleID " & _
                    "WHERE r.[status] <> 0 " & _
                    "AND MU.[Status] <> 0 " & _
                    "AND MU.UserID = '" & Session("UserId").ToString & "' "
            NamaRoles = SQLExecuteScalar(str)


            Dim butto As LinkButton = CType(Grid_Invoice_Parent.FindRowCellTemplateControl(e.VisibleIndex, Nothing, "tbPrintInvoice"), System.Web.UI.WebControls.LinkButton)

            If status = "" And (NamaRoles.ToUpper = "RL001" Or NamaRoles.ToUpper = "RL005" Or NamaRoles.ToUpper = "RL004") Then
                butto.Visible = False
            ElseIf status = "" And (NamaRoles.ToUpper <> "RL001" Or NamaRoles.ToUpper <> "RL005" Or NamaRoles.ToUpper <> "RL004") Then
                butto.Visible = False
            ElseIf status <> 1 Or (NamaRoles.ToUpper = "RL001" Or NamaRoles.ToUpper = "RL005" Or NamaRoles.ToUpper = "RL004") Then
                butto.Visible = True
            Else
                butto.Visible = False
            End If

            'Dim butto1 As LinkButton = CType(Grid_Invoice_Parent.FindRowCellTemplateControl(e.VisibleIndex, Nothing, "tbShow"), System.Web.UI.WebControls.LinkButton)
            'If NamaRoles.ToUpper = "RL001" Or NamaRoles.ToUpper = "RL005" Or NamaRoles.ToUpper = "RL004" Then
            '    butto1.Visible = True
            'Else
            '    butto1.Visible = False
            'End If


        Catch ex As Exception
            Throw New Exception(ex.ToString)
        End Try
    End Sub

    Private Sub Grid_Invoice_Parent_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Grid_Invoice_Parent.PreRender
        Grid_Invoice_Parent.FocusedRowIndex = -1
    End Sub

    Private Sub Grid_Invoice_Parent_RowCommand(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewRowCommandEventArgs) Handles Grid_Invoice_Parent.RowCommand
        Try
            Dim pisah() As String
            Dim str As String = ""

            Select Case e.CommandArgs.CommandName
                Case "PrintInvoice"
                    If validation_print() Then
                        lblError.Visible = False
                        lblError.Text = ""


                        If Grid_Invoice_Parent.GetRowValues(e.VisibleIndex, "Keterangan").ToString.Contains("PembayaranMinimum") Then
                            pisah = Grid_Invoice_Parent.GetRowValues(e.VisibleIndex, "Keterangan").ToString.Split("-")

                            TxtMinCharge.Text = pisah(1).Trim

                            If TxtMinCharge.Text.Trim.Replace("'", "") <> "" Then
                                'lblReport.Text = invoiceHeader()
                                lblReport.Text = invoiceHeader(Grid_Invoice_Parent.GetRowValues(e.VisibleIndex, "ID").ToString, pisah(0), Grid_Invoice_Parent.GetRowValues(e.VisibleIndex, "KeteranganOngkosAngkut").ToString, Grid_Invoice_Parent.GetRowValues(e.VisibleIndex, "KeteranganDS").ToString)
                            End If

                        Else
                            'lblReport.Text = invoiceHeader()
                            lblReport.Text = invoiceHeader(Grid_Invoice_Parent.GetRowValues(e.VisibleIndex, "ID").ToString, Grid_Invoice_Parent.GetRowValues(e.VisibleIndex, "Keterangan").ToString, Grid_Invoice_Parent.GetRowValues(e.VisibleIndex, "KeteranganOngkosAngkut").ToString, Grid_Invoice_Parent.GetRowValues(e.VisibleIndex, "KeteranganDS").ToString)
                        End If

                        Panel_Input.Visible = False
                        Panel_Grid.Visible = False
                        Panel_Report.Visible = True


                        If ddltype.SelectedValue = "InvoiceNDP" Then

                            If Grid_Invoice_Parent.GetRowValues(e.VisibleIndex, "KeteranganDS").ToString = "Yes" Then

                                str = "UPDATE InvoiceHeader SET [status] = 7 where No_Invoice = '" & Grid_Invoice_Parent.GetRowValues(e.VisibleIndex, "No_Invoice").ToString & "' AND [status] <> 0; " & _
                                      "UPDATE InvoiceDetail Set [status] = 7 No_Invoice = '" & Grid_Invoice_Parent.GetRowValues(e.VisibleIndex, "No_Invoice").ToString & "' AND [status] <> 0 "
                            End If
                            Close(Grid_Invoice_Parent.GetRowValues(e.VisibleIndex, "ID").ToString, Grid_Invoice_Parent.GetRowValues(e.VisibleIndex, "No_Invoice").ToString, Grid_Invoice_Parent.GetRowValues(e.VisibleIndex, "MuatBarangID").ToString)
                            CloseHeader(Grid_Invoice_Parent.GetRowValues(e.VisibleIndex, "ID").ToString, Grid_Invoice_Parent.GetRowValues(e.VisibleIndex, "No_Invoice").ToString, Grid_Invoice_Parent.GetRowValues(e.VisibleIndex, "MuatBarangID").ToString)

                            str = "UPDATE InvoiceHeader SET StatusPrint = 1 WHERE No_Invoice = '" & Grid_Invoice_Parent.GetRowValues(e.VisibleIndex, "No_Invoice").ToString & "' " & _
                                    "AND MuatBarangID = '" & Grid_Invoice_Parent.GetRowValues(e.VisibleIndex, "MuatBarangID").ToString & "';"
                            result = SQLExecuteNonQuery(str)
                        ElseIf ddltype.SelectedValue = "InvoiceDP" Then

                            CloseDP(Grid_Invoice_Parent.GetRowValues(e.VisibleIndex, "No_Invoice").ToString, Grid_Invoice_Parent.GetRowValues(e.VisibleIndex, "MuatBarangID").ToString, Grid_Invoice_Parent.GetRowValues(e.VisibleIndex, "Paid").ToString)
                            str = "UPDATE InvoiceHeader SET StatusPrint = 1 WHERE No_Invoice = '" & Grid_Invoice_Parent.GetRowValues(e.VisibleIndex, "No_Invoice").ToString & "' " & _
                                    "AND MuatBarangID = '" & Grid_Invoice_Parent.GetRowValues(e.VisibleIndex, "MuatBarangID").ToString & "';"
                            result = SQLExecuteNonQuery(str)
                        End If

                    End If

                Case "TampilkanButton"

                    If Grid_Invoice_Parent.GetRowValues(e.VisibleIndex, "KeteranganDS").ToString = "Yes" Then
                        str = "Update InvoiceHeader Set statusPrint = 0 where No_Invoice = '" & Grid_Invoice_Parent.GetRowValues(e.VisibleIndex, "No_Invoice").ToString & "';"
                        str &= "Update InvoiceHeader set [status] = 1 where No_Invoice = '" & Grid_Invoice_Parent.GetRowValues(e.VisibleIndex, "No_Invoice").ToString & "' and [status] = 7;"
                        str &= "Update Invoicedetail set [status] = 1 where No_Invoice = '" & Grid_Invoice_Parent.GetRowValues(e.VisibleIndex, "No_Invoice").ToString & "' and [status] = 7"
                    Else
                        Update_status_Print(Grid_Invoice_Parent.GetRowValues(e.VisibleIndex, "No_Invoice").ToString, Grid_Invoice_Parent.GetRowValues(e.VisibleIndex, "MuatBarangID").ToString)
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

    Private Function invoiceHeader(ByVal blid As String, ByVal Ket As String, ByVal OngkosAngkutan As String, ByVal KetDS As String) As String
        Dim HeaderReport As String
        Dim Tanggal As String
        Dim QuoNo As String
        Dim year As String
        Dim sqlstring As String
        Dim ds As DataSet
        Dim dt As DataTable
        Dim cekdt As DataTable
        Dim cekstring As String
        Dim Detail As String
        Dim HargaSementara As Double
        Dim HargaTotal As Double
        Dim noinvoice As String
        Dim quantity As String
        Dim satuan As String
        Dim namabarang As String
        Dim ejaanharga As String
        Dim tanggalinvoice As String
        Dim bulan As String
        Dim location As String = ""
        Dim tanggalPengiriman As String = ""
        Dim hargaQuo As Double = 0
        Dim total As Double = 0
        Dim totalhargaperitem As Double = 0
        Dim TotalBiaya As Double = 0
        Dim TinggiIsiInvoice As Integer = 0
        Dim BR As String = ""

        sqlstring = " select invoicedate from invoiceheader where no_invoice = '" & blid.ToString & "' and [status] <> 0"
        tanggalinvoice = SQLExecuteScalar(sqlstring)
        bulan = CDate(tanggalinvoice).ToString("MM")

        QuoNo = Session("Quotation_No")
        Tanggal = CDate(tanggalinvoice).ToString("dd") & " " & CekBulan(bulan, 2) & " " & CDate(tanggalinvoice).ToString("yyyy")
        year = CDate(tanggalinvoice).ToString("yyyy")
        namabarang = ""
        Detail = ""
        satuan = ""
        noinvoice = blid.ToString
        If ddltype.SelectedValue = "InvoiceDP" Then
            sqlstring = "select distinct " & _
                            "WH.WarehouseItem_Code,  " & _
                            "IH.Ditujukan as Ditujukan,ih.No_Invoice,   " & _
                            "qd.Quotation_No, QD.Harga as Total,mq.Tujuan, mq.Penerima,mh.NamaHarga,qd.Nama_Barang,qd.Harga, " & _
                            "id.Hargatotal as TotalHarga, id.Paid, '0' as quantity,   " & _
                            "mk.Nama_Kapal,   " & _
                            "ih.DaerahDitujukan as DaerahDitujukan,MB.Tanggal,'A' as Nama_Satuan,    " & _
                            "ih.TotalAsuransi,MB.Mb_No,ih.Keterangan,ih.Total as totalInvoice,ISNULL(id.TotalUkuran,0) as TotUk,ih.NamaBarang  " & _
                            "FROM InvoiceHeader IH  " & _
                            "JOIN InvoiceDetail ID ON IH.No_Invoice = ID.No_Invoice " & _
                            "JOIN Kapal MK on IH.KapalID = MK.IDDetail   " & _
                            "JOIN MuatBarang MB on ID.Mb_No = MB.Mb_No  " & _
                            "JOIN WarehouseHeader WH on MB.WarehouseHeaderID = WH.WarehouseItem_Code  " & _
                            "JOIN MasterQuotation MQ ON WH.Quotation_No = MQ.Quotation_No   " & _
                            "JOIn QuotationDetail QD on (MQ.Quotation_No = QD.Quotation_No AND ID.QuotationDetailID = QD.IDDetail)  " & _
                            "join MasterHargaDefault MH ON QD.SatuanID = MH.ID  " & _
                            "where IH.No_Invoice = '" & blid & "' " & _
                            "AND IH.status <> 0   " & _
                            "AND MB.status <> 0   " & _
                            "AND ID.status <> 0   " & _
                            "AND WH.status <> 0   " & _
                            "and QD.status <> 0   " & _
                            "and MQ.status <> 0   " & _
                            "and MH.status <> 0   " & _
                            "and MK.status <> 0   " & _
                            "and ID.QuotationDetailID = QD.IDDetail "

        Else

            If Ket = "PembayaranMinimum" Then
                sqlstring = "select wd.WarehouseItem_Code,ih.TotalAsuransi as HargaAsuransi,ih.Ditujukan,ih.No_Invoice,qd.Quotation_No, wd.QuotationDetailID, wd.Container,wd.Nama_Satuan,ih.Ditujukan, " & _
                           "ih.Total as TotalHarga,mh.NamaHarga,qd.Nama_Barang,qd.Harga,id.Paid,SUM(mbd.Quantity) as quantity,ih.Total as totalInvoice, " & _
                           "mc.Jenis_Perusahaan + ' ' + mc.Nama_Customer as Nama_Customer,mq.Penerima,mk.Nama_Kapal,ih.DaerahDitujukan as DaerahDitujukan, mc.KotaDitunjukan as kotaaslbrg, mc.Area,mq.Tujuan,mbd.Tanggal,mbd.Mb_No,ih.TotalAsuransi,ih.Keterangan,ih.NamaBarang," & _
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
                           "and mbd.status<>0 and mc.status = 1 and mk.status = 1 and ih.No_Invoice ='" & blid.ToString & "'  " & _
                           "GROUP BY wd.WarehouseItem_Code,ih.TotalAsuransi,ih.No_Invoice,wd.QuotationDetailID, wd.Container, wd.Nama_Satuan ,mh.NamaHarga,qd.Nama_Barang,ih.HargaAsuransi, " & _
                            "ID.Paid, mc.Nama_Customer, mq.Penerima, mk.Nama_Kapal, mc.KotaDitunjukan,qd.Quotation_No,qd.Harga, mc.Area, mq.Tujuan, mbd.Tanggal, ih.Ditujukan,ih.DaerahDitujukan, mc.Jenis_Perusahaan,ih.Keterangan,mbd.Mb_No,ih.total, ih.NamaBarang"
            Else
                
                sqlstring = "select distinct " & _
                            "WH.WarehouseItem_Code,  " & _
                            "IH.Ditujukan as Ditujukan,ih.No_Invoice,   " & _
                            "qd.Quotation_No, QD.Harga as Total,mq.Tujuan, mq.Penerima,mh.NamaHarga,qd.Nama_Barang,qd.Harga, " & _
                            "id.Hargatotal as TotalHarga, id.Paid, '0' as quantity,   " & _
                            "mc.Jenis_Perusahaan + ' ' + mc.Nama_Customer as Nama_Customer,mk.Nama_Kapal,   " & _
                            "ih.DaerahDitujukan as DaerahDitujukan,MB.Tanggal,'A' as Nama_Satuan,    " & _
                            "ih.TotalAsuransi,MB.Mb_No,ih.Keterangan,ih.Total as totalInvoice,ISNULL(id.TotalUkuran,0) as TotUk,ih.NamaBarang  " & _
                            "FROM InvoiceHeader IH  " & _
                            "JOIN InvoiceDetail ID ON IH.No_Invoice = ID.No_Invoice " & _
                            "JOIN MasterCustomer MC ON MC.Kode_Customer = IH.CodeCust " & _
                            "JOIN Kapal MK on IH.KapalID = MK.IDDetail   " & _
                            "JOIN MuatBarang MB on ID.Mb_No = MB.Mb_No  " & _
                            "JOIN WarehouseHeader WH on MB.WarehouseHeaderID = WH.WarehouseItem_Code  " & _
                            "JOIN MasterQuotation MQ ON WH.Quotation_No = MQ.Quotation_No   " & _
                            "JOIn QuotationDetail QD on (MQ.Quotation_No = QD.Quotation_No AND ID.QuotationDetailID = QD.IDDetail)  " & _
                            "join MasterHargaDefault MH ON QD.SatuanID = MH.ID  " & _
                            "where IH.No_Invoice = '" & blid & "' " & _
                            "AND IH.status <> 0   " & _
                            "AND MB.status <> 0   " & _
                            "AND ID.status <> 0   " & _
                            "AND WH.status <> 0   " & _
                            "and QD.status <> 0   " & _
                            "and MQ.status <> 0   " & _
                            "and MC.status <> 0   " & _
                            "and MH.status <> 0   " & _
                            "and MK.status <> 0   " & _
                            "and ID.QuotationDetailID = QD.IDDetail " & _
                            "and ID.Paid <> 100 "


            End If

            If KetDS.ToUpper = "YES" Then
                sqlstring = "select ih.InvoiceDate,ih.Total as totalInvoice, mc.KotaDitunjukan as daerahasalbrg,mc.Area as DaerahDitujukan, mc.Nama_Customer + ',' + mc.jenis_perusahaan as Ditujukan, K.Nama_Kapal , " & _
                            "IDS.IDDetail, IDS.No_Invoice, IDS.NamaBarang, IDS.Quantity, MSO.Nama_Satuan ,ids.HargaSatuan,   " & _
                            "ih.TotalAsuransi as HargaAsuransi, ih.Keterangan, " & _
                            "MHD.NamaHarga, IDS.TotalUkuran,iDS.HargaID,SatuanID,IDS.Paid,   " & _
                            "CAST(IDS.TotalUkuran as decimal(30,3)) * IDS.HargaSatuan as TotalHarga   " & _
                            "FROM InvoiceHEader IH " & _
                            "JOIN InvoiceDetailDS IDS on IH.No_Invoice = IDS.No_Invoice   " & _
                            "JOIN MasterHargaDefault MHD ON IDS.HargaID = MHD.ID   " & _
                            "JOIN MasterSatuanOther MSO ON IDS.SatuanID = MSO.IDDetail " & _
                            "JOIN MasterCustomer MC ON IH.CodeCust = MC.Kode_Customer  " & _
                            "JOIN Kapal K ON IH.KapalID = K.IDDetail  " & _
                            "WHERE IDS.status <> 0 And MHD.status <> 0 And MSO.status <> 0 " & _
                            "AND IH.status <> 0 and MC.status <> 0 and K.status = 1   " & _
                            "AND IDS.No_Invoice = '" & blid & "'  "
            End If

        End If
        ds = SQLExecuteQuery(sqlstring)
        dt = ds.Tables(0)
        If dt.Rows.Count > 0 Then
            Dim tglKeberangkatan As String = ""
            Dim strsqlstring As String = ""


            If KetDS = "Yes" Then
                tglKeberangkatan = dt.Rows(0).Item("InvoiceDate").ToString
                tanggalPengiriman = CDate(tglKeberangkatan.ToString).ToString("dd") & " " & CekBulan(bulan, 2) & " " & CDate(tglKeberangkatan.ToString).ToString("yyyy")
                noinvoice = dt.Rows(0).Item("No_Invoice").ToString
                location = dt.Rows(0).Item("daerahasalbrg").ToString
                namabarang = ""

                For i As Integer = 0 To dt.Rows.Count - 1

                    With dt.Rows(i)

                        If i = dt.Rows.Count - 1 Or dt.Rows.Count = 1 Then
                            namabarang &= dt.Rows(i).Item("Quantity").ToString & " " & dt.Rows(i).Item("Nama_Satuan") & " " & dt.Rows(i).Item("NamaBarang").ToString
                        Else
                            namabarang &= dt.Rows(i).Item("Quantity").ToString & " " & dt.Rows(i).Item("Nama_Satuan") & " " & dt.Rows(i).Item("NamaBarang").ToString & ", "
                        End If

                        total = CDbl(dt.Rows(i).Item("TotalUkuran").ToString)
                        totalhargaperitem = .Item("TotalHarga")
                        TotalBiaya = dt.Rows(0).Item("totalInvoice").ToString

                        If TxtMinCharge.Text = "" Then
                            TxtMinCharge.Text = 0
                        End If

                        Select Case dt.Rows(i).Item("NamaHarga").ToString.ToUpper
                            Case "KUBIK"
                                If CDbl(TxtMinCharge.Text) > 0 Then

                                    Detail &= "( " & "Min. Charge " & TxtMinCharge.Text & " &nbsp;M<sup>3</sup> X Rp " & (Double.Parse(.Item("HargaSatuan").ToString).ToString("###,##")).Replace(",", ".") & ",- = RP " & (Double.Parse((CDbl(.Item("HargaSatuan")) * CDbl(TxtMinCharge.Text)).ToString).ToString("###,##")).Replace(",", ".") & ",- ),<BR/>"
                                Else
                                    Detail &= "( " & Tmbh3digit(Cek_Data(Format(CDbl(total), "##,###,###,##.000").ToString)) & " &nbsp;M<sup>3</sup> X Rp " & (Double.Parse(.Item("HargaSatuan").ToString).ToString("###,##")).Replace(",", ".") & ",- = RP " & (Double.Parse(totalhargaperitem.ToString).ToString("###,##")).Replace(",", ".") & ",- ),<BR/>"

                                End If

                            Case "CONTAINER"
                                Detail &= "( " & dt.Rows(i).Item("Quantity").ToString & " &nbsp;Container X Rp " & (Double.Parse(.Item("HargaSatuan").ToString).ToString("###,##")).Replace(",", ".") & ",- = RP " & (Double.Parse(totalhargaperitem.ToString).ToString("###,##")).Replace(",", ".") & ",- ),<BR/>"
                            Case "TON"

                                If CDbl(TxtMinCharge.Text) > 0 Then
                                    Detail &= "( " & "Min. Charge " & TxtMinCharge.Text & " &nbsp;Ton X Rp " & (Double.Parse(.Item("HargaSatuan").ToString).ToString("###,##")).Replace(",", ".") & ",- = RP " & (Double.Parse((CDbl(.Item("HargaSatuan")) * CDbl(TxtMinCharge.Text)).ToString).ToString("###,##")).Replace(",", ".") & ",- ),<BR/>"
                                Else
                                    Detail &= "( " & Tmbh3digit(Cek_Data(Format(CDbl(total), "##,###,###,##.000").ToString)) & " &nbsp;Ton X Rp " & (Double.Parse(.Item("HargaSatuan").ToString).ToString("###,##")).Replace(",", ".") & ",- = RP " & (Double.Parse(totalhargaperitem.ToString).ToString("###,##")).Replace(",", ".") & ",- ),<BR/>"

                                End If

                            Case "UNIT"
                                Detail &= "( " & Cek_Data(Format(CDbl(total), "##,###,###,##.000").ToString) & " &nbsp;Unit X Rp " & (Double.Parse(.Item("HargaSatuan").ToString).ToString("###,##")).Replace(",", ".") & ",- = RP " & (Double.Parse(totalhargaperitem.ToString).ToString("###,##")).Replace(",", ".") & ",- ),<BR/>"
                            Case "SATUAN"
                                Detail &= "( " & Cek_Data(Format(CDbl(total), "##,###,###,##.000").ToString) & " &nbsp;Colly X Rp " & (Double.Parse(.Item("HargaSatuan").ToString).ToString("###,##")).Replace(",", ".") & ",- = RP " & (Double.Parse(totalhargaperitem.ToString).ToString("###,##")).Replace(",", ".") & ",- ),<BR/>"
                        End Select


                    End With
                    
                Next
            Else



                strsqlstring = "select distinct mbr.Depart_Date from MuatBarangReport MBR " & _
                                "JOIN MBRDetail MBRD " & _
                                "ON MBR.Mbr_No = MBRD.Mbr_No " & _
                                "WHERE MBRD.Mb_No = '" & dt.Rows(0).Item("Mb_No").ToString & "' "
                tglKeberangkatan = SQLExecuteScalar(strsqlstring)

                If tglKeberangkatan = "" Then
                    tglKeberangkatan = dt.Rows(0).Item("Tanggal").ToString
                End If

                hargaQuo = CDbl(dt.Rows(0).Item("Harga").ToString)

                bulan = CDate(tglKeberangkatan).ToString("MM")

                tanggalPengiriman = CDate(tglKeberangkatan.ToString).ToString("dd") & " " & CekBulan(bulan, 2) & " " & CDate(tglKeberangkatan.ToString).ToString("yyyy")

                location = Lokasi_Barang(dt.Rows(0).Item("WarehouseItem_Code").ToString)

                For i As Integer = 0 To dt.Rows.Count - 1
                    With dt.Rows(i)
                        quantity = CDbl(.Item("Quantity").ToString)
                        If .Item("NamaHarga") = "Container" And .Item("Keterangan") = "" Then
                            If .Item("NamaHarga") = "Container" And CDbl(.Item("Harga")) = hargaQuo Then

                                total = total + CInt(.Item("TotUk"))

                                If dt.Rows(0).Item("Paid").ToString > 0 Then
                                    totalhargaperitem = (CDbl(.Item("Harga")) * total) * (CDbl(dt.Rows(0).Item("Paid")) / 100)
                                Else
                                    totalhargaperitem = (CDbl(.Item("Harga")) * total)
                                End If


                                If i = dt.Rows.Count - 1 Then
                                    TotalBiaya = TotalBiaya + totalhargaperitem

                                    If dt.Rows.Count > 0 Then


                                        If ddltype.SelectedValue = "InvoiceNDP" Then
                                            Detail &= "( " & Cek_Data(Format(CDbl(total), "##,###,###,##.000").ToString) & " &nbsp;Container X Rp " & (Double.Parse(.Item("Harga").ToString).ToString("###,##")).Replace(",", ".") & ",- = RP " & (Double.Parse(totalhargaperitem.ToString).ToString("###,##")).Replace(",", ".") & ",- ),<BR/>"
                                        Else
                                            Detail &= "( " & Cek_Data(Format(CDbl(total), "##,###,###,##.000").ToString) & " &nbsp;Container X Rp " & (Double.Parse(.Item("Harga").ToString).ToString("###,##")).Replace(",", ".") & ",- X " & .Item("Paid").ToString & " %  = RP " & (Double.Parse(totalhargaperitem.ToString).ToString("###,##")).Replace(",", ".") & ",- ),<BR/>"
                                        End If

                                    End If

                                    satuan = .Item("Nama_Satuan").ToString
                                    total = 0
                                ElseIf dt.Rows(i + 1).Item("NamaHarga") <> "Container" Then
                                    TotalBiaya = TotalBiaya + totalhargaperitem
                                    If (dt.Rows.Count > 0 And quantity > 1) Or quantity = 1 Then


                                        If ddltype.SelectedValue = "InvoiceNDP" Then
                                            Detail &= "( " & Cek_Data(Format(CDbl(total), "##,###,###,##.000").ToString) & " &nbsp;Container X Rp " & (Double.Parse(.Item("Harga").ToString).ToString("###,##")).Replace(",", ".") & ",- = RP " & (Double.Parse(totalhargaperitem.ToString).ToString("###,##")).Replace(",", ".") & ",- ),<BR/>"
                                        Else
                                            Detail &= "( " & Cek_Data(Format(CDbl(total), "##,###,###,##.000").ToString) & " &nbsp;Container X Rp " & (Double.Parse(.Item("Harga").ToString).ToString("###,##")).Replace(",", ".") & ",- X " & .Item("Paid").ToString & " %  = RP " & (Double.Parse(totalhargaperitem.ToString).ToString("###,##")).Replace(",", ".") & ",- ),<BR/>"
                                        End If
                                    End If
                                    satuan = .Item("Nama_Satuan").ToString
                                    total = 0
                                ElseIf dt.Rows(i + 1).Item("NamaHarga") = "Container" And CDbl(dt.Rows(i + 1).Item("Harga")) = hargaQuo Then

                                    hargaQuo = CDbl(dt.Rows(i + 1).Item("Harga"))
                                ElseIf dt.Rows(i + 1).Item("NamaHarga") = "Container" And CDbl(dt.Rows(i + 1).Item("Harga")) <> hargaQuo Then
                                    TotalBiaya = TotalBiaya + totalhargaperitem

                                    If i = dt.Rows.Count - 1 Then

                                    Else
                                        hargaQuo = CDbl(dt.Rows(i + 1).Item("Harga"))
                                    End If

                                    If ddltype.SelectedValue = "InvoiceNDP" Then
                                        Detail &= "( " & Cek_Data(Format(CDbl(total), "##,###,###,##.000").ToString) & " &nbsp;Container X Rp " & (Double.Parse(.Item("Harga").ToString).ToString("###,##")).Replace(",", ".") & ",- = RP " & (Double.Parse(totalhargaperitem.ToString).ToString("###,##")).Replace(",", ".") & ",- ),<BR/>"
                                    Else
                                        Detail &= "( " & Cek_Data(Format(CDbl(total), "##,###,###,##.000").ToString) & " &nbsp;Container X Rp " & (Double.Parse(.Item("Harga").ToString).ToString("###,##")).Replace(",", ".") & ",- X " & .Item("Paid").ToString & " %  = RP " & (Double.Parse(totalhargaperitem.ToString).ToString("###,##")).Replace(",", ".") & ",- ),<BR/>"
                                    End If

                                    satuan = .Item("Nama_Satuan").ToString
                                    total = 0
                                End If

                            Else
                                Detail &= "( " & Cek_Data(Format(CDbl(total), "##,###,###,##.000").ToString) & " &nbsp;Container X Rp " & (Double.Parse(.Item("Harga").ToString).ToString("###,##")).Replace(",", ".") & ",- X " & .Item("Paid").ToString & " %  = RP " & (Double.Parse(.Item("TotalHarga").ToString).ToString("###,##")).Replace(",", ".") & ",- ),<BR/>"
                                satuan = .Item("Nama_Satuan").ToString

                            End If

                            'Detail &= "( " & .Item("Total").ToString & " Container X Rp " & (Double.Parse(CekNilai(.Item("Harga").ToString)).ToString("###,##").ToString).Replace(",", ".") & ",- X " & .Item("Paid").ToString & " %  = " & (Double.Parse(CekNilai(.Item("TotalHarga").ToString)).ToString("###,##")).Replace(",", ".") & " ),<BR/>"
                            If Chkberatcontainer.Checked = True Then
                                satuan = "Kg"
                                cekstring = "select totalberat from ContainerHeader where QuotationNo = '" & .Item("Quotation_No").ToString & "' " & _
                                                    " and QuotationDetailID = '" & .Item("QuotationDetailID").ToString & "' and status = 1 and [type] = 'Container' "
                                cekdt = SQLExecuteQuery(cekstring).Tables(0)
                                quantity = CDbl(cekdt.Rows(0).Item("totalberat").ToString)
                            Else
                                satuan = "Container"
                            End If
                        ElseIf .Item("NamaHarga") = "kubikasi" And .Item("Keterangan") = "" Then
                            Detail &= "( " & .Item("Total").ToString & " M3 X Rp " & (Integer.Parse(.Item("Harga").ToString).ToString("###,##.00")) & ",- X " & .Item("Paid").ToString & " %  = " & (Double.Parse(.Item("TotalHarga").ToString).ToString("###,##.00")) & " ),<BR/>"
                            satuan = "Colly"
                        Else
                            If (.Item("NamaHarga") = "Kubik" Or .Item("Nama_Satuan") = "kubik") And .Item("Keterangan") = "" Then
                                If (.Item("NamaHarga") = "Kubik" Or .Item("Nama_Satuan") = "kubik") And CDbl(.Item("Harga")) = hargaQuo Then
                                    

                                    total = total + CDbl(.Item("TotUk"))

                                    'cek untuk pelunasan ato ga
                                    If dt.Rows(0).Item("Paid").ToString > 0 Then
                                        totalhargaperitem = (CDbl(.Item("Harga")) * total) * (CDbl(dt.Rows(0).Item("Paid")) / 100)
                                    Else
                                        totalhargaperitem = (CDbl(.Item("Harga")) * total)
                                    End If

                                    If i = dt.Rows.Count - 1 Then

                                    Else
                                        hargaQuo = CDbl(dt.Rows(i + 1).Item("Harga"))
                                    End If
                                    
                                    If i = 0 Then
                                        TotalBiaya = TotalBiaya + totalhargaperitem
                                        If ddltype.SelectedValue = "InvoiceNDP" Then
                                            Detail &= "( " & Tmbh3digit(Cek_Data(Format(CDbl(total), "##,###,###,##.000").ToString)) & " &nbsp;M<sup>3</sup> X Rp " & (Double.Parse(.Item("Harga").ToString).ToString("###,##")).Replace(",", ".") & ",- = RP " & (Double.Parse(totalhargaperitem.ToString).ToString("###,##")).Replace(",", ".") & ",- ),<BR/>"
                                        Else
                                            Detail &= "( " & Tmbh3digit(Cek_Data(Format(CDbl(total), "##,###,###,##.000").ToString)) & " &nbsp;M<sup>3</sup> X Rp " & (Double.Parse(.Item("Harga").ToString).ToString("###,##")).Replace(",", ".") & ",- X " & .Item("Paid").ToString & " %  = RP " & (Double.Parse(totalhargaperitem.ToString).ToString("###,##")).Replace(",", ".") & ",- ),<BR/>"
                                        End If

                                        satuan = .Item("Nama_Satuan").ToString
                                        total = 0

                                    ElseIf i = dt.Rows.Count - 1 Then
                                        TotalBiaya = TotalBiaya + totalhargaperitem
                                        If ddltype.SelectedValue = "InvoiceNDP" Then
                                            Detail &= "( " & Tmbh3digit(Cek_Data(Format(CDbl(total), "##,###,###,##.000").ToString)) & " &nbsp;M<sup>3</sup> X Rp " & (Double.Parse(.Item("Harga").ToString).ToString("###,##")).Replace(",", ".") & ",- = RP " & (Double.Parse(totalhargaperitem.ToString).ToString("###,##")).Replace(",", ".") & ",- ),<BR/>"
                                        Else
                                            totalhargaperitem = (.Item("Paid") / 100) * totalhargaperitem
                                            Detail &= "( " & Tmbh3digit(Cek_Data(Format(CDbl(total), "##,###,###,##.000").ToString)) & " &nbsp;M<sup>3</sup> X Rp " & (Double.Parse(.Item("Harga").ToString).ToString("###,##")).Replace(",", ".") & ",- X " & .Item("Paid").ToString & " %  = RP " & (Double.Parse(totalhargaperitem.ToString).ToString("###,##")).Replace(",", ".") & ",- ),<BR/>"
                                        End If

                                        satuan = .Item("Nama_Satuan").ToString
                                        total = 0
                                    ElseIf dt.Rows(i + 1).Item("NamaHarga") <> "Kubik" Or CDbl(dt.Rows(i).Item("Harga")) <> hargaQuo Then
                                        TotalBiaya = TotalBiaya + totalhargaperitem
                                        If ddltype.SelectedValue = "InvoiceNDP" Then
                                            Detail &= "( " & Tmbh3digit(Cek_Data(Format(CDbl(total), "##,###,###,##.000").ToString)) & " &nbsp;M<sup>3</sup>X Rp " & (Double.Parse(.Item("Harga").ToString).ToString("###,##")).Replace(",", ".") & ",- = RP " & (Double.Parse(totalhargaperitem.ToString).ToString("###,##")).Replace(",", ".") & ",- ),<BR/>"
                                        Else
                                            Detail &= "( " & Tmbh3digit(Cek_Data(Format(CDbl(total), "##,###,###,##.000").ToString)) & " &nbsp;M<sup>3</sup>X Rp " & (Double.Parse(.Item("Harga").ToString).ToString("###,##")).Replace(",", ".") & ",- X " & .Item("Paid").ToString & " %  = RP " & (Double.Parse(totalhargaperitem.ToString).ToString("###,##")).Replace(",", ".") & ",- ),<BR/>"
                                        End If

                                        satuan = .Item("Nama_Satuan").ToString
                                        total = 0
                                    ElseIf dt.Rows(i + 1).Item("NamaHarga") = "Kubik" Or CDbl(dt.Rows(i).Item("Harga")) = hargaQuo Then
                                        'total = total + CDbl(.Item("Total"))
                                        hargaQuo = CDbl(dt.Rows(i + 1).Item("Harga"))
                                        satuan = .Item("Nama_Satuan").ToString
                                    End If

                                    

                                End If


                            ElseIf (.Item("NamaHarga") = "Ton" Or .Item("Nama_Satuan") = "ton" Or .Item("Nama_Satuan") = "Berat" Or .Item("Nama_Satuan") = "berat") And .Item("Keterangan") = "" Then
                                If (.Item("NamaHarga") = "Ton" Or .Item("Nama_Satuan") = "ton") And CDbl(.Item("Harga")) = hargaQuo Then


                                    'diubah disini buat tampilin nilai total - (Edwin)
                                    total = total + CDbl(System.Math.Round(.Item("TotUk") * 1000 / 1000, 3))
                                    'System.Math.Round(total, 3)


                                    If dt.Rows(0).Item("Paid").ToString > 0 Then
                                        totalhargaperitem = (CDbl(.Item("Harga")) * total) * (CDbl(dt.Rows(0).Item("Paid")) / 100)
                                    Else
                                        totalhargaperitem = (CDbl(.Item("Harga")) * total)
                                    End If

                                    If i = dt.Rows.Count - 1 Then

                                    Else
                                        hargaQuo = CDbl(dt.Rows(i + 1).Item("Harga"))
                                    End If

                                    If i = 0 Then
                                        TotalBiaya = TotalBiaya + totalhargaperitem
                                        If ddltype.SelectedValue = "InvoiceNDP" Then

                                            Detail &= "( " & Tmbh3digit(Cek_Data(Format(CDbl(total), "##,###,###,##.000").ToString)) & " &nbsp;Ton X Rp " & (Double.Parse(.Item("Harga").ToString).ToString("###,##")).Replace(",", ".") & ",- = RP " & (Double.Parse(totalhargaperitem.ToString).ToString("###,##")).Replace(",", ".") & ",- ),<BR/>"
                                        Else
                                            Detail &= "( " & Tmbh3digit(Cek_Data(Format(CDbl(total), "##,###,###,##.000").ToString)) & " &nbsp;Ton X Rp " & (Double.Parse(.Item("Harga").ToString).ToString("###,##")).Replace(",", ".") & ",- X " & .Item("Paid").ToString & " %  = RP " & (Double.Parse(totalhargaperitem.ToString).ToString("###,##")).Replace(",", ".") & ",- ),<BR/>"
                                        End If

                                        satuan = .Item("Nama_Satuan").ToString
                                        total = 0

                                    ElseIf i = dt.Rows.Count - 1 Then
                                        TotalBiaya = TotalBiaya + totalhargaperitem
                                        If ddltype.SelectedValue = "InvoiceNDP" Then

                                            Detail &= "( " & Tmbh3digit(Cek_Data(Format(CDbl(total), "##,###,###,##.000").ToString)) & " &nbsp;Ton X Rp " & (Double.Parse(.Item("Harga").ToString).ToString("###,##")).Replace(",", ".") & ",- = RP " & (Double.Parse(totalhargaperitem.ToString).ToString("###,##")).Replace(",", ".") & ",- ),<BR/>"
                                        Else
                                            Detail &= "( " & Tmbh3digit(Cek_Data(Format(CDbl(total), "##,###,###,##.000").ToString)) & " &nbsp;Ton X Rp " & (Double.Parse(.Item("Harga").ToString).ToString("###,##")).Replace(",", ".") & ",- X " & .Item("Paid").ToString & " %  = RP " & (Double.Parse(totalhargaperitem.ToString).ToString("###,##")).Replace(",", ".") & ",- ),<BR/>"
                                        End If

                                        satuan = .Item("Nama_Satuan").ToString
                                        total = 0
                                    ElseIf dt.Rows(i + 1).Item("NamaHarga") <> "Ton" Or CDbl(dt.Rows(i).Item("Harga")) <> hargaQuo Then
                                        TotalBiaya = TotalBiaya + totalhargaperitem

                                        If ddltype.SelectedValue = "InvoiceNDP" Then
                                            Detail &= "( " & Tmbh3digit(Cek_Data(Format(CDbl(total), "##,###,###,##.000").ToString)) & " &nbsp;Ton X Rp " & (Double.Parse(.Item("Harga").ToString).ToString("###,##")).Replace(",", ".") & ",- = RP " & (Double.Parse(totalhargaperitem.ToString).ToString("###,##")).Replace(",", ".") & ",- ),<BR/>"
                                        Else
                                            Detail &= "( " & Tmbh3digit(Cek_Data(Format(CDbl(total), "##,###,###,##.000").ToString)) & " &nbsp;Ton X Rp " & (Double.Parse(.Item("Harga").ToString).ToString("###,##")).Replace(",", ".") & ",- X " & .Item("Paid").ToString & " %  = RP " & (Double.Parse(totalhargaperitem.ToString).ToString("###,##")).Replace(",", ".") & ",- ),<BR/>"
                                        End If

                                        satuan = .Item("Nama_Satuan").ToString
                                        total = 0
                                    ElseIf dt.Rows(i + 1).Item("NamaHarga") = "Ton" Or CDbl(dt.Rows(i).Item("Harga")) = hargaQuo Then
                                        'total = total + CDbl(.Item("Total"))
                                        hargaQuo = CDbl(dt.Rows(i + 1).Item("Harga"))
                                        satuan = .Item("Nama_Satuan").ToString
                                    End If

                                End If

                                'Detail &= "( " & Cek_Data(Format(CDbl(.Item("Total")), "##,###,###,##.000").ToString) & " Ton X Rp " & (Double.Parse(CekNilai(.Item("Harga")).ToString).ToString("###,##")).Replace(",", ".") & ",- X " & .Item("Paid").ToString & " %  = " & (Double.Parse(CekNilai(.Item("TotalHarga")).ToString).ToString("###,##")).Replace(",", ".") & ",- ),<BR/>"
                                'satuan = .Item("Nama_Satuan").ToString

                            ElseIf (.Item("NamaHarga") = "Unit" Or .Item("Nama_Satuan") = "unit") And .Item("Keterangan") = "" Then
                                If (.Item("NamaHarga") = "Unit" Or .Item("Nama_Satuan") = "unit") And CDbl(.Item("Harga")) = hargaQuo Then

                                    total = total + CInt(.Item("TotUk"))

                                    If dt.Rows(0).Item("Paid").ToString > 0 Then
                                        totalhargaperitem = (CDbl(.Item("Harga")) * total) * (CDbl(dt.Rows(0).Item("Paid")) / 100)
                                    Else
                                        totalhargaperitem = (CDbl(.Item("Harga")) * total)
                                    End If

                                    If i = dt.Rows.Count - 1 Then

                                    Else
                                        hargaQuo = CDbl(dt.Rows(i + 1).Item("Harga"))
                                    End If


                                    If i = dt.Rows.Count - 1 Then
                                        TotalBiaya = TotalBiaya + totalhargaperitem

                                        If ddltype.SelectedValue = "InvoiceNDP" Then
                                            Detail &= "( " & Cek_Data(Format(CDbl(total), "##,###,###,##.000").ToString) & "&nbsp;Unit X Rp " & (Double.Parse(.Item("Harga").ToString).ToString("###,##")).Replace(",", ".") & ",- = " & (Double.Parse(totalhargaperitem.ToString).ToString("###,##")).Replace(",", ".") & ",- ),<BR/>"
                                        Else
                                            Detail &= "( " & Cek_Data(Format(CDbl(total), "##,###,###,##.000").ToString) & "&nbsp;Unit X Rp " & (Double.Parse(.Item("Harga").ToString).ToString("###,##")).Replace(",", ".") & ",- X " & .Item("Paid").ToString & " %  = " & (Double.Parse(totalhargaperitem.ToString).ToString("###,##")).Replace(",", ".") & ",- ),<BR/>"
                                        End If


                                        satuan = "Unit"
                                        total = 0
                                    ElseIf dt.Rows(i + 1).Item("NamaHarga") <> "Unit" Then
                                        TotalBiaya = TotalBiaya + totalhargaperitem

                                        If ddltype.SelectedValue = "InvoiceNDP" Then
                                            Detail &= "( " & Cek_Data(Format(CDbl(total), "##,###,###,##.000").ToString) & "&nbsp;Unit X Rp " & (Double.Parse(.Item("Harga").ToString).ToString("###,##")).Replace(",", ".") & ",- = RP " & (Double.Parse(totalhargaperitem.ToString).ToString("###,##")).Replace(",", ".") & ",- ),<BR/>"
                                        Else
                                            Detail &= "( " & Cek_Data(Format(CDbl(total), "##,###,###,##.000").ToString) & "&nbsp;Unit X Rp " & (Double.Parse(.Item("Harga").ToString).ToString("###,##")).Replace(",", ".") & ",- X " & .Item("Paid").ToString & " %  = RP " & (Double.Parse(totalhargaperitem.ToString).ToString("###,##")).Replace(",", ".") & ",- ),<BR/>"
                                        End If

                                        satuan = "Unit"
                                        total = 0
                                    ElseIf dt.Rows(i + 1).Item("NamaHarga") = "Unit" Then
                                        'total = total + CDbl(.Item("Total"))
                                        hargaQuo = CDbl(dt.Rows(i + 1).Item("Harga"))
                                        satuan = "Unit"
                                    End If

                                End If

                                'Detail &= "( " & .Item("Total").ToString & " Unit X Rp " & (Double.Parse(.Item("Harga").ToString).ToString("###,##")).Replace(",", ".") & ",- X " & .Item("Paid").ToString & " %  = " & (Double.Parse(.Item("TotalHarga").ToString).ToString("###,##")).Replace(",", ".") & ",- ),<BR/>"
                                'satuan = "Unit"
                            ElseIf (.Item("NamaHarga") = "Kubik" Or .Item("Nama_Satuan") = "kubik") And .Item("Keterangan").ToString.Contains("PembayaranMinimum") Then
                                If CDbl(TxtMinCharge.Text) > 0 Then


                                    If ddltype.SelectedValue = "InvoiceNDP" Then
                                        Detail &= "( " & "Min. Charge " & TxtMinCharge.Text & " &nbsp;M<sup>3</sup> X Rp " & (Double.Parse(.Item("Harga").ToString).ToString("###,##")).Replace(",", ".") & ",- = RP " & (Double.Parse(.Item("TotalHarga").ToString).ToString("###,##")).Replace(",", ".") & ",- ),<BR/>"
                                    Else
                                        Detail &= "( " & "Min. Charge " & TxtMinCharge.Text & " &nbsp;M<sup>3</sup> X Rp " & (Double.Parse(.Item("Harga").ToString).ToString("###,##")).Replace(",", ".") & ",- X " & .Item("Paid").ToString & " %  = RP " & (Double.Parse(.Item("totalInvoice").ToString).ToString("###,##")).Replace(",", ".") & ",- ),<BR/>"
                                    End If
                                End If

                                TotalBiaya = .Item("TotalHarga").ToString
                                satuan = .Item("Nama_Satuan").ToString
                            ElseIf (.Item("NamaHarga") = "Ton" Or .Item("Nama_Satuan") = "ton" Or .Item("Nama_Satuan") = "Berat" Or .Item("Nama_Satuan") = "berat") And .Item("Keterangan").ToString.Contains("PembayaranMinimum") Then
                                If CDbl(TxtMinCharge.Text) > 0 Then
                                    If ddltype.SelectedValue = "InvoiceNDP" Then
                                        Detail &= "( " & "Min. Charge " & TxtMinCharge.Text & " Ton X Rp " & (Double.Parse(CekNilai(.Item("Harga")).ToString).ToString("###,##")).Replace(",", ".") & ",- = RP " & (Double.Parse(CekNilai(.Item("TotalHarga")).ToString).ToString("###,##")).Replace(",", ".") & ",- ),<BR/>"
                                    Else
                                        Detail &= "( " & "Min. Charge " & TxtMinCharge.Text & " Ton X Rp " & (Double.Parse(CekNilai(.Item("Harga")).ToString).ToString("###,##")).Replace(",", ".") & ",- X " & .Item("Paid").ToString & " %  = RP " & (Double.Parse(CekNilai(.Item("totalInvoice")).ToString).ToString("###,##")).Replace(",", ".") & ",- ),<BR/>"
                                    End If
                                End If

                                TotalBiaya = .Item("TotalHarga").ToString
                                satuan = .Item("Nama_Satuan").ToString
                            ElseIf (.Item("NamaHarga") = "Unit" Or .Item("Nama_Satuan") = "unit") And .Item("Keterangan").ToString.Contains("PembayaranMinimum") Then
                                If CDbl(TxtMinCharge.Text) > 0 Then
                                    If ddltype.SelectedValue = "InvoiceNDP" Then
                                        Detail &= "( " & "Min. Charge " & TxtMinCharge.Text & " Unit X Rp " & (Double.Parse(.Item("Harga").ToString).ToString("###,##")).Replace(",", ".") & ",- = RP " & (Double.Parse(.Item("TotalHarga").ToString).ToString("###,##")).Replace(",", ".") & ",- ),<BR/>"
                                    Else
                                        Detail &= "( " & "Min. Charge " & TxtMinCharge.Text & " Unit X Rp " & (Double.Parse(.Item("Harga").ToString).ToString("###,##")).Replace(",", ".") & ",- X " & .Item("Paid").ToString & " %  = RP " & (Double.Parse(.Item("totalInvoice").ToString).ToString("###,##")).Replace(",", ".") & ",- ),<BR/>"
                                    End If
                                End If
                                TotalBiaya = .Item("TotalHarga").ToString
                                satuan = "Unit"
                            Else
                                If i = dt.Rows.Count - 1 Then

                                Else
                                    hargaQuo = CDbl(dt.Rows(i + 1).Item("Harga"))
                                End If

                                total = total + CDbl(.Item("TotUk"))

                                If dt.Rows(0).Item("Paid").ToString > 0 Then
                                    totalhargaperitem = (CDbl(.Item("Harga")) * total) * (CDbl(dt.Rows(0).Item("Paid")) / 100)
                                Else
                                    totalhargaperitem = (CDbl(.Item("Harga")) * total)
                                End If

                                Detail &= "( " & Cek_Data(Format(CDbl(total), "##,###,###,##.000").ToString) & " Colly X Rp " & (Double.Parse(.Item("Harga").ToString).ToString("###,##")).Replace(",", ".") & ",- = RP " & (Double.Parse(.Item("TotalHarga").ToString).ToString("###,##")).Replace(",", ".") & ",- ),<BR/>"
                                satuan = "Colly"
                                total = 0
                                TotalBiaya = .Item("TotalHarga").ToString
                            End If

                        End If
                        'If i < dt.Rows.Count - 1 Then
                        '    namabarang &= quantity.ToString + " " + satuan.ToString + " " + .Item("Nama_Barang").ToString + ","
                        'ElseIf i = dt.Rows.Count - 1 Then
                        '    namabarang &= quantity.ToString + " " + satuan.ToString + " " + .Item("Nama_Barang").ToString
                        'Else
                        '    namabarang &= quantity.ToString + " " + satuan.ToString + " " + .Item("Nama_Barang").ToString + ","
                        'End If

                        If i = dt.Rows.Count - 1 Then
                            namabarang = .Item("NamaBarang").ToString
                        End If


                        HargaSementara = CDbl(.Item("TotalHarga").ToString)
                        HargaTotal = HargaTotal + HargaSementara

                        TotalBiaya = dt.Rows(0).Item("totalInvoice").ToString

                        If Ket = "PembayaranMinimum" Then
                            namabarang = .Item("NamaBarang").ToString
                            Exit For
                        End If

                    End With
                Next
            End If
        End If

        If ChkTotalGabung.Checked = True Then
            TotalBiaya = HargaTotal + CDbl(dt.Rows(0).Item("TotalAsuransi").ToString)
        End If

        ejaanharga = Bilangan2(TotalBiaya.ToString("###,###,###,###,###"))

        If OngkosAngkutan <> "" Then
            Dim split() As String
            split = OngkosAngkutan.ToString.Split("-")
            'TotalBiaya = TotalBiaya + CDbl(split(1).ToString.Replace(".", ""))

            Detail &= "( " & split(0) & " = Rp " & split(1) & " ,- " & " )"
        End If

        If KetDS = "Yes" Then
            HeaderReport = "<Table width=772px border=0 ><tr style=""height:97px""><td></td></tr></table> " & _
                       "<table border=0 width=772px bgcolor=white cellpadding=0 cellspacing=0 style=""font-family:verdana;font-size:12px;position:relative"" >" & _
                       " <tr>" & _
                       "    <td valign=""center"" align=right style=""padding-right:30px"" height=""28px"">" & _
                       "        <b><font size=""2""> " & noinvoice.ToString & "</font></b> " & _
                       "    </td>" & _
                       "    <td valign=""top"" align=right style=""width:40px;"" height=""35px"" >" & _
                       "    </td>" & _
                       " </tr>" & _
                       "</table>"
        Else
            HeaderReport = "<Table width=772px border=0 ><tr style=""height:97px""><td></td></tr></table> " & _
                       "<table width=772px bgcolor=white border=0 cellpadding=0 cellspacing=0 style=""font-family:verdana;font-size:12px;position:relative"" >" & _
                       " <tr>" & _
                       "    <td valign=""center"" align=right style=""padding-right:30px""  height=""28px"" >" & _
                       "        <b><font size=""2""> " & noinvoice.ToString & "</font></b> " & _
                       "    </td>" & _
                       "    <td valign=""top"" align=right style=""width:43px;"" height=""35px"" >" & _
                       "    </td>" & _
                       " </tr>" & _
                       "</table>"
        End If

        

        HeaderReport &= "<table width=772px bgcolor=white cellpadding=5 cellspacing=0 style=""font-family:verdana;font-size:12px;position:relative"" >" & _
                       " <tr style=""height25px"">" & _
                       "    <td align=left  style=""width:170px;"" >" & _
                       "       &nbsp;" & _
                       "    </td>" & _
                       "    <td align=left  style=""width:273px;"">" & _
                       "<b> " & TukarNamaPerusahaan(dt.Rows(0).Item("Ditujukan").ToString) & _
                       "    </b></td>" & _
                       "    <td align=right>" & _
                       "        Di :" & _
                       "    </td>" & _
                       "    <td align=left style=""padding-left:14px;"">" & _
                       " " & dt.Rows(0).Item("DaerahDitujukan").ToString & _
                       "    </td>" & _
                       " </tr>" & _
                       "</table>"
        If ChkViewRekening.Checked = True Then
            TinggiIsiInvoice = 177
        Else
            TinggiIsiInvoice = 235
        End If

        HeaderReport &= "<table width=772px bgcolor=white cellpadding=5 cellspacing=0 style=""height:" & TinggiIsiInvoice & "px;font-family:verdana;font-size:12px;position:relative"" >" & _
                       " <tr>" & _
                       "    <td align=left style=""width:170px;"">" & _
                       "        &nbsp; " & _
                       "    </td>" & _
                       "    <td align=left valign=""Top""><br/> " & _
                       " #" & ejaanharga & " RUPIAH# " & _
                       "    </td>" & _
                       " </tr>" & _
                       "<tr><td></td></tr> " & _
                       " <tr >" & _
                       "    <td align=left valign=""top"" >" & _
                       "        &nbsp;" & _
                       "    </td>"
        If ChkViewRekening.Checked = True Then
            BR = "<BR />"
        End If

        If CDbl(dt.Rows(0).Item("Paid").ToString) > 0 Then
            If ddltype.SelectedValue = "InvoiceDP" Then
                HeaderReport &= "    <td colspan=""2"" align=left valign=""top"">" & _
                               "" & BR & " Uang Muka " & dt.Rows(0).Item("Paid").ToString & "% atas pengiriman " & namabarang.ToString & " dari " & location.ToString & " ke " & TxtKotaTujuan.Text.ToString & " dengan " & dt.Rows(0).Item("Nama_Kapal").ToString & " tgl. " & tanggalPengiriman & ". <BR/>" & _
                               "" & Detail.ToString & _
                               "    </td>" & _
                               "    <td align=left valign=""top"" style=""width:43px;"">" & _
                               "    </td>" & _
                               " </tr>"
            Else
                HeaderReport &= "    <td colspan=""2"" align=left valign=""top"">" & _
                               "" & BR & " Pelunasan " & dt.Rows(0).Item("Paid").ToString & "% atas pengiriman " & namabarang.ToString & " dari " & dt.Rows(0).Item("DaerahDitujukan").ToString & " ke " & TxtKotaTujuan.Text.ToString & " dengan KM. " & dt.Rows(0).Item("Nama_Kapal").ToString & " tgl. " & tanggalPengiriman & ". <BR/>" & _
                               "" & Detail.ToString & _
                               "    </td>" & _
                               "    <td align=left valign=""top"" style=""width:43px;"">" & _
                               "    </td>" & _
                               " </tr>"
            End If
        Else

            HeaderReport &= "    <td colspan=""2"" align=left valign=""top"">" & _
                               "" & BR & " Freight " & dt.Rows(0).Item("Nama_Kapal").ToString & " atas pengiriman " & namabarang.ToString & " dari " & location & " ke " & TxtKotaTujuan.Text.ToString & " tanggal " & tanggalPengiriman & ". <BR/>" & _
                               "" & Detail.ToString & _
                               "    </td>" & _
                               "    <td align=left valign=""top"" style=""width:43px;"">" & _
                               "    </td>" & _
                               " </tr>"
        End If

        If dt.Rows.Count > 0 Then
            '    HeaderReport &= " <tr >" & _
            '                    "    <td align=left valign=""top"" style="" border-left:1px black solid;border-bottom:1px black solid;"">" & _
            '                    "        &nbsp;" & _
            '                    "    </td>" & _
            '                    "    <td align=left valign=""top"" style="" border-bottom:1px black solid;"">" & _
            '                    "        &nbsp; " & _
            '                    "    </td>" & _
            '                    "    <td align=left valign=""top"" style=""  border-right:1px black solid;border-bottom:1px black solid;"">" & _
            '                    "       <u>Total Biaya  " & TotalBiaya.ToString("###,##").Replace(",", ".") & "</u>" & _
            '                    "    </td>" & _
            '                    " </tr>" & _
            '                    "</table>"
            'Else

            HeaderReport &= " <tr >" & _
                                        "    <td align=left valign=""top"" >" & _
                                        "        &nbsp;" & _
                                        "    </td>" & _
                                        "    <td align=left valign=""top"" >" & _
                                        "        &nbsp; " & _
                                        "    </td>" & _
                                        "    <td align=center valign=""top"" >" & _
                                        "       &nbsp;" & _
                                        "    </td>" & _
                                        " </tr>" & _
                                        "</table>"


        End If

        If ChkViewRekening.Checked = True Then
            HeaderReport &= "<table width=772px bgcolor=white cellpadding=0 cellspacing=0 style=""font-family:verdana;font-size:12px;position:relative"" >" & _
                 "<tr>" & _
                 "<td rowspan = ""3"" style=""width = 20px;"">&nbsp;&nbsp;&nbsp;<br/><br/></td> " & _
                 "   <td style=""border:solid 1px White; width :450px""> " & _
                 "      <table>" & _
                 "          <tr> " & _
                 "              <td style="" width :80px;"">Bank</td><td>:</td> " & _
                 "              <td>" & TxtNamaBank.Text.Trim.Replace("'", "''") & "</td> " & _
                 "          </tr> " & _
                 "          <tr> " & _
                 "              <td style="" width :80px;"">Atas Nama</td><td>:</td> " & _
                 "              <td>" & txtAtasNama.Text.Trim.Replace("'", "''") & "</td> " & _
                 "          </tr> " & _
                 "          <tr> " & _
                 "              <td style="" width :80px;"">No Rekening</td><td>:</td> " & _
                 "              <td>" & TxtRekening.Text.ToString & "</td> " & _
                 "          </tr> " & _
                 "      </table> " & _
                 "    </td></tr> " & _
                 "<tr><td style=""Height:30px;"">&nbsp;<br/><br/></td> " & _
                 "   <td valign=""center"" align = ""left"" style=""width = 50%;padding-left:10px;""""><br/> " & _
                 "      " & Tanggal & _
                 "   </td>" & _
                 "</tr> " & _
                 "<tr><td align =""left"" style=""padding-left:120px;""> " & _
                 "       <font size=""4""><b>  " & TotalBiaya.ToString("###,##").ToString.Replace(",", ".") & ",- " & _
                 "   </font></b></td> " & _
                 "   <td style=""width = 25%;""> " & _
                 "   &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp</td> " & _
                 "</tr>" & _
                 "</table>"
        Else
            HeaderReport &= "<table width=772px border=0 bgcolor=white cellpadding=0 cellspacing=0 style=""font-family:verdana;font-size:12px;position:relative"" >" & _
                 "<tr>" & _
                 "   <td style=""width :250px;padding-left:125px;""><br /><br /><br /> " & _
                 "       &nbsp; <font size=""4""> <b>  " & TotalBiaya.ToString("###,##").ToString.Replace(",", ".") & ",- " & _
                 "   </font></b></td> " & _
                 "   <td style=""width = 25%;"" >" & _
                 "   &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<br/><br/></td> " & _
                 "   <td valign=""center"" align = ""left"" style=""width = 50%;padding-left:70px;""><br /> " & _
                 "       " & Tanggal & _
                 "   </td>" & _
                 "</tr>" & _
                 "</table>"
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


        If TxtKotaTujuan.Text.Trim = "" Then
            lblError.Visible = True
            lblError.Text = "kota tujuan invoice masih kosong "
            Return False
        End If

        If ChkViewRekening.Checked = True Then
            If TxtRekening.Text.Trim = "" Then
                lblError.Visible = True
                lblError.Text = "Harap isi no rekening apabila anda ingin menampilkannya "
                Return False
            End If
        End If

        Return True
    End Function
#End Region

#Region "METHOD"

    Private Sub Update_status_Print(ByVal NoInvoice As String, ByVal MuatBarang As String)
        Try
            Dim strstring As String = ""
            Dim warehouseCode As String = ""
            Dim zDT As DataTable
            Dim QuoNo As String = ""
            Dim statusQUo As String = ""
            Dim QuoNoCLose As String = ""

            sqlstring = "Update InvoiceHeader Set statusPrint = 0 where No_Invoice = '" & NoInvoice & "';"
            sqlstring &= "Update InvoiceHeader set [status] = 1 where No_Invoice = '" & NoInvoice & "' and [status] = 7;"
            sqlstring &= "Update Invoicedetail set [status] = 1 where No_Invoice = '" & NoInvoice & "' and [status] = 7"

            sqlstring &= "UPDATE MuatBarang Set [status] = 5 Where MB_No = '" & MuatBarang.ToString & "' AND [status] <> 0 ;"
            sqlstring &= "UPDATE MuatBarangDetail Set [status] = 5 where Mb_No = '" & MuatBarang.ToString & "' AND [status] <> 0 ;"

            strstring = "SELECT WarehouseHeaderID FROM MuatBarang Where Mb_No = '" & MuatBarang.ToString & "' AND [status] <> 0"
            warehouseCode = SQLExecuteScalar(strstring)

            strstring = "Select Quotation_No FROM wareHouseHeader Where WarehouseItem_Code = '" & warehouseCode.ToString & "' AND [status] <> 0 "
            QuoNo = SQLExecuteScalar(strstring)

            sqlstring &= "UPDATE WarehouseHeader set [status] = 1 where Warehouseitem_Code = '" & warehouseCode.ToString & "' AND [status] <> 0;"
            sqlstring &= "UPDATE WarehouseDetail set [status] = 1 where warehouseItem_Code = '" & warehouseCode.ToString & "' AND [status] <> 0;"

            strstring = "SELECT Container, Nama_Barang FROM WarehouseDetail WHere WarehouseItem_Code = '" & warehouseCode.ToString & "' AND [status] <> 0"
            zDT = SQLExecuteQuery(strstring).Tables(0)

            If zDT.Rows.Count > 0 Then
                For i As Integer = 0 To zDT.Rows.Count - 1
                    If zDT.Rows(i).Item("Container").ToString.ToUpper = "TRUE" Then
                        sqlstring &= "UPDATE ContainerHeader set [status] = 1 WHERE ContainerCode = '" & zDT.Rows(i).Item("Nama_Barang").ToString & "' AND [stataus] <> 0; "
                    End If
                Next
            End If

            'select quotation No, untuk update quotation
            strstring = "SELECT [status] FROM MasterQuotation WHERE Quotation_No = '" & QuoNo & "' AND [status] <> 0"
            statusQUo = SQLExecuteScalar(strstring)

            If statusQUo = "1" Then
                strstring = "SELECT QuotationClose_No FROM MasterQuotation WHERE Quotation_No = '" & QuoNo & "' AND [status] = 1"
                QuoNoCLose = SQLExecuteScalar(strstring)

                If QuoNoCLose <> "" Then
                    sqlstring &= "UPDATE WarehouseHeader SET Quotation_No = '" & QuoNoCLose & "' WHERE Quotation_No = '" & QuoNo & "' AND [status] <> 0; "
                    sqlstring &= "UPDATE ContainerHeader SET QuotationNo = '" & QuoNoCLose & "' WHERE Quotation_No = '" & QuoNo & "' AND [status] <> 0; "
                    sqlstring &= "UPDATE MasterQuotation Set [status] = 1, [Close] = 'False' where Quotation_No = '" & QuoNoCLose & "' AND [status] = 5;"
                    sqlstring &= "UPDATE QuotationDetail Set [status] = 1 where Quotation_No = '" & QuoNoCLose & "' AND [status] <> 0;"
                    sqlstring &= "Delete from MasterQuotation WHERE Quotation_No = '" & QuoNo & "' AND [status] <> 0;"
                    sqlstring &= "DELETE FROM QuotationDetail WHERE Quotation_No = '" & QuoNo & "' AND [status] <> 0;"

                End If
            ElseIf statusQUo = "2" Then
                sqlstring &= "UPDATE MasterQuotation Set [status] = 1, [Close] = 'False' where Quotation_No = '" & QuoNo & "' AND [status] = 2;"
                sqlstring &= "UPDATE QuotationDetail Set [status] = 1 where Quotation_No = '" & QuoNo & "' AND [status] <> 0;"
            End If

            hasil = SQLExecuteNonQuery(sqlstring)

            If hasil <> 0 Then
                lblError.Text = "Reset Invoice Berhasil, Silakan edit Invoice"
                lblError.Visible = True
            End If
        Catch ex As Exception
            Throw New Exception(ex.ToString)
        End Try
    End Sub

    Private Function Lokasi_Barang(ByVal Warehouseitemcode As String)
        Try
            Dim str As String = ""
            Dim location As String = ""

            str = "select distinct mw.location from MasterWarehouse MW " & _
                "JOIN WarehouseHeader WHD " & _
                "ON WHD.Warehouse_Code = MW.Warehouse_Code " & _
                "WHERE WHD.WarehouseItem_Code = '" & Warehouseitemcode & "' " & _
                "AND MW.[status] = 1 AND WHD.[status] <> 0"
            location = SQLExecuteScalar(str)

            Return location
        Catch ex As Exception
            Throw New Exception(ex.ToString)
        End Try
    End Function

    Private Sub Close(ByVal ID As String, ByVal NoInvoice As String, ByVal IDMuatBarang As String)
        Dim str As String = ""
        Dim strdelete As String = ""
        Dim ddt As DataTable
        Dim dds As DataSet
        Dim dstr As String
        Dim edt As DataTable
        Dim eds As DataSet
        Dim estr As String
        Dim hasil As String
        Dim cekstr As String
        Dim cdt As DataTable
        Dim cds As DataSet
        Dim warehouse_detail_id = ""
        Dim warehouseheaderid = ""
        Dim tempjumlah As Integer = 0
        Dim jumlah As Integer = 0
        Dim QuotationDetailID As String = ""
        Dim QuotationNo As String = ""
        Dim TempQuotationDetailID As String = ""
        Dim DTCEK As New DataTable
        Dim cekb As Boolean = True
        Dim MBNO As String = ""
        Dim muatbrgNo As String = ""

        Try

            cekstr = "SELECT IDDetail FROM MuatBarangDetail WHERE MB_No = '" & IDMuatBarang & "' and status <> 0 "
            cds = SQLExecuteQuery(cekstr)
            cdt = cds.Tables(0)

            If cdt.Rows.Count > 0 Then
                For i As Integer = 0 To cdt.Rows.Count - 1
                    str &= "UPDATE MuatBarangDetail " & _
                             "SET " & _
                             "LastModified = '" & Now.ToString & "' " & _
                             ",[status] = 7 " & _
                             "WHERE IDDetail = " & cdt.Rows(i).Item("IDDetail").ToString & " and Mb_No = '" & IDMuatBarang & "' and status = 5; "

                    dstr = "Select Warehouse_Id,WarehouseHeaderID from V_MuatBarang_Detail where IDDetail = " & cdt.Rows(i).Item("IDDetail").ToString & " and Mb_No = '" & IDMuatBarang.ToString & "' ;"
                    dds = SQLExecuteQuery(dstr)
                    ddt = dds.Tables(0)
                    warehouse_detail_id = ddt.Rows(0).Item("Warehouse_Id").ToString
                    warehouseheaderid = ddt.Rows(0).Item("WarehouseHeaderID").ToString

                    str &= " UPDATE WarehouseDetail " & _
                           "SET " & _
                           "LastModified = '" & Now.ToString & "' " & _
                           ",[status] = 1 " & _
                           "WHERE IDDetail = '" & ddt.Rows(0).Item("Warehouse_Id").ToString & "' and WarehouseItem_Code = '" & ddt.Rows(0).Item("WarehouseHeaderID").ToString & "' and (Quantity = 0 OR Quantity < 0); "

                    estr = "select Container,QuotationDetailID,Nama_Barang,Quotation_No from V_Warehouse_Satuan where IDDetail = '" & warehouse_detail_id.ToString & "' and WarehouseItem_Code = '" & warehouseheaderid.ToString & "' ;"
                    eds = SQLExecuteQuery(estr)
                    edt = eds.Tables(0)

                    If edt.Rows.Count > 0 Then
                        QuotationNo = edt.Rows(0).Item("Quotation_No").ToString

                        If edt.Rows(0).Item("Container").ToString = "true" Then
                            str &= " UPDATE QuotationDetail " & _
                                   "SET " & _
                                   "LastModified = '" & Now.ToString & "' " & _
                                   ",[status] = 2 " & _
                                   "WHERE IDDetail = '" & edt.Rows(0).Item("QuotationDetailID").ToString & "' and Quotation_No = '" & QuotationNo & "' and status <> 0 ; "
                            str &= " UPDATE ContainerHeader " & _
                                    "SET " & _
                                    "LastModified = '" & Now.ToString & "' " & _
                                    ",[status] = 2 " & _
                                    "WHERE ContainerCode = '" & edt.Rows(0).Item("Nama_Barang").ToString & "' and status <> 0; "

                            str &= " UPDATE ContainerDetail " & _
                                        "SET " & _
                                        "LastModified = '" & Now.ToString & "' " & _
                                        ",[status] = 1 " & _
                                        "WHERE ContainerCode = '" & edt.Rows(0).Item("Nama_Barang").ToString & "' and status <> 0; "

                        Else
                            strdelete &= " UPDATE QuotationDetail " & _
                                "SET " & _
                                "LastModified = '" & Now.ToString & "' " & _
                                ",[status] = 2 " & _
                                "WHERE IDDetail = '" & edt.Rows(0).Item("QuotationDetailID").ToString & "' and Quotation_No = '" & QuotationNo & "' and status <> 0; "
                            str &= " UPDATE QuotationDetail " & _
                                "SET " & _
                                "LastModified = '" & Now.ToString & "' " & _
                                ",[status] = 2 " & _
                                "WHERE IDDetail = '" & edt.Rows(0).Item("QuotationDetailID").ToString & "' and Quotation_No = '" & QuotationNo & "' and status <> 0 ; "
                        End If

                    End If
                Next
            End If

            If str <> "" Then

                hasil = SQLExecuteNonQuery(str)

            End If


            str = ""
            str = "UPDATE InvoiceHeader " & _
                       "SET " & _
                      "LastModified = '" & Now.ToString & "' " & _
                      ",[status] = 7 " & _
                      "WHERE No_Invoice = '" & NoInvoice.ToString & "' and status = 1; "

            str &= "UPDATE InvoiceDetail " & _
                         "SET " & _
                         "LastModified = '" & Now.ToString & "' " & _
                         ",[status] = 7 " & _
                         "WHERE No_Invoice = '" & NoInvoice.ToString & "' and status = 1; "

            If Left(NoInvoice.ToString, 1) = "b" Or Left(NoInvoice.ToString, 1) = "B" Then
                hasil = SQLExecuteNonQuery(str, False, True)
            Else
                hasil = SQLExecuteNonQuery(str)
            End If





            'If Left(NoInvoice.ToString, 1) = "b" Or Left(NoInvoice.ToString, 1) = "B" Then
            '    str = "UPDATE InvoiceHeader " & _
            '           "SET " & _
            '          "LastModified = '" & Now.ToString & "' " & _
            '          ",[status] = 7 " & _
            '          "WHERE No_Invoice = '" & NoInvoice.ToString & "' and status = 1; "

            '    str &= "UPDATE InvoiceDetail " & _
            '                 "SET " & _
            '                 "LastModified = '" & Now.ToString & "' " & _
            '                 ",[status] = 7 " & _
            '                 "WHERE No_Invoice = '" & NoInvoice.ToString & "' and status = 1; "



            '    cekstr = "select IDDetail as MuatBarangDetailID  from MuatBarangDetail WHERE MB_No = '" & IDMuatBarang & "' and status <> 0"
            '    cds = SQLExecuteQuery(cekstr)
            '    cdt = cds.Tables(0)
            '    DTCEK = create_table()


            '    If cdt.Rows.Count > 0 Then
            '        For i As Integer = 0 To cdt.Rows.Count - 1
            '            str &= "UPDATE MuatBarangDetail " & _
            '                 "SET " & _
            '                 "LastModified = '" & Now.ToString & "' " & _
            '                 ",[status] = 7 " & _
            '                 "WHERE IDDetail = " & cdt.Rows(i).Item("MuatBarangDetailID").ToString & " and Mb_No = '" & IDMuatBarang & "' and status = 5; "

            '            'strdelete &= " delete from MuatBarangDetail where IDDetail = " & cdt.Rows(i).Item("MuatBarangDetailID").ToString & " and Mb_No = '" & IDMuatBarang & "' ;"

            '            dstr = "Select Warehouse_Id,WarehouseHeaderID from V_MuatBarang_Detail where IDDetail = " & cdt.Rows(i).Item("MuatBarangDetailID").ToString & " and Mb_No = '" & IDMuatBarang.ToString & "' ;"
            '            dds = SQLExecuteQuery(dstr)
            '            ddt = dds.Tables(0)
            '            warehouse_detail_id = ddt.Rows(0).Item("Warehouse_Id").ToString
            '            warehouseheaderid = ddt.Rows(0).Item("WarehouseHeaderID").ToString
            '            If ddt.Rows.Count > 0 Then
            '                str &= " UPDATE WarehouseDetail " & _
            '                        "SET " & _
            '                        "LastModified = '" & Now.ToString & "' " & _
            '                        ",[status] = 7 " & _
            '                        "WHERE IDDetail = '" & ddt.Rows(0).Item("Warehouse_Id").ToString & "' and WarehouseItem_Code = '" & ddt.Rows(0).Item("WarehouseHeaderID").ToString & "' and Quantity = 0; "
            '                dstr = "select id.id from InvoiceDetail id left join V_MuatBarang_Detail mbd on id.Mb_No = mbd.Mb_No left join " & _
            '                            " WarehouseDetail wd on (mbd.Warehouse_Id = wd.IDDetail and wd.WarehouseItem_Code = mbd.WarehouseHeaderID ) " & _
            '                            " where wd.IDDetail =  '" & ddt.Rows(0).Item("Warehouse_Id").ToString & "' and wd.WarehouseItem_Code = '" & ddt.Rows(0).Item("WarehouseHeaderID").ToString & "' ;"
            '                dds = SQLExecuteQuery(dstr, False)
            '                ddt = dds.Tables(0)
            '                If ddt.Rows.Count <= 0 Then
            '                    strdelete &= " delete from WarehouseDetail where IDDetail = " & warehouse_detail_id.ToString & " and WarehouseItem_Code = '" & warehouseheaderid.ToString & "' and status = 0;"
            '                Else
            '                    strdelete &= " UPDATE WarehouseDetail " & _
            '                      "SET " & _
            '                      "LastModified = '" & Now.ToString & "' " & _
            '                      ",[status] = 7 " & _
            '                      "WHERE IDDetail = '" & warehouse_detail_id.ToString & "' and WarehouseItem_Code = '" & warehouseheaderid.ToString & "' and Quantity = 0; "
            '                End If

            '                estr = "select Container,QuotationDetailID,Nama_Barang,Quotation_No from V_Warehouse_Satuan where IDDetail = '" & warehouse_detail_id.ToString & "' and WarehouseItem_Code = '" & warehouseheaderid.ToString & "' and Quantity = 0 ;"
            '                eds = SQLExecuteQuery(estr)
            '                edt = eds.Tables(0)
            '                QuotationNo = edt.Rows(0).Item("Quotation_No").ToString
            '                If edt.Rows.Count > 0 Then
            '                    If edt.Rows(0).Item("Container").ToString = "true" Then
            '                        strdelete &= " UPDATE QuotationDetail " & _
            '                                "SET " & _
            '                                "LastModified = '" & Now.ToString & "' " & _
            '                                ",[status] = 2 " & _
            '                                "WHERE IDDetail = '" & edt.Rows(0).Item("QuotationDetailID").ToString & "' and Quotation_No = '" & QuotationNo & "' and status = 1 ; "
            '                        strdelete &= " UPDATE ContainerHeader " & _
            '                                "SET " & _
            '                                "LastModified = '" & Now.ToString & "' " & _
            '                                ",[status] = 2 " & _
            '                                "WHERE ContainerCode = '" & edt.Rows(0).Item("Nama_Barang").ToString & "' and status = 1; "

            '                        strdelete &= " UPDATE ContainerDetail " & _
            '                                    "SET " & _
            '                                    "LastModified = '" & Now.ToString & "' " & _
            '                                    ",[status] = 2 " & _
            '                                    "WHERE ContainerCode = '" & edt.Rows(0).Item("Nama_Barang").ToString & "' and status = 1; "
            '                        str &= " UPDATE QuotationDetail " & _
            '                               "SET " & _
            '                               "LastModified = '" & Now.ToString & "' " & _
            '                               ",[status] = 2 " & _
            '                               "WHERE IDDetail = '" & edt.Rows(0).Item("QuotationDetailID").ToString & "' and Quotation_No = '" & QuotationNo & "' and status = 1 ; "
            '                        str &= " UPDATE ContainerHeader " & _
            '                                "SET " & _
            '                                "LastModified = '" & Now.ToString & "' " & _
            '                                ",[status] = 2 " & _
            '                                "WHERE ContainerCode = '" & edt.Rows(0).Item("Nama_Barang").ToString & "' and status = 1; "

            '                        str &= " UPDATE ContainerDetail " & _
            '                                    "SET " & _
            '                                    "LastModified = '" & Now.ToString & "' " & _
            '                                    ",[status] = 2 " & _
            '                                    "WHERE ContainerCode = '" & edt.Rows(0).Item("Nama_Barang").ToString & "' and status = 1; "

            '                    Else
            '                        strdelete &= " UPDATE QuotationDetail " & _
            '                            "SET " & _
            '                            "LastModified = '" & Now.ToString & "' " & _
            '                            ",[status] = 2 " & _
            '                            "WHERE IDDetail = '" & edt.Rows(0).Item("QuotationDetailID").ToString & "' and Quotation_No = '" & QuotationNo & "' and status = 1 ; "
            '                        str &= " UPDATE QuotationDetail " & _
            '                            "SET " & _
            '                            "LastModified = '" & Now.ToString & "' " & _
            '                            ",[status] = 2 " & _
            '                            "WHERE IDDetail = '" & edt.Rows(0).Item("QuotationDetailID").ToString & "' and Quotation_No = '" & QuotationNo & "' and status = 1 ; "

            '                    End If
            '                End If
            '            End If
            '        Next
            '    End If
            '    If strdelete <> "" Then
            '        hasil = SQLExecuteNonQuery(strdelete, True, False) ' PPN
            '    End If
            '    If str <> "" Then
            '        hasil = SQLExecuteNonQuery(str, False, True) 'NPPN
            '    End If
            'Else
            '    str = "UPDATE InvoiceHeader " & _
            '            "SET " & _
            '           "LastModified = '" & Now.ToString & "' " & _
            '           ",[status] = 7 " & _
            '           "WHERE No_Invoice = '" & NoInvoice.ToString & "' and status = 1; "

            '    str &= "UPDATE InvoiceDetail " & _
            '                 "SET " & _
            '                 "LastModified = '" & Now.ToString & "' " & _
            '                 ",[status] = 7 " & _
            '                 "WHERE No_Invoice = '" & NoInvoice.ToString & "' and status = 1; "
            '    strdelete = "UPDATE InvoiceDetail " & _
            '                        "SET " & _
            '                        "LastModified = '" & Now.ToString & "' " & _
            '                        ",[status] = 7 " & _
            '                        "WHERE No_Invoice = '" & NoInvoice.ToString & "' and status = 1; "

            '    cekstr = "select Mb_No from InvoiceDetail WHERE No_Invoice = '" & NoInvoice.ToString & "' and status = 1"
            '    MBNO = SQLExecuteScalar(cekstr)

            '    cekstr = "select IDDetail as MuatBarangDetailID, Mb_No FROM MuatBarangDetail Where Mb_No = '" & MBNO & "' AND [status] <> 0"
            '    cds = SQLExecuteQuery(cekstr)
            '    cdt = cds.Tables(0)
            '    DTCEK = create_table()
            '    If cdt.Rows.Count > 0 Then
            '        For i As Integer = 0 To cdt.Rows.Count - 1
            '            str &= "UPDATE MuatBarangDetail " & _
            '                 "SET " & _
            '                 "LastModified = '" & Now.ToString & "' " & _
            '                 ",[status] = 7 " & _
            '                 "WHERE IDDetail = " & cdt.Rows(i).Item("MuatBarangDetailID").ToString & " and Mb_No = '" & cdt.Rows(i).Item("Mb_No").ToString & "' and status = 5; "

            '            dstr = "Select Warehouse_Id,WarehouseHeaderID  from V_MuatBarang_Detail where IDDetail = " & cdt.Rows(i).Item("MuatBarangDetailID").ToString & " and Mb_No = '" & IDMuatBarang.ToString & "' and [status] <> 0 ;"
            '            dds = SQLExecuteQuery(dstr)
            '            ddt = dds.Tables(0)
            '            If ddt.Rows.Count > 0 Then
            '                str &= " UPDATE WarehouseDetail " & _
            '                        "SET " & _
            '                        "LastModified = '" & Now.ToString & "' " & _
            '                        ",[status] = 7 " & _
            '                        "WHERE IDDetail = '" & ddt.Rows(0).Item("Warehouse_Id").ToString & "' and WarehouseItem_Code = '" & ddt.Rows(0).Item("WarehouseHeaderID").ToString & "' and Quantity = 0 ; "

            '                estr = "select Container,QuotationDetailID,Nama_Barang,Quotation_No from V_Warehouse_Satuan where IDDetail = '" & ddt.Rows(0).Item("Warehouse_Id").ToString & "' and WarehouseItem_Code = '" & ddt.Rows(0).Item("WarehouseHeaderID").ToString & "' and Quantity = 0 and [status] <> 0;"
            '                eds = SQLExecuteQuery(estr)
            '                edt = eds.Tables(0)
            '                QuotationNo = edt.Rows(0).Item("Quotation_No").ToString
            '                If edt.Rows.Count > 0 Then
            '                    If edt.Rows(0).Item("Container").ToString = "true" Then
            '                        str &= " UPDATE QuotationDetail " & _
            '                                    "SET " & _
            '                                    "LastModified = '" & Now.ToString & "' " & _
            '                                    ",[status] = 2 " & _
            '                                    "WHERE IDDetail = '" & edt.Rows(0).Item("QuotationDetailID").ToString & "' and Quotation_No = '" & QuotationNo & "' and status = 1 ; "
            '                        str &= " UPDATE ContainerHeader " & _
            '                                    "SET " & _
            '                                    "LastModified = '" & Now.ToString & "' " & _
            '                                    ",[status] = 2 " & _
            '                                    "WHERE ContainerCode = '" & edt.Rows(0).Item("Nama_Barang").ToString & "' and status =1; "

            '                        str &= " UPDATE ContainerDetail " & _
            '                                    "SET " & _
            '                                    "LastModified = '" & Now.ToString & "' " & _
            '                                    ",[status] = 2 " & _
            '                                    "WHERE ContainerCode= '" & edt.Rows(0).Item("Nama_Barang").ToString & "' and status =1; "

            '                    Else
            '                        str &= " UPDATE QuotationDetail " & _
            '                    "SET " & _
            '                    "LastModified = '" & Now.ToString & "' " & _
            '                    ",[status] = 2 " & _
            '                    "WHERE IDDetail = '" & edt.Rows(0).Item("QuotationDetailID").ToString & "' and Quotation_No = '" & QuotationNo & "' and status = 1 ; "


            '                    End If
            '                End If
            '            End If
            '        Next
            '    End If
            '    If str <> "" Then
            '        hasil = SQLExecuteNonQuery(str)
            '    End If
            '    If strdelete <> "" Then
            '        hasil = SQLExecuteNonQuery(strdelete, True, False)
            '    End If
            'End If
        Catch ex As Exception
            Throw New Exception("Error Close Invoice function <BR> : " & ex.ToString)
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
        Dim fstr As String

        Try

            str = " UPDATE MuatBarang " & _
                "SET " & _
                "LastModified = '" & Now.ToString & "' " & _
                ",[status] = 7 " & _
                "WHERE Mb_No = '" & IDMuatBarang.ToString & "' and status <> 0; "

            If Right(NoInvoice, 2).ToString <> "DS" Then
                mbstring = "select WarehouseheaderID from MuatBarang where Mb_No = '" & IDMuatBarang & "' AND [status] <> 0 "
                mbds = SQLExecuteQuery(mbstring)
                mbdt = mbds.Tables(0)
                Warehouse = mbdt.Rows(0).Item("WarehouseheaderID").ToString

                str &= " UPDATE WarehouseHeader " & _
                    "SET " & _
                    "LastModified = '" & Now.ToString & "' " & _
                    ",[status] = 1 " & _
                    "WHERE WarehouseItem_Code = '" & Warehouse.ToString & "' and status <> 0; "

                fstr = "select Quotation_No from Warehouseheader where WarehouseItem_Code = '" & Warehouse.ToString & "' and status <> 0"
                quotation = SQLExecuteScalar(fstr)

                str &= " UPDATE MasterQuotation " & _
                    "SET " & _
                    "LastModified = '" & Now.ToString & "' " & _
                    ",[Close] = 'Deal'  " & _
                    ",[status] = 2 " & _
                    "WHERE Quotation_No = '" & quotation.ToString & "' and status <> 0; "


                hasil = SQLExecuteNonQuery(str)
            End If

        Catch ex As Exception
            Throw New Exception("Error CloseHeader function " & ex.ToString)
        End Try

    End Sub

    Private Sub CloseDP(ByVal no_invoice As String, ByVal mb_no As String, ByVal paid As String)
        Try
            Dim sqlstr As String = ""
            Dim Warehouseitemcode As String = ""

            sqlstr = "SELECT WarehouseHeaderID FROM MuatBarang Where Mb_No = '" & mb_no & "' AND [status] <> 0"
            Warehouseitemcode = SQLExecuteScalar(sqlstr)


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

            sqlstring &= "Update WarehouseHeader set Paid = " & CDbl(paid) & " where Warehouseitem_Code = '" & Warehouseitemcode & "' and status = 1;"
            sqlstring &= "Update WarehouseDetail set Paid = " & CDbl(paid) & " where Warehouseitem_Code = '" & Warehouseitemcode & "' and status = 1"

            If Left(no_invoice, 1) = "b" Or Left(no_invoice, 1) = "B" Then
                result = SQLExecuteNonQuery(sqlstring, False, True)
            Else
                result = SQLExecuteNonQuery(sqlstring)
            End If

            'If result > 0 Then
            '    linfoberhasil.Visible = True
            '    linfoberhasil.Text = "Close Berhasil"
            '    load_grid_invoice_header()
            'End If
        Catch ex As Exception
            Throw New Exception("<b>Error function Delete</b>" & ex.ToString)
        End Try
    End Sub


#End Region

#Region "BUTTON"
    Protected Sub btKembaliDevPeriod_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btKembaliDevPeriod.Click
        Try
            Dim Flag As String = ""

            Panel_Input.Visible = True
            Panel_Grid.Visible = True
            Panel_Report.Visible = False

            Grid_Invoice_Parent.DataSource = Nothing
            Grid_Invoice_Parent.DataBind()

            If ddltype.SelectedValue = "InvoiceDP" Then
                Flag = "Yes"
            ElseIf ddltype.SelectedValue = "InvoiceNDP" Then
                Flag = "No"
            End If

            load_invoice_parent(Flag)

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
            If ddltype.SelectedValue = "InvoiceDP" Then
                load_invoice_parent("Yes")
            Else
                load_invoice_parent("No")
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