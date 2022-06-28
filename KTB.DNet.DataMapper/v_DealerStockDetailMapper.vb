
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : v_DealerStockDetail Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2009 
'// ---------------------
'// $History      : $
'// Generated on 8/24/2009 - 1:07:02 PM
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

    Public Class v_DealerStockDetailMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_Insertv_DealerStockDetail"
        Private m_UpdateStatement As String = "up_Updatev_DealerStockDetail"
        Private m_RetrieveStatement As String = "up_Retrievev_DealerStockDetail"
        Private m_RetrieveListStatement As String = "up_Retrievev_DealerStockDetailList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_Deletev_DealerStockDetail"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim v_DealerStockDetail As v_DealerStockDetail = Nothing
            While dr.Read

                v_DealerStockDetail = Me.CreateObject(dr)

            End While

            Return v_DealerStockDetail

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim v_DealerStockDetailList As ArrayList = New ArrayList

            While dr.Read
                Dim v_DealerStockDetail As v_DealerStockDetail = Me.CreateObject(dr)
                v_DealerStockDetailList.Add(v_DealerStockDetail)
            End While

            Return v_DealerStockDetailList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim v_DealerStockDetail As v_DealerStockDetail = CType(obj, v_DealerStockDetail)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, v_DealerStockDetail.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim v_DealerStockDetail As v_DealerStockDetail = CType(obj, v_DealerStockDetail)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@FakturStatus", DbType.AnsiString, v_DealerStockDetail.FakturStatus)
            DbCommandWrapper.AddInParameter("@ChassisNumber", DbType.AnsiString, v_DealerStockDetail.ChassisNumber)
            DbCommandWrapper.AddInParameter("@DODate", DbType.DateTime, v_DealerStockDetail.DODate)
            DbCommandWrapper.AddInParameter("@PaymentType", DbType.AnsiString, v_DealerStockDetail.PaymentType)
            DbCommandWrapper.AddInParameter("@SONumber", DbType.AnsiString, v_DealerStockDetail.SONumber)
            DbCommandWrapper.AddInParameter("@DONumber", DbType.AnsiString, v_DealerStockDetail.DONumber)
            DbCommandWrapper.AddInParameter("@CategoryCode", DbType.AnsiString, v_DealerStockDetail.CategoryCode)
            DbCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, v_DealerStockDetail.DealerCode)
            DbCommandWrapper.AddInParameter("@VechileTypeCode", DbType.AnsiString, v_DealerStockDetail.VechileTypeCode)
            DbCommandWrapper.AddInParameter("@ColorCode", DbType.AnsiString, v_DealerStockDetail.ColorCode)
            DbCommandWrapper.AddInParameter("@ProjectName", DbType.AnsiString, v_DealerStockDetail.ProjectName)
            DbCommandWrapper.AddInParameter("@Name1", DbType.AnsiString, v_DealerStockDetail.Name1)
            DbCommandWrapper.AddInParameter("@FakturDate", DbType.DateTime, v_DealerStockDetail.FakturDate)
            DbCommandWrapper.AddInParameter("@OpenFakturDate", DbType.DateTime, v_DealerStockDetail.OpenFakturDate)
            DbCommandWrapper.AddInParameter("@dID", DbType.Int16, v_DealerStockDetail.dID)
            DbCommandWrapper.AddInParameter("@vtID", DbType.Int16, v_DealerStockDetail.vtID)
            DbCommandWrapper.AddInParameter("@vcID", DbType.Int16, v_DealerStockDetail.vcID)
            DbCommandWrapper.AddInParameter("@pkhID", DbType.Int32, v_DealerStockDetail.pkhID)
            DbCommandWrapper.AddInParameter("@cID", DbType.Int32, v_DealerStockDetail.cID)
            DbCommandWrapper.AddInParameter("@ecID", DbType.Int32, v_DealerStockDetail.ecID)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, v_DealerStockDetail.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, v_DealerStockDetail.LastUpdateBy)
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

            Dim v_DealerStockDetail As v_DealerStockDetail = CType(obj, v_DealerStockDetail)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, v_DealerStockDetail.ID)
            DbCommandWrapper.AddInParameter("@FakturStatus", DbType.AnsiString, v_DealerStockDetail.FakturStatus)
            DbCommandWrapper.AddInParameter("@ChassisNumber", DbType.AnsiString, v_DealerStockDetail.ChassisNumber)
            DbCommandWrapper.AddInParameter("@DODate", DbType.DateTime, v_DealerStockDetail.DODate)
            DbCommandWrapper.AddInParameter("@PaymentType", DbType.AnsiString, v_DealerStockDetail.PaymentType)
            DbCommandWrapper.AddInParameter("@SONumber", DbType.AnsiString, v_DealerStockDetail.SONumber)
            DbCommandWrapper.AddInParameter("@DONumber", DbType.AnsiString, v_DealerStockDetail.DONumber)
            DbCommandWrapper.AddInParameter("@CategoryCode", DbType.AnsiString, v_DealerStockDetail.CategoryCode)
            DbCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, v_DealerStockDetail.DealerCode)
            DbCommandWrapper.AddInParameter("@VechileTypeCode", DbType.AnsiString, v_DealerStockDetail.VechileTypeCode)
            DbCommandWrapper.AddInParameter("@ColorCode", DbType.AnsiString, v_DealerStockDetail.ColorCode)
            DbCommandWrapper.AddInParameter("@ProjectName", DbType.AnsiString, v_DealerStockDetail.ProjectName)
            DbCommandWrapper.AddInParameter("@Name1", DbType.AnsiString, v_DealerStockDetail.Name1)
            DbCommandWrapper.AddInParameter("@FakturDate", DbType.DateTime, v_DealerStockDetail.FakturDate)
            DbCommandWrapper.AddInParameter("@OpenFakturDate", DbType.DateTime, v_DealerStockDetail.OpenFakturDate)
            DbCommandWrapper.AddInParameter("@dID", DbType.Int16, v_DealerStockDetail.dID)
            DbCommandWrapper.AddInParameter("@vtID", DbType.Int16, v_DealerStockDetail.vtID)
            DbCommandWrapper.AddInParameter("@vcID", DbType.Int16, v_DealerStockDetail.vcID)
            DbCommandWrapper.AddInParameter("@pkhID", DbType.Int32, v_DealerStockDetail.pkhID)
            DbCommandWrapper.AddInParameter("@cID", DbType.Int32, v_DealerStockDetail.cID)
            DbCommandWrapper.AddInParameter("@ecID", DbType.Int32, v_DealerStockDetail.ecID)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, v_DealerStockDetail.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, v_DealerStockDetail.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As v_DealerStockDetail

            Dim v_DealerStockDetail As v_DealerStockDetail = New v_DealerStockDetail

            v_DealerStockDetail.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("FakturStatus")) Then v_DealerStockDetail.FakturStatus = dr("FakturStatus").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ChassisNumber")) Then v_DealerStockDetail.ChassisNumber = dr("ChassisNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DODate")) Then v_DealerStockDetail.DODate = CType(dr("DODate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("PaymentType")) Then v_DealerStockDetail.PaymentType = dr("PaymentType").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SONumber")) Then v_DealerStockDetail.SONumber = dr("SONumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DONumber")) Then v_DealerStockDetail.DONumber = dr("DONumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CategoryCode")) Then v_DealerStockDetail.CategoryCode = dr("CategoryCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DealerCode")) Then v_DealerStockDetail.DealerCode = dr("DealerCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("VechileTypeCode")) Then v_DealerStockDetail.VechileTypeCode = dr("VechileTypeCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ColorCode")) Then v_DealerStockDetail.ColorCode = dr("ColorCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ProjectName")) Then v_DealerStockDetail.ProjectName = dr("ProjectName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Name1")) Then v_DealerStockDetail.Name1 = dr("Name1").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("FakturDate")) Then v_DealerStockDetail.FakturDate = CType(dr("FakturDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("OpenFakturDate")) Then v_DealerStockDetail.OpenFakturDate = CType(dr("OpenFakturDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("dID")) Then v_DealerStockDetail.dID = CType(dr("dID"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("vtID")) Then v_DealerStockDetail.vtID = CType(dr("vtID"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("vcID")) Then v_DealerStockDetail.vcID = CType(dr("vcID"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("pkhID")) Then v_DealerStockDetail.pkhID = CType(dr("pkhID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("cID")) Then v_DealerStockDetail.cID = CType(dr("cID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("ecID")) Then v_DealerStockDetail.ecID = CType(dr("ecID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then v_DealerStockDetail.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then v_DealerStockDetail.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then v_DealerStockDetail.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then v_DealerStockDetail.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then v_DealerStockDetail.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            Return v_DealerStockDetail

        End Function

        Private Sub SetTableName()

            If Not (GetType(v_DealerStockDetail) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(v_DealerStockDetail), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(v_DealerStockDetail).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

