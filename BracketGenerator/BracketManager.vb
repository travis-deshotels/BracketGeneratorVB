Public Class clsBracketManager
    Private a_objBrackets As ArrayList
    Private a_objBracketsDone As ArrayList
    Private intCurrentBracket As Integer
    Private a_intMatchCount As ArrayList
    Private intMatchCounter As Integer      'used to assign internal match numbers

    Public Sub New()
        a_objBrackets = New ArrayList
        a_objBracketsDone = New ArrayList
        a_intMatchCount = New ArrayList
        intCurrentBracket = -1
    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    '**Bracket Operations**
    Public Sub AddBracket(ByVal strBracketName As String)
        'Create Bracket
        Dim objBracket As New clsBracket
        objBracket.DivisionName = strBracketName
        objBracket.PlayerCount = 0
        objBracket.a_objMatches = New ArrayList

        'Add Bracket to list
        a_objBrackets.Add(objBracket)
        intCurrentBracket += 1

        'Add a match counter for each bracket
        Call a_intMatchCount.Add(-1)
        intMatchCounter = 0
        objBracket = Nothing
    End Sub

    Public Sub DeleteBracket(ByVal v_strDevisionName As String)

    End Sub

    Public Function blnBracketsFinished() As Boolean
        Return a_objBrackets.Count = 0
    End Function

    Public Sub MarkBracketFinished()
        '
        'Marks the current bracket as finished
        'Moves currentbracket backward
        '
        Call a_objBracketsDone.Insert(intCurrentBracket, objGetCurrentBracket)
        'If blnLastBracket() Then
        Call a_objBrackets.RemoveAt(intCurrentBracket)
        intCurrentBracket -= 1
        'End If
        'Call a_objBrackets.RemoveAt(intCurrentBracket)
    End Sub

    Public Sub MarkBracketsReady()
        '
        'Marks all brackets as unprocessed
        'Initializes all match pointers
        '
        For Count As Integer = 0 To a_objBracketsDone.Count - 1
            Call a_objBrackets.Add(a_objBracketsDone(Count))
        Next

        Call a_objBracketsDone.Clear()

        'set each bracket to first match
        Call FirstBracket()
        Do Until blnLastBracket()
            Call FirstMatch()
            Call NextBracket()
        Loop

        Call FirstBracket()

        MsgBox("Brackets ready: " & a_objBrackets.Count.ToString & vbNewLine & _
               "Brackets done: " & a_objBracketsDone.Count.ToString)
    End Sub

    Public Sub NextBracket()
        intCurrentBracket += 1
    End Sub

    Public Sub FirstBracket()
        intCurrentBracket = 0
    End Sub

    Public Function blnLastBracket() As Boolean
        Return intCurrentBracket = a_objBrackets.Count
    End Function

    Public Function objGetCurrentBracket() As clsBracket
        Return CType(a_objBrackets(intCurrentBracket), clsBracket)
    End Function

    '**Match Operations**
    Public Sub AddMatch(ByVal v_strPlayer1 As String, _
                        ByVal v_strPlayer2 As String)
        'Create Match
        Dim objMatch As New clsMatch
        objMatch.player1 = v_strPlayer1
        objMatch.player2 = v_strPlayer2
        objMatch.bracketOrder = intMatchCounter

        'Add Match to list
        objGetCurrentBracket.a_objMatches.Add(objMatch)

        intMatchCounter += 1

        objMatch = Nothing
    End Sub

    Public Sub NextMatch()
        a_intMatchCount(intCurrentBracket) = CInt(a_intMatchCount(intCurrentBracket)) + 1
    End Sub

    Public Sub FirstMatch()
        a_intMatchCount(intCurrentBracket) = 0
    End Sub

    Public Function blnLastMatch() As Boolean
        Return CInt(a_intMatchCount(intCurrentBracket)) = objGetCurrentBracket.a_objMatches.Count - 1
    End Function

    Public Function objGetCurrentMatch() As clsMatch
        Return CType(objGetCurrentBracket.a_objMatches(CInt(a_intMatchCount(intCurrentBracket))), clsMatch)
    End Function

End Class
