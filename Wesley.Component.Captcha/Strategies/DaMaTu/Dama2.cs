using System.Runtime.InteropServices;
using System.Text;

namespace Wesley.Component.Captcha.Strategies.DaMaTu
{
   public struct Rect
    {
        public long Left;
        public long Top;
        public long Right;
        public long Bottom;
    }

   public class Dama2
    {
        //************************************
        //			error code
        //************************************
        //success code
        public const int ERR_CC_SUCCESS = 0;
        //parameter eror
        public const int ERR_CC_SOFTWARE_NAME_ERR = -1;
        public const int ERR_CC_SOFTWARE_ID_ERR = -2;
        public const int ERR_CC_FILE_URL_ERR = -3;
        public const int ERR_CC_COOKIE_ERR = -4;
        public const int ERR_CC_REFERER_ERR = -5;
        public const int ERR_CC_VCODE_LEN_ERR = -6;
        public const int ERR_CC_VCODE_TYPE_ID_ERR = -7;
        public const int ERR_CC_POINTER_ERROR = -8;
        public const int ERR_CC_TIMEOUT_ERR = -9;
        public const int ERR_CC_INVALID_SOFTWARE = -10;
        public const int ERR_CC_COOKIE_BUFFER_TOO_SMALL = -11;
        public const int ERR_CC_PARAMETER_ERROR = -12;
        //user error
        public const int ERR_CC_USER_ALREADY_EXIST = -100;
        public const int ERR_CC_BALANCE_NOT_ENOUGH = -101;
        public const int ERR_CC_USER_NAME_ERR = -102;
        public const int ERR_CC_USER_PASSWORD_ERR = -103;
        public const int ERR_CC_QQ_NO_ERR = -104;
        public const int ERR_CC_EMAIL_ERR = -105;
        public const int ERR_CC_TELNO_ERR = -106;
        public const int ERR_CC_DYNC_VCODE_SEND_MODE_ERR = -107;
        public const int ERR_CC_INVALID_CARDNO = -108;
        public const int ERR_CC_DYNC_VCODE_OVERFLOW = -109;
        public const int ERR_CC_DYNC_VCODE_TIMEOUT = -110;
        public const int ERR_CC_USER_SOFTWARE_NOT_MATCH = -111;
        public const int ERR_CC_NEED_DYNC_VCODE = -112;
        //logic error
        public const int ERR_CC_NOT_LOGIN = -201;
        public const int ERR_CC_ALREADY_LOGIN = -202;
        public const int ERR_CC_INVALID_REQUEST_ID = -203;		//invalid request id, perhaps request is timeout
        public const int ERR_CC_INVALID_VCODE_ID = -204;		//invalid captcha id
        public const int ERR_CC_NO_RESULT = -205;
        public const int ERR_CC_NOT_INIT_PARAM = -206;
        public const int ERR_CC_ALREADY_INIT_PARAM = -207;
        public const int ERR_CC_SOFTWARE_DISABLED = -208;
        public const int ERR_CC_NEED_RELOGIN = -209;
        public const int EER_CC_ILLEGAL_USER = -210;
        public const int EER_CC_REQUEST_TOO_MUCH = -211;        //concurrent request is too much

        //system error
        public const int ERR_CC_CONFIG_ERROR = -301;
        public const int ERR_CC_NETWORK_ERROR = -302;
        public const int ERR_CC_DOWNLOAD_FILE_ERR = -303;
        public const int ERR_CC_CONNECT_SERVER_FAIL = -304;
        public const int ERR_CC_MEMORY_OVERFLOW = -305;
        public const int ERR_CC_SYSTEM_ERR = -306;
        public const int ERR_CC_SERVER_ERR = -307;
        public const int ERR_CC_VERSION_ERROR = -308;
        public const int ERR_CC_READ_FILE = -309; //读图片文件失败



        /**
                 功能：　　　　获取原错误码
                 函数名：　　　GetOrigError
                 返回值：　　　0 成功 其它 - 原错误码
                 参数：　　　　无
                 说明：用户在函数调用发生错误时会通过本函数会返回一个错误码编号，供开发人员提交给平台服务商查找错误源代码。
                */
        [DllImport("CrackCaptchaAPI.dll", CharSet = CharSet.Ansi)]
        public static extern int GetOrigError();


