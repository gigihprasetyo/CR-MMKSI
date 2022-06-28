
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : MaxTOPDay Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2012 
'// ---------------------
'// $History      : $
'// Generated on 10/12/2012 - 10:58:14 AM
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

    Public Class MaxTOPDayMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertMaxTOPDay"
        Private m_UpdateStatement As String = "up_UpdateMaxTOPDay"
        Private m_RetrieveStatement As String = "up_RetrieveMaxTOPDay"
        Private m_RetrieveListStatement As String = "up_RetrieveMaxTOPDayList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteMaxTOPDay"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim maxTOPDay As MaxTOPDay = Nothing
            While dr.Read

                maxTOPDay = Me.CreateObject(dr)

            End While

            Return maxTOPDay

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim maxTOPDayList As ArrayList = New ArrayList

            While dr.Read
                Dim maxTOPDay As MaxTOPDay = Me.CreateObject(dr)
                maxTOPDayList.Add(maxTOPDay)
            End While

            Return maxTOPDayList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim maxTOPDay As MaxTOPDay = CType(obj, MaxTOPDay)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, maxTOPDay.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim maxTOPDay As MaxTOPDay = CType(obj, MaxTOPDay)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, maxTOPDay.DealerID)
            DbCommandWrapper.AddInParameter("@VechileTypeID", DbType.Int32, maxTOPDay.VechileTypeID)
            DbCommandWrapper.AddInParameter("@Normal", DbType.Int32, maxTOPDay.Normal)
            DbCommandWrapper.AddInParameter("@Factoring", DbType.Int32, maxTOPDay.Factoring)
            DbCommandWrapper.AddInParameter("@IsCOD", DbType.Int32, maxTOPDay.IsCOD)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, maxTOPDay.RowStatus)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, maxTOPDay.LastUpdateBy)


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

            Dim maxTOPDay As MaxTOPDay = CType(obj, MaxTOPDay)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, maxTOPDay.ID)
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, maxTOPDay.DealerID)
            DbCommandWrapper.AddInParameter("@VechileTypeID", DbType.Int32, maxTOPDay.VechileTypeID)
            DbCommandWrapper.AddInParameter("@Normal", DbType.Int32, maxTOPDay.Normal)
            DbCommandWrapper.AddInParameter("@Factoring", DbType.Int32, maxTOPDay.Factoring)
            DbCommandWrapper.AddInParameter("@IsCOD", DbType.Int32, maxTOPDay.IsCOD)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, maxTOPDay.RowStatus)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, maxTOPDay.CreatedBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As MaxTOPDay

            Dim maxTOPDay As MaxTOPDay = New MaxTOPDay

            maxTOPDay.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then maxTOPDay.DealerID = CType(dr("DealerID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("VechileTypeID")) Then maxTOPDay.VechileTypeID = CType(dr("VechileTypeID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Normal")) Then maxTOPDay.Normal = CType(dr("Normal"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Factoring")) Then maxTOPDay.Factoring = CType(dr("Factoring"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("IsCOD")) Then maxTOPDay.IsCOD = CType(dr("IsCOD"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then maxTOPDay.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then maxTOPDay.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then maxTOPDay.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then maxTOPDay.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then maxTOPDay.LastUpdateBy = dr("LastUpdateBy").ToString

            Return maxTOPDay

        End Function

        Private Sub SetTableName()

            If Not (GetType(MaxTOPDay) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(MaxTOPDay), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(MaxTOPDay).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

