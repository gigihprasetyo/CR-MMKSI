#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : VWI_Dealer Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2020 
'// ---------------------
'// $History      : $
'// Generated on 9/18/2020 - 4:12:22 PM
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

    Public Class VWI_DealerMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertVWI_Dealer"
        Private m_UpdateStatement As String = "up_UpdateVWI_Dealer"
        Private m_RetrieveStatement As String = "up_RetrieveVWI_Dealer"
        Private m_RetrieveListStatement As String = "up_RetrieveVWI_DealerList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteVWI_Dealer"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim vWI_Dealer As VWI_Dealer = Nothing
            While dr.Read

                vWI_Dealer = Me.CreateObject(dr)

            End While

            Return vWI_Dealer

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim vWI_DealerList As ArrayList = New ArrayList

            While dr.Read
                Dim vWI_Dealer As VWI_Dealer = Me.CreateObject(dr)
                vWI_DealerList.Add(vWI_Dealer)
            End While

            Return vWI_DealerList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim vWI_Dealer As VWI_Dealer = CType(obj, VWI_Dealer)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@id", DbType.Int16, vWI_Dealer.id)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim vWI_Dealer As VWI_Dealer = CType(obj, VWI_Dealer)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@id", DbType.Int16, 2)
            DbCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, vWI_Dealer.DealerCode)
            DbCommandWrapper.AddInParameter("@DealerName", DbType.AnsiString, vWI_Dealer.DealerName)
            DbCommandWrapper.AddInParameter("@Term", DbType.AnsiString, vWI_Dealer.Term)
            DbCommandWrapper.AddInParameter("@kategori", DbType.AnsiString, vWI_Dealer.kategori)
            DbCommandWrapper.AddInParameter("@Status", DbType.AnsiString, vWI_Dealer.Status)
            DbCommandWrapper.AddInParameter("@Address", DbType.AnsiString, vWI_Dealer.Address)
            DbCommandWrapper.AddInParameter("@CityName", DbType.AnsiString, vWI_Dealer.CityName)
            DbCommandWrapper.AddInParameter("@ProvinceName", DbType.AnsiString, vWI_Dealer.ProvinceName)
            DbCommandWrapper.AddInParameter("@Phone", DbType.AnsiString, vWI_Dealer.Phone)
            DbCommandWrapper.AddInParameter("@SalesUnitFlag", DbType.AnsiString, vWI_Dealer.SalesUnitFlag)
            DbCommandWrapper.AddInParameter("@ServiceFlag", DbType.AnsiString, vWI_Dealer.ServiceFlag)
            DbCommandWrapper.AddInParameter("@SparepartFlag", DbType.AnsiString, vWI_Dealer.SparepartFlag)
            DbCommandWrapper.AddInParameter("@SystemID", DbType.Int16, vWI_Dealer.SystemID)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, vWI_Dealer.RowStatus)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@NicknameDigital", DbType.AnsiString, vWI_Dealer.NicknameDigital)
            DbCommandWrapper.AddInParameter("@NicknameEcommerce", DbType.AnsiString, vWI_Dealer.NicknameEcommerce)
            DbCommandWrapper.AddInParameter("@Longitude", DbType.AnsiString, vWI_Dealer.Longitude)
            DbCommandWrapper.AddInParameter("@Latitude", DbType.AnsiString, vWI_Dealer.Latitude)
            DbCommandWrapper.AddInParameter("@IsPublish", DbType.Int16, vWI_Dealer.IsPublish)


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

            Dim vWI_Dealer As VWI_Dealer = CType(obj, VWI_Dealer)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@id", DbType.Int16, vWI_Dealer.id)
            DbCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, vWI_Dealer.DealerCode)
            DbCommandWrapper.AddInParameter("@DealerName", DbType.AnsiString, vWI_Dealer.DealerName)
            DbCommandWrapper.AddInParameter("@Term", DbType.AnsiString, vWI_Dealer.Term)
            DbCommandWrapper.AddInParameter("@kategori", DbType.AnsiString, vWI_Dealer.kategori)
            DbCommandWrapper.AddInParameter("@Status", DbType.AnsiString, vWI_Dealer.Status)
            DbCommandWrapper.AddInParameter("@Address", DbType.AnsiString, vWI_Dealer.Address)
            DbCommandWrapper.AddInParameter("@CityName", DbType.AnsiString, vWI_Dealer.CityName)
            DbCommandWrapper.AddInParameter("@ProvinceName", DbType.AnsiString, vWI_Dealer.ProvinceName)
            DbCommandWrapper.AddInParameter("@Phone", DbType.AnsiString, vWI_Dealer.Phone)
            DbCommandWrapper.AddInParameter("@SalesUnitFlag", DbType.AnsiString, vWI_Dealer.SalesUnitFlag)
            DbCommandWrapper.AddInParameter("@ServiceFlag", DbType.AnsiString, vWI_Dealer.ServiceFlag)
            DbCommandWrapper.AddInParameter("@SparepartFlag", DbType.AnsiString, vWI_Dealer.SparepartFlag)
            DbCommandWrapper.AddInParameter("@SystemID", DbType.Int16, vWI_Dealer.SystemID)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, vWI_Dealer.RowStatus)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@NicknameDigital", DbType.AnsiString, vWI_Dealer.NicknameDigital)
            DbCommandWrapper.AddInParameter("@NicknameEcommerce", DbType.AnsiString, vWI_Dealer.NicknameEcommerce)
            DbCommandWrapper.AddInParameter("@Longitude", DbType.AnsiString, vWI_Dealer.Longitude)
            DbCommandWrapper.AddInParameter("@Latitude", DbType.AnsiString, vWI_Dealer.Latitude)
            DbCommandWrapper.AddInParameter("@IsPublish", DbType.Int16, vWI_Dealer.IsPublish)


            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As VWI_Dealer

            Dim vWI_Dealer As VWI_Dealer = New VWI_Dealer

            vWI_Dealer.id = CType(dr("id"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerCode")) Then vWI_Dealer.DealerCode = dr("DealerCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DealerName")) Then vWI_Dealer.DealerName = dr("DealerName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Term")) Then vWI_Dealer.Term = dr("Term").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("kategori")) Then vWI_Dealer.kategori = dr("kategori").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then vWI_Dealer.Status = dr("Status").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Address")) Then vWI_Dealer.Address = dr("Address").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CityName")) Then vWI_Dealer.CityName = dr("CityName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ProvinceName")) Then vWI_Dealer.ProvinceName = dr("ProvinceName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Phone")) Then vWI_Dealer.Phone = dr("Phone").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SalesUnitFlag")) Then vWI_Dealer.SalesUnitFlag = dr("SalesUnitFlag").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ServiceFlag")) Then vWI_Dealer.ServiceFlag = dr("ServiceFlag").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SparepartFlag")) Then vWI_Dealer.SparepartFlag = dr("SparepartFlag").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SystemID")) Then vWI_Dealer.SystemID = CType(dr("SystemID"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then vWI_Dealer.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then vWI_Dealer.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then vWI_Dealer.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            If Not dr.IsDBNull(dr.GetOrdinal("NicknameDigital")) Then vWI_Dealer.NicknameDigital = dr("NicknameDigital").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("NicknameEcommerce")) Then vWI_Dealer.NicknameEcommerce = dr("NicknameEcommerce").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Longitude")) Then vWI_Dealer.Longitude = dr("Longitude").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Latitude")) Then vWI_Dealer.Latitude = dr("Latitude").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("IsPublish")) Then vWI_Dealer.IsPublish = CType(dr("IsPublish"), Integer)

            Return vWI_Dealer

        End Function

        Private Sub SetTableName()

            If Not (GetType(VWI_Dealer) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(VWI_Dealer), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(VWI_Dealer).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace
