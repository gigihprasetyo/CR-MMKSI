#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : DiscountProposalDetailApproval Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2020 
'// ---------------------
'// $History      : $
'// Generated on 7/24/2020 - 1:25:27 PM
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

    Public Class DiscountProposalDetailApprovalMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertDiscountProposalDetailApproval"
        Private m_UpdateStatement As String = "up_UpdateDiscountProposalDetailApproval"
        Private m_RetrieveStatement As String = "up_RetrieveDiscountProposalDetailApproval"
        Private m_RetrieveListStatement As String = "up_RetrieveDiscountProposalDetailApprovalList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteDiscountProposalDetailApproval"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim discountProposalDetailApproval As DiscountProposalDetailApproval = Nothing
            While dr.Read

                discountProposalDetailApproval = Me.CreateObject(dr)

            End While

            Return discountProposalDetailApproval

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim discountProposalDetailApprovalList As ArrayList = New ArrayList

            While dr.Read
                Dim discountProposalDetailApproval As DiscountProposalDetailApproval = Me.CreateObject(dr)
                discountProposalDetailApprovalList.Add(discountProposalDetailApproval)
            End While

            Return discountProposalDetailApprovalList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim discountProposalDetailApproval As DiscountProposalDetailApproval = CType(obj, DiscountProposalDetailApproval)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, discountProposalDetailApproval.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim discountProposalDetailApproval As DiscountProposalDetailApproval = CType(obj, DiscountProposalDetailApproval)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@ResponseQty", DbType.Int32, discountProposalDetailApproval.ResponseQty)
            DbCommandWrapper.AddInParameter("@PriceReff", DbType.DateTime, discountProposalDetailApproval.PriceReff)
            DbCommandWrapper.AddInParameter("@MaxTOPDay", DbType.Int32, discountProposalDetailApproval.MaxTOPDay)
            DbCommandWrapper.AddInParameter("@FreeIntIndicator", DbType.Int16, discountProposalDetailApproval.FreeIntIndicator)
            DbCommandWrapper.AddInParameter("@DeliveryDate", DbType.DateTime, discountProposalDetailApproval.DeliveryDate)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, discountProposalDetailApproval.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, discountProposalDetailApproval.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@VechileTypeID", DbType.Int32, Me.GetRefObject(discountProposalDetailApproval.VechileType))
            DbCommandWrapper.AddInParameter("@DiscountProposalHeaderID", DbType.Int32, Me.GetRefObject(discountProposalDetailApproval.DiscountProposalHeader))

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

            Dim discountProposalDetailApproval As DiscountProposalDetailApproval = CType(obj, DiscountProposalDetailApproval)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, discountProposalDetailApproval.ID)
            DbCommandWrapper.AddInParameter("@ResponseQty", DbType.Int32, discountProposalDetailApproval.ResponseQty)
            DbCommandWrapper.AddInParameter("@PriceReff", DbType.DateTime, discountProposalDetailApproval.PriceReff)
            DbCommandWrapper.AddInParameter("@MaxTOPDay", DbType.Int32, discountProposalDetailApproval.MaxTOPDay)
            DbCommandWrapper.AddInParameter("@FreeIntIndicator", DbType.Int16, discountProposalDetailApproval.FreeIntIndicator)
            DbCommandWrapper.AddInParameter("@DeliveryDate", DbType.DateTime, discountProposalDetailApproval.DeliveryDate)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, discountProposalDetailApproval.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, discountProposalDetailApproval.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@VechileTypeID", DbType.Int32, Me.GetRefObject(discountProposalDetailApproval.VechileType))
            DbCommandWrapper.AddInParameter("@DiscountProposalHeaderID", DbType.Int32, Me.GetRefObject(discountProposalDetailApproval.DiscountProposalHeader))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As DiscountProposalDetailApproval

            Dim discountProposalDetailApproval As DiscountProposalDetailApproval = New DiscountProposalDetailApproval

            discountProposalDetailApproval.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("ResponseQty")) Then discountProposalDetailApproval.ResponseQty = CType(dr("ResponseQty"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("PriceReff")) Then discountProposalDetailApproval.PriceReff = CType(dr("PriceReff"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("MaxTOPDay")) Then discountProposalDetailApproval.MaxTOPDay = CType(dr("MaxTOPDay"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("FreeIntIndicator")) Then discountProposalDetailApproval.FreeIntIndicator = CType(dr("FreeIntIndicator"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("DeliveryDate")) Then discountProposalDetailApproval.DeliveryDate = CType(dr("DeliveryDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then discountProposalDetailApproval.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then discountProposalDetailApproval.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then discountProposalDetailApproval.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then discountProposalDetailApproval.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then discountProposalDetailApproval.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("VechileTypeID")) Then
                discountProposalDetailApproval.VechileType = New VechileType(CType(dr("VechileTypeID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("DiscountProposalHeaderID")) Then
                discountProposalDetailApproval.DiscountProposalHeader = New DiscountProposalHeader(CType(dr("DiscountProposalHeaderID"), Integer))
            End If

            Return discountProposalDetailApproval

        End Function

        Private Sub SetTableName()

            If Not (GetType(DiscountProposalDetailApproval) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(DiscountProposalDetailApproval), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(DiscountProposalDetailApproval).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace
