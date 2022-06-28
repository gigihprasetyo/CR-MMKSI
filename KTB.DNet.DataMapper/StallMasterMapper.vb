#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : StallMaster Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2005 
'// ---------------------
'// $History      : $
'// Generated on 11/30/2005 - 1:05:06 PM
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

    Public Class StallMasterMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertStallMaster"
        Private m_UpdateStatement As String = "up_UpdateStallMaster"
        Private m_RetrieveStatement As String = "up_RetrieveStallMaster"
        Private m_RetrieveListStatement As String = "up_RetrieveStallMasterList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteStallMaster"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim StallMaster As StallMaster = Nothing
            While dr.Read

                StallMaster = Me.CreateObject(dr)

            End While

            Return StallMaster

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim StallMasterList As ArrayList = New ArrayList

            While dr.Read
                Dim StallMaster As StallMaster = Me.CreateObject(dr)
                StallMasterList.Add(StallMaster)
            End While

            Return StallMasterList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim StallMaster As StallMaster = CType(obj, StallMaster)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, StallMaster.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim StallMaster As StallMaster = CType(obj, StallMaster)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@StallCode", DbType.AnsiString, StallMaster.StallCode)
            DbCommandWrapper.AddInParameter("@StallCodeDealer", DbType.AnsiString, StallMaster.StallCodeDealer)
            DbCommandWrapper.AddInParameter("@StallName", DbType.AnsiString, StallMaster.StallName)
            DbCommandWrapper.AddInParameter("@StallLocation", DbType.AnsiString, StallMaster.StallLocation)
            DbCommandWrapper.AddInParameter("@StallType", DbType.AnsiString, StallMaster.StallType)
            DbCommandWrapper.AddInParameter("@StallCategory", DbType.AnsiString, StallMaster.StallCategory)
            DbCommandWrapper.AddInParameter("@IsBodyPaint", DbType.AnsiString, StallMaster.IsBodyPaint)
            DbCommandWrapper.AddInParameter("@Status", DbType.AnsiString, StallMaster.Status)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, StallMaster.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdatedBy", DbType.AnsiString, StallMaster.LastUpdatedBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, Me.GetRefObject(StallMaster.Dealer))

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

            Dim StallMaster As StallMaster = CType(obj, StallMaster)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, StallMaster.ID)
            DbCommandWrapper.AddInParameter("@StallCode", DbType.AnsiString, StallMaster.StallCode)
            DbCommandWrapper.AddInParameter("@StallCodeDealer", DbType.AnsiString, StallMaster.StallCodeDealer)
            DbCommandWrapper.AddInParameter("@StallName", DbType.AnsiString, StallMaster.StallName)
            DbCommandWrapper.AddInParameter("@StallLocation", DbType.AnsiString, StallMaster.StallLocation)
            DbCommandWrapper.AddInParameter("@StallType", DbType.AnsiString, StallMaster.StallType)
            DbCommandWrapper.AddInParameter("@StallCategory", DbType.AnsiString, StallMaster.StallCategory)
            DbCommandWrapper.AddInParameter("@IsBodyPaint", DbType.AnsiString, StallMaster.IsBodyPaint)
            DbCommandWrapper.AddInParameter("@Status", DbType.AnsiString, StallMaster.Status)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, StallMaster.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, StallMaster.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, Me.GetRefObject(StallMaster.Dealer))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As StallMaster

            Dim StallMaster As StallMaster = New StallMaster

            StallMaster.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("StallCode")) Then StallMaster.StallCode = dr("StallCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("StallCodeDealer")) Then StallMaster.StallCodeDealer = dr("StallCodeDealer").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("StallName")) Then StallMaster.StallName = dr("StallName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("StallLocation")) Then StallMaster.StallLocation = dr("StallLocation").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("StallType")) Then StallMaster.StallType = dr("StallType").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("StallCategory")) Then StallMaster.StallCategory = dr("StallCategory").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("IsBodyPaint")) Then StallMaster.IsBodyPaint = dr("IsBodyPaint").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then StallMaster.Status = dr("Status").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then StallMaster.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then StallMaster.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then StallMaster.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdatedBy")) Then StallMaster.LastUpdatedBy = dr("LastUpdatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdatedTime")) Then StallMaster.LastUpdatedTime = CType(dr("LastUpdatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then
                StallMaster.Dealer = New Dealer(CType(dr("DealerID"), Short))
            End If

            Return StallMaster

        End Function

        Private Sub SetTableName()

            If Not (GetType(StallMaster) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(StallMaster), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(StallMaster).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

