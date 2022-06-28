
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : MSPExMaster Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2020 
'// ---------------------
'// $History      : $
'// Generated on 11/10/2020 - 9:32:28 AM
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

    Public Class MSPExMasterMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertMSPExMaster"
        Private m_UpdateStatement As String = "up_UpdateMSPExMaster"
        Private m_RetrieveStatement As String = "up_RetrieveMSPExMaster"
        Private m_RetrieveListStatement As String = "up_RetrieveMSPExMasterList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteMSPExMaster"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim mSPExMaster As MSPExMaster = Nothing
            While dr.Read

                mSPExMaster = Me.CreateObject(dr)

            End While

            Return mSPExMaster

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim mSPExMasterList As ArrayList = New ArrayList

            While dr.Read
                Dim mSPExMaster As MSPExMaster = Me.CreateObject(dr)
                mSPExMasterList.Add(mSPExMaster)
            End While

            Return mSPExMasterList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim mSPExMaster As MSPExMaster = CType(obj, MSPExMaster)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, mSPExMaster.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim mSPExMaster As MSPExMaster = CType(obj, MSPExMaster)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@StartDate", DbType.DateTime, mSPExMaster.StartDate)
            DbCommandWrapper.AddInParameter("@EndDate", DbType.DateTime, mSPExMaster.EndDate)
            DbCommandWrapper.AddInParameter("@Duration", DbType.Int16, mSPExMaster.Duration)
            DbCommandWrapper.AddInParameter("@MSPExKM", DbType.Int32, mSPExMaster.MSPExKM)
            DbCommandWrapper.AddInParameter("@Amount", DbType.Currency, mSPExMaster.Amount)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, mSPExMaster.Status)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, mSPExMaster.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, mSPExMaster.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@MSPExTypeID", DbType.Int32, Me.GetRefObject(mSPExMaster.MSPExType))
            DbCommandWrapper.AddInParameter("@VechileTypeID", DbType.Int16, Me.GetRefObject(mSPExMaster.VechileType))

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

            Dim mSPExMaster As MSPExMaster = CType(obj, MSPExMaster)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, mSPExMaster.ID)
            DbCommandWrapper.AddInParameter("@StartDate", DbType.DateTime, mSPExMaster.StartDate)
            DbCommandWrapper.AddInParameter("@EndDate", DbType.DateTime, mSPExMaster.EndDate)
            DbCommandWrapper.AddInParameter("@Duration", DbType.Int16, mSPExMaster.Duration)
            DbCommandWrapper.AddInParameter("@MSPExKM", DbType.Int32, mSPExMaster.MSPExKM)
            DbCommandWrapper.AddInParameter("@Amount", DbType.Currency, mSPExMaster.Amount)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, mSPExMaster.Status)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, mSPExMaster.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, mSPExMaster.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@MSPExTypeID", DbType.Int32, Me.GetRefObject(mSPExMaster.MSPExType))
            DbCommandWrapper.AddInParameter("@VechileTypeID", DbType.Int16, Me.GetRefObject(mSPExMaster.VechileType))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As MSPExMaster

            Dim mSPExMaster As MSPExMaster = New MSPExMaster

            mSPExMaster.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("StartDate")) Then mSPExMaster.StartDate = CType(dr("StartDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("EndDate")) Then mSPExMaster.EndDate = CType(dr("EndDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("Duration")) Then mSPExMaster.Duration = CType(dr("Duration"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("MSPExKM")) Then mSPExMaster.MSPExKM = CType(dr("MSPExKM"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Amount")) Then mSPExMaster.Amount = CType(dr("Amount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then mSPExMaster.Status = CType(dr("Status"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then mSPExMaster.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then mSPExMaster.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then mSPExMaster.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then mSPExMaster.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then mSPExMaster.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("MSPExTypeID")) Then
                mSPExMaster.MSPExType = New MSPExType(CType(dr("MSPExTypeID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("VechileTypeID")) Then
                mSPExMaster.VechileType = New VechileType(CType(dr("VechileTypeID"), Short))
            End If

            Return mSPExMaster

        End Function

        Private Sub SetTableName()

            If Not (GetType(MSPExMaster) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(MSPExMaster), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(MSPExMaster).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

