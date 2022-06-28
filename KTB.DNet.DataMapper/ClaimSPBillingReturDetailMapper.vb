#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : ClaimSPBillingReturDetail Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2020 
'// ---------------------
'// $History      : $
'// Generated on 8/11/2020 - 9:58:49 AM
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

    Public Class ClaimSPBillingReturDetailMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertClaimSPBillingReturDetail"
        Private m_UpdateStatement As String = "up_UpdateClaimSPBillingReturDetail"
        Private m_RetrieveStatement As String = "up_RetrieveClaimSPBillingReturDetail"
        Private m_RetrieveListStatement As String = "up_RetrieveClaimSPBillingReturDetailList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteClaimSPBillingReturDetail"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim claimSPBillingReturDetail As ClaimSPBillingReturDetail = Nothing
            While dr.Read

                claimSPBillingReturDetail = Me.CreateObject(dr)

            End While

            Return claimSPBillingReturDetail

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim claimSPBillingReturDetailList As ArrayList = New ArrayList

            While dr.Read
                Dim claimSPBillingReturDetail As ClaimSPBillingReturDetail = Me.CreateObject(dr)
                claimSPBillingReturDetailList.Add(claimSPBillingReturDetail)
            End While

            Return claimSPBillingReturDetailList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim claimSPBillingReturDetail As ClaimSPBillingReturDetail = CType(obj, ClaimSPBillingReturDetail)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, claimSPBillingReturDetail.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim claimSPBillingReturDetail As ClaimSPBillingReturDetail = CType(obj, ClaimSPBillingReturDetail)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            'DbCommandWrapper.AddInParameter("@ClaimSPBillingReturID", DbType.Int32, claimSPBillingReturDetail.ClaimSPBillingReturID)
            'DbCommandWrapper.AddInParameter("@SparePartDODetailID", DbType.Int32, claimSPBillingReturDetail.SparePartDODetailID)
            DbCommandWrapper.AddInParameter("@Qty", DbType.Int32, claimSPBillingReturDetail.Qty)
            DbCommandWrapper.AddInParameter("@Amount", DbType.Currency, claimSPBillingReturDetail.Amount)
            DbCommandWrapper.AddInParameter("@Tax", DbType.Currency, claimSPBillingReturDetail.Tax)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, claimSPBillingReturDetail.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, claimSPBillingReturDetail.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@ClaimSPBillingReturID", DbType.Int32, Me.GetRefObject(claimSPBillingReturDetail.ClaimSPBillingRetur))
            DbCommandWrapper.AddInParameter("@SparePartDODetailID", DbType.Int32, Me.GetRefObject(claimSPBillingReturDetail.SparePartDODetail))



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

            Dim claimSPBillingReturDetail As ClaimSPBillingReturDetail = CType(obj, ClaimSPBillingReturDetail)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, claimSPBillingReturDetail.ID)
            'DbCommandWrapper.AddInParameter("@ClaimSPBillingReturID", DbType.Int32, claimSPBillingReturDetail.ClaimSPBillingReturID)
            'DbCommandWrapper.AddInParameter("@SparePartDODetailID", DbType.Int32, claimSPBillingReturDetail.SparePartDODetailID)
            DbCommandWrapper.AddInParameter("@Qty", DbType.Int32, claimSPBillingReturDetail.Qty)
            DbCommandWrapper.AddInParameter("@Amount", DbType.Currency, claimSPBillingReturDetail.Amount)
            DbCommandWrapper.AddInParameter("@Tax", DbType.Currency, claimSPBillingReturDetail.Tax)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, claimSPBillingReturDetail.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, claimSPBillingReturDetail.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@ClaimSPBillingReturID", DbType.Int32, Me.GetRefObject(claimSPBillingReturDetail.ClaimSPBillingRetur))
            DbCommandWrapper.AddInParameter("@SparePartDODetailID", DbType.Int32, Me.GetRefObject(claimSPBillingReturDetail.SparePartDODetail))



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As ClaimSPBillingReturDetail

            Dim claimSPBillingReturDetail As ClaimSPBillingReturDetail = New ClaimSPBillingReturDetail

            claimSPBillingReturDetail.ID = CType(dr("ID"), Integer)
            'If Not dr.IsDBNull(dr.GetOrdinal("ClaimSPBillingReturID")) Then claimSPBillingReturDetail.ClaimSPBillingReturID = CType(dr("ClaimSPBillingReturID"), Integer)
            'If Not dr.IsDBNull(dr.GetOrdinal("SparePartDODetailID")) Then claimSPBillingReturDetail.SparePartDODetailID = CType(dr("SparePartDODetailID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Qty")) Then claimSPBillingReturDetail.Qty = CType(dr("Qty"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Amount")) Then claimSPBillingReturDetail.Amount = CType(dr("Amount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("Tax")) Then claimSPBillingReturDetail.Tax = CType(dr("Tax"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then claimSPBillingReturDetail.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then claimSPBillingReturDetail.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then claimSPBillingReturDetail.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then claimSPBillingReturDetail.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then claimSPBillingReturDetail.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            If Not dr.IsDBNull(dr.GetOrdinal("ClaimSPBillingReturID")) Then
                claimSPBillingReturDetail.ClaimSPBillingRetur = New ClaimSPBillingRetur(CType(dr("ClaimSPBillingReturID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("SparePartDODetailID")) Then
                claimSPBillingReturDetail.SparePartDODetail = New SparePartDODetail(CType(dr("SparePartDODetailID"), Integer))
            End If

            Return claimSPBillingReturDetail

        End Function

        Private Sub SetTableName()

            If Not (GetType(ClaimSPBillingReturDetail) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(ClaimSPBillingReturDetail), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(ClaimSPBillingReturDetail).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace
