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
'// Generated on 8/01/2005 - 3:38:00 PM
'//
'// ===========================================================================		
#End Region

#Region ".NET Base Class Namespace Imports"

Imports System
Imports System.Collections
Imports System.Reflection
Imports System.Text

#End Region

Namespace KTB.DNet.Domain.Search

    Public Class CriteriaComposite
        Implements ICriteria

        Dim sb As StringBuilder = New StringBuilder
        Dim m_Criterias As ArrayList = New ArrayList

        Private _m_IsRoot As Boolean = True

        Protected Property M_IsRoot() As Boolean
            Get
                Return _m_IsRoot
            End Get
            Set(ByVal Value As Boolean)
                _m_IsRoot = Value
            End Set
        End Property

        Public Sub New(ByVal Criteria As ICriteria)

            m_Criterias.Add(Criteria)

        End Sub

        Public Sub opAnd(ByVal Criteria As ICriteria)

            m_Criterias.Add(" AND ")

            If TypeOf Criteria Is CriteriaComposite Then

                M_IsRoot = False

            End If

            m_Criterias.Add(Criteria)

        End Sub

        Public Sub opAnd(ByVal DomainObjectType As Type, ByVal PropertyName As String, ByVal Value As Object)
            opAnd(New Criteria(DomainObjectType, PropertyName, Value))
        End Sub

        Public Sub opAnd(ByVal DomainObjectType As Type, ByVal PropertyName As String, ByVal Match As MatchType, ByVal Value As Object)
            opAnd(New Criteria(DomainObjectType, PropertyName, Match, Value))
        End Sub

        Public Sub opAnd(ByVal Criteria As ICriteria, ByVal parenthesis As String, ByVal isOpenParenthesis As Boolean)
            If isOpenParenthesis Then
                m_Criterias.Add(" AND " & parenthesis & " ")
            Else
                m_Criterias.Add(" AND ")
            End If


            If TypeOf Criteria Is CriteriaComposite Then

                M_IsRoot = False

            End If

            m_Criterias.Add(Criteria)

            If Not isOpenParenthesis Then
                m_Criterias.Add(" " & parenthesis & " ")
            End If
        End Sub

        Public Sub opOr(ByVal Criteria As ICriteria, ByVal parenthesis As String, ByVal isOpenParenthesis As Boolean)
            If isOpenParenthesis Then
                m_Criterias.Add(" OR " & parenthesis & " ")
            Else
                m_Criterias.Add(" OR ")
            End If

            If TypeOf Criteria Is CriteriaComposite Then

                M_IsRoot = False

            End If

            m_Criterias.Add(Criteria)

            If Not isOpenParenthesis Then
                m_Criterias.Add(" " & parenthesis & " ")
            End If
        End Sub

        Public Sub opOr(ByVal Criteria As ICriteria)

            m_Criterias.Add(" OR ")

            If TypeOf Criteria Is CriteriaComposite Then

                M_IsRoot = False

            End If

            m_Criterias.Add(Criteria)

        End Sub

        Public Sub opOr(ByVal DomainObjectType As Type, ByVal PropertyName As String, ByVal Value As Object)
            opOr(New Criteria(DomainObjectType, PropertyName, Value))
        End Sub

        Public Sub opOr(ByVal DomainObjectType As Type, ByVal PropertyName As String, ByVal Match As MatchType, ByVal Value As Object)
            opOr(New Criteria(DomainObjectType, PropertyName, Match, Value))
        End Sub

        Public Overrides Function ToString() As String Implements ICriteria.ToString

            sb = New StringBuilder

            '//Add WHEN and Inner Join Clause for Root Criteria Composite

            If (M_IsRoot) Then

                '//Get Join Clause From childs criteria
                sb.Append(Me.GetJoinClause())

                sb.Append(" WHERE ")

            End If

            sb.Append("(")

            For Each obj As Object In m_Criterias

                sb.Append(obj.ToString().Replace("= IsNull", "Is Null"))

            Next

            sb.Append(")")

            '//In case the m_Criterias returns nothing or "()"
            '//Return an empty string instead;

            If sb.Length = 2 Then

                Return String.Empty

            End If

            Return sb.ToString

        End Function

#Region "Protected Methods"

        Protected Function GetJoinClause() As String

            Dim sbJoin As StringBuilder = New StringBuilder

            For Each obj As Object In m_Criterias
                If (TypeOf obj Is Criteria) Then
                    If ((CType(obj, Criteria)).PropertyName.IndexOf(".") <> -1) Then

                        Dim Properties() As String = (CType(obj, Criteria)).PropertyName.Split((".").ToCharArray())
                        Dim dummyType As Type = (CType(obj, Criteria)).DomainObjectType
                        Dim prop As PropertyInfo
                        For i As Integer = 0 To Properties.Length - 2
                            prop = dummyType.GetProperty(Properties(i))
                            dummyType = prop.PropertyType
                            If Not IsNothing(prop) Then
                                Dim attr As RelationInfoAttribute = CType(Attribute.GetCustomAttribute(prop, GetType(RelationInfoAttribute)), RelationInfoAttribute)
                                If Not IsNothing(attr) Then
                                    Dim tableInfoAttr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(prop.DeclaringType, GetType(TableInfoAttribute)), TableInfoAttribute)

                                    If (tableInfoAttr.TableName = attr.PrimaryKeyTable) Then

                                        If (sbJoin.ToString().IndexOf(" INNER JOIN " + attr.ForeignKeyTable + " ON " + attr.PrimaryKeyTable + "." + attr.PrimaryKeyColumn + " = " + attr.ForeignKeyTable + "." + attr.ForeignKeyColumn) = -1) Then

                                            sbJoin.Append(" INNER JOIN " + attr.ForeignKeyTable + " ON " + attr.PrimaryKeyTable + "." + attr.PrimaryKeyColumn + " = " + attr.ForeignKeyTable + "." + attr.ForeignKeyColumn)

                                        End If

                                    Else

                                        If (sbJoin.ToString().IndexOf(" INNER JOIN " + attr.PrimaryKeyTable + " ON " + attr.ForeignKeyTable + "." + attr.ForeignKeyColumn + " = " + attr.PrimaryKeyTable + "." + attr.PrimaryKeyColumn) = -1) Then

                                            sbJoin.Append(" INNER JOIN " + attr.PrimaryKeyTable + " ON " + attr.ForeignKeyTable + "." + attr.ForeignKeyColumn + " = " + attr.PrimaryKeyTable + "." + attr.PrimaryKeyColumn)

                                        End If

                                    End If

                                Else
                                    Throw New SearchException(Properties(i) + " Property of " + dummyType.ToString() + " does not have RelationInfoAttribute.")

                                End If
                            Else
                                Throw New SearchException("There is no " + Properties(i) + " Property in " + dummyType.ToString() + ".")
                            End If
                        Next
                    End If
                ElseIf (TypeOf obj Is CriteriaComposite()) Then
                    sbJoin.Append((CType(obj, CriteriaComposite)).GetJoinClause())
                End If
            Next

            Return sbJoin.ToString

        End Function

#End Region

    End Class

    


End Namespace