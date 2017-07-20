Imports System.Data
Imports System.Data.SqlClient
Partial Public Class kapal
    Inherits System.Web.UI.Page
    Private DT As DataTable
    Private DS As DataSet
    Private DR As DataRow
    Private sqlstring As String
    Private sdr As SqlDataReader
    Private transferID As String
    Dim kDT As New DataTable
    Dim result As String
    Dim hasil As Integer

#Region "PAGE"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Page.Title = "Master Kapal - Logistics Management System"
            ltrInfo.Text = ""

            If Session("userID") = Nothing Then
                FormsAuthentication.SignOut()
                Response.Redirect("login.aspx")
            End If

            If Not Page.IsPostBack Then
                load_grid_kapal()
                hfMode.Value = "Insert"
            End If

            If Not Session("Grid_Kapal") Is Nothing Then
                Grid_Kapal.DataSource = CType(Session("Grid_Kapal"), DataTable)
                Grid_Kapal.DataBind()
            End If
        Catch ex As Exception
            Throw New Exception(ex.ToString)
        End Try

    End Sub


#End Region

#Region "GRID"
    Private Sub load_grid_kapal()
        sqlstring = " select IDDetail,Nama_Kapal,Nahkoda_Kapal, Alias_Kapal, KeteranganKapal from Kapal where status = 1"
        DS = SQLExecuteQuery(sqlstring)
        DT = DS.Tables(0)

        kDT.Columns.Add(New DataColumn("ID", GetType(String)))
        kDT.Columns.Add(New DataColumn("Nama_Kapal", GetType(String)))
        kDT.Columns.Add(New DataColumn("Nahkoda_Kapal", GetType(String)))
        kDT.Columns.Add(New DataColumn("Alias_Kapal", GetType(String)))
        kDT.Columns.Add(New DataColumn("KeteranganKapal", GetType(String)))


        If DT.Rows.Count > 0 Then
            For i As Integer = 0 To DT.Rows.Count - 1
                With DT.Rows(i)
                    DR = kDT.NewRow
                    DR("ID") = .Item("IDDetail").ToString
                    DR("Nama_Kapal") = .Item("Nama_Kapal").ToString
                    DR("Nahkoda_Kapal") = .Item("Nahkoda_Kapal").ToString
                    DR("Alias_Kapal") = .Item("Alias_Kapal").ToString
                    DR("KeteranganKapal") = .Item("KeteranganKapal").ToString
                    kDT.Rows.Add(DR)
                End With
            Next

            Session("Grid_Kapal") = kDT
            Grid_Kapal.DataSource = kDT
            Grid_Kapal.KeyFieldName = "ID"
            Grid_Kapal.DataBind()
        Else
            Grid_Kapal.DataSource = Nothing
            Grid_Kapal.DataBind()
        End If
    End Sub

    Private Sub Grid_Kapal_RowCommand(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewRowCommandEventArgs) Handles Grid_Kapal.RowCommand
        Try
            clear_label()
            Select Case e.CommandArgs.CommandName
                Case "Delete"
                    Dim hsl As Integer = 0

                    hsl = CekUsingNumber("MuatBarang", "Kapal", Grid_Kapal.GetRowValues(e.VisibleIndex, "ID").ToString)

                    If hsl = 0 Then
                        Delete(Grid_Kapal.GetRowValues(e.VisibleIndex, "ID").ToString)
                    Else
                        ltrInfo.Text = "<div class=""alert alert-danger alert-dismissible"" id=""info-alert""><button type=""button"" class=""close"" data-dismiss=""alert"" aria-hidden=""true"">&times;</button><h4><i class=""icon fa fa-warning""></i> Tidak bisa dihapus, sudah pernah digunakan!</h4></div>"
                    End If


                Case "Edit"
                    btSimpan.Text = "Update"
                    hfMode.Value = "Update"
                    TxtNamaKapal.Text = Grid_Kapal.GetRowValues(e.VisibleIndex, "Nama_Kapal").ToString
                    DDLMilik.SelectedValue = Grid_Kapal.GetRowValues(e.VisibleIndex, "KeteranganKapal").ToString
                    TxtNahkoda.Text = Grid_Kapal.GetRowValues(e.VisibleIndex, "Nahkoda_Kapal").ToString
                    TxtSingkatanKapal.Text = Grid_Kapal.GetRowValues(e.VisibleIndex, "Alias_Kapal").ToString
                    hfID.Value = Grid_Kapal.GetRowValues(e.VisibleIndex, "ID").ToString
            End Select
        Catch ex As Exception
            Throw New Exception("Error Grid_Kapal_RowCommand : " & ex.ToString)
        End Try
    End Sub

    Private Sub Grid_Kapal_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Grid_Kapal.PreRender
        If Not Page.IsPostBack Then
            Grid_Kapal.FocusedRowIndex = -1
        End If
    End Sub
#End Region

#Region "VALIDATION"
    Private Function Validation() As Boolean
        Try
            clear_label()
            If TxtNamaKapal.Text.Trim = "" Then
                ltrInfo.Text = "<div class=""alert alert-danger alert-dismissible"" id=""info-alert""><button type=""button"" class=""close"" data-dismiss=""alert"" aria-hidden=""true"">&times;</button><h4><i class=""icon fa fa-warning""></i> Nama kapal harus diisi!</h4></div>"
                Return False
            End If

            If TxtNahkoda.Text.Trim = "" Then
                ltrInfo.Text = "<div class=""alert alert-danger alert-dismissible"" id=""info-alert""><button type=""button"" class=""close"" data-dismiss=""alert"" aria-hidden=""true"">&times;</button><h4><i class=""icon fa fa-warning""></i> Nahkoda kapal harus diisi!</h4></div>"
                Return False
            End If

            If TxtSingkatanKapal.Text.Trim = "" Then
                ltrInfo.Text = "<div class=""alert alert-danger alert-dismissible"" id=""info-alert""><button type=""button"" class=""close"" data-dismiss=""alert"" aria-hidden=""true"">&times;</button><h4><i class=""icon fa fa-warning""></i> Singkatan kapal harus diisi!</h4></div>"
                Return False
            End If

            Return True
        Catch ex As Exception
            Response.Write(ex.ToString)
        End Try

    End Function

    Private Function Input_Validation() As Boolean
        sqlstring = "SELECT " & _
                    "ID FROM Kapal " & _
                    "WHERE " & _
                    "(Nama_Kapal = '" & TxtNamaKapal.Text.Replace("'", "''") & "' " & _
                    "OR " & _
                    "Alias_Kapal = '" & TxtSingkatanKapal.Text.Replace("'", "''") & "') " & _
                    "AND " & _
                    "[status] = 1 "
        result = SQLExecuteScalar(sqlstring)
        If result <> "" Then
            ltrInfo.Text = "<div class=""alert alert-danger alert-dismissible"" id=""info-alert""><button type=""button"" class=""close"" data-dismiss=""alert"" aria-hidden=""true"">&times;</button><h4><i class=""icon fa fa-warning""></i> Nama kapal atau alias sudah terdaftar!</h4></div>"
            TxtNamaKapal.Focus()
            Return False
        End If

        Return True
    End Function
#End Region


