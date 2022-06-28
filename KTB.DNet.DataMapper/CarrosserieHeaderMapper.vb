
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : Mitrais Team
'// PURPOSE       : CarrosserieHeader Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 21/03/2018 - 10:47:52
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

    Public Class CarrosserieHeaderMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertCarrosserieHeader"
        Private m_UpdateStatement As String = "up_UpdateCarrosserieHeader"
        Private m_RetrieveStatement As String = "up_RetrieveCarrosserieHeader"
        Private m_RetrieveListStatement As String = "up_RetrieveCarrosserieHeaderList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteCarrosserieHeader"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim carrosserieHeader As CarrosserieHeader = Nothing
            While dr.Read

                carrosserieHeader = Me.CreateObject(dr)

            End While

            Return carrosserieHeader

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim carrosserieHeaderList As ArrayList = New ArrayList

            While dr.Read
                Dim carrosserieHeader As CarrosserieHeader = Me.CreateObject(dr)
                carrosserieHeaderList.Add(carrosserieHeader)
            End While

            Return carrosserieHeaderList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim carrosserieHeader As CarrosserieHeader = CType(obj, CarrosserieHeader)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, carrosserieHeader.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim carrosserieHeader As CarrosserieHeader = CType(obj, CarrosserieHeader)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@PDIStateCode", DbType.Int16, carrosserieHeader.PDIStateCode)
            DbCommandWrapper.AddInParameter("@PDIStatusCode", DbType.Int16, carrosserieHeader.PDIStatusCode)
            DbCommandWrapper.AddInParameter("@BUCode", DbType.AnsiString, carrosserieHeader.BUCode)
            DbCommandWrapper.AddInParameter("@BUName", DbType.AnsiString, carrosserieHeader.BUName)
            DbCommandWrapper.AddInParameter("@PDIName", DbType.AnsiString, carrosserieHeader.PDIName)
            DbCommandWrapper.AddInParameter("@PDIReceiptNo", DbType.AnsiString, carrosserieHeader.PDIReceiptNo)
            DbCommandWrapper.AddInParameter("@PDIReceiptRefName", DbType.AnsiString, carrosserieHeader.PDIReceiptRefName)
            DbCommandWrapper.AddInParameter("@PDIReceiptStatus", DbType.Int16, carrosserieHeader.PDIReceiptStatus)
            DbCommandWrapper.AddInParameter("@TransactionDate", DbType.DateTime, carrosserieHeader.TransactionDate)
            DbCommandWrapper.AddInParameter("@TransactionType", DbType.Int16, carrosserieHeader.TransactionType)
            DbCommandWrapper.AddInParameter("@VendorName", DbType.AnsiString, carrosserieHeader.VendorName)
            DbCommandWrapper.AddInParameter("@ChassisNumber", DbType.AnsiString, carrosserieHeader.ChassisNumber)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, carrosserieHeader.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, carrosserieHeader.LastUpdateBy)
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

            Dim carrosserieHeader As CarrosserieHeader = CType(obj, CarrosserieHeader)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, carrosserieHeader.ID)
            DbCommandWrapper.AddInParameter("@PDIStateCode", DbType.Int16, carrosserieHeader.PDIStateCode)
            DbCommandWrapper.AddInParameter("@PDIStatusCode", DbType.Int16, carrosserieHeader.PDIStatusCode)
            DbCommandWrapper.AddInParameter("@BUCode", DbType.AnsiString, carrosserieHeader.BUCode)
            DbCommandWrapper.AddInParameter("@BUName", DbType.AnsiString, carrosserieHeader.BUName)
            DbCommandWrapper.AddInParameter("@PDIName", DbType.AnsiString, carrosserieHeader.PDIName)
            DbCommandWrapper.AddInParameter("@PDIReceiptNo", DbType.AnsiString, carrosserieHeader.PDIReceiptNo)
            DbCommandWrapper.AddInParameter("@PDIReceiptRefName", DbType.AnsiString, carrosserieHeader.PDIReceiptRefName)
            DbCommandWrapper.AddInParameter("@PDIReceiptStatus", DbType.Int16, carrosserieHeader.PDIReceiptStatus)
            DbCommandWrapper.AddInParameter("@TransactionDate", DbType.DateTime, carrosserieHeader.TransactionDate)
            DbCommandWrapper.AddInParameter("@TransactionType", DbType.Int16, carrosserieHeader.TransactionType)
            DbCommandWrapper.AddInParameter("@VendorName", DbType.AnsiString, carrosserieHeader.VendorName)
            DbCommandWrapper.AddInParameter("@ChassisNumber", DbType.AnsiString, carrosserieHeader.ChassisNumber)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, carrosserieHeader.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, carrosserieHeader.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As CarrosserieHeader

            Dim carrosserieHeader As CarrosserieHeader = New CarrosserieHeader

            carrosserieHeader.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("PDIStateCode")) Then carrosserieHeader.PDIStateCode = CType(dr("PDIStateCode"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("PDIStatusCode")) Then carrosserieHeader.PDIStatusCode = CType(dr("PDIStatusCode"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("BUCode")) Then carrosserieHeader.BUCode = dr("BUCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("BUName")) Then carrosserieHeader.BUName = dr("BUName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PDIName")) Then carrosserieHeader.PDIName = dr("PDIName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PDIReceiptNo")) Then carrosserieHeader.PDIReceiptNo = dr("PDIReceiptNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PDIReceiptRefName")) Then carrosserieHeader.PDIReceiptRefName = dr("PDIReceiptRefName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PDIReceiptStatus")) Then carrosserieHeader.PDIReceiptStatus = CType(dr("PDIReceiptStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("TransactionDate")) Then carrosserieHeader.TransactionDate = CType(dr("TransactionDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("TransactionType")) Then carrosserieHeader.TransactionType = CType(dr("TransactionType"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("VendorName")) Then carrosserieHeader.VendorName = dr("VendorName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ChassisNumber")) Then carrosserieHeader.ChassisNumber = dr("ChassisNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then carrosserieHeader.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then carrosserieHeader.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then carrosserieHeader.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then carrosserieHeader.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then carrosserieHeader.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            Return carrosserieHeader

        End Function

        Private Sub SetTableName()

            If Not (GetType(CarrosserieHeader) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(CarrosserieHeader), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(CarrosserieHeader).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

