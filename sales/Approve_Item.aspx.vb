Imports System.Data
Imports System.Data.SqlClient
Partial Class Inventory_Approve_Item
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
            If Not Page.IsPostBack Then
                PanelPending.Visible = False
                hfMode.Value = "Insert"
                Call load_grid()
                Call Load_Item_Code()
            End If

            If Not Session("GridItem_Approve") Is Nothing Then
                GridItem_Approve.DataSource = CType(Session("GridItem_Approve"), DataTable)
                GridItem_Approve.DataBind()
            End If
            If Not Session("GridItem_Pending") Is Nothing Then
                GridItem_Pending.DataSource = CType(Session("GridItem_Pending"), DataTable)
                GridItem_Pending.DataBind()
            End If
        Catch ex As Exception
            Response.Write("Error Page Load :<BR>" & ex.ToString)
        End Try
    End Sub
    Private Sub Load_Item_Code()
        Dim month, year, tanggal As String
        Dim value As String
        Dim no As Integer

        Try
            month = Date.Today.ToString("MM")
            year = Date.Today.ToString("yy")
            tanggal = Date.Today.ToString("dd")

            sqlstring = "SELECT TOP 1 Kode_Barang FROM MasterItem " & _
                        "WHERE Kode_Barang LIKE 'IT/" & year.ToString & month.ToString & tanggal.ToString & "%' " & _
                        "ORDER BY ID DESC"
            result = SQLExecuteScalar(sqlstring)

            If result.ToString <> "" Then
                no = CDbl(Right(result, 4)) + 1
            Else
                no = 1
            End If
            value = "IT/" & year.ToString & month.ToString & tanggal.ToString & no.ToString("0000")

            lblKodeItem.Visible = True
            lblKodeItem.Text = value.ToString

        Catch ex As Exception
            Response.Write("Error Load_Quotation_No :<BR>" & ex.ToString)
        End Try
    End Sub


    Protected Sub DDLStatusItem_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DDLStatusItem.SelectedIndexChanged
        If DDLStatusItem.SelectedValue = "0" Then
            PanelApprove.Visible = True
            PanelPending.Visible = False
            Call load_grid()
        Else
            PanelApprove.Visible = False
            PanelPending.Visible = True
            Call load_Pending()
        End If
    End Sub

