
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : sp_PaymentTransfer Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2016 
'// ---------------------
'// $History      : $
'// Generated on 9/26/2016 - 10:16:24 AM
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

    Public Class sp_PaymentTransferMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_Insertsp_PaymentTransfer"
        Private m_UpdateStatement As String = "up_Updatesp_PaymentTransfer"
        Private m_RetrieveStatement As String = "up_Retrievesp_PaymentTransfer"
        Private m_RetrieveListStatement As String = "up_Retrievesp_PaymentTransferList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_Deletesp_PaymentTransfer"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim sp_PaymentTransfer As sp_PaymentTransfer = Nothing
            While dr.Read

                sp_PaymentTransfer = Me.CreateObject(dr)

            End While

            Return sp_PaymentTransfer

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim sp_PaymentTransferList As ArrayList = New ArrayList

            While dr.Read
                Dim sp_PaymentTransfer As sp_PaymentTransfer = Me.CreateObject(dr)
                sp_PaymentTransferList.Add(sp_PaymentTransfer)
            End While

            Return sp_PaymentTransferList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim sp_PaymentTransfer As sp_PaymentTransfer = CType(obj, sp_PaymentTransfer)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@id", DbType.Int32, sp_PaymentTransfer.id)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim sp_PaymentTransfer As sp_PaymentTransfer = CType(obj, sp_PaymentTransfer)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@id", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@SONumber", DbType.AnsiString, sp_PaymentTransfer.SONumber)
            DbCommandWrapper.AddInParameter("@DueDate", DbType.DateTime, sp_PaymentTransfer.DueDate)
            DbCommandWrapper.AddInParameter("@TotalVH", DbType.Currency, sp_PaymentTransfer.TotalVH)
            DbCommandWrapper.AddInParameter("@TotalPP", DbType.Currency, sp_PaymentTransfer.TotalPP)
            DbCommandWrapper.AddInParameter("@TotalIT", DbType.Currency, sp_PaymentTransfer.TotalIT)
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, sp_PaymentTransfer.DealerID)
            DbCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, sp_PaymentTransfer.DealerCode)
            DbCommandWrapper.AddInParameter("@DealerName", DbType.AnsiString, sp_PaymentTransfer.DealerName)
            DbCommandWrapper.AddInParameter("@CityName", DbType.AnsiString, sp_PaymentTransfer.CityName)


            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetNewID(ByVal dbCommandWrapper As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper) As Integer

            Return CType(dbCommandWrapper.GetParameterValue("@id"), Integer)

        End Function

        Protected Overrides Function GetPagingRetrieveCommand() As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_PagingQuery)
            DbCommandWrapper.AddInParameter("@Table", DbType.String, m_TableName)
            DbCommandWrapper.AddInParameter("@PK", DbType.String, "id")

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
            DbCommandWrapper.AddInParameter("@id", DbType.Int32, id)

            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetUpdateParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim sp_PaymentTransfer As sp_PaymentTransfer = CType(obj, sp_PaymentTransfer)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@id", DbType.Int32, sp_PaymentTransfer.id)
            DbCommandWrapper.AddInParameter("@SONumber", DbType.AnsiString, sp_PaymentTransfer.SONumber)
            DbCommandWrapper.AddInParameter("@DueDate", DbType.DateTime, sp_PaymentTransfer.DueDate)
            DbCommandWrapper.AddInParameter("@TotalVH", DbType.Currency, sp_PaymentTransfer.TotalVH)
            DbCommandWrapper.AddInParameter("@TotalPP", DbType.Currency, sp_PaymentTransfer.TotalPP)
            DbCommandWrapper.AddInParameter("@TotalIT", DbType.Currency, sp_PaymentTransfer.TotalIT)
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, sp_PaymentTransfer.DealerID)
            DbCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, sp_PaymentTransfer.DealerCode)
            DbCommandWrapper.AddInParameter("@DealerName", DbType.AnsiString, sp_PaymentTransfer.DealerName)
            DbCommandWrapper.AddInParameter("@CityName", DbType.AnsiString, sp_PaymentTransfer.CityName)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As sp_PaymentTransfer

            Dim sp_PaymentTransfer As sp_PaymentTransfer = New sp_PaymentTransfer

            sp_PaymentTransfer.id = CType(dr("id"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("SONumber")) Then sp_PaymentTransfer.SONumber = dr("SONumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PaymentPurposeCode")) Then sp_PaymentTransfer.PaymentPurposeCode = dr("PaymentPurposeCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreditAccount")) Then sp_PaymentTransfer.CreditAccount = dr("CreditAccount").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DueDate")) Then sp_PaymentTransfer.DueDate = CType(dr("DueDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("TotalAmount")) Then sp_PaymentTransfer.TotalAmount = CType(dr("TotalAmount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("TotalVH")) Then sp_PaymentTransfer.TotalVH = CType(dr("TotalVH"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("TotalPP")) Then sp_PaymentTransfer.TotalPP = CType(dr("TotalPP"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("TotalIT")) Then sp_PaymentTransfer.TotalIT = CType(dr("TotalIT"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then sp_PaymentTransfer.DealerID = CType(dr("DealerID"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerCode")) Then sp_PaymentTransfer.DealerCode = dr("DealerCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DealerName")) Then sp_PaymentTransfer.DealerName = dr("DealerName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CityName")) Then sp_PaymentTransfer.CityName = dr("CityName").ToString

            Return sp_PaymentTransfer

        End Function

        Private Sub SetTableName()

            If Not (GetType(sp_PaymentTransfer) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(sp_PaymentTransfer), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(sp_PaymentTransfer).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

