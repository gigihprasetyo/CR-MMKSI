using System.Collections;
using System.Data;
using KTB.DNet.Domain;
using System;

namespace KTB.DNet.NewSAPProxy
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	public class SAPDNet
	{
		#region "Default"
		private string _ConStr;	
		public SAPDNet(string ConnectionString)
		{
			//
			// TODO: Add constructor logic here
			//
			_ConStr = ConnectionString;
		}

		public SAPDNet()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public string SAPConnectionString
		{
			get
			{
				return _ConStr;
			}
			set
			{
				_ConStr = value;
			}
		}

	
		#endregion

		#region "CekMaterial"
		public DataTable ValidateCassisnumberList(DataTable CassisnumberList)
		{

			NewSAPProxy oSAPComponent = new NewSAPProxy(SAPConnectionString);
			//TODO:Put Code to Call SAP RFC in this section

			return CassisnumberList;
		}

		public bool ValidateCassisnumber(string CassisNumber)
		{
			NewSAPProxy oSAPComponent = new NewSAPProxy(SAPConnectionString);
			//TODO:Put Code to Call SAP RFC in this section

			return true;
		}

		public ArrayList CheckMaterial( ArrayList arlSparepartList)
		{
			//bool bRerrasult =false;
			ArrayList arlDTReturn = new ArrayList();
			try
			{
				NewSAPProxy oSAPComponent = new NewSAPProxy(SAPConnectionString);
				//TODO:Put Code to Call SAP RFC in this section
				ZSPST0028_01Table oMainpartTable = new ZSPST0028_01Table();
				ZSPST0028_02Table oSubtitutionPartTable = new ZSPST0028_02Table();
				ZSPST0028_01 oMainPart;
				//Add to Table Parameter
				for (int i=0; i<arlSparepartList.Count;i++)
				{
					oMainPart = new ZSPST0028_01();
					oMainPart.Matnr = (string) ((SparePartMaster)arlSparepartList[i]).PartNumber;
					oMainPart.Rqqty = ((SparePartMaster)arlSparepartList[i]).MaxStock.ToString() ;
					oMainpartTable.Add(oMainPart);
				}
				oSAPComponent.Connection.Open();
				oSAPComponent.Zrfc_Check_Material("", ref oMainpartTable,ref oSubtitutionPartTable);
				oSAPComponent.Connection.Close();
				//arlDTReturn.Add(oMainpartTable.ToADODataTable());
				//arlDTReturn.Add(oSubtitutionPartTable.ToADODataTable());
				arlDTReturn.Add(oMainpartTable);
				arlDTReturn.Add(oSubtitutionPartTable);
				//bResult=true;
			}
			catch (SAP.Connector.RfcException exRFCError)
			{
				throw new Exception (" RFC Error" + exRFCError.Message);
			}
			return arlDTReturn;
		}

		#endregion

		
		#region "MaterialPrice"

		public ArrayList GetMaterialPrice( ArrayList arlEED)
		{
			ArrayList arlDTReturn = new ArrayList();
			try
			{
				MaterialPrice objMP = new MaterialPrice(SAPConnectionString);
				ZKTB_INQ_INTable objIIT = new ZKTB_INQ_INTable();
				ZKTB_INQ_OUTTable objIOT = new ZKTB_INQ_OUTTable();
				ZKTB_INQ_IN objII;
				
				
				for (int i=0; i<arlEED.Count;i++)
				{
					objII = new ZKTB_INQ_IN();
					objII.Customer = (string) ((EstimationEquipDetail)arlEED[i]).EstimationEquipHeader.Dealer.DealerCode ;
					objII.Material = ((EstimationEquipDetail)arlEED[i]).SparePartMaster.PartNumber.ToString() ;
					objIIT.Add(objII);
				}
				objMP.Connection.Open();
				objMP.ZKTB_DNET_INQUIRY( "" , ref objIIT, ref objIOT);
				//objMP.Zktb_Dnet_Inquiry (  ref objIIT, ref objIOT);
				objMP.Connection.Close();
				//arlDTReturn.Add(oMainpartTable.ToADODataTable());
				//arlDTReturn.Add(oSubtitutionPartTable.ToADODataTable());
				arlDTReturn.Add(objIIT);
				arlDTReturn.Add(objIOT);
				//bResult=true;
			}
			catch (SAP.Connector.RfcException exRFCError)
			{
				throw new Exception (" RFC Error" + exRFCError.Message);
			}
			return arlDTReturn;
		}
		

		#endregion



		#region "CreditControll"

		public ArrayList GetCreditControl(ArrayList  sapCreditControlList)
		{
			ArrayList list = new ArrayList(); 
			NewSAPProxy oSAPComponent = new NewSAPProxy(SAPConnectionString);
			ZFUST0042Table creditControlTable = new ZFUST0042Table();
			ZFUST0042 oCreditControl;
			try
			{
				foreach (SAPCreditCeiling  item in sapCreditControlList) 
				{
					oCreditControl = new ZFUST0042();
					oCreditControl.Knkli = item.DealerCode.ToString();
					oCreditControl.Splnm  = item.SPLNumber.ToString();
					oCreditControl.Klyear  = item.PeriodYear.ToString();
					oCreditControl.Klmonth  = item.PeriodMonth.ToString();
					creditControlTable.Add(oCreditControl);
				}
				oSAPComponent.Connection.Open();
				oSAPComponent.Zrfc_Credit_Ceiling(ref creditControlTable);
				oSAPComponent.Connection.Close();
				list.Add( creditControlTable);
			}
			catch(SAP.Connector.RfcException exRFCError)
			{
				throw new Exception (" RFC Error" + exRFCError.Message);
			}
			finally
			{
				if (oSAPComponent != null)
				{
					oSAPComponent =null;
				}
			}
			
			return list;
		}

		#endregion
		
	}
}
