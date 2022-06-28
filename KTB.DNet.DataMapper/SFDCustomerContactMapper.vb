#Region "Summary"
'// ===========================================================================
'// AUTHOR        : Dnet
'// PURPOSE       : SFDCustomerContact Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2021 
'// ---------------------
'// $History      : $
'// Generated on 8/19/2021 - 4:35:22 PM
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

    Public Class SFDCustomerContactMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertSFDCustomerContact"
        Private m_UpdateStatement As String = "up_UpdateSFDCustomerContact"
        Private m_RetrieveStatement As String = "up_RetrieveSFDCustomerContact"
        Private m_RetrieveListStatement As String = "up_RetrieveSFDCustomerContactList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteSFDCustomerContact"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim sFDCustomerContact As SFDCustomerContact = Nothing
            While dr.Read

                sFDCustomerContact = Me.CreateObject(dr)

            End While

            Return sFDCustomerContact

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim sFDCustomerContactList As ArrayList = New ArrayList

            While dr.Read
                Dim sFDCustomerContact As SFDCustomerContact = Me.CreateObject(dr)
                sFDCustomerContactList.Add(sFDCustomerContact)
            End While

            Return sFDCustomerContactList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim sFDCustomerContact As SFDCustomerContact = CType(obj, SFDCustomerContact)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int64, sFDCustomerContact.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim sFDCustomerContact As SFDCustomerContact = CType(obj, SFDCustomerContact)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int64, 8)
            DbCommandWrapper.AddInParameter("@SalesmanCode", DbType.AnsiString, sFDCustomerContact.SalesmanCode)
            DbCommandWrapper.AddInParameter("@FirstName", DbType.AnsiString, sFDCustomerContact.FirstName)
            DbCommandWrapper.AddInParameter("@LastName", DbType.AnsiString, sFDCustomerContact.LastName)
            DbCommandWrapper.AddInParameter("@HPNo", DbType.AnsiString, sFDCustomerContact.HPNo)
            DbCommandWrapper.AddInParameter("@PhoneType", DbType.Int16, sFDCustomerContact.PhoneType)
            DbCommandWrapper.AddInParameter("@Phone", DbType.AnsiString, sFDCustomerContact.Phone)
            DbCommandWrapper.AddInParameter("@Email", DbType.AnsiString, sFDCustomerContact.Email)
            DbCommandWrapper.AddInParameter("@Gender", DbType.Byte, sFDCustomerContact.Gender)
            DbCommandWrapper.AddInParameter("@Address", DbType.AnsiString, sFDCustomerContact.Address)
            DbCommandWrapper.AddInParameter("@CustomerType", DbType.Int16, sFDCustomerContact.CustomerType)
            DbCommandWrapper.AddInParameter("@LeadSource", DbType.Int16, sFDCustomerContact.LeadSource)
            DbCommandWrapper.AddInParameter("@Notes", DbType.AnsiString, sFDCustomerContact.Notes)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, sFDCustomerContact.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, sFDCustomerContact.LastUpdateBy)
            DbCommandWrapper.AddInParameter("@LastUpdateTime", DbType.DateTime, sFDCustomerContact.LastUpdateTime)

            DbCommandWrapper.AddInParameter("@SPKMasterCountryCodePhoneID", DbType.Int32, sFDCustomerContact.SPKMasterCountryCodePhoneID)
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, Me.GetRefObject(sFDCustomerContact.Dealer))
            DbCommandWrapper.AddInParameter("@CityID", DbType.Int16, Me.GetRefObject(sFDCustomerContact.City))

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

            Dim sFDCustomerContact As SFDCustomerContact = CType(obj, SFDCustomerContact)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int64, sFDCustomerContact.ID)
            DbCommandWrapper.AddInParameter("@SalesmanCode", DbType.AnsiString, sFDCustomerContact.SalesmanCode)
            DbCommandWrapper.AddInParameter("@FirstName", DbType.AnsiString, sFDCustomerContact.FirstName)
            DbCommandWrapper.AddInParameter("@LastName", DbType.AnsiString, sFDCustomerContact.LastName)
            DbCommandWrapper.AddInParameter("@HPNo", DbType.AnsiString, sFDCustomerContact.HPNo)
            DbCommandWrapper.AddInParameter("@PhoneType", DbType.Int16, sFDCustomerContact.PhoneType)
            DbCommandWrapper.AddInParameter("@Phone", DbType.AnsiString, sFDCustomerContact.Phone)
            DbCommandWrapper.AddInParameter("@Email", DbType.AnsiString, sFDCustomerContact.Email)
            DbCommandWrapper.AddInParameter("@Gender", DbType.Byte, sFDCustomerContact.Gender)
            DbCommandWrapper.AddInParameter("@Address", DbType.AnsiString, sFDCustomerContact.Address)
            DbCommandWrapper.AddInParameter("@CustomerType", DbType.Int16, sFDCustomerContact.CustomerType)
            DbCommandWrapper.AddInParameter("@LeadSource", DbType.Int16, sFDCustomerContact.LeadSource)
            DbCommandWrapper.AddInParameter("@Notes", DbType.AnsiString, sFDCustomerContact.Notes)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, sFDCustomerContact.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, sFDCustomerContact.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, sFDCustomerContact.LastUpdateBy)
            DbCommandWrapper.AddInParameter("@LastUpdateTime", DbType.DateTime, sFDCustomerContact.LastUpdateTime)


            DbCommandWrapper.AddInParameter("@SPKMasterCountryCodePhoneID", DbType.Int32, sFDCustomerContact.SPKMasterCountryCodePhoneID)
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, Me.GetRefObject(sFDCustomerContact.Dealer))
            DbCommandWrapper.AddInParameter("@CityID", DbType.Int16, Me.GetRefObject(sFDCustomerContact.City))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As SFDCustomerContact

            Dim sFDCustomerContact As SFDCustomerContact = New SFDCustomerContact

            sFDCustomerContact.ID = CType(dr("ID"), Long)
            sFDCustomerContact.SalesmanCode = dr("SalesmanCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("FirstName")) Then sFDCustomerContact.FirstName = dr("FirstName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastName")) Then sFDCustomerContact.LastName = dr("LastName").ToString
            sFDCustomerContact.HPNo = dr("HPNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PhoneType")) Then sFDCustomerContact.PhoneType = CType(dr("PhoneType"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("Phone")) Then sFDCustomerContact.Phone = dr("Phone").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Email")) Then sFDCustomerContact.Email = dr("Email").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Gender")) Then sFDCustomerContact.Gender = CType(dr("Gender"), Byte)
            If Not dr.IsDBNull(dr.GetOrdinal("Address")) Then sFDCustomerContact.Address = dr("Address").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CustomerType")) Then sFDCustomerContact.CustomerType = CType(dr("CustomerType"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("LeadSource")) Then sFDCustomerContact.LeadSource = CType(dr("LeadSource"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("Notes")) Then sFDCustomerContact.Notes = dr("Notes").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then sFDCustomerContact.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then sFDCustomerContact.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then sFDCustomerContact.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then sFDCustomerContact.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then sFDCustomerContact.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            If Not dr.IsDBNull(dr.GetOrdinal("SPKMasterCountryCodePhoneID")) Then
                sFDCustomerContact.SPKMasterCountryCodePhoneID = CType(dr("SPKMasterCountryCodePhoneID"), Integer)
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then
                sFDCustomerContact.Dealer = New Dealer(CType(dr("DealerID"), Short))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("CityID")) Then
                sFDCustomerContact.City = New City(CType(dr("CityID"), Short))
            End If

            Return sFDCustomerContact

        End Function

        Private Sub SetTableName()

            If Not (GetType(SFDCustomerContact) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(SFDCustomerContact), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(SFDCustomerContact).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace
