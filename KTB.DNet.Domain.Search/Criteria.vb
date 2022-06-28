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
'// Generated on 8/01/2005 - 2:42:00 PM
'//
'// ===========================================================================		
#End Region

#Region ".NET Base Class Namespace Imports"

Imports System
Imports System.Reflection

#End Region

#Region "Custom Namespace Imports"

'using Astra.PssHso.DataMapper.Framework;

#End Region

Namespace KTB.DNet.Domain.Search

    Public Class Criteria
        Implements ICriteria

#Region "Private Variables"

        Dim _propertyName As String
        Dim _propertyCompareName As String
        Dim _Match As MatchType = MatchType.Exact
        Dim _CompareMatch As CompareMatchType = CompareMatchType.Exact

        Dim _Value As Object = Nothing
        Dim m_ColumnName As String = String.Empty
        Dim m_CompareColumnName As String = String.Empty
        Dim m_FormatString As String = String.Empty
        Dim _DomainObjectType As Type = Nothing
        Dim isCompare As Boolean

#End Region

#Region "Constructors/Destructors/Finalizers"

        Public Sub New(ByVal DomainObjectType As Type, ByVal PropertyName As String, ByVal Value As Object)
            isCompare = False
            _propertyName = PropertyName
            _Value = Value
            _DomainObjectType = DomainObjectType
            SetColumnName()
        End Sub

        Public Sub New(ByVal DomainObjectType As Type, ByVal PropertyName As String, ByVal Match As MatchType, ByVal Value As Object)
            isCompare = False
            _propertyName = PropertyName
            _Match = Match
            _Value = Value
            _DomainObjectType = DomainObjectType
            SetColumnName()
        End Sub

        Public Sub New(ByVal DomainObjectType As Type, ByVal PropertyName As String, ByVal Match As CompareMatchType, ByVal PropertyCompareName As String, ByVal dummySignature As Boolean)
            isCompare = True
            _propertyName = PropertyName
            _CompareMatch = Match
            _propertyCompareName = PropertyCompareName
            _DomainObjectType = DomainObjectType
            SetColumnName()
            SetCompareColumnName()
        End Sub

        Public Property PropertyName() As String
            Get
                Return _propertyName
            End Get
            Set(ByVal Value As String)
                _propertyName = Value
                SetColumnName()
            End Set
        End Property

        Public Property PropertyCompareName() As String
            Get
                Return _propertyCompareName
            End Get
            Set(ByVal Value As String)
                _propertyCompareName = Value
                SetCompareColumnName()
            End Set
        End Property

        Public Property Match() As MatchType
            Get
                Return _Match
            End Get
            Set(ByVal Value As MatchType)
                _Match = Value
            End Set
        End Property

        Public Property CompareMatch() As CompareMatchType
            Get
                Return _CompareMatch
            End Get
            Set(ByVal Value As CompareMatchType)
                _CompareMatch = Value
            End Set
        End Property


        Public Property Value() As Object
            Get
                Return _Value
            End Get
            Set(ByVal Value As Object)
                Dim strValue As String = Value.ToString
                Dim strToReplace As String = "'"

                For Each chr As Char In strToReplace.ToCharArray()
                    strValue = strValue.Replace(chr.ToString(), "")
                Next
                _Value = Value
            End Set
        End Property

        Public ReadOnly Property DomainObjectType() As Type
            Get
                Return _DomainObjectType
            End Get
        End Property

#End Region

#Region "Public Methods"

        Public Overrides Function ToString() As String Implements ICriteria.ToString
            If isCompare Then
                If m_ColumnName <> String.Empty And m_CompareColumnName <> String.Empty Then
                    Return m_ColumnName + GetCompareMatchType(_CompareMatch) + m_CompareColumnName
                End If
            Else
                If m_ColumnName <> String.Empty Then
                    Return m_ColumnName + GetMatchType(_Match)
                End If
            End If
            Return ""
        End Function

#End Region

