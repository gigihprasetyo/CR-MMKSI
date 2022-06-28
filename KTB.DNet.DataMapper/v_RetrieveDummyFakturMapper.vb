#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : v_RetrieveDummyFaktur Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2009 
'// ---------------------
'// $History      : $
'// Generated on 18/4/2019 - 9:22:43 AM
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
    Public Class v_RetrieveDummyFakturMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_Insertv_RetrieveDummyFaktur"
        Private m_UpdateStatement As String = "up_Updatev_RetrieveDummyFaktur"
        Private m_RetrieveStatement As String = "up_Retrievev_RetrieveDummyFaktur"
        Private m_RetrieveListStatement As String = "up_Retrievev_RetrieveDummyFakturList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_Deletev_RetrieveDummyFaktur"

#End Region

#Region "Protected Methods"
        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object
            Dim v_RetrieveDummyFaktur As v_RetrieveDummyFaktur = Nothing
            While dr.Read
                v_RetrieveDummyFaktur = Me.CreateObject(dr)
            End While
            Return v_RetrieveDummyFaktur
        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim v_RetrieveDummyFakturList As ArrayList = New ArrayList

            While dr.Read
                Dim v_RetrieveDummyFaktur As v_RetrieveDummyFaktur = Me.CreateObject(dr)
                v_RetrieveDummyFakturList.Add(v_RetrieveDummyFaktur)
            End While

            Return v_RetrieveDummyFakturList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim v_RetrieveDummyFaktur As v_RetrieveDummyFaktur = CType(obj, v_RetrieveDummyFaktur)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, v_RetrieveDummyFaktur.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim v_RetrieveDummyFaktur As v_RetrieveDummyFaktur = CType(obj, v_RetrieveDummyFaktur)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@FakturStatus", DbType.AnsiString, v_RetrieveDummyFaktur.FakturStatus)
            DbCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, v_RetrieveDummyFaktur.DealerCode)
            DbCommandWrapper.AddInParameter("@ChassisNumber", DbType.AnsiString, v_RetrieveDummyFaktur.ChassisNumber)
            DbCommandWrapper.AddInParameter("@ProjectName", DbType.AnsiString, v_RetrieveDummyFaktur.ProjectName)
            DbCommandWrapper.AddInParameter("@DODate", DbType.DateTime, v_RetrieveDummyFaktur.DODate)
            DbCommandWrapper.AddInParameter("@FakturDate", DbType.DateTime, v_RetrieveDummyFaktur.FakturDate)
            DbCommandWrapper.AddInParameter("@OpenFakturDate", DbType.DateTime, v_RetrieveDummyFaktur.OpenFakturDate)
            DbCommandWrapper.AddInParameter("@ValidateTime", DbType.DateTime, v_RetrieveDummyFaktur.ValidateTime)
            DbCommandWrapper.AddInParameter("@DONumber", DbType.AnsiString, v_RetrieveDummyFaktur.DONumber)
            DbCommandWrapper.AddInParameter("@AlreadySaled", DbType.Byte, v_RetrieveDummyFaktur.AlreadySaled)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, v_RetrieveDummyFaktur.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, v_RetrieveDummyFaktur.LastUpdateBy)
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

            Dim v_RetrieveDummyFaktur As v_RetrieveDummyFaktur = CType(obj, v_RetrieveDummyFaktur)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, v_RetrieveDummyFaktur.ID)
            DbCommandWrapper.AddInParameter("@FakturStatus", DbType.AnsiString, v_RetrieveDummyFaktur.FakturStatus)
            DbCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, v_RetrieveDummyFaktur.DealerCode)
            DbCommandWrapper.AddInParameter("@ChassisNumber", DbType.AnsiString, v_RetrieveDummyFaktur.ChassisNumber)
            DbCommandWrapper.AddInParameter("@ProjectName", DbType.AnsiString, v_RetrieveDummyFaktur.ProjectName)
            DbCommandWrapper.AddInParameter("@DODate", DbType.DateTime, v_RetrieveDummyFaktur.DODate)
            DbCommandWrapper.AddInParameter("@FakturDate", DbType.DateTime, v_RetrieveDummyFaktur.FakturDate)
            DbCommandWrapper.AddInParameter("@OpenFakturDate", DbType.DateTime, v_RetrieveDummyFaktur.OpenFakturDate)
            DbCommandWrapper.AddInParameter("@ValidateTime", DbType.DateTime, v_RetrieveDummyFaktur.ValidateTime)
            DbCommandWrapper.AddInParameter("@DONumber", DbType.AnsiString, v_RetrieveDummyFaktur.DONumber)
            DbCommandWrapper.AddInParameter("@AlreadySaled", DbType.Byte, v_RetrieveDummyFaktur.AlreadySaled)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, v_RetrieveDummyFaktur.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, v_RetrieveDummyFaktur.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            Return DbCommandWrapper

        End Function

#End Region

#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As v_RetrieveDummyFaktur

            Dim v_RetrieveDummyFaktur As v_RetrieveDummyFaktur = New v_RetrieveDummyFaktur

            v_RetrieveDummyFaktur.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("ChassisNumber")) Then v_RetrieveDummyFaktur.ChassisNumber = dr("ChassisNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DODate")) Then v_RetrieveDummyFaktur.DODate = CType(dr("DODate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("DONumber")) Then v_RetrieveDummyFaktur.DONumber = dr("DONumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("AlreadySaled")) Then v_RetrieveDummyFaktur.AlreadySaled = CType(dr("AlreadySaled"), Int16)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then v_RetrieveDummyFaktur.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then v_RetrieveDummyFaktur.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then v_RetrieveDummyFaktur.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then v_RetrieveDummyFaktur.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then v_RetrieveDummyFaktur.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("FakturStatus")) Then v_RetrieveDummyFaktur.FakturStatus = dr("FakturStatus").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DealerCode")) Then v_RetrieveDummyFaktur.DealerCode = dr("DealerCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("ProjectName")) Then v_RetrieveDummyFaktur.ProjectName = dr("ProjectName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("FakturNumber")) Then v_RetrieveDummyFaktur.FakturNumber = dr("FakturNumber").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("FakturDate")) Then v_RetrieveDummyFaktur.FakturDate = CType(dr("FakturDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("OpenFakturDate")) Then v_RetrieveDummyFaktur.OpenFakturDate = CType(dr("OpenFakturDate"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("ValidateTime")) Then v_RetrieveDummyFaktur.ValidateTime = CType(dr("ValidateTime"), DateTime)

            If Not dr.IsDBNull(dr.GetOrdinal("CategoryID")) Then
                v_RetrieveDummyFaktur.Category = New Category(CType(dr("CategoryID"), Short))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("VehicleTypeID")) Then
                v_RetrieveDummyFaktur.VehicleType = New VechileType(CType(dr("VehicleTypeID"), Short))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("VehicleColorID")) Then
                v_RetrieveDummyFaktur.VehicleColor = New VechileColor(CType(dr("VehicleColorID"), Short))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("VehicleModelID")) Then
                v_RetrieveDummyFaktur.VehicleModel = New VechileModel(CType(dr("VehicleModelID"), Short))
            End If

            Return v_RetrieveDummyFaktur

        End Function

        Private Sub SetTableName()

            If Not (GetType(v_RetrieveDummyFaktur) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(v_RetrieveDummyFaktur), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not IsNothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(v_RetrieveDummyFaktur).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

    End Class
End Namespace


