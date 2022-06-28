#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : V_EventProposalDetail Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2009 
'// ---------------------
'// $History      : $
'// Generated on 9/2/2009 - 5:07:11 PM
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

    Public Class V_EventProposalDetailMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertV_EventProposalDetail"
        Private m_UpdateStatement As String = "up_UpdateV_EventProposalDetail"
        Private m_RetrieveStatement As String = "up_RetrieveV_EventProposalDetail"
        Private m_RetrieveListStatement As String = "up_RetrieveV_EventProposalDetailList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteV_EventProposalDetail"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim v_EventProposalDetail As V_EventProposalDetail = Nothing
            While dr.Read

                v_EventProposalDetail = Me.CreateObject(dr)

            End While

            Return v_EventProposalDetail

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim v_EventProposalDetailList As ArrayList = New ArrayList

            While dr.Read
                Dim v_EventProposalDetail As V_EventProposalDetail = Me.CreateObject(dr)
                v_EventProposalDetailList.Add(v_EventProposalDetail)
            End While

            Return v_EventProposalDetailList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim v_EventProposalDetail As V_EventProposalDetail = CType(obj, V_EventProposalDetail)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, v_EventProposalDetail.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim v_EventProposalDetail As V_EventProposalDetail = CType(obj, V_EventProposalDetail)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@EventProposalID", DbType.Int32, v_EventProposalDetail.EventProposalID)
            DbCommandWrapper.AddInParameter("@EventActivityTypeID", DbType.Int32, v_EventProposalDetail.EventActivityTypeID)
            DbCommandWrapper.AddInParameter("@VechileTypeID", DbType.Int16, v_EventProposalDetail.VechileTypeID)
            DbCommandWrapper.AddInParameter("@Item", DbType.AnsiString, v_EventProposalDetail.Item)
            DbCommandWrapper.AddInParameter("@Quantity", DbType.Int32, v_EventProposalDetail.Quantity)
            DbCommandWrapper.AddInParameter("@UnitCost", DbType.Currency, v_EventProposalDetail.UnitCost)
            DbCommandWrapper.AddInParameter("@TotalCost", DbType.Currency, v_EventProposalDetail.TotalCost)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, v_EventProposalDetail.Description)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, v_EventProposalDetail.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, v_EventProposalDetail.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


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

            Dim v_EventProposalDetail As V_EventProposalDetail = CType(obj, V_EventProposalDetail)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, v_EventProposalDetail.ID)
            DbCommandWrapper.AddInParameter("@EventProposalID", DbType.Int32, v_EventProposalDetail.EventProposalID)
            DbCommandWrapper.AddInParameter("@EventActivityTypeID", DbType.Int32, v_EventProposalDetail.EventActivityTypeID)
            DbCommandWrapper.AddInParameter("@VechileTypeID", DbType.Int16, v_EventProposalDetail.VechileTypeID)
            DbCommandWrapper.AddInParameter("@Item", DbType.AnsiString, v_EventProposalDetail.Item)
            DbCommandWrapper.AddInParameter("@Quantity", DbType.Int32, v_EventProposalDetail.Quantity)
            DbCommandWrapper.AddInParameter("@UnitCost", DbType.Currency, v_EventProposalDetail.UnitCost)
            DbCommandWrapper.AddInParameter("@TotalCost", DbType.Currency, v_EventProposalDetail.TotalCost)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, v_EventProposalDetail.Description)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, v_EventProposalDetail.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, v_EventProposalDetail.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As V_EventProposalDetail

            Dim v_EventProposalDetail As V_EventProposalDetail = New V_EventProposalDetail

            v_EventProposalDetail.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("EventProposalID")) Then v_EventProposalDetail.EventProposalID = CType(dr("EventProposalID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("EventActivityTypeID")) Then v_EventProposalDetail.EventActivityTypeID = CType(dr("EventActivityTypeID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("VechileTypeID")) Then v_EventProposalDetail.VechileTypeID = CType(dr("VechileTypeID"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("Item")) Then v_EventProposalDetail.Item = dr("Item").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Quantity")) Then v_EventProposalDetail.Quantity = CType(dr("Quantity"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("UnitCost")) Then v_EventProposalDetail.UnitCost = CType(dr("UnitCost"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("TotalCost")) Then v_EventProposalDetail.TotalCost = CType(dr("TotalCost"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("Description")) Then v_EventProposalDetail.Description = dr("Description").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then v_EventProposalDetail.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then v_EventProposalDetail.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then v_EventProposalDetail.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then v_EventProposalDetail.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then v_EventProposalDetail.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            Return v_EventProposalDetail

        End Function

        Private Sub SetTableName()

            If Not (GetType(V_EventProposalDetail) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(V_EventProposalDetail), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(V_EventProposalDetail).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

