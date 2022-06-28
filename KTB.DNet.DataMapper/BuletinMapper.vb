#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : Buletin Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2007 
'// ---------------------
'// $History      : $
'// Generated on 8/1/2007 - 10:55:46 AM
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

    Public Class BuletinMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertBuletin"
        Private m_UpdateStatement As String = "up_UpdateBuletin"
        Private m_RetrieveStatement As String = "up_RetrieveBuletin"
        Private m_RetrieveListStatement As String = "up_RetrieveBuletinList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteBuletin"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim buletin As Buletin = Nothing
            While dr.Read

                buletin = Me.CreateObject(dr)

            End While

            Return buletin

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim buletinList As ArrayList = New ArrayList

            While dr.Read
                Dim buletin As Buletin = Me.CreateObject(dr)
                buletinList.Add(buletin)
            End While

            Return buletinList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim buletin As Buletin = CType(obj, Buletin)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, buletin.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim buletin As Buletin = CType(obj, Buletin)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@Title", DbType.AnsiString, buletin.Title)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, buletin.Description)
            DbCommandWrapper.AddInParameter("@ValidFrom", DbType.DateTime, buletin.ValidFrom)
            DbCommandWrapper.AddInParameter("@ValidTo", DbType.DateTime, buletin.ValidTo)
            DbCommandWrapper.AddInParameter("@Keywords", DbType.AnsiString, buletin.Keywords)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int32, buletin.Status)
            DbCommandWrapper.AddInParameter("@UploadBy", DbType.AnsiString, buletin.UploadBy)
            DbCommandWrapper.AddInParameter("@UploadDate", DbType.DateTime, buletin.UploadDate)
            DbCommandWrapper.AddInParameter("@PeriodMonth", DbType.Byte, buletin.PeriodMonth)
            DbCommandWrapper.AddInParameter("@PeriodYear", DbType.Int32, buletin.PeriodYear)
            DbCommandWrapper.AddInParameter("@FileName", DbType.AnsiString, buletin.FileName)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, buletin.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, buletin.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@CategoryID", DbType.Int32, Me.GetRefObject(buletin.BuletinCategory))

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

            Dim buletin As Buletin = CType(obj, Buletin)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, buletin.ID)
            DbCommandWrapper.AddInParameter("@Title", DbType.AnsiString, buletin.Title)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, buletin.Description)
            DbCommandWrapper.AddInParameter("@ValidFrom", DbType.DateTime, buletin.ValidFrom)
            DbCommandWrapper.AddInParameter("@ValidTo", DbType.DateTime, buletin.ValidTo)
            DbCommandWrapper.AddInParameter("@Keywords", DbType.AnsiString, buletin.Keywords)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int32, buletin.Status)
            DbCommandWrapper.AddInParameter("@UploadBy", DbType.AnsiString, buletin.UploadBy)
            DbCommandWrapper.AddInParameter("@UploadDate", DbType.DateTime, buletin.UploadDate)
            DbCommandWrapper.AddInParameter("@PeriodMonth", DbType.Byte, buletin.PeriodMonth)
            DbCommandWrapper.AddInParameter("@PeriodYear", DbType.Int32, buletin.PeriodYear)
            DbCommandWrapper.AddInParameter("@FileName", DbType.AnsiString, buletin.FileName)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, buletin.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, buletin.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@CategoryID", DbType.Int32, Me.GetRefObject(buletin.BuletinCategory))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As Buletin

            Dim buletin As Buletin = New Buletin

            buletin.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Title")) Then buletin.Title = dr("Title").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Description")) Then buletin.Description = dr("Description").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ValidFrom")) Then buletin.ValidFrom = CType(dr("ValidFrom"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("ValidTo")) Then buletin.ValidTo = CType(dr("ValidTo"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("Keywords")) Then buletin.Keywords = dr("Keywords").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then buletin.Status = CType(dr("Status"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("UploadBy")) Then buletin.UploadBy = dr("UploadBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("UploadDate")) Then buletin.UploadDate = CType(dr("UploadDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("PeriodMonth")) Then buletin.PeriodMonth = CType(dr("PeriodMonth"), Byte)
            If Not dr.IsDBNull(dr.GetOrdinal("PeriodYear")) Then buletin.PeriodYear = CType(dr("PeriodYear"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("FileName")) Then buletin.FileName = dr("FileName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then buletin.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then buletin.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then buletin.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then buletin.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then buletin.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("CategoryID")) Then
                buletin.BuletinCategory = New BuletinCategory(CType(dr("CategoryID"), Integer))
            End If

            Return buletin

        End Function

        Private Sub SetTableName()

            If Not (GetType(Buletin) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(Buletin), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(Buletin).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

