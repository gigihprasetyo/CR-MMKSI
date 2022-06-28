#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : SalesmanTurnOverAnalisa Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2007 
'// ---------------------
'// $History      : $
'// Generated on 7/25/2007 - 9:14:59 AM
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

    Public Class SalesmanTurnOverAnalisaMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertSalesmanTurnOverAnalisa"
        Private m_UpdateStatement As String = "up_UpdateSalesmanTurnOverAnalisa"
        Private m_RetrieveStatement As String = "up_RetrieveSalesmanTurnOverAnalisa"
        Private m_RetrieveListStatement As String = "up_RetrieveSalesmanTurnOverAnalisaList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteSalesmanTurnOverAnalisa"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim salesmanTurnOverAnalisa As SalesmanTurnOverAnalisa = Nothing
            While dr.Read

                salesmanTurnOverAnalisa = Me.CreateObject(dr)

            End While

            Return salesmanTurnOverAnalisa

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim salesmanTurnOverAnalisaList As ArrayList = New ArrayList

            While dr.Read
                Dim salesmanTurnOverAnalisa As SalesmanTurnOverAnalisa = Me.CreateObject(dr)
                salesmanTurnOverAnalisaList.Add(salesmanTurnOverAnalisa)
            End While

            Return salesmanTurnOverAnalisaList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim salesmanTurnOverAnalisa As SalesmanTurnOverAnalisa = CType(obj, SalesmanTurnOverAnalisa)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, salesmanTurnOverAnalisa.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim salesmanTurnOverAnalisa As SalesmanTurnOverAnalisa = CType(obj, SalesmanTurnOverAnalisa)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@PeriodeStart", DbType.DateTime, salesmanTurnOverAnalisa.PeriodeStart)
            DbCommandWrapper.AddInParameter("@PeriodeEnd", DbType.DateTime, salesmanTurnOverAnalisa.PeriodeEnd)
            DbCommandWrapper.AddInParameter("@Analisa", DbType.AnsiString, salesmanTurnOverAnalisa.Analisa)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, salesmanTurnOverAnalisa.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, salesmanTurnOverAnalisa.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, Me.GetRefObject(salesmanTurnOverAnalisa.Dealer))

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

            Dim salesmanTurnOverAnalisa As SalesmanTurnOverAnalisa = CType(obj, SalesmanTurnOverAnalisa)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, salesmanTurnOverAnalisa.ID)
            DbCommandWrapper.AddInParameter("@PeriodeStart", DbType.DateTime, salesmanTurnOverAnalisa.PeriodeStart)
            DbCommandWrapper.AddInParameter("@PeriodeEnd", DbType.DateTime, salesmanTurnOverAnalisa.PeriodeEnd)
            DbCommandWrapper.AddInParameter("@Analisa", DbType.AnsiString, salesmanTurnOverAnalisa.Analisa)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, salesmanTurnOverAnalisa.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, salesmanTurnOverAnalisa.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, Me.GetRefObject(salesmanTurnOverAnalisa.Dealer))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As SalesmanTurnOverAnalisa

            Dim salesmanTurnOverAnalisa As SalesmanTurnOverAnalisa = New SalesmanTurnOverAnalisa

            salesmanTurnOverAnalisa.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("PeriodeStart")) Then salesmanTurnOverAnalisa.PeriodeStart = CType(dr("PeriodeStart"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("PeriodeEnd")) Then salesmanTurnOverAnalisa.PeriodeEnd = CType(dr("PeriodeEnd"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("Analisa")) Then salesmanTurnOverAnalisa.Analisa = dr("Analisa").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then salesmanTurnOverAnalisa.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then salesmanTurnOverAnalisa.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then salesmanTurnOverAnalisa.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then salesmanTurnOverAnalisa.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then salesmanTurnOverAnalisa.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then
                salesmanTurnOverAnalisa.Dealer = New Dealer(CType(dr("DealerID"), Integer))
            End If

            Return salesmanTurnOverAnalisa

        End Function

        Private Sub SetTableName()

            If Not (GetType(SalesmanTurnOverAnalisa) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(SalesmanTurnOverAnalisa), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(SalesmanTurnOverAnalisa).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

