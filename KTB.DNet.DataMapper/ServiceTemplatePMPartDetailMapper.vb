#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : ServiceTemplatePMPartDetail Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2005 
'// ---------------------
'// $History      : $
'// Generated on 11/30/2005 - 1:05:06 PM
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

    Public Class ServiceTemplatePMPartDetailMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertServiceTemplatePMPartDetail"
        Private m_UpdateStatement As String = "up_UpdateServiceTemplatePMPartDetail"
        Private m_RetrieveStatement As String = "up_RetrieveServiceTemplatePMPartDetail"
        Private m_RetrieveListStatement As String = "up_RetrieveServiceTemplatePMPartDetailList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteServiceTemplatePMPartDetail"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim ServiceTemplatePMPartDetail As ServiceTemplatePMPartDetail = Nothing
            While dr.Read

                ServiceTemplatePMPartDetail = Me.CreateObject(dr)

            End While

            Return ServiceTemplatePMPartDetail

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim ServiceTemplatePMPartDetailList As ArrayList = New ArrayList

            While dr.Read
                Dim ServiceTemplatePMPartDetail As ServiceTemplatePMPartDetail = Me.CreateObject(dr)
                ServiceTemplatePMPartDetailList.Add(ServiceTemplatePMPartDetail)
            End While

            Return ServiceTemplatePMPartDetailList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim ServiceTemplatePMPartDetail As ServiceTemplatePMPartDetail = CType(obj, ServiceTemplatePMPartDetail)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, ServiceTemplatePMPartDetail.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim ServiceTemplatePMPartDetail As ServiceTemplatePMPartDetail = CType(obj, ServiceTemplatePMPartDetail)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            'DbCommandWrapper.AddInParameter("@PartAmount", DbType.Decimal, ServiceTemplatePMPartDetail.PartAmount)
            DbCommandWrapper.AddInParameter("@PartQuantity", DbType.Decimal, ServiceTemplatePMPartDetail.PartQuantity)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, ServiceTemplatePMPartDetail.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdatedBy", DbType.AnsiString, ServiceTemplatePMPartDetail.LastUpdatedBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@ServiceTemplatePMPartHeaderID", DbType.Int32, Me.GetRefObject(ServiceTemplatePMPartDetail.ServiceTemplatePMPartHeader))
            DbCommandWrapper.AddInParameter("@SparePartMasterID", DbType.Int32, Me.GetRefObject(ServiceTemplatePMPartDetail.SparePartMaster))

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

            Dim ServiceTemplatePMPartDetail As ServiceTemplatePMPartDetail = CType(obj, ServiceTemplatePMPartDetail)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, ServiceTemplatePMPartDetail.ID)
            'DbCommandWrapper.AddInParameter("@PartAmount", DbType.Decimal, ServiceTemplatePMPartDetail.PartAmount)
            DbCommandWrapper.AddInParameter("@PartQuantity", DbType.Decimal, ServiceTemplatePMPartDetail.PartQuantity)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, ServiceTemplatePMPartDetail.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, ServiceTemplatePMPartDetail.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@ServiceTemplatePMPartHeaderID", DbType.Int32, Me.GetRefObject(ServiceTemplatePMPartDetail.ServiceTemplatePMPartHeader))
            DbCommandWrapper.AddInParameter("@SparePartMasterID", DbType.Int32, Me.GetRefObject(ServiceTemplatePMPartDetail.SparePartMaster))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As ServiceTemplatePMPartDetail

            Dim ServiceTemplatePMPartDetail As ServiceTemplatePMPartDetail = New ServiceTemplatePMPartDetail

            ServiceTemplatePMPartDetail.ID = CType(dr("ID"), Integer)
            'If Not dr.IsDBNull(dr.GetOrdinal("PartAmount")) Then ServiceTemplatePMPartDetail.PartAmount = CDec(dr("PartAmount").ToString)
            If Not dr.IsDBNull(dr.GetOrdinal("PartQuantity")) Then ServiceTemplatePMPartDetail.PartQuantity = CDec(dr("PartQuantity").ToString)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then ServiceTemplatePMPartDetail.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then ServiceTemplatePMPartDetail.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then ServiceTemplatePMPartDetail.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdatedBy")) Then ServiceTemplatePMPartDetail.LastUpdatedBy = dr("LastUpdatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdatedTime")) Then ServiceTemplatePMPartDetail.LastUpdatedTime = CType(dr("LastUpdatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("ServiceTemplatePMPartHeaderID")) Then
                ServiceTemplatePMPartDetail.ServiceTemplatePMPartHeader = New ServiceTemplatePMPartHeader(CType(dr("ServiceTemplatePMPartHeaderID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("SparePartMasterID")) Then
                ServiceTemplatePMPartDetail.SparePartMaster = New SparePartMaster(CType(dr("SparePartMasterID"), Integer))
            End If

            Return ServiceTemplatePMPartDetail

        End Function

        Private Sub SetTableName()

            If Not (GetType(ServiceTemplatePMPartDetail) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(ServiceTemplatePMPartDetail), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(ServiceTemplatePMPartDetail).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

