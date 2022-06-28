#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : DealerContactNotificationCase Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2021 
'// ---------------------
'// $History      : $
'// Generated on 3/12/2021 - 10:48:56 AM
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

    Public Class DealerContactNotificationCaseMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertDealerContactNotificationCase"
        Private m_UpdateStatement As String = "up_UpdateDealerContactNotificationCase"
        Private m_RetrieveStatement As String = "up_RetrieveDealerContactNotificationCase"
        Private m_RetrieveListStatement As String = "up_RetrieveDealerContactNotificationCaseList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteDealerContactNotificationCase"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim dealerContactNotificationCase As DealerContactNotificationCase = Nothing
            While dr.Read

                dealerContactNotificationCase = Me.CreateObject(dr)

            End While

            Return dealerContactNotificationCase

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim dealerContactNotificationCaseList As ArrayList = New ArrayList

            While dr.Read
                Dim dealerContactNotificationCase As DealerContactNotificationCase = Me.CreateObject(dr)
                dealerContactNotificationCaseList.Add(dealerContactNotificationCase)
            End While

            Return dealerContactNotificationCaseList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim dealerContactNotificationCase As DealerContactNotificationCase = CType(obj, DealerContactNotificationCase)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, dealerContactNotificationCase.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim dealerContactNotificationCase As DealerContactNotificationCase = CType(obj, DealerContactNotificationCase)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, dealerContactNotificationCase.DealerID)
            DbCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, dealerContactNotificationCase.DealerCode)
            DbCommandWrapper.AddInParameter("@DealerName", DbType.AnsiString, dealerContactNotificationCase.DealerName)
            DbCommandWrapper.AddInParameter("@SearchTerm1", DbType.AnsiString, dealerContactNotificationCase.SearchTerm1)
            DbCommandWrapper.AddInParameter("@JobPosition", DbType.AnsiString, dealerContactNotificationCase.JobPosition)
            DbCommandWrapper.AddInParameter("@jobpositionid", DbType.Int32, dealerContactNotificationCase.jobpositionid)
            DbCommandWrapper.AddInParameter("@JobPosisi", DbType.AnsiString, dealerContactNotificationCase.JobPosisi)
            DbCommandWrapper.AddInParameter("@Phone", DbType.AnsiString, dealerContactNotificationCase.Phone)
            DbCommandWrapper.AddInParameter("@SalesmanHeaderID", DbType.Int32, dealerContactNotificationCase.SalesmanHeaderID)
            DbCommandWrapper.AddInParameter("@Tipe", DbType.Int32, dealerContactNotificationCase.Tipe)
            DbCommandWrapper.AddInParameter("@LokasiUbah", DbType.AnsiString, dealerContactNotificationCase.LokasiUbah)


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

            Dim dealerContactNotificationCase As DealerContactNotificationCase = CType(obj, DealerContactNotificationCase)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, dealerContactNotificationCase.ID)
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, dealerContactNotificationCase.DealerID)
            DbCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, dealerContactNotificationCase.DealerCode)
            DbCommandWrapper.AddInParameter("@DealerName", DbType.AnsiString, dealerContactNotificationCase.DealerName)
            DbCommandWrapper.AddInParameter("@SearchTerm1", DbType.AnsiString, dealerContactNotificationCase.SearchTerm1)
            DbCommandWrapper.AddInParameter("@JobPosition", DbType.AnsiString, dealerContactNotificationCase.JobPosition)
            DbCommandWrapper.AddInParameter("@jobpositionid", DbType.Int32, dealerContactNotificationCase.jobpositionid)
            DbCommandWrapper.AddInParameter("@JobPosisi", DbType.AnsiString, dealerContactNotificationCase.JobPosisi)
            DbCommandWrapper.AddInParameter("@Phone", DbType.AnsiString, dealerContactNotificationCase.Phone)
            DbCommandWrapper.AddInParameter("@SalesmanHeaderID", DbType.Int32, dealerContactNotificationCase.SalesmanHeaderID)
            DbCommandWrapper.AddInParameter("@Tipe", DbType.Int32, dealerContactNotificationCase.Tipe)
            DbCommandWrapper.AddInParameter("@LokasiUbah", DbType.AnsiString, dealerContactNotificationCase.LokasiUbah)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As DealerContactNotificationCase

            Dim dealerContactNotificationCase As DealerContactNotificationCase = New DealerContactNotificationCase

            dealerContactNotificationCase.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then dealerContactNotificationCase.DealerID = CType(dr("DealerID"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerCode")) Then dealerContactNotificationCase.DealerCode = dr("DealerCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DealerName")) Then dealerContactNotificationCase.DealerName = dr("DealerName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SearchTerm1")) Then dealerContactNotificationCase.SearchTerm1 = dr("SearchTerm1").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("JobPosition")) Then dealerContactNotificationCase.JobPosition = dr("JobPosition").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("jobpositionid")) Then dealerContactNotificationCase.jobpositionid = CType(dr("jobpositionid"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("JobPosisi")) Then dealerContactNotificationCase.JobPosisi = dr("JobPosisi").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Phone")) Then dealerContactNotificationCase.Phone = dr("Phone").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SalesmanHeaderID")) Then dealerContactNotificationCase.SalesmanHeaderID = CType(dr("SalesmanHeaderID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Tipe")) Then dealerContactNotificationCase.Tipe = CType(dr("Tipe"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("LokasiUbah")) Then dealerContactNotificationCase.LokasiUbah = dr("LokasiUbah").ToString

            Return dealerContactNotificationCase

        End Function

        Private Sub SetTableName()

            If Not (GetType(DealerContactNotificationCase) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(DealerContactNotificationCase), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(DealerContactNotificationCase).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace
