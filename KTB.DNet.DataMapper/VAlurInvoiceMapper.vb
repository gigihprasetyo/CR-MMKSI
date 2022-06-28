
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : VAlurInvoice Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2008 
'// ---------------------
'// $History      : $
'// Generated on 12/3/2008 - 12:57:25 PM
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

    Public Class VAlurInvoiceMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertVAlurInvoice"
        Private m_UpdateStatement As String = "up_UpdateVAlurInvoice"
        Private m_RetrieveStatement As String = "up_RetrieveVAlurInvoice"
        Private m_RetrieveListStatement As String = "up_RetrieveVAlurInvoiceList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteVAlurInvoice"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim vAlurInvoice As VAlurInvoice = Nothing
            While dr.Read

                vAlurInvoice = Me.CreateObject(dr)

            End While

            Return vAlurInvoice

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim vAlurInvoiceList As ArrayList = New ArrayList

            While dr.Read
                Dim vAlurInvoice As VAlurInvoice = Me.CreateObject(dr)
                vAlurInvoiceList.Add(vAlurInvoice)
            End While

            Return vAlurInvoiceList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim vAlurInvoice As VAlurInvoice = CType(obj, VAlurInvoice)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, vAlurInvoice.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim vAlurInvoice As VAlurInvoice = CType(obj, VAlurInvoice)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@PKNumber", DbType.AnsiString, vAlurInvoice.PKNumber)
            DbCommandWrapper.AddInParameter("@ContractNumber", DbType.AnsiString, vAlurInvoice.ContractNumber)
            DbCommandWrapper.AddInParameter("@PONumber", DbType.AnsiString, vAlurInvoice.PONumber)
            DbCommandWrapper.AddInParameter("@SONumber", DbType.AnsiString, vAlurInvoice.SONumber)
            DbCommandWrapper.AddInParameter("@InvoiceNumber", DbType.AnsiString, vAlurInvoice.InvoiceNumber)
            DbCommandWrapper.AddInParameter("@PKQty", DbType.Int32, vAlurInvoice.PKQty)
            DbCommandWrapper.AddInParameter("@ContractQty", DbType.Int32, vAlurInvoice.ContractQty)
            DbCommandWrapper.AddInParameter("@POQty", DbType.Int32, vAlurInvoice.POQty)
            DbCommandWrapper.AddInParameter("@InvQty", DbType.Int32, vAlurInvoice.InvQty)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, vAlurInvoice.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, vAlurInvoice.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


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

            Dim vAlurInvoice As VAlurInvoice = CType(obj, VAlurInvoice)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, vAlurInvoice.ID)
            DbCommandWrapper.AddInParameter("@PKNumber", DbType.AnsiString, vAlurInvoice.PKNumber)
            DbCommandWrapper.AddInParameter("@ContractNumber", DbType.AnsiString, vAlurInvoice.ContractNumber)
            DbCommandWrapper.AddInParameter("@PONumber", DbType.AnsiString, vAlurInvoice.PONumber)
            DbCommandWrapper.AddInParameter("@SONumber", DbType.AnsiString, vAlurInvoice.SONumber)
            DbCommandWrapper.AddInParameter("@InvoiceNumber", DbType.AnsiString, vAlurInvoice.InvoiceNumber)
            DbCommandWrapper.AddInParameter("@PKQty", DbType.Int32, vAlurInvoice.PKQty)
            DbCommandWrapper.AddInParameter("@ContractQty", DbType.Int32, vAlurInvoice.ContractQty)
            DbCommandWrapper.AddInParameter("@POQty", DbType.Int32, vAlurInvoice.POQty)
            DbCommandWrapper.AddInParameter("@InvQty", DbType.Int32, vAlurInvoice.InvQty)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, vAlurInvoice.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, vAlurInvoice.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As VAlurInvoice

            Dim vAlurInvoice As VAlurInvoice = New VAlurInvoice

            vAlurInvoice.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("PKNumber")) Then vAlurInvoice.PKNumber = dr("PKNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ContractNumber")) Then vAlurInvoice.ContractNumber = dr("ContractNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PONumber")) Then vAlurInvoice.PONumber = dr("PONumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SONumber")) Then vAlurInvoice.SONumber = dr("SONumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("InvoiceNumber")) Then vAlurInvoice.InvoiceNumber = dr("InvoiceNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PKQty")) Then vAlurInvoice.PKQty = CType(dr("PKQty"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("ContractQty")) Then vAlurInvoice.ContractQty = CType(dr("ContractQty"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("POQty")) Then vAlurInvoice.POQty = CType(dr("POQty"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("InvQty")) Then vAlurInvoice.InvQty = CType(dr("InvQty"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then vAlurInvoice.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then vAlurInvoice.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then vAlurInvoice.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then vAlurInvoice.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then vAlurInvoice.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            Return vAlurInvoice

        End Function

        Private Sub SetTableName()

            If Not (GetType(VAlurInvoice) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(VAlurInvoice), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(VAlurInvoice).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

