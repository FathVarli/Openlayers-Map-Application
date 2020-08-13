namespace Core.Entities.Concrete
{
    public class Kapi:IEntity
    {
        public int MahalleId { get; set; }

        public int KapiNo { get; set; }

        public float KapiKoordinatlar { get; set; }
    }
}