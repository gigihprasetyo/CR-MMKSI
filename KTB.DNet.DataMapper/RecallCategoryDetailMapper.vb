
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

    Public Class RecallCategoryDetailMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertRecallCategoryDetail"
        Private m_UpdateStatement As String = "up_UpdateRecallCategoryDetail"
        Private m_RetrieveStatement As String = "up_RetrieveRecallCategoryDetail"
        Private m_RetrieveListStatement As String = "up_RetrieveRecallCategoryDetailList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteRecallCategoryDetail"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim recallCategoryDetail As RecallCategoryDetail = Nothing
            While dr.Read

                recallCategoryDetail = Me.CreateObject(dr)

            End While

            Return recallCategoryDetail

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim recallCategoryList As ArrayList = New ArrayList

            While dr.Read
                Dim recallCategoryDetail As RecallCategoryDetail = Me.CreateObject(dr)
                recallCategoryList.Add(recallCategoryDetail)
            End While

            Return recallCategoryList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim recallCategoryDetail As RecallCategoryDetail = CType(obj, RecallCategoryDetail)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, recallCategoryDetail.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim recallCategoryDetail As RecallCategoryDetail = CType(obj, RecallCategoryDetail)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)

            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@RecallCategoryID", DbType.Int32, recallCategoryDetail.RecallCategoryID)
            DbCommandWrapper.AddInParameter("@LaborMasterID", DbType.Int32, recallCategoryDetail.LaborMasterID)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int32, recallCategoryDetail.Status)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, recallCategoryDetail.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, recallCategoryDetail.LastUpdateBy)

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

            Dim recallCategoryDetail As RecallCategoryDetail = CType(obj, RecallCategoryDetail)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, recallCategoryDetail.ID)
            DbCommandWrapper.AddInParameter("@LaborMasterID", DbType.Int32, recallCategoryDetail.LaborMasterID)
            DbCommandWrapper.AddInParameter("@RecallCategoryID", DbType.Int32, recallCategoryDetail.RecallCategoryID)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, recallCategoryDetail.RowStatus)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, recallCategoryDetail.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, recallCategoryDetail.CreatedBy)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As RecallCategoryDetail

            Dim recallCategoryDetail As RecallCategoryDetail = New RecallCategoryDetail

            recallCategoryDetail.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("LaborMasterID")) Then recallCategoryDetail.LaborMasterID = dr("LaborMasterID")
            If Not dr.IsDBNull(dr.GetOrdinal("RecallCategoryID")) Then recallCategoryDetail.RecallCategoryID = dr("RecallCategoryID")
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then recallCategoryDetail.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then recallCategoryDetail.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then recallCategoryDetail.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then recallCategoryDetail.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then recallCategoryDetail.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            Return recallCategoryDetail

        End Function

        Private Sub SetTableName()

            If Not (GetType(RecallCategoryDetail) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(RecallCategoryDetail), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(RecallCategoryDetail).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

