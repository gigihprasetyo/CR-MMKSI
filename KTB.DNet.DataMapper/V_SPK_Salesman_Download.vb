
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : V_SPK_Salesman_Download Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2013 
'// ---------------------
'// $History      : $
'// Generated on 3/13/2013 - 9:31:02 AM
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

    Public Class V_SPK_Salesman_DownloadMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertV_SPK_Salesman_Download"
        Private m_UpdateStatement As String = "up_UpdateV_SPK_Salesman_Download"
        Private m_RetrieveStatement As String = "up_RetrieveV_SPK_Salesman_Download"
        Private m_RetrieveListStatement As String = "up_RetrieveV_SPK_Salesman_DownloadList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteV_SPK_Salesman_Download"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim v_SPK_Salesman_Download As V_SPK_Salesman_Download = Nothing
            While dr.Read

                v_SPK_Salesman_Download = Me.CreateObject(dr)

            End While

            Return v_SPK_Salesman_Download

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim v_SPK_Salesman_DownloadList As ArrayList = New ArrayList

            While dr.Read
                Dim v_SPK_Salesman_Download As V_SPK_Salesman_Download = Me.CreateObject(dr)
                v_SPK_Salesman_DownloadList.Add(v_SPK_Salesman_Download)
            End While

            Return v_SPK_Salesman_DownloadList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim v_SPK_Salesman_Download As V_SPK_Salesman_Download = CType(obj, V_SPK_Salesman_Download)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@SPKFakturID", DbType.Int32, v_SPK_Salesman_Download.SPKFakturID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim v_SPK_Salesman_Download As V_SPK_Salesman_Download = CType(obj, V_SPK_Salesman_Download)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@SPKFakturID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@FakturStatus", DbType.AnsiString, v_SPK_Salesman_Download.FakturStatus)
            DbCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, v_SPK_Salesman_Download.DealerCode)
            DBCommandWrapper.AddInParameter("@DealerName", DbType.AnsiString, v_SPK_Salesman_Download.DealerName)
            DBCommandWrapper.AddInParameter("@DealerBranchName", DbType.AnsiString, v_SPK_Salesman_Download.DealerBranchName)
            DBCommandWrapper.AddInParameter("@GroupName", DbType.AnsiString, v_SPK_Salesman_Download.GroupName)
            DBCommandWrapper.AddInParameter("@DealerArea", DbType.AnsiString, v_SPK_Salesman_Download.DealerArea)
            DBCommandWrapper.AddInParameter("@Address", DbType.AnsiString, v_SPK_Salesman_Download.Address)
            DbCommandWrapper.AddInParameter("@CityName", DbType.AnsiString, v_SPK_Salesman_Download.CityName)
            DbCommandWrapper.AddInParameter("@SalesmanHeaderID", DbType.Int32, v_SPK_Salesman_Download.SalesmanHeaderID)
            DbCommandWrapper.AddInParameter("@SalesmanCode", DbType.AnsiString, v_SPK_Salesman_Download.SalesmanCode)
            DBCommandWrapper.AddInParameter("@SalesmanName", DbType.AnsiString, v_SPK_Salesman_Download.SalesmanName)
            DBCommandWrapper.AddInParameter("@Level", DbType.AnsiString, v_SPK_Salesman_Download.Level)
            DBCommandWrapper.AddInParameter("@HireDate", DbType.DateTime, v_SPK_Salesman_Download.HireDate)
            DBCommandWrapper.AddInParameter("@JobPosition", DbType.AnsiString, v_SPK_Salesman_Download.JobPosition)
            DBCommandWrapper.AddInParameter("@LeaderId", DbType.Int32, v_SPK_Salesman_Download.LeaderId)
            DbCommandWrapper.AddInParameter("@LeaderName", DbType.AnsiString, v_SPK_Salesman_Download.LeaderName)
            DbCommandWrapper.AddInParameter("@FakturDate", DbType.DateTime, v_SPK_Salesman_Download.FakturDate)
            DbCommandWrapper.AddInParameter("@FakturNumber", DbType.AnsiString, v_SPK_Salesman_Download.FakturNumber)
            DbCommandWrapper.AddInParameter("@SPKDate", DbType.DateTime, v_SPK_Salesman_Download.SPKDate)
            DBCommandWrapper.AddInParameter("@SPKNumber", DbType.AnsiString, v_SPK_Salesman_Download.SPKNumber)
            DBCommandWrapper.AddInParameter("@DealerSPKNumber", DbType.AnsiString, v_SPK_Salesman_Download.DealerSPKNumber)
            DbCommandWrapper.AddInParameter("@ChassisMasterID", DbType.Int32, v_SPK_Salesman_Download.ChassisMasterID)
            DbCommandWrapper.AddInParameter("@ChassisNumber", DbType.AnsiString, v_SPK_Salesman_Download.ChassisNumber)
            DbCommandWrapper.AddInParameter("@CategoryID", DbType.Byte, v_SPK_Salesman_Download.CategoryID)
            DbCommandWrapper.AddInParameter("@CategoryCode", DbType.AnsiString, v_SPK_Salesman_Download.CategoryCode)
            DbCommandWrapper.AddInParameter("@MaterialDescription", DbType.AnsiString, v_SPK_Salesman_Download.MaterialDescription)
            DbCommandWrapper.AddInParameter("@CustomerName", DbType.AnsiString, v_SPK_Salesman_Download.CustomerName)
            DbCommandWrapper.AddInParameter("@PhoneNo", DbType.AnsiString, v_SPK_Salesman_Download.PhoneNo)
            DbCommandWrapper.AddInParameter("@CategoryTeam", DbType.AnsiString, v_SPK_Salesman_Download.CategoryTeam)
            DBCommandWrapper.AddInParameter("@Alamat", DbType.AnsiString, v_SPK_Salesman_Download.Alamat)
            DBCommandWrapper.AddInParameter("@Kelurahan", DbType.AnsiString, v_SPK_Salesman_Download.Kelurahan)
            DBCommandWrapper.AddInParameter("@Kecamatan", DbType.AnsiString, v_SPK_Salesman_Download.Kecamatan)
            DBCommandWrapper.AddInParameter("@Kota", DbType.AnsiString, v_SPK_Salesman_Download.Kota)
            DBCommandWrapper.AddInParameter("@FakturValidateTime", DbType.DateTime, v_SPK_Salesman_Download.FakturValidateTime)
            DBCommandWrapper.AddInParameter("@InvoiceOpen", DbType.DateTime, v_SPK_Salesman_Download.InvoiceOpen)
            DBCommandWrapper.AddInParameter("@ConfirmTime", DbType.DateTime, v_SPK_Salesman_Download.ConfirmTime)

            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetNewID(ByVal dbCommandWrapper As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper) As Integer

            Return CType(dbCommandWrapper.GetParameterValue("@SPKFakturID"), Integer)

        End Function

        Protected Overrides Function GetPagingRetrieveCommand() As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_PagingQuery)
            DbCommandWrapper.AddInParameter("@Table", DbType.String, m_TableName)
            DbCommandWrapper.AddInParameter("@PK", DbType.String, "SPKFakturID")

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
            DbCommandWrapper.AddInParameter("@SPKFakturID", DbType.Int32, id)

            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetUpdateParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim v_SPK_Salesman_Download As V_SPK_Salesman_Download = CType(obj, V_SPK_Salesman_Download)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@SPKFakturID", DbType.Int32, v_SPK_Salesman_Download.SPKFakturID)
            DbCommandWrapper.AddInParameter("@FakturStatus", DbType.AnsiString, v_SPK_Salesman_Download.FakturStatus)
            DbCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, v_SPK_Salesman_Download.DealerCode)
            DBCommandWrapper.AddInParameter("@DealerName", DbType.AnsiString, v_SPK_Salesman_Download.DealerName)
            DBCommandWrapper.AddInParameter("@DealerBranchName", DbType.AnsiString, v_SPK_Salesman_Download.DealerBranchName)
            DBCommandWrapper.AddInParameter("@GroupName", DbType.AnsiString, v_SPK_Salesman_Download.GroupName)
            DBCommandWrapper.AddInParameter("@DealerArea", DbType.AnsiString, v_SPK_Salesman_Download.DealerArea)
            DbCommandWrapper.AddInParameter("@Address", DbType.AnsiString, v_SPK_Salesman_Download.Address)
            DbCommandWrapper.AddInParameter("@CityName", DbType.AnsiString, v_SPK_Salesman_Download.CityName)
            DbCommandWrapper.AddInParameter("@SalesmanHeaderID", DbType.Int32, v_SPK_Salesman_Download.SalesmanHeaderID)
            DbCommandWrapper.AddInParameter("@SalesmanCode", DbType.AnsiString, v_SPK_Salesman_Download.SalesmanCode)
            DBCommandWrapper.AddInParameter("@SalesmanName", DbType.AnsiString, v_SPK_Salesman_Download.SalesmanName)
            DBCommandWrapper.AddInParameter("@Level", DbType.AnsiString, v_SPK_Salesman_Download.Level)
            DBCommandWrapper.AddInParameter("@HireDate", DbType.DateTime, v_SPK_Salesman_Download.HireDate)
            DBCommandWrapper.AddInParameter("@JobPosition", DbType.AnsiString, v_SPK_Salesman_Download.JobPosition)
            DbCommandWrapper.AddInParameter("@LeaderId", DbType.Int32, v_SPK_Salesman_Download.LeaderId)
            DbCommandWrapper.AddInParameter("@LeaderName", DbType.AnsiString, v_SPK_Salesman_Download.LeaderName)
            DbCommandWrapper.AddInParameter("@FakturDate", DbType.DateTime, v_SPK_Salesman_Download.FakturDate)
            DbCommandWrapper.AddInParameter("@FakturNumber", DbType.AnsiString, v_SPK_Salesman_Download.FakturNumber)
            DbCommandWrapper.AddInParameter("@SPKDate", DbType.DateTime, v_SPK_Salesman_Download.SPKDate)
            DBCommandWrapper.AddInParameter("@SPKNumber", DbType.AnsiString, v_SPK_Salesman_Download.SPKNumber)
            DBCommandWrapper.AddInParameter("@DealerSPKNumber", DbType.AnsiString, v_SPK_Salesman_Download.DealerSPKNumber)
            DbCommandWrapper.AddInParameter("@ChassisMasterID", DbType.Int32, v_SPK_Salesman_Download.ChassisMasterID)
            DbCommandWrapper.AddInParameter("@ChassisNumber", DbType.AnsiString, v_SPK_Salesman_Download.ChassisNumber)
            DbCommandWrapper.AddInParameter("@CategoryID", DbType.Byte, v_SPK_Salesman_Download.CategoryID)
            DbCommandWrapper.AddInParameter("@CategoryCode", DbType.AnsiString, v_SPK_Salesman_Download.CategoryCode)
            DbCommandWrapper.AddInParameter("@MaterialDescription", DbType.AnsiString, v_SPK_Salesman_Download.MaterialDescription)
            DbCommandWrapper.AddInParameter("@CustomerName", DbType.AnsiString, v_SPK_Salesman_Download.CustomerName)
            DbCommandWrapper.AddInParameter("@PhoneNo", DbType.AnsiString, v_SPK_Salesman_Download.PhoneNo)
            DbCommandWrapper.AddInParameter("@CategoryTeam", DbType.AnsiString, v_SPK_Salesman_Download.CategoryTeam)
            DBCommandWrapper.AddInParameter("@Alamat", DbType.AnsiString, v_SPK_Salesman_Download.Alamat)
            DBCommandWrapper.AddInParameter("@Kelurahan", DbType.AnsiString, v_SPK_Salesman_Download.Kelurahan)
            DBCommandWrapper.AddInParameter("@Kecamatan", DbType.AnsiString, v_SPK_Salesman_Download.Kecamatan)
            DBCommandWrapper.AddInParameter("@Kota", DbType.AnsiString, v_SPK_Salesman_Download.Kota)
            DBCommandWrapper.AddInParameter("@FakturValidateTime", DbType.DateTime, v_SPK_Salesman_Download.FakturValidateTime)
            DBCommandWrapper.AddInParameter("@InvoiceOpen", DbType.DateTime, v_SPK_Salesman_Download.InvoiceOpen)
            DBCommandWrapper.AddInParameter("@ConfirmTime", DbType.DateTime, v_SPK_Salesman_Download.ConfirmTime)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As V_SPK_Salesman_Download

            Dim v_SPK_Salesman_Download As V_SPK_Salesman_Download = New V_SPK_Salesman_Download

            v_SPK_Salesman_Download.SPKFakturID = CType(dr("SPKFakturID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("FakturStatus")) Then v_SPK_Salesman_Download.FakturStatus = dr("FakturStatus").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DealerCode")) Then v_SPK_Salesman_Download.DealerCode = dr("DealerCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DealerName")) Then v_SPK_Salesman_Download.DealerName = dr("DealerName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DealerBranchName")) Then v_SPK_Salesman_Download.DealerBranchName = dr("DealerBranchName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("GroupName")) Then v_SPK_Salesman_Download.GroupName = dr("GroupName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DealerArea")) Then v_SPK_Salesman_Download.DealerArea = dr("DealerArea").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Address")) Then v_SPK_Salesman_Download.Address = dr("Address").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CityName")) Then v_SPK_Salesman_Download.CityName = dr("CityName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SalesmanHeaderID")) Then v_SPK_Salesman_Download.SalesmanHeaderID = CType(dr("SalesmanHeaderID"), Int32)
            If Not dr.IsDBNull(dr.GetOrdinal("SalesmanCode")) Then v_SPK_Salesman_Download.SalesmanCode = dr("SalesmanCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SalesmanName")) Then v_SPK_Salesman_Download.SalesmanName = dr("SalesmanName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Level")) Then v_SPK_Salesman_Download.Level = dr("Level").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("HireDate")) Then v_SPK_Salesman_Download.HireDate = CType(dr("HireDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("JobPosition")) Then v_SPK_Salesman_Download.JobPosition = dr("JobPosition").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LeaderId")) Then v_SPK_Salesman_Download.LeaderId = CType(dr("LeaderId"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("LeaderName")) Then v_SPK_Salesman_Download.LeaderName = dr("LeaderName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("FakturDate")) Then v_SPK_Salesman_Download.FakturDate = CType(dr("FakturDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("FakturNumber")) Then v_SPK_Salesman_Download.FakturNumber = dr("FakturNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SPKDate")) Then v_SPK_Salesman_Download.SPKDate = CType(dr("SPKDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("SPKNumber")) Then v_SPK_Salesman_Download.SPKNumber = dr("SPKNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DealerSPKNumber")) Then v_SPK_Salesman_Download.DealerSPKNumber = dr("DealerSPKNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ChassisMasterID")) Then v_SPK_Salesman_Download.ChassisMasterID = CType(dr("ChassisMasterID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("ChassisNumber")) Then v_SPK_Salesman_Download.ChassisNumber = dr("ChassisNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CategoryID")) Then v_SPK_Salesman_Download.CategoryID = CType(dr("CategoryID"), Byte)
            If Not dr.IsDBNull(dr.GetOrdinal("CategoryCode")) Then v_SPK_Salesman_Download.CategoryCode = dr("CategoryCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("MaterialDescription")) Then v_SPK_Salesman_Download.MaterialDescription = dr("MaterialDescription").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CustomerName")) Then v_SPK_Salesman_Download.CustomerName = dr("CustomerName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PhoneNo")) Then v_SPK_Salesman_Download.PhoneNo = dr("PhoneNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CategoryTeam")) Then v_SPK_Salesman_Download.CategoryTeam = dr("CategoryTeam").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Alamat")) Then v_SPK_Salesman_Download.Alamat = dr("Alamat").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Kelurahan")) Then v_SPK_Salesman_Download.Kelurahan = dr("Kelurahan").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Kecamatan")) Then v_SPK_Salesman_Download.Kecamatan = dr("Kecamatan").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Kota")) Then v_SPK_Salesman_Download.Kota = dr("Kota").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("FakturValidateTime")) Then v_SPK_Salesman_Download.FakturValidateTime = CType(dr("FakturValidateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("InvoiceOpen")) Then v_SPK_Salesman_Download.InvoiceOpen = CType(dr("InvoiceOpen"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("ConfirmTime")) Then v_SPK_Salesman_Download.ConfirmTime = CType(dr("ConfirmTime"), DateTime)


            Return v_SPK_Salesman_Download

        End Function

        Private Sub SetTableName()

            If Not (GetType(V_SPK_Salesman_Download) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(V_SPK_Salesman_Download), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(V_SPK_Salesman_Download).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

