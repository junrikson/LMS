Imports System.Data
Imports System.Data.SqlClient
Partial Class role_detail
    Inherits System.Web.UI.Page
    Private DS As DataSet
    Private DT As DataTable
    Private sqlstring As String
    Private SDR As SqlDataReader
#Region " Page "

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Session("UserID") = Nothing Then

            FormsAuthentication.SignOut()
            Response.Redirect("index.aspx")

        End If

        Try

            If Not Page.IsPostBack Then

                Call Load_DDLRole()
                Call Load_menu()
            End If

        Catch ex As Exception
            Response.Write("Page_Load Exception :<br>" & ex.ToString)
        End Try

    End Sub

#End Region


#Region " Methods "

    Private Sub update()

        Dim result As Integer

        Try
            Dim str As String = ""
            Dim hasil As String = ""

            For i As Integer = 0 To rptMenu.Items.Count - 1
                Dim ri As RepeaterItem = rptMenu.Items(i)
                Dim cblChild As CheckBoxList = ri.FindControl("cblChild")
                For j As Integer = 0 To cblChild.Items.Count - 1
                    str = "SELECT MenuID FROM RolesDetail Where RoleID = '" & DDLRoles.SelectedValue.ToString & "' " & _
                                "AND [status] = 1 and menuid='" & cblChild.Items(j).Value & "'"
                    hasil = SQLExecuteScalar(str)

                    If hasil.ToString <> "" Then
                        If cblChild.Items(j).Selected = True Then
                            Update_syntax("True", cblChild.Items(j).Value)
                        Else
                            Update_syntax("False", cblChild.Items(j).Value)
                        End If

                    Else
                        If cblChild.Items(j).Selected = True Then
                            Insert_Syntax("True", cblChild.Items(j).Value)
                        Else
                            Insert_Syntax("False", cblChild.Items(j).Value)
                        End If
                    End If


                Next

            Next
            result = SQLExecuteNonQuery(sqlstring)

            If result > 0 Then
                linfoberhasil.Visible = True
                linfoberhasil.Text = "Konfigurasi Role Berubah"
            End If

        Catch ex As Exception
            Response.Write("Insert Exception :<br>" & ex.ToString)
        End Try

    End Sub

    Private Sub Update_syntax(ByVal TorF As String, ByVal MenuID As String)
        sqlstring &= "UPDATE RolesDetail " & _
            "SET " & _
            "Visible = '" & TorF & "', " & _
            "UserName   = '" & Session("UserId").ToString & "', " & _
            "LastModified = '" & Now.ToString & "' " & _
            "WHERE RoleID = '" & DDLRoles.SelectedValue & "'  " & _
            "AND MenuID =  '" & MenuID & "'  ; "
    End Sub

    Private Sub Insert_Syntax(ByVal TorF As String, ByVal MenuID As String)
        sqlstring &= "INSERT INTO RolesDetail(RoleID, MenuID, Visible, UserName, [status], Timestamp) " & _
            "VALUES " & _
            "('" & DDLRoles.SelectedValue.ToString & "', '" & MenuID & "', " & _
            "'" & TorF & "', '" & Session("UserId").ToString & "', 1, '" & Now.ToString & "');"
    End Sub
#End Region


#Region "DropDownList"
    Private Sub Load_DDLRole()
        Try
            If Session("namaroles").ToString.Contains("Account") Then
                sqlstring = " SELECT roleid, Nama from Roles where status = 1 order by nama"
            Else
                sqlstring = " SELECT roleid, Nama from Roles where status = 1 and nama not like '%Account%' order by nama"
            End If
            Dim dt As DataTable = SQLExecuteQuery(sqlstring).Tables(0)
            With DDLRoles
                .DataSource = dt
                .DataTextField = "Nama"
                .DataValueField = "RoleID"
                .DataBind()
            End With
            'DDLGolongan.Items.Insert(0, "ALL")
            'DDLGolongan.Items.Item(0).Value = "%"
        Catch ex As Exception
            Response.Write("DDLRole Exception :<br>" & ex.ToString)
        End Try
    End Sub


    Private Sub Load_menu()
        Try
            sqlstring = "SELECT DISTINCT Parent FROM MasterMenu WHERE [status] = 1"
            DS = SQLExecuteQuery(sqlstring)
            DT = DS.Tables(0)
            Dim a As Integer = DT.Rows.Count
            rptMenu.DataSource = DT
            rptMenu.DataBind()

        Catch ex As Exception
            Response.Write("Load_Menu Exception :<br>" & ex.ToString)
        End Try

    End Sub

