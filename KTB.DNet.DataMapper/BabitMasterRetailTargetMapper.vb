
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : BabitMasterRetailTarget Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2019 
'// ---------------------
'// $History      : $
'// Generated on 20/06/2019 - 8:33:06
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

    Public Class BabitMasterRetailTargetMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertBabitMasterRetailTarget"
        Private m_UpdateStatement As String = "up_UpdateBabitMasterRetailTarget"
        Private m_RetrieveStatement As String = "up_RetrieveBabitMasterRetailTarget"
        Private m_RetrieveListStatement As String = "up_RetrieveBabitMasterRetailTargetList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteBabitMasterRetailTarget"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim babitMasterRetailTarget As BabitMasterRetailTarget = Nothing
            While dr.Read

                babitMasterRetailTarget = Me.CreateObject(dr)

            End While

            Return babitMasterRetailTarget

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim babitMasterRetailTargetList As ArrayList = New ArrayList

            While dr.Read
                Dim babitMasterRetailTarget As BabitMasterRetailTarget = Me.CreateObject(dr)
                babitMasterRetailTargetList.Add(babitMasterRetailTarget)
            End While

            Return babitMasterRetailTargetList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim babitMasterRetailTarget As BabitMasterRetailTarget = CType(obj, BabitMasterRetailTarget)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, babitMasterRetailTarget.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim babitMasterRetailTarget As BabitMasterRetailTarget = CType(obj, BabitMasterRetailTarget)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@MonthPeriod", DbType.Byte, babitMasterRetailTarget.MonthPeriod)
            DbCommandWrapper.AddInParameter("@YearPeriod", DbType.Int16, babitMasterRetailTarget.YearPeriod)
            DbCommandWrapper.AddInParameter("@RetailTarget", DbType.Int32, babitMasterRetailTarget.RetailTarget)
            DbCommandWrapper.AddInParameter("@Status", DbType.AnsiString, babitMasterRetailTarget.Status)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, babitMasterRetailTarget.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, babitMasterRetailTarget.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, Me.GetRefObject(babitMasterRetailTarget.Dealer))
            DbCommandWrapper.AddInParameter("@DealerBranchID", DbType.Int32, Me.GetRefObject(babitMasterRetailTarget.DealerBranch))
            DbCommandWrapper.AddInParameter("@SubCategoryVehicleID", DbType.Int16, Me.GetRefObject(babitMasterRetailTarget.SubCategoryVehicle))

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

            Dim babitMasterRetailTarget As BabitMasterRetailTarget = CType(obj, BabitMasterRetailTarget)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, babitMasterRetailTarget.ID)
            DbCommandWrapper.AddInParameter("@MonthPeriod", DbType.Byte, babitMasterRetailTarget.MonthPeriod)
            DbCommandWrapper.AddInParameter("@YearPeriod", DbType.Int16, babitMasterRetailTarget.YearPeriod)
            DbCommandWrapper.AddInParameter("@RetailTarget", DbType.Int32, babitMasterRetailTarget.RetailTarget)
            DbCommandWrapper.AddInParameter("@Status", DbType.AnsiString, babitMasterRetailTarget.Status)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, babitMasterRetailTarget.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, babitMasterRetailTarget.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, Me.GetRefObject(babitMasterRetailTarget.Dealer))
            DbCommandWrapper.AddInParameter("@DealerBranchID", DbType.Int32, Me.GetRefObject(babitMasterRetailTarget.DealerBranch))
            DbCommandWrapper.AddInParameter("@SubCategoryVehicleID", DbType.Int16, Me.GetRefObject(babitMasterRetailTarget.SubCategoryVehicle))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As BabitMasterRetailTarget

            Dim babitMasterRetailTarget As BabitMasterRetailTarget = New BabitMasterRetailTarget

            babitMasterRetailTarget.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("MonthPeriod")) Then babitMasterRetailTarget.MonthPeriod = CType(dr("MonthPeriod"), Byte)
            If Not dr.IsDBNull(dr.GetOrdinal("YearPeriod")) Then babitMasterRetailTarget.YearPeriod = CType(dr("YearPeriod"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("RetailTarget")) Then babitMasterRetailTarget.RetailTarget = CType(dr("RetailTarget"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then babitMasterRetailTarget.Status = dr("Status").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then babitMasterRetailTarget.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then babitMasterRetailTarget.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then babitMasterRetailTarget.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then babitMasterRetailTarget.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then babitMasterRetailTarget.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then
                babitMasterRetailTarget.Dealer = New Dealer(CType(dr("DealerID"), Short))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("DealerBranchID")) Then
                babitMasterRetailTarget.DealerBranch = New DealerBranch(CType(dr("DealerBranchID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("SubCategoryVehicleID")) Then
                babitMasterRetailTarget.SubCategoryVehicle = New SubCategoryVehicle(CType(dr("SubCategoryVehicleID"), Short))
            End If

            Return babitMasterRetailTarget

        End Function

        Private Sub SetTableName()

            If Not (GetType(BabitMasterRetailTarget) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(BabitMasterRetailTarget), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(BabitMasterRetailTarget).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

