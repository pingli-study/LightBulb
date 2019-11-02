﻿using System;
using System.Collections.Generic;
using System.Text;
using Tyrrrz.Extensions;

namespace LightBulb.Internal
{
    internal static class Extensions
    {
        public static double StepTo(this double from, double to, double absStep)
        {
            absStep = Math.Abs(absStep);
            return to >= from ? (from + absStep).ClampMax(to) : (from - absStep).ClampMin(to);
        }

        public static DateTimeOffset StepTo(this DateTimeOffset from, DateTimeOffset to, TimeSpan absStep)
        {
            absStep = absStep.Duration();
            return to >= from ? (from + absStep).ClampMax(to) : (from - absStep).ClampMin(to);
        }

        public static void AddRange<T>(this ICollection<T> collection, IEnumerable<T> items)
        {
            foreach (var item in items)
                collection.Add(item);
        }

        public static StringBuilder AppendIfNotEmpty(this StringBuilder builder, char value) =>
            builder.Length > 0 ? builder.Append(value) : builder;

        public static DateTimeOffset ResetTimeOfDay(this DateTimeOffset dateTime) =>
            new DateTimeOffset(dateTime.Year, dateTime.Month, dateTime.Day, 0, 0, 0, 0, dateTime.Offset);

        public static DateTimeOffset AtTimeOfDay(this DateTimeOffset dateTime, TimeSpan timeOfDay) =>
            dateTime.ResetTimeOfDay() + timeOfDay;

        public static DateTimeOffset NextTimeOfDay(this DateTimeOffset dateTime, TimeSpan timeOfDay) =>
            dateTime.TimeOfDay <= timeOfDay
                ? dateTime.AtTimeOfDay(timeOfDay)
                : dateTime.AddDays(1).AtTimeOfDay(timeOfDay);

        public static DateTimeOffset PreviousTimeOfDay(this DateTimeOffset dateTime, TimeSpan timeOfDay) =>
            dateTime.TimeOfDay > timeOfDay
                ? dateTime.AtTimeOfDay(timeOfDay)
                : dateTime.AddDays(-1).AtTimeOfDay(timeOfDay);

        public static void OpenInBrowser(this Uri uri) => ProcessEx.StartShellExecute(uri.ToString());
    }
}