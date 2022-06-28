#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : EventLaporanPenjualan Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2009 
'// ---------------------
'// $History      : $
'// Generated on 11/20/2009 - 2:13:13 PM
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

    Public Class EventLaporanPenjualanMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertEventLaporanPenjualan"
        Private m_UpdateStatement As String = "up_UpdateEventLaporanPenjualan"
        Private m_RetrieveStatement As String = "up_RetrieveEventLaporanPenjualan"
        Private m_RetrieveListStatement As String = "up_RetrieveEventLaporanPenjualanList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteEventLaporanPenjualan"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim eventLaporanPenjualan As EventLaporanPenjualan = Nothing
            While dr.Read

                eventLaporanPenjualan = Me.CreateObject(dr)

            End While

            Return eventLaporanPenjualan

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim eventLaporanPenjualanList As ArrayList = New ArrayList

            While dr.Read
                Dim eventLaporanPenjualan As EventLaporanPenjualan = Me.CreateObject(dr)
                eventLaporanPenjualanList.Add(eventLaporanPenjualan)
            End While

            Return eventLaporanPenjualanList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim eventLaporanPenjualan As EventLaporanPenjualan = CType(obj, EventLaporanPenjualan)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, eventLaporanPenjualan.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim eventLaporanPenjualan As EventLaporanPenjualan = CType(obj, EventLaporanPenjualan)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, eventLaporanPenjualan.Description)
            DbCommandWrapper.AddInParameter("@Jumlah", DbType.Int32, eventLaporanPenjualan.Jumlah)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, eventLaporanPenjualan.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, eventLaporanPenjualan.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@VechileTypeID", DbType.Int16, Me.GetRefObject(eventLaporanPenjualan.VechileType))
            DbCommandWrapper.AddInParameter("@EventParameterID", DbType.Int32, Me.GetRefObject(eventLaporanPenjualan.EventParameter))
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, Me.GetRefObject(eventLaporanPenjualan.Dealer))

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

            Dim eventLaporanPenjualan As EventLaporanPenjualan = CType(obj, EventLaporanPenjualan)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, eventLaporanPenjualan.ID)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, eventLaporanPenjualan.Description)
            DbCommandWrapper.AddInParameter("@Jumlah", DbType.Int32, eventLaporanPenjualan.Jumlah)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, eventLaporanPenjualan.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, eventLaporanPenjualan.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@VechileTypeID", DbType.Int16, Me.GetRefObject(eventLaporanPenjualan.VechileType))
            DbCommandWrapper.AddInParameter("@EventParameterID", DbType.Int32, Me.GetRefObject(eventLaporanPenjualan.EventParameter))
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, Me.GetRefObject(eventLaporanPenjualan.Dealer))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As EventLaporanPenjualan

            Dim eventLaporanPenjualan As EventLaporanPenjualan = New EventLaporanPenjualan

            eventLaporanPenjualan.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Description")) Then eventLaporanPenjualan.Description = dr("Description").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Jumlah")) Then eventLaporanPenjualan.Jumlah = CType(dr("Jumlah"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then eventLaporanPenjualan.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then eventLaporanPenjualan.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then eventLaporanPenjualan.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then eventLaporanPenjualan.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then eventLaporanPenjualan.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("VechileTypeID")) Then
                eventLaporanPenjualan.VechileType = New VechileType(CType(dr("VechileTypeID"), Short))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("EventParameterID")) Then
                eventLaporanPenjualan.EventParameter = New EventParameter(CType(dr("EventParameterID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then
                eventLaporanPenjualan.Dealer = New Dealer(CType(dr("DealerID"), Short))
            End If

            Return eventLaporanPenjualan

        End Function

        Private Sub SetTableName()

            If Not (GetType(EventLaporanPenjualan) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(EventLaporanPenjualan), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(EventLaporanPenjualan).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

