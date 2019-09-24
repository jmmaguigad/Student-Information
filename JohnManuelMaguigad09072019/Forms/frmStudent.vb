Imports System.Data.OleDb
Public Class frmStudent
    Dim daStud As New OleDbDataAdapter
    Dim dataStudSet As New DataSet
    Dim dataStudCommand As New OleDbCommand
    Dim textSelected As String
    Dim sSQL As String

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click
        AEswitch = "a"
        frmAddEditStudents.Text = "Add New Record"
        frmAddEditStudents.txtStudentID.Clear()
        frmAddEditStudents.txtFirstName.Clear()
        frmAddEditStudents.txtMiddleName.Clear()
        frmAddEditStudents.txtLastName.Clear()
        frmAddEditStudents.ShowDialog()
    End Sub

    Private Sub frmStudent_Load(sender As Object, e As EventArgs) Handles Me.Load
        StudentLoad()
    End Sub

    Private Sub StudentLoad()
        sSQL = "SELECT * FROM tblStudents order by studID"
        RetrieveData(dgvStudents, lblRecords, sSQL, "tblStudents")
        formatGrid()
    End Sub

    Function retrieveStudentInfo()
        'daStud = New OleDbDataAdapter("SELECT * FROM tblStudents order by studID", Con)
        'dataStudSet = New DataSet
        'daStud.Fill(dataStudSet, "tblStudents")
        'dgvStudents.DataSource = dataStudSet.Tables("tblStudents").DefaultView
        'lblRecords.Text = dataStudSet.Tables("tblStudents").Rows.Count & " Records/Found"
        'formatGrid()
        'dgvStudents.Refresh
        'Return True
    End Function

    Function formatGrid()
        dgvStudents.Font = New Font("Arial New", 12)
        dgvStudents.Columns(0).Width = 100
        dgvStudents.Columns(0).HeaderText = "ID"
        dgvStudents.Columns(1).Width = 130
        dgvStudents.Columns(1).HeaderText = "First Name"
        dgvStudents.Columns(2).Width = 130
        dgvStudents.Columns(2).HeaderText = "Middle Name"
        dgvStudents.Columns(3).Width = 130
        dgvStudents.Columns(3).HeaderText = "Last Name"
        dgvStudents.Columns(4).Width = 130
        dgvStudents.Columns(4).HeaderText = "Department"
        dgvStudents.Columns(5).Width = 130
        dgvStudents.Columns(5).HeaderText = "Course"
        Return True
    End Function

    Private Sub dgvStudents_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvStudents.CellClick
        If dgvStudents.CurrentRow.Index <> -1 Then
            textSelected = dgvStudents.Item(0, dgvStudents.CurrentRow.Index).Value
        End If
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        deleteStudent()
    End Sub

    Function deleteStudent()
        If MessageBox.Show("Are you sure you want to delete this record?", "Delete Student", MessageBoxButtons.OKCancel) = DialogResult.OK Then
            MessageBox.Show(textSelected)
            dataStudCommand = New OleDbCommand("DELETE FROM tblStudent where studID='" & textSelected & "'", Con)
            dataStudCommand.ExecuteNonQuery()
            retrieveStudentInfo()
        End If
        Return True
    End Function

    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        'DBConnection()
        daStud = New OleDbDataAdapter("SELECT * FROM tblStudents where LastName LIKE '%" & Trim(txtSearch.Text) & "%'", Con)
        dataStudSet = New DataSet
        daStud.Fill(dataStudSet, "tblStudents")
        dgvStudents.DataSource = dataStudSet.Tables("tblStudents").DefaultView
        lblRecords.Text = dataStudSet.Tables("tblStudents").Rows.Count & " Records/Found"
        formatGrid()
        dgvStudents.Refresh()
    End Sub

    Private Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click
        AEswitch = "e"
        frmAddEditStudents.Text = "Edit New Record"
        frmAddEditStudents.studSelectedInfo = textSelected
        frmAddEditStudents.txtStudentID.Clear()
        frmAddEditStudents.txtFirstName.Clear()
        frmAddEditStudents.txtMiddleName.Clear()
        frmAddEditStudents.txtLastName.Clear()
        frmAddEditStudents.ShowDialog()
    End Sub

    Private Sub frmStudent_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        StudentLoad()
    End Sub

    Private Sub frmStudent_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Me.KeyPress

    End Sub

    Private Sub frmStudent_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.F1 Then
            MsgBox("f1")
        End If
    End Sub

    Private Sub txtSearch_KeyDown(sender As Object, e As KeyEventArgs) Handles txtSearch.KeyDown
        If e.KeyCode = Keys.F1 Then
            MsgBox("f1")
        End If
    End Sub

    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        frmReportViewer.ShowDialog()
    End Sub
End Class
