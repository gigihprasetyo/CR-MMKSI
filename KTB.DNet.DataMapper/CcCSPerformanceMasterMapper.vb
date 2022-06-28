
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : CcCSPerformanceMaster Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 5/8/2018 - 2:47:18 PM
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

    Public Class CcCSPerformanceMasterMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertCcCSPerformanceMaster"
        Private m_UpdateStatement As String = "up_UpdateCcCSPerformanceMaster"
        Private m_RetrieveStatement As String = "up_RetrieveCcCSPerformanceMaster"
        Private m_RetrieveListStatement As String = "up_RetrieveCcCSPerformanceMasterList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteCcCSPerformanceMaster"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim ccCSPerformanceMaster As CcCSPerformanceMaster = Nothing
            While dr.Read

                ccCSPerformanceMaster = Me.CreateObject(dr)

            End While

            Return ccCSPerformanceMaster

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim ccCSPerformanceMasterList As ArrayList = New ArrayList

            While dr.Read
                Dim ccCSPerformanceMaster As CcCSPerformanceMaster = Me.CreateObject(dr)
                ccCSPerformanceMasterList.Add(ccCSPerformanceMaster)
            End While

            Return ccCSPerformanceMasterList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim ccCSPerformanceMaster As CcCSPerformanceMaster = CType(obj, CcCSPerformanceMaster)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, ccCSPerformanceMaster.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim ccCSPerformanceMaster As CcCSPerformanceMaster = CType(obj, CcCSPerformanceMaster)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@Code", DbType.AnsiString, ccCSPerformanceMaster.Code)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, ccCSPerformanceMaster.Description)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, ccCSPerformanceMaster.Status)
            DbCommandWrapper.AddInParameter("@referenceID", DbType.Int32, ccCSPerformanceMaster.ReferenceID)
            DbCommandWrapper.AddInParameter("@CcPeriodIDFrom", DbType.Int32, ccCSPerformanceMaster.CcPeriodIDFrom)
            DbCommandWrapper.AddInParameter("@CcPeriodIDTo", DbType.Int32, ccCSPerformanceMaster.CcPeriodIDTo)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, ccCSPerformanceMaster.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, ccCSPerformanceMaster.LastUpdateBy)
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

            Dim ccCSPerformanceMaster As CcCSPerformanceMaster = CType(obj, CcCSPerformanceMaster)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, ccCSPerformanceMaster.ID)
            DbCommandWrapper.AddInParameter("@Code", DbType.AnsiString, ccCSPerformanceMaster.Code)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, ccCSPerformanceMaster.Description)
            DbCommandWrapper.AddInParameter("@referenceID", DbType.Int32, ccCSPerformanceMaster.ReferenceID)
            DbCommandWrapper.AddInParameter("@CcPeriodIDFrom", DbType.Int32, ccCSPerformanceMaster.CcPeriodIDFrom)
            DbCommandWrapper.AddInParameter("@CcPeriodIDTo", DbType.Int32, ccCSPerformanceMaster.CcPeriodIDTo)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, ccCSPerformanceMaster.Status)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, ccCSPerformanceMaster.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, ccCSPerformanceMaster.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As CcCSPerformanceMaster

            Dim ccCSPerformanceMaster As CcCSPerformanceMaster = New CcCSPerformanceMaster

            ccCSPerformanceMaster.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Code")) Then ccCSPerformanceMaster.Code = dr("Code").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Description")) Then ccCSPerformanceMaster.Description = dr("Description").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then ccCSPerformanceMaster.Status = CType(dr("Status"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("ReferenceID")) Then ccCSPerformanceMaster.ReferenceID = CType(dr("ReferenceID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("CcPeriodIDFrom")) Then ccCSPerformanceMaster.CcPeriodIDFrom = CType(dr("CcPeriodIDFrom"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("CcPeriodIDTo")) Then ccCSPerformanceMaster.CcPeriodIDTo = CType(dr("CcPeriodIDTo"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then ccCSPerformanceMaster.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then ccCSPerformanceMaster.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then ccCSPerformanceMaster.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then ccCSPerformanceMaster.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then ccCSPerformanceMaster.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            Return ccCSPerformanceMaster

        End Function

        Private Sub SetTableName()

            If Not (GetType(CcCSPerformanceMaster) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(CcCSPerformanceMaster), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(CcCSPerformanceMaster).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

