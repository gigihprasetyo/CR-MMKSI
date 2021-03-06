#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : PaymentObligationType Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2007 
'// ---------------------
'// $History      : $
'// Generated on 10/25/2007 - 8:46:19 AM
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

    Public Class PaymentObligationTypeMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertPaymentObligationType"
        Private m_UpdateStatement As String = "up_UpdatePaymentObligationType"
        Private m_RetrieveStatement As String = "up_RetrievePaymentObligationType"
        Private m_RetrieveListStatement As String = "up_RetrievePaymentObligationTypeList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeletePaymentObligationType"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim paymentObligationType As PaymentObligationType = Nothing
            While dr.Read

                paymentObligationType = Me.CreateObject(dr)

            End While

            Return paymentObligationType

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim paymentObligationTypeList As ArrayList = New ArrayList

            While dr.Read
                Dim paymentObligationType As PaymentObligationType = Me.CreateObject(dr)
                paymentObligationTypeList.Add(paymentObligationType)
            End While

            Return paymentObligationTypeList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim paymentObligationType As PaymentObligationType = CType(obj, PaymentObligationType)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, paymentObligationType.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim paymentObligationType As PaymentObligationType = CType(obj, PaymentObligationType)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@Code", DbType.AnsiString, paymentObligationType.Code)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, paymentObligationType.Description)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int32, paymentObligationType.Status)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, paymentObligationType.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, paymentObligationType.LastUpdateBy)
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

            Dim paymentObligationType As PaymentObligationType = CType(obj, PaymentObligationType)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, paymentObligationType.ID)
            DbCommandWrapper.AddInParameter("@Code", DbType.AnsiString, paymentObligationType.Code)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, paymentObligationType.Description)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int32, paymentObligationType.Status)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, paymentObligationType.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, paymentObligationType.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As PaymentObligationType

            Dim paymentObligationType As PaymentObligationType = New PaymentObligationType

            paymentObligationType.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Code")) Then paymentObligationType.Code = dr("Code").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Description")) Then paymentObligationType.Description = dr("Description").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then paymentObligationType.Status = CType(dr("Status"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then paymentObligationType.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then paymentObligationType.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then paymentObligationType.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then paymentObligationType.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then paymentObligationType.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            Return paymentObligationType

        End Function

        Private Sub SetTableName()

            If Not (GetType(PaymentObligationType) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(PaymentObligationType), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(PaymentObligationType).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

