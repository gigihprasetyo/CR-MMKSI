#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : FleetGradeDiscount Objects Mapper.
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

    Public Class FleetGradeDiscountMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertFleetGradeDiscount"
        Private m_UpdateStatement As String = "up_UpdateFleetGradeDiscount"
        Private m_RetrieveStatement As String = "up_RetrieveFleetGradeDiscount"
        Private m_RetrieveListStatement As String = "up_RetrieveFleetGradeDiscountList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteFleetGradeDiscount"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim FleetGradeDiscount As FleetGradeDiscount = Nothing
            While dr.Read

                FleetGradeDiscount = Me.CreateObject(dr)

            End While

            Return FleetGradeDiscount

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim FleetGradeDiscountList As ArrayList = New ArrayList

            While dr.Read
                Dim FleetGradeDiscount As FleetGradeDiscount = Me.CreateObject(dr)
                FleetGradeDiscountList.Add(FleetGradeDiscount)
            End While

            Return FleetGradeDiscountList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim FleetGradeDiscount As FleetGradeDiscount = CType(obj, FleetGradeDiscount)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, FleetGradeDiscount.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim FleetGradeDiscount As FleetGradeDiscount = CType(obj, FleetGradeDiscount)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@Grade", DbType.AnsiString, FleetGradeDiscount.Grade)
            DbCommandWrapper.AddInParameter("@VehicleType", DbType.AnsiString, FleetGradeDiscount.VehicleType)
            DbCommandWrapper.AddInParameter("@Discount", DbType.Decimal, FleetGradeDiscount.Discount)
            DbCommandWrapper.AddInParameter("@PeriodStart", DbType.DateTime, FleetGradeDiscount.PeriodStart)
            DbCommandWrapper.AddInParameter("@PeriodEnd", DbType.DateTime, FleetGradeDiscount.PeriodEnd)

            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, FleetGradeDiscount.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, FleetGradeDiscount.LastUpdateBy)
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

            Dim FleetGradeDiscount As FleetGradeDiscount = CType(obj, FleetGradeDiscount)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, FleetGradeDiscount.ID)
            DbCommandWrapper.AddInParameter("@Grade", DbType.AnsiString, FleetGradeDiscount.Grade)
            DbCommandWrapper.AddInParameter("@VehicleType", DbType.AnsiString, FleetGradeDiscount.VehicleType)
            DbCommandWrapper.AddInParameter("@Discount", DbType.Decimal, FleetGradeDiscount.Discount)
            DbCommandWrapper.AddInParameter("@PeriodStart", DbType.DateTime, FleetGradeDiscount.PeriodStart)
            DbCommandWrapper.AddInParameter("@PeriodEnd", DbType.DateTime, FleetGradeDiscount.PeriodEnd)

            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, FleetGradeDiscount.RowStatus)
            'DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, FleetGradeDiscount.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As FleetGradeDiscount

            Dim FleetGradeDiscount As FleetGradeDiscount = New FleetGradeDiscount

            FleetGradeDiscount.ID = CType(dr("ID"), Integer)

            If Not dr.IsDBNull(dr.GetOrdinal("Grade")) Then FleetGradeDiscount.Grade = dr("Grade").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("VehicleType")) Then FleetGradeDiscount.VehicleType = dr("VehicleType").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Discount")) Then FleetGradeDiscount.Discount = CType(dr("Discount"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("PeriodStart")) Then FleetGradeDiscount.PeriodStart = CType(dr("PeriodStart"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("PeriodEnd")) Then FleetGradeDiscount.PeriodEnd = CType(dr("PeriodEnd"), DateTime)

            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then FleetGradeDiscount.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then FleetGradeDiscount.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then FleetGradeDiscount.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then FleetGradeDiscount.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then FleetGradeDiscount.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            Return FleetGradeDiscount

        End Function

        Private Sub SetTableName()

            If Not (GetType(FleetGradeDiscount) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(FleetGradeDiscount), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(FleetGradeDiscount).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

