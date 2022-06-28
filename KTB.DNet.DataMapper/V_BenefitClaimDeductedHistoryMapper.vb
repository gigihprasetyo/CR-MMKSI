
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : V_BenefitClaimDeductedHistoryMapper Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2017 
'// ---------------------
'// $History      : $
'// Generated on 07/12/2017 - 5:17:06 PM
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

    Public Class V_BenefitClaimDeductedHistoryMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertV_BenefitClaimDeductedHistory"
        Private m_UpdateStatement As String = "up_UpdateV_BenefitClaimDeductedHistory"
        Private m_RetrieveStatement As String = "up_RetrieveV_BenefitClaimDeductedHistory"
        Private m_RetrieveListStatement As String = "up_RetrieveV_BenefitClaimDeductedHistoryList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteV_BenefitClaimDeductedHistory"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim V_BenefitClaimDeductedHistory As V_BenefitClaimDeductedHistory = Nothing
            While dr.Read

                V_BenefitClaimDeductedHistory = Me.CreateObject(dr)

            End While

            Return V_BenefitClaimDeductedHistory

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim V_BenefitClaimDeductedHistoryList As ArrayList = New ArrayList

            While dr.Read
                Dim V_BenefitClaimDeductedHistory As V_BenefitClaimDeductedHistory = Me.CreateObject(dr)
                V_BenefitClaimDeductedHistoryList.Add(V_BenefitClaimDeductedHistory)
            End While

            Return V_BenefitClaimDeductedHistoryList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim V_BenefitClaimDeductedHistory As V_BenefitClaimDeductedHistory = CType(obj, V_BenefitClaimDeductedHistory)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, V_BenefitClaimDeductedHistory.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim V_BenefitClaimDeductedHistory As V_BenefitClaimDeductedHistory = CType(obj, V_BenefitClaimDeductedHistory)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)

            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)

            DbCommandWrapper.AddInParameter("@RegNumber", DbType.AnsiString, V_BenefitClaimDeductedHistory.RegNumber)
            DbCommandWrapper.AddInParameter("@ClaimRegNo", DbType.AnsiString, V_BenefitClaimDeductedHistory.ClaimRegNo)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, V_BenefitClaimDeductedHistory.Description)
            DbCommandWrapper.AddInParameter("@AmountDeducted", DbType.Decimal, V_BenefitClaimDeductedHistory.AmountDeducted)
            DbCommandWrapper.AddInParameter("@TransactionDate", DbType.DateTime, V_BenefitClaimDeductedHistory.TransactionDate)

            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, V_BenefitClaimDeductedHistory.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, V_BenefitClaimDeductedHistory.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@BenefitClaimDeductedID", DbType.Int32, Me.GetRefObject(V_BenefitClaimDeductedHistory.V_BenefitClaimDeducted))

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

            Dim V_BenefitClaimDeductedHistory As V_BenefitClaimDeductedHistory = CType(obj, V_BenefitClaimDeductedHistory)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, V_BenefitClaimDeductedHistory.ID)
            
            DbCommandWrapper.AddInParameter("@RegNumber", DbType.AnsiString, V_BenefitClaimDeductedHistory.RegNumber)
            DbCommandWrapper.AddInParameter("@ClaimRegNo", DbType.AnsiString, V_BenefitClaimDeductedHistory.ClaimRegNo)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, V_BenefitClaimDeductedHistory.Description)
            DbCommandWrapper.AddInParameter("@AmountDeducted", DbType.Decimal, V_BenefitClaimDeductedHistory.AmountDeducted)
            DbCommandWrapper.AddInParameter("@TransactionDate", DbType.DateTime, V_BenefitClaimDeductedHistory.TransactionDate)

            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, V_BenefitClaimDeductedHistory.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, V_BenefitClaimDeductedHistory.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@BenefitClaimDeductedID", DbType.Int32, Me.GetRefObject(V_BenefitClaimDeductedHistory.V_BenefitClaimDeducted))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As V_BenefitClaimDeductedHistory

            Dim V_BenefitClaimDeductedHistory As V_BenefitClaimDeductedHistory = New V_BenefitClaimDeductedHistory

            V_BenefitClaimDeductedHistory.ID = CType(dr("ID"), Integer)

            If Not dr.IsDBNull(dr.GetOrdinal("RegNumber")) Then V_BenefitClaimDeductedHistory.RegNumber = dr("RegNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ClaimRegNo")) Then V_BenefitClaimDeductedHistory.ClaimRegNo = dr("ClaimRegNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Description")) Then V_BenefitClaimDeductedHistory.Description = dr("Description").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("AmountDeducted")) Then V_BenefitClaimDeductedHistory.AmountDeducted = CType(dr("AmountDeducted"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("TransactionDate")) Then V_BenefitClaimDeductedHistory.TransactionDate = CType(dr("TransactionDate"), DateTime)

            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then V_BenefitClaimDeductedHistory.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then V_BenefitClaimDeductedHistory.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then V_BenefitClaimDeductedHistory.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then V_BenefitClaimDeductedHistory.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then V_BenefitClaimDeductedHistory.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            If Not dr.IsDBNull(dr.GetOrdinal("BenefitClaimDeductedID")) Then
                V_BenefitClaimDeductedHistory.V_BenefitClaimDeducted = New BenefitClaimDeducted(CType(dr("BenefitClaimDeductedID"), Integer))
            End If

            Return V_BenefitClaimDeductedHistory

        End Function

        Private Sub SetTableName()

            If Not (GetType(V_BenefitClaimDeductedHistory) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(V_BenefitClaimDeductedHistory), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(V_BenefitClaimDeductedHistory).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

