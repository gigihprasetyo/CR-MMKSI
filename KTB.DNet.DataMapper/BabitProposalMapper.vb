#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : BabitProposal Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2007 
'// ---------------------
'// $History      : $
'// Generated on 8/30/2007 - 2:28:45 PM
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

    Public Class BabitProposalMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertBabitProposal"
        Private m_UpdateStatement As String = "up_UpdateBabitProposal"
        Private m_RetrieveStatement As String = "up_RetrieveBabitProposal"
        Private m_RetrieveListStatement As String = "up_RetrieveBabitProposalList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteBabitProposal"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim babitProposal As BabitProposal = Nothing
            While dr.Read

                babitProposal = Me.CreateObject(dr)

            End While

            Return babitProposal

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim babitProposalList As ArrayList = New ArrayList

            While dr.Read
                Dim babitProposal As BabitProposal = Me.CreateObject(dr)
                babitProposalList.Add(babitProposal)
            End While

            Return babitProposalList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim babitProposal As BabitProposal = CType(obj, BabitProposal)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, babitProposal.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim babitProposal As BabitProposal = CType(obj, BabitProposal)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DBCommandWrapper.AddInParameter("@Status", DbType.Int32, babitProposal.Status)
            DBCommandWrapper.AddInParameter("@NoSuratDealer", DbType.AnsiString, babitProposal.NoSuratDealer)
            DbCommandWrapper.AddInParameter("@NoPengajuan", DbType.AnsiString, babitProposal.NoPengajuan)
            DbCommandWrapper.AddInParameter("@NoPersetujuan", DbType.AnsiString, babitProposal.NoPersetujuan)
            DbCommandWrapper.AddInParameter("@ActivityType", DbType.Int32, babitProposal.ActivityType)
            DbCommandWrapper.AddInParameter("@TglTerimaEvidance", DbType.DateTime, babitProposal.TglTerimaEvidance)
            DbCommandWrapper.AddInParameter("@KTBApprovalAmount", DbType.Currency, babitProposal.KTBApprovalAmount)
            DbCommandWrapper.AddInParameter("@BabitKhususAmount", DbType.Decimal, babitProposal.BabitKhususAmount)
            DbCommandWrapper.AddInParameter("@FileName", DbType.AnsiString, babitProposal.FileName)
            DBCommandWrapper.AddInParameter("@BabitRealizationFile", DbType.AnsiString, babitProposal.BabitRealizationFile)
            DBCommandWrapper.AddInParameter("@Description", DbType.AnsiString, babitProposal.Description)
            DBCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, babitProposal.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, babitProposal.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@BPPameranID", DbType.Int32, Me.GetRefObject(babitProposal.BPPameran))
            DbCommandWrapper.AddInParameter("@GLAccountID", DbType.Int32, Me.GetRefObject(babitProposal.GLAccount))
            DbCommandWrapper.AddInParameter("@BPEventID", DbType.Int32, Me.GetRefObject(babitProposal.BPEvent))
            DbCommandWrapper.AddInParameter("@BabitAllocationID", DbType.Int32, Me.GetRefObject(babitProposal.BabitAllocation))
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, Me.GetRefObject(babitProposal.Dealer))

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

            Dim babitProposal As BabitProposal = CType(obj, BabitProposal)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, babitProposal.ID)
            DBCommandWrapper.AddInParameter("@Status", DbType.Int32, babitProposal.Status)
            DBCommandWrapper.AddInParameter("@NoSuratDealer", DbType.AnsiString, babitProposal.NoSuratDealer)
            DbCommandWrapper.AddInParameter("@NoPengajuan", DbType.AnsiString, babitProposal.NoPengajuan)
            DbCommandWrapper.AddInParameter("@NoPersetujuan", DbType.AnsiString, babitProposal.NoPersetujuan)
            DbCommandWrapper.AddInParameter("@ActivityType", DbType.Int32, babitProposal.ActivityType)
            DbCommandWrapper.AddInParameter("@TglTerimaEvidance", DbType.DateTime, babitProposal.TglTerimaEvidance)
            DbCommandWrapper.AddInParameter("@KTBApprovalAmount", DbType.Currency, babitProposal.KTBApprovalAmount)
            DbCommandWrapper.AddInParameter("@BabitKhususAmount", DbType.Decimal, babitProposal.BabitKhususAmount)
            DbCommandWrapper.AddInParameter("@FileName", DbType.AnsiString, babitProposal.FileName)
            DBCommandWrapper.AddInParameter("@BabitRealizationFile", DbType.AnsiString, babitProposal.BabitRealizationFile)
            DBCommandWrapper.AddInParameter("@Description", DbType.AnsiString, babitProposal.Description)
            DBCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, babitProposal.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, babitProposal.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@BPPameranID", DbType.Int32, Me.GetRefObject(babitProposal.BPPameran))
            DbCommandWrapper.AddInParameter("@GLAccountID", DbType.Int32, Me.GetRefObject(babitProposal.GLAccount))
            DbCommandWrapper.AddInParameter("@BPEventID", DbType.Int32, Me.GetRefObject(babitProposal.BPEvent))
            DbCommandWrapper.AddInParameter("@BabitAllocationID", DbType.Int32, Me.GetRefObject(babitProposal.BabitAllocation))
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, Me.GetRefObject(babitProposal.Dealer))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As BabitProposal

            Dim babitProposal As BabitProposal = New BabitProposal

            babitProposal.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then babitProposal.Status = CType(dr("Status"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("NoSuratDealer")) Then babitProposal.NoSuratDealer = dr("NoSuratDealer").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("NoPengajuan")) Then babitProposal.NoPengajuan = dr("NoPengajuan").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("NoPersetujuan")) Then babitProposal.NoPersetujuan = dr("NoPersetujuan").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ActivityType")) Then babitProposal.ActivityType = CType(dr("ActivityType"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("TglTerimaEvidance")) Then babitProposal.TglTerimaEvidance = CType(dr("TglTerimaEvidance"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("KTBApprovalAmount")) Then babitProposal.KTBApprovalAmount = CType(dr("KTBApprovalAmount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("BabitKhususAmount")) Then babitProposal.BabitKhususAmount = CType(dr("BabitKhususAmount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("FileName")) Then babitProposal.FileName = dr("FileName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("BabitRealizationFile")) Then babitProposal.BabitRealizationFile = dr("BabitRealizationFile").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Description")) Then babitProposal.Description = dr("Description").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then babitProposal.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then babitProposal.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then babitProposal.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then babitProposal.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then babitProposal.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("BPPameranID")) Then
                babitProposal.BPPameran = New BPPameran(CType(dr("BPPameranID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("GLAccountID")) Then
                babitProposal.GLAccount = New GLAccount(CType(dr("GLAccountID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("BPEventID")) Then
                babitProposal.BPEvent = New BPEvent(CType(dr("BPEventID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("BabitAllocationID")) Then
                babitProposal.BabitAllocation = New BabitAllocation(CType(dr("BabitAllocationID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then
                babitProposal.Dealer = New Dealer(CType(dr("DealerID"), Integer))
            End If

            Return babitProposal

        End Function

        Private Sub SetTableName()

            If Not (GetType(BabitProposal) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(BabitProposal), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(BabitProposal).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

