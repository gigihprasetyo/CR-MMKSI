
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : VechileModel Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2005 
'// ---------------------
'// $History      : $
'// Generated on 17/11/2005 - 9:11:12
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

    Public Class VechileModelMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertVechileModel"
        Private m_UpdateStatement As String = "up_UpdateVechileModel"
        Private m_RetrieveStatement As String = "up_RetrieveVechileModel"
        Private m_RetrieveListStatement As String = "up_RetrieveVechileModelList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteVechileModel"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim vechileModel As VechileModel = Nothing
            While dr.Read

                vechileModel = Me.CreateObject(dr)

            End While

            Return vechileModel

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim vechileModelList As ArrayList = New ArrayList

            While dr.Read
                Dim vechileModel As VechileModel = Me.CreateObject(dr)
                vechileModelList.Add(vechileModel)
            End While

            Return vechileModelList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim vechileModel As VechileModel = CType(obj, VechileModel)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int16, vechileModel.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim vechileModel As VechileModel = CType(obj, VechileModel)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int16, 2)
            'DbCommandWrapper.AddInParameter("@SAPCode", DbType.AnsiString, vechileModel.SAPCode)
            DbCommandWrapper.AddInParameter("@VechileModelCode", DbType.AnsiString, vechileModel.VechileModelCode)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, vechileModel.Description)
            DbCommandWrapper.AddInParameter("@VechileModelIndCode", DbType.AnsiString, vechileModel.VechileModelIndCode)
            DbCommandWrapper.AddInParameter("@IndDescription", DbType.AnsiString, vechileModel.IndDescription)
            DbCommandWrapper.AddInParameter("@SalesFlag", DbType.Int16, vechileModel.SalesFlag)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, vechileModel.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, vechileModel.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@CategoryID", DbType.Byte, Me.GetRefObject(vechileModel.Category))

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

            Dim vechileModel As VechileModel = CType(obj, VechileModel)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int16, vechileModel.ID)
            'DbCommandWrapper.AddInParameter("@SAPCode", DbType.AnsiString, vechileModel.SAPCode)
            DbCommandWrapper.AddInParameter("@VechileModelCode", DbType.AnsiString, vechileModel.VechileModelCode)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, vechileModel.Description)
            DbCommandWrapper.AddInParameter("@VechileModelIndCode", DbType.AnsiString, vechileModel.VechileModelIndCode)
            DbCommandWrapper.AddInParameter("@IndDescription", DbType.AnsiString, vechileModel.IndDescription)
            DbCommandWrapper.AddInParameter("@SalesFlag", DbType.Int16, vechileModel.SalesFlag)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, vechileModel.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, vechileModel.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@CategoryID", DbType.Byte, Me.GetRefObject(vechileModel.Category))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As VechileModel

            Dim vechileModel As VechileModel = New VechileModel

            vechileModel.ID = CType(dr("ID"), Short)
            'If Not dr.IsDBNull(dr.GetOrdinal("SAPCode")) Then vechileModel.SAPCode = dr("SAPCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("VechileModelCode")) Then vechileModel.VechileModelCode = dr("VechileModelCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Description")) Then vechileModel.Description = dr("Description").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("VechileModelIndCode")) Then vechileModel.VechileModelIndCode = dr("VechileModelIndCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("IndDescription")) Then vechileModel.IndDescription = dr("IndDescription").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SalesFlag")) Then vechileModel.SalesFlag = CType(dr("SalesFlag"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then vechileModel.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then vechileModel.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then vechileModel.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then vechileModel.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then vechileModel.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("CategoryID")) Then
                vechileModel.Category = New Category(CType(dr("CategoryID"), Byte))
            End If

            Return vechileModel

        End Function

        Private Sub SetTableName()

            If Not (GetType(VechileModel) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(VechileModel), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(VechileModel).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

