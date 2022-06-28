
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : v_SOPayment Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2016 
'// ---------------------
'// $History      : $
'// Generated on 09/08/2016 - 16:43:56
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

    Public Class v_SOPaymentMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_Insertv_SOPayment"
        Private m_UpdateStatement As String = "up_Updatev_SOPayment"
        Private m_RetrieveStatement As String = "up_Retrievev_SOPayment"
        Private m_RetrieveListStatement As String = "up_Retrievev_SOPaymentList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_Deletev_SOPayment"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim v_SOPayment As v_SOPayment = Nothing
            While dr.Read

                v_SOPayment = Me.CreateObject(dr)

            End While

            Return v_SOPayment

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim v_SOPaymentList As ArrayList = New ArrayList

            While dr.Read
                Dim v_SOPayment As v_SOPayment = Me.CreateObject(dr)
                v_SOPaymentList.Add(v_SOPayment)
            End While

            Return v_SOPaymentList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim v_SOPayment As v_SOPayment = CType(obj, v_SOPayment)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, v_SOPayment.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim v_SOPayment As v_SOPayment = CType(obj, v_SOPayment)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@SONumber", DbType.AnsiString, v_SOPayment.SONumber)
            DbCommandWrapper.AddInParameter("@PONumber", DbType.AnsiString, v_SOPayment.PONumber)
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, v_SOPayment.DealerID)
            DbCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, v_SOPayment.DealerCode)
            DbCommandWrapper.AddInParameter("@TotalVH", DbType.Currency, v_SOPayment.TotalVH)
            DbCommandWrapper.AddInParameter("@TotalPP", DbType.Currency, v_SOPayment.TotalPP)
            DbCommandWrapper.AddInParameter("@TotalIT", DbType.Currency, v_SOPayment.TotalIT)
            DbCommandWrapper.AddInParameter("@IsFullyPaidVH", DbType.Int32, v_SOPayment.IsFullyPaidVH)
            DbCommandWrapper.AddInParameter("@IsFullyPaidPP", DbType.Int32, v_SOPayment.IsFullyPaidPP)
            DbCommandWrapper.AddInParameter("@IsFullyPaidIT", DbType.Int32, v_SOPayment.IsFullyPaidIT)
            DbCommandWrapper.AddInParameter("@PaymentVH", DbType.Currency, v_SOPayment.PaymentVH)
            DbCommandWrapper.AddInParameter("@PaymentPP", DbType.Currency, v_SOPayment.PaymentPP)
            DbCommandWrapper.AddInParameter("@PaymentIT", DbType.Currency, v_SOPayment.PaymentIT)


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

            Dim v_SOPayment As v_SOPayment = CType(obj, v_SOPayment)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, v_SOPayment.ID)
            DbCommandWrapper.AddInParameter("@SONumber", DbType.AnsiString, v_SOPayment.SONumber)
            DbCommandWrapper.AddInParameter("@PONumber", DbType.AnsiString, v_SOPayment.PONumber)
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, v_SOPayment.DealerID)
            DbCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, v_SOPayment.DealerCode)
            DbCommandWrapper.AddInParameter("@TotalVH", DbType.Currency, v_SOPayment.TotalVH)
            DbCommandWrapper.AddInParameter("@TotalPP", DbType.Currency, v_SOPayment.TotalPP)
            DbCommandWrapper.AddInParameter("@TotalIT", DbType.Currency, v_SOPayment.TotalIT)
            DbCommandWrapper.AddInParameter("@IsFullyPaidVH", DbType.Int32, v_SOPayment.IsFullyPaidVH)
            DbCommandWrapper.AddInParameter("@IsFullyPaidPP", DbType.Int32, v_SOPayment.IsFullyPaidPP)
            DbCommandWrapper.AddInParameter("@IsFullyPaidIT", DbType.Int32, v_SOPayment.IsFullyPaidIT)
            DbCommandWrapper.AddInParameter("@PaymentVH", DbType.Currency, v_SOPayment.PaymentVH)
            DbCommandWrapper.AddInParameter("@PaymentPP", DbType.Currency, v_SOPayment.PaymentPP)
            DbCommandWrapper.AddInParameter("@PaymentIT", DbType.Currency, v_SOPayment.PaymentIT)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As v_SOPayment

            Dim v_SOPayment As v_SOPayment = New v_SOPayment

            v_SOPayment.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("SONumber")) Then v_SOPayment.SONumber = dr("SONumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PONumber")) Then v_SOPayment.PONumber = dr("PONumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then v_SOPayment.DealerID = CType(dr("DealerID"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerCode")) Then v_SOPayment.DealerCode = dr("DealerCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("TotalVH")) Then v_SOPayment.TotalVH = CType(dr("TotalVH"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("TotalPP")) Then v_SOPayment.TotalPP = CType(dr("TotalPP"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("TotalIT")) Then v_SOPayment.TotalIT = CType(dr("TotalIT"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("IsFullyPaidVH")) Then v_SOPayment.IsFullyPaidVH = CType(dr("IsFullyPaidVH"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("IsFullyPaidPP")) Then v_SOPayment.IsFullyPaidPP = CType(dr("IsFullyPaidPP"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("IsFullyPaidIT")) Then v_SOPayment.IsFullyPaidIT = CType(dr("IsFullyPaidIT"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("PaymentVH")) Then v_SOPayment.PaymentVH = CType(dr("PaymentVH"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("PaymentPP")) Then v_SOPayment.PaymentPP = CType(dr("PaymentPP"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("PaymentIT")) Then v_SOPayment.PaymentIT = CType(dr("PaymentIT"), Decimal)

            Return v_SOPayment

        End Function

        Private Sub SetTableName()

            If Not (GetType(v_SOPayment) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(v_SOPayment), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(v_SOPayment).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

