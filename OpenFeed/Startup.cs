using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpenFeed.Services.Cache;
using OpenFeed.Services.Database;
using OpenFeed.Services.DateTimeProvider;
using OpenFeed.Services.NewsAPI;
using OpenFeed.Services.NewsManager;
using OpenFeed.Services.NewsRepository;
using OpenFeed.Services.NewsService;
using OpenFeed.Services.NewsService.QueryBuilder;
using OpenFeed.Services.Pagination;
using OpenFeed.Services.RSS;
using IDatabaseProvider = OpenFeed.Services.Database.IDatabaseProvider;

namespace OpenFeed
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMemoryCache();
            services.AddTransient<ICache, MemoryCacheWrapper>();
            services.AddTransient<IDateTimeProvider, UtcDateTimeProvider>();
	        services.AddTransient<IPaginationService, PaginationService>();

			services.AddTransient<IDatabaseProvider, SqlDatabaseProvider>();
	        services.AddTransient<IMongoDatabaseProvider, MongoDatabaseProvider>();

	        services.AddTransient<IQueryableArticleRepository, MongoArticleRepository>();
	        services.AddTransient<IArticleRepository, MongoArticleRepository>();
	        services.AddTransient<ISortFactory<ArticleData>, ArticleSortFactory>();

			//services.AddTransient<INewsService, CachingNewsService<NewsService>>();
			services.AddTransient<INewsService, NewsService>();
            services.AddTransient<NewsService>();
            services.AddTransient<INewsApiClientProvider, NewsApiClientProvider>();
            services.AddTransient<INewsImporter, NewsImporter>();
            services.AddTransient<INewsAggregator, NewsApiNewsAggregator>();

            services.AddTransient<INewsQueryBuilder, NewsQueryBuilder>();
            services.AddTransient<INewsFilter, CategoryNewsFilter>();
            services.AddTransient<INewsFilter, TextNewsFilter>();

            services.AddTransient<IRssFeedService, RssFeedService>();
            services.AddTransient<IRssFeedRepository, RssFeedRepository>();

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
                {
                    HotModuleReplacement = true,
                    ReactHotModuleReplacement = true
                });
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapSpaFallbackRoute(
                    name: "spa-fallback",
                    defaults: new { controller = "Home", action = "Index" });
            });
        }
    }
}
