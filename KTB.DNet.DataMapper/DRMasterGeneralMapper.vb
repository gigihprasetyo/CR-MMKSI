#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : DRMasterGeneral Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2020 
'// ---------------------
'// $History      : $
'// Generated on 3/4/2020 - 2:50:21 PM
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

    Public Class DRMasterGeneralMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertDRMasterGeneral"
        Private m_UpdateStatement As String = "up_UpdateDRMasterGeneral"
        Private m_RetrieveStatement As String = "up_RetrieveDRMasterGeneral"
        Private m_RetrieveListStatement As String = "up_RetrieveDRMasterGeneralList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteDRMasterGeneral"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim dRMasterGeneral As DRMasterGeneral = Nothing
            While dr.Read

                dRMasterGeneral = Me.CreateObject(dr)

            End While

            Return dRMasterGeneral

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim dRMasterGeneralList As ArrayList = New ArrayList

            While dr.Read
                Dim dRMasterGeneral As DRMasterGeneral = Me.CreateObject(dr)
                dRMasterGeneralList.Add(dRMasterGeneral)
            End While

            Return dRMasterGeneralList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim dRMasterGeneral As DRMasterGeneral = CType(obj, DRMasterGeneral)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, dRMasterGeneral.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim dRMasterGeneral As DRMasterGeneral = CType(obj, DRMasterGeneral)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@GeneralName", DbType.AnsiString, dRMasterGeneral.GeneralName)
            DbCommandWrapper.AddInParameter("@GeneralValue", DbType.Decimal, dRMasterGeneral.GeneralValue)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, dRMasterGeneral.Description)
            DbCommandWrapper.AddInParameter("@SeqNumber", DbType.Int32, dRMasterGeneral.SeqNumber)
            DbCommandWrapper.AddInParameter("@CcCustomerCategoryID", DbType.Int16, dRMasterGeneral.CcCustomerCategoryID)
            DbCommandWrapper.AddInParameter("@ProductCategory", DbType.AnsiString, dRMasterGeneral.ProductCategory)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, dRMasterGeneral.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, dRMasterGeneral.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@CategoryID", DbType.Int32, dRMasterGeneral.CategoryID)

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

            Dim dRMasterGeneral As DRMasterGeneral = CType(obj, DRMasterGeneral)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, dRMasterGeneral.ID)
            DbCommandWrapper.AddInParameter("@GeneralName", DbType.AnsiString, dRMasterGeneral.GeneralName)
            DbCommandWrapper.AddInParameter("@GeneralValue", DbType.Decimal, dRMasterGeneral.GeneralValue)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, dRMasterGeneral.Description)
            DbCommandWrapper.AddInParameter("@SeqNumber", DbType.Int32, dRMasterGeneral.SeqNumber)
            DbCommandWrapper.AddInParameter("@CcCustomerCategoryID", DbType.Int16, dRMasterGeneral.CcCustomerCategoryID)
            DbCommandWrapper.AddInParameter("@ProductCategory", DbType.AnsiString, dRMasterGeneral.ProductCategory)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, dRMasterGeneral.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, dRMasterGeneral.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@CategoryID", DbType.Int32, dRMasterGeneral.CategoryID)

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As DRMasterGeneral

            Dim dRMasterGeneral As DRMasterGeneral = New DRMasterGeneral

            dRMasterGeneral.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("GeneralName")) Then dRMasterGeneral.GeneralName = dr("GeneralName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("GeneralValue")) Then dRMasterGeneral.GeneralValue = CType(dr("GeneralValue"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("Description")) Then dRMasterGeneral.Description = dr("Description").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SeqNumber")) Then dRMasterGeneral.SeqNumber = CType(dr("SeqNumber"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("CcCustomerCategoryID")) Then dRMasterGeneral.CcCustomerCategoryID = CType(dr("CcCustomerCategoryID"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("ProductCategory")) Then dRMasterGeneral.ProductCategory = dr("ProductCategory").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then dRMasterGeneral.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then dRMasterGeneral.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then dRMasterGeneral.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then dRMasterGeneral.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then dRMasterGeneral.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("CategoryID")) Then
                dRMasterGeneral.CategoryID = CType(dr("CategoryID"), Integer)
            End If

            Return dRMasterGeneral

        End Function

        Private Sub SetTableName()

            If Not (GetType(DRMasterGeneral) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(DRMasterGeneral), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(DRMasterGeneral).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace
