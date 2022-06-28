
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : Mitrais Team
'// PURPOSE       : SparePartPriceList Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 28/03/2018 - 11:49:55
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

    Public Class VWI_SparePartPriceListMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertVWI_SparePartPriceList"
        Private m_UpdateStatement As String = "up_UpdateVWI_SparePartPriceList"
        Private m_RetrieveStatement As String = "up_RetrieveVWI_SparePartPriceList"
        Private m_RetrieveListStatement As String = "up_RetrieveVWI_SparePartPriceListList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteVWI_SparePartPriceList"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim sparePartPriceList As VWI_SparePartPriceList = Nothing
            While dr.Read

                sparePartPriceList = Me.CreateObject(dr)

            End While

            Return sparePartPriceList

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim sparePartPriceListList As ArrayList = New ArrayList

            While dr.Read
                Dim sparePartPriceList As VWI_SparePartPriceList = Me.CreateObject(dr)
                sparePartPriceListList.Add(sparePartPriceList)
            End While

            Return sparePartPriceListList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim sparePartPriceList As VWI_SparePartPriceList = CType(obj, VWI_SparePartPriceList)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, sparePartPriceList.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim sparePartPriceList As VWI_SparePartPriceList = CType(obj, VWI_SparePartPriceList)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@PartNumber", DbType.AnsiString, sparePartPriceList.PartNumber)
            DbCommandWrapper.AddInParameter("@PartName", DbType.AnsiString, sparePartPriceList.PartName)
            DbCommandWrapper.AddInParameter("@UoM", DbType.AnsiString, sparePartPriceList.UoM)
            DbCommandWrapper.AddInParameter("@RetailPrice", DbType.Currency, sparePartPriceList.RetailPrice)
            DbCommandWrapper.AddInParameter("@ActiveStatus", DbType.Int16, sparePartPriceList.ActiveStatus)


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

            Dim sparePartPriceList As VWI_SparePartPriceList = CType(obj, VWI_SparePartPriceList)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, sparePartPriceList.ID)
            DbCommandWrapper.AddInParameter("@PartNumber", DbType.AnsiString, sparePartPriceList.PartNumber)
            DbCommandWrapper.AddInParameter("@PartName", DbType.AnsiString, sparePartPriceList.PartName)
            DbCommandWrapper.AddInParameter("@UoM", DbType.AnsiString, sparePartPriceList.UoM)
            DbCommandWrapper.AddInParameter("@RetailPrice", DbType.Currency, sparePartPriceList.RetailPrice)
            DbCommandWrapper.AddInParameter("@ActiveStatus", DbType.Int16, sparePartPriceList.ActiveStatus)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As VWI_SparePartPriceList

            Dim sparePartPriceList As VWI_SparePartPriceList = New VWI_SparePartPriceList

            sparePartPriceList.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("PartNumber")) Then sparePartPriceList.PartNumber = dr("PartNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PartName")) Then sparePartPriceList.PartName = dr("PartName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("UoM")) Then sparePartPriceList.UoM = dr("UoM").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RetailPrice")) Then sparePartPriceList.RetailPrice = CType(dr("RetailPrice"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("ActiveStatus")) Then sparePartPriceList.ActiveStatus = CType(dr("ActiveStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then sparePartPriceList.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            Return sparePartPriceList

        End Function

        Private Sub SetTableName()

            If Not (GetType(VWI_SparePartPriceList) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(VWI_SparePartPriceList), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(VWI_SparePartPriceList).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

