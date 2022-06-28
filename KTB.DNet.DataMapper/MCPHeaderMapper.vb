
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : MCPHeader Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2015 
'// ---------------------
'// $History      : $
'// Generated on 6/22/2015 - 9:06:45 AM
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

    Public Class MCPHeaderMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertMCPHeader"
        Private m_UpdateStatement As String = "up_UpdateMCPHeader"
        Private m_RetrieveStatement As String = "up_RetrieveMCPHeader"
        Private m_RetrieveListStatement As String = "up_RetrieveMCPHeaderList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteMCPHeader"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim mCPHeader As MCPHeader = Nothing
            While dr.Read

                mCPHeader = Me.CreateObject(dr)

            End While

            Return mCPHeader

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim mCPHeaderList As ArrayList = New ArrayList

            While dr.Read
                Dim mCPHeader As MCPHeader = Me.CreateObject(dr)
                mCPHeaderList.Add(mCPHeader)
            End While

            Return mCPHeaderList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim mCPHeader As MCPHeader = CType(obj, MCPHeader)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, mCPHeader.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim mCPHeader As MCPHeader = CType(obj, MCPHeader)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@ReferenceNumber", DbType.String, mCPHeader.ReferenceNumber)
            DbCommandWrapper.AddInParameter("@LetterDate", DbType.DateTime, mCPHeader.LetterDate)
            DbCommandWrapper.AddInParameter("@GovInstName", DbType.String, mCPHeader.GovInstName)
            DbCommandWrapper.AddInParameter("@Description", DbType.String, mCPHeader.Description)
            DbCommandWrapper.AddInParameter("@Attachment", DbType.String, mCPHeader.Attachment)
            DbCommandWrapper.AddInParameter("@Classification", DbType.Int16, mCPHeader.Classification)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, mCPHeader.Status)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, mCPHeader.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, mCPHeader.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, Me.GetRefObject(mCPHeader.Dealer))

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

            Dim mCPHeader As MCPHeader = CType(obj, MCPHeader)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, mCPHeader.ID)
            DbCommandWrapper.AddInParameter("@ReferenceNumber", DbType.String, mCPHeader.ReferenceNumber)
            DbCommandWrapper.AddInParameter("@LetterDate", DbType.DateTime, mCPHeader.LetterDate)
            DbCommandWrapper.AddInParameter("@GovInstName", DbType.String, mCPHeader.GovInstName)
            DbCommandWrapper.AddInParameter("@Description", DbType.String, mCPHeader.Description)
            DbCommandWrapper.AddInParameter("@Attachment", DbType.String, mCPHeader.Attachment)
            DbCommandWrapper.AddInParameter("@Classification", DbType.Int16, mCPHeader.Classification)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, mCPHeader.Status)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, mCPHeader.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, mCPHeader.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, Me.GetRefObject(mCPHeader.Dealer))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As MCPHeader

            Dim mCPHeader As MCPHeader = New MCPHeader

            mCPHeader.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("ReferenceNumber")) Then mCPHeader.ReferenceNumber = dr("ReferenceNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LetterDate")) Then mCPHeader.LetterDate = CType(dr("LetterDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("GovInstName")) Then mCPHeader.GovInstName = dr("GovInstName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Description")) Then mCPHeader.Description = dr("Description").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Attachment")) Then mCPHeader.Attachment = dr("Attachment").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Classification")) Then mCPHeader.Classification = CType(dr("Classification"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then mCPHeader.Status = CType(dr("Status"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then mCPHeader.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then mCPHeader.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then mCPHeader.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then mCPHeader.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then mCPHeader.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then
                mCPHeader.Dealer = New Dealer(CType(dr("DealerID"), Short))
            End If

            Return mCPHeader

        End Function

        Private Sub SetTableName()

            If Not (GetType(MCPHeader) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(MCPHeader), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(MCPHeader).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

