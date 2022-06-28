
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : BabitBudgetDetail Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2019 
'// ---------------------
'// $History      : $
'// Generated on 08/10/2019 - 17:03:10
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

    Public Class BabitBudgetDetailMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertBabitBudgetDetail"
        Private m_UpdateStatement As String = "up_UpdateBabitBudgetDetail"
        Private m_RetrieveStatement As String = "up_RetrieveBabitBudgetDetail"
        Private m_RetrieveListStatement As String = "up_RetrieveBabitBudgetDetailList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteBabitBudgetDetail"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim babitBudgetDetail As BabitBudgetDetail = Nothing
            While dr.Read

                babitBudgetDetail = Me.CreateObject(dr)

            End While

            Return babitBudgetDetail

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim babitBudgetDetailList As ArrayList = New ArrayList

            While dr.Read
                Dim babitBudgetDetail As BabitBudgetDetail = Me.CreateObject(dr)
                babitBudgetDetailList.Add(babitBudgetDetail)
            End While

            Return babitBudgetDetailList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim babitBudgetDetail As BabitBudgetDetail = CType(obj, BabitBudgetDetail)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, babitBudgetDetail.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim babitBudgetDetail As BabitBudgetDetail = CType(obj, BabitBudgetDetail)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@TotalAmount", DbType.Currency, babitBudgetDetail.TotalAmount)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, babitBudgetDetail.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, babitBudgetDetail.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@BabitBudgetHeaderID", DbType.Int16, Me.GetRefObject(babitBudgetDetail.BabitBudgetHeader))
            DbCommandWrapper.AddInParameter("@BabitMasterPriceID", DbType.Int32, Me.GetRefObject(babitBudgetDetail.BabitMasterPrice))
            DbCommandWrapper.AddInParameter("@BabitMasterRetailTargetID", DbType.Int32, Me.GetRefObject(babitBudgetDetail.BabitMasterRetailTarget))

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

            Dim babitBudgetDetail As BabitBudgetDetail = CType(obj, BabitBudgetDetail)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, babitBudgetDetail.ID)
            DbCommandWrapper.AddInParameter("@TotalAmount", DbType.Currency, babitBudgetDetail.TotalAmount)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, babitBudgetDetail.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, babitBudgetDetail.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@BabitBudgetHeaderID", DbType.Int16, Me.GetRefObject(babitBudgetDetail.BabitBudgetHeader))
            DbCommandWrapper.AddInParameter("@BabitMasterPriceID", DbType.Int32, Me.GetRefObject(babitBudgetDetail.BabitMasterPrice))
            DbCommandWrapper.AddInParameter("@BabitMasterRetailTargetID", DbType.Int32, Me.GetRefObject(babitBudgetDetail.BabitMasterRetailTarget))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As BabitBudgetDetail

            Dim babitBudgetDetail As BabitBudgetDetail = New BabitBudgetDetail

            babitBudgetDetail.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("TotalAmount")) Then babitBudgetDetail.TotalAmount = CType(dr("TotalAmount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then babitBudgetDetail.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then babitBudgetDetail.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then babitBudgetDetail.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then babitBudgetDetail.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then babitBudgetDetail.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            If Not dr.IsDBNull(dr.GetOrdinal("BabitBudgetHeaderID")) Then
                babitBudgetDetail.BabitBudgetHeader = New BabitBudgetHeader(CType(dr("BabitBudgetHeaderID"), Short))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("BabitMasterPriceID")) Then
                babitBudgetDetail.BabitMasterPrice = New BabitMasterPrice(CType(dr("BabitMasterPriceID"), Short))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("BabitMasterRetailTargetID")) Then
                babitBudgetDetail.BabitMasterRetailTarget = New BabitMasterRetailTarget(CType(dr("BabitMasterRetailTargetID"), Short))
            End If

            Return babitBudgetDetail

        End Function

        Private Sub SetTableName()

            If Not (GetType(BabitBudgetDetail) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(BabitBudgetDetail), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(BabitBudgetDetail).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

