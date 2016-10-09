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
        Me.txtNames = New System.Windows.Forms.TextBox
        Me.btnAddBracket = New System.Windows.Forms.Button
        Me.txtDebug = New System.Windows.Forms.TextBox
        Me.txtDivisionName = New System.Windows.Forms.TextBox
        Me.lblDivision = New System.Windows.Forms.Label
        Me.lblPlayers = New System.Windows.Forms.Label
        Me.lstBrackets = New System.Windows.Forms.ListBox
        Me.btnDump = New System.Windows.Forms.Button
        Me.lstBracketType = New System.Windows.Forms.ListBox
        Me.lblType = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'txtNames
        '
        Me.txtNames.Location = New System.Drawing.Point(33, 69)
        Me.txtNames.Multiline = True
        Me.txtNames.Name = "txtNames"
        Me.txtNames.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtNames.Size = New System.Drawing.Size(442, 149)
        Me.txtNames.TabIndex = 2
        '
        'btnAddBracket
        '
        Me.btnAddBracket.Location = New System.Drawing.Point(394, 280)
        Me.btnAddBracket.Name = "btnAddBracket"
        Me.btnAddBracket.Size = New System.Drawing.Size(82, 23)
        Me.btnAddBracket.TabIndex = 4
        Me.btnAddBracket.Text = "Process"
        Me.btnAddBracket.UseVisualStyleBackColor = True
        '
        'txtDebug
        '
        Me.txtDebug.Location = New System.Drawing.Point(33, 604)
        Me.txtDebug.Multiline = True
        Me.txtDebug.Name = "txtDebug"
        Me.txtDebug.ReadOnly = True
        Me.txtDebug.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtDebug.Size = New System.Drawing.Size(442, 34)
        Me.txtDebug.TabIndex = 98
        '
        'txtDivisionName
        '
        Me.txtDivisionName.Location = New System.Drawing.Point(33, 24)
        Me.txtDivisionName.Name = "txtDivisionName"
        Me.txtDivisionName.Size = New System.Drawing.Size(442, 20)
        Me.txtDivisionName.TabIndex = 1
        '
        'lblDivision
        '
        Me.lblDivision.AutoSize = True
        Me.lblDivision.Location = New System.Drawing.Point(30, 8)
        Me.lblDivision.Name = "lblDivision"
        Me.lblDivision.Size = New System.Drawing.Size(44, 13)
        Me.lblDivision.TabIndex = 6
        Me.lblDivision.Text = "Division"
        '
        'lblPlayers
        '
        Me.lblPlayers.AutoSize = True
        Me.lblPlayers.Location = New System.Drawing.Point(30, 53)
        Me.lblPlayers.Name = "lblPlayers"
        Me.lblPlayers.Size = New System.Drawing.Size(41, 13)
        Me.lblPlayers.TabIndex = 7
        Me.lblPlayers.Text = "Players"
        '
        'lstBrackets
        '
        Me.lstBrackets.FormattingEnabled = True
        Me.lstBrackets.Location = New System.Drawing.Point(34, 399)
        Me.lstBrackets.Name = "lstBrackets"
        Me.lstBrackets.Size = New System.Drawing.Size(442, 199)
        Me.lstBrackets.TabIndex = 99
        '
        'btnDump
        '
        Me.btnDump.Location = New System.Drawing.Point(313, 280)
        Me.btnDump.Name = "btnDump"
        Me.btnDump.Size = New System.Drawing.Size(75, 23)
        Me.btnDump.TabIndex = 5
        Me.btnDump.Text = "Dump"
        Me.btnDump.UseVisualStyleBackColor = True
        '
        'lstBracketType
        '
        Me.lstBracketType.FormattingEnabled = True
        Me.lstBracketType.Location = New System.Drawing.Point(33, 256)
        Me.lstBracketType.Name = "lstBracketType"
        Me.lstBracketType.Size = New System.Drawing.Size(217, 95)
        Me.lstBracketType.TabIndex = 3
        '
        'lblType
        '
        Me.lblType.AutoSize = True
        Me.lblType.Location = New System.Drawing.Point(33, 237)
        Me.lblType.Name = "lblType"
        Me.lblType.Size = New System.Drawing.Size(71, 13)
        Me.lblType.TabIndex = 11
        Me.lblType.Text = "Bracket Type"
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(518, 650)
        Me.Controls.Add(Me.lblType)
        Me.Controls.Add(Me.lstBracketType)
        Me.Controls.Add(Me.btnDump)
        Me.Controls.Add(Me.lstBrackets)
        Me.Controls.Add(Me.lblPlayers)
        Me.Controls.Add(Me.lblDivision)
        Me.Controls.Add(Me.txtDivisionName)
        Me.Controls.Add(Me.txtDebug)
        Me.Controls.Add(Me.btnAddBracket)
        Me.Controls.Add(Me.txtNames)
        Me.Name = "frmMain"
        Me.Text = "Bracket Generator"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtNames As System.Windows.Forms.TextBox
    Friend WithEvents btnAddBracket As System.Windows.Forms.Button
    Friend WithEvents txtDebug As System.Windows.Forms.TextBox
    Friend WithEvents txtDivisionName As System.Windows.Forms.TextBox
    Friend WithEvents lblDivision As System.Windows.Forms.Label
    Friend WithEvents lblPlayers As System.Windows.Forms.Label
    Friend WithEvents lstBrackets As System.Windows.Forms.ListBox
    Friend WithEvents btnDump As System.Windows.Forms.Button
    Friend WithEvents lstBracketType As System.Windows.Forms.ListBox
    Friend WithEvents lblType As System.Windows.Forms.Label

End Class
