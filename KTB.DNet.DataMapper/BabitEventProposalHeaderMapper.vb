
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : BabitEventProposalHeader Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2019 
'// ---------------------
'// $History      : $
'// Generated on 02/09/2019 - 16:29:02
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

    Public Class BabitEventProposalHeaderMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertBabitEventProposalHeader"
        Private m_UpdateStatement As String = "up_UpdateBabitEventProposalHeader"
        Private m_RetrieveStatement As String = "up_RetrieveBabitEventProposalHeader"
        Private m_RetrieveListStatement As String = "up_RetrieveBabitEventProposalHeaderList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteBabitEventProposalHeader"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim babitEventProposalHeader As BabitEventProposalHeader = Nothing
            While dr.Read

                babitEventProposalHeader = Me.CreateObject(dr)

            End While

            Return babitEventProposalHeader

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim babitEventProposalHeaderList As ArrayList = New ArrayList

            While dr.Read
                Dim babitEventProposalHeader As BabitEventProposalHeader = Me.CreateObject(dr)
                babitEventProposalHeaderList.Add(babitEventProposalHeader)
            End While

            Return babitEventProposalHeaderList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim babitEventProposalHeader As BabitEventProposalHeader = CType(obj, BabitEventProposalHeader)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, babitEventProposalHeader.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim babitEventProposalHeader As BabitEventProposalHeader = CType(obj, BabitEventProposalHeader)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)

            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@EventRegNumber", DbType.AnsiString, babitEventProposalHeader.EventRegNumber)
            DbCommandWrapper.AddInParameter("@EventProposalName", DbType.AnsiString, babitEventProposalHeader.EventProposalName)
            DbCommandWrapper.AddInParameter("@PeriodStart", DbType.DateTime, babitEventProposalHeader.PeriodStart)
            DbCommandWrapper.AddInParameter("@PeriodEnd", DbType.DateTime, babitEventProposalHeader.PeriodEnd)
            DbCommandWrapper.AddInParameter("@LocationName", DbType.AnsiString, babitEventProposalHeader.LocationName)
            DbCommandWrapper.AddInParameter("@EventStatus", DbType.Int16, babitEventProposalHeader.EventStatus)
            DbCommandWrapper.AddInParameter("@Notes", DbType.AnsiString, babitEventProposalHeader.Notes)
            DbCommandWrapper.AddInParameter("@CollaborateDealer", DbType.AnsiString, babitEventProposalHeader.CollaborateDealer)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, babitEventProposalHeader.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, babitEventProposalHeader.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@DealerBranchID", DbType.Int32, Me.GetRefObject(babitEventProposalHeader.DealerBranch))
            DbCommandWrapper.AddInParameter("@EventDealerHeaderID", DbType.Int32, Me.GetRefObject(babitEventProposalHeader.EventDealerHeader))
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, Me.GetRefObject(babitEventProposalHeader.Dealer))
            DbCommandWrapper.AddInParameter("@CityID", DbType.Int32, Me.GetRefObject(babitEventProposalHeader.City))

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

            Dim babitEventProposalHeader As BabitEventProposalHeader = CType(obj, BabitEventProposalHeader)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, babitEventProposalHeader.ID)
            DbCommandWrapper.AddInParameter("@EventRegNumber", DbType.AnsiString, babitEventProposalHeader.EventRegNumber)
            DbCommandWrapper.AddInParameter("@EventProposalName", DbType.AnsiString, babitEventProposalHeader.EventProposalName)
            DbCommandWrapper.AddInParameter("@PeriodStart", DbType.DateTime, babitEventProposalHeader.PeriodStart)
            DbCommandWrapper.AddInParameter("@PeriodEnd", DbType.DateTime, babitEventProposalHeader.PeriodEnd)
            DbCommandWrapper.AddInParameter("@LocationName", DbType.AnsiString, babitEventProposalHeader.LocationName)
            DbCommandWrapper.AddInParameter("@EventStatus", DbType.Int16, babitEventProposalHeader.EventStatus)
            DbCommandWrapper.AddInParameter("@Notes", DbType.AnsiString, babitEventProposalHeader.Notes)
            DbCommandWrapper.AddInParameter("@CollaborateDealer", DbType.AnsiString, babitEventProposalHeader.CollaborateDealer)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, babitEventProposalHeader.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, babitEventProposalHeader.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@DealerBranchID", DbType.Int32, Me.GetRefObject(babitEventProposalHeader.DealerBranch))
            DbCommandWrapper.AddInParameter("@EventDealerHeaderID", DbType.Int32, Me.GetRefObject(babitEventProposalHeader.EventDealerHeader))
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, Me.GetRefObject(babitEventProposalHeader.Dealer))
            DbCommandWrapper.AddInParameter("@CityID", DbType.Int32, Me.GetRefObject(babitEventProposalHeader.City))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As BabitEventProposalHeader

            Dim babitEventProposalHeader As BabitEventProposalHeader = New BabitEventProposalHeader

            babitEventProposalHeader.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("EventRegNumber")) Then babitEventProposalHeader.EventRegNumber = dr("EventRegNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("EventProposalName")) Then babitEventProposalHeader.EventProposalName = dr("EventProposalName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PeriodStart")) Then babitEventProposalHeader.PeriodStart = CType(dr("PeriodStart"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("PeriodEnd")) Then babitEventProposalHeader.PeriodEnd = CType(dr("PeriodEnd"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LocationName")) Then babitEventProposalHeader.LocationName = dr("LocationName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("EventStatus")) Then babitEventProposalHeader.EventStatus = CType(dr("EventStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("Notes")) Then babitEventProposalHeader.Notes = dr("Notes").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CollaborateDealer")) Then babitEventProposalHeader.CollaborateDealer = dr("CollaborateDealer").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then babitEventProposalHeader.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then babitEventProposalHeader.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then babitEventProposalHeader.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then babitEventProposalHeader.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then babitEventProposalHeader.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerBranchID")) Then
                babitEventProposalHeader.DealerBranch = New DealerBranch(CType(dr("DealerBranchID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("EventDealerHeaderID")) Then
                babitEventProposalHeader.EventDealerHeader = New EventDealerHeader(CType(dr("EventDealerHeaderID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then
                babitEventProposalHeader.Dealer = New Dealer(CType(dr("DealerID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("CityID")) Then
                babitEventProposalHeader.City = New City(CType(dr("CityID"), Integer))
            End If

            Return babitEventProposalHeader

        End Function

        Private Sub SetTableName()

            If Not (GetType(BabitEventProposalHeader) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(BabitEventProposalHeader), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(BabitEventProposalHeader).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

