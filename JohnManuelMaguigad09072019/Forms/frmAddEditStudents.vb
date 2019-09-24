Imports System.Data.OleDb
Public Class frmAddEditStudents
    Dim da As New OleDbDataAdapter
    Dim dsetDept, dsetCourses, dsetStudent As New DataSet
    Dim Com As New OleDbCommand
    Public studSelectedInfo As String

    Function retrieveDepartments()
        Dim recNo As Integer = 0
        da = New OleDbDataAdapter("SELECT * FROM tblDepartments ORDER BY deptCode", Con)
        dsetDept = New DataSet
        da.Fill(dsetDept, "tblDepartments")
        cboDept.Items.Clear()
        For recNo = 0 To dsetDept.Tables("tbldepartments").Rows.Count - 1
            cboDept.Items.Add(dsetDept.Tables("tbldepartments").Rows(recNo).Item("deptCode").ToString)
        Next
        Return True
    End Function

    Function retrieveCourses()
        Dim recNo As Integer = 0
        da = New OleDbDataAdapter("SELECT * FROM tblCourses where deptCode='" & cboDept.Text & "'", Con)
        dsetCourses = New DataSet
        da.Fill(dsetCourses, "tblCourses")
        cboCourse.Items.Clear()
        For recNo = 0 To dsetCourses.Tables("tblcourses").Rows.Count - 1
            cboCourse.Items.Add(dsetCourses.Tables("tblcourses").Rows(recNo).Item("cCode").ToString)
        Next
        Return True
    End Function

    Public Function retrieveStudentInfo()
        da = New OleDbDataAdapter("SELECT * FROM tblStudents where studID='" & studSelectedInfo & "'", Con)
        dsetStudent = New DataSet
        da.Fill(dsetStudent, "tblStudents")
        txtStudentID.Text = dsetStudent.Tables("tblstudents").Rows(0).Item("studID").ToString
        txtFirstName.Text = dsetStudent.Tables("tblstudents").Rows(0).Item("FirstName").ToString
        txtMiddleName.Text = dsetStudent.Tables("tblstudents").Rows(0).Item("MiddleName").ToString
        txtLastName.Text = dsetStudent.Tables("tblstudents").Rows(0).Item("LastName").ToString
        cboDept.Text = dsetStudent.Tables("tblstudents").Rows(0).Item("deptCode").ToString
        cboCourse.Text = dsetStudent.Tables("tblstudents").Rows(0).Item("cCode").ToString
        Return True
    End Function

    Private Sub frmAddEditStudents_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        DBConnection()
        retrieveDepartments()
        If AEswitch = "e" Then
            retrieveStudentInfo()
        End If
    End Sub

    Private Sub cboDept_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboDept.SelectedIndexChanged
        retrieveCourses()
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        DBConnection()
        saveRec()
    End Sub

    Function saveRec()
        If txtStudentID.Text <> "" Or txtFirstName.Text <> "" Or txtMiddleName.Text <> "" Or txtLastName.Text <> "" Then
            If AEswitch = "a" Then
                AddEditData("INSERT INTO tblStudents VALUES ('" & txtStudentID.Text & "','" & txtFirstName.Text & "','" & txtMiddleName.Text & "','" & txtLastName.Text & "','" & cboDept.Text & "','" & cboCourse.Text & "')", Me)
                'Com = New OleDbCommand("INSERT INTO tblStudents VALUES ('" & txtStudentID.Text & "','" & txtFirstName.Text & "','" & txtMiddleName.Text & "','" & txtLastName.Text & "','" & cboDept.Text & "','" & cboCourse.Text & "')", Con)
                'Com.ExecuteNonQuery()
                'MsgBox("Successfully saved")
                'Me.Close()
            ElseIf AEswitch = "e" Then
                AddEditData("UPDATE tblStudents SET FirstName='" & txtFirstName.Text & "',MiddleName='" & txtMiddleName.Text & "',LastName='" & txtLastName.Text & "',deptCode='" & cboDept.Text & "',cCode='" & cboCourse.Text & "' WHERE studID='" & studSelectedInfo & "'", Me)
                'Com = New OleDbCommand("UPDATE tblStudents SET FirstName='" & txtFirstName.Text & "',MiddleName='" & txtMiddleName.Text & "',LastName='" & txtLastName.Text & "',deptCode='" & cboDept.Text & "',cCode='" & cboCourse.Text & "' WHERE studID='" & studSelectedInfo & "'", Con)
                'Com.ExecuteNonQuery()
                'MsgBox("Successfully saved")
                'Me.Close()
            End If
        End If
        Return True
    End Function
End Class