Imports System.Data
Imports System.Data.SqlClient

Partial Class dialog2
    Inherits System.Web.UI.Page
    Private DS As DataSet
    Private DT As DataTable
    Private sqlString As String
    Private sDR As SqlDataReader
    Private con As SqlConnection

#Region " Page "

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try

            If Not Page.IsPostBack Then
                Response.Cache.SetCacheability(HttpCacheability.NoCache)

                Select Case Request("Mode").ToString

                    Case "Mutasi"
                        Call Load_Grid_mutasi(Request("gudang_code").ToString)
                    Case "View"
                        Call Load_Grid_View(Request("transfer_no").ToString)
                    Case "Customer"
                        Call Load_Grid_Customer()
                    Case "Sales"
                        Call Load_Grid_Sales()
                    Case "Packet"
                        Call Load_Grid_Packet()
                    Case "MasterCustomer"
                        Call Load_Grid_Master_Customer()
                    Case "MasterCustomerW"
                        Call Load_Grid_Master_CustomerW()
                    Case "Kapal"
                        Call load_grid_kapal()
                    Case "Muat"
                        Call load_muat_barang()
                    Case "Penerima"
                        Call load_penerima()
                    Case "CustItem"
                        Call load_cust_item()
                    Case "Satuan"
                        Call load_satuan()
                    Case "Tujuan"
                        Call load_tujuan()
                    Case "Kondisi"
                        Call load_kondisi()
                    Case "CustomerInvoice"
                        Call Load_Cust_Invoice()
                    Case "CustomerInvoiceDS"
                        Call Load_Cust_InvoiceDS()

                End Select
            End If

            If Not Session("grid_Dialog_" & Request("Mode").ToString) Is Nothing Then
                grid_Dialog.DataSource = CType(Session("grid_Dialog_" & Request("Mode").ToString), DataTable)
                grid_Dialog.DataBind()
            End If

        Catch ex As Exception
            lError.Text = "Page_Load Exception :<br>" & ex.ToString
        End Try

    End Sub

#End Region

