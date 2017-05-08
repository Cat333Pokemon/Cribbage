Imports Cribbage.CardGameFramework.Deck

Public Class Form1
    Public myHand As New List(Of CardGameFramework.Card)
    Public myRealHand As List(Of CardGameFramework.Card) 'Backup of myHand used to score after pegging
    Public oppHand As New List(Of CardGameFramework.Card)
    Public oppRealHand As List(Of CardGameFramework.Card) 'Backup of oppHand used to score after pegging
    Public theDeck As New CardGameFramework.Deck
    Public crib As New List(Of CardGameFramework.Card)
    Public cardsInPlay As New List(Of CardGameFramework.Card)
    Public cardsInPlayArray() As CardGameFramework.Card
    Public TurnUpCard As CardGameFramework.Card
    Public myScore As Integer = 0
    Public oppScore As Integer = 0
    Public playAreaScore As Integer = 0
    Public AlreadyGoOnce As Boolean = False
    Public CPUGoesNext As Boolean = False
    Public CurrentState As GameState
    Public PlayersCrib = True
    Public MessagesPrinted As Integer = 0
    Public HandNumber As Integer = 1


    Dim ran As Random

    'Options
    Public CPUDelay As Integer = GameSpeeds.Fast 'Medium is best after testing is done
    Public CardBack As Integer = CardBacks.Crosshatch_1
    Public PlayByPlay As Boolean = False
    Public PlayTo As Integer = 121
    Public SoundOn As Boolean = True
    Public CurrentDifficulty As Integer = Difficulty.Medium
    Public AllowSeeingCPUCards As Boolean = False

    Enum GameSpeeds As Integer
        Slow = 3000
        Medium = 1000
        Fast = 500
        Instant = 0
    End Enum

    Enum CardBacks As Integer
        Plain
        Crosshatch_1
        Crosshatch_2
        Robot
        Roses
        Black_Vines
        Blue_Vines
        Four_Fish
        Three_Fish
        Shell
        Mansion
        Beach
        Hand
    End Enum

    Enum GameState
        HandFinished
        PuttingInCrib
        CPUPuttingInCrib
        Pegging
        Counting1
        Counting2
        CountingCrib
        GameOver
        GameReady
    End Enum

    Enum Difficulty
        Beginner
        Easy
        Medium
        Hard
    End Enum

    Enum NumberWords
        Zero
        One
        Two
        Three
        Four
        Five
        Six
        Seven
        Eight
        Nine
        Ten
        Eleven
        Twelve
        Thirteen
        Fourteen
        Fifteen
        Sixteen
        Seventeen
        Eighteen
        Nineteen
        Twenty
        Twenty_one
        Twenty_two
        Twenty_three
        Twenty_four
        Twenty_five
        Twenty_six
        Twenty_seven
        Twenty_eight
        Twenty_nine
        Thirty
        Thirty_one
    End Enum

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CurrentState = GameState.GameOver

        PrintMessage("*Welcome to Cat's Cribbage!")
        PlayVoice("Welcome to Cat's Cribbage.")

        ran = New Random()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles btnNewRound.Click
        UnboldMessages()
        btnNewRound.BackColor = Color.LightGray
        Dim scoreres As Integer = 0

        If CurrentState = GameState.GameOver Then
            btnNewRound.Enabled = True
            btnNewRound.Text = "Continue"
            CurrentState = GameState.HandFinished
            theDeck = New CardGameFramework.Deck
            theDeck.Shuffle()

            MessagesPrinted = 0
            HandNumber = 1
            playAreaScore = 0
            myScore = 0
            oppScore = 0
            CribGroupBox.Text = "Crib"

            PrintPlayAreaScore()
            myHand.Clear()
            oppHand.Clear()
            crib.Clear()
            cardsInPlay.Clear()
            PrintMyCards(True)
            PrintCPUCards(AllowSeeingCPUCards)
            PrintCrib(AllowSeeingCPUCards)
            PrintScore()
            TurnUpCardImage.Visible = False
            PrintCardsInPlay()
            myHand.Add(theDeck.Draw())
            oppHand.Add(theDeck.Draw())
            If PlayByPlay Then PrintMessage("You draw the " & myHand.Last.ToString() & ".")
            If PlayByPlay Then PrintMessage("The CPU draws the " & oppHand.Last.ToString() & ".")
            Update()
            Wait(True)
            PrintMyCards(True)
            Update()
            Wait(True)
            PrintCPUCards(True)
            While myHand.Last.FaceVal = oppHand.Last.FaceVal
                'Same value cards. Draw another one.

                'To prevent crashing
                If myHand.Count > 5 Then myHand.Clear()
                If oppHand.Count > 5 Then oppHand.Clear()

                PrintMessage("You and the CPU draw cards of the same value. You will both draw new cards.")
                myHand.Add(theDeck.Draw())
                oppHand.Add(theDeck.Draw())
                If PlayByPlay Then PrintMessage("You draw the " & myHand.Last.ToString() & ".")
                If PlayByPlay Then PrintMessage("The CPU draws the " & oppHand.Last.ToString() & ".")
                Update()
                Wait(True)
                PrintMyCards(True)
                Update()
                Wait(True)
                PrintCPUCards(True)
            End While

            If myHand.Last.FaceVal < oppHand.Last.FaceVal Then
                PlayersCrib = True
                PlayVoice("You deal first.")
                PrintMessage("You draw the low card and will be the first dealer.")
            Else
                PlayVoice("I deal first.")
                PrintMessage("The CPU draws the low card and will be the first dealer.")
                PlayersCrib = False
            End If

        ElseIf CurrentState = GameState.HandFinished Then
            btnNewRound.Enabled = False
            btnNewRound.Text = "Continue"
            theDeck = New CardGameFramework.Deck
            GC.Collect() 'Garbage collect

            theDeck.Shuffle()
            myHand.Clear()
            oppHand.Clear()
            crib.Clear()
            cardsInPlay.Clear()
            playAreaScore = 0
            PrintPlayAreaScore()

            If PlayersCrib Then
                CribGroupBox.Text = "Your Crib"
            Else
                CribGroupBox.Text = "CPU's Crib"
            End If

            If PlayersCrib Then
                PrintMessage("This is hand #" & HandNumber & ". It is your deal.")
            Else
                PrintMessage("This is hand #" & HandNumber & ". It is the CPU's deal.")
            End If

            If PlayByPlay Then
                PrintMessage("Your score is " & myScore & ".")
                PrintMessage("The CPU's score is " & oppScore & ".")
            End If

            TurnUpCardImage.Visible = False
            PrintMyCards(True)
            PrintCPUCards(AllowSeeingCPUCards)
            PrintCrib(True)
            PrintCardsInPlay()
            If PrintScore() Then Exit Sub
            Update()

            'myHand.Add(New CardGameFramework.Card(CardGameFramework.Suit.Clubs, CardGameFramework.FaceValue.Five, True))
            'myHand.Add(New CardGameFramework.Card(CardGameFramework.Suit.Diamonds, CardGameFramework.FaceValue.Five, True))
            'myHand.Add(New CardGameFramework.Card(CardGameFramework.Suit.Hearts, CardGameFramework.FaceValue.Five, True))
            'myHand.Add(New CardGameFramework.Card(CardGameFramework.Suit.Spades, CardGameFramework.FaceValue.Five, True))
            'myHand.Add(New CardGameFramework.Card(CardGameFramework.Suit.Diamonds, CardGameFramework.FaceValue.Jack, True))
            'myHand.Add(New CardGameFramework.Card(CardGameFramework.Suit.Spades, CardGameFramework.FaceValue.Jack, True))
            'oppHand.Add(New CardGameFramework.Card(CardGameFramework.Suit.Clubs, CardGameFramework.FaceValue.Five, True))
            'oppHand.Add(New CardGameFramework.Card(CardGameFramework.Suit.Diamonds, CardGameFramework.FaceValue.Five, True))
            'oppHand.Add(New CardGameFramework.Card(CardGameFramework.Suit.Hearts, CardGameFramework.FaceValue.Five, True))
            'oppHand.Add(New CardGameFramework.Card(CardGameFramework.Suit.Spades, CardGameFramework.FaceValue.Seven, True))
            'oppHand.Add(New CardGameFramework.Card(CardGameFramework.Suit.Diamonds, CardGameFramework.FaceValue.Eight, True))
            'oppHand.Add(New CardGameFramework.Card(CardGameFramework.Suit.Clubs, CardGameFramework.FaceValue.Nine, True))
            For i As Integer = 0 To 5
                'Deal to CPU first if it's player's deal
                If PlayersCrib Then
                    oppHand.Add(theDeck.Draw())
                    PrintCPUCards(AllowSeeingCPUCards)
                    Update()
                    Wait(True)
                End If

                myHand.Add(theDeck.Draw())
                PrintMyCards(True)
                Update()
                Wait(True)

                If Not PlayersCrib Then
                    oppHand.Add(theDeck.Draw())
                    PrintCPUCards(AllowSeeingCPUCards)
                    Update()
                    Wait(True)
                End If

            Next

            TurnUpCard = theDeck.Draw()

            If AllowSeeingCPUCards Then
                TurnUpCardImage.Image = CType(My.Resources.ResourceManager.GetObject("_" & TurnUpCard.FaceVal & TurnUpCard.Suit.ToString.Substring(0, 1).ToLower), Image)
            Else
                TurnUpCardImage.Image = CType(My.Resources.ResourceManager.GetObject("b" & CardBack), Image)
            End If

            TurnUpCardImage.Visible = True


            If PlayersCrib Then
                PrintMessage("Select two cards to send to YOUR crib.")
                PlayVoice("It's your crib.")
            Else
                PrintMessage("Select two cards to send to the CPU's crib.")
                PlayVoice("It's my crib.")
            End If

            CurrentState = GameState.PuttingInCrib
        ElseIf CurrentState = GameState.CPUPuttingInCrib Then
            'btnNewRound.Enabled = False
            'TODO: Better AI than this should be added later. Maybe this can be the "easy" difficulty to select two cards at random.
            'CPUPutInCrib()
        ElseIf CurrentState = GameState.Pegging Then

            'Go button
            btnNewRound.Enabled = False
            btnNewRound.Text = "Continue"
            'lblNextAction.Text = "Select a card to play."
            'lblNextAction.Text = Nothing
            If AlreadyGoOnce Then
                AlreadyGoOnce = False
                ResetPegging()
            ElseIf oppHand.Count > 0 Then
                PrintMessage("You say 'go.'")
            End If
            If Not CPUGoesNext Then
                'This is a hack, but whatever
                If playAreaScore > 0 Then
                    AlreadyGoOnce = False
                    ResetPegging()
                End If
                'MsgBox("Bug? " & playAreaScore)
                'AlreadyGoOnce = False
                'ResetPegging()
                'CPUPlayCard(False)
            Else


                AlreadyGoOnce = True
                CPUPlayCard(False)
                If oppScore >= PlayTo Or myScore >= PlayTo Then Exit Sub
            End If
        ElseIf CurrentState = GameState.Counting1 Then
            CurrentState = GameState.Counting2
            'lblNextAction.Text = "click button"
            ResetPegging()
            myHand = New List(Of CardGameFramework.Card)(myRealHand)
            oppHand = New List(Of CardGameFramework.Card)(oppRealHand)
            If Not PlayersCrib Then 'Score opponent first
                PrintMessage("Your hand will be scored first.")
                PrintMyCards(True)
                PrintCPUCards(AllowSeeingCPUCards)
                scoreres = HandScoring(myHand)
                myScore += scoreres
                PrintMessage("You peg " & scoreres & ".")
                PrintPlayAreaScore(scoreres)
                PlayVoice("Your hand scores " & scoreres)
            Else
                PrintMessage("The CPU's hand will be scored first.")
                PrintMyCards(AllowSeeingCPUCards)
                PrintCPUCards(True)
                scoreres = HandScoring(oppHand)
                oppScore += scoreres
                PrintMessage("The CPU pegs " & scoreres & ".")
                PrintPlayAreaScore(scoreres)
                PlayVoice("My hand scores " & scoreres)
            End If
            If PrintScore() Then Exit Sub
        ElseIf CurrentState = GameState.Counting2 Then
            CurrentState = GameState.CountingCrib
            If PlayersCrib Then
                PrintMessage("Now your hand will be scored.")
                PrintMyCards(True)
                PrintCPUCards(AllowSeeingCPUCards)
                scoreres = HandScoring(myHand)
                myScore += scoreres
                PrintMessage("You peg " & scoreres & ".")
                PrintPlayAreaScore(scoreres)
                PlayVoice("Your hand scores " & scoreres)
            Else
                PrintMessage("Now the CPU's hand will be scored.")
                PrintMyCards(AllowSeeingCPUCards)
                PrintCPUCards(True)
                scoreres = HandScoring(oppHand)
                oppScore += scoreres
                PrintMessage("The CPU pegs " & scoreres & ".")
                PrintPlayAreaScore(scoreres)
                PlayVoice("My hand scores " & scoreres)
            End If
            If PrintScore() Then Exit Sub
        ElseIf CurrentState = GameState.CountingCrib Then
            CurrentState = GameState.HandFinished
            If PlayersCrib Then
                PrintMessage("Finally, your crib will be scored.")
            Else
                PrintMessage("Finally, the CPU's crib will be scored.")
            End If
            PrintMyCards(AllowSeeingCPUCards)
            PrintCPUCards(AllowSeeingCPUCards)
            PrintCrib(True)
            scoreres = HandScoring(crib, True)

            If PlayersCrib Then
                myScore += scoreres
                PrintMessage("You peg " & scoreres & ".")
                PlayVoice("Your crib scores " & scoreres)
            Else
                oppScore += scoreres
                PrintMessage("The CPU pegs " & scoreres & ".")
                PlayVoice("My crib scores " & scoreres)
            End If
            PrintPlayAreaScore(scoreres)
            PrintMessage("Let's start the next hand.")
            HandNumber += 1
            PlayersCrib = Not PlayersCrib 'Switch whose crib it is
            If PrintScore() Then Exit Sub
        End If

    End Sub

    Public Sub TurnUpTheCard()
        If PlayByPlay Then PrintMessage("The cut card is the " & TurnUpCard.ToString() & ".")
        TurnUpCardImage.Image = CType(My.Resources.ResourceManager.GetObject("_" & TurnUpCard.FaceVal & TurnUpCard.Suit.ToString.Substring(0, 1).ToLower), Image)
        If TurnUpCard.FaceVal = CardGameFramework.FaceValue.Jack Then
            If PlayersCrib Then
                myScore += 2
                PrintMessage("You peg 2 for his heels.")
            Else
                oppScore += 2
                PrintMessage("The CPU pegs 2 for his heels.")
            End If
            If PrintScore() Then Exit Sub
        End If
    End Sub

    'Return the win condition, and the calling function should terminate if true
    Public Function PrintScore() As Boolean
        lblMyScore.Text = myScore 'IIf(myScore >= PlayTo, "WIN", myScore)
        lblOppScore.Text = oppScore 'IIf(oppScore >= PlayTo, "WIN", oppScore)
        If myScore >= PlayTo Or oppScore >= PlayTo Then
            CurrentState = GameState.GameOver
            btnNewRound.Enabled = True
            btnNewRound.Text = "New Game"
            If myScore >= PlayTo Then
                PrintMessage("You win!")
            Else
                PrintMessage("The CPU wins!")
            End If
            If myScore < PlayTo - 90 Or oppScore < PlayTo - 90 Then
                PlaySound("skunk")
                PrintMessage("It's a triple skunk!")
            ElseIf myScore < PlayTo - 60 Or oppScore < PlayTo - 60 Then
                PlaySound("skunk")
                PrintMessage("It's a double skunk!")
            ElseIf myScore < PlayTo - 30 Or oppScore < PlayTo - 30 Then
                PlaySound("skunk")
                PrintMessage("It's a skunk!")
            End If
            If PlayByPlay Then
                PrintMessage("The final score is " & myScore & " (you) to " & oppScore & " (CPU).")
            End If

            If myScore >= PlayTo Then
                If oppScore < PlayTo - 90 Then
                    PlaySound("skunk")
                    MsgBox("Congratulations, you triple-skunked the CPU!", MsgBoxStyle.Information)
                ElseIf oppScore < PlayTo - 60 Then
                    PlaySound("skunk")
                    MsgBox("Congratulations, you double-skunked the CPU!", MsgBoxStyle.Information)
                ElseIf oppScore < PlayTo - 30 Then
                    PlaySound("skunk")
                    MsgBox("Congratulations, you skunked the CPU!", MsgBoxStyle.Information)
                Else
                    PlayVoice("Congratulations, you win!")
                    MsgBox("Congratulations, you win!", MsgBoxStyle.Information)
                End If
            Else
                If myScore < PlayTo - 90 Then
                    MsgBox("The CPU triple-skunked you! Better luck next time.", MsgBoxStyle.Information)
                ElseIf myScore < PlayTo - 60 Then
                    MsgBox("The CPU double-skunked you! Better luck next time.", MsgBoxStyle.Information)
                ElseIf myScore < PlayTo - 30 Then
                    MsgBox("The CPU skunked you! Better luck next time.", MsgBoxStyle.Information)
                Else
                    PlayVoice("I win! Better luck next time.")
                    MsgBox("The CPU wins! Better luck next time.", MsgBoxStyle.Information)
                End If
            End If

            Return True 'Winner; end game
        Else
            Return False
        End If

    End Function

    Public Sub PrintPlayAreaScore(ByVal Optional scores As Integer = -1)
        If scores > -1 Then
            lblPlayAreaScore.Text = "Score:" & vbCrLf & scores
            Update()
        Else
            lblPlayAreaScore.Text = "Count:" & vbCrLf & playAreaScore
            Update()

            If playAreaScore > 0 Then
                If playAreaScore = 15 Then
                    PlayVoice("15 for two.", True)
                    'PlaySound("_15_2", True)
                ElseIf playAreaScore = 31 Then
                    PlayVoice("31 for two.", True)
                    'PlaySound("_31_2", True)
                Else
                    PlayVoice(playAreaScore, True)
                    'PlaySound("_" & playAreaScore, True)
                End If
            End If
        End If
    End Sub

    Public Sub PlaySound(ByVal ResourceName As String, ByVal Optional WaitForFinish As Boolean = False)
        Update() 'Even if not playing a sound, an update should still be forced
        If SoundOn Then
            If WaitForFinish Then
                Me.Cursor = Cursors.WaitCursor
                My.Computer.Audio.Play(My.Resources.ResourceManager.GetObject(ResourceName), AudioPlayMode.WaitToComplete)
                Me.Cursor = Cursors.Default
            Else
                My.Computer.Audio.Play(My.Resources.ResourceManager.GetObject(ResourceName), AudioPlayMode.Background)
            End If
        End If
    End Sub

    Public Sub PlayVoice(ByVal voicestring As String, ByVal Optional WaitForFinish As Boolean = False)
        Update()

        If SoundOn Then

            Dim speechsy As New System.Speech.Synthesis.SpeechSynthesizer()
            Select Case CPUDelay
                Case GameSpeeds.Slow
                    speechsy.Rate = -1
                Case GameSpeeds.Medium
                    speechsy.Rate = 0
                Case GameSpeeds.Fast
                    speechsy.Rate = 2
                Case GameSpeeds.Instant
                    speechsy.Rate = 4
            End Select

            Me.Cursor = Cursors.WaitCursor
            If WaitForFinish Then
                speechsy.Speak(voicestring)
            Else
                speechsy.SpeakAsync(voicestring)
            End If
            Me.Cursor = Cursors.Default
            Update()
        End If
    End Sub

    Public Sub PrintMyCards(ByVal visible As Boolean)
        For i As Integer = 6 To myHand.Count + 1 Step -1
            CType(CardGroupBox.Controls("Card" & i), PictureBox).Visible = False
        Next
        For i As Integer = 1 To myHand.Count
            CType(CardGroupBox.Controls("Card" & i), PictureBox).Visible = True
        Next
        Try
            If visible Then
                For i As Integer = 1 To myHand.Count
                    CType(CardGroupBox.Controls("Card" & i), PictureBox).Image =
                        CType(My.Resources.ResourceManager.GetObject("_" & myHand(i - 1).FaceVal & myHand(i - 1).Suit.ToString.Substring(0, 1).ToLower), Image)
                Next
            Else
                For i As Integer = 1 To myHand.Count
                    CType(CardGroupBox.Controls("Card" & i), PictureBox).Image = CType(My.Resources.ResourceManager.GetObject("b" & CardBack), Image)
                Next
            End If
        Catch fnf As System.IO.FileNotFoundException
            MsgBox("File not found: " & fnf.Message)
        Catch oor As IndexOutOfRangeException
        Catch other As Exception
            Throw other

        End Try
    End Sub

    Public Sub PrintCPUCards(ByVal visible As Boolean)
        For i As Integer = 6 To oppHand.Count + 1 Step -1
            CType(CPUCardGroupBox.Controls("CPUCard" & i), PictureBox).Visible = False
        Next
        For i As Integer = 1 To oppHand.Count
            CType(CPUCardGroupBox.Controls("CPUCard" & i), PictureBox).Visible = True
        Next
        Try
            If visible Then
                For i As Integer = 1 To oppHand.Count
                    CType(CPUCardGroupBox.Controls("CPUCard" & i), PictureBox).Image =
                        CType(My.Resources.ResourceManager.GetObject("_" & oppHand(i - 1).FaceVal & oppHand(i - 1).Suit.ToString.Substring(0, 1).ToLower), Image)

                Next
            Else
                For i As Integer = 1 To oppHand.Count
                    CType(CPUCardGroupBox.Controls("CPUCard" & i), PictureBox).Image = CType(My.Resources.ResourceManager.GetObject("b" & CardBack), Image)
                Next
            End If
        Catch fnf As System.IO.FileNotFoundException
            MsgBox("File not found: " & fnf.Message)
        Catch oor As IndexOutOfRangeException
        Catch other As Exception
            Throw other

        End Try
    End Sub

    Public Sub PrintCrib(ByVal visible As Boolean)
        For i As Integer = 4 To crib.Count + 1 Step -1
            CType(CribGroupBox.Controls("CribCard" & i), PictureBox).Visible = False
        Next
        For i As Integer = 1 To crib.Count
            CType(CribGroupBox.Controls("CribCard" & i), PictureBox).Visible = True
        Next
        Try
            If visible Then
                For i As Integer = 1 To crib.Count
                    CType(CribGroupBox.Controls("CribCard" & i), PictureBox).Image =
                        CType(My.Resources.ResourceManager.GetObject("_" & crib(i - 1).FaceVal & crib(i - 1).Suit.ToString.Substring(0, 1).ToLower), Image)
                Next
            Else
                For i As Integer = 1 To crib.Count
                    CType(CribGroupBox.Controls("CribCard" & i), PictureBox).Image = CType(My.Resources.ResourceManager.GetObject("b" & CardBack), Image)
                Next
            End If
        Catch fnf As System.IO.FileNotFoundException
            MsgBox("File not found: " & fnf.Message)
        Catch oor As IndexOutOfRangeException
        Catch other As Exception
            Throw other

        End Try
    End Sub

    Public Sub PrintCardsInPlay()
        For i = 1 To PlayGroupBox.Controls.Count
            PlayGroupBox.Controls(0).Dispose()
        Next
        Dim startspot As Integer = 6
        For Each pc As CardGameFramework.Card In cardsInPlay
            Dim NewCard As New PictureBox
            NewCard.Size = New System.Drawing.Size(71, 96)
            NewCard.Parent = PlayGroupBox
            NewCard.SizeMode = PictureBoxSizeMode.CenterImage
            Try
                NewCard.Image = CType(My.Resources.ResourceManager.GetObject("_" & pc.FaceVal & pc.Suit.ToString.Substring(0, 1).ToLower), Image)
            Catch fnf As System.IO.FileNotFoundException
                MsgBox("File not found: " & fnf.Message)
            Catch oor As IndexOutOfRangeException
            Catch other As Exception
                Throw other

            End Try
            NewCard.Location = New System.Drawing.Point(startspot, 20)
            NewCard.BringToFront()
            startspot += 20
        Next
    End Sub

    Public Sub PrintMessage(ByRef message As String)
        'Dim sse = txtMessages.TextLength
        Dim bold As New Font(DirectCast(txtMessages.SelectionFont.Clone(), Font), txtMessages.SelectionFont.Style Or FontStyle.Bold)
        'Dim bolditalic As New Font(DirectCast(txtMessages.SelectionFont.Clone(), Font), txtMessages.SelectionFont.Style Or FontStyle.Bold Or FontStyle.Italic)
        'txtMessages.SelectionStart = sse + 1
        'txtMessages.SelectionLength = txtMessages.Text.Length - sse - 1
        'txtMessages.Select(sse + 1, txtMessages.Text.Length - sse - 1)
        'MsgBox(txtMessages.SelectionLength)
        txtMessages.SelectionStart = txtMessages.TextLength
        txtMessages.SelectionLength = 0
        txtMessages.SelectionColor = Color.Black

        If message.Chars(0) = "*" Then
            'txtMessages.SelectionFont = bolditalic
            txtMessages.SelectionFont = bold
            txtMessages.SelectionColor = Color.DimGray
            txtMessages.AppendText(message.Substring(1) & vbCrLf)
        Else
            MessagesPrinted += 1
            txtMessages.SelectionColor = Color.Brown
            txtMessages.AppendText(MessagesPrinted & ". ")
            txtMessages.SelectionFont = bold
            If message.Contains("peg") Then

                txtMessages.SelectionColor = Color.Blue
            Else
                txtMessages.SelectionColor = Color.Black
            End If
            txtMessages.AppendText(message & vbCrLf)
        End If

        txtMessages.SelectionStart = txtMessages.TextLength
        txtMessages.ScrollToCaret()

        Update()
    End Sub

    Public Sub UnboldMessages()
        txtMessages.SelectAll()
        txtMessages.SelectionFont = txtMessages.Font
        txtMessages.SelectionStart = txtMessages.TextLength
        txtMessages.ScrollToCaret()
        Update()
    End Sub

    Private Sub Card_Click(sender As Object, e As EventArgs) Handles _
        Card1.Click, Card2.Click, Card3.Click, Card4.Click, Card5.Click, Card6.Click

        UnboldMessages()

        If CurrentState = GameState.PuttingInCrib Then
            'MsgBox(myHand(CInt(CType(sender, PictureBox).Name.Substring(4, 1)) - 1))
            crib.Add(myHand(CInt(CType(sender, PictureBox).Name.Substring(4, 1)) - 1))
            myHand.RemoveAt(CInt(CType(sender, PictureBox).Name.Substring(4, 1)) - 1)

            PrintCrib(AllowSeeingCPUCards)
            PrintMyCards(True)
            If myHand.Count = 4 Then
                If PlayByPlay Then PrintMessage("You put two cards in the crib.")
                'Two cards put in
                CurrentState = GameState.CPUPuttingInCrib
                PrintMessage("The CPU puts two cards in the crib.")

                CPUPutInCrib()
                PrintMessage("The cut card is turned up.")
                Wait()

                TurnUpTheCard()

                PrintMessage("Let's play.")

                If PlayersCrib Then
                    CPUPlayCard(False)
                    If oppScore >= PlayTo Or myScore >= PlayTo Then Exit Sub
                End If
                'btnNewRound.Text = "Continue"

                'btnNewRound.Enabled = True
            End If
        ElseIf CurrentState = GameState.Pegging Then
            'Playing a card
            Dim thisscore As Integer = myHand(CInt(CType(sender, PictureBox).Name.Substring(4, 1)) - 1).FaceVal
            If thisscore > 10 Then thisscore = 10
            If playAreaScore + thisscore <= 31 Then
                cardsInPlay.Add(myHand(CInt(CType(sender, PictureBox).Name.Substring(4, 1)) - 1))
                If PlayByPlay Then PrintMessage("You play the " & myHand(CInt(CType(sender, PictureBox).Name.Substring(4, 1)) - 1).ToString() & ".")
                myHand.RemoveAt(CInt(CType(sender, PictureBox).Name.Substring(4, 1)) - 1)
                PrintMyCards(True)
                playAreaScore += thisscore

                PrintCardsInPlay()
                Dim myadv As Integer = CheckPlayAdvancedScoring()
                If myadv > 0 Then
                    PrintMessage("You peg " & myadv & ".")
                    myScore += myadv
                    If PrintScore() Then Exit Sub
                End If
                PrintPlayAreaScore()
                If playAreaScore = 31 Then
                    btnNewRound.Text = "Continue"
                    btnNewRound.Enabled = True
                    myScore += 2
                    If myHand.Count = 0 And oppHand.Count = 0 Then
                        CurrentState = GameState.Counting1
                        PrintMessage("You peg 2 for thirty-one. All cards have been played.")
                    Else
                        PrintMessage("You peg 2 for thirty-one.")
                        CPUGoesNext = True
                        AlreadyGoOnce = True 'To force CPU to go next
                    End If
                    If PrintScore() Then Exit Sub
                Else
                    CPUGoesNext = True
                    CPUPlayCard(True)
                    If oppScore >= PlayTo Or myScore >= PlayTo Then Exit Sub
                End If
            Else
                MsgBox("Playing that card would make the count exceed 31.", MsgBoxStyle.Exclamation)
            End If

        End If

    End Sub

    Public Sub Wait(ByVal Optional HalfDelay As Boolean = False)
        Me.Cursor = Cursors.WaitCursor
        'If SoundOn Then HalfDelay = True
        If HalfDelay Then
            Threading.Thread.Sleep(CPUDelay / 2)
        Else
            Threading.Thread.Sleep(CPUDelay)
        End If
        Me.Cursor = Cursors.Default
    End Sub

    'Assigns arbitrary values to two cards for CPU discarding
    Public Function PairValue(ByVal card1 As CardGameFramework.Card, ByVal card2 As CardGameFramework.Card) As Integer
        If (card1.FaceVal = 5 And card2.FaceVal = 5) Then 'The most dangerous pair
            Return 6
        ElseIf (card1.FaceVal = 8 And card2.FaceVal = 7) Or (card1.FaceVal = 7 And card2.FaceVal = 8) Then '7-8 also dangerous
            Return 5
        ElseIf card1.FaceVal + card2.FaceVal = 15 Then 'Fifteen
            Return 4
        ElseIf card1.FaceVal = 5 Or card2.FaceVal = 5 Then 'Either is five
            Return 4
        ElseIf card1.FaceVal = card2.FaceVal Then 'Pair
            Return 3
        ElseIf card1.FaceVal + card2.FaceVal = 5 Then 'Add to five
            Return 3
        ElseIf card1.FaceVal + card2.FaceVal = 20 Then 'Two ten-value cards
            Return 2
        ElseIf card1.FaceVal + 1 = card2.FaceVal Or card1.FaceVal - 1 = card2.FaceVal Then 'cards are 1 apart
            Return 2
        ElseIf PlayersCrib And
                card1.FaceVal = CardGameFramework.FaceValue.Ace Or card2.FaceVal = CardGameFramework.FaceValue.Ace Then 'Ace useful when playing
            Return 2
        ElseIf card1.Suit = card2.Suit Then
            Return 1
        ElseIf Not PlayersCrib And
                card1.FaceVal = CardGameFramework.FaceValue.Ace Or card2.FaceVal = CardGameFramework.FaceValue.Ace Then 'Ace useful when playing
            Return -1
        Else
            Return 0
        End If
    End Function

    Public Sub CPUPutInCrib()
        Dim foundafifteen As Integer = -1
        Dim safecards As New ArrayList
        Dim temphand As List(Of CardGameFramework.Card)
        Dim temphandscore As Integer
        Dim besthand As Integer = -1
        Dim besttwotoremove(1) As Integer
        Dim discardedcardvalue As Integer = 999

        'TODO: Better AI for difficulties above Easy
        Update()
        Wait()

        If CurrentDifficulty = Difficulty.Medium Then
            For i = 0 To oppHand.Count - 1
                For j = 0 To oppHand.Count - 1
                    If i <> j Then
                        temphand = oppHand.ToList()
                        temphand.RemoveAt(i)
                        temphand.RemoveAt(IIf(j > i, j - 1, j))
                        temphandscore = HandScoring(temphand, False, True) 'BestOfFour could be false for Expert :)
                        If temphandscore > besthand Then
                            'CPU wants high value
                            besthand = temphandscore
                            besttwotoremove = {i, j}
                            discardedcardvalue = PairValue(oppHand(i), oppHand(j))
                        ElseIf temphandscore = besthand Then
                            'Keeper part is the same...what about the discard?
                            If PlayersCrib And PairValue(oppHand(i), oppHand(j)) < discardedcardvalue Then
                                'CPU wants LOW value
                                besttwotoremove = {i, j}
                            ElseIf Not PlayersCrib And PairValue(oppHand(i), oppHand(j)) > discardedcardvalue Then
                                'CPU wants HIGH value
                                besttwotoremove = {i, j}
                            End If
                        End If
                    End If
                Next j
            Next i

            If PlayByPlay And AllowSeeingCPUCards Then PrintMessage("CPU best hand score: " & besthand & " with discard " & discardedcardvalue)

            crib.Add(oppHand(besttwotoremove(0)))
            oppHand.RemoveAt(besttwotoremove(0))
            PrintCrib(AllowSeeingCPUCards)
            PrintCPUCards(AllowSeeingCPUCards)
            Update()
            Wait()

            crib.Add(oppHand(IIf(besttwotoremove(1) > besttwotoremove(0), besttwotoremove(1) - 1, besttwotoremove(1))))
            oppHand.RemoveAt(IIf(besttwotoremove(1) > besttwotoremove(0), besttwotoremove(1) - 1, besttwotoremove(1)))
            PrintCrib(AllowSeeingCPUCards)
            PrintCPUCards(AllowSeeingCPUCards)
            Update()
            Wait()

        Else
            For j = 0 To 1
                foundafifteen = -1
                If CurrentDifficulty = Difficulty.Beginner Then
                    For i = 0 To oppHand.Count - 1
                        'Beginner: Discard 5 and ace
                        If oppHand(i).FaceVal = CardGameFramework.FaceValue.Five Or
                            oppHand(i).FaceVal = CardGameFramework.FaceValue.Ace Then
                            foundafifteen = i
                            Exit For 'Go to next card
                        End If

                    Next i
                ElseIf CurrentDifficulty <> Difficulty.Easy Then
                    safecards.Clear()
                    For i = 0 To oppHand.Count - 1
                        'Hard
                        'Don't discard fifteens overall

                        'TODO: Possibly improve logic for avoiding fifteens and pairs; maybe also avoid getting rid of own pairs

                        '1 is just in case the CPU is discarding first...not implemented yet
                        If crib.Count = 3 Or crib.Count = 1 Then
                            'Player's crib, avoids 5/ace/15/pair; CPU's crib, all those are great (except keep ace; useful for pegging 31)
                            If (PlayersCrib And oppHand(i).FaceVal <> CardGameFramework.FaceValue.Five And
                                    oppHand(i).FaceVal <> CardGameFramework.FaceValue.Ace And
                                    oppHand(i).FaceVal + crib.Last.FaceVal <> 15 And
                                    oppHand(i).FaceVal <> crib.Last.FaceVal) Or
                                (Not PlayersCrib And (oppHand(i).FaceVal = CardGameFramework.FaceValue.Five Or
                                    oppHand(i).FaceVal + crib.Last.FaceVal = 15 Or
                                    oppHand(i).FaceVal = crib.Last.FaceVal)) Then 'keep the ace
                                safecards.Add(i)
                            End If
                        Else 'first card
                            If (PlayersCrib And oppHand(i).FaceVal <> CardGameFramework.FaceValue.Five And
                                    oppHand(i).FaceVal <> CardGameFramework.FaceValue.Ace) Or
                                (Not PlayersCrib And oppHand(i).FaceVal = CardGameFramework.FaceValue.Five) Then
                                safecards.Add(i)
                            End If
                        End If



                        'Medium
                        'Player's crib, avoids 5/15; CPU's crib, both those are great!

                        'If crib.Count = 3 Or crib.Count = 1 Then '1 is just in case the CPU is discarding first...not implemented yet
                        '    If (PlayersCrib And oppHand(i).FaceVal <> CardGameFramework.FaceValue.Five And
                        '                oppHand(i).FaceVal + crib.Last.FaceVal <> 15) Or
                        '            (Not PlayersCrib And (oppHand(i).FaceVal = CardGameFramework.FaceValue.Five Or
                        '                oppHand(i).FaceVal + crib.Last.FaceVal = 15)) Then
                        '        safecards.Add(i)
                        '    End If
                        'Else 'first card
                        '    If (PlayersCrib And oppHand(i).FaceVal <> CardGameFramework.FaceValue.Five) Or
                        '            (Not PlayersCrib And (oppHand(i).FaceVal = CardGameFramework.FaceValue.Five)) Then
                        '        safecards.Add(i)
                        '    End If
                        'End If


                    Next i

                    If safecards.Count > 0 Then
                        foundafifteen = ran.Next(0, safecards.Count) 'Get the index's index :)
                        foundafifteen = safecards(foundafifteen) 'Get the actual index of the card
                    Else
                        foundafifteen = ran.Next(0, oppHand.Count)
                    End If
                End If

                If foundafifteen = -1 Then foundafifteen = ran.Next(0, oppHand.Count)
                crib.Add(oppHand(foundafifteen))
                oppHand.RemoveAt(foundafifteen)
                PrintCrib(AllowSeeingCPUCards)
                PrintCPUCards(AllowSeeingCPUCards)
                Update()
                Wait()
            Next j
        End If

        'Copy hands so they can be played
        myRealHand = New List(Of CardGameFramework.Card)(myHand)
        oppRealHand = New List(Of CardGameFramework.Card)(oppHand)

        CurrentState = GameState.Pegging

    End Sub

    Public Function CheckPlayAdvancedScoring() As Integer
        Dim totalAdvancedScore As Integer = 0
        'cardsInPlay.CopyTo(cardsInPlayArray)
        'cardsInPlayArray.OrderBy(Of CardGameFramework.Card, CardGameFramework.FaceValue,
        'cardsInPlayArray = cardsInPlay.OrderBy(Function(x) x.FaceVal).ToArray()
        Dim cardCount() As Integer = {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}
        Dim isThisOneARun As Boolean = False
        Dim hasFoundOne As Integer = 0

        If playAreaScore = 15 Then
            totalAdvancedScore += 2
            PrintMessage("*Fifteen for 2.")
        End If

        'RUNS
        'Don't bother unless there are at least three cards'
        If cardsInPlay.Count > 2 Then
            For i = cardsInPlay.Count - 1 To 2 Step -1
                cardCount = {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}
                cardsInPlayArray = cardsInPlay.GetRange(cardsInPlay.Count - 1 - i, i + 1).OrderBy(Function(x) x.FaceVal).ToArray
                isThisOneARun = True
                For Each cd In cardsInPlayArray
                    cardCount(cd.FaceVal) += 1
                    If cardCount(cd.FaceVal) >= 2 Then
                        isThisOneARun = False
                        Exit For
                    End If
                Next
                If isThisOneARun Then
                    'Eligible to be a run.
                    hasFoundOne = 0
                    For j = 0 To 13
                        If hasFoundOne > 0 Then
                            If cardCount(j) <> 1 Then
                                If Not hasFoundOne = cardsInPlayArray.Count Then
                                    isThisOneARun = False
                                End If
                                Exit For
                            Else
                                hasFoundOne += 1
                            End If
                        Else
                            If cardCount(j) = 1 Then
                                hasFoundOne += 1
                            End If
                        End If
                    Next j

                    'MsgBox(String.Join(" ", cardCount) & vbCrLf & "Ones in a row: " & hasFoundOne)
                End If
                'Still a run!
                If isThisOneARun Then
                    PrintMessage("*A run of " & System.Enum.GetName(GetType(NumberWords), cardsInPlayArray.Count).ToLower() & " for " & cardsInPlayArray.Count & ".")
                    totalAdvancedScore += cardsInPlayArray.Count
                    PlayVoice("A run of " & cardsInPlayArray.Count & " for " & cardsInPlayArray.Count, True)
                    Exit For 'Only score one run!
                End If
            Next i
        End If



        'PAIRS
        If cardsInPlay.Count > 3 AndAlso
             cardsInPlay(cardsInPlay.Count - 1).FaceVal = cardsInPlay(cardsInPlay.Count - 2).FaceVal AndAlso
             cardsInPlay(cardsInPlay.Count - 2).FaceVal = cardsInPlay(cardsInPlay.Count - 3).FaceVal AndAlso
             cardsInPlay(cardsInPlay.Count - 3).FaceVal = cardsInPlay(cardsInPlay.Count - 4).FaceVal Then
            'Four of a kind
            PrintMessage("*A double pair royal for 12.")
            totalAdvancedScore += 12
            PlayVoice("A double pair royal for 12", True)
        ElseIf cardsInPlay.Count > 2 AndAlso
             cardsInPlay(cardsInPlay.Count - 1).FaceVal = cardsInPlay(cardsInPlay.Count - 2).FaceVal AndAlso
             cardsInPlay(cardsInPlay.Count - 2).FaceVal = cardsInPlay(cardsInPlay.Count - 3).FaceVal Then
            'Three of a kind
            PrintMessage("*A pair royal for 6.")
            totalAdvancedScore += 6
            PlayVoice("A pair royal for 6", True)
        ElseIf cardsInPlay.Count > 1 AndAlso
             cardsInPlay(cardsInPlay.Count - 1).FaceVal = cardsInPlay(cardsInPlay.Count - 2).FaceVal Then
            'Pair
            PrintMessage("*A pair for 2.")
            totalAdvancedScore += 2
            PlayVoice("A pair for 2", True)
        End If
        Return totalAdvancedScore
    End Function

    Public Function HandScoring(ByRef WhoseHand As List(Of CardGameFramework.Card), ByVal Optional IsThisTheCrib As Boolean = False,
                                ByVal Optional BestOfFour As Boolean = False) As Integer
        'Fifteens, pairs, runs, flush, nobs
        'There should be only four cards in the hand; don't add the cut card

        If WhoseHand.Count < 4 Or WhoseHand.Count > 5 Then
            PrintMessage("INVALID HAND SCORED.")
            Return 0
        End If

        Dim temphand As List(Of CardGameFramework.Card) = WhoseHand.ToList()
        If temphand.Count = 4 And Not BestOfFour Then temphand.Add(TurnUpCard)

        Dim sets As Array = New Integer()()() {
            New Integer()() {New Integer() {0, 1}, New Integer() {0, 2}, New Integer() {0, 3}, New Integer() {0, 4},
            New Integer() {1, 2}, New Integer() {1, 3}, New Integer() {1, 4}, New Integer() {2, 3}, New Integer() {2, 4}, New Integer() {3, 4}},
            New Integer()() {New Integer() {0, 1, 2}, New Integer() {0, 1, 3}, New Integer() {0, 2, 3}, New Integer() {1, 2, 3},
            New Integer() {0, 1, 4}, New Integer() {0, 2, 4}, New Integer() {1, 2, 4},
            New Integer() {0, 3, 4}, New Integer() {1, 3, 4}, New Integer() {2, 3, 4}},
            New Integer()() {New Integer() {0, 1, 2, 3}, New Integer() {0, 1, 2, 4}, New Integer() {0, 1, 3, 4}, New Integer() {0, 2, 3, 4}, New Integer() {1, 2, 3, 4}},
            New Integer()() {New Integer() {0, 1, 2, 3, 4}}
            }

        Dim totalHandScore As Integer = 0

        'Fifteens first
        Dim sum As Integer = 0
        Dim fifteensfound As Integer = 0
        For i = 0 To sets.Length - 1
            For j = 0 To IIf(BestOfFour, sets(i).Length - 2, sets(i).Length - 1) 'skip the set of 5 if BestOfFour
                sum = 0
                For k = 0 To sets(i)(j).Length - 1
                    If Not BestOfFour Or sets(i)(j)(k) <= 3 Then
                        sum += IIf(temphand(sets(i)(j)(k)).FaceVal > 10, 10, temphand(sets(i)(j)(k)).FaceVal)
                    Else
                        'BestOfFour, one of the cards is the fifth card
                        sum = 0
                        Exit For
                    End If
                Next k
                If sum = 15 Then
                    totalHandScore += 2
                    fifteensfound += 1
                End If
            Next j
        Next i
        If fifteensfound > 0 And Not BestOfFour Then PrintMessage("*" &
            System.Enum.GetName(GetType(NumberWords), fifteensfound) &
            " fifteen" & IIf(fifteensfound = 1, "", "s") & " for " & (fifteensfound * 2) & ".")

        'Pairs
        Dim pairsfound As Integer = 0
        For j = 0 To sets(0).Length - 1 'Just the sets that contain two
            If Not BestOfFour Or (sets(0)(j)(0) < 4 And sets(0)(j)(1) < 4) Then
                If temphand(sets(0)(j)(0)).FaceVal = temphand(sets(0)(j)(1)).FaceVal Then

                    totalHandScore += 2
                    pairsfound += 1
                End If
            End If
        Next j
        If pairsfound > 0 And Not BestOfFour Then PrintMessage("*" &
            System.Enum.GetName(GetType(NumberWords), pairsfound) &
            " pair" & IIf(pairsfound = 1, "", "s") & " for " & (pairsfound * 2) & ".")

        'Runs
        temphand = temphand.OrderBy(Function(x) x.FaceVal).ToList() 'Sort
        Dim whichrunfound As Integer = 0
        Dim subrunfound As Boolean = True
        Dim runsfound As Integer = 0
        For i = IIf(BestOfFour, 2, 3) To 1 Step -1 'Iif: again, skip that set of five

            For j = 0 To sets(i).Length - 1
                subrunfound = True
                For k = 0 To sets(i)(j).Length - 2
                    If Not BestOfFour Or (sets(i)(j)(k) < 4 And sets(i)(j)(k + 1) < 4) Then
                        If (temphand(sets(i)(j)(k + 1)).FaceVal - temphand(sets(i)(j)(k)).FaceVal <> 1) Then
                            subrunfound = False
                            Exit For
                        End If
                    Else
                        subrunfound = False
                        Exit For
                    End If
                Next k
                If subrunfound Then
                    'For l = 0 To i + 1
                    '    PrintMessage(temphand(sets(i)(j)(l)).ToString())
                    'Next l
                    runsfound += 1
                    totalHandScore += i + 2
                    whichrunfound = i + 2
                End If
            Next j
            If whichrunfound > 0 Then Exit For
        Next i
        If whichrunfound > 0 And Not BestOfFour Then PrintMessage("*" & System.Enum.GetName(GetType(NumberWords), runsfound) &
            " run" & IIf(runsfound = 1, "", "s") & " of " & System.Enum.GetName(GetType(NumberWords), whichrunfound).ToLower() & " for " &
            (runsfound * whichrunfound) & ".")

        'Flush
        If IsThisTheCrib And Not BestOfFour Then
            'Only five is valid
            If WhoseHand(0).Suit = WhoseHand(1).Suit And WhoseHand(1).Suit = WhoseHand(2).Suit And
                    WhoseHand(2).Suit = WhoseHand(3).Suit And WhoseHand(3).Suit = TurnUpCard.Suit Then
                PrintMessage("*A five-card flush for 5.")
                totalHandScore += 5
            End If
        ElseIf Not IsThisTheCrib Then
            'AndAlso to prevent crash on BestOfFour
            If Not BestOfFour AndAlso (WhoseHand(0).Suit = WhoseHand(1).Suit AndAlso WhoseHand(1).Suit = WhoseHand(2).Suit AndAlso
                    WhoseHand(2).Suit = WhoseHand(3).Suit AndAlso WhoseHand(3).Suit = TurnUpCard.Suit) Then
                PrintMessage("*A five-card flush for 5.")
                totalHandScore += 5
            ElseIf WhoseHand(0).Suit = WhoseHand(1).Suit And WhoseHand(1).Suit = WhoseHand(2).Suit And
                    WhoseHand(2).Suit = WhoseHand(3).Suit Then
                If Not BestOfFour Then PrintMessage("*A four-card flush for 4.")
                totalHandScore += 4
            End If
        End If

        'Nobs
        For Each crd In WhoseHand 'Skip the cut card!
            If crd.FaceVal = CardGameFramework.FaceValue.Jack And crd.Suit = TurnUpCard.Suit Then
                If Not BestOfFour Then PrintMessage("*Nobs for 1.")
                totalHandScore += 1
                Exit For
            End If
        Next crd
        Return totalHandScore
    End Function

    Public Sub CPUPlayCard(ByVal PlayerPlayedLastCard As Boolean)
        If oppHand.Count > 0 Then
            Dim noGoodCardsCPU As Boolean = True
            'Make sure at least one card keeps the score under 31
            Dim FoundAFifteen As Integer = -1
            Dim nonfives As New ArrayList
            For i = 0 To oppHand.Count - 1
                If IIf(oppHand(i).FaceVal > 10, 10, oppHand(i).FaceVal) + playAreaScore <= 31 Then
                    noGoodCardsCPU = False

                    'Medium/Hard: Look for fifteen, thirty-one, or pair
                    'Beginner: Intentionally leave five or twenty-one
                    If CurrentDifficulty = Difficulty.Beginner Then
                        If IIf(oppHand(i).FaceVal > 10, 10, oppHand(i).FaceVal) + playAreaScore = 5 Or
                           IIf(oppHand(i).FaceVal > 10, 10, oppHand(i).FaceVal) + playAreaScore = 21 Then
                            FoundAFifteen = i
                            nonfives.Clear()
                            Exit For
                        End If
                    ElseIf CurrentDifficulty <> Difficulty.Easy Then
                        If IIf(oppHand(i).FaceVal > 10, 10, oppHand(i).FaceVal) + playAreaScore = 15 Or
                           IIf(oppHand(i).FaceVal > 10, 10, oppHand(i).FaceVal) + playAreaScore = 31 Then
                            '15 and 31
                            FoundAFifteen = i
                            nonfives.Clear()
                            Exit For
                        ElseIf (cardsInPlay.Count > 0 AndAlso oppHand(i).FaceVal = cardsInPlay.Last.FaceVal) Then
                            'Pair
                            FoundAFifteen = i
                            nonfives.Clear()
                            Exit For
                        Else
                            'Avoid leaving 5 or 21
                            If IIf(oppHand(i).FaceVal > 10, 10, oppHand(i).FaceVal) + playAreaScore <> 5 And
                                IIf(oppHand(i).FaceVal > 10, 10, oppHand(i).FaceVal) + playAreaScore <> 21 Then
                                nonfives.Add(i)
                            End If

                        End If
                    Else
                        Exit For
                    End If

                End If
            Next
            If noGoodCardsCPU Then
                If Not CPUGoesNext And AlreadyGoOnce Then
                    ResetPegging()
                Else
                    If myHand.Count > 0 Then
                        PrintMessage("The CPU says 'go.'")
                        PlayVoice("Go", True)
                    End If
                    AlreadyGoOnce = True
                    'CPUGoesNext = False
                End If
            Else
                Update()
                If Not SoundOn Then Wait()


                Dim posat As Integer

                If FoundAFifteen > -1 Then
                    'Prioritize fifteens and thirty-ones
                    posat = FoundAFifteen
                Else
                    If nonfives.Count > 0 Then
                        posat = ran.Next(0, nonfives.Count)
                    Else
                        posat = ran.Next(0, oppHand.Count)
                    End If
                    'Now look for pairs (and runs later)
                    Dim nfp As Integer
                    If nonfives.Count > 0 Then 'To prevent nonfives(posat) from evaluating
                        nfp = nonfives(posat)
                    Else
                        nfp = posat
                    End If
                    While playAreaScore + IIf(oppHand(nfp).FaceVal > 10, 10, oppHand(nfp).FaceVal) > 31
                        If nonfives.Count > 0 Then
                            posat = ran.Next(0, nonfives.Count)
                        Else
                            posat = ran.Next(0, oppHand.Count)
                        End If
                        If nonfives.Count > 0 Then 'To prevent nonfives(posat) from evaluating
                            nfp = nonfives(posat)
                        Else
                            nfp = posat
                        End If
                    End While
                End If

                If FoundAFifteen < 0 And nonfives.Count > 0 Then
                    playAreaScore += IIf(oppHand(nonfives(posat)).FaceVal > 10, 10, oppHand(nonfives(posat)).FaceVal)
                    cardsInPlay.Add(oppHand(nonfives(posat)))
                    If PlayByPlay Then PrintMessage("The CPU plays the " & oppHand(nonfives(posat)).ToString() & ".")
                    oppHand.RemoveAt(nonfives(posat))
                Else
                    playAreaScore += IIf(oppHand(posat).FaceVal > 10, 10, oppHand(posat).FaceVal)
                    cardsInPlay.Add(oppHand(posat))
                    If PlayByPlay Then PrintMessage("The CPU plays the " & oppHand(posat).ToString() & ".")
                    oppHand.RemoveAt(posat)
                End If


                PrintCPUCards(AllowSeeingCPUCards)
                PrintCardsInPlay()
                Dim cpuadv As Integer = CheckPlayAdvancedScoring()
                If cpuadv > 0 Then
                    PrintMessage("The CPU pegs " & cpuadv & ".")
                    oppScore += cpuadv
                    If PrintScore() Then Exit Sub
                End If
                PlayerPlayedLastCard = False
                PrintPlayAreaScore()
            End If


            If playAreaScore = 31 Then
                btnNewRound.Text = "Continue"
                btnNewRound.Enabled = True
                oppScore += 2

                If myHand.Count = 0 And oppHand.Count = 0 Then
                    CurrentState = GameState.Counting1
                    PrintMessage("The CPU pegs 2 for thirty-one. All cards have been played.")
                Else
                    PrintMessage("The CPU pegs 2 for thirty-one.")
                    AlreadyGoOnce = True
                    CPUGoesNext = False
                End If
                If PrintScore() Then Exit Sub
            Else
                If myHand.Count > 0 Then
                    Dim noGoodCardsMe As Boolean = True
                    'Make sure at least one card keeps the score under 31 (For you now)
                    For Each myc In myHand
                        If IIf(myc.FaceVal > 10, 10, myc.FaceVal) + playAreaScore <= 31 Then
                            noGoodCardsMe = False
                            Exit For
                        End If
                    Next
                    If noGoodCardsMe And noGoodCardsCPU Then
                        btnNewRound.Text = "Continue"
                        btnNewRound.Enabled = True
                        PrintMessage("You peg 1 for go.")
                        CPUGoesNext = True
                        myScore += 1
                        PlayVoice("Go for one.", True)
                        If PrintScore() Then Exit Sub
                    ElseIf noGoodCardsMe Then
                        noGoodCardsCPU = True
                        'Make sure at least one card keeps the score under 31
                        For Each cpuc In oppHand
                            If IIf(cpuc.FaceVal > 10, 10, cpuc.FaceVal) + playAreaScore <= 31 Then
                                noGoodCardsCPU = False
                                Exit For
                            End If
                        Next
                        If noGoodCardsCPU Then
                            If PlayerPlayedLastCard Then
                                PrintMessage("You peg 1 for go.")
                                CPUGoesNext = True
                                myScore += 1
                                PlayVoice("Go for one.", True)
                                If PrintScore() Then Exit Sub
                            Else
                                If Not AlreadyGoOnce Then PrintMessage("You say 'go.'")
                                PrintMessage("The CPU pegs 1 for go.")
                                CPUGoesNext = False
                                oppScore += 1
                                PlayVoice("Go for one.", True)
                                If PrintScore() Then Exit Sub
                            End If
                            btnNewRound.Text = "Continue"
                            btnNewRound.Enabled = True
                            AlreadyGoOnce = True
                        Else
                            btnNewRound.Text = "Go"
                            btnNewRound.Enabled = True
                            PrintMessage("You cannot play. You must say 'go.'")
                            CPUGoesNext = True
                            AlreadyGoOnce = False
                        End If

                    Else
                        AlreadyGoOnce = False
                    End If

                Else
                    'You have no cards left, but the CPU does.
                    If noGoodCardsCPU Then
                        btnNewRound.Text = "Continue"
                        btnNewRound.Enabled = True
                        If CPUGoesNext Then
                            PrintMessage("The CPU says 'go.'")
                            PrintMessage("You peg 1 for go.")
                            CPUGoesNext = True
                            myScore += 1
                            PlayVoice("Go", True)
                            PlayVoice("Go for one.", True)
                            If PrintScore() Then Exit Sub
                        Else
                            PrintMessage("The CPU pegs 1 for go.")
                            CPUGoesNext = False
                            oppScore += 1
                            PlayVoice("Go for one.", True)
                            If PrintScore() Then Exit Sub
                        End If

                        AlreadyGoOnce = True
                    Else
                        CPUPlayCard(False)
                        If oppScore >= PlayTo Or myScore >= PlayTo Then Exit Sub
                    End If

                End If
            End If
        ElseIf myHand.Count = 0 Then
            'Both hands empty
            CurrentState = GameState.Counting1
            btnNewRound.Text = "Continue"
            btnNewRound.Enabled = True

            If PlayerPlayedLastCard Then
                PrintMessage("You peg 1 for the last card.")
                myScore += 1
                PlayVoice("Last card for one.", True)
                If PrintScore() Then Exit Sub
            Else
                PrintMessage("The CPU pegs 1 for the last card.")
                oppScore += 1
                PlayVoice("Last card for one.", True)
                If PrintScore() Then Exit Sub
            End If

        Else
            Dim noGoodCardsMe As Boolean = True
            'Make sure at least one card keeps the score under 31 (For you now)
            For Each myc In myHand
                If IIf(myc.FaceVal > 10, 10, myc.FaceVal) + playAreaScore <= 31 Then
                    noGoodCardsMe = False
                    Exit For
                End If
            Next
            If noGoodCardsMe Then
                btnNewRound.Text = "Continue"
                btnNewRound.Enabled = True
                PrintMessage("You peg 1 for go.")
                CPUGoesNext = False
                AlreadyGoOnce = False
                myScore += 1
                PlayVoice("Go for one.", True)
                If PrintScore() Then Exit Sub
            End If
        End If
    End Sub

    Private Sub CPUCard_Click(sender As Object, e As EventArgs) Handles _
    CPUCard1.Click, CPUCard2.Click, CPUCard3.Click, CPUCard4.Click, CPUCard5.Click, CPUCard6.Click, TurnUpCardImage.Click,
    CribCard1.Click, CribCard2.Click, CribCard3.Click, CribCard4.Click
        If CurrentState = GameState.PuttingInCrib Or CurrentState = GameState.Pegging Then
            MsgBox("Those are not your cards.", MsgBoxStyle.Exclamation)
        End If
    End Sub

    Public Sub ResetPegging()
        AlreadyGoOnce = False
        cardsInPlay.Clear()
        PrintCardsInPlay()
        playAreaScore = 0
        PrintPlayAreaScore()
    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click
        OptionsDialog.ShowDialog()
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        If btnNewRound.Enabled Then

            If btnNewRound.BackColor = Color.LightGray Then
                btnNewRound.BackColor = Color.DarkGray
            Else
                btnNewRound.BackColor = Color.LightGray
            End If
        Else
            btnNewRound.BackColor = Color.LightGray
        End If
    End Sub

End Class