        /**
                 功能：　　　　软件初始化
                 函数名：　　　Init
                 返回值：　　　0 成功, 其它失败，详见错误码定义
                 参数：　　　　pszSoftwareName（最大31个字符）
                        pszSoftwareID（32个16hex字符组成）
                说明： 本函数只需要调用一次即可
                */
        [DllImport("CrackCaptchaAPI.dll", CharSet = CharSet.Ansi)]
        public static extern int Init(string softwareName, string softwareID);


        /**
　                功能：　　　　软件反初始化，释放系统资源
　                函数名：　　　Uninit
　                返回值：　　　0 成功, 其它失败，详见错误码定义
　                参数：　　　　无
                    说明：本函数在软件退出时调用，以释放系统资源
                */
        [DllImport("CrackCaptchaAPI.dll", CharSet = CharSet.Ansi)]
        public static extern void Uninit();

        /**
                 功能：　　　　用户登录
                 函数名：　　　Login
                 返回值：　　　0 成功, 其它失败，详见错误码定义
                 参数：　　　　[in]pszUserName（用户名，最大31字节）
                        [in]pszUserPassword（密码，最大16字节）
                        [in]pDyncVerificationCode（动态验证码，没有动态验证码可直接传NULL）
                        [out]pszSysAnnouncementURL（返回打码兔平台公告URL，传入的缓冲区建议512字节，开发者可自行决定是否在界面上显示）
                        [out]pszAppAnnouncementURL（返回打码兔开发者后台自已定义的公告URL，传入的缓冲区建议512字节）
                说明：本函数只需要调用一次即可，重复登录会返回重复登录的错误码。
                       如果需要切换用户，可调用Logoff后再登录新的用户。
                */
        [DllImport("CrackCaptchaAPI.dll", CharSet = CharSet.Ansi)]
        public static extern int Login(string pszUserName, string pszUserPassword,
            string pDyncVerificationCode,
            StringBuilder pszSysAnnouncementURL, StringBuilder pszAppAnnouncementURL);

        /**
                功能：　　　　用户登录(简化版)
                函数名：　　　Login2
                返回值：　　　0 成功, 其它失败，详见错误码定义
                参数：　　　　[in]pszUserName（用户名，最大31字节）
                       [in]pszUserPassword（密码，最大16字节）
                说明：本函数只需要调用一次即可，重复登录会返回重复登录的错误码。
                      如果需要切换用户，可调用Logoff后再登录新的用户。
               */
        [DllImport("CrackCaptchaAPI.dll", CharSet = CharSet.Ansi)]
        public static extern int Login2(string pszUserName, string pszUserPassword);

        /**
                 功能：　　　　用户登出、用户注销
                 函数名：　　　Logoff
                 返回值：　　　0 成功, 其它失败，详见错误码定义
                 参数：　　　　无
                说明：用户登出后，很多操作将不可用。建议在切换用户或软件退出时调用此函数。
                */
        [DllImport("CrackCaptchaAPI.dll", CharSet = CharSet.Ansi)]
        public static extern int Logoff();

        /**
                 功能：　　　　用户注册
                 函数名：　　　Register
                 返回值：　　　0 成功, 其它失败，详见错误码定义
                 参数：　　　　pszUserName - 用户名，最大31个字节
                        pszUserPassword - 密码，最大16字节
                        pszQQ - QQ号码，可为空，最大16字节
                        pszTelNo - 手机号码，最大16字节，如果动态码发送方式为1或3，手机号则必填
                        pszEmail - 邮箱，最大48字节，如果动态码发送方式为2或3，则邮箱必填
                        nDyncSendMode - 动态码发送方式，1手机 2邮箱 3手机加邮箱
                说明：动态码发送方式分为：1、手机；2、邮箱；3、手机加邮箱，此功能有效地防止用户账号被盗用，如果用户异地登录或进行重要操作时，会需要使用动态验证码验证，确保用户账户安全！
                 */
        [DllImport("CrackCaptchaAPI.dll", CharSet = CharSet.Ansi)]
        public static extern int Register(string pszUserName, string pszUserPassword,
            string pszQQ, string pszTelNo, string pszEmail, int nDyncSendMode);


