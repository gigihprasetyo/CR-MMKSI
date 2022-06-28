#Region "Summary"
'// ===========================================================================
'// AUTHOR        : Mitrais Team
'// PURPOSE       : VWI_ProfileDetailFromHeaderCode Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 16/04/2018 - 15:42:23
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

    Public Class VWI_ProfileDetailFromHeaderCodeMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_RetrieveStatement As String = "up_RetrieveVWI_ProfileDetailFromHeaderCode"
        Private m_RetrieveListStatement As String = "up_RetrieveVWI_ProfileDetailFromHeaderCodeList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim vWI_ProfileDetailFromHeaderCode As VWI_ProfileDetailFromHeaderCode = Nothing
            While dr.Read

                vWI_ProfileDetailFromHeaderCode = Me.CreateObject(dr)

            End While

            Return vWI_ProfileDetailFromHeaderCode

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim vWI_ProfileDetailFromHeaderCodeList As ArrayList = New ArrayList

            While dr.Read
                Dim vWI_ProfileDetailFromHeaderCode As VWI_ProfileDetailFromHeaderCode = Me.CreateObject(dr)
                vWI_ProfileDetailFromHeaderCodeList.Add(vWI_ProfileDetailFromHeaderCode)
            End While

            Return vWI_ProfileDetailFromHeaderCodeList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Throw New NotImplementedException

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Throw New NotImplementedException

        End Function

        Protected Overrides Function GetNewID(ByVal dbCommandWrapper As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper) As Integer

            Throw New NotImplementedException

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

            Throw New NotImplementedException

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As VWI_ProfileDetailFromHeaderCode

            Dim vWI_ProfileDetailFromHeaderCode As VWI_ProfileDetailFromHeaderCode = New VWI_ProfileDetailFromHeaderCode

            vWI_ProfileDetailFromHeaderCode.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("ProfileHeaderID")) Then vWI_ProfileDetailFromHeaderCode.ProfileHeaderID = CType(dr("Status"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("ProfileHeaderCode")) Then vWI_ProfileDetailFromHeaderCode.ProfileHeaderCode = dr("ProfileHeaderCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ProfileHeaderDesc")) Then vWI_ProfileDetailFromHeaderCode.ProfileHeaderDesc = dr("ProfileHeaderDesc").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ProfileDetailCode")) Then vWI_ProfileDetailFromHeaderCode.ProfileDetailCode = dr("ProfileDetailCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ProfileDetailDesc")) Then vWI_ProfileDetailFromHeaderCode.ProfileDetailDesc = dr("ProfileDetailDesc").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then vWI_ProfileDetailFromHeaderCode.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then vWI_ProfileDetailFromHeaderCode.Status = CType(dr("Status"), Integer)

            Return vWI_ProfileDetailFromHeaderCode

        End Function

        Private Sub SetTableName()

            If Not (GetType(VWI_ProfileDetailFromHeaderCode) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(VWI_ProfileDetailFromHeaderCode), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(VWI_ProfileDetailFromHeaderCode).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace


