#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : NationalHoliday Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2005 
'// ---------------------
'// $History      : $
'// Generated on 10/31/2005 - 1:30:53 PM
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

    Public Class NationalHolidayMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertNationalHoliday"
        Private m_UpdateStatement As String = "up_UpdateNationalHoliday"
        Private m_RetrieveStatement As String = "up_RetrieveNationalHoliday"
        Private m_RetrieveListStatement As String = "up_RetrieveNationalHolidayList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteNationalHoliday"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim nationalHoliday As NationalHoliday = Nothing
            While dr.Read

                nationalHoliday = Me.CreateObject(dr)

            End While

            Return nationalHoliday

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim nationalHolidayList As ArrayList = New ArrayList

            While dr.Read
                Dim nationalHoliday As NationalHoliday = Me.CreateObject(dr)
                nationalHolidayList.Add(nationalHoliday)
            End While

            Return nationalHolidayList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim nationalHoliday As NationalHoliday = CType(obj, NationalHoliday)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, nationalHoliday.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim nationalHoliday As NationalHoliday = CType(obj, NationalHoliday)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@HolidayYear", DbType.Int16, nationalHoliday.HolidayYear)
            DbCommandWrapper.AddInParameter("@HolidayDate", DbType.Int16, nationalHoliday.HolidayDate)
            DbCommandWrapper.AddInParameter("@HolidayMonth", DbType.Int16, nationalHoliday.HolidayMonth)
            DbCommandWrapper.AddInParameter("@HolidayDateTime", DbType.DateTime, nationalHoliday.HolidayDateTime)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, nationalHoliday.Description)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, nationalHoliday.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, nationalHoliday.LastUpdateBy)
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

            Dim nationalHoliday As NationalHoliday = CType(obj, NationalHoliday)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, nationalHoliday.ID)
            DbCommandWrapper.AddInParameter("@HolidayYear", DbType.Int16, nationalHoliday.HolidayYear)
            DbCommandWrapper.AddInParameter("@HolidayDate", DbType.Int16, nationalHoliday.HolidayDate)
            DbCommandWrapper.AddInParameter("@HolidayMonth", DbType.Int16, nationalHoliday.HolidayMonth)
            DbCommandWrapper.AddInParameter("@HolidayDateTime", DbType.DateTime, nationalHoliday.HolidayDateTime)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, nationalHoliday.Description)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, nationalHoliday.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, nationalHoliday.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As NationalHoliday

            Dim nationalHoliday As NationalHoliday = New NationalHoliday

            nationalHoliday.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("HolidayYear")) Then nationalHoliday.HolidayYear = CType(dr("HolidayYear"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("HolidayDate")) Then nationalHoliday.HolidayDate = CType(dr("HolidayDate"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("HolidayMonth")) Then nationalHoliday.HolidayMonth = CType(dr("HolidayMonth"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("HolidayDateTime")) Then nationalHoliday.HolidayDateTime = CType(dr("HolidayDateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("Description")) Then nationalHoliday.Description = dr("Description").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then nationalHoliday.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then nationalHoliday.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then nationalHoliday.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then nationalHoliday.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then nationalHoliday.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            Return nationalHoliday

        End Function

        Private Sub SetTableName()

            If Not (GetType(NationalHoliday) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(NationalHoliday), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(NationalHoliday).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

