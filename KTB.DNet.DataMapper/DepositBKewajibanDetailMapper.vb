
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : DepositBKewajibanDetail Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2016 
'// ---------------------
'// $History      : $
'// Generated on 3/17/2016 - 8:20:54 AM
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

    Public Class DepositBKewajibanDetailMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertDepositBKewajibanDetail"
        Private m_UpdateStatement As String = "up_UpdateDepositBKewajibanDetail"
        Private m_RetrieveStatement As String = "up_RetrieveDepositBKewajibanDetail"
        Private m_RetrieveListStatement As String = "up_RetrieveDepositBKewajibanDetailList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteDepositBKewajibanDetail"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim depositBKewajibanDetail As DepositBKewajibanDetail = Nothing
            While dr.Read

                depositBKewajibanDetail = Me.CreateObject(dr)

            End While

            Return depositBKewajibanDetail

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim depositBKewajibanDetailList As ArrayList = New ArrayList

            While dr.Read
                Dim depositBKewajibanDetail As DepositBKewajibanDetail = Me.CreateObject(dr)
                depositBKewajibanDetailList.Add(depositBKewajibanDetail)
            End While

            Return depositBKewajibanDetailList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim depositBKewajibanDetail As DepositBKewajibanDetail = CType(obj, DepositBKewajibanDetail)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, depositBKewajibanDetail.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim depositBKewajibanDetail As DepositBKewajibanDetail = CType(obj, DepositBKewajibanDetail)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@Qty", DbType.Int16, depositBKewajibanDetail.Qty)
            DbCommandWrapper.AddInParameter("@Harga", DbType.Currency, depositBKewajibanDetail.Harga)
            DbCommandWrapper.AddInParameter("@Tax", DbType.Currency, depositBKewajibanDetail.Tax)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, depositBKewajibanDetail.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, depositBKewajibanDetail.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@DepositBKewajibanHeaderID", DbType.Int32, Me.GetRefObject(depositBKewajibanDetail.DepositBKewajibanHeader))
            DbCommandWrapper.AddInParameter("@EquipmentMasterID", DbType.Int32, Me.GetRefObject(depositBKewajibanDetail.EquipmentMaster))
            DbCommandWrapper.AddInParameter("@SparePartMasterID", DbType.Int32, Me.GetRefObject(depositBKewajibanDetail.SparePartMaster))

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

            Dim depositBKewajibanDetail As DepositBKewajibanDetail = CType(obj, DepositBKewajibanDetail)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, depositBKewajibanDetail.ID)
            DbCommandWrapper.AddInParameter("@Qty", DbType.Int16, depositBKewajibanDetail.Qty)
            DbCommandWrapper.AddInParameter("@Harga", DbType.Currency, depositBKewajibanDetail.Harga)
            DbCommandWrapper.AddInParameter("@Tax", DbType.Currency, depositBKewajibanDetail.Tax)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, depositBKewajibanDetail.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, depositBKewajibanDetail.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@DepositBKewajibanHeaderID", DbType.Int32, Me.GetRefObject(depositBKewajibanDetail.DepositBKewajibanHeader))
            DbCommandWrapper.AddInParameter("@EquipmentMasterID", DbType.Int32, Me.GetRefObject(depositBKewajibanDetail.EquipmentMaster))
            DbCommandWrapper.AddInParameter("@SparePartMasterID", DbType.Int32, Me.GetRefObject(depositBKewajibanDetail.SparePartMaster))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As DepositBKewajibanDetail

            Dim depositBKewajibanDetail As DepositBKewajibanDetail = New DepositBKewajibanDetail

            depositBKewajibanDetail.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Qty")) Then depositBKewajibanDetail.Qty = CType(dr("Qty"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("Harga")) Then depositBKewajibanDetail.Harga = CType(dr("Harga"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("Tax")) Then depositBKewajibanDetail.Tax = CType(dr("Tax"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then depositBKewajibanDetail.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then depositBKewajibanDetail.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then depositBKewajibanDetail.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then depositBKewajibanDetail.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then depositBKewajibanDetail.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("DepositBKewajibanHeaderID")) Then
                depositBKewajibanDetail.DepositBKewajibanHeader = New DepositBKewajibanHeader(CType(dr("DepositBKewajibanHeaderID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("EquipmentMasterID")) Then
                depositBKewajibanDetail.EquipmentMaster = New EquipmentMaster(CType(dr("EquipmentMasterID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("SparePartMasterID")) Then
                depositBKewajibanDetail.SparePartMaster = New SparePartMaster(CType(dr("SparePartMasterID"), Integer))
            End If

            Return depositBKewajibanDetail

        End Function

        Private Sub SetTableName()

            If Not (GetType(DepositBKewajibanDetail) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(DepositBKewajibanDetail), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(DepositBKewajibanDetail).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

