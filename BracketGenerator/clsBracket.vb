Public Enum ce_BracketType
    SingleE = 0
    DoubleEWinner = 1
    TwoOutOfThree = 2
    TrueDouble = 3
    DoubleELoser = 4
End Enum

Public Class clsBracket
    Private strDivisionName As String
    Private e_Type As ce_BracketType
    Friend a_objMatches As ArrayList
    Private intBracketOrder As Integer
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

    'Property PlayerCount() As Integer
    '    Get
    '        Return intPlayerCount
    '    End Get
    '    Set(ByVal value As Integer)
    '        intPlayerCount = value
    '    End Set
    'End Property

End Class
