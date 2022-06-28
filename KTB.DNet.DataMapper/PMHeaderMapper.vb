#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : PMHeader Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2007 
'// ---------------------
'// $History      : $
'// Generated on 8/1/2007 - 4:46:38 PM
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

    Public Class PMHeaderMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertPMHeader"
        Private m_UpdateStatement As String = "up_UpdatePMHeader"
        Private m_RetrieveStatement As String = "up_RetrievePMHeader"
        Private m_RetrieveListStatement As String = "up_RetrievePMHeaderList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeletePMHeader"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim pMHeader As PMHeader = Nothing
            While dr.Read

                pMHeader = Me.CreateObject(dr)

            End While

            Return pMHeader

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim pMHeaderList As ArrayList = New ArrayList

            While dr.Read
                Dim pMHeader As PMHeader = Me.CreateObject(dr)
                pMHeaderList.Add(pMHeader)
            End While

            Return pMHeaderList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim pMHeader As PMHeader = CType(obj, PMHeader)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, pMHeader.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim pMHeader As PMHeader = CType(obj, PMHeader)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@StandKM", DbType.Int32, pMHeader.StandKM)
            DbCommandWrapper.AddInParameter("@ServiceDate", DbType.DateTime, pMHeader.ServiceDate)
            DbCommandWrapper.AddInParameter("@ReleaseDate", DbType.DateTime, pMHeader.ReleaseDate)
            DbCommandWrapper.AddInParameter("@PMStatus", DbType.AnsiString, pMHeader.PMStatus)
            DbCommandWrapper.AddInParameter("@EntryType", DbType.AnsiString, pMHeader.EntryType)
            DbCommandWrapper.AddInParameter("@WorkOrderNumber", DbType.AnsiString, pMHeader.WorkOrderNumber)
            DbCommandWrapper.AddInParameter("@BookingNo", DbType.AnsiString, pMHeader.BookingNo)
            DbCommandWrapper.AddInParameter("@VisitType", DbType.AnsiString, pMHeader.VisitType)
            DbCommandWrapper.AddInParameter("@Remarks", DbType.AnsiString, pMHeader.Remarks)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, pMHeader.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, pMHeader.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@ChassisNumberID", DbType.Int32, Me.GetRefObject(pMHeader.ChassisMaster))
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, Me.GetRefObject(pMHeader.Dealer))
            DbCommandWrapper.AddInParameter("@DealerBranchID", DbType.Int32, Me.GetRefObject(pMHeader.DealerBranch))
            DbCommandWrapper.AddInParameter("@PMKindID", DbType.Int32, Me.GetRefObject(pMHeader.PMKind))

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

            Dim pMHeader As PMHeader = CType(obj, PMHeader)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, pMHeader.ID)
            DbCommandWrapper.AddInParameter("@StandKM", DbType.Int32, pMHeader.StandKM)
            DbCommandWrapper.AddInParameter("@ServiceDate", DbType.DateTime, pMHeader.ServiceDate)
            DbCommandWrapper.AddInParameter("@ReleaseDate", DbType.DateTime, pMHeader.ReleaseDate)
            DbCommandWrapper.AddInParameter("@PMStatus", DbType.AnsiString, pMHeader.PMStatus)
            DbCommandWrapper.AddInParameter("@EntryType", DbType.AnsiString, pMHeader.EntryType)
            DbCommandWrapper.AddInParameter("@WorkOrderNumber", DbType.AnsiString, pMHeader.WorkOrderNumber)
            DbCommandWrapper.AddInParameter("@BookingNo", DbType.AnsiString, pMHeader.BookingNo)
            DbCommandWrapper.AddInParameter("@VisitType", DbType.AnsiString, pMHeader.VisitType)
            DbCommandWrapper.AddInParameter("@Remarks", DbType.AnsiString, pMHeader.Remarks)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, pMHeader.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, pMHeader.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@ChassisNumberID", DbType.Int32, Me.GetRefObject(pMHeader.ChassisMaster))
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, Me.GetRefObject(pMHeader.Dealer))
            DbCommandWrapper.AddInParameter("@DealerBranchID", DbType.Int32, Me.GetRefObject(pMHeader.DealerBranch))
            DbCommandWrapper.AddInParameter("@PMKindID", DbType.Int32, Me.GetRefObject(pMHeader.PMKind))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As PMHeader

            Dim pMHeader As PMHeader = New PMHeader

            pMHeader.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("StandKM")) Then pMHeader.StandKM = CType(dr("StandKM"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("ServiceDate")) Then pMHeader.ServiceDate = CType(dr("ServiceDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("ReleaseDate")) Then pMHeader.ReleaseDate = CType(dr("ReleaseDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("PMStatus")) Then pMHeader.PMStatus = dr("PMStatus").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("EntryType")) Then pMHeader.EntryType = dr("EntryType").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("WorkOrderNumber")) Then pMHeader.WorkOrderNumber = dr("WorkOrderNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("BookingNo")) Then pMHeader.BookingNo = dr("BookingNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("VisitType")) Then pMHeader.VisitType = dr("VisitType").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Remarks")) Then pMHeader.Remarks = dr("Remarks").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then pMHeader.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then pMHeader.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then pMHeader.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then pMHeader.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then pMHeader.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("ChassisNumberID")) Then
                pMHeader.ChassisMaster = New ChassisMaster(CType(dr("ChassisNumberID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then
                pMHeader.Dealer = New Dealer(CType(dr("DealerID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("DealerBranchID")) Then
                pMHeader.DealerBranch = New DealerBranch(CType(dr("DealerBranchID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("PMKindID")) Then
                pMHeader.PMKind = New PMKind(CType(dr("PMKindID"), Integer))
            End If
            Return pMHeader

        End Function

        Private Sub SetTableName()

            If Not (GetType(PMHeader) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(PMHeader), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(PMHeader).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

