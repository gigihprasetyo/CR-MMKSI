
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : CessieDetail Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2010 
'// ---------------------
'// $History      : $
'// Generated on 10/8/2010 - 10:41:46 AM
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

    Public Class CessieDetailMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertCessieDetail"
        Private m_UpdateStatement As String = "up_UpdateCessieDetail"
        Private m_RetrieveStatement As String = "up_RetrieveCessieDetail"
        Private m_RetrieveListStatement As String = "up_RetrieveCessieDetailList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteCessieDetail"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim cessieDetail As CessieDetail = Nothing
            While dr.Read

                cessieDetail = Me.CreateObject(dr)

            End While

            Return cessieDetail

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim cessieDetailList As ArrayList = New ArrayList

            While dr.Read
                Dim cessieDetail As CessieDetail = Me.CreateObject(dr)
                cessieDetailList.Add(cessieDetail)
            End While

            Return cessieDetailList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim cessieDetail As CessieDetail = CType(obj, CessieDetail)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, cessieDetail.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim cessieDetail As CessieDetail = CType(obj, CessieDetail)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@BankName", DbType.AnsiString, cessieDetail.BankName)
            DBCommandWrapper.AddInParameter("@RefNumber", DbType.AnsiString, cessieDetail.RefNumber)
            DBCommandWrapper.AddInParameter("@Amount", DbType.Currency, cessieDetail.Amount)
            DBCommandWrapper.AddInParameter("@TransferDate", DbType.DateTime, cessieDetail.TransferDate)
            DBCommandWrapper.AddInParameter("@RegNumber", DbType.AnsiString, cessieDetail.RegNumber)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, cessieDetail.RowStatus)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, cessieDetail.LastUpdateBy)

            'DbCommandWrapper.AddInParameter("@CessieID", DbType.Int32, cessieDetail.CessieID)
            DBCommandWrapper.AddInParameter("@CessieID", DbType.Int32, Me.GetRefObject(cessieDetail.Cessie))
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

            Dim cessieDetail As CessieDetail = CType(obj, CessieDetail)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, cessieDetail.ID)
            DbCommandWrapper.AddInParameter("@BankName", DbType.AnsiString, cessieDetail.BankName)
            DBCommandWrapper.AddInParameter("@RefNumber", DbType.AnsiString, cessieDetail.RefNumber)
            DBCommandWrapper.AddInParameter("@Amount", DbType.Currency, cessieDetail.Amount)
            DBCommandWrapper.AddInParameter("@TransferDate", DbType.DateTime, cessieDetail.TransferDate)
            DBCommandWrapper.AddInParameter("@RegNumber", DbType.AnsiString, cessieDetail.RegNumber)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, cessieDetail.RowStatus)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, cessieDetail.CreatedBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)


            'DbCommandWrapper.AddInParameter("@CessieID", DbType.Int32, cessieDetail.CessieID)
            DBCommandWrapper.AddInParameter("@CessieID", DbType.Int32, Me.GetRefObject(cessieDetail.Cessie))
            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As CessieDetail

            Dim cessieDetail As CessieDetail = New CessieDetail

            cessieDetail.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("BankName")) Then cessieDetail.BankName = dr("BankName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RefNumber")) Then cessieDetail.RefNumber = dr("RefNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Amount")) Then cessieDetail.Amount = CType(dr("Amount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("TransferDate")) Then cessieDetail.TransferDate = CType(dr("TransferDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("RegNumber")) Then cessieDetail.RegNumber = dr("RegNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then cessieDetail.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then cessieDetail.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then cessieDetail.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then cessieDetail.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then cessieDetail.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CessieID")) Then
                cessieDetail.Cessie = New Cessie(CType(dr("CessieID"), Integer))
            End If

            Return cessieDetail
        End Function

        Private Sub SetTableName()

            If Not (GetType(CessieDetail) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(CessieDetail), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(CessieDetail).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

