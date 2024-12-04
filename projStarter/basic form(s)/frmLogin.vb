Public Class frmLogin
    Private Sub btnRegister_Click(sender As Object, e As EventArgs) Handles btnRegister.Click
        Dim frm As New frmUserRegistration
        frm.RegistrationMode = True
        frm.ShowDialog(Me)
        ClearEntry()
    End Sub
    Private Sub ClearEntry()
        txtUsername.Text = ""
        txtPassword.Text = ""
        txtUsername.Focus()
    End Sub
    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        If (LoginMe(txtUsername.Text, txtPassword.Text)) Then
            Me.Hide()
            Dim frm As New frmNFA
            frm.Show()
        Else
            MsgBox("Unable to login. User credentials is incorrect!", MsgBoxStyle.OkOnly & MsgBoxStyle.Exclamation, "Login Validation")
            ClearEntry()
        End If
    End Sub
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
    Private Sub frmLogin_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'If My.Settings.key1 = "" Then
        '    Dim frm As New frmLicenseEntry
        '    frm.ShowDialog(Me)
        '    If (My.Settings.key1 = "") Then
        '        MessageBox.Show(Me, "Please contact the developer immediately!", "License Usage", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '        End
        '    End If
        'End If

        'Label1.Text = String.Format("LOGIN WINDOW ({0})", My.Settings.Use_Counter)

        'If (Val(My.Settings.Use_Counter) > 100) Then
        '    MessageBox.Show(Me, "You've exceeded the maximum use of this trial version. Please contact the developer immediately!", "Trial Expired", MessageBoxButtons.OK, MessageBoxIcon.Error)
        'End If

        ConnectDB()
        ClearEntry()
    End Sub
    Private Sub txtUsername_KeyDown(sender As Object, e As KeyEventArgs) Handles txtUsername.KeyDown
        If e.KeyCode = Keys.Enter Then
            txtPassword.Focus()
        End If
    End Sub
    Private Sub txtPassword_KeyDown(sender As Object, e As KeyEventArgs) Handles txtPassword.KeyDown
        If e.KeyCode = Keys.Enter Then
            btnLogin_Click(Nothing, Nothing)
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs)
        Dim d As New clsDataManipulation
        d.Insert("tbltransaction",
            {"transaction_type => 'MILLING'"}
            )
    End Sub

    Private Sub btnapp_Click(sender As Object, e As EventArgs) Handles btnapp.Click
        Dim frm As New frmLicenseEntry
        frm.ShowDialog(Me)
    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click

        Dim ask As String = InputBox("Please enter the password for ""System Reset"": ", "System Reset", "")
        If (ask <> "") Then
            If (CheckResetPassword(ask)) Then
                Dim frm As New frmSystemReset
                frm.ShowDialog()
            Else
                MessageBox.Show("Unable to perform operation. Please contact your administrator.", "Operation Terminated", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End If

    End Sub

End Class