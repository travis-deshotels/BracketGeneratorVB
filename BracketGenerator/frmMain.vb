Imports System.IO

Public Class frmMain
    Private objBracketManager As clsBracketManager

    Private Sub frmMain_Load(ByVal sender As System.Object, _
                             ByVal e As System.EventArgs) _
                             Handles MyBase.Load
        objBracketManager = New clsBracketManager
        lstBracketType.Items.Add("Single Elimation")
        lstBracketType.Items.Add("Double Elimation")
        lstBracketType.SelectedIndex = ce_BracketType.SingleE
        'DumpMatches()
        'Call Test()
    End Sub

    Private Sub btnAddBracket_Click(ByVal sender As System.Object, _
                                    ByVal e As System.EventArgs) _
                                    Handles btnAddBracket.Click
        If lstBracketType.SelectedIndex = ce_BracketType.SingleE Then
            Call CreateBracket(ce_BracketType.SingleE)
        Else
            Call CreateBracket(ce_BracketType.DoubleEWinner)
        End If
    End Sub

    Private Sub Test()
        txtDivisionName.Text = "Senior Men's 178"
        txtNames.Text = txtNames.Text & "Joey" & vbNewLine
        txtNames.Text = txtNames.Text & "Matt" & vbNewLine
        txtNames.Text = txtNames.Text & "Sonnier" & vbNewLine
        txtNames.Text = txtNames.Text & "Eric" & vbNewLine
        txtNames.Text = txtNames.Text & "Cheramie" & vbNewLine
        txtNames.Text = txtNames.Text & "Davis" & vbNewLine
        txtNames.Text = txtNames.Text & "Sam" & vbNewLine
        txtNames.Text = txtNames.Text & "Nick"
        Call CreateBracket(ce_BracketType.SingleE)

        txtDivisionName.Text = "Senior Women's 120"
        txtNames.Text = txtNames.Text & "Kerri" & vbNewLine
        txtNames.Text = txtNames.Text & "Alyssa" & vbNewLine
        txtNames.Text = txtNames.Text & "Lisa" & vbNewLine
        txtNames.Text = txtNames.Text & "Camille" & vbNewLine
        txtNames.Text = txtNames.Text & "Christina" & vbNewLine
        txtNames.Text = txtNames.Text & "Brittney"
        Call CreateBracket(ce_BracketType.SingleE)

        txtDivisionName.Text = "Novice Men's 178"
        txtNames.Text = txtNames.Text & "Joe V" & vbNewLine
        txtNames.Text = txtNames.Text & "Tom" & vbNewLine
        txtNames.Text = txtNames.Text & "Jason" & vbNewLine
        txtNames.Text = txtNames.Text & "Jacob" & vbNewLine
        txtNames.Text = txtNames.Text & "Summer" & vbNewLine
        txtNames.Text = txtNames.Text & "Zack" & vbNewLine
        txtNames.Text = txtNames.Text & "Brian H" & vbNewLine
        txtNames.Text = txtNames.Text & "Ryan" & vbNewLine
        txtNames.Text = txtNames.Text & "David" & vbNewLine
        txtNames.Text = txtNames.Text & "Sean"
    End Sub

    Private Sub CreateBracket(ByVal ve_Type As ce_BracketType)
        '
        'Create a bracket from the list of names
        'and clear the list
        '
        Dim a_strNames() As String
        Dim objPlayersList As New ArrayList
        Dim intPlayerCount As Integer
        Dim intPlayersEntered As Integer
        Dim intMatchCount As Integer
        Dim intPlayerIndex As Integer
        Dim intMatchIndex As Integer
        Dim intCurrentMatchNum As Integer = 0
        Dim intBracketSize As Integer

        a_strNames = Split(txtNames.Text, vbNewLine)

        'Cancel if less than two names are entered
        If a_strNames.Length < 2 Then
            MsgBox("Enter at least two names to create a bracket")
            Exit Sub
        Else
            intPlayersEntered = a_strNames.Length
        End If

        intBracketSize = intGetBracketSize(intPlayersEntered)

        Call AddByeSpots(a_strNames, _
                         objPlayersList, _
                         intBracketSize - intPlayersEntered)

        intPlayerCount = objPlayersList.Count
        intMatchCount = intPlayerCount - 1

        If ve_Type = ce_BracketType.SingleE Then
            Call objBracketManager.AddBracket(txtDivisionName.Text, ce_BracketType.SingleE)
        ElseIf ve_Type = ce_BracketType.DoubleEWinner Then
            Call objBracketManager.AddBracket(txtDivisionName.Text, ce_BracketType.DoubleEWinner)
        End If

        For intPlayerIndex = 0 To intBracketSize - 1 Step 2
            objBracketManager.AddMatch(CStr(objPlayersList(intPlayerIndex)), _
                                       CStr(objPlayersList(intPlayerIndex + 1)))
            intCurrentMatchNum += 1
        Next
        'Insert placeholders
        For intMatchIndex = intCurrentMatchNum To intMatchCount - 1 Step 1
            objBracketManager.AddMatch(String.Empty, String.Empty)
        Next

        lstBrackets.Items.Add(txtDivisionName.Text & ": " & CStr(intPlayersEntered))

        If ve_Type = ce_BracketType.DoubleEWinner Then
            Call CreateLosersBracket(intPlayersEntered - 1)
        End If

        a_strNames = Nothing
        objPlayersList = Nothing
        txtNames.Text = String.Empty
        txtDivisionName.Text = String.Empty

    End Sub

    Private Sub CreateLosersBracket(ByVal v_intPlayerCount As Integer)
        Dim a_strNames(v_intPlayerCount) As String
        Dim objPlayersList As New ArrayList
        Dim intPlayerCount As Integer
        'Dim intPlayersEntered As Integer
        Dim intMatchCount As Integer
        Dim intPlayerIndex As Integer
        Dim intMatchIndex As Integer
        Dim intCurrentMatchNum As Integer = 0
        Dim intBracketSize As Integer
        Dim intCount As Integer

        For intCount = 0 To v_intPlayerCount
            a_strNames.SetValue(String.Empty, intCount)
        Next

        intBracketSize = intGetBracketSize(v_intPlayerCount)

        Call AddByeSpots(a_strNames, _
                         objPlayersList, _
                         intBracketSize - v_intPlayerCount)

        intPlayerCount = objPlayersList.Count
        intMatchCount = intPlayerCount - 1

        Call objBracketManager.AddBracket(txtDivisionName.Text & "Loser's Bracket", ce_BracketType.DoubleELoser)
        For intPlayerIndex = 0 To intBracketSize - 1 Step 2
            objBracketManager.AddMatch(CStr(objPlayersList(intPlayerIndex)), _
                                       CStr(objPlayersList(intPlayerIndex + 1)))
            intCurrentMatchNum += 1
        Next
        'Insert placeholders
        For intMatchIndex = intCurrentMatchNum To intMatchCount - 1 Step 1
            objBracketManager.AddMatch(String.Empty, String.Empty)
        Next

        a_strNames = Nothing
        objPlayersList = Nothing
    End Sub

    Private Sub AddByeSpots(ByRef ra_strInputList() As String, _
                            ByRef r_objOutputList As ArrayList, _
                            ByVal v_intByeCount As Integer)
        '
        'Returns a new list of players, including byes
        '
        Dim intCount As Integer

        For intCount = 0 To ra_strInputList.Count - 1
            r_objOutputList.Add(ra_strInputList(intCount))
            If v_intByeCount > 0 Then
                r_objOutputList.Add("bye")
                v_intByeCount -= 1
            End If
        Next
    End Sub

    Private Function intGetBracketSize(ByVal intPlayerCount As Integer) As Integer
        '
        'Returns the minimum bracket size needed for a given number of players
        'Player count must be >= 2
        '
        Dim intCount As Integer = 2

        Do While True
            If intCount >= intPlayerCount Then
                Return intCount
            Else
                intCount *= 2
            End If
        Loop
    End Function

    Private Sub DumpMatches()
        '
        'Displays each bracket and its matches
        'Used for debugging
        '
        Call objBracketManager.MarkBracketsReady()

        Dim objMatch As New clsMatch
        Dim FileWriter As StreamWriter
        FileWriter = New StreamWriter("C:\Users\me\Documents\test.txt")

        Call objBracketManager.FirstBracket()
        Do Until objBracketManager.blnLastBracket
            'Print Bracket Name
            FileWriter.Write(objBracketManager.objGetCurrentBracket. _
                             DivisionName & ":")
            FileWriter.WriteLine()                                      'line break
            FileWriter.WriteLine()
            'Call objBracketManager.FirstMatch()
            Call objBracketManager.LevelStart()                         'print by bracket level
            While True
                objMatch = objBracketManager.objGetCurrentMatch()
                'Print Match
                If Not objMatch.matchNumber = 0 Then
                    FileWriter.Write(objMatch.matchNumber & " ")
                End If
                FileWriter.Write(objMatch.player1 & " - ")
                FileWriter.Write(objMatch.player2)
                FileWriter.WriteLine()
                If objBracketManager.blnLevelIsDone Then
                    FileWriter.WriteLine()
                End If

                If objBracketManager.blnLastMatch Then
                    Exit While
                Else
                    Call objBracketManager.NextMatch()
                End If
            End While
            FileWriter.WriteLine()
            Call objBracketManager.NextBracket()
        Loop

        Call objBracketManager.MarkBracketsReady()
        FileWriter.Close()
    End Sub

    Private Sub NumberMatches()
        Dim intMatchNum As Integer = 0

        Call objBracketManager.MarkBracketsReady()

        While Not objBracketManager.blnBracketsFinished
            Call objBracketManager.FirstBracket()
            Do Until objBracketManager.blnLastBracket
                'Don't number matches that are byes
                If objBracketManager.objGetCurrentMatch.player2 <> "bye" Then
                    intMatchNum += 1
                    objBracketManager.objGetCurrentMatch.matchNumber = intMatchNum
                End If
                If objBracketManager.blnLastMatch Then
                    'Done numbering current bracket
                    Call objBracketManager.MarkBracketFinished()
                Else
                    Call objBracketManager.NextMatch()
                End If
                Call objBracketManager.NextBracket()
            Loop
        End While

        Call objBracketManager.MarkBracketsReady()
    End Sub

    Private Sub AddNamesToLosersBrackets()
        Dim int1stMatchNumber As Integer
        Dim int2ndMatchNumber As Integer

        Call objBracketManager.MarkBracketsReady()

        While Not objBracketManager.blnBracketsFinished

            Call objBracketManager.FirstBracket()

            Do Until objBracketManager.blnLastBracket
                'Skip any single elimination brackets
                If objBracketManager.objGetCurrentBracket.BracketType = _
                   ce_BracketType.SingleE Then
                    Call objBracketManager.MarkBracketFinished()
                Else
                    'Find two non-bye matches to store the numbers
                    int1stMatchNumber = objBracketManager.objGetCurrentMatch.matchNumber
                    Call objBracketManager.NextMatch()
                    int2ndMatchNumber = objBracketManager.objGetCurrentMatch.matchNumber
                    Call objBracketManager.NextMatch()

                    Call objBracketManager.NextBracket()

                    'Find two names to change
                    objBracketManager.objGetCurrentMatch.player1 = "L-" & _
                                                                    CStr(int1stMatchNumber)
                    objBracketManager.objGetCurrentMatch.player2 = "L-" & _
                                                                    CStr(int2ndMatchNumber)
                    Call objBracketManager.NextMatch()
                End If
                Call objBracketManager.NextBracket()
            Loop

        End While

        Call objBracketManager.MarkBracketsReady()
    End Sub

    Private Sub btnDump_Click(ByVal sender As System.Object, _
                              ByVal e As System.EventArgs) _
                              Handles btnDump.Click
        Call NumberMatches()
        Call DumpMatches()
    End Sub
End Class
