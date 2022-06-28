#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : PaymentPurpose Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2005 
'// ---------------------
'// $History      : $
'// Generated on 10/19/2005 - 8:48:08 AM
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

    Public Class PaymentPurposeMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertPaymentPurpose"
        Private m_UpdateStatement As String = "up_UpdatePaymentPurpose"
        Private m_RetrieveStatement As String = "up_RetrievePaymentPurpose"
        Private m_RetrieveListStatement As String = "up_RetrievePaymentPurposeList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeletePaymentPurpose"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim paymentPurpose As paymentPurpose = Nothing
            While dr.Read

                paymentPurpose = Me.CreateObject(dr)

            End While

            Return paymentPurpose

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim paymentPurposeList As ArrayList = New ArrayList

            While dr.Read
                Dim paymentPurpose As paymentPurpose = Me.CreateObject(dr)
                paymentPurposeList.Add(paymentPurpose)
            End While

            Return paymentPurposeList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim paymentPurpose As paymentPurpose = CType(obj, paymentPurpose)
            DBCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DBCommandWrapper.AddInParameter("@ID", DbType.Int32, paymentPurpose.ID)
            Return DBCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim paymentPurpose As paymentPurpose = CType(obj, paymentPurpose)
            DBCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DBCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DBCommandWrapper.AddInParameter("@PaymentPurposeCode", DbType.AnsiString, paymentPurpose.PaymentPurposeCode)
            DBCommandWrapper.AddInParameter("@Description", DbType.AnsiString, paymentPurpose.Description)
            DBCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, paymentPurpose.RowStatus)
            DBCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DBCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, paymentPurpose.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


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

            Dim paymentPurpose As paymentPurpose = CType(obj, paymentPurpose)

            DBCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DBCommandWrapper.AddInParameter("@ID", DbType.Int32, paymentPurpose.ID)
            DBCommandWrapper.AddInParameter("@PaymentPurposeCode", DbType.AnsiString, paymentPurpose.PaymentPurposeCode)
            DBCommandWrapper.AddInParameter("@Description", DbType.AnsiString, paymentPurpose.Description)
            DBCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, paymentPurpose.RowStatus)
            DBCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, paymentPurpose.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DBCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)



            Return DBCommandWrapper

        End Function

#End Region

#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As PaymentPurpose

            Dim paymentPurpose As paymentPurpose = New paymentPurpose

            paymentPurpose.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("PaymentPurposeCode")) Then paymentPurpose.PaymentPurposeCode = dr("PaymentPurposeCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Description")) Then paymentPurpose.Description = dr("Description").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then paymentPurpose.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then paymentPurpose.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then paymentPurpose.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then paymentPurpose.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then paymentPurpose.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            Return paymentPurpose

        End Function

        Private Sub SetTableName()

            If Not (GetType(PaymentPurpose) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(PaymentPurpose), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(PaymentPurpose).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace