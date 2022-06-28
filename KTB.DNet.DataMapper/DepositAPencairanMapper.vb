
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : DepositAPencairan Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2008 
'// ---------------------
'// $History      : $
'// Generated on 12/4/2008 - 10:02:36 AM
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

    Public Class DepositAPencairanMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertDepositAPencairan"
        Private m_UpdateStatement As String = "up_UpdateDepositAPencairan"
        Private m_RetrieveStatement As String = "up_RetrieveDepositAPencairan"
        Private m_RetrieveListStatement As String = "up_RetrieveDepositAPencairanList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteDepositAPencairan"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim depositAPencairan As DepositAPencairan = Nothing
            While dr.Read

                depositAPencairan = Me.CreateObject(dr)

            End While

            Return depositAPencairan

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim depositAPencairanList As ArrayList = New ArrayList

            While dr.Read
                Dim depositAPencairan As DepositAPencairan = Me.CreateObject(dr)
                depositAPencairanList.Add(depositAPencairan)
            End While

            Return depositAPencairanList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim depositAPencairan As DepositAPencairan = CType(obj, DepositAPencairan)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, depositAPencairan.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim depositAPencairan As DepositAPencairan = CType(obj, DepositAPencairan)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
			DbCommandWrapper.AddInParameter("@DNNumber",DbType.AnsiStringFixedLength,depositAPencairan.DNNumber)
			DbCommandWrapper.AddInParameter("@AssignmentNumber",DbType.AnsiString,depositAPencairan.AssignmentNumber)
            DbCommandWrapper.AddInParameter("@Type", DbType.Byte, depositAPencairan.Type)
            DbCommandWrapper.AddInParameter("@NoSurat", DbType.AnsiStringFixedLength, depositAPencairan.NoSurat)
            DbCommandWrapper.AddInParameter("@DealerAmount", DbType.Currency, depositAPencairan.DealerAmount)
            DbCommandWrapper.AddInParameter("@ApprovalAmount", DbType.Currency, depositAPencairan.ApprovalAmount)
			DbCommandWrapper.AddInParameter("@Description",DbType.AnsiString,depositAPencairan.Description)
            DbCommandWrapper.AddInParameter("@KTBReason", DbType.AnsiString, depositAPencairan.KTBReason)
            DbCommandWrapper.AddInParameter("@Status", DbType.Byte, depositAPencairan.Status)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, depositAPencairan.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, depositAPencairan.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, Me.GetRefObject(depositAPencairan.Dealer))
			DbCommandWrapper.AddInParameter("@DealerBankAccountID",DbType.Int32,Me.GetRefObject(depositAPencairan.DealerBankAccount))

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

            Dim depositAPencairan As DepositAPencairan = CType(obj, DepositAPencairan)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, depositAPencairan.ID)
			DbCommandWrapper.AddInParameter("@DNNumber",DbType.AnsiStringFixedLength,depositAPencairan.DNNumber)
			DbCommandWrapper.AddInParameter("@AssignmentNumber",DbType.AnsiString,depositAPencairan.AssignmentNumber)
            DbCommandWrapper.AddInParameter("@Type", DbType.Byte, depositAPencairan.Type)
            DbCommandWrapper.AddInParameter("@NoSurat", DbType.AnsiStringFixedLength, depositAPencairan.NoSurat)
            DbCommandWrapper.AddInParameter("@DealerAmount", DbType.Currency, depositAPencairan.DealerAmount)
            DbCommandWrapper.AddInParameter("@ApprovalAmount", DbType.Currency, depositAPencairan.ApprovalAmount)
			DbCommandWrapper.AddInParameter("@Description",DbType.AnsiString,depositAPencairan.Description)
            DbCommandWrapper.AddInParameter("@KTBReason", DbType.AnsiString, depositAPencairan.KTBReason)
            DbCommandWrapper.AddInParameter("@Status", DbType.Byte, depositAPencairan.Status)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, depositAPencairan.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, depositAPencairan.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, Me.GetRefObject(depositAPencairan.Dealer))
			DbCommandWrapper.AddInParameter("@DealerBankAccountID",DbType.Int32, Me.GetRefObject(depositAPencairan.DealerBankAccount))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As DepositAPencairan

            Dim depositAPencairan As DepositAPencairan = New DepositAPencairan

            depositAPencairan.ID = CType(dr("ID"), Integer)
			if not dr.IsDBNull(dr.GetOrdinal("DNNumber")) then depositAPencairan.DNNumber = dr("DNNumber").ToString 
			if not dr.IsDBNull(dr.GetOrdinal("AssignmentNumber")) then depositAPencairan.AssignmentNumber = dr("AssignmentNumber").ToString 
            If Not dr.IsDBNull(dr.GetOrdinal("Type")) Then depositAPencairan.Type = CType(dr("Type"), Byte)
            If Not dr.IsDBNull(dr.GetOrdinal("NoSurat")) Then depositAPencairan.NoSurat = dr("NoSurat").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DealerAmount")) Then depositAPencairan.DealerAmount = CType(dr("DealerAmount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("ApprovalAmount")) Then depositAPencairan.ApprovalAmount = CType(dr("ApprovalAmount"), Decimal)
			if not dr.IsDBNull(dr.GetOrdinal("Description")) then depositAPencairan.Description = dr("Description").ToString 
            If Not dr.IsDBNull(dr.GetOrdinal("KTBReason")) Then depositAPencairan.KTBReason = dr("KTBReason").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then depositAPencairan.Status = CType(dr("Status"), Byte)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then depositAPencairan.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then depositAPencairan.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then depositAPencairan.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then depositAPencairan.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then depositAPencairan.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then
                depositAPencairan.Dealer = New Dealer(CType(dr("DealerID"), Integer))
            End If
			if not dr.IsDBNull(dr.GetOrdinal("DealerBankAccountID")) then
				depositAPencairan.DealerBankAccount = new DealerBankAccount (ctype(dr("DealerBankAccountID"), integer))
            End If

            Return depositAPencairan

        End Function

        Private Sub SetTableName()

            If Not (GetType(DepositAPencairan) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(DepositAPencairan), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(DepositAPencairan).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

