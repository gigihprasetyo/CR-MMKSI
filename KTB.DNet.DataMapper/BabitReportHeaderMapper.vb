
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : BabitReportHeader Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2019 
'// ---------------------
'// $History      : $
'// Generated on 12/09/2019 - 14:22:11
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

    Public Class BabitReportHeaderMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertBabitReportHeader"
        Private m_UpdateStatement As String = "up_UpdateBabitReportHeader"
        Private m_RetrieveStatement As String = "up_RetrieveBabitReportHeader"
        Private m_RetrieveListStatement As String = "up_RetrieveBabitReportHeaderList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteBabitReportHeader"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim babitReportHeader As BabitReportHeader = Nothing
            While dr.Read

                babitReportHeader = Me.CreateObject(dr)

            End While

            Return babitReportHeader

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim babitReportHeaderList As ArrayList = New ArrayList

            While dr.Read
                Dim babitReportHeader As BabitReportHeader = Me.CreateObject(dr)
                babitReportHeaderList.Add(babitReportHeader)
            End While

            Return babitReportHeaderList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim babitReportHeader As BabitReportHeader = CType(obj, BabitReportHeader)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, babitReportHeader.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim babitReportHeader As BabitReportHeader = CType(obj, BabitReportHeader)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@PeriodStart", DbType.DateTime, babitReportHeader.PeriodStart)
            DbCommandWrapper.AddInParameter("@PeriodEnd", DbType.DateTime, babitReportHeader.PeriodEnd)
            DbCommandWrapper.AddInParameter("@InvitationQty", DbType.Int32, babitReportHeader.InvitationQty)
            DbCommandWrapper.AddInParameter("@AttendeeQty", DbType.Int32, babitReportHeader.AttendeeQty)
            DbCommandWrapper.AddInParameter("@Notes", DbType.AnsiString, babitReportHeader.Notes)
            DbCommandWrapper.AddInParameter("@BabitReportStatus", DbType.Int16, babitReportHeader.BabitReportStatus)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, babitReportHeader.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, babitReportHeader.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@BabitHeaderID", DbType.Int32, Me.GetRefObject(babitReportHeader.BabitHeader))
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, Me.GetRefObject(babitReportHeader.Dealer))

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

            Dim babitReportHeader As BabitReportHeader = CType(obj, BabitReportHeader)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, babitReportHeader.ID)
            DbCommandWrapper.AddInParameter("@PeriodStart", DbType.DateTime, babitReportHeader.PeriodStart)
            DbCommandWrapper.AddInParameter("@PeriodEnd", DbType.DateTime, babitReportHeader.PeriodEnd)
            DbCommandWrapper.AddInParameter("@InvitationQty", DbType.Int32, babitReportHeader.InvitationQty)
            DbCommandWrapper.AddInParameter("@AttendeeQty", DbType.Int32, babitReportHeader.AttendeeQty)
            DbCommandWrapper.AddInParameter("@Notes", DbType.AnsiString, babitReportHeader.Notes)
            DbCommandWrapper.AddInParameter("@BabitReportStatus", DbType.Int16, babitReportHeader.BabitReportStatus)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, babitReportHeader.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, babitReportHeader.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)
            
            DbCommandWrapper.AddInParameter("@BabitHeaderID", DbType.Int32, Me.GetRefObject(babitReportHeader.BabitHeader))
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, Me.GetRefObject(babitReportHeader.Dealer))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As BabitReportHeader

            Dim babitReportHeader As BabitReportHeader = New BabitReportHeader

            babitReportHeader.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("PeriodStart")) Then babitReportHeader.PeriodStart = CType(dr("PeriodStart"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("PeriodEnd")) Then babitReportHeader.PeriodEnd = CType(dr("PeriodEnd"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("InvitationQty")) Then babitReportHeader.InvitationQty = CType(dr("InvitationQty"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("AttendeeQty")) Then babitReportHeader.AttendeeQty = CType(dr("AttendeeQty"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Notes")) Then babitReportHeader.Notes = dr("Notes").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("BabitReportStatus")) Then babitReportHeader.BabitReportStatus = CType(dr("BabitReportStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then babitReportHeader.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then babitReportHeader.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then babitReportHeader.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then babitReportHeader.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then babitReportHeader.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            If Not dr.IsDBNull(dr.GetOrdinal("BabitHeaderID")) Then
                babitReportHeader.BabitHeader = New BabitHeader(CType(dr("BabitHeaderID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then
                babitReportHeader.Dealer = New Dealer(CType(dr("DealerID"), Integer))
            End If

            Return babitReportHeader

        End Function

        Private Sub SetTableName()

            If Not (GetType(BabitReportHeader) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(BabitReportHeader), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(BabitReportHeader).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

