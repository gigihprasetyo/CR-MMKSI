
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : RevisionSAPDoc Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 9/5/2018 - 9:57:05 AM
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

    Public Class RevisionSAPDocMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertRevisionSAPDoc"
        Private m_UpdateStatement As String = "up_UpdateRevisionSAPDoc"
        Private m_RetrieveStatement As String = "up_RetrieveRevisionSAPDoc"
        Private m_RetrieveListStatement As String = "up_RetrieveRevisionSAPDocList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteRevisionSAPDoc"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim revisionSAPDoc As RevisionSAPDoc = Nothing
            While dr.Read

                revisionSAPDoc = Me.CreateObject(dr)

            End While

            Return revisionSAPDoc

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim revisionSAPDocList As ArrayList = New ArrayList

            While dr.Read
                Dim revisionSAPDoc As RevisionSAPDoc = Me.CreateObject(dr)
                revisionSAPDocList.Add(revisionSAPDoc)
            End While

            Return revisionSAPDocList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim revisionSAPDoc As RevisionSAPDoc = CType(obj, RevisionSAPDoc)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, revisionSAPDoc.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim revisionSAPDoc As RevisionSAPDoc = CType(obj, RevisionSAPDoc)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@DebitChargeNo", DbType.AnsiString, revisionSAPDoc.DebitChargeNo)
            DbCommandWrapper.AddInParameter("@DCAmount", DbType.Currency, revisionSAPDoc.DCAmount)
            DbCommandWrapper.AddInParameter("@DebitMemoNo", DbType.AnsiString, revisionSAPDoc.DebitMemoNo)
            DbCommandWrapper.AddInParameter("@DMAmount", DbType.Currency, revisionSAPDoc.DMAmount)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, revisionSAPDoc.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, revisionSAPDoc.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@RevisionFakturID", DbType.Int32, Me.GetRefObject(revisionSAPDoc.RevisionFaktur))

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

            Dim revisionSAPDoc As RevisionSAPDoc = CType(obj, RevisionSAPDoc)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, revisionSAPDoc.ID)
            DbCommandWrapper.AddInParameter("@DebitChargeNo", DbType.AnsiString, revisionSAPDoc.DebitChargeNo)
            DbCommandWrapper.AddInParameter("@DCAmount", DbType.Currency, revisionSAPDoc.DCAmount)
            DbCommandWrapper.AddInParameter("@DebitMemoNo", DbType.AnsiString, revisionSAPDoc.DebitMemoNo)
            DbCommandWrapper.AddInParameter("@DMAmount", DbType.Currency, revisionSAPDoc.DMAmount)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, revisionSAPDoc.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, revisionSAPDoc.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@RevisionFakturID", DbType.Int32, Me.GetRefObject(revisionSAPDoc.RevisionFaktur))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As RevisionSAPDoc

            Dim revisionSAPDoc As RevisionSAPDoc = New RevisionSAPDoc

            revisionSAPDoc.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("DebitChargeNo")) Then revisionSAPDoc.DebitChargeNo = dr("DebitChargeNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DCAmount")) Then revisionSAPDoc.DCAmount = CType(dr("DCAmount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("DebitMemoNo")) Then revisionSAPDoc.DebitMemoNo = dr("DebitMemoNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DMAmount")) Then revisionSAPDoc.DMAmount = CType(dr("DMAmount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then revisionSAPDoc.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then revisionSAPDoc.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then revisionSAPDoc.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then revisionSAPDoc.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then revisionSAPDoc.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            If Not dr.IsDBNull(dr.GetOrdinal("RevisionFakturID")) Then
                revisionSAPDoc.RevisionFaktur = New RevisionFaktur(CType(dr("RevisionFakturID"), Integer))
            End If

            Return revisionSAPDoc

        End Function

        Private Sub SetTableName()

            If Not (GetType(RevisionSAPDoc) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(RevisionSAPDoc), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(RevisionSAPDoc).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

