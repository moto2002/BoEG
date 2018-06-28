namespace Components.Visable
{
    public interface IVisableData
    {
        bool HasSpotted { get; }
        bool HasInvisability { get; }
        bool HasHidden { get; }
    }
}