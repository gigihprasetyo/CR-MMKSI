
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : DealerPOTarget Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2019 
'// ---------------------
'// $History      : $
'// Generated on 05/07/2019 - 14:40:15
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

    Public Class DealerPOTargetMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertDealerPOTarget"
        Private m_UpdateStatement As String = "up_UpdateDealerPOTarget"
        Private m_RetrieveStatement As String = "up_RetrieveDealerPOTarget"
        Private m_RetrieveListStatement As String = "up_RetrieveDealerPOTargetList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteDealerPOTarget"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim dealerPOTarget As DealerPOTarget = Nothing
            While dr.Read

                dealerPOTarget = Me.CreateObject(dr)

            End While

            Return dealerPOTarget

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim dealerPOTargetList As ArrayList = New ArrayList

            While dr.Read
                Dim dealerPOTarget As DealerPOTarget = Me.CreateObject(dr)
                dealerPOTargetList.Add(dealerPOTarget)
            End While

            Return dealerPOTargetList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim dealerPOTarget As DealerPOTarget = CType(obj, DealerPOTarget)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, dealerPOTarget.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim dealerPOTarget As DealerPOTarget = CType(obj, DealerPOTarget)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            'DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, dealerPOTarget.DealerID)
            'DbCommandWrapper.AddInParameter("@SubVehicleCategoryID", DbType.Int32, dealerPOTarget.SubVehicleCategoryID)
            DbCommandWrapper.AddInParameter("@MaxQuantity", DbType.Int32, dealerPOTarget.MaxQuantity)
            DbCommandWrapper.AddInParameter("@Sequence", DbType.Int16, dealerPOTarget.Sequence)
            DbCommandWrapper.AddInParameter("@FreeDays", DbType.Int32, dealerPOTarget.FreeDays)
            DbCommandWrapper.AddInParameter("@MaxTOPDay", DbType.Int32, dealerPOTarget.MaxTOPDay)
            DbCommandWrapper.AddInParameter("@ValidFrom", DbType.DateTime, dealerPOTarget.ValidFrom)
            DbCommandWrapper.AddInParameter("@ValidTo", DbType.DateTime, dealerPOTarget.ValidTo)
            DbCommandWrapper.AddInParameter("@IsDefault", DbType.Int16, dealerPOTarget.IsDefault)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, dealerPOTarget.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, dealerPOTarget.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, Me.GetRefObject(dealerPOTarget.Dealer))
            DbCommandWrapper.AddInParameter("@VehicleModelID", DbType.Int32, Me.GetRefObject(dealerPOTarget.VechileModel))

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

            Dim dealerPOTarget As DealerPOTarget = CType(obj, DealerPOTarget)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, dealerPOTarget.ID)
            'DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, dealerPOTarget.DealerID)
            'DbCommandWrapper.AddInParameter("@SubVehicleCategoryID", DbType.Int32, dealerPOTarget.SubVehicleCategoryID)
            DbCommandWrapper.AddInParameter("@MaxQuantity", DbType.Int32, dealerPOTarget.MaxQuantity)
            DbCommandWrapper.AddInParameter("@Sequence", DbType.Int16, dealerPOTarget.Sequence)
            DbCommandWrapper.AddInParameter("@FreeDays", DbType.Int32, dealerPOTarget.FreeDays)
            DbCommandWrapper.AddInParameter("@MaxTOPDay", DbType.Int32, dealerPOTarget.MaxTOPDay)
            DbCommandWrapper.AddInParameter("@ValidFrom", DbType.DateTime, dealerPOTarget.ValidFrom)
            DbCommandWrapper.AddInParameter("@ValidTo", DbType.DateTime, dealerPOTarget.ValidTo)
            DbCommandWrapper.AddInParameter("@IsDefault", DbType.Int16, dealerPOTarget.IsDefault)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, dealerPOTarget.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, dealerPOTarget.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, Me.GetRefObject(dealerPOTarget.Dealer))
            DbCommandWrapper.AddInParameter("@VehicleModelID", DbType.Int32, Me.GetRefObject(dealerPOTarget.VechileModel))


            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As DealerPOTarget

            Dim dealerPOTarget As DealerPOTarget = New DealerPOTarget

            dealerPOTarget.ID = CType(dr("ID"), Integer)
            'If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then dealerPOTarget.DealerID = CType(dr("DealerID"), Integer)
            'If Not dr.IsDBNull(dr.GetOrdinal("SubVehicleCategoryID")) Then dealerPOTarget.SubVehicleCategoryID = CType(dr("SubVehicleCategoryID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("MaxQuantity")) Then dealerPOTarget.MaxQuantity = CType(dr("MaxQuantity"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Sequence")) Then dealerPOTarget.Sequence = CType(dr("Sequence"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("FreeDays")) Then dealerPOTarget.FreeDays = CType(dr("FreeDays"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("MaxTOPDay")) Then dealerPOTarget.MaxTOPDay = CType(dr("MaxTOPDay"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("ValidFrom")) Then dealerPOTarget.ValidFrom = CType(dr("ValidFrom"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("ValidTo")) Then dealerPOTarget.ValidTo = CType(dr("ValidTo"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("IsDefault")) Then dealerPOTarget.IsDefault = CType(dr("IsDefault"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then dealerPOTarget.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then dealerPOTarget.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then dealerPOTarget.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then dealerPOTarget.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then dealerPOTarget.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then
                dealerPOTarget.Dealer = New Dealer(CType(dr("DealerID"), Short))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("VehicleModelID")) Then
                dealerPOTarget.VechileModel = New VechileModel(CType(dr("VehicleModelID"), Short))
            End If

            Return dealerPOTarget

        End Function

        Private Sub SetTableName()

            If Not (GetType(DealerPOTarget) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(DealerPOTarget), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(DealerPOTarget).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

