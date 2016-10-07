Public Class clsBracket
    Private strDivisionName As String
    Private intPlayerCount As Integer
    Public a_objMatches As ArrayList

    Public Sub New()
        a_objMatches = New ArrayList
        intPlayerCount = 0
    End Sub

    Property DivisionName() As String
        Get
            Return strDivisionName
        End Get
        Set(ByVal value As String)
            strDivisionName = value
        End Set
    End Property

    Property PlayerCount() As Integer
        Get
            Return intPlayerCount
        End Get
        Set(ByVal value As Integer)
            intPlayerCount = value
        End Set
    End Property

End Class
