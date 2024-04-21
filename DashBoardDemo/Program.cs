using DashBoardDemo.Interface;
using DashBoardDemo.Interface.Interface_ServerSide;
using DashBoardDemo.Models;
using DashBoardDemo.Repository;
using DashBoardDemo.ServerSideRepository;
using DashBoardDemo.Services;
using DashBoardDemo.Services_ServerSide;
using Microsoft.Build.Framework;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
IConfiguration configuration = builder.Configuration;
// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

builder.Services.AddDbContext<AppDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


// Configure session
builder.Services.AddDistributedMemoryCache(); // Add a distributed cache implementation if required
builder.Services.AddSession(options =>
{
    options.Cookie.Name = ".YourApp.Session";
    options.IdleTimeout = TimeSpan.FromMinutes(30);
   /* options.Cookie.IsEssential = true;*/
});


#region Repo

builder.Services.AddScoped<PatientsRepo>();
builder.Services.AddScoped<DoctorsRepo>();
builder.Services.AddScoped<PatientsRepos>();
builder.Services.AddScoped<UserLoginRepo>();
builder.Services.AddScoped<DoctorsRepos>();
builder.Services.AddScoped<TimeSlot_PatientsRepo>();
builder.Services.AddScoped<ServerSide_DoctosListRepo>();
builder.Services.AddScoped<ServerSide_patientslistRepo>();
builder.Services.AddScoped<ServerSide_PatientsTimeSlotRepo>();
builder.Services.AddScoped<ServerSide_DoctorsTimeSlotRepo>();
builder.Services.AddScoped<StatisticsRepo>();
builder.Services.AddScoped<PieChartRepo>();
builder.Services.AddScoped<LoginRepo>();
builder.Services.AddScoped<Edit_UpdateRepo>();
builder.Services.AddScoped<PatientDataRepo>();
builder.Services.AddScoped<LoginDetailsRepo>();
#endregion


#region Services
builder.Services.AddScoped<IAdminService, AdminService>();
builder.Services.AddScoped<IAdminDemoService, AdminDemoService>();
builder.Services.AddScoped<IPatientsService, PatientsService>();
builder.Services.AddScoped<IDoctorsService, DoctorsService>();
builder.Services.AddScoped<IServerSideDoctorsListService, ServerSideDoctorsListService>();
builder.Services.AddScoped<IServerSidePatientsListService, ServerSidePatientsListService>();
builder.Services.AddScoped<IServerSide_PatientsTimeSlotService, ServerSide_PatientsTimeSlotService>();
builder.Services.AddScoped<IServerSide_DoctorsTimeSlotService, ServerSide_DoctorsTimeSlotService>();
builder.Services.AddScoped<IStatisticsService, StatisticsService>();
builder.Services.AddScoped<IpatientLoginService, patientLoginService>();
builder.Services.AddScoped<IPieChartService, PieChartService>();
builder.Services.AddScoped<IPatientDataService, PatientDataService>();
builder.Services.AddScoped<ILoginDetailsService, LoginDetailsService>();
builder.Services.AddScoped<IEncryptionService, EncryptionService>();
#endregion

/*builder.Services.AddSession();*/
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
app.UseSession();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=PatientLogin}/{action=LoginPage}/{id?}");

app.Run();
