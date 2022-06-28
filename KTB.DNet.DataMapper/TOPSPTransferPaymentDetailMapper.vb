
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : TOPSPTransferPaymentDetail Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 9/10/2018 - 10:00:44 AM
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

    Public Class TOPSPTransferPaymentDetailMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertTOPSPTransferPaymentDetail"
        Private m_UpdateStatement As String = "up_UpdateTOPSPTransferPaymentDetail"
        Private m_RetrieveStatement As String = "up_RetrieveTOPSPTransferPaymentDetail"
        Private m_RetrieveListStatement As String = "up_RetrieveTOPSPTransferPaymentDetailList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteTOPSPTransferPaymentDetail"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim tOPSPTransferPaymentDetail As TOPSPTransferPaymentDetail = Nothing
            While dr.Read

                tOPSPTransferPaymentDetail = Me.CreateObject(dr)

            End While

            Return tOPSPTransferPaymentDetail

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim tOPSPTransferPaymentDetailList As ArrayList = New ArrayList

            While dr.Read
                Dim tOPSPTransferPaymentDetail As TOPSPTransferPaymentDetail = Me.CreateObject(dr)
                tOPSPTransferPaymentDetailList.Add(tOPSPTransferPaymentDetail)
            End While

            Return tOPSPTransferPaymentDetailList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim tOPSPTransferPaymentDetail As TOPSPTransferPaymentDetail = CType(obj, TOPSPTransferPaymentDetail)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, tOPSPTransferPaymentDetail.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim tOPSPTransferPaymentDetail As TOPSPTransferPaymentDetail = CType(obj, TOPSPTransferPaymentDetail)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@Amount", DbType.Currency, tOPSPTransferPaymentDetail.Amount)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, tOPSPTransferPaymentDetail.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, tOPSPTransferPaymentDetail.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@SparePartBillingID", DbType.Int32, Me.GetRefObject(tOPSPTransferPaymentDetail.SparePartBilling))
            DbCommandWrapper.AddInParameter("@TOPSPTransferPaymentID", DbType.Int32, Me.GetRefObject(tOPSPTransferPaymentDetail.TOPSPTransferPayment))

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

            Dim tOPSPTransferPaymentDetail As TOPSPTransferPaymentDetail = CType(obj, TOPSPTransferPaymentDetail)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, tOPSPTransferPaymentDetail.ID)
            DbCommandWrapper.AddInParameter("@Amount", DbType.Currency, tOPSPTransferPaymentDetail.Amount)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, tOPSPTransferPaymentDetail.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, tOPSPTransferPaymentDetail.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@SparePartBillingID", DbType.Int32, Me.GetRefObject(tOPSPTransferPaymentDetail.SparePartBilling))
            DbCommandWrapper.AddInParameter("@TOPSPTransferPaymentID", DbType.Int32, Me.GetRefObject(tOPSPTransferPaymentDetail.TOPSPTransferPayment))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As TOPSPTransferPaymentDetail

            Dim tOPSPTransferPaymentDetail As TOPSPTransferPaymentDetail = New TOPSPTransferPaymentDetail

            tOPSPTransferPaymentDetail.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Amount")) Then tOPSPTransferPaymentDetail.Amount = CType(dr("Amount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then tOPSPTransferPaymentDetail.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then tOPSPTransferPaymentDetail.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then tOPSPTransferPaymentDetail.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then tOPSPTransferPaymentDetail.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then tOPSPTransferPaymentDetail.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("SparePartBillingID")) Then
                tOPSPTransferPaymentDetail.SparePartBilling = New SparePartBilling(CType(dr("SparePartBillingID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("TOPSPTransferPaymentID")) Then
                tOPSPTransferPaymentDetail.TOPSPTransferPayment = New TOPSPTransferPayment(CType(dr("TOPSPTransferPaymentID"), Integer))
            End If

            Return tOPSPTransferPaymentDetail

        End Function

        Private Sub SetTableName()

            If Not (GetType(TOPSPTransferPaymentDetail) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(TOPSPTransferPaymentDetail), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(TOPSPTransferPaymentDetail).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

