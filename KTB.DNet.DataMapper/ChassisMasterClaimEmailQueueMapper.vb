
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : ChassisMasterClaimEmailQueue Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2020 
'// ---------------------
'// $History      : $
'// Generated on 10/7/2020 - 10:20:16 AM
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

    Public Class ChassisMasterClaimEmailQueueMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertChassisMasterClaimEmailQueue"
        Private m_UpdateStatement As String = "up_UpdateChassisMasterClaimEmailQueue"
        Private m_RetrieveStatement As String = "up_RetrieveChassisMasterClaimEmailQueue"
        Private m_RetrieveListStatement As String = "up_RetrieveChassisMasterClaimEmailQueueList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteChassisMasterClaimEmailQueue"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim chassisMasterClaimEmailQueue As ChassisMasterClaimEmailQueue = Nothing
            While dr.Read

                chassisMasterClaimEmailQueue = Me.CreateObject(dr)

            End While

            Return chassisMasterClaimEmailQueue

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim chassisMasterClaimEmailQueueList As ArrayList = New ArrayList

            While dr.Read
                Dim chassisMasterClaimEmailQueue As ChassisMasterClaimEmailQueue = Me.CreateObject(dr)
                chassisMasterClaimEmailQueueList.Add(chassisMasterClaimEmailQueue)
            End While

            Return chassisMasterClaimEmailQueueList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim chassisMasterClaimEmailQueue As ChassisMasterClaimEmailQueue = CType(obj, ChassisMasterClaimEmailQueue)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, chassisMasterClaimEmailQueue.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim chassisMasterClaimEmailQueue As ChassisMasterClaimEmailQueue = CType(obj, ChassisMasterClaimEmailQueue)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@ClaimNumber", DbType.AnsiString, chassisMasterClaimEmailQueue.ClaimNumber)
            DbCommandWrapper.AddInParameter("@StatusClaim", DbType.Int16, chassisMasterClaimEmailQueue.StatusClaim)
            DbCommandWrapper.AddInParameter("@StatusReturnProcess", DbType.Int16, chassisMasterClaimEmailQueue.StatusReturnProcess)
            DbCommandWrapper.AddInParameter("@IsSend", DbType.Int16, chassisMasterClaimEmailQueue.IsSend)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, chassisMasterClaimEmailQueue.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, chassisMasterClaimEmailQueue.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


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

            Dim chassisMasterClaimEmailQueue As ChassisMasterClaimEmailQueue = CType(obj, ChassisMasterClaimEmailQueue)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, chassisMasterClaimEmailQueue.ID)
            DbCommandWrapper.AddInParameter("@ClaimNumber", DbType.AnsiString, chassisMasterClaimEmailQueue.ClaimNumber)
            DbCommandWrapper.AddInParameter("@StatusClaim", DbType.Int16, chassisMasterClaimEmailQueue.StatusClaim)
            DbCommandWrapper.AddInParameter("@StatusReturnProcess", DbType.Int16, chassisMasterClaimEmailQueue.StatusReturnProcess)
            DbCommandWrapper.AddInParameter("@IsSend", DbType.Int16, chassisMasterClaimEmailQueue.IsSend)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, chassisMasterClaimEmailQueue.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, chassisMasterClaimEmailQueue.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As ChassisMasterClaimEmailQueue

            Dim chassisMasterClaimEmailQueue As ChassisMasterClaimEmailQueue = New ChassisMasterClaimEmailQueue

            chassisMasterClaimEmailQueue.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("ClaimNumber")) Then chassisMasterClaimEmailQueue.ClaimNumber = dr("ClaimNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("StatusClaim")) Then chassisMasterClaimEmailQueue.StatusClaim = CType(dr("StatusClaim"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("StatusReturnProcess")) Then chassisMasterClaimEmailQueue.StatusReturnProcess = CType(dr("StatusReturnProcess"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("IsSend")) Then chassisMasterClaimEmailQueue.IsSend = CType(dr("IsSend"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then chassisMasterClaimEmailQueue.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then chassisMasterClaimEmailQueue.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then chassisMasterClaimEmailQueue.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then chassisMasterClaimEmailQueue.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then chassisMasterClaimEmailQueue.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            Return chassisMasterClaimEmailQueue

        End Function

        Private Sub SetTableName()

            If Not (GetType(ChassisMasterClaimEmailQueue) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(ChassisMasterClaimEmailQueue), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(ChassisMasterClaimEmailQueue).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

