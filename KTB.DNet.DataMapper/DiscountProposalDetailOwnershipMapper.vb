
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : DiscountProposalDetailOwnership Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2020 
'// ---------------------
'// $History      : $
'// Generated on 19/06/2020 - 14:56:52
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

    Public Class DiscountProposalDetailOwnershipMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertDiscountProposalDetailOwnership"
        Private m_UpdateStatement As String = "up_UpdateDiscountProposalDetailOwnership"
        Private m_RetrieveStatement As String = "up_RetrieveDiscountProposalDetailOwnership"
        Private m_RetrieveListStatement As String = "up_RetrieveDiscountProposalDetailOwnershipList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteDiscountProposalDetailOwnership"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim discountProposalDetailOwnership As DiscountProposalDetailOwnership = Nothing
            While dr.Read

                discountProposalDetailOwnership = Me.CreateObject(dr)

            End While

            Return discountProposalDetailOwnership

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim discountProposalDetailOwnershipList As ArrayList = New ArrayList

            While dr.Read
                Dim discountProposalDetailOwnership As DiscountProposalDetailOwnership = Me.CreateObject(dr)
                discountProposalDetailOwnershipList.Add(discountProposalDetailOwnership)
            End While

            Return discountProposalDetailOwnershipList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim discountProposalDetailOwnership As DiscountProposalDetailOwnership = CType(obj, DiscountProposalDetailOwnership)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, discountProposalDetailOwnership.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim discountProposalDetailOwnership As DiscountProposalDetailOwnership = CType(obj, DiscountProposalDetailOwnership)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@VehicleBrandCategory", DbType.Int16, discountProposalDetailOwnership.VehicleBrandCategory)
            DbCommandWrapper.AddInParameter("@VehicleBrandName", DbType.AnsiString, discountProposalDetailOwnership.VehicleBrandName)
            DbCommandWrapper.AddInParameter("@VehicleModel", DbType.AnsiString, discountProposalDetailOwnership.VehicleModel)
            DbCommandWrapper.AddInParameter("@Quantity", DbType.Int32, discountProposalDetailOwnership.Quantity)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, discountProposalDetailOwnership.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, discountProposalDetailOwnership.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@DiscountProposalHeaderID", DbType.Int32, Me.GetRefObject(discountProposalDetailOwnership.DiscountProposalHeader))

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

            Dim discountProposalDetailOwnership As DiscountProposalDetailOwnership = CType(obj, DiscountProposalDetailOwnership)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, discountProposalDetailOwnership.ID)
            DbCommandWrapper.AddInParameter("@VehicleBrandCategory", DbType.Int16, discountProposalDetailOwnership.VehicleBrandCategory)
            DbCommandWrapper.AddInParameter("@VehicleBrandName", DbType.AnsiString, discountProposalDetailOwnership.VehicleBrandName)
            DbCommandWrapper.AddInParameter("@VehicleModel", DbType.AnsiString, discountProposalDetailOwnership.VehicleModel)
            DbCommandWrapper.AddInParameter("@Quantity", DbType.Int32, discountProposalDetailOwnership.Quantity)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, discountProposalDetailOwnership.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, discountProposalDetailOwnership.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@DiscountProposalHeaderID", DbType.Int32, Me.GetRefObject(discountProposalDetailOwnership.DiscountProposalHeader))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As DiscountProposalDetailOwnership

            Dim discountProposalDetailOwnership As DiscountProposalDetailOwnership = New DiscountProposalDetailOwnership

            discountProposalDetailOwnership.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("VehicleBrandCategory")) Then discountProposalDetailOwnership.VehicleBrandCategory = CType(dr("VehicleBrandCategory"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("VehicleBrandName")) Then discountProposalDetailOwnership.VehicleBrandName = dr("VehicleBrandName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("VehicleModel")) Then discountProposalDetailOwnership.VehicleModel = dr("VehicleModel").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Quantity")) Then discountProposalDetailOwnership.Quantity = CType(dr("Quantity"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then discountProposalDetailOwnership.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then discountProposalDetailOwnership.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then discountProposalDetailOwnership.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then discountProposalDetailOwnership.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then discountProposalDetailOwnership.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("DiscountProposalHeaderID")) Then
                discountProposalDetailOwnership.DiscountProposalHeader = New DiscountProposalHeader(CType(dr("DiscountProposalHeaderID"), Integer))
            End If

            Return discountProposalDetailOwnership

        End Function

        Private Sub SetTableName()

            If Not (GetType(DiscountProposalDetailOwnership) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(DiscountProposalDetailOwnership), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(DiscountProposalDetailOwnership).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

