Imports System.Data
Imports System.Data.SqlClient
Imports DevExpress.Web.ASPxGridView
Partial Public Class BAST
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
                Panel_Input.Visible = True
                Panel_Grid.Visible = True
                Panel_Report.Visible = False
                historygrid.Visible = False
                historyinput.Visible = False
                hfIDK.Value = "Insert"
                load_ddl()
                load_ddl_history()
                BASTDate.Date = Today
                'create_session()
                Session("Grid_Kapal_Parent_Bast") = Nothing
                Session("Grid_HistoryBAST") = Nothing
            End If

            If Not Session("Grid_Kapal_Parent_Bast") Is Nothing Then
                Grid_Kapal_Parent.DataSource = CType(Session("Grid_Kapal_Parent_Bast"), DataTable)
                Grid_Kapal_Parent.DataBind()
            End If
            If Not Session("Grid_HistoryBAST") Is Nothing Then
                Grid_History.DataSource = CType(Session("Grid_HistoryBAST"), DataTable)
                Grid_History.DataBind()
            End If
        Catch ex As Exception
            Response.Write(ex.ToString)
        End Try
    End Sub


    Private Function loadbastno(ByVal IDKAPAL As String) As String
        Dim month, year, tanggal As String
        Dim value As String = ""
        Dim no As Integer
        Dim pisah As String()
        Dim sql As String
        Dim dt As DataTable
        Dim ds As DataSet
        Dim aliaskapal As String
        Try
            tanggal = CDate(BASTDate.Text)
            month = CDate(tanggal).ToString("MM")
            year = CDate(tanggal).ToString("yy")
            sql = " select Alias_Kapal from Kapal where IDDetail = '" & IDKAPAL & "' and status = 1"
            ds = SQLExecuteQuery(sql)
            dt = ds.Tables(0)
            aliaskapal = ""
            If dt.Rows.Count > 0 Then
                aliaskapal = dt.Rows(0).Item("Alias_Kapal").ToString
            End If

            sqlstring = "SELECT TOP 1 BastNo FROM BAST " & _
                        "ORDER BY ID DESC"
            result = SQLExecuteScalar(sqlstring)

            If result.ToString <> "" Then

                If Date.Compare(Now.ToString("MM/dd"), CDate(("01/01").ToString)) = 0 Then
                    no = "1"
                Else
                    pisah = result.Split("/")
                    no = CDbl(pisah(0)) + 1
                End If
            Else
                no = 1
            End If
            value = no & "/" & Singkatan & "/" & "BAST" & "/" & aliaskapal & "/" & CekBulan(month) & "/" & year



        Catch ex As Exception
            Response.Write("Error Load_Quotation_No :<BR>" & ex.ToString)
        End Try
        Return value
    End Function

#End Region

#Region "GRID"
    Private Sub load_kapal_parent()
        Try
            'sqlstring = " SELECT mb.Mb_No as ID ,mb.WarehouseHeaderID,mb.Customer_Id,mc.Jenis_Perusahaan + ' ' + mc.Nama_Customer as Nama_Customer,mb.Penerima,mb.Tanggal," & _
            '            " mb.Mb_No,mb.Kapal,mk.Nama_Kapal From MuatBarang mb " & _
            '            " left join MasterCustomer mc on mb.Customer_Id = mc.Kode_Customer " & _
            '            " left join Kapal mk on mb.Kapal = mk.IDDetail Where (mb.status = 5 or mb.status = 7) and " & _
            '            " mb.Mb_No not in ( select bt.MB_ID from BAST bt where status = 1) and " & _
            '            " mb.Kapal = '" & hfIDK.Value & "' and mc.status = 1 and mk.status = 1 and mb.tanggal = '" & BASTDate.Text.ToString & "' order by mb.ID desc "

            sqlstring = "SELECT distinct mb.Mb_No as ID ,mb.WarehouseHeaderID,mb.Customer_Id,mc.Jenis_Perusahaan + ' ' + mc.Nama_Customer as Nama_Customer,mb.Penerima,MBRD.Depart_Date as Tanggal, " & _
                        "mb.Mb_No,mb.Kapal,mk.Nama_Kapal From MuatBarang mb  " & _
                        "left join MasterCustomer mc on mb.Customer_Id = mc.Kode_Customer " & _
                        "left join Kapal mk on mb.Kapal = mk.IDDetail " & _
                        "left join MBRDetail MBRD on mb.Mb_No = MBRD.Mb_No " & _
                        "Where (mb.status = 2 or mb.status = 5) and  " & _
                        "mb.Mb_No not in ( select bt.MB_ID from BAST bt where status = 1) and  " & _
                        "mb.Kapal = '" & hfIDK.Value & "' and mc.status = 1 and mk.status = 1 and " & _
                        "MBRD.Depart_Date = '" & BASTDate.Text.ToString & "'"

            DS = SQLExecuteQuery(sqlstring)
            DT = DS.Tables(0)
            iDT.Columns.Add(New DataColumn("ID", GetType(String)))
            iDT.Columns.Add(New DataColumn("WarehouseHeaderID", GetType(String)))
            iDT.Columns.Add(New DataColumn("Penerima", GetType(String)))
            iDT.Columns.Add(New DataColumn("Customer_Id", GetType(String)))
            iDT.Columns.Add(New DataColumn("Nama_Customer", GetType(String)))
            iDT.Columns.Add(New DataColumn("Tanggal", GetType(DateTime)))
            iDT.Columns.Add(New DataColumn("Mb_No", GetType(String)))
            iDT.Columns.Add(New DataColumn("KapalID", GetType(String)))
            iDT.Columns.Add(New DataColumn("Kapal", GetType(String)))

            If DT.Rows.Count > 0 Then
                For i As Integer = 0 To DT.Rows.Count - 1
                    With DT.Rows(i)
                        DR = iDT.NewRow()
                        DR("ID") = .Item("ID")
                        DR("WarehouseHeaderID") = .Item("WarehouseHeaderID")
                        DR("Penerima") = TukarNamaPerusahaan(.Item("Penerima").ToString)
                        DR("Customer_Id") = .Item("Customer_Id")
                        DR("Nama_Customer") = .Item("Nama_Customer")
                        DR("Tanggal") = CDate(.Item("Tanggal").ToString).ToString("MM/dd/yyyy")
                        DR("Mb_No") = .Item("Mb_No")
                        DR("KapalID") = .Item("Kapal")
                        DR("Kapal") = .Item("Nama_Kapal")
                        iDT.Rows.Add(DR)
                    End With
                Next
                Session("Grid_Kapal_Parent_Bast") = iDT
                Grid_Kapal_Parent.DataSource = iDT
                Grid_Kapal_Parent.KeyFieldName = "Mb_No"
                Grid_Kapal_Parent.DataBind()
            Else
                Grid_Kapal_Parent.DataSource = Nothing
                Grid_Kapal_Parent.DataBind()
            End If

        Catch ex As Exception
            Response.Write("load_kapal_parent : <BR> " & ex.ToString)
        End Try
    End Sub
    Private Sub load_bast()
        Try


            sqlstring = " select bt.ID,mc.Nama_Customer, MB.Penerima, bt.BastNo,bt.Tanggal,bt.Kapal,k.Nama_Kapal,bt.MB_ID from " & _
                        " BAST bt left join Kapal k on bt.Kapal = k.IDDetail " & _
                        " left join MuatBarang mb on mb.Mb_No = bt.MB_ID " & _
                        " left join MasterCustomer mc on mb.Customer_Id = mc.Kode_Customer " & _
                        " where bt.status = 1 " & _
                        " and k.status <> 0 " & _
                        " and mb.status<> 0 " & _
                        " and mc.status <> 0 " & _
                        " and k.status <> 0 and bt.Kapal = '" & hfIDKHIS.Value & "' and bt.Tanggal Between '" & tbFrom.Text.ToString & "' AND '" & tbSampai.Text.ToString & "'"
            DS = SQLExecuteQuery(sqlstring)
            DT = DS.Tables(0)

            iDT.Columns.Add(New DataColumn("ID", GetType(String)))
            iDT.Columns.Add(New DataColumn("Nama_Customer", GetType(String)))
            iDT.Columns.Add(New DataColumn("Penerima", GetType(String)))
            iDT.Columns.Add(New DataColumn("BastNo", GetType(String)))
            iDT.Columns.Add(New DataColumn("Tanggal", GetType(DateTime)))
            iDT.Columns.Add(New DataColumn("Kapal", GetType(String)))
            iDT.Columns.Add(New DataColumn("Nama_Kapal", GetType(String)))
            iDT.Columns.Add(New DataColumn("MB_ID", GetType(String)))
            If DT.Rows.Count > 0 Then
                For i As Integer = 0 To DT.Rows.Count - 1
                    With DT.Rows(i)
                        DR = iDT.NewRow()
                        DR("ID") = .Item("ID")
                        DR("BastNo") = .Item("BastNo")
                        DR("Tanggal") = CDate(.Item("Tanggal").ToString).ToString("MM/dd/yyyy")
                        DR("Kapal") = .Item("Kapal")
                        DR("Nama_Kapal") = .Item("Nama_Kapal")
                        DR("MB_ID") = .Item("MB_ID")
                        DR("Nama_Customer") = .Item("Nama_Customer")
                        DR("Penerima") = .Item("Penerima")
                        iDT.Rows.Add(DR)
                    End With
                Next
                Session("Grid_HistoryBAST") = iDT
                Grid_History.DataSource = iDT
                Grid_History.KeyFieldName = "ID"
                Grid_History.DataBind()
            Else
                Grid_History.DataSource = Nothing
                Grid_History.DataBind()
            End If

            'hfIDKHIS.Value = ""
        Catch ex As Exception
            Response.Write("load_kapal_parent : <BR> " & ex.ToString)
        End Try
    End Sub
    Private Sub load_kapal_child(ByVal grid As ASPxGridView)
        Dim kDT As New DataTable
        Dim cDS As New DataSet
        Dim cDT As New DataTable
        Dim kSTR As String
        Dim namabarang As String
        Try

            kDT.Columns.Add(New DataColumn("ID", GetType(String)))
            kDT.Columns.Add(New DataColumn("Mb_No", GetType(String)))
            kDT.Columns.Add(New DataColumn("Nama_Barang", GetType(String)))
            kDT.Columns.Add(New DataColumn("Quantity", GetType(String)))
            kDT.Columns.Add(New DataColumn("PackedContID", GetType(String)))
            kDT.Columns.Add(New DataColumn("NoContainer", GetType(String)))

            sqlstring = " SELECT MBD.IDDetail as ID,MBD.Mb_No,MBD.PackedContID,WH.Nama_Barang,MBD.Quantity,WH.Container " & _
                                " FROM MuatBarangDetail MBD JOIN V_Warehouse_Satuan WH on " & _
                                " (MBD.Warehouse_Id = WH.IDDetail and  WH.WarehouseItem_Code = '" & grid.GetMasterRowFieldValues("WarehouseHeaderID") & "' )" & _
                                " Where MBD.Mb_No = '" & grid.GetMasterRowKeyValue() & "' AND MBD.status = 5 and WH.status <>0 Order by MBD.timestamp desc"

            DS = SQLExecuteQuery(sqlstring)
            DT = DS.Tables(0)
            If DT.Rows.Count > 0 Then
                For i As Integer = 0 To DT.Rows.Count - 1
                    DR = kDT.NewRow
                    With DT.Rows(i)
                        If .Item("Container").ToString = "true" Or .Item("Container").ToString = "kubikasi" Then
                            kSTR = "Select cd.NamaBarang,ch.NoKontainer as ContainerCode from ContainerDetail cd " & _
                                           " left join ContainerHeader ch on cd.ContainerCode = ch.ContainerCode " & _
                                           " where cd.ContainerCode= '" & .Item("Nama_Barang").ToString & "' and cd.status =1 "
                            cDS = SQLExecuteQuery(kSTR)
                            cDT = cDS.Tables(0)
                            namabarang = ""
                            If cDT.Rows.Count > 0 Then
                                For e As Integer = 0 To cDT.Rows.Count - 1
                                    If e > 3 Then
                                        namabarang &= "dll."
                                        Exit For
                                    End If

                                    If e = cDT.Rows.Count - 1 Then
                                        namabarang &= cDT.Rows(e).Item("NamaBarang").ToString + "."
                                    Else
                                        namabarang &= cDT.Rows(e).Item("NamaBarang").ToString + ","
                                    End If

                                Next
                                DR("Nama_Barang") = namabarang.ToString
                                DR("Quantity") = .Item("Quantity").ToString
                                DR("Mb_No") = .Item("Mb_No").ToString
                                DR("ID") = .Item("ID").ToString
                                DR("PackedContID") = .Item("PackedContID").ToString
                                If .Item("PackedContID").ToString = "0" Then
                                    DR("NoContainer") = cDT.Rows(0).Item("ContainerCode").ToString
                                Else
                                    DR("NoContainer") = .Item("PackedContID").ToString
                                End If
                                kDT.Rows.Add(DR)
                            End If
                        Else
                            DR("Nama_Barang") = .Item("Nama_Barang").ToString
                            DR("Quantity") = .Item("Quantity").ToString
                            DR("Mb_No") = .Item("Mb_No").ToString
                            DR("ID") = .Item("ID").ToString
                            DR("PackedContID") = .Item("PackedContID").ToString
                            If .Item("PackedContID").ToString = "0" Then
                                DR("NoContainer") = "--"
                            Else
                                DR("NoContainer") = .Item("ContainerCode").ToString
                            End If
                            kDT.Rows.Add(DR)
                        End If
                    End With
                Next
            End If

            grid.DataSource = kDT
        Catch ex As Exception
            Response.Write("Error Load Grid Child :<BR>" & ex.ToString)
        End Try
    End Sub

    Protected Sub Grid_Kapal_Child_DataSelect(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Call load_kapal_child(TryCast(sender, ASPxGridView))
        Catch ex As Exception
            Response.Write("Error Load Grid Child DataSelect  :<BR>" & ex.ToString)
        End Try
    End Sub


    Private Sub Grid_History_RowCommand(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewRowCommandEventArgs) Handles Grid_History.RowCommand
        Try
            Select Case e.CommandArgs.CommandName
                Case "Print"
                    
                    If validation_print() Then
                        hfIDK.Value = Grid_History.GetRowValues(e.VisibleIndex, "Kapal").ToString
                        hfBastNo.Value = Grid_History.GetRowValues(e.VisibleIndex, "BastNo").ToString
                        'lblReport.Text = quoKonfHeaderHist()
                        lblReport.Text = quotationHeaderHist(Grid_History.GetRowValues(e.VisibleIndex, "MB_ID").ToString)
                        Panel_Input.Visible = False
                        Panel_Grid.Visible = False
                        historygrid.Visible = False
                        historyinput.Visible = False
                        Panel_Report.Visible = True

                    End If

            End Select
        Catch ex As Exception
            Throw New Exception("Error Grid History Row Command <BR>: " & ex.ToString)
        End Try
    End Sub

    Private Sub Grid_Kapal_Parent_RowCommand(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewRowCommandEventArgs) Handles Grid_Kapal_Parent.RowCommand
        Try
            Select Case e.CommandArgs.CommandName
                Case "PrintBAST"
                   
                    If validation() Then

                        'lblReport.Text = quoKonfHeader()
                        lblReport.Text = quotationHeader(Grid_Kapal_Parent.GetRowValues(e.VisibleIndex, "ID").ToString)
                        insertbast(Grid_Kapal_Parent.GetRowValues(e.VisibleIndex, "ID").ToString, Grid_Kapal_Parent.GetRowValues(e.VisibleIndex, "KapalID").ToString, e.VisibleIndex)
                        Panel_Input.Visible = False
                        Panel_Grid.Visible = False
                        Panel_Report.Visible = True

                    End If
            End Select
        Catch ex As Exception
            Response.Write("Error Grid_Kapal_Parent_RowCommand <BR> : " & ex.ToString)
        End Try
    End Sub

    Private Sub Grid_Kapal_Parent_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Grid_Kapal_Parent.PreRender
        Grid_Kapal_Parent.FocusedRowIndex = -1
    End Sub

#End Region

#Region "METHOD"

    Private Sub Create_Season()
        Try
            Dim iDT As New DataTable

            iDT.Columns.Add(New DataColumn("ID", GetType(String)))
            iDT.Columns.Add(New DataColumn("Nama_Customer", GetType(String)))
            iDT.Columns.Add(New DataColumn("Penerima", GetType(String)))
            iDT.Columns.Add(New DataColumn("BastNo", GetType(String)))
            iDT.Columns.Add(New DataColumn("Tanggal", GetType(DateTime)))
            iDT.Columns.Add(New DataColumn("Kapal", GetType(String)))
            iDT.Columns.Add(New DataColumn("Nama_Kapal", GetType(String)))
            iDT.Columns.Add(New DataColumn("MB_ID", GetType(String)))

            Session("Grid_Kapal_Parent_Bast") = iDT
            Grid_Kapal_Parent.DataSource = iDT
            Grid_Kapal_Parent.KeyFieldName = "Mb_No"
            Grid_Kapal_Parent.DataBind()
        Catch ex As Exception
            Throw New Exception(ex.ToString)
        End Try
    End Sub

    Private Sub remove_item(ByVal Key As Integer)
        Try
            Dim aDT As DataTable = CType(Session("Grid_Kapal_Parent_Bast"), DataTable)

            aDT.Rows.RemoveAt(Key)
            Create_Season()
            Session("Grid_Kapal_Parent_Bast") = aDT
            Grid_Kapal_Parent.DataSource = aDT
            Grid_Kapal_Parent.DataBind()
        Catch ex As Exception
            Throw New Exception(ex.ToString)
        End Try
    End Sub

    Private Function tableHeader() As String
        Dim HeaderReport As String

        HeaderReport = "     <table width=772px cellpadding=4 cellspacing=0 style=""height:440px;font-family:verdana;font-size:11px;table-layout:fixed;"">" & _
                      "       <tr>" & _
                      "         <td align=""left"" style=""width: 40px;height:10px;vertical-align :top;border-top:1px black solid;border-right:1px black solid;border-bottom:1px black solid;"">" & _
                      "           <b>JUMLAH</b>" & _
                      "         </td>" & _
                      "         <td align=""left"" style=""width: 200px;vertical-align :top;height:20px;border-top:1px black solid;border-right:1px black solid;border-bottom:1px black solid;"">" & _
                      "           <b>NAMA BARANG</b>" & _
                      "         </td>" & _
                      "         <td align=""left"" style=""width: 100px;vertical-align :top;height:20px;border-top:1px black solid;border-right:1px black solid;border-bottom:1px black solid;"">" & _
                      "           <b>KETERANGAN</b>" & _
                      "         </td>" & _
                      "       </tr>"

        Return HeaderReport

    End Function

    Private Function tableHeaderOver() As String
        Dim HeaderReport As String

        HeaderReport = "     <table width=772px cellpadding=4 cellspacing=0 style=""height:900px;font-family:verdana;font-size:11px;table-layout:fixed;"">" & _
                      "       <tr>" & _
                      "         <td style=""width: 40px;height:10px;vertical-align :top;border-left:1px black solid;border-top:1px black solid;border-right:1px black solid;border-bottom:1px black solid;"">" & _
                      "           <b>JUMLAH</b>" & _
                      "         </td>" & _
                      "         <td style=""width: 200px;vertical-align :top;height:20px;border-top:1px black solid;border-right:1px black solid;border-bottom:1px black solid;"">" & _
                      "           <b>NAMA BARANG</b>" & _
                      "         </td>" & _
                      "         <td style=""width: 100px;vertical-align :top;height:20px;border-top:1px black solid;border-right:1px black solid;border-bottom:1px black solid;"">" & _
                      "           <b>KETERANGAN</b>" & _
                      "         </td>" & _
                      "       </tr>"

        Return HeaderReport

    End Function

    Private Function isiOver(ByVal blid As String) As String
        Dim isiTable As String

        Dim cDT As DataTable
        Dim cDS As DataSet
        Dim cSt As String
        Dim namabarang As String
        Dim BanyakBarang As String
        Dim Ukuran As String
        Dim totalcont As Integer
        Dim totalcoll As Integer
        Dim borderbotton As String
        Dim keterangan As String = ""
        Dim mDT As DataTable

        Try

            Dim jumlahdata As Integer = 30
            isiTable = ""
            

            'sqlstring = "select A.Penerima, A.Nama_Customer, A.Container, A.Nama_Barang, SUM(A.Quantity) as Quantity, " & _
            '            " A.Kapal, A.Nama_Satuan, A.Keterangan, A.Nama_kapal, SUM(totalberat) as totalberat, Sum(A.totalukuran) as totalukuran, " & _
            '            " A.NamaHarga, A.panjang, A.lebar, A.tinggi " & _
            '            " FROM " & _
            '            " (select mbd.IDDetail as ID,mb.Penerima,mc.Jenis_Perusahaan + ' ' + mc.Nama_Customer as Nama_Customer, " & _
            '            " wd.Container, wd.Nama_Barang,  mbd.Quantity as Quantity,wd.Nama_Satuan, " & _
            '            " wd.Keterangan,mk.Nama_Kapal,mb.Kapal,(wd.Berat * mbd.Quantity) as totalberat , " & _
            '            " cast((wd.Panjang * wd.Lebar * wd.Tinggi * mbd.Quantity ) as decimal (20,8)) as totalukuran,mhd.NamaHarga, " & _
            '            " wd.Panjang, wd.Lebar, wd.Tinggi from MuatBarang mb  " & _
            '            " left join MuatBarangDetail mbd  on mb.Mb_No = mbd.Mb_No  " & _
            '            " left join V_Warehouse_Satuan wd on (mbd.Warehouse_Id = wd.IDDetail and wd.WarehouseItem_Code = mb.WarehouseHeaderID )  " & _
            '            " left join Kapal mk on mb.Kapal = mk.IDDetail  left join MasterCustomer mc on mb.Customer_Id = mc.Kode_Customer  " & _
            '            " left join QuotationDetail qd on qd.Quotation_No = wd.Quotation_No and wd.QuotationDetailID = qd.IDDetail  " & _
            '            " left join MasterHargaDefault mhd on mhd.ID = qd.SatuanID  " & _
            '            " where (mb.status = 2 or mb.status=7 or mb.status=5) and " & _
            '            " (mbd.status = 2 or mbd.status=7 or mb.status=5) " & _
            '            " and mc.status = 1 and (wd.status = 1 or wd.status =7) " & _
            '            " and mk.status = 1 " & _
            '            " and mhd.[status] = 1 and mb.Mb_No = '" & blid.ToString & "' ) as A " & _
            '            " Group by A.Penerima, A.Nama_Customer, " & _
            '            " A.Container, A.Nama_Barang, A.Kapal,A.Nama_Satuan, " & _
            '            " A.Keterangan,A.Nama_Kapal,A.NamaHarga, " & _
            '            " A.Panjang, A.Lebar, A.Tinggi "

            sqlstring = "select A.Penerima, A.Nama_Customer, A.Container, A.Nama_Barang, SUM(A.Quantity) as Quantity, " & _
                        " A.Kapal, A.Nama_Satuan, A.Keterangan, A.Nama_kapal, SUM(totalberat) as totalberat, Sum(A.totalukuran) as totalukuran, " & _
                        " A.NamaHarga, A.panjang, A.lebar, A.tinggi " & _
                        " FROM " & _
                        " (select mbd.IDDetail as ID,mb.Penerima,mc.Jenis_Perusahaan + ' ' + mc.Nama_Customer as Nama_Customer, " & _
                        " wd.Container, wd.Nama_Barang,  mbd.Quantity as Quantity,wd.Nama_Satuan, " & _
                        " wd.Keterangan,mk.Nama_Kapal,mb.Kapal,(wd.Berat * mbd.Quantity) as totalberat , " & _
                        " cast((wd.Panjang * wd.Lebar * wd.Tinggi * mbd.Quantity ) as decimal (20,8)) as totalukuran,mhd.NamaHarga, " & _
                        " wd.Panjang, wd.Lebar, wd.Tinggi from MuatBarang mb  " & _
                        " left join MuatBarangDetail mbd  on mb.Mb_No = mbd.Mb_No  " & _
                        " left join V_Warehouse_Satuan wd on (mbd.Warehouse_Id = wd.IDDetail and wd.WarehouseItem_Code = mb.WarehouseHeaderID )  " & _
                        " left join Kapal mk on mb.Kapal = mk.IDDetail  left join MasterCustomer mc on mb.Customer_Id = mc.Kode_Customer  " & _
                        " left join QuotationDetail qd on qd.Quotation_No = wd.Quotation_No and wd.QuotationDetailID = qd.IDDetail  " & _
                        " left join MasterHargaDefault mhd on mhd.ID = qd.SatuanID  " & _
                        " where (mb.status = 2 or mb.status=7 or mb.status=5) and " & _
                        " (mbd.status = 2 or mbd.status=7 or mb.status=5) " & _
                        " and mc.status = 1 and (wd.status = 1 or wd.status =7) " & _
                        " and mk.status = 1 " & _
                        " and mhd.[status] = 1 and mb.Mb_No = '" & blid.ToString & "' ) as A " & _
                        " Group by A.Penerima, A.Nama_Customer, " & _
                        " A.Container, A.Nama_Barang, A.Kapal,A.Nama_Satuan, " & _
                        " A.Keterangan,A.Nama_Kapal,A.NamaHarga, " & _
                        " A.Panjang, A.Lebar, A.Tinggi "

            mDT = CType(Session("BastQuery"), DataTable)


            If mDT.Rows.Count > 0 Then
                If mDT.Rows.Count > jumlahdata Then
                    Dim i As Integer = 0
                    Dim a As Integer = jumlahdata
                    Dim b As Integer = 1
                    Dim c As Integer

                    c = bulatkankeatas(CInt(mDT.Rows.Count) / jumlahdata) 'untuk nentuin halamannya berapa banyak

                    For j As Integer = 0 To c - 1 'looping halaman

                        isiTable &= " <Table width=780px><tr style=""height:230px""><td></td></tr></table>  "
                        isiTable &= tableHeaderOver()

                        If j = c - 1 Then
                            a = mDT.Rows.Count
                        End If

                        For z As Integer = i To a - 1


                            namabarang = ""
                            BanyakBarang = ""
                            Ukuran = ""
                            borderbotton = ""
                            With mDT.Rows(z)
                                If .Item("Container") = "true" Or .Item("Container").ToString = "kubikasi" Then
                                    cSt = "Select ch.NoKontainer, cd.NamaBarang,cd.Qty,mso.Nama_Satuan from ContainerDetail cd join MasterSatuanOther mso on cd.SatuanID = mso.IDDetail JOIN Containerheader ch on ch.ContainerCode = cd.ContainerCode " & _
                                      "where cd.ContainerCode = '" & .Item("Nama_Barang").ToString & "' and cd.status <> 0 and mso.status = 1"
                                    cDS = SQLExecuteQuery(cSt)
                                    cDT = cDS.Tables(0)
                                    If cDT.Rows.Count > 0 Then
                                        For e As Integer = 0 To cDT.Rows.Count - 1
                                            If e > 4 Then
                                                namabarang &= "dll."
                                                Exit For
                                            End If
                                            If e = cDT.Rows.Count - 1 Then
                                                namabarang &= cDT.Rows(e).Item("NamaBarang").ToString + "." & " (" & cDT.Rows(e).Item("NoKontainer").ToString & ")"
                                            Else
                                                namabarang &= cDT.Rows(e).Item("NamaBarang").ToString + " ,"
                                            End If
                                        Next
                                    End If

                                    If .Item("Container").ToString = "true" Then
                                        BanyakBarang = .Item("Quantity").ToString + " Container"
                                        totalcont = totalcont + CInt(.Item("Quantity").ToString)
                                        keterangan = .Item("Keterangan").ToString
                                    Else
                                        BanyakBarang = .Item("Quantity").ToString + " Colly"
                                        totalcoll = totalcoll + CInt(.Item("Quantity").ToString)
                                        keterangan = .Item("Keterangan").ToString
                                    End If
                                Else
                                    namabarang &= .Item("Nama_Barang").ToString
                                    BanyakBarang = Cek_Data(Format(CDbl(.Item("Quantity").ToString), "##,###,###,##").ToString) + " " + .Item("Nama_Satuan").ToString
                                    Ukuran = Cek_Data(Format(CDbl(.Item("totalukuran").ToString), "##,###,###,##").ToString)
                                    keterangan = .Item("Keterangan").ToString
                                    totalcoll = totalcoll + CInt(.Item("Quantity").ToString)
                                End If
                                If i = mDT.Rows.Count - 1 Then
                                    borderbotton = "border-bottom:1px black solid;"
                                End If
                                If i = mDT.Rows.Count - 1 Then
                                    isiTable &= " <tr style=""height:7px;""> " & _
                                              " <td align=left style=""border-right:1px black solid;border-left:1px black solid;"" > " & _
                                              "   " & BanyakBarang.ToString & _
                                              " </td>" & _
                                              " <td align=left style=""border-right:1px black solid;"" > " & _
                                              "   " & namabarang.ToString & _
                                              " </td>" & _
                                              " <td align=left style=""border-right:1px black solid;"" > " & _
                                              " " & keterangan & "  " & _
                                              " </td>" & _
                                              "</tr>"
                                Else
                                    isiTable &= " <tr style=""height:7px;""> " & _
                                                  " <td align=left style=""border-right:1px black solid;border-left:1px black solid;"" > " & _
                                                  "   " & BanyakBarang.ToString & _
                                                  " </td>" & _
                                                  " <td align=left style=""border-right:1px black solid;"" > " & _
                                                  "   " & namabarang.ToString & _
                                                  " </td>" & _
                                                  " <td align=left style=""border-right:1px black solid;"" > " & _
                                                  "  " & keterangan & " " & _
                                                  " </td>" & _
                                                  "</tr>"
                                End If
                            End With
                        Next
                        i = a
                        b = b + 1
                        a = (jumlahdata * b)

                        isiTable &= "       <tr >" & _
                                    "         <td style=""vertical-align :bottom;border-right:1px black solid;border-bottom:1px black solid;border-left:1px black solid;"">" & _
                                    "         </td>" & _
                                    "         <td style=""vertical-align :top;border-right:1px black solid;border-bottom:1px black solid;"">" & _
                                    "           &nbsp;" & _
                                    "         </td>" & _
                                    "         <td style=""vertical-align :top;border-right:1px black solid;border-bottom:1px black solid;"">" & _
                                    "           &nbsp;" & _
                                    "         </td>" & _
                                    "       </tr>" & _
                                    "       </table>"

                    Next

                ElseIf mDT.Rows.Count < jumlahdata Then

                    isiTable = " <Table width=780px><tr style=""height:230px""><td></td></tr></table>  "
                    isiTable &= tableHeaderOver()


                    For i As Integer = 0 To mDT.Rows.Count - 1


                        namabarang = ""
                        BanyakBarang = ""
                        Ukuran = ""
                        borderbotton = ""
                        With mDT.Rows(i)
                            If .Item("Container") = "true" Or .Item("Container").ToString = "kubikasi" Then
                                cSt = "Select cd.NamaBarang,cd.Qty,mso.Nama_Satuan from ContainerDetail cd left join MasterSatuanOther mso on cd.SatuanID = mso.IDDetail where cd.ContainerCode = '" & .Item("Nama_Barang").ToString & "' and cd.status <> 0 and mso.status = 1"
                                cDS = SQLExecuteQuery(cSt)
                                cDT = cDS.Tables(0)
                                If cDT.Rows.Count > 0 Then
                                    For e As Integer = 0 To cDT.Rows.Count - 1
                                        If e > 4 Then
                                            namabarang &= "dll."
                                            Exit For
                                        End If
                                        If e = cDT.Rows.Count - 1 Then
                                            namabarang &= cDT.Rows(e).Item("NamaBarang").ToString + "."
                                        Else
                                            namabarang &= cDT.Rows(e).Item("NamaBarang").ToString + " ,"
                                        End If
                                    Next
                                End If

                                If .Item("Container").ToString = "true" Then
                                    BanyakBarang = .Item("Quantity").ToString + " Container"
                                    totalcont = totalcont + CInt(.Item("Quantity").ToString)
                                    keterangan = .Item("Keterangan").ToString
                                Else
                                    BanyakBarang = .Item("Quantity").ToString + " Colly"
                                    totalcoll = totalcoll + CInt(.Item("Quantity").ToString)
                                    keterangan = .Item("Keterangan").ToString
                                End If
                            Else
                                namabarang &= .Item("Nama_Barang").ToString
                                BanyakBarang = Cek_Data(Format(CDbl(.Item("Quantity").ToString), "##,###,###,##").ToString) + " " + .Item("Nama_Satuan").ToString
                                Ukuran = Cek_Data(Format(CDbl(.Item("totalukuran").ToString), "##,###,###,##").ToString)
                                keterangan = .Item("Keterangan").ToString
                                totalcoll = totalcoll + CInt(.Item("Quantity").ToString)
                            End If
                            If i = mDT.Rows.Count - 1 Then
                                borderbotton = "border-bottom:1px black solid;"
                            End If
                            If i = mDT.Rows.Count - 1 Then
                                isiTable &= " <tr style=""height:7px;""> " & _
                                          " <td align=left style=""border-right:1px black solid;border-left:1px black solid;"" > " & _
                                          "   " & BanyakBarang.ToString & _
                                          " </td>" & _
                                          " <td align=left style=""border-right:1px black solid;"" > " & _
                                          "   " & namabarang.ToString & _
                                          " </td>" & _
                                          " <td align=left style=""border-right:1px black solid;"" > " & _
                                          " " & keterangan & "  " & _
                                          " </td>" & _
                                          "</tr>"
                            Else
                                isiTable &= " <tr style=""height:7px;""> " & _
                                              " <td align=left style=""border-right:1px black solid;border-left:1px black solid;"" > " & _
                                              "   " & BanyakBarang.ToString & _
                                              " </td>" & _
                                              " <td align=left style=""border-right:1px black solid;"" > " & _
                                              "   " & namabarang.ToString & _
                                              " </td>" & _
                                              " <td align=left style=""border-right:1px black solid;"" > " & _
                                              "  " & keterangan & " " & _
                                              " </td>" & _
                                              "</tr>"
                            End If
                        End With
                    Next

                    isiTable &= "       <tr >" & _
                                "         <td style=""vertical-align :bottom;border-right:1px black solid;border-bottom:1px black solid;border-left:1px black solid;"">" & _
                                "         </td>" & _
                                "         <td style=""vertical-align :top;border-right:1px black solid;border-bottom:1px black solid;"">" & _
                                "           &nbsp;" & _
                                "         </td>" & _
                                "         <td style=""vertical-align :top;border-right:1px black solid;border-bottom:1px black solid;"">" & _
                                "           &nbsp;" & _
                                "         </td>" & _
                                "       </tr>" & _
                                "       </table>"

                End If
            End If


        Catch ex As Exception
            Throw New Exception("Error isiOver function :" & ex.ToString)
        End Try

        Return isiTable
    End Function

    Private Function quoTable(ByVal blid As String) As String
        Dim isiTable As String
        Dim cDT As DataTable
        Dim cDS As DataSet
        Dim cSt As String
        Dim namabarang As String
        Dim BanyakBarang As String
        Dim Ukuran As String
        Dim totalcont As Integer
        Dim totalcoll As Integer
        Dim borderbotton As String
        Dim banyakbast As Integer
        Dim keterangan As String = ""
        Dim JenisSatuan As String = ""
        Dim Ket As String = ""
        Dim flg As Integer = 0

        Try
            isiTable = tableHeader()
            'sqlstring = " select mbd.IDDetail as ID,mb.Penerima,mc.Jenis_Perusahaan + ' ' + mc.Nama_Customer as Nama_Customer,wd.Container, wd.Nama_Barang,mbd.PackedContID, " & _
            '            " mbd.Quantity,mb.Kapal,wd.Nama_Satuan,wd.Keterangan,mk.Nama_Kapal,mb.Kapal,(wd.Berat * mbd.Quantity) as totalberat , " & _
            '            " (wd.Panjang * wd.Lebar * wd.Tinggi * mbd.Quantity) as totalukuran from MuatBarang mb " & _
            '            " left join MuatBarangDetail mbd on mb.Mb_No = mbd.Mb_No" & _
            '            "  left join V_Warehouse_Satuan wd on ( mbd.Warehouse_Id = wd.IDDetail and wd.WarehouseItem_Code = mb.WarehouseHeaderID ) " & _
            '            " left join Kapal mk on mb.Kapal = mk.IDDetail " & _
            '            " left join MasterCustomer mc on mb.Customer_Id = mc.Kode_Customer " & _
            '            " where (mb.status = 5 or mb.status=7) and (mbd.status = 5 or mbd.status=7) And mb.Mb_No = '" & blid.ToString & "' and mk.status = 1 and mc.status = 1 and (wd.status = 1 or wd.status =7)"

            'sqlstring = " select wd.Nama_Barang,wd.Container,wd.Quantity,wd.Panjang,wd.Lebar,wd.Tinggi,wd.Berat from BillLand bl join MuatBarangReport mbr on bl.MBR_ID = mbr.ID join " & _
            '      " MuatBarang mb on mbr.Mb_Id =mb.ID join MuatBarangDetail mbd on mb.Mb_No = mbd.Mb_No join WarehouseDetail wd " & _
            '      " on mbd.Warehouse_Id = wd.ID where bl.status = 1 and mbr.status = 1 and mb.status = 1 and mbd.status = 1 and wd.status = 1 and bl.ID = '" & blid.ToString & "' "

            sqlstring = "select A.Penerima, A.Nama_Customer, A.Container, A.Nama_Barang, SUM(A.Quantity) as Quantity, " & _
                        " A.Kapal, A.Nama_Satuan, A.Keterangan, A.Nama_kapal, SUM(totalberat) as totalberat, Sum(A.totalukuran) as totalukuran, " & _
                        " A.NamaHarga, A.panjang, A.lebar, A.tinggi " & _
                        " FROM " & _
                        " (select mbd.IDDetail as ID,mb.Penerima,mc.Jenis_Perusahaan + ' ' + mc.Nama_Customer as Nama_Customer, " & _
                        " wd.Container, wd.Nama_Barang,  mbd.Quantity as Quantity,wd.Nama_Satuan, " & _
                        " wd.Keterangan,mk.Nama_Kapal,mb.Kapal,(wd.Berat * mbd.Quantity) as totalberat , " & _
                        " cast((wd.Panjang * wd.Lebar * wd.Tinggi * mbd.Quantity ) as decimal (20,8)) as totalukuran,mhd.NamaHarga, " & _
                        " wd.Panjang, wd.Lebar, wd.Tinggi from MuatBarang mb  " & _
                        " left join MuatBarangDetail mbd  on mb.Mb_No = mbd.Mb_No  " & _
                        " left join V_Warehouse_Satuan wd on (mbd.Warehouse_Id = wd.IDDetail and wd.WarehouseItem_Code = mb.WarehouseHeaderID )  " & _
                        " left join Kapal mk on mb.Kapal = mk.IDDetail  left join MasterCustomer mc on mb.Customer_Id = mc.Kode_Customer  " & _
                        " left join QuotationDetail qd on qd.Quotation_No = wd.Quotation_No and wd.QuotationDetailID = qd.IDDetail  " & _
                        " left join MasterHargaDefault mhd on mhd.ID = qd.SatuanID  " & _
                        " where (mb.status = 2 or mb.status=7 or mb.status=5) and " & _
                        " (mbd.status = 2 or mbd.status=7 or mb.status=5) " & _
                        " and mc.status = 1 and (wd.status = 1 or wd.status =7) " & _
                        " and mk.status = 1 " & _
                        " and mhd.[status] = 1 and mb.Mb_No = '" & blid.ToString & "' ) as A " & _
                        " Group by A.Penerima, A.Nama_Customer, " & _
                        " A.Container, A.Nama_Barang, A.Kapal,A.Nama_Satuan, " & _
                        " A.Keterangan,A.Nama_Kapal,A.NamaHarga, " & _
                        " A.Panjang, A.Lebar, A.Tinggi "

            DS = SQLExecuteQuery(sqlstring)
            DT = DS.Tables(0)

            Session("BastQuery") = DT

            totalcont = 0
            totalcoll = 0

            If DT.Rows.Count > 0 Then

                If DT.Rows.Count > 15 Then
                    Session("OverBAST") = "True"
                    banyakbast = 0
                    namabarang = ""
                    For i As Integer = 0 To DT.Rows.Count - 1
                        With DT.Rows(i)
                            banyakbast = banyakbast + CInt(.Item("Quantity").ToString)
                            If i = 0 Then
                                namabarang = .Item("Nama_Barang").ToString & ", "
                            ElseIf i < 3 Then
                                namabarang = .Item("Nama_Barang").ToString & ", "
                            ElseIf i = 3 Then
                                namabarang &= namabarang + .Item("Nama_Barang").ToString + " dll"
                            End If

                            JenisSatuan &= .Item("NamaHarga").ToString.ToUpper + ";"
                        End With


                    Next
                    borderbotton = "border-bottom:1px black solid;"
                    isiTable &= " <tr style=""height:10px;""> " & _
                                  " <td align=left style=""border-right:1px black solid;"" > " & _
                                  "   " & banyakbast.ToString & _
                                  " </td>" & _
                                  " <td align=left style=""border-right:1px black solid;"" > " & _
                                  "   " & namabarang.ToString & _
                                  " </td>" & _
                                  " <td align=left style=""border-right:1px black solid;"" > " & _
                                  "   " & _
                                  " </td>" & _
                                  "</tr>"
                Else



                    For i As Integer = 0 To DT.Rows.Count - 1
                        namabarang = ""
                        BanyakBarang = ""
                        Ukuran = ""
                        borderbotton = ""
                        With DT.Rows(i)
                            If .Item("Container") = "true" Or .Item("Container").ToString = "kubikasi" Then
                                cSt = "Select ch.NoKontainer, cd.NamaBarang,cd.Qty,mso.Nama_Satuan from ContainerDetail cd join MasterSatuanOther mso on cd.SatuanID = mso.IDDetail JOIN Containerheader ch on ch.ContainerCode = cd.ContainerCode " & _
                                      "where cd.ContainerCode = '" & .Item("Nama_Barang").ToString & "' and cd.status <> 0 and mso.status = 1"
                                cDS = SQLExecuteQuery(cSt)
                                cDT = cDS.Tables(0)
                                If cDT.Rows.Count > 0 Then
                                    For e As Integer = 0 To cDT.Rows.Count - 1
                                        If e > 4 Then
                                            namabarang &= "dll."
                                            Exit For
                                        End If
                                        If e = cDT.Rows.Count - 1 Then
                                            namabarang &= cDT.Rows(e).Item("NamaBarang").ToString + "." & " (" & cDT.Rows(e).Item("NoKontainer").ToString & ")"
                                        Else
                                            namabarang &= cDT.Rows(e).Item("NamaBarang").ToString + " ,"
                                        End If
                                    Next
                                End If

                                If .Item("Container").ToString = "true" Then
                                    BanyakBarang = .Item("Quantity").ToString + " Container"
                                    totalcont = totalcont + CInt(.Item("Quantity").ToString)
                                    keterangan = .Item("Keterangan").ToString
                                Else
                                    BanyakBarang = .Item("Quantity").ToString + " Colly"
                                    totalcoll = totalcoll + CInt(.Item("Quantity").ToString)
                                    keterangan = .Item("Keterangan").ToString
                                End If
                            Else
                                namabarang &= .Item("Nama_Barang").ToString
                                BanyakBarang = Cek_Data(Format(CDbl(.Item("Quantity").ToString), "##,###,###,##").ToString) + " " + .Item("Nama_Satuan").ToString
                                Ukuran = Cek_Data(Format(CDbl(.Item("totalukuran").ToString), "##,###,###,##").ToString)
                                keterangan = .Item("Keterangan").ToString
                                totalcoll = totalcoll + CInt(.Item("Quantity").ToString)
                            End If
                            If i = DT.Rows.Count - 1 Then
                                borderbotton = "border-bottom:1px black solid;"
                            End If
                            If i = DT.Rows.Count - 1 Then
                                isiTable &= " <tr style=""height:8px;""> " & _
                                          " <td align=left style=""border-right:1px black solid;"" > " & _
                                          "   " & BanyakBarang.ToString & _
                                          " </td>" & _
                                          " <td align=left style=""border-right:1px black solid;"" > " & _
                                          "   " & namabarang.ToString & _
                                          " </td>" & _
                                          " <td align=left style=""border-right:1px black solid;"" > " & _
                                          " " & keterangan & "  " & _
                                          " </td>" & _
                                          "</tr>"
                            Else
                                isiTable &= " <tr style=""height:8px;""> " & _
                                              " <td align=left style=""border-right:1px black solid;"" > " & _
                                              "   " & BanyakBarang.ToString & _
                                              " </td>" & _
                                              " <td align=left style=""border-right:1px black solid;"" > " & _
                                              "   " & namabarang.ToString & _
                                              " </td>" & _
                                              " <td align=left style=""border-right:1px black solid;"" > " & _
                                              "  " & keterangan & " " & _
                                              " </td>" & _
                                              "</tr>"
                            End If

                            JenisSatuan &= .Item("NamaHarga").ToString.ToUpper + ";"

                        End With

                    Next
                End If


            End If

            If JenisSatuan.ToString.ToUpper.Contains("CONTAINER") And (JenisSatuan.ToString.ToUpper.Contains("KUBIK") Or JenisSatuan.ToString.ToUpper.Contains("TON") Or JenisSatuan.ToString.ToUpper.Contains("SATUAN")) Then
                Ket = totalcont.ToString + " Container  <br />" + Cek_Data(Format(CDbl(totalcoll.ToString), "##,###,###,##").ToString) + " Colli"
            ElseIf JenisSatuan.ToString.ToUpper.Contains("CONTAINER") Then
                Ket = totalcont.ToString + " Container  <br />"
            ElseIf JenisSatuan.ToString.ToUpper.Contains("KUBIK") Or JenisSatuan.ToString.ToUpper.Contains("TON") Or JenisSatuan.ToString.ToUpper.Contains("SATUAN") Then
                Ket = Cek_Data(Format(CDbl(totalcoll.ToString), "##,###,###,##").ToString) + " Colli"

            End If


            '" & borderbotton & "
            isiTable &= "       <tr >" & _
                        "         <td style=""vertical-align :bottom;border-right:1px black solid;border-bottom:1px black solid;"">" & _
                        "           " & Ket & _
                        "         </td>" & _
                        "         <td style=""vertical-align :top;border-right:1px black solid;border-bottom:1px black solid;"">" & _
                        "           &nbsp;" & _
                        "         </td>" & _
                        "         <td style=""vertical-align :top;border-right:1px black solid;border-bottom:1px black solid;"">" & _
                        "           &nbsp;" & _
                        "         </td>" & _
                        "       </tr>" & _
                        "       </table>"

        Catch ex As Exception
            Throw New Exception("Error Isi Table function : " & ex.ToString)
        End Try
        Return isiTable
    End Function
    Private Function quoKonfHeaderHist() As String
        Dim header As String
        Dim sesHeader As String
        Dim STR As String
        Dim iDT As DataTable
        Dim hDS As DataSet
        Dim NamaPerusahaan As String
        sesHeader = ddlHeaderHist.SelectedValue
        STR = "Select * from HeaderForm where ID = '" & sesHeader & "' and status = 1;"
        hDS = SQLExecuteQuery(STR)
        iDT = hDS.Tables(0)
        NamaPerusahaan = iDT.Rows(0).Item("Nama").ToString
        header = "<table width=772px bgcolor=white cellpadding=0 cellspacing=0 style=""font-family:verdana;font-size:12px; "">" & _
             " <tr>" & _
             "   <td style=""width:60px;vertical-align:top;"" colspan=2 align=center  >" & _
             "      <img src=""" & iDT.Rows(0).Item("PathLogo").ToString & """ style=""height: 100px; width: 100px"" />" & _
             "   </td>" & _
             "   <td style=""width:610px;vertical-align:top;#2c3848;"" colspan=2 align=center >" & _
             "      <table width=610px bgcolor=white cellpadding=0 cellspacing=0 style=""font-family:verdana;font-size:10px;"">" & _
             " <tr>" & _
             "   <td style=""width:610px;vertical-align:top;"" colspan=2 align=center>" & _
             "     <font size=""4""><b>" & iDT.Rows(0).Item("Jenis").ToString & "</b> </font>" & _
             "   </td>" & _
             " </tr>"
        If NamaPerusahaan = "PT. Ligita Jaya" Or NamaPerusahaan = "PT.Ligita Jaya" Then
            header &= "             <tr>" & _
                                            "                 <td colspan=2 align=center>" & _
                                            "                     <font size=""6""><b>" & iDT.Rows(0).Item("Nama").ToString & "</b>" & _
                                            "                 </td>" & _
                                            "             </tr>"
        Else
            header &= "             <tr>" & _
                                            "                 <td colspan=2 align=center>" & _
                                            "                     <font size=""5""><b>" & iDT.Rows(0).Item("Nama").ToString & "</b>" & _
                                            "                 </td>" & _
                                            "             </tr>"
        End If
        header &= "         <tr>" & _
             "            <td style=""vertical-align:top"" colspan=2 align=center>" & _
             "              <font size=""2"" > <b>" & iDT.Rows(0).Item("Alamat").ToString & " </b><font> " & _
             "            </td>" & _
             "         </tr>" & _
             "  <tr>" & _
             "      <td style=""vertical-align:top"" colspan=2 align=center>" & _
             "          <font size=""2"" > <b>TELP: " & iDT.Rows(0).Item("No_Telp1").ToString & " , " & iDT.Rows(0).Item("No_Telp2").ToString & "</b><font> " & _
             "      </td>" & _
             "  </tr>" & _
             "  <tr>" & _
             "      <td style=""vertical-align:top"" colspan=2 align=center>" & _
             "              <font size=""2"" > <b>E-mail: " & iDT.Rows(0).Item("Email").ToString & " </b><font> " & _
             "      </td>" & _
             "  </tr>" & _
             "      </table>" & _
             "   </td>" & _
             " </tr>" & _
             " </table>" & _
             " <table width=772px bgcolor=white cellpadding=0 cellspacing=0 style=""font-family:verdana;font-size:10px;"">" & _
             "  <tr>"
        If iDT.Rows(0).Item("NO_Angta_INSA").ToString = "" Then
            header &= "      <td style=""vertical-align:top;border-bottom:double thick #2c3848;"" colspan=2 align=right>" & _
                                 "              <font size=""2"" > Nomor Anggota INSA: <b> --- </b><font> " & _
                                 "      </td>"
        Else
            header &= "      <td style=""vertical-align:top;border-bottom:double thick #2c3848;"" colspan=2 align=right>" & _
                               "              <font size=""2"" > Nomor Anggota INSA: <b>" & iDT.Rows(0).Item("NO_Angta_INSA").ToString & " </b><font> " & _
                               "      </td>"
        End If

        header &= "  </tr>" & _
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
                            " </br>"
        Return header
    End Function
    Private Function quoKonfHeader() As String
        Dim header As String
        Dim sesHeader As String
        Dim STR As String
        Dim iDT As DataTable
        Dim hDS As DataSet
        Dim NamaPerusahaan As String
        sesHeader = ddlHeader.SelectedValue
        STR = "Select * from HeaderForm where ID = '" & sesHeader & "' and status = 1;"
        hDS = SQLExecuteQuery(STR)
        iDT = hDS.Tables(0)
        NamaPerusahaan = iDT.Rows(0).Item("Nama").ToString
        header = "<table width=772px bgcolor=white cellpadding=0 cellspacing=0 style=""font-family:verdana;font-size:12px; "">" & _
             " <tr>" & _
             "   <td style=""width:60px;vertical-align:top;"" colspan=2 align=center  >" & _
             "      <img src=""" & iDT.Rows(0).Item("PathLogo").ToString & """ style=""height: 100px; width: 100px"" />" & _
             "   </td>" & _
             "   <td style=""width:610px;vertical-align:top;#2c3848;"" colspan=2 align=center >" & _
             "      <table width=610px bgcolor=white cellpadding=0 cellspacing=0 style=""font-family:verdana;font-size:10px;"">" & _
             " <tr>" & _
             "   <td style=""width:610px;vertical-align:top;"" colspan=2 align=center>" & _
             "     <font size=""4""><b>" & iDT.Rows(0).Item("Jenis").ToString & "</b> </font>" & _
             "   </td>" & _
             " </tr>"
        If NamaPerusahaan = "PT. Ligita Jaya" Or NamaPerusahaan = "PT.Ligita Jaya" Then
            header &= "             <tr>" & _
                                            "                 <td colspan=2 align=center>" & _
                                            "                     <font size=""6""><b>" & iDT.Rows(0).Item("Nama").ToString & "</b>" & _
                                            "                 </td>" & _
                                            "             </tr>"
        Else
            header &= "             <tr>" & _
                                            "                 <td colspan=2 align=center>" & _
                                            "                     <font size=""4""><b>" & iDT.Rows(0).Item("Nama").ToString & "</b>" & _
                                            "                 </td>" & _
                                            "             </tr>"
        End If
        header &= "         <tr>" & _
                             "            <td style=""vertical-align:top"" colspan=2 align=center>" & _
                             "              <font size=""2"" > <b>" & iDT.Rows(0).Item("Alamat").ToString & " </b><font> " & _
                             "            </td>" & _
                             "         </tr>" & _
                             "  <tr>" & _
                             "      <td style=""vertical-align:top"" colspan=2 align=center>" & _
                             "          <font size=""2"" > <b>TELP: " & iDT.Rows(0).Item("No_Telp1").ToString & " , " & iDT.Rows(0).Item("No_Telp2").ToString & "</b><font> " & _
                             "      </td>" & _
                             "  </tr>" & _
                             "  <tr>" & _
                             "      <td style=""vertical-align:top"" colspan=2 align=center>" & _
                             "              <font size=""2"" > <b>E-mail: " & iDT.Rows(0).Item("Email").ToString & " </b><font> " & _
                             "      </td>" & _
                             "  </tr>" & _
                             "      </table>" & _
                             "   </td>" & _
                             " </tr>" & _
                             " </table>" & _
                             " <table width=772px bgcolor=white cellpadding=0 cellspacing=0 style=""font-family:verdana;font-size:10px;"">" & _
                             "  <tr>"
        If iDT.Rows(0).Item("NO_Angta_INSA").ToString = "" Then
            header &= "      <td style=""vertical-align:top;border-bottom:double thick #2c3848;"" colspan=2 align=right>" & _
                                 "              <font size=""2"" > Nomor Anggota INSA: <b> --- </b><font> " & _
                                 "      </td>"
        Else
            header &= "      <td style=""vertical-align:top;border-bottom:double thick #2c3848;"" colspan=2 align=right>" & _
                               "              <font size=""2"" > Nomor Anggota INSA: <b>" & iDT.Rows(0).Item("NO_Angta_INSA").ToString & " </b><font> " & _
                               "      </td>"
        End If

        header &= "  </tr>" & _
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
                             " </br>"
        Return header
    End Function

    Private Function quotationHeaderHist(ByVal blid As String) As String
        Dim HeaderReport As String
        Dim Tanggal As Date
        Dim tanggalliat As String
        Dim bulan As String
        Dim hari As String
        Dim year As String
        Dim sqlstring As String
        Dim ds As DataSet
        Dim dt As DataTable
        Dim bast As String
        Dim idkapal As String


        sqlstring = " select mt.Pelabuhan as Tujuan,mb.Penerima,mc.Jenis_Perusahaan + ' ' + mc.Nama_Customer as Nama_Customer,mb.Kapal,mk.Nama_Kapal,mk.Nahkoda_Kapal,mc.Area, bt.Tanggal from MuatBarang mb " & _
                    " left join WarehouseHeader wh on mb.WarehouseHeaderID = wh.WarehouseItem_Code " & _
                    "  left join MasterQuotation mq on wh.Quotation_No = mq.Quotation_No " & _
                    " left join MasterCustomer mc on mb.Customer_Id = mc.Kode_Customer " & _
                    " left join Kapal mk on mb.Kapal = mk.IDDetail " & _
                    " left join MasterTujuan mt on mt.Tujuan = mq.Tujuan " & _
                    " left join BAST bt on mb.Mb_No = bt.MB_ID " & _
                    " where mb.Mb_No = '" & blid.ToString & "'  " & _
                    " and (mb.status = 5 or mb.status=7 or mb.status=2) and mk.status = 1 and (wh.status = 1 or wh.status = 7) and mt.status = 1 and mq.status <> 0 and mc.status = 1"
        ds = SQLExecuteQuery(sqlstring)
        dt = ds.Tables(0)
        idkapal = dt.Rows(0).Item("Kapal").ToString

        Tanggal = CDate(dt.Rows(0).Item("Tanggal").ToString)
        bulan = CDate(Tanggal).ToString("MM")
        tanggalliat = Tanggal.ToString("dd") + " " + CekBulan(bulan, 2) + " " + Tanggal.ToString("yyyy")
        hari = Tanggal.ToString("dddd")
        year = CDate(Tanggal).ToString("yyyy")

        bast = hfBastNo.Value
        Session("bastno") = bast.ToString

        HeaderReport = " <Table width=772px><tr style=""height:180px""><td></td></tr></table>"
        HeaderReport &= "<table width=772px bgcolor=white cellpadding=0 cellspacing=0 style=""font-family:verdana;font-size:12px;position:relative"">" & _
              " <tr>" & _
              "   <td colspan=2 align=center>" & _
              "     <b><u><font size=""3"">BERITA ACARA SERAH TERIMA</font> </u></b>" & _
              "   </td>" & _
              " </tr>" & _
              " </br>" & _
              " <tr>" & _
              "   <td colspan=2 align=center>" & _
              "     No.    <b> " & bast.ToString & "</b>" & _
              "   </td>" & _
              " </tr>" & _
              " <tr>" & _
              "</table> <br />" & _
              "<table width=772px bgcolor=white cellpadding=0 cellspacing=0 style=""font-family:verdana;font-size:12px;position:relative"">" & _
              "   <td colspan=2 align=left>" & _
              "      Pada hari ini, Hari <b> " & CekHari(hari, 0) & " " & _
              "   </td>" & _
              "   <td colspan=2 align=left>" & _
              "      Tanggal <b>" & tanggalliat & "</b>" & _
              "   </td>" & _
              "   <td colspan=2 align=left>" & _
              "      &nbsp; " & _
              "   </td>" & _
              " </tr>" & _
              " <tr>" & _
              "   <td colspan=2 align=left>" & _
              "      telah diserahkan kepada  " & _
              "   </td>" & _
              "   <td colspan=2 align=left>" & _
              "      : " & TukarNamaPerusahaan(dt.Rows(0).Item("Penerima").ToString) & " " & _
              "   </td>" & _
              "   <td colspan=2 align=left>" & _
              "      Di " & TxtKotaTujuanHis.Text.ToString & " " & _
              "   </td>" & _
              " </tr>" & _
              " <tr>" & _
              "   <td colspan=2 align=left>" & _
              "      Pengirim  " & _
              "   </td>" & _
              "   <td colspan=2 align=left>" & _
              "      : " & TukarNamaPerusahaan(dt.Rows(0).Item("Nama_Customer").ToString) & " " & _
              "   </td>" & _
              "   <td colspan=2 align=left>" & _
              "      Di " & dt.Rows(0).Item("Area").ToString & " " & _
              "   </td>" & _
              " </tr>" & _
              " </table> " & _
              "<BR />"

        HeaderReport &= quoTable(blid.ToString)

        HeaderReport &= "<table width=772px bgcolor=white cellpadding=0 cellspacing=0 style=""font-family:verdana;font-size:12px;position:relative"">" & _
                        "<tr>" & _
                        "   <td style=""width:122px;vertical-align:top;height:25px;"" align=""left"">" & _
                        "       dibongkar dari   " & _
                        "   </td>" & _
                        "   <td style=""width:650px;vertical-align:top;height:25px;"" align=""left""> " & _
                        "<b> " & dt.Rows(0).Item("Nama_Kapal").ToString & "</b> " & _
                        "   </td>" & _
                        " </tr> " & _
                        "<tr>" & _
                        "   <td style=""border-bottom:1px black solid;"" align=""left"" align=""left"">" & _
                        "       Tanggal    " & _
                        "   </td>" & _
                        "   <td style=""border-bottom:1px black solid;"" align=""left""> " & _
                        " " & tanggalliat & "" & _
                        "   </td>" & _
                        " </tr> " & _
                        "</table>"

        HeaderReport &= "<table width=772px bgcolor=white cellpadding=0 cellspacing=0 style="" height:150px; font-family:verdana;font-size:12px;position:relative"">" & _
                        "<tr>" & _
                        "   <td style=""width:122px;vertical-align:top;"" align=""left"">" & _
                        "       <u>KETERANGAN </u>" & _
                        "   </td>" & _
                        "   <td style=""width:650px;vertical-align:top;"" align=""left""> " & _
                        "    :   " & _
                        "   </td>" & _
                        " </tr> " & _
                        "<tr>" & _
                        "   <td style =""border-bottom:1px black solid;"">" & _
                        "   </td>" & _
                        "   <td style =""border-bottom:1px black solid;""> " & _
                        "   </td>" & _
                        " </tr> " & _
                        "</table>"

        HeaderReport &= "<table width=772px bgcolor=white cellpadding=0 cellspacing=0 style=""font-family:verdana;font-size:12px;position:relative"">" & _
                        "<tr>" & _
                        "   <td align = left>" & _
                        "       Demikianlah BERITA ACARA SERAH TERIMA ini dibuat dengan sebenarnya untuk dapat dipergunakan semestinya." & _
                        "   </td>" & _
                        "</tr>" & _
                        "<tr>" & _
                        "   <td align = right style=""padding-right:55px;"">" & _
                        "<BR />" & txtKotaBongkarHist.Text + ", " + tanggalliat & _
                        "   </td>" & _
                        "</tr>" & _
                        "</table>" & _
                        "<br />"

        HeaderReport &= "<table width=772px bgcolor=white cellpadding=0 cellspacing=0 style="" font-family:verdana;font-size:12px;position:relative"">" & _
                        "<tr style=""height:80px"">" & _
                        "   <td valign=top align = center >" & _
                        "       Yang Menerima" & _
                        "   </td>" & _
                        "   <td valign=top align = center >" & _
                        "       Yang Menyerahkan" & _
                        "   </td>" & _
                        "</tr> " & _
                        "<tr> " & _
                        "   <td valign=top align = center >" & _
                        "       (.....................................)" & _
                        "   </td>" & _
                        "   <td valign=top align = center >" & _
                        "       ( " & TxtMenyerahkanHist.Text & " )" & _
                        "   </td>" & _
                        "</table>"

        If Session("OverBAST") = "True" Then
            HeaderReport &= isiOver(blid)
        End If

        Return HeaderReport
    End Function

    Private Function quotationHeader(ByVal blid As String) As String
        Dim HeaderReport As String
        Dim Tanggal As Date
        Dim QuoNo As String
        Dim tanggalliat As String
        Dim bulan As String
        Dim hari As String
        Dim year As String
        Dim sqlstring As String
        Dim ds As DataSet
        Dim dt As DataTable
        Dim bast As String
        Dim idkapal As String
        'sqlstring = " select Tujuan , Nama_Customer, Kapal, Nahkoda, Penerima from BillLand bl "
        QuoNo = Session("Quotation_No")
        Tanggal = CDate(DtBongkar.Text)
        bulan = CDate(Tanggal).ToString("MM")
        tanggalliat = Tanggal.ToString("dd") + " " + CekBulan(bulan, 2) + " " + Tanggal.ToString("yyyy")
        hari = Tanggal.ToString("dddd")
        year = CDate(Tanggal).ToString("yyyy")

        sqlstring = " select mt.Pelabuhan as Tujuan,mb.Penerima,mc.Jenis_Perusahaan + ' ' + mc.Nama_Customer as Nama_Customer,mb.Kapal,mk.Nama_Kapal,mk.Nahkoda_Kapal,mc.Area from MuatBarang mb " & _
                            " left join WarehouseHeader wh on mb.WarehouseHeaderID = wh.WarehouseItem_Code " & _
                            " left join MasterQuotation mq on wh.Quotation_No = mq.Quotation_No  " & _
                            " left join MasterCustomer mc on mb.Customer_Id = mc.Kode_Customer " & _
                            " left join Kapal mk on mb.Kapal = mk.IDDetail " & _
                            " left join MasterTujuan mt on mt.Tujuan = mq.Tujuan " & _
                            " where mb.Mb_No = '" & blid.ToString & "' " & _
                            " and (mb.status = 2 or mb.status=7 or mb.status=5) and mk.status = 1 and (wh.status = 1 or wh.status = 7) and mt.status = 1 and mq.status <> 0 "
        ds = SQLExecuteQuery(sqlstring)
        dt = ds.Tables(0)
        idkapal = dt.Rows(0).Item("Kapal").ToString
        bast = loadbastno(idkapal)
        Session("bastno") = bast.ToString

        HeaderReport = " <Table width=772px><tr style=""height:180px""><td></td></tr></table>"
        HeaderReport &= "<table width=772px bgcolor=white cellpadding=0 cellspacing=0 style=""font-family:verdana;font-size:12px;position:relative"">" & _
              " <tr>" & _
              "   <td colspan=2 align=center>" & _
              "     <b><u>BERITA ACARA SERAH TERIMA </u></b>" & _
              "   </td>" & _
              " </tr>" & _
              " </br>" & _
              " <tr>" & _
              "   <td colspan=2 align=center>" & _
              "     No.    <b> " & bast.ToString & "</b>" & _
              "   </td>" & _
              " </tr>" & _
              " <tr>" & _
              "</table>" & _
              "<table width=772px bgcolor=white cellpadding=0 cellspacing=0 style=""font-family:verdana;font-size:12px;position:relative"">" & _
              "   <td colspan=2 align=left>" & _
              "      Pada hari ini, Hari <b> " & CekHari(hari, 0) & " " & _
              "   </td>" & _
              "   <td colspan=2 align=left>" & _
              "      Tanggal <b>" & tanggalliat & "</b>" & _
              "   </td>" & _
              "   <td colspan=2 align=left>" & _
              "      &nbsp; " & _
              "   </td>" & _
              " </tr>" & _
              " <tr>" & _
              "   <td colspan=2 align=left>" & _
              "      telah diserahkan kepada  " & _
              "   </td>" & _
              "   <td colspan=2 align=left>" & _
              "      :" & TukarNamaPerusahaan(dt.Rows(0).Item("Penerima").ToString) & " " & _
              "   </td>" & _
              "   <td colspan=2 align=left>" & _
              "      DI " & TxtKotaTujuan.Text.ToString & " " & _
              "   </td>" & _
              " </tr>" & _
              " <tr>" & _
              "   <td colspan=2 align=left>" & _
              "      Pengirim  " & _
              "   </td>" & _
              "   <td colspan=2 align=left>" & _
              "      : " & TukarNamaPerusahaan(dt.Rows(0).Item("Nama_Customer").ToString) & " " & _
              "   </td>" & _
              "   <td colspan=2 align=left>" & _
              "      DI " & dt.Rows(0).Item("Area").ToString & " " & _
              "   </td>" & _
              " </tr>" & _
              " </table>"

        HeaderReport &= quoTable(blid.ToString)

        HeaderReport &= "<table width=772px bgcolor=white cellpadding=0 cellspacing=0 style=""font-family:verdana;font-size:12px;position:relative"">" & _
                        "<tr>" & _
                        "   <td style=""width:122px;vertical-align:top;height:25px;"" align=""left"">" & _
                        "       dibongkar dari    " & _
                        "   </td>" & _
                        "   <td style=""width:650px;vertical-align:top;height:25px;"" align=""left""> " & _
                        "<b> " & dt.Rows(0).Item("Nama_Kapal").ToString & "</b> " & _
                        "   </td>" & _
                        " </tr> " & _
                        "<tr>" & _
                        "   <td style=""border-bottom:2px black solid;"" align=""left"">" & _
                        "       Tanggal    " & _
                        "   </td>" & _
                        "   <td style=""border-bottom:2px black solid;"" align=""left""> " & _
                        " " & tanggalliat & "" & _
                        "   </td>" & _
                        " </tr> " & _
                        "</table>"

        HeaderReport &= "<table width=772px bgcolor=white cellpadding=0 cellspacing=0 style="" height:150px; font-family:verdana;font-size:12px;position:relative"">" & _
                        "<tr>" & _
                        "   <td style=""width:122px;vertical-align:top;"" align=""left"">" & _
                        "       <u>KETERANGAN </u>" & _
                        "   </td>" & _
                        "   <td style=""width:650px;vertical-align:top;"" align=""left""> " & _
                        "    :   " & _
                        "   </td>" & _
                        " </tr> " & _
                        "<tr>" & _
                        "   <td style =""border-bottom:1px black solid;"">" & _
                        "   </td>" & _
                        "   <td style =""border-bottom:1px black solid;""> " & _
                        "   </td>" & _
                        " </tr> " & _
                        "</table>"

        HeaderReport &= "<table width=772px bgcolor=white cellpadding=0 cellspacing=0 style=""font-family:verdana;font-size:12px;position:relative"">" & _
                        "<tr>" & _
                        "   <td align = left>" & _
                        "       Demikianlah BERITA ACARA SERAH TERIMA ini dibuat dengan sebenarnya untuk dapat dipergunakan semestinya." & _
                        "   </td>" & _
                        "</tr>" & _
                        "<tr>" & _
                        "   <td align = right style=""padding-right:55px;"">" & _
                        "<BR />" & TxtKotaBongkar.Text + ", " + tanggalliat & _
                        "   </td>" & _
                        "</tr>" & _
                        "</table>" & _
                        "<br />"

        HeaderReport &= "<table width=772px bgcolor=white cellpadding=0 cellspacing=0 style="" font-family:verdana;font-size:12px;position:relative"">" & _
                        "<tr style=""height:80px"">" & _
                        "   <td valign=top align = center >" & _
                        "       Yang Menerima" & _
                        "   </td>" & _
                        "   <td valign=top align = center >" & _
                        "       Yang Menyerahkan" & _
                        "   </td>" & _
                        "</tr> " & _
                        "<tr> " & _
                        "   <td valign=top align = center >" & _
                        "       (.....................................)" & _
                        "   </td>" & _
                        "   <td valign=top align = center >" & _
                        "       ( " & TxtMenyerahkan.Text & " )" & _
                        "   </td>" & _
                        "</table>"

        If Session("OverBAST") = "True" Then
            HeaderReport &= isiOver(blid)
        End If

        Return HeaderReport
    End Function
    Private Sub insertbast(ByVal MBID As String, ByVal IDKAPAL As String, ByVal index As Integer)
        Dim bastno As String
        Dim str As String
        Dim ds As DataSet
        Dim dt As DataTable
        Dim cek As Boolean
        Try
            bastno = Session("bastno").ToString
            str = " select MB_ID from BAST where status = 1"
            ds = SQLExecuteQuery(str)
            dt = ds.Tables(0)
            cek = True
            If dt.Rows.Count > 0 Then
                For i As Integer = 0 To dt.Rows.Count - 1
                    If dt.Rows(i).Item("MB_ID").ToString = MBID.ToString Then
                        cek = False
                        Exit For
                    End If
                Next
            End If
            If cek Then
                sqlstring = "insert into BAST (BASTNO,Tanggal,MB_ID,Kapal,UserName,[status]) values(" & _
                        "'" & bastno.ToString & "' , '" & DtBongkar.Date & "', '" & MBID.ToString & "', '" & hfIDK.Value & "', " & _
                        "'" & Session("UserId") & "', 1)"
                result = SQLExecuteNonQuery(sqlstring)
                If result <> 0 Then
                    remove_item(index)
                    clear_insert()
                    clear_label()
                End If
            End If
        Catch ex As Exception
            Throw New Exception("Error function Insert <BR>: " & ex.ToString)
        End Try
    End Sub
    Private Sub clear_insert()
        Session("bastno") = ""
    End Sub
    Private Sub Clear()
        hfIDK.Value = ""
        ddlHeader.SelectedIndex = 0
    End Sub

    Private Sub clear_label()
        lblError.Text = ""
        lblerror2.Text = ""
    End Sub
#End Region

#Region "VALIDATION"

    Private Function validation_history() As Boolean
        clear_label()

        If hfIDKHIS.Value = "" Then
            lblerror2.Visible = True
            lblerror2.Text = " pilih kapal "
            Return False
        End If

        If tbFrom.Text.Trim = "" Or tbSampai.Text.Trim = "" Then
            lblerror2.Visible = True
            lblerror2.Text = "pilih tanggal "
            Return False
        End If
        Return True
    End Function

    Private Function validation_print() As Boolean
        clear_label()
        If TxtKotaTujuanHis.Text.Trim = "" Then
            lblerror2.Visible = True
            lblerror2.Text = "Isi Kota Tujuan"
            Return False
        End If

        If TxtMenyerahkanHist.Text = "" Then
            lblerror2.Visible = True
            lblerror2.Text = "Isi Nama yang Menyerahkan"
            Return False
        End If

        If txtKotaBongkarHist.Text = "" Then
            lblerror2.Visible = True
            lblerror2.Text = "Isi KOta Bongkar"
            Return False
        End If
        Return True
    End Function
    Private Function validation() As Boolean
        clear_label()
        If TxtKotaTujuan.Text.Trim = "" Then
            lblError.Visible = True
            lblError.Text = "Isi Kota Tujuan"
            Return False
        End If

        If DtBongkar.Text.Trim = "" Then
            lblError.Visible = True
            lblError.Text = "Pilih Tanggal Bongkar Kapal"
            Return False
        End If

        If TxtMenyerahkan.Text = "" Then
            lblerror2.Visible = True
            lblerror2.Text = "Isi Nama yang Menyerahkan"
            Return False
        End If

        If TxtKotaBongkar.Text = "" Then
            lblerror2.Visible = True
            lblerror2.Text = "Isi KOta Bongkar"
            Return False
        End If

        Return True
    End Function
    Private Function validation_add() As Boolean
        clear_label()
        If hfIDK.Value = "" Then
            lblError.Visible = True
            lblError.Text = " Harap Pilih Kapal untuk dicetak"
            Return False
        End If

        Return True
    End Function
#End Region

#Region "BUTTON"
    Protected Sub btKembaliDevPeriod_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btKembaliDevPeriod.Click
        'Panel_Input.Visible = True
        'Panel_Grid.Visible = True
        'Panel_Report.Visible = False
        'historygrid.Visible = False
        'historygrid.Visible = False

        Panel_Input.Visible = False
        Panel_Grid.Visible = False
        Panel_Report.Visible = False
        historygrid.Visible = True
        historyinput.Visible = True

        lblError.Visible = False
        lblError.Text = ""
        Session("OverBAST") = ""
        Session("BastQuery") = ""
    End Sub

    Protected Sub Back_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Back.Click
        Panel_Input.Visible = True
        Panel_Grid.Visible = True
        Panel_Report.Visible = False
        historygrid.Visible = False
        historyinput.Visible = False

    End Sub

    Private Sub btAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btAdd.Click
        If validation_add() Then
            TxtKapal.Text = hfnamakapal.Value
            load_kapal_parent()
        End If
    End Sub

    Private Sub btReset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btReset.Click
        Clear()
        clear_label()
        clear_insert()
    End Sub
    Private Sub btViewHistory_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btViewHistory.Click
        Panel_Input.Visible = False
        Panel_Grid.Visible = False
        Panel_Report.Visible = False
        historygrid.Visible = True
        historyinput.Visible = True
        clear_label()
        load_bast()
    End Sub

    Private Sub ViewGrid_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ViewGrid.Click
        If validation_history() Then
            TxtKapalhis.Text = hfnamakapal.Value
            load_bast()
        End If
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

            'ddlHeader.Items.Insert(0, "Please Select header")

        Catch ex As Exception
            Throw New Exception("Error load ddl header: " & ex.ToString)
        End Try
    End Sub

    Private Sub load_ddl_history()

        Try
            sqlstring = "select ID,Nama from HeaderForm where [status] = 1 and Nama LIKE '%" & Profil & "%'"
            DS = SQLExecuteQuery(sqlstring)
            DT = DS.Tables(0)

            With ddlHeaderHist
                .DataSource = DT
                .DataTextField = "Nama"
                .DataValueField = "ID"
                .DataBind()
            End With

            'ddlHeaderHist.Items.Insert(0, "Please Select header")

        Catch ex As Exception
            Throw New Exception("Error load ddl load kapal ;<BR> " & ex.ToString)
        End Try


    End Sub
#End Region

End Class