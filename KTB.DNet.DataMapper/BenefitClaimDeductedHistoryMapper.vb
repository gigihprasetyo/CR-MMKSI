﻿
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : BenefitClaimDeductedHistory Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2019 
'// ---------------------
'// $History      : $
'// Generated on 16/12/2019 - 14:19:09
'//
'// ===========================================================================	
#End Region


#Region ".NET Base Class Namespace Imports"
Imports System
Imports System.Data
Imports System.Collections
#End Region

#Region "Custom Namespace Imports"
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports Microsoft.Practices.EnterpriseLibrary.ExceptionHandling
Imports Microsoft.Practices.EnterpriseLibrary.Logging
Imports KTB.DNet.DataMapper.Framework
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
#End Region

Namespace KTB.DNet.DataMapper

    Public Class BenefitClaimDeductedHistoryMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertBenefitClaimDeductedHistory"
        Private m_UpdateStatement As String = "up_UpdateBenefitClaimDeductedHistory"
        Private m_RetrieveStatement As String = "up_RetrieveBenefitClaimDeductedHistory"
        Private m_RetrieveListStatement As String = "up_RetrieveBenefitClaimDeductedHistoryList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteBenefitClaimDeductedHistory"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim benefitClaimDeductedHistory As BenefitClaimDeductedHistory = Nothing
            While dr.Read

                benefitClaimDeductedHistory = Me.CreateObject(dr)

            End While

            Return benefitClaimDeductedHistory

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim benefitClaimDeductedHistoryList As ArrayList = New ArrayList

            While dr.Read
                Dim benefitClaimDeductedHistory As BenefitClaimDeductedHistory = Me.CreateObject(dr)
                benefitClaimDeductedHistoryList.Add(benefitClaimDeductedHistory)
            End While

            Return benefitClaimDeductedHistoryList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim benefitClaimDeductedHistory As BenefitClaimDeductedHistory = CType(obj, BenefitClaimDeductedHistory)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int16, benefitClaimDeductedHistory.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim benefitClaimDeductedHistory As BenefitClaimDeductedHistory = CType(obj, BenefitClaimDeductedHistory)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int16, 2)
            DbCommandWrapper.AddInParameter("@AmountDeducted", DbType.Currency, benefitClaimDeductedHistory.AmountDeducted)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, benefitClaimDeductedHistory.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, benefitClaimDeductedHistory.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@BenefitClaimDeductedID", DbType.Int32, Me.GetRefObject(benefitClaimDeductedHistory.BenefitClaimDeducted))
            DbCommandWrapper.AddInParameter("@BenefitClaimHeaderID", DbType.Int32, Me.GetRefObject(benefitClaimDeductedHistory.BenefitClaimHeader))

            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetNewID(ByVal dbCommandWrapper As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper) As Integer

            Return CType(dbCommandWrapper.GetParameterValue("@ID"), Integer)

        End Function

        Protected Overrides Function GetPagingRetrieveCommand() As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_PagingQuery)
            DbCommandWrapper.AddInParameter("@Table", DbType.String, m_TableName)
            DbCommandWrapper.AddInParameter("@PK", DbType.String, "ID")

            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetRetrieveCommand() As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DinamicQuery)
            DbCommandWrapper.AddInParameter("@sqlQuery", DbType.String, "SELECT " + m_TableName + ".* FROM " + m_TableName)

            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetRetrieveListParameter() As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_RetrieveListStatement)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetRetrieveParameter(ByVal id As Integer) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_RetrieveStatement)
            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, id)

            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetUpdateParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim benefitClaimDeductedHistory As BenefitClaimDeductedHistory = CType(obj, BenefitClaimDeductedHistory)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int16, benefitClaimDeductedHistory.ID)
            DbCommandWrapper.AddInParameter("@AmountDeducted", DbType.Currency, benefitClaimDeductedHistory.AmountDeducted)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, benefitClaimDeductedHistory.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, benefitClaimDeductedHistory.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@BenefitClaimDeductedID", DbType.Int32, Me.GetRefObject(benefitClaimDeductedHistory.BenefitClaimDeducted))
            DbCommandWrapper.AddInParameter("@BenefitClaimHeaderID", DbType.Int32, Me.GetRefObject(benefitClaimDeductedHistory.BenefitClaimHeader))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As BenefitClaimDeductedHistory

            Dim benefitClaimDeductedHistory As BenefitClaimDeductedHistory = New BenefitClaimDeductedHistory

            benefitClaimDeductedHistory.ID = CType(dr("ID"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("AmountDeducted")) Then benefitClaimDeductedHistory.AmountDeducted = CType(dr("AmountDeducted"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then benefitClaimDeductedHistory.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then benefitClaimDeductedHistory.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then benefitClaimDeductedHistory.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then benefitClaimDeductedHistory.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then benefitClaimDeductedHistory.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("BenefitClaimDeductedID")) Then
                benefitClaimDeductedHistory.BenefitClaimDeducted = New BenefitClaimDeducted(CType(dr("BenefitClaimDeductedID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("BenefitClaimHeaderID")) Then
                benefitClaimDeductedHistory.BenefitClaimHeader = New BenefitClaimHeader(CType(dr("BenefitClaimHeaderID"), Integer))
            End If

            Return benefitClaimDeductedHistory

        End Function

        Private Sub SetTableName()

            If Not (GetType(BenefitClaimDeductedHistory) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(BenefitClaimDeductedHistory), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(BenefitClaimDeductedHistory).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

