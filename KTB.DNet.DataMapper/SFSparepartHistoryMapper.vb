
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : SFSparepartHistory Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 03/07/2018 - 2:49:10 PM
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

    Public Class SFSparepartHistoryMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertSFSparepartHistory"
        Private m_UpdateStatement As String = "up_UpdateSFSparepartHistory"
        Private m_RetrieveStatement As String = "up_RetrieveSFSparepartHistory"
        Private m_RetrieveListStatement As String = "up_RetrieveSFSparepartHistoryList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteSFSparepartHistory"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim SFSparepartHistory As SFSparepartHistory = Nothing
            While dr.Read

                SFSparepartHistory = Me.CreateObject(dr)

            End While

            Return SFSparepartHistory

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim SFSparepartHistoryList As ArrayList = New ArrayList

            While dr.Read
                Dim SFSparepartHistory As SFSparepartHistory = Me.CreateObject(dr)
                SFSparepartHistoryList.Add(SFSparepartHistory)
            End While

            Return SFSparepartHistoryList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim SFSparepartHistory As SFSparepartHistory = CType(obj, SFSparepartHistory)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, SFSparepartHistory.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim SFSparepartHistory As SFSparepartHistory = CType(obj, SFSparepartHistory)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@KeyID", DbType.AnsiString, SFSparepartHistory.KeyID)
            'DbCommandWrapper.AddInParameter("@PMHeaderID", DbType.Int32, Me.GetRefObject(SFSparepartHistory.PMHeader))
            DbCommandWrapper.AddInParameter("@AssistPartSalesID", DbType.Int32, Me.GetRefObject(SFSparepartHistory.AssistPartSales))
            'DbCommandWrapper.AddInParameter("@EndCustomerID", DbType.Int32, Me.GetRefObject(SFSparepartHistory.EndCustomer))
            'DbCommandWrapper.AddInParameter("@AssistPartSalesID", DbType.Int32, Me.GetRefObject(SFSparepartHistory.AssistServiceIncoming))
            DbCommandWrapper.AddInParameter("@IsSynchronizeSF", DbType.Boolean, SFSparepartHistory.IsSynchronizeSF)
            DbCommandWrapper.AddInParameter("@SynchronizeDateSF", DbType.DateTime, SFSparepartHistory.SynchronizeDateSF)
            DbCommandWrapper.AddInParameter("@IsSynchronizeMMID", DbType.Boolean, SFSparepartHistory.IsSynchronizeMMID)
            DbCommandWrapper.AddInParameter("@SynchronizeDateMMID", DbType.DateTime, SFSparepartHistory.SynchronizeDateMMID)
            DbCommandWrapper.AddInParameter("@IsActive", DbType.Boolean, SFSparepartHistory.IsActive)
            DbCommandWrapper.AddInParameter("@SFID", DbType.AnsiString, SFSparepartHistory.SFID)
            DbCommandWrapper.AddInParameter("@RetrySF", DbType.Int16, SFSparepartHistory.RetrySF)
            DbCommandWrapper.AddInParameter("@RetryMMID", DbType.Int16, SFSparepartHistory.RetryMMID)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, SFSparepartHistory.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, SFSparepartHistory.LastUpdateBy)
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

            Dim SFSparepartHistory As SFSparepartHistory = CType(obj, SFSparepartHistory)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, SFSparepartHistory.ID)
            DbCommandWrapper.AddInParameter("@KeyID", DbType.AnsiString, SFSparepartHistory.KeyID)
            'DbCommandWrapper.AddInParameter("@PMHeaderID", DbType.Int32, Me.GetRefObject(SFSparepartHistory.PMHeader))
            'DbCommandWrapper.AddInParameter("@AssistServiceIncomingID", DbType.Int32, Me.GetRefObject(SFSparepartHistory.AssistServiceIncoming))
            DbCommandWrapper.AddInParameter("@AssistPartSalesID", DbType.Int32, Me.GetRefObject(SFSparepartHistory.AssistPartSales))
            'DbCommandWrapper.AddInParameter("@EndCustomerID", DbType.Int32, Me.GetRefObject(SFSparepartHistory.EndCustomer))
            DbCommandWrapper.AddInParameter("@IsSynchronizeSF", DbType.Boolean, SFSparepartHistory.IsSynchronizeSF)
            DbCommandWrapper.AddInParameter("@SynchronizeDateSF", DbType.DateTime, SFSparepartHistory.SynchronizeDateSF)
            DbCommandWrapper.AddInParameter("@IsSynchronizeMMID", DbType.Boolean, SFSparepartHistory.IsSynchronizeMMID)
            DbCommandWrapper.AddInParameter("@SynchronizeDateMMID", DbType.DateTime, SFSparepartHistory.SynchronizeDateMMID)
            DbCommandWrapper.AddInParameter("@IsActive", DbType.Boolean, SFSparepartHistory.IsActive)
            DbCommandWrapper.AddInParameter("@SFID", DbType.AnsiString, SFSparepartHistory.SFID)
            DbCommandWrapper.AddInParameter("@RetrySF", DbType.Int16, SFSparepartHistory.RetrySF)
            DbCommandWrapper.AddInParameter("@RetryMMID", DbType.Int16, SFSparepartHistory.RetryMMID)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, SFSparepartHistory.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, SFSparepartHistory.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As SFSparepartHistory

            Dim SFSparepartHistory As SFSparepartHistory = New SFSparepartHistory

            SFSparepartHistory.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("KeyID")) Then SFSparepartHistory.KeyID = dr("KeyID").ToString
            'If Not dr.IsDBNull(dr.GetOrdinal("PMHeaderID")) Then SFSparepartHistory.PMHeader = New PMHeader(ID:=CType(dr("PMHeaderID"), Integer))
            If Not dr.IsDBNull(dr.GetOrdinal("AssistPartSalesID")) Then SFSparepartHistory.AssistPartSales = New AssistPartSales(ID:=CType(dr("AssistPartSalesID"), Integer))
            'If Not dr.IsDBNull(dr.GetOrdinal("EndCustomerID")) Then SFSparepartHistory.EndCustomer = New EndCustomer(ID:=CType(dr("EndCustomerID"), Integer))
            'If Not dr.IsDBNull(dr.GetOrdinal("AssistServiceIncomingID")) Then SFSparepartHistory.AssistServiceIncoming = New AssistServiceIncoming(ID:=CType(dr("AssistServiceIncomingID"), Integer))
            If Not dr.IsDBNull(dr.GetOrdinal("IsSynchronizeSF")) Then SFSparepartHistory.IsSynchronizeSF = CType(dr("IsSynchronizeSF"), Boolean)
            If Not dr.IsDBNull(dr.GetOrdinal("SynchronizeDateSF")) Then SFSparepartHistory.SynchronizeDateSF = CType(dr("SynchronizeDateSF"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("IsSynchronizeMMID")) Then SFSparepartHistory.IsSynchronizeMMID = CType(dr("IsSynchronizeMMID"), Boolean)
            If Not dr.IsDBNull(dr.GetOrdinal("SynchronizeDateMMID")) Then SFSparepartHistory.SynchronizeDateMMID = CType(dr("SynchronizeDateMMID"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("IsActive")) Then SFSparepartHistory.IsActive = CType(dr("IsActive"), Boolean)
            If Not dr.IsDBNull(dr.GetOrdinal("SFID")) Then SFSparepartHistory.SFID = dr("SFID").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RetrySF")) Then SFSparepartHistory.RetrySF = CType(dr("RetrySF"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("RetryMMID")) Then SFSparepartHistory.RetryMMID = CType(dr("RetryMMID"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then SFSparepartHistory.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then SFSparepartHistory.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then SFSparepartHistory.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then SFSparepartHistory.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then SFSparepartHistory.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            Return SFSparepartHistory

        End Function

        Private Sub SetTableName()

            If Not (GetType(SFSparepartHistory) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(SFSparepartHistory), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(SFSparepartHistory).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

