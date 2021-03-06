
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : BabitReportJVtoReceipt Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2019 
'// ---------------------
'// $History      : $
'// Generated on 02/10/2019 - 14:04:46
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

    Public Class BabitReportJVtoReceiptMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertBabitReportJVtoReceipt"
        Private m_UpdateStatement As String = "up_UpdateBabitReportJVtoReceipt"
        Private m_RetrieveStatement As String = "up_RetrieveBabitReportJVtoReceipt"
        Private m_RetrieveListStatement As String = "up_RetrieveBabitReportJVtoReceiptList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteBabitReportJVtoReceipt"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim babitReportJVtoReceipt As BabitReportJVtoReceipt = Nothing
            While dr.Read

                babitReportJVtoReceipt = Me.CreateObject(dr)

            End While

            Return babitReportJVtoReceipt

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim babitReportJVtoReceiptList As ArrayList = New ArrayList

            While dr.Read
                Dim babitReportJVtoReceipt As BabitReportJVtoReceipt = Me.CreateObject(dr)
                babitReportJVtoReceiptList.Add(babitReportJVtoReceipt)
            End While

            Return babitReportJVtoReceiptList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim babitReportJVtoReceipt As BabitReportJVtoReceipt = CType(obj, BabitReportJVtoReceipt)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, babitReportJVtoReceipt.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim babitReportJVtoReceipt As BabitReportJVtoReceipt = CType(obj, BabitReportJVtoReceipt)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, babitReportJVtoReceipt.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, babitReportJVtoReceipt.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@BabitReportJVID", DbType.Int32, Me.GetRefObject(babitReportJVtoReceipt.BabitReportJV))
            DbCommandWrapper.AddInParameter("@BabitReportReceiptID", DbType.Int32, Me.GetRefObject(babitReportJVtoReceipt.BabitReportReceipt))

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

            Dim babitReportJVtoReceipt As BabitReportJVtoReceipt = CType(obj, BabitReportJVtoReceipt)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, babitReportJVtoReceipt.ID)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, babitReportJVtoReceipt.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, babitReportJVtoReceipt.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@BabitReportJVID", DbType.Int32, Me.GetRefObject(babitReportJVtoReceipt.BabitReportJV))
            DbCommandWrapper.AddInParameter("@BabitReportReceiptID", DbType.Int32, Me.GetRefObject(babitReportJVtoReceipt.BabitReportReceipt))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As BabitReportJVtoReceipt

            Dim babitReportJVtoReceipt As BabitReportJVtoReceipt = New BabitReportJVtoReceipt

            babitReportJVtoReceipt.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then babitReportJVtoReceipt.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then babitReportJVtoReceipt.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then babitReportJVtoReceipt.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then babitReportJVtoReceipt.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then babitReportJVtoReceipt.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            If Not dr.IsDBNull(dr.GetOrdinal("BabitReportJVID")) Then
                babitReportJVtoReceipt.BabitReportJV = New BabitReportJV(CType(dr("BabitReportJVID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("BabitReportReceiptID")) Then
                babitReportJVtoReceipt.BabitReportReceipt = New BabitReportReceipt(CType(dr("BabitReportReceiptID"), Integer))
            End If

            Return babitReportJVtoReceipt

        End Function

        Private Sub SetTableName()

            If Not (GetType(BabitReportJVtoReceipt) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(BabitReportJVtoReceipt), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(BabitReportJVtoReceipt).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

