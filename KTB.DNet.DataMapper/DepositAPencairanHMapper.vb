
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

    Public Class DepositAPencairanHMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertDepositAPencairanH"
        Private m_UpdateStatement As String = "up_UpdateDepositAPencairanH"
        Private m_RetrieveStatement As String = "up_RetrieveDepositAPencairanH"
        Private m_RetrieveListStatement As String = "up_RetrieveDepositAPencairanHList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteDepositAPencairanH"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim depositAPencairanH As depositAPencairanH = Nothing
            While dr.Read

                depositAPencairanH = Me.CreateObject(dr)

            End While

            Return depositAPencairanH

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim depositAPencairanHList As ArrayList = New ArrayList

            While dr.Read
                Dim depositAPencairanH As depositAPencairanH = Me.CreateObject(dr)
                depositAPencairanHList.Add(depositAPencairanH)
            End While

            Return depositAPencairanHList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim depositAPencairanH As depositAPencairanH = CType(obj, depositAPencairanH)
            DBCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DBCommandWrapper.AddInParameter("@ID", DbType.Int32, depositAPencairanH.ID)
            Return DBCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim depositAPencairanH As depositAPencairanH = CType(obj, depositAPencairanH)
            DBCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DBCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DBCommandWrapper.AddInParameter("@NoSurat", DbType.AnsiString, depositAPencairanH.NoSurat)
            DBCommandWrapper.AddInParameter("@DNNumber", DbType.AnsiString, depositAPencairanH.DNNumber)
            DBCommandWrapper.AddInParameter("@AssignmentNumber", DbType.AnsiString, depositAPencairanH.AssignmentNumber)
            DBCommandWrapper.AddInParameter("@Type", DbType.Byte, depositAPencairanH.Type)
            DBCommandWrapper.AddInParameter("@DealerAmount", DbType.Currency, depositAPencairanH.DealerAmount)
            DBCommandWrapper.AddInParameter("@ApprovalAmount", DbType.Currency, depositAPencairanH.ApprovalAmount)
            DBCommandWrapper.AddInParameter("@KTBReason", DbType.AnsiString, depositAPencairanH.KTBReason)
            DBCommandWrapper.AddInParameter("@Status", DbType.Byte, depositAPencairanH.Status)
            DBCommandWrapper.AddInParameter("@NoReg", DbType.AnsiString, depositAPencairanH.NoReg)            
            DBCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, depositAPencairanH.RowStatus)
            DBCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DBCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, depositAPencairanH.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, Me.GetRefObject(depositAPencairanH.Dealer))
            DbCommandWrapper.AddInParameter("@ProductCategoryID", DbType.Int32, Me.GetRefObject(depositAPencairanH.ProductCategory))
            DBCommandWrapper.AddInParameter("@DealerBankAccountID", DbType.Int32, Me.GetRefObject(depositAPencairanH.DealerBankAccount))
            DBCommandWrapper.AddInParameter("@DepositAInterestHID", DbType.Int32, Me.GetRefObject(depositAPencairanH.DepositAInterestH))
            DbCommandWrapper.AddInParameter("@AnnualDepositAHeaderID", DbType.Int32, Me.GetRefObject(depositAPencairanH.AnnualDepositAHeader))
            Return DBCommandWrapper

        End Function

        Protected Overrides Function GetNewID(ByVal dbCommandWrapper As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper) As Integer

            Return CType(dbCommandWrapper.GetParameterValue("@ID"), Integer)

        End Function

        Protected Overrides Function GetPagingRetrieveCommand() As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            DBCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_PagingQuery)
            DBCommandWrapper.AddInParameter("@Table", DbType.String, m_TableName)
            DBCommandWrapper.AddInParameter("@PK", DbType.String, "ID")

            Return DBCommandWrapper

        End Function

        Protected Overrides Function GetRetrieveCommand() As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            DBCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DinamicQuery)
            DBCommandWrapper.AddInParameter("@sqlQuery", DbType.String, "SELECT " + m_TableName + ".* FROM " + m_TableName)

            Return DBCommandWrapper

        End Function

        Protected Overrides Function GetRetrieveListParameter() As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            DBCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_RetrieveListStatement)
            Return DBCommandWrapper

        End Function

        Protected Overrides Function GetRetrieveParameter(ByVal id As Integer) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            DBCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_RetrieveStatement)
            DBCommandWrapper.AddInParameter("@ID", DbType.Int32, id)

            Return DBCommandWrapper

        End Function

        Protected Overrides Function GetUpdateParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim depositAPencairanH As depositAPencairanH = CType(obj, depositAPencairanH)

            DBCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DBCommandWrapper.AddInParameter("@ID", DbType.Int32, depositAPencairanH.ID)
            DBCommandWrapper.AddInParameter("@NoSurat", DbType.AnsiString, depositAPencairanH.NoSurat)
            DBCommandWrapper.AddInParameter("@DNNumber", DbType.AnsiString, depositAPencairanH.DNNumber)
            DBCommandWrapper.AddInParameter("@AssignmentNumber", DbType.AnsiString, depositAPencairanH.AssignmentNumber)
            DBCommandWrapper.AddInParameter("@Type", DbType.Byte, depositAPencairanH.Type)
            DBCommandWrapper.AddInParameter("@DealerAmount", DbType.Currency, depositAPencairanH.DealerAmount)
            DBCommandWrapper.AddInParameter("@ApprovalAmount", DbType.Currency, depositAPencairanH.ApprovalAmount)
            DBCommandWrapper.AddInParameter("@KTBReason", DbType.AnsiString, depositAPencairanH.KTBReason)
            DBCommandWrapper.AddInParameter("@Status", DbType.Byte, depositAPencairanH.Status)
            DBCommandWrapper.AddInParameter("@NoReg", DbType.AnsiString, depositAPencairanH.NoReg)            
            DBCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, depositAPencairanH.RowStatus)
            DBCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, depositAPencairanH.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DBCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, Me.GetRefObject(depositAPencairanH.Dealer))
            DbCommandWrapper.AddInParameter("@ProductCategoryID", DbType.Int32, Me.GetRefObject(depositAPencairanH.ProductCategory))
            DBCommandWrapper.AddInParameter("@DealerBankAccountID", DbType.Int32, Me.GetRefObject(depositAPencairanH.DealerBankAccount))
            DBCommandWrapper.AddInParameter("@DepositAInterestHID", DbType.Int32, Me.GetRefObject(depositAPencairanH.DepositAInterestH))
            DbCommandWrapper.AddInParameter("@AnnualDepositAHeaderID", DbType.Int32, Me.GetRefObject(depositAPencairanH.AnnualDepositAHeader))
            Return DBCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As DepositAPencairanH

            Dim depositAPencairanH As depositAPencairanH = New depositAPencairanH

            depositAPencairanH.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("NoSurat")) Then depositAPencairanH.NoSurat = dr("NoSurat").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DNNumber")) Then depositAPencairanH.DNNumber = dr("DNNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("AssignmentNumber")) Then depositAPencairanH.AssignmentNumber = dr("AssignmentNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Type")) Then depositAPencairanH.Type = CType(dr("Type"), Byte)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerAmount")) Then depositAPencairanH.DealerAmount = CType(dr("DealerAmount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("ApprovalAmount")) Then depositAPencairanH.ApprovalAmount = CType(dr("ApprovalAmount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("KTBReason")) Then depositAPencairanH.KTBReason = dr("KTBReason").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then depositAPencairanH.Status = CType(dr("Status"), Byte)
            If Not dr.IsDBNull(dr.GetOrdinal("NoReg")) Then depositAPencairanH.NoReg = dr("NoReg").ToString            
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then depositAPencairanH.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then depositAPencairanH.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then depositAPencairanH.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then depositAPencairanH.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then depositAPencairanH.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then
                depositAPencairanH.Dealer = New Dealer(CType(dr("DealerID"), Integer))
            End If

            If Not dr.IsDBNull(dr.GetOrdinal("ProductCategoryID")) Then
                depositAPencairanH.ProductCategory = New ProductCategory(CType(dr("ProductCategoryID"), Integer))
            End If

            If Not dr.IsDBNull(dr.GetOrdinal("DealerBankAccountID")) Then
                depositAPencairanH.DealerBankAccount = New DealerBankAccount(CType(dr("DealerBankAccountID"), Integer))
            End If

            If Not dr.IsDBNull(dr.GetOrdinal("DepositAInterestHID")) Then
                depositAPencairanH.DepositAInterestH = New DepositAInterestH(CType(dr("DepositAInterestHID"), Integer))
            End If
 
            If Not dr.IsDBNull(dr.GetOrdinal("AnnualDepositAHeaderID")) Then
                depositAPencairanH.AnnualDepositAHeader = New AnnualDepositAHeader(CType(dr("AnnualDepositAHeaderID"), Integer))
            End If


            Return depositAPencairanH

        End Function

        Private Sub SetTableName()

			if not (gettype (DepositAPencairanH) is nothing) then

				dim attr as TableInfoAttribute = ctype(Attribute.GetCustomAttribute(gettype(DepositAPencairanH),gettype(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
					throw new SearchException(gettype(DepositAPencairanH).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

