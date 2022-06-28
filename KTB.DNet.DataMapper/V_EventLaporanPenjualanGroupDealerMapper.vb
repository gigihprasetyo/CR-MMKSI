#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : V_EventLaporanPenjualanGroupDealer Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2009 
'// ---------------------
'// $History      : $
'// Generated on 11/20/2009 - 3:33:10 PM
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

    Public Class V_EventLaporanPenjualanGroupDealerMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertV_EventLaporanPenjualanGroupDealer"
        Private m_UpdateStatement As String = "up_UpdateV_EventLaporanPenjualanGroupDealer"
        Private m_RetrieveStatement As String = "up_RetrieveV_EventLaporanPenjualanGroupDealer"
        Private m_RetrieveListStatement As String = "up_RetrieveV_EventLaporanPenjualanGroupDealerList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteV_EventLaporanPenjualanGroupDealer"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim v_EventLaporanPenjualanGroupDealer As V_EventLaporanPenjualanGroupDealer = Nothing
            While dr.Read

                v_EventLaporanPenjualanGroupDealer = Me.CreateObject(dr)

            End While

            Return v_EventLaporanPenjualanGroupDealer

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim v_EventLaporanPenjualanGroupDealerList As ArrayList = New ArrayList

            While dr.Read
                Dim v_EventLaporanPenjualanGroupDealer As V_EventLaporanPenjualanGroupDealer = Me.CreateObject(dr)
                v_EventLaporanPenjualanGroupDealerList.Add(v_EventLaporanPenjualanGroupDealer)
            End While

            Return v_EventLaporanPenjualanGroupDealerList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim v_EventLaporanPenjualanGroupDealer As V_EventLaporanPenjualanGroupDealer = CType(obj, V_EventLaporanPenjualanGroupDealer)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, v_EventLaporanPenjualanGroupDealer.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim v_EventLaporanPenjualanGroupDealer As V_EventLaporanPenjualanGroupDealer = CType(obj, V_EventLaporanPenjualanGroupDealer)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@EventParameterID", DbType.Int32, v_EventLaporanPenjualanGroupDealer.EventParameterID)
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, v_EventLaporanPenjualanGroupDealer.DealerID)
            DbCommandWrapper.AddInParameter("@VechileTypeID", DbType.Int16, v_EventLaporanPenjualanGroupDealer.VechileTypeID)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, v_EventLaporanPenjualanGroupDealer.Description)
            DbCommandWrapper.AddInParameter("@Jumlah", DbType.Int32, v_EventLaporanPenjualanGroupDealer.Jumlah)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, v_EventLaporanPenjualanGroupDealer.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, v_EventLaporanPenjualanGroupDealer.LastUpdateBy)
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

            Dim v_EventLaporanPenjualanGroupDealer As V_EventLaporanPenjualanGroupDealer = CType(obj, V_EventLaporanPenjualanGroupDealer)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, v_EventLaporanPenjualanGroupDealer.ID)
            DbCommandWrapper.AddInParameter("@EventParameterID", DbType.Int32, v_EventLaporanPenjualanGroupDealer.EventParameterID)
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, v_EventLaporanPenjualanGroupDealer.DealerID)
            DbCommandWrapper.AddInParameter("@VechileTypeID", DbType.Int16, v_EventLaporanPenjualanGroupDealer.VechileTypeID)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, v_EventLaporanPenjualanGroupDealer.Description)
            DbCommandWrapper.AddInParameter("@Jumlah", DbType.Int32, v_EventLaporanPenjualanGroupDealer.Jumlah)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, v_EventLaporanPenjualanGroupDealer.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, v_EventLaporanPenjualanGroupDealer.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As V_EventLaporanPenjualanGroupDealer

            Dim v_EventLaporanPenjualanGroupDealer As V_EventLaporanPenjualanGroupDealer = New V_EventLaporanPenjualanGroupDealer

            v_EventLaporanPenjualanGroupDealer.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("EventParameterID")) Then v_EventLaporanPenjualanGroupDealer.EventParameterID = CType(dr("EventParameterID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then v_EventLaporanPenjualanGroupDealer.DealerID = CType(dr("DealerID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("VechileTypeID")) Then v_EventLaporanPenjualanGroupDealer.VechileTypeID = CType(dr("VechileTypeID"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("Description")) Then v_EventLaporanPenjualanGroupDealer.Description = dr("Description").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Jumlah")) Then v_EventLaporanPenjualanGroupDealer.Jumlah = CType(dr("Jumlah"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then v_EventLaporanPenjualanGroupDealer.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then v_EventLaporanPenjualanGroupDealer.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then v_EventLaporanPenjualanGroupDealer.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then v_EventLaporanPenjualanGroupDealer.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then v_EventLaporanPenjualanGroupDealer.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            Return v_EventLaporanPenjualanGroupDealer

        End Function

        Private Sub SetTableName()

            If Not (GetType(V_EventLaporanPenjualanGroupDealer) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(V_EventLaporanPenjualanGroupDealer), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(V_EventLaporanPenjualanGroupDealer).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

