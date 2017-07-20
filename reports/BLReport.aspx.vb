Imports System.Data
Imports DevExpress.Web.ASPxGridView
Imports System.Data.SqlClient

'***********************************************************************************************************************************************
' TAG               Programmer      Purpose
' EH00_20111014_01  Eddy Handaya    Clean up SQL and Fix bug on Print BL.
'***********************************************************************************************************************************************

Partial Public Class BLReport
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
                Session("Grid_Kapal_Parent_BL") = Nothing
                Panel_Input.Visible = True
                Panel_Grid.Visible = True
                Panel_Report.Visible = False
                historygrid.Visible = False
                Panel_HistoryInput.Visible = False
                hfMode.Value = "Insert"
                load_ddlhis()
                load_kapal_parent()
                BLDate.Date = Today
                load_ddl()
                'create_session()
                Session("Grid_Kapal_Parent_BL") = Nothing
                Session("Grid_HistoryBL") = Nothing
            End If

            If Not Session("Grid_Kapal_Parent_BL") Is Nothing Then
                Grid_Kapal_Parent.DataSource = CType(Session("Grid_Kapal_Parent_BL"), DataTable)
                Grid_Kapal_Parent.DataBind()
            End If
            If Not Session("Grid_HistoryBL") Is Nothing Then
                Grid_Kapal_History.DataSource = CType(Session("Grid_HistoryBL"), DataTable)
                Grid_Kapal_History.DataBind()
            End If
        Catch ex As Exception
            Response.Write(ex.ToString)
        End Try

    End Sub

#End Region

#Region "GRID"
    Private Sub load_bl()
        Try

            '***** EH00_20111014_01 START *****
            'sqlstring = " SELECT bl.ID,mb.WarehouseHeaderID, mc.Jenis_Perusahaan + ' ' + mc.Nama_Customer as Nama_Customer,mb.Penerima,bl.Tanggal,mb.Mb_No,mb.Kapal, " & _
            '                    " mk.Nama_Kapal From BillLand bl,MuatBarang mb,MasterCustomer mc,Kapal mk " & _
            '                    " Where bl.MB_ID = mb.Mb_No and  mb.Customer_Id = mc.Kode_Customer and mb.Kapal = mk.IDDetail and mb.status <> 0 and " & _
            '                    "  mb.Kapal = '" & hfIDKHIS.Value & "' and mc.status = 1 and mk.status = 1 and bl.Tanggal Between '" & tbFrom.Text.ToString & "' AND '" & tbSampai.Text.ToString & "' order by mb.ID desc "
            sqlstring = " Select BL.ID ,BL.Tanggal ,MB.WarehouseHeaderID , mc.Jenis_Perusahaan + ' ' + mc.Nama_Customer as Nama_Customer ,MB.Penerima ,MB.Mb_No,MB.Kapal, K.Nama_Kapal " & _
                        " FROM  	BillLand BL  , MuatBarang MB , MasterCustomer MC , Kapal K " & _
                        " WHERE		BL.Status <> '0' " & _
                        " AND		BL.Tanggal BETWEEN '" & tbFrom.Text.ToString & "' AND '" & tbSampai.Text.ToString & "'  " & _
                        " AND		MB.Kapal = '" & hfIDKHIS.Value & "' " & _
                        " AND		MB.status <> '0' " & _
                        " AND		MB.Mb_No = BL.MB_ID " & _
                        " AND		K.status <> '0' " & _
                        " AND		K.IDDetail = MB.Kapal " & _
                        " AND		MC.status <> '0' " & _
                        " AND		MC.Kode_Customer = MB.Customer_Id " & _
                        " ORDER	BY  MB.ID Desc "
            '***** EH00_20111014_01 END   *****

            DS = SQLExecuteQuery(sqlstring)
            DT = DS.Tables(0)
            iDT.Columns.Add(New DataColumn("ID", GetType(String)))
            iDT.Columns.Add(New DataColumn("WarehouseHeaderID", GetType(String)))
            iDT.Columns.Add(New DataColumn("Penerima", GetType(String)))
            iDT.Columns.Add(New DataColumn("Nama_Customer", GetType(String)))
            iDT.Columns.Add(New DataColumn("Tanggal", GetType(DateTime)))
            iDT.Columns.Add(New DataColumn("Mb_No", GetType(String)))
            iDT.Columns.Add(New DataColumn("Kapal", GetType(String)))
            iDT.Columns.Add(New DataColumn("KapalID", GetType(String)))
            If DT.Rows.Count > 0 Then
                For i As Integer = 0 To DT.Rows.Count - 1
                    With DT.Rows(i)
                        DR = iDT.NewRow()
                        DR("ID") = .Item("ID")
                        DR("WarehouseHeaderID") = .Item("WarehouseHeaderID")
                        DR("Penerima") = TukarNamaPerusahaan(.Item("Penerima").ToString)
                        DR("Nama_Customer") = .Item("Nama_Customer")
                        DR("Tanggal") = CDate(.Item("Tanggal").ToString).ToString("MM/dd/yyyy")
                        DR("Mb_No") = .Item("Mb_No")
                        DR("Kapal") = .Item("Nama_Kapal")
                        DR("KapalID") = .Item("Kapal")
                        iDT.Rows.Add(DR)
                    End With
                Next
                Session("Grid_HistoryBL") = iDT
                Grid_Kapal_History.DataSource = iDT
                Grid_Kapal_History.KeyFieldName = "Mb_No"
                Grid_Kapal_History.DataBind()
            Else
                Grid_Kapal_History.DataSource = Nothing
                Grid_Kapal_History.DataBind()
            End If

        Catch ex As Exception
            Response.Write("load_bl Exception: <BR> " & ex.ToString)
        End Try
    End Sub
    Private Sub load_kapal_parent()
        Try
            'sqlstring = " SELECT Distinct mb.Mb_No as ID,mb.WarehouseHeaderID,mb.Customer_Id,mc.Jenis_Perusahaan + ' ' + mc.Nama_Customer as Nama_Customer,mb.Penerima,mb.Tanggal,mb.Mb_No, " & _
            '                    " mb.Kapal,mk.Nama_Kapal From MuatBarang mb,MasterCustomer mc,Kapal mk" & _
            '                    " Where mb.Customer_Id = mc.Kode_Customer and mb.Kapal = mk.IDDetail and mb.status = 2 and " & _
            '                     " mb.Mb_No not in ( select bt.MB_ID from BillLand bt where status = 1) and " & _
            '                     "  mb.Kapal = '" & hfIDK.Value & "' and mc.status = 1 and mk.status = 1 order by mb.Mb_No desc "

            sqlstring = " SELECT Distinct mb.Mb_No as ID,mb.WarehouseHeaderID,mb.Customer_Id,mc.Jenis_Perusahaan + ' ' + mc.Nama_Customer as Nama_Customer,mb.Penerima,mbr.Depart_Date as Tanggal ,mb.Mb_No, " & _
                               " mb.Kapal,mk.Nama_Kapal From MuatBarang mb,MasterCustomer mc,Kapal mk, MBRDetail mbr " & _
                               " Where mb.Customer_Id = mc.Kode_Customer and mb.Kapal = mk.IDDetail and (mb.status = 2 OR MB.status = 1)  and mb.Mb_No = mbr.Mb_No and  " & _
                                " mb.Mb_No not in ( select bt.MB_ID from BillLand bt where status = 1) and " & _
                                "  mb.Kapal = '" & hfIDK.Value & "' and mbr.Depart_Date = '" & BLDate.Date & "' and mc.status = 1 and mk.status = 1 order by mb.Mb_No desc "

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
                Session("Grid_Kapal_Parent_BL") = iDT
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

            sqlstring = " SELECT MBD.IDDetail as ID,MBD.Mb_No,MBD.PackedContID,WH.Nama_Barang,MBD.Quantity,WH.Container FROM MuatBarangDetail " & _
                        " MBD JOIN V_Warehouse_Satuan WH on (MBD.Warehouse_Id = WH.IDDetail and WH.WarehouseItem_Code = '" & grid.GetMasterRowFieldValues("WarehouseHeaderID") & "') " & _
                        " Where MBD.Mb_No = '" & grid.GetMasterRowKeyValue() & "' AND MBD.status =2 and WH.status <>0 Order by MBD.timestamp desc"

            DS = SQLExecuteQuery(sqlstring)
            DT = DS.Tables(0)
            If DT.Rows.Count > 0 Then
                For i As Integer = 0 To DT.Rows.Count - 1
                    DR = kDT.NewRow
                    With DT.Rows(i)
                        If .Item("Container").ToString = "true" Or .Item("Container").ToString = "kubikasi" Then
                            kSTR = "Select cd.NamaBarang,ch.NoKontainer as ContainerCode from ContainerDetail cd left join ContainerHeader ch on cd.ContainerCode = ch.ContainerCode where cd.ContainerCode = '" & .Item("Nama_Barang").ToString & "' and cd.status =1 "
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
                                DR("NoContainer") = .Item("PackedContID").ToString
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
    Protected Sub Grid_Kapal_Child_History_DataSelect(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Call load_kapal_child(TryCast(sender, ASPxGridView))
        Catch ex As Exception
            Response.Write("Error Load Grid Child DataSelect  :<BR>" & ex.ToString)
        End Try
    End Sub
    Protected Sub Grid_Kapal_Child_DataSelect(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Call load_kapal_child(TryCast(sender, ASPxGridView))
        Catch ex As Exception
            Response.Write("Error Load Grid Child DataSelect  :<BR>" & ex.ToString)
        End Try
    End Sub
    Private Sub Grid_Kapal_History_RowCommand(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewRowCommandEventArgs) Handles Grid_Kapal_History.RowCommand
        Try
            Select Case e.CommandArgs.CommandName
                Case "PrintBL"

                    hfIDK.Value = Grid_Kapal_History.GetRowValues(e.VisibleIndex, "Kapal").ToString
                    lblReport.Text = quoKonfHeader(Grid_Kapal_History.GetRowValues(e.VisibleIndex, "Mb_No").ToString)
                    lblReport.Text &= quotationHeader(Grid_Kapal_History.GetRowValues(e.VisibleIndex, "Mb_No").ToString)
                    Panel_Input.Visible = False
                    Panel_Grid.Visible = False
                    historygrid.Visible = False
                    Panel_HistoryInput.Visible = False
                    Panel_Report.Visible = True

            End Select
        Catch ex As Exception
            Throw New Exception("Error Grid History Row Command <BR>: " & ex.ToString)
        End Try
    End Sub
    Private Sub Grid_Kapal_Parent_RowCommand(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewRowCommandEventArgs) Handles Grid_Kapal_Parent.RowCommand
        Try
            lblError.Visible = False
            lblError.Text = ""
            Select Case e.CommandArgs.CommandName


                Case "PrintBL"

                    lblReport.Text = quoKonfHeader(Grid_Kapal_Parent.GetRowValues(e.VisibleIndex, "Mb_No").ToString)
                    lblReport.Text &= quotationHeader(Grid_Kapal_Parent.GetRowValues(e.VisibleIndex, "Mb_No").ToString)
                    insert_bl(Grid_Kapal_Parent.GetRowValues(e.VisibleIndex, "ID").ToString, hfIDK.Value, e.VisibleIndex)
                    Panel_Input.Visible = False
                    Panel_Grid.Visible = False
                    Panel_Report.Visible = True


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
    Private Sub insert_bl(ByVal MBID As String, ByVal IDKAPAL As String, ByVal index As Integer)
        Dim str As String
        Dim ds As DataSet
        Dim dt As DataTable
        Dim cek As Boolean
        Try
            str = " select ID,Mb_ID from BillLand where status = 1"
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
                sqlstring = "insert into BillLand (Tanggal,MB_ID,Kapal,UserName,[status]) values(" & _
                        " '" & BLDate.Text.ToString & "', '" & MBID.ToString & "', '" & hfIDK.Value & "', " & _
                        "'" & Session("UserId") & "', 1)"
                result = SQLExecuteNonQuery(sqlstring)
                If result > 0 Then
                    remove_item(index)
                    clear_label()
                End If
            End If
        Catch ex As Exception
            Throw New Exception("Error Function Insert_BL :" & ex.ToString)
        End Try

    End Sub
    Private Function NotaHeader() As String
        Dim HeaderReport As String

        HeaderReport = " <Table width=780px><tr style=""height:187px""><td></td></tr></table> " & _
                        "<Table width=780px><tr><td align = ""center"">LAMPIRAN</td></tr></table>" & _
                      "     <table width=780px cellpadding=4 cellspacing=0 style=""height:870px;font-family:verdana;font-size:11px;table-layout:fixed;"">" & _
                      "       <tr style=""height:10px;"">" & _
                      "         <td align=center style=""width: 40px;height:10px;vertical-align :top;border-top:1px black solid;border-right:1px black solid;border-left:1px black solid;border-bottom:1px black solid;"">" & _
                      "           <b>MERK</b>" & _
                      "         </td>" & _
                      "         <td align=center style=""width: 100px;vertical-align :top;height:20px;border-top:1px black solid;border-right:1px black solid;border-bottom:1px black solid;"">" & _
                      "           <b>BANYAKNYA</b>" & _
                      "         </td>" & _
                      "         <td align=center style=""width: 300px;vertical-align :top;height:20px;border-top:1px black solid;border-right:1px black solid;border-bottom:1px black solid;"">" & _
                      "           <b>JENIS BARANG</b>" & _
                      "         </td>" & _
                      "         <td align=center style=""width: 60px;vertical-align :top;height:20px;border-top:1px black solid;border-right:1px black solid;border-bottom:1px black solid;"">" & _
                      "           <b>M<sup>3</sup><br>" & _
                      "         </td>" & _
                      "         <td align=center style=""width: 60px;vertical-align :top;height:20px;border-top:1px black solid;border-right:1px black solid;border-bottom:1px black solid;"">" & _
                      "           <b>TON</b>" & _
                      "         </td>" & _
                      "       </tr>"

        Return HeaderReport
    End Function
    Private Function tableHeader() As String
        Dim HeaderReport As String

        HeaderReport = "     <table width=780px cellpadding=4 cellspacing=0 style=""height:520px;font-family:verdana;font-size:11px;table-layout:fixed;"">" & _
                      "       <tr style=""height:10px;"">" & _
                      "         <td align=center style=""width: 40px;height:10px;vertical-align :top;border-top:1px black solid;border-right:1px black solid;border-left:1px black solid;border-bottom:1px black solid;"">" & _
                      "           <b>MERK</b>" & _
                      "         </td>" & _
                      "         <td align=center style=""width: 100px;vertical-align :top;height:20px;border-top:1px black solid;border-right:1px black solid;border-bottom:1px black solid;"">" & _
                      "           <b>BANYAKNYA</b>" & _
                      "         </td>" & _
                      "         <td align=center style=""width: 300px;vertical-align :top;height:20px;border-top:1px black solid;border-right:1px black solid;border-bottom:1px black solid;"">" & _
                      "           <b>JENIS BARANG</b>" & _
                      "         </td>" & _
                      "         <td align=center style=""width: 60px;vertical-align :top;height:20px;border-top:1px black solid;border-right:1px black solid;border-bottom:1px black solid;"">" & _
                      "           <b>M<sup>3</sup><br>" & _
                      "         </td>" & _
                      "         <td align=center style=""width: 60px;vertical-align :top;height:20px;border-top:1px black solid;border-right:1px black solid;border-bottom:1px black solid;"">" & _
                      "           <b>TON</b>" & _
                      "         </td>" & _
                      "       </tr> "
        Return HeaderReport
    End Function

    Private Function tableHeaderOver() As String
        Dim HeaderReport As String

        HeaderReport = "   <table width=780px cellpadding=4 cellspacing=0 style=""height:560px;font-family:verdana;font-size:12px;table-layout:fixed;"">" & _
                      "       <tr style=""height:10px;"">" & _
                      "         <td align=center style=""width: 40px;height:10px;vertical-align :top;border-top:1px black solid;border-right:1px black solid;border-left:1px black solid;border-bottom:1px black solid;"">" & _
                      "           <b>MERK</b>" & _
                      "         </td>" & _
                      "         <td align=center style=""width: 100px;vertical-align :top;height:20px;border-top:1px black solid;border-right:1px black solid;border-bottom:1px black solid;"">" & _
                      "           <b>BANYAKNYA</b>" & _
                      "         </td>" & _
                      "         <td align=center style=""width: 300px;vertical-align :top;height:20px;border-top:1px black solid;border-right:1px black solid;border-bottom:1px black solid;"">" & _
                      "           <b>JENIS BARANG</b>" & _
                      "         </td>" & _
                      "         <td align=center style=""width: 60px;vertical-align :top;height:20px;border-top:1px black solid;border-right:1px black solid;border-bottom:1px black solid;"">" & _
                      "           <b>m<sup>3</sup><br>" & _
                      "         </td>" & _
                      "         <td align=center style=""width: 60px;vertical-align :top;height:20px;border-top:1px black solid;border-right:1px black solid;border-bottom:1px black solid;"">" & _
                      "           <b>TON</b>" & _
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
        Dim namasatuan As String = ""

        Try
            isiTable = ""
            Dim jumlahdata As Integer = 30

            'sqlstring = " select mbd.IDDetail as ID,mb.Penerima,mc.Jenis_Perusahaan + ' ' + mc.Nama_Customer as Nama_Customer,wd.Container, wd.Nama_Barang,mbd.PackedContID, " & _
            '            " mbd.Quantity,mb.Kapal,wd.Nama_Satuan,wd.Keterangan,mk.Nama_Kapal,mb.Kapal,(wd.Berat * mbd.Quantity) as totalberat , " & _
            '            " (wd.Panjang * wd.Lebar * wd.Tinggi * mbd.Quantity ) as totalukuran,mhd.NamaHarga, wd.Panjang, wd.Lebar, wd.Tinggi from MuatBarang mb " & _
            '            " left join MuatBarangDetail mbd  on mb.Mb_No = mbd.Mb_No " & _
            '            " left join V_Warehouse_Satuan wd on (mbd.Warehouse_Id = wd.IDDetail and wd.WarehouseItem_Code = mb.WarehouseHeaderID ) " & _
            '            " left join Kapal mk on mb.Kapal = mk.IDDetail " & _
            '            " left join MasterCustomer mc on mb.Customer_Id = mc.Kode_Customer " & _
            '            " left join QuotationDetail qd on qd.Quotation_No = wd.Quotation_No and wd.QuotationDetailID = qd.IDDetail " & _
            '            " left join MasterHargaDefault mhd on mhd.ID = qd.SatuanID " & _
            '            " where mb.status <> 0 And mbd.status <>0  and wd.status <>0 and mc.status = 1 and mk.status = 1 and qd.status <> 0 and mhd.[status] = 1 and mb.Mb_No = '" & blid.ToString & "' "

            sqlstring = "select A.Penerima, A.Nama_Customer, A.Container, A.Nama_Barang, SUM(A.Quantity) as Quantity, " & _
                        " A.Kapal, A.Nama_Satuan, A.Keterangan, A.Nama_kapal, SUM(totalberat) as totalberat, Sum(A.totalukuran) as totalukuran, " & _
                        " A.NamaHarga, A.panjang, A.lebar, A.tinggi " & _
                        " FROM " & _
                        " (select mbd.IDDetail as ID,mb.Penerima,mc.Jenis_Perusahaan + ' ' + mc.Nama_Customer as Nama_Customer, " & _
                        " wd.Container, wd.Nama_Barang,  mbd.Quantity as Quantity,wd.Nama_Satuan, " & _
                        " wd.Keterangan,mk.Nama_Kapal,mb.Kapal,(wd.Berat * mbd.Quantity) as totalberat , " & _
                        " (wd.Panjang * wd.Lebar * wd.Tinggi * mbd.Quantity ) as totalukuran,mhd.NamaHarga, " & _
                        " wd.Panjang, wd.Lebar, wd.Tinggi from MuatBarang mb  " & _
                        " left join MuatBarangDetail mbd  on mb.Mb_No = mbd.Mb_No  " & _
                        " left join V_Warehouse_Satuan wd on (mbd.Warehouse_Id = wd.IDDetail and wd.WarehouseItem_Code = mb.WarehouseHeaderID )  " & _
                        " left join Kapal mk on mb.Kapal = mk.IDDetail  left join MasterCustomer mc on mb.Customer_Id = mc.Kode_Customer  " & _
                        " left join QuotationDetail qd on qd.Quotation_No = wd.Quotation_No and wd.QuotationDetailID = qd.IDDetail  " & _
                        " left join MasterHargaDefault mhd on mhd.ID = qd.SatuanID  " & _
                        " where(mb.status <> 0 And mbd.status <> 0 And wd.status <> 0) " & _
                        " and mc.status = 1 and mk.status = 1 and qd.status <> 0  " & _
                        " and mhd.[status] = 1 and mb.Mb_No = '" & blid.ToString & "' ) as A " & _
                        " Group by A.Penerima, A.Nama_Customer, " & _
                        " A.Container, A.Nama_Barang, A.Kapal,A.Nama_Satuan, " & _
                        " A.Keterangan,A.Nama_Kapal,A.NamaHarga, " & _
                        " A.Panjang, A.Lebar, A.Tinggi "

            DS = SQLExecuteQuery(sqlstring)
            DT = DS.Tables(0)


            If DT.Rows.Count > 0 Then


                If DT.Rows.Count > jumlahdata Then
                    BanyakBarang = 0
                    Dim i As Integer = 0
                    Dim a As Integer = jumlahdata
                    Dim b As Integer = 1
                    Dim c As Integer

                    c = bulatkankeatas(CInt(DT.Rows.Count) / jumlahdata) 'untuk nentuin halamannya berapa banyak

                    For j As Integer = 0 To c - 1 'looping halaman
                        isiTable &= NotaHeader() ' create header untuk halaman

                        If j = c - 1 Then
                            a = DT.Rows.Count
                        End If

                        For z As Integer = i To a - 1
                            namabarang = ""
                            BanyakBarang = ""
                            Ukuran = ""
                            With DT.Rows(z)
                                If .Item("Container") = "true" Or .Item("Container") = "kubikasi" Then
                                    cSt = "Select ch.NoKontainer, cd.NamaBarang,cd.Qty,mso.Nama_Satuan from ContainerDetail cd join MasterSatuanOther mso on cd.SatuanID = mso.IDDetail JOIN Containerheader ch on ch.ContainerCode = cd.ContainerCode " & _
                                              "where cd.ContainerCode = '" & .Item("Nama_Barang").ToString & "' and (cd.status = 1 or cd.status = 2)and mso.status = 1"
                                    cDS = SQLExecuteQuery(cSt)
                                    cDT = cDS.Tables(0)
                                    If cDT.Rows.Count > 0 Then
                                        For e As Integer = 0 To cDT.Rows.Count - 1
                                            If e > 3 Then
                                                namabarang &= "dll." & " (" & cDT.Rows(e).Item("NoKontainer").ToString & ")"
                                            End If
                                            If e = cDT.Rows.Count - 1 Then
                                                namabarang &= cDT.Rows(e).Item("Qty").ToString + " " + cDT.Rows(e).Item("Nama_Satuan").ToString + " " + cDT.Rows(e).Item("NamaBarang").ToString + "." & " (" & cDT.Rows(e).Item("NoKontainer").ToString & ")"
                                            Else
                                                namabarang &= cDT.Rows(e).Item("Qty").ToString + " " + cDT.Rows(e).Item("Nama_Satuan").ToString + " " + cDT.Rows(e).Item("NamaBarang").ToString + ","
                                            End If
                                        Next
                                    End If
                                    If .Item("Container") = "true" Then
                                        BanyakBarang = .Item("Quantity").ToString
                                        namasatuan = " Container"
                                    Else
                                        BanyakBarang = .Item("Quantity").ToString
                                        namasatuan = " Colly"
                                    End If
                                Else
                                    namabarang &= .Item("Nama_Barang").ToString
                                    BanyakBarang = UbahKoma(.Item("Quantity").ToString)
                                    namasatuan = .Item("Nama_Satuan").ToString
                                    Ukuran = .Item("totalukuran").ToString
                                End If

                                If .Item("NamaHarga").ToString.Trim = "Kubik" Or .Item("NamaHarga").ToString.Trim = "KUBIK" Or .Item("NamaHarga").ToString.Trim = "kubik" Then
                                    isiTable &= " <tr style=""height:8px;""> " & _
                                              " <td align=left style="" border-right:1px black solid;border-left:1px black solid;"">" & _
                                              "   " & _
                                              " </td>" & _
                                              " <td align=left style=""border-right:1px black solid;"">" & _
                                                  "     <table> " & _
                                                  "         <tr> " & _
                                                  "             <td align=left  style=""width:30px;"">" & BanyakBarang.ToString & "</td> " & _
                                                  "             <td>" & namasatuan & " </td> " & _
                                                  "         </tr> " & _
                                                  "     </table> " & _
                                              " </td>" & _
                                              " <td align=left style=""border-right:1px black solid;"">" & _
                                              "   " & namabarang.ToString & " " & _
                                              "   (" & (CDbl(.Item("Panjang")) * 100) & " x " & (CDbl(.Item("Lebar")) * 100) & " x " & (CDbl(.Item("Tinggi")) * 100) & ")  " & _
                                              " </td>" & _
                                              " </td>" & _
                                              " <td align=right style=""border-right:1px black solid;"">" & _
                                              "   " & Tmbh3digit(Cek_Data(Ukuran.ToString)) & _
                                              " </td>" & _
                                              " <td align=right style=""border-right:1px black solid;"">" & _
                                              "   " & Tmbh3digit(Cek_Data(.Item("totalberat") / 1000).ToString) & _
                                              " </td>" & _
                                              "</tr>"
                                Else
                                    isiTable &= " <tr style=""height:8px;""> " & _
                                              " <td align=left style="" border-right:1px black solid;border-left:1px black solid;"">" & _
                                              "   " & _
                                              " </td>" & _
                                              " <td align=left style=""border-right:1px black solid;"">" & _
                                               "     <table> " & _
                                                  "         <tr> " & _
                                                  "             <td align=left  style=""width:30px;"">" & BanyakBarang.ToString & "</td> " & _
                                                  "             <td>" & namasatuan & " </td> " & _
                                                  "         </tr> " & _
                                                  "     </table> " & _
                                              " </td>" & _
                                              " <td align=left style=""border-right:1px black solid;"">" & _
                                              "   " & namabarang.ToString & _
                                              " </td>" & _
                                              " <td align=right style=""border-right:1px black solid;"">" & _
                                              "   " & Tmbh3digit(Cek_Data(Ukuran.ToString)) & _
                                              " </td>" & _
                                              " <td align=right style=""border-right:1px black solid;"">" & _
                                              "   " & Tmbh3digit(Cek_Data(.Item("totalberat") / 1000).ToString) & _
                                              " </td>" & _
                                              "</tr>"
                                End If


                            End With
                        Next
                        i = a
                        b = b + 1
                        a = (jumlahdata * b)

                        isiTable &= "       <tr >" & _
                                "         <td style=""vertical-align :top;border-left:1px black solid;border-right:1px black solid;border-bottom:1px black solid;"">" & _
                                "           &nbsp;" & _
                                "         </td>" & _
                                "         <td style=""vertical-align :top;border-right:1px black solid;border-bottom:1px black solid;"">" & _
                                "   &nbsp;" & _
                                "         </td>" & _
                                "         <td style=""vertical-align :top;border-right:1px black solid;border-bottom:1px black solid;"">" & _
                                "   &nbsp;" & _
                                "         </td>" & _
                                "         <td style=""vertical-align :top;border-right:1px black solid;border-bottom:1px black solid;"">" & _
                                "   &nbsp;" & _
                                "         </td>" & _
                                "         <td style=""vertical-align :top;border-right:1px black solid;border-bottom:1px black solid;"">" & _
                                "   &nbsp;" & _
                                "         </td>" & _
                                "       </tr>"
                    Next


                ElseIf DT.Rows.Count < jumlahdata Then
                    isiTable = NotaHeader()
                    BanyakBarang = 0
                    For i As Integer = 0 To DT.Rows.Count - 1
                        namabarang = ""
                        BanyakBarang = ""
                        Ukuran = ""
                        With DT.Rows(i)
                            If .Item("Container") = "true" Or .Item("Container") = "kubikasi" Then
                                cSt = "Select ch.NoKontainer, cd.NamaBarang,cd.Qty,mso.Nama_Satuan from ContainerDetail cd join MasterSatuanOther mso on cd.SatuanID = mso.IDDetail JOIN Containerheader ch on ch.ContainerCode = cd.ContainerCode " & _
                                          "where cd.ContainerCode = '" & .Item("Nama_Barang").ToString & "' and (cd.status = 1 or cd.status = 2)and mso.status = 1"
                                cDS = SQLExecuteQuery(cSt)
                                cDT = cDS.Tables(0)
                                If cDT.Rows.Count > 0 Then
                                    For e As Integer = 0 To cDT.Rows.Count - 1
                                        If e > 3 Then
                                            namabarang &= "dll." & " (" & cDT.Rows(e).Item("NoKontainer").ToString & ")"
                                        End If
                                        If e = cDT.Rows.Count - 1 Then
                                            namabarang &= cDT.Rows(e).Item("Qty").ToString + " " + cDT.Rows(e).Item("Nama_Satuan").ToString + " " + cDT.Rows(e).Item("NamaBarang").ToString + "." & " (" & cDT.Rows(e).Item("NoKontainer").ToString & ")"
                                        Else
                                            namabarang &= cDT.Rows(e).Item("Qty").ToString + " " + cDT.Rows(e).Item("Nama_Satuan").ToString + " " + cDT.Rows(e).Item("NamaBarang").ToString + ","
                                        End If
                                    Next
                                End If
                                If .Item("Container") = "true" Then
                                    BanyakBarang = .Item("Quantity").ToString
                                    namasatuan = " Container"
                                Else
                                    BanyakBarang = .Item("Quantity").ToString
                                    namasatuan = " Colly"
                                End If
                            Else
                                namabarang &= .Item("Nama_Barang").ToString
                                BanyakBarang = UbahKoma(.Item("Quantity").ToString)
                                namasatuan = .Item("Nama_Satuan").ToString
                                Ukuran = .Item("totalukuran").ToString
                            End If

                            If .Item("NamaHarga").ToString.Trim = "Kubik" Or .Item("NamaHarga").ToString.Trim = "KUBIK" Or .Item("NamaHarga").ToString.Trim = "kubik" Then
                                isiTable &= " <tr style=""height:8px;""> " & _
                                          " <td align=left style="" border-right:1px black solid;border-left:1px black solid;"">" & _
                                          "   " & _
                                          " </td>" & _
                                          " <td align=left style=""border-right:1px black solid;"">" & _
                                              "     <table> " & _
                                              "         <tr> " & _
                                              "             <td align=left  style=""width:30px;"">" & BanyakBarang.ToString & "</td> " & _
                                              "             <td>" & namasatuan & " </td> " & _
                                              "         </tr> " & _
                                              "     </table> " & _
                                          " </td>" & _
                                          " <td align=left style=""border-right:1px black solid;"">" & _
                                          "   " & namabarang.ToString & " " & _
                                          "   (" & (CDbl(.Item("Panjang")) * 100) & " x " & (CDbl(.Item("Lebar")) * 100) & " x " & (CDbl(.Item("Tinggi")) * 100) & ")  " & _
                                          " </td>" & _
                                          " </td>" & _
                                          " <td align=right style=""border-right:1px black solid;"">" & _
                                          "   " & Tmbh3digit(Cek_Data(Ukuran.ToString)) & _
                                          " </td>" & _
                                          " <td align=right style=""border-right:1px black solid;"">" & _
                                          "   " & Tmbh3digit(Cek_Data(.Item("totalberat") / 1000).ToString) & _
                                          " </td>" & _
                                          "</tr>"
                            Else
                                isiTable &= " <tr style=""height:8px;""> " & _
                                          " <td align=left style="" border-right:1px black solid;border-left:1px black solid;"">" & _
                                          "   " & _
                                          " </td>" & _
                                          " <td align=left style=""border-right:1px black solid;"">" & _
                                           "     <table> " & _
                                              "         <tr> " & _
                                              "             <td align=left  style=""width:30px;"">" & BanyakBarang.ToString & "</td> " & _
                                              "             <td>" & namasatuan & " </td> " & _
                                              "         </tr> " & _
                                              "     </table> " & _
                                          " </td>" & _
                                          " <td align=left style=""border-right:1px black solid;"">" & _
                                          "   " & namabarang.ToString & _
                                          " </td>" & _
                                          " <td align=right style=""border-right:1px black solid;"">" & _
                                          "   " & Tmbh3digit(Cek_Data(Ukuran.ToString)) & _
                                          " </td>" & _
                                          " <td align=right style=""border-right:1px black solid;"">" & _
                                          "   " & Tmbh3digit(Cek_Data(.Item("totalberat") / 1000).ToString) & _
                                          " </td>" & _
                                          "</tr>"
                            End If


                        End With
                    Next
                    isiTable &= "       <tr >" & _
                                "         <td style=""vertical-align :top;border-left:1px black solid;border-right:1px black solid;border-bottom:1px black solid;"">" & _
                                "           &nbsp;" & _
                                "         </td>" & _
                                "         <td style=""vertical-align :top;border-right:1px black solid;border-bottom:1px black solid;"">" & _
                                "   &nbsp;" & _
                                "         </td>" & _
                                "         <td style=""vertical-align :top;border-right:1px black solid;border-bottom:1px black solid;"">" & _
                                "   &nbsp;" & _
                                "         </td>" & _
                                "         <td style=""vertical-align :top;border-right:1px black solid;border-bottom:1px black solid;"">" & _
                                "   &nbsp;" & _
                                "         </td>" & _
                                "         <td style=""vertical-align :top;border-right:1px black solid;border-bottom:1px black solid;"">" & _
                                "   &nbsp;" & _
                                "         </td>" & _
                                "       </tr>"

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
        Dim namabarang As String = ""
        Dim BanyakBarang As String
        Dim Ukuran As String
        Dim ukuransementara As Decimal
        Dim ukurantotal As Decimal
        Dim beratsementara As Double
        Dim berattotal As Double
        Dim banyakbarangtotal As Double
        Dim banyakbarangsementara As Double
        Dim namasatuan As String = ""
        Dim MeterTon As String = ""

        Try
            'sqlstring = " select mbd.IDDetail as ID,mb.Penerima,mc.Nama_Customer,wd.Container, wd.Nama_Barang,mbd.PackedContID, " & _
            '            " mbd.Quantity,mb.Kapal,wd.Nama_Satuan,wd.Keterangan,mk.Nama_Kapal,mb.Kapal,(wd.Berat * mbd.Quantity) as totalberat , " & _
            '            " (wd.Panjang * wd.Lebar * wd.Tinggi * mbd.Quantity ) as totalukuran,mhd.NamaHarga, wd.Panjang, wd.Lebar, wd.Tinggi from MuatBarang mb " & _
            '            " left join MuatBarangDetail mbd  on mb.Mb_No = mbd.Mb_No " & _
            '            " left join V_Warehouse_Satuan wd on (mbd.Warehouse_Id = wd.IDDetail and wd.WarehouseItem_Code = mb.WarehouseHeaderID ) " & _
            '            " left join Kapal mk on mb.Kapal = mk.IDDetail " & _
            '            " left join MasterCustomer mc on mb.Customer_Id = mc.Kode_Customer " & _
            '            " left join QuotationDetail qd on qd.Quotation_No = wd.Quotation_No " & _
            '            " left join MasterHargaDefault mhd on mhd.ID = qd.SatuanID " & _
            '            " where mb.status <> 0 And mbd.status <>0  and wd.status <>0 and mc.status = 1 and mk.status = 1 and qd.status <> 0 and mhd.[status] = 1 and mb.Mb_No = '" & blid.ToString & "' "

            'sqlstring = " select mbd.IDDetail as ID,mb.Penerima,mc.Jenis_Perusahaan + ' ' + mc.Nama_Customer as Nama_Customer,wd.Container, wd.Nama_Barang,mbd.PackedContID, " & _
            '            " mbd.Quantity,mb.Kapal,wd.Nama_Satuan,wd.Keterangan,mk.Nama_Kapal,mb.Kapal,(wd.Berat * mbd.Quantity) as totalberat , " & _
            '            " (wd.Panjang * wd.Lebar * wd.Tinggi * mbd.Quantity ) as totalukuran,mhd.NamaHarga, wd.Panjang, wd.Lebar, wd.Tinggi from MuatBarang mb " & _
            '            " left join MuatBarangDetail mbd  on mb.Mb_No = mbd.Mb_No " & _
            '            " left join V_Warehouse_Satuan wd on (mbd.Warehouse_Id = wd.IDDetail and wd.WarehouseItem_Code = mb.WarehouseHeaderID ) " & _
            '            " left join Kapal mk on mb.Kapal = mk.IDDetail " & _
            '            " left join MasterCustomer mc on mb.Customer_Id = mc.Kode_Customer " & _
            '            " left join QuotationDetail qd on qd.Quotation_No = wd.Quotation_No and wd.QuotationDetailID = qd.IDDetail " & _
            '            " left join MasterHargaDefault mhd on mhd.ID = qd.SatuanID " & _
            '            " where mb.status <> 0 And mbd.status <>0  and wd.status <>0 and mc.status = 1 and mk.status = 1 and qd.status <> 0 and mhd.[status] = 1 and mb.Mb_No = '" & blid.ToString & "' "



            'sqlstring = " select wd.Nama_Barang,wd.Container,wd.Quantity,wd.Panjang,wd.Lebar,wd.Tinggi,wd.Berat from BillLand bl join MuatBarangReport mbr on bl.MBR_ID = mbr.ID join " & _
            '      " MuatBarang mb on mbr.Mb_Id =mb.ID join MuatBarangDetail mbd on mb.Mb_No = mbd.Mb_No join WarehouseDetail wd " & _
            '      " on mbd.Warehouse_Id = wd.ID where bl.status = 1 and mbr.status = 1 and mb.status = 1 and mbd.status = 1 and wd.status = 1 and bl.ID = '" & blid.ToString & "' "

            sqlstring = "select A.Penerima, A.Nama_Customer, A.Container, A.Nama_Barang, SUM(A.Quantity) as Quantity, " & _
                        " A.Kapal, A.Nama_Satuan, A.Keterangan, A.Nama_kapal, SUM(totalberat) as totalberat, Sum(A.totalukuran) as totalukuran, " & _
                        " A.NamaHarga, A.panjang, A.lebar, A.tinggi " & _
                        " FROM " & _
                        " (select distinct mbd.IDDetail as ID,mb.Penerima,mc.Jenis_Perusahaan + ' ' + mc.Nama_Customer as Nama_Customer, " & _
                        " wd.Container, wd.Nama_Barang,  mbd.Quantity as Quantity,wd.Nama_Satuan, " & _
                        " wd.Keterangan,mk.Nama_Kapal,mb.Kapal,(wd.Berat * mbd.Quantity) as totalberat , " & _
                        " cast((wd.Panjang * wd.Lebar * wd.Tinggi * mbd.Quantity ) as decimal (20,3) ) as totalukuran,mhd.NamaHarga, " & _
                        " wd.Panjang, wd.Lebar, wd.Tinggi from MuatBarang mb  " & _
                        " left join MuatBarangDetail mbd  on mb.Mb_No = mbd.Mb_No  " & _
                        " left join V_Warehouse_Satuan wd on (mbd.Warehouse_Id = wd.IDDetail and wd.WarehouseItem_Code = mb.WarehouseHeaderID )  " & _
                        " left join Kapal mk on mb.Kapal = mk.IDDetail  left join MasterCustomer mc on mb.Customer_Id = mc.Kode_Customer  " & _
                        " left join QuotationDetail qd on qd.Quotation_No = wd.Quotation_No and wd.QuotationDetailID = qd.IDDetail  " & _
                        " left join MasterHargaDefault mhd on mhd.ID = qd.SatuanID  " & _
                        " where(mb.status <> 0 And mbd.status <> 0 And wd.status <> 0) " & _
                        " and mc.status = 1 and mk.status = 1 and qd.status <> 0  " & _
                        " and mhd.[status] = 1 and mb.Mb_No = '" & blid.ToString & "' ) as A " & _
                        " Group by A.Penerima, A.Nama_Customer, " & _
                        " A.Container, A.Nama_Barang, A.Kapal,A.Nama_Satuan, " & _
                        " A.Keterangan,A.Nama_Kapal,A.NamaHarga, " & _
                        " A.Panjang, A.Lebar, A.Tinggi "

            DS = SQLExecuteQuery(sqlstring)
            DT = DS.Tables(0)
            'If DT.Rows.Count > 20 Then
            '    Session("OverDrive") = "True"
            'End If
            If DT.Rows.Count > 0 Then
                If DT.Rows.Count > 15 Then
                    Session("Over") = "True"
                    isiTable = tableHeaderOver()
                    ukurantotal = 0
                    berattotal = 0
                    BanyakBarang = 0
                    For i As Integer = 0 To DT.Rows.Count - 1
                        With DT.Rows(i)
                            If i = 0 Then
                                namabarang = .Item("Nama_Barang").ToString & ", "
                            ElseIf i < 3 Then
                                namabarang = .Item("Nama_Barang").ToString & ", "
                            ElseIf i = 3 Then
                                namabarang &= namabarang + .Item("Nama_Barang").ToString + " dll"
                            End If
                            ukuransementara = FormatNumber(CDec(.Item("totalukuran").ToString), 3)
                            ukurantotal = ukurantotal + ukuransementara
                            beratsementara = FormatNumber(CDbl(.Item("totalberat").ToString), 3)
                            berattotal = berattotal + beratsementara
                            banyakbarangsementara = CDbl(.Item("Quantity").ToString)
                            banyakbarangtotal = banyakbarangtotal + banyakbarangsementara



                        End With
                    Next

                    MeterTon = Cek_Data((berattotal / 1000) + ukurantotal).ToString

                    isiTable &= " <tr style=""height:5px;""> " & _
                                       " <td align=left style="" vertical-align :top;border-right:1px black solid;border-left:1px black solid;"">" & _
                                       "   " & _
                                       " </td>" & _
                                       " <td align=left style=""vertical-align :top;border-right:1px black solid;"">" & _
                                       "   " & UbahKoma(banyakbarangtotal.ToString) & " COLLI  " & _
                                       " </td>" & _
                                       " <td align=left style=""vertical-align :top;border-right:1px black solid;border-bottom:1px black solid;"">" & _
                                       " " & namabarang & " <BR /> " & _
                                       " </td>" & _
                                       " <td align=right style=""vertical-align :top;border-right:1px black solid;"">" & _
                                       "   " & Tmbh3digit(Cek_Data(ukurantotal.ToString)) & _
                                       " </td>" & _
                                       " <td align=right style=""vertical-align :top;border-right:1px black solid;"">" & _
                                       "   " & Tmbh3digit(Cek_Data(CDbl(berattotal / 1000).ToString)) & _
                                       " </td>" & _
                                       "</tr>"

                    isiTable &= "       <tr >" & _
                           "         <td style=""vertical-align :top;border-left:1px black solid;border-right:1px black solid;"">" & _
                           "           &nbsp;" & _
                           "         </td>" & _
                           "         <td style=""vertical-align :top;border-right:1px black solid;"">" & _
                           "   &nbsp;" & _
                           "         </td>" & _
                           "         <td style=""vertical-align :top;border-right:1px black solid;"">" & _
                           " (" & Bilangan2(banyakbarangtotal.ToString("###,##.00")) & ")&nbsp;COLLI " & _
                           "         </td>" & _
                           "         <td style=""vertical-align :top;border-right:1px black solid;"">" & _
                           "   &nbsp;" & _
                           "         </td>" & _
                           "         <td style=""vertical-align :top;border-right:1px black solid;"">" & _
                           "   &nbsp;" & _
                           "         </td>" & _
                           "       </tr>"
                Else
                    Session("Over") = "False"
                    isiTable = tableHeader()
                    ukurantotal = 0
                    berattotal = 0
                    banyakbarangtotal = 0
                    For i As Integer = 0 To DT.Rows.Count - 1
                        namabarang = ""
                        BanyakBarang = ""
                        Ukuran = ""
                        With DT.Rows(i)
                            ukuransementara = FormatNumber(CDec(.Item("totalukuran").ToString), 3)
                            ukurantotal = ukurantotal + ukuransementara
                            beratsementara = FormatNumber(CDbl(.Item("totalberat").ToString), 3)
                            berattotal = berattotal + beratsementara
                            banyakbarangsementara = CDbl(.Item("Quantity").ToString)
                            banyakbarangtotal = banyakbarangtotal + banyakbarangsementara
                            If .Item("Container") = "true" Or .Item("Container") = "kubikasi" Then
                                cSt = "Select ch.NoKontainer, cd.NamaBarang,cd.Qty,mso.Nama_Satuan from ContainerDetail cd join MasterSatuanOther mso on cd.SatuanID = mso.IDDetail JOIN Containerheader ch on ch.ContainerCode = cd.ContainerCode " & _
                                      "where cd.ContainerCode = '" & .Item("Nama_Barang").ToString & "' and (cd.status = 1 or cd.status = 2)and mso.status = 1"
                                cDS = SQLExecuteQuery(cSt)
                                cDT = cDS.Tables(0)
                                If cDT.Rows.Count > 0 Then
                                    For e As Integer = 0 To cDT.Rows.Count - 1
                                        If e > 3 Then
                                            namabarang &= "dll." & " (" & cDT.Rows(e).Item("NoKontainer").ToString & ")"
                                            Exit For
                                        End If
                                        If e = cDT.Rows.Count - 1 Then
                                            namabarang &= cDT.Rows(e).Item("NamaBarang").ToString + "." & " (" & cDT.Rows(e).Item("NoKontainer").ToString & ")"
                                        Else
                                            namabarang &= cDT.Rows(e).Item("NamaBarang").ToString + ","
                                        End If
                                    Next
                                End If
                                If .Item("Container") = "true" Then
                                    BanyakBarang = .Item("Quantity").ToString
                                    namasatuan = " Container"
                                Else
                                    BanyakBarang = .Item("Quantity").ToString
                                    namasatuan = " Colly"
                                End If
                            Else
                                namabarang &= .Item("Nama_Barang").ToString
                                BanyakBarang = UbahKoma(.Item("Quantity").ToString)
                                namasatuan = .Item("Nama_Satuan").ToString
                                Ukuran = .Item("totalukuran").ToString
                            End If

                            If .Item("NamaHarga").ToString.Trim = "Kubik" Or .Item("NamaHarga").ToString.Trim = "KUBIK" Or .Item("NamaHarga").ToString.Trim = "kubik" Then
                                isiTable &= " <tr style=""height:3px;""> " & _
                                          " <td align=left style="" border-right:1px black solid;border-left:1px black solid;"">" & _
                                          "   " & _
                                          " </td>" & _
                                          " <td align=left style=""border-right:1px black solid;"">" & _
                                          "     <table> " & _
                                          "         <tr> " & _
                                          "             <td align=left  style=""width:30px;"">" & BanyakBarang.ToString & "</td> " & _
                                          "             <td>" & namasatuan & " </td> " & _
                                          "         </tr> " & _
                                          "     </table> " & _
                                          " </td>" & _
                                          " <td align=left style=""border-right:1px black solid;"">" & _
                                          "   " & namabarang.ToString & " " & _
                                          "   (" & (CDbl(.Item("Panjang")) * 100) & " x " & (CDbl(.Item("Lebar")) * 100) & " x " & (CDbl(.Item("Tinggi")) * 100) & ")  " & _
                                          " </td>" & _
                                          " <td align=right style=""border-right:1px black solid;"">" & _
                                          "   " & Tmbh3digit(Cek_Data(Ukuran)) & _
                                          " </td>" & _
                                          " <td align=right style=""border-right:1px black solid;"">" & _
                                          "   " & Tmbh3digit(Cek_Data(CDbl(.Item("totalberat") / 1000).ToString)) & _
                                          " </td>" & _
                                          "</tr>"

                            Else
                                isiTable &= " <tr style=""height:3px;""> " & _
                                              " <td align=left style="" border-right:1px black solid;border-left:1px black solid;"">" & _
                                              "   " & _
                                              " </td>" & _
                                              " <td align=left style=""border-right:1px black solid;"">" & _
                                              "     <table> " & _
                                              "         <tr> " & _
                                              "             <td align=left  style=""width:30px;"">" & BanyakBarang.ToString & "</td> " & _
                                              "             <td>" & namasatuan & " </td> " & _
                                              "         </tr> " & _
                                              "     </table> " & _
                                              " </td>" & _
                                              " <td align=left style=""border-right:1px black solid;"">" & _
                                              "   " & namabarang.ToString & _
                                              " </td>" & _
                                              " <td align=right style=""border-right:1px black solid;"">" & _
                                              "   " & Tmbh3digit(Cek_Data(Ukuran)) & _
                                              " </td>" & _
                                              " <td align=right style=""border-right:1px black solid;"">" & _
                                              "   " & Tmbh3digit(Cek_Data(CDbl(.Item("totalberat") / 1000).ToString)) & _
                                              " </td>" & _
                                              "</tr>"
                            End If
                        End With

                    Next
                    MeterTon = Cek_Data((berattotal / 1000) + ukurantotal).ToString



                    isiTable &= "       <tr >" & _
                                "         <td style=""vertical-align :top;border-left:1px black solid;border-right:1px black solid;"">" & _
                                "           &nbsp;" & _
                                "         </td>" & _
                                "         <td style=""vertical-align :top;border-right:1px black solid;border-top:1px black solid;"">" & _
                                "   " & Cek_Data(Format(CDbl(banyakbarangtotal), "##,###,###,##").ToString) & _
                                "         </td>" & _
                                "         <td style=""vertical-align :top;border-right:1px black solid;border-top:1px black solid;"">" & _
                                "   (" & Bilangan2(banyakbarangtotal.ToString("###,##.00")) + ") COLLI " & _
                                "         </td>" & _
                                "         <td align=right style=""vertical-align :top;border-right:1px black solid;border-top:1px black solid;"">" & _
                                "   " & Tmbh3digit(Cek_Data(ukurantotal)) & _
                                "         </td>" & _
                                "         <td align=right style=""vertical-align :top;border-right:1px black solid;border-top:1px black solid;"">" & _
                                "   " & Tmbh3digit(Cek_Data(CDbl(berattotal / 1000).ToString)) & _
                                "         </td>" & _
                                "       </tr> "

                End If
            End If
            isiTable &= "<table width=780px cellpadding=0 cellspacing=0 style=""font-family:verdana;font-size:14px;table-layout:fixed;height:20px""> " & _
                       "       <tr style=""height:21px;""> " & _
                           "        <td style=""width:42px;vertical-align :top;border-left:1px black solid;border-right:1px black solid;border-bottom:1px black solid;"">&nbsp;</td>" & _
                           "        <td style=""width: 95px;vertical-align :top;border-right:1px black solid;border-bottom:1px black solid;""></td>" & _
                           "         <td style=""width: 272px;vertical-align :center;border-right:1px black solid;border-bottom:1px black solid;"" align=""center"" > " & _
                           "                Freight As Agreed " & _
                           "         </td> " & _
                           "        <td align=""center"" colspan=""2"" style=""width: 121px;vertical-align :top;border-top:1px black solid;border-right:1px black solid;border-bottom:1px black solid;font-family:verdana;font-size:11px;""><table style=""font-family:verdana;font-size:11px;""> Total = " & MeterTon & " Ton/M<sup>3</sup></table></td>" & _
                       "       </tr> " & _
                    "</table> "
        Catch ex As Exception
            Throw New Exception("Error Isi Table function : " & ex.ToString)
        End Try
        Return isiTable
    End Function
    Private Function quoKonfHeader(ByVal Mb_No As String) As String
        Dim header As String
        Dim sesHeader As String
        Dim STR As String
        Dim iDT As DataTable
        Dim hDS As DataSet
        Dim NamaPerusahaan As String
        Dim NoPelayaran As Integer = getNoPelayaranPrint(Mb_No)
        Dim NoKonosemen As Integer = getNoKonosemenPrint(Mb_No)
        If Panel_HistoryInput.Visible = False Then
            sesHeader = ddlHeader.SelectedValue
        Else
            sesHeader = ddlHeaderHist.SelectedValue
        End If
        STR = "Select * from HeaderForm where ID = '" & sesHeader & "' and status = 1;"
        hDS = SQLExecuteQuery(STR)
        iDT = hDS.Tables(0)
        NamaPerusahaan = iDT.Rows(0).Item("Nama").ToString
        '     " <tr>" & _
        '     "   <td style=""width:60px;vertical-align:center;"" align=center  >" & _
        '     "      <img src=""" & iDT.Rows(0).Item("PathLogo").ToString & """ style=""height: 100px; width: 100px"" />" & _
        '     "   </td>" & _
        '     "   <td style=""width:610px;vertical-align:top;#2c3848;""   align=center >" & _
        '      "      <table width=400px bgcolor=white cellpadding=0 cellspacing=0 style=""font-family:verdana;font-size:10px;"">" & _
        '     " <tr>" & _
        '     "   <td style=""vertical-align:top;""   align=center>" & _
        '     "     <font size=""4""><b>" & iDT.Rows(0).Item("Jenis").ToString & "</b> </font>" & _
        '     "   </td>" & _
        '     " </tr>"
        'If NamaPerusahaan = "PT. Ligita Jaya" Or NamaPerusahaan = "PT.Ligita Jaya" Then
        '    header &= "             <tr>" & _
        '                                    "                 <td colspan=2 align=center>" & _
        '                                    "                     <font size=""10""><b>" & iDT.Rows(0).Item("Nama").ToString & "</b>" & _
        '                                    "                 </td>" & _
        '                                    "             </tr>"
        'Else
        '    header &= "             <tr>" & _
        '                                    "                 <td colspan=2 align=center>" & _
        '                                    "                     <font size=""5""><b>" & iDT.Rows(0).Item("Nama").ToString & "</b>" & _
        '                                    "                 </td>" & _
        '                                    "             </tr>"
        'End If
        header = " <Table width=780px><tr style=""height:160px""><td></td></tr></table>" & _
                 "<table width=780px bgcolor=white cellpadding=0 cellspacing=0 style=""font-family:verdana;font-size:12px; "">"
        header &= "         <tr>" & _
                             "            <td style=""vertical-align:top;padding-left:480px;width:670px""   align=center>" & _
                             "              <table bgcolor=white cellpadding=0 cellspacing=0 style=""font-family:verdana;font-size:12px; "">" & _
                             "                  <tr>" & _
                             "                      <td>" & _
                             "                          <b>PELAYARAN &nbsp;NO : " & getRomanNumber(NoPelayaran) & "/" & CDate(Now).ToString("yy") & " </b>" & _
                             "                      </td >" & _
                             "                  </tr>" & _
                             "                  <tr>" & _
                             "                      <td>" & _
                             "                          <b>KONOSEMEN NO : " & NoKonosemen & "</b>" & _
                             "                      </td >" & _
                             "                  </tr>" & _
                             "              </table>" & _
                             "            </td>" & _
                             "          </tr>" & _
                             "      </table>" & _
                             "   </td>" & _
                             " </tr>" & _
                             " </table>"
        Return header
    End Function

    Private Function quotationHeader(ByVal blid As String) As String
        Dim HeaderReport As String
        Dim Tanggal As String
        Dim tanggalliat As Date
        Dim QuoNo As String
        Dim year As String
        Dim sqlstring As String
        Dim ds As DataSet
        Dim dt As DataTable
        Dim sesHeader As String
        Dim STR As String
        Dim iDT As DataTable
        Dim hDS As DataSet
        Dim NamaPerusahaan As String
        Dim tanggalberangkat As String = ""
        Dim sqlstr As String = ""
        Dim Pelabuhan As String = ""

        If Panel_HistoryInput.Visible = False Then
            sesHeader = ddlHeader.SelectedValue
        Else
            sesHeader = ddlHeaderHist.SelectedValue
        End If
        STR = "Select * from HeaderForm where ID = '" & sesHeader & "' and status = 1;"
        hDS = SQLExecuteQuery(STR)
        iDT = hDS.Tables(0)
        NamaPerusahaan = iDT.Rows(0).Item("Nama").ToString

        'sqlstring = " select Tujuan , Nama_Customer, Kapal, Nahkoda, Penerima from BillLand bl "
        QuoNo = Session("Quotation_No")

        sqlstring = " select mq.Tujuan,mb.Penerima,mc.Jenis_Perusahaan + ' ' + mc.Nama_Customer as Nama_Customer,mk.Nama_Kapal,mk.Nahkoda_Kapal,mc.KotaDitunjukan as Area, mb.Tanggal " & _
                            " from MuatBarang mb left join WarehouseHeader wh on  mb.WarehouseHeaderID = wh.WarehouseItem_Code " & _
                            " left join MasterQuotation mq on wh.Quotation_No = mq.Quotation_No " & _
                            " left join MasterCustomer mc on mb.Customer_Id = mc.Kode_Customer " & _
                            " left join Kapal mk on mb.Kapal = mk.IDDetail where mb.Mb_No = '" & blid.ToString & "' " & _
                            " and mb.status <>0  and wh.status <>0  and mc.status = 1 and mk.status <> 0"
        ds = SQLExecuteQuery(sqlstring)
        dt = ds.Tables(0)

        If dt.Rows.Count > 0 Then
            'get tanggal kapal berangkat
            sqlstr = "select distinct mbr.Depart_Date From MuatBarangReport MBR " & _
                    " JOIN MBRDetail MBRD on MBR.Mbr_No = MBRD.Mbr_No " & _
                    "where MBRD.Mb_No = '" & blid.ToString & "' AND mbr.[status] <> 0"
            tanggalberangkat = SQLExecuteScalar(sqlstr)

            'get pelabuhan

            sqlstr = "SELECT Pelabuhan from mastertujuan where Tujuan = '" & dt.Rows(0).Item("Tujuan").ToString & "' AND [status] <> 0"
            Pelabuhan = SQLExecuteScalar(sqlstr)


            tanggalliat = CDate(tanggalberangkat.ToString).ToString("dd MMMM yyyy")
            Tanggal = tanggalliat.ToString("dd") + " " + CekBulan(tanggalliat.ToString("MM"), 2) + " " + tanggalliat.ToString("yyyy")

            year = CDate(tanggalliat).ToString("yyyy")

        End If



        HeaderReport = "<table width=780px bgcolor=white cellpadding=0 cellspacing=0 style=""font-family:verdana;font-size:12px;"">" & _
              " <tr>" & _
              "   <td align=left style=""width:125px;"">" & _
              "     Alamat Kawat " & _
              "   </td>" & _
              "   <td align=left style=""width:2px;"">" & _
              "     : " & _
              "   </td>" & _
              "   <td align=left style=""width:625px;padding-left:20px;"">" & _
              "    <b>" & TxtKawat.Text.ToString & "</b>" & _
              "   </td>" & _
              " </tr>" & _
              " <tr>" & _
              "   <td align=left style=""width:125px;"">" & _
              "     Di terima dari " & _
              "   </td>" & _
              "   <td align=left style=""width:2px;"">" & _
              "     : " & _
              "   </td>" & _
              "   <td align=left style=""width:495px;"">" & _
              "    <b>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" & TukarNamaPerusahaan(dt.Rows(0).Item("Nama_Customer").ToString) & "</b>" & _
              "   </td>" & _
              "   <td align=left style=""width:180px;"">" & _
              "     di <b>" & dt.Rows(0).Item("Area").ToString & "</b>" & _
              "   </td>" & _
              " </tr>" & _
              "   <tr> " & _
              "   <td colspan=""2"" align=left style=""width:250px;"">" & _
              "      Untuk dimuat di kapal motor " & _
              "   </td>" & _
              "   <td align=left style=""width:500px;"">" & _
              "     <b>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" & dt.Rows(0).Item("Nama_Kapal").ToString & "</b>" & _
              "   </td>" & _
              "   <td align=left style=""width:150px;"">" & _
              "     &nbsp;" & _
              "   </td>" & _
              " </tr>" & _
              "</table>" & _
              "<table width=780px bgcolor=white cellpadding=0 cellspacing=0 style=""font-family:verdana;font-size:12px"">" & _
              " <tr>" & _
              "   <td align=left style=""width:210px;"">" & _
              "      Nahkoda " & _
              "   </td>" & _
               "   <td  align=left>" & _
              "    <b> " & dt.Rows(0).Item("Nahkoda_Kapal").ToString & "</b>" & _
              "   </td>" & _
              "   <td   align=right >" & _
              "     satu dan lain menurut;" & _
              "   </td>" & _
              " </tr>" & _
              "</table>" & _
              "<table width=780px bgcolor=white cellpadding=0 cellspacing=0 style=""font-family:verdana;font-size:12px;position:relative"">" & _
              " <tr>" & _
              "   <td   align=left style = ""text-align :justify;"" >" & _
              "     kehendak Nahkoda masuk atau keluar dari Pelabuhan-pelabuhan dengan atau tidak dengan pertolongan dari pandu, untuk menerima dan membongkar muatan-muatan mengambil dan menurunkan penumpang - penumpang atau menolong kapal-kapal atau perahu-perahu yang berada dalam bahaya atau untuk keperluan lain juga, menurut peraturan-peraturan yang tersebut di belakang ini Barang tersebut di bawah ini yang dialamatkan " & _
              "   </td>" & _
              " </tr>" & _
              "</table>" & _
              "<table width=780px bgcolor=white cellpadding=0 cellspacing=0 style=""font-family:verdana;font-size:12px;position:relative"">" & _
              " <tr>" & _
              "   <td   colspan=""2"" align=left style=""width:185px;"">" & _
              "      kepada " & _
              "   </td>" & _
              "   <td   align=left style=""width:365px;"">" & _
              "     <b>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" & TukarNamaPerusahaan(dt.Rows(0).Item("Penerima").ToString) & "</b>" & _
              "   </td>" & _
              "   <td   align=left style=""width:222px;"">" & _
              "     &nbsp;" & _
              "   </td>" & _
              " </tr>" & _
              " <tr>" & _
              "   <td   colspan=""2"" align=left style=""width:185px;"">" & _
              "      di " & _
              "   </td>" & _
              "   <td   align=left style=""width:365px;"">" & _
              "     <b>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" & Pelabuhan & "</b>" & _
              "   </td>" & _
              "   <td   align=left style=""width:222px;"">" & _
              "     &nbsp;" & _
              "   </td>" & _
              " </tr>" & _
              " </table> " & _
              "<BR />"

        HeaderReport &= quoTable(blid.ToString)

        HeaderReport &= "<table width=780px bgcolor=white cellpadding=0 cellspacing=0 style=""font-family:verdana;font-size:12px;position:relative"">" & _
                        "<tr>" & _
                        "   <td colspan =""2"" > " & _
                        "       <table bgcolor=white cellpadding=0 cellspacing=0 style=""font-family:verdana;font-size:12px;position:relative"">" & _
                        "           <tr>" & _
                        "               <td align=left style = ""text-align :justify;"">" & _
                        "                   Dengan menerima surat muatan ini maka si pengirim mengaku menerima baik dan tunduk kepada segala peraturan - peraturan yang tersebut di belakang ini dan tambahan - tambahan baik yang di tulis dan/atau di cap ataupun yang diselenggarakan dengan cara lain . Surat muatan ini telah ditanda tangani 3 lembar yang sama bunyinya dan setelah satu dari padanya dilunaskan dan dikembalikan dengan dibubuhi tanda tangan serta keterangan bahwa barang sudah diterima, maka lembar yang lain tidak berharga lagi .   " & _
                        "               </td>" & _
                        "           </tr>" & _
                        "       </table>" & _
                        "   </td> " & _
                        " </tr> " & _
                        " <tr>" & _
                        "       <td align = left> " & _
                        "           <table width=350px bgcolor=white cellpadding=1 cellspacing=0> " & _
                        "               <tr> " & _
                        "                   <td> " & _
                        "                       <br /><b></b> " & _
                        "                   </td> " & _
                        "               </tr> " & _
                        "               <tr> " & _
                        "                   <td> " & _
                        "                       <b></b> " & _
                        "                   </td> " & _
                        "               </tr> " & _
                        "           </table> " & _
                        "       </td> " & _
                        "       <td align = center valign = ""top"" align=""left"">" & _
                        "           <table width=350px bgcolor=white cellpadding=1 cellspacing=0> " & _
                        "               <tr> " & _
                        "                   <td align = ""center""> " & _
                        "                       <br><b>" & dt.Rows(0).Item("Area").ToString & "</b>" + ", " + Tanggal & " " & _
                        "                       <br>Nahkoda atau Atas Nama" & _
                        "                   </td> " & _
                        "               </tr> " & _
                        "           </table> " & _
                        "       </td>" & _
                        "  </tr>" & _
                        "  <tr>" & _
                        "       <td colspan = ""2"" align = right ><br /><br />" & _
                        "       </td>" & _
                        " </tr>" & _
                        " </table>"


        If Session("Over") = "True" Then
            HeaderReport &= isiOver(blid)
        End If

        Return HeaderReport
    End Function
    'Private Sub delete(ByVal ID As String)
    '    Try
    '        sqlstring = " UPDATE BillLand " & _
    '                     " SET " & _
    '                     " LastModified = '" & Now.ToString & "', " & _
    '                     " [status] = 0 " & _
    '                     " WHERE ID = '" & ID.ToString & "' And [status] = 1; "
    '        hasil = SQLExecuteNonQuery(sqlstring )
    '        If hasil > 0 Then
    '            load_grid()
    '            Clear()
    '            clear_label()
    '        End If
    '    Catch ex As Exception
    '        Throw New Exception("Error Function Delete :" & ex.ToString)
    '    End Try
    'End Sub
    'Private Sub Update(ByVal ID As String)
    '    Try
    '        sqlstring = " UPDATE BillLand" & _
    '                    " SET " & _
    '                    " MBR_ID = '" & hfMRID.Value&  "', " & _
    '                    " LastModified = '" & Now.ToString & "', " & _
    '                    " [status] = 1 " & _
    '                    " WHERE ID= '" & ID.ToString & "'; "

    '        result = SQLExecuteNonQuery(sqlstring )
    '        If result > 0 Then
    '            Clear()
    '            clear_label()
    '            load_grid()
    '            hfMode.Value = "Insert"
    '        End If
    '    Catch ex As Exception
    '        Response.Write(" Error Function Update <BR> " & ex.ToString)
    '    End Try
    'End Sub
    'Private Sub insert()
    '    Dim hasil As Integer
    '    Dim STR As String
    '    Try
    '        STR = " Insert into BillLand (MBR_ID,UserName,[Status]) values ( " & _
    '              " '" & hfMRID.Value & "' , '" & Session("UserId").ToString & "', 1)"
    '        hasil = SQLExecuteNonQuery(STR )
    '        If hasil > 0 Then
    '            load_grid()
    '            Clear()
    '            clear_label()
    '        End If
    '    Catch ex As Exception
    '        Throw New Exception("Error function Insert : " & ex.ToString)
    '    End Try
    'End Sub
    Private Sub Clear()
        hfCID.Value = ""
        hfMRID.Value = ""
        Hfnamakapal.Value = ""
    End Sub

    Private Sub clear_label()
        lblError.Text = ""
    End Sub

    Private Sub remove_item(ByVal Key As Integer)
        Try
            Dim aDT As DataTable = CType(Session("Grid_Kapal_Parent_BL"), DataTable)

            aDT.Rows.RemoveAt(Key)
            Create_Season()
            Session("Grid_Kapal_Parent_BL") = aDT
            Grid_Kapal_Parent.DataSource = aDT
            Grid_Kapal_Parent.DataBind()
        Catch ex As Exception
            Throw New Exception(ex.ToString)
        End Try
    End Sub

    Private Sub Create_Season()
        Try
            Dim iDT As New DataTable

            iDT.Columns.Add(New DataColumn("ID", GetType(String)))
            iDT.Columns.Add(New DataColumn("WarehouseHeaderID", GetType(String)))
            iDT.Columns.Add(New DataColumn("Penerima", GetType(String)))
            iDT.Columns.Add(New DataColumn("Customer_Id", GetType(String)))
            iDT.Columns.Add(New DataColumn("Nama_Customer", GetType(String)))
            iDT.Columns.Add(New DataColumn("Tanggal", GetType(DateTime)))
            iDT.Columns.Add(New DataColumn("Mb_No", GetType(String)))
            iDT.Columns.Add(New DataColumn("KapalID", GetType(String)))
            iDT.Columns.Add(New DataColumn("Kapal", GetType(String)))

            Session("Grid_Kapal_Parent_BL") = iDT
            Grid_Kapal_Parent.DataSource = iDT
            Grid_Kapal_Parent.KeyFieldName = "Mb_No"
            Grid_Kapal_Parent.DataBind()
        Catch ex As Exception
            Throw New Exception(ex.ToString)
        End Try
    End Sub
#End Region

#Region "VALIDATION"
    Private Function validation() As Boolean
        clear_label()
        If hfMRID.Value = "" Then
            lblError.Visible = True
            lblError.Text = " Kondisi belum diisi"
            Return False
        End If



        Return True
    End Function

    Private Function validation_add() As Boolean

        clear_label()
        If hfIDK.Value = "" Then
            lblError.Visible = True
            lblError.Text = "Pilih Kapal"
            Return False
        End If

        Return True
    End Function
    Private Function validation_history() As Boolean
        clear_label()
        If hfIDKHIS.Value = "" Then
            lblerror2.Visible = True
            lblerror2.Text = " Pilih Kapal "
            Return False
        End If

        If tbFrom.Text.Trim = "" Or tbSampai.Text.Trim = "" Then
            lblerror2.Visible = True
            lblerror2.Text = " Harap pilih tanggal "
            Return False
        End If
        Return True

        Return True
    End Function
#End Region

#Region "BUTTON"
    'Protected Sub btSave_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btSave.Click
    '    Try
    '        If validation() Then
    '            If hfMode.Value = "Insert" Then
    '                Call insert()
    '            Else
    '                Call Update(hfBLID.Value)
    '            End If
    '        End If
    '    Catch ex As Exception
    '        Throw New Exception("Error View Report : " & ex.ToString)
    '    End Try

    'End Sub

    Protected Sub btKembaliDevPeriod_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btKembaliDevPeriod.Click
        'Panel_Input.Visible = True
        'Panel_Grid.Visible = True
        'Panel_Report.Visible = False

        Panel_Input.Visible = False
        Panel_Grid.Visible = False
        Panel_Report.Visible = False
        historygrid.Visible = True
        Panel_HistoryInput.Visible = True


        lblError.Visible = False
        lblError.Text = ""
        Session("Over") = ""
    End Sub

    'Protected Sub Back_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Back.Click
    '    Panel_Input.Visible = True
    '    Panel_Grid.Visible = True
    '    Panel_Report.Visible = False
    'End Sub
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

    Private Sub load_ddlhis()
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

        Catch ex As Exception
            Throw New Exception("Error load ddl header: " & ex.ToString)
        End Try
    End Sub
#End Region


    Protected Sub Back_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Back.Click
        Panel_Input.Visible = True
        Panel_Grid.Visible = True
        Panel_Report.Visible = False
        historygrid.Visible = False
        Panel_HistoryInput.Visible = False
    End Sub

    Protected Sub ViewGrid_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ViewGrid.Click
        If validation_history() Then
            load_bl()
            TxtKapalhis.Text = Hfnamakapal.Value
        End If
    End Sub

    Protected Sub btViewHistory_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btViewHistory.Click
        Panel_Input.Visible = False
        Panel_Grid.Visible = False
        Panel_Report.Visible = False
        historygrid.Visible = True
        Panel_HistoryInput.Visible = True
        clear_label()
        Hfnamakapal.Value = ""
    End Sub

    Protected Sub btReset_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btReset.Click
        Clear()
        clear_label()
    End Sub

    Protected Sub btAdd_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btAdd.Click
        TxtKapal.Text = Hfnamakapal.Value
        If validation_add() Then

            load_kapal_parent()
        End If
    End Sub


End Class