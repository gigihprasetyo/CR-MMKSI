
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : Karoseri Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 2/28/2018 - 11:39:51 AM
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

    Public Class KaroseriMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertKaroseri"
        Private m_UpdateStatement As String = "up_UpdateKaroseri"
        Private m_RetrieveStatement As String = "up_RetrieveKaroseri"
        Private m_RetrieveListStatement As String = "up_RetrieveKaroseriList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteKaroseri"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim karoseri As Karoseri = Nothing
            While dr.Read

                karoseri = Me.CreateObject(dr)

            End While

            Return karoseri

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim karoseriList As ArrayList = New ArrayList

            While dr.Read
                Dim karoseri As Karoseri = Me.CreateObject(dr)
                karoseriList.Add(karoseri)
            End While

            Return karoseriList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim karoseri As Karoseri = CType(obj, Karoseri)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, karoseri.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim karoseri As Karoseri = CType(obj, Karoseri)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@Code", DbType.AnsiString, karoseri.Code)
            DbCommandWrapper.AddInParameter("@Name", DbType.AnsiString, karoseri.Name)
            DbCommandWrapper.AddInParameter("@City", DbType.AnsiString, karoseri.City)
            DbCommandWrapper.AddInParameter("@Alamat", DbType.AnsiString, karoseri.Alamat)
            DbCommandWrapper.AddInParameter("@Kelurahan", DbType.AnsiString, karoseri.Kelurahan)
            DbCommandWrapper.AddInParameter("@Kecamatan", DbType.AnsiString, karoseri.Kecamatan)
            DbCommandWrapper.AddInParameter("@Province", DbType.AnsiString, karoseri.Province)
            DbCommandWrapper.AddInParameter("@PostalCode", DbType.AnsiString, karoseri.PostalCode)
            DbCommandWrapper.AddInParameter("@PhoneNo", DbType.AnsiString, karoseri.PhoneNo)
            DbCommandWrapper.AddInParameter("@Fax", DbType.AnsiString, karoseri.Fax)
            DbCommandWrapper.AddInParameter("@WebSite", DbType.AnsiString, karoseri.WebSite)
            DbCommandWrapper.AddInParameter("@Email", DbType.String, karoseri.Email)
            DbCommandWrapper.AddInParameter("@ContactPerson", DbType.AnsiString, karoseri.ContactPerson)
            DbCommandWrapper.AddInParameter("@HP", DbType.AnsiString, karoseri.HP)
            DbCommandWrapper.AddInParameter("@Status", DbType.Byte, karoseri.Status)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, karoseri.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, karoseri.LastUpdateBy)
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

            Dim karoseri As Karoseri = CType(obj, Karoseri)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, karoseri.ID)
            DbCommandWrapper.AddInParameter("@Code", DbType.AnsiString, karoseri.Code)
            DbCommandWrapper.AddInParameter("@Name", DbType.AnsiString, karoseri.Name)
            DbCommandWrapper.AddInParameter("@City", DbType.AnsiString, karoseri.City)
            DbCommandWrapper.AddInParameter("@Alamat", DbType.AnsiString, karoseri.Alamat)
            DbCommandWrapper.AddInParameter("@Kelurahan", DbType.AnsiString, karoseri.Kelurahan)
            DbCommandWrapper.AddInParameter("@Kecamatan", DbType.AnsiString, karoseri.Kecamatan)
            DbCommandWrapper.AddInParameter("@Province", DbType.AnsiString, karoseri.Province)
            DbCommandWrapper.AddInParameter("@PostalCode", DbType.AnsiString, karoseri.PostalCode)
            DbCommandWrapper.AddInParameter("@PhoneNo", DbType.AnsiString, karoseri.PhoneNo)
            DbCommandWrapper.AddInParameter("@Fax", DbType.AnsiString, karoseri.Fax)
            DbCommandWrapper.AddInParameter("@WebSite", DbType.AnsiString, karoseri.WebSite)
            DbCommandWrapper.AddInParameter("@Email", DbType.String, karoseri.Email)
            DbCommandWrapper.AddInParameter("@ContactPerson", DbType.AnsiString, karoseri.ContactPerson)
            DbCommandWrapper.AddInParameter("@HP", DbType.AnsiString, karoseri.HP)
            DbCommandWrapper.AddInParameter("@Status", DbType.Byte, karoseri.Status)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, karoseri.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, karoseri.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As Karoseri

            Dim karoseri As Karoseri = New Karoseri

            karoseri.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Code")) Then karoseri.Code = dr("Code").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Name")) Then karoseri.Name = dr("Name").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("City")) Then karoseri.City = dr("City").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Alamat")) Then karoseri.Alamat = dr("Alamat").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Kelurahan")) Then karoseri.Kelurahan = dr("Kelurahan").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Kecamatan")) Then karoseri.Kecamatan = dr("Kecamatan").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Province")) Then karoseri.Province = dr("Province").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PostalCode")) Then karoseri.PostalCode = dr("PostalCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PhoneNo")) Then karoseri.PhoneNo = dr("PhoneNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Fax")) Then karoseri.Fax = dr("Fax").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("WebSite")) Then karoseri.WebSite = dr("WebSite").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Email")) Then karoseri.Email = dr("Email").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ContactPerson")) Then karoseri.ContactPerson = dr("ContactPerson").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("HP")) Then karoseri.HP = dr("HP").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then karoseri.Status = CType(dr("Status"), Byte)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then karoseri.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then karoseri.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then karoseri.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then karoseri.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then karoseri.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            Return karoseri

        End Function

        Private Sub SetTableName()

            If Not (GetType(Karoseri) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(Karoseri), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(Karoseri).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

