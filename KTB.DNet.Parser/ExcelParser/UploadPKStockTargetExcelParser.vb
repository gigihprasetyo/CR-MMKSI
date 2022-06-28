#Region "Summary"
'// ===========================================================================		
'// Author Name   : Afa
'// PURPOSE       : 
'// SPECIAL NOTES : 
'// ---------------------
'// Copyright © 2005 
'// ---------------------
'// $History      : $
'// Generated on 14/10/2005
'// ===========================================================================		
#End Region

#Region ".NET Base Class Namespace Imports"
'Imports System.Data.Odbc
Imports Excel
Imports System.Security.Principal
Imports System.Linq
#End Region

#Region "Custom Namespace"
Imports KTB.DNet.Domain
Imports KTB.DNet.Domain.Search
Imports KTB.DNet.BusinessFacade.SparePart
Imports System.IO
Imports KTB.DNet.BusinessFacade.General
Imports KTB.DNet.BusinessFacade.FinishUnit

#End Region

Namespace KTB.DNet.Parser
    Public Class UploadPKStockTargetExcelParser
        Inherits AbstractExcelParser

#Region "Protected Methods"

        Protected Overrides Function ParsingExcelNoTransaction(ByVal fileName As String, ByVal sheetName As String, ByVal user As String) As Object
            DataCollection = New ArrayList
            Dim parts() As String = fileName.Split(".".ToCharArray())
            Dim ext As String = parts(parts.Length - 1).Trim().ToLower()
            Dim objReader As IExcelDataReader = Nothing

            Dim DealerCode As String        '-- Part number
            Dim companyCode As String = KTB.DNet.Lib.WebConfig.GetValue("CompanyCode")

            Try

                Using stream As FileStream = File.Open(fileName, FileMode.Open, FileAccess.Read)
                    Dim Row As Integer = 0
                    If (ext = "xls") Then
                        objReader = ExcelReaderFactory.CreateBinaryReader(stream)
                    ElseIf (ext = "xlsx") Then
                        objReader = ExcelReaderFactory.CreateOpenXmlReader(stream)
                    End If
                    objReader.IsFirstRowAsColumnNames = True


                    If (Not IsNothing(objReader)) Then
                        Dim wsmUser = New System.Security.Principal.GenericPrincipal(New GenericIdentity("WSM"), Nothing)
                        While objReader.Read()

                            '-- In case of failed reading subsequent sparepart
                            '-- simply return already successfully read spareparts
                            If (Row = 0 AndAlso objReader.IsFirstRowAsColumnNames) Then
                                Row = Row + 1
                                Continue While

                            End If
                            Try
                                DealerCode = objReader.GetString(0)  '-- Dealer code
                                If String.IsNullOrEmpty(DealerCode) Then
                                    DealerCode = ""
                                End If
                                DealerCode = DealerCode.Trim()  '-- Always trim spaces

                            Catch ex As Exception
                                'If objConn.State = ConnectionState.Open Then
                                '    objConn.Close()  '-- Close connection if already open
                                'End If

                                Return DataCollection
                            End Try
                            Row = Row + 1


                            Dim _stockTargetError As String = "" '--Stock TargetError


                            '--get values from other cols
                            Dim Model As VechileModel = New VechileModel
                            Model.Description = ""
                            Model.Category = New Category
                            Model.MarkLoaded()
                            Model.ID = -1
                            Dim ValidFrom As Date
                            Dim Target As Integer
                            Dim Ratio As Decimal
                            Dim BlockDealer As Boolean
                            Dim BlockKTB As Boolean
                            Try
                                Dim modelName As String = objReader.GetString(1)  '-- Model Name
                                modelName = modelName.Trim()  '-- Always trim spaces
                                Dim modelcriterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.VechileModel), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                                If Not String.IsNullOrEmpty(modelName) Then
                                    modelcriterias.opAnd(New Criteria(GetType(VechileModel), "Description", MatchType.Exact, modelName))
                                    Model = New VechileModelFacade(wsmUser).Retrieve(modelcriterias).Item(0)
                                    If (Model Is Nothing OrElse Model.ID < 0 OrElse String.IsNullOrEmpty(Model.Description)) Then
                                        Throw New Exception
                                    Else
                                        If Model.Category.ProductCategory.Code.Trim <> companyCode Then
                                            _stockTargetError &= "Model description tidak ada pada Kategori Produk;"
                                        End If
                                    End If

                                Else
                                    _stockTargetError &= "Model tidak valid;"
                                End If

                            Catch ex As Exception
                                Model = New VechileModel
                                Model.Description = ""
                                Model.ID = -1
                                Model.Category = New Category
                                Model.MarkLoaded()



                                _stockTargetError &= "Model tidak valid;"
                            End Try
                            Try
                                ValidFrom = Date.ParseExact(objReader.GetString(2).Trim(), "yyyyMMdd", Nothing)

                            Catch ex As Exception
                                _stockTargetError &= "Valid From tidak valid;"
                            End Try
                            Try
                                Target = objReader.GetInt32(3)  '-- Target

                            Catch ex As Exception
                                _stockTargetError &= "Target tidak valid;"
                            End Try
                            Try
                                Ratio = CType(objReader.GetString(4).Trim().Replace(".", ","), Decimal)  '-- Ratio

                            Catch ex As Exception
                                _stockTargetError &= "Ratio tidak valid;"
                            End Try
                            Try
                                Dim sBlockDealer As String = objReader.GetString(5)  '-- BlockDealer
                                sBlockDealer = sBlockDealer.Trim()  '-- Always trim spaces
                                If sBlockDealer.Equals("Y") Then
                                    BlockDealer = True
                                ElseIf sBlockDealer.Equals("N") Then
                                    BlockDealer = False
                                Else
                                    _stockTargetError &= "BlockDealer tidak valid;"
                                End If

                            Catch ex As Exception
                                _stockTargetError &= "BlockDealer tidak valid;"
                            End Try

                            Try
                                Dim sBlockKTB As String = objReader.GetString(6)  '-- BlockKTB
                                sBlockKTB = sBlockKTB.Trim()  '-- Always trim spaces
                                If sBlockKTB.Equals("Y") Then
                                    BlockKTB = True
                                ElseIf sBlockKTB.Equals("N") Then
                                    BlockKTB = False
                                Else
                                    _stockTargetError &= "BlockKTB tidak valid;"
                                End If

                            Catch ex As Exception
                                _stockTargetError &= "BlockKTB tidak valid;"
                            End Try


                            '--Retrieve dealer
                            Dim criterias As New CriteriaComposite(New Criteria(GetType(KTB.DNet.Domain.Dealer), "RowStatus", MatchType.Exact, CType(DBRowStatus.Active, Short)))
                            If Not String.IsNullOrEmpty(DealerCode) Then
                                criterias.opAnd(New Criteria(GetType(Dealer), "DealerCode", MatchType.Exact, DealerCode))

                            End If

                            criterias.opAnd(New Criteria(GetType(Dealer), "ID", MatchType.No, 1))
                            criterias.opAnd(New Criteria(GetType(Dealer), "Title", MatchType.Exact, "0"))
                            criterias.opAnd(New Criteria(GetType(Dealer), "Status", MatchType.Exact, "1"))

                            Dim listDealer As ArrayList = New DealerFacade(wsmUser).Retrieve(criterias)
                            If listDealer.Count <= 0 Then

                                _stockTargetError &= "Kode dealer tidak ditemukan;"
                                Dim dealer As New Dealer
                                dealer.DealerCode = ""
                                dealer.MarkLoaded()
                                listDealer.Add(dealer)
                            End If

                            'Allow multiple/redundant data in a file, 
                            'lower data more valid data, means. lower row will update the upper row
                            'it will 

                            'If listDealer.Count = 1 Then
                            '    '--if it's a single row remove duplicate stocktarget from default if any
                            '    Dim _dealer As Dealer = listDealer.Item(0)
                            '    Dim query As Generic.List(Of StockTarget) = (From _stockTarget As StockTarget In DataCollection
                            '        Where _stockTarget.Dealer.DealerCode.Equals(_dealer.DealerCode) AndAlso _stockTarget.VechileModel.Description.Equals(Model.Description) AndAlso _stockTarget.ValidFrom.Equals(ValidFrom)
                            '        Select _stockTarget).ToList

                            '    For Each _stockTarget As StockTarget In query
                            '        DataCollection.Remove(_stockTarget)
                            '    Next
                            'End If

                            If listDealer.Count >= 1 Then '--handle all dealers
                                For Each dealer As Dealer In listDealer

                                    Dim _stockTarget As New StockTarget
                                    _stockTarget.ErrorMessage &= _stockTargetError

                                    _stockTarget.Dealer = dealer
                                    _stockTarget.VechileModel = Model
                                    _stockTarget.ValidFrom = ValidFrom
                                    _stockTarget.Target = Target
                                    _stockTarget.TargetRatio = Ratio
                                    _stockTarget.IsDealerBlock = BlockDealer
                                    _stockTarget.IsKTBBlock = BlockKTB
                                    DataCollection.Add(_stockTarget)
                                Next
                            End If




                        End While




                    End If

                End Using



            Catch ex As Exception
                Dim str As String = ex.Message
                Return Nothing

            Finally
                'If objConn.State = ConnectionState.Open Then
                '    objConn.Close()  '-- Close connection if already open
                'End If
            End Try

            Return DataCollection  '-- Return list of sparepart PO details
        End Function


#End Region

    End Class

End Namespace