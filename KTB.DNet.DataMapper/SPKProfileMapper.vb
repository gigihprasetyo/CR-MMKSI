
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : SPKProfile Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2011 
'// ---------------------
'// $History      : $
'// Generated on 3/7/2011 - 8:40:03 AM
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

    Public Class SPKProfileMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertSPKProfile"
        Private m_UpdateStatement As String = "up_UpdateSPKProfile"
        Private m_RetrieveStatement As String = "up_RetrieveSPKProfile"
        Private m_RetrieveListStatement As String = "up_RetrieveSPKProfileList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteSPKProfile"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim sPKProfile As SPKProfile = Nothing
            While dr.Read

                sPKProfile = Me.CreateObject(dr)

            End While

            Return sPKProfile

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim sPKProfileList As ArrayList = New ArrayList

            While dr.Read
                Dim sPKProfile As SPKProfile = Me.CreateObject(dr)
                sPKProfileList.Add(sPKProfile)
            End While

            Return sPKProfileList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim sPKProfile As SPKProfile = CType(obj, SPKProfile)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, sPKProfile.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim sPKProfile As SPKProfile = CType(obj, SPKProfile)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@ProfileValue", DbType.AnsiString, sPKProfile.ProfileValue)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, sPKProfile.RowStatus)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.String, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.String, sPKProfile.LastUpdateBy)

            DbCommandWrapper.AddInParameter("@ProfileHeaderID", DbType.Int32, Me.GetRefObject(sPKProfile.ProfileHeader))
            DbCommandWrapper.AddInParameter("@GroupID", DbType.Int32, Me.GetRefObject(sPKProfile.ProfileGroup))
            DbCommandWrapper.AddInParameter("@SPKDetailID", DbType.Int32, Me.GetRefObject(sPKProfile.SPKDetail))
            DbCommandWrapper.AddInParameter("@SPKDetailCustomerID", DbType.Int32, Me.GetRefObject(sPKProfile.SPKDetailCustomer))

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

            Dim sPKProfile As SPKProfile = CType(obj, SPKProfile)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, sPKProfile.ID)
            DbCommandWrapper.AddInParameter("@ProfileValue", DbType.AnsiString, sPKProfile.ProfileValue)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, sPKProfile.RowStatus)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.String, sPKProfile.CreatedBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.String, User)


            DbCommandWrapper.AddInParameter("@ProfileHeaderID", DbType.Int32, Me.GetRefObject(sPKProfile.ProfileHeader))
            DbCommandWrapper.AddInParameter("@GroupID", DbType.Int32, Me.GetRefObject(sPKProfile.ProfileGroup))
            DbCommandWrapper.AddInParameter("@SPKDetailID", DbType.Int32, Me.GetRefObject(sPKProfile.SPKDetail))
            DbCommandWrapper.AddInParameter("@SPKDetailCustomerID", DbType.Int32, Me.GetRefObject(sPKProfile.SPKDetailCustomer))
            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As SPKProfile

            Dim sPKProfile As SPKProfile = New SPKProfile

            sPKProfile.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("ProfileValue")) Then sPKProfile.ProfileValue = dr("ProfileValue").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then sPKProfile.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then sPKProfile.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then sPKProfile.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then sPKProfile.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then sPKProfile.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ProfileHeaderID")) Then
                sPKProfile.ProfileHeader = New ProfileHeader(CType(dr("ProfileHeaderID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("GroupID")) Then
                sPKProfile.ProfileGroup = New ProfileGroup(CType(dr("GroupID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("SPKDetailID")) Then
                sPKProfile.SPKDetail = New SPKDetail(CType(dr("SPKDetailID"), Integer))
            End If

            If Not dr.IsDBNull(dr.GetOrdinal("SPKDetailCustomerID")) Then
                sPKProfile.SPKDetailCustomer = New SPKDetailCustomer(CType(dr("SPKDetailCustomerID"), Integer))
            End If

            If Not dr.IsDBNull(dr.GetOrdinal("SPKDetailCustomerID")) Then
                sPKProfile.SPKDetailCustomerID = CInt(dr.GetOrdinal("SPKDetailCustomerID"))
            End If


            Return sPKProfile

        End Function

        Private Sub SetTableName()

            If Not (GetType(SPKProfile) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(SPKProfile), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(SPKProfile).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

