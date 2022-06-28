
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : VInvoice Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2008 
'// ---------------------
'// $History      : $
'// Generated on 12/3/2008 - 11:18:38 AM
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

    Public Class VInvoiceMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertVInvoice"
        Private m_UpdateStatement As String = "up_UpdateVInvoice"
        Private m_RetrieveStatement As String = "up_RetrieveVInvoice"
        Private m_RetrieveListStatement As String = "up_RetrieveVInvoiceList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteVInvoice"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim vInvoice As VInvoice = Nothing
            While dr.Read

                vInvoice = Me.CreateObject(dr)

            End While

            Return vInvoice

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim vInvoiceList As ArrayList = New ArrayList

            While dr.Read
                Dim vInvoice As VInvoice = Me.CreateObject(dr)
                vInvoiceList.Add(vInvoice)
            End While

            Return vInvoiceList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim vInvoice As VInvoice = CType(obj, VInvoice)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, vInvoice.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim vInvoice As VInvoice = CType(obj, VInvoice)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, vInvoice.DealerCode)
            DbCommandWrapper.AddInParameter("@ProjectName", DbType.AnsiString, vInvoice.ProjectName)
            DbCommandWrapper.AddInParameter("@SONumber", DbType.AnsiString, vInvoice.SONumber)
            DbCommandWrapper.AddInParameter("@DealerPONumber", DbType.AnsiString, vInvoice.DealerPONumber)
            DbCommandWrapper.AddInParameter("@ContractNumber", DbType.AnsiString, vInvoice.ContractNumber)
            DbCommandWrapper.AddInParameter("@CategoryCode", DbType.AnsiString, vInvoice.CategoryCode)
            DbCommandWrapper.AddInParameter("@InvoiceNumber", DbType.AnsiString, vInvoice.InvoiceNumber)
            DbCommandWrapper.AddInParameter("@InvoiceDate", DbType.DateTime, vInvoice.InvoiceDate)
            DbCommandWrapper.AddInParameter("@Amount", DbType.Currency, vInvoice.Amount)
            DbCommandWrapper.AddInParameter("@Cancelled", DbType.AnsiString, vInvoice.Cancelled)
            DbCommandWrapper.AddInParameter("@InvoiceType", DbType.AnsiString, vInvoice.InvoiceType)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, vInvoice.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, vInvoice.LastUpdateBy)
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

            Dim vInvoice As VInvoice = CType(obj, VInvoice)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, vInvoice.ID)
            DbCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, vInvoice.DealerCode)
            DbCommandWrapper.AddInParameter("@ProjectName", DbType.AnsiString, vInvoice.ProjectName)
            DbCommandWrapper.AddInParameter("@SONumber", DbType.AnsiString, vInvoice.SONumber)
            DbCommandWrapper.AddInParameter("@DealerPONumber", DbType.AnsiString, vInvoice.DealerPONumber)
            DbCommandWrapper.AddInParameter("@ContractNumber", DbType.AnsiString, vInvoice.ContractNumber)
            DbCommandWrapper.AddInParameter("@CategoryCode", DbType.AnsiString, vInvoice.CategoryCode)
            DbCommandWrapper.AddInParameter("@InvoiceNumber", DbType.AnsiString, vInvoice.InvoiceNumber)
            DbCommandWrapper.AddInParameter("@InvoiceDate", DbType.DateTime, vInvoice.InvoiceDate)
            DbCommandWrapper.AddInParameter("@Amount", DbType.Currency, vInvoice.Amount)
            DbCommandWrapper.AddInParameter("@Cancelled", DbType.AnsiString, vInvoice.Cancelled)
            DbCommandWrapper.AddInParameter("@InvoiceType", DbType.AnsiString, vInvoice.InvoiceType)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, vInvoice.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, vInvoice.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As VInvoice

            Dim vInvoice As VInvoice = New VInvoice

            vInvoice.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerCode")) Then vInvoice.DealerCode = dr("DealerCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ProjectName")) Then vInvoice.ProjectName = dr("ProjectName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SONumber")) Then vInvoice.SONumber = dr("SONumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DealerPONumber")) Then vInvoice.DealerPONumber = dr("DealerPONumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ContractNumber")) Then vInvoice.ContractNumber = dr("ContractNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CategoryCode")) Then vInvoice.CategoryCode = dr("CategoryCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("InvoiceNumber")) Then vInvoice.InvoiceNumber = dr("InvoiceNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("InvoiceDate")) Then vInvoice.InvoiceDate = CType(dr("InvoiceDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("Amount")) Then vInvoice.Amount = CType(dr("Amount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("Cancelled")) Then vInvoice.Cancelled = dr("Cancelled").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("InvoiceType")) Then vInvoice.InvoiceType = dr("InvoiceType").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then vInvoice.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then vInvoice.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then vInvoice.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then vInvoice.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then vInvoice.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            Return vInvoice

        End Function

        Private Sub SetTableName()

            If Not (GetType(VInvoice) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(VInvoice), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(VInvoice).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

