
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : AnnualDepositAHeader Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2009 
'// ---------------------
'// $History      : $
'// Generated on 1/24/2009 - 10:14:00 AM
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

    Public Class AnnualDepositAHeaderMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertAnnualDepositAHeader"
        Private m_UpdateStatement As String = "up_UpdateAnnualDepositAHeader"
        Private m_RetrieveStatement As String = "up_RetrieveAnnualDepositAHeader"
        Private m_RetrieveListStatement As String = "up_RetrieveAnnualDepositAHeaderList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteAnnualDepositAHeader"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim annualDepositAHeader As AnnualDepositAHeader = Nothing
            While dr.Read

                annualDepositAHeader = Me.CreateObject(dr)

            End While

            Return annualDepositAHeader

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim annualDepositAHeaderList As ArrayList = New ArrayList

            While dr.Read
                Dim annualDepositAHeader As AnnualDepositAHeader = Me.CreateObject(dr)
                annualDepositAHeaderList.Add(annualDepositAHeader)
            End While

            Return annualDepositAHeaderList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim annualDepositAHeader As AnnualDepositAHeader = CType(obj, AnnualDepositAHeader)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, annualDepositAHeader.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim annualDepositAHeader As AnnualDepositAHeader = CType(obj, AnnualDepositAHeader)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@FromDate", DbType.DateTime, annualDepositAHeader.FromDate)
            DbCommandWrapper.AddInParameter("@ToDate", DbType.DateTime, annualDepositAHeader.ToDate)
            DbCommandWrapper.AddInParameter("@NettoAmount", DbType.Currency, annualDepositAHeader.NettoAmount)
            DbCommandWrapper.AddInParameter("@GrossAmount", DbType.Currency, annualDepositAHeader.GrossAmount)
            DBCommandWrapper.AddInParameter("@Status", DbType.Int16, annualDepositAHeader.Status)
            DBCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, annualDepositAHeader.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, annualDepositAHeader.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, Me.GetRefObject(annualDepositAHeader.Dealer))
            DbCommandWrapper.AddInParameter("@PRoductCategoryID", DbType.Int32, Me.GetRefObject(annualDepositAHeader.ProductCategory))

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

            Dim annualDepositAHeader As AnnualDepositAHeader = CType(obj, AnnualDepositAHeader)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, annualDepositAHeader.ID)
            DbCommandWrapper.AddInParameter("@FromDate", DbType.DateTime, annualDepositAHeader.FromDate)
            DbCommandWrapper.AddInParameter("@ToDate", DbType.DateTime, annualDepositAHeader.ToDate)
            DbCommandWrapper.AddInParameter("@NettoAmount", DbType.Currency, annualDepositAHeader.NettoAmount)
            DBCommandWrapper.AddInParameter("@GrossAmount", DbType.Currency, annualDepositAHeader.GrossAmount)
            DBCommandWrapper.AddInParameter("@Status", DbType.Int16, annualDepositAHeader.Status)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, annualDepositAHeader.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, annualDepositAHeader.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, Me.GetRefObject(annualDepositAHeader.Dealer))
            DbCommandWrapper.AddInParameter("@PRoductCategoryID", DbType.Int32, Me.GetRefObject(annualDepositAHeader.ProductCategory))
            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As AnnualDepositAHeader

            Dim annualDepositAHeader As AnnualDepositAHeader = New AnnualDepositAHeader

            annualDepositAHeader.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("FromDate")) Then annualDepositAHeader.FromDate = CType(dr("FromDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("ToDate")) Then annualDepositAHeader.ToDate = CType(dr("ToDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("NettoAmount")) Then annualDepositAHeader.NettoAmount = CType(dr("NettoAmount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("GrossAmount")) Then annualDepositAHeader.GrossAmount = CType(dr("GrossAmount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then annualDepositAHeader.Status = CType(dr("Status"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then annualDepositAHeader.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then annualDepositAHeader.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then annualDepositAHeader.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then annualDepositAHeader.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then annualDepositAHeader.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then
                annualDepositAHeader.Dealer = New Dealer(CType(dr("DealerID"), Integer))
            End If

            If Not dr.IsDBNull(dr.GetOrdinal("ProductCategoryID")) Then
                annualDepositAHeader.ProductCategory = New ProductCategory(CType(dr("ProductCategoryID"), Integer))
            End If

            Return annualDepositAHeader

        End Function

        Private Sub SetTableName()

            If Not (GetType(AnnualDepositAHeader) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(AnnualDepositAHeader), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(AnnualDepositAHeader).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

