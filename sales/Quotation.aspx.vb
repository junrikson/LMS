Imports System.Data
Imports System.Data.SqlClient
Imports DevExpress.Web.ASPxGridView

Partial Class Sales_Quotation
    Inherits System.Web.UI.Page
    Private DS As DataSet
    Private DT As DataTable
    Private DR As DataRow
    Private sqlstring As String
    Private iDT As New DataTable
    Private aDT As New DataTable
    Private cDT As New DataTable
    Dim hasil As Integer
    Dim STR As String = ""
    Dim result As String

#Region "PAGE"

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            If Session("UserID") = Nothing Then
                FormsAuthentication.SignOut()
                Response.Redirect("Index.aspx")
            End If

            If Not Page.IsPostBack Then
                PanelApprove.Visible = True
                Panel_Close.Visible = False
                Session("Grid_SO") = Nothing
                Session("Grid_SO_Parent") = Nothing
                Session("Grid_Parent_Close") = Nothing
                PanelButton()
                LoadDDLKota()
                hfCID.Value = ""
                hfStatusClose.Value = "False"
                hfClose.Value = ""
                tb_tgl.Date = Today
                load_ddl_satuan()
                Session("iddetail") = Nothing
                load_grid_parent()
                Call create_session()
                lblQoNo.Visible = True
                lblQoNo.Text = Load_Quotation_Number()
                hfMode.Value = "Insert"
                hfModeEdit.Value = "Insert"
                hfItem.Value = ""
                TxtHargaItem.Attributes.Add("onkeyup", "changenumberformat('" & TxtHargaItem.ClientID & "')")
            End If

            If Not Session("Grid_SO") Is Nothing Then
                Grid_SO.DataSource = CType(Session("Grid_SO"), DataTable)
                Grid_SO.DataBind()
            End If

            If Not Session("Grid_SO_Parent") Is Nothing Then
                Grid_SO_Parent.DataSource = CType(Session("Grid_SO_Parent"), DataTable)
                Grid_SO_Parent.DataBind()
            End If

            If Not Session("Grid_Parent_Close") Is Nothing Then
                Grid_Parent_Close.DataSource = CType(Session("Grid_Parent_Close"), DataTable)
                Grid_Parent_Close.DataBind()
            End If

        Catch ex As Exception
            Response.Write("Error Page Load :<BR>" & ex.ToString)
        End Try
    End Sub

    Private Function Load_Quotation_Number() As String
        Dim month, year, tanggal As String
        Dim value As String
        Dim no As Integer
        Dim pisah As String()
        Dim zDT As DataTable
        Dim strData As String = ""

        Try
            month = Date.Today.ToString("MM")
            year = Date.Today.ToString("yy")
            tanggal = Date.Today.ToString("dd")

            strData = "SELECT TOP 1 Quotation_No FROM MasterQuotation where status <> 0 and Tanggal BETWEEN '1/1/' + '" & Now.ToString("yyyy") & "' AND '12/31/' + '" & Now.ToString("yyyy") & "' " & _
                        "ORDER BY ID DESC"
            zDT = SQLExecuteQuery(strData).Tables(0)


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

    Private Function loadnama() As String
        Dim nama As String
        Dim no As Integer
        DT = CType(Session("Grid_SO"), DataTable)
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
            aDT.Columns.Add(New DataColumn("KeteranganS", GetType(String)))

            Session("Grid_SO") = aDT
            Grid_SO.DataSource = aDT
            Grid_SO.KeyFieldName = "IDS"
            Grid_SO.DataBind()

        Catch ex As Exception
            Response.Write("Error Load Create Session :<BR>" & ex.ToString)
        End Try

    End Sub

    Private Sub load_grid(ByVal qono As String)
        Try
            Session("Grid_SO") = Nothing
            Dim ID As String
            If hfMode.Value = "Close" Then
                sqlstring = "SELECT qd.IDDetail, qd.Quotation_No, qd.Nama_Barang, qd.SatuanID,mh.NamaHarga ," & _
                        " qd.Harga,qd.QuotationClose_No, qd.Keterangan FROM QuotationDetail qd " & _
                        "left Join MasterHargaDefault mh on qd.SatuanID = mh.ID " & _
                        "Where qd.Quotation_No = '" & qono.ToString & "' AND qd.status = 5 Order by qd.timestamp desc"
            Else
                sqlstring = "SELECT qd.IDDetail, qd.Quotation_No, qd.Nama_Barang, qd.SatuanID,mh.NamaHarga ," & _
                        " qd.Harga,qd.QuotationClose_No, qd.Keterangan FROM QuotationDetail qd " & _
                        "left Join MasterHargaDefault mh on qd.SatuanID = mh.ID " & _
                        "Where qd.Quotation_No = '" & qono.ToString & "' AND (qd.status = 1 or qd.status = 2) Order by qd.timestamp desc"
            End If

            aDT.Columns.Add(New DataColumn("IDS", GetType(String)))
            aDT.Columns.Add(New DataColumn("Quotation_NoS", GetType(String)))
            aDT.Columns.Add(New DataColumn("NamaBarangS", GetType(String)))
            aDT.Columns.Add(New DataColumn("SatuanIDS", GetType(String)))
            aDT.Columns.Add(New DataColumn("NamaHargaS", GetType(String)))
            aDT.Columns.Add(New DataColumn("HargaS", GetType(Double)))
            aDT.Columns.Add(New DataColumn("QuotationClose_NoS", GetType(String)))
            aDT.Columns.Add(New DataColumn("KeteranganS", GetType(String)))

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
                        DR("QuotationClose_NoS") = .Item("QuotationClose_No").ToString
                        DR("KeteranganS") = .Item("Keterangan").ToString
                        aDT.Rows.Add(DR)
                    End With
                Next
                Session("Grid_SO") = aDT
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
            sqlstring = " SELECT QO.ID, QO.Quotation_No, QO.Tanggal, QO.Customer_ID, C.Nama_Customer, C.Kode_Customer, " & _
                        " QO.Penerima,QO.UP,QO.JabatanUP, QO.Tujuan,QO.[Close],QO.QuotationClose_No, QO.Username " & _
                        " FROM MasterQuotation QO JOIN MasterCustomer C " & _
                        " ON qo.Customer_ID = C.Kode_Customer WHERE (qo.status = 1 or qo.status = 2) AND C.status = 1 Order by qo.timestamp desc"
            DS = SQLExecuteQuery(sqlstring)
            DT = DS.Tables(0)

            iDT.Columns.Add(New DataColumn("ID", GetType(String)))
            iDT.Columns.Add(New DataColumn("Quotation_No", GetType(String)))
            iDT.Columns.Add(New DataColumn("Tanggal", GetType(DateTime)))
            iDT.Columns.Add(New DataColumn("Cust_ID", GetType(String)))
            iDT.Columns.Add(New DataColumn("Cust_Name", GetType(String)))
            iDT.Columns.Add(New DataColumn("Cust_Code", GetType(String)))
            iDT.Columns.Add(New DataColumn("Tujuan", GetType(String)))
            iDT.Columns.Add(New DataColumn("Penerima", GetType(String)))
            iDT.Columns.Add(New DataColumn("UP", GetType(String)))
            'iDT.Columns.Add(New DataColumn("Pembayar", GetType(String)))
            'iDT.Columns.Add(New DataColumn("KotaPembayar", GetType(String)))
            iDT.Columns.Add(New DataColumn("JabatanUP", GetType(String)))
            iDT.Columns.Add(New DataColumn("Close", GetType(String)))
            iDT.Columns.Add(New DataColumn("QuotationClose_No", GetType(String)))
            iDT.Columns.Add(New DataColumn("YgInput", GetType(String)))
            If DT.Rows.Count > 0 Then
                For i As Integer = 0 To DT.Rows.Count - 1
                    With DT.Rows(i)
                        DR = iDT.NewRow()
                        DR("ID") = .Item("ID").ToString
                        DR("Quotation_No") = .Item("Quotation_No").ToString
                        DR("Tanggal") = Changedate(.Item("Tanggal").ToString, 1)
                        DR("Cust_ID") = .Item("Kode_Customer").ToString
                        DR("Cust_Name") = .Item("Nama_Customer").ToString
                        DR("Cust_Code") = .Item("Kode_Customer").ToString
                        DR("Tujuan") = .Item("Tujuan").ToString
                        DR("Penerima") = .Item("Penerima").ToString
                        DR("UP") = .Item("UP").ToString
                        'DR("Pembayar") = .Item("Pembayar").ToString
                        'DR("KotaPembayar") = .Item("KotaPembayar").ToString
                        DR("JabatanUP") = .Item("JabatanUP").ToString
                        DR("Close") = .Item("Close").ToString
                        DR("QuotationClose_No") = .Item("QuotationClose_No").ToString
                        DR("YgInput") = .Item("Username").ToString
                        iDT.Rows.Add(DR)
                    End With
                Next

                If Session("namaroles").ToString.Trim = "Admin" Or Session("namaroles").ToString.Trim = "Master Accounting" Or Session("namaroles").ToString.Trim = "Accounting" Then
                    Grid_SO_Parent.Columns("YgInput").Visible = True
                Else
                    Grid_SO_Parent.Columns("YgInput").Visible = False
                End If

                Session("Grid_SO_Parent") = iDT
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

    Private Sub load_grid_parent_close()
        Try
            sqlstring = " SELECT QO.ID, QO.Quotation_No, QO.Tanggal, QO.Customer_ID, C.Nama_Customer, C.Kode_Customer, " & _
                        " QO.Penerima,QO.UP ,QO.JabatanUP,QO.Tujuan" & _
                        " FROM MasterQuotation QO JOIN MasterCustomer C" & _
                        " ON QO.Customer_ID = C.Kode_Customer WHERE QO.status = 5 AND C.status = 1 Order by QO.timestamp desc"

            DS = SQLExecuteQuery(sqlstring)
            DT = DS.Tables(0)

            iDT.Columns.Add(New DataColumn("ID", GetType(String)))
            iDT.Columns.Add(New DataColumn("Quotation_No", GetType(String)))
            iDT.Columns.Add(New DataColumn("Tanggal", GetType(DateTime)))
            iDT.Columns.Add(New DataColumn("Cust_ID", GetType(String)))
            iDT.Columns.Add(New DataColumn("Cust_Name", GetType(String)))
            iDT.Columns.Add(New DataColumn("Cust_Code", GetType(String)))
            iDT.Columns.Add(New DataColumn("Tujuan", GetType(String)))
            iDT.Columns.Add(New DataColumn("Penerima", GetType(String)))
            iDT.Columns.Add(New DataColumn("UP", GetType(String)))
            iDT.Columns.Add(New DataColumn("JabatanUP", GetType(String)))
            If DT.Rows.Count > 0 Then
                For i As Integer = 0 To DT.Rows.Count - 1
                    With DT.Rows(i)
                        DR = iDT.NewRow()
                        DR("ID") = .Item("ID").ToString
                        DR("Quotation_No") = .Item("Quotation_No").ToString
                        DR("Tanggal") = CDate(.Item("Tanggal").ToString).ToString("MM/dd/yyyy")
                        DR("Cust_ID") = .Item("Kode_Customer").ToString
                        DR("Cust_Name") = .Item("Nama_Customer").ToString
                        DR("Cust_Code") = .Item("Kode_Customer").ToString
                        DR("Tujuan") = .Item("Tujuan").ToString
                        DR("Penerima") = .Item("Penerima").ToString
                        DR("UP") = .Item("UP").ToString
                        DR("JabatanUP") = .Item("JabatanUP").ToString
                        iDT.Rows.Add(DR)
                    End With
                Next
                Session("Grid_Parent_Close") = iDT
                Grid_Parent_Close.DataSource = iDT
                Grid_Parent_Close.KeyFieldName = "Quotation_No"
                Grid_Parent_Close.DataBind()
            Else
                Grid_Parent_Close.DataSource = Nothing
                Grid_Parent_Close.DataBind()
            End If

        Catch ex As Exception
            Response.Write("Error Load Grid Parent Close:<BR>" & ex.ToString)
        End Try
    End Sub

    Private Sub load_grid_child(ByVal grid As ASPxGridView)
        Try

            sqlstring = "SELECT qd.IDDetail as ID, qd.Quotation_No, qd.Nama_Barang, qd.SatuanID,mh.NamaHarga ," & _
                        "qd.Harga, qd.Keterangan FROM QuotationDetail qd " & _
                        "left Join MasterHargaDefault mh on qd.SatuanID = mh.ID " & _
                        "Where qd.Quotation_No = '" & grid.GetMasterRowKeyValue() & "' AND (qd.status = 1 or qd.status = 2) Order by qd.timestamp desc"

            DS = SQLExecuteQuery(sqlstring)
            DT = DS.Tables(0)
            grid.DataSource = DT
        Catch ex As Exception
            Response.Write("Error Load Grid Child :<BR>" & ex.ToString)
        End Try
    End Sub

    Private Sub load_grid_child_close(ByVal grid As ASPxGridView)
        Try
            sqlstring = "SELECT qd.IDDetail as ID, (Select case qd.QuotationClose_No when '' then (qd.Quotation_No) " & _
                                "else (qd.QuotationClose_No)END ) as Quotation_No, qd.Nama_Barang, qd.SatuanID,mh.NamaHarga , " & _
                                " qd.Harga, qd.Keterangan FROM QuotationDetail qd " & _
                                "left Join MasterHargaDefault mh on qd.SatuanID = mh.ID " & _
                                "Where qd.Quotation_No = '" & grid.GetMasterRowKeyValue() & "' AND qd.status = 5 Order by qd.timestamp desc"
            DS = SQLExecuteQuery(sqlstring)
            DT = DS.Tables(0)
            grid.DataSource = DT
        Catch ex As Exception
            Response.Write("Error Load Grid Child :<BR>" & ex.ToString)
        End Try
    End Sub

    Protected Sub Grid_Child_Close_DataSelect(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Call load_grid_child_close(TryCast(sender, ASPxGridView))
        Catch ex As Exception
            Response.Write("Error Load Grid Child DataSelect  :<BR>" & ex.ToString)
        End Try
    End Sub

    Protected Sub Grid_SO_Child_DataSelect(ByVal sender As Object, ByVal e As EventArgs)
        Try
            Call load_grid_child(TryCast(sender, ASPxGridView))
        Catch ex As Exception
            Response.Write("Error Load Grid Child DataSelect  :<BR>" & ex.ToString)
        End Try
    End Sub

    Private Sub Grid_SO_Parent_HtmlRowCreated(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewTableRowEventArgs) Handles Grid_SO_Parent.HtmlRowCreated
        Try
            If e.RowType <> DevExpress.Web.ASPxGridView.GridViewRowType.Data Then
                Return
            End If
            Dim butto As LinkButton = CType(Grid_SO_Parent.FindRowCellTemplateControl(e.VisibleIndex, Nothing, "tbEdit"), System.Web.UI.WebControls.LinkButton)
            Dim buttoU As LinkButton = CType(Grid_SO_Parent.FindRowCellTemplateControl(e.VisibleIndex, Nothing, "tbUpdate"), System.Web.UI.WebControls.LinkButton)

            If (Grid_SO_Parent.GetRowValues(e.VisibleIndex, "Close").ToString = "Deal") Then
                butto.Visible = False
                buttoU.Visible = True
            Else
                butto.Visible = True
                buttoU.Visible = False
            End If
        Catch ex As Exception
            Throw New Exception("Error function HTML row created :" & ex.ToString)
        End Try
    End Sub

    Protected Sub Grid_SO_Parent_RowCommand(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewRowCommandEventArgs) Handles Grid_SO_Parent.RowCommand
        Try
            clear_label()

            Select Case e.CommandArgs.CommandName
                Case "Update"
                    hfMode.Value = "UpdateQuotation"
                    hfCID.Value = Grid_SO_Parent.GetRowValues(e.VisibleIndex, "Cust_ID").ToString
                    HfSo.Value = Grid_SO_Parent.GetRowValues(e.VisibleIndex, "Quotation_No").ToString
                    TxtCustName.Text = Grid_SO_Parent.GetRowValues(e.VisibleIndex, "Cust_Name").ToString
                    hfNamaCustomer.Value = Grid_SO_Parent.GetRowValues(e.VisibleIndex, "Cust_Name").ToString
                    hfCodeCustomer.Value = Grid_SO_Parent.GetRowValues(e.VisibleIndex, "Cust_Code").ToString
                    TxtCustCode.Text = Grid_SO_Parent.GetRowValues(e.VisibleIndex, "Cust_Code").ToString

                    lblQoNo.Text = Grid_SO_Parent.GetRowValues(e.VisibleIndex, "Quotation_No").ToString
                    TxtPenerima.Text = Grid_SO_Parent.GetRowValues(e.VisibleIndex, "Penerima").ToString

                    TxtUp.Text = Grid_SO_Parent.GetRowValues(e.VisibleIndex, "UP").ToString
                    DDLKotaTujuan.SelectedValue = Grid_SO_Parent.GetRowValues(e.VisibleIndex, "Tujuan").ToString
                    TxtJabatan.Text = Grid_SO_Parent.GetRowValues(e.VisibleIndex, "JabatanUP").ToString
                    hfStatusClose.Value = Grid_SO_Parent.GetRowValues(e.VisibleIndex, "Close").ToString
                    lblQoNo.Text = Load_Quotation_Number()
                    load_grid(Grid_SO_Parent.GetRowValues(e.VisibleIndex, "Quotation_No").ToString)

                Case "Edit"
                    hfMode.Value = "Update"
                    hfCID.Value = Grid_SO_Parent.GetRowValues(e.VisibleIndex, "Cust_ID").ToString
                    HfSo.Value = Grid_SO_Parent.GetRowValues(e.VisibleIndex, "Quotation_No").ToString
                    TxtCustName.Text = Grid_SO_Parent.GetRowValues(e.VisibleIndex, "Cust_Name").ToString
                    TxtCustCode.Text = Grid_SO_Parent.GetRowValues(e.VisibleIndex, "Cust_Code").ToString
                    hfNamaCustomer.Value = Grid_SO_Parent.GetRowValues(e.VisibleIndex, "Cust_Name").ToString
                    hfCodeCustomer.Value = Grid_SO_Parent.GetRowValues(e.VisibleIndex, "Cust_Code").ToString
                    lblQoNo.Text = Grid_SO_Parent.GetRowValues(e.VisibleIndex, "Quotation_No").ToString
                    TxtPenerima.Text = Grid_SO_Parent.GetRowValues(e.VisibleIndex, "Penerima").ToString
                    'TxtPembayaranOleh.Text = Grid_SO_Parent.GetRowValues(e.VisibleIndex, "Pembayar").ToString
                    'DDLKotaPembayar.SelectedValue = Grid_SO_Parent.GetRowValues(e.VisibleIndex, "KotaPembayar").ToString
                    TxtUp.Text = Grid_SO_Parent.GetRowValues(e.VisibleIndex, "UP").ToString
                    DDLKotaTujuan.SelectedValue = Grid_SO_Parent.GetRowValues(e.VisibleIndex, "Tujuan").ToString
                    TxtJabatan.Text = Grid_SO_Parent.GetRowValues(e.VisibleIndex, "JabatanUP").ToString
                    hfStatusClose.Value = Grid_SO_Parent.GetRowValues(e.VisibleIndex, "Close").ToString
                    load_grid(Grid_SO_Parent.GetRowValues(e.VisibleIndex, "Quotation_No").ToString)
                Case "Delete"
                    Dim hsl As Integer

                    If Grid_SO_Parent.GetRowValues(e.VisibleIndex, "Close").ToString = "False" Then

                        hsl = CekUsingNumber("WarehouseHeader", "Quotation_No", Grid_SO_Parent.GetRowValues(e.VisibleIndex, "Quotation_No").ToString)

                        hsl += CekUsingNumber("ContainerHeader", "Quotation_No", Grid_SO_Parent.GetRowValues(e.VisibleIndex, "Quotation_No").ToString)

                        If hsl = 0 Then
                            Delete(Grid_SO_Parent.GetRowValues(e.VisibleIndex, "Quotation_No").ToString)
                        Else
                            lblinfo.Visible = True
                            lblinfo.Text = " Anda Tidak Boleh Mendelete Quotation Yang sudah diPakai  "
                        End If

                    ElseIf Grid_SO_Parent.GetRowValues(e.VisibleIndex, "Close").ToString = "Deal" Then
                        'Close(Grid_SO_Parent.GetRowValues(e.VisibleIndex, "Quotation_No").ToString)
                        'cekClose(Grid_SO_Parent.GetRowValues(e.VisibleIndex, "Quotation_No").ToString)
                        'Add_Clear()
                        'Clear()
                        clear_label()
                        lblinfo.Visible = True
                        lblinfo.Text = " Anda Tidak Boleh Mendelete Quotation Yang sudah diPakai  "
                        'load_grid_parent()
                    ElseIf Grid_SO_Parent.GetRowValues(e.VisibleIndex, "Close").ToString = "True" Then
                        Delete(Grid_SO_Parent.GetRowValues(e.VisibleIndex, "Quotation_No").ToString)
                        CloseUse(Grid_SO_Parent.GetRowValues(e.VisibleIndex, "QuotationClose_No").ToString)
                        'Add_Clear()
                        'Clear()
                        lblinfo.Visible = True
                        lblinfo.Text = " Close success "
                        'load_grid_parent()
                    End If
                    'Case "Close"
                    '    Close(Grid_SO_Parent.GetRowValues(e.VisibleIndex, "Quotation_No").ToString)
                Case "Print"
                    Session("Quotation_No") = Grid_SO_Parent.GetRowValues(e.VisibleIndex, "Quotation_No").ToString
                    Response.Redirect("../Report/ReportQuoKub.aspx")
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
                    Else
                        If Grid_SO.GetRowValues(e.VisibleIndex, "IDS").ToString <> "" Then
                            lblinfo.Visible = True
                            lblinfo.Text = "Anda Tidak bisa mendelete data yg sudah ada(karena data sudah terpakai)."
                        Else
                            Remove_Item_When_Update_Quotation(e.VisibleIndex)
                        End If
                        
                    End If

                Case "Edit"

                    If hfMode.Value = "Insert" Then
                        hfModeEdit.Value = "Edit"
                        TxtItem.Text = Grid_SO.GetRowValues(e.VisibleIndex, "NamaBarangS").ToString
                        hfItem.Value = Grid_SO.GetRowValues(e.VisibleIndex, "NamaBarangS").ToString
                        If Grid_SO.GetRowValues(e.VisibleIndex, "SatuanIDS").ToString = "" Then
                            DDLSatuan.SelectedValue = 0
                        Else
                            DDLSatuan.SelectedValue = Grid_SO.GetRowValues(e.VisibleIndex, "SatuanIDS").ToString
                        End If
                        'hfOID.Value = Grid_SO.GetRowValues(e.VisibleIndex, "OtherIDS").ToString
                        TxtHargaItem.Text = UbahKoma(Grid_SO.GetRowValues(e.VisibleIndex, "HargaS").ToString)
                        TxtKeterangan.Text = Grid_SO.GetRowValues(e.VisibleIndex, "KeteranganS").ToString
                    ElseIf hfMode.Value = "Update" Then
                        hfModeEdit.Value = "Edit"
                        TxtItem.Text = Grid_SO.GetRowValues(e.VisibleIndex, "NamaBarangS").ToString
                        hfItem.Value = Grid_SO.GetRowValues(e.VisibleIndex, "NamaBarangS").ToString
                        If Grid_SO.GetRowValues(e.VisibleIndex, "SatuanIDS").ToString = "" Then
                            DDLSatuan.SelectedValue = 0
                        Else
                            DDLSatuan.SelectedValue = Grid_SO.GetRowValues(e.VisibleIndex, "SatuanIDS").ToString
                        End If
                        TxtHargaItem.Text = UbahKoma(Grid_SO.GetRowValues(e.VisibleIndex, "HargaS").ToString)
                        hfID.Value = Grid_SO.GetRowValues(e.VisibleIndex, "IDS").ToString
                        TxtKeterangan.Text = Grid_SO.GetRowValues(e.VisibleIndex, "KeteranganS").ToString
                    Else
                        hfModeEdit.Value = "Edit"
                        TxtItem.Text = Grid_SO.GetRowValues(e.VisibleIndex, "NamaBarangS").ToString
                        hfItem.Value = Grid_SO.GetRowValues(e.VisibleIndex, "NamaBarangS").ToString
                        If Grid_SO.GetRowValues(e.VisibleIndex, "SatuanIDS").ToString = "" Then
                            DDLSatuan.SelectedValue = 0
                        Else
                            DDLSatuan.SelectedValue = Grid_SO.GetRowValues(e.VisibleIndex, "SatuanIDS").ToString
                        End If
                        TxtHargaItem.Text = ReplaceString(Format(CDbl(Grid_SO.GetRowValues(e.VisibleIndex, "HargaS").ToString), "###,###,###").ToString)
                        hfID.Value = Grid_SO.GetRowValues(e.VisibleIndex, "IDS").ToString
                        TxtKeterangan.Text = Grid_SO.GetRowValues(e.VisibleIndex, "KeteranganS").ToString
                        If Grid_SO.GetRowValues(e.VisibleIndex, "IDS").ToString = "" Then
                            DDLSatuan.Enabled = True
                        Else
                            DDLSatuan.Enabled = False
                        End If

                    End If

                    'If hfClose.Value = "Use" Then
                    '    lblinfo.Visible = True
                    '    lblinfo.Text = "Anda Tidak boleh mengedit data dalam function ""Use"" .harap pakai function ""Userev"" untuk mengedit data "
                    'Else
                    'End If
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

    Protected Sub Grid_Parent_Close_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Grid_Parent_Close.PreRender
        If Not Page.IsPostBack Then
            Grid_Parent_Close.FocusedRowIndex = -1
        End If
    End Sub

    Protected Sub Grid_SO_Parent_CustomCallback(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs) Handles Grid_SO_Parent.CustomCallback
        Call load_grid_parent()
    End Sub

    Private Sub Grid_Parent_Close_CustomCallback(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs) Handles Grid_Parent_Close.CustomCallback
        Call load_grid_parent_close()
    End Sub

#End Region

#Region "BUTTON"
    Protected Sub btn_add_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_add.Click
        If hfModeEdit.Value = "Insert" Then
            If Validation() Then
                Add_Item()
                hfOID.Value = ""
                TxtCustName.Text = hfNamaCustomer.Value
                TxtCustCode.Text = hfCodeCustomer.Value
            End If
        Else
            If Validation() Then
                Edit_Item()
                hfOID.Value = ""
                TxtCustName.Text = hfNamaCustomer.Value
                TxtCustCode.Text = hfCodeCustomer.Value
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
                    If validationInsert() Then
                        Insert()
                    End If


                ElseIf hfMode.Value = "Update" Then
                    Update(lblQoNo.Text.ToString)
                Else
                    UpdateQuotation(HfSo.Value)
                    'If hfClose.Value = "Use" Then
                    '    Insert_Close(lblQoNo.Text.ToString)
                    'ElseIf hfClose.Value = "UseRev" Then
                    '    Insert_CloseRev(lblQoNo.Text.ToString)
                    'End If
                End If

            End If

        Catch ex As Exception
            Response.Write("Error Button Save : <BR> " & ex.ToString)
        End Try
    End Sub

#End Region

#Region "METHOD"
    Private Sub Insert()
        Dim result As Integer
        Dim iddetail As Integer = 0
        Dim noquo As String = Load_Quotation_Number()
        Try
            STR &= " insert into MasterQuotation(Quotation_No,Tanggal,Customer_Id," & _
                    " Penerima,Tujuan,UP,JabatanUP,[status], UserName,[Close],QuotationClose_No) values (" & _
                    " '" & noquo & "','" & Changedate(tb_tgl.Text.ToString, 2) & "', " & _
                    " '" & hfCID.Value & "','" & TxtPenerima.Text.ToString & "', " & _
                    " '" & DDLKotaTujuan.SelectedValue & "', '" & TxtUp.Text.ToString & "' , '" & TxtJabatan.Text.ToString & "' ," & _
                    " 1 , '" & Session("UserID").ToString & "','False','');"

            For i As Integer = 0 To Grid_SO.VisibleRowCount - 1

                With Grid_SO
                    iddetail = iddetail + 1
                    STR &= " insert into QuotationDetail(Quotation_No,IDDetail,Customer_Id,SatuanID,Nama_Barang, " & _
                            " Harga,[status], UserName,QuotationClose_No, Keterangan ) values (" & _
                            " '" & noquo & "' , " & iddetail & ",'" & hfCID.Value & "', '" & .GetRowValues(i, "SatuanIDS") & "'," & _
                            " '" & .GetRowValues(i, "NamaBarangS") & "', " & _
                            " '" & .GetRowValues(i, "HargaS").ToString & "' , 1 , '" & Session("UserID").ToString & "','','" & .GetRowValues(i, "KeteranganS").ToString & "' );"
                End With
            Next


            result = SQLExecuteNonQuery(STR)

            If result <> 0 Then
                GetQuotation(noquo)
                Session("Grid_SO") = Nothing
                Session("Grid_SO_Parent") = Nothing
                Add_Clear()
                Clear()
                LblinfoS.Visible = True
                LblinfoS.Text = " Insert success "
                load_grid_parent()
                lblQoNo.Text = Load_Quotation_Number()
                STR = ""
            End If
        Catch ex As Exception
            Response.Write(ex.ToString)
        End Try
    End Sub

    Private Sub GetQuotation(ByVal quotationNo As String)
        Try
            sqlstring = "Select ID FROM QuotationDetail where Quotation_No = '" & quotationNo & "' AND [status] <> 0 "
            DT = SQLExecuteQuery(sqlstring).Tables(0)

            sqlstring = ""
            For i As Integer = 0 To DT.Rows.Count - 1
                sqlstring &= "UPDATE QuotationDetail Set IDDetail = '" & i + 1 & "' " & _
                            "WHERE ID = " & DT.Rows(i).Item("ID") & " AND Quotation_No = '" & quotationNo & "' ; "

            Next

            If sqlstring <> "" Then
                SQLExecuteNonQuery(sqlstring)
            End If

        Catch ex As Exception
            Throw New Exception(ex.ToString)
        End Try
    End Sub

    Private Sub Update(ByVal QoNo As String)
        Dim ID As String
        Dim iddetail As Integer = 0
        Try
            sqlstring = " UPDATE MasterQuotation" & _
                        " SET " & _
                        " Tanggal = '" & Changedate(tb_tgl.Text.ToString, 2) & "', " & _
                        " Customer_Id = '" & hfCID.Value & "'," & _
                        " Tujuan = '" & DDLKotaTujuan.SelectedValue & "', " & _
                        " Penerima = '" & TxtPenerima.Text.ToString & "'," & _
                        " UP = '" & TxtUp.Text.ToString & "'," & _
                        " JabatanUP = '" & TxtJabatan.Text.ToString & "'," & _
                        " LastModified = '" & Now.ToString & "' " & _
                        " WHERE Quotation_No = '" & QoNo.ToString & "' and [status] = 1; "

            iddetail = getDetailID("QuotationDetail", "Quotation_No", lblQoNo.Text.ToString)

            For i As Integer = 0 To Grid_SO.VisibleRowCount - 1
                If Grid_SO.GetRowValues(i, "IDS").ToString = "" Then
                    iddetail = iddetail + 1
                    With Grid_SO
                        sqlstring &= " insert into QuotationDetail(Quotation_No,IDDetail,Customer_Id,SatuanID,Nama_Barang, " & _
                            " Harga,[status], UserName,QuotationClose_No, Keterangan ) values (" & _
                            " '" & lblQoNo.Text.ToString & "' , " & iddetail & ",'" & hfCID.Value & "', '" & .GetRowValues(i, "SatuanIDS") & "'," & _
                            " '" & .GetRowValues(i, "NamaBarangS") & "', " & _
                            " '" & .GetRowValues(i, "HargaS").ToString & "' , 1 , '" & Session("UserID").ToString & "','', '" & .GetRowValues(i, "KeteranganS").ToString & "' );"
                    End With
                Else
                    With Grid_SO
                        ID = .GetRowValues(i, "IDS")
                        sqlstring &= " UPDATE QuotationDetail" & _
                             " SET " & _
                             " Customer_Id = '" & hfCID.Value & "', " & _
                             " Nama_Barang = '" & .GetRowValues(i, "NamaBarangS") & "' ," & _
                             " SatuanID = '" & .GetRowValues(i, "SatuanIDS") & "', " & _
                             " Harga = '" & .GetRowValues(i, "HargaS").ToString & "' ," & _
                             " Keterangan = '" & .GetRowValues(i, "KeteranganS").ToString & "', " & _
                             " LastModified = '" & Now.ToString & "' " & _
                             " WHERE Quotation_No = '" & QoNo.ToString & "' AND IDDetail = '" & .GetRowValues(i, "IDS") & "' and status <> 0 ; "
                    End With
                End If
            Next
            sqlstring &= hfDel.Value
            result = SQLExecuteNonQuery(sqlstring)
            If result <> 0 Then
                Session("Grid_SO") = Nothing
                Session("Grid_SO_Parent") = Nothing
                Add_Clear()
                Clear()
                LblinfoS.Visible = True
                LblinfoS.Text = " Update success "
                load_grid_parent()
                hfMode.Value = "Insert"
                STR = ""
            End If
        Catch ex As Exception
            Response.Write(" Error Function Update <BR> " & ex.ToString)
        End Try
    End Sub
    Private Sub UpdateQuotation(ByVal QoNo As String)
        Dim ID As String
        Dim iddetail As Integer = 0


        Try
            sqlstring = " UPDATE MasterQuotation" & _
                        " SET " & _
                        " LastModified = '" & Now.ToString & "', " & _
                        " [status] = 5 " & _
                        " WHERE Quotation_No = '" & QoNo.ToString & "' and [status] <> 0; "

            sqlstring &= " UPDATE QuotationDetail " & _
                     " SET " & _
                     " LastModified = '" & Now.ToString & "', " & _
                     " [status] = 5 " & _
                     " WHERE Quotation_No = '" & QoNo.ToString & "' And [status] <> 0; "


            iddetail = getDetailID("QuotationDetail", "Quotation_No", QoNo)

            lblQoNo.Text = Load_Quotation_Number()

            sqlstring &= " insert into MasterQuotation(Quotation_No,Tanggal,Customer_Id," & _
                   " Penerima,Tujuan,UP,JabatanUP,[status], UserName,[Close],QuotationClose_No ) values (" & _
                   " '" & lblQoNo.Text.ToString & "','" & Changedate(tb_tgl.Text.ToString, 2) & "', " & _
                   " '" & hfCID.Value & "','" & TxtPenerima.Text.ToString & "', " & _
                   " '" & DDLKotaTujuan.SelectedValue.ToString & "', '" & TxtUp.Text.ToString & "' , '" & TxtJabatan.Text.ToString & "' ," & _
                   " 1 , '" & Session("UserID").ToString & "','True','" & QoNo.ToString & "');"


            For i As Integer = 0 To Grid_SO.VisibleRowCount - 1
                If Grid_SO.GetRowValues(i, "IDS").ToString = "" Then
                    iddetail = iddetail + 1
                    With Grid_SO
                        sqlstring &= " insert into QuotationDetail(Quotation_No,IDDetail,Customer_Id,SatuanID,Nama_Barang, " & _
                            " Harga,[status], UserName,QuotationClose_No, Keterangan ) values (" & _
                            " '" & lblQoNo.Text.ToString & "' , " & iddetail & ",'" & hfCID.Value & "', '" & .GetRowValues(i, "SatuanIDS") & "'," & _
                            " '" & .GetRowValues(i, "NamaBarangS") & "', " & _
                            " '" & .GetRowValues(i, "HargaS").ToString & "' , 1 , '" & Session("UserID").ToString & "','', '" & .GetRowValues(i, "KeteranganS").ToString & "' );"
                    End With
                Else
                    With Grid_SO
                        ID = .GetRowValues(i, "IDS")
                        sqlstring &= " insert into QuotationDetail(Quotation_No,IDDetail,Customer_Id,SatuanID,Nama_Barang, " & _
                            " Harga,[status], UserName,QuotationClose_No, Keterangan ) values (" & _
                            " '" & lblQoNo.Text.ToString & "' , " & ID & ",'" & hfCID.Value & "', '" & .GetRowValues(i, "SatuanIDS") & "'," & _
                            " '" & .GetRowValues(i, "NamaBarangS") & "', " & _
                            " '" & .GetRowValues(i, "HargaS").ToString & "' , 1 , '" & Session("UserID").ToString & "','', '" & .GetRowValues(i, "KeteranganS").ToString & "' );"
                    End With
                End If
            Next


            sqlstring &= hfDel.Value
            result = SQLExecuteNonQuery(sqlstring)
            If result <> 0 Then
                Session("Grid_SO") = Nothing
                Session("Grid_SO_Parent") = Nothing
                cekGudang(lblQoNo.Text, HfSo.Value)
                Add_Clear()
                Clear()
                LblinfoS.Visible = True
                LblinfoS.Text = " Update Quotation success "
                load_grid_parent()
                hfMode.Value = "Insert"
            End If
        Catch ex As Exception
            Response.Write(" Error Function Update <BR> " & ex.ToString)
        End Try
    End Sub
    Private Sub cekGudang(ByVal QoNo As String, ByVal QoNoR As String)
        Dim sqlstring As String
        Dim rst As String = ""
        Try
            sqlstring = ""
            sqlstring = " UPDATE WarehouseHeader" & _
                        " SET " & _
                        " Quotation_No = '" & QoNo.ToString & "' ,  " & _
                        " LastModified = '" & Now.ToString & "' " & _
                        " WHERE Quotation_No = '" & QoNoR.ToString & "' and [status] <> 0; "
            sqlstring &= " UPDATE ContainerHeader" & _
                        " SET " & _
                        " QuotationNo = '" & QoNo.ToString & "' ,  " & _
                        " LastModified = '" & Now.ToString & "' " & _
                        " WHERE QuotationNo = '" & QoNoR.ToString & "' and [status] <> 0; "

            rst = SQLExecuteNonQuery(sqlstring)



        Catch ex As Exception
            Throw New Exception("Error cekClose function :" & ex.ToString)
        End Try

    End Sub
    'Private Sub Close(ByVal QoNo As String)
    '    Dim result As Integer
    '    Try
    '        sqlstring = " UPDATE MasterQuotation " & _
    '                    " SET " & _
    '                    " LastModified = '" & Now.ToString & "', " & _
    '                    " [status] = 5 " & _
    '                    " WHERE Quotation_No = '" & QoNo.ToString & "' And [status] = 1 or [status] = 10; "

    '        sqlstring &= " UPDATE QuotationDetail " & _
    '                     " SET " & _
    '                     " LastModified = '" & Now.ToString & "', " & _
    '                     " [status] = 5 " & _
    '                     " WHERE Quotation_No = '" & QoNo.ToString & "' And [status] = 1 or [status] = 10; "

    '        result = SQLExecuteNonQuery(sqlstring)

    '    Catch ex As Exception
    '        Response.Write("Error Function Delete <BR> " & ex.ToString)
    '    End Try
    'End Sub

    Private Sub Delete(ByVal QoNo As String)
        Dim result As Integer
        Try
            sqlstring = " UPDATE MasterQuotation " & _
                        " SET " & _
                        " LastModified = '" & Now.ToString & "', " & _
                        " [status] = 0 " & _
                        " WHERE Quotation_No = '" & QoNo.ToString & "' And [status] = 1; "

            sqlstring &= " UPDATE QuotationDetail " & _
                         " SET " & _
                         " LastModified = '" & Now.ToString & "', " & _
                         " [status] = 0 " & _
                         " WHERE Quotation_No = '" & QoNo.ToString & "' And [status] = 1; "

            result = SQLExecuteNonQuery(sqlstring)
            If result > 0 Then
                Add_Clear()
                Clear()
                LblinfoS.Visible = True
                LblinfoS.Text = " Delete success "
                load_grid_parent()
            End If
        Catch ex As Exception
            Response.Write("Error Function Delete <BR> " & ex.ToString)
        End Try
    End Sub

    Private Sub Remove_item(ByVal JenisBarang As String)
        Dim DT As DataTable
        Try
            DT = CType(Session("Grid_SO"), DataTable)
            For i As Integer = 0 To DT.Rows.Count - 1
                If DT.Rows(i).Item("NamaBarangS").ToString = JenisBarang.ToString Then
                    DT.Rows(i).Delete()
                    Exit For
                End If
            Next
            create_session()
            Session("Grid_SO") = DT
            Grid_SO.DataSource = DT
            Grid_SO.DataBind()

            Call Refresh_Grid()
            Call Add_Clear()
        Catch ex As Exception
            Response.Write("Error Remove Item : <BR> " & ex.ToString)
        End Try

    End Sub

    Private Sub Remove_Item_When_Update_Quotation(ByVal VisibleIndex As Integer)
        Try
            DT = CType(Session("Grid_SO"), DataTable)

            DT.Rows.RemoveAt(VisibleIndex)
            Session("Grid_SO") = DT
            Grid_SO.DataSource = DT
            Grid_SO.DataBind()

            clear_label()
            Call Add_Clear()
        Catch ex As Exception
            Throw New Exception(ex.ToString)
        End Try
    End Sub

    Private Sub Remove_itemDB(ByVal ID As String, ByVal QoNo As String)

        Try
            Dim str As String = ""
            Dim CDT As DataTable

            str = "select IDDetail FROM V_Warehouse_Satuan where Quotation_No = '" & QoNo & "' " & _
                    "and QuotationDetailID = " & ID & " "
            CDT = SQLExecuteQuery(str).Tables(0)

            If CDT.Rows.Count = 0 Then
                sqlstring = hfDel.Value
                sqlstring &= " UPDATE QuotationDetail " & _
                             " SET " & _
                             " LastModified = '" & Now.ToString & "', " & _
                             " [status] = 0 " & _
                             " WHERE IDDetail = '" & ID.ToString & "' and Quotation_No = '" & QoNo.ToString & "'  and status <>0; "

                hfDel.Value = sqlstring

                DT = CType(Session("Grid_SO"), DataTable)
                For i As Integer = 0 To DT.Rows.Count - 1
                    If DT.Rows(i).Item("IDS").ToString = ID.ToString Then
                        DT.Rows(i).Delete()
                        Exit For
                    End If
                Next
                create_session()
                Session("Grid_SO") = DT
                Grid_SO.DataSource = DT
                Grid_SO.DataBind()

                Call Refresh_Grid()
                Call Add_Clear()
            Else
                lblinfo.Visible = True
                lblinfo.Text = "Data sudah terpakai, tidak dapat di delete"
            End If



        Catch ex As Exception
            Throw New Exception(ex.ToString)
        End Try


    End Sub

    Private Sub Edit_Item()
        If hfMode.Value = "Insert" Then
            Edit_Item(hfID.Value.ToString)
        ElseIf hfMode.Value = "Update" Then
            Edit_Item(hfID.Value.ToString)
        Else
            Edit_Item(hfID.Value.ToString)
        End If
    End Sub

    Private Sub Add_Item()
        If hfMode.Value = "Insert" Then
            If Validation() Then
                Dim DT As DataTable
                Dim DR As DataRow
                DT = CType(Session("Grid_SO"), DataTable)
                For i As Integer = 0 To DT.Rows.Count - 1
                    If DT.Rows(i).Item("NamaBarangS").ToString = TxtItem.Text.Trim And DT.Rows(i).Item("HargaS").ToString = ReplaceString(TxtHargaItem.Text) And DT.Rows(i).Item("SatuanIDS").ToString = DDLSatuan.SelectedValue.ToString Then
                        Add_Clear()
                        lblinfo.Visible = True
                        lblinfo.Text = "Data sudah di Add"
                        Exit Sub
                    End If
                Next
                DR = DT.NewRow
                DR("Quotation_NoS") = lblQoNo.Text
                DR("NamaBarangS") = TxtItem.Text.Trim.Replace("'", "`")
                DR("SatuanIDS") = DDLSatuan.SelectedValue
                DR("NamaHargaS") = DDLSatuan.SelectedItem.Text
                DR("HargaS") = ReplaceString(TxtHargaItem.Text)
                DR("KeteranganS") = TxtKeterangan.Text.Trim.ToString.Replace("'", "''")
                DT.Rows.Add(DR)

                Session("Grid_SO") = DT
                Grid_SO.DataSource = DT
                Grid_SO.DataBind()

                'Call Refresh_Grid()
                Call Add_Clear()
            End If
        ElseIf hfMode.Value = "Update" Then
            Dim DT As DataTable
            Dim DR As DataRow
            DT = CType(Session("Grid_SO"), DataTable)
            For i As Integer = 0 To DT.Rows.Count - 1
                If DT.Rows(i).Item("NamaBarangS").ToString = TxtItem.Text.Trim And DT.Rows(i).Item("HargaS").ToString = ReplaceString(TxtHargaItem.Text) And DT.Rows(i).Item("SatuanIDS").ToString = DDLSatuan.SelectedValue.ToString Then
                    Add_Clear()
                    lblinfo.Visible = True
                    lblinfo.Text = "Data sudah di add"
                    Exit Sub
                End If
            Next
            DR = DT.NewRow
            DR("IDS") = ""
            DR("Quotation_NoS") = lblQoNo.Text
            DR("NamaBarangS") = TxtItem.Text.Trim.Replace("'", "''")
            DR("SatuanIDS") = DDLSatuan.SelectedValue
            DR("NamaHargaS") = DDLSatuan.SelectedItem.Text
            DR("HargaS") = ReplaceString(TxtHargaItem.Text)
            DR("KeteranganS") = TxtKeterangan.Text.Trim.ToString.Replace("'", "''")
            DT.Rows.Add(DR)

            Session("Grid_SO") = DT
            Grid_SO.DataSource = DT
            Grid_SO.DataBind()

            'Call Refresh_Grid()
            Call Add_Clear()
        Else
            Dim DT As DataTable
            Dim DR As DataRow
            DT = CType(Session("Grid_SO"), DataTable)
            For i As Integer = 0 To DT.Rows.Count - 1
                If DT.Rows(i).Item("NamaBarangS").ToString = TxtItem.Text.Trim Then
                    Add_Clear()
                    lblinfo.Visible = True
                    lblinfo.Text = "data sudah di add"
                    Exit Sub
                End If
            Next
            DR = DT.NewRow
            DR("IDS") = ""
            DR("Quotation_NoS") = lblQoNo.Text
            DR("NamaBarangS") = TxtItem.Text.Trim.Replace("'", "''")
            DR("SatuanIDS") = DDLSatuan.SelectedValue
            DR("NamaHargaS") = DDLSatuan.SelectedItem.Text
            DR("HargaS") = ReplaceString(TxtHargaItem.Text)
            DR("KeteranganS") = TxtKeterangan.Text.Trim.ToString.Replace("'", "''")
            DT.Rows.Add(DR)

            Session("Grid_SO") = DT
            Grid_SO.DataSource = DT
            Grid_SO.DataBind()

            'Call Refresh_Grid()
            Call Add_Clear()

        End If
    End Sub

    Private Sub Edit_Item(ByVal JenisBarang As String)
        Dim DT As DataTable
        Try
            DT = CType(Session("Grid_SO"), DataTable)
            For i As Integer = 0 To DT.Rows.Count - 1
                If DT.Rows(i).Item("IDS").ToString = JenisBarang.ToString Then

                    DT.Rows(i).Item("Quotation_NoS") = lblQoNo.Text
                    DT.Rows(i).Item("NamaBarangS") = TxtItem.Text.Trim.Replace("'", "''")
                    DT.Rows(i).Item("SatuanIDS") = DDLSatuan.SelectedValue
                    DT.Rows(i).Item("NamaHargaS") = DDLSatuan.SelectedItem.Text
                    DT.Rows(i).Item("HargaS") = ReplaceString(TxtHargaItem.Text)
                    DT.Rows(i).Item("KeteranganS") = TxtKeterangan.Text.Trim.Replace("'", "''")
                    Exit For
                End If
            Next
            Session("Grid_SO") = Nothing
            Session("Grid_SO") = DT
            Grid_SO.DataSource = DT
            Grid_SO.DataBind()


            Call Add_Clear()
            hfID.Value = ""
            hfModeEdit.Value = "Insert"
        Catch ex As Exception
            Response.Write("Error Edit Item : <BR> " & ex.ToString)
        End Try
    End Sub

    'Private Sub Edit_ItemU(ByVal JenisBarang As String)
    '    Try

    '        sqlstring &= " UPDATE QuotationDetail" & _
    '             " SET " & _
    '             " Customer_Id = '" & hfCID.Value & "', " & _
    '             " Nama_Barang = '" & TxtItem.Text.ToString & "' ," & _
    '             " Harga = '" & TxtHargaItem.Text.ToString & "' ," & _
    '             " LastModified = '" & Now.ToString & "', " & _
    '             " [status] = 1 " & _
    '             " WHERE Quotation_No = '" & lblQoNo.Text.ToString & "' AND IDDetail = '" & hfID.Value & "' ; "

    '        result = SQLExecuteNonQuery(sqlstring)
    '        load_grid(lblQoNo.Text.ToString)
    '        Add_Clear()

    '        hfModeEdit.Value = "Insert"
    '    Catch ex As Exception
    '        Response.Write("Error Edit Item : <BR> " & ex.ToString)
    '    End Try
    'End Sub

    'Private Sub Insert_Close(ByVal QoNo As String)
    '    Dim ID As String
    '    Dim iddetail As Integer = 0
    '    Dim cekstring As String
    '    Dim cekdt As DataTable
    '    Dim quoPP As String
    '    Dim quoPPDT As DataTable
    '    Dim sqlpp As String = ""
    '    Dim kim As Boolean = True
    '    Try
    '        sqlstring = hfDel.Value
    '        sqlstring &= " UPDATE MasterQuotation" & _
    '                    " SET " & _
    '                    " Tanggal = '" & tb_tgl.Text.ToString & "', " & _
    '                    " Customer_Id = '" & hfCID.Value & "'," & _
    '                    " Tujuan = '" & TxtTujuan.Text.ToString & "', " & _
    '                    " Penerima = '" & TxtPenerima.Text.ToString & "'," & _
    '                    " UP = '" & TxtUp.Text.ToString & "'," & _
    '                    " JabatanUP = '" & TxtJabatan.Text.ToString & "'," & _
    '                    " Quotation_No = '" & QoNo.ToString & "', " & _
    '                    " LastModified = '" & Now.ToString & "', " & _
    '                    " [Close] = 'Use', " & _
    '                    " [status] = 1 " & _
    '                    " WHERE Quotation_No = '" & hfQuotationClose.Value & "' and status = 5;"

    '        cekstring = "select id from QuotationDetail where quotation_no = '" & hfQuotationClose.Value & "' and status =5"
    '        cekdt = SQLExecuteQuery(cekstring).Tables(0)

    '        If cekdt.Rows.Count > 0 Then
    '            iddetail = getDetailID("QuotationDetail", "Quotation_No", hfQuotationClose.Value)
    '        Else
    '            iddetail = 0
    '        End If
    '        quoPP = "select IDDetail,Quotation_No from QuotationDetail where Quotation_No = '" & hfQuotationClose.Value & "' and status = 5 "
    '        quoPPDT = SQLExecuteQuery(quoPP, False).Tables(0)
    '        For i As Integer = 0 To Grid_SO.VisibleRowCount - 1
    '            kim = True
    '            With Grid_SO
    '                ID = .GetRowValues(i, "IDS")
    '                For e As Integer = 0 To quoPPDT.Rows.Count - 1
    '                    If ID = quoPPDT.Rows(e).Item("IDDetail").ToString And hfQuotationClose.Value = quoPPDT.Rows(e).Item("Quotation_No").ToString Then
    '                        sqlstring &= " UPDATE QuotationDetail" & _
    '                                               " SET " & _
    '                                               " Customer_Id = '" & hfCID.Value & "', " & _
    '                                               " Nama_Barang = '" & .GetRowValues(i, "NamaBarangS") & "' ," & _
    '                                               " SatuanID = '" & .GetRowValues(i, "SatuanIDS") & "', " & _
    '                                               " Harga = '" & .GetRowValues(i, "HargaS").ToString & "' ," & _
    '                                               " Quotation_No = '" & QoNo.ToString & "' ," & _
    '                                               " LastModified = '" & Now.ToString & "', " & _
    '                                               " [status] = 1 " & _
    '                                               " WHERE IDDetail = '" & .GetRowValues(i, "IDS") & "' and Quotation_No = '" & hfQuotationClose.Value & "' and  status = 5; "
    '                        kim = False
    '                        Exit For

    '                    End If
    '                Next
    '                If kim = True Then
    '                    sqlstring &= " UPDATE QuotationDetail" & _
    '                                              " SET " & _
    '                                              " Customer_Id = '" & hfCID.Value & "', " & _
    '                                              " Nama_Barang = '" & .GetRowValues(i, "NamaBarangS") & "' ," & _
    '                                              " SatuanID = '" & .GetRowValues(i, "SatuanIDS") & "', " & _
    '                                              " Harga = '" & .GetRowValues(i, "HargaS").ToString & "' ," & _
    '                                              " Quotation_No = '" & QoNo.ToString & "' ," & _
    '                                              " LastModified = '" & Now.ToString & "', " & _
    '                                              " [status] = 1 " & _
    '                                              " WHERE IDDetail = '" & .GetRowValues(i, "IDS") & "' and Quotation_No = '" & hfQuotationClose.Value & "' and  status = 5; "
    '                    sqlpp &= " insert into QuotationDetail(Quotation_No,IDDetail,Customer_Id,Nama_Barang,SatuanID, " & _
    '                                             " Harga,[status], UserName ) values (" & _
    '                                             " '" & lblQoNo.Text.ToString & "' ," & ID & ", '" & hfCID.Value & "', " & _
    '                                             " '" & .GetRowValues(i, "NamaBarangS") & "', '" & .GetRowValues(i, "SatuanIDS") & "', " & _
    '                                             " '" & .GetRowValues(i, "HargaS").ToString & "' , 1 , '" & Session("UserID").ToString & "' );"
    '                End If

    '            End With
    '        Next
    '        If sqlstring <> "" Then
    '            result = SQLExecuteNonQuery(sqlstring)
    '        End If
    '        If sqlpp <> "" Then
    '            result = SQLExecuteNonQuery(sqlpp, True, False)
    '        End If
    '        If result > 0 Then
    '            Add_Clear()
    '            Clear()
    '            lblinfo.Visible = True
    '            lblinfo.Text = " Update success "
    '            load_grid_parent()
    '            PanelApprove.Visible = True
    '            Panel_Close.Visible = False
    '            hfMode.Value = "Insert"
    '        End If
    '    Catch ex As Exception
    '        Response.Write(" Error Function Update <BR> " & ex.ToString)
    '    End Try

    'End Sub
    Private Function CloseUse(ByVal QoNo As String) As Boolean
        Dim result As Integer
        Try
            sqlstring = " UPDATE MasterQuotation " & _
                        " SET " & _
                        " LastModified = '" & Now.ToString & "', " & _
                        " [status] = 2 " & _
                        " WHERE Quotation_No = '" & QoNo.ToString & "' And [status] = 5 ; "

            sqlstring &= " UPDATE QuotationDetail " & _
                         " SET " & _
                         " LastModified = '" & Now.ToString & "', " & _
                         " [status] = 2 " & _
                         " WHERE Quotation_No = '" & QoNo.ToString & "' And [status] = 5 ; "

            result = SQLExecuteNonQuery(sqlstring)
            If result > 0 Then
                Return True
            End If
        Catch ex As Exception
            Response.Write("Error Function CloseUse <BR> " & ex.ToString)
        End Try
        Return False
    End Function


    Private Sub Clear()

        Try
            tb_tgl.Date = Today
            TxtUp.Text = ""
            TxtJabatan.Text = ""
            TxtCustCode.Text = ""
            DDLKotaTujuan.SelectedIndex = 0
            TxtPenerima.Text = ""
            hfCID.Value = ""
            hfClose.Value = ""
            hfStatusClose.Value = "False"
            TxtCustName.Text = ""
            Session("iddetail") = Nothing
            lblinfo.Text = ""
            DDLQuotation.SelectedValue = "0"
            hfStatusClose.Value = ""
            hfDel.Value = ""
            TxtKeterangan.Text = ""
            hfNamaCustomer.Value = ""
            hfCodeCustomer.Value = ""
            '   TxtPembayaranOleh.Text = ""
            create_session()
        Catch ex As Exception
            Response.Write(ex.ToString)
        End Try

    End Sub

    Private Sub Refresh_Grid()

        '#
        Dim DT As DataTable
        Try
            DT = CType(Session("Grid_SO"), DataTable)
            'For i As Integer = 0 To DT.Rows.Count - 1
            '    DT.DefaultView(i).Row("IDS") = (i + 1).ToString
            'Next
            Session("Grid_SO") = DT
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
            hfItem.Value = ""
            lblinfo.Visible = False
            LblinfoS.Visible = False
            TxtKeterangan.Text = ""
            DDLSatuan.Enabled = True

        Catch ex As Exception
            Response.Write(ex.ToString)
        End Try

    End Sub

    Private Sub clear_label()
        lblinfo.Visible = False
        LblinfoS.Visible = False
        lblinfo.Text = ""
        LblinfoS.Text = ""
    End Sub

    Private Function GetRole(ByVal nama As String) As String
        Try
            Dim namaroles As String = ""


            STR = "select R.RoleID from Roles R " & _
                    "LEFT JOIN MasterUser MU on R.RoleID = MU.RoleID " & _
                    "WHERE r.[status] <> 0 " & _
                    "AND MU.[Status] <> 0 " & _
                    "AND MU.UserID = '" & nama & "' "
            namaroles = SQLExecuteScalar(STR)

            Return namaroles
        Catch ex As Exception
            Throw New Exception(ex.ToString)
        End Try
    End Function

    Private Sub PanelButton()
        Try
            Dim RolesID As String = GetRole(Session("UserId").ToString)

            If RolesID = "RL001" Or RolesID = "RL005" Or RolesID = "RL004" Then
                pnlbutton.Visible = True
            Else
                pnlbutton.Visible = False
            End If

        Catch ex As Exception
            Throw New Exception(ex.ToString)
        End Try
    End Sub

    Private Function Cek_Quotation_Gudang(ByVal NoQuotation As String, ByVal IDQuotationDetail As String) As Boolean
        Try
            Dim HslDT As New DataTable

            sqlstring = "select WD.ID from MasterQuotation MQ " & _
                        "JOIN WarehouseHeader WH ON MQ.Quotation_No = WH.Quotation_No " & _
                        "JOIN WarehouseDetail WD ON WH.WarehouseItem_Code = WD.WarehouseItem_Code " & _
                        "WHERE MQ.Quotation_No = '" & NoQuotation & "' " & _
                        "and WD.QuotationDetailID = '" & IDQuotationDetail & "' " & _
                        "and WD.[status] <> 0 " & _
                        "and WH.[status] <> 0 "
            HslDT = SQLExecuteQuery(sqlstring).Tables(0)

            If HslDT.Rows.Count > 0 Then
                Return False
            Else
                Return True
            End If

        Catch ex As Exception
            Throw New Exception(ex.ToString)
        End Try
    End Function
#End Region

#Region "VALIDATION"
    Private Function Validation_Update() As Boolean
        Try

            If hfItem.Value = TxtItem.Text.ToString Then
                lblinfo.Visible = True
                lblinfo.Text = "Anda tidak bisa merubah nama barang , untuk merubah nama barang ini harap delete dan tambahkan data baru yang benar ."
                Return False
            End If

            If DDLSatuan.SelectedIndex = 0 Then
                lblinfo.Visible = True
                lblinfo.Text = "Jenis satuan Harus diisi"
                Return False
            End If

            If TxtItem.Text.ToString = "" Then
                lblinfo.Visible = True
                lblinfo.Text = "Item Harus DiIsi"
                TxtItem.Focus()
                Return False
            End If


            If TxtHargaItem.Text.ToString = "" Then
                lblinfo.Visible = True
                lblinfo.Text = "Anda Harus Mengisi Harga Barang!"
                TxtItem.Focus()
                Return False
            End If
            Return True
        Catch ex As Exception
            Response.Write("Validation Exception : <br>" & ex.ToString)
        End Try
    End Function
    Private Function Validation() As Boolean
        Try
            If DDLSatuan.SelectedIndex = 0 Then
                lblinfo.Visible = True
                lblinfo.Text = "Jenis satuan Harus diisi"
                Return False
            End If

            If TxtItem.Text.ToString = "" Then
                lblinfo.Visible = True
                lblinfo.Text = "jenis Barang harus diisi"
                TxtItem.Focus()
                Return False
            End If


            If TxtHargaItem.Text.ToString = "" Then
                lblinfo.Visible = True
                lblinfo.Text = "Harga barang harus diisi"
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
            If tb_tgl.Text = "" Then
                lblinfo.Visible = True
                lblinfo.Text = "Tanggal Harus Dipilih"
                tb_tgl.Focus()
                Return False
            End If

            If Grid_SO.VisibleRowCount = 0 Then
                lblinfo.Visible = True
                lblinfo.Text = "Masukan Item, minimal 1"
                Return False
            End If

            If TxtCustCode.Text.Trim = "" Then
                lblinfo.Visible = True
                lblinfo.Text = "Pilih Customer"
                TxtCustCode.Focus()
                Return False
            End If

            If DDLKotaTujuan.SelectedIndex = 0 Then
                lblinfo.Visible = True
                lblinfo.Text = "Pilih Kota Tujuan"

                Return False
            End If

            'If TxtCustName.Text.Trim = "" Then
            '    lblinfo.Visible = True
            '    lblinfo.Text = "Masukan nama customer"
            '    TxtCustName.Focus()
            '    Return False
            'End If

            'If TxtPembayaranOleh.Text.Trim = "" Then
            '    lblinfo.Visible = True
            '    lblinfo.Text = "Masukan nama Pembayar"
            '    TxtPembayaranOleh.Focus()
            '    Return False
            'End If

            'If DDLKotaPembayar.SelectedIndex = 0 Then
            '    lblinfo.Visible = True
            '    lblinfo.Text = "Pilih Kota Pembayar"

            '    Return False
            'End If

            Return True
        Catch ex As Exception
            Response.Write("Validation Exception : <br>" & ex.ToString)
        End Try
    End Function

    Private Function validationInsert() As Boolean
        Try

            Dim hasil As String

            sqlstring = "SELECT ID FROM MasterQuotation " & _
                        "WHERE Customer_Id = '" & hfCID.Value & "' " & _
                        "AND Tujuan = '" & DDLKotaTujuan.SelectedValue & "' " & _
                        "AND Penerima = '" & TxtPenerima.Text.Trim.Replace("'", "''") & "' " & _
                        "AND [status] <> 0 "
            hasil = SQLExecuteScalar(sqlstring)

            If hasil <> "" Then
                lblinfo.Text = "Data dengan nama Pengirim :<b> " & " " & TxtCustName.Text & " " & "</b> Sudah ada"
                lblinfo.Visible = True
                Return False
            End If

            Return True
        Catch ex As Exception
            Throw New Exception("<b>Error ValidationInsert :</b>" & ex.ToString)
        End Try
    End Function

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

    Private Sub LoadDDLKota()
        Try
            sqlstring = " SELECT DISTINCT" & _
                          "		ID, " & _
                          "	Tujuan, status " & _
                          "	FROM MasterTujuan " & _
                          " WHERE status = 1 ORDER BY Tujuan "
            DS = SQLExecuteQuery(sqlstring)
            DT = DS.Tables(0)

            'DDLKotaPembayar.Items.Clear()
            DDLKotaTujuan.Items.Clear()

            'With DDLKotaPembayar
            '    .DataSource = DT
            '    .DataTextField = "Tujuan"
            '    .DataValueField = "Tujuan"
            '    .DataBind()
            'End With

            'DDLKotaPembayar.Items.Insert(0, "Pilih Kota")

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

    Private Sub DDLQuotation_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DDLQuotation.SelectedIndexChanged
        If DDLQuotation.SelectedValue = "0" Then
            PanelApprove.Visible = True
            Panel_Close.Visible = False
            Call load_grid_parent()
        ElseIf DDLQuotation.SelectedValue = "1" Then
            PanelApprove.Visible = False
            Panel_Close.Visible = True
            Call load_grid_parent_close()

        End If
    End Sub

    'Private Sub DDLSatuan_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DDLSatuan.SelectedIndexChanged
    '    Dim namacontainer As String
    '    If DDLSatuan.SelectedIndex = 0 Then
    '        TxtItem.Text = ""
    '        hfOID.Value = ""
    '        TxtHargaItem.Text = ""
    '        Exit Sub
    '    End If

    '    load_harga()
    '    If DDLSatuan.SelectedItem.Text = "Kubik" Then
    '        TxtItem.Text = ""
    '        hfOID.Value = ""
    '    ElseIf DDLSatuan.SelectedItem.Text = "Ton" Then
    '        TxtItem.Text = ""
    '        hfOID.Value = ""
    '    ElseIf DDLSatuan.SelectedItem.Text = "Unit" Then
    '        TxtItem.Text = ""
    '        hfOID.Value = ""
    '    ElseIf DDLSatuan.SelectedItem.Text = "Satuan" Then
    '        hfOID.Value = ""
    '        TxtItem.Text = ""
    '    Else
    '        namacontainer = loadnama()
    '        hfOID.Value = ""
    '        TxtItem.Text = namacontainer
    '    End If
    'End Sub

#End Region


End Class
