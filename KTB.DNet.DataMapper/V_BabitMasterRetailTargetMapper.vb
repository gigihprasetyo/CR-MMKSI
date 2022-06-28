
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : V_BabitMasterRetailTargetMapper Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2017 
'// ---------------------
'// $History      : $
'// Generated on 07/12/2017 - 5:17:06 PM
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

    Public Class V_BabitMasterRetailTargetMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertV_BabitMasterRetailTarget"
        Private m_UpdateStatement As String = "up_UpdateV_BabitMasterRetailTarget"
        Private m_RetrieveStatement As String = "up_RetrieveV_BabitMasterRetailTarget"
        Private m_RetrieveListStatement As String = "up_RetrieveV_BabitMasterRetailTargetList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteV_BabitMasterRetailTarget"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim V_BabitMasterRetailTarget As V_BabitMasterRetailTarget = Nothing
            While dr.Read

                V_BabitMasterRetailTarget = Me.CreateObject(dr)

            End While

            Return V_BabitMasterRetailTarget

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim V_BabitMasterRetailTargetList As ArrayList = New ArrayList

            While dr.Read
                Dim V_BabitMasterRetailTarget As V_BabitMasterRetailTarget = Me.CreateObject(dr)
                V_BabitMasterRetailTargetList.Add(V_BabitMasterRetailTarget)
            End While

            Return V_BabitMasterRetailTargetList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim V_BabitMasterRetailTarget As V_BabitMasterRetailTarget = CType(obj, V_BabitMasterRetailTarget)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, V_BabitMasterRetailTarget.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim V_BabitMasterRetailTarget As V_BabitMasterRetailTarget = CType(obj, V_BabitMasterRetailTarget)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)

            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@RetailTarget", DbType.Int16, V_BabitMasterRetailTarget.RetailTarget)
            DbCommandWrapper.AddInParameter("@QuarterPeriod", DbType.Int16, V_BabitMasterRetailTarget.QuarterPeriod)
            DbCommandWrapper.AddInParameter("@QuarterPeriodText", DbType.AnsiString, V_BabitMasterRetailTarget.QuarterPeriodText)
            DbCommandWrapper.AddInParameter("@YearPeriod", DbType.Int16, V_BabitMasterRetailTarget.YearPeriod)
            DbCommandWrapper.AddInParameter("@SubCategoryVehicleID", DbType.Int16, V_BabitMasterRetailTarget.SubCategoryVehicleID)
            DbCommandWrapper.AddInParameter("@AllocationBabit", DbType.Decimal, V_BabitMasterRetailTarget.AllocationBabit)
            DbCommandWrapper.AddInParameter("@AdditionalPrice", DbType.Decimal, V_BabitMasterRetailTarget.AdditionalPrice)
            DbCommandWrapper.AddInParameter("@TotalAllocationBabit", DbType.Decimal, V_BabitMasterRetailTarget.TotalAllocationBabit)
            DbCommandWrapper.AddInParameter("@SumSubsidyAmount", DbType.Decimal, V_BabitMasterRetailTarget.SumSubsidyAmount)
            DbCommandWrapper.AddInParameter("@OnGoing", DbType.Decimal, V_BabitMasterRetailTarget.OnGoing)

            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, V_BabitMasterRetailTarget.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, V_BabitMasterRetailTarget.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, Me.GetRefObject(V_BabitMasterRetailTarget.Dealer))
            DbCommandWrapper.AddInParameter("@DealerBranchID", DbType.Int32, Me.GetRefObject(V_BabitMasterRetailTarget.DealerBranch))
            DbCommandWrapper.AddInParameter("@CategoryID", DbType.Int32, Me.GetRefObject(V_BabitMasterRetailTarget.Category))

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

            Dim V_BabitMasterRetailTarget As V_BabitMasterRetailTarget = CType(obj, V_BabitMasterRetailTarget)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, V_BabitMasterRetailTarget.ID)
            
            DbCommandWrapper.AddInParameter("@RetailTarget", DbType.Int16, V_BabitMasterRetailTarget.RetailTarget)
            DbCommandWrapper.AddInParameter("@QuarterPeriod", DbType.Int16, V_BabitMasterRetailTarget.QuarterPeriod)
            DbCommandWrapper.AddInParameter("@QuarterPeriodText", DbType.AnsiString, V_BabitMasterRetailTarget.QuarterPeriodText)
            DbCommandWrapper.AddInParameter("@YearPeriod", DbType.Int16, V_BabitMasterRetailTarget.YearPeriod)
            DbCommandWrapper.AddInParameter("@SubCategoryVehicleID", DbType.Int16, V_BabitMasterRetailTarget.SubCategoryVehicleID)
            DbCommandWrapper.AddInParameter("@AllocationBabit", DbType.Decimal, V_BabitMasterRetailTarget.AllocationBabit)
            DbCommandWrapper.AddInParameter("@AdditionalPrice", DbType.Decimal, V_BabitMasterRetailTarget.AdditionalPrice)
            DbCommandWrapper.AddInParameter("@TotalAllocationBabit", DbType.Decimal, V_BabitMasterRetailTarget.TotalAllocationBabit)
            DbCommandWrapper.AddInParameter("@SumSubsidyAmount", DbType.Decimal, V_BabitMasterRetailTarget.SumSubsidyAmount)
            DbCommandWrapper.AddInParameter("@OnGoing", DbType.Decimal, V_BabitMasterRetailTarget.OnGoing)

            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, V_BabitMasterRetailTarget.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, V_BabitMasterRetailTarget.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, Me.GetRefObject(V_BabitMasterRetailTarget.Dealer))
            DbCommandWrapper.AddInParameter("@DealerBranchID", DbType.Int32, Me.GetRefObject(V_BabitMasterRetailTarget.DealerBranch))
            DbCommandWrapper.AddInParameter("@CategoryID", DbType.Int32, Me.GetRefObject(V_BabitMasterRetailTarget.Category))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As V_BabitMasterRetailTarget

            Dim V_BabitMasterRetailTarget As V_BabitMasterRetailTarget = New V_BabitMasterRetailTarget

            V_BabitMasterRetailTarget.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("RetailTarget")) Then V_BabitMasterRetailTarget.RetailTarget = dr("RetailTarget").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("QuarterPeriod")) Then V_BabitMasterRetailTarget.QuarterPeriod = dr("QuarterPeriod").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("QuarterPeriodText")) Then V_BabitMasterRetailTarget.QuarterPeriodText = dr("QuarterPeriodText").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("YearPeriod")) Then V_BabitMasterRetailTarget.YearPeriod = dr("YearPeriod").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SubCategoryVehicleID")) Then V_BabitMasterRetailTarget.SubCategoryVehicleID = dr("SubCategoryVehicleID").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("AllocationBabit")) Then V_BabitMasterRetailTarget.AllocationBabit = dr("AllocationBabit").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("AdditionalPrice")) Then V_BabitMasterRetailTarget.AdditionalPrice = dr("AdditionalPrice").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("TotalAllocationBabit")) Then V_BabitMasterRetailTarget.TotalAllocationBabit = dr("TotalAllocationBabit").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SumSubsidyAmount")) Then V_BabitMasterRetailTarget.SumSubsidyAmount = dr("SumSubsidyAmount").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("OnGoing")) Then V_BabitMasterRetailTarget.OnGoing = dr("OnGoing").ToString

            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then V_BabitMasterRetailTarget.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then V_BabitMasterRetailTarget.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then V_BabitMasterRetailTarget.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then V_BabitMasterRetailTarget.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then V_BabitMasterRetailTarget.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then
                V_BabitMasterRetailTarget.Dealer = New Dealer(CType(dr("DealerID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("DealerBranchID")) Then
                V_BabitMasterRetailTarget.DealerBranch = New DealerBranch(CType(dr("DealerBranchID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("CategoryID")) Then
                V_BabitMasterRetailTarget.Category = New Category(CType(dr("CategoryID"), Integer))
            End If

            Return V_BabitMasterRetailTarget

        End Function

        Private Sub SetTableName()

            If Not (GetType(V_BabitMasterRetailTarget) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(V_BabitMasterRetailTarget), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(V_BabitMasterRetailTarget).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

