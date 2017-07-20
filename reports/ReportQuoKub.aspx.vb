Imports System.Data
Imports System.Data.SqlClient
Partial Public Class ReportQuoKub
    Inherits System.Web.UI.Page
    Private DT As DataTable
    Private DTD As DataTable
    Private DS As DataSet
    Private DSD As DataSet
    Private DR As DataRow
    Private sqlstring As String
    Dim iDT As New DataTable
    Dim result As String
    Dim hasil As Integer

#Region "PAGE"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            load_ddl()
            Panel_Input.Visible = True
            Panel_Grid.Visible = True
            Panel_Report.Visible = False
            create_session()
        End If

        If Not Session("Grid_Kondisi") Is Nothing Then
            Grid_Kondisi.DataSource = CType(Session("Grid_Kondisi"), DataTable)
            Grid_Kondisi.DataBind()
        End If
    End Sub

#End Region

#Region "GRID"
    Private Sub create_session()
        iDT.Columns.Add(New DataColumn("ID", GetType(String)))
        iDT.Columns.Add(New DataColumn("Nama_Kondisi", GetType(String)))
        Session("Grid_Kondisi") = iDT
        Grid_Kondisi.DataSource = iDT
        Grid_Kondisi.KeyFieldName = "ID"
        Grid_Kondisi.DataBind()
    End Sub

    Private Sub Grid_Kondisi_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Grid_Kondisi.PreRender
        If Not Page.IsPostBack Then
            Grid_Kondisi.FocusedRowIndex = -1
        End If
    End Sub

    Private Sub Grid_Kondisi_RowCommand(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewRowCommandEventArgs) Handles Grid_Kondisi.RowCommand
        Try
            Select Case e.CommandArgs.CommandName
                Case "Delete"
                    remove_item(Grid_Kondisi.GetRowValues(e.VisibleIndex, "ID").ToString)
            End Select
        Catch ex As Exception
            Throw New Exception("Error Grid Manifest row command : " & ex.ToString)
        End Try
    End Sub
#End Region

#Region "VALIDATION"
    Private Function validation() As Boolean
        DT = CType(Session("Grid_Kondisi"), DataTable)
        For i As Integer = 0 To DT.Rows.Count - 1
            If DT.Rows(i).Item("ID") = hfTID.Value Then
                lblError.Visible = True
                lblError.Text = " Kondisi Yang Di masukkan Telah Ada"
                Return False
            End If
        Next

        If hfKID.Value = "" Or hfTID.Value = "" Then
            lblError.Visible = True
            lblError.Text = " Kondisi belum diisi"
            Return False
        End If
        Return True
    End Function

    Private Function validation_view() As Boolean
        DT = CType(Session("Grid_Kondisi"), DataTable)
        If DT.Rows.Count < 1 Then
            lblError.Visible = True
            lblError.Text = " Harap Isi Kondisi"
            Return False
        End If

        If TxtTulisan.Text.Trim = "" Then
            lblError.Visible = True
            lblError.Text = "Harap isi header untuk quotataion"
            Return False
        End If

        If Txttanda.Text.Trim = "" Then
            lblError.Visible = True
            lblError.Text = "Isi Nama Penandatangan"
            Return False
        End If
        Return True
    End Function

#End Region

