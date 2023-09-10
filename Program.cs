using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using MVCExamProject.Data;
using MVCExamProject.Repository;
using MVCExamProject.Repository.Interfaces;

namespace MVCExamProject
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			builder.Services.AddControllersWithViews();
			builder.Services.AddDbContext<ExamContext>(options =>
			options.UseSqlServer(builder.Configuration.GetConnectionString("Exams")));

			//builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
			//   .AddCookie();

			builder.Services.AddScoped<IAdminRepository, AdminService>();
			builder.Services.AddScoped<IUserRepository, UserService>();
			builder.Services.AddScoped<IContactUsRepository, ContactUsService>();
			builder.Services.AddScoped<IExamRepository, ExamService>();
			builder.Services.AddScoped<IExamQuestionRepository, ExamQuestionService>();
			builder.Services.AddScoped<IQuestionOptionRepository, QuestionOptionService>();
			builder.Services.AddScoped<IStudentRepository, StudentService>();
			builder.Services.AddScoped<IUserExamRepository, UserExamService>();

			builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();



			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (!app.Environment.IsDevelopment())
			{
				app.UseExceptionHandler("/Home/Error");
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthorization();

			app.MapControllerRoute(
				name: "default",
				pattern: "{controller=Home}/{action=Index}/{id?}");

			app.Run();
		}
	}
}