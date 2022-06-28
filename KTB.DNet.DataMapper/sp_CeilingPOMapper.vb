
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : sp_CeilingPO Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2012 
'// ---------------------
'// $History      : $
'// Generated on 1/2/2012 - 4:37:40 PM
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

    Public Class sp_CeilingPOMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_Insertsp_CeilingPO"
        Private m_UpdateStatement As String = "up_Updatesp_CeilingPO"
        Private m_RetrieveStatement As String = "up_Retrievesp_CeilingPO"
        Private m_RetrieveListStatement As String = "up_Retrievesp_CeilingPOList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_Deletesp_CeilingPO"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim sp_CeilingPO As sp_CeilingPO = Nothing
            While dr.Read

                sp_CeilingPO = Me.CreateObject(dr)

            End While

            Return sp_CeilingPO

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim sp_CeilingPOList As ArrayList = New ArrayList

            While dr.Read
                Dim sp_CeilingPO As sp_CeilingPO = Me.CreateObject(dr)
                sp_CeilingPOList.Add(sp_CeilingPO)
            End While

            Return sp_CeilingPOList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim sp_CeilingPO As sp_CeilingPO = CType(obj, sp_CeilingPO)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, sp_CeilingPO.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim sp_CeilingPO As sp_CeilingPO = CType(obj, sp_CeilingPO)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@Status", DbType.AnsiString, sp_CeilingPO.Status)
            DbCommandWrapper.AddInParameter("@ReqAllocationDateTime", DbType.DateTime, sp_CeilingPO.ReqAllocationDateTime)
            DBCommandWrapper.AddInParameter("@EffectiveDate", DbType.DateTime, sp_CeilingPO.EffectiveDate)
            DBCommandWrapper.AddInParameter("@GyroNumber", DbType.AnsiString, sp_CeilingPO.GyroNumber)
            DBCommandWrapper.AddInParameter("@GyroStatus", DbType.Int16, sp_CeilingPO.GyroStatus)
            DbCommandWrapper.AddInParameter("@TotalDetail", DbType.Currency, sp_CeilingPO.TotalDetail)
            DbCommandWrapper.AddInParameter("@Remark", DbType.AnsiString, sp_CeilingPO.Remark)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, sp_CeilingPO.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, sp_CeilingPO.LastUpdateBy)
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

            Dim sp_CeilingPO As sp_CeilingPO = CType(obj, sp_CeilingPO)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, sp_CeilingPO.ID)
            DbCommandWrapper.AddInParameter("@Status", DbType.AnsiString, sp_CeilingPO.Status)
            DbCommandWrapper.AddInParameter("@ReqAllocationDateTime", DbType.DateTime, sp_CeilingPO.ReqAllocationDateTime)
            DbCommandWrapper.AddInParameter("@EffectiveDate", DbType.DateTime, sp_CeilingPO.EffectiveDate)
            DBCommandWrapper.AddInParameter("@GyroNumber", DbType.AnsiString, sp_CeilingPO.GyroNumber)
            DBCommandWrapper.AddInParameter("@GyroStatus", DbType.Int16, sp_CeilingPO.GyroStatus)
            DBCommandWrapper.AddInParameter("@TotalDetail", DbType.Currency, sp_CeilingPO.TotalDetail)
            DbCommandWrapper.AddInParameter("@Remark", DbType.AnsiString, sp_CeilingPO.Remark)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, sp_CeilingPO.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, sp_CeilingPO.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As sp_CeilingPO

            Dim sp_CeilingPO As sp_CeilingPO = New sp_CeilingPO

            sp_CeilingPO.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then sp_CeilingPO.Status = dr("Status").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ReqAllocationDateTime")) Then sp_CeilingPO.ReqAllocationDateTime = CType(dr("ReqAllocationDateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("EffectiveDate")) Then sp_CeilingPO.EffectiveDate = CType(dr("EffectiveDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("GyroNumber")) Then sp_CeilingPO.GyroNumber = dr("GyroNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("GyroStatus")) Then sp_CeilingPO.GyroStatus = CType(dr("GyroStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("TotalDetail")) Then sp_CeilingPO.TotalDetail = CType(dr("TotalDetail"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("Remark")) Then sp_CeilingPO.Remark = dr("Remark").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then sp_CeilingPO.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then sp_CeilingPO.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then sp_CeilingPO.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then sp_CeilingPO.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then sp_CeilingPO.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            Return sp_CeilingPO

        End Function

        Private Sub SetTableName()

            If Not (GetType(sp_CeilingPO) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(sp_CeilingPO), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(sp_CeilingPO).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

