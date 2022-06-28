#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : CustomerProfile Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2007 
'// ---------------------
'// $History      : $
'// Generated on 7/23/2007 - 10:30:33 AM
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

    Public Class CustomerProfileMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertCustomerProfile"
        Private m_UpdateStatement As String = "up_UpdateCustomerProfile"
        Private m_RetrieveStatement As String = "up_RetrieveCustomerProfile"
        Private m_RetrieveListStatement As String = "up_RetrieveCustomerProfileList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteCustomerProfile"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim customerProfile As CustomerProfile = Nothing
            While dr.Read

                customerProfile = Me.CreateObject(dr)

            End While

            Return customerProfile

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim customerProfileList As ArrayList = New ArrayList

            While dr.Read
                Dim customerProfile As CustomerProfile = Me.CreateObject(dr)
                customerProfileList.Add(customerProfile)
            End While

            Return customerProfileList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim customerProfile As CustomerProfile = CType(obj, CustomerProfile)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, customerProfile.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim customerProfile As CustomerProfile = CType(obj, CustomerProfile)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@ProfileValue", DbType.AnsiString, customerProfile.ProfileValue)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, customerProfile.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, customerProfile.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@ProfileHeaderID", DbType.Int32, Me.GetRefObject(customerProfile.ProfileHeader))
            DbCommandWrapper.AddInParameter("@GroupID", DbType.Int32, Me.GetRefObject(customerProfile.ProfileGroup))
            DbCommandWrapper.AddInParameter("@CustomerID", DbType.Int32, Me.GetRefObject(customerProfile.Customer))

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

            Dim customerProfile As CustomerProfile = CType(obj, CustomerProfile)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, customerProfile.ID)
            DbCommandWrapper.AddInParameter("@ProfileValue", DbType.AnsiString, customerProfile.ProfileValue)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, customerProfile.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, customerProfile.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@ProfileHeaderID", DbType.Int32, Me.GetRefObject(customerProfile.ProfileHeader))
            DbCommandWrapper.AddInParameter("@GroupID", DbType.Int32, Me.GetRefObject(customerProfile.ProfileGroup))
            DbCommandWrapper.AddInParameter("@CustomerID", DbType.Int32, Me.GetRefObject(customerProfile.Customer))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As CustomerProfile

            Dim customerProfile As CustomerProfile = New CustomerProfile

            customerProfile.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("ProfileValue")) Then customerProfile.ProfileValue = dr("ProfileValue").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then customerProfile.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then customerProfile.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then customerProfile.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then customerProfile.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then customerProfile.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("ProfileHeaderID")) Then
                customerProfile.ProfileHeader = New ProfileHeader(CType(dr("ProfileHeaderID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("GroupID")) Then
                customerProfile.ProfileGroup = New ProfileGroup(CType(dr("GroupID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("CustomerID")) Then
                customerProfile.Customer = New Customer(CType(dr("CustomerID"), Integer))
            End If

            Return customerProfile

        End Function

        Private Sub SetTableName()

            If Not (GetType(CustomerProfile) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(CustomerProfile), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(CustomerProfile).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

