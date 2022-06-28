
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : IndentPartHeader Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2008 
'// ---------------------
'// $History      : $
'// Generated on 12/12/2008 - 1:51:16 PM
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

    Public Class IndentPartHeaderMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertIndentPartHeader"
        Private m_UpdateStatement As String = "up_UpdateIndentPartHeader"
        Private m_RetrieveStatement As String = "up_RetrieveIndentPartHeader"
        Private m_RetrieveListStatement As String = "up_RetrieveIndentPartHeaderList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteIndentPartHeader"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim indentPartHeader As IndentPartHeader = Nothing
            While dr.Read

                indentPartHeader = Me.CreateObject(dr)

            End While

            Return indentPartHeader

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim indentPartHeaderList As ArrayList = New ArrayList

            While dr.Read
                Dim indentPartHeader As IndentPartHeader = Me.CreateObject(dr)
                indentPartHeaderList.Add(indentPartHeader)
            End While

            Return indentPartHeaderList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim indentPartHeader As IndentPartHeader = CType(obj, IndentPartHeader)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, indentPartHeader.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim indentPartHeader As IndentPartHeader = CType(obj, IndentPartHeader)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@RequestNo", DbType.AnsiString, indentPartHeader.RequestNo)
            DbCommandWrapper.AddInParameter("@RequestDate", DbType.DateTime, indentPartHeader.RequestDate)
            DbCommandWrapper.AddInParameter("@MaterialType", DbType.Int32, indentPartHeader.MaterialType)
            DbCommandWrapper.AddInParameter("@Status", DbType.Byte, indentPartHeader.Status)
            DbCommandWrapper.AddInParameter("@StatusKTB", DbType.Byte, indentPartHeader.StatusKTB)
            DbCommandWrapper.AddInParameter("@SubmitFile", DbType.AnsiString, indentPartHeader.SubmitFile)
            DbCommandWrapper.AddInParameter("@PaymentType", DbType.Byte, indentPartHeader.PaymentType)
            DbCommandWrapper.AddInParameter("@Price", DbType.Currency, indentPartHeader.Price)
            DbCommandWrapper.AddInParameter("@KTBConfirmedDate", DbType.DateTime, indentPartHeader.KTBConfirmedDate)
            DbCommandWrapper.AddInParameter("@DescID", DbType.Byte, indentPartHeader.DescID)
            DbCommandWrapper.AddInParameter("@ChassisNumber", DbType.AnsiString, indentPartHeader.ChassisNumber)
            DbCommandWrapper.AddInParameter("@DMSPRNo", DbType.AnsiString, indentPartHeader.DMSPRNo)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, indentPartHeader.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, indentPartHeader.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, Me.GetRefObject(indentPartHeader.Dealer))
            DbCommandWrapper.AddInParameter("@TermOfPaymentID", DbType.Int32, Me.GetRefObject(indentPartHeader.TermOfPayment))
            DbCommandWrapper.AddInParameter("@TOPBlockStatusID", DbType.Int32, Me.GetRefObject(indentPartHeader.TOPBlockStatus))

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

            Dim indentPartHeader As IndentPartHeader = CType(obj, IndentPartHeader)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, indentPartHeader.ID)
            DbCommandWrapper.AddInParameter("@RequestNo", DbType.AnsiString, indentPartHeader.RequestNo)
            DbCommandWrapper.AddInParameter("@RequestDate", DbType.DateTime, indentPartHeader.RequestDate)
            DbCommandWrapper.AddInParameter("@MaterialType", DbType.Int32, indentPartHeader.MaterialType)
            DbCommandWrapper.AddInParameter("@Status", DbType.Byte, indentPartHeader.Status)
            DbCommandWrapper.AddInParameter("@StatusKTB", DbType.Byte, indentPartHeader.StatusKTB)
            DbCommandWrapper.AddInParameter("@SubmitFile", DbType.AnsiString, indentPartHeader.SubmitFile)
            DbCommandWrapper.AddInParameter("@PaymentType", DbType.Byte, indentPartHeader.PaymentType)
            DbCommandWrapper.AddInParameter("@Price", DbType.Currency, indentPartHeader.Price)
            DbCommandWrapper.AddInParameter("@KTBConfirmedDate", DbType.DateTime, indentPartHeader.KTBConfirmedDate)
            DbCommandWrapper.AddInParameter("@DescID", DbType.Byte, indentPartHeader.DescID)
            DbCommandWrapper.AddInParameter("@ChassisNumber", DbType.AnsiString, indentPartHeader.ChassisNumber)
            DbCommandWrapper.AddInParameter("@DMSPRNo", DbType.AnsiString, indentPartHeader.DMSPRNo)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, indentPartHeader.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, indentPartHeader.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, Me.GetRefObject(indentPartHeader.Dealer))
            DbCommandWrapper.AddInParameter("@TermOfPaymentID", DbType.Int16, Me.GetRefObject(indentPartHeader.TermOfPayment))
            DbCommandWrapper.AddInParameter("@TOPBlockStatusID", DbType.Int32, Me.GetRefObject(indentPartHeader.TOPBlockStatus))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As IndentPartHeader

            Dim indentPartHeader As IndentPartHeader = New IndentPartHeader

            indentPartHeader.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("RequestNo")) Then indentPartHeader.RequestNo = dr("RequestNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RequestDate")) Then indentPartHeader.RequestDate = CType(dr("RequestDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("MaterialType")) Then indentPartHeader.MaterialType = CType(dr("MaterialType"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then indentPartHeader.Status = CType(dr("Status"), Byte)
            If Not dr.IsDBNull(dr.GetOrdinal("StatusKTB")) Then indentPartHeader.StatusKTB = CType(dr("StatusKTB"), Byte)
            If Not dr.IsDBNull(dr.GetOrdinal("SubmitFile")) Then indentPartHeader.SubmitFile = dr("SubmitFile").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PaymentType")) Then indentPartHeader.PaymentType = CType(dr("PaymentType"), Byte)
            If Not dr.IsDBNull(dr.GetOrdinal("Price")) Then indentPartHeader.Price = CType(dr("Price"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("KTBConfirmedDate")) Then indentPartHeader.KTBConfirmedDate = CType(dr("KTBConfirmedDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("DescID")) Then indentPartHeader.DescID = CType(dr("DescID"), Byte)
            If Not dr.IsDBNull(dr.GetOrdinal("ChassisNumber")) Then indentPartHeader.ChassisNumber = dr("ChassisNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DMSPRNo")) Then indentPartHeader.DMSPRNo = dr("DMSPRNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then indentPartHeader.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then indentPartHeader.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then indentPartHeader.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then indentPartHeader.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then indentPartHeader.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then
                indentPartHeader.Dealer = New Dealer(CType(dr("DealerID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("TermOfPaymentID")) Then
                indentPartHeader.TermOfPayment = New TermOfPayment(CType(dr("TermOfPaymentID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("TOPBlockStatusID")) Then
                indentPartHeader.TOPBlockStatus = New TOPBlockStatus(CType(dr("TOPBlockStatusID"), Integer))
            End If
            Return indentPartHeader

        End Function

        Private Sub SetTableName()

            If Not (GetType(IndentPartHeader) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(IndentPartHeader), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(IndentPartHeader).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

