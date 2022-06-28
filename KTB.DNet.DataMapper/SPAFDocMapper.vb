#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : SPAFDoc Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2009 
'// ---------------------
'// $History      : $
'// Generated on 10/5/2009 - 4:56:04 PM
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

    Public Class SPAFDocMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertSPAFDoc"
        Private m_UpdateStatement As String = "up_UpdateSPAFDoc"
        Private m_RetrieveStatement As String = "up_RetrieveSPAFDoc"
        Private m_RetrieveListStatement As String = "up_RetrieveSPAFDocList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteSPAFDoc"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim sPAFDoc As SPAFDoc = Nothing
            While dr.Read

                sPAFDoc = Me.CreateObject(dr)

            End While

            Return sPAFDoc

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim sPAFDocList As ArrayList = New ArrayList

            While dr.Read
                Dim sPAFDoc As SPAFDoc = Me.CreateObject(dr)
                sPAFDocList.Add(sPAFDoc)
            End While

            Return sPAFDocList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim sPAFDoc As SPAFDoc = CType(obj, SPAFDoc)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, sPAFDoc.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim sPAFDoc As SPAFDoc = CType(obj, SPAFDoc)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, sPAFDoc.Status)
            DbCommandWrapper.AddInParameter("@DocType", DbType.Int16, sPAFDoc.DocType)
            DbCommandWrapper.AddInParameter("@OrderDealer", DbType.AnsiString, sPAFDoc.OrderDealer)
            DbCommandWrapper.AddInParameter("@PostingDate", DbType.DateTime, sPAFDoc.PostingDate)
            DbCommandWrapper.AddInParameter("@ReffLetter", DbType.AnsiString, sPAFDoc.ReffLetter)
            DbCommandWrapper.AddInParameter("@DateLetter", DbType.DateTime, sPAFDoc.DateLetter)
            DbCommandWrapper.AddInParameter("@CustomerName", DbType.AnsiString, sPAFDoc.CustomerName)
            DbCommandWrapper.AddInParameter("@RetailPrice", DbType.Currency, sPAFDoc.RetailPrice)
            DbCommandWrapper.AddInParameter("@SPAF", DbType.Currency, sPAFDoc.SPAF)
            DbCommandWrapper.AddInParameter("@Subsidi", DbType.Currency, sPAFDoc.Subsidi)
            DbCommandWrapper.AddInParameter("@TglSetuju", DbType.DateTime, sPAFDoc.TglSetuju)
            DbCommandWrapper.AddInParameter("@UploadFile", DbType.AnsiString, sPAFDoc.UploadFile)
            DbCommandWrapper.AddInParameter("@UploadBy", DbType.AnsiString, sPAFDoc.UploadBy)
            DbCommandWrapper.AddInParameter("@UploadDate", DbType.DateTime, sPAFDoc.UploadDate)
            DbCommandWrapper.AddInParameter("@AlasanPenolakan", DbType.AnsiString, sPAFDoc.AlasanPenolakan)
            DbCommandWrapper.AddInParameter("@DealerLeasing", DbType.AnsiString, sPAFDoc.DealerLeasing)
            DbCommandWrapper.AddInParameter("@SellingType", DbType.Int16, sPAFDoc.SellingType)
            DbCommandWrapper.AddInParameter("@PPhPercent", DbType.Decimal, sPAFDoc.PPhPercent)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, sPAFDoc.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, sPAFDoc.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, Me.GetRefObject(sPAFDoc.Dealer))
            DbCommandWrapper.AddInParameter("@ChassisMasterID", DbType.Int32, Me.GetRefObject(sPAFDoc.ChassisMaster))

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

            Dim sPAFDoc As SPAFDoc = CType(obj, SPAFDoc)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, sPAFDoc.ID)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, sPAFDoc.Status)
            DbCommandWrapper.AddInParameter("@DocType", DbType.Int16, sPAFDoc.DocType)
            DbCommandWrapper.AddInParameter("@OrderDealer", DbType.AnsiString, sPAFDoc.OrderDealer)
            DbCommandWrapper.AddInParameter("@PostingDate", DbType.DateTime, sPAFDoc.PostingDate)
            DbCommandWrapper.AddInParameter("@ReffLetter", DbType.AnsiString, sPAFDoc.ReffLetter)
            DbCommandWrapper.AddInParameter("@DateLetter", DbType.DateTime, sPAFDoc.DateLetter)
            DbCommandWrapper.AddInParameter("@CustomerName", DbType.AnsiString, sPAFDoc.CustomerName)
            DbCommandWrapper.AddInParameter("@RetailPrice", DbType.Currency, sPAFDoc.RetailPrice)
            DbCommandWrapper.AddInParameter("@SPAF", DbType.Currency, sPAFDoc.SPAF)
            DbCommandWrapper.AddInParameter("@Subsidi", DbType.Currency, sPAFDoc.Subsidi)
            DbCommandWrapper.AddInParameter("@TglSetuju", DbType.DateTime, sPAFDoc.TglSetuju)
            DbCommandWrapper.AddInParameter("@UploadFile", DbType.AnsiString, sPAFDoc.UploadFile)
            DbCommandWrapper.AddInParameter("@UploadBy", DbType.AnsiString, sPAFDoc.UploadBy)
            DbCommandWrapper.AddInParameter("@UploadDate", DbType.DateTime, sPAFDoc.UploadDate)
            DbCommandWrapper.AddInParameter("@AlasanPenolakan", DbType.AnsiString, sPAFDoc.AlasanPenolakan)
            DbCommandWrapper.AddInParameter("@DealerLeasing", DbType.AnsiString, sPAFDoc.DealerLeasing)
            DbCommandWrapper.AddInParameter("@SellingType", DbType.Int16, sPAFDoc.SellingType)
            DbCommandWrapper.AddInParameter("@PPhPercent", DbType.Decimal, sPAFDoc.PPhPercent)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, sPAFDoc.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, sPAFDoc.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, Me.GetRefObject(sPAFDoc.Dealer))
            DbCommandWrapper.AddInParameter("@ChassisMasterID", DbType.Int32, Me.GetRefObject(sPAFDoc.ChassisMaster))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As SPAFDoc

            Dim sPAFDoc As SPAFDoc = New SPAFDoc

            sPAFDoc.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then sPAFDoc.Status = CType(dr("Status"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("DocType")) Then sPAFDoc.DocType = CType(dr("DocType"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("OrderDealer")) Then sPAFDoc.OrderDealer = dr("OrderDealer").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PostingDate")) Then sPAFDoc.PostingDate = CType(dr("PostingDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("ReffLetter")) Then sPAFDoc.ReffLetter = dr("ReffLetter").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DateLetter")) Then sPAFDoc.DateLetter = CType(dr("DateLetter"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("CustomerName")) Then sPAFDoc.CustomerName = dr("CustomerName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RetailPrice")) Then sPAFDoc.RetailPrice = CType(dr("RetailPrice"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("SPAF")) Then sPAFDoc.SPAF = CType(dr("SPAF"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("Subsidi")) Then sPAFDoc.Subsidi = CType(dr("Subsidi"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("TglSetuju")) Then sPAFDoc.TglSetuju = CType(dr("TglSetuju"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("UploadFile")) Then sPAFDoc.UploadFile = dr("UploadFile").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("UploadBy")) Then sPAFDoc.UploadBy = dr("UploadBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("UploadDate")) Then sPAFDoc.UploadDate = CType(dr("UploadDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("AlasanPenolakan")) Then sPAFDoc.AlasanPenolakan = dr("AlasanPenolakan").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DealerLeasing")) Then sPAFDoc.DealerLeasing = dr("DealerLeasing").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SellingType")) Then sPAFDoc.SellingType = CType(dr("SellingType"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("PPhPercent")) Then sPAFDoc.PPhPercent = CType(dr("PPhPercent"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then sPAFDoc.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then sPAFDoc.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then sPAFDoc.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then sPAFDoc.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then sPAFDoc.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then
                sPAFDoc.Dealer = New Dealer(CType(dr("DealerID"), Short))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("ChassisMasterID")) Then
                sPAFDoc.ChassisMaster = New ChassisMaster(CType(dr("ChassisMasterID"), Integer))
            End If

            Return sPAFDoc

        End Function

        Private Sub SetTableName()

            If Not (GetType(SPAFDoc) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(SPAFDoc), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(SPAFDoc).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

