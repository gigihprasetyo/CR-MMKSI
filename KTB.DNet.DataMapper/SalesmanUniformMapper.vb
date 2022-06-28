#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : SalesmanUniform Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2007 
'// ---------------------
'// $History      : $
'// Generated on 8/7/2007 - 10:24:45 AM
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

    Public Class SalesmanUniformMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertSalesmanUniform"
        Private m_UpdateStatement As String = "up_UpdateSalesmanUniform"
        Private m_RetrieveStatement As String = "up_RetrieveSalesmanUniform"
        Private m_RetrieveListStatement As String = "up_RetrieveSalesmanUniformList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteSalesmanUniform"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim salesmanUniform As SalesmanUniform = Nothing
            While dr.Read

                salesmanUniform = Me.CreateObject(dr)

            End While

            Return salesmanUniform

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim salesmanUniformList As ArrayList = New ArrayList

            While dr.Read
                Dim salesmanUniform As SalesmanUniform = Me.CreateObject(dr)
                salesmanUniformList.Add(salesmanUniform)
            End While

            Return salesmanUniformList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim salesmanUniform As SalesmanUniform = CType(obj, SalesmanUniform)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, salesmanUniform.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim salesmanUniform As SalesmanUniform = CType(obj, SalesmanUniform)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@SalesmanUniformCode", DbType.AnsiString, salesmanUniform.SalesmanUniformCode)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, salesmanUniform.Description)
            DbCommandWrapper.AddInParameter("@NormalPrice", DbType.Currency, salesmanUniform.NormalPrice)
            DbCommandWrapper.AddInParameter("@DealerPrice", DbType.Currency, salesmanUniform.DealerPrice)
            DbCommandWrapper.AddInParameter("@Image", DbType.Binary, salesmanUniform.Image)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, salesmanUniform.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, salesmanUniform.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@SalesmanUnifDistributionID", DbType.Int32, Me.GetRefObject(salesmanUniform.SalesmanUnifDistribution))

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

            Dim salesmanUniform As SalesmanUniform = CType(obj, SalesmanUniform)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, salesmanUniform.ID)
            DbCommandWrapper.AddInParameter("@SalesmanUniformCode", DbType.AnsiString, salesmanUniform.SalesmanUniformCode)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, salesmanUniform.Description)
            DbCommandWrapper.AddInParameter("@NormalPrice", DbType.Currency, salesmanUniform.NormalPrice)
            DbCommandWrapper.AddInParameter("@DealerPrice", DbType.Currency, salesmanUniform.DealerPrice)
            DbCommandWrapper.AddInParameter("@Image", DbType.Binary, salesmanUniform.Image)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, salesmanUniform.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, salesmanUniform.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@SalesmanUnifDistributionID", DbType.Int32, Me.GetRefObject(salesmanUniform.SalesmanUnifDistribution))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As SalesmanUniform

            Dim salesmanUniform As SalesmanUniform = New SalesmanUniform

            salesmanUniform.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("SalesmanUniformCode")) Then salesmanUniform.SalesmanUniformCode = dr("SalesmanUniformCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Description")) Then salesmanUniform.Description = dr("Description").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("NormalPrice")) Then salesmanUniform.NormalPrice = CType(dr("NormalPrice"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerPrice")) Then salesmanUniform.DealerPrice = CType(dr("DealerPrice"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("Image")) Then salesmanUniform.Image = CType(dr("Image"), Byte())
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then salesmanUniform.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then salesmanUniform.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then salesmanUniform.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then salesmanUniform.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then salesmanUniform.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("SalesmanUnifDistributionID")) Then
                salesmanUniform.SalesmanUnifDistribution = New SalesmanUnifDistribution(CType(dr("SalesmanUnifDistributionID"), Integer))
            End If

            Return salesmanUniform

        End Function

        Private Sub SetTableName()

            If Not (GetType(SalesmanUniform) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(SalesmanUniform), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(SalesmanUniform).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

