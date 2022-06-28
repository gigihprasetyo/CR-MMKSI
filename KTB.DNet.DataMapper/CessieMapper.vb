
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : Cessie Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2010 
'// ---------------------
'// $History      : $
'// Generated on 10/8/2010 - 10:51:40 AM
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

    Public Class CessieMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertCessie"
        Private m_UpdateStatement As String = "up_UpdateCessie"
        Private m_RetrieveStatement As String = "up_RetrieveCessie"
        Private m_RetrieveListStatement As String = "up_RetrieveCessieList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteCessie"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim cessie As Cessie = Nothing
            While dr.Read

                cessie = Me.CreateObject(dr)

            End While

            Return cessie

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim cessieList As ArrayList = New ArrayList

            While dr.Read
                Dim cessie As Cessie = Me.CreateObject(dr)
                cessieList.Add(cessie)
            End While

            Return cessieList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim cessie As Cessie = CType(obj, Cessie)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, cessie.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim cessie As Cessie = CType(obj, Cessie)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@CessieNumber", DbType.AnsiString, cessie.CessieNumber)
            DbCommandWrapper.AddInParameter("@CessieDate", DbType.DateTime, cessie.CessieDate)
            DbCommandWrapper.AddInParameter("@Amount", DbType.Currency, cessie.Amount)
            DbCommandWrapper.AddInParameter("@PaymentDate", DbType.DateTime, cessie.PaymentDate)
            DbCommandWrapper.AddInParameter("@PurchaseAmount", DbType.Currency, cessie.PurchaseAmount)
            DBCommandWrapper.AddInParameter("@PDFFile", DbType.AnsiString, cessie.PDFFile)
            DBCommandWrapper.AddInParameter("@PDFFile2", DbType.AnsiString, cessie.PDFFile2)
            DBCommandWrapper.AddInParameter("@TextFile", DbType.AnsiString, cessie.TextFile)
            DBCommandWrapper.AddInParameter("@DownloadedTime", DbType.DateTime, cessie.DownloadedTime)
            DbCommandWrapper.AddInParameter("@DownloadedBy", DbType.AnsiString, cessie.DownloadedBy)
            DBCommandWrapper.AddInParameter("@AdminFee", DbType.Currency, cessie.AdminFee)
            DBCommandWrapper.AddInParameter("@DifferenceAmount", DbType.Currency, cessie.DifferenceAmount)
            DBCommandWrapper.AddInParameter("@NumOfTransfered", DbType.Int32, cessie.NumOfTransfered)
            DBCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, cessie.RowStatus)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, cessie.LastUpdateBy)

            DBCommandWrapper.AddInParameter("@ProductCategoryID", DbType.Int16, Me.GetRefObject(cessie.ProductCategory))
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

            Dim cessie As Cessie = CType(obj, Cessie)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, cessie.ID)
            DbCommandWrapper.AddInParameter("@CessieNumber", DbType.AnsiString, cessie.CessieNumber)
            DbCommandWrapper.AddInParameter("@CessieDate", DbType.DateTime, cessie.CessieDate)
            DbCommandWrapper.AddInParameter("@Amount", DbType.Currency, cessie.Amount)
            DbCommandWrapper.AddInParameter("@PaymentDate", DbType.DateTime, cessie.PaymentDate)
            DbCommandWrapper.AddInParameter("@PurchaseAmount", DbType.Currency, cessie.PurchaseAmount)
            DBCommandWrapper.AddInParameter("@PDFFile", DbType.AnsiString, cessie.PDFFile)
            DBCommandWrapper.AddInParameter("@PDFFile2", DbType.AnsiString, cessie.PDFFile2)
            DBCommandWrapper.AddInParameter("@TextFile", DbType.AnsiString, cessie.TextFile)
            DBCommandWrapper.AddInParameter("@DownloadedTime", DbType.DateTime, cessie.DownloadedTime)
            DbCommandWrapper.AddInParameter("@DownloadedBy", DbType.AnsiString, cessie.DownloadedBy)
            DBCommandWrapper.AddInParameter("@AdminFee", DbType.Currency, cessie.AdminFee)
            DBCommandWrapper.AddInParameter("@DifferenceAmount", DbType.Currency, cessie.DifferenceAmount)
            DBCommandWrapper.AddInParameter("@NumOfTransfered", DbType.Int32, cessie.NumOfTransfered)
            DBCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, cessie.RowStatus)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, cessie.CreatedBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)

            DBCommandWrapper.AddInParameter("@ProductCategoryID", DbType.Int16, Me.GetRefObject(cessie.ProductCategory))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As Cessie

            Dim cessie As Cessie = New Cessie

            cessie.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("CessieNumber")) Then cessie.CessieNumber = dr("CessieNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CessieDate")) Then cessie.CessieDate = CType(dr("CessieDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("Amount")) Then cessie.Amount = CType(dr("Amount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("PaymentDate")) Then cessie.PaymentDate = CType(dr("PaymentDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("PurchaseAmount")) Then cessie.PurchaseAmount = CType(dr("PurchaseAmount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("PDFFile")) Then cessie.PDFFile = dr("PDFFile").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PDFFile2")) Then cessie.PDFFile2 = dr("PDFFile2").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("TextFile")) Then cessie.TextFile = dr("TextFile").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DownloadedTime")) Then cessie.DownloadedTime = CType(dr("DownloadedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("DownloadedBy")) Then cessie.DownloadedBy = dr("DownloadedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("AdminFee")) Then cessie.AdminFee = CType(dr("AdminFee"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("DifferenceAmount")) Then cessie.DifferenceAmount = CType(dr("DifferenceAmount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("NumOfTransfered")) Then cessie.NumOfTransfered = CType(dr("NumOfTransfered"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then cessie.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then cessie.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then cessie.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then cessie.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then cessie.LastUpdateBy = dr("LastUpdateBy").ToString

            If Not dr.IsDBNull(dr.GetOrdinal("ProductCategoryID")) Then
                cessie.ProductCategory = New ProductCategory(CType(dr("ProductCategoryID"), Short))
            End If

            Return cessie

        End Function

        Private Sub SetTableName()

            If Not (GetType(Cessie) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(Cessie), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(Cessie).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

