Imports System.Data
Imports System.Data.SqlClient
Imports DevExpress.Web.ASPxGridView
Partial Public Class QuotationPotential
    Inherits System.Web.UI.Page
    Private DS As DataSet
    Private DT As DataTable
    Private DR As DataRow
    Private sqlstring As String
    Private iDT As New DataTable
    Private aDT As New DataTable
    Private cDT As New DataTable
    Dim hasil As Integer
    Dim STR As String
    Dim result As String

#Region "PAGE"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Session("UserID") = Nothing Then
                FormsAuthentication.SignOut()
                Response.Redirect("Index.aspx")
            End If

            If Not Page.IsPostBack Then
                Session("Grid_SOP") = Nothing
                Session("Grid_SO_ParentP") = Nothing
                PanelInput.Visible = False
                PanelGrid.Visible = True
                hfCID.Value = ""
                tb_tgl.Date = Today
                load_ddl_satuan()
                load_grid_parent()
                LoadDDLKota()
                Call create_session()
                lblQoNo.Visible = True
                lblQoNo.Text = Load_Quotation_Number()
                hfMode.Value = "Insert"
                hfModeEdit.Value = "Insert"
                TxtHargaItem.Attributes.Add("onkeyup", "changenumberformat('" & TxtHargaItem.ClientID & "')")
            End If


            If Not Session("Grid_SOP") Is Nothing Then
                Grid_SO.DataSource = CType(Session("Grid_SOP"), DataTable)
                Grid_SO.DataBind()
            End If

            If Not Session("Grid_SO_ParentP") Is Nothing Then
                Grid_SO_Parent.DataSource = CType(Session("Grid_SO_ParentP"), DataTable)
                Grid_SO_Parent.DataBind()
            End If

        Catch ex As Exception
            Response.Write(ex.ToString)
        End Try
        
    End Sub

    Private Function Load_Quotation_Number_Real() As String
        Dim month, year, tanggal As String
        Dim value As String = ""
        Dim no As Integer
        Dim pisah As String()

        Dim zDT As DataTable

        Try
            month = Date.Today.ToString("MM")
            year = Date.Today.ToString("yy")
            tanggal = Date.Today.ToString("dd")

            sqlstring = "SELECT TOP 1 Quotation_No FROM MasterQuotation where status <> 0 and Tanggal BETWEEN '1/1/' + '" & Now.ToString("yyyy") & "' AND '12/31/' + '" & Now.ToString("yyyy") & "' " & _
                        "ORDER BY ID DESC"
            zDT = SQLExecuteQuery(sqlstring).Tables(0)

            If zDT.Rows.Count > 0 Then
                pisah = zDT.Rows(0).Item("Quotation_No").ToString.Split("/")
                no = CDbl(pisah(0)) + 1
            Else
                no = 1
            End If
            value = no & "/" & Singkatan & "/" & "p.tal" & "/" & CekBulan(month) & "/" & year

            Return value.ToString
        Catch ex As Exception
            Throw New Exception("Error Load_Quotation_No :<BR>" & ex.ToString)
        End Try

    End Function

    Private Function Load_Quotation_Number() As String
        Dim month, year, tanggal As String
        Dim value As String
        Dim no As Integer
        Dim pisah As String()
        Dim zDT As DataTable

        Try
            month = Date.Today.ToString("MM")
            year = Date.Today.ToString("yy")
            tanggal = Date.Today.ToString("dd")

            sqlstring = "SELECT TOP 1 Quotation_No FROM PotentialQuotation where status <> 0 and Tanggal BETWEEN '1/1/' + '" & Now.ToString("yyyy") & "' AND '12/31/' + '" & Now.ToString("yyyy") & "' " & _
                        "ORDER BY ID DESC"
            zDT = SQLExecuteQuery(sqlstring).Tables(0)

            If zDT.Rows.Count > 0 Then
                pisah = zDT.Rows(0).Item("Quotation_No").ToString.Split("/")
                no = CDbl(pisah(0)) + 1
            Else
                no = 1
            End If
            value = no & "/" & Singkatan & "/" & "p.tal" & "/" & CekBulan(month) & "/" & year

            Return value.ToString
        Catch ex As Exception
            Throw New Exception("Error Load_Quotation_No :<BR>" & ex.ToString)
        End Try

    End Function

    Private Function Load_Kode_Customer() As String
        Dim month, year, tanggal As String
        Dim value As String = ""
        Dim no As Integer
        Try
            month = Date.Today.ToString("MM")
            year = Date.Today.ToString("yy")
            tanggal = Date.Today.ToString("dd")

            sqlstring = "SELECT TOP 1 Kode_Customer FROM MasterCustomer " & _
                        "WHERE Kode_Customer LIKE 'CS/" & year.ToString & month.ToString & tanggal.ToString & "%'  and status <>0" & _
                        "ORDER BY ID DESC"
            result = SQLExecuteScalar(sqlstring)

            If result.ToString <> "" Then
                no = CDbl(Right(result, 4)) + 1
            Else
                no = 1
            End If
            value = "CS/" & year.ToString & month.ToString & tanggal.ToString & no.ToString("0000")

        Catch ex As Exception
            Response.Write("Error Load_Kode_Customer :<BR>" & ex.ToString)
        End Try
        Return value
    End Function

#End Region

