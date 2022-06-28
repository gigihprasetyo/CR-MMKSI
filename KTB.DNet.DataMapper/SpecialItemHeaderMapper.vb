
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : SpecialItemHeader Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2005 
'// ---------------------
'// $History      : $
'// Generated on 28/11/2005 - 10:35:45
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

    Public Class SpecialItemHeaderMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertSpecialItemHeader"
        Private m_UpdateStatement As String = "up_UpdateSpecialItemHeader"
        Private m_RetrieveStatement As String = "up_RetrieveSpecialItemHeader"
        Private m_RetrieveListStatement As String = "up_RetrieveSpecialItemHeaderList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteSpecialItemHeader"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim specialItemHeader As SpecialItemHeader = Nothing
            While dr.Read

                specialItemHeader = Me.CreateObject(dr)

            End While

            Return specialItemHeader

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim specialItemHeaderList As ArrayList = New ArrayList

            While dr.Read
                Dim specialItemHeader As SpecialItemHeader = Me.CreateObject(dr)
                specialItemHeaderList.Add(specialItemHeader)
            End While

            Return specialItemHeaderList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim specialItemHeader As SpecialItemHeader = CType(obj, SpecialItemHeader)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, specialItemHeader.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim specialItemHeader As SpecialItemHeader = CType(obj, SpecialItemHeader)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@MonthPeriode", DbType.Int16, specialItemHeader.MonthPeriode)
            DbCommandWrapper.AddInParameter("@YearPeriode", DbType.Int16, specialItemHeader.YearPeriode)
            DbCommandWrapper.AddInParameter("@Remark", DbType.AnsiString, specialItemHeader.Remark)
            DbCommandWrapper.AddInParameter("@Reference", DbType.AnsiString, specialItemHeader.Reference)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, specialItemHeader.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, specialItemHeader.LastUpdateBy)
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

            Dim specialItemHeader As SpecialItemHeader = CType(obj, SpecialItemHeader)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, specialItemHeader.ID)
            DbCommandWrapper.AddInParameter("@MonthPeriode", DbType.Int16, specialItemHeader.MonthPeriode)
            DbCommandWrapper.AddInParameter("@YearPeriode", DbType.Int16, specialItemHeader.YearPeriode)
            DbCommandWrapper.AddInParameter("@Remark", DbType.AnsiString, specialItemHeader.Remark)
            DbCommandWrapper.AddInParameter("@Reference", DbType.AnsiString, specialItemHeader.Reference)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, specialItemHeader.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, specialItemHeader.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As SpecialItemHeader

            Dim specialItemHeader As SpecialItemHeader = New SpecialItemHeader

            specialItemHeader.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("MonthPeriode")) Then specialItemHeader.MonthPeriode = CType(dr("MonthPeriode"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("YearPeriode")) Then specialItemHeader.YearPeriode = CType(dr("YearPeriode"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("Remark")) Then specialItemHeader.Remark = dr("Remark").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Reference")) Then specialItemHeader.Reference = dr("Reference").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then specialItemHeader.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then specialItemHeader.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then specialItemHeader.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then specialItemHeader.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then specialItemHeader.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            Return specialItemHeader

        End Function

        Private Sub SetTableName()

            If Not (GetType(SpecialItemHeader) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(SpecialItemHeader), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(SpecialItemHeader).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

