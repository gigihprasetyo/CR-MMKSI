
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : BabitMasterPrice Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2019 
'// ---------------------
'// $History      : $
'// Generated on 20/06/2019 - 8:28:01
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

    Public Class BabitMasterPriceMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertBabitMasterPrice"
        Private m_UpdateStatement As String = "up_UpdateBabitMasterPrice"
        Private m_RetrieveStatement As String = "up_RetrieveBabitMasterPrice"
        Private m_RetrieveListStatement As String = "up_RetrieveBabitMasterPriceList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteBabitMasterPrice"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim babitMasterPrice As BabitMasterPrice = Nothing
            While dr.Read

                babitMasterPrice = Me.CreateObject(dr)

            End While

            Return babitMasterPrice

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim babitMasterPriceList As ArrayList = New ArrayList

            While dr.Read
                Dim babitMasterPrice As BabitMasterPrice = Me.CreateObject(dr)
                babitMasterPriceList.Add(babitMasterPrice)
            End While

            Return babitMasterPriceList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim babitMasterPrice As BabitMasterPrice = CType(obj, BabitMasterPrice)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, babitMasterPrice.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim babitMasterPrice As BabitMasterPrice = CType(obj, BabitMasterPrice)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@ValidFrom", DbType.DateTime, babitMasterPrice.ValidFrom)
            DbCommandWrapper.AddInParameter("@ValidTo", DbType.DateTime, babitMasterPrice.ValidTo)
            DbCommandWrapper.AddInParameter("@UnitPrice", DbType.Currency, babitMasterPrice.UnitPrice)
            DbCommandWrapper.AddInParameter("@SpecialCategoryFlag", DbType.Int16, babitMasterPrice.SpecialCategoryFlag)
            DbCommandWrapper.AddInParameter("@SpecialFlag", DbType.Int16, babitMasterPrice.SpecialFlag)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, babitMasterPrice.Status)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, babitMasterPrice.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, babitMasterPrice.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            'DbCommandWrapper.AddInParameter("@SubCategoryVehicleID", DbType.Int16, babitMasterPrice.SubCategoryVehicleID)
            DbCommandWrapper.AddInParameter("@SubCategoryVehicleID", DbType.Int32, Me.GetRefObject(babitMasterPrice.SubCategoryVehicle))

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

            Dim babitMasterPrice As BabitMasterPrice = CType(obj, BabitMasterPrice)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, babitMasterPrice.ID)
            DbCommandWrapper.AddInParameter("@ValidFrom", DbType.DateTime, babitMasterPrice.ValidFrom)
            DbCommandWrapper.AddInParameter("@ValidTo", DbType.DateTime, babitMasterPrice.ValidTo)
            DbCommandWrapper.AddInParameter("@UnitPrice", DbType.Currency, babitMasterPrice.UnitPrice)
            DbCommandWrapper.AddInParameter("@SpecialCategoryFlag", DbType.Int16, babitMasterPrice.SpecialCategoryFlag)
            DbCommandWrapper.AddInParameter("@SpecialFlag", DbType.Int16, babitMasterPrice.SpecialFlag)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, babitMasterPrice.Status)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, babitMasterPrice.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, babitMasterPrice.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            'DbCommandWrapper.AddInParameter("@SubCategoryVehicleID", DbType.Int16, babitMasterPrice.SubCategoryVehicleID)
            DbCommandWrapper.AddInParameter("@SubCategoryVehicleID", DbType.Int32, Me.GetRefObject(babitMasterPrice.SubCategoryVehicle))


            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As BabitMasterPrice

            Dim babitMasterPrice As BabitMasterPrice = New BabitMasterPrice

            babitMasterPrice.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("ValidFrom")) Then babitMasterPrice.ValidFrom = CType(dr("ValidFrom"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("ValidTo")) Then babitMasterPrice.ValidTo = CType(dr("ValidTo"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("UnitPrice")) Then babitMasterPrice.UnitPrice = CType(dr("UnitPrice"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("SpecialCategoryFlag")) Then babitMasterPrice.SpecialCategoryFlag = CType(dr("SpecialCategoryFlag"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("SpecialFlag")) Then babitMasterPrice.SpecialFlag = CType(dr("SpecialFlag"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then babitMasterPrice.Status = CType(dr("Status"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then babitMasterPrice.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then babitMasterPrice.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then babitMasterPrice.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then babitMasterPrice.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then babitMasterPrice.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            'If Not dr.IsDBNull(dr.GetOrdinal("SubCategoryVehicleID")) Then babitMasterPrice.SubCategoryVehicleID = CType(dr("SubCategoryVehicleID"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("SubCategoryVehicleID")) Then
                babitMasterPrice.SubCategoryVehicle = New SubCategoryVehicle(CType(dr("SubCategoryVehicleID"), Short))
            End If

            Return babitMasterPrice

        End Function

        Private Sub SetTableName()

            If Not (GetType(BabitMasterPrice) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(BabitMasterPrice), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(BabitMasterPrice).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

