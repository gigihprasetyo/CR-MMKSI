
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : BabitEventProposalActivity Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2019 
'// ---------------------
'// $History      : $
'// Generated on 22/05/2019 - 8:28:53
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

    Public Class BabitEventProposalActivityMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertBabitEventProposalActivity"
        Private m_UpdateStatement As String = "up_UpdateBabitEventProposalActivity"
        Private m_RetrieveStatement As String = "up_RetrieveBabitEventProposalActivity"
        Private m_RetrieveListStatement As String = "up_RetrieveBabitEventProposalActivityList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteBabitEventProposalActivity"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim babitEventProposalActivity As BabitEventProposalActivity = Nothing
            While dr.Read

                babitEventProposalActivity = Me.CreateObject(dr)

            End While

            Return babitEventProposalActivity

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim babitEventProposalActivityList As ArrayList = New ArrayList

            While dr.Read
                Dim babitEventProposalActivity As BabitEventProposalActivity = Me.CreateObject(dr)
                babitEventProposalActivityList.Add(babitEventProposalActivity)
            End While

            Return babitEventProposalActivityList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim babitEventProposalActivity As BabitEventProposalActivity = CType(obj, BabitEventProposalActivity)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, babitEventProposalActivity.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim babitEventProposalActivity As BabitEventProposalActivity = CType(obj, BabitEventProposalActivity)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@ActivityType", DbType.Int32, babitEventProposalActivity.ActivityType)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, babitEventProposalActivity.Description)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, babitEventProposalActivity.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, babitEventProposalActivity.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@BabitEventProposalHeaderID", DbType.Int32, Me.GetRefObject(babitEventProposalActivity.BabitEventProposalHeader))

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

            Dim babitEventProposalActivity As BabitEventProposalActivity = CType(obj, BabitEventProposalActivity)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, babitEventProposalActivity.ID)
            DbCommandWrapper.AddInParameter("@ActivityType", DbType.Int32, babitEventProposalActivity.ActivityType)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, babitEventProposalActivity.Description)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, babitEventProposalActivity.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, babitEventProposalActivity.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@BabitEventProposalHeaderID", DbType.Int32, Me.GetRefObject(babitEventProposalActivity.BabitEventProposalHeader))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As BabitEventProposalActivity

            Dim babitEventProposalActivity As BabitEventProposalActivity = New BabitEventProposalActivity

            babitEventProposalActivity.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("ActivityType")) Then babitEventProposalActivity.ActivityType = CType(dr("ActivityType"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Description")) Then babitEventProposalActivity.Description = dr("Description").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then babitEventProposalActivity.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then babitEventProposalActivity.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then babitEventProposalActivity.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then babitEventProposalActivity.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then babitEventProposalActivity.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("BabitEventProposalHeaderID")) Then
                babitEventProposalActivity.BabitEventProposalHeader = New BabitEventProposalHeader(CType(dr("BabitEventProposalHeaderID"), Integer))
            End If

            Return babitEventProposalActivity

        End Function

        Private Sub SetTableName()

            If Not (GetType(BabitEventProposalActivity) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(BabitEventProposalActivity), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(BabitEventProposalActivity).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

