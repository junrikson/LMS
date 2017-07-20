Imports System.Data
Imports System.Data.SqlClient
Partial Public Class container
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
            Page.Title = "Master Kontainer - Logistics Management System"
            ltrInfo.Text = ""

            If Session("userID") = Nothing Then
                FormsAuthentication.SignOut()
                Response.Redirect("login.aspx")
            End If

            If Not Page.IsPostBack Then
                load_grid()
                hfMode.Value = "Insert"
            End If

            If Not Session("Grid_Kontainer") Is Nothing Then
                Grid_Kontainer.DataSource = CType(Session("Grid_Kontainer"), DataTable)
                Grid_Kontainer.DataBind()
            End If
        Catch ex As Exception
            Throw New Exception(ex.ToString)
        End Try

    End Sub


#End Region

#Region "GRID"
    Private Sub load_grid()
        sqlstring = " SELECT ID, code, name, note from masterContainer WHERE status = 1 ORDER BY ID DESC"
        DS = SQLExecuteQuery(sqlstring)
        DT = DS.Tables(0)

        If DT.Rows.Count > 0 Then

            Session("Grid_Kontainer") = DT
            Grid_Kontainer.DataSource = DT
            Grid_Kontainer.KeyFieldName = "ID"
            Grid_Kontainer.DataBind()
        Else
            Grid_Kontainer.DataSource = Nothing
            Grid_Kontainer.DataBind()
        End If
    End Sub

    Private Sub Grid_Kontainer_RowCommand(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewRowCommandEventArgs) Handles Grid_Kontainer.RowCommand
        Try
            clear_label()
            Select Case e.CommandArgs.CommandName
                Case "Delete"
                    Dim hsl As Integer = 0

                    hsl = CekUsingNumber("containerTransaction", "codeContainer", Grid_Kontainer.GetRowValues(e.VisibleIndex, "code").ToString)

                    If hsl = 0 Then
                        Delete(Grid_Kontainer.GetRowValues(e.VisibleIndex, "ID").ToString)
                    Else
                        ltrInfo.Text = "<div class=""alert alert-danger alert-dismissible"" id=""info-alert""><button type=""button"" class=""close"" data-dismiss=""alert"" aria-hidden=""true"">&times;</button><h4><i class=""icon fa fa-warning""></i> Tidak bisa dihapus, sudah pernah digunakan!</h4></div>"
                    End If
                Case "Edit"
                    btSimpan.Text = "Update"
                    hfMode.Value = "Update"
                    TxtCode.Text = Grid_Kontainer.GetRowValues(e.VisibleIndex, "code").ToString
                    TxtName.Text = Grid_Kontainer.GetRowValues(e.VisibleIndex, "name").ToString
                    TxtNote.Text = Grid_Kontainer.GetRowValues(e.VisibleIndex, "note").ToString
                    hfID.Value = Grid_Kontainer.GetRowValues(e.VisibleIndex, "ID").ToString
            End Select
        Catch ex As Exception
            Throw New Exception("Error Grid_Kontainer_RowCommand : " & ex.ToString)
        End Try
    End Sub

    Private Sub Grid_Kontainer_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Grid_Kontainer.PreRender
        If Not Page.IsPostBack Then
            Grid_Kontainer.FocusedRowIndex = -1
        End If
    End Sub
#End Region

