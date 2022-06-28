#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : PCPhisingGuard Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2007 
'// ---------------------
'// $History      : $
'// Generated on 1/16/2007 - 12:03:16 PM
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

    Public Class PCPhisingGuardMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertPCPhisingGuard"
        Private m_UpdateStatement As String = "up_UpdatePCPhisingGuard"
        Private m_RetrieveStatement As String = "up_RetrievePCPhisingGuard"
        Private m_RetrieveListStatement As String = "up_RetrievePCPhisingGuardList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeletePCPhisingGuard"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim pCPhisingGuard As PCPhisingGuard = Nothing
            While dr.Read

                pCPhisingGuard = Me.CreateObject(dr)

            End While

            Return pCPhisingGuard

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim pCPhisingGuardList As ArrayList = New ArrayList

            While dr.Read
                Dim pCPhisingGuard As PCPhisingGuard = Me.CreateObject(dr)
                pCPhisingGuardList.Add(pCPhisingGuard)
            End While

            Return pCPhisingGuardList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim pCPhisingGuard As PCPhisingGuard = CType(obj, PCPhisingGuard)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, pCPhisingGuard.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim pCPhisingGuard As PCPhisingGuard = CType(obj, PCPhisingGuard)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@CookiesName", DbType.AnsiString, pCPhisingGuard.CookiesName)
            DbCommandWrapper.AddInParameter("@CookiesValue", DbType.AnsiString, pCPhisingGuard.CookiesValue)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, pCPhisingGuard.Description)
            DbCommandWrapper.AddInParameter("@Image", DbType.Binary, pCPhisingGuard.Image)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int32, pCPhisingGuard.Status)
            DbCommandWrapper.AddInParameter("@EncKey", DbType.AnsiString, pCPhisingGuard.EncKey)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, pCPhisingGuard.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, pCPhisingGuard.LastUpdateBy)
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

            Dim pCPhisingGuard As PCPhisingGuard = CType(obj, PCPhisingGuard)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, pCPhisingGuard.ID)
            DbCommandWrapper.AddInParameter("@CookiesName", DbType.AnsiString, pCPhisingGuard.CookiesName)
            DbCommandWrapper.AddInParameter("@CookiesValue", DbType.AnsiString, pCPhisingGuard.CookiesValue)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, pCPhisingGuard.Description)
            DbCommandWrapper.AddInParameter("@Image", DbType.Binary, pCPhisingGuard.Image)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int32, pCPhisingGuard.Status)
            DbCommandWrapper.AddInParameter("@EncKey", DbType.AnsiString, pCPhisingGuard.EncKey)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, pCPhisingGuard.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, pCPhisingGuard.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As PCPhisingGuard

            Dim pCPhisingGuard As PCPhisingGuard = New PCPhisingGuard

            pCPhisingGuard.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("CookiesName")) Then pCPhisingGuard.CookiesName = dr("CookiesName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CookiesValue")) Then pCPhisingGuard.CookiesValue = dr("CookiesValue").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Description")) Then pCPhisingGuard.Description = dr("Description").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Image")) Then pCPhisingGuard.Image = CType(dr("Image"), Byte())
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then pCPhisingGuard.Status = CType(dr("Status"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("EncKey")) Then pCPhisingGuard.EncKey = dr("EncKey").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then pCPhisingGuard.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then pCPhisingGuard.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then pCPhisingGuard.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then pCPhisingGuard.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then pCPhisingGuard.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            Return pCPhisingGuard

        End Function

        Private Sub SetTableName()

            If Not (GetType(PCPhisingGuard) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(PCPhisingGuard), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(PCPhisingGuard).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

