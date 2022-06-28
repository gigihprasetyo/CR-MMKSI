
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : Mitrais Team
'// PURPOSE       : VWI_EmployeeParts Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 24/04/2018 - 14:45:28
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

    Public Class VWI_EmployeePartsMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertVWI_EmployeeParts"
        Private m_UpdateStatement As String = "up_UpdateVWI_EmployeeParts"
        Private m_RetrieveStatement As String = "up_RetrieveVWI_EmployeeParts"
        Private m_RetrieveListStatement As String = "up_RetrieveVWI_EmployeePartsList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteVWI_EmployeeParts"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim vWI_EmployeeParts As VWI_EmployeeParts = Nothing
            While dr.Read

                vWI_EmployeeParts = Me.CreateObject(dr)

            End While

            Return vWI_EmployeeParts

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim vWI_EmployeePartsList As ArrayList = New ArrayList

            While dr.Read
                Dim vWI_EmployeeParts As VWI_EmployeeParts = Me.CreateObject(dr)
                vWI_EmployeePartsList.Add(vWI_EmployeeParts)
            End While

            Return vWI_EmployeePartsList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim vWI_EmployeeParts As VWI_EmployeeParts = CType(obj, VWI_EmployeeParts)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, vWI_EmployeeParts.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim vWI_EmployeeParts As VWI_EmployeeParts = CType(obj, VWI_EmployeeParts)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@SalesmanCode", DbType.AnsiString, vWI_EmployeeParts.SalesmanCode)
            DbCommandWrapper.AddInParameter("@DealerId", DbType.Int16, vWI_EmployeeParts.DealerId)
            DbCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, vWI_EmployeeParts.DealerCode)
            DbCommandWrapper.AddInParameter("@DealerBranchId", DbType.Int32, vWI_EmployeeParts.DealerBranchId)
            DbCommandWrapper.AddInParameter("@DealerBranchCode", DbType.AnsiString, vWI_EmployeeParts.DealerBranchCode)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int32, vWI_EmployeeParts.Status)
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

            Dim vWI_EmployeeParts As VWI_EmployeeParts = CType(obj, VWI_EmployeeParts)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, vWI_EmployeeParts.ID)
            DbCommandWrapper.AddInParameter("@SalesmanCode", DbType.AnsiString, vWI_EmployeeParts.SalesmanCode)
            DbCommandWrapper.AddInParameter("@DealerId", DbType.Int16, vWI_EmployeeParts.DealerId)
            DbCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, vWI_EmployeeParts.DealerCode)
            DbCommandWrapper.AddInParameter("@DealerBranchId", DbType.Int32, vWI_EmployeeParts.DealerBranchId)
            DbCommandWrapper.AddInParameter("@DealerBranchCode", DbType.AnsiString, vWI_EmployeeParts.DealerBranchCode)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int32, vWI_EmployeeParts.Status)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As VWI_EmployeeParts

            Dim vWI_EmployeeParts As VWI_EmployeeParts = New VWI_EmployeeParts

            vWI_EmployeeParts.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("SalesmanCode")) Then vWI_EmployeeParts.SalesmanCode = dr("SalesmanCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Name")) Then vWI_EmployeeParts.Name = dr("Name").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PlaceOfBirth")) Then vWI_EmployeeParts.PlaceOfBirth = dr("PlaceOfBirth").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DateOfBirth")) Then vWI_EmployeeParts.DateOfBirth = CType(dr("DateOfBirth"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("Gender")) Then vWI_EmployeeParts.Gender = CType(dr("Gender"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("MarriedStatus")) Then vWI_EmployeeParts.MarriedStatus = CType(dr("MarriedStatus"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Address")) Then vWI_EmployeeParts.Address = dr("Address").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("City")) Then vWI_EmployeeParts.City = dr("City").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("HireDate")) Then vWI_EmployeeParts.HireDate = CType(dr("HireDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("ResignDate")) Then vWI_EmployeeParts.ResignDate = CType(dr("ResignDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("ResignReason")) Then vWI_EmployeeParts.ResignReason = dr("ResignReason").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DealerId")) Then vWI_EmployeeParts.DealerId = CType(dr("DealerId"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerCode")) Then vWI_EmployeeParts.DealerCode = dr("DealerCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DealerBranchId")) Then vWI_EmployeeParts.DealerBranchId = CType(dr("DealerBranchId"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerBranchCode")) Then vWI_EmployeeParts.DealerBranchCode = dr("DealerBranchCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then vWI_EmployeeParts.Status = CType(dr("Status"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then vWI_EmployeeParts.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            Return vWI_EmployeeParts

        End Function

        Private Sub SetTableName()

            If Not (GetType(VWI_EmployeeParts) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(VWI_EmployeeParts), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(VWI_EmployeeParts).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace