#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : EventProposalDetail Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2009 
'// ---------------------
'// $History      : $
'// Generated on 9/4/2009 - 1:55:45 PM
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

    Public Class EventProposalDetailMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertEventProposalDetail"
        Private m_UpdateStatement As String = "up_UpdateEventProposalDetail"
        Private m_RetrieveStatement As String = "up_RetrieveEventProposalDetail"
        Private m_RetrieveListStatement As String = "up_RetrieveEventProposalDetailList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteEventProposalDetail"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim eventProposalDetail As EventProposalDetail = Nothing
            While dr.Read

                eventProposalDetail = Me.CreateObject(dr)

            End While

            Return eventProposalDetail

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim eventProposalDetailList As ArrayList = New ArrayList

            While dr.Read
                Dim eventProposalDetail As EventProposalDetail = Me.CreateObject(dr)
                eventProposalDetailList.Add(eventProposalDetail)
            End While

            Return eventProposalDetailList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim eventProposalDetail As EventProposalDetail = CType(obj, EventProposalDetail)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, eventProposalDetail.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim eventProposalDetail As EventProposalDetail = CType(obj, EventProposalDetail)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@Item", DbType.AnsiString, eventProposalDetail.Item)
            DbCommandWrapper.AddInParameter("@Quantity", DbType.Int32, eventProposalDetail.Quantity)
            DbCommandWrapper.AddInParameter("@UnitCost", DbType.Currency, eventProposalDetail.UnitCost)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, eventProposalDetail.Description)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, eventProposalDetail.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, eventProposalDetail.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@EventProposalID", DbType.Int32, Me.GetRefObject(eventProposalDetail.EventProposal))
            DbCommandWrapper.AddInParameter("@EventActivityTypeID", DbType.Int32, Me.GetRefObject(eventProposalDetail.EventActivityType))
            DbCommandWrapper.AddInParameter("@VechileTypeID", DbType.Int16, Me.GetRefObject(eventProposalDetail.VechileType))

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

            Dim eventProposalDetail As EventProposalDetail = CType(obj, EventProposalDetail)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, eventProposalDetail.ID)
            DbCommandWrapper.AddInParameter("@Item", DbType.AnsiString, eventProposalDetail.Item)
            DbCommandWrapper.AddInParameter("@Quantity", DbType.Int32, eventProposalDetail.Quantity)
            DbCommandWrapper.AddInParameter("@UnitCost", DbType.Currency, eventProposalDetail.UnitCost)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, eventProposalDetail.Description)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, eventProposalDetail.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, eventProposalDetail.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@EventProposalID", DbType.Int32, Me.GetRefObject(eventProposalDetail.EventProposal))
            DbCommandWrapper.AddInParameter("@EventActivityTypeID", DbType.Int32, Me.GetRefObject(eventProposalDetail.EventActivityType))
            DbCommandWrapper.AddInParameter("@VechileTypeID", DbType.Int16, Me.GetRefObject(eventProposalDetail.VechileType))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As EventProposalDetail

            Dim eventProposalDetail As EventProposalDetail = New EventProposalDetail

            eventProposalDetail.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Item")) Then eventProposalDetail.Item = dr("Item").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Quantity")) Then eventProposalDetail.Quantity = CType(dr("Quantity"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("UnitCost")) Then eventProposalDetail.UnitCost = CType(dr("UnitCost"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("Description")) Then eventProposalDetail.Description = dr("Description").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then eventProposalDetail.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then eventProposalDetail.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then eventProposalDetail.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then eventProposalDetail.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then eventProposalDetail.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("EventProposalID")) Then
                eventProposalDetail.EventProposal = New EventProposal(CType(dr("EventProposalID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("EventActivityTypeID")) Then
                eventProposalDetail.EventActivityType = New EventActivityType(CType(dr("EventActivityTypeID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("VechileTypeID")) Then
                eventProposalDetail.VechileType = New VechileType(CType(dr("VechileTypeID"), Short))
            End If

            Return eventProposalDetail

        End Function

        Private Sub SetTableName()

            If Not (GetType(EventProposalDetail) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(EventProposalDetail), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(EventProposalDetail).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

