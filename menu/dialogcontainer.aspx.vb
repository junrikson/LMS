Imports System.Data
Imports System.Data.SqlClient
Imports DevExpress.Web.ASPxGridView
Partial Public Class dialogcontainer
    Inherits System.Web.UI.Page
    Private DS As DataSet
    Private DT As DataTable
    Private DR As DataRow
    Private sqlString As String

#Region "PAGE"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not Page.IsPostBack Then
                Response.Cache.SetCacheability(HttpCacheability.NoCache)
                Select Case Request("Mode").ToString
                    Case "Container"
                        Call load_grid_container()
                    Case "Kontainer"
                        Call load_grid_container_all()
                End Select
            End If

            If Not Session("grid_Dialog_Warehouse") Is Nothing Then
                Grid_Dialog.DataSource = CType(Session("grid_Dialog_Warehouse"), DataTable)
                Grid_Dialog.DataBind()
            End If

        Catch ex As Exception
            Response.Write("Error Page Load Dialog :<BR>" & ex.ToString)
        End Try
    End Sub
#End Region

#Region "GRID_VIEW"
    Private Sub load_grid_container()
        Try
            Dim iDT As New DataTable
            Dim qdid As String
            With iDT.Columns
                .Add(New DataColumn("ID", GetType(String)))
                .Add(New DataColumn("ContainerCode", GetType(String)))
                .Add(New DataColumn("QuotationNo", GetType(String)))
                .Add(New DataColumn("Nama_Customer", GetType(String)))
            End With

            qdid = Request("QDID").ToString
            sqlString = " SELECT CH.NoKontainer as ContainerCode,CH.ContainerCode as ID,CH.totalberat as QuotationDetailID,CH.NoSeal, MC.Nama_Customer " & _
                        " ,MQ.Penerima " & _
                        " FROM ContainerHeader CH join MasterCustomer MC on CH.CustomerID = MC.Kode_Customer " & _
                        " left JOIN MasterQuotation MQ on CH.QuotationNo = MQ.Quotation_No " & _
                        " WHERE MC.[status] = 1 " & _
                        " AND (MQ.[status] = 1 or MQ.[status] = 2 ) " & _
                        " AND CH.[status] = 1 " & _
                        " AND CH.QuotationDetailID = '" & qdid.ToString & "' " & _
                        " AND CH.[statuspengiriman] = 0 " & _
                        " ORDER BY CH.ID"
            '                        " left JOIN ContainerDetail CD on CH.ID = CD.ContainerHeaderID " & _

            DS = SQLExecuteQuery(sqlString)
            DT = DS.Tables(0)

            For i As Integer = 0 To DT.Rows.Count - 1
                With DT.Rows(i)
                    DR = iDT.NewRow
                    DR("ID") = .Item("ID").ToString
                    DR("ContainerCode") = .Item("ContainerCode").ToString
                    DR("QuotationNo") = .Item("NoSeal").ToString
                    DR("Nama_Customer") = .Item("Nama_Customer").ToString
                    iDT.Rows.Add(DR)
                End With
            Next
            Grid_Dialog.Columns("Nama_Customer").Caption = "Nama Konsumen "
            Grid_Dialog.Columns("QuotationNo").Caption = "No Seal"
            Session("grid_Dialog_Warehouse") = iDT
            Grid_Dialog.DataSource = iDT
            Grid_Dialog.DataBind()
        Catch ex As Exception
            Response.Write("Show dialog container error :<BR>" & ex.ToString)
        End Try
    End Sub

    Private Sub load_grid_container_all()
        Try
            Dim iDT As New DataTable
            With iDT.Columns
                .Add(New DataColumn("ID", GetType(String)))
                .Add(New DataColumn("ContainerCode", GetType(String)))
                .Add(New DataColumn("QuotationNo", GetType(String)))
                .Add(New DataColumn("Nama_Customer", GetType(String)))
            End With

            sqlString = " SELECT CH.NoKontainer as ContainerCode,CH.ContainerCode as ID,CH.QuotationDetailID, MC.Nama_Customer " & _
                        " ,MQ.Penerima " & _
                        " FROM ContainerHeader CH join MasterCustomer MC on CH.CustomerID = MC.Kode_Customer  " & _
                        " left JOIN MasterQuotation MQ on CH.QuotationNo = MQ.Quotation_No " & _
                        " WHERE MC.[status] = 1 " & _
                        " AND (MQ.[status] = 1 or MQ.[status] = 2 ) " & _
                        " AND CH.[status] = 1 " & _
                        " AND CH.[statuspengiriman] = 0 " & _
                        " ORDER BY CH.ID"
            '                        " left JOIN ContainerDetail CD on CH.ID = CD.ContainerHeaderID " & _

            DS = SQLExecuteQuery(sqlString)
            DT = DS.Tables(0)

            For i As Integer = 0 To DT.Rows.Count - 1
                With DT.Rows(i)
                    DR = iDT.NewRow
                    DR("ID") = .Item("ID").ToString
                    DR("ContainerCode") = .Item("ContainerCode").ToString
                    DR("QuotationNo") = .Item("QuotationDetailID").ToString
                    DR("Nama_Customer") = .Item("Nama_Customer").ToString
                    iDT.Rows.Add(DR)
                End With
            Next
            Grid_Dialog.Columns("Nama_Customer").Caption = "Nama Konsumen "
            Session("grid_Dialog_Warehouse") = iDT
            Grid_Dialog.DataSource = iDT
            Grid_Dialog.DataBind()
        Catch ex As Exception
            Response.Write("Show dialog container error :<BR>" & ex.ToString)
        End Try
    End Sub
    Protected Sub Grid_Dialog_Child_DataSelect(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Call load_grid_child(TryCast(sender, ASPxGridView))
        Catch ex As Exception
            Response.Write("Error Load Grid Child DataSelect  :<BR>" & ex.ToString)
        End Try
    End Sub

    Private Sub load_grid_child(ByVal grid As ASPxGridView)
        Try
            sqlString = " SELECT CD.ID,CD.ContainerHeaderID,CD.NamaBarang,MSO.Nama_Satuan,CD.Qty FROM ContainerDetail " & _
                        " CD JOIN MasterSatuanOther MSO on CD.SatuanID= MSO.IDDetail" & _
                        " Where CD.ContainerCode = '" & grid.GetMasterRowKeyValue() & "' AND CD.status = 1 Order by CD.timestamp desc"
            DS = SQLExecuteQuery(sqlString)
            DT = DS.Tables(0)
            grid.DataSource = DT
        Catch ex As Exception
            Response.Write("Error Load Grid Child :<BR>" & ex.ToString)
        End Try
    End Sub

#End Region
    
End Class