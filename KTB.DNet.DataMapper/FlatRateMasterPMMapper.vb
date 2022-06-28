#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : FlatRateMasterPM Objects Mapper.
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

    Public Class FlatRateMasterPMMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertFlatRateMasterPM"
        Private m_UpdateStatement As String = "up_UpdateFlatRateMasterPM"
        Private m_RetrieveStatement As String = "up_RetrieveFlatRateMasterPM"
        Private m_RetrieveListStatement As String = "up_RetrieveFlatRateMasterPMList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteFlatRateMasterPM"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim FlatRateMasterPM As FlatRateMasterPM = Nothing
            While dr.Read

                FlatRateMasterPM = Me.CreateObject(dr)

            End While

            Return FlatRateMasterPM

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim FlatRateMasterPMList As ArrayList = New ArrayList

            While dr.Read
                Dim FlatRateMasterPM As FlatRateMasterPM = Me.CreateObject(dr)
                FlatRateMasterPMList.Add(FlatRateMasterPM)
            End While

            Return FlatRateMasterPMList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim FlatRateMasterPM As FlatRateMasterPM = CType(obj, FlatRateMasterPM)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, FlatRateMasterPM.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim FlatRateMasterPM As FlatRateMasterPM = CType(obj, FlatRateMasterPM)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@Varian", DbType.AnsiString, FlatRateMasterPM.Varian)
            DbCommandWrapper.AddInParameter("@FlatRate", DbType.Decimal, FlatRateMasterPM.FlatRate)
            DbCommandWrapper.AddInParameter("@Status", DbType.AnsiString, FlatRateMasterPM.Status)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, FlatRateMasterPM.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdatedBy", DbType.AnsiString, FlatRateMasterPM.LastUpdatedBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@PMKindID", DbType.Int32, Me.GetRefObject(FlatRateMasterPM.PMKind))

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

            Dim FlatRateMasterPM As FlatRateMasterPM = CType(obj, FlatRateMasterPM)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, FlatRateMasterPM.ID)
            DbCommandWrapper.AddInParameter("@Varian", DbType.AnsiString, FlatRateMasterPM.Varian)
            DbCommandWrapper.AddInParameter("@FlatRate", DbType.Decimal, FlatRateMasterPM.FlatRate)
            DbCommandWrapper.AddInParameter("@Status", DbType.AnsiString, FlatRateMasterPM.Status)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, FlatRateMasterPM.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, FlatRateMasterPM.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@PMKindID", DbType.Int32, Me.GetRefObject(FlatRateMasterPM.PMKind))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As FlatRateMasterPM

            Dim FlatRateMasterPM As FlatRateMasterPM = New FlatRateMasterPM

            FlatRateMasterPM.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Varian")) Then FlatRateMasterPM.Varian = dr("Varian").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("FlatRate")) Then FlatRateMasterPM.FlatRate = CDec(dr("FlatRate").ToString)
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then FlatRateMasterPM.Status = dr("Status").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then FlatRateMasterPM.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then FlatRateMasterPM.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then FlatRateMasterPM.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdatedBy")) Then FlatRateMasterPM.LastUpdatedBy = dr("LastUpdatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdatedTime")) Then FlatRateMasterPM.LastUpdatedTime = CType(dr("LastUpdatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("PMKindID")) Then
                FlatRateMasterPM.PMKind = New PMKind(CType(dr("PMKindID"), Integer))
            End If

            Return FlatRateMasterPM

        End Function

        Private Sub SetTableName()

            If Not (GetType(FlatRateMasterPM) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(FlatRateMasterPM), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(FlatRateMasterPM).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

