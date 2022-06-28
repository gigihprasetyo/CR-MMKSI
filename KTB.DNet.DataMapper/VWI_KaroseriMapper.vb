
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : VWI_Karoseri Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 20/07/2018 - 13:24:15
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

    Public Class VWI_KaroseriMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertVWI_Karoseri"
        Private m_UpdateStatement As String = "up_UpdateVWI_Karoseri"
        Private m_RetrieveStatement As String = "up_RetrieveVWI_Karoseri"
        Private m_RetrieveListStatement As String = "up_RetrieveVWI_KaroseriList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteVWI_Karoseri"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim VWI_Karoseri As VWI_Karoseri = Nothing
            While dr.Read

                VWI_Karoseri = Me.CreateObject(dr)

            End While

            Return VWI_Karoseri

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim VWI_KaroseriList As ArrayList = New ArrayList

            While dr.Read
                Dim VWI_Karoseri As VWI_Karoseri = Me.CreateObject(dr)
                VWI_KaroseriList.Add(VWI_Karoseri)
            End While

            Return VWI_KaroseriList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim VWI_Karoseri As VWI_Karoseri = CType(obj, VWI_Karoseri)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@CODE", DbType.AnsiString, VWI_Karoseri.CODE)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim VWI_Karoseri As VWI_Karoseri = CType(obj, VWI_Karoseri)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@CODE", DbType.AnsiString, 16)
            DbCommandWrapper.AddInParameter("@NAME", DbType.AnsiString, VWI_Karoseri.NAME)
            DbCommandWrapper.AddInParameter("@PROVINCE", DbType.AnsiString, VWI_Karoseri.PROVINCE)
            DbCommandWrapper.AddInParameter("@CITY", DbType.AnsiString, VWI_Karoseri.CITY)
            DbCommandWrapper.AddInParameter("@LASTUPDATETIME", DbType.DateTime, VWI_Karoseri.LASTUPDATETIME)
            DbCommandWrapper.AddInParameter("@STATUS", DbType.Int32, VWI_Karoseri.STATUS)


            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetNewID(ByVal dbCommandWrapper As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper) As Integer

            Return CType(dbCommandWrapper.GetParameterValue("@CODE"), Integer)

        End Function

        Protected Overrides Function GetPagingRetrieveCommand() As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_PagingQuery)
            DbCommandWrapper.AddInParameter("@Table", DbType.String, m_TableName)
            DbCommandWrapper.AddInParameter("@PK", DbType.String, "CODE")

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
            DbCommandWrapper.AddInParameter("@CODE", DbType.Int32, id)

            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetUpdateParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim VWI_Karoseri As VWI_Karoseri = CType(obj, VWI_Karoseri)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@CODE", DbType.AnsiString, VWI_Karoseri.CODE)
            DbCommandWrapper.AddInParameter("@NAME", DbType.AnsiString, VWI_Karoseri.NAME)
            DbCommandWrapper.AddInParameter("@PROVINCE", DbType.AnsiString, VWI_Karoseri.PROVINCE)
            DbCommandWrapper.AddInParameter("@CITY", DbType.AnsiString, VWI_Karoseri.CITY)
            DbCommandWrapper.AddInParameter("@LASTUPDATETIME", DbType.DateTime, VWI_Karoseri.LASTUPDATETIME)
            DbCommandWrapper.AddInParameter("@STATUS", DbType.Int32, VWI_Karoseri.STATUS)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As VWI_Karoseri

            Dim VWI_Karoseri As VWI_Karoseri = New VWI_Karoseri

            VWI_Karoseri.CODE = dr("CODE").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("NAME")) Then VWI_Karoseri.NAME = dr("NAME").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("PROVINCE")) Then VWI_Karoseri.PROVINCE = dr("PROVINCE").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CITY")) Then VWI_Karoseri.CITY = dr("CITY").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LASTUPDATETIME")) Then VWI_Karoseri.LASTUPDATETIME = CType(dr("LASTUPDATETIME"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("STATUS")) Then VWI_Karoseri.STATUS = CType(dr("STATUS"), Integer)

            Return VWI_Karoseri

        End Function

        Private Sub SetTableName()

            If Not (GetType(VWI_Karoseri) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(VWI_Karoseri), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(VWI_Karoseri).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

