
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : VW_ServiceTemplateActivity Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 20/07/2018 - 13:24:15
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

    Public Class VW_ServiceTemplateActivityMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertVW_ServiceTemplateActivity"
        Private m_UpdateStatement As String = "up_UpdateVW_ServiceTemplateActivity"
        Private m_RetrieveStatement As String = "up_RetrieveVW_ServiceTemplateActivity"
        Private m_RetrieveListStatement As String = "up_RetrieveVW_ServiceTemplateActivityList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteVW_ServiceTemplateActivity"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim VW_ServiceTemplateActivity As VW_ServiceTemplateActivity = Nothing
            While dr.Read

                VW_ServiceTemplateActivity = Me.CreateObject(dr)

            End While

            Return VW_ServiceTemplateActivity

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim VW_ServiceTemplateActivityList As ArrayList = New ArrayList

            While dr.Read
                Dim VW_ServiceTemplateActivity As VW_ServiceTemplateActivity = Me.CreateObject(dr)
                VW_ServiceTemplateActivityList.Add(VW_ServiceTemplateActivity)
            End While

            Return VW_ServiceTemplateActivityList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim VW_ServiceTemplateActivity As VW_ServiceTemplateActivity = CType(obj, VW_ServiceTemplateActivity)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.StringFixedLength, VW_ServiceTemplateActivity.ID)

            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim VW_ServiceTemplateActivity As VW_ServiceTemplateActivity = CType(obj, VW_ServiceTemplateActivity)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)
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

            Dim VW_ServiceTemplateActivity As VW_ServiceTemplateActivity = CType(obj, VW_ServiceTemplateActivity)

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As VW_ServiceTemplateActivity

            Dim VW_ServiceTemplateActivity As VW_ServiceTemplateActivity = New VW_ServiceTemplateActivity

            VW_ServiceTemplateActivity.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("ServiceType")) Then VW_ServiceTemplateActivity.ServiceType = dr("ServiceType").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ServiceTemplateHeaderID")) Then VW_ServiceTemplateActivity.ServiceTemplateHeaderID = CType(dr("ServiceTemplateHeaderID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("KindID")) Then VW_ServiceTemplateActivity.KindID = CType(dr("KindID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("VechileTypeID")) Then VW_ServiceTemplateActivity.VechileTypeID = CType(dr("VechileTypeID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("ValidFrom")) Then VW_ServiceTemplateActivity.ValidFrom = CType(dr("ValidFrom"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("KindCode")) Then VW_ServiceTemplateActivity.KindCode = dr("KindCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("KindDescription")) Then VW_ServiceTemplateActivity.KindDescription = dr("KindDescription").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("VehicleTypeCode")) Then VW_ServiceTemplateActivity.VehicleTypeCode = dr("VehicleTypeCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ActName")) Then VW_ServiceTemplateActivity.ActName = dr("ActName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ActSequence")) Then VW_ServiceTemplateActivity.ActSequence = dr("ActSequence").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Duration")) Then VW_ServiceTemplateActivity.Duration = CType(dr("Duration"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("SVCTemplateActivity")) Then VW_ServiceTemplateActivity.SVCTemplateActivity = dr("SVCTemplateActivity").ToString

            Return VW_ServiceTemplateActivity

        End Function

        Private Sub SetTableName()

            If Not (GetType(VW_ServiceTemplateActivity) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(VW_ServiceTemplateActivity), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(VW_ServiceTemplateActivity).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

