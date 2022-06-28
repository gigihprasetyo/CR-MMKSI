
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : PODestination Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2016 
'// ---------------------
'// $History      : $
'// Generated on 12/5/2016 - 4:12:45 PM
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

    Public Class PODestinationMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertPODestination"
        Private m_UpdateStatement As String = "up_UpdatePODestination"
        Private m_RetrieveStatement As String = "up_RetrievePODestination"
        Private m_RetrieveListStatement As String = "up_RetrievePODestinationList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeletePODestination"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim pODestination As PODestination = Nothing
            While dr.Read

                pODestination = Me.CreateObject(dr)

            End While

            Return pODestination

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim pODestinationList As ArrayList = New ArrayList

            While dr.Read
                Dim pODestination As PODestination = Me.CreateObject(dr)
                pODestinationList.Add(pODestination)
            End While

            Return pODestinationList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim pODestination As PODestination = CType(obj, PODestination)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, pODestination.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim pODestination As PODestination = CType(obj, PODestination)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@Code", DbType.AnsiString, pODestination.Code)
            DbCommandWrapper.AddInParameter("@Nama", DbType.AnsiString, pODestination.Nama)
            DbCommandWrapper.AddInParameter("@Alamat", DbType.AnsiString, pODestination.Alamat)
            DbCommandWrapper.AddInParameter("@RegionCode", DbType.AnsiString, pODestination.RegionCode)
            DbCommandWrapper.AddInParameter("@RegionDesc", DbType.AnsiString, pODestination.RegionDesc)
            DbCommandWrapper.AddInParameter("@LeadTime", DbType.Int16, pODestination.LeadTime)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, pODestination.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, pODestination.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@CityID", DbType.Int16, Me.GetRefObject(pODestination.City))
            'DbCommandWrapper.AddInParameter("@PODestinationRegionID", DbType.Int32, Me.GetRefObject(pODestination.PODestinationRegion))
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, Me.GetRefObject(pODestination.Dealer))
            DbCommandWrapper.AddInParameter("@DealerDestinationCode", DbType.Int16, Me.GetRefObject(pODestination.DealerDestinationCode))

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

            Dim pODestination As PODestination = CType(obj, PODestination)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, pODestination.ID)
            DbCommandWrapper.AddInParameter("@Code", DbType.AnsiString, pODestination.Code)
            DbCommandWrapper.AddInParameter("@Nama", DbType.AnsiString, pODestination.Nama)
            DbCommandWrapper.AddInParameter("@Alamat", DbType.AnsiString, pODestination.Alamat)
            DbCommandWrapper.AddInParameter("@RegionCode", DbType.AnsiString, pODestination.RegionCode)
            DbCommandWrapper.AddInParameter("@RegionDesc", DbType.AnsiString, pODestination.RegionDesc)
            DbCommandWrapper.AddInParameter("@LeadTime", DbType.Int16, pODestination.LeadTime)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, pODestination.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, pODestination.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@CityID", DbType.Int16, Me.GetRefObject(pODestination.City))
            'DbCommandWrapper.AddInParameter("@PODestinationRegionID", DbType.Int32, Me.GetRefObject(pODestination.PODestinationRegion))
            DbCommandWrapper.AddInParameter("@DealerDestinationCode", DbType.Int16, Me.GetRefObject(pODestination.DealerDestinationCode))
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, Me.GetRefObject(pODestination.Dealer))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As PODestination

            Dim pODestination As PODestination = New PODestination

            pODestination.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Code")) Then pODestination.Code = dr("Code").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Nama")) Then pODestination.Nama = dr("Nama").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Alamat")) Then pODestination.Alamat = dr("Alamat").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RegionCode")) Then pODestination.RegionCode = dr("RegionCode").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("RegionDesc")) Then pODestination.RegionDesc = dr("RegionDesc").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LeadTime")) Then pODestination.LeadTime = CType(dr("LeadTime"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then pODestination.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then pODestination.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then pODestination.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then pODestination.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then pODestination.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("CityID")) Then
                pODestination.City = New City(CType(dr("CityID"), Short))
            End If
            'If Not dr.IsDBNull(dr.GetOrdinal("PODestinationRegionID")) Then
            '    pODestination.PODestinationRegion = New PODestinationRegion(CType(dr("PODestinationRegionID"), Integer))
            'End If
            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then
                pODestination.Dealer = New Dealer(CType(dr("DealerID"), Short))
            End If
            If Not dr.IsDBNull(dr.GetOrdinal("DealerDestinationCode")) Then
                pODestination.DealerDestinationCode = New VWI_Dealer(CType(dr("DealerDestinationCode"), Short))
            End If

            Return pODestination

        End Function

        Private Sub SetTableName()

            If Not (GetType(PODestination) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(PODestination), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(PODestination).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

