#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : BabitPayment Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2007 
'// ---------------------
'// $History      : $
'// Generated on 9/7/2007 - 3:58:55 PM
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

    Public Class BabitPaymentMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertBabitPayment"
        Private m_UpdateStatement As String = "up_UpdateBabitPayment"
        Private m_RetrieveStatement As String = "up_RetrieveBabitPayment"
        Private m_RetrieveListStatement As String = "up_RetrieveBabitPaymentList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteBabitPayment"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim babitPayment As BabitPayment = Nothing
            While dr.Read

                babitPayment = Me.CreateObject(dr)

            End While

            Return babitPayment

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim babitPaymentList As ArrayList = New ArrayList

            While dr.Read
                Dim babitPayment As BabitPayment = Me.CreateObject(dr)
                babitPaymentList.Add(babitPayment)
            End While

            Return babitPaymentList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim babitPayment As BabitPayment = CType(obj, BabitPayment)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, babitPayment.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim babitPayment As BabitPayment = CType(obj, BabitPayment)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@PaymentDate", DbType.DateTime, babitPayment.PaymentDate)
            DbCommandWrapper.AddInParameter("@DealerInvoice", DbType.AnsiString, babitPayment.DealerInvoice)
            DbCommandWrapper.AddInParameter("@PaymentStatus", DbType.Int32, babitPayment.PaymentStatus)
            DbCommandWrapper.AddInParameter("@NomorPembayaran", DbType.AnsiString, babitPayment.NomorPembayaran)
            DbCommandWrapper.AddInParameter("@Keterangan", DbType.AnsiString, babitPayment.Keterangan)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, babitPayment.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, babitPayment.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, Me.GetRefObject(babitPayment.Dealer))
            DbCommandWrapper.AddInParameter("@BabitProposalID", DbType.Int32, Me.GetRefObject(babitPayment.BabitProposal))
            DbCommandWrapper.AddInParameter("@GLAccountID", DbType.Int32, Me.GetRefObject(babitPayment.GLAccount))
            DbCommandWrapper.AddInParameter("@CostCenterID", DbType.Int32, Me.GetRefObject(babitPayment.CostCenter))

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

            Dim babitPayment As BabitPayment = CType(obj, BabitPayment)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, babitPayment.ID)
            DbCommandWrapper.AddInParameter("@PaymentDate", DbType.DateTime, babitPayment.PaymentDate)
            DbCommandWrapper.AddInParameter("@DealerInvoice", DbType.AnsiString, babitPayment.DealerInvoice)
            DbCommandWrapper.AddInParameter("@PaymentStatus", DbType.Int32, babitPayment.PaymentStatus)
            DbCommandWrapper.AddInParameter("@NomorPembayaran", DbType.AnsiString, babitPayment.NomorPembayaran)
            DbCommandWrapper.AddInParameter("@Keterangan", DbType.AnsiString, babitPayment.Keterangan)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, babitPayment.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, babitPayment.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, Me.GetRefObject(babitPayment.Dealer))
            DbCommandWrapper.AddInParameter("@BabitProposalID", DbType.Int32, Me.GetRefObject(babitPayment.BabitProposal))
            DbCommandWrapper.AddInParameter("@GLAccountID", DbType.Int32, Me.GetRefObject(babitPayment.GLAccount))
            DbCommandWrapper.AddInParameter("@CostCenterID", DbType.Int32, Me.GetRefObject(babitPayment.CostCenter))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As BabitPayment

            Dim babitPayment As BabitPayment = New BabitPayment

            babitPayment.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("PaymentDate")) Then babitPayment.PaymentDate = CType(dr("PaymentDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerInvoice")) Then babitPayment.DealerInvoice = dr("DealerInvoice").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PaymentStatus")) Then babitPayment.PaymentStatus = CType(dr("PaymentStatus"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("NomorPembayaran")) Then babitPayment.NomorPembayaran = dr("NomorPembayaran").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Keterangan")) Then babitPayment.Keterangan = dr("Keterangan").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then babitPayment.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then babitPayment.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then babitPayment.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then babitPayment.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then babitPayment.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then
                babitPayment.Dealer = New Dealer(CType(dr("DealerID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("BabitProposalID")) Then
                babitPayment.BabitProposal = New BabitProposal(CType(dr("BabitProposalID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("GLAccountID")) Then
                babitPayment.GLAccount = New GLAccount(CType(dr("GLAccountID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("CostCenterID")) Then
                babitPayment.CostCenter = New CostCenter(CType(dr("CostCenterID"), Integer))
            End If

            Return babitPayment

        End Function

        Private Sub SetTableName()

            If Not (GetType(BabitPayment) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(BabitPayment), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(BabitPayment).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

