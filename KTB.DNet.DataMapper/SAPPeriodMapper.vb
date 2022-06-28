#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : SAPPeriod Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2007 
'// ---------------------
'// $History      : $
'// Generated on 9/13/2007 - 1:28:58 PM
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

    Public Class SAPPeriodMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertSAPPeriod"
        Private m_UpdateStatement As String = "up_UpdateSAPPeriod"
        Private m_RetrieveStatement As String = "up_RetrieveSAPPeriod"
        Private m_RetrieveListStatement As String = "up_RetrieveSAPPeriodList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteSAPPeriod"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim sAPPeriod As SAPPeriod = Nothing
            While dr.Read

                sAPPeriod = Me.CreateObject(dr)

            End While

            Return sAPPeriod

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim sAPPeriodList As ArrayList = New ArrayList

            While dr.Read
                Dim sAPPeriod As SAPPeriod = Me.CreateObject(dr)
                sAPPeriodList.Add(sAPPeriod)
            End While

            Return sAPPeriodList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim sAPPeriod As SAPPeriod = CType(obj, SAPPeriod)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, sAPPeriod.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim sAPPeriod As SAPPeriod = CType(obj, SAPPeriod)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@SAPNumber", DbType.AnsiString, sAPPeriod.SAPNumber)
            DbCommandWrapper.AddInParameter("@StartDate", DbType.DateTime, sAPPeriod.StartDate)
            DbCommandWrapper.AddInParameter("@EndDate", DbType.DateTime, sAPPeriod.EndDate)
            DbCommandWrapper.AddInParameter("@EndConfirmedDate", DbType.DateTime, sAPPeriod.EndConfirmedDate)
            DbCommandWrapper.AddInParameter("@EndConfirmHour", DbType.AnsiString, sAPPeriod.EndConfirmHour)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, sAPPeriod.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, sAPPeriod.LastUpdateBy)
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

            Dim sAPPeriod As SAPPeriod = CType(obj, SAPPeriod)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, sAPPeriod.ID)
            DbCommandWrapper.AddInParameter("@SAPNumber", DbType.AnsiString, sAPPeriod.SAPNumber)
            DbCommandWrapper.AddInParameter("@StartDate", DbType.DateTime, sAPPeriod.StartDate)
            DbCommandWrapper.AddInParameter("@EndDate", DbType.DateTime, sAPPeriod.EndDate)
            DbCommandWrapper.AddInParameter("@EndConfirmedDate", DbType.DateTime, sAPPeriod.EndConfirmedDate)
            DbCommandWrapper.AddInParameter("@EndConfirmHour", DbType.AnsiString, sAPPeriod.EndConfirmHour)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, sAPPeriod.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, sAPPeriod.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As SAPPeriod

            Dim sAPPeriod As SAPPeriod = New SAPPeriod

            sAPPeriod.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("SAPNumber")) Then sAPPeriod.SAPNumber = dr("SAPNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("StartDate")) Then sAPPeriod.StartDate = CType(dr("StartDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("EndDate")) Then sAPPeriod.EndDate = CType(dr("EndDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("EndConfirmedDate")) Then sAPPeriod.EndConfirmedDate = CType(dr("EndConfirmedDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("EndConfirmHour")) Then sAPPeriod.EndConfirmHour = dr("EndConfirmHour").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then sAPPeriod.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then sAPPeriod.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then sAPPeriod.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then sAPPeriod.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then sAPPeriod.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            Return sAPPeriod

        End Function

        Private Sub SetTableName()

            If Not (GetType(SAPPeriod) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(SAPPeriod), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(SAPPeriod).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

