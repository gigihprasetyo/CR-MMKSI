
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : BenefitMasterDetail Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2015 
'// ---------------------
'// $History      : $
'// Generated on 12/11/2015 - 8:51:49 AM
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

    Public Class BenefitMasterDetailMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertBenefitMasterDetail"
        Private m_UpdateStatement As String = "up_UpdateBenefitMasterDetail"
        Private m_RetrieveStatement As String = "up_RetrieveBenefitMasterDetail"
        Private m_RetrieveListStatement As String = "up_RetrieveBenefitMasterDetailList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteBenefitMasterDetail"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim benefitMasterDetail As BenefitMasterDetail = Nothing
            While dr.Read

                benefitMasterDetail = Me.CreateObject(dr)

            End While

            Return benefitMasterDetail

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim benefitMasterDetailList As ArrayList = New ArrayList

            While dr.Read
                Dim benefitMasterDetail As BenefitMasterDetail = Me.CreateObject(dr)
                benefitMasterDetailList.Add(benefitMasterDetail)
            End While

            Return benefitMasterDetailList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim benefitMasterDetail As BenefitMasterDetail = CType(obj, BenefitMasterDetail)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, benefitMasterDetail.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim benefitMasterDetail As BenefitMasterDetail = CType(obj, BenefitMasterDetail)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@FormulaID", DbType.AnsiStringFixedLength, benefitMasterDetail.FormulaID)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, benefitMasterDetail.Description)
            DbCommandWrapper.AddInParameter("@Amount", DbType.Currency, benefitMasterDetail.Amount)
            DbCommandWrapper.AddInParameter("@FakturValidationStart", DbType.DateTime, benefitMasterDetail.FakturValidationStart)
            DbCommandWrapper.AddInParameter("@FakturValidationEnd", DbType.DateTime, benefitMasterDetail.FakturValidationEnd)

            DbCommandWrapper.AddInParameter("@FakturOpenStart", DbType.DateTime, benefitMasterDetail.FakturOpenStart)
            DbCommandWrapper.AddInParameter("@FakturOpenEnd", DbType.DateTime, benefitMasterDetail.FakturOpenEnd)

            DbCommandWrapper.AddInParameter("@AssyYear", DbType.Int16, benefitMasterDetail.AssyYear)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, benefitMasterDetail.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, benefitMasterDetail.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@MaxClaim", DbType.Int16, benefitMasterDetail.MaxClaim)
            DbCommandWrapper.AddInParameter("@WSDiscount", DbType.Int16, benefitMasterDetail.WSDiscount)

            DbCommandWrapper.AddInParameter("@BenefitMasterHeaderID", DbType.Int32, Me.GetRefObject(benefitMasterDetail.BenefitMasterHeader))
            DbCommandWrapper.AddInParameter("@BenefitTypeID", DbType.Int16, Me.GetRefObject(benefitMasterDetail.BenefitType))

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

            Dim benefitMasterDetail As BenefitMasterDetail = CType(obj, BenefitMasterDetail)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, benefitMasterDetail.ID)
            DbCommandWrapper.AddInParameter("@FormulaID", DbType.AnsiStringFixedLength, benefitMasterDetail.FormulaID)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, benefitMasterDetail.Description)
            DbCommandWrapper.AddInParameter("@Amount", DbType.Currency, benefitMasterDetail.Amount)
            DbCommandWrapper.AddInParameter("@FakturValidationStart", DbType.DateTime, benefitMasterDetail.FakturValidationStart)
            DbCommandWrapper.AddInParameter("@FakturValidationEnd", DbType.DateTime, benefitMasterDetail.FakturValidationEnd)

            DbCommandWrapper.AddInParameter("@FakturOpenStart", DbType.DateTime, benefitMasterDetail.FakturOpenStart)
            DbCommandWrapper.AddInParameter("@FakturOpenEnd", DbType.DateTime, benefitMasterDetail.FakturOpenEnd)

            DbCommandWrapper.AddInParameter("@AssyYear", DbType.Int16, benefitMasterDetail.AssyYear)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, benefitMasterDetail.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, benefitMasterDetail.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@MaxClaim", DbType.Int16, benefitMasterDetail.MaxClaim)
            DbCommandWrapper.AddInParameter("@WSDiscount", DbType.Int16, benefitMasterDetail.WSDiscount)

            DbCommandWrapper.AddInParameter("@BenefitMasterHeaderID", DbType.Int32, Me.GetRefObject(benefitMasterDetail.BenefitMasterHeader))
            DbCommandWrapper.AddInParameter("@BenefitTypeID", DbType.Int16, Me.GetRefObject(benefitMasterDetail.BenefitType))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As BenefitMasterDetail

            Dim benefitMasterDetail As BenefitMasterDetail = New BenefitMasterDetail

            benefitMasterDetail.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("FormulaID")) Then benefitMasterDetail.FormulaID = dr("FormulaID").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Description")) Then benefitMasterDetail.Description = dr("Description").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Amount")) Then benefitMasterDetail.Amount = CType(dr("Amount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("FakturValidationStart")) Then benefitMasterDetail.FakturValidationStart = CType(dr("FakturValidationStart"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("FakturValidationEnd")) Then benefitMasterDetail.FakturValidationEnd = CType(dr("FakturValidationEnd"), DateTime)

            If Not dr.IsDBNull(dr.GetOrdinal("FakturOpenStart")) Then benefitMasterDetail.FakturOpenStart = CType(dr("FakturOpenStart"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("FakturOpenEnd")) Then benefitMasterDetail.FakturOpenEnd = CType(dr("FakturOpenEnd"), DateTime)

            If Not dr.IsDBNull(dr.GetOrdinal("AssyYear")) Then benefitMasterDetail.AssyYear = CType(dr("AssyYear"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then benefitMasterDetail.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then benefitMasterDetail.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then benefitMasterDetail.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then benefitMasterDetail.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then benefitMasterDetail.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("MaxClaim")) Then benefitMasterDetail.MaxClaim = CType(dr("MaxClaim"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("WSDiscount")) Then benefitMasterDetail.WSDiscount = CType(dr("WSDiscount"), Short)


            If Not dr.IsDBNull(dr.GetOrdinal("BenefitMasterHeaderID")) Then
                benefitMasterDetail.BenefitMasterHeader = New BenefitMasterHeader(CType(dr("BenefitMasterHeaderID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("BenefitTypeID")) Then
                benefitMasterDetail.BenefitType = New BenefitType(CType(dr("BenefitTypeID"), Short))
            End If

            Return benefitMasterDetail

        End Function

        Private Sub SetTableName()

            If Not (GetType(BenefitMasterDetail) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(BenefitMasterDetail), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(BenefitMasterDetail).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

