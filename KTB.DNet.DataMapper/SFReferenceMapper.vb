
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : SFReference Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 19/12/2018 - 10:37:26
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

    Public Class SFReferenceMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertSFReference"
        Private m_UpdateStatement As String = "up_UpdateSFReference"
        Private m_RetrieveStatement As String = "up_RetrieveSFReference"
        Private m_RetrieveListStatement As String = "up_RetrieveSFReferenceList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteSFReference"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim sFReference As SFReference = Nothing
            While dr.Read

                sFReference = Me.CreateObject(dr)

            End While

            Return sFReference

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim sFReferenceList As ArrayList = New ArrayList

            While dr.Read
                Dim sFReference As SFReference = Me.CreateObject(dr)
                sFReferenceList.Add(sFReference)
            End While

            Return sFReferenceList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim sFReference As SFReference = CType(obj, SFReference)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int64, sFReference.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim sFReference As SFReference = CType(obj, SFReference)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int64, 8)
            DbCommandWrapper.AddInParameter("@RefID", DbType.Int64, sFReference.RefID)
            DbCommandWrapper.AddInParameter("@IsSend", DbType.Boolean, sFReference.IsSend)
            DbCommandWrapper.AddInParameter("@RefTable", DbType.AnsiString, sFReference.RefTable)
            DbCommandWrapper.AddInParameter("@SalesForceID", DbType.AnsiString, sFReference.SalesForceID)
            DbCommandWrapper.AddInParameter("@FreeField2", DbType.AnsiString, sFReference.FreeField2)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, sFReference.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, sFReference.LastUpdateBy)
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

            Dim sFReference As SFReference = CType(obj, SFReference)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int64, sFReference.ID)
            DbCommandWrapper.AddInParameter("@RefID", DbType.Int64, sFReference.RefID)
            DbCommandWrapper.AddInParameter("@IsSend", DbType.Boolean, sFReference.IsSend)
            DbCommandWrapper.AddInParameter("@RefTable", DbType.AnsiString, sFReference.RefTable)
            DbCommandWrapper.AddInParameter("@SalesForceID", DbType.AnsiString, sFReference.SalesForceID)
            DbCommandWrapper.AddInParameter("@FreeField2", DbType.AnsiString, sFReference.FreeField2)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, sFReference.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, sFReference.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As SFReference

            Dim sFReference As SFReference = New SFReference

            sFReference.ID = CType(dr("ID"), Long)
            If Not dr.IsDBNull(dr.GetOrdinal("RefID")) Then sFReference.RefID = CType(dr("RefID"), Long)
            If Not dr.IsDBNull(dr.GetOrdinal("RefTable")) Then sFReference.RefTable = dr("RefTable").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("IsSend")) Then sFReference.IsSend = CType(dr("IsSend"), Boolean)
            If Not dr.IsDBNull(dr.GetOrdinal("SalesForceID")) Then sFReference.SalesForceID = dr("SalesForceID").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("FreeField2")) Then sFReference.FreeField2 = dr("FreeField2").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then sFReference.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then sFReference.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then sFReference.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then sFReference.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then sFReference.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            Return sFReference

        End Function

        Private Sub SetTableName()

            If Not (GetType(SFReference) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(SFReference), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(SFReference).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

