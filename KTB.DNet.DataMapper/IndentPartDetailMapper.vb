#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : IndentPartDetail Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2007 
'// ---------------------
'// $History      : $
'// Generated on 01/10/2007 - 12:07:39
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

    Public Class IndentPartDetailMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertIndentPartDetail"
        Private m_UpdateStatement As String = "up_UpdateIndentPartDetail"
        Private m_RetrieveStatement As String = "up_RetrieveIndentPartDetail"
        Private m_RetrieveListStatement As String = "up_RetrieveIndentPartDetailList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteIndentPartDetail"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim indentPartDetail As IndentPartDetail = Nothing
            While dr.Read

                indentPartDetail = Me.CreateObject(dr)

            End While

            Return indentPartDetail

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim indentPartDetailList As ArrayList = New ArrayList

            While dr.Read
                Dim indentPartDetail As IndentPartDetail = Me.CreateObject(dr)
                indentPartDetailList.Add(indentPartDetail)
            End While

            Return indentPartDetailList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim indentPartDetail As IndentPartDetail = CType(obj, IndentPartDetail)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, indentPartDetail.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim indentPartDetail As IndentPartDetail = CType(obj, IndentPartDetail)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@TotalForecast", DbType.Int32, indentPartDetail.TotalForecast)
            DbCommandWrapper.AddInParameter("@Qty", DbType.Int32, indentPartDetail.Qty)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, indentPartDetail.Description)
            DbCommandWrapper.AddInParameter("@AllocationQty", DbType.Int32, indentPartDetail.AllocationQty)
            DbCommandWrapper.AddInParameter("@IsCompletedAllocation", DbType.Byte, indentPartDetail.IsCompletedAllocation)
            DbCommandWrapper.AddInParameter("@Price", DbType.Currency, indentPartDetail.Price)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, indentPartDetail.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, indentPartDetail.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@SparePartMasterID", DbType.Int32, Me.GetRefObject(indentPartDetail.SparePartMaster))
            DbCommandWrapper.AddInParameter("@IndentPartHeaderID", DbType.Int32, Me.GetRefObject(indentPartDetail.IndentPartHeader))

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

            Dim indentPartDetail As IndentPartDetail = CType(obj, IndentPartDetail)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, indentPartDetail.ID)
            DbCommandWrapper.AddInParameter("@TotalForecast", DbType.Int32, indentPartDetail.TotalForecast)
            DbCommandWrapper.AddInParameter("@Qty", DbType.Int32, indentPartDetail.Qty)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, indentPartDetail.Description)
            DbCommandWrapper.AddInParameter("@AllocationQty", DbType.Int32, indentPartDetail.AllocationQty)
            DbCommandWrapper.AddInParameter("@IsCompletedAllocation", DbType.Byte, indentPartDetail.IsCompletedAllocation)
            DbCommandWrapper.AddInParameter("@Price", DbType.Currency, indentPartDetail.Price)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, indentPartDetail.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, indentPartDetail.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@SparePartMasterID", DbType.Int32, Me.GetRefObject(indentPartDetail.SparePartMaster))
            DbCommandWrapper.AddInParameter("@IndentPartHeaderID", DbType.Int32, Me.GetRefObject(indentPartDetail.IndentPartHeader))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As IndentPartDetail

            Dim indentPartDetail As IndentPartDetail = New IndentPartDetail

            indentPartDetail.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("TotalForecast")) Then indentPartDetail.TotalForecast = CType(dr("TotalForecast"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Qty")) Then indentPartDetail.Qty = CType(dr("Qty"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Description")) Then indentPartDetail.Description = dr("Description").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("AllocationQty")) Then indentPartDetail.AllocationQty = CType(dr("AllocationQty"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("IsCompletedAllocation")) Then indentPartDetail.IsCompletedAllocation = CType(dr("IsCompletedAllocation"), Byte)
            If Not dr.IsDBNull(dr.GetOrdinal("Price")) Then indentPartDetail.Price = CType(dr("Price"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then indentPartDetail.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then indentPartDetail.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then indentPartDetail.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then indentPartDetail.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then indentPartDetail.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("SparePartMasterID")) Then
                indentPartDetail.SparePartMaster = New SparePartMaster(CType(dr("SparePartMasterID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("IndentPartHeaderID")) Then
                indentPartDetail.IndentPartHeader = New IndentPartHeader(CType(dr("IndentPartHeaderID"), Integer))
            End If

            Return indentPartDetail

        End Function

        Private Sub SetTableName()

            If Not (GetType(IndentPartDetail) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(IndentPartDetail), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(IndentPartDetail).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

