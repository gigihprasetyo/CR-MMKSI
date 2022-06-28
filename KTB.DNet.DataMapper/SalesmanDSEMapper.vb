#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : SalesmanDSE Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2020 
'// ---------------------
'// $History      : $
'// Generated on 7/27/2020 - 10:42:37 AM
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

    Public Class SalesmanDSEMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertSalesmanDSE"
        Private m_UpdateStatement As String = "up_UpdateSalesmanDSE"
        Private m_RetrieveStatement As String = "up_RetrieveSalesmanDSE"
        Private m_RetrieveListStatement As String = "up_RetrieveSalesmanDSEList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteSalesmanDSE"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim salesmanDSE As SalesmanDSE = Nothing
            While dr.Read

                salesmanDSE = Me.CreateObject(dr)

            End While

            Return salesmanDSE

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim salesmanDSEList As ArrayList = New ArrayList

            While dr.Read
                Dim salesmanDSE As SalesmanDSE = Me.CreateObject(dr)
                salesmanDSEList.Add(salesmanDSE)
            End While

            Return salesmanDSEList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim salesmanDSE As SalesmanDSE = CType(obj, SalesmanDSE)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, salesmanDSE.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim salesmanDSE As SalesmanDSE = CType(obj, SalesmanDSE)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, Me.GetRefObject(salesmanDSE.Dealer))
            DbCommandWrapper.AddInParameter("@SalesmanHeaderID", DbType.Int32, Me.GetRefObject(salesmanDSE.SalesmanHeader))
            DbCommandWrapper.AddInParameter("@Priority", DbType.Int16, salesmanDSE.Priority)
            DbCommandWrapper.AddInParameter("@PhoneNumber", DbType.AnsiString, salesmanDSE.PhoneNumber)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, salesmanDSE.Status)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, salesmanDSE.RowStatus)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.StringFixedLength, salesmanDSE.LastUpdateBy)


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

            Dim salesmanDSE As SalesmanDSE = CType(obj, SalesmanDSE)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, salesmanDSE.ID)
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, Me.GetRefObject(salesmanDSE.Dealer))
            DbCommandWrapper.AddInParameter("@SalesmanHeaderID", DbType.Int32, Me.GetRefObject(salesmanDSE.SalesmanHeader))
            DbCommandWrapper.AddInParameter("@Priority", DbType.Int16, salesmanDSE.Priority)
            DbCommandWrapper.AddInParameter("@PhoneNumber", DbType.AnsiString, salesmanDSE.PhoneNumber)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, salesmanDSE.Status)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, salesmanDSE.RowStatus)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, salesmanDSE.CreatedBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.StringFixedLength, User)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As SalesmanDSE

            Dim salesmanDSE As SalesmanDSE = New SalesmanDSE

            salesmanDSE.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Priority")) Then salesmanDSE.Priority = CType(dr("Priority"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("PhoneNumber")) Then salesmanDSE.PhoneNumber = dr("PhoneNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then salesmanDSE.Status = CType(dr("Status"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then salesmanDSE.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then salesmanDSE.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then salesmanDSE.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then salesmanDSE.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then salesmanDSE.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then
                salesmanDSE.Dealer = New Dealer(CType(dr("DealerID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("SalesmanHeaderID")) Then
                salesmanDSE.SalesmanHeader = New SalesmanHeader(CType(dr("SalesmanHeaderID"), Integer))
            End If

            Return salesmanDSE

        End Function

        Private Sub SetTableName()

            If Not (GetType(SalesmanDSE) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(SalesmanDSE), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(SalesmanDSE).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace
