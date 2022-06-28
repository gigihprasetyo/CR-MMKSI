
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : V_DataSiswa Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2017 
'// ---------------------
'// $History      : $
'// Generated on 12/14/2017 - 1:21:56 PM
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

    Public Class V_DataSiswaMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_RetrieveStatement As String = "up_RetrieveV_DataSiswa"
        Private m_RetrieveListStatement As String = "up_RetrieveV_DataSiswaList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim v_DataSiswa As V_DataSiswa = Nothing
            While dr.Read

                v_DataSiswa = Me.CreateObject(dr)

            End While

            Return v_DataSiswa

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim v_DataSiswaList As ArrayList = New ArrayList

            While dr.Read
                Dim v_DataSiswa As V_DataSiswa = Me.CreateObject(dr)
                v_DataSiswaList.Add(v_DataSiswa)
            End While

            Return v_DataSiswaList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Return Nothing

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Return Nothing

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

            Return Nothing

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As V_DataSiswa

            Dim v_DataSiswa As V_DataSiswa = New V_DataSiswa

            v_DataSiswa.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("TanggalRegister")) Then v_DataSiswa.TanggalRegister = dr("TanggalRegister").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("TanggalRegister2")) Then v_DataSiswa.TanggalRegister2 = dr("TanggalRegister2").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Name")) Then v_DataSiswa.Name = dr("Name").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("JobPosition")) Then v_DataSiswa.JobPosition = dr("JobPosition").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DealerCode")) Then v_DataSiswa.DealerCode = dr("DealerCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DealerName")) Then v_DataSiswa.DealerName = dr("DealerName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CityName")) Then v_DataSiswa.CityName = dr("CityName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then v_DataSiswa.Status = dr("Status").ToString
            'If Not dr.IsDBNull(dr.GetOrdinal("Description")) Then v_DataSiswa.Description = dr("description").ToString
            'If Not dr.IsDBNull(dr.GetOrdinal("SalesmanCode")) Then v_DataSiswa.SalesmanCode = dr("SalesmanCode").ToString
            Return v_DataSiswa

        End Function

        Private Sub SetTableName()

            If Not (GetType(V_DataSiswa) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(V_DataSiswa), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(V_DataSiswa).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

