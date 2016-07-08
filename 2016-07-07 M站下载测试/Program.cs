using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductSettlement.SDK;
using ProductSettlement.SDK.Request;

namespace _2016_07_07_M站下载测试
{
    class Program
    {
        static void Main(string[] args)
        {
            // <summary>
            /// 调用用户体系Url
            /// </summary>
            string userInfoUrl = "http://10.1.1.4:9090/"; //ConfigSettings.UserInfoAppUrl;

            /// <summary>
            /// 调用用户体系key
            /// </summary>
            string userInfoAppKey = "32095506b784c5b9"; //ConfigSettings.UserInfoAppKey;

            /// <summary>
            /// 调用用户体系密钥
            /// </summary>
            string userInfoAppSecret = "R2A6RL6w/EWwzyGAMKr6jg=="; //ConfigSettings.UserInfoAppSecret;

            // 构造请求客户端实例
            ITopClient clientInstance = new DefaultResourceClient(userInfoUrl, userInfoAppKey, userInfoAppSecret);
            // 构造资料验证请求对象
            ValidationRequest vRequest = new ValidationRequest();
            string jsonContent = "[{\"SoftIDs\":[5318766,5318767],\"DownloadChannel\":1},{\"SoftIDs\":[5318761,5318763],\"DownloadChannel\":2}]";

            if (!string.IsNullOrEmpty(jsonContent))
            {
                //资料与下载通道集合格式："[{'SoftIDs':[5318766,5318767],'DownloadChannel':1},{'SoftIDs':[5318761,5318763],'DownloadChannel':2}]";                
                vRequest.ApiParam.JsonContents = jsonContent;
                vRequest.ApiParam.Code = "";  // 防恶意下载验证码, 多次下载会出现这玩意
                //用户ID：28244293;
                vRequest.ApiParam.UserID = Convert.ToInt32(28244293);
                // 调用平台：SDK.Model.EnumPlatForm.PC             
                vRequest.ApiParam.PlatForm = (ProductSettlement.SDK.Model.EnumPlatForm)Convert.ToInt32(ProductSettlement.SDK.Model.EnumPlatForm.PC);
                // 请求来源：SDK.Model.EnumRequestSource.ZXXK;
                vRequest.ApiParam.RequestSource = (ProductSettlement.SDK.Model.EnumRequestSource)Convert.ToInt32(ProductSettlement.SDK.Model.EnumRequestSource.ZXXK);
                // 发送请求
                var res = clientInstance.Execute(vRequest);
                Debug.WriteLine(res.ErrMsg);
                // 返回结果
                ProductSettlement.SDK.Model.E_ValidationData result = res.Result;
                // 后续对结果处理
            }
        }
    }
}
