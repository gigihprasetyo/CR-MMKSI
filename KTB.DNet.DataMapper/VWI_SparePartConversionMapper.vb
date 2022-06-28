
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : VWI_SparePartConversion Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 28/03/2018 - 9:04:29
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

    Public Class VWI_SparePartConversionMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertVWI_SparePartConversion"
        Private m_UpdateStatement As String = "up_UpdateVWI_SparePartConversion"
        Private m_RetrieveStatement As String = "up_RetrieveVWI_SparePartConversion"
        Private m_RetrieveListStatement As String = "up_RetrieveVWI_SparePartConversionList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteVWI_SparePartConversion"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim VWI_SparePartConversion As VWI_SparePartConversion = Nothing
            While dr.Read

                VWI_SparePartConversion = Me.CreateObject(dr)

            End While

            Return VWI_SparePartConversion

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim VWI_SparePartConversionList As ArrayList = New ArrayList

            While dr.Read
                Dim VWI_SparePartConversion As VWI_SparePartConversion = Me.CreateObject(dr)
                VWI_SparePartConversionList.Add(VWI_SparePartConversion)
            End While

            Return VWI_SparePartConversionList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim VWI_SparePartConversion As VWI_SparePartConversion = CType(obj, VWI_SparePartConversion)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, VWI_SparePartConversion.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim VWI_SparePartConversion As VWI_SparePartConversion = CType(obj, VWI_SparePartConversion)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@PartNumber", DbType.AnsiString, VWI_SparePartConversion.PartNumber)
            DbCommandWrapper.AddInParameter("@PartName", DbType.AnsiString, VWI_SparePartConversion.PartName)
            DbCommandWrapper.AddInParameter("@UOMFrom", DbType.AnsiString, VWI_SparePartConversion.UOMFrom)
            DbCommandWrapper.AddInParameter("@UOMTo", DbType.AnsiString, VWI_SparePartConversion.UOMTo)
            DbCommandWrapper.AddInParameter("@Qty", DbType.Int32, VWI_SparePartConversion.Qty)
            DbCommandWrapper.AddInParameter("@PartNumberReff", DbType.AnsiString, VWI_SparePartConversion.PartNumberReff)


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

            Dim VWI_SparePartConversion As VWI_SparePartConversion = CType(obj, VWI_SparePartConversion)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, VWI_SparePartConversion.ID)
            DbCommandWrapper.AddInParameter("@PartNumber", DbType.AnsiString, VWI_SparePartConversion.PartNumber)
            DbCommandWrapper.AddInParameter("@PartName", DbType.AnsiString, VWI_SparePartConversion.PartName)
            DbCommandWrapper.AddInParameter("@UOMFrom", DbType.AnsiString, VWI_SparePartConversion.UOMFrom)
            DbCommandWrapper.AddInParameter("@UOMTo", DbType.AnsiString, VWI_SparePartConversion.UOMTo)
            DbCommandWrapper.AddInParameter("@Qty", DbType.Int32, VWI_SparePartConversion.Qty)
            DbCommandWrapper.AddInParameter("@PartNumberReff", DbType.AnsiString, VWI_SparePartConversion.PartNumberReff)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As VWI_SparePartConversion

            Dim VWI_SparePartConversion As VWI_SparePartConversion = New VWI_SparePartConversion

            VWI_SparePartConversion.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("PartNumber")) Then VWI_SparePartConversion.PartNumber = dr("PartNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PartName")) Then VWI_SparePartConversion.PartName = dr("PartName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("TypeCode")) Then VWI_SparePartConversion.TypeCode = dr("TypeCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ModelCode")) Then VWI_SparePartConversion.ModelCode = dr("ModelCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("UOMFrom")) Then VWI_SparePartConversion.UOMFrom = dr("UOMFrom").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("UOMTo")) Then VWI_SparePartConversion.UOMTo = dr("UOMTo").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Qty")) Then VWI_SparePartConversion.Qty = CType(dr("Qty"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("PartNumberReff")) Then VWI_SparePartConversion.PartNumberReff = dr("PartNumberReff").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then VWI_SparePartConversion.Status = CType(dr("Status"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("ProductType")) Then VWI_SparePartConversion.ProductType = dr("ProductType").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then VWI_SparePartConversion.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            Return VWI_SparePartConversion

        End Function

        Private Sub SetTableName()

            If Not (GetType(VWI_SparePartConversion) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(VWI_SparePartConversion), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(VWI_SparePartConversion).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

