Imports System.Data
Imports System.Data.SqlClient
Partial Class item
    Inherits System.Web.UI.Page
    Private DS As DataSet
    Private DT As DataTable
    Private DR As DataRow
    Private sqlstring As String
    Private iDT As New DataTable
    Dim hasil As Integer
    Dim result As String
#Region "PAGE LOAD"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Page.Title = "Master Item - Logistics Management System"
            ltrInfo.Text = ""

            If Session("userID") = Nothing Then
                FormsAuthentication.SignOut()
                Response.Redirect("login.aspx")
            End If

            If Not Page.IsPostBack Then
                hfMode.Value = "Insert"
                load_grid()
                LblCode.Visible = True
                LblCode.Text = Load_Item_Code()
                hfCID.Value = ""
            End If

            If Not Session("GridItem_Approve") Is Nothing Then
                GridItem_Approve.DataSource = CType(Session("GridItem_Approve"), DataTable)
                GridItem_Approve.DataBind()
            End If

        Catch ex As Exception
            Response.Write("Error Page Load :<BR>" & ex.ToString)
        End Try
    End Sub

    Private Function Load_Item_Code() As String
        Dim month, year, tanggal As String
        Dim value As String
        Dim no As Integer

        Try
            month = Date.Today.ToString("MM")
            year = Date.Today.ToString("yy")
            tanggal = Date.Today.ToString("dd")

            sqlstring = "SELECT TOP 1 code FROM masterItem " & _
                        "WHERE code LIKE 'IT/" & year.ToString & month.ToString & tanggal.ToString & "%' " & _
                        "ORDER BY ID DESC"
            result = SQLExecuteScalar(sqlstring)

            If result.ToString <> "" Then
                no = CDbl(Right(result, 4)) + 1
            Else
                no = 1
            End If
            value = "IT/" & year.ToString & month.ToString & tanggal.ToString & no.ToString("0000")



            Return value.ToString
        Catch ex As Exception
            Response.Write("Error Load_Quotation_No :<BR>" & ex.ToString)
        End Try
        Return Nothing
    End Function
#End Region

#Region "GRID"
    Private Sub load_grid()
        Try
            sqlstring = " SELECT ID, customer, code, name, weight, length, width, height, quantity, note FROM masterItem " & _
                        " WHERE [status] = 1  ORDER BY ID DESC "
            DS = SQLExecuteQuery(sqlstring)
            DT = DS.Tables(0)

            If DT.Rows.Count > 0 Then
                Session("GridItem_Approve") = DT
                GridItem_Approve.DataSource = DT
                GridItem_Approve.DataBind()
            Else
                GridItem_Approve.DataSource = Nothing
                GridItem_Approve.DataBind()
            End If

        Catch ex As Exception
            Response.Write("Error Load Grid Item:<BR>" & ex.ToString)
        End Try

    End Sub


    Protected Sub GridItem_Approve_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridItem_Approve.PreRender
        Try

            If Not Page.IsPostBack Then
                GridItem_Approve.FocusedRowIndex = -1
            End If

        Catch ex As Exception
            Response.Write("grid_Master_User_PreRender Exception :<br>" & ex.ToString)
        End Try
    End Sub

    Protected Sub GridItem_Approve_RowCommand(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewRowCommandEventArgs) Handles GridItem_Approve.RowCommand
        Try
            Select Case e.CommandArgs.CommandName
                Case "Edit"
                    TxtCustomer.Text = GridItem_Approve.GetRowValues(e.VisibleIndex, "customer").ToString
                    LblCode.Text = GridItem_Approve.GetRowValues(e.VisibleIndex, "code").ToString
                    TxtName.Text = GridItem_Approve.GetRowValues(e.VisibleIndex, "name").ToString
                    TxtWeight.Text = GridItem_Approve.GetRowValues(e.VisibleIndex, "weight").ToString
                    TxtLength.Text = GridItem_Approve.GetRowValues(e.VisibleIndex, "length").ToString
                    TxtWidth.Text = GridItem_Approve.GetRowValues(e.VisibleIndex, "width").ToString
                    TxtHeight.Text = GridItem_Approve.GetRowValues(e.VisibleIndex, "height").ToString
                    TxtQuantity.Text = GridItem_Approve.GetRowValues(e.VisibleIndex, "quantity").ToString
                    TxtNote.Text = GridItem_Approve.GetRowValues(e.VisibleIndex, "note").ToString
                    btSimpan.Text = "Update"
                    hfMode.Value = "Update"
                    hfID.Value = GridItem_Approve.GetRowValues(e.VisibleIndex, "ID").ToString
                Case "Delete"
                    Delete(GridItem_Approve.GetRowValues(e.VisibleIndex, "ID").ToString)
            End Select

        Catch ex As Exception
            Response.Write(ex.ToString)
        End Try
    End Sub

#End Region

#Region "VALIDATION"
    Private Function validation() As Boolean
        clear_label()
        If TxtCustomer.Text.Trim = "" Then
            ltrInfo.Text = "<div class=""alert alert-danger alert-dismissible"" id=""info-alert""><button type=""button"" class=""close"" data-dismiss=""alert"" aria-hidden=""true"">&times;</button><h4><i class=""icon fa fa-warning""></i> Customer belum diisi!</h4></div>"
            TxtCustomer.Focus()
            Return False
        End If

        If TxtName.Text.Trim = "" Then
            ltrInfo.Text = "<div class=""alert alert-danger alert-dismissible"" id=""info-alert""><button type=""button"" class=""close"" data-dismiss=""alert"" aria-hidden=""true"">&times;</button><h4><i class=""icon fa fa-warning""></i> Nama barang belum diisi!</h4></div>"
            TxtName.Focus()
            Return False
        End If

        If LblCode.Text.Trim = "" Then
            ltrInfo.Text = "<div class=""alert alert-danger alert-dismissible"" id=""info-alert""><button type=""button"" class=""close"" data-dismiss=""alert"" aria-hidden=""true"">&times;</button><h4><i class=""icon fa fa-warning""></i> Kode barang error, logout dan login kembali ke system!</h4></div>"
            TxtCustomer.Focus()
            Return False
        End If
        Return True
    End Function
#End Region

#Region "BUTTON"
    Protected Sub btSimpan_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btSimpan.Click
        Try
            If validation() Then
                If hfMode.Value = "Insert" Then
                    Insert()
                Else
                    Update(hfID.Value)
                    hfMode.Value = "Insert"
                End If
            End If
        Catch ex As Exception
            Response.Write("Error BtSimpan :<BR>" & ex.ToString)
        End Try
    End Sub

    Protected Sub btBatal_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btBatal.Click
        Try
            clear()
            clear_label()
        Catch ex As Exception
            Response.Write("Error btBatal :<BR>" & ex.ToString)
        End Try
    End Sub
#End Region

#Region "METHOD"
    Private Sub Insert()
        Try
            sqlstring = " INSERT INTO masterItem " & _
                        " (customer, code, name, weight, length, width, height, quantity, note, userID, [status] ) VALUES " & _
                        " ('" & TxtCustomer.Text.Replace("'", "`") & "', '" & Load_Item_Code() & "', '" & TxtName.Text.Replace("'", "`") & "' ," & _
                        " '" & UbahKomaJdTitik(CekNilai(TxtWeight.Text.Replace("'", "''")).ToString) & "', " & _
                        " '" & UbahKomaJdTitik(CekNilai(TxtLength.Text.Replace("'", "''")).ToString) & "', '" & UbahKomaJdTitik(CekNilai(TxtWidth.Text.Replace("'", "''")).ToString) & "', " & _
                        " '" & UbahKomaJdTitik(CekNilai(TxtHeight.Text.Replace("'", "''")).ToString) & "', '" & TxtQuantity.Text.Replace("'", "''").Replace(",", ".") & "', " & _
                        " '" & TxtNote.Text.Replace("'", "''") & "', '" & Session("UserId").ToString & "', 1)"

            If SQLExecuteNonQuery(sqlstring) > 0 Then
                load_grid()
                ltrInfo.Text = "<div class=""alert alert-info alert-dismissible"" id=""info-alert""><button type=""button"" class=""close"" data-dismiss=""alert"" aria-hidden=""true"">&times;</button><h4><i class=""icon fa fa-check""></i> Penambahan Data Berhasil!</h4></div>"
                clear()
            End If

        Catch ex As Exception
            Response.Write("Error Function Insert() :<BR>" & ex.ToString)
        End Try
    End Sub
    Private Sub Delete(ByVal ID As String)
        Try
            sqlstring = " UPDATE " & _
                  "		masterItem " & _
                  "	    SET " & _
                  "		LastModified = '" & Now.ToString("yyyy-MM-dd HH:mm:ss") & "', " & _
                  "		[status] = 0 " & _
                  "	    WHERE ID = '" & ID.ToString & "' and status <>0; "

            If SQLExecuteNonQuery(sqlstring) > 0 Then
                load_grid()
                ltrInfo.Text = "<div class=""alert alert-danger alert-dismissible"" id=""info-alert""><button type=""button"" class=""close"" data-dismiss=""alert"" aria-hidden=""true"">&times;</button><h4><i class=""icon fa fa-check""></i> Penghapusan Berhasil!</h4></div>"
                clear()
            End If
        Catch ex As Exception
            Response.Write("Delete Exception :<br>" & ex.ToString)
        End Try

    End Sub
    Private Sub Update(ByVal ID As String)
        Try
            sqlstring = " UPDATE " & _
                  "		masterItem " & _
                  "	    SET " & _
                  "		customer = '" & TxtCustomer.Text.Replace("'", "`") & "', " & _
                  "	    name   = '" & TxtName.Text.Replace("'", "`") & "', " & _
                  "		weight = '" & UbahKomaJdTitik(CekNilai(TxtWeight.Text.Replace("'", "''")).ToString) & "', " & _
                  "		length = '" & UbahKomaJdTitik(CekNilai(TxtLength.Text.Replace("'", "''")).ToString) & "', " & _
                  "		width = '" & UbahKomaJdTitik(CekNilai(TxtWidth.Text.Replace("'", "''")).ToString) & "', " & _
                  "		height = '" & UbahKomaJdTitik(CekNilai(TxtHeight.Text.Replace("'", "''")).ToString) & "', " & _
                  "		lastModified = '" & Now.ToString("yyyy-MM-dd HH:mm:ss") & "', " & _
                  "		note = '" & TxtNote.Text.Replace("'", "`") & "', " & _
                  "		quantity = '" & TxtQuantity.Text.Replace("'", "`") & "' " & _
                  "	WHERE ID = '" & ID.ToString & "' and status <> 0 ; "

            If SQLExecuteNonQuery(sqlstring) > 0 Then
                load_grid()
                btSimpan.Text = "Simpan"
                ltrInfo.Text = "<div class=""alert alert-success alert-dismissible"" id=""info-alert""><button type=""button"" class=""close"" data-dismiss=""alert"" aria-hidden=""true"">&times;</button><h4><i class=""icon fa fa-check""></i> Pembaharuan Berhasil!</h4></div>"
                clear()
            End If

        Catch ex As Exception
            Response.Write("Update Exception :<br>" & ex.ToString)
        End Try

    End Sub
    Private Sub clear()
        Try
            TxtCustomer.Text = ""
            TxtName.Text = ""
            TxtLength.Text = ""
            TxtQuantity.Text = ""
            TxtWeight.Text = ""
            TxtHeight.Text = ""
            TxtWidth.Text = ""
            HFNamaCustomer.Value = ""
            HFkodecust.Value = ""
            Load_Item_Code()
            hfID.Value = ""

        Catch ex As Exception
            Response.Write("Error Function Clear :<BR>" & ex.ToString)
        End Try
    End Sub
    Private Sub clear_label()
        lInfo.Visible = False
        linfoberhasil.Visible = False
    End Sub
#End Region

End Class
