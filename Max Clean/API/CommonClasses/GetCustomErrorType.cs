using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.CommonClasses
{
    public class GetCustomErrorType
    {
        public static string GetCustomError(dynamic ErrorID)
        {
            if (ErrorID > 0)
                return "Success";
            else if (ErrorID == -1)
                return "Dublicate Entry";
            else if (ErrorID == -2)
                return "Email ID Dublicate";
            else if (ErrorID == -3)
                return "Mobile Number Dublicate";
            else if (ErrorID == -4)
                return "ID Not Found";
            else if (ErrorID == -5)
                return "Please check your paying amount.";
            else if (ErrorID == -6)
                return "Already Booked.";
            else if (ErrorID == -7)
                return "No Reward Points";
            else if (ErrorID == -8)
                return "Status Cannot be Updated in Backward Direction";
            else if (ErrorID == -9)
                return "Minimum 80% Payment Required";
            else if (ErrorID == -10)
                return "Please Tag Order Before Sending To Factory";
            else if (ErrorID == -11)
                return "Bad Request";
            else
                return "Some thing went wrong.";
            
        }
    }
}
