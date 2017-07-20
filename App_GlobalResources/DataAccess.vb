Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.Text
Imports System.Globalization



Public Module DataAccess
    Dim countzero As Integer
    Public Key As String = "LMS"
    Public ConnectionString As String = System.Configuration.ConfigurationManager.AppSettings.Get("ConString2")
    Public ConnectionString2 As String = System.Configuration.ConfigurationManager.AppSettings.Get("ConString1")
    Public Singkatan As String = System.Configuration.ConfigurationManager.AppSettings.Get("Singkatan")
    Public Profil As String = System.Configuration.ConfigurationManager.AppSettings.Get("Initial")


    Public Function CekNilai(ByVal Value As String) As Double
        Try
            Dim hasilangka As Double

            If Value.ToString.Trim = "" Then
                hasilangka = 0

            Else
                hasilangka = Value
            End If

            Return hasilangka

        Catch ex As Exception
            Throw New Exception("<b>Error Function Cek Nilai :</b>" & ex.ToString)
        End Try
    End Function

    Public Function CekValue(ByVal Value As String) As String
        Try
            Dim hasilangka As String

            If Value = "" Then
                hasilangka = 0

            Else
                hasilangka = Value
            End If

            Return hasilangka

        Catch ex As Exception
            Throw New Exception("<b>Error Function Cek Nilai :</b>" & ex.ToString)
        End Try
    End Function

    Public Function toSqlString(ByVal value As String) As String
        value = value.Replace("'", "`")
        Return value
    End Function

    Public Function p(ByVal str As String) As String
        str = "<p>" & str & "</p>"
        Return str
    End Function

    Public Function zeroifblank(ByVal value As String) As Double
        Try
            If value.ToString = "" Then
                Return 0
            Else
                Return CDec(value.ToString)
            End If
        Catch ex As Exception
            Throw New Exception("ZeroIfBlank Exceptions : <br>" & ex.ToString)
        End Try
    End Function

    Public Function CekHari(ByVal Day As String, ByVal Type As Integer) As String
        Try

            Select Case Day
                Case "Monday"
                    Return "Senin"
                Case "Tuesday"
                    Return "Selasa"
                Case "Wednesday"
                    Return "Rabu"
                Case "Thursday"
                    Return "Kamis"
                Case "Friday"
                    Return "Jumat"
                Case "Saturday"
                    Return "Sabtu"
                Case "Sunday"
                    Return "Minggu"
            End Select
        Catch ex As Exception
            Throw New Exception("Cek Bulan Exceptions : " & vbCrLf & ex.ToString)
        End Try
        Return ""
    End Function

    Public Function CekBulan(ByVal Month As String, ByVal Type As Integer) As String
        Try

            Select Case CInt(Month)
                Case 1
                    If Type = 1 Then
                        Return "I"
                    ElseIf Type = 2 Then
                        Return "Januari"
                    End If
                Case 2
                    If Type = 1 Then
                        Return "II"
                    ElseIf Type = 2 Then
                        Return "Februari"
                    End If
                Case 3
                    If Type = 1 Then
                        Return "III"
                    ElseIf Type = 2 Then
                        Return "Maret"
                    End If
                Case 4
                    If Type = 1 Then
                        Return "IV"
                    ElseIf Type = 2 Then
                        Return "April"
                    End If
                Case 5
                    If Type = 1 Then
                        Return "V"
                    ElseIf Type = 2 Then
                        Return "Mei"
                    End If
                Case 6
                    If Type = 1 Then
                        Return "VI"
                    ElseIf Type = 2 Then
                        Return "Juni"
                    End If
                Case 7
                    If Type = 1 Then
                        Return "VII"
                    ElseIf Type = 2 Then
                        Return "Juli"
                    End If
                Case 8
                    If Type = 1 Then
                        Return "VIII"
                    ElseIf Type = 2 Then
                        Return "Agustus"
                    End If
                Case 9
                    If Type = 1 Then
                        Return "IX"
                    ElseIf Type = 2 Then
                        Return "September"
                    End If
                Case 10
                    If Type = 1 Then
                        Return "X"
                    ElseIf Type = 2 Then
                        Return "Oktober"
                    End If
                Case 11
                    If Type = 1 Then
                        Return "XI"
                    ElseIf Type = 2 Then
                        Return "November"
                    End If
                Case 12
                    If Type = 1 Then
                        Return "XII"
                    ElseIf Type = 2 Then
                        Return "Desember"
                    End If
            End Select
        Catch ex As Exception
            Throw New Exception("Cek Bulan Exceptions : " & vbCrLf & ex.ToString)
        End Try
        Return ""
    End Function

    Public Function CekBulan(ByVal Month As String) As String
        Try

            Select Case CInt(Month)
                Case 1
                    Return "I"
                Case 2
                    Return "II"
                Case 3
                    Return "III"
                Case 4
                    Return "IV"
                Case 5
                    Return "V"
                Case 6
                    Return "VI"
                Case 7
                    Return "VII"
                Case 8
                    Return "VIII"
                Case 9
                    Return "IX"
                Case 10
                    Return "X"
                Case 11
                    Return "XI"
                Case 12
                    Return "XII"
            End Select
        Catch ex As Exception
            Throw New Exception("Cek Bulan Exceptions : " & vbCrLf & ex.ToString)
        End Try
        Return ""
    End Function

    'SQL BUAT DUA DATABASE NEW :D 


#Region " Rheza SQL for Yuki "

