
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : BenefitClaimRecommendation Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2015 
'// ---------------------
'// $History      : $
'// Generated on 12/2/2015 - 11:09:12 AM
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

    Public Class BenefitClaimRecommendationMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertBenefitClaimRecommendation"
        Private m_UpdateStatement As String = "up_UpdateBenefitClaimRecommendation"
        Private m_RetrieveStatement As String = "up_RetrieveBenefitClaimRecommendation"
        Private m_RetrieveListStatement As String = "up_RetrieveBenefitClaimRecommendationList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteBenefitClaimRecommendation"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim benefitClaimRecommendation As BenefitClaimRecommendation = Nothing
            While dr.Read

                benefitClaimRecommendation = Me.CreateObject(dr)

            End While

            Return benefitClaimRecommendation

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim benefitClaimRecommendationList As ArrayList = New ArrayList

            While dr.Read
                Dim benefitClaimRecommendation As BenefitClaimRecommendation = Me.CreateObject(dr)
                benefitClaimRecommendationList.Add(benefitClaimRecommendation)
            End While

            Return benefitClaimRecommendationList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim benefitClaimRecommendation As BenefitClaimRecommendation = CType(obj, BenefitClaimRecommendation)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, benefitClaimRecommendation.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim benefitClaimRecommendation As BenefitClaimRecommendation = CType(obj, BenefitClaimRecommendation)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@BenefitClaimRecReg", DbType.AnsiString, benefitClaimRecommendation.BenefitClaimRecReg)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, benefitClaimRecommendation.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, benefitClaimRecommendation.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@BenefitClaimDetailsID", DbType.Int32, Me.GetRefObject(benefitClaimRecommendation.BenefitClaimDetails))

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

            Dim benefitClaimRecommendation As BenefitClaimRecommendation = CType(obj, BenefitClaimRecommendation)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, benefitClaimRecommendation.ID)
            DbCommandWrapper.AddInParameter("@BenefitClaimRecReg", DbType.AnsiString, benefitClaimRecommendation.BenefitClaimRecReg)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, benefitClaimRecommendation.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, benefitClaimRecommendation.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@BenefitClaimDetailsID", DbType.Int32, Me.GetRefObject(benefitClaimRecommendation.BenefitClaimDetails))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As BenefitClaimRecommendation

            Dim benefitClaimRecommendation As BenefitClaimRecommendation = New BenefitClaimRecommendation

            benefitClaimRecommendation.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("BenefitClaimRecReg")) Then benefitClaimRecommendation.BenefitClaimRecReg = dr("BenefitClaimRecReg").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then benefitClaimRecommendation.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then benefitClaimRecommendation.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then benefitClaimRecommendation.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then benefitClaimRecommendation.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then benefitClaimRecommendation.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("BenefitClaimDetailsID")) Then
                benefitClaimRecommendation.BenefitClaimDetails = New BenefitClaimDetails(CType(dr("BenefitClaimDetailsID"), Integer))
            End If

            Return benefitClaimRecommendation

        End Function

        Private Sub SetTableName()

            If Not (GetType(BenefitClaimRecommendation) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(BenefitClaimRecommendation), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(BenefitClaimRecommendation).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

