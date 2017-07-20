Public Partial Class GenerateCode
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then

        End If
    End Sub

    Protected Sub btngenerate_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btngenerate.Click
        Try
            Dim month, year, tanggal As String
            Dim value As String
            Dim no As Integer
            Dim sqlstr As String = ""
            Dim sqlstring As String = ""
            Dim result As String
            Dim code As String = ""
            Dim ds As New DataSet
            Dim dt As New DataTable

            

            month = Date.Today.ToString("MM")
            year = Date.Today.ToString("yy")
            tanggal = Date.Today.ToString("dd")

            sqlstring = "SELECT TOP 1 Kode_Barang FROM MasterItem " & _
                        "WHERE Kode_Barang LIKE 'IT/" & year.ToString & month.ToString & tanggal.ToString & "%' " & _
                        "ORDER BY ID DESC"
            result = SQLExecuteScalar(sqlstring, True, False)

            If result.ToString <> "" Then
                no = CDbl(Right(result, 4)) + 1
            Else
                no = 1
            End If
            value = "IT/" & year.ToString & month.ToString & tanggal.ToString & no.ToString("0000")

            sqlstr = "select ID FROM MasterItem where Kode_Barang = '' and [status] = 1"
            dt = SQLExecuteQuery(sqlstr, True, False).Tables(0)

            If dt.Rows.Count > 0 Then
                code = value
                For i As Integer = 0 To dt.Rows.Count - 1
                    no = CDbl(Right(code, 4)) + 1
                    code = "IT/" & year.ToString & month.ToString & tanggal.ToString & no.ToString("0000")
                    sqlstr &= "UPDATE MasterItem SET Kode_Barang = '" & code.ToString & "' where [status] = 1 and id = " & dt.Rows(i).Item("ID") & "; "

                Next
            End If

            result = SQLExecuteNonQuery(sqlstr, True, False)

            If result <> "" Then
                lblhasil.Text = "Berhasil di Generate, dengan jumlah row : " & result

            End If


        Catch ex As Exception
            Response.Write(ex.ToString)
        End Try
       

        


    End Sub
End Class