#End Region
#Region "GRID"
    Private Sub load_grid()
        Try
            sqlstring = " SELECT I.ID, I.Customer_ID, C.Nama_Customer, C.Kode_Customer, I.Kode_Barang, I.Nama_Barang, I.Berat, " & _
                        " I.Panjang ,I.Lebar , I.Tinggi,I.Unit FROM MasterItem I JOIN " & _
                        " MasterCustomer C On I.Customer_ID=C.ID WHERE C.status = 1 AND " & _
                        " I.status = 1 AND I.Status_Barang = 'Approve' Order by I.timestamp desc "
            DS = SQLExecuteQuery(sqlstring)
            DT = DS.Tables(0)

            iDT.Columns.Add(New DataColumn("ID", GetType(String)))
            iDT.Columns.Add(New DataColumn("CustomerID", GetType(String)))
            iDT.Columns.Add(New DataColumn("KodeKostumer", GetType(String)))
            iDT.Columns.Add(New DataColumn("NamaKostumer", GetType(String)))
            iDT.Columns.Add(New DataColumn("KodeBarang", GetType(String)))
            iDT.Columns.Add(New DataColumn("NamaBarang", GetType(String)))
            iDT.Columns.Add(New DataColumn("Berat", GetType(String)))
            iDT.Columns.Add(New DataColumn("Panjang", GetType(String)))
            iDT.Columns.Add(New DataColumn("Lebar", GetType(String)))
            iDT.Columns.Add(New DataColumn("Tinggi", GetType(String)))
            iDT.Columns.Add(New DataColumn("Unit", GetType(String)))

            If DT.Rows.Count > 0 Then
                For i As Integer = 0 To DT.Rows.Count - 1
                    With DT.Rows(i)
                        DR = iDT.NewRow
                        DR("ID") = .Item("ID").ToString
                        DR("CustomerID") = .Item("Customer_Id").ToString
                        DR("KodeKostumer") = .Item("Kode_Customer").ToString
                        DR("NamaKostumer") = .Item("Nama_Customer").ToString
                        DR("KodeBarang") = .Item("Kode_Barang").ToString
                        DR("NamaBarang") = .Item("Nama_Barang").ToString
                        DR("Berat") = .Item("Berat").ToString
                        DR("Panjang") = .Item("Panjang").ToString
                        DR("Lebar") = .Item("Lebar").ToString
                        DR("Tinggi") = .Item("Tinggi").ToString
                        DR("Unit") = .Item("Unit").ToString
                        iDT.Rows.Add(DR)
                    End With
                Next
                Session("GridItem_Approve") = iDT
                GridItem_Approve.DataSource = iDT
                GridItem_Approve.DataBind()
            Else
                GridItem_Approve.DataSource = Nothing
                GridItem_Approve.DataBind()
            End If

        Catch ex As Exception
            Response.Write("Error Load Grid Item:<BR>" & ex.ToString)
        End Try

    End Sub
    Private Sub load_Pending()
        Try
            sqlstring = " SELECT I.ID, I.Customer_ID, C.Nama_Customer, C.Kode_Customer, I.Kode_Barang, I.Nama_Barang, I.Berat, " & _
                        " I.Panjang ,I.Lebar , I.Tinggi,I.Unit FROM MasterItem I JOIN " & _
                        " MasterCustomer C On I.Customer_ID=C.ID WHERE C.status = 1 AND " & _
                        " I.status = 1 AND I.Status_Barang = 'Pending' Order by I.timestamp desc "
            DS = SQLExecuteQuery(sqlstring)
            DT = DS.Tables(0)

            iDT.Columns.Add(New DataColumn("ID", GetType(String)))
            iDT.Columns.Add(New DataColumn("CustomerID", GetType(String)))
            iDT.Columns.Add(New DataColumn("KodeKostumer", GetType(String)))
            iDT.Columns.Add(New DataColumn("NamaKostumer", GetType(String)))
            iDT.Columns.Add(New DataColumn("KodeBarang", GetType(String)))
            iDT.Columns.Add(New DataColumn("NamaBarang", GetType(String)))
            iDT.Columns.Add(New DataColumn("Berat", GetType(String)))
            iDT.Columns.Add(New DataColumn("Panjang", GetType(String)))
            iDT.Columns.Add(New DataColumn("Lebar", GetType(String)))
            iDT.Columns.Add(New DataColumn("Tinggi", GetType(String)))
            iDT.Columns.Add(New DataColumn("Unit", GetType(String)))

            If DT.Rows.Count > 0 Then
                For i As Integer = 0 To DT.Rows.Count - 1
                    With DT.Rows(i)
                        DR = iDT.NewRow
                        DR("ID") = .Item("ID").ToString
                        DR("CustomerID") = .Item("Customer_Id").ToString
                        DR("KodeKostumer") = .Item("Kode_Customer").ToString
                        DR("NamaKostumer") = .Item("Nama_Customer").ToString
                        DR("KodeBarang") = .Item("Kode_Barang").ToString
                        DR("NamaBarang") = .Item("Nama_Barang").ToString
                        DR("Berat") = .Item("Berat").ToString
                        DR("Panjang") = .Item("Panjang").ToString
                        DR("Lebar") = .Item("Lebar").ToString
                        DR("Tinggi") = .Item("Tinggi").ToString
                        DR("Unit") = .Item("Unit").ToString
                        iDT.Rows.Add(DR)
                    End With
                Next
                Session("GridView_Item") = iDT
                GridItem_Pending.DataSource = iDT
                GridItem_Pending.DataBind()
            Else
                GridItem_Pending.DataSource = Nothing
                GridItem_Pending.DataBind()
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
    Protected Sub GridItem_Pending_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles GridItem_Pending.PreRender
        Try

            If Not Page.IsPostBack Then
                GridItem_Pending.FocusedRowIndex = -1
            End If

        Catch ex As Exception
            Response.Write("grid_Master_User_PreRender Exception :<br>" & ex.ToString)
        End Try
    End Sub

    Protected Sub GridItem_Approve_RowCommand(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewRowCommandEventArgs) Handles GridItem_Approve.RowCommand
        Try
            Select Case e.CommandArgs.CommandName
                Case "Edit"
                    hfCID.Value = GridItem_Approve.GetRowValues(e.VisibleIndex, "CustomerID").ToString
                    TxtKodeCost.Text = GridItem_Approve.GetRowValues(e.VisibleIndex, "KodeKostumer").ToString
                    TxtNameCost.Text = GridItem_Approve.GetRowValues(e.VisibleIndex, "NamaKostumer").ToString
                    lblKodeItem.Text = GridItem_Approve.GetRowValues(e.VisibleIndex, "KodeBarang").ToString
                    TxtBarang.Text = GridItem_Approve.GetRowValues(e.VisibleIndex, "NamaBarang").ToString
                    TxtBerat.Text = GridItem_Approve.GetRowValues(e.VisibleIndex, "Berat").ToString
                    TxtPanjang.Text = GridItem_Approve.GetRowValues(e.VisibleIndex, "Panjang").ToString
                    TxtLebar.Text = GridItem_Approve.GetRowValues(e.VisibleIndex, "Lebar").ToString
                    TxtTinggi.Text = GridItem_Approve.GetRowValues(e.VisibleIndex, "Tinggi").ToString
                    TxtUnit.Text = GridItem_Approve.GetRowValues(e.VisibleIndex, "Unit").ToString
                    hfMode.Value = "Update"
                    hfID.Value = GridItem_Approve.GetRowValues(e.VisibleIndex, "ID").ToString
                Case "Delete"
                    Delete(GridItem_Approve.GetRowValues(e.VisibleIndex, "ID").ToString)
            End Select

        Catch ex As Exception
            Response.Write(ex.ToString)
        End Try
    End Sub
    Protected Sub GridItem_Pending_RowCommand(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewRowCommandEventArgs) Handles GridItem_Pending.RowCommand
        Try
            Select Case e.CommandArgs.CommandName
                Case "Edit"
                    hfCID.Value = GridItem_Pending.GetRowValues(e.VisibleIndex, "CustomerID").ToString
                    TxtKodeCost.Text = GridItem_Pending.GetRowValues(e.VisibleIndex, "KodeKostumer").ToString
                    TxtNameCost.Text = GridItem_Pending.GetRowValues(e.VisibleIndex, "NamaKostumer").ToString
                    lblKodeItem.Text = GridItem_Pending.GetRowValues(e.VisibleIndex, "KodeBarang").ToString
                    TxtBarang.Text = GridItem_Pending.GetRowValues(e.VisibleIndex, "NamaBarang").ToString
                    TxtBerat.Text = GridItem_Pending.GetRowValues(e.VisibleIndex, "Berat").ToString
                    TxtPanjang.Text = GridItem_Pending.GetRowValues(e.VisibleIndex, "Panjang").ToString
                    TxtLebar.Text = GridItem_Pending.GetRowValues(e.VisibleIndex, "Lebar").ToString
                    TxtTinggi.Text = GridItem_Pending.GetRowValues(e.VisibleIndex, "Tinggi").ToString
                    TxtUnit.Text = GridItem_Pending.GetRowValues(e.VisibleIndex, "Unit").ToString
                    hfMode.Value = "Update"
                    hfID.Value = GridItem_Pending.GetRowValues(e.VisibleIndex, "ID").ToString
                Case "Delete"
                    Delete(GridItem_Pending.GetRowValues(e.VisibleIndex, "ID").ToString)
                Case "Approve"
                    Approve(GridItem_Pending.GetRowValues(e.VisibleIndex, "ID").ToString)
            End Select

        Catch ex As Exception
            Response.Write(ex.ToString)
        End Try
    End Sub
