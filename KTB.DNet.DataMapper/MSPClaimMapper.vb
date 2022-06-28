
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : MSPClaim Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 1/22/2018 - 1:38:51 PM
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

    Public Class MSPClaimMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertMSPClaim"
        Private m_UpdateStatement As String = "up_UpdateMSPClaim"
        Private m_RetrieveStatement As String = "up_RetrieveMSPClaim"
        Private m_RetrieveListStatement As String = "up_RetrieveMSPClaimList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteMSPClaim"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim mSPClaim As MSPClaim = Nothing
            While dr.Read

                mSPClaim = Me.CreateObject(dr)

            End While

            Return mSPClaim

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim mSPClaimList As ArrayList = New ArrayList

            While dr.Read
                Dim mSPClaim As MSPClaim = Me.CreateObject(dr)
                mSPClaimList.Add(mSPClaim)
            End While

            Return mSPClaimList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim mSPClaim As MSPClaim = CType(obj, MSPClaim)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, mSPClaim.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim mSPClaim As MSPClaim = CType(obj, MSPClaim)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, mSPClaim.Dealer.ID)
            DbCommandWrapper.AddInParameter("@PMHeaderID", DbType.Int32, Me.GetRefObject(mSPClaim.PMHeader))
            DbCommandWrapper.AddInParameter("@MSPRegistrationHistoryID", DbType.Int32, Me.GetRefObject(mSPClaim.MSPRegistrationHistory))
            DbCommandWrapper.AddInParameter("@ClaimNumber", DbType.AnsiString, mSPClaim.ClaimNumber)
            DbCommandWrapper.AddInParameter("@ClaimDate", DbType.DateTime, mSPClaim.ClaimDate)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, mSPClaim.Status)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, mSPClaim.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, mSPClaim.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@StandKM", DbType.Int32, mSPClaim.StandKM)
            DbCommandWrapper.AddInParameter("@ServiceDate", DbType.DateTime, mSPClaim.ServiceDate)
            DbCommandWrapper.AddInParameter("@ReleaseDate", DbType.DateTime, mSPClaim.ReleaseDate)
            DbCommandWrapper.AddInParameter("@VisitType", DbType.AnsiString, mSPClaim.VisitType)
            DbCommandWrapper.AddInParameter("@Remarks", DbType.AnsiString, mSPClaim.Remarks)
            DbCommandWrapper.AddInParameter("@ChassisNumberID", DbType.Int32, Me.GetRefObject(mSPClaim.ChassisMaster))
            DbCommandWrapper.AddInParameter("@PMKindID", DbType.Int32, Me.GetRefObject(mSPClaim.PMKind))

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

            Dim mSPClaim As MSPClaim = CType(obj, MSPClaim)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, mSPClaim.ID)
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, mSPClaim.Dealer.ID)
            DbCommandWrapper.AddInParameter("@PMHeaderID", DbType.Int32, Me.GetRefObject(mSPClaim.PMHeader))
            DbCommandWrapper.AddInParameter("@MSPRegistrationHistoryID", DbType.Int32, Me.GetRefObject(mSPClaim.MSPRegistrationHistory))
            DbCommandWrapper.AddInParameter("@ClaimNumber", DbType.AnsiString, mSPClaim.ClaimNumber)
            DbCommandWrapper.AddInParameter("@ClaimDate", DbType.DateTime, mSPClaim.ClaimDate)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, mSPClaim.Status)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, mSPClaim.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, mSPClaim.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@StandKM", DbType.Int32, mSPClaim.StandKM)
            DbCommandWrapper.AddInParameter("@ServiceDate", DbType.DateTime, mSPClaim.ServiceDate)
            DbCommandWrapper.AddInParameter("@ReleaseDate", DbType.DateTime, mSPClaim.ReleaseDate)
            DbCommandWrapper.AddInParameter("@VisitType", DbType.AnsiString, mSPClaim.VisitType)
            DbCommandWrapper.AddInParameter("@Remarks", DbType.AnsiString, mSPClaim.Remarks)
            DbCommandWrapper.AddInParameter("@ChassisNumberID", DbType.Int32, Me.GetRefObject(mSPClaim.ChassisMaster))
            DbCommandWrapper.AddInParameter("@PMKindID", DbType.Int32, Me.GetRefObject(mSPClaim.PMKind))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As MSPClaim

            Dim mSPClaim As MSPClaim = New MSPClaim

            mSPClaim.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then
                mSPClaim.Dealer = New Dealer(CType(dr("DealerID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("PMHeaderID")) Then
                mSPClaim.PMHeader = New PMHeader(CType(dr("PMHeaderID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("MSPRegistrationHistoryID")) Then
                mSPClaim.MSPRegistrationHistory = New MSPRegistrationHistory(CType(dr("MSPRegistrationHistoryID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("ClaimNumber")) Then mSPClaim.ClaimNumber = dr("ClaimNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ClaimDate")) Then mSPClaim.ClaimDate = CType(dr("ClaimDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then mSPClaim.Status = CType(dr("Status"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then mSPClaim.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then mSPClaim.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then mSPClaim.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then mSPClaim.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then mSPClaim.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("StandKM")) Then mSPClaim.StandKM = CType(dr("StandKM"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("ServiceDate")) Then mSPClaim.ServiceDate = CType(dr("ServiceDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("ReleaseDate")) Then mSPClaim.ReleaseDate = CType(dr("ReleaseDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("VisitType")) Then mSPClaim.VisitType = dr("VisitType").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Remarks")) Then mSPClaim.Remarks = dr("Remarks").ToString()
            If Not dr.IsDBNull(dr.GetOrdinal("ChassisNumberID")) Then
                mSPClaim.ChassisMaster = New ChassisMaster(CType(dr("ChassisNumberID"), Integer))
            End If

            If Not dr.IsDBNull(dr.GetOrdinal("PMKindID")) Then
                mSPClaim.PMKind = New PMKind(CType(dr("PMKindID"), Integer))
            End If
            Return mSPClaim

        End Function

        Private Sub SetTableName()

            If Not (GetType(MSPClaim) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(MSPClaim), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(MSPClaim).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

