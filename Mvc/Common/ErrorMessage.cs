using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mvc.Common
{
    public class ErrorMessage
    {
        public static string AlreadyCreated(string pageName, string desPageName)
        {
            return string.Format("{0} can not be updated. {1} already created!", pageName, desPageName);
        }

        public static string RecordSave
        {
            get { return "Record saved succussfully"; }
        }

    }
}