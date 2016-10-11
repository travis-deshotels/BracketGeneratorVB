Imports System.IO

Public Class frmMain
    Private objBracketManager As clsBracketManager
    Private objPrint As clsPrint
    Const c_AutoIndex As Integer = 0

    Private Sub frmMain_Load(ByVal sender As System.Object, _
                             ByVal e As System.EventArgs) _
                             Handles MyBase.Load

        objBracketManager = New clsBracketManager
        lstBracketType.Items.Add("AUTO")
        lstBracketType.Items.Add("Single Elimation")
        lstBracketType.Items.Add("Modified Double")
        lstBracketType.Items.Add("2 out of 3")
        lstBracketType.Items.Add("True Double")
        lstBracketType.SelectedIndex = c_AutoIndex

        'Call Test()
    End Sub

    Private Sub btnAddBracket_Click(ByVal sender As System.Object, _
                                    ByVal e As System.EventArgs) _
                                    Handles btnAddBracket.Click
        Dim a_strNames() As String
        Dim intPlayerCount As Integer

        a_strNames = Split(txtNames.Text, vbNewLine)
        Call ClearNewLines(a_strNames)
        intPlayerCount = a_strNames.Length

        'Cancel if less than two names are entered
        If intPlayerCount < 2 Then
            MsgBox("Enter at least two names to create a bracket")
        ElseIf txtDivisionName.Text = String.Empty Then
            'or no division name is entered
            MsgBox("Enter a division name")
        Else
            Select Case lstBracketType.SelectedIndex
                Case ce_BracketType.SingleE, ce_BracketType.DoubleEWinner
                    Call CreateBracket(CType(lstBracketType.SelectedIndex, ce_BracketType))
                Case ce_BracketType.TwoOutOfThree
                    Call Create2OutOf3Bracket()
                Case ce_BracketType.TrueDouble
                    Call CreateTrueDoubleBracket()
                Case Else
                    'choose bracket type based on number of players
                    Select Case intPlayerCount
                        Case 2
                            Call Create2OutOf3Bracket()
                        Case 3
                            Call CreateTrueDoubleBracket()
                        Case Else
                            Call CreateBracket(ce_BracketType.DoubleEWinner)
                    End Select
            End Select
        End If
    End Sub

    Private Sub Test()
        'txtDivisionName.Text = "Senior Men's 178"
        'txtNames.Text = txtNames.Text & "Joey" & vbNewLine
        'txtNames.Text = txtNames.Text & "Matt" & vbNewLine
        'txtNames.Text = txtNames.Text & "Sonnier" & vbNewLine
        'txtNames.Text = txtNames.Text & "Eric" & vbNewLine
        'txtNames.Text = txtNames.Text & "Cheramie" & vbNewLine
        'txtNames.Text = txtNames.Text & "Davis" & vbNewLine
        'txtNames.Text = txtNames.Text & "Sam" & vbNewLine
        'txtNames.Text = txtNames.Text & "Nick"
        'Call CreateBracket(ce_BracketType.SingleE)

        'txtDivisionName.Text = "Senior Women's 120"
        'txtNames.Text = txtNames.Text & "Kerri" & vbNewLine
        'txtNames.Text = txtNames.Text & "Alyssa" & vbNewLine
        'txtNames.Text = txtNames.Text & "Lisa" & vbNewLine
        'txtNames.Text = txtNames.Text & "Camille" & vbNewLine
        'txtNames.Text = txtNames.Text & "Christina" & vbNewLine
        'txtNames.Text = txtNames.Text & "Brittney"
        'Call CreateBracket(ce_BracketType.SingleE)

        'txtDivisionName.Text = "Novice Men's 178"
        'txtNames.Text = txtNames.Text & "Joe V" & vbNewLine
        'txtNames.Text = txtNames.Text & "Tom" & vbNewLine
        'txtNames.Text = txtNames.Text & "Jason" & vbNewLine
        'txtNames.Text = txtNames.Text & "Jacob" & vbNewLine
        'txtNames.Text = txtNames.Text & "Summer" & vbNewLine
        'txtNames.Text = txtNames.Text & "Zack" & vbNewLine
        'txtNames.Text = txtNames.Text & "Brian H" & vbNewLine
        'txtNames.Text = txtNames.Text & "Ryan" & vbNewLine
        'txtNames.Text = txtNames.Text & "David" & vbNewLine
        'txtNames.Text = txtNames.Text & "Sean"
    End Sub

    Private Sub ClearNewLines(ByRef ra_strList() As String)
        '
        'Clear all line breaks in the name's list
        '
        Dim a_strTemp As New ArrayList
        Dim intCount As Integer

        For intCount = 0 To ra_strList.Length - 1
            If ra_strList(intCount) <> String.Empty _
            AndAlso ra_strList(intCount) <> vbNewLine Then
                a_strTemp.Add(ra_strList(intCount))
            End If
        Next

        ra_strList = Nothing
        ReDim ra_strList(a_strTemp.Count - 1)

        intCount = 0

        For Each objItem As Object In a_strTemp
            ra_strList(intCount) = CStr(objItem)
            intCount += 1
        Next

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

        Call ClearNewLines(a_strNames)

        intPlayersEntered = a_strNames.Length

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

        Call LoadBracketNames()

        If ve_Type = ce_BracketType.DoubleEWinner Then
            Call CreateLosersBracket(intPlayersEntered - 1)
        End If

        a_strNames = Nothing
        objPlayersList = Nothing
        txtNames.Text = String.Empty
        txtDivisionName.Text = String.Empty

    End Sub

    Private Sub Create2OutOf3Bracket()
        Dim a_strNames() As String

        a_strNames = Split(txtNames.Text, vbNewLine)
        Call ClearNewLines(a_strNames)

        Call objBracketManager.AddBracket(txtDivisionName.Text, ce_BracketType.TwoOutOfThree)
        Call objBracketManager.AddMatch(a_strNames(0), a_strNames(1))
        Call objBracketManager.AddMatch(String.Empty, String.Empty)
        Call objBracketManager.AddMatch(String.Empty, String.Empty)
        Call LoadBracketNames()
        txtNames.Text = String.Empty
        txtDivisionName.Text = String.Empty
    End Sub

    Private Sub CreateTrueDoubleBracket()
        Dim a_strNames() As String

        a_strNames = Split(txtNames.Text, vbNewLine)
        Call ClearNewLines(a_strNames)

        Call objBracketManager.AddBracket(txtDivisionName.Text, ce_BracketType.TrueDouble)
        Call objBracketManager.AddMatch(a_strNames(0), a_strNames(1))
        Call objBracketManager.AddMatch(a_strNames(2), "bye")
        Call objBracketManager.AddMatch(String.Empty, "bye")
        Call objBracketManager.AddMatch("bye", "bye")

        Call objBracketManager.AddMatch(String.Empty, a_strNames(2))
        Call objBracketManager.AddMatch(String.Empty, String.Empty)

        Call objBracketManager.AddMatch(String.Empty, String.Empty)

        Call objBracketManager.AddMatch(String.Empty, String.Empty)
        Call LoadBracketNames()
        txtNames.Text = String.Empty
        txtDivisionName.Text = String.Empty
    End Sub

    Private Sub CreateLosersBracket(ByVal v_intPlayerCount As Integer)
        Dim a_strNames(v_intPlayerCount - 1) As String
        Dim objPlayersList As New ArrayList
        Dim intPlayerCount As Integer
        'Dim intPlayersEntered As Integer
        Dim intMatchCount As Integer
        Dim intPlayerIndex As Integer
        Dim intMatchIndex As Integer
        Dim intCurrentMatchNum As Integer = 0
        Dim intBracketSize As Integer
        Dim intCount As Integer

        For intCount = 0 To v_intPlayerCount - 1
            a_strNames.SetValue(String.Empty, intCount)
        Next

        intBracketSize = intGetBracketSize(v_intPlayerCount)

        Call AddByeSpots(a_strNames, _
                         objPlayersList, _
                         intBracketSize - v_intPlayerCount)

        intPlayerCount = objPlayersList.Count
        intMatchCount = intPlayerCount - 1

        Call objBracketManager.AddBracket(txtDivisionName.Text & " Loser's Bracket", ce_BracketType.DoubleELoser)
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
        Dim intByeCount As Integer = v_intByeCount
        Dim intMidPoint As Integer = CInt(ra_strInputList.Length / 2)

        If (intMidPoint Mod 2) = 0 Then
            intMidPoint += 1
        End If

        Select Case v_intByeCount
            Case 1
                'Make the first match a bye
                Call r_objOutputList.Add(ra_strInputList(0))
                Call r_objOutputList.Add("bye")

                'Add the rest of the match to the output list
                For intCount = 1 To ra_strInputList.Count - 1
                    Call r_objOutputList.Add(ra_strInputList(intCount))
                Next
            Case 2
                'Make the first and last match a bye
                Call r_objOutputList.Add(ra_strInputList(0))
                Call r_objOutputList.Add("bye")

                For intCount = 1 To ra_strInputList.Count - 2
                    Call r_objOutputList.Add(ra_strInputList(intCount))
                Next

                Call r_objOutputList.Add(ra_strInputList(ra_strInputList.Length - 1))
                Call r_objOutputList.Add("bye")
            Case 3
                'Make the first, middle, and last match a bye
                Call r_objOutputList.Add(ra_strInputList(0))
                Call r_objOutputList.Add("bye")

                For intCount = 1 To intMidPoint - 1
                    Call r_objOutputList.Add(ra_strInputList(intCount))
                Next

                Call r_objOutputList.Add(ra_strInputList(intMidPoint))
                Call r_objOutputList.Add("bye")

                For intCount = intMidPoint + 1 To ra_strInputList.Count - 2
                    Call r_objOutputList.Add(ra_strInputList(intCount))
                Next

                Call r_objOutputList.Add(ra_strInputList(ra_strInputList.Length - 1))
                Call r_objOutputList.Add("bye")
            Case Else
                'Make every other match a bye, as long as that's possible
                For intCount = 0 To ra_strInputList.Count - 1
                    If ra_strInputList.Length - intCount = intByeCount Then
                        Call r_objOutputList.Add(ra_strInputList(intCount))
                        Call r_objOutputList.Add("bye")
                        intByeCount -= 1
                    Else
                        Call r_objOutputList.Add(ra_strInputList(intCount))
                        If intCount Mod 3 = 0 AndAlso intByeCount > 0 Then
                            Call r_objOutputList.Add("bye")
                            intByeCount -= 1
                        End If
                    End If
                Next

        End Select
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
        Dim FileWriter As IO.StreamWriter
        Dim intMatchNumberBye As Integer = 0
        Dim e_Color As ce_MatchColor = ce_MatchColor.e_Blue

        FileWriter = New IO.StreamWriter(System.Environment.CurrentDirectory & "\test.txt")
        FileWriter.Write("Brackets")
        FileWriter.WriteLine()
        FileWriter.WriteLine()
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
                    FileWriter.Write(objMatch.matchNumber & vbTab)
                Else
                    FileWriter.Write("*" & vbTab)
                End If
                FileWriter.Write(objMatch.player1 & " - ")
                FileWriter.Write(objMatch.player2)
                FileWriter.WriteLine()
                If objBracketManager.blnLevelIsDone Then
                    FileWriter.WriteLine()
                End If

                Call objBracketManager.NextMatch()
                If objBracketManager.blnLastMatch Then
                    Exit While
                End If
            End While
            FileWriter.WriteLine()
            Call objBracketManager.NextBracket()
        Loop
        FileWriter.WriteLine()
        FileWriter.WriteLine()
        FileWriter.Write("Match Cards")
        FileWriter.WriteLine()
        FileWriter.WriteLine()
        Call objBracketManager.MarkBracketsReady()

        'Print Match Cards
        'Call objBracketManager.FirstBracket()
        'Do Until objBracketManager.blnLastBracket
        '    Select objBracketManager.objGetCurrentBracket.BracketType
        '        Case ce_BracketType.DoubleEWinner, ce_BracketType.SingleE
        '            Call objBracketManager.LevelStart()
        '            While True
        '                FileWriter.Write(objBracketManager.objGetCurrentMatch.player1 & vbTab & vbTab)
        '                If objBracketManager.objGetCurrentMatch.player2 = "bye" Then
        '                    Call objBracketManager.intGetMatchNumberBye(intMatchNumberBye, e_Color)
        '                    If e_Color = ce_MatchColor.e_Blue Then
        '                        FileWriter.Write(CStr(intMatchNumberBye) & vbTab & "Blue")
        '                    Else
        '                        FileWriter.Write(CStr(intMatchNumberBye) & vbTab & "White")
        '                    End If
        '                Else
        '                    FileWriter.Write(objBracketManager.objGetCurrentMatch.matchNumber & vbTab & "Blue")
        '                End If
        '                FileWriter.WriteLine()

        '                If objBracketManager.objGetCurrentMatch.player2 <> "bye" Then
        '                    FileWriter.Write(objBracketManager.objGetCurrentMatch.player2 & vbTab & vbTab)
        '                    FileWriter.Write(objBracketManager.objGetCurrentMatch.matchNumber & vbTab & "White")
        '                    FileWriter.WriteLine()
        '                End If

        '                If objBracketManager.blnLevelIsDone Then
        '                    Exit While
        '                End If
        '                Call objBracketManager.NextMatch()
        '            End While
        '        Case ce_BracketType.TrueDouble
        '            FileWriter.Write(objBracketManager.objGetCurrentMatch.player1 & vbTab & vbTab)
        '            FileWriter.Write(objBracketManager.objGetCurrentMatch.matchNumber & vbTab & "Blue")
        '            FileWriter.WriteLine()
        '            FileWriter.Write(objBracketManager.objGetCurrentMatch.player2 & vbTab & vbTab)
        '            FileWriter.Write(objBracketManager.objGetCurrentMatch.matchNumber & vbTab & "White")
        '            FileWriter.WriteLine()
        '            Call objBracketManager.NextMatch()
        '            FileWriter.Write(objBracketManager.objGetCurrentMatch.player1 & vbTab & vbTab)
        '            Call objBracketManager.intGetMatchNumberBye(intMatchNumberBye, e_Color)
        '            FileWriter.Write(CStr(intMatchNumberBye) & vbTab & "Blue")
        '            FileWriter.WriteLine()

        '        Case ce_BracketType.TwoOutOfThree
        '            FileWriter.Write(objBracketManager.objGetCurrentMatch.player1 & vbTab & vbTab)
        '            FileWriter.Write(objBracketManager.objGetCurrentMatch.matchNumber & vbTab & "Blue")
        '            FileWriter.WriteLine()
        '            FileWriter.Write(objBracketManager.objGetCurrentMatch.player2 & vbTab & vbTab)
        '            FileWriter.Write(objBracketManager.objGetCurrentMatch.matchNumber & vbTab & "Blue")
        '            FileWriter.WriteLine()
        '    End Select

        '    Call objBracketManager.NextBracket()
        'Loop

        FileWriter.Close()
    End Sub

    Private Sub NumberMatches()
        Dim intMatchNum As Integer = 0
        Dim intOneQuarter As Integer = CInt(objBracketManager.intGetTotalMatchCount / 4)

        Call objBracketManager.MarkBracketsReady()

        While Not objBracketManager.blnBracketsFinished
            Call objBracketManager.FirstBracket()
            Do Until objBracketManager.blnLastBracket
                'Skip numbering loser's brackets until 1/4 are done
                If objBracketManager.objGetCurrentBracket.BracketType <> ce_BracketType.DoubleELoser _
                OrElse intMatchNum > intOneQuarter Then
                    'Don't number matches that are byes
                    If objBracketManager.objGetCurrentMatch.player2 <> "bye" Then
                        intMatchNum += 1
                        objBracketManager.objGetCurrentMatch.matchNumber = intMatchNum
                    End If
                    Call objBracketManager.NextMatch()
                    If objBracketManager.blnLastMatch Then
                        'Done numbering current bracket
                        Call objBracketManager.MarkBracketFinished()
                    End If
                End If
                Call objBracketManager.NextBracket()
            Loop
        End While

        Call objBracketManager.MarkBracketsReady()
    End Sub

    Private Sub NumberMatches2()
        Dim intMatchNum As Integer = 0
        Dim blnFirstRoundDone = False

        Call objBracketManager.MarkBracketsReady()

        While Not objBracketManager.blnBracketsFinished
            Call objBracketManager.FirstBracket()
            Do Until objBracketManager.blnLastBracket
                If objBracketManager.objGetCurrentBracket.BracketType <> _
                ce_BracketType.DoubleELoser OrElse blnFirstRoundDone Then
                    If Not blnFirstRoundDone Then
                        Call objBracketManager.LevelStart()
                    End If

                    While True
                        'Don't number matches that are byes
                        If objBracketManager.objGetCurrentMatch.player2 <> "bye" Then
                            intMatchNum += 1
                            objBracketManager.objGetCurrentMatch.matchNumber = intMatchNum
                        End If
                        Call objBracketManager.NextMatch()
                        If objBracketManager.blnLastMatch Then
                            'Done numbering current bracket
                            Call objBracketManager.MarkBracketFinished()
                            Exit While
                        ElseIf objBracketManager.blnLevelIsDone Then
                            Exit While
                        End If

                    End While
                End If
                Call objBracketManager.NextBracket()
            Loop
            blnFirstRoundDone = True
        End While

        Call objBracketManager.MarkBracketsReady()
    End Sub

    Private Sub AddNamesToLosersBrackets()
        Dim intMatchNumber As Integer
        Dim blnDone As Boolean = False

        Call objBracketManager.MarkBracketsReady()

        'Skip any non modified double brackets
        Call objBracketManager.FirstBracket()
        Do Until objBracketManager.blnLastBracket
            If objBracketManager.objGetCurrentBracket.BracketType = ce_BracketType.SingleE OrElse _
               objBracketManager.objGetCurrentBracket.BracketType = ce_BracketType.TrueDouble OrElse _
               objBracketManager.objGetCurrentBracket.BracketType = ce_BracketType.TwoOutOfThree Then
                Call objBracketManager.MarkBracketFinished()
            End If
            Call objBracketManager.NextBracket()
        Loop

        While Not objBracketManager.blnBracketsFinished
            Call objBracketManager.FirstBracket()

            Do Until objBracketManager.blnLastBracket
                'This starts at a winner's bracket
                If objBracketManager.objGetCurrentMatch.player2 <> "bye" Then
                    'non-bye match, so store the number
                    intMatchNumber = objBracketManager.objGetCurrentMatch.matchNumber
                    Call objBracketManager.NextMatch()
                    If objBracketManager.blnLastMatch Then
                        Call objBracketManager.MarkBracketFinished()
                        blnDone = True
                    End If

                    Call objBracketManager.NextBracket()

                    'Now at loser's bracket
                    If objBracketManager.objGetCurrentMatch.player2 = "bye" Then
                        objBracketManager.objGetCurrentMatch.player1 = "L-" & CStr(intMatchNumber)
                        If blnDone Then
                            Call objBracketManager.MarkBracketFinished()
                            blnDone = False
                        Else
                            Call objBracketManager.NextMatch()
                        End If
                    ElseIf objBracketManager.objGetCurrentMatch.player1 = String.Empty Then
                        objBracketManager.objGetCurrentMatch.player1 = "L-" & CStr(intMatchNumber)
                        'fix?
                        'If blnDone Then
                        '    Call objBracketManager.MarkBracketFinished()
                        '    blnDone = False
                        'End If
                    Else
                        'It's a non-bye and player 1 is named, so name player 2
                        objBracketManager.objGetCurrentMatch.player2 = "L-" & CStr(intMatchNumber)
                        If blnDone Then
                            Call objBracketManager.MarkBracketFinished()
                            blnDone = False
                        Else
                            Call objBracketManager.NextMatch()
                        End If
                    End If

                    'Go to next winner's bracket
                    Call objBracketManager.NextBracket()

                Else
                    'Skip to the next winner's bracket
                    Call objBracketManager.NextMatch()
                    Call objBracketManager.NextBracket()
                    Call objBracketManager.NextBracket()
                End If
            Loop

        End While

        Call objBracketManager.MarkBracketsReady()

    End Sub

    Private Sub btnDump_Click(ByVal sender As System.Object, _
                              ByVal e As System.EventArgs) _
                              Handles btnDump.Click
        Dim objAnswer As MsgBoxResult

        objAnswer = MsgBox("Ready to print?", MsgBoxStyle.YesNo)

        If objAnswer = MsgBoxResult.Yes Then
            btnDump.Enabled = False
            txtDivisionName.Enabled = False
            txtNames.Enabled = False
            btnAddBracket.Enabled = False
            btnDelete.Enabled = False

            Call NumberMatches()
            Call AddNamesToLosersBrackets()

            Call DumpMatches()
            'objPrint = New clsPrint(objBracketManager)
            'Call objPrint.PrintAllBrackets()
            Call objBracketManager.MarkBracketsReady()
            btnModify.Enabled = True
        End If

    End Sub

    Private Sub lstBrackets_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) _
                                                 Handles lstBrackets.SelectedIndexChanged
        If Not IsNothing(lstBrackets.SelectedItem) Then
            Call objBracketManager.MarkBracketsReady()
            Call objBracketManager.FirstBracket()
            Do Until objBracketManager.blnLastBracket
                If objBracketManager.objGetCurrentBracket.DivisionName = _
                   CStr(lstBrackets.SelectedItem) Then
                    Call LoadNames()
                    Exit Sub
                End If
                Call objBracketManager.NextBracket()
            Loop
        End If
    End Sub

    Private Sub LoadNames()
        'Loads list of player names for selected bracket
        Dim strPlayer1 As String
        Dim strPlayer2 As String

        Call lstNames.Items.Clear()

        If objBracketManager.objGetCurrentBracket.BracketType = ce_BracketType.TrueDouble Then
            Call lstNames.Items.Add(objBracketManager.objGetCurrentMatch.player1)
            Call lstNames.Items.Add(objBracketManager.objGetCurrentMatch.player2)

            Call objBracketManager.NextMatch()

            Call lstNames.Items.Add(objBracketManager.objGetCurrentMatch.player1)
        Else
            Do Until objBracketManager.blnLastMatch
                strPlayer1 = objBracketManager.objGetCurrentMatch.player1
                strPlayer2 = objBracketManager.objGetCurrentMatch.player2
                If strPlayer1 <> String.Empty AndAlso strPlayer1 <> "bye" Then
                    Call lstNames.Items.Add(strPlayer1)
                End If
                If strPlayer2 <> String.Empty AndAlso strPlayer2 <> "bye" Then
                    Call lstNames.Items.Add(strPlayer2)
                End If

                Call objBracketManager.NextMatch()
            Loop
        End If
        Call objBracketManager.MarkBracketsReady()
    End Sub

    Private Sub LoadBracketNames()
        Call lstBrackets.Items.Clear()

        Call objBracketManager.MarkBracketsReady()

        Do Until objBracketManager.blnLastBracket
            If objBracketManager.objGetCurrentBracket.BracketType <> ce_BracketType.DoubleELoser Then
                Call lstBrackets.Items.Add(objBracketManager.objGetCurrentBracket.DivisionName)
            End If
            Call objBracketManager.NextBracket()
        Loop

    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Dim objAnswer As MsgBoxResult

        If Not IsNothing(lstBrackets.SelectedItem) Then
            objAnswer = MsgBox("Delete selected bracket?", MsgBoxStyle.YesNo)

            If objAnswer = MsgBoxResult.Yes Then
                Call objBracketManager.DeleteBracket(CStr(lstBrackets.SelectedItem))
                Call LoadBracketNames()
                Call lstNames.Items.Clear()
            End If
        Else
            MsgBox("Select a bracket to delete", MsgBoxStyle.OkOnly)
        End If
    End Sub

    Private Sub btnModify_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnModify.Click
        Call ClearMatchNumbers()
        Call ClearLoserBracketNames()

        btnDump.Enabled = True
        btnAddBracket.Enabled = True
        txtDivisionName.Enabled = True
        txtNames.Enabled = True
        btnDelete.Enabled = True
        btnModify.Enabled = False
    End Sub

    Private Sub ClearMatchNumbers()
        'Used when brackets needed to be reprocessed
        Call objBracketManager.MarkBracketsReady()

        Do Until objBracketManager.blnLastBracket
            Do Until objBracketManager.blnLastMatch
                objBracketManager.objGetCurrentMatch.matchNumber = 0
                Call objBracketManager.NextMatch()
            Loop
            Call objBracketManager.NextBracket()
        Loop

    End Sub

    Private Sub ClearLoserBracketNames()
        Call objBracketManager.MarkBracketsReady()

        Do Until objBracketManager.blnLastBracket
            If objBracketManager.objGetCurrentBracket.BracketType <> ce_BracketType.DoubleELoser Then
                Call objBracketManager.MarkBracketFinished()
            End If
            Call objBracketManager.NextBracket()
        Loop

        Call objBracketManager.FirstBracket()

        'Loop through the Loser's brackets
        Do Until objBracketManager.blnLastBracket
            Do Until objBracketManager.blnLastMatch
                objBracketManager.objGetCurrentMatch.player1 = String.Empty
                If objBracketManager.objGetCurrentMatch.player2 <> "bye" Then
                    objBracketManager.objGetCurrentMatch.player2 = String.Empty
                End If
                Call objBracketManager.NextMatch()
            Loop
            Call objBracketManager.MarkBracketFinished()
            Call objBracketManager.NextBracket()
        Loop
        Call objBracketManager.MarkBracketsReady()

    End Sub

End Class
