// TimeInterval.cs
using System;

namespace ConsoleApp1
{
    public readonly struct TimeInterval : IEquatable<TimeInterval>
    {
        private readonly int startHours;
        private readonly int startMinutes;
        private readonly int endHours;
        private readonly int endMinutes;

        public TimeInterval(int startHours, int startMinutes, int endHours, int endMinutes)
        {
            if (startHours < 0 || startHours > 24 ||
                endHours < 0 || endHours > 24)
                throw new ArgumentException("Невірна година.");

            if (startMinutes < 0 || startMinutes >= 60 ||
                endMinutes < 0 || endMinutes >= 60)
                throw new ArgumentException("Невірні хвилини.");

            int beginTotal = startHours * 60 + startMinutes;
            int endTotal = endHours * 60 + endMinutes;

            if (beginTotal > endTotal)
                throw new ArgumentException("Початок після кінця.");

            this.startHours = startHours;
            this.startMinutes = startMinutes;
            this.endHours = endHours;
            this.endMinutes = endMinutes;
        }

        public TimeInterval(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                throw new ArgumentException("Порожній рядок.");

            var parts = text.Split('-');
            if (parts.Length != 2)
                throw new ArgumentException("Невірний формат рядка.");

            var partsBegin = parts[0].Split(':');
            var partsEnd = parts[1].Split(':');

            if (partsBegin.Length != 2 || partsEnd.Length != 2)
                throw new ArgumentException("Невірний формат часу.");

            int hoursBegin = int.Parse(partsBegin[0]);
            int minutesBegin = int.Parse(partsBegin[1]);

            int hoursEnd = int.Parse(partsEnd[0]);
            int minutesEnd = int.Parse(partsEnd[1]);

            if (hoursBegin < 0 || hoursBegin > 24 ||
                hoursEnd < 0 || hoursEnd > 24)
                throw new ArgumentException("Невірна година.");

            if (minutesBegin < 0 || minutesBegin >= 60 ||
                minutesEnd < 0 || minutesEnd >= 60)
                throw new ArgumentException("Невірні хвилини.");

            int beginTotal = hoursBegin * 60 + minutesBegin;
            int endTotal = hoursEnd * 60 + minutesEnd;

            if (beginTotal > endTotal)
                throw new ArgumentException("Початок після кінця.");

            this.startHours = hoursBegin;
            this.startMinutes = minutesBegin;
            this.endHours = hoursEnd;
            this.endMinutes = minutesEnd;
        }

        private int StartTotalMinutes => startHours * 60 + startMinutes;
        private int EndTotalMinutes => endHours * 60 + endMinutes;

        public int Length()
        {
            return EndTotalMinutes - StartTotalMinutes;
        }

        public bool Overlaps(TimeInterval other)
        {
            return !(other.EndTotalMinutes < this.StartTotalMinutes ||
                     other.StartTotalMinutes > this.EndTotalMinutes);
        }

        public bool Overlaps(int minuteTotal)
        {
            return minuteTotal >= this.StartTotalMinutes && minuteTotal <= this.EndTotalMinutes;
        }

        public int this[int i]
        {
            get
            {
                return i switch
                {
                    0 => StartTotalMinutes,
                    1 => EndTotalMinutes,
                    _ => throw new ArgumentOutOfRangeException(nameof(i), "Index must be 0 or 1.")
                };
            }
        }

        public int this[string name]
        {
            get
            {
                if (name == null)
                    throw new ArgumentNullException(nameof(name));

                var lowered = name.ToLowerInvariant();
                return lowered switch
                {
                    "start" => StartTotalMinutes,
                    "end" => EndTotalMinutes,
                    _ => throw new ArgumentOutOfRangeException(nameof(name), "Use \"start\" or \"end\".")
                };
            }
        }

        public override string ToString()
        {
            return $"[{this.startHours:D2}:{this.startMinutes:D2}-{this.endHours:D2}:{this.endMinutes:D2}]";
        }

        public override bool Equals(object obj)
        {
            if (obj is TimeInterval other)
            {
                return Equals(other);
            }
            return false;
        }

        public bool Equals(TimeInterval other)
        {
            return this.startHours == other.startHours &&
                   this.startMinutes == other.startMinutes &&
                   this.endHours == other.endHours &&
                   this.endMinutes == other.endMinutes;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(startHours, startMinutes, endHours, endMinutes);
        }

        public static bool operator ==(TimeInterval a, TimeInterval b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(TimeInterval a, TimeInterval b)
        {
            return !a.Equals(b);
        }

        public static TimeInterval operator +(TimeInterval a, TimeInterval b)
        {
            int startMin = Math.Min(a.StartTotalMinutes, b.StartTotalMinutes);
            int endMin = Math.Max(a.EndTotalMinutes, b.EndTotalMinutes);

            int sh = startMin / 60;
            int sm = startMin % 60;
            int eh = endMin / 60;
            int em = endMin % 60;

            return new TimeInterval(sh, sm, eh, em);
        }

        public static TimeInterval operator *(TimeInterval a, TimeInterval b)
        {
            int startMin = Math.Max(a.StartTotalMinutes, b.StartTotalMinutes);
            int endMin = Math.Min(a.EndTotalMinutes, b.EndTotalMinutes);

            if (startMin > endMin)
                throw new InvalidOperationException("Intervals do not intersect.");

            int sh = startMin / 60;
            int sm = startMin % 60;
            int eh = endMin / 60;
            int em = endMin % 60;

            return new TimeInterval(sh, sm, eh, em);
        }

        public static explicit operator int(TimeInterval t)
        {
            return t.Length();
        }
    }
}
