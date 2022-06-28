
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : V_LKPPbyDealer Objects Mapper.
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

    Public Class V_LKPPbyDealerMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertV_LKPPbyDealer"
        Private m_UpdateStatement As String = "up_UpdateV_LKPPbyDealer"
        Private m_RetrieveStatement As String = "up_RetrieveV_LKPPbyDealer"
        Private m_RetrieveListStatement As String = "up_RetrieveV_LKPPbyDealerList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteV_LKPPbyDealer"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim v_LKPPbyDealer As V_LKPPbyDealer = Nothing
            While dr.Read

                v_LKPPbyDealer = Me.CreateObject(dr)

            End While

            Return v_LKPPbyDealer

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim v_LKPPbyDealerList As ArrayList = New ArrayList

            While dr.Read
                Dim v_LKPPbyDealer As V_LKPPbyDealer = Me.CreateObject(dr)
                v_LKPPbyDealerList.Add(v_LKPPbyDealer)
            End While

            Return v_LKPPbyDealerList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim v_LKPPbyDealer As V_LKPPbyDealer = CType(obj, V_LKPPbyDealer)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, v_LKPPbyDealer.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim v_LKPPbyDealer As V_LKPPbyDealer = CType(obj, V_LKPPbyDealer)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, v_LKPPbyDealer.DealerID)
            DbCommandWrapper.AddInParameter("@LKPPHeaderID", DbType.Int32, v_LKPPbyDealer.LKPPHeaderID)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, v_LKPPbyDealer.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, v_LKPPbyDealer.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@ReferenceNumber", DbType.String, v_LKPPbyDealer.ReferenceNumber)
            DbCommandWrapper.AddInParameter("@LetterDate", DbType.DateTime, v_LKPPbyDealer.LetterDate)
            DbCommandWrapper.AddInParameter("@GovInstName", DbType.String, v_LKPPbyDealer.GovInstName)
            DbCommandWrapper.AddInParameter("@Description", DbType.String, v_LKPPbyDealer.Description)
            DbCommandWrapper.AddInParameter("@Attachment", DbType.String, v_LKPPbyDealer.Attachment)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, v_LKPPbyDealer.Status)
            DbCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, v_LKPPbyDealer.DealerCode)
            DbCommandWrapper.AddInParameter("@DealerName", DbType.AnsiString, v_LKPPbyDealer.DealerName)
            DbCommandWrapper.AddInParameter("@Title", DbType.AnsiString, v_LKPPbyDealer.Title)
            DbCommandWrapper.AddInParameter("@SearchTerm1", DbType.AnsiString, v_LKPPbyDealer.SearchTerm1)
            DbCommandWrapper.AddInParameter("@SearchTerm2", DbType.AnsiString, v_LKPPbyDealer.SearchTerm2)
            DbCommandWrapper.AddInParameter("@DealerGroupID", DbType.Int32, v_LKPPbyDealer.DealerGroupID)


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

            Dim v_LKPPbyDealer As V_LKPPbyDealer = CType(obj, V_LKPPbyDealer)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, v_LKPPbyDealer.ID)
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, v_LKPPbyDealer.DealerID)
            DbCommandWrapper.AddInParameter("@LKPPHeaderID", DbType.Int32, v_LKPPbyDealer.LKPPHeaderID)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, v_LKPPbyDealer.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, v_LKPPbyDealer.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@ReferenceNumber", DbType.String, v_LKPPbyDealer.ReferenceNumber)
            DbCommandWrapper.AddInParameter("@LetterDate", DbType.DateTime, v_LKPPbyDealer.LetterDate)
            DbCommandWrapper.AddInParameter("@GovInstName", DbType.String, v_LKPPbyDealer.GovInstName)
            DbCommandWrapper.AddInParameter("@Description", DbType.String, v_LKPPbyDealer.Description)
            DbCommandWrapper.AddInParameter("@Attachment", DbType.String, v_LKPPbyDealer.Attachment)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, v_LKPPbyDealer.Status)
            DbCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, v_LKPPbyDealer.DealerCode)
            DbCommandWrapper.AddInParameter("@DealerName", DbType.AnsiString, v_LKPPbyDealer.DealerName)
            DbCommandWrapper.AddInParameter("@Title", DbType.AnsiString, v_LKPPbyDealer.Title)
            DbCommandWrapper.AddInParameter("@SearchTerm1", DbType.AnsiString, v_LKPPbyDealer.SearchTerm1)
            DbCommandWrapper.AddInParameter("@SearchTerm2", DbType.AnsiString, v_LKPPbyDealer.SearchTerm2)
            DbCommandWrapper.AddInParameter("@DealerGroupID", DbType.Int32, v_LKPPbyDealer.DealerGroupID)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As V_LKPPbyDealer

            Dim v_LKPPbyDealer As V_LKPPbyDealer = New V_LKPPbyDealer

            v_LKPPbyDealer.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then v_LKPPbyDealer.DealerID = CType(dr("DealerID"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("LKPPHeaderID")) Then v_LKPPbyDealer.LKPPHeaderID = CType(dr("LKPPHeaderID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then v_LKPPbyDealer.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then v_LKPPbyDealer.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then v_LKPPbyDealer.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then v_LKPPbyDealer.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then v_LKPPbyDealer.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("ReferenceNumber")) Then v_LKPPbyDealer.ReferenceNumber = dr("ReferenceNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LetterDate")) Then v_LKPPbyDealer.LetterDate = CType(dr("LetterDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("GovInstName")) Then v_LKPPbyDealer.GovInstName = dr("GovInstName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Description")) Then v_LKPPbyDealer.Description = dr("Description").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Attachment")) Then v_LKPPbyDealer.Attachment = dr("Attachment").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then v_LKPPbyDealer.Status = CType(dr("Status"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerCode")) Then v_LKPPbyDealer.DealerCode = dr("DealerCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DealerName")) Then v_LKPPbyDealer.DealerName = dr("DealerName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Title")) Then v_LKPPbyDealer.Title = dr("Title").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SearchTerm1")) Then v_LKPPbyDealer.SearchTerm1 = dr("SearchTerm1").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SearchTerm2")) Then v_LKPPbyDealer.SearchTerm2 = dr("SearchTerm2").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DealerGroupID")) Then v_LKPPbyDealer.DealerGroupID = CType(dr("DealerGroupID"), Integer)

            Return v_LKPPbyDealer

        End Function

        Private Sub SetTableName()

            If Not (GetType(V_LKPPbyDealer) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(V_LKPPbyDealer), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(V_LKPPbyDealer).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

