
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : sp_RedemptionCeiling Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2010 
'// ---------------------
'// $History      : $
'// Generated on 4/27/2010 - 10:36:35 AM
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

    Public Class sp_RedemptionCeilingMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_Insertsp_RedemptionCeiling"
        Private m_UpdateStatement As String = "up_Updatesp_RedemptionCeiling"
        Private m_RetrieveStatement As String = "up_Retrievesp_RedemptionCeiling"
        Private m_RetrieveListStatement As String = "up_Retrievesp_RedemptionCeilingList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_Deletesp_RedemptionCeiling"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim sp_RedemptionCeiling As sp_RedemptionCeiling = Nothing
            While dr.Read

                sp_RedemptionCeiling = Me.CreateObject(dr)

            End While

            Return sp_RedemptionCeiling

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim sp_RedemptionCeilingList As ArrayList = New ArrayList

            While dr.Read
                Dim sp_RedemptionCeiling As sp_RedemptionCeiling = Me.CreateObject(dr)
                sp_RedemptionCeilingList.Add(sp_RedemptionCeiling)
            End While

            Return sp_RedemptionCeilingList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim sp_RedemptionCeiling As sp_RedemptionCeiling = CType(obj, sp_RedemptionCeiling)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, sp_RedemptionCeiling.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim sp_RedemptionCeiling As sp_RedemptionCeiling = CType(obj, sp_RedemptionCeiling)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@CeilingDate", DbType.DateTime, sp_RedemptionCeiling.CeilingDate)
            DbCommandWrapper.AddInParameter("@InitialCeiling", DbType.Currency, sp_RedemptionCeiling.InitialCeiling)
            DbCommandWrapper.AddInParameter("@TotalProposed", DbType.Currency, sp_RedemptionCeiling.TotalProposed)
            DbCommandWrapper.AddInParameter("@TotalLiquified", DbType.Currency, sp_RedemptionCeiling.TotalLiquified)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int32, sp_RedemptionCeiling.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, sp_RedemptionCeiling.LastUpdateBy)
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

            Dim sp_RedemptionCeiling As sp_RedemptionCeiling = CType(obj, sp_RedemptionCeiling)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, sp_RedemptionCeiling.ID)
            DbCommandWrapper.AddInParameter("@CeilingDate", DbType.DateTime, sp_RedemptionCeiling.CeilingDate)
            DbCommandWrapper.AddInParameter("@InitialCeiling", DbType.Currency, sp_RedemptionCeiling.InitialCeiling)
            DbCommandWrapper.AddInParameter("@TotalProposed", DbType.Currency, sp_RedemptionCeiling.TotalProposed)
            DbCommandWrapper.AddInParameter("@TotalLiquified", DbType.Currency, sp_RedemptionCeiling.TotalLiquified)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int32, sp_RedemptionCeiling.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, sp_RedemptionCeiling.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As sp_RedemptionCeiling

            Dim sp_RedemptionCeiling As sp_RedemptionCeiling = New sp_RedemptionCeiling

            sp_RedemptionCeiling.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("CeilingDate")) Then sp_RedemptionCeiling.CeilingDate = CType(dr("CeilingDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("InitialCeiling")) Then sp_RedemptionCeiling.InitialCeiling = CType(dr("InitialCeiling"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("TotalProposed")) Then sp_RedemptionCeiling.TotalProposed = CType(dr("TotalProposed"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("TotalLiquified")) Then sp_RedemptionCeiling.TotalLiquified = CType(dr("TotalLiquified"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then sp_RedemptionCeiling.RowStatus = CType(dr("RowStatus"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then sp_RedemptionCeiling.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then sp_RedemptionCeiling.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then sp_RedemptionCeiling.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then sp_RedemptionCeiling.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            Return sp_RedemptionCeiling

        End Function

        Private Sub SetTableName()

            If Not (GetType(sp_RedemptionCeiling) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(sp_RedemptionCeiling), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(sp_RedemptionCeiling).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

