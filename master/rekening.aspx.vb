Partial Public Class rekening
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
            Page.Title = "Master Rekening - Logistics Management System"
            ltrInfo.Text = ""

            If Session("userID") = Nothing Then
                FormsAuthentication.SignOut()
                Response.Redirect("login.aspx")
            End If

            If Not Page.IsPostBack Then
                Load_Grid()
                hfMode.Value = "Insert"
            End If

            If Not Session("GridRekening") Is Nothing Then
                GridView_satuanother.DataSource = CType(Session("GridRekening"), DataTable)
                GridView_satuanother.DataBind()
            End If
        Catch ex As Exception
            Response.Write("Error Page Load : " & ex.ToString)
        End Try
    End Sub
#End Region

#Region "GRID"
    Private Sub Load_Grid()
        Try
            sqlstring = "SELECT IDDetail, AN,NoRek,NamaBank from MasterRekening where [status] = 1"
            DS = SQLExecuteQuery(sqlstring)
            DT = DS.Tables(0)


            With iDT.Columns
                .Add(New DataColumn("ID", GetType(String)))
                .Add(New DataColumn("AN", GetType(String)))
                .Add(New DataColumn("NoRek", GetType(String)))
                .Add(New DataColumn("NamaBank", GetType(String)))
            End With

            For i As Integer = 0 To DT.Rows.Count - 1
                With DT.Rows(i)
                    DR = iDT.NewRow
                    DR("ID") = .Item("IDDetail").ToString
                    DR("AN") = .Item("AN").ToString
                    DR("NoRek") = .Item("NoRek").ToString
                    DR("NamaBank") = .Item("NamaBank").ToString
                    iDT.Rows.Add(DR)
                End With
            Next

            Session("GridRekening") = iDT
            GridView_satuanother.DataSource = iDT
            GridView_satuanother.DataBind()


        Catch ex As Exception
            Throw New Exception("Error load grid rekening : " & ex.ToString)
        End Try
    End Sub
#End Region

#Region "VALIDATION"
    Private Function Validation() As Boolean
        Try
            If hfMode.Value = "Insert" Then
                If tbnorek.Text.Trim = "" Then
                    ltrInfo.Text = "<div class=""alert alert-danger alert-dismissible"" id=""info-alert""><button type=""button"" class=""close"" data-dismiss=""alert"" aria-hidden=""true"">&times;</button><h4><i class=""icon fa fa-warning""></i> Nomor rekening harus diisi!</h4></div>"
                    Return False
                End If

                If tbnama.Text.Trim = "" Then
                    ltrInfo.Text = "<div class=""alert alert-danger alert-dismissible"" id=""info-alert""><button type=""button"" class=""close"" data-dismiss=""alert"" aria-hidden=""true"">&times;</button><h4><i class=""icon fa fa-warning""></i> Nama harus diisi!</h4></div>"
                    Return False
                End If

                If tbbank.Text.Trim = "" Then
                    ltrInfo.Text = "<div class=""alert alert-danger alert-dismissible"" id=""info-alert""><button type=""button"" class=""close"" data-dismiss=""alert"" aria-hidden=""true"">&times;</button><h4><i class=""icon fa fa-warning""></i> Nama bank harus diisi!</h4></div>"
                    Return False
                End If


                sqlstring = "SELECT ID From MasterRekening WHERE NoRek = '" & tbnorek.Text.Replace("'", "''") & "' AND [status] = 1"
                result = SQLExecuteScalar(sqlstring)

                If result.ToString <> "" Then
                    ltrInfo.Text = "<div class=""alert alert-danger alert-dismissible"" id=""info-alert""><button type=""button"" class=""close"" data-dismiss=""alert"" aria-hidden=""true"">&times;</button><h4><i class=""icon fa fa-warning""></i> Nomor rekening sudah ada!</h4></div>"
                    tbnorek.Focus()
                    Return False
                End If

            End If

            If hfMode.Value = "Update" Then
                If tbnorek.Text.Trim = "" Then
                    ltrInfo.Text = "<div class=""alert alert-danger alert-dismissible"" id=""info-alert""><button type=""button"" class=""close"" data-dismiss=""alert"" aria-hidden=""true"">&times;</button><h4><i class=""icon fa fa-warning""></i> Nomor rekening harus diisi!</h4></div>"
                    Return False
                End If
            End If

            Return True
        Catch ex As Exception
            Throw New Exception("Error Function Validasi : " & ex.ToString)
        End Try
    End Function
#End Region

#Region "METHOD"

    Private Sub Insert()
        Dim iddetail As Integer
        Dim cekstring As String
        Dim cekdt As DataTable

        Try
            cekstring = "select id from MasterRekening where status = 1 "
            cekdt = SQLExecuteQuery(cekstring).Tables(0)
            If cekdt.Rows.Count > 0 Then
                iddetail = getDetailIDMaster("MasterRekening") + 1
            Else
                iddetail = 1
            End If
            sqlstring = " INSERT INTO MasterRekening " & _
                        " (IDDetail,NamaBank,AN,NoRek, UserName, [status]) VALUES " & _
                        " ( " & iddetail & ",'" & tbbank.Text.Replace("'", "''") & "','" & tbnama.Text.Replace("'", "''") & "','" & tbnorek.Text.Replace("'", "''") & "', '" & Session("UserId") & "', 1)"

            If SQLExecuteNonQuery(sqlstring) > 0 Then
                Load_Grid()
                ltrInfo.Text = "<div class=""alert alert-info alert-dismissible"" id=""info-alert""><button type=""button"" class=""close"" data-dismiss=""alert"" aria-hidden=""true"">&times;</button><h4><i class=""icon fa fa-check""></i> Penambahan Data Berhasil!</h4></div>"
                clear()
            End If

        Catch ex As Exception
            Throw New Exception("Error Method insert : " & ex.ToString)
        End Try

    End Sub

    Private Sub Update(ByVal id As String)
        Try
            sqlstring = " UPDATE MasterRekening " & _
                        " SET NoRek = '" & tbnorek.Text.Replace("'", "''") & "', " & _
                        " NamaBank = '" & tbbank.Text.Replace("'", "''") & "', " & _
                        " AN = '" & tbnama.Text.Replace("'", "''") & "', " & _
                        " UserName = '" & Session("UserId") & "', " & _
                        " LastModified = '" & Now.ToString("yyyy-MM-dd HH:mm:ss") & "' " & _
                        " WHERE IDDetail = " & id.ToString & " and status <>0"

            If SQLExecuteNonQuery(sqlstring) > 0 Then
                Load_Grid()
                btSimpan.Text = "Simpan"
                ltrInfo.Text = "<div class=""alert alert-success alert-dismissible"" id=""info-alert""><button type=""button"" class=""close"" data-dismiss=""alert"" aria-hidden=""true"">&times;</button><h4><i class=""icon fa fa-check""></i> Pembaharuan Berhasil!</h4></div>"
                clear()
            End If
        Catch ex As Exception
            Throw New Exception("Error function Update : " & ex.ToString)
        End Try
    End Sub
    Private Sub delete(ByVal id As String)
        Try
            sqlstring = "UPDATE MasterRekening " & _
                        "SET UserName = '" & Session("UserId") & "', " & _
                        "LastModified = '" & Now.ToString("yyyy-MM-dd HH:mm:ss") & "', " & _
                        "[status] = 0 " & _
                        "WHERE IDDetail = " & id.ToString & " and status <>0"

            If SQLExecuteNonQuery(sqlstring) > 0 Then
                Load_Grid()
                ltrInfo.Text = "<div class=""alert alert-danger alert-dismissible"" id=""info-alert""><button type=""button"" class=""close"" data-dismiss=""alert"" aria-hidden=""true"">&times;</button><h4><i class=""icon fa fa-check""></i> Penghapusan Berhasil!</h4></div>"
                clear()
            End If
        Catch ex As Exception
            Throw New Exception("Error function delete : " & ex.ToString)
        End Try
    End Sub

    Private Sub clear()
        Try
            tbnorek.Text = ""
            tbnama.Text = ""
            tbbank.Text = ""
            hfMode.Value = "Insert"
            hfID.Value = ""
            tbnorek.Focus()
        Catch ex As Exception
            Throw New Exception("Error Function Clear : " & ex.ToString)
        End Try
    End Sub

    Private Sub clear_label()
        lInfo.Visible = False
        lInfo.Text = ""
        linfoberhasil.Visible = False
        linfoberhasil.Text = ""
    End Sub

#End Region

#Region "BUTTON"

    Protected Sub btSimpan_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btSimpan.Click
        Try
            If Validation() Then
                If hfMode.Value = "Insert" Then
                    Insert()
                Else
                    Update(hfID.Value)
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



    Private Sub GridView_satuanother_RowCommand(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewRowCommandEventArgs) Handles GridView_satuanother.RowCommand
        Try
            Select Case e.CommandArgs.CommandName
                Case "Edit"
                    hfMode.Value = "Update"
                    btSimpan.Text = "Update"
                    hfID.Value = GridView_satuanother.GetRowValues(e.VisibleIndex, "ID").ToString
                    tbnama.Text = GridView_satuanother.GetRowValues(e.VisibleIndex, "AN").ToString
                    tbnorek.Text = GridView_satuanother.GetRowValues(e.VisibleIndex, "NoRek").ToString
                    tbbank.Text = GridView_satuanother.GetRowValues(e.VisibleIndex, "NamaBank").ToString

                Case "Delete"
                    delete(GridView_satuanother.GetRowValues(e.VisibleIndex, "ID").ToString)
            End Select
        Catch ex As Exception
            Response.Write("Error Row Command : " & ex.ToString)
        End Try
    End Sub
End Class