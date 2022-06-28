#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : EquipSPPOAlocation Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2009 
'// ---------------------
'// $History      : $
'// Generated on 9/1/2009 - 11:36:15
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

    Public Class EquipSPPOAlocationMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertEquipSPPOAlocation"
        Private m_UpdateStatement As String = "up_UpdateEquipSPPOAlocation"
        Private m_RetrieveStatement As String = "up_RetrieveEquipSPPOAlocation"
        Private m_RetrieveListStatement As String = "up_RetrieveEquipSPPOAlocationList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteEquipSPPOAlocation"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim equipSPPOAlocation As EquipSPPOAlocation = Nothing
            While dr.Read

                equipSPPOAlocation = Me.CreateObject(dr)

            End While

            Return equipSPPOAlocation

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim equipSPPOAlocationList As ArrayList = New ArrayList

            While dr.Read
                Dim equipSPPOAlocation As EquipSPPOAlocation = Me.CreateObject(dr)
                equipSPPOAlocationList.Add(equipSPPOAlocation)
            End While

            Return equipSPPOAlocationList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim equipSPPOAlocation As EquipSPPOAlocation = CType(obj, EquipSPPOAlocation)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, equipSPPOAlocation.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim equipSPPOAlocation As EquipSPPOAlocation = CType(obj, EquipSPPOAlocation)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@AlocationQty", DbType.Int32, equipSPPOAlocation.AlocationQty)
            DbCommandWrapper.AddInParameter("@RemailQty", DbType.Int32, equipSPPOAlocation.RemailQty)
            DbCommandWrapper.AddInParameter("@Note", DbType.AnsiString, equipSPPOAlocation.Note)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, equipSPPOAlocation.RowStatus)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DBCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdatedTime", DbType.DateTime, equipSPPOAlocation.LastUpdatedTime)
            DbCommandWrapper.AddInParameter("@LastUpdatedBy", DbType.AnsiString, equipSPPOAlocation.LastUpdatedBy)

            DBCommandWrapper.AddInParameter("@SparePartPODetailID", DbType.Int32, Me.GetRefObject(equipSPPOAlocation.SparePartPODetail))

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

            Dim equipSPPOAlocation As EquipSPPOAlocation = CType(obj, EquipSPPOAlocation)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, equipSPPOAlocation.ID)
            DbCommandWrapper.AddInParameter("@AlocationQty", DbType.Int32, equipSPPOAlocation.AlocationQty)
            DbCommandWrapper.AddInParameter("@RemailQty", DbType.Int32, equipSPPOAlocation.RemailQty)
            DbCommandWrapper.AddInParameter("@Note", DbType.AnsiString, equipSPPOAlocation.Note)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, equipSPPOAlocation.RowStatus)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            'DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, equipSPPOAlocation.CreatedBy)
            'DbCommandWrapper.AddInParameter("@LastUpdatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdatedBy", DbType.AnsiString, equipSPPOAlocation.LastUpdatedBy)
            DBCommandWrapper.AddInParameter("@SparePartPODetailID", DbType.Int32, Me.GetRefObject(equipSPPOAlocation.SparePartPODetail))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As EquipSPPOAlocation

            Dim equipSPPOAlocation As EquipSPPOAlocation = New EquipSPPOAlocation

            equipSPPOAlocation.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("AlocationQty")) Then equipSPPOAlocation.AlocationQty = CType(dr("AlocationQty"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("RemailQty")) Then equipSPPOAlocation.RemailQty = CType(dr("RemailQty"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Note")) Then equipSPPOAlocation.Note = dr("Note").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then equipSPPOAlocation.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then equipSPPOAlocation.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then equipSPPOAlocation.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdatedTime")) Then equipSPPOAlocation.LastUpdatedTime = CType(dr("LastUpdatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdatedBy")) Then equipSPPOAlocation.LastUpdatedBy = dr("LastUpdatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SparePartPODetailID")) Then
                equipSPPOAlocation.SparePartPODetail = New SparePartPODetail(CType(dr("SparePartPODetailID"), Integer))
            End If

            Return equipSPPOAlocation

        End Function

        Private Sub SetTableName()

            If Not (GetType(EquipSPPOAlocation) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(EquipSPPOAlocation), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(EquipSPPOAlocation).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

