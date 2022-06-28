#Region "Summary"
'// ===========================================================================
'// AUTHOR        : Noviar Sbastian
'// PURPOSE       : V_LogisticPriceAndNationality Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2005 
'// ---------------------
'// $History      : $
'// Generated on 9/27/2005 - 5:17:47 PM
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

    Public Class V_LogisticPriceAndNationalityMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertV_LogisticPriceAndNationality"
        Private m_UpdateStatement As String = "up_UpdateV_LogisticPriceAndNationality"
        Private m_RetrieveStatement As String = "up_RetrieveV_LogisticPriceAndNationality"
        Private m_RetrieveListStatement As String = "up_RetrieveV_LogisticPriceAndNationalityList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteV_LogisticPriceAndNationality"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim V_LogisticPriceAndNationality As V_LogisticPriceAndNationality = Nothing
            While dr.Read

                V_LogisticPriceAndNationality = Me.CreateObject(dr)

            End While

            Return V_LogisticPriceAndNationality

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim V_LogisticPriceAndNationalityList As ArrayList = New ArrayList

            While dr.Read
                Dim V_LogisticPriceAndNationality As V_LogisticPriceAndNationality = Me.CreateObject(dr)
                V_LogisticPriceAndNationalityList.Add(V_LogisticPriceAndNationality)
            End While

            Return V_LogisticPriceAndNationalityList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim V_LogisticPriceAndNationality As V_LogisticPriceAndNationality = CType(obj, V_LogisticPriceAndNationality)
            DBCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, V_LogisticPriceAndNationality.ID)
            Return DBCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim V_LogisticPriceAndNationality As V_LogisticPriceAndNationality = CType(obj, V_LogisticPriceAndNationality)
            DBCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DBCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@RegionCode", DbType.AnsiString, V_LogisticPriceAndNationality.RegionCode)
            DbCommandWrapper.AddInParameter("@RegionDescription", DbType.AnsiString, V_LogisticPriceAndNationality.RegionDescription)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, V_LogisticPriceAndNationality.RowStatus)
            DBCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, V_LogisticPriceAndNationality.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            Return DBCommandWrapper

        End Function

        Protected Overrides Function GetNewID(ByVal dbCommandWrapper As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper) As Integer

            Return CType(dbCommandWrapper.GetParameterValue("@ID"), Integer)

        End Function

        Protected Overrides Function GetPagingRetrieveCommand() As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            DBCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_PagingQuery)
            DBCommandWrapper.AddInParameter("@Table", DbType.String, m_TableName)
            DBCommandWrapper.AddInParameter("@PK", DbType.String, "ID")

            Return DBCommandWrapper

        End Function

        Protected Overrides Function GetRetrieveCommand() As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            DBCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DinamicQuery)
            DBCommandWrapper.AddInParameter("@sqlQuery", DbType.String, "SELECT " + m_TableName + ".* FROM " + m_TableName)

            Return DBCommandWrapper

        End Function

        Protected Overrides Function GetRetrieveListParameter() As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            DBCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_RetrieveListStatement)
            Return DBCommandWrapper

        End Function

        Protected Overrides Function GetRetrieveParameter(ByVal id As Integer) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            DBCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_RetrieveStatement)
            DBCommandWrapper.AddInParameter("@ID", DbType.Int32, id)

            Return DBCommandWrapper

        End Function

        Protected Overrides Function GetUpdateParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim V_LogisticPriceAndNationality As V_LogisticPriceAndNationality = CType(obj, V_LogisticPriceAndNationality)

            DBCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, V_LogisticPriceAndNationality.ID)
            DbCommandWrapper.AddInParameter("@RegionCode", DbType.AnsiString, V_LogisticPriceAndNationality.RegionCode)
            DbCommandWrapper.AddInParameter("@RegionDescription", DbType.AnsiString, V_LogisticPriceAndNationality.RegionDescription)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, V_LogisticPriceAndNationality.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, V_LogisticPriceAndNationality.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DBCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)



            Return DBCommandWrapper

        End Function

#End Region

#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As V_LogisticPriceAndNationality

            Dim V_LogisticPriceAndNationality As V_LogisticPriceAndNationality = New V_LogisticPriceAndNationality

            V_LogisticPriceAndNationality.ID = CType(dr("ID"), Integer)

            If Not dr.IsDBNull(dr.GetOrdinal("RegionCode")) Then V_LogisticPriceAndNationality.RegionCode = dr("RegionCode").ToString

            If Not dr.IsDBNull(dr.GetOrdinal("RegionDescription")) Then V_LogisticPriceAndNationality.RegionDescription = dr("RegionDescription").ToString

            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then V_LogisticPriceAndNationality.RowStatus = CType(dr("RowStatus"), Short)

            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then V_LogisticPriceAndNationality.CreatedBy = dr("CreatedBy").ToString

            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then V_LogisticPriceAndNationality.CreatedTime = CType(dr("CreatedTime"), DateTime)

            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then V_LogisticPriceAndNationality.LastUpdateBy = dr("LastUpdateBy").ToString

            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then V_LogisticPriceAndNationality.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)


            Return V_LogisticPriceAndNationality

        End Function

        Private Sub SetTableName()

            If Not (GetType(V_LogisticPriceAndNationality) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(V_LogisticPriceAndNationality), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(V_LogisticPriceAndNationality).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace