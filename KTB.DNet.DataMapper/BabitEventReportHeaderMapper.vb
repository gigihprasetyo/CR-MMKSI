
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : BabitEventReportHeader Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2019 
'// ---------------------
'// $History      : $
'// Generated on 27/05/2019 - 9:57:03
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

    Public Class BabitEventReportHeaderMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertBabitEventReportHeader"
        Private m_UpdateStatement As String = "up_UpdateBabitEventReportHeader"
        Private m_RetrieveStatement As String = "up_RetrieveBabitEventReportHeader"
        Private m_RetrieveListStatement As String = "up_RetrieveBabitEventReportHeaderList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteBabitEventReportHeader"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim babitEventReportHeader As BabitEventReportHeader = Nothing
            While dr.Read

                babitEventReportHeader = Me.CreateObject(dr)

            End While

            Return babitEventReportHeader

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim babitEventReportHeaderList As ArrayList = New ArrayList

            While dr.Read
                Dim babitEventReportHeader As BabitEventReportHeader = Me.CreateObject(dr)
                babitEventReportHeaderList.Add(babitEventReportHeader)
            End While

            Return babitEventReportHeaderList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim babitEventReportHeader As BabitEventReportHeader = CType(obj, BabitEventReportHeader)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, babitEventReportHeader.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim babitEventReportHeader As BabitEventReportHeader = CType(obj, BabitEventReportHeader)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@EventReportName", DbType.AnsiString, babitEventReportHeader.EventReportName)
            DbCommandWrapper.AddInParameter("@PeriodStart", DbType.DateTime, babitEventReportHeader.PeriodStart)
            DbCommandWrapper.AddInParameter("@PeriodEnd", DbType.DateTime, babitEventReportHeader.PeriodEnd)
            DbCommandWrapper.AddInParameter("@LocationName", DbType.AnsiString, babitEventReportHeader.LocationName)
            DbCommandWrapper.AddInParameter("@InvitationQty", DbType.Int32, babitEventReportHeader.InvitationQty)
            DbCommandWrapper.AddInParameter("@AttendeeQty", DbType.Int32, babitEventReportHeader.AttendeeQty)
            DbCommandWrapper.AddInParameter("@Notes", DbType.AnsiString, babitEventReportHeader.Notes)
            DbCommandWrapper.AddInParameter("@NotesMMKSI", DbType.AnsiString, babitEventReportHeader.NotesMMKSI)
            DbCommandWrapper.AddInParameter("@CollaborateDealer", DbType.AnsiString, babitEventReportHeader.CollaborateDealer)
            DbCommandWrapper.AddInParameter("@ApprovalNumber", DbType.AnsiString, babitEventReportHeader.ApprovalNumber)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, babitEventReportHeader.Status)
            DbCommandWrapper.AddInParameter("@ConfirmedBudget", DbType.Decimal, babitEventReportHeader.ConfirmedBudget)
            DbCommandWrapper.AddInParameter("@ApprovedBudget", DbType.Decimal, babitEventReportHeader.ApprovedBudget)

            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, babitEventReportHeader.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, babitEventReportHeader.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@DealerBranchID", DbType.Int32, Me.GetRefObject(babitEventReportHeader.DealerBranch))
            DbCommandWrapper.AddInParameter("@BabitEventProposalHeaderID", DbType.Int32, Me.GetRefObject(babitEventReportHeader.BabitEventProposalHeader))
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, Me.GetRefObject(babitEventReportHeader.Dealer))
            DbCommandWrapper.AddInParameter("@CityID", DbType.Int32, Me.GetRefObject(babitEventReportHeader.City))

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

            Dim babitEventReportHeader As BabitEventReportHeader = CType(obj, BabitEventReportHeader)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, babitEventReportHeader.ID)
            DbCommandWrapper.AddInParameter("@EventReportName", DbType.AnsiString, babitEventReportHeader.EventReportName)
            DbCommandWrapper.AddInParameter("@PeriodStart", DbType.DateTime, babitEventReportHeader.PeriodStart)
            DbCommandWrapper.AddInParameter("@PeriodEnd", DbType.DateTime, babitEventReportHeader.PeriodEnd)
            DbCommandWrapper.AddInParameter("@LocationName", DbType.AnsiString, babitEventReportHeader.LocationName)
            DbCommandWrapper.AddInParameter("@InvitationQty", DbType.Int32, babitEventReportHeader.InvitationQty)
            DbCommandWrapper.AddInParameter("@AttendeeQty", DbType.Int32, babitEventReportHeader.AttendeeQty)
            DbCommandWrapper.AddInParameter("@Notes", DbType.AnsiString, babitEventReportHeader.Notes)
            DbCommandWrapper.AddInParameter("@NotesMMKSI", DbType.AnsiString, babitEventReportHeader.NotesMMKSI)
            DbCommandWrapper.AddInParameter("@CollaborateDealer", DbType.AnsiString, babitEventReportHeader.CollaborateDealer)
            DbCommandWrapper.AddInParameter("@ApprovalNumber", DbType.AnsiString, babitEventReportHeader.ApprovalNumber)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, babitEventReportHeader.Status)
            DbCommandWrapper.AddInParameter("@ConfirmedBudget", DbType.Decimal, babitEventReportHeader.ConfirmedBudget)
            DbCommandWrapper.AddInParameter("@ApprovedBudget", DbType.Decimal, babitEventReportHeader.ApprovedBudget)

            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, babitEventReportHeader.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, babitEventReportHeader.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@DealerBranchID", DbType.Int32, Me.GetRefObject(babitEventReportHeader.DealerBranch))
            DbCommandWrapper.AddInParameter("@BabitEventProposalHeaderID", DbType.Int32, Me.GetRefObject(babitEventReportHeader.BabitEventProposalHeader))
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, Me.GetRefObject(babitEventReportHeader.Dealer))
            DbCommandWrapper.AddInParameter("@CityID", DbType.Int32, Me.GetRefObject(babitEventReportHeader.City))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As BabitEventReportHeader

            Dim babitEventReportHeader As BabitEventReportHeader = New BabitEventReportHeader

            babitEventReportHeader.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("EventReportName")) Then babitEventReportHeader.EventReportName = dr("EventReportName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PeriodStart")) Then babitEventReportHeader.PeriodStart = CType(dr("PeriodStart"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("PeriodEnd")) Then babitEventReportHeader.PeriodEnd = CType(dr("PeriodEnd"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LocationName")) Then babitEventReportHeader.LocationName = dr("LocationName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("InvitationQty")) Then babitEventReportHeader.InvitationQty = CType(dr("InvitationQty"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("AttendeeQty")) Then babitEventReportHeader.AttendeeQty = CType(dr("AttendeeQty"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Notes")) Then babitEventReportHeader.Notes = dr("Notes").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("NotesMMKSI")) Then babitEventReportHeader.NotesMMKSI = dr("NotesMMKSI").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CollaborateDealer")) Then babitEventReportHeader.CollaborateDealer = dr("CollaborateDealer").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ApprovalNumber")) Then babitEventReportHeader.ApprovalNumber = dr("ApprovalNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then babitEventReportHeader.Status = CType(dr("Status"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("ConfirmedBudget")) Then babitEventReportHeader.ConfirmedBudget = CType(dr("ConfirmedBudget"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("ApprovedBudget")) Then babitEventReportHeader.ApprovedBudget = CType(dr("ApprovedBudget"), Decimal)

            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then babitEventReportHeader.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then babitEventReportHeader.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then babitEventReportHeader.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then babitEventReportHeader.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then babitEventReportHeader.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            If Not dr.IsDBNull(dr.GetOrdinal("DealerBranchID")) Then
                babitEventReportHeader.DealerBranch = New DealerBranch(CType(dr("DealerBranchID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then
                babitEventReportHeader.Dealer = New Dealer(CType(dr("DealerID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("BabitEventProposalHeaderID")) Then
                babitEventReportHeader.BabitEventProposalHeader = New BabitEventProposalHeader(CType(dr("BabitEventProposalHeaderID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("CityID")) Then
                babitEventReportHeader.City = New City(CType(dr("CityID"), Integer))
            End If
            Return babitEventReportHeader

        End Function

        Private Sub SetTableName()

            If Not (GetType(BabitEventReportHeader) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(BabitEventReportHeader), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(BabitEventReportHeader).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

