#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : AnnualDiscountAchievement Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2005 
'// ---------------------
'// $History      : $
'// Generated on 11/30/2005 - 1:20:00 PM
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

    Public Class AnnualDiscountAchievementMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertAnnualDiscountAchievement"
        Private m_UpdateStatement As String = "up_UpdateAnnualDiscountAchievement"
        Private m_RetrieveStatement As String = "up_RetrieveAnnualDiscountAchievement"
        Private m_RetrieveListStatement As String = "up_RetrieveAnnualDiscountAchievementList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteAnnualDiscountAchievement"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim annualDiscountAchievement As AnnualDiscountAchievement = Nothing
            While dr.Read

                annualDiscountAchievement = Me.CreateObject(dr)

            End While

            Return annualDiscountAchievement

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim annualDiscountAchievementList As ArrayList = New ArrayList

            While dr.Read
                Dim annualDiscountAchievement As AnnualDiscountAchievement = Me.CreateObject(dr)
                annualDiscountAchievementList.Add(annualDiscountAchievement)
            End While

            Return annualDiscountAchievementList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim annualDiscountAchievement As AnnualDiscountAchievement = CType(obj, AnnualDiscountAchievement)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, annualDiscountAchievement.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim annualDiscountAchievement As AnnualDiscountAchievement = CType(obj, AnnualDiscountAchievement)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@MaterialCode", DbType.AnsiString, annualDiscountAchievement.MaterialCode)
            DbCommandWrapper.AddInParameter("@MaterialDescription", DbType.AnsiString, annualDiscountAchievement.MaterialDescription)
            DbCommandWrapper.AddInParameter("@Point", DbType.Int32, annualDiscountAchievement.Point)
            DbCommandWrapper.AddInParameter("@MinimumQty", DbType.Int32, annualDiscountAchievement.MinimumQty)
            DbCommandWrapper.AddInParameter("@BillQtyThisMonth", DbType.Int32, annualDiscountAchievement.BillQtyThisMonth)
            DbCommandWrapper.AddInParameter("@BillQtyThisPeriod", DbType.Int32, annualDiscountAchievement.BillQtyThisPeriod)
            DbCommandWrapper.AddInParameter("@RebateQtyThisPeriod", DbType.Int32, annualDiscountAchievement.RebateQtyThisPeriod)
            DbCommandWrapper.AddInParameter("@Semester", DbType.Int16, annualDiscountAchievement.Semester)
            DbCommandWrapper.AddInParameter("@RebateAmountThisPeriod", DbType.Int32, annualDiscountAchievement.RebateAmountThisPeriod)
            DbCommandWrapper.AddInParameter("@RemainQty", DbType.Int32, annualDiscountAchievement.RemainQty)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, annualDiscountAchievement.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, annualDiscountAchievement.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@AnnualDiscountAchivementHeaderID", DbType.Int32, Me.GetRefObject(annualDiscountAchievement.AnnualDiscountAchievementHeader))

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

            Dim annualDiscountAchievement As AnnualDiscountAchievement = CType(obj, AnnualDiscountAchievement)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, annualDiscountAchievement.ID)
            DbCommandWrapper.AddInParameter("@MaterialCode", DbType.AnsiString, annualDiscountAchievement.MaterialCode)
            DbCommandWrapper.AddInParameter("@MaterialDescription", DbType.AnsiString, annualDiscountAchievement.MaterialDescription)
            DbCommandWrapper.AddInParameter("@Point", DbType.Int32, annualDiscountAchievement.Point)
            DbCommandWrapper.AddInParameter("@MinimumQty", DbType.Int32, annualDiscountAchievement.MinimumQty)
            DbCommandWrapper.AddInParameter("@BillQtyThisMonth", DbType.Int32, annualDiscountAchievement.BillQtyThisMonth)
            DbCommandWrapper.AddInParameter("@BillQtyThisPeriod", DbType.Int32, annualDiscountAchievement.BillQtyThisPeriod)
            DbCommandWrapper.AddInParameter("@RebateQtyThisPeriod", DbType.Int32, annualDiscountAchievement.RebateQtyThisPeriod)
            DbCommandWrapper.AddInParameter("@Semester", DbType.Int16, annualDiscountAchievement.Semester)
            DbCommandWrapper.AddInParameter("@RebateAmountThisPeriod", DbType.Int32, annualDiscountAchievement.RebateAmountThisPeriod)
            DbCommandWrapper.AddInParameter("@RemainQty", DbType.Int32, annualDiscountAchievement.RemainQty)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, annualDiscountAchievement.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, annualDiscountAchievement.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@AnnualDiscountAchivementHeaderID", DbType.Int32, Me.GetRefObject(annualDiscountAchievement.AnnualDiscountAchievementHeader))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As AnnualDiscountAchievement

            Dim annualDiscountAchievement As AnnualDiscountAchievement = New AnnualDiscountAchievement

            annualDiscountAchievement.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("MaterialCode")) Then annualDiscountAchievement.MaterialCode = dr("MaterialCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("MaterialDescription")) Then annualDiscountAchievement.MaterialDescription = dr("MaterialDescription").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Point")) Then annualDiscountAchievement.Point = CType(dr("Point"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("MinimumQty")) Then annualDiscountAchievement.MinimumQty = CType(dr("MinimumQty"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("BillQtyThisMonth")) Then annualDiscountAchievement.BillQtyThisMonth = CType(dr("BillQtyThisMonth"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("BillQtyThisPeriod")) Then annualDiscountAchievement.BillQtyThisPeriod = CType(dr("BillQtyThisPeriod"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("RebateQtyThisPeriod")) Then annualDiscountAchievement.RebateQtyThisPeriod = CType(dr("RebateQtyThisPeriod"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Semester")) Then annualDiscountAchievement.Semester = CType(dr("Semester"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("RebateAmountThisPeriod")) Then annualDiscountAchievement.RebateAmountThisPeriod = CType(dr("RebateAmountThisPeriod"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("RemainQty")) Then annualDiscountAchievement.RemainQty = CType(dr("RemainQty"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then annualDiscountAchievement.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then annualDiscountAchievement.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then annualDiscountAchievement.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then annualDiscountAchievement.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then annualDiscountAchievement.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("AnnualDiscountAchivementHeaderID")) Then
                annualDiscountAchievement.AnnualDiscountAchievementHeader = New AnnualDiscountAchievementHeader(CType(dr("AnnualDiscountAchivementHeaderID"), Integer))
            End If

            Return annualDiscountAchievement

        End Function

        Private Sub SetTableName()

            If Not (GetType(AnnualDiscountAchievement) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(AnnualDiscountAchievement), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(AnnualDiscountAchievement).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

