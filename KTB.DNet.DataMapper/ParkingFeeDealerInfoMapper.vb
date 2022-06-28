
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : ParkingFeeDealerInfo Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2012 
'// ---------------------
'// $History      : $
'// Generated on 3/9/2012 - 4:28:08 PM
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

    Public Class ParkingFeeDealerInfoMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertParkingFeeDealerInfo"
        Private m_UpdateStatement As String = "up_UpdateParkingFeeDealerInfo"
        Private m_RetrieveStatement As String = "up_RetrieveParkingFeeDealerInfo"
        Private m_RetrieveListStatement As String = "up_RetrieveParkingFeeDealerInfoList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteParkingFeeDealerInfo"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim parkingFeeDealerInfo As ParkingFeeDealerInfo = Nothing
            While dr.Read

                parkingFeeDealerInfo = Me.CreateObject(dr)

            End While

            Return parkingFeeDealerInfo

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim parkingFeeDealerInfoList As ArrayList = New ArrayList

            While dr.Read
                Dim parkingFeeDealerInfo As ParkingFeeDealerInfo = Me.CreateObject(dr)
                parkingFeeDealerInfoList.Add(parkingFeeDealerInfo)
            End While

            Return parkingFeeDealerInfoList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim parkingFeeDealerInfo As ParkingFeeDealerInfo = CType(obj, ParkingFeeDealerInfo)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int16, parkingFeeDealerInfo.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim parkingFeeDealerInfo As ParkingFeeDealerInfo = CType(obj, ParkingFeeDealerInfo)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int16, 2)
            DbCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, parkingFeeDealerInfo.DealerCode)
            DbCommandWrapper.AddInParameter("@DealerName", DbType.AnsiString, parkingFeeDealerInfo.DealerName)
            DbCommandWrapper.AddInParameter("@Address", DbType.AnsiString, parkingFeeDealerInfo.Address)
            DbCommandWrapper.AddInParameter("@City", DbType.AnsiString, parkingFeeDealerInfo.City)
            DbCommandWrapper.AddInParameter("@Owners", DbType.AnsiString, parkingFeeDealerInfo.Owners)
            DbCommandWrapper.AddInParameter("@SearchTerm1", DbType.AnsiString, parkingFeeDealerInfo.SearchTerm1)
            DbCommandWrapper.AddInParameter("@GroupName", DbType.AnsiString, parkingFeeDealerInfo.GroupName)
            DBCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, parkingFeeDealerInfo.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, parkingFeeDealerInfo.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, Me.GetRefObject(parkingFeeDealerInfo.Dealer))

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

            Dim parkingFeeDealerInfo As ParkingFeeDealerInfo = CType(obj, ParkingFeeDealerInfo)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int16, parkingFeeDealerInfo.ID)
            DbCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, parkingFeeDealerInfo.DealerCode)
            DbCommandWrapper.AddInParameter("@DealerName", DbType.AnsiString, parkingFeeDealerInfo.DealerName)
            DbCommandWrapper.AddInParameter("@Address", DbType.AnsiString, parkingFeeDealerInfo.Address)
            DbCommandWrapper.AddInParameter("@City", DbType.AnsiString, parkingFeeDealerInfo.City)
            DbCommandWrapper.AddInParameter("@Owners", DbType.AnsiString, parkingFeeDealerInfo.Owners)
            DbCommandWrapper.AddInParameter("@SearchTerm1", DbType.AnsiString, parkingFeeDealerInfo.SearchTerm1)
            DbCommandWrapper.AddInParameter("@GroupName", DbType.AnsiString, parkingFeeDealerInfo.GroupName)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Byte, parkingFeeDealerInfo.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, parkingFeeDealerInfo.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, Me.GetRefObject(parkingFeeDealerInfo.Dealer))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As ParkingFeeDealerInfo

            Dim parkingFeeDealerInfo As ParkingFeeDealerInfo = New ParkingFeeDealerInfo

            parkingFeeDealerInfo.ID = CType(dr("ID"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerCode")) Then parkingFeeDealerInfo.DealerCode = dr("DealerCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DealerName")) Then parkingFeeDealerInfo.DealerName = dr("DealerName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Address")) Then parkingFeeDealerInfo.Address = dr("Address").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("City")) Then parkingFeeDealerInfo.City = dr("City").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Owners")) Then parkingFeeDealerInfo.Owners = dr("Owners").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SearchTerm1")) Then parkingFeeDealerInfo.SearchTerm1 = dr("SearchTerm1").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("GroupName")) Then parkingFeeDealerInfo.GroupName = dr("GroupName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then parkingFeeDealerInfo.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then parkingFeeDealerInfo.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then parkingFeeDealerInfo.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then parkingFeeDealerInfo.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then parkingFeeDealerInfo.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then
                parkingFeeDealerInfo.Dealer = New Dealer(CType(dr("DealerID"), Short))
            End If

            Return parkingFeeDealerInfo

        End Function

        Private Sub SetTableName()

            If Not (GetType(ParkingFeeDealerInfo) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(ParkingFeeDealerInfo), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(ParkingFeeDealerInfo).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

