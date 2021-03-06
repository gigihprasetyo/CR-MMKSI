#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : FlatRateMasterGR Objects Mapper.
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

    Public Class FlatRateMasterGRMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertFlatRateMasterGR"
        Private m_UpdateStatement As String = "up_UpdateFlatRateMasterGR"
        Private m_RetrieveStatement As String = "up_RetrieveFlatRateMasterGR"
        Private m_RetrieveListStatement As String = "up_RetrieveFlatRateMasterGRList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteFlatRateMasterGR"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim FlatRateMasterGR As FlatRateMasterGR = Nothing
            While dr.Read

                FlatRateMasterGR = Me.CreateObject(dr)

            End While

            Return FlatRateMasterGR

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim FlatRateMasterGRList As ArrayList = New ArrayList

            While dr.Read
                Dim FlatRateMasterGR As FlatRateMasterGR = Me.CreateObject(dr)
                FlatRateMasterGRList.Add(FlatRateMasterGR)
            End While

            Return FlatRateMasterGRList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim FlatRateMasterGR As FlatRateMasterGR = CType(obj, FlatRateMasterGR)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, FlatRateMasterGR.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim FlatRateMasterGR As FlatRateMasterGR = CType(obj, FlatRateMasterGR)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@Varian", DbType.AnsiString, FlatRateMasterGR.Varian)
            DbCommandWrapper.AddInParameter("@FlatRate", DbType.Decimal, FlatRateMasterGR.FlatRate)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int32, FlatRateMasterGR.Status)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, FlatRateMasterGR.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdatedBy", DbType.AnsiString, FlatRateMasterGR.LastUpdatedBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@GRKindID", DbType.Int32, Me.GetRefObject(FlatRateMasterGR.GRKind))

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

            Dim FlatRateMasterGR As FlatRateMasterGR = CType(obj, FlatRateMasterGR)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, FlatRateMasterGR.ID)
            DbCommandWrapper.AddInParameter("@Varian", DbType.AnsiString, FlatRateMasterGR.Varian)
            DbCommandWrapper.AddInParameter("@FlatRate", DbType.Decimal, FlatRateMasterGR.FlatRate)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int32, FlatRateMasterGR.Status)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, FlatRateMasterGR.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, FlatRateMasterGR.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@GRKindID", DbType.Int32, Me.GetRefObject(FlatRateMasterGR.GRKind))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As FlatRateMasterGR

            Dim FlatRateMasterGR As FlatRateMasterGR = New FlatRateMasterGR

            FlatRateMasterGR.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Varian")) Then FlatRateMasterGR.Varian = dr("Varian").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("FlatRate")) Then FlatRateMasterGR.FlatRate = CDec(dr("FlatRate").ToString)
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then FlatRateMasterGR.Status = dr("Status").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then FlatRateMasterGR.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then FlatRateMasterGR.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then FlatRateMasterGR.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdatedBy")) Then FlatRateMasterGR.LastUpdatedBy = dr("LastUpdatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdatedTime")) Then FlatRateMasterGR.LastUpdatedTime = CType(dr("LastUpdatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("GRKindID")) Then
                FlatRateMasterGR.GRKind = New GRKind(CType(dr("GRKindID"), Integer))
            End If

            Return FlatRateMasterGR

        End Function

        Private Sub SetTableName()

            If Not (GetType(FlatRateMasterGR) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(FlatRateMasterGR), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(FlatRateMasterGR).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

