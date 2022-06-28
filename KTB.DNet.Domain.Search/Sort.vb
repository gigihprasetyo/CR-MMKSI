#Region "Code Disclaimer"
'Copyright PT. Puspa Intimedia Internusa (Intimedia) 2005

'Intimedia grants you the right to use and modify the code in this Persistence Framework
'(code under the Framework Namespace) but 
'(i) only for the solutions that are developed by Intimedia for you 
'(ii) or in solutions that are developed in join development between you and Intimedia.

'All rights not expressly granted, are reserved.
#End Region

#Region "Summary"
'///////////////////////////////////////////////////////////////////////////////////////
'// Author Name: 
'// PURPOSE       : Enter summary here after generation.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright © 2005 
'// ---------------------
'// $History      : $
'// Generated on 7/28/2005 - 8:53:00 AM
'//
'// ===========================================================================		
#End Region

#Region ".NET Base Class Namespace Imports"

Imports System
Imports System.Reflection

#End Region

Namespace KTB.DNet.Domain.Search

    Public Class Sort

        Private _SortDirection As Byte = CType(SortDirection.ASC, Byte)

        Dim m_PropertyName As String
        Dim m_ColumnName As String = String.Empty
        Dim m_DomainObjectType As Type = Nothing

        Public Sub New(ByVal DomainObjectType As Type, ByVal PropertyName As String)

            m_DomainObjectType = DomainObjectType
            m_PropertyName = PropertyName
            SetColumnName()

        End Sub

        Public Sub New(ByVal DomainObjectType As Type, ByVal PropertyName As String, ByVal SortDir As SortDirection)

            m_DomainObjectType = DomainObjectType
            m_PropertyName = PropertyName
            _SortDirection = CType(SortDir, Byte)
            SetColumnName()

        End Sub

#Region "Private Methods"

        Private Sub SetColumnName()

            Dim prop As PropertyInfo

            '//Check to see whether it is a deep search
            If m_PropertyName.IndexOf(".") <> -1 Then

                Dim Properties() As String = m_PropertyName.Split((".").ToCharArray())

                Dim dummyType As Type = m_DomainObjectType

                For i As Integer = 0 To Properties.Length - 2

                    dummyType = dummyType.GetProperty(Properties(i)).PropertyType

                Next

                prop = dummyType.GetProperty(Properties(Properties.Length - 1))

            Else

                prop = m_DomainObjectType.GetProperty(m_PropertyName)

            End If

            If Not IsNothing(prop) Then

                Dim attr As ColumnInfoAttribute = CType(Attribute.GetCustomAttribute(prop, GetType(ColumnInfoAttribute)), ColumnInfoAttribute)

                If Not IsNothing(attr) Then

                    Dim tableInfoAttr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(prop.DeclaringType, GetType(TableInfoAttribute)), TableInfoAttribute)

                    m_ColumnName = tableInfoAttr.TableName + "." + attr.ColumnName

                Else

                    Throw New SearchException(m_PropertyName + " Property of " + m_DomainObjectType.ToString() + " does not have ColumnInfoAttribute.")

                End If

            Else

                Throw New SearchException("There is no " + m_PropertyName + " Property in " + m_DomainObjectType.ToString() + ".")

            End If

        End Sub

#End Region

#Region "Public Methods"

        Public Overrides Function ToString() As String

            'return m_ColumnName & (_SortDirection = ctype(SortDirection.ASC, byte) ? " ASC" : " DESC")

            If _SortDirection = CType(SortDirection.ASC, Byte) Then
                Return m_ColumnName & " ASC"
            Else
                Return m_ColumnName & " DESC"

            End If

        End Function

        Public Function GetJoinClause() As System.Collections.Specialized.StringCollection

            Dim joinClauses As System.Collections.Specialized.StringCollection = New System.Collections.Specialized.StringCollection

            '//Check to see whether it is a deep search

            If m_PropertyName.IndexOf(".") <> -1 Then

                Dim Properties() As String = m_PropertyName.Split((".").ToCharArray())
                Dim dummyType As Type = m_DomainObjectType
                Dim prop As PropertyInfo

                For i As Integer = 0 To Properties.Length - 2

                    prop = dummyType.GetProperty(Properties(i))
                    dummyType = prop.PropertyType

                    If Not IsNothing(prop) Then

                        Dim attr As RelationInfoAttribute = CType(Attribute.GetCustomAttribute(prop, GetType(RelationInfoAttribute)), RelationInfoAttribute)

                        If Not IsNothing(attr) Then

                            Dim tableInfoAttr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(prop.DeclaringType, GetType(TableInfoAttribute)), TableInfoAttribute)

                            If (tableInfoAttr.TableName = attr.PrimaryKeyTable) Then

                                joinClauses.Add(" INNER JOIN " + attr.ForeignKeyTable + " ON " + attr.PrimaryKeyTable + "." + attr.PrimaryKeyColumn + " = " + attr.ForeignKeyTable + "." + attr.ForeignKeyColumn)

                            Else

                                joinClauses.Add(" INNER JOIN " + attr.PrimaryKeyTable + " ON " + attr.ForeignKeyTable + "." + attr.ForeignKeyColumn + " = " + attr.PrimaryKeyTable + "." + attr.PrimaryKeyColumn)

                            End If

                        Else

                            Throw New SearchException(Properties(i) + " Property of " + dummyType.ToString() + " does not have RelationInfoAttribute.")

                        End If

                    Else

                        Throw New SearchException(" There is no " & Properties(i) & " Property in " & dummyType.ToString & ".")

                    End If

                Next

            End If

            Return joinClauses

        End Function

#End Region
        Public Enum SortDirection

            ASC
            DESC

        End Enum


    End Class


End Namespace