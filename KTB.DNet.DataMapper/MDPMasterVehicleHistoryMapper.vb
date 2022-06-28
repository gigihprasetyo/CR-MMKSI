
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : MDPMasterVehicleHistory Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2019 
'// ---------------------
'// $History      : $
'// Generated on 20/02/2019 - 16:41:37
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

    Public Class MDPMasterVehicleHistoryMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertMDPMasterVehicleHistory"
        Private m_UpdateStatement As String = "up_UpdateMDPMasterVehicleHistory"
        Private m_RetrieveStatement As String = "up_RetrieveMDPMasterVehicleHistory"
        Private m_RetrieveListStatement As String = "up_RetrieveMDPMasterVehicleHistoryList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteMDPMasterVehicleHistory"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim mDPMasterVehicleHistory As MDPMasterVehicleHistory = Nothing
            While dr.Read

                mDPMasterVehicleHistory = Me.CreateObject(dr)

            End While

            Return mDPMasterVehicleHistory

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim mDPMasterVehicleHistoryList As ArrayList = New ArrayList

            While dr.Read
                Dim mDPMasterVehicleHistory As MDPMasterVehicleHistory = Me.CreateObject(dr)
                mDPMasterVehicleHistoryList.Add(mDPMasterVehicleHistory)
            End While

            Return mDPMasterVehicleHistoryList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim mDPMasterVehicleHistory As MDPMasterVehicleHistory = CType(obj, MDPMasterVehicleHistory)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, mDPMasterVehicleHistory.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim mDPMasterVehicleHistory As MDPMasterVehicleHistory = CType(obj, MDPMasterVehicleHistory)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            'DbCommandWrapper.AddInParameter("@MDPMasterVehicleID", DbType.Int32, mDPMasterVehicleHistory.MDPMasterVehicleID)
            DbCommandWrapper.AddInParameter("@StatusFrom", DbType.Int16, mDPMasterVehicleHistory.StatusFrom)
            DbCommandWrapper.AddInParameter("@StatusTo", DbType.Int16, mDPMasterVehicleHistory.StatusTo)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, mDPMasterVehicleHistory.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, mDPMasterVehicleHistory.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@MDPMasterVehicleID", DbType.Int32, Me.GetRefObject(mDPMasterVehicleHistory.MDPMasterVehicle))

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

            Dim mDPMasterVehicleHistory As MDPMasterVehicleHistory = CType(obj, MDPMasterVehicleHistory)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, mDPMasterVehicleHistory.ID)
            'DbCommandWrapper.AddInParameter("@MDPMasterVehicleID", DbType.Int32, mDPMasterVehicleHistory.MDPMasterVehicleID)
            DbCommandWrapper.AddInParameter("@StatusFrom", DbType.Int16, mDPMasterVehicleHistory.StatusFrom)
            DbCommandWrapper.AddInParameter("@StatusTo", DbType.Int16, mDPMasterVehicleHistory.StatusTo)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, mDPMasterVehicleHistory.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, mDPMasterVehicleHistory.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@MDPMasterVehicleID", DbType.Int32, Me.GetRefObject(mDPMasterVehicleHistory.MDPMasterVehicle))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As MDPMasterVehicleHistory

            Dim mDPMasterVehicleHistory As MDPMasterVehicleHistory = New MDPMasterVehicleHistory

            mDPMasterVehicleHistory.ID = CType(dr("ID"), Integer)
            'If Not dr.IsDBNull(dr.GetOrdinal("MDPMasterVehicleID")) Then mDPMasterVehicleHistory.MDPMasterVehicleID = CType(dr("MDPMasterVehicleID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("StatusFrom")) Then mDPMasterVehicleHistory.StatusFrom = CType(dr("StatusFrom"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("StatusTo")) Then mDPMasterVehicleHistory.StatusTo = CType(dr("StatusTo"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then mDPMasterVehicleHistory.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then mDPMasterVehicleHistory.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then mDPMasterVehicleHistory.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then mDPMasterVehicleHistory.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then mDPMasterVehicleHistory.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            If Not dr.IsDBNull(dr.GetOrdinal("MDPMasterVehicleID")) Then
                mDPMasterVehicleHistory.MDPMasterVehicle = New MDPMasterVehicle(CType(dr("MDPMasterVehicleID"), Integer))
            End If

            Return mDPMasterVehicleHistory

        End Function

        Private Sub SetTableName()

            If Not (GetType(MDPMasterVehicleHistory) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(MDPMasterVehicleHistory), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(MDPMasterVehicleHistory).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

