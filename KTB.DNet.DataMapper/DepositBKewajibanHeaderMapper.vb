
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : DepositBKewajibanHeader Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2016 
'// ---------------------
'// $History      : $
'// Generated on 3/14/2016 - 11:22:44 AM
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

    Public Class DepositBKewajibanHeaderMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertDepositBKewajibanHeader"
        Private m_UpdateStatement As String = "up_UpdateDepositBKewajibanHeader"
        Private m_RetrieveStatement As String = "up_RetrieveDepositBKewajibanHeader"
        Private m_RetrieveListStatement As String = "up_RetrieveDepositBKewajibanHeaderList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteDepositBKewajibanHeader"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim depositBKewajibanHeader As DepositBKewajibanHeader = Nothing
            While dr.Read

                depositBKewajibanHeader = Me.CreateObject(dr)

            End While

            Return depositBKewajibanHeader

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim depositBKewajibanHeaderList As ArrayList = New ArrayList

            While dr.Read
                Dim depositBKewajibanHeader As DepositBKewajibanHeader = Me.CreateObject(dr)
                depositBKewajibanHeaderList.Add(depositBKewajibanHeader)
            End While

            Return depositBKewajibanHeaderList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim depositBKewajibanHeader As DepositBKewajibanHeader = CType(obj, DepositBKewajibanHeader)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, depositBKewajibanHeader.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim depositBKewajibanHeader As DepositBKewajibanHeader = CType(obj, DepositBKewajibanHeader)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@NoRegKewajiban", DbType.String, depositBKewajibanHeader.NoRegKewajiban)
            DbCommandWrapper.AddInParameter("@TipeKewajiban", DbType.Int16, depositBKewajibanHeader.TipeKewajiban)
            DbCommandWrapper.AddInParameter("@PeriodYear", DbType.Int16, depositBKewajibanHeader.PeriodYear)
            DbCommandWrapper.AddInParameter("@NoSalesorder", DbType.String, depositBKewajibanHeader.NoSalesorder)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, depositBKewajibanHeader.Status)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, depositBKewajibanHeader.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, depositBKewajibanHeader.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, Me.GetRefObject(depositBKewajibanHeader.Dealer))
            DbCommandWrapper.AddInParameter("@ProductCategoryID", DbType.Int16, Me.GetRefObject(depositBKewajibanHeader.ProductCategory))

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

            Dim depositBKewajibanHeader As DepositBKewajibanHeader = CType(obj, DepositBKewajibanHeader)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, depositBKewajibanHeader.ID)
            DbCommandWrapper.AddInParameter("@NoRegKewajiban", DbType.String, depositBKewajibanHeader.NoRegKewajiban)
            DbCommandWrapper.AddInParameter("@TipeKewajiban", DbType.Int16, depositBKewajibanHeader.TipeKewajiban)
            DbCommandWrapper.AddInParameter("@PeriodYear", DbType.Int16, depositBKewajibanHeader.PeriodYear)
            DbCommandWrapper.AddInParameter("@NoSalesorder", DbType.String, depositBKewajibanHeader.NoSalesorder)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, depositBKewajibanHeader.Status)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, depositBKewajibanHeader.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, depositBKewajibanHeader.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, Me.GetRefObject(depositBKewajibanHeader.Dealer))
            DbCommandWrapper.AddInParameter("@ProductCategoryID", DbType.Int16, Me.GetRefObject(depositBKewajibanHeader.ProductCategory))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As DepositBKewajibanHeader

            Dim depositBKewajibanHeader As DepositBKewajibanHeader = New DepositBKewajibanHeader

            depositBKewajibanHeader.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("NoRegKewajiban")) Then depositBKewajibanHeader.NoRegKewajiban = dr("NoRegKewajiban").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("TipeKewajiban")) Then depositBKewajibanHeader.TipeKewajiban = CType(dr("TipeKewajiban"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("PeriodYear")) Then depositBKewajibanHeader.PeriodYear = CType(dr("PeriodYear"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("NoSalesorder")) Then depositBKewajibanHeader.NoSalesorder = dr("NoSalesorder").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then depositBKewajibanHeader.Status = CType(dr("Status"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then depositBKewajibanHeader.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then depositBKewajibanHeader.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then depositBKewajibanHeader.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then depositBKewajibanHeader.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then depositBKewajibanHeader.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then
                depositBKewajibanHeader.Dealer = New Dealer(CType(dr("DealerID"), Short))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("ProductCategoryID")) Then
                depositBKewajibanHeader.ProductCategory = New ProductCategory(CType(dr("ProductCategoryID"), Integer))
            End If

            Return depositBKewajibanHeader

        End Function

        Private Sub SetTableName()

            If Not (GetType(DepositBKewajibanHeader) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(DepositBKewajibanHeader), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(DepositBKewajibanHeader).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

