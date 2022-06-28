﻿
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : DiscountProposalDetailCustomer Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2020 
'// ---------------------
'// $History      : $
'// Generated on 19/06/2020 - 14:58:20
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

    Public Class DiscountProposalDetailCustomerMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertDiscountProposalDetailCustomer"
        Private m_UpdateStatement As String = "up_UpdateDiscountProposalDetailCustomer"
        Private m_RetrieveStatement As String = "up_RetrieveDiscountProposalDetailCustomer"
        Private m_RetrieveListStatement As String = "up_RetrieveDiscountProposalDetailCustomerList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteDiscountProposalDetailCustomer"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim discountProposalDetailCustomer As DiscountProposalDetailCustomer = Nothing
            While dr.Read

                discountProposalDetailCustomer = Me.CreateObject(dr)

            End While

            Return discountProposalDetailCustomer

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim discountProposalDetailCustomerList As ArrayList = New ArrayList

            While dr.Read
                Dim discountProposalDetailCustomer As DiscountProposalDetailCustomer = Me.CreateObject(dr)
                discountProposalDetailCustomerList.Add(discountProposalDetailCustomer)
            End While

            Return discountProposalDetailCustomerList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim discountProposalDetailCustomer As DiscountProposalDetailCustomer = CType(obj, DiscountProposalDetailCustomer)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, discountProposalDetailCustomer.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim discountProposalDetailCustomer As DiscountProposalDetailCustomer = CType(obj, DiscountProposalDetailCustomer)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@Name", DbType.AnsiString, discountProposalDetailCustomer.Name)
            DbCommandWrapper.AddInParameter("@IdentityNumber", DbType.AnsiString, discountProposalDetailCustomer.IdentityNumber)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, discountProposalDetailCustomer.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, discountProposalDetailCustomer.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@DiscountProposalHeaderID", DbType.Int32, Me.GetRefObject(discountProposalDetailCustomer.DiscountProposalHeader))

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

            Dim discountProposalDetailCustomer As DiscountProposalDetailCustomer = CType(obj, DiscountProposalDetailCustomer)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, discountProposalDetailCustomer.ID)
            DbCommandWrapper.AddInParameter("@Name", DbType.AnsiString, discountProposalDetailCustomer.Name)
            DbCommandWrapper.AddInParameter("@IdentityNumber", DbType.AnsiString, discountProposalDetailCustomer.IdentityNumber)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, discountProposalDetailCustomer.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, discountProposalDetailCustomer.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@DiscountProposalHeaderID", DbType.Int32, Me.GetRefObject(discountProposalDetailCustomer.DiscountProposalHeader))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As DiscountProposalDetailCustomer

            Dim discountProposalDetailCustomer As DiscountProposalDetailCustomer = New DiscountProposalDetailCustomer

            discountProposalDetailCustomer.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Name")) Then discountProposalDetailCustomer.Name = dr("Name").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("IdentityNumber")) Then discountProposalDetailCustomer.IdentityNumber = dr("IdentityNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then discountProposalDetailCustomer.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then discountProposalDetailCustomer.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then discountProposalDetailCustomer.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then discountProposalDetailCustomer.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then discountProposalDetailCustomer.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("DiscountProposalHeaderID")) Then
                discountProposalDetailCustomer.DiscountProposalHeader = New DiscountProposalHeader(CType(dr("DiscountProposalHeaderID"), Integer))
            End If

            Return discountProposalDetailCustomer

        End Function

        Private Sub SetTableName()

            If Not (GetType(DiscountProposalDetailCustomer) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(DiscountProposalDetailCustomer), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(DiscountProposalDetailCustomer).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

