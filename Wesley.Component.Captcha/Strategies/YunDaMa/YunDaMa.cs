using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Wesley.Component.Captcha.Strategies.YunDaMa
{
    public static class YunDaMa
    {
        [DllImport("yundamaAPI.dll")]
        public static extern void YDM_SetAppInfo(int nAppId, string lpAppKey);

        [DllImport("yundamaAPI.dll")]
        public static extern int YDM_Login(string lpUserName, string lpPassWord);

        [DllImport("yundamaAPI.dll")]
        public static extern int YDM_DecodeByPath(string lpFilePath, int nCodeType, StringBuilder pCodeResult);

        [DllImport("yundamaAPI.dll")]
        public static extern int YDM_UploadByPath(string lpFilePath, int nCodeType);

        [DllImport("yundamaAPI.dll")]
        public static extern int YDM_EasyDecodeByPath(string lpUserName, string lpPassWord, int nAppId, string lpAppKey, string lpFilePath, int nCodeType, int nTimeOut, StringBuilder pCodeResult);

        [DllImport("yundamaAPI.dll")]
        public static extern int YDM_DecodeByBytes(byte[] lpBuffer, int nNumberOfBytesToRead, int nCodeType, StringBuilder pCodeResult);

        [DllImport("yundamaAPI.dll")]
        public static extern int YDM_UploadByBytes(byte[] lpBuffer, int nNumberOfBytesToRead, int nCodeType);

        [DllImport("yundamaAPI.dll")]
        public static extern int YDM_EasyDecodeByBytes(string lpUserName, string lpPassWord, int nAppId, string lpAppKey, byte[] lpBuffer, int nNumberOfBytesToRead, int nCodeType, int nTimeOut, StringBuilder pCodeResult);

        [DllImport("yundamaAPI.dll")]
        public static extern int YDM_GetResult(int nCaptchaId, StringBuilder pCodeResult);

        [DllImport("yundamaAPI.dll")]
        public static extern int YDM_Report(int nCaptchaId, bool bCorrect);

        [DllImport("yundamaAPI.dll")]
        public static extern int YDM_EasyReport(string lpUserName, string lpPassWord, int nAppId, string lpAppKey, int nCaptchaId, bool bCorrect);

        [DllImport("yundamaAPI.dll")]
        public static extern int YDM_GetBalance(string lpUserName, string lpPassWord);

        [DllImport("yundamaAPI.dll")]
        public static extern int YDM_EasyGetBalance(string lpUserName, string lpPassWord, int nAppId, string lpAppKey);

        [DllImport("yundamaAPI.dll")]
        public static extern int YDM_SetTimeOut(int nTimeOut);

        [DllImport("yundamaAPI.dll")]
        public static extern int YDM_Reg(string lpUserName, string lpPassWord, string lpEmail, string lpMobile, string lpQQUin);

        [DllImport("yundamaAPI.dll")]
        public static extern int YDM_EasyReg(int nAppId, string lpAppKey, string lpUserName, string lpPassWord, string lpEmail, string lpMobile, string lpQQUin);

        [DllImport("yundamaAPI.dll")]
        public static extern int YDM_Pay(string lpUserName, string lpPassWord, string lpCard);

        [DllImport("yundamaAPI.dll")]
        public static extern int YDM_EasyPay(string lpUserName, string lpPassWord, long nAppId, string lpAppKey, string lpCard);
    }
}
