using Microsoft.EntityFrameworkCore;

namespace BackendTest_WebAPI.Middlewares;

public class TransactionHandingMiddleware : IMiddleware
{
    private readonly ApplicationDbContext dbcontext;
    private readonly HashSet<string> CommandMethods = new HashSet<string>
    {
        "POST",
        "PUT",
        "PATCH",
        "DELETE"
    };

    public TransactionHandingMiddleware(ApplicationDbContext dbcontext)
    {
        this.dbcontext = dbcontext;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        if (IsCommand(context))
        {
            var strategy = dbcontext.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                await using var transaction = await dbcontext.Database.BeginTransactionAsync(); // Isolation Level here
                {
                    try
                    {
                        await next(context);
                        await dbcontext.SaveChangesAsync();
                        await transaction.CommitAsync();
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                }
            });
        }
        else
            await next(context);
    }

    private bool IsCommand(HttpContext context)
    {
        var method = context.Request.Method;
        return CommandMethods.Contains(method);
    }
}
