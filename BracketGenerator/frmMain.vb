Imports System.IO

Public Class frmMain
    Private objBracketManager As clsBracketManager

    Private Sub frmMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        objBracketManager = New clsBracketManager
        DumpMatches()
        ' Call Test()
    End Sub

    Private Sub btnAddBracket_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddBracket.Click
        'Call Test()
        Call CreateBracket()
    End Sub

    Private Sub Test()
        'Dim objMatch As New clsMatch
        txtDivisionName.Text = "Senior Men's 178"
        txtNames.Text = txtNames.Text & "Joey" & vbNewLine
        txtNames.Text = txtNames.Text & "Matt" & vbNewLine
        txtNames.Text = txtNames.Text & "Sonnier" & vbNewLine
        txtNames.Text = txtNames.Text & "Eric" & vbNewLine
        txtNames.Text = txtNames.Text & "Cheramie" & vbNewLine
        txtNames.Text = txtNames.Text & "Davis" & vbNewLine
        txtNames.Text = txtNames.Text & "Sam" & vbNewLine
        txtNames.Text = txtNames.Text & "Nick"
        Call CreateBracket()

        txtDivisionName.Text = "Senior Women's 120"
        txtNames.Text = txtNames.Text & "Kerri" & vbNewLine
        txtNames.Text = txtNames.Text & "Alyssa" & vbNewLine
        txtNames.Text = txtNames.Text & "Lisa" & vbNewLine
        txtNames.Text = txtNames.Text & "Camille" & vbNewLine
        txtNames.Text = txtNames.Text & "Christina" & vbNewLine
        txtNames.Text = txtNames.Text & "Brittney"
        Call CreateBracket()

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

        'Call DumpMatches()
    End Sub

    Private Sub CreateBracket()
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

        Call objBracketManager.AddBracket(txtDivisionName.Text)
        For intPlayerIndex = 0 To intBracketSize - 1 Step 2
            objBracketManager.AddMatch(CStr(objPlayersList(intPlayerIndex)), CStr(objPlayersList(intPlayerIndex + 1)))
            intCurrentMatchNum += 1
        Next
        'Insert placeholders
        For intMatchIndex = intCurrentMatchNum To intMatchCount - 1 Step 1
            objBracketManager.AddMatch(String.Empty, String.Empty)
        Next

        lstBrackets.Items.Add(txtDivisionName.Text & ": " & CStr(intPlayersEntered))

        a_strNames = Nothing
        objPlayersList = Nothing
        txtNames.Text = String.Empty
        txtDivisionName.Text = String.Empty

    End Sub

    Private Sub AddByeSpots(ByRef ra_strInputList() As String, ByRef r_objOutputList As ArrayList, ByVal v_intByeCount As Integer)
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
        Dim objMatch As New clsMatch

        'txtDebug.Text = String.Empty

        Call objBracketManager.FirstBracket()
        Do Until objBracketManager.blnLastBracket
            '    'Print Bracket Name
            '    txtDebug.Text = txtDebug.Text & objBracketManager.objGetCurrentBracket.DivisionName & ":" & vbNewLine
            Call objBracketManager.FirstMatch()
            Do Until objBracketManager.blnLastMatch
                objMatch = objBracketManager.objGetCurrentMatch()
                'Print Match
                '        txtDebug.Text = txtDebug.Text & objMatch.bracketOrder & vbNewLine
                '        txtDebug.Text = txtDebug.Text & vbTab & objMatch.player1 & vbNewLine
                '        txtDebug.Text = txtDebug.Text & vbTab & objMatch.player2 & vbNewLine

                Call objBracketManager.NextMatch()
            Loop
            Call objBracketManager.NextBracket()
        Loop

        Dim FileWriter As StreamWriter
        FileWriter = New StreamWriter("C:\Users\me\Documents\test.txt")

        FileWriter.Write("hello world")
        FileWriter.WriteLine()
        FileWriter.Close()

    End Sub

    Private Sub NumberMatches()
        'Dim objMatch As New clsMatch
        ' Dim intCount As Integer
        Dim intMatchNum As Integer = 0
        'Dim blnFlag As Boolean = False

        Call objBracketManager.MarkBracketsReady()

        While Not objBracketManager.blnBracketsFinished
            Call objBracketManager.FirstBracket()
            Do Until objBracketManager.blnLastBracket
                'Don't number matches that are byes
                If objBracketManager.objGetCurrentMatch.player2 <> "bye" Then
                    intMatchNum += 1
                    txtDebug.Text = txtDebug.Text & "Match " & intMatchNum & ": "
                End If
                txtDebug.Text = txtDebug.Text & objBracketManager.objGetCurrentMatch.player1 & " - "
                txtDebug.Text = txtDebug.Text & objBracketManager.objGetCurrentMatch.player2 & vbNewLine
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

    Private Sub btnDump_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDump.Click
        'Call objBracketManager.MarkBracketsReady()
        'Call DumpMatches()
        Call NumberMatches()
    End Sub
End Class
