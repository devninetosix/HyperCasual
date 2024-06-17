using UnityEngine;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Random = UnityEngine.Random;

public static class Utils
{
    /// <summary>
    /// JSON 문자열을 예쁘게 포매팅하여 Debug.Log로 출력합니다.
    /// </summary>
    /// <param name="header">헤더</param>
    /// <param name="json">포매팅할 JSON 문자열</param>
    public static void LogFormattedJson(string header, string json)
    {
// TODO: 테스트 코드 제거
#if UNITY_EDITOR
        try
        {
            var parsedJson = JToken.Parse(json);
            var formattedJson = parsedJson.ToString(Formatting.Indented);
            Log(header + "\n" + formattedJson);
        }
        catch (Exception ex)
        {
            Log("Invalid JSON string: " + ex.Message, true);
        }
#endif
    }

    public static void Log(object message, bool warning = false)
    {
#if UNITY_EDITOR
        if (warning)
        {
            Debug.LogError($"<b><color=red>{message}</color></b>");
        }
        else
        {
            Debug.Log($"<b><color=white>{message}</color></b>");
        }
#endif
    }

    public static string GetTimeUntilMidnight()
    {
        DateTime now = DateTime.UtcNow;
        DateTime midnight = now.Date.AddDays(1); // 내일 자정 시간

        TimeSpan timeUntilMidnight = midnight - now;
        return FormatTimeSpan(timeUntilMidnight);
    }

    public static string GetTimeUntilEndOfWeek()
    {
        DateTime now = DateTime.UtcNow;
        int daysUntilEndOfWeek = ((int)DayOfWeek.Sunday - (int)now.DayOfWeek + 7) % 7;
        DateTime endOfWeek = now.AddDays(daysUntilEndOfWeek).Date.AddDays(1).AddTicks(-1); // 이번 주 일요일의 끝

        TimeSpan timeUntilEndOfWeek = endOfWeek - now;
        return FormatTimeSpan(timeUntilEndOfWeek);
    }

    public static string GetTimeUntilEndOfMonth()
    {
        DateTime now = DateTime.UtcNow;
        DateTime endOfMonth = new DateTime(now.Year, now.Month, DateTime.DaysInMonth(now.Year, now.Month)).AddDays(1)
            .AddTicks(-1); // 이번 달의 마지막 날의 끝

        TimeSpan timeUntilEndOfMonth = endOfMonth - now;
        return FormatTimeSpan(timeUntilEndOfMonth);
    }

    private static string FormatTimeSpan(TimeSpan timeSpan)
    {
        int days = timeSpan.Days;
        int hours = timeSpan.Hours;
        int minutes = timeSpan.Minutes;
        int seconds = timeSpan.Seconds;

        if (days > 0)
        {
            return $"{days}D {hours}H {minutes}M {seconds}S";
        }

        return $"{hours}H {minutes}M {seconds}S";
    }

    public static string RandomNameGenerator()
    {
        List<string> firstNames = new List<string>()
        {
            "Liam", "Noah", "Oliver", "Elijah", "Mateo", "Lucas", "Levi", "Ezra", "Asher", "Leo", "James", "Henry",
            "Theodore", "William", "Mason", "Benjamin", "Grayson", "Jack", "Ethan", "Alexander", "Wyatt", "Josiah",
            "Samuel", "Aiden", "David", "Owen", "Ezekiel", "Julian", "Luke", "Carter", "Santiago", "Isaiah", "Miles",
            "Jayden", "Logan", "Isaac", "Matthew", "John", "Adam", "Nolan", "Nathan", "Caleb", "Joseph", "Cooper",
            "Thomas", "Anthony", "Micah", "Roman", "Lincoln", "Amir", "Olivia", "Emma", "Amelia", "Sophia", "Charlotte",
            "Isabella", "Ava", "Mia", "Luna", "Ellie", "Harper", "Camila", "Sofia", "Scarlett", "Elizabeth", "Eleanor",
            "Emily", "Chloe", "Mila", "Violet", "Penelope", "Gianna", "Aria", "Abigail", "Ella", "Avery", "Hazel",
            "Nora", "Layla", "Lily", "Aurora", "Nova", "Madison", "Grace", "Isla", "Zoe", "Riley", "Stella", "Eliana",
            "Ivy", "Victoria", "Emilia", "Zoey", "Naomi", "Hannah", "Lucy", "Elena", "Lillian", "Maya", "Leah",
        };

        List<string> lastNames = new List<string>()
        {
            "Smith", "Johnson", "Williams", "Brown", "Jones", "Garcia", "Miller", "Davis", "Rodriguez", "Martinez",
            "Hernandez", "Lopez", "Gonzalez", "Wilson", "Anderson", "Thomas", "Taylor", "Moore", "Jackson", "Martin",
            "Lee", "Perez", "Thompson", "White", "Harris", "Sanchez", "Clark", "Ramirez", "Lewis", "Robinson", "Walker",
            "Young", "Allen", "King", "Wright", "Scott", "Torres", "Nguyen", "Hill", "Flores", "Green", "Adams",
            "Nelson", "Baker", "Hall", "Rivera", "Campbell", "Mitchell", "Carter", "Roberts",
        };

        return firstNames[Random.Range(0, firstNames.Count)] + lastNames[Random.Range(0, lastNames.Count)];
    }
}

public static class Contant
{
    public const string BestScore = "BestScore";
    public const string Theme = "Theme";
}