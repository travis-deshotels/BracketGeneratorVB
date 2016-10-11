Public Enum ce_BracketType
    SingleE = 1
    DoubleEWinner = 2
    TwoOutOfThree = 3
    TrueDouble = 4
    DoubleELoser = 5
End Enum

Public Class clsBracket
    Private strDivisionName As String
    Private e_Type As ce_BracketType
    Friend a_objMatches As ArrayList
    Private intBracketOrder As Integer
    Private strExPlayer1 As String
    Private strExPlayer2 As String
    Private strExPlayer3 As String
    Private intExMatch1 As Integer
    Private intExMatch2 As Integer

    'Private intPlayerCount As Integer

    Public Sub New()
        a_objMatches = New ArrayList
        'intPlayerCount = 0
    End Sub

    Property DivisionName() As String
        Get
            Return strDivisionName
        End Get
        Set(ByVal value As String)
            strDivisionName = value
        End Set
    End Property

    Property BracketType() As ce_BracketType
        Get
            Return e_Type
        End Get
        Set(ByVal value As ce_BracketType)
            e_Type = value
        End Set
    End Property

    Property BracketOrder() As Integer
        Get
            Return intBracketOrder
        End Get
        Set(ByVal value As Integer)
            intBracketOrder = value
        End Set
    End Property

    Property ExPlayer1() As String
        Get
            Return strExPlayer1
        End Get
        Set(ByVal value As String)
            strExPlayer1 = value
        End Set
    End Property

    Property ExPlayer2() As String
        Get
            Return strExPlayer2
        End Get
        Set(ByVal value As String)
            strExPlayer2 = value
        End Set
    End Property

    Property ExPlayer3() As String
        Get
            Return strExPlayer3
        End Get
        Set(ByVal value As String)
            strExPlayer3 = value
        End Set
    End Property

    Property ExMatch1() As Integer
        Get
            Return intExMatch1
        End Get
        Set(ByVal value As Integer)
            intExMatch1 = value
        End Set
    End Property

    Property ExMatch2() As Integer
        Get
            Return intExMatch2
        End Get
        Set(ByVal value As Integer)
            intExMatch2 = value
        End Set
    End Property

    'Property PlayerCount() As Integer
    '    Get
    '        Return intPlayerCount
    '    End Get
    '    Set(ByVal value As Integer)
    '        intPlayerCount = value
    '    End Set
    'End Property

End Class
