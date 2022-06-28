
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : Mitrais Team
'// PURPOSE       : VWI_ChassisStatusFakturMapper Objects Mapper.
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

    Public Class VWI_ChassisStatusFakturMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertVWI_ChassisStatusFakturMapper"
        Private m_UpdateStatement As String = "up_UpdateVWI_ChassisStatusFakturMapper"
        Private m_RetrieveStatement As String = "up_RetrieveVWI_ChassisStatusFakturMapper"
        Private m_RetrieveListStatement As String = "up_RetrieveVWI_ChassisStatusFakturMapper"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteVWI_ChassisStatusFakturMapper"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim VWI_ChassisStatusFaktur As VWI_ChassisStatusFaktur = Nothing
            While dr.Read

                VWI_ChassisStatusFaktur = Me.CreateObject(dr)

            End While

            Return VWI_ChassisStatusFaktur

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim VWI_ChassisStatusFakturList As ArrayList = New ArrayList

            While dr.Read
                Dim VWI_ChassisStatusFaktur As VWI_ChassisStatusFaktur = Me.CreateObject(dr)
                VWI_ChassisStatusFakturList.Add(VWI_ChassisStatusFaktur)
            End While

            Return VWI_ChassisStatusFakturList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim VWI_ChassisStatusFaktur As VWI_ChassisStatusFaktur = CType(obj, VWI_ChassisStatusFaktur)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, VWI_ChassisStatusFaktur.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper


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

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As VWI_ChassisStatusFaktur

            Dim VWI_ChassisStatusFaktur As VWI_ChassisStatusFaktur = New VWI_ChassisStatusFaktur

            VWI_ChassisStatusFaktur.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("ChassisNumner")) Then VWI_ChassisStatusFaktur.ChassisNumber = dr("SoldDealerID").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DealerCode")) Then VWI_ChassisStatusFaktur.DealerCode = dr("DealerCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DealerName")) Then VWI_ChassisStatusFaktur.DealerName = dr("DealerName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RevisionStatus")) Then VWI_ChassisStatusFaktur.RevisionStatus = dr("RevisionStatus").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RevisionDate")) Then VWI_ChassisStatusFaktur.RevisionDate = CType(dr("RevisionDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("RevisionType")) Then VWI_ChassisStatusFaktur.RevisionType = dr("RevisionType").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("SPKNumber")) Then VWI_ChassisStatusFaktur.SPKNumber = dr("SPKNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DealerSPKNumber")) Then VWI_ChassisStatusFaktur.DealerSPKNumber = dr("DealerSPKNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("FakturDate")) Then VWI_ChassisStatusFaktur.FakturNumber = dr("FakturNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("OpenFakturDate")) Then VWI_ChassisStatusFaktur.FakturDate = CType(dr("FakturDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("ValidateDate")) Then VWI_ChassisStatusFaktur.ValidateDate = CType(dr("ValidateDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("ConfirmDate")) Then VWI_ChassisStatusFaktur.ConfirmDate = CType(dr("ConfirmDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("DownloadDate")) Then VWI_ChassisStatusFaktur.DownloadDate = CType(dr("DownloadDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("PrintedDate")) Then VWI_ChassisStatusFaktur.PrintedDate = CType(dr("PrintedDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("FakturStatus")) Then VWI_ChassisStatusFaktur.FakturStatus = dr("FakturStatus").ToString

            If Not dr.IsDBNull(dr.GetOrdinal("ETDDate")) Then VWI_ChassisStatusFaktur.ETDDate = CType(dr("ETDDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("EffectiveDate")) Then VWI_ChassisStatusFaktur.EffectiveDate = CType(dr("EffectiveDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("ReleaseDate")) Then VWI_ChassisStatusFaktur.ReleaseDate = CType(dr("ReleaseDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("ATDDate")) Then VWI_ChassisStatusFaktur.ATDDate = CType(dr("ATDDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("ETADate")) Then VWI_ChassisStatusFaktur.ETADate = CType(dr("ETADate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("ATADate")) Then VWI_ChassisStatusFaktur.ATADate = CType(dr("ATADate"), DateTime)

            Return VWI_ChassisStatusFaktur

        End Function

        Private Sub SetTableName()

            If Not (GetType(VWI_ChassisStatusFaktur) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(VWI_ChassisStatusFaktur), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(VWI_ChassisStatusFaktur).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace
Public Class VWI_ChassisStatusFakturMapper

End Class
