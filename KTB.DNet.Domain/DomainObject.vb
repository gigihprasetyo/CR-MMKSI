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
'// Generated on 7/28/2005 - 10:47:00 AM
'//
'// ===========================================================================		
#End Region

#Region ".NET Base Class Namespace Imports"

Imports System
Imports System.Collections

#End Region

#Region "Custom Namespace Imports"


Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.DataMapper.Framework
#End Region

Namespace KTB.DNet.Domain

    Public MustInherit Class DomainObject

        Private _isLoaded As Boolean
        Private _errorMessage As String
        Private _IsNotChange As Boolean


        Property IsNotChange() As Boolean
            Get
                Return _IsNotChange
            End Get
            Set(ByVal Value As Boolean)
                _IsNotChange = Value
            End Set
        End Property



        Property ErrorMessage() As String
            Get
                Return _errorMessage
            End Get
            Set(ByVal Value As String)
                _errorMessage = Value
            End Set
        End Property

        Public ReadOnly Property IsLoaded() As Boolean
            Get
                Return Me._isLoaded
            End Get
        End Property

        Public Sub MarkLoaded()

            Me._isLoaded = True

        End Sub

        Public Sub MarkUnLoaded()

            Me._isLoaded = False

        End Sub

        Public Function DoLoad(ByVal type As String, ByVal id As Integer) As DomainObject

            Dim mapper As IMapper = MapperFactory.GetInstance().GetMapper(type)
            Return CType(mapper.Retrieve(id), DomainObject)

        End Function

        Public Function DoLoad(ByVal type As String, ByVal id As Integer, ByVal instance As String) As DomainObject

            Dim mapper As IMapper = MapperFactory.GetInstance().GetMapper(type, instance)
            Return CType(mapper.Retrieve(id), DomainObject)

        End Function

        Public Function DoLoadArray(ByVal type As String, ByVal Criterias As CriteriaComposite) As ArrayList

            Dim mapper As IMapper = MapperFactory.GetInstance().GetMapper(type)
            Return mapper.RetrieveByCriteria(Criterias)

        End Function

        Public Function DoLoadArray(ByVal type As String, ByVal Criterias As CriteriaComposite, ByVal Sorts As SortCollection) As ArrayList

            Dim mapper As IMapper = MapperFactory.GetInstance().GetMapper(type)
            Return mapper.RetrieveByCriteria(Criterias, Sorts)

        End Function


        Public Function DoLoadScalar(ByVal type As String, ByVal aggregation As IAggregate, ByVal Criterias As CriteriaComposite) As Integer

            Dim mapper As IMapper = MapperFactory.GetInstance().GetMapper(type)
            If mapper.RetrieveScalar(aggregation, Criterias) Is DBNull.Value Then
                Return 0
            Else
                Return CType(mapper.RetrieveScalar(aggregation, Criterias), Integer)
            End If

        End Function

        Public Function DoLoadScalarDecimal(ByVal type As String, ByVal aggregation As IAggregate, ByVal Criterias As CriteriaComposite) As Decimal

            Dim mapper As IMapper = MapperFactory.GetInstance().GetMapper(type)
            If mapper.RetrieveScalar(aggregation, Criterias) Is DBNull.Value Then
                Return 0
            Else
                Return CType(mapper.RetrieveScalar(aggregation, Criterias), Decimal)
            End If

        End Function
    End Class

End Namespace