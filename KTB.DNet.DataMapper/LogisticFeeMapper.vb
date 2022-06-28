
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : LogisticFee Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2017 
'// ---------------------
'// $History      : $
'// Generated on 9/14/2017 - 9:42:37 AM
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

    Public Class LogisticFeeMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertLogisticFee"
        Private m_UpdateStatement As String = "up_UpdateLogisticFee"
        Private m_RetrieveStatement As String = "up_RetrieveLogisticFee"
        Private m_RetrieveListStatement As String = "up_RetrieveLogisticFeeList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteLogisticFee"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim logisticFee As LogisticFee = Nothing
            While dr.Read

                logisticFee = Me.CreateObject(dr)

            End While

            Return logisticFee

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim logisticFeeList As ArrayList = New ArrayList

            While dr.Read
                Dim logisticFee As LogisticFee = Me.CreateObject(dr)
                logisticFeeList.Add(logisticFee)
            End While

            Return logisticFeeList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim logisticFee As LogisticFee = CType(obj, LogisticFee)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, logisticFee.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim logisticFee As LogisticFee = CType(obj, LogisticFee)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, Me.GetRefObject(logisticFee.Dealer))
            DbCommandWrapper.AddInParameter("@DebitMemoDate", DbType.DateTime, logisticFee.DebitMemoDate)
            DbCommandWrapper.AddInParameter("@LogisticDNID", DbType.Int32, Me.GetRefObject(logisticFee.LogisticDN))
            DbCommandWrapper.AddInParameter("@FileNameDebitMemo", DbType.AnsiString, logisticFee.FileNameDebitMemo)
            DbCommandWrapper.AddInParameter("@FileNameLogistic", DbType.AnsiString, logisticFee.FileNameLogistic)
            DbCommandWrapper.AddInParameter("@FakturPajakNo", DbType.AnsiString, logisticFee.FakturPajakNo)
            DbCommandWrapper.AddInParameter("@Amount", DbType.Currency, logisticFee.Amount)
            DbCommandWrapper.AddInParameter("@Status", DbType.Byte, logisticFee.Status)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, logisticFee.Description)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, logisticFee.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, logisticFee.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


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

            Dim logisticFee As LogisticFee = CType(obj, LogisticFee)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, logisticFee.ID)
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, Me.GetRefObject(logisticFee.Dealer))
            DbCommandWrapper.AddInParameter("@DebitMemoDate", DbType.DateTime, logisticFee.DebitMemoDate)
            DbCommandWrapper.AddInParameter("@LogisticDNID", DbType.Int32, Me.GetRefObject(logisticFee.LogisticDN))
            DbCommandWrapper.AddInParameter("@FileNameDebitMemo", DbType.AnsiString, logisticFee.FileNameDebitMemo)
            DbCommandWrapper.AddInParameter("@FileNameLogistic", DbType.AnsiString, logisticFee.FileNameLogistic)
            DbCommandWrapper.AddInParameter("@FakturPajakNo", DbType.AnsiString, logisticFee.FakturPajakNo)
            DbCommandWrapper.AddInParameter("@Amount", DbType.Currency, logisticFee.Amount)
            DbCommandWrapper.AddInParameter("@Status", DbType.Byte, logisticFee.Status)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, logisticFee.Description)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, logisticFee.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, logisticFee.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As LogisticFee

            Dim logisticFee As LogisticFee = New LogisticFee

            logisticFee.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("DebitMemoDate")) Then logisticFee.DebitMemoDate = CType(dr("DebitMemoDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("FileNameDebitMemo")) Then logisticFee.FileNameDebitMemo = dr("FileNameDebitMemo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("FileNameLogistic")) Then logisticFee.FileNameLogistic = dr("FileNameLogistic").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("FakturPajakNo")) Then logisticFee.FakturPajakNo = dr("FakturPajakNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Amount")) Then logisticFee.Amount = CType(dr("Amount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then logisticFee.Status = CType(dr("Status"), Byte)
            If Not dr.IsDBNull(dr.GetOrdinal("Description")) Then logisticFee.Description = dr("Description").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then logisticFee.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then logisticFee.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then logisticFee.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then logisticFee.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then logisticFee.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then
                logisticFee.Dealer = New Dealer(CType(dr("DealerID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("LogisticDNID")) Then
                logisticFee.LogisticDN = New LogisticDN(CType(dr("LogisticDNID"), Integer))
            End If
            Return logisticFee

        End Function

        Private Sub SetTableName()

            If Not (GetType(LogisticFee) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(LogisticFee), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(LogisticFee).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

