#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : EstimationEquipHeader Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2009 
'// ---------------------
'// $History      : $
'// Generated on 8/19/2009 - 12:54:08
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

    Public Class EstimationEquipHeaderMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertEstimationEquipHeader"
        Private m_UpdateStatement As String = "up_UpdateEstimationEquipHeader"
        Private m_RetrieveStatement As String = "up_RetrieveEstimationEquipHeader"
        Private m_RetrieveListStatement As String = "up_RetrieveEstimationEquipHeaderList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteEstimationEquipHeader"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim estimationEquipHeader As EstimationEquipHeader = Nothing
            While dr.Read

                estimationEquipHeader = Me.CreateObject(dr)

            End While

            Return estimationEquipHeader

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim estimationEquipHeaderList As ArrayList = New ArrayList

            While dr.Read
                Dim estimationEquipHeader As EstimationEquipHeader = Me.CreateObject(dr)
                estimationEquipHeaderList.Add(estimationEquipHeader)
            End While

            Return estimationEquipHeaderList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim estimationEquipHeader As EstimationEquipHeader = CType(obj, EstimationEquipHeader)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, estimationEquipHeader.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim estimationEquipHeader As EstimationEquipHeader = CType(obj, EstimationEquipHeader)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@EstimationNumber", DbType.AnsiString, estimationEquipHeader.EstimationNumber)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, estimationEquipHeader.Status)
            DbCommandWrapper.AddInParameter("@DMSPRNo", DbType.AnsiString, estimationEquipHeader.DMSPRNo)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, estimationEquipHeader.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DBCommandWrapper.AddInParameter("@LastUpdatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdatedTime", DbType.DateTime, estimationEquipHeader.LastUpdatedTime)

            DBCommandWrapper.AddInParameter("@DealerID", DbType.Int16, Me.GetRefObject(estimationEquipHeader.Dealer))
            DbCommandWrapper.AddInParameter("@DepositBKewajibanHeaderID", DbType.Int16, Me.GetRefObject(estimationEquipHeader.DepositBKewajibanHeader))

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

            Dim estimationEquipHeader As EstimationEquipHeader = CType(obj, EstimationEquipHeader)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, estimationEquipHeader.ID)
            DbCommandWrapper.AddInParameter("@EstimationNumber", DbType.AnsiString, estimationEquipHeader.EstimationNumber)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, estimationEquipHeader.Status)
            DbCommandWrapper.AddInParameter("@DMSPRNo", DbType.AnsiString, estimationEquipHeader.DMSPRNo)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, estimationEquipHeader.RowStatus)
            'DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, estimationEquipHeader.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DBCommandWrapper.AddInParameter("@LastUpdatedBy", DbType.AnsiString, User)
            'DBCommandWrapper.AddInParameter("@LastUpdatedTime", DbType.DateTime, DateTime.Now)

            DBCommandWrapper.AddInParameter("@DealerID", DbType.Int16, Me.GetRefObject(estimationEquipHeader.Dealer))
            DbCommandWrapper.AddInParameter("@DepositBKewajibanHeaderID", DbType.Int16, Me.GetRefObject(estimationEquipHeader.DepositBKewajibanHeader))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As EstimationEquipHeader

            Dim estimationEquipHeader As EstimationEquipHeader = New EstimationEquipHeader

            estimationEquipHeader.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("EstimationNumber")) Then estimationEquipHeader.EstimationNumber = dr("EstimationNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then estimationEquipHeader.Status = CType(dr("Status"), Byte)
            If Not dr.IsDBNull(dr.GetOrdinal("DMSPRNo")) Then estimationEquipHeader.DMSPRNo = dr("DMSPRNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then estimationEquipHeader.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then estimationEquipHeader.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then estimationEquipHeader.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdatedBy")) Then estimationEquipHeader.LastUpdatedBy = dr("LastUpdatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdatedTime")) Then estimationEquipHeader.LastUpdatedTime = CType(dr("LastUpdatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then
                estimationEquipHeader.Dealer = New Dealer(CType(dr("DealerID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("DepositBKewajibanHeaderID")) Then
                estimationEquipHeader.DepositBKewajibanHeader = New DepositBKewajibanHeader(CType(dr("DepositBKewajibanHeaderID"), Integer))
            End If

            Return estimationEquipHeader

        End Function

        Private Sub SetTableName()

            If Not (GetType(EstimationEquipHeader) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(EstimationEquipHeader), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(EstimationEquipHeader).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

