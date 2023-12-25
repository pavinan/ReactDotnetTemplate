using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ReactDotnetTemplate.Application.Extensions;
using ReactDotnetTemplate.Application.Services.AppEvents;
using ReactDotnetTemplate.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReactDotnetTemplate.Application.Behaviours;

public class TransactionBehavior<TRequest, TResponse>(
    AppDbContext dbContext,
    IAppEventLogService appEventLogService,
    ILogger<TransactionBehavior<TRequest, TResponse>> logger
    ) : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var response = default(TResponse);
        var typeName = request.GetGenericTypeName();

        try
        {
            if (dbContext.HasActiveTransaction)
            {
                return await next();
            }

            var strategy = dbContext.Database.CreateExecutionStrategy();

            await strategy.ExecuteAsync(async () =>
            {
                Guid transactionId;

                await using var transaction = await dbContext.BeginTransactionAsync();

                logger.LogInformation("----- Begin transaction {TransactionId} for {CommandName} ({@Command})", transaction!.TransactionId, typeName, request);

                response = await next();

                logger.LogInformation("----- Commit transaction {TransactionId} for {CommandName}", transaction.TransactionId, typeName);

                await dbContext.CommitTransactionAsync(transaction);

                transactionId = transaction.TransactionId;

                await PublishEvents(transactionId);

            });
            return response!;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "ERROR Handling transaction for {CommandName} ({@Command})", typeName, request);

            throw;
        }

    }


    private async Task PublishEvents(Guid transactionId)
    {
        var pendingEventLogs = await appEventLogService.RetrieveEventLogsPendingToPublishAsync(transactionId.ToString());

        foreach (var log in pendingEventLogs)
        {
            logger.LogInformation("----- Publishing event: {EventType} - ({AppEvent})", log.EventType, log.Content);

            try
            {
                await appEventLogService.MarkEventAsInProgressAsync(log.Id!);
                //Publish via some message broker
                await appEventLogService.MarkEventAsPublishedAsync(log.Id!);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "ERROR publishing event: {EventType} - ({AppEvent})", log.EventType, log.Content);

                await appEventLogService.MarkEventAsFailedAsync(log.Id!);
            }
        }
    }

}

