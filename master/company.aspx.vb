Imports System.Data
Imports System.Data.SqlClient
Partial Public Class company
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
            Page.Title = "Master Company - Logistics Management System"
            ltrInfo.Text = ""

            If Session("userID") = Nothing Then
                FormsAuthentication.SignOut()
                Response.Redirect("login.aspx")
            End If

            If Not Page.IsPostBack Then
                Load_Grid()
                hfMode.Value = "Insert"
            End If

            If Not Session("GridViewMasterCompany") Is Nothing Then
                GridView_MasterCompany.DataSource = CType(Session("GridViewMasterCompany"), DataTable)
                GridView_MasterCompany.DataBind()
            End If

        Catch ex As Exception
            Response.Write("Error Page Load : " & ex.ToString)
        End Try
    End Sub
#End Region

#Region "Button"
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

    Private Sub GridView_MasterCompany_RowCommand(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewRowCommandEventArgs) Handles GridView_MasterCompany.RowCommand
        Try
            Select Case e.CommandArgs.CommandName
                Case "Edit"
                    btSimpan.Text = "Update"
                    hfMode.Value = "Update"
                    hfID.Value = GridView_MasterCompany.GetRowValues(e.VisibleIndex, "ID").ToString
                    TxtCompanyID.Text = GridView_MasterCompany.GetRowValues(e.VisibleIndex, "companyID").ToString
                    TxtName.Text = GridView_MasterCompany.GetRowValues(e.VisibleIndex, "name").ToString
                    TxtType.Text = GridView_MasterCompany.GetRowValues(e.VisibleIndex, "type").ToString
                    TxtCity.Text = GridView_MasterCompany.GetRowValues(e.VisibleIndex, "city").ToString
                    TxtAddress.Text = GridView_MasterCompany.GetRowValues(e.VisibleIndex, "address").ToString
                    TxtZip.Text = GridView_MasterCompany.GetRowValues(e.VisibleIndex, "zip").ToString
                    TxtPhone1.Text = GridView_MasterCompany.GetRowValues(e.VisibleIndex, "phone1").ToString
                    TxtPhone2.Text = GridView_MasterCompany.GetRowValues(e.VisibleIndex, "phone2").ToString
                    TxtFax.Text = GridView_MasterCompany.GetRowValues(e.VisibleIndex, "fax").ToString
                    TxtEmail.Text = GridView_MasterCompany.GetRowValues(e.VisibleIndex, "email").ToString

                Case "Delete"
                    Dim hsl As Integer

                    hsl = CekUsingNumber("masterUser", "companyID", GridView_MasterCompany.GetRowValues(e.VisibleIndex, "companyID").ToString)


                    If hsl = 0 Then
                        delete(GridView_MasterCompany.GetRowValues(e.VisibleIndex, "ID").ToString)
                    Else
                        ltrInfo.Text = "<div class=""alert alert-danger alert-dismissible"" id=""info-alert""><button type=""button"" class=""close"" data-dismiss=""alert"" aria-hidden=""true"">&times;</button><h4><i class=""icon fa fa-warning""></i> Tidak bisa dihapus, sudah pernah digunakan!</h4></div>"
                    End If

            End Select
        Catch ex As Exception
            Response.Write("Error Row Command : " & ex.ToString)
        End Try
    End Sub
#End Region

#Region "Methods"
    Private Sub Insert()
        Try
            sqlstring = " INSERT INTO masterCompany " & _
                        " (companyID, name, type, city, address, zip, phone1, phone2, fax, email, [status]) VALUES " & _
                        " ('" & TxtCompanyID.Text.Replace("'", "''") & "', '" & TxtName.Text.Replace("'", "''") & "', '" & TxtType.Text.Replace("'", "''") & "', '" & TxtCity.Text.Replace("'", "''") & "', '" & TxtAddress.Text.Replace("'", "''") & "', '" & TxtZip.Text.Replace("'", "''") & "', '" & TxtPhone1.Text.Replace("'", "''") & "', '" & TxtPhone2.Text.Replace("'", "''") & "', '" & TxtFax.Text.Replace("'", "''") & "', '" & TxtEmail.Text.Replace("'", "''") & "', 1)"

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
            sqlstring = " UPDATE masterCompany " & _
                        " SET companyID = '" & TxtCompanyID.Text.Replace("'", "''") & "', " & _
                        " name = '" & TxtName.Text.Replace("'", "''") & "', " & _
                        " type = '" & TxtType.Text.Replace("'", "''") & "', " & _
                        " city = '" & TxtCity.Text.Replace("'", "''") & "', " & _
                        " address = '" & TxtAddress.Text.Replace("'", "''") & "', " & _
                        " zip = '" & TxtZip.Text.Replace("'", "''") & "', " & _
                        " phone1 = '" & TxtPhone1.Text.Replace("'", "''") & "', " & _
                        " phone2 = '" & TxtPhone2.Text.Replace("'", "''") & "', " & _
                        " fax = '" & TxtFax.Text.Replace("'", "''") & "', " & _
                        " email = '" & TxtEmail.Text.Replace("'", "''") & "', " & _
                        " userID = '" & Session("UserId") & "', " & _
                        " LastModified = '" & Now.ToString("yyyy-MM-dd HH:mm:ss") & "' " & _
                        " WHERE ID = " & id.ToString & " and status <>0"

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
            sqlstring = "UPDATE masterCompany " & _
                        "SET userID = '" & Session("userId") & "', " & _
                        "LastModified = '" & Now.ToString("yyyy-MM-dd HH:mm:ss") & "', " & _
                        "[status] = 0 " & _
                        "WHERE ID = " & id.ToString & " and status <>0"

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
            TxtCompanyID.Text = ""
            hfMode.Value = "Insert"
            lInfo.Visible = False
            lInfo.Text = ""
            linfoberhasil.Visible = False
            linfoberhasil.Text = ""
            hfID.Value = ""
            TxtCompanyID.Focus()
        Catch ex As Exception
            Throw New Exception("Error Function Clear : " & ex.ToString)
        End Try
    End Sub
