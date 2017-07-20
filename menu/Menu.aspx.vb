Imports System.Data
Imports System.Data.SqlClient
Partial Class menu_Menu
    Inherits System.Web.UI.Page
    Private sqlString As String
    Private sDR As SqlDataAdapter
    Private con As SqlConnection
    Private DS As DataSet
    Private DSchild As DataSet
    Private DSrole As DataSet
    Private DT As DataTable
    Private DTchild As DataTable
    Private DTrole As DataTable

#Region " Function "

    Private Sub tree()
        Dim roleid As String
        Dim i As Integer
        Dim j As Integer

        Try
            'get roleid
            sqlString = " SELECT RoleID FROM masteruser  where status=1 AND UserID  ='" & Session("UserId") & "' "
            roleid = SQLExecuteScalar(sqlString)

            'get parent
            sqlString = " SELECT DISTINCT " & _
               "	a.parent " & _
               "	FROM MasterMenu a inner join RolesDetail b on b.MenuID = a.id " & _
               "	WHERE a.Status = 1 AND b.RoleID = '" & roleid & "' AND b.Status = 1 AND b.visible='true' " & _
               "    ORDER BY a.parent"

            DS = SQLExecuteQuery(sqlString)
            DT = DS.Tables(0)

            If DT.Rows.Count > 0 Then
                'get child
                For i = 0 To DT.Rows.Count - 1
                    TreeView1.Nodes.Add(New TreeNode(DT.Rows(i).Item("parent").ToString))
                    TreeView1.Nodes(i).SelectAction = TreeNodeSelectAction.Expand

                    sqlString = " SELECT " & _
                     "		a.ID, " & _
                     "		a.child, " & _
                     "		a.url, " & _
                     "		a.Target " & _
                     "	FROM MasterMenu a inner join RolesDetail b on b.MenuID = a.id " & _
                     "	WHERE a.Status = 1 AND parent = '" & DT.Rows(i).Item("parent").ToString & "' AND b.RoleID = '" & roleid & "' AND b.Status = 1 AND b.visible='true'  " & _
                     "	ORDER BY a.child "


                    DSchild = SQLExecuteQuery(sqlString)
                    DTchild = DSchild.Tables(0)
                    If DTchild.Rows.Count > 0 Then
                        For j = 0 To DTchild.Rows.Count - 1
                            With DTchild.Rows(j)
                                TreeView1.Nodes(i).ChildNodes.Add(New TreeNode(.Item("child").ToString, "", "", .Item("url"), .Item("target")))
                            End With
                        Next
                    End If
                Next
            End If
        Catch ex As Exception
            Response.Write("Load Tree Exception :<br>" & ex.ToString)
        End Try

    End Sub


#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try

            If Not Page.IsPostBack Then
                Call tree()
            End If



        Catch ex As Exception
            Response.Write("Page_Load Exception :<br>" & ex.ToString)
        End Try
    End Sub

    Protected Sub TreeView1_SelectedNodeChanged(sender As Object, e As EventArgs) Handles TreeView1.SelectedNodeChanged

    End Sub
End Class
