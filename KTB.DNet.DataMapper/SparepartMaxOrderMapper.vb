
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : SparepartMaxOrder Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2006 
'// ---------------------
'// $History      : $
'// Generated on 10/17/2006 - 9:17:14 AM
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

    Public Class SparepartMaxOrderMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertSparepartMaxOrder"
        Private m_UpdateStatement As String = "up_UpdateSparepartMaxOrder"
        Private m_RetrieveStatement As String = "up_RetrieveSparepartMaxOrder"
        Private m_RetrieveListStatement As String = "up_RetrieveSparepartMaxOrderList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteSparepartMaxOrder"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim sparepartMaxOrder As SparepartMaxOrder = Nothing
            While dr.Read

                sparepartMaxOrder = Me.CreateObject(dr)

            End While

            Return sparepartMaxOrder

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim sparepartMaxOrderList As ArrayList = New ArrayList

            While dr.Read
                Dim sparepartMaxOrder As SparepartMaxOrder = Me.CreateObject(dr)
                sparepartMaxOrderList.Add(sparepartMaxOrder)
            End While

            Return sparepartMaxOrderList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim sparepartMaxOrder As SparepartMaxOrder = CType(obj, SparepartMaxOrder)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, sparepartMaxOrder.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim sparepartMaxOrder As SparepartMaxOrder = CType(obj, SparepartMaxOrder)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@MaxRequest", DbType.Int32, sparepartMaxOrder.MaxRequest)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, sparepartMaxOrder.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, sparepartMaxOrder.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@SparepartID", DbType.Int32, Me.GetRefObject(sparepartMaxOrder.SparePartMaster))

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

            Dim sparepartMaxOrder As SparepartMaxOrder = CType(obj, SparepartMaxOrder)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, sparepartMaxOrder.ID)
            DbCommandWrapper.AddInParameter("@MaxRequest", DbType.Int32, sparepartMaxOrder.MaxRequest)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, sparepartMaxOrder.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, sparepartMaxOrder.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@SparepartID", DbType.Int32, Me.GetRefObject(sparepartMaxOrder.SparePartMaster))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As SparepartMaxOrder

            Dim sparepartMaxOrder As SparepartMaxOrder = New SparepartMaxOrder

            sparepartMaxOrder.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("MaxRequest")) Then sparepartMaxOrder.MaxRequest = CType(dr("MaxRequest"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then sparepartMaxOrder.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then sparepartMaxOrder.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then sparepartMaxOrder.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then sparepartMaxOrder.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then sparepartMaxOrder.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("SparepartID")) Then
                sparepartMaxOrder.SparePartMaster = New SparePartMaster(CType(dr("SparepartID"), Integer))
            End If

            Return sparepartMaxOrder

        End Function

        Private Sub SetTableName()

            If Not (GetType(SparepartMaxOrder) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(SparepartMaxOrder), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(SparepartMaxOrder).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

