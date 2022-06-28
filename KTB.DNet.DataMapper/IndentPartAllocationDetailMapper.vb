#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : IndentPartAllocationDetail Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2007 
'// ---------------------
'// $History      : $
'// Generated on 8/6/2007 - 9:05:34 AM
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

    Public Class IndentPartAllocationDetailMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertIndentPartAllocationDetail"
        Private m_UpdateStatement As String = "up_UpdateIndentPartAllocationDetail"
        Private m_RetrieveStatement As String = "up_RetrieveIndentPartAllocationDetail"
        Private m_RetrieveListStatement As String = "up_RetrieveIndentPartAllocationDetailList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteIndentPartAllocationDetail"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim indentPartAllocationDetail As IndentPartAllocationDetail = Nothing
            While dr.Read

                indentPartAllocationDetail = Me.CreateObject(dr)

            End While

            Return indentPartAllocationDetail

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim indentPartAllocationDetailList As ArrayList = New ArrayList

            While dr.Read
                Dim indentPartAllocationDetail As IndentPartAllocationDetail = Me.CreateObject(dr)
                indentPartAllocationDetailList.Add(indentPartAllocationDetail)
            End While

            Return indentPartAllocationDetailList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim indentPartAllocationDetail As IndentPartAllocationDetail = CType(obj, IndentPartAllocationDetail)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, indentPartAllocationDetail.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim indentPartAllocationDetail As IndentPartAllocationDetail = CType(obj, IndentPartAllocationDetail)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@AllocatedQty", DbType.Int32, indentPartAllocationDetail.AllocatedQty)
            DbCommandWrapper.AddInParameter("@PONumber", DbType.AnsiString, indentPartAllocationDetail.PONumber)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, indentPartAllocationDetail.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, indentPartAllocationDetail.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DBCommandWrapper.AddInParameter("@IndentPartDetailID", DbType.Int32, indentPartAllocationDetail.IndentPartDetail)
            DBCommandWrapper.AddInParameter("@IndentPartAllocationHeaderID", DbType.Int32, indentPartAllocationDetail.IndentPartAllocationHeader)

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

            Dim indentPartAllocationDetail As IndentPartAllocationDetail = CType(obj, IndentPartAllocationDetail)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, indentPartAllocationDetail.ID)
            DbCommandWrapper.AddInParameter("@AllocatedQty", DbType.Int32, indentPartAllocationDetail.AllocatedQty)
            DbCommandWrapper.AddInParameter("@PONumber", DbType.AnsiString, indentPartAllocationDetail.PONumber)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, indentPartAllocationDetail.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, indentPartAllocationDetail.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DBCommandWrapper.AddInParameter("@IndentPartDetailID", DbType.Int32, indentPartAllocationDetail.IndentPartDetail)
            DBCommandWrapper.AddInParameter("@IndentPartAllocationHeaderID", DbType.Int32, indentPartAllocationDetail.IndentPartAllocationHeader)

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As IndentPartAllocationDetail

            Dim indentPartAllocationDetail As IndentPartAllocationDetail = New IndentPartAllocationDetail

            indentPartAllocationDetail.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("AllocatedQty")) Then indentPartAllocationDetail.AllocatedQty = CType(dr("AllocatedQty"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("PONumber")) Then indentPartAllocationDetail.PONumber = dr("PONumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then indentPartAllocationDetail.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then indentPartAllocationDetail.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then indentPartAllocationDetail.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then indentPartAllocationDetail.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then indentPartAllocationDetail.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("IndentPartDetailID")) Then
                indentPartAllocationDetail.IndentPartDetail = New IndentPartDetail(CType(dr("IndentPartDetailID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("IndentPartAllocationHeaderID")) Then
                indentPartAllocationDetail.IndentPartAllocationHeader = New IndentPartAllocationHeader(CType(dr("IndentPartAllocationHeaderID"), Integer))
            End If

            Return indentPartAllocationDetail

        End Function

        Private Sub SetTableName()

            If Not (GetType(IndentPartAllocationDetail) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(IndentPartAllocationDetail), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(IndentPartAllocationDetail).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

