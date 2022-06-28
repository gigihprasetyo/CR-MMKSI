#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : UniformDistribution Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2007 
'// ---------------------
'// $History      : $
'// Generated on 7/16/2007 - 11:19:14 AM
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

    Public Class UniformDistributionMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertUniformDistribution"
        Private m_UpdateStatement As String = "up_UpdateUniformDistribution"
        Private m_RetrieveStatement As String = "up_RetrieveUniformDistribution"
        Private m_RetrieveListStatement As String = "up_RetrieveUniformDistributionList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteUniformDistribution"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim uniformDistribution As UniformDistribution = Nothing
            While dr.Read

                uniformDistribution = Me.CreateObject(dr)

            End While

            Return uniformDistribution

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim uniformDistributionList As ArrayList = New ArrayList

            While dr.Read
                Dim uniformDistribution As UniformDistribution = Me.CreateObject(dr)
                uniformDistributionList.Add(uniformDistribution)
            End While

            Return uniformDistributionList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim uniformDistribution As UniformDistribution = CType(obj, UniformDistribution)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, uniformDistribution.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim uniformDistribution As UniformDistribution = CType(obj, UniformDistribution)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@DistributionCode", DbType.AnsiString, uniformDistribution.DistributionCode)
            DbCommandWrapper.AddInParameter("@UniformCode", DbType.AnsiString, uniformDistribution.UniformCode)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, uniformDistribution.Description)
            DbCommandWrapper.AddInParameter("@StandardPrice", DbType.Decimal, uniformDistribution.StandardPrice)
            DbCommandWrapper.AddInParameter("@DealerPrice", DbType.Decimal, uniformDistribution.DealerPrice)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, uniformDistribution.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, uniformDistribution.LastUpdateBy)
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

            Dim uniformDistribution As UniformDistribution = CType(obj, UniformDistribution)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, uniformDistribution.ID)
            DbCommandWrapper.AddInParameter("@DistributionCode", DbType.AnsiString, uniformDistribution.DistributionCode)
            DbCommandWrapper.AddInParameter("@UniformCode", DbType.AnsiString, uniformDistribution.UniformCode)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, uniformDistribution.Description)
            DbCommandWrapper.AddInParameter("@StandardPrice", DbType.Decimal, uniformDistribution.StandardPrice)
            DbCommandWrapper.AddInParameter("@DealerPrice", DbType.Decimal, uniformDistribution.DealerPrice)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, uniformDistribution.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, uniformDistribution.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As UniformDistribution

            Dim uniformDistribution As UniformDistribution = New UniformDistribution

            uniformDistribution.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("DistributionCode")) Then uniformDistribution.DistributionCode = dr("DistributionCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("UniformCode")) Then uniformDistribution.UniformCode = dr("UniformCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Description")) Then uniformDistribution.Description = dr("Description").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("StandardPrice")) Then uniformDistribution.StandardPrice = CType(dr("StandardPrice"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerPrice")) Then uniformDistribution.DealerPrice = CType(dr("DealerPrice"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then uniformDistribution.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then uniformDistribution.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then uniformDistribution.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then uniformDistribution.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then uniformDistribution.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            Return uniformDistribution

        End Function

        Private Sub SetTableName()

            If Not (GetType(UniformDistribution) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(UniformDistribution), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(UniformDistribution).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

