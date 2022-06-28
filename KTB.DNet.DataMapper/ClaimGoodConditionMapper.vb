#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : ClaimGoodCondition Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2007 
'// ---------------------
'// $History      : $
'// Generated on 7/16/2007 - 10:48:59 AM
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

    Public Class ClaimGoodConditionMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertClaimGoodCondition"
        Private m_UpdateStatement As String = "up_UpdateClaimGoodCondition"
        Private m_RetrieveStatement As String = "up_RetrieveClaimGoodCondition"
        Private m_RetrieveListStatement As String = "up_RetrieveClaimGoodConditionList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteClaimGoodCondition"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim claimGoodCondition As ClaimGoodCondition = Nothing
            While dr.Read

                claimGoodCondition = Me.CreateObject(dr)

            End While

            Return claimGoodCondition

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim claimGoodConditionList As ArrayList = New ArrayList

            While dr.Read
                Dim claimGoodCondition As ClaimGoodCondition = Me.CreateObject(dr)
                claimGoodConditionList.Add(claimGoodCondition)
            End While

            Return claimGoodConditionList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim claimGoodCondition As ClaimGoodCondition = CType(obj, ClaimGoodCondition)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, claimGoodCondition.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim claimGoodCondition As ClaimGoodCondition = CType(obj, ClaimGoodCondition)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@Code", DbType.AnsiString, claimGoodCondition.Code)
            DbCommandWrapper.AddInParameter("@Condition", DbType.AnsiString, claimGoodCondition.Condition)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, claimGoodCondition.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, claimGoodCondition.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


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

            Dim claimGoodCondition As ClaimGoodCondition = CType(obj, ClaimGoodCondition)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, claimGoodCondition.ID)
            DbCommandWrapper.AddInParameter("@Code", DbType.AnsiString, claimGoodCondition.Code)
            DbCommandWrapper.AddInParameter("@Condition", DbType.AnsiString, claimGoodCondition.Condition)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, claimGoodCondition.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, claimGoodCondition.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As ClaimGoodCondition

            Dim claimGoodCondition As ClaimGoodCondition = New ClaimGoodCondition

            claimGoodCondition.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Code")) Then claimGoodCondition.Code = dr("Code").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Condition")) Then claimGoodCondition.Condition = dr("Condition").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then claimGoodCondition.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then claimGoodCondition.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then claimGoodCondition.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then claimGoodCondition.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then claimGoodCondition.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            Return claimGoodCondition

        End Function

        Private Sub SetTableName()

            If Not (GetType(ClaimGoodCondition) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(ClaimGoodCondition), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(ClaimGoodCondition).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

