using System;
using System.Collections; 
 
 

namespace KTB.DNet.Lib
{
	/// <summary>
	/// Summary description for RandomGenerator.
	/// </summary>
	public class RandomGenerator
	{
		private Random random ;
		private string[] listAbjad=new string[28];
		public RandomGenerator()
		{
			PopulateListCharacter();
			Random RandomGen = new Random();
			int _valRandom = RandomGen.Next(1,100000000);  
			int ts = _valRandom * (System.DateTime.Now.Day  + System.DateTime.Now.Hour + System.DateTime.Now.Minute + System.DateTime.Now.Second + System.DateTime.Now.Millisecond);  
			random = new Random(ts);
		}

		public string GetActivationCode(int length)
		{
			string result=string.Empty ;
			for(int i=0;i<length;i++)
			{
				result+=GetRandomCharacter(1); 
			}
			return result;
		}

		private void PopulateListCharacter()
		{
			listAbjad[0]="A";
			listAbjad[1]="B";
			listAbjad[2]="C";
			listAbjad[3]="D";
			listAbjad[4]="E";
			listAbjad[5]="F";
			listAbjad[6]="G";
			listAbjad[7]="H";
			listAbjad[8]="I";
			listAbjad[9]="Z";
			listAbjad[10]="K";
			listAbjad[11]="L";
			listAbjad[12]="M";
			listAbjad[13]="N";
			listAbjad[14]="X";
			listAbjad[15]="P";
			listAbjad[16]="Q";
			listAbjad[17]="R";
			listAbjad[18]="S";
			listAbjad[19]="T";
			listAbjad[20]="U";
			listAbjad[21]="V";
			listAbjad[22]="W";
			listAbjad[23]="X";
			listAbjad[24]="A";
			listAbjad[25]="Z";
		}

		public string GetRandomNumeric(int count)
		{
			string result=string.Empty;
			for(int i=0;i<count;i++)
			{
				result+=this.random.Next(0,10).ToString();
			}
			return result; 
		}

		public string GetRandomNumeric(int count,int min, int max,bool separator)
		{
			string result=string.Empty;
			Hashtable ht = new Hashtable(count); 
			int x=0;
			for(int i=0;i<count;i++)
			{
				x= this.random.Next(min,max+1);
				while (ht.Contains(x)) 
				{
					x= this.random.Next(min,max+1);
				}
				
				ht.Add(x,""); 
				
				if (separator)
				{
					result+=x.ToString()+";";
				}
				else
				{
					result+=x.ToString();
				}
			}
			return result.Trim(";".ToCharArray());
		}

		public string GetRandomCharacter(int count)
		{
			string result=string.Empty;
			int selectedIndex;
			for(int i=0;i<count;i++)
			{
				selectedIndex = this.random.Next(0,25);
				result+=listAbjad[selectedIndex].ToLower(); 
			}
			return result; 
		}

		public string GenarateRandom(int length)
		{
			string result =string.Empty; 
			for(int i=0;i<length;i++)
			{
				int  isAlphabet = random.Next(2);
				if (isAlphabet ==0)
				{
					result += GetRandomCharacter(1); 
				}
				else
				{
					result += GetRandomNumeric(1);
				}
			}
			return result; 
		}

		public string GenarateRandomCombination()
		{
			string result =string.Empty; 
			result += GetRandomCharacter(1); 
			result += GetRandomNumeric(1);
			return result; 
		}

		public string GenarateRandomNumericOnly(int length)
		{
			string result =string.Empty; 
			for(int i=0;i<length;i++)
			{
					result += GetRandomNumeric(1);
			}
			return result; 
		}

		public string GenarateRandomCharacterOnly(int length)
		{
			string result =string.Empty; 
			for(int i=0;i<length;i++)
			{
				result += GetRandomCharacter(1);
			}
			return result; 
		}


	}
}
