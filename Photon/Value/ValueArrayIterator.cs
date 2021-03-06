﻿using System.Collections.Generic;

namespace Photon
{
    class ValueArrayIterator : ValueIterator
    {
        IEnumerator<Value> _data;
        int _index;

        public ValueArrayIterator( Array arr )
        {
            _data = arr.Raw.GetEnumerator();
            _index = 0;
            _data.MoveNext();
        }

        internal override void Next()
        {
            _data.MoveNext();
            _index++;
        }

        // k, v, iter = ITER( x, iter )
        internal override bool Iterate(DataStack ds)
        {
            if (_data.Current == null)
                return false;

            ds.Push(this); 
            ds.Push(_data.Current);
            ds.Push(new ValueInteger32(_index));

            return true;
        }

        public IEnumerator<Value> RawValue
        {
            get { return _data; }
        }

        internal override object Raw
        {
            get { return _data; }
        }        

        public override bool Equals(object other)
        {
            var otherT = other as ValueArrayIterator;
            if (otherT == null)
                return false;

            return otherT._data.Equals(_data);
        }

        public override int GetHashCode()
        {
            return _data.GetHashCode();
        }

        public override string DebugString()
        {
            return string.Format("'{0}' (arr iterator)", _data.Current);
        }

        public override ValueKind Kind
        {
            get { return ValueKind.ArrayIterator; }
        }
    }



 
}
