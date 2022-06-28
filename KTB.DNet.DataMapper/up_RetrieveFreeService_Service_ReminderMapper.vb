
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : sp_CeilingPO Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2012 
'// ---------------------
'// $History      : $
'// Generated on 1/2/2012 - 4:37:40 PM
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

    Public Class up_RetrieveFreeService_Service_ReminderMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

       
        Private m_RetrieveStatement As String = "up_Retrievesp_CeilingPO"
        Private m_RetrieveListStatement As String = "up_Retrievesp_CeilingPOList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_Deletesp_CeilingPO"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim up_ser_rem As up_RetrieveFreeService_Service_Reminder = Nothing
            While dr.Read

                up_ser_rem = Me.CreateObject(dr)

            End While

            Return up_ser_rem

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim arrup_ser_rem As ArrayList = New ArrayList

            While dr.Read
                Dim up_ser_rem As up_RetrieveFreeService_Service_Reminder = Me.CreateObject(dr)
                arrup_ser_rem.Add(up_ser_rem)
            End While

            Return arrup_ser_rem

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper


        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

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

            

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As up_RetrieveFreeService_Service_Reminder

            Dim up_RetrieveFreeService_Service_Reminder As up_RetrieveFreeService_Service_Reminder = New up_RetrieveFreeService_Service_Reminder

            up_RetrieveFreeService_Service_Reminder.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Name")) Then up_RetrieveFreeService_Service_Reminder.Name = dr("Name").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("KM_LAST")) Then up_RetrieveFreeService_Service_Reminder.KM_LAST = CType(dr("KM_LAST"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("CityName")) Then up_RetrieveFreeService_Service_Reminder.CityName = dr("CityName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("NoHP")) Then up_RetrieveFreeService_Service_Reminder.NoHP = dr("NoHP").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("OpenFakturDate")) Then up_RetrieveFreeService_Service_Reminder.OpenFakturDate = CType(dr("OpenFakturDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("PKTDate")) Then up_RetrieveFreeService_Service_Reminder.PKTDate = CType(dr("PKTDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("ChassisNumber")) Then up_RetrieveFreeService_Service_Reminder.ChassisNumber = dr("ChassisNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("FSType")) Then up_RetrieveFreeService_Service_Reminder.FSType = dr("FSType").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("KindDescription")) Then up_RetrieveFreeService_Service_Reminder.KindDescription = dr("KindDescription").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ID")) Then up_RetrieveFreeService_Service_Reminder.FSKindID = dr("ID").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("FakturDate")) Then up_RetrieveFreeService_Service_Reminder.FakturDate = dr("FakturDate").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ExpiredDateByOpenFakturDate")) Then up_RetrieveFreeService_Service_Reminder.ExpiredDateByOpenFakturDate = CType(dr("ExpiredDateByOpenFakturDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("ExpiredDateByPKTDate")) Then up_RetrieveFreeService_Service_Reminder.ExpiredDateByPKTDate = CType(dr("ExpiredDateByPKTDate"), DateTime)
            Return up_RetrieveFreeService_Service_Reminder

        End Function

        Private Sub SetTableName()

            If Not (GetType(up_RetrieveFreeService_Service_Reminder) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(up_RetrieveFreeService_Service_Reminder), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(up_RetrieveFreeService_Service_Reminder).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

