
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : OCRIdentity Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 02/15/2018 - 7:56:50 AM
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

    Public Class OCRIdentityMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertOCRIdentity"
        Private m_UpdateStatement As String = "up_UpdateOCRIdentity"
        Private m_RetrieveStatement As String = "up_RetrieveOCRIdentity"
        Private m_RetrieveListStatement As String = "up_RetrieveOCRIdentityList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteOCRIdentity"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim oCRIdentity As OCRIdentity = Nothing
            While dr.Read

                oCRIdentity = Me.CreateObject(dr)

            End While

            Return oCRIdentity

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim oCRIdentityList As ArrayList = New ArrayList

            While dr.Read
                Dim oCRIdentity As OCRIdentity = Me.CreateObject(dr)
                oCRIdentityList.Add(oCRIdentity)
            End While

            Return oCRIdentityList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim oCRIdentity As OCRIdentity = CType(obj, OCRIdentity)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, oCRIdentity.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim oCRIdentity As OCRIdentity = CType(obj, OCRIdentity)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            'DbCommandWrapper.AddInParameter("@SPKCustomerID", DbType.Int32, oCRIdentity.SPKCustomerID)
            DbCommandWrapper.AddInParameter("@SPKCustomerID", DbType.Int32, Me.GetRefObject(oCRIdentity.SPKCustomer))
            DbCommandWrapper.AddInParameter("@SPKDetailCustomerID", DbType.Int32, Me.GetRefObject(oCRIdentity.SPKDetailCustomer))
            DbCommandWrapper.AddInParameter("@Type", DbType.Int16, oCRIdentity.Type)
            DbCommandWrapper.AddInParameter("@ImageID", DbType.AnsiString, oCRIdentity.ImageID)
            DbCommandWrapper.AddInParameter("@ImagePath", DbType.AnsiString, oCRIdentity.ImagePath)
            DbCommandWrapper.AddInParameter("@IdentityNumber", DbType.AnsiString, oCRIdentity.IdentityNumber)
            DbCommandWrapper.AddInParameter("@Name", DbType.AnsiString, oCRIdentity.Name)
            DbCommandWrapper.AddInParameter("@BirthOfDate", DbType.AnsiString, oCRIdentity.BirthOfDate)
            DbCommandWrapper.AddInParameter("@BirthOfPlace", DbType.AnsiString, oCRIdentity.BirthOfPlace)
            DbCommandWrapper.AddInParameter("@Gender", DbType.AnsiString, oCRIdentity.Gender)
            DbCommandWrapper.AddInParameter("@Height", DbType.AnsiString, oCRIdentity.Height)
            DbCommandWrapper.AddInParameter("@Address", DbType.AnsiString, oCRIdentity.Address)
            DbCommandWrapper.AddInParameter("@RtRw", DbType.AnsiString, oCRIdentity.RtRw)
            DbCommandWrapper.AddInParameter("@District", DbType.AnsiString, oCRIdentity.District)
            DbCommandWrapper.AddInParameter("@Subdistrict", DbType.AnsiString, oCRIdentity.Subdistrict)
            DbCommandWrapper.AddInParameter("@Regency", DbType.AnsiString, oCRIdentity.Regency)
            DbCommandWrapper.AddInParameter("@Province", DbType.AnsiString, oCRIdentity.Province)
            DbCommandWrapper.AddInParameter("@Religion", DbType.AnsiString, oCRIdentity.Religion)
            DbCommandWrapper.AddInParameter("@MaritalStatus", DbType.AnsiString, oCRIdentity.MaritalStatus)
            DbCommandWrapper.AddInParameter("@Occupation", DbType.AnsiString, oCRIdentity.Occupation)
            DbCommandWrapper.AddInParameter("@Citizenship", DbType.AnsiString, oCRIdentity.Citizenship)
            DbCommandWrapper.AddInParameter("@ValidUntil", DbType.AnsiString, oCRIdentity.ValidUntil)
            DbCommandWrapper.AddInParameter("@Polda", DbType.AnsiString, oCRIdentity.Polda)
            DbCommandWrapper.AddInParameter("@TotalChars", DbType.Int32, oCRIdentity.TotalChars)
            DbCommandWrapper.AddInParameter("@ConfidenceChars", DbType.Int32, oCRIdentity.ConfidenceChars)
            DbCommandWrapper.AddInParameter("@ProcessingTime", DbType.Double, oCRIdentity.ProcessingTime)
            DbCommandWrapper.AddInParameter("@Errors", DbType.AnsiString, oCRIdentity.Errors)
            DbCommandWrapper.AddInParameter("@JSon", DbType.AnsiString, oCRIdentity.JSon)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, oCRIdentity.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, oCRIdentity.LastUpdateBy)
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

            Dim oCRIdentity As OCRIdentity = CType(obj, OCRIdentity)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, oCRIdentity.ID)
            'DbCommandWrapper.AddInParameter("@SPKCustomerID", DbType.Int32, oCRIdentity.SPKCustomerID)
            DbCommandWrapper.AddInParameter("@SPKCustomerID", DbType.Int32, Me.GetRefObject(oCRIdentity.SPKCustomer))
            DbCommandWrapper.AddInParameter("@SPKDetailCustomerID", DbType.Int32, Me.GetRefObject(oCRIdentity.SPKDetailCustomer))
            DbCommandWrapper.AddInParameter("@Type", DbType.Int16, oCRIdentity.Type)
            DbCommandWrapper.AddInParameter("@ImageID", DbType.AnsiString, oCRIdentity.ImageID)
            DbCommandWrapper.AddInParameter("@ImagePath", DbType.AnsiString, oCRIdentity.ImagePath)
            DbCommandWrapper.AddInParameter("@IdentityNumber", DbType.AnsiString, oCRIdentity.IdentityNumber)
            DbCommandWrapper.AddInParameter("@Name", DbType.AnsiString, oCRIdentity.Name)
            DbCommandWrapper.AddInParameter("@BirthOfDate", DbType.AnsiString, oCRIdentity.BirthOfDate)
            DbCommandWrapper.AddInParameter("@BirthOfPlace", DbType.AnsiString, oCRIdentity.BirthOfPlace)
            DbCommandWrapper.AddInParameter("@Gender", DbType.AnsiString, oCRIdentity.Gender)
            DbCommandWrapper.AddInParameter("@Height", DbType.AnsiString, oCRIdentity.Height)
            DbCommandWrapper.AddInParameter("@Address", DbType.AnsiString, oCRIdentity.Address)
            DbCommandWrapper.AddInParameter("@RtRw", DbType.AnsiString, oCRIdentity.RtRw)
            DbCommandWrapper.AddInParameter("@District", DbType.AnsiString, oCRIdentity.District)
            DbCommandWrapper.AddInParameter("@Subdistrict", DbType.AnsiString, oCRIdentity.Subdistrict)
            DbCommandWrapper.AddInParameter("@Regency", DbType.AnsiString, oCRIdentity.Regency)
            DbCommandWrapper.AddInParameter("@Province", DbType.AnsiString, oCRIdentity.Province)
            DbCommandWrapper.AddInParameter("@Religion", DbType.AnsiString, oCRIdentity.Religion)
            DbCommandWrapper.AddInParameter("@MaritalStatus", DbType.AnsiString, oCRIdentity.MaritalStatus)
            DbCommandWrapper.AddInParameter("@Occupation", DbType.AnsiString, oCRIdentity.Occupation)
            DbCommandWrapper.AddInParameter("@Citizenship", DbType.AnsiString, oCRIdentity.Citizenship)
            DbCommandWrapper.AddInParameter("@ValidUntil", DbType.AnsiString, oCRIdentity.ValidUntil)
            DbCommandWrapper.AddInParameter("@Polda", DbType.AnsiString, oCRIdentity.Polda)
            DbCommandWrapper.AddInParameter("@TotalChars", DbType.Int32, oCRIdentity.TotalChars)
            DbCommandWrapper.AddInParameter("@ConfidenceChars", DbType.Int32, oCRIdentity.ConfidenceChars)
            DbCommandWrapper.AddInParameter("@ProcessingTime", DbType.Double, oCRIdentity.ProcessingTime)
            DbCommandWrapper.AddInParameter("@Errors", DbType.AnsiString, oCRIdentity.Errors)
            DbCommandWrapper.AddInParameter("@JSon", DbType.AnsiString, oCRIdentity.JSon)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, oCRIdentity.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, oCRIdentity.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As OCRIdentity

            Dim oCRIdentity As OCRIdentity = New OCRIdentity

            oCRIdentity.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("SPKCustomerID")) Then oCRIdentity.SPKCustomerID = CType(dr("SPKCustomerID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("SPKDetailCustomerID")) Then oCRIdentity.SPKDetailCustomerID = CType(dr("SPKDetailCustomerID"), Integer)

            If Not dr.IsDBNull(dr.GetOrdinal("SPKCustomerID")) Then
                oCRIdentity.SPKCustomer = New SPKCustomer(CType(dr("SPKCustomerID"), Integer))
            End If


            If Not dr.IsDBNull(dr.GetOrdinal("SPKDetailCustomerID")) Then
                oCRIdentity.SPKDetailCustomer = New SPKDetailCustomer(CType(dr("SPKDetailCustomerID"), Integer))
            End If


            If Not dr.IsDBNull(dr.GetOrdinal("Type")) Then oCRIdentity.Type = CType(dr("Type"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("ImageID")) Then oCRIdentity.ImageID = dr("ImageID").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ImagePath")) Then oCRIdentity.ImagePath = dr("ImagePath").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("IdentityNumber")) Then oCRIdentity.IdentityNumber = dr("IdentityNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Name")) Then oCRIdentity.Name = dr("Name").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("BirthOfDate")) Then oCRIdentity.BirthOfDate = dr("BirthOfDate").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("BirthOfPlace")) Then oCRIdentity.BirthOfPlace = dr("BirthOfPlace").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Gender")) Then oCRIdentity.Gender = dr("Gender").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Height")) Then oCRIdentity.Height = dr("Height").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Address")) Then oCRIdentity.Address = dr("Address").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RtRw")) Then oCRIdentity.RtRw = dr("RtRw").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("District")) Then oCRIdentity.District = dr("District").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Subdistrict")) Then oCRIdentity.Subdistrict = dr("Subdistrict").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Regency")) Then oCRIdentity.Regency = dr("Regency").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Province")) Then oCRIdentity.Province = dr("Province").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Religion")) Then oCRIdentity.Religion = dr("Religion").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("MaritalStatus")) Then oCRIdentity.MaritalStatus = dr("MaritalStatus").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Occupation")) Then oCRIdentity.Occupation = dr("Occupation").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Citizenship")) Then oCRIdentity.Citizenship = dr("Citizenship").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ValidUntil")) Then oCRIdentity.ValidUntil = dr("ValidUntil").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Polda")) Then oCRIdentity.Polda = dr("Polda").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("TotalChars")) Then oCRIdentity.TotalChars = CType(dr("TotalChars"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("ConfidenceChars")) Then oCRIdentity.ConfidenceChars = CType(dr("ConfidenceChars"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("ProcessingTime")) Then oCRIdentity.ProcessingTime = CType(dr("ProcessingTime"), Double)
            If Not dr.IsDBNull(dr.GetOrdinal("Errors")) Then oCRIdentity.Errors = dr("Errors").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("JSon")) Then oCRIdentity.JSon = dr("JSon").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then oCRIdentity.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then oCRIdentity.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then oCRIdentity.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then oCRIdentity.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then oCRIdentity.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            Return oCRIdentity

        End Function

        Private Sub SetTableName()

            If Not (GetType(OCRIdentity) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(OCRIdentity), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(OCRIdentity).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