#Region "Private Methods"

        Private Function QuoteString(ByVal strValue As String) As String
            Return "'" + strValue + "'"
        End Function

        Private Function SetFormatString(ByVal objValue As Object, ByVal strFormatString As String) As String
            Dim str As String = String.Format(strFormatString, objValue)
            If str.StartsWith("'") And str.EndsWith("'") Then
                Dim temp As String = String.Empty
                temp = str.Substring(1, str.Length - 2)
                temp = temp.Replace("'", "''")
                str = "'" + temp + "'"
            End If
            Return str
        End Function

        Private Function GetMatchType(ByVal type As MatchType) As String
            Select Case type
                Case MatchType.Exact
                    Return " = " + SetFormatString(_Value, m_FormatString)
                Case MatchType.No
                    Return " <> " + SetFormatString(_Value, m_FormatString)
                Case MatchType.[Partial]
                    Return " like " + SetFormatString("%" + _Value.ToString() + "%", m_FormatString)
                Case MatchType.StartsWith
                    Return " like " + SetFormatString(_Value.ToString() + "%", m_FormatString)
                Case MatchType.EndsWith
                    Return " like " + SetFormatString("%" + _Value.ToString(), m_FormatString)
                Case MatchType.Greater
                    Return " > " + SetFormatString(_Value, m_FormatString)
                Case MatchType.Lesser
                    Return " < " + SetFormatString(_Value, m_FormatString)
                Case MatchType.IsNull
                    Return " is null"
                Case MatchType.IsNotNull
                    Return " is not null"
                Case MatchType.GreaterOrEqual
                    Return " >= " + SetFormatString(_Value, m_FormatString)
                Case MatchType.LesserOrEqual
                    Return " <= " + SetFormatString(_Value, m_FormatString)
                Case MatchType.InSet
                    Return " in " + _Value.ToString()
                Case MatchType.NotInSet
                    Return " not in " + "(" + _Value.ToString() + ")"
                Case MatchType.NotLike
                    Return " not like " + SetFormatString("%" + _Value.ToString() + "%", m_FormatString)
                Case Else
                    Return " = " + SetFormatString(_Value, m_FormatString)
            End Select
        End Function

        Private Function GetCompareMatchType(ByVal type As CompareMatchType) As String
            Select Case type
                Case MatchType.Exact
                    Return " = "
                Case MatchType.No
                    Return " <> "
                Case MatchType.Greater
                    Return " > "
                Case MatchType.Lesser
                    Return " < "
                Case MatchType.GreaterOrEqual
                    Return " >= "
                Case MatchType.LesserOrEqual
                    Return " <= "
                Case Else
                    Return " = "
            End Select
        End Function

        Private Sub SetColumnName()
            Dim prop As PropertyInfo

            '//Check to see whether it is a deep search
            If _propertyName.IndexOf(".") <> -1 Then
                Dim Properties() As String = _propertyName.Split((".").ToCharArray())
                Dim dummyType As Type = _DomainObjectType

                For i As Integer = 0 To Properties.Length - 2
                    dummyType = dummyType.GetProperty(Properties(i)).PropertyType
                Next
                prop = dummyType.GetProperty(Properties(Properties.Length - 1))
            Else
                prop = _DomainObjectType.GetProperty(_propertyName)
            End If

            If Not IsNothing(prop) Then
                Dim attr As ColumnInfoAttribute = CType(Attribute.GetCustomAttribute(prop, GetType(ColumnInfoAttribute)), ColumnInfoAttribute)
                If Not IsNothing(attr) Then
                    Dim tableInfoAttr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(prop.DeclaringType, GetType(TableInfoAttribute)), TableInfoAttribute)

                    m_ColumnName = tableInfoAttr.TableName + "." + attr.ColumnName
                    m_FormatString = attr.FormatString
                Else
                    Throw New SearchException(_propertyName + " Property of " + _DomainObjectType.ToString() + " does not have ColumnInfoAttribute.")
                End If
            Else
                Throw New SearchException("There is no " + _propertyName + " Property in " + _DomainObjectType.ToString() + ".")
            End If
        End Sub

        Private Sub SetCompareColumnName()
            Dim prop As PropertyInfo

            '//Check to see whether it is a deep search
            If _propertyCompareName.IndexOf(".") <> -1 Then
                Dim Properties() As String = _propertyCompareName.Split((".").ToCharArray())
                Dim dummyType As Type = _DomainObjectType

                For i As Integer = 0 To Properties.Length - 2
                    dummyType = dummyType.GetProperty(Properties(i)).PropertyType
                Next
                prop = dummyType.GetProperty(Properties(Properties.Length - 1))
            Else
                prop = _DomainObjectType.GetProperty(_propertyCompareName)
            End If

            If Not IsNothing(prop) Then
                Dim attr As ColumnInfoAttribute = CType(Attribute.GetCustomAttribute(prop, GetType(ColumnInfoAttribute)), ColumnInfoAttribute)
                If Not IsNothing(attr) Then
                    Dim tableInfoAttr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(prop.DeclaringType, GetType(TableInfoAttribute)), TableInfoAttribute)

                    m_CompareColumnName = tableInfoAttr.TableName + "." + attr.ColumnName
                    m_FormatString = attr.FormatString
                Else
                    Throw New SearchException(_propertyCompareName + " Property of " + _DomainObjectType.ToString() + " does not have ColumnInfoAttribute.")
                End If
            Else
                Throw New SearchException("There is no " + _propertyCompareName + " Property in " + _DomainObjectType.ToString() + ".")
            End If
        End Sub
#End Region



    End Class

End Namespace