#End Region

#Region "VALIDATION"
    Private Function validation() As Boolean
        If TxtNameCost.Text.Trim = "" Then
            lInfo.Visible = True
            lInfo.Text = "Id Kostumer belum diisi"
            TxtNameCost.Focus()
            Return False
        End If

        If TxtBarang.Text.Trim = "" Then
            lInfo.Visible = True
            lInfo.Text = "Nama barang belum diisi"
            TxtBarang.Focus()
            Return False
        End If

        If TxtLebar.Text.Trim = "" Then
            lInfo.Visible = True
            lInfo.Text = "Lebar belum diisi"
            TxtLebar.Focus()
            Return False
        End If

        If TxtBerat.Text.Trim = "" Then
            lInfo.Visible = True
            lInfo.Text = "Berat belum diisi"
            TxtBerat.Focus()
            Return False
        End If

        If TxtTinggi.Text.Trim = "" Then
            lInfo.Visible = True
            lInfo.Text = "Tinggi belum diisi"
            TxtTinggi.Focus()
            Return False
        End If
        Return True
    End Function
    Private Function validationInput() As Boolean
        Try
            sqlstring = "SELECT Nama_barang " & _
                        "FROM MasterItem " & _
                        "WHERE " & _
                        "Berat = '" & TxtBerat.Text.ToString & "' AND " & _
                        "Panjang = '" & TxtPanjang.Text.ToString & "' AND " & _
                        "Lebar = '" & TxtLebar.Text.ToString & "' AND " & _
                        "Unit = '" & TxtUnit.Text.ToString & "' AND " & _
                        "Tinggi = '" & TxtTinggi.Text.ToString & "' AND " & _
                        "Customer_Id = '" & hfCID.Value & "' AND " & _
                        "Nama_barang = '" & TxtBarang.Text.ToString & "' AND " & _
                        "[Status] = 1"
            Response.Write(sqlstring)
            result = SQLExecuteScalar(sqlstring)
            Response.Write(sqlstring)
            If result.ToString <> "" Then
                lInfo.Visible = True
                lInfo.Text = "Barang" & "&nbsp<B>" & TxtBarang.Text & "</B>&nbsp" & "telah terdaftar"
                TxtNameCost.Focus()
                Return False
            End If

            Return True
        Catch ex As Exception
            Response.Write("Error Validasi Insert :<BR>" & ex.ToString)
        End Try
    End Function
