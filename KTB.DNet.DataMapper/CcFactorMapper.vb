
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : CcFactor Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2020 
'// ---------------------
'// $History      : $
'// Generated on 12/03/2020 - 11:03:16
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

    Public Class CcFactorMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertCcFactor"
        Private m_UpdateStatement As String = "up_UpdateCcFactor"
        Private m_RetrieveStatement As String = "up_RetrieveCcFactor"
        Private m_RetrieveListStatement As String = "up_RetrieveCcFactorList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteCcFactor"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim ccFactor As CcFactor = Nothing
            While dr.Read

                ccFactor = Me.CreateObject(dr)

            End While

            Return ccFactor

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim ccFactorList As ArrayList = New ArrayList

            While dr.Read
                Dim ccFactor As CcFactor = Me.CreateObject(dr)
                ccFactorList.Add(ccFactor)
            End While

            Return ccFactorList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim ccFactor As CcFactor = CType(obj, CcFactor)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int16, ccFactor.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim ccFactor As CcFactor = CType(obj, CcFactor)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int16, 2)
            DbCommandWrapper.AddInParameter("@CcCustomerCategoryID", DbType.Int16, ccFactor.CcCustomerCategoryID)
            DbCommandWrapper.AddInParameter("@CcVehicleCategoryID", DbType.Int16, ccFactor.CcVehicleCategoryID)
            DbCommandWrapper.AddInParameter("@FactorNo", DbType.Int16, ccFactor.FactorNo)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, ccFactor.Description)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, ccFactor.Status)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, ccFactor.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, ccFactor.LastUpdateBy)
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

            Dim ccFactor As CcFactor = CType(obj, CcFactor)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int16, ccFactor.ID)
            DbCommandWrapper.AddInParameter("@CcCustomerCategoryID", DbType.Int16, ccFactor.CcCustomerCategoryID)
            DbCommandWrapper.AddInParameter("@CcVehicleCategoryID", DbType.Int16, ccFactor.CcVehicleCategoryID)
            DbCommandWrapper.AddInParameter("@FactorNo", DbType.Int16, ccFactor.FactorNo)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, ccFactor.Description)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, ccFactor.Status)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, ccFactor.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, ccFactor.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As CcFactor

            Dim ccFactor As CcFactor = New CcFactor

            ccFactor.ID = CType(dr("ID"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CcCustomerCategoryID")) Then
                ccFactor.CcCustomerCategoryID = CType(dr("CcCustomerCategoryID"), Short)
                ccFactor.CcCustomerCategory = New CcCustomerCategory(ccFactor.CcCustomerCategoryID)
            End If

            If Not dr.IsDBNull(dr.GetOrdinal("CcVehicleCategoryID")) Then
                ccFactor.CcVehicleCategoryID = CType(dr("CcVehicleCategoryID"), Short)
                ccFactor.CcVehicleCategory = New CcVehicleCategory(ccFactor.CcVehicleCategoryID)
            End If

            If Not dr.IsDBNull(dr.GetOrdinal("FactorNo")) Then ccFactor.FactorNo = CType(dr("FactorNo"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("Description")) Then ccFactor.Description = dr("Description").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then ccFactor.Status = CType(dr("Status"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then ccFactor.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then ccFactor.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then ccFactor.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then ccFactor.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then ccFactor.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            Return ccFactor

        End Function

        Private Sub SetTableName()

            If Not (GetType(CcFactor) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(CcFactor), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(CcFactor).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

