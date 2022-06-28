
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : MSPExPayment Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2020 
'// ---------------------
'// $History      : $
'// Generated on 11/10/2020 - 4:03:56 PM
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

    Public Class MSPExPaymentMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertMSPExPayment"
        Private m_UpdateStatement As String = "up_UpdateMSPExPayment"
        Private m_RetrieveStatement As String = "up_RetrieveMSPExPayment"
        Private m_RetrieveListStatement As String = "up_RetrieveMSPExPaymentList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteMSPExPayment"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim mSPExPayment As MSPExPayment = Nothing
            While dr.Read

                mSPExPayment = Me.CreateObject(dr)

            End While

            Return mSPExPayment

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim mSPExPaymentList As ArrayList = New ArrayList

            While dr.Read
                Dim mSPExPayment As MSPExPayment = Me.CreateObject(dr)
                mSPExPaymentList.Add(mSPExPayment)
            End While

            Return mSPExPaymentList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim mSPExPayment As MSPExPayment = CType(obj, MSPExPayment)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, mSPExPayment.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim mSPExPayment As MSPExPayment = CType(obj, MSPExPayment)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@RegNumber", DbType.AnsiString, mSPExPayment.RegNumber)
            DbCommandWrapper.AddInParameter("@PlanTransferDate", DbType.DateTime, mSPExPayment.PlanTransferDate)
            DbCommandWrapper.AddInParameter("@TotalAmount", DbType.Currency, mSPExPayment.TotalAmount)
            DbCommandWrapper.AddInParameter("@ActualTransferDate", DbType.DateTime, mSPExPayment.ActualTransferDate)
            DbCommandWrapper.AddInParameter("@ActualTotalAmount", DbType.Currency, mSPExPayment.ActualTotalAmount)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, mSPExPayment.Status)
            DbCommandWrapper.AddInParameter("@IsTransfertoSAP", DbType.Int16, mSPExPayment.IsTransfertoSAP)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, mSPExPayment.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, mSPExPayment.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, Me.GetRefObject(mSPExPayment.Dealer))
            DbCommandWrapper.AddInParameter("@ClearingTotalAmount", DbType.Currency, mSPExPayment.ClearingTotalAmount)
            DbCommandWrapper.AddInParameter("@ClearingDate", DbType.DateTime, mSPExPayment.ClearingDate)
            DbCommandWrapper.AddInParameter("@TRNumber", DbType.AnsiString, mSPExPayment.TRNumber)
            DbCommandWrapper.AddInParameter("@BankReffNumber", DbType.AnsiString, mSPExPayment.BankReffNumber)

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

            Dim mSPExPayment As MSPExPayment = CType(obj, MSPExPayment)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, mSPExPayment.ID)
            DbCommandWrapper.AddInParameter("@RegNumber", DbType.AnsiString, mSPExPayment.RegNumber)
            DbCommandWrapper.AddInParameter("@PlanTransferDate", DbType.DateTime, mSPExPayment.PlanTransferDate)
            DbCommandWrapper.AddInParameter("@TotalAmount", DbType.Currency, mSPExPayment.TotalAmount)
            DbCommandWrapper.AddInParameter("@ActualTransferDate", DbType.DateTime, mSPExPayment.ActualTransferDate)
            DbCommandWrapper.AddInParameter("@ActualTotalAmount", DbType.Currency, mSPExPayment.ActualTotalAmount)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, mSPExPayment.Status)
            DbCommandWrapper.AddInParameter("@IsTransfertoSAP", DbType.Int16, mSPExPayment.IsTransfertoSAP)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, mSPExPayment.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, mSPExPayment.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, Me.GetRefObject(mSPExPayment.Dealer))
            DbCommandWrapper.AddInParameter("@ClearingTotalAmount", DbType.Currency, mSPExPayment.ClearingTotalAmount)
            DbCommandWrapper.AddInParameter("@ClearingDate", DbType.DateTime, mSPExPayment.ClearingDate)
            DbCommandWrapper.AddInParameter("@TRNumber", DbType.AnsiString, mSPExPayment.TRNumber)
            DbCommandWrapper.AddInParameter("@BankReffNumber", DbType.AnsiString, mSPExPayment.BankReffNumber)

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As MSPExPayment

            Dim mSPExPayment As MSPExPayment = New MSPExPayment

            mSPExPayment.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("RegNumber")) Then mSPExPayment.RegNumber = dr("RegNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PlanTransferDate")) Then mSPExPayment.PlanTransferDate = CType(dr("PlanTransferDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("TotalAmount")) Then mSPExPayment.TotalAmount = CType(dr("TotalAmount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("ActualTransferDate")) Then mSPExPayment.ActualTransferDate = CType(dr("ActualTransferDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("ActualTotalAmount")) Then mSPExPayment.ActualTotalAmount = CType(dr("ActualTotalAmount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then mSPExPayment.Status = CType(dr("Status"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("IsTransfertoSAP")) Then mSPExPayment.IsTransfertoSAP = CType(dr("IsTransfertoSAP"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then mSPExPayment.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then mSPExPayment.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then mSPExPayment.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then mSPExPayment.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then mSPExPayment.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then
                mSPExPayment.Dealer = New Dealer(CType(dr("DealerID"), Short))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("ClearingTotalAmount")) Then mSPExPayment.ClearingTotalAmount = CType(dr("ClearingTotalAmount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("ClearingDate")) Then mSPExPayment.ClearingDate = CType(dr("ClearingDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("TRNumber")) Then mSPExPayment.TRNumber = dr("TRNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("BankReffNumber")) Then mSPExPayment.BankReffNumber = dr("BankReffNumber").ToString

            Return mSPExPayment

        End Function

        Private Sub SetTableName()

            If Not (GetType(MSPExPayment) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(MSPExPayment), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(MSPExPayment).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

