
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : SparePartDOExpedition Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2016 
'// ---------------------
'// $History      : $
'// Generated on 9/27/2016 - 11:39:26 AM
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

    Public Class SparePartDOExpeditionMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertSparePartDOExpedition"
        Private m_UpdateStatement As String = "up_UpdateSparePartDOExpedition"
        Private m_RetrieveStatement As String = "up_RetrieveSparePartDOExpedition"
        Private m_RetrieveListStatement As String = "up_RetrieveSparePartDOExpeditionList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteSparePartDOExpedition"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim sparePartDOExpedition As SparePartDOExpedition = Nothing
            While dr.Read

                sparePartDOExpedition = Me.CreateObject(dr)

            End While

            Return sparePartDOExpedition

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim sparePartDOExpeditionList As ArrayList = New ArrayList

            While dr.Read
                Dim sparePartDOExpedition As SparePartDOExpedition = Me.CreateObject(dr)
                sparePartDOExpeditionList.Add(sparePartDOExpedition)
            End While

            Return sparePartDOExpeditionList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim sparePartDOExpedition As SparePartDOExpedition = CType(obj, SparePartDOExpedition)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, sparePartDOExpedition.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim sparePartDOExpedition As SparePartDOExpedition = CType(obj, SparePartDOExpedition)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@ExpeditionNo", DbType.AnsiString, sparePartDOExpedition.ExpeditionNo)
            DbCommandWrapper.AddInParameter("@ExpeditionName", DbType.AnsiString, sparePartDOExpedition.ExpeditionName)
            DbCommandWrapper.AddInParameter("@ETA", DbType.DateTime, sparePartDOExpedition.ETA)
            DbCommandWrapper.AddInParameter("@ETD", DbType.DateTime, sparePartDOExpedition.ETD)
            DbCommandWrapper.AddInParameter("@ATD", DbType.DateTime, sparePartDOExpedition.ATD)
            DbCommandWrapper.AddInParameter("@ATA", DbType.DateTime, sparePartDOExpedition.ATA)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, sparePartDOExpedition.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, sparePartDOExpedition.LastUpdateBy)
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

            Dim sparePartDOExpedition As SparePartDOExpedition = CType(obj, SparePartDOExpedition)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, sparePartDOExpedition.ID)
            DbCommandWrapper.AddInParameter("@ExpeditionNo", DbType.AnsiString, sparePartDOExpedition.ExpeditionNo)
            DbCommandWrapper.AddInParameter("@ExpeditionName", DbType.AnsiString, sparePartDOExpedition.ExpeditionName)
            DbCommandWrapper.AddInParameter("@ETA", DbType.DateTime, sparePartDOExpedition.ETA)
            DbCommandWrapper.AddInParameter("@ETD", DbType.DateTime, sparePartDOExpedition.ETD)
            DbCommandWrapper.AddInParameter("@ATD", DbType.DateTime, sparePartDOExpedition.ATD)
            DbCommandWrapper.AddInParameter("@ATA", DbType.DateTime, sparePartDOExpedition.ATA)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, sparePartDOExpedition.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, sparePartDOExpedition.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As SparePartDOExpedition

            Dim sparePartDOExpedition As SparePartDOExpedition = New SparePartDOExpedition

            sparePartDOExpedition.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("ExpeditionNo")) Then sparePartDOExpedition.ExpeditionNo = dr("ExpeditionNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ExpeditionName")) Then sparePartDOExpedition.ExpeditionName = dr("ExpeditionName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ETA")) Then sparePartDOExpedition.ETA = CType(dr("ETA"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("ETD")) Then sparePartDOExpedition.ETD = CType(dr("ETD"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("ATD")) Then sparePartDOExpedition.ATD = CType(dr("ATD"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("ATA")) Then sparePartDOExpedition.ATA = CType(dr("ATA"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then sparePartDOExpedition.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then sparePartDOExpedition.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then sparePartDOExpedition.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then sparePartDOExpedition.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then sparePartDOExpedition.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            Return sparePartDOExpedition

        End Function

        Private Sub SetTableName()

            If Not (GetType(SparePartDOExpedition) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(SparePartDOExpedition), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(SparePartDOExpedition).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