'Execute Non QUery With Transaction (modified)
    Public Function SQLExecuteNonQuery(ByRef SQLString As String, Optional ByVal ConnString As String = "", Optional ByVal DBMaxTry As Integer = 2, Optional ByVal SleepInterval As Integer = 250) As Long
        Dim cn As New SqlConnection
        Dim cmd As New SqlCommand
        Dim recordsAffected As Long = 0
        Dim TryCount As Integer
        Dim Success As Boolean
        Dim CummulatedMessage As String = ""

        Try
            If ConnString = "" Then
                ConnString = ConnectionString
            End If

            '---- Looping untill success or maxtry reached ------
            TryCount = 0
            Success = False
            While Not Success And TryCount <= DBMaxTry
                Try
                    cn = New SqlConnection(ConnString)
                    cmd = New SqlCommand(SQLString, cn)
                    cn.Open()

                    cmd.Transaction = cn.BeginTransaction
                    recordsAffected = cmd.ExecuteNonQuery()

                    cmd.Transaction.Commit()
                    Success = True
                Catch ex As Threading.ThreadAbortException
                Catch ex As Exception
                    If ex.Message.ToLower.IndexOf("network") <> -1 _
                        Or ex.Message.ToLower.IndexOf("timeout") <> -1 _
                        Or ex.Message.ToLower.IndexOf("access") <> -1 _
                        Or ex.Message.ToLower.IndexOf("deadlocked") <> -1 Then
                        If TryCount >= DBMaxTry Then
                            Throw New Exception("SQLExecuteNonQuery - MaxTry Exceeded." & vbCrLf & "CummulatedMessage = " & CummulatedMessage & vbCrLf & "Trycount = " & TryCount.ToString & _
                                      "SQL: " & SQLString & vbCrLf)
                        End If
                        CummulatedMessage += "Try: " & TryCount.ToString & " - " & ex.Message & vbCrLf
                        TryCount += 1
                        Threading.Thread.Sleep(SleepInterval)
                    Else
                        cmd.Transaction.Rollback()
                        Throw New Exception("SQLExecuteNonQuery Loop Exception: " & vbCrLf & ex.ToString)
                    End If
                End Try
            End While
            '---- Looping untill success or maxtry reached END------
        Catch ex As Threading.ThreadAbortException
        Catch e As Exception

            Throw New Exception("SQLExecuteNonQuery  Exception: " & vbCrLf & e.ToString & vbCrLf & "SQLString = " & SQLString & vbCrLf)
        Finally
            If Not cn Is Nothing Then cn.Dispose()
            If Not cmd Is Nothing Then cmd.Dispose()
        End Try

        Return recordsAffected
    End Function

    Public Function SQLExecuteQuery(ByRef SQLString As String, Optional ByVal PPN As Boolean = True, Optional ByVal ConnString As String = "", Optional ByVal DBMaxTry As Integer = 2, Optional ByVal SleepInterval As Integer = 250) As DataSet
        Dim con As New SqlConnection
        Dim da As New SqlDataAdapter
        Dim ds As New DataSet
        Dim TryCount As Integer
        Dim Success As Boolean
        Dim CummulatedMessage As String = ""

        Try
            If ConnString = "" Then
                If PPN Then
                    ConnString = ConnectionString
                Else
                    ConnString = ConnectionString2
                End If
            End If

            '---- Looping untill success or maxtry reached ------
            TryCount = 0
            Success = False
            While Not Success And TryCount <= DBMaxTry
                Try
                    con = New SqlConnection(ConnString)
                    da = New SqlDataAdapter(SQLString, con)
                    con.Open()
                    da.Fill(ds)
                    Success = True
                    Return ds
                Catch ex1 As Threading.ThreadAbortException
                Catch ex1 As Exception
                    'If ex1.Message.ToLower.IndexOf("network") <> -1 _
                    '    Or ex1.Message.ToLower.IndexOf("timeout") <> -1 _
                    '    Or ex1.Message.ToLower.IndexOf("access") <> -1 _
                    '    Or ex1.Message.ToLower.IndexOf("deadlocked") <> -1 Then
                    If TryCount >= DBMaxTry Then
                        Try
                            If Not con Is Nothing Then con.Dispose()
                            If Not da Is Nothing Then da.Dispose()
                            If PPN Then
                                ConnString = ConnectionString2
                            Else
                                ConnString = ConnectionString
                            End If

                            '---- Looping untill success or maxtry reached ------
                            TryCount = 0
                            While Not Success And TryCount <= DBMaxTry

                                Try
                                    con = New SqlConnection(ConnString)
                                    da = New SqlDataAdapter(SQLString, con)
                                    con.Open()
                                    da.Fill(ds)

                                    Success = True
                                    Return ds
                                Catch ex2 As Threading.ThreadAbortException
                                Catch ex2 As Exception
                                    If ex2.Message.ToLower.IndexOf("network") <> -1 _
                                        Or ex2.Message.ToLower.IndexOf("timeout") <> -1 _
                                        Or ex2.Message.ToLower.IndexOf("access") <> -1 _
                                        Or ex2.Message.ToLower.IndexOf("deadlocked") <> -1 Then
                                        If TryCount >= DBMaxTry Then
                                            Throw New Exception("SQLExecuteQuery2 - MaxTry Exceeded." & vbCrLf & "CummulatedMessage = " & CummulatedMessage & vbCrLf & "Trycount = " & TryCount.ToString & vbCrLf & _
                                                              "SQLString: " & SQLString & vbCrLf & "ConnString: " & vbCrLf & ConnString & vbCrLf)
                                        End If
                                        CummulatedMessage += "Try: " & TryCount.ToString & " - " & ex2.Message & vbCrLf
                                        TryCount += 1
                                        Threading.Thread.Sleep(SleepInterval)
                                    Else
                                        Throw New Exception("SQLExecuteQuery Loop Exception: " & vbCrLf & ex2.ToString)
                                    End If
                                End Try
                            End While
                            '---- Looping untill success or maxtry reached END------
                        Catch ex2 As Threading.ThreadAbortException
                        Catch e2 As Exception
                            Throw New Exception("SQLExecuteQuery Exception: " & vbCrLf & e2.ToString & vbCrLf & "SQLString = " & SQLString)
                        End Try
                    End If
                    CummulatedMessage += "Try: " & TryCount.ToString & " - " & ex1.Message & vbCrLf
                    TryCount += 1
                    Threading.Thread.Sleep(SleepInterval)
                    'Else
                    'Throw New Exception("SQLExecuteQuery Loop Exception: " & vbCrLf & ex1.ToString)
                    'End If
                End Try
            End While
            '---- Looping untill success or maxtry reached END------
        Catch ex1 As Threading.ThreadAbortException
        Catch e1 As Exception
            Throw New Exception("SQLExecuteQuery Exception: " & vbCrLf & e1.ToString & vbCrLf & "SQLString = " & SQLString)
        Finally
            If Not con Is Nothing Then con.Dispose()
            If Not da Is Nothing Then da.Dispose()
        End Try
        Return Nothing
    End Function

    Public Function SQLExecuteScalar(ByRef SQLString As String, Optional ByVal PPN As Boolean = True, Optional ByVal ConnString As String = "", Optional ByVal DBMaxTry As Integer = 2, Optional ByVal SleepInterval As Integer = 250) As String
        Dim cn As New SqlConnection
        Dim cmd As New SqlCommand
        Dim TryCount As Integer
        Dim Success As Boolean
        Dim CummulatedMessage As String = ""
        Dim Obj As Object
        Dim Result As String

        Try
            If ConnString = "" Then
                If PPN Then
                    ConnString = ConnectionString
                Else
                    ConnString = ConnectionString2
                End If
            End If

            '---- Looping untill success or maxtry reached ------
            TryCount = 0
            Success = False
            While Not Success And TryCount <= DBMaxTry
                Try
                    cn = New SqlConnection(ConnString)
                    cmd = New SqlCommand(SQLString, cn)
                    cn.Open()
                    Obj = cmd.ExecuteScalar
                    If IsDBNull(Obj) Then
                        Result = "0"
                    Else
                        If IsNothing(Obj) Then
                            Result = ""
                        Else
                            Result = Obj.ToString
                        End If
                    End If
                    Success = True

                    Return Result
                Catch ex1 As Threading.ThreadAbortException
                Catch ex1 As Exception
                    'If ex1.Message.ToLower.IndexOf("network") <> -1 _
                    '    Or ex1.Message.ToLower.IndexOf("timeout") <> -1 _
                    '    Or ex1.Message.ToLower.IndexOf("access") <> -1 _
                    '    Or ex1.Message.ToLower.IndexOf("deadlocked") <> -1 Then
                    If TryCount >= DBMaxTry Then
                        Try
                            If Not cn Is Nothing Then cn.Dispose()
                            If Not cmd Is Nothing Then cmd.Dispose()
                            If PPN Then
                                ConnString = ConnectionString2
                            Else
                                ConnString = ConnectionString
                            End If
                            '---- Looping untill success or maxtry reached ------
                            TryCount = 0
                            While Not Success
                                Try
                                    cn = New SqlConnection(ConnString)
                                    cmd = New SqlCommand(SQLString, cn)
                                    cn.Open()
                                    Obj = cmd.ExecuteScalar
                                    If IsDBNull(Obj) Then
                                        Result = "0"
                                    Else
                                        If IsNothing(Obj) Then
                                            Result = ""
                                        Else
                                            Result = Obj.ToString
                                        End If
                                    End If
                                    Success = True

                                    Return Result
                                Catch ex2 As Threading.ThreadAbortException
                                Catch ex2 As Exception
                                    If ex2.Message.ToLower.IndexOf("network") <> -1 _
                                        Or ex2.Message.ToLower.IndexOf("timeout") <> -1 _
                                        Or ex2.Message.ToLower.IndexOf("access") <> -1 _
                                        Or ex2.Message.ToLower.IndexOf("deadlocked") <> -1 Then
                                        If TryCount >= DBMaxTry Then
                                            Throw New Exception("SQLExecuteScalar - MaxTry Exceeded." & vbCrLf & "CummulatedMessage = " & CummulatedMessage & vbCrLf & "Trycount = " & TryCount.ToString & _
                                                      "SQL: " & SQLString & vbCrLf)
                                        End If
                                        CummulatedMessage += "Try: " & TryCount.ToString & " - " & ex2.Message & vbCrLf
                                        TryCount += 1
                                        Threading.Thread.Sleep(SleepInterval)
                                    Else
                                        Throw New Exception("SQLExecuteScalar2 Loop Exception: " & vbCrLf & ex2.ToString)
                                    End If
                                End Try
                            End While
                            '---- Looping untill success or maxtry reached END------

                        Catch ex2 As Threading.ThreadAbortException
                        Catch e2 As Exception
                            Throw New Exception("SQLExecuteScalar2  Exception: " & vbCrLf & e2.ToString & vbCrLf & "SQLString = " & SQLString & vbCrLf)
                        End Try
                    End If
                    CummulatedMessage += "Try: " & TryCount.ToString & " - " & ex1.Message & vbCrLf
                    TryCount += 1
                    Threading.Thread.Sleep(SleepInterval)
                    'Else
                    '    Throw New Exception("SQLExecuteScalar Loop Exception: " & vbCrLf & ex1.ToString)
                    'End If
                End Try
            End While
            '---- Looping untill success or maxtry reached END------

        Catch ex1 As Threading.ThreadAbortException
        Catch e1 As Exception
            Throw New Exception("SQLExecuteScalar  Exception: " & vbCrLf & e1.ToString & vbCrLf & "SQLString = " & SQLString & vbCrLf)
        Finally
            If Not cn Is Nothing Then cn.Dispose()
            If Not cmd Is Nothing Then cmd.Dispose()
        End Try
        Return Nothing
    End Function

    Public Function SQLConnectionString(Optional ByVal DBMaxTry As Integer = 2, Optional ByVal SleepInterval As Integer = 250) As String
        Dim con As New SqlConnection
        Dim ConnString As String
        Dim TryCount As Integer
        Dim Success As Boolean
        Dim CummulatedMessage As String = ""
        Try
            ConnString = ConnectionString
            '---- Looping untill success or maxtry reached ------
            TryCount = 0
            Success = False
            While Not Success And TryCount <= DBMaxTry
                Try
                    con = New SqlConnection(ConnString)
                    'da = New SqlDataAdapter(SQLString, con)
                    con.Open()
                    'da.Fill(ds)
                    Success = True
                    If Not con Is Nothing Then con.Dispose()
                    'If Not da Is Nothing Then da.Dispose()
                    Return ConnString
                Catch ex1 As Threading.ThreadAbortException
                Catch ex1 As Exception
                    If TryCount >= DBMaxTry Then
                        ConnString = ConnectionString2
                        TryCount = 0
                        Success = False
                        While Not Success And TryCount <= DBMaxTry
                            Try
                                con = New SqlConnection(ConnString)
                                'da = New SqlDataAdapter(SQLString, con)
                                con.Open()
                                'da.Fill(ds)
                                Success = True
                                'If Not con Is Nothing Then con.Dispose()
                                'If Not da Is Nothing Then da.Dispose()
                                Return ConnString
                            Catch ex2 As Threading.ThreadAbortException
                            Catch ex2 As Exception
                                If TryCount >= DBMaxTry Then
                                    Throw New Exception("SQLExecuteNonQuery - MaxTry Exceeded." & vbCrLf & "CummulatedMessage = " & CummulatedMessage & vbCrLf & "Trycount = " & TryCount.ToString)
                                End If
                                CummulatedMessage += "Try: " & TryCount.ToString & " - " & ex2.Message & vbCrLf
                                TryCount += 1
                                Threading.Thread.Sleep(SleepInterval)
                            End Try
                        End While
                    End If
                    CummulatedMessage += "Try: " & TryCount.ToString & " - " & ex1.Message & vbCrLf
                    TryCount += 1
                    Threading.Thread.Sleep(SleepInterval)
                End Try
            End While
            '---- Looping untill success or maxtry reached END------
        Catch ex1 As Threading.ThreadAbortException
        Catch e1 As Exception
            Throw New Exception("SQLExecuteQuery Exception: " & vbCrLf & e1.ToString)
        Finally
            If Not con Is Nothing Then con.Dispose()
            'If Not da Is Nothing Then da.Dispose()
        End Try
        Return Nothing
    End Function

