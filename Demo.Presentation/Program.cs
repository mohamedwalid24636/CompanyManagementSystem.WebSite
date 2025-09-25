using Demo.BusinessLogic.Profiles;
using Demo.BusinessLogic.Services;
using Demo.DataAccesse.Data.Contexts;
using Demo.DataAccesse.Repositories.Classes;
using Demo.DataAccesse.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using Demo.BusinessLogic.Services.AttachementServices;
using Demo.DataAccesse.Models.IdentityModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.DataProtection;
namespace Demo.Presentation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.



            #region Add Services to container 

            // AddControllersWithViews => to use MVC Pattern => use  (Controllers + Views)
            builder.Services.AddControllersWithViews(Options =>
            { 
            
                Options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute()); // This attribute is used to prevent CSRF attacks by validating the request token only from browser "cant use postman" .

            });


            //builder.Services.AddScoped<ApplicationDbContext>();
            builder.Services.AddDbContext<ApplicationDbContext>(Options=>

            {
                Options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
                Options.UseLazyLoadingProxies(); // Enable Lazy Loading Proxies
                //Options.UseSqlServer(builder.Configuration["ConnectionStrings:DefaultConnection"]);
            }
            );

            builder.Services.AddScoped<IDepartmentRepository,DepartmentRepository>();
            builder.Services.AddScoped<IDepartmentService , DepartmentService>();
            builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            builder.Services.AddScoped<IEmployeeService, EmployeeService>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IAttachementService , AttachementService>();
            //builder.Services.AddAutoMapper(typeof(MappingProfiles).Assembly);
            builder.Services.AddAutoMapper(M => M.AddProfile(new MappingProfiles()));
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()  //Inject UserManage (Servics Of Identity Package) 
           .AddEntityFrameworkStores<ApplicationDbContext>()
           .AddDefaultTokenProviders();                                    //Add Token Sevice

            //بعد ال IdentityService عشان تتحكم في مسار ال login page 
            //builder.Services.ConfigureApplicationCookie(options =>
            //{
            //    options.LoginPath = "/Account/Login"; // غيرها لأي URL تاني لو حابب
            //    options.AccessDeniedPath = "/Account/AccessDenied"; // لو معندكش صلاحية
            //});



            //   builder.Services.AddIdentity<ApplicationUser, IdentityRole>(Options =>
            //   {
            //       Options.User.RequireUniqueEmail = true;
            //       Options.Password.RequireUppercase = true;
            //       Options.Password.RequireLowercase = true;
            //   })    //Inject UserManage (Servics Of Identity Package) 
            //.AddEntityFrameworkStores<ApplicationDbContext>();
            #endregion




            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error"); 
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication(); // This middleware is used to authenticate the user "Check if the user is logged in or not"
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}



//Project Key Features 


//1-Genaric Model Configurations 
//2-Injection Of ApplicationDbContext
//3-Genaric Repository
//4-Unit Of Work & LazyLoading 
//5-AutoMapper
//6-IQueryable Vs IEnumerable
//7-Image Upload
//8-Search By Name
//9-Idintity Package 
//10-Mail Service
//11-public IEnumerable<TResult> GetAll<TResult>(Expression<Func<TEntity, TResult>> Selector) ==> Selector will be send as a parameter to .Select ()
//12-public IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> Predicate)  ==> Predicate will be send as a parameter to .Where ()
//13- Scoped Vs Transient Vs Singleton

//Advanced Points 


//1-Malkit Male Service 
//2-Identity With RazorPages
//3-Admin DashBoard 
//4-SignalR (RealTime chat)



// You still Need To Write Instalation Steps For
// Identity Package & MailKit Package & AutoMapper Package & LazyLoadingProxies Package ..etc 













//Error 405 Action NOt Allowed