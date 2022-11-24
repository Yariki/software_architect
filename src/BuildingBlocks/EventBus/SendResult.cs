namespace EventBus;

public enum SendResult
{
    None,
    Acknowledged,
    RecoverableFailure,
    NoneRecoverableFailure
}