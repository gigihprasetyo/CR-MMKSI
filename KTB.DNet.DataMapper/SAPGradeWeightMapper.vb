#Region "Summary"
'// ===========================================================================
'// AUTHOR        : DNet Team
'// PURPOSE       : SAPGradeWeight Objects Mapper.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright  2007 
'// ---------------------
'// $History      : $
'// Generated on 9/5/2007 - 4:50:24 PM
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

    Public Class SAPGradeWeightMapper
        Inherits AbstractMapper

#Region "Constructors/Destructors/Finalizers"

        Public Sub New()
            Db = DatabaseFactory.CreateDatabase
            SetTableName()
        End Sub

#End Region

#Region "Private Variables"

        Private m_InsertStatement As String = "up_InsertSAPGradeWeight"
        Private m_UpdateStatement As String = "up_UpdateSAPGradeWeight"
        Private m_RetrieveStatement As String = "up_RetrieveSAPGradeWeight"
        Private m_RetrieveListStatement As String = "up_RetrieveSAPGradeWeightList"
        Private m_DinamicQuery As String = "up_DinamicQuery"
        Private m_PagingQuery As String = "up_PagingQuery"
        Private m_DeleteStatement As String = "up_DeleteSAPGradeWeight"

#End Region

#Region "Protected Methods"

        Protected Overrides Function DoRetrieve(ByVal dr As System.Data.IDataReader) As Object

            Dim sAPGradeWeight As SAPGradeWeight = Nothing
            While dr.Read

                sAPGradeWeight = Me.CreateObject(dr)

            End While

            Return sAPGradeWeight

        End Function

        Protected Overrides Function DoRetrieveList(ByVal dr As System.Data.IDataReader) As System.Collections.ArrayList

            Dim sAPGradeWeightList As ArrayList = New ArrayList

            While dr.Read
                Dim sAPGradeWeight As SAPGradeWeight = Me.CreateObject(dr)
                sAPGradeWeightList.Add(sAPGradeWeight)
            End While

            Return sAPGradeWeightList

        End Function

        Protected Overrides Function GetDeleteParameter(ByVal obj As Object) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim sAPGradeWeight As SAPGradeWeight = CType(obj, SAPGradeWeight)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_DeleteStatement)


            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, sAPGradeWeight.ID)
            Return DbCommandWrapper

        End Function

        Protected Overrides Function GetInsertParameter(ByVal obj As Object, ByVal User As String) As Microsoft.Practices.EnterpriseLibrary.Data.DBCommandWrapper

            Dim sAPGradeWeight As SAPGradeWeight = CType(obj, SAPGradeWeight)
            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_InsertStatement)


            DbCommandWrapper.AddOutParameter("@ID", DbType.Int32, 0)
            DbCommandWrapper.AddInParameter("@Type", DbType.Int16, sAPGradeWeight.Type)
            DbCommandWrapper.AddInParameter("@Code", DbType.AnsiString, sAPGradeWeight.Code)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, sAPGradeWeight.Description)
            DbCommandWrapper.AddInParameter("@Bobot", DbType.Int16, sAPGradeWeight.Bobot)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, sAPGradeWeight.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, sAPGradeWeight.LastUpdateBy)
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

            Dim sAPGradeWeight As SAPGradeWeight = CType(obj, SAPGradeWeight)

            DbCommandWrapper = Db.GetStoredProcCommandWrapper(Me.m_UpdateStatement)

            DbCommandWrapper.AddInParameter("@ID", DbType.Int32, sAPGradeWeight.ID)
            DbCommandWrapper.AddInParameter("@Type", DbType.Int16, sAPGradeWeight.Type)
            DbCommandWrapper.AddInParameter("@Code", DbType.AnsiString, sAPGradeWeight.Code)
            DbCommandWrapper.AddInParameter("@Description", DbType.AnsiString, sAPGradeWeight.Description)
            DbCommandWrapper.AddInParameter("@Bobot", DbType.Int16, sAPGradeWeight.Bobot)
            DbCommandWrapper.AddInParameter("@RowStatus", DbType.Int16, sAPGradeWeight.RowStatus)
            DbCommandWrapper.AddInParameter("@CreatedBy", DbType.AnsiString, sAPGradeWeight.CreatedBy)
            'DbCommandWrapper.AddInParameter("@CreatedTime",DbType.DateTime,DateTime.Now)
            DbCommandWrapper.AddInParameter("@LastUpdateBy", DbType.AnsiString, User)
            'DbCommandWrapper.AddInParameter("@LastUpdateTime",DbType.DateTime,DateTime.Now)



            Return DbCommandWrapper

        End Function

#End Region


#Region "Private Methods"

        Private Function CreateObject(ByVal dr As IDataReader) As SAPGradeWeight

            Dim sAPGradeWeight As SAPGradeWeight = New SAPGradeWeight

            sAPGradeWeight.ID = CType(dr("ID"), Integer)
            If Not dr.IsDBNull(dr.GetOrdinal("Type")) Then sAPGradeWeight.Type = CType(dr("Type"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("Code")) Then sAPGradeWeight.Code = dr("Code").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Description")) Then sAPGradeWeight.Description = dr("Description").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("Bobot")) Then sAPGradeWeight.Bobot = CType(dr("Bobot"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("RowStatus")) Then sAPGradeWeight.RowStatus = CType(dr("RowStatus"), Short)
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedBy")) Then sAPGradeWeight.CreatedBy = dr("CreatedBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("CreatedTime")) Then sAPGradeWeight.CreatedTime = CType(dr("CreatedTime"), DateTime)
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateBy")) Then sAPGradeWeight.LastUpdateBy = dr("LastUpdateBy").ToString
            If Not dr.IsDBNull(dr.GetOrdinal("LastUpdateTime")) Then sAPGradeWeight.LastUpdateTime = CType(dr("LastUpdateTime"), DateTime)

            Return sAPGradeWeight

        End Function

        Private Sub SetTableName()

            If Not (GetType(SAPGradeWeight) Is Nothing) Then

                Dim attr As TableInfoAttribute = CType(Attribute.GetCustomAttribute(GetType(SAPGradeWeight), GetType(TableInfoAttribute)), TableInfoAttribute)

                If Not isnothing(attr) Then
                    m_TableName = attr.TableName
                Else
                    Throw New SearchException(GetType(SAPGradeWeight).ToString + " does not have TableInfoAttribute.")
                End If
            End If
        End Sub

#End Region

#Region "Custom Method"

#End Region

    End Class
End Namespace