#Region "GRID"
    Private Sub create_session()
        Try
            aDT.Columns.Add(New DataColumn("IDS", GetType(String)))
            aDT.Columns.Add(New DataColumn("Quotation_NoS", GetType(String)))
            aDT.Columns.Add(New DataColumn("NamaBarangS", GetType(String)))
            aDT.Columns.Add(New DataColumn("SatuanIDS", GetType(String)))
            aDT.Columns.Add(New DataColumn("NamaHargaS", GetType(String)))
            aDT.Columns.Add(New DataColumn("HargaS", GetType(Double)))

            Session("Grid_SOP") = aDT
            Grid_SO.DataSource = aDT
            Grid_SO.KeyFieldName = "IDS"
            Grid_SO.DataBind()

        Catch ex As Exception
            Response.Write("Error Load Create Session :<BR>" & ex.ToString)
        End Try

    End Sub

    Private Sub load_grid(ByVal qono As String)
        Try
            Dim ID As String
            sqlstring = "SELECT qd.IDDetail , qd.Quotation_No, qd.Nama_Barang, qd.SatuanID,mh.NamaHarga ," & _
                        " qd.Harga FROM PotentialQuotationDetail qd " & _
                        "left Join MasterHargaDefault mh on qd.SatuanID = mh.ID " & _
                        "Where qd.Quotation_No = '" & qono.ToString & "' AND qd.status = 1 Order by qd.timestamp desc"

            aDT.Columns.Add(New DataColumn("IDS", GetType(String)))
            aDT.Columns.Add(New DataColumn("Quotation_NoS", GetType(String)))
            aDT.Columns.Add(New DataColumn("NamaBarangS", GetType(String)))
            aDT.Columns.Add(New DataColumn("SatuanIDS", GetType(String)))
            aDT.Columns.Add(New DataColumn("NamaHargaS", GetType(String)))
            aDT.Columns.Add(New DataColumn("HargaS", GetType(Double)))

            DS = SQLExecuteQuery(sqlstring)
            DT = DS.Tables(0)
            If DT.Rows.Count > 0 Then
                For i As Integer = 0 To DT.Rows.Count - 1
                    With DT.Rows(i)
                        DR = aDT.NewRow
                        ID = .Item("IDDetail").ToString
                        DR("IDS") = .Item("IDDetail").ToString
                        DR("Quotation_NoS") = .Item("Quotation_No").ToString
                        DR("NamaBarangS") = .Item("Nama_Barang").ToString
                        DR("SatuanIDS") = .Item("SatuanID").ToString
                        DR("NamaHargaS") = .Item("NamaHarga").ToString
                        DR("HargaS") = .Item("Harga").ToString
                        aDT.Rows.Add(DR)
                    End With
                Next
                Session("Grid_SOP") = aDT
                Grid_SO.DataSource = aDT
                Grid_SO.KeyFieldName = "IDS"
                Grid_SO.DataBind()
            Else
                Grid_SO.DataSource = Nothing
                Grid_SO.DataBind()
            End If
        Catch ex As Exception
            Response.Write("Error Load Create Session :<BR>" & ex.ToString)
        End Try

    End Sub

    Private Sub load_grid_parent()
        Try
            sqlstring = " SELECT ID,Quotation_No,Tanggal, Nama_Customer, Alamat, Kota, Kode_area_Telp, No_Telp, " & _
                        " Penerima,UP,JabatanUP, Tujuan, KotaAsalBrg" & _
                        " FROM PotentialQuotation WHERE status = 1 Order by timestamp desc"
            DS = SQLExecuteQuery(sqlstring)
            DT = DS.Tables(0)

            iDT.Columns.Add(New DataColumn("ID", GetType(String)))
            iDT.Columns.Add(New DataColumn("Quotation_No", GetType(String)))
            iDT.Columns.Add(New DataColumn("Tanggal", GetType(DateTime)))
            iDT.Columns.Add(New DataColumn("Nama_Customer", GetType(String)))
            iDT.Columns.Add(New DataColumn("Alamat", GetType(String)))
            iDT.Columns.Add(New DataColumn("Kode_Area_Telp", GetType(String)))
            iDT.Columns.Add(New DataColumn("Kota", GetType(String)))
            iDT.Columns.Add(New DataColumn("No_Telp", GetType(String)))
            iDT.Columns.Add(New DataColumn("Tujuan", GetType(String)))
            iDT.Columns.Add(New DataColumn("Penerima", GetType(String)))
            iDT.Columns.Add(New DataColumn("UP", GetType(String)))
            iDT.Columns.Add(New DataColumn("JabatanUP", GetType(String)))
            iDT.Columns.Add(New DataColumn("KotaAsalBrg", GetType(String)))

            If DT.Rows.Count > 0 Then
                For i As Integer = 0 To DT.Rows.Count - 1
                    With DT.Rows(i)
                        DR = iDT.NewRow()
                        DR("ID") = .Item("ID").ToString
                        DR("Quotation_No") = .Item("Quotation_No").ToString
                        DR("Tanggal") = CDate(.Item("Tanggal").ToString).ToString("MM/dd/yyyy")
                        DR("Nama_Customer") = .Item("Nama_Customer").ToString
                        DR("Alamat") = .Item("Alamat").ToString
                        DR("Kota") = .Item("Kota").ToString
                        DR("No_Telp") = DisplayPhone(.Item("Kode_Area_Telp").ToString, .Item("No_Telp").ToString)
                        DR("Tujuan") = .Item("Tujuan").ToString
                        DR("Penerima") = .Item("Penerima").ToString
                        DR("UP") = .Item("UP").ToString
                        DR("JabatanUP") = .Item("JabatanUP").ToString
                        DR("KotaAsalBrg") = .Item("KotaAsalBrg").ToString
                        iDT.Rows.Add(DR)
                    End With
                Next
                Session("Grid_SO_ParentP") = iDT
                Grid_SO_Parent.DataSource = iDT
                Grid_SO_Parent.KeyFieldName = "Quotation_No"
                Grid_SO_Parent.DataBind()
            Else
                Grid_SO_Parent.DataSource = Nothing
                Grid_SO_Parent.DataBind()
            End If

        Catch ex As Exception
            Response.Write("Error Load Grid Parent :<BR>" & ex.ToString)
        End Try
    End Sub

    Private Sub load_grid_child(ByVal grid As ASPxGridView)
        Try

            sqlstring = "SELECT qd.IDDetail as ID , qd.Quotation_No, qd.Nama_Barang, qd.SatuanID,mh.NamaHarga ," & _
                        "qd.Harga FROM PotentialQuotationDetail qd " & _
                        "left Join MasterHargaDefault mh on qd.SatuanID = mh.ID  " & _
                        "Where qd.Quotation_No = '" & grid.GetMasterRowKeyValue() & "' AND qd.status = 1 Order by qd.timestamp desc"

            DS = SQLExecuteQuery(sqlstring)
            DT = DS.Tables(0)
            grid.DataSource = DT
        Catch ex As Exception
            Response.Write("Error Load Grid Child :<BR>" & ex.ToString)
        End Try
    End Sub

    Protected Sub Grid_SO_Child_DataSelect(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Call load_grid_child(TryCast(sender, ASPxGridView))
        Catch ex As Exception
            Response.Write("Error Load Grid Child DataSelect  :<BR>" & ex.ToString)
        End Try
    End Sub

    Protected Sub Grid_SO_Parent_RowCommand(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewRowCommandEventArgs) Handles Grid_SO_Parent.RowCommand
        Try
            Dim pisah() As String

            Select Case e.CommandArgs.CommandName
                Case "Edit"
                    hfMode.Value = "Update"
                    PanelView.Visible = False
                    PanelInput.Visible = True
                    clear_label()
                    lblQoNo.Text = Load_Quotation_Number()
                    HfSo.Value = Grid_SO_Parent.GetRowValues(e.VisibleIndex, "Quotation_No").ToString
                    TxtNamaCustomer.Text = Grid_SO_Parent.GetRowValues(e.VisibleIndex, "Nama_Customer").ToString
                    TxtAlamat.Text = Grid_SO_Parent.GetRowValues(e.VisibleIndex, "Alamat").ToString
                    TxtArea.Text = Grid_SO_Parent.GetRowValues(e.VisibleIndex, "Kota").ToString
                    pisah = Grid_SO_Parent.GetRowValues(e.VisibleIndex, "No_Telp").ToString.Split(")")
                    txtkodearea.Text = pisah(0).Replace("(", "").ToString
                    TxtNoHP.Text = pisah(1).Replace("-", "").ToString
                    lblQoNo.Text = Grid_SO_Parent.GetRowValues(e.VisibleIndex, "Quotation_No").ToString
                    TxtPenerima.Text = Grid_SO_Parent.GetRowValues(e.VisibleIndex, "Penerima").ToString
                    TxtUp.Text = Grid_SO_Parent.GetRowValues(e.VisibleIndex, "UP").ToString
                    DDLKotaTujuan.SelectedValue = Grid_SO_Parent.GetRowValues(e.VisibleIndex, "Tujuan").ToString
                    DDLKotaASAlBarang.SelectedValue = Grid_SO_Parent.GetRowValues(e.VisibleIndex, "KotaAsalBrg").ToString
                    TxtJabatan.Text = Grid_SO_Parent.GetRowValues(e.VisibleIndex, "JabatanUP").ToString
                    load_grid(Grid_SO_Parent.GetRowValues(e.VisibleIndex, "Quotation_No").ToString)
                Case "Delete"
                    Delete(Grid_SO_Parent.GetRowValues(e.VisibleIndex, "Quotation_No").ToString)
                Case "Deal"
                    Insert_Quotation(Grid_SO_Parent.GetRowValues(e.VisibleIndex, "Quotation_No").ToString)
            End Select
        Catch ex As Exception
            Response.Write(ex.ToString)
        End Try
    End Sub

    Protected Sub Grid_SO_RowCommand(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewRowCommandEventArgs) Handles Grid_SO.RowCommand
        Try
            Select Case e.CommandArgs.CommandName
                Case "Delete"
                    If hfMode.Value = "Insert" Then
                        Remove_item(Grid_SO.GetRowValues(e.VisibleIndex, "NamaBarangS").ToString)
                    ElseIf hfMode.Value = "Update" Then
                        If Grid_SO.GetRowValues(e.VisibleIndex, "IDS").ToString = "" Then
                            Remove_item(Grid_SO.GetRowValues(e.VisibleIndex, "NamaBarangS").ToString)
                        Else
                            Remove_itemDB(Grid_SO.GetRowValues(e.VisibleIndex, "IDS").ToString, Grid_SO.GetRowValues(e.VisibleIndex, "Quotation_NoS".ToString))
                        End If
                    End If
                Case "Edit"

                    If hfMode.Value = "Insert" Then
                        hfModeEdit.Value = "Edit"
                        TxtItem.Text = Grid_SO.GetRowValues(e.VisibleIndex, "NamaBarangS").ToString
                        If Grid_SO.GetRowValues(e.VisibleIndex, "SatuanIDS").ToString = "" Then
                            DDLSatuan.SelectedValue = 0
                        Else
                            DDLSatuan.SelectedValue = Grid_SO.GetRowValues(e.VisibleIndex, "SatuanIDS").ToString
                        End If
                        'hfOID.Value = Grid_SO.GetRowValues(e.VisibleIndex, "OtherIDS").ToString
                        TxtHargaItem.Text = UbahKoma(Grid_SO.GetRowValues(e.VisibleIndex, "HargaS").ToString)
                    ElseIf hfMode.Value = "Update" Then
                        hfModeEdit.Value = "Edit"
                        TxtItem.Text = Grid_SO.GetRowValues(e.VisibleIndex, "NamaBarangS").ToString
                        If Grid_SO.GetRowValues(e.VisibleIndex, "SatuanIDS").ToString = "" Then
                            DDLSatuan.SelectedValue = 0
                        Else
                            DDLSatuan.SelectedValue = Grid_SO.GetRowValues(e.VisibleIndex, "SatuanIDS").ToString
                        End If
                        TxtHargaItem.Text = UbahKoma(Grid_SO.GetRowValues(e.VisibleIndex, "HargaS").ToString)
                        hfID.Value = Grid_SO.GetRowValues(e.VisibleIndex, "IDS").ToString
                    Else
                        hfModeEdit.Value = "Edit"
                        TxtItem.Text = Grid_SO.GetRowValues(e.VisibleIndex, "NamaBarangS").ToString
                        If Grid_SO.GetRowValues(e.VisibleIndex, "SatuanIDS").ToString = "" Then
                            DDLSatuan.SelectedValue = 0
                        Else
                            DDLSatuan.SelectedValue = Grid_SO.GetRowValues(e.VisibleIndex, "SatuanIDS").ToString
                        End If
                        TxtHargaItem.Text = UbahKoma(Grid_SO.GetRowValues(e.VisibleIndex, "HargaS").ToString)
                        hfID.Value = Grid_SO.GetRowValues(e.VisibleIndex, "IDS").ToString
                    End If
            End Select
        Catch ex As Exception
            Response.Write(ex.ToString)
        End Try

    End Sub

    Protected Sub Grid_SO_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Grid_SO.PreRender
        If Not Page.IsPostBack Then
            Grid_SO.FocusedRowIndex = -1
        End If
    End Sub

    Protected Sub Grid_SO_Parent_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Grid_SO_Parent.PreRender
        If Not Page.IsPostBack Then
            Grid_SO_Parent.FocusedRowIndex = -1
        End If
    End Sub

    Protected Sub Grid_SO_Parent_CustomCallback(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs) Handles Grid_SO_Parent.CustomCallback
        Call load_grid_parent()
    End Sub

#End Region

#Region "VALIDATION"

    Private Function Validation() As Boolean
        Try
            clear_label()
            If TxtItem.Text.ToString = "" Then
                lInfo.Visible = True
                lInfo.Text = "Item Harus DiPilih!"
                TxtItem.Focus()
                Return False
            End If


            If TxtHargaItem.Text.ToString = "" Then
                lInfo.Visible = True
                lInfo.Text = "Anda Harus Mengisi Harga Barang!"
                TxtItem.Focus()
                Return False
            End If
            Return True
        Catch ex As Exception
            Response.Write("Validation Exception : <br>" & ex.ToString)
        End Try
    End Function

    Private Function Validation_Input() As Boolean
        Try
            clear_label()
            If tb_tgl.Text = "" Then
                lInfo.Visible = True
                lInfo.Text = "Tanggal Harus Dipilih"
                tb_tgl.Focus()
                Return False
            End If

            If Grid_SO.VisibleRowCount = 0 Then
                lInfo.Visible = True
                lInfo.Text = "Masukan data"
                Return False
            End If

            If TxtNamaCustomer.Text.Trim = "" Then
                lInfo.Visible = True
                lInfo.Text = "Nama Customer Kosong"
                TxtNamaCustomer.Focus()
                Return False
            End If

            If TxtAlamat.Text.Trim = "" Then
                lInfo.Visible = True
                lInfo.Text = "Alamat masih Kosong"
                TxtAlamat.Focus()
                Return False
            End If


            If TxtArea.Text.Trim = "" Then
                lInfo.Visible = True
                lInfo.Text = "Kota Customer harus diisi"
                TxtArea.Focus()
                Return False
            End If

            If DDLKotaASAlBarang.SelectedIndex = 0 Then
                lInfo.Visible = True
                lInfo.Text = "Kota asal barang harap diisi"
                DDLKotaASAlBarang.Focus()
                Return False
            End If


            If TxtNoHP.Text.Trim = "" Then
                lInfo.Visible = True
                lInfo.Text = "No Telp harap diisi"
                TxtNoHP.Focus()
                Return False
            Else
                If Not IsNumeric(TxtNoHP.Text.ToString) Then
                    lInfo.Visible = True
                    lInfo.Text = " No telepon harus Angka"
                    TxtNoHP.Focus()
                End If
            End If

            Return True
        Catch ex As Exception
            Response.Write("Validation Exception : <br>" & ex.ToString)
        End Try
    End Function
#End Region

#Region "BUTTON"

    Private Sub btn_new_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_new.Click
        PanelInput.Visible = True
        PanelView.Visible = False
        Load_Quotation_Number()
        clear_label()
    End Sub

    Protected Sub btn_add_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_add.Click
        If hfModeEdit.Value = "Insert" Then
            If Validation() Then
                Add_Item()
                hfOID.Value = ""
            End If
        Else
            If Validation() Then
                Edit_Item()
                hfOID.Value = ""
            End If
        End If

    End Sub

    Protected Sub btn_reset_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_reset.Click
        Try
            Add_Clear()
            Clear()
        Catch ex As Exception
            Response.Write(ex.ToString)
        End Try
    End Sub

    Protected Sub btSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btSave.Click
        Try
            If Validation_Input() Then
                If hfMode.Value = "Insert" Then
                    Insert()
                Else
                    Update(lblQoNo.Text.ToString)
                End If

            End If

        Catch ex As Exception
            Response.Write("Error Button Save : <BR> " & ex.ToString)
        End Try
    End Sub

    Protected Sub btback_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btback.Click
        Try
            Add_Clear()
            Clear()
            clear_label()
            lInfo.Visible = False
            PanelInput.Visible = False
            PanelView.Visible = True
        Catch ex As Exception
            Response.Write("error BtnBack : " & ex.ToString)
        End Try
    End Sub

#End Region

#Region "METHOD"
    Private Sub Insert_Quotation(ByVal QONO As String)
        Dim result As Integer
        Dim DataString As String
        Dim TableDTP As DataTable
        Dim TableDTC As DataTable
        Dim DSD As DataSet
        Dim quotationno As String
        Dim namacustomer As String
        Dim iddetail As Integer = 0
        Try
            DataString = " Select * from PotentialQuotation where Quotation_No = '" & QONO.ToString & "' and status = 1;"
            DataString &= " Select * from PotentialQuotationDetail Where Quotation_No = '" & QONO.ToString & "' and status = 1;"

            DSD = SQLExecuteQuery(DataString)
            TableDTP = DSD.Tables(0)
            TableDTC = DSD.Tables(1)

            quotationno = Load_Quotation_Number_Real()
            namacustomer = Load_Kode_Customer()
            STR &= " set nocount on; declare @id as int;insert into MasterCustomer(Kode_Customer,Nama_Customer,Alamat,Area,Kode_Area_Telp1, No_Telp1,No_Telp2,No_HP,[status],UserName, KotaDitunjukan ) values(" & _
                    " '" & namacustomer.ToString & "','" & TableDTP.Rows(0).Item("Nama_Customer").ToString & "', '" & TableDTP.Rows(0).Item("Alamat").ToString & "' , " & _
                    " '" & TableDTP.Rows(0).Item("Kota").ToString & "', '" & TableDTP.Rows(0).Item("Kode_Area_Telp").ToString & "' , '" & TableDTP.Rows(0).Item("No_Telp").ToString & "',0,0, " & _
                    " 1 , '" & Session("UserID").ToString & "', '" & TableDTP.Rows(0).Item("KotaAsalBrg").ToString & "');" & " select @id = @@identity; "

            STR &= " insert into MasterQuotation(Quotation_No,Tanggal,Customer_Id," & _
                    " Penerima,Tujuan,UP,JabatanUP,[status], UserName ) values (" & _
                    " '" & quotationno.ToString & "','" & TableDTP.Rows(0).Item("Tanggal").ToString & "','" & namacustomer.ToString & "', " & _
                    " '" & TableDTP.Rows(0).Item("Penerima").ToString & "', '" & TableDTP.Rows(0).Item("Tujuan").ToString & "', " & _
                    " '" & TableDTP.Rows(0).Item("UP").ToString & "', '" & TableDTP.Rows(0).Item("JabatanUP").ToString & "', " & _
                    " 1 , '" & Session("UserID").ToString & "');"

            For i As Integer = 0 To TableDTC.Rows.Count - 1
                iddetail = iddetail + 1
                With TableDTC.Rows(i)
                    STR &= " insert into QuotationDetail(Quotation_No,IDDetail,Customer_Id,SatuanID,Nama_Barang, " & _
                            " Harga,[status], UserName ) values (" & _
                            " '" & quotationno.ToString & "'," & iddetail & ",'" & namacustomer.ToString & "', '" & .Item("SatuanID").ToString & "'," & _
                            " '" & .Item("Nama_Barang").ToString & "', '" & .Item("Harga").ToString & "' , 1 , '" & Session("UserID").ToString & "' );"
                End With
            Next

            STR &= " UPDATE PotentialQuotation " & _
                        " SET " & _
                        " LastModified = '" & Now.ToString & "', " & _
                        " [status] = 5 " & _
                        " WHERE Quotation_No = '" & QONO.ToString & "' And [status] = 1; "

            STR &= " UPDATE PotentialQuotationDetail" & _
                         " SET " & _
                         " LastModified = '" & Now.ToString & "', " & _
                         " [status] = 5 " & _
                         " WHERE Quotation_No = '" & QONO.ToString & "' And [status] = 1; "

            result = SQLExecuteNonQuery(STR.ToString)


            Add_Clear()
            Clear()
            clear_label()
            lInfo.Visible = True
            PanelInput.Visible = False
            PanelView.Visible = True
            lInfo.Text = " Insert success "
            load_grid_parent()
        Catch ex As Exception
            Response.Write(ex.ToString)
        End Try
    End Sub

    Private Sub Insert()
        Dim result As Integer
        Dim STR As String
        Dim iddetail As Integer = 0
        lblQoNo.Text = Load_Quotation_Number()
        Try
            STR = " insert into PotentialQuotation(Quotation_No,Tanggal,Nama_Customer,Alamat,Kota, Kode_area_Telp, No_Telp," & _
                    " Penerima,Tujuan,UP,JabatanUP,[status], UserName, KotaAsalBrg ) values (" & _
                    " '" & lblQoNo.Text.ToString & "','" & tb_tgl.Text.ToString & "', " & _
                    " '" & TxtNamaCustomer.Text.ToString & "','" & TxtAlamat.Text.ToString & "','" & TxtArea.Text.ToString & "','" & txtkodearea.Text.Replace(" ", "") & "','" & TxtNoHP.Text.ToString.Replace(" ", "") & "','" & TxtPenerima.Text.ToString & "', " & _
                    " '" & DDLKotaTujuan.SelectedValue & "', '" & TxtUp.Text.ToString & "' , '" & TxtJabatan.Text.ToString & "' ," & _
                    " 1 , '" & Session("UserID").ToString & "', '" & DDLKotaASAlBarang.SelectedValue.ToString & "');"

            For i As Integer = 0 To Grid_SO.VisibleRowCount - 1
                iddetail = iddetail + 1
                With Grid_SO
                    STR &= " insert into PotentialQuotationDetail(Quotation_No,IDDetail,SatuanID,Nama_Barang, " & _
                            " Harga,[status], UserName ) values (" & _
                            " '" & lblQoNo.Text.ToString & "' , " & iddetail & ",'" & .GetRowValues(i, "SatuanIDS") & "'," & _
                            " '" & .GetRowValues(i, "NamaBarangS") & "','" & .GetRowValues(i, "HargaS").ToString & "' , 1 , '" & Session("UserID").ToString & "' );"
                End With
            Next

            result = SQLExecuteNonQuery(STR.ToString)

            If (result > 0) Then
                Add_Clear()
                Clear()
                clear_label()
                lInfo.Visible = True
                PanelInput.Visible = False
                PanelView.Visible = True
                lInfo.Text = " Insert success "
                Session("Grid_SO_ParentP") = Nothing
                Grid_SO_Parent.DataSource = Nothing
                Grid_SO_Parent.DataBind()
                Session("Grid_SOP") = Nothing
                Grid_SO.DataSource = Nothing
                Grid_SO.DataBind()
                load_grid_parent()
            End If
        Catch ex As Exception
            Response.Write(ex.ToString)
        End Try
    End Sub

    Private Sub Update(ByVal QoNo As String)
        Dim ID As String
        Dim sqlstring As String
        Dim iddetail As Integer
        Dim cekstring As String
        Dim cekdt As DataTable

        Try
            sqlstring = " UPDATE PotentialQuotation" & _
                        " SET " & _
                        " Tanggal = '" & tb_tgl.Text.ToString & "', " & _
                        " Nama_Customer = '" & TxtNamaCustomer.Text.ToString & "'," & _
                        " Alamat = '" & TxtAlamat.Text.ToString & "'," & _
                        " Kota = '" & TxtArea.Text.ToString & "'," & _
                        " Kode_Area_Telp = '" & txtkodearea.Text.Replace(" ", "") & "', " & _
                        " No_Telp = '" & TxtNoHP.Text.ToString.Replace(" ", "") & "'," & _
                        " Tujuan = '" & DDLKotaTujuan.SelectedValue.ToString & "', " & _
                        " Penerima = '" & TxtPenerima.Text.ToString & "'," & _
                        " UP = '" & TxtUp.Text.ToString & "'," & _
                        " JabatanUP = '" & TxtJabatan.Text.ToString & "'," & _
                        " LastModified = '" & Now.ToString & "', " & _
                        " [status] = 1, " & _
                        " KotaAsalBrg = '" & DDLKotaASAlBarang.SelectedValue.ToString & "' " & _
                        " WHERE Quotation_No = '" & QoNo.ToString & "' and status <>0; "
            cekstring = "select ID from PotentialQuotationDetail where Quotation_No = '" & QoNo.ToString & "' and status <>0 "
            cekdt = SQLExecuteQuery(cekstring).Tables(0)
            If cekdt.Rows.Count > 0 Then
                iddetail = getDetailID("PotentialQuotationDetail", "Quotation_No", QoNo.ToString)
            Else
                iddetail = 0
            End If
            For i As Integer = 0 To Grid_SO.VisibleRowCount - 1
                If Grid_SO.GetRowValues(i, "IDS").ToString = "" Then
                    iddetail = iddetail + 1
                    With Grid_SO
                        sqlstring &= " insert into PotentialQuotationDetail(Quotation_No,IDDetail,SatuanID,Nama_Barang, " & _
                            " Harga,[status], UserName ) values (" & _
                            " '" & lblQoNo.Text.ToString & "' , " & iddetail & ",'" & .GetRowValues(i, "SatuanIDS") & "'," & _
                            " '" & .GetRowValues(i, "NamaBarangS") & "', '" & .GetRowValues(i, "HargaS").ToString & "' , 1 , '" & Session("UserID").ToString & "' );"
                    End With
                Else
                    With Grid_SO
                        ID = .GetRowValues(i, "IDS")
                        sqlstring &= " UPDATE PotentialQuotationDetail" & _
                             " SET " & _
                             " Nama_Barang = '" & .GetRowValues(i, "NamaBarangS") & "' ," & _
                             " SatuanID = '" & .GetRowValues(i, "SatuanIDS") & "', " & _
                             " Harga = '" & .GetRowValues(i, "HargaS").ToString & "' ," & _
                             " LastModified = '" & Now.ToString & "', " & _
                             " [status] = 1 " & _
                             " WHERE Quotation_No = '" & QoNo.ToString & "' AND IDDetail = '" & .GetRowValues(i, "IDS") & "' and status <>0; "
                    End With
                End If
            Next
            sqlstring &= hfDel.Value
            result = SQLExecuteNonQuery(sqlstring)
            If result > 0 Then
                Add_Clear()
                Clear()
                clear_label()
                lInfo.Visible = True
                lInfo.Text = " Update success "
                PanelInput.Visible = False
                PanelView.Visible = True
                Session("Grid_SO_ParentP") = Nothing
                Grid_SO_Parent.DataSource = Nothing
                Grid_SO_Parent.DataBind()
                Session("Grid_SOP") = Nothing
                Grid_SO.DataSource = Nothing
                Grid_SO.DataBind()
                load_grid_parent()
                hfMode.Value = "Insert"
            End If
        Catch ex As Exception
            Response.Write(" Error Function Update <BR> " & ex.ToString)
        End Try
    End Sub

    Private Sub Remove_item(ByVal JenisBarang As String)
        Dim DT As DataTable
        Try
            DT = CType(Session("Grid_SOP"), DataTable)
            For i As Integer = 0 To DT.Rows.Count - 1
                If DT.Rows(i).Item("NamaBarangS").ToString = JenisBarang.ToString Then
                    DT.Rows(i).Delete()
                    Exit For
                End If
            Next
            create_session()
            Session("Grid_SOP") = DT
            Grid_SO.DataSource = DT
            Grid_SO.DataBind()

            Call Refresh_Grid()
            Call Add_Clear()
        Catch ex As Exception
            Response.Write("Error Remove Item : <BR> " & ex.ToString)
        End Try

    End Sub

    Private Sub Remove_itemDB(ByVal ID As String, ByVal QoNo As String)
        sqlstring = hfDel.Value
        sqlstring &= " UPDATE PotentialQuotationDetail " & _
                     " SET " & _
                     " LastModified = '" & Now.ToString & "', " & _
                     " [status] = 0 " & _
                     " WHERE IDDetail = '" & ID.ToString & "' and Quotation_No = '" & QoNo.ToString & "' and status <> 0; "

        hfDel.Value = sqlstring

        DT = CType(Session("Grid_SOP"), DataTable)
        For i As Integer = 0 To DT.Rows.Count - 1
            If DT.Rows(i).Item("IDS").ToString = ID.ToString Then
                DT.Rows(i).Delete()
                Exit For
            End If
        Next
        create_session()
        Session("Grid_SOP") = DT
        Grid_SO.DataSource = DT
        Grid_SO.DataBind()

        Call Refresh_Grid()
        Call Add_Clear()

    End Sub

    Private Sub Delete(ByVal QoNo As String)
        Dim result As Integer
        Try
            sqlstring = " UPDATE PotentialQuotation " & _
                        " SET " & _
                        " LastModified = '" & Now.ToString & "', " & _
                        " [status] = 0 " & _
                        " WHERE Quotation_No = '" & QoNo.ToString & "' And [status] = 1; "

            sqlstring &= " UPDATE PotentialQuotationDetail" & _
                         " SET " & _
                         " LastModified = '" & Now.ToString & "', " & _
                         " [status] = 0 " & _
                         " WHERE Quotation_No = '" & QoNo.ToString & "' And [status] = 1; "

            result = SQLExecuteNonQuery(sqlstring)
            If result > 0 Then
                Add_Clear()
                Clear()
                lInfo.Visible = True
                lInfo.Text = " Delete success "
                load_grid_parent()
            End If
        Catch ex As Exception
            Response.Write("Error Function Delete <BR> " & ex.ToString)
        End Try
    End Sub

    Private Sub Edit_Item()
        If hfMode.Value = "Insert" Then
            Edit_Item(TxtItem.Text.ToString)
        ElseIf hfMode.Value = "Update" Then
            Edit_Item(TxtItem.Text.ToString)
        Else
            Edit_Item(TxtItem.Text.ToString)
        End If
    End Sub

    Private Sub Add_Item()
        If hfMode.Value = "Insert" Then
            If Validation() Then
                Dim DT As DataTable
                Dim DR As DataRow
                DT = CType(Session("Grid_SOP"), DataTable)
                If DT.Rows.Count > 0 Then
                    For i As Integer = 0 To DT.Rows.Count - 1
                        If DT.Rows(i).Item("NamaBarangS").ToString = TxtItem.Text Then
                            Add_Clear()
                            lInfo.Visible = True
                            lInfo.Text = "Already ADD"
                            Exit Sub
                        End If
                    Next
                End If
                DR = DT.NewRow
                DR("Quotation_NoS") = lblQoNo.Text
                DR("NamaBarangS") = TxtItem.Text.Trim.Replace("'", "''")
                DR("SatuanIDS") = DDLSatuan.SelectedValue
                DR("NamaHargaS") = DDLSatuan.SelectedItem.Text
                DR("HargaS") = ReplaceString(TxtHargaItem.Text)
                DT.Rows.Add(DR)

                Session("Grid_SOP") = DT
                Grid_SO.DataSource = DT
                Grid_SO.DataBind()

                Call Refresh_Grid()
                Call Add_Clear()
            End If
        ElseIf hfMode.Value = "Update" Then
            Dim DT As DataTable
            Dim DR As DataRow
            DT = CType(Session("Grid_SOP"), DataTable)
            If DT.Rows.Count > 0 Then
                For i As Integer = 0 To DT.Rows.Count - 1
                    If DT.Rows(i).Item("NamaBarangS").ToString = TxtItem.Text Then
                        Add_Clear()
                        lInfo.Visible = True
                        lInfo.Text = "Already ADD"
                        Exit Sub
                    End If
                Next
            End If
            DR = DT.NewRow
            DR("IDS") = ""
            DR("Quotation_NoS") = lblQoNo.Text
            DR("NamaBarangS") = TxtItem.Text.Trim.Replace("'", "''")
            DR("SatuanIDS") = DDLSatuan.SelectedValue
            DR("NamaHargaS") = DDLSatuan.SelectedItem.Text
            DR("HargaS") = ReplaceString(TxtHargaItem.Text)
            DT.Rows.Add(DR)

            Session("Grid_SOP") = DT
            Grid_SO.DataSource = DT
            Grid_SO.DataBind()

            Call Refresh_Grid()
            Call Add_Clear()
        Else
            Dim DT As DataTable
            Dim DR As DataRow
            DT = CType(Session("Grid_SOP"), DataTable)
            If DT.Rows.Count > 0 Then
                For i As Integer = 0 To DT.Rows.Count - 1
                    If DT.Rows(i).Item("NamaBarangS").ToString = TxtItem.Text Then
                        Add_Clear()
                        lInfo.Visible = True
                        lInfo.Text = "Already ADD"
                        Exit Sub
                    End If
                Next
            End If
            DR = DT.NewRow
            DR("IDS") = ""
            DR("Quotation_NoS") = lblQoNo.Text
            DR("NamaBarangS") = TxtItem.Text.Trim.Replace("'", "''")
            DR("SatuanIDS") = DDLSatuan.SelectedValue
            DR("NamaHargaS") = DDLSatuan.SelectedItem.Text
            DR("HargaS") = ReplaceString(TxtHargaItem.Text)
            DT.Rows.Add(DR)

            Session("Grid_SOP") = DT
            Grid_SO.DataSource = DT
            Grid_SO.DataBind()

            Call Refresh_Grid()
            Call Add_Clear()
        End If
    End Sub

    Private Sub Edit_Item(ByVal JenisBarang As String)
        Dim DT As DataTable
        Try
            DT = CType(Session("Grid_SOP"), DataTable)
            If DT.Rows.Count > 0 Then
                For i As Integer = 0 To DT.Rows.Count - 1
                    If DT.Rows(i).Item("NamaBarangS").ToString = JenisBarang.ToString Then
                        DT.Rows(i).Item("Quotation_NoS") = lblQoNo.Text
                        DT.Rows(i).Item("NamaBarangS") = TxtItem.Text.Trim.Replace("'", "''")
                        DT.Rows(i).Item("SatuanIDS") = DDLSatuan.SelectedValue
                        DT.Rows(i).Item("NamaHargaS") = DDLSatuan.SelectedItem.Text
                        DT.Rows(i).Item("HargaS") = ReplaceString(TxtHargaItem.Text.Trim)
                        Exit For
                    End If
                Next
            End If
            create_session()
            Session("Grid_SOP") = DT
            Grid_SO.DataSource = DT
            Grid_SO.DataBind()

            Call Refresh_Grid()
            Call Add_Clear()
            hfModeEdit.Value = "Insert"
        Catch ex As Exception
            Response.Write("Error Edit Item : <BR> " & ex.ToString)
        End Try
    End Sub

    Private Sub Clear()

        Try
            tb_tgl.Date = Today
            TxtUp.Text = ""
            TxtJabatan.Text = ""
            TxtNamaCustomer.Text = ""
            DDLKotaTujuan.SelectedIndex = 0
            TxtPenerima.Text = ""
            hfCID.Value = ""
            TxtAlamat.Text = ""
            TxtArea.Text = ""
            TxtNoHP.Text = ""
            hfDel.Value = ""
            txtkodearea.Text = ""
            create_session()
        Catch ex As Exception
            Response.Write(ex.ToString)
        End Try

    End Sub

    Private Sub clear_label()
        lInfo.Text = ""
        linfoberhasil.Text = ""
        lInfo.Visible = False
    End Sub

    Private Sub Refresh_Grid()

        '#
        Dim DT As DataTable
        Try
            DT = CType(Session("Grid_SOP"), DataTable)
            Session("Grid_SOP") = DT
            Grid_SO.DataSource = DT
            Grid_SO.DataBind()
        Catch ex As Exception
            Response.Write(ex.ToString)
        End Try

    End Sub

    Private Sub Add_Clear()
        Try
            DDLSatuan.SelectedIndex = 0
            TxtItem.Text = ""
            TxtHargaItem.Text = ""

        Catch ex As Exception
            Response.Write(ex.ToString)
        End Try

    End Sub
#End Region

#Region "DDL"
    Private Sub load_harga()
        Dim harga As Double
        Try
            sqlstring = "select harga from MasterHargaDefault where ID = '" & DDLSatuan.SelectedValue & "' and [status]=1"
            harga = SQLExecuteScalar(sqlstring)
            TxtHargaItem.Text = harga
        Catch ex As Exception
            Throw New Exception("Error load_harga_function :" & ex.ToString)
        End Try


    End Sub

    Private Sub load_ddl_satuan()
        Try
            sqlstring = " SELECT ID,NamaHarga from MasterHargaDefault where [status] = 1 order by NamaHarga"
            Dim dt As DataTable = SQLExecuteQuery(sqlstring).Tables(0)
            With DDLSatuan
                .DataSource = dt
                .DataTextField = "NamaHarga"
                .DataValueField = "ID"
                .DataBind()
            End With
            DDLSatuan.Items.Insert(0, "-Please Select Satuan-")
        Catch ex As Exception
            Throw New Exception("Error load_ddl_satuan function :" & ex.ToString)
        End Try

    End Sub

    'Private Sub DDLSatuan_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DDLSatuan.SelectedIndexChanged
    '    load_harga()
    '    Dim namacontainer As String
    '    If DDLSatuan.SelectedItem.Text = "Kubik" Then
    '        hfOID.Value = ""
    '        TxtItem.Text = ""
    '    ElseIf DDLSatuan.SelectedItem.Text = "Ton" Then
    '        hfOID.Value = ""
    '        TxtItem.Text = ""
    '    ElseIf DDLSatuan.SelectedItem.Text = "Unit" Then
    '        hfOID.Value = ""
    '        TxtItem.Text = ""
    '    ElseIf DDLSatuan.SelectedItem.Text = "Satuan" Then
    '        hfOID.Value = ""
    '        TxtItem.Text = ""
    '    Else
    '        namacontainer = loadnama()
    '        hfOID.Value = ""
    '        TxtItem.Text = namacontainer
    '    End If
    'End Sub
    Private Function loadnama() As String
        Dim nama As String
        Dim no As Integer
        DT = CType(Session("Grid_SOP"), DataTable)
        If DT.Rows.Count > 0 Then
            no = 1
            For i As Integer = 0 To DT.Rows.Count - 1
                If DT.Rows(i).Item("NamaHargaS") = "Container" Or DT.Rows(i).Item("NamaHargaS") = "Kontainer" Then
                    no = no + 1
                End If
            Next
        Else
            no = 1
        End If

        nama = "Container " & no
        Return nama
    End Function

    Private Sub LoadDDLKota()
        Try
            sqlstring = " SELECT DISTINCT" & _
                          "		ID, " & _
                          "	Tujuan, status " & _
                          "	FROM MasterTujuan " & _
                          " WHERE status = 1 ORDER BY Tujuan "
            DS = SQLExecuteQuery(sqlstring)
            DT = DS.Tables(0)

            DDLKotaASAlBarang.Items.Clear()
            DDLKotaTujuan.Items.Clear()

            With DDLKotaASAlBarang
                .DataSource = DT
                .DataTextField = "Tujuan"
                .DataValueField = "Tujuan"
                .DataBind()
            End With

            DDLKotaASAlBarang.Items.Insert(0, "Pilih Kota Asal Barang")

            With DDLKotaTujuan
                .DataSource = DT
                .DataTextField = "Tujuan"
                .DataValueField = "Tujuan"
                .DataBind()
            End With

            DDLKotaTujuan.Items.Insert(0, "Pilih Kota Tujuan")
        Catch ex As Exception
            Throw New Exception(ex.ToString)
        End Try
    End Sub

#End Region
    
   

End Class