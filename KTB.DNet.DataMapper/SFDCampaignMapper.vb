#Region "Summary"
'// ===========================================================================
'// AUTHOR        : Dnet
'// PURPOSE       : SFDCampaign Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2021 
'// ---------------------
'// $History      : $
'// Generated on 10/27/2021 - 4:57:32 PM
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

    Public Class SFDCampaignMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertSFDCampaign"
        Private m_UpdateStatement As String = "up_UpdateSFDCampaign"
        Private m_RetrieveStatement As String = "up_RetrieveSFDCampaign"
        Private m_RetrieveListStatement As String = "up_RetrieveSFDCampaignList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteSFDCampaign"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim sFDCampaign As SFDCampaign = Nothing
            While dr.Read

                sFDCampaign = Me.CreateObject(dr)

            End While

            Return sFDCampaign

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim sFDCampaignList As ArrayList = New ArrayList

            While dr.Read
                Dim sFDCampaign As SFDCampaign = Me.CreateObject(dr)
                sFDCampaignList.Add(sFDCampaign)
            End While

            Return sFDCampaignList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim sFDCampaign As SFDCampaign = CType(obj, SFDCampaign)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int64, sFDCampaign.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim sFDCampaign As SFDCampaign = CType(obj, SFDCampaign)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int64, 8)
            DbCommandWrapper.AddInParameter("@DealerBranchID", DbType.Int16, sFDCampaign.DealerBranchID)
            DbCommandWrapper.AddInParameter("@GUID", DbType.AnsiString, sFDCampaign.GUID)
            DbCommandWrapper.AddInParameter("@CampaignName", DbType.AnsiString, sFDCampaign.CampaignName)
            DbCommandWrapper.AddInParameter("@CampaignCode", DbType.AnsiString, sFDCampaign.CampaignCode)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, sFDCampaign.Status)
            DbCommandWrapper.AddInParameter("@LastUpdateinDMS", DbType.DateTime, sFDCampaign.LastUpdateinDMS)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, sFDCampaign.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, sFDCampaign.LastUpdateBy)
            DbCommandWrapper.AddInParameter("@LastUpdateTime", DbType.DateTime, sFDCampaign.LastUpdateTime)

            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, Me.GetRefObject(sFDCampaign.Dealer))

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

            Dim sFDCampaign As SFDCampaign = CType(obj, SFDCampaign)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int64, sFDCampaign.ID)
            DbCommandWrapper.AddInParameter("@DealerBranchID", DbType.Int16, sFDCampaign.DealerBranchID)
            DbCommandWrapper.AddInParameter("@GUID", DbType.AnsiString, sFDCampaign.GUID)
            DbCommandWrapper.AddInParameter("@CampaignName", DbType.AnsiString, sFDCampaign.CampaignName)
            DbCommandWrapper.AddInParameter("@CampaignCode", DbType.AnsiString, sFDCampaign.CampaignCode)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, sFDCampaign.Status)
            DbCommandWrapper.AddInParameter("@LastUpdateinDMS", DbType.DateTime, sFDCampaign.LastUpdateinDMS)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, sFDCampaign.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, sFDCampaign.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, sFDCampaign.LastUpdateBy)
            DbCommandWrapper.AddInParameter("@LastUpdateTime", DbType.DateTime, sFDCampaign.LastUpdateTime)


            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, Me.GetRefObject(sFDCampaign.Dealer))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As SFDCampaign

            Dim sFDCampaign As SFDCampaign = New SFDCampaign

            sFDCampaign.ID = CType(dr("ID"), Long)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerBranchID")) Then sFDCampaign.DealerBranchID = CType(dr("DealerBranchID"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("GUID")) Then sFDCampaign.GUID = dr("GUID").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CampaignName")) Then sFDCampaign.CampaignName = dr("CampaignName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CampaignCode")) Then sFDCampaign.CampaignCode = dr("CampaignCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then sFDCampaign.Status = CType(dr("Status"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateinDMS")) Then sFDCampaign.LastUpdateinDMS = CType(dr("LastUpdateinDMS"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then sFDCampaign.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then sFDCampaign.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then sFDCampaign.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then sFDCampaign.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then sFDCampaign.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then
                sFDCampaign.Dealer = New Dealer(CType(dr("DealerID"), Short))
            End If

            Return sFDCampaign

        End Function

        Private Sub SetTableName()

            If Not (GetType(SFDCampaign) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(SFDCampaign), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(SFDCampaign).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace
