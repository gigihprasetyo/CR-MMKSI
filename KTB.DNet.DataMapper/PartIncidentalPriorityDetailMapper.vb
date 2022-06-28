
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : PartIncidentalPriorityDetail Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2009 
'// ---------------------
'// $History      : $
'// Generated on 10/5/2009 - 1:57:03 PM
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

    Public Class PartIncidentalPriorityDetailMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertPartIncidentalPriorityDetail"
        Private m_UpdateStatement As String = "up_UpdatePartIncidentalPriorityDetail"
        Private m_RetrieveStatement As String = "up_RetrievePartIncidentalPriorityDetail"
        Private m_RetrieveListStatement As String = "up_RetrievePartIncidentalPriorityDetailList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeletePartIncidentalPriorityDetail"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim partIncidentalPriorityDetail As PartIncidentalPriorityDetail = Nothing
            While dr.Read

                partIncidentalPriorityDetail = Me.CreateObject(dr)

            End While

            Return partIncidentalPriorityDetail

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim partIncidentalPriorityDetailList As ArrayList = New ArrayList

            While dr.Read
                Dim partIncidentalPriorityDetail As PartIncidentalPriorityDetail = Me.CreateObject(dr)
                partIncidentalPriorityDetailList.Add(partIncidentalPriorityDetail)
            End While

            Return partIncidentalPriorityDetailList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim partIncidentalPriorityDetail As PartIncidentalPriorityDetail = CType(obj, PartIncidentalPriorityDetail)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int16, partIncidentalPriorityDetail.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim partIncidentalPriorityDetail As PartIncidentalPriorityDetail = CType(obj, PartIncidentalPriorityDetail)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int16, 2)
            DbCommandWrapper.AddInParameter("@TypeCode", DbType.AnsiString, partIncidentalPriorityDetail.TypeCode)
            DbCommandWrapper.AddInParameter("@StartProdYear", DbType.Int16, partIncidentalPriorityDetail.StartProdYear)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, partIncidentalPriorityDetail.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, partIncidentalPriorityDetail.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@PartIncidentalPriorityID", DbType.Byte, Me.GetRefObject(partIncidentalPriorityDetail.PartIncidentalPriority))

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

            Dim partIncidentalPriorityDetail As PartIncidentalPriorityDetail = CType(obj, PartIncidentalPriorityDetail)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int16, partIncidentalPriorityDetail.ID)
            DbCommandWrapper.AddInParameter("@TypeCode", DbType.AnsiString, partIncidentalPriorityDetail.TypeCode)
            DbCommandWrapper.AddInParameter("@StartProdYear", DbType.Int16, partIncidentalPriorityDetail.StartProdYear)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, partIncidentalPriorityDetail.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, partIncidentalPriorityDetail.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@PartIncidentalPriorityID", DbType.Byte, Me.GetRefObject(partIncidentalPriorityDetail.PartIncidentalPriority))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As PartIncidentalPriorityDetail

            Dim partIncidentalPriorityDetail As PartIncidentalPriorityDetail = New PartIncidentalPriorityDetail

            partIncidentalPriorityDetail.ID = CType(dr("ID"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("TypeCode")) Then partIncidentalPriorityDetail.TypeCode = dr("TypeCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("StartProdYear")) Then partIncidentalPriorityDetail.StartProdYear = CType(dr("StartProdYear"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then partIncidentalPriorityDetail.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then partIncidentalPriorityDetail.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then partIncidentalPriorityDetail.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then partIncidentalPriorityDetail.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then partIncidentalPriorityDetail.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("PartIncidentalPriorityID")) Then
                partIncidentalPriorityDetail.PartIncidentalPriority = New PartIncidentalPriority(CType(dr("PartIncidentalPriorityID"), Byte))
            End If

            Return partIncidentalPriorityDetail

        End Function

        Private Sub SetTableName()

            If Not (GetType(PartIncidentalPriorityDetail) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(PartIncidentalPriorityDetail), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(PartIncidentalPriorityDetail).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