#Region "VALIDATION"
    Private Function Validation() As Boolean
        Try
            clear_label()
            If TxtCode.Text.Trim = "" Then
                ltrInfo.Text = "<div class=""alert alert-danger alert-dismissible"" id=""info-alert""><button type=""button"" class=""close"" data-dismiss=""alert"" aria-hidden=""true"">&times;</button><h4><i class=""icon fa fa-warning""></i> Kode kontainer harus diisi!</h4></div>"
                Return False
            End If

            If TxtName.Text.Trim = "" Then
                ltrInfo.Text = "<div class=""alert alert-danger alert-dismissible"" id=""info-alert""><button type=""button"" class=""close"" data-dismiss=""alert"" aria-hidden=""true"">&times;</button><h4><i class=""icon fa fa-warning""></i> Nama kontainer harus diisi!</h4></div>"
                Return False
            End If

            Return True
        Catch ex As Exception
            Response.Write(ex.ToString)
        End Try

    End Function

    Private Function Input_Validation() As Boolean
        sqlstring = "SELECT " & _
                    "ID FROM masterContainer " & _
                    "WHERE " & _
                    "code = '" & TxtCode.Text.Replace("'", "''") & "' " & _
                    "AND " & _
                    "[status] = 1 "

        result = SQLExecuteScalar(sqlstring)
        If result <> "" Then
            ltrInfo.Text = "<div class=""alert alert-danger alert-dismissible"" id=""info-alert""><button type=""button"" class=""close"" data-dismiss=""alert"" aria-hidden=""true"">&times;</button><h4><i class=""icon fa fa-warning""></i> Kode kontainer sudah terdaftar!</h4></div>"
            TxtCode.Focus()
            Return False
        End If

        Return True
    End Function
#End Region

#Region "METHOD"
    Private Sub Insert()
        Try
            sqlstring = " INSERT Into masterContainer (code, name, note ,[status], userID) VALUES (" & _
                        " '" & TxtCode.Text.ToString.Replace("'", "''") & "','" & TxtName.Text.ToString.Replace("'", "''") & "' , '" & TxtNote.Text.ToString.Replace("'", "''") & "', 1, '" & Session("UserID").ToString & "');"

            If SQLExecuteNonQuery(sqlstring) > 0 Then
                load_grid()
                ltrInfo.Text = "<div class=""alert alert-info alert-dismissible"" id=""info-alert""><button type=""button"" class=""close"" data-dismiss=""alert"" aria-hidden=""true"">&times;</button><h4><i class=""icon fa fa-check""></i> Penambahan Data Berhasil!</h4></div>"
                Clear()
            End If

        Catch ex As Exception
            Throw New Exception("Error Insert : " & ex.ToString)
        End Try
    End Sub

    Private Sub Delete(ByVal ID As String)
        Try
            sqlstring = " UPDATE masterContainer " & _
                        " SET " & _
                        " lastModified = '" & Now.ToString("yyyy-MM-dd HH:mm:ss") & "', " & _
                        " [status] = 0 " & _
                        " WHERE ID = '" & ID.ToString & "' And [status] <> 0; "

            If SQLExecuteNonQuery(sqlstring) > 0 Then
                load_grid()
                ltrInfo.Text = "<div class=""alert alert-danger alert-dismissible"" id=""info-alert""><button type=""button"" class=""close"" data-dismiss=""alert"" aria-hidden=""true"">&times;</button><h4><i class=""icon fa fa-check""></i> Penghapusan Berhasil!</h4></div>"
                Clear()
            End If

        Catch ex As Exception
            Throw New Exception("Error Delete : " & ex.ToString)
        End Try

    End Sub

    Private Sub Update(ByVal ID As String)
        Try
            sqlstring = " UPDATE masterContainer" & _
                        " SET " & _
                        " code = '" & TxtCode.Text.ToString.Replace("'", "''") & "', " & _
                        " name = '" & TxtName.Text.ToString.Replace("'", "''") & "', " & _
                        " note = '" & TxtNote.Text.ToString.Replace("'", "''") & "', " & _
                        " LastModified = '" & Now.ToString("yyyy-MM-dd HH:mm:ss") & "', " & _
                        " [status] = 1 " & _
                        " WHERE ID = '" & ID.ToString & "' and status <> 0; "

            If SQLExecuteNonQuery(sqlstring) > 0 Then
                load_grid()
                btSimpan.Text = "Simpan"
                ltrInfo.Text = "<div class=""alert alert-success alert-dismissible"" id=""info-alert""><button type=""button"" class=""close"" data-dismiss=""alert"" aria-hidden=""true"">&times;</button><h4><i class=""icon fa fa-check""></i> Pembaharuan Berhasil!</h4></div>"
                Clear()
            End If

        Catch ex As Exception
            Throw New Exception("Error Update : " & ex.ToString)
        End Try
    End Sub
    Private Sub Clear()
        TxtCode.Text = ""
        TxtName.Text = ""
        TxtNote.Text = ""
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