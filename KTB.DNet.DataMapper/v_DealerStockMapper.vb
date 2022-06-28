
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : v_DealerStock Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2009 
'// ---------------------
'// $History      : $
'// Generated on 12/3/2009 - 9:22:43 AM
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

    Public Class v_DealerStockMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_Insertv_DealerStock"
        Private m_UpdateStatement As String = "up_Updatev_DealerStock"
        Private m_RetrieveStatement As String = "up_Retrievev_DealerStock"
        Private m_RetrieveListStatement As String = "up_Retrievev_DealerStockList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_Deletev_DealerStock"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim v_DealerStock As v_DealerStock = Nothing
            While dr.Read

                v_DealerStock = Me.CreateObject(dr)

            End While

            Return v_DealerStock

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim v_DealerStockList As ArrayList = New ArrayList

            While dr.Read
                Dim v_DealerStock As v_DealerStock = Me.CreateObject(dr)
                v_DealerStockList.Add(v_DealerStock)
            End While

            Return v_DealerStockList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim v_DealerStock As v_DealerStock = CType(obj, v_DealerStock)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, v_DealerStock.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim v_DealerStock As v_DealerStock = CType(obj, v_DealerStock)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@FakturStatus", DbType.AnsiString, v_DealerStock.FakturStatus)
            DBCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, v_DealerStock.DealerCode)
            DBCommandWrapper.AddInParameter("@SearchTerm1", DbType.AnsiString, v_DealerStock.SearchTerm1)
            DBCommandWrapper.AddInParameter("@GroupName", DbType.AnsiString, v_DealerStock.GroupName)
            DBCommandWrapper.AddInParameter("@VechileTypeCode", DbType.AnsiString, v_DealerStock.VechileTypeCode)
            DBCommandWrapper.AddInParameter("@ColorCode", DbType.AnsiString, v_DealerStock.ColorCode)
            DBCommandWrapper.AddInParameter("@ChassisNumber", DbType.AnsiString, v_DealerStock.ChassisNumber)
            DBCommandWrapper.AddInParameter("@Description", DbType.AnsiString, v_DealerStock.Description)
            DBCommandWrapper.AddInParameter("@ProjectName", DbType.AnsiString, v_DealerStock.ProjectName)
            DBCommandWrapper.AddInParameter("@Name1", DbType.AnsiString, v_DealerStock.Name1)
            DBCommandWrapper.AddInParameter("@ProvinceName", DbType.AnsiString, v_DealerStock.ProvinceName)
            DBCommandWrapper.AddInParameter("@DODate", DbType.DateTime, v_DealerStock.DODate)
            DbCommandWrapper.AddInParameter("@FakturDate", DbType.DateTime, v_DealerStock.FakturDate)
            DbCommandWrapper.AddInParameter("@OpenFakturDate", DbType.DateTime, v_DealerStock.OpenFakturDate)
            DbCommandWrapper.AddInParameter("@ValidateTime", DbType.DateTime, v_DealerStock.ValidateTime)
            DBCommandWrapper.AddInParameter("@PrintedTime", DbType.DateTime, v_DealerStock.PrintedTime)

            DbCommandWrapper.AddInParameter("@TOPID", DbType.Int32, v_DealerStock.TOPID)
            DbCommandWrapper.AddInParameter("@SONumber", DbType.AnsiString, v_DealerStock.SONumber)
            DbCommandWrapper.AddInParameter("@DONumber", DbType.AnsiString, v_DealerStock.DONumber)
            DbCommandWrapper.AddInParameter("@CategoryID", DbType.Byte, v_DealerStock.CategoryID)
            DbCommandWrapper.AddInParameter("@AlreadySaled", DbType.Byte, v_DealerStock.AlreadySaled)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, v_DealerStock.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, v_DealerStock.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@ModelDescription", DbType.AnsiString, v_DealerStock.ModelDescription)

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

            Dim v_DealerStock As v_DealerStock = CType(obj, v_DealerStock)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, v_DealerStock.ID)
            DbCommandWrapper.AddInParameter("@FakturStatus", DbType.AnsiString, v_DealerStock.FakturStatus)
            DBCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, v_DealerStock.DealerCode)
            DBCommandWrapper.AddInParameter("@SearchTerm1", DbType.AnsiString, v_DealerStock.SearchTerm1)
            DBCommandWrapper.AddInParameter("@GroupName", DbType.AnsiString, v_DealerStock.GroupName)
            DBCommandWrapper.AddInParameter("@VechileTypeCode", DbType.AnsiString, v_DealerStock.VechileTypeCode)
            DBCommandWrapper.AddInParameter("@ColorCode", DbType.AnsiString, v_DealerStock.ColorCode)
            DBCommandWrapper.AddInParameter("@ChassisNumber", DbType.AnsiString, v_DealerStock.ChassisNumber)
            DBCommandWrapper.AddInParameter("@Description", DbType.AnsiString, v_DealerStock.Description)
            DBCommandWrapper.AddInParameter("@ProjectName", DbType.AnsiString, v_DealerStock.ProjectName)
            DBCommandWrapper.AddInParameter("@Name1", DbType.AnsiString, v_DealerStock.Name1)
            DBCommandWrapper.AddInParameter("@ProvinceName", DbType.AnsiString, v_DealerStock.ProvinceName)
            DBCommandWrapper.AddInParameter("@DODate", DbType.DateTime, v_DealerStock.DODate)
            DbCommandWrapper.AddInParameter("@FakturDate", DbType.DateTime, v_DealerStock.FakturDate)
            DbCommandWrapper.AddInParameter("@OpenFakturDate", DbType.DateTime, v_DealerStock.OpenFakturDate)
            DbCommandWrapper.AddInParameter("@ValidateTime", DbType.DateTime, v_DealerStock.ValidateTime)
            DBCommandWrapper.AddInParameter("@PrintedTime", DbType.DateTime, v_DealerStock.PrintedTime)

            DbCommandWrapper.AddInParameter("@TOPID", DbType.Int32, v_DealerStock.TOPID)
            DbCommandWrapper.AddInParameter("@SONumber", DbType.AnsiString, v_DealerStock.SONumber)
            DbCommandWrapper.AddInParameter("@DONumber", DbType.AnsiString, v_DealerStock.DONumber)
            DbCommandWrapper.AddInParameter("@CategoryID", DbType.Byte, v_DealerStock.CategoryID)
            DbCommandWrapper.AddInParameter("@AlreadySaled", DbType.Byte, v_DealerStock.AlreadySaled)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, v_DealerStock.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, v_DealerStock.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@ModelDescription", DbType.AnsiString, v_DealerStock.ModelDescription)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As v_DealerStock

            Dim v_DealerStock As v_DealerStock = New v_DealerStock

            v_DealerStock.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("FakturStatus")) Then v_DealerStock.FakturStatus = dr("FakturStatus").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DealerCode")) Then v_DealerStock.DealerCode = dr("DealerCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SearchTerm1")) Then v_DealerStock.SearchTerm1 = dr("SearchTerm1").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("GroupName")) Then v_DealerStock.GroupName = dr("GroupName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("VechileTypeCode")) Then v_DealerStock.VechileTypeCode = dr("VechileTypeCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ColorCode")) Then v_DealerStock.ColorCode = dr("ColorCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ChassisNumber")) Then v_DealerStock.ChassisNumber = dr("ChassisNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Description")) Then v_DealerStock.Description = dr("Description").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ProjectName")) Then v_DealerStock.ProjectName = dr("ProjectName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Name1")) Then v_DealerStock.Name1 = dr("Name1").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ProvinceName")) Then v_DealerStock.ProvinceName = dr("ProvinceName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DODate")) Then v_DealerStock.DODate = CType(dr("DODate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("FakturNumber")) Then v_DealerStock.FakturNumber = dr("FakturNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("FakturDate")) Then v_DealerStock.FakturDate = CType(dr("FakturDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("OpenFakturDate")) Then v_DealerStock.OpenFakturDate = CType(dr("OpenFakturDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("ValidateTime")) Then v_DealerStock.ValidateTime = CType(dr("ValidateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("PrintedTime")) Then v_DealerStock.PrintedTime = CType(dr("PrintedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("TOPID")) Then v_DealerStock.TOPID = CType(dr("TOPID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("SONumber")) Then v_DealerStock.SONumber = dr("SONumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DONumber")) Then v_DealerStock.DONumber = dr("DONumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CategoryID")) Then v_DealerStock.CategoryID = CType(dr("CategoryID"), Byte)
            If Not dr.IsDBNull(dr.GetOrdinal("AlreadySaled")) Then v_DealerStock.AlreadySaled = CType(dr("AlreadySaled"), Byte)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then v_DealerStock.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then v_DealerStock.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then v_DealerStock.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then v_DealerStock.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then v_DealerStock.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("ProductionYear")) Then v_DealerStock.ProductionYear = CType(dr("ProductionYear"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("ModelDescription")) Then v_DealerStock.ModelDescription = dr("ModelDescription").ToString

            If Not dr.IsDBNull(dr.GetOrdinal("ConfirmFakturDate")) Then v_DealerStock.ConfirmFakturDate = CType(dr("ConfirmFakturDate"), DateTime)
            Return v_DealerStock

        End Function

        Private Sub SetTableName()

            If Not (GetType(v_DealerStock) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(v_DealerStock), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(v_DealerStock).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

