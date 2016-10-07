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

Public Class clsBracket
    Private strDivisionName As String
    Private intPlayerCount As Integer
    Public a_objMatches As ArrayList

    Public Sub New()
        a_objMatches = New ArrayList
        intPlayerCount = 0
    End Sub

    Property DivisionName() As String
        Get
            Return strDivisionName
        End Get
        Set(ByVal value As String)
            strDivisionName = value
        End Set
    End Property

    Property PlayerCount() As Integer
        Get
            Return intPlayerCount
        End Get
        Set(ByVal value As Integer)
            intPlayerCount = value
        End Set
    End Property

End Class
