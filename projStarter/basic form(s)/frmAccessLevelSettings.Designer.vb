<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmAccessLevelSettings
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAccessLevelSettings))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cbopermisionkey = New System.Windows.Forms.ComboBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.CT_Allow = New System.Windows.Forms.CheckBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.DT_PRINTCOMPLETED = New System.Windows.Forms.CheckBox()
        Me.DT_PRINTPENDING = New System.Windows.Forms.CheckBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.SR_ChangePass = New System.Windows.Forms.CheckBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.OA_Allow = New System.Windows.Forms.CheckBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.ALS_DeleteRemove = New System.Windows.Forms.CheckBox()
        Me.ALS_EditUpdate = New System.Windows.Forms.CheckBox()
        Me.ALS_CreateAdd = New System.Windows.Forms.CheckBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.UA_DeleteRemove = New System.Windows.Forms.CheckBox()
        Me.UA_EditUpdate = New System.Windows.Forms.CheckBox()
        Me.UA_CreateAdd = New System.Windows.Forms.CheckBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.PC_Allow = New System.Windows.Forms.CheckBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.DT_Export = New System.Windows.Forms.CheckBox()
        Me.DT_DeleteRemove = New System.Windows.Forms.CheckBox()
        Me.DT_EditUpdate = New System.Windows.Forms.CheckBox()
        Me.DT_CreateAdd = New System.Windows.Forms.CheckBox()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.btnClearEntry = New System.Windows.Forms.Button()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.ImageList3 = New System.Windows.Forms.ImageList(Me.components)
        Me.btnremove = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 68)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(165, 32)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Access Level : "
        '
        'cbopermisionkey
        '
        Me.cbopermisionkey.FormattingEnabled = True
        Me.cbopermisionkey.Location = New System.Drawing.Point(98, 64)
        Me.cbopermisionkey.Name = "cbopermisionkey"
        Me.cbopermisionkey.Size = New System.Drawing.Size(327, 40)
        Me.cbopermisionkey.TabIndex = 1
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.CT_Allow)
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Controls.Add(Me.DT_PRINTCOMPLETED)
        Me.GroupBox1.Controls.Add(Me.DT_PRINTPENDING)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.SR_ChangePass)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.OA_Allow)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.ALS_DeleteRemove)
        Me.GroupBox1.Controls.Add(Me.ALS_EditUpdate)
        Me.GroupBox1.Controls.Add(Me.ALS_CreateAdd)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.UA_DeleteRemove)
        Me.GroupBox1.Controls.Add(Me.UA_EditUpdate)
        Me.GroupBox1.Controls.Add(Me.UA_CreateAdd)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.PC_Allow)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.DT_Export)
        Me.GroupBox1.Controls.Add(Me.DT_DeleteRemove)
        Me.GroupBox1.Controls.Add(Me.DT_EditUpdate)
        Me.GroupBox1.Controls.Add(Me.DT_CreateAdd)
        Me.GroupBox1.Location = New System.Drawing.Point(15, 101)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(513, 378)
        Me.GroupBox1.TabIndex = 2
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Actions Limitations"
        '
        'CT_Allow
        '
        Me.CT_Allow.Checked = True
        Me.CT_Allow.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CT_Allow.Location = New System.Drawing.Point(282, 291)
        Me.CT_Allow.Name = "CT_Allow"
        Me.CT_Allow.Size = New System.Drawing.Size(223, 22)
        Me.CT_Allow.TabIndex = 29
        Me.CT_Allow.Text = "Allow EDIT COMPLETED Transaction"
        Me.CT_Allow.UseVisualStyleBackColor = True
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(279, 271)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(277, 32)
        Me.Label9.TabIndex = 28
        Me.Label9.Text = "Completed Transaction"
        '
        'DT_PRINTCOMPLETED
        '
        Me.DT_PRINTCOMPLETED.AutoSize = True
        Me.DT_PRINTCOMPLETED.Checked = True
        Me.DT_PRINTCOMPLETED.CheckState = System.Windows.Forms.CheckState.Checked
        Me.DT_PRINTCOMPLETED.Location = New System.Drawing.Point(15, 180)
        Me.DT_PRINTCOMPLETED.Name = "DT_PRINTCOMPLETED"
        Me.DT_PRINTCOMPLETED.Size = New System.Drawing.Size(456, 36)
        Me.DT_PRINTCOMPLETED.TabIndex = 27
        Me.DT_PRINTCOMPLETED.Text = "Allow to PRINT Completed Transaction"
        Me.DT_PRINTCOMPLETED.UseVisualStyleBackColor = True
        '
        'DT_PRINTPENDING
        '
        Me.DT_PRINTPENDING.AutoSize = True
        Me.DT_PRINTPENDING.Checked = True
        Me.DT_PRINTPENDING.CheckState = System.Windows.Forms.CheckState.Checked
        Me.DT_PRINTPENDING.Location = New System.Drawing.Point(15, 155)
        Me.DT_PRINTPENDING.Name = "DT_PRINTPENDING"
        Me.DT_PRINTPENDING.Size = New System.Drawing.Size(435, 36)
        Me.DT_PRINTPENDING.TabIndex = 26
        Me.DT_PRINTPENDING.Text = "Allow to PRINT Pending Transactions"
        Me.DT_PRINTPENDING.UseVisualStyleBackColor = True
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.Red
        Me.Label8.Location = New System.Drawing.Point(10, 311)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(162, 32)
        Me.Label8.TabIndex = 25
        Me.Label8.Text = "System Reset"
        '
        'SR_ChangePass
        '
        Me.SR_ChangePass.AutoSize = True
        Me.SR_ChangePass.Location = New System.Drawing.Point(15, 335)
        Me.SR_ChangePass.Name = "SR_ChangePass"
        Me.SR_ChangePass.Size = New System.Drawing.Size(501, 36)
        Me.SR_ChangePass.TabIndex = 23
        Me.SR_ChangePass.Text = "Allow to change password for system reset"
        Me.SR_ChangePass.UseVisualStyleBackColor = True
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(274, 190)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(168, 32)
        Me.Label7.TabIndex = 21
        Me.Label7.Text = "Own Account"
        '
        'OA_Allow
        '
        Me.OA_Allow.Checked = True
        Me.OA_Allow.CheckState = System.Windows.Forms.CheckState.Checked
        Me.OA_Allow.Location = New System.Drawing.Point(282, 212)
        Me.OA_Allow.Name = "OA_Allow"
        Me.OA_Allow.Size = New System.Drawing.Size(223, 54)
        Me.OA_Allow.TabIndex = 20
        Me.OA_Allow.Text = "Allow to make changes to own account. Change on Access Level is disabled"
        Me.OA_Allow.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(274, 31)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(227, 32)
        Me.Label5.TabIndex = 19
        Me.Label5.Text = "Access Level Setup"
        '
        'ALS_DeleteRemove
        '
        Me.ALS_DeleteRemove.AutoSize = True
        Me.ALS_DeleteRemove.Location = New System.Drawing.Point(282, 105)
        Me.ALS_DeleteRemove.Name = "ALS_DeleteRemove"
        Me.ALS_DeleteRemove.Size = New System.Drawing.Size(441, 36)
        Me.ALS_DeleteRemove.TabIndex = 18
        Me.ALS_DeleteRemove.Text = "Allow to DELETE/REMOVE permission"
        Me.ALS_DeleteRemove.UseVisualStyleBackColor = True
        '
        'ALS_EditUpdate
        '
        Me.ALS_EditUpdate.AutoSize = True
        Me.ALS_EditUpdate.Location = New System.Drawing.Point(282, 80)
        Me.ALS_EditUpdate.Name = "ALS_EditUpdate"
        Me.ALS_EditUpdate.Size = New System.Drawing.Size(403, 36)
        Me.ALS_EditUpdate.TabIndex = 17
        Me.ALS_EditUpdate.Text = "Allow to EDIT/UPDATE permission"
        Me.ALS_EditUpdate.UseVisualStyleBackColor = True
        '
        'ALS_CreateAdd
        '
        Me.ALS_CreateAdd.AutoSize = True
        Me.ALS_CreateAdd.Location = New System.Drawing.Point(282, 55)
        Me.ALS_CreateAdd.Name = "ALS_CreateAdd"
        Me.ALS_CreateAdd.Size = New System.Drawing.Size(400, 36)
        Me.ALS_CreateAdd.TabIndex = 16
        Me.ALS_CreateAdd.Text = "Allow to CREATE/ADD permission"
        Me.ALS_CreateAdd.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(7, 207)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(178, 32)
        Me.Label4.TabIndex = 15
        Me.Label4.Text = "User Accounts"
        '
        'UA_DeleteRemove
        '
        Me.UA_DeleteRemove.AutoSize = True
        Me.UA_DeleteRemove.Location = New System.Drawing.Point(15, 281)
        Me.UA_DeleteRemove.Name = "UA_DeleteRemove"
        Me.UA_DeleteRemove.Size = New System.Drawing.Size(370, 36)
        Me.UA_DeleteRemove.TabIndex = 13
        Me.UA_DeleteRemove.Text = "Allow to DELETE/REMOVE user"
        Me.UA_DeleteRemove.UseVisualStyleBackColor = True
        '
        'UA_EditUpdate
        '
        Me.UA_EditUpdate.AutoSize = True
        Me.UA_EditUpdate.Location = New System.Drawing.Point(15, 256)
        Me.UA_EditUpdate.Name = "UA_EditUpdate"
        Me.UA_EditUpdate.Size = New System.Drawing.Size(332, 36)
        Me.UA_EditUpdate.TabIndex = 12
        Me.UA_EditUpdate.Text = "Allow to EDIT/UPDATE user"
        Me.UA_EditUpdate.UseVisualStyleBackColor = True
        '
        'UA_CreateAdd
        '
        Me.UA_CreateAdd.AutoSize = True
        Me.UA_CreateAdd.Location = New System.Drawing.Point(15, 231)
        Me.UA_CreateAdd.Name = "UA_CreateAdd"
        Me.UA_CreateAdd.Size = New System.Drawing.Size(329, 36)
        Me.UA_CreateAdd.TabIndex = 11
        Me.UA_CreateAdd.Text = "Allow to CREATE/ADD user"
        Me.UA_CreateAdd.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(274, 137)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(230, 32)
        Me.Label3.TabIndex = 10
        Me.Label3.Text = "Port Configuration"
        '
        'PC_Allow
        '
        Me.PC_Allow.AutoSize = True
        Me.PC_Allow.Checked = True
        Me.PC_Allow.CheckState = System.Windows.Forms.CheckState.Checked
        Me.PC_Allow.Location = New System.Drawing.Point(282, 161)
        Me.PC_Allow.Name = "PC_Allow"
        Me.PC_Allow.Size = New System.Drawing.Size(339, 36)
        Me.PC_Allow.TabIndex = 6
        Me.PC_Allow.Text = "Allow to full configurations."
        Me.PC_Allow.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(7, 31)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(291, 32)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "Dashboard-Transactions"
        '
        'DT_Export
        '
        Me.DT_Export.AutoSize = True
        Me.DT_Export.Checked = True
        Me.DT_Export.CheckState = System.Windows.Forms.CheckState.Checked
        Me.DT_Export.Location = New System.Drawing.Point(15, 130)
        Me.DT_Export.Name = "DT_Export"
        Me.DT_Export.Size = New System.Drawing.Size(310, 36)
        Me.DT_Export.TabIndex = 4
        Me.DT_Export.Text = "Allow to EXPORT reports."
        Me.DT_Export.UseVisualStyleBackColor = True
        '
        'DT_DeleteRemove
        '
        Me.DT_DeleteRemove.AutoSize = True
        Me.DT_DeleteRemove.Checked = True
        Me.DT_DeleteRemove.CheckState = System.Windows.Forms.CheckState.Checked
        Me.DT_DeleteRemove.Location = New System.Drawing.Point(15, 105)
        Me.DT_DeleteRemove.Name = "DT_DeleteRemove"
        Me.DT_DeleteRemove.Size = New System.Drawing.Size(452, 36)
        Me.DT_DeleteRemove.TabIndex = 3
        Me.DT_DeleteRemove.Text = "Allow to DELETE/REMOVE transactions"
        Me.DT_DeleteRemove.UseVisualStyleBackColor = True
        '
        'DT_EditUpdate
        '
        Me.DT_EditUpdate.AutoSize = True
        Me.DT_EditUpdate.Checked = True
        Me.DT_EditUpdate.CheckState = System.Windows.Forms.CheckState.Checked
        Me.DT_EditUpdate.Location = New System.Drawing.Point(15, 80)
        Me.DT_EditUpdate.Name = "DT_EditUpdate"
        Me.DT_EditUpdate.Size = New System.Drawing.Size(414, 36)
        Me.DT_EditUpdate.TabIndex = 2
        Me.DT_EditUpdate.Text = "Allow to EDIT/UPDATE transactions"
        Me.DT_EditUpdate.UseVisualStyleBackColor = True
        '
        'DT_CreateAdd
        '
        Me.DT_CreateAdd.AutoSize = True
        Me.DT_CreateAdd.Checked = True
        Me.DT_CreateAdd.CheckState = System.Windows.Forms.CheckState.Checked
        Me.DT_CreateAdd.Location = New System.Drawing.Point(15, 55)
        Me.DT_CreateAdd.Name = "DT_CreateAdd"
        Me.DT_CreateAdd.Size = New System.Drawing.Size(411, 36)
        Me.DT_CreateAdd.TabIndex = 0
        Me.DT_CreateAdd.Text = "Allow to CREATE/ADD transactions"
        Me.DT_CreateAdd.UseVisualStyleBackColor = True
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Panel3.Controls.Add(Me.btnCancel)
        Me.Panel3.Controls.Add(Me.btnSave)
        Me.Panel3.Controls.Add(Me.btnClearEntry)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel3.Font = New System.Drawing.Font("Segoe UI", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel3.Location = New System.Drawing.Point(0, 492)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(542, 45)
        Me.Panel3.TabIndex = 14
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.btnCancel.BackColor = System.Drawing.SystemColors.ControlLight
        Me.btnCancel.FlatAppearance.BorderSize = 0
        Me.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCancel.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCancel.Location = New System.Drawing.Point(430, 8)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(101, 29)
        Me.btnCancel.TabIndex = 1
        Me.btnCancel.Text = "&Close"
        Me.btnCancel.UseVisualStyleBackColor = False
        '
        'btnSave
        '
        Me.btnSave.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.btnSave.BackColor = System.Drawing.SystemColors.ControlLight
        Me.btnSave.FlatAppearance.BorderSize = 0
        Me.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSave.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.Location = New System.Drawing.Point(320, 8)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(101, 29)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = "&Save/Update"
        Me.btnSave.UseVisualStyleBackColor = False
        '
        'btnClearEntry
        '
        Me.btnClearEntry.BackColor = System.Drawing.SystemColors.ControlLight
        Me.btnClearEntry.FlatAppearance.BorderSize = 0
        Me.btnClearEntry.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClearEntry.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnClearEntry.Location = New System.Drawing.Point(11, 8)
        Me.btnClearEntry.Name = "btnClearEntry"
        Me.btnClearEntry.Size = New System.Drawing.Size(101, 29)
        Me.btnClearEntry.TabIndex = 12
        Me.btnClearEntry.Text = "Clear &Entry"
        Me.btnClearEntry.UseVisualStyleBackColor = False
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.DodgerBlue
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Font = New System.Drawing.Font("Segoe UI", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel2.Location = New System.Drawing.Point(0, 42)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(542, 3)
        Me.Panel2.TabIndex = 17
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Panel1.Controls.Add(Me.PictureBox1)
        Me.Panel1.Controls.Add(Me.Label6)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Font = New System.Drawing.Font("Segoe UI", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(542, 42)
        Me.Panel1.TabIndex = 16
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), System.Drawing.Image)
        Me.PictureBox1.Location = New System.Drawing.Point(6, 7)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(28, 28)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 15
        Me.PictureBox1.TabStop = False
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.DodgerBlue
        Me.Label6.Location = New System.Drawing.Point(40, 14)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(238, 32)
        Me.Label6.TabIndex = 0
        Me.Label6.Text = "ACCESS LEVEL SETUP"
        '
        'ImageList3
        '
        Me.ImageList3.ImageStream = CType(resources.GetObject("ImageList3.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList3.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList3.Images.SetKeyName(0, "close x.png")
        Me.ImageList3.Images.SetKeyName(1, "close x -2.png")
        '
        'btnremove
        '
        Me.btnremove.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.btnremove.BackColor = System.Drawing.SystemColors.ControlLight
        Me.btnremove.FlatAppearance.BorderSize = 0
        Me.btnremove.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnremove.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnremove.Location = New System.Drawing.Point(430, 61)
        Me.btnremove.Name = "btnremove"
        Me.btnremove.Size = New System.Drawing.Size(101, 29)
        Me.btnremove.TabIndex = 18
        Me.btnremove.Text = "&Remove"
        Me.btnremove.UseVisualStyleBackColor = False
        '
        'frmAccessLevelSettings
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(13.0!, 32.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(542, 537)
        Me.ControlBox = False
        Me.Controls.Add(Me.btnremove)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.cbopermisionkey)
        Me.Controls.Add(Me.Label1)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Margin = New System.Windows.Forms.Padding(2, 5, 2, 5)
        Me.Name = "frmAccessLevelSettings"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Access Level Setup"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents cbopermisionkey As ComboBox
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents DT_CreateAdd As CheckBox
    Friend WithEvents Label5 As Label
    Friend WithEvents ALS_DeleteRemove As CheckBox
    Friend WithEvents ALS_EditUpdate As CheckBox
    Friend WithEvents ALS_CreateAdd As CheckBox
    Friend WithEvents Label4 As Label
    Friend WithEvents UA_DeleteRemove As CheckBox
    Friend WithEvents UA_EditUpdate As CheckBox
    Friend WithEvents UA_CreateAdd As CheckBox
    Friend WithEvents Label3 As Label
    Friend WithEvents PC_Allow As CheckBox
    Friend WithEvents Label2 As Label
    Friend WithEvents DT_Export As CheckBox
    Friend WithEvents DT_DeleteRemove As CheckBox
    Friend WithEvents DT_EditUpdate As CheckBox
    Friend WithEvents Panel3 As Panel
    Friend WithEvents btnCancel As Button
    Friend WithEvents btnSave As Button
    Friend WithEvents btnClearEntry As Button
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Panel1 As Panel
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents Label6 As Label
    Friend WithEvents ImageList3 As ImageList
    Friend WithEvents Label7 As Label
    Friend WithEvents OA_Allow As CheckBox
    Friend WithEvents btnremove As Button
    Friend WithEvents Label8 As Label
    Friend WithEvents SR_ChangePass As CheckBox
    Friend WithEvents DT_PRINTCOMPLETED As CheckBox
    Friend WithEvents DT_PRINTPENDING As CheckBox
    Friend WithEvents CT_Allow As CheckBox
    Friend WithEvents Label9 As Label
End Class
