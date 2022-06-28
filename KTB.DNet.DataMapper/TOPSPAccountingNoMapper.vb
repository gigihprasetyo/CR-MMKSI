#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : TOPSPAccountingNo Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2020 
'// ---------------------
'// $History      : $
'// Generated on 7/16/2020 - 8:57:24 AM
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

    Public Class TOPSPAccountingNoMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertTOPSPAccountingNo"
        Private m_UpdateStatement As String = "up_UpdateTOPSPAccountingNo"
        Private m_RetrieveStatement As String = "up_RetrieveTOPSPAccountingNo"
        Private m_RetrieveListStatement As String = "up_RetrieveTOPSPAccountingNoList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteTOPSPAccountingNo"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim tOPSPAccountingNo As TOPSPAccountingNo = Nothing
            While dr.Read

                tOPSPAccountingNo = Me.CreateObject(dr)

            End While

            Return tOPSPAccountingNo

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim tOPSPAccountingNoList As ArrayList = New ArrayList

            While dr.Read
                Dim tOPSPAccountingNo As TOPSPAccountingNo = Me.CreateObject(dr)
                tOPSPAccountingNoList.Add(tOPSPAccountingNo)
            End While

            Return tOPSPAccountingNoList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim tOPSPAccountingNo As TOPSPAccountingNo = CType(obj, TOPSPAccountingNo)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, tOPSPAccountingNo.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim tOPSPAccountingNo As TOPSPAccountingNo = CType(obj, TOPSPAccountingNo)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@KliringDate", DbType.DateTime, tOPSPAccountingNo.KliringDate)
            DbCommandWrapper.AddInParameter("@KliringAmount", DbType.Currency, tOPSPAccountingNo.KliringAmount)
            DbCommandWrapper.AddInParameter("@TRNo", DbType.AnsiString, tOPSPAccountingNo.TRNo)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, tOPSPAccountingNo.RowStatus)
            DbCommandWrapper.AddInParameter("@Createdby", DbType.AnsiString, User)
            DbCommandWrapper.AddInParameter("@Createdtime", DbType.DateTime, DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdatedby", DbType.AnsiString, tOPSPAccountingNo.LastUpdatedby)
            'DbCommandWrapper.AddInParameter("@LastUpdatedtime", DbType.DateTime, tOPSPAccountingNo.LastUpdatedtime)

            DbCommandWrapper.AddInParameter("@TOPSPTransferPaymentID", DbType.Int32, Me.GetRefObject(tOPSPAccountingNo.TOPSPTransferPayment))

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

            Dim tOPSPAccountingNo As TOPSPAccountingNo = CType(obj, TOPSPAccountingNo)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, tOPSPAccountingNo.ID)
            DbCommandWrapper.AddInParameter("@KliringDate", DbType.DateTime, tOPSPAccountingNo.KliringDate)
            DbCommandWrapper.AddInParameter("@KliringAmount", DbType.Currency, tOPSPAccountingNo.KliringAmount)
            DbCommandWrapper.AddInParameter("@TRNo", DbType.AnsiString, tOPSPAccountingNo.TRNo)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, tOPSPAccountingNo.RowStatus)
            DbCommandWrapper.AddInParameter("@Createdby", DbType.AnsiString, tOPSPAccountingNo.Createdby)
            'DbCommandWrapper.AddInParameter("@Createdtime", DbType.DateTime, tOPSPAccountingNo.Createdtime)
            DbCommandWrapper.AddInParameter("@LastUpdatedby", DbType.AnsiString, User)
            DbCommandWrapper.AddInParameter("@LastUpdatedtime", DbType.DateTime, DateTime.Now)


            DbCommandWrapper.AddInParameter("@TOPSPTransferPaymentID", DbType.Int32, Me.GetRefObject(tOPSPAccountingNo.TOPSPTransferPayment))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As TOPSPAccountingNo

            Dim tOPSPAccountingNo As TOPSPAccountingNo = New TOPSPAccountingNo

            tOPSPAccountingNo.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("KliringDate")) Then tOPSPAccountingNo.KliringDate = CType(dr("KliringDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("KliringAmount")) Then tOPSPAccountingNo.KliringAmount = CType(dr("KliringAmount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("TRNo")) Then tOPSPAccountingNo.TRNo = dr("TRNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then tOPSPAccountingNo.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("Createdby")) Then tOPSPAccountingNo.Createdby = dr("Createdby").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Createdtime")) Then tOPSPAccountingNo.Createdtime = CType(dr("Createdtime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdatedby")) Then tOPSPAccountingNo.LastUpdatedby = dr("LastUpdatedby").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdatedtime")) Then tOPSPAccountingNo.LastUpdatedtime = CType(dr("LastUpdatedtime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("TOPSPTransferPaymentID")) Then
                tOPSPAccountingNo.TOPSPTransferPayment = New TOPSPTransferPayment(CType(dr("TOPSPTransferPaymentID"), Integer))
            End If
            Return tOPSPAccountingNo

        End Function

        Private Sub SetTableName()

            If Not (GetType(TOPSPAccountingNo) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(TOPSPAccountingNo), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(TOPSPAccountingNo).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace
