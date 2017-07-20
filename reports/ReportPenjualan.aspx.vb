Public Partial Class ReportPenjualan
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Session("UserID") = Nothing Then
                FormsAuthentication.SignOut()
                Response.Redirect("Index.aspx")
            End If

            If Not Page.IsPostBack Then
                Load_ddl_kapal()
                tbStartDate.Date = Today
                tbEndDate.Date = Today
            End If
            Load_ddl_kapal()
        Catch ex As Exception
            Response.Write("Page_Load Exception :<br>" & ex.ToString)
        End Try
    End Sub

    Private Sub Load_ddl_kapal()
        Try
            Dim sqlstring As String
            sqlstring = "select Nama_Kapal, Alias_kapal from kapal where status = 1"
            Dim dt As DataTable = SQLExecuteQuery(sqlstring).Tables(0)
            With DDLKapal
                .DataSource = dt
                .DataTextField = "Nama_Kapal"
                .DataValueField = "Alias_kapal"
                .DataBind()
            End With
            DDLKapal.Items.Insert(0, "Semua")
            'DDLKapal.SelectedValue = "0"
        Catch ex As Exception
            Throw New Exception("<b>Error Load DDL :</b>" & ex.ToString)
        End Try
    End Sub

End Class