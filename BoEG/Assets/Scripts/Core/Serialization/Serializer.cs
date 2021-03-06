using System;
using System.IO;

namespace Core.Serialization
{
	
	
	public class Serializer : ISerializer, IDisposable
	{
		public Serializer()
		{	
			_stream = new MemoryStream();
			_writer = new BinaryWriter(_stream);
		}
	
		private readonly MemoryStream _stream;
		private readonly BinaryWriter _writer;
	
		public void Dispose()
		{
			_stream.Dispose();
			((IDisposable)_writer).Dispose();
		}
	
		public void Write(bool value)
		{
			//byte
			Write(BitConverter.GetBytes(value));
		}
		public void Write(byte value)
		{
			_writer.Write(value);
		}
		public void Write(sbyte value)
		{
			_writer.Write(value);
		}
		public void Write(byte[] value)
		{
			_writer.Write(value);
		}
		public void Write(byte[] value, int start, int len)
		{
			_writer.Write(value,start,len);
		}
		public void Write(char value)
		{
			_writer.Write(value);
		}
		public void Write(char[] value)
		{
			_writer.Write(value);
		}
		public void Write(char[] value, int start, int len)
		{
			_writer.Write(value, start, len);
		}

		public void Write(string value)
		{
			_writer.Write(value);
		}

		public void Write(short value)
		{
			_writer.Write(value);
		}
		public void Write(ushort value)
		{
			_writer.Write(value);
		}
		public void Write(float value)
		{
			_writer.Write(value);
		}
		public void Write(int value)
		{
			_writer.Write(value);
		}
		public void Write(uint value)
		{
			_writer.Write(value);
		}
		public void Write(double value)
		{
			_writer.Write(value);
		}
		public void Write(long value)
		{		
			_writer.Write(value);
		}
		public void Write(ulong value)
		{
			_writer.Write(value);
		}
		public void Write(decimal value)
		{
			_writer.Write(value);
		}
		public byte[] Buffer()
		{
			return _stream.ToArray();
		}
	}
}