#End Region



    'SQL EXECUTE LAMA
    'Public Function SQLExecuteNonQuery(ByRef SQLString As String, Optional ByVal ConnString As String = "", Optional ByVal DBMaxTry As Integer = 5, Optional ByVal SleepInterval As Integer = 1000) As Long
    '    Dim cn As New SqlConnection
    '    Dim cmd As New SqlCommand
    '    Dim recordsAffected As Long = 0
    '    Dim TryCount As Integer
    '    Dim Success As Boolean
    '    Dim CummulatedMessage As String = ""

    '    Try
    '        If ConnString = "" Then
    '            ConnString = ConnectionString
    '        End If

    '        '---- Looping untill success or maxtry reached ------
    '        TryCount = 0
    '        Success = False
    '        While Not Success
    '            Try
    '                cn = New SqlConnection(ConnString)
    '                cmd = New SqlCommand(SQLString, cn)
    '                cn.Open()
    '                recordsAffected = cmd.ExecuteNonQuery()
    '                Success = True

    '                Return recordsAffected

    '            Catch ex As Threading.ThreadAbortException
    '            Catch ex As Exception
    '                If ex.Message.ToLower.IndexOf("network") <> -1 _
    '                    Or ex.Message.ToLower.IndexOf("timeout") <> -1 _
    '                    Or ex.Message.ToLower.IndexOf("access") <> -1 _
    '                    Or ex.Message.ToLower.IndexOf("deadlocked") <> -1 Then
    '                    If TryCount >= DBMaxTry Then
    '                        Throw New Exception("SQLExecuteNonQuery - MaxTry Exceeded." & vbCrLf & "CummulatedMessage = " & CummulatedMessage & vbCrLf & "Trycount = " & TryCount.ToString & _
    '                                  "SQL: " & SQLString & vbCrLf)
    '                    End If
    '                    CummulatedMessage += "Try: " & TryCount.ToString & " - " & ex.Message & vbCrLf
    '                    TryCount += 1
    '                    Threading.Thread.Sleep(SleepInterval)
    '                Else
    '                    Throw New Exception("SQLExecuteNonQuery Loop Exception: " & vbCrLf & ex.ToString)
    '                End If
    '            End Try
    '        End While
    '        '---- Looping untill success or maxtry reached END------

    '    Catch ex As Threading.ThreadAbortException
    '    Catch e As Exception
    '        Throw New Exception("SQLExecuteNonQuery  Exception: " & vbCrLf & e.ToString & vbCrLf & "SQLString = " & SQLString & vbCrLf)
    '    Finally
    '        If Not cn Is Nothing Then cn.Dispose()
    '        If Not cmd Is Nothing Then cmd.Dispose()
    '    End Try
    'End Function

    'Public Function SQLExecuteNonQuerytransaction(ByRef SQLString As String, Optional ByVal ConnString As String = "", Optional ByVal DBMaxTry As Integer = 5, Optional ByVal SleepInterval As Integer = 1000) As Long
    '    Dim cn As New SqlConnection
    '    Dim cmd As New SqlCommand
    '    Dim recordsAffected As Long = 0
    '    Dim TryCount As Integer
    '    Dim Success As Boolean
    '    Dim CummulatedMessage As String = ""

    '    Try
    '        If ConnString = "" Then
    '            ConnString = ConnectionString
    '        End If

    '        '---- Looping untill success or maxtry reached ------
    '        TryCount = 0
    '        Success = False
    '        While Not Success
    '            Try
    '                cn = New SqlConnection(ConnString)
    '                cmd = New SqlCommand(SQLString, cn)
    '                cn.Open()
    '                cmd.Transaction = cn.BeginTransaction
    '                recordsAffected = cmd.ExecuteNonQuery()
    '                Success = True
    '                cmd.Transaction.Commit()
    '                Return recordsAffected
    '            Catch ex As Threading.ThreadAbortException
    '            Catch ex As Exception
    '                If ex.Message.ToLower.IndexOf("network") <> -1 _
    '                        Or ex.Message.ToLower.IndexOf("timeout") <> -1 _
    '                        Or ex.Message.ToLower.IndexOf("access") <> -1 _
    '                        Or ex.Message.ToLower.IndexOf("deadlocked") <> -1 Then
    '                    If TryCount >= DBMaxTry Then
    '                        Throw New Exception("SQLExecuteNonQuery - MaxTry Exceeded." & vbCrLf & "CummulatedMessage = " & CummulatedMessage & vbCrLf & "Trycount = " & TryCount.ToString & _
    '                                            "SQL: " & SQLString & vbCrLf)
    '                    End If
    '                    CummulatedMessage += "Try: " & TryCount.ToString & " - " & ex.Message & vbCrLf
    '                    TryCount += 1
    '                    Threading.Thread.Sleep(SleepInterval)
    '                Else
    '                    'kalo nggk masuk di roolback 
    '                    cmd.Transaction.Rollback()
    '                    Throw New Exception("SQLExecuteNonQuery Loop Exception: " & vbCrLf & ex.ToString)
    '                End If
    '            End Try
    '        End While
    '        '---- Looping untill success or maxtry reached END------

    '    Catch ex As Threading.ThreadAbortException
    '    Catch e As Exception
    '        Throw New Exception("SQLExecuteNonQuery  Exception: " & vbCrLf & e.ToString & vbCrLf & "SQLString = " & SQLString & vbCrLf)
    '    Finally
    '        If Not cn Is Nothing Then cn.Dispose()
    '        If Not cmd Is Nothing Then cmd.Dispose()
    '    End Try
    'End Function

    'Public Function SQLExecuteQuery(ByRef SQLString As String, Optional ByVal ConnString As String = "", Optional ByVal DBMaxTry As Integer = 5, Optional ByVal SleepInterval As Integer = 1000) As DataSet
    '    Dim con As New SqlConnection
    '    Dim da As New SqlDataAdapter
    '    Dim ds As New DataSet
    '    Dim TryCount As Integer
    '    Dim Success As Boolean
    '    Dim CummulatedMessage As String = ""

    '    Try
    '        If ConnString = "" Then
    '            ConnString = ConnectionString
    '        End If
    '        'ConnString = Decrypt(ConnString.ToString, Key.ToString)
    '        '---- Looping untill success or maxtry reached ------
    '        TryCount = 0
    '        Success = False
    '        While Not Success
    '            Try
    '                con = New SqlConnection(ConnString)
    '                da = New SqlDataAdapter(SQLString, con)
    '                con.Open()
    '                da.Fill(ds)

    '                Success = True
    '                Return ds
    '            Catch ex As Threading.ThreadAbortException
    '            Catch ex As Exception
    '                If ex.Message.ToLower.IndexOf("network") <> -1 _
    '                        Or ex.Message.ToLower.IndexOf("timeout") <> -1 _
    '                        Or ex.Message.ToLower.IndexOf("access") <> -1 _
    '                        Or ex.Message.ToLower.IndexOf("deadlocked") <> -1 Then
    '                    If TryCount >= DBMaxTry Then
    '                        Throw New Exception("SQLExecuteQuery - MaxTry Exceeded." & vbCrLf & "CummulatedMessage = " & CummulatedMessage & vbCrLf & "Trycount = " & TryCount.ToString & vbCrLf & _
    '                                                            "SQLString: " & SQLString & vbCrLf & "ConnString: " & vbCrLf & ConnString & vbCrLf)
    '                    End If
    '                    CummulatedMessage += "Try: " & TryCount.ToString & " - " & ex.Message & vbCrLf
    '                    TryCount += 1
    '                    Threading.Thread.Sleep(SleepInterval)
    '                Else
    '                    Throw New Exception("SQLExecuteQuery Loop Exception: " & vbCrLf & ex.ToString)
    '                End If
    '            End Try
    '        End While
    '        '---- Looping untill success or maxtry reached END------
    '    Catch ex As Threading.ThreadAbortException
    '    Catch e As Exception
    '        Throw New Exception("SQLExecuteQuery Exception: " & vbCrLf & e.ToString & vbCrLf & "SQLString = " & SQLString)
    '    Finally
    '        If Not con Is Nothing Then con.Dispose()
    '        If Not da Is Nothing Then da.Dispose()
    '    End Try
    '    Return Nothing
    'End Function

    'Public Function SQLExecuteScalar(ByRef SQLString As String, Optional ByVal ConnString As String = "", Optional ByVal DBMaxTry As Integer = 5, Optional ByVal SleepInterval As Integer = 1000) As String
    '    Dim cn As New SqlConnection
    '    Dim cmd As New SqlCommand
    '    Dim TryCount As Integer
    '    Dim Success As Boolean
    '    Dim CummulatedMessage As String = ""
    '    Dim Obj As Object
    '    Dim Result As String

    '    Try
    '        If ConnString = "" Then
    '            ConnString = ConnectionString
    '        End If
    '        'ConnString = Decrypt(ConnString.ToString, Key.ToString)
    '        '---- Looping untill success or maxtry reached ------
    '        TryCount = 0
    '        Success = False
    '        While Not Success
    '            Try
    '                cn = New SqlConnection(ConnString)
    '                cmd = New SqlCommand(SQLString, cn)
    '                cn.Open()
    '                Obj = cmd.ExecuteScalar
    '                If IsDBNull(Obj) Then
    '                    Result = "0"
    '                Else
    '                    If IsNothing(Obj) Then
    '                        Result = ""
    '                    Else
    '                        Result = Obj.ToString
    '                    End If
    '                End If
    '                Success = True

    '                Return Result
    '            Catch ex As Threading.ThreadAbortException
    '            Catch ex As Exception
    '                If ex.Message.ToLower.IndexOf("network") <> -1 _
    '                        Or ex.Message.ToLower.IndexOf("timeout") <> -1 _
    '                        Or ex.Message.ToLower.IndexOf("access") <> -1 _
    '                        Or ex.Message.ToLower.IndexOf("deadlocked") <> -1 Then
    '                    If TryCount >= DBMaxTry Then
    '                        Throw New Exception("SQLExecuteScalar - MaxTry Exceeded." & vbCrLf & "CummulatedMessage = " & CummulatedMessage & vbCrLf & "Trycount = " & TryCount.ToString & _
    '                                            "SQL: " & SQLString & vbCrLf)
    '                    End If
    '                    CummulatedMessage += "Try: " & TryCount.ToString & " - " & ex.Message & vbCrLf
    '                    TryCount += 1
    '                    Threading.Thread.Sleep(SleepInterval)
    '                Else
    '                    Throw New Exception("SQLExecuteScalar Loop Exception: " & vbCrLf & ex.ToString)
    '                End If
    '            End Try
    '        End While
    '        '---- Looping untill success or maxtry reached END------

    '    Catch ex As Threading.ThreadAbortException
    '    Catch e As Exception
    '        Throw New Exception("SQLExecuteScalar  Exception: " & vbCrLf & e.ToString & vbCrLf & "SQLString = " & SQLString & vbCrLf)
    '    Finally
    '        If Not cn Is Nothing Then cn.Dispose()
    '        If Not cmd Is Nothing Then cmd.Dispose()
    '    End Try
    '    Return Nothing
    'End Function

    Public Function FormatDate(ByVal Input As String) As String
        Dim Value As String = ""
        Dim VMonth, VDay, VYear As String
        Dim Index As Integer
        Try
            If Input.Contains("/") = False Then
                Return Input
            End If

            'modify needed using split("/")
            If Input.Contains("/") Then
                Index = Input.IndexOf("/")
                VDay = Input.Substring(0, Index)
                If VDay.Length = 1 Then
                    VDay = "0" & VDay
                End If
                Input = Input.Substring(Index + 1)

                Index = Input.IndexOf("/")
                VMonth = Input.Substring(0, Index)
                If VMonth.Length = 1 Then
                    VMonth = "0" & VMonth
                End If
                Input = Input.Substring(Index + 1)
                VYear = Input
                If VYear.ToString.Length = 2 Then
                    VYear = "20" + VYear
                End If
                Value = VMonth + "/" + VDay + "/" + VYear
            Else
                Index = Input.IndexOf("-")
                VDay = Input.Substring(0, Index)
                If VDay.Length = 1 Then
                    VDay = "0" & VDay
                End If
                Input = Input.Substring(Index + 1)

                Index = Input.IndexOf("-")
                VMonth = Input.Substring(0, Index)
                If VMonth.ToString = 1 Then
                    VMonth = "0" & VMonth
                End If
                Input = Input.Substring(Index + 1)
                VYear = Input
                If VYear.Length = 2 Then
                    VYear = "20" + VYear
                End If
                Value = VMonth + "/" + VDay + "/" + VYear
            End If

            Return Value
        Catch ex As Exception
            Throw New Exception("FormatDate Exception: " & vbCrLf & ex.ToString & vbCrLf & "Input: " & Input.ToString)
        End Try
    End Function

    Public Function Bilangan(ByVal value As String) As String

        Dim angka, RatusanSen As String()
        Dim Result As String = ""
        Dim trilyunan, Milyardan, Ratusan, Ribuan, Jutaan, senan As String
        Dim trilyun, Milyar, Juta, Ribu, Ratus, sen As String

        Try
            trilyunan = ""
            Milyardan = ""
            Ratusan = ""
            Ribuan = ""
            Jutaan = ""
            senan = ""
            sen = ""
            angka = value.ToString.Split(",")
            countzero = 0
            Select Case angka.Length
                Case 5
                    Ratusan = angka(4).ToString
                    Ribuan = angka(3).ToString
                    Jutaan = angka(2).ToString
                    Milyardan = angka(1).ToString
                    trilyunan = angka(0).ToString
                Case 4
                    Ratusan = angka(3).ToString
                    Ribuan = angka(2).ToString
                    Jutaan = angka(1).ToString
                    Milyardan = angka(0).ToString
                Case 3
                    Ratusan = angka(2).ToString
                    Ribuan = angka(1).ToString
                    Jutaan = angka(0).ToString
                Case 2
                    Ratusan = angka(1).ToString
                    Ribuan = angka(0).ToString
                Case 1
                    Ratusan = angka(0).ToString
            End Select
            If Ratusan.ToString.Contains(".") Then
                RatusanSen = Ratusan.ToString.Split(".")
                Ratusan = RatusanSen(0).ToString
                senan = RatusanSen(1).ToString
                sen = Baca(senan.ToString, 0)
            End If
            trilyun = Baca(trilyunan.ToString, 0)
            Milyar = Baca(Milyardan.ToString, 0)
            Juta = Baca(Jutaan.ToString, 0)
            Ribu = Baca(Ribuan.ToString, 1)
            Ratus = Baca(Ratusan.ToString, 0)

            If trilyun.ToString.Trim <> "" Then
                Result = trilyun.ToString.Trim & " Trilyun "
            End If
            If Milyar.ToString.Trim <> "" Then
                Result &= Milyar.ToString.Trim & " Milyar "
            End If
            If Juta.ToString.Trim <> "" Then
                Result &= Juta.ToString.Trim & " Juta "
            End If
            If Ribu.ToString.Trim <> "" Then
                Result &= Ribu.ToString.Trim & "Ribu "
            End If
            Result &= Ratus.ToString

            Result = Result.ToString.Trim.ToUpper & " RUPIAH"
            If sen.ToString.Trim <> "" Then
                Result = Result.ToString.Trim & " " & sen.ToString.ToUpper & "SEN"
            End If

            Return Result
        Catch ex As Exception
            Throw New Exception("Bilangan Exception: " & vbCrLf & ex.ToString)
        End Try
    End Function
    Public Function bilanganbiasa(ByVal value As String) As String
        Dim result As String = ""
        Try

        Catch ex As Exception
            Throw New Exception("Error function bilanganbiasa :" & ex.ToString)
        End Try

        Return result
    End Function
    Public Function Bilangan2(ByVal value As String) As String

        Dim angka, RatusanSen As String()
        Dim Result As String = ""
        Dim trilyunan, Milyardan, Ratusan, Ribuan, Jutaan, senan As String
        Dim trilyun, Milyar, Juta, Ribu, Ratus, sen As String

        Try
            trilyunan = ""
            Milyardan = ""
            Ratusan = ""
            Ribuan = ""
            Jutaan = ""
            senan = ""
            sen = ""
            angka = value.ToString.Split(",")
            countzero = 0
            Select Case angka.Length
                Case 5
                    Ratusan = angka(4).ToString
                    Ribuan = angka(3).ToString
                    Jutaan = angka(2).ToString
                    Milyardan = angka(1).ToString
                    trilyunan = angka(0).ToString
                Case 4
                    Ratusan = angka(3).ToString
                    Ribuan = angka(2).ToString
                    Jutaan = angka(1).ToString
                    Milyardan = angka(0).ToString
                Case 3
                    Ratusan = angka(2).ToString
                    Ribuan = angka(1).ToString
                    Jutaan = angka(0).ToString
                Case 2
                    Ratusan = angka(1).ToString
                    Ribuan = angka(0).ToString
                Case 1
                    Ratusan = angka(0).ToString
            End Select
            If Ratusan.ToString.Contains(".") Then
                RatusanSen = Ratusan.ToString.Split(".")
                Ratusan = RatusanSen(0).ToString
                senan = RatusanSen(1).ToString
                sen = Baca(senan.ToString, 0)
            End If
            trilyun = Baca(trilyunan.ToString, 0)
            Milyar = Baca(Milyardan.ToString, 0)
            Juta = Baca(Jutaan.ToString, 0)
            Ribu = Baca(Ribuan.ToString, 1)
            Ratus = Baca(Ratusan.ToString, 0)

            If trilyun.ToString.Trim <> "" Then
                Result = trilyun.ToString.Trim & " Trilyun "
            End If
            If Milyar.ToString.Trim <> "" Then
                Result &= Milyar.ToString.Trim & " Milyar "
            End If
            If Juta.ToString.Trim <> "" Then
                Result &= Juta.ToString.Trim & " Juta "
            End If
            If Ribu.ToString.Trim <> "" Then
                Result &= Ribu.ToString.Trim & " Ribu "
            End If
            Result &= Ratus.ToString

            Result = Result.ToString.Trim.ToUpper
            If sen.ToString.Trim <> "" Then
                Result = Result.ToString.Trim & " " & senan.ToString & "/100 "
            End If

            Return Result
        Catch ex As Exception
            Throw New Exception("Bilangan2 Exception: " & vbCrLf & ex.ToString)
        End Try
    End Function




    Private Function Baca(ByVal angka As String, ByVal id As Integer) As String
        Dim pecahan As String = ""
        Dim i, j, indicator As Integer
        Dim Hasil As String
        Dim Ratusan(3) As String
        Try
            Hasil = ""
            indicator = 0
            Ratusan(0) = " "
            Ratusan(1) = " "
            Ratusan(2) = " "
            If id = 1 Then
                Ratusan(0) = "0"
                Ratusan(1) = "0"
                Ratusan(2) = "0"
            End If
            j = 0
            For i = (3 - angka.Length) To 2
                Ratusan(i) = angka.ToString.Substring(j, 1)
                j = j + 1
            Next
            For i = 0 To 2
                If Ratusan(i).ToString <> " " Then
                    If Ratusan(i).ToString = "1" And i = 1 Then
                        indicator = 1
                    ElseIf Ratusan(i).ToString = "0" And i = 2 Then
                        If indicator = 1 Then
                            indicator = 2
                        End If
                        Hasil &= cekAngka(Ratusan(i), i, indicator, id) & " "
                    Else
                        Hasil &= cekAngka(Ratusan(i), i, indicator, id) & " "
                        indicator = 0
                    End If
                End If
            Next
            Return Hasil.ToString
        Catch ex As Exception
            Throw New Exception("Baca Exceptions : " & vbCrLf & ex.ToString)
        End Try
    End Function

    Private Function cekAngka(ByVal Number As String, ByVal index As Integer, ByVal indicator As Integer, ByVal id As Integer) As String
        Dim value As String = ""
        Try
            If id = 1 Then
                If Number.ToString = "0" Then
                    countzero += 1
                End If
            End If
            Select Case Number.ToString.Trim
                Case "1"
                    If index < 2 Then
                        value = "Se"
                    Else
                        If id = 0 Then
                            value = "Satu"
                        ElseIf id = 1 Then
                            If countzero = 2 Then
                                value = "Se"
                            Else
                                value = "Satu "
                            End If
                        End If
                        If indicator = 1 Then
                            value = "Sebelas "
                        End If
                    End If
                Case "2"
                    value = "Dua "
                    If indicator = 1 Then
                        value &= "Belas "
                    End If

                Case "3"
                    value = "Tiga "
                    If indicator = 1 Then
                        value &= "Belas "
                    End If

                Case "4"
                    value = "Empat "
                    If indicator = 1 Then
                        value &= "Belas "
                    End If

                Case "5"
                    value = "Lima "
                    If indicator = 1 Then
                        value &= "Belas "
                    End If

                Case "6"
                    value = "Enam "
                    If indicator = 1 Then
                        value &= "Belas "
                    End If

                Case "7"
                    value = "Tujuh "
                    If indicator = 1 Then
                        value &= "Belas "
                    End If

                Case "8"
                    value = "Delapan "
                    If indicator = 1 Then
                        value &= "Belas "
                    End If

                Case "9"
                    value = "Sembilan "
                    If indicator = 1 Then
                        value &= "Belas "
                    End If

                Case "0"
                    If indicator = 1 Then
                        If index = 2 Then
                            value = "Puluh"
                        End If
                    End If
                    If indicator = 2 Then
                        If index = 2 Then
                            value = "Sepuluh "
                        End If
                    End If
            End Select

            Select Case index.ToString
                Case "0"
                    If Number.ToString.Trim <> "0" Then
                        value &= "Ratus"
                    End If
                Case "1"
                    If Number.ToString.Trim <> "0" Then
                        value &= "puluh"
                    End If
            End Select

            Return value.ToString
        Catch ex As Exception
            Throw New Exception("cekAngka Exceptions : " & vbCrLf & ex.ToString)
        End Try
    End Function
    Public Function check_server_connection(Optional ByVal ConnString As String = "") As Boolean

        If ConnString = "" Then
            ConnString = ConnectionString
        End If

        Try
            Using cn As New SqlConnection(ConnString)
                cn.Open()

                If cn.State = ConnectionState.Open Then
                    cn.Close()
                    Return True
                Else
                    Return False
                End If
            End Using
        Catch ex As Exception
            Return False
        End Try
        Return False
    End Function

    Public Function DisplayPhone(ByVal Area As String, ByVal Phone As String) As String
        Dim result As String = ""

        If Phone.Length < 4 Then
            result = "(" & Area & ")" & "-" & Phone
        Else
            Dim SubPhone As String = Phone.Substring(0, Phone.Length - 4)

            result = "(" & Area & ")" & "-" & SubPhone & "-" & Right(Phone, 4)
        End If



        Return result
    End Function
    Public Function ambilTahunAkhir() As String
        Dim tahun As String
        Dim cekstring As String

        Try
            cekstring = "select TOP 1 Tahun from TahunTutup where Closed = 'True' and status = 1 order by Tahun Desc"
            tahun = SQLExecuteScalar(cekstring)

        Catch ex As Exception
            Throw New Exception("error function ambilTanggalAkhir :" & ex.ToString)
        End Try
        Return tahun
    End Function

    Public Function recalculateSaldo(ByVal tahunAwal As Integer, ByVal looping As Integer, ByVal id As String) As Boolean
        Dim sqlstring As String
        Dim cdt As DataTable
        Dim cekstring As String
        Dim tahunUpdate As Integer
        Dim DorC As String
        Dim hasil As String
        Dim tanggalcek As String
        Dim tanggalakhir As String

        Try
            tahunUpdate = tahunAwal
            For i As Integer = 0 To looping
                sqlstring = ""
                tahunUpdate = tahunUpdate + 1
                tanggalcek = "01/01/" + CStr(tahunUpdate - 1)
                tanggalakhir = "12/31/" + CStr(tahunUpdate - 1)
                cekstring = "select A.Code As AccountNo,AN.AccName,A.[Types], UPPER(A.Name) as AccountName ," & _
                                       "ISNULL((select case A.Code " & _
                                       "when '0003.311.002.00'  " & _
                                       "then ( " & _
                                         "(abs(ISNULL(( SELECT SUM(Amount) FROM V_Journal_View WHERE Class='0004' AND TransDate Between '" & tanggalcek & "' and '" & tanggalakhir & "'), 0))  " & _
                                       "-" & _
                                       "abs(ISNULL(( SELECT SUM(Amount) FROM V_Journal_View WHERE Class='0005' AND TransDate Between '" & tanggalcek & "' and '" & tanggalakhir & "'), 0))  " & _
                                       "+ " & _
                                       "abs(ISNULL(( SELECT SUM(Amount) FROM V_Journal_View WHERE Class='0006' AND TransDate Between '" & tanggalcek & "' and '" & tanggalakhir & "'), 0))  " & _
                                        "+ " & _
                                       "abs(ISNULL(( SELECT Amount FROM V_Journal_View WHERE (Parent = A.Code or AccCode = A.code) AND TransDate Between '" & tanggalcek & "' and '" & tanggalakhir & "'), 0)))  " & _
                                       " * - 1)" & _
                                       "else ( SELECT SUM(Amount) FROM V_Journal_View WHERE (Parent = A.Code or AccCode = A.Code) AND TransDate Between '" & tanggalcek & "' and '" & tanggalakhir & "')" & _
                                       "end ),0) as Total ," & _
                                       "ISNULL((select case A.[Types] " & _
                                       "		When '0001' then ('Assets')" & _
                                       "		When '0002' then ('Assets')	" & _
                                       "		When '0003' then ('modal&equity')" & _
                                       "		When '0007' then ('modal&equity') end" & _
                                       "		),0) as jenis " & _
                                       "from ChartOfAccount A,AccountName AN where A.[Types] = AN.AccCode and (A.[Types] between '0001' and '0003' or A.[Types] ='0007')" & _
                                       "and A.Levels=2 Order by A.Code "
                cdt = SQLExecuteQuery(cekstring).Tables(0)

                If cdt.Rows.Count > 0 Then
                    For e As Integer = 0 To cdt.Rows.Count - 1
                        With cdt.Rows(e)
                            If CInt(.Item("Total").ToString) < 0 Then
                                DorC = "Credit"
                            Else
                                DorC = "Debit"
                            End If
                            sqlstring &= "Update JournalAwal set " & _
                                                    "DorC = '" & DorC.ToString & "', " & _
                                                    "Amount = '" & Math.Abs(CInt(.Item("Total").ToString)) & "' " & _
                                                    "where AccCode = '" & .Item("AccountNo").ToString & "' and GJNO = '" & tahunUpdate & "'  ;"
                        End With
                    Next
                End If

                cekstring = "select GJNO from GeneralJournal where Tanggal between '" & tanggalcek & "' and '" & tanggalakhir & "' and (status = 1 or status = 10)"
                cdt = SQLExecuteQuery(cekstring).Tables(0)
                If cdt.Rows.Count > 0 Then
                    For h As Integer = 0 To cdt.Rows.Count - 1
                        With cdt.Rows(h)
                            sqlstring &= "Update GeneralJournal set " & _
                                       " status = 10 , " & _
                                       " LastModified = '" & Now.ToString & "' , " & _
                                       " UserName = '" & id.ToString & "' " & _
                                       " where GJNO = '" & .Item("GJNO").ToString & "' and status = 1 ;"

                            sqlstring &= "Update  GeneralJournalDetail set" & _
                                                    " status = 10 , " & _
                                                   " LastModified = '" & Now.ToString & "' , " & _
                                                   " UserName = '" & id.ToString & "' " & _
                                                   " where GJNO = '" & .Item("GJNO").ToString & "' and status = 1 ;"

                        End With
                    Next
                End If
                sqlstring &= "Update JournalAwal set " & _
                                                   " status = 10 , " & _
                                                   " LastModified = '" & Now.ToString & "' , " & _
                                                   " UserName = '" & id.ToString & "' " & _
                                                   " where Tanggal between '" & tanggalcek & "' and '" & tanggalakhir & "' and status = 1 ;"

                sqlstring &= "Update JournalPenyusutan set " & _
                                        " status = 10 , " & _
                                        " LastModified = '" & Now.ToString & "' , " & _
                                        " UserName = '" & id.ToString & "' " & _
                                        " where Tanggal between '" & tanggalcek & "' and '" & tanggalakhir & "' and status = 1;"
                hasil = SQLExecuteNonQuery(sqlstring)
            Next

        Catch ex As Exception
            Throw New Exception("Error recalculate saldo :" & ex.ToString)
        End Try
        Return True
    End Function


    Public Function PindahkanBuku(ByVal tahunAwal As Integer, ByVal id As String) As Boolean
        Dim sqlstring As String = ""
        Dim cekstring As String
        Dim cdt As DataTable
        Dim tanggal As String
        Dim DorC As String
        Dim hasil As String
        Dim tanggalcek As String
        Dim tanggalakhir As String
        Dim IDDEtail As String

        Try

            tanggal = "01/01/" + CStr(tahunAwal + 1)
            tanggalcek = "01/01/" + CStr(tahunAwal)
            tanggalakhir = "12/31/" + CStr(tahunAwal)
            cekstring = "select A.Code As AccountNo,AN.AccName,A.[Types], UPPER(A.Name) as AccountName ," & _
                                       "ISNULL((select case A.Code " & _
                                       "when '0003.311.002.00'  " & _
                                       "then ( " & _
                                       "(abs(ISNULL(( SELECT SUM(Amount) FROM V_Journal WHERE Class='0004' AND TransDate Between '" & tanggalcek & "' and '" & tanggalakhir & "'), 0))  " & _
                                       "-" & _
                                       "abs(ISNULL(( SELECT SUM(Amount) FROM V_Journal WHERE Class='0005' AND TransDate Between '" & tanggalcek & "' and '" & tanggalakhir & "'), 0))  " & _
                                       "+ " & _
                                       "abs(ISNULL(( SELECT SUM(Amount) FROM V_Journal WHERE Class='0006' AND TransDate Between '" & tanggalcek & "' and '" & tanggalakhir & "'), 0))  " & _
                                        "+ " & _
                                       "abs(ISNULL(( SELECT Amount FROM V_Journal WHERE (Parent = A.Code or AccCode = A.code) AND TransDate Between '" & tanggalcek & "' and '" & tanggalakhir & "'), 0)))  " & _
                                       " * - 1)" & _
                                       "else ( SELECT SUM(Amount) FROM V_Journal WHERE (Parent = A.Code or AccCode = A.Code) AND TransDate Between '" & tanggalcek & "' and '" & tanggalakhir & "')" & _
                                       "end ),0) as Total ," & _
                                       "ISNULL((select case A.[Types] " & _
                                       "		When '0001' then ('Assets')" & _
                                       "		When '0002' then ('Assets')	" & _
                                       "		When '0003' then ('modal&equity')" & _
                                       "		When '0007' then ('modal&equity') end" & _
                                       "		),0) as jenis " & _
                                       "from ChartOfAccount A,AccountName AN where A.[Types] = AN.AccCode and (A.[Types] between '0001' and '0003' or A.[Types] ='0007')" & _
                                       "and A.Levels=2 Order by A.Code "

            cdt = SQLExecuteQuery(cekstring).Tables(0)

            cekstring = "SELECT TOP 1 IDDetail from JournalAwal ORDER BY ID DESC"
            IDDEtail = SQLExecuteScalar(cekstring)

            If IDDEtail = "" Then
                IDDEtail = "0"
            End If

            If cdt.Rows.Count > 0 Then
                For i As Integer = 0 To cdt.Rows.Count - 1
                    With cdt.Rows(i)
                        If CInt(.Item("Total").ToString) < 0 Then
                            DorC = "Credit"
                        Else
                            DorC = "Debit"
                        End If

                        IDDEtail = CInt(IDDEtail) + 1

                        sqlstring &= " insert into JournalAwal (IDDetail, tanggal,AccCode,GJNO,DorC,Amount,Description,UserName,status) values(" & _
                                                " " & CInt(IDDEtail) & ", '" & tanggal.ToString & "', '" & .Item("AccountNo").ToString & "','" & tahunAwal + 1 & "',  " & _
                                                " '" & DorC.ToString & "', '" & Math.Abs(CInt(.Item("Total").ToString)) & "', 'Saldo Awal', '" & id.ToString & "', 1 );"

                    End With
                Next
                cekstring = "select GJNO from GeneralJournal where Tanggal between '" & tanggalcek & "' and '" & tanggalakhir & "' and (status = 1 or status = 10)"
                cdt = SQLExecuteQuery(cekstring).Tables(0)
                If cdt.Rows.Count > 0 Then
                    For h As Integer = 0 To cdt.Rows.Count - 1
                        With cdt.Rows(h)
                            sqlstring &= "Update GeneralJournal set " & _
                                       " status = 10 , " & _
                                       " LastModified = '" & Now.ToString & "' , " & _
                                       " UserName = '" & id.ToString & "' " & _
                                       " where GJNO = '" & .Item("GJNO").ToString & "' and status = 1 ;"

                            sqlstring &= "Update  GeneralJournalDetail set" & _
                                                    " status = 10 , " & _
                                                   " LastModified = '" & Now.ToString & "' , " & _
                                                   " UserName = '" & id.ToString & "' " & _
                                                   " where GJNO = '" & .Item("GJNO").ToString & "' and status = 1 ;"

                        End With
                    Next
                End If
                sqlstring &= "Update JournalAwal set " & _
                                                   " status = 10 , " & _
                                                   " LastModified = '" & Now.ToString & "' , " & _
                                                   " UserName = '" & id.ToString & "' " & _
                                                   " where Tanggal between '" & tanggalcek & "' and '" & tanggalakhir & "' and status = 1 ;"

                sqlstring &= "Update JournalPenyusutan set " & _
                                        " status = 10 , " & _
                                        " LastModified = '" & Now.ToString & "' , " & _
                                        " UserName = '" & id.ToString & "' " & _
                                        " where Tanggal between '" & tanggalcek & "' and '" & tanggalakhir & "' and status = 1;"



                sqlstring &= "insert into TahunTutup (Tahun,Closed,UserName,status) values('" & tahunAwal & "' , 'True', '" & id.ToString & "' , 1);"
                hasil = SQLExecuteNonQuery(sqlstring)

            End If

        Catch ex As Exception
            Throw New Exception("Error function pindahkan buku :" & ex.ToString)
        End Try

        Return True
    End Function
    Public Function cekTutupBuku(ByVal tahun As Date) As Boolean
        Dim dt As DataTable
        Dim ds As DataSet
        Dim cekstring As String
        Dim tahunsebelumnya As Integer

        Try
            tahunsebelumnya = CInt(tahun.ToString("yyyy")) - 1
            cekstring = "select id from TahunTutup where Tahun = '" & tahunsebelumnya & "' and Closed = 'True' and status = 1 "
            ds = SQLExecuteQuery(cekstring)
            dt = ds.Tables(0)
            If dt.Rows.Count <= 0 Then
                Return False
            End If
        Catch ex As Exception
            Throw New Exception("Error function TutupBuku :" & ex.ToString)
        End Try

        Return True
    End Function

    Public Function cekMasterAccount(ByVal id As String) As Boolean
        Dim cekstring As String
        Dim roleid As String
        Try
            cekstring = "select RoleID from MasterUser where UserID = '" & id.ToString & "' and status = 1"
            roleid = SQLExecuteScalar(cekstring)
            If roleid <> "" Then
                If roleid = "RL005" Or roleid = "RL001" Then
                    Return True
                End If
            Else
                Return False
            End If
        Catch ex As Exception
            Throw New Exception("Error function cekMasterAccount :" & ex.ToString)
        End Try
        Return False
    End Function
    Public Function ExistTutup(ByVal tahun As Date) As Boolean
        Dim dt As DataTable
        Dim ds As DataSet
        Dim cekstring As String

        Try
            cekstring = " select id from TahunTutup where Tahun = '" & tahun.ToString("yyyy") & "' and Closed = 'True' and status = 1"
            ds = SQLExecuteQuery(cekstring)
            dt = ds.Tables(0)

            If dt.Rows.Count > 0 Then
                Return False
            End If

        Catch ex As Exception
            Throw New Exception("Error function ExistTutup :" & ex.ToString)
        End Try

        Return True
    End Function

    Public Function getDetailID(ByVal table As String, ByVal Refno As String, ByVal code As String) As Integer
        Dim id As String
        Dim sqlcek As String

        Try
            sqlcek = "select MAX(IDDetail) from " & table.ToString & " where " & Refno.ToString & " = '" & code.ToString & "' "
            id = SQLExecuteScalar(sqlcek)

            If id = "" Then
                id = 0
            End If

            Return CInt(id)
        Catch ex As Exception
            Throw New Exception("Error function getDetailID dataacccess :" & ex.ToString)
        End Try
        Return id
    End Function
    Public Function getNoPelayaran(ByVal table As String, ByVal Refno As String, ByVal code As String, ByVal dari As String, ByVal ke As String) As Integer
        Dim id As Integer
        Dim sqlcek As String
        Dim dtid As DataTable
        Try
            sqlcek = "select top 1 NoPelayaran from " & table.ToString & " where " & Refno.ToString & " = '" & code.ToString & "' " & _
                            " and Depart_Date between '" & dari & "' and '" & ke & "'  and status <> 0 order by CONVERT(float,NoPelayaran) Desc"
            dtid = SQLExecuteQuery(sqlcek).Tables(0)
            If dtid.Rows.Count > 0 Then
                id = CInt(dtid.Rows(0).Item("NoPelayaran").ToString)
            Else
                id = 0
            End If

        Catch ex As Exception
            Throw New Exception("Error function getNoPelayaran dataacccess :" & ex.ToString)
        End Try
        Return id
    End Function
    Public Function getNoPelayaranPrint(ByVal Mb_No As String) As Integer
        Dim id As Integer
        Dim sqlcek As String
        Dim dtid As DataTable
        Try
            sqlcek = "select NoPelayaran from MBRDetail where Mb_No ='" & Mb_No.ToString & "' and status <> 0 order by NoPelayaran Desc"
            dtid = SQLExecuteQuery(sqlcek).Tables(0)
            If dtid.Rows.Count > 0 Then
                id = CInt(dtid.Rows(0).Item("NoPelayaran").ToString)
            Else
                id = 0
            End If
        Catch ex As Exception
            Throw New Exception("Error function getNoPelayaranPrint dataacccess :" & ex.ToString)
        End Try
        Return id
    End Function
    Public Function getRomanNumber(ByVal number As Integer) As String
        Dim result As StringBuilder = New StringBuilder
        Dim awal As String = ""
        Dim values() As Integer = New Integer() {1000, 900, 500, 400, 100, 90, 50, 40, 10, 9, 5, 4, 1}
        Dim numerals() As String = New String() {"M", "CM", "D", "CD", "C", "XC", "L", "XL", "X", "IX", "V", "IV", "I"}
        If number = 0 Then
            Return awal
        End If
        For i As Integer = 0 To 12
            While number >= values(i)
                number = number - values(i)
                result.Append(numerals(i))
            End While
        Next
        Return result.ToString
    End Function
    Public Function getNoKonosemenPrint(ByVal Mb_No As String) As Integer
        Dim id As Integer
        Dim sqlcek As String
        Dim dtid As DataTable
        Try
            sqlcek = "select NoKonosemen from MuatBarang where Mb_No ='" & Mb_No.ToString & "' and status <> 0"
            dtid = SQLExecuteQuery(sqlcek).Tables(0)
            If dtid.Rows.Count > 0 Then
                id = CInt(dtid.Rows(0).Item("NoKonosemen").ToString)
            Else
                id = 0
            End If
        Catch ex As Exception
            Throw New Exception("Error function getNoPelayaranPrint dataacccess :" & ex.ToString)
        End Try
        Return id
    End Function
    Public Function getDetailIDMaster(ByVal table As String) As Integer
        Dim id As Integer
        Dim sqlcek As String

        Try
            sqlcek = "select top 1 IDDetail from " & table.ToString & " where status <> 0 order by IDDetail Desc"
            id = SQLExecuteScalar(sqlcek)

        Catch ex As Exception
            Throw New Exception("Error function getDetailID dataacccess :" & ex.ToString)
        End Try
        Return id
    End Function

    Public Function getDetailIDMasterSatuan(ByVal table As String) As Integer
        Dim id As Integer
        Dim sqlcek As String

        Try
            sqlcek = "select top 1 IDDetail from " & table.ToString & " order by IDDetail Desc"
            id = SQLExecuteScalar(sqlcek)

        Catch ex As Exception
            Throw New Exception("Error function getDetailID dataacccess :" & ex.ToString)
        End Try
        Return id
    End Function

    Public Function MakeIDTable(ByVal table As String, ByRef Field As String) As Integer
        Dim id As Integer
        Dim sqlcek As String
        Dim hsl As String = ""

        Try
            sqlcek = "select top 1 " & Field & " from " & table.ToString & " order by " & Field & " Desc"
            hsl = SQLExecuteScalar(sqlcek)

            If hsl = "" Then
                id = 1
            Else
                id = CInt(hsl) + 1
            End If

        Catch ex As Exception
            Throw New Exception("Error function getDetailID dataacccess :" & ex.ToString)
        End Try
        Return id
    End Function

    Public Function Get_From_LinkedAccount(ByVal TableName As String, ByVal Jenis As String, ByVal Desc As String, ByVal Mode As String) As DataSet
        Try
            Dim str As String
            Dim iDS As New DataSet

            str = "SELECT Debet, Kredit FROM " & TableName & " WHERE Jenis = '" & Jenis & "' AND [Description] LIKE '" & Desc & "' " & _
                  "AND Mode = '" & Mode & "' AND [status] = 1"
            iDS = SQLExecuteQuery(str)

            Return iDS
        Catch ex As Exception
            Throw New Exception("<b>Error Function Get_From_LinkedAccount :</b>" & ex.ToString)
        End Try

    End Function

    Public Function Cek_Data(ByVal Value As String) As String
        Try
            Dim Mark As String = ""

            If IsNumeric(Value) = True Then
                If Value.Contains(".") Then
                    Dim pisah() As String
                    Dim nilai As String
                    nilai = FormatNumber(Value.ToString, 3)

                    pisah = nilai.Split(".")

                    pisah(0) = pisah(0).Replace(",", ".")

                    'add 10 Agustus 2010, kalo error delete aja yg ini
                    If pisah(1).Contains("000") = True Then
                        Mark = pisah(0)
                    Else
                        Mark = pisah(0) & "," & pisah(1)
                    End If


                ElseIf Value.Contains(",") Then
                    Mark = Value.Replace(",", ".")
                Else
                    Mark = Value
                End If
            Else
                Mark = Value
            End If

            Return Mark
        Catch ex As Exception
            Throw New Exception("<b>Error Cek Data :</b>" & ex.ToString)
        End Try
    End Function

    Public Function Cek_Format(ByVal Value As String) As String
        Try
            Dim Mark As String = ""
            Dim pisah() As String
            Dim nilai As String

            If IsNumeric(Value) = True Then
                If Value.Contains(".") Then
                    
                    nilai = FormatNumber(Value.ToString, 3)

                    pisah = nilai.Split(".")

                    pisah(0) = pisah(0).Replace(",", ".")

                    'add 10 Agustus 2010, kalo error delete aja yg ini
                    If pisah(1).Contains("000") = True Then
                        Mark = pisah(0)
                    Else
                        Mark = pisah(0) & "," & pisah(1)
                    End If


                ElseIf Value.Contains(",") Then
                    Mark = Value.Replace(",", ".")
                Else
                    If Value = 0 Then
                        Mark = Value
                    Else
                        nilai = FormatNumber(Value.ToString, 3)

                        pisah = nilai.Split(".")

                        pisah(0) = pisah(0).Replace(",", ".")

                        Mark = pisah(0) & "," & pisah(1)
                    End If
                    

                End If
            Else
                Mark = Value
            End If

            Return Mark
        Catch ex As Exception
            Throw New Exception("<b>Error Cek Data :</b>" & ex.ToString)
        End Try
    End Function

    Public Function UbahKomaJdTitik(ByVal Value As String)
        Try
            Dim Mark As String = ""


            If Value.Contains(",") Or Value.Contains(".") Then
                Dim pisah() As String

                Value = Value.Replace(".", "")
                pisah = Value.Split(",")

                If pisah.Length > 1 Then

                    Mark = pisah(0) & "." & pisah(1)
                Else
                    Mark = Value
                End If

            ElseIf Value = "" Then
                Mark = 0

            Else
                Mark = Value

            End If

            Return Mark
        Catch ex As Exception
            Throw New Exception("<b>Error Cek Data :</b>" & ex.ToString)
        End Try
    End Function


    Public Function ReplaceString(ByVal value As String)
        Try
            Dim hasil As String = ""

            If value.Contains(".") Or value.Contains(",") Then
                hasil = value.Replace(".", "")
            Else
                hasil = value
            End If
            Return hasil
        Catch ex As Exception
            Throw New Exception("<b>Error replace string ;</b>" & ex.ToString)
        End Try
    End Function

    Public Function Tmbh3digit(ByVal val As String) As String
        Try
            Dim pisah() As String
            Dim Hasil As String

            If val.Contains(",") Then
                pisah = val.Split(",")

                If pisah(1) = True Then
                    Hasil = val
                Else
                    Hasil = val.ToString & ",000"
                End If

            Else
                If val = "0" Then
                    Hasil = val
                ElseIf val = "" Then
                    Hasil = "0"
                Else
                    Hasil = val.ToString & ",000"
                End If

            End If

            

            Return Hasil
        Catch ex As Exception
            Throw New Exception(ex.ToString)
        End Try
    End Function

    'Public Function ReplaceString(ByVal value As String)
    '    Try
    '        Dim mark As String = ""
    '        Dim pisah() As String

    '        If value.Contains(".") Or value.Contains(",") Then
    '            hasil = value.Replace(".", "")
    '        Else
    '            hasil = value
    '        End If
    '        Return mark
    '    Catch ex As Exception
    '        Throw New Exception("<b>Error replace string ;</b>" & ex.ToString)
    '    End Try
    'End Function

    Public Function UbahKoma(ByVal Value As String)
        Try
            Dim hasil As String = ""

            hasil = Cek_Data(Format(CDbl(Value), "##,###,###,##"))

            Return hasil
        Catch ex As Exception
            Throw New Exception(ex.ToString)
        End Try
    End Function

    Public Function UbahKomaDlmUkuran(ByVal value As String)
        Try
            Dim Mark As String = ""



            If value.Contains(".") Then
                Dim pisah() As String
                value = Format(CDbl(value), "##,###,###,##.000")
                pisah = value.Split(".")


                If pisah.Length > 1 Then
                    pisah(0) = pisah(0).Replace(",", ".")

                    Mark = pisah(0) & "," & pisah(1)
                Else
                    Mark = value
                End If
            Else

                Mark = Format(CDbl(value), "##,###,###,##").ToString.Replace(",", ".")

            End If

            If value = "0" Then
                Mark = 0
            End If

            Return Mark
        Catch ex As Exception
            Throw New Exception(ex.ToString)
        End Try
    End Function

    Public Function TukarNamaPerusahaan(ByVal Value As String) As String
        Dim Nama As String
        Try

            Dim Pisah() As String

            If Value.Trim.ToString.Contains(",") Then
                Pisah = Value.Split(",")

                Nama = Pisah(1) & " " & Pisah(0)
            ElseIf Value.Trim.ToString.Contains(".") Then
                Pisah = Value.Split(".")

                Nama = Pisah(1) & " " & Pisah(0)
            Else
                Nama = Value
            End If

            Return Nama
        Catch ex As Exception
            Throw New Exception(ex.ToString)
        End Try

    End Function

    Public Function Changedate(ByVal Value As String, ByVal flag As Integer) As DateTime
        Try
            Dim hasil As DateTime

            If flag = 1 Then
                hasil = CDate(Value).ToString("dd MMMM yyyy")
            ElseIf flag = 2 Then
                hasil = CDate(Value).ToString("MM/dd/yyyy")
            End If

            Return hasil
        Catch ex As Exception
            Throw New Exception(ex.ToString)
        End Try
    End Function

    Public Function FillMonth() As DataTable
        Try
            Dim mDT As New DataTable
            Dim mDR As DataRow
            Dim NamaBulan As String = ""
            Dim AngkaBulan As String = ""
            Dim PisahNamabulan() As String
            Dim PisahAngkaBulan() As String

            Namabulan = "Januari,Februari,Maret,April,Mei,Juni,Juli,Agustus,September,Oktober,November,Desember"
            AngkaBulan = "1,2,3,4,5,6,7,8,9,10,11,12"
            PisahNamabulan = NamaBulan.Split(",")
            PisahAngkaBulan = AngkaBulan.Split(",")

            mDT.Columns.Add(New DataColumn("NamaBulan", GetType(String)))
            mDT.Columns.Add(New DataColumn("AngkaBulan", GetType(String)))

            For i As Integer = 0 To 11
                mDR = mDT.NewRow
                mDR("NamaBulan") = PisahNamabulan(i).ToString
                mDR("AngkaBulan") = PisahAngkaBulan(i).ToString
                mDT.Rows.Add(mDR)
            Next

            Return mDT
        Catch ex As Exception
            Throw New Exception(ex.ToString)
        End Try
    End Function

    Public Function bulatkankeatas(ByVal BilDes As Double) As Integer
        Try
            Dim temp As Integer

            temp = Val(BilDes)

            If BilDes / temp > 1 Then
                bulatkankeatas = temp + 1
            Else
                bulatkankeatas = temp
            End If


        Catch ex As Exception
            Throw New Exception(ex.ToString)
        End Try
    End Function

    Public Function CekUsingNumber(ByVal TableName As String, ByVal fieldName As String, ByVal Code As String) As Integer
        Try
            Dim CDT As DataTable

            Dim str As String
            Dim hsl As Integer = 0

            str = "SELECT ID FROM " & TableName & " where " & fieldName & " = '" & Code & "' and [status] <> 0 "
            CDT = SQLExecuteQuery(str).Tables(0)

            If CDT.Rows.Count > 0 Then
                hsl = 1
            Else
                hsl = 0
            End If

            Return hsl
        Catch ex As Exception
            Throw New Exception(ex.ToString)
        End Try

    End Function

    Public Function GETNAMAROLE(ByVal userID As String) As String

        Try
            Dim str As String
            Dim namaroles As String

            str = "select R.Nama from Roles R " & _
                    "LEFT JOIN MasterUser MU on R.RoleID = MU.RoleID " & _
                    "WHERE r.[status] <> 0 " & _
                    "AND MU.[Status] <> 0 " & _
                    "AND MU.UserID = '" & userID & "' "
            namaroles = SQLExecuteScalar(str)

            Return namaroles
        Catch ex As Exception
            Throw New Exception(ex.ToString)
        End Try

    End Function

    Public Function GetLokasi(ByVal code As Integer) As String
        Try
            Dim lokasi As String = ""

            Select Case code
                Case 1
                    lokasi = "Jakarta"
                Case 2
                    lokasi = "Pangkal Pinang"
                Case 3
                    lokasi = "Tanjung Pandan"
                Case 4
                    lokasi = "Gabungan"
            End Select

            Return lokasi
        Catch ex As Exception
            Throw New Exception(ex.ToString)
        End Try
    End Function

    'added for AR Journal
    Public Function GetConfigAR(ByVal Cust As String, ByVal IDKapal As String) As DataSet
        Try
            Dim sqlcari As String
            Dim eDS As New DataSet
            Dim sqlkapal As String
            Dim NmKapal As String

            sqlkapal = "SELECT Nama_Kapal FROM Kapal WHERE status =1 AND IDDetail= " & IDKapal.ToString
            NmKapal = SQLExecuteScalar(sqlkapal)

            sqlcari = "SELECT Debet, Kredit FROM LinkedAccount WHERE Description = 'PENJUALAN - " & NmKapal.ToString & " - " & Cust.Trim & "'"
            eDS = SQLExecuteQuery(sqlcari)

            Return eDS
        Catch ex As Exception
            Throw New Exception(ex.ToString)
        End Try
    End Function

    Public Function GetCOAPiutangCust(ByVal Cust As String) As Boolean
        Try
            Dim sqlcari As String
            Dim hasil As String

            sqlcari = "SELECT Code FROM ChartOfAccount WHERE Name = 'PIUTANG DAGANG - " & Cust.ToString & "'"
            hasil = SQLExecuteScalar(sqlcari)

            If hasil = "" Then
                Return False
            Else
                Return True
            End If
        Catch ex As Exception
            Throw New Exception(ex.ToString)
        End Try
    End Function

    Public Function GetCOAPenghasilanKapal(ByVal IdKapal As String) As Boolean
        Try
            Dim sqlcari As String
            Dim hasil As String

            Dim NamaKapal As String

            NamaKapal = GetNamaKapal(IdKapal.ToString)

            sqlcari = "SELECT Code FROM ChartOfAccount WHERE Name = 'PENGHASILAN KAPAL ' + SUBSTRING('" & NamaKapal.ToString & "',4,50)"
            hasil = SQLExecuteScalar(sqlcari)

            If hasil = "" Then
                Return False
            Else
                Return True
            End If
        Catch ex As Exception
            Throw New Exception(ex.ToString)
        End Try
    End Function

    Public Function GetDescCOA(ByVal NoCOA As String) As String
        Try
            Dim Str As String

            Str = "SELECT Name from ChartOfAccount where Code LIKE '" & NoCOA & "' " & _
                        "AND [status] = 1"

            Return SQLExecuteScalar(Str)
        Catch ex As Exception
            Throw New Exception("<b>Error GetDescCOA :</b>" & ex.ToString)
        End Try

    End Function

    Public Function GetRunNoHeader() As Int64
        Dim str As String
        Dim hasil As String
        Dim number As Int64

        Try
            Str = "SELECT TOP 1 RunNo FROM JournalSalesHeader ORDER BY RunNo DESC"
            hasil = SQLExecuteScalar(Str)

            If hasil = "" Then
                Number = 1
            Else
                Number = hasil + 1
            End If

            Return Number
        Catch ex As Exception
            Throw New Exception("<b>Error Get RunNo Header :</b>" & ex.ToString)
        End Try
    End Function

    Public Function GetNamaKapal(ByVal IdKapal As String) As String
        Dim NamaKapal As String
        Dim sqlstring As String

        sqlstring = "SELECT Nama_Kapal FROM KAPAL WHERE status=1 AND IDDetail= " & IdKapal.ToString
        NamaKapal = SQLExecuteScalar(sqlstring)

        Return NamaKapal

    End Function

    Public Sub InsertLinkedAccount(ByVal IdKapal As String, ByVal NamaCust As String, ByVal username As String)
        Dim sqlstring As String = ""

        Dim sqlcoakapal As String = ""
        Dim sqlcoacust As String = ""
        Dim coakapal As String = ""
        Dim coacust As String = ""

        Dim NamaKapal As String = ""

        NamaKapal = GetNamaKapal(IdKapal)

        sqlcoakapal = "SELECT Code FROM ChartOfAccount WHERE Name= 'PENGHASILAN KAPAL " & NamaKapal.ToString & "'"
        coakapal = SQLExecuteScalar(sqlcoakapal)

        sqlcoacust = "SELECT Code FROM ChartOfAccount WHERE Name= 'PIUTANG DAGANG - " & NamaCust.ToString & "'"
        coacust = SQLExecuteScalar(sqlcoacust)

        sqlstring = "INSERT INTO LinkedAccount(Mode,Jenis,Description,Debet,Kredit,UserName,status,timestamp)" & _
                    "VALUES('Sales','AR','PENJUALAN - " & NamaKapal.ToString & " - " & NamaCust.ToString & "','" & coacust.ToString & "','" & coakapal.ToString & "','" & username.ToString & "','1', GETDATE())"
        SQLExecuteNonQuery(sqlstring)
    End Sub

    Public Sub InsertCOAPiutang(ByVal Name As String, ByVal username As String)
        Dim sqlstring As String = ""
        Dim sqlcode As String = ""
        Dim code As String = ""

        sqlcode = "SELECT COUNT(CODE) FROM ChartOfAccount WHERE Parent='0001.114.001.0000'"
        code = Right("0000" & CInt(SQLExecuteScalar(sqlcode)) + 1, 4)


        sqlstring = "INSERT INTO ChartOfAccount (Types, Parent, Code, NAME, Levels, UserName, status,timestamp,Lokasi)" & _
                    "VALUES('0001','0001.114.001.0000','0001.114.001." & code.ToString & "', 'PIUTANG DAGANG - " & Name.ToString & "', '3', '" & username.ToString & "','1', GETDATE(), '1')"
        SQLExecuteNonQuery(sqlstring)
    End Sub

    Public Sub InsertCOAPenghasilanKapal(ByVal IdKapal As String, ByVal username As String)
        Dim sqlstring As String = ""
        Dim sqlcode As String = ""
        Dim code As String = ""

        Dim NamaKapal As String
        NamaKapal = GetNamaKapal(IdKapal)

        sqlcode = "SELECT COUNT(CODE) FROM ChartOfAccount WHERE Parent='0004.411.001.0000'"
        code = CInt(SQLExecuteScalar(sqlcode)) + 1


        sqlstring = "INSERT INTO ChartOfAccount (Types, Parent, Code, NAME, Levels, UserName, status,timestamp,Lokasi)" & _
                    "VALUES('0001','0004.411.001.0000','0004.411.001." & code.ToString & "', 'PENGHASILAN KAPAL " & NamaKapal.ToString & "', '3', '" & username.ToString & "','1', GETDATE(), '1')"
        SQLExecuteNonQuery(sqlstring)
    End Sub

    Public Sub InsertARHeader(ByVal RunNo As Int64, ByVal RefNo As String, ByVal tgl As Date, _
                                ByVal Amount As Double, ByVal Kode_Customer As String, ByVal Ket As String, ByVal UserName As String)
        Try
            Dim sqlj As String = ""

            sqlj = "INSERT INTO JournalARHeader (RunNo,RefNo,TransDate,Mode,Description,NoNota,Amount,Kode_Customer,Ket,status,UserName,TimeStamp)" & _
                    "VALUES (" & RunNo & ",'" & RefNo.ToString & "','" & tgl & "','SALES','" & Kode_Customer.ToString & " - " & RefNo.ToString & "','AR','" & Amount & "','" & Kode_Customer & "',NULL,'1','" & UserName & "',GETDATE())"

            SQLExecuteNonQuery(sqlj)

        Catch ex As Exception
            Throw New Exception(ex.ToString)
        End Try
    End Sub

    Public Sub InsertARDetailDebit(ByVal RunNo As Int64, ByVal NamaCust As String, _
                                ByVal Amount As Double, ByVal UserName As String)
        Try
            Dim sqlacc As String = ""
            Dim sqlj As String = ""
            Dim eDS As DataSet
            Dim eDT As DataTable

            sqlacc = "SELECT * FROM ChartOfAccount WHERE Name = 'PIUTANG DAGANG - " & NamaCust.ToString & "'"
            eDS = SQLExecuteQuery(sqlacc)
            eDT = eDS.Tables(0)

            sqlj = "INSERT INTO JournalARDetail(RunNoHeader,IDDetail,AccountNo,Type,Amount,Description,status,UserName,TimeStamp)" & _
                    "VALUES (" & RunNo & ",'1','" & eDT.Rows(0).Item("Code").ToString & "','DEBIT'," & Amount & ",'" & eDT.Rows(0).Item("Name").ToString & "','1','" & UserName & "',GETDATE())"

            SQLExecuteNonQuery(sqlj)

        Catch ex As Exception
            Throw New Exception(ex.ToString)
        End Try
    End Sub

    Public Sub InsertARDetailKredit(ByVal RunNo As Int64, ByVal IdKapal As String, _
                                ByVal Amount As Double, ByVal UserName As String)
        Try
            Dim sqlacc As String = ""
            Dim sqlj As String = ""
            Dim eDS As DataSet
            Dim eDT As DataTable
            Dim NamaKapal As String = ""

            NamaKapal = GetNamaKapal(IdKapal)

            sqlacc = "SELECT * FROM ChartOfAccount WHERE Name = 'PENGHASILAN KAPAL ' + SUBSTRING('" & NamaKapal.ToString & "',4,50)"
            eDS = SQLExecuteQuery(sqlacc)
            eDT = eDS.Tables(0)

            sqlj = "INSERT INTO JournalARDetail(RunNoHeader,IDDetail,AccountNo,Type,Amount,Description,status,UserName,TimeStamp)" & _
                    "VALUES (" & RunNo & ",'2','" & eDT.Rows(0).Item("Code").ToString & "','KREDIT'," & Amount & ",'" & eDT.Rows(0).Item("Name").ToString & "','1','" & UserName & "',GETDATE())"

            SQLExecuteNonQuery(sqlj)

        Catch ex As Exception
            Throw New Exception(ex.ToString)
        End Try
    End Sub
End Module