#Region " GridView "

    Private Sub Create_Grid_Session()
        Dim iDT As New DataTable
        Session("grid_Dialog_" & Request("Mode").ToString) = iDT
    End Sub


    Private Sub Load_Grid_mutasi(ByVal gudang_code As String)

        '#
        Dim STR As String = ""

        Try

            Call Create_Grid_Session()

            '-» [ Filter by Department tidak Jadi¬
            'If Not Request("filter") Is Nothing Then
            '	If Not Session("Department") Is Nothing Then
            '		STR = "	WHERE UPPER(Department_Code) = '" & Session("Department").ToString & "'"
            '	Else
            '		'-: [ Renew Session ]
            '	End If
            'End If

            sqlString = " SELECT " & _
                  "		item_code CODE, " & _
                  "	uom ""NAME"",qty ""QTY"" " & _
                  "	FROM tbl_warehouse where status = 1 and gudang_code = '" & gudang_code.ToString & "' " & _
                  STR & _
                  " ORDER BY item_code "
            DS = SQLExecuteQuery(sqlString)
            DT = DS.Tables(0)
            Session("grid_Dialog_" & Request("Mode").ToString) = DT
            grid_Dialog.DataSource = DT
            grid_Dialog.DataBind()
            grid_Dialog.Columns("NAME").Caption = "Pack"
        Catch ex As Exception
            lError.Text = "Load_Grid_Packet Exception :<br>" & ex.ToString
        End Try

    End Sub


    Private Sub Load_Grid_View(ByVal transfer_no As String)

        '#
        Dim STR As String = ""

        Try

            Call Create_Grid_Session()

            '-» [ Filter by Department tidak Jadi¬
            'If Not Request("filter") Is Nothing Then
            '	If Not Session("Department") Is Nothing Then
            '		STR = "	WHERE UPPER(Department_Code) = '" & Session("Department").ToString & "'"
            '	Else
            '		'-: [ Renew Session ]
            '	End If
            'End If

            sqlString = " SELECT " & _
                  "		d.item_code CODE, " & _
                  "	d.unit ""NAME"",d.qty ""QTY"" " & _
                  "	FROM transfer_item t,transfer_item_detail d where t.transfer_no = '" & transfer_no.ToString & "' and t.id = d.idtransfer_item " & _
                  STR & _
                  " ORDER BY d.item_code "
            DS = SQLExecuteQuery(sqlString)
            DT = DS.Tables(0)
            Session("grid_Dialog_" & Request("Mode").ToString) = DT
            grid_Dialog.DataSource = DT
            grid_Dialog.DataBind()
            grid_Dialog.Columns("NAME").Caption = "Pack"
        Catch ex As Exception
            lError.Text = "Load_Grid_Packet Exception :<br>" & ex.ToString
        End Try

    End Sub


    Private Sub Load_Grid_Customer()

        '#
        Dim STR As String = ""

        Try

            Call Create_Grid_Session()

            '-» [ Filter by Department tidak Jadi¬
            'If Not Request("filter") Is Nothing Then
            '	If Not Session("Department") Is Nothing Then
            '		STR = "	WHERE UPPER(Department_Code) = '" & Session("Department").ToString & "'"
            '	Else
            '		'-: [ Renew Session ]
            '	End If
            'End If

            sqlString = " SELECT " & _
                  "		customer_id CODE, " & _
                  "	customer_name ""NAME"",sex ""QTY"" " & _
                  "	FROM customer " & _
                  STR & _
                  " ORDER BY customer_name "
            DS = SQLExecuteQuery(sqlString)
            DT = DS.Tables(0)
            Session("grid_Dialog_" & Request("Mode").ToString) = DT
            grid_Dialog.DataSource = DT
            grid_Dialog.DataBind()
            grid_Dialog.Columns("QTY").Caption = "Sex"
        Catch ex As Exception
            lError.Text = "Load_Grid_Packet Exception :<br>" & ex.ToString
        End Try

    End Sub


    Private Sub load_satuan()

        '#
        Dim STR As String = ""
        Try

            Call Create_Grid_Session()
            sqlString = " SELECT DISTINCT" & _
                  "		IDDetail CODE, " & _
                  "	Nama_Satuan ""NAME"" , status ""QTY"" " & _
                  "	FROM MasterSatuanOther  " & _
                  " WHERE status = 1 ORDER BY Nama_Satuan "
            DS = SQLExecuteQuery(sqlString)
            DT = DS.Tables(0)
            Session("grid_Dialog_" & Request("Mode").ToString) = DT
            grid_Dialog.DataSource = DT
            grid_Dialog.DataBind()
            grid_Dialog.Columns("QTY").Visible = False
        Catch ex As Exception
            lError.Text = "Load_Grid_Packet Exception :<br>" & ex.ToString
        End Try

    End Sub

    Private Sub load_tujuan()

        '#
        Dim STR As String = ""
        Try

            Call Create_Grid_Session()
            sqlString = " SELECT DISTINCT" & _
                  "		ID CODE, " & _
                  "	Tujuan ""NAME"" , status ""QTY"" " & _
                  "	FROM MasterTujuan " & _
                  " WHERE status = 1 ORDER BY Tujuan "
            DS = SQLExecuteQuery(sqlString)
            DT = DS.Tables(0)
            Session("grid_Dialog_" & Request("Mode").ToString) = DT
            grid_Dialog.DataSource = DT
            grid_Dialog.DataBind()
            grid_Dialog.Columns("QTY").Visible = False
        Catch ex As Exception
            lError.Text = "Load_Grid_Packet Exception :<br>" & ex.ToString
        End Try

    End Sub

    Private Sub load_kondisi()

        '#
        Dim STR As String = ""
        Try

            Call Create_Grid_Session()
            sqlString = " SELECT " & _
                  "		ID CODE, " & _
                  "	Nama_Kondisi ""NAME"" , status ""QTY"" " & _
                  "	FROM KondisiPengiriman " & _
                  " WHERE status = 1 ORDER BY Nama_Kondisi "
            DS = SQLExecuteQuery(sqlString)
            DT = DS.Tables(0)
            Session("grid_Dialog_" & Request("Mode").ToString) = DT
            grid_Dialog.DataSource = DT
            grid_Dialog.DataBind()
            grid_Dialog.Columns("QTY").Visible = False
        Catch ex As Exception
            lError.Text = "Load_Grid_Packet Exception :<br>" & ex.ToString
        End Try

    End Sub

    Private Sub load_cust_item()

        '#
        Dim STR As String = ""
        Dim kapal As String = ""
        Try

            Call Create_Grid_Session()
            kapal = Request("customer").ToString
            sqlString = " SELECT DISTINCT" & _
                  "		mi.Customer_Id CODE, " & _
                  "	mc.Nama_Customer ""NAME"",mi.Customer_Id ""QTY"" " & _
                  "	FROM MasterItem mi,MasterCustomer mc " & _
                  " WHERE mi.Customer_Id = mc.Kode_Customer and mi.status = 1 ORDER BY mc.Nama_Customer "
            DS = SQLExecuteQuery(sqlString)
            DT = DS.Tables(0)
            Session("grid_Dialog_" & Request("Mode").ToString) = DT
            grid_Dialog.DataSource = DT
            grid_Dialog.DataBind()
            grid_Dialog.Columns("QTY").Visible = False
        Catch ex As Exception
            lError.Text = "Load_Grid_Packet Exception :<br>" & ex.ToString
        End Try

    End Sub

    Private Sub load_muat_barang()

        '#
        Dim STR As String = ""
        Dim kapal As String = ""
        Try

            Call Create_Grid_Session()
            kapal = Request("customer").ToString
            sqlString = " SELECT " & _
                  "		mb.ID CODE, " & _
                  "	mc.Nama_Customer ""NAME"",mb.Penerima ""QTY"" " & _
                  "	FROM MuatBarang mb,MasterCustomer mc " & _
                  " WHERE mb.Customer_Id = mc.KodeCustomer and mb.status = 1 and mb.Kapal = '" & kapal.ToString & "' ORDER BY mc.Nama_Customer "
            DS = SQLExecuteQuery(sqlString)
            DT = DS.Tables(0)
            Session("grid_Dialog_" & Request("Mode").ToString) = DT
            grid_Dialog.DataSource = DT
            grid_Dialog.DataBind()
            grid_Dialog.Columns("QTY").Caption = "Penerima"
        Catch ex As Exception
            lError.Text = "Load_Grid_Packet Exception :<br>" & ex.ToString
        End Try

    End Sub

    Private Sub load_penerima()

        '#
        Dim STR As String = ""
        Dim ID As String
        Try

            Call Create_Grid_Session()
            ID = Request("customer").ToString
            sqlString = " SELECT " & _
                  "		ID CODE, " & _
                  "	Penerima ""NAME"",Quotation_No ""QTY"" " & _
                  "	FROM MasterQuotation " & _
                  " WHERE status = 1 and Customer_Id = '" & ID.ToString & "' ORDER BY Penerima "
            DS = SQLExecuteQuery(sqlString)
            DT = DS.Tables(0)
            Session("grid_Dialog_" & Request("Mode").ToString) = DT
            grid_Dialog.DataSource = DT
            grid_Dialog.DataBind()
            grid_Dialog.Columns("CODE").Visible = False
            grid_Dialog.Columns("QTY").Visible = False
            grid_Dialog.Columns("NAME").Caption = "Penerima"
        Catch ex As Exception
            lError.Text = "Load_Grid_Packet Exception :<br>" & ex.ToString
        End Try

    End Sub
    Private Sub Load_Grid_Master_Customer()

        '#
        Dim STR As String = ""

        Try

            Call Create_Grid_Session()

            sqlString = " SELECT " & _
                  "		Kode_Customer CODE, " & _
                  "	Nama_Customer ""NAME"",Notes ""QTY"" " & _
                  "	FROM MasterCustomer " & _
                  " WHERE status = 1 ORDER BY Nama_Customer "
            DS = SQLExecuteQuery(sqlString)
            DT = DS.Tables(0)
            Session("grid_Dialog_" & Request("Mode").ToString) = DT
            grid_Dialog.DataSource = DT
            grid_Dialog.DataBind()
            grid_Dialog.Columns("QTY").Caption = "Notes"
            grid_Dialog.Columns("CODE").Visible = True
        Catch ex As Exception
            lError.Text = "Load_Grid_Packet Exception :<br>" & ex.ToString
        End Try

    End Sub

    Private Sub Load_Grid_Master_CustomerW()

        '#
        Dim STR As String = ""

        Try

            Call Create_Grid_Session()

            sqlString = " SELECT " & _
                  "		WH.ID CODE, " & _
                  "	MC.Nama_Customer ""NAME"",MQ.Penerima ""QTY"" " & _
                  "	FROM WarehouseHeader WH join MasterQuotation MQ on WH.Quotation_No = MQ.Quotation_No join MasterCustomer MC on MQ.Customer_Id = MC.Kode_Customer" & _
                  " WHERE WH.status = 1 ORDER BY MC.Nama_Customer "
            DS = SQLExecuteQuery(sqlString)
            DT = DS.Tables(0)
            Session("grid_Dialog_" & Request("Mode").ToString) = DT
            grid_Dialog.DataSource = DT
            grid_Dialog.DataBind()
            grid_Dialog.Columns("NAME").Caption = "Nama"
            grid_Dialog.Columns("QTY").Caption = "Penerima"
        Catch ex As Exception
            lError.Text = "Load_Grid_Packet Exception :<br>" & ex.ToString
        End Try

    End Sub

    Private Sub load_grid_kapal()

        '#
        Dim STR As String = ""

        Try

            Call Create_Grid_Session()

            sqlString = " SELECT " & _
                  "		IDDetail CODE, " & _
                  "	Nama_Kapal ""NAME"",Nahkoda_Kapal ""QTY"" " & _
                  "	FROM Kapal " & _
                  " WHERE status = 1 ORDER BY Nama_Kapal "
            DS = SQLExecuteQuery(sqlString)
            DT = DS.Tables(0)
            Session("grid_Dialog_" & Request("Mode").ToString) = DT
            grid_Dialog.DataSource = DT
            grid_Dialog.DataBind()
            grid_Dialog.Columns("QTY").Caption = "Nahkoda"
        Catch ex As Exception
            lError.Text = "Load_Grid_Packet Exception :<br>" & ex.ToString
        End Try

    End Sub
    Private Sub Load_Grid_Master_Item()

        '#
        Dim STR As String = ""

        Try

            Call Create_Grid_Session()

            sqlString = " SELECT " & _
                  "		ID CODE, " & _
                  "	Nama_Barang ""NAME"",Kode_Barang ""QTY"" " & _
                  "	FROM MasterItem " & _
                  " WHERE status = 1 ORDER BY Nama_Barang "
            DS = SQLExecuteQuery(sqlString)
            DT = DS.Tables(0)
            Session("grid_Dialog_" & Request("Mode").ToString) = DT
            grid_Dialog.DataSource = DT
            grid_Dialog.DataBind()
            grid_Dialog.Columns("QTY").Caption = "Kode"
        Catch ex As Exception
            lError.Text = "Load_Grid_Packet Exception :<br>" & ex.ToString
        End Try

    End Sub
    Private Sub Load_Grid_Packet()

        '#
        Dim STR As String = ""

        Try

            Call Create_Grid_Session()

            '-» [ Filter by Department tidak Jadi¬
            'If Not Request("filter") Is Nothing Then
            '	If Not Session("Department") Is Nothing Then
            '		STR = "	WHERE UPPER(Department_Code) = '" & Session("Department").ToString & "'"
            '	Else
            '		'-: [ Renew Session ]
            '	End If
            'End If

            sqlString = " SELECT " & _
                  "		packet_code CODE, " & _
                  "	packet_name ""NAME"",jenis ""QTY"" " & _
                  "	FROM packet " & _
                  STR & _
                  " ORDER BY packet_name "
            DS = SQLExecuteQuery(sqlString)
            DT = DS.Tables(0)
            Session("grid_Dialog_" & Request("Mode").ToString) = DT
            grid_Dialog.DataSource = DT
            grid_Dialog.DataBind()
            grid_Dialog.Columns("QTY").Caption = "Jenis"
        Catch ex As Exception
            lError.Text = "Load_Grid_Packet Exception :<br>" & ex.ToString
        End Try

    End Sub


    Private Sub Load_Grid_Sales()

        '#
        Dim STR As String = ""

        Try

            Call Create_Grid_Session()

            '-» [ Filter by Department tidak Jadi¬
            'If Not Request("filter") Is Nothing Then
            '	If Not Session("Department") Is Nothing Then
            '		STR = "	WHERE UPPER(Department_Code) = '" & Session("Department").ToString & "'"
            '	Else
            '		'-: [ Renew Session ]
            '	End If
            'End If

            sqlString = " SELECT " & _
                  "		userid CODE, " & _
                  "	namauser ""NAME"",roleid ""QTY"" " & _
                  "	FROM masteruser where roleid = 'RL002' " & _
                  STR & _
                  " ORDER BY namauser "
            DS = SQLExecuteQuery(sqlString)
            DT = DS.Tables(0)
            Session("grid_Dialog_" & Request("Mode").ToString) = DT
            grid_Dialog.DataSource = DT
            grid_Dialog.DataBind()
            grid_Dialog.Columns("QTY").Visible = False
        Catch ex As Exception
            lError.Text = "Load_Grid_Packet Exception :<br>" & ex.ToString
        End Try

    End Sub

    Private Sub Load_Cust_Invoice()
        Try
            Call Create_Grid_Session()

            sqlString = " SELECT Distinct " & _
                  "		IH.CodeCust CODE, " & _
                  "	MC.Nama_Customer ""NAME"",IH.CodeCust ""QTY"" " & _
                  "	FROM InvoiceHeader IH " & _
                  " JOIN MasterCustomer MC ON IH.CodeCust = MC.Kode_Customer " & _
                  " WHERE MC.status = 1 AND IH.[status] <> 0 ORDER BY MC.Nama_Customer "
            DS = SQLExecuteQuery(sqlString)
            DT = DS.Tables(0)
            Session("grid_Dialog_" & Request("Mode").ToString) = DT
            grid_Dialog.DataSource = DT
            grid_Dialog.DataBind()
            grid_Dialog.Columns("QTY").Caption = "Kode"
        Catch ex As Exception
            Throw New Exception(ex.ToString)
        End Try
    End Sub

    Private Sub Load_Cust_InvoiceDS()

        '#
        Dim STR As String = ""

        Try

            Call Create_Grid_Session()

            sqlString = " SELECT " & _
                  "		Kode_Customer CODE, " & _
                  "	Nama_Customer ""NAME"",Area + ' (' + Notes + ')' ""QTY"" " & _
                  "	FROM MasterCustomer " & _
                  " WHERE status = 1 ORDER BY Nama_Customer "
            DS = SQLExecuteQuery(sqlString)
            DT = DS.Tables(0)
            Session("grid_Dialog_" & Request("Mode").ToString) = DT
            grid_Dialog.DataSource = DT
            grid_Dialog.DataBind()
            grid_Dialog.Columns("QTY").Caption = "Area"
            grid_Dialog.Columns("CODE").Visible = True
        Catch ex As Exception
            lError.Text = "Load_Grid_Packet Exception :<br>" & ex.ToString
        End Try
    End Sub

    Protected Sub grid_Dialog_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles grid_Dialog.PreRender
        If Not Page.IsPostBack Then
            grid_Dialog.FocusedRowIndex = -1
        End If
    End Sub

#End Region

End Class
