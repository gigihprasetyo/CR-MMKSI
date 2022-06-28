#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : EFakturItem Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2021 
'// ---------------------
'// $History      : $
'// Generated on 1/12/2021 - 2:56:51 PM
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

    Public Class EFakturItemMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertEFakturItem"
        Private m_UpdateStatement As String = "up_UpdateEFakturItem"
        Private m_RetrieveStatement As String = "up_RetrieveEFakturItem"
        Private m_RetrieveListStatement As String = "up_RetrieveEFakturItemList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteEFakturItem"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim eFakturItem As EFakturItem = Nothing
            While dr.Read

                eFakturItem = Me.CreateObject(dr)

            End While

            Return eFakturItem

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim eFakturItemList As ArrayList = New ArrayList

            While dr.Read
                Dim eFakturItem As EFakturItem = Me.CreateObject(dr)
                eFakturItemList.Add(eFakturItem)
            End While

            Return eFakturItemList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim eFakturItem As EFakturItem = CType(obj, EFakturItem)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, eFakturItem.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim eFakturItem As EFakturItem = CType(obj, EFakturItem)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@PageNumber", DbType.AnsiString, eFakturItem.PageNumber)
            DbCommandWrapper.AddInParameter("@DocNumber", DbType.AnsiString, eFakturItem.DocNumber)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, eFakturItem.Status)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, eFakturItem.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdatedBy", DbType.AnsiString, eFakturItem.LastUpdatedBy)
            DbCommandWrapper.AddInParameter("@LastUpdatedTime", DbType.DateTime, eFakturItem.LastUpdatedTime)

            DbCommandWrapper.AddInParameter("@EFakturHeaderID", DbType.Int32, Me.GetRefObject(eFakturItem.EFakturHeader))

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

            Dim eFakturItem As EFakturItem = CType(obj, EFakturItem)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, eFakturItem.ID)
            DbCommandWrapper.AddInParameter("@PageNumber", DbType.AnsiString, eFakturItem.PageNumber)
            DbCommandWrapper.AddInParameter("@DocNumber", DbType.AnsiString, eFakturItem.DocNumber)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, eFakturItem.Status)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, eFakturItem.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, eFakturItem.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdatedBy", DbType.AnsiString, eFakturItem.LastUpdatedBy)
            DbCommandWrapper.AddInParameter("@LastUpdatedTime", DbType.DateTime, DateTime.Now)


            DbCommandWrapper.AddInParameter("@EFakturHeaderID", DbType.Int32, Me.GetRefObject(eFakturItem.EFakturHeader))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As EFakturItem

            Dim eFakturItem As EFakturItem = New EFakturItem

            eFakturItem.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("PageNumber")) Then eFakturItem.PageNumber = dr("PageNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DocNumber")) Then eFakturItem.DocNumber = dr("DocNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then eFakturItem.Status = CType(dr("Status"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then eFakturItem.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then eFakturItem.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then eFakturItem.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdatedBy")) Then eFakturItem.LastUpdatedBy = dr("LastUpdatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdatedTime")) Then eFakturItem.LastUpdatedTime = CType(dr("LastUpdatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("EFakturHeaderID")) Then
                eFakturItem.EFakturHeader = New EFakturHeader(CType(dr("EFakturHeaderID"), Integer))
            End If

            Return eFakturItem

        End Function

        Private Sub SetTableName()

            If Not (GetType(EFakturItem) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(EFakturItem), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(EFakturItem).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace
