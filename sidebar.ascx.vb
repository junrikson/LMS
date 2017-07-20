Imports System.Data
Imports System.Data.SqlClient
Public Class sidebar
    Inherits System.Web.UI.UserControl
    Private dt As DataTable
    Private ds As DataSet
    Private sqlstring As String
    Private con As SqlConnection
    Private sdr As SqlDataReader

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        sqlstring = "SELECT D.[menuID], D.[name], D.[order], D.[level], D.[parent], D.[icon], D.[cssClass], D.[url] " & _
                    "FROM roles A INNER JOIN rolesDetail B " & _
                    "ON A.[roleID] = B.[RoleID] " & _
                    "INNER JOIN masterMenu C " & _
                    "ON C.[menuID] = B.[menuID] " & _
                    "INNER JOIN masterMenu D " & _
                    "ON D.[parent] = C.[menuID] " & _
                    "WHERE A.[roleID] = '" & Session("roleID") & "' " & _
                    "AND A.[status] = 1 " & _
                    "AND B.[active] = 1 " & _
                    "AND D.[status] = 1 " & _
                    "ORDER BY D.[order]"

        ds = SQLExecuteQuery(sqlstring)
        dt = ds.Tables(0)

        Dim hasil As String = ""
        Dim i As Integer = 0
        Dim ul As Boolean = 0
        Dim j As Integer = dt.Rows.Count
        If j > 0 Then
            For Each row As DataRow In dt.Rows
                If i > 0 Then
                    If dt.Rows(i).Item("level").ToString > dt.Rows(i - 1).Item("level").ToString Then
                        hasil = hasil & "<ul class=""nav child_menu""><li><a href=""" & dt.Rows(i).Item("url").ToString & """>" & dt.Rows(i).Item("name").ToString & "</a>"
                    ElseIf dt.Rows(i).Item("level").ToString = dt.Rows(i - 1).Item("level").ToString Then
                        If dt.Rows(i).Item("level").ToString = "1" Then
                            hasil = hasil & "</li>"
                        Else
                            hasil = hasil & "</li><li><a href=""" & dt.Rows(i).Item("url").ToString & """>" & dt.Rows(i).Item("name").ToString & "</a>"
                        End If
                    ElseIf dt.Rows(i).Item("level").ToString < dt.Rows(i - 1).Item("level").ToString Then
                        If dt.Rows(i).Item("level").ToString = "1" Then
                            hasil = hasil & "</li></ul></li><li><a><i class=""" & dt.Rows(i).Item("icon").ToString & """></i> " & dt.Rows(i).Item("name").ToString & "<span class=""" & dt.Rows(i).Item("cssClass").ToString & """></span></a>"
                        Else
                            hasil = hasil & "</li></ul></li><li><a href=""" & dt.Rows(i).Item("url").ToString & """>" & dt.Rows(i).Item("name").ToString & "</a>"
                        End If
                    End If
                Else
                    If dt.Rows(i).Item("level").ToString = "1" Then
                        hasil = hasil & "<li><a><i class=""" & dt.Rows(i).Item("icon").ToString & """></i> " & dt.Rows(i).Item("name").ToString & "<span class=""" & dt.Rows(i).Item("cssClass").ToString & """></span></a>"
                    Else
                        hasil = hasil & "<li><a href=""" & dt.Rows(i).Item("url").ToString & """>" & dt.Rows(i).Item("name").ToString & "</a>"
                    End If
                End If

                i = i + 1

                If i = j Then
                    If dt.Rows(i - 1).Item("level").ToString = 1 Then
                        hasil = hasil & "</li>"
                    ElseIf dt.Rows(i - 1).Item("level").ToString = 2 Then
                        hasil = hasil & "</li></ul></li>"
                    ElseIf dt.Rows(i - 1).Item("level").ToString = 3 Then
                        hasil = hasil & "</li></ul></li></ul></li>"
                    ElseIf dt.Rows(i - 1).Item("level").ToString = 4 Then
                        hasil = hasil & "</li></ul></li></ul></li></ul></li>"
                    ElseIf dt.Rows(i - 1).Item("level").ToString = 5 Then
                        hasil = hasil & "</li></ul></li></ul></li></ul></li></ul></li>"
                    End If
                End If
            Next
        End If

        ltrSidebar.Text = hasil
    End Sub

End Class