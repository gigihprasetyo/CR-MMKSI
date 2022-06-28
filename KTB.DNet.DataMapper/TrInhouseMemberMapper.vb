
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DN
'// PURPOSE       : TrInhouseMember Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2009 
'// ---------------------
'// $History      : $
'// Generated on 8/7/2009 - 11:36:36 AM
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

    Public Class TrInhouseMemberMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertTrInhouseMember"
        Private m_UpdateStatement As String = "up_UpdateTrInhouseMember"
        Private m_RetrieveStatement As String = "up_RetrieveTrInhouseMember"
        Private m_RetrieveListStatement As String = "up_RetrieveTrInhouseMemberList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteTrInhouseMember"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim trInhouseMember As TrInhouseMember = Nothing
            While dr.Read

                trInhouseMember = Me.CreateObject(dr)

            End While

            Return trInhouseMember

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim trInhouseMemberList As ArrayList = New ArrayList

            While dr.Read
                Dim trInhouseMember As TrInhouseMember = Me.CreateObject(dr)
                trInhouseMemberList.Add(trInhouseMember)
            End While

            Return trInhouseMemberList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim trInhouseMember As TrInhouseMember = CType(obj, TrInhouseMember)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, trInhouseMember.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim trInhouseMember As TrInhouseMember = CType(obj, TrInhouseMember)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DBCommandWrapper.AddInParameter("@InhouseID", DbType.Int32, trInhouseMember.InhouseID)
            DBCommandWrapper.AddInParameter("@CourseID", DbType.Int32, trInhouseMember.CourseID)
            DbCommandWrapper.AddInParameter("@TraineeID", DbType.Int32, trInhouseMember.TraineeID)
            DbCommandWrapper.AddInParameter("@ClassID", DbType.Int32, trInhouseMember.ClassID)
            DbCommandWrapper.AddInParameter("@Result", DbType.Decimal, trInhouseMember.Result)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, trInhouseMember.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, trInhouseMember.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, trInhouseMember.ID)

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

            Dim trInhouseMember As TrInhouseMember = CType(obj, TrInhouseMember)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DBCommandWrapper.AddInParameter("@InhouseID", DbType.Int32, trInhouseMember.InhouseID)
            DBCommandWrapper.AddInParameter("@CourseID", DbType.Int32, trInhouseMember.CourseID)
            DbCommandWrapper.AddInParameter("@TraineeID", DbType.Int32, trInhouseMember.TraineeID)
            DbCommandWrapper.AddInParameter("@ClassID", DbType.Int32, trInhouseMember.ClassID)
            DbCommandWrapper.AddInParameter("@Result", DbType.Decimal, trInhouseMember.Result)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, trInhouseMember.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, trInhouseMember.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, trInhouseMember.ID)

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As TrInhouseMember

            Dim trInhouseMember As TrInhouseMember = New TrInhouseMember

            If Not dr.IsDBNull(dr.GetOrdinal("InhouseID")) Then trInhouseMember.InhouseID = CType(dr("InhouseID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("CourseID")) Then trInhouseMember.CourseID = CType(dr("CourseID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("TraineeID")) Then trInhouseMember.TraineeID = CType(dr("TraineeID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("ClassID")) Then trInhouseMember.ClassID = CType(dr("ClassID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Result")) Then trInhouseMember.Result = CType(dr("Result"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then trInhouseMember.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then trInhouseMember.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then trInhouseMember.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then trInhouseMember.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then trInhouseMember.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("ID")) Then
                trInhouseMember.ID = CType(dr("ID"), Integer)
            End If

            Return trInhouseMember

        End Function

        Private Sub SetTableName()

            If Not (GetType(TrInhouseMember) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(TrInhouseMember), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(TrInhouseMember).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

