
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : SPKCustomer Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2011 
'// ---------------------
'// $History      : $
'// Generated on 3/7/2011 - 2:13:13 PM
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

    Public Class SPKCustomerMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertSPKCustomer"
        Private m_UpdateStatement As String = "up_UpdateSPKCustomer"
        Private m_RetrieveStatement As String = "up_RetrieveSPKCustomer"
        Private m_RetrieveListStatement As String = "up_RetrieveSPKCustomerList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteSPKCustomer"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim sPKCustomer As SPKCustomer = Nothing
            While dr.Read

                sPKCustomer = Me.CreateObject(dr)

            End While

            Return sPKCustomer

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim sPKCustomerList As ArrayList = New ArrayList

            While dr.Read
                Dim sPKCustomer As SPKCustomer = Me.CreateObject(dr)
                sPKCustomerList.Add(sPKCustomer)
            End While

            Return sPKCustomerList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim sPKCustomer As SPKCustomer = CType(obj, SPKCustomer)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, sPKCustomer.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim sPKCustomer As SPKCustomer = CType(obj, SPKCustomer)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@Code", DbType.AnsiString, sPKCustomer.Code)
            DbCommandWrapper.AddInParameter("@ReffCode", DbType.AnsiString, sPKCustomer.ReffCode)
            DbCommandWrapper.AddInParameter("@TipeCustomer", DbType.Int16, sPKCustomer.TipeCustomer)
            DbCommandWrapper.AddInParameter("@TipePerusahaan", DbType.Int16, sPKCustomer.TipePerusahaan)
            DbCommandWrapper.AddInParameter("@Name1", DbType.String, sPKCustomer.Name1)
            DbCommandWrapper.AddInParameter("@Name2", DbType.String, sPKCustomer.Name2)
            DbCommandWrapper.AddInParameter("@Name3", DbType.String, sPKCustomer.Name3)
            DbCommandWrapper.AddInParameter("@Alamat", DbType.String, sPKCustomer.Alamat)
            DbCommandWrapper.AddInParameter("@Kelurahan", DbType.String, sPKCustomer.Kelurahan)
            DbCommandWrapper.AddInParameter("@Kecamatan", DbType.String, sPKCustomer.Kecamatan)
            DbCommandWrapper.AddInParameter("@PostalCode", DbType.String, sPKCustomer.PostalCode)
            DbCommandWrapper.AddInParameter("@PreArea", DbType.AnsiString, sPKCustomer.PreArea)
            DbCommandWrapper.AddInParameter("@PrintRegion", DbType.AnsiString, sPKCustomer.PrintRegion)
            DbCommandWrapper.AddInParameter("@PhoneNo", DbType.String, sPKCustomer.PhoneNo)
            DbCommandWrapper.AddInParameter("@OfficeNo", DbType.String, sPKCustomer.OfficeNo)
            DbCommandWrapper.AddInParameter("@HomeNo", DbType.String, sPKCustomer.HomeNo)
            DbCommandWrapper.AddInParameter("@HpNo", DbType.String, sPKCustomer.HpNo)
            DbCommandWrapper.AddInParameter("@Email", DbType.String, sPKCustomer.Email)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int32, sPKCustomer.Status)
            DbCommandWrapper.AddInParameter("@MCPStatus", DbType.Int16, sPKCustomer.MCPStatus)
            DbCommandWrapper.AddInParameter("@LKPPStatus", DbType.Int16, sPKCustomer.LKPPStatus)
            DbCommandWrapper.AddInParameter("@LKPPReference", DbType.String, sPKCustomer.LKPPReference)
            DbCommandWrapper.AddInParameter("@BusinessSectorDetailID", DbType.Int32, Me.GetRefObject(sPKCustomer.BusinessSectorDetail))
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, sPKCustomer.RowStatus)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.String, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.String, sPKCustomer.LastUpdateBy)

            DbCommandWrapper.AddInParameter("@CityID", DbType.Int16, Me.GetRefObject(sPKCustomer.City))
            DbCommandWrapper.AddInParameter("@SAPCustomerID", DbType.Int32, Me.GetRefObject(sPKCustomer.SAPCustomer))

            DbCommandWrapper.AddInParameter("@ImagePath", DbType.AnsiString, sPKCustomer.ImagePath)
            'CR SPK
            DbCommandWrapper.AddInParameter("@TypeIdentitas", DbType.String, sPKCustomer.TypeIdentitas)
            DbCommandWrapper.AddInParameter("@TypePerorangan", DbType.Int32, sPKCustomer.TypePerorangan)
            DbCommandWrapper.AddInParameter("@CountryCode", DbType.String, sPKCustomer.CountryCode)
            '

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

            Dim sPKCustomer As SPKCustomer = CType(obj, SPKCustomer)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, sPKCustomer.ID)
            DbCommandWrapper.AddInParameter("@Code", DbType.AnsiString, sPKCustomer.Code)
            DbCommandWrapper.AddInParameter("@ReffCode", DbType.AnsiString, sPKCustomer.ReffCode)
            DbCommandWrapper.AddInParameter("@TipeCustomer", DbType.Int16, sPKCustomer.TipeCustomer)
            DbCommandWrapper.AddInParameter("@TipePerusahaan", DbType.Int16, sPKCustomer.TipePerusahaan)
            DbCommandWrapper.AddInParameter("@Name1", DbType.String, sPKCustomer.Name1)
            DbCommandWrapper.AddInParameter("@Name2", DbType.String, sPKCustomer.Name2)
            DbCommandWrapper.AddInParameter("@Name3", DbType.String, sPKCustomer.Name3)
            DbCommandWrapper.AddInParameter("@Alamat", DbType.String, sPKCustomer.Alamat)
            DbCommandWrapper.AddInParameter("@Kelurahan", DbType.String, sPKCustomer.Kelurahan)
            DbCommandWrapper.AddInParameter("@Kecamatan", DbType.String, sPKCustomer.Kecamatan)
            DbCommandWrapper.AddInParameter("@PostalCode", DbType.String, sPKCustomer.PostalCode)
            DbCommandWrapper.AddInParameter("@PreArea", DbType.AnsiString, sPKCustomer.PreArea)
            DbCommandWrapper.AddInParameter("@PrintRegion", DbType.AnsiString, sPKCustomer.PrintRegion)
            DbCommandWrapper.AddInParameter("@PhoneNo", DbType.String, sPKCustomer.PhoneNo)
            DbCommandWrapper.AddInParameter("@OfficeNo", DbType.String, sPKCustomer.OfficeNo)
            DbCommandWrapper.AddInParameter("@HomeNo", DbType.String, sPKCustomer.HomeNo)
            DbCommandWrapper.AddInParameter("@HpNo", DbType.String, sPKCustomer.HpNo)
            DbCommandWrapper.AddInParameter("@Email", DbType.String, sPKCustomer.Email)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int32, sPKCustomer.Status)
            DbCommandWrapper.AddInParameter("@MCPStatus", DbType.Int16, sPKCustomer.MCPStatus)
            DbCommandWrapper.AddInParameter("@LKPPStatus", DbType.Int16, sPKCustomer.LKPPStatus)
            DbCommandWrapper.AddInParameter("@LKPPReference", DbType.String, sPKCustomer.LKPPReference)
            DbCommandWrapper.AddInParameter("@BusinessSectorDetailID", DbType.Int32, Me.GetRefObject(sPKCustomer.BusinessSectorDetail))
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, sPKCustomer.RowStatus)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.String, sPKCustomer.CreatedBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.String, User)


            DbCommandWrapper.AddInParameter("@CityID", DbType.Int16, Me.GetRefObject(sPKCustomer.City))
            DbCommandWrapper.AddInParameter("@SAPCustomerID", DbType.Int32, Me.GetRefObject(sPKCustomer.SAPCustomer))
            DbCommandWrapper.AddInParameter("@ImagePath", DbType.AnsiString, sPKCustomer.ImagePath)
            'CR SPK
            DbCommandWrapper.AddInParameter("@TypeIdentitas", DbType.String, sPKCustomer.TypeIdentitas)
            DbCommandWrapper.AddInParameter("@TypePerorangan", DbType.Int32, sPKCustomer.TypePerorangan)
            DbCommandWrapper.AddInParameter("@CountryCode", DbType.String, sPKCustomer.CountryCode)
            '
            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As SPKCustomer

            Dim sPKCustomer As SPKCustomer = New SPKCustomer

            sPKCustomer.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Code")) Then sPKCustomer.Code = dr("Code").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ReffCode")) Then sPKCustomer.ReffCode = dr("ReffCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("TipeCustomer")) Then sPKCustomer.TipeCustomer = CType(dr("TipeCustomer"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("TipePerusahaan")) Then sPKCustomer.TipePerusahaan = CType(dr("TipePerusahaan"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("Name1")) Then sPKCustomer.Name1 = dr("Name1").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Name2")) Then sPKCustomer.Name2 = dr("Name2").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Name3")) Then sPKCustomer.Name3 = dr("Name3").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Alamat")) Then sPKCustomer.Alamat = dr("Alamat").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Kelurahan")) Then sPKCustomer.Kelurahan = dr("Kelurahan").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Kecamatan")) Then sPKCustomer.Kecamatan = dr("Kecamatan").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PostalCode")) Then sPKCustomer.PostalCode = dr("PostalCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PreArea")) Then sPKCustomer.PreArea = dr("PreArea").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PrintRegion")) Then sPKCustomer.PrintRegion = dr("PrintRegion").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PhoneNo")) Then sPKCustomer.PhoneNo = dr("PhoneNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("OfficeNo")) Then sPKCustomer.OfficeNo = dr("OfficeNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("HomeNo")) Then sPKCustomer.HomeNo = dr("HomeNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("HpNo")) Then sPKCustomer.HpNo = dr("HpNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Email")) Then sPKCustomer.Email = dr("Email").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then sPKCustomer.Status = CType(dr("Status"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("MCPStatus")) Then sPKCustomer.MCPStatus = CType(dr("MCPStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("LKPPStatus")) Then sPKCustomer.LKPPStatus = CType(dr("LKPPStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("LKPPReference")) Then sPKCustomer.LKPPReference = CType(dr("LKPPReference"), String)
            If Not dr.IsDBNull(dr.GetOrdinal("ImagePath")) Then sPKCustomer.ImagePath = CType(dr("ImagePath"), String)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then sPKCustomer.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then sPKCustomer.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then sPKCustomer.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then sPKCustomer.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then sPKCustomer.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CityID")) Then
                sPKCustomer.City = New City(CType(dr("CityID"), Short))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("SAPCustomerID")) Then
                sPKCustomer.SAPCustomer = New SAPCustomer(CType(dr("SAPCustomerID"), Integer))
            End If

            If Not dr.IsDBNull(dr.GetOrdinal("ImagePath")) Then sPKCustomer.ImagePath = dr("ImagePath").ToString

	    If Not dr.IsDBNull(dr.GetOrdinal("BusinessSectorDetailID")) Then
                sPKCustomer.BusinessSectorDetail = New BusinessSectorDetail(CType(dr("BusinessSectorDetailID"), Integer))
            End If
            'CR SPK
            If Not dr.IsDBNull(dr.GetOrdinal("TypeIdentitas")) Then sPKCustomer.TypeIdentitas = dr("TypeIdentitas").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("TypePerorangan")) Then sPKCustomer.TypePerorangan = CType(dr("TypePerorangan"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("CountryCode")) Then sPKCustomer.CountryCode = dr("CountryCode").ToString
            '
            Return sPKCustomer

        End Function

        Private Sub SetTableName()

            If Not (GetType(SPKCustomer) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(SPKCustomer), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(SPKCustomer).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