#Region "BUTTON"
    Private Sub btviewquotation_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btviewquotation.Click
        Response.Redirect("../Sales/Quotation.aspx")
    End Sub
    Protected Sub btView_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btView.Click
        
        Try
            If validation_view() Then
                If ChkTmplHeader.Checked Then
                    If ddlHeader.SelectedIndex = 0 Then
                        lblError.Visible = True
                        lblError.Text = "Harap Pilih Header terlebih dahulu"
                    Else
                        Panel_Input.Visible = False
                        Panel_Grid.Visible = False
                        Panel_Report.Visible = True
                        lblReport.Text = quoKonfHeader()
                        lblReport.Text &= quotationHeader()
                    End If
                Else
                    Panel_Input.Visible = False
                    Panel_Grid.Visible = False
                    Panel_Report.Visible = True
                    'lblReport.Text = quoKonfHeader()
                    lblReport.Text = quotationHeader()
                End If
                


            End If
        Catch ex As Exception
            Throw New Exception("Error View Report : " & ex.ToString)
        End Try

    End Sub

    Protected Sub btKembaliDevPeriod_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btKembaliDevPeriod.Click
        Panel_Input.Visible = True
        Panel_Grid.Visible = True
        Panel_Report.Visible = False
    End Sub

    'Protected Sub Back_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Back.Click
    '    Panel_Input.Visible = True
    '    Panel_Grid.Visible = True
    '    Panel_Report.Visible = False
    'End Sub


    Protected Sub btAdd_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btAdd.Click
        Dim DT As New DataTable
        Dim DR As DataRow

        Try
            If validation() Then
                DT = CType(Session("Grid_Kondisi"), DataTable)
                DR = DT.NewRow
                DR("ID") = hfTID.Value
                DR("Nama_Kondisi") = hfKID.Value
                DT.Rows.Add(DR)
                Session("Grid_Kondisi") = DT
                Grid_Kondisi.DataSource = DT
                Grid_Kondisi.DataBind()

                Call Refresh_Grid()
                Call Clear()
            End If
        Catch ex As Exception
            Throw New Exception("Error Button Add : " & ex.ToString)
        End Try
    End Sub

#End Region

