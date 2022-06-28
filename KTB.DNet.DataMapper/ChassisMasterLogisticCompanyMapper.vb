#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : ChassisMasterLogisticCompany Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2020 
'// ---------------------
'// $History      : $
'// Generated on 9/9/2020 - 4:39:24 PM
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

    Public Class ChassisMasterLogisticCompanyMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertChassisMasterLogisticCompany"
        Private m_UpdateStatement As String = "up_UpdateChassisMasterLogisticCompany"
        Private m_RetrieveStatement As String = "up_RetrieveChassisMasterLogisticCompany"
        Private m_RetrieveListStatement As String = "up_RetrieveChassisMasterLogisticCompanyList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteChassisMasterLogisticCompany"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim chassisMasterLogisticCompany As ChassisMasterLogisticCompany = Nothing
            While dr.Read

                chassisMasterLogisticCompany = Me.CreateObject(dr)

            End While

            Return chassisMasterLogisticCompany

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim chassisMasterLogisticCompanyList As ArrayList = New ArrayList

            While dr.Read
                Dim chassisMasterLogisticCompany As ChassisMasterLogisticCompany = Me.CreateObject(dr)
                chassisMasterLogisticCompanyList.Add(chassisMasterLogisticCompany)
            End While

            Return chassisMasterLogisticCompanyList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim chassisMasterLogisticCompany As ChassisMasterLogisticCompany = CType(obj, ChassisMasterLogisticCompany)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, chassisMasterLogisticCompany.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim chassisMasterLogisticCompany As ChassisMasterLogisticCompany = CType(obj, ChassisMasterLogisticCompany)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@Name", DbType.AnsiString, chassisMasterLogisticCompany.Name)
            DbCommandWrapper.AddInParameter("@Kode", DbType.AnsiString, chassisMasterLogisticCompany.Kode)
            DbCommandWrapper.AddInParameter("@Address", DbType.AnsiString, chassisMasterLogisticCompany.Address)
            DbCommandWrapper.AddInParameter("@NoTelfon", DbType.Int32, chassisMasterLogisticCompany.NoTelfon)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, chassisMasterLogisticCompany.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, chassisMasterLogisticCompany.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@CityID", DbType.Int16, Me.GetRefObject(chassisMasterLogisticCompany.City))

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

            Dim chassisMasterLogisticCompany As ChassisMasterLogisticCompany = CType(obj, ChassisMasterLogisticCompany)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, chassisMasterLogisticCompany.ID)
            DbCommandWrapper.AddInParameter("@Name", DbType.AnsiString, chassisMasterLogisticCompany.Name)
            DbCommandWrapper.AddInParameter("@Kode", DbType.AnsiString, chassisMasterLogisticCompany.Kode)
            DbCommandWrapper.AddInParameter("@Address", DbType.AnsiString, chassisMasterLogisticCompany.Address)
            DbCommandWrapper.AddInParameter("@NoTelfon", DbType.Int32, chassisMasterLogisticCompany.NoTelfon)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, chassisMasterLogisticCompany.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, chassisMasterLogisticCompany.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@CityID", DbType.Int16, Me.GetRefObject(chassisMasterLogisticCompany.City))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As ChassisMasterLogisticCompany

            Dim chassisMasterLogisticCompany As ChassisMasterLogisticCompany = New ChassisMasterLogisticCompany

            chassisMasterLogisticCompany.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Name")) Then chassisMasterLogisticCompany.Name = dr("Name").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Kode")) Then chassisMasterLogisticCompany.Kode = dr("Kode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Address")) Then chassisMasterLogisticCompany.Address = dr("Address").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("NoTelfon")) Then chassisMasterLogisticCompany.NoTelfon = CType(dr("NoTelfon"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then chassisMasterLogisticCompany.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then chassisMasterLogisticCompany.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then chassisMasterLogisticCompany.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then chassisMasterLogisticCompany.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then chassisMasterLogisticCompany.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("CityID")) Then
                chassisMasterLogisticCompany.City = New City(CType(dr("CityID"), Short))
            End If
            Return chassisMasterLogisticCompany

        End Function

        Private Sub SetTableName()

            If Not (GetType(ChassisMasterLogisticCompany) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(ChassisMasterLogisticCompany), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(ChassisMasterLogisticCompany).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace
