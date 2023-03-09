using PlantShop.Services;
using Microsoft.AspNetCore.Identity;
using PlantShop.Context;
using Microsoft.AspNetCore.Identity.UI.Services;
using PlantShop.Models;

public class Startup
{   
    public IConfiguration _Configuration { get; }
     
    public Startup(IConfiguration configuration)
    {
        this._Configuration = configuration;
    }

    public void ConfigurationServices(IServiceCollection services)
    {
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        // services.AddRazorPages();
        // services.AddDefaultIdentity<UserModel>(options => options.SignIn.RequireConfirmedAccount = false)
        //         .AddEntityFrameworkStores<PlantShopIdentityDbContext>();
        services.AddIdentity<UserModel, IdentityRole>()
                .AddEntityFrameworkStores<PlantShopIdentityDbContext>()
                .AddDefaultTokenProviders();

        //IdentityOptions
        services.Configure<IdentityOptions> (options => {
            //password
            options.Password.RequireDigit = false;
            options.Password.RequireLowercase = false; 
            options.Password.RequireNonAlphanumeric = false; 
            options.Password.RequireUppercase = false; 
            options.Password.RequiredLength = 3;
            options.Password.RequiredUniqueChars = 1; 

            //lock user
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes (5); 
            options.Lockout.MaxFailedAccessAttempts = 5; 
            options.Lockout.AllowedForNewUsers = true;

            //user
            options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
            options.User.RequireUniqueEmail = true;  

            //login
            options.SignIn.RequireConfirmedEmail = true;            
            options.SignIn.RequireConfirmedPhoneNumber = false;     

        });

        //mail
        services.AddOptions ();                                        
        var mailsettings = _Configuration.GetSection("MailSettings");  
        services.Configure<SendMailService>(mailsettings);              
        services.AddTransient<IEmailSender, SendMailService>();  

        services.AddScoped<IUserService, UserService>();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment()) {
            app.UseDeveloperExceptionPage();
        } else {
            app.UseExceptionHandler("/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        // app.UseEndpoints(endpoints =>
        // {
        //     endpoints.MapControllerRoute(
        //         name: "default",
        //         pattern: "{controller=Home}/{action=Index}/{id?}");
        //     endpoints.MapRazorPages();
        // });
    }
}