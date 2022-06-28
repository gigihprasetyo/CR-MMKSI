
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : BabitEventProposalDocument Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2019 
'// ---------------------
'// $History      : $
'// Generated on 15/05/2019 - 7:56:37
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

    Public Class BabitEventProposalDocumentMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertBabitEventProposalDocument"
        Private m_UpdateStatement As String = "up_UpdateBabitEventProposalDocument"
        Private m_RetrieveStatement As String = "up_RetrieveBabitEventProposalDocument"
        Private m_RetrieveListStatement As String = "up_RetrieveBabitEventProposalDocumentList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteBabitEventProposalDocument"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim babitEventProposalDocument As BabitEventProposalDocument = Nothing
            While dr.Read

                babitEventProposalDocument = Me.CreateObject(dr)

            End While

            Return babitEventProposalDocument

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim babitEventProposalDocumentList As ArrayList = New ArrayList

            While dr.Read
                Dim babitEventProposalDocument As BabitEventProposalDocument = Me.CreateObject(dr)
                babitEventProposalDocumentList.Add(babitEventProposalDocument)
            End While

            Return babitEventProposalDocumentList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim babitEventProposalDocument As BabitEventProposalDocument = CType(obj, BabitEventProposalDocument)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, babitEventProposalDocument.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim babitEventProposalDocument As BabitEventProposalDocument = CType(obj, BabitEventProposalDocument)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)

            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@FileName", DbType.AnsiString, babitEventProposalDocument.FileName)
            DbCommandWrapper.AddInParameter("@FileDescription", DbType.AnsiString, babitEventProposalDocument.FileDescription)
            DbCommandWrapper.AddInParameter("@Path", DbType.AnsiString, babitEventProposalDocument.Path)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, babitEventProposalDocument.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, babitEventProposalDocument.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@BabitEventProposalHeaderID", DbType.Int32, Me.GetRefObject(babitEventProposalDocument.BabitEventProposalHeader))
            DbCommandWrapper.AddInParameter("@EventDealerRequiredDocumentID", DbType.Int32, Me.GetRefObject(babitEventProposalDocument.EventDealerRequiredDocument))

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

            Dim babitEventProposalDocument As BabitEventProposalDocument = CType(obj, BabitEventProposalDocument)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, babitEventProposalDocument.ID)
            DbCommandWrapper.AddInParameter("@FileName", DbType.AnsiString, babitEventProposalDocument.FileName)
            DbCommandWrapper.AddInParameter("@FileDescription", DbType.AnsiString, babitEventProposalDocument.FileDescription)
            DbCommandWrapper.AddInParameter("@Path", DbType.AnsiString, babitEventProposalDocument.Path)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, babitEventProposalDocument.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, babitEventProposalDocument.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@BabitEventProposalHeaderID", DbType.Int32, Me.GetRefObject(babitEventProposalDocument.BabitEventProposalHeader))
            DbCommandWrapper.AddInParameter("@EventDealerRequiredDocumentID", DbType.Int32, Me.GetRefObject(babitEventProposalDocument.EventDealerRequiredDocument))

            Return DbCommandWrapper

        End Function

#End Region

#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As BabitEventProposalDocument

            Dim babitEventProposalDocument As BabitEventProposalDocument = New BabitEventProposalDocument

            babitEventProposalDocument.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("FileName")) Then babitEventProposalDocument.FileName = dr("FileName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("FileDescription")) Then babitEventProposalDocument.FileDescription = dr("FileDescription").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Path")) Then babitEventProposalDocument.Path = dr("Path").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then babitEventProposalDocument.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then babitEventProposalDocument.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then babitEventProposalDocument.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then babitEventProposalDocument.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then babitEventProposalDocument.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("BabitEventProposalHeaderID")) Then
                babitEventProposalDocument.BabitEventProposalHeader = New BabitEventProposalHeader(CType(dr("BabitEventProposalHeaderID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("EventDealerRequiredDocumentID")) Then
                babitEventProposalDocument.EventDealerRequiredDocument = New EventDealerRequiredDocument(CType(dr("EventDealerRequiredDocumentID"), Integer))
            End If

            Return babitEventProposalDocument

        End Function

        Private Sub SetTableName()

            If Not (GetType(BabitEventProposalDocument) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(BabitEventProposalDocument), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(BabitEventProposalDocument).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

