Imports System.Collections.Generic

Namespace CardGameFramework
    Public Class Deck
        'Creates a list of cards
        Protected _cards As New List(Of Card)
        'Returns the card at the given position
        Default Public ReadOnly Property _position(ByVal position As Integer) As Card
            Get
                Return _cards(position)
            End Get
        End Property

        ''' <summary>
        ''' One complete deck with every face value and suit
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub New()
            For Each suit As Suit In System.Enum.GetValues(GetType(Suit))
                For Each faceVal As FaceValue In System.Enum.GetValues(GetType(FaceValue))
                    _cards.Add(New Card(suit, faceVal, True))
                Next
            Next
        End Sub

        ''' <summary>
        ''' Draws one card and removes it from the deck
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function Draw() As Card
            Dim card As Card = _cards(0)
            _cards.RemoveAt(0)
            Return card
        End Function

        ''' <summary>
        ''' Shuffles the cards in the deck
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub Shuffle()
            Dim random As New Random
            Dim i As Integer = 0
            While i < _cards.Count
                Dim index1 As Integer = i
                Dim index2 As Integer = random.Next(_cards.Count)
                SwapCard(index1, index2)
                i = i + 1
            End While
        End Sub

        ''' <summary>
        ''' Swaps the placement of two cards
        ''' </summary>
        ''' <param name="index1"></param>
        ''' <param name="index2"></param>
        ''' <remarks></remarks>
        Private Sub SwapCard(ByVal index1 As Integer, ByVal index2 As Integer)
            Dim card As Card = _cards(index1)
            _cards(index1) = _cards(index2)
            _cards(index2) = card
        End Sub
    End Class
End Namespace