Public Class clsLevelIterator
    Private intLvlCount As Integer
    Private intLvlLevel As Integer
    Private intLvLBracketSize As Integer
    Private objBracket As clsBracket

    Public Sub New(ByVal v_objBracket As clsBracket)
        objBracket = v_objBracket
    End Sub

    Public Sub LevelStart()
        intLvLBracketSize = objBracket.a_objMatches.Count + 1
        intLvlCount = 0
        intLvlLevel = CInt(intLvLBracketSize / 2)
    End Sub

    Public Function blnLevelIsDone() As Boolean
        intLvlCount += 1
        If intLvlCount = intLvlLevel Then
            intLvlLevel = CInt(intLvlLevel / 2)
            intLvlCount = 0
            Return True
        Else
            Return False
        End If
    End Function

End Class
