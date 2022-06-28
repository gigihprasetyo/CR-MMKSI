#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : SpecialItemDetail Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2006 
'// ---------------------
'// $History      : $
'// Generated on 1/19/2006 - 10:47:23 AM
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

    Public Class SpecialItemDetailMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertSpecialItemDetail"
        Private m_UpdateStatement As String = "up_UpdateSpecialItemDetail"
        Private m_RetrieveStatement As String = "up_RetrieveSpecialItemDetail"
        Private m_RetrieveListStatement As String = "up_RetrieveSpecialItemDetailList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteSpecialItemDetail"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim specialItemDetail As SpecialItemDetail = Nothing
            While dr.Read

                specialItemDetail = Me.CreateObject(dr)

            End While

            Return specialItemDetail

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim specialItemDetailList As ArrayList = New ArrayList

            While dr.Read
                Dim specialItemDetail As SpecialItemDetail = Me.CreateObject(dr)
                specialItemDetailList.Add(specialItemDetail)
            End While

            Return specialItemDetailList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim specialItemDetail As SpecialItemDetail = CType(obj, SpecialItemDetail)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, specialItemDetail.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim specialItemDetail As SpecialItemDetail = CType(obj, SpecialItemDetail)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@PartName", DbType.AnsiString, specialItemDetail.PartName)
            DbCommandWrapper.AddInParameter("@ModelCode", DbType.AnsiString, specialItemDetail.ModelCode)
            DbCommandWrapper.AddInParameter("@ItemStatus", DbType.Int16, specialItemDetail.ItemStatus)
            DbCommandWrapper.AddInParameter("@ExtMaterialGroup", DbType.AnsiString, specialItemDetail.ExtMaterialGroup)
            DbCommandWrapper.AddInParameter("@Remark", DbType.AnsiString, specialItemDetail.Remark)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, specialItemDetail.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, specialItemDetail.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@SparePartMasterID", DbType.Int32, Me.GetRefObject(specialItemDetail.SparePartMaster))
            DbCommandWrapper.AddInParameter("@SpecialItemHeaderID", DbType.Int32, Me.GetRefObject(specialItemDetail.SpecialItemHeader))

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

            Dim specialItemDetail As SpecialItemDetail = CType(obj, SpecialItemDetail)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, specialItemDetail.ID)
            DbCommandWrapper.AddInParameter("@PartName", DbType.AnsiString, specialItemDetail.PartName)
            DbCommandWrapper.AddInParameter("@ModelCode", DbType.AnsiString, specialItemDetail.ModelCode)
            DbCommandWrapper.AddInParameter("@ItemStatus", DbType.Int16, specialItemDetail.ItemStatus)
            DbCommandWrapper.AddInParameter("@ExtMaterialGroup", DbType.AnsiString, specialItemDetail.ExtMaterialGroup)
            DbCommandWrapper.AddInParameter("@Remark", DbType.AnsiString, specialItemDetail.Remark)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, specialItemDetail.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, specialItemDetail.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@SparePartMasterID", DbType.Int32, Me.GetRefObject(specialItemDetail.SparePartMaster))
            DbCommandWrapper.AddInParameter("@SpecialItemHeaderID", DbType.Int32, Me.GetRefObject(specialItemDetail.SpecialItemHeader))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As SpecialItemDetail

            Dim specialItemDetail As SpecialItemDetail = New SpecialItemDetail

            specialItemDetail.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("PartName")) Then specialItemDetail.PartName = dr("PartName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ModelCode")) Then specialItemDetail.ModelCode = dr("ModelCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ItemStatus")) Then specialItemDetail.ItemStatus = CType(dr("ItemStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("ExtMaterialGroup")) Then specialItemDetail.ExtMaterialGroup = dr("ExtMaterialGroup").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Remark")) Then specialItemDetail.Remark = dr("Remark").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then specialItemDetail.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then specialItemDetail.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then specialItemDetail.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then specialItemDetail.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then specialItemDetail.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("SparePartMasterID")) Then
                specialItemDetail.SparePartMaster = New SparePartMaster(CType(dr("SparePartMasterID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("SpecialItemHeaderID")) Then
                specialItemDetail.SpecialItemHeader = New SpecialItemHeader(CType(dr("SpecialItemHeaderID"), Integer))
            End If

            Return specialItemDetail

        End Function

        Private Sub SetTableName()

            If Not (GetType(SpecialItemDetail) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(SpecialItemDetail), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(SpecialItemDetail).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

