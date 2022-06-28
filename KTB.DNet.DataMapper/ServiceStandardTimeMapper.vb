#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : ServiceStandardTime Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2005 
'// ---------------------
'// $History      : $
'// Generated on 11/30/2005 - 1:05:06 PM
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

    Public Class ServiceStandardTimeMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertServiceStandardTime"
        Private m_UpdateStatement As String = "up_UpdateServiceStandardTime"
        Private m_RetrieveStatement As String = "up_RetrieveServiceStandardTime"
        Private m_RetrieveListStatement As String = "up_RetrieveServiceStandardTimeList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteServiceStandardTime"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim ServiceStandardTime As ServiceStandardTime = Nothing
            While dr.Read

                ServiceStandardTime = Me.CreateObject(dr)

            End While

            Return ServiceStandardTime

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim ServiceStandardTimeList As ArrayList = New ArrayList

            While dr.Read
                Dim ServiceStandardTime As ServiceStandardTime = Me.CreateObject(dr)
                ServiceStandardTimeList.Add(ServiceStandardTime)
            End While

            Return ServiceStandardTimeList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim ServiceStandardTime As ServiceStandardTime = CType(obj, ServiceStandardTime)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, ServiceStandardTime.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim ServiceStandardTime As ServiceStandardTime = CType(obj, ServiceStandardTime)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)

            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@ServiceTypeID", DbType.Int16, ServiceStandardTime.ServiceTypeID)
            DbCommandWrapper.AddInParameter("@KindCode", DbType.AnsiString, ServiceStandardTime.KindCode)
            DbCommandWrapper.AddInParameter("@DealerStandardTime", DbType.Decimal, ServiceStandardTime.DealerStandardTime)
            DbCommandWrapper.AddInParameter("@SystemStandardTime", DbType.Decimal, ServiceStandardTime.SystemStandardTime)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, ServiceStandardTime.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdatedBy", DbType.AnsiString, ServiceStandardTime.LastUpdatedBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, Me.GetRefObject(ServiceStandardTime.Dealer))
            DbCommandWrapper.AddInParameter("@AssistServiceTypeCode", DbType.AnsiString, ServiceStandardTime.AssistServiceTypeCode)
            DbCommandWrapper.AddInParameter("@VechileModelID", DbType.Int16, Me.GetRefObject(ServiceStandardTime.VechileModel))
            DbCommandWrapper.AddInParameter("@VechileTypeID", DbType.Int16, Me.GetRefObject(ServiceStandardTime.VechileType))
            DbCommandWrapper.AddInParameter("@ProcessCode", DbType.AnsiString, ServiceStandardTime.ProcessCode)
            DbCommandWrapper.AddInParameter("@Notes", DbType.AnsiString, ServiceStandardTime.Notes)


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

            Dim ServiceStandardTime As ServiceStandardTime = CType(obj, ServiceStandardTime)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, ServiceStandardTime.ID)
            DbCommandWrapper.AddInParameter("@ServiceTypeID", DbType.Int16, ServiceStandardTime.ServiceTypeID)
            DbCommandWrapper.AddInParameter("@KindCode", DbType.AnsiString, ServiceStandardTime.KindCode)
            DbCommandWrapper.AddInParameter("@DealerStandardTime", DbType.Decimal, ServiceStandardTime.DealerStandardTime)
            DbCommandWrapper.AddInParameter("@SystemStandardTime", DbType.Decimal, ServiceStandardTime.SystemStandardTime)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, ServiceStandardTime.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, ServiceStandardTime.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, Me.GetRefObject(ServiceStandardTime.Dealer))
            DbCommandWrapper.AddInParameter("@AssistServiceTypeCode", DbType.AnsiString, ServiceStandardTime.AssistServiceTypeCode)
            DbCommandWrapper.AddInParameter("@VechileModelID", DbType.Int16, Me.GetRefObject(ServiceStandardTime.VechileModel))
            DbCommandWrapper.AddInParameter("@VechileTypeID", DbType.Int16, Me.GetRefObject(ServiceStandardTime.VechileType))
            DbCommandWrapper.AddInParameter("@ProcessCode", DbType.AnsiString, ServiceStandardTime.ProcessCode)
            DbCommandWrapper.AddInParameter("@Notes", DbType.AnsiString, ServiceStandardTime.Notes)


            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As ServiceStandardTime

            Dim ServiceStandardTime As ServiceStandardTime = New ServiceStandardTime

            ServiceStandardTime.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("ServiceTypeID")) Then ServiceStandardTime.ServiceTypeID = CType(dr("ServiceTypeID"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("KindCode")) Then ServiceStandardTime.KindCode = dr("KindCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Notes")) Then ServiceStandardTime.Notes = dr("Notes").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then ServiceStandardTime.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then ServiceStandardTime.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then ServiceStandardTime.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdatedBy")) Then ServiceStandardTime.LastUpdatedBy = dr("LastUpdatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdatedTime")) Then ServiceStandardTime.LastUpdatedTime = CType(dr("LastUpdatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then
                ServiceStandardTime.Dealer = New Dealer(CType(dr("DealerID"), Short))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("SystemStandardTime")) Then ServiceStandardTime.SystemStandardTime = CType(dr("SystemStandardTime"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerStandardTime")) Then ServiceStandardTime.DealerStandardTime = CType(dr("DealerStandardTime"), Decimal)

            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then ServiceStandardTime.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("AssistServiceTypeCode")) Then ServiceStandardTime.AssistServiceTypeCode = dr("AssistServiceTypeCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("VechileModelID")) Then
                ServiceStandardTime.VechileModel = New VechileModel(CType(dr("VechileModelID"), Short))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("VechileTypeID")) Then
                ServiceStandardTime.VechileType = New VechileType(CType(dr("VechileTypeID"), Short))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("ProcessCode")) Then ServiceStandardTime.ProcessCode = dr("ProcessCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Notes")) Then ServiceStandardTime.ProcessCode = dr("Notes").ToString



            Return ServiceStandardTime

        End Function

        Private Sub SetTableName()

            If Not (GetType(ServiceStandardTime) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(ServiceStandardTime), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(ServiceStandardTime).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

