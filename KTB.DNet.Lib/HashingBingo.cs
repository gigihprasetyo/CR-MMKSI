using System;
using System.Text;
using System.Collections;

namespace KTB.DNet.Lib
{
	/// <summary>
	/// Summary description for Bingo.
	/// </summary>
	/// 
	
	public class HashingBingo
	{ 
		private int _x = 0;
		private int _y = 0;
		private string _bingoSMS = string.Empty;
		private ArrayList _bingoDB = new ArrayList();
		private string _bingoEmail = string.Empty; 
		private string[] _bingo;
		private int _digit = 2;
		private string _delimiterSMS =" ";

		public HashingBingo()
		{
			 
		}

	
		public HashingBingo(int x,int y,int digit)
		{
			_x = x;
			_y = y; 
			_digit = digit;
		}

		
	 	public string BingoSMS
		{
			get {return this._bingoSMS ;}
		}
		
		public string BingoEmail
		{
			get {return this._bingoEmail ;}
		}

		public int X
		{
			get {return this._x ;}
			set {this._x =value;}
		}

		public int Y
		{
			get {return this._y ;}
			set {this._y =value;}
		}

		public int Digit
		{
			get {return this._digit ;}
			set {this._digit =value;}
		}

		public string DelimiterSMS
		{
			get {return this._delimiterSMS ;}
			set {this._delimiterSMS =value;}
		}

		public ArrayList Bingo
		{
			get {return this._bingoDB ;}
			set {this._bingoDB =value;}
		}


		private string[] GeneratRandom(int digit,RandomGenerator rnd)
		{
		
			string[] result = new string[2];
			string rndString = rnd.GenarateRandomCombination().ToUpper(); 
			string enc = DNetEncryption.ComputeHash(rndString, "SHA512", null);
			result[0] = rndString;
			result[1] = enc;
			return result;
		}
		
		public void GenerateBingo()
		{
			StringBuilder sms = new StringBuilder(); 
			StringBuilder email = new StringBuilder();
			email.Append("<Table border=0>"); 
			RandomGenerator rnd = new RandomGenerator();
			for (int y=1;y<=_y;y++)
			{
				string line = "#" + y + DelimiterSMS;
				for (int x=1;x<=_x;x++)
				{
					string[] ran = GeneratRandom(_digit,rnd);
					_bingo = new string[3];
					_bingo[0] = y.ToString();
					_bingo[1] = x.ToString();  
					_bingo[2] = ran[1];
					
					line += ran[0].ToUpper();
					line +=DelimiterSMS;
					Bingo.Add(_bingo); 
				}
				line = line.Trim(DelimiterSMS.ToCharArray());
				sms.Append(line);
				sms.Append(Environment.NewLine );
				email.Append("<TR><TD>"); 
				email.Append(line);
				email.Append("</TD></TR>");
			}
			email.Append("</Table border=0>"); 
			
			_bingoDB = Bingo;	
			_bingoSMS = sms.ToString();
			_bingoEmail = email.ToString();	
		}
	 	
	}
}

