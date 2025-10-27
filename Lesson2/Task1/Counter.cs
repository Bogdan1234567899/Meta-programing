using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Counter_homework
{
    public class Counter
    {
        private int _value;

        public int Value
        {
            get { return this._value; }
        }

        public Counter()
            : this(0)
        {
        }

        public Counter(int start)
        {
            if (start < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(start), "Лічильник не може стартувати з від'ємного значення.");
            }

            this._value = start;
        }

        public void Increment()
        {
            this._value = this._value + 1;
        }

        public void Decrement()
        {
            if (this._value == 0)
            {
                throw new InvalidOperationException("Не можна зменшити: значення вже 0.");
            }

            this._value = this._value - 1;
        }

        public bool TryDecrement()
        {
            if (this._value == 0)
            {
                return false;
            }
            else
            {
                this._value = this._value - 1;
                return true;
            }
        }

        public void Reset()
        {
            this._value = 0;
        }
    }
}
