
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : BenefitClaimReceipt Objects Mapper.
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

    Public Class BenefitClaimReceiptMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertBenefitClaimReceipt"
        Private m_UpdateStatement As String = "up_UpdateBenefitClaimReceipt"
        Private m_RetrieveStatement As String = "up_RetrieveBenefitClaimReceipt"
        Private m_RetrieveListStatement As String = "up_RetrieveBenefitClaimReceiptList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteBenefitClaimReceipt"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim benefitClaimReceipt As BenefitClaimReceipt = Nothing
            While dr.Read

                benefitClaimReceipt = Me.CreateObject(dr)

            End While

            Return benefitClaimReceipt

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim benefitClaimReceiptList As ArrayList = New ArrayList

            While dr.Read
                Dim benefitClaimReceipt As BenefitClaimReceipt = Me.CreateObject(dr)
                benefitClaimReceiptList.Add(benefitClaimReceipt)
            End While

            Return benefitClaimReceiptList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim benefitClaimReceipt As BenefitClaimReceipt = CType(obj, BenefitClaimReceipt)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, benefitClaimReceipt.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim benefitClaimReceipt As BenefitClaimReceipt = CType(obj, BenefitClaimReceipt)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@ReceiptNo", DbType.AnsiString, benefitClaimReceipt.ReceiptNo)
            DbCommandWrapper.AddInParameter("@ReceiptDate", DbType.DateTime, benefitClaimReceipt.ReceiptDate)
            DbCommandWrapper.AddInParameter("@ReceiptAmount", DbType.Currency, benefitClaimReceipt.ReceiptAmount)
            DbCommandWrapper.AddInParameter("@ReceiptAmountDeducted", DbType.Currency, benefitClaimReceipt.ReceiptAmountDeducted)
            DbCommandWrapper.AddInParameter("@VATTotal", DbType.Currency, benefitClaimReceipt.VATTotal)
            DbCommandWrapper.AddInParameter("@PPHTotal", DbType.Currency, benefitClaimReceipt.PPHTotal)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, benefitClaimReceipt.Status)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, benefitClaimReceipt.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, benefitClaimReceipt.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@FakturPajakNo", DbType.AnsiString, benefitClaimReceipt.FakturPajakNo)
            DbCommandWrapper.AddInParameter("@Name", DbType.AnsiString, benefitClaimReceipt.Name)
            DbCommandWrapper.AddInParameter("@Title", DbType.AnsiString, benefitClaimReceipt.Title)

            DbCommandWrapper.AddInParameter("@BenefitClaimHeaderID", DbType.Int32, Me.GetRefObject(benefitClaimReceipt.BenefitClaimHeader))
            DbCommandWrapper.AddInParameter("@FakturPajakDate", DbType.DateTime, benefitClaimReceipt.FakturPajakDate)
            DbCommandWrapper.AddInParameter("@DealerBankAccountID", DbType.Int32, benefitClaimReceipt.DealerBankAccount.ID)
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

            Dim benefitClaimReceipt As BenefitClaimReceipt = CType(obj, BenefitClaimReceipt)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, benefitClaimReceipt.ID)
            DbCommandWrapper.AddInParameter("@ReceiptNo", DbType.AnsiString, benefitClaimReceipt.ReceiptNo)
            DbCommandWrapper.AddInParameter("@ReceiptDate", DbType.DateTime, benefitClaimReceipt.ReceiptDate)
            DbCommandWrapper.AddInParameter("@ReceiptAmount", DbType.Currency, benefitClaimReceipt.ReceiptAmount)
            DbCommandWrapper.AddInParameter("@ReceiptAmountDeducted", DbType.Currency, benefitClaimReceipt.ReceiptAmountDeducted)
            DbCommandWrapper.AddInParameter("@VATTotal", DbType.Currency, benefitClaimReceipt.VATTotal)
            DbCommandWrapper.AddInParameter("@PPHTotal", DbType.Currency, benefitClaimReceipt.PPHTotal)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, benefitClaimReceipt.Status)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, benefitClaimReceipt.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, benefitClaimReceipt.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@FakturPajakNo", DbType.AnsiString, benefitClaimReceipt.FakturPajakNo)
            DbCommandWrapper.AddInParameter("@Name", DbType.AnsiString, benefitClaimReceipt.Name)
            DbCommandWrapper.AddInParameter("@Title", DbType.AnsiString, benefitClaimReceipt.Title)
            DbCommandWrapper.AddInParameter("@FakturPajakDate", DbType.DateTime, benefitClaimReceipt.FakturPajakDate)

            DbCommandWrapper.AddInParameter("@BenefitClaimHeaderID", DbType.Int32, Me.GetRefObject(benefitClaimReceipt.BenefitClaimHeader))
            DbCommandWrapper.AddInParameter("@DealerBankAccountID", DbType.Int32, benefitClaimReceipt.DealerBankAccount.ID)

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As BenefitClaimReceipt

            Dim benefitClaimReceipt As BenefitClaimReceipt = New BenefitClaimReceipt

            benefitClaimReceipt.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("ReceiptNo")) Then benefitClaimReceipt.ReceiptNo = dr("ReceiptNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ReceiptDate")) Then benefitClaimReceipt.ReceiptDate = CType(dr("ReceiptDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("ReceiptAmount")) Then benefitClaimReceipt.ReceiptAmount = CType(dr("ReceiptAmount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("ReceiptAmountDeducted")) Then benefitClaimReceipt.ReceiptAmountDeducted = CType(dr("ReceiptAmountDeducted"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("VATTotal")) Then benefitClaimReceipt.VATTotal = CType(dr("VATTotal"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("PPHTotal")) Then benefitClaimReceipt.PPHTotal = CType(dr("PPHTotal"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then benefitClaimReceipt.Status = CType(dr("Status"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then benefitClaimReceipt.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then benefitClaimReceipt.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then benefitClaimReceipt.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then benefitClaimReceipt.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then benefitClaimReceipt.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            If Not dr.IsDBNull(dr.GetOrdinal("FakturPajakNo")) Then benefitClaimReceipt.FakturPajakNo = dr("FakturPajakNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Name")) Then benefitClaimReceipt.Name = dr("Name").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Title")) Then benefitClaimReceipt.Title = dr("Title").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("FakturPajakDate")) Then benefitClaimReceipt.FakturPajakDate = CType(dr("FakturPajakDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("BenefitClaimHeaderID")) Then
                benefitClaimReceipt.BenefitClaimHeader = New BenefitClaimHeader(CType(dr("BenefitClaimHeaderID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("DealerBankAccountID")) Then
                benefitClaimReceipt.DealerBankAccount = New DealerBankAccount(CType(dr("DealerBankAccountID"), Integer))
            End If

            Return benefitClaimReceipt

        End Function

        Private Sub SetTableName()

            If Not (GetType(BenefitClaimReceipt) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(BenefitClaimReceipt), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(BenefitClaimReceipt).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

