Imports System.ComponentModel
Imports System.Globalization
Imports System.IO
Imports System.IO.Ports
Imports System.Text.RegularExpressions
Imports System.Xml
Imports Excel = Microsoft.Office.Interop.Excel

Public Class frmNFA

    Dim transactionpro_status As String = ""
    Dim pending_status As String = ""
    Dim completed_status As String = ""

    Private Sub btnpendingexport_Click(sender As Object, e As EventArgs) Handles btnpendingexport.Click
        ExportCSV(getPendingTransactionQuery, "Pending")
    End Sub
    Private Sub btncompletedexport_Click(sender As Object, e As EventArgs) Handles btncompletedexport.Click
        ExportCSV(getCompletedTransactionQuery, "Completed")
    End Sub

    Dim activeDtg As DataGridView

    Public Function getPendingTransactionQuery(Optional ByVal top_cnt As String = "") As String


        Dim searchText() As String = Split(txtsearchcompleted.Text, " ")
        Dim searchString As String = ""
        For i As Integer = 0 To searchText.Count - 1
            If (searchString <> "") Then
                searchString &= " AND "
            End If
            searchString &= $"(plate_no LIKE '%{searchText(i).ToString}%' OR 
                                        reference_no LIKE '%{searchText(i).ToString}%' OR 
                                        customer_name LIKE '%{searchText(i).ToString}%' OR 
                                        weigher LIKE '%{searchText(i).ToString}%' OR 
                                        product LIKE '%{searchText(i).ToString}%' OR 
                                        mode_of_payment LIKE '%{searchText(i).ToString}%' OR 
                                        remarks LIKE '%{searchText(i).ToString}%' OR 
                                        driver_name LIKE '%{searchText(i).ToString}%')"
        Next

        Return $"SELECT {top_cnt}
        format(transaction_date,""yyyy-MM-dd"") AS `Date`,
        reference_no,
        plate_no,
        driver_name,
        customer_name, 
        weigher,
        product, 
        mode_of_payment,
        amount, 
        remarks,
        inbound, 
        inbound_datetime, 
        keyID 
        FROM tbltransaction
        WHERE 
        archive=0 
        AND ({searchString}) 
        AND outbound <=0
        ORDER BY transaction_date DESC"
    End Function

    Public Function getCompletedTransactionQuery(Optional ByVal top_cnt As String = "") As String

        Dim searchText() As String = Split(txtsearchcompleted.Text, " ")
        Dim searchString As String = ""
        For i As Integer = 0 To searchText.Count - 1
            If (searchString <> "") Then
                searchString &= " AND "
            End If
            searchString &= $"(plate_no LIKE '%{searchText(i).ToString}%' OR 
                                        reference_no LIKE '%{searchText(i).ToString}%' OR 
                                        customer_name LIKE '%{searchText(i).ToString}%' OR 
                                        weigher LIKE '%{searchText(i).ToString}%' OR 
                                        product LIKE '%{searchText(i).ToString}%' OR 
                                        mode_of_payment LIKE '%{searchText(i).ToString}%' OR 
                                        remarks LIKE '%{searchText(i).ToString}%' OR 
                                        driver_name LIKE '%{searchText(i).ToString}%')"
        Next



        Dim date_condition As String = ""

        Select Case cbodisplayfilter.Text
            Case "All Transaction"
                date_condition = ""
            Case "Recent 30 Days"
                date_condition = " AND transaction_date > " & Format(DateAdd(DateInterval.Day, -30, Now), "#MM/dd/yyyy#")
            Case "Custom Date Range"
                date_condition = " AND (transaction_date >= " & Format(dtpfrom.Value, "#MM/dd/yyyy#") & " AND transaction_date <= " & Format(dtpto.Value, "#MM/dd/yyyy#") & ")"
        End Select

        Return $"SELECT {top_cnt} 
        format(transaction_date,""yyyy-MM-dd"") AS `Date`,
        reference_no,
        plate_no,
        driver_name,
        customer_name, 
        weigher,
        product, 
        mode_of_payment,
        amount, 
        gross_weight, 
        tare_weight, 
        net_weight,  
        remarks,
        inbound, 
        inbound_datetime, 
        outbound, 
        outbound_datetime,
        keyID 
        FROM tbltransaction
        WHERE 
        archive=0 
        AND ({searchString}) 
        AND (inbound >0 AND outbound >0) 
        {date_condition} 
        ORDER BY transaction_date DESC"

    End Function

    Private Sub PopulatePendingTransactions()
        pending_status = "Populating record to the grid..."
        PopulateDataGridView(Me.dtgpendingtransaction,
                        getPendingTransactionQuery("TOP 100"), True)
        pending_status = String.Format("{0} pending transaction(s). Ready! ", dtgpendingtransaction.RowCount)
    End Sub
    Public Sub PopulateCompletedTransaction()
        completed_status = "Populating record to the grid..."
        PopulateDataGridView(Me.dtgcompletedtransaction,
                       getCompletedTransactionQuery("TOP 100"), True)
        completed_status = String.Format("Loaded the most 100 recent transaction ({0}/100). Ready!", IIf(Me.dtgcompletedtransaction.Rows.Count > 100, 100, Me.dtgcompletedtransaction.Rows.Count))
    End Sub
    Private Sub txtsearchpending_KeyDown(sender As Object, e As KeyEventArgs) Handles txtsearchpending.KeyDown
        If e.KeyCode = Keys.Enter Then
            PopulatePendingTransactions()
        End If
    End Sub
    Private Sub btnrefreshcompleted_Click(sender As Object, e As EventArgs) Handles btnrefreshcompleted.Click
        txtsearchcompleted.Text = ""
        PopulateCompletedTransaction()
    End Sub
    Private Sub btnrefreshpending_Click(sender As Object, e As EventArgs) Handles btnrefreshpending.Click
        txtsearchpending.Text = ""
        PopulatePendingTransactions()
    End Sub
    Private Sub Initialize()

        dtgcompletedtransaction.AllowUserToResizeColumns = True
        dtgpendingtransaction.AllowUserToResizeColumns = True

        cbodisplayfilter.SelectedIndex = 1
        transactionpro_status = "Initializing components..."

        'PopulateComboBoxes()
        ClearEntry()
        UserRestrictions()

        ConnectToWeighScale()
        LoadPortInfo()

        transactionpro_status = "Ready. Standby mode"
        lblUserAccount.Text = String.Format("Login as:  {0} ({1})", thisUser.Lastname & ", " & thisUser.Firstname, thisUser.UserFunction)

    End Sub
    Public Sub UserRestrictions()

        'dashboard-transaction
        '  add/create
        menuitem_addtransaction.Enabled = IsAllowed("DT_CreateAdd")
        menuitem_addtransaction.Visible = IsAllowed("DT_CreateAdd")
        btnAddTransaction.Enabled = IsAllowed("DT_CreateAdd")
        btnAddTransaction.Visible = IsAllowed("DT_CreateAdd")
        '  edit/update
        menuitem_edittransaction.Enabled = IsAllowed("DT_EditUpdate")
        menuitem_edittransaction.Visible = IsAllowed("DT_EditUpdate")
        btnEditTransaction.Enabled = IsAllowed("DT_EditUpdate")
        btnEditTransaction.Visible = IsAllowed("DT_EditUpdate")
        '  delete/remove
        menuitem_deletetransaction.Enabled = IsAllowed("DT_DeleteRemove")
        menuitem_deletetransaction.Visible = IsAllowed("DT_DeleteRemove")
        btnDelete.Enabled = IsAllowed("DT_DeleteRemove")
        btnDelete.Visible = IsAllowed("DT_DeleteRemove")
        '  export
        btnpendingexport.Enabled = IsAllowed("DT_Export")
        btnpendingexport.Visible = IsAllowed("DT_Export")
        btncompletedexport.Enabled = IsAllowed("DT_Export")
        btncompletedexport.Visible = IsAllowed("DT_Export")

        PermissionLevelSetupToolStripMenuItem.Visible = (IsAllowed("ALS_CreateAdd") Or IsAllowed("ALS_EditUpdate") Or IsAllowed("ALS_DeleteRemove"))
        menuitem_portconfiguration.Visible = IsAllowed("PC_Allow")
        ChangeSystemResetPasswordToolStripMenuItem.Visible = IsAllowed("SR_ChangePass")

        ' special access
        txtgross.Enabled = (thisUser.UserFunction.ToString.ToLower.Contains("admin"))
        txttare.Enabled = (thisUser.UserFunction.ToString.ToLower.Contains("admin"))
        txtnet.Enabled = (thisUser.UserFunction.ToString.ToLower.Contains("admin"))
        txtInBound.Enabled = (thisUser.UserFunction.ToString.ToLower.Contains("admin"))
        txtOutBound.Enabled = (thisUser.UserFunction.ToString.ToLower.Contains("admin"))

        'del on cbolisting
        'deldriver_name.Visible = (thisUser.UserFunction.ToString.ToLower.Contains("admin"))
        'delproduct.Visible = (thisUser.UserFunction.ToString.ToLower.Contains("admin"))
        'delmode_of_payment.Visible = (thisUser.UserFunction.ToString.ToLower.Contains("admin"))
        'delcustomer_name.Visible = (thisUser.UserFunction.ToString.ToLower.Contains("admin"))
        'delweigher.Visible = (thisUser.UserFunction.ToString.ToLower.Contains("admin"))

    End Sub
    Private Sub PopulateComboBoxes()
        PopulateCbo1(cbocustomer_name, "SELECT DISTINCT Customer_Name FROM tbltransaction WHERE  FORMAT(transaction_date,""yyyy-MM-dd"") >='" & Format(DateAdd(DateInterval.Day, -30, Now()), "yyyy-MM-dd") & "'")
        PopulateCbo1(cboproduct, "SELECT DISTINCT product FROM tbltransaction WHERE  FORMAT(transaction_date,""yyyy-MM-dd"") >='" & Format(DateAdd(DateInterval.Day, -30, Now()), "yyyy-MM-dd") & "'")
        PopulateCbo1(cbodriver_name, "SELECT DISTINCT driver_name FROM tbltransaction WHERE  FORMAT(transaction_date,""yyyy-MM-dd"") >='" & Format(DateAdd(DateInterval.Day, -30, Now()), "yyyy-MM-dd") & "'")
        PopulateCbo1(cboweigher, "SELECT DISTINCT weigher FROM tbltransaction WHERE  FORMAT(transaction_date,""yyyy-MM-dd"") >='" & Format(DateAdd(DateInterval.Day, -30, Now()), "yyyy-MM-dd") & "'")
        PopulateCbo1(cbomode_of_payment, "SELECT DISTINCT mode_of_payment FROM tbltransaction WHERE  FORMAT(transaction_date,""yyyy-MM-dd"") >='" & Format(DateAdd(DateInterval.Day, -30, Now()), "yyyy-MM-dd") & "'")

        cbocustomer_name.Text = ""
        cboproduct.Text = ""
        cbodriver_name.Text = ""
        cboweigher.Text = ""
        cbomode_of_payment.Text = ""
        'ClearCboListing(cbocustomer_name)
        'ClearCboListing(cboproduct)
        'ClearCboListing(cbodriver_name)
        'ClearCboListing(cboweigher)
        'ClearCboListing(cbomode_of_payment)

    End Sub


    Private Sub frmNFA_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CheckForIllegalCrossThreadCalls = False

        ' prepare the controls 
        Initialize()

        ' populate the lists
        PopulatePendingTransactions()
        PopulateCompletedTransaction()


    End Sub

    Private Sub ConnectToWeighScale()
        Try
            weighScale.Close()
        Catch ex As Exception

        End Try
        transactionpro_status = "Connecting to weighing scale..."
        Try
            With weighScale
                .PortName = My.Settings.portname
                .ReadTimeout = My.Settings.readtimeout
                .BaudRate = My.Settings.baudrate
                .DataBits = My.Settings.databits
                .StopBits = My.Settings.stopbits
                .Open()
            End With
            lblportError.Text = ""
            transactionpro_status = "Successfully connected to the weighing scale."
        Catch ex As Exception
            transactionpro_status = "Error connecting to the scale. Check your port configuration!"
            lblportError.Text = "Error connecting to the scale. Check your port configuration!"
            lblreading.ForeColor = Color.Red
        End Try

        transactionpro_status = "Ready. Standby mode"
    End Sub

    Private Sub cbodisplayfilter_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbodisplayfilter.SelectedIndexChanged
        Panel5.Visible = (cbodisplayfilter.SelectedIndex = 2)
    End Sub

    Private Sub btnAddTransaction_Click(sender As Object, e As EventArgs) Handles btnAddTransaction.Click, menuitem_addtransaction.Click
        EditMode = False
        activeDtg = Nothing
        TabControl1.SelectedIndex = 1
        ClearEntry()
        transactionpro_status = "Entry mode, waiting for the New entries to be complete..."
    End Sub
    Private Sub menuitem_refreshall_Click(sender As Object, e As EventArgs) Handles menuitem_refreshall.Click
        Try
            PopulatePendingTransactions()
            PopulateCompletedTransaction()
            LoadPortInfo()
            ConnectToWeighScale()
        Catch ex As Exception

        End Try
    End Sub

    Sub LoadPortInfo()
        With My.Settings
            lblPort.Text = .portname
            lblreadtimeout.Text = .readtimeout
            lblbaudrate.Text = .baudrate
            lbldatabits.Text = .databits
            lblstopbits.Text = .stopbits
        End With
    End Sub

    Private Sub PortConfigurationToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles menuitem_portconfiguration.Click
        Dim frm As New frmPortConfiguration
        frm.ShowDialog()
        ConnectToWeighScale()
        menuitem_refreshall_Click(sender, e)

    End Sub
    Private Sub btnEditTransaction_Click(sender As Object, e As EventArgs) Handles btnEditTransaction.Click, dtgcompletedtransaction.DoubleClick, menuitem_edittransaction.Click
        Try
            LoadRecord(activeDtg.SelectedRows.Item(0).Cells(activeDtg.ColumnCount - 1).Value)
            TabControl1.SelectedIndex = 1
        Catch ex As Exception
            MessageBox.Show(Me, "Unable to perform Edit. Operation terminated!", "Transaction", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click, menuitem_deletetransaction.Click

        Dim forDelete As String = ""
        For i As Integer = 0 To activeDtg.Rows.Count - 1
            If Not (activeDtg.Rows(i).Cells(0).Value = 0 And activeDtg.Rows(i).Cells(0).Value = False) Then
                forDelete &= IIf(forDelete <> "", "," & activeDtg.Rows(i).Cells(activeDtg.ColumnCount - 1).Value, activeDtg.Rows(i).Cells(activeDtg.ColumnCount - 1).Value)
            End If
        Next

        If (forDelete <> "") Then
            Dim ask As MsgBoxResult = MessageBox.Show("Are you sure you want to delete the selected records? ", "Delete User Account", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
            If ask = MsgBoxResult.Yes Then
                Dim d As New clsDataManipulation
                d.Exec("UPDATE tbltransaction SET archive=1, archive_by='" & thisUser.FullName & "', archived_date='" & Format(Now(), "yyyy-MM-dd") & "' where keyid in (" & forDelete & ")")
                MessageBox.Show("Record/s successfully deleted!", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        End If

        PopulateCompletedTransaction()
        PopulatePendingTransactions()
    End Sub

    Dim checkedKeyids As String = ""
    Function CountCheckedRows() As Integer
        If activeDtg Is Nothing Then Return 0
        Dim ctr As Integer = 0
        Dim row As DataGridViewRowCollection = activeDtg.Rows
        For i As Integer = 0 To row.Count - 1
            If (CType(row.Item(i).Cells(0).Value, Boolean) = True) Then
                ctr += 1
                checkedKeyids &= IIf(checkedKeyids <> "", row.Item(i).Cells(activeDtg.ColumnCount - 1).Value & ",", row.Item(i).Cells(activeDtg.ColumnCount - 1).Value)
            End If
        Next
        Return ctr
    End Function

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Panel5.Visible = False
    End Sub

    Private Sub btnApply_Click(sender As Object, e As EventArgs) Handles btnApply.Click
        Panel5.Visible = False
        PopulateCompletedTransaction()
    End Sub

    Private Sub ExportCSV(querySelection As String, ExportOn As String)

        Dim filename As String = ""

        If ExportOn = "Pending" Then
            pending_status = "Exporting pending transation, please wait..."
            filename = "WSTP_Pending_" & Format(Now(), "yyyyMMdd_hhmmss") & ".csv"
        Else
            completed_status = "Exporting completed transation, please wait..."
            filename = "WSTP_Completed_" & Format(Now(), "yyyyMMdd_hhmmss") & ".csv"
        End If

        Dim saveFileDialog As New SaveFileDialog
        saveFileDialog.FileName = filename
        saveFileDialog.DefaultExt = ".csv"


        If (saveFileDialog.ShowDialog() = DialogResult.Cancel) Then
            Exit Sub
        End If

        Dim sWriter As New StreamWriter(saveFileDialog.FileName)

        Dim header As String = ""

        Dim d As New clsDataManipulation
        With d
            If (.Fetch(querySelection)) Then
                With .DataReader
                    For x As Integer = 0 To .FieldCount - 2
                        header &= IIf(header <> "", ",""" & .GetName(x) & """", """" & .GetName(x) & """")
                    Next
                    header = header.ToUpper()
                    sWriter.WriteLine(header)
                    Do While .Read
                        Dim content As String = ""
                        For x As Integer = 0 To .FieldCount - 2
                            content &= IIf(content <> "", ",""" & .Item(x).ToString() & """", """" & .Item(x).ToString() & """")
                        Next
                        sWriter.WriteLine(content)
                    Loop
                    .Close()
                End With
            End If
        End With
        sWriter.Close()

        MessageBox.Show(Me, "Export successfully finished!", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information)
        transactionpro_status = "System export complete!"

        If ExportOn = "Pending" Then
            pending_status = filename & " has been created! Standby Mode"
        Else
            completed_status = filename & " has been created! Standby Mode"
        End If

    End Sub

    Private Sub UserAccountsToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles menuitem_useraccounts.Click
        If (IsAllowed("UA_CreateAdd") Or IsAllowed("UA_EditUpdate") Or IsAllowed("UA_DeleteRemove")) Then
            Dim frm As New frmUserAccounts
            frm.ShowDialog(Me)
        Else
            If (IsAllowed("OA_Allow")) Then
                Dim frm As New frmUserRegistration
                frm.EditMode = True
                frm.OwnAccount = True
                frm.RecNo = thisUser.keyid
                frm.ShowDialog()
            End If
        End If

    End Sub

    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click
        Me.Close()
    End Sub
    Private Sub frmNFA_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        weighScale.Close()
        End
    End Sub
    Function ReplaceAll(ByVal strText As String) As String
        Dim RegExp As Object
        RegExp = CreateObject("VBScript.RegExp")
        With RegExp
            .Pattern = "([^0-9 )])"
            .Global = True
            .IgnoreCase = False
            .MultiLine = True
            ReplaceAll = .Replace(strText, "") ' " $1"
        End With
    End Function
    Private Sub weighScale_DataReceived(sender As Object, e As Ports.SerialDataReceivedEventArgs) Handles weighScale.DataReceived


        Dim str As String = weighScale.ReadLine()
        Dim noletters As String = Regex.Replace(str, "[a-z]", String.Empty)
        noletters = Regex.Replace(noletters, "[A-Z]", String.Empty)
        noletters = Regex.Replace(noletters, "[, ]", String.Empty)
        lblreading.Text = noletters



        'Dim str As String = weighScale.ReadExisting()
        'Dim str2 As String = Replace(ReplaceAll(str), "  ", " ")
        'If str2.Length > 5 Then
        '    'TextBox2.Text = str2
        '    Dim str3() As String = Split(str2, " ")

        '    Dim rIndex As Integer = 0
        '    For i As Integer = 0 To str3.Count - 1
        '        If Val(str3(i)) > 0 Then
        '            rIndex = i
        '            Exit For
        '        End If
        '    Next
        '    lblreading.Text = Val(str3(rIndex))

        'TextBox1.Text = Val(str3(0)) & " " & Val(str3(1)) & " " & Val(str3(2))


    End Sub

    Private Sub btnClearEntry_Click(sender As Object, e As EventArgs) Handles btnClearEntry.Click
        ClearEntry()
    End Sub
    Public Sub ClearEntry()
        txtInBound.Enabled = (thisUser.UserFunction.ToString.ToLower.Contains("admin"))
        txtOutBound.Enabled = (thisUser.UserFunction.ToString.ToLower.Contains("admin"))

        txtremarks.Text = ""
        txtreferenceno.Text = GetLatestTicketNo()
        cbocustomer_name.Text = ""
        cboproduct.Text = ""
        cbodriver_name.Text = ""
        txtplateno.Text = ""
        cboweigher.Text = ""
        cbomode_of_payment.Text = ""
        txtamount.Text = "0"

        txttransactiondate.Text = Format(Now, "yyyy-MM-dd")


        txtgross.Text = "0"
        txttare.Text = "0"
        txtnet.Text = "0"

        lblinbounddate.Text = ""
        lbloutbounddate.Text = ""
        txtOutBound.Text = 0
        txtInBound.Text = 0

        dtpfrom.Text = Now
        dtpto.Text = Now

        txtreferenceno.Focus()

        'chkGQ.Checked = True


        btnreprint.Visible = False
        'btnSave.Visible = Not (btnreprint.Visible)
        'btnsaveandprint.Visible = Not (btnreprint.Visible) And (IsAllowed("DT_SAVEPRINT"))

        panel_transaction.Enabled = True

        PopulateComboBoxes()

        cbodriver_name.Enabled = True
        cbocustomer_name.Enabled = True
        cboweigher.Enabled = True
        cboproduct.Enabled = True
        txtplateno.Enabled = True
        cbomode_of_payment.Enabled = True
        txtamount.Enabled = True
        txtremarks.Enabled = True

        btnSave.Visible = True
        btnsaveandprint.Visible = True

    End Sub
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Record()
    End Sub
    Private Sub btnsaveandprint_Click(sender As Object, e As EventArgs) Handles btnsaveandprint.Click
        Record()
        If (RecSaveandPrint) Then Print(txtreferenceno.Text)
    End Sub

    Dim EditMode As Boolean = False
    Dim RecSaveandPrint As Boolean = False
    Private Sub Record()
        RecSaveandPrint = False

        Dim current_user As String = thisUser.FullName

        Dim d As New clsDataManipulation

        Dim cmdText As String = ""
        If Not (EditMode) Then
            transactionpro_status = "Saving transaction..."
            If MessageBox.Show(Me, "Are you sure you want to save this transaction?", "New Transaction", MsgBoxStyle.YesNo, MsgBoxStyle.Question) = MsgBoxResult.Yes Then
                Try
                    d.Insert("tbltransaction",
                        {
                            "s reference_no            => " & txtreferenceno.Text,
                            "d transaction_date        => " & txttransactiondate.Text,
                            "s plate_no                => " & txtplateno.Text,
                            "s driver_name           => " & cbodriver_name.Text,
                            "s customer_name           => " & cbocustomer_name.Text,
                            "s weigher           => " & cboweigher.Text,
                            "s product                 => " & cboproduct.Text,
                            "s remarks                 => " & txtremarks.Text,
                            "s mode_of_payment         => " & cbomode_of_payment.Text,
                            "n amount                  => " & Val(txtamount.Text),
                            "n inbound                 => " & txtInBound.Text,
                            "d inbound_datetime        => " & lblinbounddate.Text,
                            "n outbound                => " & txtOutBound.Text,
                            "d outbound_datetime       => " & lbloutbounddate.Text,
                            "n gross_weight            => " & txtgross.Text,
                            "d gross_captured_date     => " & gross_datetime,
                            "n tare_weight             => " & txttare.Text,
                            "d tare_captured_date      => " & tare_datetime,
                            "n net_weight              => " & txtnet.Text,
                            "s transacted_by           => " & thisUser.FullName
                        }
                     )
                    transactionpro_status = "Transaction saved!"
                    RecSaveandPrint = True
                    TabControl1.SelectedIndex = 0
                Catch ex As Exception
                    MsgBox(ex.Message & vbNewLine & vbNewLine & "Please contact your administrator immediately!", MsgBoxStyle.OkOnly & MsgBoxStyle.Critical, "Error")
                    transactionpro_status = "Saving transaction encountered error! Contact your admin!"
                End Try
            End If
        Else
            transactionpro_status = "Updating transaction..."
            If MessageBox.Show(Me, "Are you sure you want to update this transaction?", "Update Transaction", MsgBoxStyle.YesNo, MsgBoxStyle.Question) = MsgBoxResult.Yes Then
                Try
                    d.Update("tbltransaction",
                       {
                            "s reference_no            => " & txtreferenceno.Text,
                            "d transaction_date        => " & txttransactiondate.Text,
                            "s plate_no                => " & txtplateno.Text,
                            "s driver_name           => " & cbodriver_name.Text,
                            "s customer_name           => " & cbocustomer_name.Text,
                            "s weigher           => " & cboweigher.Text,
                            "s product                 => " & cboproduct.Text,
                            "s remarks                 => " & txtremarks.Text,
                            "s mode_of_payment         => " & cbomode_of_payment.Text,
                            "n amount                  => " & Val(txtamount.Text),
                            "n inbound                 => " & txtInBound.Text,
                            "d inbound_datetime        => " & lblinbounddate.Text,
                            "n outbound                => " & txtOutBound.Text,
                            "d outbound_datetime       => " & lbloutbounddate.Text,
                            "n gross_weight            => " & txtgross.Text,
                            "d gross_captured_date     => " & gross_datetime,
                            "n tare_weight             => " & txttare.Text,
                            "d tare_captured_date      => " & tare_datetime,
                            "n net_weight              => " & txtnet.Text,
                            "s transacted_by           => " & thisUser.FullName
                        },
                        {"keyid IN (" & txttransactionid.Text & ")"}
                     )
                    transactionpro_status = "Transaction udpated!"
                    RecSaveandPrint = True
                    TabControl1.SelectedIndex = 0
                Catch ex As Exception
                    MsgBox(ex.Message & vbNewLine & vbNewLine & "Please contact your administrator immediately!", MsgBoxStyle.OkOnly & MsgBoxStyle.Critical, "Error")
                    transactionpro_status = "Transaction update encountered error! Contact your admin!"
                End Try
            End If
        End If

        'this will remove the cbo value to the cbo listing limitation
        RemoveFromDeletedCboListing("driver_name", cbodriver_name.Text)
        RemoveFromDeletedCboListing("customer_name", cbocustomer_name.Text)
        RemoveFromDeletedCboListing("weigher", cboweigher.Text)
        RemoveFromDeletedCboListing("product", cboproduct.Text)
        RemoveFromDeletedCboListing("mode_of_payment", cbomode_of_payment.Text)

        PopulatePendingTransactions()
        PopulateCompletedTransaction()
    End Sub


    Dim gross_datetime As String = ""
    Dim tare_datetime As String = ""
    Private Sub btnW2_Click(sender As Object, e As EventArgs) Handles btnW2.Click
        BeginCapture(txtOutBound)
    End Sub
    Private Sub btnW1_Click(sender As Object, e As EventArgs) Handles btnW1.Click
        BeginCapture(txtInBound)
    End Sub
    Private Sub BeginCapture(destinationTextbox As TextBox)
        transactionpro_status = "Capturing reading..."

        destinationTextbox.Text = Val(lblreading.Text)

        If destinationTextbox.Name = "txtInBound" Then
            lblinbounddate.Text = Format(Now, "yyyy-MM-dd hh:mm:ss tt")
        Else
            lbloutbounddate.Text = Format(Now, "yyyy-MM-dd hh:mm:ss tt")
        End If

        If Val(txtInBound.Text) > Val(txtOutBound.Text) Then
            txtgross.Text = txtInBound.Text
            gross_datetime = lblinbounddate.Text
            txttare.Text = txtOutBound.Text
            tare_datetime = lbloutbounddate.Text
        Else
            txtgross.Text = txtOutBound.Text
            gross_datetime = lbloutbounddate.Text
            txttare.Text = txtInBound.Text
            tare_datetime = lblinbounddate.Text
        End If
        txtnet.Text = Val(Val(txtgross.Text) - Val(txttare.Text))
        transactionpro_status = "Reading captured!"

    End Sub

    Function printBuffer(str As String, l As Integer) As String
        printBuffer = str
        For i As Integer = Len(str) To l
            printBuffer &= vbFormFeed
        Next
        Return str
    End Function
    Public Sub PrepPrint(ticketno As String)
        thisTransanction = New Transaction
        Dim stock_condition As String = ""
        Dim d As New clsDataManipulation
        With d
            If (.Fetch("select * from tbltransaction where reference_no = '" & ticketno & "'")) Then
                With .DataReader
                    Do While .Read
                        thisTransanction.ReferenceNo = .Item("reference_no").ToString
                        thisTransanction.TransactionDate = .Item("transaction_date").ToString
                        thisTransanction.DriverName = .Item("driver_name").ToString
                        thisTransanction.CustomerName = .Item("customer_name").ToString
                        thisTransanction.ModeOfPayment = .Item("mode_of_payment").ToString
                        thisTransanction.Product = .Item("product").ToString
                        thisTransanction.PlateNo = .Item("plate_no").ToString
                        thisTransanction.Gross = .Item("gross_weight").ToString
                        thisTransanction.GrossCapturedDate = .Item("gross_captured_date").ToString
                        thisTransanction.Tare = .Item("tare_weight").ToString
                        thisTransanction.TareCapturedDate = .Item("tare_captured_date")
                        thisTransanction.Weigher = .Item("weigher").ToString
                        thisTransanction.Amount = .Item("amount").ToString
                        thisTransanction.Net = .Item("net_weight").ToString
                        thisTransanction.Remarks = .Item("remarks").ToString
                    Loop
                    .Close()
                End With

            End If
        End With

    End Sub
    Private Sub Print(ticketno As String)

        PrepPrint(ticketno)

        Dim template As String = Environment.CurrentDirectory & "\_Print_Master.xlsx"
        Dim thisOne As String = ""
        thisOne = Environment.CurrentDirectory & "\_Print.xlsx"

        ' delete the file first
        Try
            Kill(thisOne)
        Catch ex As Exception

        End Try


        ' preparation of the transaction information
        Dim xlApp As Excel.Application = New Excel.Application
        xlApp.DisplayAlerts = False
        Dim xlWorkBook As Excel.Workbook
        Dim xlWorkSheet As Excel.Worksheet
        xlWorkBook = xlApp.Workbooks.Open(template)
        xlWorkSheet = xlWorkBook.Worksheets(1)

        With thisTransanction
            xlWorkSheet.UsedRange.Replace("wstp_transactiondate", .TransactionDate)
            xlWorkSheet.UsedRange.Replace("wstp_reference_no", .ReferenceNo)
            xlWorkSheet.UsedRange.Replace("wstp_plateno", .PlateNo)
            xlWorkSheet.UsedRange.Replace("wstp_customer_name", .CustomerName)
            xlWorkSheet.UsedRange.Replace("wstp_product", .Product)
            xlWorkSheet.UsedRange.Replace("wstp_grosscapturedate", .GrossCapturedDate)
            xlWorkSheet.UsedRange.Replace("wstp_tarecapturedate", .TareCapturedDate)
            xlWorkSheet.UsedRange.Replace("wstp_amount", .Amount)
            xlWorkSheet.UsedRange.Replace("wstp_mode_of_payment", .ModeOfPayment)
            xlWorkSheet.UsedRange.Replace("wstp_driver_name", .DriverName)
            xlWorkSheet.UsedRange.Replace("wstp_weigher", .Weigher)
            xlWorkSheet.UsedRange.Replace("wstp_gross", .Gross)
            xlWorkSheet.UsedRange.Replace("wstp_tare", .Tare)
            xlWorkSheet.UsedRange.Replace("wstp_net", .Net)
            xlWorkSheet.UsedRange.Replace("wstp_remarks", .Remarks)
        End With

        xlWorkBook.SaveAs(thisOne)

        xlWorkBook.Close()
        xlApp.Quit()

        releaseObject(xlApp)
        releaseObject(xlWorkBook)
        releaseObject(xlWorkSheet)

        BeginPrint(thisOne)

    End Sub

    Private Sub BeginPrint(the_file As String)
        Dim monProcess As New Process()
        monProcess.StartInfo.FileName = the_file
        monProcess.StartInfo.Verb = "Print"
        monProcess.StartInfo.CreateNoWindow = True
        monProcess.Start()

        Process.Start("cmd", "/c taskkill /f /im excel.exe")

        TabControl1.SelectedIndex = 0
    End Sub
    Private Sub releaseObject(ByVal obj As Object)
        Try
            System.Runtime.InteropServices.Marshal.ReleaseComObject(obj)
            obj = Nothing
        Catch ex As Exception
            obj = Nothing
        Finally
            GC.Collect()
        End Try
    End Sub

    Sub Calc()
        If Val(txtInBound.Text) > Val(txtOutBound.Text) Then
            txtgross.Text = txtInBound.Text
            gross_datetime = lblinbounddate.Text
            txttare.Text = txtOutBound.Text
            tare_datetime = lbloutbounddate.Text
        Else
            txtgross.Text = txtOutBound.Text
            gross_datetime = lbloutbounddate.Text
            txttare.Text = txtInBound.Text
            tare_datetime = lblinbounddate.Text
        End If
        txtnet.Text = Val(Val(txtgross.Text) - Val(txttare.Text))
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        lblstatus.Text = String.Format("State: {0}", transactionpro_status)
        lbleditstatus.Text = String.Format("Edit Mode: {0}", EditMode)
        lblportStatus.Text = String.Format("Port: {0} ({1})", My.Settings.portname, IIf((weighScale.IsOpen), "Open", "Closed"))

        LoadPortInfo()

        If Not (weighScale.IsOpen()) Or lblreading.Text = "00000" Then
            transactionpro_status = "Error connecting to the scale. Check your port configuration!"
            lblportError.Text = "Error connecting to the scale. Check your port configuration!"
            lblreading.ForeColor = Color.Red
        Else
            lblreading.ForeColor = Color.Lime
        End If

        ' capture button
        btnW1.Enabled = (Not (EditMode) And Val(lblreading.Text) > 0)

        If (activeDtg IsNot Nothing) Then
            btnW2.Enabled = ((EditMode) And Val(lblreading.Text) > 0) And Not (activeDtg.Name.Contains("complete"))
        Else
            btnW2.Enabled = ((EditMode) And Val(lblreading.Text) > 0)
        End If
        PictureBox5.Visible = btnW2.Enabled
        PictureBox4.Visible = btnW1.Enabled

        lblpendingstatus.Text = pending_status
        lblcompletedstatus.Text = completed_status

        btnpendingexport.Enabled = (dtgpendingtransaction.RowCount > 0)
        btncompletedexport.Enabled = (dtgcompletedtransaction.RowCount > 0)

        btnSave.Enabled = AllRequireFieldsHasInput() 'And (IsAllowed("DT_CreateAdd") Or IsAllowed("DT_EditUpdate")) And (activeDtg.Name = dtgpendingtransaction.Name)

        btnsaveandprint.Enabled = AllRequireFieldsHasInput() And (Val(txtOutBound.Text) > 0) 'And (IsAllowed("DT_CreateAdd") Or IsAllowed("DT_EditUpdate")) And (activeDtg.Name = dtgpendingtransaction.Name)

        Try
            Calc()
        Catch ex As Exception

        End Try
    End Sub


    Private Sub LoadRecord(transactionID As String)
        ClearEntry()

        EditMode = True
        transactionpro_status = "Updating transaction, waiting to apply changes..."
        Dim d As New clsDataManipulation
        With d
            If (.Fetch("SELECT * FROM tbltransaction WHERE keyid IN (" & transactionID & ") And archive=0")) Then
                With .DataReader
                    Do While .Read

                        Dim enableDisable As Boolean = False

                        If activeDtg.Name.ToLower().Contains("complete") Then
                            cbodriver_name.Enabled = IsAllowed("CT_Allow")
                            cbocustomer_name.Enabled = IsAllowed("CT_Allow")
                            cboweigher.Enabled = IsAllowed("CT_Allow")
                            cboproduct.Enabled = IsAllowed("CT_Allow")
                            txtplateno.Enabled = IsAllowed("CT_Allow")
                            cbomode_of_payment.Enabled = IsAllowed("CT_Allow")
                            txtamount.Enabled = IsAllowed("CT_Allow")
                            txtremarks.Enabled = IsAllowed("CT_Allow")

                            txtgross.Enabled = False
                            txttare.Enabled = False
                            txtnet.Enabled = False
                            txtInBound.Enabled = False
                            txtOutBound.Enabled = False
                        Else
                            cbodriver_name.Enabled = IsAllowed("DT_EditUpdate")
                            cbocustomer_name.Enabled = IsAllowed("DT_EditUpdate")
                            cboweigher.Enabled = IsAllowed("DT_EditUpdate")
                            cboproduct.Enabled = IsAllowed("DT_EditUpdate")
                            txtplateno.Enabled = IsAllowed("DT_EditUpdate")
                            cbomode_of_payment.Enabled = IsAllowed("DT_EditUpdate")
                            txtamount.Enabled = IsAllowed("DT_EditUpdate")
                            txtremarks.Enabled = IsAllowed("DT_EditUpdate")

                            txtInBound.Enabled = (thisUser.UserFunction.ToString.ToLower.Contains("admin"))
                            txtOutBound.Enabled = (thisUser.UserFunction.ToString.ToLower.Contains("admin"))
                            txtgross.Enabled = (IsAllowed("CT_Allow"))
                            txttare.Enabled = (IsAllowed("CT_Allow"))
                            txtnet.Enabled = (IsAllowed("CT_Allow"))
                        End If



                        txttransactionid.Text = .Item("keyid").ToString()

                        txtreferenceno.Text = .Item("reference_no").ToString()
                        cbodriver_name.Text = .Item("driver_name").ToString()
                        cbocustomer_name.Text = .Item("customer_name").ToString()
                        cboweigher.Text = .Item("weigher").ToString()
                        cboproduct.Text = .Item("product").ToString()
                        txtplateno.Text = .Item("plate_no").ToString()
                        cbomode_of_payment.Text = .Item("mode_of_payment").ToString
                        txtamount.Text = .Item("amount").ToString

                        txttransactiondate.Text = Format(Date.Parse(.Item("transaction_date").ToString()), "yyyy-MM-dd")


                        txtgross.Text = .Item("gross_weight").ToString()
                        gross_datetime = .Item("gross_captured_date").ToString()
                        txttare.Text = .Item("tare_weight").ToString()
                        tare_datetime = .Item("tare_captured_date").ToString()
                        txtnet.Text = .Item("net_weight").ToString()

                        ' inbound
                        txtInBound.Text = .Item("inbound").ToString()
                        lblinbounddate.Text = .Item("inbound_datetime").ToString()

                        ' outbound
                        txtOutBound.Text = .Item("outbound").ToString()
                        lbloutbounddate.Text = .Item("outbound_datetime").ToString()

                        txtremarks.Text = .Item("remarks").ToString()

                        'set buttons
                        btnSave.Visible = False
                        btnsaveandprint.Visible = False
                        btnreprint.Visible = False
                        btnreprintforGuest.Visible = False

                        If Not (activeDtg.Name = dtgcompletedtransaction.Name) Then
                            btnreprintforGuest.Visible = IsAllowed("DT_PRINTPENDING")
                            btnsaveandprint.Visible = (IsAllowed("DT_CreateAdd"))
                            btnSave.Visible = (IsAllowed("DT_CreateAdd"))
                            panel_transaction.Enabled = True

                            'btnSave.Visible = True
                            'txtgross.Enabled = True
                            'txttare.Enabled = True
                            'txtnet.Enabled = True
                            'txtInBound.Enabled = True
                            'txtOutBound.Enabled = True
                        Else
                            txtgross.Enabled = False
                            txttare.Enabled = False
                            txtnet.Enabled = False

                            btnsaveandprint.Enabled = False
                            btnSave.Visible = (IsAllowed("CT_Allow"))
                            btnreprint.Visible = IsAllowed("DT_PRINTCOMPLETED")
                            panel_transaction.Enabled = IsAllowed("CT_Allow")
                        End If

                    Loop
                    .Close()
                End With

            End If
        End With
    End Sub

    Private Sub btncanceltransaction_Click(sender As Object, e As EventArgs) Handles btncanceltransaction.Click
        ClearEntry()
        TabControl1.SelectedIndex = 0
    End Sub

    Public Function IsPortActive() As Boolean
        IsPortActive = False
        For Each port As String In SerialPort.GetPortNames
            If My.Settings.portname = port Then
                IsPortActive = True
                Exit For
            End If
        Next
        Return IsPortActive
    End Function

    Private Sub txtplateno_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtplateno.KeyPress
        e.KeyChar = e.KeyChar.ToString.ToUpper
    End Sub
    Private Sub dtgpendingtransaction_DoubleClick(sender As Object, e As EventArgs) Handles dtgpendingtransaction.DoubleClick
        LoadRecord(dtgpendingtransaction.SelectedRows.Item(0).Cells(dtgpendingtransaction.ColumnCount - 1).Value)
        TabControl1.SelectedIndex = 1
    End Sub
    Private Sub txtInBound_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtInBound.KeyPress, txtOutBound.KeyPress
        If (thisUser.UserFunction.ToString.ToLower.Contains("admin")) Then
            If Not IsNumeric(e.KeyChar) And e.KeyChar <> vbBack Then
                e.KeyChar = ""
            End If
        Else
            e.KeyChar = ""
        End If
    End Sub
    Private Sub dtgpendingtransaction_Click(sender As Object, e As EventArgs) Handles dtgpendingtransaction.Click
        activeDtg = dtgpendingtransaction
    End Sub

    Private Sub dtgcompletedtransaction_Click(sender As Object, e As EventArgs) Handles dtgcompletedtransaction.Click
        activeDtg = dtgcompletedtransaction
    End Sub
    Private Sub txtweigh_of_sack_KeyPress(sender As Object, e As KeyPressEventArgs)
        If Not IsNumeric(e.KeyChar) And e.KeyChar <> "." And e.KeyChar <> vbBack Then
            e.KeyChar = ""
        End If
    End Sub
    Private Sub txtsearchcompleted_KeyDown(sender As Object, e As KeyEventArgs) Handles txtsearchcompleted.KeyDown
        If e.KeyCode = Keys.Enter Then
            PopulateCompletedTransaction()
        End If
    End Sub

    Private Sub btnsearchpending_Click(sender As Object, e As EventArgs) Handles btnsearchpending.Click
        PopulatePendingTransactions()
    End Sub

    Private Sub btnsearchcompleted_Click(sender As Object, e As EventArgs) Handles btnsearchcompleted.Click
        PopulateCompletedTransaction()
    End Sub



    Private Function IsNotPending(plateNo As String) As Boolean
        IsNotPending = False

        Dim count As Integer = 0
        Dim d As New clsDataManipulation
        With d
            If (.Fetch("SELECT COUNT(*) FROM tbltransaction WHERE archive=0 AND (
                plate_no = '" & FixApostrophe(plateNo) & "' AND outbound <=0)")) Then
                With .DataReader
                    Do While .Read
                        count = Val(.Item(0).ToString())
                    Loop
                    .Close()
                End With

            End If
        End With
        IsNotPending = (count <= 0)

        Return IsNotPending
    End Function

    Private Sub txtplateno_Leave(sender As Object, e As EventArgs) Handles txtplateno.Leave
        If Not (IsNotPending(txtplateno.Text)) And Not (EditMode) Then
            MessageBox.Show(Me, "Operation denied, PlateNo has a pending transaction!", "Transaction Validation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            txtplateno.SelectionStart = 0
            txtplateno.SelectionLength = txtplateno.TextLength
            txtplateno.Select()
        End If
    End Sub

    Private Function AllRequireFieldsHasInput() As Boolean

        AllRequireFieldsHasInput = (FixSpaces(txtreferenceno.Text) <> "" And
            FixSpaces(cbocustomer_name.Text) <> "" And
            FixSpaces(txtplateno.Text) <> "" And
            FixSpaces(cbocustomer_name.Text) <> "" And
            FixSpaces(cboweigher.Text) <> "" And
            FixSpaces(cboproduct.Text) <> "" And
            FixSpaces(cbomode_of_payment.Text) <> "" And
            Val(txtInBound.Text) > 0)

        If (EditMode) And (Not (IsAllowed("DT_EditUpdate"))) Then
            AllRequireFieldsHasInput = (Val(txtOutBound.Text) > 0)
        End If

        Return AllRequireFieldsHasInput
    End Function

    Private Sub _GotFocusCbo(sender As Object, e As EventArgs)

        Dim txt As ComboBox = CType(sender, ComboBox)
        txt.SelectionStart = 0
        txt.SelectionLength = txt.Text.Length
        txt.Select()
    End Sub

    Private Sub _FormatDecimal(sender As Object, e As EventArgs)

        Dim txt As ComboBox = CType(sender, ComboBox)
        Dim valStr As Decimal = Val(txt.Text)
        txt.Text = valStr.ToString("n3")
    End Sub

    Private Sub btnreprint_Click(sender As Object, e As EventArgs) Handles btnreprint.Click, btnreprintforGuest.Click
        If (MessageBox.Show("Are you sure you want to re-print the selected record?", "Print confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes) Then
            Print(txtreferenceno.Text)
        End If
    End Sub




    Private Sub Button2_Click(sender As Object, e As EventArgs)
        PrintDialog1.Document = PrintDocument1
        PrintDialog1.PrinterSettings = PrintDocument1.PrinterSettings
        PrintDialog1.AllowSomePages = True

        If PrintDialog1.ShowDialog = DialogResult.OK Then
            PrintDocument1.PrinterSettings = PrintDialog1.PrinterSettings
            PrintDocument1.Print()
        End If
    End Sub

    Private Sub BackgroundWorker1_DoWork(sender As Object, e As DoWorkEventArgs) Handles BackgroundWorker1.DoWork

    End Sub

    Private Sub Button2_Click_1(sender As Object, e As EventArgs) Handles Button2.Click
        txtOutBound.Text = "5"
        lbloutbounddate.Text = Format(Now, "yyyy-MM-dd hh:mm:ss tt")

        'lblinbounddate.Text = Format(Now, "yyyy-MM-dd hh:mm:ss tt")


        'transactionpro_status = "Reading captured!"
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        'Record()
        txtInBound.Text = 10
        lblinbounddate.Text = Format(Now, "yyyy-MM-dd hh:mm:ss tt")
        'Print(txtticketno.Text)
    End Sub

    Private Sub txtamount_TextChanged(sender As Object, e As EventArgs) Handles txtamount.TextChanged

    End Sub

    Private Sub txtamount_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtamount.KeyPress
        If Not IsNumeric(e.KeyChar) And e.KeyChar <> vbBack Then
            e.KeyChar = ""
        End If
    End Sub

    Private Sub txtamount_Leave(sender As Object, e As EventArgs) Handles txtamount.Leave
        Dim amnt As Decimal = Val(txtamount.Text)
        txtamount.Text = amnt.ToString("n2")
    End Sub

    Private Sub Label47_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub menuitem_exit_Click(sender As Object, e As EventArgs) Handles menuitem_exit.Click
        End
    End Sub

    Private Sub PermissionLevelSetupToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PermissionLevelSetupToolStripMenuItem.Click
        Dim frm As New frmAccessLevelSettings
        frm.ShowDialog()
    End Sub

    Private Sub ChangeSystemResetPasswordToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ChangeSystemResetPasswordToolStripMenuItem.Click
        Dim d As New clsDataManipulation, ask As String = InputBox("Please input new password for ""System Reset"" :", "Password Reset (""System Reset"" )", "<password here>")

        If ask <> "<password here>" Then
            d.Exec("update tblsystem set reset_password = '" & TextEncode(ask.ToString) & "' where keyid = " & GetMaxKeyID("select max(keyid) from tblsystem where reset_password <> ''"))
        End If

    End Sub

    Private Sub txtamount_GotFocus(sender As Object, e As EventArgs) Handles txtamount.GotFocus

    End Sub

    Private Sub txtamount_Click(sender As Object, e As EventArgs) Handles txtamount.Click
        txtamount.SelectionStart = 0
        txtamount.SelectionLength = txtamount.TextLength
        txtamount.SelectAll()
    End Sub

    Private Sub delcbolisting(sender As Object, e As EventArgs) Handles _
        deldriver_name.Click,
        delcustomer_name.Click,
        delmode_of_payment.Click,
        delproduct.Click,
        delweigher.Click

        If Not (thisUser.UserFunction.ToString.ToLower.Contains("admin")) Then
            Return
        End If


        Dim btn As Button = CType(sender, Button)

        AddToCboListing(btn.Name.Substring(3, btn.Name.Length - 3))

        PopulateComboBoxes()

    End Sub


    Private Sub RemoveFromDeletedCboListing(fieldname As String, fieldvalue As String)

        Dim sql As String = String.Format("update tblcbolisting set {0}=null where {0} = '{1}'", fieldname, fieldvalue)
        Dim d As New clsDataManipulation
        Try
            d.Exec(sql)
        Catch ex As Exception
            MessageBox.Show(Me, ex.Message, "Exception Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub AddToCboListing(fieldname As String)
        Dim cbo As ComboBox
        For Each ctl As Control In panel_transaction.Controls
            If (TypeOf ctl Is ComboBox) And ctl.Name = "cbo" & fieldname Then
                cbo = ctl
            End If
        Next

        Dim d As New clsDataManipulation
        Try
            d.Fetch(String.Format("select * from tblcbolisting where {0} = '{1}'", fieldname, cbo.Text))
            If Not (d.DataReader.HasRows()) Then
                d.Exec(String.Format("insert into tblcbolisting ({0}) values ('{1}')", fieldname, cbo.Text))
            End If
            d.DataReader.Close()
        Catch ex As Exception
            MessageBox.Show(Me, ex.Message, "Exception Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try


    End Sub

End Class
