
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : VWI_ServiceTemplate Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 26/07/2018 - 11:59:14
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

    Public Class VWI_ServiceTemplateMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertVWI_ServiceTemplate"
        Private m_UpdateStatement As String = "up_UpdateVWI_ServiceTemplate"
        Private m_RetrieveStatement As String = "up_RetrieveVWI_ServiceTemplate"
        Private m_RetrieveListStatement As String = "up_RetrieveVWI_ServiceTemplateList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteVWI_ServiceTemplate"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim VWI_ServiceTemplate As VWI_ServiceTemplate = Nothing
            While dr.Read

                VWI_ServiceTemplate = Me.CreateObject(dr)

            End While

            Return VWI_ServiceTemplate

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim VWI_ServiceTemplateList As ArrayList = New ArrayList

            While dr.Read
                Dim VWI_ServiceTemplate As VWI_ServiceTemplate = Me.CreateObject(dr)
                VWI_ServiceTemplateList.Add(VWI_ServiceTemplate)
            End While

            Return VWI_ServiceTemplateList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim VWI_ServiceTemplate As VWI_ServiceTemplate = CType(obj, VWI_ServiceTemplate)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, VWI_ServiceTemplate.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim VWI_ServiceTemplate As VWI_ServiceTemplate = CType(obj, VWI_ServiceTemplate)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@SVCTMPTParentGroup", DbType.AnsiString, VWI_ServiceTemplate.SVCTMPTParentGroup)
            DbCommandWrapper.AddInParameter("@SVCTMPTParent", DbType.AnsiString, VWI_ServiceTemplate.SVCTMPTParent)
            DbCommandWrapper.AddInParameter("@SVCTMPTSubGroup", DbType.AnsiString, VWI_ServiceTemplate.SVCTMPTSubGroup)
            DbCommandWrapper.AddInParameter("@SvcTemplateCode", DbType.AnsiString, VWI_ServiceTemplate.SvcTemplateCode)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, VWI_ServiceTemplate.Description)
            DbCommandWrapper.AddInParameter("@DNETKind", DbType.AnsiString, VWI_ServiceTemplate.DNETKind)
            DbCommandWrapper.AddInParameter("@IntervalKM", DbType.Int32, VWI_ServiceTemplate.IntervalKM)
            DbCommandWrapper.AddInParameter("@ServiceTemplateActivityDesc", DbType.AnsiString, VWI_ServiceTemplate.ServiceTemplateActivityDesc)
            DbCommandWrapper.AddInParameter("@Duration", DbType.Double, VWI_ServiceTemplate.Duration)
            DbCommandWrapper.AddInParameter("@Item", DbType.AnsiString, VWI_ServiceTemplate.Item)
            DbCommandWrapper.AddInParameter("@ItemDesc", DbType.AnsiString, VWI_ServiceTemplate.ItemDesc)
            DbCommandWrapper.AddInParameter("@Qty", DbType.Double, VWI_ServiceTemplate.Qty)
            DbCommandWrapper.AddInParameter("@Price", DbType.Currency, VWI_ServiceTemplate.Price)


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

            Dim VWI_ServiceTemplate As VWI_ServiceTemplate = CType(obj, VWI_ServiceTemplate)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, VWI_ServiceTemplate.ID)
            DbCommandWrapper.AddInParameter("@SVCTMPTParentGroup", DbType.AnsiString, VWI_ServiceTemplate.SVCTMPTParentGroup)
            DbCommandWrapper.AddInParameter("@SVCTMPTParent", DbType.AnsiString, VWI_ServiceTemplate.SVCTMPTParent)
            DbCommandWrapper.AddInParameter("@SVCTMPTSubGroup", DbType.AnsiString, VWI_ServiceTemplate.SVCTMPTSubGroup)
            DbCommandWrapper.AddInParameter("@SvcTemplateCode", DbType.AnsiString, VWI_ServiceTemplate.SvcTemplateCode)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, VWI_ServiceTemplate.Description)
            DbCommandWrapper.AddInParameter("@DNETKind", DbType.AnsiString, VWI_ServiceTemplate.DNETKind)
            DbCommandWrapper.AddInParameter("@IntervalKM", DbType.Int32, VWI_ServiceTemplate.IntervalKM)
            DbCommandWrapper.AddInParameter("@ServiceTemplateActivityDesc", DbType.AnsiString, VWI_ServiceTemplate.ServiceTemplateActivityDesc)
            DbCommandWrapper.AddInParameter("@Duration", DbType.Double, VWI_ServiceTemplate.Duration)
            DbCommandWrapper.AddInParameter("@Item", DbType.AnsiString, VWI_ServiceTemplate.Item)
            DbCommandWrapper.AddInParameter("@ItemDesc", DbType.AnsiString, VWI_ServiceTemplate.ItemDesc)
            DbCommandWrapper.AddInParameter("@Qty", DbType.Double, VWI_ServiceTemplate.Qty)
            DbCommandWrapper.AddInParameter("@Price", DbType.Currency, VWI_ServiceTemplate.Price)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As VWI_ServiceTemplate

            Dim VWI_ServiceTemplate As VWI_ServiceTemplate = New VWI_ServiceTemplate

            'VWI_ServiceTemplate.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("SVCTMPTParentGroup")) Then VWI_ServiceTemplate.SVCTMPTParentGroup = dr("SVCTMPTParentGroup").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SVCTMPTParent")) Then VWI_ServiceTemplate.SVCTMPTParent = dr("SVCTMPTParent").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SVCTMPTSubGroup")) Then VWI_ServiceTemplate.SVCTMPTSubGroup = dr("SVCTMPTSubGroup").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SvcTemplateCode")) Then VWI_ServiceTemplate.SvcTemplateCode = dr("SvcTemplateCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Description")) Then VWI_ServiceTemplate.Description = dr("Description").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DNETKind")) Then VWI_ServiceTemplate.DNETKind = dr("DNETKind").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("IntervalKM")) Then VWI_ServiceTemplate.IntervalKM = CType(dr("IntervalKM"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("ServiceTemplateActivityDesc")) Then VWI_ServiceTemplate.ServiceTemplateActivityDesc = dr("ServiceTemplateActivityDesc").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Duration")) Then VWI_ServiceTemplate.Duration = CType(dr("Duration"), Double)
            If Not dr.IsDBNull(dr.GetOrdinal("Item")) Then VWI_ServiceTemplate.Item = dr("Item").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ItemDesc")) Then VWI_ServiceTemplate.ItemDesc = dr("ItemDesc").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Qty")) Then VWI_ServiceTemplate.Qty = CType(dr("Qty"), Double)
            If Not dr.IsDBNull(dr.GetOrdinal("Price")) Then VWI_ServiceTemplate.Price = CType(dr("Price"), Decimal)

            Return VWI_ServiceTemplate

        End Function

        Private Sub SetTableName()

            If Not (GetType(VWI_ServiceTemplate) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(VWI_ServiceTemplate), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(VWI_ServiceTemplate).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

