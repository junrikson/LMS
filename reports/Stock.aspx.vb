Imports System.Data
Imports System.Data.SqlClient
Imports DevExpress.Web.ASPxGridView
Partial Public Class Stock
    Inherits System.Web.UI.Page
    Dim sqlstring As String = ""
    Dim DT As DataTable
    Dim DS As DataSet

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Session("UserID") = Nothing Then
                FormsAuthentication.SignOut()
                Response.Redirect("Index.aspx")
            End If

            LoadDDL_nama_gudang()
            LoadDDLKota()
        Catch ex As Exception
            Response.Write(ex.ToString)
        End Try
    End Sub

    Private Sub LoadDDL_nama_gudang()
        Try
            sqlstring = " SELECT Warehouse_code, Warehouse_Name from MasterWarehouse where status = 1 order by Warehouse_Name"
            dt = SQLExecuteQuery(sqlstring).Tables(0)
            With DDLGudang
                .DataSource = dt
                .DataTextField = "Warehouse_Name"
                .DataValueField = "Warehouse_code"
                .DataBind()
            End With
            DDLGudang.Items.Insert(0, "Pilih Gudang")
        Catch ex As Exception
            Response.Write("DDLPaymentType Exception :<br>" & ex.ToString)
        End Try
    End Sub

    Private Sub LoadDDLKota()
        Try
            sqlstring = " SELECT DISTINCT" & _
                          "		ID, " & _
                          "	Tujuan, status " & _
                          "	FROM MasterTujuan " & _
                          " WHERE status = 1 ORDER BY Tujuan "
            DS = SQLExecuteQuery(sqlstring)
            DT = DS.Tables(0)

            'DDLKotaPembayar.Items.Clear()
            DDLTujuan.Items.Clear()

            With DDLTujuan
                .DataSource = DT
                .DataTextField = "Tujuan"
                .DataValueField = "Tujuan"
                .DataBind()
            End With

            DDLTujuan.Items.Insert(0, "Pilih Kota Tujuan")
        Catch ex As Exception
            Throw New Exception(ex.ToString)
        End Try
    End Sub
End Class