namespace Degussa.FPASS.Util.Enums
{
    /// <summary>
    /// Holds the list of available ID card types (the chips)
    /// Hitag2 and Mifare are relevant for FPASS, PKI not saved here
    /// New in FPASS V5
    /// </summary>
	public static class IDCardTypes
    {	
        public const string Mifare = "MIFARE";
        public const string Hitag2 = "HITAG2";
	}
}
