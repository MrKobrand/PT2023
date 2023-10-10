using System;
using Application.Common.Interfaces;

namespace Infrastructure.Services;

public class DateService : IDateService
{
    public DateTimeOffset GetCurrentUtcDate()
    {
        return DateTimeOffset.UtcNow;
    }
}
