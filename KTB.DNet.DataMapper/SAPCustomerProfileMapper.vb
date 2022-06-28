#Region "Summary"
'// ===========================================================================
'// AUTHOR        : Dnet
'// PURPOSE       : SAPCustomerProfile Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2021 
'// ---------------------
'// $History      : $
'// Generated on 10/8/2021 - 1:36:38 PM
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

    Public Class SAPCustomerProfileMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertSAPCustomerProfile"
        Private m_UpdateStatement As String = "up_UpdateSAPCustomerProfile"
        Private m_RetrieveStatement As String = "up_RetrieveSAPCustomerProfile"
        Private m_RetrieveListStatement As String = "up_RetrieveSAPCustomerProfileList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteSAPCustomerProfile"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim sAPCustomerProfile As SAPCustomerProfile = Nothing
            While dr.Read

                sAPCustomerProfile = Me.CreateObject(dr)

            End While

            Return sAPCustomerProfile

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim sAPCustomerProfileList As ArrayList = New ArrayList

            While dr.Read
                Dim sAPCustomerProfile As SAPCustomerProfile = Me.CreateObject(dr)
                sAPCustomerProfileList.Add(sAPCustomerProfile)
            End While

            Return sAPCustomerProfileList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim sAPCustomerProfile As SAPCustomerProfile = CType(obj, SAPCustomerProfile)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, sAPCustomerProfile.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim sAPCustomerProfile As SAPCustomerProfile = CType(obj, SAPCustomerProfile)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@ProfileID", DbType.Int32, sAPCustomerProfile.ProfileID)
            DbCommandWrapper.AddInParameter("@LeadStatus", DbType.Byte, sAPCustomerProfile.LeadStatus)
            DbCommandWrapper.AddInParameter("@Value", DbType.AnsiString, sAPCustomerProfile.Value)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, sAPCustomerProfile.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, sAPCustomerProfile.LastUpdateBy)
            DbCommandWrapper.AddInParameter("@LastUpdateTime", DbType.DateTime, sAPCustomerProfile.LastUpdateTime)

            DbCommandWrapper.AddInParameter("@SAPCustomerID", DbType.Byte, Me.GetRefObject(sAPCustomerProfile.SAPCustomer))
            DbCommandWrapper.AddInParameter("@GroupID", DbType.Byte, Me.GetRefObject(sAPCustomerProfile.ProfileGroup))
            DbCommandWrapper.AddInParameter("@ProfileHeaderID", DbType.Byte, Me.GetRefObject(sAPCustomerProfile.ProfileHeader))

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

            Dim sAPCustomerProfile As SAPCustomerProfile = CType(obj, SAPCustomerProfile)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, sAPCustomerProfile.ID)
            DbCommandWrapper.AddInParameter("@ProfileID", DbType.Int32, sAPCustomerProfile.ProfileID)
            DbCommandWrapper.AddInParameter("@LeadStatus", DbType.Byte, sAPCustomerProfile.LeadStatus)
            DbCommandWrapper.AddInParameter("@Value", DbType.AnsiString, sAPCustomerProfile.Value)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, sAPCustomerProfile.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, sAPCustomerProfile.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, sAPCustomerProfile.LastUpdateBy)
            DbCommandWrapper.AddInParameter("@LastUpdateTime", DbType.DateTime, sAPCustomerProfile.LastUpdateTime)

            DbCommandWrapper.AddInParameter("@SAPCustomerID", DbType.Byte, Me.GetRefObject(sAPCustomerProfile.SAPCustomer))
            DbCommandWrapper.AddInParameter("@GroupID", DbType.Byte, Me.GetRefObject(sAPCustomerProfile.ProfileGroup))
            DbCommandWrapper.AddInParameter("@ProfileHeaderID", DbType.Byte, Me.GetRefObject(sAPCustomerProfile.ProfileHeader))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As SAPCustomerProfile

            Dim sAPCustomerProfile As SAPCustomerProfile = New SAPCustomerProfile

            sAPCustomerProfile.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("ProfileID")) Then sAPCustomerProfile.ProfileID = CType(dr("ProfileID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("LeadStatus")) Then sAPCustomerProfile.LeadStatus = CType(dr("LeadStatus"), Byte)
            If Not dr.IsDBNull(dr.GetOrdinal("Value")) Then sAPCustomerProfile.Value = dr("Value").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then sAPCustomerProfile.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then sAPCustomerProfile.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then sAPCustomerProfile.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then sAPCustomerProfile.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then sAPCustomerProfile.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("SAPCustomerID")) Then
                sAPCustomerProfile.SAPCustomer = New SAPCustomer(CType(dr("SAPCustomerID"), Byte))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("GroupID")) Then
                sAPCustomerProfile.ProfileGroup = New ProfileGroup(CType(dr("GroupID"), Byte))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("ProfileHeaderID")) Then
                sAPCustomerProfile.ProfileHeader = New ProfileHeader(CType(dr("ProfileHeaderID"), Byte))
            End If

            Return sAPCustomerProfile

        End Function

        Private Sub SetTableName()

            If Not (GetType(SAPCustomerProfile) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(SAPCustomerProfile), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(SAPCustomerProfile).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace
