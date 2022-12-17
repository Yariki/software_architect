using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logging.Services;

public interface ICorrelationIdGenerator
{
    string CorrelationId { get; }

    void SetCorrelaltionId(string correlationId);
}

public class CorrelationIdGenerator  : ICorrelationIdGenerator
{
    private string _currentCorrelationId = Guid.NewGuid().ToString();

    public CorrelationIdGenerator()
    {
    }

    public string CorrelationId
    {
        get => _currentCorrelationId;
    }
    

    public void SetCorrelaltionId(string correlationId) => 
        _currentCorrelationId = correlationId;
    
}
