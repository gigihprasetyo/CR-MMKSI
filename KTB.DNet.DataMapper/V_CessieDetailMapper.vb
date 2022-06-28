
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : V_CessieDetail Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2011 
'// ---------------------
'// $History      : $
'// Generated on 6/3/2011 - 10:49:16 AM
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

    Public Class V_CessieDetailMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertV_CessieDetail"
        Private m_UpdateStatement As String = "up_UpdateV_CessieDetail"
        Private m_RetrieveStatement As String = "up_RetrieveV_CessieDetail"
        Private m_RetrieveListStatement As String = "up_RetrieveV_CessieDetailList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteV_CessieDetail"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim v_CessieDetail As V_CessieDetail = Nothing
            While dr.Read

                v_CessieDetail = Me.CreateObject(dr)

            End While

            Return v_CessieDetail

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim v_CessieDetailList As ArrayList = New ArrayList

            While dr.Read
                Dim v_CessieDetail As V_CessieDetail = Me.CreateObject(dr)
                v_CessieDetailList.Add(v_CessieDetail)
            End While

            Return v_CessieDetailList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim v_CessieDetail As V_CessieDetail = CType(obj, V_CessieDetail)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, v_CessieDetail.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim v_CessieDetail As V_CessieDetail = CType(obj, V_CessieDetail)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@CessieNumber", DbType.AnsiString, v_CessieDetail.CessieNumber)
            DbCommandWrapper.AddInParameter("@CessieDate", DbType.DateTime, v_CessieDetail.CessieDate)
            DbCommandWrapper.AddInParameter("@Amount", DbType.Currency, v_CessieDetail.Amount)
            DbCommandWrapper.AddInParameter("@VH", DbType.Currency, v_CessieDetail.VH)
            DbCommandWrapper.AddInParameter("@IT", DbType.Currency, v_CessieDetail.IT)
            DbCommandWrapper.AddInParameter("@PP", DbType.Currency, v_CessieDetail.PP)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, v_CessieDetail.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastupdateBy", DbType.AnsiString, v_CessieDetail.LastupdateBy)
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

            Dim v_CessieDetail As V_CessieDetail = CType(obj, V_CessieDetail)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, v_CessieDetail.ID)
            DbCommandWrapper.AddInParameter("@CessieNumber", DbType.AnsiString, v_CessieDetail.CessieNumber)
            DbCommandWrapper.AddInParameter("@CessieDate", DbType.DateTime, v_CessieDetail.CessieDate)
            DbCommandWrapper.AddInParameter("@Amount", DbType.Currency, v_CessieDetail.Amount)
            DbCommandWrapper.AddInParameter("@VH", DbType.Currency, v_CessieDetail.VH)
            DbCommandWrapper.AddInParameter("@IT", DbType.Currency, v_CessieDetail.IT)
            DbCommandWrapper.AddInParameter("@PP", DbType.Currency, v_CessieDetail.PP)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, v_CessieDetail.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, v_CessieDetail.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastupdateBy", DbType.AnsiString, v_CessieDetail.LastupdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As V_CessieDetail

            Dim v_CessieDetail As V_CessieDetail = New V_CessieDetail

            v_CessieDetail.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("CessieNumber")) Then v_CessieDetail.CessieNumber = dr("CessieNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CessieDate")) Then v_CessieDetail.CessieDate = CType(dr("CessieDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("Amount")) Then v_CessieDetail.Amount = CType(dr("Amount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("VH")) Then v_CessieDetail.VH = CType(dr("VH"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("IT")) Then v_CessieDetail.IT = CType(dr("IT"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("PP")) Then v_CessieDetail.PP = CType(dr("PP"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then v_CessieDetail.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then v_CessieDetail.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then v_CessieDetail.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastupdateBy")) Then v_CessieDetail.LastupdateBy = dr("LastupdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then v_CessieDetail.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            Return v_CessieDetail

        End Function

        Private Sub SetTableName()

            If Not (GetType(V_CessieDetail) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(V_CessieDetail), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(V_CessieDetail).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

