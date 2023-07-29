using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cms.Backend.Api.Repository;
using Cms.Backend.Db;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Cms.Backend.Api.Params;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Cms.Backend.Api.Utils;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Cms.Backend.Filters;

namespace Cms.Backend.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        //设置跨域字符串
        private readonly string _allowCors = "AllowAllCores";


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers().AddNewtonsoftJson(option =>
            option.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            );

            // 注入跨域
            services.AddCors(options =>
            {
                options.AddPolicy(_allowCors,
                builder => builder
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowAnyOrigin());
            });

            /// <summary>
            ///   注入数据库上下文
            /// </summary>
            /// <value></value>
            services.AddDbContext<CmsBackendDb>(o =>
            {
                var connectionStrings = Configuration.GetConnectionString("MySQLConnection");
                // o.UseSqlServer(connectionStrings);

                var serverVersion = ServerVersion.AutoDetect(connectionStrings);
                o.UseMySql(connectionStrings, serverVersion);
            });

            // services.AddDbContext<CmsBackendDb>(o => { o.UseMySql(); });


            // 注入 IRepository接口及其实现类
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));


            // 配置token验证的设置
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                var tokenParameter = Configuration.GetSection("TokenParameter").Get<TokenParameter>();
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,// 验证签名，必须，要不然token就没意义了
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenParameter.Secret)),
                    ValidateIssuer = true,// 验证发行人
                    ValidIssuer = tokenParameter.Issuer,
                    ValidateAudience = false,// 不验证受众
                    ValidateLifetime = true,//  验证token的有效时间，通常都需要
                    ClockSkew = TimeSpan.Zero // 不处理计算机之间的时间误差
                };
                options.Events = new JwtBearerEvents
                {
                    //token验证失败后执行
                    OnChallenge = context =>
                    {
                        // 跳过默认响应逻辑
                        context.HandleResponse();
                        // 自定义401时返回的信息
                        var result = JsonHelper.Serialize(new { Code = "401", Message = "验证失败" });
                        context.Response.ContentType = "application/json";
                        //验证失败返回401
                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        context.Response.WriteAsync(result);
                        return Task.FromResult(0);
                    }
                };
            });


            /// <summary>
            /// 注入操作日志
            /// </summary>
            /// <value></value>
            // services.AddControllers(options =>
            // {
            //     options.Filters.Add(typeof(AuditLogActionFilter));
            // });

            services.AddControllers().AddNewtonsoftJson(option =>
               //忽略循环引用
               option.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore
              );

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Cms.Backend.Api", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Cms.Backend.Api v1"));
            }

            app.UseRouting();


            // 注册跨域中间件
            app.UseCors(_allowCors);

            // 注册验证token的中间件
            // 注意它需要添加在app.UseAuthorization();之前，app.UseRouting();之后，顺序很重要，否则身份认证不会生效。
            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });



        }
    }
}
