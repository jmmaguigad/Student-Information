Imports System.Data.OleDb
Module DatabaseConfig
    Public ConStr As String = "Provider=Microsoft.jet.oledb.4.0;data source=" & Application.StartupPath & "\dbStudent.mdb"
    Public Con As New OleDbConnection(ConStr)
    Public AEswitch As String = ""
    Public PrintCode As String = ""

    Public Function DBConnection()
        Try
            If Con.State = ConnectionState.Closed Then
                Con.Open()
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Return True
    End Function
End Module
