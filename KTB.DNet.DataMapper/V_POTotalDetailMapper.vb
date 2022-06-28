
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : V_POTotalDetail Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2010 
'// ---------------------
'// $History      : $
'// Generated on 10/6/2010 - 10:59:08 AM
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

    Public Class V_POTotalDetailMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertV_POTotalDetail"
        Private m_UpdateStatement As String = "up_UpdateV_POTotalDetail"
        Private m_RetrieveStatement As String = "up_RetrieveV_POTotalDetail"
        Private m_RetrieveListStatement As String = "up_RetrieveV_POTotalDetailList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteV_POTotalDetail"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim v_POTotalDetail As V_POTotalDetail = Nothing
            While dr.Read

                v_POTotalDetail = Me.CreateObject(dr)

            End While

            Return v_POTotalDetail

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim v_POTotalDetailList As ArrayList = New ArrayList

            While dr.Read
                Dim v_POTotalDetail As V_POTotalDetail = Me.CreateObject(dr)
                v_POTotalDetailList.Add(v_POTotalDetail)
            End While

            Return v_POTotalDetailList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim v_POTotalDetail As V_POTotalDetail = CType(obj, V_POTotalDetail)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, v_POTotalDetail.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim v_POTotalDetail As V_POTotalDetail = CType(obj, V_POTotalDetail)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DBCommandWrapper.AddInParameter("@CreditAccount", DbType.AnsiString, v_POTotalDetail.CreditAccount)
            DBCommandWrapper.AddInParameter("@IsFactoring", DbType.Int16, v_POTotalDetail.IsFactoring)
            DbCommandWrapper.AddInParameter("@Status", DbType.AnsiString, v_POTotalDetail.Status)
            DbCommandWrapper.AddInParameter("@TotalDetail", DbType.Currency, v_POTotalDetail.TotalDetail)
            DbCommandWrapper.AddInParameter("@Gyro", DbType.Int32, v_POTotalDetail.Gyro)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, v_POTotalDetail.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, v_POTotalDetail.LastUpdateBy)
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

            Dim v_POTotalDetail As V_POTotalDetail = CType(obj, V_POTotalDetail)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, v_POTotalDetail.ID)
            DbCommandWrapper.AddInParameter("@CreditAccount", DbType.AnsiString, v_POTotalDetail.CreditAccount)
            DbCommandWrapper.AddInParameter("@IsFactoring", DbType.Int16, v_POTotalDetail.IsFactoring)
            DbCommandWrapper.AddInParameter("@Status", DbType.AnsiString, v_POTotalDetail.Status)
            DbCommandWrapper.AddInParameter("@TotalDetail", DbType.Currency, v_POTotalDetail.TotalDetail)
            DbCommandWrapper.AddInParameter("@Gyro", DbType.Int32, v_POTotalDetail.Gyro)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, v_POTotalDetail.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, v_POTotalDetail.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As V_POTotalDetail

            Dim v_POTotalDetail As V_POTotalDetail = New V_POTotalDetail

            v_POTotalDetail.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("CreditAccount")) Then v_POTotalDetail.CreditAccount = dr("CreditAccount").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("IsFactoring")) Then v_POTotalDetail.IsFactoring = CType(dr("IsFactoring"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then v_POTotalDetail.Status = dr("Status").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("TotalDetail")) Then v_POTotalDetail.TotalDetail = CType(dr("TotalDetail"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("Gyro")) Then v_POTotalDetail.Gyro = CType(dr("Gyro"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then v_POTotalDetail.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then v_POTotalDetail.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then v_POTotalDetail.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then v_POTotalDetail.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then v_POTotalDetail.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("ProductCategoryID")) Then
                v_POTotalDetail.ProductCategory = New ProductCategory(CType(dr("ProductCategoryID"), Short))
            End If
            Return v_POTotalDetail

        End Function

        Private Sub SetTableName()

            If Not (GetType(V_POTotalDetail) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(V_POTotalDetail), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(V_POTotalDetail).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

