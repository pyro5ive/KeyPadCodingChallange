/// keypad coding test
/// 9/2021
// steve maroney

/////////////////////////////////
/// Main()
int Main(string[] args)
{
  int string_counts; 
  //List<string> keys = new List<string> { "LAP", "RHO", "IVQW", "SKJ", "TZU", "MDX", "NGYC", "BEF" };
  //List<string> keys = new List<string> { "OVK", "IPD", "UBZW", "QCA", "MJY", "GFTN", "XRL", "SEH" };
  //List<string> keys = new List<string> { "WPL", "HTVG", "QIN", "YSMX", "KAF", "OCJ", "UZR", "DEB"};
  //List<string> keys = new List<string> { "MGJ", "YIZ", "DKS", "BHP", "VENA", "FLQ", "URT", "CWOX"};
  //string message = "HEY";
  //string message = "OCWQQDH";
  //string message = "LEARN IT";
    string code; 

        
   code = FindCode(message, keys);  // find code. 
   string_counts = FindPossibles(code, keys); // find possible string counts.


    int count = string_counts % 1000000007;
    Console.WriteLine("String Combos: " + string_counts);
    Console.WriteLine("Mod: " + count);

    return count;

}
////// END OF Main()


/////// FINDCODE() /////////////
string FindCode(string message, List<string> keys)
{
	string code = "";
	int charpos;
	int this_button;

	// find char in button
	// determine button length
	foreach (char alpha in message)
	{
		// if char is space
		if (alpha == ' ')
		{
			code += '1';
		}		
		else
		{
			this_button = 2;
			foreach (string key in keys)
			{
				charpos = key.IndexOf(alpha);
				if (charpos > -1)
				{
					//charpos += 1; // offset index
					while (charpos >= 0)
					{
						code += this_button;
						charpos -= 1;
					}  
				}
				this_button += 1;
			}
		}	
	}

     ///// debug output //////////////////////
     Console.WriteLine("message: " + message);
     Console.WriteLine("Code:" + code);
     Console.WriteLine("Keys: ");
     foreach (string key in keys)
     Console.WriteLine(' ' + key);
     Console.WriteLine("---------------------------------------");
     //////////////////////////////////////////
           
     return code; 
 }
//////// END OF FindCode()


////////////////////////////////////////////////////////////////////////////////////
/// possible strings by code
// number of combinations per digit depends on repeat_count and key_length. 
// 1 digits = 1 possible
// 5
/////////
// 2 digits = 2 possible
// 55
// 5,5
/////////
// 3 digits = 4 possibles
// 555
// 5,5,5
// 5,55
// 55,5
/////////
// 4 digits = 5 possbile strings
// 5555
// 5,5,5,5
// 5,555
// 55,55
// 555,5
////////////////////
static int FindPossibles(string code, List<string> keys)
{
		//declare
    string this_code_substring, digit_repeat_string;
    string[] codes;
    int substring_start_index, key_length;
    int string_counts;
    int key_offset, key_index, ascii_offset; 
    int[] times = { 0, 1, 2, 4, 5 };
	
		// init 
    key_offset = 2;
    string_counts = 1; 
    ascii_offset = 48;
    // super important, split code by '1' into array. '1' = space. 
    codes = code.Split('1');
    // debug
    Console.WriteLine("size of code(s) array:" + codes.Length);

		/// loop through each_code in codes[]
    for (int i = 0; i < codes.Length; i++)
    {
				string this_code = codes[i];
        substring_start_index = 0;
        ///////////////////////////////////////////////////////////////////////////////
        foreach (char digit in this_code)
				{
						key_index = (digit - ascii_offset) - key_offset; // '2' is first key. minus offset = 0. so '2' is "LAP"
            key_length = keys[key_index].Length;  // key length using the index

						// loop to find repeating digits, only find max of key_length digits
            for (int digit_repeat_loop = key_length; digit_repeat_loop > 0; digit_repeat_loop--)
						{
								if ((substring_start_index + digit_repeat_loop) > this_code.Length) { continue; }  // dont run off the end of code.. skip this loop
					
								// reset string back to null 
								digit_repeat_string = "";
								// build repeat variable based on digit_repeat_loop for comparision                 
								while (digit_repeat_string.Length < digit_repeat_loop)
                {
                			digit_repeat_string += digit;
                 }

                 // compare substring
                 this_code_substring = this_code.Substring(substring_start_index, digit_repeat_loop);
                 if (Equals(this_code_substring, digit_repeat_string))
                 {
                            string_counts *= times[digit_repeat_loop];
                            substring_start_index += digit_repeat_loop;
                 }
            }
		
			}
			/////////////////////////////////////////////////////////////////
   }
	
  return string_counts; 
}
/////// END OF FindPossibles()
