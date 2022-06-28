
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : RecallService Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2016 
'// ---------------------
'// $History      : $
'// Generated on 19/04/2016 - 13:07:21
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

    Public Class RecallServiceMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertRecallService"
        Private m_UpdateStatement As String = "up_UpdateRecallService"
        Private m_RetrieveStatement As String = "up_RetrieveRecallService"
        Private m_RetrieveListStatement As String = "up_RetrieveRecallServiceList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteRecallService"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim recallService As RecallService = Nothing
            While dr.Read

                recallService = Me.CreateObject(dr)

            End While

            Return recallService

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim recallServiceList As ArrayList = New ArrayList

            While dr.Read
                Dim recallService As RecallService = Me.CreateObject(dr)
                recallServiceList.Add(recallService)
            End While

            Return recallServiceList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim recallService As RecallService = CType(obj, RecallService)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, recallService.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim recallService As RecallService = CType(obj, RecallService)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@MileAge", DbType.Int32, recallService.MileAge)
            DbCommandWrapper.AddInParameter("@ServiceDate", DbType.DateTime, recallService.ServiceDate)
            DbCommandWrapper.AddInParameter("@WorkOrderNumber", DbType.AnsiString, recallService.WorkOrderNumber)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, recallService.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, recallService.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            If recallService.ChassisID <> 0 Then
                DbCommandWrapper.AddInParameter("@ChassisMasterID", DbType.Int32, recallService.ChassisID)
            ElseIf recallService.ChassisBBID <> 0 Then
                DbCommandWrapper.AddInParameter("@ChassisMasterBBID", DbType.Int32, recallService.ChassisBBID)
            ElseIf (Not IsNothing(recallService.ChassisMaster)) Then
                DbCommandWrapper.AddInParameter("@ChassisMasterID", DbType.Int32, Me.GetRefObject(recallService.ChassisMaster))
            ElseIf (Not IsNothing(recallService.ChassisMasterBB)) Then
                DbCommandWrapper.AddInParameter("@ChassisMasterBBID", DbType.Int32, Me.GetRefObject(recallService.ChassisMasterBB))
            End If

            DbCommandWrapper.AddInParameter("@RecallChassisMasterID", DbType.Int32, Me.GetRefObject(recallService.RecallChassisMaster))
            DbCommandWrapper.AddInParameter("@ServiceDealerID", DbType.Int16, Me.GetRefObject(recallService.Dealer))
            DbCommandWrapper.AddInParameter("@DealerBranchID", DbType.Int32, Me.GetRefObject(recallService.DealerBranch))

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

            Dim recallService As RecallService = CType(obj, RecallService)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, recallService.ID)
            DbCommandWrapper.AddInParameter("@MileAge", DbType.Int32, recallService.MileAge)
            DbCommandWrapper.AddInParameter("@ServiceDate", DbType.DateTime, recallService.ServiceDate)
            DbCommandWrapper.AddInParameter("@WorkOrderNumber", DbType.AnsiString, recallService.WorkOrderNumber)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, recallService.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, recallService.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            If recallService.ChassisID <> 0 Then
                DbCommandWrapper.AddInParameter("@ChassisMasterID", DbType.Int32, recallService.ChassisID)
            ElseIf recallService.ChassisBBID <> 0 Then
                DbCommandWrapper.AddInParameter("@ChassisMasterBBID", DbType.Int32, recallService.ChassisBBID)
            ElseIf (Not IsNothing(recallService.ChassisMaster)) Then
                DbCommandWrapper.AddInParameter("@ChassisMasterID", DbType.Int32, Me.GetRefObject(recallService.ChassisMaster))
            ElseIf (Not IsNothing(recallService.ChassisMasterBB)) Then
                DbCommandWrapper.AddInParameter("@ChassisMasterBBID", DbType.Int32, Me.GetRefObject(recallService.ChassisMasterBB))
            End If

            DbCommandWrapper.AddInParameter("@RecallChassisMasterID", DbType.Int32, Me.GetRefObject(recallService.RecallChassisMaster))
            DbCommandWrapper.AddInParameter("@ServiceDealerID", DbType.Int16, Me.GetRefObject(recallService.Dealer))
            DbCommandWrapper.AddInParameter("@DealerBranchID", DbType.Int32, Me.GetRefObject(recallService.DealerBranch))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As RecallService

            Dim recallService As RecallService = New RecallService

            recallService.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("MileAge")) Then recallService.MileAge = CType(dr("MileAge"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("ServiceDate")) Then recallService.ServiceDate = CType(dr("ServiceDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("WorkOrderNumber")) Then recallService.WorkOrderNumber = dr("WorkOrderNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then recallService.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then recallService.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then recallService.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then recallService.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then recallService.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("ChassisMasterID")) Then
                recallService.ChassisMaster = New ChassisMaster(CType(dr("ChassisMasterID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("ChassisMasterBBID")) Then
                recallService.ChassisMasterBB = New ChassisMasterBB(CType(dr("ChassisMasterBBID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("RecallChassisMasterID")) Then
                recallService.RecallChassisMaster = New RecallChassisMaster(CType(dr("RecallChassisMasterID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("ServiceDealerID")) Then
                recallService.Dealer = New Dealer(CType(dr("ServiceDealerID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("DealerBranchID")) Then
                recallService.DealerBranch = New DealerBranch(CType(dr("DealerBranchID"), Integer))
            End If

            Return recallService

        End Function

        Private Sub SetTableName()

            If Not (GetType(RecallService) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(RecallService), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(RecallService).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

