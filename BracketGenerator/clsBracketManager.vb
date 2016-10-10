Public Class clsBracketManager
    Private a_objBrackets As ArrayList
    Private a_objBracketsDone As ArrayList
    Private intCurrentBracket As Integer
    Private a_intMatchCount As ArrayList
    Private intMatchCounter As Integer      'used to assign internal match numbers

    'These are used for processing a bracket by level
    Private intLvlBracketSize As Integer
    Private intLvlCount As Integer
    Private intLvlLevel As Integer

    Public Sub New()
        a_objBrackets = New ArrayList
        a_objBracketsDone = New ArrayList
        a_intMatchCount = New ArrayList
        intCurrentBracket = -1
    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub
    '
    '  **Bracket Operations**
    '
    Public Sub AddBracket(ByVal strBracketName As String, ByVal e_Type As ce_BracketType)
        'Create Bracket
        Dim objBracket As New clsBracket
        objBracket.DivisionName = strBracketName
        objBracket.a_objMatches = New ArrayList
        objBracket.BracketType = e_Type
        objBracket.BracketOrder = a_objBrackets.Count

        'Add Bracket to list
        a_objBrackets.Add(objBracket)
        intCurrentBracket += 1

        'Add a match counter for each bracket
        Call a_intMatchCount.Add(-1)
        intMatchCounter = 0
        objBracket = Nothing
    End Sub

    'Public Sub AddBracket(ByVal v_objBracket As clsBracket)

    '    'Add Bracket to list
    '    a_objBrackets.Add(v_objBracket)
    '    intCurrentBracket += 1

    '    'Add a match counter for each bracket
    '    Call a_intMatchCount.Add(-1)
    'End Sub

    Public Sub DeleteBracket(ByVal v_strDivisionName As String)

    End Sub

    Public Function blnBracketsFinished() As Boolean
        '
        'Returns true when the all bracket have been processed
        '
        Return a_objBrackets.Count = 0
    End Function

    Public Sub MarkBracketFinished()
        '
        'Marks the current bracket as finished
        'Adjusts current match pointer
        'Moves currentbracket backward
        '
        Call a_objBracketsDone.Add(objGetCurrentBracket)
        Call a_objBrackets.RemoveAt(intCurrentBracket)
        Call a_intMatchCount.RemoveAt(intCurrentBracket)
        intCurrentBracket -= 1

    End Sub

    Public Sub MarkBracketsReady()
        '
        'Marks all brackets as unprocessed
        'Initializes all match pointers
        '
        If a_objBracketsDone.Count > 0 Then
            Call SortFinishedBrackets()

            For Count As Integer = 0 To a_objBracketsDone.Count - 1
                Call a_objBrackets.Add(a_objBracketsDone(Count))
                Call a_intMatchCount.Add(-1)
            Next

            Call a_objBracketsDone.Clear()
        End If

        'set each bracket to first match
        Call FirstBracket()
        Do Until blnLastBracket()
            Call FirstMatch()
            Call NextBracket()
        Loop

        Call FirstBracket()
    End Sub
    '
    '  **Bracket iterator implementation**
    '
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
    '
    '  **Match Operations**
    '
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
        Return CInt(a_intMatchCount(intCurrentBracket)) = objGetCurrentBracket.a_objMatches.Count
    End Function

    Public Function objGetCurrentMatch() As clsMatch
        Return CType(objGetCurrentBracket.a_objMatches(CInt(a_intMatchCount(intCurrentBracket))), clsMatch)
    End Function

    '
    '  **Other operations
    '
    Public Sub intGetMatchNumberBye(ByRef r_intMatchNumber As Integer, ByRef r_Color As ce_MatchColor)
        '
        'Returns the match number for a 1st round bye player
        '
        Dim intIndex As Integer
        Dim intMatchCount As Integer = objGetCurrentBracket.a_objMatches.Count
        Dim intCurrentMatchIndex As Integer = CInt(a_intMatchCount(intCurrentBracket))
        Dim intMatchNumber As Integer = CInt(intMatchCount / 2)         'the return value

        For intIndex = 0 To intMatchCount Step 2
            If intIndex = intCurrentMatchIndex Then
                r_intMatchNumber = CType(objGetCurrentBracket.a_objMatches(intMatchNumber), clsMatch).matchNumber
                r_Color = ce_MatchColor.e_Blue
            ElseIf (intIndex + 1) = intCurrentMatchIndex Then
                r_intMatchNumber = CType(objGetCurrentBracket.a_objMatches(intMatchNumber), clsMatch).matchNumber
                r_Color = ce_MatchColor.e_White
            Else
                intMatchNumber += 1
            End If
        Next intIndex
    End Sub

    Private Sub SortFinishedBrackets()
        Dim intX As Integer
        Dim intY As Integer
        Dim intMin As Integer
        Dim intMinIndex As Integer
        Dim objTemp As clsBracket

        For intY = 0 To a_objBracketsDone.Count - 1
            intMinIndex = intY
            intMin = CType(a_objBracketsDone.Item(intY), clsBracket).BracketOrder
            For intX = intY + 1 To a_objBracketsDone.Count - 1
                If CType(a_objBracketsDone.Item(intX), clsBracket).BracketOrder < intMin Then
                    intMinIndex = intX
                    intMin = CType(a_objBracketsDone.Item(intX), clsBracket).BracketOrder
                End If
            Next
            objTemp = CType(a_objBracketsDone.Item(intY), clsBracket)
            a_objBracketsDone.Item(intY) = CType(a_objBracketsDone.Item(intMinIndex), clsBracket)
            a_objBracketsDone.Item(intMinIndex) = objTemp
            objTemp = Nothing
        Next

    End Sub

    Public Function intGetTotalMatchCount() As Integer
        Dim intTotal As Integer = 0

        For Each objBracket As clsBracket In a_objBrackets
            intTotal += objBracket.a_objMatches.Count
        Next

        Return intTotal
    End Function

    Public Function intGetCurrentBracketSize() As Integer
        Return objGetCurrentBracket.a_objMatches.Count + 1
    End Function

    Public Sub TestSort()
        Dim b As clsBracket

        b = New clsBracket
        b.BracketOrder = 8
        a_objBracketsDone.Add(b)

        b = New clsBracket
        b.BracketOrder = 5
        a_objBracketsDone.Add(b)

        b = New clsBracket
        b.BracketOrder = 1
        a_objBracketsDone.Add(b)

        b = New clsBracket
        b.BracketOrder = 0
        a_objBracketsDone.Add(b)

        b = New clsBracket
        b.BracketOrder = 2
        a_objBracketsDone.Add(b)

        b = New clsBracket
        b.BracketOrder = 3
        a_objBracketsDone.Add(b)

        b = New clsBracket
        b.BracketOrder = 6
        a_objBracketsDone.Add(b)

        b = New clsBracket
        b.BracketOrder = 7
        a_objBracketsDone.Add(b)

        b = New clsBracket
        b.BracketOrder = 4
        a_objBracketsDone.Add(b)

        Call SortFinishedBrackets()
    End Sub

    '
    '  Used for processing brackets by level
    '
    Public Sub LevelStart()
        intLvlBracketSize = objGetCurrentBracket.a_objMatches.Count + 1
        intLvlCount = 0
        intLvlLevel = CInt(intLvlBracketSize / 2)
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
