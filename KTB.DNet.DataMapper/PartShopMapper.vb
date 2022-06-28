
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : PartShop Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2011 
'// ---------------------
'// $History      : $
'// Generated on 7/15/2011 - 9:19:12 AM
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

    Public Class PartShopMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertPartShop"
        Private m_UpdateStatement As String = "up_UpdatePartShop"
        Private m_RetrieveStatement As String = "up_RetrievePartShop"
        Private m_RetrieveListStatement As String = "up_RetrievePartShopList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeletePartShop"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim partShop As PartShop = Nothing
            While dr.Read

                partShop = Me.CreateObject(dr)

            End While

            Return partShop

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim partShopList As ArrayList = New ArrayList

            While dr.Read
                Dim partShop As PartShop = Me.CreateObject(dr)
                partShopList.Add(partShop)
            End While

            Return partShopList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim partShop As PartShop = CType(obj, PartShop)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, partShop.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim partShop As PartShop = CType(obj, PartShop)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@PartShopCode", DbType.AnsiString, partShop.PartShopCode)
            DbCommandWrapper.AddInParameter("@Name", DbType.AnsiString, partShop.Name)
            DbCommandWrapper.AddInParameter("@Address", DbType.AnsiString, partShop.Address)
            DbCommandWrapper.AddInParameter("@Phone", DbType.AnsiString, partShop.Phone)
            DbCommandWrapper.AddInParameter("@Fax", DbType.AnsiString, partShop.Fax)
            DbCommandWrapper.AddInParameter("@Email", DbType.AnsiString, partShop.Email)
            DbCommandWrapper.AddInParameter("@Status", DbType.Byte, partShop.Status)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, partShop.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, partShop.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@CityID", DbType.Int32, Me.GetRefObject(partShop.City))
            DbCommandWrapper.AddInParameter("@CityPartID", DbType.Int32, Me.GetRefObject(partShop.CityPart))
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, Me.GetRefObject(partShop.Dealer))

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

            Dim partShop As PartShop = CType(obj, PartShop)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, partShop.ID)
            DbCommandWrapper.AddInParameter("@PartShopCode", DbType.AnsiString, partShop.PartShopCode)
            DbCommandWrapper.AddInParameter("@Name", DbType.AnsiString, partShop.Name)
            DbCommandWrapper.AddInParameter("@Address", DbType.AnsiString, partShop.Address)
            DbCommandWrapper.AddInParameter("@Phone", DbType.AnsiString, partShop.Phone)
            DbCommandWrapper.AddInParameter("@Fax", DbType.AnsiString, partShop.Fax)
            DbCommandWrapper.AddInParameter("@Email", DbType.AnsiString, partShop.Email)
            DbCommandWrapper.AddInParameter("@Status", DbType.Byte, partShop.Status)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, partShop.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, partShop.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@CityID", DbType.Int32, Me.GetRefObject(partShop.City))
            DbCommandWrapper.AddInParameter("@CityPartID", DbType.Int32, Me.GetRefObject(partShop.CityPart))
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, Me.GetRefObject(partShop.Dealer))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As PartShop

            Dim partShop As PartShop = New PartShop

            partShop.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("PartShopCode")) Then partShop.PartShopCode = dr("PartShopCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("OldPartShopCode")) Then partShop.OldPartShopCode = dr("OldPartShopCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Name")) Then partShop.Name = dr("Name").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Address")) Then partShop.Address = dr("Address").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Phone")) Then partShop.Phone = dr("Phone").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Fax")) Then partShop.Fax = dr("Fax").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Email")) Then partShop.Email = dr("Email").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then partShop.Status = CType(dr("Status"), Byte)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then partShop.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then partShop.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then partShop.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then partShop.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then partShop.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("CityPartID")) Then
                partShop.CityPart = New CityPart(CType(dr("CityPartID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("CityID")) Then
                partShop.City = New City(CType(dr("CityID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then
                partShop.Dealer = New Dealer(CType(dr("DealerID"), Short))
            End If

            Return partShop

        End Function

        Private Sub SetTableName()

            If Not (GetType(PartShop) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(PartShop), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(PartShop).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

