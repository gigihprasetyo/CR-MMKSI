
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : CcReportDealer Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2011 
'// ---------------------
'// $History      : $
'// Generated on 10/31/2011 - 3:40:16 PM
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

    Public Class CcReportDealerMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertCcReportDealer"
        Private m_UpdateStatement As String = "up_UpdateCcReportDealer"
        Private m_RetrieveStatement As String = "up_RetrieveCcReportDealer"
        Private m_RetrieveListStatement As String = "up_RetrieveCcReportDealerList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteCcReportDealer"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim ccReportDealer As CcReportDealer = Nothing
            While dr.Read

                ccReportDealer = Me.CreateObject(dr)

            End While

            Return ccReportDealer

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim ccReportDealerList As ArrayList = New ArrayList

            While dr.Read
                Dim ccReportDealer As CcReportDealer = Me.CreateObject(dr)
                ccReportDealerList.Add(ccReportDealer)
            End While

            Return ccReportDealerList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim ccReportDealer As CcReportDealer = CType(obj, CcReportDealer)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, ccReportDealer.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim ccReportDealer As CcReportDealer = CType(obj, CcReportDealer)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@PeriodFrom", DbType.DateTime, ccReportDealer.PeriodFrom)
            DbCommandWrapper.AddInParameter("@PeriodTo", DbType.DateTime, ccReportDealer.PeriodTo)
            DbCommandWrapper.AddInParameter("@PdfFileName", DbType.AnsiString, ccReportDealer.PdfFileName)
            DbCommandWrapper.AddInParameter("@ReportStatus", DbType.Int16, ccReportDealer.ReportStatus)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, ccReportDealer.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            'DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, ccReportDealer.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@CcCustomerCategoryID", DbType.Int16, Me.GetRefObject(ccReportDealer.CcCustomerCategory))
            DbCommandWrapper.AddInParameter("@CcVehicleCategoryID", DbType.Int16, Me.GetRefObject(ccReportDealer.CcVehicleCategory))
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, Me.GetRefObject(ccReportDealer.Dealer))
            DbCommandWrapper.AddInParameter("@CcReportMasterID", DbType.Int32, Me.GetRefObject(ccReportDealer.CcReportMaster))

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

            Dim ccReportDealer As CcReportDealer = CType(obj, CcReportDealer)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, ccReportDealer.ID)
            DbCommandWrapper.AddInParameter("@PeriodFrom", DbType.DateTime, ccReportDealer.PeriodFrom)
            DbCommandWrapper.AddInParameter("@PeriodTo", DbType.DateTime, ccReportDealer.PeriodTo)
            DbCommandWrapper.AddInParameter("@PdfFileName", DbType.AnsiString, ccReportDealer.PdfFileName)
            DbCommandWrapper.AddInParameter("@ReportStatus", DbType.Int16, ccReportDealer.ReportStatus)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, ccReportDealer.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, ccReportDealer.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@CcCustomerCategoryID", DbType.Int16, Me.GetRefObject(ccReportDealer.CcCustomerCategory))
            DbCommandWrapper.AddInParameter("@CcVehicleCategoryID", DbType.Int16, Me.GetRefObject(ccReportDealer.CcVehicleCategory))
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, Me.GetRefObject(ccReportDealer.Dealer))
            DbCommandWrapper.AddInParameter("@CcReportMasterID", DbType.Int32, Me.GetRefObject(ccReportDealer.CcReportMaster))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As CcReportDealer

            Dim ccReportDealer As CcReportDealer = New CcReportDealer

            ccReportDealer.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("PeriodFrom")) Then ccReportDealer.PeriodFrom = CType(dr("PeriodFrom"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("PeriodTo")) Then ccReportDealer.PeriodTo = CType(dr("PeriodTo"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("PdfFileName")) Then ccReportDealer.PdfFileName = dr("PdfFileName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ReportStatus")) Then ccReportDealer.ReportStatus = CType(dr("ReportStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then ccReportDealer.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then ccReportDealer.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then ccReportDealer.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then ccReportDealer.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then ccReportDealer.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("CcCustomerCategoryID")) Then
                ccReportDealer.CcCustomerCategory = New CcCustomerCategory(CType(dr("CcCustomerCategoryID"), Short))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("CcVehicleCategoryID")) Then
                ccReportDealer.CcVehicleCategory = New CcVehicleCategory(CType(dr("CcVehicleCategoryID"), Short))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then
                ccReportDealer.Dealer = New Dealer(CType(dr("DealerID"), Short))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("CcReportMasterID")) Then
                ccReportDealer.CcReportMaster = New CcReportMaster(CType(dr("CcReportMasterID"), Integer))
            End If

            Return ccReportDealer

        End Function

        Private Sub SetTableName()

            If Not (GetType(CcReportDealer) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(CcReportDealer), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(CcReportDealer).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

