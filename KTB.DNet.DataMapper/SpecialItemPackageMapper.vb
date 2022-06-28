
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : SpecialItemPackage Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2005 
'// ---------------------
'// $History      : $
'// Generated on 28/11/2005 - 10:37:48
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

    Public Class SpecialItemPackageMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertSpecialItemPackage"
        Private m_UpdateStatement As String = "up_UpdateSpecialItemPackage"
        Private m_RetrieveStatement As String = "up_RetrieveSpecialItemPackage"
        Private m_RetrieveListStatement As String = "up_RetrieveSpecialItemPackageList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteSpecialItemPackage"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim specialItemPackage As SpecialItemPackage = Nothing
            While dr.Read

                specialItemPackage = Me.CreateObject(dr)

            End While

            Return specialItemPackage

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim specialItemPackageList As ArrayList = New ArrayList

            While dr.Read
                Dim specialItemPackage As SpecialItemPackage = Me.CreateObject(dr)
                specialItemPackageList.Add(specialItemPackage)
            End While

            Return specialItemPackageList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim specialItemPackage As SpecialItemPackage = CType(obj, SpecialItemPackage)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, specialItemPackage.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim specialItemPackage As SpecialItemPackage = CType(obj, SpecialItemPackage)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@PackageNo", DbType.Int16, specialItemPackage.PackageNo)
            DbCommandWrapper.AddInParameter("@PackagePrice", DbType.Currency, specialItemPackage.PackagePrice)
            DbCommandWrapper.AddInParameter("@PackageDescription", DbType.AnsiString, specialItemPackage.PackageDescription)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, specialItemPackage.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, specialItemPackage.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@SpecialItemDetailID", DbType.Int32, Me.GetRefObject(specialItemPackage.SpecialItemDetail))

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

            Dim specialItemPackage As SpecialItemPackage = CType(obj, SpecialItemPackage)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, specialItemPackage.ID)
            DbCommandWrapper.AddInParameter("@PackageNo", DbType.Int16, specialItemPackage.PackageNo)
            DbCommandWrapper.AddInParameter("@PackagePrice", DbType.Currency, specialItemPackage.PackagePrice)
            DbCommandWrapper.AddInParameter("@PackageDescription", DbType.AnsiString, specialItemPackage.PackageDescription)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, specialItemPackage.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, specialItemPackage.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@SpecialItemDetailID", DbType.Int32, Me.GetRefObject(specialItemPackage.SpecialItemDetail))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As SpecialItemPackage

            Dim specialItemPackage As SpecialItemPackage = New SpecialItemPackage

            specialItemPackage.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("PackageNo")) Then specialItemPackage.PackageNo = CType(dr("PackageNo"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("PackagePrice")) Then specialItemPackage.PackagePrice = CType(dr("PackagePrice"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("PackageDescription")) Then specialItemPackage.PackageDescription = dr("PackageDescription").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then specialItemPackage.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then specialItemPackage.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then specialItemPackage.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then specialItemPackage.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then specialItemPackage.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("SpecialItemDetailID")) Then
                specialItemPackage.SpecialItemDetail = New SpecialItemDetail(CType(dr("SpecialItemDetailID"), Integer))
            End If

            Return specialItemPackage

        End Function

        Private Sub SetTableName()

            If Not (GetType(SpecialItemPackage) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(SpecialItemPackage), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(SpecialItemPackage).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

