Public Class frmAccessLevelSettings
    Private Sub frmAccessLevelSettings_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        RefreshCboList()
        btnremove.Enabled = IsAllowed("ALS_DeleteRemove")
    End Sub

    Sub ClearPreviousConfigurations()
        Dim d As New clsDataManipulation
        d.Exec("delete from tblaccesslevelconfiguration where permission_key = '" & cbopermisionkey.Text & "'")
    End Sub
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If (MessageBox.Show("Are you sure you want to save or update the selected ""Access Level""?", "Save or Update Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes) Then
            ClearPreviousConfigurations()
            SaveConfiguration()
            MessageBox.Show("Settings have been successfully saved!", "Access Level Configuration")
        End If

        RefreshCboList()

    End Sub
    Sub SaveConfiguration()
        Dim d As New clsDataManipulation
        Dim x As String = " "
        For Each ctrl As Control In GroupBox1.Controls
            If TypeOf (ctrl) Is CheckBox Then
                Dim chk As CheckBox = ctrl
                If (chk.Checked) Then
                    'MsgBox(chk.Name)
                    d.Exec("insert into tblaccesslevelconfiguration (permission_key, access) values ('" & cbopermisionkey.Text & "', '" & chk.Name & "')")
                    x &= "insert into tblaccesslevelconfiguration (permission_key, access) values ('" & cbopermisionkey.Text & "', '" & chk.Name & "');"

                End If
            End If

        Next

    End Sub
    Sub RefreshCboList()
        If (IsAllowed("ALS_EditUpdate")) Then
            PopulateCbo(cbopermisionkey, "select distinct permission_key from tblaccesslevelconfiguration where permission_key <>''")
        End If
    End Sub
    Sub ClearEntry()
        RefreshCboList()
        ClearCheckboxes()
        cbopermisionkey.Text = ""
    End Sub
    Sub ClearCheckboxes()
        'clear checkboxes
        For Each ctrl As Control In GroupBox1.Controls
            If TypeOf (ctrl) Is CheckBox Then
                Dim chk As CheckBox = ctrl
                chk.Checked = False
            End If
        Next
    End Sub
    Private Sub cbopermisionkey_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbopermisionkey.SelectedIndexChanged
        ClearCheckboxes()
        Dim d As New clsDataManipulation
        d.Fetch("select * from tblaccesslevelconfiguration where permission_key = '" & cbopermisionkey.Text & "'")
        Do While d.DataReader.Read
            For Each ctrl As Control In GroupBox1.Controls
                If TypeOf (ctrl) Is CheckBox Then
                    Dim chk As CheckBox = ctrl
                    If (chk.Name = d.DataReader.Item("access").ToString()) Then
                        chk.Checked = True
                    End If
                End If
            Next
        Loop
    End Sub

    Private Sub btnremove_Click(sender As Object, e As EventArgs) Handles btnremove.Click
        If (MessageBox.Show("Are you sure you want to remove the selected ""Access Level""?", "Remove Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes) Then
            ClearPreviousConfigurations()
            cbopermisionkey.Text = ""
        End If

        RefreshCboList()

    End Sub

    Private Sub btnClearEntry_Click(sender As Object, e As EventArgs) Handles btnClearEntry.Click
        ClearEntry()
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub cbopermisionkey_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cbopermisionkey.KeyPress
        If Not (IsAllowed("ALS_CreateAdd")) Then
            e.KeyChar = ""
        End If
    End Sub
End Class