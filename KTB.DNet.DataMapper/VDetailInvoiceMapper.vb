
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : VDetailInvoice Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2008 
'// ---------------------
'// $History      : $
'// Generated on 12/1/2008 - 9:29:06 AM
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

    Public Class VDetailInvoiceMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertVDetailInvoice"
        Private m_UpdateStatement As String = "up_UpdateVDetailInvoice"
        Private m_RetrieveStatement As String = "up_RetrieveVDetailInvoice"
        Private m_RetrieveListStatement As String = "up_RetrieveVDetailInvoiceList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteVDetailInvoice"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim vDetailInvoice As VDetailInvoice = Nothing
            While dr.Read

                vDetailInvoice = Me.CreateObject(dr)

            End While

            Return vDetailInvoice

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim vDetailInvoiceList As ArrayList = New ArrayList

            While dr.Read
                Dim vDetailInvoice As VDetailInvoice = Me.CreateObject(dr)
                vDetailInvoiceList.Add(vDetailInvoice)
            End While

            Return vDetailInvoiceList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim vDetailInvoice As VDetailInvoice = CType(obj, VDetailInvoice)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, vDetailInvoice.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim vDetailInvoice As VDetailInvoice = CType(obj, VDetailInvoice)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, vDetailInvoice.DealerCode)
            DbCommandWrapper.AddInParameter("@SearchTerm1", DbType.AnsiString, vDetailInvoice.SearchTerm1)
            DbCommandWrapper.AddInParameter("@DealerName", DbType.AnsiString, vDetailInvoice.DealerName)
            DbCommandWrapper.AddInParameter("@DealerPONumber", DbType.AnsiString, vDetailInvoice.DealerPONumber)
            DbCommandWrapper.AddInParameter("@SONumber", DbType.AnsiString, vDetailInvoice.SONumber)
            DbCommandWrapper.AddInParameter("@TglPengajuan", DbType.DateTime, vDetailInvoice.TglPengajuan)
            DbCommandWrapper.AddInParameter("@ReqAllocationDateTime", DbType.DateTime, vDetailInvoice.ReqAllocationDateTime)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, vDetailInvoice.Description)
            DbCommandWrapper.AddInParameter("@CityName", DbType.AnsiString, vDetailInvoice.CityName)
            DbCommandWrapper.AddInParameter("@ContractNumber", DbType.AnsiString, vDetailInvoice.ContractNumber)
            DbCommandWrapper.AddInParameter("@ContractType", DbType.Int16, vDetailInvoice.ContractType)
            DbCommandWrapper.AddInParameter("@CategoryCode", DbType.AnsiString, vDetailInvoice.CategoryCode)
            DbCommandWrapper.AddInParameter("@ProductionYear", DbType.Int16, vDetailInvoice.ProductionYear)
            DbCommandWrapper.AddInParameter("@ProjectName", DbType.AnsiString, vDetailInvoice.ProjectName)
            DbCommandWrapper.AddInParameter("@POType", DbType.AnsiStringFixedLength, vDetailInvoice.POType)
            DbCommandWrapper.AddInParameter("@Amount", DbType.Currency, vDetailInvoice.Amount)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, vDetailInvoice.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, vDetailInvoice.LastUpdateBy)
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

            Dim vDetailInvoice As VDetailInvoice = CType(obj, VDetailInvoice)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, vDetailInvoice.ID)
            DbCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, vDetailInvoice.DealerCode)
            DbCommandWrapper.AddInParameter("@SearchTerm1", DbType.AnsiString, vDetailInvoice.SearchTerm1)
            DbCommandWrapper.AddInParameter("@DealerName", DbType.AnsiString, vDetailInvoice.DealerName)
            DbCommandWrapper.AddInParameter("@DealerPONumber", DbType.AnsiString, vDetailInvoice.DealerPONumber)
            DbCommandWrapper.AddInParameter("@SONumber", DbType.AnsiString, vDetailInvoice.SONumber)
            DbCommandWrapper.AddInParameter("@TglPengajuan", DbType.DateTime, vDetailInvoice.TglPengajuan)
            DbCommandWrapper.AddInParameter("@ReqAllocationDateTime", DbType.DateTime, vDetailInvoice.ReqAllocationDateTime)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, vDetailInvoice.Description)
            DbCommandWrapper.AddInParameter("@CityName", DbType.AnsiString, vDetailInvoice.CityName)
            DbCommandWrapper.AddInParameter("@ContractNumber", DbType.AnsiString, vDetailInvoice.ContractNumber)
            DbCommandWrapper.AddInParameter("@ContractType", DbType.Int16, vDetailInvoice.ContractType)
            DbCommandWrapper.AddInParameter("@CategoryCode", DbType.AnsiString, vDetailInvoice.CategoryCode)
            DbCommandWrapper.AddInParameter("@ProductionYear", DbType.Int16, vDetailInvoice.ProductionYear)
            DbCommandWrapper.AddInParameter("@ProjectName", DbType.AnsiString, vDetailInvoice.ProjectName)
            DbCommandWrapper.AddInParameter("@POType", DbType.AnsiStringFixedLength, vDetailInvoice.POType)
            DbCommandWrapper.AddInParameter("@Amount", DbType.Currency, vDetailInvoice.Amount)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, vDetailInvoice.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, vDetailInvoice.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As VDetailInvoice

            Dim vDetailInvoice As VDetailInvoice = New VDetailInvoice

            vDetailInvoice.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerCode")) Then vDetailInvoice.DealerCode = dr("DealerCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SearchTerm1")) Then vDetailInvoice.SearchTerm1 = dr("SearchTerm1").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DealerName")) Then vDetailInvoice.DealerName = dr("DealerName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DealerPONumber")) Then vDetailInvoice.DealerPONumber = dr("DealerPONumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SONumber")) Then vDetailInvoice.SONumber = dr("SONumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("TglPengajuan")) Then vDetailInvoice.TglPengajuan = CType(dr("TglPengajuan"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("ReqAllocationDateTime")) Then vDetailInvoice.ReqAllocationDateTime = CType(dr("ReqAllocationDateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("Description")) Then vDetailInvoice.Description = dr("Description").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CityName")) Then vDetailInvoice.CityName = dr("CityName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ContractNumber")) Then vDetailInvoice.ContractNumber = dr("ContractNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ContractType")) Then vDetailInvoice.ContractType = CType(dr("ContractType"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CategoryCode")) Then vDetailInvoice.CategoryCode = dr("CategoryCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ProductionYear")) Then vDetailInvoice.ProductionYear = CType(dr("ProductionYear"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("ProjectName")) Then vDetailInvoice.ProjectName = dr("ProjectName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("POType")) Then vDetailInvoice.POType = dr("POType").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Amount")) Then vDetailInvoice.Amount = CType(dr("Amount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then vDetailInvoice.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then vDetailInvoice.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then vDetailInvoice.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then vDetailInvoice.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then vDetailInvoice.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            Return vDetailInvoice

        End Function

        Private Sub SetTableName()

            If Not (GetType(VDetailInvoice) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(VDetailInvoice), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(VDetailInvoice).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

