
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : Mitrais Team
'// PURPOSE       : VWI_OpenFaktur Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2018 
'// ---------------------
'// $History      : $
'// Generated on 06/03/2018 - 16:24:16
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

    Public Class VWI_OpenFakturMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertVWI_OpenFaktur"
        Private m_UpdateStatement As String = "up_UpdateVWI_OpenFaktur"
        Private m_RetrieveStatement As String = "up_RetrieveVWI_OpenFaktur"
        Private m_RetrieveListStatement As String = "up_RetrieveVWI_OpenFakturList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteVWI_OpenFaktur"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim vWI_OpenFaktur As VWI_OpenFaktur = Nothing
            While dr.Read

                vWI_OpenFaktur = Me.CreateObject(dr)

            End While

            Return vWI_OpenFaktur

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim vWI_OpenFakturList As ArrayList = New ArrayList

            While dr.Read
                Dim vWI_OpenFaktur As VWI_OpenFaktur = Me.CreateObject(dr)
                vWI_OpenFakturList.Add(vWI_OpenFaktur)
            End While

            Return vWI_OpenFakturList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim vWI_OpenFaktur As VWI_OpenFaktur = CType(obj, VWI_OpenFaktur)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, vWI_OpenFaktur.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim vWI_OpenFaktur As VWI_OpenFaktur = CType(obj, VWI_OpenFaktur)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@SoldDealerID", DbType.Int16, vWI_OpenFaktur.SoldDealerID)
            DbCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, vWI_OpenFaktur.DealerCode)
            DbCommandWrapper.AddInParameter("@ChassisNumber", DbType.AnsiString, vWI_OpenFaktur.ChassisNumber)
            DbCommandWrapper.AddInParameter("@SPKNumber", DbType.AnsiString, vWI_OpenFaktur.SPKNumber)
            DbCommandWrapper.AddInParameter("@OpenFakturDate", DbType.DateTime, vWI_OpenFaktur.OpenFakturDate)
            DbCommandWrapper.AddInParameter("@LastUpdateTime", DbType.DateTime, DateTime.Now)


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

            Dim vWI_OpenFaktur As VWI_OpenFaktur = CType(obj, VWI_OpenFaktur)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, vWI_OpenFaktur.ID)
            DbCommandWrapper.AddInParameter("@SoldDealerID", DbType.Int16, vWI_OpenFaktur.SoldDealerID)
            DbCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, vWI_OpenFaktur.DealerCode)
            DbCommandWrapper.AddInParameter("@ChassisNumber", DbType.AnsiString, vWI_OpenFaktur.ChassisNumber)
            DbCommandWrapper.AddInParameter("@SPKNumber", DbType.AnsiString, vWI_OpenFaktur.SPKNumber)
            DbCommandWrapper.AddInParameter("@OpenFakturDate", DbType.DateTime, vWI_OpenFaktur.OpenFakturDate)
            DbCommandWrapper.AddInParameter("@LastUpdateTime", DbType.DateTime, DateTime.Now)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As VWI_OpenFaktur

            Dim vWI_OpenFaktur As VWI_OpenFaktur = New VWI_OpenFaktur

            vWI_OpenFaktur.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("SoldDealerID")) Then vWI_OpenFaktur.SoldDealerID = CType(dr("SoldDealerID"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerCode")) Then vWI_OpenFaktur.DealerCode = dr("DealerCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ChassisNumber")) Then vWI_OpenFaktur.ChassisNumber = dr("ChassisNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SPKNumber")) Then vWI_OpenFaktur.SPKNumber = dr("SPKNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("OpenFakturDate")) Then vWI_OpenFaktur.OpenFakturDate = CType(dr("OpenFakturDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then vWI_OpenFaktur.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            Return vWI_OpenFaktur

        End Function

        Private Sub SetTableName()

            If Not (GetType(VWI_OpenFaktur) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(VWI_OpenFaktur), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(VWI_OpenFaktur).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace