using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ProductSettlement.SDK;
using ProductSettlement.SDK.Request;
using ZxxkUser.ParaModel.UserAccount;
using ZxxkUser.SDK.Request;
using ZxxkUser.ViewModel.UserAccount;

namespace _2016_07_07_M站下载测试
{
    class Program
    {
        static void Main()
        {
            //Main1();
            Main2();
        }

        /// <summary>
        /// 资料验证
        /// </summary>
        /// <param name="args"></param>
        static void Main1()
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
            //string jsonContent = "[{'SoftIDs':[5318766,5318767],'DownloadChannel':1},{'SoftIDs':[5318761,5318763],'DownloadChannel':2}]";
            string jsonContent = "[{'SoftIDs':[5390434],'DownloadChannel':4}]"; // 4 按点下载 5390434 免费资料

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

        /// <summary>
        /// 资料下载
        /// </summary>
        static void Main2()
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
            ITopClient clientInstance = new DefaultResourceClient(userInfoUrl, userInfoAppKey, userInfoAppSecret);
            DownloadRequest vRequest = new DownloadRequest();
            //string jsonContent = "[{'SoftIDs':[5318766,5318767],'DownloadChannel':1},{'SoftIDs':[5318761,5318763],'DownloadChannel':2}]";
            string jsonContent = "[{'SoftIDs':[5390434],'DownloadChannel':4}]"; // 4 按点下载 5390434 免费资料

            if (!string.IsNullOrEmpty(jsonContent))
            {
                vRequest.ApiParam.JsonContents = jsonContent;//"[{'SoftIDs':[5318766,5318767],'DownloadChannel':1},{'SoftIDs':[5318761,5318763],'DownloadChannel':2}]";
                vRequest.ApiParam.Code = "";
                vRequest.ApiParam.UserID = Convert.ToInt32(28244293);//28244293;
                vRequest.ApiParam.PlatForm = (ProductSettlement.SDK.Model.EnumPlatForm)Convert.ToInt32(ProductSettlement.SDK.Model.EnumPlatForm.PC);//  SDK.Model.EnumPlatForm.PC;
                vRequest.ApiParam.RequestSource = (ProductSettlement.SDK.Model.EnumRequestSource)Convert.ToInt32(ProductSettlement.SDK.Model.EnumRequestSource.ZXXK);// SDK.Model.EnumRequestSource.ZXXK;
                var res = clientInstance.Execute(vRequest);

                ProductSettlement.SDK.Model.E_DownloadData result = res.Result;

                var innertext = JsonConvert.SerializeObject(result);
            }

        }

        static void Main3()
        {

            // <summary>
            /// 调用用户体系Url
            /// </summary>
            string userInfoUrl = "http://10.1.1.44:5556/"; //ConfigSettings.UserInfoAppUrl;

            /// <summary>
            /// 调用用户体系key
            /// </summary>
            string userInfoAppKey = "25b03dd3c0119079"; //ConfigSettings.UserInfoAppKey;

            /// <summary>
            /// 调用用户体系密钥
            /// </summary>

            string userInfoAppSecret = "JIIyvRDPE0a9dMREE16R3w=="; //ConfigSettings.UserInfoAppSecret;
            ZxxkUser.SDK.DefaultResourceClient _userServiceClient = new ZxxkUser.SDK.DefaultResourceClient(userInfoUrl, userInfoAppKey, userInfoAppSecret);
            E_UserDownloadChannelCollection re = null;
            var softids = new List<int> { 5387196, 5390434, 5391218 };
            try
            {
                re = _userServiceClient.Execute(new GetUserDownloadChannelRequest()
                {
                    ApiParam = new E_GetUserDownloadChannel()
                    {
                        clentIp = "36.110.49.98",
                        SoftIds = string.Join(",", softids),
                        UserId = 28126018
                    }
                })?.Result;

            }
            catch (Exception e)
            {
                // ignored
            }
        }
    }
}