#Region "METHOD"
    Private Sub Insert()
        Dim iddetail As Integer
        Dim cekstring As String
        Dim cekdt As DataTable

        Try
            cekstring = "select * from Kapal where status = 1 "
            cekdt = SQLExecuteQuery(cekstring).Tables(0)
            If cekdt.Rows.Count > 0 Then
                iddetail = getDetailIDMaster("Kapal") + 1
            Else
                iddetail = 1
            End If
            sqlstring = " Insert Into Kapal(IDDetail,Nama_Kapal,Nahkoda_Kapal,Alias_Kapal,[status],UserName, KeteranganKapal ) values (" & _
                        " " & iddetail & ",'" & TxtNamaKapal.Text.ToString.Replace("'", "''") & "' , '" & TxtNahkoda.Text.ToString.Replace("'", "''") & "', '" & TxtSingkatanKapal.Text.ToString.Replace("'", "''") & "' ,1, '" & Session("UserID").ToString & "', '" & DDLMilik.SelectedValue & "');"

            If SQLExecuteNonQuery(sqlstring) > 0 Then
                load_grid_kapal()
                ltrInfo.Text = "<div class=""alert alert-info alert-dismissible"" id=""info-alert""><button type=""button"" class=""close"" data-dismiss=""alert"" aria-hidden=""true"">&times;</button><h4><i class=""icon fa fa-check""></i> Penambahan Data Berhasil!</h4></div>"
                Clear()
            End If

        Catch ex As Exception
            Throw New Exception("Error Insert : " & ex.ToString)
        End Try
    End Sub

    Private Sub Delete(ByVal ID As String)
        Try
            sqlstring = " UPDATE Kapal " & _
                        " SET " & _
                        " LastModified = '" & Now.ToString("yyyy-MM-dd HH:mm:ss") & "', " & _
                        " [status] = 0 " & _
                        " WHERE IDDetail = '" & ID.ToString & "' And [status] <>0; "

            If SQLExecuteNonQuery(sqlstring) > 0 Then
                load_grid_kapal()
                ltrInfo.Text = "<div class=""alert alert-danger alert-dismissible"" id=""info-alert""><button type=""button"" class=""close"" data-dismiss=""alert"" aria-hidden=""true"">&times;</button><h4><i class=""icon fa fa-check""></i> Penghapusan Berhasil!</h4></div>"
                Clear()
            End If

        Catch ex As Exception
            Throw New Exception("Error Delete : " & ex.ToString)
        End Try

    End Sub

    Private Sub Update(ByVal ID As String)
        Try
            sqlstring = " UPDATE Kapal" & _
                        " SET " & _
                        " Nama_Kapal = '" & TxtNamaKapal.Text.ToString.Replace("'", "''") & "', " & _
                        " Nahkoda_Kapal = '" & TxtNahkoda.Text.ToString.Replace("'", "''") & "', " & _
                        " Alias_Kapal = '" & TxtSingkatanKapal.Text.ToString.Replace("'", "''") & "', " & _
                        " KeteranganKapal = '" & DDLMilik.SelectedValue & "', " & _
                        " LastModified = '" & Now.ToString("yyyy-MM-dd HH:mm:ss") & "', " & _
                        " [status] = 1 " & _
                        " WHERE IDDetail = '" & ID.ToString & "' and status <>0; "

            If SQLExecuteNonQuery(sqlstring) > 0 Then
                load_grid_kapal()
                btSimpan.Text = "Simpan"
                ltrInfo.Text = "<div class=""alert alert-success alert-dismissible"" id=""info-alert""><button type=""button"" class=""close"" data-dismiss=""alert"" aria-hidden=""true"">&times;</button><h4><i class=""icon fa fa-check""></i> Pembaharuan Berhasil!</h4></div>"
                Clear()
            End If

        Catch ex As Exception
            Throw New Exception("Error Update : " & ex.ToString)
        End Try
    End Sub
    Private Sub Clear()
        TxtNahkoda.Text = ""
        TxtNamaKapal.Text = ""
        TxtSingkatanKapal.Text = ""

    End Sub
    Private Sub clear_label()
        lInfo.Text = ""
        lInfo.Visible = False
        linfoberhasil.Text = ""
        linfoberhasil.Visible = False
    End Sub
#End Region
#Region "BUTTON"
    Protected Sub btSimpan_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btSimpan.Click
        Try
            If hfMode.Value = "Insert" Then
                If Validation() Then
                    If Input_Validation() Then
                        Insert()
                    End If
                End If
            Else
                If Validation() Then
                    Update(hfID.Value)
                End If
            End If
        Catch ex As Exception
            Response.Write(ex.ToString)
        End Try

    End Sub

    Protected Sub btBatal_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btBatal.Click
        clear_label()
        Clear()
    End Sub
#End Region

End Class