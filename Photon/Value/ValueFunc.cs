﻿
using MarkSerializer;
using SharpLexer;
namespace Photon
{
    class ValueFunc : Value
    {
        internal int ID = -1;
        
        ObjectName _name;

        public ObjectName Name
        {
            get { return _name; }
        }

        // 传入参数数量
        internal int InputValueCount { get; set; }

        // 返回值数量
        internal int OutputValueCount { get; set; }


        public override void Serialize(BinarySerializer ser)
        {
            ser.Serialize(ref ID);
            ser.Serialize(ref _name);
        }

        public ValueFunc()
        {

        }

        internal ValueFunc(ObjectName name)
        {
            _name = name;
        }

        internal virtual bool Invoke(VMachine vm, int argCount, int receiverCount, ValueClosure closure)
        {
            return false;
        }

        internal virtual void DebugPrint(Executable exe )
        {

        }
        
        public override ValueKind Kind
        {
            get { return ValueKind.Func; }
        }
    }

 
}