#End Region

#Region "Load Grid"
    Private Sub Load_Grid()
        Try
            sqlstring = "SELECT ID, companyID, name, type, city, address, zip, phone1, phone2, fax, email from masterCompany where [status] = 1"
            DS = SQLExecuteQuery(sqlstring)
            DT = DS.Tables(0)

            With iDT.Columns
                .Add(New DataColumn("ID", GetType(String)))
                .Add(New DataColumn("companyID", GetType(String)))
                .Add(New DataColumn("name", GetType(String)))
                .Add(New DataColumn("type", GetType(String)))
                .Add(New DataColumn("city", GetType(String)))
                .Add(New DataColumn("address", GetType(String)))
                .Add(New DataColumn("zip", GetType(String)))
                .Add(New DataColumn("phone1", GetType(String)))
                .Add(New DataColumn("phone2", GetType(String)))
                .Add(New DataColumn("fax", GetType(String)))
                .Add(New DataColumn("email", GetType(String)))
            End With

            For i As Integer = 0 To DT.Rows.Count - 1
                With DT.Rows(i)
                    DR = iDT.NewRow
                    DR("ID") = .Item("ID").ToString
                    DR("companyID") = .Item("companyID").ToString
                    DR("name") = .Item("name").ToString
                    DR("type") = .Item("type").ToString
                    DR("city") = .Item("city").ToString
                    DR("address") = .Item("address").ToString
                    DR("zip") = .Item("zip").ToString
                    DR("phone1") = .Item("phone1").ToString
                    DR("phone2") = .Item("phone2").ToString
                    DR("fax") = .Item("fax").ToString
                    DR("email") = .Item("email").ToString
                    iDT.Rows.Add(DR)
                End With
            Next

            Session("GridViewMasterCompany") = iDT
            GridView_MasterCompany.DataSource = iDT
            GridView_MasterCompany.DataBind()


        Catch ex As Exception
            Throw New Exception("Error load grid master company : " & ex.ToString)
        End Try
    End Sub
#End Region

#Region "Validasi"
    Private Function Validation() As Boolean
        Try
            If TxtCompanyID.Text.Trim = "" Then
                ltrInfo.Text = "<div class=""alert alert-danger alert-dismissible"" id=""info-alert""><button type=""button"" class=""close"" data-dismiss=""alert"" aria-hidden=""true"">&times;</button><h4><i class=""icon fa fa-warning""></i> Kode Perusahaan harus diisi!</h4></div>"
                Return False
            End If

            If TxtName.Text.Trim = "" Then
                ltrInfo.Text = "<div class=""alert alert-danger alert-dismissible"" id=""info-alert""><button type=""button"" class=""close"" data-dismiss=""alert"" aria-hidden=""true"">&times;</button><h4><i class=""icon fa fa-warning""></i> Nama Perusahaan harus diisi!</h4></div>"
                Return False
            End If

            If hfMode.Value = "Insert" Then
                sqlstring = "SELECT companyID From masterCompany WHERE companyID = '" & TxtCompanyID.Text.Replace("'", "''") & "' AND [status] = 1"
                result = SQLExecuteScalar(sqlstring)

                If result.ToString <> "" Then
                    ltrInfo.Text = "<div class=""alert alert-danger alert-dismissible"" id=""info-alert""><button type=""button"" class=""close"" data-dismiss=""alert"" aria-hidden=""true"">&times;</button><h4><i class=""icon fa fa-warning""></i> Kode Perusahaan sudah ada!</h4></div>"
                    TxtCompanyID.Focus()
                    Return False
                End If
            End If
            Return True
        Catch ex As Exception
            Throw New Exception("Error Function Validasi : " & ex.ToString)
        End Try
    End Function
#End Region

End Class