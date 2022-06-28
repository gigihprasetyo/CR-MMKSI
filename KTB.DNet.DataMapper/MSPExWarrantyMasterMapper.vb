#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : MSPExWarrantyMaster Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2020 
'// ---------------------
'// $History      : $
'// Generated on 12/17/2020 - 2:14:25 PM
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

    Public Class MSPExWarrantyMasterMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertMSPExWarrantyMaster"
        Private m_UpdateStatement As String = "up_UpdateMSPExWarrantyMaster"
        Private m_RetrieveStatement As String = "up_RetrieveMSPExWarrantyMaster"
        Private m_RetrieveListStatement As String = "up_RetrieveMSPExWarrantyMasterList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteMSPExWarrantyMaster"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim mSPExWarrantyMaster As MSPExWarrantyMaster = Nothing
            While dr.Read

                mSPExWarrantyMaster = Me.CreateObject(dr)

            End While

            Return mSPExWarrantyMaster

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim mSPExWarrantyMasterList As ArrayList = New ArrayList

            While dr.Read
                Dim mSPExWarrantyMaster As MSPExWarrantyMaster = Me.CreateObject(dr)
                mSPExWarrantyMasterList.Add(mSPExWarrantyMaster)
            End While

            Return mSPExWarrantyMasterList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim mSPExWarrantyMaster As MSPExWarrantyMaster = CType(obj, MSPExWarrantyMaster)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, mSPExWarrantyMaster.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim mSPExWarrantyMaster As MSPExWarrantyMaster = CType(obj, MSPExWarrantyMaster)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@Duration", DbType.Int16, mSPExWarrantyMaster.Duration)
            DbCommandWrapper.AddInParameter("@KM", DbType.Int32, mSPExWarrantyMaster.KM)
            DbCommandWrapper.AddInParameter("@StartDate", DbType.DateTime, mSPExWarrantyMaster.StartDate)
            DbCommandWrapper.AddInParameter("@EndDate", DbType.DateTime, mSPExWarrantyMaster.EndDate)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, mSPExWarrantyMaster.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, mSPExWarrantyMaster.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@MSPExTypeID", DbType.Int32, Me.GetRefObject(mSPExWarrantyMaster.MSPExType))

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

            Dim mSPExWarrantyMaster As MSPExWarrantyMaster = CType(obj, MSPExWarrantyMaster)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, mSPExWarrantyMaster.ID)
            DbCommandWrapper.AddInParameter("@Duration", DbType.Int16, mSPExWarrantyMaster.Duration)
            DbCommandWrapper.AddInParameter("@KM", DbType.Int32, mSPExWarrantyMaster.KM)
            DbCommandWrapper.AddInParameter("@StartDate", DbType.DateTime, mSPExWarrantyMaster.StartDate)
            DbCommandWrapper.AddInParameter("@EndDate", DbType.DateTime, mSPExWarrantyMaster.EndDate)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, mSPExWarrantyMaster.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, mSPExWarrantyMaster.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@MSPExTypeID", DbType.Int32, Me.GetRefObject(mSPExWarrantyMaster.MSPExType))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As MSPExWarrantyMaster

            Dim mSPExWarrantyMaster As MSPExWarrantyMaster = New MSPExWarrantyMaster

            mSPExWarrantyMaster.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Duration")) Then mSPExWarrantyMaster.Duration = CType(dr("Duration"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("KM")) Then mSPExWarrantyMaster.KM = CType(dr("KM"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("StartDate")) Then mSPExWarrantyMaster.StartDate = CType(dr("StartDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("EndDate")) Then mSPExWarrantyMaster.EndDate = CType(dr("EndDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then mSPExWarrantyMaster.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then mSPExWarrantyMaster.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then mSPExWarrantyMaster.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then mSPExWarrantyMaster.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then mSPExWarrantyMaster.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            If Not dr.IsDBNull(dr.GetOrdinal("MSPExTypeID")) Then
                mSPExWarrantyMaster.MSPExType = New MSPExType(CType(dr("MSPExTypeID"), Integer))
            End If

            Return mSPExWarrantyMaster

        End Function

        Private Sub SetTableName()

            If Not (GetType(MSPExWarrantyMaster) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(MSPExWarrantyMaster), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(MSPExWarrantyMaster).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace
