using System.IO;
using AspNetCore.LoggerBuilder;
using Microsoft.AspNetCore.Builder;

namespace AspNetCore.LoggerBuilder.Extension
{
    /// <summary>
    /// 功能描述    ：ApplicationBuilderDataMappingExtension  
    /// 创 建 者    ：jinghe
    /// 创建日期    ：2020/11/24 17:02:09
    /// 版权说明    ：Copyright ©- 2020 -xxx 所有
    /// </summary>
    public static class ApplicationBuilderExtension
    {
       
        
        /// <summary>
        /// 启用log4net,记录日志
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseLog4netBuilder(this IApplicationBuilder app)
        {
            Log4Builder.LoadLog4netconfig(Path.Combine(Directory.GetCurrentDirectory(), "dllconfigs\\Appsetting.log4net.config"));
            return app;
        }       

    }
}
