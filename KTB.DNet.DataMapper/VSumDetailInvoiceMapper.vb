
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : VSumDetailInvoice Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2008 
'// ---------------------
'// $History      : $
'// Generated on 12/4/2008 - 5:11:19 PM
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

    Public Class VSumDetailInvoiceMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertVSumDetailInvoice"
        Private m_UpdateStatement As String = "up_UpdateVSumDetailInvoice"
        Private m_RetrieveStatement As String = "up_RetrieveVSumDetailInvoice"
        Private m_RetrieveListStatement As String = "up_RetrieveVSumDetailInvoiceList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteVSumDetailInvoice"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim vSumDetailInvoice As VSumDetailInvoice = Nothing
            While dr.Read

                vSumDetailInvoice = Me.CreateObject(dr)

            End While

            Return vSumDetailInvoice

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim vSumDetailInvoiceList As ArrayList = New ArrayList

            While dr.Read
                Dim vSumDetailInvoice As VSumDetailInvoice = Me.CreateObject(dr)
                vSumDetailInvoiceList.Add(vSumDetailInvoice)
            End While

            Return vSumDetailInvoiceList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim vSumDetailInvoice As VSumDetailInvoice = CType(obj, VSumDetailInvoice)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, vSumDetailInvoice.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim vSumDetailInvoice As VSumDetailInvoice = CType(obj, VSumDetailInvoice)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@MaterialNumber", DbType.AnsiString, vSumDetailInvoice.MaterialNumber)
            DbCommandWrapper.AddInParameter("@MaterialDescription", DbType.AnsiString, vSumDetailInvoice.MaterialDescription)
            DbCommandWrapper.AddInParameter("@BilledQty", DbType.Int64, vSumDetailInvoice.BilledQty)
            DbCommandWrapper.AddInParameter("@ItemAmount", DbType.Currency, vSumDetailInvoice.ItemAmount)
            DbCommandWrapper.AddInParameter("@PPH22", DbType.Currency, vSumDetailInvoice.PPH22)
            DbCommandWrapper.AddInParameter("@Interest", DbType.Currency, vSumDetailInvoice.Interest)


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

            Dim vSumDetailInvoice As VSumDetailInvoice = CType(obj, VSumDetailInvoice)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, vSumDetailInvoice.ID)
            DbCommandWrapper.AddInParameter("@MaterialNumber", DbType.AnsiString, vSumDetailInvoice.MaterialNumber)
            DbCommandWrapper.AddInParameter("@MaterialDescription", DbType.AnsiString, vSumDetailInvoice.MaterialDescription)
            DbCommandWrapper.AddInParameter("@BilledQty", DbType.Int64, vSumDetailInvoice.BilledQty)
            DbCommandWrapper.AddInParameter("@ItemAmount", DbType.Currency, vSumDetailInvoice.ItemAmount)
            DbCommandWrapper.AddInParameter("@PPH22", DbType.Currency, vSumDetailInvoice.PPH22)
            DbCommandWrapper.AddInParameter("@Interest", DbType.Currency, vSumDetailInvoice.Interest)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As VSumDetailInvoice

            Dim vSumDetailInvoice As VSumDetailInvoice = New VSumDetailInvoice

            vSumDetailInvoice.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("MaterialNumber")) Then vSumDetailInvoice.MaterialNumber = dr("MaterialNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("MaterialDescription")) Then vSumDetailInvoice.MaterialDescription = dr("MaterialDescription").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("BilledQty")) Then vSumDetailInvoice.BilledQty = CType(dr("BilledQty"), Long)
            If Not dr.IsDBNull(dr.GetOrdinal("ItemAmount")) Then vSumDetailInvoice.ItemAmount = CType(dr("ItemAmount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("PPH22")) Then vSumDetailInvoice.PPH22 = CType(dr("PPH22"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("Interest")) Then vSumDetailInvoice.Interest = CType(dr("Interest"), Decimal)

            Return vSumDetailInvoice

        End Function

        Private Sub SetTableName()

            If Not (GetType(VSumDetailInvoice) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(VSumDetailInvoice), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(VSumDetailInvoice).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

