
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : RedemptionStatus Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2012 
'// ---------------------
'// $History      : $
'// Generated on 3/26/2012 - 2:07:26 PM
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

    Public Class RedemptionStatusMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertRedemptionStatus"
        Private m_UpdateStatement As String = "up_UpdateRedemptionStatus"
        Private m_RetrieveStatement As String = "up_RetrieveRedemptionStatus"
        Private m_RetrieveListStatement As String = "up_RetrieveRedemptionStatusList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteRedemptionStatus"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim redemptionStatus As RedemptionStatus = Nothing
            While dr.Read

                redemptionStatus = Me.CreateObject(dr)

            End While

            Return redemptionStatus

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim redemptionStatusList As ArrayList = New ArrayList

            While dr.Read
                Dim redemptionStatus As RedemptionStatus = Me.CreateObject(dr)
                redemptionStatusList.Add(redemptionStatus)
            End While

            Return redemptionStatusList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim redemptionStatus As RedemptionStatus = CType(obj, RedemptionStatus)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, redemptionStatus.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim redemptionStatus As RedemptionStatus = CType(obj, RedemptionStatus)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@PeriodDate", DbType.DateTime, redemptionStatus.PeriodDate)
            DBCommandWrapper.AddInParameter("@CategoryCode", DbType.AnsiString, redemptionStatus.CategoryCode)
            DBCommandWrapper.AddInParameter("@SubCategoryCode", DbType.AnsiString, redemptionStatus.SubCategoryCode)
            DBCommandWrapper.AddInParameter("@Status", DbType.Int16, redemptionStatus.Status)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, redemptionStatus.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, redemptionStatus.LastUpdateBy)
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

            Dim redemptionStatus As RedemptionStatus = CType(obj, RedemptionStatus)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, redemptionStatus.ID)
            DbCommandWrapper.AddInParameter("@PeriodDate", DbType.DateTime, redemptionStatus.PeriodDate)
            DBCommandWrapper.AddInParameter("@CategoryCode", DbType.AnsiString, redemptionStatus.CategoryCode)
            DBCommandWrapper.AddInParameter("@SubCategoryCode", DbType.AnsiString, redemptionStatus.SubCategoryCode)
            DBCommandWrapper.AddInParameter("@Status", DbType.Int16, redemptionStatus.Status)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, redemptionStatus.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, redemptionStatus.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As RedemptionStatus

            Dim redemptionStatus As RedemptionStatus = New RedemptionStatus

            redemptionStatus.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("PeriodDate")) Then redemptionStatus.PeriodDate = CType(dr("PeriodDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("CategoryCode")) Then redemptionStatus.CategoryCode = dr("CategoryCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SubCategoryCode")) Then redemptionStatus.SubCategoryCode = dr("SubCategoryCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then redemptionStatus.Status = CType(dr("Status"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then redemptionStatus.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then redemptionStatus.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then redemptionStatus.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then redemptionStatus.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then redemptionStatus.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            Return redemptionStatus

        End Function

        Private Sub SetTableName()

            If Not (GetType(RedemptionStatus) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(RedemptionStatus), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(RedemptionStatus).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

