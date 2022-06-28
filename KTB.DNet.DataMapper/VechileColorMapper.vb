#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : VechileColor Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2005 
'// ---------------------
'// $History      : $
'// Generated on 10/3/2005 - 2:10:46 PM
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

    Public Class VechileColorMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertVechileColor"
        Private m_UpdateStatement As String = "up_UpdateVechileColor"
        Private m_RetrieveStatement As String = "up_RetrieveVechileColor"
        Private m_RetrieveListStatement As String = "up_RetrieveVechileColorList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteVechileColor"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim vechileColor As VechileColor = Nothing
            While dr.Read

                vechileColor = Me.CreateObject(dr)

            End While

            Return vechileColor

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim vechileColorList As ArrayList = New ArrayList

            While dr.Read
                Dim vechileColor As VechileColor = Me.CreateObject(dr)
                vechileColorList.Add(vechileColor)
            End While

            Return vechileColorList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim vechileColor As VechileColor = CType(obj, VechileColor)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, vechileColor.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim vechileColor As VechileColor = CType(obj, VechileColor)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@ColorCode", DbType.AnsiString, vechileColor.ColorCode)
            DbCommandWrapper.AddInParameter("@ColorIndName", DbType.AnsiString, vechileColor.ColorIndName)
            DbCommandWrapper.AddInParameter("@ColorEngName", DbType.AnsiString, vechileColor.ColorEngName)
            DbCommandWrapper.AddInParameter("@MaterialNumber", DbType.AnsiString, vechileColor.MaterialNumber)
            DbCommandWrapper.AddInParameter("@MaterialDescription", DbType.AnsiString, vechileColor.MaterialDescription)
            DbCommandWrapper.AddInParameter("@HeaderBOM", DbType.AnsiString, vechileColor.HeaderBOM)
            DbCommandWrapper.AddInParameter("@MarketCode", DbType.AnsiString, vechileColor.MarketCode)
            DbCommandWrapper.AddInParameter("@SpecialFlag", DbType.AnsiString, vechileColor.SpecialFlag)
            DbCommandWrapper.AddInParameter("@Status", DbType.AnsiString, vechileColor.Status)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, vechileColor.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, vechileColor.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@VechileTypeID", DbType.Int32, Me.GetRefObject(vechileColor.VechileType))

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

            Dim vechileColor As VechileColor = CType(obj, VechileColor)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, vechileColor.ID)
            DbCommandWrapper.AddInParameter("@ColorCode", DbType.AnsiString, vechileColor.ColorCode)
            DbCommandWrapper.AddInParameter("@ColorIndName", DbType.AnsiString, vechileColor.ColorIndName)
            DbCommandWrapper.AddInParameter("@ColorEngName", DbType.AnsiString, vechileColor.ColorEngName)
            DbCommandWrapper.AddInParameter("@MaterialNumber", DbType.AnsiString, vechileColor.MaterialNumber)
            DbCommandWrapper.AddInParameter("@MaterialDescription", DbType.AnsiString, vechileColor.MaterialDescription)
            DbCommandWrapper.AddInParameter("@HeaderBOM", DbType.AnsiString, vechileColor.HeaderBOM)
            DbCommandWrapper.AddInParameter("@MarketCode", DbType.AnsiString, vechileColor.MarketCode)
            DbCommandWrapper.AddInParameter("@SpecialFlag", DbType.AnsiString, vechileColor.SpecialFlag)
            DbCommandWrapper.AddInParameter("@Status", DbType.AnsiString, vechileColor.Status)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, vechileColor.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, vechileColor.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@VechileTypeID", DbType.Int32, Me.GetRefObject(vechileColor.VechileType))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As VechileColor

            Dim vechileColor As VechileColor = New VechileColor

            vechileColor.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("ColorCode")) Then vechileColor.ColorCode = dr("ColorCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ColorIndName")) Then vechileColor.ColorIndName = dr("ColorIndName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ColorEngName")) Then vechileColor.ColorEngName = dr("ColorEngName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("MaterialNumber")) Then vechileColor.MaterialNumber = dr("MaterialNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("MaterialDescription")) Then vechileColor.MaterialDescription = dr("MaterialDescription").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("HeaderBOM")) Then vechileColor.HeaderBOM = dr("HeaderBOM").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("MarketCode")) Then vechileColor.MarketCode = dr("MarketCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SpecialFlag")) Then vechileColor.SpecialFlag = dr("SpecialFlag").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then vechileColor.Status = dr("Status").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then vechileColor.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then vechileColor.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then vechileColor.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then vechileColor.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then vechileColor.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("VechileTypeID")) Then
                vechileColor.VechileType = New VechileType(CType(dr("VechileTypeID"), Integer))
            End If

            Return vechileColor

        End Function

        Private Sub SetTableName()

            If Not (GetType(VechileColor) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(VechileColor), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(VechileColor).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

