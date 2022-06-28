#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : MaterialPromotionPeriod Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2007 
'// ---------------------
'// $History      : $
'// Generated on 10/8/2007 - 3:08:28 PM
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

    Public Class MaterialPromotionPeriodMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertMaterialPromotionPeriod"
        Private m_UpdateStatement As String = "up_UpdateMaterialPromotionPeriod"
        Private m_RetrieveStatement As String = "up_RetrieveMaterialPromotionPeriod"
        Private m_RetrieveListStatement As String = "up_RetrieveMaterialPromotionPeriodList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteMaterialPromotionPeriod"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim materialPromotionPeriod As MaterialPromotionPeriod = Nothing
            While dr.Read

                materialPromotionPeriod = Me.CreateObject(dr)

            End While

            Return materialPromotionPeriod

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim materialPromotionPeriodList As ArrayList = New ArrayList

            While dr.Read
                Dim materialPromotionPeriod As MaterialPromotionPeriod = Me.CreateObject(dr)
                materialPromotionPeriodList.Add(materialPromotionPeriod)
            End While

            Return materialPromotionPeriodList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim materialPromotionPeriod As MaterialPromotionPeriod = CType(obj, MaterialPromotionPeriod)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, materialPromotionPeriod.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim materialPromotionPeriod As MaterialPromotionPeriod = CType(obj, MaterialPromotionPeriod)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@PeriodName", DbType.AnsiString, materialPromotionPeriod.PeriodName)
            DbCommandWrapper.AddInParameter("@StartDate", DbType.DateTime, materialPromotionPeriod.StartDate)
            DbCommandWrapper.AddInParameter("@EndDate", DbType.DateTime, materialPromotionPeriod.EndDate)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, materialPromotionPeriod.Description)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, materialPromotionPeriod.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, materialPromotionPeriod.LastUpdateBy)
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

            Dim materialPromotionPeriod As MaterialPromotionPeriod = CType(obj, MaterialPromotionPeriod)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, materialPromotionPeriod.ID)
            DbCommandWrapper.AddInParameter("@PeriodName", DbType.AnsiString, materialPromotionPeriod.PeriodName)
            DbCommandWrapper.AddInParameter("@StartDate", DbType.DateTime, materialPromotionPeriod.StartDate)
            DbCommandWrapper.AddInParameter("@EndDate", DbType.DateTime, materialPromotionPeriod.EndDate)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, materialPromotionPeriod.Description)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, materialPromotionPeriod.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, materialPromotionPeriod.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As MaterialPromotionPeriod

            Dim materialPromotionPeriod As MaterialPromotionPeriod = New MaterialPromotionPeriod

            materialPromotionPeriod.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("PeriodName")) Then materialPromotionPeriod.PeriodName = dr("PeriodName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("StartDate")) Then materialPromotionPeriod.StartDate = CType(dr("StartDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("EndDate")) Then materialPromotionPeriod.EndDate = CType(dr("EndDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("Description")) Then materialPromotionPeriod.Description = dr("Description").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then materialPromotionPeriod.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then materialPromotionPeriod.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then materialPromotionPeriod.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then materialPromotionPeriod.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then materialPromotionPeriod.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            Return materialPromotionPeriod

        End Function

        Private Sub SetTableName()

            If Not (GetType(MaterialPromotionPeriod) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(MaterialPromotionPeriod), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(MaterialPromotionPeriod).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