#Region "METHOD"
    Private Function ambilKondisi() As String
        Dim kondisi As String
        Dim DT As DataTable

        kondisi = ""
        DT = CType(Session("Grid_Kondisi"), DataTable)
        If DT.Rows.Count > 0 Then
            kondisi &= "<tr>"
            kondisi &= "<td>"
            kondisi &= "Kondisi & syarat, sebagai berikut :"
            kondisi &= "</tr>"
            kondisi &= "</td>"
            kondisi &= "<tr>"
            kondisi &= "<td>"
            kondisi &= "<table>"
            For i As Integer = 0 To DT.Rows.Count - 1
                kondisi &= "<tr>"
                kondisi &= "<td style=""vertical-align:top"">"
                kondisi &= "" & i + 1 & "."
                kondisi &= "</td>"
                kondisi &= "<td>"
                kondisi &= DT.Rows(i).Item("Nama_Kondisi").ToString
                kondisi &= "</tr>"
                kondisi &= "</td>"
            Next
            kondisi &= "</table>"
            kondisi &= "</td>"
            kondisi &= "</tr>"
        End If



        Return kondisi
    End Function
    Private Function bodyFill() As String
        Dim body As String
        Dim BorderBottom As String
        Dim satuanHarga As String
        Dim up As String
        Dim pengirim As String
        Dim tujuan As String
        Dim Dari As String
        Dim jabatan As String
        Dim year As String
        Dim pelabuhanBongkar As String
        Dim pelabuhanKirim As String
        Dim QuoNo As String
        Dim Tanggal As Date
        Dim hurufUang As String
        Dim PBDT As DataTable
        Dim PDDS As DataSet
        Dim PDStr As String
        Dim tulisan As String

        Try
            Tanggal = CDate(Now).ToString("dd MMMMM yyyy")
            year = CDate(Tanggal).ToString("yyyy")
            QuoNo = Session("Quotation_No")
            sqlstring = "select mc.Nama_Customer,mc.Area,mq.Tujuan,mq.Penerima,mq.UP,mq.JabatanUP from MasterQuotation mq ,MasterCustomer mc where mq.Customer_Id = mc.Kode_Customer and mq.Quotation_No = '" & QuoNo.ToString & "' and mq.status =1 "
            DS = SQLExecuteQuery(sqlstring)
            DT = DS.Tables(0)
            pengirim = DT.Rows(0).Item("Nama_Customer").ToString
            up = DT.Rows(0).Item("UP").ToString
            jabatan = DT.Rows(0).Item("JabatanUP").ToString
            tujuan = DT.Rows(0).Item("Tujuan").ToString
            Dari = DT.Rows(0).Item("Area").ToString

            PDStr = "select Pelabuhan from MasterTujuan where Tujuan = '" & tujuan.ToString & "' and status = 1 "
            PDDS = SQLExecuteQuery(PDStr)
            PBDT = PDDS.Tables(0)
            If PBDT.Rows.Count > 0 Then
                pelabuhanBongkar = PBDT.Rows(0).Item("Pelabuhan").ToString
            Else
                pelabuhanBongkar = ""
            End If

            PDStr = "select Pelabuhan from MasterTujuan where Tujuan = '" & Dari.ToString & "' and status = 1 "
            PDDS = SQLExecuteQuery(PDStr)
            PBDT = PDDS.Tables(0)
            If PBDT.Rows.Count > 0 Then
                pelabuhanKirim = PBDT.Rows(0).Item("Pelabuhan").ToString
            Else
                pelabuhanKirim = ""
            End If

            sqlstring = "Select qd.Nama_Barang,mhd.NamaHarga,qd.Harga from QuotationDetail qd,MasterHargaDefault mhd where qd.SatuanID = mhd.ID and qd.Quotation_No = '" & QuoNo.ToString & "' "
            DSD = SQLExecuteQuery(sqlstring)
            DTD = DSD.Tables(0)
            body = ""
            'tulisan = "  Memenuhi permintaan Bapak sehubungan dengan adanya rencana pengiriman general cargo " & _
            '                 "     dari " & Dari.ToString & " tujuan " & tujuan.ToString & " untuk periode TAHUN " & year.ToString & " ," & _
            '                 "     dengan ini kami ajukan penawaran biaya pengiriman dengan kondisi & syarat, sebagai berikut : "
            'tulisan = "Memenuhi permintaan Bapak sehubungan dengan adanya rencana pengiriman " & DTD.Rows(0).Item("Nama_Barang").ToString & " " & _
            '              "     dari " & Dari.ToString & " tujuan " & tujuan.ToString & " untuk periode TAHUN " & year.ToString & " ," & _
            '              "     dengan ini kami ajukan penawaran biaya pengiriman dengan kondisi & syarat, sebagai berikut :  "
            tulisan = TxtTulisan.Text.ToString
            If DTD.Rows.Count > 0 Then
                If DTD.Rows.Count > 1 Then
                    body = " <tr>" & _
                         "   <td colspan=2 align=left style=""text-align :justify"">" & _
                         "    " & tulisan & _
                         "   </td>" & _
                         " </tr>" & _
                         " <tr>" & _
                          " <td>" & _
                          " &nbsp;" & _
                          " </td>" & _
                          " </tr>" & _
                          " <tr>" & _
                          "   <td colspan=2 align=center>" & _
                          "     <table border=""1"">" & _
                        "         <tr>" & _
                        "             <td style=""width: 200px; "" >" & _
                        "                 LOKASI/TUJUAN:" & _
                        "             </td>" & _
                        "             <td style=""width: 170px; "">" & _
                        "                 JENIS BARANG:" & _
                        "             </td>" & _
                        "             <td style=""width: 300px; "">" & _
                        "                 TARIF(RP):" & _
                        "             </td>" & _
                        "         </tr>"
                    BorderBottom = "border-bottom:1px black solid;"
                    For e As Integer = 0 To DTD.Rows.Count - 1
                        If DTD.Rows(e).Item("NamaHarga").ToString = "Kubik" Then
                            satuanHarga = "M3"
                        ElseIf DTD.Rows(e).Item("NamaHarga").ToString = "Ton" Then
                            satuanHarga = "Ton"
                        ElseIf DTD.Rows(e).Item("NamaHarga").ToString = "Unit" Then
                            satuanHarga = "Unit"
                        ElseIf DTD.Rows(e).Item("NamaHarga").ToString = "Satuan" Then
                            satuanHarga = "Colly"
                        Else
                            satuanHarga = "Kontainer"
                        End If
                        body &= "<tr>"
                        If e = 0 Then

                            body &= "<td style=""text-align:left;"">"
                            body &= tujuan.ToString
                            body &= "</td>"
                        Else
                            body &= "<td style=""text-align:left;"">"
                            body &= " "
                            body &= "</td>"
                        End If
                        body &= "<td style=""text-align:left;"">"
                        body &= DTD.Rows(e).Item("Nama_Barang").ToString()
                        body &= "</td>"
                        body &= "<td style=""text-align:left;"">"
                        body &= Cek_Data(Format(CDbl(DTD.Rows(e).Item("Harga").ToString), "##,###,###,##.000").ToString) + " ,-/ " + satuanHarga.ToString
                        'body &= Cek_Data(CDbl(DTD.Rows(e).Item("Harga").ToString()).ToString("###,##.00")) + " ,-/ " + satuanHarga.ToString
                        body &= "</td>"
                        body &= "</tr>"

                    Next

                    body &= "     </table>" & _
                            "   </td>" & _
                            " </tr>" & _
                            ""
                Else

                    If DTD.Rows(0).Item("NamaHarga").ToString = "Kubik" Then
                        satuanHarga = "M3"
                    Else
                        satuanHarga = "Ton"
                    End If
                    hurufUang = ""
                    hurufUang = Bilangan2(Integer.Parse(DTD.Rows(0).Item("Harga").ToString).ToString("###,##.00"))
                    body = " <tr>" & _
                          "   <td colspan=2 align=left style=""text-align :justify"">" & _
                          "     " & tulisan & _
                          "   </td>" & _
                          " </tr>" & _
                          " <tr>" & _
                          " <td>" & _
                          " &nbsp;" & _
                          " </td>" & _
                          " </tr>" & _
                          " <tr>" & _
                          "   <td colspan=2 align=left>" & _
                          "     <table>" & _
                          "         <tr>" & _
                          "             <td style = "" width : 250px "" >" & _
                          "                 Pelabuhan muat" & _
                          "             </td>" & _
                          "             <td>" & _
                          "                 :" & _
                          "             </td>" & _
                          "             <td>" & _
                          "                 Pelabuhan " & pelabuhanKirim.ToString & " ." & _
                          "             </td>" & _
                          "         </tr>" & _
                          "         <tr>" & _
                          "             <td style = "" width : 250px "" >" & _
                          "                 Pelabuhan bongkar" & _
                          "             </td>" & _
                          "             <td>" & _
                          "                 :" & _
                          "             </td>" & _
                          "             <td>" & _
                          "                 Pelabuhan " & pelabuhanBongkar.ToString & " ." & _
                          "             </td>" & _
                          "         </tr>" & _
                          "         <tr>" & _
                          "             <td style = "" width : 250px "" >" & _
                          "                 Tarif" & _
                          "             </td>" & _
                          "             <td>" & _
                          "                 :" & _
                          "             </td>" & _
                          "             <td>" & _
                          "                 " & Cek_Data(Format(CDbl(DTD.Rows(0).Item("Harga").ToString), "##,###,###,##.000").ToString) & " ,- ( " & hurufUang.ToString & " RUPIAH ) per " & satuanHarga.ToString & "." & _
                          "             </td>" & _
                          "         </tr>" & _
                          "     </table>" & _
                          "   </td>" & _
                          " </tr>" & _
                          ""
                End If
            End If

        Catch ex As Exception
            Throw New Exception("Error BodyFill function :" & ex.ToString)
        End Try
        Return body
    End Function
    Private Function ambilDetail(ByVal sesHeader As String) As String
        Dim detail As String
        Dim isiDetail As String
        Dim strd As String
        Dim DDT As DataTable
        Dim DDDT As DataTable
        Dim DDS As DataSet

        detail = "            <td style=""vertical-align:top"" align=left >" & _
           "              <table style=""position:relative ;"" bgcolor=white> "

        strd = "Select * from HeaderForm where ID = '" & sesHeader & "' and (status = 1 or status = 7)"
        DDS = SQLExecuteQuery(strd)
        DDDT = DDS.Tables(0)
        If DDDT.Rows.Count > 0 Then

            With DDDT.Rows(0)
                isiDetail = .Item("Alamat").ToString + ", TEL." + .Item("No_Telp1").ToString + " - " + .Item("No_Telp2").ToString + " FAX." + .Item("Fax").ToString + " E-mail:" + .Item("Email").ToString
                detail &= "<tr> "
                detail &= "<td style=""vertical-align:top;padding-left = 50px; "" align=left> "
                detail &= .Item("Daerah").ToString
                detail &= "</td > "
                detail &= "<td style=""vertical-align:top;padding-left = 50px;""  align=left> "
                detail &= ":"
                detail &= "</td> "
                detail &= "<td style=""vertical-align:top;padding-left = 50px;""  align=left> "
                detail &= isiDetail.ToString
                detail &= "</td> "
                detail &= "</tr> "
            End With
            
        End If

        strd = "Select * from DetailHeaderForm where IDHeaderForm = '" & sesHeader & "' and (status = 1 or status = 7) "
        DDS = SQLExecuteQuery(strd)
        DDT = DDS.Tables(0)

        If DDT.Rows.Count > 0 Then
            For i As Integer = 0 To DDT.Rows.Count - 1
                With DDT.Rows(i)
                    isiDetail = .Item("Alamat").ToString + ",TEL." + .Item("No_Telp1").ToString + " - " + .Item("No_Telp2").ToString + " - " + .Item("No_Telp3").ToString + " FAX." + .Item("Fax").ToString
                    detail &= "<tr> "
                    detail &= "<td style=""vertical-align:top;padding-left = 50px; "" align=left> "
                    detail &= .Item("Daerah").ToString
                    detail &= "</td > "
                    detail &= "<td style=""vertical-align:top;padding-left = 50px;""  align=left> "
                    detail &= ":"
                    detail &= "</td> "
                    detail &= "<td style=""vertical-align:top;padding-left = 50px;""  align=left> "
                    detail &= isiDetail.ToString
                    detail &= "</td> "
                    detail &= "</tr> "
                End With
            Next
        End If
        detail &= "</table><br /> " & _
                     "</td>"


        Return detail
    End Function
    Private Function quoKonfHeader() As String
        Dim header As String
        Dim sesHeader As String
        Dim STR As String
        Dim hDT As DataTable
        Dim hDS As DataSet
        Dim headDetail As String
        Dim namaperusahaan As String

        sesHeader = ddlHeader.SelectedValue
        STR = "Select * from HeaderForm where ID = '" & sesHeader & "' and status = 1;"
        hDS = SQLExecuteQuery(STR)
        hDT = hDS.Tables(0)
        headDetail = ambilDetail(sesHeader)
        NamaPerusahaan = hDT.Rows(0).Item("Nama").ToString
        header = "<table width=772px bgcolor=white cellpadding=0 cellspacing=0 style=""font-family:verdana;font-size:12px;height : 140px;"">" & _
             " <tr>" & _
             "   <td style=""width : 50px ; align=center valign=""top""  >" & _
             "      <img src=""" & hDT.Rows(0).Item("PathLogo").ToString & """ style=""height: 120px; width: 120px"" />" & _
             "   </td>" & _
             "   <td style=""width:610px;vertical-align:top;#2c3848;""   align=center >" & _
             "      <table bgcolor=white cellpadding=0 cellspacing=0 style=""font-family:verdana;font-size:10px;"">" & _
             "          <tr>" & _
             "              <td style=""vertical-align:top;""   align=center>" & _
             "                  <font size=""4""><b>" & hDT.Rows(0).Item("Jenis").ToString & "</b> </font>" & _
             "              </td>" & _
             "          </tr>"

        If NamaPerusahaan = "PT. Ligita Jaya" Or NamaPerusahaan = "PT.Ligita Jaya" Then
            header &= "             <tr>" & _
                                            "                 <td align=center>" & _
                                            "                     <font size=""10""><b>" & hDT.Rows(0).Item("Nama").ToString & "</b>" & _
                                            "                 </td>" & _
                                            "             </tr>"
        Else
            header &= "             <tr>" & _
                                            "                 <td align=center>" & _
                                            "                     <font size=""6""><b>" & hDT.Rows(0).Item("Nama").ToString & "</b>" & _
                                            "                 </td>" & _
                                            "             </tr>"
        End If

        header &= "          <tr>" & _
             "              " & headDetail.ToString & " " & _
             "               </tr> " & _
             "           </table>   </td> " & _
             "          </tr>" & _
             "</table> " & _
             "<table width=772px bgcolor=white cellpadding=0 cellspacing=0 style=""font-family:verdana;font-size:12px;"">" & _
             "  <tr> " & _
             "      <td style=""; vertical-align:center;border-bottom:double thick #2c3848;"" colspan=2 align=center valign=""top""  >" & _
             "      </td> " & _
             "  </tr> " & _
             " </table>" & _
             "<table width=772px bgcolor=white><tr style=""height : 5px;""><td  > " & _
             "</td></tr></table>"
        Return header
    End Function
    Private Function quotationHeader() As String
        Dim HeaderReport As String = ""
        Dim Tanggal As Date
        Dim QuoNo As String
        Dim pengirim As String
        Dim tujuan As String
        Dim Dari As String
        Dim up As String
        Dim body As String
        Dim kondisi As String
        Dim jabatan As String
        Dim year As String
        Dim pelabuhanBongkar As String
        Dim pelabuhanKirim As String
        Dim tanggalliat As String
        QuoNo = Session("Quotation_No")
        Tanggal = CDate(Now).ToString("dd MMMMM yyyy")
        year = CDate(Tanggal).ToString("yyyy")

        tanggalliat = Tanggal.ToString("dd") + " " + CekBulan(Tanggal.ToString("MM"), 2) + " " + Tanggal.ToString("yyyy")

        sqlstring = "select mc.Jenis_Perusahaan + ' ' + mc.Nama_Customer as Nama_Customer,mq.Tujuan,mq.Penerima,mq.UP,mq.JabatanUP from MasterQuotation mq ,MasterCustomer mc where mq.Customer_Id = mc.Kode_Customer and mq.Quotation_No = '" & QuoNo.ToString & "' and mq.status =1 "
        DS = SQLExecuteQuery(sqlstring)
        DT = DS.Tables(0)
        pengirim = DT.Rows(0).Item("Nama_Customer").ToString
        up = DT.Rows(0).Item("UP").ToString
        jabatan = DT.Rows(0).Item("JabatanUP").ToString
        If DT.Rows(0).Item("Tujuan").ToString = "Jakarta " Then
            tujuan = "Jakarta"
            Dari = "Belitung"
            pelabuhanKirim = "Tanjung Pandan"
            pelabuhanBongkar = "Sunda Kelapa"
        Else
            tujuan = "Belitung"
            Dari = "Jakarta"
            pelabuhanKirim = "Sunda Kelapa"
            pelabuhanBongkar = "Tanjung Pandan"
        End If

        kondisi = ambilKondisi()
        sqlstring = "Select qd.Nama_Barang,mhd.NamaHarga,qd.Harga from QuotationDetail qd,MasterHargaDefault mhd where qd.SatuanID = mhd.ID and qd.Quotation_No = '" & QuoNo.ToString & "' "
        DSD = SQLExecuteQuery(sqlstring)
        DTD = DSD.Tables(0)
        body = bodyFill()

        If ChkTmplHeader.Checked = False Then
            HeaderReport = "<br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /><br /> "
        End If

        HeaderReport &= "<table width=772px bgcolor=white cellpadding=0 cellspacing=0 style=""font-family:verdana;font-size:12px;position:relative"">" & _
              " <tr>" & _
              "   <td colspan=2 align=left>" & _
              "     <b>No : " & QuoNo.ToString & " </b> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>Jakarta, " & tanggalliat & " </b>" & _
              "   </td>" & _
              " </tr>" & _
              " </br>" & _
              " <tr>" & _
              "   <td colspan=2 align=left>" & _
              "     <b>Kepada Yth. &nbsp; :</b>" & _
              "   </td>" & _
              " </tr>" & _
              " <tr>" & _
              "   <td colspan=2 align=left>" & _
              "     <b>" & pengirim & "</b>" & _
              "   </td>" & _
              " </tr>" & _
              " <tr>" & _
              "   <td colspan=2 align=left>" & _
              "     <b> u.p.&nbsp; :" & up & "</b>" & _
              "   </td>" & _
              " </tr>" & _
              " <tr>" & _
              "   <td colspan=2 align=left>" & _
              "     <b> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; " & jabatan & "</b>" & _
              "   </td>" & _
              " </tr>" & _
              " <tr>" & _
              "   <td colspan=2 align=left>" & _
              "     <b> di-</b>" & _
              "   </td>" & _
              " </tr>" & _
              " <tr>" & _
              "   <td colspan=2 align=left>" & _
              "     <u> " & Dari.ToString & "</u>" & _
              "   </td>" & _
              " </tr>" & _
              " <tr>" & _
              " <td>" & _
              " &nbsp;" & _
              " </td>" & _
              " </tr>" & _
              " <tr>" & _
              "   <td colspan=2 align=center>" & _
              "     <u> <b> Perihal : Penawaran Biaya Angkutan Laut </b></u>" & _
              "   </td>" & _
              " </tr>" & _
              " <tr>" & _
              " <td>" & _
              " &nbsp;" & _
              " </td>" & _
              " </tr>" & _
              " <tr>" & _
              "   <td colspan=2 align=left>" & _
              "     Dengan Hormat," & _
              "   </td>" & _
              " </tr>" & _
              " <tr>" & _
              " <td>" & _
              " &nbsp;" & _
              " </td>" & _
              " </tr>" & _
              " " & body.ToString & "" & _
              " " & kondisi.ToString & "" & _
              " <tr>" & _
              " <td>" & _
              " &nbsp;" & _
              " </td>" & _
              " </tr>" & _
              "   <td colspan=2 align=left style=""text-align :justify"">" & _
              "     Demikian penawaran harga dari kami, dan apabila tidak ada jawaban/konfirmasi maka" & _
              "     penawaran harga ini dianggap dapat diterima/disetujui ( Fax : 021-6908829 )." & _
              "   </td>" & _
              " </tr>" & _
              " <tr>" & _
              " <td>" & _
              "     Atas perhatiannya, kami ucapkan terima kasih" & _
              " </td>" & _
              " </tr>" & _
               " <tr>" & _
              " <td>" & _
              " &nbsp;" & _
              " </td>" & _
              " </tr>" & _
              " <tr>" & _
              "   <td colspan=2 align=center>" & _
              "     &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Hormat kami ," & _
              "   </td>" & _
              " </tr>" & _
              " <tr>" & _
              " <td>" & _
              " &nbsp;" & _
              " </td>" & _
              " </tr>" & _
              " <tr>" & _
              " <td>" & _
              " &nbsp;" & _
              " </td>" & _
              " </tr>" & _
              " <tr>" & _
              " <td>" & _
              " &nbsp;" & _
              " </td>" & _
              " </tr>" & _
              " <tr>" & _
              " <td>" & _
              " &nbsp;" & _
              " </td>" & _
              " </tr>" & _
              " <tr>" & _
              " <td>" & _
              " &nbsp;" & _
              " </td>" & _
              " </tr>" & _
              " <tr>" & _
              "   <td colspan=2 align=center>" & _
              "     &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>" & Txttanda.Text.ToString & "</b>" & _
              "   </td>" & _
              " </tr>" & _
              " <tr>" & _
              "   <td colspan=2 align=center>" & _
              "     &nbsp;&nbsp;&nbsp;" & TxtJabatan.Text.Trim & "" & _
              "   </td>" & _
              " </tr>"

        Return HeaderReport
    End Function
    Private Sub remove_item(ByVal tid As String)
        Try
            DT = CType(Session("Grid_Kondisi"), DataTable)
            For i As Integer = 0 To DT.Rows.Count - 1
                If DT.Rows(i).Item("ID").ToString = tid.ToString Then
                    DT.Rows(i).Delete()
                    Exit For
                End If
            Next
            create_session()
            Session("Grid_Kondisi") = DT
            Grid_Kondisi.DataSource = DT
            Grid_Kondisi.DataBind()

            Call Refresh_Grid()
            Call Clear()
        Catch ex As Exception
            Response.Write("Error Remove_Item <BR> : " & ex.ToString)
        End Try
    End Sub
    Private Sub Clear()
        hfKID.Value = ""
        hfTID.Value = ""
        TxtKondisi.Text = ""
        lblError.Text = ""
    End Sub
    Private Sub Refresh_Grid()
        Dim DT As DataTable
        Try
            DT = CType(Session("Grid_Kondisi"), DataTable)
            Session("Grid_Kondisi") = DT
            Grid_Kondisi.DataSource = DT
            Grid_Kondisi.DataBind()
        Catch ex As Exception
            Response.Write(ex.ToString)
        End Try

    End Sub
#End Region



#Region "DDL"

    Private Sub load_ddl()
        Try
            sqlstring = "select ID,Nama from HeaderForm where [status] = 1"
            DS = SQLExecuteQuery(sqlstring)
            DT = DS.Tables(0)

            With ddlHeader
                .DataSource = DT
                .DataTextField = "Nama"
                .DataValueField = "ID"
                .DataBind()
            End With

            ddlHeader.Items.Insert(0, "Please Select header")

        Catch ex As Exception
            Throw New Exception("Error load ddl header: " & ex.ToString)
        End Try
    End Sub

#End Region
End Class