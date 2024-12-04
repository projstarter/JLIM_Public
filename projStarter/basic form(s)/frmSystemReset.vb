Public Class frmSystemReset
    Private Sub chklvl1_CheckedChanged(sender As Object, e As EventArgs) Handles chklvl1.CheckedChanged
        chklvl2.Checked = False
    End Sub

    Private Sub chklvl2_CheckedChanged(sender As Object, e As EventArgs) Handles chklvl2.CheckedChanged
        chklvl1.Checked = False
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        btnproceed.Enabled = ((chklvl1.Checked) Or (chklvl2.Checked))
    End Sub

    Private Sub btncancel_Click(sender As Object, e As EventArgs) Handles btncancel.Click
        Me.Close()
    End Sub
    Private Sub TruncateTransactionTable()
        Dim d As New clsDataManipulation
        d.Exec("DELETE FROM tbltransaction")
        d.Exec("ALTER TABLE tbltransaction ALTER COLUMN keyid INT")
        d.Exec("ALTER TABLE tbltransaction ALTER COLUMN keyid AUTOINCREMENT")
    End Sub
    Private Sub TruncateUserTable()
        Dim d As New clsDataManipulation
        d.Exec("DELETE FROM tbluser")
        d.Exec("DELETE FROM tblsystem")
        d.Exec("DELETE FROM tblaccesslevelconfiguration")
        d.Exec("insert into tblsystem (reset_password) values ('" & TextEncode(FixApostrophe("admin")) & "');")
        d.Exec("INSERT INTO tbluser (isactive,uname, pw, rpw,function) VALUES (true,'admin','" & TextEncode(FixApostrophe("admin")) & "', '" & TextEncode(FixApostrophe("admin")) & "','Super Admin')")

        Dim fullAdminAccess As String() = {"DT_CreateAdd", "DT_EditUpdate", "DT_DeleteRemove", "DT_Export",
                                                                    "UA_CreateAdd", "UA_EditUpdate", "UA_DeleteRemove",
                                                                    "ALS_CreateAdd", "ALS_EditUpdate", "ALS_DeleteRemove",
                                                                    "PC_Allow",
                                                                    "OA_Allow",
                                                                    "SR_Allow", "SR_ChangePass"}

        For i As Integer = 0 To fullAdminAccess.Count - 1
            d.Exec("insert into tblaccesslevelconfiguration (permission_key, access) values ('Super Admin', '" & fullAdminAccess(i).ToString & "');")
        Next


    End Sub
    Private Sub btnproceed_Click(sender As Object, e As EventArgs) Handles btnproceed.Click
        If (MessageBox.Show(Me, "Be cautious of your action! Are you sure you want to proceed?", "System Reset Confirmation", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button3)) = DialogResult.Yes Then
            If (chklvl1.Checked) Then
                TruncateTransactionTable()
            End If
            If (chklvl2.Checked) Then
                TruncateUserTable()
                TruncateTransactionTable()
            End If
            MessageBox.Show(Me, "System reset completed!", "System Reset Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.Close()
        End If
    End Sub
End Class