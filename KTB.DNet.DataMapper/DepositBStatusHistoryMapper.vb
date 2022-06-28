
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : DepositBStatusHistory Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2016 
'// ---------------------
'// $History      : $
'// Generated on 6/1/2016 - 11:20:01 AM
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

    Public Class DepositBStatusHistoryMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertDepositBStatusHistory"
        Private m_UpdateStatement As String = "up_UpdateDepositBStatusHistory"
        Private m_RetrieveStatement As String = "up_RetrieveDepositBStatusHistory"
        Private m_RetrieveListStatement As String = "up_RetrieveDepositBStatusHistoryList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteDepositBStatusHistory"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim depositBStatusHistory As DepositBStatusHistory = Nothing
            While dr.Read

                depositBStatusHistory = Me.CreateObject(dr)

            End While

            Return depositBStatusHistory

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim depositBStatusHistoryList As ArrayList = New ArrayList

            While dr.Read
                Dim depositBStatusHistory As DepositBStatusHistory = Me.CreateObject(dr)
                depositBStatusHistoryList.Add(depositBStatusHistory)
            End While

            Return depositBStatusHistoryList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim depositBStatusHistory As DepositBStatusHistory = CType(obj, DepositBStatusHistory)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, depositBStatusHistory.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim depositBStatusHistory As DepositBStatusHistory = CType(obj, DepositBStatusHistory)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@StatusType", DbType.Int16, depositBStatusHistory.StatusType)
            DbCommandWrapper.AddInParameter("@OldStatus", DbType.Int16, depositBStatusHistory.OldStatus)
            DbCommandWrapper.AddInParameter("@NewStatus", DbType.Int16, depositBStatusHistory.NewStatus)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, depositBStatusHistory.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, depositBStatusHistory.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@DepositBPencairanHeaderID", DbType.Int32, Me.GetRefObject(depositBStatusHistory.DepositBPencairanHeader))
            DbCommandWrapper.AddInParameter("@DepositBDebitNoteID", DbType.Int32, Me.GetRefObject(depositBStatusHistory.DepositBDebitNote))
            DbCommandWrapper.AddInParameter("@DepositBInterestHID", DbType.Int32, Me.GetRefObject(depositBStatusHistory.DepositBInterestHeader))
            DbCommandWrapper.AddInParameter("@KewajibanHeaderID", DbType.Int32, Me.GetRefObject(depositBStatusHistory.DepositBKewajibanHeader))
            'DbCommandWrapper.AddInParameter("@IndentPartEqHeaderID", DbType.Int32, Me.GetRefObject(depositBStatusHistory.IndentPartHeader))

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

            Dim depositBStatusHistory As DepositBStatusHistory = CType(obj, DepositBStatusHistory)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, depositBStatusHistory.ID)
            DbCommandWrapper.AddInParameter("@StatusType", DbType.Int16, depositBStatusHistory.StatusType)
            DbCommandWrapper.AddInParameter("@OldStatus", DbType.Int16, depositBStatusHistory.OldStatus)
            DbCommandWrapper.AddInParameter("@NewStatus", DbType.Int16, depositBStatusHistory.NewStatus)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, depositBStatusHistory.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, depositBStatusHistory.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@DepositBPencairanHeaderID", DbType.Int32, Me.GetRefObject(depositBStatusHistory.DepositBPencairanHeader))
            DbCommandWrapper.AddInParameter("@DepositBDebitNoteID", DbType.Int32, Me.GetRefObject(depositBStatusHistory.DepositBDebitNote))
            DbCommandWrapper.AddInParameter("@DepositBInterestHID", DbType.Int32, Me.GetRefObject(depositBStatusHistory.DepositBInterestHeader))
            DbCommandWrapper.AddInParameter("@KewajibanHeaderID", DbType.Int32, Me.GetRefObject(depositBStatusHistory.DepositBKewajibanHeader))
            'DbCommandWrapper.AddInParameter("@IndentPartEqHeaderID", DbType.Int32, Me.GetRefObject(depositBStatusHistory.IndentPartHeader))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As DepositBStatusHistory

            Dim depositBStatusHistory As DepositBStatusHistory = New DepositBStatusHistory

            depositBStatusHistory.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("StatusType")) Then depositBStatusHistory.StatusType = CType(dr("StatusType"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("OldStatus")) Then depositBStatusHistory.OldStatus = CType(dr("OldStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("NewStatus")) Then depositBStatusHistory.NewStatus = CType(dr("NewStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then depositBStatusHistory.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then depositBStatusHistory.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then depositBStatusHistory.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then depositBStatusHistory.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then depositBStatusHistory.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("DepositBPencairanHeaderID")) Then
                depositBStatusHistory.DepositBPencairanHeader = New DepositBPencairanHeader(CType(dr("DepositBPencairanHeaderID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("DepositBDebitNoteID")) Then
                depositBStatusHistory.DepositBDebitNote = New DepositBDebitNote(CType(dr("DepositBDebitNoteID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("DepositBInterestHID")) Then
                depositBStatusHistory.DepositBInterestHeader = New DepositBInterestHeader(CType(dr("DepositBInterestHID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("KewajibanHeaderID")) Then
                depositBStatusHistory.DepositBKewajibanHeader = New DepositBKewajibanHeader(CType(dr("KewajibanHeaderID"), Integer))
            End If
            'If Not dr.IsDBNull(dr.GetOrdinal("IndentPartEqHeaderID")) Then
            '    depositBStatusHistory.IndentPartHeader = New IndentPartHeader(CType(dr("IndentPartEqHeaderID"), Integer))
            'End If

            Return depositBStatusHistory

        End Function

        Private Sub SetTableName()

            If Not (GetType(DepositBStatusHistory) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(DepositBStatusHistory), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(DepositBStatusHistory).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

