#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : SalesmanUnifDistribution Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2007 
'// ---------------------
'// $History      : $
'// Generated on 8/2/2007 - 4:16:16 PM
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

    Public Class SalesmanUnifDistributionMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertSalesmanUnifDistribution"
        Private m_UpdateStatement As String = "up_UpdateSalesmanUnifDistribution"
        Private m_RetrieveStatement As String = "up_RetrieveSalesmanUnifDistribution"
        Private m_RetrieveListStatement As String = "up_RetrieveSalesmanUnifDistributionList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteSalesmanUnifDistribution"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim salesmanUnifDistribution As SalesmanUnifDistribution = Nothing
            While dr.Read

                salesmanUnifDistribution = Me.CreateObject(dr)

            End While

            Return salesmanUnifDistribution

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim salesmanUnifDistributionList As ArrayList = New ArrayList

            While dr.Read
                Dim salesmanUnifDistribution As SalesmanUnifDistribution = Me.CreateObject(dr)
                salesmanUnifDistributionList.Add(salesmanUnifDistribution)
            End While

            Return salesmanUnifDistributionList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim salesmanUnifDistribution As SalesmanUnifDistribution = CType(obj, SalesmanUnifDistribution)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, salesmanUnifDistribution.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim salesmanUnifDistribution As SalesmanUnifDistribution = CType(obj, SalesmanUnifDistribution)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@SalesmanUnifDistributionCode", DbType.AnsiString, salesmanUnifDistribution.SalesmanUnifDistributionCode)
            DBCommandWrapper.AddInParameter("@Description", DbType.AnsiString, salesmanUnifDistribution.Description)
            DBCommandWrapper.AddInParameter("@IsActive", DbType.Byte, salesmanUnifDistribution.IsActive)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, salesmanUnifDistribution.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, salesmanUnifDistribution.LastUpdateBy)
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

            Dim salesmanUnifDistribution As SalesmanUnifDistribution = CType(obj, SalesmanUnifDistribution)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, salesmanUnifDistribution.ID)
            DbCommandWrapper.AddInParameter("@SalesmanUnifDistributionCode", DbType.AnsiString, salesmanUnifDistribution.SalesmanUnifDistributionCode)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, salesmanUnifDistribution.Description)
            DBCommandWrapper.AddInParameter("@IsActive", DbType.Byte, salesmanUnifDistribution.IsActive)
            DBCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, salesmanUnifDistribution.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, salesmanUnifDistribution.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As SalesmanUnifDistribution

            Dim salesmanUnifDistribution As SalesmanUnifDistribution = New SalesmanUnifDistribution

            salesmanUnifDistribution.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("SalesmanUnifDistributionCode")) Then salesmanUnifDistribution.SalesmanUnifDistributionCode = dr("SalesmanUnifDistributionCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Description")) Then salesmanUnifDistribution.Description = dr("Description").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("IsActive")) Then salesmanUnifDistribution.IsActive = CType(dr("IsActive"), Byte)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then salesmanUnifDistribution.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then salesmanUnifDistribution.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then salesmanUnifDistribution.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then salesmanUnifDistribution.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then salesmanUnifDistribution.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            Return salesmanUnifDistribution

        End Function

        Private Sub SetTableName()

            If Not (GetType(SalesmanUnifDistribution) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(SalesmanUnifDistribution), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(SalesmanUnifDistribution).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

