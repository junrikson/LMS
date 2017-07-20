Imports System.Data
Imports System.Data.SqlClient
Partial Public Class Show_Item
    Inherits System.Web.UI.Page

    Private DS As DataSet
    Private DT As DataTable
    Private sqlString As String
    Private sDR As SqlDataReader
    Private con As SqlConnection

#Region " Page "

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try

            If Not Page.IsPostBack Then
                Response.Cache.SetCacheability(HttpCacheability.NoCache)

                Select Case Request("Mode").ToString
                    Case "MasterItem"
                        Call Load_Grid_Master_Item()
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

#Region " GridView "

    Private Sub Create_Grid_Session()
        Dim iDT As New DataTable
        Session("grid_Dialog_" & Request("Mode").ToString) = iDT
    End Sub
    Private Sub Load_Grid_Master_Item()

        '#
        Dim STR As String = ""

        Try

            Call Create_Grid_Session()

            sqlString = " SELECT " & _
                  "		QD.ID ID, QD.Quotation_No ""QUOTATION_NO"" ," & _
                  "	MC.Nama_Customer ""NAMA_CUSTOMER"" ,QD.Nama_Barang ""Nama_Barang""," & _
                  "	 QD.Harga ""HARGA"" " & _
                  " FROM QuotationDetail QD JOIN MasterQuotation MQ on QD.Quotation_No = MQ.Quotation_No " & _
                  " JOIN MasterCustomer MC on MQ.Customer_Id = MC.Kode_Customer " & _
                  " WHERE QD.status = 5 ORDER BY QD.Nama_Barang "
            DS = SQLExecuteQuery(sqlString)
            DT = DS.Tables(0)
            Session("grid_Dialog_" & Request("Mode").ToString) = DT
            grid_Dialog.DataSource = DT
            grid_Dialog.DataBind()
        Catch ex As Exception
            lError.Text = "Load_Grid_Packet Exception :<br>" & ex.ToString
        End Try

    End Sub

    Protected Sub grid_Dialog_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles grid_Dialog.PreRender
        If Not Page.IsPostBack Then
            grid_Dialog.FocusedRowIndex = -1
        End If
    End Sub

#End Region

End Class