using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Wesley.Component.Captcha.Strategies.YouYouYun
{
    public static class YouYouYun
    {

        [DllImport("UUWiseHelper.dll")]
        public static extern void uu_CheckApiSign(int softId, string softKey, string guid, string fileMd5, string fileCrc, StringBuilder checkResult);


        [DllImport("UUWiseHelper.dll")]
        public static extern void uu_setSoftInfo(int softId, string softKey);


        [DllImport("UUWiseHelper.dll")]
        public static extern int uu_login(string u, string p);


        [DllImport("UUWiseHelper.dll")]
        public static extern int uu_reguser(string u, string p, int softid, string softkey);


        [DllImport("UUWiseHelper.dll")]
        public static extern int uu_reportError(int codeid);


        [DllImport("UUWiseHelper.dll")]
        public static extern int uu_getScore(string username, string password);


        [DllImport("UUWiseHelper.dll")]
        public static extern int uu_pay(string username, string card, int softId, string softKey);


        [DllImport("UUWiseHelper.dll")]
        public static extern int uu_recognizeByCodeTypeAndPath(string path, int codeType, StringBuilder result);


        [DllImport("UUWiseHelper.dll")]
        public static extern int uu_recognizeByCodeTypeAndBytes(byte[] picContent, int codeLength, int codeType, StringBuilder result);

        [DllImport("UUWiseHelper.dll")]
        public static extern int uu_recognizeByCodeTypeAndUrl(string url, string inCookie, int codeType, string cookieResult, StringBuilder result);


        [DllImport("UUWiseHelper.dll")]
        public static extern int uu_SysCallOneParam(int repeatTime, int maxRepeat);


        [DllImport("UUWiseHelper.dll")]
        public static extern int uu_easyRecognizeFile(int softId, string softKey, string userName, string password, string picPath, int codeType, StringBuilder result);
    }
}
