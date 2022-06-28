#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : V_DraftPOTotalDetailMapper Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2010 
'// ---------------------
'// $History      : $
'// Generated on 12/26/2018 - 10:59:08 AM
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

    Public Class V_DraftPOTotalDetailMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertV_DraftPOTotalDetail"
        Private m_UpdateStatement As String = "up_UpdateV_DraftPOTotalDetail"
        Private m_RetrieveStatement As String = "up_RetrieveV_DraftPOTotalDetail"
        Private m_RetrieveListStatement As String = "up_RetrieveV_DraftPOTotalDetailList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteV_DraftPOTotalDetail"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim V_DraftPOTotalDetail As V_DraftPOTotalDetail = Nothing
            While dr.Read

                V_DraftPOTotalDetail = Me.CreateObject(dr)

            End While

            Return V_DraftPOTotalDetail

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim V_DraftPOTotalDetailList As ArrayList = New ArrayList

            While dr.Read
                Dim V_DraftPOTotalDetail As V_DraftPOTotalDetail = Me.CreateObject(dr)
                V_DraftPOTotalDetailList.Add(V_DraftPOTotalDetail)
            End While

            Return V_DraftPOTotalDetailList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim V_DraftPOTotalDetail As V_DraftPOTotalDetail = CType(obj, V_DraftPOTotalDetail)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, V_DraftPOTotalDetail.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim V_DraftPOTotalDetail As V_DraftPOTotalDetail = CType(obj, V_DraftPOTotalDetail)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@CreditAccount", DbType.AnsiString, V_DraftPOTotalDetail.CreditAccount)
            DbCommandWrapper.AddInParameter("@IsFactoring", DbType.Int16, V_DraftPOTotalDetail.IsFactoring)
            DbCommandWrapper.AddInParameter("@Status", DbType.AnsiString, V_DraftPOTotalDetail.Status)
            DbCommandWrapper.AddInParameter("@TotalDetail", DbType.Currency, V_DraftPOTotalDetail.TotalDetail)
            DbCommandWrapper.AddInParameter("@Gyro", DbType.Int32, V_DraftPOTotalDetail.Gyro)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, V_DraftPOTotalDetail.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, V_DraftPOTotalDetail.LastUpdateBy)
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

            Dim V_DraftPOTotalDetail As V_DraftPOTotalDetail = CType(obj, V_DraftPOTotalDetail)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, V_DraftPOTotalDetail.ID)
            DbCommandWrapper.AddInParameter("@CreditAccount", DbType.AnsiString, V_DraftPOTotalDetail.CreditAccount)
            DbCommandWrapper.AddInParameter("@IsFactoring", DbType.Int16, V_DraftPOTotalDetail.IsFactoring)
            DbCommandWrapper.AddInParameter("@Status", DbType.AnsiString, V_DraftPOTotalDetail.Status)
            DbCommandWrapper.AddInParameter("@TotalDetail", DbType.Currency, V_DraftPOTotalDetail.TotalDetail)
            DbCommandWrapper.AddInParameter("@Gyro", DbType.Int32, V_DraftPOTotalDetail.Gyro)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, V_DraftPOTotalDetail.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, V_DraftPOTotalDetail.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As V_DraftPOTotalDetail

            Dim V_DraftPOTotalDetail As V_DraftPOTotalDetail = New V_DraftPOTotalDetail

            V_DraftPOTotalDetail.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("CreditAccount")) Then V_DraftPOTotalDetail.CreditAccount = dr("CreditAccount").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("IsFactoring")) Then V_DraftPOTotalDetail.IsFactoring = CType(dr("IsFactoring"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then V_DraftPOTotalDetail.Status = dr("Status").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("TotalDetail")) Then V_DraftPOTotalDetail.TotalDetail = CType(dr("TotalDetail"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("Gyro")) Then V_DraftPOTotalDetail.Gyro = CType(dr("Gyro"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then V_DraftPOTotalDetail.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then V_DraftPOTotalDetail.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then V_DraftPOTotalDetail.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then V_DraftPOTotalDetail.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then V_DraftPOTotalDetail.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("ProductCategoryID")) Then
                V_DraftPOTotalDetail.ProductCategory = New ProductCategory(CType(dr("ProductCategoryID"), Short))
            End If
            Return V_DraftPOTotalDetail

        End Function

        Private Sub SetTableName()

            If Not (GetType(V_DraftPOTotalDetail) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(V_DraftPOTotalDetail), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(V_DraftPOTotalDetail).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

