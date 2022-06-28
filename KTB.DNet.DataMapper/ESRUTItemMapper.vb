#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : ESRUTItem Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2019 
'// ---------------------
'// $History      : $
'// Generated on 8/29/2019 - 10:46:29 AM
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

    Public Class ESRUTItemMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertESRUTItem"
        Private m_UpdateStatement As String = "up_UpdateESRUTItem"
        Private m_RetrieveStatement As String = "up_RetrieveESRUTItem"
        Private m_RetrieveListStatement As String = "up_RetrieveESRUTItemList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteESRUTItem"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim ESRUTItem As ESRUTItem = Nothing
            While dr.Read

                ESRUTItem = Me.CreateObject(dr)

            End While

            Return ESRUTItem

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim ESRUTItemList As ArrayList = New ArrayList

            While dr.Read
                Dim ESRUTItem As ESRUTItem = Me.CreateObject(dr)
                ESRUTItemList.Add(ESRUTItem)
            End While

            Return ESRUTItemList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim ESRUTItem As ESRUTItem = CType(obj, ESRUTItem)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, ESRUTItem.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim ESRUTItem As ESRUTItem = CType(obj, ESRUTItem)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@ESRUTHeaderID", DbType.Int32, Me.GetRefObject(ESRUTItem.ESRUTHeader))
            DbCommandWrapper.AddInParameter("@PageNumber", DbType.Int32, ESRUTItem.PageNumber)
            DbCommandWrapper.AddInParameter("@EngineNumber", DbType.AnsiString, ESRUTItem.EngineNumber)
            DbCommandWrapper.AddInParameter("@ChassisNumber", DbType.AnsiString, ESRUTItem.ChassisNumber)
            DbCommandWrapper.AddInParameter("@NomorSRUT", DbType.AnsiString, ESRUTItem.NomorSRUT)
            DbCommandWrapper.AddInParameter("@URLQRCode", DbType.AnsiString, ESRUTItem.URLQRCode)
            DbCommandWrapper.AddInParameter("@ChassisMasterID", DbType.Int32, Me.GetRefObject(ESRUTItem.ChassisMaster))
            DbCommandWrapper.AddInParameter("@IsRevision", DbType.Boolean, ESRUTItem.IsRevision)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, ESRUTItem.Status)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, ESRUTItem.RowStatus)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.StringFixedLength, ESRUTItem.LastUpdateBy)


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

            Dim ESRUTItem As ESRUTItem = CType(obj, ESRUTItem)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, ESRUTItem.ID)
            DbCommandWrapper.AddInParameter("@ESRUTHeaderID", DbType.Int32, Me.GetRefObject(ESRUTItem.ESRUTHeader))
            DbCommandWrapper.AddInParameter("@PageNumber", DbType.Int32, ESRUTItem.PageNumber)
            DbCommandWrapper.AddInParameter("@EngineNumber", DbType.AnsiString, ESRUTItem.EngineNumber)
            DbCommandWrapper.AddInParameter("@ChassisNumber", DbType.AnsiString, ESRUTItem.ChassisNumber)
            DbCommandWrapper.AddInParameter("@NomorSRUT", DbType.AnsiString, ESRUTItem.NomorSRUT)
            DbCommandWrapper.AddInParameter("@URLQRCode", DbType.AnsiString, ESRUTItem.URLQRCode)
            DbCommandWrapper.AddInParameter("@ChassisMasterID", DbType.Int32, Me.GetRefObject(ESRUTItem.ChassisMaster))
            DbCommandWrapper.AddInParameter("@IsRevision", DbType.Boolean, ESRUTItem.IsRevision)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, ESRUTItem.Status)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, ESRUTItem.RowStatus)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, ESRUTItem.CreatedBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.StringFixedLength, User)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As ESRUTItem

            Dim ESRUTItem As ESRUTItem = New ESRUTItem

            ESRUTItem.ID = CType(dr("ID"), Integer)

            If Not dr.IsDBNull(dr.GetOrdinal("PageNumber")) Then ESRUTItem.PageNumber = CType(dr("PageNumber"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("EngineNumber")) Then ESRUTItem.EngineNumber = dr("EngineNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ChassisNumber")) Then ESRUTItem.ChassisNumber = dr("ChassisNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("NomorSRUT")) Then ESRUTItem.NomorSRUT = dr("NomorSRUT").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("URLQRCode")) Then ESRUTItem.URLQRCode = dr("URLQRCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("IsRevision")) Then ESRUTItem.IsRevision = CType(dr("IsRevision"), Boolean)
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then ESRUTItem.Status = CType(dr("Status"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then ESRUTItem.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then ESRUTItem.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then ESRUTItem.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then ESRUTItem.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then ESRUTItem.LastUpdateBy = dr("LastUpdateBy").ToString

            If Not dr.IsDBNull(dr.GetOrdinal("ESRUTHeaderID")) Then
                ESRUTItem.ESRUTHeader = New ESRUTHeader(CType(dr("ESRUTHeaderID"), Integer))
            End If

            If Not dr.IsDBNull(dr.GetOrdinal("ChassisMasterID")) Then
                ESRUTItem.ChassisMaster = New ChassisMaster(CType(dr("ChassisMasterID"), Integer))
            End If


            Return ESRUTItem

        End Function

        Private Sub SetTableName()

            If Not (GetType(ESRUTItem) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(ESRUTItem), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(ESRUTItem).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace
