#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : TrBookingCourse Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2019 
'// ---------------------
'// $History      : $
'// Generated on 8/5/2019 - 11:06:07 AM
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

    Public Class TrBookingCourseMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertTrBookingCourse"
        Private m_UpdateStatement As String = "up_UpdateTrBookingCourse"
        Private m_RetrieveStatement As String = "up_RetrieveTrBookingCourse"
        Private m_RetrieveListStatement As String = "up_RetrieveTrBookingCourseList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteTrBookingCourse"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim trBookingCourse As TrBookingCourse = Nothing
            While dr.Read

                trBookingCourse = Me.CreateObject(dr)

            End While

            Return trBookingCourse

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim trBookingCourseList As ArrayList = New ArrayList

            While dr.Read
                Dim trBookingCourse As TrBookingCourse = Me.CreateObject(dr)
                trBookingCourseList.Add(trBookingCourse)
            End While

            Return trBookingCourseList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim trBookingCourse As TrBookingCourse = CType(obj, TrBookingCourse)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, trBookingCourse.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim trBookingCourse As TrBookingCourse = CType(obj, TrBookingCourse)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@TraineeID", DbType.Int32, Me.GetRefObject(trBookingCourse.TrTrainee))
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, Me.GetRefObject(trBookingCourse.Dealer))
            DbCommandWrapper.AddInParameter("@TrCourseID", DbType.Int32, Me.GetRefObject(trBookingCourse.TrCourse))
            DbCommandWrapper.AddInParameter("@FiscalYear", DbType.AnsiString, trBookingCourse.FiscalYear)
            DbCommandWrapper.AddInParameter("@RegistrationDate", DbType.DateTime, trBookingCourse.RegistrationDate)
            DbCommandWrapper.AddInParameter("@TrClassRegistrationID", DbType.Int32, Me.GetRefObject(trBookingCourse.TrClassRegistration))
            DbCommandWrapper.AddInParameter("@PrioritySequence", DbType.Int16, trBookingCourse.PrioritySequence)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, trBookingCourse.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            If trBookingCourse.ValidateDate.Year = 1753 Then
                DbCommandWrapper.AddInParameter("@ValidateDate", DbType.DateTime, DBNull.Value)
            Else
                DbCommandWrapper.AddInParameter("@ValidateDate", DbType.DateTime, trBookingCourse.ValidateDate)
            End If
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, trBookingCourse.LastUpdateBy)
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

            Dim trBookingCourse As TrBookingCourse = CType(obj, TrBookingCourse)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, trBookingCourse.ID)
            DbCommandWrapper.AddInParameter("@TraineeID", DbType.Int32, Me.GetRefObject(trBookingCourse.TrTrainee))
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, Me.GetRefObject(trBookingCourse.Dealer))
            DbCommandWrapper.AddInParameter("@TrCourseID", DbType.Int32, Me.GetRefObject(trBookingCourse.TrCourse))
            DbCommandWrapper.AddInParameter("@FiscalYear", DbType.AnsiString, trBookingCourse.FiscalYear)
            DbCommandWrapper.AddInParameter("@RegistrationDate", DbType.DateTime, trBookingCourse.RegistrationDate)
            DbCommandWrapper.AddInParameter("@TrClassRegistrationID", DbType.Int32, Me.GetRefObject(trBookingCourse.TrClassRegistration))
            DbCommandWrapper.AddInParameter("@PrioritySequence", DbType.Int16, trBookingCourse.PrioritySequence)
            If trBookingCourse.ValidateDate.Year = 1753 Then
                DbCommandWrapper.AddInParameter("@ValidateDate", DbType.DateTime, DBNull.Value)
            Else
                DbCommandWrapper.AddInParameter("@ValidateDate", DbType.DateTime, trBookingCourse.ValidateDate)
            End If
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, trBookingCourse.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, trBookingCourse.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As TrBookingCourse

            Dim trBookingCourse As TrBookingCourse = New TrBookingCourse

            trBookingCourse.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("TraineeID")) Then trBookingCourse.TrTrainee = New TrTrainee(CType(dr("TraineeID"), Integer))
            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then trBookingCourse.Dealer = New Dealer(CType(dr("DealerID"), Short))
            If Not dr.IsDBNull(dr.GetOrdinal("TrCourseID")) Then trBookingCourse.TrCourse = New TrCourse(CType(dr("TrCourseID"), Integer))
            If Not dr.IsDBNull(dr.GetOrdinal("FiscalYear")) Then trBookingCourse.FiscalYear = dr("FiscalYear").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RegistrationDate")) Then trBookingCourse.RegistrationDate = CType(dr("RegistrationDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("TrClassRegistrationID")) Then trBookingCourse.TrClassRegistration = New TrClassRegistration(CType(dr("TrClassRegistrationID"), Integer))
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then trBookingCourse.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("PrioritySequence")) Then trBookingCourse.PrioritySequence = CType(dr("PrioritySequence"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("ValidateDate")) Then trBookingCourse.ValidateDate = CType(dr("ValidateDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then trBookingCourse.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then trBookingCourse.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then trBookingCourse.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then trBookingCourse.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            Return trBookingCourse

        End Function

        Private Sub SetTableName()

            If Not (GetType(TrBookingCourse) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(TrBookingCourse), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(TrBookingCourse).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace
