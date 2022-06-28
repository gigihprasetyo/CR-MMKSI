
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : SparePartPOEstimate Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2015 
'// ---------------------
'// $History      : $
'// Generated on 10/27/2015 - 3:26:04 PM
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

    Public Class SparePartPOEstimateMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertSparePartPOEstimate"
        Private m_UpdateStatement As String = "up_UpdateSparePartPOEstimate"
        Private m_RetrieveStatement As String = "up_RetrieveSparePartPOEstimate"
        Private m_RetrieveListStatement As String = "up_RetrieveSparePartPOEstimateList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteSparePartPOEstimate"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim sparePartPOEstimate As SparePartPOEstimate = Nothing
            While dr.Read

                sparePartPOEstimate = Me.CreateObject(dr)

            End While

            Return sparePartPOEstimate

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim sparePartPOEstimateList As ArrayList = New ArrayList

            While dr.Read
                Dim sparePartPOEstimate As SparePartPOEstimate = Me.CreateObject(dr)
                sparePartPOEstimateList.Add(sparePartPOEstimate)
            End While

            Return sparePartPOEstimateList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim sparePartPOEstimate As SparePartPOEstimate = CType(obj, SparePartPOEstimate)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, sparePartPOEstimate.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim sparePartPOEstimate As SparePartPOEstimate = CType(obj, SparePartPOEstimate)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@SONumber", DbType.AnsiString, sparePartPOEstimate.SONumber)
            DbCommandWrapper.AddInParameter("@SODate", DbType.DateTime, sparePartPOEstimate.SODate)
            DbCommandWrapper.AddInParameter("@DeliveryDate", DbType.DateTime, sparePartPOEstimate.DeliveryDate)
            DbCommandWrapper.AddInParameter("@DocumentType", DbType.AnsiString, sparePartPOEstimate.DocumentType)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, sparePartPOEstimate.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, sparePartPOEstimate.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@SparePartPOID", DbType.Int32, Me.GetRefObject(sparePartPOEstimate.SparePartPO))

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

            Dim sparePartPOEstimate As SparePartPOEstimate = CType(obj, SparePartPOEstimate)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, sparePartPOEstimate.ID)
            DbCommandWrapper.AddInParameter("@SONumber", DbType.AnsiString, sparePartPOEstimate.SONumber)
            DbCommandWrapper.AddInParameter("@SODate", DbType.DateTime, sparePartPOEstimate.SODate)
            DbCommandWrapper.AddInParameter("@DeliveryDate", DbType.DateTime, sparePartPOEstimate.DeliveryDate)
            DbCommandWrapper.AddInParameter("@DocumentType", DbType.AnsiString, sparePartPOEstimate.DocumentType)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, sparePartPOEstimate.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, sparePartPOEstimate.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@SparePartPOID", DbType.Int32, Me.GetRefObject(sparePartPOEstimate.SparePartPO))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As SparePartPOEstimate

            Dim sparePartPOEstimate As SparePartPOEstimate = New SparePartPOEstimate

            sparePartPOEstimate.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("SONumber")) Then sparePartPOEstimate.SONumber = dr("SONumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SODate")) Then sparePartPOEstimate.SODate = CType(dr("SODate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("DeliveryDate")) Then sparePartPOEstimate.DeliveryDate = CType(dr("DeliveryDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("DocumentType")) Then sparePartPOEstimate.DocumentType = dr("DocumentType").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then sparePartPOEstimate.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then sparePartPOEstimate.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then sparePartPOEstimate.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then sparePartPOEstimate.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then sparePartPOEstimate.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("SparePartPOID")) Then
                sparePartPOEstimate.SparePartPO = New SparePartPO(CType(dr("SparePartPOID"), Integer))
            End If

            Return sparePartPOEstimate

        End Function

        Private Sub SetTableName()

            If Not (GetType(SparePartPOEstimate) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(SparePartPOEstimate), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(SparePartPOEstimate).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

