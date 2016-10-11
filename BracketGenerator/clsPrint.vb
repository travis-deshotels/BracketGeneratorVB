Public Class clsPrint
    Private objMat1Manager As clsBracketManager
    Private objMat2Manager As clsBracketManager
    Private objFW As IO.StreamWriter
    Private blnMatchCardPageBreak As Boolean = False

    Public Sub New(ByVal v_objBracketMananger1 As clsBracketManager, ByVal v_objBracketMananger2 As clsBracketManager)
        objMat1Manager = v_objBracketMananger1
        objMat2Manager = v_objBracketMananger2
    End Sub

    Public Sub PrintAllBrackets()
        Dim lstBracketManagers As New ArrayList
        Dim intMatNumber As Integer = 1
        Dim intWBracketCount As Integer
        objFW = New IO.StreamWriter(System.Environment.CurrentDirectory & "\brackets.html")

        Call lstBracketManagers.Add(objMat1Manager)
        Call lstBracketManagers.Add(objMat2Manager)

        Call PrintHeader()

        For Each objBracketManager As clsBracketManager In lstBracketManagers

            Call objBracketManager.MarkBracketsReady()


            While Not objBracketManager.blnLastBracket
                Select Case objBracketManager.objGetCurrentBracket.BracketType
                    Case ce_BracketType.SingleE
                        Call PrintSingle(objBracketManager, intMatNumber)
                    Case ce_BracketType.TwoOutOfThree
                        Call Print2Outof3(objBracketManager, intMatNumber)
                    Case ce_BracketType.TrueDouble
                        Call PrintTrueDouble(objBracketManager, intMatNumber)
                    Case Else
                        Select Case objBracketManager.intGetCurrentBracketSize
                            Case 4
                                Call Print4Header(objBracketManager, intMatNumber)
                                Call Print4Player(objBracketManager)
                                Call objBracketManager.NextBracket()
                                'Print Loser's Bracket
                                Call Print4Player(objBracketManager)
                                Call PrintResultFooter()
                            Case 8
                                Call Print8Header(objBracketManager, intMatNumber)
                                Call Print8player(objBracketManager)
                                intWBracketCount = objBracketManager.intGetPlayerCount()
                                Call objBracketManager.NextBracket()
                                'Print Loser's Bracket
                                If intWBracketCount > 5 Then
                                    Call Print8player(objBracketManager)
                                    Call Print8Footer()
                                Else
                                    Call Print4Player(objBracketManager)
                                    Call Print8Footer()
                                End If
                            Case 16
                                Call Print8Header(objBracketManager, intMatNumber)
                                Call Print16player(objBracketManager)
                                Call Print8Footer()
                                intWBracketCount = objBracketManager.intGetPlayerCount()
                                Call objBracketManager.NextBracket()
                                'Print Loser's Bracket
                                If intWBracketCount > 9 Then
                                    Call Print8Header(objBracketManager, intMatNumber)
                                    Call Print16player(objBracketManager)
                                    Call Print8Footer()
                                Else
                                    Call Print8Header(objBracketManager, intMatNumber)
                                    Call Print8player(objBracketManager)
                                    Call Print8Footer()
                                End If
                            Case Else
                                Exit Sub
                        End Select
                End Select

                Call objBracketManager.NextBracket()
            End While

            Call objBracketManager.MarkBracketsReady()
            intMatNumber = 2
        Next objBracketManager
        Call PrintFoot()
        Call objFW.Close()
    End Sub

    Public Sub PrintMatchCards()
        Dim lstBrackets As New ArrayList
        Call lstBrackets.Add(objMat1Manager)
        Call lstBrackets.Add(objMat2Manager)
        Dim intMatNumber As Integer = 1
        Dim intMatchNumberBye As Integer
        Dim e_Color As ce_MatchColor

        objFW = New IO.StreamWriter(System.Environment.CurrentDirectory & "\matchcards.htm")
        Call PrintMatchCardHeader()

        For Each objBracketManager As clsBracketManager In lstBrackets

            Call objBracketManager.FirstBracket()
            Do Until objBracketManager.blnLastBracket
                Select Case objBracketManager.objGetCurrentBracket.BracketType
                    Case ce_BracketType.DoubleEWinner
                        Call objBracketManager.LevelStart()
                        While True
                            If objBracketManager.objGetCurrentMatch.player2 = "bye" Then
                                Call objBracketManager.intGetMatchNumberBye(intMatchNumberBye, e_Color)
                                Call PrintMatchCard(intMatNumber, objBracketManager.objGetCurrentMatch.player1, _
                                                    intMatchNumberBye, e_Color)
                            Else
                                Call PrintMatchCard(intMatNumber, objBracketManager.objGetCurrentMatch.player1, _
                                                    objBracketManager.objGetCurrentMatch.matchNumber, ce_MatchColor.e_Blue)
                            End If

                            If objBracketManager.objGetCurrentMatch.player2 <> "bye" Then
                                Call PrintMatchCard(intMatNumber, objBracketManager.objGetCurrentMatch.player2, _
                                                    objBracketManager.objGetCurrentMatch.matchNumber, ce_MatchColor.e_White)
                            End If

                            If objBracketManager.blnLevelIsDone Then
                                Exit While
                            End If
                            Call objBracketManager.NextMatch()
                        End While
                    Case ce_BracketType.TrueDouble
                        Call PrintMatchCard(intMatNumber, objBracketManager.objGetCurrentMatch.player1, _
                                            objBracketManager.objGetCurrentMatch.matchNumber, ce_MatchColor.e_Blue)
                        Call PrintMatchCard(intMatNumber, objBracketManager.objGetCurrentMatch.player2, _
                                            objBracketManager.objGetCurrentMatch.matchNumber, ce_MatchColor.e_White)
                        Call objBracketManager.NextMatch()
                        Call objBracketManager.intGetMatchNumberBye(intMatchNumberBye, e_Color)
                        Call PrintMatchCard(intMatNumber, objBracketManager.objGetCurrentMatch.player1, _
                                            intMatchNumberBye, ce_MatchColor.e_Blue)

                    Case ce_BracketType.TwoOutOfThree, ce_BracketType.SingleE
                        Call PrintMatchCard(intMatNumber, objBracketManager.objGetCurrentMatch.player1, _
                                            objBracketManager.objGetCurrentMatch.matchNumber, ce_MatchColor.e_Blue)
                        Call PrintMatchCard(intMatNumber, objBracketManager.objGetCurrentMatch.player2, _
                                            objBracketManager.objGetCurrentMatch.matchNumber, ce_MatchColor.e_White)
                End Select

                Call objBracketManager.NextBracket()
            Loop
            intMatNumber = 2
        Next objBracketManager
        Call PrintMatchCardFooter()
        Call objFW.Close()

    End Sub

    Private Sub PrintMatchCardHeader()
        objFW.Write("<html>")
        objFW.Write("<head><title>hide this title</title>")
        objFW.Write("<link rel='stylesheet' href='brackets.css' type='text/css' media='all'>")
        objFW.Write("</head>")
        objFW.Write("<body>")
    End Sub

    Private Sub PrintMatchCardFooter()
        objFW.Write("</body></html>")
    End Sub

    Private Sub PrintMatchCard(ByVal v_intMatNumber As Integer, ByVal v_strName As String, _
                              ByVal v_intMatchNumber As Integer, ByVal ve_Color As ce_MatchColor)
        objFW.Write("<div class=""matchCard"">")
        objFW.Write("<div class=""playerName"">Name: <span>")
        objFW.Write(v_strName)
        objFW.Write("</span></div>")
        objFW.Write("<div class=""tournamentHeader"">")
        objFW.Write("2011 Louisiana State Championships<br>")
        objFW.Write("March 19, 2011<br>")
        objFW.Write("Lafayette, Louisiana")
        objFW.Write("</div>")
        objFW.Write("<div class=""matNumber"">")
        objFW.Write("Mat #")
        objFW.Write("<span>" & CStr(v_intMatNumber) & "</span>")
        objFW.Write("</div>")
        ' objFW.Write(v_strName)
        objFW.Write("<table border=""0"" cellpadding=""0"" cellspacing=""0"" class=""matchTable"">")
        objFW.Write("<tr><th>Match #:</th>")
        objFW.Write("<td>" & CStr(v_intMatchNumber) & "</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("</tr>")
        objFW.Write("<tr class=""giColor"">")
        objFW.Write("<th>Gi Color:</th>")
        If ve_Color = ce_MatchColor.e_Blue Then
            objFW.Write("<td><span class=""selected"">Blue</span><span>White</span></td>")
        Else
            objFW.Write("<td><span>Blue</span><span class=""selected"">White</span></td>")
        End If
        objFW.Write("<td><span>Blue</span><span>White</span></td>")
        objFW.Write("<td><span>Blue</span><span>White</span></td>")
        objFW.Write("<td><span>Blue</span><span>White</span></td>")
        objFW.Write("<td><span>Blue</span><span>White</span></td>")
        objFW.Write("<td><span>Blue</span><span>White</span></td>")
        objFW.Write("<td><span>Blue</span><span>White</span></td>")
        objFW.Write("<td><span>Blue</span><span>White</span></td>")
        objFW.Write("</tr>")
        objFW.Write("<tr>")
        objFW.Write("<th>Opponent:</th>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("</tr>")
        objFW.Write("<tr>")
        objFW.Write("<th>Result:</th>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("</tr>")
        objFW.Write("</table>")
        objFW.Write("<br>")
        objFW.Write("<div class=""bracketMsg"">")
        objFW.Write("<span>IMPORTANT!!!</span> Report to your assigned mat -- 3 matches before your Match Number")
        objFW.Write("</div>")
        objFW.Write("</table>")
        objFW.Write("</div>")
        If blnMatchCardPageBreak Then
            objFW.Write("<div class=""page-break""></div>")
            blnMatchCardPageBreak = False
        Else
            blnMatchCardPageBreak = True
        End If
    End Sub

    Private Sub PrintHeader()
        objFW.Write("<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.01//EN"" ""http://www.w3.org/TR/html4/strict.dtd"">")
        objFW.Write("<html lang='en'>")
        objFW.Write("<head>")
        objFW.Write("<meta http-equiv='Content-Type' content='text/html; charset=ISO-8859-1'>")
        objFW.Write("<title>Page Title</title>")
        objFW.Write("<link rel='stylesheet' href='brackets.css' type='text/css'>")
        objFW.Write("</head>")
        objFW.Write("<body>")
        objFW.Write("<div id=""bodyContainer"">")
    End Sub

    Private Function strMatchNumOrBye(ByVal v_intMatchNumber As Integer) As String
        If v_intMatchNumber > 0 Then
            Return CStr(v_intMatchNumber)
        Else
            Return "&nbsp;"
        End If
    End Function

    Private Sub PrintSingle(ByRef r_objBracketManager As clsBracketManager, ByVal v_intMatNumber As Integer)
        objFW.Write("<h3 align=right>Mat " & CStr(v_intMatNumber) & "</h3>")
        objFW.Write("<h2 align=center>2011 Louisiana State Judo Championships</h2>")
        objFW.Write("<h1 align=center>")
        objFW.Write(r_objBracketManager.objGetCurrentBracket.DivisionName)
        objFW.Write("</h1>")
        objFW.Write("<table border=""0"" cellspacing=""0"" cellpadding=""0"" class=""tbl_bracket"">")
        objFW.Write("<tr>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("</tr>")
        objFW.Write("<tr>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("</tr>")
        objFW.Write("<tr>")
        objFW.Write("<td class=""cell_1"">&nbsp;</td>")
        objFW.Write("<td class=""cell_2"">&nbsp;</td>")
        objFW.Write("<td class=""cell_3"">&nbsp;</td>")
        objFW.Write("<td class=""cell_4"">&nbsp;</td>")
        objFW.Write("</tr>")
        objFW.Write("<tr>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("</tr>")
        objFW.Write("<tr>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("<td class=""brack_under"">")
        objFW.Write(r_objBracketManager.objGetCurrentMatch.player1)
        objFW.Write("</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("</tr>")
        objFW.Write("<tr>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("<td class=""brack_right"" align=right>")
        objFW.Write(r_objBracketManager.objGetCurrentMatch.matchNumber)
        objFW.Write("</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("</tr>")
        objFW.Write("<tr>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("<td class=""brack_right"">&nbsp;</td>")
        objFW.Write("<td class=""brack_under"">&nbsp;</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("</tr>")
        objFW.Write("<tr>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("<td class=""brack_right"">&nbsp;</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("</tr>")
        objFW.Write("<tr>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("<td class=""brack_under_right_up"">")
        objFW.Write(r_objBracketManager.objGetCurrentMatch.player2)
        objFW.Write("</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("</tr>")
        objFW.Write("<tr>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("</tr>")
        objFW.Write("<tr>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("</tr>")
        objFW.Write("</table>")
        objFW.Write("<table class=""foo"">")
        objFW.Write("<tr><td>&nbsp;</td></tr>")
        objFW.Write("<tr>")
        objFW.Write("<td>&nbsp</td>")
        objFW.Write("</tr>")
        objFW.Write("</table>")
        objFW.Write("<div class=""brResults"">")
        objFW.Write("<h2>Results<h2>")
        objFW.Write("<table cellspacing=""0"" cellpadding=""0"" border=""0"">")
        objFW.Write("<tr>")
        objFW.Write("<th>1st Place</th>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("</tr>")
        objFW.Write("<tr>")
        objFW.Write("<th>2nd Place</th>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("</tr>")
        objFW.Write("</table>")
        objFW.Write("</div>")
        objFW.Write("<div class=""page-break""></div>")
    End Sub

    Private Sub Print2Outof3(ByRef r_objBracketManager As clsBracketManager, ByVal v_intMatNumber As Integer)
        Dim strPlayer2 As String = String.Empty
        Dim intMatchNumber2 As Integer = 0
        Dim intMatchNumber3 As Integer = 0
        objFW.Write("<h3 align=right>Mat ")
        objFW.Write(v_intMatNumber)
        objFW.Write("</h3>")
        objFW.Write("<h2 align=center>2011 Louisiana State Judo Championships</h2>")
        objFW.Write("<h1>")
        objFW.Write(r_objBracketManager.objGetCurrentBracket.DivisionName)
        objFW.Write("</h1>")
        objFW.Write("<table border=""0"" cellspacing=""0"" cellpadding=""0"" class=""tbl_bracket"">")
        objFW.Write("<tr>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("</tr>")
        objFW.Write("<tr>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("</tr>")
        objFW.Write("<tr>")
        objFW.Write("<td class=""brack_under cell_1"">")
        objFW.Write(r_objBracketManager.objGetCurrentMatch.player1)
        objFW.Write("</td>")
        objFW.Write("<td class=""cell_2"">&nbsp;</td>")
        objFW.Write("<td class=""cell_3"">&nbsp;</td>")
        objFW.Write("<td class=""cell_4"">&nbsp;</td>")
        objFW.Write("</tr>")
        objFW.Write("<tr>")
        objFW.Write("<td class=""brack_right"" align=right>")
        objFW.Write(r_objBracketManager.objGetCurrentMatch.matchNumber)

        strPlayer2 = r_objBracketManager.objGetCurrentMatch.player2
        Call r_objBracketManager.NextMatch()
        intMatchNumber2 = r_objBracketManager.objGetCurrentMatch.matchNumber
        Call r_objBracketManager.NextMatch()
        intMatchNumber3 = r_objBracketManager.objGetCurrentMatch.matchNumber

        objFW.Write("</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("</tr>")
        objFW.Write("<tr>")
        objFW.Write("<td class=""brack_right"" align=right>&nbsp;</td>")
        objFW.Write("<td class=""brack_under"">&nbsp;</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("</tr>")
        objFW.Write("<tr>")
        objFW.Write("<td class=""brack_right"" align=right>&nbsp;</td>")
        objFW.Write("<td class=""brack_right"" align=right>")
        objFW.Write(CStr(intMatchNumber2))
        objFW.Write("</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("</tr>")
        objFW.Write("<tr>")
        objFW.Write("<td class=""brack_under_right_up"">")
        objFW.Write(strPlayer2)
        objFW.Write("</td>")
        objFW.Write("<td class=""brack_right"">&nbsp;</td>")
        objFW.Write("<td class=""brack_under"">&nbsp;</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("</tr>")
        objFW.Write("<tr>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("<td class=""brack_right"">&nbsp;</td>")
        objFW.Write("<td class=""brack_right"" align=right>")
        objFW.Write(CStr(intMatchNumber3) & "*")
        objFW.Write("</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("</tr>")
        objFW.Write("<tr>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("<td class=""brack_under_right_up"">&nbsp;</td>")
        objFW.Write("<td class=""brack_right"">&nbsp;</td>")
        objFW.Write("<td class=""brack_under"">&nbsp;</td>")
        objFW.Write("</tr>")
        objFW.Write("<tr>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("<td class=""brack_right"">&nbsp;</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("</tr>")
        objFW.Write("<tr>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("<td class=""brack_under_right_up"">&nbsp;</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("</tr>")
        objFW.Write("</table>")
        objFW.Write("<table class=""foo"">")
        objFW.Write("<tr><td>&nbsp;</td></tr>")
        objFW.Write("<tr>")
        objFW.Write("<td><h3>* &nbsp;Last match only occurs if neither player has two losses</h3></td>")
        objFW.Write("</tr>")
        objFW.Write("</table>")
        objFW.Write("<div class=""brResults"">")
        objFW.Write("<h2>Results<h2>")
        objFW.Write("<table cellspacing=""0"" cellpadding=""0"" border=""0"">")
        objFW.Write("<tr>")
        objFW.Write("<th>1st Place</th>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("</tr>")
        objFW.Write("<tr>")
        objFW.Write("<th>2nd Place</th>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("</tr>")
        objFW.Write("</table>")
        objFW.Write("</div>")
        objFW.Write("<div class=""page-break""></div>")
    End Sub

    Private Sub PrintTrueDouble(ByRef r_objBracketManager As clsBracketManager, ByVal v_intMatNumber As Integer)
        Dim strPlayer3 As String
        Dim intMatch3 As Integer = 0
        objFW.Write("<h3 align=right>Mat ")
        objFW.Write(v_intMatNumber)
        objFW.Write("</h3>")
        objFW.Write("<h2 align=center>2011 Louisiana State Judo Championships</h2>")
        objFW.Write("<h1>")
        objFW.Write(r_objBracketManager.objGetCurrentBracket.DivisionName)
        objFW.Write("</h1>")
        objFW.Write("<table border=""0"" cellspacing=""0"" cellpadding=""0"" class=""tbl_bracket"">")
        objFW.Write("<tr>")
        objFW.Write("<td class=""brack_under cell_1"">")
        objFW.Write(r_objBracketManager.objGetCurrentMatch.player1)
        objFW.Write("</td>")
        objFW.Write("<td class=""cell_2"">&nbsp;</td>")
        objFW.Write("<td class=""cell_3"">&nbsp;</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("</tr>")
        objFW.Write("<tr>")
        objFW.Write("<td class=""brack_right"" align=right>")
        objFW.Write(r_objBracketManager.objGetCurrentMatch.matchNumber)
        objFW.Write("</td>")
        objFW.Write("<td class=""brack_under"">&nbsp;</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("</tr>")
        objFW.Write("<tr>")
        objFW.Write("<td class=""brack_under_right_up"">")
        objFW.Write(r_objBracketManager.objGetCurrentMatch.player2)
        objFW.Write("</td>")
        Call r_objBracketManager.NextMatch()
        strPlayer3 = r_objBracketManager.objGetCurrentMatch.player1
        Call r_objBracketManager.NextMatch()
        Call r_objBracketManager.NextMatch()
        Call r_objBracketManager.NextMatch()    'Match 2
        objFW.Write("<td class=""brack_right"" align=right>")
        objFW.Write(r_objBracketManager.objGetCurrentMatch.matchNumber)
        objFW.Write("</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("</tr>")
        objFW.Write("<tr>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("<td class=""brack_right"">&nbsp;</td>")
        objFW.Write("<td class=""brack_under"">&nbsp;</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("</tr>")
        objFW.Write("<tr>")
        objFW.Write("<td class=""brack_under"">")
        objFW.Write(strPlayer3)
        objFW.Write("</td>")
        objFW.Write("<td class=""brack_right"">&nbsp;</td>")
        Call r_objBracketManager.NextMatch()    'Match 3
        intMatch3 = r_objBracketManager.objGetCurrentMatch.matchNumber
        Call r_objBracketManager.NextMatch()    'Match 4
        objFW.Write("<td class=""brack_right"" align=right>")
        objFW.Write(r_objBracketManager.objGetCurrentMatch.matchNumber)
        objFW.Write("</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("</tr>")
        objFW.Write("<tr>")
        objFW.Write("<td class=""brack_right"" align=right>&nbsp;</td>")
        objFW.Write("<td class=""brack_under_right_up"">")
        objFW.Write(strPlayer3)
        objFW.Write("</td>")
        objFW.Write("<td class=""brack_right"">&nbsp;</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("</tr>")
        objFW.Write("<tr>")
        objFW.Write("<td class=""brack_under_right_up"">bye</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("<td class=""brack_right"">&nbsp;</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("</tr>")
        objFW.Write("<tr>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("<td class=""brack_right"">&nbsp;</td>")
        objFW.Write("<td class=""brack_under"">&nbsp;</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("</tr>")
        objFW.Write("<tr>")
        objFW.Write("<td class=""brack_under cell_1"">&nbsp;</td>")
        objFW.Write("<td class=""cell_2"">&nbsp;</td>")
        objFW.Write("<td class=""brack_right"">&nbsp;</td>")
        Call r_objBracketManager.NextMatch()    'Match 5
        objFW.Write("<td class=""brack_right"" align=right>")
        objFW.Write(r_objBracketManager.objGetCurrentMatch.matchNumber)
        objFW.Write("*</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("</tr>")
        objFW.Write("<tr>")
        objFW.Write("<td class=""brack_right"" align=right>&nbsp;</td>")
        objFW.Write("<td class=""brack_under"">&nbsp;</td>")
        objFW.Write("<td class=""brack_right"">&nbsp;</td>")
        objFW.Write("<td class=""brack_right"">&nbsp;</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("</tr>")
        objFW.Write("<tr>")
        objFW.Write("<td class=""brack_under_right_up"">bye</td>")
        objFW.Write("<td class=""brack_right"" align=right>")
        objFW.Write(intMatch3)
        objFW.Write("</td>")
        objFW.Write("<td class=""brack_right"">&nbsp;</td>")
        objFW.Write("<td class=""brack_right"">&nbsp;</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("</tr>")
        objFW.Write("<tr>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("<td class=""brack_right"">&nbsp;</td>")
        objFW.Write("<td class=""brack_under_right_up"">&nbsp;</td>")
        objFW.Write("<td class=""brack_right"">&nbsp;</td>")
        objFW.Write("<td class=""brack_under"">&nbsp;</td>")
        objFW.Write("</tr>")
        objFW.Write("<tr>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("<td class=""brack_right"">&nbsp;</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("<td class=""brack_right"">&nbsp;</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("</tr>")
        objFW.Write("<tr>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("<td class=""brack_under_right_up"">&nbsp;</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("<td class=""brack_right"">&nbsp;</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("</tr>")
        objFW.Write("<tr>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("<td class=""brack_right"">&nbsp;</td>")
        objFW.Write("</tr>")
        objFW.Write("<tr>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("<td class=""brack_under_right_up"">&nbsp;</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("</tr>")
        objFW.Write("</table>")
        objFW.Write("<table class=""foo"">")
        objFW.Write("<tr>")
        objFW.Write("<td><h3>* &nbsp;not necessary if winner of last match is from winner's bracket</h3></td>")
        objFW.Write("</tr>")
        objFW.Write("</table>")
        objFW.Write("<div class=""brResults"">")
        objFW.Write("<h2>Results<h2>")
        objFW.Write("<table cellspacing=""0"" cellpadding=""0"" border=""0"">")
        objFW.Write("<tr>")
        objFW.Write("<th>1st Place</th>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("</tr>")
        objFW.Write("<tr>")
        objFW.Write("<th>2nd Place</th>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("</tr>")
        objFW.Write("<tr>")
        objFW.Write("<th>3rd Place</th>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("</tr>")
        objFW.Write("</table>")
        objFW.Write("</div>")
        objFW.Write("<div class=""page-break""></div>")
    End Sub

    Private Sub Print4Player(ByRef r_objBracketManager As clsBracketManager)
        Dim intMatch2 As Integer
        Dim strPlayer3 As String
        Dim strPlayer4 As String

        objFW.Write("<table border=""0"" cellspacing=""0"" cellpadding=""0"" class=""tbl_bracket"">")
        objFW.Write("<tr>")
        objFW.Write("<td class=""brack_under cell_1"">")
        objFW.Write(r_objBracketManager.objGetCurrentMatch.player1)
        objFW.Write("</td>")
        objFW.Write("<td class=""cell_2"">&nbsp;</td>")
        objFW.Write("<td class=""cell_3"">&nbsp;</td>")
        objFW.Write("</tr>")
        objFW.Write("<tr>")
        objFW.Write("<td class=""brack_right"" align=right>")
        objFW.Write(strMatchNumOrBye(r_objBracketManager.objGetCurrentMatch.matchNumber))
        objFW.Write("</td>")
        objFW.Write("<td class=""brack_under"">&nbsp;</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("</tr>")
        objFW.Write("<tr>")
        objFW.Write("<td class=""brack_under_right_up"">")
        objFW.Write(r_objBracketManager.objGetCurrentMatch.player2)
        objFW.Write("</td>")
        Call r_objBracketManager.NextMatch()    'Match 2
        intMatch2 = r_objBracketManager.objGetCurrentMatch.matchNumber
        strPlayer3 = r_objBracketManager.objGetCurrentMatch.player1
        strPlayer4 = r_objBracketManager.objGetCurrentMatch.player2
        Call r_objBracketManager.NextMatch()    'Match 3
        objFW.Write("<td class=""brack_right"" align=right>")
        objFW.Write(strMatchNumOrBye(r_objBracketManager.objGetCurrentMatch.matchNumber))
        objFW.Write("</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("</tr>")
        objFW.Write("<tr>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("<td class=""brack_right"">&nbsp;</td>")
        objFW.Write("<td class=""brack_under"">&nbsp;</td>")
        objFW.Write("</tr>")
        objFW.Write("<tr>")
        objFW.Write("<td class=""brack_under"">")
        objFW.Write(strPlayer3)
        objFW.Write("</td>")
        objFW.Write("<td class=""brack_right"">&nbsp;</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("</tr>")
        objFW.Write("<tr>")
        objFW.Write("<td class=""brack_right"" align=right>")
        objFW.Write(strMatchNumOrBye(intMatch2))
        objFW.Write("</td>")
        objFW.Write("<td class=""brack_under_right_up"">&nbsp;</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("</tr>")
        objFW.Write("<tr>")
        objFW.Write("<td class=""brack_under_right_up"">")
        objFW.Write(strPlayer4)
        objFW.Write("</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("</tr>")
        objFW.Write("</table>")

    End Sub

    Private Sub Print4Header(ByRef r_objBracketManager As clsBracketManager, ByVal v_intMatNumber As Integer)
        objFW.Write("<h3 align=right>Mat ")
        objFW.Write(v_intMatNumber)
        objFW.Write("</h3>")
        objFW.Write("<h2 align=center>2011 Louisiana State Judo Championships</h2>")
        objFW.Write("<h1>")
        objFW.Write(r_objBracketManager.objGetCurrentBracket.DivisionName)
        objFW.Write("</h1>")
    End Sub

    Private Sub Print8Header(ByRef r_objBracketManager As clsBracketManager, ByVal v_intMatNumber As Integer)
        objFW.Write("<h3 align=right>Mat ")
        objFW.Write(v_intMatNumber)
        objFW.Write("</h3>")
        objFW.Write("<h2 align=center>2011 Louisiana State Judo Championships</h2>")
        objFW.Write("<h1>")
        objFW.Write(r_objBracketManager.objGetCurrentBracket.DivisionName)
        objFW.Write("</h1>")
    End Sub

    Private Sub Print8player(ByRef r_objBracketManager As clsBracketManager)
        Dim intMatch2 As Integer
        Dim intMatch3 As Integer
        Dim intMatch4 As Integer
        Dim intMatch6 As Integer

        Dim strPlayer3 As String
        Dim strPlayer4 As String
        Dim strPlayer5 As String
        Dim strPlayer6 As String
        Dim strPlayer7 As String
        Dim strPlayer8 As String

        objFW.Write("<table border=""0"" cellspacing=""0"" cellpadding=""0"" class=""tbl_bracket tbl8"">")
        objFW.Write("<tr>")
        objFW.Write("<td class=""brack_under cell_1"">")
        objFW.Write(r_objBracketManager.objGetCurrentMatch.player1)
        objFW.Write("</td>")
        objFW.Write("<td class=""cell_2"">&nbsp;</td>")
        objFW.Write("<td class=""cell_3"">&nbsp;</td>")
        objFW.Write("<td class=""cell_4"">&nbsp;</td>")
        objFW.Write("</tr>")
        objFW.Write("<tr>")
        objFW.Write("<td class=""brack_right""align=right>")
        objFW.Write(strMatchNumOrBye(r_objBracketManager.objGetCurrentMatch.matchNumber))
        objFW.Write("</td>")
        objFW.Write("<td class=""brack_under"">&nbsp;</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("</tr>")
        objFW.Write("<tr>")
        objFW.Write("<td class=""brack_under_right_up"">")
        objFW.Write(r_objBracketManager.objGetCurrentMatch.player2)
        objFW.Write("</td>")

        Call r_objBracketManager.NextMatch()    'Match 2
        strPlayer3 = r_objBracketManager.objGetCurrentMatch.player1
        strPlayer4 = r_objBracketManager.objGetCurrentMatch.player2
        intMatch2 = r_objBracketManager.objGetCurrentMatch.matchNumber

        Call r_objBracketManager.NextMatch()    'Match 3
        strPlayer5 = r_objBracketManager.objGetCurrentMatch.player1
        strPlayer6 = r_objBracketManager.objGetCurrentMatch.player2
        intMatch3 = r_objBracketManager.objGetCurrentMatch.matchNumber

        Call r_objBracketManager.NextMatch()    'Match 4
        strPlayer7 = r_objBracketManager.objGetCurrentMatch.player1
        strPlayer8 = r_objBracketManager.objGetCurrentMatch.player2
        intMatch4 = r_objBracketManager.objGetCurrentMatch.matchNumber

        Call r_objBracketManager.NextMatch()    'Match 5
        objFW.Write("<td class=""brack_right"" align=right>")
        objFW.Write(strMatchNumOrBye(r_objBracketManager.objGetCurrentMatch.matchNumber))
        objFW.Write("</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("</tr>")
        objFW.Write("<tr>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("<td class=""brack_right"">&nbsp;</td>")
        objFW.Write("<td class=""brack_under"">&nbsp;</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("</tr>")
        objFW.Write("<tr>")
        objFW.Write("<td class=""brack_under"">")
        objFW.Write(strPlayer3)
        objFW.Write("</td>")
        objFW.Write("<td class=""brack_right"">&nbsp;</td>")

        Call r_objBracketManager.NextMatch()    'Match 6
        intMatch6 = r_objBracketManager.objGetCurrentMatch.matchNumber

        Call r_objBracketManager.NextMatch()    'Match 7

        objFW.Write("<td class=""brack_right"" align=right>")
        objFW.Write(r_objBracketManager.objGetCurrentMatch.matchNumber)
        objFW.Write("</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("</tr>")
        objFW.Write("<tr>")
        objFW.Write("<td class=""brack_right"" align=right>")
        objFW.Write(strMatchNumOrBye(intMatch2))
        objFW.Write("</td>")
        objFW.Write("<td class=""brack_under_right_up"">&nbsp;</td>")
        objFW.Write("<td class=""brack_right"">&nbsp;</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("</tr>")
        objFW.Write("<tr>")
        objFW.Write("<td class=""brack_under_right_up"">")
        objFW.Write(strPlayer4)
        objFW.Write("</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("<td class=""brack_right"">&nbsp;</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("</tr>")
        objFW.Write("<tr>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("<td class=""brack_right"">&nbsp;</td>")
        objFW.Write("<td class=""brack_under"">&nbsp;</td>")
        objFW.Write("</tr>")
        objFW.Write("<tr>")
        objFW.Write("<td class=""brack_under"">")
        objFW.Write(strPlayer5)
        objFW.Write("</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("<td class=""brack_right"">&nbsp;</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("</tr>")
        objFW.Write("<tr>")
        objFW.Write("<td class=""brack_right"" align=right>")
        objFW.Write(strMatchNumOrBye(intMatch3))
        objFW.Write("</td>")
        objFW.Write("<td class=""brack_under"">&nbsp;</td>")
        objFW.Write("<td class=""brack_right"" >&nbsp;</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("</tr>")
        objFW.Write("<tr>")
        objFW.Write("<td class=""brack_under_right_up"">")
        objFW.Write(strPlayer6)
        objFW.Write("</td>")
        objFW.Write("<td class=""brack_right"" align=right>")
        objFW.Write(intMatch6)
        objFW.Write("</td>")
        objFW.Write("<td class=""brack_right"" >&nbsp;</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("</tr>")
        objFW.Write("<tr>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("<td class=""brack_right"">&nbsp;</td>")
        objFW.Write("<td class=""brack_under_right_up"">&nbsp;</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("</tr>")
        objFW.Write("<tr>")
        objFW.Write("<td class=""brack_under"">")
        objFW.Write(strPlayer7)
        objFW.Write("</td>")
        objFW.Write("<td class=""brack_right"">&nbsp;</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("</tr>")
        objFW.Write("<tr>")
        objFW.Write("<td class=""brack_right"" align=right>")
        objFW.Write(strMatchNumOrBye(intMatch4))
        objFW.Write("</td>")
        objFW.Write("<td class=""brack_under_right_up"">&nbsp;</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("</tr>")
        objFW.Write("<tr>")
        objFW.Write("<td class=""brack_under_right_up"">")
        objFW.Write(strPlayer8)
        objFW.Write("</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("</tr>")
        objFW.Write("</table>")
    End Sub

    Private Sub Print8Footer()
        objFW.Write("<div class=""page-break""></div>")
    End Sub

    Private Sub Print16player(ByVal r_objBracketManager As clsBracketManager)
        Dim intMatch1 As Integer
        Dim intMatch2 As Integer
        Dim intMatch3 As Integer
        Dim intMatch4 As Integer
        Dim intMatch5 As Integer
        Dim intMatch6 As Integer
        Dim intMatch7 As Integer
        Dim intMatch8 As Integer
        Dim intMatch9 As Integer
        Dim intMatch10 As Integer
        Dim intMatch11 As Integer
        Dim intMatch12 As Integer
        Dim intMatch13 As Integer
        Dim intMatch14 As Integer
        Dim intMatch15 As Integer

        Dim strName1 As String
        Dim strName2 As String
        Dim strName3 As String
        Dim strName4 As String
        Dim strName5 As String
        Dim strName6 As String
        Dim strName7 As String
        Dim strName8 As String
        Dim strName9 As String
        Dim strName10 As String
        Dim strName11 As String
        Dim strName12 As String
        Dim strName13 As String
        Dim strName14 As String
        Dim strName15 As String
        Dim strName16 As String

        '                                       'Match 1
        strName1 = r_objBracketManager.objGetCurrentMatch.player1
        strName2 = r_objBracketManager.objGetCurrentMatch.player2
        intMatch1 = r_objBracketManager.objGetCurrentMatch.matchNumber

        Call r_objBracketManager.NextMatch()    'Match 2
        strName3 = r_objBracketManager.objGetCurrentMatch.player1
        strName4 = r_objBracketManager.objGetCurrentMatch.player2
        intMatch2 = r_objBracketManager.objGetCurrentMatch.matchNumber

        Call r_objBracketManager.NextMatch()    'Match 3
        strName5 = r_objBracketManager.objGetCurrentMatch.player1
        strName6 = r_objBracketManager.objGetCurrentMatch.player2
        intMatch3 = r_objBracketManager.objGetCurrentMatch.matchNumber

        Call r_objBracketManager.NextMatch()    'Match 4
        strName7 = r_objBracketManager.objGetCurrentMatch.player1
        strName8 = r_objBracketManager.objGetCurrentMatch.player2
        intMatch4 = r_objBracketManager.objGetCurrentMatch.matchNumber

        Call r_objBracketManager.NextMatch()    'Match 5
        strName9 = r_objBracketManager.objGetCurrentMatch.player1
        strName10 = r_objBracketManager.objGetCurrentMatch.player2
        intMatch5 = r_objBracketManager.objGetCurrentMatch.matchNumber

        Call r_objBracketManager.NextMatch()    'Match 6
        strName11 = r_objBracketManager.objGetCurrentMatch.player1
        strName12 = r_objBracketManager.objGetCurrentMatch.player2
        intMatch6 = r_objBracketManager.objGetCurrentMatch.matchNumber

        Call r_objBracketManager.NextMatch()    'Match 7
        strName13 = r_objBracketManager.objGetCurrentMatch.player1
        strName14 = r_objBracketManager.objGetCurrentMatch.player2
        intMatch7 = r_objBracketManager.objGetCurrentMatch.matchNumber

        Call r_objBracketManager.NextMatch()    'Match 8
        strName15 = r_objBracketManager.objGetCurrentMatch.player1
        strName16 = r_objBracketManager.objGetCurrentMatch.player2
        intMatch8 = r_objBracketManager.objGetCurrentMatch.matchNumber

        Call r_objBracketManager.NextMatch()
        intMatch9 = r_objBracketManager.objGetCurrentMatch.matchNumber

        Call r_objBracketManager.NextMatch()
        intMatch10 = r_objBracketManager.objGetCurrentMatch.matchNumber

        Call r_objBracketManager.NextMatch()
        intMatch11 = r_objBracketManager.objGetCurrentMatch.matchNumber

        Call r_objBracketManager.NextMatch()
        intMatch12 = r_objBracketManager.objGetCurrentMatch.matchNumber

        Call r_objBracketManager.NextMatch()
        intMatch13 = r_objBracketManager.objGetCurrentMatch.matchNumber

        Call r_objBracketManager.NextMatch()
        intMatch14 = r_objBracketManager.objGetCurrentMatch.matchNumber

        Call r_objBracketManager.NextMatch()
        intMatch15 = r_objBracketManager.objGetCurrentMatch.matchNumber

        objFW.Write("<table border=""0"" cellspacing=""0"" cellpadding=""0"" class=""tbl_bracket tbl8"">")
        objFW.Write("<tr>")
        objFW.Write("<td class=""brack_under cell_1"">")
        objFW.Write(strName1)
        objFW.Write("</td>")
        objFW.Write("<td class=""cell_2"">&nbsp;</td>")
        objFW.Write("<td class=""cell_3"">&nbsp;</td>")
        objFW.Write("<td class=""cell_4"">&nbsp;</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("</tr>")
        objFW.Write("<tr>")
        objFW.Write("<td class=""brack_right""align=right>")
        objFW.Write(strMatchNumOrBye(intMatch1))
        objFW.Write("</td>")
        objFW.Write("<td class=""brack_under"">&nbsp;</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("</tr>")
        objFW.Write("<tr>")
        objFW.Write("<td class=""brack_under_right_up"">")
        objFW.Write(strName2)
        objFW.Write("</td>")
        objFW.Write("<td class=""brack_right"" align=right>")
        objFW.Write(strMatchNumOrBye(intMatch9))
        objFW.Write("</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("</tr>")
        objFW.Write("<tr>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("<td class=""brack_right"">&nbsp;</td>")
        objFW.Write("<td class=""brack_under"">&nbsp;</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("</tr>")
        objFW.Write("<tr>")
        objFW.Write("<td class=""brack_under"">")
        objFW.Write(strName3)
        objFW.Write("</td>")
        objFW.Write("<td class=""brack_right"">&nbsp;</td>")
        objFW.Write("<td class=""brack_right"" align=right>")
        objFW.Write(strMatchNumOrBye(intMatch13))
        objFW.Write("</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("</tr>")
        objFW.Write("<tr>")
        objFW.Write("<td class=""brack_right"" align=right>")
        objFW.Write(strMatchNumOrBye(intMatch2))
        objFW.Write("</td>")
        objFW.Write("<td class=""brack_under_right_up"">&nbsp;</td>")
        objFW.Write("<td class=""brack_right"">&nbsp;</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("</tr>")
        objFW.Write("<tr>")
        objFW.Write("<td class=""brack_under_right_up"">")
        objFW.Write(strName4)
        objFW.Write("</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("<td class=""brack_right"">&nbsp;</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("</tr>")
        objFW.Write("<tr>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("<td class=""brack_right"">&nbsp;</td>")
        objFW.Write("<td class=""brack_under"">&nbsp;</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("</tr>")
        objFW.Write("<tr>")
        objFW.Write("<td class=""brack_under"">")
        objFW.Write(strName5)
        objFW.Write("</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("<td class=""brack_right"">&nbsp;</td>")
        objFW.Write("<td class=""brack_right"" align=right>")
        objFW.Write(strMatchNumOrBye(intMatch15))
        objFW.Write("</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("</tr>")
        objFW.Write("<tr>")
        objFW.Write("<td class=""brack_right"" align=right>")
        objFW.Write(strMatchNumOrBye(intMatch3))
        objFW.Write("</td>")
        objFW.Write("<td class=""brack_under"">&nbsp;</td>")
        objFW.Write("<td class=""brack_right"" >&nbsp;</td>")
        objFW.Write("<td class=""brack_right"" >&nbsp;</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("</tr>")
        objFW.Write("<tr>")
        objFW.Write("<td class=""brack_under_right_up"">")
        objFW.Write(strName6)
        objFW.Write("</td>")
        objFW.Write("<td class=""brack_right"" align=right>")
        objFW.Write(strMatchNumOrBye(intMatch10))
        objFW.Write("</td>")
        objFW.Write("<td class=""brack_right"" >&nbsp;</td>")
        objFW.Write("<td class=""brack_right"" >&nbsp;</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("</tr>")
        objFW.Write("<tr>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("<td class=""brack_right"">&nbsp;</td>")
        objFW.Write("<td class=""brack_under_right_up"">&nbsp;</td>")
        objFW.Write("<td class=""brack_right"" >&nbsp;</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("</tr>")
        objFW.Write("<tr>")
        objFW.Write("<td class=""brack_under"">")
        objFW.Write(strName7)
        objFW.Write("</td>")
        objFW.Write("<td class=""brack_right"">&nbsp;</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("<td class=""brack_right"" >&nbsp;</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("</tr>")
        objFW.Write("<tr>")
        objFW.Write("<td class=""brack_right"" align=right>")
        objFW.Write(strMatchNumOrBye(intMatch4))
        objFW.Write("</td>")
        objFW.Write("<td class=""brack_under_right_up"">&nbsp;</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("<td class=""brack_right"" >&nbsp;</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("</tr>")
        objFW.Write("<tr>")
        objFW.Write("<td class=""brack_under_right_up"">")
        objFW.Write(strName8)
        objFW.Write("</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("<td class=""brack_right"" >&nbsp;</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("</tr>")
        objFW.Write("<tr>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("<td class=""brack_right"" >&nbsp;</td>")
        objFW.Write("<td class=""brack_under"">&nbsp;</td>")
        objFW.Write("</tr>")
        objFW.Write("<tr>")
        objFW.Write("<td class=""brack_under cell_1"">")
        objFW.Write(strName9)
        objFW.Write("</td>")
        objFW.Write("<td class=""cell_2"">&nbsp;</td>")
        objFW.Write("<td class=""cell_3"">&nbsp;</td>")
        objFW.Write("<td class=""brack_right"" >&nbsp;</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("</tr>")
        objFW.Write("<tr>")
        objFW.Write("<td class=""brack_right""align=right>")
        objFW.Write(strMatchNumOrBye(intMatch5))
        objFW.Write("</td>")
        objFW.Write("<td class=""brack_under"">&nbsp;</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("<td class=""brack_right"" >&nbsp;</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("</tr>")
        objFW.Write("<tr>")
        objFW.Write("<td class=""brack_under_right_up"">")
        objFW.Write(strName10)
        objFW.Write("</td>")
        objFW.Write("<td class=""brack_right"" align=right>")
        objFW.Write(strMatchNumOrBye(intMatch11))
        objFW.Write("</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("<td class=""brack_right"" >&nbsp;</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("</tr>")
        objFW.Write("<tr>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("<td class=""brack_right"">&nbsp;</td>")
        objFW.Write("<td class=""brack_under"">&nbsp;</td>")
        objFW.Write("<td class=""brack_right"" >&nbsp;</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("</tr>")
        objFW.Write("<tr>")
        objFW.Write("<td class=""brack_under"">")
        objFW.Write(strName11)
        objFW.Write("</td>")
        objFW.Write("<td class=""brack_right"">&nbsp;</td>")
        objFW.Write("<td class=""brack_right"" align=right>")
        objFW.Write(strMatchNumOrBye(intMatch14))
        objFW.Write("</td>")
        objFW.Write("<td class=""brack_right"" >&nbsp;</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("</tr>")
        objFW.Write("<tr>")
        objFW.Write("<td class=""brack_right"" align=right>")
        objFW.Write(strMatchNumOrBye(intMatch6))
        objFW.Write("</td>")
        objFW.Write("<td class=""brack_under_right_up"">&nbsp;</td>")
        objFW.Write("<td class=""brack_right"">&nbsp;</td>")
        objFW.Write("<td class=""brack_right"" >&nbsp;</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("</tr>")
        objFW.Write("<tr>")
        objFW.Write("<td class=""brack_under_right_up"">")
        objFW.Write(strName12)
        objFW.Write("</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("<td class=""brack_right"">&nbsp;</td>")
        objFW.Write("<td class=""brack_right"" >&nbsp;</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("</tr>")
        objFW.Write("<tr>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("<td class=""brack_right"">&nbsp;</td>")
        objFW.Write("<td class=""brack_under_right_up"">&nbsp;</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("</tr>")
        objFW.Write("<tr>")
        objFW.Write("<td class=""brack_under"">")
        objFW.Write(strName13)
        objFW.Write("</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("<td class=""brack_right"">&nbsp;</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("</tr>")
        objFW.Write("<tr>")
        objFW.Write("<td class=""brack_right"" align=right>")
        objFW.Write(strMatchNumOrBye(intMatch7))
        objFW.Write("</td>")
        objFW.Write("<td class=""brack_under"">&nbsp;</td>")
        objFW.Write("<td class=""brack_right"" >&nbsp;</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("</tr>")
        objFW.Write("<tr>")
        objFW.Write("<td class=""brack_under_right_up"">")
        objFW.Write(strName14)
        objFW.Write("</td>")
        objFW.Write("<td class=""brack_right"" align=right>")
        objFW.Write(strMatchNumOrBye(intMatch12))
        objFW.Write("</td>")
        objFW.Write("<td class=""brack_right"" >&nbsp;</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("</tr>")
        objFW.Write("<tr>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("<td class=""brack_right"">&nbsp;</td>")
        objFW.Write("<td class=""brack_under_right_up"">&nbsp;</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("</tr>")
        objFW.Write("<tr>")
        objFW.Write("<td class=""brack_under"">")
        objFW.Write(strName15)
        objFW.Write("</td>")
        objFW.Write("<td class=""brack_right"">&nbsp;</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("</tr>")
        objFW.Write("<tr>")
        objFW.Write("<td class=""brack_right"" align=right>")
        objFW.Write(strMatchNumOrBye(intMatch8))
        objFW.Write("</td>")
        objFW.Write("<td class=""brack_under_right_up"">&nbsp;</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("</tr>")
        objFW.Write("<tr>")
        objFW.Write("<td class=""brack_under_right_up"">")
        objFW.Write(strName16)
        objFW.Write("</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("</tr>")
        objFW.Write("</table>")
    End Sub

    Private Sub PrintResultFooter()
        objFW.Write("<div class=""brResults"">")
        objFW.Write("<h2>Results<h2>")
        objFW.Write("<table cellspacing=""0"" cellpadding=""0"" border=""0"">")
        objFW.Write("<tr>")
        objFW.Write("<th>1st Place</th>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("</tr>")
        objFW.Write("<tr>")
        objFW.Write("<th>2nd Place</th>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("</tr>")
        objFW.Write("<tr>")
        objFW.Write("<th>3rd Place</th>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("</tr>")
        objFW.Write("</table>")
        objFW.Write("</div>")
        objFW.Write("<div class=""page-break""></div>")
    End Sub

    Private Sub PrintFoot()
        objFW.Write("</div>")
        objFW.Write("</body>")
        objFW.Write("</html>")
    End Sub

    Private Sub PrintSpacing()
        objFW.Write("<table>")
        objFW.Write("<tr><td>&nbsp;</td></tr>")
        objFW.Write("<tr><td>&nbsp;</td></tr>")
        objFW.Write("<tr><td>&nbsp;</td></tr>")
        objFW.Write("</table>")
    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub
End Class
