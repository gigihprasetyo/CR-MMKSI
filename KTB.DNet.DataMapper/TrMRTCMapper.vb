
#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : TrMRTC Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2019 
'// ---------------------
'// $History      : $
'// Generated on 10/07/2019 - 10:13:06
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

    Public Class TrMRTCMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertTrMRTC"
        Private m_UpdateStatement As String = "up_UpdateTrMRTC"
        Private m_RetrieveStatement As String = "up_RetrieveTrMRTC"
        Private m_RetrieveListStatement As String = "up_RetrieveTrMRTCList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteTrMRTC"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim trMRTC As TrMRTC = Nothing
            While dr.Read

                trMRTC = Me.CreateObject(dr)

            End While

            Return trMRTC

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim trMRTCList As ArrayList = New ArrayList

            While dr.Read
                Dim trMRTC As TrMRTC = Me.CreateObject(dr)
                trMRTCList.Add(trMRTC)
            End While

            Return trMRTCList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim trMRTC As TrMRTC = CType(obj, TrMRTC)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, trMRTC.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim trMRTC As TrMRTC = CType(obj, TrMRTC)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 4)
            DbCommandWrapper.AddInParameter("@Code", DbType.AnsiString, trMRTC.Code)
            DbCommandWrapper.AddInParameter("@Name", DbType.AnsiString, trMRTC.Name)
            DbCommandWrapper.AddInParameter("@Grade", DbType.Int16, trMRTC.Grade)
            DbCommandWrapper.AddInParameter("@IsMainDealer", DbType.Boolean, trMRTC.IsMainDealer)
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, Me.GetRefObject(trMRTC.Dealer))
            DbCommandWrapper.AddInParameter("@Address", DbType.AnsiString, trMRTC.Address)
            DbCommandWrapper.AddInParameter("@MainAreaID", DbType.Int32, Me.GetRefObject(trMRTC.MainArea))
            DbCommandWrapper.AddInParameter("@Area1ID", DbType.Int32, Me.GetRefObject(trMRTC.Area1))
            DbCommandWrapper.AddInParameter("@CityID", DbType.Int32, Me.GetRefObject(trMRTC.City))
            DbCommandWrapper.AddInParameter("@PricePerDay", DbType.Decimal, trMRTC.PricePerDay)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, trMRTC.Status)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, trMRTC.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, trMRTC.LastUpdateBy)
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

            Dim trMRTC As TrMRTC = CType(obj, TrMRTC)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, trMRTC.ID)
            DbCommandWrapper.AddInParameter("@Code", DbType.AnsiString, trMRTC.Code)
            DbCommandWrapper.AddInParameter("@Name", DbType.AnsiString, trMRTC.Name)
            DbCommandWrapper.AddInParameter("@Grade", DbType.Int16, trMRTC.Grade)
            DbCommandWrapper.AddInParameter("@IsMainDealer", DbType.Boolean, trMRTC.IsMainDealer)
            DbCommandWrapper.AddInParameter("@DealerID", DbType.Int16, Me.GetRefObject(trMRTC.Dealer))
            DbCommandWrapper.AddInParameter("@Address", DbType.AnsiString, trMRTC.Address)
            DbCommandWrapper.AddInParameter("@MainAreaID", DbType.Int32, Me.GetRefObject(trMRTC.MainArea))
            DbCommandWrapper.AddInParameter("@Area1ID", DbType.Int32, Me.GetRefObject(trMRTC.Area1))
            DbCommandWrapper.AddInParameter("@CityID", DbType.Int32, Me.GetRefObject(trMRTC.City))
            DbCommandWrapper.AddInParameter("@PricePerDay", DbType.Decimal, trMRTC.PricePerDay)
            DbCommandWrapper.AddInParameter("@Status", DbType.Int16, trMRTC.Status)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, trMRTC.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, trMRTC.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As TrMRTC

            Dim trMRTC As TrMRTC = New TrMRTC

            trMRTC.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Code")) Then trMRTC.Code = dr("Code").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Name")) Then trMRTC.Name = dr("Name").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Grade")) Then trMRTC.Grade = CType(dr("Grade"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("IsMainDealer")) Then trMRTC.IsMainDealer = CBool(dr("IsMainDealer"))
            If Not dr.IsDBNull(dr.GetOrdinal("Address")) Then trMRTC.Address = dr("Address").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Status")) Then trMRTC.Status = CType(dr("Status"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("PricePerDay")) Then trMRTC.PricePerDay = CType(dr("PricePerDay"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then trMRTC.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then trMRTC.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then trMRTC.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then trMRTC.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then trMRTC.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            If Not dr.IsDBNull(dr.GetOrdinal("DealerID")) Then
                trMRTC.Dealer = New Dealer(CType(dr("DealerID"), Integer))
            End If

            If Not dr.IsDBNull(dr.GetOrdinal("MainAreaID")) Then
                trMRTC.MainArea = New MainArea(CType(dr("MainAreaID"), Integer))
            End If

            If Not dr.IsDBNull(dr.GetOrdinal("Area1ID")) Then
                trMRTC.Area1 = New Area1(CType(dr("Area1ID"), Integer))
            End If

            If Not dr.IsDBNull(dr.GetOrdinal("CityID")) Then
                trMRTC.City = New City(CType(dr("CityID"), Integer))
            End If

            Return trMRTC

        End Function

        Private Sub SetTableName()

            If Not (GetType(TrMRTC) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(TrMRTC), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(TrMRTC).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