        /**
                 功能：　　　　读取用户信息
                 函数名：　　　ReadInfo
                 返回值：　　　0 成功, 其它失败，详见错误码定义
                 参数：　　　　[out]pszUserName - 用户名，传入缓冲区最小需32字节
                        [out]pszQQ - QQ号码，传入缓冲区最小需16字节
                        [out]pszTelNo - 手机号码，传入缓冲区最小需16字节
                        [out]pszEmail - 邮箱，传入缓冲区最小需48字节
                        [out]pDyncSendMode - 动态码发送方式
                */
        [DllImport("CrackCaptchaAPI.dll", CharSet = CharSet.Ansi)]
        public static extern int ReadInfo(StringBuilder pszUserName, StringBuilder pszQQ,
            StringBuilder pszTelNo, StringBuilder pszEmail, ref int pDyncSendMode);

        /**
                 功能：　　　　修改登录用户信息
                 函数名：　　　ChangeInfo
                 返回值：　　　0 成功, 其它失败，详见错误码定义
                 参数：　　　　pUserOldPassword - 旧密码，最大16字节
                        pUserNewPassword - 新密码，最大16字节
                        pszQQ - QQ号码，可为空，最大16字节
                        pszTelNo - 手机号码，最大16字节，如果动态码发送方式为1或3，手机号则必填
                        pszEmail - 邮箱，最大48字节，如果动态码发送方式为2或3，则邮箱必填
                        pszDyncVCode - 动态验证码，第一次调用可传NULL，当有动态验证码后，填入用户输入的动态码再次调用。
                        nDyncSendMode - 动态码发送方式，1手机 2邮箱 3手机加邮箱
                说明：修改用户信息属于重要操作，为了用户账号安全，需要校验用户动态验证码，所以要进行两次调用。
                        在调用时需特别注意：
                            第一次：pszDyncVCode传空调用修改资料，平台会返回DAMA2_RET_NEED_DYNC_VCODE的错误码，并对用户发送动态验证码，开发者此时需要提示用户输入动态验证码。
                            第二次：将用户填入的动态验证码填到pszDyncVCode再次调用本函数。
                */
        [DllImport("CrackCaptchaAPI.dll", CharSet = CharSet.Ansi)]
        public static extern int ChangeInfo(string pUserOldPassword, string pUserNewPassword,
            string pszQQ, string pszTelNo, string pszEmail, string pszDyncVCode,
            int nDyncSendMode);


        /**
                 功能：　　　　通过传入验证码图片URL地址请求打码，由打码兔控件负责下载上传
                 函数名：　　　Decode
                 返回值：　　　0 成功, 其它失败，详见错误码定义
                 参数：　　　　[in]pszFileURL - 验证码图片URL，最长511
                        [in]pszCookie - 获取验证码所需Cookie，最长4095字节
                        [in]pszReferer - 获取验证码所需Referer，最长511字节
                        [in]ucVerificationCodeLen - 验证码长度，传入正确的验证码长度，将优先被识别。如果长度不定，可传0
                        [in]usTimeout - 验证码超时时间，即过多久验证码将失效。单位秒。推荐120
                        [in]ulVCodeTypeID - 验证码类型ID，请通过打码兔开发者后台您添加的软件中添加自己软件可能用到的验证码类型，并获取生成的ID
                        [in]bDownloadPictureByLocalMachine - 是否本机下载，因为有些验证码绑定IP，不允许远程获取，如果此标志为TRUE，则打码兔控件将在您机器上自动下载图片并上传。
                                        对于没有此限制的验证码，将会由打码用户端下载，效率更高！建议填FALSE
                        [out]pulRequestID - 返回请求ID，为后面的GetResult取打码结果所用。
                */
        [DllImport("CrackCaptchaAPI.dll", CharSet = CharSet.Ansi)]
        public static extern int Decode(string pszFileURL,
            string pszCookie, string pszReferer,
            byte ucVerificationCodeLen, ushort usTimeout,
            uint ulVCodeTypeID, int bDownloadPictureByLocalMachine,
            ref uint pulRequestID);

        /**
                 功能：　　　　通过传入验证码图片URL地址请求打码，由打码兔控件负责下载上传(同步版本)
                 函数名：　　　DecodeSync
                 返回值：　　　>0，成功，返回验证码ID，用于调用ReportResult,；小于0，失败，详见错误码定义
                 参数：　　　　[in]pszFileURL - 验证码图片URL，最长511
                        [in]pszCookie - 获取验证码所需Cookie，最长4095字节
                        [in]pszReferer - 获取验证码所需Referer，最长511字节
                        [in]usTimeout - 验证码超时时间，即过多久验证码将失效。单位秒。推荐120
                        [in]ulVCodeTypeID - 验证码类型ID，请通过打码兔开发者后台您添加的软件中添加自己软件可能用到的验证码类型，并获取生成的ID
                        [out]pszVCodeText - 成功时返回验证文本。
                        [out]pszRetCookie - 成功时返回Cookie。
                */
        [DllImport("CrackCaptchaAPI.dll", CharSet = CharSet.Ansi)]
        public static extern int DecodeSync(string pszFileURL,
            string pszCookie, string pszReferer,
            ushort usTimeout,
            uint ulVCodeTypeID,
            StringBuilder pszVCodeText, StringBuilder pszRetCookie);

        /**
                 功能：　　　　通过传入验证码图片数据流请求打码，开发者需要自行下载并打开验证码图片，获得图片数据后调用本函数请求打码
                 函数名：　　　DecodeBuf
                 返回值：　　　0 成功, 其它失败，详见错误码定义
                 参数：　　　　[in]pImageData - 验证码图片数据
                        [in]dwDataLen - 验证码图片数据长度，即pImageData大小，限制4M
                        [in]pszExtName - 图片扩展名，如JPEG、BMP、PNG、GIF
                        [in]ucVerificationCodeLen - 验证码长度，传入正确的验证码长度，将优先被识别。如果长度不定，可传0
                        [in]usTimeout - 验证码超时时间，即过多久验证码将失效。单位秒。推荐120
                        [in]ulVCodeTypeID - 验证码类型ID，请通过打码兔开发者后台您添加的软件中添加自己软件可能用到的验证码类型，并获取生成的ID
                        [out]pulRequestID - 返回请求ID，为后面的GetResult取打码结果所用。
                */
        [DllImport("CrackCaptchaAPI.dll", CharSet = CharSet.Ansi)]
        public static extern int DecodeBuf(
            byte[] pImageData, uint dwDataLen, string pszExtName,
            byte ucVerificationCodeLen, ushort usTimeout,
            uint ulVCodeTypeID,
            ref uint pulRequestID);

        /**
                 功能：　　　　通过传入验证码图片数据流请求打码，开发者需要自行下载并打开验证码图片，获得图片数据后调用本函数请求打码（同步版本）
                 函数名：　　　DecodeBufSync
                 返回值：　　　>0，成功，返回验证码ID，用于调用ReportResult,；小于0，失败，详见错误码定义
                 参数：　　　　[in]pImageData - 验证码图片数据
                        [in]dwDataLen - 验证码图片数据长度，即pImageData大小，限制4M
                        [in]pszExtName - 图片扩展名，如JPEG、BMP、PNG、GIF
                        [in]usTimeout - 验证码超时时间，即过多久验证码将失效。单位秒。推荐120
                        [in]ulVCodeTypeID - 验证码类型ID，请通过打码兔开发者后台您添加的软件中添加自己软件可能用到的验证码类型，并获取生成的ID
                         [out]pszVCodeText - 成功时返回验证文本。
                */
        [DllImport("CrackCaptchaAPI.dll", CharSet = CharSet.Ansi)]
        public static extern int DecodeBufSync(
            byte[] pImageData, uint dwDataLen, string pszExtName,
            ushort usTimeout,
            uint ulVCodeTypeID,
            StringBuilder pszVCodeText);

        /**
　                功能：　　　　通过窗口定义捕获窗口图片请求打码
　                函数名：　　　DecodeWnd
　                返回值：　　　0 成功, 其它失败，详见错误码定义
　                参数：　　　　[in]pszWndDef - 窗口定义字串，详见下面描述
　　　　　　　　                [in]lpRect - 要截取的窗口内容矩形(相对于窗口最左上角),NULL表示截取整个窗口内容
　　　　　　　　                [in]ucVerificationCodeLen - 验证码长度，传入正确的验证码长度，将优先被识别。如果长度不定，可传0
　　　　　　　　                [in]usTimeout - 验证码超时时间，即过多久验证码将失效。单位秒。推荐120
　　　　　　　　                [in]ulVCodeTypeID - 验证码类型ID，请通过打码兔开发者后台您添加的软件中添加自己软件可能用到的验证码类型，并获取生成的ID
　　　　　　　　                [out]pulRequestID - 返回请求ID，为后面的GetResult取打码结果所用。
                                窗口定义字串,格式如下: 
　　                                  由"\n"分隔的多个子串组成,一个子串表示一级窗口查找的条件.第一级窗口须为顶级窗口
　　                                  每个子串由3个元素组成:窗口Class名,窗口名,窗口索引.元素之间以逗号(半角)分隔
　　　　                                窗口类名:如不想通过类名查找,填"ANY_CLASS"
　　　　                                窗口名:窗口的名字,如没有窗口名,填"ANY_NAME"
　　　　                                序号:以1开始数字,1表示第1个窗口名和窗口类名符合的窗口,
　　　　　　                                  如序号不为1,则依次查找符合条件和序号的窗口
　　                                  窗口级最大为50级
　　                                  如要查找要查找第一个类名为"TopClass"窗口名不限的第二子窗口(类名和类名都不限),
　　                                  字串如下:
　　　　                                TopClass,ANY_NAME,1\nANY_CLASS,ANY_NAME,2                        
                */
        [DllImport("CrackCaptchaAPI.dll", CharSet = CharSet.Ansi)]
        public static extern int DecodeWnd(
            string pszWndDef, ref Rect lpRect,
            byte ucVerificationCodeLen, ushort usTimeout,
            uint ulVCodeTypeID,
            ref uint pulRequestID);

        /**
　                功能：　　　　通过窗口定义捕获窗口图片请求打码(同步版本)
　                函数名：　　　DecodeWndSync
　                返回值：　　　>0，成功，返回验证码ID，用于调用ReportResult,；小于0，失败，详见错误码定义
　                参数：　　　　[in]pszWndDef - 窗口定义字串，详见下面描述
　　　　　　　　                [in]lpRect - 要截取的窗口内容矩形(相对于窗口最左上角),NULL表示截取整个窗口内容
　　　　　　　　                [in]ucVerificationCodeLen - 验证码长度，传入正确的验证码长度，将优先被识别。如果长度不定，可传0
　　　　　　　　                [in]usTimeout - 验证码超时时间，即过多久验证码将失效。单位秒。推荐120
　　　　　　　　                [in]ulVCodeTypeID - 验证码类型ID，请通过打码兔开发者后台您添加的软件中添加自己软件可能用到的验证码类型，并获取生成的ID
　　　　　　　　                [out]pulRequestID - 返回请求ID，为后面的GetResult取打码结果所用。
                                             [out]pszVCodeText - 成功时返回验证文本。
                                窗口定义字串,格式如下: 
　　                                  由"\n"分隔的多个子串组成,一个子串表示一级窗口查找的条件.第一级窗口须为顶级窗口
　　                                  每个子串由3个元素组成:窗口Class名,窗口名,窗口索引.元素之间以逗号(半角)分隔
　　　　                                窗口类名:如不想通过类名查找,填"ANY_CLASS"
　　　　                                窗口名:窗口的名字,如没有窗口名,填"ANY_NAME"
　　　　                                序号:以1开始数字,1表示第1个窗口名和窗口类名符合的窗口,
　　　　　　                                  如序号不为1,则依次查找符合条件和序号的窗口
　　                                  窗口级最大为50级
　　                                  如要查找要查找第一个类名为"TopClass"窗口名不限的第二子窗口(类名和类名都不限),
　　                                  字串如下:
　　　　                                TopClass,ANY_NAME,1\nANY_CLASS,ANY_NAME,2                        
                */
        [DllImport("CrackCaptchaAPI.dll", CharSet = CharSet.Ansi)]
        public static extern int DecodeWndSync(
            string pszWndDef, ref Rect lpRect,
            ushort usTimeout,
            uint ulVCodeTypeID,
            StringBuilder pszVCodeText);


