
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : SparePartPacking Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2016 
'// ---------------------
'// $History      : $
'// Generated on 10/5/2016 - 9:24:12 AM
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

    Public Class SparePartPackingMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertSparePartPacking"
        Private m_UpdateStatement As String = "up_UpdateSparePartPacking"
        Private m_RetrieveStatement As String = "up_RetrieveSparePartPacking"
        Private m_RetrieveListStatement As String = "up_RetrieveSparePartPackingList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteSparePartPacking"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim sparePartPacking As SparePartPacking = Nothing
            While dr.Read

                sparePartPacking = Me.CreateObject(dr)

            End While

            Return sparePartPacking

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim sparePartPackingList As ArrayList = New ArrayList

            While dr.Read
                Dim sparePartPacking As SparePartPacking = Me.CreateObject(dr)
                sparePartPackingList.Add(sparePartPacking)
            End While

            Return sparePartPackingList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim sparePartPacking As SparePartPacking = CType(obj, SparePartPacking)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, sparePartPacking.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim sparePartPacking As SparePartPacking = CType(obj, SparePartPacking)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@InternalHUNo", DbType.AnsiString, sparePartPacking.InternalHUNo)
            DbCommandWrapper.AddInParameter("@PackMaterial", DbType.AnsiString, sparePartPacking.PackMaterial)
            DbCommandWrapper.AddInParameter("@PackMaterialDesc", DbType.AnsiString, sparePartPacking.PackMaterialDesc)
            DbCommandWrapper.AddInParameter("@LotCase", DbType.AnsiString, sparePartPacking.LotCase)
            DbCommandWrapper.AddInParameter("@Weight", DbType.Decimal, sparePartPacking.Weight)
            DbCommandWrapper.AddInParameter("@Volume", DbType.Decimal, sparePartPacking.Volume)
            DbCommandWrapper.AddInParameter("@TotalItem", DbType.Decimal, sparePartPacking.TotalItem)
            DbCommandWrapper.AddInParameter("@TotalQty", DbType.Decimal, sparePartPacking.TotalQty)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, sparePartPacking.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, sparePartPacking.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@SparePartDOExpeditionID", DbType.Int32, Me.GetRefObject(sparePartPacking.SparePartDOExpedition))

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

            Dim sparePartPacking As SparePartPacking = CType(obj, SparePartPacking)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, sparePartPacking.ID)
            DbCommandWrapper.AddInParameter("@InternalHUNo", DbType.AnsiString, sparePartPacking.InternalHUNo)
            DbCommandWrapper.AddInParameter("@PackMaterial", DbType.AnsiString, sparePartPacking.PackMaterial)
            DbCommandWrapper.AddInParameter("@PackMaterialDesc", DbType.AnsiString, sparePartPacking.PackMaterialDesc)
            DbCommandWrapper.AddInParameter("@LotCase", DbType.AnsiString, sparePartPacking.LotCase)
            DbCommandWrapper.AddInParameter("@Weight", DbType.Decimal, sparePartPacking.Weight)
            DbCommandWrapper.AddInParameter("@Volume", DbType.Decimal, sparePartPacking.Volume)
            DbCommandWrapper.AddInParameter("@TotalItem", DbType.Decimal, sparePartPacking.TotalItem)
            DbCommandWrapper.AddInParameter("@TotalQty", DbType.Decimal, sparePartPacking.TotalQty)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, sparePartPacking.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, sparePartPacking.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@SparePartDOExpeditionID", DbType.Int32, Me.GetRefObject(sparePartPacking.SparePartDOExpedition))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As SparePartPacking

            Dim sparePartPacking As SparePartPacking = New SparePartPacking

            sparePartPacking.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("InternalHUNo")) Then sparePartPacking.InternalHUNo = dr("InternalHUNo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PackMaterial")) Then sparePartPacking.PackMaterial = dr("PackMaterial").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PackMaterialDesc")) Then sparePartPacking.PackMaterialDesc = dr("PackMaterialDesc").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LotCase")) Then sparePartPacking.LotCase = dr("LotCase").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Weight")) Then sparePartPacking.Weight = CType(dr("Weight"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("Volume")) Then sparePartPacking.Volume = CType(dr("Volume"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("TotalItem")) Then sparePartPacking.TotalItem = CType(dr("TotalItem"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("TotalQty")) Then sparePartPacking.TotalQty = CType(dr("TotalQty"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then sparePartPacking.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then sparePartPacking.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then sparePartPacking.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then sparePartPacking.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then sparePartPacking.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("SparePartDOExpeditionID")) Then
                sparePartPacking.SparePartDOExpedition = New SparePartDOExpedition(CType(dr("SparePartDOExpeditionID"), Integer))
            End If

            Return sparePartPacking

        End Function

        Private Sub SetTableName()

            If Not (GetType(SparePartPacking) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(SparePartPacking), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(SparePartPacking).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

