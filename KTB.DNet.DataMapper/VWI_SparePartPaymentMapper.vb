
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : Mitrais Team
'// PURPOSE       : VWI_SparePartPayment Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 03/10/2018 - 14:14:50
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

    Public Class VWI_SparePartPaymentMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertVWI_SparePartPayment"
        Private m_UpdateStatement As String = "up_UpdateVWI_SparePartPayment"
        Private m_RetrieveStatement As String = "up_RetrieveVWI_SparePartPayment"
        Private m_RetrieveListStatement As String = "up_RetrieveVWI_SparePartPaymentList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteVWI_SparePartPayment"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim VWI_SparePartPayment As VWI_SparePartPayment = Nothing
            While dr.Read

                VWI_SparePartPayment = Me.CreateObject(dr)

            End While

            Return VWI_SparePartPayment

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim VWI_SparePartPaymentList As ArrayList = New ArrayList

            While dr.Read
                Dim VWI_SparePartPayment As VWI_SparePartPayment = Me.CreateObject(dr)
                VWI_SparePartPaymentList.Add(VWI_SparePartPayment)
            End While

            Return VWI_SparePartPaymentList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim VWI_SparePartPayment As VWI_SparePartPayment = CType(obj, VWI_SparePartPayment)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.StringFixedLength, VWI_SparePartPayment.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim VWI_SparePartPayment As VWI_SparePartPayment = CType(obj, VWI_SparePartPayment)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.StringFixedLength, 10)
            DbCommandWrapper.AddInParameter("@ReferenceNo", DbType.AnsiString, VWI_SparePartPayment.ReferenceNo)
            DbCommandWrapper.AddInParameter("@InvoiceNo", DbType.AnsiString, VWI_SparePartPayment.InvoiceNo)
            DbCommandWrapper.AddInParameter("@PostingDate", DbType.DateTime, VWI_SparePartPayment.PostingDate)
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, VWI_SparePartPayment.DealerID)
            DbCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, VWI_SparePartPayment.DealerCode)
            DbCommandWrapper.AddInParameter("@SONumber", DbType.AnsiString, VWI_SparePartPayment.SONumber)
            DbCommandWrapper.AddInParameter("@Amount", DbType.Currency, VWI_SparePartPayment.Amount)
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

            Dim VWI_SparePartPayment As VWI_SparePartPayment = CType(obj, VWI_SparePartPayment)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.StringFixedLength, VWI_SparePartPayment.ID)
            DbCommandWrapper.AddInParameter("@ReferenceNo", DbType.AnsiString, VWI_SparePartPayment.ReferenceNo)
            DbCommandWrapper.AddInParameter("@InvoiceNo", DbType.AnsiString, VWI_SparePartPayment.InvoiceNo)
            DbCommandWrapper.AddInParameter("@PostingDate", DbType.DateTime, VWI_SparePartPayment.PostingDate)
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, VWI_SparePartPayment.DealerID)
            DbCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, VWI_SparePartPayment.DealerCode)
            DbCommandWrapper.AddInParameter("@SONumber", DbType.AnsiString, VWI_SparePartPayment.SONumber)
            DbCommandWrapper.AddInParameter("@Amount", DbType.Currency, VWI_SparePartPayment.Amount)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As VWI_SparePartPayment

            Dim VWI_SparePartPayment As VWI_SparePartPayment = New VWI_SparePartPayment

            'VWI_SparePartPayment.ID = dr("ID").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ReferenceNo")) Then VWI_SparePartPayment.ReferenceNo = dr("ReferenceNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("InvoiceNo")) Then VWI_SparePartPayment.InvoiceNo = dr("InvoiceNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PostingDate")) Then VWI_SparePartPayment.PostingDate = CType(dr("PostingDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then VWI_SparePartPayment.DealerID = CType(dr("DealerID"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerCode")) Then VWI_SparePartPayment.DealerCode = dr("DealerCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SONumber")) Then VWI_SparePartPayment.SONumber = dr("SONumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DMSPRNo")) Then VWI_SparePartPayment.DMSPRNo = dr("DMSPRNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("OrderType")) Then VWI_SparePartPayment.OrderType = dr("OrderType").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Amount")) Then VWI_SparePartPayment.Amount = CType(dr("Amount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("BillingAmount")) Then VWI_SparePartPayment.BillingAmount = CType(dr("BillingAmount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then VWI_SparePartPayment.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("IsTOP")) Then VWI_SparePartPayment.IsTOP = CType(dr("IsTOP"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("IsPenalty")) Then VWI_SparePartPayment.IsPenalty = CType(dr("IsPenalty"), Short)
            Return VWI_SparePartPayment

        End Function

        Private Sub SetTableName()

            If Not (GetType(VWI_SparePartPayment) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(VWI_SparePartPayment), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(VWI_SparePartPayment).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

