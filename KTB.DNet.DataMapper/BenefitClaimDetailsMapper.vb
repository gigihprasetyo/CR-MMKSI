
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : BenefitClaimDetails Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2015 
'// ---------------------
'// $History      : $
'// Generated on 12/2/2015 - 11:07:21 AM
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

    Public Class BenefitClaimDetailsMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertBenefitClaimDetails"
        Private m_UpdateStatement As String = "up_UpdateBenefitClaimDetails"
        Private m_RetrieveStatement As String = "up_RetrieveBenefitClaimDetails"
        Private m_RetrieveListStatement As String = "up_RetrieveBenefitClaimDetailsList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteBenefitClaimDetails"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim benefitClaimDetails As BenefitClaimDetails = Nothing
            While dr.Read

                benefitClaimDetails = Me.CreateObject(dr)

            End While

            Return benefitClaimDetails

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim benefitClaimDetailsList As ArrayList = New ArrayList

            While dr.Read
                Dim benefitClaimDetails As BenefitClaimDetails = Me.CreateObject(dr)
                benefitClaimDetailsList.Add(benefitClaimDetails)
            End While

            Return benefitClaimDetailsList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim benefitClaimDetails As BenefitClaimDetails = CType(obj, BenefitClaimDetails)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, benefitClaimDetails.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim benefitClaimDetails As BenefitClaimDetails = CType(obj, BenefitClaimDetails)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@RecLetterRegNo", DbType.AnsiString, benefitClaimDetails.RecLetterRegNo)
            DbCommandWrapper.AddInParameter("@DetailStatus", DbType.Int16, benefitClaimDetails.DetailStatus)
            DbCommandWrapper.AddInParameter("@StatusUpload", DbType.Int16, benefitClaimDetails.StatusUpload)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, benefitClaimDetails.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, benefitClaimDetails.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@DescDealer", DbType.AnsiString, benefitClaimDetails.DescDealer)
            DbCommandWrapper.AddInParameter("@DescKtb", DbType.AnsiString, benefitClaimDetails.DescKtb)

            DbCommandWrapper.AddInParameter("@BenefitClaimHeaderID", DbType.Int32, Me.GetRefObject(benefitClaimDetails.BenefitClaimHeader))
            DbCommandWrapper.AddInParameter("@ChassisMasterID", DbType.Int32, Me.GetRefObject(benefitClaimDetails.ChassisMaster))

            DbCommandWrapper.AddInParameter("@BenefitMasterDetailID", DbType.Int32, Me.GetRefObject(benefitClaimDetails.BenefitMasterDetail))

            DbCommandWrapper.AddInParameter("@LeasingCompanyID", DbType.Int32, Me.GetRefObject(benefitClaimDetails.LeasingCompany))


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

            Dim benefitClaimDetails As BenefitClaimDetails = CType(obj, BenefitClaimDetails)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, benefitClaimDetails.ID)
            DbCommandWrapper.AddInParameter("@RecLetterRegNo", DbType.AnsiString, benefitClaimDetails.RecLetterRegNo)
            DbCommandWrapper.AddInParameter("@DetailStatus", DbType.Int16, benefitClaimDetails.DetailStatus)
            DbCommandWrapper.AddInParameter("@StatusUpload", DbType.Int16, benefitClaimDetails.StatusUpload)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, benefitClaimDetails.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, benefitClaimDetails.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@DescDealer", DbType.AnsiString, benefitClaimDetails.DescDealer)
            DbCommandWrapper.AddInParameter("@DescKtb", DbType.AnsiString, benefitClaimDetails.DescKtb)


            DbCommandWrapper.AddInParameter("@BenefitClaimHeaderID", DbType.Int32, Me.GetRefObject(benefitClaimDetails.BenefitClaimHeader))
            DbCommandWrapper.AddInParameter("@ChassisMasterID", DbType.Int32, Me.GetRefObject(benefitClaimDetails.ChassisMaster))

            DbCommandWrapper.AddInParameter("@BenefitMasterDetailID", DbType.Int32, Me.GetRefObject(benefitClaimDetails.BenefitMasterDetail))

            DbCommandWrapper.AddInParameter("@LeasingCompanyID", DbType.Int32, Me.GetRefObject(benefitClaimDetails.LeasingCompany))


            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As BenefitClaimDetails

            Dim benefitClaimDetails As BenefitClaimDetails = New BenefitClaimDetails

            benefitClaimDetails.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("RecLetterRegNo")) Then benefitClaimDetails.RecLetterRegNo = dr("RecLetterRegNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DetailStatus")) Then benefitClaimDetails.DetailStatus = CType(dr("DetailStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("StatusUpload")) Then benefitClaimDetails.StatusUpload = CType(dr("StatusUpload"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then benefitClaimDetails.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then benefitClaimDetails.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then benefitClaimDetails.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then benefitClaimDetails.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then benefitClaimDetails.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            If Not dr.IsDBNull(dr.GetOrdinal("DescDealer")) Then benefitClaimDetails.DescDealer = dr("DescDealer").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DescKtb")) Then benefitClaimDetails.DescKtb = dr("DescKtb").ToString

            If Not dr.IsDBNull(dr.GetOrdinal("BenefitClaimHeaderID")) Then
                benefitClaimDetails.BenefitClaimHeader = New BenefitClaimHeader(CType(dr("BenefitClaimHeaderID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("ChassisMasterID")) Then
                benefitClaimDetails.ChassisMaster = New ChassisMaster(CType(dr("ChassisMasterID"), Integer))
            End If

            If Not dr.IsDBNull(dr.GetOrdinal("BenefitMasterDetailID")) Then
                benefitClaimDetails.BenefitMasterDetail = New BenefitMasterDetail(CType(dr("BenefitMasterDetailID"), Integer))
            End If

            If Not dr.IsDBNull(dr.GetOrdinal("LeasingCompanyID")) Then
                benefitClaimDetails.LeasingCompany = New LeasingCompany(CType(dr("LeasingCompanyID"), Short))
            End If

            Return benefitClaimDetails

        End Function

        Private Sub SetTableName()

            If Not (GetType(BenefitClaimDetails) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(BenefitClaimDetails), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(BenefitClaimDetails).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