        /**
                 功能：　　　　通过传入本地验证码图片文件名请求打码（同步版本）
                 函数名：　　　DecodeFileSync
                 返回值：　　　>0，成功，返回验证码ID，用于调用ReportResult,；小于0，失败，详见错误码定义
                 参数：　　　　[in]pszFileName - 本地验证码图片文件名
                        [in]ucVerificationCodeLen - 验证码长度，传入正确的验证码长度，将优先被识别。如果长度不定，可传0
                        [in]usTimeout - 验证码超时时间，即过多久验证码将失效。单位秒。推荐120
                        [in]ulVCodeTypeID - 验证码类型ID，请通过打码兔开发者后台您添加的软件中添加自己软件可能用到的验证码类型，并获取生成的ID
                         [out]pszVCodeText - 成功时返回验证文本。
                */
        [DllImport("CrackCaptchaAPI.dll", CharSet = CharSet.Ansi)]
        public static extern int DecodeFileSync(
            string pszFileName,
            ushort usTimeout,
            uint ulVCodeTypeID,
            StringBuilder pszVCodeText);

        /**
                 功能：　　　　取验证码识别结果
                 函数名：　　　GetResult
                 返回值：　　　0 成功, 其它失败，详见错误码定义
                 参数：　　　　[in]ulRequestID - 验证码请求ID，由Decode、DecodeBuf、DecodeWnd等函数返回
                        [in]ulTimeout - GetResult函数等待超时时间(单位为毫秒)，如果填0函数将不阻塞立即返回，如果返回值为ERR_CC_NO_RESULT，则需由开发者循环调用直到返回成功或其它错误。
                                如果填有效超时时间，函数将阻塞等待结果，如果等到结果会立即返回，没等到将在超时时间后返回。
                        [out]pszVCode - 验证码识别结果，将通过本参数返回识别结果
                        [in]ulVCodeBufLen - 接收验证码识别结果缓冲区大小，即pszVCode缓冲区大小
                        [out]pulVCodeID - 返回验证码ID，如果调用成功取到验证码结果，开发者需保存此验证码ID，用于ReportResult函数报告验证码结果的成功失败状态。
                        [out]pszReturnCookie - 传回下载验证码图片时返回的Cookie
                        [in]ulCookieBufferLen - 接收传回cookie的缓冲区大小，即pszReturnCookie的大小
                说明：取识别结果之前需要开发者需调用Decode等请求打码的方法，获取到请求ID(ulRequestID)，通过请求ID获取验证码识别结果
                */
        [DllImport("CrackCaptchaAPI.dll", CharSet = CharSet.Ansi)]
        public static extern int GetResult(uint ulRequestID,
            uint ulTimeout, StringBuilder pszVCode, uint ulVCodeBufLen,
            ref uint pulVCodeID,
            StringBuilder pszReturnCookie, uint ulCookieBufferLen);

        /**
                 功能：　　　　报告验证码结果正确性
                 函数名：　　　GetResult
                 返回值：　　　0 成功, 其它失败，详见错误码定义
                 参数：　　　　ulVCodeID - 验证码ID，使用GetResult函数返回的验证码ID
                        bCorrect - TRUE正确 FALSE 错误
                 说明：在GetResult成功后，开发者能获取到验证码ID，使用验证码ID来报告验证码的正确性，bCorrect如果为TRUE，代表验证码正确，为FALSE则代表验证码错误。
                */
        [DllImport("CrackCaptchaAPI.dll", CharSet = CharSet.Ansi)]
        public static extern int ReportResult(uint ulVCodeID, int bCorrect);


        /**
                 功能：　　　　查询用户余额
                 函数名：　　　QueryBalance
                 返回值：　　　0 成功, 其它失败，详见错误码定义
                 参数：　　　　[out]pulBalance（返回用户余额题分）
                说明：返回的pulBalance为用户剩余题分，不是货币单位。
                */
        [DllImport("CrackCaptchaAPI.dll", CharSet = CharSet.Ansi)]
        public static extern int QueryBalance(ref uint pulBalance);


