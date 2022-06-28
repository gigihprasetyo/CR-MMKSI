
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : DealerSalesTarget Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2019 
'// ---------------------
'// $History      : $
'// Generated on 03/15/2019 - 5:05:04 PM
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

    Public Class DealerSalesTargetMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertDealerSalesTarget"
        Private m_UpdateStatement As String = "up_UpdateDealerSalesTarget"
        Private m_RetrieveStatement As String = "up_RetrieveDealerSalesTarget"
        Private m_RetrieveListStatement As String = "up_RetrieveDealerSalesTargetList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteDealerSalesTarget"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim dealerSalesTarget As DealerSalesTarget = Nothing
            While dr.Read

                dealerSalesTarget = Me.CreateObject(dr)

            End While

            Return dealerSalesTarget

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim dealerSalesTargetList As ArrayList = New ArrayList

            While dr.Read
                Dim dealerSalesTarget As DealerSalesTarget = Me.CreateObject(dr)
                dealerSalesTargetList.Add(dealerSalesTarget)
            End While

            Return dealerSalesTargetList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim dealerSalesTarget As DealerSalesTarget = CType(obj, DealerSalesTarget)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, dealerSalesTarget.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim dealerSalesTarget As DealerSalesTarget = CType(obj, DealerSalesTarget)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            'DbCommandWrapper.AddInParameter("@DealerID",DbType.Int32,dealerSalesTarget.DealerID)
            'DbCommandWrapper.AddInParameter("@VehicleModelID",DbType.Int32,dealerSalesTarget.VehicleModelID)
            DbCommandWrapper.AddInParameter("@Sequence", DbType.Int16, dealerSalesTarget.Sequence)
            DbCommandWrapper.AddInParameter("@FreeDays", DbType.Int32, dealerSalesTarget.FreeDays)
            DbCommandWrapper.AddInParameter("@MaxQuantity", DbType.Int32, dealerSalesTarget.MaxQuantity)
            DbCommandWrapper.AddInParameter("@ValidFrom", DbType.DateTime, dealerSalesTarget.ValidFrom)
            DbCommandWrapper.AddInParameter("@MaxTOPDay", DbType.Int32, dealerSalesTarget.MaxTOPDay)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, dealerSalesTarget.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, dealerSalesTarget.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, Me.GetRefObject(dealerSalesTarget.Dealer))
            DbCommandWrapper.AddInParameter("@VehicleModelID", DbType.Int32, Me.GetRefObject(dealerSalesTarget.VehicleModel))

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

            Dim dealerSalesTarget As DealerSalesTarget = CType(obj, DealerSalesTarget)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, dealerSalesTarget.ID)
            'DbCommandWrapper.AddInParameter("@DealerID",DbType.Int32,dealerSalesTarget.DealerID)
            'DbCommandWrapper.AddInParameter("@VehicleModelID",DbType.Int32,dealerSalesTarget.VehicleModelID)
            DbCommandWrapper.AddInParameter("@Sequence", DbType.Int16, dealerSalesTarget.Sequence)
            DbCommandWrapper.AddInParameter("@FreeDays", DbType.Int32, dealerSalesTarget.FreeDays)
            DbCommandWrapper.AddInParameter("@MaxQuantity", DbType.Int32, dealerSalesTarget.MaxQuantity)
            DbCommandWrapper.AddInParameter("@ValidFrom", DbType.DateTime, dealerSalesTarget.ValidFrom)
            DbCommandWrapper.AddInParameter("@MaxTOPDay", DbType.Int32, dealerSalesTarget.MaxTOPDay)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, dealerSalesTarget.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, dealerSalesTarget.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, Me.GetRefObject(dealerSalesTarget.Dealer))
            DbCommandWrapper.AddInParameter("@VehicleModelID", DbType.Int32, Me.GetRefObject(dealerSalesTarget.VehicleModel))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As DealerSalesTarget

            Dim dealerSalesTarget As DealerSalesTarget = New DealerSalesTarget

            dealerSalesTarget.ID = CType(dr("ID"), Integer)
            'if not dr.IsDBNull(dr.GetOrdinal("DealerID")) then dealerSalesTarget.DealerID = ctype(dr("DealerID"), integer) 
            'if not dr.IsDBNull(dr.GetOrdinal("VehicleModelID")) then dealerSalesTarget.VehicleModelID = ctype(dr("VehicleModelID"), integer) 
            If Not dr.IsDBNull(dr.GetOrdinal("Sequence")) Then dealerSalesTarget.Sequence = CType(dr("Sequence"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("FreeDays")) Then dealerSalesTarget.FreeDays = CType(dr("FreeDays"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("MaxQuantity")) Then dealerSalesTarget.MaxQuantity = CType(dr("MaxQuantity"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("ValidFrom")) Then dealerSalesTarget.ValidFrom = CType(dr("ValidFrom"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("MaxTOPDay")) Then dealerSalesTarget.MaxTOPDay = CType(dr("MaxTOPDay"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then dealerSalesTarget.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then dealerSalesTarget.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then dealerSalesTarget.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then dealerSalesTarget.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then dealerSalesTarget.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then
                dealerSalesTarget.Dealer = New Dealer(CType(dr("DealerID"), Short))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("VehicleModelID")) Then
                dealerSalesTarget.VehicleModel = New VechileModel(CType(dr("VehicleModelID"), Short))
            End If

            Return dealerSalesTarget

        End Function

        Private Sub SetTableName()

            If Not (GetType(DealerSalesTarget) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(DealerSalesTarget), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(DealerSalesTarget).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

