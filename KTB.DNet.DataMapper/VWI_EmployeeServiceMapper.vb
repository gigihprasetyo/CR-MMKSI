
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : Mitrais Team
'// PURPOSE       : VWI_EmployeeService Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 23/04/2018 - 15:45:08
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

    Public Class VWI_EmployeeServiceMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertVWI_EmployeeMechanic"
        Private m_UpdateStatement As String = "up_UpdateVWI_EmployeeMechanic"
        Private m_RetrieveStatement As String = "up_RetrieveVWI_EmployeeMechanic"
        Private m_RetrieveListStatement As String = "up_RetrieveVWI_EmployeeMechanicList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteVWI_EmployeeMechanic"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim vWI_EmployeeMechanic As VWI_EmployeeService = Nothing
            While dr.Read

                vWI_EmployeeMechanic = Me.CreateObject(dr)

            End While

            Return vWI_EmployeeMechanic

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim vWI_EmployeeMechanicList As ArrayList = New ArrayList

            While dr.Read
                Dim vWI_EmployeeMechanic As VWI_EmployeeService = Me.CreateObject(dr)
                vWI_EmployeeMechanicList.Add(vWI_EmployeeMechanic)
            End While

            Return vWI_EmployeeMechanicList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim vWI_EmployeeMechanic As VWI_EmployeeService = CType(obj, VWI_EmployeeService)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, vWI_EmployeeMechanic.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim vWI_EmployeeMechanic As VWI_EmployeeService = CType(obj, VWI_EmployeeService)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@Name", DbType.AnsiString, vWI_EmployeeMechanic.Name)
            DbCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, vWI_EmployeeMechanic.DealerCode)
            DbCommandWrapper.AddInParameter("@BirthDate", DbType.DateTime, vWI_EmployeeMechanic.BirthDate)
            DbCommandWrapper.AddInParameter("@Gender", DbType.Int16, vWI_EmployeeMechanic.Gender)
            DbCommandWrapper.AddInParameter("@StartWorkingDate", DbType.DateTime, vWI_EmployeeMechanic.StartWorkingDate)
            DbCommandWrapper.AddInParameter("@JobPosition", DbType.AnsiString, vWI_EmployeeMechanic.JobPosition)
            DbCommandWrapper.AddInParameter("@EducationLevel", DbType.AnsiString, vWI_EmployeeMechanic.EducationLevel)
            DbCommandWrapper.AddInParameter("@Photo", DbType.Binary, vWI_EmployeeMechanic.Photo)
            DbCommandWrapper.AddInParameter("@ShirtSize", DbType.AnsiString, vWI_EmployeeMechanic.ShirtSize)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int32, vWI_EmployeeMechanic.Status)
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

            Dim vWI_EmployeeMechanic As VWI_EmployeeService = CType(obj, VWI_EmployeeService)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, vWI_EmployeeMechanic.ID)
            DbCommandWrapper.AddInParameter("@Name", DbType.AnsiString, vWI_EmployeeMechanic.Name)
            DbCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, vWI_EmployeeMechanic.DealerCode)
            DbCommandWrapper.AddInParameter("@BirthDate", DbType.DateTime, vWI_EmployeeMechanic.BirthDate)
            DbCommandWrapper.AddInParameter("@Gender", DbType.Int16, vWI_EmployeeMechanic.Gender)
            DbCommandWrapper.AddInParameter("@StartWorkingDate", DbType.DateTime, vWI_EmployeeMechanic.StartWorkingDate)
            DbCommandWrapper.AddInParameter("@JobPosition", DbType.AnsiString, vWI_EmployeeMechanic.JobPosition)
            DbCommandWrapper.AddInParameter("@EducationLevel", DbType.AnsiString, vWI_EmployeeMechanic.EducationLevel)
            DbCommandWrapper.AddInParameter("@Photo", DbType.Binary, vWI_EmployeeMechanic.Photo)
            DbCommandWrapper.AddInParameter("@ShirtSize", DbType.AnsiString, vWI_EmployeeMechanic.ShirtSize)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int32, vWI_EmployeeMechanic.Status)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As VWI_EmployeeService

            Dim vWI_EmployeeMechanic As VWI_EmployeeService = New VWI_EmployeeService

            vWI_EmployeeMechanic.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Name")) Then vWI_EmployeeMechanic.Name = dr("Name").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DealerCode")) Then vWI_EmployeeMechanic.DealerCode = dr("DealerCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DealerBranchCode")) Then vWI_EmployeeMechanic.DealerBranchCode = dr("DealerBranchCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("BirthDate")) Then vWI_EmployeeMechanic.BirthDate = CType(dr("BirthDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("Gender")) Then vWI_EmployeeMechanic.Gender = CType(dr("Gender"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("NoKTP")) Then vWI_EmployeeMechanic.NoKTP = dr("NoKTP").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Email")) Then vWI_EmployeeMechanic.Email = dr("Email").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("StartWorkingDate")) Then vWI_EmployeeMechanic.StartWorkingDate = CType(dr("StartWorkingDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("JobPosition")) Then vWI_EmployeeMechanic.JobPosition = dr("JobPosition").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("EducationLevel")) Then vWI_EmployeeMechanic.EducationLevel = dr("EducationLevel").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Photo")) Then vWI_EmployeeMechanic.Photo = CType(dr("Photo"), Byte())
            If Not dr.IsDBNull(dr.GetOrdinal("ShirtSize")) Then vWI_EmployeeMechanic.ShirtSize = dr("ShirtSize").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then vWI_EmployeeMechanic.Status = CType(dr("Status"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then vWI_EmployeeMechanic.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            Return vWI_EmployeeMechanic

        End Function

        Private Sub SetTableName()

            If Not (GetType(VWI_EmployeeService) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(VWI_EmployeeService), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(VWI_EmployeeService).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace