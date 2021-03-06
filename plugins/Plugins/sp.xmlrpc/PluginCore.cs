﻿using System;
using System.Web;
using JR.DevFw.PluginKernel;
using sp.xmlrpc.XmlRpc.src;
using JR.DevFw.Web.Plugin;

namespace sp.xmlrpc
{
	/// <summary>
	/// 使用/test.sh/开始访问
	/// </summary>
	public class XmlRpcPluginCore:IPlugin
	{
		internal static PluginPackAttribute attr;
		private static WeblogRPCService blogService;
        private PluginPackAttribute _attr;
		
		/// <summary>
		/// 初始化插件
		/// </summary>
		/// <param name="_app"></param>
		/// <returns></returns>
		public PluginConnectionResult Connect(IPluginHost _app)
		{
			IPluginApp app = _app as IPluginApp;
			if (app != null)
			{
				//注册
                app.Register(this,app_OnExtendModuleRequest, app_OnExtendModulePost);
			    app.MapPluginRoute(this, "xmlrpc");
			}
			
			//初始化设置
            attr = this.GetAttribute();
			bool isChanged=false;
			
			if(attr.Settings.Contains("enable_base64_image"))
			{
				WeblogRPCService.EnableBase64Images=attr.Settings["enable_base64_image"]=="yes";
			}
			else
			{
				WeblogRPCService.EnableBase64Images=false;
				attr.Settings.Set("enable_base64_image","no");
				isChanged=true;
			}
			
			if(isChanged)
			{
				attr.Settings.Flush();
			}
			
			return PluginConnectionResult.Success;
		}

		void app_OnExtendModuleRequest(HttpContext t,ref bool handled)
        {
            if (blogService == null)
            {
                blogService = new WeblogRPCService();
            }
            t.Handler = blogService;
			t.Handler.ProcessRequest(t);
			handled=true;
		}
		
		void app_OnExtendModulePost(HttpContext t,  ref bool handled)
		{
            if (blogService == null)
            {
                blogService = new WeblogRPCService();
            }
            t.Handler = blogService;
			t.Handler.ProcessRequest(t);
			handled=true;
		}

		
		#region IPlugin的成员
		
		public string GetMessage()
		{
			return "";
		}

		public bool Install()
		{
			return true;
		}

		public void Pause()
		{
		}

		public void Run()
		{
		}

		public bool Uninstall()
		{
			return false;
		}




		
		public object Call(string method, params object[] parameters)
		{
			throw new NotImplementedException();
		}
		#endregion


        public void Logln(string line)
        {
            throw new NotImplementedException();
        }


        public PluginPackAttribute GetAttribute()
        {
            if (this._attr == null)
            {
                this._attr = PluginUtil.GetAttribute(this);
            }
            return this._attr;
        }
    }
}
