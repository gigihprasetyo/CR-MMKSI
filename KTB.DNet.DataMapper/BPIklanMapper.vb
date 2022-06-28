#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : BPIklan Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2007 
'// ---------------------
'// $History      : $
'// Generated on 8/28/2007 - 8:19:32 AM
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

    Public Class BPIklanMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertBPIklan"
        Private m_UpdateStatement As String = "up_UpdateBPIklan"
        Private m_RetrieveStatement As String = "up_RetrieveBPIklan"
        Private m_RetrieveListStatement As String = "up_RetrieveBPIklanList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteBPIklan"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim bPIklan As BPIklan = Nothing
            While dr.Read

                bPIklan = Me.CreateObject(dr)

            End While

            Return bPIklan

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim bPIklanList As ArrayList = New ArrayList

            While dr.Read
                Dim bPIklan As BPIklan = Me.CreateObject(dr)
                bPIklanList.Add(bPIklan)
            End While

            Return bPIklanList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim bPIklan As BPIklan = CType(obj, BPIklan)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, bPIklan.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim bPIklan As BPIklan = CType(obj, BPIklan)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@MediaType", DbType.Int32, bPIklan.MediaType)
            DbCommandWrapper.AddInParameter("@MediaName", DbType.AnsiString, bPIklan.MediaName)
            DbCommandWrapper.AddInParameter("@StartDate", DbType.DateTime, bPIklan.StartDate)
            DbCommandWrapper.AddInParameter("@EndDate", DbType.DateTime, bPIklan.EndDate)
            DBCommandWrapper.AddInParameter("@Expense", DbType.Currency, bPIklan.Expense)
            DBCommandWrapper.AddInParameter("@KTBApprovalAmount", DbType.Decimal, bPIklan.KTBApprovalAmount)
            DBCommandWrapper.AddInParameter("@Status", DbType.Int16, bPIklan.Status)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, bPIklan.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, bPIklan.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@BabitProposalID", DbType.Int32, Me.GetRefObject(bPIklan.BabitProposal))
            DBCommandWrapper.AddInParameter("@CategoryID", DbType.Int32, Me.GetRefObject(bPIklan.Category))
            DBCommandWrapper.AddInParameter("@VechileTypeID", DbType.Int32, Me.GetRefObject(bPIklan.VechileType))

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

            Dim bPIklan As BPIklan = CType(obj, BPIklan)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, bPIklan.ID)
            DbCommandWrapper.AddInParameter("@MediaType", DbType.Int32, bPIklan.MediaType)
            DbCommandWrapper.AddInParameter("@MediaName", DbType.AnsiString, bPIklan.MediaName)
            DbCommandWrapper.AddInParameter("@StartDate", DbType.DateTime, bPIklan.StartDate)
            DbCommandWrapper.AddInParameter("@EndDate", DbType.DateTime, bPIklan.EndDate)
            DBCommandWrapper.AddInParameter("@Expense", DbType.Currency, bPIklan.Expense)
            DBCommandWrapper.AddInParameter("@KTBApprovalAmount", DbType.Decimal, bPIklan.KTBApprovalAmount)
            DBCommandWrapper.AddInParameter("@Status", DbType.Int16, bPIklan.Status)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, bPIklan.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, bPIklan.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@BabitProposalID", DbType.Int32, Me.GetRefObject(bPIklan.BabitProposal))
            DBCommandWrapper.AddInParameter("@CategoryID", DbType.Int32, Me.GetRefObject(bPIklan.Category))
            DBCommandWrapper.AddInParameter("@VechileTypeID", DbType.Int32, Me.GetRefObject(bPIklan.VechileType))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As BPIklan

            Dim bPIklan As BPIklan = New BPIklan

            bPIklan.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("MediaType")) Then bPIklan.MediaType = CType(dr("MediaType"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("MediaName")) Then bPIklan.MediaName = dr("MediaName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("StartDate")) Then bPIklan.StartDate = CType(dr("StartDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("EndDate")) Then bPIklan.EndDate = CType(dr("EndDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("Expense")) Then bPIklan.Expense = CType(dr("Expense"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("KTBApprovalAmount")) Then bPIklan.KTBApprovalAmount = CType(dr("KTBApprovalAmount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then bPIklan.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then bPIklan.Status = CType(dr("Status"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then bPIklan.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then bPIklan.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then bPIklan.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then bPIklan.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("BabitProposalID")) Then
                bPIklan.BabitProposal = New BabitProposal(CType(dr("BabitProposalID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("CategoryID")) Then
                bPIklan.Category = New Category(CType(dr("CategoryID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("VechileTypeID")) Then
                bPIklan.VechileType = New VechileType(CType(dr("VechileTypeID"), Integer))
            End If

            Return bPIklan

        End Function

        Private Sub SetTableName()

            If Not (GetType(BPIklan) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(BPIklan), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(BPIklan).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

