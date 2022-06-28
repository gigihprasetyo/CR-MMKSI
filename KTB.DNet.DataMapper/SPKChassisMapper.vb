
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : Mitrais Team
'// PURPOSE       : SPKChassis Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 22/02/2018 - 11:15:56
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

    Public Class SPKChassisMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertSPKChassis"
        Private m_UpdateStatement As String = "up_UpdateSPKChassis"
        Private m_RetrieveStatement As String = "up_RetrieveSPKChassis"
        Private m_RetrieveListStatement As String = "up_RetrieveSPKChassisList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteSPKChassis"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim sPKChassis As SPKChassis = Nothing
            While dr.Read

                sPKChassis = Me.CreateObject(dr)

            End While

            Return sPKChassis

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim sPKChassisList As ArrayList = New ArrayList

            While dr.Read
                Dim sPKChassis As SPKChassis = Me.CreateObject(dr)
                sPKChassisList.Add(sPKChassis)
            End While

            Return sPKChassisList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim sPKChassis As SPKChassis = CType(obj, SPKChassis)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, sPKChassis.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim sPKChassis As SPKChassis = CType(obj, SPKChassis)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@MatchingType", DbType.Int16, sPKChassis.MatchingType)
            DbCommandWrapper.AddInParameter("@MatchingDate", DbType.DateTime, sPKChassis.MatchingDate)
            DbCommandWrapper.AddInParameter("@MatchingNumber", DbType.AnsiString, sPKChassis.MatchingNumber)
            DbCommandWrapper.AddInParameter("@ReferenceNumber", DbType.AnsiString, sPKChassis.ReferenceNumber)
            DbCommandWrapper.AddInParameter("@KeyNumber", DbType.AnsiString, sPKChassis.KeyNumber)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, sPKChassis.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, sPKChassis.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@ChassisMasterID", DbType.Int32, Me.GetRefObject(sPKChassis.ChassisMaster))
            DbCommandWrapper.AddInParameter("@SPKDetailID", DbType.Int32, Me.GetRefObject(sPKChassis.SPKDetail))

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

            Dim sPKChassis As SPKChassis = CType(obj, SPKChassis)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, sPKChassis.ID)
            DbCommandWrapper.AddInParameter("@MatchingType", DbType.Int16, sPKChassis.MatchingType)
            DbCommandWrapper.AddInParameter("@MatchingDate", DbType.DateTime, sPKChassis.MatchingDate)
            DbCommandWrapper.AddInParameter("@MatchingNumber", DbType.AnsiString, sPKChassis.MatchingNumber)
            DbCommandWrapper.AddInParameter("@ReferenceNumber", DbType.AnsiString, sPKChassis.ReferenceNumber)
            DbCommandWrapper.AddInParameter("@KeyNumber", DbType.AnsiString, sPKChassis.KeyNumber)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, sPKChassis.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, sPKChassis.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@ChassisMasterID", DbType.Int32, Me.GetRefObject(sPKChassis.ChassisMaster))
            DbCommandWrapper.AddInParameter("@SPKDetailID", DbType.Int32, Me.GetRefObject(sPKChassis.SPKDetail))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As SPKChassis

            Dim sPKChassis As SPKChassis = New SPKChassis

            sPKChassis.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("MatchingType")) Then sPKChassis.MatchingType = CType(dr("MatchingType"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("MatchingDate")) Then sPKChassis.MatchingDate = CType(dr("MatchingDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("MatchingNumber")) Then sPKChassis.MatchingNumber = dr("MatchingNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ReferenceNumber")) Then sPKChassis.ReferenceNumber = dr("ReferenceNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("KeyNumber")) Then sPKChassis.KeyNumber = dr("KeyNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then sPKChassis.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then sPKChassis.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then sPKChassis.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then sPKChassis.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then sPKChassis.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("ChassisMasterID")) Then
                sPKChassis.ChassisMaster = New ChassisMaster(CType(dr("ChassisMasterID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("SPKDetailID")) Then
                sPKChassis.SPKDetail = New SPKDetail(CType(dr("SPKDetailID"), Integer))
            End If

            Return sPKChassis

        End Function

        Private Sub SetTableName()

            If Not (GetType(SPKChassis) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(SPKChassis), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(SPKChassis).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

