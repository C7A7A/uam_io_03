using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Text;

namespace WatchTogetherAPI.Helpers
{
    public class UriHelper
    {
        public static string DecodeUrlString(string url)
        {
            string newUrl;
            while ((newUrl = HttpUtility.UrlDecode(url, Encoding.GetEncoding("iso-8859-1"))) != url)
                url = newUrl;
            return newUrl;
        }

    }
}
