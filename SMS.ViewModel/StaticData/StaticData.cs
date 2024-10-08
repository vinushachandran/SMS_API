using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.ViewModel.StaticData
{
    public class StaticData
    {
        public static readonly string SOMETHING_WENT_WRONG = "Something went wrong please try again ";
        public static readonly string NO_DATA_FOUND = "Unable to find the {0}";
        public static readonly string SUCCESS_MESSAGE = "Successfully {0}";

        public static readonly int STATUSCODE_NOTFOUND = 404;
        public static readonly int STATUSCODE_VALIDATION = 422;
        public static readonly int STATUSCODE_INTERNAL_SERVAR_ERROR = 500;
    }
}
