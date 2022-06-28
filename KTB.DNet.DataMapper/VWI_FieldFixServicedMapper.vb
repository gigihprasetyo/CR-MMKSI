
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : Mitrais Team
'// PURPOSE       : VWI_FieldFixServiced Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 07/11/2018 - 10:51:30
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

    Public Class VWI_FieldFixServicedMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertVWI_FieldFixServiced"
        Private m_UpdateStatement As String = "up_UpdateVWI_FieldFixServiced"
        Private m_RetrieveStatement As String = "up_RetrieveVWI_FieldFixServiced"
        Private m_RetrieveListStatement As String = "up_RetrieveVWI_FieldFixServicedList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteVWI_FieldFixServiced"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim vWI_FieldFixServiced As VWI_FieldFixServiced = Nothing
            While dr.Read

                vWI_FieldFixServiced = Me.CreateObject(dr)

            End While

            Return vWI_FieldFixServiced

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim vWI_FieldFixServicedList As ArrayList = New ArrayList

            While dr.Read
                Dim vWI_FieldFixServiced As VWI_FieldFixServiced = Me.CreateObject(dr)
                vWI_FieldFixServicedList.Add(vWI_FieldFixServiced)
            End While

            Return vWI_FieldFixServicedList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim vWI_FieldFixServiced As VWI_FieldFixServiced = CType(obj, VWI_FieldFixServiced)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, vWI_FieldFixServiced.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim vWI_FieldFixServiced As VWI_FieldFixServiced = CType(obj, VWI_FieldFixServiced)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@ChassisNumber", DbType.AnsiString, vWI_FieldFixServiced.ChassisNumber)
            DbCommandWrapper.AddInParameter("@ServiceDate", DbType.DateTime, vWI_FieldFixServiced.ServiceDate)
            DbCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, vWI_FieldFixServiced.DealerCode)
            DbCommandWrapper.AddInParameter("@DealerBranchCode", DbType.AnsiString, vWI_FieldFixServiced.DealerBranchCode)
            DbCommandWrapper.AddInParameter("@WorkOrderNumber", DbType.AnsiString, vWI_FieldFixServiced.WorkOrderNumber)
            DbCommandWrapper.AddInParameter("@RecallRegNo", DbType.AnsiString, vWI_FieldFixServiced.RecallRegNo)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, vWI_FieldFixServiced.Description)
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

            Dim vWI_FieldFixServiced As VWI_FieldFixServiced = CType(obj, VWI_FieldFixServiced)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, vWI_FieldFixServiced.ID)
            DbCommandWrapper.AddInParameter("@ChassisNumber", DbType.AnsiString, vWI_FieldFixServiced.ChassisNumber)
            DbCommandWrapper.AddInParameter("@ServiceDate", DbType.DateTime, vWI_FieldFixServiced.ServiceDate)
            DbCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, vWI_FieldFixServiced.DealerCode)
            DbCommandWrapper.AddInParameter("@DealerBranchCode", DbType.AnsiString, vWI_FieldFixServiced.DealerBranchCode)
            DbCommandWrapper.AddInParameter("@WorkOrderNumber", DbType.AnsiString, vWI_FieldFixServiced.WorkOrderNumber)
            DbCommandWrapper.AddInParameter("@RecallRegNo", DbType.AnsiString, vWI_FieldFixServiced.RecallRegNo)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, vWI_FieldFixServiced.Description)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As VWI_FieldFixServiced

            Dim vWI_FieldFixServiced As VWI_FieldFixServiced = New VWI_FieldFixServiced

            vWI_FieldFixServiced.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("ChassisNumber")) Then vWI_FieldFixServiced.ChassisNumber = dr("ChassisNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ServiceDate")) Then vWI_FieldFixServiced.ServiceDate = CType(dr("ServiceDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerCode")) Then vWI_FieldFixServiced.DealerCode = dr("DealerCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DealerBranchCode")) Then vWI_FieldFixServiced.DealerBranchCode = dr("DealerBranchCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("WorkOrderNumber")) Then vWI_FieldFixServiced.WorkOrderNumber = dr("WorkOrderNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RecallRegNo")) Then vWI_FieldFixServiced.RecallRegNo = dr("RecallRegNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Description")) Then vWI_FieldFixServiced.Description = dr("Description").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then vWI_FieldFixServiced.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("SystemID")) Then vWI_FieldFixServiced.SystemID = CType(dr("SystemID"), Integer)

            Return vWI_FieldFixServiced

        End Function

        Private Sub SetTableName()

            If Not (GetType(VWI_FieldFixServiced) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(VWI_FieldFixServiced), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(VWI_FieldFixServiced).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

