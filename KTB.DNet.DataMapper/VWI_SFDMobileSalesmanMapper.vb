
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : VWI_SFDMobileSalesman Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2019 
'// ---------------------
'// $History      : $
'// Generated on 06/29/2019 - 8:00:52 PM
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

    Public Class VWI_SFDMobileSalesmanMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertVWI_SFDMobileSalesman"
        Private m_UpdateStatement As String = "up_UpdateVWI_SFDMobileSalesman"
        Private m_RetrieveStatement As String = "up_RetrieveVWI_SFDMobileSalesman"
        Private m_RetrieveListStatement As String = "up_RetrieveVWI_SFDMobileSalesmanList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteVWI_SFDMobileSalesman"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim VWI_SFDMobileSalesman As VWI_SFDMobileSalesman = Nothing
            While dr.Read

                VWI_SFDMobileSalesman = Me.CreateObject(dr)

            End While

            Return VWI_SFDMobileSalesman

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim VWI_SFDMobileSalesmanList As ArrayList = New ArrayList

            While dr.Read
                Dim VWI_SFDMobileSalesman As VWI_SFDMobileSalesman = Me.CreateObject(dr)
                VWI_SFDMobileSalesmanList.Add(VWI_SFDMobileSalesman)
            End While

            Return VWI_SFDMobileSalesmanList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim VWI_SFDMobileSalesman As VWI_SFDMobileSalesman = CType(obj, VWI_SFDMobileSalesman)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, VWI_SFDMobileSalesman.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim VWI_SFDMobileSalesman As VWI_SFDMobileSalesman = CType(obj, VWI_SFDMobileSalesman)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, VWI_SFDMobileSalesman.DealerCode)
            DbCommandWrapper.AddInParameter("@DealerName", DbType.AnsiString, VWI_SFDMobileSalesman.DealerName)
            DbCommandWrapper.AddInParameter("@DealerCity", DbType.AnsiString, VWI_SFDMobileSalesman.DealerCity)
            DbCommandWrapper.AddInParameter("@DealerGroup", DbType.AnsiString, VWI_SFDMobileSalesman.DealerGroup)
            DbCommandWrapper.AddInParameter("@DealerArea", DbType.AnsiString, VWI_SFDMobileSalesman.DealerArea)
            DbCommandWrapper.AddInParameter("@DealerBranchCode", DbType.AnsiString, VWI_SFDMobileSalesman.DealerBranchCode)
            DbCommandWrapper.AddInParameter("@DealerBranchName", DbType.AnsiString, VWI_SFDMobileSalesman.DealerBranchName)
            DbCommandWrapper.AddInParameter("@SalesmanCode", DbType.AnsiString, VWI_SFDMobileSalesman.SalesmanCode)
            DbCommandWrapper.AddInParameter("@SalesmanName", DbType.AnsiString, VWI_SFDMobileSalesman.SalesmanName)
            DbCommandWrapper.AddInParameter("@SalesmanHireDate", DbType.Date, VWI_SFDMobileSalesman.SalesmanHireDate)
            DbCommandWrapper.AddInParameter("@JobDescription", DbType.AnsiString, VWI_SFDMobileSalesman.JobDescription)
            DbCommandWrapper.AddInParameter("@LevelDescription", DbType.AnsiString, VWI_SFDMobileSalesman.LevelDescription)
            DbCommandWrapper.AddInParameter("@SuperiorName", DbType.AnsiString, VWI_SFDMobileSalesman.SuperiorName)
            DbCommandWrapper.AddInParameter("@SuperiorCode", DbType.AnsiString, VWI_SFDMobileSalesman.SuperiorCode)
            DbCommandWrapper.AddInParameter("@SalesmanEmail", DbType.AnsiString, VWI_SFDMobileSalesman.SalesmanEmail)
            DbCommandWrapper.AddInParameter("@SalesmanHandphone", DbType.AnsiString, VWI_SFDMobileSalesman.SalesmanHandphone)
            DbCommandWrapper.AddInParameter("@SalesmanTeamCategory", DbType.AnsiString, VWI_SFDMobileSalesman.SalesmanTeamCategory)
            DbCommandWrapper.AddInParameter("@SalesmanStatus", DbType.AnsiString, VWI_SFDMobileSalesman.SalesmanStatus)
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

            Dim VWI_SFDMobileSalesman As VWI_SFDMobileSalesman = CType(obj, VWI_SFDMobileSalesman)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, VWI_SFDMobileSalesman.ID)
            DbCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, VWI_SFDMobileSalesman.DealerCode)
            DbCommandWrapper.AddInParameter("@DealerName", DbType.AnsiString, VWI_SFDMobileSalesman.DealerName)
            DbCommandWrapper.AddInParameter("@DealerCity", DbType.AnsiString, VWI_SFDMobileSalesman.DealerCity)
            DbCommandWrapper.AddInParameter("@DealerGroup", DbType.AnsiString, VWI_SFDMobileSalesman.DealerGroup)
            DbCommandWrapper.AddInParameter("@DealerArea", DbType.AnsiString, VWI_SFDMobileSalesman.DealerArea)
            DbCommandWrapper.AddInParameter("@DealerBranchCode", DbType.AnsiString, VWI_SFDMobileSalesman.DealerBranchCode)
            DbCommandWrapper.AddInParameter("@DealerBranchName", DbType.AnsiString, VWI_SFDMobileSalesman.DealerBranchName)
            DbCommandWrapper.AddInParameter("@SalesmanCode", DbType.AnsiString, VWI_SFDMobileSalesman.SalesmanCode)
            DbCommandWrapper.AddInParameter("@SalesmanName", DbType.AnsiString, VWI_SFDMobileSalesman.SalesmanName)
            DbCommandWrapper.AddInParameter("@SalesmanHireDate", DbType.Date, VWI_SFDMobileSalesman.SalesmanHireDate)
            DbCommandWrapper.AddInParameter("@JobDescription", DbType.AnsiString, VWI_SFDMobileSalesman.JobDescription)
            DbCommandWrapper.AddInParameter("@LevelDescription", DbType.AnsiString, VWI_SFDMobileSalesman.LevelDescription)
            DbCommandWrapper.AddInParameter("@SuperiorName", DbType.AnsiString, VWI_SFDMobileSalesman.SuperiorName)
            DbCommandWrapper.AddInParameter("@SuperiorCode", DbType.AnsiString, VWI_SFDMobileSalesman.SuperiorCode)
            DbCommandWrapper.AddInParameter("@SalesmanEmail", DbType.AnsiString, VWI_SFDMobileSalesman.SalesmanEmail)
            DbCommandWrapper.AddInParameter("@SalesmanHandphone", DbType.AnsiString, VWI_SFDMobileSalesman.SalesmanHandphone)
            DbCommandWrapper.AddInParameter("@SalesmanTeamCategory", DbType.AnsiString, VWI_SFDMobileSalesman.SalesmanTeamCategory)
            DbCommandWrapper.AddInParameter("@SalesmanStatus", DbType.AnsiString, VWI_SFDMobileSalesman.SalesmanStatus)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As VWI_SFDMobileSalesman

            Dim VWI_SFDMobileSalesman As VWI_SFDMobileSalesman = New VWI_SFDMobileSalesman

            VWI_SFDMobileSalesman.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerCode")) Then VWI_SFDMobileSalesman.DealerCode = dr("DealerCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DealerName")) Then VWI_SFDMobileSalesman.DealerName = dr("DealerName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DealerCity")) Then VWI_SFDMobileSalesman.DealerCity = dr("DealerCity").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DealerGroup")) Then VWI_SFDMobileSalesman.DealerGroup = dr("DealerGroup").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DealerArea")) Then VWI_SFDMobileSalesman.DealerArea = dr("DealerArea").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DealerBranchCode")) Then VWI_SFDMobileSalesman.DealerBranchCode = dr("DealerBranchCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DealerBranchName")) Then VWI_SFDMobileSalesman.DealerBranchName = dr("DealerBranchName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SalesmanCode")) Then VWI_SFDMobileSalesman.SalesmanCode = dr("SalesmanCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SalesmanName")) Then VWI_SFDMobileSalesman.SalesmanName = dr("SalesmanName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SalesmanHireDate")) Then VWI_SFDMobileSalesman.SalesmanHireDate = CType(dr("SalesmanHireDate"), Date)
            If Not dr.IsDBNull(dr.GetOrdinal("JobDescription")) Then VWI_SFDMobileSalesman.JobDescription = dr("JobDescription").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LevelDescription")) Then VWI_SFDMobileSalesman.LevelDescription = dr("LevelDescription").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SuperiorName")) Then VWI_SFDMobileSalesman.SuperiorName = dr("SuperiorName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SuperiorCode")) Then VWI_SFDMobileSalesman.SuperiorCode = dr("SuperiorCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SalesmanEmail")) Then VWI_SFDMobileSalesman.SalesmanEmail = dr("SalesmanEmail").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SalesmanHandphone")) Then VWI_SFDMobileSalesman.SalesmanHandphone = dr("SalesmanHandphone").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SalesmanTeamCategory")) Then VWI_SFDMobileSalesman.SalesmanTeamCategory = dr("SalesmanTeamCategory").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SalesmanStatus")) Then VWI_SFDMobileSalesman.SalesmanStatus = dr("SalesmanStatus").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then VWI_SFDMobileSalesman.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            Return VWI_SFDMobileSalesman

        End Function

        Private Sub SetTableName()

            If Not (GetType(VWI_SFDMobileSalesman) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(VWI_SFDMobileSalesman), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(VWI_SFDMobileSalesman).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

