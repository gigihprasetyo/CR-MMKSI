#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : PKProductionPlan Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2005 
'// ---------------------
'// $History      : $
'// Generated on 12/1/2005 - 1:05:49 PM
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

    Public Class PKProductionPlanMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertPKProductionPlan"
        Private m_UpdateStatement As String = "up_UpdatePKProductionPlan"
        Private m_RetrieveStatement As String = "up_RetrievePKProductionPlan"
        Private m_RetrieveListStatement As String = "up_RetrievePKProductionPlanList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeletePKProductionPlan"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim pKProductionPlan As PKProductionPlan = Nothing
            While dr.Read

                pKProductionPlan = Me.CreateObject(dr)

            End While

            Return pKProductionPlan

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim pKProductionPlanList As ArrayList = New ArrayList

            While dr.Read
                Dim pKProductionPlan As PKProductionPlan = Me.CreateObject(dr)
                pKProductionPlanList.Add(pKProductionPlan)
            End While

            Return pKProductionPlanList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim pKProductionPlan As PKProductionPlan = CType(obj, PKProductionPlan)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@id", DbType.Int32, pKProductionPlan.id)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim pKProductionPlan As PKProductionPlan = CType(obj, PKProductionPlan)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@id", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@PeriodMonth", DbType.Int16, pKProductionPlan.PeriodMonth)
            DbCommandWrapper.AddInParameter("@PeriodYear", DbType.Int16, pKProductionPlan.PeriodYear)
            DbCommandWrapper.AddInParameter("@ProductionYear", DbType.Int16, pKProductionPlan.ProductionYear)
            DbCommandWrapper.AddInParameter("@CarryOverPreviousQty", DbType.Int32, pKProductionPlan.CarryOverPreviousQty)
            DbCommandWrapper.AddInParameter("@PlanQty", DbType.Int32, pKProductionPlan.PlanQty)
            DbCommandWrapper.AddInParameter("@UnselledStock", DbType.Int32, pKProductionPlan.UnselledStock)

            DbCommandWrapper.AddInParameter("@AllocationQty", DbType.Int32, pKProductionPlan.AllocationQty)
            DbCommandWrapper.AddInParameter("@ReserveQty", DbType.Int32, pKProductionPlan.ReserveQty)

            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, pKProductionPlan.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LasUpdateBy", DbType.AnsiString, pKProductionPlan.LasUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@VehicleColorID", DbType.Int32, Me.GetRefObject(pKProductionPlan.VechileColor))

            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetNewID(ByVal dbCommandWrapper As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper) As Integer

            Return CType(dbCommandWrapper.GetParameterValue("@id"), Integer)

        End Function

        Protected Overrides Function GetPagingRetrieveCommand() As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_PagingQuery)
            DbCommandWrapper.AddInParameter("@Table", DbType.String, m_TableName)
            DbCommandWrapper.AddInParameter("@PK", DbType.String, "id")

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
            DbCommandWrapper.AddInParameter("@id", DbType.Int32, id)

            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetUpdateParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim pKProductionPlan As PKProductionPlan = CType(obj, PKProductionPlan)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@id", DbType.Int32, pKProductionPlan.id)
            DbCommandWrapper.AddInParameter("@PeriodMonth", DbType.Int16, pKProductionPlan.PeriodMonth)
            DbCommandWrapper.AddInParameter("@PeriodYear", DbType.Int16, pKProductionPlan.PeriodYear)
            DbCommandWrapper.AddInParameter("@ProductionYear", DbType.Int16, pKProductionPlan.ProductionYear)
            DbCommandWrapper.AddInParameter("@CarryOverPreviousQty", DbType.Int32, pKProductionPlan.CarryOverPreviousQty)
            DbCommandWrapper.AddInParameter("@PlanQty", DbType.Int32, pKProductionPlan.PlanQty)
            DbCommandWrapper.AddInParameter("@UnselledStock", DbType.Int32, pKProductionPlan.UnselledStock)

            DbCommandWrapper.AddInParameter("@AllocationQty", DbType.Int32, pKProductionPlan.AllocationQty)
            DbCommandWrapper.AddInParameter("@ReserveQty", DbType.Int32, pKProductionPlan.ReserveQty)

            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, pKProductionPlan.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, pKProductionPlan.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LasUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@VehicleColorID", DbType.Int32, Me.GetRefObject(pKProductionPlan.VechileColor))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As PKProductionPlan

            Dim pKProductionPlan As PKProductionPlan = New PKProductionPlan

            pKProductionPlan.id = CType(dr("id"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("PeriodMonth")) Then pKProductionPlan.PeriodMonth = CType(dr("PeriodMonth"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("PeriodYear")) Then pKProductionPlan.PeriodYear = CType(dr("PeriodYear"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("ProductionYear")) Then pKProductionPlan.ProductionYear = CType(dr("ProductionYear"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CarryOverPreviousQty")) Then pKProductionPlan.CarryOverPreviousQty = CType(dr("CarryOverPreviousQty"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("PlanQty")) Then pKProductionPlan.PlanQty = CType(dr("PlanQty"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("UnselledStock")) Then pKProductionPlan.UnselledStock = CType(dr("UnselledStock"), Integer)

            If Not dr.IsDBNull(dr.GetOrdinal("AllocationQty")) Then pKProductionPlan.AllocationQty = CType(dr("AllocationQty"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("ReserveQty")) Then pKProductionPlan.ReserveQty = CType(dr("ReserveQty"), Integer)

            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then pKProductionPlan.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then pKProductionPlan.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then pKProductionPlan.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LasUpdateBy")) Then pKProductionPlan.LasUpdateBy = dr("LasUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then pKProductionPlan.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("VehicleColorID")) Then
                pKProductionPlan.VechileColor = New VechileColor(CType(dr("VehicleColorID"), Integer))
            End If

            Return pKProductionPlan

        End Function

        Private Sub SetTableName()

            If Not (GetType(PKProductionPlan) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(PKProductionPlan), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(PKProductionPlan).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

