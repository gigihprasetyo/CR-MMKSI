#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : V_LeasingDaftarDokumen Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2009 
'// ---------------------
'// $History      : $
'// Generated on 8/19/2009 - 11:55:53 AM
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

    Public Class V_LeasingDaftarDokumenMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertV_LeasingDaftarDokumen"
        Private m_UpdateStatement As String = "up_UpdateV_LeasingDaftarDokumen"
        Private m_RetrieveStatement As String = "up_RetrieveV_LeasingDaftarDokumen"
        Private m_RetrieveListStatement As String = "up_RetrieveV_LeasingDaftarDokumenList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteV_LeasingDaftarDokumen"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim v_LeasingDaftarDokumen As V_LeasingDaftarDokumen = Nothing
            While dr.Read

                v_LeasingDaftarDokumen = Me.CreateObject(dr)

            End While

            Return v_LeasingDaftarDokumen

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim v_LeasingDaftarDokumenList As ArrayList = New ArrayList

            While dr.Read
                Dim v_LeasingDaftarDokumen As V_LeasingDaftarDokumen = Me.CreateObject(dr)
                v_LeasingDaftarDokumenList.Add(v_LeasingDaftarDokumen)
            End While

            Return v_LeasingDaftarDokumenList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim v_LeasingDaftarDokumen As V_LeasingDaftarDokumen = CType(obj, V_LeasingDaftarDokumen)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, v_LeasingDaftarDokumen.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim v_LeasingDaftarDokumen As V_LeasingDaftarDokumen = CType(obj, V_LeasingDaftarDokumen)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, v_LeasingDaftarDokumen.DealerID)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, v_LeasingDaftarDokumen.Status)
            DbCommandWrapper.AddInParameter("@DocType", DbType.Int16, v_LeasingDaftarDokumen.DocType)
            DbCommandWrapper.AddInParameter("@OrderDealer", DbType.AnsiString, v_LeasingDaftarDokumen.OrderDealer)
            DbCommandWrapper.AddInParameter("@ChassisMasterID", DbType.Int32, v_LeasingDaftarDokumen.ChassisMasterID)
            DbCommandWrapper.AddInParameter("@PostingDate", DbType.DateTime, v_LeasingDaftarDokumen.PostingDate)
            DbCommandWrapper.AddInParameter("@ReffLetter", DbType.AnsiString, v_LeasingDaftarDokumen.ReffLetter)
            DbCommandWrapper.AddInParameter("@DateLetter", DbType.DateTime, v_LeasingDaftarDokumen.DateLetter)
            DbCommandWrapper.AddInParameter("@CustomerName", DbType.AnsiString, v_LeasingDaftarDokumen.CustomerName)
            DbCommandWrapper.AddInParameter("@RetailPrice", DbType.Currency, v_LeasingDaftarDokumen.RetailPrice)
            DbCommandWrapper.AddInParameter("@SPAF", DbType.Currency, v_LeasingDaftarDokumen.SPAF)
            DbCommandWrapper.AddInParameter("@Subsidi", DbType.Currency, v_LeasingDaftarDokumen.Subsidi)
            DbCommandWrapper.AddInParameter("@TglSetuju", DbType.DateTime, v_LeasingDaftarDokumen.TglSetuju)
            DbCommandWrapper.AddInParameter("@UploadFile", DbType.AnsiString, v_LeasingDaftarDokumen.UploadFile)
            DbCommandWrapper.AddInParameter("@UploadBy", DbType.AnsiString, v_LeasingDaftarDokumen.UploadBy)
            DbCommandWrapper.AddInParameter("@UploadDate", DbType.DateTime, v_LeasingDaftarDokumen.UploadDate)
            DbCommandWrapper.AddInParameter("@AlasanPenolakan", DbType.AnsiString, v_LeasingDaftarDokumen.AlasanPenolakan)
            DbCommandWrapper.AddInParameter("@DealerLeasing", DbType.AnsiString, v_LeasingDaftarDokumen.DealerLeasing)
            DbCommandWrapper.AddInParameter("@SellingType", DbType.Int16, v_LeasingDaftarDokumen.SellingType)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, v_LeasingDaftarDokumen.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, v_LeasingDaftarDokumen.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@PPh", DbType.Decimal, v_LeasingDaftarDokumen.PPh)
            DbCommandWrapper.AddInParameter("@AfterPPh", DbType.Decimal, v_LeasingDaftarDokumen.AfterPPh)
            DbCommandWrapper.AddInParameter("@PPn", DbType.Decimal, v_LeasingDaftarDokumen.PPn)


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

            Dim v_LeasingDaftarDokumen As V_LeasingDaftarDokumen = CType(obj, V_LeasingDaftarDokumen)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, v_LeasingDaftarDokumen.ID)
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, v_LeasingDaftarDokumen.DealerID)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, v_LeasingDaftarDokumen.Status)
            DbCommandWrapper.AddInParameter("@DocType", DbType.Int16, v_LeasingDaftarDokumen.DocType)
            DbCommandWrapper.AddInParameter("@OrderDealer", DbType.AnsiString, v_LeasingDaftarDokumen.OrderDealer)
            DbCommandWrapper.AddInParameter("@ChassisMasterID", DbType.Int32, v_LeasingDaftarDokumen.ChassisMasterID)
            DbCommandWrapper.AddInParameter("@PostingDate", DbType.DateTime, v_LeasingDaftarDokumen.PostingDate)
            DbCommandWrapper.AddInParameter("@ReffLetter", DbType.AnsiString, v_LeasingDaftarDokumen.ReffLetter)
            DbCommandWrapper.AddInParameter("@DateLetter", DbType.DateTime, v_LeasingDaftarDokumen.DateLetter)
            DbCommandWrapper.AddInParameter("@CustomerName", DbType.AnsiString, v_LeasingDaftarDokumen.CustomerName)
            DbCommandWrapper.AddInParameter("@RetailPrice", DbType.Currency, v_LeasingDaftarDokumen.RetailPrice)
            DbCommandWrapper.AddInParameter("@SPAF", DbType.Currency, v_LeasingDaftarDokumen.SPAF)
            DbCommandWrapper.AddInParameter("@Subsidi", DbType.Currency, v_LeasingDaftarDokumen.Subsidi)
            DbCommandWrapper.AddInParameter("@TglSetuju", DbType.DateTime, v_LeasingDaftarDokumen.TglSetuju)
            DbCommandWrapper.AddInParameter("@UploadFile", DbType.AnsiString, v_LeasingDaftarDokumen.UploadFile)
            DbCommandWrapper.AddInParameter("@UploadBy", DbType.AnsiString, v_LeasingDaftarDokumen.UploadBy)
            DbCommandWrapper.AddInParameter("@UploadDate", DbType.DateTime, v_LeasingDaftarDokumen.UploadDate)
            DbCommandWrapper.AddInParameter("@AlasanPenolakan", DbType.AnsiString, v_LeasingDaftarDokumen.AlasanPenolakan)
            DbCommandWrapper.AddInParameter("@DealerLeasing", DbType.AnsiString, v_LeasingDaftarDokumen.DealerLeasing)
            DbCommandWrapper.AddInParameter("@SellingType", DbType.Int16, v_LeasingDaftarDokumen.SellingType)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, v_LeasingDaftarDokumen.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, v_LeasingDaftarDokumen.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@PPh", DbType.Decimal, v_LeasingDaftarDokumen.PPh)
            DbCommandWrapper.AddInParameter("@AfterPPh", DbType.Decimal, v_LeasingDaftarDokumen.AfterPPh)
            DbCommandWrapper.AddInParameter("@PPn", DbType.Decimal, v_LeasingDaftarDokumen.PPn)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As V_LeasingDaftarDokumen

            Dim v_LeasingDaftarDokumen As V_LeasingDaftarDokumen = New V_LeasingDaftarDokumen

            v_LeasingDaftarDokumen.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then v_LeasingDaftarDokumen.DealerID = CType(dr("DealerID"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then v_LeasingDaftarDokumen.Status = CType(dr("Status"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("DocType")) Then v_LeasingDaftarDokumen.DocType = CType(dr("DocType"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("OrderDealer")) Then v_LeasingDaftarDokumen.OrderDealer = dr("OrderDealer").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ChassisMasterID")) Then v_LeasingDaftarDokumen.ChassisMasterID = CType(dr("ChassisMasterID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("PostingDate")) Then v_LeasingDaftarDokumen.PostingDate = CType(dr("PostingDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("ReffLetter")) Then v_LeasingDaftarDokumen.ReffLetter = dr("ReffLetter").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DateLetter")) Then v_LeasingDaftarDokumen.DateLetter = CType(dr("DateLetter"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("CustomerName")) Then v_LeasingDaftarDokumen.CustomerName = dr("CustomerName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RetailPrice")) Then v_LeasingDaftarDokumen.RetailPrice = CType(dr("RetailPrice"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("SPAF")) Then v_LeasingDaftarDokumen.SPAF = CType(dr("SPAF"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("Subsidi")) Then v_LeasingDaftarDokumen.Subsidi = CType(dr("Subsidi"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("TglSetuju")) Then v_LeasingDaftarDokumen.TglSetuju = CType(dr("TglSetuju"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("UploadFile")) Then v_LeasingDaftarDokumen.UploadFile = dr("UploadFile").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("UploadBy")) Then v_LeasingDaftarDokumen.UploadBy = dr("UploadBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("UploadDate")) Then v_LeasingDaftarDokumen.UploadDate = CType(dr("UploadDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("AlasanPenolakan")) Then v_LeasingDaftarDokumen.AlasanPenolakan = dr("AlasanPenolakan").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DealerLeasing")) Then v_LeasingDaftarDokumen.DealerLeasing = dr("DealerLeasing").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SellingType")) Then v_LeasingDaftarDokumen.SellingType = CType(dr("SellingType"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then v_LeasingDaftarDokumen.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then v_LeasingDaftarDokumen.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then v_LeasingDaftarDokumen.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then v_LeasingDaftarDokumen.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then v_LeasingDaftarDokumen.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("PPh")) Then v_LeasingDaftarDokumen.PPh = CType(dr("PPh"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("AfterPPh")) Then v_LeasingDaftarDokumen.AfterPPh = CType(dr("AfterPPh"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("PPn")) Then v_LeasingDaftarDokumen.PPn = CType(dr("PPn"), Decimal)

            Return v_LeasingDaftarDokumen

        End Function

        Private Sub SetTableName()

            If Not (GetType(V_LeasingDaftarDokumen) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(V_LeasingDaftarDokumen), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(V_LeasingDaftarDokumen).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

