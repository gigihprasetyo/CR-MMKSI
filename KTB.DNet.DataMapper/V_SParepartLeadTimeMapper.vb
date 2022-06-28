
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : V_SParepartLeadTime Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2016 
'// ---------------------
'// $History      : $
'// Generated on 9/9/2016 - 2:47:05 PM
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

    Public Class V_SParepartLeadTimeMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertV_SParepartLeadTime"
        Private m_UpdateStatement As String = "up_UpdateV_SParepartLeadTime"
        Private m_RetrieveStatement As String = "up_RetrieveV_SParepartLeadTime"
        Private m_RetrieveListStatement As String = "up_RetrieveV_SParepartLeadTimeList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteV_SParepartLeadTime"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim v_SParepartLeadTime As V_SParepartLeadTime = Nothing
            While dr.Read

                v_SParepartLeadTime = Me.CreateObject(dr)

            End While

            Return v_SParepartLeadTime

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim v_SParepartLeadTimeList As ArrayList = New ArrayList

            While dr.Read
                Dim v_SParepartLeadTime As V_SParepartLeadTime = Me.CreateObject(dr)
                v_SParepartLeadTimeList.Add(v_SParepartLeadTime)
            End While

            Return v_SParepartLeadTimeList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim v_SParepartLeadTime As V_SParepartLeadTime = CType(obj, V_SParepartLeadTime)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int16, v_SParepartLeadTime.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim v_SParepartLeadTime As V_SParepartLeadTime = CType(obj, V_SParepartLeadTime)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int16, 2)
            DbCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, v_SParepartLeadTime.DealerCode)
            DbCommandWrapper.AddInParameter("@DealerName", DbType.AnsiString, v_SParepartLeadTime.DealerName)
            DbCommandWrapper.AddInParameter("@CityName", DbType.AnsiString, v_SParepartLeadTime.CityName)
            DbCommandWrapper.AddInParameter("@Area", DbType.AnsiString, v_SParepartLeadTime.Area)
            DbCommandWrapper.AddInParameter("@RO", DbType.Int16, v_SParepartLeadTime.RO)
            DbCommandWrapper.AddInParameter("@EO", DbType.Int16, v_SParepartLeadTime.EO)


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

            Dim v_SParepartLeadTime As V_SParepartLeadTime = CType(obj, V_SParepartLeadTime)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int16, v_SParepartLeadTime.ID)
            DbCommandWrapper.AddInParameter("@DealerCode", DbType.AnsiString, v_SParepartLeadTime.DealerCode)
            DbCommandWrapper.AddInParameter("@DealerName", DbType.AnsiString, v_SParepartLeadTime.DealerName)
            DbCommandWrapper.AddInParameter("@CityName", DbType.AnsiString, v_SParepartLeadTime.CityName)
            DbCommandWrapper.AddInParameter("@Area", DbType.AnsiString, v_SParepartLeadTime.Area)
            DbCommandWrapper.AddInParameter("@RO", DbType.Int16, v_SParepartLeadTime.RO)
            DbCommandWrapper.AddInParameter("@EO", DbType.Int16, v_SParepartLeadTime.EO)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As V_SParepartLeadTime

            Dim v_SParepartLeadTime As V_SParepartLeadTime = New V_SParepartLeadTime

            v_SParepartLeadTime.ID = CType(dr("ID"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("DealerCode")) Then v_SParepartLeadTime.DealerCode = dr("DealerCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("DealerName")) Then v_SParepartLeadTime.DealerName = dr("DealerName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CityName")) Then v_SParepartLeadTime.CityName = dr("CityName").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Area")) Then v_SParepartLeadTime.Area = dr("Area").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RO")) Then v_SParepartLeadTime.RO = CType(dr("RO"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("EO")) Then v_SParepartLeadTime.EO = CType(dr("EO"), Short)

            Return v_SParepartLeadTime

        End Function

        Private Sub SetTableName()

            If Not (GetType(V_SParepartLeadTime) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(V_SParepartLeadTime), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(V_SParepartLeadTime).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

