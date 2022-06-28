#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : SparePartMaster Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2007 
'// ---------------------
'// $History      : $
'// Generated on 9/27/2007 - 8:06:23 AM
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

    Public Class SparePartMasterMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertSparePartMaster"
        Private m_UpdateStatement As String = "up_UpdateSparePartMaster"
        Private m_RetrieveStatement As String = "up_RetrieveSparePartMaster"
        Private m_RetrieveListStatement As String = "up_RetrieveSparePartMasterList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteSparePartMaster"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim sparePartMaster As SparePartMaster = Nothing
            While dr.Read

                sparePartMaster = Me.CreateObject(dr)

            End While

            Return sparePartMaster

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim sparePartMasterList As ArrayList = New ArrayList

            While dr.Read
                Dim sparePartMaster As SparePartMaster = Me.CreateObject(dr)
                sparePartMasterList.Add(sparePartMaster)
            End While

            Return sparePartMasterList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim sparePartMaster As SparePartMaster = CType(obj, SparePartMaster)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, sparePartMaster.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim sparePartMaster As SparePartMaster = CType(obj, SparePartMaster)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@PartNumber", DbType.AnsiString, sparePartMaster.PartNumber)
            DbCommandWrapper.AddInParameter("@UoM", DbType.AnsiString, sparePartMaster.UoM)
            DbCommandWrapper.AddInParameter("@PartNumberReff", DbType.AnsiString, sparePartMaster.PartNumberReff)
            DbCommandWrapper.AddInParameter("@MaterialCategoryCode", DbType.AnsiString, sparePartMaster.MaterialCategoryCode)
            DbCommandWrapper.AddInParameter("@PartName", DbType.AnsiString, sparePartMaster.PartName)
            DbCommandWrapper.AddInParameter("@AltPartNumber", DbType.AnsiString, sparePartMaster.AltPartNumber)
            DbCommandWrapper.AddInParameter("@AltPartName", DbType.AnsiString, sparePartMaster.AltPartName)
            DbCommandWrapper.AddInParameter("@PartCode", DbType.AnsiString, sparePartMaster.PartCode)
            DbCommandWrapper.AddInParameter("@ModelCode", DbType.AnsiString, sparePartMaster.ModelCode)
            DbCommandWrapper.AddInParameter("@SupplierCode", DbType.AnsiString, sparePartMaster.SupplierCode)
            DbCommandWrapper.AddInParameter("@TypeCode", DbType.AnsiString, sparePartMaster.TypeCode)
            DbCommandWrapper.AddInParameter("@Stock", DbType.Int32, sparePartMaster.Stock)
            DbCommandWrapper.AddInParameter("@RetalPrice", DbType.Currency, sparePartMaster.RetalPrice)
            DbCommandWrapper.AddInParameter("@PartStatus", DbType.AnsiString, sparePartMaster.PartStatus)
            DbCommandWrapper.AddInParameter("@ProductType", DbType.AnsiString, sparePartMaster.ProductType)
            DbCommandWrapper.AddInParameter("@ActiveStatus", DbType.Int16, sparePartMaster.ActiveStatus)
            DbCommandWrapper.AddInParameter("@AccessoriesType", DbType.Int16, sparePartMaster.AccessoriesType)
            DbCommandWrapper.AddInParameter("@IsWarranty", DbType.Int16, sparePartMaster.IsWarranty)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, sparePartMaster.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, sparePartMaster.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@ProductCategoryID", DbType.Int32, Me.GetRefObject(sparePartMaster.ProductCategory))
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

            Dim sparePartMaster As SparePartMaster = CType(obj, SparePartMaster)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, sparePartMaster.ID)
            DbCommandWrapper.AddInParameter("@PartNumber", DbType.AnsiString, sparePartMaster.PartNumber)
            DbCommandWrapper.AddInParameter("@UoM", DbType.AnsiString, sparePartMaster.UoM)
            DbCommandWrapper.AddInParameter("@PartNumberReff", DbType.AnsiString, sparePartMaster.PartNumberReff)
            DbCommandWrapper.AddInParameter("@MaterialCategoryCode", DbType.AnsiString, sparePartMaster.MaterialCategoryCode)
            DbCommandWrapper.AddInParameter("@PartName", DbType.AnsiString, sparePartMaster.PartName)
            DbCommandWrapper.AddInParameter("@AltPartNumber", DbType.AnsiString, sparePartMaster.AltPartNumber)
            DbCommandWrapper.AddInParameter("@AltPartName", DbType.AnsiString, sparePartMaster.AltPartName)
            DbCommandWrapper.AddInParameter("@PartCode", DbType.AnsiString, sparePartMaster.PartCode)
            DbCommandWrapper.AddInParameter("@ModelCode", DbType.AnsiString, sparePartMaster.ModelCode)
            DbCommandWrapper.AddInParameter("@SupplierCode", DbType.AnsiString, sparePartMaster.SupplierCode)
            DbCommandWrapper.AddInParameter("@TypeCode", DbType.AnsiString, sparePartMaster.TypeCode)
            DbCommandWrapper.AddInParameter("@Stock", DbType.Int32, sparePartMaster.Stock)
            DbCommandWrapper.AddInParameter("@RetalPrice", DbType.Currency, sparePartMaster.RetalPrice)
            DbCommandWrapper.AddInParameter("@PartStatus", DbType.AnsiString, sparePartMaster.PartStatus)
            DbCommandWrapper.AddInParameter("@ProductType", DbType.AnsiString, sparePartMaster.ProductType)
            DbCommandWrapper.AddInParameter("@ActiveStatus", DbType.Int16, sparePartMaster.ActiveStatus)
            DbCommandWrapper.AddInParameter("@AccessoriesType", DbType.Int16, sparePartMaster.AccessoriesType)
            DbCommandWrapper.AddInParameter("@IsWarranty", DbType.Int16, sparePartMaster.IsWarranty)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, sparePartMaster.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, sparePartMaster.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@ProductCategoryID", DbType.Int32, Me.GetRefObject(sparePartMaster.ProductCategory))


            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As SparePartMaster

            Dim sparePartMaster As SparePartMaster = New SparePartMaster

            sparePartMaster.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("PartNumber")) Then sparePartMaster.PartNumber = dr("PartNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("UoM")) Then sparePartMaster.UoM = dr("UoM").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("MaterialCategoryCode")) Then sparePartMaster.MaterialCategoryCode = dr("MaterialCategoryCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PartNumberReff")) Then sparePartMaster.PartNumberReff = dr("PartNumberReff").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PartName")) Then sparePartMaster.PartName = dr("PartName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("AltPartNumber")) Then sparePartMaster.AltPartNumber = dr("AltPartNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("AltPartName")) Then sparePartMaster.AltPartName = dr("AltPartName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PartCode")) Then sparePartMaster.PartCode = dr("PartCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ModelCode")) Then sparePartMaster.ModelCode = dr("ModelCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SupplierCode")) Then sparePartMaster.SupplierCode = dr("SupplierCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("TypeCode")) Then sparePartMaster.TypeCode = dr("TypeCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Stock")) Then sparePartMaster.Stock = CType(dr("Stock"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("RetalPrice")) Then sparePartMaster.RetalPrice = CType(dr("RetalPrice"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("PartStatus")) Then sparePartMaster.PartStatus = dr("PartStatus").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ProductType")) Then sparePartMaster.ProductType = dr("ProductType").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ActiveStatus")) Then sparePartMaster.ActiveStatus = CType(dr("ActiveStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("AccessoriesType")) Then sparePartMaster.AccessoriesType = CType(dr("AccessoriesType"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("IsWarranty")) Then sparePartMaster.IsWarranty = CType(dr("IsWarranty"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then sparePartMaster.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then sparePartMaster.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then sparePartMaster.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then sparePartMaster.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then sparePartMaster.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("ProductCategoryID")) Then
                sparePartMaster.ProductCategory = New ProductCategory(CType(dr("ProductCategoryID"), Integer))
            End If
            Return sparePartMaster

        End Function

        Private Sub SetTableName()

            If Not (GetType(SparePartMaster) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(SparePartMaster), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(SparePartMaster).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"
        Public Function RetrieveSparpartModel(ByVal modelCode As String, Optional ByVal companyCode As String = "") As ArrayList
            Dim dr As IDataReader = Nothing
            Dim domainObjects As ArrayList = New ArrayList
            Dim sqlQuery As String

            Dim iProduct As Integer = 0
            If companyCode.Trim.ToUpper = "MMC" Then
                iProduct = 1
            End If
            If companyCode.Trim.ToUpper = "MFTBC" Then
                iProduct = 2
            End If

            modelCode = modelCode.Replace(";", "")
            modelCode = modelCode.Replace("--", "")
            modelCode = modelCode.Replace("'", "")
            Try
                DbCommandWrapper = GetRetrieveCommand()
                If modelCode.Trim = "" Then
                    sqlQuery = "select distinct(ModelCode) from sparepartmaster "
                    If iProduct > 0 Then
                        sqlQuery = sqlQuery & " where ProductCategoryID = " & iProduct
                    End If
                Else
                    sqlQuery = "select distinct(ModelCode) from sparepartmaster where ModelCode like '%" & modelCode.Trim & "%'"
                    If iProduct > 0 Then
                        sqlQuery = sqlQuery & " and ProductCategoryID = " & iProduct
                    End If
                End If

                DbCommandWrapper.SetParameterValue("@sqlQuery", sqlQuery)
                dr = Db.ExecuteReader(DbCommandWrapper)
                Dim spModel As SparepartMasterModel
                While dr.Read
                    spModel = New SparepartMasterModel
                    spModel.ModelCode = CType(dr(0), String)
                    domainObjects.Add(spModel)
                End While
            Catch ex As Exception
                Dim rethrow As Boolean = ExceptionPolicy.HandleException(ex, "")
                If rethrow Then
                    Throw
                End If
            Finally
                If (Not IsNothing(dr)) AndAlso Not (dr.IsClosed) Then
                    dr.Close()
                End If
            End Try
            Return domainObjects
        End Function
#End Region

    End Class

    Public Class SparepartMasterModel
        Private _model As String
        Public Property ModelCode() As String
            Get
                Return _model
            End Get
            Set(ByVal Value As String)
                _model = Value
            End Set
        End Property
    End Class
End Namespace



