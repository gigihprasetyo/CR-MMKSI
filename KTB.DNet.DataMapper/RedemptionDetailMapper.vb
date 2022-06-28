
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : RedemptionDetail Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2010 
'// ---------------------
'// $History      : $
'// Generated on 4/16/2010 - 9:39:09 AM
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

    Public Class RedemptionDetailMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertRedemptionDetail"
        Private m_UpdateStatement As String = "up_UpdateRedemptionDetail"
        Private m_RetrieveStatement As String = "up_RetrieveRedemptionDetail"
        Private m_RetrieveListStatement As String = "up_RetrieveRedemptionDetailList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteRedemptionDetail"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim redemptionDetail As RedemptionDetail = Nothing
            While dr.Read

                redemptionDetail = Me.CreateObject(dr)

            End While

            Return redemptionDetail

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim redemptionDetailList As ArrayList = New ArrayList

            While dr.Read
                Dim redemptionDetail As RedemptionDetail = Me.CreateObject(dr)
                redemptionDetailList.Add(redemptionDetail)
            End While

            Return redemptionDetailList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim redemptionDetail As RedemptionDetail = CType(obj, RedemptionDetail)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, redemptionDetail.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim redemptionDetail As RedemptionDetail = CType(obj, RedemptionDetail)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@RequestQty", DbType.Int32, redemptionDetail.RequestQty)
            DbCommandWrapper.AddInParameter("@RespondQty", DbType.Int32, redemptionDetail.RespondQty)
            DBCommandWrapper.AddInParameter("@IsManualAlloc", DbType.Int16, redemptionDetail.IsManualAlloc)
            DBCommandWrapper.AddInParameter("@IsInProcess", DbType.Int16, redemptionDetail.IsInProcess)
            DBCommandWrapper.AddInParameter("@OriRequestQty", DbType.Int32, redemptionDetail.OriRequestQty)
            DBCommandWrapper.AddInParameter("@OriRespondQty", DbType.Int32, redemptionDetail.OriRespondQty)
            DBCommandWrapper.AddInParameter("@Sequence", DbType.Int32, redemptionDetail.Sequence)
            DBCommandWrapper.AddInParameter("@Ceiling", DbType.Currency, redemptionDetail.Ceiling)
            DBCommandWrapper.AddInParameter("@Stock", DbType.Int32, redemptionDetail.Stock)
            DBCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, redemptionDetail.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, redemptionDetail.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DBCommandWrapper.AddInParameter("@RedemptionHeaderID", DbType.Int32, Me.GetRefObject(redemptionDetail.RedemptionHeader))
            DBCommandWrapper.AddInParameter("@DealerID", DbType.Int16, Me.GetRefObject(redemptionDetail.Dealer))
            DBCommandWrapper.AddInParameter("@TermOfPaymentID", DbType.Int32, Me.GetRefObject(redemptionDetail.TermOfPayment))

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

            Dim redemptionDetail As RedemptionDetail = CType(obj, RedemptionDetail)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, redemptionDetail.ID)
            DbCommandWrapper.AddInParameter("@RequestQty", DbType.Int32, redemptionDetail.RequestQty)
            DbCommandWrapper.AddInParameter("@RespondQty", DbType.Int32, redemptionDetail.RespondQty)
            DBCommandWrapper.AddInParameter("@IsManualAlloc", DbType.Int16, redemptionDetail.IsManualAlloc)
            DBCommandWrapper.AddInParameter("@IsInProcess", DbType.Int16, redemptionDetail.IsInProcess)
            DBCommandWrapper.AddInParameter("@OriRequestQty", DbType.Int32, redemptionDetail.OriRequestQty)
            DBCommandWrapper.AddInParameter("@OriRespondQty", DbType.Int32, redemptionDetail.OriRespondQty)
            DBCommandWrapper.AddInParameter("@Sequence", DbType.Int32, redemptionDetail.Sequence)
            DBCommandWrapper.AddInParameter("@Ceiling", DbType.Currency, redemptionDetail.Ceiling)
            DBCommandWrapper.AddInParameter("@Stock", DbType.Int32, redemptionDetail.Stock)
            DBCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, redemptionDetail.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, redemptionDetail.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DBCommandWrapper.AddInParameter("@RedemptionHeaderID", DbType.Int32, Me.GetRefObject(redemptionDetail.RedemptionHeader))
            DBCommandWrapper.AddInParameter("@DealerID", DbType.Int16, Me.GetRefObject(redemptionDetail.Dealer))
            DBCommandWrapper.AddInParameter("@TermOfPaymentID", DbType.Int32, Me.GetRefObject(redemptionDetail.TermOfPayment))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As RedemptionDetail

            Dim redemptionDetail As RedemptionDetail = New RedemptionDetail

            redemptionDetail.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("RequestQty")) Then redemptionDetail.RequestQty = CType(dr("RequestQty"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("RespondQty")) Then redemptionDetail.RespondQty = CType(dr("RespondQty"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("IsManualAlloc")) Then redemptionDetail.IsManualAlloc = CType(dr("IsManualAlloc"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("IsInProcess")) Then redemptionDetail.IsInProcess = CType(dr("IsInProcess"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("OriRequestQty")) Then redemptionDetail.OriRequestQty = CType(dr("OriRequestQty"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("OriRespondQty")) Then redemptionDetail.OriRespondQty = CType(dr("OriRespondQty"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Sequence")) Then redemptionDetail.Sequence = CType(dr("Sequence"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Ceiling")) Then redemptionDetail.Ceiling = CType(dr("Ceiling"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("Stock")) Then redemptionDetail.Stock = CType(dr("Stock"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then redemptionDetail.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then redemptionDetail.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then redemptionDetail.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then redemptionDetail.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then redemptionDetail.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("RedemptionHeaderID")) Then
                'redemptionDetail.RedemptionHeaderID = CType(dr("RedemptionHeaderID"), Integer)
                redemptionDetail.RedemptionHeader = New RedemptionHeader(CType(dr("RedemptionHeaderID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then
                'redemptionDetail.DealerID = CType(dr("DealerID"), Short)
                redemptionDetail.Dealer = New Dealer(CType(dr("DealerID"), Short))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("TermOfPaymentID")) Then
                'redemptionDetail.TermOfPaymentID = CType(dr("TermOfPaymentID"), Integer)
                redemptionDetail.TermOfPayment = New TermOfPayment(CType(dr("TermOfPaymentID"), Integer))
            End If

            Return redemptionDetail

        End Function

        Private Sub SetTableName()

            If Not (GetType(RedemptionDetail) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(RedemptionDetail), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(RedemptionDetail).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

