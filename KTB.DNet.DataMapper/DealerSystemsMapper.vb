
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : DealerSystems Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 20/09/2018 - 14:34:38
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

    Public Class DealerSystemsMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertDealerSystems"
        Private m_UpdateStatement As String = "up_UpdateDealerSystems"
        Private m_RetrieveStatement As String = "up_RetrieveDealerSystems"
        Private m_RetrieveListStatement As String = "up_RetrieveDealerSystemsList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteDealerSystems"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim dealerSystems As DealerSystems = Nothing
            While dr.Read

                dealerSystems = Me.CreateObject(dr)

            End While

            Return dealerSystems

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim dealerSystemsList As ArrayList = New ArrayList

            While dr.Read
                Dim dealerSystems As DealerSystems = Me.CreateObject(dr)
                dealerSystemsList.Add(dealerSystems)
            End While

            Return dealerSystemsList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim dealerSystems As DealerSystems = CType(obj, DealerSystems)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, dealerSystems.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim dealerSystems As DealerSystems = CType(obj, DealerSystems)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            'DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, dealerSystems.DealerID)
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, Me.GetRefObject(dealerSystems.Dealer))
            DbCommandWrapper.AddInParameter("@SystemID", DbType.Int32, dealerSystems.SystemID)
            DbCommandWrapper.AddInParameter("@isSPKMatchFaktur", DbType.Boolean, dealerSystems.isSPKMatchFaktur)
            DbCommandWrapper.AddInParameter("@isOnlyUploadPhotoTenagaPenjual", DbType.Boolean, dealerSystems.isOnlyUploadPhotoTenagaPenjual)
            DbCommandWrapper.AddInParameter("@isSPKDNET", DbType.Boolean, dealerSystems.isSPKDNET)
            'DbCommandWrapper.AddInParameter("@isSalesFunnelValidate", DbType.Boolean, dealerSystems.isSalesFunnelValidate)
            DbCommandWrapper.AddInParameter("@GoLiveDate", DbType.Date, dealerSystems.GoLiveDate)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, dealerSystems.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, dealerSystems.LastUpdateBy)
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

            Dim dealerSystems As DealerSystems = CType(obj, DealerSystems)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, dealerSystems.ID)
            'DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, dealerSystems.DealerID)
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, Me.GetRefObject(dealerSystems.Dealer))
            DbCommandWrapper.AddInParameter("@SystemID", DbType.Int32, dealerSystems.SystemID)
            DbCommandWrapper.AddInParameter("@isSPKMatchFaktur", DbType.Boolean, dealerSystems.isSPKMatchFaktur)
            DbCommandWrapper.AddInParameter("@isOnlyUploadPhotoTenagaPenjual", DbType.Boolean, dealerSystems.isOnlyUploadPhotoTenagaPenjual)
            DbCommandWrapper.AddInParameter("@isSPKDNET", DbType.Boolean, dealerSystems.isSPKDNET)
            'DbCommandWrapper.AddInParameter("@isSalesFunnelValidate", DbType.Boolean, dealerSystems.isSalesFunnelValidate)
            DbCommandWrapper.AddInParameter("@GoLiveDate", DbType.Date, dealerSystems.GoLiveDate)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, dealerSystems.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, dealerSystems.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As DealerSystems

            Dim dealerSystems As DealerSystems = New DealerSystems

            dealerSystems.ID = CType(dr("ID"), Integer)
            'If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then dealerSystems.DealerID = CType(dr("DealerID"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("SystemID")) Then dealerSystems.SystemID = CType(dr("SystemID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("isSPKMatchFaktur")) Then dealerSystems.isSPKMatchFaktur = CType(dr("isSPKMatchFaktur"), Boolean)
            If Not dr.IsDBNull(dr.GetOrdinal("isOnlyUploadPhotoTenagaPenjual")) Then dealerSystems.isOnlyUploadPhotoTenagaPenjual = CType(dr("isOnlyUploadPhotoTenagaPenjual"), Boolean)
            'If Not dr.IsDBNull(dr.GetOrdinal("isSalesFunnelValidate")) Then dealerSystems.isSalesFunnelValidate = CType(dr("isSalesFunnelValidate"), Boolean)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then dealerSystems.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then dealerSystems.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then dealerSystems.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then dealerSystems.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then dealerSystems.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then
                dealerSystems.Dealer = New Dealer(CType(dr("DealerID"), Short))
            End If

            If Not dr.IsDBNull(dr.GetOrdinal("isSPKDNET")) Then dealerSystems.isSPKDNET = CType(dr("isSPKDNET"), Boolean)
            If Not dr.IsDBNull(dr.GetOrdinal("GoLiveDate")) Then dealerSystems.GoLiveDate = CType(dr("GoLiveDate"), Date)

            Return dealerSystems

        End Function

        Private Sub SetTableName()

            If Not (GetType(DealerSystems) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(DealerSystems), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(DealerSystems).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

