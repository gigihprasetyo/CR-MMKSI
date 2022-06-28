#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : TrBookingClass Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2019 
'// ---------------------
'// $History      : $
'// Generated on 8/5/2019 - 11:06:58 AM
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

    Public Class TrBookingClassMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertTrBookingClass"
        Private m_UpdateStatement As String = "up_UpdateTrBookingClass"
        Private m_RetrieveStatement As String = "up_RetrieveTrBookingClass"
        Private m_RetrieveListStatement As String = "up_RetrieveTrBookingClassList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteTrBookingClass"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim trBookingClass As TrBookingClass = Nothing
            While dr.Read

                trBookingClass = Me.CreateObject(dr)

            End While

            Return trBookingClass

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim trBookingClassList As ArrayList = New ArrayList

            While dr.Read
                Dim trBookingClass As TrBookingClass = Me.CreateObject(dr)
                trBookingClassList.Add(trBookingClass)
            End While

            Return trBookingClassList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim trBookingClass As TrBookingClass = CType(obj, TrBookingClass)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, trBookingClass.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim trBookingClass As TrBookingClass = CType(obj, TrBookingClass)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@TrBookingCourseID", DbType.Int32, Me.GetRefObject(trBookingClass.TrBookingCourse))
            DbCommandWrapper.AddInParameter("@TrClassID", DbType.Int32, Me.GetRefObject(trBookingClass.TrClass))
            DbCommandWrapper.AddInParameter("@TrClassRegistrationID", DbType.Int32, Me.GetRefObject(trBookingClass.TrClassRegistration))
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, trBookingClass.Status)
            DbCommandWrapper.AddInParameter("@Reason", DbType.AnsiString, trBookingClass.Reason)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, trBookingClass.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, trBookingClass.LastUpdateBy)
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

            Dim trBookingClass As TrBookingClass = CType(obj, TrBookingClass)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, trBookingClass.ID)
            DbCommandWrapper.AddInParameter("@TrBookingCourseID", DbType.Int32, Me.GetRefObject(trBookingClass.TrBookingCourse))
            DbCommandWrapper.AddInParameter("@TrClassID", DbType.Int32, Me.GetRefObject(trBookingClass.TrClass))
            DbCommandWrapper.AddInParameter("@TrClassRegistrationID", DbType.Int32, Me.GetRefObject(trBookingClass.TrClassRegistration))
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, trBookingClass.Status)
            DbCommandWrapper.AddInParameter("@Reason", DbType.AnsiString, trBookingClass.Reason)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, trBookingClass.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, trBookingClass.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As TrBookingClass

            Dim trBookingClass As TrBookingClass = New TrBookingClass

            trBookingClass.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("TrBookingCourseID")) Then trBookingClass.TrBookingCourse = New TrBookingCourse(CType(dr("TrBookingCourseID"), Integer))
            If Not dr.IsDBNull(dr.GetOrdinal("TrClassID")) Then trBookingClass.TrClass = New TrClass(CType(dr("TrClassID"), Integer))
            If Not dr.IsDBNull(dr.GetOrdinal("TrClassRegistrationID")) Then trBookingClass.TrClassRegistration = New TrClassRegistration(CType(dr("TrClassRegistrationID"), Integer))
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then trBookingClass.Status = CType(dr("Status"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("Reason")) Then trBookingClass.Reason = dr("Reason").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then trBookingClass.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then trBookingClass.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then trBookingClass.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then trBookingClass.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then trBookingClass.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            Return trBookingClass

        End Function

        Private Sub SetTableName()

            If Not (GetType(TrBookingClass) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(TrBookingClass), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(TrBookingClass).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace
