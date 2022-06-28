using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace KTB.DNet.BusinessValidation
{
    public class SalesOrderInterestValidation
    {
          #region Constructor
        public SalesOrderInterestValidation()
        {
        }
        #endregion

        public  class PPHWitholdingForm
        {
            private static string _strPDF = "";
            public PPHWitholdingForm(string strPDF)
            {
                _strPDF = strPDF;
                _strPDF = _strPDF.Replace("/", " ");
                ParseResult = "";
                DoProcess();
            }

            public bool IsValid
            {
                get;
                set;
            }

            public string ParseResult
            {
                get;
                set;
            }

            #region "Fungsi"

            private void DoProcess()
            {
                try
                {
                    string _DN, _ND, _TD, _NP, _NPN, _TP, _NT;
                    string _DPP, _PPH;
                    NoBuktiPotong = _strPDF.Substring(_strPDF.ToUpper().IndexOf("KODE POS : TANGGAL : :") + 23).Split(' ')[0];
                    NPWPTerpotong = _strPDF.Substring(_strPDF.ToUpper().IndexOf("KODE POS : TANGGAL : :") + 23).Split(' ')[1];
                    MasaPajak = _strPDF.Substring(_strPDF.ToUpper().IndexOf("ADM. JAKARTA DKI JAKARTA 13210") + 30).Split(' ')[1];

                    GetAmountTax("DKI JAKARTA 13210", out _DPP, out _PPH);
                    TotalPDPAmount = GetNumeric(_DPP);
                    TotalPPHAmount = GetNumeric(_PPH);



                    GetDocInfo(_strPDF, out _DN, out _ND, out _TD, out _NP, out _NPN, out _TP, out _NT);
                    DocumentName = _DN;
                    DocumentNumber = _ND;
                    DocumentDate = GetDate(_TD);
                    NPWPPemotong = _NP;
                    NamaWajibPajak = _NPN;
                    TanggalPemotongan = GetDate(_TP);
                    NamaPendatangan = _NT;



                    IsValid = true;
                }
                catch (Exception ex)
                {
                    IsValid = false;
                    ParseResult = ex.Message.ToString();
                    throw;
                }

            }

            private static void GetAmountTax(string strCont, out string DPP, out string PPH)
            {
                DPP = "0"; PPH = "0";

                var tt = _strPDF.Substring(_strPDF.ToUpper().IndexOf(strCont) + strCont.Length).Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                //DPP
                bool _end = false;
                int i = 1;
                while (!_end)
                {

                    string tmp = tt[i];



                    if (tmp.Trim() == "24-102-01" && tt[i + 1] != "0" && tt[i + 2] != "15")
                    {
                        DPP = tt[i + 1].ToString().Trim();
                        _end = true;
                    }


                    if (i > 5)
                    {
                        _end = true;
                    }
                    i++;
                }


                //pph
                _end = false;
                i = 1;
                while (!_end)
                {

                    string tmp = tt[i];



                    if (tt[1].Trim() == "24-102-01" && tt[i] == "15")
                    {
                        PPH = tt[i + 1].ToString().Trim();
                        _end = true;
                    }


                    if (i > 6)
                    {
                        _end = true;
                    }
                    i++;
                }

            }

            private static void GetDocInfo(string _strPDF, out string DocName, out string DocNo, out string DocDate, out string npw, out string npn, out string TP, out string NT)
            {
                string tempPPH = _strPDF.Substring(_strPDF.ToUpper().IndexOf("ADM. JAKARTA DKI JAKARTA 13210") + 30).Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[5];
                DocNo = ""; DocDate = "";
                DocName = "";


                bool _end = false;
                int idxDoc = _strPDF.ToUpper().IndexOf(tempPPH);
                string nn = _strPDF.Substring(idxDoc);
                int i = 1;
                var ar = _strPDF.Substring(_strPDF.ToUpper().IndexOf(tempPPH) + tempPPH.Length).Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                while (!_end)
                {

                    string tmp = _strPDF.Substring(_strPDF.ToUpper().IndexOf(tempPPH) + tempPPH.Length).Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[i];

                    if (IsDate(tmp))
                    {
                        DocDate = tmp;
                        DocNo = ar[i - 1];

                        for (int x = 0; x < i; x++)
                        {

                            string tmp2 = ar[x];

                            if (tmp2 != ar[i - 1])
                            {
                                DocName = DocName + " " + tmp2;
                            }
                        }





                        _end = true;
                    }


                    if (i > 10)
                    {
                        _end = true;
                    }
                    i++;
                }



                //npwp
                _end = false;
                npw = string.Empty;
                i = 1;
                var _strTemp = _strPDF.Substring(_strPDF.ToUpper().IndexOf(tempPPH) + tempPPH.Length + DocName.Length).Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                while (!_end)
                {

                    string tmp = _strTemp[i];


                    if (IsDate(tmp) && i >= 1)
                    {
                        npw = _strTemp[i + 1].ToString();
                        _end = true;
                    }


                    if (i > 10)
                    {
                        _end = true;
                    }
                    i++;
                }


                npn = string.Empty;
                _end = false;

                i = 1;
                _strTemp = _strPDF.Substring(_strPDF.ToUpper().IndexOf(npw.ToUpper()) + npw.Length).Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                while (!_end)
                {

                    string tmp = _strTemp[i];


                    if (IsDate(tmp))
                    {
                        int y = 0;
                        while (y < i)
                        {
                            npn = npn + " " + _strTemp[y].ToString();
                            y++;
                        }

                        _end = true;
                    }


                    if (i > 10)
                    {
                        _end = true;
                    }
                    i++;
                }

                //tanggal potong
                TP = string.Empty;
                _end = false;

                i = 1;
                string ln = "";
                using (var reader = new StringReader(_strPDF.Substring(_strPDF.ToUpper().IndexOf(npw.ToUpper()) + npw.Length)))
                {
                    ln = reader.ReadLine();
                }
                _strTemp = ln.Substring(ln.ToUpper().IndexOf(npw.ToUpper()) + npw.Length).Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                while (!_end)
                {

                    string tmp = _strTemp[i];


                    if (IsDate(tmp))
                    {
                        TP = tmp;

                        _end = true;
                    }


                    if (i > 10)
                    {
                        _end = true;
                    }
                    i++;
                }


                //nama tanda tangan
                NT = string.Empty;
                _end = false;

                i = 1;
                ln = "";
                using (var reader = new StringReader(_strPDF.Substring(_strPDF.ToUpper().IndexOf(npw.ToUpper()) + npw.Length)))
                {
                    ln = reader.ReadLine();
                }
                _strTemp = ln.Substring(ln.ToUpper().IndexOf(npw.ToUpper()) + npw.Length).Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                while (!_end)
                {

                    string tmp = _strTemp[i];


                    if (IsDate(tmp))
                    {
                        int y = i + 1;
                        while (y < _strTemp.Length)
                        {
                            NT = NT + " " + _strTemp[y].ToString();
                            y++;
                        }

                        _end = true;
                    }


                    if (i > 10)
                    {
                        _end = true;
                    }
                    i++;
                }

            }
            private static double GetNumeric(string val)
            {

                string str = val.Trim();
                if (str.Substring(val.Length - 3, 3) == ",00")
                {
                    str = str.Substring(0, str.Length - 3);
                }
                else if (str.Substring(val.Length - 3, 3) == ".00")
                {
                    str = str.Substring(0, str.Length - 3);
                }

                char ch1 = ',';
                char ch2 = '.';

                int freq1 = str.Count(f => (f == ch1));
                int freq2 = str.Count(f => (f == ch2));
                if (freq1 > freq2)
                {
                    str = str.Replace(",", "");
                }
                else
                {
                    str = str.Replace(".", "");
                }
                return Convert.ToDouble(str);

            }

            private static bool IsNumeric(string val)
            {
                Regex reNum = new Regex(@"^\d+$");
                return reNum.Match(val).Success;
            }

            private static bool IsDate(string val)
            {

                try
                {
                    string tStr = val.Trim();
                    if (tStr.Split('-').Length == 3)
                    {
                        if (IsNumeric(tStr.Split('-')[0]) && IsNumeric(tStr.Split('-')[1]) && IsNumeric(tStr.Split('-')[2]))
                        {
                            int dd = 0;
                            int mm = 0;
                            int yy = 0;
                            int.TryParse(tStr.Split('-')[0], out dd);
                            int.TryParse(tStr.Split('-')[1], out mm);
                            int.TryParse(tStr.Split('-')[2], out yy);
                            if (dd <= 31 && dd >= 1 && mm <= 12 && mm >= 1 & yy > 1900)
                            {

                                return true;


                            }
                        }

                    }
                    else if (tStr.Split('/').Length == 3)
                    {
                        if (IsNumeric(tStr.Split('/')[0]) && IsNumeric(tStr.Split('/')[1]) && IsNumeric(tStr.Split('/')[2]))
                        {
                            int dd = 0;
                            int mm = 0;
                            int yy = 0;
                            int.TryParse(tStr.Split('/')[0], out dd);
                            int.TryParse(tStr.Split('/')[1], out mm);
                            int.TryParse(tStr.Split('/')[2], out yy);
                            if (dd <= 31 && dd >= 1 && mm <= 12 && mm >= 1 & yy > 1900)
                            {

                                return true;


                            }
                        }

                    }
                    {


                    }
                }
                catch (Exception)
                {

                    return false;
                }

                return false;
            }

            private static DateTime GetDate(string val)
            {

                DateTime result = new DateTime(1900, 1, 1);
                try
                {
                    string tStr = val.Trim();
                    if (tStr.Split('-').Length == 3)
                    {
                        if (IsNumeric(tStr.Split('-')[0]) && IsNumeric(tStr.Split('-')[1]) && IsNumeric(tStr.Split('-')[2]))
                        {
                            int dd = 0;
                            int mm = 0;
                            int yy = 0;
                            int.TryParse(tStr.Split('-')[0], out dd);
                            int.TryParse(tStr.Split('-')[1], out mm);
                            int.TryParse(tStr.Split('-')[2], out yy);
                            if (dd <= 31 && dd >= 1 && mm <= 12 && mm >= 1 & yy > 1900)
                            {

                                result = new DateTime(yy, mm, dd);
                                return result;

                            }
                        }

                    }
                    else if (tStr.Split('/').Length == 3)
                    {
                        if (IsNumeric(tStr.Split('/')[0]) && IsNumeric(tStr.Split('/')[1]) && IsNumeric(tStr.Split('/')[2]))
                        {
                            int dd = 0;
                            int mm = 0;
                            int yy = 0;
                            int.TryParse(tStr.Split('/')[0], out dd);
                            int.TryParse(tStr.Split('/')[1], out mm);
                            int.TryParse(tStr.Split('/')[2], out yy);
                            if (dd <= 31 && dd >= 1 && mm <= 12 && mm >= 1 & yy > 1900)
                            {
                                result = new DateTime(yy, mm, dd);
                                return result;


                            }
                        }

                    }
                    {


                    }
                }
                catch (Exception)
                {

                    return result;
                }

                return result;
            }


            #endregion

            public string NoBuktiPotong
            {
                get;
                set;
            }

            public string NPWPTerpotong
            {
                get;
                set;
            }

            public string MasaPajak
            {
                get;
                set;
            }

            public double TotalPDPAmount
            {
                get;
                set;
            }

            public double TotalPPHAmount
            {
                get;
                set;
            }

            public string DocumentName
            {
                get;
                set;
            }

            public string DocumentNumber
            {
                get;
                set;
            }

            public DateTime DocumentDate
            {
                get;
                set;
            }

            public string NPWPPemotong
            {
                get;
                set;
            }

            public string NamaWajibPajak
            {
                get;
                set;
            }

            public DateTime TanggalPemotongan
            {
                get;
                set;
            }

            public string NamaPendatangan
            {
                get;
                set;
            }

        }
    }
}
