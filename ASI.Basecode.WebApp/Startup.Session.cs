using ASI.Basecode.Resources.Constants;
using Microsoft.Extensions.DependencyInjection;

namespace ASI.Basecode.WebApp
{
    public partial class Startup
    {
        private void ConfigureSession(IServiceCollection services)
        {
            services.AddSession(options =>
            {
                options.Cookie.Name = Const.Issuer;
            });
        }
    }
}
