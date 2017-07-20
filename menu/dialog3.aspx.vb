Imports System.Data
Imports System.Data.SqlClient
Partial Public Class dialog3
    Inherits System.Web.UI.Page
    Private DS As DataSet
    Private DT As DataTable
    Private sqlString As String
    Private sDR As SqlDataReader
    Private con As SqlConnection
   

#Region "PAGE"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try

            If Not Page.IsPostBack Then
                Response.Cache.SetCacheability(HttpCacheability.NoCache)

                Select Case Request("Mode").ToString

                    Case "MasterCustomerW"
                        Call Load_Grid_Master_CustomerW()
                    
                End Select
            End If

            If Not Session("grid_Dialog_" & Request("Mode").ToString) Is Nothing Then
                grid_Dialog.DataSource = CType(Session("grid_Dialog_" & Request("Mode").ToString), DataTable)
                grid_Dialog.DataBind()
            End If

        Catch ex As Exception
            lError.Text = "Page_Load Exception :<br>" & ex.ToString
        End Try
    End Sub
#End Region
#Region "GRID"
    Private Sub Create_Grid_Session()
        Dim iDT As New DataTable
        Session("grid_Dialog_" & Request("Mode").ToString) = iDT
    End Sub


    Private Sub Load_Grid_Master_CustomerW()

        '#
        Dim STR As String = ""

        Try

            Call Create_Grid_Session()

            sqlString = " SELECT " & _
                  "		WH.WarehouseItem_Code CODE, " & _
                  "	MC.Nama_Customer ""NAME"",MQ.Penerima ""QTY"" , MC.Kode_Customer ""CUSTOMERID"" " & _
                  "	FROM WarehouseHeader WH join MasterQuotation MQ on WH.Quotation_No = MQ.Quotation_No join MasterCustomer MC on MQ.Customer_Id = MC.Kode_Customer" & _
                  " WHERE WH.status = 1 and MQ.[status] <> 0 and MC.[status] = 1 And WH.[statuspengiriman] = 0 ORDER BY MC.Nama_Customer "
            DS = SQLExecuteQuery(sqlString)
            DT = DS.Tables(0)
            Session("grid_Dialog_" & Request("Mode").ToString) = DT
            grid_Dialog.DataSource = DT
            grid_Dialog.DataBind()
            grid_Dialog.Columns("NAME").Caption = "Nama"
            grid_Dialog.Columns("QTY").Caption = "Penerima"
            grid_Dialog.Columns("CODE").Caption = "Kode Input Gudang"
            grid_Dialog.Columns("CODE").Visible = True
        Catch ex As Exception
            lError.Text = "Load_Grid_Packet Exception :<br>" & ex.ToString
        End Try

    End Sub
#End Region

End Class