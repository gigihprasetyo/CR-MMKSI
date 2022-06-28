#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : TrTraineePaymentHeader Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2019 
'// ---------------------
'// $History      : $
'// Generated on 8/26/2019 - 1:06:29 PM
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

    Public Class TrTraineePaymentHeaderMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertTrTraineePaymentHeader"
        Private m_UpdateStatement As String = "up_UpdateTrTraineePaymentHeader"
        Private m_RetrieveStatement As String = "up_RetrieveTrTraineePaymentHeader"
        Private m_RetrieveListStatement As String = "up_RetrieveTrTraineePaymentHeaderList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteTrTraineePaymentHeader"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim trTraineePaymentHeader As TrTraineePaymentHeader = Nothing
            While dr.Read

                trTraineePaymentHeader = Me.CreateObject(dr)

            End While

            Return trTraineePaymentHeader

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim trTraineePaymentHeaderList As ArrayList = New ArrayList

            While dr.Read
                Dim trTraineePaymentHeader As TrTraineePaymentHeader = Me.CreateObject(dr)
                trTraineePaymentHeaderList.Add(trTraineePaymentHeader)
            End While

            Return trTraineePaymentHeaderList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim trTraineePaymentHeader As TrTraineePaymentHeader = CType(obj, TrTraineePaymentHeader)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, trTraineePaymentHeader.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim trTraineePaymentHeader As TrTraineePaymentHeader = CType(obj, TrTraineePaymentHeader)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, Me.GetRefObject(trTraineePaymentHeader.Dealer))
            DbCommandWrapper.AddInParameter("@PaymentType", DbType.AnsiString, trTraineePaymentHeader.PaymentType)
            DbCommandWrapper.AddInParameter("@RegNumber", DbType.AnsiString, trTraineePaymentHeader.RegNumber)
            DbCommandWrapper.AddInParameter("@RevisionPaymentDocID", DbType.Int32, trTraineePaymentHeader.RevisionPaymentDocID)
            DbCommandWrapper.AddInParameter("@TotalAmount", DbType.Currency, trTraineePaymentHeader.TotalAmount)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, trTraineePaymentHeader.Status)
            DbCommandWrapper.AddInParameter("@EvidencePath", DbType.AnsiString, trTraineePaymentHeader.EvidencePath)
            DbCommandWrapper.AddInParameter("@ActualPaymentDate", DbType.DateTime, trTraineePaymentHeader.ActualPaymentDate)
            DbCommandWrapper.AddInParameter("@ActualPaymentAmount", DbType.Currency, trTraineePaymentHeader.ActualPaymentAmount)
            DbCommandWrapper.AddInParameter("@AccDocNumber", DbType.AnsiString, trTraineePaymentHeader.AccDocNumber)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, trTraineePaymentHeader.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, trTraineePaymentHeader.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


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

            Dim trTraineePaymentHeader As TrTraineePaymentHeader = CType(obj, TrTraineePaymentHeader)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, trTraineePaymentHeader.ID)
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, Me.GetRefObject(trTraineePaymentHeader.Dealer))
            DbCommandWrapper.AddInParameter("@PaymentType", DbType.AnsiString, trTraineePaymentHeader.PaymentType)
            DbCommandWrapper.AddInParameter("@RegNumber", DbType.AnsiString, trTraineePaymentHeader.RegNumber)
            DbCommandWrapper.AddInParameter("@RevisionPaymentDocID", DbType.Int32, trTraineePaymentHeader.RevisionPaymentDocID)
            DbCommandWrapper.AddInParameter("@TotalAmount", DbType.Currency, trTraineePaymentHeader.TotalAmount)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, trTraineePaymentHeader.Status)
            DbCommandWrapper.AddInParameter("@EvidencePath", DbType.AnsiString, trTraineePaymentHeader.EvidencePath)
            DbCommandWrapper.AddInParameter("@ActualPaymentDate", DbType.DateTime, trTraineePaymentHeader.ActualPaymentDate)
            DbCommandWrapper.AddInParameter("@ActualPaymentAmount", DbType.Currency, trTraineePaymentHeader.ActualPaymentAmount)
            DbCommandWrapper.AddInParameter("@AccDocNumber", DbType.AnsiString, trTraineePaymentHeader.AccDocNumber)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, trTraineePaymentHeader.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, trTraineePaymentHeader.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As TrTraineePaymentHeader

            Dim trTraineePaymentHeader As TrTraineePaymentHeader = New TrTraineePaymentHeader

            trTraineePaymentHeader.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then trTraineePaymentHeader.Dealer = New Dealer(CType(dr("DealerID"), Integer))
            If Not dr.IsDBNull(dr.GetOrdinal("PaymentType")) Then trTraineePaymentHeader.PaymentType = dr("PaymentType").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RegNumber")) Then trTraineePaymentHeader.RegNumber = dr("RegNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RevisionPaymentDocID")) Then trTraineePaymentHeader.RevisionPaymentDocID = CType(dr("RevisionPaymentDocID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("TotalAmount")) Then trTraineePaymentHeader.TotalAmount = CType(dr("TotalAmount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then trTraineePaymentHeader.Status = CType(dr("Status"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("EvidencePath")) Then trTraineePaymentHeader.EvidencePath = dr("EvidencePath").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ActualPaymentDate")) Then trTraineePaymentHeader.ActualPaymentDate = CType(dr("ActualPaymentDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("ActualPaymentAmount")) Then trTraineePaymentHeader.ActualPaymentAmount = CType(dr("ActualPaymentAmount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("AccDocNumber")) Then trTraineePaymentHeader.AccDocNumber = dr("AccDocNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then trTraineePaymentHeader.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then trTraineePaymentHeader.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then trTraineePaymentHeader.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then trTraineePaymentHeader.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then trTraineePaymentHeader.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            Return trTraineePaymentHeader

        End Function

        Private Sub SetTableName()

            If Not (GetType(TrTraineePaymentHeader) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(TrTraineePaymentHeader), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(TrTraineePaymentHeader).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace
