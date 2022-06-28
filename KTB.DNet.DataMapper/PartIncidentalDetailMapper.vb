
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : PartIncidentalDetail Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2006 
'// ---------------------
'// $History      : $
'// Generated on 10/31/2006 - 9:10:14 AM
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

    Public Class PartIncidentalDetailMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertPartIncidentalDetail"
        Private m_UpdateStatement As String = "up_UpdatePartIncidentalDetail"
        Private m_RetrieveStatement As String = "up_RetrievePartIncidentalDetail"
        Private m_RetrieveListStatement As String = "up_RetrievePartIncidentalDetailList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeletePartIncidentalDetail"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim partIncidentalDetail As PartIncidentalDetail = Nothing
            While dr.Read

                partIncidentalDetail = Me.CreateObject(dr)

            End While

            Return partIncidentalDetail

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim partIncidentalDetailList As ArrayList = New ArrayList

            While dr.Read
                Dim partIncidentalDetail As PartIncidentalDetail = Me.CreateObject(dr)
                partIncidentalDetailList.Add(partIncidentalDetail)
            End While

            Return partIncidentalDetailList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim partIncidentalDetail As PartIncidentalDetail = CType(obj, PartIncidentalDetail)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, partIncidentalDetail.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim partIncidentalDetail As PartIncidentalDetail = CType(obj, PartIncidentalDetail)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@Quantity", DbType.Int32, partIncidentalDetail.Quantity)
            DBCommandWrapper.AddInParameter("@Status", DbType.Int32, partIncidentalDetail.Status)
            DBCommandWrapper.AddInParameter("@StatusDetail", DbType.Int32, partIncidentalDetail.StatusDetail)
            DBCommandWrapper.AddInParameter("@ChassisNumber", DbType.AnsiString, partIncidentalDetail.ChassisNumber)
            DBCommandWrapper.AddInParameter("@AssemblyYear", DbType.AnsiString, partIncidentalDetail.AssemblyYear)
            DbCommandWrapper.AddInParameter("@Remark", DbType.AnsiString, partIncidentalDetail.Remark)
            DbCommandWrapper.AddInParameter("@SparePartMasterSubstitutionID", DbType.Int32, partIncidentalDetail.SparePartMasterSubstitutionID)
            DbCommandWrapper.AddInParameter("@PlanDate", DbType.DateTime, partIncidentalDetail.PlanDate)
            DBCommandWrapper.AddInParameter("@Reject", DbType.Int32, partIncidentalDetail.Reject)
            DBCommandWrapper.AddInParameter("@Alokasi", DbType.Int32, partIncidentalDetail.Alokasi)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, partIncidentalDetail.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, partIncidentalDetail.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@PartIncidentalHeaderID", DbType.Int32, Me.GetRefObject(partIncidentalDetail.PartIncidentalHeader))
            DbCommandWrapper.AddInParameter("@SparePartMasterID", DbType.Int32, Me.GetRefObject(partIncidentalDetail.SparePartMaster))

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

            Dim partIncidentalDetail As PartIncidentalDetail = CType(obj, PartIncidentalDetail)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, partIncidentalDetail.ID)
            DbCommandWrapper.AddInParameter("@Quantity", DbType.Int32, partIncidentalDetail.Quantity)
            DBCommandWrapper.AddInParameter("@Status", DbType.Int32, partIncidentalDetail.Status)
            DBCommandWrapper.AddInParameter("@StatusDetail", DbType.Int32, partIncidentalDetail.StatusDetail)
            DBCommandWrapper.AddInParameter("@ChassisNumber", DbType.AnsiString, partIncidentalDetail.ChassisNumber)
            DBCommandWrapper.AddInParameter("@AssemblyYear", DbType.AnsiString, partIncidentalDetail.AssemblyYear)
            DbCommandWrapper.AddInParameter("@Remark", DbType.AnsiString, partIncidentalDetail.Remark)
            DbCommandWrapper.AddInParameter("@SparePartMasterSubstitutionID", DbType.Int32, partIncidentalDetail.SparePartMasterSubstitutionID)
            DBCommandWrapper.AddInParameter("@PlanDate", DbType.DateTime, partIncidentalDetail.PlanDate)            
            DBCommandWrapper.AddInParameter("@Reject", DbType.Int32, partIncidentalDetail.Reject)
            DBCommandWrapper.AddInParameter("@Alokasi", DbType.Int32, partIncidentalDetail.Alokasi)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, partIncidentalDetail.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, partIncidentalDetail.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@PartIncidentalHeaderID", DbType.Int32, Me.GetRefObject(partIncidentalDetail.PartIncidentalHeader))
            DbCommandWrapper.AddInParameter("@SparePartMasterID", DbType.Int32, Me.GetRefObject(partIncidentalDetail.SparePartMaster))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As PartIncidentalDetail

            Dim partIncidentalDetail As PartIncidentalDetail = New PartIncidentalDetail

            partIncidentalDetail.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Quantity")) Then partIncidentalDetail.Quantity = CType(dr("Quantity"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then partIncidentalDetail.Status = CType(dr("Status"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("StatusDetail")) Then partIncidentalDetail.StatusDetail = CType(dr("StatusDetail"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("ChassisNumber")) Then partIncidentalDetail.ChassisNumber = CType(dr("ChassisNumber"), String)
            If Not dr.IsDBNull(dr.GetOrdinal("AssemblyYear")) Then partIncidentalDetail.AssemblyYear = CType(dr("AssemblyYear"), String)
            If Not dr.IsDBNull(dr.GetOrdinal("Remark")) Then partIncidentalDetail.Remark = dr("Remark").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SparePartMasterSubstitutionID")) Then partIncidentalDetail.SparePartMasterSubstitutionID = CType(dr("SparePartMasterSubstitutionID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("PlanDate")) Then partIncidentalDetail.PlanDate = CType(dr("PlanDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("Reject")) Then partIncidentalDetail.Reject = CType(dr("Reject"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Alokasi")) Then partIncidentalDetail.Alokasi = CType(dr("Alokasi"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then partIncidentalDetail.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then partIncidentalDetail.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then partIncidentalDetail.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then partIncidentalDetail.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then partIncidentalDetail.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("PartIncidentalHeaderID")) Then
                partIncidentalDetail.PartIncidentalHeader = New PartIncidentalHeader(CType(dr("PartIncidentalHeaderID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("SparePartMasterID")) Then
                partIncidentalDetail.SparePartMaster = New SparePartMaster(CType(dr("SparePartMasterID"), Integer))
            End If

            Return partIncidentalDetail

        End Function

        Private Sub SetTableName()

            If Not (GetType(PartIncidentalDetail) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(PartIncidentalDetail), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(PartIncidentalDetail).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

