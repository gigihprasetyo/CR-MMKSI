
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : DealerOperationAreaBussiness Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2020 
'// ---------------------
'// $History      : $
'// Generated on 5/28/2020 - 9:04:15 AM
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

    Public Class DealerOperationAreaBussinessMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertDealerOperationAreaBussiness"
        Private m_UpdateStatement As String = "up_UpdateDealerOperationAreaBussiness"
        Private m_RetrieveStatement As String = "up_RetrieveDealerOperationAreaBussiness"
        Private m_RetrieveListStatement As String = "up_RetrieveDealerOperationAreaBussinessList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteDealerOperationAreaBussiness"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim dealerOperationAreaBussiness As DealerOperationAreaBussiness = Nothing
            While dr.Read

                dealerOperationAreaBussiness = Me.CreateObject(dr)

            End While

            Return dealerOperationAreaBussiness

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim dealerOperationAreaBussinessList As ArrayList = New ArrayList

            While dr.Read
                Dim dealerOperationAreaBussiness As DealerOperationAreaBussiness = Me.CreateObject(dr)
                dealerOperationAreaBussinessList.Add(dealerOperationAreaBussiness)
            End While

            Return dealerOperationAreaBussinessList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim dealerOperationAreaBussiness As DealerOperationAreaBussiness = CType(obj, DealerOperationAreaBussiness)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, dealerOperationAreaBussiness.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim dealerOperationAreaBussiness As DealerOperationAreaBussiness = CType(obj, DealerOperationAreaBussiness)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            'DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, dealerOperationAreaBussiness.DealerID)
            DbCommandWrapper.AddInParameter("@AreaBusiness", DbType.Int16, dealerOperationAreaBussiness.AreaBusiness)
            DbCommandWrapper.AddInParameter("@DealerOperation", DbType.AnsiString, dealerOperationAreaBussiness.DealerOperation)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, dealerOperationAreaBussiness.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, dealerOperationAreaBussiness.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, Me.GetRefObject(dealerOperationAreaBussiness.Dealer))

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

            Dim dealerOperationAreaBussiness As DealerOperationAreaBussiness = CType(obj, DealerOperationAreaBussiness)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, dealerOperationAreaBussiness.ID)
            'DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, dealerOperationAreaBussiness.DealerID)
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, Me.GetRefObject(dealerOperationAreaBussiness.Dealer))
            DbCommandWrapper.AddInParameter("@AreaBusiness", DbType.Int16, dealerOperationAreaBussiness.AreaBusiness)
            DbCommandWrapper.AddInParameter("@DealerOperation", DbType.AnsiString, dealerOperationAreaBussiness.DealerOperation)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, dealerOperationAreaBussiness.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, dealerOperationAreaBussiness.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As DealerOperationAreaBussiness

            Dim dealerOperationAreaBussiness As DealerOperationAreaBussiness = New DealerOperationAreaBussiness

            dealerOperationAreaBussiness.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then dealerOperationAreaBussiness.DealerID = CType(dr("DealerID"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("AreaBusiness")) Then dealerOperationAreaBussiness.AreaBusiness = CType(dr("AreaBusiness"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerOperation")) Then dealerOperationAreaBussiness.DealerOperation = dr("DealerOperation").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then dealerOperationAreaBussiness.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then dealerOperationAreaBussiness.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then dealerOperationAreaBussiness.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then dealerOperationAreaBussiness.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then dealerOperationAreaBussiness.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then
                dealerOperationAreaBussiness.Dealer = New Dealer(CType(dr("DealerID"), Integer))
            End If
            Return dealerOperationAreaBussiness

        End Function

        Private Sub SetTableName()

            If Not (GetType(DealerOperationAreaBussiness) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(DealerOperationAreaBussiness), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(DealerOperationAreaBussiness).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

