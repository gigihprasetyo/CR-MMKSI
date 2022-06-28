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
    Public Class SparePartForecastHeaderMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertSparePartForecastHeader"
        Private m_UpdateStatement As String = "up_UpdateSparePartForecastHeader"
        Private m_RetrieveStatement As String = "up_RetrieveSparePartForecastHeader"
        Private m_RetrieveListStatement As String = "up_RetrieveSparePartForecastHeaderList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteSparePartForecastHeader"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim sparePartForecastHeader As SparePartForecastHeader = Nothing
            While dr.Read

                sparePartForecastHeader = Me.CreateObject(dr)

            End While

            Return sparePartForecastHeader

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim sparePartForecastHeaderList As ArrayList = New ArrayList

            While dr.Read
                Dim sparePartForecastHeader As SparePartForecastHeader = Me.CreateObject(dr)
                sparePartForecastHeaderList.Add(sparePartForecastHeader)
            End While

            Return sparePartForecastHeaderList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim sparePartForecastHeader As SparePartForecastHeader = CType(obj, SparePartForecastHeader)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, sparePartForecastHeader.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim sparePartForecastHeader As SparePartForecastHeader = CType(obj, SparePartForecastHeader)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)

            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@PoNumber", DbType.AnsiString, sparePartForecastHeader.PoNumber)
            DbCommandWrapper.AddInParameter("@PoDate", DbType.DateTime, sparePartForecastHeader.PoDate)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, sparePartForecastHeader.Status)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, sparePartForecastHeader.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdatedBy", DbType.AnsiString, sparePartForecastHeader.LastUpdatedBy)
            'DbCommandWrapper.AddInParameter("@LastUpdatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@DMSPRNo", DbType.AnsiString, sparePartForecastHeader.DMSPRNo)

            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, Me.GetRefObject(sparePartForecastHeader.Dealer))
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

            Dim sparePartForecastHeader As SparePartForecastHeader = CType(obj, SparePartForecastHeader)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, sparePartForecastHeader.ID)
            DbCommandWrapper.AddInParameter("@PoNumber", DbType.AnsiString, sparePartForecastHeader.PoNumber)
            DbCommandWrapper.AddInParameter("@PoDate", DbType.DateTime, sparePartForecastHeader.PoDate)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, sparePartForecastHeader.Status)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, sparePartForecastHeader.RowStatus)
            'DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, sparePartForecastHeader.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdatedTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, Me.GetRefObject(sparePartForecastHeader.Dealer))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As SparePartForecastHeader

            Dim sparePartForecastHeader As SparePartForecastHeader = New SparePartForecastHeader

            sparePartForecastHeader.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("PoNumber")) Then sparePartForecastHeader.PoNumber = dr("PoNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PoDate")) Then sparePartForecastHeader.PoDate = CType(dr("PoDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then sparePartForecastHeader.Status = CType(dr("Status"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then sparePartForecastHeader.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then sparePartForecastHeader.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then sparePartForecastHeader.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdatedBy")) Then sparePartForecastHeader.LastUpdatedBy = dr("LastUpdatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdatedTime")) Then sparePartForecastHeader.LastUpdatedTime = CType(dr("LastUpdatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then
                sparePartForecastHeader.Dealer = New Dealer(CType(dr("DealerID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("DMSPRNo")) Then sparePartForecastHeader.DMSPRNo = dr("DMSPRNo").ToString

            Return sparePartForecastHeader

        End Function

        Private Sub SetTableName()

            If Not (GetType(SparePartForecastHeader) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(SparePartForecastHeader), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(SparePartForecastHeader).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

    End Class
End Namespace
