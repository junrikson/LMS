Imports System.Data
Imports System.Data.SqlClient
Partial Public Class kondisi
    Inherits System.Web.UI.Page
    Private DS As DataSet
    Private DT As DataTable
    Private DR As DataRow
    Private sqlstring As String
    Dim iDT As New DataTable
    Dim hasil As Integer
    Dim result As String

#Region "Page Load"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Page.Title = "Master Kondisi - Logistics Management System"
            ltrInfo.Text = ""

            If Session("userID") = Nothing Then
                FormsAuthentication.SignOut()
                Response.Redirect("login.aspx")
            End If

            If Not Page.IsPostBack Then
                load_grid_kondisi()
                hfMode.Value = "Insert"
            End If

            If Not Session("GridViewKondisi") Is Nothing Then
                GridView_Kondisi.DataSource = CType(Session("GridViewKondisi"), DataTable)
                GridView_Kondisi.DataBind()
            End If
        Catch ex As Exception
            Response.Write("Error page load : " & ex.ToString)
        End Try
    End Sub
#End Region

#Region "Button"
    Protected Sub btSimpan_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btSimpan.Click
        Try
            If Validasi() Then
                If hfMode.Value = "Insert" Then
                    Insert()
                Else
                    Update(hfID.Value)
                End If
            End If
        Catch ex As Exception
            Response.Write("<b>Error button simpan :</b>" & ex.ToString)
        End Try
    End Sub

    Protected Sub btBatal_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btBatal.Click
        Try
            clear()
        Catch ex As Exception
            Response.Write("Error button Batal " & ex.ToString)
        End Try
    End Sub

    Private Sub GridView_Kondisi_RowCommand(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewRowCommandEventArgs) Handles GridView_Kondisi.RowCommand
        Try
            Select Case e.CommandArgs.CommandName
                Case "Edit"
                    btSimpan.Text = "Update"
                    hfMode.Value = "Update"
                    hfID.Value = GridView_Kondisi.GetRowValues(e.VisibleIndex, "ID").ToString
                    tbkondisi.Text = GridView_Kondisi.GetRowValues(e.VisibleIndex, "namakondisi").ToString

                Case "Delete"
                    delete(GridView_Kondisi.GetRowValues(e.VisibleIndex, "ID").ToString)

            End Select
        Catch ex As Exception
            Response.Write("<b>Error row command :</b>" & ex.ToString)
        End Try
    End Sub

#End Region

#Region "Methods"
    Private Sub Insert()
        Try
            sqlstring = "INSERT INTO KondisiPengiriman " & _
                        "(Nama_Kondisi, UserName, [status]) VALUES " & _
                        "('" & tbkondisi.Text.Replace("'", "''") & "', '" & Session("UserId") & "', 1)"

            If SQLExecuteNonQuery(sqlstring) > 0 Then
                load_grid_kondisi()
                ltrInfo.Text = "<div class=""alert alert-info alert-dismissible"" id=""info-alert""><button type=""button"" class=""close"" data-dismiss=""alert"" aria-hidden=""true"">&times;</button><h4><i class=""icon fa fa-check""></i> Penambahan Data Berhasil!</h4></div>"
                clear()
            End If

        Catch ex As Exception
            Throw New Exception("<b>Error function insert :</b> " & ex.ToString)
        End Try
    End Sub

    Private Sub Update(ByVal id As String)
        Try
            sqlstring = " UPDATE KondisiPengiriman " & _
                        " SET Nama_Kondisi = '" & tbkondisi.Text.Replace("'", "''") & "', " & _
                        " UserName = '" & Session("UserId") & "', " & _
                        " LastModified = '" & Now.ToString("yyyy-MM-dd HH:mm:ss") & "' " & _
                        " WHERE ID = " & id.ToString & " "

            If SQLExecuteNonQuery(sqlstring) > 0 Then
                load_grid_kondisi()
                btSimpan.Text = "Simpan"
                ltrInfo.Text = "<div class=""alert alert-success alert-dismissible"" id=""info-alert""><button type=""button"" class=""close"" data-dismiss=""alert"" aria-hidden=""true"">&times;</button><h4><i class=""icon fa fa-check""></i> Pembaharuan Berhasil!</h4></div>"
                clear()
            End If
        Catch ex As Exception
            Throw New Exception("<b>Error function Update :</b> " & ex.ToString)
        End Try
    End Sub

    Private Sub delete(ByVal id As String)
        Try
            sqlstring = "UPDATE KondisiPengiriman " & _
                        "SET UserName = '" & Session("UserId") & "', " & _
                        "LastModified = '" & Now.ToString("yyyy-MM-dd HH:mm:ss") & "', " & _
                        "[status] = 0 " & _
                        "WHERE ID = " & id.ToString & ""
            
            If SQLExecuteNonQuery(sqlstring) > 0 Then
                load_grid_kondisi()
                ltrInfo.Text = "<div class=""alert alert-danger alert-dismissible"" id=""info-alert""><button type=""button"" class=""close"" data-dismiss=""alert"" aria-hidden=""true"">&times;</button><h4><i class=""icon fa fa-check""></i> Penghapusan Berhasil!</h4></div>"
                clear()
            End If
        Catch ex As Exception
            Throw New Exception("Error function delete : " & ex.ToString)
        End Try
    End Sub
    Private Sub clear_label()
        lInfo.Visible = False
        lInfo.Text = ""
        linfoberhasil.Visible = False
        linfoberhasil.Text = ""
    End Sub
    Private Sub clear()
        Try
            tbkondisi.Text = ""
            hfMode.Value = "Insert"
            lInfo.Visible = False
            lInfo.Text = ""
            linfoberhasil.Visible = False
            linfoberhasil.Text = ""
            hfID.Value = ""
            tbkondisi.Focus()
        Catch ex As Exception
            Throw New Exception("Error Function Clear : " & ex.ToString)
        End Try
    End Sub
