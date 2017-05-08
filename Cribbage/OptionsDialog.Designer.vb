<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class OptionsDialog
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(OptionsDialog))
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.OK_Button = New System.Windows.Forms.Button()
        Me.Cancel_Button = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cboGameSpeed = New System.Windows.Forms.ComboBox()
        Me.chkSound = New System.Windows.Forms.CheckBox()
        Me.cboCardBacks = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.picCardBack = New System.Windows.Forms.PictureBox()
        Me.chkPlayByPlayLogging = New System.Windows.Forms.CheckBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.numPlayTo = New System.Windows.Forms.NumericUpDown()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cboDifficulty = New System.Windows.Forms.ComboBox()
        Me.chkShowCPUCards = New System.Windows.Forms.CheckBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.ColorDialog1 = New System.Windows.Forms.ColorDialog()
        Me.TableLayoutPanel1.SuspendLayout()
        CType(Me.picCardBack, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.numPlayTo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.OK_Button, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Cancel_Button, 1, 0)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(78, 253)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(146, 29)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'OK_Button
        '
        Me.OK_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.OK_Button.Location = New System.Drawing.Point(3, 3)
        Me.OK_Button.Name = "OK_Button"
        Me.OK_Button.Size = New System.Drawing.Size(67, 23)
        Me.OK_Button.TabIndex = 0
        Me.OK_Button.Text = "OK"
        '
        'Cancel_Button
        '
        Me.Cancel_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Cancel_Button.Location = New System.Drawing.Point(76, 3)
        Me.Cancel_Button.Name = "Cancel_Button"
        Me.Cancel_Button.Size = New System.Drawing.Size(67, 23)
        Me.Cancel_Button.TabIndex = 1
        Me.Cancel_Button.Text = "Cancel"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 15)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(72, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Game Speed:"
        '
        'cboGameSpeed
        '
        Me.cboGameSpeed.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboGameSpeed.FormattingEnabled = True
        Me.cboGameSpeed.Items.AddRange(New Object() {"Slow", "Medium", "Fast", "Instant"})
        Me.cboGameSpeed.Location = New System.Drawing.Point(121, 12)
        Me.cboGameSpeed.Name = "cboGameSpeed"
        Me.cboGameSpeed.Size = New System.Drawing.Size(94, 21)
        Me.cboGameSpeed.TabIndex = 2
        '
        'chkSound
        '
        Me.chkSound.AutoSize = True
        Me.chkSound.Location = New System.Drawing.Point(15, 97)
        Me.chkSound.Name = "chkSound"
        Me.chkSound.Size = New System.Drawing.Size(57, 17)
        Me.chkSound.TabIndex = 3
        Me.chkSound.Text = "Sound"
        Me.chkSound.UseVisualStyleBackColor = True
        '
        'cboCardBacks
        '
        Me.cboCardBacks.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCardBacks.FormattingEnabled = True
        Me.cboCardBacks.Location = New System.Drawing.Point(121, 66)
        Me.cboCardBacks.Name = "cboCardBacks"
        Me.cboCardBacks.Size = New System.Drawing.Size(94, 21)
        Me.cboCardBacks.TabIndex = 4
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 69)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(65, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Card Backs:"
        '
        'picCardBack
        '
        Me.picCardBack.Location = New System.Drawing.Point(144, 93)
        Me.picCardBack.Name = "picCardBack"
        Me.picCardBack.Size = New System.Drawing.Size(71, 96)
        Me.picCardBack.TabIndex = 5
        Me.picCardBack.TabStop = False
        '
        'chkPlayByPlayLogging
        '
        Me.chkPlayByPlayLogging.AutoSize = True
        Me.chkPlayByPlayLogging.Location = New System.Drawing.Point(15, 120)
        Me.chkPlayByPlayLogging.Name = "chkPlayByPlayLogging"
        Me.chkPlayByPlayLogging.Size = New System.Drawing.Size(125, 17)
        Me.chkPlayByPlayLogging.TabIndex = 6
        Me.chkPlayByPlayLogging.Text = "Play-By-Play Logging"
        Me.chkPlayByPlayLogging.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 197)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(46, 13)
        Me.Label3.TabIndex = 7
        Me.Label3.Text = "Play To:"
        '
        'numPlayTo
        '
        Me.numPlayTo.BackColor = System.Drawing.Color.White
        Me.numPlayTo.Increment = New Decimal(New Integer() {30, 0, 0, 0})
        Me.numPlayTo.Location = New System.Drawing.Point(121, 195)
        Me.numPlayTo.Maximum = New Decimal(New Integer() {301, 0, 0, 0})
        Me.numPlayTo.Minimum = New Decimal(New Integer() {31, 0, 0, 0})
        Me.numPlayTo.Name = "numPlayTo"
        Me.numPlayTo.ReadOnly = True
        Me.numPlayTo.Size = New System.Drawing.Size(94, 20)
        Me.numPlayTo.TabIndex = 8
        Me.numPlayTo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.numPlayTo.Value = New Decimal(New Integer() {121, 0, 0, 0})
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(12, 42)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(50, 13)
        Me.Label4.TabIndex = 9
        Me.Label4.Text = "Difficulty:"
        '
        'cboDifficulty
        '
        Me.cboDifficulty.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboDifficulty.FormattingEnabled = True
        Me.cboDifficulty.Location = New System.Drawing.Point(121, 39)
        Me.cboDifficulty.Name = "cboDifficulty"
        Me.cboDifficulty.Size = New System.Drawing.Size(94, 21)
        Me.cboDifficulty.TabIndex = 10
        '
        'chkShowCPUCards
        '
        Me.chkShowCPUCards.AutoSize = True
        Me.chkShowCPUCards.Location = New System.Drawing.Point(15, 143)
        Me.chkShowCPUCards.Name = "chkShowCPUCards"
        Me.chkShowCPUCards.Size = New System.Drawing.Size(111, 17)
        Me.chkShowCPUCards.TabIndex = 11
        Me.chkShowCPUCards.Text = "All Cards Face Up"
        Me.chkShowCPUCards.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(92, 39)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(23, 21)
        Me.Button1.TabIndex = 12
        Me.Button1.Text = "?"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(121, 221)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(94, 23)
        Me.Button2.TabIndex = 13
        Me.Button2.Text = "Background..."
        Me.Button2.UseVisualStyleBackColor = True
        '
        'OptionsDialog
        '
        Me.AcceptButton = Me.OK_Button
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.Cancel_Button
        Me.ClientSize = New System.Drawing.Size(236, 294)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.chkShowCPUCards)
        Me.Controls.Add(Me.cboDifficulty)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.numPlayTo)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.chkPlayByPlayLogging)
        Me.Controls.Add(Me.picCardBack)
        Me.Controls.Add(Me.cboCardBacks)
        Me.Controls.Add(Me.chkSound)
        Me.Controls.Add(Me.cboGameSpeed)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "OptionsDialog"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Options"
        Me.TableLayoutPanel1.ResumeLayout(False)
        CType(Me.picCardBack, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.numPlayTo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents Cancel_Button As System.Windows.Forms.Button
    Friend WithEvents Label1 As Label
    Friend WithEvents cboGameSpeed As ComboBox
    Friend WithEvents chkSound As CheckBox
    Friend WithEvents cboCardBacks As ComboBox
    Friend WithEvents Label2 As Label
    Friend WithEvents picCardBack As PictureBox
    Friend WithEvents chkPlayByPlayLogging As CheckBox
    Friend WithEvents Label3 As Label
    Friend WithEvents numPlayTo As NumericUpDown
    Friend WithEvents Label4 As Label
    Friend WithEvents cboDifficulty As ComboBox
    Friend WithEvents chkShowCPUCards As CheckBox
    Friend WithEvents Button1 As Button
    Friend WithEvents Button2 As Button
    Friend WithEvents ColorDialog1 As ColorDialog
End Class
