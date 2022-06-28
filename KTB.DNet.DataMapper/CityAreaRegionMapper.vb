#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : CityAreaRegion Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2021 
'// ---------------------
'// $History      : $
'// Generated on 4/23/2021 - 7:49:11 AM
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

    Public Class CityAreaRegionMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertCityAreaRegion"
        Private m_UpdateStatement As String = "up_UpdateCityAreaRegion"
        Private m_RetrieveStatement As String = "up_RetrieveCityAreaRegion"
        Private m_RetrieveListStatement As String = "up_RetrieveCityAreaRegionList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteCityAreaRegion"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim cityAreaRegion As CityAreaRegion = Nothing
            While dr.Read

                cityAreaRegion = Me.CreateObject(dr)

            End While

            Return cityAreaRegion

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim cityAreaRegionList As ArrayList = New ArrayList

            While dr.Read
                Dim cityAreaRegion As CityAreaRegion = Me.CreateObject(dr)
                cityAreaRegionList.Add(cityAreaRegion)
            End While

            Return cityAreaRegionList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim cityAreaRegion As CityAreaRegion = CType(obj, CityAreaRegion)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, cityAreaRegion.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim cityAreaRegion As CityAreaRegion = CType(obj, CityAreaRegion)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)

            DbCommandWrapper.AddInParameter("@Area1ID", DbType.Int32, Me.GetRefObject(cityAreaRegion.Area1))
            DbCommandWrapper.AddInParameter("@CityID", DbType.Int16, Me.GetRefObject(cityAreaRegion.City))
            DbCommandWrapper.AddInParameter("@MainAreaID", DbType.Int32, Me.GetRefObject(cityAreaRegion.MainArea))
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, cityAreaRegion.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, cityAreaRegion.LastUpdateBy)
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

            Dim cityAreaRegion As CityAreaRegion = CType(obj, CityAreaRegion)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, cityAreaRegion.ID)


            DbCommandWrapper.AddInParameter("@Area1ID", DbType.Int32, Me.GetRefObject(cityAreaRegion.Area1))
            DbCommandWrapper.AddInParameter("@CityID", DbType.Int16, Me.GetRefObject(cityAreaRegion.City))
            DbCommandWrapper.AddInParameter("@MainAreaID", DbType.Int32, Me.GetRefObject(cityAreaRegion.MainArea))

            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, cityAreaRegion.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, cityAreaRegion.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As CityAreaRegion

            Dim cityAreaRegion As CityAreaRegion = New CityAreaRegion

            cityAreaRegion.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Area1ID")) Then
                cityAreaRegion.Area1 = New Area1(CType(dr("Area1ID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("CityID")) Then
                cityAreaRegion.City = New City(CType(dr("CityID"), Short))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("MainAreaID")) Then
                cityAreaRegion.MainArea = New MainArea(CType(dr("MainAreaID"), Integer))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then cityAreaRegion.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then cityAreaRegion.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then cityAreaRegion.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then cityAreaRegion.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then cityAreaRegion.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            Return cityAreaRegion

        End Function

        Private Sub SetTableName()

            If Not (GetType(CityAreaRegion) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(CityAreaRegion), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(CityAreaRegion).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace
