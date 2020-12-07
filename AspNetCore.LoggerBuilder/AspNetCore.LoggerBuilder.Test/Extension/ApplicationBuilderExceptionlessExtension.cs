using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Exceptionless;

namespace AspNetCore.LoggerExceptionless
{
    /// <summary>
    /// Exceptionless-扩展
    /// </summary>
    public static class ApplicationBuilderExceptionlessExtension
    {
        /// <summary>
        /// 启用-Exceptionless官方平台-记录日志
        /// </summary>
        /// <param name="app"></param>
        /// <param name="configuration"></param>
        public static void UseOfficialExceptionless(this IApplicationBuilder app, IConfiguration configuration)
        {            
            var exceptionlessKey=configuration.GetSection(nameof(LoggerBuilder.Exceptionless.ExceptionlessSecret)).Get<LoggerBuilder.Exceptionless.ExceptionlessSecret>();
            app.UseExceptionless(apiKey:exceptionlessKey?.ApiKey);
        }

        /// <summary>
        /// 启用-Exceptionless(本地搭建)平台-记录日志
        /// </summary>
        /// <param name="app"></param>
        /// <param name="configuration"></param>
        public static void UseLocalExceptionless(this IApplicationBuilder app, IConfiguration configuration)
        {           
            
            app.UseExceptionless(configuration);
        }
    }
}
