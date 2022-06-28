
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : SPAFDoc_UploadDetail Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2010 
'// ---------------------
'// $History      : $
'// Generated on 5/25/2010 - 4:23:58 PM
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

    Public Class SPAFDoc_UploadDetailMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertSPAFDoc_UploadDetail"
        Private m_UpdateStatement As String = "up_UpdateSPAFDoc_UploadDetail"
        Private m_RetrieveStatement As String = "up_RetrieveSPAFDoc_UploadDetail"
        Private m_RetrieveListStatement As String = "up_RetrieveSPAFDoc_UploadDetailList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteSPAFDoc_UploadDetail"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim sPAFDoc_UploadDetail As SPAFDoc_UploadDetail = Nothing
            While dr.Read

                sPAFDoc_UploadDetail = Me.CreateObject(dr)

            End While

            Return sPAFDoc_UploadDetail

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim sPAFDoc_UploadDetailList As ArrayList = New ArrayList

            While dr.Read
                Dim sPAFDoc_UploadDetail As SPAFDoc_UploadDetail = Me.CreateObject(dr)
                sPAFDoc_UploadDetailList.Add(sPAFDoc_UploadDetail)
            End While

            Return sPAFDoc_UploadDetailList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim sPAFDoc_UploadDetail As SPAFDoc_UploadDetail = CType(obj, SPAFDoc_UploadDetail)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, sPAFDoc_UploadDetail.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim sPAFDoc_UploadDetail As SPAFDoc_UploadDetail = CType(obj, SPAFDoc_UploadDetail)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DBCommandWrapper.AddInParameter("@SPAFDoc_UploadHeaderID", DbType.Int32, Me.GetRefObject(sPAFDoc_UploadDetail.SPAFDoc_UploadHeader))
            DBCommandWrapper.AddInParameter("@SPAFFType", DbType.AnsiString, sPAFDoc_UploadDetail.SPAFFType)
            DBCommandWrapper.AddInParameter("@DealerID", DbType.Int16, Me.GetRefObject(sPAFDoc_UploadDetail.Dealer))
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, sPAFDoc_UploadDetail.Status)
            DbCommandWrapper.AddInParameter("@DocType", DbType.Int16, sPAFDoc_UploadDetail.DocType)
            DbCommandWrapper.AddInParameter("@OrderDealer", DbType.AnsiString, sPAFDoc_UploadDetail.OrderDealer)
            DBCommandWrapper.AddInParameter("@ChassisMasterID", DbType.Int32, Me.GetRefObject(sPAFDoc_UploadDetail.ChassisMaster))
            DbCommandWrapper.AddInParameter("@PostingDate", DbType.DateTime, sPAFDoc_UploadDetail.PostingDate)
            DbCommandWrapper.AddInParameter("@ReffLetter", DbType.AnsiString, sPAFDoc_UploadDetail.ReffLetter)
            DbCommandWrapper.AddInParameter("@DateLetter", DbType.DateTime, sPAFDoc_UploadDetail.DateLetter)
            DbCommandWrapper.AddInParameter("@CustomerName", DbType.AnsiString, sPAFDoc_UploadDetail.CustomerName)
            DbCommandWrapper.AddInParameter("@RetailPrice", DbType.Currency, sPAFDoc_UploadDetail.RetailPrice)
            DbCommandWrapper.AddInParameter("@SPAF", DbType.Currency, sPAFDoc_UploadDetail.SPAF)
            DbCommandWrapper.AddInParameter("@Subsidi", DbType.Currency, sPAFDoc_UploadDetail.Subsidi)
            DbCommandWrapper.AddInParameter("@TglSetuju", DbType.DateTime, sPAFDoc_UploadDetail.TglSetuju)
            DbCommandWrapper.AddInParameter("@UploadFile", DbType.AnsiString, sPAFDoc_UploadDetail.UploadFile)
            DbCommandWrapper.AddInParameter("@UploadBy", DbType.AnsiString, sPAFDoc_UploadDetail.UploadBy)
            DbCommandWrapper.AddInParameter("@UploadDate", DbType.DateTime, sPAFDoc_UploadDetail.UploadDate)
            DbCommandWrapper.AddInParameter("@AlasanPenolakan", DbType.AnsiString, sPAFDoc_UploadDetail.AlasanPenolakan)
            DbCommandWrapper.AddInParameter("@DealerLeasing", DbType.AnsiString, sPAFDoc_UploadDetail.DealerLeasing)
            DbCommandWrapper.AddInParameter("@SellingType", DbType.Int16, sPAFDoc_UploadDetail.SellingType)
            DbCommandWrapper.AddInParameter("@PPhPercent", DbType.Decimal, sPAFDoc_UploadDetail.PPhPercent)
            DBCommandWrapper.AddInParameter("@ErrorMessage", DbType.AnsiString, sPAFDoc_UploadDetail.ErrorMessage)
            DBCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, sPAFDoc_UploadDetail.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, sPAFDoc_UploadDetail.LastUpdateBy)
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

            Dim sPAFDoc_UploadDetail As SPAFDoc_UploadDetail = CType(obj, SPAFDoc_UploadDetail)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, sPAFDoc_UploadDetail.ID)
            DBCommandWrapper.AddInParameter("@SPAFDoc_UploadHeaderID", DbType.Int32, Me.GetRefObject(sPAFDoc_UploadDetail.SPAFDoc_UploadHeader))
            DBCommandWrapper.AddInParameter("@SPAFFType", DbType.AnsiString, sPAFDoc_UploadDetail.SPAFFType)
            DBCommandWrapper.AddInParameter("@DealerID", DbType.Int16, Me.GetRefObject(sPAFDoc_UploadDetail.Dealer))
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, sPAFDoc_UploadDetail.Status)
            DbCommandWrapper.AddInParameter("@DocType", DbType.Int16, sPAFDoc_UploadDetail.DocType)
            DbCommandWrapper.AddInParameter("@OrderDealer", DbType.AnsiString, sPAFDoc_UploadDetail.OrderDealer)
            DBCommandWrapper.AddInParameter("@ChassisMasterID", DbType.Int32, Me.GetRefObject(sPAFDoc_UploadDetail.ChassisMaster))
            DbCommandWrapper.AddInParameter("@PostingDate", DbType.DateTime, sPAFDoc_UploadDetail.PostingDate)
            DbCommandWrapper.AddInParameter("@ReffLetter", DbType.AnsiString, sPAFDoc_UploadDetail.ReffLetter)
            DbCommandWrapper.AddInParameter("@DateLetter", DbType.DateTime, sPAFDoc_UploadDetail.DateLetter)
            DbCommandWrapper.AddInParameter("@CustomerName", DbType.AnsiString, sPAFDoc_UploadDetail.CustomerName)
            DbCommandWrapper.AddInParameter("@RetailPrice", DbType.Currency, sPAFDoc_UploadDetail.RetailPrice)
            DbCommandWrapper.AddInParameter("@SPAF", DbType.Currency, sPAFDoc_UploadDetail.SPAF)
            DbCommandWrapper.AddInParameter("@Subsidi", DbType.Currency, sPAFDoc_UploadDetail.Subsidi)
            DbCommandWrapper.AddInParameter("@TglSetuju", DbType.DateTime, sPAFDoc_UploadDetail.TglSetuju)
            DbCommandWrapper.AddInParameter("@UploadFile", DbType.AnsiString, sPAFDoc_UploadDetail.UploadFile)
            DbCommandWrapper.AddInParameter("@UploadBy", DbType.AnsiString, sPAFDoc_UploadDetail.UploadBy)
            DbCommandWrapper.AddInParameter("@UploadDate", DbType.DateTime, sPAFDoc_UploadDetail.UploadDate)
            DbCommandWrapper.AddInParameter("@AlasanPenolakan", DbType.AnsiString, sPAFDoc_UploadDetail.AlasanPenolakan)
            DbCommandWrapper.AddInParameter("@DealerLeasing", DbType.AnsiString, sPAFDoc_UploadDetail.DealerLeasing)
            DbCommandWrapper.AddInParameter("@SellingType", DbType.Int16, sPAFDoc_UploadDetail.SellingType)
            DbCommandWrapper.AddInParameter("@PPhPercent", DbType.Decimal, sPAFDoc_UploadDetail.PPhPercent)
            DBCommandWrapper.AddInParameter("@ErrorMessage", DbType.AnsiString, sPAFDoc_UploadDetail.ErrorMessage)
            DBCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, sPAFDoc_UploadDetail.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, sPAFDoc_UploadDetail.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As SPAFDoc_UploadDetail

            Dim sPAFDoc_UploadDetail As SPAFDoc_UploadDetail = New SPAFDoc_UploadDetail

            sPAFDoc_UploadDetail.ID = CType(dr("ID"), Integer)
            'If Not dr.IsDBNull(dr.GetOrdinal("SPAFDoc_UploadHeaderID")) Then sPAFDoc_UploadDetail.SPAFDoc_UploadHeaderID = CType(dr("SPAFDoc_UploadHeaderID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("SPAFFType")) Then sPAFDoc_UploadDetail.SPAFFType = dr("SPAFFType").ToString
            'If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then sPAFDoc_UploadDetail.DealerID = CType(dr("DealerID"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then sPAFDoc_UploadDetail.Status = CType(dr("Status"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("DocType")) Then sPAFDoc_UploadDetail.DocType = CType(dr("DocType"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("OrderDealer")) Then sPAFDoc_UploadDetail.OrderDealer = dr("OrderDealer").ToString
            'If Not dr.IsDBNull(dr.GetOrdinal("ChassisMasterID")) Then sPAFDoc_UploadDetail.ChassisMasterID = CType(dr("ChassisMasterID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("PostingDate")) Then sPAFDoc_UploadDetail.PostingDate = CType(dr("PostingDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("ReffLetter")) Then sPAFDoc_UploadDetail.ReffLetter = dr("ReffLetter").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DateLetter")) Then sPAFDoc_UploadDetail.DateLetter = CType(dr("DateLetter"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("CustomerName")) Then sPAFDoc_UploadDetail.CustomerName = dr("CustomerName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RetailPrice")) Then sPAFDoc_UploadDetail.RetailPrice = CType(dr("RetailPrice"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("SPAF")) Then sPAFDoc_UploadDetail.SPAF = CType(dr("SPAF"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("Subsidi")) Then sPAFDoc_UploadDetail.Subsidi = CType(dr("Subsidi"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("TglSetuju")) Then sPAFDoc_UploadDetail.TglSetuju = CType(dr("TglSetuju"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("UploadFile")) Then sPAFDoc_UploadDetail.UploadFile = dr("UploadFile").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("UploadBy")) Then sPAFDoc_UploadDetail.UploadBy = dr("UploadBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("UploadDate")) Then sPAFDoc_UploadDetail.UploadDate = CType(dr("UploadDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("AlasanPenolakan")) Then sPAFDoc_UploadDetail.AlasanPenolakan = dr("AlasanPenolakan").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DealerLeasing")) Then sPAFDoc_UploadDetail.DealerLeasing = dr("DealerLeasing").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SellingType")) Then sPAFDoc_UploadDetail.SellingType = CType(dr("SellingType"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("PPhPercent")) Then sPAFDoc_UploadDetail.PPhPercent = CType(dr("PPhPercent"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("ErrorMessage")) Then sPAFDoc_UploadDetail.ErrorMessage = dr("ErrorMessage").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then sPAFDoc_UploadDetail.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then sPAFDoc_UploadDetail.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then sPAFDoc_UploadDetail.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then sPAFDoc_UploadDetail.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then sPAFDoc_UploadDetail.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            If Not dr.IsDBNull(dr.GetOrdinal("SPAFDoc_UploadHeaderID")) Then
                sPAFDoc_UploadDetail.SPAFDoc_UploadHeader = New SPAFDoc_UploadHeader(CType(dr("SPAFDoc_UploadHeaderID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then
                sPAFDoc_UploadDetail.Dealer = New Dealer(CType(dr("DealerID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("ChassisMasterID")) Then
                If CType(dr("ChassisMasterID"), Integer) < 1 Then
                    sPAFDoc_UploadDetail.ChassisMaster = New ChassisMaster
                Else
                    sPAFDoc_UploadDetail.ChassisMaster = New ChassisMaster(CType(dr("ChassisMasterID"), Integer))
                End If
            End If
            Return sPAFDoc_UploadDetail

        End Function

        Private Sub SetTableName()

            If Not (GetType(SPAFDoc_UploadDetail) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(SPAFDoc_UploadDetail), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(SPAFDoc_UploadDetail).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

