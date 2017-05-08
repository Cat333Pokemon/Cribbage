Imports System.Windows.Forms
Imports System.Runtime.InteropServices

Public Class OptionsDialog
    '<DllImport("cards32.dll")>
    'Private Shared Function cdtInit(ByRef width As Integer, ByRef height As Integer) As Boolean
    'End Function
    '<DllImport("cards.dll")>
    'Private Shared Sub cdtTerm()
    'End Sub
    '<DllImport("cards.dll")>
    'Private Shared Function cdtDraw(hdc As IntPtr, x As Integer, y As Integer, card As Integer, mode As Integer, color As Long) As Boolean
    'End Function

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Select Case cboGameSpeed.SelectedIndex
            Case 0
                Form1.CPUDelay = Form1.GameSpeeds.Slow
            Case 1
                Form1.CPUDelay = Form1.GameSpeeds.Medium
            Case 2
                Form1.CPUDelay = Form1.GameSpeeds.Fast
            Case 3
                Form1.CPUDelay = Form1.GameSpeeds.Instant
        End Select

        Form1.CardBack = cboCardBacks.SelectedIndex
        Form1.Timer1.Start()
        Form1.PlayByPlay = chkPlayByPlayLogging.Checked
        Form1.PlayTo = numPlayTo.Value
        Form1.CurrentDifficulty = cboDifficulty.SelectedIndex
        Form1.AllowSeeingCPUCards = chkShowCPUCards.Checked
        Form1.SoundOn = chkSound.Checked
        GC.Collect()
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Form1.Timer1.Start()
        GC.Collect()
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub OptionsDialog_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Form1.Timer1.Stop()

        Select Case Form1.CPUDelay
            Case Form1.GameSpeeds.Slow
                cboGameSpeed.SelectedIndex = 0
            Case Form1.GameSpeeds.Medium
                cboGameSpeed.SelectedIndex = 1
            Case Form1.GameSpeeds.Fast
                cboGameSpeed.SelectedIndex = 2
            Case Form1.GameSpeeds.Instant
                cboGameSpeed.SelectedIndex = 3
        End Select

        cboCardBacks.Items.Clear()
        For Each en In System.Enum.GetValues(GetType(Form1.CardBacks))
            cboCardBacks.Items.Add(en.ToString.Replace("_", " "))
        Next
        cboCardBacks.SelectedIndex = Form1.CardBack

        cboDifficulty.Items.Clear()
        For Each df In System.Enum.GetValues(GetType(Form1.Difficulty))
            cboDifficulty.Items.Add(df)
        Next
        cboDifficulty.SelectedIndex = Form1.CurrentDifficulty


        chkPlayByPlayLogging.Checked = Form1.PlayByPlay
        chkShowCPUCards.Checked = Form1.AllowSeeingCPUCards
        chkSound.Checked = Form1.SoundOn

        numPlayTo.Value = Form1.PlayTo


        If Form1.CurrentState = Form1.GameState.GameOver Then
            chkPlayByPlayLogging.Enabled = True
            chkShowCPUCards.Enabled = True
            numPlayTo.Enabled = True
            cboDifficulty.Enabled = True
        Else
            chkPlayByPlayLogging.Enabled = False
            chkShowCPUCards.Enabled = False
            numPlayTo.Enabled = False
            cboDifficulty.Enabled = False
        End If
    End Sub

    Private Sub ComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboCardBacks.SelectedIndexChanged
        picCardBack.Image = CType(My.Resources.ResourceManager.GetObject("b" & cboCardBacks.SelectedIndex), Image)
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        MsgBox("How does the CPU think?" & vbCrLf & vbCrLf &
               "BEGINNER" & vbCrLf &
               " • Discards fives and aces first. Other discards are random." & vbCrLf &
               " • Plays to leave fives and twenty-ones. Other plays are random." & vbCrLf & vbCrLf &
               "EASY" & vbCrLf &
               " • Discards randomly." & vbCrLf &
               " • Plays randomly." & vbCrLf & vbCrLf &
               "MEDIUM" & vbCrLf &
               " • Holds best hand with best discard to self and worst discard to you." & vbCrLf &
               " • Plays fifteens, thirty-ones, and pairs. Avoids leaving fives and twenty-ones. Other plays are random." & vbCrLf & vbCrLf &
               "HARD" & vbCrLf &
               " • Discards fives, fifteens, and pairs to self. Avoids those to you. Holds aces. Other discards are random." & vbCrLf &
               " • Plays fifteens, thirty-ones, and pairs. Avoids leaving fives and twenty-ones. Other plays are random." _
               , MsgBoxStyle.Information)
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If ColorDialog1.ShowDialog() = DialogResult.OK Then
            Form1.BackColor = ColorDialog1.Color
        End If
    End Sub
End Class
