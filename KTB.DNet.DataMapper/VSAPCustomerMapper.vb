
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : VSAPCustomer Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2009 
'// ---------------------
'// $History      : $
'// Generated on 1/9/2009 - 10:34:26 AM
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

    Public Class VSAPCustomerMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertVSAPCustomer"
        Private m_UpdateStatement As String = "up_UpdateVSAPCustomer"
        Private m_RetrieveStatement As String = "up_RetrieveVSAPCustomer"
        Private m_RetrieveListStatement As String = "up_RetrieveVSAPCustomerList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteVSAPCustomer"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim vSAPCustomer As VSAPCustomer = Nothing
            While dr.Read

                vSAPCustomer = Me.CreateObject(dr)

            End While

            Return vSAPCustomer

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim vSAPCustomerList As ArrayList = New ArrayList

            While dr.Read
                Dim vSAPCustomer As VSAPCustomer = Me.CreateObject(dr)
                vSAPCustomerList.Add(vSAPCustomer)
            End While

            Return vSAPCustomerList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim vSAPCustomer As VSAPCustomer = CType(obj, VSAPCustomer)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, vSAPCustomer.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim vSAPCustomer As VSAPCustomer = CType(obj, VSAPCustomer)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@SalesmanHeaderID", DbType.Int32, vSAPCustomer.SalesmanHeaderID)
            DbCommandWrapper.AddInParameter("@CustomerCode", DbType.AnsiString, vSAPCustomer.CustomerCode)
            DbCommandWrapper.AddInParameter("@CustomerName", DbType.AnsiString, vSAPCustomer.CustomerName)
            DbCommandWrapper.AddInParameter("@CustomerAddress", DbType.AnsiString, vSAPCustomer.CustomerAddress)
            DbCommandWrapper.AddInParameter("@Status", DbType.Byte, vSAPCustomer.Status)
            DbCommandWrapper.AddInParameter("@Qty", DbType.Int32, vSAPCustomer.Qty)
            DbCommandWrapper.AddInParameter("@ProspectDate", DbType.DateTime, vSAPCustomer.ProspectDate)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, vSAPCustomer.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, vSAPCustomer.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetNewID(ByVal dbCommandWrapper As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper) As Integer

            Return CType(dbCommandWrapper.GetParameterValue("@ID"), Integer)

        End Function

        Protected Overrides Function GetPagingRetrieveCommand() As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_PagingQuery)
            DBCommandWrapper.AddInParameter("@Table", DbType.String, m_TableName)
            DBCommandWrapper.AddInParameter("@PK", DbType.String, "ID")

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

            Dim vSAPCustomer As VSAPCustomer = CType(obj, VSAPCustomer)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, vSAPCustomer.ID)
            DbCommandWrapper.AddInParameter("@SalesmanHeaderID", DbType.Int32, vSAPCustomer.SalesmanHeaderID)
            DbCommandWrapper.AddInParameter("@CustomerCode", DbType.AnsiString, vSAPCustomer.CustomerCode)
            DbCommandWrapper.AddInParameter("@CustomerName", DbType.AnsiString, vSAPCustomer.CustomerName)
            DbCommandWrapper.AddInParameter("@CustomerAddress", DbType.AnsiString, vSAPCustomer.CustomerAddress)
            DbCommandWrapper.AddInParameter("@Status", DbType.Byte, vSAPCustomer.Status)
            DbCommandWrapper.AddInParameter("@Qty", DbType.Int32, vSAPCustomer.Qty)
            DbCommandWrapper.AddInParameter("@ProspectDate", DbType.DateTime, vSAPCustomer.ProspectDate)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, vSAPCustomer.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, vSAPCustomer.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As VSAPCustomer

            Dim vSAPCustomer As VSAPCustomer = New VSAPCustomer

            vSAPCustomer.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("SalesmanHeaderID")) Then vSAPCustomer.SalesmanHeaderID = CType(dr("SalesmanHeaderID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("CustomerCode")) Then vSAPCustomer.CustomerCode = dr("CustomerCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CustomerName")) Then vSAPCustomer.CustomerName = dr("CustomerName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CustomerAddress")) Then vSAPCustomer.CustomerAddress = dr("CustomerAddress").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then vSAPCustomer.Status = CType(dr("Status"), Byte)
            If Not dr.IsDBNull(dr.GetOrdinal("Qty")) Then vSAPCustomer.Qty = CType(dr("Qty"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("ProspectDate")) Then vSAPCustomer.ProspectDate = CType(dr("ProspectDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then vSAPCustomer.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then vSAPCustomer.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then vSAPCustomer.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then vSAPCustomer.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then vSAPCustomer.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            Return vSAPCustomer

        End Function

        Private Sub SetTableName()

            If Not (GetType(VSAPCustomer) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(VSAPCustomer), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(VSAPCustomer).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

