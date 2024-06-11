using UnityEngine;
using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public static class Utils
{
    /// <summary>
    /// JSON 문자열을 예쁘게 포매팅하여 Debug.Log로 출력합니다.
    /// </summary>
    /// <param name="json">포매팅할 JSON 문자열</param>
    public static void LogFormattedJson(string header, string json)
    {
        try
        {
            var parsedJson = JToken.Parse(json);
            var formattedJson = parsedJson.ToString(Formatting.Indented);
            Debug.Log(header + "\n" + formattedJson);
        }
        catch (Exception ex)
        {
            Debug.LogError("Invalid JSON string: " + ex.Message);
        }
    }
    
    public static string GetTimeUntilMidnight()
    {
        DateTime now = DateTime.Now;
        DateTime midnight = now.AddDays(1).Date; // 내일 자정 시간

        TimeSpan timeUntilMidnight = midnight - now;
        return timeUntilMidnight.ToString(@"hh\:mm\:ss");
    }
}