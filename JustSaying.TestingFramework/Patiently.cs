﻿using System;
using System.Threading.Tasks;
using NUnit.Framework;

namespace JustSaying.TestingFramework
{
    public static class Patiently
    {
        public static async Task AssertThatAsync(Func<bool> func) => await AssertThatAsync(func, 5.Seconds());

        public static async Task AssertThatAsync(Func<bool> func, TimeSpan timeout)
        {
            var started = DateTime.Now;
            var timeoutAt = DateTime.Now + timeout;
            do
            {
                if (func.Invoke())
                {
                    return;
                }

                await Task.Delay(50.Milliseconds());
                Console.WriteLine(
                    $"Waiting for {(DateTime.Now - started).TotalMilliseconds} ms - Still Checking.");
            } while (DateTime.Now < timeoutAt);

            Assert.True(func.Invoke());
        }
    }
    public static class Extensions
    {
        public static TimeSpan Seconds(this int n) => TimeSpan.FromSeconds(n);

        public static TimeSpan Milliseconds(this int n) => TimeSpan.FromMilliseconds(n);
    }
}
