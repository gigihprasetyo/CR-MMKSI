#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : ServiceTemplatePMPartHeader Objects Mapper.
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

    Public Class ServiceTemplatePMPartHeaderMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertServiceTemplatePMPartHeader"
        Private m_UpdateStatement As String = "up_UpdateServiceTemplatePMPartHeader"
        Private m_RetrieveStatement As String = "up_RetrieveServiceTemplatePMPartHeader"
        Private m_RetrieveListStatement As String = "up_RetrieveServiceTemplatePMPartHeaderList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteServiceTemplatePMPartHeader"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim ServiceTemplatePMPartHeader As ServiceTemplatePMPartHeader = Nothing
            While dr.Read

                ServiceTemplatePMPartHeader = Me.CreateObject(dr)

            End While

            Return ServiceTemplatePMPartHeader

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim ServiceTemplatePMPartHeaderList As ArrayList = New ArrayList

            While dr.Read
                Dim ServiceTemplatePMPartHeader As ServiceTemplatePMPartHeader = Me.CreateObject(dr)
                ServiceTemplatePMPartHeaderList.Add(ServiceTemplatePMPartHeader)
            End While

            Return ServiceTemplatePMPartHeaderList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim ServiceTemplatePMPartHeader As ServiceTemplatePMPartHeader = CType(obj, ServiceTemplatePMPartHeader)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, ServiceTemplatePMPartHeader.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim ServiceTemplatePMPartHeader As ServiceTemplatePMPartHeader = CType(obj, ServiceTemplatePMPartHeader)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@Varian", DbType.AnsiString, ServiceTemplatePMPartHeader.Varian)
            'DbCommandWrapper.AddInParameter("@ValidFrom", DbType.DateTime, ServiceTemplatePMPartHeader.ValidFrom)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, ServiceTemplatePMPartHeader.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdatedBy", DbType.AnsiString, ServiceTemplatePMPartHeader.LastUpdatedBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@PMKindID", DbType.Int32, Me.GetRefObject(ServiceTemplatePMPartHeader.PMKind))
            'DbCommandWrapper.AddInParameter("@VechileTypeID", DbType.Int16, Me.GetRefObject(ServiceTemplatePMPartHeader.VechileType))

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

            Dim ServiceTemplatePMPartHeader As ServiceTemplatePMPartHeader = CType(obj, ServiceTemplatePMPartHeader)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, ServiceTemplatePMPartHeader.ID)
            DbCommandWrapper.AddInParameter("@Varian", DbType.AnsiString, ServiceTemplatePMPartHeader.Varian)
            'DbCommandWrapper.AddInParameter("@ValidFrom", DbType.DateTime, ServiceTemplatePMPartHeader.ValidFrom)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, ServiceTemplatePMPartHeader.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, ServiceTemplatePMPartHeader.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@PMKindID", DbType.Int32, Me.GetRefObject(ServiceTemplatePMPartHeader.PMKind))
            'DbCommandWrapper.AddInParameter("@VechileTypeID", DbType.Int16, Me.GetRefObject(ServiceTemplatePMPartHeader.VechileType))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As ServiceTemplatePMPartHeader

            Dim ServiceTemplatePMPartHeader As ServiceTemplatePMPartHeader = New ServiceTemplatePMPartHeader

            ServiceTemplatePMPartHeader.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Varian")) Then ServiceTemplatePMPartHeader.Varian = dr("Varian").ToString
            'If Not dr.IsDBNull(dr.GetOrdinal("ValidFrom")) Then ServiceTemplatePMPartHeader.ValidFrom = CType(dr("ValidFrom"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then ServiceTemplatePMPartHeader.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then ServiceTemplatePMPartHeader.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then ServiceTemplatePMPartHeader.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdatedBy")) Then ServiceTemplatePMPartHeader.LastUpdatedBy = dr("LastUpdatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdatedTime")) Then ServiceTemplatePMPartHeader.LastUpdatedTime = CType(dr("LastUpdatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("PMKindID")) Then
                ServiceTemplatePMPartHeader.PMKind = New PMKind(CType(dr("PMKindID"), Integer))
            End If
            'If Not dr.IsDBNull(dr.GetOrdinal("VechileTypeID")) Then
            '    ServiceTemplatePMPartHeader.VechileType = New VechileType(CType(dr("VechileTypeID"), Short))
            'End If

            Return ServiceTemplatePMPartHeader

        End Function

        Private Sub SetTableName()

            If Not (GetType(ServiceTemplatePMPartHeader) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(ServiceTemplatePMPartHeader), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(ServiceTemplatePMPartHeader).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

