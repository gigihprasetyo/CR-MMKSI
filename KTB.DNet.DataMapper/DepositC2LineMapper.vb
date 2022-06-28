#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : DepositC2Line Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2005 
'// ---------------------
'// $History      : $
'// Generated on 25/11/2005 - 16:05:45
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

    Public Class DepositC2LineMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertDepositC2Line"
        Private m_UpdateStatement As String = "up_UpdateDepositC2Line"
        Private m_RetrieveStatement As String = "up_RetrieveDepositC2Line"
        Private m_RetrieveListStatement As String = "up_RetrieveDepositC2LineList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteDepositC2Line"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim depositC2Line As DepositC2Line = Nothing
            While dr.Read

                depositC2Line = Me.CreateObject(dr)

            End While

            Return depositC2Line

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim depositC2LineList As ArrayList = New ArrayList

            While dr.Read
                Dim depositC2Line As DepositC2Line = Me.CreateObject(dr)
                depositC2LineList.Add(depositC2Line)
            End While

            Return depositC2LineList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim depositC2Line As DepositC2Line = CType(obj, DepositC2Line)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, depositC2Line.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim depositC2Line As DepositC2Line = CType(obj, DepositC2Line)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@DocumentNo", DbType.AnsiString, depositC2Line.DocumentNo)
            DbCommandWrapper.AddInParameter("@DocumentDate", DbType.DateTime, depositC2Line.DocumentDate)
            DbCommandWrapper.AddInParameter("@DepositC2Amnt", DbType.Currency, depositC2Line.DepositC2Amnt)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, depositC2Line.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, depositC2Line.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@DepositC2ID", DbType.Int32, Me.GetRefObject(depositC2Line.DepositC2))

            DbCommandWrapper.AddInParameter("@BillingNumber", DbType.AnsiString, depositC2Line.BillingNumber)

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

            Dim depositC2Line As DepositC2Line = CType(obj, DepositC2Line)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, depositC2Line.ID)
            DbCommandWrapper.AddInParameter("@DocumentNo", DbType.AnsiString, depositC2Line.DocumentNo)
            DbCommandWrapper.AddInParameter("@DocumentDate", DbType.DateTime, depositC2Line.DocumentDate)
            DbCommandWrapper.AddInParameter("@DepositC2Amnt", DbType.Currency, depositC2Line.DepositC2Amnt)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, depositC2Line.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, depositC2Line.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@DepositC2ID", DbType.Int32, Me.GetRefObject(depositC2Line.DepositC2))

            DbCommandWrapper.AddInParameter("@BillingNumber", DbType.AnsiString, depositC2Line.BillingNumber)

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As DepositC2Line

            Dim depositC2Line As DepositC2Line = New DepositC2Line

            depositC2Line.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("DocumentNo")) Then depositC2Line.DocumentNo = dr("DocumentNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DocumentDate")) Then depositC2Line.DocumentDate = CType(dr("DocumentDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("DepositC2Amnt")) Then depositC2Line.DepositC2Amnt = CType(dr("DepositC2Amnt"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then depositC2Line.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then depositC2Line.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then depositC2Line.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then depositC2Line.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then depositC2Line.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("DepositC2ID")) Then
                depositC2Line.DepositC2 = New DepositC2(CType(dr("DepositC2ID"), Integer))
            End If

            If Not dr.IsDBNull(dr.GetOrdinal("BillingNumber")) Then depositC2Line.BillingNumber = dr("BillingNumber").ToString

            Return depositC2Line

        End Function

        Private Sub SetTableName()

            If Not (GetType(DepositC2Line) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(DepositC2Line), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(DepositC2Line).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

