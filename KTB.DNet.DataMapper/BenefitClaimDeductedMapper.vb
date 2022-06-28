
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : BenefitClaimDeducted Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2019 
'// ---------------------
'// $History      : $
'// Generated on 13/12/2019 - 10:05:41
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

    Public Class BenefitClaimDeductedMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertBenefitClaimDeducted"
        Private m_UpdateStatement As String = "up_UpdateBenefitClaimDeducted"
        Private m_RetrieveStatement As String = "up_RetrieveBenefitClaimDeducted"
        Private m_RetrieveListStatement As String = "up_RetrieveBenefitClaimDeductedList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteBenefitClaimDeducted"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim benefitClaimDeducted As BenefitClaimDeducted = Nothing
            While dr.Read

                benefitClaimDeducted = Me.CreateObject(dr)

            End While

            Return benefitClaimDeducted

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim benefitClaimDeductedList As ArrayList = New ArrayList

            While dr.Read
                Dim benefitClaimDeducted As BenefitClaimDeducted = Me.CreateObject(dr)
                benefitClaimDeductedList.Add(benefitClaimDeducted)
            End While

            Return benefitClaimDeductedList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim benefitClaimDeducted As BenefitClaimDeducted = CType(obj, BenefitClaimDeducted)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, benefitClaimDeducted.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim benefitClaimDeducted As BenefitClaimDeducted = CType(obj, BenefitClaimDeducted)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@DeductedAmount", DbType.Currency, benefitClaimDeducted.DeductedAmount)
            DbCommandWrapper.AddInParameter("@RemainAmount", DbType.Currency, benefitClaimDeducted.RemainAmount)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, benefitClaimDeducted.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, benefitClaimDeducted.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@BenefitClaimHeaderID", DbType.Int32, Me.GetRefObject(benefitClaimDeducted.BenefitClaimHeader))
            DbCommandWrapper.AddInParameter("@DSFLeasingClaimID", DbType.Int32, Me.GetRefObject(benefitClaimDeducted.DSFLeasingClaim))

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

            Dim benefitClaimDeducted As BenefitClaimDeducted = CType(obj, BenefitClaimDeducted)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, benefitClaimDeducted.ID)
            DbCommandWrapper.AddInParameter("@DeductedAmount", DbType.Currency, benefitClaimDeducted.DeductedAmount)
            DbCommandWrapper.AddInParameter("@RemainAmount", DbType.Currency, benefitClaimDeducted.RemainAmount)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, benefitClaimDeducted.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, benefitClaimDeducted.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@BenefitClaimHeaderID", DbType.Int32, Me.GetRefObject(benefitClaimDeducted.BenefitClaimHeader))
            DbCommandWrapper.AddInParameter("@DSFLeasingClaimID", DbType.Int32, Me.GetRefObject(benefitClaimDeducted.DSFLeasingClaim))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As BenefitClaimDeducted

            Dim benefitClaimDeducted As BenefitClaimDeducted = New BenefitClaimDeducted

            benefitClaimDeducted.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("DeductedAmount")) Then benefitClaimDeducted.DeductedAmount = CType(dr("DeductedAmount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("RemainAmount")) Then benefitClaimDeducted.RemainAmount = CType(dr("RemainAmount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then benefitClaimDeducted.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then benefitClaimDeducted.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then benefitClaimDeducted.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then benefitClaimDeducted.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then benefitClaimDeducted.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("BenefitClaimHeaderID")) Then
                benefitClaimDeducted.BenefitClaimHeader = New BenefitClaimHeader(CType(dr("BenefitClaimHeaderID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("DSFLeasingClaimID")) Then
                benefitClaimDeducted.DSFLeasingClaim = New DSFLeasingClaim(CType(dr("DSFLeasingClaimID"), Integer))
            End If

            Return benefitClaimDeducted

        End Function

        Private Sub SetTableName()

            If Not (GetType(BenefitClaimDeducted) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(BenefitClaimDeducted), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(BenefitClaimDeducted).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

