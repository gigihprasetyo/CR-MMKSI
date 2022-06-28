
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : v_SOPaymentDetail Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2016 
'// ---------------------
'// $History      : $
'// Generated on 09/08/2016 - 16:44:37
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

    Public Class v_SOPaymentDetailMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_Insertv_SOPaymentDetail"
        Private m_UpdateStatement As String = "up_Updatev_SOPaymentDetail"
        Private m_RetrieveStatement As String = "up_Retrievev_SOPaymentDetail"
        Private m_RetrieveListStatement As String = "up_Retrievev_SOPaymentDetailList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_Deletev_SOPaymentDetail"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim v_SOPaymentDetail As v_SOPaymentDetail = Nothing
            While dr.Read

                v_SOPaymentDetail = Me.CreateObject(dr)

            End While

            Return v_SOPaymentDetail

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim v_SOPaymentDetailList As ArrayList = New ArrayList

            While dr.Read
                Dim v_SOPaymentDetail As v_SOPaymentDetail = Me.CreateObject(dr)
                v_SOPaymentDetailList.Add(v_SOPaymentDetail)
            End While

            Return v_SOPaymentDetailList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim v_SOPaymentDetail As v_SOPaymentDetail = CType(obj, v_SOPaymentDetail)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, v_SOPaymentDetail.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim v_SOPaymentDetail As v_SOPaymentDetail = CType(obj, v_SOPaymentDetail)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@SONumber", DbType.AnsiString, v_SOPaymentDetail.SONumber)
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, v_SOPaymentDetail.DealerID)
            DbCommandWrapper.AddInParameter("@PONumber", DbType.AnsiString, v_SOPaymentDetail.PONumber)
            DbCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, v_SOPaymentDetail.DealerCode)
            DbCommandWrapper.AddInParameter("@TotalVH", DbType.Currency, v_SOPaymentDetail.TotalVH)
            DbCommandWrapper.AddInParameter("@TotalPP", DbType.Currency, v_SOPaymentDetail.TotalPP)
            DbCommandWrapper.AddInParameter("@TotalIT", DbType.Currency, v_SOPaymentDetail.TotalIT)
            DbCommandWrapper.AddInParameter("@RegNumber", DbType.AnsiString, v_SOPaymentDetail.RegNumber)
            DbCommandWrapper.AddInParameter("@PaymentPurposeID", DbType.Byte, v_SOPaymentDetail.PaymentPurposeID)
            DbCommandWrapper.AddInParameter("@PaymentPurposeCode", DbType.AnsiString, v_SOPaymentDetail.PaymentPurposeCode)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@DueDate", DbType.DateTime, v_SOPaymentDetail.DueDate)
            DbCommandWrapper.AddInParameter("@PlanTransferDate", DbType.DateTime, v_SOPaymentDetail.PlanTransferDate)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, v_SOPaymentDetail.Status)
            DbCommandWrapper.AddInParameter("@Amount", DbType.Currency, v_SOPaymentDetail.Amount)


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

            Dim v_SOPaymentDetail As v_SOPaymentDetail = CType(obj, v_SOPaymentDetail)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, v_SOPaymentDetail.ID)
            DbCommandWrapper.AddInParameter("@SONumber", DbType.AnsiString, v_SOPaymentDetail.SONumber)
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, v_SOPaymentDetail.DealerID)
            DbCommandWrapper.AddInParameter("@PONumber", DbType.AnsiString, v_SOPaymentDetail.PONumber)
            DbCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, v_SOPaymentDetail.DealerCode)
            DbCommandWrapper.AddInParameter("@TotalVH", DbType.Currency, v_SOPaymentDetail.TotalVH)
            DbCommandWrapper.AddInParameter("@TotalPP", DbType.Currency, v_SOPaymentDetail.TotalPP)
            DbCommandWrapper.AddInParameter("@TotalIT", DbType.Currency, v_SOPaymentDetail.TotalIT)
            DbCommandWrapper.AddInParameter("@RegNumber", DbType.AnsiString, v_SOPaymentDetail.RegNumber)
            DbCommandWrapper.AddInParameter("@PaymentPurposeID", DbType.Byte, v_SOPaymentDetail.PaymentPurposeID)
            DbCommandWrapper.AddInParameter("@PaymentPurposeCode", DbType.AnsiString, v_SOPaymentDetail.PaymentPurposeCode)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@DueDate", DbType.DateTime, v_SOPaymentDetail.DueDate)
            DbCommandWrapper.AddInParameter("@PlanTransferDate", DbType.DateTime, v_SOPaymentDetail.PlanTransferDate)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, v_SOPaymentDetail.Status)
            DbCommandWrapper.AddInParameter("@Amount", DbType.Currency, v_SOPaymentDetail.Amount)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As v_SOPaymentDetail

            Dim v_SOPaymentDetail As v_SOPaymentDetail = New v_SOPaymentDetail

            v_SOPaymentDetail.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("SONumber")) Then v_SOPaymentDetail.SONumber = dr("SONumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then v_SOPaymentDetail.DealerID = CType(dr("DealerID"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("PONumber")) Then v_SOPaymentDetail.PONumber = dr("PONumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DealerCode")) Then v_SOPaymentDetail.DealerCode = dr("DealerCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("TotalVH")) Then v_SOPaymentDetail.TotalVH = CType(dr("TotalVH"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("TotalPP")) Then v_SOPaymentDetail.TotalPP = CType(dr("TotalPP"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("TotalIT")) Then v_SOPaymentDetail.TotalIT = CType(dr("TotalIT"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("RegNumber")) Then v_SOPaymentDetail.RegNumber = dr("RegNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PaymentPurposeID")) Then v_SOPaymentDetail.PaymentPurposeID = CType(dr("PaymentPurposeID"), Byte)
            If Not dr.IsDBNull(dr.GetOrdinal("PaymentPurposeCode")) Then v_SOPaymentDetail.PaymentPurposeCode = dr("PaymentPurposeCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then v_SOPaymentDetail.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("DueDate")) Then v_SOPaymentDetail.DueDate = CType(dr("DueDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("PlanTransferDate")) Then v_SOPaymentDetail.PlanTransferDate = CType(dr("PlanTransferDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then v_SOPaymentDetail.Status = CType(dr("Status"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("Amount")) Then v_SOPaymentDetail.Amount = CType(dr("Amount"), Decimal)

            Return v_SOPaymentDetail

        End Function

        Private Sub SetTableName()

            If Not (GetType(v_SOPaymentDetail) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(v_SOPaymentDetail), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(v_SOPaymentDetail).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

