using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMSDesk
{
    public static class AppSession
    {
        public static string LicenseName { get; private set; }

        public static string BaseUrl { get; private set; }

        public static string ApiFolder { get; private set; }

        public static void SetLicense(string licenseName)
        {
            LicenseName = licenseName;
        }


        public static void SetBaseUrl(string baseurl)
        {
            BaseUrl = baseurl;
            SetApiFolder();
        }

        public static void SetApiFolder()
        {

            ApiFolder = BaseUrl+ "api/";
        }

        public static bool IsLoggedIn => !string.IsNullOrEmpty(LicenseName);

        public static void Clear()
        {
            LicenseName = null;
        }
    }
}
