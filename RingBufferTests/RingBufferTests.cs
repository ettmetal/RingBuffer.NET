using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RingBuffer;

namespace RingBufferTests {
    [TestClass]
    public class RingBufferTests {

        private int iterations = 1000;

        /// <summary>
        /// Ensures that size is correctly augmented when items are added.
        /// </summary>
        [TestMethod()]
        public void PutIncrementsSize() {
            RingBuffer<int> _buffer = new RingBuffer<int>();
            for(int i = 0; i < iterations; i++) {
                int _tmp = i;
                _buffer.Put(_tmp);
                Assert.AreEqual(i + 1, _buffer.Size, "Size is not equal to number of elements added.");
            }
        }

        /// <summary>
        /// Ensures that size is correctly adjusted when elements are removed.
        /// </summary>
        [TestMethod()]
        public void GetDecrementsSize() {
            RingBuffer<int> _buffer = new RingBuffer<int>();
            populateBuffer(iterations, _buffer);
            for(int i = iterations; i > 0; i--) {
                int _tmp = _buffer.Get();
                Assert.AreEqual(i-1, _buffer.Size, "Size does not reflect the correct number of removed elements.");
            }
        }

        /// <summary>
        /// Ensures that capacity expands as needed
        /// </summary>
        [TestMethod()]
        public void CapacityExpands() {
            int _startCapacity = 12;
            RingBuffer<double> _testBuffer = new RingBuffer<double>(_startCapacity);
            for(int i = 0; i < _startCapacity + 1; i++) {
                _testBuffer.Put((double)i);
            }
            Assert.AreEqual(_startCapacity * 2, _testBuffer.Capacity, "Capacity not expanded");
            Assert.AreEqual(_startCapacity + 1, _testBuffer.Size, "incorrect number of elements");
        }

        /// <summary>
        /// Ensures that head/tail move properly by testing the value of data
        /// returned by get.
        /// </summary>
        [TestMethod()]
        public void RetrievedInCorrectOrder() {
            int _iterations = 10;
            RingBuffer<int> _buffer = new RingBuffer<int>();
            populateBuffer(iterations, _buffer);
            for(int i = 0; i < _iterations; i++) {
                int _tmp = _buffer.Get();
                Assert.AreEqual(i, _tmp, "Incorrect Sequence");
            }
        }

        /// <summary>
        /// Ensures that an exception is thrown when Get() is called on an
        /// empty buffer.
        /// </summary>
        [TestMethod(), ExpectedException(typeof(InvalidOperationException))]
        public void ThrowsError_GetEmpty() {
            RingBuffer<byte> buffer = new RingBuffer<byte>();
            byte _tmp = buffer.Get();
        }

        /// <summary>
        /// Ensures that foreach iteration covers only the range of active
        /// items
        /// </summary>
        [TestMethod()]
        public void CanIterateForeach() {
            RingBuffer<int> buffer = new RingBuffer<int>();
            populateBuffer(iterations, buffer);
            int _iterations = 0;
            foreach(int i in buffer){
                _iterations++;
            }
            Assert.AreEqual(iterations, _iterations, "Wrong number of foreach iterations.");
        }

        private void populateBuffer(int elements, RingBuffer<int> buffer) {
            for(int i = 0; i < elements; i++) {
                buffer.Put(i);
            }
        }
    }
}