        /**
                 功能：　　　　用户充值
                 函数名：　　　Recharge
                 返回值：　　　0 成功, 其它失败，详见错误码定义
                 参数：　　　　[in]pszUserName - 充值用户名，最大32字节
                        [in]pszCardNo - 充值卡号，32字节
                        [out]pulBalance - 返回用户充值后的余额
                说明：在用户未登录也可调用。
                */
        [DllImport("CrackCaptchaAPI.dll", CharSet = CharSet.Ansi)]
        public static extern int Recharge(
            string pszUserName, string pszCardNo, ref uint pulBalance);

        /**
                 功能：　　　　一步通过传入本地验证码图片文件名完成请求打码（同步版本）
                 函数名：　　　D2File
                 返回值：　　　>0，成功，返回验证码ID，用于调用ReportResult,；小于0，失败，详见错误码定义
                            应该停机处理的错误码包括：-1~-199（参数错误、用户错误）、-208（软件禁用）、-210（非法用户）、-301（配置错误、DLL找不到）
                 参数：
                        [in]pszSoftwareID（32个16hex字符组成）
                        [in]pszUserName（用户名，最大31字节）
                        [in]pszUserPassword（密码，最大16字节）
                        [in]pszFileName - 本地验证码图片文件名
                        [in]ucVerificationCodeLen - 验证码长度，传入正确的验证码长度，将优先被识别。如果长度不定，可传0
                        [in]usTimeout - 验证码超时时间，即过多久验证码将失效。单位秒。推荐120
                        [in]ulVCodeTypeID - 验证码类型ID，请通过打码兔开发者后台您添加的软件中添加自己软件可能用到的验证码类型，并获取生成的ID
                         [out]pszVCodeText - 成功时返回验证文本。
                */
        [DllImport("CrackCaptchaAPI.dll", CharSet = CharSet.Ansi)]
        public static extern int D2File(
            string pszSoftwareID,
            string pszUserName,
            string pszUswrPassword,
            string pszFileName,
            ushort usTimeout,
            uint ulVCodeTypeID,
            StringBuilder pszVCodeText);


        /**
                 功能：　　　　一步通过传入验证码图片数据流请求打码，开发者需要自行下载并打开验证码图片，获得图片数据后调用本函数请求打码（同步版本）
                 函数名：　　　D2Buf
                 返回值：　　　>0，成功，返回验证码ID，用于调用ReportResult,；小于0，失败，详见错误码定义
                 参数：　　　　
                        [in]pszSoftwareID（32个16hex字符组成）
                        [in]pszUserName（用户名，最大31字节）
                        [in]pszUserPassword（密码，最大16字节）
                        [in]pImageData - 验证码图片数据
                        [in]dwDataLen - 验证码图片数据长度，即pImageData大小，限制4M
                        [in]usTimeout - 验证码超时时间，即过多久验证码将失效。单位秒。推荐120
                        [in]ulVCodeTypeID - 验证码类型ID，请通过打码兔开发者后台您添加的软件中添加自己软件可能用到的验证码类型，并获取生成的ID
                         [out]pszVCodeText - 成功时返回验证文本。
                */
        [DllImport("CrackCaptchaAPI.dll", CharSet = CharSet.Ansi)]
        public static extern int D2Buf(
            string pszSoftwareID,
            string pszUserName,
            string pszUswrPassword,
            byte[] pImageData, uint dwDataLen,
            ushort usTimeout,
            uint ulVCodeTypeID,
            StringBuilder pszVCodeText);

        /**
                 功能：　　　　一步通过查询用户余额
                 函数名：　　　D2Balance
                 返回值：　　　=0，成功；小于0，失败，详见错误码定义
                 参数：　　　　
                        [in]pszSoftwareID（32个16hex字符组成）
                        [in]pszUserName（用户名，最大31字节）
                        [in]pszUserPassword（密码，最大16字节）
                        [out]pulBalance - 返回用户提分
                */
        [DllImport("CrackCaptchaAPI.dll", CharSet = CharSet.Ansi)]
        public static extern int D2Balance(
            string pszSoftwareID,
            string pszUserName,
            string pszUswrPassword,
            ref uint pulBalance);
    }
}
