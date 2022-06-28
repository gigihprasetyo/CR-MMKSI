
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : BabitEventReportJV Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2019 
'// ---------------------
'// $History      : $
'// Generated on 03/10/2019 - 13:54:28
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

    Public Class BabitEventReportJVMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertBabitEventReportJV"
        Private m_UpdateStatement As String = "up_UpdateBabitEventReportJV"
        Private m_RetrieveStatement As String = "up_RetrieveBabitEventReportJV"
        Private m_RetrieveListStatement As String = "up_RetrieveBabitEventReportJVList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteBabitEventReportJV"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim babitEventReportJV As BabitEventReportJV = Nothing
            While dr.Read

                babitEventReportJV = Me.CreateObject(dr)

            End While

            Return babitEventReportJV

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim babitEventReportJVList As ArrayList = New ArrayList

            While dr.Read
                Dim babitEventReportJV As BabitEventReportJV = Me.CreateObject(dr)
                babitEventReportJVList.Add(babitEventReportJV)
            End While

            Return babitEventReportJVList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim babitEventReportJV As BabitEventReportJV = CType(obj, BabitEventReportJV)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, babitEventReportJV.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim babitEventReportJV As BabitEventReportJV = CType(obj, BabitEventReportJV)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@RegNumber", DbType.AnsiString, babitEventReportJV.RegNumber)
            DbCommandWrapper.AddInParameter("@TextReceiptNo", DbType.AnsiString, babitEventReportJV.TextReceiptNo)
            DbCommandWrapper.AddInParameter("@TextRefNo", DbType.AnsiString, babitEventReportJV.TextRefNo)
            DbCommandWrapper.AddInParameter("@NoJV", DbType.AnsiString, babitEventReportJV.NoJV)
            DbCommandWrapper.AddInParameter("@IsTransfer", DbType.Byte, babitEventReportJV.IsTransfer)
            DbCommandWrapper.AddInParameter("@TglProses", DbType.DateTime, babitEventReportJV.TglProses)
            DbCommandWrapper.AddInParameter("@TglPencairan", DbType.DateTime, babitEventReportJV.TglPencairan)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, babitEventReportJV.Status)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, babitEventReportJV.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, babitEventReportJV.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, Me.GetRefObject(babitEventReportJV.Dealer))

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

            Dim babitEventReportJV As BabitEventReportJV = CType(obj, BabitEventReportJV)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, babitEventReportJV.ID)
            DbCommandWrapper.AddInParameter("@RegNumber", DbType.AnsiString, babitEventReportJV.RegNumber)
            DbCommandWrapper.AddInParameter("@TextReceiptNo", DbType.AnsiString, babitEventReportJV.TextReceiptNo)
            DbCommandWrapper.AddInParameter("@TextRefNo", DbType.AnsiString, babitEventReportJV.TextRefNo)
            DbCommandWrapper.AddInParameter("@NoJV", DbType.AnsiString, babitEventReportJV.NoJV)
            DbCommandWrapper.AddInParameter("@IsTransfer", DbType.Byte, babitEventReportJV.IsTransfer)
            DbCommandWrapper.AddInParameter("@TglProses", DbType.DateTime, babitEventReportJV.TglProses)
            DbCommandWrapper.AddInParameter("@TglPencairan", DbType.DateTime, babitEventReportJV.TglPencairan)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, babitEventReportJV.Status)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, babitEventReportJV.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, babitEventReportJV.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, Me.GetRefObject(babitEventReportJV.Dealer))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As BabitEventReportJV

            Dim babitEventReportJV As BabitEventReportJV = New BabitEventReportJV

            babitEventReportJV.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("RegNumber")) Then babitEventReportJV.RegNumber = dr("RegNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("TextReceiptNo")) Then babitEventReportJV.TextReceiptNo = dr("TextReceiptNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("TextRefNo")) Then babitEventReportJV.TextRefNo = dr("TextRefNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("NoJV")) Then babitEventReportJV.NoJV = dr("NoJV").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("IsTransfer")) Then babitEventReportJV.IsTransfer = CType(dr("IsTransfer"), Byte)
            If Not dr.IsDBNull(dr.GetOrdinal("TglProses")) Then babitEventReportJV.TglProses = CType(dr("TglProses"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("TglPencairan")) Then babitEventReportJV.TglPencairan = CType(dr("TglPencairan"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then babitEventReportJV.Status = CType(dr("Status"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then babitEventReportJV.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then babitEventReportJV.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then babitEventReportJV.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then babitEventReportJV.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then babitEventReportJV.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then
                babitEventReportJV.Dealer = New Dealer(CType(dr("DealerID"), Short))
            End If

            Return babitEventReportJV

        End Function

        Private Sub SetTableName()

            If Not (GetType(BabitEventReportJV) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(BabitEventReportJV), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(BabitEventReportJV).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

