#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : ClaimSPBillingRetur Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2020 
'// ---------------------
'// $History      : $
'// Generated on 8/11/2020 - 9:17:27 AM
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

    Public Class ClaimSPBillingReturMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertClaimSPBillingRetur"
        Private m_UpdateStatement As String = "up_UpdateClaimSPBillingRetur"
        Private m_RetrieveStatement As String = "up_RetrieveClaimSPBillingRetur"
        Private m_RetrieveListStatement As String = "up_RetrieveClaimSPBillingReturList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteClaimSPBillingRetur"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim claimSPBillingRetur As ClaimSPBillingRetur = Nothing
            While dr.Read

                claimSPBillingRetur = Me.CreateObject(dr)

            End While

            Return claimSPBillingRetur

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim claimSPBillingReturList As ArrayList = New ArrayList

            While dr.Read
                Dim claimSPBillingRetur As ClaimSPBillingRetur = Me.CreateObject(dr)
                claimSPBillingReturList.Add(claimSPBillingRetur)
            End While

            Return claimSPBillingReturList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim claimSPBillingRetur As ClaimSPBillingRetur = CType(obj, ClaimSPBillingRetur)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, claimSPBillingRetur.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim claimSPBillingRetur As ClaimSPBillingRetur = CType(obj, ClaimSPBillingRetur)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            'DbCommandWrapper.AddInParameter("@ClaimHeaderID", DbType.Int32, claimSPBillingRetur.ClaimHeaderID)
            'DbCommandWrapper.AddInParameter("@SparePartBillingID", DbType.Int32, claimSPBillingRetur.SparePartBillingID)
            DbCommandWrapper.AddInParameter("@BillingReturNumber", DbType.AnsiString, claimSPBillingRetur.BillingReturNumber)
            DbCommandWrapper.AddInParameter("@BillingReturDate", DbType.DateTime, claimSPBillingRetur.BillingReturDate)
            DbCommandWrapper.AddInParameter("@SORetur", DbType.AnsiString, claimSPBillingRetur.SORetur)
            DbCommandWrapper.AddInParameter("@SOReturDate", DbType.DateTime, claimSPBillingRetur.SOReturDate)
            DbCommandWrapper.AddInParameter("@DORetur", DbType.AnsiString, claimSPBillingRetur.DORetur)
            DbCommandWrapper.AddInParameter("@DOReturDate", DbType.DateTime, claimSPBillingRetur.DOReturDate)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, claimSPBillingRetur.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, claimSPBillingRetur.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@ClaimHeaderID", DbType.Int32, Me.GetRefObject(claimSPBillingRetur.ClaimHeader))
            DbCommandWrapper.AddInParameter("@SparePartBillingID", DbType.Int32, Me.GetRefObject(claimSPBillingRetur.SparePartBilling))



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

            Dim claimSPBillingRetur As ClaimSPBillingRetur = CType(obj, ClaimSPBillingRetur)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, claimSPBillingRetur.ID)
            'DbCommandWrapper.AddInParameter("@ClaimHeaderID", DbType.Int32, claimSPBillingRetur.ClaimHeaderID)
            'DbCommandWrapper.AddInParameter("@SparePartBillingID", DbType.Int32, claimSPBillingRetur.SparePartBillingID)
            DbCommandWrapper.AddInParameter("@BillingReturNumber", DbType.AnsiString, claimSPBillingRetur.BillingReturNumber)
            DbCommandWrapper.AddInParameter("@BillingReturDate", DbType.DateTime, claimSPBillingRetur.BillingReturDate)
            DbCommandWrapper.AddInParameter("@SORetur", DbType.AnsiString, claimSPBillingRetur.SORetur)
            DbCommandWrapper.AddInParameter("@SOReturDate", DbType.DateTime, claimSPBillingRetur.SOReturDate)
            DbCommandWrapper.AddInParameter("@DORetur", DbType.AnsiString, claimSPBillingRetur.DORetur)
            DbCommandWrapper.AddInParameter("@DOReturDate", DbType.DateTime, claimSPBillingRetur.DOReturDate)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, claimSPBillingRetur.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, claimSPBillingRetur.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@ClaimHeaderID", DbType.Int32, Me.GetRefObject(claimSPBillingRetur.ClaimHeader))
            DbCommandWrapper.AddInParameter("@SparePartBillingID", DbType.Int32, Me.GetRefObject(claimSPBillingRetur.SparePartBilling))



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As ClaimSPBillingRetur

            Dim claimSPBillingRetur As ClaimSPBillingRetur = New ClaimSPBillingRetur

            claimSPBillingRetur.ID = CType(dr("ID"), Integer)
            'If Not dr.IsDBNull(dr.GetOrdinal("ClaimHeaderID")) Then claimSPBillingRetur.ClaimHeaderID = CType(dr("ClaimHeaderID"), Integer)
            'If Not dr.IsDBNull(dr.GetOrdinal("SparePartBillingID")) Then claimSPBillingRetur.SparePartBillingID = CType(dr("SparePartBillingID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("BillingReturNumber")) Then claimSPBillingRetur.BillingReturNumber = dr("BillingReturNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("BillingReturDate")) Then claimSPBillingRetur.BillingReturDate = CType(dr("BillingReturDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("SORetur")) Then claimSPBillingRetur.SORetur = dr("SORetur").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SOReturDate")) Then claimSPBillingRetur.SOReturDate = CType(dr("SOReturDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("DORetur")) Then claimSPBillingRetur.DORetur = dr("DORetur").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DOReturDate")) Then claimSPBillingRetur.DOReturDate = CType(dr("DOReturDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then claimSPBillingRetur.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then claimSPBillingRetur.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then claimSPBillingRetur.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then claimSPBillingRetur.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then claimSPBillingRetur.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            If Not dr.IsDBNull(dr.GetOrdinal("ClaimHeaderID")) Then
                claimSPBillingRetur.ClaimHeader = New ClaimHeader(CType(dr("ClaimHeaderID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("SparePartBillingID")) Then
                claimSPBillingRetur.SparePartBilling = New SparePartBilling(CType(dr("SparePartBillingID"), Integer))
            End If

            Return claimSPBillingRetur

        End Function

        Private Sub SetTableName()

            If Not (GetType(ClaimSPBillingRetur) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(ClaimSPBillingRetur), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(ClaimSPBillingRetur).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace
