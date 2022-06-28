#Region "Summary"
'// ===========================================================================
'// AUTHOR        : Dnet
'// PURPOSE       : SFServiceHistoryBooklet Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2021 
'// ---------------------
'// $History      : $
'// Generated on 10/15/2021 - 9:42:03 AM
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

    Public Class SFServiceHistoryBookletMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertSFServiceHistoryBooklet"
        Private m_UpdateStatement As String = "up_UpdateSFServiceHistoryBooklet"
        Private m_RetrieveStatement As String = "up_RetrieveSFServiceHistoryBooklet"
        Private m_RetrieveListStatement As String = "up_RetrieveSFServiceHistoryBookletList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteSFServiceHistoryBooklet"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim sFServiceHistoryBooklet As SFServiceHistoryBooklet = Nothing
            While dr.Read

                sFServiceHistoryBooklet = Me.CreateObject(dr)

            End While

            Return sFServiceHistoryBooklet

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim sFServiceHistoryBookletList As ArrayList = New ArrayList

            While dr.Read
                Dim sFServiceHistoryBooklet As SFServiceHistoryBooklet = Me.CreateObject(dr)
                sFServiceHistoryBookletList.Add(sFServiceHistoryBooklet)
            End While

            Return sFServiceHistoryBookletList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim sFServiceHistoryBooklet As SFServiceHistoryBooklet = CType(obj, SFServiceHistoryBooklet)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, sFServiceHistoryBooklet.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim sFServiceHistoryBooklet As SFServiceHistoryBooklet = CType(obj, SFServiceHistoryBooklet)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@KeyID", DbType.AnsiString, sFServiceHistoryBooklet.KeyID)
            DbCommandWrapper.AddInParameter("@EndCustomerID", DbType.Int32, sFServiceHistoryBooklet.EndCustomerID)
            DbCommandWrapper.AddInParameter("@PMHeaderID", DbType.Int32, sFServiceHistoryBooklet.PMHeaderID)
            DbCommandWrapper.AddInParameter("@AssistServiceIncomingID", DbType.Int32, sFServiceHistoryBooklet.AssistServiceIncomingID)
            DbCommandWrapper.AddInParameter("@IsSynchronizeSF", DbType.Boolean, sFServiceHistoryBooklet.IsSynchronizeSF)
            DbCommandWrapper.AddInParameter("@SynchronizeDateSF", DbType.DateTime, sFServiceHistoryBooklet.SynchronizeDateSF)
            DbCommandWrapper.AddInParameter("@IsSynchronizeMMID", DbType.Boolean, sFServiceHistoryBooklet.IsSynchronizeMMID)
            DbCommandWrapper.AddInParameter("@SynchronizeDateMMID", DbType.DateTime, sFServiceHistoryBooklet.SynchronizeDateMMID)
            DbCommandWrapper.AddInParameter("@IsActive", DbType.Boolean, sFServiceHistoryBooklet.IsActive)
            DbCommandWrapper.AddInParameter("@SFID", DbType.AnsiString, sFServiceHistoryBooklet.SFID)
            DbCommandWrapper.AddInParameter("@RetrySF", DbType.Int16, sFServiceHistoryBooklet.RowStatus)
            DbCommandWrapper.AddInParameter("@RetryMMID", DbType.Int16, sFServiceHistoryBooklet.RowStatus)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, sFServiceHistoryBooklet.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, sFServiceHistoryBooklet.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime", DbType.DateTime, sFServiceHistoryBooklet.LastUpdateTime)


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

            Dim sFServiceHistoryBooklet As SFServiceHistoryBooklet = CType(obj, SFServiceHistoryBooklet)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, sFServiceHistoryBooklet.ID)
            DbCommandWrapper.AddInParameter("@KeyID", DbType.AnsiString, sFServiceHistoryBooklet.KeyID)
            DbCommandWrapper.AddInParameter("@EndCustomerID", DbType.Int32, sFServiceHistoryBooklet.EndCustomerID)
            DbCommandWrapper.AddInParameter("@PMHeaderID", DbType.Int32, sFServiceHistoryBooklet.PMHeaderID)
            DbCommandWrapper.AddInParameter("@AssistServiceIncomingID", DbType.Int32, sFServiceHistoryBooklet.AssistServiceIncomingID)
            DbCommandWrapper.AddInParameter("@IsSynchronizeSF", DbType.Boolean, sFServiceHistoryBooklet.IsSynchronizeSF)
            DbCommandWrapper.AddInParameter("@SynchronizeDateSF", DbType.DateTime, sFServiceHistoryBooklet.SynchronizeDateSF)
            DbCommandWrapper.AddInParameter("@IsSynchronizeMMID", DbType.Boolean, sFServiceHistoryBooklet.IsSynchronizeMMID)
            DbCommandWrapper.AddInParameter("@SynchronizeDateMMID", DbType.DateTime, sFServiceHistoryBooklet.SynchronizeDateMMID)
            DbCommandWrapper.AddInParameter("@IsActive", DbType.Boolean, sFServiceHistoryBooklet.IsActive)
            DbCommandWrapper.AddInParameter("@SFID", DbType.AnsiString, sFServiceHistoryBooklet.SFID)
            DbCommandWrapper.AddInParameter("@RetrySF", DbType.Int16, sFServiceHistoryBooklet.RetrySF)
            DbCommandWrapper.AddInParameter("@RetryMMID", DbType.Int16, sFServiceHistoryBooklet.RetryMMID)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, sFServiceHistoryBooklet.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, sFServiceHistoryBooklet.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime", DbType.DateTime, sFServiceHistoryBooklet.LastUpdateTime)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As SFServiceHistoryBooklet

            Dim sFServiceHistoryBooklet As SFServiceHistoryBooklet = New SFServiceHistoryBooklet

            sFServiceHistoryBooklet.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("KeyID")) Then sFServiceHistoryBooklet.KeyID = dr("KeyID").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("EndCustomerID")) Then sFServiceHistoryBooklet.EndCustomerID = CType(dr("EndCustomerID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("PMHeaderID")) Then sFServiceHistoryBooklet.PMHeaderID = CType(dr("PMHeaderID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("AssistServiceIncomingID")) Then sFServiceHistoryBooklet.AssistServiceIncomingID = CType(dr("AssistServiceIncomingID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("IsSynchronizeSF")) Then sFServiceHistoryBooklet.IsSynchronizeSF = CType(dr("IsSynchronizeSF"), Boolean)
            If Not dr.IsDBNull(dr.GetOrdinal("SynchronizeDateSF")) Then sFServiceHistoryBooklet.SynchronizeDateSF = CType(dr("SynchronizeDateSF"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("IsSynchronizeMMID")) Then sFServiceHistoryBooklet.IsSynchronizeMMID = CType(dr("IsSynchronizeMMID"), Boolean)
            If Not dr.IsDBNull(dr.GetOrdinal("SynchronizeDateMMID")) Then sFServiceHistoryBooklet.SynchronizeDateMMID = CType(dr("SynchronizeDateMMID"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("IsActive")) Then sFServiceHistoryBooklet.IsActive = CType(dr("IsActive"), Boolean)
            If Not dr.IsDBNull(dr.GetOrdinal("SFID")) Then sFServiceHistoryBooklet.SFID = dr("SFID").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RetrySF")) Then sFServiceHistoryBooklet.RetrySF = CType(dr("RetrySF"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("RetryMMID")) Then sFServiceHistoryBooklet.RetryMMID = CType(dr("RetryMMID"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then sFServiceHistoryBooklet.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then sFServiceHistoryBooklet.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then sFServiceHistoryBooklet.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then sFServiceHistoryBooklet.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then sFServiceHistoryBooklet.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            Return sFServiceHistoryBooklet

        End Function

        Private Sub SetTableName()

            If Not (GetType(SFServiceHistoryBooklet) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(SFServiceHistoryBooklet), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(SFServiceHistoryBooklet).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace
