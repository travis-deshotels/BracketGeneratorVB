' Copyright (C) 2016 Travis Deshotels
'
' This program Is free software: you can redistribute it And/Or modify
' it under the terms of the GNU General Public License as published by
' the Free Software Foundation, either version 3 of the License, Or
' (at your option) any later version.
'
' This program Is distributed in the hope that it will be useful,
' but WITHOUT ANY WARRANTY; without even the implied warranty of
' MERCHANTABILITY Or FITNESS FOR A PARTICULAR PURPOSE.  See the
' GNU General Public License for more details.
'
' You should have received a copy of the GNU General Public License
' along with this program.  If Not, see <http://www.gnu.org/licenses/>.

'travis.deshotels@gmail.com

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
