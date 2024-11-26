namespace Coursework.WebAPI.Contracts
{
    public record PechResponseRange
    (
        Guid Id,
        int index,
        float firstItem,
        float lastItem,
        float stepItem,
        float tPech,
        float height,
        float tNach,
        float kTeplo,
        float Teplo,
        float p,
        float tPov,
        float kTeploOtd
    );
}
