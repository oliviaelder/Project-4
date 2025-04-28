using Microsoft.EntityFrameworkCore;
using Appt_Scheduler.Data;

var builder = WebApplication.CreateBuilder(args);

// Add DbContext to the DI container
builder.Services.AddDbContext<DomainContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MyConnection")));

// Add services to the container.
builder.Services.AddControllersWithViews();

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

//Pre - load DoctorAvailability table with 9 hourly appointment every day in May
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<DomainContext>();
    LoadPatients.LoadDefaultPatients(context);
    LoadAppointments.LoadDoctorAvailability(context);
}

app.Run();
