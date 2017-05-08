Namespace CardGameFramework

    ''' <summary>
    ''' Card suit values
    ''' </summary>
    Public Enum Suit
        Diamonds
        Spades
        Clubs
        Hearts
    End Enum

    ''' <summary>
    ''' Card face values
    ''' </summary>
    Public Enum FaceValue
        Ace = 1
        Two = 2
        Three = 3
        Four = 4
        Five = 5
        Six = 6
        Seven = 7
        Eight = 8
        Nine = 9
        Ten = 10
        Jack = 11
        Queen = 12
        King = 13
    End Enum

    Public Class Card
        ' Objects for card information
        Private _suitProp As Suit
        Private _faceValProp As FaceValue
        Private _isCardUpProp As Boolean

        Public Property Suit() As Suit
            Get
                Return _suitProp
            End Get
            Set(ByVal value As Suit)
                _suitProp = value
            End Set
        End Property

        Public Property FaceVal() As FaceValue
            Get
                Return _faceValProp
            End Get
            Set(ByVal value As FaceValue)
                _faceValProp = value
            End Set
        End Property

        Public Property IsCardUp() As Boolean
            Get
                Return _isCardUpProp
            End Get
            Set(ByVal value As Boolean)
                _isCardUpProp = value
            End Set
        End Property

        ''' <summary>
        ''' Constructor for a new card. 
        ''' </summary>
        ''' <param name="suitProp"></param>
        ''' <param name="faceValProp"></param>
        ''' <param name="isCardUpProp"></param>
        ''' <remarks>Assign the card a suit, face value, and if the card is facing up or down</remarks>
        Public Sub New(ByVal suitProp As Suit, ByVal faceValProp As FaceValue, ByVal isCardUpProp As Boolean)
            Me._suitProp = suitProp
            Me._faceValProp = faceValProp
            Me._isCardUpProp = isCardUpProp
        End Sub

        ''' <summary>
        ''' Return the card as a string (i.e. "The Ace of Spades")
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Overloads Overrides Function ToString() As String
            Return _faceValProp.ToString & " of " & _suitProp.ToString
        End Function
    End Class
End Namespace