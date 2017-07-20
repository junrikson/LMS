Imports System.Data
Imports System.Data.SqlClient

Partial Class dialog7
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

                    Case "SO"
                        Call Load_Grid_SO()
                    Case "DO"
                        Call Load_Grid_DO()
                    Case "Retur"
                        Call Load_Grid_Retur()
                    Case "SO_Replace"
                        Call Load_Grid_Replace()
                    Case "DO_Replace"
                        Load_Grid_Replace_DO()
                    Case "MasterItem"
                        Call Load_Grid_Master_Item()
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


    Private Sub Load_Grid_SO()

        Dim STR As String = ""
        Dim sDT As DataTable
        Dim DR As DataRow
        Try
            sDT = New DataTable
            With sDT.Columns
                .Add(New DataColumn("CODE", GetType(String)))
                .Add(New DataColumn("NAME", GetType(String)))
                .Add(New DataColumn("WAREHOUSE", GetType(String)))
                .Add(New DataColumn("TYPE", GetType(String)))
                .Add(New DataColumn("PRICE", GetType(String)))
                .Add(New DataColumn("DP", GetType(String)))
                .Add(New DataColumn("MEMO", GetType(String)))
            End With
            Call Create_Grid_Session()


            sqlString = " SELECT " & _
                  "		so_no CODE, " & _
                  "	customer_name ""NAME"",gudang_name ""WAREHOUSE"",type_sale ""TYPE"",harga_total ""PRICE"",dp ""DP"",memo ""MEMO"" " & _
                  "	FROM SO_Header where status = 0 " & _
                  STR & _
                  " ORDER BY so_no "
            DS = SQLExecuteQuery(sqlString)
            DT = DS.Tables(0)
            If DT.Rows.Count > 0 Then
                For i As Integer = 0 To DT.Rows.Count - 1
                    DR = sDT.NewRow
                    With DT.Rows(i)
                        DR("CODE") = .Item("CODE")
                        DR("NAME") = .Item("NAME")
                        DR("WAREHOUSE") = .Item("WAREHOUSE")
                        DR("TYPE") = .Item("TYPE")
                        DR("PRICE") = .Item("PRICE")
                        DR("DP") = .Item("DP")
                        DR("MEMO") = .Item("MEMO")
                    End With
                    sDT.Rows.Add(DR)
                Next
            End If

            'sqlString = " SELECT " & _
            '      "		ro_no CODE, " & _
            '      "	customer_name ""NAME"",gudang_name ""WAREHOUSE"",type_sale ""TYPE"",harga_total ""PRICE"",dp ""DP"",memo ""MEMO"" " & _
            '      "	FROM RO_Header where status = 0 " & _
            '      " ORDER BY ro_no "

            'DS = SQLExecuteQuery(sqlString )
            'DT = DS.Tables(0)
            'If DT.Rows.Count > 0 Then
            '    For i As Integer = 0 To DT.Rows.Count - 1
            '        DR = sDT.NewRow
            '        With DT.Rows(i)
            '            DR("CODE") = .Item("CODE")
            '            DR("NAME") = .Item("NAME")
            '            DR("WAREHOUSE") = .Item("WAREHOUSE")
            '            DR("TYPE") = .Item("TYPE")
            '            DR("PRICE") = .Item("PRICE")
            '            DR("DP") = .Item("DP")
            '            DR("MEMO") = .Item("MEMO")
            '        End With
            '        sDT.Rows.Add(DR)
            '    Next
            'End If
            If sDT.Rows.Count > 0 Then
                Session("grid_Dialog_" & Request("Mode").ToString) = sDT
                grid_Dialog.DataSource = sDT
                grid_Dialog.DataBind()
            Else
                Session("grid_Dialog_" & Request("Mode").ToString) = sDT
                grid_Dialog.DataSource = Nothing
                grid_Dialog.DataBind()
            End If

        Catch ex As Exception
            lError.Text = "Load_Grid_SO Exception :<br>" & ex.ToString
        End Try

    End Sub


    Private Sub Load_Grid_Replace()

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
                  "		so_no CODE, " & _
                  "	customer_name ""NAME"",customer_no ""WAREHOUSE"",gudang_name ""TYPE"",sales_person ""PRICE"",harga_total ""DP"",memo ""MEMO"" " & _
                  "	FROM SO_Header where so_no like 'SO%' " & _
                  STR & _
                  " ORDER BY so_no "

            DS = SQLExecuteQuery(sqlString)
            DT = DS.Tables(0)
            Session("grid_Dialog_" & Request("Mode").ToString) = DT
            grid_Dialog.DataSource = DT
            grid_Dialog.DataBind()
            grid_Dialog.Columns("NAME").Caption = "Customer"
            grid_Dialog.Columns("WAREHOUSE").Caption = "Customer Id"
            grid_Dialog.Columns("TYPE").Caption = "Warehouse"
            grid_Dialog.Columns("PRICE").Caption = "Sales"
            grid_Dialog.Columns("DP").Caption = "Total Price"
        Catch ex As Exception
            lError.Text = "Load_Grid_SO Exception :<br>" & ex.ToString
        End Try

    End Sub

    Private Sub Load_Grid_Replace_DO()

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

            sqlString = " SELECT distinct" & _
                  "		d.do_no CODE, " & _
                  "	d.customer_name ""NAME"",h.customer_no ""WAREHOUSE"",d.gudang_name ""TYPE"",h.sales_person ""PRICE"",d.type_sale ""DP"",d.harga_total ""MEMO"" " & _
                  "	FROM DO d, SO_Header h where h.so_no = d.so_no and d.customer_name = h.customer_name " & _
                  STR & _
                  " ORDER BY d.do_no "

            DS = SQLExecuteQuery(sqlString)
            DT = DS.Tables(0)
            Session("grid_Dialog_" & Request("Mode").ToString) = DT
            grid_Dialog.DataSource = DT
            grid_Dialog.DataBind()
            grid_Dialog.Columns("NAME").Caption = "Customer"
            grid_Dialog.Columns("WAREHOUSE").Caption = "Customer Id"
            grid_Dialog.Columns("TYPE").Caption = "Warehouse"
            grid_Dialog.Columns("PRICE").Caption = "Sales"
            grid_Dialog.Columns("DP").Caption = "Type"
            grid_Dialog.Columns("MEMO").Caption = "Total Price"
            grid_Dialog.Columns("MEMO").Visible = False
        Catch ex As Exception
            lError.Text = "Load_Grid_SO Exception :<br>" & ex.ToString
        End Try

    End Sub

    Private Sub Load_Grid_Master_Item()

        '#
        Dim STR As String = ""

        Try

            Call Create_Grid_Session()

            Dim idcust As String = ""

            idcust = Request("idcust")

            sqlString = " SELECT " & _
                  "		ID CODE, " & _
                  "	Nama_Barang ""NAME"",status ""WAREHOUSE"", " & _
                  "	Berat ""TYPE"",Panjang ""PRICE"", " & _
                  "	Tinggi ""DP"",Lebar ""MEMO"", " & _
                  "	Unit ""UNIT"" FROM MasterItem " & _
                  " WHERE Customer_Id = '" & idcust.ToString & "' " & _
                  " AND [status] = 1 ORDER BY Nama_Barang "
            DS = SQLExecuteQuery(sqlString)
            DT = DS.Tables(0)
            Session("grid_Dialog_" & Request("Mode").ToString) = DT
            grid_Dialog.DataSource = DT
            grid_Dialog.DataBind()
            grid_Dialog.Columns("NAME").Caption = "Nama Barang"
            grid_Dialog.Columns("WAREHOUSE").Visible = False
            grid_Dialog.Columns("TYPE").Caption = "Berat"
            grid_Dialog.Columns("PRICE").Caption = "Panjang"
            grid_Dialog.Columns("DP").Caption = "Tinggi"
            grid_Dialog.Columns("MEMO").Caption = "Lebar"
        Catch ex As Exception
            lError.Text = "Load_Grid_Packet Exception :<br>" & ex.ToString
        End Try

    End Sub
    Private Sub Load_Grid_DO()

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

            sqlString = " SELECT distinct " & _
                  "		do_no CODE, " & _
                  "	so_no ""NAME"",gudang_name ""WAREHOUSE"",type_sale ""TYPE"",tanggal ""PRICE"",customer_name ""DP"",harga_total ""MEMO"" " & _
                  "	FROM DO where status = 1 " & _
                  STR & _
                  " ORDER BY do_no "

            DS = SQLExecuteQuery(sqlString)
            DT = DS.Tables(0)
            Session("grid_Dialog_" & Request("Mode").ToString) = DT
            grid_Dialog.DataSource = DT
            grid_Dialog.DataBind()

            grid_Dialog.Columns("DP").Caption = "Customer Name"
            grid_Dialog.Columns("CODE").Caption = "DO No"
            grid_Dialog.Columns("NAME").Caption = "SO No"
            grid_Dialog.Columns("PRICE").Caption = "DATE"
            grid_Dialog.Columns("MEMO").Caption = "Total Price"
            grid_Dialog.Columns("MEMO").Visible = False
        Catch ex As Exception
            lError.Text = "Load_Grid_SO Exception :<br>" & ex.ToString
        End Try

    End Sub


    Private Sub Load_Grid_Retur()

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

            sqlString = " SELECT distinct " & _
                  "		retur_no CODE, " & _
                  "	retur_date ""NAME"",do_no ""WAREHOUSE"",gudang_name ""TYPE"",customer_name ""PRICE"",memo ""DP"",status ""MEMO"" " & _
                  "	FROM retur_header where status = 0 " & _
                  STR & _
                  " ORDER BY retur_no "

            DS = SQLExecuteQuery(sqlString)
            DT = DS.Tables(0)
            Session("grid_Dialog_" & Request("Mode").ToString) = DT
            grid_Dialog.DataSource = DT
            grid_Dialog.DataBind()

            grid_Dialog.Columns("MEMO").Visible = False
            grid_Dialog.Columns("CODE").Caption = "Retur No"
            grid_Dialog.Columns("NAME").Caption = "Retur Date"
            grid_Dialog.Columns("WAREHOUSE").Caption = "DO No"
            grid_Dialog.Columns("TYPE").Caption = "Warehouse"
            grid_Dialog.Columns("PRICE").Caption = "Customer Name"
            grid_Dialog.Columns("DP").Caption = "MeMo"
            grid_Dialog.Columns("MEMO").Caption = "Status"
        Catch ex As Exception
            lError.Text = "Load_Grid_SO Exception :<br>" & ex.ToString
        End Try

    End Sub


    Protected Sub grid_Dialog_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles grid_Dialog.PreRender
        If Not Page.IsPostBack Then
            grid_Dialog.FocusedRowIndex = -1
        End If
    End Sub

#End Region

End Class
