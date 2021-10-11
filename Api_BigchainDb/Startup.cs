using Api_BigchainDb.Middleware;
using Api_BigchainDb.DatabaseSettings;
using Api_BigchainDb.Repository;
using Api_BigchainDb.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Serialization;
using Api_BigchainDb.Queue;

namespace Api_BigchainDb
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor();

            services.Configure<StoreDatabaseSettings>(
                Configuration.GetSection(nameof(StoreDatabaseSettings)));

            services.AddSingleton<IStoreDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<StoreDatabaseSettings>>().Value);

            services.AddSingleton<UserService>();
            services.AddSingleton<BookService>();

            services.Configure<StoreDatabaseSettings>(Configuration.GetSection("BlockchainstoreDatabaseSettings"));

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IBookService, BookService>();
       
            //services.AddScoped<IBackgroundTaskQueue, DefaultBackgroundTaskQueue>();

            services.AddControllers();

            services.AddControllers()
            .AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
            });

            services.AddSingleton<IContactsRepository, ContactsRepository>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            app.ApplyUserKeyValidation();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            loggerFactory.AddFile("Logs/mylog-{Date}.txt");
        }
    }
}
