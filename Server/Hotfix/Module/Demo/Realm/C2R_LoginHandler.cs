﻿using System;
using System.Net;
using ETModel;

namespace ETHotfix
{
	[MessageHandler(AppType.Realm)]
	public class C2R_LoginHandler : AMRpcHandler<C2R_Login, R2C_Login>
	{
		protected override void Run(Session session, C2R_Login message, Action<R2C_Login> reply)
		{
			RunAsync(session, message, reply).Coroutine();
		}

		private async ETVoid RunAsync(Session session, C2R_Login message, Action<R2C_Login> reply)
		{
			R2C_Login response = new R2C_Login();
            try
            {
                User user = Game.Scene.GetComponent<UserComponent>().GetByAccount(message.Account);
                if (user != null)
                {
                    if (message.Password == user.Password)
                    {
                        Console.WriteLine(" 用户名: " + user.Account + " 验证通过！");

                        // 随机分配一个Gate
                        StartConfig config = Game.Scene.GetComponent<RealmGateAddressComponent>().GetAddress();
                        //Log.Debug($"gate address: {MongoHelper.ToJson(config)}");
                        IPEndPoint innerAddress = config.GetComponent<InnerConfig>().IPEndPoint;
                        Session gateSession = Game.Scene.GetComponent<NetInnerComponent>().Get(innerAddress);

                        // 向gate请求一个key,客户端可以拿着这个key连接gate
                        G2R_GetLoginKey g2RGetLoginKey = (G2R_GetLoginKey)await gateSession.Call(new R2G_GetLoginKey() { Account = message.Account });
                        string outerAddress = config.GetComponent<OuterConfig>().Address2;

                        response.Key = g2RGetLoginKey.Key;
                        response.Address = outerAddress;
                        reply(response);
                    }
                    else
                    {
                        Console.WriteLine(" 用户名: " + message.Account + " 密码验证错误！");

                        response.Error = ErrorCode.ERR_AccountOrPasswordError;
                        response.Message = " 用户名: " + message.Account + " 密码验证错误！";
                        reply(response);
                        return;
                    }
                }
                else
                {
                    Console.WriteLine("用户名：" + message.Account + "不存在！");

                    response.Error = ErrorCode.ERR_AccountOrPasswordError;
                    response.Message = " 用户名: " + message.Account + " 不存在！";
                    reply(response);
                    return;
                }              
            }
            catch (Exception e)
            {
                ReplyError(response, e, reply);
            }
		}
	}
}