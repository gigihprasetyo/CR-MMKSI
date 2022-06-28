#Region "Summary"
'// ===========================================================================
'// AUTHOR        : Noviar Sbastian
'// PURPOSE       : BasicProduct Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2005 
'// ---------------------
'// $History      : $
'// Generated on 9/22/2005 - 5:10:47 AM
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

    Public Class BasicProductMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertBasicProduct"
        Private m_UpdateStatement As String = "up_UpdateBasicProduct"
        Private m_RetrieveStatement As String = "up_RetrieveBasicProduct"
        Private m_RetrieveListStatement As String = "up_RetrieveBasicProductList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteBasicProduct"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim basicProduct As basicProduct = Nothing
            While dr.Read

                basicProduct = Me.CreateObject(dr)

            End While

            Return basicProduct

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim basicProductList As ArrayList = New ArrayList

            While dr.Read
                Dim basicProduct As basicProduct = Me.CreateObject(dr)
                basicProductList.Add(basicProduct)
            End While

            Return basicProductList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim basicProduct As basicProduct = CType(obj, basicProduct)
            DBCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DBCommandWrapper.AddInParameter("@ID", DbType.Int32, basicProduct.ID)
            Return DBCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim basicProduct As basicProduct = CType(obj, basicProduct)
            DBCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DBCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DBCommandWrapper.AddInParameter("@BasicProductCode", DbType.AnsiString, BasicProduct.BasicProductCode)
            DBCommandWrapper.AddInParameter("@BasicProductName", DbType.AnsiString, BasicProduct.BasicProductName)
            DBCommandWrapper.AddInParameter("@Description", DbType.AnsiString, BasicProduct.Description)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DBCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdatedTime",DbType.DateTime,DateTime.Now)
            DBCommandWrapper.AddInParameter("@LastUpdatedBy", DbType.AnsiString, BasicProduct.LastUpdatedBy)
            DBCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, BasicProduct.RowStatus)
            DBCommandWrapper.AddInParameter("@LockedBy", DbType.AnsiString, BasicProduct.LockedBy)


            Return DBCommandWrapper

        End Function

        Protected Overrides Function GetNewID(ByVal dbCommandWrapper As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper) As Integer

            Return CType(dbCommandWrapper.GetParameterValue("@ID"), Integer)

        End Function

        Protected Overrides Function GetPagingRetrieveCommand() As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            DBCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_PagingQuery)
            DBCommandWrapper.AddInParameter("@Table", DbType.String, m_TableName)
            DBCommandWrapper.AddInParameter("@PK", DbType.String, "ID")

            Return DBCommandWrapper

        End Function

        Protected Overrides Function GetRetrieveCommand() As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            DBCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DinamicQuery)
            DBCommandWrapper.AddInParameter("@sqlQuery", DbType.String, "SELECT " + m_TableName + ".* FROM " + m_TableName)

            Return DBCommandWrapper

        End Function

        Protected Overrides Function GetRetrieveListParameter() As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            DBCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_RetrieveListStatement)
            Return DBCommandWrapper

        End Function

        Protected Overrides Function GetRetrieveParameter(ByVal id As Integer) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            DBCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_RetrieveStatement)
            DBCommandWrapper.AddInParameter("@ID", DbType.Int32, id)

            Return DBCommandWrapper

        End Function

        Protected Overrides Function GetUpdateParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim basicProduct As basicProduct = CType(obj, basicProduct)

            DBCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DBCommandWrapper.AddInParameter("@ID", DbType.Int32, BasicProduct.ID)
            DBCommandWrapper.AddInParameter("@BasicProductCode", DbType.AnsiString, BasicProduct.BasicProductCode)
            DBCommandWrapper.AddInParameter("@BasicProductName", DbType.AnsiString, BasicProduct.BasicProductName)
            DBCommandWrapper.AddInParameter("@Description", DbType.AnsiString, BasicProduct.Description)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DBCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, BasicProduct.CreatedBy)
            'DbCommandWrapper.AddInParameter("@LastUpdatedTime",DbType.DateTime,DateTime.Now)
            DBCommandWrapper.AddInParameter("@LastUpdatedBy", DbType.AnsiString, BasicProduct.LastUpdatedBy)
            DBCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, BasicProduct.RowStatus)
            DBCommandWrapper.AddInParameter("@LockedBy", DbType.AnsiString, BasicProduct.LockedBy)



            Return DBCommandWrapper

        End Function

#End Region

#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As BasicProduct

            Dim basicProduct As basicProduct = New basicProduct

            basicProduct.ID = CType(dr("ID"), Integer)

            If Not dr.IsDBNull(dr.GetOrdinal("BasicProductCode")) Then basicProduct.BasicProductCode = dr("BasicProductCode").ToString

            If Not dr.IsDBNull(dr.GetOrdinal("BasicProductName")) Then basicProduct.BasicProductName = dr("BasicProductName").ToString

            If Not dr.IsDBNull(dr.GetOrdinal("Description")) Then basicProduct.Description = dr("Description").ToString

            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then basicProduct.CreatedTime = CType(dr("CreatedTime"), DateTime)

            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then basicProduct.CreatedBy = dr("CreatedBy").ToString

            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdatedTime")) Then basicProduct.LastUpdatedTime = CType(dr("LastUpdatedTime"), DateTime)

            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdatedBy")) Then basicProduct.LastUpdatedBy = dr("LastUpdatedBy").ToString

            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then basicProduct.RowStatus = CType(dr("RowStatus"), Short)

            If Not dr.IsDBNull(dr.GetOrdinal("LockedBy")) Then basicProduct.LockedBy = dr("LockedBy").ToString



            Return basicProduct

        End Function

        Private Sub SetTableName()

            If Not (GetType(BasicProduct) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(BasicProduct), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(BasicProduct).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace