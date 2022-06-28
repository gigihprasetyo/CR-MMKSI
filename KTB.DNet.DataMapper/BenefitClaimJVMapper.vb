
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : BenefitClaimJV Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2015 
'// ---------------------
'// $History      : $
'// Generated on 12/2/2015 - 11:08:47 AM
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

    Public Class BenefitClaimJVMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertBenefitClaimJV"
        Private m_UpdateStatement As String = "up_UpdateBenefitClaimJV"
        Private m_RetrieveStatement As String = "up_RetrieveBenefitClaimJV"
        Private m_RetrieveListStatement As String = "up_RetrieveBenefitClaimJVList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteBenefitClaimJV"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim benefitClaimJV As BenefitClaimJV = Nothing
            While dr.Read

                benefitClaimJV = Me.CreateObject(dr)

            End While

            Return benefitClaimJV

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim benefitClaimJVList As ArrayList = New ArrayList

            While dr.Read
                Dim benefitClaimJV As BenefitClaimJV = Me.CreateObject(dr)
                benefitClaimJVList.Add(benefitClaimJV)
            End While

            Return benefitClaimJVList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim benefitClaimJV As BenefitClaimJV = CType(obj, BenefitClaimJV)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, benefitClaimJV.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim benefitClaimJV As BenefitClaimJV = CType(obj, BenefitClaimJV)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@TipeAccount", DbType.AnsiString, benefitClaimJV.TipeAccount)
            DbCommandWrapper.AddInParameter("@VendorID", DbType.AnsiString, benefitClaimJV.VendorID)
            DbCommandWrapper.AddInParameter("@Amount", DbType.Currency, benefitClaimJV.Amount)
            DbCommandWrapper.AddInParameter("@AccuredMonth", DbType.Currency, benefitClaimJV.AccuredMount)
            DbCommandWrapper.AddInParameter("@BusinessArea", DbType.AnsiString, benefitClaimJV.BusinessArea)
            DbCommandWrapper.AddInParameter("@CostCenter", DbType.AnsiString, benefitClaimJV.CostCenter)
            DbCommandWrapper.AddInParameter("@PaymentDate", DbType.DateTime, benefitClaimJV.PaymentDate)
            DbCommandWrapper.AddInParameter("@ActualPaymentDate", DbType.DateTime, benefitClaimJV.ActualPaymentDate)
            DbCommandWrapper.AddInParameter("@JVNumber", DbType.AnsiString, benefitClaimJV.JVNumber)
            DbCommandWrapper.AddInParameter("@InternalOrder", DbType.AnsiString, benefitClaimJV.InternalOrder)
            DbCommandWrapper.AddInParameter("@Remarks", DbType.AnsiString, benefitClaimJV.Remarks)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, benefitClaimJV.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, benefitClaimJV.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@Month", DbType.Int16, benefitClaimJV.Month)

            DbCommandWrapper.AddInParameter("@BenefitClaimHeaderID", DbType.Int32, Me.GetRefObject(benefitClaimJV.BenefitClaimHeader))
            DbCommandWrapper.AddInParameter("@MasterAccruedID", DbType.Int32, Me.GetRefObject(benefitClaimJV.MasterAccrued))

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

            Dim benefitClaimJV As BenefitClaimJV = CType(obj, BenefitClaimJV)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, benefitClaimJV.ID)
            DbCommandWrapper.AddInParameter("@TipeAccount", DbType.AnsiString, benefitClaimJV.TipeAccount)
            DbCommandWrapper.AddInParameter("@VendorID", DbType.AnsiString, benefitClaimJV.VendorID)
            DbCommandWrapper.AddInParameter("@Amount", DbType.Currency, benefitClaimJV.Amount)
            DbCommandWrapper.AddInParameter("@AccuredMonth", DbType.Currency, benefitClaimJV.AccuredMount)
            DbCommandWrapper.AddInParameter("@BusinessArea", DbType.AnsiString, benefitClaimJV.BusinessArea)
            DbCommandWrapper.AddInParameter("@CostCenter", DbType.AnsiString, benefitClaimJV.CostCenter)
            DbCommandWrapper.AddInParameter("@PaymentDate", DbType.DateTime, benefitClaimJV.PaymentDate)
            DbCommandWrapper.AddInParameter("@ActualPaymentDate", DbType.DateTime, benefitClaimJV.ActualPaymentDate)
            DbCommandWrapper.AddInParameter("@JVNumber", DbType.AnsiString, benefitClaimJV.JVNumber)
            DbCommandWrapper.AddInParameter("@InternalOrder", DbType.AnsiString, benefitClaimJV.InternalOrder)
            DbCommandWrapper.AddInParameter("@Remarks", DbType.AnsiString, benefitClaimJV.Remarks)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, benefitClaimJV.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, benefitClaimJV.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@Month", DbType.Int16, benefitClaimJV.Month)

            DbCommandWrapper.AddInParameter("@BenefitClaimHeaderID", DbType.Int32, Me.GetRefObject(benefitClaimJV.BenefitClaimHeader))
            DbCommandWrapper.AddInParameter("@MasterAccruedID", DbType.Int32, Me.GetRefObject(benefitClaimJV.MasterAccrued))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As BenefitClaimJV

            Dim benefitClaimJV As BenefitClaimJV = New BenefitClaimJV

            benefitClaimJV.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("TipeAccount")) Then benefitClaimJV.TipeAccount = dr("TipeAccount").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("VendorID")) Then benefitClaimJV.VendorID = dr("VendorID").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Amount")) Then benefitClaimJV.Amount = CType(dr("Amount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("AccuredMonth")) Then benefitClaimJV.AccuredMount = CType(dr("AccuredMonth"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("BusinessArea")) Then benefitClaimJV.BusinessArea = dr("BusinessArea").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CostCenter")) Then benefitClaimJV.CostCenter = dr("CostCenter").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PaymentDate")) Then benefitClaimJV.PaymentDate = CType(dr("PaymentDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("ActualPaymentDate")) Then benefitClaimJV.ActualPaymentDate = CType(dr("ActualPaymentDate"), DateTime)

            If Not dr.IsDBNull(dr.GetOrdinal("JVNumber")) Then benefitClaimJV.JVNumber = dr("JVNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("InternalOrder")) Then benefitClaimJV.InternalOrder = dr("InternalOrder").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Remarks")) Then benefitClaimJV.Remarks = dr("Remarks").ToString

            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then benefitClaimJV.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then benefitClaimJV.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then benefitClaimJV.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then benefitClaimJV.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then benefitClaimJV.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            If Not dr.IsDBNull(dr.GetOrdinal("Month")) Then benefitClaimJV.Month = CType(dr("Month"), Short)
          
            If Not dr.IsDBNull(dr.GetOrdinal("BenefitClaimHeaderID")) Then
                benefitClaimJV.BenefitClaimHeader = New BenefitClaimHeader(CType(dr("BenefitClaimHeaderID"), Integer))
            End If

            If Not dr.IsDBNull(dr.GetOrdinal("MasterAccruedID")) Then
                benefitClaimJV.MasterAccrued = New MasterAccrued(CType(dr("MasterAccruedID"), Integer))
            End If

            Return benefitClaimJV

        End Function

        Private Sub SetTableName()

            If Not (GetType(BenefitClaimJV) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(BenefitClaimJV), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(BenefitClaimJV).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

