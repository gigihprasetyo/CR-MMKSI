
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : MItrais Team
'// PURPOSE       : VWI_PartShop2 Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 23/04/2018 - 6:42:54
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

    Public Class VWI_PartShopMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertVWI_PartShop2"
        Private m_UpdateStatement As String = "up_UpdateVWI_PartShop2"
        Private m_RetrieveStatement As String = "up_RetrieveVWI_PartShop2"
        Private m_RetrieveListStatement As String = "up_RetrieveVWI_PartShop2List"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteVWI_PartShop2"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim vWI_PartShop2 As VWI_PartShop = Nothing
            While dr.Read

                vWI_PartShop2 = Me.CreateObject(dr)

            End While

            Return vWI_PartShop2

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim vWI_PartShop2List As ArrayList = New ArrayList

            While dr.Read
                Dim vWI_PartShop2 As VWI_PartShop = Me.CreateObject(dr)
                vWI_PartShop2List.Add(vWI_PartShop2)
            End While

            Return vWI_PartShop2List

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim vWI_PartShop2 As VWI_PartShop = CType(obj, VWI_PartShop)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, vWI_PartShop2.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim vWI_PartShop2 As VWI_PartShop = CType(obj, VWI_PartShop)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, vWI_PartShop2.DealerCode)
            DbCommandWrapper.AddInParameter("@CityCode", DbType.AnsiString, vWI_PartShop2.CityCode)
            DbCommandWrapper.AddInParameter("@PartShopCode", DbType.AnsiString, vWI_PartShop2.PartShopCode)
            DbCommandWrapper.AddInParameter("@OldPartShopCode", DbType.AnsiString, vWI_PartShop2.OldPartShopCode)
            DbCommandWrapper.AddInParameter("@Name", DbType.AnsiString, vWI_PartShop2.Name)
            DbCommandWrapper.AddInParameter("@Address", DbType.AnsiString, vWI_PartShop2.Address)
            DbCommandWrapper.AddInParameter("@Phone", DbType.AnsiString, vWI_PartShop2.Phone)
            DbCommandWrapper.AddInParameter("@Fax", DbType.AnsiString, vWI_PartShop2.Fax)
            DbCommandWrapper.AddInParameter("@Email", DbType.AnsiString, vWI_PartShop2.Email)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int32, vWI_PartShop2.Status)


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

            Dim vWI_PartShop2 As VWI_PartShop = CType(obj, VWI_PartShop)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, vWI_PartShop2.ID)
            DbCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, vWI_PartShop2.DealerCode)
            DbCommandWrapper.AddInParameter("@CityCode", DbType.AnsiString, vWI_PartShop2.CityCode)
            DbCommandWrapper.AddInParameter("@PartShopCode", DbType.AnsiString, vWI_PartShop2.PartShopCode)
            DbCommandWrapper.AddInParameter("@OldPartShopCode", DbType.AnsiString, vWI_PartShop2.OldPartShopCode)
            DbCommandWrapper.AddInParameter("@Name", DbType.AnsiString, vWI_PartShop2.Name)
            DbCommandWrapper.AddInParameter("@Address", DbType.AnsiString, vWI_PartShop2.Address)
            DbCommandWrapper.AddInParameter("@Phone", DbType.AnsiString, vWI_PartShop2.Phone)
            DbCommandWrapper.AddInParameter("@Fax", DbType.AnsiString, vWI_PartShop2.Fax)
            DbCommandWrapper.AddInParameter("@Email", DbType.AnsiString, vWI_PartShop2.Email)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int32, vWI_PartShop2.Status)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As VWI_PartShop

            Dim vWI_PartShop2 As VWI_PartShop = New VWI_PartShop

            vWI_PartShop2.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerCode")) Then vWI_PartShop2.DealerCode = dr("DealerCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CityCode")) Then vWI_PartShop2.CityCode = dr("CityCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PartShopCode")) Then vWI_PartShop2.PartShopCode = dr("PartShopCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("OldPartShopCode")) Then vWI_PartShop2.OldPartShopCode = dr("OldPartShopCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Name")) Then vWI_PartShop2.Name = dr("Name").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Address")) Then vWI_PartShop2.Address = dr("Address").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Phone")) Then vWI_PartShop2.Phone = dr("Phone").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Fax")) Then vWI_PartShop2.Fax = dr("Fax").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Email")) Then vWI_PartShop2.Email = dr("Email").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then vWI_PartShop2.Status = CType(dr("Status"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then vWI_PartShop2.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            Return vWI_PartShop2

        End Function

        Private Sub SetTableName()

            If Not (GetType(VWI_PartShop) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(VWI_PartShop), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(VWI_PartShop).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

