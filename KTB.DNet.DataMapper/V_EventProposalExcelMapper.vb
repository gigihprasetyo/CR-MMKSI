#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : V_EventProposalExcel Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2009 
'// ---------------------
'// $History      : $
'// Generated on 9/10/2009 - 11:58:36 AM
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

    Public Class V_EventProposalExcelMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertV_EventProposalExcel"
        Private m_UpdateStatement As String = "up_UpdateV_EventProposalExcel"
        Private m_RetrieveStatement As String = "up_RetrieveV_EventProposalExcel"
        Private m_RetrieveListStatement As String = "up_RetrieveV_EventProposalExcelList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteV_EventProposalExcel"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim v_EventProposalExcel As V_EventProposalExcel = Nothing
            While dr.Read

                v_EventProposalExcel = Me.CreateObject(dr)

            End While

            Return v_EventProposalExcel

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim v_EventProposalExcelList As ArrayList = New ArrayList

            While dr.Read
                Dim v_EventProposalExcel As V_EventProposalExcel = Me.CreateObject(dr)
                v_EventProposalExcelList.Add(v_EventProposalExcel)
            End While

            Return v_EventProposalExcelList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim v_EventProposalExcel As V_EventProposalExcel = CType(obj, V_EventProposalExcel)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, v_EventProposalExcel.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim v_EventProposalExcel As V_EventProposalExcel = CType(obj, V_EventProposalExcel)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@EventProposalID", DbType.Int32, v_EventProposalExcel.EventProposalID)
            DbCommandWrapper.AddInParameter("@EventProposalStatus", DbType.Byte, v_EventProposalExcel.EventProposalStatus)
            DBCommandWrapper.AddInParameter("@EventName", DbType.AnsiString, v_EventProposalExcel.EventName)
            DBCommandWrapper.AddInParameter("@ActivityName", DbType.AnsiString, v_EventProposalExcel.ActivityName)
            DbCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, v_EventProposalExcel.DealerCode)
            DbCommandWrapper.AddInParameter("@DealerName", DbType.AnsiString, v_EventProposalExcel.DealerName)
            DbCommandWrapper.AddInParameter("@ActivitySchedule", DbType.DateTime, v_EventProposalExcel.ActivitySchedule)
            DBCommandWrapper.AddInParameter("@ActivityPlace", DbType.AnsiString, v_EventProposalExcel.ActivityPlace)
            DBCommandWrapper.AddInParameter("@TotalCost", DbType.Decimal, v_EventProposalExcel.TotalCost)
            DbCommandWrapper.AddInParameter("@TamuName", DbType.AnsiString, v_EventProposalExcel.TamuName)
            DbCommandWrapper.AddInParameter("@GuestType", DbType.AnsiString, v_EventProposalExcel.GuestType)
            DbCommandWrapper.AddInParameter("@JabatanName", DbType.AnsiString, v_EventProposalExcel.JabatanName)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int32, v_EventProposalExcel.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, v_EventProposalExcel.LastUpdateBy)
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

            Dim v_EventProposalExcel As V_EventProposalExcel = CType(obj, V_EventProposalExcel)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, v_EventProposalExcel.ID)
            DbCommandWrapper.AddInParameter("@EventProposalID", DbType.Int32, v_EventProposalExcel.EventProposalID)
            DbCommandWrapper.AddInParameter("@EventProposalStatus", DbType.Byte, v_EventProposalExcel.EventProposalStatus)
            DBCommandWrapper.AddInParameter("@EventName", DbType.AnsiString, v_EventProposalExcel.EventName)
            DBCommandWrapper.AddInParameter("@ActivityName", DbType.AnsiString, v_EventProposalExcel.ActivityName)
            DbCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, v_EventProposalExcel.DealerCode)
            DbCommandWrapper.AddInParameter("@DealerName", DbType.AnsiString, v_EventProposalExcel.DealerName)
            DbCommandWrapper.AddInParameter("@ActivitySchedule", DbType.DateTime, v_EventProposalExcel.ActivitySchedule)
            DbCommandWrapper.AddInParameter("@ActivityPlace", DbType.AnsiString, v_EventProposalExcel.ActivityPlace)
            DBCommandWrapper.AddInParameter("@TotalCost", DbType.Decimal, v_EventProposalExcel.TotalCost)
            DBCommandWrapper.AddInParameter("@TamuName", DbType.AnsiString, v_EventProposalExcel.TamuName)
            DbCommandWrapper.AddInParameter("@GuestType", DbType.AnsiString, v_EventProposalExcel.GuestType)
            DbCommandWrapper.AddInParameter("@JabatanName", DbType.AnsiString, v_EventProposalExcel.JabatanName)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int32, v_EventProposalExcel.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, v_EventProposalExcel.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As V_EventProposalExcel

            Dim v_EventProposalExcel As V_EventProposalExcel = New V_EventProposalExcel

            v_EventProposalExcel.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("EventProposalID")) Then v_EventProposalExcel.EventProposalID = CType(dr("EventProposalID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("EventProposalStatus")) Then v_EventProposalExcel.EventProposalStatus = CType(dr("EventProposalStatus"), Byte)
            If Not dr.IsDBNull(dr.GetOrdinal("EventName")) Then v_EventProposalExcel.EventName = dr("EventName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ActivityName")) Then v_EventProposalExcel.ActivityName = dr("ActivityName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DealerCode")) Then v_EventProposalExcel.DealerCode = dr("DealerCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DealerName")) Then v_EventProposalExcel.DealerName = dr("DealerName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ActivitySchedule")) Then v_EventProposalExcel.ActivitySchedule = CType(dr("ActivitySchedule"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("ActivityPlace")) Then v_EventProposalExcel.ActivityPlace = dr("ActivityPlace").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("TotalCost")) Then v_EventProposalExcel.TotalCost = CType(dr("TotalCost"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("TamuName")) Then v_EventProposalExcel.TamuName = dr("TamuName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("GuestType")) Then v_EventProposalExcel.GuestType = dr("GuestType").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("JabatanName")) Then v_EventProposalExcel.JabatanName = dr("JabatanName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then v_EventProposalExcel.RowStatus = CType(dr("RowStatus"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then v_EventProposalExcel.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then v_EventProposalExcel.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then v_EventProposalExcel.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then v_EventProposalExcel.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            Return v_EventProposalExcel

        End Function

        Private Sub SetTableName()

            If Not (GetType(V_EventProposalExcel) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(V_EventProposalExcel), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(V_EventProposalExcel).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

