Imports System.IO
Imports System.Runtime.Remoting.Contexts

Public Class frmSettings
    Private ReadOnly jsonPath As String = IO.Path.Combine(Application.StartupPath, "System.json")

    Private Sub frmSettings_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        With My.Settings
            txtcompanyTitle.Text = .companyTitle
            txtaddressLine.Text = .companyAddressLine
        End With
    End Sub

    Private Sub TabControl1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControl1.SelectedIndexChanged
        If TabControl1.SelectedIndex = 0 Then
            lbldescription.Text = SystemCustomSettings.GetValue("settings.application.description.value")
            txtcompanyTitle.Text = SystemCustomSettings.GetValue("settings.application.companyName.value")
            txtaddressLine.Text = SystemCustomSettings.GetValue("settings.application.addressLine.value")
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        With My.Settings
            .companyTitle = txtcompanyTitle.Text
            .companyAddressLine = txtaddressLine.Text
            .Save()
        End With
    End Sub
End Class