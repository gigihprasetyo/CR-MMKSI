
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : BabitHeader Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2019 
'// ---------------------
'// $History      : $
'// Generated on 08/05/2019 - 7:57:15
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

    Public Class BabitHeaderMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertBabitHeader"
        Private m_UpdateStatement As String = "up_UpdateBabitHeader"
        Private m_RetrieveStatement As String = "up_RetrieveBabitHeader"
        Private m_RetrieveListStatement As String = "up_RetrieveBabitHeaderList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteBabitHeader"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim babitHeader As BabitHeader = Nothing
            While dr.Read

                babitHeader = Me.CreateObject(dr)

            End While

            Return babitHeader

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim babitHeaderList As ArrayList = New ArrayList

            While dr.Read
                Dim babitHeader As BabitHeader = Me.CreateObject(dr)
                babitHeaderList.Add(babitHeader)
            End While

            Return babitHeaderList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim babitHeader As BabitHeader = CType(obj, BabitHeader)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, babitHeader.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim babitHeader As BabitHeader = CType(obj, BabitHeader)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@BabitRegNumber", DbType.AnsiString, babitHeader.BabitRegNumber)
            DbCommandWrapper.AddInParameter("@BabitDealerNumber", DbType.AnsiString, babitHeader.BabitDealerNumber)
            DbCommandWrapper.AddInParameter("@AllocationType", DbType.Int16, babitHeader.AllocationType)
            DbCommandWrapper.AddInParameter("@PeriodStart", DbType.DateTime, babitHeader.PeriodStart)
            DbCommandWrapper.AddInParameter("@PeriodEnd", DbType.DateTime, babitHeader.PeriodEnd)
            DbCommandWrapper.AddInParameter("@BabitStatus", DbType.Int16, babitHeader.BabitStatus)
            DbCommandWrapper.AddInParameter("@Location", DbType.AnsiString, babitHeader.Location)
            DbCommandWrapper.AddInParameter("@ProspectTarget", DbType.Int32, babitHeader.ProspectTarget)
            DbCommandWrapper.AddInParameter("@LuasArea", DbType.Int32, babitHeader.LuasArea)
            DbCommandWrapper.AddInParameter("@InvitationQty", DbType.Int32, babitHeader.InvitationQty)
            DbCommandWrapper.AddInParameter("@MarboxID", DbType.AnsiString, babitHeader.MarboxID)
            DbCommandWrapper.AddInParameter("@Notes", DbType.AnsiString, babitHeader.Notes)
            DbCommandWrapper.AddInParameter("@ApprovalNumber", DbType.AnsiString, babitHeader.ApprovalNumber)
            DbCommandWrapper.AddInParameter("@SPKTarget", DbType.Int32, babitHeader.SPKTarget)
            DbCommandWrapper.AddInParameter("@BabitDealerGroup", DbType.AnsiString, babitHeader.BabitDealerGroup)

            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, babitHeader.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, babitHeader.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, Me.GetRefObject(babitHeader.Dealer))
            DbCommandWrapper.AddInParameter("@DealerBranchID", DbType.Int32, Me.GetRefObject(babitHeader.DealerBranch))
            DbCommandWrapper.AddInParameter("@CityID", DbType.Int32, Me.GetRefObject(babitHeader.City))
            DbCommandWrapper.AddInParameter("@BabitMasterLocationID", DbType.Int32, Me.GetRefObject(babitHeader.BabitMasterLocation))
            DbCommandWrapper.AddInParameter("@BabitMasterEventTypeID", DbType.Int32, Me.GetRefObject(babitHeader.BabitMasterEventType))

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

            Dim babitHeader As BabitHeader = CType(obj, BabitHeader)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, babitHeader.ID)
            DbCommandWrapper.AddInParameter("@BabitRegNumber", DbType.AnsiString, babitHeader.BabitRegNumber)
            DbCommandWrapper.AddInParameter("@BabitDealerNumber", DbType.AnsiString, babitHeader.BabitDealerNumber)
            DbCommandWrapper.AddInParameter("@AllocationType", DbType.Int16, babitHeader.AllocationType)
            DbCommandWrapper.AddInParameter("@PeriodStart", DbType.DateTime, babitHeader.PeriodStart)
            DbCommandWrapper.AddInParameter("@PeriodEnd", DbType.DateTime, babitHeader.PeriodEnd)
            DbCommandWrapper.AddInParameter("@BabitStatus", DbType.Int16, babitHeader.BabitStatus)
            DbCommandWrapper.AddInParameter("@Location", DbType.AnsiString, babitHeader.Location)
            DbCommandWrapper.AddInParameter("@ProspectTarget", DbType.Int32, babitHeader.ProspectTarget)
            DbCommandWrapper.AddInParameter("@LuasArea", DbType.Int32, babitHeader.LuasArea)
            DbCommandWrapper.AddInParameter("@InvitationQty", DbType.Int32, babitHeader.InvitationQty)
            DbCommandWrapper.AddInParameter("@MarboxID", DbType.AnsiString, babitHeader.MarboxID)
            DbCommandWrapper.AddInParameter("@Notes", DbType.AnsiString, babitHeader.Notes)
            DbCommandWrapper.AddInParameter("@ApprovalNumber", DbType.AnsiString, babitHeader.ApprovalNumber)
            DbCommandWrapper.AddInParameter("@SPKTarget", DbType.Int32, babitHeader.SPKTarget)
            DbCommandWrapper.AddInParameter("@BabitDealerGroup", DbType.AnsiString, babitHeader.BabitDealerGroup)

            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, babitHeader.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, babitHeader.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, Me.GetRefObject(babitHeader.Dealer))
            DbCommandWrapper.AddInParameter("@DealerBranchID", DbType.Int32, Me.GetRefObject(babitHeader.DealerBranch))
            DbCommandWrapper.AddInParameter("@CityID", DbType.Int32, Me.GetRefObject(babitHeader.City))
            DbCommandWrapper.AddInParameter("@BabitMasterLocationID", DbType.Int32, Me.GetRefObject(babitHeader.BabitMasterLocation))
            DbCommandWrapper.AddInParameter("@BabitMasterEventTypeID", DbType.Int32, Me.GetRefObject(babitHeader.BabitMasterEventType))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As BabitHeader

            Dim babitHeader As BabitHeader = New BabitHeader

            babitHeader.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("BabitRegNumber")) Then babitHeader.BabitRegNumber = dr("BabitRegNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("BabitDealerNumber")) Then babitHeader.BabitDealerNumber = dr("BabitDealerNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("AllocationType")) Then babitHeader.AllocationType = CType(dr("AllocationType"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("PeriodStart")) Then babitHeader.PeriodStart = CType(dr("PeriodStart"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("PeriodEnd")) Then babitHeader.PeriodEnd = CType(dr("PeriodEnd"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("BabitStatus")) Then babitHeader.BabitStatus = CType(dr("BabitStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("Location")) Then babitHeader.Location = dr("Location").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ProspectTarget")) Then babitHeader.ProspectTarget = CType(dr("ProspectTarget"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("LuasArea")) Then babitHeader.LuasArea = CType(dr("LuasArea"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("InvitationQty")) Then babitHeader.InvitationQty = CType(dr("InvitationQty"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("MarboxID")) Then babitHeader.MarboxID = dr("MarboxID").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Notes")) Then babitHeader.Notes = dr("Notes").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ApprovalNumber")) Then babitHeader.ApprovalNumber = dr("ApprovalNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SPKTarget")) Then babitHeader.SPKTarget = CType(dr("SPKTarget"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("BabitDealerGroup")) Then babitHeader.BabitDealerGroup = dr("BabitDealerGroup").ToString

            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then babitHeader.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then babitHeader.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then babitHeader.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then babitHeader.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then babitHeader.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then
                babitHeader.Dealer = New Dealer(CType(dr("DealerID"), Short))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("DealerBranchID")) Then
                babitHeader.DealerBranch = New DealerBranch(CType(dr("DealerBranchID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("CityID")) Then
                babitHeader.City = New City(CType(dr("CityID"), Short))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("BabitMasterLocationID")) Then
                babitHeader.BabitMasterLocation = New BabitMasterLocation(CType(dr("BabitMasterLocationID"), Short))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("BabitMasterEventTypeID")) Then
                babitHeader.BabitMasterEventType = New BabitMasterEventType(CType(dr("BabitMasterEventTypeID"), Short))
            End If

            Return babitHeader

        End Function

        Private Sub SetTableName()

            If Not (GetType(BabitHeader) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(BabitHeader), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(BabitHeader).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

