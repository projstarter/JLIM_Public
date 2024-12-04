Public Class frmUserRegistration

    Public EditMode As Boolean
    Public RegistrationMode As Boolean
    Public OwnAccount As Boolean = False
    Public RecNo As Integer
    Public ShowStatusControl As Boolean
    Private Sub frmUserRegistration_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        If Not (RegistrationMode) Then
            txtFirstname.Enabled = IsAllowed("UA_CreateAdd") Or IsAllowed("UA_EditUpdate")
            txtLastname.Enabled = IsAllowed("UA_CreateAdd") Or IsAllowed("UA_EditUpdate")
            txtNickname.Enabled = IsAllowed("UA_CreateAdd") Or IsAllowed("UA_EditUpdate")
            txtUsername.Enabled = IsAllowed("UA_CreateAdd") Or IsAllowed("UA_EditUpdate")
            btnClearEntry.Enabled = IsAllowed("UA_CreateAdd")
        End If


        If (thisUser.keyid = Nothing) Then
            cbostatus.SelectedIndex = 1
        Else
            If (RecNo = thisUser.keyid) Then
                If Not (RegistrationMode) Then
                    cboUserType.Enabled = IsAllowed("UA_EditUpdate")
                    cbostatus.Enabled = IsAllowed("UA_EditUpdate")
                End If
            End If

            If (EditMode) Then
                'status control
                cbostatus.Visible = True
                Label10.Visible = True
                '--
                Dim d As New clsDataManipulation
                d.Fetch("select * from tbluser where keyid = " & RecNo & " and archive <> 1")
                Do While d.DataReader.Read
                    With d.DataReader
                        txtFirstname.Text = .Item("firstname").ToString()
                        txtLastname.Text = .Item("lastname").ToString()
                        txtNickname.Text = .Item("nickname").ToString()
                        cboUserType.Text = .Item("function").ToString()
                        txtUsername.Text = .Item("uname").ToString()
                        cbostatus.SelectedIndex = IIf(.Item("isactive") = True, 0, 1)
                        'txtPassword.Text = .Item("pw").ToString()
                        'txtRetypePassword.Text = .Item("rpw").ToString()
                    End With
                Loop
                d.DataReader.Close()
            Else
                cbostatus.SelectedIndex = 1

                If Not (RegistrationMode) Then
                    cbostatus.Visible = IsAllowed("UA_CreateAdd") And IsAllowed("UA_EditUpdate")
                    Label10.Visible = IsAllowed("UA_CreateAdd") And IsAllowed("UA_EditUpdate")
                End If
            End If
        End If

        PopulateCbo(cboUserType, "select distinct permission_key from tblaccesslevelconfiguration where permission_key <>''")

    End Sub

    Private Sub CloseMe(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub btnClearEntry_Click(sender As Object, e As EventArgs) Handles btnClearEntry.Click
        EditMode = False
        ClearEntry()

    End Sub
    Function btnsaveOnRegistrationMode() As Boolean
        If (RegistrationMode) Then
            Return False
        Else
            Return (IsAllowed("UA_EditUpdate"))
        End If
    End Function

    Function RequiredFieldsNotBlank() As Boolean
        Return (txtFirstname.Text.Trim <> "" And
                       txtLastname.Text.Trim <> "" And
                       txtNickname.Text.Trim <> "" And
                       cboUserType.Text.Trim <> "" And
                       txtUsername.Text.Trim <> "")
    End Function


    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        If (EditMode) And (RecNo <> thisUser.keyid) Then
            btnSave.Enabled = RequiredFieldsNotBlank() And (txtPassword.Text.Trim = txtRetypePassword.Text.Trim)
        Else
            btnSave.Enabled = RequiredFieldsNotBlank() And (txtPassword.Text.Trim <> "" And txtRetypePassword.Text.Trim <> "") And (txtPassword.Text.Trim = txtRetypePassword.Text.Trim)
        End If

        If (txtPassword.Text.Trim <> txtRetypePassword.Text.Trim) And (txtPassword.Text.Trim <> "" And txtRetypePassword.Text.Trim <> "") Then
            Label9.Text = "Password did not match!"
        Else
            Label9.Text = "* Required fields cannot be blank."
        End If
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        SaveRegistration()
        Me.Close()
    End Sub

    Private Sub ClearEntry()
        txtFirstname.Text = ""
        txtLastname.Text = ""
        txtNickname.Text = ""
        cboUserType.Text = ""
        txtUsername.Text = ""
        txtPassword.Text = ""
        txtRetypePassword.Text = ""
        txtFirstname.Focus()
    End Sub
    Private Sub SaveRegistration()

        '"s uname      => " & txtUsername.Text,
        '"s pw             => " & txtPassword.Text,
        '"s rpw           => " & txtRetypePassword.Text,

        Dim d As New clsDataManipulation
        If (EditMode) Then
            d.Update("tbluser",
                            {
                                "s firstname => " & txtFirstname.Text,
                                "s lastname  => " & txtLastname.Text,
                                "s uname  => " & txtUsername.Text,
                                "s nickname => " & txtNickname.Text,
                                "s function   => " & cboUserType.Text,
                                "n isactive    => " & IIf(cbostatus.Text = "Disable", 0, 1)
                            },
                            {"keyid IN (" & RecNo & ")"}
                         )

            If (txtPassword.Text <> "") Then
                d.Update("tbluser",
                            {
                                "s pw             => " & TextEncode(FixApostrophe(txtPassword.Text)),
                                "s rpw           => " & TextEncode(FixApostrophe(txtRetypePassword.Text))
                            },
                            {"keyid IN (" & RecNo & ")"}
                         )
            End If
            MessageBox.Show(Me, "User account successfully updated!", "Update Alert", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Else
            d.Insert("tbluser",
                            {
                                "s uname      => " & txtUsername.Text,
                                 "s pw           => " & TextEncode(FixApostrophe(txtPassword.Text)),
                                "s rpw           => " & TextEncode(FixApostrophe(txtRetypePassword.Text)),
                                "s firstname => " & txtFirstname.Text,
                                "s lastname  => " & txtLastname.Text,
                                "s nickname => " & txtNickname.Text,
                                "s function   => " & cboUserType.Text,
                                "n isactive    => " & IIf(cbostatus.Text = "Disable", 0, 1)
                            }
                         )
            MessageBox.Show(Me, "Registration completed successfully!", "Registration Alert", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If

    End Sub
    Private Sub txtFirstname_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtFirstname.KeyPress, txtLastname.KeyPress, txtNickname.KeyPress
        e.KeyChar = e.KeyChar.ToString.ToUpper
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub cboUserType_KeyDown(sender As Object, e As KeyEventArgs) Handles cboUserType.KeyDown

    End Sub

    Private Sub cboUserType_KeyPress(sender As Object, e As KeyPressEventArgs) Handles cboUserType.KeyPress
        e.KeyChar = ""
    End Sub
End Class