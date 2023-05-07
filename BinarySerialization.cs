using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinSer
{
    public class BinarySerialization
    {
        public byte[] buffer => bufRaw.ToArray();
        public List<byte> bufRaw = new List<byte>();
        public void AddLong(long v)
        {
            byte[] b = BitConverter.GetBytes(v);
            for (int i = 0; i < b.Length; i++) bufRaw.Add(b[i]);
        }
        public void AddInt(int v)
        {
            byte[] b = BitConverter.GetBytes(v);
            for (int i = 0; i < b.Length; i++) bufRaw.Add(b[i]);
        }
        public void AddShort(short v)
        {
            byte[] b = BitConverter.GetBytes(v);
            for (int i = 0; i < b.Length; i++) bufRaw.Add(b[i]);
        }
        public void AddFloat(float v)
        {
            byte[] b = BitConverter.GetBytes(v);
            for (int i = 0; i < b.Length; i++) bufRaw.Add(b[i]);
        }
        public void AddDouble(double v)
        {
            byte[] b = BitConverter.GetBytes(v);
            for (int i = 0; i < b.Length; i++) bufRaw.Add(b[i]);
        }
        public void AddByte(byte v)
        {
            bufRaw.Add(v);
        }
        public void AddBool(bool v)
        {
            bufRaw.Add((byte)(v ? 1 : 0));
        }
        public void AddBytes(byte[] b)
        {
            for (int i = 0; i < b.Length; i++) bufRaw.Add(b[i]);
        }
        public void AddObject(object v)
        {
            switch (Type.GetTypeCode(v.GetType()))
            {
                case TypeCode.Int32: int t = (int)v; AddInt(t); break;
                case TypeCode.UInt32:    t = (int)v; AddInt(t); break;
                case TypeCode.Int16:     t = (short)v; AddShort((short)t); break;
                case TypeCode.UInt16:    t = (short)v; AddShort((short)t); break;
                case TypeCode.Single: float a = (float)v; AddFloat(a); break;
                case TypeCode.Double: double b = (double)v; AddDouble(b); break;
                case TypeCode.String: string c = (string)v; AddString(c); break;
            }
        }
        public void AddString(string s)
        {
            if (s == null)
            {
                AddShort(0);
                return;
            }
            byte[] b = Encoding.ASCII.GetBytes(s);
            AddShort((short)b.Length);
            for (int i = 0; i < b.Length; i++) bufRaw.Add(b[i]);
        }
    }
    public class BinaryDeserialization
    {
        public byte[] buffer;
        public int pos = 0;
        public long GetLong()
        {
            long r = BitConverter.ToInt64(buffer, pos);
            pos += sizeof(long);
            return r;
        }
        public int GetInt()
        {
            int r = BitConverter.ToInt32(buffer, pos);
            pos += sizeof(int);
            return r;
        }
        public short GetShort()
        {
            short r = BitConverter.ToInt16(buffer, pos);
            pos += sizeof(short);
            return r;
        }
        public float GetFloat()
        {
            float r = BitConverter.ToSingle(buffer, pos);
            pos += sizeof(float);
            return r;
        }
        public double GetDouble()
        {
            double r = BitConverter.ToDouble(buffer, pos);
            pos += sizeof(double);
            return r;
        }
        public byte GetByte()
        {
            return buffer[pos++];
        }
        public void GetBytes(int len, ref byte[] r)
        {
            for (int i = 0; i < len; i++) { r[i] = buffer[pos + i]; }
            pos += len;
        }
        public bool GetBool()
        {
            return buffer[pos++] != 0;
        }
        public string GetString()
        {
            short L = GetShort();
            string s = Encoding.ASCII.GetString(buffer, pos, L);
            pos += L;
            return s;
        }
    }
}
