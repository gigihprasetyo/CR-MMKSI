
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : MDPMasterDealerHistory Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2019 
'// ---------------------
'// $History      : $
'// Generated on 19/02/2019 - 16:32:06
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

    Public Class MDPMasterDealerHistoryMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertMDPMasterDealerHistory"
        Private m_UpdateStatement As String = "up_UpdateMDPMasterDealerHistory"
        Private m_RetrieveStatement As String = "up_RetrieveMDPMasterDealerHistory"
        Private m_RetrieveListStatement As String = "up_RetrieveMDPMasterDealerHistoryList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteMDPMasterDealerHistory"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim mDPMasterDealerHistory As MDPMasterDealerHistory = Nothing
            While dr.Read

                mDPMasterDealerHistory = Me.CreateObject(dr)

            End While

            Return mDPMasterDealerHistory

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim mDPMasterDealerHistoryList As ArrayList = New ArrayList

            While dr.Read
                Dim mDPMasterDealerHistory As MDPMasterDealerHistory = Me.CreateObject(dr)
                mDPMasterDealerHistoryList.Add(mDPMasterDealerHistory)
            End While

            Return mDPMasterDealerHistoryList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim mDPMasterDealerHistory As MDPMasterDealerHistory = CType(obj, MDPMasterDealerHistory)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, mDPMasterDealerHistory.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim mDPMasterDealerHistory As MDPMasterDealerHistory = CType(obj, MDPMasterDealerHistory)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            'DbCommandWrapper.AddInParameter("@MDPMasterDealerID", DbType.Int32, mDPMasterDealerHistory.MDPMasterDealerID)
            DbCommandWrapper.AddInParameter("@StatusFrom", DbType.Int32, mDPMasterDealerHistory.StatusFrom)
            DbCommandWrapper.AddInParameter("@StatusTo", DbType.Int32, mDPMasterDealerHistory.StatusTo)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, mDPMasterDealerHistory.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, mDPMasterDealerHistory.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@MDPMasterDealerID", DbType.Int32, Me.GetRefObject(mDPMasterDealerHistory.MDPMasterDealer))

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

            Dim mDPMasterDealerHistory As MDPMasterDealerHistory = CType(obj, MDPMasterDealerHistory)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, mDPMasterDealerHistory.ID)
            'DbCommandWrapper.AddInParameter("@MDPMasterDealerID", DbType.Int32, mDPMasterDealerHistory.MDPMasterDealerID)
            DbCommandWrapper.AddInParameter("@StatusFrom", DbType.Int32, mDPMasterDealerHistory.StatusFrom)
            DbCommandWrapper.AddInParameter("@StatusTo", DbType.Int32, mDPMasterDealerHistory.StatusTo)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, mDPMasterDealerHistory.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, mDPMasterDealerHistory.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@MDPMasterDealerID", DbType.Int32, Me.GetRefObject(mDPMasterDealerHistory.MDPMasterDealer))


            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As MDPMasterDealerHistory

            Dim mDPMasterDealerHistory As MDPMasterDealerHistory = New MDPMasterDealerHistory

            mDPMasterDealerHistory.ID = CType(dr("ID"), Integer)
            'If Not dr.IsDBNull(dr.GetOrdinal("MDPMasterDealerID")) Then mDPMasterDealerHistory.MDPMasterDealerID = CType(dr("MDPMasterDealerID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("StatusFrom")) Then mDPMasterDealerHistory.StatusFrom = CType(dr("StatusFrom"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("StatusTo")) Then mDPMasterDealerHistory.StatusTo = CType(dr("StatusTo"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then mDPMasterDealerHistory.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then mDPMasterDealerHistory.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then mDPMasterDealerHistory.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then mDPMasterDealerHistory.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then mDPMasterDealerHistory.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            If Not dr.IsDBNull(dr.GetOrdinal("MDPMasterDealerID")) Then
                mDPMasterDealerHistory.MDPMasterDealer = New MDPMasterDealer(CType(dr("MDPMasterDealerID"), Integer))
            End If

            Return mDPMasterDealerHistory

        End Function

        Private Sub SetTableName()

            If Not (GetType(MDPMasterDealerHistory) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(MDPMasterDealerHistory), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(MDPMasterDealerHistory).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

