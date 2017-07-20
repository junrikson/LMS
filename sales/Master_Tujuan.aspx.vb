Imports System.Data
Imports System.Data.SqlClient
Partial Public Class Master_Tujuan
    Inherits System.Web.UI.Page
    Private DS As DataSet
    Private DT As DataTable
    Private DR As DataRow
    Private sqlstring As String
    Dim iDT As New DataTable
    Dim hasil As Integer
    Dim result As String
#Region "PAGE"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Session("UserID") = Nothing Then
                FormsAuthentication.SignOut()
                Response.Redirect("Index.aspx")
            End If

            If Not Page.IsPostBack Then
                Load_Grid_Tujuan()
                hfMode.Value = "Insert"
            End If

            If Not Session("Gridview_Tujuan") Is Nothing Then
                Gridview_Tujuan.DataSource = CType(Session("Gridview_Tujuan"), DataTable)
                Gridview_Tujuan.DataBind()
            End If

        Catch ex As Exception
            Response.Write("Error Page Load : " & ex.ToString)
        End Try
    End Sub

#End Region


#Region "GRID"
    Private Sub Load_Grid_Tujuan()
        Try
            sqlstring = "SELECT ID, Tujuan,Pelabuhan from MasterTujuan where [status] = 1"
            DS = SQLExecuteQuery(sqlstring)
            DT = DS.Tables(0)


            With iDT.Columns
                .Add(New DataColumn("ID", GetType(String)))
                .Add(New DataColumn("Tujuan", GetType(String)))
                .Add(New DataColumn("Pelabuhan", GetType(String)))
            End With

            For i As Integer = 0 To DT.Rows.Count - 1
                With DT.Rows(i)
                    DR = iDT.NewRow
                    DR("ID") = .Item("ID").ToString
                    DR("Tujuan") = .Item("Tujuan").ToString
                    DR("Pelabuhan") = .Item("Pelabuhan").ToString
                    iDT.Rows.Add(DR)
                End With
            Next

            Session("Gridview_Tujuan") = iDT
            Gridview_Tujuan.DataSource = iDT
            Gridview_Tujuan.DataBind()


        Catch ex As Exception
            Throw New Exception("Error load grid satuan : " & ex.ToString)
        End Try
    End Sub

    Private Sub Gridview_Tujuan_RowCommand(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewRowCommandEventArgs) Handles Gridview_Tujuan.RowCommand
        Try
            Select Case e.CommandArgs.CommandName
                Case "Edit"
                    hfMode.Value = "Update"
                    hfID.Value = Gridview_Tujuan.GetRowValues(e.VisibleIndex, "ID").ToString
                    TxtTujuan.Text = Gridview_Tujuan.GetRowValues(e.VisibleIndex, "Tujuan").ToString
                    TxtPelabuhan.Text = Gridview_Tujuan.GetRowValues(e.VisibleIndex, "Pelabuhan").ToString

                Case "Delete"
 


                    delete(Gridview_Tujuan.GetRowValues(e.VisibleIndex, "ID").ToString)
            End Select
        Catch ex As Exception
            Response.Write("Error Row Command : " & ex.ToString)
        End Try
    End Sub
#End Region

#Region "METHOD"
    Private Sub Insert()
        Try
            sqlstring = " INSERT INTO MasterTujuan " & _
                        " (Tujuan,Pelabuhan, UserName, [status]) VALUES " & _
                        " ('" & TxtTujuan.Text.Replace("'", "''") & "','" & Txtpelabuhan.Text.Replace("'", "''") & "',  '" & Session("UserId") & "', 1)"
            hasil = SQLExecuteNonQuery(sqlstring)

            If hasil > 0 Then
                Load_Grid_Tujuan()
                clear()
            End If

        Catch ex As Exception
            Throw New Exception("Error Method insert : " & ex.ToString)
        End Try

    End Sub
    Private Sub Update(ByVal id As String)
        Try
            sqlstring = " UPDATE MasterTujuan " & _
                        " SET Tujuan = '" & TxtTujuan.Text.Replace("'", "''") & "', " & _
                        " Pelabuhan = '" & Txtpelabuhan.Text.Replace("'", "''") & "', " & _
                        " UserName = '" & Session("UserId") & "', " & _
                        " LastModified = '" & Now.ToString & "' " & _
                        " WHERE ID = " & id.ToString & " "
            hasil = SQLExecuteNonQuery(sqlstring)

            If hasil > 0 Then
                Load_Grid_Tujuan()
                clear()
            End If
        Catch ex As Exception
            Throw New Exception("Error function Update : " & ex.ToString)
        End Try
    End Sub
    Private Sub delete(ByVal id As String)
        Try
            sqlstring = "UPDATE MasterTujuan " & _
                        "SET UserName = '" & Session("UserId") & "', " & _
                        "LastModified = '" & Now.ToString & "', " & _
                        "[status] = 0 " & _
                        "WHERE ID = " & id.ToString & ""
            hasil = SQLExecuteNonQuery(sqlstring)

            If hasil > 0 Then
                Load_Grid_Tujuan()
                clear()
            End If
        Catch ex As Exception
            Throw New Exception("Error function delete : " & ex.ToString)
        End Try
    End Sub

    Private Sub clear()
        Try
            TxtTujuan.Text = ""
            Txtpelabuhan.Text = ""
            hfMode.Value = "Insert"
            lInfo.Visible = False
            lInfo.Text = ""
            linfoberhasil.Visible = False
            linfoberhasil.Text = ""
            hfID.Value = ""
            TxtTujuan.Focus()
        Catch ex As Exception
            Throw New Exception("Error Function Clear : " & ex.ToString)
        End Try
    End Sub

    Private Sub clear_Label()
        lInfo.Visible = False
        lInfo.Text = ""
        linfoberhasil.Visible = False
        linfoberhasil.Text = ""
    End Sub
#End Region

#Region "VALIDATION"
    Private Function Validation() As Boolean
        Try
            If hfMode.Value = "Insert" Then
                If TxtTujuan.Text.Trim = "" Then
                    clear_Label()
                    lInfo.Visible = True
                    lInfo.Text = "Nama Tujuan harus di isi"
                    Return False
                End If
                If Txtpelabuhan.Text.Trim = "" Then
                    clear_Label()
                    lInfo.Visible = True
                    lInfo.Text = "Nama pelabuhan harus di isi"
                    Return False
                End If

                sqlstring = "SELECT ID From MasterTujuan WHERE Tujuan = '" & TxtTujuan.Text.Replace("'", "''") & "' AND [status] = 1"
                result = SQLExecuteScalar(sqlstring)

                If result.ToString <> "" Then
                    clear_Label()
                    lInfo.Visible = True
                    lInfo.Text = "Nama Tujuan Sudah ada"
                    TxtTujuan.Focus()
                    Return False
                End If

            End If

            If hfMode.Value = "Update" Then
                If TxtTujuan.Text.Trim = "" Then
                    clear_Label()
                    lInfo.Visible = True
                    lInfo.Text = "Nama Tujuan harus diisi"
                    Return False
                End If
                If Txtpelabuhan.Text.Trim = "" Then
                    clear_Label()
                    lInfo.Visible = True
                    lInfo.Text = "Nama pelabuhan harus di isi"
                    Return False
                End If
            End If

            Return True
        Catch ex As Exception
            Throw New Exception("Error Function Validasi : " & ex.ToString)
        End Try
    End Function
#End Region

#Region "BUTTON"
    Protected Sub btSimpan_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btSimpan.Click
        Try
            If Validation() Then
                If hfMode.Value = "Insert" Then
                    Insert()
                    linfoberhasil.Visible = True
                    linfoberhasil.Text = "Simpan Berhasil"

                Else
                    Update(hfID.Value)
                    linfoberhasil.Visible = True
                    linfoberhasil.Text = "Update Berhasil"

                End If
            End If
        Catch ex As Exception
            Response.Write("Error button save : " & ex.ToString)
        End Try
    End Sub

    Protected Sub btBatal_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btBatal.Click
        Try
            clear()
        Catch ex As Exception
            Response.Write("Error button Batal " & ex.ToString)
        End Try
    End Sub
#End Region


End Class