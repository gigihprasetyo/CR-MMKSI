
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : DiscountProposalDetail Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2020 
'// ---------------------
'// $History      : $
'// Generated on 19/06/2020 - 14:55:13
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

    Public Class DiscountProposalDetailMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertDiscountProposalDetail"
        Private m_UpdateStatement As String = "up_UpdateDiscountProposalDetail"
        Private m_RetrieveStatement As String = "up_RetrieveDiscountProposalDetail"
        Private m_RetrieveListStatement As String = "up_RetrieveDiscountProposalDetailList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteDiscountProposalDetail"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim discountProposalDetail As DiscountProposalDetail = Nothing
            While dr.Read

                discountProposalDetail = Me.CreateObject(dr)

            End While

            Return discountProposalDetail

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim discountProposalDetailList As ArrayList = New ArrayList

            While dr.Read
                Dim discountProposalDetail As DiscountProposalDetail = Me.CreateObject(dr)
                discountProposalDetailList.Add(discountProposalDetail)
            End While

            Return discountProposalDetailList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim discountProposalDetail As DiscountProposalDetail = CType(obj, DiscountProposalDetail)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, discountProposalDetail.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim discountProposalDetail As DiscountProposalDetail = CType(obj, DiscountProposalDetail)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@AssyYear", DbType.Int16, discountProposalDetail.AssyYear)
            DbCommandWrapper.AddInParameter("@ModelYear", DbType.Int16, discountProposalDetail.ModelYear)
            DbCommandWrapper.AddInParameter("@ProposeQty", DbType.Int32, discountProposalDetail.ProposeQty)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, discountProposalDetail.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, discountProposalDetail.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@DiscountProposalHeaderID", DbType.Int32, Me.GetRefObject(discountProposalDetail.DiscountProposalHeader))
            DbCommandWrapper.AddInParameter("@SubCategoryVehicleID", DbType.Int16, Me.GetRefObject(discountProposalDetail.SubCategoryVehicle))
            DbCommandWrapper.AddInParameter("@VechileColorIsActiveOnPKID", DbType.Int32, Me.GetRefObject(discountProposalDetail.VechileColorIsActiveOnPK))

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

            Dim discountProposalDetail As DiscountProposalDetail = CType(obj, DiscountProposalDetail)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, discountProposalDetail.ID)
            DbCommandWrapper.AddInParameter("@AssyYear", DbType.Int16, discountProposalDetail.AssyYear)
            DbCommandWrapper.AddInParameter("@ModelYear", DbType.Int16, discountProposalDetail.ModelYear)
            DbCommandWrapper.AddInParameter("@ProposeQty", DbType.Int32, discountProposalDetail.ProposeQty)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, discountProposalDetail.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, discountProposalDetail.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@DiscountProposalHeaderID", DbType.Int32, Me.GetRefObject(discountProposalDetail.DiscountProposalHeader))
            DbCommandWrapper.AddInParameter("@SubCategoryVehicleID", DbType.Int16, Me.GetRefObject(discountProposalDetail.SubCategoryVehicle))
            DbCommandWrapper.AddInParameter("@VechileColorIsActiveOnPKID", DbType.Int32, Me.GetRefObject(discountProposalDetail.VechileColorIsActiveOnPK))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As DiscountProposalDetail

            Dim discountProposalDetail As DiscountProposalDetail = New DiscountProposalDetail

            discountProposalDetail.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("AssyYear")) Then discountProposalDetail.AssyYear = CType(dr("AssyYear"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("ModelYear")) Then discountProposalDetail.ModelYear = CType(dr("ModelYear"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("ProposeQty")) Then discountProposalDetail.ProposeQty = CType(dr("ProposeQty"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then discountProposalDetail.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then discountProposalDetail.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then discountProposalDetail.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then discountProposalDetail.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then discountProposalDetail.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("DiscountProposalHeaderID")) Then
                discountProposalDetail.DiscountProposalHeader = New DiscountProposalHeader(CType(dr("DiscountProposalHeaderID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("SubCategoryVehicleID")) Then
                discountProposalDetail.SubCategoryVehicle = New SubCategoryVehicle(CType(dr("SubCategoryVehicleID"), Short))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("VechileColorIsActiveOnPKID")) Then
                discountProposalDetail.VechileColorIsActiveOnPK = New VechileColorIsActiveOnPK(CType(dr("VechileColorIsActiveOnPKID"), Integer))
            End If

            Return discountProposalDetail

        End Function

        Private Sub SetTableName()

            If Not (GetType(DiscountProposalDetail) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(DiscountProposalDetail), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(DiscountProposalDetail).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

