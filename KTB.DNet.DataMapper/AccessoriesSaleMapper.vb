
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : AccessoriesSale Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2012 
'// ---------------------
'// $History      : $
'// Generated on 7/16/2012 - 3:33:48 PM
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

    Public Class AccessoriesSaleMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertAccessoriesSale"
        Private m_UpdateStatement As String = "up_UpdateAccessoriesSale"
        Private m_RetrieveStatement As String = "up_RetrieveAccessoriesSale"
        Private m_RetrieveListStatement As String = "up_RetrieveAccessoriesSaleList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteAccessoriesSale"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim accessoriesSale As AccessoriesSale = Nothing
            While dr.Read

                accessoriesSale = Me.CreateObject(dr)

            End While

            Return accessoriesSale

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim accessoriesSaleList As ArrayList = New ArrayList

            While dr.Read
                Dim accessoriesSale As AccessoriesSale = Me.CreateObject(dr)
                accessoriesSaleList.Add(accessoriesSale)
            End While

            Return accessoriesSaleList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim accessoriesSale As AccessoriesSale = CType(obj, AccessoriesSale)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, accessoriesSale.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim accessoriesSale As AccessoriesSale = CType(obj, AccessoriesSale)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@ReportNumber", DbType.AnsiString, accessoriesSale.ReportNumber)
            DbCommandWrapper.AddInParameter("@RefNumber", DbType.AnsiString, accessoriesSale.RefNumber)
            'DBCommandWrapper.AddInParameter("@DealerID", DbType.Int32, accessoriesSale.DealerID)
            'DbCommandWrapper.AddInParameter("@AccessoriesCategoryID", DbType.Int32, accessoriesSale.AccessoriesCategoryID)
            'DbCommandWrapper.AddInParameter("@ChassisMasterID", DbType.Int32, accessoriesSale.ChassisMasterID)
            DbCommandWrapper.AddInParameter("@SoldDate", DbType.DateTime, accessoriesSale.SoldDate)
            DbCommandWrapper.AddInParameter("@CustomerName", DbType.AnsiString, accessoriesSale.CustomerName)
            DbCommandWrapper.AddInParameter("@CustomerPhone", DbType.AnsiString, accessoriesSale.CustomerPhone)
            DbCommandWrapper.AddInParameter("@Comment", DbType.AnsiString, accessoriesSale.Comment)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, accessoriesSale.Status)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, accessoriesSale.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, accessoriesSale.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DBCommandWrapper.AddInParameter("@DealerID", DbType.Int32, Me.GetRefObject(accessoriesSale.Dealer))
            DBCommandWrapper.AddInParameter("@AccessoriesCategoryID", DbType.Int32, Me.GetRefObject(accessoriesSale.AccessoriesCategory))
            DBCommandWrapper.AddInParameter("@ChassisMasterID", DbType.Int32, Me.GetRefObject(accessoriesSale.ChassisMaster))


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

            Dim accessoriesSale As AccessoriesSale = CType(obj, AccessoriesSale)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, accessoriesSale.ID)
            DbCommandWrapper.AddInParameter("@ReportNumber", DbType.AnsiString, accessoriesSale.ReportNumber)
            DbCommandWrapper.AddInParameter("@RefNumber", DbType.AnsiString, accessoriesSale.RefNumber)
            'DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, accessoriesSale.DealerID)
            'DbCommandWrapper.AddInParameter("@AccessoriesCategoryID", DbType.Int32, accessoriesSale.AccessoriesCategoryID)
            'DbCommandWrapper.AddInParameter("@ChassisMasterID", DbType.Int32, accessoriesSale.ChassisMasterID)
            DbCommandWrapper.AddInParameter("@SoldDate", DbType.DateTime, accessoriesSale.SoldDate)
            DbCommandWrapper.AddInParameter("@CustomerName", DbType.AnsiString, accessoriesSale.CustomerName)
            DbCommandWrapper.AddInParameter("@CustomerPhone", DbType.AnsiString, accessoriesSale.CustomerPhone)
            DbCommandWrapper.AddInParameter("@Comment", DbType.AnsiString, accessoriesSale.Comment)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, accessoriesSale.Status)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, accessoriesSale.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, accessoriesSale.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DBCommandWrapper.AddInParameter("@DealerID", DbType.Int32, Me.GetRefObject(accessoriesSale.Dealer))
            DBCommandWrapper.AddInParameter("@AccessoriesCategoryID", DbType.Int32, Me.GetRefObject(accessoriesSale.AccessoriesCategory))
            DBCommandWrapper.AddInParameter("@ChassisMasterID", DbType.Int32, Me.GetRefObject(accessoriesSale.ChassisMaster))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As AccessoriesSale

            Dim accessoriesSale As AccessoriesSale = New AccessoriesSale

            accessoriesSale.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("ReportNumber")) Then accessoriesSale.ReportNumber = dr("ReportNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RefNumber")) Then accessoriesSale.RefNumber = dr("RefNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then accessoriesSale.DealerID = CType(dr("DealerID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("AccessoriesCategoryID")) Then accessoriesSale.AccessoriesCategoryID = CType(dr("AccessoriesCategoryID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("ChassisMasterID")) Then accessoriesSale.ChassisMasterID = CType(dr("ChassisMasterID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("SoldDate")) Then accessoriesSale.SoldDate = CType(dr("SoldDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("CustomerName")) Then accessoriesSale.CustomerName = dr("CustomerName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CustomerPhone")) Then accessoriesSale.CustomerPhone = dr("CustomerPhone").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Comment")) Then accessoriesSale.Comment = dr("Comment").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then accessoriesSale.Status = CType(dr("Status"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then accessoriesSale.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then accessoriesSale.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then accessoriesSale.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then accessoriesSale.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then accessoriesSale.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)


            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then
                accessoriesSale.Dealer = New Dealer(CType(dr("DealerID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("AccessoriesCategoryID")) Then
                accessoriesSale.AccessoriesCategory = New AccessoriesCategory(CType(dr("AccessoriesCategoryID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("ChassisMasterID")) Then
                accessoriesSale.ChassisMaster = New ChassisMaster(CType(dr("ChassisMasterID"), Integer))
            End If


            Return accessoriesSale

        End Function

        Private Sub SetTableName()

            If Not (GetType(AccessoriesSale) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(AccessoriesSale), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(AccessoriesSale).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

