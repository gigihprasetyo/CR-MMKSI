
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : sp_MaxTOPDay Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2012 
'// ---------------------
'// $History      : $
'// Generated on 10/12/2012 - 11:11:11 AM
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

    Public Class sp_MaxTOPDayMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_Insertsp_MaxTOPDay"
        Private m_UpdateStatement As String = "up_Updatesp_MaxTOPDay"
        Private m_RetrieveStatement As String = "up_Retrievesp_MaxTOPDay"
        Private m_RetrieveListStatement As String = "up_Retrievesp_MaxTOPDayList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_Deletesp_MaxTOPDay"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim sp_MaxTOPDay As sp_MaxTOPDay = Nothing
            While dr.Read

                sp_MaxTOPDay = Me.CreateObject(dr)

            End While

            Return sp_MaxTOPDay

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim sp_MaxTOPDayList As ArrayList = New ArrayList

            While dr.Read
                Dim sp_MaxTOPDay As sp_MaxTOPDay = Me.CreateObject(dr)
                sp_MaxTOPDayList.Add(sp_MaxTOPDay)
            End While

            Return sp_MaxTOPDayList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim sp_MaxTOPDay As sp_MaxTOPDay = CType(obj, sp_MaxTOPDay)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, sp_MaxTOPDay.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim sp_MaxTOPDay As sp_MaxTOPDay = CType(obj, sp_MaxTOPDay)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, sp_MaxTOPDay.DealerID)
            DbCommandWrapper.AddInParameter("@VechileTypeID", DbType.Int32, sp_MaxTOPDay.VechileTypeID)
            DbCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, sp_MaxTOPDay.DealerCode)
            DbCommandWrapper.AddInParameter("@ProvinceName", DbType.AnsiString, sp_MaxTOPDay.ProvinceName)
            DbCommandWrapper.AddInParameter("@CategoryCode", DbType.AnsiString, sp_MaxTOPDay.CategoryCode)
            DbCommandWrapper.AddInParameter("@VechileTypeCode", DbType.AnsiString, sp_MaxTOPDay.VechileTypeCode)
            DbCommandWrapper.AddInParameter("@Normal", DbType.Int32, sp_MaxTOPDay.Normal)
            DbCommandWrapper.AddInParameter("@Factoring", DbType.Int32, sp_MaxTOPDay.Factoring)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int32, sp_MaxTOPDay.RowStatus)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, sp_MaxTOPDay.LastUpdateBy)


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

            Dim sp_MaxTOPDay As sp_MaxTOPDay = CType(obj, sp_MaxTOPDay)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, sp_MaxTOPDay.ID)
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int32, sp_MaxTOPDay.DealerID)
            DbCommandWrapper.AddInParameter("@VechileTypeID", DbType.Int32, sp_MaxTOPDay.VechileTypeID)
            DbCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, sp_MaxTOPDay.DealerCode)
            DbCommandWrapper.AddInParameter("@ProvinceName", DbType.AnsiString, sp_MaxTOPDay.ProvinceName)
            DbCommandWrapper.AddInParameter("@CategoryCode", DbType.AnsiString, sp_MaxTOPDay.CategoryCode)
            DbCommandWrapper.AddInParameter("@VechileTypeCode", DbType.AnsiString, sp_MaxTOPDay.VechileTypeCode)
            DbCommandWrapper.AddInParameter("@Normal", DbType.Int32, sp_MaxTOPDay.Normal)
            DbCommandWrapper.AddInParameter("@Factoring", DbType.Int32, sp_MaxTOPDay.Factoring)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int32, sp_MaxTOPDay.RowStatus)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, sp_MaxTOPDay.CreatedBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As sp_MaxTOPDay

            Dim sp_MaxTOPDay As sp_MaxTOPDay = New sp_MaxTOPDay

            sp_MaxTOPDay.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then sp_MaxTOPDay.DealerID = CType(dr("DealerID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("VechileTypeID")) Then sp_MaxTOPDay.VechileTypeID = CType(dr("VechileTypeID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerCode")) Then sp_MaxTOPDay.DealerCode = dr("DealerCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ProvinceName")) Then sp_MaxTOPDay.ProvinceName = dr("ProvinceName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CategoryCode")) Then sp_MaxTOPDay.CategoryCode = dr("CategoryCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("VechileTypeCode")) Then sp_MaxTOPDay.VechileTypeCode = dr("VechileTypeCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Normal")) Then sp_MaxTOPDay.Normal = CType(dr("Normal"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Factoring")) Then sp_MaxTOPDay.Factoring = CType(dr("Factoring"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("IsCOD")) Then sp_MaxTOPDay.IsCOD = CType(dr("IsCOD"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then sp_MaxTOPDay.RowStatus = CType(dr("RowStatus"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then sp_MaxTOPDay.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then sp_MaxTOPDay.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then sp_MaxTOPDay.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then sp_MaxTOPDay.LastUpdateBy = dr("LastUpdateBy").ToString

            Return sp_MaxTOPDay

        End Function

        Private Sub SetTableName()

            If Not (GetType(sp_MaxTOPDay) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(sp_MaxTOPDay), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(sp_MaxTOPDay).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

