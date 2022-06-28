
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : VWI_BusinessSector Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 22/06/2018 - 14:08:39
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

    Public Class VWI_BusinessSectorMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertVWI_BusinessSector"
        Private m_UpdateStatement As String = "up_UpdateVWI_BusinessSector"
        Private m_RetrieveStatement As String = "up_RetrieveVWI_BusinessSector"
        Private m_RetrieveListStatement As String = "up_RetrieveVWI_BusinessSectorList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteVWI_BusinessSector"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim vWI_BusinessSector2 As VWI_BusinessSector = Nothing
            While dr.Read

                vWI_BusinessSector2 = Me.CreateObject(dr)

            End While

            Return vWI_BusinessSector2

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim vWI_BusinessSector2List As ArrayList = New ArrayList

            While dr.Read
                Dim vWI_BusinessSector2 As VWI_BusinessSector = Me.CreateObject(dr)
                vWI_BusinessSector2List.Add(vWI_BusinessSector2)
            End While

            Return vWI_BusinessSector2List

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim vWI_BusinessSector2 As VWI_BusinessSector = CType(obj, VWI_BusinessSector)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, vWI_BusinessSector2.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim vWI_BusinessSector2 As VWI_BusinessSector = CType(obj, VWI_BusinessSector)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@BusinessSectorName", DbType.AnsiString, vWI_BusinessSector2.BusinessSectorName)
            DbCommandWrapper.AddInParameter("@BusinessDomain", DbType.AnsiString, vWI_BusinessSector2.BusinessDomain)
            DbCommandWrapper.AddInParameter("@BusinessName", DbType.AnsiString, vWI_BusinessSector2.BusinessName)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, vWI_BusinessSector2.Status)
            DbCommandWrapper.AddInParameter("@LastUpdateTime", DbType.DateTime, DateTime.Now)

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

            Dim vWI_BusinessSector2 As VWI_BusinessSector = CType(obj, VWI_BusinessSector)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, vWI_BusinessSector2.ID)
            DbCommandWrapper.AddInParameter("@BusinessSectorName", DbType.AnsiString, vWI_BusinessSector2.BusinessSectorName)
            DbCommandWrapper.AddInParameter("@BusinessDomain", DbType.AnsiString, vWI_BusinessSector2.BusinessDomain)
            DbCommandWrapper.AddInParameter("@BusinessName", DbType.AnsiString, vWI_BusinessSector2.BusinessName)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, vWI_BusinessSector2.Status)
            DbCommandWrapper.AddInParameter("@LastUpdateTime", DbType.DateTime, DateTime.Now)


            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As VWI_BusinessSector

            Dim vWI_BusinessSector2 As VWI_BusinessSector = New VWI_BusinessSector

            vWI_BusinessSector2.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("BusinessSectorName")) Then vWI_BusinessSector2.BusinessSectorName = dr("BusinessSectorName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("BusinessDomain")) Then vWI_BusinessSector2.BusinessDomain = dr("BusinessDomain").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("BusinessName")) Then vWI_BusinessSector2.BusinessName = dr("BusinessName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then vWI_BusinessSector2.Status = CType(dr("Status"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then vWI_BusinessSector2.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            Return vWI_BusinessSector2

        End Function

        Private Sub SetTableName()

            If Not (GetType(VWI_BusinessSector) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(VWI_BusinessSector), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(VWI_BusinessSector).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

