#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : UniformGuide Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2007 
'// ---------------------
'// $History      : $
'// Generated on 7/16/2007 - 11:20:39 AM
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

    Public Class UniformGuideMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertUniformGuide"
        Private m_UpdateStatement As String = "up_UpdateUniformGuide"
        Private m_RetrieveStatement As String = "up_RetrieveUniformGuide"
        Private m_RetrieveListStatement As String = "up_RetrieveUniformGuideList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteUniformGuide"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim uniformGuide As UniformGuide = Nothing
            While dr.Read

                uniformGuide = Me.CreateObject(dr)

            End While

            Return uniformGuide

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim uniformGuideList As ArrayList = New ArrayList

            While dr.Read
                Dim uniformGuide As UniformGuide = Me.CreateObject(dr)
                uniformGuideList.Add(uniformGuide)
            End While

            Return uniformGuideList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim uniformGuide As UniformGuide = CType(obj, UniformGuide)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, uniformGuide.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim uniformGuide As UniformGuide = CType(obj, UniformGuide)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@UniformImage", DbType.Binary, uniformGuide.UniformImage)
            DbCommandWrapper.AddInParameter("@Information", DbType.AnsiString, uniformGuide.Information)
            DbCommandWrapper.AddInParameter("@S", DbType.Decimal, uniformGuide.S)
            DbCommandWrapper.AddInParameter("@M", DbType.Decimal, uniformGuide.M)
            DbCommandWrapper.AddInParameter("@L", DbType.Decimal, uniformGuide.L)
            DbCommandWrapper.AddInParameter("@XL", DbType.Decimal, uniformGuide.XL)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, uniformGuide.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, uniformGuide.LastUpdateBy)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)

            DbCommandWrapper.AddInParameter("@UniformDistributionId", DbType.Int32, Me.GetRefObject(uniformGuide.UniformDistribution))

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

            Dim uniformGuide As UniformGuide = CType(obj, UniformGuide)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, uniformGuide.ID)
            DbCommandWrapper.AddInParameter("@UniformImage", DbType.Binary, uniformGuide.UniformImage)
            DbCommandWrapper.AddInParameter("@Information", DbType.AnsiString, uniformGuide.Information)
            DbCommandWrapper.AddInParameter("@S", DbType.Decimal, uniformGuide.S)
            DbCommandWrapper.AddInParameter("@M", DbType.Decimal, uniformGuide.M)
            DbCommandWrapper.AddInParameter("@L", DbType.Decimal, uniformGuide.L)
            DbCommandWrapper.AddInParameter("@XL", DbType.Decimal, uniformGuide.XL)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, uniformGuide.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, uniformGuide.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)


            DbCommandWrapper.AddInParameter("@UniformDistributionId", DbType.Int32, Me.GetRefObject(uniformGuide.UniformDistribution))

            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As UniformGuide

            Dim uniformGuide As UniformGuide = New UniformGuide

            uniformGuide.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("UniformImage")) Then uniformGuide.UniformImage = CType(dr("UniformImage"), Byte())
            If Not dr.IsDBNull(dr.GetOrdinal("Information")) Then uniformGuide.Information = dr("Information").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("S")) Then uniformGuide.S = CType(dr("S"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("M")) Then uniformGuide.M = CType(dr("M"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("L")) Then uniformGuide.L = CType(dr("L"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("XL")) Then uniformGuide.XL = CType(dr("XL"), Decimal)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then uniformGuide.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then uniformGuide.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then uniformGuide.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then uniformGuide.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then uniformGuide.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("UniformDistributionId")) Then
                uniformGuide.UniformDistribution = New UniformDistribution(CType(dr("UniformDistributionId"), Integer))
            End If

            Return uniformGuide

        End Function

        Private Sub SetTableName()

            If Not (GetType(UniformGuide) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(UniformGuide), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(UniformGuide).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

