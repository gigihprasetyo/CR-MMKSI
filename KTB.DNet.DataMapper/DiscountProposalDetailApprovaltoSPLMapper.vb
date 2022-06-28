#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : DiscountProposalDetailApprovaltoSPL Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2020 
'// ---------------------
'// $History      : $
'// Generated on 7/24/2020 - 11:04:49 AM
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

    Public Class DiscountProposalDetailApprovaltoSPLMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertDiscountProposalDetailApprovaltoSPL"
        Private m_UpdateStatement As String = "up_UpdateDiscountProposalDetailApprovaltoSPL"
        Private m_RetrieveStatement As String = "up_RetrieveDiscountProposalDetailApprovaltoSPL"
        Private m_RetrieveListStatement As String = "up_RetrieveDiscountProposalDetailApprovaltoSPLList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteDiscountProposalDetailApprovaltoSPL"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim discountProposalDetailApprovaltoSPL As DiscountProposalDetailApprovaltoSPL = Nothing
            While dr.Read

                discountProposalDetailApprovaltoSPL = Me.CreateObject(dr)

            End While

            Return discountProposalDetailApprovaltoSPL

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim discountProposalDetailApprovaltoSPLList As ArrayList = New ArrayList

            While dr.Read
                Dim discountProposalDetailApprovaltoSPL As DiscountProposalDetailApprovaltoSPL = Me.CreateObject(dr)
                discountProposalDetailApprovaltoSPLList.Add(discountProposalDetailApprovaltoSPL)
            End While

            Return discountProposalDetailApprovaltoSPLList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim discountProposalDetailApprovaltoSPL As DiscountProposalDetailApprovaltoSPL = CType(obj, DiscountProposalDetailApprovaltoSPL)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, discountProposalDetailApprovaltoSPL.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim discountProposalDetailApprovaltoSPL As DiscountProposalDetailApprovaltoSPL = CType(obj, DiscountProposalDetailApprovaltoSPL)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@DiscountProposed", DbType.Currency, discountProposalDetailApprovaltoSPL.DiscountProposed)
            DbCommandWrapper.AddInParameter("@DiscountApproved", DbType.Currency, discountProposalDetailApprovaltoSPL.DiscountApproved)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, discountProposalDetailApprovaltoSPL.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, discountProposalDetailApprovaltoSPL.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@DiscountMasterID", DbType.Int32, Me.GetRefObject(discountProposalDetailApprovaltoSPL.DiscountMaster))
            DbCommandWrapper.AddInParameter("@DiscountProposalDetailApprovalID", DbType.Int32, Me.GetRefObject(discountProposalDetailApprovaltoSPL.DiscountProposalDetailApproval))
            DbCommandWrapper.AddInParameter("@SPLDetailID", DbType.Int32, Me.GetRefObject(discountProposalDetailApprovaltoSPL.SPLDetail))

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

            Dim discountProposalDetailApprovaltoSPL As DiscountProposalDetailApprovaltoSPL = CType(obj, DiscountProposalDetailApprovaltoSPL)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, discountProposalDetailApprovaltoSPL.ID)
            DbCommandWrapper.AddInParameter("@DiscountProposed", DbType.Currency, discountProposalDetailApprovaltoSPL.DiscountProposed)
            DbCommandWrapper.AddInParameter("@DiscountApproved", DbType.Currency, discountProposalDetailApprovaltoSPL.DiscountApproved)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, discountProposalDetailApprovaltoSPL.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, discountProposalDetailApprovaltoSPL.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@DiscountMasterID", DbType.Int32, Me.GetRefObject(discountProposalDetailApprovaltoSPL.DiscountMaster))
            DbCommandWrapper.AddInParameter("@DiscountProposalDetailApprovalID", DbType.Int32, Me.GetRefObject(discountProposalDetailApprovaltoSPL.DiscountProposalDetailApproval))
            DbCommandWrapper.AddInParameter("@SPLDetailID", DbType.Int32, Me.GetRefObject(discountProposalDetailApprovaltoSPL.SPLDetail))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As DiscountProposalDetailApprovaltoSPL

            Dim discountProposalDetailApprovaltoSPL As DiscountProposalDetailApprovaltoSPL = New DiscountProposalDetailApprovaltoSPL

            discountProposalDetailApprovaltoSPL.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("DiscountProposed")) Then discountProposalDetailApprovaltoSPL.DiscountProposed = CType(dr("DiscountProposed"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("DiscountApproved")) Then discountProposalDetailApprovaltoSPL.DiscountApproved = CType(dr("DiscountApproved"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then discountProposalDetailApprovaltoSPL.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then discountProposalDetailApprovaltoSPL.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then discountProposalDetailApprovaltoSPL.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then discountProposalDetailApprovaltoSPL.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then discountProposalDetailApprovaltoSPL.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("DiscountMasterID")) Then
                discountProposalDetailApprovaltoSPL.DiscountMaster = New DiscountMaster(CType(dr("DiscountMasterID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("DiscountProposalDetailApprovalID")) Then
                discountProposalDetailApprovaltoSPL.DiscountProposalDetailApproval = New DiscountProposalDetailApproval(CType(dr("DiscountProposalDetailApprovalID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("SPLDetailID")) Then
                discountProposalDetailApprovaltoSPL.SPLDetail = New SPLDetail(CType(dr("SPLDetailID"), Integer))
            End If

            Return discountProposalDetailApprovaltoSPL

        End Function

        Private Sub SetTableName()

            If Not (GetType(DiscountProposalDetailApprovaltoSPL) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(DiscountProposalDetailApprovaltoSPL), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(DiscountProposalDetailApprovaltoSPL).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace
