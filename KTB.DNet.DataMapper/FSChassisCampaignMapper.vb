
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : FSChassisCampaign Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2015 
'// ---------------------
'// $History      : $
'// Generated on 23/12/2015 - 11:20:42
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

    Public Class FSChassisCampaignMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertFSChassisCampaign"
        Private m_UpdateStatement As String = "up_UpdateFSChassisCampaign"
        Private m_RetrieveStatement As String = "up_RetrieveFSChassisCampaign"
        Private m_RetrieveListStatement As String = "up_RetrieveFSChassisCampaignList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteFSChassisCampaign"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim fSChassisCampaign As FSChassisCampaign = Nothing
            While dr.Read

                fSChassisCampaign = Me.CreateObject(dr)

            End While

            Return fSChassisCampaign

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim fSChassisCampaignList As ArrayList = New ArrayList

            While dr.Read
                Dim fSChassisCampaign As FSChassisCampaign = Me.CreateObject(dr)
                fSChassisCampaignList.Add(fSChassisCampaign)
            End While

            Return fSChassisCampaignList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim fSChassisCampaign As FSChassisCampaign = CType(obj, FSChassisCampaign)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, fSChassisCampaign.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim fSChassisCampaign As FSChassisCampaign = CType(obj, FSChassisCampaign)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@Remarks", DbType.AnsiString, fSChassisCampaign.Remarks)
            DbCommandWrapper.AddInParameter("@IsAllow", DbType.Boolean, fSChassisCampaign.IsAllow)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, fSChassisCampaign.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, fSChassisCampaign.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@ChassisMasterID", DbType.Int32, Me.GetRefObject(fSChassisCampaign.ChassisMaster))
            DbCommandWrapper.AddInParameter("@FSKindID", DbType.Int32, Me.GetRefObject(fSChassisCampaign.FSKind))

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

            Dim fSChassisCampaign As FSChassisCampaign = CType(obj, FSChassisCampaign)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, fSChassisCampaign.ID)
            DbCommandWrapper.AddInParameter("@Remarks", DbType.AnsiString, fSChassisCampaign.Remarks)
            DbCommandWrapper.AddInParameter("@IsAllow", DbType.Boolean, fSChassisCampaign.IsAllow)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, fSChassisCampaign.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, fSChassisCampaign.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@ChassisMasterID", DbType.Int32, Me.GetRefObject(fSChassisCampaign.ChassisMaster))
            DbCommandWrapper.AddInParameter("@FSKindID", DbType.Int32, Me.GetRefObject(fSChassisCampaign.FSKind))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As FSChassisCampaign

            Dim fSChassisCampaign As FSChassisCampaign = New FSChassisCampaign

            fSChassisCampaign.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("IsAllow")) Then fSChassisCampaign.IsAllow = CType(dr("IsAllow"), Boolean)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then fSChassisCampaign.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("Remarks")) Then fSChassisCampaign.Remarks = dr("Remarks").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then fSChassisCampaign.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then fSChassisCampaign.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then fSChassisCampaign.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then fSChassisCampaign.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            If Not dr.IsDBNull(dr.GetOrdinal("ChassisMasterID")) Then
                fSChassisCampaign.ChassisMaster = New ChassisMaster(CType(dr("ChassisMasterID"), Integer))
            End If
             
            If Not dr.IsDBNull(dr.GetOrdinal("FSKindID")) Then
                fSChassisCampaign.FSKind = New FSKind(CType(dr("FSKindID"), Integer))
            End If

            Return fSChassisCampaign

        End Function

        Private Sub SetTableName()

            If Not (GetType(FSChassisCampaign) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(FSChassisCampaign), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(FSChassisCampaign).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

