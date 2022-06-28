#Region "Code Disclaimer"
'Copyright PT. Puspa Intimedia Internusa (Intimedia) 2005

'Intimedia grants you the right to use and modify the code in this Persistence Framework
'(code under the Framework Namespace) but 
'(i) only for the solutions that are developed by Intimedia for you 
'(ii) or in solutions that are developed in join development between you and Intimedia.

'All rights not expressly granted, are reserved.
#End Region

#Region "Summary"
'///////////////////////////////////////////////////////////////////////////////////////
'// Author Name: 
'// PURPOSE       : Enter summary here after generation.
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright © 2005 
'// ---------------------
'// $History      : $
'// Generated on 8/18/2005 - 12:01:00 PM
'//
'// ===========================================================================		
#End Region

#Region ".NET Base Class Namespace Imports"

Imports System
Imports System.Data
Imports System.Reflection
Imports System.Collections

#End Region

Namespace KTB.DNet.Utility

    Public Class ArrayListConverter

#Region "Private Declaration"

        Private _ArrayListData As ArrayList
        Private _PropertyInfos As PropertyInfo() = Nothing

#End Region

#Region "Constructor"

        Public Sub New(ByVal data As ArrayList)
            _ArrayListData = data
        End Sub

#End Region

#Region "Private property"

        Private Function GetPropertyInfos() As PropertyInfo()
            If _PropertyInfos Is Nothing Then
                _PropertyInfos = GetPropertyInfo()
            End If
            Return _PropertyInfos
        End Function

#End Region

#Region "Private Function"

        Private Function GetPropertyInfo() As PropertyInfo()
            If (_ArrayListData.Count > 0) Then
                Dim oEnumerator As IEnumerator = _ArrayListData.GetEnumerator()
                oEnumerator.MoveNext()
                Return oEnumerator.Current.GetType().GetProperties()
            End If
            Return Nothing
        End Function

        Private Function CreateDataTable() As DataTable
            Try
                Dim oDataTable As DataTable = New DataTable("GridDataTable")
                Dim _type As System.Type
                For Each oProperty As PropertyInfo In GetPropertyInfos()
                    _type = oProperty.PropertyType
                    If Not (_type.FullName.Equals("System.Collections.ArrayList")) Then
                        oDataTable.Columns.Add(oProperty.Name.ToString(), _type)
                    End If
                Next
                Return oDataTable
            Catch ex As Exception
                Return Nothing
            End Try
        End Function

        Private Function CreateDataSet() As DataSet
            Dim oDataSet As DataSet = New DataSet("GridDataSet")
            oDataSet.Tables.Add(FillDataTable())
            Return oDataSet
        End Function

        Private Function FillDataRow(ByVal oDataRow As DataRow, ByVal oData As Object)
            Dim _type As Type
            For Each oPropertyInfo As PropertyInfo In GetPropertyInfos()
                _type = oPropertyInfo.PropertyType
                If Not (_type.FullName.Equals("System.Collections.ArrayList")) Then
                    Try
                        oDataRow(oPropertyInfo.Name.ToString()) = oPropertyInfo.GetValue(oData, Nothing)
                    Catch ex As Exception
                        'oDataRow(oPropertyInfo.Name.ToString()) = Nothing
                    End Try
                End If
            Next
            Return oDataRow
        End Function

        Private Function FillDataTable() As DataTable
            Dim oArrayList As ArrayList = CType(_ArrayListData, ArrayList)
            Dim oEnumerator As IEnumerator = oArrayList.GetEnumerator
            Dim oDataTable As DataTable = CreateDataTable()
            While (oEnumerator.MoveNext)
                oDataTable.Rows.Add(FillDataRow(oDataTable.NewRow(), oEnumerator.Current))
            End While
            Return oDataTable
        End Function

#End Region

#Region "Public"

        Public Function ToDataSet() As DataSet
            Return CreateDataSet()
        End Function

        Public Function ToDataTable() As DataTable
            Return FillDataTable()
        End Function

#End Region

    End Class

End Namespace