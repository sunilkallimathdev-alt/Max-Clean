using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.CommonClasses
{
    public class Base64Encode_Decode
    {
        public static string base64Encode(string PlainString)
        {
            if (!string.IsNullOrEmpty(PlainString))
            {
                try
                {
                    byte[] encData_byte = new byte[PlainString.Length];
                    encData_byte = System.Text.Encoding.UTF8.GetBytes(PlainString);
                    string encodedData = Convert.ToBase64String(encData_byte);
                    return encodedData;
                }
                catch (Exception e)
                {
                    throw new Exception("Error in base64Encode" + e.Message);
                }
            }
            else { return ""; }
        }

        public static string base64Decode2(string EncodedString)
        {
            if (!string.IsNullOrEmpty(EncodedString))
            {
                try
                {
                    System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
                    System.Text.Decoder utf8Decode = encoder.GetDecoder();
                    byte[] todecode_byte = Convert.FromBase64String(EncodedString);
                    int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
                    char[] decoded_char = new char[charCount];
                    utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
                    string result = new String(decoded_char);
                    return result;
                }
                catch (Exception e)
                {
                    throw new Exception("Error in base64Encode" + e.Message);
                }
            }
            else { return ""; }
        }
    }
}
