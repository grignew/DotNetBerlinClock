using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BerlinClock
{
    public class TimeConverter : ITimeConverter
    {
        const char EMPTY = 'O';
        const char YELLOW = 'Y';
        const char RED = 'R';

        private enum clockPart
        {
            TwoSeconds = 0
            ,FiveHours = 1
            ,OneHour = 2
            ,FiveMinutes = 3
            , OneMinute = 4
        }

        private char[][] _clock;

        private string[] _inputTime;
        private int _seconds;
        private int _minutes;
        private int _hours;

        private void InitClock(string aTime)
        {
            _clock = new char[5][];
            _clock[(int)clockPart.TwoSeconds] = Enumerable.Range(0, 1).Select(_ => EMPTY).ToArray();
            _clock[(int)clockPart.FiveHours] = Enumerable.Range(0, 4).Select(_ => EMPTY).ToArray();
            _clock[(int)clockPart.OneHour] = Enumerable.Range(0, 4).Select(_ => EMPTY).ToArray();
            _clock[(int)clockPart.FiveMinutes] = Enumerable.Range(0, 11).Select(_ => EMPTY).ToArray();
            _clock[(int)clockPart.OneMinute] = Enumerable.Range(0, 4).Select(_ => EMPTY).ToArray();

            _inputTime = aTime.Split(':');

            _hours = Convert.ToInt16(_inputTime[0]);
            _minutes = Convert.ToInt16(_inputTime[1]);
            _seconds = Convert.ToInt16(_inputTime[2]);
        }

        public string convertTime(string aTime)
        {
            InitClock(aTime);

            _clock[(int)clockPart.TwoSeconds][0] = _seconds % 2 == 0 ? YELLOW : EMPTY;


            for (int i = 0; i < _hours / 5; i++)
                _clock[(int)clockPart.FiveHours][i] = RED;

            for (int i = 0; i < _hours - _hours / 5 * 5; i++)
                _clock[(int)clockPart.OneHour][i] = RED;

            for (int i = 1; i <= _minutes / 5; i++)
                _clock[(int)clockPart.FiveMinutes][i - 1] = (i < 3 || i % 3 > 0) ? YELLOW : RED;

            for (int i = 0; i < _minutes - _minutes / 5 * 5; i++)
                _clock[(int)clockPart.OneMinute][i] = YELLOW;


            return String.Join("\r\n", _clock.Select(p => new string(p)));
        }
    }
}
