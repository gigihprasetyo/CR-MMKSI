#Region "Summary"
'// ===========================================================================
'// AUTHOR        : Dnet
'// PURPOSE       : InterestPPHHeader Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2021 
'// ---------------------
'// $History      : $
'// Generated on 8/9/2021 - 10:55:17 AM
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

    Public Class InterestPPHHeaderMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertInterestPPHHeader"
        Private m_UpdateStatement As String = "up_UpdateInterestPPHHeader"
        Private m_RetrieveStatement As String = "up_RetrieveInterestPPHHeader"
        Private m_RetrieveListStatement As String = "up_RetrieveInterestPPHHeaderList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteInterestPPHHeader"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim interestPPHHeader As InterestPPHHeader = Nothing
            While dr.Read

                interestPPHHeader = Me.CreateObject(dr)

            End While

            Return interestPPHHeader

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim interestPPHHeaderList As ArrayList = New ArrayList

            While dr.Read
                Dim interestPPHHeader As InterestPPHHeader = Me.CreateObject(dr)
                interestPPHHeaderList.Add(interestPPHHeader)
            End While

            Return interestPPHHeaderList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim interestPPHHeader As InterestPPHHeader = CType(obj, InterestPPHHeader)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, interestPPHHeader.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim interestPPHHeader As InterestPPHHeader = CType(obj, InterestPPHHeader)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@NoReg", DbType.AnsiString, interestPPHHeader.NoReg)
            DbCommandWrapper.AddInParameter("@WitholdingNumber", DbType.AnsiString, interestPPHHeader.WitholdingNumber)
            DbCommandWrapper.AddInParameter("@WitholdingDate", DbType.DateTime, interestPPHHeader.WitholdingDate)
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, Me.GetRefObject(interestPPHHeader.Dealer))
            DbCommandWrapper.AddInParameter("@TaxPeriod", DbType.DateTime, interestPPHHeader.TaxPeriod)
            DbCommandWrapper.AddInParameter("@DealerTaxName", DbType.AnsiString, interestPPHHeader.DealerTaxName)
            DbCommandWrapper.AddInParameter("@DealerSignatureName", DbType.AnsiString, interestPPHHeader.DealerSignatureName)
            DbCommandWrapper.AddInParameter("@TotalDPPAmount", DbType.Currency, interestPPHHeader.TotalDPPAmount)
            DbCommandWrapper.AddInParameter("@TotalPPHAmount", DbType.Currency, interestPPHHeader.TotalPPHAmount)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, interestPPHHeader.Description)
            DbCommandWrapper.AddInParameter("@TaxInformation", DbType.AnsiString, interestPPHHeader.TaxInformation)
            DbCommandWrapper.AddInParameter("@Remark", DbType.AnsiString, interestPPHHeader.Remark)
            DbCommandWrapper.AddInParameter("@JVReturn", DbType.AnsiString, interestPPHHeader.JVReturn)
            DbCommandWrapper.AddInParameter("@SubmissionStatus", DbType.Int16, interestPPHHeader.SubmissionStatus)
            DbCommandWrapper.AddInParameter("@EvidencePDFName", DbType.AnsiString, interestPPHHeader.EvidencePDFName)
            DbCommandWrapper.AddInParameter("@EvidencePDFPath", DbType.AnsiString, interestPPHHeader.EvidencePDFPath)
            DbCommandWrapper.AddInParameter("@ReferenceDocName", DbType.AnsiString, interestPPHHeader.ReferenceDocName)
            DbCommandWrapper.AddInParameter("@ReferenceDocPath", DbType.AnsiString, interestPPHHeader.ReferenceDocPath)
            DbCommandWrapper.AddInParameter("@ReferenceDocType", DbType.AnsiString, interestPPHHeader.ReferenceDocType)
            DbCommandWrapper.AddInParameter("@ReferenceDocNumber", DbType.AnsiString, interestPPHHeader.ReferenceDocNumber)
            DbCommandWrapper.AddInParameter("@DealerNPWP", DbType.AnsiString, interestPPHHeader.DealerNPWP)
            DbCommandWrapper.AddInParameter("@PembetulanKe", DbType.Int16, interestPPHHeader.PembetulanKe)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, interestPPHHeader.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, interestPPHHeader.LastUpdateBy)
            DbCommandWrapper.AddInParameter("@LastUpdateTime", DbType.DateTime, interestPPHHeader.LastUpdateTime)


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

            Dim interestPPHHeader As InterestPPHHeader = CType(obj, InterestPPHHeader)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, interestPPHHeader.ID)
            DbCommandWrapper.AddInParameter("@NoReg", DbType.AnsiString, interestPPHHeader.NoReg)
            DbCommandWrapper.AddInParameter("@WitholdingNumber", DbType.AnsiString, interestPPHHeader.WitholdingNumber)
            DbCommandWrapper.AddInParameter("@WitholdingDate", DbType.DateTime, interestPPHHeader.WitholdingDate)
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, Me.GetRefObject(interestPPHHeader.Dealer))
            DbCommandWrapper.AddInParameter("@TaxPeriod", DbType.DateTime, interestPPHHeader.TaxPeriod)
            DbCommandWrapper.AddInParameter("@DealerTaxName", DbType.AnsiString, interestPPHHeader.DealerTaxName)
            DbCommandWrapper.AddInParameter("@DealerSignatureName", DbType.AnsiString, interestPPHHeader.DealerSignatureName)
            DbCommandWrapper.AddInParameter("@TotalDPPAmount", DbType.Currency, interestPPHHeader.TotalDPPAmount)
            DbCommandWrapper.AddInParameter("@TotalPPHAmount", DbType.Currency, interestPPHHeader.TotalPPHAmount)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, interestPPHHeader.Description)
            DbCommandWrapper.AddInParameter("@TaxInformation", DbType.AnsiString, interestPPHHeader.TaxInformation)
            DbCommandWrapper.AddInParameter("@Remark", DbType.AnsiString, interestPPHHeader.Remark)
            DbCommandWrapper.AddInParameter("@JVReturn", DbType.AnsiString, interestPPHHeader.JVReturn)
            DbCommandWrapper.AddInParameter("@SubmissionStatus", DbType.Int16, interestPPHHeader.SubmissionStatus)
            DbCommandWrapper.AddInParameter("@EvidencePDFName", DbType.AnsiString, interestPPHHeader.EvidencePDFName)
            DbCommandWrapper.AddInParameter("@EvidencePDFPath", DbType.AnsiString, interestPPHHeader.EvidencePDFPath)
            DbCommandWrapper.AddInParameter("@ReferenceDocName", DbType.AnsiString, interestPPHHeader.ReferenceDocName)
            DbCommandWrapper.AddInParameter("@ReferenceDocPath", DbType.AnsiString, interestPPHHeader.ReferenceDocPath)
            DbCommandWrapper.AddInParameter("@ReferenceDocType", DbType.AnsiString, interestPPHHeader.ReferenceDocType)
            DbCommandWrapper.AddInParameter("@ReferenceDocNumber", DbType.AnsiString, interestPPHHeader.ReferenceDocNumber)
            DbCommandWrapper.AddInParameter("@DealerNPWP", DbType.AnsiString, interestPPHHeader.DealerNPWP)
            DbCommandWrapper.AddInParameter("@PembetulanKe", DbType.Int16, interestPPHHeader.PembetulanKe)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, interestPPHHeader.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, interestPPHHeader.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime", DbType.DateTime, interestPPHHeader.LastUpdateTime)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As InterestPPHHeader

            Dim interestPPHHeader As InterestPPHHeader = New InterestPPHHeader

            interestPPHHeader.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("NoReg")) Then interestPPHHeader.NoReg = dr("NoReg").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("WitholdingNumber")) Then interestPPHHeader.WitholdingNumber = dr("WitholdingNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("WitholdingDate")) Then interestPPHHeader.WitholdingDate = CType(dr("WitholdingDate"), DateTime)
            'If Not dr.IsDBNull(dr.GetOrdinal("DedalerID")) Then interestPPHHeader.DedalerID = CType(dr("DedalerID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("TaxPeriod")) Then interestPPHHeader.TaxPeriod = CType(dr("TaxPeriod"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerTaxName")) Then interestPPHHeader.DealerTaxName = dr("DealerTaxName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DealerSignatureName")) Then interestPPHHeader.DealerSignatureName = dr("DealerSignatureName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("TotalDPPAmount")) Then interestPPHHeader.TotalDPPAmount = CType(dr("TotalDPPAmount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("TotalPPHAmount")) Then interestPPHHeader.TotalPPHAmount = CType(dr("TotalPPHAmount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("Description")) Then interestPPHHeader.Description = dr("Description").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("TaxInformation")) Then interestPPHHeader.TaxInformation = dr("TaxInformation").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Remark")) Then interestPPHHeader.Remark = dr("Remark").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("JVReturn")) Then interestPPHHeader.JVReturn = dr("JVReturn").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SubmissionStatus")) Then interestPPHHeader.SubmissionStatus = CType(dr("SubmissionStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("EvidencePDFName")) Then interestPPHHeader.EvidencePDFName = dr("EvidencePDFName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("EvidencePDFPath")) Then interestPPHHeader.EvidencePDFPath = dr("EvidencePDFPath").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ReferenceDocName")) Then interestPPHHeader.ReferenceDocName = dr("ReferenceDocName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ReferenceDocPath")) Then interestPPHHeader.ReferenceDocPath = dr("ReferenceDocPath").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ReferenceDocType")) Then interestPPHHeader.ReferenceDocType = dr("ReferenceDocType").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ReferenceDocNumber")) Then interestPPHHeader.ReferenceDocNumber = dr("ReferenceDocNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DealerNPWP")) Then interestPPHHeader.DealerNPWP = dr("DealerNPWP").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PembetulanKe")) Then interestPPHHeader.PembetulanKe = CType(dr("PembetulanKe"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then interestPPHHeader.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then interestPPHHeader.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then interestPPHHeader.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then interestPPHHeader.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then interestPPHHeader.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then
                interestPPHHeader.Dealer = New Dealer(CType(dr("DealerID"), Integer))
            End If

            Return interestPPHHeader

        End Function

        Private Sub SetTableName()

            If Not (GetType(InterestPPHHeader) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(InterestPPHHeader), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(InterestPPHHeader).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace
