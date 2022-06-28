#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : SalesmanHeaderToDealer Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2019 
'// ---------------------
'// $History      : $
'// Generated on 11/12/2019 - 10:16:12 AM
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

    Public Class SalesmanHeaderToDealerMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertSalesmanHeaderToDealer"
        Private m_UpdateStatement As String = "up_UpdateSalesmanHeaderToDealer"
        Private m_RetrieveStatement As String = "up_RetrieveSalesmanHeaderToDealer"
        Private m_RetrieveListStatement As String = "up_RetrieveSalesmanHeaderToDealerList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteSalesmanHeaderToDealer"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim salesmanHeaderToDealer As SalesmanHeaderToDealer = Nothing
            While dr.Read

                salesmanHeaderToDealer = Me.CreateObject(dr)

            End While

            Return salesmanHeaderToDealer

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim salesmanHeaderToDealerList As ArrayList = New ArrayList

            While dr.Read
                Dim salesmanHeaderToDealer As SalesmanHeaderToDealer = Me.CreateObject(dr)
                salesmanHeaderToDealerList.Add(salesmanHeaderToDealer)
            End While

            Return salesmanHeaderToDealerList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim salesmanHeaderToDealer As SalesmanHeaderToDealer = CType(obj, SalesmanHeaderToDealer)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, salesmanHeaderToDealer.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim salesmanHeaderToDealer As SalesmanHeaderToDealer = CType(obj, SalesmanHeaderToDealer)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@SalesmanHeaderID", DbType.Int32, Me.GetRefObject(salesmanHeaderToDealer.SalesmanHeader))
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, Me.GetRefObject(salesmanHeaderToDealer.Dealer))
            DbCommandWrapper.AddInParameter("@Username", DbType.AnsiString, salesmanHeaderToDealer.Username)
            DbCommandWrapper.AddInParameter("@IsMainDealer", DbType.Boolean, salesmanHeaderToDealer.IsMainDealer)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, salesmanHeaderToDealer.RowStatus)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, salesmanHeaderToDealer.Status)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, salesmanHeaderToDealer.LastUpdateBy)
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

            Dim salesmanHeaderToDealer As SalesmanHeaderToDealer = CType(obj, SalesmanHeaderToDealer)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, salesmanHeaderToDealer.ID)
            DbCommandWrapper.AddInParameter("@SalesmanHeaderID", DbType.Int32, Me.GetRefObject(salesmanHeaderToDealer.SalesmanHeader))
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, Me.GetRefObject(salesmanHeaderToDealer.Dealer))
            DbCommandWrapper.AddInParameter("@Username", DbType.AnsiString, salesmanHeaderToDealer.Username)
            DbCommandWrapper.AddInParameter("@IsMainDealer", DbType.Boolean, salesmanHeaderToDealer.IsMainDealer)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, salesmanHeaderToDealer.RowStatus)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, salesmanHeaderToDealer.Status)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, salesmanHeaderToDealer.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As SalesmanHeaderToDealer

            Dim salesmanHeaderToDealer As SalesmanHeaderToDealer = New SalesmanHeaderToDealer

            salesmanHeaderToDealer.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("SalesmanHeaderID")) Then salesmanHeaderToDealer.SalesmanHeader = New SalesmanHeader(CType(dr("SalesmanHeaderID"), Integer))
            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then salesmanHeaderToDealer.Dealer = New Dealer(CType(dr("DealerID"), Integer))
            If Not dr.IsDBNull(dr.GetOrdinal("Username")) Then salesmanHeaderToDealer.Username = dr("Username").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("IsMainDealer")) Then salesmanHeaderToDealer.IsMainDealer = CType(dr("IsMainDealer"), Boolean)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then salesmanHeaderToDealer.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then salesmanHeaderToDealer.Status = CType(dr("Status"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then salesmanHeaderToDealer.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then salesmanHeaderToDealer.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then salesmanHeaderToDealer.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then salesmanHeaderToDealer.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            Return salesmanHeaderToDealer

        End Function

        Private Sub SetTableName()

            If Not (GetType(SalesmanHeaderToDealer) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(SalesmanHeaderToDealer), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(SalesmanHeaderToDealer).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace
