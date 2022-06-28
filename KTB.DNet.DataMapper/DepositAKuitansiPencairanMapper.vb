
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : DepositAKuitansiPencairan Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2009 
'// ---------------------
'// $History      : $
'// Generated on 1/7/2009 - 2:45:22 PM
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

    Public Class DepositAKuitansiPencairanMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertDepositAKuitansiPencairan"
        Private m_UpdateStatement As String = "up_UpdateDepositAKuitansiPencairan"
        Private m_RetrieveStatement As String = "up_RetrieveDepositAKuitansiPencairan"
        Private m_RetrieveListStatement As String = "up_RetrieveDepositAKuitansiPencairanList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteDepositAKuitansiPencairan"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim depositAKuitansiPencairan As DepositAKuitansiPencairan = Nothing
            While dr.Read

                depositAKuitansiPencairan = Me.CreateObject(dr)

            End While

            Return depositAKuitansiPencairan

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim depositAKuitansiPencairanList As ArrayList = New ArrayList

            While dr.Read
                Dim depositAKuitansiPencairan As DepositAKuitansiPencairan = Me.CreateObject(dr)
                depositAKuitansiPencairanList.Add(depositAKuitansiPencairan)
            End While

            Return depositAKuitansiPencairanList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim depositAKuitansiPencairan As DepositAKuitansiPencairan = CType(obj, DepositAKuitansiPencairan)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, depositAKuitansiPencairan.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim depositAKuitansiPencairan As DepositAKuitansiPencairan = CType(obj, DepositAKuitansiPencairan)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@Type", DbType.Byte, depositAKuitansiPencairan.Type)
            DbCommandWrapper.AddInParameter("@NoSurat", DbType.AnsiStringFixedLength, depositAKuitansiPencairan.NoSurat)
			DbCommandWrapper.AddInParameter("@DNNumber",DbType.AnsiString,depositAKuitansiPencairan.DNNumber)
			DbCommandWrapper.AddInParameter("@AssignmentNumber",DbType.AnsiString,depositAKuitansiPencairan.AssignmentNumber)
            DbCommandWrapper.AddInParameter("@RequestedTime", DbType.DateTime, depositAKuitansiPencairan.RequestedTime)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, depositAKuitansiPencairan.Description)
            DbCommandWrapper.AddInParameter("@Status", DbType.Byte, depositAKuitansiPencairan.Status)
            'DbCommandWrapper.AddInParameter("@FakturPajakNo", DbType.AnsiStringFixedLength, depositAKuitansiPencairan.FakturPajakNo)
            'DbCommandWrapper.AddInParameter("@FakturPajakDate", DbType.DateTime, depositAKuitansiPencairan.FakturPajakDate)
            DbCommandWrapper.AddInParameter("@ReceiptNumber", DbType.AnsiStringFixedLength, depositAKuitansiPencairan.ReceiptNumber)
            DbCommandWrapper.AddInParameter("@ReceiptDate", DbType.DateTime, depositAKuitansiPencairan.ReceiptDate)
            DbCommandWrapper.AddInParameter("@TotalAmount", DbType.Currency, depositAKuitansiPencairan.TotalAmount)
            DBCommandWrapper.AddInParameter("@SignedBy", DbType.AnsiString, depositAKuitansiPencairan.SignedBy)
            DBCommandWrapper.AddInParameter("@Jabatan", DbType.AnsiString, depositAKuitansiPencairan.Jabatan)
            DBCommandWrapper.AddInParameter("@TglPencairan", DbType.DateTime, depositAKuitansiPencairan.TglPencairan)
            DBCommandWrapper.AddInParameter("@IsTransfer", DbType.Byte, depositAKuitansiPencairan.IsTransfer)
            DBCommandWrapper.AddInParameter("@NoJV", DbType.AnsiString, depositAKuitansiPencairan.NoJV)
            DBCommandWrapper.AddInParameter("@NoReg", DbType.AnsiString, depositAKuitansiPencairan.NoReg)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, depositAKuitansiPencairan.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, depositAKuitansiPencairan.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, Me.GetRefObject(depositAKuitansiPencairan.Dealer))
            DbCommandWrapper.AddInParameter("@ProductCategoryID", DbType.Int32, Me.GetRefObject(depositAKuitansiPencairan.ProductCategory))

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

            Dim depositAKuitansiPencairan As DepositAKuitansiPencairan = CType(obj, DepositAKuitansiPencairan)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, depositAKuitansiPencairan.ID)
            DbCommandWrapper.AddInParameter("@Type", DbType.Byte, depositAKuitansiPencairan.Type)
            DbCommandWrapper.AddInParameter("@NoSurat", DbType.AnsiStringFixedLength, depositAKuitansiPencairan.NoSurat)
			DbCommandWrapper.AddInParameter("@DNNumber",DbType.AnsiString,depositAKuitansiPencairan.DNNumber)
			DbCommandWrapper.AddInParameter("@AssignmentNumber",DbType.AnsiString,depositAKuitansiPencairan.AssignmentNumber)
            DbCommandWrapper.AddInParameter("@RequestedTime", DbType.DateTime, depositAKuitansiPencairan.RequestedTime)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, depositAKuitansiPencairan.Description)
            DbCommandWrapper.AddInParameter("@Status", DbType.Byte, depositAKuitansiPencairan.Status)
            'DbCommandWrapper.AddInParameter("@FakturPajakNo", DbType.AnsiStringFixedLength, depositAKuitansiPencairan.FakturPajakNo)
            'DbCommandWrapper.AddInParameter("@FakturPajakDate", DbType.DateTime, depositAKuitansiPencairan.FakturPajakDate)
            DbCommandWrapper.AddInParameter("@ReceiptNumber", DbType.AnsiStringFixedLength, depositAKuitansiPencairan.ReceiptNumber)
            DbCommandWrapper.AddInParameter("@ReceiptDate", DbType.DateTime, depositAKuitansiPencairan.ReceiptDate)
            DbCommandWrapper.AddInParameter("@TotalAmount", DbType.Currency, depositAKuitansiPencairan.TotalAmount)
            DBCommandWrapper.AddInParameter("@SignedBy", DbType.AnsiString, depositAKuitansiPencairan.SignedBy)
            DBCommandWrapper.AddInParameter("@Jabatan", DbType.AnsiString, depositAKuitansiPencairan.Jabatan)
            DBCommandWrapper.AddInParameter("@TglPencairan", DbType.DateTime, depositAKuitansiPencairan.TglPencairan)
            DBCommandWrapper.AddInParameter("@IsTransfer", DbType.Byte, depositAKuitansiPencairan.IsTransfer)
            DBCommandWrapper.AddInParameter("@NoJV", DbType.AnsiString, depositAKuitansiPencairan.NoJV)
            DBCommandWrapper.AddInParameter("@NoReg", DbType.AnsiString, depositAKuitansiPencairan.NoReg)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, depositAKuitansiPencairan.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, depositAKuitansiPencairan.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, Me.GetRefObject(depositAKuitansiPencairan.Dealer))
            DbCommandWrapper.AddInParameter("@ProductCategoryID", DbType.Int32, Me.GetRefObject(depositAKuitansiPencairan.ProductCategory))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As DepositAKuitansiPencairan

            Dim depositAKuitansiPencairan As DepositAKuitansiPencairan = New DepositAKuitansiPencairan

            depositAKuitansiPencairan.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Type")) Then depositAKuitansiPencairan.Type = CType(dr("Type"), Byte)
            If Not dr.IsDBNull(dr.GetOrdinal("NoSurat")) Then depositAKuitansiPencairan.NoSurat = dr("NoSurat").ToString
			if not dr.IsDBNull(dr.GetOrdinal("DNNumber")) then depositAKuitansiPencairan.DNNumber = dr("DNNumber").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("AssignmentNumber")) then depositAKuitansiPencairan.AssignmentNumber = dr("AssignmentNumber").ToString 
            If Not dr.IsDBNull(dr.GetOrdinal("RequestedTime")) Then depositAKuitansiPencairan.RequestedTime = CType(dr("RequestedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("Description")) Then depositAKuitansiPencairan.Description = dr("Description").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then depositAKuitansiPencairan.Status = CType(dr("Status"), Byte)
            'If Not dr.IsDBNull(dr.GetOrdinal("FakturPajakNo")) Then depositAKuitansiPencairan.FakturPajakNo = dr("FakturPajakNo").ToString
            'If Not dr.IsDBNull(dr.GetOrdinal("FakturPajakDate")) Then depositAKuitansiPencairan.FakturPajakDate = CType(dr("FakturPajakDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("ReceiptNumber")) Then depositAKuitansiPencairan.ReceiptNumber = dr("ReceiptNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ReceiptDate")) Then depositAKuitansiPencairan.ReceiptDate = CType(dr("ReceiptDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("TotalAmount")) Then depositAKuitansiPencairan.TotalAmount = CType(dr("TotalAmount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("SignedBy")) Then depositAKuitansiPencairan.SignedBy = dr("SignedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Jabatan")) Then depositAKuitansiPencairan.Jabatan = dr("Jabatan").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("TglPencairan")) Then depositAKuitansiPencairan.TglPencairan = CType(dr("TglPencairan"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("IsTransfer")) Then depositAKuitansiPencairan.IsTransfer = CType(dr("IsTransfer"), Byte)
            If Not dr.IsDBNull(dr.GetOrdinal("NoJV")) Then depositAKuitansiPencairan.NoJV = dr("NoJV").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("NoReg")) Then depositAKuitansiPencairan.NoReg = dr("NoReg").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then depositAKuitansiPencairan.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then depositAKuitansiPencairan.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then depositAKuitansiPencairan.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then depositAKuitansiPencairan.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then depositAKuitansiPencairan.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then
                depositAKuitansiPencairan.Dealer = New Dealer(CType(dr("DealerID"), Integer))
            End If

            If Not dr.IsDBNull(dr.GetOrdinal("ProductCategoryID")) Then
                depositAKuitansiPencairan.ProductCategory = New ProductCategory(CType(dr("ProductCategoryID"), Integer))
            End If

            Return depositAKuitansiPencairan

        End Function

        Private Sub SetTableName()

            If Not (GetType(DepositAKuitansiPencairan) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(DepositAKuitansiPencairan), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(DepositAKuitansiPencairan).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

