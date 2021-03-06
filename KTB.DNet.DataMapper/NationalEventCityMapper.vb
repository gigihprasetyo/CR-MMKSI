#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : NationalEventCity Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2021 
'// ---------------------
'// $History      : $
'// Generated on 4/22/2021 - 8:40:00 AM
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

    Public Class NationalEventCityMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertNationalEventCity"
        Private m_UpdateStatement As String = "up_UpdateNationalEventCity"
        Private m_RetrieveStatement As String = "up_RetrieveNationalEventCity"
        Private m_RetrieveListStatement As String = "up_RetrieveNationalEventCityList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteNationalEventCity"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim nationalEventCity As NationalEventCity = Nothing
            While dr.Read

                nationalEventCity = Me.CreateObject(dr)

            End While

            Return nationalEventCity

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim nationalEventCityList As ArrayList = New ArrayList

            While dr.Read
                Dim nationalEventCity As NationalEventCity = Me.CreateObject(dr)
                nationalEventCityList.Add(nationalEventCity)
            End While

            Return nationalEventCityList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim nationalEventCity As NationalEventCity = CType(obj, NationalEventCity)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, nationalEventCity.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim nationalEventCity As NationalEventCity = CType(obj, NationalEventCity)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@Status", DbType.AnsiString, nationalEventCity.Status)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, nationalEventCity.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, nationalEventCity.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@CityID", DbType.Int16, Me.GetRefObject(nationalEventCity.City))

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

            Dim nationalEventCity As NationalEventCity = CType(obj, NationalEventCity)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, nationalEventCity.ID)
            DbCommandWrapper.AddInParameter("@Status", DbType.AnsiString, nationalEventCity.Status)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, nationalEventCity.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, nationalEventCity.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@CityID", DbType.Int16, Me.GetRefObject(nationalEventCity.City))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As NationalEventCity

            Dim nationalEventCity As NationalEventCity = New NationalEventCity

            nationalEventCity.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then nationalEventCity.Status = dr("Status").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then nationalEventCity.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then nationalEventCity.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then nationalEventCity.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then nationalEventCity.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then nationalEventCity.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("CityID")) Then
                nationalEventCity.City = New City(CType(dr("CityID"), Integer))
            End If

            Return nationalEventCity

        End Function

        Private Sub SetTableName()

            If Not (GetType(NationalEventCity) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(NationalEventCity), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(NationalEventCity).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace
