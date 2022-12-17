using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Primitives;

namespace Logging;
public static class LoggingConsts
{
    public const string CorrelationHeader = "x-correlation-id";

    public const string CorrelationIdProperty = "CorrelationId";

    public const string ApmTransaction = "ApmTransaction";

}
