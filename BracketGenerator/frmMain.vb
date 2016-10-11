Imports System.IO

Public Class frmMain
    Private objMat1Manager As clsBracketManager
    Private objMat2Manager As clsBracketManager
    Private objPrint As clsPrint
    Const c_AutoIndex As Integer = 0

    Private Sub frmMain_Load(ByVal sender As System.Object, _
                             ByVal e As System.EventArgs) _
                             Handles MyBase.Load

        objMat1Manager = New clsBracketManager
        objMat2Manager = New clsBracketManager

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
        ElseIf intPlayerCount > 16 Then
            MsgBox("Do not enter more than 16 names")
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
            Call LoadBracketNames()
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
            Call objMat1Manager.AddBracket(txtDivisionName.Text, ce_BracketType.SingleE)
        ElseIf ve_Type = ce_BracketType.DoubleEWinner Then
            Call objMat1Manager.AddBracket(txtDivisionName.Text, ce_BracketType.DoubleEWinner)
        End If

        For intPlayerIndex = 0 To intBracketSize - 1 Step 2
            objMat1Manager.AddMatch(CStr(objPlayersList(intPlayerIndex)), _
                                       CStr(objPlayersList(intPlayerIndex + 1)))
            intCurrentMatchNum += 1
        Next
        'Insert placeholders
        For intMatchIndex = intCurrentMatchNum To intMatchCount - 1 Step 1
            objMat1Manager.AddMatch(String.Empty, String.Empty)
        Next

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

        Call objMat1Manager.AddBracket(txtDivisionName.Text, ce_BracketType.TwoOutOfThree)
        Call objMat1Manager.AddMatch(a_strNames(0), a_strNames(1))
        Call objMat1Manager.AddMatch(String.Empty, String.Empty)
        Call objMat1Manager.AddMatch(String.Empty, String.Empty)

        txtNames.Text = String.Empty
        txtDivisionName.Text = String.Empty
    End Sub

    Private Sub CreateTrueDoubleBracket()
        Dim a_strNames() As String

        a_strNames = Split(txtNames.Text, vbNewLine)
        Call ClearNewLines(a_strNames)

        Call objMat1Manager.AddBracket(txtDivisionName.Text, ce_BracketType.TrueDouble)
        Call objMat1Manager.AddMatch(a_strNames(0), a_strNames(1))
        Call objMat1Manager.AddMatch(a_strNames(2), "bye")
        Call objMat1Manager.AddMatch(String.Empty, "bye")
        Call objMat1Manager.AddMatch("bye", "bye")

        Call objMat1Manager.AddMatch(String.Empty, a_strNames(2))
        Call objMat1Manager.AddMatch(String.Empty, String.Empty)

        Call objMat1Manager.AddMatch(String.Empty, String.Empty)

        Call objMat1Manager.AddMatch(String.Empty, String.Empty)

        txtNames.Text = String.Empty
        txtDivisionName.Text = String.Empty
    End Sub

    Private Sub CreateLosersBracket(ByVal v_intPlayerCount As Integer)
        Dim a_strNames(v_intPlayerCount - 1) As String
        Dim objPlayersList As New ArrayList
        Dim intPlayerCount As Integer
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

        Call objMat1Manager.AddBracket(txtDivisionName.Text & " Loser's Bracket", ce_BracketType.DoubleELoser)
        For intPlayerIndex = 0 To intBracketSize - 1 Step 2
            objMat1Manager.AddMatch(CStr(objPlayersList(intPlayerIndex)), _
                                       CStr(objPlayersList(intPlayerIndex + 1)))
            intCurrentMatchNum += 1
        Next
        'Insert placeholders
        For intMatchIndex = intCurrentMatchNum To intMatchCount - 1 Step 1
            objMat1Manager.AddMatch(String.Empty, String.Empty)
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
        Dim objBracketManager As clsBracketManager
        Dim objMatch As New clsMatch
        Dim FileWriter As IO.StreamWriter
        Dim intMatchNumberBye As Integer = 0
        Dim e_Color As ce_MatchColor = ce_MatchColor.e_Blue

        FileWriter = New IO.StreamWriter(System.Environment.CurrentDirectory & "\test.txt")
        objBracketManager = objMat1Manager
        FileWriter.Write("Mat 1")
        FileWriter.WriteLine()
        FileWriter.WriteLine()

        For intCount As Integer = 0 To 1

            Call objBracketManager.MarkBracketsReady()
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
            Call objBracketManager.FirstBracket()
            Do Until objBracketManager.blnLastBracket
                Select Case objBracketManager.objGetCurrentBracket.BracketType
                    Case ce_BracketType.DoubleEWinner, ce_BracketType.SingleE
                        Call objBracketManager.LevelStart()
                        While True
                            FileWriter.Write(objBracketManager.objGetCurrentMatch.player1 & vbTab & vbTab)
                            If objBracketManager.objGetCurrentMatch.player2 = "bye" Then
                                Call objBracketManager.intGetMatchNumberBye(intMatchNumberBye, e_Color)
                                If e_Color = ce_MatchColor.e_Blue Then
                                    FileWriter.Write(CStr(intMatchNumberBye) & vbTab & "Blue")
                                Else
                                    FileWriter.Write(CStr(intMatchNumberBye) & vbTab & "White")
                                End If
                            Else
                                FileWriter.Write(objBracketManager.objGetCurrentMatch.matchNumber & vbTab & "Blue")
                            End If
                            FileWriter.WriteLine()

                            If objBracketManager.objGetCurrentMatch.player2 <> "bye" Then
                                FileWriter.Write(objBracketManager.objGetCurrentMatch.player2 & vbTab & vbTab)
                                FileWriter.Write(objBracketManager.objGetCurrentMatch.matchNumber & vbTab & "White")
                                FileWriter.WriteLine()
                            End If

                            If objBracketManager.blnLevelIsDone Then
                                Exit While
                            End If
                            Call objBracketManager.NextMatch()
                        End While
                    Case ce_BracketType.TrueDouble
                        FileWriter.Write(objBracketManager.objGetCurrentMatch.player1 & vbTab & vbTab)
                        FileWriter.Write(objBracketManager.objGetCurrentMatch.matchNumber & vbTab & "Blue")
                        FileWriter.WriteLine()
                        FileWriter.Write(objBracketManager.objGetCurrentMatch.player2 & vbTab & vbTab)
                        FileWriter.Write(objBracketManager.objGetCurrentMatch.matchNumber & vbTab & "White")
                        FileWriter.WriteLine()
                        Call objBracketManager.NextMatch()
                        FileWriter.Write(objBracketManager.objGetCurrentMatch.player1 & vbTab & vbTab)
                        Call objBracketManager.intGetMatchNumberBye(intMatchNumberBye, e_Color)
                        FileWriter.Write(CStr(intMatchNumberBye) & vbTab & "Blue")
                        FileWriter.WriteLine()

                    Case ce_BracketType.TwoOutOfThree
                        FileWriter.Write(objBracketManager.objGetCurrentMatch.player1 & vbTab & vbTab)
                        FileWriter.Write(objBracketManager.objGetCurrentMatch.matchNumber & vbTab & "Blue")
                        FileWriter.WriteLine()
                        FileWriter.Write(objBracketManager.objGetCurrentMatch.player2 & vbTab & vbTab)
                        FileWriter.Write(objBracketManager.objGetCurrentMatch.matchNumber & vbTab & "Blue")
                        FileWriter.WriteLine()
                End Select

                Call objBracketManager.NextBracket()
            Loop
            objBracketManager = objMat2Manager
            If intCount = 0 Then
                FileWriter.WriteLine()
                FileWriter.WriteLine()
                FileWriter.Write("Mat 2")
                FileWriter.WriteLine()
                FileWriter.WriteLine()
            End If
        Next
        FileWriter.Close()
    End Sub

    Private Sub NumberMatches(ByRef r_objBracketManager As clsBracketManager)
        Dim intMatchNum As Integer = 0
        Dim intOneQuarter As Integer = CInt(r_objBracketManager.intGetTotalMatchCount / 4)

        Call r_objBracketManager.MarkBracketsReady()

        While Not r_objBracketManager.blnBracketsFinished
            Call r_objBracketManager.FirstBracket()
            Do Until r_objBracketManager.blnLastBracket
                'Skip numbering loser's brackets until 1/4 are done
                If r_objBracketManager.objGetCurrentBracket.BracketType <> ce_BracketType.DoubleELoser _
                OrElse intMatchNum > intOneQuarter Then
                    'Don't number matches that are byes
                    If r_objBracketManager.objGetCurrentMatch.player2 <> "bye" Then
                        intMatchNum += 1
                        r_objBracketManager.objGetCurrentMatch.matchNumber = intMatchNum
                    End If
                    Call r_objBracketManager.NextMatch()
                    If r_objBracketManager.blnLastMatch Then
                        'Done numbering current bracket
                        Call r_objBracketManager.MarkBracketFinished()
                    End If
                End If
                Call r_objBracketManager.NextBracket()
            Loop
        End While

        Call r_objBracketManager.MarkBracketsReady()
    End Sub

    Private Sub AddNamesToLosersBrackets(ByRef r_objBracketManager As clsBracketManager)
        Dim intMatchNumber As Integer
        Dim blnDone As Boolean = False

        Call r_objBracketManager.MarkBracketsReady()

        'Skip any non modified double brackets
        Call r_objBracketManager.FirstBracket()
        Do Until r_objBracketManager.blnLastBracket
            If r_objBracketManager.objGetCurrentBracket.BracketType = ce_BracketType.SingleE OrElse _
               r_objBracketManager.objGetCurrentBracket.BracketType = ce_BracketType.TrueDouble OrElse _
               r_objBracketManager.objGetCurrentBracket.BracketType = ce_BracketType.TwoOutOfThree Then
                Call r_objBracketManager.MarkBracketFinished()
            End If
            Call r_objBracketManager.NextBracket()
        Loop

        While Not r_objBracketManager.blnBracketsFinished
            Call r_objBracketManager.FirstBracket()

            Do Until r_objBracketManager.blnLastBracket
                'This starts at a winner's bracket
                If r_objBracketManager.objGetCurrentMatch.player2 <> "bye" Then
                    'non-bye match, so store the number
                    intMatchNumber = r_objBracketManager.objGetCurrentMatch.matchNumber
                    Call r_objBracketManager.NextMatch()
                    If r_objBracketManager.blnLastMatch Then
                        Call r_objBracketManager.MarkBracketFinished()
                        blnDone = True
                    End If

                    Call r_objBracketManager.NextBracket()

                    'Now at loser's bracket
                    If r_objBracketManager.objGetCurrentMatch.player2 = "bye" Then
                        r_objBracketManager.objGetCurrentMatch.player1 = "L-" & CStr(intMatchNumber)
                        If blnDone Then
                            Call r_objBracketManager.MarkBracketFinished()
                            blnDone = False
                        Else
                            Call r_objBracketManager.NextMatch()
                        End If
                    ElseIf r_objBracketManager.objGetCurrentMatch.player1 = String.Empty Then
                        r_objBracketManager.objGetCurrentMatch.player1 = "L-" & CStr(intMatchNumber)
                    Else
                        'It's a non-bye and player 1 is named, so name player 2
                        r_objBracketManager.objGetCurrentMatch.player2 = "L-" & CStr(intMatchNumber)
                        If blnDone Then
                            Call r_objBracketManager.MarkBracketFinished()
                            blnDone = False
                        Else
                            Call r_objBracketManager.NextMatch()
                        End If
                    End If

                    'Go to next winner's bracket
                    Call r_objBracketManager.NextBracket()

                Else
                    'Skip to the next winner's bracket
                    Call r_objBracketManager.NextMatch()
                    Call r_objBracketManager.NextBracket()
                    Call r_objBracketManager.NextBracket()
                End If
            Loop

        End While

        Call r_objBracketManager.MarkBracketsReady()

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
            btnDeleteMat1.Enabled = False
            btnDeleteMat2.Enabled = False
            btnUp.Enabled = False
            btnDown.Enabled = False

            If objMat1Manager.intGetTotalMatchCount > 0 Then
                Call NumberMatches(objMat1Manager)
                Call AddNamesToLosersBrackets(objMat1Manager)
            End If

            If objMat2Manager.intGetTotalMatchCount > 0 Then
                Call NumberMatches(objMat2Manager)
                Call AddNamesToLosersBrackets(objMat2Manager)
            End If

            'Call DumpMatches()
            objPrint = New clsPrint(objMat1Manager, objMat2Manager)
            Call objPrint.PrintAllBrackets()
            Call objPrint.PrintMatchCards()
            Call objMat1Manager.MarkBracketsReady()
            Call objMat2Manager.MarkBracketsReady()
            btnModify.Enabled = True
        End If

    End Sub

    Private Sub lstBrackets_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) _
                                                 Handles lstBrackets.SelectedIndexChanged
        If Not IsNothing(lstBrackets.SelectedItem) Then
            Call objMat1Manager.MarkBracketsReady()
            Call objMat1Manager.FirstBracket()
            Do Until objMat1Manager.blnLastBracket
                If "  " & objMat1Manager.objGetCurrentBracket.DivisionName = _
                   CStr(lstBrackets.SelectedItem) Then
                    Call LoadNames(objMat1Manager)
                    Exit Sub
                End If
                Call objMat1Manager.NextBracket()
            Loop

            Call objMat2Manager.MarkBracketsReady()
            Call objMat2Manager.FirstBracket()
            Do Until objMat2Manager.blnLastBracket
                If "  " & objMat2Manager.objGetCurrentBracket.DivisionName = _
                   CStr(lstBrackets.SelectedItem) Then
                    Call LoadNames(objMat2Manager)
                    Exit Sub
                End If
                Call objMat2Manager.NextBracket()
            Loop

        End If
    End Sub

    Private Sub LoadNames(ByRef r_objBracketManager As clsBracketManager)
        'Loads list of player names for selected bracket
        Dim strPlayer1 As String
        Dim strPlayer2 As String

        Call lstNames.Items.Clear()

        If r_objBracketManager.objGetCurrentBracket.BracketType = ce_BracketType.TrueDouble Then
            Call lstNames.Items.Add(r_objBracketManager.objGetCurrentMatch.player1)
            Call lstNames.Items.Add(r_objBracketManager.objGetCurrentMatch.player2)

            Call r_objBracketManager.NextMatch()

            Call lstNames.Items.Add(r_objBracketManager.objGetCurrentMatch.player1)
        Else
            Do Until r_objBracketManager.blnLastMatch
                strPlayer1 = r_objBracketManager.objGetCurrentMatch.player1
                strPlayer2 = r_objBracketManager.objGetCurrentMatch.player2
                If strPlayer1 <> String.Empty AndAlso strPlayer1 <> "bye" Then
                    Call lstNames.Items.Add(strPlayer1)
                End If
                If strPlayer2 <> String.Empty AndAlso strPlayer2 <> "bye" Then
                    Call lstNames.Items.Add(strPlayer2)
                End If

                Call r_objBracketManager.NextMatch()
            Loop
        End If
        Call r_objBracketManager.MarkBracketsReady()
    End Sub

    Private Sub LoadBracketNames()
        Dim intPlayerCount As Integer = 0
        Dim intTotalPlayerCount As Integer = 0
        Call lstBrackets.Items.Clear()
        Call lstMat1.Items.Clear()
        Call lstMat2.Items.Clear()

        'Load Mat 1
        Call lstBrackets.Items.Add("Mat 1")
        Call objMat1Manager.MarkBracketsReady()

        Do Until objMat1Manager.blnLastBracket
            If objMat1Manager.objGetCurrentBracket.BracketType <> ce_BracketType.DoubleELoser Then
                intPlayerCount = objMat1Manager.intGetPlayerCount
                Call lstBrackets.Items.Add("  " & objMat1Manager.objGetCurrentBracket.DivisionName)
                Call lstMat1.Items.Add(objMat1Manager.objGetCurrentBracket.DivisionName & ": " & _
                                       intPlayerCount)
                intTotalPlayerCount += intPlayerCount
            End If
            Call objMat1Manager.NextBracket()
        Loop
        lblMat1Count.Text = CStr(intTotalPlayerCount)

        intPlayerCount = 0
        intTotalPlayerCount = 0
        'Load Mat 2
        Call lstBrackets.Items.Add("Mat 2")

        Call objMat2Manager.MarkBracketsReady()

        Do Until objMat2Manager.blnLastBracket
            If objMat2Manager.objGetCurrentBracket.BracketType <> ce_BracketType.DoubleELoser Then
                intPlayerCount = objMat2Manager.intGetPlayerCount
                Call lstBrackets.Items.Add("  " & objMat2Manager.objGetCurrentBracket.DivisionName)
                Call lstMat2.Items.Add(objMat2Manager.objGetCurrentBracket.DivisionName & ": " & _
                                       intPlayerCount)
                intTotalPlayerCount += intPlayerCount
            End If
            Call objMat2Manager.NextBracket()
        Loop
        lblMat2Count.Text = CStr(intTotalPlayerCount)
    End Sub

    Private Sub btnModify_Click(ByVal sender As System.Object, _
                                ByVal e As System.EventArgs) _
                                Handles btnModify.Click
        Call ClearMatchNumbers(objMat1Manager)
        Call ClearLoserBracketNames(objMat1Manager)

        Call ClearMatchNumbers(objMat2Manager)
        Call ClearLoserBracketNames(objMat2Manager)

        btnDump.Enabled = True
        btnAddBracket.Enabled = True
        txtDivisionName.Enabled = True
        txtNames.Enabled = True
        btnDeleteMat1.Enabled = True
        btnDeleteMat2.Enabled = True
        btnUp.Enabled = True
        btnDown.Enabled = True
        btnModify.Enabled = False
    End Sub

    Private Sub ClearMatchNumbers(ByRef r_objBracketManager As clsBracketManager)
        'Used when brackets needed to be reprocessed
        Call r_objBracketManager.MarkBracketsReady()

        Do Until r_objBracketManager.blnLastBracket
            Do Until r_objBracketManager.blnLastMatch
                r_objBracketManager.objGetCurrentMatch.matchNumber = 0
                Call r_objBracketManager.NextMatch()
            Loop
            Call r_objBracketManager.NextBracket()
        Loop

    End Sub

    Private Sub ClearLoserBracketNames(ByRef r_objBracketManager As clsBracketManager)
        Call r_objBracketManager.MarkBracketsReady()

        Do Until r_objBracketManager.blnLastBracket
            If r_objBracketManager.objGetCurrentBracket.BracketType <> ce_BracketType.DoubleELoser Then
                Call r_objBracketManager.MarkBracketFinished()
            End If
            Call r_objBracketManager.NextBracket()
        Loop

        Call r_objBracketManager.FirstBracket()

        'Loop through the Loser's brackets
        Do Until r_objBracketManager.blnLastBracket
            Do Until r_objBracketManager.blnLastMatch
                r_objBracketManager.objGetCurrentMatch.player1 = String.Empty
                If r_objBracketManager.objGetCurrentMatch.player2 <> "bye" Then
                    r_objBracketManager.objGetCurrentMatch.player2 = String.Empty
                End If
                Call r_objBracketManager.NextMatch()
            Loop
            Call r_objBracketManager.MarkBracketFinished()
            Call r_objBracketManager.NextBracket()
        Loop
        Call r_objBracketManager.MarkBracketsReady()

    End Sub

    Private Sub btnDown_Click(ByVal sender As System.Object, _
                              ByVal e As System.EventArgs) _
                              Handles btnDown.Click
 
        If Not IsNothing(lstMat1.SelectedItem) Then
            If objMat1Manager.objGetBracket(lstMat1.SelectedIndex).BracketType = _
               ce_BracketType.DoubleEWinner Then
                Call objMat2Manager.AddBracket(objMat1Manager.objGetBracket(lstMat1.SelectedIndex))
                Call objMat2Manager.AddBracket(objMat1Manager.objGetLosersBracket(lstMat1.SelectedIndex))
                Call objMat1Manager.DeleteBracket(lstMat1.SelectedIndex, True)
            Else
                Call objMat2Manager.AddBracket(objMat1Manager.objGetBracket(lstMat1.SelectedIndex))
                Call objMat1Manager.DeleteBracket(lstMat1.SelectedIndex)
            End If

            Call LoadBracketNames()
        End If
    End Sub

    Private Sub btnUp_Click(ByVal sender As System.Object, _
                            ByVal e As System.EventArgs) _
                            Handles btnUp.Click

        If Not IsNothing(lstMat2.SelectedItem) Then
            If objMat2Manager.objGetBracket(lstMat2.SelectedIndex).BracketType = _
               ce_BracketType.DoubleEWinner Then
                Call objMat1Manager.AddBracket(objMat2Manager.objGetBracket(lstMat2.SelectedIndex))
                Call objMat1Manager.AddBracket(objMat2Manager.objGetLosersBracket(lstMat2.SelectedIndex))
                Call objMat2Manager.DeleteBracket(lstMat2.SelectedIndex, True)
            Else
                Call objMat1Manager.AddBracket(objMat2Manager.objGetBracket(lstMat2.SelectedIndex))
                Call objMat2Manager.DeleteBracket(lstMat2.SelectedIndex)
            End If

            Call LoadBracketNames()
        End If
    End Sub

    Private Sub btnDeleteMat1_Click(ByVal sender As System.Object, _
                                    ByVal e As System.EventArgs) _
                                    Handles btnDeleteMat1.Click
        Dim objAnswer As MsgBoxResult
        If Not IsNothing(lstMat1.SelectedItem) Then
            objAnswer = MsgBox("Delete " & objMat1Manager.objGetBracket(lstMat1.SelectedIndex). _
                                DivisionName & "?", MsgBoxStyle.Question)
            If objAnswer = MsgBoxResult.Ok Then
                If objMat1Manager.objGetBracket(lstMat1.SelectedIndex).BracketType = ce_BracketType.DoubleEWinner Then
                    Call objMat1Manager.DeleteBracket(lstMat1.SelectedIndex, True)
                Else
                    Call objMat1Manager.DeleteBracket(lstMat1.SelectedIndex)
                End If
                Call LoadBracketNames()
            End If
        End If
    End Sub

    Private Sub btnDeleteMat2_Click(ByVal sender As System.Object, _
                                    ByVal e As System.EventArgs) _
                                    Handles btnDeleteMat2.Click
        Dim objAnswer As MsgBoxResult
        If Not IsNothing(lstMat2.SelectedItem) Then
            objAnswer = MsgBox("Delete " & objMat2Manager.objGetBracket(lstMat2.SelectedIndex). _
                               DivisionName & "?", MsgBoxStyle.Question)
            If objAnswer = MsgBoxResult.Ok Then
                If objMat2Manager.objGetBracket(lstMat2.SelectedIndex).BracketType = ce_BracketType.DoubleEWinner Then
                    Call objMat2Manager.DeleteBracket(lstMat2.SelectedIndex, True)
                Else
                    Call objMat2Manager.DeleteBracket(lstMat2.SelectedIndex)
                End If
                Call LoadBracketNames()
            End If
        End If
    End Sub

End Class
