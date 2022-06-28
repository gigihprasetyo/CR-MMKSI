
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : AssistCutOffPeriod Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2017 
'// ---------------------
'// $History      : $
'// Generated on 12/15/2017 - 9:13:23 AM
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

    Public Class AssistCutOffPeriodMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertAssistCutOffPeriod"
        Private m_UpdateStatement As String = "up_UpdateAssistCutOffPeriod"
        Private m_RetrieveStatement As String = "up_RetrieveAssistCutOffPeriod"
        Private m_RetrieveListStatement As String = "up_RetrieveAssistCutOffPeriodList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteAssistCutOffPeriod"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim assistCutOffPeriod As AssistCutOffPeriod = Nothing
            While dr.Read

                assistCutOffPeriod = Me.CreateObject(dr)

            End While

            Return assistCutOffPeriod

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim assistCutOffPeriodList As ArrayList = New ArrayList

            While dr.Read
                Dim assistCutOffPeriod As AssistCutOffPeriod = Me.CreateObject(dr)
                assistCutOffPeriodList.Add(assistCutOffPeriod)
            End While

            Return assistCutOffPeriodList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim assistCutOffPeriod As AssistCutOffPeriod = CType(obj, AssistCutOffPeriod)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, assistCutOffPeriod.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim assistCutOffPeriod As AssistCutOffPeriod = CType(obj, AssistCutOffPeriod)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, Me.GetRefObject(assistCutOffPeriod.Dealer))
            DbCommandWrapper.AddInParameter("@Month", DbType.Int32, assistCutOffPeriod.Month)
            DbCommandWrapper.AddInParameter("@Year", DbType.Int32, assistCutOffPeriod.Year)
            DbCommandWrapper.AddInParameter("@CutOffDate", DbType.DateTime, assistCutOffPeriod.CutOffDate)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, assistCutOffPeriod.Status)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, assistCutOffPeriod.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, assistCutOffPeriod.LastUpdateBy)
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

            Dim assistCutOffPeriod As AssistCutOffPeriod = CType(obj, AssistCutOffPeriod)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, assistCutOffPeriod.ID)
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, Me.GetRefObject(assistCutOffPeriod.Dealer))
            DbCommandWrapper.AddInParameter("@Month", DbType.Int32, assistCutOffPeriod.Month)
            DbCommandWrapper.AddInParameter("@Year", DbType.Int32, assistCutOffPeriod.Year)
            DbCommandWrapper.AddInParameter("@CutOffDate", DbType.DateTime, assistCutOffPeriod.CutOffDate)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, assistCutOffPeriod.Status)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, assistCutOffPeriod.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, assistCutOffPeriod.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As AssistCutOffPeriod

            Dim assistCutOffPeriod As AssistCutOffPeriod = New AssistCutOffPeriod

            assistCutOffPeriod.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Month")) Then assistCutOffPeriod.Month = CType(dr("Month"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Year")) Then assistCutOffPeriod.Year = CType(dr("Year"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("CutOffDate")) Then assistCutOffPeriod.CutOffDate = CType(dr("CutOffDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then assistCutOffPeriod.Status = CType(dr("Status"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then assistCutOffPeriod.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then assistCutOffPeriod.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then assistCutOffPeriod.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then assistCutOffPeriod.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then assistCutOffPeriod.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then
                assistCutOffPeriod.Dealer = New Dealer(CType(dr("DealerID"), Integer))
            End If
            Return assistCutOffPeriod

        End Function

        Private Sub SetTableName()

            If Not (GetType(AssistCutOffPeriod) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(AssistCutOffPeriod), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(AssistCutOffPeriod).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

