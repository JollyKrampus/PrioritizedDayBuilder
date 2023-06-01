using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Hangfire;
using PrioritizedDayBuilder.Jobs;
using Hangfire.MemoryStorage;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

// Hangfire configuration
builder.Services.AddHangfire(config => config.UseMemoryStorage());
builder.Services.AddHangfireServer();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapBlazorHub();
    endpoints.MapFallbackToPage("/_Host");
    endpoints.MapHangfireDashboard();
});

string cronExpression = Cron.Daily(); // This sets the job to run daily at 00:00
TimeZoneInfo mountainStandardTime = TimeZoneInfo.FindSystemTimeZoneById("Mountain Standard Time");

RecurringJob.AddOrUpdate(() => new BuildPrioritizedDay().Build(), cronExpression, mountainStandardTime);


app.Run();
