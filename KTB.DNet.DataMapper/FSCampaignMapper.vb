
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : FSCampaign Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2010 
'// ---------------------
'// $History      : $
'// Generated on 9/6/2010 - 8:25:10 AM
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

    Public Class FSCampaignMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertFSCampaign"
        Private m_UpdateStatement As String = "up_UpdateFSCampaign"
        Private m_RetrieveStatement As String = "up_RetrieveFSCampaign"
        Private m_RetrieveListStatement As String = "up_RetrieveFSCampaignList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteFSCampaign"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim fSCampaign As FSCampaign = Nothing
            While dr.Read

                fSCampaign = Me.CreateObject(dr)

            End While

            Return fSCampaign

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim fSCampaignList As ArrayList = New ArrayList

            While dr.Read
                Dim fSCampaign As FSCampaign = Me.CreateObject(dr)
                fSCampaignList.Add(fSCampaign)
            End While

            Return fSCampaignList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim fSCampaign As FSCampaign = CType(obj, FSCampaign)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, fSCampaign.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim fSCampaign As FSCampaign = CType(obj, FSCampaign)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@Description", DbType.String, fSCampaign.Description)
            DbCommandWrapper.AddInParameter("@ErrMessage", DbType.String, fSCampaign.ErrMessage)
            DbCommandWrapper.AddInParameter("@PKTDateFrom", DbType.DateTime, fSCampaign.PktDateFrom)
            DbCommandWrapper.AddInParameter("@PKTDateTo", DbType.DateTime, fSCampaign.pktDateTo)
            DbCommandWrapper.AddInParameter("@DateFrom", DbType.DateTime, fSCampaign.DateFrom)
            DbCommandWrapper.AddInParameter("@DateTo", DbType.DateTime, fSCampaign.DateTo)
            DbCommandWrapper.AddInParameter("@DealerChecked", DbType.Boolean, fSCampaign.DealerChecked)
            DbCommandWrapper.AddInParameter("@FSTypeChecked", DbType.Boolean, fSCampaign.FSTypeChecked)
            DbCommandWrapper.AddInParameter("@VehicleTypeChecked", DbType.Boolean, fSCampaign.VehicleTypeChecked)
            DbCommandWrapper.AddInParameter("@FakturDateChecked", DbType.Boolean, fSCampaign.FakturDateChecked)
            DbCommandWrapper.AddInParameter("@PKTDateChecked", DbType.Boolean, fSCampaign.PKTDateChecked)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, fSCampaign.Status)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, fSCampaign.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, fSCampaign.LastUpdateBy)


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

            Dim fSCampaign As FSCampaign = CType(obj, FSCampaign)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, fSCampaign.ID)
            DbCommandWrapper.AddInParameter("@Description", DbType.String, fSCampaign.Description)
            DbCommandWrapper.AddInParameter("@ErrMessage", DbType.String, fSCampaign.ErrMessage)
            DbCommandWrapper.AddInParameter("@PKTDateFrom", DbType.DateTime, fSCampaign.PKTDateFrom)
            DbCommandWrapper.AddInParameter("@PKTDateTo", DbType.DateTime, fSCampaign.PKTDateTo)
            DbCommandWrapper.AddInParameter("@DateFrom", DbType.DateTime, fSCampaign.DateFrom)
            DbCommandWrapper.AddInParameter("@DateTo", DbType.DateTime, fSCampaign.DateTo)
            DbCommandWrapper.AddInParameter("@DealerChecked", DbType.Boolean, fSCampaign.DealerChecked)
            DbCommandWrapper.AddInParameter("@FSTypeChecked", DbType.Boolean, fSCampaign.FSTypeChecked)
            DbCommandWrapper.AddInParameter("@VehicleTypeChecked", DbType.Boolean, fSCampaign.VehicleTypeChecked)
            DbCommandWrapper.AddInParameter("@FakturDateChecked", DbType.Boolean, fSCampaign.FakturDateChecked)
            DbCommandWrapper.AddInParameter("@PKTDateChecked", DbType.Boolean, fSCampaign.PKTDateChecked)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, fSCampaign.Status)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, fSCampaign.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, fSCampaign.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As FSCampaign

            Dim fSCampaign As FSCampaign = New FSCampaign

            fSCampaign.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Description")) Then fSCampaign.Description = dr("Description").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ErrMessage")) Then fSCampaign.ErrMessage = dr("ErrMessage").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DateFrom")) Then fSCampaign.DateFrom = CType(dr("DateFrom"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("DateTo")) Then fSCampaign.DateTo = CType(dr("DateTo"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("PKTDateFrom")) Then fSCampaign.PKTDateFrom = CType(dr("PKTDateFrom"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("PKTDateTo")) Then fSCampaign.PKTDateTo = CType(dr("PKTDateTo"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerChecked")) Then fSCampaign.DealerChecked = CType(dr("DealerChecked"), Boolean)
            If Not dr.IsDBNull(dr.GetOrdinal("FSTypeChecked")) Then fSCampaign.FSTypeChecked = CType(dr("FSTypeChecked"), Boolean)
            If Not dr.IsDBNull(dr.GetOrdinal("VehicleTypeChecked")) Then fSCampaign.VehicleTypeChecked = CType(dr("VehicleTypeChecked"), Boolean)
            If Not dr.IsDBNull(dr.GetOrdinal("FakturDateChecked")) Then fSCampaign.FakturDateChecked = CType(dr("FakturDateChecked"), Boolean)
            If Not dr.IsDBNull(dr.GetOrdinal("PKTDateChecked")) Then fSCampaign.PKTDateChecked = CType(dr("PKTDateChecked"), Boolean)
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then fSCampaign.Status = CType(dr("Status"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then fSCampaign.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then fSCampaign.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then fSCampaign.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then fSCampaign.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then fSCampaign.LastUpdateBy = dr("LastUpdateBy").ToString

            Return fSCampaign

        End Function

        Private Sub SetTableName()

            If Not (GetType(FSCampaign) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(FSCampaign), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(FSCampaign).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

