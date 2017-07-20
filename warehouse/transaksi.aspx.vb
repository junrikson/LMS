Imports System.Data
Imports System.Data.SqlClient
Partial Class transaksi
    Inherits System.Web.UI.Page
    Private DT As DataTable
    Private DS As DataSet
    Private SDR As SqlDataReader
    Private sqlstring As String
    Private result As String
    Private hasil As Integer
#Region "PAGE"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Page.Title = "Transaksi Kontainer - Logistics Management System"
            ltrInfo.Text = ""

            If Session("userID") = Nothing Then
                FormsAuthentication.SignOut()
                Response.Redirect("login.aspx")
            End If

            If Not Page.IsPostBack Then
                Load_Grid()
                load_transaction_code()
                hfMode.Value = "Insert"
                TxtDate.Text = Now.ToString("yyyy-MM-dd")
            End If
        Catch ex As Exception
            Response.Write(ex.ToString)
        End Try
    End Sub
    Private Sub load_transaction_code()
        Dim month, year, tanggal As String
        Dim value As String
        Dim no As Integer

        Try
            month = Date.Today.ToString("MM")
            year = Date.Today.ToString("yy")
            tanggal = Date.Today.ToString("dd")

            sqlstring = "SELECT TOP 1 code FROM containerTransaction " & _
                        "WHERE code LIKE 'TRX/" & year.ToString & month.ToString & tanggal.ToString & "%' and status <> 0" & _
                        "ORDER BY ID DESC"
            result = SQLExecuteScalar(sqlstring)

            If result.ToString <> "" Then
                no = CDbl(Right(result, 4)) + 1
            Else
                no = 1
            End If
            value = "TRX/" & year.ToString & month.ToString & tanggal.ToString & no.ToString("0000")

            LblCode.Visible = True
            LblCode.Text = value.ToString

        Catch ex As Exception
            Response.Write("Error load_transaction_code :<BR>" & ex.ToString)
        End Try
    End Sub
#End Region

#Region "GRID"
    Private Sub Load_Grid()
        Try
            sqlstring = " SELECT ID, code, date, container, seal, sender, receiver, type, brand, weight, note " & _
         "	FROM containerTransaction" & _
         "	WHERE [Status] = 1 " & _
         "	ORDER BY ID DESC "

            DS = SQLExecuteQuery(sqlstring)
            DT = DS.Tables(0)
            Session("Grid_Transaction") = DT
            Grid_Transaction.DataSource = DT
            Grid_Transaction.DataBind()

        Catch ex As Exception
            Response.Write("Load_Grid Exception :<br>" & ex.ToString)
        End Try
    End Sub


    Protected Sub Grid_Transaction_RowCommand(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewRowCommandEventArgs) Handles Grid_Transaction.RowCommand
        Try
            Select Case e.CommandArgs.CommandName
                Case "Edit"
                    btSimpan.Text = "Update"
                    hfMode.Value = "Update"
                    hfID.Value = Grid_Transaction.GetRowValues(e.VisibleIndex, "ID").ToString
                    LblCode.Text = Grid_Transaction.GetRowValues(e.VisibleIndex, "code").ToString
                    TxtDate.Text = CDate(Grid_Transaction.GetRowValues(e.VisibleIndex, "date")).ToString("yyyy-MM-dd")
                    TxtContainer.Text = Grid_Transaction.GetRowValues(e.VisibleIndex, "container").ToString
                    TxtSeal.Text = Grid_Transaction.GetRowValues(e.VisibleIndex, "seal").ToString
                    TxtSender.Text = Grid_Transaction.GetRowValues(e.VisibleIndex, "sender").ToString
                    TxtReceiver.Text = Grid_Transaction.GetRowValues(e.VisibleIndex, "receiver").ToString
                    TxtType.Text = Grid_Transaction.GetRowValues(e.VisibleIndex, "type").ToString
                    TxtBrand.Text = Grid_Transaction.GetRowValues(e.VisibleIndex, "brand").ToString
                    TxtWeight.Text = Grid_Transaction.GetRowValues(e.VisibleIndex, "weight").ToString
                    TxtNote.Text = Grid_Transaction.GetRowValues(e.VisibleIndex, "note").ToString
                Case "Delete"
                    Delete(Grid_Transaction.GetRowValues(e.VisibleIndex, "ID").ToString)
            End Select
        Catch ex As Exception
            Response.Write(ex.ToString)
        End Try


    End Sub
#End Region

#Region "METHOD"
    Private Sub clear()
        Load_Grid()
        load_transaction_code()
        TxtDate.Text = Now.ToString("yyyy-MM-dd")
        TxtContainer.Text = ""
        TxtSeal.Text = ""
        TxtSender.Text = ""
        TxtReceiver.Text = ""
        TxtType.Text = ""
        TxtBrand.Text = ""
        TxtWeight.Text = ""
        TxtNote.Text = ""
        lInfo.Visible = False
        linfoberhasil.Visible = False
    End Sub

    Private Sub Insert()
        Try
            sqlstring = " INSERT INTO containerTransaction ( code, date, container, seal, sender, receiver, type, brand, weight, note, [status], userID) VALUES ( " & _
                " '" & LblCode.Text.ToString & "' , '" & TxtDate.Text.ToString & "' , " & _
                " '" & TxtContainer.Text.ToString & "' , '" & TxtSeal.Text.ToString & "' , " & _
                " '" & TxtSender.Text.ToString & "' , '" & TxtReceiver.Text.ToString & "' , " & _
                " '" & TxtType.Text.ToString & "' , '" & TxtBrand.Text.ToString & "' , " & _
                " '" & TxtWeight.Text.ToString & "' , '" & TxtNote.Text.ToString & "' , " & _
                " 1, '" & Session("UserId").ToString & "' ) ;"

            If SQLExecuteNonQuery(sqlstring) > 0 Then
                Load_Grid()
                ltrInfo.Text = "<div class=""alert alert-info alert-dismissible"" id=""info-alert""><button type=""button"" class=""close"" data-dismiss=""alert"" aria-hidden=""true"">&times;</button><h4><i class=""icon fa fa-check""></i> Penambahan Data Berhasil!</h4></div>"
                clear()
            End If
        Catch ex As Exception
            Response.Write("Error function Insert <BR> " & ex.ToString)
        End Try

    End Sub
    Private Sub Update(ByVal ID As String)
        Try
            sqlstring = " UPDATE containerTransaction SET " & _
                        " date = '" & TxtDate.Text.ToString & "' ," & _
                        " container = '" & TxtContainer.Text.ToString & "' ," & _
                        " seal = '" & TxtSeal.Text.ToString & "' ," & _
                        " sender = '" & TxtSender.Text.ToString & "', " & _
                        " receiver = '" & TxtReceiver.Text.ToString & "', " & _
                        " type = '" & TxtType.Text.ToString & "', " & _
                        " brand = '" & TxtBrand.Text.ToString & "', " & _
                        " weight = '" & TxtWeight.Text.ToString & "', " & _
                        " note = '" & TxtNote.Text.ToString & "', " & _
                        " lastModified = '" & Now.ToString("yyyy-MM-dd HH:mm:ss") & "', " & _
                        " [Status] = 1 " & _
                        " WHERE ID = '" & ID.ToString & "'"

            If SQLExecuteNonQuery(sqlstring) > 0 Then
                Load_Grid()
                btSimpan.Text = "Simpan"
                ltrInfo.Text = "<div class=""alert alert-success alert-dismissible"" id=""info-alert""><button type=""button"" class=""close"" data-dismiss=""alert"" aria-hidden=""true"">&times;</button><h4><i class=""icon fa fa-check""></i> Pembaharuan Berhasil!</h4></div>"
                hfMode.Value = "Insert"
                clear()
            End If
        Catch ex As Exception
            Response.Write("Error function Update <BR> " & ex.ToString)
        End Try
    End Sub
    Private Sub Delete(ByVal ID As String)
        Try
            sqlstring = " Update containerTransaction Set " & _
                        " lastModified = '" & Now.ToString("yyyy-MM-dd HH:mm:ss") & "', " & _
                        " [Status] = 0 " & _
                        " WHERE ID = '" & ID & "'"

            If SQLExecuteNonQuery(sqlstring) > 0 Then
                Load_Grid()
                ltrInfo.Text = "<div class=""alert alert-danger alert-dismissible"" id=""info-alert""><button type=""button"" class=""close"" data-dismiss=""alert"" aria-hidden=""true"">&times;</button><h4><i class=""icon fa fa-check""></i> Penghapusan Berhasil!</h4></div>"
                hfMode.Value = "Insert"
                clear()
            End If
        Catch ex As Exception
            Response.Write("Error function Delete <BR> " & ex.ToString)
        End Try
    End Sub
#End Region

#Region "VALIDATION"
    Private Function Validation() As Boolean
        If TxtDate.Text.Trim = "" Then
            ltrInfo.Text = "<div class=""alert alert-danger alert-dismissible"" id=""info-alert""><button type=""button"" class=""close"" data-dismiss=""alert"" aria-hidden=""true"">&times;</button><h4><i class=""icon fa fa-warning""></i> Nama gudang harus diisi!</h4></div>"
            Return False
        End If
        Return True
    End Function
#End Region

#Region "BUTTON"

    Protected Sub btSimpan_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btSimpan.Click
        If Validation() Then
            If hfMode.Value = "Insert" Then
                    Insert()
            Else
                Update(hfID.Value)
                hfMode.Value = "Insert"
            End If
        End If
    End Sub

    Protected Sub btBatal_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btBatal.Click
        clear()
    End Sub
#End Region

End Class
