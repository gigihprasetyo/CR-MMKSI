#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : WarrantyActivation Objects Mapper.
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

    Public Class WarrantyActivationMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertWarrantyActivation"
        Private m_UpdateStatement As String = "up_UpdateWarrantyActivation"
        Private m_RetrieveStatement As String = "up_RetrieveWarrantyActivation"
        Private m_RetrieveListStatement As String = "up_RetrieveWarrantyActivationList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteWarrantyActivation"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim WarrantyActivation As WarrantyActivation = Nothing
            While dr.Read

                WarrantyActivation = Me.CreateObject(dr)

            End While

            Return WarrantyActivation

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim WarrantyActivationList As ArrayList = New ArrayList

            While dr.Read
                Dim WarrantyActivation As WarrantyActivation = Me.CreateObject(dr)
                WarrantyActivationList.Add(WarrantyActivation)
            End While

            Return WarrantyActivationList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim WarrantyActivation As WarrantyActivation = CType(obj, WarrantyActivation)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, WarrantyActivation.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim WarrantyActivation As WarrantyActivation = CType(obj, WarrantyActivation)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, WarrantyActivation.Status)
            DbCommandWrapper.AddInParameter("@WADate", DbType.DateTime, WarrantyActivation.WADate)
            DbCommandWrapper.AddInParameter("@FileName", DbType.AnsiString, WarrantyActivation.FileName)
            DbCommandWrapper.AddInParameter("@DSFilePath", DbType.AnsiString, WarrantyActivation.DSFilePath)
            DbCommandWrapper.AddInParameter("@CustomerName", DbType.AnsiString, WarrantyActivation.CustomerName)
            DbCommandWrapper.AddInParameter("@HandphoneNo", DbType.AnsiString, WarrantyActivation.HandphoneNo)
            DbCommandWrapper.AddInParameter("@PlateNumber", DbType.AnsiString, WarrantyActivation.PlateNumber)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, WarrantyActivation.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdatedBy", DbType.AnsiString, WarrantyActivation.LastUpdatedBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@ChassisMasterID", DbType.Int32, Me.GetRefObject(WarrantyActivation.ChassisMaster))
            DbCommandWrapper.AddInParameter("@PDIID", DbType.Int32, Me.GetRefObject(WarrantyActivation.PDI))
            DbCommandWrapper.AddInParameter("@ChassisMasterPKTID", DbType.Int32, Me.GetRefObject(WarrantyActivation.ChassisMasterPKT))

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

            Dim WarrantyActivation As WarrantyActivation = CType(obj, WarrantyActivation)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, WarrantyActivation.ID)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, WarrantyActivation.Status)
            DbCommandWrapper.AddInParameter("@WADate", DbType.DateTime, WarrantyActivation.WADate)
            DbCommandWrapper.AddInParameter("@FileName", DbType.AnsiString, WarrantyActivation.FileName)
            DbCommandWrapper.AddInParameter("@DSFilePath", DbType.AnsiString, WarrantyActivation.DSFilePath)
            DbCommandWrapper.AddInParameter("@CustomerName", DbType.AnsiString, WarrantyActivation.CustomerName)
            DbCommandWrapper.AddInParameter("@HandphoneNo", DbType.AnsiString, WarrantyActivation.HandphoneNo)
            DbCommandWrapper.AddInParameter("@PlateNumber", DbType.AnsiString, WarrantyActivation.PlateNumber)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, WarrantyActivation.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, WarrantyActivation.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@ChassisMasterID", DbType.Int32, Me.GetRefObject(WarrantyActivation.ChassisMaster))
            DbCommandWrapper.AddInParameter("@PDIID", DbType.Int32, Me.GetRefObject(WarrantyActivation.PDI))
            DbCommandWrapper.AddInParameter("@ChassisMasterPKTID", DbType.Int32, Me.GetRefObject(WarrantyActivation.ChassisMasterPKT))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As WarrantyActivation

            Dim WarrantyActivation As WarrantyActivation = New WarrantyActivation

            WarrantyActivation.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then WarrantyActivation.Status = CType(dr("Status"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("WADate")) Then WarrantyActivation.WADate = CType(dr("WADate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("FileName")) Then WarrantyActivation.FileName = dr("FileName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DSFilePath")) Then WarrantyActivation.DSFilePath = dr("DSFilePath").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CustomerName")) Then WarrantyActivation.CustomerName = dr("CustomerName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("HandphoneNo")) Then WarrantyActivation.HandphoneNo = dr("HandphoneNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PlateNumber")) Then WarrantyActivation.PlateNumber = dr("PlateNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then WarrantyActivation.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then WarrantyActivation.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then WarrantyActivation.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdatedBy")) Then WarrantyActivation.LastUpdatedBy = dr("LastUpdatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdatedTime")) Then WarrantyActivation.LastUpdatedTime = CType(dr("LastUpdatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("ChassisMasterID")) Then
                WarrantyActivation.ChassisMaster = New ChassisMaster(CType(dr("ChassisMasterID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("PDIID")) Then
                WarrantyActivation.PDI = New PDI(CType(dr("PDIID"), Integer))
            End If

            If Not dr.IsDBNull(dr.GetOrdinal("ChassisMasterPKTID")) Then
                WarrantyActivation.ChassisMasterPKT = New ChassisMasterPKT(CType(dr("ChassisMasterPKTID"), Integer))
            End If

            Return WarrantyActivation

        End Function

        Private Sub SetTableName()

            If Not (GetType(WarrantyActivation) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(WarrantyActivation), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(WarrantyActivation).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

