
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : Leasing Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 3/2/2018 - 1:47:50 PM
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

    Public Class LeasingMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertLeasing"
        Private m_UpdateStatement As String = "up_UpdateLeasing"
        Private m_RetrieveStatement As String = "up_RetrieveLeasing"
        Private m_RetrieveListStatement As String = "up_RetrieveLeasingList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteLeasing"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim leasing As Leasing = Nothing
            While dr.Read

                leasing = Me.CreateObject(dr)

            End While

            Return leasing

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim leasingList As ArrayList = New ArrayList

            While dr.Read
                Dim leasing As Leasing = Me.CreateObject(dr)
                leasingList.Add(leasing)
            End While

            Return leasingList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim leasing As Leasing = CType(obj, Leasing)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, leasing.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim leasing As Leasing = CType(obj, Leasing)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@LeasingGroupName", DbType.AnsiString, leasing.LeasingGroupName)
            DbCommandWrapper.AddInParameter("@LeasingCode", DbType.AnsiString, leasing.LeasingCode)
            DbCommandWrapper.AddInParameter("@LeasingName", DbType.AnsiString, leasing.LeasingName)
            DbCommandWrapper.AddInParameter("@City", DbType.AnsiString, leasing.City)
            DbCommandWrapper.AddInParameter("@Alamat", DbType.AnsiString, leasing.Alamat)
            DbCommandWrapper.AddInParameter("@Kelurahan", DbType.AnsiString, leasing.Kelurahan)
            DbCommandWrapper.AddInParameter("@Kecamatan", DbType.AnsiString, leasing.Kecamatan)
            DbCommandWrapper.AddInParameter("@Province", DbType.AnsiString, leasing.Province)
            DbCommandWrapper.AddInParameter("@PostalCode", DbType.AnsiString, leasing.PostalCode)
            DbCommandWrapper.AddInParameter("@PhoneNo", DbType.AnsiString, leasing.PhoneNo)
            DbCommandWrapper.AddInParameter("@Fax", DbType.AnsiString, leasing.Fax)
            DbCommandWrapper.AddInParameter("@WebSite", DbType.AnsiString, leasing.WebSite)
            DbCommandWrapper.AddInParameter("@Email", DbType.String, leasing.Email)
            DbCommandWrapper.AddInParameter("@ContactPerson", DbType.AnsiString, leasing.ContactPerson)
            DbCommandWrapper.AddInParameter("@HP", DbType.AnsiString, leasing.HP)
            DbCommandWrapper.AddInParameter("@Status", DbType.Byte, leasing.Status)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, leasing.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, leasing.LastUpdateBy)
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

            Dim leasing As Leasing = CType(obj, Leasing)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, leasing.ID)
            DbCommandWrapper.AddInParameter("@LeasingGroupName", DbType.AnsiString, leasing.LeasingGroupName)
            DbCommandWrapper.AddInParameter("@LeasingCode", DbType.AnsiString, leasing.LeasingCode)
            DbCommandWrapper.AddInParameter("@LeasingName", DbType.AnsiString, leasing.LeasingName)
            DbCommandWrapper.AddInParameter("@City", DbType.AnsiString, leasing.City)
            DbCommandWrapper.AddInParameter("@Alamat", DbType.AnsiString, leasing.Alamat)
            DbCommandWrapper.AddInParameter("@Kelurahan", DbType.AnsiString, leasing.Kelurahan)
            DbCommandWrapper.AddInParameter("@Kecamatan", DbType.AnsiString, leasing.Kecamatan)
            DbCommandWrapper.AddInParameter("@Province", DbType.AnsiString, leasing.Province)
            DbCommandWrapper.AddInParameter("@PostalCode", DbType.AnsiString, leasing.PostalCode)
            DbCommandWrapper.AddInParameter("@PhoneNo", DbType.AnsiString, leasing.PhoneNo)
            DbCommandWrapper.AddInParameter("@Fax", DbType.AnsiString, leasing.Fax)
            DbCommandWrapper.AddInParameter("@WebSite", DbType.AnsiString, leasing.WebSite)
            DbCommandWrapper.AddInParameter("@Email", DbType.String, leasing.Email)
            DbCommandWrapper.AddInParameter("@ContactPerson", DbType.AnsiString, leasing.ContactPerson)
            DbCommandWrapper.AddInParameter("@HP", DbType.AnsiString, leasing.HP)
            DbCommandWrapper.AddInParameter("@Status", DbType.Byte, leasing.Status)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, leasing.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, leasing.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As Leasing

            Dim leasing As Leasing = New Leasing

            leasing.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("LeasingGroupName")) Then leasing.LeasingGroupName = dr("LeasingGroupName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LeasingCode")) Then leasing.LeasingCode = dr("LeasingCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LeasingName")) Then leasing.LeasingName = dr("LeasingName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("City")) Then leasing.City = dr("City").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Alamat")) Then leasing.Alamat = dr("Alamat").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Kelurahan")) Then leasing.Kelurahan = dr("Kelurahan").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Kecamatan")) Then leasing.Kecamatan = dr("Kecamatan").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Province")) Then leasing.Province = dr("Province").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PostalCode")) Then leasing.PostalCode = dr("PostalCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PhoneNo")) Then leasing.PhoneNo = dr("PhoneNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Fax")) Then leasing.Fax = dr("Fax").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("WebSite")) Then leasing.WebSite = dr("WebSite").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Email")) Then leasing.Email = dr("Email").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ContactPerson")) Then leasing.ContactPerson = dr("ContactPerson").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("HP")) Then leasing.HP = dr("HP").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then leasing.Status = CType(dr("Status"), Byte)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then leasing.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then leasing.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then leasing.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then leasing.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then leasing.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            Return leasing

        End Function

        Private Sub SetTableName()

            If Not (GetType(Leasing) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(Leasing), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(Leasing).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

