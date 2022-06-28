﻿
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : BenefitMasterLeasing Domain Object.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2015 
'// ---------------------
'// $History      : $
'// Generated on 12/11/2015 - 8:50:04 AM
'//
'// ===========================================================================	
#End Region

#Region ".NET Base Class Namespace Imports"
Imports System
Imports System.Collections
#End Region

#Region "Custom Namespace Imports"
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports KTB.DNet.Domain.Search
#End Region

Namespace KTB.DNet.Domain
    <Serializable(), TableInfo("BenefitMasterLeasing")> _
    Public Class BenefitMasterLeasing
        Inherits DomainObject

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
        End Sub

        Public Sub New(ByVal ID As Integer)
            _iD = ID
        End Sub

#End Region

#Region "Private Variables"

        Private _iD As Integer
        Private _rowStatus As Short
        Private _createdBy As String = String.Empty
        Private _createdTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)
        Private _lastUpdateBy As String = String.Empty
        Private _lastUpdateTime As DateTime = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime)

        Private _leasingCompany As LeasingCompany
        Private _benefitMasterDetail As BenefitMasterDetail



#End Region

#Region "Public Properties"

        <ColumnInfo("ID", "{0}")> _
        Public Property ID As Integer
            Get
                Return _iD
            End Get
            Set(ByVal value As Integer)
                _iD = value
            End Set
        End Property


        <ColumnInfo("RowStatus", "{0}")> _
        Public Property RowStatus As Short
            Get
                Return _rowStatus
            End Get
            Set(ByVal value As Short)
                _rowStatus = value
            End Set
        End Property


        <ColumnInfo("CreatedBy", "'{0}'")> _
        Public Property CreatedBy As String
            Get
                Return _createdBy
            End Get
            Set(ByVal value As String)
                _createdBy = value
            End Set
        End Property


        <ColumnInfo("CreatedTime", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property CreatedTime As DateTime
            Get
                Return _createdTime
            End Get
            Set(ByVal value As DateTime)
                _createdTime = value
            End Set
        End Property


        <ColumnInfo("LastUpdateBy", "'{0}'")> _
        Public Property LastUpdateBy As String
            Get
                Return _lastUpdateBy
            End Get
            Set(ByVal value As String)
                _lastUpdateBy = value
            End Set
        End Property


        <ColumnInfo("LastUpdateTime", "'{0:yyyy/MM/dd HH:mm:ss}'")> _
        Public Property LastUpdateTime As DateTime
            Get
                Return _lastUpdateTime
            End Get
            Set(ByVal value As DateTime)
                _lastUpdateTime = value
            End Set
        End Property


        <ColumnInfo("LeasingCompanyID", "{0}"), _
        RelationInfo("LeasingCompany", "ID", "BenefitMasterLeasing", "LeasingCompanyID")> _
        Public Property LeasingCompany As LeasingCompany
            Get
                Try
                    If Not IsNothing(Me._leasingCompany) AndAlso (Not Me._leasingCompany.IsLoaded) AndAlso (_leasingCompany.ID > 0) Then

                        Me._leasingCompany = CType(DoLoad(GetType(LeasingCompany).ToString(), _leasingCompany.ID), LeasingCompany)
                        Me._leasingCompany.MarkLoaded()

                    End If

                    Return Me._leasingCompany

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As LeasingCompany)

                Me._leasingCompany = value
                If (Not isnothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._leasingCompany.MarkLoaded()
                End If
            End Set
        End Property

        <ColumnInfo("BenefitMasterDetailID", "{0}"), _
        RelationInfo("BenefitMasterDetail", "ID", "BenefitMasterLeasing", "BenefitMasterDetailID")> _
        Public Property BenefitMasterDetail As BenefitMasterDetail
            Get
                Try
                    If Not IsNothing(Me._benefitMasterDetail) AndAlso (Not Me._benefitMasterDetail.IsLoaded) AndAlso (_benefitMasterDetail.ID > 0) Then

                        'Me._benefitMasterDetail = CType(DoLoad(GetType(BenefitMasterDetail).ToString(), _benefitMasterDetail.ID), BenefitMasterDetail)
                        Me._benefitMasterDetail = _benefitMasterDetail
                        Me._benefitMasterDetail.MarkLoaded()

                    End If

                    Return Me._benefitMasterDetail

                Catch ex As Exception

                    Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "Domain Policy")

                    If rethrow Then
                        Throw
                    End If

                End Try

                Return Nothing
            End Get

            Set(ByVal value As BenefitMasterDetail)

                Me._benefitMasterDetail = value
                If (Not isnothing(value)) AndAlso (CType(value, DomainObject)).IsLoaded Then
                    Me._benefitMasterDetail.MarkLoaded()
                End If
            End Set
        End Property



#End Region

#Region "Generated Method"
        Public Function GetStrDate(ByVal dateInput As DateTime, ByVal dateFormat As String) As String
            If dateInput = CType(System.Data.SqlTypes.SqlDateTime.MinValue.Value, DateTime) Then
                Return ""
            Else
                Return Format(dateInput, dateFormat)
            End If
        End Function
#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

