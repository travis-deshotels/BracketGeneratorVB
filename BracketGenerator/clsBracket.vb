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

Public Enum ce_BracketType
    SingleE = 0
    DoubleEWinner = 1
    DoubleELoser = 2
End Enum

Public Class clsBracket
    Private strDivisionName As String
    Private e_Type As ce_BracketType
    Friend a_objMatches As ArrayList
    Private intBracketOrder As Integer
    'Private intPlayerCount As Integer

    Public Sub New()
        a_objMatches = New ArrayList
        'intPlayerCount = 0
    End Sub

    Property DivisionName() As String
        Get
            Return strDivisionName
        End Get
        Set(ByVal value As String)
            strDivisionName = value
        End Set
    End Property

    Property BracketType() As ce_BracketType
        Get
            Return e_Type
        End Get
        Set(ByVal value As ce_BracketType)
            e_Type = value
        End Set
    End Property

    Property BracketOrder() As Integer
        Get
            Return intBracketOrder
        End Get
        Set(ByVal value As Integer)
            intBracketOrder = value
        End Set
    End Property

    'Property PlayerCount() As Integer
    '    Get
    '        Return intPlayerCount
    '    End Get
    '    Set(ByVal value As Integer)
    '        intPlayerCount = value
    '    End Set
    'End Property

End Class
