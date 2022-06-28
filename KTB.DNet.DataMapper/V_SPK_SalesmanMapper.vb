
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : V_SPK_Salesman Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2012 
'// ---------------------
'// $History      : $
'// Generated on 4/19/2012 - 9:26:31 AM
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

    Public Class V_SPK_SalesmanMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertV_SPK_Salesman"
        Private m_UpdateStatement As String = "up_UpdateV_SPK_Salesman"
        Private m_RetrieveStatement As String = "up_RetrieveV_SPK_Salesman"
        Private m_RetrieveListStatement As String = "up_RetrieveV_SPK_SalesmanList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteV_SPK_Salesman"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim v_SPK_Salesman As V_SPK_Salesman = Nothing
            While dr.Read

                v_SPK_Salesman = Me.CreateObject(dr)

            End While

            Return v_SPK_Salesman

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim v_SPK_SalesmanList As ArrayList = New ArrayList

            While dr.Read
                Dim v_SPK_Salesman As V_SPK_Salesman = Me.CreateObject(dr)
                v_SPK_SalesmanList.Add(v_SPK_Salesman)
            End While

            Return v_SPK_SalesmanList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim v_SPK_Salesman As V_SPK_Salesman = CType(obj, V_SPK_Salesman)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@SPKFakturID", DbType.Int32, v_SPK_Salesman.SPKFakturID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim v_SPK_Salesman As V_SPK_Salesman = CType(obj, V_SPK_Salesman)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@SPKFakturID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@FakturStatus", DbType.AnsiString, v_SPK_Salesman.FakturStatus)
            DbCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, v_SPK_Salesman.DealerCode)
            DbCommandWrapper.AddInParameter("@DealerName", DbType.AnsiString, v_SPK_Salesman.DealerName)
            DbCommandWrapper.AddInParameter("@Address", DbType.AnsiString, v_SPK_Salesman.Address)
            DbCommandWrapper.AddInParameter("@CityName", DbType.AnsiString, v_SPK_Salesman.CityName)
            DbCommandWrapper.AddInParameter("@SalesmanHeaderID", DbType.Int32, v_SPK_Salesman.SalesmanHeaderID)
            DBCommandWrapper.AddInParameter("@SalesmanCode", DbType.AnsiString, v_SPK_Salesman.SalesmanCode)
            DBCommandWrapper.AddInParameter("@SalesmanName", DbType.AnsiString, v_SPK_Salesman.SalesmanName)
            DbCommandWrapper.AddInParameter("@LeaderId", DbType.Int32, v_SPK_Salesman.LeaderId)
            DbCommandWrapper.AddInParameter("@LeaderName", DbType.AnsiString, v_SPK_Salesman.LeaderName)
            DbCommandWrapper.AddInParameter("@FakturDate", DbType.DateTime, v_SPK_Salesman.FakturDate)
            DbCommandWrapper.AddInParameter("@FakturNumber", DbType.AnsiString, v_SPK_Salesman.FakturNumber)
            DbCommandWrapper.AddInParameter("@SPKDate", DbType.DateTime, v_SPK_Salesman.SPKDate)
            DbCommandWrapper.AddInParameter("@SPKNumber", DbType.AnsiString, v_SPK_Salesman.SPKNumber)
            DbCommandWrapper.AddInParameter("@ChassisMasterID", DbType.Int32, v_SPK_Salesman.ChassisMasterID)
            DbCommandWrapper.AddInParameter("@ChassisNumber", DbType.AnsiString, v_SPK_Salesman.ChassisNumber)
            DBCommandWrapper.AddInParameter("@CategoryID", DbType.Byte, v_SPK_Salesman.CategoryID)
            DBCommandWrapper.AddInParameter("@CategoryCode", DbType.AnsiString, v_SPK_Salesman.CategoryCode)
            DbCommandWrapper.AddInParameter("@MaterialDescription", DbType.AnsiString, v_SPK_Salesman.MaterialDescription)
            DbCommandWrapper.AddInParameter("@CustomerName", DbType.AnsiString, v_SPK_Salesman.CustomerName)
            DBCommandWrapper.AddInParameter("@PhoneNo", DbType.AnsiString, v_SPK_Salesman.PhoneNo)
            DBCommandWrapper.AddInParameter("@CategoryTeam", DbType.AnsiString, v_SPK_Salesman.CategoryTeam)
            DBCommandWrapper.AddInParameter("@Alamat", DbType.AnsiString, v_SPK_Salesman.Alamat)
            DBCommandWrapper.AddInParameter("@FakturValidateTime", DbType.DateTime, v_SPK_Salesman.FakturValidateTime)
            DBCommandWrapper.AddInParameter("@InvoiceOpen", DbType.DateTime, v_SPK_Salesman.InvoiceOpen)
            DBCommandWrapper.AddInParameter("@ConfirmTime", DbType.DateTime, v_SPK_Salesman.ConfirmTime)


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

            Dim v_SPK_Salesman As V_SPK_Salesman = CType(obj, V_SPK_Salesman)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@SPKFakturID", DbType.Int32, v_SPK_Salesman.SPKFakturID)
            DbCommandWrapper.AddInParameter("@FakturStatus", DbType.AnsiString, v_SPK_Salesman.FakturStatus)
            DbCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, v_SPK_Salesman.DealerCode)
            DbCommandWrapper.AddInParameter("@DealerName", DbType.AnsiString, v_SPK_Salesman.DealerName)
            DbCommandWrapper.AddInParameter("@Address", DbType.AnsiString, v_SPK_Salesman.Address)
            DbCommandWrapper.AddInParameter("@CityName", DbType.AnsiString, v_SPK_Salesman.CityName)
            DbCommandWrapper.AddInParameter("@SalesmanHeaderID", DbType.Int32, v_SPK_Salesman.SalesmanHeaderID)
            DBCommandWrapper.AddInParameter("@SalesmanCode", DbType.AnsiString, v_SPK_Salesman.SalesmanCode)
            DBCommandWrapper.AddInParameter("@SalesmanName", DbType.AnsiString, v_SPK_Salesman.SalesmanName)
            DbCommandWrapper.AddInParameter("@LeaderId", DbType.Int32, v_SPK_Salesman.LeaderId)
            DbCommandWrapper.AddInParameter("@LeaderName", DbType.AnsiString, v_SPK_Salesman.LeaderName)
            DbCommandWrapper.AddInParameter("@FakturDate", DbType.DateTime, v_SPK_Salesman.FakturDate)
            DbCommandWrapper.AddInParameter("@FakturNumber", DbType.AnsiString, v_SPK_Salesman.FakturNumber)
            DbCommandWrapper.AddInParameter("@SPKDate", DbType.DateTime, v_SPK_Salesman.SPKDate)
            DbCommandWrapper.AddInParameter("@SPKNumber", DbType.AnsiString, v_SPK_Salesman.SPKNumber)
            DbCommandWrapper.AddInParameter("@ChassisMasterID", DbType.Int32, v_SPK_Salesman.ChassisMasterID)
            DbCommandWrapper.AddInParameter("@ChassisNumber", DbType.AnsiString, v_SPK_Salesman.ChassisNumber)
            DbCommandWrapper.AddInParameter("@CategoryID", DbType.Byte, v_SPK_Salesman.CategoryID)
            DbCommandWrapper.AddInParameter("@MaterialDescription", DbType.AnsiString, v_SPK_Salesman.MaterialDescription)
            DbCommandWrapper.AddInParameter("@CustomerName", DbType.AnsiString, v_SPK_Salesman.CustomerName)
            DBCommandWrapper.AddInParameter("@PhoneNo", DbType.AnsiString, v_SPK_Salesman.PhoneNo)
            DBCommandWrapper.AddInParameter("@CategoryTeam", DbType.AnsiString, v_SPK_Salesman.CategoryTeam)
            DBCommandWrapper.AddInParameter("@Alamat", DbType.AnsiString, v_SPK_Salesman.Alamat)
            DBCommandWrapper.AddInParameter("@FakturValidateTime", DbType.DateTime, v_SPK_Salesman.FakturValidateTime)
            DBCommandWrapper.AddInParameter("@InvoiceOpen", DbType.DateTime, v_SPK_Salesman.InvoiceOpen)
            DBCommandWrapper.AddInParameter("@ConfirmTime", DbType.DateTime, v_SPK_Salesman.ConfirmTime)

            Return DbCommandWrapper

        End Function

#End Region

#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As V_SPK_Salesman

            Dim v_SPK_Salesman As V_SPK_Salesman = New V_SPK_Salesman

            v_SPK_Salesman.SPKFakturID = CType(dr("SPKFakturID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("FakturStatus")) Then v_SPK_Salesman.FakturStatus = dr("FakturStatus").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DealerCode")) Then v_SPK_Salesman.DealerCode = dr("DealerCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DealerName")) Then v_SPK_Salesman.DealerName = dr("DealerName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Address")) Then v_SPK_Salesman.Address = dr("Address").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CityName")) Then v_SPK_Salesman.CityName = dr("CityName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SalesmanHeaderID")) Then v_SPK_Salesman.SalesmanHeaderID = CType(dr("SalesmanHeaderID"), Int32)
            If Not dr.IsDBNull(dr.GetOrdinal("SalesmanCode")) Then v_SPK_Salesman.SalesmanCode = dr("SalesmanCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SalesmanName")) Then v_SPK_Salesman.SalesmanName = dr("SalesmanName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LeaderId")) Then v_SPK_Salesman.LeaderId = CType(dr("LeaderId"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("LeaderName")) Then v_SPK_Salesman.LeaderName = dr("LeaderName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("FakturDate")) Then v_SPK_Salesman.FakturDate = CType(dr("FakturDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("FakturNumber")) Then v_SPK_Salesman.FakturNumber = dr("FakturNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SPKDate")) Then v_SPK_Salesman.SPKDate = CType(dr("SPKDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("SPKNumber")) Then v_SPK_Salesman.SPKNumber = dr("SPKNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ChassisMasterID")) Then v_SPK_Salesman.ChassisMasterID = CType(dr("ChassisMasterID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("ChassisNumber")) Then v_SPK_Salesman.ChassisNumber = dr("ChassisNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CategoryID")) Then v_SPK_Salesman.CategoryID = CType(dr("CategoryID"), Byte)
            If Not dr.IsDBNull(dr.GetOrdinal("CategoryCode")) Then v_SPK_Salesman.CategoryCode = dr("CategoryCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("MaterialDescription")) Then v_SPK_Salesman.MaterialDescription = dr("MaterialDescription").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CustomerName")) Then v_SPK_Salesman.CustomerName = dr("CustomerName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PhoneNo")) Then v_SPK_Salesman.PhoneNo = dr("PhoneNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CategoryTeam")) Then v_SPK_Salesman.CategoryTeam = dr("CategoryTeam").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Alamat")) Then v_SPK_Salesman.Alamat = dr("Alamat").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("FakturValidateTime")) Then v_SPK_Salesman.FakturValidateTime = CType(dr("FakturValidateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("InvoiceOpen")) Then v_SPK_Salesman.InvoiceOpen = CType(dr("InvoiceOpen"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("ConfirmTime")) Then v_SPK_Salesman.ConfirmTime = CType(dr("ConfirmTime"), DateTime)

            Return v_SPK_Salesman

        End Function

        Private Sub SetTableName()

            If Not (GetType(V_SPK_Salesman) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(V_SPK_Salesman), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(V_SPK_Salesman).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

