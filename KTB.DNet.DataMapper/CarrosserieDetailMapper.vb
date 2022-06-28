
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : Mitrais Team
'// PURPOSE       : CarrosserieDetail Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 21/03/2018 - 10:50:38
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

    Public Class CarrosserieDetailMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertCarrosserieDetail"
        Private m_UpdateStatement As String = "up_UpdateCarrosserieDetail"
        Private m_RetrieveStatement As String = "up_RetrieveCarrosserieDetail"
        Private m_RetrieveListStatement As String = "up_RetrieveCarrosserieDetailList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteCarrosserieDetail"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim carrosserieDetail As CarrosserieDetail = Nothing
            While dr.Read

                carrosserieDetail = Me.CreateObject(dr)

            End While

            Return carrosserieDetail

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim carrosserieDetailList As ArrayList = New ArrayList

            While dr.Read
                Dim carrosserieDetail As CarrosserieDetail = Me.CreateObject(dr)
                carrosserieDetailList.Add(carrosserieDetail)
            End While

            Return carrosserieDetailList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim carrosserieDetail As CarrosserieDetail = CType(obj, CarrosserieDetail)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, carrosserieDetail.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim carrosserieDetail As CarrosserieDetail = CType(obj, CarrosserieDetail)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@PDIStateCode", DbType.Int16, carrosserieDetail.PDIStateCode)
            DbCommandWrapper.AddInParameter("@PDIStatusCode", DbType.Int16, carrosserieDetail.PDIStatusCode)
            DbCommandWrapper.AddInParameter("@AccessorriesDescription", DbType.AnsiString, carrosserieDetail.AccessorriesDescription)
            DbCommandWrapper.AddInParameter("@AccessorriesName", DbType.AnsiString, carrosserieDetail.AccessorriesName)
            DbCommandWrapper.AddInParameter("@BUCode", DbType.AnsiString, carrosserieDetail.BUCode)
            DbCommandWrapper.AddInParameter("@BUName", DbType.AnsiString, carrosserieDetail.BUName)
            DbCommandWrapper.AddInParameter("@KITName", DbType.AnsiString, carrosserieDetail.KITName)
            DbCommandWrapper.AddInParameter("@PBUCode", DbType.AnsiString, carrosserieDetail.PBUCode)
            DbCommandWrapper.AddInParameter("@PBUName", DbType.AnsiString, carrosserieDetail.PBUName)
            DbCommandWrapper.AddInParameter("@PDIDetailName", DbType.AnsiString, carrosserieDetail.PDIDetailName)
            DbCommandWrapper.AddInParameter("@PDIReceiptDetailNo", DbType.AnsiString, carrosserieDetail.PDIReceiptDetailNo)
            DbCommandWrapper.AddInParameter("@PDIReceiptName", DbType.AnsiString, carrosserieDetail.PDIReceiptName)
            DbCommandWrapper.AddInParameter("@ReceiveQuantity", DbType.Double, carrosserieDetail.ReceiveQuantity)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, carrosserieDetail.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, carrosserieDetail.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@CarrosserieHeaderID", DbType.Int32, Me.GetRefObject(carrosserieDetail.CarrosserieHeader))

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

            Dim carrosserieDetail As CarrosserieDetail = CType(obj, CarrosserieDetail)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, carrosserieDetail.ID)
            DbCommandWrapper.AddInParameter("@PDIStateCode", DbType.Int16, carrosserieDetail.PDIStateCode)
            DbCommandWrapper.AddInParameter("@PDIStatusCode", DbType.Int16, carrosserieDetail.PDIStatusCode)
            DbCommandWrapper.AddInParameter("@AccessorriesDescription", DbType.AnsiString, carrosserieDetail.AccessorriesDescription)
            DbCommandWrapper.AddInParameter("@AccessorriesName", DbType.AnsiString, carrosserieDetail.AccessorriesName)
            DbCommandWrapper.AddInParameter("@BUCode", DbType.AnsiString, carrosserieDetail.BUCode)
            DbCommandWrapper.AddInParameter("@BUName", DbType.AnsiString, carrosserieDetail.BUName)
            DbCommandWrapper.AddInParameter("@KITName", DbType.AnsiString, carrosserieDetail.KITName)
            DbCommandWrapper.AddInParameter("@PBUCode", DbType.AnsiString, carrosserieDetail.PBUCode)
            DbCommandWrapper.AddInParameter("@PBUName", DbType.AnsiString, carrosserieDetail.PBUName)
            DbCommandWrapper.AddInParameter("@PDIDetailName", DbType.AnsiString, carrosserieDetail.PDIDetailName)
            DbCommandWrapper.AddInParameter("@PDIReceiptDetailNo", DbType.AnsiString, carrosserieDetail.PDIReceiptDetailNo)
            DbCommandWrapper.AddInParameter("@PDIReceiptName", DbType.AnsiString, carrosserieDetail.PDIReceiptName)
            DbCommandWrapper.AddInParameter("@ReceiveQuantity", DbType.Double, carrosserieDetail.ReceiveQuantity)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, carrosserieDetail.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, carrosserieDetail.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@CarrosserieHeaderID", DbType.Int32, Me.GetRefObject(carrosserieDetail.CarrosserieHeader))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As CarrosserieDetail

            Dim carrosserieDetail As CarrosserieDetail = New CarrosserieDetail

            carrosserieDetail.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("PDIStateCode")) Then carrosserieDetail.PDIStateCode = CType(dr("PDIStateCode"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("PDIStatusCode")) Then carrosserieDetail.PDIStatusCode = CType(dr("PDIStatusCode"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("AccessorriesDescription")) Then carrosserieDetail.AccessorriesDescription = dr("AccessorriesDescription").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("AccessorriesName")) Then carrosserieDetail.AccessorriesName = dr("AccessorriesName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("BUCode")) Then carrosserieDetail.BUCode = dr("BUCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("BUName")) Then carrosserieDetail.BUName = dr("BUName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("KITName")) Then carrosserieDetail.KITName = dr("KITName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PBUCode")) Then carrosserieDetail.PBUCode = dr("PBUCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PBUName")) Then carrosserieDetail.PBUName = dr("PBUName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PDIDetailName")) Then carrosserieDetail.PDIDetailName = dr("PDIDetailName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PDIReceiptDetailNo")) Then carrosserieDetail.PDIReceiptDetailNo = dr("PDIReceiptDetailNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PDIReceiptName")) Then carrosserieDetail.PDIReceiptName = dr("PDIReceiptName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ReceiveQuantity")) Then carrosserieDetail.ReceiveQuantity = CType(dr("ReceiveQuantity"), Double)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then carrosserieDetail.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then carrosserieDetail.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then carrosserieDetail.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then carrosserieDetail.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then carrosserieDetail.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("CarrosserieHeaderID")) Then
                carrosserieDetail.CarrosserieHeader = New CarrosserieHeader(CType(dr("CarrosserieHeaderID"), Integer))
            End If

            Return carrosserieDetail

        End Function

        Private Sub SetTableName()

            If Not (GetType(CarrosserieDetail) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(CarrosserieDetail), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(CarrosserieDetail).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

