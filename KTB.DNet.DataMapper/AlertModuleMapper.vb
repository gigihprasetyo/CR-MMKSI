#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : AlertModul Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2007 
'// ---------------------
'// $History      : $
'// Generated on 21/09/2007 - 8:47:20
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

    Public Class AlertModulMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertAlertModul"
        Private m_UpdateStatement As String = "up_UpdateAlertModul"
        Private m_RetrieveStatement As String = "up_RetrieveAlertModul"
        Private m_RetrieveListStatement As String = "up_RetrieveAlertModulList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteAlertModul"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim alertModul As AlertModul = Nothing
            While dr.Read

                alertModul = Me.CreateObject(dr)

            End While

            Return alertModul

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim alertModulList As ArrayList = New ArrayList

            While dr.Read
                Dim alertModul As AlertModul = Me.CreateObject(dr)
                alertModulList.Add(alertModul)
            End While

            Return alertModulList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim alertModul As AlertModul = CType(obj, AlertModul)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, alertModul.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim alertModul As AlertModul = CType(obj, AlertModul)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@Code", DbType.AnsiStringFixedLength, alertModul.Code)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, alertModul.Description)
            DbCommandWrapper.AddInParameter("@EnumClassName", DbType.AnsiString, alertModul.EnumClassName)
            DbCommandWrapper.AddInParameter("@EnumAssemblyName", DbType.AnsiString, alertModul.EnumAssemblyName)
            DbCommandWrapper.AddInParameter("@EnumMethodToCall", DbType.AnsiString, alertModul.EnumMethodToCall)
            DbCommandWrapper.AddInParameter("@EnumStatusIDPropertName", DbType.AnsiString, alertModul.EnumStatusIDPropertName)
            DbCommandWrapper.AddInParameter("@EnumStatusNamePropertyName", DbType.AnsiString, alertModul.EnumStatusNamePropertyName)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, alertModul.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, alertModul.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@AlertCategoryID", DbType.Int32, Me.GetRefObject(alertModul.AlertCategory))

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

            Dim alertModul As AlertModul = CType(obj, AlertModul)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, alertModul.ID)
            DbCommandWrapper.AddInParameter("@Code", DbType.AnsiStringFixedLength, alertModul.Code)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, alertModul.Description)
            DbCommandWrapper.AddInParameter("@EnumClassName", DbType.AnsiString, alertModul.EnumClassName)
            DbCommandWrapper.AddInParameter("@EnumAssemblyName", DbType.AnsiString, alertModul.EnumAssemblyName)
            DbCommandWrapper.AddInParameter("@EnumMethodToCall", DbType.AnsiString, alertModul.EnumMethodToCall)
            DbCommandWrapper.AddInParameter("@EnumStatusIDPropertName", DbType.AnsiString, alertModul.EnumStatusIDPropertName)
            DbCommandWrapper.AddInParameter("@EnumStatusNamePropertyName", DbType.AnsiString, alertModul.EnumStatusNamePropertyName)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, alertModul.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, alertModul.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@AlertCategoryID", DbType.Int32, Me.GetRefObject(alertModul.AlertCategory))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As AlertModul

            Dim alertModul As AlertModul = New AlertModul

            alertModul.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Code")) Then alertModul.Code = dr("Code").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Description")) Then alertModul.Description = dr("Description").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("EnumClassName")) Then alertModul.EnumClassName = dr("EnumClassName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("EnumAssemblyName")) Then alertModul.EnumAssemblyName = dr("EnumAssemblyName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("EnumMethodToCall")) Then alertModul.EnumMethodToCall = dr("EnumMethodToCall").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("EnumStatusIDPropertName")) Then alertModul.EnumStatusIDPropertName = dr("EnumStatusIDPropertName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("EnumStatusNamePropertyName")) Then alertModul.EnumStatusNamePropertyName = dr("EnumStatusNamePropertyName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then alertModul.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then alertModul.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then alertModul.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then alertModul.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then alertModul.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("AlertCategoryID")) Then
                alertModul.AlertCategory = New AlertCategory(CType(dr("AlertCategoryID"), Integer))
            End If

            Return alertModul

        End Function

        Private Sub SetTableName()

            If Not (GetType(AlertModul) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(AlertModul), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(AlertModul).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

