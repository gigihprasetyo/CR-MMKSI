
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : BabitIklanDetail Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2019 
'// ---------------------
'// $History      : $
'// Generated on 08/05/2019 - 8:26:55
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

    Public Class BabitIklanDetailMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertBabitIklanDetail"
        Private m_UpdateStatement As String = "up_UpdateBabitIklanDetail"
        Private m_RetrieveStatement As String = "up_RetrieveBabitIklanDetail"
        Private m_RetrieveListStatement As String = "up_RetrieveBabitIklanDetailList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteBabitIklanDetail"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim babitIklanDetail As BabitIklanDetail = Nothing
            While dr.Read

                babitIklanDetail = Me.CreateObject(dr)

            End While

            Return babitIklanDetail

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim babitIklanDetailList As ArrayList = New ArrayList

            While dr.Read
                Dim babitIklanDetail As BabitIklanDetail = Me.CreateObject(dr)
                babitIklanDetailList.Add(babitIklanDetail)
            End While

            Return babitIklanDetailList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim babitIklanDetail As BabitIklanDetail = CType(obj, BabitIklanDetail)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, babitIklanDetail.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim babitIklanDetail As BabitIklanDetail = CType(obj, BabitIklanDetail)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@MediaName", DbType.AnsiString, babitIklanDetail.MediaName)
            DbCommandWrapper.AddInParameter("@Size", DbType.AnsiString, babitIklanDetail.Size)
            DbCommandWrapper.AddInParameter("@ViewNumber", DbType.Int32, babitIklanDetail.ViewNumber)
            DbCommandWrapper.AddInParameter("@SubmissionAmount", DbType.Currency, babitIklanDetail.SubmissionAmount)
            DbCommandWrapper.AddInParameter("@PeriodIklanStart", DbType.DateTime, babitIklanDetail.PeriodIklanStart)
            DbCommandWrapper.AddInParameter("@PeriodIklanEnd", DbType.DateTime, babitIklanDetail.PeriodIklanEnd)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, babitIklanDetail.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, babitIklanDetail.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@BabitHeaderID", DbType.Int32, Me.GetRefObject(babitIklanDetail.BabitHeader))
            DbCommandWrapper.AddInParameter("@BabitParameterDetailID", DbType.Int32, Me.GetRefObject(babitIklanDetail.BabitParameterDetail))
            DbCommandWrapper.AddInParameter("@BabitParameterHeaderID", DbType.Int32, Me.GetRefObject(babitIklanDetail.BabitParameterHeader))
            DbCommandWrapper.AddInParameter("@CategoryID", DbType.Int32, Me.GetRefObject(babitIklanDetail.Category))
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

            Dim babitIklanDetail As BabitIklanDetail = CType(obj, BabitIklanDetail)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, babitIklanDetail.ID)
            DbCommandWrapper.AddInParameter("@MediaName", DbType.AnsiString, babitIklanDetail.MediaName)
            DbCommandWrapper.AddInParameter("@Size", DbType.AnsiString, babitIklanDetail.Size)
            DbCommandWrapper.AddInParameter("@ViewNumber", DbType.Int32, babitIklanDetail.ViewNumber)
            DbCommandWrapper.AddInParameter("@SubmissionAmount", DbType.Currency, babitIklanDetail.SubmissionAmount)
            DbCommandWrapper.AddInParameter("@PeriodIklanStart", DbType.DateTime, babitIklanDetail.PeriodIklanStart)
            DbCommandWrapper.AddInParameter("@PeriodIklanEnd", DbType.DateTime, babitIklanDetail.PeriodIklanEnd)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, babitIklanDetail.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, babitIklanDetail.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@BabitHeaderID", DbType.Int32, Me.GetRefObject(babitIklanDetail.BabitHeader))
            DbCommandWrapper.AddInParameter("@BabitParameterDetailID", DbType.Int32, Me.GetRefObject(babitIklanDetail.BabitParameterDetail))
            DbCommandWrapper.AddInParameter("@BabitParameterHeaderID", DbType.Int32, Me.GetRefObject(babitIklanDetail.BabitParameterHeader))
            DbCommandWrapper.AddInParameter("@CategoryID", DbType.Int32, Me.GetRefObject(babitIklanDetail.Category))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As BabitIklanDetail

            Dim babitIklanDetail As BabitIklanDetail = New BabitIklanDetail

            babitIklanDetail.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("MediaName")) Then babitIklanDetail.MediaName = dr("MediaName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Size")) Then babitIklanDetail.Size = dr("Size").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ViewNumber")) Then babitIklanDetail.ViewNumber = CType(dr("ViewNumber"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("SubmissionAmount")) Then babitIklanDetail.SubmissionAmount = CType(dr("SubmissionAmount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("PeriodIklanStart")) Then babitIklanDetail.PeriodIklanStart = CType(dr("PeriodIklanStart"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("PeriodIklanEnd")) Then babitIklanDetail.PeriodIklanEnd = CType(dr("PeriodIklanEnd"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then babitIklanDetail.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then babitIklanDetail.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then babitIklanDetail.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then babitIklanDetail.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then babitIklanDetail.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("BabitHeaderID")) Then
                babitIklanDetail.BabitHeader = New BabitHeader(CType(dr("BabitHeaderID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("BabitParameterDetailID")) Then
                babitIklanDetail.BabitParameterDetail = New BabitParameterDetail(CType(dr("BabitParameterDetailID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("BabitParameterHeaderID")) Then
                babitIklanDetail.BabitParameterHeader = New BabitParameterHeader(CType(dr("BabitParameterHeaderID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("CategoryID")) Then
                babitIklanDetail.Category = New Category(CType(dr("CategoryID"), Integer))
            End If
            Return babitIklanDetail

        End Function

        Private Sub SetTableName()

            If Not (GetType(BabitIklanDetail) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(BabitIklanDetail), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(BabitIklanDetail).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

