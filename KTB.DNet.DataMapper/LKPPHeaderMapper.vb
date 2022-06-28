
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : LKPPHeader Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2015 
'// ---------------------
'// $History      : $
'// Generated on 12/8/2015 - 10:18:17 AM
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

    Public Class LKPPHeaderMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertLKPPHeader"
        Private m_UpdateStatement As String = "up_UpdateLKPPHeader"
        Private m_RetrieveStatement As String = "up_RetrieveLKPPHeader"
        Private m_RetrieveListStatement As String = "up_RetrieveLKPPHeaderList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteLKPPHeader"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim lKPPHeader As LKPPHeader = Nothing
            While dr.Read

                lKPPHeader = Me.CreateObject(dr)

            End While

            Return lKPPHeader

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim lKPPHeaderList As ArrayList = New ArrayList

            While dr.Read
                Dim lKPPHeader As LKPPHeader = Me.CreateObject(dr)
                lKPPHeaderList.Add(lKPPHeader)
            End While

            Return lKPPHeaderList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim lKPPHeader As LKPPHeader = CType(obj, LKPPHeader)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, lKPPHeader.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim lKPPHeader As LKPPHeader = CType(obj, LKPPHeader)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@ReferenceNumber", DbType.String, lKPPHeader.ReferenceNumber)
            DbCommandWrapper.AddInParameter("@LetterDate", DbType.DateTime, lKPPHeader.LetterDate)
            DbCommandWrapper.AddInParameter("@GovInstName", DbType.String, lKPPHeader.GovInstName)
            DbCommandWrapper.AddInParameter("@Description", DbType.String, lKPPHeader.Description)
            DbCommandWrapper.AddInParameter("@Attachment", DbType.String, lKPPHeader.Attachment)
            DbCommandWrapper.AddInParameter("@Classification", DbType.Int16, lKPPHeader.Classification)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, lKPPHeader.Status)
            DbCommandWrapper.AddInParameter("@Notes", DbType.String, lKPPHeader.Notes)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, lKPPHeader.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, lKPPHeader.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, Me.GetRefObject(lKPPHeader.Dealer))

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

            Dim lKPPHeader As LKPPHeader = CType(obj, LKPPHeader)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, lKPPHeader.ID)
            DbCommandWrapper.AddInParameter("@ReferenceNumber", DbType.String, lKPPHeader.ReferenceNumber)
            DbCommandWrapper.AddInParameter("@LetterDate", DbType.DateTime, lKPPHeader.LetterDate)
            DbCommandWrapper.AddInParameter("@GovInstName", DbType.String, lKPPHeader.GovInstName)
            DbCommandWrapper.AddInParameter("@Description", DbType.String, lKPPHeader.Description)
            DbCommandWrapper.AddInParameter("@Attachment", DbType.String, lKPPHeader.Attachment)
            DbCommandWrapper.AddInParameter("@Classification", DbType.Int16, lKPPHeader.Classification)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, lKPPHeader.Status)
            DbCommandWrapper.AddInParameter("@Notes", DbType.String, lKPPHeader.Notes)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, lKPPHeader.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, lKPPHeader.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, Me.GetRefObject(lKPPHeader.Dealer))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As LKPPHeader

            Dim lKPPHeader As LKPPHeader = New LKPPHeader

            lKPPHeader.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("ReferenceNumber")) Then lKPPHeader.ReferenceNumber = dr("ReferenceNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LetterDate")) Then lKPPHeader.LetterDate = CType(dr("LetterDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("GovInstName")) Then lKPPHeader.GovInstName = dr("GovInstName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Description")) Then lKPPHeader.Description = dr("Description").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Attachment")) Then lKPPHeader.Attachment = dr("Attachment").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Classification")) Then lKPPHeader.Classification = CType(dr("Classification"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then lKPPHeader.Status = CType(dr("Status"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("Notes")) Then lKPPHeader.Notes = dr("Notes").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then lKPPHeader.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then lKPPHeader.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then lKPPHeader.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then lKPPHeader.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then lKPPHeader.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then
                lKPPHeader.Dealer = New Dealer(CType(dr("DealerID"), Short))
            End If

            Return lKPPHeader

        End Function

        Private Sub SetTableName()

            If Not (GetType(LKPPHeader) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(LKPPHeader), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(LKPPHeader).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

