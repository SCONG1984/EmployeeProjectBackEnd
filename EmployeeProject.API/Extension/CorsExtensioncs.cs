namespace EmployeeProject.API.Extension
{
    public static class CorsExtensioncs
    {
        public static void RegisterCors(this IServiceCollection serviceProvider, IConfiguration config)
        {
            var corsConfig = config.GetSection("WebUrls:EmployeeWebUrl");
            var urls = corsConfig.GetChildren().Select(x => x.Value).ToArray();
            serviceProvider.AddCors(options =>
            {
                options.AddPolicy("CorsApi",
                    builder => builder.WithOrigins(urls)
                        .AllowAnyHeader()
                        .AllowAnyMethod());

            });
        }
    }
}
