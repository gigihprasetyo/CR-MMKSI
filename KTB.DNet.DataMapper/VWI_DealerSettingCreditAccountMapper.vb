
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : VWI_DealerSettingCreditAccount Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 20/08/2018 - 12:40:24
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

    Public Class VWI_DealerSettingCreditAccountMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertVWI_DealerSettingCreditAccount"
        Private m_UpdateStatement As String = "up_UpdateVWI_DealerSettingCreditAccount"
        Private m_RetrieveStatement As String = "up_RetrieveVWI_DealerSettingCreditAccount"
        Private m_RetrieveListStatement As String = "up_RetrieveVWI_DealerSettingCreditAccountList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteVWI_DealerSettingCreditAccount"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim VWI_DealerSettingCreditAccount As VWI_DealerSettingCreditAccount = Nothing
            While dr.Read

                VWI_DealerSettingCreditAccount = Me.CreateObject(dr)

            End While

            Return VWI_DealerSettingCreditAccount

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim VWI_DealerSettingCreditAccountList As ArrayList = New ArrayList

            While dr.Read
                Dim VWI_DealerSettingCreditAccount As VWI_DealerSettingCreditAccount = Me.CreateObject(dr)
                VWI_DealerSettingCreditAccountList.Add(VWI_DealerSettingCreditAccount)
            End While

            Return VWI_DealerSettingCreditAccountList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim VWI_DealerSettingCreditAccount As VWI_DealerSettingCreditAccount = CType(obj, VWI_DealerSettingCreditAccount)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@id", DbType.Int32, VWI_DealerSettingCreditAccount.id)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim VWI_DealerSettingCreditAccount As VWI_DealerSettingCreditAccount = CType(obj, VWI_DealerSettingCreditAccount)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@id", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@DealerId", DbType.Int16, VWI_DealerSettingCreditAccount.DealerId)
            DbCommandWrapper.AddInParameter("@DealerGroupId", DbType.Int16, VWI_DealerSettingCreditAccount.DealerGroupId)
            DbCommandWrapper.AddInParameter("@ProvinceId", DbType.Int16, VWI_DealerSettingCreditAccount.ProvinceId)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, VWI_DealerSettingCreditAccount.RowStatus)
            DbCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, VWI_DealerSettingCreditAccount.DealerCode)
            DbCommandWrapper.AddInParameter("@DealerName", DbType.AnsiString, VWI_DealerSettingCreditAccount.DealerName)
            DbCommandWrapper.AddInParameter("@CityName", DbType.AnsiString, VWI_DealerSettingCreditAccount.CityName)
            DbCommandWrapper.AddInParameter("@ProvinceName", DbType.AnsiString, VWI_DealerSettingCreditAccount.ProvinceName)
            DbCommandWrapper.AddInParameter("@GroupName", DbType.AnsiString, VWI_DealerSettingCreditAccount.GroupName)
            DbCommandWrapper.AddInParameter("@SearchTerm1", DbType.AnsiString, VWI_DealerSettingCreditAccount.SearchTerm1)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int32, VWI_DealerSettingCreditAccount.Status)
            DbCommandWrapper.AddInParameter("@SalesUnitFlag", DbType.AnsiString, VWI_DealerSettingCreditAccount.SalesUnitFlag)
            DbCommandWrapper.AddInParameter("@ServiceFlag", DbType.AnsiString, VWI_DealerSettingCreditAccount.ServiceFlag)
            DbCommandWrapper.AddInParameter("@SparepartFlag", DbType.AnsiString, VWI_DealerSettingCreditAccount.SparepartFlag)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@TermOfPaymentID", DbType.Int32, VWI_DealerSettingCreditAccount.TermOfPaymentID)
            DbCommandWrapper.AddInParameter("@CreditAccount", DbType.AnsiString, VWI_DealerSettingCreditAccount.CreditAccount)
            DbCommandWrapper.AddInParameter("@KelipatanPembayaran", DbType.Int32, VWI_DealerSettingCreditAccount.KelipatanPembayaran)


            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetNewID(ByVal dbCommandWrapper As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper) As Integer

            Return CType(dbCommandWrapper.GetParameterValue("@id"), Integer)

        End Function

        Protected Overrides Function GetPagingRetrieveCommand() As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_PagingQuery)
            DbCommandWrapper.AddInParameter("@Table", DbType.String, m_TableName)
            DbCommandWrapper.AddInParameter("@PK", DbType.String, "id")

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
            DbCommandWrapper.AddInParameter("@id", DbType.Int32, id)

            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetUpdateParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim VWI_DealerSettingCreditAccount As VWI_DealerSettingCreditAccount = CType(obj, VWI_DealerSettingCreditAccount)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@id", DbType.Int32, VWI_DealerSettingCreditAccount.id)
            DbCommandWrapper.AddInParameter("@DealerId", DbType.Int16, VWI_DealerSettingCreditAccount.DealerId)
            DbCommandWrapper.AddInParameter("@DealerGroupId", DbType.Int16, VWI_DealerSettingCreditAccount.DealerGroupId)
            DbCommandWrapper.AddInParameter("@ProvinceId", DbType.Int16, VWI_DealerSettingCreditAccount.ProvinceId)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, VWI_DealerSettingCreditAccount.RowStatus)
            DbCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, VWI_DealerSettingCreditAccount.DealerCode)
            DbCommandWrapper.AddInParameter("@DealerName", DbType.AnsiString, VWI_DealerSettingCreditAccount.DealerName)
            DbCommandWrapper.AddInParameter("@CityName", DbType.AnsiString, VWI_DealerSettingCreditAccount.CityName)
            DbCommandWrapper.AddInParameter("@ProvinceName", DbType.AnsiString, VWI_DealerSettingCreditAccount.ProvinceName)
            DbCommandWrapper.AddInParameter("@GroupName", DbType.AnsiString, VWI_DealerSettingCreditAccount.GroupName)
            DbCommandWrapper.AddInParameter("@SearchTerm1", DbType.AnsiString, VWI_DealerSettingCreditAccount.SearchTerm1)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int32, VWI_DealerSettingCreditAccount.Status)
            DbCommandWrapper.AddInParameter("@SalesUnitFlag", DbType.AnsiString, VWI_DealerSettingCreditAccount.SalesUnitFlag)
            DbCommandWrapper.AddInParameter("@ServiceFlag", DbType.AnsiString, VWI_DealerSettingCreditAccount.ServiceFlag)
            DbCommandWrapper.AddInParameter("@SparepartFlag", DbType.AnsiString, VWI_DealerSettingCreditAccount.SparepartFlag)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@TermOfPaymentID", DbType.Int32, VWI_DealerSettingCreditAccount.TermOfPaymentID)
            DbCommandWrapper.AddInParameter("@CreditAccount", DbType.AnsiString, VWI_DealerSettingCreditAccount.CreditAccount)
            DbCommandWrapper.AddInParameter("@KelipatanPembayaran", DbType.Int32, VWI_DealerSettingCreditAccount.KelipatanPembayaran)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As VWI_DealerSettingCreditAccount

            Dim VWI_DealerSettingCreditAccount As VWI_DealerSettingCreditAccount = New VWI_DealerSettingCreditAccount

            'VWI_DealerSettingCreditAccount.id = CType(dr("id"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("id")) Then VWI_DealerSettingCreditAccount.id = CType(dr("id"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerId")) Then VWI_DealerSettingCreditAccount.DealerId = CType(dr("DealerId"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerGroupId")) Then VWI_DealerSettingCreditAccount.DealerGroupId = CType(dr("DealerGroupId"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("ProvinceId")) Then VWI_DealerSettingCreditAccount.ProvinceId = CType(dr("ProvinceId"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then VWI_DealerSettingCreditAccount.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerCode")) Then VWI_DealerSettingCreditAccount.DealerCode = dr("DealerCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DealerName")) Then VWI_DealerSettingCreditAccount.DealerName = dr("DealerName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CityName")) Then VWI_DealerSettingCreditAccount.CityName = dr("CityName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ProvinceName")) Then VWI_DealerSettingCreditAccount.ProvinceName = dr("ProvinceName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("GroupName")) Then VWI_DealerSettingCreditAccount.GroupName = dr("GroupName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SearchTerm1")) Then VWI_DealerSettingCreditAccount.SearchTerm1 = dr("SearchTerm1").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then VWI_DealerSettingCreditAccount.Status = CType(dr("Status"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("SalesUnitFlag")) Then VWI_DealerSettingCreditAccount.SalesUnitFlag = dr("SalesUnitFlag").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ServiceFlag")) Then VWI_DealerSettingCreditAccount.ServiceFlag = dr("ServiceFlag").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SparepartFlag")) Then VWI_DealerSettingCreditAccount.SparepartFlag = dr("SparepartFlag").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then VWI_DealerSettingCreditAccount.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("TermOfPaymentID")) Then VWI_DealerSettingCreditAccount.TermOfPaymentID = CType(dr("TermOfPaymentID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("CreditAccount")) Then VWI_DealerSettingCreditAccount.CreditAccount = dr("CreditAccount").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("KelipatanPembayaran")) Then VWI_DealerSettingCreditAccount.KelipatanPembayaran = CType(dr("KelipatanPembayaran"), Integer)

            Return VWI_DealerSettingCreditAccount

        End Function

        Private Sub SetTableName()

            If Not (GetType(VWI_DealerSettingCreditAccount) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(VWI_DealerSettingCreditAccount), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(VWI_DealerSettingCreditAccount).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

