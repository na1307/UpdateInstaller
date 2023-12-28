namespace UpdateInstaller.Features;

/// <summary>
/// 확장 메서드 모음
/// </summary>
public static class StringExtensions {
    /// <summary>
    /// <paramref name="source"/> 문자열에서 왼쪽으로 <paramref name="length"/>만큼의 문자열을 반환
    /// </summary>
    /// <param name="source">원본 문자열</param>
    /// <param name="length">자를 길이</param>
    /// <returns><paramref name="source"/>에서 <paramref name="length"/>만큼 자른 문자열</returns>
    public static string Left(this string source, int length) => source.Substring(0, length);

    /// <summary>
    /// <paramref name="source"/> 문자열에서 오른쪽으로 <paramref name="length"/>만큼의 문자열을 반환
    /// </summary>
    /// <param name="source">원본 문자열</param>
    /// <param name="length">자를 길이</param>
    /// <returns><paramref name="source"/>에서 <paramref name="length"/>만큼 자른 문자열</returns>
    public static string Right(this string source, int length) => source.Substring(source.Length - length);

#nullable disable
    /// <summary>
    /// 하나의 문자열로 합침
    /// </summary>
    /// <param name="strings"><see cref="IEnumerable{string}"/></param>
    /// <returns>합쳐진 문자열</returns>
    public static string ToJoinedString(this IEnumerable<string> strings) => ToJoinedString(strings, null);

    /// <summary>
    /// 하나의 문자열로 합침
    /// </summary>
    /// <param name="strings"><see cref="IEnumerable{string}"/></param>
    /// <param name="separator">구분자</param>
    /// <returns>합쳐진 문자열</returns>
    public static string ToJoinedString(this IEnumerable<string> strings, string separator) => string.Join(separator,
#if !NETFRAMEWORK || NET40_OR_GREATER
        strings
#else
        new List<string>(strings).ToArray()
#endif
        );
#nullable restore
}