#End Region

#Region " Button "

    Protected Sub btSimpan_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btSimpan.Click

        Try
            Call update()

            body1.Attributes.Add("onLoad", "javascript:reloadframe();")
        Catch ex As Exception
            Response.Write("btSimpan Exception :<br>" & ex.ToString)
        End Try

    End Sub

    Protected Sub btBatal_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btBatal.Click

        Try
            Response.Redirect("SettingRoles.aspx")

        Catch ex As Exception
            Response.Write("btBatal Exception :<br>" & ex.ToString)
        End Try

    End Sub

    Protected Sub DDLRoles_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DDLRoles.SelectedIndexChanged
        Call Load_menu()

    End Sub
#End Region


    Private Sub rptMenu_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.RepeaterItemEventArgs) Handles rptMenu.ItemDataBound
        Try
            Dim Role As String = ""

            If DDLRoles.SelectedValue.ToString = "RL005" Or DDLRoles.SelectedValue.ToString = "RL004" Or DDLRoles.SelectedValue.ToString = "RL013" Or DDLRoles.SelectedValue.ToString = "RL014" Then
                Role = "Acc"
            Else
                Role = "Other"
            End If

            If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
                Dim rDS As New DataSet
                Dim rDT As New DataTable

                Dim lblHeader As Label = e.Item.FindControl("lblHeader")

                Dim cblChild As CheckBoxList = e.Item.FindControl("cblChild")
                Dim dr As DataRowView = e.Item.DataItem
                lblHeader.Text = dr.Item("Parent").ToString

                sqlstring = "SELECT " & _
                            "m.ID,  " & _
                            "m.child, ISNULL(d.visible, 'False') as visible " & _
                            "FROM MasterMenu m LEFT JOIN RolesDetail d ON d.MenuID=m.id and d.RoleID = '" & DDLRoles.SelectedValue & "' and d.Status = 1 " & _
                            "WHERE m.Status = 1  and m.parent= '" & dr.Item("Parent") & "'   " & _
                            "ORDER BY m.ID "
                rDS = SQLExecuteQuery(sqlstring)
                rDT = rDS.Tables(0)

                If rDT.Rows.Count > 0 Then
                    For i As Integer = 0 To rDT.Rows.Count - 1
                        cblChild.Items.Add(rDT.Rows(i).Item("child").ToString)
                        cblChild.Items(i).Value = rDT.Rows(i).Item("ID").ToString
                        If dr.Item("Parent").ToString = "Accounting" And Role = "Acc" Then
                            If rDT.Rows(i).Item("visible") = "True" Then

                                cblChild.Items(i).Selected = True

                            End If
                        ElseIf dr.Item("Parent").ToString <> "Accounting" And Role = "Acc" Then
                            If rDT.Rows(i).Item("visible") = "True" Then

                                cblChild.Items(i).Selected = True

                            End If
                        ElseIf dr.Item("Parent").ToString = "Accounting" And Role <> "Acc" Then


                            cblChild.Items(i).Selected = False
                            cblChild.Items(i).Enabled = False

                        ElseIf dr.Item("Parent").ToString <> "Accounting" And Role <> "Acc" Then
                            If rDT.Rows(i).Item("visible") = "True" Then

                                cblChild.Items(i).Selected = True

                            End If
                        End If

                    Next
                End If
            End If
        Catch ex As Exception
            Throw New Exception("<b>Error RptMenu Data bound :</b>" & ex.ToString)
        End Try
    End Sub
End Class
