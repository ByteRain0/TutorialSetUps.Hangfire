using Hangfire;
using HangfirePOC.Business.SimpleCommand;
using HangfirePOC.Business.SimpleService;
using HangfirePOC.Infrastructure;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<ISimpleService, SimpleService>();
builder.Services.AddMessageDispatcher(builder.Configuration);
builder.Services.AddMediatR(typeof(SimpleCommand).Assembly);

var app = builder.Build();

RecurringJob.AddOrUpdate(
    recurringJobId: nameof(ISimpleService.DoWork), 
    methodCall: () => app.Services.GetRequiredService<ISimpleService>().DoWork(), 
    cronExpression: "*/30 * * * *");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseMessageDispatcher();

app.MapPost("/test", (IMediator mediator) => 
{
    mediator.Enqueue(Guid.NewGuid().ToString(), new SimpleCommand());
});

app.Run();