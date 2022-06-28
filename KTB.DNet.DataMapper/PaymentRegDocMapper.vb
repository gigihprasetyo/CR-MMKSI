#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : PaymentRegDoc Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2007 
'// ---------------------
'// $History      : $
'// Generated on 10/26/2007 - 2:42:03 PM
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

    Public Class PaymentRegDocMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertPaymentRegDoc"
        Private m_UpdateStatement As String = "up_UpdatePaymentRegDoc"
        Private m_RetrieveStatement As String = "up_RetrievePaymentRegDoc"
        Private m_RetrieveListStatement As String = "up_RetrievePaymentRegDocList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeletePaymentRegDoc"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim paymentRegDoc As PaymentRegDoc = Nothing
            While dr.Read

                paymentRegDoc = Me.CreateObject(dr)

            End While

            Return paymentRegDoc

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim paymentRegDocList As ArrayList = New ArrayList

            While dr.Read
                Dim paymentRegDoc As PaymentRegDoc = Me.CreateObject(dr)
                paymentRegDocList.Add(paymentRegDoc)
            End While

            Return paymentRegDocList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim paymentRegDoc As PaymentRegDoc = CType(obj, PaymentRegDoc)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, paymentRegDoc.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim paymentRegDoc As PaymentRegDoc = CType(obj, PaymentRegDoc)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@CreateTime", DbType.DateTime, paymentRegDoc.CreateTime)
            DbCommandWrapper.AddInParameter("@IpAddress", DbType.AnsiString, paymentRegDoc.IpAddress)
            DbCommandWrapper.AddInParameter("@BORNumber", DbType.AnsiString, paymentRegDoc.BORNumber)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, paymentRegDoc.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, paymentRegDoc.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@UserInfoID", DbType.Int32, Me.GetRefObject(paymentRegDoc.UserInfo))
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, Me.GetRefObject(paymentRegDoc.Dealer))

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

            Dim paymentRegDoc As PaymentRegDoc = CType(obj, PaymentRegDoc)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, paymentRegDoc.ID)
            DbCommandWrapper.AddInParameter("@CreateTime", DbType.DateTime, paymentRegDoc.CreateTime)
            DbCommandWrapper.AddInParameter("@IpAddress", DbType.AnsiString, paymentRegDoc.IpAddress)
            DbCommandWrapper.AddInParameter("@BORNumber", DbType.AnsiString, paymentRegDoc.BORNumber)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, paymentRegDoc.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, paymentRegDoc.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@UserInfoID", DbType.Int32, Me.GetRefObject(paymentRegDoc.UserInfo))
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, Me.GetRefObject(paymentRegDoc.Dealer))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As PaymentRegDoc

            Dim paymentRegDoc As PaymentRegDoc = New PaymentRegDoc

            paymentRegDoc.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("CreateTime")) Then paymentRegDoc.CreateTime = CType(dr("CreateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("IpAddress")) Then paymentRegDoc.IpAddress = dr("IpAddress").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("BORNumber")) Then paymentRegDoc.BORNumber = dr("BORNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then paymentRegDoc.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then paymentRegDoc.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then paymentRegDoc.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then paymentRegDoc.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then paymentRegDoc.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("UserInfoID")) Then
                paymentRegDoc.UserInfo = New UserInfo(CType(dr("UserInfoID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then
                paymentRegDoc.Dealer = New Dealer(CType(dr("DealerID"), Integer))
            End If

            Return paymentRegDoc

        End Function

        Private Sub SetTableName()

            If Not (GetType(PaymentRegDoc) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(PaymentRegDoc), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(PaymentRegDoc).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

