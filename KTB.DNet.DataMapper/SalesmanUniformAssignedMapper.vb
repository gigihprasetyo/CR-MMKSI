#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : SalesmanUniformAssigned Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2007 
'// ---------------------
'// $History      : $
'// Generated on 8/10/2007 - 1:11:43 PM
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

    Public Class SalesmanUniformAssignedMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertSalesmanUniformAssigned"
        Private m_UpdateStatement As String = "up_UpdateSalesmanUniformAssigned"
        Private m_RetrieveStatement As String = "up_RetrieveSalesmanUniformAssigned"
        Private m_RetrieveListStatement As String = "up_RetrieveSalesmanUniformAssignedList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteSalesmanUniformAssigned"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim salesmanUniformAssigned As SalesmanUniformAssigned = Nothing
            While dr.Read

                salesmanUniformAssigned = Me.CreateObject(dr)

            End While

            Return salesmanUniformAssigned

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim salesmanUniformAssignedList As ArrayList = New ArrayList

            While dr.Read
                Dim salesmanUniformAssigned As SalesmanUniformAssigned = Me.CreateObject(dr)
                salesmanUniformAssignedList.Add(salesmanUniformAssigned)
            End While

            Return salesmanUniformAssignedList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim salesmanUniformAssigned As SalesmanUniformAssigned = CType(obj, SalesmanUniformAssigned)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, salesmanUniformAssigned.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim salesmanUniformAssigned As SalesmanUniformAssigned = CType(obj, SalesmanUniformAssigned)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@UniformSize", DbType.Byte, salesmanUniformAssigned.UniformSize)
            DbCommandWrapper.AddInParameter("@IsReleased", DbType.Byte, salesmanUniformAssigned.IsReleased)
            DbCommandWrapper.AddInParameter("@IsValidate", DbType.Byte, salesmanUniformAssigned.IsValidate)
            DbCommandWrapper.AddInParameter("@Qty", DbType.Int32, salesmanUniformAssigned.Qty)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, salesmanUniformAssigned.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, salesmanUniformAssigned.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@SalesmanUniformID", DbType.Int32, Me.GetRefObject(salesmanUniformAssigned.SalesmanUniform))
            DbCommandWrapper.AddInParameter("@SalesmanHeaderID", DbType.Int32, Me.GetRefObject(salesmanUniformAssigned.SalesmanHeader))

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

            Dim salesmanUniformAssigned As SalesmanUniformAssigned = CType(obj, SalesmanUniformAssigned)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, salesmanUniformAssigned.ID)
            DbCommandWrapper.AddInParameter("@UniformSize", DbType.Byte, salesmanUniformAssigned.UniformSize)
            DbCommandWrapper.AddInParameter("@IsReleased", DbType.Byte, salesmanUniformAssigned.IsReleased)
            DbCommandWrapper.AddInParameter("@IsValidate", DbType.Byte, salesmanUniformAssigned.IsValidate)
            DbCommandWrapper.AddInParameter("@Qty", DbType.Int32, salesmanUniformAssigned.Qty)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, salesmanUniformAssigned.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, salesmanUniformAssigned.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@SalesmanUniformID", DbType.Int32, Me.GetRefObject(salesmanUniformAssigned.SalesmanUniform))
            DbCommandWrapper.AddInParameter("@SalesmanHeaderID", DbType.Int32, Me.GetRefObject(salesmanUniformAssigned.SalesmanHeader))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As SalesmanUniformAssigned

            Dim salesmanUniformAssigned As SalesmanUniformAssigned = New SalesmanUniformAssigned

            salesmanUniformAssigned.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("UniformSize")) Then salesmanUniformAssigned.UniformSize = CType(dr("UniformSize"), Byte)
            If Not dr.IsDBNull(dr.GetOrdinal("IsReleased")) Then salesmanUniformAssigned.IsReleased = CType(dr("IsReleased"), Byte)
            If Not dr.IsDBNull(dr.GetOrdinal("IsValidate")) Then salesmanUniformAssigned.IsValidate = CType(dr("IsValidate"), Byte)
            If Not dr.IsDBNull(dr.GetOrdinal("Qty")) Then salesmanUniformAssigned.Qty = CType(dr("Qty"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then salesmanUniformAssigned.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then salesmanUniformAssigned.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then salesmanUniformAssigned.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then salesmanUniformAssigned.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then salesmanUniformAssigned.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("SalesmanUniformID")) Then
                salesmanUniformAssigned.SalesmanUniform = New SalesmanUniform(CType(dr("SalesmanUniformID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("SalesmanHeaderID")) Then
                salesmanUniformAssigned.SalesmanHeader = New SalesmanHeader(CType(dr("SalesmanHeaderID"), Integer))
            End If

            Return salesmanUniformAssigned

        End Function

        Private Sub SetTableName()

            If Not (GetType(SalesmanUniformAssigned) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(SalesmanUniformAssigned), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(SalesmanUniformAssigned).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

