#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : FleetGradeRetention Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2009 
'// ---------------------
'// $History      : $
'// Generated on 8/26/2009 - 10:42:15 AM
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

    Public Class FleetGradeRetentionMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertFleetGradeRetention"
        Private m_UpdateStatement As String = "up_UpdateFleetGradeRetention"
        Private m_RetrieveStatement As String = "up_RetrieveFleetGradeRetention"
        Private m_RetrieveListStatement As String = "up_RetrieveFleetGradeRetentionList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteFleetGradeRetention"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim FleetGradeRetention As FleetGradeRetention = Nothing
            While dr.Read

                FleetGradeRetention = Me.CreateObject(dr)

            End While

            Return FleetGradeRetention

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim FleetGradeRetentionList As ArrayList = New ArrayList

            While dr.Read
                Dim FleetGradeRetention As FleetGradeRetention = Me.CreateObject(dr)
                FleetGradeRetentionList.Add(FleetGradeRetention)
            End While

            Return FleetGradeRetentionList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim FleetGradeRetention As FleetGradeRetention = CType(obj, FleetGradeRetention)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, FleetGradeRetention.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim FleetGradeRetention As FleetGradeRetention = CType(obj, FleetGradeRetention)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)

            DbCommandWrapper.AddInParameter("@Category", DbType.Int16, FleetGradeRetention.Category)
            DbCommandWrapper.AddInParameter("@Grade", DbType.AnsiString, FleetGradeRetention.Grade)
            DbCommandWrapper.AddInParameter("@VehicleType", DbType.AnsiString, FleetGradeRetention.VehicleType)
            DbCommandWrapper.AddInParameter("@Operators", DbType.AnsiString, FleetGradeRetention.Operators)
            DbCommandWrapper.AddInParameter("@UnitFrom", DbType.Int32, FleetGradeRetention.UnitFrom)
            DbCommandWrapper.AddInParameter("@UnitTo", DbType.Int32, FleetGradeRetention.UnitTo)

            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, FleetGradeRetention.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, FleetGradeRetention.LastUpdateBy)
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

            Dim FleetGradeRetention As FleetGradeRetention = CType(obj, FleetGradeRetention)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, FleetGradeRetention.ID)
            DbCommandWrapper.AddInParameter("@Category", DbType.Int16, FleetGradeRetention.Category)
            DbCommandWrapper.AddInParameter("@Grade", DbType.AnsiString, FleetGradeRetention.Grade)
            DbCommandWrapper.AddInParameter("@VehicleType", DbType.AnsiString, FleetGradeRetention.VehicleType)
            DbCommandWrapper.AddInParameter("@Operators", DbType.AnsiString, FleetGradeRetention.Operators)
            DbCommandWrapper.AddInParameter("@UnitFrom", DbType.Int32, FleetGradeRetention.UnitFrom)
            DbCommandWrapper.AddInParameter("@UnitTo", DbType.Int32, FleetGradeRetention.UnitTo)

            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, FleetGradeRetention.RowStatus)
            'DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, FleetGradeRetention.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As FleetGradeRetention

            Dim FleetGradeRetention As FleetGradeRetention = New FleetGradeRetention

            FleetGradeRetention.ID = CType(dr("ID"), Integer)

            If Not dr.IsDBNull(dr.GetOrdinal("Category")) Then FleetGradeRetention.Category = CType(dr("Category"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("Grade")) Then FleetGradeRetention.Grade = dr("Grade").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("VehicleType")) Then FleetGradeRetention.VehicleType = dr("VehicleType").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Operators")) Then FleetGradeRetention.Operators = dr("Operators").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("UnitFrom")) Then FleetGradeRetention.UnitFrom = CType(dr("UnitFrom"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("UnitTo")) Then FleetGradeRetention.UnitTo = CType(dr("UnitTo"), Short)

            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then FleetGradeRetention.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then FleetGradeRetention.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then FleetGradeRetention.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then FleetGradeRetention.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then FleetGradeRetention.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            Return FleetGradeRetention

        End Function

        Private Sub SetTableName()

            If Not (GetType(FleetGradeRetention) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(FleetGradeRetention), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(FleetGradeRetention).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

