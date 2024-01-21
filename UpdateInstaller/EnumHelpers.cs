namespace UpdateInstaller;

public static class EnumHelpers {
    public static bool TryParse<TEnum>(string? value, out TEnum result) where TEnum : struct, Enum {
#if !NET40_OR_GREATER
        try {
            result = (TEnum)Enum.Parse(typeof(TEnum), value);
            return true;
        } catch (Exception) {
            result = default;
            return false;
        }
#else
        return Enum.TryParse(value, out result);
#endif
    }
}
