#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : TempTempServiceBookingActivity Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2005 
'// ---------------------
'// $History      : $
'// Generated on 11/30/2005 - 1:05:06 PM
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

    Public Class TempServiceBookingActivityMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertTempServiceBookingActivity"
        Private m_UpdateStatement As String = "up_UpdateTempServiceBookingActivity"
        Private m_RetrieveStatement As String = "up_RetrieveTempServiceBookingActivity"
        Private m_RetrieveListStatement As String = "up_RetrieveTempServiceBookingActivityList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteTempServiceBookingActivity"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim TempServiceBookingActivity As TempServiceBookingActivity = Nothing
            While dr.Read

                TempServiceBookingActivity = Me.CreateObject(dr)

            End While

            Return TempServiceBookingActivity

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim TempServiceBookingActivityList As ArrayList = New ArrayList

            While dr.Read
                Dim TempServiceBookingActivity As TempServiceBookingActivity = Me.CreateObject(dr)
                TempServiceBookingActivityList.Add(TempServiceBookingActivity)
            End While

            Return TempServiceBookingActivityList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim TempServiceBookingActivity As TempServiceBookingActivity = CType(obj, TempServiceBookingActivity)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, TempServiceBookingActivity.DealerCode)
            DbCommandWrapper.AddInParameter("@VechileTypeCode", DbType.AnsiString, TempServiceBookingActivity.VechileTypeCode)
            DbCommandWrapper.AddInParameter("@ChassisNumber", DbType.AnsiString, TempServiceBookingActivity.ChassisNumber)

            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim TempServiceBookingActivity As TempServiceBookingActivity = CType(obj, TempServiceBookingActivity)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, TempServiceBookingActivity.DealerCode)
            DbCommandWrapper.AddInParameter("@VechileTypeCode", DbType.AnsiString, TempServiceBookingActivity.VechileTypeCode)
            DbCommandWrapper.AddInParameter("@ChassisNumber", DbType.AnsiString, TempServiceBookingActivity.ChassisNumber)
            DbCommandWrapper.AddInParameter("@JenisKegiatan", DbType.Int16, TempServiceBookingActivity.JenisKegiatan)
            DbCommandWrapper.AddInParameter("@JenisService", DbType.Int16, TempServiceBookingActivity.JenisService)

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

            Dim TempServiceBookingActivity As TempServiceBookingActivity = CType(obj, TempServiceBookingActivity)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, TempServiceBookingActivity.ID)

            DbCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, TempServiceBookingActivity.DealerCode)
            DbCommandWrapper.AddInParameter("@VechileTypeCode", DbType.AnsiString, TempServiceBookingActivity.VechileTypeCode)
            DbCommandWrapper.AddInParameter("@ChassisNumber", DbType.AnsiString, TempServiceBookingActivity.ChassisNumber)
            DbCommandWrapper.AddInParameter("@JenisKegiatan", DbType.Int16, TempServiceBookingActivity.JenisKegiatan)
            DbCommandWrapper.AddInParameter("@JenisService", DbType.Int16, TempServiceBookingActivity.JenisService)

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As TempServiceBookingActivity

            Dim TempServiceBookingActivity As TempServiceBookingActivity = New TempServiceBookingActivity

            TempServiceBookingActivity.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerCode")) Then TempServiceBookingActivity.DealerCode = dr("DealerCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("VechileTypeCode")) Then TempServiceBookingActivity.VechileTypeCode = dr("VechileTypeCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ChassisNumber")) Then TempServiceBookingActivity.ChassisNumber = dr("ChassisNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("JenisKegiatan")) Then TempServiceBookingActivity.JenisKegiatan = CType(dr("JenisKegiatan"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("JenisService")) Then TempServiceBookingActivity.JenisService = CType(dr("JenisService"), Integer)

            Return TempServiceBookingActivity

        End Function

        Private Sub SetTableName()

            If Not (GetType(TempServiceBookingActivity) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(TempServiceBookingActivity), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(TempServiceBookingActivity).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

