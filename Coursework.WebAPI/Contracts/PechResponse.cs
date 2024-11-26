namespace Coursework.WebAPI.Contracts
{
    public record PechResponse
    (
        Guid Id,
        float tPech, 
        float height,
        float tNach,
        float kTeplo,
        float Teplo,
        float p,
        float tPov,
        float kTeploOtd,
        float VremNagr,
        float T
    );
}
