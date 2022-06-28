
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : Presentation Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2016 
'// ---------------------
'// $History      : $
'// Generated on 25/02/2016 - 11:12:49
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

    Public Class PresentationMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertPresentation"
        Private m_UpdateStatement As String = "up_UpdatePresentation"
        Private m_RetrieveStatement As String = "up_RetrievePresentation"
        Private m_RetrieveListStatement As String = "up_RetrievePresentationList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeletePresentation"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim presentation As Presentation = Nothing
            While dr.Read

                presentation = Me.CreateObject(dr)

            End While

            Return presentation

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim presentationList As ArrayList = New ArrayList

            While dr.Read
                Dim presentation As Presentation = Me.CreateObject(dr)
                presentationList.Add(presentation)
            End While

            Return presentationList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim presentation As Presentation = CType(obj, Presentation)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, presentation.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim presentation As Presentation = CType(obj, Presentation)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@Title", DbType.AnsiString, presentation.Title)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, presentation.Description)
            DbCommandWrapper.AddInParameter("@ValidFrom", DbType.DateTime, presentation.ValidFrom)
            DbCommandWrapper.AddInParameter("@ValidTo", DbType.DateTime, presentation.ValidTo)
            DbCommandWrapper.AddInParameter("@UniqueName", DbType.AnsiString, presentation.UniqueName)
            DbCommandWrapper.AddInParameter("@FileName", DbType.AnsiString, presentation.FileName)
            DbCommandWrapper.AddInParameter("@LogoName", DbType.AnsiString, presentation.LogoName)
            DbCommandWrapper.AddInParameter("@Status", DbType.Byte, presentation.Status)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, presentation.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, presentation.LastUpdateBy)
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

            Dim presentation As Presentation = CType(obj, Presentation)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, presentation.ID)
            DbCommandWrapper.AddInParameter("@Title", DbType.AnsiString, presentation.Title)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, presentation.Description)
            DbCommandWrapper.AddInParameter("@ValidFrom", DbType.DateTime, presentation.ValidFrom)
            DbCommandWrapper.AddInParameter("@ValidTo", DbType.DateTime, presentation.ValidTo)
            DbCommandWrapper.AddInParameter("@UniqueName", DbType.AnsiString, presentation.UniqueName)
            DbCommandWrapper.AddInParameter("@FileName", DbType.AnsiString, presentation.FileName)
            DbCommandWrapper.AddInParameter("@LogoName", DbType.AnsiString, presentation.LogoName)
            DbCommandWrapper.AddInParameter("@Status", DbType.Byte, presentation.Status)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, presentation.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, presentation.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As Presentation

            Dim presentation As Presentation = New Presentation

            presentation.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Title")) Then presentation.Title = dr("Title").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Description")) Then presentation.Description = dr("Description").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ValidFrom")) Then presentation.ValidFrom = CType(dr("ValidFrom"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("ValidTo")) Then presentation.ValidTo = CType(dr("ValidTo"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("UniqueName")) Then presentation.UniqueName = dr("UniqueName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("FileName")) Then presentation.FileName = dr("FileName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LogoName")) Then presentation.LogoName = dr("LogoName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then presentation.Status = CBool(dr("Status"))
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then presentation.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then presentation.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then presentation.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then presentation.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then presentation.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            Return presentation

        End Function

        Private Sub SetTableName()

            If Not (GetType(Presentation) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(Presentation), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(Presentation).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

