using System;
using System.Text;

namespace KTB.DNet.Lib
{
	/// <summary>
	/// Summary description for Bingo.
	/// </summary>
	public class Bingo
	{
		private string _delimiter=" ";
		private int _length = 0;
		private int _width = 0;
		private string _bingoDB = string.Empty;
		private string _bingoSMS = string.Empty;
		private int _lengthBingo = 2;
		
		public Bingo()
		{
			 
		}

		public string BingoDB
		{
			get {return this._bingoDB ;}
		}

		public string BingoSMS
		{
			get {return this._bingoSMS ;}
		}
		public Bingo(int length,int width)
		{
			_length=length;
 			_width =width; 
		}

		public int Width
		{
			get {return this._width ;}
			set {this._width =value;}
		}

		public int Length
		{
			get {return this._length ;}
			set {this._length =value;}
		}

		
		public void GenerateBingo()
		{
			StringBuilder bingoSMS = new StringBuilder(); 
			StringBuilder bingoDB = new StringBuilder(); 
			RandomGenerator generator = new RandomGenerator();
			for(int i=0;i<Length;i++)
			{
				for(int j=0;j<Width;j++)
				{
					bingoSMS.Append(generator.GetRandomCharacter(1));
   					bingoSMS.Append(generator.GetRandomNumeric(1));
					bingoDB.Append(generator.GetRandomCharacter(1));
					bingoDB.Append(generator.GetRandomNumeric(1));
					bingoDB.Append(_delimiter);
					if (j < Width - 1)
					{
						bingoSMS.Append(_delimiter);
					}
				}
				bingoSMS.Append(Environment.NewLine);  
			}
			_bingoSMS = bingoSMS.ToString();  
            _bingoDB  = bingoDB.ToString().Trim();   
		}
		 

		public string GetExistingBingo(int width,int length,string bingo)
		{
			string separator = " ";
			string[] _bingos = bingo.Split(separator.ToCharArray()); 
			StringBuilder bingoBuilder = new StringBuilder();
			for (int i =0;i< length;i++)
			{
				for (int j =0;j< width;j++)
				{
					bingoBuilder.Append(_bingos[j+(width*i)]); 
				}
				bingoBuilder.Append(Environment.NewLine);   
  			}
			return bingoBuilder.ToString(); 
		}

		public string GetBingoByIndex(int indexWidth,int indexLength,int width,int length,string bingo)
		{
			string separator = " ";
			string[] _bingos = bingo.Split(separator.ToCharArray());
			int index = ((indexLength-1) * width) + indexWidth;  
			return _bingos[index]; 
		}
	}
}
