Imports System.Net.Sockets

Public Class frmUserAccounts
    Private Sub frmUserAccounts_Load(sender As Object, e As EventArgs) Handles MyBase.Load, btnrefresh.Click, txtSearch.TextChanged, btnsearch.Click
        Initialize()
        PopulateList()
    End Sub
    Sub Initialize()
        btnrefresh.Enabled = IsAllowed("UA_CreateAdd") Or IsAllowed("UA_DeleteRemove")
        btnAddUser.Enabled = IsAllowed("UA_CreateAdd")
        btndelete.Enabled = IsAllowed("UA_DeleteRemove")
    End Sub

    Sub PopulateList()
        PopulateDataGridView(dtgList, "SELECT keyid," &
                             "lastname & ', ' & firstname," &
                             "uname," &
                             "pw," &
                             "function," &
                             "SWITCH (isactive=0,'Suspended',isactive<>0,'Active')" &
                            "FROM tbluser " &
                            "WHERE uname & lastname & firstname LIKE '%" & txtSearch.Text & "%'", True)
        lblstatus.Text = String.Format("{0} user(s) found in the system!", dtgList.RowCount)
    End Sub

    Private Sub dtgList_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles dtgList.CellClick

    End Sub

    Sub UpdateUserStatus(keyid As Integer, toActive As Boolean)
        Dim d As New clsDataManipulation
        Dim cmdText As String = ""
        If (toActive) Then
            cmdText = "UPDATE tbluser SET isactive=1,activated_by='" & thisUser.FullName & "',activated_date='" & Format(Now, "yyyy-MM-dd") & "',suspended_by=null,suspended_date=null WHERE keyid IN (" & keyid & ") "
        Else
            cmdText = "UPDATE tbluser SET isactive=0,suspended_by='" & thisUser.FullName & "',suspended_date='" & Format(Now, "yyyy-MM-dd") & "' WHERE keyid IN (" & keyid & ") "
        End If
        d.Exec(cmdText)
        PopulateList()
    End Sub

    Private Sub btnAddUser_Click(sender As Object, e As EventArgs) Handles btnAddUser.Click
        Dim frm As New frmUserRegistration
        frm.ShowDialog(Me)
        PopulateList()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub dtgList_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dtgList.CellContentClick

    End Sub

    Private Sub dtgList_DoubleClick(sender As Object, e As EventArgs) Handles dtgList.DoubleClick
        If (IsAllowed("UA_EditUpdate")) Then
            Dim frm As New frmUserRegistration
            frm.EditMode = True
            frm.RecNo = dtgList.SelectedRows.Item(0).Cells(1).Value
            frm.ShowDialog()
            PopulateList()
        End If

        'If IsAllowed("UA_EditUpdate") Then
        '    UpdateUserStatus(dtgList.Item(0, e.RowIndex).Value, (dtgList.Item(e.ColumnIndex, e.RowIndex).Value = "Activate"))
        'End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles btndelete.Click


        Dim forDelete As String = ""
        For i As Integer = 0 To dtgList.Rows.Count - 1
            If Not (dtgList.Rows(i).Cells(0).Value = 0 And dtgList.Rows(i).Cells(0).Value = False) Then
                forDelete &= IIf(forDelete <> "", "," & dtgList.Rows(i).Cells(1).Value, dtgList.Rows(i).Cells(1).Value)
            End If
        Next

        If (forDelete <> "") Then
            Dim ask As MsgBoxResult = MessageBox.Show("Are you sure you want to delete the selected records? ", "Delete User Account", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
            If ask = MsgBoxResult.Yes Then
                Dim d As New clsDataManipulation
                d.Exec("delete from tbluser where keyid in (" & forDelete & ")")
                MessageBox.Show("Record/s successfully deleted!", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        End If

        PopulateList()


    End Sub

End Class