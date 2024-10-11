using Hangfire;
using HangfireDemo.Services;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add Hangfire services
builder.Services.AddHangfire(configuration =>
    configuration.UseSqlServerStorage(builder.Configuration.GetConnectionString("DefaultConnection"))); // Replace with your actual connection string
// Add Hangfire server
builder.Services.AddHangfireServer();

// Register your background job class
builder.Services.AddTransient<MyBackgroundJob>(); // Ensure MyBackgroundJob is registered as a service

// Add services for controllers and views
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Configure Hangfire dashboard middleware
app.UseHangfireDashboard();

// Register the recurring job with IBackgroundJobClient
var backgroundJobClient = app.Services.GetRequiredService<IBackgroundJobClient>();
RecurringJob.AddOrUpdate<MyBackgroundJob>(job => job.Execute(), Cron.Minutely); // This registers your recurring job

app.MapHangfireDashboard(); // Maps the Hangfire dashboard endpoint

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
