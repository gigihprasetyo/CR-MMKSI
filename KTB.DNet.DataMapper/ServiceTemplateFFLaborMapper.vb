#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : ServiceTemplateFFLabor Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2005 
'// ---------------------
'// $History      : $
'// Generated on 11/30/2005 - 1:05:06 PM
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

    Public Class ServiceTemplateFFLaborMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertServiceTemplateFFLabor"
        Private m_UpdateStatement As String = "up_UpdateServiceTemplateFFLabor"
        Private m_RetrieveStatement As String = "up_RetrieveServiceTemplateFFLabor"
        Private m_RetrieveListStatement As String = "up_RetrieveServiceTemplateFFLaborList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteServiceTemplateFFLabor"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim ServiceTemplateFFLabor As ServiceTemplateFFLabor = Nothing
            While dr.Read

                ServiceTemplateFFLabor = Me.CreateObject(dr)

            End While

            Return ServiceTemplateFFLabor

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim ServiceTemplateFFLaborList As ArrayList = New ArrayList

            While dr.Read
                Dim ServiceTemplateFFLabor As ServiceTemplateFFLabor = Me.CreateObject(dr)
                ServiceTemplateFFLaborList.Add(ServiceTemplateFFLabor)
            End While

            Return ServiceTemplateFFLaborList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim ServiceTemplateFFLabor As ServiceTemplateFFLabor = CType(obj, ServiceTemplateFFLabor)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, ServiceTemplateFFLabor.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim ServiceTemplateFFLabor As ServiceTemplateFFLabor = CType(obj, ServiceTemplateFFLabor)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@Material", DbType.AnsiString, ServiceTemplateFFLabor.Material)
            'DbCommandWrapper.AddInParameter("@LaborDuration", DbType.Decimal, ServiceTemplateFFLabor.LaborDuration)
            DbCommandWrapper.AddInParameter("@LaborCost", DbType.Decimal, ServiceTemplateFFLabor.LaborCost)
            DbCommandWrapper.AddInParameter("@ValidFrom", DbType.DateTime, ServiceTemplateFFLabor.ValidFrom)
            'DbCommandWrapper.AddInParameter("@ValidTo", DbType.DateTime, ServiceTemplateFFLabor.ValidTo)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, ServiceTemplateFFLabor.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdatedBy", DbType.AnsiString, ServiceTemplateFFLabor.LastUpdatedBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, Me.GetRefObject(ServiceTemplateFFLabor.Dealer))
            'DbCommandWrapper.AddInParameter("@RecallCategoryID", DbType.Int32, Me.GetRefObject(ServiceTemplateFFLabor.RecallCategory))
            'DbCommandWrapper.AddInParameter("@VechileTypeID", DbType.Int32, Me.GetRefObject(ServiceTemplateFFLabor.VechileType))

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

            Dim ServiceTemplateFFLabor As ServiceTemplateFFLabor = CType(obj, ServiceTemplateFFLabor)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, ServiceTemplateFFLabor.ID)
            DbCommandWrapper.AddInParameter("@Material", DbType.AnsiString, ServiceTemplateFFLabor.Material)
            'DbCommandWrapper.AddInParameter("@LaborDuration", DbType.Decimal, ServiceTemplateFFLabor.LaborDuration)
            DbCommandWrapper.AddInParameter("@LaborCost", DbType.Decimal, ServiceTemplateFFLabor.LaborCost)
            DbCommandWrapper.AddInParameter("@ValidFrom", DbType.DateTime, ServiceTemplateFFLabor.ValidFrom)
            'DbCommandWrapper.AddInParameter("@ValidTo", DbType.DateTime, ServiceTemplateFFLabor.ValidTo)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, ServiceTemplateFFLabor.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, ServiceTemplateFFLabor.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, Me.GetRefObject(ServiceTemplateFFLabor.Dealer))
            'DbCommandWrapper.AddInParameter("@RecallCategoryID", DbType.Int32, Me.GetRefObject(ServiceTemplateFFLabor.RecallCategory))
            'DbCommandWrapper.AddInParameter("@VechileTypeID", DbType.Int16, Me.GetRefObject(ServiceTemplateFFLabor.VechileType))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As ServiceTemplateFFLabor

            Dim ServiceTemplateFFLabor As ServiceTemplateFFLabor = New ServiceTemplateFFLabor

            ServiceTemplateFFLabor.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Material")) Then ServiceTemplateFFLabor.Material = dr("Material").ToString
            'If Not dr.IsDBNull(dr.GetOrdinal("LaborDuration")) Then ServiceTemplateFFLabor.LaborDuration = CDec(dr("LaborDuration").ToString)
            If Not dr.IsDBNull(dr.GetOrdinal("LaborCost")) Then ServiceTemplateFFLabor.LaborCost = CDec(dr("LaborCost").ToString)
            If Not dr.IsDBNull(dr.GetOrdinal("ValidFrom")) Then ServiceTemplateFFLabor.ValidFrom = CType(dr("ValidFrom").ToString, DateTime)
            'If Not dr.IsDBNull(dr.GetOrdinal("ValidTo")) Then ServiceTemplateFFLabor.ValidTo = CType(dr("ValidTo").ToString, DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then ServiceTemplateFFLabor.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then ServiceTemplateFFLabor.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then ServiceTemplateFFLabor.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdatedBy")) Then ServiceTemplateFFLabor.LastUpdatedBy = dr("LastUpdatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdatedTime")) Then ServiceTemplateFFLabor.LastUpdatedTime = CType(dr("LastUpdatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then
                ServiceTemplateFFLabor.Dealer = New Dealer(CType(dr("DealerID"), Short))
            End If
            'If Not dr.IsDBNull(dr.GetOrdinal("RecallCategoryID")) Then
            '    ServiceTemplateFFLabor.RecallCategory = New RecallCategory(CType(dr("RecallCategoryID"), Integer))
            'End If
            'If Not dr.IsDBNull(dr.GetOrdinal("VechileTypeID")) Then
            '    ServiceTemplateFFLabor.VechileType = New VechileType(CType(dr("VechileTypeID"), Short))
            'End If

            Return ServiceTemplateFFLabor

        End Function

        Private Sub SetTableName()

            If Not (GetType(ServiceTemplateFFLabor) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(ServiceTemplateFFLabor), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(ServiceTemplateFFLabor).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

