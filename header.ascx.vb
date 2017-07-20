Imports System.Data
Imports System.Data.SqlClient
Public Class header
    Inherits System.Web.UI.UserControl
    Private dt As DataTable
    Private ds As DataSet
    Private dr() As DataRow
    Private sqlstring As String
    Private con As SqlConnection
    Private sdr As SqlDataReader
    Private i As Integer

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim currentUri As Uri = New Uri(Request.Url.ToString())

        sqlstring = ";WITH CTERecursion AS (" & _
                    "SELECT menuID, parent " & _
                    "FROM masterMenu " & _
                    "WHERE url = '" & Trim(currentUri.AbsolutePath.ToString()) & "' " & _
                    "UNION ALL " & _
                    "SELECT masterMenu.menuID, masterMenu.parent " & _
                    "FROM masterMenu " & _
                    "INNER JOIN CTERecursion CTE ON masterMenu.menuid = CTE.parent " & _
                    "AND masterMenu.menuID <> CTE.menuid" & _
                    ") SELECT menuID FROM CTERecursion"

        ds = SQLExecuteQuery(sqlstring)
        dt = ds.Tables(0)
        Dim activeMenu(dt.Rows.Count + 1) As String
        i = 0
        For Each row As DataRow In dt.Rows
            activeMenu(i) = dt.Rows(i).Item("menuID").ToString
            i = i + 1
        Next

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
        i = 0
        Dim ul As Boolean = 0
        Dim j As Integer = dt.Rows.Count
        If j > 0 Then
            For Each row As DataRow In dt.Rows
                If (i + 1) < j Then
                    If dt.Rows(i).Item("level").ToString < dt.Rows(i + 1).Item("level").ToString Then
                        hasil = hasil & "<li class=""treeview "
                        If activeMenu.Contains(dt.Rows(i).Item("menuID").ToString) = True Then
                            hasil = hasil & "active"
                        End If
                        hasil = hasil & """><a href=""" & dt.Rows(i).Item("url").ToString & """><i class=""" & dt.Rows(i).Item("icon").ToString & """></i>"
                        If dt.Rows(i).Item("level").ToString = "1" Then
                            hasil = hasil & "<span>" & dt.Rows(i).Item("name").ToString & "</span>"
                        Else
                            hasil = hasil & dt.Rows(i).Item("name").ToString
                        End If
                        hasil = hasil & "<span class=""pull-right-container""><i class=""fa fa-angle-left pull-right""></i></span></a><ul class=""treeview-menu"">"
                    ElseIf dt.Rows(i).Item("level").ToString = dt.Rows(i + 1).Item("level").ToString Then
                        If dt.Rows(i).Item("level").ToString = "1" Then
                            hasil = hasil & "<li class="""
                            If activeMenu.Contains(dt.Rows(i).Item("menuID").ToString) = True Then
                                hasil = hasil & "active"
                            End If
                            hasil = hasil & """><a href=""" & dt.Rows(i).Item("url").ToString & """><i class=""" & dt.Rows(i).Item("icon").ToString & """></i><span>" & dt.Rows(i).Item("name").ToString & "</span></a></li>"
                        Else
                            hasil = hasil & "<li class="""
                            If activeMenu.Contains(dt.Rows(i).Item("menuID").ToString) = True Then
                                hasil = hasil & "active"
                            End If
                            hasil = hasil & """><a href=""" & dt.Rows(i).Item("url").ToString & """><i class=""" & dt.Rows(i).Item("icon").ToString & """></i>" & dt.Rows(i).Item("name").ToString & "</a></li>"
                        End If
                    Else
                        If dt.Rows(i).Item("level").ToString = "1" Then
                            hasil = hasil & "<li class="""
                            If activeMenu.Contains(dt.Rows(i).Item("menuID").ToString) = True Then
                                hasil = hasil & "active"
                            End If
                            hasil = hasil & """><a href=""" & dt.Rows(i).Item("url").ToString & """><i class=""" & dt.Rows(i).Item("icon").ToString & """></i><span>" & dt.Rows(i).Item("name").ToString & "</span></a></li></ul></li>"
                        Else
                            hasil = hasil & "<li class="""
                            If activeMenu.Contains(dt.Rows(i).Item("menuID").ToString) = True Then
                                hasil = hasil & "active"
                            End If
                            hasil = hasil & """><a href=""" & dt.Rows(i).Item("url").ToString & """><i class=""" & dt.Rows(i).Item("icon").ToString & """></i>" & dt.Rows(i).Item("name").ToString & "</a></li></ul></li>"
                        End If
                    End If
                Else
                    If dt.Rows(i).Item("level").ToString = "1" Then
                        hasil = hasil & "<li class="""
                        If activeMenu.Contains(dt.Rows(i).Item("menuID").ToString) = True Then
                            hasil = hasil & "active"
                        End If
                        hasil = hasil & """><a href=""" & dt.Rows(i).Item("url").ToString & """><i class=""" & dt.Rows(i).Item("icon").ToString & """></i><span>" & dt.Rows(i).Item("name").ToString & "</span></a>"
                    Else
                        hasil = hasil & "<li class="""
                        If activeMenu.Contains(dt.Rows(i).Item("menuID").ToString) = True Then
                            hasil = hasil & "active"
                        End If
                        hasil = hasil & """><a href=""" & dt.Rows(i).Item("url").ToString & """><i class=""" & dt.Rows(i).Item("icon").ToString & """></i>" & dt.Rows(i).Item("name").ToString & "</a>"
                    End If

                    If dt.Rows(i).Item("level").ToString = 1 Then
                        hasil = hasil & "</li>"
                    ElseIf dt.Rows(i).Item("level").ToString = 2 Then
                        hasil = hasil & "</li></ul></li>"
                    ElseIf dt.Rows(i).Item("level").ToString = 3 Then
                        hasil = hasil & "</li></ul></li></ul></li>"
                    ElseIf dt.Rows(i).Item("level").ToString = 4 Then
                        hasil = hasil & "</li></ul></li></ul></li></ul></li>"
                    ElseIf dt.Rows(i).Item("level").ToString = 5 Then
                        hasil = hasil & "</li></ul></li></ul></li></ul></li></ul></li>"
                    End If
                End If
                i = i + 1
            Next
        End If

        ltrCompanyID.Text = Session("companyID")
        ltrCompanyName.Text = Session("companyName")
        ltrName.Text = Session("userName")
        ltrName2.Text = Session("userName")
        ltrCompanyDetail.Text = Session("companyAddress") & "<br/>" & Session("companyPhone")
        ltrSidebar.Text = hasil
    End Sub
End Class