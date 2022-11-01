// ---------------------------------------------------------------
// Copyright (c) Mabrouk Mahdhi. All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using System;

namespace TheStandardBox.Core.Brokers.DateTimes
{
    public class DateTimeBroker : IDateTimeBroker
    {
        public virtual DateTimeOffset GetCurrentDateTimeOffset() =>
           DateTimeOffset.UtcNow;

        public virtual bool IsStillRecent(DateTimeOffset date, int maxMinutes)
        {
            DateTimeOffset currentDateTime =
                GetCurrentDateTimeOffset();

            TimeSpan timeDifference = currentDateTime.Subtract(date);
            TimeSpan maxMinutesSpan = TimeSpan.FromMinutes(maxMinutes);

            return timeDifference.Duration() <= maxMinutesSpan;
        }
    }
}