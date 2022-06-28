
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : Mitrais Team
'// PURPOSE       : VWI_UnmatchSPKChassis Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 11/10/2018 - 22:17:51
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

    Public Class VWI_UnmatchSPKChassisMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertVWI_UnmatchSPKChassis"
        Private m_UpdateStatement As String = "up_UpdateVWI_UnmatchSPKChassis"
        Private m_RetrieveStatement As String = "up_RetrieveVWI_UnmatchSPKChassis"
        Private m_RetrieveListStatement As String = "up_RetrieveVWI_UnmatchSPKChassisList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteVWI_UnmatchSPKChassis"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim VWI_UnmatchSPKChassis As VWI_UnmatchSPKChassis = Nothing
            While dr.Read

                VWI_UnmatchSPKChassis = Me.CreateObject(dr)

            End While

            Return VWI_UnmatchSPKChassis

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim VWI_UnmatchSPKChassisList As ArrayList = New ArrayList

            While dr.Read
                Dim VWI_UnmatchSPKChassis As VWI_UnmatchSPKChassis = Me.CreateObject(dr)
                VWI_UnmatchSPKChassisList.Add(VWI_UnmatchSPKChassis)
            End While

            Return VWI_UnmatchSPKChassisList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim VWI_UnmatchSPKChassis As VWI_UnmatchSPKChassis = CType(obj, VWI_UnmatchSPKChassis)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, VWI_UnmatchSPKChassis.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim VWI_UnmatchSPKChassis As VWI_UnmatchSPKChassis = CType(obj, VWI_UnmatchSPKChassis)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@RegNumber", DbType.AnsiString, VWI_UnmatchSPKChassis.RegNumber)
            DbCommandWrapper.AddInParameter("@RevisionDate", DbType.DateTime, VWI_UnmatchSPKChassis.RevisionDate)
            DbCommandWrapper.AddInParameter("@RevisionStatusID", DbType.Int16, VWI_UnmatchSPKChassis.RevisionStatusID)
            DbCommandWrapper.AddInParameter("@RevisionStatus", DbType.AnsiString, VWI_UnmatchSPKChassis.RevisionStatus)
            DbCommandWrapper.AddInParameter("@RevisionType", DbType.AnsiString, VWI_UnmatchSPKChassis.RevisionType)
            DbCommandWrapper.AddInParameter("@ChassisNumber", DbType.AnsiString, VWI_UnmatchSPKChassis.ChassisNumber)
            DbCommandWrapper.AddInParameter("@SPKHeaderID", DbType.Int32, VWI_UnmatchSPKChassis.SPKHeaderID)
            DbCommandWrapper.AddInParameter("@SPKNumber", DbType.AnsiString, VWI_UnmatchSPKChassis.SPKNumber)
            DbCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, VWI_UnmatchSPKChassis.DealerCode)
            DbCommandWrapper.AddInParameter("@DealerSPKNumber", DbType.AnsiString, VWI_UnmatchSPKChassis.DealerSPKNumber)
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

            Dim VWI_UnmatchSPKChassis As VWI_UnmatchSPKChassis = CType(obj, VWI_UnmatchSPKChassis)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, VWI_UnmatchSPKChassis.ID)
            DbCommandWrapper.AddInParameter("@RegNumber", DbType.AnsiString, VWI_UnmatchSPKChassis.RegNumber)
            DbCommandWrapper.AddInParameter("@RevisionDate", DbType.DateTime, VWI_UnmatchSPKChassis.RevisionDate)
            DbCommandWrapper.AddInParameter("@RevisionStatusID", DbType.Int16, VWI_UnmatchSPKChassis.RevisionStatusID)
            DbCommandWrapper.AddInParameter("@RevisionStatus", DbType.AnsiString, VWI_UnmatchSPKChassis.RevisionStatus)
            DbCommandWrapper.AddInParameter("@RevisionType", DbType.AnsiString, VWI_UnmatchSPKChassis.RevisionType)
            DbCommandWrapper.AddInParameter("@ChassisNumber", DbType.AnsiString, VWI_UnmatchSPKChassis.ChassisNumber)
            DbCommandWrapper.AddInParameter("@SPKHeaderID", DbType.Int32, VWI_UnmatchSPKChassis.SPKHeaderID)
            DbCommandWrapper.AddInParameter("@SPKNumber", DbType.AnsiString, VWI_UnmatchSPKChassis.SPKNumber)
            DbCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, VWI_UnmatchSPKChassis.DealerCode)
            DbCommandWrapper.AddInParameter("@DealerSPKNumber", DbType.AnsiString, VWI_UnmatchSPKChassis.DealerSPKNumber)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As VWI_UnmatchSPKChassis

            Dim VWI_UnmatchSPKChassis As VWI_UnmatchSPKChassis = New VWI_UnmatchSPKChassis

            VWI_UnmatchSPKChassis.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("RegNumber")) Then VWI_UnmatchSPKChassis.RegNumber = dr("RegNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RevisionDate")) Then VWI_UnmatchSPKChassis.RevisionDate = CType(dr("RevisionDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("RevisionStatusID")) Then VWI_UnmatchSPKChassis.RevisionStatusID = CType(dr("RevisionStatusID"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("RevisionStatus")) Then VWI_UnmatchSPKChassis.RevisionStatus = dr("RevisionStatus").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RevisionType")) Then VWI_UnmatchSPKChassis.RevisionType = dr("RevisionType").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ChassisNumber")) Then VWI_UnmatchSPKChassis.ChassisNumber = dr("ChassisNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SPKHeaderID")) Then VWI_UnmatchSPKChassis.SPKHeaderID = CType(dr("SPKHeaderID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("SPKNumber")) Then VWI_UnmatchSPKChassis.SPKNumber = dr("SPKNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DealerCode")) Then VWI_UnmatchSPKChassis.DealerCode = dr("DealerCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DealerSPKNumber")) Then VWI_UnmatchSPKChassis.DealerSPKNumber = dr("DealerSPKNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then VWI_UnmatchSPKChassis.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            Return VWI_UnmatchSPKChassis

        End Function

        Private Sub SetTableName()

            If Not (GetType(VWI_UnmatchSPKChassis) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(VWI_UnmatchSPKChassis), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(VWI_UnmatchSPKChassis).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

