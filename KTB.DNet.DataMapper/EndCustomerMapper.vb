#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : EndCustomer Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2005 
'// ---------------------
'// $History      : $
'// Generated on 11/23/2005 - 5:22:09 PM
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

    Public Class EndCustomerMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertEndCustomer"
        Private m_UpdateStatement As String = "up_UpdateEndCustomer"
        Private m_RetrieveStatement As String = "up_RetrieveEndCustomer"
        Private m_RetrieveListStatement As String = "up_RetrieveEndCustomerList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteEndCustomer"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim endCustomer As EndCustomer = Nothing
            While dr.Read

                endCustomer = Me.CreateObject(dr)

            End While

            Return endCustomer

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim endCustomerList As ArrayList = New ArrayList

            While dr.Read
                Dim endCustomer As EndCustomer = Me.CreateObject(dr)
                endCustomerList.Add(endCustomer)
            End While

            Return endCustomerList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim endCustomer As EndCustomer = CType(obj, EndCustomer)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, endCustomer.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim endCustomer As EndCustomer = CType(obj, EndCustomer)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@ProjectIndicator", DbType.AnsiString, endCustomer.ProjectIndicator)
            DbCommandWrapper.AddInParameter("@RefChassisNumberID", DbType.Int32, IIf(endCustomer.RefChassisNumberID = 0, DBNull.Value, endCustomer.RefChassisNumberID))
            DbCommandWrapper.AddInParameter("@Name1", DbType.AnsiString, endCustomer.Name1)
            DbCommandWrapper.AddInParameter("@FakturDate", DbType.DateTime, endCustomer.FakturDate)
            DbCommandWrapper.AddInParameter("@OpenFakturDate", DbType.DateTime, endCustomer.OpenFakturDate)
            DbCommandWrapper.AddInParameter("@FakturNumber", DbType.AnsiString, endCustomer.FakturNumber)
            DbCommandWrapper.AddInParameter("@AreaViolationFlag", DbType.AnsiString, endCustomer.AreaViolationFlag)
            DbCommandWrapper.AddInParameter("@AreaViolationyAmount", DbType.Currency, IIf(endCustomer.AreaViolationyAmount = 0, DBNull.Value, endCustomer.AreaViolationyAmount))
            DbCommandWrapper.AddInParameter("@AreaViolationBankName", DbType.AnsiString, endCustomer.AreaViolationBankName)
            DbCommandWrapper.AddInParameter("@AreaViolationGyroNumber", DbType.AnsiString, endCustomer.AreaViolationGyroNumber)
            DbCommandWrapper.AddInParameter("@PenaltyFlag", DbType.AnsiString, endCustomer.PenaltyFlag)
            DbCommandWrapper.AddInParameter("@PenaltyAmount", DbType.Currency, IIf(endCustomer.PenaltyAmount = 0, DBNull.Value, endCustomer.PenaltyAmount))
            DbCommandWrapper.AddInParameter("@PenaltyBankName", DbType.AnsiString, endCustomer.PenaltyBankName)
            DbCommandWrapper.AddInParameter("@PenaltyGyroNumber", DbType.AnsiString, endCustomer.PenaltyGyroNumber)
            DbCommandWrapper.AddInParameter("@ReferenceLetterFlag", DbType.AnsiString, endCustomer.ReferenceLetterFlag)
            DbCommandWrapper.AddInParameter("@ReferenceLetter", DbType.AnsiString, endCustomer.ReferenceLetter)
            DbCommandWrapper.AddInParameter("@SaveBy", DbType.AnsiString, endCustomer.SaveBy)
            DbCommandWrapper.AddInParameter("@SaveTime", DbType.DateTime, endCustomer.SaveTime)
            DbCommandWrapper.AddInParameter("@ValidateBy", DbType.AnsiString, endCustomer.ValidateBy)
            DbCommandWrapper.AddInParameter("@ValidateTime", DbType.DateTime, endCustomer.ValidateTime)
            DbCommandWrapper.AddInParameter("@ConfirmBy", DbType.AnsiString, endCustomer.ConfirmBy)
            DbCommandWrapper.AddInParameter("@ConfirmTime", DbType.DateTime, endCustomer.ConfirmTime)
            DbCommandWrapper.AddInParameter("@DownloadBy", DbType.AnsiString, endCustomer.DownloadBy)
            DbCommandWrapper.AddInParameter("@DownloadTime", DbType.DateTime, endCustomer.DownloadTime)
            DbCommandWrapper.AddInParameter("@PrintedBy", DbType.AnsiString, endCustomer.PrintedBy)
            DbCommandWrapper.AddInParameter("@PrintedTime", DbType.DateTime, endCustomer.PrintedTime)
            DbCommandWrapper.AddInParameter("@CleansingCustomerID", DbType.Int32, endCustomer.CleansingCustomerID)
            DbCommandWrapper.AddInParameter("@MCPStatus", DbType.Int16, endCustomer.MCPStatus)
            DbCommandWrapper.AddInParameter("@LKPPStatus", DbType.Int16, endCustomer.LKPPStatus)
            DbCommandWrapper.AddInParameter("@Remark1", DbType.AnsiString, endCustomer.Remark1)
            DbCommandWrapper.AddInParameter("@Remark2", DbType.AnsiString, endCustomer.Remark2)
            DbCommandWrapper.AddInParameter("@HandoverDate", DbType.DateTime, endCustomer.HandoverDate)
            DbCommandWrapper.AddInParameter("@IsTemporary", DbType.Int16, endCustomer.IsTemporary)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, endCustomer.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, endCustomer.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@AreaViolationPaymentMethodID", DbType.Int32, IIf(endCustomer.AreaViolationPaymentMethodID = 0, DBNull.Value, endCustomer.AreaViolationPaymentMethodID))
            DbCommandWrapper.AddInParameter("@PenaltyPaymentMethodID", DbType.Int32, IIf(endCustomer.PenaltyPaymentMethodID = 0, DBNull.Value, endCustomer.PenaltyPaymentMethodID))
            DbCommandWrapper.AddInParameter("@CustomerID", DbType.Int32, Me.GetRefObject(endCustomer.Customer))

            DbCommandWrapper.AddInParameter("@MCPHeaderID", DbType.Int32, Me.GetRefObject(endCustomer.MCPHeader))
            DbCommandWrapper.AddInParameter("@LKPPHeaderID", DbType.Int32, Me.GetRefObject(endCustomer.LKPPHeader))



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

            Dim endCustomer As EndCustomer = CType(obj, EndCustomer)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, endCustomer.ID)
            DbCommandWrapper.AddInParameter("@ProjectIndicator", DbType.AnsiString, endCustomer.ProjectIndicator)
            DbCommandWrapper.AddInParameter("@RefChassisNumberID", DbType.Int32, IIf(endCustomer.RefChassisNumberID = 0, DBNull.Value, endCustomer.RefChassisNumberID))
            DbCommandWrapper.AddInParameter("@Name1", DbType.AnsiString, endCustomer.Name1)
            DbCommandWrapper.AddInParameter("@FakturDate", DbType.DateTime, endCustomer.FakturDate)
            DbCommandWrapper.AddInParameter("@OpenFakturDate", DbType.DateTime, endCustomer.OpenFakturDate)
            DbCommandWrapper.AddInParameter("@FakturNumber", DbType.AnsiString, endCustomer.FakturNumber)
            DbCommandWrapper.AddInParameter("@AreaViolationFlag", DbType.AnsiString, endCustomer.AreaViolationFlag)
            DbCommandWrapper.AddInParameter("@AreaViolationyAmount", DbType.Currency, IIf(endCustomer.AreaViolationyAmount = 0, DBNull.Value, endCustomer.AreaViolationyAmount))
            DbCommandWrapper.AddInParameter("@AreaViolationBankName", DbType.AnsiString, endCustomer.AreaViolationBankName)
            DbCommandWrapper.AddInParameter("@AreaViolationGyroNumber", DbType.AnsiString, endCustomer.AreaViolationGyroNumber)
            DbCommandWrapper.AddInParameter("@PenaltyFlag", DbType.AnsiString, endCustomer.PenaltyFlag)
            DbCommandWrapper.AddInParameter("@PenaltyAmount", DbType.Currency, IIf(endCustomer.PenaltyAmount = 0, DBNull.Value, endCustomer.PenaltyAmount))
            DbCommandWrapper.AddInParameter("@PenaltyBankName", DbType.AnsiString, endCustomer.PenaltyBankName)
            DbCommandWrapper.AddInParameter("@PenaltyGyroNumber", DbType.AnsiString, endCustomer.PenaltyGyroNumber)
            DbCommandWrapper.AddInParameter("@ReferenceLetterFlag", DbType.AnsiString, endCustomer.ReferenceLetterFlag)
            DbCommandWrapper.AddInParameter("@ReferenceLetter", DbType.AnsiString, endCustomer.ReferenceLetter)
            DbCommandWrapper.AddInParameter("@SaveBy", DbType.AnsiString, endCustomer.SaveBy)
            DbCommandWrapper.AddInParameter("@SaveTime", DbType.DateTime, endCustomer.SaveTime)
            DbCommandWrapper.AddInParameter("@ValidateBy", DbType.AnsiString, endCustomer.ValidateBy)
            DbCommandWrapper.AddInParameter("@ValidateTime", DbType.DateTime, endCustomer.ValidateTime)
            DbCommandWrapper.AddInParameter("@ConfirmBy", DbType.AnsiString, endCustomer.ConfirmBy)
            DbCommandWrapper.AddInParameter("@ConfirmTime", DbType.DateTime, endCustomer.ConfirmTime)
            DbCommandWrapper.AddInParameter("@DownloadBy", DbType.AnsiString, endCustomer.DownloadBy)
            DbCommandWrapper.AddInParameter("@DownloadTime", DbType.DateTime, endCustomer.DownloadTime)
            DbCommandWrapper.AddInParameter("@PrintedBy", DbType.AnsiString, endCustomer.PrintedBy)
            DbCommandWrapper.AddInParameter("@PrintedTime", DbType.DateTime, endCustomer.PrintedTime)
            DbCommandWrapper.AddInParameter("@CleansingCustomerID", DbType.Int32, endCustomer.CleansingCustomerID)
            DbCommandWrapper.AddInParameter("@MCPStatus", DbType.Int16, endCustomer.MCPStatus)
            DbCommandWrapper.AddInParameter("@LKPPStatus", DbType.Int16, endCustomer.LKPPStatus)
            DbCommandWrapper.AddInParameter("@Remark1", DbType.AnsiString, endCustomer.Remark1)
            DbCommandWrapper.AddInParameter("@Remark2", DbType.AnsiString, endCustomer.Remark2)
            DbCommandWrapper.AddInParameter("@HandoverDate", DbType.DateTime, endCustomer.HandoverDate)
            DbCommandWrapper.AddInParameter("@IsTemporary", DbType.Int16, endCustomer.IsTemporary)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, endCustomer.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, endCustomer.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@AreaViolationPaymentMethodID", DbType.Int32, IIf(endCustomer.AreaViolationPaymentMethodID = 0, Nothing, endCustomer.AreaViolationPaymentMethodID))
            DbCommandWrapper.AddInParameter("@PenaltyPaymentMethodID", DbType.Int32, IIf(endCustomer.PenaltyPaymentMethodID = 0, Nothing, endCustomer.PenaltyPaymentMethodID))
            DbCommandWrapper.AddInParameter("@CustomerID", DbType.Int32, Me.GetRefObject(endCustomer.Customer))

            DbCommandWrapper.AddInParameter("@MCPHeaderID", DbType.Int32, Me.GetRefObject(endCustomer.MCPHeader))
            DbCommandWrapper.AddInParameter("@LKPPHeaderID", DbType.Int32, Me.GetRefObject(endCustomer.LKPPHeader))


            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As EndCustomer

            Dim endCustomer As EndCustomer = New EndCustomer

            endCustomer.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("ProjectIndicator")) Then endCustomer.ProjectIndicator = dr("ProjectIndicator").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RefChassisNumberID")) Then endCustomer.RefChassisNumberID = CType(dr("RefChassisNumberID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Name1")) Then endCustomer.Name1 = dr("Name1").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("FakturDate")) Then endCustomer.FakturDate = CType(dr("FakturDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("OpenFakturDate")) Then endCustomer.OpenFakturDate = CType(dr("OpenFakturDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("FakturNumber")) Then endCustomer.FakturNumber = dr("FakturNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("AreaViolationFlag")) Then endCustomer.AreaViolationFlag = dr("AreaViolationFlag").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("AreaViolationyAmount")) Then endCustomer.AreaViolationyAmount = CType(dr("AreaViolationyAmount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("AreaViolationBankName")) Then endCustomer.AreaViolationBankName = dr("AreaViolationBankName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("AreaViolationGyroNumber")) Then endCustomer.AreaViolationGyroNumber = dr("AreaViolationGyroNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PenaltyFlag")) Then endCustomer.PenaltyFlag = dr("PenaltyFlag").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PenaltyAmount")) Then endCustomer.PenaltyAmount = CType(dr("PenaltyAmount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("PenaltyBankName")) Then endCustomer.PenaltyBankName = dr("PenaltyBankName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PenaltyGyroNumber")) Then endCustomer.PenaltyGyroNumber = dr("PenaltyGyroNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ReferenceLetterFlag")) Then endCustomer.ReferenceLetterFlag = dr("ReferenceLetterFlag").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ReferenceLetter")) Then endCustomer.ReferenceLetter = dr("ReferenceLetter").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SaveBy")) Then endCustomer.SaveBy = dr("SaveBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SaveTime")) Then endCustomer.SaveTime = CType(dr("SaveTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("ValidateBy")) Then endCustomer.ValidateBy = dr("ValidateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ValidateTime")) Then endCustomer.ValidateTime = CType(dr("ValidateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("ConfirmBy")) Then endCustomer.ConfirmBy = dr("ConfirmBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ConfirmTime")) Then endCustomer.ConfirmTime = CType(dr("ConfirmTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("DownloadBy")) Then endCustomer.DownloadBy = dr("DownloadBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DownloadTime")) Then endCustomer.DownloadTime = CType(dr("DownloadTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("PrintedBy")) Then endCustomer.PrintedBy = dr("PrintedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PrintedTime")) Then endCustomer.PrintedTime = CType(dr("PrintedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("CleansingCustomerID")) Then endCustomer.CleansingCustomerID = CType(dr("CleansingCustomerID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("MCPStatus")) Then endCustomer.MCPStatus = CType(dr("MCPStatus"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("LKPPStatus")) Then endCustomer.LKPPStatus = CType(dr("LKPPStatus"), Integer)

            If Not dr.IsDBNull(dr.GetOrdinal("Remark1")) Then endCustomer.Remark1 = dr("Remark1").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Remark2")) Then endCustomer.Remark2 = dr("Remark2").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("HandoverDate")) Then endCustomer.HandoverDate = CType(dr("HandoverDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("IsTemporary")) Then endCustomer.IsTemporary = CType(dr("IsTemporary"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then endCustomer.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then endCustomer.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then endCustomer.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then endCustomer.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then endCustomer.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            If Not dr.IsDBNull(dr.GetOrdinal("AreaViolationPaymentMethodID")) Then
                endCustomer.AreaViolationPaymentMethodID = CType(dr("AreaViolationPaymentMethodID"), Integer)
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("PenaltyPaymentMethodID")) Then
                endCustomer.PenaltyPaymentMethodID = CType(dr("PenaltyPaymentMethodID"), Integer)
            End If

            If Not dr.IsDBNull(dr.GetOrdinal("CustomerID")) Then
                endCustomer.Customer = New Customer(CType(dr("CustomerID"), Integer))
            End If

            If Not dr.IsDBNull(dr.GetOrdinal("MCPHeaderID")) Then
                endCustomer.MCPHeader = New MCPHeader(CType(dr("MCPHeaderID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("LKPPHeaderID")) Then
                endCustomer.LKPPHeader = New LKPPHeader(CType(dr("LKPPHeaderID"), Integer))
            End If

            Return endCustomer

        End Function

        Private Sub SetTableName()

            If Not (GetType(EndCustomer) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(EndCustomer), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(EndCustomer).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace


