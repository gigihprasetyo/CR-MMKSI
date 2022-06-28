
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : JaminanDetail Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2010 
'// ---------------------
'// $History      : $
'// Generated on 1/7/2010 - 2:01:42 PM
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

    Public Class JaminanDetailMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertJaminanDetail"
        Private m_UpdateStatement As String = "up_UpdateJaminanDetail"
        Private m_RetrieveStatement As String = "up_RetrieveJaminanDetail"
        Private m_RetrieveListStatement As String = "up_RetrieveJaminanDetailList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteJaminanDetail"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim jaminanDetail As JaminanDetail = Nothing
            While dr.Read

                jaminanDetail = Me.CreateObject(dr)

            End While

            Return jaminanDetail

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim jaminanDetailList As ArrayList = New ArrayList

            While dr.Read
                Dim jaminanDetail As JaminanDetail = Me.CreateObject(dr)
                jaminanDetailList.Add(jaminanDetail)
            End While

            Return jaminanDetailList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim jaminanDetail As JaminanDetail = CType(obj, JaminanDetail)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, jaminanDetail.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim jaminanDetail As JaminanDetail = CType(obj, JaminanDetail)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DBCommandWrapper.AddInParameter("@PeriodMonth", DbType.Int32, jaminanDetail.PeriodMonth)
            DBCommandWrapper.AddInParameter("@PeriodYear", DbType.Int32, jaminanDetail.PeriodYear)
            DBCommandWrapper.AddInParameter("@VehicleTypeCode", DbType.AnsiString, jaminanDetail.VehicleTypeCode)
            DbCommandWrapper.AddInParameter("@Amount", DbType.Currency, jaminanDetail.Amount)
            DbCommandWrapper.AddInParameter("@Purpose", DbType.Int16, jaminanDetail.Purpose)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, jaminanDetail.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, jaminanDetail.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DBCommandWrapper.AddInParameter("@JaminanID", DbType.Int32, jaminanDetail.Jaminan.ID)

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

            Dim jaminanDetail As JaminanDetail = CType(obj, JaminanDetail)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, jaminanDetail.ID)
            DBCommandWrapper.AddInParameter("@PeriodMonth", DbType.Int32, jaminanDetail.PeriodMonth)
            DBCommandWrapper.AddInParameter("@PeriodYear", DbType.Int32, jaminanDetail.PeriodYear)
            DBCommandWrapper.AddInParameter("@VehicleTypeCode", DbType.AnsiString, jaminanDetail.VehicleTypeCode)
            DbCommandWrapper.AddInParameter("@Amount", DbType.Currency, jaminanDetail.Amount)
            DbCommandWrapper.AddInParameter("@Purpose", DbType.Int16, jaminanDetail.Purpose)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, jaminanDetail.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, jaminanDetail.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DBCommandWrapper.AddInParameter("@JaminanID", DbType.Int32, jaminanDetail.Jaminan.ID)

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As JaminanDetail

            Dim jaminanDetail As JaminanDetail = New JaminanDetail

            jaminanDetail.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("PeriodMonth")) Then jaminanDetail.PeriodMonth = CType(dr("PeriodMonth"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("PeriodYear")) Then jaminanDetail.PeriodYear = CType(dr("PeriodYear"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("VehicleTypeCode")) Then jaminanDetail.VehicleTypeCode = dr("VehicleTypeCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Amount")) Then jaminanDetail.Amount = CType(dr("Amount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("Purpose")) Then jaminanDetail.Purpose = CType(dr("Purpose"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then jaminanDetail.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then jaminanDetail.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then jaminanDetail.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then jaminanDetail.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then jaminanDetail.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("JaminanID")) Then
                jaminanDetail.Jaminan = New Jaminan(CType(dr("JaminanID"), Integer))
            End If

            Return jaminanDetail

        End Function

        Private Sub SetTableName()

            If Not (GetType(JaminanDetail) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(JaminanDetail), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(JaminanDetail).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

