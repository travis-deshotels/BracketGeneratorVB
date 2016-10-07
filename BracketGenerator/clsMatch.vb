Public Class clsMatch
    Private strPlayer1 As String
    Private strPlayer2 As String
    Private intBracketOrder As Integer
    Private intMatchNumber As Integer

    Property player1() As String
        Get
            Return strPlayer1
        End Get
        Set(ByVal value As String)
            strPlayer1 = value
        End Set
    End Property

    Property player2() As String
        Get
            Return strPlayer2
        End Get
        Set(ByVal value As String)
            strPlayer2 = value
        End Set
    End Property

    Property bracketOrder() As Integer
        Get
            Return intBracketOrder
        End Get
        Set(ByVal value As Integer)
            intBracketOrder = value
        End Set
    End Property

    Property matchNumber() As Integer
        Get
            Return intMatchNumber
        End Get
        Set(ByVal value As Integer)
            intMatchNumber = value
        End Set
    End Property

End Class
