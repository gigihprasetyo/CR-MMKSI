#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : PaymentStatus Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2005 
'// ---------------------
'// $History      : $
'// Generated on 10/11/2005 - 11:18:29 AM
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

    Public Class PaymentStatusMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertPaymentStatus"
        Private m_UpdateStatement As String = "up_UpdatePaymentStatus"
        Private m_RetrieveStatement As String = "up_RetrievePaymentStatus"
        Private m_RetrieveListStatement As String = "up_RetrievePaymentStatusList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeletePaymentStatus"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim paymentStatus As paymentStatus = Nothing
            While dr.Read

                paymentStatus = Me.CreateObject(dr)

            End While

            Return paymentStatus

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim paymentStatusList As ArrayList = New ArrayList

            While dr.Read
                Dim paymentStatus As paymentStatus = Me.CreateObject(dr)
                paymentStatusList.Add(paymentStatus)
            End While

            Return paymentStatusList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim paymentStatus As paymentStatus = CType(obj, paymentStatus)
            DBCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DBCommandWrapper.AddInParameter("@ID", DbType.Int32, paymentStatus.ID)
            Return DBCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim paymentStatus As paymentStatus = CType(obj, paymentStatus)
            DBCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DBCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DBCommandWrapper.AddInParameter("@SONumber", DbType.AnsiString, paymentStatus.SONumber)
            DBCommandWrapper.AddInParameter("@DocNumber", DbType.AnsiString, paymentStatus.DocNumber)
            DBCommandWrapper.AddInParameter("@DocDate", DbType.DateTime, paymentStatus.DocDate)
            DBCommandWrapper.AddInParameter("@BaseLineDate", DbType.DateTime, paymentStatus.BaseLineDate)
            DBCommandWrapper.AddInParameter("@GiroSlipNumber", DbType.AnsiString, paymentStatus.GiroSlipNumber)
            DBCommandWrapper.AddInParameter("@Purpose", DbType.AnsiString, paymentStatus.Purpose)
            DBCommandWrapper.AddInParameter("@RecieptNumber", DbType.AnsiString, paymentStatus.RecieptNumber)
            DBCommandWrapper.AddInParameter("@Amount", DbType.Currency, paymentStatus.Amount)
            DBCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, paymentStatus.RowStatus)
            DBCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DBCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, paymentStatus.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DBCommandWrapper.AddInParameter("@DealerID", DbType.Int32, Me.GetRefObject(paymentStatus.Dealer))
            DBCommandWrapper.AddInParameter("@POHeaderID", DbType.Int32, Me.GetRefObject(paymentStatus.POHeader))

            Return DBCommandWrapper

        End Function

        Protected Overrides Function GetNewID(ByVal dbCommandWrapper As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper) As Integer

            Return CType(dbCommandWrapper.GetParameterValue("@ID"), Integer)

        End Function

        Protected Overrides Function GetPagingRetrieveCommand() As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            DBCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_PagingQuery)
            DBCommandWrapper.AddInParameter("@Table", DbType.String, m_TableName)
            DBCommandWrapper.AddInParameter("@PK", DbType.String, "ID")

            Return DBCommandWrapper

        End Function

        Protected Overrides Function GetRetrieveCommand() As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            DBCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DinamicQuery)
            DBCommandWrapper.AddInParameter("@sqlQuery", DbType.String, "SELECT " + m_TableName + ".* FROM " + m_TableName)

            Return DBCommandWrapper

        End Function

        Protected Overrides Function GetRetrieveListParameter() As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            DBCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_RetrieveListStatement)
            Return DBCommandWrapper

        End Function

        Protected Overrides Function GetRetrieveParameter(ByVal id As Integer) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            DBCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_RetrieveStatement)
            DBCommandWrapper.AddInParameter("@ID", DbType.Int32, id)

            Return DBCommandWrapper

        End Function

        Protected Overrides Function GetUpdateParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim paymentStatus As paymentStatus = CType(obj, paymentStatus)

            DBCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DBCommandWrapper.AddInParameter("@ID", DbType.Int32, paymentStatus.ID)
            DBCommandWrapper.AddInParameter("@SONumber", DbType.AnsiString, paymentStatus.SONumber)
            DBCommandWrapper.AddInParameter("@DocNumber", DbType.AnsiString, paymentStatus.DocNumber)
            DBCommandWrapper.AddInParameter("@DocDate", DbType.DateTime, paymentStatus.DocDate)
            DBCommandWrapper.AddInParameter("@BaseLineDate", DbType.DateTime, paymentStatus.BaseLineDate)
            DBCommandWrapper.AddInParameter("@GiroSlipNumber", DbType.AnsiString, paymentStatus.GiroSlipNumber)
            DBCommandWrapper.AddInParameter("@Purpose", DbType.AnsiString, paymentStatus.Purpose)
            DBCommandWrapper.AddInParameter("@RecieptNumber", DbType.AnsiString, paymentStatus.RecieptNumber)
            DBCommandWrapper.AddInParameter("@Amount", DbType.Currency, paymentStatus.Amount)
            DBCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, paymentStatus.RowStatus)
            DBCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, paymentStatus.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DBCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DBCommandWrapper.AddInParameter("@DealerID", DbType.Int32, Me.GetRefObject(paymentStatus.Dealer))
            DBCommandWrapper.AddInParameter("@POHeaderID", DbType.Int32, Me.GetRefObject(paymentStatus.POHeader))

            Return DBCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As PaymentStatus

            Dim paymentStatus As paymentStatus = New paymentStatus

            paymentStatus.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("SONumber")) Then paymentStatus.SONumber = dr("SONumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DocNumber")) Then paymentStatus.DocNumber = dr("DocNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DocDate")) Then paymentStatus.DocDate = CType(dr("DocDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("BaseLineDate")) Then paymentStatus.BaseLineDate = CType(dr("BaseLineDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("GiroSlipNumber")) Then paymentStatus.GiroSlipNumber = dr("GiroSlipNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Purpose")) Then paymentStatus.Purpose = dr("Purpose").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RecieptNumber")) Then paymentStatus.RecieptNumber = dr("RecieptNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Amount")) Then paymentStatus.Amount = CType(dr("Amount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then paymentStatus.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then paymentStatus.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then paymentStatus.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then paymentStatus.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then paymentStatus.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then
                paymentStatus.Dealer = New Dealer(CType(dr("DealerID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("POHeaderID")) Then
                paymentStatus.POHeader = New POHeader(CType(dr("POHeaderID"), Integer))
            End If

            Return paymentStatus

        End Function

        Private Sub SetTableName()

            If Not (GetType(PaymentStatus) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(PaymentStatus), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(PaymentStatus).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

