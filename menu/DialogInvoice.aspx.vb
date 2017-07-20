Imports System.Data
Imports System.Data.SqlClient
Partial Public Class DialogInvoice
    Inherits System.Web.UI.Page
    Private DS As DataSet
    Private DT As DataTable
    Private DR As DataRow
    Private sqlString As String

#Region "Page"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Page.IsPostBack Then
                Response.Cache.SetCacheability(HttpCacheability.NoCache)
                Create_Grid_Session()
                Select Case Request("Mode").ToString
                    Case "Nama"
                        Load_Grid_Invoice_Header()
                    Case "DP"
                        Load_Grid_Invoice_DP()
                End Select
            End If

            If Not Session("Grid_Item") Is Nothing Then
                Grid_item.DataSource = Session("Grid_Item")
                Grid_item.DataBind()
            End If
        Catch ex As Exception
            Response.Write("<b>Error Page Load :</b>" & ex.ToString)
        End Try
    End Sub
#End Region

#Region "Grid"

    Private Sub Load_Grid_Invoice_Header()
        Try

            Dim iDT As New DataTable

            With iDT.Columns
                .Add(New DataColumn("ID", GetType(String)))
                .Add(New DataColumn("MB_No", GetType(String)))
                .Add(New DataColumn("Nama_Customer", GetType(String)))
                .Add(New DataColumn("Penerima", GetType(String)))
                .Add(New DataColumn("Tujuan", GetType(String)))
                .Add(New DataColumn("IDkapal", GetType(String)))
                .Add(New DataColumn("NamaKapal", GetType(String)))
                .Add(New DataColumn("Tanggal", GetType(String)))
                .Add(New DataColumn("Paid", GetType(String)))
                .Add(New DataColumn("Kode_Customer", GetType(String)))
            End With

            sqlString = "Select distinct mb.Mb_No as ID,mb.Mb_No,mc.Nama_Customer,mc.Kode_Customer, " & _
                        "mq.Penerima, mq.Tujuan, mb.Kapal, K.Nama_Kapal, wh.Paid, MBR.Depart_Date as Tanggal  " & _
                        "from MuatBarang mb " & _
                        "left join MasterCustomer mc on mb.Customer_Id = mc.Kode_Customer " & _
                        "left join WarehouseHeader wh on mb.WarehouseHeaderID = wh.WarehouseItem_Code " & _
                        "left join MasterQuotation mq on wh.Quotation_No = mq.Quotation_No " & _
                        "left join Kapal K on mb.Kapal = K.IDDetail " & _
                        "left join MBRDetail MBRD on mb.Mb_No = MBRD.Mb_No " & _
                        "left join MuatBarangReport MBR on MBR.Mbr_No = MBRD.Mbr_No   " & _
                        "where " & _
                        "mb.status = 5 " & _
                        "and mc.status = 1 " & _
                        "and K.[status] = 1 " & _
                        "and mq.[status] <> 0 " & _
                        "and wh.[status] <> 0   " & _
                        "and wh.paid <> 100 " & _
                        "ORDER BY mb.Mb_No desc "
            DS = SQLExecuteQuery(sqlString)
            DT = DS.Tables(0)
            If DT.Rows.Count > 0 Then
                For i As Integer = 0 To DT.Rows.Count - 1
                    With DT.Rows(i)
                        DR = iDT.NewRow
                        DR("ID") = .Item("ID").ToString
                        DR("MB_No") = .Item("MB_No").ToString
                        DR("Nama_Customer") = .Item("Nama_Customer").ToString
                        DR("Penerima") = .Item("Penerima")
                        DR("Tujuan") = .Item("Tujuan")
                        DR("IDkapal") = .Item("Kapal")
                        DR("NamaKapal") = .Item("Nama_Kapal")
                        DR("Paid") = .Item("Paid").ToString
                        DR("Tanggal") = CDate(.Item("Tanggal").ToString).ToString("dd MMMM yyyy")
                        DR("Kode_Customer") = .Item("Kode_Customer").ToString
                        iDT.Rows.Add(DR)
                    End With
                Next

                Session("Grid_Item") = iDT
                Grid_item.DataSource = iDT
                Grid_item.DataBind()
            Else
                Grid_item.DataSource = Nothing
                Grid_item.DataBind()
            End If

        Catch ex As Exception
            Throw New Exception("<b>Error load_grid_invoice_header :</b>" & ex.ToString)
        End Try
    End Sub

    Private Sub Load_Grid_Invoice_DP()
        Try

            Dim iDT As New DataTable

            With iDT.Columns
                .Add(New DataColumn("ID", GetType(String)))
                .Add(New DataColumn("MB_No", GetType(String)))
                .Add(New DataColumn("Nama_Customer", GetType(String)))
                .Add(New DataColumn("Penerima", GetType(String)))
                .Add(New DataColumn("Tujuan", GetType(String)))
                .Add(New DataColumn("IDkapal", GetType(String)))
                .Add(New DataColumn("NamaKapal", GetType(String)))
                .Add(New DataColumn("Tanggal", GetType(String)))
                .Add(New DataColumn("Paid", GetType(String)))
                .Add(New DataColumn("Kode_Customer", GetType(String)))
            End With

            sqlString = "Select mb.Mb_No, wh.WarehouseItem_Code as ID,wh.WarehouseItem_Code ,mc.Nama_Customer, mc.Kode_Customer, " & _
                        "mq.Penerima, mq.Tujuan,  mb.Tanggal " & _
                        "from WarehouseHeader wh " & _
                        "left join MasterQuotation mq on wh.Quotation_No = mq.Quotation_No " & _
                        "left join MasterCustomer mc on mq.Customer_Id = mc.Kode_Customer " & _
                        "left join MuatBarang MB on wh.WarehouseItem_Code =  MB.WarehouseheaderID " & _
                        "where " & _
                        "mc.status = 1 " & _
                        "and (mq.[status] = 1 or mq.[status] = 2 ) " & _
                        "and wh.[status] <> 7 and wh.[status] <> 0 and (MB.[status] = 1 or MB.[status] = 2) " & _
                        "ORDER BY wh.WarehouseItem_Code desc "
            DS = SQLExecuteQuery(sqlString)
            DT = DS.Tables(0)
            If DT.Rows.Count > 0 Then
                For i As Integer = 0 To DT.Rows.Count - 1
                    With DT.Rows(i)
                        DR = iDT.NewRow
                        DR("ID") = .Item("ID").ToString
                        DR("MB_No") = .Item("MB_No").ToString
                        DR("Nama_Customer") = .Item("Nama_Customer").ToString
                        DR("Penerima") = .Item("Penerima")
                        DR("Tujuan") = .Item("Tujuan")
                        DR("Tanggal") = CDate(.Item("Tanggal").ToString).ToString("dd MMMM yyyy")
                        DR("Kode_Customer") = .Item("Kode_Customer").ToString
                        iDT.Rows.Add(DR)
                    End With
                Next

                Session("Grid_Item") = iDT
                Grid_item.DataSource = iDT
                Grid_item.DataBind()
            Else
                Grid_item.DataSource = Nothing
                Grid_item.DataBind()
            End If
            Grid_item.Columns("ID").Visible = True
            Grid_item.Columns("ID").Caption = "Warehouse Code"
            Grid_item.Columns("IDkapal").Visible = False
            Grid_item.Columns("NamaKapal").Visible = False
        Catch ex As Exception
            Throw New Exception("<b>Error load_grid_invoice_header :</b>" & ex.ToString)
        End Try
    End Sub

#End Region

#Region "Create Session"
    Private Sub Create_Grid_Session()
        Dim iDT As New DataTable
        With iDT.Columns
            .Add(New DataColumn("ID", GetType(String)))
            .Add(New DataColumn("MB_No", GetType(String)))
            .Add(New DataColumn("Nama_Customer", GetType(String)))
            .Add(New DataColumn("Nama_Barang", GetType(String)))
            .Add(New DataColumn("Pembayaran", GetType(String)))
            .Add(New DataColumn("Penerima", GetType(String)))
            .Add(New DataColumn("Tujuan", GetType(String)))
            .Add(New DataColumn("IDkapal", GetType(String)))
            .Add(New DataColumn("NamaKapal", GetType(String)))
            .Add(New DataColumn("Tanggal", GetType(String)))
            .Add(New DataColumn("Paid", GetType(String)))
            .Add(New DataColumn("Kode_Customer", GetType(String)))
        End With
        Grid_item.KeyFieldName = "ID"
        Session("Grid_Item") = iDT
    End Sub
#End Region


    Private Sub Grid_item_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Grid_item.PreRender
        If Not Page.IsPostBack Then
            Grid_item.FocusedRowIndex = -1
        End If
    End Sub
End Class