#End Region
#Region "BUTTON"
    Protected Sub btSimpan_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btSimpan.Click
        Try
            If validation() Then
                If hfMode.Value = "Insert" Then
                    If validationInput() Then
                        Call Insert()
                        load_Pending()
                        linfoberhasil.Visible = True
                        linfoberhasil.Text = "Data berhasil disimpan"
                    End If
                Else
                    If validationInput() Then
                        Call Update(hfID.Value)
                        load_Pending()
                        linfoberhasil.Visible = True
                        linfoberhasil.Text = "Data Berhasil diupdate"
                        hfMode.Value = "Insert"
                    End If
                End If
            End If
        Catch ex As Exception
            Response.Write("Error BtSimpan :<BR>" & ex.ToString)
        End Try
    End Sub

    Protected Sub btBatal_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btBatal.Click
        Try
            Call clear()
        Catch ex As Exception
            Response.Write("Error btBatal :<BR>" & ex.ToString)
        End Try
    End Sub
#End Region

#Region "METHOD"
    Private Sub Insert()
        Try
            sqlstring = " INSERT INTO MasterItem " & _
                        " (Customer_Id, Nama_Barang, Kode_Barang, Berat, " & _
                        " Panjang,Tinggi, Lebar,Unit,Status_Barang,[status], UserName ) VALUES " & _
                        " ('" & hfCID.Value & "', '" & TxtBarang.Text.Replace("'", "`") & "' ," & _
                        " '" & lblKodeItem.Text.ToString & "', '" & TxtBerat.Text.Replace("'", "`") & "', " & _
                        " '" & TxtPanjang.Text.Replace("'", "`") & "', '" & TxtTinggi.Text.Replace("'", "`") & "', " & _
                        " '" & TxtLebar.Text.Replace("'", "`") & "', '" & TxtUnit.Text.Replace("'", "`") & "', " & _
                        " 'Pending',1, '" & Session("UserId").ToString & "')"
            hasil = SQLExecuteNonQuery(sqlstring)

            If hasil > 0 Then
                Call clear()
            End If

        Catch ex As Exception
            Response.Write("Error Function Insert() :<BR>" & ex.ToString)
        End Try
    End Sub
    Private Sub Delete(ByVal ID As String)

        linfoberhasil.Visible = True
        linfoberhasil.Text = ID
        '#
        Try
            sqlstring = " UPDATE " & _
                  "		MasterItem " & _
                  "	    SET " & _
                  "		LastModified = '" & Now.ToString & "', " & _
                  "		[status] = 0 " & _
                  "	    WHERE ID = '" & ID.ToString & "'; "
            Dim result As Integer = SQLExecuteNonQuery(sqlstring)
            If result > 0 Then
                Call load_grid()
                GridItem_Pending.FocusedRowIndex = 0
                GridItem_Approve.FocusedRowIndex = 0
                Call clear()
            End If
        Catch ex As Exception
            Response.Write("Delete Exception :<br>" & ex.ToString)
        End Try

    End Sub
    Private Sub Approve(ByVal ID As String)

        linfoberhasil.Visible = True
        linfoberhasil.Text = ID
        '#
        Try
            sqlstring = " UPDATE " & _
                  "		MasterItem " & _
                  "	    SET " & _
                  "		LastModified = '" & Now.ToString & "', " & _
                  "		Status_Barang = 'Approve' " & _
                  "	    WHERE ID = '" & ID.ToString & "'; "
            Dim result As Integer = SQLExecuteNonQuery(sqlstring)
            If result > 0 Then
                Call load_Pending()
                GridItem_Pending.FocusedRowIndex = 0
                GridItem_Approve.FocusedRowIndex = 0
                Call clear()
            End If
        Catch ex As Exception
            Response.Write("Delete Exception :<br>" & ex.ToString)
        End Try

    End Sub
    Private Sub Update(ByVal ID As String)

        '#
        Dim result As Integer

        Try

            sqlstring = " UPDATE " & _
                  "		MasterItem " & _
                  "	    SET " & _
                  "		Customer_Id = '" & hfCID.Value & "', " & _
                  "	    Kode_Barang   = '" & lblKodeItem.Text.ToString & "', " & _
                  "	    Nama_Barang   = '" & TxtBarang.Text.Replace("'", "`") & "', " & _
                  "		Berat = '" & TxtBerat.Text.Replace("'", "`") & "', " & _
                  "		Panjang = '" & TxtPanjang.Text.Replace("'", "`") & "', " & _
                  "		Lebar = '" & TxtLebar.Text.Replace("'", "`") & "', " & _
                  "		Tinggi = '" & TxtTinggi.Text.Replace("'", "`") & "', " & _
                  "		Unit = '" & TxtUnit.Text.Replace("'", "`") & "' " & _
                  "	WHERE ID = '" & ID.ToString & "'; "
            result = SQLExecuteNonQuery(sqlstring)
            If result > 0 Then
                Call clear()
            End If

        Catch ex As Exception
            Response.Write("Update Exception :<br>" & ex.ToString)
        End Try

    End Sub
    Private Sub clear()
        Try
            TxtKodeCost.Text = ""
            TxtNameCost.Text = ""
            TxtBarang.Text = ""
            TxtPanjang.Text = ""
            TxtUnit.Text = ""
            TxtBerat.Text = ""
            TxtTinggi.Text = ""
            TxtLebar.Text = ""
            lInfo.Visible = False
            linfoberhasil.Visible = False
            Load_Item_Code()

        Catch ex As Exception
            Response.Write("Error Function Clear :<BR>" & ex.ToString)
        End Try
    End Sub
#End Region

End Class
