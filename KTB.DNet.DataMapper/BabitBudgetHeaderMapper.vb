
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : BabitBudgetHeader Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2019 
'// ---------------------
'// $History      : $
'// Generated on 08/10/2019 - 17:00:22
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

    Public Class BabitBudgetHeaderMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertBabitBudgetHeader"
        Private m_UpdateStatement As String = "up_UpdateBabitBudgetHeader"
        Private m_RetrieveStatement As String = "up_RetrieveBabitBudgetHeader"
        Private m_RetrieveListStatement As String = "up_RetrieveBabitBudgetHeaderList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteBabitBudgetHeader"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim babitBudgetHeader As BabitBudgetHeader = Nothing
            While dr.Read

                babitBudgetHeader = Me.CreateObject(dr)

            End While

            Return babitBudgetHeader

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim babitBudgetHeaderList As ArrayList = New ArrayList

            While dr.Read
                Dim babitBudgetHeader As BabitBudgetHeader = Me.CreateObject(dr)
                babitBudgetHeaderList.Add(babitBudgetHeader)
            End While

            Return babitBudgetHeaderList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim babitBudgetHeader As BabitBudgetHeader = CType(obj, BabitBudgetHeader)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, babitBudgetHeader.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim babitBudgetHeader As BabitBudgetHeader = CType(obj, BabitBudgetHeader)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@QuarterPeriod", DbType.Int16, babitBudgetHeader.QuarterPeriod)
            DbCommandWrapper.AddInParameter("@YearPeriod", DbType.Int16, babitBudgetHeader.YearPeriod)
            DbCommandWrapper.AddInParameter("@AllocationBabit", DbType.Currency, babitBudgetHeader.AllocationBabit)
            DbCommandWrapper.AddInParameter("@AdditionalPrice", DbType.Currency, babitBudgetHeader.AdditionalPrice)
            DbCommandWrapper.AddInParameter("@TotalAllocationBabit", DbType.Currency, babitBudgetHeader.TotalAllocationBabit)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, babitBudgetHeader.Description)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, babitBudgetHeader.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, babitBudgetHeader.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, Me.GetRefObject(babitBudgetHeader.Dealer))
            DbCommandWrapper.AddInParameter("@DealerBranchID", DbType.Int32, Me.GetRefObject(babitBudgetHeader.DealerBranch))
            DbCommandWrapper.AddInParameter("@CategoryID", DbType.Int32, Me.GetRefObject(babitBudgetHeader.Category))
            'DbCommandWrapper.AddInParameter("@SubCategoryVehicleID", DbType.Int32, Me.GetRefObject(babitBudgetHeader.SubCategoryVehicle))
            DbCommandWrapper.AddInParameter("@SubCategoryVehicleID", DbType.Int16, babitBudgetHeader.SubCategoryVehicleID)

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

            Dim babitBudgetHeader As BabitBudgetHeader = CType(obj, BabitBudgetHeader)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, babitBudgetHeader.ID)
            DbCommandWrapper.AddInParameter("@QuarterPeriod", DbType.Int16, babitBudgetHeader.QuarterPeriod)
            DbCommandWrapper.AddInParameter("@YearPeriod", DbType.Int16, babitBudgetHeader.YearPeriod)
            DbCommandWrapper.AddInParameter("@AllocationBabit", DbType.Currency, babitBudgetHeader.AllocationBabit)
            DbCommandWrapper.AddInParameter("@AdditionalPrice", DbType.Currency, babitBudgetHeader.AdditionalPrice)
            DbCommandWrapper.AddInParameter("@TotalAllocationBabit", DbType.Currency, babitBudgetHeader.TotalAllocationBabit)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, babitBudgetHeader.Description)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, babitBudgetHeader.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, babitBudgetHeader.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, Me.GetRefObject(babitBudgetHeader.Dealer))
            DbCommandWrapper.AddInParameter("@DealerBranchID", DbType.Int32, Me.GetRefObject(babitBudgetHeader.DealerBranch))
            DbCommandWrapper.AddInParameter("@CategoryID", DbType.Int32, Me.GetRefObject(babitBudgetHeader.Category))
            'DbCommandWrapper.AddInParameter("@SubCategoryVehicleID", DbType.Int32, Me.GetRefObject(babitBudgetHeader.SubCategoryVehicle))
            DbCommandWrapper.AddInParameter("@SubCategoryVehicleID", DbType.Int16, babitBudgetHeader.SubCategoryVehicleID)

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As BabitBudgetHeader

            Dim babitBudgetHeader As BabitBudgetHeader = New BabitBudgetHeader

            babitBudgetHeader.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("YearPeriod")) Then babitBudgetHeader.YearPeriod = CType(dr("YearPeriod"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("QuarterPeriod")) Then babitBudgetHeader.QuarterPeriod = CType(dr("QuarterPeriod"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("AllocationBabit")) Then babitBudgetHeader.AllocationBabit = CType(dr("AllocationBabit"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("AdditionalPrice")) Then babitBudgetHeader.AdditionalPrice = CType(dr("AdditionalPrice"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("TotalAllocationBabit")) Then babitBudgetHeader.TotalAllocationBabit = CType(dr("TotalAllocationBabit"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("Description")) Then babitBudgetHeader.Description = dr("Description").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then babitBudgetHeader.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then babitBudgetHeader.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then babitBudgetHeader.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then babitBudgetHeader.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then babitBudgetHeader.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then
                babitBudgetHeader.Dealer = New Dealer(CType(dr("DealerID"), Short))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("DealerBranchID")) Then
                babitBudgetHeader.DealerBranch = New DealerBranch(CType(dr("DealerBranchID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("CategoryID")) Then
                babitBudgetHeader.Category = New Category(CType(dr("CategoryID"), Short))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("SubCategoryVehicleID")) Then
                babitBudgetHeader.SubCategoryVehicle = New SubCategoryVehicle(CType(dr("SubCategoryVehicleID"), Short))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("SubCategoryVehicleID")) Then babitBudgetHeader.SubCategoryVehicleID = CType(dr("SubCategoryVehicleID"), Short)

            Return babitBudgetHeader

        End Function

        Private Sub SetTableName()

            If Not (GetType(BabitBudgetHeader) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(BabitBudgetHeader), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(BabitBudgetHeader).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

