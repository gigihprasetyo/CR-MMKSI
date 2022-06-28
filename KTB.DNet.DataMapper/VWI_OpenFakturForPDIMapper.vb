
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : Mitrais Team
'// PURPOSE       : VWI_OpenFakturForPDI Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2019 
'// ---------------------
'// $History      : $
'// Generated on 09/01/2019 - 7:32:34
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

    Public Class VWI_OpenFakturForPDIMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertVWI_OpenFakturForPDI"
        Private m_UpdateStatement As String = "up_UpdateVWI_OpenFakturForPDI"
        Private m_RetrieveStatement As String = "up_RetrieveVWI_OpenFakturForPDI"
        Private m_RetrieveListStatement As String = "up_RetrieveVWI_OpenFakturForPDIList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteVWI_OpenFakturForPDI"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim VWI_OpenFakturForPDI As VWI_OpenFakturForPDI = Nothing
            While dr.Read

                VWI_OpenFakturForPDI = Me.CreateObject(dr)

            End While

            Return VWI_OpenFakturForPDI

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim VWI_OpenFakturForPDIList As ArrayList = New ArrayList

            While dr.Read
                Dim VWI_OpenFakturForPDI As VWI_OpenFakturForPDI = Me.CreateObject(dr)
                VWI_OpenFakturForPDIList.Add(VWI_OpenFakturForPDI)
            End While

            Return VWI_OpenFakturForPDIList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim VWI_OpenFakturForPDI As VWI_OpenFakturForPDI = CType(obj, VWI_OpenFakturForPDI)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, VWI_OpenFakturForPDI.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim VWI_OpenFakturForPDI As VWI_OpenFakturForPDI = CType(obj, VWI_OpenFakturForPDI)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@OpenFakturDate", DbType.DateTime, VWI_OpenFakturForPDI.OpenFakturDate)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@SoldDealerID", DbType.Int16, VWI_OpenFakturForPDI.SoldDealerID)
            DbCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, VWI_OpenFakturForPDI.DealerCode)
            DbCommandWrapper.AddInParameter("@ChassisNumber", DbType.AnsiString, VWI_OpenFakturForPDI.ChassisNumber)


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

            Dim VWI_OpenFakturForPDI As VWI_OpenFakturForPDI = CType(obj, VWI_OpenFakturForPDI)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, VWI_OpenFakturForPDI.ID)
            DbCommandWrapper.AddInParameter("@OpenFakturDate", DbType.DateTime, VWI_OpenFakturForPDI.OpenFakturDate)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@SoldDealerID", DbType.Int16, VWI_OpenFakturForPDI.SoldDealerID)
            DbCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, VWI_OpenFakturForPDI.DealerCode)
            DbCommandWrapper.AddInParameter("@ChassisNumber", DbType.AnsiString, VWI_OpenFakturForPDI.ChassisNumber)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As VWI_OpenFakturForPDI

            Dim VWI_OpenFakturForPDI As VWI_OpenFakturForPDI = New VWI_OpenFakturForPDI

            VWI_OpenFakturForPDI.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("OpenFakturDate")) Then VWI_OpenFakturForPDI.OpenFakturDate = CType(dr("OpenFakturDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then VWI_OpenFakturForPDI.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("SoldDealerID")) Then VWI_OpenFakturForPDI.SoldDealerID = CType(dr("SoldDealerID"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerCode")) Then VWI_OpenFakturForPDI.DealerCode = dr("DealerCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ChassisNumber")) Then VWI_OpenFakturForPDI.ChassisNumber = dr("ChassisNumber").ToString

            Return VWI_OpenFakturForPDI

        End Function

        Private Sub SetTableName()

            If Not (GetType(VWI_OpenFakturForPDI) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(VWI_OpenFakturForPDI), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(VWI_OpenFakturForPDI).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

