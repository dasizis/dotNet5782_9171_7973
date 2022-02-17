namespace BLApi
{
    public class BLFactory
    {
        public static IBL GetBL()
        {
            return BL.BL.Instance;
        }
    }
}
