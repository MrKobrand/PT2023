using System;

namespace Application.Common.Interfaces;

public interface IDateService
{
    DateTimeOffset GetCurrentUtcDate();
}
