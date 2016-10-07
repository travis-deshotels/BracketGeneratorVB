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

Public Class clsMatch
    Private strPlayer1 As String
    Private strPlayer2 As String
    Private intBracketOrder As Integer
    Private intMatchNumber As Integer

    Property player1() As String
        Get
            Return strPlayer1
        End Get
        Set(ByVal value As String)
            strPlayer1 = value
        End Set
    End Property

    Property player2() As String
        Get
            Return strPlayer2
        End Get
        Set(ByVal value As String)
            strPlayer2 = value
        End Set
    End Property

    Property bracketOrder() As Integer
        Get
            Return intBracketOrder
        End Get
        Set(ByVal value As Integer)
            intBracketOrder = value
        End Set
    End Property

    Property matchNumber() As Integer
        Get
            Return intMatchNumber
        End Get
        Set(ByVal value As Integer)
            intMatchNumber = value
        End Set
    End Property

End Class