#End Region

#Region "Load Grid"
    Private Sub load_grid_kondisi()
        Try
            sqlstring = "SELECT ID, Nama_Kondisi from KondisiPengiriman where [status] = 1"
            DS = SQLExecuteQuery(sqlstring)
            DT = DS.Tables(0)

            With iDT.Columns
                .Add(New DataColumn("ID", GetType(String)))
                .Add(New DataColumn("namakondisi", GetType(String)))
            End With

            For i As Integer = 0 To DT.Rows.Count - 1
                With DT.Rows(i)
                    DR = iDT.NewRow
                    DR("ID") = .Item("ID").ToString
                    DR("namakondisi") = .Item("Nama_Kondisi").ToString
                    iDT.Rows.Add(DR)
                End With
            Next

            Session("GridViewKondisi") = iDT
            GridView_Kondisi.DataSource = iDT
            GridView_Kondisi.DataBind()

        Catch ex As Exception
            Throw New Exception("<b>Error load grid kondisi : </b> " & ex.ToString)
        End Try
    End Sub
#End Region

#Region "Validasi"
    Private Function Validasi() As Boolean
        Try
            clear_label()
            If hfMode.Value = "Insert" Then
                If tbkondisi.Text.Trim = "" Then
                    ltrInfo.Text = "<div class=""alert alert-danger alert-dismissible"" id=""info-alert""><button type=""button"" class=""close"" data-dismiss=""alert"" aria-hidden=""true"">&times;</button><h4><i class=""icon fa fa-warning""></i> Nama kondisi masih kosong!</h4></div>"
                    Return False
                End If

                sqlstring = "SELECT ID FROM KondisiPengiriman " & _
                            "WHERE Nama_Kondisi = '" & tbkondisi.Text.Replace("'", "''") & "' " & _
                            "AND [status] = 1 "
                result = SQLExecuteScalar(sqlstring)

                If result <> "" Then
                    ltrInfo.Text = "<div class=""alert alert-danger alert-dismissible"" id=""info-alert""><button type=""button"" class=""close"" data-dismiss=""alert"" aria-hidden=""true"">&times;</button><h4><i class=""icon fa fa-warning""></i> Nama kondisi sudah ada!</h4></div>"
                    Return False
                End If

            End If

            If hfMode.Value = "Update" Then
                If tbkondisi.Text.Trim = "" Then
                    ltrInfo.Text = "<div class=""alert alert-danger alert-dismissible"" id=""info-alert""><button type=""button"" class=""close"" data-dismiss=""alert"" aria-hidden=""true"">&times;</button><h4><i class=""icon fa fa-warning""></i> Nama kondisi masih kosong!</h4></div>"
                    Return False
                End If
            End If


            Return True
        Catch ex As Exception
            Throw New Exception("Error function Validasi : " & ex.ToString)
        End Try
    End Function
#End Region

End Class