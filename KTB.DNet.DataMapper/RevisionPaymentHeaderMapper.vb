
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : RevisionPaymentHeader Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 9/4/2018 - 9:10:58 AM
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

    Public Class RevisionPaymentHeaderMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertRevisionPaymentHeader"
        Private m_UpdateStatement As String = "up_UpdateRevisionPaymentHeader"
        Private m_RetrieveStatement As String = "up_RetrieveRevisionPaymentHeader"
        Private m_RetrieveListStatement As String = "up_RetrieveRevisionPaymentHeaderList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteRevisionPaymentHeader"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim revisionPaymentHeader As RevisionPaymentHeader = Nothing
            While dr.Read

                revisionPaymentHeader = Me.CreateObject(dr)

            End While

            Return revisionPaymentHeader

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim revisionPaymentHeaderList As ArrayList = New ArrayList

            While dr.Read
                Dim revisionPaymentHeader As RevisionPaymentHeader = Me.CreateObject(dr)
                revisionPaymentHeaderList.Add(revisionPaymentHeader)
            End While

            Return revisionPaymentHeaderList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim revisionPaymentHeader As RevisionPaymentHeader = CType(obj, RevisionPaymentHeader)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, revisionPaymentHeader.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim revisionPaymentHeader As RevisionPaymentHeader = CType(obj, RevisionPaymentHeader)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@PaymentType", DbType.AnsiString, revisionPaymentHeader.PaymentType)
            DbCommandWrapper.AddInParameter("@RegNumber", DbType.AnsiString, revisionPaymentHeader.RegNumber)
            DbCommandWrapper.AddInParameter("@RevisionPaymentDocID", DbType.Int32, revisionPaymentHeader.RevisionPaymentDocID)
            DbCommandWrapper.AddInParameter("@SlipNumber", DbType.AnsiString, revisionPaymentHeader.SlipNumber)
            DbCommandWrapper.AddInParameter("@TotalAmount", DbType.Currency, revisionPaymentHeader.TotalAmount)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, revisionPaymentHeader.Status)
            DbCommandWrapper.AddInParameter("@EvidencePath", DbType.AnsiString, revisionPaymentHeader.EvidencePath)
            DbCommandWrapper.AddInParameter("@ActualPaymentDate", DbType.DateTime, revisionPaymentHeader.ActualPaymentDate)
            DbCommandWrapper.AddInParameter("@ActualPaymentAmount", DbType.Currency, revisionPaymentHeader.ActualPaymentAmount)
            DbCommandWrapper.AddInParameter("@AccDocNumber", DbType.AnsiString, revisionPaymentHeader.AccDocNumber)
            DbCommandWrapper.AddInParameter("@GyroDate", DbType.DateTime, revisionPaymentHeader.GyroDate)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, revisionPaymentHeader.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, revisionPaymentHeader.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, Me.GetRefObject(revisionPaymentHeader.Dealer))

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

            Dim revisionPaymentHeader As RevisionPaymentHeader = CType(obj, RevisionPaymentHeader)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, revisionPaymentHeader.ID)
            DbCommandWrapper.AddInParameter("@PaymentType", DbType.AnsiString, revisionPaymentHeader.PaymentType)
            DbCommandWrapper.AddInParameter("@RegNumber", DbType.AnsiString, revisionPaymentHeader.RegNumber)
            DbCommandWrapper.AddInParameter("@RevisionPaymentDocID", DbType.Int32, revisionPaymentHeader.RevisionPaymentDocID)
            DbCommandWrapper.AddInParameter("@SlipNumber", DbType.AnsiString, revisionPaymentHeader.SlipNumber)
            DbCommandWrapper.AddInParameter("@TotalAmount", DbType.Currency, revisionPaymentHeader.TotalAmount)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, revisionPaymentHeader.Status)
            DbCommandWrapper.AddInParameter("@EvidencePath", DbType.AnsiString, revisionPaymentHeader.EvidencePath)
            DbCommandWrapper.AddInParameter("@ActualPaymentDate", DbType.DateTime, revisionPaymentHeader.ActualPaymentDate)
            DbCommandWrapper.AddInParameter("@ActualPaymentAmount", DbType.Currency, revisionPaymentHeader.ActualPaymentAmount)
            DbCommandWrapper.AddInParameter("@AccDocNumber", DbType.AnsiString, revisionPaymentHeader.AccDocNumber)
            DbCommandWrapper.AddInParameter("@GyroDate", DbType.DateTime, revisionPaymentHeader.GyroDate)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, revisionPaymentHeader.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, revisionPaymentHeader.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, Me.GetRefObject(revisionPaymentHeader.Dealer))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As RevisionPaymentHeader

            Dim revisionPaymentHeader As RevisionPaymentHeader = New RevisionPaymentHeader

            revisionPaymentHeader.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("PaymentType")) Then revisionPaymentHeader.PaymentType = dr("PaymentType").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RegNumber")) Then revisionPaymentHeader.RegNumber = dr("RegNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RevisionPaymentDocID")) Then revisionPaymentHeader.RevisionPaymentDocID = CType(dr("RevisionPaymentDocID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("SlipNumber")) Then revisionPaymentHeader.SlipNumber = dr("SlipNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("TotalAmount")) Then revisionPaymentHeader.TotalAmount = CType(dr("TotalAmount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then revisionPaymentHeader.Status = CType(dr("Status"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("EvidencePath")) Then revisionPaymentHeader.EvidencePath = dr("EvidencePath").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ActualPaymentDate")) Then revisionPaymentHeader.ActualPaymentDate = CType(dr("ActualPaymentDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("ActualPaymentAmount")) Then revisionPaymentHeader.ActualPaymentAmount = CType(dr("ActualPaymentAmount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("AccDocNumber")) Then revisionPaymentHeader.AccDocNumber = dr("AccDocNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("GyroDate")) Then revisionPaymentHeader.GyroDate = CType(dr("GyroDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then revisionPaymentHeader.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then revisionPaymentHeader.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then revisionPaymentHeader.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then revisionPaymentHeader.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then revisionPaymentHeader.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then
                revisionPaymentHeader.Dealer = New Dealer(CType(dr("DealerID"), Integer))
            End If

            Return revisionPaymentHeader

        End Function

        Private Sub SetTableName()

            If Not (GetType(RevisionPaymentHeader) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(RevisionPaymentHeader), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(RevisionPaymentHeader).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

