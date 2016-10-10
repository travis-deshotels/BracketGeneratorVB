Public Class clsPrint
    Private objBracketManager As clsBracketManager
    Private objFW As IO.StreamWriter

    Public Sub New(ByVal v_objBracketMananger As clsBracketManager)
        objBracketManager = v_objBracketMananger
        objBracketManager.MarkBracketsReady()
        objFW = New IO.StreamWriter(System.Environment.CurrentDirectory & "\test.htm")
    End Sub

    Public Sub PrintAllBrackets()
        Call objBracketManager.FirstBracket()
        Call PrintHead()

        While Not objBracketManager.blnLastBracket
            Select Case objBracketManager.intGetCurrentBracketSize
                Case 4
                    Call Print4Player()
                    If objBracketManager.objGetCurrentBracket.BracketType = ce_BracketType.DoubleEWinner Then
                        Call objBracketManager.NextBracket()
                        Call PrintSpacing()
                        Call Print4Player()
                    End If
                Case Else
                    Exit Sub

            End Select

            Call objBracketManager.NextBracket()
        End While

        Call PrintFoot()
        Call objBracketManager.MarkBracketsReady()
    End Sub

    Private Sub PrintHead()
        objFW.Write("<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.01//EN"" ""http://www.w3.org/TR/html4/strict.dtd"">")
        objFW.Write("<html lang='en'>")
        objFW.Write("<head>")
        objFW.Write("<meta http-equiv='Content-Type' content='text/html; charset=ISO-8859-1'>")
        objFW.Write("<title>Page Title</title>")
        objFW.Write("<!-- link rel='stylesheet' href='style.css' type='text/css' -->")
        objFW.Write("<style>")
        objFW.Write(".tbl_bracket {")
        objFW.Write("width:1500px;")
        objFW.Write("}")
        objFW.Write(".tbl_bracket td {")
        objFW.Write("width:475px;")
        objFW.Write("}")
        objFW.Write(".tbl_bracket .brack_under {")
        objFW.Write("border-bottom:5px solid #111314;")
        objFW.Write("}")
        objFW.Write(".tbl_bracket .brack_under_right_up {")
        objFW.Write("border-bottom:5px solid #111314;")
        objFW.Write("border-right:5px solid #111314;")
        objFW.Write("}")
        objFW.Write(".tbl_bracket .brack_right {")
        objFW.Write("border-right:5px solid #111314;")
        objFW.Write("}")

        objFW.Write("</style>")
        objFW.Write("</head>")
        objFW.Write("<body>")
    End Sub

    Private Function strMatchNumOrBye(ByVal v_intMatchNumber As Integer) As String
        If v_intMatchNumber > 0 Then
            Return CStr(v_intMatchNumber)
        Else
            Return "&nbsp;"
        End If
    End Function

    Private Sub Print4Player()
        Dim intMatchNumber As Integer = objBracketManager.objGetCurrentMatch.matchNumber
        Dim objTempMatch As clsMatch
        'division name
        objFW.Write("<table border=""0"" cellspacing=""0"" cellpadding=""0"" class=""tbl_bracket"">")
        objFW.Write("<tr><td>" & objBracketManager.objGetCurrentBracket.DivisionName & "</td></tr>")
        objFW.Write("<tr><td>&nbsp;</td></tr><tr><td>&nbsp;</td></tr>")

        'player 1
        objFW.Write("<tr>")
        objFW.Write("<td class=""brack_under cell_1"">")
        objFW.Write(objBracketManager.objGetCurrentMatch.player1)
        objFW.Write("</td>")
        objFW.Write("<td class=""cell_2"">&nbsp;</td>")
        objFW.Write("<td class=""cell_3"">&nbsp;</td>")
        objFW.Write("<td class=""cell_4"">&nbsp;</td>")
        objFW.Write("<td class=""cell_5"">&nbsp;</td>")
        objFW.Write("<td class=""cell_6"">&nbsp;</td>")
        objFW.Write("</tr>")

        'first match number
        objFW.Write("<tr>")
        objFW.Write("<td class=""brack_right"" align=right>")
        objFW.Write(strMatchNumOrBye(intMatchNumber))
        objFW.Write("</td>")
        objFW.Write("<td class=""brack_under"">&nbsp;</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("</tr>")

        'player 2
        objFW.Write("<tr>")
        objFW.Write("<td class=""brack_under_right_up"">")
        objFW.Write(objBracketManager.objGetCurrentMatch.player2)
        objFW.Write("</td>")
        objFW.Write("<td class=""brack_right"" align=right>")

        'third match number
        Call objBracketManager.NextMatch()
        objTempMatch = objBracketManager.objGetCurrentMatch
        Call objBracketManager.NextMatch()
        intMatchNumber = objBracketManager.objGetCurrentMatch.matchNumber
        objFW.Write(strMatchNumOrBye(intMatchNumber))
        objFW.Write("</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("</tr>")

        'spacing
        objFW.Write("<tr>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("<td class=""brack_right"">&nbsp;</td>")
        objFW.Write("<td class=""brack_under"">&nbsp;</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("</tr>")

        'player 3
        objFW.Write("<tr>")
        objFW.Write("<td class=""brack_under"">")
        objFW.Write(objTempMatch.player1)
        objFW.Write("</td>")
        objFW.Write("<td class=""brack_right"">&nbsp;</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("</tr>")

        'second match number
        objFW.Write("<tr>")
        objFW.Write("<td class=""brack_right"" align=right>")
        intMatchNumber = objTempMatch.matchNumber
        objFW.Write(strMatchNumOrBye(intMatchNumber))
        objFW.Write("</td>")
        objFW.Write("<td class=""brack_under_right_up"">&nbsp;</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("</tr>")

        'player 4
        objFW.Write("<tr>")
        objFW.Write("<td class=""brack_under_right_up"">")
        objFW.Write(objTempMatch.player2)
        objFW.Write("</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("<td>&nbsp;</td>")
        objFW.Write("</tr>")
        objFW.Write("</table>")
    End Sub

    Private Sub PrintFoot()
        objFW.Write("</body></html>")
        objFW.Close()
    End Sub

    Private Sub PrintSpacing()
        objFW.Write("<table>")
        objFW.Write("<tr><td>&nbsp;</td></tr>")
        objFW.Write("<tr><td>&nbsp;</td></tr>")
        objFW.Write("<tr><td>&nbsp;</td></tr>")
        objFW.Write("</table>")
    End Sub

    Protected Overrides Sub Finalize()
        objBracketManager = Nothing
        MyBase.Finalize()
    End Sub
End Class
