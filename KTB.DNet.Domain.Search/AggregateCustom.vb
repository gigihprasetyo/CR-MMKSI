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
'// Generated on 8/01/2005 - 2:14:00 PM
'//
'// ===========================================================================		
#End Region

#Region ".NET Base Class Namespace Imports"

Imports System
Imports System.Reflection

#End Region


Namespace KTB.DNet.Domain.Search

    ''' <summary>
    ''' This class is for custom aggregate
    ''' </summary>
    ''' <remarks></remarks>
    Public Class AggregateCustom
        Implements IAggregate

#Region "Private Variables"

        Dim _propertyName As String
        Dim _AggregateType As AggregateType = AggregateType.Count
        Dim _IsDistinct As Boolean = False
        Dim m_DomainObjectType As Type = Nothing

#End Region

#Region "Constructors/Destructors/Finalizers"

        Public Sub New(ByVal DomainObjectType As Type, ByVal PropertyName As String, ByVal aggregate As AggregateType)

            _propertyName = PropertyName
            _IsDistinct = False
            _AggregateType = aggregate
            m_DomainObjectType = DomainObjectType

        End Sub

        Public Sub New(ByVal DomainObjectType As Type, ByVal PropertyName As String, ByVal aggregate As AggregateType, ByVal isDistinct As Boolean)

            _propertyName = PropertyName
            _IsDistinct = isDistinct
            _AggregateType = aggregate
            m_DomainObjectType = DomainObjectType

        End Sub

        Public Property PropertyName() As String
            Get
                Return _propertyName
            End Get
            Set(ByVal Value As String)
                _propertyName = Value
            End Set
        End Property

        Public Property Aggregation() As AggregateType
            Get
                Return _AggregateType
            End Get
            Set(ByVal Value As AggregateType)
                _AggregateType = Value
            End Set
        End Property

        Public Property Distinct() As Boolean
            Get
                Return _IsDistinct
            End Get
            Set(ByVal Value As Boolean)
                _IsDistinct = Value
            End Set
        End Property

#End Region

#Region "Public Methods"

        Public Overrides Function ToString() As String Implements IAggregate.ToString



            Return IsDistinct() + " " + GetAggregationType(_AggregateType) + " (" + _propertyName + ") "


            Return ""

        End Function

#End Region

#Region "Private Methods"

        Private Function IsDistinct() As String

            If (_IsDistinct) Then

                Return "Distinct"

            Else

                Return ""

            End If

        End Function

        Private Function GetAggregationType(ByVal aggregation As AggregateType) As String

            Select Case aggregation

                Case AggregateType.Avg : Return "Avg"
                Case AggregateType.Count : Return "Count"
                Case AggregateType.Max : Return "Max"
                Case AggregateType.Min : Return "Min"
                Case AggregateType.Sum : Return "Sum"
                Case AggregateType.Distinct : Return "Distinct"
                Case Else : Return "Count"

            End Select

        End Function

#End Region

    End Class

End Namespace