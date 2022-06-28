
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : V_MCPbyDealer Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2015 
'// ---------------------
'// $History      : $
'// Generated on 6/23/2015 - 9:32:20 AM
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

    Public Class V_MCPbyDealerMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertV_MCPbyDealer"
        Private m_UpdateStatement As String = "up_UpdateV_MCPbyDealer"
        Private m_RetrieveStatement As String = "up_RetrieveV_MCPbyDealer"
        Private m_RetrieveListStatement As String = "up_RetrieveV_MCPbyDealerList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteV_MCPbyDealer"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim v_MCPbyDealer As V_MCPbyDealer = Nothing
            While dr.Read

                v_MCPbyDealer = Me.CreateObject(dr)

            End While

            Return v_MCPbyDealer

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim v_MCPbyDealerList As ArrayList = New ArrayList

            While dr.Read
                Dim v_MCPbyDealer As V_MCPbyDealer = Me.CreateObject(dr)
                v_MCPbyDealerList.Add(v_MCPbyDealer)
            End While

            Return v_MCPbyDealerList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim v_MCPbyDealer As V_MCPbyDealer = CType(obj, V_MCPbyDealer)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, v_MCPbyDealer.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim v_MCPbyDealer As V_MCPbyDealer = CType(obj, V_MCPbyDealer)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, v_MCPbyDealer.DealerID)
            DbCommandWrapper.AddInParameter("@MCPHeaderID", DbType.Int32, v_MCPbyDealer.MCPHeaderID)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, v_MCPbyDealer.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, v_MCPbyDealer.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@ReferenceNumber", DbType.String, v_MCPbyDealer.ReferenceNumber)
            DbCommandWrapper.AddInParameter("@LetterDate", DbType.DateTime, v_MCPbyDealer.LetterDate)
            DbCommandWrapper.AddInParameter("@GovInstName", DbType.String, v_MCPbyDealer.GovInstName)
            DbCommandWrapper.AddInParameter("@Description", DbType.String, v_MCPbyDealer.Description)
            DbCommandWrapper.AddInParameter("@Attachment", DbType.String, v_MCPbyDealer.Attachment)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, v_MCPbyDealer.Status)
            DbCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, v_MCPbyDealer.DealerCode)
            DbCommandWrapper.AddInParameter("@DealerName", DbType.AnsiString, v_MCPbyDealer.DealerName)
            DbCommandWrapper.AddInParameter("@Title", DbType.AnsiString, v_MCPbyDealer.Title)
            DbCommandWrapper.AddInParameter("@SearchTerm1", DbType.AnsiString, v_MCPbyDealer.SearchTerm1)
            DbCommandWrapper.AddInParameter("@SearchTerm2", DbType.AnsiString, v_MCPbyDealer.SearchTerm2)
            DbCommandWrapper.AddInParameter("@DealerGroupID", DbType.Int32, v_MCPbyDealer.DealerGroupID)


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

            Dim v_MCPbyDealer As V_MCPbyDealer = CType(obj, V_MCPbyDealer)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, v_MCPbyDealer.ID)
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, v_MCPbyDealer.DealerID)
            DbCommandWrapper.AddInParameter("@MCPHeaderID", DbType.Int32, v_MCPbyDealer.MCPHeaderID)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, v_MCPbyDealer.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, v_MCPbyDealer.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@ReferenceNumber", DbType.String, v_MCPbyDealer.ReferenceNumber)
            DbCommandWrapper.AddInParameter("@LetterDate", DbType.DateTime, v_MCPbyDealer.LetterDate)
            DbCommandWrapper.AddInParameter("@GovInstName", DbType.String, v_MCPbyDealer.GovInstName)
            DbCommandWrapper.AddInParameter("@Description", DbType.String, v_MCPbyDealer.Description)
            DbCommandWrapper.AddInParameter("@Attachment", DbType.String, v_MCPbyDealer.Attachment)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, v_MCPbyDealer.Status)
            DbCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, v_MCPbyDealer.DealerCode)
            DbCommandWrapper.AddInParameter("@DealerName", DbType.AnsiString, v_MCPbyDealer.DealerName)
            DbCommandWrapper.AddInParameter("@Title", DbType.AnsiString, v_MCPbyDealer.Title)
            DbCommandWrapper.AddInParameter("@SearchTerm1", DbType.AnsiString, v_MCPbyDealer.SearchTerm1)
            DbCommandWrapper.AddInParameter("@SearchTerm2", DbType.AnsiString, v_MCPbyDealer.SearchTerm2)
            DbCommandWrapper.AddInParameter("@DealerGroupID", DbType.Int32, v_MCPbyDealer.DealerGroupID)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As V_MCPbyDealer

            Dim v_MCPbyDealer As V_MCPbyDealer = New V_MCPbyDealer

            v_MCPbyDealer.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then v_MCPbyDealer.DealerID = CType(dr("DealerID"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("MCPHeaderID")) Then v_MCPbyDealer.MCPHeaderID = CType(dr("MCPHeaderID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then v_MCPbyDealer.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then v_MCPbyDealer.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then v_MCPbyDealer.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then v_MCPbyDealer.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then v_MCPbyDealer.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("ReferenceNumber")) Then v_MCPbyDealer.ReferenceNumber = dr("ReferenceNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LetterDate")) Then v_MCPbyDealer.LetterDate = CType(dr("LetterDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("GovInstName")) Then v_MCPbyDealer.GovInstName = dr("GovInstName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Description")) Then v_MCPbyDealer.Description = dr("Description").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Attachment")) Then v_MCPbyDealer.Attachment = dr("Attachment").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then v_MCPbyDealer.Status = CType(dr("Status"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerCode")) Then v_MCPbyDealer.DealerCode = dr("DealerCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DealerName")) Then v_MCPbyDealer.DealerName = dr("DealerName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Title")) Then v_MCPbyDealer.Title = dr("Title").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SearchTerm1")) Then v_MCPbyDealer.SearchTerm1 = dr("SearchTerm1").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SearchTerm2")) Then v_MCPbyDealer.SearchTerm2 = dr("SearchTerm2").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DealerGroupID")) Then v_MCPbyDealer.DealerGroupID = CType(dr("DealerGroupID"), Integer)

            Return v_MCPbyDealer

        End Function

        Private Sub SetTableName()

            If Not (GetType(V_MCPbyDealer) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(V_MCPbyDealer), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(V_MCPbyDealer).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

