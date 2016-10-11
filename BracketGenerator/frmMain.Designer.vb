<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMain
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.btnDump = New System.Windows.Forms.Button
        Me.pagDelete = New System.Windows.Forms.TabPage
        Me.grpDelMain = New System.Windows.Forms.GroupBox
        Me.grpDelPlayers = New System.Windows.Forms.GroupBox
        Me.lstNames = New System.Windows.Forms.ListBox
        Me.lblNames = New System.Windows.Forms.Label
        Me.grpBrackets = New System.Windows.Forms.GroupBox
        Me.lstBrackets = New System.Windows.Forms.ListBox
        Me.lblBrackets = New System.Windows.Forms.Label
        Me.btnDeleteMat1 = New System.Windows.Forms.Button
        Me.pagAdd = New System.Windows.Forms.TabPage
        Me.grpAddMain = New System.Windows.Forms.GroupBox
        Me.grpNames = New System.Windows.Forms.GroupBox
        Me.txtNames = New System.Windows.Forms.TextBox
        Me.lblPlayers = New System.Windows.Forms.Label
        Me.grpType = New System.Windows.Forms.GroupBox
        Me.lstBracketType = New System.Windows.Forms.ListBox
        Me.lblType = New System.Windows.Forms.Label
        Me.grpDivi = New System.Windows.Forms.GroupBox
        Me.txtDivisionName = New System.Windows.Forms.TextBox
        Me.lblDivision = New System.Windows.Forms.Label
        Me.btnAddBracket = New System.Windows.Forms.Button
        Me.tabMain = New System.Windows.Forms.TabControl
        Me.pagMats = New System.Windows.Forms.TabPage
        Me.grpMatsMain = New System.Windows.Forms.GroupBox
        Me.btnDown = New System.Windows.Forms.Button
        Me.btnUp = New System.Windows.Forms.Button
        Me.grpmat1 = New System.Windows.Forms.GroupBox
        Me.lstMat1 = New System.Windows.Forms.ListBox
        Me.lblMat1 = New System.Windows.Forms.Label
        Me.grpmat2 = New System.Windows.Forms.GroupBox
        Me.btnDeleteMat2 = New System.Windows.Forms.Button
        Me.lblMat2 = New System.Windows.Forms.Label
        Me.lstMat2 = New System.Windows.Forms.ListBox
        Me.btnModify = New System.Windows.Forms.Button
        Me.lblMat1Count = New System.Windows.Forms.Label
        Me.lblMat2Count = New System.Windows.Forms.Label
        Me.pagDelete.SuspendLayout()
        Me.grpDelMain.SuspendLayout()
        Me.grpDelPlayers.SuspendLayout()
        Me.grpBrackets.SuspendLayout()
        Me.pagAdd.SuspendLayout()
        Me.grpAddMain.SuspendLayout()
        Me.grpNames.SuspendLayout()
        Me.grpType.SuspendLayout()
        Me.grpDivi.SuspendLayout()
        Me.tabMain.SuspendLayout()
        Me.pagMats.SuspendLayout()
        Me.grpMatsMain.SuspendLayout()
        Me.grpmat1.SuspendLayout()
        Me.grpmat2.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnDump
        '
        Me.btnDump.Location = New System.Drawing.Point(515, 385)
        Me.btnDump.Name = "btnDump"
        Me.btnDump.Size = New System.Drawing.Size(75, 23)
        Me.btnDump.TabIndex = 99
        Me.btnDump.TabStop = False
        Me.btnDump.Text = "&Output"
        Me.btnDump.UseVisualStyleBackColor = True
        '
        'pagDelete
        '
        Me.pagDelete.Controls.Add(Me.grpDelMain)
        Me.pagDelete.Location = New System.Drawing.Point(4, 22)
        Me.pagDelete.Name = "pagDelete"
        Me.pagDelete.Padding = New System.Windows.Forms.Padding(3)
        Me.pagDelete.Size = New System.Drawing.Size(586, 352)
        Me.pagDelete.TabIndex = 1
        Me.pagDelete.Text = "   View   "
        Me.pagDelete.UseVisualStyleBackColor = True
        '
        'grpDelMain
        '
        Me.grpDelMain.Controls.Add(Me.grpDelPlayers)
        Me.grpDelMain.Controls.Add(Me.grpBrackets)
        Me.grpDelMain.Location = New System.Drawing.Point(8, 3)
        Me.grpDelMain.Name = "grpDelMain"
        Me.grpDelMain.Size = New System.Drawing.Size(572, 343)
        Me.grpDelMain.TabIndex = 104
        Me.grpDelMain.TabStop = False
        '
        'grpDelPlayers
        '
        Me.grpDelPlayers.Controls.Add(Me.lstNames)
        Me.grpDelPlayers.Controls.Add(Me.lblNames)
        Me.grpDelPlayers.Location = New System.Drawing.Point(82, 167)
        Me.grpDelPlayers.Name = "grpDelPlayers"
        Me.grpDelPlayers.Size = New System.Drawing.Size(401, 173)
        Me.grpDelPlayers.TabIndex = 105
        Me.grpDelPlayers.TabStop = False
        '
        'lstNames
        '
        Me.lstNames.FormattingEnabled = True
        Me.lstNames.Location = New System.Drawing.Point(6, 31)
        Me.lstNames.Name = "lstNames"
        Me.lstNames.SelectionMode = System.Windows.Forms.SelectionMode.None
        Me.lstNames.Size = New System.Drawing.Size(389, 134)
        Me.lstNames.TabIndex = 102
        '
        'lblNames
        '
        Me.lblNames.AutoSize = True
        Me.lblNames.Location = New System.Drawing.Point(6, 15)
        Me.lblNames.Name = "lblNames"
        Me.lblNames.Size = New System.Drawing.Size(41, 13)
        Me.lblNames.TabIndex = 103
        Me.lblNames.Text = "Players"
        '
        'grpBrackets
        '
        Me.grpBrackets.Controls.Add(Me.lstBrackets)
        Me.grpBrackets.Controls.Add(Me.lblBrackets)
        Me.grpBrackets.Location = New System.Drawing.Point(81, 5)
        Me.grpBrackets.Name = "grpBrackets"
        Me.grpBrackets.Size = New System.Drawing.Size(402, 158)
        Me.grpBrackets.TabIndex = 104
        Me.grpBrackets.TabStop = False
        '
        'lstBrackets
        '
        Me.lstBrackets.FormattingEnabled = True
        Me.lstBrackets.Location = New System.Drawing.Point(6, 28)
        Me.lstBrackets.Name = "lstBrackets"
        Me.lstBrackets.Size = New System.Drawing.Size(390, 108)
        Me.lstBrackets.TabIndex = 99
        '
        'lblBrackets
        '
        Me.lblBrackets.AutoSize = True
        Me.lblBrackets.Location = New System.Drawing.Point(3, 12)
        Me.lblBrackets.Name = "lblBrackets"
        Me.lblBrackets.Size = New System.Drawing.Size(49, 13)
        Me.lblBrackets.TabIndex = 101
        Me.lblBrackets.Text = "Brackets"
        '
        'btnDeleteMat1
        '
        Me.btnDeleteMat1.Location = New System.Drawing.Point(390, 68)
        Me.btnDeleteMat1.Name = "btnDeleteMat1"
        Me.btnDeleteMat1.Size = New System.Drawing.Size(75, 23)
        Me.btnDeleteMat1.TabIndex = 100
        Me.btnDeleteMat1.Text = "Delete"
        Me.btnDeleteMat1.UseVisualStyleBackColor = True
        '
        'pagAdd
        '
        Me.pagAdd.Controls.Add(Me.grpAddMain)
        Me.pagAdd.Location = New System.Drawing.Point(4, 22)
        Me.pagAdd.Name = "pagAdd"
        Me.pagAdd.Padding = New System.Windows.Forms.Padding(3)
        Me.pagAdd.Size = New System.Drawing.Size(586, 352)
        Me.pagAdd.TabIndex = 0
        Me.pagAdd.Text = "   Add   "
        Me.pagAdd.UseVisualStyleBackColor = True
        '
        'grpAddMain
        '
        Me.grpAddMain.Controls.Add(Me.grpNames)
        Me.grpAddMain.Controls.Add(Me.grpType)
        Me.grpAddMain.Controls.Add(Me.grpDivi)
        Me.grpAddMain.Controls.Add(Me.btnAddBracket)
        Me.grpAddMain.Location = New System.Drawing.Point(8, 3)
        Me.grpAddMain.Name = "grpAddMain"
        Me.grpAddMain.Size = New System.Drawing.Size(572, 343)
        Me.grpAddMain.TabIndex = 1
        Me.grpAddMain.TabStop = False
        '
        'grpNames
        '
        Me.grpNames.Controls.Add(Me.txtNames)
        Me.grpNames.Controls.Add(Me.lblPlayers)
        Me.grpNames.Location = New System.Drawing.Point(294, 19)
        Me.grpNames.Name = "grpNames"
        Me.grpNames.Size = New System.Drawing.Size(261, 312)
        Me.grpNames.TabIndex = 4
        Me.grpNames.TabStop = False
        '
        'txtNames
        '
        Me.txtNames.Location = New System.Drawing.Point(9, 28)
        Me.txtNames.Multiline = True
        Me.txtNames.Name = "txtNames"
        Me.txtNames.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtNames.Size = New System.Drawing.Size(246, 274)
        Me.txtNames.TabIndex = 5
        '
        'lblPlayers
        '
        Me.lblPlayers.AutoSize = True
        Me.lblPlayers.Location = New System.Drawing.Point(6, 12)
        Me.lblPlayers.Name = "lblPlayers"
        Me.lblPlayers.Size = New System.Drawing.Size(41, 13)
        Me.lblPlayers.TabIndex = 4
        Me.lblPlayers.Text = "&Players"
        '
        'grpType
        '
        Me.grpType.Controls.Add(Me.lstBracketType)
        Me.grpType.Controls.Add(Me.lblType)
        Me.grpType.Location = New System.Drawing.Point(21, 192)
        Me.grpType.Name = "grpType"
        Me.grpType.Size = New System.Drawing.Size(257, 139)
        Me.grpType.TabIndex = 6
        Me.grpType.TabStop = False
        '
        'lstBracketType
        '
        Me.lstBracketType.FormattingEnabled = True
        Me.lstBracketType.Location = New System.Drawing.Point(9, 32)
        Me.lstBracketType.Name = "lstBracketType"
        Me.lstBracketType.Size = New System.Drawing.Size(162, 95)
        Me.lstBracketType.TabIndex = 7
        '
        'lblType
        '
        Me.lblType.AutoSize = True
        Me.lblType.Location = New System.Drawing.Point(6, 16)
        Me.lblType.Name = "lblType"
        Me.lblType.Size = New System.Drawing.Size(71, 13)
        Me.lblType.TabIndex = 6
        Me.lblType.Text = "Bracket &Type"
        '
        'grpDivi
        '
        Me.grpDivi.Controls.Add(Me.txtDivisionName)
        Me.grpDivi.Controls.Add(Me.lblDivision)
        Me.grpDivi.Location = New System.Drawing.Point(21, 19)
        Me.grpDivi.Name = "grpDivi"
        Me.grpDivi.Size = New System.Drawing.Size(257, 58)
        Me.grpDivi.TabIndex = 2
        Me.grpDivi.TabStop = False
        '
        'txtDivisionName
        '
        Me.txtDivisionName.Location = New System.Drawing.Point(9, 28)
        Me.txtDivisionName.Name = "txtDivisionName"
        Me.txtDivisionName.Size = New System.Drawing.Size(234, 20)
        Me.txtDivisionName.TabIndex = 3
        '
        'lblDivision
        '
        Me.lblDivision.AutoSize = True
        Me.lblDivision.Location = New System.Drawing.Point(6, 12)
        Me.lblDivision.Name = "lblDivision"
        Me.lblDivision.Size = New System.Drawing.Size(44, 13)
        Me.lblDivision.TabIndex = 2
        Me.lblDivision.Text = "&Division"
        '
        'btnAddBracket
        '
        Me.btnAddBracket.Location = New System.Drawing.Point(196, 83)
        Me.btnAddBracket.Name = "btnAddBracket"
        Me.btnAddBracket.Size = New System.Drawing.Size(82, 23)
        Me.btnAddBracket.TabIndex = 4
        Me.btnAddBracket.Text = "&Add"
        Me.btnAddBracket.UseVisualStyleBackColor = True
        '
        'tabMain
        '
        Me.tabMain.Controls.Add(Me.pagAdd)
        Me.tabMain.Controls.Add(Me.pagDelete)
        Me.tabMain.Controls.Add(Me.pagMats)
        Me.tabMain.Location = New System.Drawing.Point(0, 1)
        Me.tabMain.Name = "tabMain"
        Me.tabMain.SelectedIndex = 0
        Me.tabMain.Size = New System.Drawing.Size(594, 378)
        Me.tabMain.TabIndex = 0
        Me.tabMain.TabStop = False
        '
        'pagMats
        '
        Me.pagMats.Controls.Add(Me.grpMatsMain)
        Me.pagMats.Location = New System.Drawing.Point(4, 22)
        Me.pagMats.Name = "pagMats"
        Me.pagMats.Padding = New System.Windows.Forms.Padding(3)
        Me.pagMats.Size = New System.Drawing.Size(586, 352)
        Me.pagMats.TabIndex = 2
        Me.pagMats.Text = "   Mats   "
        Me.pagMats.UseVisualStyleBackColor = True
        '
        'grpMatsMain
        '
        Me.grpMatsMain.Controls.Add(Me.btnDown)
        Me.grpMatsMain.Controls.Add(Me.btnUp)
        Me.grpMatsMain.Controls.Add(Me.grpmat1)
        Me.grpMatsMain.Controls.Add(Me.grpmat2)
        Me.grpMatsMain.Location = New System.Drawing.Point(7, 2)
        Me.grpMatsMain.Name = "grpMatsMain"
        Me.grpMatsMain.Size = New System.Drawing.Size(572, 343)
        Me.grpMatsMain.TabIndex = 9
        Me.grpMatsMain.TabStop = False
        '
        'btnDown
        '
        Me.btnDown.Location = New System.Drawing.Point(277, 160)
        Me.btnDown.Name = "btnDown"
        Me.btnDown.Size = New System.Drawing.Size(75, 23)
        Me.btnDown.TabIndex = 7
        Me.btnDown.Text = "Down"
        Me.btnDown.UseVisualStyleBackColor = True
        '
        'btnUp
        '
        Me.btnUp.Location = New System.Drawing.Point(196, 160)
        Me.btnUp.Name = "btnUp"
        Me.btnUp.Size = New System.Drawing.Size(75, 23)
        Me.btnUp.TabIndex = 6
        Me.btnUp.Text = "Up"
        Me.btnUp.UseVisualStyleBackColor = True
        '
        'grpmat1
        '
        Me.grpmat1.Controls.Add(Me.lblMat1Count)
        Me.grpmat1.Controls.Add(Me.lstMat1)
        Me.grpmat1.Controls.Add(Me.lblMat1)
        Me.grpmat1.Controls.Add(Me.btnDeleteMat1)
        Me.grpmat1.Location = New System.Drawing.Point(47, 3)
        Me.grpmat1.Name = "grpmat1"
        Me.grpmat1.Size = New System.Drawing.Size(470, 155)
        Me.grpmat1.TabIndex = 4
        Me.grpmat1.TabStop = False
        '
        'lstMat1
        '
        Me.lstMat1.FormattingEnabled = True
        Me.lstMat1.Location = New System.Drawing.Point(6, 28)
        Me.lstMat1.Name = "lstMat1"
        Me.lstMat1.Size = New System.Drawing.Size(378, 121)
        Me.lstMat1.TabIndex = 0
        '
        'lblMat1
        '
        Me.lblMat1.AutoSize = True
        Me.lblMat1.Location = New System.Drawing.Point(6, 10)
        Me.lblMat1.Name = "lblMat1"
        Me.lblMat1.Size = New System.Drawing.Size(34, 13)
        Me.lblMat1.TabIndex = 2
        Me.lblMat1.Text = "Mat 1"
        '
        'grpmat2
        '
        Me.grpmat2.Controls.Add(Me.lblMat2Count)
        Me.grpmat2.Controls.Add(Me.btnDeleteMat2)
        Me.grpmat2.Controls.Add(Me.lblMat2)
        Me.grpmat2.Controls.Add(Me.lstMat2)
        Me.grpmat2.Location = New System.Drawing.Point(47, 179)
        Me.grpmat2.Name = "grpmat2"
        Me.grpmat2.Size = New System.Drawing.Size(470, 160)
        Me.grpmat2.TabIndex = 5
        Me.grpmat2.TabStop = False
        '
        'btnDeleteMat2
        '
        Me.btnDeleteMat2.Location = New System.Drawing.Point(390, 84)
        Me.btnDeleteMat2.Name = "btnDeleteMat2"
        Me.btnDeleteMat2.Size = New System.Drawing.Size(75, 23)
        Me.btnDeleteMat2.TabIndex = 4
        Me.btnDeleteMat2.Text = "Delete"
        Me.btnDeleteMat2.UseVisualStyleBackColor = True
        '
        'lblMat2
        '
        Me.lblMat2.AutoSize = True
        Me.lblMat2.Location = New System.Drawing.Point(6, 16)
        Me.lblMat2.Name = "lblMat2"
        Me.lblMat2.Size = New System.Drawing.Size(34, 13)
        Me.lblMat2.TabIndex = 3
        Me.lblMat2.Text = "Mat 2"
        '
        'lstMat2
        '
        Me.lstMat2.FormattingEnabled = True
        Me.lstMat2.Location = New System.Drawing.Point(6, 33)
        Me.lstMat2.Name = "lstMat2"
        Me.lstMat2.Size = New System.Drawing.Size(378, 121)
        Me.lstMat2.TabIndex = 1
        '
        'btnModify
        '
        Me.btnModify.Enabled = False
        Me.btnModify.Location = New System.Drawing.Point(434, 385)
        Me.btnModify.Name = "btnModify"
        Me.btnModify.Size = New System.Drawing.Size(75, 23)
        Me.btnModify.TabIndex = 100
        Me.btnModify.Text = "Modify"
        Me.btnModify.UseVisualStyleBackColor = True
        '
        'lblMat1Count
        '
        Me.lblMat1Count.AutoSize = True
        Me.lblMat1Count.Location = New System.Drawing.Point(403, 136)
        Me.lblMat1Count.Name = "lblMat1Count"
        Me.lblMat1Count.Size = New System.Drawing.Size(13, 13)
        Me.lblMat1Count.TabIndex = 101
        Me.lblMat1Count.Text = "0"
        '
        'lblMat2Count
        '
        Me.lblMat2Count.AutoSize = True
        Me.lblMat2Count.Location = New System.Drawing.Point(406, 146)
        Me.lblMat2Count.Name = "lblMat2Count"
        Me.lblMat2Count.Size = New System.Drawing.Size(13, 13)
        Me.lblMat2Count.TabIndex = 5
        Me.lblMat2Count.Text = "0"
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(593, 415)
        Me.Controls.Add(Me.btnModify)
        Me.Controls.Add(Me.btnDump)
        Me.Controls.Add(Me.tabMain)
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(609, 453)
        Me.MinimumSize = New System.Drawing.Size(609, 453)
        Me.Name = "frmMain"
        Me.ShowIcon = False
        Me.Text = "Bracket Generator"
        Me.pagDelete.ResumeLayout(False)
        Me.grpDelMain.ResumeLayout(False)
        Me.grpDelPlayers.ResumeLayout(False)
        Me.grpDelPlayers.PerformLayout()
        Me.grpBrackets.ResumeLayout(False)
        Me.grpBrackets.PerformLayout()
        Me.pagAdd.ResumeLayout(False)
        Me.grpAddMain.ResumeLayout(False)
        Me.grpNames.ResumeLayout(False)
        Me.grpNames.PerformLayout()
        Me.grpType.ResumeLayout(False)
        Me.grpType.PerformLayout()
        Me.grpDivi.ResumeLayout(False)
        Me.grpDivi.PerformLayout()
        Me.tabMain.ResumeLayout(False)
        Me.pagMats.ResumeLayout(False)
        Me.grpMatsMain.ResumeLayout(False)
        Me.grpmat1.ResumeLayout(False)
        Me.grpmat1.PerformLayout()
        Me.grpmat2.ResumeLayout(False)
        Me.grpmat2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnDump As System.Windows.Forms.Button
    Friend WithEvents pagDelete As System.Windows.Forms.TabPage
    Friend WithEvents lstBrackets As System.Windows.Forms.ListBox
    Friend WithEvents lblNames As System.Windows.Forms.Label
    Friend WithEvents btnDeleteMat1 As System.Windows.Forms.Button
    Friend WithEvents lstNames As System.Windows.Forms.ListBox
    Friend WithEvents lblBrackets As System.Windows.Forms.Label
    Friend WithEvents pagAdd As System.Windows.Forms.TabPage
    Friend WithEvents txtDivisionName As System.Windows.Forms.TextBox
    Friend WithEvents txtNames As System.Windows.Forms.TextBox
    Friend WithEvents lblDivision As System.Windows.Forms.Label
    Friend WithEvents lblPlayers As System.Windows.Forms.Label
    Friend WithEvents lstBracketType As System.Windows.Forms.ListBox
    Friend WithEvents lblType As System.Windows.Forms.Label
    Friend WithEvents btnAddBracket As System.Windows.Forms.Button
    Friend WithEvents tabMain As System.Windows.Forms.TabControl
    Friend WithEvents grpAddMain As System.Windows.Forms.GroupBox
    Friend WithEvents grpType As System.Windows.Forms.GroupBox
    Friend WithEvents grpDivi As System.Windows.Forms.GroupBox
    Friend WithEvents grpNames As System.Windows.Forms.GroupBox
    Friend WithEvents grpDelMain As System.Windows.Forms.GroupBox
    Friend WithEvents grpBrackets As System.Windows.Forms.GroupBox
    Friend WithEvents grpDelPlayers As System.Windows.Forms.GroupBox
    Friend WithEvents btnModify As System.Windows.Forms.Button
    Friend WithEvents pagMats As System.Windows.Forms.TabPage
    Friend WithEvents lstMat2 As System.Windows.Forms.ListBox
    Friend WithEvents lstMat1 As System.Windows.Forms.ListBox
    Friend WithEvents lblMat1 As System.Windows.Forms.Label
    Friend WithEvents grpmat2 As System.Windows.Forms.GroupBox
    Friend WithEvents lblMat2 As System.Windows.Forms.Label
    Friend WithEvents grpmat1 As System.Windows.Forms.GroupBox
    Friend WithEvents grpMatsMain As System.Windows.Forms.GroupBox
    Friend WithEvents btnDown As System.Windows.Forms.Button
    Friend WithEvents btnUp As System.Windows.Forms.Button
    Friend WithEvents btnDeleteMat2 As System.Windows.Forms.Button
    Friend WithEvents lblMat1Count As System.Windows.Forms.Label
    Friend WithEvents lblMat2Count As System.Windows.Forms.Label

End Class
