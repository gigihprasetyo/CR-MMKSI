
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : V_RekapDOHelper Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2009 
'// ---------------------
'// $History      : $
'// Generated on 5/1/2009 - 11:14:12 AM
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

    Public Class V_RekapDOHelperMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertV_RekapDOHelper"
        Private m_UpdateStatement As String = "up_UpdateV_RekapDOHelper"
        Private m_RetrieveStatement As String = "up_RetrieveV_RekapDOHelper"
        Private m_RetrieveListStatement As String = "up_RetrieveV_RekapDOHelperList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteV_RekapDOHelper"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim v_RekapDOHelper As V_RekapDOHelper = Nothing
            While dr.Read

                v_RekapDOHelper = Me.CreateObject(dr)

            End While

            Return v_RekapDOHelper

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim v_RekapDOHelperList As ArrayList = New ArrayList

            While dr.Read
                Dim v_RekapDOHelper As V_RekapDOHelper = Me.CreateObject(dr)
                v_RekapDOHelperList.Add(v_RekapDOHelper)
            End While

            Return v_RekapDOHelperList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim v_RekapDOHelper As V_RekapDOHelper = CType(obj, V_RekapDOHelper)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, v_RekapDOHelper.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim v_RekapDOHelper As V_RekapDOHelper = CType(obj, V_RekapDOHelper)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, v_RekapDOHelper.DealerCode)
            DbCommandWrapper.AddInParameter("@VechileTypeCode", DbType.AnsiString, v_RekapDOHelper.VechileTypeCode)
            DbCommandWrapper.AddInParameter("@VechileColorID", DbType.Int16, v_RekapDOHelper.VechileColorID)
            DbCommandWrapper.AddInParameter("@DODate", DbType.DateTime, v_RekapDOHelper.DODate)
            DbCommandWrapper.AddInParameter("@GIDate", DbType.DateTime, v_RekapDOHelper.GIDate)
            DbCommandWrapper.AddInParameter("@ParkingAmount", DbType.Currency, v_RekapDOHelper.ParkingAmount)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, v_RekapDOHelper.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, v_RekapDOHelper.LastUpdateBy)
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

            Dim v_RekapDOHelper As V_RekapDOHelper = CType(obj, V_RekapDOHelper)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, v_RekapDOHelper.ID)
            DbCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, v_RekapDOHelper.DealerCode)
            DbCommandWrapper.AddInParameter("@VechileTypeCode", DbType.AnsiString, v_RekapDOHelper.VechileTypeCode)
            DbCommandWrapper.AddInParameter("@VechileColorID", DbType.Int16, v_RekapDOHelper.VechileColorID)
            DbCommandWrapper.AddInParameter("@DODate", DbType.DateTime, v_RekapDOHelper.DODate)
            DbCommandWrapper.AddInParameter("@GIDate", DbType.DateTime, v_RekapDOHelper.GIDate)
            DbCommandWrapper.AddInParameter("@ParkingAmount", DbType.Currency, v_RekapDOHelper.ParkingAmount)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, v_RekapDOHelper.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, v_RekapDOHelper.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As V_RekapDOHelper

            Dim v_RekapDOHelper As V_RekapDOHelper = New V_RekapDOHelper

            v_RekapDOHelper.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerCode")) Then v_RekapDOHelper.DealerCode = dr("DealerCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("VechileTypeCode")) Then v_RekapDOHelper.VechileTypeCode = dr("VechileTypeCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("VechileColorID")) Then v_RekapDOHelper.VechileColorID = CType(dr("VechileColorID"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("DODate")) Then v_RekapDOHelper.DODate = CType(dr("DODate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("GIDate")) Then v_RekapDOHelper.GIDate = CType(dr("GIDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("ParkingAmount")) Then v_RekapDOHelper.ParkingAmount = CType(dr("ParkingAmount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then v_RekapDOHelper.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then v_RekapDOHelper.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then v_RekapDOHelper.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then v_RekapDOHelper.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then v_RekapDOHelper.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            Return v_RekapDOHelper

        End Function

        Private Sub SetTableName()

            If Not (GetType(V_RekapDOHelper) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(V_RekapDOHelper), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(V_RekapDOHelper).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

