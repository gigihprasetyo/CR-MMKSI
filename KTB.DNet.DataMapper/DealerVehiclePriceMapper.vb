#Region "Summary"
'// ===========================================================================
'// AUTHOR        : Dnet
'// PURPOSE       : DealerVehiclePrice Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2021 
'// ---------------------
'// $History      : $
'// Generated on 8/19/2021 - 3:26:55 PM
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

    Public Class DealerVehiclePriceMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertDealerVehiclePrice"
        Private m_UpdateStatement As String = "up_UpdateDealerVehiclePrice"
        Private m_RetrieveStatement As String = "up_RetrieveDealerVehiclePrice"
        Private m_RetrieveListStatement As String = "up_RetrieveDealerVehiclePriceList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteDealerVehiclePrice"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim dealerVehiclePrice As DealerVehiclePrice = Nothing
            While dr.Read

                dealerVehiclePrice = Me.CreateObject(dr)

            End While

            Return dealerVehiclePrice

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim dealerVehiclePriceList As ArrayList = New ArrayList

            While dr.Read
                Dim dealerVehiclePrice As DealerVehiclePrice = Me.CreateObject(dr)
                dealerVehiclePriceList.Add(dealerVehiclePrice)
            End While

            Return dealerVehiclePriceList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim dealerVehiclePrice As DealerVehiclePrice = CType(obj, DealerVehiclePrice)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, dealerVehiclePrice.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim dealerVehiclePrice As DealerVehiclePrice = CType(obj, DealerVehiclePrice)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@Name", DbType.AnsiString, dealerVehiclePrice.Name)
            DbCommandWrapper.AddInParameter("@Currency", DbType.AnsiString, dealerVehiclePrice.Currency)
            DbCommandWrapper.AddInParameter("@EffectiveStartDate", DbType.DateTime, dealerVehiclePrice.EffectiveStartDate)
            DbCommandWrapper.AddInParameter("@CustomerClass", DbType.AnsiString, dealerVehiclePrice.CustomerClass)
            DbCommandWrapper.AddInParameter("@CustomerTypeDMS", DbType.AnsiString, dealerVehiclePrice.CustomerTypeDMS)
            DbCommandWrapper.AddInParameter("@CustomerTypeDNET", DbType.Int16, dealerVehiclePrice.CustomerTypeDNET)
            DbCommandWrapper.AddInParameter("@GUID", DbType.AnsiString, dealerVehiclePrice.GUID)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, dealerVehiclePrice.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, dealerVehiclePrice.LastUpdateBy)
            DbCommandWrapper.AddInParameter("@LastUpdateTime", DbType.DateTime, dealerVehiclePrice.LastUpdateTime)

            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, Me.GetRefObject(dealerVehiclePrice.Dealer))

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

            Dim dealerVehiclePrice As DealerVehiclePrice = CType(obj, DealerVehiclePrice)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, dealerVehiclePrice.ID)
            DbCommandWrapper.AddInParameter("@Name", DbType.AnsiString, dealerVehiclePrice.Name)
            DbCommandWrapper.AddInParameter("@Currency", DbType.AnsiString, dealerVehiclePrice.Currency)
            DbCommandWrapper.AddInParameter("@EffectiveStartDate", DbType.DateTime, dealerVehiclePrice.EffectiveStartDate)
            DbCommandWrapper.AddInParameter("@CustomerClass", DbType.AnsiString, dealerVehiclePrice.CustomerClass)
            DbCommandWrapper.AddInParameter("@CustomerTypeDMS", DbType.AnsiString, dealerVehiclePrice.CustomerTypeDMS)
            DbCommandWrapper.AddInParameter("@CustomerTypeDNET", DbType.Int16, dealerVehiclePrice.CustomerTypeDNET)
            DbCommandWrapper.AddInParameter("@GUID", DbType.AnsiString, dealerVehiclePrice.GUID)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, dealerVehiclePrice.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, dealerVehiclePrice.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, dealerVehiclePrice.LastUpdateBy)
            DbCommandWrapper.AddInParameter("@LastUpdateTime", DbType.DateTime, dealerVehiclePrice.LastUpdateTime)


            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, Me.GetRefObject(dealerVehiclePrice.Dealer))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As DealerVehiclePrice

            Dim dealerVehiclePrice As DealerVehiclePrice = New DealerVehiclePrice

            dealerVehiclePrice.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Name")) Then dealerVehiclePrice.Name = dr("Name").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Currency")) Then dealerVehiclePrice.Currency = dr("Currency").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("EffectiveStartDate")) Then dealerVehiclePrice.EffectiveStartDate = CType(dr("EffectiveStartDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("CustomerClass")) Then dealerVehiclePrice.CustomerClass = dr("CustomerClass").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CustomerTypeDMS")) Then dealerVehiclePrice.CustomerTypeDMS = dr("CustomerTypeDMS").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CustomerTypeDNET")) Then dealerVehiclePrice.CustomerTypeDNET = CType(dr("CustomerTypeDNET"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("GUID")) Then dealerVehiclePrice.GUID = dr("GUID").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then dealerVehiclePrice.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then dealerVehiclePrice.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then dealerVehiclePrice.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then dealerVehiclePrice.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then dealerVehiclePrice.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then
                dealerVehiclePrice.Dealer = New Dealer(CType(dr("DealerID"), Short))
            End If

            Return dealerVehiclePrice

        End Function

        Private Sub SetTableName()

            If Not (GetType(DealerVehiclePrice) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(DealerVehiclePrice), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(DealerVehiclePrice).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace
