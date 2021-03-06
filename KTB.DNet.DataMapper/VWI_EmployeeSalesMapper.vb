
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : Mitrais Team
'// PURPOSE       : VWI_EmployeeSales Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 24/04/2018 - 10:06:28
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

    Public Class VWI_EmployeeSalesMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertVWI_EmployeeSales"
        Private m_UpdateStatement As String = "up_UpdateVWI_EmployeeSales"
        Private m_RetrieveStatement As String = "up_RetrieveVWI_EmployeeSales"
        Private m_RetrieveListStatement As String = "up_RetrieveVWI_EmployeeSalesList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteVWI_EmployeeSales"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim vWI_EmployeeSales As VWI_EmployeeSales = Nothing
            While dr.Read

                vWI_EmployeeSales = Me.CreateObject(dr)

            End While

            Return vWI_EmployeeSales

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim vWI_EmployeeSalesList As ArrayList = New ArrayList

            While dr.Read
                Dim vWI_EmployeeSales As VWI_EmployeeSales = Me.CreateObject(dr)
                vWI_EmployeeSalesList.Add(vWI_EmployeeSales)
            End While

            Return vWI_EmployeeSalesList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim vWI_EmployeeSales As VWI_EmployeeSales = CType(obj, VWI_EmployeeSales)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, vWI_EmployeeSales.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim vWI_EmployeeSales As VWI_EmployeeSales = CType(obj, VWI_EmployeeSales)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@SalesmanCode", DbType.AnsiString, vWI_EmployeeSales.SalesmanCode)
            DbCommandWrapper.AddInParameter("@DealerId", DbType.Int16, vWI_EmployeeSales.DealerId)
            DbCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, vWI_EmployeeSales.DealerCode)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int32, vWI_EmployeeSales.Status)
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

            Dim vWI_EmployeeSales As VWI_EmployeeSales = CType(obj, VWI_EmployeeSales)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, vWI_EmployeeSales.ID)
            DbCommandWrapper.AddInParameter("@SalesmanCode", DbType.AnsiString, vWI_EmployeeSales.SalesmanCode)
            DbCommandWrapper.AddInParameter("@DealerId", DbType.Int16, vWI_EmployeeSales.DealerId)
            DbCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, vWI_EmployeeSales.DealerCode)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int32, vWI_EmployeeSales.Status)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As VWI_EmployeeSales

            Dim vWI_EmployeeSales As VWI_EmployeeSales = New VWI_EmployeeSales

            vWI_EmployeeSales.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("SalesmanCode")) Then vWI_EmployeeSales.SalesmanCode = dr("SalesmanCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Name")) Then vWI_EmployeeSales.Name = dr("Name").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PlaceOfBirth")) Then vWI_EmployeeSales.PlaceOfBirth = dr("PlaceOfBirth").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DateOfBirth")) Then vWI_EmployeeSales.DateOfBirth = CType(dr("DateOfBirth"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("Gender")) Then vWI_EmployeeSales.Gender = CType(dr("Gender"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("MarriedStatus")) Then vWI_EmployeeSales.MarriedStatus = CType(dr("MarriedStatus"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Address")) Then vWI_EmployeeSales.Address = dr("Address").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("City")) Then vWI_EmployeeSales.City = dr("City").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SalesmanAreaID")) Then vWI_EmployeeSales.SalesmanAreaID = CType(dr("SalesmanAreaID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("SalesmanLevelID")) Then vWI_EmployeeSales.SalesmanLevelID = CType(dr("SalesmanLevelID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("JobPositionID")) Then vWI_EmployeeSales.JobPositionID = CType(dr("JobPositionID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("LeaderSalesmanCode")) Then vWI_EmployeeSales.LeaderSalesmanCode = dr("LeaderSalesmanCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LeaderSalesmanName")) Then vWI_EmployeeSales.LeaderSalesmanName = dr("LeaderSalesmanName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("HireDate")) Then vWI_EmployeeSales.HireDate = CType(dr("HireDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("ResignDate")) Then vWI_EmployeeSales.ResignDate = CType(dr("ResignDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("ResignReason")) Then vWI_EmployeeSales.ResignReason = dr("ResignReason").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DealerId")) Then vWI_EmployeeSales.DealerId = CType(dr("DealerId"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerCode")) Then vWI_EmployeeSales.DealerCode = dr("DealerCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DealerBranchID")) Then vWI_EmployeeSales.DealerBranchID = CType(dr("DealerBranchID"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerBranchCode")) Then vWI_EmployeeSales.DealerBranchCode = dr("DealerBranchCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then vWI_EmployeeSales.Status = CType(dr("Status"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then vWI_EmployeeSales.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            Return vWI_EmployeeSales

        End Function

        Private Sub SetTableName()

            If Not (GetType(VWI_EmployeeSales) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(VWI_EmployeeSales), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(VWI_EmployeeSales).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace