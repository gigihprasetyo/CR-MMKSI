
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : BabitReportJV Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2019 
'// ---------------------
'// $History      : $
'// Generated on 02/10/2019 - 14:04:27
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

    Public Class BabitReportJVMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertBabitReportJV"
        Private m_UpdateStatement As String = "up_UpdateBabitReportJV"
        Private m_RetrieveStatement As String = "up_RetrieveBabitReportJV"
        Private m_RetrieveListStatement As String = "up_RetrieveBabitReportJVList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteBabitReportJV"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim babitReportJV As BabitReportJV = Nothing
            While dr.Read

                babitReportJV = Me.CreateObject(dr)

            End While

            Return babitReportJV

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim babitReportJVList As ArrayList = New ArrayList

            While dr.Read
                Dim babitReportJV As BabitReportJV = Me.CreateObject(dr)
                babitReportJVList.Add(babitReportJV)
            End While

            Return babitReportJVList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim babitReportJV As BabitReportJV = CType(obj, BabitReportJV)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, babitReportJV.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim babitReportJV As BabitReportJV = CType(obj, BabitReportJV)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@RegNumber", DbType.AnsiString, babitReportJV.RegNumber)
            DbCommandWrapper.AddInParameter("@TextReceiptNo", DbType.AnsiString, babitReportJV.TextReceiptNo)
            DbCommandWrapper.AddInParameter("@TextRefNo", DbType.AnsiString, babitReportJV.TextRefNo)
            DbCommandWrapper.AddInParameter("@NoJV", DbType.AnsiString, babitReportJV.NoJV)
            DbCommandWrapper.AddInParameter("@IsTransfer", DbType.Byte, babitReportJV.IsTransfer)
            DbCommandWrapper.AddInParameter("@TglProses", DbType.DateTime, babitReportJV.TglProses)
            DbCommandWrapper.AddInParameter("@TglPencairan", DbType.DateTime, babitReportJV.TglPencairan)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, babitReportJV.Status)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, babitReportJV.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, babitReportJV.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, Me.GetRefObject(babitReportJV.Dealer))

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

            Dim babitReportJV As BabitReportJV = CType(obj, BabitReportJV)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, babitReportJV.ID)
            DbCommandWrapper.AddInParameter("@RegNumber", DbType.AnsiString, babitReportJV.RegNumber)
            DbCommandWrapper.AddInParameter("@TextReceiptNo", DbType.AnsiString, babitReportJV.TextReceiptNo)
            DbCommandWrapper.AddInParameter("@TextRefNo", DbType.AnsiString, babitReportJV.TextRefNo)
            DbCommandWrapper.AddInParameter("@NoJV", DbType.AnsiString, babitReportJV.NoJV)
            DbCommandWrapper.AddInParameter("@IsTransfer", DbType.Byte, babitReportJV.IsTransfer)
            DbCommandWrapper.AddInParameter("@TglProses", DbType.DateTime, babitReportJV.TglProses)
            DbCommandWrapper.AddInParameter("@TglPencairan", DbType.DateTime, babitReportJV.TglPencairan)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, babitReportJV.Status)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, babitReportJV.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, babitReportJV.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, Me.GetRefObject(babitReportJV.Dealer))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As BabitReportJV

            Dim babitReportJV As BabitReportJV = New BabitReportJV

            babitReportJV.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("RegNumber")) Then babitReportJV.RegNumber = dr("RegNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("TextReceiptNo")) Then babitReportJV.TextReceiptNo = dr("TextReceiptNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("TextRefNo")) Then babitReportJV.TextRefNo = dr("TextRefNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("NoJV")) Then babitReportJV.NoJV = dr("NoJV").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("IsTransfer")) Then babitReportJV.IsTransfer = CType(dr("IsTransfer"), Byte)
            If Not dr.IsDBNull(dr.GetOrdinal("TglProses")) Then babitReportJV.TglProses = CType(dr("TglProses"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("TglPencairan")) Then babitReportJV.TglPencairan = CType(dr("TglPencairan"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then babitReportJV.Status = CType(dr("Status"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then babitReportJV.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then babitReportJV.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then babitReportJV.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then babitReportJV.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then babitReportJV.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then
                babitReportJV.Dealer = New Dealer(CType(dr("DealerID"), Short))
            End If

            Return babitReportJV

        End Function

        Private Sub SetTableName()

            If Not (GetType(BabitReportJV) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(BabitReportJV), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(BabitReportJV).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

