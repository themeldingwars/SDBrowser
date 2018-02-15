using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using static FauFau.Util.BinaryStream;

namespace FauFau.Util
{
    public class BinaryWrapper
    {
        private Endianness bitOrder = Endianness.LittleEndian;
        private Endianness byteOrder = Endianness.LittleEndian;
        private TextEncoding defaultTextEncoding = TextEncoding.DEFAULT;

        /// <summary>
        /// Order of the bits to be read & written in.
        /// </summary>
        public Endianness BitOrder
        {
            get { return bitOrder; }
            set { bitOrder = value; }
        }

        /// <summary>
        /// Order of the bytes to be read & written in.
        /// </summary>
        public Endianness ByteOrder
        {
            get { return byteOrder; }
            set { byteOrder = value; }
        }

        /// <summary>
        /// Default encoding to be used on string.
        /// </summary>
        public TextEncoding DefaultTextEncoding
        {
            get { return defaultTextEncoding; }
            set
            {
                defaultTextEncoding = value;
            }
        }

        /// <summary>
        /// Reads a binary structure from a file.
        /// </summary>
        public void Read(string file)
        {
            using (BinaryStream bs = new BinaryStream(File.Open(file, FileMode.Open), byteOrder, bitOrder, defaultTextEncoding))
            {
                Read(bs);
            }
        }

        /// <summary>
        /// Reads a binary structure from a stream.
        /// </summary>
        public void Read(Stream stream)
        {
            using (BinaryStream bs = new BinaryStream(stream, byteOrder, bitOrder, defaultTextEncoding))
            {
                Read(bs);
            }
        }

        /// <summary>
        /// Reads a binary structure from a byte array.
        /// </summary>
        public void Read(byte[] bytes)
        {
            using (BinaryStream bs = new BinaryStream(new MemoryStream(bytes), byteOrder, bitOrder, defaultTextEncoding))
            {
                Read(bs);
            }
        }

        /// <summary>
        /// Reads a binary structure from a BinaryStream. Override this one.
        /// </summary>
        public virtual void Read(BinaryStream bs)
        {

        }

        /// <summary>
        /// Writes a binary structure to a file.
        /// </summary>
        public void Write(string file)
        {
            using (BinaryStream bs = new BinaryStream(File.Open(file, FileMode.OpenOrCreate), byteOrder, bitOrder, defaultTextEncoding))
            {
                Write(bs);
            }
        }

        /// <summary>
        /// Writes a binary structure to a stream.
        /// </summary>
        public void Write(Stream stream)
        {
            using (BinaryStream bs = new BinaryStream(stream, byteOrder, bitOrder, defaultTextEncoding))
            {
                Write(bs);
            }
        }

        /// <summary>
        /// Writes a binary structure to a byte array.
        /// </summary>
        public void Write(out byte[] bytes)
        {
            using (MemoryStream ms = new MemoryStream())
            using (BinaryStream bs = new BinaryStream(ms, byteOrder, bitOrder, defaultTextEncoding))
            {
                Write(bs);
                bytes = ms.ToArray();
            }
        }

        /// <summary>
        /// Writes a binary structure to a BinaryStream. Override this one.
        /// </summary>
        public virtual void Write(BinaryStream bs)
        {

        }

        public interface ReadWrite
        {
            void Read(BinaryStream bs);
            void Write(BinaryStream bs);
        }

    }
}
