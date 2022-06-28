#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : SalesmanUnifGuide Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2007 
'// ---------------------
'// $History      : $
'// Generated on 22/10/2007 - 13:20:43
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

    Public Class SalesmanUnifGuideMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertSalesmanUnifGuide"
        Private m_UpdateStatement As String = "up_UpdateSalesmanUnifGuide"
        Private m_RetrieveStatement As String = "up_RetrieveSalesmanUnifGuide"
        Private m_RetrieveListStatement As String = "up_RetrieveSalesmanUnifGuideList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteSalesmanUnifGuide"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim salesmanUnifGuide As SalesmanUnifGuide = Nothing
            While dr.Read

                salesmanUnifGuide = Me.CreateObject(dr)

            End While

            Return salesmanUnifGuide

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim salesmanUnifGuideList As ArrayList = New ArrayList

            While dr.Read
                Dim salesmanUnifGuide As SalesmanUnifGuide = Me.CreateObject(dr)
                salesmanUnifGuideList.Add(salesmanUnifGuide)
            End While

            Return salesmanUnifGuideList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim salesmanUnifGuide As SalesmanUnifGuide = CType(obj, SalesmanUnifGuide)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, salesmanUnifGuide.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim salesmanUnifGuide As SalesmanUnifGuide = CType(obj, SalesmanUnifGuide)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, salesmanUnifGuide.Description)
            DbCommandWrapper.AddInParameter("@SSize", DbType.Int32, salesmanUnifGuide.SSize)
            DbCommandWrapper.AddInParameter("@MSize", DbType.Int32, salesmanUnifGuide.MSize)
            DbCommandWrapper.AddInParameter("@LSize", DbType.Int32, salesmanUnifGuide.LSize)
            DbCommandWrapper.AddInParameter("@XLSize", DbType.Int32, salesmanUnifGuide.XLSize)
            DbCommandWrapper.AddInParameter("@XXLSize", DbType.Int32, salesmanUnifGuide.XXLSize)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, salesmanUnifGuide.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, salesmanUnifGuide.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@SalesmanUniformID", DbType.Int32, Me.GetRefObject(salesmanUnifGuide.SalesmanUniform))

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

            Dim salesmanUnifGuide As SalesmanUnifGuide = CType(obj, SalesmanUnifGuide)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, salesmanUnifGuide.ID)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, salesmanUnifGuide.Description)
            DbCommandWrapper.AddInParameter("@SSize", DbType.Int32, salesmanUnifGuide.SSize)
            DbCommandWrapper.AddInParameter("@MSize", DbType.Int32, salesmanUnifGuide.MSize)
            DbCommandWrapper.AddInParameter("@LSize", DbType.Int32, salesmanUnifGuide.LSize)
            DbCommandWrapper.AddInParameter("@XLSize", DbType.Int32, salesmanUnifGuide.XLSize)
            DbCommandWrapper.AddInParameter("@XXLSize", DbType.Int32, salesmanUnifGuide.XXLSize)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, salesmanUnifGuide.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, salesmanUnifGuide.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@SalesmanUniformID", DbType.Int32, Me.GetRefObject(salesmanUnifGuide.SalesmanUniform))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As SalesmanUnifGuide

            Dim salesmanUnifGuide As SalesmanUnifGuide = New SalesmanUnifGuide

            salesmanUnifGuide.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Description")) Then salesmanUnifGuide.Description = dr("Description").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SSize")) Then salesmanUnifGuide.SSize = CType(dr("SSize"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("MSize")) Then salesmanUnifGuide.MSize = CType(dr("MSize"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("LSize")) Then salesmanUnifGuide.LSize = CType(dr("LSize"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("XLSize")) Then salesmanUnifGuide.XLSize = CType(dr("XLSize"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("XXLSize")) Then salesmanUnifGuide.XXLSize = CType(dr("XXLSize"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then salesmanUnifGuide.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then salesmanUnifGuide.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then salesmanUnifGuide.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then salesmanUnifGuide.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then salesmanUnifGuide.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("SalesmanUniformID")) Then
                salesmanUnifGuide.SalesmanUniform = New SalesmanUniform(CType(dr("SalesmanUniformID"), Integer))
            End If

            Return salesmanUnifGuide

        End Function

        Private Sub SetTableName()

            If Not (GetType(SalesmanUnifGuide) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(SalesmanUnifGuide), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(SalesmanUnifGuide).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

