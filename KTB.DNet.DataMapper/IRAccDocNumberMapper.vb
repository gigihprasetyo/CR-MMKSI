
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : IRAccDocNumber Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2020 
'// ---------------------
'// $History      : $
'// Generated on 22/04/2020 - 9:34:58
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

    Public Class IRAccDocNumberMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertIRAccDocNumber"
        Private m_UpdateStatement As String = "up_UpdateIRAccDocNumber"
        Private m_RetrieveStatement As String = "up_RetrieveIRAccDocNumber"
        Private m_RetrieveListStatement As String = "up_RetrieveIRAccDocNumberList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteIRAccDocNumber"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim iRAccDocNumber As IRAccDocNumber = Nothing
            While dr.Read

                iRAccDocNumber = Me.CreateObject(dr)

            End While

            Return iRAccDocNumber

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim iRAccDocNumberList As ArrayList = New ArrayList

            While dr.Read
                Dim iRAccDocNumber As IRAccDocNumber = Me.CreateObject(dr)
                iRAccDocNumberList.Add(iRAccDocNumber)
            End While

            Return iRAccDocNumberList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim iRAccDocNumber As IRAccDocNumber = CType(obj, IRAccDocNumber)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, iRAccDocNumber.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim iRAccDocNumber As IRAccDocNumber = CType(obj, IRAccDocNumber)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@DebitChargeNo", DbType.AnsiString, iRAccDocNumber.DebitChargeNo)
            DbCommandWrapper.AddInParameter("@TRNo", DbType.AnsiString, iRAccDocNumber.TRNo)
            DbCommandWrapper.AddInParameter("@Amount", DbType.Currency, iRAccDocNumber.Amount)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, iRAccDocNumber.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, iRAccDocNumber.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@RevisionPaymentHeaderID", DbType.Int32, Me.GetRefObject(iRAccDocNumber.RevisionPaymentHeader))

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

            Dim iRAccDocNumber As IRAccDocNumber = CType(obj, IRAccDocNumber)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, iRAccDocNumber.ID)
            DbCommandWrapper.AddInParameter("@DebitChargeNo", DbType.AnsiString, iRAccDocNumber.DebitChargeNo)
            DbCommandWrapper.AddInParameter("@TRNo", DbType.AnsiString, iRAccDocNumber.TRNo)
            DbCommandWrapper.AddInParameter("@Amount", DbType.Currency, iRAccDocNumber.Amount)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, iRAccDocNumber.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, iRAccDocNumber.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@RevisionPaymentHeaderID", DbType.Int32, Me.GetRefObject(iRAccDocNumber.RevisionPaymentHeader))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As IRAccDocNumber

            Dim iRAccDocNumber As IRAccDocNumber = New IRAccDocNumber

            iRAccDocNumber.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("DebitChargeNo")) Then iRAccDocNumber.DebitChargeNo = dr("DebitChargeNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("TRNo")) Then iRAccDocNumber.TRNo = dr("TRNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Amount")) Then iRAccDocNumber.Amount = CType(dr("Amount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then iRAccDocNumber.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then iRAccDocNumber.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then iRAccDocNumber.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then iRAccDocNumber.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then iRAccDocNumber.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("RevisionPaymentHeaderID")) Then
                iRAccDocNumber.RevisionPaymentHeader = New RevisionPaymentHeader(CType(dr("RevisionPaymentHeaderID"), Integer))
            End If

            Return iRAccDocNumber

        End Function

        Private Sub SetTableName()

            If Not (GetType(IRAccDocNumber) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(IRAccDocNumber), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(IRAccDocNumber).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

