
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : VechileColorIsActiveOnPK Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2020 
'// ---------------------
'// $History      : $
'// Generated on 04/03/2020 - 13:23:44
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

    Public Class VechileColorIsActiveOnPKMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertVechileColorIsActiveOnPK"
        Private m_UpdateStatement As String = "up_UpdateVechileColorIsActiveOnPK"
        Private m_RetrieveStatement As String = "up_RetrieveVechileColorIsActiveOnPK"
        Private m_RetrieveListStatement As String = "up_RetrieveVechileColorIsActiveOnPKList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteVechileColorIsActiveOnPK"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim vechileColorIsActiveOnPK As VechileColorIsActiveOnPK = Nothing
            While dr.Read

                vechileColorIsActiveOnPK = Me.CreateObject(dr)

            End While

            Return vechileColorIsActiveOnPK

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim vechileColorIsActiveOnPKList As ArrayList = New ArrayList

            While dr.Read
                Dim vechileColorIsActiveOnPK As VechileColorIsActiveOnPK = Me.CreateObject(dr)
                vechileColorIsActiveOnPKList.Add(vechileColorIsActiveOnPK)
            End While

            Return vechileColorIsActiveOnPKList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim vechileColorIsActiveOnPK As VechileColorIsActiveOnPK = CType(obj, VechileColorIsActiveOnPK)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@id", DbType.Int32, vechileColorIsActiveOnPK.id)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim vechileColorIsActiveOnPK As VechileColorIsActiveOnPK = CType(obj, VechileColorIsActiveOnPK)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@id", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@ProductionYear", DbType.Int16, vechileColorIsActiveOnPK.ProductionYear)
            DbCommandWrapper.AddInParameter("@Status", DbType.AnsiString, vechileColorIsActiveOnPK.Status)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, vechileColorIsActiveOnPK.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LasUpdateBy", DbType.AnsiString, vechileColorIsActiveOnPK.LasUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@VehicleColorID", DbType.Int16, Me.GetRefObject(vechileColorIsActiveOnPK.VechileColor))
            DbCommandWrapper.AddInParameter("@VechileTypeGeneralID", DbType.Int16, Me.GetRefObject(vechileColorIsActiveOnPK.VechileTypeGeneral))
            DbCommandWrapper.AddInParameter("@ModelYear", DbType.Int16, vechileColorIsActiveOnPK.ModelYear)

            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetNewID(ByVal dbCommandWrapper As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper) As Integer

            Return CType(dbCommandWrapper.GetParameterValue("@id"), Integer)

        End Function

        Protected Overrides Function GetPagingRetrieveCommand() As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_PagingQuery)
            DbCommandWrapper.AddInParameter("@Table", DbType.String, m_TableName)
            DbCommandWrapper.AddInParameter("@PK", DbType.String, "id")

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
            DbCommandWrapper.AddInParameter("@id", DbType.Int32, id)

            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetUpdateParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim vechileColorIsActiveOnPK As VechileColorIsActiveOnPK = CType(obj, VechileColorIsActiveOnPK)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@id", DbType.Int32, vechileColorIsActiveOnPK.id)
            DbCommandWrapper.AddInParameter("@ProductionYear", DbType.Int16, vechileColorIsActiveOnPK.ProductionYear)
            DbCommandWrapper.AddInParameter("@Status", DbType.AnsiString, vechileColorIsActiveOnPK.Status)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, vechileColorIsActiveOnPK.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, vechileColorIsActiveOnPK.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LasUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@VehicleColorID", DbType.Int16, Me.GetRefObject(vechileColorIsActiveOnPK.VechileColor))
            DbCommandWrapper.AddInParameter("@VechileTypeGeneralID", DbType.Int16, Me.GetRefObject(vechileColorIsActiveOnPK.VechileTypeGeneral))
            DbCommandWrapper.AddInParameter("@ModelYear", DbType.Int16, vechileColorIsActiveOnPK.ModelYear)

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As VechileColorIsActiveOnPK

            Dim vechileColorIsActiveOnPK As VechileColorIsActiveOnPK = New VechileColorIsActiveOnPK

            vechileColorIsActiveOnPK.id = CType(dr("id"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("ProductionYear")) Then vechileColorIsActiveOnPK.ProductionYear = CType(dr("ProductionYear"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then vechileColorIsActiveOnPK.Status = dr("Status").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then vechileColorIsActiveOnPK.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then vechileColorIsActiveOnPK.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then vechileColorIsActiveOnPK.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LasUpdateBy")) Then vechileColorIsActiveOnPK.LasUpdateBy = dr("LasUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then vechileColorIsActiveOnPK.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("VehicleColorID")) Then
                vechileColorIsActiveOnPK.VechileColor = New VechileColor(CType(dr("VehicleColorID"), Short))
            End If

            If Not dr.IsDBNull(dr.GetOrdinal("VechileTypeGeneralID")) Then
                vechileColorIsActiveOnPK.VechileTypeGeneral = New VechileTypeGeneral(CType(dr("VechileTypeGeneralID"), Short))
            End If

            If Not dr.IsDBNull(dr.GetOrdinal("ModelYear")) Then vechileColorIsActiveOnPK.ModelYear = CType(dr("ModelYear"), Short)


            Return vechileColorIsActiveOnPK

        End Function

        Private Sub SetTableName()

            If Not (GetType(VechileColorIsActiveOnPK) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(VechileColorIsActiveOnPK), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(VechileColorIsActiveOnPK).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

