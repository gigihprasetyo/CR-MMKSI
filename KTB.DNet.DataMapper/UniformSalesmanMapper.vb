#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : UniformSalesman Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2007 
'// ---------------------
'// $History      : $
'// Generated on 7/16/2007 - 11:21:48 AM
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

    Public Class UniformSalesmanMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertUniformSalesman"
        Private m_UpdateStatement As String = "up_UpdateUniformSalesman"
        Private m_RetrieveStatement As String = "up_RetrieveUniformSalesman"
        Private m_RetrieveListStatement As String = "up_RetrieveUniformSalesmanList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteUniformSalesman"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim uniformSalesman As UniformSalesman = Nothing
            While dr.Read

                uniformSalesman = Me.CreateObject(dr)

            End While

            Return uniformSalesman

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim uniformSalesmanList As ArrayList = New ArrayList

            While dr.Read
                Dim uniformSalesman As UniformSalesman = Me.CreateObject(dr)
                uniformSalesmanList.Add(uniformSalesman)
            End While

            Return uniformSalesmanList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim uniformSalesman As UniformSalesman = CType(obj, UniformSalesman)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, uniformSalesman.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim uniformSalesman As UniformSalesman = CType(obj, UniformSalesman)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@DealerId", DbType.Int32, uniformSalesman.DealerId)
            DbCommandWrapper.AddInParameter("@SalesmanLevel", DbType.AnsiStringFixedLength, uniformSalesman.SalesmanLevel)
            DbCommandWrapper.AddInParameter("@Validation", DbType.AnsiStringFixedLength, uniformSalesman.Validation)
            DbCommandWrapper.AddInParameter("@PersonInCharge", DbType.AnsiString, uniformSalesman.PersonInCharge)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, uniformSalesman.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, uniformSalesman.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@UniformDistributionId", DbType.Int32, Me.GetRefObject(uniformSalesman.UniformDistribution))
            DbCommandWrapper.AddInParameter("@SalesmanId", DbType.Int32, Me.GetRefObject(uniformSalesman.SalesmanHeader))

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

            Dim uniformSalesman As UniformSalesman = CType(obj, UniformSalesman)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, uniformSalesman.ID)
            DbCommandWrapper.AddInParameter("@DealerId", DbType.Int32, uniformSalesman.DealerId)
            DbCommandWrapper.AddInParameter("@SalesmanLevel", DbType.AnsiStringFixedLength, uniformSalesman.SalesmanLevel)
            DbCommandWrapper.AddInParameter("@Validation", DbType.AnsiStringFixedLength, uniformSalesman.Validation)
            DbCommandWrapper.AddInParameter("@PersonInCharge", DbType.AnsiString, uniformSalesman.PersonInCharge)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, uniformSalesman.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, uniformSalesman.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@UniformDistributionId", DbType.Int32, Me.GetRefObject(uniformSalesman.UniformDistribution))
            DbCommandWrapper.AddInParameter("@SalesmanId", DbType.Int32, Me.GetRefObject(uniformSalesman.SalesmanHeader))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As UniformSalesman

            Dim uniformSalesman As UniformSalesman = New UniformSalesman

            uniformSalesman.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerId")) Then uniformSalesman.DealerId = CType(dr("DealerId"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("SalesmanLevel")) Then uniformSalesman.SalesmanLevel = dr("SalesmanLevel").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Validation")) Then uniformSalesman.Validation = dr("Validation").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PersonInCharge")) Then uniformSalesman.PersonInCharge = dr("PersonInCharge").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then uniformSalesman.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then uniformSalesman.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then uniformSalesman.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then uniformSalesman.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then uniformSalesman.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("UniformDistributionId")) Then
                uniformSalesman.UniformDistribution = New UniformDistribution(CType(dr("UniformDistributionId"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("SalesmanId")) Then
                uniformSalesman.SalesmanHeader = New SalesmanHeader(CType(dr("SalesmanId"), Integer))
            End If

            Return uniformSalesman

        End Function

        Private Sub SetTableName()

            If Not (GetType(UniformSalesman) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(UniformSalesman), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(UniformSalesman).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

