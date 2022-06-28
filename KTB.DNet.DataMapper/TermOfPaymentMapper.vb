#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : TermOfPayment Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2005 
'// ---------------------
'// $History      : $
'// Generated on 10/12/2005 - 9:20:04 AM
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

    Public Class TermOfPaymentMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertTermOfPayment"
        Private m_UpdateStatement As String = "up_UpdateTermOfPayment"
        Private m_RetrieveStatement As String = "up_RetrieveTermOfPayment"
        Private m_RetrieveListStatement As String = "up_RetrieveTermOfPaymentList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteTermOfPayment"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim termOfPayment As TermOfPayment = Nothing
            While dr.Read

                termOfPayment = Me.CreateObject(dr)

            End While

            Return termOfPayment

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim termOfPaymentList As ArrayList = New ArrayList

            While dr.Read
                Dim termOfPayment As TermOfPayment = Me.CreateObject(dr)
                termOfPaymentList.Add(termOfPayment)
            End While

            Return termOfPaymentList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim termOfPayment As TermOfPayment = CType(obj, TermOfPayment)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, termOfPayment.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim termOfPayment As TermOfPayment = CType(obj, TermOfPayment)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@TermOfPaymentCode", DbType.AnsiString, termOfPayment.TermOfPaymentCode)
            DbCommandWrapper.AddInParameter("@TermOfPaymentValue", DbType.Int16, termOfPayment.TermOfPaymentValue)
            DBCommandWrapper.AddInParameter("@PaymentType", DbType.Int16, termOfPayment.PaymentType)
            DBCommandWrapper.AddInParameter("@Description", DbType.AnsiString, termOfPayment.Description)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, termOfPayment.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, termOfPayment.LastUpdateBy)
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

            Dim termOfPayment As TermOfPayment = CType(obj, TermOfPayment)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, termOfPayment.ID)
            DbCommandWrapper.AddInParameter("@TermOfPaymentCode", DbType.AnsiString, termOfPayment.TermOfPaymentCode)
            DbCommandWrapper.AddInParameter("@TermOfPaymentValue", DbType.Int16, termOfPayment.TermOfPaymentValue)
            DBCommandWrapper.AddInParameter("@PaymentType", DbType.Int16, termOfPayment.PaymentType)
            DBCommandWrapper.AddInParameter("@Description", DbType.AnsiString, termOfPayment.Description)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, termOfPayment.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, termOfPayment.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As TermOfPayment

            Dim termOfPayment As TermOfPayment = New TermOfPayment

            termOfPayment.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("TermOfPaymentCode")) Then termOfPayment.TermOfPaymentCode = dr("TermOfPaymentCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("TermOfPaymentValue")) Then termOfPayment.TermOfPaymentValue = CType(dr("TermOfPaymentValue"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("PaymentType")) Then termOfPayment.PaymentType = CType(dr("PaymentType"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("Description")) Then termOfPayment.Description = dr("Description").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then termOfPayment.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then termOfPayment.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then termOfPayment.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then termOfPayment.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then termOfPayment.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            Return termOfPayment

        End Function

        Private Sub SetTableName()

            If Not (GetType(TermOfPayment) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(TermOfPayment), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(TermOfPayment).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

