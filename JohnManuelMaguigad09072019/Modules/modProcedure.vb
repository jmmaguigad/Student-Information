Imports System.Data.OleDb
Module modProcedure
    Public Sub RetrieveData(dgExecute As DataGridView, lblRecord As Label, ByVal dataRetrieveSQL As String, ByVal strTable As String)
        Dim CN As OleDbConnection
        CN = New OleDbConnection
        Dim sqlCmd As OleDbCommand = New OleDbCommand(dataRetrieveSQL)
        Try
            With CN
                If .State = ConnectionState.Open Then .Close()
                .ConnectionString = ConStr
                .Open()
            End With

            Dim da As OleDbDataAdapter = New OleDbDataAdapter(dataRetrieveSQL, CN)
            Dim dt As New DataSet

            da.Fill(dt, strTable)

            dgExecute.DataSource = dt.Tables(strTable).DefaultView
            lblRecord.Text = dt.Tables(strTable).Rows.Count & " Records/Found"
            dgExecute.Refresh()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Public Sub QueryExec(ByVal dataSQL As String, subjectForm As Form)
        Dim CN As OleDbConnection
        CN = New OleDbConnection
        Dim sqlCmd As OleDbCommand = New OleDbCommand(dataSQL, CN)
        Try
            With CN
                If .State = ConnectionState.Open Then .Close()
                .ConnectionString = ConStr
                .Open()
            End With

            If sqlCmd.ExecuteNonQuery() Then
                MsgBox("Operation Successful")
                subjectForm.Close()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub
End Module
