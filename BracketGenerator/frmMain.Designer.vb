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
        Me.SuspendLayout()
        '
        'txtNames
        '
        Me.txtNames.Location = New System.Drawing.Point(33, 69)
        Me.txtNames.Multiline = True
        Me.txtNames.Name = "txtNames"
        Me.txtNames.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtNames.Size = New System.Drawing.Size(442, 149)
        Me.txtNames.TabIndex = 1
        '
        'btnAddBracket
        '
        Me.btnAddBracket.Location = New System.Drawing.Point(393, 224)
        Me.btnAddBracket.Name = "btnAddBracket"
        Me.btnAddBracket.Size = New System.Drawing.Size(82, 23)
        Me.btnAddBracket.TabIndex = 2
        Me.btnAddBracket.Text = "Process"
        Me.btnAddBracket.UseVisualStyleBackColor = True
        '
        'txtDebug
        '
        Me.txtDebug.Location = New System.Drawing.Point(33, 486)
        Me.txtDebug.Multiline = True
        Me.txtDebug.Name = "txtDebug"
        Me.txtDebug.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtDebug.Size = New System.Drawing.Size(442, 254)
        Me.txtDebug.TabIndex = 3
        '
        'txtDivisionName
        '
        Me.txtDivisionName.Location = New System.Drawing.Point(33, 24)
        Me.txtDivisionName.Name = "txtDivisionName"
        Me.txtDivisionName.Size = New System.Drawing.Size(442, 20)
        Me.txtDivisionName.TabIndex = 5
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
        Me.lstBrackets.Location = New System.Drawing.Point(33, 260)
        Me.lstBrackets.Name = "lstBrackets"
        Me.lstBrackets.Size = New System.Drawing.Size(442, 199)
        Me.lstBrackets.TabIndex = 8
        '
        'btnDump
        '
        Me.btnDump.Location = New System.Drawing.Point(67, 224)
        Me.btnDump.Name = "btnDump"
        Me.btnDump.Size = New System.Drawing.Size(75, 23)
        Me.btnDump.TabIndex = 9
        Me.btnDump.Text = "Dump"
        Me.btnDump.UseVisualStyleBackColor = True
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(518, 752)
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

End Class
